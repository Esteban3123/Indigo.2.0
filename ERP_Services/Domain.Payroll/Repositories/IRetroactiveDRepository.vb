'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Oscar stiven Astudillo reyes
' Created          : 2025-10-16
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IRetroactiveDRepository
    Inherits IRepository(Of RetroactiveD)

End Interface