'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Text

Public Class CostProductionCenterRepository
    Inherits GenericRepository(Of CostProductionCenter)
    Implements ICostProductionCenterRepository
    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetCostProductionCenter(code As String) As CostProductionCenter Implements ICostProductionCenterRepository.GetCostProductionCenter
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From o In _context.CostProductionCenter.Include("CostProductionCenterServiceArea").Include("CostProductionCenterHomologation").Include("CostProductionCenterCostCenter") Where o.Code.Equals(code) Select o).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From o In _context.CostProductionCenter.AsNoTracking() Where o.Code.Equals(code) Select o).FirstOrDefault()
            query.NullTextOrganizationalStructure = (From o In _context.CostOrganizationalStructureOfCosts Where o.Id = query.OrganizationalStructureOfCostId Select String.Concat(o.Code, " - ", o.Name)).FirstOrDefault()

            If query.CategoryId IsNot Nothing Then
                query.CategoryCodeName = (From o In _context.CostProductionCenterCategory.AsNoTracking Where o.Id = query.CategoryId Select String.Concat(o.Code, " - ", o.Name)).FirstOrDefault()
            End If

            If query.CancellationCostMainAccountId IsNot Nothing Then
                query.NumberNameMainAccountCancellationCost = (From o In _context.MainAccounts.AsNoTracking Where o.Id = query.CancellationCostMainAccountId Select String.Concat(o.Number, " - ", o.Name)).FirstOrDefault()
            End If

            If query.CostProductionCenterServiceArea IsNot Nothing AndAlso query.CostProductionCenterServiceArea.Any() Then
                For Each item As CostProductionCenterServiceArea In query.CostProductionCenterServiceArea
                    Dim funcUnit As FunctionalUnit = (From f In _context.FunctionalUnit.AsNoTracking() Where f.Id = item.FunctionalUnitId Select f).FirstOrDefault()
                    If funcUnit IsNot Nothing AndAlso funcUnit.Id > 0 Then
                        item.FunctionalUnitName = funcUnit.Name
                        item.FunctionalUnitCode = funcUnit.Code
                        'item.CTNCuentaServiceArea = servArea.CTNCUENTA2
                    End If
                Next
            End If

            If query.CostProductionCenterHomologation IsNot Nothing AndAlso query.CostProductionCenterHomologation.Any() Then
                For Each item As CostProductionCenterHomologation In query.CostProductionCenterHomologation
                    Dim mainAccountOrigin As String = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.AccountOriginId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                    Dim mainAccountDestination As String = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.AccountTargetId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                    If Not String.IsNullOrEmpty(mainAccountOrigin) Then
                        item.FullNameAccountOrigin = mainAccountOrigin
                    End If
                    If Not String.IsNullOrEmpty(mainAccountDestination) Then
                        item.FullNameAccountDestination = mainAccountDestination
                    End If
                Next
            End If

            If query.CostProductionCenterCostCenter IsNot Nothing AndAlso query.CostProductionCenterCostCenter.Any() Then
                For Each item As CostProductionCenterCostCenter In query.CostProductionCenterCostCenter
                    Dim costCenter As CostCenter = (From c In _context.CostCenter.AsNoTracking() Where c.Id = item.CostCenterId Select c).FirstOrDefault()
                    item.CostCenterCode = costCenter.Code
                    item.CostCenterName = costCenter.Name
                Next
            End If

            Return query
        Else
            Return New CostProductionCenter()
        End If
    End Function

    Public Function GetCostProductionCenterById(id As Integer) As CostProductionCenter Implements ICostProductionCenterRepository.GetCostProductionCenterById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From o In _context.CostProductionCenter Where o.Id = id Select o).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From o In _context.CostProductionCenter.AsNoTracking() Where o.Id = id Select o).FirstOrDefault()
            Return query
        Else
            Return New CostProductionCenter()
        End If
    End Function

    Public Function ListCostProductionCenter() As List(Of CostProductionCenter) Implements ICostProductionCenterRepository.ListCostProductionCenter
        Return (From pc In _context.CostProductionCenter Select pc).ToList()
    End Function

    Public Function GetProductionCenterByCostCenterId(costCenterId As Integer) As CostProductionCenter Implements ICostProductionCenterRepository.GetProductionCenterByCostCenterId
        Return (From pc In _context.CostProductionCenter.AsNoTracking() Where pc.CostProductionCenterCostCenter.Any(Function(x) x.CostCenterId = costCenterId) Select pc).FirstOrDefault()
    End Function

    Public Function ListReportOperatingResult(InitialMonth As Integer, EndMonth As Integer, Year As Integer, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResult_Result) Implements ICostProductionCenterRepository.ListReportOperatingResult
        Dim result = _context.SP_CostReportOperatingResult(InitialMonth, EndMonth, Year, CodePCenterIni, CodePCenterFin, StructureOfCostId).ToList()
        Return result
    End Function

    Public Function ListReportOperatingResultsByOrganizationalStructure(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResultsByOrganizationalStructure_Result) Implements ICostProductionCenterRepository.ListReportOperatingResultsByOrganizationalStructure
        Dim result = _context.SP_CostReportOperatingResultsByOrganizationalStructure(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId).ToList()
        Return result
    End Function

    Public Function ListReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, StructureOfCostId As Integer) As List(Of SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics_Result) Implements ICostProductionCenterRepository.ListReportOperatingResultsByOrganizationalStatisticalGraphics
        Dim result = _context.SP_CostReportOperatingResultsByOrganizationalStatisticalGraphics(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, StructureOfCostId).ToList()
        Return result
    End Function

    ''' <summary>
    ''' Lista la informacion de costo por Unidad de Medida
    ''' </summary>
    ''' <param name="initialMonth"></param>
    ''' <param name="initialYear"></param>
    ''' <param name="endMonth"></param>
    ''' <param name="endYear"></param>
    ''' <param name="initialMeasurementUnitCode"></param>
    ''' <param name="endMeasurementUnitCode"></param>
    ''' <param name="ProductionCenterId"></param>
    ''' <returns></returns>
    Public Function ListReportCostCostMeasurementUnit(initialMonth As Integer, initialYear As Integer, endMonth As Integer, endYear As Integer, initialMeasurementUnitCode As String, endMeasurementUnitCode As String, ProductionCenterId As Integer) As List(Of spCostReportCostMeasurementUnit_Result) Implements ICostProductionCenterRepository.ListReportCostCostMeasurementUnit
        If initialMonth = Nothing OrElse initialMonth = 0 Then
            Throw New ArgumentNullException("initialMonth")
        End If
        If initialYear = Nothing OrElse initialYear = 0 Then
            Throw New ArgumentNullException("initialYear")
        End If
        If endMonth = Nothing OrElse endMonth = 0 Then
            Throw New ArgumentNullException("endMonth")
        End If
        If endYear = Nothing OrElse endYear = 0 Then
            Throw New ArgumentNullException("endYear")
        End If
        If ProductionCenterId = Nothing OrElse ProductionCenterId = 0 Then
            Throw New ArgumentNullException("ProductionCenterId")
        End If

        Dim result As List(Of spCostReportCostMeasurementUnit_Result)

        'si vienen los filtros de centro de costo vacios
        If initialMeasurementUnitCode = String.Empty And endMeasurementUnitCode = String.Empty Then
            result = _context.spCostReportCostMeasurementUnit(initialMonth, initialYear, endMonth, endYear, Nothing, Nothing, ProductionCenterId).ToList()
        Else
            result = _context.spCostReportCostMeasurementUnit(initialMonth, initialYear, endMonth, endYear, initialMeasurementUnitCode, endMeasurementUnitCode, ProductionCenterId).ToList()
        End If

        'result = _context.spCostReportCostMeasurementUnit(initialMonth, initialYear, endMonth, endYear, initialMeasurementUnitCode, endMeasurementUnitCode, ProductionCenterId).ToList()
        Return result
    End Function

    ''' <summary>
    ''' Valida que los centros de costo que esten en la entidad no existan en un centro de producción omitiendo el centro de producción actual
    ''' </summary>
    ''' <param name="ListInfo">Obtiene la lista de todos los centros de costo que se encuentran registrados en otros centros de producción</param>
    ''' <param name="IdCurrent">Obtiene el Id del centro de producción actual</param>
    ''' <returns></returns>
    Public Function ValidateCostCenterIds(ListInfo As List(Of CostProductionCenterCostCenter), IdCurrent As String) As String Implements ICostProductionCenterRepository.ValidateCostCenterIds
        Dim ListIds As New List(Of Integer)
        Dim ListReturn As New StringBuilder
        If ListInfo IsNot Nothing AndAlso ListInfo.Count > 0 Then
            ListInfo.ForEach(Sub(x) ListIds.Add(x.CostCenterId))
        End If
        If ListIds.Count > 0 Then
            Dim val = (From cpcc In _context.CostProductionCenterCostCenter.AsNoTracking
                       Join cpc In _context.CostProductionCenter.AsNoTracking On cpcc.ProductionCenterId Equals cpc.Id
                       Join cc In _context.CostCenter.AsNoTracking On cpcc.CostCenterId Equals cc.Id
                       Where ListIds.Contains(cc.Id) AndAlso cpc.Id <> IdCurrent Select New With {Key .CodeCPC = cpc.Code, Key .NameCPC = cpc.Name, Key .CodeCC = cc.Code, Key .NameCC = cc.Name}).Distinct().ToList()
            val.ForEach(Sub(x) ListReturn.AppendLine("El centro de costo " + x.CodeCC + " - " + x.NameCC + " ya existe en un centro de producción " + x.CodeCPC + " - " + x.NameCPC))
        End If
        Return ListReturn.ToString
    End Function

End Class
