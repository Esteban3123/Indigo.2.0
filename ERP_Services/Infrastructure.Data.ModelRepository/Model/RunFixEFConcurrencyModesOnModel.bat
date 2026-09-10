@echo off
title Habilitando el control de concurrencia...

FixEFConcurrencyModes.exe -i GlobalModel.edmx -t timestamp

pause