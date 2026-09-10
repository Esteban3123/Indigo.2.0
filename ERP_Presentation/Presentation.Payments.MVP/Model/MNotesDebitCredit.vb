'***********************************************************************
' Assembly         : Presentacion.Payments.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Modelo de conexion con los servicios distribuidos de la corporacion
''' </summary>
Public Class MNotesDebitCredit
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtiene una nota debito/credito por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPaymentsNote(ByVal code As String) As Task(Of ActionResult(Of PaymentNotes))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetPaymentsNoteAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una nota debito/credito
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePaymentsNote(ByVal record As PaymentNotes, ByVal idSequense As Int64) As Task(Of ActionResult(Of PaymentNotes))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SavePaymentsNoteAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una nota debito/credito
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePaymentNotesComplete(ByVal record As PaymentNotes, ByVal listAccountPayable As List(Of AccountPayable), ByVal listAdvancePayments As List(Of AdvancePayments), modeConfirm As Boolean, ByVal idSequense As Int64) As Task(Of ActionResult(Of PaymentNotes))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SavePaymentNotesCompleteAsync(record, listAccountPayable, listAdvancePayments, modeConfirm, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Confirma la nota debito/credito
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmNotesDebitCredit(ByVal record As PaymentNotes, ByVal listAccountPayable As List(Of AccountPayable), ByVal listAdvancePayments As List(Of AdvancePayments), ByVal idSequense As Int64) As Task(Of ActionResult(Of PaymentNotes))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ConfirmPaymentNotesAsync(record, listAccountPayable, listAdvancePayments, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una nota debito/credito
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeletePaymentsNote(ByVal record As PaymentNotes) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.DeletePaymentsNoteAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of PaymentNotes))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ChangeStatePaymentsNoteAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    Public Async Function ImportBillsToPortfolioNote(dataCopyPaste As List(Of List(Of String)), parameters As List(Of Object)) As Task(Of ActionResult(Of List(Of AccountPayable)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ImportBillsToPortfolioNoteAsync(dataCopyPaste, parameters)
    End Function

    Public Async Function ImportAdvancesToPortfolioNote(dataCopyPaste As List(Of List(Of String)), parameters As List(Of Object)) As Task(Of ActionResult(Of List(Of AdvancePayments)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ImportAdvancesToPortfolioNoteAsync(dataCopyPaste, parameters)
    End Function

    ''' <summary>
    ''' Obtiene las obligaciones relacionadas con la cuenta por pagar
    ''' </summary>
    ''' <param name="AccountPayableId"></param>
    ''' <returns></returns>
    Public Function GetObligationDetails(AccountPayableId As Integer, Nature As Integer) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListObligationDetailByEntity("AccountPayable", AccountPayableId, Nature)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
