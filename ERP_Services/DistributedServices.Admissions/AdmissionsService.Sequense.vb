#Region "Imports"

Imports Application.Admissions
Imports Microsoft.Practices.Unity

#End Region

Partial Class AdmissionsService
    Implements IAdmissionsSequence

    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements IAdmissionsSequence.GetNumericSequenseGroupById
        Using service As IAdmissionsSequenseAdminService = Container.Current.Resolve(Of IAdmissionsSequenseAdminService)()
            Return service.GetNumericSequenseGroupById(id)
        End Using
    End Function

    Public Function GetSequenseByIdForm(idForm As String) As Domain.Entities.AdmissionsSequence Implements IAdmissionsSequence.GetSequenseByIdForm
        Using service As IAdmissionsSequenseAdminService = Container.Current.Resolve(Of IAdmissionsSequenseAdminService)()
            Return service.GetSequenseByIdForm(idForm)
        End Using
    End Function

    Public Function SaveSequence(seq As Domain.Entities.AdmissionsSequence) As Domain.Base.Entities.ActionResult Implements IAdmissionsSequence.SaveSequence
        Using service As IAdmissionsSequenseAdminService = Container.Current.Resolve(Of IAdmissionsSequenseAdminService)()
            Return service.SaveSequence(seq)
        End Using
    End Function

End Class
