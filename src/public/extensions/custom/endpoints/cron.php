<?php

use Directus\Application\Http\Request;
use Directus\Application\Http\Response;
use Directus\Services\FilesServices;
use Directus\Services\ItemsService;

// http://localhost:8881/_/custom/cron/news

function mova21_http_get_json($url) {
	$client = new GuzzleHttp\Client();
	$res = $client->request('GET', $url);
	if ($res->getStatusCode() !== 200) {
		throw new Exception('Invalid Response from Wordpress: '. $res->getStatusCode() . ' ' . $res->getBody());
	}
	return json_decode($res->getBody(), true);
}

return [
	'/news' => [
		'method' => 'GET',
		'handler' => function (Request $request, Response $response) {

			/** @var $this Directus\Application\Container */

			// TODO: move to settings
			$MOVA_WP_URL = 'https://www.mova.ch/wp-json/wp/v2/posts';

			$itemsService = new ItemsService($this);
			$filesService = new FilesServices($this);

			$languages = ['de', 'fr', 'it', 'en'];

			$total_posts = 0;
			$created_count = 0;
			$updated_count = 0;

			foreach ($languages as $lang) {
				$data = mova21_http_get_json($MOVA_WP_URL . '?_embed=true&lang=' . $lang);
				if (count($data)) {
					foreach ($data as $entry) {
						$total_posts++;
						$existingPost = null;
						try {
							$existingPost = $itemsService->findOne('news', ['filter' => ['wp_post_id' => $entry['id']]]);
						} catch (Exception $e) {}
						if ($existingPost) {
							// update if needed
							if (
								$existingPost['data']['language'] !== $lang or
								$existingPost['data']['title'] !== $entry['title']['rendered'] or
								$existingPost['data']['content'] !== $entry['content']['rendered'] or
								$existingPost['data']['excerpt'] !== $entry['excerpt']['rendered'] or
								$existingPost['data']['date'] !== date('Y-m-d H:i:s', strtotime($entry['date'])) or
								$existingPost['data']['image_wp_id'] !== $entry['featured_media']
							) {
								// update post
								$data = [
									"date" => date('Y-m-d H:i:s', strtotime($entry['date'])),
									"wp_post_id" => $entry['id'],
									"language" => $lang,
									"title" => $entry['title']['rendered'],
									"content" => $entry['content']['rendered'],
									"excerpt" => $entry['excerpt']['rendered'],
									"image_wp_id" => $entry['featured_media'],
									"image" => null
								];

								// only update image if needed
								if ($existingPost['data']['image_wp_id'] !== $entry['featured_media']) {
									$file = null;
									if (isset($entry['_embedded']['wp:featuredmedia'][0]['source_url'])) {
										$image_url = $entry['_embedded']['wp:featuredmedia'][0]['source_url'];
										$file = $filesService->create([
											'data' => $image_url
										]);
										$data['image_wp_id'] = $entry['featured_media'];
										$data['image'] = $file['data']['id'];
									} else {
										$data['image_wp_id'] = $entry['featured_media'];
										$data['image'] = null;
									}
								}

								$itemsService->update('news', $existingPost['data']['id'], $data);
								$updated_count++;
							}
						} else {
							// load first featured media image
							$file = null;
							if (isset($entry['_embedded']['wp:featuredmedia'][0]['source_url'])) {
								$image_url = $entry['_embedded']['wp:featuredmedia'][0]['source_url'];
								$file = $filesService->create([
									'data' => $image_url
								]);
							}

							$data = [
								"status" => "published",
								"date" => date('Y-m-d H:i:s', strtotime($entry['date'])),
								"wp_post_id" => $entry['id'],
								"language" => $lang,
								"title" => $entry['title']['rendered'],
								"content" => $entry['content']['rendered'],
								"excerpt" => $entry['excerpt']['rendered'],
								"image_wp_id" => $entry['featured_media'],
								"image" => $file ? $file['data']['id'] : null
							];

							$itemsService->createItem('news', $data);
							$created_count++;
						}
					}
				}
			}

			// TODO: delete removed news

			// return stats
			return $response->withJson(['posts' => $total_posts, 'created' => $created_count, 'updated' => $updated_count]);
		}
	]
];
