'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class PaymentsNoteConceptRepository
    Inherits GenericRepository(Of AccountPayableConceptNotes)
    Implements IPaymentsNoteConceptRepository, Inject

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
    ''' Obtiene un concepto de nota
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentNoteConcept(code As String, Optional tracking As Boolean = True) As AccountPayableConceptNotes Implements IPaymentsNoteConceptRepository.GetPaymentNoteConcept
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As AccountPayableConceptNotes In Me._context.AccountPayableConceptNotes Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.IdAccount IsNot Nothing Then
                Dim account = (From a In _context.MainAccounts.AsNoTracking Where res.IdAccount = a.Id Select a).FirstOrDefault
                res.NumberNameAccount = account.Number + " - " + account.Name
            End If

            'Concepto Retención
            If res.RetentionConceptId IsNot Nothing Then
                Dim retentionConcept = (From rc In _context.RetentionConcepts.AsNoTracking Where res.RetentionConceptId = rc.Id Select rc).FirstOrDefault
                res.RetentionConceptCodeName = retentionConcept.Code + " - " + retentionConcept.Name
            End If

            'Concepto Retencíón 383
            If res.RetentionConcept383Id IsNot Nothing Then
                Dim retentionConcept = (From rc In _context.RetentionConcepts.AsNoTracking Where res.RetentionConcept383Id = rc.Id Select rc).FirstOrDefault
                res.RetentionConcept383CodeName = retentionConcept.Code + " - " + retentionConcept.Name
            End If

            'Cuenta conatble retención 383
            If res.RetentionMainAccount383Id IsNot Nothing Then
                Dim account383 = (From a In _context.MainAccounts.AsNoTracking Where res.RetentionMainAccount383Id = a.Id Select a).FirstOrDefault
                res.RetentionMainAccount383NumberName = account383.Number + " - " + account383.Name
            End If

            res.OriginalValue = (From d As AccountPayableConceptNotes In Me._context.AccountPayableConceptNotes.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New AccountPayableConceptNotes()
        End If
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPaymentsNoteConcepts() As List(Of AccountPayableConceptNotes) Implements IPaymentsNoteConceptRepository.ListAllPaymentsNoteConcepts
        Dim paymentsNoteConcept = From e In _context.AccountPayableConceptNotes
                             Select e
        Return paymentsNoteConcept.ToList()
    End Function

    ''' <summary>
    ''' Consulta el concepto de nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentNoteConceptById(id As String, Optional tracking As Boolean = True) As AccountPayableConceptNotes Implements IPaymentsNoteConceptRepository.GetPaymentNoteConceptById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.AccountPayableConceptNotes.Include("MainAccounts") Where d.Id = id Select d)
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As AccountPayableConceptNotes In Me._context.AccountPayableConceptNotes.Include("MainAccounts").AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New AccountPayableConceptNotes()
        End If
    End Function

End Class
