'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CashRegisterRepository
    Inherits GenericRepository(Of CashRegisters)
    Implements ICashRegisterRepository



    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un registro de caja
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetCashRegister(code As String, Optional tracking As Boolean = True) As CashRegisters Implements ICashRegisterRepository.GetCashRegister
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As CashRegisters In Me._context.CashRegisters.Include("CashRegisterUser").Include("Currency") Where d.Code.Equals(code.Trim()) Select d).ToList()
        If res?.Any() Then
            Dim IdCash As Integer = res(0).Id
            res(0).hasMovements = (From tb In Me._context.TreasuryBalance Where tb.CashRegisterId = IdCash)?.Any()
            'Dim IdMainAccount As Integer = res(0).IdMainAccount
            'res(0).MainAccountHandlesCostCenter = (From cc In _context.MainAccounts Where IdMainAccount Select cc.HandlesCostCenter).FirstOrDefault()
            res(0).OriginalValue = (From d As CashRegisters In Me._context.CashRegisters.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            If res(0).CurrencyId IsNot Nothing Then
                res(0).CurrencyName = res(0).Currency.Abbreviation
            End If
            Return res(0)
        Else
            Return New CashRegisters()
        End If
    End Function

    ''' <summary>
    ''' Lista todos los registros de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashRegister() As List(Of CashRegisters) Implements ICashRegisterRepository.ListCashRegister
        Dim search = From e In _context.CashRegisters Select e
        Return search.ToList
    End Function

    ''' <summary>
    ''' Obtiene una caja por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetCashRegisterById(Id As Integer, Optional currencyflag As Boolean = True) As CashRegisters Implements ICashRegisterRepository.GetCashRegisterById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If

        Dim res = (From d As CashRegisters In Me._context.CashRegisters.Include("CashRegisterUser").Include("Currency")
                   Where d.Id.Equals(Id)
                   Select d).FirstOrDefault()

        If res IsNot Nothing Then
            Dim IdMainAccount As Integer = res.IdMainAccount
            res.MainAccountHandlesCostCenter = (From cc In _context.MainAccounts.AsNoTracking() Where cc.Id = IdMainAccount Select cc.HandlesCostCenter).FirstOrDefault()
            res.OriginalValue = (From d As CashRegisters In Me._context.CashRegisters.AsNoTracking() Where d.Id.Equals(Id) Select d).SingleOrDefault()
            If res.CurrencyId IsNot Nothing Then
                res.CurrencyName = res.Currency?.Abbreviation
            Else
                Dim _companySettings = (From x In _context.CompanySettings.Include("Currency").AsNoTracking() Select x)?.FirstOrDefault
                res.CurrencyId = _companySettings?.OfficialCurrencyId
                res.CurrencyName = _companySettings?.Currency?.Abbreviation
                If currencyflag Then
                    res.Currency = _companySettings?.Currency
                End If
            End If
            Return res
        Else
            Return New CashRegisters()
        End If
    End Function

    ''' <summary>
    ''' Lista todos los prefijos que existen en la tabla
    ''' </summary>
    ''' <returns>Lista de prefijos</returns>
    Public Function ListPrefixs() As List(Of String) Implements ICashRegisterRepository.ListPrefixs
        Return (From b As CashRegisters In Me._context.CashRegisters Select b.Prefix).Distinct().ToList()
    End Function

    Public Function GetFirstCashbyUserId(UserId As Integer, Optional currencyId As Integer? = Nothing) As CashRegisters Implements ICashRegisterRepository.GetFirstCashbyUserId
        Return (From b As CashRegisters In Me._context.CashRegisters.AsNoTracking() Join cru In _context.CashRegisterUser.AsNoTracking() On cru.IdCashRegister Equals b.Id
                Where cru.IdUser = UserId And (currencyId Is Nothing OrElse b.CurrencyId = currencyId)
                Select b).FirstOrDefault()
    End Function

    Public Function ListCashRegisterUserByCashRegisterIdAndUser(cashId As Integer, userId As Integer) As List(Of CashRegisterUser) Implements ICashRegisterRepository.ListCashRegisterUserByCashRegisterIdAndUser
        Return (From cu In _context.CashRegisterUser.AsNoTracking() Where cu.IdCashRegister = cashId And cu.IdUser = userId Select cu).ToList()
    End Function
End Class
