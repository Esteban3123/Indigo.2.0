@echo off
title Habilitando el control de concurrencia...

FixEFConcurrencyModes.exe -i DocumentalSystemModel.edmx -t timestamp

pause