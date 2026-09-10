'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 04-12-2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCHISPACARepository
    Inherits IRepository(Of HCHISPACA)

    Function GetHCHISPACAByID(id As Integer) As HCHISPACA

End Interface