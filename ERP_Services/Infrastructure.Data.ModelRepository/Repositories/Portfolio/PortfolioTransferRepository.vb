'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class PortfolioTransferRepository
    Inherits GenericRepository(Of PortfolioTransfer)
    Implements IPortfolioTransferRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' lista todos los documnetos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioTransferMassiveConfirm(listDocuments As List(Of String)) As List(Of PortfolioTransfer) Implements IPortfolioTransferRepository.ListPortfolioTransferMassiveConfirm
        Return (From cr In _context.PortfolioTransfer.Include("PortfolioTransferDetail").Include("PortfolioTransferOtherConcept") Where listDocuments.Contains(cr.Code) Select cr).ToList()
    End Function

    ''' <summary>
    ''' obtiene los detalles del traslado
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    Public Function GatPortfolioTransferDetailByIdPortfolioTransfer(idPortfolioTransfer As Integer) As List(Of PortfolioTransferDetail) Implements IPortfolioTransferRepository.GatPortfolioTransferDetailByIdPortfolioTransfer
        Dim res = (From ptd In _context.PortfolioTransferDetail Where ptd.PortfolioTrasferId = idPortfolioTransfer Select ptd).ToList()
        For Each item In res
            Dim accountReceivable = (From ar In _context.AccountReceivable.AsNoTracking().Include("AccountReceivableAccounting") Where ar.Id = item.AccountReceivableId Select ar).FirstOrDefault()
            item.ValueBill = accountReceivable.AccountReceivableAccounting.Where(Function(x) x.AccountReceivableId = accountReceivable.Id And x.MainAccountId = item.MainAccountId).FirstOrDefault().Value
            item.Balance = accountReceivable.AccountReceivableAccounting.Where(Function(x) x.AccountReceivableId = accountReceivable.Id And x.MainAccountId = item.MainAccountId).FirstOrDefault().Balance
            item.InvoiceNumber = accountReceivable.InvoiceNumber
            Dim account = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.MainAccountId Select ma).FirstOrDefault()
            item.CodeNameMainAccount = account.Number + " - " + account.Name
        Next
        Return res
    End Function

    ''' <summary>
    ''' obtiene un traslado por id
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    Public Function GetPortfolioTransferById(idPortfolioTransfer As Integer) As PortfolioTransfer Implements IPortfolioTransferRepository.GetPortfolioTransferById
        Dim res = (From pt In _context.PortfolioTransfer Where pt.Id = idPortfolioTransfer Select pt).FirstOrDefault()
        If res IsNot Nothing Then
            If res.CustomerId IsNot Nothing AndAlso res.CustomerId <> 0 Then
                Dim customer = (From c In _context.Customer.AsNoTracking() Where c.Id = res.CustomerId Select c).FirstOrDefault()
                res.CodeNameCustomer = customer.Nit + " - " + customer.Name
            End If
            Dim advance = (From a In _context.PortfolioAdvance.AsNoTracking() Where a.Id = res.PortfolioAdvanceId Select a).FirstOrDefault()
            res.CodeNameAdvance = advance.Code
            Dim currency = (From cc In _context.Currency.AsNoTracking() Where cc.Id = advance.CurrencyId Select cc).FirstOrDefault()
            res.CurrencyId = currency?.Id
            res.CurrencyAbbreviation = currency.Abbreviation
            Dim account = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.MainAccountId Select ma).FirstOrDefault()
            res.CodeNameMainAccount = account.Number + " - " + account.Name
            If res.CostCenterId IsNot Nothing Then
                Dim costCenter = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = res.CostCenterId Select cc).FirstOrDefault()
                res.CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name
            End If
            res.OriginalValue = (From pt In _context.PortfolioTransfer.AsNoTracking() Where pt.Id = idPortfolioTransfer Select pt).FirstOrDefault()
            Return res
        Else
            Return New PortfolioTransfer
        End If
    End Function

    ''' <summary>
    ''' obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetPortfolioTransfersByCode(code As String) As PortfolioTransfer Implements IPortfolioTransferRepository.GetPortfolioTransfersByCode
        Dim res = (From pt In _context.PortfolioTransfer Where pt.Code = code Select pt).FirstOrDefault()
        If res IsNot Nothing Then
            If res.CustomerId IsNot Nothing Then
                res.CustomerIdTemp = res.CustomerId 'se utiliza para reversion desde nota debito de cartera
                Dim customer = (From c In _context.Customer.AsNoTracking() Where c.Id = res.CustomerId Select c).FirstOrDefault()
                res.CodeNameCustomer = customer.Nit + " - " + customer.Name
            Else
                Dim third = (From t In _context.ThirdParty.AsNoTracking() Where t.Id = res.ThirdPartyId Select t).FirstOrDefault()
                res.CodeNameCustomer = third.Nit + " - " + third.Name
                'se utiliza para reversion desde nota debito de cartera
                Dim customer = (From c In _context.Customer.AsNoTracking() Where c.ThirdPartyId = res.ThirdPartyId Select c).FirstOrDefault()
                If customer IsNot Nothing
                     res.CustomerIdTemp = customer.Id
                End If
            End If

            Dim advance = (From a In _context.PortfolioAdvance.AsNoTracking() Where a.Id = res.PortfolioAdvanceId Select a).FirstOrDefault()
            res.CodeNameAdvance = advance.Code
            Dim account = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.MainAccountId Select ma).FirstOrDefault()
            res.CodeNameMainAccount = account.Number + " - " + account.Name
            If res.CostCenterId IsNot Nothing Then
                Dim costCenter = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = res.CostCenterId Select cc).FirstOrDefault()
                res.CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name
            End If
            res.OriginalValue = (From pt In _context.PortfolioTransfer.AsNoTracking() Where pt.Code = code Select pt).FirstOrDefault()
            Return res
        Else
            Return New PortfolioTransfer
        End If
    End Function

    ''' <summary>
    ''' Gets the portfolio transfer detail by identifier.
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetPortfolioTransferDetailById(Id As Integer) As PortfolioTransferDetail Implements IPortfolioTransferRepository.GetPortfolioTransferDetailById
        Dim query = (From pt In _context.PortfolioTransferDetail.Include("PortfolioTransfer") Where pt.Id = Id Select pt).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return New PortfolioTransferDetail()
        End If
    End Function

    ''' <summary>
    ''' Gets the portfolio transfer detail by account receivable identifier.
    ''' </summary>
    ''' <param name="AccountReceivableId">The account receivable identifier.</param>
    ''' <returns></returns>
    Public Function GetPortfolioTransferDetailByAccountReceivableId(AccountReceivableId As Integer) As List(Of PortfolioTransferDetail) Implements IPortfolioTransferRepository.GetPortfolioTransferDetailByAccountReceivableId
        Return (From pt In _context.PortfolioTransferDetail.Include("PortfolioTransfer") Where pt.AccountReceivableId = AccountReceivableId Select pt).ToList()
    End Function


    ''' <summary>
    ''' lista los otros conceptos de traslados
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    Public Function GetPortfolioTransferOtherConceptByIdPortfolioTransfer(idPortfolioTransfer As Integer) As List(Of PortfolioTransferOtherConcept) Implements IPortfolioTransferRepository.GetPortfolioTransferOtherConceptByIdPortfolioTransfer
        Dim res = (From ptoc In _context.PortfolioTransferOtherConcept Where ptoc.PortfolioTransferId = idPortfolioTransfer Select ptoc).ToList()
        For Each item In res
            item.CodeNameNoteConcept = (From nc In _context.PortfolioNoteConcept.AsNoTracking() Where nc.Id = item.PortfolioNoteConceptId Select String.Concat(nc.Code, " - ", nc.Name)).FirstOrDefault()
            item.NumberNameMainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.MainAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            If item.ThirdPartyId IsNot Nothing Then
                item.CodeNameThirdParty = (From cc In _context.ThirdParty.AsNoTracking() Where cc.Id = item.ThirdPartyId Select String.Concat(cc.Nit, " - ", cc.Name)).FirstOrDefault()
            End If
            If item.CostCenterId IsNot Nothing Then
                item.CodeNameCostCenter = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = item.CostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault()
            End If
        Next
        Return res
    End Function

    ''' <summary>
    ''' Valida el CopyPaste del form de traslados de cartera
    ''' </summary>
    ''' <param name="xmlObject">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_CopyAndPasteTransfer(xmlObject As String, TransferType As Integer, CompanyType As Integer) As List(Of SP_CopyAndPasteTransfer_Result) Implements IPortfolioTransferRepository.SP_CopyAndPasteTransfer
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteTransfer(xmlObject, TransferType, CompanyType).ToList
    End Function

    ''' <summary>
    ''' Guarda un traslado con el objeto xml
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_SavePortfolioTransfer(XmlObject As String, codeUser As String, companyType As Byte) As SP_SavePortfolioTransfer_Result Implements IPortfolioTransferRepository.SP_SavePortfolioTransfer
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SavePortfolioTransfer(XmlObject, codeUser, companyType).SingleOrDefault
    End Function

End Class
