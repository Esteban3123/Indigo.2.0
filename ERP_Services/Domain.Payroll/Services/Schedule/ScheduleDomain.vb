'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 17-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Diagnostics
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Class ScheduleDomain
    Implements IScheduleDomain

    ' Repositorio de horarios o cuadro de turnos
    Private _ScheduleRepository As IScheduleRepository
    ' Repositorio de horarios o cuadro de turnos
    Private _ScheduleRepositoryCommit As IScheduleRepository
    'Repositorio de Schedule Detail
    Private _ScheduleDetailRepository As IScheduleDetailRepository
    'Repositiorio de las unidades funcionales
    Private _FunctionalUnitRepository As IFunctionalUnitRepository
    'Repositorio de empleados
    Private _employeeRepository As IEmployeeRepository
    'Repositorio de conceptos
    Private _conceptRepository As IConceptRepository

    ''' <summary>
    ''' Contructor el cual inicia la instancia del repositorio de Schedule
    ''' </summary>
    ''' <param name="repository">Repositorio de cuadro de turnos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IScheduleRepository, ByVal scheduleRepositoryCommit As IScheduleRepository, ByVal repositoryDetail As IScheduleDetailRepository, functionalUnitRepository As IFunctionalUnitRepository, employeeRepository As IEmployeeRepository, conceptRepository As IConceptRepository)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("ScheduleRepository Vacio")
        End If
        _ScheduleDetailRepository = repositoryDetail
        _ScheduleRepositoryCommit = scheduleRepositoryCommit
        _ScheduleRepository = repository
        _FunctionalUnitRepository = functionalUnitRepository
        _employeeRepository = employeeRepository
        _conceptRepository = conceptRepository
    End Sub

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
    Public Function SaveScheduleDetailMasive(_scheduleDetail As ScheduleDetail, _employeesCheked As List(Of Integer), _dayToSave As List(Of Date), newFunctionalUnit As FunctionalUnit, Include_Holiday As Boolean, currentFunctionalUnit As FunctionalUnit, ScheduleDatasource As List(Of Schedule), Period As String, _List_Holidays As List(Of Domain.Entities.Holiday), audit As AuditMessage, Optional EditFlag As Boolean = False) As ActionMessageResult(Of List(Of Schedule)) Implements IScheduleDomain.SaveScheduleDetailMasive
        Dim parts As String() = Period.Split(New Char() {"/"c})
        Dim YearControl As Integer = CInt(parts(1))
        Dim MonthControl As Integer = CInt(parts(0))
        Dim _listScheduleWithoutSave As List(Of MessageResult) = New List(Of MessageResult)
        Dim _listScheduleToSave As List(Of Schedule) = New List(Of Schedule)
        Dim _listToNotSave As List(Of Integer) = New List(Of Integer)
        Dim _schedule As Schedule
        Dim _validContract As Contract
        Dim FunctionalUnitTemp As FunctionalUnit

        Dim _ScheduleFunctionalUnit As FunctionalUnit ' para almacenar el Id de la unidad funcional que vamos a registrar turnos
        For Each _itemEmployee In _employeesCheked
            If newFunctionalUnit Is Nothing Then ' se registrara en la misma unidad funcional
                _ScheduleFunctionalUnit = currentFunctionalUnit
            Else ' se registrara en otra unidad funcional
                _ScheduleFunctionalUnit = newFunctionalUnit
            End If

            _schedule = _ScheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(_itemEmployee, Period, _ScheduleFunctionalUnit.Id, False)
            If _schedule Is Nothing Then
                Dim employee As Employee = _employeeRepository.GetEmployeeById(_itemEmployee)
                _schedule = New Schedule
                With _schedule
                    .Employee = employee
                    .EmployeeId = employee.Id
                    .Period = Period
                    .FunctionalUnit = _ScheduleFunctionalUnit
                    .FunctionalUnitId = _ScheduleFunctionalUnit.Id
                End With
            End If


            _validContract = _schedule.Employee.Contract.Where(Function(x) x.Valid = True And x.Status = 1).FirstOrDefault
            FunctionalUnitTemp = _FunctionalUnitRepository.GetFunctionalUnitById(_validContract.FunctionalUnitId)
            Dim T_General_Hours As Decimal = 0
            For Each _DateToSave In _dayToSave
                'Validar si registramos en el schedule de este periodo, o si se pasa a otro schedule
                Dim _schDate As Date = New Date(YearControl, MonthControl, 1).AddMonths(1)
                Dim _toRegDate As Date = New Date(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day)
                If _toRegDate >= _schDate Then
                    Dim _period As String
                    If _toRegDate.Month.ToString.Length = 1 Then
                        _period = "0" & _toRegDate.Month & "/" & _toRegDate.Year
                    Else
                        _period = _toRegDate.Month & "/" & _toRegDate.Year
                    End If
                    If _schedule.Period <> _period Then
                        _schedule = _ScheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(_itemEmployee, _period, _ScheduleFunctionalUnit.Id, False)
                        If _schedule Is Nothing Then
                            Dim employee As Employee = _employeeRepository.GetEmployeeById(_itemEmployee)
                            _schedule = New Schedule
                            With _schedule
                                .Employee = employee
                                .EmployeeId = employee.Id
                                .Period = _period
                                .FunctionalUnitId = _ScheduleFunctionalUnit.Id
                            End With
                        End If
                    End If
                    _schDate.AddMonths(1)
                End If

                Dim schDetByEmployee As List(Of ScheduleDetail) ' = New List(Of ScheduleDetail)
                If _listScheduleToSave.Count = 0 Then
                    schDetByEmployee = _ScheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDate(_schedule.EmployeeId, New Date(_DateToSave.Year, _DateToSave.Month, 1), New Date(_DateToSave.Year, _DateToSave.Month, DateTime.DaysInMonth(_DateToSave.Year, _DateToSave.Month)))
                    _listScheduleToSave.Add(_schedule)
                    T_General_Hours = 0 '_ScheduleDetailRepository.GetHoursNumber_ScheduleDetailByEmployeeBetweenDate(_schedule.EmployeeId, New Date(_DateToSave.Year, _DateToSave.Month, 1), New Date(_DateToSave.Year, _DateToSave.Month, DateTime.DaysInMonth(_DateToSave.Year, _DateToSave.Month)))
                    If schDetByEmployee IsNot Nothing AndAlso schDetByEmployee.Count > 0 Then
                        For Each itemSDetail As ScheduleDetail In schDetByEmployee
                            T_General_Hours += itemSDetail.TotalNumberHours
                        Next
                    End If
                End If
                If _listScheduleToSave.Count > 0 AndAlso _listScheduleToSave.Find(Function(x) x.Period = _schedule.Period And x.EmployeeId = _schedule.EmployeeId) Is Nothing Then ' OrElse _listScheduleToSave.Find(Function(x) x.EmployeeId = _schedule.EmployeeId) Is Nothing Then
                    schDetByEmployee = _ScheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDate(_schedule.EmployeeId, New Date(_DateToSave.Year, _DateToSave.Month, 1), New Date(_DateToSave.Year, _DateToSave.Month, DateTime.DaysInMonth(_DateToSave.Year, _DateToSave.Month)))
                    _listScheduleToSave.Add(_schedule)
                    T_General_Hours = 0 ' _ScheduleDetailRepository.GetHoursNumber_ScheduleDetailByEmployeeBetweenDate(_schedule.EmployeeId, New Date(_DateToSave.Year, _DateToSave.Month, 1), New Date(_DateToSave.Year, _DateToSave.Month, DateTime.DaysInMonth(_DateToSave.Year, _DateToSave.Month)))
                    If schDetByEmployee IsNot Nothing AndAlso schDetByEmployee.Count > 0 Then
                        For Each itemSDetail As ScheduleDetail In schDetByEmployee
                            T_General_Hours += itemSDetail.TotalNumberHours
                        Next
                    End If
                End If

                Dim TmpListScheduleDetailHour As New TrackableCollection(Of ScheduleDetailHour)
                If EditFlag = True Then
                    For Each ObjScheduleDetailHour As ScheduleDetailHour In _scheduleDetail.ScheduleDetailHour
                        'If ObjScheduleDetailHour.Event = True Then
                        TmpListScheduleDetailHour.Add(ObjScheduleDetailHour)
                        'End If
                    Next
                Else
                    TmpListScheduleDetailHour = _scheduleDetail.ScheduleDetailHour
                End If

                'Validamos el Dia a registrar de un determinado schedule
                Dim stateValidation As MessageResult = ValidateScheduleDetailInSchedule(_schedule, _DateToSave, Include_Holiday, _validContract, TmpListScheduleDetailHour, schDetByEmployee, _List_Holidays, audit, EditFlag)
                If stateValidation Is Nothing Then
                    Dim _scheduleDetailTemp As ScheduleDetail = New ScheduleDetail
                    With _scheduleDetailTemp
                        .Letter = _scheduleDetail.Letter
                        .ScheduleTemplateId = _scheduleDetail.ScheduleTemplateId
                        .PayrollLiquidationNumber = _scheduleDetail.PayrollLiquidationNumber
                        .DateDetail = _DateToSave
                        .ScheduleFunctionalUnitId = _ScheduleFunctionalUnit.Id
                        Dim newListScheduleDetailHours As TrackableCollection(Of ScheduleDetailHour) = New TrackableCollection(Of ScheduleDetailHour)
                        Dim Num_Hours_Details As Decimal = 0
                        For Each detailHours In _scheduleDetail.ScheduleDetailHour
                            Dim hi As TimeSpan = New TimeSpan(detailHours.DateTimeInitial.Hour, detailHours.DateTimeInitial.Minute, detailHours.DateTimeInitial.Second)
                            Dim he As TimeSpan = New TimeSpan(detailHours.DateTimeEnding.Hour, detailHours.DateTimeEnding.Minute, detailHours.DateTimeEnding.Second)
                            Dim hij As TimeSpan = _validContract.Group.PayrollParameter.InitialTimeOrdinaryDay
                            Dim hej As TimeSpan = _validContract.Group.PayrollParameter.EndTimeOrdinaryDay

                            If he = New TimeSpan(0, 0, 0) Then
                                If detailHours.NextDay = False Then
                                    Num_Hours_Details += 24 - hi.Hours
                                Else
                                    Num_Hours_Details += (he - hi).Hours
                                End If
                            Else
                                'Num_Hours_Details += (he - hi).Hours
                                Num_Hours_Details += (he - hi).TotalHours
                            End If
                            If detailHours.Event = True Then

                                Dim list_ordinary_normal As List(Of GroupEventConcept) = _validContract.Group.GroupEventConcept _
                                                                                                        .Where(Function(x) x.TypeDay = 1 And x.TypeSchedule = 1).ToList
                                Dim list_ordinary_night As List(Of GroupEventConcept) = _validContract.Group.GroupEventConcept _
                                                                                                        .Where(Function(x) x.TypeDay = 1 And x.TypeSchedule = 2).ToList
                                Dim list_holiday_normal As List(Of GroupEventConcept) = _validContract.Group.GroupEventConcept _
                                                                                                        .Where(Function(x) x.TypeDay = 2 And x.TypeSchedule = 1).ToList
                                Dim list_holiday_night As List(Of GroupEventConcept) = _validContract.Group.GroupEventConcept _
                                                                                                        .Where(Function(x) x.TypeDay = 2 And x.TypeSchedule = 2).ToList
                                Dim newSchDetH1 As ScheduleDetailHour = GetSchDetHourInEvents(detailHours,
                                                                       New DateTime(_toRegDate.Year, _toRegDate.Month, _toRegDate.Day,
                                                                           hi.Hours, hi.Minutes, hi.Seconds),
                                                                       New DateTime(_toRegDate.Year, _toRegDate.Month, _toRegDate.Day,
                                                                           hij.Hours, hij.Minutes, hij.Seconds), _List_Holidays)
                                If newSchDetH1.AppliedLiquidationConcept = True Then
                                    For Each schDetCon As GroupEventConcept In list_holiday_night
                                        Dim newschDetConc As ScheduleDetailConcept = New ScheduleDetailConcept()
                                        With newschDetConc
                                            .ConceptId = schDetCon.ConceptId
                                            .ConceptType = 1 ' de dia feriado
                                        End With
                                        newSchDetH1.ScheduleDetailConcept.Add(newschDetConc)
                                    Next
                                Else
                                    For Each schDetCon As GroupEventConcept In list_ordinary_night
                                        Dim newschDetConc As ScheduleDetailConcept = New ScheduleDetailConcept()
                                        With newschDetConc
                                            .ConceptId = schDetCon.ConceptId
                                            .ConceptType = 0 ' de dia ordinario
                                        End With
                                        newSchDetH1.ScheduleDetailConcept.Add(newschDetConc)
                                    Next
                                End If

                                Dim newSchDetH2 As ScheduleDetailHour = GetSchDetHourInEvents(detailHours,
                                                                New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                   hij.Hours, hij.Minutes, hij.Seconds),
                                                                New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                   hej.Hours, hej.Minutes, hej.Seconds), _List_Holidays)
                                newSchDetH2.PaidEventLastMonth = _schedule.Employee.Contract(_schedule.Employee.Contract.Count - 1).Group.NextDateLiquidation
                                newSchDetH2.EventLastMonth = detailHours.EventLastMonth
                                If newSchDetH2.AppliedLiquidationConcept = True Then
                                    For Each schDetCon As GroupEventConcept In list_holiday_normal
                                        Dim newschDetConc As ScheduleDetailConcept = New ScheduleDetailConcept()
                                        With newschDetConc
                                            .ConceptId = schDetCon.ConceptId
                                            .ConceptType = 1 ' de dia feriado
                                        End With
                                        newSchDetH2.ScheduleDetailConcept.Add(newschDetConc)
                                    Next
                                Else
                                    For Each schDetCon As GroupEventConcept In list_ordinary_normal
                                        Dim newschDetConc As ScheduleDetailConcept = New ScheduleDetailConcept()
                                        With newschDetConc
                                            .ConceptId = schDetCon.ConceptId
                                            .ConceptType = 0 ' de dia ordinario
                                        End With
                                        newSchDetH2.ScheduleDetailConcept.Add(newschDetConc)
                                    Next
                                End If

                                Dim newSchDetH3 As ScheduleDetailHour = GetSchDetHourInEvents(detailHours,
                                                                 New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                   hej.Hours, hej.Minutes, hej.Seconds),
                                                                New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                   he.Hours, he.Minutes, he.Seconds), _List_Holidays)
                                If newSchDetH3.AppliedLiquidationConcept = True Then
                                    For Each schDetCon As GroupEventConcept In list_holiday_night
                                        Dim newschDetConc As ScheduleDetailConcept = New ScheduleDetailConcept()
                                        With newschDetConc
                                            .ConceptId = schDetCon.ConceptId
                                            .ConceptType = 1 ' de dia feriado
                                        End With
                                        newSchDetH3.ScheduleDetailConcept.Add(newschDetConc)
                                    Next
                                Else
                                    For Each schDetCon As GroupEventConcept In list_ordinary_night
                                        Dim newschDetConc As ScheduleDetailConcept = New ScheduleDetailConcept()
                                        With newschDetConc
                                            .ConceptId = schDetCon.ConceptId
                                            .ConceptType = 0
                                        End With
                                        newSchDetH3.ScheduleDetailConcept.Add(newschDetConc)
                                    Next
                                End If

                                Dim he_condition
                                If he = New TimeSpan(0, 0, 0) Then
                                    he_condition = New TimeSpan(23, 59, 59)
                                Else
                                    he_condition = he
                                End If
                                If hi < hij Then
                                    If he_condition <= hij Then

                                        'parte en 1, antes del rango de jornada, noctuno
                                        With newSchDetH1
                                            .DateTimeInitial = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                   hi.Hours, hi.Minutes, hi.Seconds)
                                            .DateTimeEnding = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                   he.Hours, he.Minutes, he.Seconds)
                                        End With

                                        newSchDetH1.TotalNumberHours = GetDifBetwenHours(newSchDetH1.DateTimeEnding.TimeOfDay, newSchDetH1.DateTimeInitial.TimeOfDay)

                                        newListScheduleDetailHours.Add(newSchDetH1)

                                    ElseIf he_condition <= hej Then
                                        'parte en 2, antes del rango de jornada y entre el rango de jornada, primero nocturno, y segundo normal
                                        With newSchDetH1
                                            .DateTimeInitial = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                   hi.Hours, hi.Minutes, hi.Seconds)
                                            .DateTimeEnding = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                   hij.Hours, hij.Minutes, hij.Seconds)
                                        End With
                                        newSchDetH1.TotalNumberHours = GetDifBetwenHours(newSchDetH1.DateTimeEnding.TimeOfDay, newSchDetH1.DateTimeInitial.TimeOfDay)
                                        With newSchDetH2
                                            .DateTimeInitial = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                   hij.Hours, hij.Minutes, hij.Seconds)
                                            .DateTimeEnding = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                   he.Hours, he.Minutes, he.Seconds)
                                        End With
                                        newSchDetH2.TotalNumberHours = GetDifBetwenHours(newSchDetH2.DateTimeEnding.TimeOfDay, newSchDetH2.DateTimeInitial.TimeOfDay)
                                        newListScheduleDetailHours.Add(newSchDetH1)
                                        newListScheduleDetailHours.Add(newSchDetH2)

                                    Else
                                        'parte en 3, primero nocturno, segundo normal, tercero nocturno
                                        newSchDetH1.TotalNumberHours = GetDifBetwenHours(newSchDetH1.DateTimeEnding.TimeOfDay, newSchDetH1.DateTimeInitial.TimeOfDay)
                                        newSchDetH2.TotalNumberHours = GetDifBetwenHours(newSchDetH2.DateTimeEnding.TimeOfDay, newSchDetH2.DateTimeInitial.TimeOfDay)
                                        newSchDetH3.TotalNumberHours = GetDifBetwenHours(newSchDetH3.DateTimeEnding.TimeOfDay, newSchDetH3.DateTimeInitial.TimeOfDay)
                                        newListScheduleDetailHours.Add(newSchDetH1)
                                        newListScheduleDetailHours.Add(newSchDetH2)
                                        newListScheduleDetailHours.Add(newSchDetH3)


                                    End If
                                ElseIf hi >= hij And hi < hej Then
                                    If he_condition <= hej Then
                                        'parte en 1, dentro del rango de jornada, dia normal
                                        With newSchDetH2
                                            .DateTimeInitial = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                  hi.Hours, hi.Minutes, hi.Seconds)
                                            .DateTimeEnding = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                   he.Hours, he.Minutes, he.Seconds)
                                        End With
                                        newSchDetH2.TotalNumberHours = GetDifBetwenHours(newSchDetH2.DateTimeEnding.TimeOfDay, newSchDetH2.DateTimeInitial.TimeOfDay)
                                        newListScheduleDetailHours.Add(newSchDetH2)
                                    Else
                                        'parte en 2, dentro del rango, y despues del rango de jornada, primero dia normal, y segundo dia nocturno
                                        With newSchDetH2
                                            .DateTimeInitial = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                  hi.Hours, hi.Minutes, hi.Seconds)
                                            .DateTimeEnding = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                   hej.Hours, hej.Minutes, hej.Seconds)
                                        End With
                                        With newSchDetH3
                                            .DateTimeInitial = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                  hej.Hours, hej.Minutes, hej.Seconds)
                                            .DateTimeEnding = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                                   he.Hours, he.Minutes, he.Seconds)
                                        End With
                                        newSchDetH2.TotalNumberHours = GetDifBetwenHours(newSchDetH2.DateTimeEnding.TimeOfDay, newSchDetH2.DateTimeInitial.TimeOfDay)
                                        newSchDetH3.TotalNumberHours = GetDifBetwenHours(newSchDetH3.DateTimeEnding.TimeOfDay, newSchDetH3.DateTimeInitial.TimeOfDay)
                                        newListScheduleDetailHours.Add(newSchDetH2)
                                        newListScheduleDetailHours.Add(newSchDetH3)
                                    End If
                                Else
                                    'parte en 1, despues del rango de jornada, dia nocturno
                                    With newSchDetH3
                                        .DateTimeInitial = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                              hi.Hours, hi.Minutes, hi.Seconds)
                                        .DateTimeEnding = New DateTime(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day,
                                                               he.Hours, he.Minutes, he.Seconds)
                                    End With
                                    newSchDetH3.TotalNumberHours = GetDifBetwenHours(newSchDetH3.DateTimeEnding.TimeOfDay, newSchDetH3.DateTimeInitial.TimeOfDay)
                                    newListScheduleDetailHours.Add(newSchDetH3)
                                End If

                            Else

                                Dim newDetailHours As New ScheduleDetailHour
                                Dim newListScheduleDetailConcept As TrackableCollection(Of ScheduleDetailConcept) = New TrackableCollection(Of ScheduleDetailConcept)

                                For Each detailConcept In detailHours.ScheduleDetailConcept
                                    Dim newDetailConcept As ScheduleDetailConcept = New ScheduleDetailConcept
                                    With newDetailConcept
                                        .ConceptId = detailConcept.ConceptId
                                        .ConceptType = detailConcept.ConceptType
                                        newListScheduleDetailConcept.Add(newDetailConcept)
                                    End With
                                Next
                                With newDetailHours ' cargo los datos de los detalles de horas
                                    .DateTimeEnding = New Date(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day, detailHours.DateTimeEnding.Hour, detailHours.DateTimeEnding.Minute, detailHours.DateTimeEnding.Second)
                                    .DateTimeInitial = New Date(_DateToSave.Year, _DateToSave.Month, _DateToSave.Day, detailHours.DateTimeInitial.Hour, detailHours.DateTimeInitial.Minute, detailHours.DateTimeInitial.Second)
                                    .NextDay = detailHours.NextDay
                                    .TotalNumberHours = detailHours.TotalNumberHours
                                    .Event = detailHours.Event
                                    .ScheduleDetailConcept = newListScheduleDetailConcept
                                    If .NextDay = True Then
                                        If _List_Holidays.Find(Function(x) x.Holiday1 = .DateTimeInitial.Date.AddDays(1)) _
                                            IsNot Nothing Or Weekday(.DateTimeInitial.Date.AddDays(1), FirstDayOfWeek.Sunday) = 1 Then
                                            .AppliedLiquidationConcept = True
                                        Else
                                            .AppliedLiquidationConcept = False
                                        End If
                                    Else
                                        If _List_Holidays.Find(Function(x) x.Holiday1 = .DateTimeInitial.Date) _
                                            IsNot Nothing Or Weekday(.DateTimeInitial.Date, FirstDayOfWeek.Sunday) = 1 Then
                                            .AppliedLiquidationConcept = True
                                        Else
                                            .AppliedLiquidationConcept = False
                                        End If
                                    End If

                                End With
                                newListScheduleDetailHours.Add(newDetailHours)
                            End If

                        Next
                        T_General_Hours += Num_Hours_Details ' totalizar las horas del schedule general, con estas horas recien ingresadas
                        .ScheduleDetailHour = newListScheduleDetailHours
                        .TotalNumberHours = Num_Hours_Details
                    End With
                    With _scheduleDetailTemp
                        .EmployeeId = _schedule.EmployeeId
                        .ContractId = _validContract.Id
                        .FunctionalUnitId = _validContract.FunctionalUnitId
                        .BranchOfficeId = FunctionalUnitTemp.BranchOfficeId
                        .CompanyId = FunctionalUnitTemp.BranchOffice.CompanyId
                        .GroupId = _validContract.GroupId
                        .CenterCostId = FunctionalUnitTemp.CostCenterId
                    End With
                    'Valido que no exceda las horas maximas del contrato
                    If T_General_Hours <= _validContract.Position.MaxHourAmount Then

                        Dim _type As Type = _schedule.GetType()
                        Dim _Property_Name As String = String.Empty
                        If _DateToSave.Day = 1 Then
                            _Property_Name = "ScheduleDetail"
                        Else
                            _Property_Name = "ScheduleDetail" & (_DateToSave.Day) - 1
                        End If
                        Dim _Property = _type.GetProperty(_Property_Name)
                        _Property.SetValue(_schedule, _scheduleDetailTemp)
                    Else
                        Dim newMessageResult As MessageResult = New MessageResult("-008", "", "", "", "", "")
                        With newMessageResult
                            .Parameters(0) = _schedule.Employee.ThirdParty.Name
                            .Parameters(1) = _DateToSave.Day
                            .Parameters(2) = _DateToSave.Month
                            .Parameters(3) = _DateToSave.Year
                            .Parameters(4) = _scheduleDetail.ScheduleTemplateId
                        End With
                        _listScheduleWithoutSave.Add(newMessageResult) 'Exede las horas maximas de contrato
                    End If
                Else
                    'guardar dias que no registroooo
                    With stateValidation
                        .Parameters(0) = _schedule.Employee.ThirdParty.Name
                        .Parameters(1) = _DateToSave.Day
                        .Parameters(2) = _DateToSave.Month
                        .Parameters(3) = _DateToSave.Year
                        .Parameters(4) = _scheduleDetail.ScheduleTemplateId
                    End With
                    _listScheduleWithoutSave.Add(stateValidation)
                End If
            Next
        Next
        Dim newActiMessRes As New ActionMessageResult(Of List(Of Schedule))
        newActiMessRes.ObjectEmbbeded = _listScheduleToSave
        newActiMessRes.MessageResult = _listScheduleWithoutSave
        newActiMessRes.StateResult = True
        Return newActiMessRes
    End Function

    ''' <summary>
    ''' Metodo para obtener diferencia entre dos horas
    ''' </summary>
    ''' <param name="hEnding"></param>
    ''' <param name="hInitial"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDifBetwenHours(hEnding As TimeSpan, hInitial As TimeSpan) As Decimal
        If hEnding = TimeSpan.Zero Then
            hEnding = TimeSpan.FromHours(24)
        End If
        Return CDec((hEnding - hInitial).TotalHours)
    End Function

    ''' <summary>
    ''' Funcion para validar un nuevo registro de turno(Schedule Detail) que se va a ingresar
    ''' </summary>
    ''' <param name="_schedule">El schedule al que se le registrara el schedule detail</param>
    ''' <param name="day">el dia a regsitrar</param>
    ''' <param name="Include_Holidays">Indica si se tienen en cuenta los domingos y feriados</param>
    ''' <param name="_validContract">El contrato valido</param>
    ''' <param name="detHours">listado de detalle de horas que tiene el turno</param>
    ''' <param name="ScheduleInPeriod">Listado de scheduledetails que tenga el empleado</param>
    ''' <param name="List_Holiday">Listado de festivos para ese periodo</param>
    ''' <returns>El messege result</returns>
    ''' <remarks></remarks>
    Public Function ValidateScheduleDetailInSchedule(_schedule As Schedule, day As Date, Include_Holidays As Boolean, _validContract As Contract, detHours As TrackableCollection(Of ScheduleDetailHour), ScheduleInPeriod As List(Of ScheduleDetail), List_Holiday As List(Of Domain.Entities.Holiday), audit As AuditMessage, Optional EditFlag As Boolean = False) As Domain.Base.Entities.MessageResult Implements IScheduleDomain.ValidateScheduleDetailInSchedule
        ValidateScheduleDetailInSchedule = Nothing
        Dim _thisDay As Date = day
        If Include_Holidays = False Then 'valido los festivos parametrizados
            If List_Holiday.Find(Function(x) x.Holiday1 = _thisDay) IsNot Nothing Then
                ValidateScheduleDetailInSchedule = New Domain.Base.Entities.MessageResult("-001", "", "", "", "", "") ' Error de festivo
                Exit Function
            End If
            If Weekday(_thisDay, FirstDayOfWeek.Sunday) = 1 Then ' valido los domingos
                ValidateScheduleDetailInSchedule = New Domain.Base.Entities.MessageResult("-001", "", "", "", "", "") ' Error de festivo
                Exit Function
            End If
        End If
        If day < _validContract.JobBondingDate Or day > _validContract.ContractEndingDate Then ' Valido que la fecha este entre el día del contrato
            ValidateScheduleDetailInSchedule = New Domain.Base.Entities.MessageResult("-002", "", "", "", "", "") 'No tiene contrato
            Exit Function
        End If

        ' Valido que no exista ya un ScheduleDetail para este día en la misma unidad funcional, sin importar si las horas se cruzan.
        ' Antes solo se validaba el cruce de horario (-006/-007 más abajo), lo que permitía crear un segundo ScheduleDetail para un día
        ' que ya tenía turno cuando el horario nuevo no chocaba con el existente, dejando el turno viejo huérfano (Schedule.Dxx pasaba
        ' a apuntar solo al nuevo, sin borrar el anterior).
        If ScheduleInPeriod IsNot Nothing AndAlso ScheduleInPeriod.Any(Function(x) x.DateDetail = day And x.ScheduleFunctionalUnitId = _schedule.FunctionalUnitId) Then
            ValidateScheduleDetailInSchedule = New Domain.Base.Entities.MessageResult("-007", "", "", "", "", "") ' ya hay un detalle de schedule para ese dia
            Exit Function
        End If

        If audit.CompanyType = 1 Then
            If Validate18Hours(ScheduleInPeriod, detHours) = False Then
                ValidateScheduleDetailInSchedule = New Domain.Base.Entities.MessageResult("-011", "", "", "", "", "") 'Se supera el numero maximo de horas continuas permitidas (18)
                Exit Function
            End If
        End If

        If ValidateScheduleDetailInSchedule Is Nothing Then ' valido que no se crucen las horas
            If ScheduleInPeriod IsNot Nothing AndAlso ScheduleInPeriod.Count > 0 Then
                For Each schDet As ScheduleDetail In ScheduleInPeriod.FindAll(Function(x) x.DateDetail = day Or x.DateDetail = day.AddDays(-1) Or x.DateDetail = day.AddDays(1)) ' Busco solo si ya ha tenido turnos el dia anterior, el actual y el posterior a el que se va a registrar, para validar que no se cruce los horarios
                    Dim endDate As Date? = Nothing
                    Dim totalHoursToValidate As Double = 0
                    'Validar Novedades
                    For Each itemH As ScheduleDetailHour In detHours

                        If itemH.DateTimeInitial = endDate Or endDate Is Nothing Then
                            totalHoursToValidate = totalHoursToValidate + itemH.TotalNumberHours
                            endDate = itemH.DateTimeEnding
                        Else
                            totalHoursToValidate = 0
                        End If

                        Dim DiH As Date = day
                        Dim DeH As Date = day
                        Dim TiH As TimeSpan = New TimeSpan(itemH.DateTimeInitial.Hour, itemH.DateTimeInitial.Minute, itemH.DateTimeInitial.Second)
                        Dim TeH As TimeSpan = New TimeSpan(itemH.DateTimeEnding.Hour, itemH.DateTimeEnding.Minute, itemH.DateTimeEnding.Second)
                        If itemH.NextDay = True Then ' se suma un dia a la dia si es de un siguiente dia, para tener la fecha exacta
                            DiH = DiH.AddDays(1)
                            DeH = DeH.AddDays(1)
                        End If
                        Dim schDetValidNovelty As ScheduleDetail = ScheduleInPeriod.Find(Function(x) x.DateDetail = DiH.Date And x.TotalNumberHours = 0) ' Validar si hay novedad para ese día
                        If schDetValidNovelty IsNot Nothing Then
                            If schDetValidNovelty.TotalNumberHours = 0 Then
                                Select Case schDetValidNovelty.Letter
                                    Case Is = "I"
                                        ValidateScheduleDetailInSchedule = New Domain.Base.Entities.MessageResult("-003", "", "", "", "", "") ' hay incapacidad
                                        Exit Function
                                    Case Is = "L"
                                        ValidateScheduleDetailInSchedule = New Domain.Base.Entities.MessageResult("-004", "", "", "", "", "") ' hay licencia
                                        Exit Function
                                    Case Is = "S"
                                        ValidateScheduleDetailInSchedule = New Domain.Base.Entities.MessageResult("-005", "", "", "", "", "") ' hay sanción
                                        Exit Function
                                    Case Is = "V"
                                        ValidateScheduleDetailInSchedule = New Domain.Base.Entities.MessageResult("-009", "", "", "", "", "") ' hay vacaciones
                                        Exit Function
                                    Case Is = "PV"
                                        ValidateScheduleDetailInSchedule = New Domain.Base.Entities.MessageResult("-010", "", "", "", "", "") ' hay permiso de vacaciones
                                        Exit Function
                                End Select
                            End If
                        End If
                    Next
                    Dim endDateAux As Date? = Nothing
                    Dim totalHoursToValidateAux As Double = 0
                    Dim endDatePlus As Date? = Nothing
                    'Dim enddatePlusAux As Date
                    For Each itemSH As ScheduleDetailHour In schDet.ScheduleDetailHour 'recorro lo que encontre para compararlos con los que agregare

                        If itemSH.DateTimeInitial = endDateAux OrElse endDateAux Is Nothing Then
                            totalHoursToValidateAux = totalHoursToValidateAux + itemSH.TotalNumberHours
                            endDateAux = itemSH.DateTimeEnding

                        Else
                            totalHoursToValidateAux = 0
                        End If




                        Dim Di As New Date(itemSH.DateTimeInitial.Year, itemSH.DateTimeInitial.Month, itemSH.DateTimeInitial.Day)
                        Dim De As New Date(itemSH.DateTimeEnding.Year, itemSH.DateTimeEnding.Month, itemSH.DateTimeEnding.Day)
                        Dim Ti As TimeSpan = New TimeSpan(itemSH.DateTimeInitial.Hour, itemSH.DateTimeInitial.Minute, itemSH.DateTimeInitial.Second)
                        Dim Te As TimeSpan = New TimeSpan(itemSH.DateTimeEnding.Hour, itemSH.DateTimeEnding.Minute, itemSH.DateTimeEnding.Second)
                        If itemSH.NextDay = True Then ' se suma un dia a la dia si es de un siguiente dia, para tener la fecha exacta
                            Di = Di.AddDays(1)
                            De = De.AddDays(1)
                        End If
                        For Each itemH As ScheduleDetailHour In detHours ' se recorren los detalles de hora de los que van a agregar
                            Dim DiH As Date = day
                            Dim DeH As Date = day
                            Dim TiH As TimeSpan = New TimeSpan(itemH.DateTimeInitial.Hour, itemH.DateTimeInitial.Minute, itemH.DateTimeInitial.Second)
                            Dim TeH As TimeSpan = New TimeSpan(itemH.DateTimeEnding.Hour, itemH.DateTimeEnding.Minute, itemH.DateTimeEnding.Second)
                            If itemH.NextDay = True Then ' se suma un dia a la dia si es de un siguiente dia, para tener la fecha exacta
                                DiH = DiH.AddDays(1)
                                DeH = DeH.AddDays(1)

                                'If itemH.Event = True Then
                                '    DiH = DiH.AddDays(1)
                                '    DeH = DeH.AddDays(1)
                                'End If
                            End If

                            If Di.Date = DiH.Date Then ' Se Entra a verificar las horas, solo si los detalles a comparar son del mismo dia
                                For i As Integer = 1 To (TeH - TiH).Hours ' Valido si cualquiera de los horarios se cruza con otros
                                    Dim _hour As TimeSpan = TiH.Add(New TimeSpan(i, 0, 0))
                                    If _hour > Ti And _hour <= Te And audit.CompanyType = 1 Then
                                        ValidateScheduleDetailInSchedule = New Domain.Base.Entities.MessageResult("-006", "", "", "", "", "") 'Se cruza el horario con otro schedule
                                        Exit Function
                                    End If
                                Next
                            End If

                            Dim schDetValidOtherScheduleDetail As ScheduleDetail = ScheduleInPeriod.Where(Function(x) x.ScheduleFunctionalUnitId = _schedule.FunctionalUnitId And x.ScheduleDetailHour.ToList().Any(Function(y) y.DateTimeInitial.Date >= DiH.Date And y.DateTimeEnding.Date <= DeH.Date And y.DateTimeInitial.TimeOfDay >= TiH And y.DateTimeEnding.TimeOfDay <= TeH)).FirstOrDefault()

                            If schDetValidOtherScheduleDetail IsNot Nothing Then
                                If schDetValidOtherScheduleDetail.ScheduleDetailHour.Any(Function(x) x.NextDay = True) Then
                                    For Each objScheduleDetailHour As ScheduleDetailHour In schDetValidOtherScheduleDetail.ScheduleDetailHour
                                        If objScheduleDetailHour.NextDay = True Then
                                            Dim CompareInitialDate = objScheduleDetailHour.DateTimeInitial.AddDays(1)
                                            Dim CompareEndDate = objScheduleDetailHour.DateTimeEnding.AddDays(1)
                                            If CompareInitialDate >= DiH.Date And CompareEndDate <= DeH.Date And CompareInitialDate.TimeOfDay >= TiH And CompareEndDate.TimeOfDay <= TeH Then
                                                ValidateScheduleDetailInSchedule = New Domain.Base.Entities.MessageResult("-007", "", "", "", "", "") 'ya hay un detalle de schedule para ese dia
                                                Exit Function
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                        Next

                    Next

                Next
            End If
        End If
    End Function

    ''' <summary>
    ''' Funcion que retorna un schedule detail armado, cuando se quieren cortar por ser eventos
    ''' </summary>
    ''' <param name="detailHours">detalle de horas general</param>
    ''' <param name="DtInitial">Fecha Hora, Inicial</param>
    ''' <param name="DtEnding">Fecha Hora, Fin</param>
    ''' <param name="_List_Holidays">Listado de festivos para este periodo</param>
    ''' <returns>El schedule detail hora</returns>
    Public Function GetSchDetHourInEvents(detailHours As ScheduleDetailHour, DtInitial As DateTime, DtEnding As DateTime, _List_Holidays As List(Of Domain.Entities.Holiday)) As ScheduleDetailHour Implements IScheduleDomain.GetSchDetHourInEvents
        Dim newDschDetHour As ScheduleDetailHour = New ScheduleDetailHour()

        If detailHours.NextDay = True Then
            DtInitial = DtInitial.AddDays(1)
            DtEnding = DtEnding.AddDays(1)
        End If

        With newDschDetHour
            .Event = True
            .DateTimeInitial = DtInitial
            '.DateTimeEnding = DtEnding
            .DateTimeEnding = DtEnding
            .NextDay = detailHours.NextDay
            If .DateTimeEnding.TimeOfDay = New TimeSpan(0, 0, 0) Then
                .TotalNumberHours = 24 - .DateTimeInitial.Hour
            Else
                .TotalNumberHours = .DateTimeEnding.Hour - .DateTimeInitial.Hour
            End If

            If _List_Holidays.Find(Function(x) x.Holiday1 = .DateTimeInitial.Date) IsNot Nothing Or Weekday(.DateTimeInitial.Date, FirstDayOfWeek.Sunday) = 1 Then
                .AppliedLiquidationConcept = True
            Else
                .AppliedLiquidationConcept = False
            End If
        End With
        Return newDschDetHour
    End Function

    Public Function Validate18Hours(schedulePeriod As List(Of ScheduleDetail), scheduleDetail As TrackableCollection(Of ScheduleDetailHour)) As Boolean

        If scheduleDetail.Sum(Function(x) x.TotalNumberHours) > 18 Then
            Return False
        End If

        If scheduleDetail.Any(Function(x) x.Event = True And x.TotalNumberHours = 0) Then
            Dim TotalHours = 0

            For Each objScheduleDetailHour As ScheduleDetailHour In scheduleDetail

                If objScheduleDetailHour.TotalNumberHours > 0 Then
                    TotalHours = TotalHours + objScheduleDetailHour.TotalNumberHours
                Else
                    Dim tmpHours As Integer = Math.Abs(DateDiff(DateInterval.Hour, objScheduleDetailHour.DateTimeEnding, objScheduleDetailHour.DateTimeInitial))
                    TotalHours = TotalHours + tmpHours
                End If
            Next

            If TotalHours > 18 Then
                Return False
            End If
        End If

        If schedulePeriod IsNot Nothing AndAlso schedulePeriod.Count > 0 Then
            Dim HourEnding As Date?
            Dim InsertHourEnding As Date?
            Dim InsertHourInitial As Date?
            Dim TotalHour As Integer = 0
            Dim TotalInsertHour As Integer = 0
            Dim CompareHourEnding As Date?
            'Evalúo con el día anterior
            For Each schDet As ScheduleDetail In schedulePeriod.FindAll(Function(x) x.DateDetail.Date = scheduleDetail.FirstOrDefault.DateTimeInitial.AddDays(-1).Date) ' Busco solo si ya ha tenido turnos el dia anterior, el actual y el posterior a el que se va a registrar, para validar que no se cruce los horarios
                If schDet.ScheduleDetailHour.Any(Function(x) x.NextDay = True) Then

                    Dim ListAnalist = schDet.ScheduleDetailHour.OrderBy(Function(x) x.DateTimeInitial)

                    For Each schDetHour As ScheduleDetailHour In ListAnalist.OrderBy(Function(x) x.NextDay)

                        If HourEnding Is Nothing Then
                            HourEnding = schDetHour.DateTimeEnding
                            TotalHour = schDetHour.TotalNumberHours
                        Else
                            If schDetHour.DateTimeInitial = HourEnding Then
                                TotalHour = TotalHour + schDetHour.TotalNumberHours
                            Else
                                TotalHour = 0
                                TotalHour = TotalHour + schDetHour.TotalNumberHours
                            End If
                            HourEnding = schDetHour.DateTimeEnding

                        End If
                    Next

                    Dim CompareDate = HourEnding.Value.AddDays(1)

                    Dim TotalHourEvent As Integer = 0
                    For Each schDetHourInsert As ScheduleDetailHour In scheduleDetail.OrderBy(Function(x) x.DateTimeInitial)

                        If schDetHourInsert.TotalNumberHours = 0 And schDetHourInsert.Event = True Then
                            TotalHourEvent = Math.Abs(DateDiff(DateInterval.Hour, schDetHourInsert.DateTimeEnding, schDetHourInsert.DateTimeInitial))
                        End If

                        If InsertHourEnding Is Nothing Then
                            If CompareDate.Date <> schDetHourInsert.DateTimeInitial.Date Then
                                Exit For
                            End If

                            If CompareDate.Hour <> schDetHourInsert.DateTimeInitial.Hour Then
                                Exit For
                            End If

                            InsertHourEnding = schDetHourInsert.DateTimeEnding
                            TotalInsertHour = schDetHourInsert.TotalNumberHours
                            InsertHourInitial = schDetHourInsert.DateTimeInitial

                        Else
                            If schDetHourInsert.DateTimeInitial = InsertHourEnding Then
                                TotalInsertHour = TotalInsertHour + schDetHourInsert.TotalNumberHours + TotalHourEvent
                            Else
                                Exit For
                            End If
                            InsertHourEnding = schDetHourInsert.DateTimeEnding
                        End If
                    Next

                    If TotalHour + TotalInsertHour > 18 Then
                        Return False
                    End If
                End If

            Next


            Dim InsertHourEndingNext As Date?
            Dim TotalInsertHourNext As Integer = 0

            Dim TotalHourNext As Integer = 0
            Dim HourEndingNow As Date?
            'Evalúo con el día próximo dia
            If scheduleDetail.Any(Function(x) x.NextDay = True) Then
                For Each objScheduleDetailHour As ScheduleDetailHour In scheduleDetail
                    If HourEndingNow Is Nothing Then
                        HourEndingNow = objScheduleDetailHour.DateTimeEnding
                        TotalHourNext = objScheduleDetailHour.TotalNumberHours
                    Else
                        If objScheduleDetailHour.DateTimeInitial = HourEndingNow Then
                            TotalHourNext = TotalHourNext + objScheduleDetailHour.TotalNumberHours
                        Else
                            TotalHourNext = 0
                            TotalHourNext = TotalHourNext + objScheduleDetailHour.TotalNumberHours
                        End If
                        HourEndingNow = objScheduleDetailHour.DateTimeEnding

                    End If
                Next


                For Each schDet As ScheduleDetail In schedulePeriod.FindAll(Function(x) x.DateDetail.Date = scheduleDetail.FirstOrDefault.DateTimeInitial.AddDays(1).Date) ' Busco solo si ya ha tenido turnos el dia anterior, el actual y el posterior a el que se va a registrar, para validar que no se cruce los horarios

                    For Each schDetHourInsert As ScheduleDetailHour In schDet.ScheduleDetailHour
                        If InsertHourEndingNext Is Nothing Then
                            If HourEndingNow <> schDetHourInsert.DateTimeInitial Then
                                Exit For
                            End If
                            InsertHourEndingNext = schDetHourInsert.DateTimeEnding
                            TotalInsertHourNext = schDetHourInsert.TotalNumberHours
                        Else
                            If schDetHourInsert.DateTimeInitial = InsertHourEndingNext Then
                                TotalInsertHourNext = TotalInsertHourNext + schDetHourInsert.TotalNumberHours
                            Else
                                Exit For
                            End If
                            InsertHourEndingNext = schDetHourInsert.DateTimeEnding
                        End If
                    Next

                Next

                If TotalHourNext + TotalInsertHourNext > 18 Then
                    Return False
                End If

            End If

        End If

        Return True
    End Function


#Region "Delete"
    ''' <summary>
    ''' Agrupa los detalles por empleado y los devuelve en un diccionario
    ''' </summary>
    ''' <param name="listDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GroupScheduleDetailByEmployee(listDetail As List(Of ScheduleDetail)) As Dictionary(Of Integer, List(Of ScheduleDetail)) Implements IScheduleDomain.GroupScheduleDetailByEmployee
        Dim dictionaryDetail As New Dictionary(Of Integer, List(Of ScheduleDetail))
        For Each detail As ScheduleDetail In listDetail
            If dictionaryDetail.ContainsKey(detail.EmployeeId) Then
                dictionaryDetail(detail.EmployeeId).Add(detail)
            Else
                Dim listTmp As New List(Of ScheduleDetail)
                listTmp.Add(detail)
                dictionaryDetail.Add(detail.EmployeeId, listTmp)
            End If
        Next
        Return dictionaryDetail
    End Function

    ''' <summary>
    ''' Marca como eliminados los detalles y actualiza las horas del schedule
    ''' </summary>
    ''' <param name="schedule"></param>
    ''' <param name="listDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteScheduleDetail(schedule As Schedule, listDetail As List(Of ScheduleDetail)) As Object Implements IScheduleDomain.DeleteScheduleDetail
        Dim type As Type = schedule.GetType()
        Dim hoursInitial = schedule.TotalHour
        'schedule.StartTracking()
        For Each item As ScheduleDetail In listDetail
            Dim strProperty As String = "ScheduleDetail"
            If item.DateDetail.Day > 1 Then
                strProperty &= (item.DateDetail.Day - 1).ToString()
            End If
            Dim ObjProperty = type.GetProperty(strProperty)
            ObjProperty.SetValue(schedule, Nothing)
            schedule.TotalHour -= item.TotalNumberHours
        Next
        If schedule.TotalHour <> hoursInitial Then
            schedule.MarkAsModified()
        End If
        If schedule.TotalHour = 0 Then
            Dim banMarkAsDelete = True 'bandera para saber si se elimna todo el shcedule o no
            'si el numero de horas de este schedule es 0 y no tiene novedades en el mes, se le elimina el reg schedule
            Dim _type As Type = schedule.GetType()
            Dim _Property_Name As String = String.Empty
            For i As Integer = 1 To 31
                If i = 1 Then
                    _Property_Name = "ScheduleDetail"
                Else
                    _Property_Name = "ScheduleDetail" & (i) - 1
                End If
                Dim _Property = _type.GetProperty(_Property_Name)
                Dim _schDet As ScheduleDetail = CType(_Property.GetValue(schedule), ScheduleDetail)
                If _schDet IsNot Nothing Then
                    banMarkAsDelete = False
                End If
            Next
            If banMarkAsDelete = True Then
                schedule.MarkAsDeleted()
            End If
        End If

        Return schedule
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ScheduleDetailRepository = Nothing
            _ScheduleRepositoryCommit = Nothing
            _ScheduleRepository = Nothing
            _FunctionalUnitRepository = Nothing
            _employeeRepository = Nothing
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




