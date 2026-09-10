'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-12-2014
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
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.BillingRepository
Imports InventoryProductXpo = Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo

#End Region

Public Class MATC
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

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

#Region "Methods"

    ''' <summary>
    ''' Obtiene un atc por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetATC(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of ATC))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetATCAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un atc por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetATCById(ByVal id As Integer) As Task(Of ATC)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetATCByIdAsync(id)
        End Using
    End Function
    ''' <summary>
    ''' Obtiene un atc por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetATCByIdSimple(ByVal id As Integer) As ATC
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetATCById(id)
        End Using
    End Function

    ''' <summary>
    ''' Updates the state atc.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function UpdateStateATC(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of ATC))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.UpdateStateATCAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda un atc
    ''' </summary>
    Public Async Function SaveATC(ByVal atc As ATC, ByVal idSequense As Int64) As Task(Of ActionResult(Of ATC))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveATCAsync(atc, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un atc
    ''' </summary>
    ''' <returns></returns>
    Public Async Function DeleteATC(ByVal atc As ATC) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteATCAsync(atc, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the list admissions filter.
    ''' </summary>
    Function ListAllIHFORMEDI() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListAllIHFORMEDI()
    End Function

    ''' <summary>
    ''' Metodo para obtener el producto segun el medicamento
    ''' </summary>
    ''' <param name="ATCId"></param>
    ''' <returns></returns>
    Public Function GetProductByATCEntityXpo(ATCId As Integer) As InventoryProductXpo
        Dim filtroConsulta As String = "ATCId = " & ATCId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryProductXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Metodo para obtener lista de productos segun el medicamento
    ''' </summary>
    ''' <param name="ATCId"></param>
    ''' <returns></returns>
    Public Function GetProductByATCXpo(ATCId As Integer) As List(Of InventoryProductXpo)
        Dim filtroConsulta As String = "ATCId = " & ATCId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryProductXpo)(Nothing, filtroConsulta)
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
