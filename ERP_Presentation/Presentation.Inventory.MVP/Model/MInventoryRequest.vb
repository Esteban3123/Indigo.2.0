'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 04/05/2015
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
#End Region

Public Class MInventoryRequest
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

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

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
    ''' Obtiene una solicitud de inventario por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventoryRequestByCode(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of InventoryRequest))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryRequestByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una solicitud de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventoryRequestById(ByVal id As Integer) As Task(Of InventoryRequest)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryRequestByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una solicitud de inventario
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveInventoryRequest(ByVal record As InventoryRequest, ByVal idSequense As Int64, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of InventoryRequest))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveInventoryRequestAsync(record, idSequense, Me.Indigo.AuditMessageWcf, sequenceC)
    End Function

    ''' <summary>
    ''' Elimina una solicitud de inventario
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteInventoryRequest(ByVal record As InventoryRequest) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteInventoryRequestAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateInventoryRequest(ByVal code As String, ByVal state As Byte) As Task(Of ActionResult(Of InventoryRequest))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ChangeStateInventoryRequestAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' CopyPaste/Import solicitudes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_CopyPasteAndImportRequests(data As List(Of List(Of String))) As ActionResult(Of List(Of InventoryRequestDetail))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SP_CopyPasteAndImportRequests(data)
    End Function

    ''' <summary>
    ''' CopyPaste/Import medicamentos,insumos o otros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_CopyPasteAndImportRequestsOtherDetail(data As List(Of List(Of String))) As ActionResult(Of List(Of InventoryRequestDetailOther))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SP_CopyPasteAndImportRequestsOtherDetail(data)
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
