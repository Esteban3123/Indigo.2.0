'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 3-12-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports System.Text
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IPayrollPermissionSchedule

    <OperationContract()>
    Function GetPermissionRoll(CodeRoll As String, RolId As Integer, session As SessionValues) As List(Of PositionRoll)

    <OperationContract()>
    Function SavePermissionRol(ListPermissionRol As List(Of PositionRoll), ListPermissionRolDelete As List(Of PositionRoll), session As SessionValues) As ActionResult(Of List(Of PositionRoll))

    <OperationContract()>
    Function GetPositionUser(UserId As Integer, session As SessionValues) As List(Of PositionUser)

    <OperationContract()>
    Function GetFunctionalUnitResponsible(UserId As Integer, session As SessionValues) As List(Of FunctionalUnitResponsible)

    <OperationContract()>
    Function SavePermissionUser(ListPermissionUser As List(Of Domain.Payroll.Entities.PositionUser), ListDeletePermissionUser As List(Of Domain.Payroll.Entities.PositionUser), ListPermissionFunctionalUnit As List(Of Domain.Payroll.Entities.FunctionalUnitResponsible), ListDeletePermissionFunctionalUnit As List(Of Domain.Payroll.Entities.FunctionalUnitResponsible), session As SessionValues) As ActionResult(Of String)

End Interface
