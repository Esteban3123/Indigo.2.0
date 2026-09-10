'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Duvan Mejia
' Created          : 23/07/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class QuantityRemainingRepository
    Inherits GenericRepository(Of QuantityRemaining)
    Implements IQuantityRemainingRepository, Inject

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

    ''' <summary>
    ''' Funcion para retornar todos los registro de la tabla sobrantes que esten en estado activo
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllQuantityRemaining() As List(Of QuantityRemaining) Implements IQuantityRemainingRepository.GetAllQuantityRemaining
        Dim Query = (From x In _context.QuantityRemaining.Include("InventoryProduct.ATC").Include("BatchSerial").Include("CampaignDetail")
                     Where x.Status = 1 Select x).ToList()
        Query.ForEach(Sub(v)
                          v.BatchSerialCode = v.BatchSerial.BatchCode
                          v.ProductFullName = $"{v.InventoryProduct.Code} - {v.InventoryProduct.Name}"
                          v.CampaignDetailNumber = $"Campaña Numero : {v.CampaignDetail.CampaignNumber}"
                          If v.BatchSerialId Is Nothing Then
                              v.ExpirationDate = v.InventoryProduct.ExpirationDate
                          Else
                              v.ExpirationDate = v.BatchSerial.ExpirationDate
                          End If
                          If v.InventoryProduct.ATCId Is Nothing Then
                              v.UnitMeasurementId = v.InventoryProduct.MeasurementUnitId
                          Else
                              If v.InventoryProduct.ATC.FormulationType = 2 Then
                                  v.UnitMeasurementId = v.InventoryProduct.ATC.VolumeMeasureUnit
                              Else
                                  v.UnitMeasurementId = v.InventoryProduct.ATC.WeightMeasureUnit
                              End If
                          End If
                      End Sub)
        Return Query
    End Function


    ''' <summary>
    ''' funcion para consultar la tabla de remanentes por almacen de remanente el cual se parametriza por central de mezclas
    ''' </summary>
    ''' <param name="_cMConfigurationId"></param>
    ''' <returns></returns>
    Public Function GetQuantityRemainingByMixingStation(_cMConfigurationId As Integer) As List(Of QuantityRemaining) Implements IQuantityRemainingRepository.GetQuantityRemainingByMixingStation

        If _cMConfigurationId = 0 Then
            Return New List(Of QuantityRemaining)
        End If

        Dim RemnantWareHouse = (From s In _context.CMWarehouse.AsNoTracking().Include("Warehouse").AsNoTracking()
                                Where s.IdMixingStation = _cMConfigurationId And s.WarehouseType = 6
                                Select s)?.FirstOrDefault

        If RemnantWareHouse Is Nothing Then
            Return New List(Of QuantityRemaining)
        End If

        Dim Query = (From x In _context.QuantityRemaining.Include("InventoryProduct.ATC").Include("BatchSerial").Include("CampaignDetail")
                     Where x.Status = 1 And x.WareHouseId = RemnantWareHouse.IdWarehouse Select x).ToList()
        Query.ForEach(Sub(v)
                          v.BatchSerialCode = v.BatchSerial.BatchCode
                          v.ProductFullName = $"{v.InventoryProduct.Code} - {v.InventoryProduct.Name}"
                          v.RemnantFullName = $"{RemnantWareHouse.Warehouse.Code} - {RemnantWareHouse.Warehouse.Name}"
                          v.CampaignDetailNumber = $"Campaña Numero : {v.CampaignDetail.CampaignNumber}"
                          If v.BatchSerialId Is Nothing Then
                              v.ExpirationDate = v.InventoryProduct.ExpirationDate
                          Else
                              v.ExpirationDate = v.BatchSerial.ExpirationDate
                          End If
                          If v.InventoryProduct.ATCId Is Nothing Then
                              v.UnitMeasurementId = v.InventoryProduct.MeasurementUnitId
                          Else
                              If v.InventoryProduct.ATC.FormulationType = 2 Then
                                  v.UnitMeasurementId = v.InventoryProduct.ATC.VolumeMeasureUnit
                              Else
                                  v.UnitMeasurementId = v.InventoryProduct.ATC.WeightMeasureUnit
                              End If
                          End If
                      End Sub)
        Return Query
    End Function

    ''' <summary>
    ''' Obtiene una lista de sobrantes filtrando por la campaña, el producto y el serial
    ''' </summary>
    ''' <param name="CampaignDetailId"></param>
    ''' <param name="ProductId"></param>
    ''' <param name="BatchSerialId"></param>
    ''' <returns></returns>
    Public Function GetListQuantityRemaining(CampaignDetailId As Integer, ProductId As Integer, BatchSerialId As Integer?) As List(Of QuantityRemaining) Implements IQuantityRemainingRepository.GetListQuantityRemaining

        If CampaignDetailId = 0 OrElse ProductId = 0 Then
            Return New List(Of QuantityRemaining)
        End If

        Dim Query = (From x In _context.QuantityRemaining.AsNoTracking()
                     Where x.CampaignDetailId = CampaignDetailId And x.ProductId = ProductId And x.BatchSerialId = BatchSerialId
                     Select x).ToList()

        Return Query
    End Function

End Class
