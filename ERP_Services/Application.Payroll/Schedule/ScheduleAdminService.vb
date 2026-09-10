'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar Narvaez
' Created          : 06-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class ScheduleAdminService
    Implements IScheduleAdminService

    ' Repositorio de horarios o cuadro de turnos
    Private _ScheduleRepository As IScheduleRepository

    ' Repositorio de horarios o cuadro de turnos
    Private _ScheduleRepositoryCommit As IScheduleRepository

    'Repositorio de Schedule Detail
    Private _ScheduleDetailRepository As IScheduleDetailRepository

    'Repositorio de Schedule Detail hour
    Private _ScheduleDetailHourRepository As IScheduleDetailHourRepository

    'Repositorio de Schedule Detail concept
    Private _ScheduleDetailConceptRepository As IScheduleDetailConceptRepository

    Private _ScheduleDomain As IScheduleDomain

    'Repositorio de Schedule Detail para commit
    Private _ScheduleDetailRepositoryCommit As IScheduleDetailRepository

    'Repositiorio de las unidades funcionales
    Private _FunctionalUnitRepository As IFunctionalUnitRepository

    ''' <summary>
    ''' Contructor el cual inicia la instancia del repositorio de Schedule
    ''' </summary>
    ''' <param name="repository">Repositorio de cuadro de turnos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IScheduleRepository, ByVal repositoryDetail As IScheduleDetailRepository, ByVal scheduleDomain As IScheduleDomain, ScheduleDetailRepositoryCommit As IScheduleDetailRepository, scheduleDetailHourRepository As IScheduleDetailHourRepository, scheduleDetailConceptRepository As IScheduleDetailConceptRepository, scheduleRepositoryCommit As IScheduleRepository, functionalUnitRepository As IFunctionalUnitRepository)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("ScheduleRepository Vacio")
        End If
        _ScheduleDetailRepository = repositoryDetail
        _ScheduleRepository = repository
        _ScheduleDomain = scheduleDomain
        _ScheduleDetailRepositoryCommit = ScheduleDetailRepositoryCommit
        _ScheduleDetailHourRepository = scheduleDetailHourRepository
        _ScheduleDetailConceptRepository = scheduleDetailConceptRepository
        _ScheduleRepositoryCommit = scheduleRepositoryCommit
        _FunctionalUnitRepository = functionalUnitRepository
    End Sub

    ''' <summary>
    ''' Elimina un horario
    ''' </summary>
    ''' <param name="schedule">Horario a eliminar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteSchedule(schedule As Schedule, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IScheduleAdminService.DeleteSchedule
        If schedule Is Nothing Then
            Throw New ArgumentNullException("Horario Vacio")
        End If
        Dim unitWork As IUnitWork = _ScheduleRepository.UnitWork
        Try
            _ScheduleRepository.DeleteEntity(schedule)
            unitWork.Commit()
            'IndigoAuditSimpleEntity(Of Schedule).Execute(schedule, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, schedule)
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un horario especifico
    ''' </summary>
    ''' <param name="functionalUnitCode">Codigo unidad funcional</param>
    ''' <param name="period">Peridodo del horario</param>
    ''' <returns>Horario</returns>
    ''' <remarks></remarks>
    Public Function GetSchedule(functionalUnitId As String, period As String) As List(Of Schedule) Implements IScheduleAdminService.GetSchedule
        If String.IsNullOrEmpty(functionalUnitId) Or String.IsNullOrEmpty(period) Then
            Throw New ArgumentNullException("Codigo Unidad Funcional o Periodo son Vacios")
        End If
        Try
            Return _ScheduleRepository.GetSchedule(functionalUnitId, period)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los horarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllSchedule() As List(Of Schedule) Implements IScheduleAdminService.ListAllSchedule
        Try
            Return _ScheduleRepository.ListAllSchedule()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda un Horario
    ''' </summary>
    ''' <param name="schedule">Horario</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveSchedule(schedule As Schedule, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IScheduleAdminService.SaveSchedule
        If schedule Is Nothing Then
            Throw New ArgumentNullException("Horario vacio")
        End If
        Dim unitWork As IUnitWork = _ScheduleRepositoryCommit.UnitWork
        Try
            If schedule.ChangeTracker.State = ObjectState.Modified Then
                schedule.ScheduleAux = _ScheduleRepository.GetScheduleByIdAsNoTracking(schedule.Id)
            End If

            Dim _type As Type = schedule.GetType()
            Dim _Property_Name As String = String.Empty
            For i As Integer = 1 To 31
                If i = 1 Then
                    _Property_Name = "ScheduleDetail"
                Else
                    _Property_Name = "ScheduleDetail" & (i - 1)
                End If
                Dim _Property = _type.GetProperty(_Property_Name)
                Dim _schDetHere As ScheduleDetail = CType(_Property.GetValue(schedule), ScheduleDetail)
                If _schDetHere IsNot Nothing AndAlso _schDetHere.ChangeTracker.State = ObjectState.Modified Then
                    _schDetHere.ScheduleDetailAux = _ScheduleDetailRepository.GetScheduleDetail(_schDetHere.Id, False)
                End If
            Next
            _ScheduleRepositoryCommit.SaveEntity(schedule)
            unitWork.Commit()
            '**********Auditoria Básica*************/
            Dim _type1 As Type = schedule.GetType()
            Dim _Property_Name1 As String = String.Empty
            For i As Integer = 1 To 31
                If i = 1 Then
                    _Property_Name1 = "ScheduleDetail"
                Else
                    _Property_Name1 = "ScheduleDetail" & (i - 1)
                End If
                Dim _Property = _type1.GetProperty(_Property_Name)
                Dim _schDetHere As ScheduleDetail = CType(_Property.GetValue(schedule), ScheduleDetail)
                If _schDetHere IsNot Nothing AndAlso _schDetHere.ChangeTracker.State = ObjectState.Added Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute(_schDetHere.GetType.Name, audit.Functional, _schDetHere.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                    '/*****Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of ScheduleDetail)(_schDetHere, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                    auditObject.Execute()
                ElseIf _schDetHere IsNot Nothing AndAlso _schDetHere.ChangeTracker.State = ObjectState.Modified Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute(_schDetHere.GetType.Name, audit.Functional, _schDetHere.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                    '/*****Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of ScheduleDetail)(_schDetHere, audit, Infrastructure.CrossCutting.Audit.Actions.Update, _schDetHere.ScheduleDetailAux)
                    auditObject.Execute()
                ElseIf _schDetHere IsNot Nothing AndAlso _schDetHere.ChangeTracker.State = ObjectState.Deleted Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute(_schDetHere.GetType.Name, audit.Functional, _schDetHere.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                    '/*****Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of ScheduleDetail)(_schDetHere, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                    auditObject.Execute()
                End If
            Next
            If schedule.ChangeTracker.State = ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(schedule.GetType.Name, audit.Functional, schedule.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/*****Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Schedule)(schedule, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf schedule.ChangeTracker.State = ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(schedule.GetType.Name, audit.Functional, schedule.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/*****Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Schedule)(schedule, audit, Infrastructure.CrossCutting.Audit.Actions.Update, schedule.ScheduleAux)
                auditObject.Execute()
            End If

            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            ex = New Exception($"Error desde Cuadro de turno: employeesId {schedule.EmployeeId} || User: {audit.CodeUser}, Machine {audit.ComputerName}", ex)
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' funcion para obtener todos lo turnos de un empleado en cierto rango de un periodo
    ''' </summary>
    ''' <param name="idEmployee">id del empleado</param>
    ''' <param name="period">periodo del calendario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleByEmployeePeriod(idEmployee As Integer, period As String, diaMin As Integer, diaMax As Integer) As List(Of Schedule) Implements IScheduleAdminService.GetScheduleByEmployeePeriod
        Try
            Return _ScheduleRepository.GetScheduleByEmployeePeriod(idEmployee, period, diaMin, diaMax)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion para obtener todos los turno que hay en un periodo y que tenga plantilla en un dia especifico
    ''' </summary>
    ''' <param name="period">periodo del calendario</param>
    ''' <param name="day">dia que debe tener plantilla</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleByPeriodDay(period As String, day As Integer, functionalUnitId As Integer) As List(Of Schedule) Implements IScheduleAdminService.GetScheduleByPeriodDay
        Try
            Return _ScheduleRepository.GetScheduleByPeriodDay(period, day, functionalUnitId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' funcion la cual obtiene un turno del empleado en un periodo especifico y en una unidad funcional especifica
    ''' </summary>
    ''' <param name="idEmployee">empleado</param>
    ''' <param name="period">Periodo</param>
    ''' <param name="functionalUnitId">unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleByEmployeePeriodFunctionalUnit(idEmployee As Integer, period As String, functionalUnitId As Integer) As Schedule Implements IScheduleAdminService.GetScheduleByEmployeePeriodFunctionalUnit
        Try
            Return _ScheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(idEmployee, period, functionalUnitId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' funcion para obtener todos lo turnos de un empleado en cierto rango de un periodo y en una unidad funcional especifica
    ''' </summary>
    ''' <param name="idEmployee">id del empleado</param>
    ''' <param name="period">periodo del calendario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleByEmployeePeriodFunctionalUnitRangeDays(idEmployee As Integer, period As String, functionalUnitId As Integer, diaMin As Integer, diaMax As Integer) As Schedule Implements IScheduleAdminService.GetScheduleByEmployeePeriodFunctionalUnitRangeDays
        Try
            Return _ScheduleRepository.GetScheduleByEmployeePeriodFunctionalUnitRangeDays(idEmployee, period, functionalUnitId, diaMin, diaMax)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion que retorna lista de turnos de empleado para un rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="dateInitial">Fecha Inicio</param>
    ''' <param name="dateEnd">Fecha Fin</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByEmployeeBetweenDate(employeeId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail) Implements IScheduleAdminService.GetScheduleDetailByEmployeeBetweenDate
        Try
            Return _ScheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDate(employeeId, dateInitial, dateEnd)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion que elimina masivamente una lista de detalles
    ''' </summary>
    ''' <param name="listScheduleDetail">Lista de detalles de un calendario</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteScheduleDetailMasive(listScheduleDetail As List(Of ScheduleDetail), audit As AuditMessage) As Boolean Implements IScheduleAdminService.DeleteScheduleDetailMasive
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadUncommitted
        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Dim unitWork As IUnitWork = _ScheduleRepository.UnitWork
            Dim unitWorkCommit As IUnitWork = _ScheduleRepositoryCommit.UnitWork
            Dim unitWorkDetail As IUnitWork = _ScheduleDetailRepository.UnitWork
            Dim unitWorkHour As IUnitWork = _ScheduleDetailHourRepository.UnitWork
            Dim unitWorkConcept As IUnitWork = _ScheduleDetailConceptRepository.UnitWork

            Try
                Dim ListTmpScheduleDetail As New List(Of ScheduleDetail)
                For Each detail As ScheduleDetail In listScheduleDetail
                    ' If detail.ScheduleDetailHour IsNot Nothing AndAlso detail.ScheduleDetailHour.Count > 0 Then
                    For Each ObjScheduleDetailHour As ScheduleDetailHour In detail.ScheduleDetailHour
                        Dim StringDeleteConcept = "DELETE FROM Payroll.ScheduleDetailConcept WHERE ScheduleDetailHourId = " + ObjScheduleDetailHour.Id.ToString
                        _ScheduleDetailConceptRepository.UnitWork.ExecuteNonQuery(StringDeleteConcept)

                        Dim StringDeleteHour = "DELETE FROM Payroll.ScheduleDetailHour WHERE Id = " + ObjScheduleDetailHour.Id.ToString
                        _ScheduleDetailHourRepository.UnitWork.ExecuteNonQuery(StringDeleteHour)

                    Next

                    ListTmpScheduleDetail.Add(detail)
                Next

                ' Verificacion integridad después de preparar la lista
                For Each objScheduleDetail As ScheduleDetail In ListTmpScheduleDetail
                    If Not ValidateScheduleDetailIntegrity(objScheduleDetail) Then
                        Return False
                    End If
                Next

                'Recorro ahora para actualizar o borrar el schedule
                For Each objScheduleDetail As ScheduleDetail In ListTmpScheduleDetail
                    Dim period = objScheduleDetail.DateDetail.ToString("MM/yyyy")
                    Dim schedule = Me.GetScheduleByEmployeePeriodFunctionalUnit(objScheduleDetail.EmployeeId, period, objScheduleDetail.ScheduleFunctionalUnitId) 'cargo el schedule de su unidad funcional, ya que no puede borar
                    If schedule IsNot Nothing Then
                        'schedule = _ScheduleDomain.DeleteScheduleDetail(schedule, ListTmpScheduleDetail)
                        Dim TotalHours As Integer = schedule.TotalHour - objScheduleDetail.TotalNumberHours

                        Dim Day As String

                        If objScheduleDetail.DateDetail.Day < 10 Then
                            Day = "0" + objScheduleDetail.DateDetail.Day.ToString()
                        Else
                            Day = objScheduleDetail.DateDetail.Day.ToString()
                        End If

                        If TotalHours < 0 Then
                            TotalHours = 0
                        End If

                        If TotalHours > 0 Then
                            Dim StringUpdate = "UPDATE Payroll.Schedule SET D" + Day + " = NULL, TotalHour = " + TotalHours.ToString() + " WHERE Id = " + schedule.Id.ToString
                            _ScheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringUpdate)
                        Else
                            Dim StringUpdate = "DELETE FROM Payroll.Schedule WHERE Id = " + schedule.Id.ToString
                            _ScheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringUpdate)
                        End If

                    End If

                    Dim StringDeleteDetail = "DELETE FROM Payroll.ScheduleDetail WHERE Id = " + objScheduleDetail.Id.ToString

                    _ScheduleDetailRepository.UnitWork.ExecuteNonQuery(StringDeleteDetail)

                Next

                '*************Auditoria**************'

                For Each detail As ScheduleDetail In listScheduleDetail
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute(detail.GetType.Name, audit.Functional, detail.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                    '/*****Auditoria Avanzada ******/
                    Dim auditObject As New IndigoAuditSimpleEntity(Of ScheduleDetail)(detail, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                Next

                unitWorkHour.Commit()
                unitWorkConcept.Commit()
                unitWorkDetail.Commit()
                unitWorkCommit.Commit()

                scope.Complete()

                Return True
            Catch ex As Exception
                unitWorkConcept.RollbackChanges()
                unitWorkHour.RollbackChanges()
                unitWorkDetail.RollbackChanges()
                unitWork.RollbackChanges()
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")

                Return False
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Guarda varios schedule a varios empleados de diferentes dias
    ''' </summary>
    ''' <param name="_scheduleDetail">El schedule detail (turno) que se replicará</param>
    ''' <param name="_employeesCheked">Listado de Id's de los empleados que se les registrara el turno</param>
    ''' <param name="_dayToSave">Listado de días que se van a registrar</param>
    ''' <param name="newFunctionalUnit">Objeto tipo FunctionalUnit, para saber si se registrar en una unidad funcional diferente</param>
    ''' <param name="Include_Holiday">Indica si se registran los dias feriados</param>
    ''' <param name="currentFunctionalUnit">Objeto que contiene la unidad funcional actual</param>
    ''' <param name="ScheduleDatasource">Listado de schedule que tiene el datasource actual</param>
    ''' <param name="Period">Periodo del schedule inicial en el formulario</param>
    ''' <param name="_List_Holidays">Listado de festivos para ese periodo</param>
    ''' <returns>Si se realizo o no la operación</returns>
    ''' <remarks></remarks>
    Public Function SaveScheduleDetailMasive(_scheduleDetail As ScheduleDetail, _employeesCheked As List(Of Integer), _dayToSave As List(Of Date), newFunctionalUnit As FunctionalUnit, Include_Holiday As Boolean, currentFunctionalUnit As FunctionalUnit, ScheduleDatasource As List(Of Schedule), Period As String, _List_Holidays As List(Of Domain.Entities.Holiday), audit As AuditMessage, Optional EditFlag As Boolean = False) As ActionMessageResult(Of List(Of Schedule)) Implements IScheduleAdminService.SaveScheduleDetailMasive
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadUncommitted
        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                If EditFlag = True AndAlso _scheduleDetail.Id > 0 Then
                    Dim StringDeleteConcept = "DELETE FROM Payroll.ScheduleDetailConcept WHERE ScheduleDetailHourId in (Select SDH.Id from Payroll.ScheduleDetail SD, Payroll.ScheduleDetailHour SDH WHERE SD.Id = SDH.ScheduleDetailId AND SD.Id = " + _scheduleDetail.Id.ToString + " ) "
                    _ScheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringDeleteConcept)

                    Dim StringDeleteHour = "DELETE FROM Payroll.ScheduleDetailHour WHERE ScheduleDetailId = " + _scheduleDetail.Id.ToString
                    _ScheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringDeleteHour)

                    Dim ObjPeriod = _scheduleDetail.DateDetail.ToString("MM/yyyy")

                    Dim Day As String

                    If _scheduleDetail.DateDetail.Day < 10 Then
                        Day = "0" + _scheduleDetail.DateDetail.Day.ToString()
                    Else
                        Day = _scheduleDetail.DateDetail.Day.ToString()
                    End If

                    Dim StringUpdateSchedule = "UPDATE Payroll.Schedule SET D" + Day + " = NULL WHERE EmployeeId = " + _scheduleDetail.EmployeeId.ToString + " and Period = '" + ObjPeriod + "'"
                    _ScheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringUpdateSchedule)
                    _ScheduleRepositoryCommit.UnitWork.Commit()

                    Dim StringDelete = "DELETE FROM Payroll.ScheduleDetail WHERE Id = " + _scheduleDetail.Id.ToString
                    _ScheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringDelete)
                End If

                Dim _listScheduleToSave = _ScheduleDomain.SaveScheduleDetailMasive(_scheduleDetail, _employeesCheked, _dayToSave, newFunctionalUnit, Include_Holiday, currentFunctionalUnit, ScheduleDatasource, Period, _List_Holidays, audit, EditFlag)
                For Each _sch As Schedule In _listScheduleToSave.ObjectEmbbeded
                    'calcular numero de horas en el schedule
                    Dim tH As Decimal = _sch.TotalHour 'acumulador para almacenar el total de horas
                    Dim _type As Type = _sch.GetType()
                    Dim _Property_Name As String = String.Empty

                    If EditFlag = True Then
                        tH = 0
                    End If

                    For i As Integer = 1 To 31
                        If i = 1 Then
                            _Property_Name = "ScheduleDetail"
                        Else
                            _Property_Name = "ScheduleDetail" & (i) - 1
                        End If
                        Dim _Property = _type.GetProperty(_Property_Name)

                        Dim _schDet As ScheduleDetail = CType(_Property.GetValue(_sch), ScheduleDetail)
                        If _schDet IsNot Nothing Then
                            For Each detailHour In _schDet.ScheduleDetailHour
                                'Si es un evento y no se agrego nada en ScheduleDetailConcept, significa que no han parametrizado conceptos de eventos
                                If detailHour.Event AndAlso detailHour.ScheduleDetailConcept.Count = 0 Then
                                    Return New ActionMessageResult(Of List(Of Schedule)) With
                                     {
                                         .StateResult = False,
                                         .Message = "No se encontraron conceptos de eventos en el grupo del empleado. Asegúrese de parametrizar estos conceptos en el formulario de 'Grupos'-segmento-'Concepto Eventos' e intente nuevamente."
                                     }
                                End If
                            Next
                            tH += _schDet.TotalNumberHours
                            Dim _type_s As Type = _schDet.GetType()
                            Dim _propertyNameS As String = String.Empty
                            For j As Integer = 1 To 31
                                If j = 1 Then
                                    _propertyNameS = "Schedule"
                                Else
                                    _propertyNameS = "Schedule" & (j) - 1
                                End If
                                Dim _PropertyS = _type_s.GetProperty(_propertyNameS)
                                Dim _thisSchedule As TrackableCollection(Of Schedule) = CType(_PropertyS.GetValue(_schDet), TrackableCollection(Of Schedule))
                                For Each item In _thisSchedule
                                    With item 'quita agregados
                                        If .FunctionalUnit IsNot Nothing Then
                                            .FunctionalUnitId = .FunctionalUnit.Id
                                            .FunctionalUnit = Nothing
                                        End If
                                        If .Employee IsNot Nothing Then
                                            .EmployeeId = .Employee.Id
                                            .Employee = Nothing
                                        End If
                                    End With
                                Next
                            Next
                        End If
                    Next

                    If EditFlag = True Then
                        Dim tmpScheduleDetail = Me.GetScheduleDetailByEmployeeBetweenDate(_scheduleDetail.EmployeeId, New Date(_scheduleDetail.DateDetail.Year, _scheduleDetail.DateDetail.Month, 1), New Date(_scheduleDetail.DateDetail.Year, _scheduleDetail.DateDetail.Month, Date.DaysInMonth(_scheduleDetail.DateDetail.Year, _scheduleDetail.DateDetail.Month)))
                        If tmpScheduleDetail IsNot Nothing AndAlso tmpScheduleDetail.Count > 0 Then
                            tH = tH + tmpScheduleDetail.Sum(Function(x) x.TotalNumberHours)
                        End If
                    End If

                    If _sch.TotalHour <> tH Then
                        _sch.TotalHour = tH
                        If _sch.ChangeTracker.State <> ObjectState.Added Then
                            _sch.MarkAsModified()
                        End If
                    End If

                    With _sch 'quita agregados
                        If .FunctionalUnit IsNot Nothing Then
                            .FunctionalUnitId = .FunctionalUnit.Id
                            .FunctionalUnit = Nothing
                        End If
                        If .Employee IsNot Nothing Then
                            .EmployeeId = .Employee.Id
                            .Employee = Nothing
                        End If
                    End With
                    If _sch.ChangeTracker.State = ObjectState.Modified Then
                        _sch.ScheduleAux = _ScheduleRepository.GetScheduleByIdAsNoTracking(_sch.Id)
                    End If
                    If Not (_sch.ChangeTracker.State = ObjectState.Added AndAlso _sch.TotalHour = 0) Then

                        If _listScheduleToSave.MessageResult.Count = 0 Then

                            If _sch.Id > 0 And EditFlag = True Then
                                _sch.MarkAsModified()
                            End If

                            _ScheduleRepositoryCommit.SaveEntity(_sch)


                        Else
                            If _listScheduleToSave.MessageResult.Any(Function(x) x.CodeMessage <> "-011") Then
                                If _sch.Id > 0 And EditFlag = True Then
                                    _sch.MarkAsModified()
                                End If

                                If _sch.Id > 0 And EditFlag = True And _listScheduleToSave.MessageResult.Any(Function(x) x.CodeMessage = "-008") Then
                                    _sch.MarkAsUnchanged()
                                    Return New ActionMessageResult(Of List(Of Schedule)) With
                                     {
                                         .StateResult = True,
                                         .MessageResult = _listScheduleToSave.MessageResult
                                     }
                                End If


                                _ScheduleRepositoryCommit.SaveEntity(_sch)
                            ElseIf _listScheduleToSave.MessageResult.Any(Function(x) x.CodeMessage = "-011") Then
                                Return New ActionMessageResult(Of List(Of Schedule)) With
                                  {
                                      .StateResult = True,
                                      .MessageResult = _listScheduleToSave.MessageResult
                                  }
                            End If
                        End If
                    End If
                Next

                '**********Auditoria Básica*************/
                For Each _sch As Schedule In _listScheduleToSave.ObjectEmbbeded
                    Dim _type As Type = _sch.GetType()
                    Dim _Property_Name As String = String.Empty
                    For i As Integer = 1 To 31
                        If i = 1 Then
                            _Property_Name = "ScheduleDetail"
                        Else
                            _Property_Name = "ScheduleDetail" & (i - 1)
                        End If
                        Dim _Property = _type.GetProperty(_Property_Name)
                        Dim _schDetHere As ScheduleDetail = CType(_Property.GetValue(_sch), ScheduleDetail)
                        If _schDetHere IsNot Nothing AndAlso _schDetHere.ChangeTracker.State = ObjectState.Added Then
                            '/*****Auditoria Basica ******/
                            IndigoAuditBasic.Execute(_schDetHere.GetType.Name, audit.Functional, _schDetHere.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company)
                            '/*****Auditoria Avanzada ******/
                            Dim auditObject As New IndigoAuditSimpleEntity(Of ScheduleDetail)(_schDetHere, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                            auditObject.Execute()
                        ElseIf _schDetHere IsNot Nothing AndAlso _schDetHere.ChangeTracker.State = ObjectState.Modified Then
                            IndigoAuditBasic.Execute(_schDetHere.GetType.Name, audit.Functional, _schDetHere.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company)
                        ElseIf _schDetHere IsNot Nothing AndAlso _schDetHere.ChangeTracker.State = ObjectState.Deleted Then
                            IndigoAuditBasic.Execute(_schDetHere.GetType.Name, audit.Functional, _schDetHere.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company)
                        End If
                    Next
                    If _sch.ChangeTracker.State = ObjectState.Added AndAlso _sch.TotalHour > 0 Then
                        '/*****Auditoria Básica ******/
                        IndigoAuditBasic.Execute(_sch.GetType.Name, audit.Functional, _sch.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company)
                        '/*****Auditoria Avanzada ******/
                        Dim auditObject As New IndigoAuditSimpleEntity(Of Schedule)(_sch, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                        auditObject.Execute()
                    ElseIf _sch.ChangeTracker.State = ObjectState.Modified Then
                        '/*****Auditoria Básica ******/
                        IndigoAuditBasic.Execute(_sch.GetType.Name, audit.Functional, _sch.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company)
                        '/*****Auditoria Avanzada ******/
                        Dim auditObject As New IndigoAuditSimpleEntity(Of Schedule)(_sch, audit, Infrastructure.CrossCutting.Audit.Actions.Update, _sch.ScheduleAux)
                        auditObject.Execute()
                    End If
                Next

                _ScheduleRepositoryCommit.UnitWork.Commit()
                scope.Complete()

                'Se marcan los objetos hijos como Unchanged para evitar duplicados
                For Each sch In _listScheduleToSave.ObjectEmbbeded
                    For Each i In Enumerable.Range(1, 31)
                        Dim propName = If(i = 1, "ScheduleDetail", "ScheduleDetail" & (i - 1))
                        Dim schDet = TryCast(sch.GetType().GetProperty(propName)?.GetValue(sch), ScheduleDetail)
                        If schDet IsNot Nothing Then
                            schDet.MarkAsUnchanged
                            For Each sdh In schDet.ScheduleDetailHour
                                sdh.MarkAsUnchanged
                            Next
                        End If
                    Next
                    sch.MarkAsUnchanged
                Next

                Return New ActionMessageResult(Of List(Of Schedule)) With
                {
                    .StateResult = True,
                    .MessageResult = _listScheduleToSave.MessageResult,
                    .ObjectEmbbeded = _listScheduleToSave.ObjectEmbbeded
                }
            Catch ex As Exception
                _ScheduleDetailRepositoryCommit.UnitWork.RollbackChanges()
                _ScheduleRepositoryCommit.UnitWork.RollbackChanges()
                scope.Dispose()
                ex = New Exception($"Error desde Cuadro de turno: employeesIds{ String.Join(",", _employeesCheked)} en los dias: {String.Join(", ", _dayToSave)} || User: {audit.CodeUser}, Machine {audit.ComputerName}", ex)
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")

                Return New ActionMessageResult(Of List(Of Schedule)) With
                {
                    .StateResult = False,
                    .Message = Utils.GetInnerExceptionMessageToString(ex)
                }
            End Try
        End Using
    End Function

    Public Function AnalisisEmployeeSchedule(InitialDate As Date, EndDate As Date, IdFunctionalUnit As Integer, IdPosition As Integer, IdEmployee As Integer) As List(Of SP_AnalisEmployeeSchedule_Result) Implements IScheduleAdminService.AnalisisEmployeeSchedule
        Try
            Return _ScheduleRepository.AnalisisEmployeeSchedule(InitialDate, EndDate, IdFunctionalUnit, IdPosition, IdEmployee)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of SP_AnalisEmployeeSchedule_Result)()
        End Try
    End Function

    Public Function SaveExtraHours(ListSpAnalisisSchedule As List(Of SP_AnalisEmployeeSchedule_Result)) As SP_SaveExtraHours_Result Implements IScheduleAdminService.SaveExtraHours

        Dim ObjReturn As New SP_SaveExtraHours_Result

        Try

            Dim ObjXml = Me.ConvertToXmlListAnalisisEmployeeSchedule(ListSpAnalisisSchedule)

            If ObjXml IsNot Nothing Then
                ObjReturn = _ScheduleRepository.ExecuteSaveExtraHours(ObjXml)
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")

        End Try

        Return ObjReturn

    End Function


    ''' <summary>
    ''' Convierte la entidad principal en xml
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertToXmlListAnalisisEmployeeSchedule(ListSpAnalisisSchedule As List(Of SP_AnalisEmployeeSchedule_Result)) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<AnalisisSchedule>")

        For Each ObjAnalisisSchedule As SP_AnalisEmployeeSchedule_Result In ListSpAnalisisSchedule

            builder.Append("<Cedula>" & ObjAnalisisSchedule.Cedula & "</Cedula>")
            builder.Append("<Empleado>" & ObjAnalisisSchedule.Empleado & "</Empleado>")
            builder.Append("<IdEmployee>" & ObjAnalisisSchedule.IdEmployee & "</IdEmployee>")
            builder.Append("<IdContract>" & ObjAnalisisSchedule.IdContract & "</IdContract>")
            builder.Append("<IdFunctionalUnit>" & ObjAnalisisSchedule.IdFunctionalUnit & "</IdFunctionalUnit>")
            builder.Append("<IdCostCenter>" & ObjAnalisisSchedule.IdCostCenter & "</IdCostCenter>")
            builder.Append("<InitialDate>" & ObjAnalisisSchedule.InitialDate.Value.ToString("dd/MM/yyyy HH:mm") & "</InitialDate>")
            builder.Append("<InitialTime>" & ObjAnalisisSchedule.InitialTime.Value.ToString() & "</InitialTime>")
            builder.Append("<EndDate>" & ObjAnalisisSchedule.EndDate.Value.ToString("dd/MM/yyyy HH:mm") & "</EndDate>")
            builder.Append("<EndTime>" & ObjAnalisisSchedule.EndTime.Value.ToString() & "</EndTime>")
            builder.Append("<Extratime>" & ObjAnalisisSchedule.ExtraTime & "</Extratime>")
            builder.Append("<HoursExtraTime>" & ObjAnalisisSchedule.HoursExtraTime & "</HoursExtraTime>")
            builder.Append("<InitialDateExtraTime>" & ObjAnalisisSchedule.InitialDateExtraTime.Value.ToString("dd/MM/yyyy HH:mm") & "</InitialDateExtraTime>")
            builder.Append("<InitialHourExtraTime>" & ObjAnalisisSchedule.InitialHourExtraTime.Value.ToString() & "</InitialHourExtraTime>")
            builder.Append("<EndDateExtraTime>" & ObjAnalisisSchedule.EndDateExtraTime.Value.ToString("dd/MM/yyyy HH:mm") & "</EndDateExtraTime>")
            builder.Append("<EndTimeExtraTime>" & ObjAnalisisSchedule.EndTimeExtraTime.Value.ToString() & "</EndTimeExtraTime>")

        Next


        builder.Append("</AnalisisSchedule>")

        Return builder.ToString()
    End Function

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _ScheduleDomain.Dispose()
            End If
            _ScheduleDetailRepository = Nothing
            _ScheduleRepository = Nothing
            _ScheduleDomain = Nothing
            _ScheduleDetailRepositoryCommit = Nothing
            _ScheduleDetailHourRepository = Nothing
            _ScheduleDetailConceptRepository = Nothing
            _ScheduleRepositoryCommit = Nothing
            _FunctionalUnitRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    ''' <summary>
    ''' Verifica si un ScheduleDetail tiene un Schedule válido asociado
    ''' </summary>
    ''' <param name="detail">ScheduleDetail a validar</param>
    ''' <returns>True si tiene Schedule válido, False si no</returns>
    Private Function ValidateScheduleDetailIntegrity(detail As ScheduleDetail) As Boolean
        Try
            If detail Is Nothing Then
                Return False
            End If

            ' Obtener el Schedule del empleado para el período
            Dim period = detail.DateDetail.ToString("MM/yyyy")
            Dim schedule = Me.GetScheduleByEmployeePeriodFunctionalUnit(detail.EmployeeId, period, detail.ScheduleFunctionalUnitId)

            If schedule Is Nothing Then
                Return False ' No existe Schedule para este empleado/período
            End If

            ' Verificar que el Schedule referencia este ScheduleDetail en el día correspondiente
            Dim day = detail.DateDetail.Day
            Dim propertyName As String = If(day = 1, "D01", "D" & day.ToString("D2"))
            Dim type As Type = schedule.GetType()
            Dim propertyInfo = type.GetProperty(propertyName)

            If propertyInfo IsNot Nothing Then
                Dim referencedDetailId = propertyInfo.GetValue(schedule)
                Return referencedDetailId IsNot Nothing AndAlso Convert.ToInt32(referencedDetailId) = detail.Id
            End If

            Return False

        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

End Class