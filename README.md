# Custom Directus App Backend Container

The code to build the custom app backend docker image.

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

`/src/public/extensions/custom/endpoints/hello.php` ist unter [http://localhost:8881/_/custom/hello]() erreichbar
