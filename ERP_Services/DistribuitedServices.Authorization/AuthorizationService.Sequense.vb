#Region "Imports"

Imports Application.Authorization
Imports Microsoft.Practices.Unity

#End Region

Partial Class AuthorizationService
    Implements IAuthorizationSequence

    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements IAuthorizationSequence.GetNumericSequenseGroupById
        Using service As IAuthorizationSequenseAdminService = Container.Current.Resolve(Of IAuthorizationSequenseAdminService)()
            Return service.GetNumericSequenseGroupById(id)
        End Using
    End Function

    Public Function GetSequenseByIdForm(idForm As String) As Domain.Entities.AuthorizationSequence Implements IAuthorizationSequence.GetSequenseByIdForm
        Using service As IAuthorizationSequenseAdminService = Container.Current.Resolve(Of IAuthorizationSequenseAdminService)()
            Return service.GetSequenseByIdForm(idForm)
        End Using
    End Function

    Public Function SaveSequence(seq As Domain.Entities.AuthorizationSequence) As Domain.Base.Entities.ActionResult Implements IAuthorizationSequence.SaveSequence
        Using service As IAuthorizationSequenseAdminService = Container.Current.Resolve(Of IAuthorizationSequenseAdminService)()
            Return service.SaveSequence(seq)
        End Using
    End Function

End Class
