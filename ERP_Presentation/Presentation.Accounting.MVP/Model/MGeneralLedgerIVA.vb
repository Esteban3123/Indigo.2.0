'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Se encarga de establecer comunicación con los servicios de GeneralLedgerIVA
''' </summary>
Public Class MGeneralLedgerIVA
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
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene el iva por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetGeneralLedgerIVAByCodeAsync(ByVal code As String) As Task(Of GeneralLedgerIVA)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetGeneralLedgerIVAByCodeAsync(code)
        End Using
    End Function

    ''' <summary>
    ''' obtiene el iva por id 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetGeneralLedgerIVAByIdAsync(ByVal id As Integer) As Task(Of ActionResult(Of GeneralLedgerIVA))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetGeneralLedgerIVAByIdAsync(id)
        End Using
    End Function

    ''' <summary>
    ''' obtiene el iva por id 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGeneralLedgerIVAById(ByVal id As Integer) As ActionResult(Of GeneralLedgerIVA)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetGeneralLedgerIVAById(id)
        End Using
    End Function

    ''' <summary>
    ''' obtiene el iva por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetGeneralLedgerIVAByCode(ByVal code As String) As GeneralLedgerIVA
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetGeneralLedgerIVAByCode(code)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un Iva
    ''' </summary>
    ''' <param name="doc">Iva a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function DeleteGeneralLedgerIVA(ByVal doc As GeneralLedgerIVA) As Task(Of ActionResult)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.DeleteGeneralLedgerIVAAsync(doc)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza el estado del Iva
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Async Function UpdateStateGeneralLedgerIVA(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of GeneralLedgerIVA))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.UpdateStateGeneralLedgerIvaAsync(code, state)
        End Using
    End Function

    ''' <summary>
    ''' Graba un Iva
    ''' </summary>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function SaveGeneralLedgerIVA(ByVal glIVA As GeneralLedgerIVA, ByVal idSequence As Int64) As Task(Of ActionResult(Of GeneralLedgerIVA))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Dim mess1 As New MessageHeader(Of Int64)(idSequence)
            Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess2 As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header2 As System.ServiceModel.Channels.MessageHeader = mess2.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header1)
            OperationContext.Current.OutgoingMessageHeaders.Add(header2)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveGeneralLedgerIVAAsync(glIVA)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequence() As Task(Of Domain.Entities.GeneralLedgerSequence)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetSequenseByIdFormAsync(Me._tagForm)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por el id de la configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia numerica</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Async Function GetNumericSequenseGroup(ByVal id As Int32) As Task(Of List(Of String))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetNumericSequenseGroupByIdAsync(id)
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
    ''' Listado de Iva
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGeneralLedgerIvaByState(status As Boolean) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListXPInstantFeedbackSource(Of GeneralLedgerIVAXpo)($"Status={status}", "Id;Code;Name;CodeName")
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
