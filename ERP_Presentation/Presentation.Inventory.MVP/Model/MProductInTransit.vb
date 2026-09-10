'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 28-10-2014
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
Imports System.Dynamic

#End Region

Public Class MProductInTransit
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

    '' <summary>
    ''' metodo para copiar y pegar o importar un archivo de excel
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetCopyPasteOrImportFileProductInTransit(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of ProductInTransitDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SetCopyPasteOrImportFileProductInTransitAsync(dataImportFile, dataCopyPaste)
    End Function

    ''' <summary>
    ''' obtiene el listado de contratos por proveedor
    ''' </summary>
    ''' <param name="supplierId"></param>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(supplierId As Integer, supplierDistributionLineId As Integer) As List(Of InventoryContractDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(supplierId, supplierDistributionLineId, 1)
    End Function

    Public Function ListPurchaseOrderDetail(SupplierId As Int32, SupplierDistributionLineId As Int32, warehouseId As Int32) As XPCollection
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineIdAndWarehouse(SupplierId, SupplierDistributionLineId, warehouseId)
    End Function

    Public Function ListPurchaseOrderDetailByWarehouseConsignment(SupplierId As Int32, SupplierDistributionLineId As Int32, warehouseId As Int32, warehouseConsignment As Boolean) As XPCollection
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineIdAndWarehouseAndWarehouseConsignment(SupplierId, SupplierDistributionLineId, warehouseId, warehouseConsignment)
    End Function

    ''' <summary>
    ''' obtiene un listado de ordener de compra por proveedor
    ''' </summary>
    ''' <param name="supplierId"></param>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(supplierId As Integer, supplierDistributionLineId As Integer) As List(Of PurchaseOrderDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineId(supplierId, supplierDistributionLineId)
    End Function
   
    ''' <summary>
    ''' lista los detalles de la remision
    ''' </summary>
    ''' <param name="ProductInTransitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProductInTransitDetailByProductInTransitId(ProductInTransitId As Integer) As List(Of ProductInTransitDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetProductInTransitDetailByProductInTransitId(ProductInTransitId)
    End Function
    ''' <summary>
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetProductInTransitByCode(code As String) As Task(Of ProductInTransit)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetProductInTransitByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' obtiene una remision por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProductInTransitById(id As Integer) As ProductInTransit
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetProductInTransitById(id)
    End Function
    ''' <summary>
    ''' guardar una remision
    ''' </summary>
    ''' <param name="ProductInTransit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveProductInTransit(ProductInTransit As ProductInTransit, idSequense As Integer, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of ProductInTransit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveProductInTransitAsync(ProductInTransit, Me._indigoSessionValues.AuditMessageWcf, idSequense, sequenceC)
    End Function
    ''' <summary>
    ''' guardar y confirmar una remision
    ''' </summary>
    ''' <param name="ProductInTransit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmProductInTransit(ProductInTransit As ProductInTransit, idSequense As Integer, action As Integer, ByVal sequenceC As Domain.Entities.InventorySequence, Optional controlCost As Boolean = False) As Task(Of ActionResult(Of Domain.Entities.ProductInTransit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAndConfirmbProductInTransitAsync(ProductInTransit, Me._indigoSessionValues.AuditMessageWcf, idSequense, sequenceC, action, controlCost)
    End Function
    ''' <summary>
    ''' lista los lotes por el id del producto
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBatchSerialByProductId(ProductId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListBatchSerialByProductId(ProductId)
    End Function
    ''' <summary>
    ''' lista los proveedores por con sus lineas de distribucion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSuppliersDistributionLines() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CommonService.ListSuppliersDistributionLines()
    End Function
    ''' <summary>
    ''' lista los almacenes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListWarehouse() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, _indigoSessionValues.UserIndigo)
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
