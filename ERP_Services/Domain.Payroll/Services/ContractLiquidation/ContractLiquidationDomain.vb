Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports System.Runtime.InteropServices.ComTypes
Imports System.Data.Entity

'***********************************************************************
' Assembly         : Domain.Payroll.Entities
' Author           : Jose Luis Rojas
' Created          : 28-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Class ContractLiquidationDomain
    Implements IContractLiquidationDomain

#Region "Repositories and Domain services"

    ''' <summary>
    ''' Servicio de dominio de liquidacion de cesantias
    ''' </summary>
    Private _unemployedLiquidationDomain As IUnemployedLiquidationDomain

    ''' <summary>
    ''' Servicio de dominio de liquidacion de nomina
    ''' </summary>
    Private _liquidationDomain As ILiquidationDomain

    ''' <summary>
    ''' Servicio de dominio de primas
    ''' </summary>
    Private _incentivePaymentDomain As IIncentivePaymentDomain

    ''' <summary>
    ''' Servicio de dominio de vacaciones
    ''' </summary>
    Private _vacationPeriodDomain As IVacationPeriodDomain

    ''' <summary>
    ''' Repositorio de liquidacion de cesantias
    ''' </summary>
    Private _unemployedLiquidationRepository As IUnemployedLiquidationRepository

    ''' <summary>
    ''' Contiene el repositorio de liquidacion de contrato
    ''' </summary>
    Private _contractLiquidationRepository As IContractLiquidationRepository

    ''' <summary>
    ''' Repositorio de liquidacion de nominas
    ''' </summary>
    Private _payrollLiquidationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Repositorio de vacaciones
    ''' </summary>
    Private _vacationPeriodRepository As IVacationPeriodRepository

    ''' <summary>
    ''' Repositorio de razones de retiro
    ''' </summary>
    Private _retirementReasonRepository As IRetirementReasonRepository

    ''' <summary>
    ''' Concepto de repositorio
    ''' </summary>
    Private _conceptRepository As IConceptRepository

    ''' <summary>
    ''' Repositorio de Novedades
    ''' </summary>
    ''' <remarks></remarks>
    Private _noveltyRepository As INoveltyRepository

    ''' <summary>
    ''' Repositorio de contratos
    ''' </summary>
    Private _contractRepository As IContractRepository

    ''' <summary>
    ''' Repositorio de Parámetros de Nómina
    ''' </summary>
    ''' <remarks></remarks>
    Private _payrollSettingsRepository As IPayrollSettingsRepository

    ''' <summary>
    ''' Repositorio de Cargos
    ''' </summary>
    Private _PositionRepository As IPositionRepository

    ''' <summary>
    ''' Repositorio de Retroactivos
    ''' </summary>
    Private _retroactiveRepository As IRetroactiveCRepository

    ''' <summary>
    ''' Repositorio de Primas
    ''' </summary>
    Private _incentivePaymentRepository As IIncentivePaymentRepository

    ''' <summary>
    ''' Repositorio de Conceptos Manuales
    ''' </summary>
    Private _ManualConceptsRepository As IManualConcepts

    ''' <summary>
    ''' Repositorio de Festivos
    ''' </summary>
    Private _holidayRepository As Domain.Entities.IHolidayRepository

    ''' <summary>
    ''' Repositorio de Bancos
    ''' </summary>
    Private _entityBankAccount As IEntityBankAccountRepository

    Private _cashRegisterRepository As ICashRegisterRepository

    ''' <summary>
    ''' Repositorio de Empleados
    ''' </summary>
    Private _employeeRepository As IEmployeeRepository

    ''' <summary>
    ''' Repositorio de Conceptos Pagos
    ''' </summary>
    Private _expenseConceptRepository As IExpenseConceptRepository

    ''' <summary>
    ''' Repositorio de Empresas
    ''' </summary>
    Private _companyRepository As ICompanyRepository

    ''' <summary>
    ''' Repositorio de Embargos
    ''' </summary>
    Private _ForeclousureRepository As IForeclousureRepository

    ''' <summary>
    ''' Repositorio de Convenios
    ''' </summary>
    Private _AgreementsCRepository As IAgreementsCRepository

    ''' <summary>
    ''' Repositorio de Conceptos de Nómina en parámetros contables
    ''' </summary>
    Private _IConceptAccountingStructureRepository As IConceptAccountingStructureRepository

    ''' <summary>
    ''' Repositorio de Distribuciones de Gastos de Nómina
    ''' </summary>
    Private _CostDistributionRepository As ICostDistributionsRepository

    ''' <summary>
    ''' Repositorio de Fondos
    ''' </summary>
    Private _FundsRepository As IFundsLevelRepository

    ''' <summary>
    ''' Repositorio de Grupos
    ''' </summary>
    Private _groupRepository As IGroupRepository

    ''' <summary>
    ''' Repositorio de Terceros
    ''' </summary>
    Private _thirdPartyRepository As IThirdPartyRepository

    ''' <summary>
    ''' Repositorio de Autorización de Conceptos
    ''' </summary>
    Private _authorizationConceptRepository As IAuthorizationConceptRepository
    ''' <summary>
    ''' Repositorio de Liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationRepository As IPayrollLiquidationRepository


#End Region

#Region "Constructor"

    Public Sub New(unemployedLiquidationDomain As IUnemployedLiquidationDomain, unemployedLiquidationRepository As IUnemployedLiquidationRepository, contractLiquidationRepository As IContractLiquidationRepository,
                   liquidationDomain As ILiquidationDomain, incentivePaymentDomain As IIncentivePaymentDomain, payrollLiquidationRepository As IPayrollLiquidationRepository,
                   vacationPeriodDomain As IVacationPeriodDomain, vacationPeriodRepository As IVacationPeriodRepository, conceptRepository As IConceptRepository,
                   retirementReasonRepository As IRetirementReasonRepository, noveltyRepository As INoveltyRepository, contractRepository As IContractRepository, payrollSettingsRepository As IPayrollSettingsRepository,
                   PositionRepository As IPositionRepository, retroactiveRepository As IRetroactiveCRepository, incentivePaymentRepository As IIncentivePaymentRepository, ManualConceptsRepository As IManualConcepts,
                   holidayRepository As Domain.Entities.IHolidayRepository, entityBankAccount As IEntityBankAccountRepository, cashRegisterRepository As ICashRegisterRepository, employeeRepository As IEmployeeRepository,
                   expenseConceptRepository As IExpenseConceptRepository, companyRepository As ICompanyRepository, foreclousureRepository As IForeclousureRepository, AgreementsCRepository As IAgreementsCRepository,
                   IConceptAccountingStructureRepository As IConceptAccountingStructureRepository, costDistributionRepository As ICostDistributionsRepository, fundsRepository As IFundsLevelRepository, groupRepository As IGroupRepository,
                   thirdPartyRepository As IThirdPartyRepository, authorizationConceptRepository As IAuthorizationConceptRepository, liquidationRepository As IPayrollLiquidationRepository)

        '------------------------------Servicios de dominio

        If unemployedLiquidationDomain Is Nothing Then
            Throw New ArgumentNullException("Servicio de dominio de liquidacion de cesantias vacio")
        End If

        If liquidationDomain Is Nothing Then
            Throw New ArgumentNullException("Servicio de dominio de liquidacion de nomina vacio")
        End If

        If incentivePaymentDomain Is Nothing Then
            Throw New ArgumentNullException("Servicio de dominio de primas de nomina vacio")
        End If

        If vacationPeriodDomain Is Nothing Then
            Throw New ArgumentNullException("Servicio de dominio de primas de nomina vacio")
        End If


        '-------------------------------Repositorios

        If unemployedLiquidationRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de liquidacion de cesantias vacio")
        End If

        If contractLiquidationRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Liquidacion de contrato vacio")
        End If

        If payrollLiquidationRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Liquidacion de Nominas vacio")
        End If

        If vacationPeriodRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de periodos de vacaciones vacio")
        End If

        If conceptRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de conceptos vacio")
        End If

        If unemployedLiquidationRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de Cesantias vacio")
        End If

        If ManualConceptsRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio de ManualConceptsRepository vacio")
        End If

        _liquidationDomain = liquidationDomain
        _incentivePaymentDomain = incentivePaymentDomain
        _unemployedLiquidationDomain = unemployedLiquidationDomain
        _vacationPeriodDomain = vacationPeriodDomain

        _unemployedLiquidationRepository = unemployedLiquidationRepository
        _contractLiquidationRepository = contractLiquidationRepository
        _payrollLiquidationRepository = payrollLiquidationRepository
        _vacationPeriodRepository = vacationPeriodRepository
        _conceptRepository = conceptRepository
        _retirementReasonRepository = retirementReasonRepository
        _noveltyRepository = noveltyRepository
        _contractRepository = contractRepository
        _payrollSettingsRepository = payrollSettingsRepository
        _PositionRepository = PositionRepository
        _retroactiveRepository = retroactiveRepository
        _incentivePaymentRepository = incentivePaymentRepository
        _ManualConceptsRepository = ManualConceptsRepository
        _holidayRepository = holidayRepository
        _entityBankAccount = entityBankAccount
        _cashRegisterRepository = cashRegisterRepository
        _employeeRepository = employeeRepository
        _expenseConceptRepository = expenseConceptRepository
        _companyRepository = companyRepository
        _ForeclousureRepository = foreclousureRepository
        _AgreementsCRepository = AgreementsCRepository
        _IConceptAccountingStructureRepository = IConceptAccountingStructureRepository
        _FundsRepository = fundsRepository
        _CostDistributionRepository = costDistributionRepository
        _groupRepository = groupRepository
        _thirdPartyRepository = thirdPartyRepository
        _authorizationConceptRepository = authorizationConceptRepository
        _liquidationRepository = liquidationRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que liquida el empleado a la fecha de retiro enviada
    ''' </summary>
    ''' <param name="employee">Empleado a liquidar</param>
    ''' <param name="retirementDate">Fecha de retiro</param>
    ''' <returns>Liquidacion del contrato</returns>
    Public Function LiquidateContract(employee As Entities.Employee, retirementDate As Date, retirementReasonId As Integer, ByVal session As SessionValues) As ActionMessageResult(Of Entities.ContractLiquidation) Implements IContractLiquidationDomain.LiquidateContract
        Dim result As New ActionMessageResult(Of ContractLiquidation)()
        result.StateResult = True

        Try

            Dim PayrollSettings = _payrollSettingsRepository.GetSettingPayroll()

            If PayrollSettings Is Nothing Then
                result.StateResult = False
                result.Message = "No se encontraron Parámetros de Nómina"
                Return result
            End If

            If PayrollSettings.IdConceptUnemployment Is Nothing Then
                result.StateResult = False
                result.Message = "En Parámetros de Nómina debe asignarle un Concepto a las Cesantias"
                Return result
            End If

            If PayrollSettings.IdConceptInterestUnemployment Is Nothing Then
                result.StateResult = False
                result.Message = "En Parámetros de Nómina debe asignarle un Concepto a los Intereses de Cesantias"
                Return result
            End If

            If PayrollSettings.ChristmasIncentivePaymentConceptId Is Nothing Then
                result.StateResult = False
                result.Message = "En Parámetros de Nómina debe asignarle un Concepto a las Primas"
                Return result
            End If

            Dim ListManualConcepts = _ManualConceptsRepository.GetManualConceptsByEmployeeIdInitialDate(employee.Id, retirementDate, 1, 5)

            Dim contractLiquidation As New ContractLiquidation

            Dim WorkDays As Integer = 0

            Dim HealthValue As Double = 0
            Dim PensionValue As Double = 0
            Dim SolidarityFundValue As Double = 0
            Dim VoluntaryHealhValue As Double = 0

            Dim Integral As Boolean = False

            'contrato vigente
            Dim validContract = employee.Contract.Where(Function(i) i.Valid = True).FirstOrDefault

            If validContract.ContractInitialDate > retirementDate And validContract.LastLiquidationDate Is Nothing Then
                validContract = employee.Contract.Where(Function(x) x.Valid = False And x.Status = 4).LastOrDefault()
            End If

            If validContract.ContractType.SalaryType = 2 Then
                Integral = True
            End If

            If validContract.ContractType.ContractClass = 3 Or validContract.ContractType.ContractClass = 4 Then
                If validContract.FundContract.Any(Function(x) x.FundType = 1) = False Then
                    result.StateResult = False
                    result.Message = "No se encontró Fondo de Salud. Debe agregarle al Empleado primero, antes de Liquidar"
                    Return result
                End If

                If validContract.FundContract.Any(Function(x) x.FundType = 2) = False AndAlso employee.Pensionary = False Then
                    result.StateResult = False
                    result.Message = "No se encontró Fondo de Pensión. Debe agregarle al Empleado primero, antes de Liquidar"
                    Return result
                End If
            End If

            Dim TarifaAprox As Integer = validContract.Group.PayrollParameter.AproximationValue

            validContract.RetirementReasonId = retirementReasonId
            validContract.Status = 2
            'Fecha de vinculacion - Fecha inicial de liquidacion
            Dim jobBondingDate As Date = validContract.JobBondingDate

            Dim PeriodInitialDate = New Date(Year(retirementDate), 1, 1)

            If PeriodInitialDate > jobBondingDate Then
                WorkDays = _liquidationDomain.Days360(PeriodInitialDate, retirementDate)
            Else
                WorkDays = _liquidationDomain.Days360(jobBondingDate, retirementDate)
            End If

            Dim NewInitialDate As Date = New Date(retirementDate.Year, 1, 1)
            Dim NewEndDate As Date = New Date(retirementDate.Year, 12, 31)

            Dim ListLiquidationLastYear = _payrollLiquidationRepository.GetConfirmLiquidationByStarEndDateRetroactive(NewInitialDate, NewEndDate, validContract.GroupId)

            '--------------------------------------------------------------------- Liquidacion de Nomina
            'Fecha minima de liquidacion de nomina pendiente - ajustada según tipo de nómina
            Dim liqMinDate As Date

            ' Calcular fecha inicial según tipo de nómina
            If validContract.Group.Liquidation = 2 AndAlso retirementDate.Day > 15 Then
                ' Nómina quincenal - segunda quincena
                liqMinDate = New Date(retirementDate.Year, retirementDate.Month, 16)
            Else
                ' Nómina mensual o quincenal primera quincena
                liqMinDate = New Date(retirementDate.Year, retirementDate.Month, 1)
            End If

            If jobBondingDate > liqMinDate Then
                liqMinDate = jobBondingDate
            End If

            Dim XtraTotalPaid As Decimal = 0
            Dim XtraTotalDeducted As Decimal = 0

            Dim xtraLiquidation As Liquidation

            Dim FlagPreviewLiquidation As Boolean = False

            Dim ListAgreements As List(Of AgreementsC)

            Dim ObjGroup = _groupRepository.GetGroupById(validContract.GroupId)

            If (validContract.LastLiquidationDate Is Nothing) Or validContract.LastLiquidationDate <= retirementDate Then
                xtraLiquidation = LiquidatePayroll(employee, liqMinDate, retirementDate, session)
                If xtraLiquidation IsNot Nothing Then
                    xtraLiquidation.Contract = validContract
                    xtraLiquidation.Group = validContract.Group

                    XtraTotalPaid = xtraLiquidation.TotalPaid
                    XtraTotalDeducted = xtraLiquidation.TotalDeducted

                    Dim messageError = From e In xtraLiquidation.Message
                                       Select e

                    If messageError IsNot Nothing Then
                        If messageError.Count > 0 Then
                            For Each itemMessage As Payroll.Entities.Message In messageError.ToList()
                                If itemMessage.Error = True Then
                                    If itemMessage.Description = "El TOTAL PAGADO está en Negativo. No se puede CONFIRMAR" Then
                                        result.StateResult = True
                                    Else
                                        result.StateResult = False
                                    End If

                                End If
                                result.MessageResult.Add(New MessageResult(itemMessage.Description, itemMessage.Error))
                            Next
                        End If
                    End If

                    For Each detail As LiquidationDetail In xtraLiquidation.LiquidationDetail

                        detail.ConceptTotalValue = Utils.RoundValue(detail.ConceptTotalValue, TarifaAprox)

                        Dim detailContract As New ContractLiquidationDetail() With {.IdConcept = detail.ConceptId, .ConceptType = detail.ConceptType, .Description = detail.ConceptDetail, .InitialDate = liqMinDate, .EndingDate = retirementDate, .ConceptFormulate = detail.ConceptFormulate, .ReplaceConceptFormulate = detail.ReplaceConceptFormulate}
                        If detail.ConceptType = "1" Then
                            detailContract.Accrued = detail.ConceptTotalValue
                        ElseIf detail.ConceptType = "2" Then

                            If detail.ConceptClass = "017" Then
                                HealthValue = detail.ConceptTotalValue
                            End If
                            If detail.ConceptClass = "014" Then
                                PensionValue = detail.ConceptTotalValue
                            End If
                            If detail.ConceptClass = "038" Then
                                SolidarityFundValue = detail.ConceptTotalValue
                            End If
                            If detail.ConceptClass = "019" Then
                                VoluntaryHealhValue = detail.ConceptTotalValue
                            End If


                            detailContract.Deducted = detail.ConceptTotalValue
                        Else
                            detailContract.Accrued = detail.ConceptTotalValue
                        End If
                        If detail.ConceptClass <> "020" Then
                            contractLiquidation.ContractLiquidationDetail.Add(detailContract)
                        End If
                    Next
                End If
            Else
                'Ya se liquidó consultamos la liquidación del mes para usarla para cálculos
                FlagPreviewLiquidation = True
                Dim TransportHealthValue = validContract.Group.PayrollParameter.TransportHelpValue
                Dim LegalSalaryMinimun = validContract.Group.PayrollParameter.LegalSalaryMinimum

                'Valido Auxilio de Tranporte
                If validContract.BasicSalary > (2 * LegalSalaryMinimun) Then
                    TransportHealthValue = 0
                End If

                'Calcular el período correcto de liquidación según tipo de nómina y fecha de retiro
                Dim InitialRetirementDate As Date
                Dim EndRetirementDate As Date
                GetPayrollPeriodDates(retirementDate, validContract.Group.Liquidation, InitialRetirementDate, EndRetirementDate)

                If validContract.ContractEndingDate > retirementDate Then

                    Dim ListAdjustSalaryValue = AdjustBasicSalary(validContract.BasicSalary, retirementDate, InitialRetirementDate, EndRetirementDate, TransportHealthValue, LegalSalaryMinimun, ObjGroup)

                    If ListAdjustSalaryValue.StateResult = True Then

                        If ListAdjustSalaryValue.ObjectEmbbeded IsNot Nothing AndAlso ListAdjustSalaryValue.ObjectEmbbeded.Count > 0 Then
                            For Each objContractDetail As ContractLiquidationDetail In ListAdjustSalaryValue.ObjectEmbbeded
                                objContractDetail.Accrued = Utils.RoundValue(objContractDetail.Accrued, TarifaAprox)
                                objContractDetail.Deducted = Utils.RoundValue(objContractDetail.Deducted, TarifaAprox)
                                contractLiquidation.ContractLiquidationDetail.Add(objContractDetail)
                            Next
                        End If
                    Else
                        If ListAdjustSalaryValue.MessageResult.Count > 0 Then
                            result.StateResult = False
                            result.MessageResult = ListAdjustSalaryValue.MessageResult
                            Return result
                        End If

                    End If

                End If

                xtraLiquidation = _payrollLiquidationRepository.GetLiquidationByEmployeeIdYearMonth(validContract.EmployeeId, retirementDate.Year, retirementDate.Month)

                'Averiguo los Convenios que existan:
                ListAgreements = _AgreementsCRepository.GetAgreementsByEmployeeLiquidationContract(validContract.EmployeeId, "2")

            End If

            validContract.RetirementDate = retirementDate

            validContract.Liquidation = Nothing
            validContract.Group.Liquidation1 = Nothing

            contractLiquidation.Employee = employee
            contractLiquidation.Contract = validContract
            contractLiquidation.RetirementDate = retirementDate
            contractLiquidation.Status = "C"
            Dim retirementReason = _retirementReasonRepository.GetRetirementReasonById(retirementReasonId)
            contractLiquidation.RetirementReason = retirementReason
            contractLiquidation.TotalDeducted = XtraTotalDeducted
            contractLiquidation.TotalPaid = XtraTotalPaid

            Dim BaseRTF As Decimal = 0
            Dim BaseHealth As Decimal = 0
            Dim BasePension As Decimal = 0
            Dim BasePrimasCesantias As Decimal = 0

            Dim BaseReajusteSalud As Decimal = 0
            Dim BaseReajustePension As Decimal = 0

            'Agregamos a la Liquidación los Conceptos Manuales creados
            If ListManualConcepts IsNot Nothing AndAlso ListManualConcepts.Count > 0 Then
                For Each ObjManualConcepts As ManualConcepts In ListManualConcepts

                    ObjManualConcepts.QuoteValue = Utils.RoundValue(If(ObjManualConcepts.QuoteValue, 0), TarifaAprox)

                    If ObjManualConcepts.Concept.ConceptType = 1 Then
                        'Devengado
                        Dim ObjManualConceptAccrued As New ContractLiquidationDetail() With {.IdConcept = ObjManualConcepts.ConceptId, .ConceptType = ObjManualConcepts.Concept.ConceptType, .Description = ObjManualConcepts.Concept.Name, .InitialDate = liqMinDate, .EndingDate = retirementDate, .Accrued = ObjManualConcepts.QuoteValue, .Deducted = 0}
                        contractLiquidation.ContractLiquidationDetail.Add(ObjManualConceptAccrued)
                        contractLiquidation.TotalAccrued += ObjManualConcepts.QuoteValue
                        contractLiquidation.TotalPaid += ObjManualConcepts.QuoteValue

                        If ObjManualConcepts.Concept.AffectIBCRTF = True Then
                            BaseRTF = BaseRTF + ObjManualConcepts.QuoteValue
                        End If

                        If ObjManualConcepts.Concept.AffectIBCHealth = True Then
                            BaseHealth = BaseHealth + ObjManualConcepts.QuoteValue
                        End If

                        If ObjManualConcepts.Concept.AffectIBCPension = True Then
                            BasePension = BasePension + ObjManualConcepts.QuoteValue
                        End If

                        If ObjManualConcepts.Concept.AffectIBCIncentivePayment = True Then
                            BasePrimasCesantias = BasePrimasCesantias + ObjManualConcepts.QuoteValue
                        End If

                    Else
                        Dim ObjManualConceptDeducted As New ContractLiquidationDetail() With {.IdConcept = ObjManualConcepts.ConceptId, .ConceptType = ObjManualConcepts.Concept.ConceptType, .Description = ObjManualConcepts.Concept.Name, .InitialDate = liqMinDate, .EndingDate = retirementDate, .Accrued = 0, .Deducted = ObjManualConcepts.QuoteValue}
                        contractLiquidation.ContractLiquidationDetail.Add(ObjManualConceptDeducted)
                        contractLiquidation.TotalDeducted += ObjManualConcepts.QuoteValue
                        contractLiquidation.TotalPaid -= ObjManualConcepts.QuoteValue

                        If ObjManualConcepts.Concept.AffectIBCRTF = True Then
                            BaseRTF = BaseRTF - ObjManualConcepts.QuoteValue
                        End If

                        If FlagPreviewLiquidation Then

                            If ObjManualConcepts.Concept.AffectIBCHealth = True Then
                                BaseReajusteSalud = BaseReajusteSalud + ObjManualConcepts.QuoteValue
                            End If

                            If ObjManualConcepts.Concept.AffectIBCPension = True Then
                                BaseReajustePension = BaseReajustePension + ObjManualConcepts.QuoteValue
                            End If
                        Else

                            If ObjManualConcepts.Concept.AffectIBCHealth = True Then
                                BaseHealth = BaseHealth - ObjManualConcepts.QuoteValue
                            End If

                            If ObjManualConcepts.Concept.AffectIBCPension = True Then
                                BasePension = BasePension - ObjManualConcepts.QuoteValue
                            End If

                            If ObjManualConcepts.Concept.AffectIBCIncentivePayment = True Then
                                BasePrimasCesantias = BasePrimasCesantias - ObjManualConcepts.QuoteValue
                            End If
                        End If

                    End If
                Next
            End If

            If contractLiquidation.ContractLiquidationDetail IsNot Nothing AndAlso contractLiquidation.ContractLiquidationDetail.Count > 0 Then

                For Each objContractLiquidationDetail As ContractLiquidationDetail In contractLiquidation.ContractLiquidationDetail

                    If objContractLiquidationDetail.IdConcept Is Nothing Then
                        result.StateResult = False
                        result.Message = "El Concepto " + objContractLiquidationDetail.Description + " no le está cargado Id de Concepto"
                        Return result
                    End If

                    Dim ObConcept = _conceptRepository.GetConceptId(objContractLiquidationDetail.IdConcept)

                    If Math.Abs(BaseHealth) > 0 Then

                        If ObConcept.ConceptClass = "017" Then
                            If objContractLiquidationDetail.Deducted > 0 Then
                                objContractLiquidationDetail.Deducted = Utils.RoundValue(Convert.ToDecimal(objContractLiquidationDetail.Deducted + (BaseHealth * 0.04)), TarifaAprox)
                                HealthValue = HealthValue + objContractLiquidationDetail.Deducted
                            End If
                        End If
                    End If

                    If Math.Abs(BasePension) > 0 Then
                        If ObConcept.ConceptClass = "014" Then
                            If objContractLiquidationDetail.Deducted > 0 Then
                                objContractLiquidationDetail.Deducted = Utils.RoundValue(Convert.ToDecimal(objContractLiquidationDetail.Deducted + (BasePension * 0.04)), TarifaAprox)
                                PensionValue = PensionValue + objContractLiquidationDetail.Deducted
                            End If
                        End If
                    End If

                Next

            End If

            If FlagPreviewLiquidation Then
                If BaseHealth > 0 Then
                    Dim ListConcept = _conceptRepository.GetConceptByConceptClass(New List(Of String)(New String() {"017"}))

                    If ListConcept IsNot Nothing AndAlso ListConcept.Count > 0 Then
                        Dim ObjConcept = ListConcept.Where(Function(x) x.ConceptType = 2 And x.Formulates.Contains("[% Salud Empleado]")).FirstOrDefault()

                        If ObjConcept IsNot Nothing Then
                            Dim ObjManualConceptDeducted As New ContractLiquidationDetail() With {.IdConcept = ObjConcept.Id, .ConceptType = ObjConcept.ConceptType, .Description = ObjConcept.Name, .InitialDate = liqMinDate, .EndingDate = retirementDate, .Accrued = 0, .Deducted = (BaseHealth * 0.04)}
                            contractLiquidation.ContractLiquidationDetail.Add(ObjManualConceptDeducted)
                            contractLiquidation.TotalDeducted += (BaseHealth * 0.04)
                            contractLiquidation.TotalPaid -= (BaseHealth * 0.04)
                        End If

                    End If
                End If

                If BasePension > 0 Then
                    Dim ListConcept = _conceptRepository.GetConceptByConceptClass(New List(Of String)(New String() {"014"}))

                    If ListConcept IsNot Nothing AndAlso ListConcept.Count > 0 Then
                        Dim ObjConcept = ListConcept.Where(Function(x) x.ConceptType = 2 And x.Formulates.Contains("[% Pensión Empleado]")).FirstOrDefault()

                        If ObjConcept IsNot Nothing Then
                            Dim ObjManualConceptDeducted As New ContractLiquidationDetail() With {.IdConcept = ObjConcept.Id, .ConceptType = ObjConcept.ConceptType, .Description = ObjConcept.Name, .InitialDate = liqMinDate, .EndingDate = retirementDate, .Accrued = 0, .Deducted = (BasePension * 0.04)}
                            contractLiquidation.ContractLiquidationDetail.Add(ObjManualConceptDeducted)
                            contractLiquidation.TotalDeducted += (BasePension * 0.04)
                            contractLiquidation.TotalPaid -= (BasePension * 0.04)
                        End If
                    End If
                End If

                If ObjGroup.GroupAdjustConceptContractLiquidation IsNot Nothing AndAlso ObjGroup.GroupAdjustConceptContractLiquidation.Count > 0 Then

                    For Each objGroupAdjuntsConcept As GroupAdjustConceptContractLiquidation In ObjGroup.GroupAdjustConceptContractLiquidation
                        If objGroupAdjuntsConcept.ConceptType = 1 Then
                            If contractLiquidation.ContractLiquidationDetail.Any(Function(x) x.IdConcept = objGroupAdjuntsConcept.IdConcept) Then
                                Dim objtmpContractLiquidation = contractLiquidation.ContractLiquidationDetail.Where(Function(x) x.IdConcept = objGroupAdjuntsConcept.IdConcept).FirstOrDefault
                                BaseReajusteSalud = BaseReajusteSalud + objtmpContractLiquidation.Deducted
                            End If

                            If contractLiquidation.ContractLiquidationDetail.Any(Function(x) x.IdConcept = objGroupAdjuntsConcept.IdConcept) Then
                                Dim objtmpContractLiquidation = contractLiquidation.ContractLiquidationDetail.Where(Function(x) x.IdConcept = objGroupAdjuntsConcept.IdConcept).FirstOrDefault
                                BaseReajustePension = BaseReajustePension + objtmpContractLiquidation.Deducted
                            End If

                        End If
                    Next
                End If


                If BaseReajusteSalud > 0 Then

                    If ObjGroup.GroupAdjustConceptContractLiquidation.Any(Function(x) x.ConceptType = 3) Then

                        Dim ObjGrouptmp = _conceptRepository.GetConceptId(ObjGroup.GroupAdjustConceptContractLiquidation.Where(Function(x) x.ConceptType = 3).FirstOrDefault.IdConcept)

                        Dim NewReajusteHealthContractDetail As New ContractLiquidationDetail
                        NewReajusteHealthContractDetail.IdConcept = ObjGrouptmp.Id
                        NewReajusteHealthContractDetail.ConceptType = ObjGrouptmp.ConceptType
                        NewReajusteHealthContractDetail.InitialDate = liqMinDate
                        NewReajusteHealthContractDetail.EndingDate = retirementDate
                        NewReajusteHealthContractDetail.Accrued = BaseReajusteSalud * 0.04
                        NewReajusteHealthContractDetail.Deducted = 0
                        NewReajusteHealthContractDetail.Description = ObjGrouptmp.Name
                        NewReajusteHealthContractDetail.ConceptFormulate = ObjGrouptmp.Formulates
                        NewReajusteHealthContractDetail.ReplaceConceptFormulate = BaseReajusteSalud
                        contractLiquidation.ContractLiquidationDetail.Add(NewReajusteHealthContractDetail)
                    End If
                End If

                If BaseReajustePension > 0 Then
                    If ObjGroup.GroupAdjustConceptContractLiquidation.Any(Function(x) x.ConceptType = 4) Then
                        Dim ObjGrouptmp = _conceptRepository.GetConceptId(ObjGroup.GroupAdjustConceptContractLiquidation.Where(Function(x) x.ConceptType = 4).FirstOrDefault.IdConcept)
                        Dim NewReajustePensionContractDetail As New ContractLiquidationDetail
                        NewReajustePensionContractDetail.IdConcept = ObjGrouptmp.Id
                        NewReajustePensionContractDetail.ConceptType = ObjGrouptmp.ConceptType
                        NewReajustePensionContractDetail.InitialDate = liqMinDate
                        NewReajustePensionContractDetail.EndingDate = retirementDate
                        NewReajustePensionContractDetail.Accrued = BaseReajustePension * 0.04
                        NewReajustePensionContractDetail.Deducted = 0
                        NewReajustePensionContractDetail.Description = ObjGrouptmp.Name
                        NewReajustePensionContractDetail.ConceptFormulate = ObjGrouptmp.Formulates
                        NewReajustePensionContractDetail.ReplaceConceptFormulate = BaseReajustePension
                        contractLiquidation.ContractLiquidationDetail.Add(NewReajustePensionContractDetail)
                    End If
                End If
            End If


            '--------------------------------------------------------------------- CONVENIOS
            If ListAgreements IsNot Nothing AndAlso ListAgreements.Count > 0 Then

                For Each objAgreementsC As AgreementsC In ListAgreements
                    Dim ListConcept = _conceptRepository.GetConceptId(objAgreementsC.ConceptId)

                    Dim ValueAgreements As Decimal = 0

                    If objAgreementsC.NumberShares = 0 Then
                        ValueAgreements = objAgreementsC.AgreementValue
                    Else
                        ValueAgreements = objAgreementsC.AgreementValue / objAgreementsC.NumberShares
                    End If

                    Dim ObjAgreements As New ContractLiquidationDetail() With {.IdConcept = objAgreementsC.ConceptId, .ConceptType = 2, .Description = ListConcept.Name, .InitialDate = retirementDate, .EndingDate = retirementDate, .Accrued = 0, .Deducted = ValueAgreements}
                    contractLiquidation.ContractLiquidationDetail.Add(ObjAgreements)
                Next


            End If


            '--------------------------------------------------------------------- Cesantias
            Dim SanctionDays As Integer = 0
            Dim unemployeedTotalPaid As Decimal = 0
            Dim unemployeedTotalInterest As Decimal = 0
            Dim unemployeedTotalPaidLastYear As Decimal = 0
            Dim unemployeedTotalInterestLastYear As Decimal = 0
            Dim UnemployementDays As Integer = 0

            If Integral = False Then

                Dim UnemployedReplaceFormulate As String
                Dim UnemployedConceptFormulate As String
                Dim UnemployementInitialDate As Date
                LiquidateUnemployed(employee, jobBondingDate, retirementDate, unemployeedTotalPaid, unemployeedTotalInterest, SanctionDays, UnemployedReplaceFormulate, UnemployedConceptFormulate, UnemployementInitialDate, xtraLiquidation, BasePrimasCesantias)

                unemployeedTotalPaid = Utils.RoundValue(unemployeedTotalPaid, TarifaAprox)
                unemployeedTotalInterest = Utils.RoundValue(unemployeedTotalInterest, TarifaAprox)

                contractLiquidation.TotalAccrued += unemployeedTotalPaid + unemployeedTotalInterest
                contractLiquidation.TotalPaid += unemployeedTotalPaid + unemployeedTotalInterest
                Dim detailUnemployeed As New ContractLiquidationDetail() With {.IdConcept = PayrollSettings.IdConceptUnemployment, .ConceptType = 1, .Description = "CESANTIAS", .InitialDate = UnemployementInitialDate, .EndingDate = retirementDate, .Accrued = unemployeedTotalPaid + 0, .Deducted = 0, .ConceptFormulate = UnemployedConceptFormulate, .ReplaceConceptFormulate = UnemployedReplaceFormulate}

                Dim InitialUnemploymentDate As Date = New Date(Year(retirementDate), 1, 1)
                Dim EndUnemploymentDate As Date = retirementDate

                If jobBondingDate > UnemployementInitialDate Then
                    UnemployementDays = _liquidationDomain.Days360(jobBondingDate, retirementDate)
                Else
                    UnemployementDays = _liquidationDomain.Days360(UnemployementInitialDate, retirementDate)
                End If

                If retirementDate.Month = 2 AndAlso ObjGroup.Month = 2 Then
                    If retirementDate.Day = 28 Then
                        UnemployementDays = UnemployementDays - 2
                    End If

                    If retirementDate.Day = 29 Then
                        UnemployementDays = UnemployementDays - 1
                    End If
                End If

                Dim InterestConceptFormulate As String = "(([Valor Cesantias] * ([Dias Intereses Trabajados] - (([Dias Lic. No Remunerada] + [Dias Sancion])) * 0.12) / 360)"
                Dim InterestReplaceFormulate As String = "((" + unemployeedTotalPaid.ToString() + " * (" + UnemployementDays.ToString + " - ( " + SanctionDays.ToString + " )) * 0.12) / 360)"
                Dim detailUnemployeedInterest As New ContractLiquidationDetail() With {.IdConcept = PayrollSettings.IdConceptInterestUnemployment, .ConceptType = 1, .Description = "INTERESES A CESANTIAS", .InitialDate = UnemployementInitialDate, .EndingDate = retirementDate, .Accrued = Utils.RoundValue(Convert.ToDecimal((((unemployeedTotalPaid * (UnemployementDays - SanctionDays) * 0.12) / 360) + 0)), TarifaAprox), .Deducted = 0, .ReplaceConceptFormulate = InterestReplaceFormulate, .ConceptFormulate = InterestConceptFormulate}
                contractLiquidation.ContractLiquidationDetail.Add(detailUnemployeed)
                contractLiquidation.ContractLiquidationDetail.Add(detailUnemployeedInterest)

                '--------------------------------------------------------------------- Cesantias Año Anterior
                If retirementDate.Month = 1 Then

                    Dim LastDateUnemployment As New Date(Year(retirementDate) - 1, 12, 31)
                    Dim LastDateInitialUnemployment As New Date(Year(retirementDate) - 1, 1, 1)

                    Dim listunemployedLiquidationPreviousYear = _unemployedLiquidationRepository.GetUnemployedLiquidationEmployeeByDate(employee.Id, LastDateInitialUnemployment, LastDateUnemployment)
                    'Si las cesantías no han sido confirmadas, las liquido y las agrego al detalle de liquidación
                    'Validando que el contrato haya iniciado en un año previo al año del retiro
                    If (listunemployedLiquidationPreviousYear.Where(Function(x) x.Status = False).Count > 0 Or listunemployedLiquidationPreviousYear.Count <= 0) And (Year(jobBondingDate) < Year(retirementDate)) Then
                        'Agrego las cesantías del año anterior
                        If jobBondingDate > LastDateInitialUnemployment Then
                            LastDateInitialUnemployment = jobBondingDate
                        End If

                        If jobBondingDate > LastDateInitialUnemployment Then
                            UnemployementDays = _liquidationDomain.Days360(jobBondingDate, LastDateUnemployment)
                        Else
                            UnemployementDays = _liquidationDomain.Days360(LastDateInitialUnemployment, retirementDate)
                        End If

                        SanctionDays = 0

                        LiquidateUnemployed(employee, LastDateInitialUnemployment, LastDateUnemployment, unemployeedTotalPaid, unemployeedTotalInterest, SanctionDays, UnemployedReplaceFormulate, UnemployedConceptFormulate, UnemployementInitialDate, Nothing, 0)

                        'Si no hay formula para cesantías es porque no tiene liquidación de Nómina y no se deben agregar esos conceptos
                        If Not String.IsNullOrEmpty(UnemployedConceptFormulate) Then
                            unemployeedTotalPaid = Utils.RoundValue(unemployeedTotalPaid, TarifaAprox)
                            unemployeedTotalInterest = Utils.RoundValue(unemployeedTotalInterest, TarifaAprox)

                            contractLiquidation.TotalAccrued += unemployeedTotalPaid + unemployeedTotalInterest
                            contractLiquidation.TotalPaid += unemployeedTotalPaid + unemployeedTotalInterest

                            Dim detailUnemployeedLastYear As New ContractLiquidationDetail() With {.IdConcept = PayrollSettings.IdConceptUnemployment, .ConceptType = 1, .Description = "CESANTIAS AÑO ANTERIOR", .InitialDate = UnemployementInitialDate, .EndingDate = retirementDate, .Accrued = unemployeedTotalPaid, .Deducted = 0, .ConceptFormulate = UnemployedConceptFormulate, .ReplaceConceptFormulate = UnemployedReplaceFormulate}

                            Dim InterestConceptFormulateLastYear As String = "(([Valor Cesantias] * ([Dias Intereses Trabajados] - (([Dias Lic. No Remunerada] + [Dias Sancion])) * 0.12) / 360)"
                            Dim InterestReplaceFormulateLastYear As String = "((" + unemployeedTotalPaid.ToString() + " * (" + UnemployementDays.ToString + " - ( " + SanctionDays.ToString + " )) * 0.12) / 360)"
                            Dim detailUnemployeedInterestLastYear As New ContractLiquidationDetail() With {.IdConcept = PayrollSettings.IdConceptInterestUnemployment, .ConceptType = 1, .Description = "INTERESES A CESANTIAS AÑO ANTERIOR", .InitialDate = UnemployementInitialDate, .EndingDate = retirementDate, .Accrued = Utils.RoundValue(Convert.ToDecimal((((unemployeedTotalPaid * (UnemployementDays - SanctionDays) * 0.12) / 360))), TarifaAprox), .Deducted = 0, .ReplaceConceptFormulate = InterestReplaceFormulateLastYear, .ConceptFormulate = InterestConceptFormulateLastYear}

                            contractLiquidation.ContractLiquidationDetail.Add(detailUnemployeedInterestLastYear)
                            contractLiquidation.ContractLiquidationDetail.Add(detailUnemployeedLastYear)
                        End If
                    End If

                End If

                '--------------------------------------------------------------------  Primas
                Dim incentiveTotalPaid As Decimal
                Dim IncentiveReplaceFormulate As String
                Dim IncentiveConceptFormulate As String
                Dim StarDateIncentivePayment As Date
                LiquidateIncentives(validContract, retirementDate, xtraLiquidation, incentiveTotalPaid, session, IncentiveReplaceFormulate, IncentiveConceptFormulate, StarDateIncentivePayment, BasePrimasCesantias)

                incentiveTotalPaid = Utils.RoundValue(incentiveTotalPaid, TarifaAprox)
                contractLiquidation.TotalAccrued += incentiveTotalPaid
                contractLiquidation.TotalPaid += incentiveTotalPaid

                Dim detailIncentives As New ContractLiquidationDetail() With {.IdConcept = PayrollSettings.ChristmasIncentivePaymentConceptId, .ConceptType = 1, .Description = "PRIMAS", .InitialDate = StarDateIncentivePayment, .EndingDate = retirementDate, .Accrued = incentiveTotalPaid, .Deducted = 0, .ConceptFormulate = IncentiveConceptFormulate, .ReplaceConceptFormulate = IncentiveReplaceFormulate}

                If incentiveTotalPaid > 0 Then
                    contractLiquidation.ContractLiquidationDetail.Add(detailIncentives)
                End If
            End If

            '-------------------------------------------------------------------- Vacaciones
            Dim vacationTotalPaid As Decimal
            Dim VacationReplaceFormulate As String
            Dim VacationConceptFormulate As String

            LiquidateVacations(employee, validContract, jobBondingDate, retirementDate, vacationTotalPaid, xtraLiquidation, VacationReplaceFormulate, VacationConceptFormulate, BasePrimasCesantias)

            vacationTotalPaid = Utils.RoundValue(vacationTotalPaid, TarifaAprox)

            If vacationTotalPaid <> 0 Then
                Dim detailVacation As New ContractLiquidationDetail() With {.IdConcept = PayrollSettings.IdVacationConcept, .ConceptType = 1, .Description = "VACACIONES", .InitialDate = employee.VacationLastDateLiquidation, .EndingDate = retirementDate, .ConceptFormulate = VacationConceptFormulate, .ReplaceConceptFormulate = VacationReplaceFormulate}
                If vacationTotalPaid > 0 Then
                    detailVacation.Accrued = vacationTotalPaid
                    detailVacation.Deducted = 0
                    contractLiquidation.ContractLiquidationDetail.Add(detailVacation)
                End If


                Dim ConceptCaja = _conceptRepository.GetConceptByClass("036")

                If ConceptCaja IsNot Nothing Then
                    If contractLiquidation.ContractLiquidationDetail.Any(Function(x) x.IdConcept = ConceptCaja.Id) Then
                        Dim ObjCaja = contractLiquidation.ContractLiquidationDetail.Where(Function(x) x.IdConcept = ConceptCaja.Id).FirstOrDefault()
                        ObjCaja.Accrued = ObjCaja.Accrued + (vacationTotalPaid * validContract.Group.PayrollParameter.CompensationFundContributionPercentage / 100)
                        ObjCaja.Accrued = Utils.RoundValue(ObjCaja.Accrued, TarifaAprox)
                        ObjCaja.ConceptFormulate = ObjCaja.ReplaceConceptFormulate + " + ([Valor Vacaciones] * [% Caja] / 100)"
                        ObjCaja.ReplaceConceptFormulate = ObjCaja.ReplaceConceptFormulate + " + (" + vacationTotalPaid.ToString + ") * " + validContract.Group.PayrollParameter.CompensationFundContributionPercentage.ToString + " / 100)"
                    End If
                End If
            End If

            'RETENCION EN LA FUENTE

            Dim VarRetentionValue As Decimal
            Dim RetentionReplaceFormulate As String

            Dim RetencionConcept = _conceptRepository.GetConcept("701")

            For Each ObjLiquidationDetail As ContractLiquidationDetail In contractLiquidation.ContractLiquidationDetail

                Dim ObjConcept = _conceptRepository.GetConceptId(ObjLiquidationDetail.IdConcept)

                If ObjConcept.AffectIBCRTF = True Then

                    If ObjLiquidationDetail.Accrued > 0 Then
                        BaseRTF = BaseRTF + ObjLiquidationDetail.Accrued
                    Else
                        BaseRTF = BaseRTF - ObjLiquidationDetail.Deducted
                    End If
                End If
            Next

            If RetencionConcept IsNot Nothing Then

                Dim RetentionPorcentage As Decimal = 0

                If PayrollSettings.RetentionFraction = True Then

                    'Se debe enviar los datos de la Liquidación del Mes Anterior
                    Dim NewInitialDateLiquidation = retirementDate.AddMonths(-1)
                    NewInitialDateLiquidation = New Date(NewInitialDateLiquidation.Year, NewInitialDateLiquidation.Month, 1)

                    Dim NewEndDateLiquidation = New Date(NewInitialDateLiquidation.Year, NewInitialDateLiquidation.Month, Date.DaysInMonth(NewInitialDateLiquidation.Year, NewInitialDateLiquidation.Month))

                    Dim ObjLiquidation = _payrollLiquidationRepository.GetConfirmLiquidationByStarEndDateRetefuente(NewInitialDateLiquidation, NewEndDateLiquidation, validContract.EmployeeId)

                    Dim IBCRtfTmp As Double
                    Dim HealthValueTmp As Double
                    Dim PensionValueTmp As Double
                    Dim SolidarityFundValueTmp As Double

                    If ObjLiquidation IsNot Nothing AndAlso ObjLiquidation.Count > 0 Then

                        For Each ObjtmpLiquidation As Liquidation In ObjLiquidation
                            For Each objTmpLiquidationDetail As LiquidationDetail In ObjtmpLiquidation.LiquidationDetail
                                If objTmpLiquidationDetail.Concept.AffectIBCRTF Then
                                    If objTmpLiquidationDetail.ConceptType = 1 Then
                                        IBCRtfTmp = IBCRtfTmp + objTmpLiquidationDetail.ConceptTotalValue
                                    Else
                                        IBCRtfTmp = IBCRtfTmp - objTmpLiquidationDetail.ConceptTotalValue
                                    End If
                                End If
                            Next

                            HealthValueTmp = ObjtmpLiquidation.LiquidationDetail.Where(Function(x) x.ConceptClass = "017").Sum(Function(y) y.DeductedValue)
                            PensionValueTmp = ObjtmpLiquidation.LiquidationDetail.Where(Function(x) x.ConceptClass = "014").Sum(Function(y) y.DeductedValue)
                            SolidarityFundValueTmp = ObjtmpLiquidation.LiquidationDetail.Where(Function(x) x.ConceptClass = "038").Sum(Function(y) y.DeductedValue)
                        Next

                    End If

                    VarRetentionValue = RetentionValue(validContract, IBCRtfTmp, PensionValueTmp, SolidarityFundValueTmp, HealthValueTmp, validContract.Group, retirementDate, VoluntaryHealhValue, PayrollSettings, ListLiquidationLastYear, unemployeedTotalPaid + ((((unemployeedTotalPaid * 12) / 100) * (UnemployementDays - SanctionDays)) / 360), PayrollSettings.RetentionFraction, RetentionPorcentage)
                End If

                VarRetentionValue = RetentionValue(validContract, BaseRTF, PensionValue, SolidarityFundValue, HealthValue, validContract.Group, retirementDate, VoluntaryHealhValue, PayrollSettings, ListLiquidationLastYear, unemployeedTotalPaid + ((((unemployeedTotalPaid * 12) / 100) * (UnemployementDays - SanctionDays)) / 360), PayrollSettings.RetentionFraction, RetentionPorcentage)

                RetentionReplaceFormulate = VarRetentionValue.ToString()
                If VarRetentionValue > 0 Then
                    Dim detailRetention As New ContractLiquidationDetail() With {.IdConcept = RetencionConcept.Id, .ConceptType = 2, .Description = RetencionConcept.Name, .InitialDate = liqMinDate, .EndingDate = retirementDate, .ReplaceConceptFormulate = RetentionReplaceFormulate, .ConceptFormulate = RetencionConcept.Formulates}
                    detailRetention.Accrued = 0
                    detailRetention.Deducted = VarRetentionValue
                    contractLiquidation.TotalDeducted += VarRetentionValue
                    contractLiquidation.ContractLiquidationDetail.Add(detailRetention)
                End If
            End If

            'Indemnizacion
            Dim VarIndemnizacion As Decimal
            If retirementReason.Compensation = True Then

                Dim AuthorizationConcept = _conceptRepository.GetConceptByClass("007")

                If AuthorizationConcept Is Nothing Then
                    result.StateResult = False
                    result.Message = "No se encontró concepto para el Pago de la Indemnización"
                    Return result
                End If

                Dim AverageSalary As Decimal = 0
                Dim SalaryIndemnization As Decimal = 0

                VarIndemnizacion = CalculateIndemnizacion(retirementDate, validContract, SalaryIndemnization, AverageSalary)
                If VarIndemnizacion > 0 Then

                    VarIndemnizacion = Utils.RoundValue(VarIndemnizacion, TarifaAprox)
                    AverageSalary = Utils.RoundValue(AverageSalary, TarifaAprox)
                    SalaryIndemnization = Utils.RoundValue(SalaryIndemnization, TarifaAprox)

                    Dim FormulaConcept As String = "[Valor Sueldo Indemnizacion] + [Promedio Indemnizacion]"
                    Dim ReplaceFormula As String = "[ " + SalaryIndemnization.ToString + " ]  +  [ " + AverageSalary.ToString + " ] "

                    Dim detailIndemnizacion As New ContractLiquidationDetail() With {.IdConcept = AuthorizationConcept.Id, .ConceptType = 1, .Description = AuthorizationConcept.Name, .InitialDate = liqMinDate, .EndingDate = retirementDate, .ReplaceConceptFormulate = VarIndemnizacion, .ConceptFormulate = AuthorizationConcept.Formulates}
                    detailIndemnizacion.Accrued = VarIndemnizacion
                    detailIndemnizacion.Deducted = 0
                    contractLiquidation.TotalAccrued += VarIndemnizacion
                    contractLiquidation.ContractLiquidationDetail.Add(detailIndemnizacion)
                End If
            End If

            'Retención en la Fuente por Indemnización
            If VarIndemnizacion > 0 Then
                Dim ObjContractLiquidationDetailIndemnization = Me.CalculateRetentionIndemnization(validContract, VarIndemnizacion, retirementDate)

                If ObjContractLiquidationDetailIndemnization.StateResult = True And ObjContractLiquidationDetailIndemnization.ObjectEmbbeded IsNot Nothing Then
                    contractLiquidation.ContractLiquidationDetail.Add(ObjContractLiquidationDetailIndemnization.ObjectEmbbeded)
                ElseIf ObjContractLiquidationDetailIndemnization.StateResult = False Then
                    result.Message = ObjContractLiquidationDetailIndemnization.Message.ToString
                    result.StateResult = False
                End If

            End If

            Dim TotalAccrued = contractLiquidation.ContractLiquidationDetail.Sum(Function(x) x.Accrued And x.ConceptType = 1)
            Dim TotalDeducted = contractLiquidation.ContractLiquidationDetail.Sum(Function(x) x.Deducted And x.ConceptType = 2)

            contractLiquidation.TotalAccrued = TotalAccrued
            contractLiquidation.TotalDeducted = TotalDeducted

            contractLiquidation.TotalPaid = contractLiquidation.TotalAccrued - contractLiquidation.TotalDeducted

            result.ObjectEmbbeded = contractLiquidation

        Catch ex As Exception
            result.Message = ex.Message.ToString
            result.StateResult = False
        End Try

        Return result

    End Function

    ''' <summary>
    ''' Liquida las nominas
    ''' </summary>
    ''' <param name="validContract"></param>
    ''' <param name="initialDate"></param>
    ''' <param name="endingDate"></param>
    ''' <remarks></remarks>
    Private Function LiquidatePayroll(validContract As Domain.Payroll.Entities.Employee, initialDate As Date, endingDate As Date, ByVal session As SessionValues) As Liquidation

        Dim result As Liquidation

        Dim Contract = validContract.Contract.Where(Function(x) x.Valid = True).SingleOrDefault()

        Dim PayrollStarDate = Contract.Group.NextDateLiquidation 'groupEmployee.NextDateLiquidation
        Dim PayrollLiquidation = Contract.Group.Liquidation

        Dim groupId = Contract.GroupId.ToString

        Dim employeeNit = Contract.Employee.ThirdParty.Nit

        'Averiguo la Fecha Fin de la Nómina
        Dim PayrollEndDate = _liquidationDomain.GetEndPayrollDate(PayrollLiquidation, PayrollStarDate)


        Dim employees As New List(Of Domain.Payroll.Entities.Employee)
        employees.Add(validContract)

        Dim PayrollLiquidationDelete = _liquidationRepository.SP_DeleteLiquidationNoConfirm(PayrollEndDate, groupId, employeeNit)
        Dim Liquidation = _liquidationDomain.NewExecuteLiquitadion(employees, Contract.Group, False, session, endingDate)
        If Liquidation.ObjectEmbbeded IsNot Nothing Then
            result = Liquidation.ObjectEmbbeded.FirstOrDefault
        Else
            result = Nothing
        End If

        Return result

    End Function

    ''' <summary>
    ''' Metodo que liquida las cesantias del empleado
    ''' </summary>
    ''' <param name="employee">Empleado a liquidar</param>
    Private Sub LiquidateUnemployed(employee As Domain.Payroll.Entities.Employee, initialDate As Date, endingDate As Date, ByRef unemployeedTotalPaid As Decimal, ByRef unemployeedTotalInterest As Decimal, ByRef SanctionDays As Integer, ByRef ReplaceFormulate As String, ByRef ConceptFormulate As String, ByRef InitialDateUnemployement As Date, Optional xtraLiquidation As Liquidation = Nothing, Optional BasePrimasCesantias As Decimal = 0)

        'Se crea una lista de empleado para poder enviar al servicio de liquidacion de cesantias
        Dim employeeList As New List(Of Domain.Payroll.Entities.Employee)
        employeeList.Add(employee)

        'Contruir Fecha Inicial de Periodo de Cesantias
        Dim InitialDateUnemployeed As Date = New Date(Year(endingDate), 1, 1)
        Dim InitialDateUnemployedPeriod As Date

        'Si la Fecha de Contratación del Empleado es mayor que la Fecha Inicio del Periodo de Cesantias

        If initialDate > InitialDateUnemployeed Then
            InitialDateUnemployedPeriod = initialDate
        Else
            InitialDateUnemployedPeriod = InitialDateUnemployeed
        End If

        InitialDateUnemployement = InitialDateUnemployedPeriod

        Dim alreadyLiquidated = _unemployedLiquidationRepository.GetUnemployedLiquidationEmployeeByDate(employee.Id, InitialDateUnemployedPeriod, endingDate)

        If alreadyLiquidated.Count > 0 Then 'Ya tiene cesantias liquidadas

            Dim unemployedAlreadyPaidToSave = alreadyLiquidated.Where(Function(i) i.UnemployedPayDate IsNot Nothing).ToList() 'cesantias ya pagas, no se pueden eliminar

            For Each item As UnemployedLiquidation In unemployedAlreadyPaidToSave
                alreadyLiquidated.Remove(item) 'Se eliminan del listado de cesantias liquidadas
            Next

            _unemployedLiquidationDomain.DeleteListUnemployedLiquidationsWithoutConfirm(alreadyLiquidated) 'Se eliminan las cesantias restantes


            If unemployedAlreadyPaidToSave.Count > 0 Then 'Si tiene cesantias liquidadas y pagas se toma el siguiente periodo como fecha de inicio
                Dim latterPaid = unemployedAlreadyPaidToSave.OrderByDescending(Function(i) i.UnemployedEndingDate).FirstOrDefault 'Se organiza x fecha y  se obtiene la ultima liquidacion paga
                Dim latterPaidEndingDate = CDate(latterPaid.UnemployedEndingDate).AddMonths(1)
                initialDate = New Date(latterPaidEndingDate.Year, latterPaidEndingDate.Month, 1)
            End If

        End If

        Dim ReplaceFormulates As String = ""
        Dim ConceptFormulates As String = ""
        Dim DateTest = New Date(1, 1, 1)

        Dim result = _unemployedLiquidationDomain.YearlyLiquidation(employeeList, InitialDateUnemployedPeriod, endingDate, False, True, False, DateTest, "", "", XtraLiquidation:=xtraLiquidation, liquidationContract:=True, ReplaceFormulate:=ReplaceFormulates, ConceptFormulate:=ConceptFormulates, BasePrimasCesantias:=BasePrimasCesantias)

        ReplaceFormulate = ReplaceFormulates
        ConceptFormulate = ConceptFormulates

        If result IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
            Dim liquidationUnemployee = result
            If liquidationUnemployee.StateResult = True Then
                For Each liquidated As UnemployedLiquidation In liquidationUnemployee.ObjectEmbbeded

                    SanctionDays = SanctionDays + liquidated.SanctionTotalDays


                    'Valor de cesantias ya pagado
                    unemployeedTotalPaid = liquidated.TotalUnemployed

                    'Valor de intereses cesantias ya pagado
                    unemployeedTotalInterest = liquidated.UnemployedInterestTotal


                Next

            End If
        End If

    End Sub

    ''' <summary>
    ''' Metodo para liquidar las primas del empleado
    ''' </summary>
    ''' <param name="validContract">Contrato valido</param>
    ''' <param name="retirementDate">Fecha de retiro</param>
    ''' <param name="xtraLiquidation">Ultima liquidacion</param>
    Private Sub LiquidateIncentives(validContract As Domain.Payroll.Entities.Contract, retirementDate As Date, xtraLiquidation As Liquidation, ByRef incentiveTotalPaid As Decimal, ByVal session As SessionValues, ByRef ReplaceFormulate As String, ByRef ConceptFormulate As String, ByRef IncentiveStarDate As Date, Optional ByVal BasePrimasCesantias As Decimal = 0)

        'Numero de primas al año segun parametro del grupo
        Dim numberOfIncentives As Integer = validContract.Group.PayrollParameter.MaxPremiumByYear
        'periodo de pago
        Dim period As Char = If(retirementDate.Month <= 6, "1", "2")
        'Fecha de inicio del periodo de pago de prima
        Dim dateNow As Date = Date.Now
        Dim startDate As Date
        If validContract.JobBondingDate.Year < dateNow.Year Then
            If numberOfIncentives > 2 Then
                numberOfIncentives = 2
            End If
            If numberOfIncentives = 0 Then
                Return
            End If
            If numberOfIncentives = 1 Then
                startDate = New Date(Year(validContract.Group.LastDateLiquidation), 1, 1)
            ElseIf numberOfIncentives = 2 Then
                If Month(validContract.Group.LastDateLiquidation) = 12 And Month(retirementDate) = 1 Then
                    startDate = If(retirementDate.Month <= 6, New Date(Year(validContract.Group.NextDateLiquidation), 1, 1), New Date(Year(validContract.Group.NextDateLiquidation), 7, 1))
                Else
                    startDate = If(retirementDate.Month <= 6, New Date(Year(validContract.Group.LastDateLiquidation), 1, 1), New Date(Year(validContract.Group.LastDateLiquidation), 7, 1))
                End If

            End If
        Else
            If numberOfIncentives = 1 Then
                startDate = New Date(validContract.JobBondingDate.Year, validContract.JobBondingDate.Month, validContract.JobBondingDate.Day)
            ElseIf numberOfIncentives = 2 Then
                If (retirementDate.Month <= 6) Then
                    startDate = New Date(validContract.JobBondingDate.Year, validContract.JobBondingDate.Month, validContract.JobBondingDate.Day)
                ElseIf validContract.JobBondingDate.Month <= 6 Then
                    startDate = New Date(validContract.JobBondingDate.Year, 7, 1)
                Else
                    startDate = New Date(validContract.JobBondingDate.Year, validContract.JobBondingDate.Month, validContract.JobBondingDate.Day)
                End If
            End If
        End If

        If startDate < validContract.JobBondingDate Then
            startDate = validContract.JobBondingDate
        End If

        IncentiveStarDate = startDate

        'Dim incentives = _incentivePaymentDomain.IncentivePaymentCalculate(Nothing, numberOfIncentives, Nothing, period, startDate, retirementDate, Nothing, validContract, xtraLiquidation)
        Dim incentives = _incentivePaymentDomain.IncentivePaymentCalculate(validContract.Group, "1", period, startDate, retirementDate, session, validContract, xtraLiquidation, True, ReplaceFormulate, ConceptFormulate, BasePrimasCesantias)

        Dim ListIncentive = _incentivePaymentRepository.GetIncentivePaymentByEmployeeIdRetefuente(validContract.EmployeeId, retirementDate.Year)

        If ListIncentive IsNot Nothing AndAlso ListIncentive.Count > 0 Then
            If ListIncentive.Any(Function(x) x.Period = period And x.RegisterStatus = 2) Then
                incentives.StateResult = False
            End If
        End If

        If incentives.StateResult = True Then
            Dim incentive = incentives.ObjectEmbbeded.FirstOrDefault

            incentiveTotalPaid = incentive.PaidValue
        Else
            incentiveTotalPaid = 0
        End If

    End Sub


    ''' <summary>
    ''' Metodo que liquida las vacaciones pendientes del empleado
    ''' </summary>
    ''' <param name="employee">Empleado a liquidar</param>
    ''' <param name="validContract">Contrato Valido</param>
    ''' <param name="jobBondingDate">Fecha de vinculacion</param>
    ''' <param name="retirementDate">Fecha de retiro</param>
    ''' <param name="vacationTotalPaid">Valor a pagar</param>
    ''' <remarks></remarks>
    Private Sub LiquidateVacations(employee As Domain.Payroll.Entities.Employee, validContract As Domain.Payroll.Entities.Contract, jobBondingDate As Date, retirementDate As Date, ByRef vacationTotalPaid As Decimal, xtraLiquidation As Liquidation, ByRef ReaplaceFormulate As String, ByRef ConceptFormulate As String, Optional ByVal BasePrimasCesantias As Decimal = 0)
        'Dim overNightAverage As Decimal = 0D
        'Dim periods As Integer = 0
        'Dim valPeriod As Integer = 0
        'Dim vacationsValueToPaid As Decimal = 0D
        'Dim vacationsValueToRefund As Decimal = 0D
        '------ Parametros
        Dim groupParameter As PayrollParameter = validContract.Group.PayrollParameter

        If Not groupParameter.LiquidateVacation Then
            vacationTotalPaid = 0
            Return
        End If

        Dim groupAdvanceVacation As Boolean = groupParameter.VacationAdvanced
        Dim groupMaxVacationsByYear As Integer = groupParameter.MaxVacationByYear
        Dim daysvacation As Integer = groupParameter.VacationDays
        Dim dateInitialTmp As Date = validContract.JobBondingDate
        Dim countYear As Integer = DateDiff(DateInterval.Year, dateInitialTmp, retirementDate)
        dateInitialTmp = dateInitialTmp.AddYears(countYear)
        Dim countDays As Integer = DateDiff(DateInterval.Day, dateInitialTmp, retirementDate.AddDays(1))
        Dim currentSalary As Decimal = validContract.BasicSalary
        Dim valueDay As Decimal = currentSalary / 360
        Dim daysForAPeriod As Decimal = 360 / groupMaxVacationsByYear
        'Validacion que permite identificar si tiene periodos con dias pendiente
        Dim ValidateUnfinishedVacation As Boolean = False
        Dim listVacationPeriodEmployee = _vacationPeriodRepository.GetVacationPeriodWithDetailByEmployee(employee.Id)
        Dim VacationLastDateLiquidation As Date = employee.VacationLastDateLiquidation

        'Bandera que permite identificar si el empleado tiene dias de vacaciones pediente donde ya haya disfrutado alguno
        ValidateUnfinishedVacation = IIf((From t In listVacationPeriodEmployee Where t.PendingDays > 0 And t.TakenDays > 0 Select t).Count > 0, True, False)

        'Se calcula el total de dias trabajados hasta la fecha de retiro desde el ultimo periodo tomado completo de vacaciones
        Dim daysWorkEmployee As Decimal = _liquidationDomain.Days360(employee.VacationLastDateLiquidation, retirementDate)

        If ValidateUnfinishedVacation Then
            'recalcula la variable VacationLastDateLiquidation 
            If groupMaxVacationsByYear = 1 Then
                employee.VacationLastDateLiquidation = employee.VacationLastDateLiquidation.AddYears(1)
            Else
                employee.VacationLastDateLiquidation = employee.VacationLastDateLiquidation.AddMonths(6)
            End If
        End If
        'Se calcula el total de dias trabajados hasta la fecha de retiro desde el ultimo periodo de vacaciones recalculado
        Dim tmpDaysWorkEmployee As Decimal = _liquidationDomain.Days360(employee.VacationLastDateLiquidation, retirementDate)

        If employee.VacationLastDateLiquidation < retirementDate Then
            daysWorkEmployee = daysWorkEmployee
        Else
            daysWorkEmployee = 0
        End If

        If retirementDate.Month = 2 AndAlso validContract.Group.Month = 2 Then
            If retirementDate.Day = 28 Then
                daysWorkEmployee = daysWorkEmployee - 2
            End If

            If retirementDate.Day = 29 Then
                daysWorkEmployee = daysWorkEmployee - 1
            End If
        End If

        If employee.VacationLastDateLiquidation = retirementDate Then
            daysWorkEmployee = 1
        End If
        Dim contractInitial = _contractRepository.GetContractById(validContract.InitialContractNumber)
        Dim DateInitialContract = contractInitial.ContractInitialDate
        'Listo todas las Licencias No Remuneradas y las Sanciones
        Dim UnpaidLicensesNovelty = _noveltyRepository.GetNoveltyUnpaidLicenses(employee.Id)
        Dim VarUnpaidLicensesDays As Integer = 0
        If UnpaidLicensesNovelty IsNot Nothing And UnpaidLicensesNovelty.Count > 0 Then
            'Busco las licencias en el periodo desde el contrato base a la fecha de retiro
            Dim ListUnpaidLicensesNovelty = UnpaidLicensesNovelty.Where(Function(x) x.RealDate >= DateInitialContract And x.RealDate <= retirementDate AndAlso x.Status <> 1).ToList()
            If ListUnpaidLicensesNovelty IsNot Nothing OrElse ListUnpaidLicensesNovelty.Count > 0 Then
                VarUnpaidLicensesDays = ListUnpaidLicensesNovelty.Sum(Function(x) x.Days)
            End If
        End If

        Dim SanctionNovelty = _noveltyRepository.GetNoveltySanctions(employee.Id)
        Dim SanctionDays As Integer = 0
        If SanctionNovelty IsNot Nothing And SanctionNovelty.Count > 0 Then
            'Busco las sanciones del empleado
            Dim ListSanctionNovelty = SanctionNovelty.Where(Function(x) x.RealDate >= DateInitialContract And x.RealDate <= retirementDate).ToList()
            If ListSanctionNovelty IsNot Nothing OrElse ListSanctionNovelty.Count > 0 Then
                SanctionDays = ListSanctionNovelty.Sum(Function(x) x.Days)
            End If
        End If

        Dim daysVacationEmployee As Decimal = 0
        Dim daysPayment As Integer = 0
        Dim PendingDays As Decimal = 0

        If listVacationPeriodEmployee IsNot Nothing And listVacationPeriodEmployee.Count > 0 Then
            For Each itemVacationPeriod In listVacationPeriodEmployee
                If itemVacationPeriod.InitialDatePeriod > retirementDate And listVacationPeriodEmployee.Count = 1 Then
                    PendingDays = 0
                    Continue For
                End If

                If itemVacationPeriod.InitialDatePeriod > retirementDate Then
                    Continue For
                Else
                    If itemVacationPeriod.Vacation IsNot Nothing AndAlso itemVacationPeriod.Vacation.Count > 0 Then

                        'Valida si el empleado lleva menos de un año en la empresa
                        If _liquidationDomain.Days360(validContract.JobBondingDate, retirementDate) < 360 Then
                            'Valida si el grupo permite vacaciones adelantadas
                            If groupAdvanceVacation Then
                                employee.VacationLastDateLiquidation = employee.VacationLastDateLiquidation.AddYears(-1)
                                Dim TmpDaysWorkedEmployee As Decimal = _liquidationDomain.Days360(employee.VacationLastDateLiquidation, retirementDate)
                                PendingDays = ((TmpDaysWorkedEmployee * daysvacation) / daysForAPeriod) - itemVacationPeriod.TakenDays
                                daysPayment = daysPayment + itemVacationPeriod.TakenDays
                                If PendingDays > 0 AndAlso daysPayment > 0 Then
                                    daysWorkEmployee = TmpDaysWorkedEmployee
                                End If
                            End If
                        Else
                            If itemVacationPeriod.Vacation.Where(Function(x) x.State = 2).Count > 0 AndAlso ((itemVacationPeriod.PendingDays > 0 And itemVacationPeriod.TakenDays > 0) Or itemVacationPeriod.EndDatePeriod < retirementDate) Then
                                PendingDays = PendingDays + itemVacationPeriod.PendingDays
                                If PendingDays > 0 Then
                                    daysPayment = daysPayment + itemVacationPeriod.TakenDays
                                End If
                            End If
                        End If
                    End If
                End If
                If itemVacationPeriod.EndDatePeriod < retirementDate AndAlso itemVacationPeriod.PendingDays = 0 Then
                    PendingDays = 0
                End If
            Next
        End If

        daysPayment = (daysPayment * daysForAPeriod) / daysvacation

        Dim PendingDaysWorkEmployee As Decimal = daysWorkEmployee - daysPayment

        daysWorkEmployee = PendingDaysWorkEmployee

        daysVacationEmployee = (daysvacation * daysWorkEmployee) / daysForAPeriod

        If listVacationPeriodEmployee.Count = 0 Then
            PendingDays = daysVacationEmployee
        End If

        vacationTotalPaid = daysWorkEmployee * valueDay
        If groupMaxVacationsByYear = 1 Then
            vacationTotalPaid = vacationTotalPaid / 2
        End If
        Dim InitialDateVacationSearch As Date = DateAdd(DateInterval.Month, -5, retirementDate)

        Dim DateSearch As Date

        If DateDiff(DateInterval.Month, VacationLastDateLiquidation, retirementDate) > 12 Then
            DateSearch = (New Date(Year(retirementDate) - 1, Month(retirementDate), 1))
            xtraLiquidation = Nothing
        Else
            DateSearch = VacationLastDateLiquidation
        End If

        Dim SearchEndDate As Date
        ' Calcular el último día del mes de la fecha de retiro
        SearchEndDate = New Date(retirementDate.Year, retirementDate.Month, Date.DaysInMonth(retirementDate.Year, retirementDate.Month))

        Dim AverageIBCVacationValue As Double = 0

        Dim VacationsValues = _contractLiquidationRepository.LiquidationEmployeeByDate(employee.Id, DateSearch, SearchEndDate)
        If xtraLiquidation IsNot Nothing AndAlso VacationsValues.Any(Function(x) x.PayrollDateLiquidated = xtraLiquidation.PayrollDateLiquidated) = False Then
            If VacationsValues.Count < 12 Then
                VacationsValues.Add(xtraLiquidation)
            End If
        End If


        Dim LiquidationDetail As New List(Of LiquidationDetail)


        Dim yearLiquidated As Boolean = True

        If VacationsValues IsNot Nothing And VacationsValues.Count > 0 Then
            For j As Integer = 0 To (VacationsValues.Count() - 1)
                LiquidationDetail = VacationsValues(j).LiquidationDetail.Where(Function(x) x.ConceptClass <> "005" AndAlso x.ConceptClass <> "006" AndAlso x.Concept.AffectIBCVacation = True).ToList()
                If LiquidationDetail IsNot Nothing AndAlso LiquidationDetail.Count() > 0 Then
                    For k As Integer = 0 To LiquidationDetail.Count() - 1
                        'If LiquidationDetail.Item(k).Concept.AffectIBCIncentivePayment = True Then

                        If LiquidationDetail.Item(k).ConceptType = 1 Then
                            AverageIBCVacationValue = AverageIBCVacationValue + LiquidationDetail.Item(k).ConceptTotalValue
                        End If

                        If LiquidationDetail.Item(k).ConceptType = 2 Then
                            AverageIBCVacationValue = AverageIBCVacationValue - LiquidationDetail.Item(k).ConceptTotalValue
                        End If

                        ' End If

                        If Month(InitialDateVacationSearch) = Month(LiquidationDetail.Item(k).PayrollDate) And Year(InitialDateVacationSearch) = Year(LiquidationDetail.Item(k).PayrollDate) Then
                            yearLiquidated = True
                        Else
                            yearLiquidated = False
                        End If
                    Next
                End If

            Next
        End If

        Dim PromedioVacaciones As Double = 0
        Dim DaysPromedy As Integer = 0


        If yearLiquidated = False Then
            Dim InitialDate As Date = New Date(Year(retirementDate), 1, 1)

            If jobBondingDate > InitialDate Then
                DaysPromedy = _liquidationDomain.Days360(jobBondingDate, retirementDate)
            ElseIf employee.VacationLastDateLiquidation > InitialDate Then
                DaysPromedy = _liquidationDomain.Days360(DateSearch, retirementDate)
            Else
                DaysPromedy = _liquidationDomain.Days360(DateSearch, retirementDate)
            End If

            If DaysPromedy > 360 Then
                DaysPromedy = 360
            End If

        Else
            DaysPromedy = daysWorkEmployee
        End If


        If daysWorkEmployee > 30 Then
            PromedioVacaciones = (AverageIBCVacationValue / DaysPromedy) * 30
        Else
            PromedioVacaciones = AverageIBCVacationValue
        End If

        If groupMaxVacationsByYear = 1 Then

            vacationTotalPaid = (currentSalary + PromedioVacaciones) / 720 * (daysWorkEmployee - VarUnpaidLicensesDays - SanctionDays)
            ConceptFormulate = "([Salario Básico] + [Salario Variable Vacaciones]) / 720 * ([Días Vacaciones] - [Dias Lic. No Remuneradas] - [Dias sancion])"
            ReaplaceFormulate = "(" + currentSalary.ToString + " + " + PromedioVacaciones.ToString() + ") / 720 * (" + daysWorkEmployee.ToString() + " - " + VarUnpaidLicensesDays.ToString() + " - " + SanctionDays.ToString() + ")"
        Else
            vacationTotalPaid = (currentSalary + PromedioVacaciones) / 360 * (daysWorkEmployee - VarUnpaidLicensesDays - SanctionDays)
            ConceptFormulate = "([Salario Básico] + [Salario Variable Vacaciones]) / 360 * ([Días Vacaciones] - [Dias Lic. No Remuneradas] - [Dias sancion])"
            ReaplaceFormulate = "(" + currentSalary.ToString + " + " + PromedioVacaciones.ToString() + ") / 360 * (" + daysWorkEmployee.ToString() + " - " + VarUnpaidLicensesDays.ToString() + " - " + SanctionDays.ToString() + ")"
        End If

        If vacationTotalPaid < 0 Then
            vacationTotalPaid = 0
            Return
        End If

    End Sub

    ''' <summary>
    ''' Metodo que obtiene la fecha minima de retiro basado en ultima fecha de liquidacion de nomina confirmada 
    ''' </summary>
    ''' <param name="contractId">Id del contrato actual</param>
    ''' <returns>La fecha minima de retiro</returns>
    Private Function GetMinimumRetirementDate(contractId As Integer) As Date

        Dim payrollsPaid = _contractLiquidationRepository.GetPaymentsByContractId(contractId)

        If payrollsPaid.Count > 0 Then

            Dim lastPayrollPaid As Liquidation = payrollsPaid.OrderByDescending(Function(i) i.PayrollDateLiquidated).FirstOrDefault

            Return lastPayrollPaid.PayrollDateLiquidated
        Else
            Return Nothing
        End If

    End Function

    Private Function RetentionValue(ByVal contract As Domain.Payroll.Entities.Contract, BaseRTF As Double, PensionValue As Double, SolidarityFund As Double, HealthValue As Double, Group As Group, RetirementDate As Date, VoluntaryHealhValue As Double, PayrollSettings As PayrollSettings, ListLiquidationLastYear As List(Of Liquidation), ByVal UnemployementValue As Double, Optional FlagRetention As Boolean = False, Optional ByRef PercentageRetentionFraction As Decimal = 0) As Double
        'Retenciones

        Dim VarRetentionValue As Double = 0

        Dim ListRetention = _liquidationDomain.Retention(BaseRTF, PensionValue, 0, SolidarityFund, 0, 0, HealthValue, VoluntaryHealhValue, 0, contract.Employee.HousingDeductionValue, Group.PayrollParameter.UVTValue, Group.PayrollParameter.RTFExemptPercentage, Group.PayrollParameter.LegalSalaryMinimum, contract.Employee.ProcedureTypeRTF, contract, contract.Employee, RetirementDate, 0, PayrollSettings, ListLiquidationLastYear, UnemployementValue, False, FlagRetention, PercentageRetentionFraction)

        For Each ObjTuple As Tuple(Of String, Double) In ListRetention

            If ObjTuple.Item1 = "Retenciones" Then
                VarRetentionValue = ObjTuple.Item2
            End If

        Next

        Return VarRetentionValue

    End Function


    Public Function InabilitiesPeriodDays(ListEmployeeNovelty As List(Of Novelty), InitialDateIncentivePayment As Date, EndDateIncentivePayment As Date) As Integer


        Dim DaysInabilities As Integer = 0
        For Each ObjEmployeeNovelty As Novelty In ListEmployeeNovelty

            If ObjEmployeeNovelty.RealDate >= InitialDateIncentivePayment And ObjEmployeeNovelty.EndDate <= EndDateIncentivePayment Then
                'La Novedad está en el Periodo de Primas
                DaysInabilities = DaysInabilities + ObjEmployeeNovelty.Days
            End If

            If ObjEmployeeNovelty.RealDate >= InitialDateIncentivePayment And ObjEmployeeNovelty.EndDate > EndDateIncentivePayment Then
                'Si la Novedad inicia en el Periodo de las Primas y terminan después que finalicen las Primas
                DaysInabilities = DaysInabilities + DateDiff(DateInterval.Day, ObjEmployeeNovelty.RealDate, EndDateIncentivePayment) + 1
            End If

            If ObjEmployeeNovelty.RealDate < InitialDateIncentivePayment And ObjEmployeeNovelty.EndDate <= EndDateIncentivePayment Then
                'Si la Novedad antes del Periodo de las Primas y terminan antes que finalicen las Primas
                DaysInabilities = DaysInabilities + DateDiff(DateInterval.Day, InitialDateIncentivePayment, ObjEmployeeNovelty.EndDate) + 1
            End If

            If ObjEmployeeNovelty.EndDate < InitialDateIncentivePayment Then
                'Si la Novedad inicia antes de que inice el Periodo de Primas, no se tiene en cuenta
                DaysInabilities = 0
            End If

            If ObjEmployeeNovelty.RealDate > EndDateIncentivePayment Then
                DaysInabilities = 0
            End If

        Next

        Return DaysInabilities

    End Function

    Public Function FormulatesData(ByVal validContract As Domain.Payroll.Entities.Contract, retirementDate As Date, ByRef RepresentationCost As Double, ByRef BonificationYearValue As Double, ByRef PaidValueAverageIncentiveServicesValue As Double, ByRef PaidValueIncentivePaymentVacation As Double)
        Dim ListPositionEmployee = _PositionRepository.ListAllPosition()

        'Busco si el Empleado se le paga Gastos de Representación
        Dim ObjPositionEmployee As Domain.Payroll.Entities.Position

        If ListPositionEmployee IsNot Nothing And ListPositionEmployee.Count > 0 Then
            ObjPositionEmployee = ListPositionEmployee.Where(Function(x) x.Id = validContract.PositionId).FirstOrDefault()
        End If

        If ObjPositionEmployee IsNot Nothing Then
            If ObjPositionEmployee.RepresentationCost = True Then
                RepresentationCost = validContract.BasicSalary * 0.135
            End If
        End If


        Dim ObjRetroactiveEmployee As RetroactiveC
        Dim ListRetroactive = _retroactiveRepository.GetListRetroactiveByYear(Year(retirementDate) - 1, validContract.GroupId)

        If ListRetroactive IsNot Nothing AndAlso ListRetroactive.Count() > 0 Then
            ObjRetroactiveEmployee = ListRetroactive.Where(Function(x) x.IdEmployee = validContract.EmployeeId).FirstOrDefault()
        End If

        Dim datePeriod As Date = New Date(validContract.Group.NextDateLiquidation.Year - 1, validContract.Group.NextDateLiquidation.Month, 1)

        Dim ObjConceptClassBonificationValue = _payrollLiquidationRepository.GetLastConceptClassListDate(IIf(validContract.InitialContractNumber = 0, validContract.Id, validContract.InitialContractNumber), "046", datePeriod)
        If ObjConceptClassBonificationValue.Count > 0 Then
            Dim BonificationConceptId As Integer
            For Each item In ObjConceptClassBonificationValue
                BonificationYearValue += item.ConceptTotalValue
                BonificationConceptId = item.ConceptId
            Next
            'If Year(Date.Now) > Year(ObjConceptClassBonificationValue.PayrollDate) Then

            If ObjRetroactiveEmployee IsNot Nothing Then
                Dim ObjConceptRetroactive = ObjRetroactiveEmployee.RetroactiveD.Where(Function(x) x.IdConcept = BonificationConceptId).FirstOrDefault()
                If ObjConceptRetroactive IsNot Nothing Then
                    BonificationYearValue = ObjConceptRetroactive.ValueConceptWithRetroactive + BonificationYearValue
                End If
            End If

            'End If
        End If

        Dim ListPaidValueAverageIncentiveServices = _incentivePaymentRepository.GetIncentivePaymentByEmployeeIdLastLiquidation(validContract.EmployeeId)
        Dim ObjPaidValueAverageIncentiveServices = ListPaidValueAverageIncentiveServices.Where(Function(x) x.Period = 1 And x.PeriodEndDate >= datePeriod).FirstOrDefault()

        If ObjPaidValueAverageIncentiveServices IsNot Nothing Then
            PaidValueAverageIncentiveServicesValue = ObjPaidValueAverageIncentiveServices.TotalAccrued

            If ObjRetroactiveEmployee IsNot Nothing Then

                'Si toca buscar el retroactivo, busco el dato del Concepto
                Dim ListStringConceptClass As New List(Of String)
                ListStringConceptClass.Add("002")
                Dim Concept = _conceptRepository.GetConceptByConceptClass(ListStringConceptClass).FirstOrDefault()

                If Concept IsNot Nothing Then
                    Dim ObjConceptRetroactive = ObjRetroactiveEmployee.RetroactiveD.Where(Function(x) x.IdConcept = Concept.Id).FirstOrDefault()
                    If ObjConceptRetroactive IsNot Nothing Then
                        PaidValueAverageIncentiveServicesValue = ObjConceptRetroactive.ValueConceptWithRetroactive + PaidValueAverageIncentiveServicesValue
                    End If
                End If
            End If

        End If

        Dim ObjConceptVacationIncentiveValue = _payrollLiquidationRepository.GetLastConceptByCode(IIf(validContract.InitialContractNumber = 0, validContract.Id, validContract.InitialContractNumber), "085")
        If ObjConceptVacationIncentiveValue IsNot Nothing Then
            PaidValueIncentivePaymentVacation = ObjConceptVacationIncentiveValue.ConceptTotalValue
        End If

    End Function

    Public Function TransportHealthValue(ByVal validContract As Domain.Payroll.Entities.Contract, WorkedPeriodDays As Integer, OriginalFormulate As String, Optional ByRef ReplaceFormulate As String = "") As Double


        Dim ReturnTransportValue As Double = 0
        Dim ObjTransportHealthValue = validContract.Group.PayrollParameter.TransportHelpValue
        Dim LegalSalaryMinimum = validContract.Group.PayrollParameter.LegalSalaryMinimum

        If validContract.BasicSalary > (2 * LegalSalaryMinimum) Then
            ObjTransportHealthValue = 0
        End If

        'ReturnTransportValue = ReplaceDataFormulates(OriginalFormulate, validContract.BasicSalary, 30, 15, LegalSalaryMinimum, 0, 0, ObjTransportHealthValue, 0, 0, 0, 0, 0, 0, validContract.HoursDaily, WorkedPeriodDays, 0, 0, 0, 0, 0, ReplaceFormulate)


        Return ReturnTransportValue

    End Function

    Public Function FoodBonificatioValue(ByVal validContract As Domain.Payroll.Entities.Contract, WorkedPeriodDays As Integer, OriginalFormulate As String, Optional ByRef ReplaceFormulate As String = "") As Double
        Dim FoodValue As Double = 0

        Dim LegalSalaryMinimum = validContract.Group.PayrollParameter.LegalSalaryMinimum
        'FoodValue = ReplaceDataFormulates(OriginalFormulate, validContract.BasicSalary, 30, 15, LegalSalaryMinimum, 0, 0, validContract.Group.PayrollParameter.TransportHelpValue, 0, 0, 0, 0, 0, 0, validContract.HoursDaily, WorkedPeriodDays, 0, 0, 0, 0, 0, ReplaceFormulate)

        Return FoodValue

    End Function

    Public Function EndDateVacation(retirementDate As Date, requestDays As Integer, PayrollParameter As PayrollParameter) As Date

        Dim incorporationDate As Date
        Dim EndDate As Date
        Dim index As Integer = 1

        Dim holidays As List(Of Domain.Entities.Holiday) = _holidayRepository.ListHolidayBetweenDate(retirementDate, retirementDate.AddMonths(6))

        If retirementDate.DayOfWeek = DayOfWeek.Friday Then
            retirementDate = retirementDate.AddDays(3)
        ElseIf retirementDate.DayOfWeek = DayOfWeek.Saturday Then
            retirementDate = retirementDate.AddDays(2)
        ElseIf retirementDate.DayOfWeek = DayOfWeek.Sunday Then
            retirementDate = retirementDate.AddDays(1)
        End If


        EndDate = retirementDate.AddDays(-1)

        While index <= requestDays 'Calculo la fecha final de vacaciones
            EndDate = EndDate.AddDays(1)

            If _vacationPeriodDomain.IsValidDay(holidays, False, False, EndDate) Then ' Si no es un festivo ni un domingo aumenta el indice
                index += 1
            End If

        End While

        incorporationDate = EndDate.AddDays(1)

        While _vacationPeriodDomain.IsValidDay(holidays, False, False, incorporationDate) = False 'Calculo la fecha de incorporacion
            incorporationDate = incorporationDate.AddDays(1)
        End While

        Return incorporationDate

    End Function

    Public Function ValidateHolidayDate(SendDate As Date) As Date


        Dim EndDate As Date
        Dim index As Integer = 1

        Dim holidays As List(Of Domain.Entities.Holiday) = _holidayRepository.ListHolidayBetweenDate(SendDate, SendDate.AddMonths(6))

        'If retirementDate.DayOfWeek = DayOfWeek.Friday Then
        '    retirementDate = retirementDate.AddDays(3)
        'ElseIf retirementDate.DayOfWeek = DayOfWeek.Saturday Then
        '    retirementDate = retirementDate.AddDays(2)
        'ElseIf retirementDate.DayOfWeek = DayOfWeek.Sunday Then
        '    retirementDate = retirementDate.AddDays(1)
        'End If

        While index <= 4 'Calculo la fecha final de vacaciones
            SendDate = SendDate.AddDays(1)

            If _vacationPeriodDomain.IsValidDay(holidays, False, False, SendDate) Then ' Si no es un festivo ni un domingo aumenta el indice
                index += 1
                Exit While
            End If

        End While

        EndDate = SendDate

        While _vacationPeriodDomain.IsValidDay(holidays, False, False, SendDate) = False 'Calculo la fecha de incorporacion
            EndDate = EndDate.AddDays(1)
        End While

        Return EndDate

    End Function

    Public Function GetEndMonthDate(payrollStarDate As Date) As Date

        Dim PayrollEndDate As Date

        PayrollEndDate = DateAdd(DateInterval.Month, 1, payrollStarDate)
        PayrollEndDate = DateAdd(DateInterval.Day, -1, PayrollEndDate)

        Return PayrollEndDate

    End Function

    Public Function ExecuteContractLiquidation(employee As Entities.Employee, retirementDate As Date, retirementReasonId As Integer, ByVal session As SessionValues) As ActionMessageResult(Of Entities.ContractLiquidation) Implements IContractLiquidationDomain.ExecuteContractLiquidation
        Dim result As New ActionMessageResult(Of ContractLiquidation)()
        result.StateResult = True

        Try

            Dim ObjPayrollSettings = _payrollSettingsRepository.GetSettingPayroll()

            If ObjPayrollSettings Is Nothing Then
                result.StateResult = False
                result.Message = "No se encontraron Parámetros de Nómina"
                Return result
            End If

            'contrato vigente
            Dim validContract As Domain.Payroll.Entities.Contract = employee.Contract.Where(Function(i) i.Valid = True).FirstOrDefault

            Dim BasicSalary = validContract.BasicSalary
            Dim TransportHealthValue = validContract.Group.PayrollParameter.TransportHelpValue
            Dim LegalSalaryMinimun = validContract.Group.PayrollParameter.LegalSalaryMinimum

            'Valido Auxilio de Tranporte
            If BasicSalary > (2 * LegalSalaryMinimun) Then
                TransportHealthValue = 0
            End If

            '--------------------------------------------------------------------- Liquidacion de Nomina
            'Fecha minima de liquidacion de nomina pendiente - ajustada según tipo de nómina
            Dim liqMinDate As Date

            ' Calcular fecha inicial según tipo de nómina
            If validContract.Group.Liquidation = 2 AndAlso retirementDate.Day > 15 Then
                ' Nómina quincenal - segunda quincena
                liqMinDate = New Date(retirementDate.Year, retirementDate.Month, 16)
            Else
                ' Nómina mensual o quincenal primera quincena
                liqMinDate = New Date(retirementDate.Year, retirementDate.Month, 1)
            End If

            Dim ContractLiquidation As New ContractLiquidation()

            'Calcular el período correcto de liquidación según tipo de nómina y fecha de retiro
            Dim InitialRetirementDate As Date
            Dim EndRetirementDate As Date
            GetPayrollPeriodDates(retirementDate, validContract.Group.Liquidation, InitialRetirementDate, EndRetirementDate)

            Dim LiquidationPast = _payrollLiquidationRepository.LiquidationEmployeeByDate(employee.Id, InitialRetirementDate, EndRetirementDate)



            If LiquidationPast IsNot Nothing AndAlso LiquidationPast.Count > 0 Then
                'Significa que en el mes del retiro ya se le pagó la nómina. Debe deducirle el sueldo y el Auxilio de Tranporte (si tiene)

                Dim ObjGroup = _groupRepository.GetGroupById(validContract.Group.Id)

                Dim ListAdjustSalaryValue = AdjustBasicSalary(BasicSalary, retirementDate, InitialRetirementDate, EndRetirementDate, TransportHealthValue, LegalSalaryMinimun, ObjGroup)

                If ListAdjustSalaryValue.StateResult = True Then
                    If ListAdjustSalaryValue.ObjectEmbbeded IsNot Nothing AndAlso ListAdjustSalaryValue.ObjectEmbbeded.Count > 0 Then
                        For Each objContractDetail As ContractLiquidationDetail In ListAdjustSalaryValue.ObjectEmbbeded
                            ContractLiquidation.ContractLiquidationDetail.Add(objContractDetail)
                        Next
                    End If
                End If

            Else

                Dim xtraLiquidation As Liquidation = LiquidatePayroll(employee, liqMinDate, retirementDate, session)

                If xtraLiquidation IsNot Nothing Then
                    xtraLiquidation.Contract = validContract
                    xtraLiquidation.Group = validContract.Group
                    Dim messageError = From e In xtraLiquidation.Message
                                       Select e
                    If messageError.Count > 0 Then
                        For Each itemMessage As Payroll.Entities.Message In messageError.ToList()
                            If itemMessage.Error = True Then
                                If itemMessage.Description = "El TOTAL PAGADO está en Negativo. No se puede CONFIRMAR" Then
                                    result.StateResult = True
                                Else
                                    result.StateResult = False
                                End If

                            End If
                            result.MessageResult.Add(New MessageResult(itemMessage.Description, itemMessage.Error))
                        Next
                    End If
                    For Each detail As LiquidationDetail In xtraLiquidation.LiquidationDetail
                        Dim detailContract As New ContractLiquidationDetail() With {.IdConcept = detail.ConceptId, .Description = detail.ConceptDetail, .InitialDate = liqMinDate, .EndingDate = retirementDate, .ConceptFormulate = detail.ConceptFormulate, .ReplaceConceptFormulate = detail.ReplaceConceptFormulate}
                        If detail.ConceptType = "1" Then
                            detailContract.Accrued = detail.ConceptTotalValue
                        ElseIf detail.ConceptType = "2" Then

                            'If detail.ConceptClass = "017" Then
                            '    HealthValue = detail.ConceptTotalValue
                            'End If
                            'If detail.ConceptClass = "014" Then
                            '    PensionValue = detail.ConceptTotalValue
                            'End If
                            'If detail.ConceptClass = "038" Then
                            '    SolidarityFundValue = detail.ConceptTotalValue
                            'End If
                            'If detail.ConceptClass = "019" Then
                            '    VoluntaryHealhValue = detail.ConceptTotalValue
                            'End If

                            detailContract.Deducted = detail.ConceptTotalValue
                        Else
                            Continue For
                        End If
                        ContractLiquidation.ContractLiquidationDetail.Add(detailContract)
                    Next

                End If
            End If

            validContract.RetirementReasonId = retirementReasonId
            validContract.Status = 2
            validContract.RetirementDate = retirementDate

            ContractLiquidation.Employee = employee
            ContractLiquidation.Contract = validContract
            ContractLiquidation.RetirementDate = retirementDate
            ContractLiquidation.Status = "C"
            Dim retirementReason = _retirementReasonRepository.GetRetirementReasonById(retirementReasonId)
            ContractLiquidation.RetirementReason = retirementReason

            If retirementReason.Bonus Then
                'Calculamos las Primas
                Dim InventivePaymentValue = IncentivePaymentCalculation(validContract, validContract.Group, ObjPayrollSettings.ContractLiquidationServiceIncentive, retirementDate, BasicSalary, TransportHealthValue, LegalSalaryMinimun)
                ContractLiquidation.ContractLiquidationDetail.Add(InventivePaymentValue)
            End If

            If retirementReason.Vacations Then
                'Calculamos las Vacaciones
                Dim VacationValue = VacationCalculation(validContract, validContract.Group, ObjPayrollSettings.ContractLiquidationVacation, retirementDate, BasicSalary, TransportHealthValue, LegalSalaryMinimun)
                ContractLiquidation.ContractLiquidationDetail.Add(VacationValue)
            End If

            If retirementReason.Severance Then
                'Calculamos las Cesantias
                Dim UnemploymentValue = UnemployementPaymentCalculation(validContract, validContract.Group, ObjPayrollSettings.ContractLiquidationUnemployment, retirementDate, BasicSalary, TransportHealthValue, LegalSalaryMinimun)
                ContractLiquidation.ContractLiquidationDetail.Add(UnemploymentValue)

                If retirementReason.SeveranceInterest Then
                    'Calculamos los Intereses de Cesantias
                    Dim UnemploymentInteresetValue = UnemployementInterestPaymentCalculation(UnemploymentValue.Accrued, UnemploymentValue.InitialDate, retirementDate)
                    ContractLiquidation.ContractLiquidationDetail.Add(UnemploymentInteresetValue)
                End If
            End If

            ContractLiquidation.TotalAccrued = CDec(ContractLiquidation.ContractLiquidationDetail.Sum(Function(x) x.Accrued))
            ContractLiquidation.TotalDeducted = CDec(ContractLiquidation.ContractLiquidationDetail.Sum(Function(x) x.Deducted))
            ContractLiquidation.TotalPaid = CDec(ContractLiquidation.TotalAccrued - ContractLiquidation.TotalDeducted)

            result.ObjectEmbbeded = ContractLiquidation

        Catch ex As Exception
            result.StateResult = False
            result.Message = ex.Message
        End Try


        Return result

    End Function

    Public Function IncentivePaymentCalculation(Contract As Domain.Payroll.Entities.Contract, Group As Group, IncentivePaymentFormulate As String, retirementDate As Date, BasicSalary As Decimal, TransportHealthValue As Decimal, LegalSalaryMinimun As Decimal) As ContractLiquidationDetail

        Dim DetailIncentivePayment = New ContractLiquidationDetail()

        Dim IncentivePaymentValue As Double = 0

        Dim IncentiveMonth As Integer = 0
        Dim IncentiveDays As Integer = 0
        Dim SanctionDays As Integer = 0
        Dim UnpaidLicensesDays As Integer = 0
        Dim IncentiveVariableSalary As Decimal = 0

        Dim InitialMonth As Integer = 0

        If Month(retirementDate) < 6 Then
            InitialMonth = 1
        Else
            InitialMonth = 7
        End If

        Dim InitialDateIncentive As New Date(retirementDate.Year, InitialMonth, 1)

        'Si el empleado ingresó después de la fecha de Inicio de la Prima
        If Contract.JobBondingDate > InitialDateIncentive Then
            InitialDateIncentive = Contract.JobBondingDate
        End If

        'Cargamos las variables
        IncentiveMonth = DateDiff(DateInterval.Month, InitialDateIncentive, retirementDate)
        IncentiveDays = _liquidationDomain.Days360(InitialDateIncentive, retirementDate)

        Dim LiquidationEmployeeList = _payrollLiquidationRepository.LiquidationByDateIncentivePayment(Contract.InitialContractNumber, InitialDateIncentive, retirementDate)

        If LiquidationEmployeeList IsNot Nothing AndAlso LiquidationEmployeeList.Count > 0 Then

            For Each ObjLiquidation As Liquidation In LiquidationEmployeeList

                For Each ObjLiquidationDetail As LiquidationDetail In ObjLiquidation.LiquidationDetail

                    If ObjLiquidationDetail.ConceptClass <> "005" Or ObjLiquidationDetail.ConceptClass <> "006" Then

                        If ObjLiquidationDetail.Concept.AffectIBCIncentivePayment = True Then
                            If ObjLiquidationDetail.Concept.ConceptType = 1 Then
                                IncentiveVariableSalary = IncentiveVariableSalary + ObjLiquidationDetail.ConceptTotalValue
                            Else
                                IncentiveVariableSalary = IncentiveVariableSalary - ObjLiquidationDetail.ConceptTotalValue
                            End If
                        End If
                    End If
                Next
            Next
        Else
            IncentiveVariableSalary = 0
        End If

        If IncentiveVariableSalary > 0 Then
            IncentiveVariableSalary = (IncentiveVariableSalary / 180) * IncentiveDays
        End If

        'Averiguo las Licencias No Remuneradas y las Sanciones
        Dim UnpaidLicensesNovelty = _noveltyRepository.GetNoveltyUnpaidLicenses(Contract.EmployeeId)

        If UnpaidLicensesNovelty IsNot Nothing And UnpaidLicensesNovelty.Count > 0 Then
            'VarUnpaidLicensesDays = UnpaidLicensesNovelty.Sum(Function(x) x.Days)
            UnpaidLicensesDays = Me.InabilitiesPeriodDays(UnpaidLicensesNovelty, InitialDateIncentive, retirementDate)
        End If

        Dim SanctionNovelty = _noveltyRepository.GetNoveltySanctions(Contract.EmployeeId)

        If SanctionNovelty IsNot Nothing And SanctionNovelty.Count > 0 Then
            SanctionDays = Me.InabilitiesPeriodDays(SanctionNovelty, InitialDateIncentive, retirementDate)
        End If

        Dim ReplaceFormula As String = String.Empty

        ' IncentivePaymentValue = ReplaceDataFormulates(IncentivePaymentFormulate, LegalSalaryMinimun, TransportHealthValue, BasicSalary, IncentiveMonth, IncentiveDays, SanctionDays, UnpaidLicensesDays, IncentiveVariableSalary, 0, 0, 0, 0, 0, 0, 0, 0, ReplaceFormula)

        With DetailIncentivePayment
            .Description = "PRIMAS"
            .Accrued = IncentivePaymentValue
            .Deducted = 0
            .InitialDate = InitialDateIncentive
            .EndingDate = retirementDate
            .ConceptFormulate = IncentivePaymentFormulate
            .ReplaceConceptFormulate = ReplaceFormula
        End With

        Return DetailIncentivePayment

    End Function

    Public Function UnemployementPaymentCalculation(Contract As Domain.Payroll.Entities.Contract, Group As Group, UnemploymentFormulate As String, retirementDate As Date, BasicSalary As Decimal, TransportHealthValue As Decimal, LegalSalaryMinimun As Decimal) As ContractLiquidationDetail
        Dim DetailUnemploymentPayment = New ContractLiquidationDetail()

        Dim UnemploymentPaymentValue As Double = 0

        Dim UnemploymentDays As Integer = 0
        Dim SanctionDays As Integer = 0
        Dim UnpaidLicensesDays As Integer = 0
        Dim UnemployementVariableSalary As Decimal = 0

        'Valido Auxilio de Tranporte
        If BasicSalary > (2 * LegalSalaryMinimun) Then
            TransportHealthValue = 0
        End If

        Dim InitialDateUnemployment As New Date(retirementDate.Year, 1, 1)

        'Si el empleado ingresó después de la fecha de Inicio de la Prima
        If Contract.JobBondingDate > InitialDateUnemployment Then
            InitialDateUnemployment = Contract.JobBondingDate
        End If

        'Cargamos las variables
        UnemploymentDays = _liquidationDomain.Days360(InitialDateUnemployment, retirementDate)

        Dim LiquidationEmployeeList = _payrollLiquidationRepository.LiquidationByDateIncentivePayment(Contract.InitialContractNumber, InitialDateUnemployment, retirementDate)

        If LiquidationEmployeeList IsNot Nothing AndAlso LiquidationEmployeeList.Count > 0 Then

            For Each ObjLiquidation As Liquidation In LiquidationEmployeeList

                For Each ObjLiquidationDetail As LiquidationDetail In ObjLiquidation.LiquidationDetail

                    If ObjLiquidationDetail.ConceptClass <> "005" Or ObjLiquidationDetail.ConceptClass <> "006" Then

                        If ObjLiquidationDetail.Concept.AffectIBCSeverance = True Then
                            If ObjLiquidationDetail.Concept.ConceptType = 1 Then
                                UnemployementVariableSalary = UnemployementVariableSalary + ObjLiquidationDetail.ConceptTotalValue
                            Else
                                UnemployementVariableSalary = UnemployementVariableSalary - ObjLiquidationDetail.ConceptTotalValue
                            End If
                        End If
                    End If
                Next
            Next
        Else
            UnemployementVariableSalary = 0
        End If

        If UnemployementVariableSalary > 0 Then
            UnemployementVariableSalary = (UnemployementVariableSalary / 360) * UnemploymentDays
        End If

        'Averiguo las Licencias No Remuneradas y las Sanciones
        Dim UnpaidLicensesNovelty = _noveltyRepository.GetNoveltyUnpaidLicenses(Contract.EmployeeId)

        If UnpaidLicensesNovelty IsNot Nothing And UnpaidLicensesNovelty.Count > 0 Then
            'VarUnpaidLicensesDays = UnpaidLicensesNovelty.Sum(Function(x) x.Days)
            UnpaidLicensesDays = Me.InabilitiesPeriodDays(UnpaidLicensesNovelty, InitialDateUnemployment, retirementDate)
        End If

        Dim SanctionNovelty = _noveltyRepository.GetNoveltySanctions(Contract.EmployeeId)

        If SanctionNovelty IsNot Nothing And SanctionNovelty.Count > 0 Then
            SanctionDays = Me.InabilitiesPeriodDays(SanctionNovelty, InitialDateUnemployment, retirementDate)
        End If

        Dim ReplaceFormula As String = String.Empty

        ' UnemploymentPaymentValue = ReplaceDataFormulates(UnemploymentFormulate, LegalSalaryMinimun, TransportHealthValue, BasicSalary, 0, 0, SanctionDays, UnpaidLicensesDays, 0, UnemployementVariableSalary, UnemploymentDays, 0, 0, 0, 0, 0, 0, ReplaceFormula)

        With DetailUnemploymentPayment
            .Description = "CESANTIAS"
            .Accrued = UnemploymentPaymentValue
            .Deducted = 0
            .InitialDate = InitialDateUnemployment
            .EndingDate = retirementDate
            .ConceptFormulate = UnemploymentFormulate
            .ReplaceConceptFormulate = ReplaceFormula
        End With

        Return DetailUnemploymentPayment
    End Function

    Public Function UnemployementInterestPaymentCalculation(UnemploymentValue As Decimal, UnemploymentInitialDate As Date, RetirementDate As Date) As ContractLiquidationDetail
        Dim DetailUnemploymentPayment = New ContractLiquidationDetail()

        Dim InterestUnemploymentValue = UnemploymentValue * 0.12

        With DetailUnemploymentPayment
            .Description = "INTERESES DE CESANTIAS"
            .Accrued = InterestUnemploymentValue
            .Deducted = 0
            .InitialDate = UnemploymentInitialDate
            .EndingDate = RetirementDate
            .ConceptFormulate = "[Valor Cesantias] * 0.12"
            .ReplaceConceptFormulate = "[ " + UnemploymentValue.ToString() + " ] * 0.12"
        End With

        Return DetailUnemploymentPayment
    End Function

    Public Function VacationCalculation(Contract As Domain.Payroll.Entities.Contract, Group As Group, VacationFormulate As String, retirementDate As Date, BasicSalary As Decimal, TransportHealthValue As Decimal, LegalSalaryMinimun As Decimal) As ContractLiquidationDetail

        Dim DetailVacation = New ContractLiquidationDetail()

        Dim VacationValue As Double = 0

        Dim daysvacation As Integer = Group.PayrollParameter.VacationDays
        Dim daysForAPeriod As Decimal = 360 / Group.PayrollParameter.MaxVacationByYear

        Dim daysWorkEmployee As Integer = 0
        If Contract.Employee.VacationLastDateLiquidation < retirementDate Then
            daysWorkEmployee = _liquidationDomain.Days360(Contract.Employee.VacationLastDateLiquidation, retirementDate)
        Else
            daysWorkEmployee = 0
        End If

        If Contract.Employee.VacationLastDateLiquidation = retirementDate Then
            daysWorkEmployee = 1
        End If

        Dim daysVacationEmployee As Decimal = (daysvacation * daysWorkEmployee) / daysForAPeriod
        Dim listVacationPeriodEmployee = _vacationPeriodRepository.GetVacationPeriodWithDetailByEmployee(Contract.EmployeeId)
        Dim daysPayment As Integer = 0

        Dim PendingDays As Integer = 0
        For Each itemVacationPeriod In listVacationPeriodEmployee
            If itemVacationPeriod.Vacation IsNot Nothing AndAlso itemVacationPeriod.Vacation.Count > 0 Then
                If itemVacationPeriod.Vacation.Where(Function(x) x.State = 2).Count > 0 Then
                    PendingDays = PendingDays + itemVacationPeriod.PendingDays
                End If
            End If

        Next

        Dim TotalPendingDays As Integer = (360 * PendingDays) / 15

        daysWorkEmployee = daysWorkEmployee + TotalPendingDays

        If daysWorkEmployee < 0 Then
            daysWorkEmployee = 0
        End If


        Dim ContractSalaryPromedy As Decimal = 0
        Dim NormalEveningRechargePromedy As Decimal = 0
        Dim FestiveEveningRechargePromedy As Decimal = 0
        Dim SundayRechargePromedy As Decimal = 0
        Dim ExtraHoursPromedy As Decimal = 0

        Dim InitialMonth = retirementDate.AddMonths(Contract.Group.PayrollParameter.AverageMonthVacationCompensation * -1)

        Dim CountMonth = Contract.Group.PayrollParameter.AverageMonthVacationCompensation

        Dim ContractSalaryList = _payrollLiquidationRepository.GetLastConceptClassListDateBetween(Contract.InitialContractNumber, "005", InitialMonth, retirementDate)
        ContractSalaryPromedy = CalcValueAcumulatedLiquidationDetail(ContractSalaryList, CountMonth)

        Dim ContractNormalEveningList = _payrollLiquidationRepository.GetLastConceptClassListDateBetween(Contract.InitialContractNumber, "042", InitialMonth, retirementDate)
        NormalEveningRechargePromedy = CalcValueAcumulatedLiquidationDetail(ContractNormalEveningList, CountMonth)

        Dim ContractFestiveEveningList = _payrollLiquidationRepository.GetLastConceptClassListDateBetween(Contract.InitialContractNumber, "043", InitialMonth, retirementDate)
        FestiveEveningRechargePromedy = CalcValueAcumulatedLiquidationDetail(ContractFestiveEveningList, CountMonth)

        Dim SundayRechargeList = _payrollLiquidationRepository.GetLastListConceptClassListDateBetween(Contract.InitialContractNumber, New List(Of String)({"051", "052"}), InitialMonth, retirementDate)
        SundayRechargePromedy = CalcValueAcumulatedLiquidationDetail(SundayRechargeList, CountMonth)

        Dim ExtraHoursList = _payrollLiquidationRepository.GetLastListConceptClassListDateBetween(Contract.InitialContractNumber, New List(Of String)({"001", "012", "013", "050"}), InitialMonth, retirementDate)
        ExtraHoursPromedy = CalcValueAcumulatedLiquidationDetail(ExtraHoursList, CountMonth)


        Dim ReplaceFormula As String = String.Empty
        ' VacationValue = ReplaceDataFormulates(VacationFormulate, LegalSalaryMinimun, TransportHealthValue, BasicSalary, 0, 0, 0, 0, 0, 0, 0, daysWorkEmployee, ContractSalaryPromedy, NormalEveningRechargePromedy, FestiveEveningRechargePromedy, SundayRechargePromedy, ExtraHoursPromedy, ReplaceFormula)

        With DetailVacation
            .Description = "VACACIONES"
            .Accrued = VacationValue
            .Deducted = 0
            .InitialDate = Contract.Employee.VacationLastDateLiquidation
            .EndingDate = retirementDate
            .ConceptFormulate = VacationFormulate
            .ReplaceConceptFormulate = ReplaceFormula
        End With

        Return DetailVacation

    End Function




    Public Function CalcValueAcumulatedLiquidationDetail(ListLiquidationDetail As List(Of LiquidationDetail), CountMonth As Integer) As Decimal
        If ListLiquidationDetail IsNot Nothing AndAlso ListLiquidationDetail.Count > 0 Then
            Dim Value As Decimal = 0
            Dim AccruedValue As Decimal = 0
            Dim DeductedValue As Decimal = 0
            Dim ObjListLiquidation As New List(Of Liquidation)

            AccruedValue = ListLiquidationDetail.Where(Function(x) x.ConceptType = 1).Sum(Function(x) x.ConceptTotalValue)
            DeductedValue = ListLiquidationDetail.Where(Function(x) x.ConceptType = 2).Sum(Function(x) x.ConceptTotalValue)

            Return (AccruedValue - DeductedValue) / CountMonth
        Else
            Return 0
        End If

    End Function

    Public Function AdjustBasicSalary(BasicSalary As Decimal, retirementDate As Date, InitialPayrollDate As Date, EndPayrollDate As Date, TransportHelpValue As Decimal, LegalSalaryMinimun As Decimal, Group As Group) As ActionMessageResult(Of List(Of ContractLiquidationDetail))

        Dim ListReturn As New ActionMessageResult(Of List(Of ContractLiquidationDetail))

        Dim ListContractLiquidationDetail As New List(Of ContractLiquidationDetail)

        'Ajuste de Salarios
        Dim objAdjustBasicSalary = New ContractLiquidationDetail()
        Dim SalaryAdjust As Decimal = 0

        'Variables de los Id's de los Conceptos
        Dim IdSalaryConcept As Integer?
        Dim IdTransportHealthConcept As Integer?
        Dim IdHealthValueConcept As Integer?
        Dim IdPensionValueConcept As Integer?
        Dim IdPensionSolidarityValueConcept As Integer?

        If retirementDate = EndPayrollDate Then
            ListReturn.StateResult = False
            Return ListReturn
        End If

        Dim workedAdjustDays = Math.Abs(_liquidationDomain.Days360(retirementDate.AddDays(1), EndPayrollDate))

        If workedAdjustDays = 0 Then
            ListReturn.StateResult = False
            Return ListReturn
        End If

        If Group.GroupAdjustConceptContractLiquidation Is Nothing Or Group.GroupAdjustConceptContractLiquidation.Count = 0 Then
            ListReturn.Message = "No ha parametrizado ningún Concepto de Ajuste en el Formulario de Grupos"
            ListReturn.StateResult = False
            Return ListReturn
        End If


        If Group.GroupAdjustConceptContractLiquidation IsNot Nothing AndAlso Group.GroupAdjustConceptContractLiquidation.Count > 0 Then

            For Each objGroupAdjuntsConcept As GroupAdjustConceptContractLiquidation In Group.GroupAdjustConceptContractLiquidation

                If objGroupAdjuntsConcept.ConceptType = 1 Then
                    IdSalaryConcept = objGroupAdjuntsConcept.IdConcept
                End If

                If objGroupAdjuntsConcept.ConceptType = 2 Then
                    IdTransportHealthConcept = objGroupAdjuntsConcept.IdConcept
                End If

                If objGroupAdjuntsConcept.ConceptType = 3 Then
                    IdHealthValueConcept = objGroupAdjuntsConcept.IdConcept
                End If

                If objGroupAdjuntsConcept.ConceptType = 4 Then
                    IdPensionValueConcept = objGroupAdjuntsConcept.IdConcept
                End If

                If objGroupAdjuntsConcept.ConceptType = 5 Then
                    IdPensionSolidarityValueConcept = objGroupAdjuntsConcept.IdConcept
                End If
            Next
        End If


        Dim ErrorMessageResult As New List(Of MessageResult)

        If IdSalaryConcept Is Nothing Then
            ErrorMessageResult.Add(New MessageResult("999", "No ha parametrizado Concepto de Ajuste de Sueldo en el Formulario de Grupos"))
        End If

        If IdTransportHealthConcept Is Nothing Then
            ErrorMessageResult.Add(New MessageResult("999", "No ha parametrizado Concepto de Ajuste de Auxilio de Transporte en el Formulario de Grupos"))
        End If

        If IdHealthValueConcept Is Nothing Then
            ErrorMessageResult.Add(New MessageResult("999", "No ha parametrizado Concepto de Ajuste de Aporte a Salud en el Formulario de Grupos"))
        End If

        If IdPensionValueConcept Is Nothing Then
            ErrorMessageResult.Add(New MessageResult("999", "No ha parametrizado Concepto de Ajuste de Aporte a Pensión en el Formulario de Grupos"))
        End If

        If ErrorMessageResult.Count > 0 Then
            ListReturn.MessageResult = ErrorMessageResult
            ListReturn.StateResult = False
            Return ListReturn
        End If

        SalaryAdjust = Math.Round((BasicSalary * workedAdjustDays) / 30)

        With objAdjustBasicSalary
            .Description = _conceptRepository.GetConceptId(IdSalaryConcept).Name
            .Accrued = 0
            .Deducted = SalaryAdjust
            .InitialDate = InitialPayrollDate
            .EndingDate = EndPayrollDate
            .IdConcept = IdSalaryConcept
            .ConceptType = 2
        End With

        ListContractLiquidationDetail.Add(objAdjustBasicSalary)

        'Ajuste de Auxilio de Transporte
        If TransportHelpValue > 0 Then
            Dim objAdjustTransport = New ContractLiquidationDetail()
            Dim TransportAdjust As Decimal = 0

            TransportAdjust = (TransportHelpValue * workedAdjustDays) / 30

            With objAdjustTransport
                .Description = _conceptRepository.GetConceptId(IdTransportHealthConcept).Name
                .Accrued = 0
                .Deducted = TransportAdjust
                .InitialDate = InitialPayrollDate
                .EndingDate = EndPayrollDate
                .IdConcept = IdTransportHealthConcept
                .ConceptType = 2
            End With

            ListContractLiquidationDetail.Add(objAdjustTransport)
        End If

        ListReturn.ObjectEmbbeded = ListContractLiquidationDetail
        ListReturn.StateResult = True

        Return ListReturn

    End Function

    ''' <summary>
    ''' Funcion para hacer ajuste salarial si el empleado se retiro antes de la nomina por formula
    ''' </summary>
    ''' <param name="BasicSalary"></param>
    ''' <param name="retirementDate"></param>
    ''' <param name="InitialPayrollDate"></param>
    ''' <param name="EndPayrollDate"></param>
    ''' <param name="Group"></param>
    ''' <param name="contractId"></param>
    ''' <returns></returns>
    Public Function AdjustBasicSalaryCR(BasicSalary As Decimal, retirementDate As Date, InitialPayrollDate As Date, EndPayrollDate As Date, Group As Group, contractId As Integer) As ActionMessageResult(Of List(Of ContractLiquidationDetail))

        Dim ListReturn As New ActionMessageResult(Of List(Of ContractLiquidationDetail))

        Dim ListContractLiquidationDetail As New List(Of ContractLiquidationDetail)

        'Ajuste de Salarios
        Dim objAdjustBasicSalary = New ContractLiquidationDetail()
        Dim SalaryAdjust As Decimal = 0

        'Variables de los Id's de los Conceptos
        Dim IdSalaryConcept As Integer?

        If retirementDate = EndPayrollDate Then
            ListReturn.StateResult = False
            Return ListReturn
        End If

        'Validar los dias trabajados de la ulima  liquidacion
        Dim lastLiquidations = _contractLiquidationRepository.GetPaymentsByContractId(contractId)
        If lastLiquidations IsNot Nothing AndAlso lastLiquidations.Count > 0 Then
            Dim lastLiquidation = lastLiquidations.OrderByDescending(Function(x) x.PayrollDateLiquidated).FirstOrDefault()
            If lastLiquidation IsNot Nothing AndAlso lastLiquidation.DaysWorked > 0 Then
                'Calcular los dias de retiro entre InitialPayrollDate y retirementDate (incluyendo ambos dias)
                Dim retirementDays = DateDiff(DateInterval.Day, InitialPayrollDate, retirementDate) + 1
                If retirementDays = lastLiquidation.DaysWorked Then
                    ListReturn.StateResult = False
                    Return ListReturn
                End If
            End If
        End If

        Dim workedAdjustDays = Math.Abs(_liquidationDomain.Days360(retirementDate.AddDays(1), EndPayrollDate))

        If workedAdjustDays = 0 Then
            ListReturn.StateResult = False
            Return ListReturn
        End If

        If Group.GroupAdjustConceptContractLiquidation Is Nothing Or Group.GroupAdjustConceptContractLiquidation.Count = 0 Then
            ListReturn.Message = "No ha parametrizado ningún Concepto de Ajuste en el Formulario de Grupos"
            ListReturn.StateResult = False
            Return ListReturn
        End If


        If Group.GroupAdjustConceptContractLiquidation IsNot Nothing AndAlso Group.GroupAdjustConceptContractLiquidation.Count > 0 Then

            For Each objGroupAdjuntsConcept As GroupAdjustConceptContractLiquidation In Group.GroupAdjustConceptContractLiquidation

                If objGroupAdjuntsConcept.ConceptType = 1 Then
                    IdSalaryConcept = objGroupAdjuntsConcept.IdConcept
                End If
            Next
        End If


        Dim ErrorMessageResult As New List(Of MessageResult)

        If IdSalaryConcept Is Nothing Then
            ErrorMessageResult.Add(New MessageResult("999", "No ha parametrizado Concepto de Ajuste de Sueldo en el Formulario de Grupos"))
        End If

        If ErrorMessageResult.Count > 0 Then
            ListReturn.MessageResult = ErrorMessageResult
            ListReturn.StateResult = False
            Return ListReturn
        End If

        SalaryAdjust = Math.Round((BasicSalary * workedAdjustDays) / 30)

        With objAdjustBasicSalary
            .Description = _conceptRepository.GetConceptId(IdSalaryConcept).Name
            .Accrued = 0
            .Deducted = SalaryAdjust
            .InitialDate = InitialPayrollDate
            .EndingDate = EndPayrollDate
            .IdConcept = IdSalaryConcept
            .ConceptType = 2 'Devengado
        End With

        ListContractLiquidationDetail.Add(objAdjustBasicSalary)
        ListReturn.ObjectEmbbeded = ListContractLiquidationDetail
        ListReturn.StateResult = True

        Return ListReturn

    End Function


    Public Function CreateVoucherTransaction(ContractLiquidation As ContractLiquidation, indigo As SessionValues) As ActionResult(Of Domain.Entities.VoucherTransaction) Implements IContractLiquidationDomain.CreateVoucherTransaction

        Dim ResultObjJournalVouchers As New ActionResult(Of Domain.Entities.VoucherTransaction)

        Try

            Dim PayrollSettings = _payrollSettingsRepository.GetSettingPayroll()

            If PayrollSettings.IdExpenseConceptsLiquidation Is Nothing Then
                Return New ActionResult(Of Domain.Entities.VoucherTransaction) With {.StateResult = False, .Message = "No ha parametrizado el Concepto de Egreso de Liquidaciones de Contrato en Parámetros de Nómina"}
            End If

            If PayrollSettings.ExpenseTypeLiquidation = 1 Then
                If PayrollSettings.IdEntityBankAccountLiquidation Is Nothing Then
                    Return New ActionResult(Of Domain.Entities.VoucherTransaction) With {.StateResult = False, .Message = "No ha parametrizado la Cuenta Bancaria de Liquidaciones de Contrato en Parámetros de Nómina"}
                End If
            Else
                If PayrollSettings.IdCashRegisterLiquidation Is Nothing Then
                    Return New ActionResult(Of Domain.Entities.VoucherTransaction) With {.StateResult = False, .Message = "No ha parametrizado la Caja de Liquidaciones de Contrato en Parámetros de Nómina"}
                End If
            End If

            Dim PayrollDate = ContractLiquidation.RetirementDate

            Dim VoucherTransaction As New Domain.Entities.VoucherTransaction

            Dim Employee = _employeeRepository.GetEmployeeById(ContractLiquidation.EmployeeId)
            Dim ObjExpenseConcepts = _expenseConceptRepository.GetExpenseConceptById(PayrollSettings.IdExpenseConceptsLiquidation)
            Dim ObjCompany = _companyRepository.GetCompanyById(ContractLiquidation.Contract.Group.CompanyId)


            'Insertamos el Detalle
            Dim VoucherTransactionDetail As New Domain.Entities.VoucherTransactionDetails
            'Débito
            VoucherTransactionDetail.IdThirdParty = Employee.ThirdPartyId
            VoucherTransactionDetail.IdExpenseConcept = PayrollSettings.IdExpenseConceptsLiquidation
            VoucherTransactionDetail.IdMainAccount = ObjExpenseConcepts.IdMainAccount
            VoucherTransactionDetail.Nature = 1
            VoucherTransactionDetail.Value = ContractLiquidation.TotalPaid
            VoucherTransactionDetail.PercentRetention = 0
            VoucherTransactionDetail.Detail = "Pago de la Liquidación de Contrato del Empleado " + Employee.ThirdParty.Nit + " - " + Employee.ThirdParty.Name
            VoucherTransaction.VoucherTransactionDetails.Add(VoucherTransactionDetail)


            'Insertamos la Cabecera
            VoucherTransaction.Code = ""
            VoucherTransaction.IdThirdParty = ObjCompany.ThirdPartyId
            VoucherTransaction.VoucherClass = 1
            VoucherTransaction.ExpenseType = PayrollSettings.ExpenseTypeLiquidation
            VoucherTransaction.Detail = "LIQUIDACIÓN DE CONTRATO del Empleado " + Employee.ThirdParty.Nit + " - " + Employee.ThirdParty.Name + " con Fecha de Retiro " + PayrollDate.ToShortDateString()
            VoucherTransaction.DocumentDate = Date.Now()

            If PayrollSettings.ExpenseTypeLiquidation = 1 Then
                Dim ObjEntityBankAccount = _entityBankAccount.GetEntityBankAccountById(PayrollSettings.IdEntityBankAccountLiquidation)
                VoucherTransaction.IdEntityBankAccount = PayrollSettings.IdEntityBankAccountLiquidation
                VoucherTransaction.IdMainAccount = ObjEntityBankAccount.IdMainAccount
                VoucherTransaction.PaymentMethod = PayrollSettings.PaymentMethodLiquidation
                VoucherTransaction.NoteNumber = 1
                VoucherTransaction.BankAccountNumber = ObjEntityBankAccount.Number
                VoucherTransaction.BankName = ObjEntityBankAccount.Bank.Name
            Else
                VoucherTransaction.IdCashRegister = PayrollSettings.IdCashRegisterLiquidation
                VoucherTransaction.IdMainAccount = _cashRegisterRepository.GetCashRegisterById(PayrollSettings.IdCashRegisterLiquidation).IdMainAccount
            End If

            VoucherTransaction.Value = VoucherTransaction.VoucherTransactionDetails.Sum(Function(x) x.Value)
            VoucherTransaction.CheckNumber = 0
            VoucherTransaction.TaxByMil = 0
            VoucherTransaction.TaxByMilValue = 0
            VoucherTransaction.CashRegisterExpense = 0
            VoucherTransaction.RefundCashRegisterExpense = 0
            VoucherTransaction.BeneficiaryIdentification = ObjCompany.ThirdParty.Nit
            VoucherTransaction.Beneficiary = ObjCompany.ThirdParty.Name
            VoucherTransaction.TransactionRelationship = 0
            VoucherTransaction.CheckReconciled = 0
            VoucherTransaction.Printed = 0
            VoucherTransaction.RTEValue = 0
            VoucherTransaction.IVAValue = 0
            VoucherTransaction.ICAValue = 0
            VoucherTransaction.OtherValue = 0
            VoucherTransaction.IdUnitOperative = indigo.IndigoOperatingUnitId
            VoucherTransaction.Status = 1
            VoucherTransaction.CreationUser = indigo.UserIndigo

            Dim DateVoucher As Date

            If ContractLiquidation.RetirementDate < Date.Now Then
                DateVoucher = ContractLiquidation.RetirementDate
            Else
                DateVoucher = Date.Now
            End If

            VoucherTransaction.CreationDate = DateVoucher
            VoucherTransaction.EmailSent = 0

            ResultObjJournalVouchers.ObjectEmbbeded = VoucherTransaction
            ResultObjJournalVouchers.StateResult = True

        Catch ex As Exception
            ResultObjJournalVouchers.ObjectEmbbeded = Nothing
            ResultObjJournalVouchers.StateResult = False
            ResultObjJournalVouchers.Message = ex.Message.ToString()
        End Try

        Return ResultObjJournalVouchers
    End Function

    ''' <summary>
    ''' Función para crear el Comprobante Contable de las Liquidaciones de Contrato
    ''' </summary>
    ''' <param name="ListContractLiquidationDetail">ListContractLiquidationDetail</param>
    ''' <param name="Employee">Employee</param>
    ''' <param name="Contract">Contract</param>
    ''' <param name="group">group</param>
    ''' <param name="audit">audit</param>
    ''' <returns>ActionResult(Of Domain.Entities.JournalVouchers)</returns>
    Public Function CreateJournalVoucher(ListContractLiquidationDetail As List(Of ContractLiquidationDetail), Employee As Domain.Payroll.Entities.Employee, Contract As Domain.Payroll.Entities.Contract, group As Group, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of List(Of Domain.Entities.JournalVouchers)) Implements IContractLiquidationDomain.CreateJournalVoucher

        Dim ResultObjJournalVouchers As New ActionResult(Of List(Of Domain.Entities.JournalVouchers))

        'Llamamos a parámetros de nómina
        Dim PayrollSettings = _payrollSettingsRepository.GetSettingPayroll()

        If PayrollSettings Is Nothing Then
            ResultObjJournalVouchers.StateResult = False
            ResultObjJournalVouchers.Message = "No se encontraron Parámetros de Nómina"
            Return ResultObjJournalVouchers
        End If

        Try

            Dim ListJournalVoucher As New List(Of Domain.Entities.JournalVouchers)
            Dim ListConceptAccount = _IConceptAccountingStructureRepository.GetAccountingStructureIntegrated(Contract.FunctionalUnitId)
            Dim ObjListAgreements = _AgreementsCRepository.GetAgreementsByEmployeeLiquidationContract(Employee.Id, "2")
            Dim ObjListForeclousure = _ForeclousureRepository.GetForeclousureByEmployeeStatus(Employee.Id, 2)

            If group.PayrollParameter.IdLiquidationContractAccount Is Nothing Then
                Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "En el Formulario de Grupos debe parametrizar la Cuenta Contable de Liquidaciones de Contrato"}
            End If

            If group.PayrollParameter.IdPrestacionVoucherType Is Nothing Then
                Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "En el Formulario de Grupos debe parametrizar el Tipo de Comprobantes Contables para Prestaciones Sociales"}
            End If

            If group.PayrollParameter.IdContractLiquidationVoucherType Is Nothing Then
                Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "En el Formulario de Grupos debe parametrizar el Tipo de Comprobantes Contables para Liquidaciones de Contrato"}
            End If

            '' Comprobante Contable de Nómina
            Dim JournalVoucher As New Domain.Entities.JournalVouchers
            Dim ListJournalVoucherDetailas As New List(Of Domain.Entities.JournalVoucherDetails)

            '' Comprobante Contable de Prestaciones Sociales
            Dim JournalVoucherPrestacion As New Domain.Entities.JournalVouchers
            Dim ListJournalVoucherDetailsPrestacion As New List(Of Domain.Entities.JournalVoucherDetails)

            'Primero, hago el Comprobante Contable de Nómina

            Dim DetailContract = ListContractLiquidationDetail.FirstOrDefault()
            Dim DateVoucher As Date
            Dim EndingDate As Date

            If DetailContract IsNot Nothing Then
                If DetailContract.ContractLiquidation.RetirementDate < Date.Now Then
                    DateVoucher = DetailContract.ContractLiquidation.RetirementDate
                Else
                    DateVoucher = Date.Now
                End If
                EndingDate = DetailContract.EndingDate
            End If

            If ListConceptAccount.Any(Function(x) x.ConceptId Is Nothing) Then
                Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "Existen conceptos con parametrización contable errónea. Verifique las cuentas Contables de los Conceptos que está liquidando"}
            End If

            With JournalVoucher

                For Each ObjContractLiquidationDetail As ContractLiquidationDetail In ListContractLiquidationDetail.Where(Function(x) x.ConceptType <> 3).ToList
                    Dim NewObjJournalVoucherDetail As New Domain.Entities.JournalVoucherDetails

                    Dim ObjConcept = _conceptRepository.GetConceptId(ObjContractLiquidationDetail.IdConcept)

                    If ListConceptAccount.Any(Function(x) x.ConceptId = ObjContractLiquidationDetail.IdConcept) = False Then
                        Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "El Concepto " + ObjConcept.Code + " - " + ObjConcept.Name + " no tiene parametrizada cuenta contable"}
                    End If

                    Dim thirdPartyId As Integer
                    Dim IdMainAccount As Integer
                    Dim AdcruedValue As Decimal = 0
                    Dim DeductetValue As Decimal = 0
                    Dim ValidateAccount As Domain.Payroll.Entities.MainAccounts = Nothing

                    If ObjContractLiquidationDetail.Accrued > 0 Then
                        thirdPartyId = Employee.ThirdPartyId
                        AdcruedValue = ObjContractLiquidationDetail.Accrued
                        DeductetValue = 0

                        Dim tmpAccount = ListConceptAccount.Where(Function(x) x.ConceptId = ObjContractLiquidationDetail.IdConcept).FirstOrDefault()
                        Dim AcruedAccount As String

                        ValidateAccount = _CostDistributionRepository.GetMainAccountByNumber(tmpAccount.AccruedAccount)

                        If ObjConcept.ConceptClass = "021" OrElse ObjConcept.ConceptClass = "023" OrElse ObjConcept.ConceptClass = "027" Then
                            ValidateAccount = _CostDistributionRepository.GetMainAccountByNumber(tmpAccount.InabilityDebitValueEmployeeAccount)
                        End If

                        If ValidateAccount Is Nothing Then
                            Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "La cuenta Contable " + tmpAccount.AccruedAccount + " del Concepto " + ObjConcept.Code + " - " + ObjConcept.Name + " no existe"}
                        End If

                        AcruedAccount = tmpAccount.AccruedAccount

                        If ObjConcept.ConceptClass = "021" OrElse ObjConcept.ConceptClass = "023" OrElse ObjConcept.ConceptClass = "027" Then
                            AcruedAccount = tmpAccount.InabilityDebitValueEmployeeAccount
                        End If

                        IdMainAccount = _CostDistributionRepository.GetMainAccountByNumber(AcruedAccount).Id
                    Else

                        If ObjConcept.ConceptClass = "041" Then
                            'Convenios
                            If ObjListAgreements.Any(Function(x) x.ConceptId = ObjConcept.Id) Then
                                Dim tmpObjAgreements = ObjListAgreements.Where(Function(x) x.ConceptId = ObjConcept.Id).FirstOrDefault()
                                thirdPartyId = tmpObjAgreements.Company.ThirdPartyId
                            End If
                        ElseIf ObjConcept.ConceptClass = "017" Then
                            'Aporte a Salud
                            Dim HealthFundContract = Contract.FundContract.Where(Function(x) x.FundType = 1 And x.State = True).FirstOrDefault()

                            If HealthFundContract Is Nothing Then
                                Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "No hay Fondo de Salud Creado para este empleado o está inactivo"}
                            End If

                            Dim HealthFund = _FundsRepository.GetFundsById(HealthFundContract.FundId)
                            thirdPartyId = HealthFund.ThirdPartyId

                        ElseIf ObjConcept.ConceptClass = "014" Or ObjConcept.ConceptClass = "038" Then
                            'Aporte a Pension
                            Dim PenionFundContract = Contract.FundContract.Where(Function(x) x.FundType = 2 And x.State = True).FirstOrDefault()

                            If PenionFundContract Is Nothing Then
                                Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "No hay Fondo de Pensión Creado para este empleado o está inactivo"}
                            End If

                            Dim PensionFund = _FundsRepository.GetFundsById(PenionFundContract.FundId)
                            thirdPartyId = PensionFund.ThirdPartyId
                        Else
                            thirdPartyId = Employee.ThirdPartyId
                        End If

                        AdcruedValue = 0
                        DeductetValue = ObjContractLiquidationDetail.Deducted

                        Dim tmpAccount = ListConceptAccount.Where(Function(x) x.ConceptId = ObjContractLiquidationDetail.IdConcept).FirstOrDefault()

                        ValidateAccount = _CostDistributionRepository.GetMainAccountByNumber(tmpAccount.DeductedAccount)
                        If ValidateAccount Is Nothing Then
                            Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "La cuenta Contable " + tmpAccount.DeductedAccount + " del Concepto " + ObjConcept.Code + " - " + ObjConcept.Name + " no existe"}
                        End If

                        IdMainAccount = _CostDistributionRepository.GetMainAccountByNumber(tmpAccount.DeductedAccount).Id

                    End If

                    With NewObjJournalVoucherDetail
                        .IdMainAccount = IdMainAccount
                        .IdThirdParty = thirdPartyId
                        If ValidateAccount IsNot Nothing AndAlso ValidateAccount.HandlesCostCenter = True Then
                            .IdCostCenter = Contract.FunctionalUnit.CostCenterId
                        End If
                        .DebitValue = Math.Round(AdcruedValue, 2)
                        .CreditValue = Math.Round(DeductetValue, 2)
                        .Detail = "Pago Generado desde Liquidación de Contrato"
                    End With

                    'Insertamos los detalles
                    .JournalVoucherDetails.Add(NewObjJournalVoucherDetail)
                Next

                'Insertamos la Cuenta x Pagar de Nómina con el total Pagado
                Dim NewPaidVoucherDetail As New Domain.Entities.JournalVoucherDetails

                'Totalizo la Liquidación de Contrato
                Dim TotalPaid = .JournalVoucherDetails.Sum(Function(x) x.DebitValue) - .JournalVoucherDetails.Sum(Function(x) x.CreditValue)

                If TotalPaid > 0 Then

                    With NewPaidVoucherDetail
                        .IdMainAccount = group.PayrollParameter.IdLiquidationContractAccount
                        .IdThirdParty = Employee.ThirdPartyId
                        .DebitValue = 0
                        .CreditValue = TotalPaid
                        .Detail = "Pago Generado desde Liquidación de Contrato"
                    End With

                    .JournalVoucherDetails.Add(NewPaidVoucherDetail)
                End If

                'Insertamos la Cabecera

                .IdJournalVoucher = group.PayrollParameter.IdContractLiquidationVoucherType
                .VoucherDate = DateVoucher
                .Detail = "Comprobante Contable de Nómina - Liquidación de Contrato - Empleado: " + Employee.ThirdParty.Nit + " - " + Employee.ThirdParty.Name
                .EntityName = "PayrollLiquidation"
                .IsClosedYear = 0
                .CreationUser = audit.CodeUser
                .CreationDate = Date.Now()
                .BookCurrencyId = PayrollSettings.CurrencyId
                .DateTRM = EndingDate
                .Status = 2
            End With

            ListJournalVoucher.Add(JournalVoucher)

            'Ahora hago el comprobante contable de prestaciones sociales
            With JournalVoucherPrestacion

                For Each ObjContractLiquidationDetail As ContractLiquidationDetail In ListContractLiquidationDetail.Where(Function(x) x.ConceptType = 3).ToList

                    Dim ObjConcept = _conceptRepository.GetConceptId(ObjContractLiquidationDetail.IdConcept)

                    If ListConceptAccount.Any(Function(x) x.ConceptId = ObjContractLiquidationDetail.IdConcept) = False Then
                        Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "El Concepto " + ObjConcept.Code + " - " + ObjConcept.Name + " no tiene parametrizada cuenta contable"}
                    End If

                    Dim tmpAccount = ListConceptAccount.Where(Function(x) x.ConceptId = ObjContractLiquidationDetail.IdConcept).FirstOrDefault()


                    For i As Integer = 0 To 1

                        Dim thirdPartyId As Integer
                        Dim NewPaidVoucherDetail As New Domain.Entities.JournalVoucherDetails

                        If ObjConcept.ConceptClass = "035" Then ' SENA
                            If _thirdPartyRepository.GetThirdPartyByNit("899999034") IsNot Nothing Then
                                thirdPartyId = _thirdPartyRepository.GetThirdPartyByNit("899999034").Id
                            End If


                        ElseIf ObjConcept.ConceptClass = "037" Then ' ICBF
                            If _thirdPartyRepository.GetThirdPartyByNit("899999239") IsNot Nothing Then
                                thirdPartyId = _thirdPartyRepository.GetThirdPartyByNit("899999034").Id
                            End If


                        ElseIf ObjConcept.ConceptClass = "036" Then ' CAJA DE COMPENSACIÓN
                            Dim CompensationFundContract = Contract.FundContract.Where(Function(x) x.FundType = 5 And x.State = True).FirstOrDefault()

                            If CompensationFundContract Is Nothing Then
                                Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "No hay Fondo de Caja de Compensación Creado para este empleado o está inactivo"}
                            End If

                            Dim CompensationFund = _FundsRepository.GetFundsById(CompensationFundContract.FundId)
                            thirdPartyId = CompensationFund.ThirdPartyId


                        ElseIf ObjConcept.ConceptClass = "018" Then ' SALUD
                            Dim HealthFundContract = Contract.FundContract.Where(Function(x) x.FundType = 1 And x.State = True).FirstOrDefault()

                            If HealthFundContract Is Nothing Then
                                Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "No hay Fondo de Salud Creado para este empleado o está inactivo"}
                            End If

                            Dim HealthFund = _FundsRepository.GetFundsById(HealthFundContract.FundId)
                            thirdPartyId = HealthFund.ThirdPartyId


                        ElseIf ObjConcept.ConceptClass = "015" Then ' PENSIÓN
                            Dim PensionFundContract = Contract.FundContract.Where(Function(x) x.FundType = 2 And x.State = True).FirstOrDefault()

                            If PensionFundContract Is Nothing Then
                                Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "No hay Fondo de Pensión Creado para este empleado o está inactivo"}
                            End If

                            Dim PensionFund = _FundsRepository.GetFundsById(PensionFundContract.FundId)
                            thirdPartyId = PensionFund.ThirdPartyId


                        ElseIf ObjConcept.ConceptClass = "009" Then 'ARL
                            Dim ARLFundContract = Contract.FundContract.Where(Function(x) x.FundType = 4 And x.State = True).FirstOrDefault()

                            If ARLFundContract Is Nothing Then
                                Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .Message = "No hay Fondo de ARL Creado para este empleado o está inactivo"}
                            End If

                            Dim ARLFund = _FundsRepository.GetFundsById(ARLFundContract.FundId)
                            thirdPartyId = ARLFund.ThirdPartyId
                        Else
                            thirdPartyId = Employee.ThirdPartyId
                        End If

                        If i = 0 Then

                            Dim AcruedAccount As String = tmpAccount.AccruedAccount
                            Dim ValidateAccountAccrued = _CostDistributionRepository.GetMainAccountByNumber(AcruedAccount)
                            Dim IdMainAccount As Integer = ValidateAccountAccrued.Id

                            'DEVENGADO
                            With NewPaidVoucherDetail
                                .IdMainAccount = IdMainAccount
                                .IdThirdParty = thirdPartyId
                                .DebitValue = ObjContractLiquidationDetail.Accrued
                                .CreditValue = 0
                                .Detail = "Pago Generado desde Liquidación de Contrato"
                                If ValidateAccountAccrued IsNot Nothing AndAlso ValidateAccountAccrued.HandlesCostCenter = True Then
                                    .IdCostCenter = Contract.FunctionalUnit.CostCenterId
                                End If
                            End With

                            .JournalVoucherDetails.Add(NewPaidVoucherDetail)

                        Else
                            'DEDUCIDO

                            Dim DeductedAccount As String = tmpAccount.DeductedAccount
                            Dim ValidateAccountDeducted = _CostDistributionRepository.GetMainAccountByNumber(DeductedAccount)
                            Dim IdMainAccount As Integer = ValidateAccountDeducted.Id

                            With NewPaidVoucherDetail
                                .IdMainAccount = IdMainAccount
                                .IdThirdParty = thirdPartyId
                                .DebitValue = 0
                                .CreditValue = ObjContractLiquidationDetail.Accrued
                                .Detail = "Pago Generado desde Liquidación de Contrato"
                                If ValidateAccountDeducted IsNot Nothing AndAlso ValidateAccountDeducted.HandlesCostCenter = True Then
                                    .IdCostCenter = Contract.FunctionalUnit.CostCenterId
                                End If
                            End With

                            .JournalVoucherDetails.Add(NewPaidVoucherDetail)

                        End If

                    Next
                Next

                'Insertamos la Cabecera

                .IdJournalVoucher = group.PayrollParameter.IdPrestacionVoucherType
                .VoucherDate = DateVoucher
                .Detail = "Comprobante Contable de Prestaciones Sociales - Liquidación de Contrato - Empleado: " + Employee.ThirdParty.Nit + " - " + Employee.ThirdParty.Name
                .EntityName = "PayrollLiquidation"
                .IsClosedYear = 0
                .CreationUser = audit.CodeUser
                .CreationDate = Date.Now()
                .Status = 1
            End With

            If JournalVoucherPrestacion.JournalVoucherDetails.Count > 0 Then
                ListJournalVoucher.Add(JournalVoucherPrestacion)
            End If

            ResultObjJournalVouchers.ObjectEmbbeded = ListJournalVoucher
            ResultObjJournalVouchers.StateResult = True

        Catch ex As Exception
            ResultObjJournalVouchers.ObjectEmbbeded = Nothing
            ResultObjJournalVouchers.StateResult = False
            ResultObjJournalVouchers.MessageResult.Add(ex.Message.ToString())
        End Try

        Return ResultObjJournalVouchers
    End Function

    Private Sub LastYearLiquidateUnemployed(employee As Domain.Payroll.Entities.Employee, initialDate As Date, endingDate As Date, ByRef unemployeedTotalPaid As Decimal, ByRef unemployeedTotalInterest As Decimal, ByRef SanctionDays As Integer, ByRef ReplaceFormulate As String, ByRef ConceptFormulate As String, ByRef InitialDateUnemployement As Date, Optional xtraLiquidation As Liquidation = Nothing)

        'Se crea una lista de empleado para poder enviar al servicio de liquidacion de cesantias
        Dim employeeList As New List(Of Domain.Payroll.Entities.Employee)
        employeeList.Add(employee)

        'Contruir Fecha Inicial de Periodo de Cesantias
        Dim InitialDateUnemployeed As Date = New Date(Year(endingDate), 1, 1)
        Dim InitialDateUnemployedPeriod As Date

        'Si la Fecha de Contratación del Empleado es mayor que la Fecha Inicio del Periodo de Cesantias
        If initialDate > InitialDateUnemployeed Then
            InitialDateUnemployedPeriod = initialDate
        Else
            InitialDateUnemployedPeriod = InitialDateUnemployeed
        End If

        Dim alreadyLiquidated = _unemployedLiquidationRepository.GetUnemployedLiquidationEmployeeByDate(employee.Id, InitialDateUnemployedPeriod, endingDate)

        If alreadyLiquidated.Count > 0 Then 'Ya tiene cesantias liquidadas

            Exit Sub

            Dim unemployedAlreadyPaidToSave = alreadyLiquidated.Where(Function(i) i.UnemployedPayDate IsNot Nothing).ToList() 'cesantias ya pagas, no se pueden eliminar

            For Each item As UnemployedLiquidation In unemployedAlreadyPaidToSave
                alreadyLiquidated.Remove(item) 'Se eliminan del listado de cesantias liquidadas
            Next

            _unemployedLiquidationDomain.DeleteListUnemployedLiquidationsWithoutConfirm(alreadyLiquidated) 'Se eliminan las cesantias restantes


            If unemployedAlreadyPaidToSave.Count > 0 Then 'Si tiene cesantias liquidadas y pagas se toma el siguiente periodo como fecha de inicio
                Dim latterPaid = unemployedAlreadyPaidToSave.OrderByDescending(Function(i) i.UnemployedEndingDate).FirstOrDefault 'Se organiza x fecha y  se obtiene la ultima liquidacion paga
                Dim latterPaidEndingDate = CDate(latterPaid.UnemployedEndingDate).AddMonths(1)
                initialDate = New Date(latterPaidEndingDate.Year, latterPaidEndingDate.Month, 1)
            End If

        End If

        Dim ReplaceFormulates As String = ""
        Dim ConceptFormulates As String = ""
        Dim DateTest = New Date(1, 1, 1)

        Dim result = _unemployedLiquidationDomain.YearlyLiquidation(employeeList, InitialDateUnemployedPeriod, endingDate, False, True, False, DateTest, "", "", XtraLiquidation:=xtraLiquidation, liquidationContract:=True, ReplaceFormulate:=ReplaceFormulates, ConceptFormulate:=ConceptFormulates)

        ReplaceFormulate = ReplaceFormulates
        ConceptFormulate = ConceptFormulates

        If result IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
            Dim liquidationUnemployee = result
            If liquidationUnemployee.StateResult = True Then
                For Each liquidated As UnemployedLiquidation In liquidationUnemployee.ObjectEmbbeded

                    For i As Integer = 0 To liquidated.UnemployedLiquidationDetail.Count() - 1
                        SanctionDays = SanctionDays + liquidated.UnemployedLiquidationDetail.Item(i).SanctionsDays
                    Next

                    'Valor de cesantias ya pagado
                    unemployeedTotalPaid = liquidated.TotalUnemployed

                    'Valor de intereses cesantias ya pagado
                    unemployeedTotalInterest = liquidated.UnemployedInterestTotal

                    InitialDateUnemployement = liquidated.UnemployedInitialDate
                Next

            End If
        End If



    End Sub

    Public Function CalculateIndemnizacion(retirementDate As Date, objcontract As Domain.Payroll.Entities.Contract, ByRef SalaryIndemnizacion As Decimal, ByRef AverageValue As Decimal) As Decimal

        Dim IndemnizacionValue As Decimal = 0

        Dim YearsDiference As Integer = DateDiff(DateInterval.Month, objcontract.JobBondingDate, retirementDate)

        If objcontract.ContractType.Undefined = False Then
            'Contrato que no es indefinido
            If objcontract.ContractEndingDate <> retirementDate Then
                Dim DaysDifference As Integer = _liquidationDomain.Days360(DateAdd(DateInterval.Day, 1, retirementDate), objcontract.ContractEndingDate)

                If DaysDifference > 0 Then
                    SalaryIndemnizacion = (objcontract.BasicSalary / 30) * DaysDifference
                End If

            End If

        Else

            AverageValue = CalculateSalaryVariableIndemnization(objcontract, retirementDate)

            'Empleados con Contrato a Término Indefinido
            If YearsDiference <= 12 Then
                'El empleado aún no completa un año, se le paga el salario como Indeminzacion
                SalaryIndemnizacion = objcontract.BasicSalary + AverageValue
            Else
                SalaryIndemnizacion = CalculateUnidefinedValueIndemnizacion(YearsDiference, objcontract, retirementDate, AverageValue)
            End If

        End If

        IndemnizacionValue = SalaryIndemnizacion

        Return IndemnizacionValue

    End Function

    Public Function CalculateUnidefinedValueIndemnizacion(YearsDiference As Integer, ObjContract As Domain.Payroll.Entities.Contract, retirementDate As Date, AverageValue As Decimal) As Decimal

        Dim LegalSalary = ObjContract.Group.PayrollParameter.LegalSalaryMinimum

        Dim MonthCount As Integer = Math.Truncate(YearsDiference / 12)

        Dim DaysCalc As Double = 0
        Dim DaysFirstContract As Integer = 0
        Dim DaysRestContract As Integer = 0

        Dim BasicSalaryAverage = ObjContract.BasicSalary + AverageValue

        If BasicSalaryAverage <= (LegalSalary * 10) Then
            DaysFirstContract = 30
            DaysRestContract = 20
        Else
            DaysFirstContract = 20
            DaysRestContract = 15
        End If

        For i As Integer = 0 To MonthCount

            If i = 0 Then
                'El primer año se pagan 30 días
                DaysCalc = DaysFirstContract
            ElseIf i = MonthCount Then
                'Ultimo año
                Dim LastInitialContract = DateAdd(DateInterval.Year, MonthCount, ObjContract.JobBondingDate)

                Dim DaysDifference = _liquidationDomain.Days360(LastInitialContract, retirementDate)

                Dim PendingDays = Math.Round(((DaysDifference * DaysRestContract) / 360), 1)

                DaysCalc = DaysCalc + PendingDays

            Else
                DaysCalc = DaysCalc + DaysRestContract
            End If

        Next

        Return (BasicSalaryAverage / 30) * DaysCalc

    End Function

    Public Function CalculateSalaryVariableIndemnization(ObjContract As Domain.Payroll.Entities.Contract, retirementDate As Date) As Decimal

        Dim DateSearch As Date
        Dim AverageValue As Decimal = 0

        DateSearch = DateAdd(DateInterval.Month, 12, retirementDate)
        Dim DateSearchEnd As Date

        Dim DateEndMonth = New Date(retirementDate.Year, retirementDate.Month, Date.DaysInMonth(retirementDate.Year, retirementDate.Month))

        If retirementDate.Day = DateEndMonth.Day Then
            DateSearch = New Date(DateAdd(DateInterval.Month, -11, retirementDate).Year, DateAdd(DateInterval.Month, -11, retirementDate).Month, 1)
            DateSearchEnd = retirementDate
        Else
            DateSearchEnd = DateAdd(DateInterval.Month, -1, retirementDate)
            DateSearchEnd = New Date(DateSearchEnd.Year, DateSearchEnd.Month, Date.DaysInMonth(DateSearchEnd.Year, DateSearchEnd.Month))
            DateSearch = New Date(DateAdd(DateInterval.Month, -11, DateSearchEnd).Year, DateAdd(DateInterval.Month, -11, DateSearchEnd).Month, 1)
        End If

        Dim LiquidationDetail = _contractLiquidationRepository.LiquidationEmployeeByDate(ObjContract.EmployeeId, DateSearch, DateSearchEnd)

        Dim AccruedValue As Decimal = 0
        Dim DeductedValue As Decimal = 0

        If LiquidationDetail IsNot Nothing And LiquidationDetail.Count > 0 Then

            For Each objLiquidation As Liquidation In LiquidationDetail

                If objLiquidation.LiquidationDetail.Any(Function(x) x.ConceptClass <> "005" AndAlso x.ConceptClass <> "006" AndAlso x.Concept.AffectIBCVacation = True) Then

                    AccruedValue = AccruedValue + objLiquidation.LiquidationDetail.Where(Function(x) x.ConceptType = 1 And x.ConceptClass <> "005" AndAlso x.ConceptClass <> "006" AndAlso x.Concept.AffectIBCVacation = True).Sum(Function(y) y.ConceptTotalValue)
                    DeductedValue = DeductedValue + objLiquidation.LiquidationDetail.Where(Function(x) x.ConceptType = 2 And x.ConceptClass <> "005" AndAlso x.ConceptClass <> "006" AndAlso x.Concept.AffectIBCVacation = True).Sum(Function(y) y.ConceptTotalValue)

                End If
            Next

        End If

        If Not LiquidationDetail?.Any() Then
            AverageValue = AverageValue
        Else
            AverageValue = (AccruedValue - DeductedValue) / LiquidationDetail.Count
        End If

        Return Math.Round(AverageValue, 2)


    End Function

    ''' <summary>
    ''' Función para Calcular la REtención en la Fuente de las Indemnizaciones
    ''' </summary>
    ''' <param name="ObjContract"></param>
    ''' <param name="IndemnizacionValue"></param>
    ''' <param name="RetirementDate"></param>
    ''' <returns></returns>
    Public Function CalculateRetentionIndemnization(ObjContract As Domain.Payroll.Entities.Contract, IndemnizacionValue As Decimal, RetirementDate As Date) As ActionResult(Of ContractLiquidationDetail)

        Dim ObjResult As New ActionResult(Of ContractLiquidationDetail)
        Try

            Dim ContractLiquidationDetail = New ContractLiquidationDetail
            Dim RetentionIndemnization As Decimal = 0

            If ObjContract.BasicSalary >= (ObjContract.Group.PayrollParameter.UVTValue * 204) Then
                'De acuerdo a lo documentado, se calcula si y solo si el empleado devenga, como salario básico más o igual a 204 UVT. Si es así, el valor de la indemnización se multiplica por el 20%
                RetentionIndemnization = IndemnizacionValue * 0.2
            End If

            If RetentionIndemnization > 0 Then

                Dim ObjConceptIndemnization = _conceptRepository.GetConceptByClass("061")
                If ObjConceptIndemnization Is Nothing Then
                    ObjResult.StateResult = False
                    ObjResult.Message = "No se encontró Concepto creado Concepto de Retención en la Fuente para Indemnizaciones"
                    Return ObjResult
                End If

                ContractLiquidationDetail.IdConcept = ObjConceptIndemnization.Id
                ContractLiquidationDetail.ConceptType = ObjConceptIndemnization.ConceptType
                ContractLiquidationDetail.Description = ObjConceptIndemnization.Name
                ContractLiquidationDetail.Accrued = 0
                ContractLiquidationDetail.Deducted = RetentionIndemnization
                ContractLiquidationDetail.InitialDate = New Date(RetirementDate.Year, RetirementDate.Month, 1)
                ContractLiquidationDetail.EndingDate = RetirementDate
                ContractLiquidationDetail.ConceptFormulate = "[Valor Indemnizacion] * 0.2"
                ContractLiquidationDetail.ReplaceConceptFormulate = RetentionIndemnization.ToString() + " * 0.2"

                ObjResult.ObjectEmbbeded = ContractLiquidationDetail
                ObjResult.StateResult = True
            Else
                ObjResult.ObjectEmbbeded = Nothing
                ObjResult.StateResult = True
            End If

            Return ObjResult

        Catch ex As Exception
            ObjResult.Message = "Ocurrió un error " + ex.Message
            ObjResult.StateResult = False
            Return ObjResult
        End Try

    End Function

    Public Function LiquidateContractByFormulate(employee As Domain.Payroll.Entities.Employee, retirementDate As Date, retirementReasonId As Integer, session As SessionValues) As ActionMessageResult(Of Entities.ContractLiquidation) Implements IContractLiquidationDomain.LiquidateContractByFormulate

        Dim result As New ActionMessageResult(Of ContractLiquidation)()
        result.StateResult = True

        Try
            Dim contractLiquidation As New ContractLiquidation
            Dim PayrollSettings = _payrollSettingsRepository.GetSettingPayroll()

            If PayrollSettings Is Nothing Then
                result.StateResult = False
                result.Message = "No se encontraron Parámetros de Nómina"
                Return result
            End If

            If PayrollSettings.ContractLiquidationTransportValue Is Nothing Then
                result.StateResult = False
                result.Message = "La fórmula de Auxilio de Transporte para la Liquidación de Contrato no está parametrizada"
                Return result
            End If

            If PayrollSettings.ContractLiquidationVacation Is Nothing Then
                result.StateResult = False
                result.Message = "La fórmula de Vacaciones para la Liquidación de Contrato no está parametrizada"
                Return result
            End If

            If PayrollSettings.ContractLiquidationUnemployment Is Nothing Then
                result.StateResult = False
                result.Message = "La fórmula de Cesantias para la Liquidación de Contrato no está parametrizada"
                Return result
            End If

            If PayrollSettings.ContractLiquidationChristmasIncentive Is Nothing Then
                result.StateResult = False
                result.Message = "La fórmula de Prima de Navidad / Diciembre para la Liquidación de Contrato no está parametrizada"
                Return result
            End If

            If PayrollSettings.ContractLiquidationServiceIncentive Is Nothing Then
                result.StateResult = False
                result.Message = "La fórmula de Prima de Servicios / Junio para la Liquidación de Contrato no está parametrizada"
                Return result
            End If

            If PayrollSettings.SeveranceInterestFormula Is Nothing Then
                result.StateResult = False
                result.Message = "La fórmula de Intereses Cesantías para la Liquidación de Contrato no está parametrizada"
                Return result
            End If

            If PayrollSettings.PreNoticeFormula Is Nothing Then
                result.StateResult = False
                result.Message = "La fórmula de Preaviso para la Liquidación de Contrato no está parametrizada"
                Return result
            End If

            If session.IndigoCompanyType = 2 Then
                'Empresa Pública
                If PayrollSettings.ContractLiquidationYearBonification Is Nothing Or PayrollSettings.ContractLiquidationYearBonification = "0" Then
                    result.StateResult = False
                    result.Message = "La fórmula de Bonificación por Año de Servicio para la Liquidación de Contrato no está parametrizada"
                    Return result
                End If

                If PayrollSettings.ContractLiquidationFoodValue Is Nothing Or PayrollSettings.ContractLiquidationFoodValue = "0" Then
                    result.StateResult = False
                    result.Message = "La fórmula de Auxilio de Alimentos para la Liquidación de Contrato no está parametrizada"
                    Return result
                End If

                If PayrollSettings.ContractLiquidationServiceIncentive Is Nothing Or PayrollSettings.ContractLiquidationServiceIncentive = "0" Then
                    result.StateResult = False
                    result.Message = "La fórmula de Prima de Vacaciones para la Liquidación de Contrato no está parametrizada"
                    Return result
                End If

                If PayrollSettings.ContractLiquidationEspecialBonification Or PayrollSettings.ContractLiquidationEspecialBonification = "0" Then
                    result.StateResult = False
                    result.Message = "La fórmula de Bonificación Especial para Recreación para la Liquidación de Contrato no está parametrizada"
                    Return result
                End If

                If PayrollSettings.ContractLiquidationIncreaseVacational Or PayrollSettings.ContractLiquidationIncreaseVacational = "0" Then
                    result.StateResult = False
                    result.Message = "La fórmula de Incremento Vacacional para la Liquidación de Contrato no está parametrizada"
                    Return result
                End If

            End If

            Dim Integral As Boolean = False

            'contrato vigente
            Dim validContract = employee.Contract.Where(Function(i) i.Valid = True).FirstOrDefault

            If validContract.ContractType.SalaryType = 2 Then
                Integral = True
            End If

            If validContract.ContractType.ContractClass = 3 Or validContract.ContractType.ContractClass = 4 Then
                If validContract.FundContract.Any(Function(x) x.FundType = 1) = False Then
                    result.StateResult = False
                    result.Message = "No se encontró Fondo de Salud. Debe agregarle al Empleado primero, antes de Liquidar"
                    Return result
                End If

                If validContract.FundContract.Any(Function(x) x.FundType = 2) = False AndAlso employee.Pensionary = False Then
                    result.StateResult = False
                    result.Message = "No se encontró Fondo de Pensión. Debe agregarle al Empleado primero, antes de Liquidar"
                    Return result
                End If
            End If

            Dim TarifaAprox As Integer = validContract.Group.PayrollParameter.AproximationValue

            validContract.RetirementReasonId = retirementReasonId
            validContract.Status = 2
            'Fecha de vinculacion - Fecha inicial de liquidacion
            Dim jobBondingDate As Date = validContract.JobBondingDate

            'Armamos la cabecera
            contractLiquidation.EmployeeId = validContract.EmployeeId
            contractLiquidation.ContractId = validContract.Id
            contractLiquidation.RetirementDate = retirementDate
            contractLiquidation.RetirementReasonId = retirementReasonId
            contractLiquidation.Status = "C"
            contractLiquidation.Employee = employee
            contractLiquidation.Contract = validContract
            Dim retirementReason = _retirementReasonRepository.GetRetirementReasonById(retirementReasonId)
            contractLiquidation.RetirementReason = retirementReason

            Dim TotalAccrued As Decimal = 0
            Dim TotalDeducted As Decimal = 0

            Dim IBCHealth As Decimal = 0
            Dim IBCPension As Decimal = 0

            Dim xtraLiquidation As Liquidation

            'Fecha minima de liquidacion de nomina pendiente - ajustada según tipo de nómina
            Dim liqMinDate As Date

            ' Calcular fecha inicial según tipo de nómina
            If validContract.Group.Liquidation = 2 AndAlso retirementDate.Day > 15 Then
                ' Nómina quincenal - segunda quincena
                liqMinDate = New Date(retirementDate.Year, retirementDate.Month, 16)
            Else
                ' Nómina mensual o quincenal primera quincena
                liqMinDate = New Date(retirementDate.Year, retirementDate.Month, 1)
            End If

            Dim ListTmpConcept = _conceptRepository.ListAllConcept()

            If (validContract.LastLiquidationDate Is Nothing) Or validContract.LastLiquidationDate < retirementDate Then
                xtraLiquidation = Me.LiquidatePayroll(employee, liqMinDate, retirementDate, session)
                If xtraLiquidation IsNot Nothing Then
                    xtraLiquidation.Contract = validContract
                    xtraLiquidation.Group = validContract.Group

                    Dim messageError = From e In xtraLiquidation.Message
                                       Select e

                    If messageError IsNot Nothing Then
                        If messageError.Count > 0 Then
                            For Each itemMessage As Payroll.Entities.Message In messageError.ToList()
                                If itemMessage.Error = True Then
                                    If itemMessage.Description = "El TOTAL PAGADO está en Negativo. No se puede CONFIRMAR" Then
                                        result.StateResult = True
                                    Else
                                        result.StateResult = False
                                    End If

                                End If
                                result.MessageResult.Add(New MessageResult(itemMessage.Description, itemMessage.Error))
                            Next
                        End If
                    End If

                    For Each detail As LiquidationDetail In xtraLiquidation.LiquidationDetail

                        Dim detailConcept = ListTmpConcept.Where(Function(x) x.Id = detail.ConceptId).FirstOrDefault()

                        detail.ConceptTotalValue = Utils.RoundValue(detail.ConceptTotalValue, TarifaAprox)

                        Dim detailContract As New ContractLiquidationDetail() With {.IdConcept = detail.ConceptId, .ConceptType = detail.ConceptType, .Description = detail.ConceptDetail, .InitialDate = liqMinDate, .EndingDate = retirementDate, .ConceptFormulate = detail.ConceptFormulate, .ReplaceConceptFormulate = detail.ReplaceConceptFormulate}
                        If detail.ConceptType = "1" Then

                            If detailConcept.AffectIBCPension = True Then
                                IBCPension = IBCPension + detail.ConceptTotalValue
                            End If

                            If detailConcept.AffectIBCHealth = True Then
                                IBCHealth = IBCHealth + detail.ConceptTotalValue
                            End If

                            detailContract.Accrued = detail.ConceptTotalValue
                            TotalAccrued = TotalAccrued + detail.ConceptTotalValue
                        ElseIf detail.ConceptType = "2" Then

                            If detail.ConceptClass = "017" Or detail.ConceptClass = "014" Or detail.ConceptClass = "038" Or detail.ConceptClass = "019" Then
                                Continue For
                            End If

                            If detailConcept.AffectIBCPension = True Then
                                IBCPension = IBCPension - detail.ConceptTotalValue
                            End If

                            If detailConcept.AffectIBCHealth = True Then
                                IBCHealth = IBCHealth - detail.ConceptTotalValue
                            End If

                            detailContract.Deducted = detail.ConceptTotalValue
                            TotalDeducted = TotalDeducted + detail.ConceptTotalValue
                        Else
                            detailContract.Accrued = detail.ConceptTotalValue
                        End If
                        contractLiquidation.ContractLiquidationDetail.Add(detailContract)
                    Next
                End If
            Else 'Nomina ya liquidada

                Dim ObjGroup = _groupRepository.GetGroupById(validContract.GroupId)

                'Calcular el período correcto de liquidación según tipo de nómina y fecha de retiro
                Dim InitialRetirementDate As Date
                Dim EndRetirementDate As Date
                GetPayrollPeriodDates(retirementDate, validContract.Group.Liquidation, InitialRetirementDate, EndRetirementDate)

                If validContract.ContractEndingDate > retirementDate Then 'Se evalua si la fecha de terminacion del contrato es mayor a la fecha de retiro

                    Dim ListAdjustSalaryValue = AdjustBasicSalaryCR(validContract.BasicSalary, retirementDate, InitialRetirementDate, EndRetirementDate, ObjGroup, validContract.Id)

                    If ListAdjustSalaryValue.StateResult Then

                        If ListAdjustSalaryValue.ObjectEmbbeded?.Any() Then
                            For Each objContractDetail As ContractLiquidationDetail In ListAdjustSalaryValue.ObjectEmbbeded
                                objContractDetail.Accrued = Utils.RoundValue(objContractDetail.Accrued, TarifaAprox)
                                objContractDetail.Deducted = Utils.RoundValue(objContractDetail.Deducted, TarifaAprox)
                                contractLiquidation.ContractLiquidationDetail.Add(objContractDetail)
                            Next
                        End If
                    Else
                        If ListAdjustSalaryValue.MessageResult.Any() Then
                            result.StateResult = False
                            result.MessageResult = ListAdjustSalaryValue.MessageResult
                            Return result
                        End If

                    End If

                End If
            End If

            'Cargo los Conceptos Manuales del Empleado
            Dim ListManualConcepts = _ManualConceptsRepository.GetManualConceptsByEmployeeIdInitialDate(employee.Id, retirementDate, 1, 5)

            'Conceptos Manuales
            If ListManualConcepts IsNot Nothing AndAlso ListManualConcepts.Count > 0 Then
                For Each objManualConcept As ManualConcepts In ListManualConcepts
                    Dim DetailLiquidaionContract As New ContractLiquidationDetail()
                    If objManualConcept.Concept.ConceptType = 1 Then
                        DetailLiquidaionContract = New ContractLiquidationDetail() With {.IdConcept = objManualConcept.Concept.Id, .ConceptType = objManualConcept.Concept.ConceptType, .Description = objManualConcept.Concept.Name, .InitialDate = liqMinDate, .EndingDate = retirementDate, .ConceptFormulate = objManualConcept.Concept.Formulates, .ReplaceConceptFormulate = objManualConcept.QuoteValue.ToString(), .Accrued = objManualConcept.QuoteValue, .Deducted = 0}
                        TotalAccrued = TotalAccrued + objManualConcept.QuoteValue
                    ElseIf objManualConcept.Concept.ConceptType = 2 Then
                        DetailLiquidaionContract = New ContractLiquidationDetail() With {.IdConcept = objManualConcept.Concept.Id, .ConceptType = objManualConcept.Concept.ConceptType, .Description = objManualConcept.Concept.Name, .InitialDate = liqMinDate, .EndingDate = retirementDate, .ConceptFormulate = objManualConcept.Concept.Formulates, .ReplaceConceptFormulate = objManualConcept.QuoteValue.ToString(), .Accrued = 0, .Deducted = objManualConcept.QuoteValue}
                        TotalDeducted = TotalDeducted + objManualConcept.QuoteValue
                    End If
                Next
            End If

            'Cargo los Convenios que tenga el empleado
            Dim ListAgreements = _AgreementsCRepository.GetAgreementsByEmployeeLiquidationContract(validContract.EmployeeId, "2")

            Dim ListAuthorizationConcept = _authorizationConceptRepository.GetAuthorizationConceptByGroupId(validContract.GroupId)

            Dim ListObjConcept As New List(Of Domain.Payroll.Entities.Concept)

            For Each objTmpConcept As Domain.Payroll.Entities.AuthorizationConcept In ListAuthorizationConcept

                If objTmpConcept.Concept.ConceptClass = "006" And objTmpConcept.Concept.ConceptType = 1 And ListObjConcept.Any(Function(x) x.Id = objTmpConcept.ConceptId) = False Then
                    'Auxilio de Transporte
                    ListObjConcept.Add(objTmpConcept.Concept)
                End If

                If objTmpConcept.Concept.ConceptClass = "062" And objTmpConcept.Concept.ConceptType = 1 And ListObjConcept.Any(Function(x) x.Id = objTmpConcept.ConceptId) = False Then
                    'Auxilio de Alimentos
                    ListObjConcept.Add(objTmpConcept.Concept)
                End If

                If objTmpConcept.Concept.ConceptClass = "017" And ListObjConcept.Any(Function(x) x.Id = objTmpConcept.ConceptId) = False Then
                    'Aporte Salud
                    objTmpConcept.Concept.Formulates = "[IBC Salud] * [% Salud Empleado] / 100"
                    ListObjConcept.Add(objTmpConcept.Concept)
                End If

                If objTmpConcept.Concept.ConceptClass = "014" And ListObjConcept.Any(Function(x) x.Id = objTmpConcept.ConceptId) = False Then
                    'Aporte Pensión
                    objTmpConcept.Concept.Formulates = "[IBC Pension] * [% Pension Empleado] / 100"
                    ListObjConcept.Add(objTmpConcept.Concept)
                End If

                If objTmpConcept.Concept.ConceptClass = "046" And objTmpConcept.Concept.ConceptType = 1 And ListObjConcept.Any(Function(x) x.Id = objTmpConcept.ConceptId) = False Then
                    'Bonificación por Año de Servicio
                    ListObjConcept.Add(objTmpConcept.Concept)
                End If

                If objTmpConcept.Concept.ConceptClass = "049" And objTmpConcept.Concept.ConceptType = 1 And ListObjConcept.Any(Function(x) x.Id = objTmpConcept.ConceptId) = False Then
                    'Prima de Vacaciones
                    ListObjConcept.Add(objTmpConcept.Concept)
                End If

                If objTmpConcept.Concept.ConceptClass = "020" And objTmpConcept.Concept.ConceptType = 2 And ListObjConcept.Any(Function(x) x.Id = objTmpConcept.ConceptId) = False Then
                    'Retención en la Fuente
                    ListObjConcept.Add(objTmpConcept.Concept)
                End If

                If objTmpConcept.Concept.ConceptClass = "007" And objTmpConcept.Concept.ConceptType = 1 And ListObjConcept.Any(Function(x) x.Id = objTmpConcept.ConceptId) = False Then
                    'Indemnizaciones
                    ListObjConcept.Add(objTmpConcept.Concept)
                End If

                If objTmpConcept.Concept.ConceptClass = "061" And objTmpConcept.Concept.ConceptType = 2 And ListObjConcept.Any(Function(x) x.Id = objTmpConcept.ConceptId) = False Then
                    'Retención en la Fuente por Indemnizaciones
                    ListObjConcept.Add(objTmpConcept.Concept)
                End If

                If retirementReason.Vacations Then
                    If objTmpConcept.Concept.Id = PayrollSettings.IdVacationConcept Then
                        'Vacaciones
                        objTmpConcept.Concept.Formulates = PayrollSettings.ContractLiquidationVacation
                        ListObjConcept.Add(objTmpConcept.Concept)
                    End If
                End If

                If objTmpConcept.Concept.ConceptClass = "063" And objTmpConcept.Concept.ConceptType = 1 And ListObjConcept.Any(Function(x) x.Id = objTmpConcept.ConceptId) = False Then
                    'Bonificación Especial para Recreación
                    ListObjConcept.Add(objTmpConcept.Concept)
                End If

                If objTmpConcept.Concept.ConceptClass = "064" And objTmpConcept.Concept.ConceptType = 1 And ListObjConcept.Any(Function(x) x.Id = objTmpConcept.ConceptId) = False Then
                    'Incremento Vacacional
                    ListObjConcept.Add(objTmpConcept.Concept)
                End If

                'Preaviso
                If retirementReason.Prenotice AndAlso PayrollSettings.PreNoticeSettlementConceptId.HasValue Then
                    Dim prenoticeConcept = ListTmpConcept.Where(Function(x) x.Id = PayrollSettings.PreNoticeSettlementConceptId).FirstOrDefault()
                    If prenoticeConcept IsNot Nothing Then
                        prenoticeConcept.Formulates = PayrollSettings.PreNoticeFormula
                        If ListObjConcept.Any(Function(x) x.Id = prenoticeConcept.Id) = False Then
                            ListObjConcept.Add(prenoticeConcept)
                        End If
                    End If
                End If

                'Cesantias
                If retirementReason.Severance AndAlso PayrollSettings.IdConceptUnemployment.HasValue Then
                    Dim ObjConceptUnemployment = ListTmpConcept.Where(Function(x) x.Id = PayrollSettings.IdConceptUnemployment).FirstOrDefault()
                    If ObjConceptUnemployment IsNot Nothing Then
                        ObjConceptUnemployment.Formulates = PayrollSettings.ContractLiquidationUnemployment
                        If ListObjConcept.Any(Function(x) x.Id = ObjConceptUnemployment.Id) = False Then
                            ListObjConcept.Add(ObjConceptUnemployment)
                        End If
                    End If
                End If

                'Intereses de Cesantias
                If retirementReason.SeveranceInterest AndAlso PayrollSettings.IdConceptInterestUnemployment.HasValue Then
                    Dim ObjConceptInterestUnemployment = ListTmpConcept.Where(Function(x) x.Id = PayrollSettings.IdConceptInterestUnemployment).FirstOrDefault()
                    If ObjConceptInterestUnemployment IsNot Nothing Then
                        ObjConceptInterestUnemployment.Formulates = PayrollSettings.SeveranceInterestFormula
                        If ListObjConcept.Any(Function(x) x.Id = ObjConceptInterestUnemployment.Id) = False Then
                            ListObjConcept.Add(ObjConceptInterestUnemployment)
                        End If
                    End If
                End If

                If retirementReason.Bonus Then
                    'Primas de Diciembre
                    If PayrollSettings.ChristmasIncentivePaymentConceptId = PayrollSettings.ServicesIncentivePaymentConceptId Then
                        If ListObjConcept.Any(Function(x) x.Id = PayrollSettings.ChristmasIncentivePaymentConceptId) = False Then
                            Dim ObjConceptDecember As Domain.Payroll.Entities.Concept = ListTmpConcept.Where(Function(x) x.Id = PayrollSettings.ChristmasIncentivePaymentConceptId).FirstOrDefault()
                            ListObjConcept.Add(ObjConceptDecember)
                        End If

                    Else
                        Dim ObjConceptDecemberIncentivePayment = ListTmpConcept.FirstOrDefault(Function(x) x.Id = PayrollSettings.ChristmasIncentivePaymentConceptId)
                        If ObjConceptDecemberIncentivePayment IsNot Nothing Then
                            ObjConceptDecemberIncentivePayment.Formulates = PayrollSettings.ContractLiquidationChristmasIncentive
                            If ListObjConcept.Any(Function(x) x.Id = PayrollSettings.ChristmasIncentivePaymentConceptId And x.Formulates = PayrollSettings.ContractLiquidationChristmasIncentive) = False Then
                                ListObjConcept.Add(ObjConceptDecemberIncentivePayment)
                            End If
                        End If

                        'Primas de Junio
                        Dim ObjConceptJuneIncentivePayment = ListTmpConcept.Where(Function(x) x.Id = PayrollSettings.ServicesIncentivePaymentConceptId).FirstOrDefault()
                        If ObjConceptJuneIncentivePayment IsNot Nothing Then
                            ObjConceptJuneIncentivePayment.Formulates = PayrollSettings.ContractLiquidationServiceIncentive
                            If ListObjConcept.Any(Function(x) x.Id = PayrollSettings.ServicesIncentivePaymentConceptId And x.Formulates = PayrollSettings.ContractLiquidationServiceIncentive) = False Then
                                ListObjConcept.Add(ObjConceptJuneIncentivePayment)
                            End If
                        End If
                    End If
                End If
            Next

            'Nómina
            'Variables Necesarias para cálculo
            Dim BasicSalary As Decimal = validContract.BasicSalary
            Dim TransportHelpValue As Decimal = TransportValue(validContract)
            Dim WorkedDays As Integer = 0
            Dim LiquidatePayroll As Boolean = False
            Dim LegalMinimunSalary As Decimal = validContract.Group.PayrollParameter.LegalSalaryMinimum

            'Variables Primas Junio
            Dim IncentivePaymentDays As Integer = Me.IncentivePaymentDays(validContract, PayrollSettings, 1, retirementDate)
            Dim SanctionDaysServiceIncentivePayment As Integer = Me.SanctionDays(validContract.EmployeeId, PayrollSettings.InitialDateServicesIncentivePayment, retirementDate)
            Dim UnpaidLicensesDaysServiceIncentivePayment As Integer = Me.UnpaidLicensesDays(validContract.EmployeeId, PayrollSettings.InitialDateServicesIncentivePayment, retirementDate)
            Dim VariableSalaryServiceIncentive As Decimal = Me.VariableSalaryIncentivePayment(validContract, PayrollSettings, 1, Nothing, retirementDate)
            Dim ValueServiceIncentivePayment As Decimal = 0

            Dim salaryServiceIncentiveAguinaldo = VariableSalaryServiceIncentiveAguinaldo(validContract, PayrollSettings, retirementDate, xtraLiquidation) 'VariableSalaryServiceIncentive
            'Valido si la empresa es privada, y la fecha de retiro es en el segundo semestre del año, no se debe pagar esta prima
            If session.IndigoCompanyType = 1 And retirementDate >= PayrollSettings.EndDateServicesIncentivePayment Then
                Dim ObjConcept = ListObjConcept.Where(Function(x) x.Id = PayrollSettings.ChristmasIncentivePaymentConceptId).FirstOrDefault()

                If ObjConcept IsNot Nothing Then
                    ObjConcept.Formulates = PayrollSettings.ContractLiquidationChristmasIncentive
                End If

                IncentivePaymentDays = 0
                SanctionDaysServiceIncentivePayment = 0
                UnpaidLicensesDaysServiceIncentivePayment = 0
                VariableSalaryServiceIncentive = 0
            End If

            'Variables Primas Diciembre
            Dim IncentiveDecemberDays As Integer = Me.IncentivePaymentDays(validContract, PayrollSettings, 2, retirementDate)
            Dim SanctionDaysDecemberIncentivePayment As Integer = Me.SanctionDays(validContract.EmployeeId, PayrollSettings.InitialDateChristmasIncentivePayment, retirementDate)
            Dim UnpaidLicensesDaysDecemberIncentivePayment As Integer = Me.UnpaidLicensesDays(validContract.EmployeeId, PayrollSettings.InitialDateChristmasIncentivePayment, retirementDate)
            Dim VariableSalaryDecemberIncentive As Decimal = Me.VariableSalaryIncentivePayment(validContract, PayrollSettings, 2, Nothing, retirementDate)
            Dim ValueDecemberIncentivePayment As Decimal = 0

            'Valido si la empresa es privada, y la fecha de retiro es en el primer semestre del año, no se debe pagar esta prima
            If session.IndigoCompanyType = 1 And retirementDate < PayrollSettings.InitialDateChristmasIncentivePayment Then

                Dim ObjConcept = ListObjConcept.Where(Function(x) x.Id = PayrollSettings.ServicesIncentivePaymentConceptId).FirstOrDefault()

                If ObjConcept IsNot Nothing Then
                    ObjConcept.Formulates = PayrollSettings.ContractLiquidationServiceIncentive
                End If

                IncentiveDecemberDays = 0
                SanctionDaysDecemberIncentivePayment = 0
                UnpaidLicensesDaysDecemberIncentivePayment = 0
                VariableSalaryDecemberIncentive = 0
            End If

            'Realizo validaciones y consultas por proceso:
            'Cesantias e Intereses
            Dim UnemploymentDays As Integer = Me.UnemploymentDays(validContract, retirementDate)
            Dim SanctionDaysUnemployment As Integer = Me.SanctionDays(validContract.EmployeeId, New Date(retirementDate.Year, 1, 1), retirementDate)
            Dim UnpaidLicensesDaysUnemployment As Integer = Me.UnpaidLicensesDays(validContract.EmployeeId, New Date(retirementDate.Year, 1, 1), retirementDate)
            Dim VariableSalaryUnemployment As Decimal = Me.VariableSalaryUnemployment(validContract, Nothing, retirementDate)
            Dim ValueUnemployment As Decimal = 0

            'Vacaciones
            Dim VacationDays As Integer = Me.VacationDays(validContract, retirementDate)
            Dim vacationDaysRetirement = CalculateVacationDaysRetirement(validContract, retirementDate)
            Dim SanctionDaysVacation As Integer = Me.SanctionDays(validContract.EmployeeId, validContract.Employee.VacationLastDateLiquidation, retirementDate)
            Dim UnpaidLicensesDaysVacation As Integer = Me.UnpaidLicensesDays(validContract.EmployeeId, validContract.Employee.VacationLastDateLiquidation, retirementDate)
            Dim VariableSalaryVacation As Decimal = Me.VariableSalaryVacation(validContract, Nothing, retirementDate)
            Dim baseAverageValueCIMA As Decimal = CalculateBaseAverageValueCIMA(validContract, retirementDate)
            Dim accumulatedValueAguinaldoBase As Decimal = CalculateAccumulatedValueAguinaldoBase(validContract, PayrollSettings, retirementDate)
            'Indemnizaciones
            Dim IndemnizationValue As Decimal = 0

            'Retención en la Fuente por Indemnizaciones
            Dim RetentionValueIndemnization As Decimal = 0

            If retirementReason.Compensation = True Then

                Dim AuthorizationConcept = _conceptRepository.GetConceptByClass("007")

                If AuthorizationConcept Is Nothing Then
                    result.StateResult = False
                    result.Message = "No se encontró concepto para el Pago de la Indemnización"
                    Return result
                End If

                Dim AverageSalary As Decimal = 0
                Dim SalaryIndemnization As Decimal = 0

                IndemnizationValue = CalculateIndemnizacion(retirementDate, validContract, SalaryIndemnization, AverageSalary)
            End If

            'Retención en la Fuente por Indemnización
            If IndemnizationValue > 0 Then

                Dim ObjContractLiquidationDetailIndemnization = Me.CalculateRetentionIndemnization(validContract, IndemnizationValue, retirementDate)

                If ObjContractLiquidationDetailIndemnization.StateResult = True And ObjContractLiquidationDetailIndemnization.ObjectEmbbeded IsNot Nothing Then
                    contractLiquidation.ContractLiquidationDetail.Add(ObjContractLiquidationDetailIndemnization.ObjectEmbbeded)
                ElseIf ObjContractLiquidationDetailIndemnization.StateResult = False Then
                    result.Message = ObjContractLiquidationDetailIndemnization.Message.ToString
                    result.StateResult = False
                End If

            End If

            'Retención en la Fuente
            Dim RetentionValue As Decimal = 0
            'Ahora debo recorrer el Listado de Conceptos para agregarlos al detalle de la Liquidación

            For Each ObjConcept In ListObjConcept
                Dim res = Me.ReplaceDataFormulates(
                    New DataFormulateModel With {
                        .FormulaConcept = ObjConcept.Formulates,
                        .LegalMinumunSalary = LegalMinimunSalary,
                        .TransportHealthValue = TransportHelpValue,
                        .BasicSalary = BasicSalary,
                        .WorkedDays = 30,
                        .IncentivePaymentDays = IncentivePaymentDays,
                        .SanctionDaysServiceIncentivePayment = SanctionDaysServiceIncentivePayment,
                        .UnpaidLicensesDaysServiceIncentivePayment = UnpaidLicensesDaysServiceIncentivePayment,
                        .VariableSalaryServiceIncentive = VariableSalaryServiceIncentive,
                        .IncentiveDecemberDays = IncentiveDecemberDays,
                        .SanctionDaysDecemberIncentivePayment = SanctionDaysDecemberIncentivePayment,
                        .UnpaidLicensesDaysDecemberIncentivePayment = UnpaidLicensesDaysDecemberIncentivePayment,
                        .VariableSalaryDecemberIncentive = VariableSalaryDecemberIncentive,
                        .UnemploymentDays = UnemploymentDays,
                        .SanctionDaysUnemployment = SanctionDaysUnemployment,
                        .UnpaidLicensesDaysUnemployment = UnpaidLicensesDaysUnemployment,
                        .VariableSalaryUnemployment = VariableSalaryUnemployment,
                        .VacationDays = VacationDays,
                        .VacationDaysRetirement = vacationDaysRetirement,
                        .SanctionDaysVacation = SanctionDaysVacation,
                        .UnpaidLicensesDaysVacation = UnpaidLicensesDaysVacation,
                        .VariableSalaryVacation = VariableSalaryVacation,
                        .IBCHealth = IBCHealth,
                        .IBCPension = IBCPension,
                        .EmployeePensionPercentage = validContract.Group.PayrollParameter.EmployeePensionContributionPercentage,
                        .EmployeeHealthPercentage = validContract.Group.PayrollParameter.EmployeeHealthContributionPercentage,
                        .IndemnizationValue = IndemnizationValue,
                        .SalaryServiceIncentiveAguinaldo = salaryServiceIncentiveAguinaldo,
                        .BaseAverageValueCIMA = baseAverageValueCIMA,
                        .RetirementDate = retirementDate,
                        .AccumulatedValueAguinaldoBase = accumulatedValueAguinaldoBase,
                        .IngressDate = validContract.ContractInitialDate,
                        .JobBondingDate = validContract.JobBondingDate
                    }
                )

                Dim valueConcept = res.Item2
                Dim replaceFormulate = res.Item1

                If valueConcept > 0 Then
                    valueConcept = Utils.RoundValue(valueConcept, TarifaAprox)

                    Dim detailLiquidaionContract As New ContractLiquidationDetail With {
                        .IdConcept = ObjConcept.Id,
                        .ConceptType = ObjConcept.ConceptType,
                        .Description = ObjConcept.Name,
                        .InitialDate = liqMinDate,
                        .EndingDate = retirementDate,
                        .ConceptFormulate = ObjConcept.Formulates,
                        .ReplaceConceptFormulate = replaceFormulate,
                        .Accrued = 0,
                        .Deducted = 0
                    }

                    If {
                        PayrollSettings.PreNoticeSettlementConceptId,
                        PayrollSettings.IdConceptUnemployment,
                        PayrollSettings.IdConceptInterestUnemployment,
                        PayrollSettings.IdVacationConcept}.Contains(ObjConcept.Id) Then
                        detailLiquidaionContract.InitialDate = validContract.JobBondingDate
                    ElseIf {PayrollSettings.ServicesIncentivePaymentConceptId, PayrollSettings.ChristmasIncentivePaymentConceptId}.Contains(ObjConcept.Id) Then
                        If ObjConcept.Formulates = PayrollSettings.ChristmasIncentivePayment Then
                            detailLiquidaionContract.InitialDate = PayrollSettings.InitialDateChristmasIncentivePayment
                        Else
                            detailLiquidaionContract.InitialDate = PayrollSettings.InitialDateServicesIncentivePayment
                        End If
                    End If

                    If ObjConcept.ConceptType = 1 Then
                        detailLiquidaionContract.Accrued = valueConcept
                        TotalAccrued = TotalAccrued + valueConcept
                    ElseIf ObjConcept.ConceptType = 2 Then
                        detailLiquidaionContract.Deducted = valueConcept
                        TotalDeducted = TotalDeducted + valueConcept
                    End If

                    contractLiquidation.ContractLiquidationDetail.Add(detailLiquidaionContract)
                End If

            Next

            contractLiquidation.TotalAccrued = TotalAccrued
            contractLiquidation.TotalDeducted = TotalDeducted
            contractLiquidation.TotalPaid = TotalAccrued - TotalDeducted

            result.ObjectEmbbeded = contractLiquidation
            result.StateResult = True

        Catch ex As Exception
            result.StateResult = False
            result.Message = ex.Message.ToString()
        End Try

        Return result

    End Function

    ''' <summary>
    ''' Calcula el valor de la variable Valor Acumulado Base Aguinaldo
    ''' </summary>
    ''' <param name="contract"></param>
    ''' <param name="payrollSettings"></param>
    ''' <param name="retirementDate"></param>
    ''' <returns></returns>
    Private Function CalculateAccumulatedValueAguinaldoBase(contract As Entities.Contract, payrollSettings As PayrollSettings, retirementDate As Date) As Decimal
        Dim initialDate = payrollSettings.InitialDateServicesIncentivePayment
        Dim variableSalary As Decimal = 0
        Dim endDate As Date = New Date(retirementDate.Year, retirementDate.Month,
                                    Date.DaysInMonth(retirementDate.Year, retirementDate.Month))
        Dim liquidations = _payrollLiquidationRepository.LiquidationEmployeeByDateAndContractStatus(contract.EmployeeId, initialDate, endDate, {CByte(1), CByte(4), CByte(5)}.ToList())
        For Each liquidation In liquidations
            Dim csum = liquidation.LiquidationDetail.Where(Function(m) m.ConceptType = 1 _
                        AndAlso m.Concept.AffectIBCIncentivePayment).Sum(Function(m) m.AccruedValue)
            variableSalary = variableSalary + csum
        Next

        Return variableSalary
    End Function

    ''' <summary>
    ''' Calcula el valor de la variable "Valor Promedio Base CIMA"
    ''' </summary>
    ''' <param name="contract"></param>
    ''' <param name="retirementDate"></param>
    ''' <returns></returns>
    Private Function CalculateBaseAverageValueCIMA(contract As Entities.Contract, retirementDate As Date) As Decimal
        Dim variableSalary As Decimal = 0
        ' Le sumamos 1 día y luego lo posicionamos a inicio del mes
        ' de esta manera sabemos que si es el último día del mes mas 1 entonces toma el mes completo
        ' sino al sumarle un día igual ignora el mes
        Dim flagDate = retirementDate.Date.AddDays(1)
        Dim endDate = New Date(flagDate.Year, flagDate.Month, 1).AddDays(-1)
        Dim validMonths = 0
        While validMonths < 6
            Dim startDate = New Date(endDate.Year, endDate.Month, 1)
            Dim liquidations = _payrollLiquidationRepository.LiquidationEmployeeByDateAndContractStatus(contract.EmployeeId, startDate, endDate, {CByte(1), CByte(4), CByte(5)}.ToList())
            If liquidations.Any() Then
                Dim isValidMonth = True
                For Each liquidation In liquidations
                    If liquidation.LiquidationDetail.Any(Function(m) {"021", "022", "027"}.Contains(m.ConceptClass)) Then
                        isValidMonth = False
                        Exit For
                    End If
                    Dim csum = liquidation.LiquidationDetail.Where(Function(m) m.ConceptType = 1 _
                        AndAlso m.Concept.AffectIBC).Sum(Function(m) m.AccruedValue)
                    variableSalary = variableSalary + csum
                Next

                If isValidMonth Then
                    validMonths += 1
                End If
            Else
                Exit While
            End If

            endDate = startDate.AddDays(-1)
        End While

        If validMonths = 0 Then
            Return 0
        End If

        Return variableSalary / validMonths
    End Function

    Public Function ReplaceDataFormulates(
        dataFormulate As DataFormulateModel
    ) As (String, Decimal)

        Dim replaceFormulate = dataFormulate.FormulaConcept.Replace("[Salario Mínimo]", Format(dataFormulate.LegalMinumunSalary, "0.00").Replace(",", ".")) _
            .Replace("[Auxilio Transporte]", Format(dataFormulate.TransportHealthValue, "0.00").Replace(",", ".")) _
            .Replace("[Sueldo Contrato]", Format(dataFormulate.BasicSalary, "0.00").Replace(",", ".")) _
            .Replace("[Dias Trabajados]", Format(dataFormulate.WorkedDays, "0.00").Replace(",", ".")) _
            .Replace("[Dias Primas Servicios]", Format(dataFormulate.IncentivePaymentDays, "0.00").Replace(",", ".")) _
            .Replace("[Dias Sancion Primas Servicios]", Format(dataFormulate.SanctionDaysServiceIncentivePayment, "0.00").Replace(",", ".")) _
            .Replace("[Dias Licencias No Remueradas Primas Servicios]", Format(dataFormulate.UnpaidLicensesDaysServiceIncentivePayment, "0.00").Replace(",", ".")) _
            .Replace("[Salario Variable Prima Servicios]", Format(dataFormulate.VariableSalaryServiceIncentive, "0.00").Replace(",", ".")) _
            .Replace("[Dias Primas Diciembre]", Format(dataFormulate.IncentiveDecemberDays, "0.00").Replace(",", ".")) _
            .Replace("[Dias Sancion Primas Diciembre]", Format(dataFormulate.SanctionDaysDecemberIncentivePayment, "0.00").Replace(",", ".")) _
            .Replace("[Dias Licencias No Remueradas Primas Diciembre]", Format(dataFormulate.UnpaidLicensesDaysDecemberIncentivePayment, "0.00").Replace(",", ".")) _
            .Replace("[Salario Variable Prima Diciembre]", Format(dataFormulate.VariableSalaryDecemberIncentive, "0.00").Replace(",", ".")) _
            .Replace("[Dias Cesantias]", Format(dataFormulate.UnemploymentDays, "0.00").Replace(",", ".")) _
            .Replace("[Dias Sancion Cesantias]", Format(dataFormulate.SanctionDaysUnemployment, "0.00").Replace(",", ".")) _
            .Replace("[Dias Licencias No Remueradas Cesantias]", Format(dataFormulate.UnpaidLicensesDaysUnemployment, "0.00").Replace(",", ".")) _
            .Replace("[Salario Variable Cesantias]", Format(dataFormulate.VariableSalaryUnemployment, "0.00").Replace(",", ".")) _
            .Replace("[Dias Vacaciones]", Format(dataFormulate.VacationDays, "0.00").Replace(",", ".")) _
            .Replace("[Dias Vacaciones Retiro]", Format(dataFormulate.VacationDaysRetirement, "0.00").Replace(",", ".")) _
            .Replace("[Dias Sancion Vacaciones]", Format(dataFormulate.SanctionDaysVacation, "0.00").Replace(",", ".")) _
            .Replace("[Dias Licencias No Remueradas Vacaciones]", Format(dataFormulate.UnpaidLicensesDaysVacation, "0.00").Replace(",", ".")) _
            .Replace("[Salario Variable Vacaciones]", Format(dataFormulate.VariableSalaryVacation, "0.00").Replace(",", ".")) _
            .Replace("[IBC Salud]", Format(dataFormulate.IBCHealth, "0.00").Replace(",", ".")) _
            .Replace("[IBC Pension]", Format(dataFormulate.IBCPension, "0.00").Replace(",", ".")) _
            .Replace("[% Salud Empleado]", Format(dataFormulate.EmployeeHealthPercentage, "0.00").Replace(",", ".")) _
            .Replace("[% Pension Empleado]", Format(dataFormulate.EmployeePensionPercentage, "0.00").Replace(",", ".")) _
            .Replace("[Valor Indemnizacion]", Format(dataFormulate.IndemnizationValue, "0.00").Replace(",", ".")) _
            .Replace("[Valor Acumulado Aguinaldo Retiro]", Format(dataFormulate.SalaryServiceIncentiveAguinaldo, "0.00").Replace(",", ".")) _
            .Replace("[Valor Promedio Base CIMA]", Format(dataFormulate.BaseAverageValueCIMA, "0.00").Replace(",", ".")) _
            .Replace("[Fecha Retiro]", $"'{Format(dataFormulate.RetirementDate, "dd/MM/yyyy")}'") _
            .Replace("[Valor Acumulado Base Aguinaldo]", Format(dataFormulate.AccumulatedValueAguinaldoBase, "0.00").Replace(",", ".")) _
            .Replace("[Fecha Ingreso]", $"'{Format(dataFormulate.IngressDate, "dd/MM/yyyy")}'") _
            .Replace("[Fecha Contratacion]", $"'{Format(dataFormulate.JobBondingDate, "dd/MM/yyyy")}'")

        Dim result = Utils.EvalExpression(replaceFormulate)
        Return (replaceFormulate, IIf(result.StateResult, CType(result.ObjectEmbbeded, Decimal), 0))
    End Function

    Private Function TransportValue(Contract As Domain.Payroll.Entities.Contract) As Decimal
        Dim VarTransportValue = 0

        If Contract.BasicSalary <= (2 * Contract.Group.PayrollParameter.LegalSalaryMinimum) Then
            VarTransportValue = Contract.Group.PayrollParameter.TransportHelpValue
        End If

        Return VarTransportValue

    End Function

    ''' <summary>
    ''' Primas de Diciembre / Navidad
    ''' </summary>
    ''' <param name="Contract"></param>
    ''' <returns></returns>
    Private Function VariableSalaryIncentivePayment(Contract As Entities.Contract, PayrollSettings As PayrollSettings, IncentivePaymentType As Integer, ActualLiquidation As Liquidation, RetirementDate As Date) As Decimal

        Dim VariableSalary As Decimal = 0
        Dim InitialDate As Date

        If IncentivePaymentType = 1 Then
            InitialDate = PayrollSettings.InitialDateServicesIncentivePayment
        Else
            InitialDate = PayrollSettings.InitialDateChristmasIncentivePayment
        End If

        Dim ListLiquidation As List(Of Liquidation) = _payrollLiquidationRepository.LiquidationEmployeeByDate(Contract.EmployeeId, InitialDate, RetirementDate)

        If ListLiquidation.Any() Then

            If ActualLiquidation IsNot Nothing AndAlso ActualLiquidation.Id > 0 Then
                ListLiquidation.Add(ActualLiquidation)
            End If

            For Each ObjLiquidation As Liquidation In ListLiquidation

                For Each ObjLiquidationDetail As LiquidationDetail In ObjLiquidation.LiquidationDetail

                    If ObjLiquidationDetail.ConceptClass <> "005" Or ObjLiquidationDetail.ConceptClass <> "006" Then
                        If ObjLiquidationDetail.Concept.AffectIBCIncentivePayment = True Then
                            If ObjLiquidationDetail.Concept.ConceptType = 1 Then
                                VariableSalary = VariableSalary + ObjLiquidationDetail.ConceptTotalValue
                            ElseIf ObjLiquidationDetail.Concept.ConceptType = 2 Then
                                VariableSalary = VariableSalary - ObjLiquidationDetail.ConceptTotalValue
                            End If
                        End If
                    End If
                Next
            Next
        End If

        Return VariableSalary

    End Function

    Private Function VariableSalaryServiceIncentiveAguinaldo(Contract As Entities.Contract, PayrollSettings As PayrollSettings, RetirementDate As Date, pendingLiquidation As Liquidation) As Decimal
        Dim initialDate = PayrollSettings.InitialDateServicesIncentivePayment
        Dim endDate As Date = New Date(RetirementDate.Year, RetirementDate.Month,
                                    Date.DaysInMonth(RetirementDate.Year, RetirementDate.Month))

        Dim liquidations = _payrollLiquidationRepository.LiquidationEmployeeByDate(Contract.EmployeeId, initialDate, endDate)
        If pendingLiquidation IsNot Nothing Then
            liquidations.Add(pendingLiquidation)
        End If

        Return liquidations.Sum(Function(m) m.LiquidationDetail _
                    .Where(Function(o) o.Concept.AffectIBCIncentivePayment AndAlso (o.Concept.ConceptType = 1 OrElse o.Concept.ConceptType = 2)) _
                .Sum(Function(o) IIf(o.Concept.ConceptType = 1, 1, -1) * o.AccruedValue))
    End Function

    Private Function IncentivePaymentDays(Contract As Domain.Payroll.Entities.Contract, PayrollSettings As Domain.Payroll.Entities.PayrollSettings, IncentivePaymentType As Integer, retirementDate As Date) As Integer
        Dim VarIncentivePaymentDays As Integer = 0

        If IncentivePaymentType = 1 Then
            'Prima de Junio
            If Contract.JobBondingDate < PayrollSettings.InitialDateServicesIncentivePayment Then
                VarIncentivePaymentDays = _liquidationDomain.Days360(PayrollSettings.InitialDateServicesIncentivePayment, retirementDate)
            Else
                VarIncentivePaymentDays = _liquidationDomain.Days360(Contract.JobBondingDate, retirementDate)
            End If
        Else
            'Prima de Diciembre
            If Contract.JobBondingDate < PayrollSettings.InitialDateChristmasIncentivePayment Then
                VarIncentivePaymentDays = _liquidationDomain.Days360(PayrollSettings.InitialDateChristmasIncentivePayment, retirementDate)
            Else
                VarIncentivePaymentDays = _liquidationDomain.Days360(Contract.JobBondingDate, retirementDate)
            End If
        End If

        Return VarIncentivePaymentDays
    End Function

    ''' <summary>
    ''' Función que me carga los Días de Sanción
    ''' </summary>
    ''' <param name="EmployeeId"></param>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns></returns>
    Private Function SanctionDays(EmployeeId As Integer, InitialDate As Date, EndDate As Date) As Integer

        Dim VarSanctionDays As Integer = 0

        Dim SanctionNovelty = _noveltyRepository.GetNoveltySanctions(EmployeeId)
        If SanctionNovelty IsNot Nothing AndAlso SanctionNovelty.Count > 0 Then
            If SanctionNovelty.Where(Function(x) x.RealDate >= InitialDate And x.EndDate <= EndDate).Any Then
                VarSanctionDays = SanctionNovelty.Where(Function(y) y.RealDate >= InitialDate And y.EndDate <= EndDate).Sum(Function(x) x.Days)
            End If
        End If

        Return VarSanctionDays

    End Function

    Private Function UnpaidLicensesDays(EmployeeId As Integer, InitialDate As Date, EndDate As Date) As Integer

        Dim VarUnpaidLicensesDays As Integer = 0

        Dim UnpaidLicensesNovelty = _noveltyRepository.GetNoveltyUnpaidLicenses(EmployeeId)
        If UnpaidLicensesNovelty IsNot Nothing AndAlso UnpaidLicensesNovelty.Count > 0 Then
            If UnpaidLicensesNovelty.Where(Function(x) x.RealDate >= InitialDate And x.EndDate <= EndDate).Any Then
                VarUnpaidLicensesDays = UnpaidLicensesNovelty.Where(Function(y) y.RealDate >= InitialDate And y.EndDate <= EndDate).Sum(Function(x) x.Days)
            End If
        End If

        Return VarUnpaidLicensesDays

    End Function

    ''' <summary>
    ''' Primas de Diciembre / Navidad
    ''' </summary>
    ''' <param name="Contract"></param>
    ''' <returns></returns>
    Private Function VariableSalaryUnemployment(Contract As Domain.Payroll.Entities.Contract, ActualLiquidation As Liquidation, RetirementDate As Date) As Decimal

        Dim VariableSalary As Decimal = 0
        Dim InitialDate As Date = New Date(RetirementDate.Year, 1, 1)

        If InitialDate < Contract.JobBondingDate Then
            InitialDate = Contract.JobBondingDate
        End If

        Dim ListLiquidation As List(Of Liquidation) = _payrollLiquidationRepository.LiquidationEmployeeByDate(Contract.EmployeeId, InitialDate, RetirementDate)

        If ListLiquidation IsNot Nothing AndAlso ListLiquidation.Count > 0 Then

            If ActualLiquidation IsNot Nothing AndAlso ActualLiquidation.Id > 0 Then
                ListLiquidation.Add(ActualLiquidation)
            End If

            For Each ObjLiquidation As Liquidation In ListLiquidation

                For Each ObjLiquidationDetail As LiquidationDetail In ObjLiquidation.LiquidationDetail

                    If ObjLiquidationDetail.ConceptClass <> "005" Or ObjLiquidationDetail.ConceptClass <> "006" Then
                        If ObjLiquidationDetail.Concept.AffectIBCSeverance = True Then
                            If ObjLiquidationDetail.Concept.ConceptType = 1 Then
                                VariableSalary = VariableSalary + ObjLiquidationDetail.ConceptTotalValue
                            ElseIf ObjLiquidationDetail.Concept.ConceptType = 2 Then
                                VariableSalary = VariableSalary - ObjLiquidationDetail.ConceptTotalValue
                            End If
                        End If
                    End If
                Next
            Next
        End If

        Return VariableSalary

    End Function

    Private Function UnemploymentDays(Contract As Entities.Contract, retirementDate As Date) As Integer
        Dim VarIncentivePaymentDays As Integer = 0

        Dim InitialDate = New Date(retirementDate.Year, 1, 1)

        If InitialDate < Contract.JobBondingDate Then
            InitialDate = Contract.JobBondingDate
        End If

        VarIncentivePaymentDays = _liquidationDomain.Days360(InitialDate, retirementDate)

        Return VarIncentivePaymentDays
    End Function

    Private Function CalculateVacationDaysRetirement(contract As Entities.Contract, retirementDate As Date) As Decimal
        Dim employee = {contract.Employee}.ToList()
        Dim res = _vacationPeriodDomain.LoadVacationPeriod(employee, Nothing, retirementDate)

        Return res.FirstOrDefault().VacationPeriod.Sum(Function(m) m.PendingDays)
    End Function

    Private Function VacationDays(Contract As Entities.Contract, retirementDate As Date) As Integer
        Dim daysWorkEmployee As Integer = 0

        If Contract.Employee.VacationLastDateLiquidation < retirementDate Then
            daysWorkEmployee = _liquidationDomain.Days360(Contract.Employee.VacationLastDateLiquidation, retirementDate)
        Else
            daysWorkEmployee = 0
        End If

        If Contract.Employee.VacationLastDateLiquidation = retirementDate Then
            daysWorkEmployee = 1
        End If

        If daysWorkEmployee > 0 Then
            Dim listVacationPeriodEmployee = _vacationPeriodRepository.GetVacationPeriodWithDetailByEmployee(Contract.EmployeeId)

            Dim PendingDays As Integer = 0

            For Each itemVacationPeriod In listVacationPeriodEmployee

                If itemVacationPeriod.Vacation IsNot Nothing AndAlso itemVacationPeriod.Vacation.Count > 0 Then
                    If itemVacationPeriod.Vacation.Where(Function(x) x.State = 2).Count > 0 Then
                        PendingDays = PendingDays + itemVacationPeriod.PendingDays
                    End If
                Else
                    If itemVacationPeriod.PendingDays > 0 And itemVacationPeriod.EndDatePeriod < retirementDate And itemVacationPeriod.InitialDatePeriod <> Contract.Employee.VacationLastDateLiquidation Then
                        PendingDays = PendingDays + itemVacationPeriod.PendingDays
                    End If
                End If

            Next

            Dim TotalPendingDays As Integer = (360 * PendingDays) / 15
            daysWorkEmployee = daysWorkEmployee + TotalPendingDays

        End If

        Return daysWorkEmployee

    End Function

    ''' <summary>
    ''' Primas de Diciembre / Navidad
    ''' </summary>
    ''' <param name="Contract"></param>
    ''' <returns></returns>
    Private Function VariableSalaryVacation(Contract As Domain.Payroll.Entities.Contract, ActualLiquidation As Liquidation, RetirementDate As Date) As Decimal

        Dim VariableSalary As Decimal = 0
        Dim InitialDate As Date = New Date(RetirementDate.Year, 1, 1)

        If InitialDate < Contract.JobBondingDate Then
            InitialDate = Contract.JobBondingDate
        End If

        If DateDiff(DateInterval.Month, InitialDate, RetirementDate) > 12 Then
            InitialDate = DateAdd(DateInterval.Month, -12, RetirementDate)
        End If

        Dim ListLiquidation As List(Of Liquidation) = _payrollLiquidationRepository.LiquidationEmployeeByDate(Contract.EmployeeId, InitialDate, RetirementDate)

        If ListLiquidation IsNot Nothing AndAlso ListLiquidation.Count > 0 Then

            If ActualLiquidation IsNot Nothing AndAlso ActualLiquidation.Id > 0 Then
                ListLiquidation.Add(ActualLiquidation)
            End If

            For Each ObjLiquidation As Liquidation In ListLiquidation

                For Each ObjLiquidationDetail As LiquidationDetail In ObjLiquidation.LiquidationDetail

                    If ObjLiquidationDetail.ConceptClass <> "005" Or ObjLiquidationDetail.ConceptClass <> "006" Then
                        If ObjLiquidationDetail.Concept.AffectIBCVacation = True Then
                            If ObjLiquidationDetail.Concept.ConceptType = 1 Then
                                VariableSalary = VariableSalary + ObjLiquidationDetail.ConceptTotalValue
                            ElseIf ObjLiquidationDetail.Concept.ConceptType = 2 Then
                                VariableSalary = VariableSalary - ObjLiquidationDetail.ConceptTotalValue
                            End If
                        End If
                    End If
                Next
            Next
        End If

        Return VariableSalary

    End Function

    ''' <summary>
    ''' Calcula el periodo de liquidación correcto según el tipo de nómina y la fecha de retiro
    ''' </summary>
    ''' <param name="retirementDate">Fecha de retiro del empleado</param>
    ''' <param name="liquidationType">Tipo de liquidacuon (1=Mensual, 2=Quincenal)</param>
    ''' <param name="initialDate">Fecha inicial del período (ByRef)</param>
    ''' <param name="endDate">Fecha final del período (ByRef)</param>
    Private Sub GetPayrollPeriodDates(retirementDate As Date, liquidationType As Integer, ByRef initialDate As Date, ByRef endDate As Date)
        If liquidationType = 1 Then ''mensual
            initialDate = New Date(retirementDate.Year, retirementDate.Month, 1)
            endDate = New Date(retirementDate.Year, retirementDate.Month, DateTime.DaysInMonth(retirementDate.Year, retirementDate.Month))

        ElseIf liquidationType = 2 Then ''quincenal
            If retirementDate.Day <= 15 Then
                ' Primera quincena (1-15)
                initialDate = New Date(retirementDate.Year, retirementDate.Month, 1)
                endDate = New Date(retirementDate.Year, retirementDate.Month, 15)
            Else
                ' Segunda quincena (16 al ultimo dia del mes)
                initialDate = New Date(retirementDate.Year, retirementDate.Month, 16)
                endDate = New Date(retirementDate.Year, retirementDate.Month, DateTime.DaysInMonth(retirementDate.Year, retirementDate.Month))
            End If
        Else
            initialDate = New Date(retirementDate.Year, retirementDate.Month, 1)
            endDate = _liquidationDomain.GetEndPayrollDate(liquidationType, initialDate)
        End If
    End Sub

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _liquidationDomain.Dispose()
                _incentivePaymentDomain.Dispose()
                _unemployedLiquidationDomain.Dispose()
                _vacationPeriodDomain.Dispose()
            End If
            _liquidationDomain = Nothing
            _incentivePaymentDomain = Nothing
            _unemployedLiquidationDomain = Nothing
            _vacationPeriodDomain = Nothing

            _unemployedLiquidationRepository = Nothing
            _contractLiquidationRepository = Nothing
            _payrollLiquidationRepository = Nothing
            _vacationPeriodRepository = Nothing
            _conceptRepository = Nothing
            _retirementReasonRepository = Nothing
            _noveltyRepository = Nothing
            _contractRepository = Nothing
            _payrollSettingsRepository = Nothing
            _PositionRepository = Nothing
            _retroactiveRepository = Nothing
            _incentivePaymentRepository = Nothing
            _ManualConceptsRepository = Nothing
            _holidayRepository = Nothing
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