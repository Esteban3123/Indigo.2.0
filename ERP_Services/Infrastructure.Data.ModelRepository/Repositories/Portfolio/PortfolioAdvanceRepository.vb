'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class PortfolioAdvanceRepository
    Inherits GenericRepository(Of PortfolioAdvance)
    Implements IPortfolioAdvanceRepository


    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un anticipo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetPortfolioAdvance(code As String) As PortfolioAdvance Implements IPortfolioAdvanceRepository.GetPortfolioAdvance
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim query = (From pa As PortfolioAdvance In _context.PortfolioAdvance Where pa.Code.Equals(code) Select pa).FirstOrDefault()
        If query IsNot Nothing Then
            query.OriginalValue = (From pa As PortfolioAdvance In _context.PortfolioAdvance.AsNoTracking() Where pa.Code.Equals(code) Select pa).FirstOrDefault()
            Return query
        Else
            Return New PortfolioAdvance()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetPortfolioAdvanceById(Id As Integer) As PortfolioAdvance Implements IPortfolioAdvanceRepository.GetPortfolioAdvanceById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From pa As PortfolioAdvance In _context.PortfolioAdvance.Include("PortfolioTransfer").Include("Currency") Where pa.Id = Id Select pa).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From pa In _context.PortfolioAdvance.AsNoTracking() Where pa.Id = Id Select pa).FirstOrDefault()
            Dim account = (From ma In _context.MainAccounts.AsNoTracking() Where query.MainAccountId = ma.Id Select ma).FirstOrDefault()
            query.FullNameMainAccount = account.Number + " - " + account.Name
            query.MainAccounts = account
            Return query
        Else
            Return New PortfolioAdvance()
        End If
    End Function


    Public Function GetOnlyPortfolioAdvanceById(Id As Integer, tracking As Boolean) As PortfolioAdvance Implements IPortfolioAdvanceRepository.GetOnlyPortfolioAdvanceById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query As PortfolioAdvance = Nothing
        If tracking Then
            query = (From pa As PortfolioAdvance In _context.PortfolioAdvance Where pa.Id = Id Select pa).FirstOrDefault()
        Else
            query = (From pa As PortfolioAdvance In _context.PortfolioAdvance.AsNoTracking() Where pa.Id = Id Select pa).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From pa In _context.PortfolioAdvance.AsNoTracking() Where pa.Id = Id Select pa).FirstOrDefault()
            Return query
        Else
            Return New PortfolioAdvance()
        End If
    End Function

    Public Function GetPortfolioAdvanceByIdSimple(id As Integer) As PortfolioAdvance Implements IPortfolioAdvanceRepository.GetPortfolioAdvanceByIdSimple
        Return (From pa In _context.PortfolioAdvance Where pa.Id = id Select pa).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista todos los anticipos por Id del tercero
    ''' </summary>
    ''' <param name="ThirdId"></param>
    ''' <returns></returns>
    Public Function ListPorfolioAdvanceByThirdId(ThirdId As Integer) As List(Of PortfolioAdvance) Implements IPortfolioAdvanceRepository.ListPorfolioAdvanceByThirdId
        If ThirdId = 0 Then
            Throw New ArgumentNullException("ThirdId")
        End If
        Dim zero As Integer = 0
        Dim Confirmado As Integer = 2
        Dim query = (From pa As PortfolioAdvance In _context.PortfolioAdvance
                     Where pa.ThirdPartyId = ThirdId And pa.Value > zero And pa.Status = Confirmado
                     Select pa).ToList()
        If query IsNot Nothing AndAlso query.Count > 0 Then
            For Each pa As PortfolioAdvance In query
                pa.FullNameMainAccount = (From ac In _context.MainAccounts Where ac.Id = pa.MainAccountId Select String.Concat(ac.Number, " - ", ac.Name)).FirstOrDefault()
                pa.CashReceiptCode = (From cr As CashReceipts In _context.CashReceipts Where cr.Id = pa.CashReceiptId Select cr.Code).FirstOrDefault()
            Next
            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Lists the porfolio advance by third identifier with balance.
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">ThirdId</exception>
    Public Function ListPorfolioAdvanceByThirdIdAndAdmissionWithBalance(ThirdId As Integer, admission As String) As List(Of PortfolioAdvance) Implements IPortfolioAdvanceRepository.ListPorfolioAdvanceByThirdIdAndAdmissionWithBalance
        If ThirdId = 0 Then
            Throw New ArgumentNullException("ThirdId")
        End If
        Dim zero As Integer = 0
        Dim Confirmado As Integer = 2
        Dim query = (From pa As PortfolioAdvance In _context.PortfolioAdvance
                     Where pa.ThirdPartyId = ThirdId And pa.AdmissionNumber.Equals(admission) And pa.Value > zero And pa.Balance > 0 And pa.Status = Confirmado
                     Select pa).ToList()
        If query IsNot Nothing AndAlso query.Count > 0 Then
            For Each pa As PortfolioAdvance In query
                pa.FullNameMainAccount = (From ac In _context.MainAccounts Where ac.Id = pa.MainAccountId Select String.Concat(ac.Number, " - ", ac.Name)).FirstOrDefault()
                pa.CashReceiptCode = (From cr As CashReceipts In _context.CashReceipts Where cr.Id = pa.CashReceiptId Select cr.Code).FirstOrDefault()
            Next
            Return query
        Else
            Return Nothing
        End If
    End Function



    ''' <summary>
    ''' obtiene un anticipo por el id del detalle del recibo de caja
    ''' </summary>
    ''' <param name="cashRecepitsDetailId"></param>
    ''' <returns></returns>
    Public Function GetPortfolioAdvanceByCashReceiptsDetailId(cashRecepitsDetailId As Integer) As PortfolioAdvance Implements IPortfolioAdvanceRepository.GetPortfolioAdvanceByCashReceiptsDetailId
        Return (From pa In _context.PortfolioAdvance.AsNoTracking() Where pa.CashReceiptDetailId = cashRecepitsDetailId Select pa).FirstOrDefault()
    End Function

    Public Function GetAdvancePortfolioNote(thirdPartyId As Integer, codeAdvance As String, nature As Integer) As PortfolioAdvance Implements IPortfolioAdvanceRepository.GetAdvancePortfolioNote
        Dim res As PortfolioAdvance
        If nature = 1 Then 'debito
            res = (From pa In _context.PortfolioAdvance Where pa.Status = 2 And pa.ThirdPartyId = thirdPartyId And pa.Code = codeAdvance Select pa).FirstOrDefault()
        Else 'credito
            res = (From pa In _context.PortfolioAdvance Where pa.Status = 2 And pa.ThirdPartyId = thirdPartyId And pa.Code = codeAdvance And pa.Balance > 0 Select pa).FirstOrDefault()
        End If
        Return res
    End Function
End Class