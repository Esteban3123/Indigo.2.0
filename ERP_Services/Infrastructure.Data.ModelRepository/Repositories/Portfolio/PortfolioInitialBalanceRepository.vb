'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Carlos ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class PortfolioInitialBalanceRepository
    Inherits GenericRepository(Of PortfolioInitialBalance)
    Implements IPortfolioInitialBalanceRepository

#Region "Context"
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    ''' <summary>
    ''' metodo para obtener las facturas del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of PortfolioInitialBalanceAccountReceivable) Implements IPortfolioInitialBalanceRepository.GetPortfolioInitialBalanceAccountReceivableByIdPortfolioInitialBalance
        Dim res = (From ibar In _context.PortfolioInitialBalanceAccountReceivable.Include("PortfolioInitialBalanceAccountReceivableAccounting").Include("PortfolioInitialBalanceAccountReceivableShare") Where ibar.PortfolioInitialBalanceId = idPortfolioInitialBalance Select ibar).ToList()
        If res.Count > 0 Then
            For Each item In res
                Dim customer = (From c In _context.Customer.AsNoTracking() Where c.Id = item.CustomerId Select c).FirstOrDefault()
                item.CodeNameCustomer = customer.Nit + " - " + customer.Name

                If item.CostCenterId IsNot Nothing Then
                    item.CodeNameGlosasCostCenter = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = item.CostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault()
                End If
                If item.AccountWithoutRadicateId IsNot Nothing Then
                    item.CodeNameAccountWithoutRadicate = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.AccountWithoutRadicateId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                End If
                If item.AccountRadicateId IsNot Nothing Then
                    item.CodeNameAccountRadicate = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.AccountRadicateId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                End If
                If item.AccountObjectionRemediedId IsNot Nothing Then
                    item.CodeNameAccountObjectionRemedied = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.AccountObjectionRemediedId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                End If
                If item.AccountConciliationId IsNot Nothing Then
                    item.CodeNameAccountConciliation = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.AccountConciliationId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                End If
                If item.AccountLegalCollectionId IsNot Nothing Then
                    item.CodeNameAccountLegalCollection = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.AccountLegalCollectionId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                End If
                If item.AccountDebtorOrder IsNot Nothing Then
                    item.CodeNameAccountDebtorOrder = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.AccountDebtorOrder Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                End If
                If item.AccountCreditorOrder IsNot Nothing Then
                    item.CodeNameAccountCreditorOrder = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.AccountCreditorOrder Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                End If
                For Each itemAccount In item.PortfolioInitialBalanceAccountReceivableAccounting
                    Dim mainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = itemAccount.MainAccountId Select ma).FirstOrDefault()
                    itemAccount.CodeNameMainAccount = mainAccount.Number + " - " + mainAccount.Name
                    If itemAccount.CostCenterId IsNot Nothing Then
                        Dim costCenter = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = itemAccount.CostCenterId Select cc).FirstOrDefault()
                        itemAccount.CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name
                    End If
                Next
            Next
        End If
        Return res
    End Function

    ''' <summary>
    ''' metodo para obtener los anticipos del saldo inicial por id del saldo inicial
    ''' </summary>
    ''' <param name="idPortfolioInitialBalance"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance(idPortfolioInitialBalance As Integer) As List(Of PortfolioInitialBalanceAdvance) Implements IPortfolioInitialBalanceRepository.GetPortfolioInitialBalanceAdvanceByIdPortfolioInitialBalance
        Dim res = (From iba In _context.PortfolioInitialBalanceAdvance Where iba.PortfolioInitialBalanceId = idPortfolioInitialBalance Select iba).ToList()
        If res.Count > 0 Then
            For Each item In res
                If item.CustomerId IsNot Nothing Then
                    Dim customer = (From c In _context.Customer.AsNoTracking() Where c.Id = item.CustomerId Select c).FirstOrDefault()
                    item.CodeNameCustomer = customer.Nit + " - " + customer.Name
                End If
                item.NitNameThirdParty = (From tp In _context.ThirdParty.AsNoTracking() Where tp.Id = item.ThirdPartyId Select String.Concat(tp.Nit, " - ", tp.Name)).FirstOrDefault()
                Dim mainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.MainAccountId Select ma).FirstOrDefault()
                item.CodeNameMainAccount = mainAccount.Number + " - " + mainAccount.Name
                If item.CostCenterId IsNot Nothing Then
                    Dim costCenter = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = item.CostCenterId Select cc).FirstOrDefault()
                    item.CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name
                End If
            Next
        End If
        Return res
    End Function

    ''' <summary>
    ''' metodo para obtener el saldo inicial por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceByCode(code As String) As PortfolioInitialBalance Implements IPortfolioInitialBalanceRepository.GetPortfolioInitialBalanceByCode
        Dim res = (From ib In _context.PortfolioInitialBalance Where ib.Code = code Select ib).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From ib In _context.PortfolioInitialBalance.AsNoTracking() Where ib.Code = code Select ib).FirstOrDefault()
            Return res
        Else
            Return New PortfolioInitialBalance
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener el saldo inicial por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceById(id As Integer) As PortfolioInitialBalance Implements IPortfolioInitialBalanceRepository.GetPortfolioInitialBalanceById
        Dim res = (From ib In _context.PortfolioInitialBalance Where ib.Id = id Select ib).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New PortfolioInitialBalance
        End If
    End Function
End Class
