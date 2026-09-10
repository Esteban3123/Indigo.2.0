'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCORDPROQRepository
    Inherits IRepository(Of HCORDPROQ)

    Function GetHCORDPROQByAuto(auto As Integer, tracking As Boolean) As HCORDPROQ

End Interface