
#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
#End Region

<ServiceContract()>
Public Interface IAccountManagementServiceAccountManagementParameters

    ''' <summary>
    ''' Obtiene la gestion de cuenta por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountManagementParametersById(id As Integer) As ActionResult(Of AccountManagementParameters)

    ''' <summary>
    ''' Guarda o actualiza la gestion de cuenta
    ''' </summary>
    ''' <param name="AccountManagementParameters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveAccountManagementParameters(ByVal AccountManagementParameters As AccountManagementParameters, audit As AuditMessage) As Task(Of ActionResult(Of AccountManagementParameters))

    ''' <summary>
    ''' Obtiene los usuarios activos para asignación de ingresos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAvailableUsers() As ActionResult(Of List(Of UsersAssignment))

    ''' <summary>
    ''' Obtiene los usuarios con ingresos o folios distribuidos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetUsersWithDistributedAccounts() As Task(Of ActionResult(Of List(Of UsersAssignment)))
End Interface
