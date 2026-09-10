'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCORDINTERepository
    Inherits IRepository(Of HCORDINTE)

    Function GetHCORDINTEByAuto(auto As Integer) As HCORDINTE

End Interface