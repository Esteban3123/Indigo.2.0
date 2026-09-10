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
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.Xpo

#End Region

Public Class MCtrProducts
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
    ''' lista los productos 
    ''' </summary>
    ''' <returns></returns>+
    ''' <remarks></remarks>
    Public Function ListInventoryProductByStatusByNoClassType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListInventoryProductByStatusByNoClassType(True, 1)
    End Function

    ''' <summary>
    ''' lista los productos por atc
    ''' </summary>
    ''' <returns></returns>+
    ''' <remarks></remarks>
    Public Function ListInventoryProductByATC(ByVal atcId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListInventoryProductByATC(True, 1, atcId)
    End Function

    ''' <summary>
    ''' lista los productos por supply
    ''' </summary>
    ''' <returns></returns>+
    ''' <remarks></remarks>
    Public Function ListInventoryProductBySupply(ByVal supplyId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListInventoryProductBySupply(True, 1, supplyId)
    End Function

    ''' <summary>
    ''' lista los productos 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInventoryProductByStatusByNoClassTypeByAlmacen(ByVal IdAlmacen As Integer, Optional VirtualStore As Boolean = False) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListInventoryProductByStatusByNoClassTypeByAlmacen(True, {1, 4}, IdAlmacen, VirtualStore)
    End Function

    ''' <summary>
    ''' Devuelve los productos de un registro de tarifas de productos y servicios
    ''' </summary>
    ''' <param name="ProductAndServiceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GeProductsByProductAndServiceId(ProductAndServiceId As Integer) As XPCollection(Of BillingRepository.ProductFeeDetailXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.GeProductsByProductAndServiceId(ProductAndServiceId)
    End Function

    ''' <summary>
    ''' Obtiene un objeto de tipo InventoryProductXpo
    ''' </summary>
    Public Function ListInventoryProductsById(productIds As List(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListInventoryProductsById(productIds)
    End Function

    ''' <summary>
    ''' lista los productos 
    ''' </summary>
    ''' <returns></returns>+
    ''' <remarks></remarks>
    Public Function ListInventoryProductByStatusByNoClassTypeByAlmacenNoAffectedInventory(ByVal IdAlmacen As Integer, Optional VirtualStore As Boolean = False, Optional ListProductsIds As List(Of Integer) = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListInventoryProductByStatusByNoClassTypeByAlmacenNoAffectedInventory(True, 1, IdAlmacen, VirtualStore, ListProductsIds)
    End Function


    ''' <summary>
    ''' lista los almacenes por el id del producto
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPhysicalInventoryByProductId(ProductId As Integer) As XPCollection(Of PhysicalInventoryXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListPhysicalInventoryByProductId(ProductId)
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
