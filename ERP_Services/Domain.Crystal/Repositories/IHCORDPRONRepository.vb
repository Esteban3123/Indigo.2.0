'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCORDPRONRepository
    Inherits IRepository(Of HCORDPRON)

    Function GetHCORDPRONByAuto(auto As Integer) As HCORDPRON

End Interface