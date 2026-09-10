'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCHOGASINRepository
    Inherits IRepository(Of HCHOGASIN)

    Function GetHCHOGASINByAuto(auto As Integer) As HCHOGASIN

End Interface