'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 05-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Transactions
Imports Application.Treasury
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.Data.PayrollRepository

Public Class PayrollLiquidationAdminService
    Implements IPayrollLiquidationAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de Grupo
    ''' </summary>
    ''' <remarks></remarks>
    Private _groupRepository As IGroupRepository

    ''' <summary>
    ''' Repositorio de Liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquitationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Repositorio de Tipos de Contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractTypeRepository As IContractTypeRepository

    ''' <summary>
    ''' Repositorio de Conceptos Autorizados
    ''' </summary>
    ''' <remarks></remarks>
    Private _autorizationConceptRepository As IAuthorizationConceptRepository

    ''' <summary>
    ''' Dominio de Liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private _LiquidationDomain As Domain.Payroll.ILiquidationDomain

    ''' <summary>
    ''' Repositorio de novedades
    ''' </summary>
    ''' <remarks></remarks>
    Private _noveltyRepository As INoveltyRepository

    ''' <summary>
    ''' Repositorio de Cesantias
    ''' </summary>
    ''' <remarks></remarks>
    Private _unemployedLiquidationRepository As IUnemployedLiquidationRepository

    ''' <summary>
    ''' Repositorio de Convenios
    ''' </summary>
    ''' <remarks></remarks>
    Private _agreementsRepository As IAgreementsCRepository

    ''' <summary>
    ''' Repositorio de Dominio de Convenios
    ''' </summary>
    ''' <remarks></remarks>
    Private _agreementsDomain As IAgreementsDomain

    ''' <summary>
    ''' Repositorio de Contratos
    ''' </summary>
    ''' <remarks></remarks>
    Private _contractRepository As IContractRepository

    ''' <summary>
    ''' Repositorio de Distribución de Gastos
    ''' </summary>
    ''' <remarks></remarks>
    Private _costDistributionRepository As ICostDistributionsRepository

    ''' <summary>
    ''' Repositorio de Vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _vacationRepository As IVacationRepository

    ''' <summary>
    ''' Repositorio de Conceptos Manuales
    ''' </summary>
    ''' <remarks></remarks>
    Private _manualConceptRepository As IManualConcepts

    ''' <summary>
    ''' Dominio de Distribución de Gasto
    ''' </summary>
    ''' <remarks></remarks>
    Private _CostDistributionDomain As ICostDistributionDomain

    ''' <summary>
    ''' Repositorio de Schedule Detail
    ''' </summary>
    ''' <remarks></remarks>
    Private _scheduleDetailRepository As IScheduleDetailRepository

    ''' <summary>
    ''' 'Repositorio de Empleado
    ''' </summary>
    ''' <remarks></remarks>
    Private _employeeRepository As IEmployeeRepository

    ''' <summary>
    ''' Repositorio de Novedades de Cuadros de Turnos
    ''' </summary>
    Private _noveltySchedule As INoveltyScheduleDetailRepository

    ''' <summary>
    ''' Repositorio de Embargos
    ''' </summary>
    Private _foreclousureRepository As IForeclousureRepository

    ''' <summary>
    ''' Repositorio de Dominio de Convenios
    ''' </summary>
    ''' <remarks></remarks>   
    Private _foreclousureDomain As IForeclosureDomain

    ''' <summary>
    ''' Repositorio de Parámetros de Nómina
    ''' </summary>
    Private _payrollSettingsRepository As IPayrollSettingsRepository

    ''' <summary>
    ''' Aplicacion de Cruce de cuentas
    ''' </summary>
    ''' <remarks></remarks>
    Private _crossingAccountAdminService As ICrossingAccountAdminService

    ''' <summary>
    ''' repositorio de la cabecera de secuencias numericas de tesoreria
    ''' </summary>
    Private _secuenseTreasuryCRepository As Domain.Entities.ISequenseTreasuryCRepository

    ''' <summary>
    ''' repositorio del detalle de secuencias numericas de tesoreria
    ''' </summary>
    Private _secuenseTreasuryDRepository As Domain.Entities.ISequenseTreasuryDRepository

    ''' <summary>
    ''' repositorio de los movimientos contables de las facturas
    ''' </summary>
    Private _accountReceivableAccountingRepository As Domain.Entities.IAccountReceivableAccountingRepository

    ''' <summary>
    ''' Repositorio de cabecera secuencias numericas
    ''' </summary>
    Private _secuenseCRepository As Domain.Entities.IPayrollSequenceRepository

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository

    ''' <summary>
    ''' repositorio de los parametros de contabilidad
    ''' </summary>
    Private _settingsAccountRepository As Domain.Entities.ISettingsAccountRepository

    ''' <summary>
    ''' repositorio de los soportes de pago de nomina electronica
    ''' </summary>
    Private _electronicPayrollPaymentSupportRepository As IElectronicPayrollPaymentSupportRepository

    ''' <summary>
    ''' repositorio de nomina electronica
    ''' </summary>
    Private _electronicPayrollRepository As IElectronicPayrollRepository

    ''' <summary>
    ''' repositorio de los Ingresos exentos acumulados por tercero y por año
    ''' </summary>
    Private _thirdPartyAccumulatedExemptIncomeRepository As Domain.Entities.IThirdPartyAccumulatedExemptIncomeRepository

#End Region

#Region "Builder"

    Public Sub New(groupRepository As IGroupRepository, liquitationRepository As IPayrollLiquidationRepository, contractTypeRepository As IContractTypeRepository, autorizationConceptRepository As IAuthorizationConceptRepository, payrollLiquidationFunctions As Domain.Payroll.ILiquidationDomain,
                   scheduleDetailRepository As IScheduleDetailRepository, noveltyRepository As INoveltyRepository, retentionRepository As IRetentionRepository, employeeRepository As IEmployeeRepository, liquitationRepositoryCommit As IPayrollLiquidationRepository,
                   messageRepositoryCommit As IMessageLiquitadionRepository, liquitationDetailRepositoryCommit As ILiquidationDetailRepository, incentivePaymentRepository As IIncentivePaymentRepository, unemployedLiquidationRepository As IUnemployedLiquidationRepository,
                   LiquidationDomain As Domain.Payroll.ILiquidationDomain, agreementsRepository As IAgreementsCRepository, agreementsDomain As IAgreementsDomain, contractRepository As IContractRepository, costDistributionRepository As ICostDistributionsRepository,
                   vacationRepository As IVacationRepository, manualConceptsRepository As IManualConcepts, costDistributionDomain As ICostDistributionDomain, noveltySchedule As INoveltyScheduleDetailRepository, foreclousureRepository As IForeclousureRepository,
                   foreclousureDomain As IForeclosureDomain, payrollSettingsRepository As IPayrollSettingsRepository, secuenseTreasuryCRepository As Domain.Entities.ISequenseTreasuryCRepository, secuenseTreasuryDRepository As Domain.Entities.ISequenseTreasuryDRepository,
                   crossingAccountAdminService As ICrossingAccountAdminService, accountReceivableAccountingRepository As Domain.Entities.IAccountReceivableAccountingRepository, secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository, secuenseCRepository As Domain.Entities.IPayrollSequenceRepository,
                   settingsAccountRepository As Domain.Entities.ISettingsAccountRepository, electronicPayrollPaymentSupportRepository As IElectronicPayrollPaymentSupportRepository, electronicPayrollRepository As IElectronicPayrollRepository, thirdPartyAccumulatedExemptIncomeRepository As Domain.Entities.IThirdPartyAccumulatedExemptIncomeRepository)
        If groupRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Grupos Vacío")
        End If
        If liquitationRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Liquidacion Vacío")
        End If
        If contractTypeRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Tipo ContratoVacío")
        End If
        If autorizationConceptRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Conceptos Vacío")
        End If
        If payrollLiquidationFunctions Is Nothing Then
            Throw New ArgumentNullException("Repositorio Funciones Vacío")
        End If
        If scheduleDetailRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Schedule Detail Vacío")
        End If
        If noveltyRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Novedades Vacío")
        End If
        If retentionRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Retenciones Vacío")
        End If
        If employeeRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Empleados Vacío")
        End If
        If incentivePaymentRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Primas Vacío")
        End If
        If unemployedLiquidationRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Cesantias Vacío")
        End If
        If noveltySchedule Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Novedades de Cuadro de Turnos Vacío")
        End If
        If foreclousureRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Embargos Vacío")
        End If

        _groupRepository = groupRepository
        _liquitationRepository = liquitationRepository
        _contractTypeRepository = contractTypeRepository
        _autorizationConceptRepository = autorizationConceptRepository
        _LiquidationDomain = LiquidationDomain
        _unemployedLiquidationRepository = unemployedLiquidationRepository
        _noveltyRepository = noveltyRepository
        _agreementsRepository = agreementsRepository
        _agreementsDomain = agreementsDomain
        _contractRepository = contractRepository
        _costDistributionRepository = costDistributionRepository
        _vacationRepository = vacationRepository
        _manualConceptRepository = manualConceptsRepository
        _CostDistributionDomain = costDistributionDomain
        _scheduleDetailRepository = scheduleDetailRepository
        _employeeRepository = employeeRepository
        _noveltySchedule = noveltySchedule
        _foreclousureRepository = foreclousureRepository
        _foreclousureDomain = foreclousureDomain
        _payrollSettingsRepository = payrollSettingsRepository
        _secuenseTreasuryCRepository = secuenseTreasuryCRepository
        _secuenseTreasuryDRepository = secuenseTreasuryDRepository
        _crossingAccountAdminService = crossingAccountAdminService
        _accountReceivableAccountingRepository = accountReceivableAccountingRepository
        _secuenseCRepository = secuenseCRepository
        _secuenseDRepository = secuenseDRepository
        _settingsAccountRepository = settingsAccountRepository
        _electronicPayrollPaymentSupportRepository = electronicPayrollPaymentSupportRepository
        _electronicPayrollRepository = electronicPayrollRepository
        _thirdPartyAccumulatedExemptIncomeRepository = thirdPartyAccumulatedExemptIncomeRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Almacena la Liquidación de Nómina
    ''' </summary>
    ''' <param name="PayrollLiquidation">Lista de Liquidaciones</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveLiquidation(PayrollLiquidation As List(Of Liquidation), session As SessionValues) As ActionMessageResult(Of List(Of Liquidation)) Implements IPayrollLiquidationAdminService.SaveLiquidation
        If PayrollLiquidation Is Nothing OrElse PayrollLiquidation.Count = 0 Then
            Throw New ArgumentNullException("Lista de Liquidación de Nómina Vacia")
        End If
        Dim audit = session.AuditMessageWcf
        Dim unitWork As IUnitWork = _liquitationRepository.UnitWork
        Dim unitWorkGroup As IUnitWork = _groupRepository.UnitWork
        Dim unitWorkNovelty As IUnitWork = _noveltyRepository.UnitWork
        Dim unitWorkAgreements As IUnitWork = _agreementsRepository.UnitWork
        Dim unitWorkContract As IUnitWork = _contractRepository.UnitWork
        Dim unitWorkVacation As IUnitWork = _vacationRepository.UnitWork
        Dim unitWorkManualConcepts As IUnitWork = _manualConceptRepository.UnitWork
        Dim unitWorkDelete As IUnitWork = _liquitationRepository.UnitWork
        Dim unitWorkNoveltySchedule As IUnitWork = _noveltySchedule.UnitWork
        Dim unitWorkForeclousure As IUnitWork = _foreclousureRepository.UnitWork
        Dim unitWorkThirdPartyAccumulatedExemptIncome As IUnitWork = _thirdPartyAccumulatedExemptIncomeRepository.UnitWork

        Dim completePayroll As Boolean
        Dim noveltyEmployee As New List(Of Novelty)
        Dim vacationEmployee As New List(Of Vacation)
        Dim vacationSingleEmployee As New List(Of Vacation)
        Dim AgreementsEmployee As New List(Of AgreementsC)
        Dim AgreementsCalc As New List(Of AgreementsC)
        Dim ForeclousureCalc As New List(Of Foreclousure)
        Dim Group As New Group
        Dim ContractList As New List(Of Contract)
        Dim EmployeeAccumulatedExemptIncome As New Domain.Entities.ThirdpartyAccumulatedExemptIncome

        Dim AgreementsList As New List(Of AgreementsC)
        Dim AgreementsDetailList As New List(Of AgreementsD)
        Dim ForeclousureList As New List(Of Foreclousure)
        Dim ForeclousureDetailList As New List(Of ForeclousureDetail)
        Dim AgreementsDetail As New AgreementsD

        Dim ListContractLiquidation As New List(Of Contract)

        Dim manualConceptContract As New List(Of ManualConcepts)
        Dim DetailManualConcept As New ManualConceptsDetail

        Dim ContractEmployee As Contract
        Dim ListLiquidationEquals As New List(Of Liquidation)

        Dim actionResult As New ActionMessageResult(Of List(Of Liquidation))
        Dim VarListMessageResult As New List(Of MessageResult)

        Dim operatingUnitId As Integer = PayrollLiquidation.FirstOrDefault.OperatingUnitId
        Dim TreasurySequenseDetailId As Integer = 0
        Dim sequenseTreasury As Domain.Entities.TreasurySequence = _secuenseTreasuryCRepository.GetSequenseByIdForm("640")
        If sequenseTreasury.Id = 0 Then
            Return New ActionMessageResult(Of List(Of Liquidation)) With {.StateResult = False, .Message = "No existe secuencia numérica para el formulario de cruce de cuentas."}
        ElseIf sequenseTreasury.IsManual Then
            Return New ActionMessageResult(Of List(Of Liquidation)) With {.StateResult = False, .Message = "La secuencia numerica de CxP es manual."}
        End If

        If sequenseTreasury.Scope = "O" Then 'Si la secuencia es por organización
            TreasurySequenseDetailId = (From x In sequenseTreasury.TreasurySequenceDetail Select x.Id).FirstOrDefault()
        Else 'Si la secuencia es por unidad operativa
            If (From x In sequenseTreasury.TreasurySequenceDetail Where x.IdOperatingUnit = operatingUnitId Select x).Count = 0 Then
                Return New ActionMessageResult(Of List(Of Liquidation)) With {.StateResult = False, .Message = "No existe la unidad operativa seleccionada en la secuencia de cruce de cuentas."}
            End If
            TreasurySequenseDetailId = (From x In sequenseTreasury.TreasurySequenceDetail Where x.IdOperatingUnit = operatingUnitId Select x.Id).FirstOrDefault()
        End If

        Dim PayrollSequenseDetailId As Integer = 0

        'La nómina electrónica se parametriza por empleador, no por la unidad operativa que el
        'usuario tenga seleccionada en la barra del formulario. Se resuelve con la unidad
        'operativa de la sesión, igual que ContractLiquidationAdminService, para que ambos
        'procesos decidan lo mismo. La unidad operativa del formulario (operatingUnitId) se
        'sigue usando para contabilidad y tesorería, que sí son dimensiones por unidad.
        'Se usa GetSettingAccountSimple y no GetSettingAccount: aquí solo se necesitan Id, IdDian y
        'HandlesElectronicPayroll (escalares). GetSettingAccount agrega includes y ~11 consultas para
        'armar descripciones, y desreferencia esos resultados sin validar nulos, por lo que lanza
        'NullReferenceException si la unidad operativa tiene una cuenta contable sin parametrizar.
        Dim electronicPayrollUnitId As Integer = session.IndigoOperatingUnitId
        Dim accountingSettings = _settingsAccountRepository.GetSettingAccountSimple(electronicPayrollUnitId)
        If accountingSettings Is Nothing OrElse accountingSettings.Id = 0 Then
            Return New ActionMessageResult(Of List(Of Liquidation)) With {.StateResult = False, .Message = "No existen parámetros de contabilidad para la unidad operativa de la sesión."}
        End If

        'Si el empleador maneja nómina electrónica pero la unidad operativa de la sesión no la
        'tiene habilitada, la parametrización es inconsistente. Se aborta en lugar de omitir los
        'soportes en silencio: ese fallo silencioso dejó 248 empleados sin documento en julio 2026.
        If Not accountingSettings.HandlesElectronicPayroll _
           AndAlso _settingsAccountRepository.EmployerHandlesElectronicPayroll(accountingSettings.IdDian) Then
            Return New ActionMessageResult(Of List(Of Liquidation)) With {.StateResult = False,
                .Message = "La unidad operativa de la sesión no tiene habilitada la nómina electrónica, " &
                           "pero el empleador sí la maneja. Inicie sesión con una unidad operativa " &
                           "habilitada, o solicite la parametrización, antes de confirmar la nómina."}
        End If

        If accountingSettings.HandlesElectronicPayroll Then
            Dim payrollSequense = _secuenseCRepository.GetSequenseByIdForm("2635")
            If payrollSequense.Id = 0 Then
                Return New ActionMessageResult(Of List(Of Liquidation)) With {.StateResult = False, .Message = "No existe secuencia numérica para los soportes de pago de nómina electrónica."}
            ElseIf payrollSequense.IsManual Then
                Return New ActionMessageResult(Of List(Of Liquidation)) With {.StateResult = False, .Message = "La secuencia numerica de los soportes de pago de nómina electrónica no puede ser manual."}
            End If

            If payrollSequense.Scope = "O" Then 'Si la secuencia es por organización
                PayrollSequenseDetailId = (From x In payrollSequense.PayrollSequenceDetail Select x.Id).FirstOrDefault()
            Else 'Si la secuencia es por unidad operativa
                If (From x In payrollSequense.PayrollSequenceDetail Where x.IdOperatingUnit = electronicPayrollUnitId Select x).Count = 0 Then
                    Return New ActionMessageResult(Of List(Of Liquidation)) With {.StateResult = False, .Message = "No existe la unidad operativa de la sesión en la secuencia de soportes de pago de nómina electrónica."}
                End If
                PayrollSequenseDetailId = (From x In payrollSequense.PayrollSequenceDetail Where x.IdOperatingUnit = electronicPayrollUnitId Select x.Id).FirstOrDefault()
            End If
        End If

        Dim payrollSettings = _payrollSettingsRepository.GetSettingPayroll()

        'configuro la transaccion
        Dim ObjTimeout = New TimeSpan(1, 30, 0)
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = ObjTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                For Each liquidationPayroll As Liquidation In PayrollLiquidation
                    liquidationPayroll.MarkAsModified()

                    Dim thirdPartyId As Integer = _employeeRepository.GetThirdPartyIdByEmployeeId(liquidationPayroll.EmployeeId)

                    Group = _groupRepository.GetGroupById(liquidationPayroll.GroupId)
                    Dim PayrollEndDate = liquidationPayroll.PayrollDateLiquidated

                    ContractEmployee = _contractRepository.GetContractById(liquidationPayroll.ContractId, True)
                    ContractEmployee.MarkAsModified()

                    'Modifico la Fecha de la última Liquidación de Contrato
                    ContractEmployee.LastLiquidationDate = liquidationPayroll.PayrollDateLiquidated

                    If ContractEmployee.InitialContractNumber = 0 Then
                        ContractEmployee.InitialContractNumber = ContractEmployee.Id
                    End If

                    If ContractEmployee.ContractType.ContractClass = 2 Or ContractEmployee.ContractType.ContractClass = 5 Then
                        If ContractEmployee.RetirementDate IsNot Nothing AndAlso ContractEmployee.RetirementDate <= liquidationPayroll.PayrollDateLiquidated AndAlso ContractEmployee.RetirementReasonId IsNot Nothing Then
                            ContractEmployee.Status = 2
                            ContractEmployee.Valid = 0
                        End If
                    End If

                    _contractRepository.SaveEntity(ContractEmployee)

                    'Ingresos exentos acumulados por tercero y año 
                    EmployeeAccumulatedExemptIncome = _thirdPartyAccumulatedExemptIncomeRepository.GetThirdpartyYear(thirdPartyId, liquidationPayroll.PayrollDateLiquidated.Year)
                    If EmployeeAccumulatedExemptIncome IsNot Nothing AndAlso EmployeeAccumulatedExemptIncome.Id > 0 Then
                        EmployeeAccumulatedExemptIncome.AccumulatedValue = EmployeeAccumulatedExemptIncome.AccumulatedValue + liquidationPayroll.ExemptValueRetention
                        EmployeeAccumulatedExemptIncome.AccumulatedMaxDeductionsAndRentExents = EmployeeAccumulatedExemptIncome.AccumulatedMaxDeductionsAndRentExents + liquidationPayroll.DeductionsAndRentExents
                        EmployeeAccumulatedExemptIncome.MarkAsModified()
                    Else
                        With EmployeeAccumulatedExemptIncome
                            .ThirdPartyId = thirdPartyId
                            .Year = liquidationPayroll.PayrollDateLiquidated.Year
                            .AccumulatedValue = liquidationPayroll.ExemptValueRetention
                            .AccumulatedMaxDeductionsAndRentExents = If(liquidationPayroll?.DeductionsAndRentExents Is Nothing, 0, liquidationPayroll?.DeductionsAndRentExents)
                        End With
                        EmployeeAccumulatedExemptIncome.MarkAsAdded()
                    End If

                    _thirdPartyAccumulatedExemptIncomeRepository.SaveEntity(EmployeeAccumulatedExemptIncome)
                    unitWorkThirdPartyAccumulatedExemptIncome.Commit()

                    'Cargo las Novedades del Empleado
                    noveltyEmployee = _noveltyRepository.GetNoveltyByEmployeeIdPayrollLiquidation(liquidationPayroll.EmployeeId)
                    If noveltyEmployee.Count() > 0 Then
                        For Each novelty As Novelty In noveltyEmployee
                            ' Si la Fecha Fin de la novedad es Mayor que la Fecha Fin de la Nómina, entonces el estado de la Novedad será PARCIALMENTE LIQUIDADO
                            If novelty.EndDate > liquidationPayroll.PayrollDateLiquidated Then
                                novelty.Status = 2
                            Else ' LIQUIDADO Y PAGADO
                                novelty.Status = 1
                            End If
                            novelty.MarkAsModified()
                            _noveltyRepository.SaveEntity(novelty)
                        Next
                    End If

                    'VACACIONES DEL EMPLEADO
                    vacationEmployee = _vacationRepository.GetVacationLiquidationDate(Group.NextDateLiquidation, 1)
                    vacationSingleEmployee = vacationEmployee.Where(Function(x) x.VacationPeriod.EmployeeId = liquidationPayroll.EmployeeId).ToList()
                    If vacationSingleEmployee.Count() > 0 Then
                        For vcs As Integer = 0 To vacationSingleEmployee.Count() - 1
                            vacationSingleEmployee.Item(vcs).State = 2
                            If vacationSingleEmployee.Item(vcs).StateIncorporation = 2 Then
                                vacationSingleEmployee.Item(vcs).StateIncorporation = 3
                            End If
                            vacationSingleEmployee.Item(vcs).MarkAsModified()
                            _vacationRepository.SaveEntity(vacationSingleEmployee.Item(vcs))
                        Next
                    End If

                    'Reviso los Conceptos Manuales
                    manualConceptContract = _manualConceptRepository.GetManualConceptsByContractNumberPayrollDate(IIf(ContractEmployee.InitialContractNumber = 0, ContractEmployee.Id, ContractEmployee.InitialContractNumber), Group.NextDateLiquidation, liquidationPayroll.PayrollDateLiquidated, 1, 1, True)
                    If manualConceptContract IsNot Nothing Then
                        For mcc As Integer = 0 To manualConceptContract.Count() - 1
                            If manualConceptContract.Item(mcc).PaidEndContract = True Then 'Si se paga hasta el Fin del Contrato, inserto en el Detalle de Conceptos Manuales
                                If manualConceptContract.Item(mcc).ManualConceptsDetail.Count > 0 Then
                                    DetailManualConcept = manualConceptContract.Item(mcc).ManualConceptsDetail.Where(Function(x) x.PayrollDateLiquidated = Group.NextDateLiquidation And x.State = 1).FirstOrDefault()
                                    If DetailManualConcept IsNot Nothing Then
                                        DetailManualConcept.State = 2
                                        DetailManualConcept.MarkAsModified()
                                    End If
                                Else
                                    DetailManualConcept = New ManualConceptsDetail()
                                    DetailManualConcept.PayrollDateLiquidated = Group.NextDateLiquidation
                                    DetailManualConcept.Value = manualConceptContract.Item(mcc).QuoteValue
                                    DetailManualConcept.State = 2 ' Estado Pagado
                                    manualConceptContract.Item(mcc).ManualConceptsDetail.Add(DetailManualConcept)
                                End If

                            Else ' Significa que tiene una Fecha Fin Determinada, entonces debo cambiar el estado en el Detalle
                                Dim ListDetailManualConcept = manualConceptContract.Item(mcc).ManualConceptsDetail

                                DetailManualConcept = ListDetailManualConcept.Where(Function(x) x.PayrollDateLiquidated = Group.NextDateLiquidation).FirstOrDefault()
                                If DetailManualConcept IsNot Nothing Then
                                    DetailManualConcept.State = 2
                                    DetailManualConcept.MarkAsModified()

                                    Dim DetailManualStatePaid = ListDetailManualConcept.Where(Function(x) x.State = 1).Count()
                                    If DetailManualStatePaid <= 0 Then
                                        manualConceptContract.Item(mcc).State = 2

                                    End If
                                End If

                            End If
                            manualConceptContract.Item(mcc).MarkAsModified()
                            _manualConceptRepository.SaveEntity(manualConceptContract.Item(mcc))
                        Next
                    End If

                    For Each LiquidationDetail As LiquidationDetail In liquidationPayroll.LiquidationDetail
                        ' Para la Clase de Concepto de Convenios
                        If LiquidationDetail.ConceptClass = "041" Then
                            AgreementsEmployee = _agreementsRepository.ListAgreementsCByEmployeeIdStarDate(LiquidationDetail.Liquidation.EmployeeId, liquidationPayroll.PayrollDateLiquidated, LiquidationDetail.ConceptId)

                            If AgreementsEmployee IsNot Nothing AndAlso AgreementsEmployee.Count > 0 Then
                                AgreementsCalc = _agreementsDomain.AgreementsCalculate(AgreementsEmployee, liquidationPayroll.PayrollDateLiquidated, LiquidationDetail.ConceptTotalValue)

                                For Each AgreementsC As AgreementsC In AgreementsEmployee
                                    If AgreementsC.CurrentBalance <= 0 Then
                                        AgreementsC.State = 4 ' Se termina el convenio
                                    End If
                                    _agreementsRepository.SaveEntity(AgreementsC)
                                Next
                                unitWorkAgreements.Commit()

                                Dim resultCrossingAccount = Me.InterfaceCrossingAccount(audit, payrollSettings, TreasurySequenseDetailId, LiquidationDetail)
                                If Not resultCrossingAccount.StateResult Then
                                    scope.Dispose()
                                    Return New ActionMessageResult(Of List(Of Liquidation)) With {.StateResult = False, .Message = resultCrossingAccount.Message}
                                End If
                            End If
                        End If

                        'Embargos
                        If LiquidationDetail.ConceptClass = "053" Then
                            ForeclousureList = _foreclousureRepository.LisForeclousureByEmployeeIdStarDate(LiquidationDetail.Liquidation.EmployeeId, Group.NextDateLiquidation, LiquidationDetail.ConceptId)
                            If ForeclousureList IsNot Nothing AndAlso ForeclousureList.Count > 0 Then
                                ForeclousureCalc = _foreclousureDomain.ForeclousureCalculate(ForeclousureList, liquidationPayroll.PayrollDateLiquidated, LiquidationDetail.ConceptTotalValue)

                                For Each ObjForeclousure As Foreclousure In ForeclousureList
                                    If ObjForeclousure.CurrentBalance IsNot Nothing AndAlso ObjForeclousure.CurrentBalance <= 0 Then
                                        ObjForeclousure.State = 5 'Termino el embargo
                                    End If

                                    If ObjForeclousure.QuoteNumber > 0 And (ObjForeclousure.QuoteNumber <= ObjForeclousure.ForeclousureDetail.Count()) Then
                                        ObjForeclousure.State = 5 'Termino el embargo
                                    End If

                                    _foreclousureRepository.SaveEntity(ObjForeclousure)
                                Next

                                unitWorkForeclousure.Commit()
                            End If
                        End If
                    Next

                    'Cambiamos el estado de las Novedades de Cuadro de Turnos
                    Dim listNoveltySchedule = _noveltySchedule.GetNoveltyScheduleDetailByEmployeeUnitBetweenDateWithoutNovelties(liquidationPayroll.EmployeeId, Group.NextDateLiquidation, 0, liquidationPayroll.GroupId)
                    If listNoveltySchedule IsNot Nothing AndAlso listNoveltySchedule.Count > 0 Then
                        For Each ObjNoveltyScheduleDetail As NoveltyScheduleDetail In listNoveltySchedule
                            ObjNoveltyScheduleDetail.Status = 1
                            ObjNoveltyScheduleDetail.MarkAsModified()
                            _noveltySchedule.SaveEntity(ObjNoveltyScheduleDetail)
                        Next
                    End If

                    liquidationPayroll.RegisterStatus = "C"
                    liquidationPayroll.PayrollConfirmationDate = Date.Now()
                    liquidationPayroll.PayrollProcessUser = audit.IdUser
                    liquidationPayroll.PayrollConfirmationUser = audit.IdUser
                    _liquitationRepository.SaveEntity(liquidationPayroll)

                    If accountingSettings.HandlesElectronicPayroll Then ''Maneja nomina electronica
                        'Busco que haya desprendibles para generar el documento de pago
                        Dim RemovablePayment = liquidationPayroll.LiquidationDetail.Where(Function(x) {1, 2}.Contains(x.ConceptType))
                        If RemovablePayment.Any Then
                            Dim support = _electronicPayrollPaymentSupportRepository.GetElectronicPayrollPaymentSupportByThirdPartyAndPeriod(thirdPartyId, liquidationPayroll.PayrollDateLiquidated.Year, liquidationPayroll.PayrollDateLiquidated.Month)
                            If support Is Nothing Then
                                support = New ElectronicPayrollPaymentSupport With {
                                    .EmployeePartyId = thirdPartyId,
                                    .Year = liquidationPayroll.PayrollDateLiquidated.Year,
                                    .Month = liquidationPayroll.PayrollDateLiquidated.Month
                                }

                                Dim seq = Me._secuenseDRepository.GetSequenseDById(PayrollSequenseDetailId)

                                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                                    support.Prefix = seq.Sequense.Pattern.Replace("#", "")
                                    support.Consecutive = Me._secuenseDRepository.IncrementSequenceAndGetNext(PayrollSequenseDetailId).ToString()
                                Else
                                    Return New ActionMessageResult(Of List(Of Liquidation)) With {.StateResult = False, .Message = "_Seq01_"}
                                End If

                                _electronicPayrollPaymentSupportRepository.SaveEntity(support)
                                _electronicPayrollPaymentSupportRepository.UnitWork.Commit()

                                Dim electronicPayroll = New ElectronicPayroll With {
                                    .DocumentType = 1,
                                    .Year = support.Year,
                                    .Month = support.Month,
                                    .EmployeePartyId = support.EmployeePartyId,
                                    .EntityName = support.GetType().Name,
                                    .EntityId = support.Id,
                                    .Prefix = support.Prefix,
                                    .DocumentNumber = support.Consecutive,
                                    .CUNE = support.CUNE,
                                    .Status = 1,
                                    .CreationDate = DateTime.Now
                                }

                                electronicPayroll.FilePath = System.IO.Path.Combine(
                                    Utils.GetPathElectronicDocuments(),
                                    session.TransactionalContainer,
                                    electronicPayroll.Year,
                                    electronicPayroll.Month,
                                    electronicPayroll.getDocumentTypeName(),
                                    electronicPayroll.Prefix,
                                    electronicPayroll.DocumentNumber
                                )

                                _electronicPayrollRepository.SaveEntity(electronicPayroll)
                                _electronicPayrollRepository.UnitWork.Commit()
                            End If

                            support.ElectronicPayrollPaymentSupportDetail.Add(New ElectronicPayrollPaymentSupportDetail With {
                                .EntityId = liquidationPayroll.Id,
                                .EntityName = liquidationPayroll.GetType().Name
                            })

                            _electronicPayrollPaymentSupportRepository.SaveEntity(support)
                            _electronicPayrollPaymentSupportRepository.UnitWork.Commit()
                        End If
                    End If
                Next

                completePayroll = PayrollLiquidation.Any(Function(x) x.CompletePayroll = True)

                ContractList = _contractRepository.GetContracEmployeeByStatus(ContractEmployee.GroupId)

                If completePayroll = True Or ContractList.Count = 1 Then
                    Group = _groupRepository.GetGroupById(PayrollLiquidation.Item(0).GroupId)
                    'Cambio las Fechas del Grupo
                    Dim PayrollDateLiquidated = PayrollLiquidation.Item(0).PayrollDateLiquidated
                    Dim LastPayrollDate = Group.NextDateLiquidation
                    Dim NextPayrollDate = DateAdd(DateInterval.Day, 1, _LiquidationDomain.GetEndPayrollDate(Group.Liquidation, LastPayrollDate))

                    Group.LastDateLiquidation = LastPayrollDate
                    Group.NextDateLiquidation = NextPayrollDate
                    Group.MarkAsModified()
                    _groupRepository.SaveEntity(Group)
                End If

                unitWorkNovelty.Commit()
                unitWorkGroup.Commit()
                unitWorkNoveltySchedule.Commit()
                unitWorkVacation.Commit()
                unitWorkManualConcepts.Commit()
                unitWorkContract.Commit()
                unitWork.Commit()
                scope.Complete()

                VarListMessageResult.Add(New MessageResult("001"))
                actionResult.StateResult = True
                actionResult.MessageResult = VarListMessageResult
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                unitWork.RollbackChangesUnitOfWork()
                unitWorkNovelty.RollbackChangesUnitOfWork()
                unitWorkGroup.RollbackChangesUnitOfWork()
                unitWorkAgreements.RollbackChangesUnitOfWork()
                unitWorkManualConcepts.RollbackChangesUnitOfWork()
                unitWorkContract.RollbackChangesUnitOfWork()
                unitWorkNoveltySchedule.RollbackChangesUnitOfWork()
                unitWorkForeclousure.RollbackChangesUnitOfWork()
                actionResult.Message = ex.InnerException.InnerException.Message
                actionResult.StateResult = False
            End Try
        End Using

        Return actionResult
    End Function

    Private Function InterfaceCrossingAccount(audit As AuditMessage, payrollSettings As PayrollSettings, treasurySequenseDetailId As Integer, liquidationDetail As LiquidationDetail) As ActionResult
        Dim AgreementsD = _agreementsRepository.GetAgreementsDByAgreementsCIdPayrollDate(liquidationDetail.AgreementsId, liquidationDetail.PayrollDate)
        If AgreementsD IsNot Nothing Then
            liquidationDetail.AgreementsDId = AgreementsD.Id
            If AgreementsD.AgreementsC.KindsAgreements.AffectsAccountsReceivable = True Then
                If payrollSettings.NoteConcepts Is Nothing Then
                    Return New ActionResult With {.StateResult = False, .Message = "No se ha parametrizado un concepto de Nota y Traslado para el cruce del convenio"}
                End If
                If payrollSettings.CashFlowConceptId Is Nothing Then
                    Return New ActionResult With {.StateResult = False, .Message = "No se ha parametrizado un concepto de Flujo de Efectivo para el cruce del convenio"}
                End If

                Dim accountReceivableAccounting = _accountReceivableAccountingRepository.GetAccountReceivableAccountingById(AgreementsD.AgreementsC.AccountReceivableAccountingId, False)
                If liquidationDetail.ConceptTotalValue > accountReceivableAccounting.Balance Then
                    Return New ActionResult With {.StateResult = False, .Message = String.Format("El cruce del convenio supera el saldo de la factura {0}", accountReceivableAccounting.AccountReceivable.InvoiceNumber)}
                End If

                Dim thirdPartyId As Integer = _employeeRepository.GetThirdPartyIdByEmployeeId(AgreementsD.AgreementsC.EmployeeId)

                Dim crossingAccount As New Domain.Entities.CrossingAccount
                With crossingAccount
                    .ThirdPartyId = thirdPartyId
                    .DocumentDate = liquidationDetail.PayrollDate
                    .Description = String.Format("Aplicación de Descuento de Nómina por pago de factura {0}", accountReceivableAccounting.AccountReceivable.InvoiceNumber)
                    .OperatingUnitId = liquidationDetail.Liquidation.OperatingUnitId
                    .CrossingType = If(thirdPartyId = accountReceivableAccounting.AccountReceivable.ThirdPartyId, 1, 2)
                    .Status = 1
                    .CrossingAccountDetailCxC.Add(New Domain.Entities.CrossingAccountDetailCxC With {
                        .AccountReceivableId = accountReceivableAccounting.AccountReceivableId,
                        .AccountReceivableAccountingId = accountReceivableAccounting.Id,
                        .MainAccountId = accountReceivableAccounting.MainAccountId,
                        .CrossingValue = liquidationDetail.ConceptTotalValue,
                        .Detail = crossingAccount.Description,
                        .IdCashFlowConcept = payrollSettings.CashFlowConceptId
                    })
                    .CrossingAccountDetailOtherConcept.Add(New Domain.Entities.CrossingAccountDetailOtherConcept With {
                        .TreasuryNoteConceptId = payrollSettings.NoteConceptsId,
                        .MainAccountId = payrollSettings.NoteConcepts.IdMainAccount,
                        .ThirdPartyId = thirdPartyId,
                        .CostCenterId = liquidationDetail.Liquidation.CostCenterId,
                        .Nature = 1,
                        .Value = liquidationDetail.ConceptTotalValue,
                        .Detail = crossingAccount.Description,
                        .IdCashFlowConcept = payrollSettings.NoteConcepts.IdCashFlowConcept
                    })
                End With

                Dim resultCrossingAccount = _crossingAccountAdminService.SaveCrossingAccount(crossingAccount, audit, True, treasurySequenseDetailId)
                If Not resultCrossingAccount.StateResult OrElse Not resultCrossingAccount.StateResultAux Then
                    Return New ActionResult With {.StateResult = False, .Message = resultCrossingAccount.Message}
                End If
            End If
        End If

        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Función para Calcular la Liquidación de la Nómina
    ''' </summary>
    ''' <param name="StringGroup">Id del Grupo</param>
    ''' <param name="employeeNit">Cédula del Empleado</param>
    ''' <returns>Action Result de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function CalculatePayrollLiquidation(StringGroup As String, ByVal session As SessionValues, Optional employeeNit As String = "") As ActionMessageResult(Of List(Of Liquidation)) Implements IPayrollLiquidationAdminService.CalculatePayrollLiquidation

        Dim unitWorkDelete As IUnitWork = _liquitationRepository.UnitWork
        Dim unitWorkSave As IUnitWork = _liquitationRepository.UnitWork
        Dim actionResult As New ActionMessageResult(Of List(Of Liquidation))


        Dim txSettings As New TransactionOptions()
        Dim MaxTimeOut As New TimeSpan(1, 30, 0)
        txSettings.Timeout = MaxTimeOut
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted
        'txSettings.IsolationLevel = System.Data.IsolationLevel.ReadCommitted ' Permite leer pero no modificar
        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)

            Try

                Dim StatusLiquidation As Boolean = False

                Dim PayrollEndDate As Date ' Fecha Final Nómina
                Dim PayrollStarDate As Date ' Fecha Inicio Nómina
                Dim PayrollLiquidation As Byte
                'Dim EmployeeLiquidated As New List(Of Liquidation)
                Dim groupId As String
                Dim groupEmployee As New Group

                Dim VarListMessageResult As New List(Of MessageResult)

                Dim completePayroll As Boolean

                If employeeNit = "" Then
                    completePayroll = True
                Else
                    completePayroll = False
                End If

                If StringGroup Is Nothing Then
                    Throw New ArgumentNullException("groupCode vacio")
                End If

                Dim ArrayGroups() As String = Split(StringGroup, ",")

                Dim ArrListLiquitadion As New List(Of Liquidation)
                Dim EmployeeLiquidated As New List(Of Liquidation)

                Dim ListLiquidationResult As New List(Of Liquidation)

                For G As Integer = 0 To (ArrayGroups.Count() - 1)

                    groupId = ArrayGroups.GetValue(G)

                    'Cargo el objeto Grupo
                    groupEmployee = _groupRepository.GetGroupById(groupId)

                    ' Cargo Datos del Grupo
                    PayrollStarDate = groupEmployee.NextDateLiquidation.ToString()
                    Dim PayrollMonth = groupEmployee.Month
                    PayrollLiquidation = groupEmployee.Liquidation

                    'Averiguo la Fecha Fin de la Nómina

                    PayrollEndDate = _LiquidationDomain.GetEndPayrollDate(PayrollLiquidation, PayrollStarDate)

                    Dim PayrollLiquidationDelete = _liquitationRepository.SP_DeleteLiquidationNoConfirm(PayrollEndDate, groupId, employeeNit)
                    If PayrollLiquidationDelete <> 3 Then
                        'Cargo los empleados que voy a Liquidar
                        Dim DateRetirement = New Date(1901, 1, 1)

                        Dim totalItems As Integer

                        Dim totalProcessedItems As Integer

                        Dim itemsSend As Integer = 100



                        Dim employeeList As New List(Of Employee)
                        employeeList = _liquitationRepository.GetEmployesPayrollLiquidation(groupId, PayrollEndDate, PayrollStarDate, employeeNit)

                        If employeeList IsNot Nothing AndAlso employeeList.Count > 0 Then

                            totalProcessedItems = 0
                            totalItems = employeeList.Count
                            If (totalItems > 0) Then
                                Dim indexSend = 0

                                While employeeList.Count > 0
                                    Dim objLock As New Object()
                                    'agregamos los items a la cabecera
                                    Dim listToSend = employeeList.Take(itemsSend).ToList()

                                    Dim quantityDetailsToProcess = If(employeeList.Count < itemsSend, employeeList.Count, itemsSend)
                                    indexSend = totalProcessedItems + 1
                                    totalProcessedItems += quantityDetailsToProcess

                                    Dim Result = _LiquidationDomain.NewExecuteLiquitadion(listToSend, groupEmployee, completePayroll, session, DateRetirement)
                                    If Result.StateResult Then
                                        ListLiquidationResult.AddRange(Result.ObjectEmbbeded)
                                        'Me._journalVoucher = Result.ObjectEmbbeded
                                    End If

                                    If quantityDetailsToProcess > 0 Then
                                        employeeList.RemoveRange(0, quantityDetailsToProcess)
                                    End If


                                End While
                            End If

                        Else
                            scope.Dispose()
                            actionResult.Message = "No se encontraron empleados para liquidar"
                            actionResult.MessageResult.Add(New MessageResult("-999", "No se encontraron empleados para liquidar"))
                            actionResult.StateResult = False
                            Return actionResult
                        End If
                    End If

                Next

                If ListLiquidationResult IsNot Nothing And ListLiquidationResult.Count > 0 Then

                    For Each ObjLiquidation As Liquidation In ListLiquidationResult
                        'ObjLiquidation.ChangeTracker.State = ObjectState.Added
                        _liquitationRepository.SaveEntity(ObjLiquidation)
                    Next

                    unitWorkSave.Commit()
                    scope.Complete()

                    actionResult.StateResult = True
                    actionResult.ObjectEmbbeded = ListLiquidationResult

                    Return actionResult
                End If



            Catch ex As Exception
                'tx.Rollback()
                scope.Dispose()
                unitWorkSave.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                actionResult.StateResult = False
                actionResult.MessageResult.Add(New MessageResult("-999", ex.Message))
                Return actionResult
            End Try
        End Using

    End Function

    Public Function CalculatePayrollLiquidationFragment(ListEmployee As List(Of Employee), IdGroup As Integer, ByVal session As SessionValues, Optional CountLiquidation As Integer = 0) As ActionMessageResult(Of List(Of Liquidation)) Implements IPayrollLiquidationAdminService.CalculatePayrollLiquidationFragment

        Dim unitWorkDelete As IUnitWork = _liquitationRepository.UnitWork
        Dim unitWorkSave As IUnitWork = _liquitationRepository.UnitWork
        Dim actionResult As New ActionMessageResult(Of List(Of Liquidation))

        Dim txSettings As New TransactionOptions()
        Dim MaxTimeOut As New TimeSpan(1, 30, 0)
        txSettings.Timeout = MaxTimeOut
        txSettings.IsolationLevel = IsolationLevel.ReadCommitted

        Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)

            Try

                Dim EmployeeNit As String = ""

                If ListEmployee.Count = 1 Then
                    'Viene solo un empleado, ese es el que envío a eliminar
                    EmployeeNit = ListEmployee.FirstOrDefault.Nit
                    If EmployeeNit Is Nothing Then
                        EmployeeNit = ""
                    End If
                End If

                Dim StatusLiquidation As Boolean = False

                Dim PayrollEndDate As Date ' Fecha Final Nómina
                Dim PayrollStarDate As Date ' Fecha Inicio Nómina
                Dim PayrollLiquidation As Byte

                Dim groupEmployee As New Group

                Dim completePayroll As Boolean

                If EmployeeNit = "" Then
                    completePayroll = True
                Else
                    completePayroll = False
                End If

                Dim ListLiquidationResult As New List(Of Liquidation)
                Dim MessageAction As New List(Of MessageResult)

                'Cargo el objeto Grupo
                groupEmployee = _groupRepository.GetGroupById(IdGroup)

                ' Cargo Datos del Grupo
                PayrollStarDate = groupEmployee.NextDateLiquidation.ToString()
                Dim PayrollMonth = groupEmployee.Month
                PayrollLiquidation = groupEmployee.Liquidation

                'Averiguo la Fecha Fin de la Nómina

                PayrollEndDate = _LiquidationDomain.GetEndPayrollDate(PayrollLiquidation, PayrollStarDate)

                Dim PayrollLiquidationDelete As Integer

                If CountLiquidation = 0 Or EmployeeNit <> "" Then
                    PayrollLiquidationDelete = _liquitationRepository.SP_DeleteLiquidationNoConfirm(PayrollEndDate, IdGroup, EmployeeNit)
                End If

                If PayrollLiquidationDelete <> 3 Then
                    'Cargo los empleados que voy a Liquidar
                    Dim DateRetirement = New Date(1901, 1, 1)

                    Dim Result = _LiquidationDomain.NewExecuteLiquitadion(ListEmployee, groupEmployee, completePayroll, session, DateRetirement)

                    If Result.StateResult Then
                        ListLiquidationResult.AddRange(Result.ObjectEmbbeded)
                        MessageAction.AddRange(Result.MessageResult)
                    Else
                        scope.Dispose()
                        actionResult.Message = "No se encontraron empleados para liquidar"
                        If Result.MessageResult.Any() Then
                            Dim message As New System.Text.StringBuilder
                            For Each messageResult In Result.MessageResult
                                If messageResult.Parameters.Any() Then
                                    For Each parameter In messageResult.Parameters
                                        message.AppendLine(parameter)
                                    Next
                                End If
                            Next
                            If message.Length > 0 Then
                                actionResult.Message = message.ToString()
                            End If
                        End If
                        actionResult.StateResult = False
                        Return actionResult
                    End If
                End If

                If ListLiquidationResult IsNot Nothing And ListLiquidationResult.Count > 0 Then

                    For Each ObjLiquidation As Liquidation In ListLiquidationResult
                        'ObjLiquidation.ChangeTracker.State = ObjectState.Added
                        _liquitationRepository.SaveEntity(ObjLiquidation)
                    Next

                    unitWorkSave.Commit()
                    scope.Complete()

                    actionResult.StateResult = True
                    actionResult.ObjectEmbbeded = ListLiquidationResult
                    actionResult.MessageResult = MessageAction

                    Return actionResult
                End If

            Catch ex As Exception
                'tx.Rollback()
                scope.Dispose()
                unitWorkSave.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                actionResult.StateResult = False
                actionResult.MessageResult.Add(New MessageResult("-999", ex.Message))
                Return actionResult
            End Try
        End Using

    End Function

    ''' <summary>
    ''' Lista los Empleados Liquidados
    ''' </summary>
    ''' <param name="DatePayrollLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function ListEmployeeLiquitaded(ByVal DatePayrollLiquidated As Date, groupId As String) As List(Of Liquidation) Implements IPayrollLiquidationAdminService.GetEmployeeLiquidated
        Try
            Return _liquitationRepository.GetEmployeeLiquidated(DatePayrollLiquidated, groupId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los Empleados Liquidados
    ''' </summary>
    ''' <param name="DatePayrollLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function CountEmployeePayroll(groupId As String) As List(Of Employee) Implements IPayrollLiquidationAdminService.CountEmployeePayroll
        Try

            Dim groupEmployee = _groupRepository.GetGroupById(groupId)

            ' Cargo Datos del Grupo
            Dim PayrollStarDate = groupEmployee.NextDateLiquidation.ToString()
            Dim PayrollMonth = groupEmployee.Month
            Dim PayrollLiquidation = groupEmployee.Liquidation

            'Averiguo la Fecha Fin de la Nómina

            Dim PayrollEndDate = _LiquidationDomain.GetEndPayrollDate(PayrollLiquidation, PayrollStarDate)

            Return _liquitationRepository.CountEmployeePayroll(groupId, PayrollEndDate, PayrollStarDate)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Elimina una Liquidación
    ''' </summary>
    ''' <param name="Liquidated">Objeto Liquidación</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeletePayrollLiquidated(ByVal Liquidated As Liquidation) As Boolean Implements IPayrollLiquidationAdminService.DeletePayrollLiquidated
        Dim unitWork As IUnitWork = _liquitationRepository.UnitWork
        Try
            Liquidated.MarkAsDeleted()
            _liquitationRepository.DeleteEntity(Liquidated)
            unitWork.Commit()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función que devuelve si un empleado tiene alguna nómina confirmada
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function LiquidationConfirmatedByContract(ByVal contractId As String) As Boolean Implements IPayrollLiquidationAdminService.LiquidationConfirmatedByContract
        Try
            Return _liquitationRepository.LiquidationConfirmatedByContract(contractId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función Para Eliminar Liquidaciones por Id del Contrato
    ''' </summary>
    ''' <param name="contractId">Id del Contrato</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteLiquidationByContractId(ByVal contractId As String) As Boolean Implements IPayrollLiquidationAdminService.DeleteLiquidationByContractId
        Dim unitWork As IUnitWork = _liquitationRepository.UnitWork
        Try
            Dim Liquidated = _liquitationRepository.ContractNotCheckLiquidation(contractId)

            If Liquidated Is Nothing Then
                Return False
            End If

            For Each l As Liquidation In Liquidated
                l.MarkAsDeleted()
                _liquitationRepository.DeleteEntity(l)
            Next

            unitWork.Commit()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Devuelve la fecha menor de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationMinDate() As Date Implements IPayrollLiquidationAdminService.GetLiquidationMinDate
        Try
            Return _liquitationRepository.GetLiquidationMinDate()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Devuelve la fecha maxima de liquidaciones de nomina
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationMaxDate() As Date Implements IPayrollLiquidationAdminService.GetLiquidationMaxDate
        Try
            Return _liquitationRepository.GetLiquidationMaxDate()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la Validación para saber si a un empleado se le paga nómina o no
    ''' </summary>
    ''' <param name="EmployeeId">Id del Empleado</param>
    ''' <param name="PayrollLiquidation">Liquidación de Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nómina</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function GetPayrollValidation(EmployeeId As Integer, PayrollLiquidation As Integer, PayrollEndDate As Date) As Boolean
        Try
            Dim Valida = _liquitationRepository.GetPayrollValidation(EmployeeId, PayrollLiquidation, PayrollEndDate)
            Return Valida
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Lista las Liquidaciones de un Empleado
    ''' </summary>
    ''' <param name="EmployeeId">Id del Empleado</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function EmployeePayrollCheckLiquidation(EmployeeId As Integer) As List(Of Liquidation)
        Try
            Dim EmployeeLiquidation = _liquitationRepository.EmployeePayrollCheckLiquidation(EmployeeId)
            Return EmployeeLiquidation
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Función que obtiene todas las Liquidaciones de Determinado Grupo
    ''' </summary>
    ''' <param name="StringGroup">Id del Grupo</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationByGrupoId(StringGroup As String) As List(Of Liquidation) Implements IPayrollLiquidationAdminService.GetLiquidationByGrupoId

        Dim ArrayGroups() As String = Split(StringGroup, ",")
        Dim ArrListLiquitadion As New List(Of Liquidation)
        Dim EmployeeLiquidation As New List(Of Liquidation)

        For G As Integer = 0 To (ArrayGroups.Count() - 1)
            Dim groupId = ArrayGroups.GetValue(G)
            EmployeeLiquidation = _liquitationRepository.GetLiquidationByGrupoId(groupId)

            If EmployeeLiquidation IsNot Nothing Then
                For i As Integer = 0 To EmployeeLiquidation.Count() - 1
                    ArrListLiquitadion.Add(EmployeeLiquidation.Item(i))
                Next
            End If

        Next

        Return ArrListLiquitadion
    End Function

    ''' <summary>
    ''' Obtiene las fechas de liquidacion existentes segun un grupo
    ''' </summary>
    ''' <param name="groupId">id Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationDatesByGroup(groupId As Integer) As List(Of Date) Implements IPayrollLiquidationAdminService.GetLiquidationDatesByGroup
        Try
            Return _liquitationRepository.GetLiquidationDatesByGroup(groupId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista los empleados que se ha pagado nomina segun fecha de pago y grupo
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <returns>Lista de Empleados que se les ha pagado una nómina</returns>
    ''' <remarks></remarks>
    Public Function ListLiquitadionByGroupAndDateLiquidated(PayrollDateLiquidated As Date, ByVal groupId As String) As List(Of Liquidation) Implements IPayrollLiquidationAdminService.ListLiquitadionByGroupAndDateLiquidated
        Try
            Return _liquitationRepository.ListLiquitadionByGroupAndDateLiquidated(PayrollDateLiquidated, groupId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Liquidaciones de Determinado Grupo
    ''' </summary>
    ''' <param name="StringGroup">Id del Grupo</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationByGrupoIdConsultLiquidation(StringGroup As String) As List(Of Liquidation) Implements IPayrollLiquidationAdminService.GetLiquidationByGrupoIdConsultLiquidation

        Dim ArrayGroups() As String = Split(StringGroup, ",")
        Dim ArrListLiquitadion As New List(Of Liquidation)
        Dim EmployeeLiquidation As New List(Of Liquidation)

        For G As Integer = 0 To (ArrayGroups.Count() - 1)
            Dim groupId = ArrayGroups.GetValue(G)
            EmployeeLiquidation = _liquitationRepository.GetLiquidationByGrupoIdConsultLiquidation(groupId)

            If EmployeeLiquidation IsNot Nothing Then
                For i As Integer = 0 To EmployeeLiquidation.Count() - 1
                    ArrListLiquitadion.Add(EmployeeLiquidation.Item(i))
                Next
            End If

        Next

        Return ArrListLiquitadion
    End Function

    ''' <summary>
    ''' Almacena las Liquidaciones de Saldos Iniciales
    ''' </summary>
    ''' <param name="LiquidationList">Lista de Liquidaciones</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveLiquidationOpenBalances(LiquidationList As List(Of Liquidation)) As Boolean Implements IPayrollLiquidationAdminService.SaveLiquidationOpenBalances
        If LiquidationList Is Nothing Then
            Throw New ArgumentNullException("LiquidationList Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _liquitationRepository.UnitWork
        Try

            For Each Liquidation As Liquidation In LiquidationList

                Liquidation.Contract = Nothing
                Liquidation.Group = Nothing
                Liquidation.Employee = Nothing

                Dim group = _groupRepository.GetGroupById(Liquidation.GroupId)
                Dim PayrollStarDate = Liquidation.PayrollDateLiquidated
                Liquidation.PayrollDateLiquidated = _LiquidationDomain.GetEndPayrollDate(group.Liquidation, PayrollStarDate)

                _liquitationRepository.SaveEntity(Liquidation)


            Next
            UnitOfWork.Commit()
            Return True
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Diccionario con el Id del Grupo y Verdadero en caso de que exista una liquidación EN BORRADOR para el grupo y la Fecha Seleccionada (Válido únicamente para la carga de la Liquidación de Nómina)
    ''' </summary>
    ''' <returns>Diccionario</returns>
    ''' <remarks></remarks>
    Public Function GetOnlyLiquidationByGrupoIdDateLiquidation() As Dictionary(Of String, Date) Implements IPayrollLiquidationAdminService.GetOnlyLiquidationByGrupoIdDateLiquidation

        Dim ArrListDescriptions As New Dictionary(Of String, Date)
        Dim descriptions As Dictionary(Of String, Date)


        Dim ListGroups = _groupRepository.ListAllGroups()

        For G As Integer = 0 To ListGroups.Count() - 1
            'Cargo el objeto Grupo


            ' Cargo Datos del Grupo
            Dim PayrollStarDate = ListGroups.Item(G).NextDateLiquidation.ToString()
            Dim PayrollMonth = ListGroups.Item(G).Month
            Dim PayrollLiquidation = ListGroups.Item(G).Liquidation

            'Averiguo la Fecha Fin de la Nómina
            Dim PayrollEndDate = _LiquidationDomain.GetEndPayrollDate(PayrollLiquidation, PayrollStarDate)
            descriptions = _liquitationRepository.GetOnlyLiquidationByGrupoIdDateLiquidation(ListGroups.Item(G).Id.ToString(), PayrollEndDate)

            If descriptions IsNot Nothing Then

                ArrListDescriptions.Add(descriptions.Keys(0), descriptions.Values(0))

            End If

        Next

        Return ArrListDescriptions

    End Function

    ''' <summary>
    ''' Lista los Empleados Liquidados
    ''' </summary>
    ''' <param name="DatePayrollLiquidated">Fecha de Liquidación</param>
    ''' <param name="groupId">groupId</param>
    ''' <returns>Lista de Liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function PayrollNotCheckLiquidatedToDelete(ByVal DatePayrollLiquidated As Date, groupId As String) As List(Of Liquidation) Implements IPayrollLiquidationAdminService.PayrollNotCheckLiquidatedToDelete
        Try
            Return _liquitationRepository.PayrollNotCheckLiquidatedToDelete(DatePayrollLiquidated, groupId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene las fechas de liquidacion existentes para Nóminas Confirmadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationDatesConfirmPayroll() As List(Of Date) Implements IPayrollLiquidationAdminService.GetLiquidationDatesConfirmPayroll
        Try
            Return _liquitationRepository.GetLiquidationDatesConfirmPayroll()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una liquidación por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetLiquidationById(id As Integer) As Liquidation Implements IPayrollLiquidationAdminService.GetLiquidationById
        Try
            Return _liquitationRepository.GetLiquidationById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una liquidación por id del empleado, año y mes
    ''' </summary>
    Public Function GetLiquidationByEmployeeIdYearMonth(employeeId As Integer, year As Integer, month As Integer) As Liquidation Implements IPayrollLiquidationAdminService.GetLiquidationByEmployeeIdYearMonth
        Try
            Return _liquitationRepository.GetLiquidationByEmployeeIdYearMonth(employeeId, year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el valor de las patronales de un empleado por año y mes
    ''' </summary>
    Public Function GetPatronalesValue(employeeId As Integer, year As Integer, month As Integer) As Decimal Implements IPayrollLiquidationAdminService.GetPatronalesValue
        Try
            Return _liquitationRepository.GetPatronalesValue(employeeId, year, month)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función que obtiene la Lista de Grupos con Fecha para el Frontal de Liquidación, Consulta Liquidación
    ''' </summary>
    ''' <param name="GroupId">Id Grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationByGroupIdConsultLiquidation(GroupId As Integer) As String Implements IPayrollLiquidationAdminService.GetLiquidationByGroupIdConsultLiquidation
        Try
            Return Infrastructure.CrossCutting.Base.Utils.SerializeObjectToJson(_liquitationRepository.GetLiquidationByGroupIdConsultLiquidation(GroupId))
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para Cargar la Cabecera, para el precargue de las Liquidaciones de Nómina, cuando se abre el frontal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getLiquidationHeader() As List(Of Liquidation) Implements IPayrollLiquidationAdminService.getLiquidationHeader

        Dim ListLiquidation As New List(Of Liquidation)
        Dim ListGroups = _groupRepository.ListAllGroups()

        For Each G As Group In ListGroups

            ' Cargo Datos del Grupo
            Dim PayrollStarDate = G.NextDateLiquidation
            Dim PayrollLiquidation = G.Liquidation

            'Averiguo la Fecha Fin de la Nómina
            Dim PayrollEndDate = _LiquidationDomain.GetEndPayrollDate(PayrollLiquidation, PayrollStarDate)
            Dim HeadLiquidation = _liquitationRepository.GetHeadLiquidation(G.Id, PayrollEndDate)

            If HeadLiquidation IsNot Nothing Then
                ListLiquidation.AddRange(HeadLiquidation)
            End If

        Next

        Return ListLiquidation

    End Function

    Public Function GetDetailMessageLiquidation(groupId As Integer) As List(Of Liquidation) Implements IPayrollLiquidationAdminService.GetDetailMessageLiquidation
        Dim group = _groupRepository.GetGroupById(groupId)

        ' Cargo Datos del Grupo
        Dim PayrollStarDate = group.NextDateLiquidation
        Dim PayrollLiquidation = group.Liquidation

        'Averiguo la Fecha Fin de la Nómina
        Dim PayrollEndDate = _LiquidationDomain.GetEndPayrollDate(PayrollLiquidation, PayrollStarDate)
        Dim CompleteLiquidation = _liquitationRepository.GetDetailMessageLiquidation(group.Id, PayrollEndDate)

        If CompleteLiquidation IsNot Nothing AndAlso CompleteLiquidation.Count() > 0 Then
            Return CompleteLiquidation
        Else
            Return Nothing
        End If

    End Function

    Public Function GetEmployesPayrollLiquidation(GroupId As Integer, NitEmployee As String) As List(Of Employee) Implements IPayrollLiquidationAdminService.GetEmployesPayrollLiquidation

        Dim group = _groupRepository.GetGroupById(GroupId)

        ' Cargo Datos del Grupo
        Dim PayrollStarDate = group.NextDateLiquidation
        Dim PayrollLiquidation = group.Liquidation

        'Averiguo la Fecha Fin de la Nómina
        Dim PayrollEndDate = _LiquidationDomain.GetEndPayrollDate(PayrollLiquidation, PayrollStarDate)
        Return _liquitationRepository.GetEmployesPayrollLiquidation(GroupId, PayrollEndDate, PayrollStarDate, NitEmployee)


    End Function

    '###########################################################

    ''' <summary>
    ''' Metodo que ejecuta el storeProcedure "Payrol.SP_ReportKardex"
    ''' </summary>
    ''' <param name="status">The status.</param>
    ''' <param name="employeeId">The employee identifier.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    Function GetListReportPayrollReportKardex(conceptType As Integer, status As Integer, employeeId As Integer, session As SessionValues) As DataSet Implements IPayrollLiquidationAdminService.GetListReportPayrollReportKardex


        'If ValidityId = 0 Then
        '    Throw New ArgumentNullException("ValidityId")
        'End If
        'If Month() = 0 Then
        '    Throw New ArgumentNullException("Month")
        'End If
        Try
            Dim ds As New DataSet
            Dim query1 As String

            query1 = "exec [Payroll].[SP_ReportKardex] " & employeeId & "," & status & "," & conceptType & ""


            Dim dt1 = Me.GetDatatable(query1, session, "ReportPayrollKardes")
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
        Using conexion As New SqlClient.SqlConnection(connectionString)
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
            End Try
        End Using
    End Function

    Function GetListReportPayrollReportNovelties(InitialDate As Date, EndDate As Date, InitialCodeGroup As String, EndCodeGroup As String, pBranchOfficeIdStart? As Integer, pBranchOfficeIdEnd? As Integer, session As SessionValues) As DataSet Implements IPayrollLiquidationAdminService.GetListReportPayrollReportNovelties
        'If ValidityId = 0 Then
        '    Throw New ArgumentNullException("ValidityId")
        'End If
        'If Month() = 0 Then
        '    Throw New ArgumentNullException("Month")
        'End If
        Try
            Dim ds As New DataSet
            Dim query1 As String

            If InitialCodeGroup Is Nothing AndAlso EndCodeGroup Is Nothing Then

                query1 = "exec [Payroll].[SP_ReportNovelties] '" & InitialDate & "','" & EndDate & "',NULL,NULL," & IIf(pBranchOfficeIdStart Is Nothing, "NULL", pBranchOfficeIdStart) & ", " & IIf(pBranchOfficeIdEnd Is Nothing, "NULL", pBranchOfficeIdEnd)
            Else
                query1 = "exec [Payroll].[SP_ReportNovelties] '" & InitialDate & "','" & EndDate & "','" & InitialCodeGroup & "','" & EndCodeGroup & "', " & IIf(pBranchOfficeIdStart Is Nothing, "NULL", pBranchOfficeIdStart) & ", " & IIf(pBranchOfficeIdEnd Is Nothing, "NULL", pBranchOfficeIdEnd) & ""
            End If



            Dim dt1 = Me.GetDatatable(query1, session, "ReportPayrollNovelties")
            ds.Tables.Add(dt1.Copy())
            Return ds

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función para Calcular Días 360
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns></returns>
    Public Function Days360(InitialDate As Date, EndDate As Date) As Integer Implements IPayrollLiquidationAdminService.Days360
        Return _LiquidationDomain.Days360(InitialDate, EndDate)
    End Function

    Public Function GetPayrollReport(pStartDate As Date, pEndDate As Date, pRegisterStatus As String, pCédula As String, pStartGroupCode As String, pEndGroupCode As String, pSession As SessionValues) As DataTable Implements IPayrollLiquidationAdminService.GetPayrollReport
        Dim dtPayrollReport As New DataTable("PayrollReport")
        Dim ds As New DataSet
        Using sqlCnn As New SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, pSession.TransactionalContainer, False))
            Using sqlCmd As New SqlCommand("Payroll.SP_GetPayrollReport", sqlCnn)
                sqlCmd.CommandType = CommandType.StoredProcedure
                Dim sqlPrm As SqlParameter

                sqlPrm = New SqlParameter
                sqlPrm.ParameterName = "@pStartDate"
                sqlPrm.Value = pStartDate
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New SqlParameter
                sqlPrm.ParameterName = "@pEndDate"
                sqlPrm.Value = pEndDate
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New SqlParameter
                sqlPrm.ParameterName = "@pRegisterStatus"
                sqlPrm.Value = pRegisterStatus
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New SqlParameter
                sqlPrm.ParameterName = "@pCédula"
                sqlPrm.Value = pCédula
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New SqlParameter
                sqlPrm.ParameterName = "@pStartGroupCode"
                sqlPrm.Value = pStartGroupCode
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New SqlParameter
                sqlPrm.ParameterName = "@pEndGroupCode"
                sqlPrm.Value = pEndGroupCode
                sqlCmd.Parameters.Add(sqlPrm)

                sqlCnn.Open()

                Using sqlDR As SqlDataReader = sqlCmd.ExecuteReader
                    dtPayrollReport.Load(sqlDR)
                End Using
            End Using

            ds.Tables.Add(dtPayrollReport)

            Return dtPayrollReport
        End Using
    End Function

    ''' <summary>
    ''' Funcion que obtiene el reporte de talento humano
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial</param>
    ''' <param name="finalDate">Fecha final</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <returns>Lista de información de empleados</returns>
    Public Function GetReportHumanTalent(initialDate As Date, finalDate As Date, session As SessionValues, Optional employeeId As Integer? = Nothing) As ActionResult(Of List(Of SP_ReportHumanTalent_Result)) Implements IPayrollLiquidationAdminService.GetReportHumanTalent
        Try
            Dim result = _liquitationRepository.GetReportHumanTalent(initialDate, finalDate, employeeId)

            Return New ActionResult(Of List(Of SP_ReportHumanTalent_Result)) With {
            .StateResult = True,
            .ObjectEmbbeded = result,
            .Message = "Consulta realizada exitosamente"
        }
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_ReportHumanTalent_Result)) With {
            .StateResult = False,
            .Message = Utils.GetInnerExceptionMessageToString(ex)
        }
        End Try
    End Function


    ''' <summary>
    ''' Obtiene el reporte de detalle de liquidación con conceptos dinámicos
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del período</param>
    ''' <param name="endDate">Fecha final del período</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <param name="pSession">Valores de sesión</param>
    ''' <returns>DataTable con el reporte de liquidación detallado</returns>
    Public Function GetLiquidationDetailReport(initialDate As Date, endDate As Date, Optional employeeId As Integer? = Nothing, Optional groupInitial As Integer? = Nothing, Optional groupFinal As Integer? = Nothing, Optional branchOfficeInitial As Integer? = Nothing, Optional branchOfficeFinal As Integer? = Nothing, Optional registerStatus As Char? = Nothing, Optional pSession As SessionValues = Nothing) As DataTable Implements IPayrollLiquidationAdminService.GetLiquidationDetailReport
        Return _liquitationRepository.GetLiquidationDetailReport(initialDate, endDate, employeeId, groupInitial, groupFinal, branchOfficeInitial, branchOfficeFinal, registerStatus, pSession)
    End Function

    ''' <summary>
    ''' Metodo que ejecuta el storeProcedure [Payroll].[SP_ReportPersonnelActions].
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del rango</param>
    ''' <param name="endDate">Fecha final del rango</param>
    ''' <param name="initialCodeGroup">Código del grupo de nómina inicial (opcional)</param>
    ''' <param name="endCodeGroup">Código del grupo de nómina final (opcional)</param>
    ''' <param name="branchOfficeInitial">Id de la sucursal inicial para filtrar (opcional)</param>
    ''' <param name="branchOfficeFinal">Id de la sucursal final para filtrar (opcional)</param>
    ''' <param name="personnelActionTypes">Códigos numéricos de tipo de novedad separados por coma (opcional)</param>
    ''' <param name="pSession">Valores de sesión</param>
    ''' <returns>DataTable con las acciones de personal del rango</returns>
    Public Function GetReportPersonnelActions(initialDate As Date, endDate As Date, Optional initialCodeGroup As String = Nothing, Optional endCodeGroup As String = Nothing, Optional branchOfficeInitial As Integer? = Nothing, Optional branchOfficeFinal As Integer? = Nothing, Optional personnelActionTypes As String = Nothing, Optional pSession As SessionValues = Nothing) As DataTable Implements IPayrollLiquidationAdminService.GetReportPersonnelActions
        Try
            Return _liquitationRepository.GetReportPersonnelActions(initialDate, endDate, initialCodeGroup, endCodeGroup, branchOfficeInitial, branchOfficeFinal, personnelActionTypes, pSession)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", pSession)
            Return Nothing
        End Try
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _CostDistributionDomain.Dispose()
                _agreementsDomain.Dispose()
                _LiquidationDomain.Dispose()
            End If
            _groupRepository = Nothing
            _liquitationRepository = Nothing
            _contractTypeRepository = Nothing
            _autorizationConceptRepository = Nothing
            _LiquidationDomain = Nothing
            _unemployedLiquidationRepository = Nothing
            _noveltyRepository = Nothing
            _agreementsRepository = Nothing
            _agreementsDomain = Nothing
            _contractRepository = Nothing
            _costDistributionRepository = Nothing
            _vacationRepository = Nothing
            _manualConceptRepository = Nothing
            _CostDistributionDomain = Nothing
            _scheduleDetailRepository = Nothing
            _employeeRepository = Nothing
            _secuenseCRepository = Nothing
            _secuenseDRepository = Nothing
            _settingsAccountRepository = Nothing
            _electronicPayrollPaymentSupportRepository = Nothing
            _electronicPayrollRepository = Nothing
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
