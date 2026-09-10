'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Duván Mejía Cortes
' Created          : 22/07/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Threading.Tasks
Imports Domain.Base

Public Interface IPickingRepository
    Inherits IRepository(Of CampaignDetailPicking)

    ''' <summary>
    ''' Funcion para Listar los medicamentos del Invetario
    ''' </summary>
    ''' <param name="atcId"></param>
    ''' <param name="productId"></param>
    ''' <param name="supplyId"></param>
    ''' <param name="warehouseId"></param>
    ''' <returns></returns>
    Function GetProductsByAtcAndWarehouseAsync(atcId As Integer?, supplyId As Integer?, productId As Integer?, warehouseId As Integer?) As Task(Of List(Of PhysicalInventory))

    ''' <summary>
    ''' Funcion listar la Materia prima: Picking 
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Function GetCampaignPickingDetail(campaignDetailId As Integer) As List(Of CampaignDetailPicking)

    ''' <summary>
    ''' Funcion para Listar Cantidad Producto en el Inventario por Bachtserial
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <param name="batchSerial"></param>
    ''' <param name="WarehouseId"></param>
    ''' <returns></returns>
    Function GetQuantityByWarehouseProductAndBatchSerial(productId As Integer, batchSerial As Integer, WarehouseId As Integer) As PhysicalInventory

End Interface
