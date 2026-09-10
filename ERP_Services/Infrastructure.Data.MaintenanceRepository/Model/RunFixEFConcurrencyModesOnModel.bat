@echo off
title Habilitando el control de concurrencia...

FixEFConcurrencyModes.exe -i MaintenanceModel.edmx -t timestamp

pause