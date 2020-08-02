FROM directus/directus:v8-apache

# copy custom code
COPY ./src/public/extensions/custom /var/directus/public/extensions/custom

# install additional composer dependencies
WORKDIR /var/directus
COPY composer.phar ./
RUN php composer.phar require league/html-to-markdown
RUN rm composer.phar
