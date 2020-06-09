# Building the custom Docker

Update the VERSION in build_docker.sh and run:

    ./build_docker.sh


# Run the dev environment for testing / development

    docker-compose up -d
    docker-compose run directus install --email admin@example.com --password admin

### Credentials 

| Local Login Url: | http://localhost:8881 |
| Username: | admin@example.com |
| Password: | admin |

For more details see: https://docs.directus.io/installation/docker.html

