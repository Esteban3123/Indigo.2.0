'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-10
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class RawMaterialDevolutionRepository
    Inherits GenericRepository(Of RawMaterialDevolution)
    Implements IRawMaterialDevolutionRepository, Inject

    ''' <summary>
    ''' Contexto de Tipo de dosis unitaria
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Tipo de Dosis Unitaria
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una devolucion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetRawMaterialDevolutionByCode(code As String, Optional tracking As Boolean = True) As RawMaterialDevolution Implements IRawMaterialDevolutionRepository.GetRawMaterialDevolutionByCode
        Dim query = (From rmd In _context.RawMaterialDevolution Where rmd.Code = code Select rmd)

        If Not tracking Then query = query.AsNoTracking()

        Dim raw = query.FirstOrDefault()

        If raw IsNot Nothing Then
            Dim campaignDetail = _context.CampaignDetail.Include("Campaign.CMConfiguration").AsNoTracking().Where(Function(m) m.Id = raw.CampaignDetailId).FirstOrDefault()
            raw.CampaignDetailName = $"CM: {campaignDetail.Campaign.CMConfiguration.Code}, Campaña # {campaignDetail.CampaignNumber}"

            Dim stockWarehouse = GetWareHouseByType(campaignDetail.Campaign.CMConfigurationId, 1)
            Dim productionWarehouse = GetWareHouseByType(campaignDetail.Campaign.CMConfigurationId, 2)

            If productionWarehouse IsNot Nothing Then
                raw.ProductionWarehouseId = productionWarehouse.Id
                raw.ProductionWarehouseCodeName = $"{productionWarehouse.Code} - {productionWarehouse.Name}"
            End If
            If stockWarehouse IsNot Nothing Then
                raw.StockWarehouseId = stockWarehouse.Id
                raw.StockWarehouseCodeName = $"{stockWarehouse.Code} - {stockWarehouse.Name}"
            End If
        End If

        Return raw
    End Function

    ''' <summary>
    ''' Consulta el almacén según el tipo
    ''' </summary>
    ''' <param name="mixingStationId"></param>
    ''' <param name="wareHouseType"></param>
    ''' <returns></returns>
    Private Function GetWareHouseByType(mixingStationId As Integer, wareHouseType As Byte) As Warehouse
        Return (From c In _context.CMWarehouse
                Join w In _context.Warehouse On c.IdWarehouse Equals w.Id
                Where c.IdMixingStation = mixingStationId AndAlso c.WarehouseType = wareHouseType AndAlso c.StateWH
                Select w).AsNoTracking().FirstOrDefault()
    End Function
End Class
