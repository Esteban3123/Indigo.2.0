@echo off
title Habilitando el control de concurrencia...

FixEFConcurrencyModes.exe -i InteropCostModel.edmx -t timestamp

pause