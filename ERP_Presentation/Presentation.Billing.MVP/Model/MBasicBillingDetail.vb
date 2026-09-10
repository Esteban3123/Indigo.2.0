'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-11-14
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
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class MBasicBillingDetail
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private Indigo As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private MyTag As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me.MyTag = tag
        Me.Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un producto por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetInventoryProductByIdWithoutAggregates(ByVal id As Integer) As Task(Of InventoryProduct)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryProductByIdWithoutAggregatesAsync(id)
    End Function

    ''' <summary>
    ''' Obtiene un producto por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetInventoryProductByCodeWithoutAggregates(ByVal code As String) As InventoryProduct
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryProductByCodeWithoutAggregates(code)
    End Function

    ''' <summary>
    ''' Obtiene el concepto de facturacion por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetBillingConceptById(ByVal id As Integer) As Task(Of BillingConcept)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetIPSServiceGroupByIdAsync(id)
    End Function

    ''' <summary>
    ''' Obtiene el activo fijo por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetPhysicalAssetById(ByVal id As Integer) As FixedAssetPhysicalAsset
        Return IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetPhysicalAssetById(id)
    End Function

    ''' <summary>
    ''' Obtiene la parte del activo fijo por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetPhysicalAssetPartById(ByVal id As Integer) As FixedAssetPhysicalAssetParts
        Return IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetPhysicalAssetPartById(id)
    End Function

    ''' <summary>
    ''' Obtiene un producto por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetRateRetentionByIdAndAddressId(ByVal id As Integer, ByVal addressId As Integer) As RetentionConceptByCity
        Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetRetentionConceptByCity(id, addressId)
    End Function

    ''' <summary>
    ''' Verifica que en el Inventario Fisico los productos tengan cantidades mayores a cero
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPhysicalInventoryByProductId(productId As Integer) As XPCollection(Of PhysicalInventoryXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetWareHouseByPhysicalInventoryByProductId(productId)
    End Function

    ''' <summary>
    ''' Obtiene un almacen por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetWarehouseById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of Warehouse))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetWarehouseByIdAsync(id, Me.Indigo.AuditMessageWcf)
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
