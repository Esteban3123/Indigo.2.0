@echo off
title Habilitando el control de concurrencia...

FixEFConcurrencyModes.exe -i PayrollModel.edmx -t timestamp

pause