<?php

use Directus\Application\Http\Request;
use Directus\Application\Http\Response;

return [
    '/hello' => [
        'method' => 'GET',
        'handler' => function (Request $request, Response $response) {
            // Simple GET endpoint example
            return $response->withJson(['hello' => 'world']);
        }
    ]
];
