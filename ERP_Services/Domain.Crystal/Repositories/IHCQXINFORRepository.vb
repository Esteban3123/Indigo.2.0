'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCQXINFORRepository
    Inherits IRepository(Of HCQXINFOR)

    Function GetHCQXINFORByAuto(auto As Integer, tracking As Boolean) As HCQXINFOR

End Interface