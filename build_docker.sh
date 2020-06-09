#!/usr/bin/env bash

export VERSION=v1

SHELLDIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd $SHELLDIR

docker build . -t demianh/directus-app-backend:$VERSION --no-cache

#docker push demianh/directus-app-backend:$VERSION
