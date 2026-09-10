'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCPROCTERRepository
    Inherits IRepository(Of HCPROCTER)

    Function GetHCPROCTERByAuto(auto As Integer) As HCPROCTER

End Interface
