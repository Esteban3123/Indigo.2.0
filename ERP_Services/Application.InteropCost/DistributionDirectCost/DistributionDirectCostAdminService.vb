'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports Domain.InteropCost
Imports Domain.InteropCost.Entities
Imports System.Transactions

Imports System.Data.SqlClient

Public Class DistributionDirectCostAdminService
    Implements IDistributionDirectCostAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de gastos generales
    ''' </summary>
    Private _distributionDirectCostRepository As IDistributionDirectCostRepository

    ''' <summary>
    ''' repositorio de secuencias numericas
    ''' </summary>
    Private _sequenceDRepository As IInteropCostSequenceDetailRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal distributionDirectCostRepository As IDistributionDirectCostRepository, ByVal sequenceDRepository As IInteropCostSequenceDetailRepository)
        If distributionDirectCostRepository Is Nothing Then
            Throw New ArgumentNullException("distributionDirectCostRepository")
        End If
        If sequenceDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenceDRepository")
        End If
        _distributionDirectCostRepository = distributionDirectCostRepository
        _sequenceDRepository = sequenceDRepository
    End Sub

    ''' <summary>
    ''' Elimina un gasto directo
    ''' </summary>
    Public Function DeleteDistributionDirectCost(distributionDirectCost As DistributionDirectCost, audit As AuditMessage) As ActionResult Implements IDistributionDirectCostAdminService.DeleteDistributionDirectCost
        If distributionDirectCost Is Nothing Then
            Throw New ArgumentNullException("distributionDirectCost")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionDirectCostRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                While distributionDirectCost.DistributionDirectCostDetail.Count > 0
                    distributionDirectCost.DistributionDirectCostDetail.Item(distributionDirectCost.DistributionDirectCostDetail.Count() - 1).MarkAsDeleted()
                End While
                distributionDirectCost.MarkAsDeleted()
                distributionDirectCost.ModificationUser = audit.CodeUser
                distributionDirectCost.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DistributionDirectCost)(distributionDirectCost, audit, status)
                Me._distributionDirectCostRepository.SaveEntity(distributionDirectCost)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un gasto directo por codigo
    ''' </summary>
    Public Function GetDistributionDirectCost(code As String, ByVal audit As AuditMessage) As ActionResult(Of DistributionDirectCost) Implements IDistributionDirectCostAdminService.GetDistributionDirectCost
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim distributionDirectCost As DistributionDirectCost = Me._distributionDirectCostRepository.GetDistributionDirectCost(code.Trim())
            If distributionDirectCost IsNot Nothing AndAlso distributionDirectCost.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DistributionDirectCost)(distributionDirectCost, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of DistributionDirectCost) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionDirectCost}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionDirectCost) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un gasto directo por id
    ''' </summary>
    Public Function GetDistributionDirectCostById(id As Integer) As DistributionDirectCost Implements IDistributionDirectCostAdminService.GetDistributionDirectCostById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Try
            Return Me._distributionDirectCostRepository.GetDistributionDirectCostById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un gasto directo
    ''' </summary>
    Public Function SaveDistributionDirectCost(distributionDirectCost As DistributionDirectCost, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of DistributionDirectCost) Implements IDistributionDirectCostAdminService.SaveDistributionDirectCost
        If distributionDirectCost Is Nothing Then
            Throw New ArgumentNullException("distributionDirectCost")
        End If
        Dim unitOfWork As IUnitWork = Me._distributionDirectCostRepository.UnitWork
        Dim sequenceUnitOfWork As IUnitWork = Me._sequenceDRepository.UnitWork
        Try

            If distributionDirectCost.Value <> Math.Round((From x In distributionDirectCost.DistributionDirectCostDetail Where x.ChangeTracker.State <> ObjectState.Deleted Select x.Value).Sum(), 0) And distributionDirectCost.Status <> 3 Then
                Return New ActionResult(Of DistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "El valor a distribuir es distinto al valor total"}
            End If
            If distributionDirectCost.DistributionDirectCostDetail Is Nothing OrElse Not distributionDirectCost.DistributionDirectCostDetail.Any() Then
                Return New ActionResult(Of DistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "Debe agregar al menos un centro de producción"}
            End If
            If distributionDirectCost.DistributionDirectCostDetail.Any(Function(x) x.Value <= 0) Then
                Return New ActionResult(Of DistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = "No debe haber detalles con valor cero o negativos"}
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                If String.IsNullOrEmpty(distributionDirectCost.Code) Then
                    Dim seq As InteropCostSecuenceDetail = _sequenceDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.InteropCostSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            distributionDirectCost.Code = res
                            seq.Next += 1
                            Me._sequenceDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of DistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                        End If
                        MessageResult = If(seq.InteropCostSecuence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), distributionDirectCost.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of DistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("SequenceNotFound")}
                    End If
                End If

                Dim auxExpenseConcept As DistributionDirectCost = Nothing
                Dim status As Integer
                If distributionDirectCost.ChangeTracker.State = ObjectState.Added Then
                    If String.IsNullOrEmpty(MessageResult) Then
                        MessageResult = String.Format(ResourceManager.GetString("SavedWithCode"), distributionDirectCost.Code)
                    End If
                    distributionDirectCost.CreationDate = Date.Now
                    distributionDirectCost.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    distributionDirectCost.ModificationDate = Date.Now
                    distributionDirectCost.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxExpenseConcept = distributionDirectCost.OriginalValue
                End If
                If distributionDirectCost.Status = 2 Then
                    distributionDirectCost.ConfirmDate = Date.Now
                    distributionDirectCost.ConfirmUser = audit.CodeUser
                ElseIf distributionDirectCost.Status = 3 Then
                    distributionDirectCost.AnnulmentDate = Date.Now
                    distributionDirectCost.AnnulmentUser = audit.CodeUser
                End If
                For Each detail In distributionDirectCost.DistributionDirectCostDetail
                    Dim idTmp = detail.MeasurementUnitId
                    detail.InventoryMeasurementUnit = Nothing
                    detail.MeasurementUnitId = idTmp
                Next
                Me._distributionDirectCostRepository.SaveEntity(distributionDirectCost)
                unitOfWork.CommitAndRefreshChanges()
                'unitOfWork.Detach(distributionDirectCost)
                Dim auditProcess As New IndigoAuditSimpleEntity(Of DistributionDirectCost)(distributionDirectCost, audit, status, auxExpenseConcept)
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult(Of DistributionDirectCost) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = distributionDirectCost, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChangesUnitOfWork()
            Return New ActionResult(Of DistributionDirectCost) With {.StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionDirectCost) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Updates the state distribution direct cost.
    ''' </summary>
    Public Function UpdateStateDistributionDirectCost(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionDirectCost) Implements IDistributionDirectCostAdminService.UpdateStateDistributionDirectCost
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim distributionDirectCost As DistributionDirectCost = Me._distributionDirectCostRepository.GetDistributionDirectCost(code.Trim())
            If distributionDirectCost IsNot Nothing AndAlso distributionDirectCost.Id > 0 Then
                distributionDirectCost.Status = state
            End If
            Return Me.SaveDistributionDirectCost(distributionDirectCost, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of DistributionDirectCost) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Gets the main account value by container number account year and motn.
    ''' </summary>
    Public Function GetMainAccountValueByContainerNumberAccountYearAndMotn(container As String, numberaccount As String, year As String, month As Integer) As ActionResult(Of SP_MainAccountValue_Result) Implements IDistributionDirectCostAdminService.GetMainAccountValueByContainerNumberAccountYearAndMotn
        If String.IsNullOrEmpty(container) Then
            Throw New ArgumentNullException("container")
        End If
        If String.IsNullOrEmpty(numberaccount) Then
            Throw New ArgumentNullException("numberaccount")
        End If
        If String.IsNullOrEmpty(year) Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("Month")
        End If

        Try
            Dim mainAccountValue As SP_MainAccountValue_Result = Me._distributionDirectCostRepository.GetMainAccountValueByContainerNumberAccountYearAndMotn(container, numberaccount, year, month)

            Return New ActionResult(Of SP_MainAccountValue_Result) With {.StateResult = True, .ObjectEmbbeded = mainAccountValue}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SP_MainAccountValue_Result) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Lista los gastos directos por año y mes
    ''' </summary>
    Public Function ListDistributionDirectCostByYearMonth(year As Integer, month As Integer) As List(Of DistributionDirectCost) Implements IDistributionDirectCostAdminService.ListDistributionDirectCostByYearMonth
        If year = 0 Then
            Throw New ArgumentNullException("year")
        End If
        If month = 0 Then
            Throw New ArgumentNullException("month")
        End If
        Try
            Return Me._distributionDirectCostRepository.ListDistributionDirectCostByYearMonth(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Calcula la distribución del valor entre los distintos centros de producción
    ''' </summary>
    ''' <param name="generalExpense">Elemento del gasto</param>
    ''' <param name="value">Valor a distribuir</param>
    ''' <returns>Lista de detalles de distribución de costos</returns>
    Public Function CalculateDistribution(GeneralExpenseId As Integer, value As Decimal, ByVal year As Int32, ByVal month As Int32, ByVal containerName As String) As ActionResult(Of List(Of DistributionDirectCostDetail)) Implements IDistributionDirectCostAdminService.CalculateDistribution
        Try
            Dim list As New List(Of DistributionDirectCostDetail)()
            Dim dicProductionCenters As New Dictionary(Of Int32, Tuple(Of Decimal, Int32))()

            'Se obtiene el elemento del costo por id
            Dim generalExpense As GeneralExpense = _distributionDirectCostRepository.GetGeneralExpenseById(GeneralExpenseId)

            For Each db As DistributionBase In generalExpense.DistributionBase.OrderBy(Function(o) o.MultipleBase).ToList()
                Select Case db.DistributionType
                    Case 2 'Calculada
                        Dim totalPercentage = 0.0
                        Dim totalQuantity As Integer = (From x In db.DistributionBaseDetail Select x.Quantity).Sum()
                        For Each dt As DistributionBaseDetail In db.DistributionBaseDetail
                            If dicProductionCenters.ContainsKey(dt.ProductionCenterId) Then
                                Dim quantityTmp As Decimal = dt.Quantity
                                If db.MeasurementUnit = 2 Then
                                    quantityTmp = Math.Round((dt.Quantity / totalQuantity) * 100, 2)
                                    totalPercentage += quantityTmp
                                End If
                                dicProductionCenters(dt.ProductionCenterId) = New Tuple(Of Decimal, Integer)(dicProductionCenters(dt.ProductionCenterId).Item1 + quantityTmp, dicProductionCenters(dt.ProductionCenterId).Item2 + 1)
                                Dim obj As DistributionDirectCostDetail = list.Where(Function(o) o.ProductionCenterId = dt.ProductionCenterId).FirstOrDefault()
                                If obj IsNot Nothing Then
                                    obj.Percentage = (dicProductionCenters(dt.ProductionCenterId).Item1 / dicProductionCenters(dt.ProductionCenterId).Item2)
                                    obj.Value = Math.Round(((value * obj.Percentage) / 100), 2)
                                    obj.ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                                End If
                            Else
                                Dim quantityTmp As Decimal = dt.Quantity
                                If db.MeasurementUnit = 2 Then
                                    quantityTmp = Math.Round((dt.Quantity / totalQuantity) * 100.0, 2)
                                    totalPercentage += quantityTmp
                                End If
                                dicProductionCenters.Add(dt.ProductionCenterId, New Tuple(Of Decimal, Integer)(quantityTmp, 1))
                                list.Add(New DistributionDirectCostDetail())
                                list(list.Count - 1).ProductionCenterId = dt.ProductionCenterId
                                list(list.Count - 1).Percentage = quantityTmp
                                list(list.Count - 1).Value = Math.Round(((value * quantityTmp) / 100), 2)
                                list(list.Count - 1).ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                            End If
                        Next
                        If db.MeasurementUnit = 2 Then
                            If totalPercentage > 100 Then
                                Dim percentageTmp = list(list.Count - 1).Percentage
                                list(list.Count - 1).Percentage = Math.Round(percentageTmp - (totalPercentage - 100.0), 2)
                                list(list.Count - 1).Value = Math.Round(((value * list(list.Count - 1).Percentage) / 100), 2)
                            ElseIf totalPercentage < 100 Then
                                Dim percentageTmp = list(list.Count - 1).Percentage
                                list(list.Count - 1).Percentage = Math.Round(percentageTmp + (100.0 - totalPercentage), 2)
                                list(list.Count - 1).Value = Math.Round(((value * list(list.Count - 1).Percentage) / 100), 2)
                            End If
                            Dim sumTotalValue = list.Sum(Function(d) d.Value)
                            If sumTotalValue < value Then
                                list(list.Count - 1).Value += value - sumTotalValue
                            ElseIf sumTotalValue > value Then
                                list(list.Count - 1).Value -= sumTotalValue - value
                            End If
                        End If


                        Dim sumTotalValueValidation = list.Sum(Function(d) d.Value)
                        Dim sumTotalPorcentajeValidation = list.Sum(Function(d) d.Percentage)
                        'valido si el total de los porcentajes es mayor o menor a 100%, de ser asi, vuelvo y obtengo los porcentajes 
                        If sumTotalPorcentajeValidation > 100 Then
                            Dim percentageTmp = list(list.Count - 1).Percentage
                            list(list.Count - 1).Percentage = Math.Round(percentageTmp - (sumTotalPorcentajeValidation - 100.0), 2)
                            list(list.Count - 1).Value = Math.Round(((value * list(list.Count - 1).Percentage) / 100), 2)
                        ElseIf sumTotalPorcentajeValidation < 100 Then
                            Dim percentageTmp = list(list.Count - 1).Percentage
                            list(list.Count - 1).Percentage = Math.Round(percentageTmp + (100.0 - sumTotalPorcentajeValidation), 2)
                            list(list.Count - 1).Value = Math.Round(((value * list(list.Count - 1).Percentage) / 100), 2)
                        End If
                        'valido que total de los valores no sea menor o mayor al valor ingresado, de ser asi aproximo al valor ingresado
                        If value > sumTotalValueValidation Then
                            list(list.Count - 1).Value -= sumTotalValueValidation - value
                        ElseIf value < sumTotalValueValidation Then
                            list(list.Count - 1).Value += value - sumTotalValueValidation
                        End If

                    Case 3 'Buscada
                        'Por Área
                        If db.Area Then
                            Dim totalMts As Decimal = db.DistributionBaseDetail.Sum(Function(o) o.ProductionCenter.Area)
                            For Each dt As DistributionBaseDetail In db.DistributionBaseDetail
                                Dim currentPercentage As Decimal = ((dt.ProductionCenter.Area * 100) / totalMts)
                                If dicProductionCenters.ContainsKey(dt.ProductionCenterId) Then
                                    dicProductionCenters(dt.ProductionCenterId) = New Tuple(Of Decimal, Integer)(dicProductionCenters(dt.ProductionCenterId).Item1 + currentPercentage, dicProductionCenters(dt.ProductionCenterId).Item2 + 1)
                                    Dim obj As DistributionDirectCostDetail = list.Where(Function(o) o.ProductionCenterId = dt.ProductionCenterId).FirstOrDefault()
                                    If obj IsNot Nothing Then
                                        obj.Percentage = (dicProductionCenters(dt.ProductionCenterId).Item1 / dicProductionCenters(dt.ProductionCenterId).Item2)
                                        obj.Value = Math.Round(((value * obj.Percentage) / 100), 2)
                                        obj.ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                                    End If
                                Else
                                    dicProductionCenters.Add(dt.ProductionCenterId, New Tuple(Of Decimal, Integer)(currentPercentage, 1))
                                    list.Add(New DistributionDirectCostDetail())
                                    list(list.Count - 1).ProductionCenterId = dt.ProductionCenterId
                                    list(list.Count - 1).Percentage = currentPercentage
                                    list(list.Count - 1).Value = Math.Round(((value * currentPercentage) / 100), 2)
                                    list(list.Count - 1).ByArea = totalMts
                                    list(list.Count - 1).ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                                End If
                            Next
                        End If
                        'Por horas trabajadas
                        If db.OfficialHours Then
                            Dim listPCenter = Me._distributionDirectCostRepository.CalculatePercentageByProductionCenter(containerName, db.Id, 1, year, month)
                            For Each dt As DistributionBaseDetail In db.DistributionBaseDetail
                                Dim pCenter = listPCenter.Where(Function(o) o.ProductionCenterId = dt.ProductionCenterId).FirstOrDefault()
                                If pCenter IsNot Nothing AndAlso pCenter.Value.HasValue AndAlso pCenter.NetoValue.HasValue Then
                                    Dim currentPercentage As Decimal = pCenter.Value
                                    If dicProductionCenters.ContainsKey(dt.ProductionCenterId) Then
                                        dicProductionCenters(dt.ProductionCenterId) = New Tuple(Of Decimal, Integer)(dicProductionCenters(dt.ProductionCenterId).Item1 + currentPercentage, dicProductionCenters(dt.ProductionCenterId).Item2 + 1)
                                        Dim obj As DistributionDirectCostDetail = list.Where(Function(o) o.ProductionCenterId = dt.ProductionCenterId).FirstOrDefault()
                                        If obj IsNot Nothing Then
                                            obj.Percentage = (dicProductionCenters(dt.ProductionCenterId).Item1 / dicProductionCenters(dt.ProductionCenterId).Item2)
                                            obj.Value = Math.Round(((value * obj.Percentage) / 100), 2)
                                            obj.ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                                        End If
                                    Else
                                        dicProductionCenters.Add(dt.ProductionCenterId, New Tuple(Of Decimal, Integer)(currentPercentage, 1))
                                        list.Add(New DistributionDirectCostDetail())
                                        list(list.Count - 1).ProductionCenterId = dt.ProductionCenterId
                                        list(list.Count - 1).Percentage = currentPercentage
                                        list(list.Count - 1).Value = Math.Round(((value * currentPercentage) / 100), 2)
                                        list(list.Count - 1).ByOfficialHours = pCenter.NetoValue.Value
                                        list(list.Count - 1).ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                                    End If
                                End If
                            Next
                        End If
                        'Por suministros
                        If db.SupplyValue Then
                            Dim listPCenter = Me._distributionDirectCostRepository.CalculatePercentageByProductionCenter(containerName, db.Id, 2, year, month)
                            For Each dt As DistributionBaseDetail In db.DistributionBaseDetail
                                Dim pCenter = listPCenter.Where(Function(o) o.ProductionCenterId = dt.ProductionCenterId).FirstOrDefault()
                                If pCenter IsNot Nothing AndAlso pCenter.Value.HasValue AndAlso pCenter.NetoValue.HasValue Then
                                    Dim currentPercentage As Decimal = pCenter.Value
                                    If dicProductionCenters.ContainsKey(dt.ProductionCenterId) Then
                                        dicProductionCenters(dt.ProductionCenterId) = New Tuple(Of Decimal, Integer)(dicProductionCenters(dt.ProductionCenterId).Item1 + currentPercentage, dicProductionCenters(dt.ProductionCenterId).Item2 + 1)
                                        Dim obj As DistributionDirectCostDetail = list.Where(Function(o) o.ProductionCenterId = dt.ProductionCenterId).FirstOrDefault()
                                        If obj IsNot Nothing Then
                                            obj.Percentage = (dicProductionCenters(dt.ProductionCenterId).Item1 / dicProductionCenters(dt.ProductionCenterId).Item2)
                                            obj.Value = Math.Round(((value * obj.Percentage) / 100), 2)
                                            obj.ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                                        End If
                                    Else
                                        dicProductionCenters.Add(dt.ProductionCenterId, New Tuple(Of Decimal, Integer)(currentPercentage, 1))
                                        list.Add(New DistributionDirectCostDetail())
                                        list(list.Count - 1).ProductionCenterId = dt.ProductionCenterId
                                        list(list.Count - 1).Percentage = currentPercentage
                                        list(list.Count - 1).Value = Math.Round(((value * currentPercentage) / 100), 2)
                                        list(list.Count - 1).BySupplyValue = pCenter.NetoValue.Value
                                        list(list.Count - 1).ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                                    End If
                                End If
                            Next
                        End If
                        'Por mano de obra
                        If db.WorkmanshipValue Then
                            Dim listPCenter = Me._distributionDirectCostRepository.CalculatePercentageByProductionCenter(containerName, db.Id, 3, year, month)
                            For Each dt As DistributionBaseDetail In db.DistributionBaseDetail
                                Dim pCenter = listPCenter.Where(Function(o) o.ProductionCenterId = dt.ProductionCenterId).FirstOrDefault()
                                If pCenter IsNot Nothing AndAlso pCenter.Value.HasValue AndAlso pCenter.NetoValue.HasValue Then
                                    Dim currentPercentage As Decimal = pCenter.Value
                                    If dicProductionCenters.ContainsKey(dt.ProductionCenterId) Then
                                        dicProductionCenters(dt.ProductionCenterId) = New Tuple(Of Decimal, Integer)(dicProductionCenters(dt.ProductionCenterId).Item1 + currentPercentage, dicProductionCenters(dt.ProductionCenterId).Item2 + 1)
                                        Dim obj As DistributionDirectCostDetail = list.Where(Function(o) o.ProductionCenterId = dt.ProductionCenterId).FirstOrDefault()
                                        If obj IsNot Nothing Then
                                            obj.Percentage = (dicProductionCenters(dt.ProductionCenterId).Item1 / dicProductionCenters(dt.ProductionCenterId).Item2)
                                            obj.Value = Math.Round(((value * obj.Percentage) / 100), 2)
                                            obj.ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                                        End If
                                    Else
                                        dicProductionCenters.Add(dt.ProductionCenterId, New Tuple(Of Decimal, Integer)(currentPercentage, 1))
                                        list.Add(New DistributionDirectCostDetail())
                                        list(list.Count - 1).ProductionCenterId = dt.ProductionCenterId
                                        list(list.Count - 1).Percentage = currentPercentage
                                        list(list.Count - 1).Value = Math.Round(((value * currentPercentage) / 100), 2)
                                        list(list.Count - 1).ByWorkmanship = pCenter.NetoValue.Value
                                        list(list.Count - 1).ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                                    End If
                                End If
                            Next
                        End If
                        'Por valor del activo
                        If db.AssetValue Then
                            Dim listPCenter = Me._distributionDirectCostRepository.CalculatePercentageByProductionCenter(containerName, db.Id, 4, year, month)
                            For Each dt As DistributionBaseDetail In db.DistributionBaseDetail
                                Dim pCenter = listPCenter.Where(Function(o) o.ProductionCenterId = dt.ProductionCenterId).FirstOrDefault()
                                If pCenter IsNot Nothing AndAlso pCenter.Value.HasValue AndAlso pCenter.NetoValue.HasValue Then
                                    Dim currentPercentage As Decimal = pCenter.Value
                                    If dicProductionCenters.ContainsKey(dt.ProductionCenterId) Then
                                        dicProductionCenters(dt.ProductionCenterId) = New Tuple(Of Decimal, Integer)(dicProductionCenters(dt.ProductionCenterId).Item1 + currentPercentage, dicProductionCenters(dt.ProductionCenterId).Item2 + 1)
                                        Dim obj As DistributionDirectCostDetail = list.Where(Function(o) o.ProductionCenterId = dt.ProductionCenterId).FirstOrDefault()
                                        If obj IsNot Nothing Then
                                            obj.Percentage = (dicProductionCenters(dt.ProductionCenterId).Item1 / dicProductionCenters(dt.ProductionCenterId).Item2)
                                            obj.Value = Math.Round(((value * obj.Percentage) / 100), 2)
                                            obj.ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                                        End If
                                    Else
                                        dicProductionCenters.Add(dt.ProductionCenterId, New Tuple(Of Decimal, Integer)(currentPercentage, 1))
                                        list.Add(New DistributionDirectCostDetail())
                                        list(list.Count - 1).ProductionCenterId = dt.ProductionCenterId
                                        list(list.Count - 1).Percentage = currentPercentage
                                        list(list.Count - 1).Value = Math.Round(((value * currentPercentage) / 100), 2)
                                        list(list.Count - 1).ByAssetValue = pCenter.NetoValue.Value
                                        list(list.Count - 1).ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                                    End If
                                End If
                            Next
                        End If
                        'Por valor de venta
                        If db.Sales Then
                            Dim listPCenter = Me._distributionDirectCostRepository.CalculatePercentageByProductionCenter(containerName, db.Id, 5, year, month)
                            For Each dt As DistributionBaseDetail In db.DistributionBaseDetail
                                Dim pCenter = listPCenter.Where(Function(o) o.ProductionCenterId = dt.ProductionCenterId).FirstOrDefault()
                                If pCenter IsNot Nothing AndAlso pCenter.Value.HasValue AndAlso pCenter.NetoValue.HasValue Then
                                    Dim currentPercentage As Decimal = pCenter.Value
                                    If dicProductionCenters.ContainsKey(dt.ProductionCenterId) Then
                                        dicProductionCenters(dt.ProductionCenterId) = New Tuple(Of Decimal, Integer)(dicProductionCenters(dt.ProductionCenterId).Item1 + currentPercentage, dicProductionCenters(dt.ProductionCenterId).Item2 + 1)
                                        Dim obj As DistributionDirectCostDetail = list.Where(Function(o) o.ProductionCenterId = dt.ProductionCenterId).FirstOrDefault()
                                        If obj IsNot Nothing Then
                                            obj.Percentage = (dicProductionCenters(dt.ProductionCenterId).Item1 / dicProductionCenters(dt.ProductionCenterId).Item2)
                                            obj.Value = Math.Round(((value * obj.Percentage) / 100), 2)
                                            obj.ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                                        End If
                                    Else
                                        dicProductionCenters.Add(dt.ProductionCenterId, New Tuple(Of Decimal, Integer)(currentPercentage, 1))
                                        list.Add(New DistributionDirectCostDetail())
                                        list(list.Count - 1).ProductionCenterId = dt.ProductionCenterId
                                        list(list.Count - 1).Percentage = currentPercentage
                                        list(list.Count - 1).Value = Math.Round(((value * currentPercentage) / 100), 2)
                                        list(list.Count - 1).BySales = pCenter.NetoValue.Value
                                        list(list.Count - 1).ProductionCenterCodeName = dt.ProductionCenter.Code + " - " + dt.ProductionCenter.Name
                                    End If
                                End If
                            Next
                        End If
                End Select
            Next

            Return New ActionResult(Of List(Of DistributionDirectCostDetail)) With {.StateResult = True, .ObjectEmbbeded = list}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of DistributionDirectCostDetail)) With {.StateResult = False, .Message = ex.Message & vbCrLf & ex.StackTrace}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la Estimación Primaria o Final.
    ''' </summary>
    Public Function GetPrimaryEstimateOrFinal(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer) As ActionResult(Of SP_ReportGeneralProfitabilityTotalCost_Result) Implements IDistributionDirectCostAdminService.GetPrimaryEstimateOrFinal

        Try
            Dim Estimate As SP_ReportGeneralProfitabilityTotalCost_Result = Me._distributionDirectCostRepository.GetPrimaryEstimateOrFinal(InitialMonth, InitialYear, LastMonth, LastYear)

            Return New ActionResult(Of SP_ReportGeneralProfitabilityTotalCost_Result) With {.StateResult = True, .ObjectEmbbeded = Estimate}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SP_ReportGeneralProfitabilityTotalCost_Result) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la Estimación Primaria o Final. B
    ''' </summary>
    Public Function GetPrimaryEstimateOrFinalB(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer) As ActionResult(Of SP_ReportGeneralProfitabilityTotalCostB_Result) Implements IDistributionDirectCostAdminService.GetPrimaryEstimateOrFinalB

        Try
            Dim Estimate As SP_ReportGeneralProfitabilityTotalCostB_Result = Me._distributionDirectCostRepository.GetPrimaryEstimateOrFinalB(InitialMonth, InitialYear, LastMonth, LastYear)

            Return New ActionResult(Of SP_ReportGeneralProfitabilityTotalCostB_Result) With {.StateResult = True, .ObjectEmbbeded = Estimate}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SP_ReportGeneralProfitabilityTotalCostB_Result) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Metodo que ejecuta el storeProcedure "InteropCost.SP_ReportGeneralProfitabilityTotalCostC"
    ''' </summary>
    ''' <returns></returns>
    Function GetPrimaryEstimateOrFinalC(InitialMonth As Integer, ByVal InitialYear As Integer, LastMonth As Integer, ByVal LastYear As Integer, ByVal CenterType As Integer, session As SessionValues) As DataSet Implements IDistributionDirectCostAdminService.GetPrimaryEstimateOrFinalC
        'If ValidityId = 0 Then
        '    Throw New ArgumentNullException("ValidityId")
        'End If
        'If Month() = 0 Then
        '    Throw New ArgumentNullException("Month")
        'End If
        Try
            Dim ds As New DataSet
            Dim query1 As String
            'If categoryStart Is Nothing AndAlso categoryEnd Is Nothing Then
            '    query1 = "exec [InteropCost].[SP_ReportGeneralProfitabilityTotalCostC]] '" & dateStart & "','" & dateEnd & "',NULL, NULL"
            'Else
            '    query1 = "exec [InteropCost].[SP_ReportGeneralProfitabilityTotalCostC] '" & dateStart & "','" & dateEnd & "','" & categoryStart & "','" & categoryEnd & "'"
            'End If
            query1 = "exec [InteropCost].[SP_ReportGeneralProfitabilityTotalCostC] '" & InitialMonth & "','" & InitialYear & "','" & LastMonth & "','" & LastYear & "','" & CenterType & "'"


            Dim dt1 = Me.GetDatatable(query1, session, "ReportGeneralProfitabilityTotalCostC")
            ds.Tables.Add(dt1.Copy())
            Return ds

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function
    Public Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection(connectionString)
            Try
                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                Dim ds As New DataSet
                da.Fill(ds, nameDt)
                GetDatatable = ds.Tables(nameDt)
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return GetDatatable

            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()
            End Try
        End Using
    End Function

    Public Function ListGeneralExpenseByPeriod(year As Integer, month As Integer) As List(Of GeneralExpense) Implements IDistributionDirectCostAdminService.ListGeneralExpenseByPeriod
        Try
            Return _distributionDirectCostRepository.ListGeneralExpenseByPeriod(year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of GeneralExpense)()
        End Try
    End Function

    Public Function ListDistributionDirectCostToReport(containerCost As String, distributionDirectCostId As Integer) As List(Of SP_ReportDistributionDirectCost_Result) Implements IDistributionDirectCostAdminService.ListDistributionDirectCostToReport
        Try
            Return _distributionDirectCostRepository.ListDistributionDirectCostToReport(containerCost, distributionDirectCostId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_ReportDistributionDirectCost_Result)()
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _distributionDirectCostRepository = Nothing
            _sequenceDRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class