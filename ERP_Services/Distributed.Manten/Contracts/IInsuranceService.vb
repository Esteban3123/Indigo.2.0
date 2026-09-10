#Region "Imports"
Imports System.ServiceModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface IMaintenanceInsurance


    <OperationContract()> _
    Function ListAllInsurance(Empresa As String) As List(Of Insurance)


    <OperationContract()> _
    Function DeleteInsurance(Empresa As String, ByVal Insurance As Insurance, ByVal audit As AuditMessage) As Boolean


    <OperationContract()> _
    Function SaveInsurance(Empresa As String, ByVal Insurance As Insurance, ByVal audit As AuditMessage) As ActionResult(Of Domain.Maintenance.Entities.Insurance)

    <OperationContract()> _
    Function GetInsurance(Empresa As String, ByVal codeInsurance As String) As Insurance

    <OperationContract()> _
    Function Change_StateInsurance(code As String, state As Boolean, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of Domain.Maintenance.Entities.Insurance)

End Interface
