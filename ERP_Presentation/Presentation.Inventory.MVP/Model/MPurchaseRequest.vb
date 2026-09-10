'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Hector Rodriguez Rubiano
' Created          : 15/04/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent
#End Region

Public Class MPurchaseRequest
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
    Public Async Function GetPurchaseRequestByCode(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of PurchaseRequest))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPurchaseRequestByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una solicitud de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPurchaseRequestById(ByVal id As Integer) As Task(Of PurchaseRequest)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPurchaseRequestByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una solicitud de inventario
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePurchaseRequest(ByVal record As PurchaseRequest, ByVal idSequense As Int64, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of PurchaseRequest))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SavePurchaseRequestAsync(record, idSequense, Me.Indigo.AuditMessageWcf, sequenceC)
    End Function

    ''' <summary>
    ''' Elimina una solicitud de inventario
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeletePurchaseRequest(ByVal record As PurchaseRequest) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeletePurchaseRequestAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStatePurchaseRequest(ByVal code As String, ByVal state As Byte) As Task(Of ActionResult(Of PurchaseRequest))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ChangeStatePurchaseRequestAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    Public Function LoadPurchaseRequestDetailToOrder() As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.LoadPurchaseRequestDetailToOrder()
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
