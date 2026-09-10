'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Carlos Mario Arias Rubiano
' Created          : 24-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Transactions
Imports System.Data.Entity.Core

Public Class ResumptionHolidayAdminService
    Implements IResumptionHolidayAdminService

#Region "Variables"

    'Repositorio para la reanudación de vacaciones
    Private _resumptionHolidayRepository As IResumptionHolidayRepository

    'Repositorio para las vacaciones
    Private _vacationRepository As IVacationRepository

    'Repositorio de calendario detalle
    Private _scheduleDetailRepository As IScheduleDetailRepository

    'Dominio del novedades
    Private _noveltyDomain As INoveltyDomain

    'Repositorio de calendario
    Private _scheduleRepository As IScheduleRepository

    'Repositorio de calendario detalle para hacer commit
    Private _scheduleDetailRepositoryCommit As IScheduleDetailRepository

    ' Repositorio de schedule detail hours
    Private _scheduleDetailHourRepository As IScheduleDetailHourRepository

    'Repositorio de schedule detail concept
    Private _scheduleDetailConceptRepository As IScheduleDetailConceptRepository

    'Repositorio de vacaciones
    Private _employeeRepository As IEmployeeRepository

    'Dominio de vacaciones
    Private _vacationDomain As IVacationPeriodDomain

#End Region

#Region "Builder"

    Public Sub New(resumptionHolidayRepository As IResumptionHolidayRepository, vacationRepository As IVacationRepository, scheduleDetailRepository As IScheduleDetailRepository,
                   noveltyDomain As INoveltyDomain, scheduleRepository As IScheduleRepository, scheduleDetailHourRepository As IScheduleDetailHourRepository,
                   scheduleDetailConceptRepository As IScheduleDetailConceptRepository, employeeRepository As IEmployeeRepository, vacationDomain As IVacationPeriodDomain,
                   scheduleDetailRepositoryCommit As IScheduleDetailRepository)
        If (resumptionHolidayRepository Is Nothing) Then
            Throw New ArgumentNullException("resumptionHolidayRepository")
        End If
        If (vacationRepository Is Nothing) Then
            Throw New ArgumentNullException("vacationRepository")
        End If
        If (scheduleDetailRepository Is Nothing) Then
            Throw New ArgumentNullException("scheduleDetailRepository")
        End If
        If (noveltyDomain Is Nothing) Then
            Throw New ArgumentNullException("noveltyDomain")
        End If
        If (scheduleRepository Is Nothing) Then
            Throw New ArgumentNullException("scheduleRepository")
        End If
        If (scheduleDetailHourRepository Is Nothing) Then
            Throw New ArgumentNullException("scheduleDetailHourRepository")
        End If
        If (scheduleDetailConceptRepository Is Nothing) Then
            Throw New ArgumentNullException("scheduleDetailConceptRepository")
        End If
        If (employeeRepository Is Nothing) Then
            Throw New ArgumentNullException("employeeRepository")
        End If
        If (vacationDomain Is Nothing) Then
            Throw New ArgumentNullException("vacationDomain")
        End If
        If (scheduleDetailRepositoryCommit Is Nothing) Then
            Throw New ArgumentNullException("scheduleDetailRepositoryCommit")
        End If
        _resumptionHolidayRepository = resumptionHolidayRepository
        _vacationRepository = vacationRepository
        _scheduleDetailRepository = scheduleDetailRepository
        _noveltyDomain = noveltyDomain
        _scheduleRepository = scheduleRepository
        _scheduleDetailHourRepository = scheduleDetailHourRepository
        _scheduleDetailConceptRepository = scheduleDetailConceptRepository
        _employeeRepository = employeeRepository
        _vacationDomain = vacationDomain
        _scheduleDetailRepositoryCommit = scheduleDetailRepositoryCommit
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la entidad
    ''' </summary>
    ''' <param name="EmployeeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetResumptionHoliday(EmployeeId As Integer) As ActionResult(Of List(Of ResumptionHoliday)) Implements IResumptionHolidayAdminService.GetResumptionHoliday
        If EmployeeId = 0 Then
            Throw New ArgumentNullException("EmployeeId")
        End If
        Try
            Dim ListResumptionHoliday As List(Of ResumptionHoliday) = Me._resumptionHolidayRepository.GetResumptionHoliday(EmployeeId)
            Return New ActionResult(Of List(Of ResumptionHoliday)) With {.StateResult = True, .ObjectEmbbeded = ListResumptionHoliday}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of ResumptionHoliday)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda la entidad
    ''' </summary>
    ''' <param name="ResumptionHoliday"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveResumptionHoliday(ResumptionHoliday As ResumptionHoliday, audit As AuditMessage) As ActionResult(Of ResumptionHoliday) Implements IResumptionHolidayAdminService.SaveResumptionHoliday
        If ResumptionHoliday Is Nothing Then
            Throw New ArgumentNullException("ResumptionHoliday")
        End If
        Dim unitOfWork As IUnitWork = Me._resumptionHolidayRepository.UnitWork
        Dim unitOfWorkVacation As IUnitWork = Me._vacationRepository.UnitWork
        Dim unitworkScheduleDetailConcept As IUnitWork = _scheduleDetailConceptRepository.UnitWork
        Dim unitworkScheduleDetailHour As IUnitWork = _scheduleDetailHourRepository.UnitWork
        Dim unitworkScheduleDetail As IUnitWork = _scheduleDetailRepositoryCommit.UnitWork
        Dim unitWorkSchedule As IUnitWork = _scheduleRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Se consultan las vacaciones del empleado
                Dim ListVacation = _resumptionHolidayRepository.GetVacationsOfEmployee(ResumptionHoliday.EmployeeId)

                'Si el empleado no tiene vacaciones
                If ListVacation Is Nothing OrElse ListVacation.Count = 0 Then
                    transaction.Dispose()
                    Return New ActionResult(Of ResumptionHoliday) With {.StateResult = False, .Message = "El empleado seleccionado no tiene vacaciones", .StatusCode = eStatusResult.WARNING}
                End If

                'Si la sumatoria de los días pendientes aplazados son igual a cero
                If ListVacation.Sum(Function(item) item.DaysDeferredPending) = 0 Then
                    transaction.Dispose()
                    Return New ActionResult(Of ResumptionHoliday) With {.StateResult = False, .Message = "El empleado seleccionado no tiene días pendientes aplazados", .StatusCode = eStatusResult.WARNING}
                End If

                'Si la cantidad de días requeridos es mayor a los días pendientes aplazados
                If ResumptionHoliday.RequestedDays > ListVacation.Sum(Function(item) item.DaysDeferredPending) Then
                    transaction.Dispose()
                    Return New ActionResult(Of ResumptionHoliday) With {.StateResult = False, .Message = "Los días solicitados por el empleado no pueden ser mayores a los días pendientes aplazados", .StatusCode = eStatusResult.WARNING}
                End If

                'Variable para saber los días solicitados por el empleado
                Dim RequestedDays As Integer = ResumptionHoliday.RequestedDays

                'Variable para saber la cantidad de días de cada vacación
                Dim RestValue As Integer = 0

                'Días asignados para los detalles de la reanudación
                Dim DaysAssign As Integer = 0

                For Each vacation In (From x In ListVacation Where x.DaysDeferredPending > 0) 'Se recorren las vacaciones siempre y cuando los días pendientes aplazados sean mayores a cero
                    'Se asigna los días pendientes de cada vacación para despues restarselos a los días solicitados
                    RestValue = vacation.DaysDeferredPending

                    If RequestedDays = vacation.DaysDeferredPending Then
                        DaysAssign = RequestedDays
                        vacation.DaysDeferredPending -= RequestedDays
                        RequestedDays -= RestValue
                    ElseIf RequestedDays > vacation.DaysDeferredPending Then
                        DaysAssign = vacation.DaysDeferredPending
                        RequestedDays -= vacation.DaysDeferredPending
                        vacation.DaysDeferredPending -= vacation.DaysDeferredPending
                    ElseIf RequestedDays < vacation.DaysDeferredPending Then
                        DaysAssign = RequestedDays
                        vacation.DaysDeferredPending -= RequestedDays
                        RequestedDays -= RequestedDays
                    End If

                    'Se agrega para el Bug 8033 / 25-07-2017 -
                    If vacation.State = 3 Then
                        vacation.State = 1
                    End If

                    'Se crea el detalle de la reanudación
                    Dim ResumptionHolidayDetail As New ResumptionHolidayDetail
                    With ResumptionHolidayDetail
                        .VacationId = vacation.Id
                        .Days = DaysAssign
                    End With

                    'Se asigna los detalles a la cabecera
                    ResumptionHoliday.ResumptionHolidayDetail.Add(ResumptionHolidayDetail)

                    'Se guarda las vacaciones
                    vacation.MarkAsModified()
                    _vacationRepository.SaveEntity(vacation)
                    unitOfWorkVacation.Commit()

                    'Si ya no quedan días para reanudar
                    If RequestedDays = 0 Then
                        Exit For
                    End If

                Next

                '-------------------------------------------- Inicio cuadro de turnos -----------------------------------------------------------------------------------------------------------'
                Dim listSchedule = _scheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDate(ResumptionHoliday.EmployeeId, ResumptionHoliday.InitialDate, ResumptionHoliday.EndDate)
                For Each itemDictionary As KeyValuePair(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) In _noveltyDomain.LoadDictionarySchedule(listSchedule)

                    Dim schedule As Schedule = _scheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(ResumptionHoliday.EmployeeId, itemDictionary.Key.Key, itemDictionary.Key.Value)

                    For Each itemDetail As ScheduleDetail In itemDictionary.Value

                        If itemDetail.ScheduleDetailHour IsNot Nothing AndAlso itemDetail.ScheduleDetailHour.Count > 0 Then

                            For Each ObjScheduleDetailHour As ScheduleDetailHour In itemDetail.ScheduleDetailHour

                                If ObjScheduleDetailHour.ScheduleDetailConcept IsNot Nothing AndAlso ObjScheduleDetailHour.ScheduleDetailConcept.Count > 0 Then
                                    Dim StringDeleteScheduleDetailConcept = "Delete from Payroll.ScheduleDetailConcept where ScheduleDetailHourId = " + ObjScheduleDetailHour.Id.ToString
                                    _scheduleDetailRepositoryCommit.UnitWork.ExecuteNonQuery(StringDeleteScheduleDetailConcept)
                                End If
                            Next

                            Dim StringDeleteScheduleDetailHour = "Delete from Payroll.ScheduleDetailHour where ScheduleDetailId = " + itemDetail.Id.ToString
                            _scheduleDetailRepositoryCommit.UnitWork.ExecuteNonQuery(StringDeleteScheduleDetailHour)
                        End If

                        'Actualizamos primero a Null la cabecera:
                        Dim Day As String

                        If itemDetail.DateDetail.Day < 10 Then
                            Day = "0" + itemDetail.DateDetail.Day.ToString()
                        Else
                            Day = itemDetail.DateDetail.Day.ToString()
                        End If

                        Dim StringUpdate = "UPDATE Payroll.Schedule SET D" + Day + " = Null WHERE Id = " + schedule.Id.ToString()

                        _scheduleRepository.UnitWork.ExecuteNonQuery(StringUpdate)

                        'Luego borramos los detalles creados
                        Dim StringDelete As String = "Delete From Payroll.ScheduleDetail where Id = " + itemDetail.Id.ToString()
                        _scheduleDetailRepositoryCommit.UnitWork.ExecuteNonQuery(StringDelete)

                    Next

                Next
                unitWorkSchedule.CommitAndRefreshChanges()
                unitworkScheduleDetailConcept.CommitAndRefreshChanges()
                unitworkScheduleDetailHour.CommitAndRefreshChanges()
                unitworkScheduleDetail.CommitAndRefreshChanges()
                Dim contractValid = _employeeRepository.GetContractValidByEmployee(ResumptionHoliday.EmployeeId)
                Dim employee = _employeeRepository.GetEmployeeById(ResumptionHoliday.EmployeeId, False)
                Dim listScheduleDetailVacation = _vacationDomain.CreateListScheduleDetailBetweenDate(employee, contractValid, ResumptionHoliday.InitialDate, ResumptionHoliday.EndDate)
                Dim dictionarySchedule As New Dictionary(Of String, Schedule) 'Diccionario solo para almacenar el periodo y el schedule
                For Each detail As ScheduleDetail In listScheduleDetailVacation
                    Dim period = detail.DateDetail.ToString("MM/yyyy")
                    If Not dictionarySchedule.ContainsKey(period) Then 'Verifico si ya lo esta en el diccionario es decir si ya tengo el objeto en memoria
                        Dim objSchedule = _scheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(ResumptionHoliday.EmployeeId, period, contractValid.FunctionalUnitId)
                        If objSchedule Is Nothing Then
                            objSchedule = New Schedule()
                            objSchedule.EmployeeId = ResumptionHoliday.EmployeeId
                            objSchedule.Period = period
                            objSchedule.FunctionalUnitId = contractValid.FunctionalUnitId
                        End If
                        dictionarySchedule.Add(period, objSchedule)
                    End If
                    Dim schedulePeriod = dictionarySchedule(period)
                    schedulePeriod.StartTracking()
                    schedulePeriod = _noveltyDomain.LoadDetailToDay(schedulePeriod, detail)
                Next
                For Each detailSchedule As KeyValuePair(Of String, Schedule) In dictionarySchedule
                    _scheduleRepository.SaveEntity(detailSchedule.Value)
                Next
                unitWorkSchedule.Commit()
                unitworkScheduleDetail.Commit()
                '------------------------------------------------------- Fin cuadro de turnos ------------------------------------------------------------------------------------------------'

                Dim auditProcess As IndigoAuditSimpleEntity(Of ResumptionHoliday)
                Dim status As Integer

                ResumptionHoliday.CreationUser = audit.CodeUser
                ResumptionHoliday.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert

                Me._resumptionHolidayRepository.SaveEntity(ResumptionHoliday)
                unitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ResumptionHoliday)(ResumptionHoliday, audit, status, Nothing)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                ResumptionHoliday.MarkAsUnchanged()

                transaction.Complete()
                Return New ActionResult(Of ResumptionHoliday) With {.StateResult = True, .ObjectEmbbeded = ResumptionHoliday, .StatusCode = eStatusResult.SUCCESS}
            Catch ex As OptimisticConcurrencyException
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                unitOfWorkVacation.RollbackChanges()
                Return New ActionResult(Of ResumptionHoliday) With {.StateResult = False, .Message = "Error de concurrencia", .StatusCode = eStatusResult.EXCEPTION}
            Catch ex As Exception
                transaction.Dispose()
                unitOfWork.RollbackChanges()
                unitOfWorkVacation.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of ResumptionHoliday) With {.StateResult = False, .Message = ex.Message, .StatusCode = eStatusResult.EXCEPTION}
            End Try
        End Using
    End Function


#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _noveltyDomain.Dispose()
                _vacationDomain.Dispose()
            End If
            _resumptionHolidayRepository = Nothing
            _vacationRepository = Nothing
            _scheduleDetailRepository = Nothing
            _noveltyDomain = Nothing
            _scheduleRepository = Nothing
            _scheduleDetailHourRepository = Nothing
            _scheduleDetailConceptRepository = Nothing
            _employeeRepository = Nothing
            _vacationDomain = Nothing
            _scheduleDetailRepositoryCommit = Nothing
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
