'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 16-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region

''' <summary>
''' Se encarga de establecer comunicación con los servicios de niveles de cuenta
''' </summary>
Public Class MAccountLevel
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Consulta un nivel de cuenta
    ''' </summary>
    ''' <param name="code">Código del tipo de documento</param>
    ''' <returns>Tipo de documento consultado</returns>
    Public Async Function GetAccountLevelByCode(ByVal code As String, ByVal Tracking As Boolean) As Task(Of MainAccountLevels)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountLevelByCodeAsync(code, Tracking)
    End Function

    Public Async Function GetAccountLevelById(ByVal id As Integer, ByVal Tracking As Boolean) As Task(Of MainAccountLevels)       
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountLevelByIdAsync(id, Tracking)
    End Function


    ''' <summary>
    ''' Elimina un nivel de cuenta
    ''' </summary>
    ''' <param name="doc">Tipo de documento a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function DeleteAccountLevel(ByVal doc As MainAccountLevels) As Task(Of ActionMessageResult(Of MainAccountLevels))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.DeleteAccountLevelAsync(doc)
        End Using
    End Function

    ''' <summary>
    ''' Graba un nivel de cuenta
    ''' </summary>
    ''' <param name="doc">Tipo de documento</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function SaveAccountLevel(ByVal doc As MainAccountLevels) As Task(Of Boolean)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveAccountLevelAsync(doc)
        End Using
    End Function

    ''' <summary>
    ''' Crea el bloqueo de un registro
    ''' </summary>
    ''' <param name="record">Registro a bloquear</param>
    ''' <returns>Resultados de la acción</returns>
    Public Async Function SaveBlockRecord(ByVal record As BlockRecordGeneralLedger) As Task(Of ActionResult(Of BlockRecordGeneralLedger))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveBlockRecordAccountingAsync(record)
        End Using
    End Function

    ''' <summary>
    ''' Elimina el bloqueo del registro
    ''' </summary>
    ''' <param name="record">Registro a desbloquear</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function DeleteBlockRecord(ByVal record As BlockRecordGeneralLedger) As Task(Of ActionResult)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.DeleteBlockRecordAccountingAsync(record)
        End Using
    End Function

    ''' <summary>
    ''' Consulta si el registro se encuentra bloqueado
    ''' </summary>
    ''' <param name="IdForm">Id del frontal</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' la información de bloqueo del registro</returns>
    Public Async Function GetBlockRecord(ByVal idForm As String, ByVal idRecord As String) As Task(Of BlockRecordGeneralLedger)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetBlockRecordAccountingByIdformAndIdRecordAsync(idForm, idRecord)
        End Using
    End Function

    ''' <summary>
    ''' Gets all accoutn level.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetAllAccoutnLevel() As Task(Of List(Of MainAccountLevels))       
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAllAccountLevelAsync()
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
