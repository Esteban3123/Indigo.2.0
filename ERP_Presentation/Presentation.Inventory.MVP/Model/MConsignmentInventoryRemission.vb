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

Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.CloudAgent

#End Region

Public Class MConsignmentInventoryRemission
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
    Public Function ListWarehouseBySupplier(SupplierId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListConsignmentWarehouseByStatusAndUser(True, SupplierId, _indigoSessionValues.UserIndigo)
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
    ''' lista las órdenes de compra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPurchaseOrderDetailByWarehouseConsignment(SupplierId As Int32, SupplierDistributionLineId As Int32, warehouseId As Int32, warehouseConsignment As Boolean) As XPCollection
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListPurchaseOrderDetailBySupplierIdAndSupplierDistributionLineIdAndWarehouseAndWarehouseConsignment(SupplierId, SupplierDistributionLineId, warehouseId, warehouseConsignment)
    End Function

    ''' <summary>
    ''' lista las remisiones de inventario en consignación sin reponer
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllConsignmentInventoryRemissionWithPendingQuantityReplacement(SupplierId As Int32, warehouseId As Int32) As XPCollection
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListAllConsignmentInventoryRemissionWithPendingQuantityReplacement(SupplierId, warehouseId)
    End Function

    ''' <summary>
    ''' cuenta el numero de movimientos en el kardex realizados en el almacen
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateChangeWarehouse(warehouseId As Integer) As Integer
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.GetKardexByWarehouseId(warehouseId)
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
    ''' obtiene el listado de contratos por proveedor
    ''' </summary>
    ''' <param name="supplierId"></param>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(supplierId As Integer, supplierDistributionLineId As Integer) As List(Of InventoryContractDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(supplierId, supplierDistributionLineId, 1)
    End Function

    ''' <summary>
    ''' obtiene una remision por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConsignmentInventoryRemissionById(id As Integer) As ConsignmentInventoryRemission
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetConsignmentInventoryRemissionById(id)
    End Function

    ''' <summary>
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetConsignmentInventoryRemissionByCode(code As String) As Task(Of ConsignmentInventoryRemission)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetConsignmentInventoryRemissionByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' lista los detalles de la remision
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConsignmentInventoryRemissionDetailByConsignmentInventoryRemissionId(ConsignmentInventoryRemissionId As Integer) As List(Of ConsignmentInventoryRemissionDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetConsignmentInventoryRemissionDetailByConsignmentInventoryRemissionId(ConsignmentInventoryRemissionId)
    End Function

    ''' <summary>
    ''' Obtiene un listado de remisisones de entrada por proveedor
    ''' </summary>
    ''' <param name="supplierId"></param>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConsignmentInventoryRemissionDetailBySupplierSupplierDistributionLineId(supplierId As Integer, supplierDistributionLineId As Integer)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListConsignmentInventoryRemissionDetailBySupplierAndSupplierDistributionLine(supplierId, supplierDistributionLineId)
    End Function

    ''' <summary>
    ''' Obtiene un listado de remisiones de entrada por proveedor
    ''' </summary>
    ''' <param name="SupplierId"></param>
    ''' <param name="SupplierDistributionLineId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConsignmentInventoryRemissionDetailBatchSerialBySupplierIdAndSupplierDistributionLineId(SupplierId As Integer, SupplierDistributionLineId As Integer)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListConsignmentInventoryRemissionDetailBatchSerialBySupplierAndSupplierDistributionLine(SupplierId, SupplierDistributionLineId)
    End Function

    ''' <summary>
    ''' guardar una remision
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemission"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveConsignmentInventoryRemission(ConsignmentInventoryRemission As ConsignmentInventoryRemission, idSequense As Integer, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of ConsignmentInventoryRemission))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveConsignmentInventoryRemissionAsync(ConsignmentInventoryRemission, idSequense, Me._indigoSessionValues.AuditMessageWcf, sequenceC)
    End Function

    ''' <summary>
    ''' guardar y confirmar una remision
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemission"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmConsignmentInventoryRemission(ConsignmentInventoryRemission As ConsignmentInventoryRemission, idSequense As Integer, action As Integer, ByVal sequenceC As Domain.Entities.InventorySequence, Optional controlCost As Boolean = False) As Task(Of ActionResult(Of Domain.Entities.ConsignmentInventoryRemission))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAndConfirmbConsignmentInventoryRemissionAsync(ConsignmentInventoryRemission, idSequense, Me._indigoSessionValues.AuditMessageWcf, sequenceC, action, controlCost)
    End Function

    ''' <summary>
    ''' lista las remisiones de inventario en consignación sin reponer
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConsignmentInventoryRemissionDetailControlXpo(DetailId As Integer, BatchSerialId As Integer?) As XPCollection(Of ConsignmentInventoryRemissionDetailControlXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListConsignmentInventoryRemissionDetailControlXpo(DetailId, BatchSerialId)
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