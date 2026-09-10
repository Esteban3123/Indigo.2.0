'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Rafael Eduardo Patiño
' Created          : 09-01-2015

' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class AccountReceivableAccountingRepository
    Inherits GenericRepository(Of AccountReceivableAccounting)
    Implements IAccountReceivableAccountingRepository

#Region "Context"
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

#Region "Method"

    ''' <summary>
    ''' Metodo que retorna Objeto del historico de Estructura de cuentas por cobrar.
    ''' </summary>
    ''' <param name="id">Id del movimiento de la cuenta por cobrar</param>
    ''' <returns>objeto historico cuenta por cobrar</returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableAccountingById(ByVal id As Integer, Optional asTracking As Boolean = True) As AccountReceivableAccounting Implements IAccountReceivableAccountingRepository.GetAccountReceivableAccountingById
        Dim accountReceivableAccounting As AccountReceivableAccounting
        If asTracking Then
            accountReceivableAccounting = (From ara In _context.AccountReceivableAccounting.Include("AccountReceivable") Where ara.Id = id).FirstOrDefault()
        Else
            accountReceivableAccounting = (From ara In _context.AccountReceivableAccounting.AsNoTracking().Include("AccountReceivable").AsNoTracking() Where ara.Id = id).FirstOrDefault()
        End If

        If accountReceivableAccounting IsNot Nothing Then
            Return accountReceivableAccounting
        Else
            Return New AccountReceivableAccounting
        End If
    End Function

    ''' <summary>
    ''' Metodo que retorna Objeto del historico de Estructura de cuentas por cobrar.
    ''' </summary>
    ''' <param name="AccountReceivableId">Id de la cuenta por cobrar</param>
    ''' <param name="MainAccountId">Id de la cuenta contable de acuerdo al proceso</param>
    ''' <returns>objeto historico cuenta por cobrar</returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableAccounting(ByVal AccountReceivableId As Integer, ByVal MainAccountId As Integer) As AccountReceivableAccounting Implements IAccountReceivableAccountingRepository.GetAccountReceivableAccounting
        Dim AccountReceivableAccounting = From e In _context.AccountReceivableAccounting
                                          Where e.AccountReceivableId = AccountReceivableId And e.MainAccountId = MainAccountId
                                          Select e
        If AccountReceivableAccounting.Count > 0 Then
            Dim tmpAccountReceivable = AccountReceivableAccounting.SingleOrDefault
            tmpAccountReceivable.OriginalValue = (From e In _context.AccountReceivableAccounting.AsNoTracking
                                                  Where e.AccountReceivableId = AccountReceivableId And e.MainAccountId = MainAccountId
                                                  Select e).SingleOrDefault
            Return tmpAccountReceivable
        Else
            Return New AccountReceivableAccounting
        End If
    End Function

    ''' <summary>
    ''' Lista de Historico de cuentas por pagar
    ''' </summary>
    ''' <param name="AccountReceivableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountReceivableAccounting(ByVal AccountReceivableId As Integer) As List(Of AccountReceivableAccounting) Implements IAccountReceivableAccountingRepository.ListAccountReceivableAccounting
        Dim AccountReceivableAccounting = (From e In _context.AccountReceivableAccounting Where e.AccountReceivableId = AccountReceivableId Select e).ToList()
        Return AccountReceivableAccounting
    End Function

    Public Function GetAccountReceivableAccountingByInvoiceNumberAndMainAccountId(invoiceNumber As String, mainAccountId As Integer, thirdPartyId As Integer) As AccountReceivableAccounting Implements IAccountReceivableAccountingRepository.GetAccountReceivableAccountingByInvoiceNumberAndMainAccountId
        If thirdPartyId = 0 Then
            Return (From aa In _context.AccountReceivableAccounting
                    Join a In _context.AccountReceivable On a.Id Equals aa.AccountReceivableId
                    Where a.InvoiceNumber = invoiceNumber And aa.MainAccountId = mainAccountId Select aa).FirstOrDefault()
        Else
            Return (From aa In _context.AccountReceivableAccounting
                    Join a In _context.AccountReceivable On a.Id Equals aa.AccountReceivableId
                    Where a.InvoiceNumber = invoiceNumber And aa.MainAccountId = mainAccountId And a.ThirdPartyId = thirdPartyId Select aa).FirstOrDefault()
        End If
    End Function

    Public Function ValidateAccountReceivable(InvoiceNumber As String, ThirdPartyId As Integer, Position As Integer) As Tuple(Of Boolean, String) Implements IAccountReceivableAccountingRepository.ValidateAccountReceivable
        Dim accountReceivable As AccountReceivable
        If ThirdPartyId = 0 Then
            accountReceivable = (From a In _context.AccountReceivable.AsNoTracking
                                 Where a.InvoiceNumber = InvoiceNumber Select a).FirstOrDefault()
        Else
            accountReceivable = (From a In _context.AccountReceivable.AsNoTracking
                                 Where a.InvoiceNumber = InvoiceNumber And a.ThirdPartyId = ThirdPartyId Select a).FirstOrDefault()
        End If
        If accountReceivable.PortfolioStatus = 1 Then
            Return New Tuple(Of Boolean, String)(False, "La factura " + accountReceivable.InvoiceNumber.ToString + " del item " + Position.ToString + " no está radicada")
        End If
        Return New Tuple(Of Boolean, String)(True, "Ok")
    End Function

#End Region

End Class
