'***********************************************************************
' Assembly         : Infrastructure.Data.CostRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/12/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class CostLogisticsProductionCenterRecordRepository
    Inherits GenericRepository(Of CostLogisticsProductionCenterRecord)
    Implements ICostLogisticsProductionCenterRecordReporsitory

#Region "Fields"

    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetCostLogisticsProductionCenterPendingImport(ProductionCenterId As Integer, Year As Integer, month As Integer) As List(Of CostLogisticsProductionCenterRecordDetail) Implements ICostLogisticsProductionCenterRecordReporsitory.GetCostLogisticsProductionCenterPendingImport
        'Dim result = (From l In _context.Logi)
        Return Nothing
    End Function

    Public Function GetCostLogisticsProductionCenterRecordByCode(code As String) As CostLogisticsProductionCenterRecord Implements ICostLogisticsProductionCenterRecordReporsitory.GetCostLogisticsProductionCenterRecordByCode
        Dim result = (From l As CostLogisticsProductionCenterRecord In _context.CostLogisticsProductionCenterRecord.Include("CostLogisticsProductionCenterRecordDetail") Where l.Code = code Select l).FirstOrDefault
        If result IsNot Nothing Then

            result.ProductionCenterCodeName = (From x In _context.CostProductionCenter.AsNoTracking Where x.Id = result.ProductionCenterId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()

            If result.CostLogisticsProductionCenterRecordDetail IsNot Nothing AndAlso result.CostLogisticsProductionCenterRecordDetail.Count > 0 Then
                For Each itemDetail In result.CostLogisticsProductionCenterRecordDetail
                    itemDetail.ProductionCenterCodeName = (From x In _context.CostProductionCenter.AsNoTracking Where x.Id = itemDetail.ProductionCenterId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
                    itemDetail.MeasurementUnitCodeName = (From x In _context.InventoryMeasurementUnit.AsNoTracking Where x.Id = itemDetail.InventoryMeasurementUnitId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
                Next
            End If

            result.OriginalValue = (From l As CostLogisticsProductionCenterRecord In _context.CostLogisticsProductionCenterRecord.AsNoTracking() Where l.Code = code Select l).FirstOrDefault()
            Return result
        End If
        Return New CostLogisticsProductionCenterRecord()
    End Function

    Public Function GetCostLogisticsProductionCenterRecordById(id As Integer) As CostLogisticsProductionCenterRecord Implements ICostLogisticsProductionCenterRecordReporsitory.GetCostLogisticsProductionCenterRecordById
        Dim result = (From l As CostLogisticsProductionCenterRecord In _context.CostLogisticsProductionCenterRecord.Include("CostLogisticsProductionCenterRecordDetail") Where l.Id = id Select l).FirstOrDefault
        If result IsNot Nothing Then
            result.OriginalValue = (From l As CostLogisticsProductionCenterRecord In _context.CostLogisticsProductionCenterRecord.AsNoTracking() Where l.Id = id Select l).FirstOrDefault()
            Return result
        End If
        Return New CostLogisticsProductionCenterRecord()
    End Function

    Public Function ListReportConsolidatedCCLgistic(ByVal FechaInicial As Date, ByVal FechaFinal As Date, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As String, ByVal ProductionCenterIdFin As String, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer, ByVal OrganizationalStructureLevel As Integer) As List(Of SP_CostReportConsolidatedCCLogisticA_Result) Implements ICostLogisticsProductionCenterRecordReporsitory.ListReportConsolidatedCCLogistic


        If ProductionCenterIdIni Is Nothing Then
            ProductionCenterIdIni = ""
        End If

        If ProductionCenterIdFin Is Nothing Then
            ProductionCenterIdFin = ""
        End If

        Dim pclStart As String = ProductionCenterIdIni
        Dim pclEnd As String = ProductionCenterIdFin

        Dim result = _context.SP_CostReportConsolidatedCCLogisticA(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin, OrganizationalStructureLevel).ToList()
        Return result
    End Function
    Public Function ListReportConsolidatedCCLogisticB(ByVal FechaInicial As DateTime, ByVal FechaFinal As DateTime, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As Integer, ByVal ProductionCenterIdFin As Integer, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer) As List(Of SP_CostReportConsolidatedCCLogisticB_Result) Implements ICostLogisticsProductionCenterRecordReporsitory.ListReportConsolidatedCCLogisticB

        Dim result = _context.SP_CostReportConsolidatedCCLogisticB(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin).ToList()
        Return result
    End Function

#End Region

End Class
