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

#End Region

''' <summary>
''' Modelo de conexion con los servicios distribuidos de la corporacion
''' </summary>
Public Class MOpeningBalance
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
    ''' Obtiene un saldo inicial
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetOpeningBalance(ByVal code As String) As Task(Of ActionResult(Of InitialBalance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetOpeningBalanceAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ' ''' <summary>
    ' ''' Obtiene un saldo inicial
    ' ''' </summary>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Function GetAdvancePaymentsById(ByVal id As Integer) As AdvancePayments
    '    Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoPayments.InnerChannel)
    '        Me.Indigo.AuditMessageWcf.Functional = _tagForm
    '        Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
    '        Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
    '        OperationContext.Current.OutgoingMessageHeaders.Add(header)
    '        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetMoneyAdvanceById(id)
    '    End Using
    'End Function

    ''' <summary>
    ''' Guarda o actualiza un saldo inicial
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveOpeningBalance(ByVal record As InitialBalance, ByVal idSequense As Int64) As Task(Of ActionResult(Of InitialBalance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SaveOpeningBalanceAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un saldo inicial
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmOpeningBalance(ByVal record As InitialBalance, ByVal modeSaveAndConfirm As Boolean, ByVal idSequense As Int64, ByVal _idOperativeUnit As Int32) As Task(Of ActionResult(Of InitialBalance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ConfirmOpeningBalanceAsync(record, modeSaveAndConfirm, _idOperativeUnit, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un saldo inicial
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteOpeningBalance(ByVal record As InitialBalance) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.DeleteOpeningBalanceAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of InitialBalance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ChangeStateOpeningBalanceAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Valida los campos del copyPaste de las facturas
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetBillsInitialBalance(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of InitialBalanceAccountPayable)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SetBillsInitialBalanceAsync(data)
    End Function

    ''' <summary>
    ''' Valida los campos del copyPaste de anticipos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetAdvanceInitialBalance(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of InitialBalanceAdvance)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SetAdvanceInitialBalanceAsync(data)
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
