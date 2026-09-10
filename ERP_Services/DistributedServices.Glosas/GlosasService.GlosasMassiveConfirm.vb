Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class GlosasService

    Public Function ConfirmGlosaDocument(processId As Integer, code As String, session As SessionValues, Optional operativeUnitId As Integer = 0) As Domain.Base.Entities.ActionResult(Of Tuple(Of String, Integer)) Implements IGlosasGlosasMassiveConfirm.ConfirmGlosaDocument
        Using massiveConfirmAdminService As IGlosasMassiveConfirmAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGlosasMassiveConfirmAdminService)()
            Return massiveConfirmAdminService.ConfirmGlosaDocument(processId, code, session.AuditMessageWcf, session, operativeUnitId)
        End Using
    End Function

    Public Function ConfirmGlosasDocuments(processId As Integer, listDocuments As List(Of String), session As SessionValues, Optional operativeUnitId As Integer = 0) As Domain.Base.Entities.ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IGlosasGlosasMassiveConfirm.ConfirmGlosasDocuments
        Using massiveConfirmAdminService As IGlosasMassiveConfirmAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IGlosasMassiveConfirmAdminService)()
            Return massiveConfirmAdminService.ConfirmGlosasDocuments(processId, listDocuments, session.AuditMessageWcf, session, operativeUnitId)
        End Using
    End Function

End Class
