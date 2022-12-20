#!/bin/bash

cd mtg.inventory.backend

dotnet publish --os linux --arch x64 -c Release -p:PublishProfile=DefaultContainer

docker tag mtg_inventory_backend:1.0.0 suterdev/mtg.inventory.backend:1.0.0
docker tag mtg_inventory_backend:1.0.0 suterdev/mtg.inventory.backend:latest
docker push suterdev/mtg.inventory.backend:1.0.0
docker push suterdev/mtg.inventory.backend:latest





cd ../mtg.inventory.frontend

npm i
ng build

docker build -t suterdev/mtg.inventory.frontend:1.0.0 .
docker tag suterdev/mtg.inventory.frontend:1.0.0 suterdev/mtg.inventory.frontend:latest
docker push suterdev/mtg.inventory.frontend:1.0.0
docker push suterdev/mtg.inventory.frontend:latest

