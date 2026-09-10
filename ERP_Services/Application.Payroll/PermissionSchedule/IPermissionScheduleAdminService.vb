'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 19-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPermissionScheduleAdminService
    Inherits IDisposable

    Function SavePermissionRol(ListPermissionRol As List(Of PositionRoll), ListPermissionRolDelete As List(Of PositionRoll), audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of List(Of PositionRoll))

    Function GetPermissionRoll(CodeRoll As String, RolId As Integer) As List(Of PositionRoll)

    Function SavePermissionUser(ListPermissionUser As List(Of PositionUser), ListDeletePermissionUser As List(Of PositionUser), ListPermissionFunctionalUnit As List(Of FunctionalUnitResponsible), ListDeletePermissionFunctionalUnit As List(Of FunctionalUnitResponsible), audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of String)

    Function GetPositionUser(UserId As Integer) As List(Of PositionUser)

    Function GetFunctionalUnitResponsible(UserId As Integer) As List(Of FunctionalUnitResponsible)

End Interface
