Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IManagementMedicalOrderAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una entidad por id
    ''' </summary>
    ''' <returns></returns>
    Function GetManagementMedicalOrderById(ByVal id As Integer) As ActionResult(Of ManagementMedicalOrder)

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    Function SaveManagementMedicalOrder(ListManagementMedicalOrder As List(Of ManagementMedicalOrder), session As SessionValues) As ActionResult(Of ManagementMedicalOrder)

    ''' <summary>
    ''' Guarda y Actualiza el desistimiento de una gestion de ordenes médicas
    ''' </summary>
    ''' <param name="managementMedicalOrder">The entity.</param>
    ''' <param name="attachment">The attachment.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    Function SaveManagementMedicalOrderWithdrawal(managementMedicalOrder As ManagementMedicalOrder, ByVal attachment As Attachment, session As SessionValues) As ActionResult(Of ManagementMedicalOrder)

End Interface
