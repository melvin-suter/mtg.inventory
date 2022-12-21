#!/bin/bash

FRONTEND_VERSION=1.0.1
BACKEND_VERSION=1.0.0

cd mtg.inventory.backend

dotnet publish --os linux --arch x64 -c Release -p:PublishProfile=DefaultContainer

docker tag mtg_inventory_backend:$BACKEND_VERSION suterdev/mtg.inventory.backend:$BACKEND_VERSION
docker tag mtg_inventory_backend:$BACKEND_VERSION suterdev/mtg.inventory.backend:latest
docker push suterdev/mtg.inventory.backend:$BACKEND_VERSION
docker push suterdev/mtg.inventory.backend:latest





cd ../mtg.inventory.frontend

npm i
ng build

docker build -t suterdev/mtg.inventory.frontend:$FRONTEND_VERSION .
docker tag suterdev/mtg.inventory.frontend:$FRONTEND_VERSION suterdev/mtg.inventory.frontend:latest
docker push suterdev/mtg.inventory.frontend:$FRONTEND_VERSION
docker push suterdev/mtg.inventory.frontend:latest

