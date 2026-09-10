'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-16
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class MixingStationSettingRepository
    Inherits GenericRepository(Of MixingStationSetting)
    Implements IMixingStationSettingRepository, Inject

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
    ''' Obtiene un parámetro de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetMixingStationSettingByOperativeUnitId(operativeUnitId As Integer, Optional tracking As Boolean = True) As MixingStationSetting Implements IMixingStationSettingRepository.GetMixingStationSettingByOperativeUnitId
        Dim query = From m In _context.MixingStationSetting
                    Where m.OperativeUnitId = operativeUnitId
                    Select m

        If Not tracking Then
            query = query.AsNoTracking()
        End If

        Dim setting = query.FirstOrDefault()

        If setting IsNot Nothing Then
            setting.OriginalValue = query.AsNoTracking().FirstOrDefault()
            Dim adjustmentConcept = _context.AdjustmentConcept.AsNoTracking() _
                .Where(Function(m) {setting.InventoryAdjustmentConceptOutputId, setting.InventoryAdjustmentConceptInputId}.Contains(m.Id)) _
                .Select(Function(m) New With {m.Id, m.Code, m.Name}).ToList()
            Dim manufacturer = _context.Manufacturer.AsNoTracking().Where(Function(m) m.Id = setting.ManufacturerId) _
                .Select(Function(m) New With {m.Code, m.Name}).FirstOrDefault()
            Dim measurementUnit = _context.InventoryMeasurementUnit.AsNoTracking().Where(Function(m) m.Id = setting.MeasurementUnitId) _
                .Select(Function(m) New With {m.Code, m.Name}).FirstOrDefault()
            Dim productType = _context.ProductType.AsNoTracking().Where(Function(m) m.Id = setting.ProductTypeId) _
                .Select(Function(m) New With {m.Code, m.Name}).FirstOrDefault()
            Dim packageUnit = _context.PackagingUnit.AsNoTracking().Where(Function(m) m.Id = setting.PackageUnitId) _
                .Select(Function(m) New With {m.Code, m.Name}).FirstOrDefault()
            Dim transitWarehouse = _context.Warehouse.AsNoTracking().Where(Function(m) m.Id = setting.TransitWarehouseId) _
                .Select(Function(m) New With {m.Code, m.Name}).FirstOrDefault()

            setting.InventoryAdjustmentConceptOutputCodeName = adjustmentConcept.Where(Function(m) m.Id = setting.InventoryAdjustmentConceptOutputId) _
                .Select(Function(m) $"{m.Code} - {m.Name}").FirstOrDefault()
            setting.InventoryAdjustmentConceptInputCodeName = adjustmentConcept.Where(Function(m) m.Id = setting.InventoryAdjustmentConceptInputId) _
                .Select(Function(m) $"{m.Code} - {m.Name}").FirstOrDefault()
            setting.ManufacturerCodeName = $"{manufacturer.Code} - {manufacturer.Name}"
            setting.MeasurementUnitCodeName = $"{measurementUnit.Code} - {measurementUnit.Name}"
            setting.ProductTypeCodeName = $"{productType.Code} - {productType.Name}"
            setting.PackageUnitCodeName = $"{packageUnit.Code} - {packageUnit.Name}"
            setting.TransitWarehouseCodeName = $"{transitWarehouse.Code} - {transitWarehouse.Name}"
        End If

        Return setting
    End Function
End Class
