'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class MoneyAdvanceRepository
    Inherits GenericRepository(Of AdvancePayments)
    Implements IMoneyAdvanceRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un anticipo especifica
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMoneyAdvance(code As String, Optional tracking As Boolean = True) As AdvancePayments Implements IMoneyAdvanceRepository.GetMoneyAdvance
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As AdvancePayments In Me._context.AdvancePayments Where d.Code.Equals(code.Trim()) Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As AdvancePayments In Me._context.AdvancePayments.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New AdvancePayments()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMoneyAdvanceById(id As Integer, Optional tracking As Boolean = True) As AdvancePayments Implements IMoneyAdvanceRepository.GetMoneyAdvanceById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d As AdvancePayments In Me._context.AdvancePayments Where d.Id = id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As AdvancePayments In Me._context.AdvancePayments.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New AdvancePayments()
        End If
    End Function

    ''' <summary>
    ''' Lists the advance payment by third identifier.
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">ThirdId</exception>
    Public Function ListAdvancePaymentByThirdId(ThirdId As Integer) As List(Of AdvancePayments) Implements IMoneyAdvanceRepository.ListAdvancePaymentByThirdId
        If ThirdId = 0 Then
            Throw New ArgumentNullException("ThirdId")
        End If
        Dim query = (From d As AdvancePayments In Me._context.AdvancePayments
                     Join s As Supplier In _context.Supplier On d.IdSupplier Equals s.Id
                     Where s.IdThirdParty = ThirdId And d.Balance > 0 And d.Status = 2
                     Select d).ToList()
        If query IsNot Nothing AndAlso query.Count > 0 Then
            For Each ap In query
                ap.FullNameMainAccount = (From ac In _context.MainAccounts Where ac.Id = ap.IdAccount Select String.Concat(ac.Number, " - ", ac.Name)).FirstOrDefault()
            Next
            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdvanceByCode(ByVal code As String) As AdvancePayments Implements IMoneyAdvanceRepository.GetAdvanceByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As AdvancePayments In Me._context.AdvancePayments Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing AndAlso res.Id <> 0 Then
            Return res
        Else
            Return New AdvancePayments()
        End If
    End Function

    Public Function ListMoneyAdvanceById(listId As List(Of Integer?)) As List(Of AdvancePayments) Implements IMoneyAdvanceRepository.ListMoneyAdvanceById
        Return (From cr In _context.AdvancePayments.AsNoTracking() Where listId.Contains(cr.Id) Select cr).ToList()
    End Function

    Public Function SaveMoneyAdvanceList(moneyAdvanceList As List(Of AdvancePayments)) As IEnumerable(Of AdvancePayments) Implements IMoneyAdvanceRepository.SaveMoneyAdvanceList
        Return _context.AdvancePayments.AddRange(moneyAdvanceList)
    End Function
End Class
