FROM directus/directus:v8-apache
COPY ./src/public/extensions/custom /var/directus/public/extensions/custom
