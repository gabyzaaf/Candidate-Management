#!/bin/bash
atq
at $1 $2 <<< "/usr/bin/dotnet $3 $4 $5 $6 $7 $8"
