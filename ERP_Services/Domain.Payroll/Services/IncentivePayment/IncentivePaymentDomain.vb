'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 14-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Common.Entities
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports System.Globalization
Imports Domain.Payroll.Entities.Concept
Imports Concept = Domain.Payroll.Entities.Concept
Imports Employee = Domain.Payroll.Entities.Employee

Public Class IncentivePaymentDomain
    Implements IIncentivePaymentDomain

    ''' <summary>
    ''' Dominio de Liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private _LiquidationDomain As ILiquidationDomain

    Private _liquidationRepository As IPayrollLiquidationRepository

    Private _ManualConceptsRepository As IManualConcepts

    Private _PositionRepository As IPositionRepository

    Private _noveltyRepository As INoveltyRepository

    Private _retroactiveRepository As IRetroactiveCRepository

    Private _NoveltyIncentivePaymentRepository As INoveltyIncentivePaymentRepository

    Private _groupRepository As IGroupRepository

    Private _settingsRepository As IPayrollSettingsRepository

    Private _contractRepository As IContractRepository

    Private _conceptRepository As IConceptRepository

    Private _foreclousureRepository As IForeclousureRepository
    ''' <summary>
    ''' Repositorio de Primas
    ''' </summary>
    ''' <remarks></remarks>
    Private _incentivePaymentRepository As IIncentivePaymentRepository

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>

    Private _personRepository As IPersonRepository

    Private _bankRepository As IBankRepository

    Private _vacationRepository As IVacationRepository

#Region "Constructor"
    Public Sub New(payrollLiquidationFunctions As ILiquidationDomain, liquidationRepository As IPayrollLiquidationRepository, ManualConceptsRepository As IManualConcepts,
                   PositionRepository As IPositionRepository, noveltyRepository As INoveltyRepository, retroactiveRepository As IRetroactiveCRepository,
                   NoveltyIncentivePaymentRepository As INoveltyIncentivePaymentRepository, GroupRepository As IGroupRepository, settingsRepository As IPayrollSettingsRepository,
                   contractRepository As IContractRepository, conceptRepository As IConceptRepository, incentivePaymentRepository As IIncentivePaymentRepository, personrepository As IPersonRepository, bankRepository As IBankRepository,
        foreclousureRepository As IForeclousureRepository, vacationRepository As IVacationRepository)
        _LiquidationDomain = payrollLiquidationFunctions
        _liquidationRepository = liquidationRepository
        _ManualConceptsRepository = ManualConceptsRepository
        _PositionRepository = PositionRepository
        _noveltyRepository = noveltyRepository
        _retroactiveRepository = retroactiveRepository
        _NoveltyIncentivePaymentRepository = NoveltyIncentivePaymentRepository
        _groupRepository = GroupRepository
        _settingsRepository = settingsRepository
        _contractRepository = contractRepository
        _conceptRepository = conceptRepository
        _incentivePaymentRepository = incentivePaymentRepository
        _personRepository = personrepository
        _bankRepository = bankRepository
        _foreclousureRepository = foreclousureRepository
        _vacationRepository = vacationRepository
    End Sub
#End Region

    Public Function WorkingDays(Contract As Domain.Payroll.Entities.Contract, IncentiveStarDate As Date, IncentiveEndDate As Date) As Integer

        Dim ContractInitialDate = Contract.JobBondingDate
        Dim ContractEndingDate = Contract.ContractEndingDate
        Dim InitialDate As Date
        Dim EndDate As Date
        Dim WorkDays As Integer

        If IncentiveStarDate >= ContractInitialDate Then
            InitialDate = IncentiveStarDate
        Else
            InitialDate = ContractInitialDate
        End If

        If IncentiveEndDate > ContractEndingDate Then
            EndDate = ContractEndingDate
        Else
            EndDate = IncentiveEndDate
        End If

        WorkDays = _LiquidationDomain.Days360(InitialDate, EndDate)

        Return WorkDays

    End Function

    ''' <summary>
    ''' función que se utiliza para Calcular Primas
    ''' </summary>
    ''' <param name="PaymentType">Tipo de Pago</param>
    ''' <param name="Period">Periodo ( 1 - 2 )</param>
    ''' <param name="IncentiveStarDate">Fecha Inicio Prima</param>
    ''' <param name="IncentiveEndDate">Fecha Fin Prima</param>
    ''' <param name="contract">Contrato (PARA LIQUIDACIÓN DE CONTRATO)</param>
    ''' <returns>Action Result Primas</returns>
    ''' <remarks></remarks>
    Public Function IncentivePaymentCalculate(Group As Group, PaymentType As Char, Period As Char, IncentiveStarDate As Date, IncentiveEndDate As Date, SessionValues As SessionValues, Optional ByVal contract As Domain.Payroll.Entities.Contract = Nothing, Optional ExtraLiquidation As Liquidation = Nothing, Optional FlagContractLiquidation As Boolean = False, Optional ByRef ReplaceFormulate As String = "", Optional ByRef ConceptFormulate As String = "", Optional ByVal BasePrimasCesantias As Decimal = 0, Optional employeeList As List(Of Employee) = Nothing) As ActionMessageResult(Of List(Of IncentivePayment)) Implements IIncentivePaymentDomain.IncentivePaymentCalculate
        Dim AverageIBCIncentivePaymentValue As Double
        Dim IncentivePaymentValue As Double
        Dim IncentivePayment As New IncentivePayment
        Dim WorkDays As Integer
        Dim SanctionDays As Integer
        Dim IncentivePaymentList As New List(Of IncentivePayment)
        Dim listContract As New List(Of Integer)
        Dim ValueAuxTransporte As Double = 0

        Dim actionResult As ActionMessageResult(Of List(Of IncentivePayment)) = New ActionMessageResult(Of List(Of IncentivePayment))()

        Try

            If FlagContractLiquidation = False Then
                'Variable para obtener fórmulas de PayrollParameter
                Dim _groupinfo = _groupRepository.GetGroupById(Group.Id)

                Dim SettingsPayroll = _settingsRepository.GetSettingPayroll()
                If SettingsPayroll Is Nothing Then
                    actionResult.StateResult = False
                    actionResult.Message = "No se ha parametrizado los Parámetros de Nómina"
                    Return actionResult
                End If

                Dim IncentivePaymentType As Byte
                Dim NewInitialDate As Date = New Date(IncentiveEndDate.Year, 1, 1)
                Dim NewEndDate As Date = New Date(IncentiveEndDate.Year, 12, 31)

                Dim ListLiquidationLastYear As New List(Of Liquidation)

                If SessionValues.IndigoCompanyType = 2 Then
                    ListLiquidationLastYear = _liquidationRepository.GetConfirmLiquidationByStarEndDateRetroactive(NewInitialDate, NewEndDate, Group.Id)
                End If

                Dim employeeLiquidation As New List(Of Domain.Payroll.Entities.Employee)

                Dim InitialDateIncentivePayment As Date
                Dim EndDateIncentivePayment As Date
                Dim ConceptId As Integer
                Dim FormulatesIncentivePayment As String = String.Empty
                Dim LegalMinimunSalary = Group.PayrollParameter.LegalSalaryMinimum


                'Cargo las Fechas de Inicio y Fin de las Primas
                If Period = "1" Then
                    'Prima de Junio
                    InitialDateIncentivePayment = SettingsPayroll.InitialDateServicesIncentivePayment
                    EndDateIncentivePayment = SettingsPayroll.EndDateServicesIncentivePayment
                    If _groupinfo.PayrollParameter.IncentivePaymentFormula1 IsNot Nothing Then
                        FormulatesIncentivePayment = _groupinfo.PayrollParameter.IncentivePaymentFormula1
                    Else
                        FormulatesIncentivePayment = SettingsPayroll.ServicesIncentivePaymentFormulates
                    End If
                    ConceptId = SettingsPayroll.ServicesIncentivePaymentConceptId
                    IncentivePaymentType = 3
                Else
                    'Prima de Diciembre
                    InitialDateIncentivePayment = SettingsPayroll.InitialDateChristmasIncentivePayment
                    EndDateIncentivePayment = SettingsPayroll.EndDateChristmasIncentivePayment
                    If _groupinfo.PayrollParameter.IncentivePaymentFormula2 IsNot Nothing Then
                        FormulatesIncentivePayment = _groupinfo.PayrollParameter.IncentivePaymentFormula2
                    Else
                        FormulatesIncentivePayment = SettingsPayroll.ChristmasIncentivePayment
                    End If
                    ConceptId = SettingsPayroll.ChristmasIncentivePaymentConceptId
                    IncentivePaymentType = 4
                End If

                If FormulatesIncentivePayment Is Nothing Then
                    actionResult.StateResult = False
                    actionResult.Message = "No se ha parametrizado la Fórmula de las Primas"
                    Return actionResult
                End If

                Dim ListRetroactive As New List(Of RetroactiveC)
                If SessionValues.IndigoCompanyType = 2 Then
                    ListRetroactive = _retroactiveRepository.GetListRetroactiveByRangeOfDates(InitialDateIncentivePayment, EndDateIncentivePayment, Group.Id)

                    If ListRetroactive Is Nothing Then
                        ListRetroactive = _retroactiveRepository.GetListRetroactiveByYear(Year(EndDateIncentivePayment), Group.Id)

                        If ListRetroactive Is Nothing Then
                            ListRetroactive = _retroactiveRepository.GetListRetroactiveByYear(Year(EndDateIncentivePayment) - 1, Group.Id)
                        End If
                    End If
                End If

                '' Cargo las Variables para usar en el Cálcul
                Dim allEmployees As List(Of Employee) = If(employeeList, _liquidationRepository.GetEmployesIncentivePayment(Group.Id, EndDateIncentivePayment, InitialDateIncentivePayment, ""))

                If allEmployees IsNot Nothing Then
                    ' ===== CACHE PARA OPTIMIZACIÓN  =====
                    Dim groupCache As New Dictionary(Of String, Group)()
                    Dim conceptCache As New Dictionary(Of String, Concept)()
                    Dim conceptByIdCache As New Dictionary(Of Integer, Concept)()
                    ' Conceptos por clase
                    Dim concept002List As New List(Of String) : concept002List.Add("002")
                    Dim concept046List As New List(Of String) : concept046List.Add("046")
                    Dim concept049List As New List(Of String) : concept049List.Add("049")
                    conceptCache("002") = _conceptRepository.GetConceptByConceptClass(concept002List).FirstOrDefault()
                    conceptCache("046") = _conceptRepository.GetConceptByConceptClass(concept046List).FirstOrDefault()
                    conceptCache("049") = _conceptRepository.GetConceptByConceptClass(concept049List).FirstOrDefault()
                    conceptCache("008") = _conceptRepository.GetConceptByClass("008")
                    conceptCache("701") = _conceptRepository.GetConcept("701")


                    For Each ObjEmployeeLiquidation As Domain.Payroll.Entities.Employee In employeeLiquidation

                        'Variables
                        Dim BasicSalary As Double = 0
                        Dim RepresentationCost As Double = 0
                        Dim BonificationValue As Double = 0
                        Dim VacationIncentiveValue As Double = 0
                        Dim ServicesIncentivePaymentValue As Double = 0
                        Dim IncentiveDays As Integer = _LiquidationDomain.Days360(InitialDateIncentivePayment, EndDateIncentivePayment)
                        Dim IncentivePaymentAverage As Double = 0
                        Dim SalaryAverage As Double = 0
                        Dim VarSanctionDays As Integer = 0
                        Dim VarUnpaidLicensesDays As Integer = 0
                        Dim AverageHelpTransportValue As Double = 0
                        Dim TransportHealthValue = Group.PayrollParameter.TransportHelpValue
                        Dim ingressDate As Date
                        Dim contractDate As Date
                        Dim VarLicensesMonth As Integer = 0

                        Dim listContractActive = ObjEmployeeLiquidation.Contract.Where(Function(x) x.Status = 1).ToList()
                        If listContractActive.Count > 0 Then
                            ingressDate = listContractActive.Item(0).ContractInitialDate
                            contractDate = listContractActive.Item(0).JobBondingDate
                        End If
                        Dim EmployeeListContract = ObjEmployeeLiquidation.Contract.Where(Function(x) x.ContractEndingDate >= InitialDateIncentivePayment And x.ContractInitialDate <= EndDateIncentivePayment And x.Status <> 2).ToList()

                        'Recorro todos los Empleados del Grupo que se le van a pagar la Prima

                        Dim ActualContract As Domain.Payroll.Entities.Contract

                        ActualContract = EmployeeListContract.OrderByDescending(Function(x) x.ContractEndingDate).FirstOrDefault()

                        If ActualContract IsNot Nothing Then
                            BasicSalary = ActualContract.BasicSalary
                        Else
                            ActualContract = ObjEmployeeLiquidation.Contract.Where(Function(x) x.ContractEndingDate > EndDateIncentivePayment And x.ContractInitialDate < EndDateIncentivePayment).FirstOrDefault()
                            If ActualContract IsNot Nothing Then
                                BasicSalary = ActualContract.BasicSalary
                            End If
                        End If

                        'Si el contrato inició después del periodo de Primas, el numero de Días de Primas para el Empleado es menor
                        Dim ActualGroup As Group
                        If ActualContract IsNot Nothing Then
                            If ActualContract.JobBondingDate > InitialDateIncentivePayment Then
                                IncentiveDays = _LiquidationDomain.Days360(ActualContract.JobBondingDate, EndDateIncentivePayment)
                            End If
                            If Not groupCache.ContainsKey(ActualContract.GroupId) Then
                                groupCache(ActualContract.GroupId) = _groupRepository.GetGroupById(ActualContract.GroupId)
                            End If
                            ActualGroup = groupCache(ActualContract.GroupId)
                        End If

                        Dim ObjPosition = ActualContract.Position

                        Dim DaysPromedy As Integer = 0

                        Dim ObjRetroactiveEmployee As New List(Of RetroactiveC)

                        If ListRetroactive IsNot Nothing AndAlso ListRetroactive.Count() > 0 Then
                            ObjRetroactiveEmployee = ListRetroactive.Where(Function(x) x.IdEmployee = ActualContract.EmployeeId And x.NextPayrollDate >= InitialDateIncentivePayment And x.NextPayrollDate <= EndDateIncentivePayment).ToList()
                        End If

                        'Históricos de Contratos
                        'Dim ListHistoricalContract = _contractRepository.GetContractByInitialNumber(ActualContract.InitialContractNumber, ObjEmployeeLiquidation.Id)
                        If EmployeeListContract IsNot Nothing Then

                            Dim NewEmployeeListContract = EmployeeListContract.Where(Function(x) x.ContractType.ContractClass = 3 Or x.ContractType.ContractClass = 4).OrderBy(Function(x) x.Id)

                            If NewEmployeeListContract.Count > 1 Then
                                For Each objContract As Domain.Payroll.Entities.Contract In NewEmployeeListContract

                                    Dim tempDaysPromedy As Integer = 0
                                    'Para contratos antiguos con variación en el Periodo de Primas
                                    Dim SalaryTypeEmployee = _contractRepository.GetContractById(objContract.Id, True)

                                    If SalaryTypeEmployee.ContractType.SalaryType = 2 Then
                                        Continue For
                                    End If

                                    If objContract.ContractInitialDate < IncentiveStarDate And objContract.ContractEndingDate >= IncentiveEndDate Then
                                        'Si no hay variaciones en todo el contrato
                                        tempDaysPromedy = IncentiveDays
                                    ElseIf objContract.ContractInitialDate > IncentiveStarDate And objContract.ContractEndingDate >= IncentiveEndDate Then
                                        'Si el Contrato inició después de la Fecha Corte de Primas y finaliza después de la FEcha Corte de PRimas
                                        tempDaysPromedy = _LiquidationDomain.Days360(objContract.ContractInitialDate, IncentiveEndDate)
                                    ElseIf objContract.ContractInitialDate > IncentiveStarDate And objContract.ContractEndingDate < IncentiveEndDate Then
                                        'Si el contrato inició después de la Fecha de Corte de Primas y finaliza antes de la FEcha Corte de Primas
                                        tempDaysPromedy = _LiquidationDomain.Days360(objContract.ContractInitialDate, objContract.ContractEndingDate)
                                    ElseIf objContract.ContractInitialDate <= IncentiveStarDate And objContract.ContractEndingDate < IncentiveEndDate Then
                                        'Si el contrato inició antes o en la Fecha de Corte de Primas y finalizó ANTES de la FEcha de corte de Primas
                                        tempDaysPromedy = _LiquidationDomain.Days360(IncentiveStarDate, objContract.ContractEndingDate)
                                    End If

                                    SalaryAverage = SalaryAverage + ((objContract.BasicSalary * tempDaysPromedy) / IncentiveDays)

                                    If objContract.BasicSalary <= 2 * LegalMinimunSalary Then
                                        AverageHelpTransportValue = AverageHelpTransportValue + ((TransportHealthValue * tempDaysPromedy) / IncentiveDays)
                                    End If
                                    BasicSalary = objContract.BasicSalary 'Se uso para que tome el salario para el contrato que merece primas
                                    ObjPosition = objContract.Position 'Se utiliza esta linea para que tome el cargo del contrato a liquidar 
                                    DaysPromedy = DaysPromedy + tempDaysPromedy
                                Next
                            Else
                                If ActualContract.BasicSalary <= 2 * LegalMinimunSalary Then
                                    AverageHelpTransportValue = TransportHealthValue
                                End If

                                ''De la linea 308 a las 322 se escribio este fragmento de codigo puesto que no queria evaluar cuando la persona tenia un solo contrato 
                                If ActualContract.ContractInitialDate < IncentiveStarDate And ActualContract.ContractEndingDate >= IncentiveEndDate Then
                                    'Si no hay variaciones en todo el contrato
                                    DaysPromedy = IncentiveDays
                                ElseIf ActualContract.ContractInitialDate > IncentiveStarDate And ActualContract.ContractEndingDate >= IncentiveEndDate Then
                                    'Si el Contrato inició después de la Fecha Corte de Primas y finaliza después de la FEcha Corte de PRimas
                                    DaysPromedy = _LiquidationDomain.Days360(ActualContract.ContractInitialDate, IncentiveEndDate)
                                ElseIf ActualContract.ContractInitialDate > IncentiveStarDate And ActualContract.ContractEndingDate < IncentiveEndDate Then
                                    'Si el contrato inició después de la Fecha de Corte de Primas y finaliza antes de la FEcha Corte de Primas
                                    DaysPromedy = _LiquidationDomain.Days360(ActualContract.ContractInitialDate, ActualContract.ContractEndingDate)
                                ElseIf ActualContract.ContractInitialDate <= IncentiveStarDate And ActualContract.ContractEndingDate < IncentiveEndDate Then
                                    'Si el contrato inició antes o en la Fecha de Corte de Primas y finalizó ANTES de la FEcha de corte de Primas
                                    DaysPromedy = _LiquidationDomain.Days360(IncentiveStarDate, ActualContract.ContractEndingDate)
                                End If

                                SalaryAverage = ActualContract.BasicSalary 'Se descomento esta linea puesto que no generaba el promedio salarial lo que ocasionaba problemas en el total pagado cuando era un solo contrato


                            End If
                        End If

                        Dim LiquidationEmployeeList As New List(Of Liquidation)
                        Dim LiquidationEmployeeInitalContractList As New List(Of Liquidation)
                        Dim TmpEmployeeList As New List(Of Domain.Payroll.Entities.Employee)
                        TmpEmployeeList.Add(ObjEmployeeLiquidation)

                        Dim contractLiquidation = _liquidationRepository.LiquidationEmployeeByDate(ObjEmployeeLiquidation.Id, IncentiveStarDate, IncentiveEndDate)

                        If contractLiquidation IsNot Nothing AndAlso contractLiquidation.Count > 0 Then
                            LiquidationEmployeeList.AddRange(contractLiquidation)
                        End If

                        If LiquidationEmployeeList IsNot Nothing AndAlso LiquidationEmployeeList.Count > 0 Then
                            LiquidationEmployeeInitalContractList = LiquidationEmployeeList.Where(Function(x) x.InitialContractNumber = ActualContract.InitialContractNumber).ToList()
                        End If

                        Dim DateRetirement = New Date(1901, 1, 1)
                        Dim ActualLiquidation As New Liquidation

                        If ActualGroup IsNot Nothing AndAlso ActualGroup.NextDateLiquidation <= IncentiveEndDate Then

                            If SessionValues.IndigoCompanyType = 2 Then
                                Dim ListLiquidation = _LiquidationDomain.NewExecuteLiquitadion(TmpEmployeeList, IIf(ActualGroup IsNot Nothing, ActualGroup, Group), False, SessionValues, DateRetirement, True).ObjectEmbbeded
                                If ListLiquidation IsNot Nothing Then
                                    ActualLiquidation = ListLiquidation.FirstOrDefault()
                                End If
                            Else
                                Dim ResultLiquidation = _LiquidationDomain.LiquidatedExtraTime(ActualGroup, ActualContract, ObjPosition, ActualGroup.NextDateLiquidation, New Date(ActualGroup.NextDateLiquidation.Year, ActualGroup.NextDateLiquidation.Month, Date.DaysInMonth(ActualGroup.NextDateLiquidation.Year, ActualGroup.NextDateLiquidation.Month)))

                                If ResultLiquidation.StateResult = True Then
                                    ActualLiquidation = ResultLiquidation.ObjectEmbbeded
                                End If
                            End If

                        End If

                        If ActualLiquidation IsNot Nothing Then
                            LiquidationEmployeeInitalContractList.Add(ActualLiquidation)
                            'Sumo los gastos de representacion
                            RepresentationCost = (From x In ActualLiquidation.LiquidationDetail Where x.ConceptClass = "047" Select x.AccruedValue).Sum()
                        End If

                        AverageIBCIncentivePaymentValue = 0

                        Dim LiquidationDetail As New List(Of LiquidationDetail)

                        If LiquidationEmployeeInitalContractList IsNot Nothing Then

                            For Each ObjLiquidation As Liquidation In LiquidationEmployeeInitalContractList

                                LiquidationDetail = ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptClass <> "005" AndAlso x.ConceptClass <> "006" AndAlso x.Concept.AffectIBCIncentivePayment = True).ToList()
                                If LiquidationDetail IsNot Nothing AndAlso LiquidationDetail.Count() > 0 Then
                                    If LiquidationDetail.Any(Function(x) x.ConceptType = 1) Then
                                        AverageIBCIncentivePaymentValue = AverageIBCIncentivePaymentValue + (LiquidationDetail.Sum(Function(x) x.AccruedValue And x.ConceptType = 1))
                                    ElseIf LiquidationDetail.Any(Function(x) x.ConceptType = 2) Then
                                        AverageIBCIncentivePaymentValue = AverageIBCIncentivePaymentValue - (LiquidationDetail.Sum(Function(x) x.DeductedValue And x.ConceptType = 2))
                                    End If
                                End If

                            Next

                        Else
                            AverageIBCIncentivePaymentValue = 0
                        End If

                        'Averiguo las Licencias No Remuneradas y las Sanciones
                        Dim UnpaidLicensesNovelty = _noveltyRepository.GetNoveltyUnpaidLicenses(ObjEmployeeLiquidation.Id)

                        If UnpaidLicensesNovelty IsNot Nothing And UnpaidLicensesNovelty.Count > 0 Then
                            'VarUnpaidLicensesDays = UnpaidLicensesNovelty.Sum(Function(x) x.Days)
                            VarUnpaidLicensesDays = Me.InabilitiesPeriodDays(UnpaidLicensesNovelty, InitialDateIncentivePayment, EndDateIncentivePayment)
                            'Month Licenses para calcular el parametro de meses trabajados restandole los meses de licensias No remuneradas
                            VarLicensesMonth = Me.InabilitiesPeriodMonth(UnpaidLicensesNovelty, InitialDateIncentivePayment, EndDateIncentivePayment)
                        End If

                        Dim SanctionNovelty = _noveltyRepository.GetNoveltySanctions(ObjEmployeeLiquidation.Id)

                        If SanctionNovelty IsNot Nothing And SanctionNovelty.Count > 0 Then

                            VarSanctionDays = Me.InabilitiesPeriodDays(SanctionNovelty, InitialDateIncentivePayment, EndDateIncentivePayment)

                        End If

                        If BasicSalary > (2 * LegalMinimunSalary) Then
                            TransportHealthValue = 0
                        End If

                        'Se calcula sobre los meses trabajados y cumplidos según el HUN, autoriza Cristian
                        Dim MonthIncentivePayment As Integer = Math.Ceiling(IncentiveDays / 30)
                        'Se agrega para Obtener los Meses cumplidos y laborados por el empleado, mas abajo le restamos los meses de Licencias No remuneradas
                        Dim MonthWorked As Integer = Math.Truncate(IncentiveDays / 30)

                        If (VarSanctionDays + VarUnpaidLicensesDays) > 0 Then
                            If (VarSanctionDays + VarUnpaidLicensesDays) >= 1 And (VarSanctionDays + VarUnpaidLicensesDays) <= 30 Then
                                MonthIncentivePayment = MonthIncentivePayment - 1
                            End If

                            If (VarSanctionDays + VarUnpaidLicensesDays) >= 31 And (VarSanctionDays + VarUnpaidLicensesDays) <= 60 Then
                                MonthIncentivePayment = MonthIncentivePayment - 2
                            End If

                            If (VarSanctionDays + VarUnpaidLicensesDays) >= 61 And (VarSanctionDays + VarUnpaidLicensesDays) <= 90 Then
                                MonthIncentivePayment = MonthIncentivePayment - 3
                            End If

                            If (VarSanctionDays + VarUnpaidLicensesDays) >= 91 And (VarSanctionDays + VarUnpaidLicensesDays) <= 120 Then
                                MonthIncentivePayment = MonthIncentivePayment - 4
                            End If

                            If (VarSanctionDays + VarUnpaidLicensesDays) >= 121 And (VarSanctionDays + VarUnpaidLicensesDays) <= 150 Then
                                MonthIncentivePayment = MonthIncentivePayment - 5
                            End If

                            If (VarSanctionDays + VarUnpaidLicensesDays) >= 151 And (VarSanctionDays + VarUnpaidLicensesDays) <= 180 Then
                                MonthIncentivePayment = MonthIncentivePayment - 6
                            End If
                        End If

                        If SessionValues.IndigoCompanyType = 2 Then
                            'Cargo con el Retroactivo el Valor de la Prima de Servicios
                            Dim ListPaidValueAverageIncentiveServices = _incentivePaymentRepository.GetIncentivePaymentByEmployeeIdLastLiquidation(ActualContract.EmployeeId)
                            Dim datePeriod As Date = InitialDateIncentivePayment 'New Date(Group.NextDateLiquidation.Year - 1, Group.NextDateLiquidation.Month, 1)
                            Dim ObjPaidValueAverageIncentiveServices = ListPaidValueAverageIncentiveServices.Where(Function(x) x.Period = 1 And x.PeriodEndDate >= datePeriod).FirstOrDefault()

                            If ObjPaidValueAverageIncentiveServices IsNot Nothing Then
                                IncentivePaymentAverage = ObjPaidValueAverageIncentiveServices.TotalAccrued

                                If ObjRetroactiveEmployee IsNot Nothing AndAlso ObjRetroactiveEmployee.Count() > 0 Then
                                    'Si toca buscar el retroactivo, busco el dato del Concepto
                                    Dim Concept = conceptCache("002")
                                    If Concept IsNot Nothing Then
                                        For Each ObjRetroactiveItem In ObjRetroactiveEmployee.Where(Function(r) r.RetroactiveD.Any(Function(x) x.IdConcept = Concept.Id))
                                            Dim ObjConceptRetroactive = ObjRetroactiveItem.RetroactiveD.Where(Function(x) x.IdConcept = Concept.Id).FirstOrDefault()
                                            If ObjConceptRetroactive IsNot Nothing Then
                                                IncentivePaymentAverage = ObjConceptRetroactive.ValueConceptWithRetroactive + IncentivePaymentAverage
                                            End If
                                        Next
                                    End If
                                End If
                            End If

                            'Cargo la bonificación x Año de Servicio

                            Dim ObjConceptClassBonificationValue = _liquidationRepository.GetLastConceptClassListDateBetween(IIf(ActualContract.InitialContractNumber = 0, ActualContract.Id, ActualContract.InitialContractNumber), "046", datePeriod, EndDateIncentivePayment)
                            If ObjConceptClassBonificationValue.Count > 0 Then
                                Dim BonificationConceptId As Integer
                                For Each item In ObjConceptClassBonificationValue
                                    BonificationValue += item.ConceptTotalValue
                                    BonificationConceptId = item.ConceptId
                                Next

                                If ObjRetroactiveEmployee IsNot Nothing AndAlso ObjRetroactiveEmployee.Count() > 0 Then
                                    For Each ObjRetroactiveItem In ObjRetroactiveEmployee.Where(Function(r) r.RetroactiveD.Any(Function(x) x.IdConcept = BonificationConceptId))
                                        Dim ObjConceptRetroactive = ObjRetroactiveItem.RetroactiveD.Where(Function(x) x.IdConcept = BonificationConceptId).FirstOrDefault()
                                        If ObjConceptRetroactive IsNot Nothing Then
                                            BonificationValue = ObjConceptRetroactive.ValueConceptWithRetroactive + BonificationValue

                                        End If
                                    Next
                                End If

                            ElseIf ObjRetroactiveEmployee IsNot Nothing AndAlso ObjRetroactiveEmployee.Count() > 0 Then
                                'Se realiza para tener en cuenta el concepto de bonificacion liquidado por retroactivo, ya que si no tenia un concepto de bonificación previo liquidado por el proceso de Nómina, no permitia consultar y sumar el retroactivo.
                                Dim Concept = conceptCache("046")
                                If Concept IsNot Nothing Then
                                    For Each ObjRetroactiveItem In ObjRetroactiveEmployee.Where(Function(r) r.RetroactiveD.Any(Function(x) x.IdConcept = Concept.Id))
                                        Dim ObjConceptRetroactive = ObjRetroactiveItem.RetroactiveD.Where(Function(x) x.IdConcept = Concept.Id).FirstOrDefault()
                                        If ObjConceptRetroactive IsNot Nothing Then
                                            BonificationValue = ObjConceptRetroactive.ValueConceptWithRetroactive + BonificationValue

                                        End If
                                    Next
                                End If
                            End If

                            'Cargo la Prima de Vacaciones
                            Dim ObjConceptClassVacationIncentiveValue = _liquidationRepository.GetLastConceptClassListDateBetween(IIf(ActualContract.InitialContractNumber = 0, ActualContract.Id, ActualContract.InitialContractNumber), "049", InitialDateIncentivePayment, EndDateIncentivePayment)
                            If ObjConceptClassVacationIncentiveValue.Count > 0 Then
                                Dim VacationIncentiveConceptId As Integer
                                For Each item In ObjConceptClassVacationIncentiveValue
                                    VacationIncentiveValue += item.ConceptTotalValue
                                    VacationIncentiveConceptId = item.ConceptId
                                Next

                                If ObjRetroactiveEmployee IsNot Nothing AndAlso ObjRetroactiveEmployee.Count() > 0 Then
                                    For Each ObjRetroactiveItem In ObjRetroactiveEmployee.Where(Function(r) r.RetroactiveD.Any(Function(x) x.IdConcept = VacationIncentiveConceptId))
                                        Dim ObjConceptRetroactive = ObjRetroactiveItem.RetroactiveD.Where(Function(x) x.IdConcept = VacationIncentiveConceptId).FirstOrDefault()
                                        If ObjConceptRetroactive IsNot Nothing Then
                                            VacationIncentiveValue = ObjConceptRetroactive.ValueConceptWithRetroactive + VacationIncentiveValue

                                        End If
                                    Next
                                End If

                            ElseIf ObjRetroactiveEmployee IsNot Nothing AndAlso ObjRetroactiveEmployee.Count() > 0 Then
                                'Se realiza para tener en cuenta el concepto de prima de vacaciones liquidado por retroactivo, ya que si no tenia un concepto de prima de vacaciones previo liquidado por el proceso de Nómina, no permitia consultar y sumar el retroactivo.
                                Dim ConceptVacationIncentive = conceptCache("049")

                                If ConceptVacationIncentive IsNot Nothing Then
                                    For Each ObjRetroactiveItem In ObjRetroactiveEmployee.Where(Function(r) r.RetroactiveD.Any(Function(x) x.IdConcept = ConceptVacationIncentive.Id))
                                        Dim ObjConceptRetroactive = ObjRetroactiveItem.RetroactiveD.Where(Function(x) x.IdConcept = ConceptVacationIncentive.Id).FirstOrDefault()
                                        If ObjConceptRetroactive IsNot Nothing Then
                                            VacationIncentiveValue = ObjConceptRetroactive.ValueConceptWithRetroactive + VacationIncentiveValue
                                        End If
                                    Next
                                End If
                            End If
                        End If

                        Dim accumulatedValueAguinaldoBase As Decimal = CalculateAccumulatedValueAguinaldoBase(ActualContract, SettingsPayroll)

                        'Si no hay liquidaciones históricas NI retroactivos de Prima de Vacaciones, obtener desde VacationDetail
                        If VacationIncentiveValue = 0 AndAlso SessionValues.IndigoCompanyType = 2 Then
                            ' Obtener Prima de Vacaciones desde la tabla VacationDetail (concepto clase 049)
                            Dim vacationIncentiveFromVacations = _vacationRepository.GetVacationIncentiveByDateRange(
                                ActualContract.EmployeeId,
                                "049",
                                InitialDateIncentivePayment,
                                EndDateIncentivePayment
                            )
                            If vacationIncentiveFromVacations IsNot Nothing AndAlso vacationIncentiveFromVacations.Count > 0 Then
                                VacationIncentiveValue = vacationIncentiveFromVacations.Sum(Function(x) x.Accrued)
                            End If
                        End If

                        'Variable para identificar los meses trabajados menos los meses No remunerados.
                        VarLicensesMonth = MonthWorked - VarLicensesMonth

                        Dim ReplaceFormulateIncentive As String = ""

                        Dim IncentivePaymentValueWithoutDiscount = Me.ReplaceDataFormulates(
                            FormulatesIncentivePayment,
                            BasicSalary,
                            LegalMinimunSalary,
                            RepresentationCost,
                            BonificationValue,
                            TransportHealthValue,
                            IncentivePaymentAverage,
                            IncentiveDays,
                            AverageIBCIncentivePaymentValue,
                            SalaryAverage,
                            VarSanctionDays,
                            VarUnpaidLicensesDays,
                            AverageHelpTransportValue,
                            MonthIncentivePayment,
                            VacationIncentiveValue,
                            ActualContract.HoursDaily,
                            ingressDate,
                            contractDate,
                            VarLicensesMonth,
                            accumulatedValueAguinaldoBase,
                            ReplaceFormulateIncentive
                        )

                        'Creación del Detalle
                        Dim TmpListDetail As New List(Of Tuple(Of Integer, Integer, Double))

                        If IncentivePaymentValueWithoutDiscount > 0 Then
                            TmpListDetail.Add(New Tuple(Of Integer, Integer, Double)(ConceptId, 1, IncentivePaymentValueWithoutDiscount))
                        End If

                        If IncentivePaymentValueWithoutDiscount > 0 And SessionValues.IndigoCompanyType = 2 Then
                            'Se agrega el concepto de provisión de cesantías por petición del bug 7738 att: Carlos Mario
                            'Primero obteniendo el concepto por la clase 008 que es la de provisión de cesantías, despues se agrega 
                            'a la tupla con el id del concepto obtenido, el tipo 1 y el valor es la multiplicación de IncentivePaymentValueWithoutDiscount * 0.0833
                            Dim conceptProvision = conceptCache("008")
                            If conceptProvision IsNot Nothing AndAlso conceptProvision.Id > 0 Then
                                TmpListDetail.Add(New Tuple(Of Integer, Integer, Double)(conceptProvision.Id, 1, IncentivePaymentValueWithoutDiscount * 0.0833))
                            End If

                        End If

                        Dim ListManualConceptsIncentive = _ManualConceptsRepository.GetManualConceptsByEmployeeIdInitialDate(ActualContract.EmployeeId, InitialDateIncentivePayment, 1, IncentivePaymentType)

                        If ListManualConceptsIncentive IsNot Nothing AndAlso ListManualConceptsIncentive.Count > 0 Then
                            For Each ObjManualConcept As ManualConcepts In ListManualConceptsIncentive
                                TmpListDetail.Add(New Tuple(Of Integer, Integer, Double)(ObjManualConcept.ConceptId, ObjManualConcept.Concept.ConceptType, ObjManualConcept.QuoteValue))
                            Next
                        End If
                        'Calculo la base de la retencion
                        Dim listConcept = _conceptRepository.GetConceptIds((From x In TmpListDetail Select x.Item1).ToList())
                        Dim retentionBase As Decimal = 0
                        Dim baseGravable As Decimal = 0
                        Dim retentionAticle As Integer
                        Dim messageRetention As String = ""
                        Dim RetentionValue As Double = 0
                        For Each itemDetail In TmpListDetail
                            If (From x In listConcept Where x.Id = itemDetail.Item1 Select x).SingleOrDefault().AffectIBCRTF = True Then
                                If (From x In listConcept Where x.Id = itemDetail.Item1 Select x).SingleOrDefault().ConceptType = 1 Then
                                    retentionBase += itemDetail.Item3
                                Else
                                    retentionBase -= itemDetail.Item3
                                End If
                            End If
                        Next
                        If PaymentType = "2" Then
                            Dim ObjRetention = _LiquidationDomain.Retention(retentionBase, 0, 0, 0, 0, 0, 0, 0, 0, 0, Group.PayrollParameter.UVTValue, Group.PayrollParameter.RTFExemptPercentage, Group.PayrollParameter.LegalSalaryMinimum, ObjEmployeeLiquidation.ProcedureTypeRTF, ActualContract, ObjEmployeeLiquidation, IncentiveEndDate, RepresentationCost, SettingsPayroll, ListLiquidationLastYear, 0, True)
                            For Each ObjTuple As Tuple(Of String, Double) In ObjRetention.Where(Function(x) x.Item1 = "Retenciones")
                                If ObjTuple.Item1 = "Retenciones" Then
                                    baseGravable = (From x In ObjRetention Where x.Item1 = "BaseGrabable" Select x.Item2).Sum()
                                    RetentionValue = ObjTuple.Item2
                                    If RetentionValue = (From x In ObjRetention Where x.Item1 = "Retention383" Select x.Item2).Sum() Then
                                        retentionAticle = 1
                                    Else
                                        retentionAticle = 2
                                    End If
                                    messageRetention = "RETENCIONES: Se calculo así: Ingresos = " + retentionBase.ToString() + " Menos Rentas Exentas = " + (From x In ObjRetention Where x.Item1 = "TotalRentasExentas" Select x.Item2).Sum().ToString() +
                                        " Subtotal (A) = " + (From x In ObjRetention Where x.Item1 = "SubtotalA" Select x.Item2).Sum().ToString() + " Menos Deducciones = " + (From x In ObjRetention Where x.Item1 = "TotalDeducciones" Select x.Item2).Sum().ToString() +
                                        " Subtotal (B) = " + (From x In ObjRetention Where x.Item1 = "SubTotalB" Select x.Item2).Sum().ToString() + " Menos Renta Exenta = " + (From x In ObjRetention Where x.Item1 = "MenosRenta" Select x.Item2).Sum().ToString() +
                                        " Base Grabable = " + (From x In ObjRetention Where x.Item1 = "BaseGrabable" Select x.Item2).Sum().ToString() + " Articulo 383 = " + (From x In ObjRetention Where x.Item1 = "Retention383" Select x.Item2).Sum().ToString() +
                                        " Articulo 384 = " + (From x In ObjRetention Where x.Item1 = "Retention384" Select x.Item2).Sum().ToString()
                                End If
                            Next
                        End If
                        If RetentionValue > 0 Then
                            Dim ObjConceptRetention = conceptCache("701")
                            TmpListDetail.Add(New Tuple(Of Integer, Integer, Double)(ObjConceptRetention.Id, 2, RetentionValue))
                        End If

                        Dim TotalAccrued As Double = 0
                        Dim TotalDeducted As Double = 0

                        'Embargos
                        Dim PercentageForeclousure As Decimal = 0D

                        ' Obtener solo embargos válidos(activos y que tengan marcada la afectación a las primas)
                        Dim listForeclosureValidated = _foreclousureRepository.GetForeclousureByEmployeeStatus(ActualContract.EmployeeId, 2).Where(Function(x) x.InitialDate <= EndDateIncentivePayment AndAlso (x.Affect1erIncentivePayment OrElse x.Affect2doIncentivePayment)).ToList()

                        If listForeclosureValidated.Any() Then

                            For Each groups In listForeclosureValidated.GroupBy(Function(x) x.IdConcept)

                                Dim conceptIds = groups.Key
                                Dim foreclosureGroup = groups.ToList()
                                Dim concept = _conceptRepository.GetConceptId(conceptIds)
                                'El concepto debe pertenecer a la clase de embargo
                                If concept IsNot Nothing AndAlso concept.ConceptClass = "053" Then
                                    Dim valueForeclosure = CaculateValueForeclousure(foreclosureGroup, PercentageForeclousure)
                                    Dim deductionValue As Decimal

                                    If valueForeclosure > 0 Or PercentageForeclousure > 0 Then
                                        If PercentageForeclousure > 0D Then
                                            Dim accruedTotal = TmpListDetail.Where(Function(t) t.Item2 = 1).Sum(Function(t) t.Item3)
                                            deductionValue = accruedTotal * PercentageForeclousure / 100D
                                            PercentageForeclousure = 0D
                                        Else
                                            deductionValue = valueForeclosure
                                        End If
                                        TmpListDetail.Add(New Tuple(Of Integer, Integer, Double)(concept.Id, 2, deductionValue))
                                    End If

                                End If
                            Next
                        End If

                        For Each ObjDetailTuple As Tuple(Of Integer, Integer, Double) In TmpListDetail
                            Dim IncentivePaymentDetail = New IncentivePaymentDetail()
                            Dim AccruedValue As Double = 0
                            Dim DeductedValue As Double = 0

                            ' Usar cache para GetConceptId
                            Dim ObjConcept As Concept
                            If Not conceptByIdCache.ContainsKey(ObjDetailTuple.Item1) Then
                                conceptByIdCache(ObjDetailTuple.Item1) = _conceptRepository.GetConceptId(ObjDetailTuple.Item1)
                            End If
                            ObjConcept = conceptByIdCache(ObjDetailTuple.Item1)

                            If ObjDetailTuple.Item2 = 1 Then
                                IncentivePaymentDetail.AccruedValue = ObjDetailTuple.Item3
                                IncentivePaymentDetail.DeductedValue = 0
                                If ObjConcept.ConceptType <> 3 Then
                                    TotalAccrued += ObjDetailTuple.Item3
                                End If
                            Else
                                IncentivePaymentDetail.DeductedValue = ObjDetailTuple.Item3
                                IncentivePaymentDetail.AccruedValue = 0
                                TotalDeducted += ObjDetailTuple.Item3
                            End If

                            IncentivePaymentDetail.ConceptName = ObjConcept.Name
                            IncentivePaymentDetail.ConceptId = ObjDetailTuple.Item1
                            IncentivePaymentDetail.ConceptType = ObjConcept.ConceptType
                            IncentivePaymentDetail.ConceptFormulate = FormulatesIncentivePayment
                            IncentivePaymentDetail.ReplaceConceptFormulate = ReplaceFormulateIncentive

                            IncentivePayment.IncentivePaymentDetail.Add(IncentivePaymentDetail)
                        Next

                        'Creación de la Cabecera
                        IncentivePayment.IBCUnemployment = 0
                        IncentivePayment.RetentionBase = baseGravable
                        IncentivePayment.ProcedureTypeRTF = ActualContract.Employee.ProcedureTypeRTF
                        IncentivePayment.TypeArticleRTF = retentionAticle
                        IncentivePayment.MessageRTF = messageRetention
                        IncentivePayment.ContractId = ActualContract.Id
                        IncentivePayment.GroupId = Group.Id
                        IncentivePayment.PaymentType = PaymentType
                        IncentivePayment.Period = Period
                        IncentivePayment.PeriodInitialDate = InitialDateIncentivePayment
                        IncentivePayment.PeriodEndDate = EndDateIncentivePayment
                        IncentivePayment.ContractInitialDate = ActualContract.JobBondingDate
                        IncentivePayment.BasicSalary = BasicSalary
                        IncentivePayment.TransportHelpValue = ValueAuxTransporte
                        IncentivePayment.AverageSalary = AverageIBCIncentivePaymentValue
                        IncentivePayment.SanctionDays = VarSanctionDays + VarUnpaidLicensesDays
                        IncentivePayment.WorkingDays = IncentiveDays
                        IncentivePayment.Month = MonthIncentivePayment
                        IncentivePayment.PayrollNextDate = Group.NextDateLiquidation
                        IncentivePayment.PaidDays = DaysPromedy - VarSanctionDays - VarUnpaidLicensesDays ' IncentiveDay - VarSanctionDays - VarUnpaidLicensesDays
                        IncentivePayment.RegisterStatus = 1
                        IncentivePayment.TotalAccrued = Math.Round(TotalAccrued, 0)
                        IncentivePayment.TotalDeducted = Math.Round(TotalDeducted, 0)
                        IncentivePayment.RetentionValue = RetentionValue
                        IncentivePayment.PaidValue = Math.Round(TotalAccrued - TotalDeducted, 0)

                        IncentivePayment.FullNameEmployee = ActualContract.Employee.ThirdParty.Nit + " - " + ActualContract.Employee.ThirdParty.Name
                        IncentivePayment.NitEmployee = ActualContract.Employee.ThirdParty.Nit
                        IncentivePayment.GroupName = Group.Name
                        IncentivePayment.PositionName = ObjPosition.Name
                        IncentivePayment.ContractNumber = ActualContract.InitialContractNumber

                        Dim EmployeeSalaryType = _contractRepository.GetContractById(ActualContract.Id, True)

                        If ActualGroup IsNot Nothing AndAlso ActualGroup.Id = Group.Id Then
                            IncentivePaymentList.Add(IncentivePayment)
                        Else
                            If EmployeeSalaryType.ContractType.SalaryType = 2 And ActualGroup.Id <> Group.Id Then
                                IncentivePaymentList.Add(IncentivePayment)
                            End If
                        End If

                        IncentivePayment = New IncentivePayment()

                    Next
                End If

            Else
                ' Para pago de Primas por Liquidación de Contrato

                IncentivePayment = New IncentivePayment()
                Dim contractLiquidationIncentive As New List(Of Liquidation)
                Dim LiquidationProcess As New List(Of Liquidation)

                Dim ContractId = contract.Id
                If contract.InitialContractNumber = 0 Then
                    ContractId = ContractId
                Else
                    ContractId = contract.InitialContractNumber
                End If

                Dim contractLiquidation = _liquidationRepository.LiquidationEmployeeByDate(contract.EmployeeId, IncentiveStarDate, IncentiveEndDate)

                If contractLiquidation.Count() > 0 Then
                    For i As Integer = 0 To contractLiquidation.Count() - 1
                        LiquidationProcess.Add(contractLiquidation.Item(i))
                    Next
                End If

                ' ExtraLiquidation.ProvisionIncentive = (ExtraLiquidation.ProvisionIncentive * ExtraLiquidation.DaysWorked) / 30
                If ExtraLiquidation IsNot Nothing Then
                    If contractLiquidation.Any(Function(x) x.PayrollDateLiquidated = ExtraLiquidation.PayrollDateLiquidated) = False Then
                        LiquidationProcess.Add(ExtraLiquidation)
                    End If
                End If

                Dim RetireEmployeeLiquidationContract As New List(Of Liquidation)
                RetireEmployeeLiquidationContract = LiquidationProcess

                Dim LiquidationDetail As New List(Of LiquidationDetail)

                ' Realizamos la Sumatoria de los IBC's de Primas
                For j As Integer = 0 To (RetireEmployeeLiquidationContract.Count() - 1)
                    LiquidationDetail = RetireEmployeeLiquidationContract(j).LiquidationDetail.Where(Function(x) x.ConceptClass <> "005" AndAlso x.ConceptClass <> "006" AndAlso x.Concept.AffectIBCIncentivePayment = True).ToList()
                    If LiquidationDetail IsNot Nothing AndAlso LiquidationDetail.Count() > 0 Then
                        For k As Integer = 0 To LiquidationDetail.Count() - 1
                            'If LiquidationDetail.Item(k).Concept.AffectIBCIncentivePayment = True Then
                            If LiquidationDetail.Item(k).ConceptType = 1 Then
                                AverageIBCIncentivePaymentValue = AverageIBCIncentivePaymentValue + LiquidationDetail.Item(k).ConceptTotalValue
                            Else
                                AverageIBCIncentivePaymentValue = AverageIBCIncentivePaymentValue - LiquidationDetail.Item(k).ConceptTotalValue
                            End If
                        Next
                    End If

                    SanctionDays = SanctionDays + IIf(RetireEmployeeLiquidationContract(j).SanctionDays Is Nothing, 0, RetireEmployeeLiquidationContract(j).SanctionDays)

                    If IncentiveStarDate < contract.JobBondingDate Then
                        WorkDays = DateDiff(DateInterval.Day, contract.JobBondingDate, IncentiveEndDate) + 1
                    Else
                        WorkDays = DateDiff(DateInterval.Day, IncentiveStarDate, IncentiveEndDate) + 1
                    End If



                Next

                Dim BonificationValue As Double = 0

                Dim BonificacionList = _ManualConceptsRepository.GetManualConceptsByContractNumberPayrollDate(IIf(contract.InitialContractNumber = 0, contract.Id, contract.InitialContractNumber), IncentiveStarDate, IncentiveEndDate, 1, 1)

                If BonificacionList IsNot Nothing Then
                    BonificacionList = BonificacionList.Where(Function(x) x.Concept.ConceptClass = "004").ToList()

                    For bon As Integer = 0 To BonificacionList.Count() - 1
                        BonificationValue = BonificacionList.Item(bon).QuoteValue
                    Next
                Else
                    BonificationValue = 0
                End If



                If contract.BasicSalary <= 2 * contract.Group.PayrollParameter.LegalSalaryMinimum Then
                    ValueAuxTransporte = contract.Group.PayrollParameter.TransportHelpValue
                Else
                    ValueAuxTransporte = 0
                End If

                Dim WorkDaysIncentive = _LiquidationDomain.Days360(IncentiveStarDate, IncentiveEndDate)

                If IncentiveEndDate.Month = 2 AndAlso contract.Group.Month = 2 Then

                    If IncentiveEndDate.Day = 28 Then
                        WorkDaysIncentive = WorkDaysIncentive - 2
                    End If

                    If IncentiveEndDate.Day = 29 Then
                        WorkDaysIncentive = WorkDaysIncentive - 1
                    End If

                End If

                Dim UnpaidLicensesNovelty = _noveltyRepository.GetNoveltyUnpaidLicenses(contract.EmployeeId)
                Dim VarUnpaidLicensesDays As Integer = 0
                If UnpaidLicensesNovelty IsNot Nothing And UnpaidLicensesNovelty.Count > 0 Then
                    VarUnpaidLicensesDays = Me.InabilitiesPeriodDays(UnpaidLicensesNovelty, IncentiveStarDate, IncentiveEndDate)
                End If

                WorkDaysIncentive = WorkDaysIncentive - VarUnpaidLicensesDays

                AverageIBCIncentivePaymentValue = AverageIBCIncentivePaymentValue + BasePrimasCesantias

                If WorkDaysIncentive > 30 Then
                    AverageIBCIncentivePaymentValue = (AverageIBCIncentivePaymentValue / WorkDaysIncentive) * 30
                End If

                If AverageIBCIncentivePaymentValue < 0 Then
                    AverageIBCIncentivePaymentValue = 0
                End If

                IncentivePaymentValue = ((contract.BasicSalary + ValueAuxTransporte + AverageIBCIncentivePaymentValue) * (WorkDaysIncentive)) / 360
                'IncentivePaymentValue = ((contract.BasicSalary + ValueAuxTransporte + BonificationValue + AverageIBCIncentivePaymentValue) * (WorkDaysIncentive)) / 360
                ConceptFormulate = "(([Salario Básico] + [Aux. Transporte] + [Salario Variable Primas]) * ([Dias Trabajados])) / 360"
                ReplaceFormulate = "((" + contract.BasicSalary.ToString() + " + " + ValueAuxTransporte.ToString() + " + " + AverageIBCIncentivePaymentValue.ToString() + ") * (" + WorkDaysIncentive.ToString() + ")) / 360"


                IncentivePaymentValue = Utils.RoundedValuesByRate(IncentivePaymentValue, Group.PayrollParameter.AproximationValue)

                IncentivePayment.GroupId = Group.Id
                IncentivePayment.PaymentType = PaymentType
                IncentivePayment.Period = Period
                IncentivePayment.PeriodInitialDate = IncentiveStarDate
                IncentivePayment.PeriodEndDate = IncentiveEndDate
                IncentivePayment.ContractId = contract.Id
                IncentivePayment.ContractInitialDate = contract.JobBondingDate
                IncentivePayment.BasicSalary = contract.BasicSalary
                IncentivePayment.TransportHelpValue = ValueAuxTransporte
                IncentivePayment.AverageSalary = AverageIBCIncentivePaymentValue
                IncentivePayment.SanctionDays = SanctionDays
                IncentivePayment.WorkingDays = WorkDays
                IncentivePayment.PayrollNextDate = Group.NextDateLiquidation
                IncentivePayment.PaidDays = WorkDays
                IncentivePayment.PaidValue = IncentivePaymentValue

                IncentivePaymentList.Add(IncentivePayment)

            End If
            actionResult.ObjectEmbbeded = IncentivePaymentList
            actionResult.StateResult = True

            Return actionResult
        Catch ex As Exception
            actionResult.StateResult = False
            actionResult.Message = ex.Message
            Return actionResult
        End Try

    End Function

    ''' <summary>
    ''' Calcula el valor del embargo acumulado de 1 sola cuota
    ''' </summary>
    ''' <param name="listForeclosure"></param>
    ''' <param name="percentageForeclosure"></param>
    ''' <returns></returns>
    Public Function CaculateValueForeclousure(listForeclosure As List(Of Foreclousure), ByRef percentageForeclosure As Decimal) As Decimal
        ' Sumar porcentaje de embargos con clase 1
        percentageForeclosure += listForeclosure.Where(Function(f) f.DiscountClass = 1).Sum(Function(f) f.Percentage)

        ' Sumar valor de embargos con clase 2
        Return listForeclosure.Where(Function(f) f.DiscountClass = 2).Sum(Function(f) f.QuoteValue)
    End Function

    ''' <summary>
    ''' Calcula el valor de la variable Valor Acumulado Base Aguinaldo
    ''' </summary>
    ''' <param name="contract"></param>
    ''' <param name="payrollSettings"></param>
    ''' <returns></returns>
    Private Function CalculateAccumulatedValueAguinaldoBase(contract As Entities.Contract, payrollSettings As PayrollSettings) As Decimal
        Dim initialDate = payrollSettings.InitialDateServicesIncentivePayment
        Dim endDate = payrollSettings.EndDateServicesIncentivePayment
        Dim variableSalary As Decimal = 0
        Dim liquidations = _liquidationRepository.LiquidationEmployeeByDateAndContractStatus(contract.EmployeeId, initialDate, endDate, {CByte(1), CByte(4), CByte(5)}.ToList())
        For Each liquidation In liquidations
            Dim csum = liquidation.LiquidationDetail.Where(Function(m) m.ConceptType = 1 _
                        AndAlso m.Concept.AffectIBCIncentivePayment).Sum(Function(m) m.AccruedValue)
            variableSalary = variableSalary + csum
        Next

        Return variableSalary
    End Function

    ''' <summary>
    ''' Función que se utiliza para Hallas las FEchas Iniciales y Finales de Acuerdo al Periodo y el número de Primas al año
    ''' </summary>
    ''' <param name="NumberIncentivePayment">Número de Primas al año</param>
    ''' <param name="Period">Periodo</param>
    ''' <param name="Year">Año</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInitialEndDatePeriod(ByVal NumberIncentivePayment As Byte, Period As Char, Year As Integer, CompanyType As Integer) As Dictionary(Of String, Date) Implements IIncentivePaymentDomain.GetInitialEndDatePeriod
        Dim InitialDate As Date
        Dim EndDate As Date
        Dim InitialMonth As Byte
        Dim InitialDay As Byte = 1
        Dim EndMonth As Byte
        Dim EndDay As Byte

        Dim Dates As Dictionary(Of String, Date)

        If NumberIncentivePayment = 1 Then ' Para Primas que se pagan 1 VEZ AL AÑO
            InitialMonth = 1
            EndMonth = 12
            EndDay = 31
        ElseIf NumberIncentivePayment = 2 Then ' Para Primas que se pagan DOS VECES AL AÑO

            If CompanyType = 1 Then
                If Period = "1" Then ' Periodo 1 (Enero a Junio)
                    InitialMonth = 1
                    EndMonth = 6
                    EndDay = 30
                Else ' Periodo 2 (Julio a Diciembre)
                    InitialMonth = 7
                    EndMonth = 12
                    EndDay = 31
                End If
            Else
                If Period = "1" Then ' Periodo (Prima de Servicios)
                    InitialMonth = 7
                    EndMonth = 6
                    EndDay = 30
                Else ' Periodo 2 (Julio a Diciembre)
                    InitialMonth = 7
                    EndMonth = 12
                    EndDay = 31
                End If
            End If
        ElseIf NumberIncentivePayment = 3 Then ' Para Primas que se pagan 3 VECES AL AÑO
            If Period = "1" Then ' Periodo 1 (Enero a Abril)
                InitialMonth = 1
                EndMonth = 4
                EndDay = 30
            ElseIf Period = "2" Then ' Periodo 2 (Mayo - Agosto)
                InitialMonth = 5
                EndMonth = 8
                EndDay = 31
            Else ' Periodo 3 (Septiembre - Diciembre)
                InitialMonth = 9
                EndMonth = 12
                EndDay = 31
            End If
        ElseIf NumberIncentivePayment = 4 Then ' Para Primas que se pagan 4 VECES AL AÑO
            If Period = "1" Then ' Periodo 1 (Enero a Marzo)
                InitialMonth = 1
                EndMonth = 3
                EndDay = 31
            ElseIf Period = "2" Then ' Periodo 2 (Abril - Junio)
                InitialMonth = 4
                EndMonth = 6
                EndDay = 30
            ElseIf Period = "3" Then ' Periodo 3 (Julio - Septiembre)
                InitialMonth = 7
                EndMonth = 9
                EndDay = 30
            Else ' Periodo 4 (Octubre - Diciembre)
                InitialMonth = 10
                EndMonth = 12
                EndDay = 31
            End If
        ElseIf NumberIncentivePayment = 6 Then ' Para Primas que se pagan 6 VECES AL AÑO
            If Period = "1" Then ' Periodo 1 (Enero a Febrero)
                InitialMonth = 1
                EndMonth = 2
                EndDay = 28
            ElseIf Period = "2" Then ' Periodo 2 (Marzo - Abril)
                InitialMonth = 3
                EndMonth = 4
                EndDay = 30
            ElseIf Period = "3" Then ' Periodo 3 (Mayo - Junio)
                InitialMonth = 5
                EndMonth = 6
                EndDay = 30
            ElseIf Period = "4" Then ' Periodo 4 (Julio a Agosto)
                InitialMonth = 7
                EndMonth = 8
                EndDay = 31
            ElseIf Period = "5" Then ' Periodo 5 (Septiembre - Octubre)
                InitialMonth = 9
                EndMonth = 10
                EndDay = 31
            Else ' Periodo 6 (Noviembre a Diciembre)
                InitialMonth = 11
                EndMonth = 12
                EndDay = 31
            End If
        Else 'PAra Primas que se pagan mensuales
            If Period = "1" Then ' Periodo 1 (Enero)
                InitialMonth = 1
                EndDay = 31
            ElseIf Period = "2" Then ' Periodo 2 (Febrero)
                InitialMonth = 2
                EndDay = 28
            ElseIf Period = "3" Then ' Periodo 3 (Marzo)
                InitialMonth = 3
                EndDay = 31
            ElseIf Period = "4" Then ' Periodo 4 (Abril)
                InitialMonth = 4
                EndDay = 30
            ElseIf Period = "5" Then ' Periodo 5 (Mayo)
                InitialMonth = 5
                EndDay = 31
            ElseIf Period = "6" Then ' Periodo 6 (Junio)
                InitialMonth = 6
                EndDay = 30
            ElseIf Period = "7" Then ' Periodo 7 (Julio)
                InitialMonth = 7
                EndDay = 31
            ElseIf Period = "8" Then ' Periodo 8 (Agosto)
                InitialMonth = 8
                EndDay = 31
            ElseIf Period = "9" Then ' Periodo 9 (Septiembre)
                InitialMonth = 9
                EndDay = 30
            ElseIf Period = "10" Then ' Periodo 10 (Octubre)
                InitialMonth = 10
                EndDay = 31
            ElseIf Period = "11" Then ' Periodo 11 (Noviembre)
                InitialMonth = 11
                EndDay = 30
            ElseIf Period = "12" Then ' Periodo 12 (Diciembre)
                InitialMonth = 12
                EndDay = 31
            End If
            EndMonth = InitialMonth
        End If

        If CompanyType = 1 Then
            InitialDate = New Date(Year, InitialMonth, InitialDay)
            EndDate = New Date(Year, EndMonth, EndDay)
        Else
            InitialDate = New Date(Year - 1, InitialMonth, InitialDay)
            EndDate = New Date(Year, EndMonth, EndDay)
        End If

        Dates = New Dictionary(Of String, Date)
        Dates.Add("InitialDate", InitialDate)
        Dates.Add("EndDate", EndDate)

        Return Dates

    End Function

    ''' <summary>
    ''' Función que escoge el Banco y arma el archivo correspondiente
    ''' </summary>
    ''' <param name="ListIncentivePayment">Lista de Primas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectBankEmployee(ListIncentivePayment As List(Of IncentivePayment), Bank As Domain.Payroll.Entities.Bank, BankAccount As String, AccountType As Integer, Company As Domain.Payroll.Entities.Company) As ActionMessageResult(Of StringBuilder) Implements IIncentivePaymentDomain.SelectBankEmployee
        Dim resultActionMessage As New ActionMessageResult(Of StringBuilder)()
        Dim result As New StringBuilder()
        Dim VarListMessageResult As New List(Of MessageResult)

        Select Case Bank.BankFileCode
            Case "001"
                result = GenerateArchiveBancolombia(ListIncentivePayment, BankAccount, AccountType, Company)
            Case "002"
                result = GenerateArchiveAvVillas(ListIncentivePayment)
            Case "003"
                result = GenerateArchiveBancoPopular(ListIncentivePayment, BankAccount, AccountType, Company)
            Case "004"
                result = GenerateArchiveBancoOccidente(ListIncentivePayment, BankAccount, Company, Bank)
            Case "005"
                result = GenerateArchiveBancoBBVA(ListIncentivePayment, AccountType)
            Case "006"
                result = GenerateArchiveDavivienda(ListIncentivePayment, BankAccount, AccountType, Company, Bank)
            Case "008"
                result = GenerateArchiveBancoBogota(ListIncentivePayment, BankAccount, AccountType, Company)
            Case "009"
                result = GenerateArchiveBancolombiaSAP(ListIncentivePayment, BankAccount, AccountType, Company)
            Case "014"
                result = GenerateArchiveDaviviendaCR(ListIncentivePayment)
            Case Else
                resultActionMessage.StateResult = False
                resultActionMessage.Message = $"El formato de archivo plano para el banco con código '{Bank.BankFileCode}' ({Bank.Name}) no está implementado"
                Return resultActionMessage
        End Select

        VarListMessageResult.Add(New MessageResult("-001"))
        resultActionMessage.MessageResult = VarListMessageResult
        resultActionMessage.StateResult = True
        resultActionMessage.ObjectEmbbeded = result

        Return resultActionMessage
    End Function

    Private Function GetVerificationCode(ByVal nit As String) As Integer
        nit = Format(Val("" & nit), "000000000000000")
        Dim residue As Integer = 0
        Dim mul As Integer = 0

        For i As Integer = 15 To 1 Step -1
            If i = 15 Then
                mul = 3
            ElseIf i = 14 Then
                mul = 7
            ElseIf i = 13 Then
                mul = 13
            ElseIf i = 12 Then
                mul = 17
            ElseIf i = 11 Then
                mul = 19
            ElseIf i = 10 Then
                mul = 23
            ElseIf i = 9 Then
                mul = 29
            ElseIf i = 8 Then
                mul = 37
            ElseIf i = 7 Then
                mul = 41
            ElseIf i = 6 Then
                mul = 43
            ElseIf i = 5 Then
                mul = 47
            ElseIf i = 4 Then
                mul = 53
            ElseIf i = 3 Then
                mul = 59
            ElseIf i = 2 Then
                mul = 67
            Else
                mul = 71
            End If
            residue = residue + (Val(GetChar(nit, i)) * mul)
        Next
        residue = residue Mod 11

        If residue = 0 Then
            residue = 0
        ElseIf residue = 1 Then
            residue = 1
        Else
            residue = 11 - residue
        End If

        Return residue
    End Function

    Private Function GetChar(ByVal text As String, ByVal position As Integer) As String
        If text.Length >= position AndAlso position > 0 Then
            Return text.Substring(position - 1, 1)
        Else
            Return "0"
        End If
    End Function


    ''' <summary>
    ''' Genera el archivo plano para Bancolombia
    ''' </summary>
    ''' <param name="ListIncentivePayment">liquidaciones de Primas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateArchiveBancolombia(ListIncentivePayment As List(Of IncentivePayment), BankAccount As String, AccountType As Integer, Company As Domain.Payroll.Entities.Company) As StringBuilder
        Dim result As New StringBuilder()

        If ListIncentivePayment IsNot Nothing AndAlso ListIncentivePayment.Count > 0 Then
            Dim DateNow As String = Date.Now.ToString("yyyyMMdd")
            Dim BankTypeAccount As String = IIf(AccountType = 1, "S", "D")
            Dim totalRegister As Integer = ListIncentivePayment.Count()
            Dim TotalPaid As Double = 0
            For i As Integer = 0 To ListIncentivePayment.Count() - 1
                TotalPaid = TotalPaid + ListIncentivePayment.Item(i).PaidValue
            Next
            TotalPaid = TotalPaid * 100

            Dim lineHead As String = Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(Company.Nit, 15, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("I", 1, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 15, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(225, 3, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("PRI" & MonthName(Month(ListIncentivePayment.Item(0).PeriodEndDate)).ToUpper(), 10, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(DateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("A", 2, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(DateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(totalRegister, 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(0, 17, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(TotalPaid, 17, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(BankAccount, 11, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(BankTypeAccount, 1, "", Utils.PadType.STR_PAD_LEFT)
            result.Append(lineHead)

            For Each IncentivePayment As IncentivePayment In ListIncentivePayment
                Dim TotalPaidEmployee = IncentivePayment.PaidValue * 100

                Dim BankAccountTypeCode As String
                Select Case IncentivePayment.Contract.BankAccountType
                    Case 1 ' Ahorros
                        BankAccountTypeCode = "37"
                    Case 2 ' Corriente
                        BankAccountTypeCode = "27"
                    Case Else 'Ahorros por defecto
                        BankAccountTypeCode = "37"
                End Select
                Dim employeeBank = _bankRepository.GetBankById(IncentivePayment.Contract.BankId)
                Dim bankAchCode As String = If(employeeBank IsNot Nothing AndAlso Not String.IsNullOrEmpty(employeeBank.AchCode), employeeBank.AchCode, "005600078")

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad(6, 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.Employee.ThirdParty.Nit, 15, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.Employee.ThirdParty.Name, 30, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(bankAchCode, 9, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.BankAccountNumber, 17, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(" ", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(BankAccountTypeCode, 2, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(TotalPaidEmployee, 17, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(0, 8, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("PRI" & MonthName(Month(ListIncentivePayment.Item(0).PeriodEndDate)).ToUpper() & " " & DateNow, 21, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(" ", 6, " ", Utils.PadType.STR_PAD_LEFT)
                result.Append(lineDet)
            Next
        End If

        Return result
    End Function

    ''' <summary>
    ''' Genera el archivo plano para BBVA
    ''' </summary>
    ''' <param name="ListIncentivePayment">liquidaciones de Primas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateArchiveBancoBBVA(ListIncentivePayment As List(Of IncentivePayment), BankAccountType As Integer) As StringBuilder
        Dim result As New StringBuilder()
        Dim DateTransaction = Date.Now
        Dim DateNow As String = DateTransaction.ToString("yyyyMMdd")
        Dim TotalPaid As Double = 0
        Dim BankTypeAccount As Char

        For i As Integer = 0 To ListIncentivePayment.Count() - 1
            TotalPaid = TotalPaid + (IIf(ListIncentivePayment.Item(i).TotalAccrued IsNot Nothing, ListIncentivePayment.Item(i).TotalAccrued, 0) - IIf(ListIncentivePayment.Item(i).TotalDeducted IsNot Nothing, ListIncentivePayment.Item(i).TotalDeducted, 0))
        Next

        TotalPaid = TotalPaid * 100

        If BankAccountType = 1 Then '1. Ahorros
            BankTypeAccount = "S"
        Else ' 2. Corriente
            BankTypeAccount = "D"
        End If

        If ListIncentivePayment IsNot Nothing AndAlso ListIncentivePayment.Count > 0 Then
            For Each ObjLiquidation As IncentivePayment In ListIncentivePayment
                Dim lineDet As String '= vbCrLf
                'tipo de documento
                Dim documentType As String = "01"
                Dim ObjPerson = _personRepository.GetPersonByIdentification(ObjLiquidation.Contract.Employee.ThirdParty.Nit)

                If ObjPerson IsNot Nothing Then

                    Select Case ObjPerson.IdentificationType
                        Case 0 'cedula 
                            documentType = "01"
                        Case 1 ' cedula de extranjeria
                            documentType = "02"
                        Case 2 'tarjeta de identidad
                            documentType = "04"
                        Case 4 'pasaporte
                            documentType = "05"
                        Case 7 'nit
                            documentType = "03"
                    End Select
                Else
                    documentType = "01"
                End If

                lineDet = Utils.StringPad(documentType, 2, "", Utils.PadType.STR_PAD_RIGHT)
                lineDet += Utils.StringPad(String.Concat(ObjLiquidation.Contract.Employee.ThirdParty.Nit, 0), 16, 0, Utils.PadType.STR_PAD_LEFT)

                Dim ObjBank = _bankRepository.GetBankById(ObjLiquidation.Contract.BankId)

                'forma de pago
                lineDet += Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)

                'banco cuenta del beneficiario
                'lineDet += Utils.StringPad(ObjBank.Code, 4, 0, Utils.PadType.STR_PAD_LEFT)

                Dim safeCodeCenit As String = Utils.StringPad(ObjBank.CenitCode, 4, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet += safeCodeCenit

                Dim accountNumber As String = String.Empty
                Dim accountType As String = String.Empty
                Dim supplierAccountNumber As String = String.Empty

                If ObjBank.CenitCode = "0013" Then
                    accountType = Utils.StringPad(If(ObjLiquidation.Contract.BankAccountType = 1, "02", "01"), 4, 0, Utils.PadType.STR_PAD_RIGHT)
                    Dim office = "0" + Left(ObjLiquidation.Contract.BankAccountNumber.Trim.Replace("-", ""), 3)
                    accountNumber = office + "00" + accountType + Right(ObjLiquidation.Contract.BankAccountNumber.Trim.Replace("-", ""), 6)
                    accountType = "00"
                    supplierAccountNumber = "00000000000000000"
                Else
                    accountNumber = "0000000000000000"
                    accountType = Utils.StringPad(If(ObjLiquidation.Contract.BankAccountType = 1, "02", "01"), 2, 0, Utils.PadType.STR_PAD_RIGHT)
                    supplierAccountNumber = ObjLiquidation.Contract.BankAccountNumber.Trim.Replace("-", "")
                End If

                'numero cuenta BBVA ==============================================================porque va en 0
                lineDet += Utils.StringPad(accountNumber, 16, 0, Utils.PadType.STR_PAD_LEFT)
                'tipo de cuenta 02 = ahorros , 01 = corriente
                lineDet += Utils.StringPad(accountType, 2, "", Utils.PadType.STR_PAD_LEFT)
                'numero cuenta beneficiario
                lineDet += Utils.StringPad(supplierAccountNumber.Trim.Replace("-", ""), 17, " ", Utils.PadType.STR_PAD_LEFT)
                'valor de la transaccion parte entera
                lineDet += Utils.StringPad((IIf(ObjLiquidation.TotalAccrued IsNot Nothing, ObjLiquidation.TotalAccrued, 0) - IIf(ObjLiquidation.TotalDeducted IsNot Nothing, ObjLiquidation.TotalDeducted, 0)), 13, 0, Utils.PadType.STR_PAD_LEFT)
                'valor de la transaccion parte decimal
                lineDet += Utils.StringPad("", 2, 0, Utils.PadType.STR_PAD_RIGHT)
                'fecha - 00000000 se hizo este cambio porque la documentacion enviada lo decia asi
                lineDet += Utils.StringPad("", 8, 0, Utils.PadType.STR_PAD_LEFT)
                'codigo oficna pagadora
                lineDet += Utils.StringPad("0000", 4, 0, Utils.PadType.STR_PAD_RIGHT)
                'nombre del beneficiario
                lineDet += Utils.StringPad(ObjLiquidation.Contract.Employee.ThirdParty.Name, 36, " ", Utils.PadType.STR_PAD_RIGHT)
                'direccion
                lineDet += Utils.StringPad("BOGOTA", 36, " ", Utils.PadType.STR_PAD_RIGHT)
                'direccion 2
                lineDet += Utils.StringPad("", 36, " ", Utils.PadType.STR_PAD_LEFT)
                'email
                lineDet += Utils.StringPad("", 48, " ", Utils.PadType.STR_PAD_LEFT)
                'concepto
                lineDet += Utils.StringPad("PRIMA" & MonthName(Month(ObjLiquidation.PeriodEndDate)).ToUpper() & Year(ObjLiquidation.PeriodEndDate), 40, " ", Utils.PadType.STR_PAD_RIGHT)
                result.AppendLine(lineDet)
            Next
        End If

        Return result
    End Function

    ''' <summary>
    ''' Genera el archivo plano para Davivienda
    ''' </summary>
    ''' <param name="ListIncentivePayment">liquidaciones de Primas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateArchiveDavivienda(ListIncentivePayment As List(Of IncentivePayment), BankAccount As String, AccountType As Integer, Company As Domain.Payroll.Entities.Company, Bank As Domain.Payroll.Entities.Bank) As StringBuilder
        Dim result As New StringBuilder()

        If ListIncentivePayment IsNot Nothing AndAlso ListIncentivePayment.Count > 0 Then
            Dim totalRegister As Integer = ListIncentivePayment.Count
            Dim TotalValue = ListIncentivePayment.Sum(Function(x) x.PaidValue) * 100

            Dim lineHead As String = Utils.StringPad("RC", 2, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(Company.ThirdParty.Nit, 16, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("PRIM", 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("PRIM", 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(BankAccount.Trim.Replace("-", ""), 16, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(IIf(AccountType = 1, "CA", "CC"), 2, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(Bank.CenitCode, 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(TotalValue, 18, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(totalRegister, 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(Date.Now.ToString("yyyyMMdd"), 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(DateTime.Now.ToString("hhmmss"), 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(9999, 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 2, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("03", 2, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 12, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 40, 0, Utils.PadType.STR_PAD_LEFT)
            result.Append(lineHead)

            For Each thirdPartyNit As IncentivePayment In ListIncentivePayment
                Dim totalPaid As Double = thirdPartyNit.PaidValue * 100
                Dim documentType As String = "01"

                Dim ObjPerson = _personRepository.GetPersonByIdentification(thirdPartyNit.Contract.Employee.ThirdParty.Nit)

                Select Case ObjPerson.IdentificationType
                    Case 0 'cedula 
                        documentType = "01"
                    Case 1 ' cedula de extranjeria
                        documentType = "02"
                    Case 2 'tarjeta de identidad
                        documentType = "04"
                    Case 4 'pasaporte
                        documentType = "05"
                    Case 7 'nit
                        documentType = "03"
                End Select


                Dim BankEmployee = _bankRepository.GetBankById(thirdPartyNit.Contract.BankId)

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad("TR", 2, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(thirdPartyNit.Contract.Employee.ThirdParty.Nit, 16, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 16, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(thirdPartyNit.Contract.BankAccountNumber.Trim.Replace("-", ""), 16, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(If(thirdPartyNit.Contract.BankAccountType = 1, "CA", "CC"), 2, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(BankEmployee.CenitCode, 6, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(totalPaid, 18, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 6, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(documentType, 2, "", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(9999, 4, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 40, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 18, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 8, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 4, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 4, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 7, 0, Utils.PadType.STR_PAD_LEFT)
                result.Append(lineDet)
            Next
        End If

        Return result
    End Function

    ''' <summary>
    ''' Genera el archivo plano para Bancolombia SAP
    ''' </summary>
    ''' <param name="ListIncentivePayment">liquidaciones de Primas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateArchiveBancolombiaSAP(ListIncentivePayment As List(Of IncentivePayment), BankAccount As String, AccountType As Integer, Company As Domain.Payroll.Entities.Company) As StringBuilder
        Dim result As New StringBuilder()

        If ListIncentivePayment IsNot Nothing AndAlso ListIncentivePayment.Count > 0 Then
            Dim dateNow As Date = Date.Now
            Dim BankTypeAccount As String = IIf(AccountType = 1, "S", "D")
            Dim totalRegister As Integer = ListIncentivePayment.Count()
            Dim TotalPaid As Double = 0
            For i As Integer = 0 To ListIncentivePayment.Count() - 1
                TotalPaid = TotalPaid + ListIncentivePayment.Item(i).PaidValue
            Next

            Dim lineHead As String = Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(Company.ThirdParty.Nit, 10, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(Company.ThirdParty.Name, 16, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(225, 3, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("PRI" & MonthName(Month(ListIncentivePayment.Item(0).PeriodEndDate)).ToUpper(), 10, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(dateNow.ToString("yyMMdd"), 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("A", 1, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(dateNow.ToString("yyMMdd"), 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(totalRegister, 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(0, 12, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(TotalPaid, 12, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(BankAccount.Trim().Replace("-", ""), 11, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(BankTypeAccount, 1, "", Utils.PadType.STR_PAD_LEFT)
            result.Append(lineHead)

            For Each IncentivePayment As IncentivePayment In ListIncentivePayment
                ' Obtener el código ACH del banco del empleado (igual que en nómina)
                Dim employeeBank = _bankRepository.GetBankById(IncentivePayment.Contract.BankId)
                Dim bankAchCode As String = If(employeeBank IsNot Nothing AndAlso Not String.IsNullOrEmpty(employeeBank.AchCode), employeeBank.AchCode, "005600078")

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad(6, 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.Employee.ThirdParty.Nit, 15, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.Employee.ThirdParty.Name, 18, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(bankAchCode, 9, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.BankAccountNumber.Trim().Replace("-", ""), 17, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("S", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(If(IncentivePayment.Contract.BankAccountType = 1, 37, 27), 2, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.PaidValue, 10, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(dateNow.ToString("yyyyMMdd"), 8, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(" PRIMA", 14, " ", Utils.PadType.STR_PAD_RIGHT)
                result.Append(lineDet)
            Next
        End If

        Return result
    End Function

    ''' <summary>
    ''' Genera el archivo plano para Av Villas
    ''' </summary>
    ''' <param name="ListIncentivePayment">liquidaciones de Primas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateArchiveAvVillas(ListIncentivePayment As List(Of IncentivePayment)) As StringBuilder
        Dim result As New StringBuilder()

        If ListIncentivePayment IsNot Nothing AndAlso ListIncentivePayment.Count > 0 Then
            Dim dateNow As String = Date.Now
            Dim totalRegister As Integer = ListIncentivePayment.Count()
            Dim TotalPaid As Double = ListIncentivePayment.Sum(Function(x) x.PaidValue)

            Dim lineHead As String = Utils.StringPad(1, 2, 0, Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(Format(dateNow, "d"), 8, 0, Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(Format(dateNow, "h:mm:ss"), 6, 0, Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("088", 3, 0, Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("02", 2, 0, Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(" ", 50, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(" ", 120, " ", Utils.PadType.STR_PAD_RIGHT)
            result.Append(lineHead)

            Dim secuence As Integer
            For Each IncentivePayment As IncentivePayment In ListIncentivePayment
                secuence = secuence + 1
                Dim EmployeeBankTypeAccount As String = IIf(IncentivePayment.Contract.BankAccountType = 1, "01", "06")

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad("02", 2, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("000023", 6, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("", 2, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("052", 3, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(EmployeeBankTypeAccount, 2, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.BankAccountNumber, 16, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(secuence, 9, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(IncentivePayment.PaidValue, 18, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(0, 16, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(0, 16, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(0, 16, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.Employee.ThirdParty.Name, 30, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.Employee.ThirdParty.Nit, 11, 0, Utils.PadType.STR_PAD_RIGHT)
                result.Append(lineDet)
            Next

            Dim lineFin As String = vbCrLf
            lineFin &= Utils.StringPad("03", 2, 0, Utils.PadType.STR_PAD_RIGHT)
            lineFin &= Utils.StringPad(totalRegister, 9, 0, Utils.PadType.STR_PAD_RIGHT)
            lineFin &= Utils.StringPad(TotalPaid, 20, 0, Utils.PadType.STR_PAD_RIGHT)
            lineFin &= Utils.StringPad(1, 15, " ", Utils.PadType.STR_PAD_LEFT)
            result.Append(lineFin)
        End If

        Return result
    End Function

    ''' <summary>
    ''' Genera el archivo plano para Banco Popular
    ''' </summary>
    ''' <param name="ListIncentivePayment">liquidaciones de Primas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateArchiveBancoPopular(ListIncentivePayment As List(Of IncentivePayment), BankAccount As String, AccountType As Integer, Company As Domain.Payroll.Entities.Company) As StringBuilder
        Dim result As New StringBuilder()

        If ListIncentivePayment IsNot Nothing AndAlso ListIncentivePayment.Count > 0 Then
            Dim dateNow As String = Date.Now.ToString("yyyyMMdd")
            Dim bankTypeAccount As String = IIf(AccountType = 1, "000", "110")
            Dim totalRegister As Integer = ListIncentivePayment.Count()
            Dim TotalPaid As Double = ListIncentivePayment.Sum(Function(x) x.PaidValue) * 100

            Dim lineHead As String = Utils.StringPad("01", 2, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(dateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(Company.ThirdParty.Name, 16, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankTypeAccount, 3, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(BankAccount, 9, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(Company.ThirdParty.Nit, 10, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(totalRegister, 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(TotalPaid, 18, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 12, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("PRIMA", 6, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 75, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("V", 1, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 41, " ", Utils.PadType.STR_PAD_LEFT)
            result.Append(lineHead)

            For Each IncentivePayment As IncentivePayment In ListIncentivePayment
                Dim totalPaidEmployee As Double = IncentivePayment.PaidValue * 100
                Dim EmployeeBankTypeAccount As String = IIf(IncentivePayment.Contract.BankAccountType = 1, "32", "22")

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad("02", 2, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.Employee.ThirdParty.Nit, 15, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(totalPaidEmployee, 18, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.Employee.ThirdParty.Name, 22, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("000010029", 9, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(EmployeeBankTypeAccount, 2, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.BankAccountNumber, 17, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(Company.ThirdParty.Nit, 17, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("PRIMA", 10, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("0", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("V", 53, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 41, " ", Utils.PadType.STR_PAD_LEFT)
                result.Append(lineDet)
            Next
        End If

        Return result
    End Function

    ''' <summary>
    ''' Genera el archivo plano para Banco Occidente
    ''' </summary>
    ''' <param name="ListIncentivePayment">liquidaciones de Primas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateArchiveBancoOccidente(ListIncentivePayment As List(Of IncentivePayment), BankAccount As String, Company As Domain.Payroll.Entities.Company, Bank As Domain.Payroll.Entities.Bank) As StringBuilder
        Dim result As New StringBuilder()

        If ListIncentivePayment IsNot Nothing AndAlso ListIncentivePayment.Count > 0 Then
            Dim dateNow As String = Date.Now.ToString("yyyyMMdd")
            Dim totalRegister As Integer = ListIncentivePayment.Count()
            Dim TotalPaid As Double = ListIncentivePayment.Sum(Function(x) x.PaidValue) * 100

            Dim lineHead As String = Utils.StringPad("10000", 5, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(dateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(totalRegister, 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(TotalPaid, 18, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(BankAccount, 16, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(0, 148, 0, Utils.PadType.STR_PAD_LEFT)
            result.Append(lineHead)

            Dim secuence As Integer
            For Each IncentivePayment As IncentivePayment In ListIncentivePayment
                secuence = secuence + 1
                Dim totalPaidEmployee As Double = IncentivePayment.PaidValue * 100
                Dim EmployeeBankTypeAccount As String = IIf(IncentivePayment.Contract.BankAccountType = 1, "A", "C")

                Dim employeeBank = _bankRepository.GetBankById(IncentivePayment.Contract.BankId)

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad("2", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(secuence, 4, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(BankAccount, 16, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.Employee.ThirdParty.Name, 30, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.Employee.ThirdParty.Nit, 11, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(employeeBank.CenitCode, 4, "0", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(dateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(If(Bank.CenitCode = employeeBank.CenitCode, "2", "3"), 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(totalPaidEmployee, 15, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.BankAccountNumber, 16, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("", 12, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(EmployeeBankTypeAccount, 1, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("PRIMA" & MonthName(Month(ListIncentivePayment.Item(0).PeriodEndDate)).ToUpper() & Year(ListIncentivePayment.Item(0).PeriodEndDate), 80, " ", Utils.PadType.STR_PAD_RIGHT)
                result.Append(lineDet)
            Next

            Dim lineEnd As String = vbCrLf
            lineEnd &= Utils.StringPad("39999", 5, " ", Utils.PadType.STR_PAD_LEFT)
            lineEnd &= Utils.StringPad(totalRegister, 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineEnd &= Utils.StringPad(TotalPaid, 18, 0, Utils.PadType.STR_PAD_LEFT)
            lineEnd &= Utils.StringPad(0, 172, 0, Utils.PadType.STR_PAD_LEFT)
            result.Append(lineEnd)
        End If

        Return result
    End Function

    ''' <summary>
    ''' Genera el archivo plano para Banco de Bogotá
    ''' </summary>
    ''' <param name="ListIncentivePayment">liquidaciones de Primas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateArchiveBancoBogota(ListIncentivePayment As List(Of IncentivePayment), BankAccount As String, AccountType As Integer, Company As Domain.Payroll.Entities.Company) As StringBuilder
        Dim result As New StringBuilder()

        If ListIncentivePayment IsNot Nothing AndAlso ListIncentivePayment.Count > 0 Then
            Dim dateNow As String = Date.Now.ToString("yyyyMMdd")
            Dim bankTypeAccount As String = IIf(AccountType = 1, "2", "1")
            Dim totalRegister As Integer = ListIncentivePayment.Count()
            Dim TotalPaid As Double = ListIncentivePayment.Sum(Function(x) x.PaidValue)

            Dim lineHead As String = Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(dateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(totalRegister, 5, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(TotalPaid, 16, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankTypeAccount, 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(0, 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(BankAccount, 11, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(Company.ThirdParty.Name, 40, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(Company.ThirdParty.Nit & GetVerificationCode(Company.ThirdParty.Nit), 11, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("002", 3, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("0032", 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("000055", 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(" ", 135, " ", Utils.PadType.STR_PAD_LEFT)
            result.Append(lineHead)

            For Each IncentivePayment As IncentivePayment In ListIncentivePayment
                Dim EmployeeBankTypeAccount As String = IIf(IncentivePayment.Contract.BankAccountType = 1, "2", "1")
                Dim documentType As String
                Dim ObjPerson = _personRepository.GetPersonByIdentification(IncentivePayment.Contract.Employee.ThirdParty.Nit)

                If ObjPerson IsNot Nothing Then
                    Select Case ObjPerson.IdentificationType
                        Case 1 ' cedula de extranjeria
                            documentType = "E"
                        Case 2 'tarjeta de identidad
                            documentType = "T"
                        Case 7 'nit
                            documentType = "N"
                        Case Else 'cedula 
                            documentType = "C"
                    End Select
                Else
                    documentType = "C"
                End If

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad("2", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(documentType, 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.Employee.ThirdParty.Nit, 11, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.Employee.ThirdParty.Name, 40, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(0, 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(EmployeeBankTypeAccount, 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(IncentivePayment.Contract.BankAccountNumber, 17, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(IncentivePayment.PaidValue, 18, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("A", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("000", 3, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("001", 3, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("0001", 4, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(" ", 80, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("0", 11, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("N", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(" ", 48, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("N00000000", 9, " ", Utils.PadType.STR_PAD_LEFT)
                result.Append(lineDet)
            Next
        End If

        Return result
    End Function

    ''' <summary>
    ''' Genera el archivo plano para Davivienda CR (Costa Rica)
    ''' </summary>
    ''' <param name="ListIncentivePayment">liquidaciones de Primas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateArchiveDaviviendaCR(ListIncentivePayment As List(Of IncentivePayment)) As StringBuilder
        Dim result As New StringBuilder()

        If ListIncentivePayment IsNot Nothing AndAlso ListIncentivePayment.Count > 0 Then
            Dim nominaDateText As String = "Pago Primas " & ListIncentivePayment.Item(0).PeriodEndDate.ToString("MMMM dd 'de' yyyy", New CultureInfo("es-CR"))

            For Each IncentivePayment As IncentivePayment In ListIncentivePayment
                Dim lineDet As String = ""
                lineDet &= Utils.StringPad("CMB", 3, 0, Utils.PadType.STR_PAD_LEFT) & ","
                lineDet &= Utils.StringPad(IncentivePayment.Contract.BankAccountNumber.Trim().Replace("-", ""), 11, 0, Utils.PadType.STR_PAD_LEFT) & ","
                lineDet &= "0,"
                lineDet &= "0,"
                lineDet &= IncentivePayment.PaidValue.ToString("F2", CultureInfo.InvariantCulture) & ","
                lineDet &= nominaDateText & ","
                lineDet &= "0,"
                lineDet &= "0,"
                lineDet &= "0"

                result.Append(lineDet & vbCrLf)
            Next
        End If

        Return result
    End Function

    Public Function ReplaceDataFormulates(
        FormulaConcept As String,
        BasicSalary As Double,
        LegalMinumunSalary As Double,
        RepresentationCost As Double,
        BonificationValue As Double,
        TransportHealthValue As Double,
        PaidValueAverageIncentiveServicesValue As Double,
        IncentivePaymentDays As Integer,
        AverageIBCIncentivePayment As Double,
        AverageSalary As Double,
        SanctionDays As Integer,
        UnpaidLicensesDays As Integer,
        AverageHelpTransportValue As Double,
        MonthIncentive As Integer,
        VacationIncentivePayment As Double,
        HorasDiarias As Integer,
        ingressDate As Date,
        contractDate As Date,
        VarLicensesMonth As Integer,
        accumulatedValueAguinaldoBase As Decimal,
        Optional ByRef ReplaceFormulate As String = ""
    ) As Double

        FormulaConcept = Replace(FormulaConcept, "[Salario Mínimo]", Format(LegalMinumunSalary, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Auxilio Transporte]", Format(TransportHealthValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Sueldo Contrato]", Format(BasicSalary, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Gastos de Representación]", Format(RepresentationCost, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Bonificacion Año Servicio]", Format(BonificationValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Prima Servicio]", Format(PaidValueAverageIncentiveServicesValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias del Periodo de Primas]", Format(IncentivePaymentDays, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Salario Variable Primas]", Format(AverageIBCIncentivePayment, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Sueldo Promedio]", Format(AverageSalary, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias de Sanción]", Format(SanctionDays, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Dias de Licencias No Remuneradas]", Format(UnpaidLicensesDays, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Auxilio Transporte Promedio]", Format(AverageHelpTransportValue, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Meses Prima]", Format(MonthIncentive, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Prima Vacaciones]", Format(VacationIncentivePayment, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Horas Diarias]", Format(HorasDiarias, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Fecha Ingreso]", "'" + Format(ingressDate, "dd/MM/yyyy") + "'")
        FormulaConcept = Replace(FormulaConcept, "[Fecha Contratacion]", "'" + Format(contractDate, "dd/MM/yyyy") + "'")
        FormulaConcept = Replace(FormulaConcept, "[Meses Laborados Prima]", Format(VarLicensesMonth, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Valor Acumulado Base Aguinaldo]", Format(accumulatedValueAguinaldoBase, "0.00").Replace(",", "."))

        ReplaceFormulate = FormulaConcept
        Dim result = Utils.EvalExpression(FormulaConcept)
        Dim conceptValue As Decimal = 0

        If result.StateResult Then
            conceptValue = CType(result.ObjectEmbbeded, Decimal)
        End If

        Return conceptValue
    End Function

    Public Function InabilitiesPeriodDays(ListEmployeeNovelty As List(Of Novelty), InitialDateIncentivePayment As Date, EndDateIncentivePayment As Date) As Integer


        Dim DaysInabilities As Integer = 0

        For Each ObjEmployeeNovelty As Novelty In ListEmployeeNovelty
            Dim DaysTmpInabilties As Integer = 0

            If ObjEmployeeNovelty.RealDate >= InitialDateIncentivePayment And ObjEmployeeNovelty.EndDate <= EndDateIncentivePayment Then
                'La Novedad está en el Periodo de Primas
                DaysTmpInabilties = ObjEmployeeNovelty.Days
            End If

            If ObjEmployeeNovelty.RealDate >= InitialDateIncentivePayment And ObjEmployeeNovelty.EndDate > EndDateIncentivePayment Then
                'Si la Novedad inicia en el Periodo de las Primas y terminan después que finalicen las Primas
                DaysTmpInabilties = DaysTmpInabilties + DateDiff(DateInterval.Day, ObjEmployeeNovelty.RealDate, EndDateIncentivePayment) + 1
            End If

            If ObjEmployeeNovelty.RealDate < InitialDateIncentivePayment And ObjEmployeeNovelty.EndDate <= EndDateIncentivePayment Then
                'Si la Novedad antes del Periodo de las Primas y terminan antes que finalicen las Primas
                DaysTmpInabilties = DaysTmpInabilties + DateDiff(DateInterval.Day, InitialDateIncentivePayment, ObjEmployeeNovelty.EndDate) + 1
            End If

            If ObjEmployeeNovelty.EndDate < InitialDateIncentivePayment Then
                'Si la Novedad inicia antes de que inice el Periodo de Primas, no se tiene en cuenta
                DaysTmpInabilties = 0
            End If

            If ObjEmployeeNovelty.RealDate > EndDateIncentivePayment Then
                DaysTmpInabilties = 0
            End If

            DaysInabilities = DaysInabilities + DaysTmpInabilties

        Next

        Return DaysInabilities

    End Function

    Public Function InabilitiesPeriodMonth(ListEmployeeNovelty As List(Of Novelty), InitialDateIncentivePayment As Date, EndDateIncentivePayment As Date) As Integer


        Dim MonthInabilities As Integer = 0
        Dim MonthDictionary As New List(Of Tuple(Of Integer, Integer))


        For Each ObjEmployeeNovelty As Novelty In ListEmployeeNovelty
            Dim MonthTmpInabilties As Integer = 0

            If ObjEmployeeNovelty.RealDate >= InitialDateIncentivePayment AndAlso ObjEmployeeNovelty.EndDate <= EndDateIncentivePayment Then
                'La Novedad está en el Periodo de Primas
                If MonthDictionary.Count = 0 Then
                    MonthDictionary.Add(New Tuple(Of Integer, Integer)(ObjEmployeeNovelty.RealDate.Month, ObjEmployeeNovelty.EndDate.Month))
                    MonthTmpInabilties = MonthTmpInabilties + DateDiff(DateInterval.Month, ObjEmployeeNovelty.RealDate, ObjEmployeeNovelty.EndDate) + 1
                Else
                    If (From x In MonthDictionary Where x.Item2 = ObjEmployeeNovelty.RealDate.Month).Count > 0 Then
                        MonthTmpInabilties = MonthTmpInabilties + DateDiff(DateInterval.Month, ObjEmployeeNovelty.RealDate, ObjEmployeeNovelty.EndDate)
                        MonthDictionary.Clear()
                        MonthDictionary.Add(New Tuple(Of Integer, Integer)(ObjEmployeeNovelty.RealDate.Month, ObjEmployeeNovelty.EndDate.Month))
                    Else
                        MonthTmpInabilties = MonthTmpInabilties + DateDiff(DateInterval.Month, ObjEmployeeNovelty.RealDate, ObjEmployeeNovelty.EndDate) + 1
                        MonthDictionary.Clear()
                        MonthDictionary.Add(New Tuple(Of Integer, Integer)(ObjEmployeeNovelty.RealDate.Month, ObjEmployeeNovelty.EndDate.Month))
                    End If
                End If
            End If

            If ObjEmployeeNovelty.RealDate >= InitialDateIncentivePayment AndAlso ObjEmployeeNovelty.EndDate > EndDateIncentivePayment Then
                'Si la Novedad inicia en el Periodo de las Primas y terminan después que finalicen las Primas
                If MonthDictionary.Count = 0 Then
                    MonthDictionary.Add(New Tuple(Of Integer, Integer)(ObjEmployeeNovelty.RealDate.Month, EndDateIncentivePayment.Month))
                    MonthTmpInabilties = MonthTmpInabilties + DateDiff(DateInterval.Month, ObjEmployeeNovelty.RealDate, EndDateIncentivePayment) + 1
                Else
                    If (From x In MonthDictionary Where x.Item2 = ObjEmployeeNovelty.RealDate.Month).Count > 0 Then
                        MonthTmpInabilties = MonthTmpInabilties + DateDiff(DateInterval.Month, ObjEmployeeNovelty.RealDate, EndDateIncentivePayment)
                        MonthDictionary.Clear()
                        MonthDictionary.Add(New Tuple(Of Integer, Integer)(ObjEmployeeNovelty.RealDate.Month, EndDateIncentivePayment.Month))
                    Else
                        MonthTmpInabilties = MonthTmpInabilties + DateDiff(DateInterval.Month, ObjEmployeeNovelty.RealDate, EndDateIncentivePayment) + 1
                        MonthDictionary.Clear()
                        MonthDictionary.Add(New Tuple(Of Integer, Integer)(ObjEmployeeNovelty.RealDate.Month, EndDateIncentivePayment.Month))
                    End If
                End If
            End If

            If ObjEmployeeNovelty.RealDate < InitialDateIncentivePayment AndAlso ObjEmployeeNovelty.EndDate <= EndDateIncentivePayment Then
                'Si la Novedad antes del Periodo de las Primas y terminan antes que finalicen las Primas
                If MonthDictionary.Count = 0 Then
                    MonthDictionary.Add(New Tuple(Of Integer, Integer)(InitialDateIncentivePayment.Month, ObjEmployeeNovelty.EndDate.Month))
                    MonthTmpInabilties = MonthTmpInabilties + DateDiff(DateInterval.Month, InitialDateIncentivePayment, ObjEmployeeNovelty.EndDate) + 1
                Else
                    If (From x In MonthDictionary Where x.Item2 = InitialDateIncentivePayment.Month).Count > 0 Then
                        MonthTmpInabilties = MonthTmpInabilties + DateDiff(DateInterval.Month, InitialDateIncentivePayment, ObjEmployeeNovelty.EndDate)
                        MonthDictionary.Clear()
                        MonthDictionary.Add(New Tuple(Of Integer, Integer)(InitialDateIncentivePayment.Month, ObjEmployeeNovelty.EndDate.Month))
                    Else
                        MonthTmpInabilties = MonthTmpInabilties + DateDiff(DateInterval.Month, InitialDateIncentivePayment, ObjEmployeeNovelty.EndDate) + 1
                        MonthDictionary.Clear()
                        MonthDictionary.Add(New Tuple(Of Integer, Integer)(InitialDateIncentivePayment.Month, ObjEmployeeNovelty.EndDate.Month))
                    End If
                End If
            End If

            If ObjEmployeeNovelty.EndDate < InitialDateIncentivePayment Then
                'Si la Novedad inicia antes de que inice el Periodo de Primas, no se tiene en cuenta
                MonthTmpInabilties = 0
            End If

            If ObjEmployeeNovelty.RealDate > EndDateIncentivePayment Then
                MonthTmpInabilties = 0
            End If

            MonthInabilities = MonthInabilities + MonthTmpInabilties
        Next

        Return MonthInabilities

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _LiquidationDomain.Dispose()
            End If
            _LiquidationDomain = Nothing
            _liquidationRepository = Nothing
            _ManualConceptsRepository = Nothing
            _PositionRepository = Nothing
            _noveltyRepository = Nothing
            _retroactiveRepository = Nothing
            _NoveltyIncentivePaymentRepository = Nothing
            _groupRepository = Nothing
            _settingsRepository = Nothing
            _contractRepository = Nothing
            _conceptRepository = Nothing
            _incentivePaymentRepository = Nothing
            _personRepository = Nothing
            _bankRepository = Nothing
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
