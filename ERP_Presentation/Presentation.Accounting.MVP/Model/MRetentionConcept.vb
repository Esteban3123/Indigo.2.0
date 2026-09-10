'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Diego Andrés Roldán
' Created          : 15-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

#End Region

''' <summary>
''' Se encarga de establecer comunicación con los servicios de conceptos de retencion
''' </summary>
Public Class MRetentionConcept
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
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.GeneralLedgerSequence)
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

    Public Async Function GetRetentionConceptByIdBrachOfficeIdAsync(brachOfficeId As Integer) As Task(Of RetentionConcepts)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetRetentionConceptByIdBrachOfficeIdAsync(brachOfficeId)
    End Function

    ''' <summary>
    ''' Consulta un concepto de retención
    ''' </summary>
    ''' <param name="code">Código del tipo de documento</param>
    ''' <returns>Tipo de documento consultado</returns>
    Public Async Function GetRetentionConcept(ByVal code As String) As Task(Of RetentionConcepts)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetRetentionConceptAsync(code)
        End Using
    End Function

    Public Async Function GetIVARetentionConceptByThirdPartyIdAsync(thirdPartyId As Integer) As Task(Of RetentionConcepts)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetIVARetentionConceptByThirdPartyIdAsync(thirdPartyId)
    End Function

    ''' <summary>
    ''' Elimina un concepto de retención
    ''' </summary>
    ''' <param name="doc">Tipo de documento a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function DeleteRetentionConcept(ByVal doc As RetentionConcepts) As Task(Of ActionResult)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.DeleteRetentionConceptAsync(doc)
        End Using
    End Function

    ''' <summary>
    ''' Graba un concepto de retención
    ''' </summary>
    ''' <param name="doc">Tipo de documento</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function SaveRetentionConcept(ByVal doc As RetentionConcepts, ByVal idSequense As Int64) As Task(Of ActionResult(Of RetentionConcepts))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Dim mess1 As New MessageHeader(Of Int64)(idSequense)
            Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess2 As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header2 As System.ServiceModel.Channels.MessageHeader = mess2.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header1)
            OperationContext.Current.OutgoingMessageHeaders.Add(header2)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveRetentionConceptAsync(doc)
        End Using
    End Function

    ''' <summary>
    ''' Saves the block record accounting.
    ''' </summary>
    ''' <param name="record">The record.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecordAccounting(ByVal record As BlockRecordGeneralLedger) As Task(Of ActionResult(Of BlockRecordGeneralLedger))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveBlockRecordAccountingAsync(record)
        End Using
    End Function

    ''' <summary>
    ''' Deletes the block record accounting.
    ''' </summary>
    ''' <param name="record">The record.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecordAccounting(ByVal record As BlockRecordGeneralLedger) As Task(Of ActionResult)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.DeleteBlockRecordAccountingAsync(record)
        End Using
    End Function

    ''' <summary>
    ''' Gets the block record accounting.
    ''' </summary>
    ''' <param name="idForm">The identifier form.</param>
    ''' <param name="idRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Async Function GetBlockRecordAccounting(ByVal idForm As String, ByVal idRecord As String) As Task(Of BlockRecordGeneralLedger)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetBlockRecordAccountingByIdformAndIdRecordAsync(idForm, idRecord)
        End Using
    End Function

    ''' <summary>
    ''' Gets the retention by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetRetentionById(ByVal id As Integer) As Task(Of RetentionConcepts)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetRetentionByIdAsync(id)
        End Using
    End Function

    ''' <summary>
    ''' Gets the retention concept by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetRetentionConceptById(ByVal id As Integer) As Task(Of RetentionConcepts)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetRetentionByIdAsync(id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un listado de rangos de retenciones para 383 y 384
    ''' </summary>
    ''' <param name="RetentionConceptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetListRetentionRangeByRetentionConceptId(RetentionConceptId As Integer) As Task(Of ActionResult(Of List(Of RetentionConceptRanges)))
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListRetentionRangeByRetentionConceptIdAsync(RetentionConceptId)
    End Function

    Public Function GetRetentionConceptByIdSimple(ByVal id As Integer) As RetentionConcepts
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetRetentionById(id)
        End Using
    End Function

    ''' <summary>
    ''' Gets the retention by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetRetentionByIdSimple(ByVal id As Integer) As RetentionConcepts
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetRetentionById(id)
        End Using
    End Function

    ''' <summary>
    ''' cambiar estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean) As Task(Of ActionResult(Of RetentionConcepts))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.ChangeStateRetentionConceptAsync(code, state)
        End Using
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
