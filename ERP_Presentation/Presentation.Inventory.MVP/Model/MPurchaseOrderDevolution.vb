'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 19/05/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports System.ServiceModel
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent.IndigoReference.Bionexo
Imports System.Text
Imports System.Data
Imports System.IO
Imports Infrastructure.Data.Xpo.InventoryRepository

Public Class MPurchaseOrderDevolution
    Implements IDisposable

#Region "Fields"

    '' <summary>
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
    ''' lista los proveedores xpo 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPurchaseOrderDevolution() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListAllInventoryPurchaseOrderDevolution()
    End Function
    ''' <summary>
    ''' lista los proveedores xpo 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSupplier() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListAllSupplier()
    End Function

    Public Function ListPurchaseOrderDetailBySupplierId(supplierId As Integer) As XPCollection(Of InventoryPurcharseOrderDetailXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListPurchaseOrderDetailBySupplierId(supplierId)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un almacen
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePurchaseOrderDevolution(ByVal record As PurchaseOrderDevolution, ByVal idSequense As Int64) As Task(Of ActionResult(Of PurchaseOrderDevolution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SavePurchaseOrderDevolutionAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    
    Public Async Function GetPurchaseOrderDevolutionById(ByVal id As Integer) As Task(Of PurchaseOrderDevolution)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPurchaseOrderDevolutionByIdAsync(id)
    End Function

    
    Public Async Function GetPurchaseOrderDevolutionByCode(ByVal code As String) As Task(Of PurchaseOrderDevolution)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPurchaseOrderDevolutionByCodeAsync(code)
    End Function

    Public Async Function GetPurchaseOrderDevolutionDetailByPurchaseOrderDevolutionId(ByVal id As String) As Task(Of List(Of PurchaseOrderDevolutionDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPurchaseOrderDevolutionDetailByPurchaseOrderDevolutionIdAsync(id)
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
