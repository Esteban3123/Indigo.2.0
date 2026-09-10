'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Pablo Alexander Salazar Sanchez
' Created          : 19-12-2022
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

#End Region

''' <summary>
''' Se encarga de establecer comunicación con los servicios de clase contable
''' </summary>
Public Class MMainAccountLevels
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Dim Indigo As SessionValues

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
        Me.Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    '''' <summary>
    '''' Obtener registro bloqueado
    '''' </summary>
    '''' <param name="IdForm"></param>
    '''' <param name="IdRecord"></param>
    '''' <returns></returns>
    'Public Async Function GetBlockRecord(ByVal IdForm As String, ByVal IdRecord As String) As Task(Of BlockRecordContract)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetBlockRecordContractByIdformAndIdRecordAsync(IdForm, IdRecord)
    'End Function

    '''' <summary>
    '''' Obtiene la secuencia numerica asignada al formulario
    '''' </summary>
    '''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of GeneralLedgerSequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetSequenseByIdFormAsync(Me._tagForm)
    End Function

    ''' <summary>
    ''' Obtiene todos Nivele de Cuentas Contables
    ''' </summary>
    ''' <returns>Currency</returns>
    Public Async Function GetAllMainAccountLevels(ByVal audit As AuditMessage) As Task(Of List(Of MainAccountLevels))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAllMainAccountLevelsAsync(Me.Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Obtiene el Nivele de cunta contable por codigo
    ''' </summary>
    ''' <param name="Code">Codigo del nivel de cuenta contable</param>
    ''' <returns>Currency</returns>
    Public Async Function GetMainAccountLevelsByCode(ByVal code As String) As Task(Of ActionResult(Of MainAccountLevels))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetMainAccountLevelsByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene el Nivele de Cuentas Contables por el Id
    ''' </summary>
    ''' <param name="code">Id de la moneda</param>
    ''' <returns>Currency</returns>
    Public Async Function GetMainAccountLevelsById(ByVal Id As String) As Task(Of ActionResult(Of MainAccountLevels))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetMainAccountLevelsByIdAsync(Id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda el Nivele de Cuentas Contables
    ''' </summary>
    ''' <param name="MainAccountLevels">Niveles de Cuentas Contables</param>
    ''' <returns>Si se hizo o no </returns>
    Public Async Function SaveMainAccountLevels(ByVal MainAccountLevels As MainAccountLevels, idSequence As Int64) As Task(Of ActionResult(Of MainAccountLevels))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveMainAccountLevelsAsync(MainAccountLevels, idSequence, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Borra el Nivele de cunta contable seleccionado
    ''' </summary>
    ''' <param name="MainAccountLevels">pais</param>
    ''' <returns>Si se hizo o no </returns>
    Public Async Function DeleteMainAccountLevels(ByVal MainAccountLevels As MainAccountLevels) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.DeleteMainAccountLevelsAsync(MainAccountLevels, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Consulta si el registro se encuentra bloqueado
    ''' </summary>
    ''' <param name="IdForm">Id del frontal</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' la información de bloqueo del registro</returns>
    'Public Async Function GetBlockRecord(ByVal idForm As String, ByVal idRecord As String) As Task(Of BlockRecordGeneralLedger)
    '    Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
    '        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
    '        Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
    '        Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
    '        OperationContext.Current.OutgoingMessageHeaders.Add(header)
    '        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetBlockRecordAccountingByIdformAndIdRecordAsync(idForm, idRecord)
    '    End Using
    'End Function

    '''' <summary>
    '''' Crea el bloqueo de un registro
    '''' </summary>
    '''' <param name="record">Registro a bloquear</param>
    '''' <returns>Resultados de la acción</returns>
    'Public Async Function SaveBlockRecord(ByVal record As BlockRecordGeneralLedger) As Task(Of ActionResult(Of BlockRecordGeneralLedger))
    '    Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
    '        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
    '        Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
    '        Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
    '        OperationContext.Current.OutgoingMessageHeaders.Add(header)
    '        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveBlockRecordAccountingAsync(record)
    '    End Using
    'End Function

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
