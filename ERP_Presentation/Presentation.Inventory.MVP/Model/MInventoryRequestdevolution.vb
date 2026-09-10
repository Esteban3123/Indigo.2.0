'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Faiber Julian Mora Dussan
' Created          : 26-09-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository

'Imports System.ServiceModel.OperationContext

#End Region

Public Class MInventoryRequestdevolution
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Instancia a los valores de sesión
    ''' </summary>
    Private _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="tag"></param>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    Sub New()
        ' TODO: Complete member initialization 
    End Sub

    ''' <summary>
    ''' Obtiene una solicitud de devolucion de solicitudes por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventoryRequestDevolutionbyCode(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of InventoryRequestDevolution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetRequestDevolutionByCodeAsync(code, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una devolución de solicitudes
    ''' </summary>
    ''' <param name="record"></param>
    ''' <param name="IdSequense"></param>
    ''' <param name="sequenceC"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveInventoryRequestdevolution(ByVal record As InventoryRequestDevolution, ByVal IdSequense As Int64, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of InventoryRequestDevolution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveInventoryRequestDevolutionAsync(record, IdSequense, Me._sessionValues.AuditMessageWcf, sequenceC)
    End Function


    ''' <summary>
    ''' Guarda y Confirma la Devolución de solicitud
    ''' </summary>
    ''' <param name="InventoryRequestDevolution"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="action"></param>
    ''' <param name="sequenceC"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmRequestDevolution(InventoryRequestDevolution As InventoryRequestDevolution, idSequense As Integer, action As Integer, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of Domain.Entities.InventoryRequestDevolution))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Dim mess1 As New MessageHeader(Of Int64)(idSequense)
            Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Me._sessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess2 As New MessageHeader(Of AuditMessage)(Me._sessionValues.AuditMessageWcf)
            Dim header2 As System.ServiceModel.Channels.MessageHeader = mess2.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Dim mess3 As New MessageHeader(Of Domain.Entities.InventorySequence)(sequenceC)
            Dim header3 As System.ServiceModel.Channels.MessageHeader = mess3.GetUntypedHeader(ConfigurationFile.SESS_SEQUENCEC, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header1)
            OperationContext.Current.OutgoingMessageHeaders.Add(header2)
            OperationContext.Current.OutgoingMessageHeaders.Add(header3)

            'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAndConfirmRequestDevolutionAsync(InventoryRequestDevolution, action)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una devolución de solicitud por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventoryRequestDevolutionById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of InventoryRequestDevolution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetRequestdevolutionByIdAsync(id, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una devolución de solicitud
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteRequestDevolution(ByVal record As InventoryRequestDevolution) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteInventoryrequestdevolutionAsync(record, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStatusInventoryRequestdevolution(ByVal code As String, ByVal status As Byte) As Task(Of ActionResult(Of InventoryRequestDevolution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ChangeStatusInventoryRequestAsync(code, status, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un listado de solicitudes por tipo de solicitud
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRequestconfirmedByType(type As Integer) As XPCollection(Of InventoryRequestDetailXpo)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetListRequestConfirmed(type)
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