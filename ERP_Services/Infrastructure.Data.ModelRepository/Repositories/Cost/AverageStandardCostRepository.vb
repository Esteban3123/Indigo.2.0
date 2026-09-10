Imports System.Data.Entity.Infrastructure
Imports System.Linq.Dynamic.Core
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class AverageStandardCostRepository
    Inherits GenericRepository(Of StandarCost)
    Implements IAverageStandardCostRepository

#Region "Builder"

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

    ''' <summary>
    ''' Obtiene al entidad por código
    ''' </summary>
    ''' <param name="standarCostCode"></param>
    ''' <returns></returns>
    Public Function GetAverageStandardCostByCode(standarCostCode As String) As StandarCost Implements IAverageStandardCostRepository.GetAverageStandardCostByCode
        Dim query = _context.StandarCost.Include("StandarCostDetails").Where(Function(w) w.Code = standarCostCode).FirstOrDefault()
        If query IsNot Nothing Then
            If query.StandarCostDetails.Any Then
                For Each item In query.StandarCostDetails
                    Dim activityEntity = _context.CostActivity.AsNoTracking().Where(Function(w) w.Id = item.CostActivityId)?.FirstOrDefault
                    If activityEntity IsNot Nothing Then
                        item.CodeNameActivity = $"{activityEntity.Code} - {activityEntity.Name}"
                    End If
                Next
            End If
        End If
        Return query
    End Function

    ''' <summary>
    ''' Obtiene al entidad por Id
    ''' </summary>
    ''' <param name="standarCostId"></param>
    ''' <returns></returns>
    Public Function GetAverageStandardCostById(standarCostId As Integer) As StandarCost Implements IAverageStandardCostRepository.GetAverageStandardCostById
        Return _context.StandarCost.Where(Function(w) w.Id = standarCostId)?.FirstOrDefault()
    End Function

    ''' <summary>
    ''' Consulta Costo Estándar por vigencia y Actividad
    ''' </summary>
    ''' <param name="validity"></param>
    ''' <param name="endDate"></param>
    ''' <param name="excludeId"></param>
    ''' <returns></returns>
    Public Function GetOverlappingStandarCosts(validity As Date, endDate As Date, excludeId As Integer) As List(Of StandarCost) Implements IAverageStandardCostRepository.GetOverlappingStandarCosts
        Dim results = _context.StandarCost.Include("StandarCostDetails") _
            .Where(Function(w) w.Id <> excludeId _
                AndAlso w.Validity <= endDate _
                AndAlso w.EndDate >= validity) _
            .ToList()

        For Each record In results
            If record.StandarCostDetails.Any Then
                For Each item In record.StandarCostDetails
                    Dim activityEntity = _context.CostActivity.AsNoTracking().Where(Function(w) w.Id = item.CostActivityId)?.FirstOrDefault
                    If activityEntity IsNot Nothing Then
                        item.CodeNameActivity = $"{activityEntity.Code} - {activityEntity.Name}"
                    End If
                Next
            End If
        Next

        Return results
    End Function

End Class
