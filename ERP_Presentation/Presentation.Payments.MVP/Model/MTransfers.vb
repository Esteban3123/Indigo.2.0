'***********************************************************************
' Assembly         : Presentacion.Payments.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/07/2014
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

#End Region

Public Class MTransfers
    Implements IDisposable

#Region "Builder"

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

#End Region

#Region "Functions"

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPaymentTransfer(ByVal code As String) As Task(Of ActionResult(Of PaymentTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetPaymentsTransferAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentTransferById(ByVal id As Integer) As ActionResult(Of PaymentTransfer)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetPaymentsTransferById(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un traslado
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePaymentTransferAsync(ByVal record As PaymentTransfer, ByVal idSequense As Int64) As Task(Of ActionResult(Of PaymentTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SavePaymentsTransferAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un concepto de nota
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeletePaymentTransferAsync(ByVal record As PaymentTransfer) As Task(Of ActionResult)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoPayments.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)

            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.DeletePaymentsTransferAsync(record, Me.Indigo.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Confirma el traslado
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmTransfer(ByVal record As PaymentTransfer, ByVal idSequense As Int64) As Task(Of ActionResult(Of PaymentTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SaveAndConfirmTransferAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    Public Async Function ImportBillsToPaymentTransfer(dataCopyPaste As List(Of List(Of String)), parameters As List(Of Object)) As Task(Of ActionResult(Of List(Of PaymentTransferDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ImportBillsToPaymentTransferAsync(dataCopyPaste, parameters)
    End Function

    ''' <summary>
    ''' Consulta EL TRM de las monedas origne vs destino
    ''' </summary>
    ''' <param name="FromCurrencyId"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Public Async Function GetTRMbyCurrencyId(ToCurrencyId As Integer, FromCurrencyId As Integer, Optional DateTrm As Date? = Nothing) As Task(Of ActionResult(Of TRM))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetTRMbyCurrencyIdAsync(ToCurrencyId, FromCurrencyId, Me.Indigo, DateTrm, Nothing)
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
