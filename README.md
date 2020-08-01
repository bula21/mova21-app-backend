# Custom Directus App Backend Container

The code to build the custom app backend docker image. Based on [directus/directus:v8-apache](https://hub.docker.com/r/directus/directus)

## Custom Endpoints

Cronjob to update News from Wordpress:

    /_/custom/cron/news

## Building the custom Docker image

Update the $VERSION in build_docker.sh and run:

    ./build_docker.sh

## Run the dev environment for testing / development

    docker-compose up -d

#### Credentials 

```
Login: http://localhost:8881
Username: admin@example.com
Password: admin
```

## Developer Hints

### Custom Endpoints

API URL Schema:

`/src/public/extensions/custom/endpoints/hello.php` is available at [http://localhost:8881/_/custom/hello]()


## Docker Commands

### Populate Empty Database

    docker-compose run --rm directus install --email admin@example.com --password admin

### Connect to MySQL console

    docker exec -it mova21_mysql_1 mysql -u root -p

### Import DB Dump

    docker exec -i mova21_mysql_1 mysql -u root -p directus < directus_db.sql
