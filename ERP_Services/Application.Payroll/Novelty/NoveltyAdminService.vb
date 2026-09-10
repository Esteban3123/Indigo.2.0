'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 07-07-2013
'
' Last Modified By : Cristhian Mauricio Salazar
' Last Modified On : 20-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Audit
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class NoveltyAdminService
    Implements INoveltyAdminService

    Private ReadOnly _payrollNoveltyDomain As IPayrollNoveltyDomain
    ''' <summary>
    ''' Repositorio de parentesco
    ''' </summary>
    ''' <remarks></remarks>
    Private _noveltyRepository As INoveltyRepository
    ''' <summary>
    ''' Repositorio de novedades
    ''' </summary>
    ''' <remarks></remarks>
    Private _noveltyRepositoryCommit As INoveltyRepository
    ''' <summary>
    ''' Repositorio de consecutivo de comunes
    ''' </summary>
    ''' <remarks></remarks>
    Private _consecutiveRepository As IConsecutiveRepository
    ''' <summary>
    ''' Repositorio de Schedule
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleRepository As IScheduleRepository
    ''' <summary>
    ''' Repositorio de Schedule
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleDetailRepository As IScheduleDetailRepository
    ''' <summary>
    ''' Repositorio de Schedule
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleRepositoryCommit As IScheduleRepository
    ''' <summary>
    ''' Repositorio de Schedule
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleDetailRepositoryCommit As IScheduleDetailRepository
    ''' <summary>
    ''' Repositorio de las detalles de las novedades
    ''' </summary>
    ''' <remarks></remarks>
    Private _noveltyScheduleRepository As INoveltyScheduleDetailRepository
    ''' <summary>
    ''' Repositorio de las detalles de las novedades
    ''' </summary>
    ''' <remarks></remarks>
    Private _noveltyScheduleRepositoryCommit As INoveltyScheduleDetailRepository
    ''' <summary>
    ''' Repositorio de las detalles de las novedades
    ''' </summary>
    ''' <remarks></remarks>
    Private _noveltyScheduleConceptRepository As INoveltyScheduleDetailConceptRepository
    ''' <summary>
    ''' Repositorio de las detalles de las novedades
    ''' </summary>
    ''' <remarks></remarks>
    Private _noveltyScheduleHourRepository As INoveltyScheduleDetailHourRepository
    ''' <summary>
    ''' Repositorio de empleado
    ''' </summary>
    ''' <remarks></remarks>
    Private _employeeRepository As IEmployeeRepository
    ''' <summary>
    ''' Repositorio de empleado
    ''' </summary>
    ''' <remarks></remarks>
    Private _employeeRepositoryCommit As IEmployeeRepository
    ''' <summary>
    ''' Repositorio de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationRepository As IPayrollLiquidationRepository
    ''' <summary>
    ''' dominio de la novedad
    ''' </summary>
    ''' <remarks></remarks>
    Private _noveltyDomain As INoveltyDomain

    ''' <summary>
    ''' dominio de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _vacationDomain As IVacationPeriodDomain

    ''' <summary>
    ''' Repositorio de periodo de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _vacationPeriodRepository As IVacationPeriodRepository

    ''' <summary>
    ''' Repositorio de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _vacationRepository As IVacationRepository

    ''' <summary>
    ''' 
    ''' </summary>
    Private _liquidationDomain As Domain.Payroll.ILiquidationDomain

    Private _contractRepository As Domain.Payroll.IContractRepository

    ''' <summary>
    ''' Repositorio de festivos
    ''' </summary>
    ''' <remarks></remarks>
    Private _holidayRepository As Domain.Entities.IHolidayRepository

    ''' <summary>
    '''  Extiende la fecha de finalización del contrato activo (no indefinido) para cubrir
    ''' dos períodos de vacaciones a partir de la última liquidación, más los días
    ''' adicionales configurados en el grupo. Persiste el cambio en el repositorio.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ExtendContractForVacationPeriods(employee As Domain.Payroll.Entities.Employee)
        Dim activeContract = employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault()
        If activeContract IsNot Nothing AndAlso activeContract.ContractType.Undefined = False Then
            'Calcula la fecha mínima que debe tener el contrato para cubrir 2 períodos de vacaciones
            If activeContract.Group?.PayrollParameter?.MaxVacationByYear Is Nothing OrElse
                activeContract.Group.PayrollParameter.MaxVacationByYear = 0 Then
                Throw New IndigoValidationException("Maximo vacaciones por año no configurado")
            End If

            Dim vacationByMonth = 12 / activeContract.Group.PayrollParameter.MaxVacationByYear
            Dim secondPeriodEndDate = DateAdd(DateInterval.Month, vacationByMonth * 2, employee.VacationLastDateLiquidation).AddDays(-1)
            'Extiende el contrato para cubrir el segundo período + días adicionales del grupo si los hay
            Dim additionalDays = activeContract.Group.AdditionalVacationDays
            activeContract.ContractEndingDate = DateAdd(DateInterval.Day, additionalDays, secondPeriodEndDate)
            activeContract.MarkAsModified()
            _contractRepository.SaveEntity(activeContract)
        End If
    End Sub

    Public Sub New(ByVal noveltyRepository As INoveltyRepository, consecutiveRepository As IConsecutiveRepository, scheduleRepository As IScheduleRepository _
                   , scheduleDetailRepository As IScheduleDetailRepository, noveltyScheduleRepository As INoveltyScheduleDetailRepository _
                   , scheduleRepository2 As IScheduleRepository, scheduleDetailRepository2 As IScheduleDetailRepository, noveltyDomain As INoveltyDomain _
                   , noveltyRepositoryCommit As INoveltyRepository, employeeRepository As IEmployeeRepository, liquidationRepository As IPayrollLiquidationRepository _
                   , noveltyScheduleConceptRepository As INoveltyScheduleDetailConceptRepository, noveltyScheduleHourRepository As INoveltyScheduleDetailHourRepository _
                   , noveltyScheduleRepositoryCommit As INoveltyScheduleDetailRepository, vacationDomain As IVacationPeriodDomain _
                   , vacationRepository As IVacationRepository, employeeRepositoryCommit As IEmployeeRepository, vacationPeriodRepository As IVacationPeriodRepository _
                   , liquidationDomain As Domain.Payroll.ILiquidationDomain, contractRepository As Domain.Payroll.IContractRepository,
                   holidaryRepository As IHolidayRepository,
                   payrollNoveltyDomain As IPayrollNoveltyDomain)
        If noveltyRepository Is Nothing Or consecutiveRepository Is Nothing Then
            Throw New ArgumentNullException("inabilityRepository Vacio")
        End If
        _noveltyRepository = noveltyRepository
        _consecutiveRepository = consecutiveRepository
        _scheduleRepository = scheduleRepository
        _scheduleDetailRepository = scheduleDetailRepository
        _noveltyScheduleRepository = noveltyScheduleRepository
        _scheduleRepositoryCommit = scheduleRepository2
        _scheduleDetailRepositoryCommit = scheduleDetailRepository2
        _noveltyRepositoryCommit = noveltyRepositoryCommit
        _noveltyDomain = noveltyDomain
        _employeeRepository = employeeRepository
        _liquidationRepository = liquidationRepository
        _noveltyScheduleConceptRepository = noveltyScheduleConceptRepository
        _noveltyScheduleHourRepository = noveltyScheduleHourRepository
        _noveltyScheduleRepositoryCommit = noveltyScheduleRepositoryCommit
        _vacationDomain = vacationDomain
        _vacationRepository = vacationRepository
        _employeeRepositoryCommit = employeeRepositoryCommit
        _vacationPeriodRepository = vacationPeriodRepository
        _liquidationDomain = liquidationDomain
        _contractRepository = contractRepository
        _holidayRepository = holidaryRepository
        _payrollNoveltyDomain = payrollNoveltyDomain
    End Sub

    ''' <summary>
    ''' Lista de Incapacidades por Empleado
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <returns>Incapacidades por Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeInability(employeeId As Integer) As List(Of Novelty) Implements INoveltyAdminService.GetEmployeeNovelty
        If String.IsNullOrEmpty(employeeId) Then
            Throw New ArgumentNullException("Id del Empleado vacio")
        End If
        Try
            Return _noveltyRepository.GetEmployeeNovelty(employeeId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una Incapacidad
    ''' </summary>
    ''' <param name="code">Código de la Incapacidad</param>
    ''' <returns>Incapacidad</returns>
    ''' <remarks></remarks>
    Public Function GetInability(code As String) As Novelty Implements INoveltyAdminService.GetNovelty
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _noveltyRepository.GetNovelty(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Novelty()
        End Try
    End Function

    ''' <summary>
    ''' Elimina una Incapacidad
    ''' </summary>
    ''' <param name="novelty">Novedad</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteInability(novelty As Novelty, audit As AuditMessage) As Boolean Implements INoveltyAdminService.DeleteNovelty
        If novelty Is Nothing Then
            Throw New ArgumentNullException("Incapacidad vacio")
        End If
        Dim unitWork As IUnitWork = _noveltyRepository.UnitWork
        Dim unitWorkSchedule As IUnitWork = _scheduleRepositoryCommit.UnitWork
        Dim unitWorkDetail As IUnitWork = _scheduleDetailRepositoryCommit.UnitWork
        Dim unitWorkNoveltyDetail As IUnitWork = _noveltyScheduleRepositoryCommit.UnitWork
        Dim unitWorkNoveltyDetailHour As IUnitWork = _noveltyScheduleHourRepository.UnitWork
        Dim unitWorkNoveltyDetailConcept As IUnitWork = _noveltyScheduleConceptRepository.UnitWork
        Dim unitWorkEmployee As IUnitWork = _employeeRepositoryCommit.UnitWork
        Dim unitWorkVacation As IUnitWork = _vacationRepository.UnitWork
        Try
            'Opciones de las transacciones
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted ' Permite leer pero no modificar
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                If (novelty.Status <> 0) Then 'Si el estado es diferente a 0 no se puede eliminar
                    Throw New ArgumentException("No se puede eliminar una novedad que ya esta liquidada")
                End If
                Dim listNoveltyScheduleDetail = _noveltyScheduleRepository.GetNoveltyScheduleDetailByEmployeeNoveltyId(novelty.EmployeeId, novelty.Id)
                For Each noveltyItem In listNoveltyScheduleDetail
                    While noveltyItem.NoveltyScheduleDetailHour.Count > 0
                        Dim hourItem = noveltyItem.NoveltyScheduleDetailHour.Item(noveltyItem.NoveltyScheduleDetailHour.Count - 1)
                        While hourItem.NoveltyScheduleDetailConcept.Count > 0
                            Dim conceptHour = hourItem.NoveltyScheduleDetailConcept.Item(hourItem.NoveltyScheduleDetailConcept.Count - 1)
                            conceptHour.MarkAsDeleted()
                            _noveltyScheduleConceptRepository.DeleteEntity(conceptHour)
                        End While
                        hourItem.MarkAsDeleted()
                        _noveltyScheduleHourRepository.DeleteEntity(hourItem)
                    End While
                    noveltyItem.MarkAsDeleted()
                    _noveltyScheduleRepositoryCommit.DeleteEntity(noveltyItem)
                Next
                Dim listScheduleDetail = _scheduleDetailRepository.GetScheduleDetailTypeNoveltyByEmployeeNoveltyId(novelty.EmployeeId, novelty.Id)
                'Verifico si tengo que eliminar registros de vacaciones
                If novelty.VacationInitialDateNovelty IsNot Nothing And novelty.VacationEndDateNovelty IsNot Nothing Then
                    Dim listVacationDetail = _scheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDate(novelty.EmployeeId, novelty.VacationInitialDateNovelty, novelty.VacationEndDateNovelty)
                    listScheduleDetail.AddRange(listVacationDetail)
                End If
                'Diccionario el cual agrupa os calendarios por periodo y unidad funcional
                Dim scheduleDictionary As Dictionary(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) = _noveltyDomain.LoadDictionarySchedule(listScheduleDetail)
                For Each itemDictionary As KeyValuePair(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) In scheduleDictionary
                    'For Each itemDetail As ScheduleDetail In itemDictionary.Value
                    '    _scheduleDetailRepositoryCommit.DeleteEntity(itemDetail)
                    'Next
                    Dim schedule As Schedule = _scheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(novelty.EmployeeId, itemDictionary.Key.Key, itemDictionary.Key.Value)
                    If schedule IsNot Nothing Then
                        schedule.StartTracking()
                        schedule = _noveltyDomain.DeleteDetailToSchedule(schedule, itemDictionary.Value)

                        'Eliminamos el SChedule Creado

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
                    End If

                    '_scheduleRepositoryCommit.SaveEntity(schedule)
                Next
                If novelty.TypeNovelty = 3 And novelty.LicenseClass = 4 Then 'Si es licencia y si es Permiso a vacaciones
                    Dim employee = _employeeRepository.GetEmployeeById(novelty.EmployeeId)

                    For Each itemVacationPeriod As VacationPeriod In employee.VacationPeriod
                        Dim index As Integer = 0
                        Dim takenDays As Integer = 0
                        While index < itemVacationPeriod.Vacation.Count
                            Dim itemVacation As Vacation = itemVacationPeriod.Vacation(index)
                            If itemVacation.VacationStartDate = novelty.RealDate And itemVacation.VacationEndDate = novelty.EndDate Then
                                takenDays += itemVacation.TakenDays
                                _vacationRepository.DeleteEntity(itemVacation)
                            End If
                            index += 1
                        End While
                        itemVacationPeriod.PendingDays += takenDays
                        itemVacationPeriod.TakenDays -= takenDays
                        itemVacationPeriod.MarkAsModified()
                        _vacationPeriodRepository.SaveEntity(itemVacationPeriod)
                    Next

                    '_employeeRepositoryCommit.SaveEntity(employee)
                End If


                If novelty.TypeNovelty = 2 Then
                    'Sanciones
                    Dim employee = _employeeRepository.GetEmployeeById(novelty.EmployeeId)
                    For Each itemVacationPeriod As VacationPeriod In employee.VacationPeriod
                        If itemVacationPeriod.InitialDatePeriod > novelty.RealDate AndAlso itemVacationPeriod.PendingDays >= 15 Then
                            Dim NewInitialDatePeriod = DateAdd(DateInterval.Day, -novelty.Days, itemVacationPeriod.InitialDatePeriod)
                            Dim NewEndDatePeriod = DateAdd(DateInterval.Day, -novelty.Days, itemVacationPeriod.EndDatePeriod)

                            itemVacationPeriod.InitialDatePeriod = NewInitialDatePeriod
                            itemVacationPeriod.EndDatePeriod = NewEndDatePeriod
                            itemVacationPeriod.MarkAsModified()
                            _vacationPeriodRepository.SaveEntity(itemVacationPeriod)

                        End If
                    Next

                    employee.VacationLastDateLiquidation = DateAdd(DateInterval.Day, -novelty.Days, employee.VacationLastDateLiquidation)
                    employee.MarkAsModified()
                    _employeeRepositoryCommit.SaveEntity(employee)

                    ExtendContractForVacationPeriods(employee)

                End If

                novelty.StartTracking()
                novelty.MarkAsDeleted()
                _noveltyRepository.SaveEntity(novelty)
                unitWorkVacation.Commit()
                unitWorkEmployee.Commit()
                unitWorkNoveltyDetailConcept.Commit()
                unitWorkNoveltyDetailHour.Commit()
                unitWorkNoveltyDetail.Commit()
                unitWorkSchedule.Commit()
                unitWorkDetail.Commit()
                unitWork.Commit()
                If novelty.VacationInitialDateNovelty IsNot Nothing And novelty.VacationEndDateNovelty IsNot Nothing Then
                    Dim listVacationTmp = _vacationRepository.GetVacationBetweenDate(novelty.EmployeeId, novelty.RealDate, novelty.EndDate)
                    Dim initialDateVacationTmp = (From e In listVacationTmp
                                                  Select e.VacationStartDate).Min()
                    Dim endDateVacationTmp = (From e In listVacationTmp
                                              Select e.IncorporationDate).Max()
                    Dim noveltyVacaction As New Novelty()
                    noveltyVacaction.Days = novelty.Days
                    noveltyVacaction.RealDate = initialDateVacationTmp
                    noveltyVacaction.EndDate = endDateVacationTmp.AddDays(-1)
                    noveltyVacaction.TypeNovelty = 99 'Tipo de novedad imaginaria para que me cree los detalles en schedule con la imagen de vacaciones
                    noveltyVacaction.LicenseClass = novelty.LicenseClass
                    noveltyVacaction.EmployeeId = novelty.EmployeeId
                    noveltyVacaction.GroupId = novelty.GroupId

                    Dim ObjVacation = listVacationTmp.Where(Function(x) x.VacationStartDate = initialDateVacationTmp).FirstOrDefault()

                    If ObjVacation IsNot Nothing Then
                        ObjVacation.IncorporationDateReal = ObjVacation.IncorporationDate
                        ObjVacation.MarkAsModified()

                        _vacationRepository.SaveEntity(ObjVacation)
                        unitWorkVacation.CommitAndRefreshChanges()
                    End If


                    SaveInability(noveltyVacaction, audit, False) 'Envio a repasar el cuadro de turnos para que vuelva a pintar las vacaciones
                End If
                scope.Complete()
            End Using
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(novelty.GetType.Name, audit.Functional, novelty.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/***** Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of Novelty)(novelty, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWorkNoveltyDetailConcept.RollbackChanges()
            unitWorkNoveltyDetailHour.RollbackChanges()
            unitWorkNoveltyDetail.RollbackChanges()
            unitWorkSchedule.RollbackChanges()
            unitWorkDetail.RollbackChanges()
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actualiza una Incapacidad
    ''' </summary>
    ''' <param name="novelty">Incapacidad</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveInability(novelty As Novelty, audit As AuditMessage, Optional saveNovelty As Boolean = True) As ActionMessageResult(Of Novelty) Implements INoveltyAdminService.SaveNovelty
        Dim result As New ActionMessageResult(Of Novelty)
        result.MessageResult = New List(Of MessageResult)()
        result.StateResult = True
        Dim employee As Domain.Payroll.Entities.Employee = _employeeRepository.GetEmployeeById(novelty.EmployeeId)
        If novelty Is Nothing Then
            Throw New ArgumentNullException("Incapacidad vacio")
        End If
        Dim unitWork As IUnitWork = _noveltyRepositoryCommit.UnitWork
        Dim unitWorkConsecutive As IUnitWork = _consecutiveRepository.UnitWork
        Dim unitWorkSchedule As IUnitWork = _scheduleRepositoryCommit.UnitWork
        Dim unitWorkScheduleDetail As IUnitWork = _scheduleDetailRepositoryCommit.UnitWork
        Dim unitWorkNoveltyDetail As IUnitWork = _noveltyScheduleRepositoryCommit.UnitWork
        Dim unitWorkNoveltyDetailHour As IUnitWork = _noveltyScheduleHourRepository.UnitWork
        Dim unitWorkNoveltyDetailConcept As IUnitWork = _noveltyScheduleConceptRepository.UnitWork
        Dim unitWorkEmployeeCommit As IUnitWork = _employeeRepositoryCommit.UnitWork
        Dim unitWorkVacation As IUnitWork = _vacationRepository.UnitWork
        Dim unitWorkVacationPeriod As IUnitWork = _vacationPeriodRepository.UnitWork
        Dim noveltyTmp As Novelty
        Dim contract As Domain.Payroll.Entities.Contract = employee.Contract.Where(Function(x) x.Valid = True).ToList().Item(0) 'Obtengo el contrato del empleado
        If contract.FunctionalUnit.BranchOffice.CompanyId Is Nothing Then
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("Compañía no parametrizada en la sucursal " & contract.FunctionalUnit.BranchOffice.Code & " - " & contract.FunctionalUnit.BranchOffice.Name))
            Return result
        End If

        Try
            Dim holidays As List(Of Domain.Entities.Holiday) = _holidayRepository.ListHolidayBetweenDate(novelty.RealDate, novelty.EndDate.AddMonths(6))

            Dim status As Integer
            Dim auditProcess As IndigoAuditSimpleEntity(Of Novelty)
            'configuro la transaccion
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.DefaultTimeout
            txSettings.IsolationLevel = IsolationLevel.ReadCommitted ' Permite leer pero no modificar
            'inicio la transaccion
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                Dim flagDeleteUpdate As Boolean = False
                Dim listScheduleDetail As List(Of ScheduleDetail) = New List(Of ScheduleDetail)()
                If novelty.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    novelty.CreationDate = Date.Now()
                    novelty.CreationUser = audit.CodeUser
                End If
                If novelty.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    novelty.ModificationUser = audit.CodeUser
                    novelty.ModificationDate = Date.Now()
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    noveltyTmp = _noveltyRepository.GetNoveltyById(novelty.Id, False)
                End If
                If novelty.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then

                    'Primero elimino las novedades de detalle si tiene
                    Dim listNoveltyScheduleDetail = _noveltyScheduleRepository.GetNoveltyScheduleDetailByEmployeeNoveltyId(novelty.EmployeeId, novelty.Id)
                    For Each noveltyItem In listNoveltyScheduleDetail
                        While noveltyItem.NoveltyScheduleDetailHour.Count > 0
                            Dim hourItem = noveltyItem.NoveltyScheduleDetailHour.Item(noveltyItem.NoveltyScheduleDetailHour.Count - 1)
                            While hourItem.NoveltyScheduleDetailConcept.Count > 0
                                Dim conceptHour = hourItem.NoveltyScheduleDetailConcept.Item(hourItem.NoveltyScheduleDetailConcept.Count - 1)
                                conceptHour.MarkAsDeleted()
                                _noveltyScheduleConceptRepository.DeleteEntity(conceptHour)
                            End While
                            hourItem.MarkAsDeleted()
                            _noveltyScheduleHourRepository.DeleteEntity(hourItem)
                        End While
                        noveltyItem.MarkAsDeleted()
                        _noveltyScheduleRepositoryCommit.DeleteEntity(noveltyItem)
                    Next
                    'Luego elimino los detalles del Schedule
                    listScheduleDetail = _scheduleDetailRepository.GetScheduleDetailTypeNoveltyByEmployeeNoveltyId(novelty.EmployeeId, novelty.Id)
                    'Diccionario el cual agrupa os calendarios por periodo y unidad funcional
                    Dim scheduleDictionary As Dictionary(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) = _noveltyDomain.LoadDictionarySchedule(listScheduleDetail)
                    For Each itemDictionary As KeyValuePair(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) In scheduleDictionary
                        Dim schedule As Schedule = _scheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(novelty.EmployeeId, itemDictionary.Key.Key, itemDictionary.Key.Value)
                        If schedule IsNot Nothing Then
                            schedule.StartTracking()
                            schedule = _noveltyDomain.DeleteDetailToSchedule(schedule, itemDictionary.Value)

                            'Eliminamos el SChedule Creado

                            For Each objScheduleDetail As ScheduleDetail In itemDictionary.Value.Where(Function(x) x.FunctionalUnitId = itemDictionary.Key.Value).ToList()

                                If objScheduleDetail.ScheduleDetailHour IsNot Nothing AndAlso objScheduleDetail.ScheduleDetailHour.Count > 0 Then

                                    For Each ObjScheduleDetailHour As ScheduleDetailHour In objScheduleDetail.ScheduleDetailHour

                                        If ObjScheduleDetailHour.ScheduleDetailConcept IsNot Nothing AndAlso ObjScheduleDetailHour.ScheduleDetailConcept.Count > 0 Then
                                            Dim StringDeleteScheduleDetailConcept = "Delete from Payroll.ScheduleDetailConcept where ScheduleDetailHourId = " + ObjScheduleDetailHour.Id.ToString
                                            _scheduleDetailRepositoryCommit.UnitWork.ExecuteNonQuery(StringDeleteScheduleDetailConcept)
                                        End If
                                    Next

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
                        End If
                    Next
                    If noveltyTmp.TypeNovelty = 3 And noveltyTmp.LicenseClass = 4 Then 'Si es licencia y si es Permiso a vacaciones
                        'Dim employeeCommit = _employeeRepository.GetEmployeeById(novelty.EmployeeId)
                        For Each itemVacationPeriod As VacationPeriod In employee.VacationPeriod
                            Dim index As Integer = 0
                            Dim takenDays As Integer = 0
                            While index < itemVacationPeriod.Vacation.Count
                                Dim itemVacation As Vacation = itemVacationPeriod.Vacation(index)
                                If itemVacation.VacationStartDate = noveltyTmp.RealDate And itemVacation.VacationEndDate = noveltyTmp.EndDate Then
                                    takenDays += itemVacation.TakenDays
                                    itemVacation.MarkAsDeleted()
                                    _vacationRepository.DeleteEntity(itemVacation)
                                End If
                                index += 1
                            End While
                            itemVacationPeriod.PendingDays += takenDays
                            itemVacationPeriod.TakenDays -= takenDays
                            itemVacationPeriod.MarkAsModified()
                            _vacationPeriodRepository.SaveEntity(itemVacationPeriod)
                        Next
                    End If

                    unitWorkVacation.Commit()
                    unitWorkVacationPeriod.Commit()
                    unitWorkSchedule.Commit()
                    unitWorkScheduleDetail.Commit()
                    flagDeleteUpdate = True
                ElseIf novelty.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added And novelty.Consecutive = 0 And saveNovelty = True Then
                    novelty.CreationUser = audit.CodeUser
                    novelty.CreationDate = Date.Now()
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                    Dim consecutive As Domain.Entities.Consecutive = _consecutiveRepository.GetConsecutiveByCode("5")
                    _noveltyDomain.LoadConsecutive(consecutive, novelty) ' Carga el consecutivo a la novedad siempre y cuando sea nuevo
                    _consecutiveRepository.SaveEntity(consecutive)
                End If
                'Averiguo si el empleado esta en vacaciones
                Dim listVacation = _vacationRepository.GetVacationBetweenDate(novelty.EmployeeId, novelty.RealDate, novelty.EndDate)
                listVacation = listVacation.Where(Function(x) x.TypeVacation = 2).ToList()
                If listVacation IsNot Nothing AndAlso listVacation.Count > 0 Then
                    If novelty.RealDate = listVacation.Item(0).IncorporationDateReal Then
                        listVacation = Nothing
                    End If
                End If

                If novelty.TypeNovelty = 2 Then
                    'Sanciones
                    For Each itemVacationPeriod As VacationPeriod In employee.VacationPeriod
                        If itemVacationPeriod.InitialDatePeriod.Year >= novelty.RealDate.Year AndAlso itemVacationPeriod.PendingDays >= 15 Then
                            Dim NewInitialDatePeriod = DateAdd(DateInterval.Day, novelty.Days, itemVacationPeriod.InitialDatePeriod)
                            Dim NewEndDatePeriod = DateAdd(DateInterval.Day, novelty.Days, itemVacationPeriod.EndDatePeriod)

                            itemVacationPeriod.InitialDatePeriod = NewInitialDatePeriod
                            itemVacationPeriod.EndDatePeriod = NewEndDatePeriod
                            itemVacationPeriod.MarkAsModified()
                            _vacationPeriodRepository.SaveEntity(itemVacationPeriod)

                        End If
                    Next

                    employee.VacationLastDateLiquidation = DateAdd(DateInterval.Day, novelty.Days, employee.VacationLastDateLiquidation)
                    employee.MarkAsModified()
                    _employeeRepositoryCommit.SaveEntity(employee)

                    ExtendContractForVacationPeriods(employee)

                End If

                If listVacation IsNot Nothing AndAlso listVacation.Count > 0 And saveNovelty = True Then
                    If novelty.TypeNovelty = 1 Or (novelty.TypeNovelty = 3 And novelty.LicenseClass <> 4) Then
                        'novelty.RealInitialDateNovelty = novelty.RealDate
                        'novelty.RealEndDateNovelty = novelty.EndDate
                        Dim incorporationDate = (From e In listVacation
                                                 Select e.IncorporationDateReal).Max()

                        Dim VacationStarDate = (From e In listVacation
                                                Select e.VacationStartDate).FirstOrDefault()

                        Dim VacationEndingDate = (From e In listVacation
                                                  Select e.VacationEndDate).FirstOrDefault()

                        Dim VacationInitialNoveltyDate As Date
                        Dim VacationEndNoveltyDate As Date

                        'Novedades dentro del rango de vacaciones
                        If (novelty.RealDate >= VacationStarDate AndAlso novelty.RealDate <= VacationEndingDate) Or
                           (novelty.EndDate >= VacationStarDate AndAlso novelty.EndDate <= VacationEndingDate) Then

                            VacationInitialNoveltyDate = novelty.EndDate.AddDays(1)
                            VacationEndNoveltyDate = incorporationDate.AddDays(novelty.Days - 1)
                            'Sacamos los días disfrutados hasta el inicio de la incapidad
                            Dim daysInterrumted As Integer
                            Dim daysEnjoyedBeforeNovelty As Integer
                            Dim takenDaysBeforeNovelty As Integer
                            
                            If novelty.RealDate >= VacationStarDate Then
                                'Incapacidad empieza dentro de vacaciones
                                daysInterrumted = DateDiff(DateInterval.Day, VacationStarDate, novelty.RealDate)
                                
                                'Calcular días disfrutados hasta el inicio de la incapacidad
                                daysEnjoyedBeforeNovelty = DateDiff(DateInterval.Day, VacationStarDate, novelty.RealDate)
                                
                                'Calcular TakenDays proporcionalmente basado en EnjoyDays
                                Dim originalTakenDays = listVacation.FirstOrDefault.TakenDays
                                Dim originalEnjoyDays = listVacation.FirstOrDefault.EnjoyDays
                                If originalEnjoyDays > 0 Then
                                    takenDaysBeforeNovelty = Math.Round((originalTakenDays * daysEnjoyedBeforeNovelty) / originalEnjoyDays, 0)
                                Else
                                    takenDaysBeforeNovelty = 0
                                End If
                            Else
                                'Incapacidad termina dentro de vacaciones
                                daysInterrumted = DateDiff(DateInterval.Day, novelty.RealDate, VacationStarDate)
                                daysEnjoyedBeforeNovelty = 0
                                takenDaysBeforeNovelty = 0
                            End If
                            
                            'Insertamos un nuevo registro por vacaciones interrumpidas
                            Dim interruptedVacation As New Vacation
                            interruptedVacation.VacationPeriodId = listVacation.FirstOrDefault.VacationPeriodId
                            interruptedVacation.VacationStartDate = novelty.RealDate
                            interruptedVacation.VacationEndDate = novelty.EndDate
                            interruptedVacation.TypeLiquidation = listVacation.FirstOrDefault.TypeLiquidation
                            interruptedVacation.TypeVacation = 5 'Interrumpidas
                            interruptedVacation.TypePayment = listVacation.FirstOrDefault.TypePayment
                            interruptedVacation.TakenDays = 0
                            interruptedVacation.EnjoyDays = 0
                            interruptedVacation.IncorporationDate = novelty.EndDate.AddDays(1)
                            interruptedVacation.WorkedDays = 0
                            interruptedVacation.NotWorkedDays = 0
                            interruptedVacation.BaseLiquidation = 0
                            interruptedVacation.VacationValue = 0
                            interruptedVacation.HealthContribution = 0
                            interruptedVacation.PensionContribution = 0
                            interruptedVacation.VacationValueNet = 0
                            interruptedVacation.State = 3
                            interruptedVacation.TakenDaysReal = 0
                            interruptedVacation.IncorporationDateReal = novelty.EndDate.AddDays(1)
                            interruptedVacation.StateIncorporation = listVacation.FirstOrDefault.StateIncorporation
                            interruptedVacation.DaysDeferredPending = listVacation.FirstOrDefault.DaysDeferredPending
                            interruptedVacation.CreationUser = audit.CodeUser
                            interruptedVacation.CreationDate = DateTime.Now
                            _vacationRepository.SaveEntity(interruptedVacation)

                            Dim remainingDays As Integer
                            Dim remainingTakenDays As Integer
                            
                            If novelty.RealDate >= VacationStarDate Then
                                'Incapacidad empieza dentro de vacaciones
                                remainingDays = listVacation.FirstOrDefault.EnjoyDays - daysEnjoyedBeforeNovelty
                                remainingTakenDays = listVacation.FirstOrDefault.TakenDays - takenDaysBeforeNovelty
                            Else
                                'Incapacidad termina dentro de vacaciones - usar días de la incapacidad
                                remainingDays = novelty.Days
                                remainingTakenDays = 0
                            End If
                            
                            'Se agrega la reanudación si la incapacidad empieza dentro de vacaciones
                            If novelty.RealDate >= VacationStarDate Then
                                Dim newVacation As New Vacation
                                Dim originalVacation = listVacation.FirstOrDefault
                                
                                newVacation.VacationPeriodId = originalVacation.VacationPeriodId
                                newVacation.VacationStartDate = novelty.EndDate.AddDays(1)
                                newVacation.VacationEndDate = newVacation.VacationStartDate.AddDays(remainingDays - 1)
                                newVacation.TypeLiquidation = originalVacation.TypeLiquidation
                                newVacation.TypeVacation = originalVacation.TypeVacation
                                newVacation.TypePayment = originalVacation.TypePayment
                                
                                'Distribuir TakenDays proporcionalmente
                                newVacation.TakenDays = remainingTakenDays
                                newVacation.EnjoyDays = remainingDays
                                newVacation.IncorporationDate = newVacation.VacationEndDate.AddDays(1)
                                newVacation.WorkedDays = originalVacation.WorkedDays
                                newVacation.NotWorkedDays = originalVacation.NotWorkedDays

                                'Mantener valores originales sin distribución proporcional (por ahora)
                                newVacation.BaseLiquidation = originalVacation.BaseLiquidation
                                newVacation.VacationValue = originalVacation.VacationValue
                                newVacation.HealthContribution = originalVacation.HealthContribution
                                newVacation.PensionContribution = originalVacation.PensionContribution
                                newVacation.SolidarityFundValue = originalVacation.SolidarityFundValue
                                newVacation.VacationValueNet = originalVacation.VacationValueNet

                                newVacation.State = originalVacation.State
                                newVacation.TakenDaysReal = remainingTakenDays
                                newVacation.IncorporationDateReal = newVacation.VacationEndDate.AddDays(1)
                                newVacation.StateIncorporation = originalVacation.StateIncorporation
                                newVacation.DaysDeferredPending = originalVacation.DaysDeferredPending
                                newVacation.CreationUser = audit.CodeUser
                                newVacation.CreationDate = DateTime.Now
                                _vacationRepository.SaveEntity(newVacation)
                            End If

                            For Each vacation In listVacation
                                If novelty.RealDate >= VacationStarDate Then
                                    'Incapacidad empieza dentro de vacaciones
                                    vacation.VacationEndDate = IIf(daysInterrumted <= 0, vacation.VacationStartDate, novelty.RealDate.AddDays(-1))
                                    vacation.EnjoyDays = daysEnjoyedBeforeNovelty
                                    vacation.TakenDays = takenDaysBeforeNovelty

                                    'Actualizar fechas de incorporación
                                    vacation.IncorporationDate = novelty.RealDate
                                    vacation.IncorporationDateReal = novelty.RealDate

                                    'Los valores se mantienen sin modificar (por ahora)
                                Else
                                    'Incapacidad termina dentro de vacaciones - ajustar fecha de inicio
                                    vacation.VacationStartDate = novelty.EndDate.AddDays(1)
                                    vacation.EnjoyDays -= remainingDays
                                End If
                                vacation.MarkAsModified
                                _vacationRepository.SaveEntity(vacation)
                            Next
                        End If

                        While IsValidDay(holidays, contract.Group.PayrollParameter.SaturdayBusinessDay, contract.Group.PayrollParameter.SundayBusinessDay, VacationEndNoveltyDate) = False 'Calculo la fecha de incorporacion
                            VacationEndNoveltyDate = VacationEndNoveltyDate.AddDays(1)
                        End While

                        novelty.VacationInitialDateNovelty = VacationInitialNoveltyDate
                        novelty.VacationEndDateNovelty = VacationEndNoveltyDate

                        Dim noveltyVacaction As New Novelty()
                        noveltyVacaction.Days = novelty.Days
                        noveltyVacaction.RealDate = novelty.VacationInitialDateNovelty
                        noveltyVacaction.EndDate = novelty.VacationEndDateNovelty
                        noveltyVacaction.TypeNovelty = 99 'Tipo de novedad imaginaria para que me cree los detalles en schedule con la imagen de vacaciones
                        noveltyVacaction.LicenseClass = novelty.LicenseClass
                        noveltyVacaction.EmployeeId = novelty.EmployeeId
                        noveltyVacaction.GroupId = novelty.GroupId
                        SaveInability(noveltyVacaction, audit, False)
                    Else
                        result.StateResult = False
                        result.MessageResult.Add(New MessageResult("N010"))
                    End If
                End If
                If novelty.TypeNovelty = 3 And novelty.LicenseClass = 4 Then 'Si es licencia y si es Permiso a vacaciones
                    'If novelty.TypeNovelty = 3 And novelty.LicenseClass = 4 Then 'Si es licencia y si es Permiso a vacaciones
                    Dim listEmployee As New List(Of Domain.Payroll.Entities.Employee)
                    listEmployee.Add(employee)
                    listEmployee = _vacationDomain.LoadVacationPeriod(listEmployee)
                    Dim actionResultVacation = _vacationDomain.CalculateValueVacation(listEmployee, novelty.Days, 2, 3, 1, novelty.RealDate)
                    If actionResultVacation.MessageResult.Count > 0 Then
                        result.MessageResult.AddRange(actionResultVacation.MessageResult)
                        result.StateResult = False
                    End If
                    employee = actionResultVacation.ObjectEmbbeded.Item(0).MarkAsModified()
                    _employeeRepositoryCommit.SaveEntity(employee)
                End If
                If saveNovelty = True Then
                    _noveltyRepositoryCommit.SaveEntity(novelty)
                End If
                Dim listScheduleDetailEmployee As List(Of ScheduleDetail) = _scheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDate(novelty.EmployeeId, novelty.RealDate, novelty.EndDate)
                If flagDeleteUpdate = True Then
                    listScheduleDetailEmployee = listScheduleDetailEmployee.FindAll(Function(x) Not listScheduleDetail.Exists(Function(y) y.Id = x.Id))
                End If
                Dim listScheduleDeduction As List(Of NoveltyScheduleDetail) = New List(Of NoveltyScheduleDetail)
                Dim listScheduleModified As List(Of ScheduleDetail) = New List(Of ScheduleDetail)
                Dim timeDiscountPeriod As Dictionary(Of KeyValuePair(Of String, Integer), Integer) = New Dictionary(Of KeyValuePair(Of String, Integer), Integer)
                Dim errorMsg As String = Nothing
                _noveltyDomain.LoadListSchedule(listScheduleDetailEmployee, novelty, employee, listScheduleDeduction, listScheduleModified, timeDiscountPeriod, errorMsg) 'Cargo las lista de deducciones y modificaciones
                If Not String.IsNullOrEmpty(errorMsg) Then
                    result.StateResult = False
                    result.MessageResult.Add(New MessageResult(errorMsg))
                    Return result
                End If

                'Recorro los detalles de las novedades para que se inserten
                For Each item As NoveltyScheduleDetail In listScheduleDeduction
                    _noveltyScheduleRepositoryCommit.SaveEntity(item)
                Next
                'Recorro los detalles del calendario que se pueden guardar ya cambiado de plantilla

                Dim TmpListScheduleDetailEdit As New List(Of ScheduleDetail)
                Dim TmpListScheduleDetailInsert As New List(Of ScheduleDetail)

                Dim dictionarySchedule As Dictionary(Of String, Schedule) = New Dictionary(Of String, Schedule)()
                For Each item As ScheduleDetail In listScheduleModified
                    If item.Id = 0 Then
                        Dim period = item.DateDetail.ToString("MM/yyyy")
                        Dim schedule As Schedule
                        If dictionarySchedule.ContainsKey(period) Then
                            schedule = dictionarySchedule(period)
                        Else
                            schedule = _scheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(novelty.EmployeeId, item.DateDetail.ToString("MM/yyyy"), contract.FunctionalUnitId, False)
                            If schedule Is Nothing Then
                                schedule = New Schedule()
                                schedule.EmployeeId = novelty.EmployeeId
                                schedule.Period = period
                                schedule.FunctionalUnitId = contract.FunctionalUnitId
                            End If
                        End If
                        schedule.StartTracking()
                        schedule = _noveltyDomain.LoadDetailToDay(schedule, item)
                        If dictionarySchedule.ContainsKey(period) Then
                            dictionarySchedule(period) = schedule
                        Else
                            dictionarySchedule.Add(period, schedule)
                        End If


                        TmpListScheduleDetailInsert.Add(item)

                        Dim StringInsert As String = "INSERT INTO Payroll.ScheduleDetail(GroupId, EmployeeId, ContractId, CompanyId, BranchOfficeId, FunctionalUnitId, CenterCostId, ScheduleFunctionalUnitId, Letter, DateDetail, TotalNumberHours, Status, State)"
                        Dim Values As String = " VALUES (" + item.GroupId.ToString + "," + item.EmployeeId.ToString() + "," + item.ContractId.ToString() + "," + item.CompanyId.ToString + "," + item.BranchOfficeId.ToString() + "," + item.FunctionalUnitId.ToString() + "," + item.CenterCostId.ToString() + "," +
                        item.ScheduleFunctionalUnitId.ToString + ",'" + item.Letter.ToString() + "','" + item.DateDetail.ToString("yyyy-MM-dd") + "'," + item.TotalNumberHours.ToString() + "," + item.Status.ToString() + ",0)"
                        _scheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringInsert + Values)
                        unitWorkSchedule.Commit()
                        ' TmpListScheduleDetail.Add(item)
                        '_scheduleDetailRepositoryCommit.SaveEntity(item)
                    Else
                        TmpListScheduleDetailEdit.Add(item)

                        'Elimino los ScheduleDetailHour y ScheduleDetailConcept
                        Dim StringDeleteConcept As String = "DELETE FROM Payroll.ScheduleDetailConcept WHERE ScheduleDetailHourId IN (Select Id from Payroll.ScheduleDetailHour where ScheduleDetailId in ( " + item.Id.ToString() + "))"
                        _scheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringDeleteConcept)
                        unitWorkSchedule.Commit()

                        Dim StringDeleteHour As String = "DELETE FROM Payroll.ScheduleDetailHour WHERE ScheduleDetailId = " + item.Id.ToString()
                        _scheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringDeleteHour)
                        unitWorkSchedule.Commit()

                        Dim StringUpdate As String = "UPDATE Payroll.ScheduleDetail SET Letter = '" + item.Letter.ToString() + "' , TotalNumberHours = " + item.TotalNumberHours.ToString() + ", Status = " + item.Status.ToString() + ", ScheduleTemplateId = Null, State = 0 WHERE Id = " + item.Id.ToString()
                        _scheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringUpdate)
                        unitWorkSchedule.Commit()

                        'TmpListScheduleDetail.Add(item)
                        ' _scheduleDetailRepositoryCommit.SaveEntity(item)
                    End If
                Next

                'Recorro y guardo los calendarios que se agregaron los detalles de tipo novedad
                For Each item As KeyValuePair(Of String, Schedule) In dictionarySchedule
                    If item.Value.Id > 0 Then
                        'Ya existe el schedule
                        For Each ObjScheduleDetail As ScheduleDetail In TmpListScheduleDetailInsert

                            If item.Value.Period = ObjScheduleDetail.DateDetail.ToString("MM/yyyy") Then

                                Dim TmpScheduleDetail = _scheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDate(item.Value.EmployeeId, ObjScheduleDetail.DateDetail, ObjScheduleDetail.DateDetail).FirstOrDefault()

                                'Es del mismo mes
                                Dim TotalHours As Integer = item.Value.TotalHour - ObjScheduleDetail.TotalNumberHours

                                Dim Day As String

                                If ObjScheduleDetail.DateDetail.Day < 10 Then
                                    Day = "0" + ObjScheduleDetail.DateDetail.Day.ToString()
                                Else
                                    Day = ObjScheduleDetail.DateDetail.Day.ToString()
                                End If

                                Dim StringUpdate = "UPDATE Payroll.Schedule SET D" + Day + " = " + TmpScheduleDetail.Id.ToString() + ", TotalHour = " + TotalHours.ToString() + " WHERE Id = " + item.Value.Id.ToString
                                _scheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringUpdate)
                                unitWorkSchedule.Commit()
                            End If

                        Next
                    Else
                        _scheduleRepositoryCommit.SaveEntity(item.Value)
                    End If

                    '_scheduleRepositoryCommit.SaveEntity(item.Value)
                Next
                'Recorro los tiempos que se tienen que descontar del calendario
                For Each item As KeyValuePair(Of KeyValuePair(Of String, Integer), Integer) In timeDiscountPeriod
                    Dim schedule As Schedule = _scheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(novelty.EmployeeId, item.Key.Key, item.Key.Value)
                    If schedule IsNot Nothing Then
                        schedule.TotalHour = schedule.TotalHour - item.Value
                        Dim Hours As Integer = schedule.TotalHour

                        Dim StringUpdate = "UPDATE Payroll.Schedule SET TotalHour = " + Hours.ToString() + " WHERE Id = " + schedule.Id.ToString
                        _scheduleRepositoryCommit.UnitWork.ExecuteNonQuery(StringUpdate)
                        unitWorkSchedule.Commit()
                    End If
                    '_scheduleRepositoryCommit.SaveEntity(schedule)
                Next
                If result.StateResult = False Then
                    scope.Dispose()
                    Return result
                End If
                unitWorkEmployeeCommit.Commit()
                unitWorkVacation.Commit()
                unitWorkNoveltyDetailConcept.Commit()
                unitWorkNoveltyDetailHour.Commit()
                unitWorkNoveltyDetail.Commit()
                unitWorkSchedule.Commit()
                unitWorkScheduleDetail.Commit()
                unitWorkConsecutive.Commit()
                unitWork.Commit()
                scope.Complete()
                auditProcess = New IndigoAuditSimpleEntity(Of Novelty)(novelty, audit, status, noveltyTmp)
                auditProcess.Execute()
            End Using
            If novelty.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(novelty.GetType.Name, audit.Functional, novelty.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Novelty)(novelty, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf novelty.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(novelty.GetType.Name, audit.Functional, novelty.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Novelty)(novelty, audit, Infrastructure.CrossCutting.Audit.Actions.Update, noveltyTmp)
                auditObject.Execute()
            End If
            result.ObjectEmbbeded = novelty
            Return result
        Catch ex As Exception
            unitWorkNoveltyDetailConcept.RollbackChanges()
            unitWorkNoveltyDetailHour.RollbackChanges()
            unitWorkNoveltyDetail.RollbackChanges()
            unitWorkScheduleDetail.RollbackChanges()
            unitWorkSchedule.RollbackChanges()
            unitWorkConsecutive.RollbackChanges()
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Lista las Novedades de un empleado y filtra por tipo de novedad (Sancion, Licencia o Incapacidad)
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="typeNovelty">Tipo de la novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInabilityByTypeNovelty(employeeId As Integer, typeNovelty As Byte) As List(Of Novelty) Implements INoveltyAdminService.GetNoveltyByTypeNovelty
        Try
            Return _noveltyRepository.GetNoveltyByTypeNovelty(employeeId, typeNovelty)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista las incapacidades de un empleados que esten liquidadas o no 
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="liquidate">Si esta liquidado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInabilityLiquidate(employeeId As Integer, liquidate As Boolean) As List(Of Novelty) Implements INoveltyAdminService.GetNoveltyLiquidate
        Try
            Return _noveltyRepository.GetNoveltyLiquidate(employeeId, liquidate)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion que lista las novedades de un empleado en un rango establecido
    ''' </summary>
    ''' <param name="employeeId">id empleado</param>
    ''' <param name="fechaInicio">Fecha Inicio</param>
    ''' <param name="fechaFin">Fecha Fin</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyByEmployeeDateInitialEnd(employeeId As Integer, fechaInicio As Date, fechaFin As Date) As List(Of Novelty) Implements INoveltyAdminService.GetNoveltyByEmployeeDateInitialEnd
        Try
            Return _noveltyRepository.GetNoveltyByEmployeeDateInitialEnd(employeeId, fechaInicio, fechaFin)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una novedad por id
    ''' </summary>
    ''' <param name="id">id de la Incapacidad</param>
    ''' <returns>Novedad</returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyById(id As Integer) As Novelty Implements INoveltyAdminService.GetNoveltyById
        Try
            Return _noveltyRepository.GetNoveltyById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' obtiene un listado de los detalles que estan dentro de un rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="functionalUnitId">Id de la unidad funcional</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Public Function GetScheduleDetailByEmployeeBetweenDate(employeeId As Integer, dateInitial As Date, dateEnd As Date) As List(Of ScheduleDetail) Implements INoveltyAdminService.GetScheduleDetailByEmployeeBetweenDate
        Try
            Return _scheduleDetailRepository.GetScheduleDetailByEmployeeBetweenDate(employeeId, dateInitial, dateEnd)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina una Incapacidad
    ''' </summary>
    ''' <param name="novelty">Novedad</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Private Function DeleteNoveltyGeneral(ByRef noveltyRepository As INoveltyRepository, ByRef scheduleRepository As IScheduleRepository, novelty As Novelty, audit As AuditMessage) As Boolean
        Try
            If novelty Is Nothing Then
                Throw New ArgumentNullException("Incapacidad vacio")
            End If
            If (novelty.Status <> 0) Then 'Si el estado es diferente a 0 no se puede eliminar
                Throw New ArgumentException("No se puede eliminar una novedad que ya esta liquidada")
            End If
            Dim contract As Domain.Payroll.Entities.Contract = novelty.Employee.Contract.Where(Function(x) x.Valid = True).ToList().Item(0)
            Dim listScheduleDetail = _scheduleDetailRepository.GetScheduleDetailTypeNoveltyByEmployeeNoveltyId(novelty.EmployeeId, novelty.Id)
            'Diccionario el cual agrupa os calendarios por periodo y unidad funcional
            Dim scheduleDictionary As Dictionary(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) = _noveltyDomain.LoadDictionarySchedule(listScheduleDetail)
            For Each itemDictionary As KeyValuePair(Of KeyValuePair(Of String, Integer), List(Of ScheduleDetail)) In scheduleDictionary
                Dim schedule As Schedule = _scheduleRepository.GetScheduleByEmployeePeriodFunctionalUnit(novelty.EmployeeId, itemDictionary.Key.Key, itemDictionary.Key.Value)
                schedule.StartTracking()
                schedule = _noveltyDomain.DeleteDetailToSchedule(schedule, itemDictionary.Value)
                scheduleRepository.SaveEntity(schedule)
                '_scheduleRepository.SaveEntity(schedule)
            Next
            novelty.StartTracking()
            noveltyRepository.SaveEntity(novelty.MarkAsDeleted())
            'IndigoAuditSimpleEntity(Of Inability).Execute(inability, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, inability)
            Return True
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Obtiene la lista de novedades que estan dentro de un rango de fechas a excepcion de la que se envia como parametro
    ''' </summary>
    ''' <param name="noveltyId">id de la novedad que se va a exonerar</param>
    ''' <param name="initialDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha Fin</param>
    ''' <returns>Lista de novedades</returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyDistinctNoveltyBetweenDate(noveltyId As Integer, employeeId As Integer, initialDate As Date, endDate As Date) As List(Of Novelty) Implements INoveltyAdminService.GetNoveltyDistinctNoveltyBetweenDate
        Try
            Return _noveltyRepository.GetNoveltyDistinctNoveltyBetweenDate(noveltyId, employeeId, initialDate, endDate)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la novedad que tenga la fecha mas alta del mismo codigo consecutivo
    ''' </summary>
    ''' <param name="consecutive">Consecutivo a buscar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUltimateNoveltyConsecutive(consecutive As Integer) As Novelty Implements INoveltyAdminService.GetUltimateNoveltyConsecutive
        Try
            Return _noveltyRepository.GetUltimateNoveltyConsecutive(consecutive)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un promedio del ibc del empleado en los ultimos meses
    ''' </summary>
    ''' <param name="contractId">Id del contrato</param>
    ''' <param name="numberLastMonth">Meses que se quiere calcular</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAverageIBCLiquidationLastMonth(contractId As Integer, numberLastMonth As Integer) As Decimal Implements INoveltyAdminService.GetAverageIBCLiquidationLastMonth
        Try

            Dim ObjContract = _contractRepository.GetContractById(contractId, True)
            Dim PayrollEndDate = _liquidationDomain.GetEndPayrollDate(ObjContract.Group.Liquidation, ObjContract.Group.NextDateLiquidation)

            'Obtiene el contrato vigente a partir del contrato inicial
            Dim ValidContract As List(Of Domain.Payroll.Entities.Contract) =
            _contractRepository.GetContractByInitialNumber(contractId, ObjContract.EmployeeId).Where(Function(x) x.Valid).ToList()
            Dim SelectedContract = ValidContract.FirstOrDefault()

            'Obtiene el detalle del contrato vigente
            Dim ActualContract = _contractRepository.GetContractById(SelectedContract.Id, True)

            Dim listLiquidation As List(Of Liquidation) = _liquidationRepository.LiquidationLastMonths(contractId, numberLastMonth, PayrollEndDate)

            Return _noveltyDomain.CalculateAverageIBCLiquidationLastMonth(listLiquidation, numberLastMonth, ActualContract.ContractType?.SalaryType)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la Lista de Novedades por consecutivo
    ''' </summary>
    ''' <param name="Consecutive">Consecutivo</param>
    ''' <returns>List(Of Novelty)</returns>
    ''' <remarks></remarks>
    Public Function GetListNoveltyByConsecutive(Consecutive As Integer) As List(Of Novelty) Implements INoveltyAdminService.GetListNoveltyByConsecutive
        Try
            Return _noveltyRepository.GetListNoveltyByConsecutive(Consecutive)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function IsValidDay(listHoliday As List(Of Domain.Entities.Holiday), saturdayBusinessDay As Boolean, sundayBusinessDay As Boolean, dateValidation As Date) As Boolean
        Dim isHoliday = listHoliday.FindAll(Function(x) x.Holiday1 = dateValidation).Count
        Dim isValid As Boolean = True
        If isHoliday = 1 Then ' Si es festivo
            isValid = False
        ElseIf saturdayBusinessDay = False And dateValidation.DayOfWeek = DayOfWeek.Saturday Then
            isValid = False
        ElseIf sundayBusinessDay = False And dateValidation.DayOfWeek = DayOfWeek.Sunday Then
            isValid = False
        End If

        Return isValid
    End Function

    Public Function CalculateTwoFirstDays(employeeId As Integer, session As SessionValues) As ActionResult(Of Decimal) Implements INoveltyAdminService.CalculateTwoFirstDays
        Try
            Return _payrollNoveltyDomain.CalculateTwoFirstDays(employeeId, session)
        Catch ex As IndigoValidationException
            Return New ActionResult(Of Decimal) With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult(Of Decimal) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function
    ''' <summary>
    ''' Funcion para calcular el valor del concepto 
    ''' </summary>
    ''' <param name="contract"></param>
    ''' <param name="noveltyDays"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function CalculateNoveltyConcept(contract As Domain.Payroll.Entities.Contract, noveltyDays As Integer, session As SessionValues) As ActionResult(Of Decimal) Implements INoveltyAdminService.CalculateNoveltyConcept
        Try
            Return _payrollNoveltyDomain.CalculateNoveltyConcept(contract, noveltyDays, session)
        Catch ex As IndigoValidationException
            Return New ActionResult(Of Decimal) With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return New ActionResult(Of Decimal) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function
#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _noveltyDomain.Dispose()
                _vacationDomain.Dispose()
                _liquidationDomain.Dispose()
            End If
            _noveltyRepository = Nothing
            _consecutiveRepository = Nothing
            _scheduleRepository = Nothing
            _scheduleDetailRepository = Nothing
            _noveltyScheduleRepository = Nothing
            _scheduleRepositoryCommit = Nothing
            _scheduleDetailRepositoryCommit = Nothing
            _noveltyRepositoryCommit = Nothing
            _noveltyDomain = Nothing
            _employeeRepository = Nothing
            _liquidationRepository = Nothing
            _noveltyScheduleConceptRepository = Nothing
            _noveltyScheduleHourRepository = Nothing
            _noveltyScheduleRepositoryCommit = Nothing
            _vacationDomain = Nothing
            _vacationRepository = Nothing
            _employeeRepositoryCommit = Nothing
            _vacationPeriodRepository = Nothing
            _liquidationDomain = Nothing
            _contractRepository = Nothing
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
