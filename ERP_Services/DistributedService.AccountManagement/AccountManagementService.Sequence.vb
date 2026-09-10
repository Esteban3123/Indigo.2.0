#Region "Imports"

Imports Application.AccountManagement
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Class AccountManagementService
    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements IAccountManagementSequence.GetNumericSequenseGroupById
        Using service As IAccountManagementSequenceAdminService = Container.Current.Resolve(Of IAccountManagementSequenceAdminService)()
            Return service.GetNumericSequenseGroupById(id)
        End Using
        'Return _sequenseAdminService.GetNumericSequenseGroupById(id)
    End Function

    Public Function GetSequenseByIdForm(idForm As String) As Domain.Entities.AccountManagementSequence Implements IAccountManagementSequence.GetSequenseByIdForm
        Using service As IAccountManagementSequenceAdminService = Container.Current.Resolve(Of IAccountManagementSequenceAdminService)()
            Return service.GetSequenseByIdForm(idForm)
        End Using
        'Return Me._sequenseAdminService.GetSequenseByIdForm(idForm)
    End Function

    Public Function SaveSequence(seq As Domain.Entities.AccountManagementSequence) As Domain.Base.Entities.ActionResult Implements IAccountManagementSequence.SaveSequence
        Using service As IAccountManagementSequenceAdminService = Container.Current.Resolve(Of IAccountManagementSequenceAdminService)()
            Return service.SaveSequence(seq)
        End Using
        'Return Me._sequenseAdminService.SaveSequence(seq)
    End Function

End Class