#!/bin/bash

pushd ./spesifications
SPESIFICATIONS_BASEADDRESS=http://localhost:5041 dotnet test
popd
