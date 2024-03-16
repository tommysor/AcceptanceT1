#!/bin/bash

pushd ./spesifications
dotnet build
popd
pwsh spesifications/DealWithSpesification/bin/Debug/net9.0/playwright.ps1 install --with-deps
