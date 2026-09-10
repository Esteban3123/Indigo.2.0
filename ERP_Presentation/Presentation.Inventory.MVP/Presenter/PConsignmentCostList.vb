'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Oscar Stiven Astudillo Reyes
' Created          : 2024-02-16
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
#End Region


Public Class PConsignmentCostList


#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IConsignmentCostList

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"
    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IConsignmentCostList, Optional value As Boolean = True)
        If value Then
            If iview Is Nothing Then
                Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
            End If
            Me.View = iview
        End If
        Indigo = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' lista los proveedores que tenga almacenes en consignación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSupplierByWarehouseConsignment()
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListSupplierByWarehouseConsignment()
    End Function

    ''' <summary>
    ''' Lista los productos que tenga relacion almacen - proveedor
    ''' </summary>
    ''' <returns></returns>
    Public Function ListProductsBySupplierWarehouse()
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListXPInstantFeedbackSource(Of InventoryProductXpo)
    End Function

    ''' <summary>
    ''' Obtiene el tipo de producto por producto
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    Public Function GetProductType(ProductId) As InventoryProductXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetXPOObject(Of InventoryProductXpo)($"Id = {ProductId}")
    End Function

#End Region

End Class
