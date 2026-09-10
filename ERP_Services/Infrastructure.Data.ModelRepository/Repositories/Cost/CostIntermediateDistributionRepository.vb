'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 25-06-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Base

Public Class CostIntermediateDistributionRepository
    Inherits GenericRepository(Of CostIntermediateDistribution)
    Implements ICostIntermediateDistributionRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetIntermediateDistribution(code As String) As CostIntermediateDistribution Implements ICostIntermediateDistributionRepository.GetIntermediateDistribution
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From g In _context.CostIntermediateDistribution.Include("CostIntermediateDistributionBase.CostIntermediateDistributionBaseDetail") Where g.Code.Equals(code) Select g).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.FullNameProductionCenter = (From p In _context.CostProductionCenter Where p.Id = query.ProductionCenterId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()
            For Each item In query.CostIntermediateDistributionBase
                Select Case item.MultipleBase
                    Case 1
                        item.DistributionBaseName = "Distribución (A)"
                    Case 2
                        item.DistributionBaseName = "Distribución (B)"
                    Case 3
                        item.DistributionBaseName = "Distribución (C)"
                    Case 4
                        item.DistributionBaseName = "Distribución (D)"
                End Select
                Select Case item.DistributionType
                    Case 1
                        item.DistributionTypeName = ResourceManager.GetString("DistributionTypeDirect", "InteropCost")
                    Case 2
                        item.DistributionTypeName = ResourceManager.GetString("DistributionTypeCalculated", "InteropCost")
                    Case 3
                        item.DistributionTypeName = ResourceManager.GetString("DistributionTypeSearch", "InteropCost")
                End Select
                Select Case item.MeasurementUnit
                    Case 1
                        item.MeasureUnitName = ResourceManager.GetString("MeasureUnitProportion", "InteropCost")
                    Case 2
                        item.MeasureUnitName = ResourceManager.GetString("MeasureUnitValue", "InteropCost")
                End Select

                For Each itemMeasureUnit In item.CostIntermediateDistributionMeasurementUnit
                    itemMeasureUnit.CodeNameMeasureUnit = (From e In _context.InventoryMeasurementUnit.AsNoTracking() Where e.Id = itemMeasureUnit.MeasurementUnitId Select String.Concat(e.Code, " - ", e.Name)).FirstOrDefault()
                Next
            Next
            Return query
        Else
            Return New CostIntermediateDistribution()
        End If
    End Function

    Public Function SP_ImportDetailsToCostIntermediateDistributionBase(distributionType As Byte, measurementUnit As Byte, xmlListIntermediateDistributionBaseDetail As Object, xmlData As Object) As Object Implements ICostIntermediateDistributionRepository.SP_ImportDetailsToCostIntermediateDistributionBase
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportDetailsToCostIntermediateDistributionBase(distributionType, measurementUnit, xmlListIntermediateDistributionBaseDetail, xmlData).ToList()
    End Function

    Public Function SP_CopyAndPasteCostIntermediateDistribution(xmlObject As String, intermediateDistributionId As Integer) As List(Of SP_CopyAndPasteCostIntermediateDistribution_Result) Implements ICostIntermediateDistributionRepository.SP_CopyAndPasteCostIntermediateDistribution
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteCostIntermediateDistribution(xmlObject, intermediateDistributionId).ToList()
    End Function
End Class