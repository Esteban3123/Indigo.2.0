#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface IAuthorizationServiceManagementMedicalOrder

    ''' <summary>
    ''' Obtiene una gestión de orden médica por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetManagementMedicalOrderById(ByVal id As Integer) As ActionResult(Of ManagementMedicalOrder)

    ''' <summary>
    ''' Guarda o Actualiza una lista de gestiones de ordenes médicas
    ''' </summary>
    ''' <param name="listManagementMedicalOrder">list entities.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveManagementMedicalOrder(listManagementMedicalOrder As List(Of ManagementMedicalOrder), session As SessionValues) As ActionResult(Of ManagementMedicalOrder)

    ''' <summary>
    ''' Guarda y Actualiza el desistimiento de una gestion de ordenes médicas
    ''' </summary>
    ''' <param name="managementMedicalOrder">The entity.</param>
    ''' <param name="attachment">The attachment.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveManagementMedicalOrderWithdrawal(managementMedicalOrder As ManagementMedicalOrder, ByVal attachment As Attachment, session As SessionValues) As ActionResult(Of ManagementMedicalOrder)

End Interface
