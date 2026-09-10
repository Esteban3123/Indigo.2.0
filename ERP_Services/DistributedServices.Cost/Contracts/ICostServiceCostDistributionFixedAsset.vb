'***********************************************************************
' Assembly         : DistributedServices.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/09/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ICostServiceCostDistributionFixedAsset

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCostDistributionFixedAssetById(id As Integer) As ActionResult(Of CostDistributionFixedAsset)

    ''' <summary>
    ''' Obtiene la entidad por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCostDistributionFixedAsset(ByVal code As String, audit As AuditMessage) As ActionResult(Of CostDistributionFixedAsset)

    ''' <summary>
    ''' Obtiene la distribucion por physicalId, año y mes
    ''' </summary>
    ''' <param name="physicalId"></param>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCostDistributionFixedAssetDataByPhysicalIdAndYearMonth(year As Integer, month As Integer, Optional physicalId As Integer = Nothing) As ActionResult(Of List(Of Domain.Entities.CostDistributionFixedAsset))

    ''' <summary>
    ''' Obtiene la distribucion por physicalId, año y mes
    ''' </summary>
    ''' <param name="physicalId"></param>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCostDistributionFixedAssetByPhysicalIdAndYearMonth(physicalId As Integer, year As Integer, month As Integer) As ActionResult(Of CostDistributionFixedAsset)

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    <OperationContract()>
    Function ListPeriodWithDistributionFixedDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Lista las distribuciones de activos por año y mes
    ''' </summary>
    <OperationContract()>
    Function ListDistributionFixedAssetByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionFixedAsset)

    ''' <summary>
    ''' Exporta la lista de distribuciones de activos fijos de un periodo
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SP_ExportExcelCostDistributionFixedAsset(Year As Integer, Month As Integer) As ActionResult(Of List(Of SP_ExportExcelCostDistributionFixedAsset_Result))

    ''' <summary>
    '''  Importa distribuciones de un periodo
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ImportCostDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer, ImportIds As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Guarda la entidad
    ''' </summary>
    ''' <param name="CostDistributionFixedAsset"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCostDistributionFixedAsset(CostDistributionFixedAsset As Domain.Entities.CostDistributionFixedAsset, audit As AuditMessage, idSequence As Int64) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostDistributionFixedAsset)

    ''' <summary>
    '''  Confirma todos las distribuciones pendientes de un periodo
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SP_ConfirmMasiveCostDistributionFixedAsset(Year As Integer, Month As Integer, OperatingUnitId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Elimina una distribución de activos fijos
    ''' </summary>
    ''' <param name="CostDistributionFixedAsset"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCostDistributionFixedAsset(CostDistributionFixedAsset As Domain.Entities.CostDistributionFixedAsset, audit As AuditMessage) As Domain.Base.Entities.ActionResult

End Interface