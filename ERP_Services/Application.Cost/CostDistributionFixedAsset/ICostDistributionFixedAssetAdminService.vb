'***********************************************************************
' Assembly         : Application.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/09/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICostDistributionFixedAssetAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    Function GetCostDistributionFixedAssetById(id As Integer) As ActionResult(Of CostDistributionFixedAsset)

    ''' <summary>
    ''' Obtiene la entidad por código
    ''' </summary>
    Function GetCostDistributionFixedAsset(ByVal code As String) As ActionResult(Of CostDistributionFixedAsset)

    ''' <summary>
    ''' Obtiene la distribucion por physicalId, año y mes
    ''' </summary>
    Function GetCostDistributionFixedAssetDataByPhysicalIdAndYearMonth(year As Integer, month As Integer, Optional physicalId As Integer = Nothing) As ActionResult(Of List(Of CostDistributionFixedAsset))

    ''' <summary>
    ''' Obtiene la distribucion por physicalId, año y mes
    ''' </summary>
    Function GetCostDistributionFixedAssetByPhysicalIdAndYearMonth(physicalId As Integer, year As Integer, month As Integer) As ActionResult(Of CostDistributionFixedAsset)

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Function ListPeriodWithDistributionFixedDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Lista las distribuciones de activos por año y mes
    ''' </summary>
    Function ListDistributionFixedAssetByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionFixedAsset)

    ''' <summary>
    ''' Exporta la lista de distribuciones de activos fijos de un periodo
    ''' </summary>
    Function SP_ExportExcelCostDistributionFixedAsset(Year As Integer, Month As Integer) As ActionResult(Of List(Of SP_ExportExcelCostDistributionFixedAsset_Result))

    ''' <summary>
    '''  Importa distribuciones de un periodo
    ''' </summary>
    Function ImportCostDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportIds As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Guarda la entidad
    ''' </summary>
    Function SaveCostDistributionFixedAsset(ByVal CostDistributionFixedAsset As CostDistributionFixedAsset, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CostDistributionFixedAsset)

    ''' <summary>
    '''  Confirma todos las distribuciones pendientes de un periodo
    ''' </summary>
    Function SP_ConfirmMasiveCostDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Elimina una distribución de activos fijos
    ''' </summary>
    Function DeleteCostDistributionFixedAsset(ByVal CostDistributionFixedAsset As CostDistributionFixedAsset, ByVal audit As AuditMessage) As ActionResult

End Interface
