#Region "Imports"

Imports Application.Authorization
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class AuthorizationService
    Implements IAuthorizationServiceManagementMedicalOrder

    Public Function GetManagementMedicalOrderById(id As Integer) As ActionResult(Of ManagementMedicalOrder) Implements IAuthorizationServiceManagementMedicalOrder.GetManagementMedicalOrderById
        Using service As IManagementMedicalOrderAdminService = Container.Current.Resolve(Of IManagementMedicalOrderAdminService)()
            Return service.GetManagementMedicalOrderById(id)
        End Using
    End Function

    Public Function SaveManagementMedicalOrder(listManagementMedicalOrder As List(Of ManagementMedicalOrder), session As SessionValues) As ActionResult(Of ManagementMedicalOrder) Implements IAuthorizationServiceManagementMedicalOrder.SaveManagementMedicalOrder
        Using service As IManagementMedicalOrderAdminService = Container.Current.Resolve(Of IManagementMedicalOrderAdminService)()
            Return service.SaveManagementMedicalOrder(listManagementMedicalOrder, session)
        End Using
    End Function

    Public Function SaveManagementMedicalOrderWithdrawal(managementMedicalOrder As ManagementMedicalOrder, ByVal attachment As Attachment, session As SessionValues) As ActionResult(Of ManagementMedicalOrder) Implements IAuthorizationServiceManagementMedicalOrder.SaveManagementMedicalOrderWithdrawal
        Using service As IManagementMedicalOrderAdminService = Container.Current.Resolve(Of IManagementMedicalOrderAdminService)()
            Return service.SaveManagementMedicalOrderWithdrawal(managementMedicalOrder, attachment, session)
        End Using
    End Function

End Class
