'***********************************************************************
' Assembly         : Presentacion.Payments.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 06-05-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Maintenance.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

Public Class MBlockRecordAndSequenseMaintenance
    Implements IDisposable

#Region "fields"
    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private Indigo As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Builder"
    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        Me.Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of MaintenanceSequence)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetSequenseByIdFormAsync(Me._tagForm, Indigo)
    End Function

    Public Function GetSequenseSimple() As MaintenanceSequence
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetSequenseByIdForm(Me._tagForm, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por el id de la configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia numerica</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Async Function GetNumericSequenseGroup(ByVal id As Int32) As Task(Of List(Of String))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetNumericSequenseGroupByIdAsync(id, Indigo)
    End Function

    ''' <summary>
    ''' Funcion para guardar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecord(ByVal Record As BlockRecordMaintenance) As Task(Of ActionResult(Of BlockRecordMaintenance))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveBlockRecordMaintenanceAsync(Record, Indigo)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para eliminar el registro bloqueado
    ''' </summary>
    ''' <param name="Record">The registro.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecord(ByVal Record As BlockRecordMaintenance) As Task(Of ActionResult)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteBlockRecordMaintenanceAsync(Record, Indigo)
        End Using
    End Function

    ''' <summary>
    ''' Obtener registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <returns></returns>
    Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of BlockRecordMaintenance)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetBlockRecordMaintenanceByIdformAndIdRecordAsync(IdForm, IdRecord, Indigo)
        End Using
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
