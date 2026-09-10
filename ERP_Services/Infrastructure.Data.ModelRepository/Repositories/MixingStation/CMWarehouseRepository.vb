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

Public Class CMWarehouseRepository
    Inherits GenericRepository(Of CMWarehouse)
    Implements ICMWarehouseRepository, Inject

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
    ''' Consulta el almacén según el tipo
    ''' </summary>
    ''' <param name="mixingStationId"></param>
    ''' <param name="wareHouseType"></param>
    ''' <returns></returns>
    Public Function GetWareHouseByType(mixingStationId As Integer, wareHouseType As Byte, Optional tracking As Boolean = True) As Warehouse Implements ICMWarehouseRepository.GetWareHouseByType
        Dim query = (From c In _context.CMWarehouse
                     Join w In _context.Warehouse On c.IdWarehouse Equals w.Id
                     Where c.IdMixingStation = mixingStationId AndAlso c.WarehouseType = wareHouseType AndAlso c.StateWH
                     Select w).AsNoTracking()

        If Not tracking Then query = query.AsNoTracking()

        Return query.FirstOrDefault()
    End Function

    ''' <summary>
    ''' Consulta el almacén según el tipo y campaña
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="wareHouseType"></param>
    ''' <returns></returns>
    Public Function GetByTypeAndCampaignDetailId(campaignDetailId As Integer, wareHouseType As Byte, Optional tracking As Boolean = True) As CMWarehouse Implements ICMWarehouseRepository.GetByTypeAndCampaignDetailId
        Dim query = (From c In _context.CMWarehouse
                     Join cp In _context.Campaign On cp.CMConfigurationId Equals c.IdMixingStation
                     Join cd In _context.CampaignDetail On cd.CampaignId Equals cp.Id
                     Where cd.Id = campaignDetailId AndAlso c.WarehouseType = wareHouseType AndAlso c.StateWH
                     Select c)

        If Not tracking Then query = query.AsNoTracking()

        Return query.FirstOrDefault()
    End Function

End Class

