'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class PaymentNotesAccountPayableAdvanceRepository
    Inherits GenericRepository(Of PaymentNotesAccountPayableAdvance)
    Implements IPaymentNotesAccountPayableAdvanceRepository

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
    ''' Obtiene el registro por id de la tabla de paymentNotesAccountPayableAdvance
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentNotesAccountPayableAdvanceById(id As String, Optional tracking As Boolean = True) As PaymentNotesAccountPayableAdvance Implements IPaymentNotesAccountPayableAdvanceRepository.GetPaymentNotesAccountPayableAdvanceById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.PaymentNotesAccountPayableAdvance.AsNoTracking Where d.Id = id Select d)
        If res.Count > 0 Then
            Return res.SingleOrDefault
        Else
            Return New PaymentNotesAccountPayableAdvance()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el listado de paymentNotesAccountPayableAdvance por el id de la cxp o del anticipo
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="type"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentNotesAccountPayableAdvanceByIdAccountPayableOrAdvance(id As Integer, type As Integer, Optional tracking As Boolean = True) As List(Of PaymentNotesAccountPayableAdvance) Implements IPaymentNotesAccountPayableAdvanceRepository.GetPaymentNotesAccountPayableAdvanceByIdAccountPayableOrAdvance
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim listPaymentNotesAccountPayableAdvance As List(Of PaymentNotesAccountPayableAdvance) = Nothing
        If type = 0 Then
            listPaymentNotesAccountPayableAdvance = (From d In Me._context.PaymentNotesAccountPayableAdvance.AsNoTracking Where d.AccountPayableId = id Select d).ToList
        ElseIf type = 1 Then
            listPaymentNotesAccountPayableAdvance = (From d In Me._context.PaymentNotesAccountPayableAdvance.AsNoTracking Where d.AdvancePaymentId = id Select d).ToList
        End If
        Return listPaymentNotesAccountPayableAdvance
    End Function

    ''' <summary>
    ''' Gets the payment notes account payable advance by account payable share identifier.
    ''' </summary>
    ''' <param name="accountPayableListId"></param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetPaymentNotesAccountPayableAdvanceByAccountPayableListId(accountPayableListId As List(Of Integer), Optional tracking As Boolean = True) As List(Of String) Implements IPaymentNotesAccountPayableAdvanceRepository.GetPaymentNotesAccountPayableAdvanceByAccountPayableListId
        Dim Uno As Byte = 1
        Return (From r In _context.PaymentNotesAccountPayableAdvance.AsNoTracking()
                     Join ap In _context.AccountPayable.AsNoTracking() On r.AccountPayableId Equals ap.Id
                     Join n In _context.PaymentNotes On r.PaymentNoteId Equals n.Id
                     Where accountPayableListId.Contains(r.AccountPayableId) AndAlso n.Status = Uno
                     Select String.Concat(ap.BillNumber, ";", n.Code)).ToList()
    End Function

End Class
