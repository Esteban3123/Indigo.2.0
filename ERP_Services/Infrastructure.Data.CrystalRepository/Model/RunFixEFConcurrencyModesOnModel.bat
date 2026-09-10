@echo off
title Habilitando el control de concurrencia...

FixEFConcurrencyModes.exe -i CrystalModel.edmx -t timestamp

pause