#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceAccountControl

#Region "Methods"

    <OperationContract()>
    Function GenerateServiceOrderMassive(objParams As String, homologations As List(Of List(Of CupsHomologation)), audit As AuditMessage) As ActionResult(Of List(Of List(Of CupsHomologation)))

    <OperationContract()>
    Function GetServiceOrderDetailHomologation(careGroupId As Integer, listHomologations As List(Of List(Of CupsHomologation)), args As String) As ActionResult(Of ServiceOrder)

    <OperationContract()>
    Function GenerateServiceOrderMassiveWithListDetail(parameters As String, listServiceOrderDetail As List(Of ServiceOrderDetail), audit As AuditMessage) As ActionResult

    <OperationContract()>
    Function GetHomologationsCups(parameter As String, careGroupId As Integer) As ActionResult(Of List(Of List(Of CupsHomologation)))

    <OperationContract()>
    Function SP_ListCareCenterHis(UserCode As String, GroupCode As String, CompanyContainer As String) As ActionResult(Of List(Of SP_ListCareCenterHis_Result))

    <OperationContract()>
    Function SP_ListFunctionalUnitHis(CareCenterCode As String, UserCode As String, GroupCode As String, CompanyContainer As String) As ActionResult(Of List(Of SP_ListFunctionalUnitHis_Result))

    <OperationContract()>
    Function GetAuthorizationParameterByTUF(CareCenterCode As String, FunctionalUnit As String, EmpresaDGH As String) As ActionResult

#End Region

End Interface
