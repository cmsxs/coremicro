#!/bin/bash
cd /pipeline/source/app/publish
dotnet run --server.urls=http://*:8080
