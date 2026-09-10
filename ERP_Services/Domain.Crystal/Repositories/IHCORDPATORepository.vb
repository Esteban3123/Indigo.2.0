'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCORDPATORepository
    Inherits IRepository(Of HCORDPATO)

    Function GetHCORDPATOByAuto(auto As Integer) As HCORDPATO

End Interface