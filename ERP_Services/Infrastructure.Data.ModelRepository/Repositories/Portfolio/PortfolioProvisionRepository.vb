'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 20/10/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Concurrent
Imports Infrastructure.CrossCutting.Base
Imports Domain.Portfolio.Model

Public Class PortfolioProvisionRepository
    Inherits GenericRepository(Of PortfolioProvision)
    Implements IPortfolioProvisionRepository

#Region "Context"

    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function SP_CopyAndPastePortfolioProvision(XmlObject As String, CourtDate As Date, Process As Integer, OperatingUnitId As Integer, ApplyDeterioration As Integer) As List(Of SP_CopyAndPasteProvisionAndDeterioration_Result) Implements IPortfolioProvisionRepository.SP_CopyAndPastePortfolioProvision
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteProvisionAndDeterioration(XmlObject, CourtDate, Process, OperatingUnitId, ApplyDeterioration).ToList
    End Function

    Public Function GetPortfolioProvision(code As String) As PortfolioProvision Implements IPortfolioProvisionRepository.GetPortfolioProvision
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d In _context.PortfolioProvision Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d In _context.PortfolioProvision.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
            Return res
        Else
            Return New PortfolioProvision()
        End If
    End Function

    Public Function GetPortfolioProvisionById(Id As Integer) As PortfolioProvision Implements IPortfolioProvisionRepository.GetPortfolioProvisionById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d In _context.PortfolioProvision Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New PortfolioProvision()
        End If
    End Function

    Public Function SP_SaveProvisionAndDeterioration(XmlObject As String, DetailForDeleteXml As String, CodeUser As String) As SP_SaveProvisionAndDeterioration_Result Implements IPortfolioProvisionRepository.SP_SaveProvisionAndDeterioration
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveProvisionAndDeterioration(XmlObject, DetailForDeleteXml, CodeUser).SingleOrDefault
    End Function

    Public Function SP_ConfirmPortfolioProvision(PortfolioProvisionId As Integer, CodeUser As String, Optional operativeUnitId As Integer? = Nothing) As SP_ConfirmProvisionAndDeterioration_Result Implements IPortfolioProvisionRepository.SP_ConfirmPortfolioProvision
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmProvisionAndDeterioration(PortfolioProvisionId, CodeUser, operativeUnitId).SingleOrDefault
    End Function

    ''' <summary>
    ''' Obtiene y calcula la información del Deterioro de Cartera de acuerdo a la Clasificación
    ''' </summary>
    ''' <param name="closingDate"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <param name="pageNumber"></param>
    ''' <param name="pageSize"></param>
    ''' <returns></returns>
    Public Function SP_GetPortfolioDeteriorationByClassification(closingDate As Date, operativeUnitId As Integer, Optional pageNumber As Integer = 1, Optional pageSize As Integer = 50000) As List(Of PortfolioDeteriorationByClassificationDTO) Implements IPortfolioProvisionRepository.SP_GetPortfolioDeteriorationByClassification
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim params = New List(Of (String, Object)) From {
                ("@ClosingDate", closingDate),
                ("@OperativeUnitId", operativeUnitId),
                ("@PageNumber", pageNumber),
                ("@PageSize", pageSize)
            }
        Dim query = Me.ExecuteStoredProcedure(Of PortfolioDeteriorationByClassificationDTO)("[Portfolio].[SP_GetPortfolioDeteriorationByClassification]", params)
        Return If(query?.ToList(), New List(Of PortfolioDeteriorationByClassificationDTO)())
    End Function

    ''' <summary>
    ''' Ejecución del SP que valida y prepara la confirmación del deterioro por clasificación
    ''' </summary>
    ''' <param name="portfolioProvisionId"></param>
    ''' <param name="codeUser"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <returns></returns>
    Public Function SP_PrepareConfirmPortfolioDeteriorationByClassification(portfolioProvisionId As Integer, codeUser As String, operativeUnitId As Integer) As (PrepareResult As PrepareConfirmDeteriorationByClassificationDTO, ThirdPartyGroups As List(Of ThirdPartyGroupDTO)) Implements IPortfolioProvisionRepository.SP_PrepareConfirmPortfolioDeteriorationByClassification
        Dim conx = String.Format(ConfigurationManager.ConnectionStrings(ConfigurationFile.CONX_GENESIS).ConnectionString, "",
                                 TryCast(_context, DbContext).Database.Connection.Database)
        Using connection As New SqlConnection(conx)
            Dim command As New SqlCommand("[Portfolio].[SP_PrepareConfirmPortfolioDeteriorationByClassification]")
            command.Connection = connection
            command.CommandType = CommandType.StoredProcedure
            command.CommandTimeout = 3600
            command.Parameters.Add(New SqlParameter("@PortfolioProvisionId", portfolioProvisionId))
            command.Parameters.Add(New SqlParameter("@CodeUser", codeUser))
            command.Parameters.Add(New SqlParameter("@OperativeUnitId", operativeUnitId))

            connection.Open()
            Dim reader = command.ExecuteReader()

            Dim prepareResult As New PrepareConfirmDeteriorationByClassificationDTO()
            If reader.Read() Then
                prepareResult.CodeMessage = Convert.ToInt32(reader("CodeMessage"))
                prepareResult.Message = If(reader("Message") IsNot DBNull.Value, reader("Message").ToString(), Nothing)
                prepareResult.Consecutive = If(reader("Consecutive") IsNot DBNull.Value, reader("Consecutive").ToString(), Nothing)
            End If

            Dim groups As New List(Of ThirdPartyGroupDTO)()
            If prepareResult.CodeMessage = 0 AndAlso reader.NextResult() Then
                While reader.Read()
                    Dim group As New ThirdPartyGroupDTO()
                    group.ThirdPartyId = Convert.ToInt32(reader("ThirdPartyId"))
                    group.DetailCount = Convert.ToInt32(reader("DetailCount"))
                    groups.Add(group)
                End While
            End If

            Return (prepareResult, groups)
        End Using
    End Function

    ''' <summary>
    ''' Ejecuta la confirmación de comprobantes contables por terceros de forma secuencial
    ''' </summary>
    ''' <param name="portfolioProvisionId"></param>
    ''' <param name="codeUser"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <param name="thirdPartyGroups"></param>
    ''' <returns></returns>
    Public Function SP_ConfirmPortfolioDeteriorationByClassificationBatch(portfolioProvisionId As Integer, codeUser As String, operativeUnitId As Integer, thirdPartyGroups As List(Of ThirdPartyGroupDTO)) As List(Of ConfirmDeteriorationByClassificationDTO) Implements IPortfolioProvisionRepository.SP_ConfirmPortfolioDeteriorationByClassificationBatch
        Dim results As New List(Of ConfirmDeteriorationByClassificationDTO)()

        For Each group In thirdPartyGroups
            Dim params = New List(Of (String, Object)) From {
                ("@PortfolioProvisionId", portfolioProvisionId),
                ("@CodeUser", codeUser),
                ("@OperativeUnitId", operativeUnitId),
                ("@ThirdPartyId", group.ThirdPartyId)
            }
            Dim result = Me.ExecuteStoredProcedure(Of ConfirmDeteriorationByClassificationDTO)(
                "[Portfolio].[SP_ConfirmPortfolioDeteriorationByClassification]", params)
            Dim dto = If(result?.FirstOrDefault(), New ConfirmDeteriorationByClassificationDTO())
            results.Add(dto)
        Next

        Return results
    End Function

    ''' <summary>
    ''' Ejecuta la confirmación completa del deterioro por clasificación de forma atómica y paralela.
    ''' Fase 1 - Prepare: conexión exclusiva para validar y obtener los grupos de terceros.
    ''' Fase 2 - Ejecución paralela (máx. 4 lanes) con commit diferido:
    '''          ninguna conexión confirma hasta que TODOS los lanes terminan exitosamente.
    '''          Terceros con menos de 5000 detalles se agrupan en un único lane.
    ''' Sin TransactionScope ni MSDTC; sin límite de TransactionManager.
    ''' </summary>
    ''' <param name="portfolioProvisionId"></param>
    ''' <param name="codeUser"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <returns></returns>
    Public Function SP_ConfirmPortfolioDeteriorationByClassificationAtomic(portfolioProvisionId As Integer, codeUser As String, operativeUnitId As Integer) As (PrepareResult As PrepareConfirmDeteriorationByClassificationDTO, BatchResults As List(Of ConfirmDeteriorationByClassificationDTO)) Implements IPortfolioProvisionRepository.SP_ConfirmPortfolioDeteriorationByClassificationAtomic
        Const SMALL_THRESHOLD As Integer = 5000
        Const MAX_PARALLEL_LANES As Integer = 4

        Dim conx = String.Format(ConfigurationManager.ConnectionStrings(ConfigurationFile.CONX_GENESIS).ConnectionString, "",
                                 TryCast(_context, DbContext).Database.Connection.Database)
        Dim prepareResult As New PrepareConfirmDeteriorationByClassificationDTO()
        Dim batchResults As New List(Of ConfirmDeteriorationByClassificationDTO)()
        Dim groups As New List(Of ThirdPartyGroupDTO)()

        ' --- Fase 1: Prepare en conexión dedicada ---
        Using prepareConnection As New SqlConnection(conx)
            prepareConnection.Open()
            Dim prepareTx = prepareConnection.BeginTransaction()
            Try
                Using prepareCmd As New SqlCommand("[Portfolio].[SP_PrepareConfirmPortfolioDeteriorationByClassification]", prepareConnection, prepareTx)
                    prepareCmd.CommandType = CommandType.StoredProcedure
                    prepareCmd.CommandTimeout = 3600
                    prepareCmd.Parameters.Add(New SqlParameter("@PortfolioProvisionId", portfolioProvisionId))
                    prepareCmd.Parameters.Add(New SqlParameter("@CodeUser", codeUser))
                    prepareCmd.Parameters.Add(New SqlParameter("@OperativeUnitId", operativeUnitId))

                    Using reader = prepareCmd.ExecuteReader()
                        If reader.Read() Then
                            prepareResult.CodeMessage = Convert.ToInt32(reader("CodeMessage"))
                            prepareResult.Message = If(reader("Message") IsNot DBNull.Value, reader("Message").ToString(), Nothing)
                            prepareResult.Consecutive = If(reader("Consecutive") IsNot DBNull.Value, reader("Consecutive").ToString(), Nothing)
                        End If

                        If prepareResult.CodeMessage = 0 AndAlso reader.NextResult() Then
                            While reader.Read()
                                Dim group As New ThirdPartyGroupDTO()
                                group.ThirdPartyId = Convert.ToInt32(reader("ThirdPartyId"))
                                group.DetailCount = Convert.ToInt32(reader("DetailCount"))
                                groups.Add(group)
                            End While
                        End If
                    End Using
                End Using

                If prepareResult.CodeMessage <> 0 Then
                    prepareTx.Rollback()
                    Return (prepareResult, batchResults)
                End If

                prepareTx.Commit()
            Catch ex As Exception
                prepareTx.Rollback()
                Throw
            End Try
        End Using

        ' --- Fase 2: Distribución en lanes ---
        ' Todos los terceros con menos de SMALL_THRESHOLD detalles van en un único lane
        Dim smallGroups = groups.Where(Function(g) g.DetailCount < SMALL_THRESHOLD).ToList()
        Dim largeGroups = groups.Where(Function(g) g.DetailCount >= SMALL_THRESHOLD) _
                                .OrderByDescending(Function(g) g.DetailCount).ToList()

        Dim lanes As New List(Of List(Of ThirdPartyGroupDTO))()
        If smallGroups.Count > 0 Then lanes.Add(smallGroups)

        Dim remainingLanes As Integer = MAX_PARALLEL_LANES - lanes.Count
        For i As Integer = 0 To Math.Min(largeGroups.Count, remainingLanes) - 1
            lanes.Add(New List(Of ThirdPartyGroupDTO) From {largeGroups(i)})
        Next

        ' Cualquier grupo grande que exceda los lanes disponibles se añade al último lane
        If largeGroups.Count > remainingLanes Then
            For i As Integer = remainingLanes To largeGroups.Count - 1
                lanes(lanes.Count - 1).Add(largeGroups(i))
            Next
        End If

        ' --- Fase 3: Apertura de una conexión + transacción por lane ---
        Dim laneConnections As New List(Of (Conn As SqlConnection, Tx As SqlTransaction, LaneGroups As List(Of ThirdPartyGroupDTO)))()
        Try
            For Each lane In lanes
                Dim laneConn As New SqlConnection(conx)
                laneConn.Open()
                Dim laneTx = laneConn.BeginTransaction()
                laneConnections.Add((laneConn, laneTx, lane))
            Next

            ' --- Fase 4: Ejecución paralela SIN commit ---
            Dim allResults As New ConcurrentBag(Of ConfirmDeteriorationByClassificationDTO)()
            Dim parallelOpts As New ParallelOptions() With {.MaxDegreeOfParallelism = MAX_PARALLEL_LANES}

            Parallel.ForEach(laneConnections, parallelOpts,
                Sub(item)
                    For Each group In item.LaneGroups
                        Using cmd As New SqlCommand("[Portfolio].[SP_ConfirmPortfolioDeteriorationByClassification]",
                                                    item.Conn, item.Tx)
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandTimeout = 3600
                            cmd.Parameters.Add(New SqlParameter("@PortfolioProvisionId", portfolioProvisionId))
                            cmd.Parameters.Add(New SqlParameter("@CodeUser", codeUser))
                            cmd.Parameters.Add(New SqlParameter("@OperativeUnitId", operativeUnitId))
                            cmd.Parameters.Add(New SqlParameter("@ThirdPartyId", group.ThirdPartyId))

                            Dim dto As New ConfirmDeteriorationByClassificationDTO()
                            Using reader = cmd.ExecuteReader()
                                If reader.Read() Then
                                    dto.CodeMessage = If(reader("CodeMessage") IsNot DBNull.Value, Convert.ToInt32(reader("CodeMessage")), 0)
                                    dto.Message = If(reader("Message") IsNot DBNull.Value, reader("Message").ToString(), Nothing)
                                    dto.Consecutive = If(reader("Consecutive") IsNot DBNull.Value, reader("Consecutive").ToString(), Nothing)
                                End If
                            End Using
                            allResults.Add(dto)
                        End Using
                    Next
                End Sub)

            ' --- Fase 5: Decisión unificada - commit o rollback en TODOS los lanes ---
            Dim hasErrors As Boolean = allResults.Any(Function(r) r.CodeMessage = 999)
            For Each item In laneConnections
                If hasErrors Then
                    item.Tx.Rollback()
                Else
                    item.Tx.Commit()
                End If
            Next

            batchResults.AddRange(allResults)
        Finally
            For Each item In laneConnections
                item.Conn.Dispose()
            Next
        End Try

        Return (prepareResult, batchResults)
    End Function

#End Region

End Class
