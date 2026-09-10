#Region "Imports"

Imports Application.Glosas
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

#End Region

Partial Class GlosasService

    Public Function CopyAndPasteTransferJuridicalDebtCollectionDetail(ByVal operatingUnitId As Integer, customerId As Integer, transferJuridicalDebtCollectionCId As Integer, copyType As Integer, dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), session As SessionValues) As ActionResult(Of List(Of TransferJuridicalDebtCollectionD)) Implements IGlosasTransferJuridicalDebtC.CopyAndPasteTransferJuridicalDebtCollectionDetail
        Using service As ITransferJuridicalDebtCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtCAdminService)()
            Return service.CopyAndPasteTransferJuridicalDebtCollectionDetail(operatingUnitId, customerId, transferJuridicalDebtCollectionCId, copyType, dataImportFile, dataCopyPaste, session)
        End Using
    End Function

    Public Function GetTransferJuridicalDebtC(Id As String, session As SessionValues) As TransferJuridicalDebtCollectionC Implements IGlosasTransferJuridicalDebtC.GetTransferJuridicalDebtC
        Using Juridical As ITransferJuridicalDebtCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtCAdminService)()
            Return Juridical.GetTransferJuridicalDebtC(Id)
        End Using
    End Function

    Public Function GetTransferJuridicalDebtCByConsecutive(Consecutive As String, session As SessionValues) As TransferJuridicalDebtCollectionC Implements IGlosasTransferJuridicalDebtC.GetTransferJuridicalDebtCByConsecutive
        Using Juridical As ITransferJuridicalDebtCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtCAdminService)()
            Return Juridical.GetTransferJuridicalDebtCByConsecutive(Consecutive)
        End Using
    End Function

    Public Function ListAllTransferJuridicalDebtC(session As SessionValues) As List(Of TransferJuridicalDebtCollectionC) Implements IGlosasTransferJuridicalDebtC.ListAllTransferJuridicalDebtC
        Using Juridical As ITransferJuridicalDebtCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtCAdminService)()
            Return Juridical.ListAllTransferJuridicalDebtC()
        End Using
    End Function

    Public Function SaveTransferJuridicalDebtC(JuridicalC As TransferJuridicalDebtCollectionC, ByVal session As SessionValues) As Domain.Base.Entities.ActionResult(Of TransferJuridicalDebtCollectionC) Implements IGlosasTransferJuridicalDebtC.SaveTransferJuridicalDebtC
        Using Juridical As ITransferJuridicalDebtCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtCAdminService)()
            Return Juridical.SaveTransferJuridicalDebtC(JuridicalC, session)
        End Using
    End Function

    Public Function ConfirmTransferJuridicalDebt(JuridicalC As TransferJuridicalDebtCollectionC, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasTransferJuridicalDebtC.ConfirmTransferJuridicalDebt
        Using Juridical As ITransferJuridicalDebtCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtCAdminService)()
            Return Juridical.ConfirmTransferJuridicalDebt(JuridicalC, session)
        End Using
    End Function

    Public Function ReverseTransferJuridical(ByVal idTransferJuridical As Integer, ByVal _IdUnitoperating As Integer, ByVal IndigoSessionValues As SessionValues) As Domain.Base.Entities.ActionResult(Of String) Implements IGlosasTransferJuridicalDebtC.ReverseTransferJuridical
        Using Juridical As ITransferJuridicalDebtCAdminService = IocFactory.Instance(IndigoSessionValues.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtCAdminService)()
            Return Juridical.ReverseTransferJuridical(idTransferJuridical, _IdUnitoperating, IndigoSessionValues)
        End Using
    End Function

    Public Function DeleteTransferJuridicalDebtC(JuridicalC As TransferJuridicalDebtCollectionC, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasTransferJuridicalDebtC.DeleteTransferJuridicalDebtC
        Using Juridical As ITransferJuridicalDebtCAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtCAdminService)()
            Return Juridical.DeleteTransferJuridicalDebtC(JuridicalC, session.AuditMessageWcf)
        End Using
    End Function

End Class
