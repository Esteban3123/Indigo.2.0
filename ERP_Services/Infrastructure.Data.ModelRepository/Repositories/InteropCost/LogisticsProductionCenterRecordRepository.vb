'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Juan F. Tamayo Puertas
' Created          : 2016-11-05
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class LogisticsProductionCenterRecordReporsitory
    Inherits GenericRepository(Of LogisticsProductionCenterRecord)
    Implements ILogisticsProductionCenterRecordReporsitory

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

    Public Function GetLogisticsProductionCenterRecordByCode(code As String) As LogisticsProductionCenterRecord Implements ILogisticsProductionCenterRecordReporsitory.GetLogisticsProductionCenterRecordByCode
        Dim result = (From l As LogisticsProductionCenterRecord In _context.LogisticsProductionCenterRecord.Include("LogisticsProductionCenterRecordDetail").Include("LogisticsProductionCenterRecordDetail.ProductionCenter").Include("LogisticsProductionCenterRecordDetail.InventoryMeasurementUnit") Where l.Code = code Select l).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Dim res = result(0)
            res.OriginalValue = (From l As LogisticsProductionCenterRecord In _context.LogisticsProductionCenterRecord.AsNoTracking().Include("LogisticsProductionCenterRecordDetail").AsNoTracking().Include("LogisticsProductionCenterRecordDetail.ProductionCenter").AsNoTracking().Include("LogisticsProductionCenterRecordDetail.InventoryMeasurementUnit").AsNoTracking() Where l.Code = code Select l).FirstOrDefault()
            Return res
        End If
        Return New LogisticsProductionCenterRecord()
    End Function

    Public Function GetLogisticsProductionCenterRecordById(id As Integer) As LogisticsProductionCenterRecord Implements ILogisticsProductionCenterRecordReporsitory.GetLogisticsProductionCenterRecordById
        Dim result = (From l As LogisticsProductionCenterRecord In _context.LogisticsProductionCenterRecord.Include("LogisticsProductionCenterRecordDetail").Include("LogisticsProductionCenterRecordDetail.ProductionCenter").Include("LogisticsProductionCenterRecordDetail.InventoryMeasurementUnit") Where l.Id = id Select l).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Dim res = result(0)
            res.OriginalValue = (From l As LogisticsProductionCenterRecord In _context.LogisticsProductionCenterRecord.AsNoTracking().Include("LogisticsProductionCenterRecordDetail").AsNoTracking().Include("LogisticsProductionCenterRecordDetail.ProductionCenter").AsNoTracking().Include("LogisticsProductionCenterRecordDetail.InventoryMeasurementUnit").AsNoTracking() Where l.Id = id Select l).FirstOrDefault()
            Return res
        End If
        Return New LogisticsProductionCenterRecord()
    End Function

    Public Function GetLogisticsProductionCenterPendingImport(ProductionCenterId As Integer, ByVal Year As Integer, ByVal month As Integer) As List(Of LogisticsProductionCenterRecordDetail) Implements ILogisticsProductionCenterRecordReporsitory.GetLogisticsProductionCenterPendingImport
        'Dim result = (From l In _context.Logi)
        Return Nothing
    End Function

    Public Function ListReportConsolidatedCCLogistic(ByVal FechaInicial As Date, ByVal FechaFinal As Date, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As String, ByVal ProductionCenterIdFin As String, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer, ByVal OrganizationalStructureLevel As Integer) As List(Of SP_ReportConsolidatedCCLogisticA_Result) Implements ILogisticsProductionCenterRecordReporsitory.ListReportConsolidatedCCLogistic


        If ProductionCenterIdIni Is Nothing Then
            ProductionCenterIdIni = ""
        End If

        If ProductionCenterIdFin Is Nothing Then
            ProductionCenterIdFin = ""
        End If

        Dim pclStart As String = ProductionCenterIdIni
        Dim pclEnd As String = ProductionCenterIdFin

        Dim result = _context.SP_ReportConsolidatedCCLogisticA(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin, OrganizationalStructureLevel).ToList()
        Return result
    End Function
    Public Function ListReportConsolidatedCCLogisticB(ByVal FechaInicial As DateTime, ByVal FechaFinal As DateTime, ByVal ProductionCenterIdLogistic As Integer, ByVal ProductionCenterIdIni As Integer, ByVal ProductionCenterIdFin As Integer, ByVal InventoryMeasurementUnitIdIni As Integer, ByVal InventoryMeasurementUnitIdFin As Integer) As List(Of SP_ReportConsolidatedCCLogisticB_Result) Implements ILogisticsProductionCenterRecordReporsitory.ListReportConsolidatedCCLogisticB

        Dim result = _context.SP_ReportConsolidatedCCLogisticB(FechaInicial, FechaFinal, ProductionCenterIdLogistic, ProductionCenterIdIni, ProductionCenterIdFin, InventoryMeasurementUnitIdIni, InventoryMeasurementUnitIdFin).ToList()
        Return result
    End Function

#End Region

End Class
