'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Faiber Julian Mora D.
' Created          : 07-10-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ICircularZeroThirtyAdminService
    Inherits IDisposable

    Function ListTrimesters() As Dictionary(Of Int32, String)

    Function GenerateDocument030(ByVal year As Int32, ByVal trimester As Int32, ByVal audit As AuditMessage) As ActionResult(Of List(Of GenerateDocument030_Result))

End Interface
