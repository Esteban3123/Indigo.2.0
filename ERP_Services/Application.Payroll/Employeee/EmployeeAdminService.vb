'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar Narvaez
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports System.Transactions
Imports Infrastructure.CrossCutting.Base

Public Class EmployeeAdminService
    Implements IEmployeeAdminService

    ''' <summary>
    ''' Repositorio de empleados
    ''' </summary>
    Private _EmployeeRepository As IEmployeeRepository

    ''' <summary>
    ''' Repositorio de empleados
    ''' </summary>
    Private _EmployeeRepositoryCommit As IEmployeeRepository

    ''' <summary>
    ''' Repositorio para actualizar el empleado
    ''' </summary>
    Private _contractRepositoryCommit As IEmployeeRepository

    ''' <summary>
    ''' Repositorio para actualizar el empleado
    ''' </summary>
    Private _employeeUpdateRepository As IEmployeeRepository

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
    Private _scheduleDetailEditRepository As IScheduleDetailRepository

    ''' <summary>
    ''' Repositorio de Schedule
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleDetailHourRepository As IScheduleDetailHourRepository

    ''' <summary>
    ''' Repositorio de Schedule
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleDetailConceptRepository As IScheduleDetailConceptRepository

    ''' <summary>
    ''' Repositorio de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Repositorio de liquidacion de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractLiquidation As IContractLiquidationRepository

    ''' <summary>
    ''' Repositorio de liquidacion de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private _agreementsRepository As IAgreementsCRepository

    ''' <summary>
    ''' Repositorio de Conceptos Manuales
    ''' </summary>
    Private _manualConceptRepository As IManualConcepts

    ''' <summary>
    ''' Repositorio de Retroactividad
    ''' </summary>
    Private _retroactiveCRepository As IRetroactiveCRepository

    ''' <summary>
    ''' Repositorio de Primas
    ''' </summary>
    Private _incentivePaymentRepository As IIncentivePaymentRepository

    ''' <summary>
    ''' Repositorio de Cesantias
    ''' </summary>
    Private _unemployementRepository As IUnemployedLiquidationRepository

    Private _contractRepository As IContractRepository

    ''' <summary>
    ''' Respositorio de rentas
    ''' </summary>
    Private _ExemptIncomeRepository As Domain.Entities.IExemptIncomeRepository

    ''' <summary>
    ''' Repositorio de Terceros
    ''' </summary>
    Private _ThirdPartyRepository As Domain.Entities.IThirdPartyRepository

    ''' <summary>
    ''' Repositorio de Auditoría de Contratos
    ''' </summary>
    Private _contractAuditRepository As IContractAuditRepository

    ''' <summary>
    ''' Repositorio de Configuración de Nómina (Global)
    ''' </summary>
    Private _PayrollSettingsRepository As IPayrollSettingsRepository

    ''' <summary>
    ''' inicia el repositorio de empleados
    ''' </summary>
    ''' <param name="employeeRepository">Repositorio de empleados</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal employeeRepository As IEmployeeRepository,
                   employeeRepositoryCommit As IEmployeeRepository,
                   scheduleRepository As IScheduleRepository,
                   scheduleDetailRepository As IScheduleDetailRepository,
                   scheduleDetailEditRepository As IScheduleDetailRepository,
                   scheduleDetailHourRepository As IScheduleDetailHourRepository,
                   scheduleDetailConceptRepository As IScheduleDetailConceptRepository,
                   liquidationRepository As IPayrollLiquidationRepository,
                   contractLiquidation As IContractLiquidationRepository,
                   agreementsRepository As IAgreementsCRepository,
                   employeeUpdateRepository As IEmployeeRepository,
                   contractRepositoryCommit As IEmployeeRepository,
                   manualConceptRepository As IManualConcepts,
                   retroactiveCRepository As IRetroactiveCRepository,
                   incentivePaymentRepository As IIncentivePaymentRepository,
                   unemployementRepository As IUnemployedLiquidationRepository,
                   contractRepository As IContractRepository,
                   exemptIncomeRepository As Domain.Entities.IExemptIncomeRepository,
                   ThirdPartyRepository As Domain.Entities.IThirdPartyRepository,
                   contractAuditRepository As IContractAuditRepository,
                   payrollSettingsRepository As IPayrollSettingsRepository)

        If (employeeRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de empleados vacio")
        End If
        _EmployeeRepository = employeeRepository
        _EmployeeRepositoryCommit = employeeRepositoryCommit
        _scheduleRepository = scheduleRepository
        _scheduleDetailRepository = scheduleDetailRepository
        _scheduleDetailEditRepository = scheduleDetailEditRepository
        _scheduleDetailHourRepository = scheduleDetailHourRepository
        _scheduleDetailConceptRepository = scheduleDetailConceptRepository
        _liquidationRepository = liquidationRepository
        _contractLiquidation = contractLiquidation
        _agreementsRepository = agreementsRepository
        _employeeUpdateRepository = employeeUpdateRepository
        _contractRepositoryCommit = contractRepositoryCommit
        _manualConceptRepository = manualConceptRepository
        _retroactiveCRepository = retroactiveCRepository
        _incentivePaymentRepository = incentivePaymentRepository
        _unemployementRepository = unemployementRepository
        _contractRepository = contractRepository
        _ExemptIncomeRepository = exemptIncomeRepository
        _ThirdPartyRepository = ThirdPartyRepository
        _contractAuditRepository = contractAuditRepository
        _PayrollSettingsRepository = payrollSettingsRepository

    End Sub

    ''' <summary>
    ''' Elimina un empleado
    ''' </summary>
    ''' <param name="employee">Empleado</param>
    ''' <returns></returns>
    Public Function DeleteEmployee(employee As Employee, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult Implements IEmployeeAdminService.DeleteEmployee
        If employee Is Nothing Then
            Throw New ArgumentNullException("Empleado vacio")
        End If
        Dim unitWork As IUnitWork = _EmployeeRepository.UnitWork
        Dim result As New ActionMessageResult()
        Try
            result.StateResult = False
            result.MessageResult = New List(Of MessageResult)
            For Each contract As Contract In employee.Contract
                If _liquidationRepository.LiquidationConfirmatedByContract(contract.Id) = True Then 'Tiene liquidaciones ese contrato
                    result.MessageResult.Add(New MessageResult("E001"))
                    Exit For
                End If
            Next
            If _contractLiquidation.GetContractLiquidationByEmployee(employee.Id).Count > 0 Then 'Ya tiene liquidaciones de contrato
                result.MessageResult.Add(New MessageResult("E002"))
            End If
            If employee.Novelty.Count > 0 Then 'El empleado tiene novedades
                result.MessageResult.Add(New MessageResult("E003"))
            End If
            If _scheduleDetailRepository.GetScheduleDetailByEmployee(employee.Id).Count > 0 Then 'El empleado tiene cuadro de turnos
                result.MessageResult.Add(New MessageResult("E004"))
            End If
            If _agreementsRepository.GetAgreementsByEmployee(employee.Id).Count > 0 Then
                result.MessageResult.Add(New MessageResult("E005"))
            End If
            If result.MessageResult.Count > 0 Then
                Return result
            End If
            result.StateResult = True
            While employee.AuthorizationConceptEmployee.Count > 0 'Elimino las autorizacion de conceptos que tenga
                Dim index = employee.AuthorizationConceptEmployee.Count - 1
                employee.AuthorizationConceptEmployee(index).MarkAsDeleted()
            End While
            While employee.CostDistributions.Count > 0 'Elimino la distribucion de costos
                Dim index = employee.CostDistributions.Count - 1
                employee.CostDistributions(index).MarkAsDeleted()
            End While
            While employee.Relationship.Count > 0 'Elimino los parentescos
                Dim index = employee.Relationship.Count - 1
                employee.Relationship(index).MarkAsDeleted()
            End While
            While employee.VacationPeriod.Count > 0 'Elimino las vacaciones si tiene
                Dim index = employee.VacationPeriod.Count - 1
                Dim vacation = employee.VacationPeriod(index)
                While vacation.Vacation.Count > 0
                    vacation.Vacation(index).MarkAsDeleted()
                End While
                vacation.MarkAsDeleted()
            End While
            While employee.Contract.Count > 0 'Elimino los contratos
                Dim index = employee.Contract.Count - 1
                Dim contract = employee.Contract(index)
                While contract.FundContract.Count > 0
                    contract.FundContract(contract.FundContract.Count - 1).MarkAsDeleted()
                End While
                contract.MarkAsDeleted()
            End While
            employee.MarkAsDeleted()
            _EmployeeRepository.SaveEntity(employee)
            unitWork.Commit()
            'IndigoAuditSimpleEntity(Of Employee).Execute(employee, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, employee)
            Return result
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un empleado con todos sus agregados atraves del nit (ASYNC)
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns>Task con el Employee</returns>
    Public Async Function GetEmployeeAsync(nit As String) As Task(Of Employee) Implements IEmployeeAdminService.GetEmployeeAsync
        If String.IsNullOrEmpty(nit) Then
            Throw New ArgumentNullException("Nit vacio")
        End If
        Try
            Return Await _EmployeeRepository.GetEmployeeAsync(nit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los empleados con agregados de tercero y persona
    ''' </summary>
    ''' <returns>Lista de empleados</returns>
    Public Function ListAllEmployee() As List(Of Employee) Implements IEmployeeAdminService.ListAllEmployee
        Try
            Return _EmployeeRepository.ListAllEmployee()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o edita un Empleado y todos sus agregados
    ''' </summary>
    ''' <returns></returns>
    Public Function SaveEmployee(employee As Employee, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional lisExemptIncome As List(Of Domain.Entities.ExemptIncome) = Nothing, Optional ByVal exemptIncomeListToDelete As List(Of Domain.Entities.ExemptIncome) = Nothing) As ActionMessageResult(Of Employee) Implements IEmployeeAdminService.SaveEmployee

        Dim result As New ActionMessageResult(Of Employee)()

        result.StateResult = True
        If employee Is Nothing Then
            Throw New ArgumentNullException("Empleado vacio")
            result.StateResult = False
        End If

        'Unidades de trabajo
        Dim employeeUnitWork As IUnitWork = _EmployeeRepositoryCommit.UnitWork
        Dim employeeUpdateUnitWork As IUnitWork = _employeeUpdateRepository.UnitWork
        Dim scheduleRepositoryUnitWork As IUnitWork = _scheduleRepository.UnitWork
        Dim scheduleDetailRepositoryUnitWork As IUnitWork = _scheduleDetailRepository.UnitWork
        Dim scheduleDetailRepositoryEditUnitWork As IUnitWork = _scheduleDetailEditRepository.UnitWork
        Dim scheduleDetailHourRepositoryUnitWork As IUnitWork = _scheduleDetailHourRepository.UnitWork
        Dim scheduleDetailConceptRepositoryUnitWork As IUnitWork = _scheduleDetailConceptRepository.UnitWork
        Dim contractUnitWork As IUnitWork = _contractRepositoryCommit.UnitWork

        Try

            Dim ContractDelete As Boolean = False
            Dim employeeAux As Employee = Nothing
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                If employee.ChangeTracker.State = ObjectState.Modified Then
                    employeeAux = _EmployeeRepository.GetEmployeeById(employee.Id, False)
                End If

                'Verifica si hay objetos removidos 
                If employee.ChangeTracker.ObjectsRemovedFromCollectionProperties.Any() Then
                    'Verifica si el objeto que se removio es de tipo Contrato
                    If employee.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey(GetType(Contract).Name) Then
                        'Obtengo el listado de contratos removidos
                        Dim contractList = employee.ChangeTracker.ObjectsRemovedFromCollectionProperties(GetType(Contract).Name)
                        'Recorre el listado de contratos para eliminar los Schedules
                        For Each c As Contract In contractList

                            ContractDelete = True

                            Dim scheduleDetailList As List(Of ScheduleDetail) = _scheduleDetailRepository.GetScheduleDetailByContractId(c.Id)
                            'Valido si el empleado tiene cuadros de turnos
                            If scheduleDetailList.Any() Then
                                result.MessageResult.Add(New MessageResult("-999", String.Format("Debe eliminar los cuadros de turno registrados en el contrato {0}", c.Id)))
                                result.StateResult = False
                            End If

                            'Valido si el empleado tiene Liquidaciones de Nómina
                            If _liquidationRepository.LiquidationByContract(c.Id) = True Then 'Tiene liquidaciones ese contrato
                                result.MessageResult.Add(New MessageResult("-999", "El contrato con Id " + c.Id.ToString() + " tiene Liquidaciones de Nómina"))
                                result.StateResult = False
                            End If

                            If _contractLiquidation.GetContractLiquidationByContractId(c.Id) IsNot Nothing Then 'Ya tiene liquidaciones de contrato
                                result.MessageResult.Add(New MessageResult("-999", "El contrato con Id " + c.Id.ToString() + " tiene Liquidación de Contrato"))
                                result.StateResult = False
                            End If

                            If _manualConceptRepository.GetManualConceptsContractId(c.Id).Count > 0 Then ' tiene Conceptos Manuales creados
                                result.MessageResult.Add(New MessageResult("-999", "El contrato con Id " + c.Id.ToString() + " tiene Conceptos Manuales de Contrato"))
                                result.StateResult = False
                            End If

                            If _retroactiveCRepository.GetListRetroactiveByContractId(c.Id).Count > 0 Then ' tiene Retroactividad
                                result.MessageResult.Add(New MessageResult("-999", "El contrato con Id " + c.Id.ToString() + " tiene Retroactividad"))
                                result.StateResult = False
                            End If

                            If _incentivePaymentRepository.GetIncentivePaymentByContractId(c.Id).Count > 0 Then ' tiene Primas
                                result.MessageResult.Add(New MessageResult("-999", "El contrato con Id " + c.Id.ToString() + " tiene Primas Liquidadas"))
                                result.StateResult = False
                            End If

                            If _unemployementRepository.GetUnemploymentLiquidationByContractId(c.Id).Count > 0 Then ' tiene Cesantias
                                result.MessageResult.Add(New MessageResult("-999", "El contrato con Id " + c.Id.ToString() + " tiene Cesantias Liquidadas"))
                                result.StateResult = False
                            End If

                            If Not result.StateResult Then
                                Return result
                            End If

                        Next
                    End If
                End If

                'Se validara la longitud del Nit solo si es un registro nuevo
                If employee.ThirdParty.Person.Id = 0 Then
                    Dim validation = _ThirdPartyRepository.ValidateLenghtNit(employee.ThirdParty.Person.IdentificationNumber, employee.ThirdParty.Person.DocumentTypeAbbreviation)
                    If Not validation.StateResult Then
                        Throw New Exception(validation.Message)
                    End If
                End If

                If employee.TradeUnion = 3 AndAlso employee.TradeUnionEmployee.Any() Then
                    For Each item In employee.TradeUnionEmployee
                        If item.TradeUnion IsNot Nothing Then
                            Dim tradeunionID = item.TradeUnion.Id
                            item.TradeUnion = Nothing
                            item.TradeUnionId = tradeunionID
                        End If

                        If item.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            item.CreationUser = audit.CodeUser
                            item.CreationDate = Date.Now
                        End If
                    Next
                End If

                If employee.ChangeTracker.State = ObjectState.Added Then
                    'Si se agrega
                    Dim ObjTmpContract = employee.Contract.FirstOrDefault()
                    If ObjTmpContract IsNot Nothing Then
                        employee.VacationLastDateLiquidation = ObjTmpContract.JobBondingDate
                    Else
                        employee.VacationLastDateLiquidation = Date.Now()
                    End If
                End If

                If employee.Contract?.Any() And Not ContractDelete Then

                    ' ANTES: No se validaba ni se manejaban ajustes extemporáneos
                    ' DESPUÉS: Se obtienen los parámetros GLOBALES de nómina (PayrollSettings) para validar ajustes extemporáneos
                    Dim payrollSettings As PayrollSettings = _PayrollSettingsRepository.GetSettingPayroll()
                    Dim allowedMonths As Byte = If(payrollSettings IsNot Nothing,
                                                    payrollSettings.AllowedMonthsForExtemporaneousAdjustments,
                                                    CByte(0))

                    For Each ObjContractAdd As Contract In employee.Contract

                        ' VALIDACIÓN DE AJUSTES EXTEMPORÁNEOS
                        ' Se aplica solo para contratos nuevos (Otro Sí) que son RowType = 2
                        If ObjContractAdd.ChangeTracker.State = ObjectState.Added AndAlso ObjContractAdd.RowType = 2 Then

                            ' Obtener el contrato original para comparación
                            Dim originalContract As Contract = Nothing
                            If ObjContractAdd.InitialContractNumber > 0 Then
                                originalContract = employee.Contract.FirstOrDefault(Function(c) c.Id = ObjContractAdd.InitialContractNumber)
                            End If

                            If originalContract IsNot Nothing Then
                                ' Validar si es un ajuste extemporáneo
                                Using extemporaneousService As IExtemporaneousAdjustmentDomain = New ExtemporaneousAdjustmentDomain()
                                    Dim errorMessage As String = String.Empty
                                    Dim isValidDate As Boolean = extemporaneousService.ValidateExtemporaneousDate(
                                        ObjContractAdd.ContractInitialDate,
                                        allowedMonths,
                                        errorMessage)

                                    If Not isValidDate Then
                                        ' La fecha no es válida para ajuste extemporáneo
                                        result.MessageResult.Add(New MessageResult("-999", errorMessage))
                                        result.StateResult = False
                                        Return result
                                    End If

                                    ' Si la fecha es extemporánea, validar campos modificados
                                    If extemporaneousService.IsExtemporaneousDate(ObjContractAdd.ContractInitialDate) Then

                                        Dim invalidFields As New List(Of String)()
                                        Dim onlyAllowedFieldsChanged As Boolean = extemporaneousService.ValidateOnlyAllowedFieldsChanged(
                                            originalContract,
                                            ObjContractAdd,
                                            invalidFields)

                                        If Not onlyAllowedFieldsChanged OrElse invalidFields.Count > 0 Then
                                            ' Se intentaron modificar campos no permitidos
                                            Dim fieldsMessage As String = String.Join(", ", invalidFields)
                                            result.MessageResult.Add(New MessageResult("-999",
                                                "Solo es posible registrar cambios extemporáneos para cargo o salario básico. " &
                                                "Los siguientes campos no pueden modificarse: " & fieldsMessage))
                                            result.StateResult = False
                                            Return result
                                        End If

                                        ' Marcar como ajuste extemporáneo
                                        ' ANTES: IsExtemporaneousChange siempre era False por defecto
                                        ' DESPUÉS: Se marca como True cuando cumple todas las condiciones
                                        ObjContractAdd.IsExtemporaneousChange = True
                                    Else
                                        ' Es un cambio normal (fecha del mes actual o futuro)
                                        ObjContractAdd.IsExtemporaneousChange = False
                                    End If
                                End Using

                                ' IMPORTANTE: En ajustes extemporáneos NO se deben ejecutar:
                                ' - Recálculos de nómina de meses anteriores
                                ' - Generación de retroactivos automáticos
                                ' - Alteración de provisiones o contabilidad
                                ' Esto se maneja en los procesos de liquidación verificando el flag IsExtemporaneousChange

                            Else
                                ' Es un contrato base nuevo, no es ajuste extemporáneo
                                ObjContractAdd.IsExtemporaneousChange = False
                            End If
                        Else
                            ' Contratos base (RowType = 1) o modificaciones, no son ajustes extemporáneos
                            If ObjContractAdd.ChangeTracker.State = ObjectState.Added Then
                                ObjContractAdd.IsExtemporaneousChange = False
                            End If
                        End If

                        '' Se validan columnas de Auditoría Simple en la Tabla de fondos de Contratos
                        If ObjContractAdd.FundContract IsNot Nothing And ObjContractAdd.FundContract.Any() Then
                            For Each ObjTmpFundContract In ObjContractAdd.FundContract
                                If ObjTmpFundContract.ChangeTracker.State = ObjectState.Added Then
                                    ObjTmpFundContract.CreationDate = Date.Now
                                    ObjTmpFundContract.CreationUser = audit.CodeUser
                                End If

                                If ObjTmpFundContract.ChangeTracker.State = ObjectState.Modified Then
                                    ObjTmpFundContract.ModificationDate = Date.Now
                                    ObjTmpFundContract.ModificationUser = audit.CodeUser
                                End If
                            Next
                        End If

                        '' Se valida para que, si es un Empleado Nuevo, el Campo de Fecha Última Vacaciones sea la misma de la Fecha de Contratación
                        If employee.ChangeTracker.State = ObjectState.Added Then
                            If ObjContractAdd.ChangeTracker.State = ObjectState.Added Then
                                employee.VacationLastDateLiquidation = ObjContractAdd.JobBondingDate
                            End If
                        End If
                    Next

                    '' Se guarda auditoría cuando se modifica la unidad funcional del contrato
                    If employee.ChangeTracker.State = ObjectState.Modified Then
                        ' Obtener el contrato original para comparar
                        Dim originalContract As Contract = Nothing
                        If employeeAux IsNot Nothing AndAlso employeeAux.Contract IsNot Nothing Then
                            originalContract = employeeAux.Contract.FirstOrDefault(Function(c) c.Status = 1)
                        End If
                        Dim ObjeContractModified = employee.Contract.FirstOrDefault(Function(c) c.Status = 1)
                        ' Verificar si cambió la unidad funcional
                        If originalContract IsNot Nothing Then

                            Dim oldValue As Integer = originalContract.FunctionalUnitId
                            Dim newValue As Integer = ObjeContractModified.FunctionalUnitId

                            If oldValue <> newValue Then
                                Dim contractAudit As New ContractAudit()
                                contractAudit.ContractId = ObjeContractModified.Id
                                contractAudit.Type = "Unidad Funcional"
                                contractAudit.FieldName = "FunctionalUnitId"
                                contractAudit.ValueOld = oldValue
                                contractAudit.ValueNew = newValue
                                contractAudit.UserCode = audit.CodeUser
                                contractAudit.Date = Date.Now()
                                contractAudit.MarkAsAdded()
                                _contractAuditRepository.SaveEntity(contractAudit)
                            End If
                        End If
                    End If

                End If

                'Eliminar datos personales direccion
                Dim addressDetail As List(Of Address)
                If employee.ThirdParty.Person.Address IsNot Nothing Then
                    addressDetail = (From e In employee.ThirdParty.Person.Address Where e.ChangeTracker.State = ObjectState.Deleted Select e).ToList()
                    For Each item In addressDetail
                        employee.ThirdParty.Person.Address.Remove(item)
                    Next
                End If

                'Eliminar datos personales telefono
                Dim phoneDetail As List(Of Phone)
                If employee.ThirdParty.Person.Phone IsNot Nothing Then
                    phoneDetail = (From e In employee.ThirdParty.Person.Phone Where e.ChangeTracker.State = ObjectState.Deleted Select e).ToList()
                    For Each item In phoneDetail
                        employee.ThirdParty.Person.Phone.Remove(item)
                    Next
                End If

                'Eliminar datos personales email
                Dim emailDetail As List(Of Email)
                If employee.ThirdParty.Person.Email IsNot Nothing Then
                    emailDetail = (From e In employee.ThirdParty.Person.Email Where e.ChangeTracker.State = ObjectState.Deleted Select e).ToList()
                    For Each item In emailDetail
                        employee.ThirdParty.Person.Email.Remove(item)
                    Next
                End If

                _EmployeeRepositoryCommit.SaveEntity(employee)
                scheduleRepositoryUnitWork.Commit()
                scheduleDetailConceptRepositoryUnitWork.Commit()
                scheduleDetailHourRepositoryUnitWork.Commit()
                scheduleDetailRepositoryEditUnitWork.Commit()
                _EmployeeRepositoryCommit.SaveEntity(employee)
                _contractAuditRepository.UnitWork.Commit()
                employeeUnitWork.Commit()

                'Se vuelve a consultar el empleado para poder asignarle el id del contrato cuando es la primera vez que se guarda el contrato
                If employee.Contract?.Any() AndAlso (From x In employee.Contract Where x.InitialContractNumber = 0 Select x).Any() Then

                    'Se consulta el empleado para poder actualizar el campo
                    Dim employeeTemp = _EmployeeRepository.GetEmployeeSimpleById(employee.Id)

                    'Se asigna el nuevo valor
                    For Each item In (From x In employeeTemp.Contract Where x.InitialContractNumber = 0 Select x).ToList()
                        item.InitialContractNumber = item.Id
                        item.MarkAsModified()
                    Next

                    'Se marca el empleado como modificado
                    employeeTemp.MarkAsModified()

                    'Se procede a guardar
                    _employeeUpdateRepository.SaveEntity(employeeTemp)
                    employeeUpdateUnitWork.Commit()

                End If

                'Se procede a guardar los detalles de la renta excenta
                If lisExemptIncome.Count > 0 Then
                    SaveExemptIncome(lisExemptIncome, audit)
                End If

                'Se procede a eliminar la renta excenta
                If exemptIncomeListToDelete.Count > 0 Then
                    DeleteExemptIncome(exemptIncomeListToDelete, audit)
                End If

                'Se valida que sea un Contrato Eliminado, y que si el empleado tiene más contratos, active el anterior
                If ContractDelete Then

                    If employee.Contract?.Any() Then
                        Dim EmployeeActivate = _EmployeeRepository.GetEmployeeSimpleById(employee.Id)

                        Dim ContractActivate = EmployeeActivate.Contract.OrderByDescending(Function(x) x.Id).FirstOrDefault()

                        For Each ObjContract As Contract In EmployeeActivate.Contract
                            If ObjContract.Id = ContractActivate.Id Then
                                ObjContract.Status = 1
                                ObjContract.Valid = True
                                ObjContract.MarkAsModified()
                            End If
                        Next

                        EmployeeActivate.MarkAsModified()

                        _contractRepositoryCommit.SaveEntity(EmployeeActivate)
                        contractUnitWork.Commit()

                    End If
                End If

                scope.Complete()
            End Using

            If employee.CostCenter IsNot Nothing Then
                Dim costCenterId As Integer = employee.CostCenterId
                employee.CostCenter = Nothing
                employee.CostCenterId = costCenterId
            End If

            If employee.WorkCenter IsNot Nothing Then
                Dim workCenterId As Integer = employee.WorkCenterId
                employee.WorkCenter = Nothing
                employee.WorkCenterId = workCenterId
            End If

            '/*******Auditoria ******/
            If employee.ChangeTracker.State = ObjectState.Added Then
                '/******* Auditoria Basica ****/
                IndigoAuditBasic.Execute(employee.GetType().Name, audit.Functional, employee.Id, audit.NameUser, audit.IdUser, audit.WindowsUser, Date.Now(), Infrastructure.CrossCutting.Base.ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/******* Auditoria Avanzada *******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Employee)(employee, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf employee.ChangeTracker.State = ObjectState.Modified Then
                '/******* Auditoria Basica ****/
                IndigoAuditBasic.Execute(employee.GetType().Name, audit.Functional, employee.Id, audit.NameUser, audit.IdUser, audit.WindowsUser, Date.Now(), Infrastructure.CrossCutting.Base.ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/******* Auditoria Avanzada *******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Employee)(employee, audit, Infrastructure.CrossCutting.Audit.Actions.Update, employeeAux)
                auditObject.Execute()
            End If

            Return result

        Catch ex As Exception
            employeeUnitWork.RollbackChanges()
            employeeUpdateUnitWork.RollbackChanges()
            scheduleRepositoryUnitWork.RollbackChanges()
            scheduleDetailRepositoryUnitWork.RollbackChanges()
            scheduleDetailHourRepositoryUnitWork.RollbackChanges()
            scheduleDetailConceptRepositoryUnitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("-999", ex.Message))
            Return result
        End Try
    End Function



    ''' <summary>
    ''' Obtiene un empleado y los agregaos de contratos y fondos de contratos atraves del nit del tercero
    ''' </summary>
    ''' <param name="nit">nit del tercero</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeBasicContract(nit As String) As Employee Implements IEmployeeAdminService.GetEmployeeBasicContract
        If String.IsNullOrEmpty(nit) Then
            Throw New ArgumentNullException("Nit Vacio")
        End If
        Try
            Return _EmployeeRepository.GetEmployeeBasicContract(nit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los empleados que tiene una unidad funcional
    ''' </summary>
    ''' <param name="functionalUnitId">Id unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeesByFunctionalUnit(functionalUnitId As Integer) As List(Of Employee) Implements IEmployeeAdminService.GetEmployeesByFunctionalUnit
        If functionalUnitId = 0 Then
            Throw New ArgumentNullException("Id Unidad Funcional vacio")
        End If
        Try
            Return _EmployeeRepository.GetEmployeesByFunctionalUnit(functionalUnitId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los empleados que tiene una unidad funcional
    ''' </summary>
    ''' <param name="functionalUnitId">Id unidad funcional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeesByGroupId(groupId As Integer) As List(Of Employee) Implements IEmployeeAdminService.GetEmployeesByGroupId
        If groupId = 0 Then
            Throw New ArgumentNullException("Id Grupo vacio")
        End If
        Try
            Return _EmployeeRepository.GetEmployeeByGroup(groupId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del id
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeById(id As Integer) As Employee Implements IEmployeeAdminService.GetEmployeeById
        Try
            Return _EmployeeRepository.GetEmployeeById(id)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Obtiene un empleado y todos sus agregados atraves del id
    ''' </summary>
    ''' <param name="id">id del empleado</param>
    ''' <returns>Empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeByIdForContractLiquidation(id As Integer) As Employee Implements IEmployeeAdminService.GetEmployeeByIdForContractLiquidation
        Try
            Return _EmployeeRepository.GetEmployeeByIdForContractLiquidation(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


    Public Function GetEmployeePensionary() As List(Of Employee) Implements IEmployeeAdminService.GetEmployeePensionary
        Try
            Return _EmployeeRepository.GetEmployeePensionary()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Metodo para guardar rentas exentas
    ''' </summary>
    ''' <param name="exemptIncome"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveExemptIncome(exemptIncome As List(Of Domain.Entities.ExemptIncome), audit As AuditMessage) As ActionMessageResult(Of Domain.Entities.ExemptIncome) Implements IEmployeeAdminService.SaveExemptIncome
        Dim unitwork As IUnitWork = _ExemptIncomeRepository.UnitWork
        Dim result As New ActionMessageResult(Of Domain.Entities.ExemptIncome)()
        Try
            result.StateResult = True
            For Each item In exemptIncome
                Me._ExemptIncomeRepository.SaveEntity(item)
            Next
            unitwork.Commit()
            Return result
        Catch ex As Exception
            unitwork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function
    ''' <summary>
    ''' Metodo para eliminar Rentas
    ''' </summary>
    ''' <param name="exemptIncome"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteExemptIncome(exemptIncome As List(Of Domain.Entities.ExemptIncome), audit As AuditMessage) As ActionMessageResult(Of Domain.Entities.ExemptIncome) Implements IEmployeeAdminService.DeleteExemptIncome
        Dim result As New ActionMessageResult(Of Domain.Entities.ExemptIncome)()
        Dim unitwork As IUnitWork = _ExemptIncomeRepository.UnitWork

        Try
            result.StateResult = True
            For Each item In exemptIncome

                item.MarkAsDeleted()
                _ExemptIncomeRepository.DeleteEntity(item)
            Next
            unitwork.Commit()
            Return result
        Catch ex As Exception
            unitwork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _EmployeeRepository = Nothing
            _EmployeeRepositoryCommit = Nothing
            _scheduleRepository = Nothing
            _scheduleDetailRepository = Nothing
            _scheduleDetailEditRepository = Nothing
            _scheduleDetailHourRepository = Nothing
            _scheduleDetailConceptRepository = Nothing
            _liquidationRepository = Nothing
            _contractLiquidation = Nothing
            _agreementsRepository = Nothing
            _employeeUpdateRepository = Nothing
            _contractRepositoryCommit = Nothing
            _manualConceptRepository = Nothing
            _retroactiveCRepository = Nothing
            _incentivePaymentRepository = Nothing
            _unemployementRepository = Nothing
            _ExemptIncomeRepository = Nothing
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
