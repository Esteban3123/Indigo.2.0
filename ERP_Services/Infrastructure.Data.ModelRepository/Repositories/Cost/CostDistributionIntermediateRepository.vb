'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Infrastructure

Public Class CostDistributionIntermediateRepository
    Inherits GenericRepository(Of CostDistributionIntermediate)
    Implements ICostDistributionIntermediateRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por codigo
    ''' </summary>
    Public Function GetDistributionIntermediate(code As String) As CostDistributionIntermediate Implements ICostDistributionIntermediateRepository.GetDistributionIntermediate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From d In _context.CostDistributionIntermediate.Include("CostDistributionIntermediateDetail") Where d.Code.Equals(code) Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.CostDistributionIntermediate.AsNoTracking() Where d.Code.Equals(code) Select d).FirstOrDefault()
            Return query
        Else
            Return New CostDistributionIntermediate()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por id
    ''' </summary>
    Public Function GetDistributionIntermediateById(id As Integer) As CostDistributionIntermediate Implements ICostDistributionIntermediateRepository.GetDistributionIntermediateById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From d In _context.CostDistributionIntermediate Where d.Id = id Select d).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.CostDistributionIntermediate.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
            Return query
        Else
            Return New CostDistributionIntermediate()
        End If
    End Function

    ''' <summary>
    ''' Lista las distribuciones Intermedia por año y mes
    ''' </summary>
    Public Function ListDistributionIntermediateByYearMonth(year As Integer, month As Integer) As List(Of CostDistributionIntermediate) Implements ICostDistributionIntermediateRepository.ListDistributionIntermediateByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim ListDistribIntermediate = (From d In _context.CostDistributionIntermediate.Include("DistributionIntermediateDetail") Where d.Year = year AndAlso d.Month = month Select d).ToList()
        For Each item As CostDistributionIntermediate In ListDistribIntermediate
            item.FullNameProductionCenter = (From t In _context.ProductionCenter Where t.Id = item.ProductionCenterId Select String.Concat(t.Code, " - ", t.Name)).FirstOrDefault()
        Next
        Return ListDistribIntermediate
    End Function

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Public Function ListPeriodWithDataByMaximumPeriodDistributionIntermediate(year As Integer, month As Integer) As List(Of String) Implements ICostDistributionIntermediateRepository.ListPeriodWithDataByMaximumPeriodDistributionIntermediate
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim listData As List(Of CostDistributionIntermediate) = (From d In _context.CostDistributionIntermediate Where d.Year <= year AndAlso d.Month <= month Select d).ToList()
        Return listData.Select(Function(x) String.Concat(x.Month, "/", x.Year)).ToList().Distinct().ToList()
    End Function

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por el id del activo fijo, año y mes
    ''' </summary>
    Public Function GetDistributionIntermediateByProductionCenterIdAndYearMonth(productionCenterId As Integer, year As Integer, month As Integer, Optional tracking As Boolean = True) As CostDistributionIntermediate Implements ICostDistributionIntermediateRepository.GetDistributionIntermediateByProductionCenterIdAndYearMonth
        If productionCenterId = 0 Then
            Throw New ArgumentNullException("productionCenterId")
        End If
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Dim query As CostDistributionIntermediate = Nothing
        If tracking Then
            query = (From d In _context.CostDistributionIntermediate.Include("CostDistributionIntermediateDetail") Where d.ProductionCenterId = productionCenterId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
        Else
            query = (From d In _context.CostDistributionIntermediate.AsNoTracking().Include("CostDistributionIntermediateDetail").AsNoTracking() Where d.ProductionCenterId = productionCenterId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From d In _context.CostDistributionIntermediate.AsNoTracking() Where d.ProductionCenterId = productionCenterId AndAlso d.Year = year AndAlso d.Month = month Select d).FirstOrDefault()
            Return query
        Else
            Return New CostDistributionIntermediate()
        End If
    End Function

    ''' <summary>
    ''' Calcula la distribución intermedia basándose en las bases de distribución configuradas
    ''' </summary>
    ''' <param name="costIntermediateDistributionId">Id del elemento de distribución intermedia</param>
    ''' <param name="year">Año del periodo</param>
    ''' <param name="month">Mes del periodo</param>
    ''' <returns>Lista de detalles de distribución calculados</returns>
    Public Function CalculateDistributionIntermediate(costIntermediateDistributionId As Integer, year As Integer, month As Integer) As List(Of SP_CalculateDistributionIntermediate_Result) Implements ICostDistributionIntermediateRepository.CalculateDistributionIntermediate
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CalculateDistributionIntermediate(costIntermediateDistributionId, year, month).ToList()
    End Function

    ''' <summary>
    ''' Guarda las cantidades de servicios agrupadas para distribución de tipo "Cantidades Procesadas"
    ''' </summary>
    Public Sub SaveServiceQuantitiesForDistribution(distributionIntermediateId As Integer, intermediateDistributionElementId As Integer, year As Integer, month As Integer, userCode As String) Implements ICostDistributionIntermediateRepository.SaveServiceQuantitiesForDistribution
        Try
            ' 1. Obtener las bases de distribución configuradas para el elemento de tipo "Cantidades Producidas"
            ' DistributionType = 3 (Buscada) y QuantitiesProduced = True
            Dim distributionBases = (From db In _context.CostIntermediateDistributionBase
                                     Where db.IntermediateDistributionId = intermediateDistributionElementId AndAlso
                                           db.DistributionType = 3 AndAlso
                                           db.QuantitiesProduced = True
                                     Select db).ToList()

            ' Si no hay bases de tipo "Buscada/Cantidades Procesadas", salir
            If distributionBases Is Nothing OrElse distributionBases.Count = 0 Then
                Return
            End If

            ' 2. Obtener los tipos de servicio configurados en las bases de distribución (desde la Base, no desde BaseDetail)
            Dim serviceTypes = (From db In distributionBases
                                Where db.ServiceType > 0
                                Select db.ServiceType).Distinct().ToList()

            If serviceTypes.Count = 0 Then
                Return
            End If

            ' 3. Configurar timeout para consultas pesadas
            DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600

            ' 4. ELIMINAR registros antiguos si existen (evitar duplicados al re-guardar)
            Dim existingRecords = (From sq In _context.CostIntermediateDistributionServiceQuantity
                                   Where sq.DistributionIntermediateId = distributionIntermediateId
                                   Select sq).ToList()

            If existingRecords IsNot Nothing AndAlso existingRecords.Count > 0 Then
                System.Diagnostics.Debug.WriteLine($"[DEBUG SaveServiceQuantities] Eliminando {existingRecords.Count} registros antiguos para DistributionIntermediateId={distributionIntermediateId}")
                For Each oldRecord In existingRecords
                    _context.CostIntermediateDistributionServiceQuantity.Remove(oldRecord)
                Next
            End If

            ' 5. Calcular fechas del mes
            Dim firstDayOfMonth As DateTime = New DateTime(year, month, 1)
            Dim lastDayOfMonth As DateTime = firstDayOfMonth.AddMonths(1).AddDays(-1)

            ' Constantes para tipos de servicio
            Const SERVICE_TYPE_PHARMACEUTICAL As Byte = 10
            Const SERVICE_ORDER_STATUS_CANCELLED As Integer = 3

            ' 6. Agregar datos de órdenes de servicio según especificaciones del PBI
            ' Primero consultar con tipo anónimo y materializar
            Dim queryResults = (From sod In _context.ServiceOrderDetail
                                Where sod.ServiceDate >= firstDayOfMonth AndAlso
                                       sod.ServiceDate <= lastDayOfMonth AndAlso
                                       sod.IsDelete = False AndAlso
                                       (sod.CUPSEntityId.HasValue OrElse sod.ProductId.HasValue)
                                Join so In _context.ServiceOrder On sod.ServiceOrderId Equals so.Id
                                Where so.Status <> SERVICE_ORDER_STATUS_CANCELLED
                                Join fu In _context.FunctionalUnit On sod.PerformsFunctionalUnitId Equals fu.Id
                                Where fu.ProductionCenterId.HasValue
                                Group Join ce In _context.CUPSEntity On sod.CUPSEntityId Equals ce.Id Into cupsGroup = Group
                                From ce In cupsGroup.DefaultIfEmpty()
                                Let serviceType = If(sod.CUPSEntityId.HasValue, ce.ServiceType, SERVICE_TYPE_PHARMACEUTICAL)
                                Where serviceTypes.Contains(serviceType)
                                Let serviceId = If(sod.CUPSEntityId.HasValue, sod.CUPSEntityId.Value, sod.ProductId.Value)
                                Group New With {sod.InvoicedQuantity, sod.GrandTotalSalesPrice} By Key = New With {
                                     Key .ServiceType = serviceType,
                                     Key .ServiceId = serviceId,
                                     Key .FunctionalUnitId = sod.PerformsFunctionalUnitId,
                                     Key .ProductionCenterId = fu.ProductionCenterId.Value
                                 } Into grouped = Group
                                Let totalQuantity = grouped.Sum(Function(x) x.InvoicedQuantity)
                                Let totalSalesValue = grouped.Sum(Function(x) x.GrandTotalSalesPrice)
                                Where totalQuantity > 0
                                Select New With {
                                     .ServiceType = Key.ServiceType,
                                     .ServiceId = Key.ServiceId,
                                     .FunctionalUnitId = Key.FunctionalUnitId,
                                     .ProductionCenterId = Key.ProductionCenterId,
                                     .Quantity = totalQuantity,
                                     .SalesValue = totalSalesValue
                                 }).ToList()

            ' Ahora crear las entidades en memoria
            Dim aggregatedData As New List(Of CostIntermediateDistributionServiceQuantity)()
            For Each item In queryResults
                Dim entity As New CostIntermediateDistributionServiceQuantity With {
                    .DistributionIntermediateId = distributionIntermediateId,
                    .CostMonth = (year * 100) + month,
                    .ServiceType = item.ServiceType,
                    .ServiceId = item.ServiceId,
                    .FunctionalUnitId = item.FunctionalUnitId,
                    .ProductionCenterId = item.ProductionCenterId,
                    .Quantity = item.Quantity,
                    .SalesValue = item.SalesValue,
                    .CreationUser = userCode,
                    .CreationDate = DateTime.Now
                }
                aggregatedData.Add(entity)
            Next

            ' 7. Guardar en base de datos si hay registros
            If aggregatedData IsNot Nothing AndAlso aggregatedData.Count > 0 Then
                For Each item In aggregatedData
                    _context.CostIntermediateDistributionServiceQuantity.Add(item)
                Next
            End If

        Catch ex As Exception
            Throw New Exception($"Error al guardar cantidades de servicios: {ex.Message}", ex)
        End Try
    End Sub

End Class