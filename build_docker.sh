#!/usr/bin/env bash

export VERSION=v3

SHELLDIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd $SHELLDIR

docker build . -t demianh/directus-app-backend:$VERSION --no-cache

#docker push demianh/directus-app-backend:$VERSION

echo -e ""
echo -e "Run the following command to push the image:"
echo -e "docker push demianh/directus-app-backend:$VERSION"
echo -e ""
