'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Hector Rodriguez Rubiano
' Created          : 08-01-2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
#End Region
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.CloudAgent

Public Class MInventorySupplie
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String


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
    ''' Obtener un insumo por su id
    ''' </summary>
    ''' <param name="id">El id del InventorySupplie</param>
    ''' <returns></returns>
    Public Function GetSupplieByIdSimple(ByVal id As Integer) As InventorySupplie
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventorySupplieById(id)
        End Using
    End Function

    ''' <summary>
    ''' Obtener un insumo por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del estado de InventorySupplie</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetInventorySupplieAsync(ByVal code As String) As Task(Of Object)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventorySupplieByCodeAsync(code, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Graba un insumo en modo asincrono
    ''' </summary>
    ''' <param name="ObjInventorySupplie"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="sequenceC"></param>
    ''' <returns></returns>
    Public Async Function SaveInventorySupplieAsync(ByVal ObjInventorySupplie As InventorySupplie, ByVal idSequense As Int64, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of InventorySupplie))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveInventorySupplieAsync(ObjInventorySupplie, idSequense, Me.Indigo.AuditMessageWcf, sequenceC)
    End Function

    ''' <summary>
    ''' Elimina un insumo modo asincrono
    ''' </summary>
    ''' <param name="ObjInventorySupplie">InventorySupplie</param>
    ''' <returns>Un valor que indica si se elimino con exito el estado de InventorySupplie</returns>
    ''' 
    Public Async Function DeleteInventorySupplieAsync(ByVal ObjInventorySupplie As InventorySupplie) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteInventorySupplieAsync(ObjInventorySupplie, Me.Indigo.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateStatus(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of InventorySupplie))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ChangeStateInventorySupplieAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Metodo para obtener un producto segun el insumo
    ''' </summary>
    ''' <param name="supplieId"></param>
    ''' <returns></returns>
    Public Function GetProductBySupplieEntityXpo(supplieId As Integer) As InventoryProductXpo
        Dim filtroConsulta As String = "SupplieId = " & supplieId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetXPOObject(Of InventoryProductXpo)(filtroConsulta)
    End Function


    ''' <summary>
    ''' Metodo para obtener lista de productos segun el insumo
    ''' </summary>
    ''' <param name="supplieId"></param>
    ''' <returns></returns>
    Public Function GetProductBySupplieXpo(supplieId As Integer) As List(Of InventoryProductXpo)
        Dim filtroConsulta As String = "SupplieId = " & supplieId
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

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class

