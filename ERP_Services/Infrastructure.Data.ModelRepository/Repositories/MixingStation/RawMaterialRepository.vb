'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : 
' Created          : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Domain.Base

Public Class RawMaterialRepository
    Inherits GenericRepository(Of CampaignDetailBasketDetail)
    Implements IRawMaterialRepository, Inject

    ''' <summary>
    ''' Contexto de Configuración de Central de Mezclas
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork
    ''' <summary>
    ''' Inicia el contexto de Configuración de Central de Mezclas
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetCampaignDetailBasketDetailById(id As String, Optional tracking As Boolean = True) As CampaignDetailBasketDetail Implements IRawMaterialRepository.GetCampaignDetailBasketDetailById
        Dim res = (From bg In _context.CampaignDetailBasketDetail Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.CampaignDetailBasketDetail.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New CampaignDetailBasketDetail
        End If
    End Function

    ''' <summary>
    ''' Listar por medio del id de campaignDetailId
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Public Function GetCampaignDetailBasketDetailBycampaignDetailId(campaignDetailId As String) As List(Of CampaignDetailBasketDetail) Implements IRawMaterialRepository.GetCampaignDetailBasketDetailBycampaignDetailId
        Return (From bg In _context.CampaignDetailBasketDetail Where bg.CampaignDetailId = campaignDetailId Select bg).ToList()
    End Function

    ''' <summary>
    ''' lista productos en la rejilla de C.U.M
    ''' </summary>
    ''' <param name="ATCNumber"></param>
    ''' <param name="warehouseId"></param>
    ''' <param name="StockId"></param>
    ''' <returns></returns>
    Public Function SP_ListPhysicalInventoryByCode(ATCNumber As String, warehouseId As Integer, StockId As Integer, MaquilaId As Integer?) As List(Of PhysicalInventory) Implements IRawMaterialRepository.SP_ListPhysicalInventoryByCodeMaterialRaw
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim result = _context.SP_ListPhysicalInventoryByCodeMaterialRaw(ATCNumber, warehouseId, StockId, MaquilaId).ToList()
        Return result?.Select(Function(item)
                                  Return New PhysicalInventory With {
                                                                        .Id = item.Id,
                                                                        .Code = item.Code,
                                                                        .WarehouseId = item.WarehouseId,
                                                                        .CodeNameWarehouse = item.WarehouseCodeName,
                                                                        .ProductId = item.ProductId,
                                                                        .CodeNameProduct = item.ProductCodeName,
                                                                        .BatchSerialId = item.BatchSerialId,
                                                                        .CodeNameBatchSerial = item.BatchSerialCode,
                                                                        .BatchSerialExpiredDate = item.BatchSerialExpirationDate,
                                                                        .Quantity = item.Quantity,
                                                                        .Covered = item.Covered
                                                                    }
                              End Function)?.ToList()
    End Function

End Class
