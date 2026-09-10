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

Public Class MCtrPhysicalInventory
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
    ''' lista los inventarios fisicos por producto y almacen
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListPhysicalInventory(ProductId As Integer, WarehouseId As Integer, Optional isInput As Boolean = False) As List(Of PhysicalInventory)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetListPhysicalInventory(ProductId, WarehouseId, isInput)
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="PatientCode"></param>
    ''' <param name="AdmissionNumber"></param>
    ''' <param name="ProductId"></param>
    ''' <param name="WarehouseId"></param>
    ''' <returns></returns>
    Public Function GetListPhysicalInventoryCustody(PatientCode As String, AdmissionNumber As String,  ProductId As Integer, WarehouseId As Integer) As List(Of PhysicalInventoryCustody)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetListPhysicalInventoryCustody(PatientCode, AdmissionNumber, ProductId, WarehouseId)
    End Function

    Public Function GetPhysicalInventoryByProductAndWarehouse(ProductId As Integer, WarehouseId As Integer) As PhysicalInventory
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPhysicalInventoryByProductAndWarehouse(ProductId, WarehouseId)
    End Function

    ''' <summary>
    ''' lista los inventarios fisicos por producto 
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListPhysicalInventoryByProduct(ProductId As Integer) As List(Of PhysicalInventory)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetListPhysicalInventoryByProduct(ProductId)
    End Function

    ''' <summary>
    ''' Obtiene el registro de inventario fisico de un producto en un almacen especifico
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPhysicalInventory(ProductId As Integer, WarehouseId As Integer) As PhysicalInventory
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPhysicalInventory(ProductId, WarehouseId)
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de un producto que en el almacen especifico
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetQuantityByProductWarehouse(ProductId As Integer, WarehouseId As Integer) As Integer
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetQuantityByProductWarehouse(ProductId, WarehouseId)
    End Function

    ''' <summary>
    ''' Obtiene la linea de distribución
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetListSuppliers(productId As Integer) As List(Of ViewListSuppliersByControlXpo)
        Dim filter As String = "ProductId = " & productId
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.GetCollection(Of ViewListSuppliersByControlXpo)(Nothing, filter).ToList()
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
