@echo off
title Habilitando el control de concurrencia...

FixEFConcurrencyModes.exe -i SecurityModel.edmx -t timestamp

pause