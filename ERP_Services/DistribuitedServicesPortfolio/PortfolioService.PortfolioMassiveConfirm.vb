Imports Application.Portfolio
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class PortfolioService

    Public Function ConfirmPortfolioDocument(processId As Integer, code As String, session As SessionValues) As ActionResult(Of Tuple(Of String, Integer)) Implements IPortfolioServicePortfolioMassiveConfirm.ConfirmPortfolioDocument
        Using service As IPortfolioMassiveConfirmAdminService = Container.Current.Resolve(Of IPortfolioMassiveConfirmAdminService)()
            Return service.ConfirmPortfolioDocument(processId, code, session.AuditMessageWcf, session)
        End Using
    End Function

    Public Function ConfirmPortfolioDocuments(processId As Integer, listDocuments As List(Of String), session As SessionValues) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IPortfolioServicePortfolioMassiveConfirm.ConfirmPortfolioDocuments
        Using service As IPortfolioMassiveConfirmAdminService = Container.Current.Resolve(Of IPortfolioMassiveConfirmAdminService)()
            Return service.ConfirmPortfolioDocuments(processId, listDocuments, session.AuditMessageWcf, session)
        End Using
    End Function

End Class