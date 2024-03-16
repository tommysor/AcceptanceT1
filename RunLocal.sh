#!/bin/bash

pushd ./src
dotnet build
dotnet run --project DealWith.AppHost/
popd
