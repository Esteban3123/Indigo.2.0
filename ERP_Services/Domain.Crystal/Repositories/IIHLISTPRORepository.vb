'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IIHLISTPRORepository
    Inherits IRepository(Of IHLISTPRO)

    Function GetIHLISTPROByCode(code As String) As IHLISTPRO

    Function GetIHLISTPROwithTrackingByCode(code As String) As IHLISTPRO

End Interface
