#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Class BillingService
    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements IBillingSequence.GetNumericSequenseGroupById
        Using service As IBillingSequenseAdminService = Container.Current.Resolve(Of IBillingSequenseAdminService)()
            Return service.GetNumericSequenseGroupById(id)
        End Using
        'Return _sequenseAdminService.GetNumericSequenseGroupById(id)
    End Function

    Public Function GetSequenseByIdForm(idForm As String) As Domain.Entities.BillingSequence Implements IBillingSequence.GetSequenseByIdForm
        Using service As IBillingSequenseAdminService = Container.Current.Resolve(Of IBillingSequenseAdminService)()
            Return service.GetSequenseByIdForm(idForm)
        End Using
        'Return Me._sequenseAdminService.GetSequenseByIdForm(idForm)
    End Function

    Public Function SaveSequence(seq As Domain.Entities.BillingSequence) As Domain.Base.Entities.ActionResult Implements IBillingAuthorization.SaveSequence
        Using service As IBillingSequenseAdminService = Container.Current.Resolve(Of IBillingSequenseAdminService)()
            Return service.SaveSequence(seq)
        End Using
        'Return Me._sequenseAdminService.SaveSequence(seq)
    End Function

End Class
