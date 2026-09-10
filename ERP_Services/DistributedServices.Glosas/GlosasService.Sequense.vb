#Region "Imports"

Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

#End Region

Partial Class GlosasService
    Implements IGlosasSequence

    Public Function GetNumericSequenseGroupById(id As Integer, session As SessionValues) As List(Of String) Implements IGlosasSequence.GetNumericSequenseGroupById
        Using service As IGlosaSequenseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGlosaSequenseAdminService)()
            Return service.GetNumericSequenseGroupById(id)
        End Using
    End Function

    Public Function GetSequenseByIdForm(idForm As String, session As SessionValues) As Domain.Entities.GlosaSequence Implements IGlosasSequence.GetSequenseByIdForm
        Using service As IGlosaSequenseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGlosaSequenseAdminService)()
            Return service.GetSequenseByIdForm(idForm)
        End Using
    End Function

    Public Function SaveSequence(seq As Domain.Entities.GlosaSequence, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasSequence.SaveSequence
        Using service As IGlosaSequenseAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGlosaSequenseAdminService)()
            Return service.SaveSequence(seq)
        End Using
    End Function

End Class
