'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Juan Diego Diaz
' Created          : 15-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Entities

Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base

#End Region

Partial Class GlosasService

    Public Function DeleteJuridicalD(JuridicalD As List(Of TransferJuridicalDebtCollectionD), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasTransferJuridicalDebtD.DeleteJuridicalD
        Using Juridical As ITransferJuridicalDebtDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtDAdminService)()
            Return Juridical.DeleteJuridicalD(JuridicalD, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetJuridicalDByIdJuridicalD(Id As String, session As SessionValues) As TransferJuridicalDebtCollectionD Implements IGlosasTransferJuridicalDebtD.GetJuridicalDByIdJuridicalD
        Using Juridical As ITransferJuridicalDebtDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtDAdminService)()
            Return Juridical.GetJuridicalDByIdJuridicalD(Id)
        End Using
    End Function

    Public Function ListAllJuridicalD(session As SessionValues) As List(Of TransferJuridicalDebtCollectionD) Implements IGlosasTransferJuridicalDebtD.ListAllJuridicalD
        Using Juridical As ITransferJuridicalDebtDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtDAdminService)()
            Return Juridical.ListAllJuridicalD
        End Using
    End Function

    Public Function ListJuridicalDDByIdJuridicalC(Id As String, session As SessionValues) As List(Of TransferJuridicalDebtCollectionD) Implements IGlosasTransferJuridicalDebtD.ListJuridicalDDByIdJuridicalC
        Using Juridical As ITransferJuridicalDebtDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtDAdminService)()
            Return Juridical.ListJuridicalDDByIdJuridicalC(Id)
        End Using
    End Function

    Public Function SaveJuridicalD(JuridicalD As List(Of TransferJuridicalDebtCollectionD), session As SessionValues) As Domain.Base.Entities.ActionResult Implements IGlosasTransferJuridicalDebtD.SaveJuridicalD
        Using Juridical As ITransferJuridicalDebtDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtDAdminService)()
            Return Juridical.SaveJuridicalD(JuridicalD, session.AuditMessageWcf)
        End Using
    End Function

    Public Function ValidateListInvoiceJuridical(ListInvoices As List(Of String), Nit As String, session As SessionValues, ByVal _IdUnitoperating As Integer) As List(Of TransferJuridicalDebtCollectionD) Implements IGlosasTransferJuridicalDebtD.ValidateListInvoiceJuridical
        Using Juridical As ITransferJuridicalDebtDAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITransferJuridicalDebtDAdminService)()
            Return Juridical.ValidateListInvoiceJuridical(ListInvoices, Nit, _IdUnitoperating)
        End Using
    End Function

End Class
