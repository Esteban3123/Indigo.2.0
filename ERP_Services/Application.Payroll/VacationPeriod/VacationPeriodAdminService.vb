'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 24-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Transactions
Imports Application.Accounting
Imports Application.Base
Imports Application.Treasury
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.Data.ModelRepository

Public Class VacationPeriodAdminService
    Implements IVacationPeriodAdminService

    ''' <summary>
    ''' Repositorio de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _vacationRepository As IVacationRepository

    ''' <summary>
    ''' Repositorio de empleado
    ''' </summary>
    Private _employeeRepository As IEmployeeRepository

    ''' <summary>
    ''' Repositorio de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _employeeRepositoryCommit As IEmployeeRepository
    ''' <summary>
    ''' Repositorio de periodos de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _vacationPeriodRepository As IVacationPeriodRepository

    ''' <summary>
    ''' Dominio de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _vacationDomain As IVacationPeriodDomain
    ''' <summary>
    ''' Repositorio de los dias festivos
    ''' </summary>
    ''' <remarks></remarks>
    Private _holidayRepository As IHolidayRepository

    ''' <summary>
    ''' Repositorio de liquidacion de nomina
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Repositorio de calendario detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleDetailRepository As IScheduleDetailRepository

    ''' <summary>
    ''' Repositorio de calendario detalle para hacer commit
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleDetailRepositoryCommit As IScheduleDetailRepository

    ''' <summary>
    ''' Repositorio de novedades
    ''' </summary>
    ''' <remarks></remarks>
    Private _noveltyRepository As INoveltyRepository

    ''' <summary>
    ''' Repositorio de calendario
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleRepository As IScheduleRepository

    ''' <summary>
    ''' Repositorio de calendario para Commit
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleRepositoryCommit As IScheduleRepository

    ''' <summary>
    ''' Dominio del novedades
    ''' </summary>
    ''' <remarks></remarks>
    Private _noveltyDomain As INoveltyDomain

    ''' <summary>
    ''' Repositorio de schedule detail hours
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleDetailHourRepository As IScheduleDetailHourRepository

    ''' <summary>
    ''' Repositorio de schedule detail concept
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleDetailConceptRepository As IScheduleDetailConceptRepository

    ''' <summary>
    ''' Repositorio de Embargos
    ''' </summary>
    Private _foreclousureRepository As IForeclousureRepository

    ''' <summary>
    ''' Repositorio de Convenios
    ''' </summary>
    Private _agreementsCRepository As IAgreementsCRepository

    ''' <summary>
    ''' Repositorio de Grupos
    ''' </summary>
    Private _groupRepository As IGroupRepository

    ''' <summary>
    ''' Repositorio de Servicios de Nómina
    ''' </summary>
    Private _domainPayroll As Domain.Payroll.ILiquidationDomain

    ''' <summary>
    ''' Aplicación de Documentos Contables
    ''' </summary>
    ''' <remarks></remarks>
    Private _AccountingDocumentAdmin As IAccountingDocumentAdminService

    ''' <summary>
    ''' Repositorio para comprobantes de egresos
    ''' </summary>
    Private _voucherTransactionAdmin As IVoucherTransactionAdminService

    ''' <summary>
    ''' REpositorio de Secuencias de Tesoreria
    ''' </summary>
    Private _sequenseTreasuryRepository As SequenseTreasuryCRepository

    ''' <summary>
    ''' Repositorio de Parámetros de Nómina
    ''' </summary>
    ''' <remarks></remarks>
    Private _payrollSettings As IPayrollSettingsRepository


    Public Sub New(ByVal repository As IVacationRepository, employeeRepository As IEmployeeRepository, vacationDomain As IVacationPeriodDomain,
                   holidayRepository As IHolidayRepository, liquidationRepository As IPayrollLiquidationRepository, vacationPeriodRepository As IVacationPeriodRepository,
                   employeeRepositoryCommit As IEmployeeRepository, scheduleDetailRepository As IScheduleDetailRepository, noveltyRepository As INoveltyRepository _
                   , scheduleRepository As IScheduleRepository, noveltyDomain As INoveltyDomain, scheduleDetailRepositoryCommit As IScheduleDetailRepository,
                   scheduleDetailHourRepository As IScheduleDetailHourRepository, scheduleDetailConceptRepository As IScheduleDetailConceptRepository,
                   agreementsCRepository As IAgreementsCRepository, foreclousureRepository As IForeclousureRepository, groupRepository As IGroupRepository, domainPayroll As Domain.Payroll.ILiquidationDomain,
                   scheduleCommitRepository As IScheduleRepository, AccountingDocumentAdmin As IAccountingDocumentAdminService, voucherTransactionAdmin As IVoucherTransactionAdminService,
                   payrollSettings As IPayrollSettingsRepository, sequenseTreasuryRepository As SequenseTreasuryCRepository)
        If (repository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de vacaciones")
        End If
        _vacationRepository = repository
        _employeeRepository = employeeRepository
        _vacationDomain = vacationDomain
        _holidayRepository = holidayRepository
        _liquidationRepository = liquidationRepository
        _vacationPeriodRepository = vacationPeriodRepository
        _employeeRepositoryCommit = employeeRepositoryCommit
        _scheduleDetailRepository = scheduleDetailRepository
        _noveltyRepository = noveltyRepository
        _scheduleRepository = scheduleRepository
        _noveltyDomain = noveltyDomain
        _scheduleDetailRepositoryCommit = scheduleDetailRepositoryCommit
        _scheduleDetailHourRepository = scheduleDetailHourRepository
        _scheduleDetailConceptRepository = scheduleDetailConceptRepository
        _agreementsCRepository = agreementsCRepository
        _foreclousureRepository = foreclousureRepository
        _groupRepository = groupRepository
        _domainPayroll = domainPayroll
        _scheduleRepositoryCommit = scheduleCommitRepository
        _AccountingDocumentAdmin = AccountingDocumentAdmin
        _voucherTransactionAdmin = voucherTransactionAdmin
        _sequenseTreasuryRepository = sequenseTreasuryRepository
        _payrollSettings = payrollSettings
    End Sub


    ''' <summary>
    ''' Elimina una vacacion
    ''' </summary>
    ''' <param name="vacation">Vacacion</param>
    ''' <returns></returns>
    Public Function DeleteVacationPeriod(vacation As Domain.Payroll.Entities.VacationPeriod, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IVacationPeriodAdminService.DeleteVacationPeriod
        If vacation Is Nothing Then
            Throw New ArgumentNullException("La vacacion no puede ser vacia")
        End If
        Dim unitWork As IUnitWork = _vacationPeriodRepository.UnitWork
        Try
            _vacationPeriodRepository.DeleteEntity(vacation)
            unitWork.Commit()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene las vacaciones que tenga solicitadas o pagas un empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationPeriodByEmployee(employeeId As Integer) As List(Of Domain.Payroll.Entities.VacationPeriod) Implements IVacationPeriodAdminService.GetVacationPeriodByEmployee
        Try
            Return _vacationPeriodRepository.GetVacationPeriodByEmployee(employeeId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of VacationPeriod)()
        End Try
    End Function

    ''' <summary>
    ''' Guarda o edita una vacacion
    ''' </summary>
    ''' <param name="vacation">vacacion</param>
    ''' <returns></returns>
    Public Function SaveVacationPeriod(vacation As Domain.Payroll.Entities.VacationPeriod, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IVacationPeriodAdminService.SaveVacationPeriod
        If vacation Is Nothing Then
            Throw New ArgumentNullException("La vacacion no puede ser vacia")
        End If
        Dim unitWork As IUnitWork = _vacationPeriodRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of VacationPeriod)
            Dim auxVacation As Domain.Payroll.Entities.VacationPeriod
            Dim status As Integer

            If vacation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                vacation.ModificationUser = audit.CodeUser
                vacation.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxVacation = New VacationPeriod()
            Else
                vacation.CreationUser = audit.CodeUser
                vacation.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _vacationPeriodRepository.SaveEntity(vacation)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of VacationPeriod)(vacation, audit, status, auxVacation)
            auditProcess.Execute()
            Return True

            _vacationPeriodRepository.SaveEntity(vacation)
            unitWork.Commit()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Lista todas la vacaciones que tiene el empleado y le agrega los periodos que se le debe
    ''' </summary>
    ''' <param name="choice">opcion de filtro 1 - Empleado 2 - Grupo</param>
    ''' <param name="value">Valor del filtro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListVacationPeriodByFilter(choice As Byte, value As String) As List(Of Domain.Payroll.Entities.Employee) Implements IVacationPeriodAdminService.ListVacationPeriodByFilter
        If value Is Nothing Or value = "" Then
            Throw New ArgumentNullException("Valor no puede ser vacio")
        End If
        Dim listEmployee As List(Of Domain.Payroll.Entities.Employee) = New List(Of Domain.Payroll.Entities.Employee)()
        'Dim listVacation As List(Of VacationPeriod) = New List(Of VacationPeriod)()
        Try
            If choice = 1 Then
                Dim employee = _employeeRepository.GetEmployeeById(CType(value, Integer))
                listEmployee.Add(employee)
            Else
                listEmployee = _employeeRepository.GetEmployeeByGroup(CType(value, Integer))
            End If
            listEmployee = _vacationDomain.LoadVacationPeriod(listEmployee)
            Return listEmployee
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Domain.Payroll.Entities.Employee)()
        End Try
    End Function

    ''' <summary>
    ''' Calcula las fechas de fin e ingreso de vacaciones
    ''' </summary>
    ''' <param name="EmployeeId"></param>
    ''' <param name="RequestDay"></param>
    ''' <param name="InitialDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateDatesResumption(EmployeeId As Integer, RequestDay As Integer, InitialDate As Date) As ActionResult(Of Tuple(Of Date, Date)) Implements IVacationPeriodAdminService.CalculateDatesResumption
        Try
            Return _vacationDomain.CalculateDatesResumption(EmployeeId, RequestDay, InitialDate)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Tuple(Of Date, Date)) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para solicitar las vacaciones de uno o varios empleados
    ''' </summary>
    ''' <param name="employees">Lista de empleados</param>
    ''' <param name="requestDays">Dias Solicitados</param>
    ''' <param name="typeCalculate">Tipo de calculo 1 - Promedio 2 - Sueldo Basico</param>
    ''' <param name="typeVacation">Tipo de vacaciones 1 - Liquuidar 2 - Disfrutar</param>
    ''' <param name="typePayment">Tipo de pago  1 - Inmedito 2 - Proxima Nomina</param>
    ''' <param name="initialDateVacation">Fecha Inicial Vacaciones</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RequestVacationEmployees(employees As List(Of Domain.Payroll.Entities.Employee), requestDays As Integer, typeCalculate As Byte, typeVacation As Byte, typePayment As Byte, initialDateVacation As Date, audit As Infrastructure.CrossCutting.Base.SessionValues) As ActionMessageResult(Of List(Of Domain.Payroll.Entities.Employee)) Implements IVacationPeriodAdminService.RequestVacationEmployees
        Try
            Dim HUN As Boolean = False
            If audit.IndigoCompanyNit = "891180268" Then
                HUN = True
            End If

            Dim actionResult = _vacationDomain.CalculateValueVacationByFormulates(employees, requestDays, typeCalculate, typeVacation, typePayment, initialDateVacation, audit, HUN)
            Return actionResult
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", audit)
            Return New ActionMessageResult(Of List(Of Domain.Payroll.Entities.Employee))
        End Try

    End Function

    ''' <summary>
    ''' Guarda o edita una vacacion desde el agregado de empleado
    ''' </summary>
    ''' <param name="listEmployee">Lista de empleados</param>
    ''' <param name="typeVacation">Tipo de vacacion 1 liquidar 2 Disfrutar</param>
    ''' <param name="audit">Auditoria</param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveVacationEmployee(listEmployee As List(Of Domain.Payroll.Entities.Employee), typeVacation As Byte, initialDate As Nullable(Of Date), endDate As Nullable(Of Date), audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult Implements IVacationPeriodAdminService.SaveVacationEmployee
        If listEmployee Is Nothing OrElse listEmployee.Count = 0 Then
            Throw New ArgumentNullException("Lista de empleado Vacia")
        End If
        Dim result As New ActionMessageResult()
        result.StateResult = True
        Dim unitWork As IUnitWork = _employeeRepositoryCommit.UnitWork
        Dim unitworkScheduleDetailConcept As IUnitWork = _scheduleDetailConceptRepository.UnitWork
        Dim unitworkScheduleDetailHour As IUnitWork = _scheduleDetailHourRepository.UnitWork
        Dim unitworkScheduleDetail As IUnitWork = _scheduleDetailRepositoryCommit.UnitWork
        Dim unitWorkSchedule As IUnitWork = _scheduleRepository.UnitWork
        Dim unitWorkVacationPeriod As IUnitWork = _vacationPeriodRepository.UnitWork
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted ' Permite leer pero no modificar
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                For Each employee As Domain.Payroll.Entities.Employee In listEmployee
                    employee.MarkAsModified()

                    'Valido el Campo de Fechas para cambiar en la Tabla Empleados, la FEcha de Vacaciones Disfrutadas de último

                    Dim ObjTmpVacation = employee.VacationPeriod.Where(Function(x) x.PendingDays = 0).ToList()

                    If ObjTmpVacation IsNot Nothing AndAlso ObjTmpVacation.Count > 0 Then
                        Dim TmpDateLastVacation As Date
                        TmpDateLastVacation = ObjTmpVacation.Select(Function(x) x.EndDatePeriod).Max()
                        TmpDateLastVacation = DateAdd(DateInterval.Day, 1, TmpDateLastVacation)
                        employee.VacationLastDateLiquidation = TmpDateLastVacation
                    End If

                    'Dim TmpDateLastVacation = employee.VacationPeriod.Where(Function(x) x.PendingDays = 0).ToList().Select(Function(x) x.EndDatePeriod).Max()

                    Dim ListVacationDetail As New List(Of VacationDetail)

                    For Each ObjVacationPeriod As VacationPeriod In employee.VacationPeriod
                        If ObjVacationPeriod.Vacation.Count > 0 Then
                            If ObjVacationPeriod.Vacation.FirstOrDefault().ChangeTracker.State = ObjectState.Added Then
                                ObjVacationPeriod.ModificationDate = Date.Now()
                                ObjVacationPeriod.ModificationUser = audit.CodeUser
                            End If

                            For Each objVacation As Vacation In ObjVacationPeriod.Vacation
                                If objVacation.ChangeTracker.State = ObjectState.Added Then
                                    objVacation.CreationUser = audit.CodeUser
                                    objVacation.CreationDate = Date.Now()
                                Else
                                    objVacation.ModificationUser = audit.CodeUser
                                    objVacation.ModificationDate = Date.Now()
                                End If
                            Next
                        End If

                        If ObjVacationPeriod.CreationUser Is Nothing And ObjVacationPeriod.CreationDate = Nothing Then
                            ObjVacationPeriod.CreationUser = audit.CodeUser
                            ObjVacationPeriod.CreationDate = Date.Now
                        End If


                        If ObjVacationPeriod.ChangeTracker.State = ObjectState.Added Then
                            ObjVacationPeriod.CreationUser = audit.CodeUser
                            ObjVacationPeriod.CreationDate = Date.Now
                        End If

                        If ObjVacationPeriod.Vacation.Any(Function(x) x.ChangeTracker.State = ObjectState.Added) Then
                            Dim ObjVacation = ObjVacationPeriod.Vacation.Where(Function(x) x.ChangeTracker.State = ObjectState.Added).FirstOrDefault()

                            If ObjVacation.VacationDetail.Count > 0 Then
                                ListVacationDetail = ObjVacation.VacationDetail.ToList()
                            End If

                        End If

                    Next

                    If ListVacationDetail.Count > 0 Then

                        For Each ObjVacationPeriod As VacationPeriod In employee.VacationPeriod
                            If ObjVacationPeriod.Vacation.Any(Function(x) x.ChangeTracker.State = ObjectState.Added) Then

                                Dim SaveFlag As Boolean = True

                                For Each ObjVacation As Vacation In ObjVacationPeriod.Vacation.Where(Function(x) x.ChangeTracker.State = ObjectState.Added).ToList()
                                    If ObjVacation.VacationDetail IsNot Nothing AndAlso ObjVacation.VacationDetail.Any(Function(x) x.IdConcept = 0) Then
                                        Dim list As List(Of String) = (From x In ListVacationDetail Where x.IdConcept = 0 Select x.Description).ToList()
                                        Dim Message As String = "El empleado No tiene autorizado el concepto de " & String.Join(" ; ", list.ToArray())
                                        result.MessageResult = New List(Of MessageResult)
                                        result.MessageResult.Add(New MessageResult("-001", Message))
                                        result.StateResult = False
                                        Return result
                                    End If

                                    If ObjVacation.VacationDetail.Count = 0 Then

                                        For Each ObjVacationDetail As VacationDetail In ListVacationDetail
                                            ObjVacationDetail.MarkAsAdded()
                                            ObjVacation.VacationDetail.Add(ObjVacationDetail)
                                        Next

                                    End If

                                    If ObjVacation.VacationDetail.Any(Function(j) j.IdConcept = 0) AndAlso audit.CompanyType = 2 Then
                                        result.MessageResult = New List(Of MessageResult)
                                        result.MessageResult.Add(New MessageResult("DONTAPPLY", $"Se debe revisar la parametrización (Clases de Concepto) y Autorización de los siguientes conceptos: {String.Join(" , ", ObjVacation.VacationDetail _
                                                                                                                                                       .Where(Function(o) o.IdConcept = 0).Select(Function(k) _
                                                                                                                                                        k.Description).ToList())}"))
                                        result.StateResult = False
                                        SaveFlag = False
                                        Exit For
                                    End If
                                Next

                                If Not SaveFlag Then
                                    Exit For
                                End If
                                _vacationPeriodRepository.SaveEntity(ObjVacationPeriod)
                                unitWorkVacationPeriod.Commit()
                            End If

                        Next
                    End If

                    If typeVacation = 2 Then 'Disfrutar
                        Dim listSchedule = _scheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDate(employee.Id, initialDate, endDate)
                        Dim listNovelty = _noveltyRepository.GetNoveltyByEmployeeDateInitialEnd(employee.Id, initialDate, endDate)
                        If listNovelty.Count > 0 Then
                            result.MessageResult.Add(New MessageResult("-001", employee.ThirdParty.Name))
                            result.StateResult = False
                        End If

                        Dim TmpListDetail As New List(Of ScheduleDetail)

                        For Each itemDictionary As KeyValuePair(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) In _noveltyDomain.LoadDictionarySchedule(listSchedule)
                            For Each itemDetail As ScheduleDetail In itemDictionary.Value

                                For Each itemDetailHour As ScheduleDetailHour In itemDetail.ScheduleDetailHour

                                    For Each itemDetailConcept As ScheduleDetailConcept In itemDetailHour.ScheduleDetailConcept
                                        Dim StringDeleteScheduleDetailConcept = "Delete from Payroll.ScheduleDetailConcept where Id = " + itemDetailConcept.Id.ToString
                                        _scheduleDetailConceptRepository.UnitWork.ExecuteNonQuery(StringDeleteScheduleDetailConcept)
                                        unitworkScheduleDetailConcept.Commit()
                                    Next

                                    Dim StringDeleteScheduleDetailHour = "Delete from Payroll.ScheduleDetailHour where Id = " + itemDetailHour.Id.ToString
                                    _scheduleDetailHourRepository.UnitWork.ExecuteNonQuery(StringDeleteScheduleDetailHour)
                                    unitworkScheduleDetailHour.Commit()

                                Next

                                TmpListDetail.Add(itemDetail)
                            Next

                            Dim schedule As Schedule = _scheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(employee.Id, itemDictionary.Key.Key, itemDictionary.Key.Value)

                            'Ahora actualizo la cabecera
                            For Each objscheduleDetail As ScheduleDetail In TmpListDetail

                                'Actualizamos primero a Null la cabecera:
                                Dim Day As String

                                If objscheduleDetail.DateDetail.Day < 10 Then
                                    Day = "0" + objscheduleDetail.DateDetail.Day.ToString()
                                Else
                                    Day = objscheduleDetail.DateDetail.Day.ToString()
                                End If

                                Dim StringUpdate = "UPDATE Payroll.Schedule SET D" + Day + " = Null WHERE Id = " + schedule.Id.ToString()

                                _scheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringUpdate)
                                unitWorkSchedule.Commit()

                                Dim StringDeleteScheduleDetail = "Delete from Payroll.ScheduleDetail where Id = " + objscheduleDetail.Id.ToString
                                _scheduleDetailRepositoryCommit.UnitWork.ExecuteNonQuery(StringDeleteScheduleDetail)
                                unitworkScheduleDetail.Commit()
                            Next
                        Next

                        'unitWorkSchedule.Commit()
                        'unitworkScheduleDetailConcept.Commit()
                        'unitworkScheduleDetailHour.Commit()
                        'unitworkScheduleDetail.Commit()

                        Dim contractValid = _employeeRepository.GetContractValidByEmployee(employee.Id)
                        Dim listScheduleDetailVacation = _vacationDomain.CreateListScheduleDetailBetweenDate(employee, contractValid, initialDate, endDate)
                        Dim objSchedule As Schedule
                        Dim NewSchedule As Schedule
                        Dim NewListSchedule As New List(Of Schedule)
                        Dim dictionarySchedule As New Dictionary(Of String, Schedule) 'Diccionario solo para almacenar el periodo y el schedule
                        For Each detail As ScheduleDetail In listScheduleDetailVacation
                            Dim period = detail.DateDetail.ToString("MM/yyyy")
                            If Not dictionarySchedule.ContainsKey(period) Then 'Verifico si ya lo esta en el diccionario es decir si ya tengo el objeto en memoria
                                objSchedule = _scheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(employee.Id, period, contractValid.FunctionalUnitId, False)
                                If objSchedule IsNot Nothing Then
                                    NewSchedule = _scheduleRepository.GetScheduleByIdAsNoTracking(objSchedule.Id)
                                Else
                                    NewSchedule = Nothing
                                End If

                                If NewSchedule Is Nothing Then
                                    NewSchedule = New Schedule()
                                    NewSchedule.EmployeeId = employee.Id
                                    NewSchedule.Period = period
                                    NewSchedule.FunctionalUnitId = contractValid.FunctionalUnitId
                                End If
                                dictionarySchedule.Add(period, NewSchedule)
                                NewListSchedule.Add(NewSchedule)
                            End If

                            Dim schedulePeriod = dictionarySchedule(period)
                        Next

                        For Each objScheduleDetailVacation As ScheduleDetail In listScheduleDetailVacation

                            Dim StringInsert As String = "INSERT INTO Payroll.ScheduleDetail(GroupId, EmployeeId, ContractId, CompanyId, BranchOfficeId, FunctionalUnitId, CenterCostId, ScheduleFunctionalUnitId, Letter, DateDetail, TotalNumberHours, Status, State)"
                            Dim Values As String = " VALUES (" + objScheduleDetailVacation.GroupId.ToString + "," + objScheduleDetailVacation.EmployeeId.ToString() + "," + objScheduleDetailVacation.ContractId.ToString() + "," + objScheduleDetailVacation.CompanyId.ToString + "," + objScheduleDetailVacation.BranchOfficeId.ToString() + "," + objScheduleDetailVacation.FunctionalUnitId.ToString() + "," + objScheduleDetailVacation.CenterCostId.ToString() + "," +
                            objScheduleDetailVacation.ScheduleFunctionalUnitId.ToString + ",'" + objScheduleDetailVacation.Letter.ToString() + "','" + objScheduleDetailVacation.DateDetail.ToString("yyyy-MM-dd") + "'," + objScheduleDetailVacation.TotalNumberHours.ToString() + "," + objScheduleDetailVacation.Status.ToString() + ",0)"
                            _scheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringInsert + Values)
                            unitworkScheduleDetail.Commit()

                            Dim period = objScheduleDetailVacation.DateDetail.ToString("MM/yyyy")
                            objSchedule = _scheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(employee.Id, period, contractValid.FunctionalUnitId, False)

                            Dim Day As String

                            If objScheduleDetailVacation.DateDetail.Day < 10 Then
                                Day = "0" + objScheduleDetailVacation.DateDetail.Day.ToString()
                            Else
                                Day = objScheduleDetailVacation.DateDetail.Day.ToString()
                            End If

                            Dim TmpScheduleDetail = _scheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDate(objScheduleDetailVacation.EmployeeId, objScheduleDetailVacation.DateDetail, objScheduleDetailVacation.DateDetail).FirstOrDefault()

                            If objSchedule IsNot Nothing Then
                                Dim objScheduleTmp = _scheduleRepository.GetScheduleByIdAsNoTracking(objSchedule.Id)

                                'El schedule ya fue creado

                                'Actualizamos primero a Null la cabecera:
                                Dim StringUpdate = "UPDATE Payroll.Schedule SET D" + Day + " = " + TmpScheduleDetail.Id.ToString + " WHERE Id = " + objScheduleTmp.Id.ToString()
                                _scheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringUpdate)
                                unitWorkSchedule.Commit()
                            Else

                                Dim StringInsertCabecera = "INSERT INTO Payroll.Schedule (EmployeeId, Period, FunctionalUnitId, TotalHour, State) VALUES ( " + objScheduleDetailVacation.EmployeeId.ToString() + ",'" + period + "'," + objScheduleDetailVacation.FunctionalUnitId.ToString + "," + "0, 0 )"
                                _scheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringInsertCabecera)
                                unitWorkSchedule.Commit()

                                Dim TmpObjSchedule = _scheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(employee.Id, period, contractValid.FunctionalUnitId, False)

                                Dim StringUpdate = "UPDATE Payroll.Schedule SET D" + Day + " = " + TmpScheduleDetail.Id.ToString + " WHERE Id = " + TmpObjSchedule.Id.ToString()
                                _scheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringUpdate)
                                unitWorkSchedule.Commit()
                            End If

                        Next

                    End If 'Fin del si el tipo de vacacion es Liquidar

                Next


                scope.Complete()
            End Using
            For Each itemEmployee As Domain.Payroll.Entities.Employee In listEmployee
                For Each itemVacationPeriod As VacationPeriod In itemEmployee.VacationPeriod
                    If itemVacationPeriod.ChangeTracker.State = ObjectState.Added Then
                        '/****** Auditoria Basica ******/
                        IndigoAuditBasic.Execute(itemVacationPeriod.GetType.Name, audit.Functional, itemVacationPeriod.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, Date.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                        For Each itemVacation As Vacation In itemVacationPeriod.Vacation
                            '/****** Auditoria Basica ******/
                            IndigoAuditBasic.Execute(itemVacation.GetType.Name, audit.Functional, itemVacation.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, Date.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                        Next
                    ElseIf itemVacationPeriod.ChangeTracker.State = ObjectState.Modified Then
                        '/****** Auditoria Basica ******/
                        IndigoAuditBasic.Execute(itemVacationPeriod.GetType.Name, audit.Functional, itemVacationPeriod.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, Date.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                        For Each itemVacation As Vacation In itemVacationPeriod.Vacation
                            '/****** Auditoria Basica ******/
                            IndigoAuditBasic.Execute(itemVacation.GetType.Name, audit.Functional, itemVacation.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, Date.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                        Next
                    End If
                Next
            Next
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            unitWorkSchedule.RollbackChanges()
            unitworkScheduleDetail.RollbackChanges()
            unitworkScheduleDetailConcept.RollbackChanges()
            unitworkScheduleDetailHour.RollbackChanges()
            unitWork.RollbackChanges()
            unitWorkVacationPeriod.RollbackChanges()
            result.Message = ex.Message.ToString()
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Función para Confirmar las Vacaciones de Tipo Pago Inmediato. Genera Comprobante Contable y Comprobante de Egreso
    ''' </summary>
    ''' <param name="listVacation"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function ConfirmVacation(listVacation As List(Of Domain.Payroll.Entities.Vacation), session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult Implements IVacationPeriodAdminService.ConfirmVacation

        Dim ObjActionResult As New ActionResult
        Dim ListMessage As New List(Of String)

        Dim unitWork As IUnitWork = _vacationRepository.UnitWork
        Dim unitWorkAgreements As IUnitWork = _agreementsCRepository.UnitWork
        Dim unitWorkForeclousure As IUnitWork = _foreclousureRepository.UnitWork
        Dim messageVoucherResult As String = ""
        'configuro la transaccion
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.DefaultTimeout
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted ' Permite leer pero no modificar

        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try

                If listVacation.Any(Function(x) x.TypePayment = 1 And x.State = 1) = False Then
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = "No hay Vacacaciones Pendientes por Confirmar o de Tipo de Pago Inmediato"}
                End If

                Dim AnaliceListVacation = listVacation.Where(Function(x) x.TypePayment = 1 And x.State = 1).ToList()

                Dim ListVacationDetail As New List(Of VacationDetail)

                For Each objVacation As Vacation In AnaliceListVacation
                    'Cambiamos el estado de las Vacaciones a "Pagas"

                    Dim ObjGroup = _groupRepository.GetGroupById(objVacation.VacationPeriod.Contract.GroupId)

                    Dim PayrollEndingDate = _domainPayroll.GetEndPayrollDate(ObjGroup.Liquidation, ObjGroup.NextDateLiquidation)

                    objVacation.State = 2
                    _vacationRepository.SaveEntity(objVacation)

                    If objVacation.ModificationUser Is Nothing Then
                        objVacation.ModificationUser = session.AuditMessageWcf.CodeUser
                        objVacation.ModificationDate = Date.Now()
                    End If

                    objVacation.ConfirmationUser = session.AuditMessageWcf.CodeUser
                    objVacation.ConfirmationDate = Date.Now()

                    'Si tiene Convenios, insertamos en el detalle del Convenio el valor
                    Dim ObjListAgreements = _agreementsCRepository.GetAgreementsByEmployeeStatusVacation(objVacation.VacationPeriod.EmployeeId, 2, objVacation.VacationStartDate)

                    If ObjListAgreements IsNot Nothing AndAlso ObjListAgreements.Count > 0 Then
                        For Each objAgreements As AgreementsC In ObjListAgreements

                            If objVacation.VacationDetail.Any(Function(x) x.IdConcept = objAgreements.ConceptId) Then

                                Dim ObjDetailVacation = objVacation.VacationDetail.Where(Function(x) x.IdConcept = objAgreements.ConceptId).FirstOrDefault()

                                If objAgreements.LiquidationType = 1 AndAlso objAgreements.TermType = 1 Then
                                    objAgreements.CurrentBalance = objAgreements.CurrentBalance - ObjDetailVacation.Deducted
                                End If

                                objAgreements.MarkAsModified()

                                'Creamos el detalle del Convenio
                                Dim ObjAgreementsD As New AgreementsD

                                With ObjAgreementsD
                                    .ShareValuePaid = ObjDetailVacation.Deducted
                                    .DatePayment = objVacation.VacationStartDate
                                    .TypePayment = 4 'Pago por Vacaciones
                                    .StateShare = "Pago registrado por Vacaciones para la Nómina de " + Date.Now.ToShortDateString
                                End With

                                objAgreements.AgreementsD.Add(ObjAgreementsD)

                                _agreementsCRepository.SaveEntity(objAgreements)

                            End If
                        Next
                    End If

                    'Si tiene Embargos, insertamos en el detalle del Embargo el valor
                    Dim ObjListForeclousure = _foreclousureRepository.GetForeclousureByEmployeeStatus(objVacation.VacationPeriod.EmployeeId, 2)

                    If ObjListForeclousure IsNot Nothing AndAlso ObjListForeclousure.Count > 0 Then
                        For Each objForeclousure As Foreclousure In ObjListForeclousure

                            If objVacation.VacationDetail.Any(Function(x) x.IdConcept = objForeclousure.IdConcept) Then

                                Dim ObjDetailVacation = objVacation.VacationDetail.Where(Function(x) x.IdConcept = objForeclousure.IdConcept).FirstOrDefault()

                                If objForeclousure.DiscountType = 3 Or objForeclousure.DiscountType = 4 Then
                                    objForeclousure.CurrentBalance = objForeclousure.CurrentBalance - ObjDetailVacation.Deducted
                                End If

                                objForeclousure.MarkAsModified()

                                'Creamos el detalle del Embargo
                                Dim ObjForeclousureDetail As New ForeclousureDetail

                                With ObjForeclousureDetail
                                    .ShareValuePaid = ObjDetailVacation.Deducted
                                    .DatePayment = PayrollEndingDate
                                    .TypePayment = 4 'Pago por Vacaciones
                                    .StateShare = "Pago registrado por Vacaciones el día " + Date.Now.ToShortDateString
                                End With

                                objForeclousure.ForeclousureDetail.Add(ObjForeclousureDetail)

                                _foreclousureRepository.SaveEntity(objForeclousure)

                            End If

                        Next

                    End If

                    ListVacationDetail.AddRange(objVacation.VacationDetail)

                Next

                'Generamos el Comprobante Contable de Vacaciones

                Dim Employee = listVacation.FirstOrDefault().VacationPeriod.Employee
                Dim Contract = Employee.Contract.Where(Function(x) x.Valid = True And x.Status = 1).FirstOrDefault()
                Dim tmpObjGroup = _groupRepository.GetGroupById(Contract.GroupId)

                Dim ObjJournalVoucherVacation = _vacationDomain.CreateJournalVoucher(ListVacationDetail, Employee, Contract, tmpObjGroup, session.AuditMessageWcf)

                If ObjJournalVoucherVacation.StateResult = False Then
                    ListMessage.Add(ObjJournalVoucherVacation.Message)
                    Return New ActionResult With {.StateResult = False, .MessageResult = ListMessage, .Message = ObjJournalVoucherVacation.Message}
                End If

                Dim ObjActionResultJournal = _AccountingDocumentAdmin.SaveAccountingDocument(ObjJournalVoucherVacation.ObjectEmbbeded, session.AuditMessageWcf, True)

                If ObjActionResultJournal.StateResult = False Then
                    ListMessage.Add(ObjActionResultJournal.Message)
                    scope.Dispose()
                    Return New ActionResult With {.StateResult = False, .MessageResult = ListMessage, .Message = ObjActionResultJournal.Message}
                End If

                If ObjActionResultJournal.ObjectEmbbeded.Consecutive = 0 Then
                    messageVoucherResult = ObjActionResultJournal.Message
                Else
                    messageVoucherResult = "Se ha generado el Comprobante Contable # " + ObjActionResultJournal.ObjectEmbbeded.Consecutive.ToString() + " - " + ObjActionResultJournal.ObjectEmbbeded.Detail.ToString()
                End If
                ListMessage.Add(messageVoucherResult)

                Dim generateVoucherTransaction = _payrollSettings.GetSettingPayroll(False)

                '' SECUENCIAS NUMÉRICAS DE COMPROBANTE DE EGRESO
                Dim SequenseDetailTreasuryId As Integer = 0
                'Se consulta la secuencia de pagos por el id del form
                Dim treasurySequence As Domain.Entities.TreasurySequence = _sequenseTreasuryRepository.GetSequenseByIdForm("636")
                If treasurySequence.Id = 0 Then
                    Return New ActionResult With {.StateResult = False, .Message = "No existe secuencia numérica para el formulario de Comprobante de Egresos."}
                End If
                'Se valida que la secuencia numerica no sea manual
                If treasurySequence.IsManual Then
                    Return New ActionResult With {.StateResult = False, .Message = "La secuencia numerica de Comprobante de Entrada es manual."}
                End If
                'Se valida la secuencia numerica
                If treasurySequence.Scope = "O" Then 'Si la secuencia es por organización
                    SequenseDetailTreasuryId = (From x In treasurySequence.TreasurySequenceDetail Select x.Id).FirstOrDefault()
                ElseIf treasurySequence.Scope = "OU" Then 'Si la secuencia es por unidad operativa
                    'Se valida que la unidad operativa seleccionada este en la secuencia
                    If (From x In treasurySequence.TreasurySequenceDetail Where x.IdOperatingUnit = session.IndigoOperatingUnitId Select x).Count = 0 Then
                        Return New ActionResult() With {.StateResult = False, .Message = "No existe la unidad operativa seleccionada en la secuencia de Comprobantes de Egresos."}
                    End If
                    SequenseDetailTreasuryId = (From x In treasurySequence.TreasurySequenceDetail Where x.IdOperatingUnit = session.IndigoOperatingUnitId Select x.Id).FirstOrDefault()
                ElseIf treasurySequence.Scope = "CC" Then 'Si la secuencia es por tipo de comprobante
                    'Se valida que exista una secuencia para el tipo de pago
                    If (From x In treasurySequence.TreasurySequenceDetail Where x.Type = 1 Select x).Count = 0 Then
                        Return New ActionResult() With {.StateResult = False, .Message = "No existe la secuencia de Comprobantes de Egresos para el tipo Pago."}
                    End If
                    SequenseDetailTreasuryId = (From x In treasurySequence.TreasurySequenceDetail Where x.Type = 1 Select x.Id).FirstOrDefault()
                End If


                ''Se valida si el parámetro de nómina, "Generar_Comprobante_de_Egreso, en el segmento de vacaciones, se encuentra activo"
                If generateVoucherTransaction.GenerateVoucherTransactionVacation Then

                    'Generamos el Comprobante de Egreso del Pago de Vacaciones
                    Dim ObjVoucherTransaction = _vacationDomain.CreateVoucherTransaction(AnaliceListVacation.FirstOrDefault(), Employee, tmpObjGroup, session)

                    If ObjVoucherTransaction.StateResult = True Then
                        Dim ObjResultVoucherTransaction = _voucherTransactionAdmin.SaveVoucherTransaction(ObjVoucherTransaction.ObjectEmbbeded, session.AuditMessageWcf, 0, SequenseDetailTreasuryId, treasurySequence, 0)
                        If ObjResultVoucherTransaction.StateResult = False Then
                            ListMessage.Add(ObjResultVoucherTransaction.Message)
                            scope.Dispose()
                            Return New ActionResult With {.StateResult = False, .MessageResult = ListMessage}
                        End If
                        messageVoucherResult = "Se ha generado el Comprobante de Egreso # " + ObjResultVoucherTransaction.ObjectEmbbeded.Code
                        ListMessage.Add(messageVoucherResult)
                    End If
                End If

                messageVoucherResult = messageVoucherResult + ". Las vacaciones se han confirmado correctamente."

                ObjActionResult.StateResult = True
                ObjActionResult.Message = messageVoucherResult

                unitWorkForeclousure.Commit()
                unitWorkAgreements.Commit()
                unitWork.Commit()
                scope.Complete()

                Return ObjActionResult

            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                unitWorkForeclousure.RollbackChanges()
                unitWorkAgreements.RollbackChanges()
                unitWork.RollbackChanges()
                scope.Dispose()
                ObjActionResult.Message = ex.Message.ToString()
                ObjActionResult.StateResult = False
                Return ObjActionResult
            End Try

        End Using

    End Function

    ''' <summary>
    ''' Cancelar la solicitud de vacaciones que aun no esta pagada
    ''' </summary>
    ''' <param name="vacation"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CancelRequest(vacation As Vacation, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Vacation) Implements IVacationPeriodAdminService.CancelRequest
        If vacation Is Nothing Then
            Throw New ArgumentNullException("Vacaciones vacia")
        End If
        Dim unitWorkVacationPeriod As IUnitWork = _vacationPeriodRepository.UnitWork
        Dim unitWorkEmployee As IUnitWork = _employeeRepositoryCommit.UnitWork
        Dim unitWorkVacation As IUnitWork = _vacationRepository.UnitWork
        Dim unitWorkScheduleDetail As IUnitWork = _scheduleDetailRepositoryCommit.UnitWork
        Dim unitWorkSchedule As IUnitWork = _scheduleRepositoryCommit.UnitWork

        Dim result As ActionMessageResult(Of Vacation) = New ActionMessageResult(Of Vacation)()
        result.StateResult = True
        Try
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted ' Permite leer pero no modificar
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)

                If vacation.State = 2 Then
                    result.MessageResult.Add(New MessageResult("-001", Nothing))
                    result.StateResult = False
                    Return result
                End If

                vacation.ModificationUser = audit.CodeUser
                vacation.ModificationDate = Date.Now()

                Dim initialDate = vacation.VacationStartDate
                Dim endDate = vacation.VacationEndDate
                Dim incorporationDate = vacation.IncorporationDateReal

                If vacation.TypeVacation = 2 Then

                    '***** Elimino los detalles del calendario de vacaciones ******
                    Dim listScheduleDetail = _scheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDate(vacation.VacationPeriod.EmployeeId, initialDate, DateAdd(DateInterval.Day, -1, incorporationDate))

                    'Diccionario el cual agrupa los calendarios por periodo y unidad funcional
                    Dim scheduleDictionary As Dictionary(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) = _noveltyDomain.LoadDictionarySchedule(listScheduleDetail)
                    For Each itemDictionary As KeyValuePair(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) In scheduleDictionary

                        Dim schedule As Schedule = _scheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(vacation.VacationPeriod.EmployeeId, itemDictionary.Key.Key, itemDictionary.Key.Value)

                        For Each itemDetail As ScheduleDetail In itemDictionary.Value

                            'Ahora actualizo la cabecera

                            'Actualizamos primero a Null la cabecera:
                            Dim Day As String

                            If itemDetail.DateDetail.Day < 10 Then
                                Day = "0" + itemDetail.DateDetail.Day.ToString()
                            Else
                                Day = itemDetail.DateDetail.Day.ToString()
                            End If

                            Dim StringUpdate = "UPDATE Payroll.Schedule SET D" + Day + " = Null WHERE Id = " + schedule.Id.ToString()

                            _scheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringUpdate)
                            unitWorkSchedule.Commit()

                            Dim StringDeleteScheduleDetail = "Delete from Payroll.ScheduleDetail where Id = " + itemDetail.Id.ToString
                            _scheduleDetailRepositoryCommit.UnitWork.ExecuteNonQuery(StringDeleteScheduleDetail)
                            unitWorkScheduleDetail.Commit()
                        Next
                    Next
                End If


                Dim idEmployee = vacation.VacationPeriod.EmployeeId
                Dim listEmployees = ListVacationPeriodByFilter(1, idEmployee.ToString())
                If listEmployees.Count > 0 Then
                    Dim employee = listEmployees(0)
                    For Each itemVacationPeriod As VacationPeriod In employee.VacationPeriod
                        Dim index As Integer = 0
                        Dim takenDays As Integer = 0
                        While index < itemVacationPeriod.Vacation.Count
                            Dim itemVacation As Vacation = itemVacationPeriod.Vacation(index)
                            If itemVacation.VacationStartDate = initialDate And itemVacation.VacationEndDate = endDate Then
                                Dim idPeriod As Integer = itemVacation.VacationPeriod.Id

                                takenDays += itemVacation.TakenDays

                                _vacationRepository.UnitWork.ExecuteNonQuery("delete from Payroll.VacationDetail where IdVacation = " + itemVacation.Id.ToString())

                                _vacationRepository.UnitWork.ExecuteNonQuery("delete from Payroll.Vacation where Id = " + itemVacation.Id.ToString())

                            End If
                            index += 1
                        End While
                        employee.RemainingVacationDays = employee.RemainingVacationDays + takenDays

                        unitWorkVacation.Commit()

                        _employeeRepository.UnitWork.ExecuteNonQuery("UPDATE Payroll.Employee " & "SET RemainingVacationDays = " & employee.RemainingVacationDays & " WHERE Id = " & employee.Id.ToString())
                        itemVacationPeriod.PendingDays += takenDays
                        itemVacationPeriod.TakenDays -= takenDays

                        If itemVacationPeriod.Id > 0 And takenDays > 0 Then
                            itemVacationPeriod.MarkAsModified()


                            If itemVacationPeriod.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                                itemVacationPeriod.CreationUser = audit.CodeUser
                                itemVacationPeriod.CreationDate = Date.Now()
                            End If

                            If itemVacationPeriod.ChangeTracker.State = ObjectState.Modified Then
                                itemVacationPeriod.ModificationUser = audit.CodeUser
                                itemVacationPeriod.ModificationDate = Date.Now()
                            End If


                            _vacationPeriodRepository.SaveEntity(itemVacationPeriod)

                        End If
                    Next
                End If
                unitWorkSchedule.Commit()
                unitWorkScheduleDetail.Commit()
                unitWorkEmployee.Commit()
                unitWorkVacationPeriod.Commit()

                scope.Complete()
            End Using
            Return result
        Catch ex As Exception
            unitWorkSchedule.RollbackChanges()
            unitWorkScheduleDetail.RollbackChanges()
            unitWorkVacation.RollbackChanges()
            unitWorkVacationPeriod.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Funcion para el realizar el ingreso forzoso
    ''' </summary>
    ''' <param name="vacation">Solicitud de vacacion</param>
    ''' <param name="dateEntry">Fecha ingreso</param>
    ''' <param name="audit">auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ForceEntry(vacation As Vacation, dateEntry As Date, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Vacation) Implements IVacationPeriodAdminService.ForceEntry
        If vacation Is Nothing Then
            Throw New ArgumentNullException("Vacaciones vacias")
        End If
        Dim unitWorkEmployee As IUnitWork = _employeeRepositoryCommit.UnitWork
        Dim unitWorkScheduleDetail As IUnitWork = _scheduleDetailRepositoryCommit.UnitWork
        Dim unitWorkSchedule As IUnitWork = _scheduleRepository.UnitWork
        Dim result As ActionMessageResult(Of Vacation) = New ActionMessageResult(Of Vacation)()
        result.StateResult = True
        Try
            Dim listHoliday = _holidayRepository.ListHolidayBetweenDate(vacation.VacationStartDate, dateEntry)
            Dim endDateReal As Date = Nothing
            Dim takenDayReal As Integer = 0
            Dim indexDateTmp As Date = dateEntry
            Dim contract = vacation.VacationPeriod.Employee.Contract.ToList().Find(Function(x) x.Valid = True)
            While endDateReal = Nothing 'Obtengo la fecha fin de la vacacion
                indexDateTmp = indexDateTmp.AddDays(-1)
                If _vacationDomain.IsValidDay(listHoliday, contract.Group.PayrollParameter.SaturdayBusinessDay, contract.Group.PayrollParameter.SundayBusinessDay, indexDateTmp) Then
                    endDateReal = indexDateTmp
                End If
            End While
            indexDateTmp = vacation.VacationStartDate
            While indexDateTmp <= endDateReal 'Obtengo la cantidad de dias tomados  reales
                If _vacationDomain.IsValidDay(listHoliday, contract.Group.PayrollParameter.SaturdayBusinessDay, contract.Group.PayrollParameter.SundayBusinessDay, indexDateTmp) Then
                    takenDayReal += 1
                End If
                indexDateTmp = indexDateTmp.AddDays(1)
            End While
            indexDateTmp = dateEntry
            Dim daysDifference = 0
            'Se vuelve a calcular los festivos entre la fecha de ingreso y la fecha final
            listHoliday = _holidayRepository.ListHolidayBetweenDate(indexDateTmp, vacation.VacationEndDate)
            While indexDateTmp <= vacation.VacationEndDate 'Calculo la diferencia de dias entre la final inicial y la final real
                If _vacationDomain.IsValidDay(listHoliday, contract.Group.PayrollParameter.SaturdayBusinessDay, contract.Group.PayrollParameter.SundayBusinessDay, indexDateTmp) Then
                    daysDifference += 1
                End If
                indexDateTmp = indexDateTmp.AddDays(1)
            End While
            Dim initialDate = vacation.VacationStartDate
            Dim endDate = vacation.VacationEndDate
            Dim idEmployee = vacation.VacationPeriod.EmployeeId
            Dim listEmployees = ListVacationPeriodByFilter(1, idEmployee.ToString())


            ' If vacation.IncomeType = 2 Then
            'Ingreso por Aplazamiento
            '***** Elimino los detalles del calendario de vacaciones ******
            Dim listScheduleDetail = _scheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDate(idEmployee, dateEntry, endDate)
            'Diccionario el cual agrupa os calendarios por periodo y unidad funcional
            Dim scheduleDictionary As Dictionary(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) = _noveltyDomain.LoadDictionarySchedule(listScheduleDetail)

            For Each itemDictionary As KeyValuePair(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) In scheduleDictionary

                Dim schedule As Schedule = _scheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(idEmployee, itemDictionary.Key.Key, itemDictionary.Key.Value)

                For Each objScheduleDetail As ScheduleDetail In itemDictionary.Value

                    If objScheduleDetail.ScheduleDetailHour IsNot Nothing AndAlso objScheduleDetail.ScheduleDetailHour.Count > 0 Then
                        Dim StringDeleteScheduleDetailHour = "Delete from Payroll.ScheduleDetailHour where ScheduleDetailId = " + objScheduleDetail.Id.ToString
                        _scheduleDetailRepositoryCommit.UnitWork.ExecuteNonQuery(StringDeleteScheduleDetailHour)
                    End If

                    'Actualizamos primero a Null la cabecera:
                    Dim Day As String

                    If objScheduleDetail.DateDetail.Day < 10 Then
                        Day = "0" + objScheduleDetail.DateDetail.Day.ToString()
                    Else
                        Day = objScheduleDetail.DateDetail.Day.ToString()
                    End If

                    Dim StringUpdate = "UPDATE Payroll.Schedule SET D" + Day + " = Null WHERE Id = " + schedule.Id.ToString()

                    _scheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringUpdate)

                    'Luego borramos los detalles creados
                    Dim StringDelete As String = "Delete From Payroll.ScheduleDetail where Id = " + objScheduleDetail.Id.ToString()
                    _scheduleDetailRepositoryCommit.UnitWork.ExecuteNonQuery(StringDelete)

                Next
            Next

            If listEmployees.Count > 0 Then
                Dim employee = listEmployees(0)
                Dim ForceIngressResolutionNumber = vacation.ForceEntryResolutionNumber
                Dim ForceIngressResolutionDate = vacation.ForceEntryResolutionDate
                employee = _vacationDomain.ForceEntryVacationEmployee(employee, initialDate, endDate, dateEntry, daysDifference, ForceIngressResolutionNumber, ForceIngressResolutionDate, vacation.IncomeType, vacation.ForceEntryResolutionDate, contract) 'Modifico las vacaciones del empleado
                _employeeRepositoryCommit.SaveEntity(employee)
            End If
            vacation.ModificationUser = audit.CodeUser
            vacation.ModificationDate = Date.Now()
            unitWorkSchedule.Commit()
            unitWorkScheduleDetail.Commit()

            unitWorkEmployee.Commit()
            Return result
        Catch ex As Exception
            unitWorkSchedule.RollbackChanges()
            unitWorkScheduleDetail.RollbackChanges()
            unitWorkEmployee.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Funcion la cual obtiene las vacaciones de una fecha de liquidacion especifica
    ''' </summary>
    ''' <param name="dateLiquidation">Fecha de liquidacion de nomina</param>
    ''' <param name="audit">Auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationLiquidationDate(dateLiquidation As Date, state As Byte, audit As Infrastructure.CrossCutting.Base.AuditMessage) As List(Of Vacation) Implements IVacationPeriodAdminService.GetVacationLiquidationDate
        Try
            Return _vacationRepository.GetVacationLiquidationDate(dateLiquidation, state)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Vacation)()
        End Try
    End Function

    ''' <summary>
    ''' Funcion para listar todas las vacaciones de tengan cierto estado de incorporacion
    ''' </summary>
    ''' <param name="stateIncorporation">Estado de incorporacion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationStateIncorporation(stateIncorporation As Byte) As List(Of Vacation) Implements IVacationPeriodAdminService.GetVacationStateIncorporation
        Try
            Return _vacationRepository.GetVacationStateIncorporation(stateIncorporation)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Vacation)()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los detalles de calendario de varios empleados en un rango de fechas
    ''' </summary>
    ''' <param name="listIdEmpleoyee">Lista de ids de empleados</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByListEmployeeBetweenDate(listIdEmpleoyee As List(Of Integer), dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail) Implements IVacationPeriodAdminService.GetScheduleDetailByListEmployeeBetweenDate
        Try
            Return _scheduleDetailRepository.GetScheduleDetailByListEmployeeBetweenDate(listIdEmpleoyee, dateInitial, dateEnd)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ScheduleDetail)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la lista de novedades que estan dentro de un rango de fechas a excepcion de la que se envia como parametro
    ''' </summary>
    ''' <param name="listEmployeeId">Lista de id de empleados</param>
    ''' <param name="initialDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha Fin</param>
    ''' <returns>Lista de novedades</returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyByListIdEmployeeBetweenDate(listEmployeeId As List(Of Integer), initialDate As Date, endDate As Date) As List(Of Novelty) Implements IVacationPeriodAdminService.GetNoveltyByListIdEmployeeBetweenDate
        Try
            Return _noveltyRepository.GetNoveltyByListIdEmployeeBetweenDate(listEmployeeId, initialDate, endDate)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Novelty)
        End Try
    End Function

    ''' <summary>
    ''' Funcion para listar las vaciones que estan en cierto rango de fechas
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationBetweenDate(employeeId As Integer, initialDate As Date, endDate As Date) As List(Of Vacation) Implements IVacationPeriodAdminService.GetVacationBetweenDate
        Try
            Return _vacationRepository.GetVacationBetweenDate(employeeId, initialDate, endDate)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Vacation)
        End Try
    End Function

    Public Function GetVacationPeriodWithDetailByEmployee(employeeId As Integer) As List(Of VacationPeriod) Implements IVacationPeriodAdminService.GetVacationPeriodWithDetailByEmployee
        Return _vacationPeriodRepository.GetVacationPeriodWithDetailByEmployee(employeeId)
    End Function

    Public Function IsValidDay(listHoliday As List(Of Domain.Entities.Holiday), saturdayBusinessDay As Boolean, sundayBusinessDay As Boolean, dateValidation As Date) As Boolean Implements IVacationPeriodAdminService.IsValidDay
        Return _vacationDomain.IsValidDay(listHoliday, saturdayBusinessDay, sundayBusinessDay, dateValidation)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _vacationDomain.Dispose()
                _noveltyDomain.Dispose()
            End If
            _vacationRepository = Nothing
            _employeeRepository = Nothing
            _vacationDomain = Nothing
            _holidayRepository = Nothing
            _liquidationRepository = Nothing
            _vacationPeriodRepository = Nothing
            _employeeRepositoryCommit = Nothing
            _scheduleDetailRepository = Nothing
            _noveltyRepository = Nothing
            _scheduleRepository = Nothing
            _noveltyDomain = Nothing
            _scheduleDetailRepositoryCommit = Nothing
            _scheduleDetailHourRepository = Nothing
            _scheduleDetailConceptRepository = Nothing
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
