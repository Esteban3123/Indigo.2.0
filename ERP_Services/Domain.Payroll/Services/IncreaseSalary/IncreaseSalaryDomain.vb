'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 03-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base.Entities
Imports Domain.Common.Entities
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports System.Dynamic
Imports Infrastructure.CrossCutting.Base

Public Class IncreaseSalaryDomain
    Implements IIncreaseSalaryDomain

#Region "Repositories"
    ''' <summary>
    ''' Repositorio de Liquidación de Nómina
    ''' </summary>
    ''' <remarks></remarks>
    Private _payrollLiquidationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Repositorio de Dominio de Liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private _domainLiquidation As ILiquidationDomain

    Private _ConceptRepository As IConceptRepository

    Private _AuthorizationConceptRepository As IAuthorizationConceptRepository

    Private _incentivePaymentRepository As IIncentivePaymentRepository

    Private _tradeUnionRepository As ITradeUnionRepository

    Private _manualConcepts As IManualConcepts

    Private _payrollSettings As IPayrollSettingsRepository


#End Region

#Region "Constructor"
    Public Sub New(ByVal payrollLiquidationRepository As IPayrollLiquidationRepository, domainLiquidation As ILiquidationDomain, ConceptRepository As IConceptRepository, AuthorizationConceptRepository As IAuthorizationConceptRepository, incentivePaymentRepository As IIncentivePaymentRepository, tradeUnionRepository As ITradeUnionRepository,
                   manualConcepts As IManualConcepts, payrollSettings As IPayrollSettingsRepository)
        If payrollLiquidationRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio Liquidación Vacío")
        End If

        If domainLiquidation Is Nothing Then
            Throw New ArgumentNullException("Repositorio Dominio de Liquidación Vacío")
        End If

        _payrollLiquidationRepository = payrollLiquidationRepository
        _domainLiquidation = domainLiquidation
        _ConceptRepository = ConceptRepository
        _AuthorizationConceptRepository = AuthorizationConceptRepository
        _incentivePaymentRepository = incentivePaymentRepository
        _tradeUnionRepository = tradeUnionRepository
        _manualConcepts = manualConcepts
        _payrollSettings = payrollSettings
    End Sub
#End Region

    Public Function IncreaseSalary(ContractList As List(Of Contract), PercentageIncrease As Decimal) As Dynamic.ExpandoObject Implements IIncreaseSalaryDomain.IncreaseSalary

        Dim IncreaseSalaryObj As Object = New ExpandoObject()
        IncreaseSalaryObj.List = New List(Of Object)()

        For Each Contract As Contract In ContractList
            Dim Aux As Object = New ExpandoObject()
            Aux.ContractId = Contract.Id
            Aux.PositionName = Contract.Position.Code & " - " & Contract.Position.Name
            Aux.FunctionalUnitName = Contract.FunctionalUnit.Code & " - " & Contract.FunctionalUnit.Name
            Aux.JobbondingDate = Contract.JobBondingDate
            Aux.NitEmployee = Contract.Employee.ThirdParty.Nit
            Aux.NameEmployee = Contract.Employee.ThirdParty.Name
            Aux.BasicSalary = Contract.BasicSalary

            Dim NewSumSalary As Double = 0


            If Contract.Group.PayrollParameter.AproximationValue = 100 Then
                NewSumSalary = Utils.RoundedValuesNearestHundred(Contract.BasicSalary * (PercentageIncrease / 100))
                Aux.IncrementValue = NewSumSalary
                Aux.NewSalary = Utils.RoundedValuesNearestHundred(Contract.BasicSalary + NewSumSalary)
            ElseIf Contract.Group.PayrollParameter.AproximationValue = 1000 Then
                NewSumSalary = Utils.RoundValueNearestThousand(Contract.BasicSalary * (PercentageIncrease / 100))
                Aux.IncrementValue = NewSumSalary
                Aux.NewSalary = Utils.RoundValueNearestThousand(Contract.BasicSalary + NewSumSalary)
            Else
                NewSumSalary = Contract.BasicSalary * (PercentageIncrease / 100)
                Aux.IncrementValue = NewSumSalary
                Aux.NewSalary = Contract.BasicSalary + NewSumSalary
            End If

            CType(IncreaseSalaryObj.List, List(Of Object)).Add(Aux)
        Next

        Return IncreaseSalaryObj

    End Function

    Public Function ExecuteRetroactive(ContractList As List(Of Contract), InitialDate As Date, PercentageIncrease As Decimal, Group As Group, PayrollPaid As Byte, IndigoSessionValues As SessionValues, Optional SpecificEmployeeId As Integer = 0) As List(Of RetroactiveC) Implements IIncreaseSalaryDomain.ExecuteRetroactive

        Dim IncreaseSalaryObj As Object = New ExpandoObject()
        IncreaseSalaryObj.List = New List(Of Object)()
        Dim ListConceptDiscount As New List(Of Concept)


        Try

            Dim SettingsPayroll = _payrollSettings.GetSettingPayroll()


            Dim ListConceptAffectRetroactive = _ConceptRepository.ListAllConcept()
            ListConceptAffectRetroactive = ListConceptAffectRetroactive.Where(Function(x) x.AffectRetroactive = True).ToList()

            Dim ListConcept = _AuthorizationConceptRepository.GetAuthorizationConceptByGroupId(Group.Id)

            Dim ListRetroactiveC As New List(Of RetroactiveC)

            'Dim SearchDate As Date = DateAdd(DateInterval.Year, 1, Date.Now())
            Dim SearchDate As Date = Date.Now()

            Dim IncentiveStarDate As New Date(Year(Date.Now()) - 1, 7, 1)
            Dim IncentiveEndDate As New Date(Year(Date.Now()), 6, 30)

            ' Optimización directa en el repositorio: pasar EmployeeId para filtrar en SQL
            Dim ListLiquidationConfirm = _payrollLiquidationRepository.GetConfirmLiquidationByStarEndDateRetroactive(InitialDate, SearchDate, Group.Id, SpecificEmployeeId)

            ' Optimización directa en el repositorio: pasar EmployeeId para filtrar en SQL
            Dim ListIncentivePayment = _incentivePaymentRepository.GetIncentivePaymentByPayrollDateGroupId(Group.Id, IncentiveStarDate, IncentiveEndDate, 2, True, SpecificEmployeeId)

            If ListLiquidationConfirm IsNot Nothing Then
                For Each Contract As Contract In ContractList

                    Dim ValueRetroactive As Double = 0
                    Dim TotalRetroactive As Double = 0
                    Dim TotalAccrued As Double = 0

                    Dim BasePension As Double = 0
                    Dim BaseHealth As Double = 0
                    Dim BaseRTF As Double = 0
                    Dim BaseSindicato As Double = 0
                    Dim BaseARL As Double = 0
                    Dim BaseSena As Double = 0
                    Dim BaseICBF As Double = 0
                    Dim BaseCajaCompensacion As Double = 0
                    Dim BaseVacation As Double = 0
                    Dim BaseUnemployement As Double = 0
                    Dim BaseIncentivePayment As Double = 0

                    Dim NewInitialDate As Date = New Date(SearchDate.Year, 1, 1)
                    Dim NewEndDate As Date = New Date(SearchDate.Year, 12, 31)

                    Dim ListLiquidationConfirmEmployee = ListLiquidationConfirm.Where(Function(x) x.EmployeeId = Contract.EmployeeId).ToList()
                    Dim ListLiquidationLastYear As List(Of Liquidation)

                    If SpecificEmployeeId > 0 Then
                        ListLiquidationLastYear = _payrollLiquidationRepository.GetConfirmLiquidationByStarEndDateRetroactive(NewInitialDate, NewEndDate, Contract.GroupId, Contract.EmployeeId)
                    Else
                        ListLiquidationLastYear = _payrollLiquidationRepository.GetConfirmLiquidationByStarEndDateRetroactive(NewInitialDate, NewEndDate, Contract.GroupId)
                    End If

                    Dim ObjRetroactive As New RetroactiveC
                    ObjRetroactive.Status = 1
                    If PayrollPaid = 1 Then
                        ObjRetroactive.PaymentType = "N"
                    Else
                        ObjRetroactive.PaymentType = "P"
                    End If

                    ObjRetroactive.InitialDateRetroactive = InitialDate
                    ObjRetroactive.UsedPercentage = PercentageIncrease
                    ObjRetroactive.IdGroup = Contract.GroupId
                    ObjRetroactive.IdEmployee = Contract.EmployeeId
                    ObjRetroactive.IdContract = Contract.Id
                    ObjRetroactive.InitialContractNumber = Contract.InitialContractNumber
                    ObjRetroactive.ExecuteProcessDate = Date.Now()
                    ObjRetroactive.CreationUserId = IndigoSessionValues.UserIndigoId
                    ObjRetroactive.CreationDate = Date.Now()
                    ObjRetroactive.Nit = Contract.Employee.ThirdParty.Nit
                    ObjRetroactive.EmployeeName = Contract.Employee.ThirdParty.Name
                    ObjRetroactive.NextPayrollDate = Contract.Group.NextDateLiquidation

                    If ListLiquidationConfirmEmployee IsNot Nothing And ListLiquidationConfirmEmployee.Count > 0 Then

                        Dim TotalDiscount As Double = 0

                        For Each ObjConcept As Concept In ListConceptAffectRetroactive

                            ' Salud y pensión empleado se recalculan por fórmula sobre IBC (bloque inferior)
                            If ObjConcept.ConceptClass = "014" OrElse ObjConcept.ConceptClass = "017" Then
                                Continue For
                            End If

                            Dim ValueConcept As Double = 0
                            Dim ObjRetroactiveDetail As New RetroactiveD


                            'ValueConcept = ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptId = ObjConcept.Id).Sum()

                            For Each ObjLiquidation As Liquidation In ListLiquidationConfirmEmployee
                                If ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptId = ObjConcept.Id) IsNot Nothing And ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptId = ObjConcept.Id).Count > 0 Then
                                    ValueConcept = ValueConcept + ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptId = ObjConcept.Id).FirstOrDefault().ConceptTotalValue
                                End If
                            Next

                            'If ObjConcept.ConceptClass = "002" Then
                            '    Dim ObjPaidValueAverageIncentiveServices = _incentivePaymentRepository.GetIncentivePaymentByEmployeeIdLastLiquidation(IIf(Contract.InitialContractNumber = 0, Contract.Id, Contract.InitialContractNumber))
                            '    If ObjPaidValueAverageIncentiveServices IsNot Nothing Then
                            '        ValueConcept = ObjPaidValueAverageIncentiveServices.Item(0).PaidValue
                            '    End If
                            'End If


                            If ValueConcept > 0 Then

                                Dim NewValue = (ValueConcept + (ValueConcept * (PercentageIncrease / 100))) - ValueConcept

                                ObjRetroactiveDetail.IdConcept = ObjConcept.Id
                                ObjRetroactiveDetail.ValueConcept = ValueConcept

                                If ObjConcept.ConceptClass = "014" Or ObjConcept.ConceptClass = "017" Then
                                    NewValue = Utils.RoundedValuesNearestHundred(NewValue)
                                End If

                                ObjRetroactiveDetail.ValueConceptWithRetroactive = NewValue

                                ObjRetroactiveDetail.ConceptCode = ObjConcept.Code
                                ObjRetroactiveDetail.ConceptName = ObjConcept.Name

                                ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetail)

                                If ObjConcept.ConceptType = 1 Then
                                    TotalRetroactive = TotalRetroactive + ObjRetroactiveDetail.ValueConceptWithRetroactive

                                    If ObjConcept.AffectIBCHealth = True Then
                                        BaseHealth = BaseHealth + ObjRetroactiveDetail.ValueConceptWithRetroactive
                                    End If

                                    If ObjConcept.AffectIBCPension = True Then
                                        BasePension = BasePension + ObjRetroactiveDetail.ValueConceptWithRetroactive
                                    End If

                                    If ObjConcept.AffectIBCRTF = True Then
                                        BaseRTF = BaseRTF + ObjRetroactiveDetail.ValueConceptWithRetroactive
                                    End If

                                    If ObjConcept.AffectIBCARP = True Then
                                        BaseARL = BaseARL + ObjRetroactiveDetail.ValueConceptWithRetroactive
                                    End If

                                    If ObjConcept.AffectIBCCompensationFund = True Then
                                        BaseCajaCompensacion = BaseCajaCompensacion + ObjRetroactiveDetail.ValueConceptWithRetroactive
                                    End If

                                    If ObjConcept.AffectIBCICBF = True Then
                                        BaseICBF = BaseICBF + ObjRetroactiveDetail.ValueConceptWithRetroactive
                                    End If


                                    If ObjConcept.AffectIBCSENA = True Then
                                        BaseSena = BaseSena + ObjRetroactiveDetail.ValueConceptWithRetroactive
                                    End If

                                    If ObjConcept.AffectIBCIncentivePayment = True Then
                                        BaseIncentivePayment = BaseIncentivePayment + ObjRetroactiveDetail.ValueConceptWithRetroactive
                                    End If

                                    If ObjConcept.AffectIBCSeverance = True Then
                                        BaseUnemployement = BaseUnemployement + ObjRetroactiveDetail.ValueConceptWithRetroactive
                                    End If

                                    If ObjConcept.AffectIBC = True Then
                                        BaseVacation = BaseVacation + ObjRetroactiveDetail.ValueConceptWithRetroactive
                                    End If


                                    TotalAccrued = TotalAccrued + ObjRetroactiveDetail.ValueConceptWithRetroactive
                                ElseIf ObjConcept.ConceptType = 2 Then
                                    TotalRetroactive = TotalRetroactive - ObjRetroactiveDetail.ValueConceptWithRetroactive
                                End If

                                If ObjConcept.Code = "001" Then
                                    BaseSindicato = ObjRetroactiveDetail.ValueConceptWithRetroactive
                                End If


                                'TotalRetroactive = TotalRetroactive + ObjRetroactiveDetail.ValueConceptWithRetroactive

                            End If

                        Next

                    End If

                    Dim ListManualConceptsRetroactive = _manualConcepts.GetManualConceptsByEmployeeIdInitialDate(Contract.EmployeeId, InitialDate, 1, 2)

                    If ListManualConceptsRetroactive IsNot Nothing Then
                        For Each ObjManualConcept As ManualConcepts In ListManualConceptsRetroactive
                            Dim ObjManualConceptRetroactiveDetail As New RetroactiveD


                            ObjManualConceptRetroactiveDetail.IdConcept = ObjManualConcept.ConceptId
                            ObjManualConceptRetroactiveDetail.ValueConcept = ObjManualConcept.QuoteValue

                            ObjManualConceptRetroactiveDetail.ValueConceptWithRetroactive = ObjManualConcept.QuoteValue

                            If ObjManualConcept.Concept.ConceptType = 1 Then
                                TotalAccrued = TotalAccrued + ObjManualConcept.QuoteValue
                                TotalRetroactive = TotalRetroactive + ObjManualConcept.QuoteValue
                            ElseIf ObjManualConcept.Concept.ConceptType = 2 Then
                                TotalRetroactive = TotalRetroactive - ObjManualConcept.QuoteValue
                            End If

                            ObjManualConceptRetroactiveDetail.ConceptCode = ObjManualConcept.Concept.Code
                            ObjManualConceptRetroactiveDetail.ConceptName = ObjManualConcept.Concept.Name

                            ObjRetroactive.RetroactiveD.Add(ObjManualConceptRetroactiveDetail)


                        Next
                    End If


                    If ListIncentivePayment IsNot Nothing Then

                        Dim ObjIncentivePayment = ListIncentivePayment.Where(Function(x) x.ContractId = Contract.Id).FirstOrDefault()

                        If ObjIncentivePayment IsNot Nothing Then

                            Dim ObjRetroactiveDetail As New RetroactiveD

                            Dim ValueConcept = ObjIncentivePayment.TotalAccrued
                            Dim NewValue = (ValueConcept + (ValueConcept * (PercentageIncrease / 100))) - ValueConcept

                            Dim ObjConceptIncentivePayment = ListConcept.Where(Function(x) x.ConceptId = SettingsPayroll.ServicesIncentivePaymentConceptId).FirstOrDefault().Concept

                            ObjRetroactiveDetail.IdConcept = ObjConceptIncentivePayment.Id
                            ObjRetroactiveDetail.ValueConcept = ValueConcept

                            ObjRetroactiveDetail.ValueConceptWithRetroactive = NewValue
                            TotalAccrued = TotalAccrued + NewValue

                            ObjRetroactiveDetail.ConceptCode = ObjConceptIncentivePayment.Code
                            ObjRetroactiveDetail.ConceptName = ObjConceptIncentivePayment.Name

                            TotalRetroactive = TotalRetroactive + ObjRetroactiveDetail.ValueConceptWithRetroactive
                            BaseRTF = BaseRTF + ObjRetroactiveDetail.ValueConceptWithRetroactive
                            BaseCajaCompensacion = BaseCajaCompensacion + ObjRetroactiveDetail.ValueConceptWithRetroactive
                            BaseSena = BaseSena + ObjRetroactiveDetail.ValueConceptWithRetroactive
                            BaseICBF = BaseICBF + ObjRetroactiveDetail.ValueConceptWithRetroactive

                            ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetail)

                        End If

                    End If


                    Dim AporteSalud As Double = 0
                    Dim AportePension As Double = 0
                    Dim AporteFondoSolidaridad As Double = 0
                    Dim Retencion As Double = 0
                    Dim Sindicato As Double = 0

                    'Dim NewValueHealth = BaseHealth * (4 / 100)

                    'Dim  = BasePension * (4 / 100)

                    'If BasePension >= 4 * (Group.PayrollParameter.LegalSalaryMinimum) Then
                    '    AporteFondoSolidaridad = BasePension * (1 / 100)
                    'End If

                    Dim ListAllConcept = _ConceptRepository.ListAllConcept()


                    'Salud
                    Dim ObjConceptHealth = ListAllConcept.Where(Function(x) x.ConceptClass = "017").FirstOrDefault()
                    Dim ObjRetroactiveDetailHealth As New RetroactiveD
                    Dim NewValueHealth = ReplaceDataFormulates(ObjConceptHealth.Formulates, Contract.Group.PayrollParameter, BaseHealth, BasePension, Contract.Employee.ProfessionalRiskPercentage, BaseARL, BaseSena, BaseICBF, BaseCajaCompensacion, BaseVacation, BaseIncentivePayment, BaseUnemployement)

                    ObjRetroactiveDetailHealth.IdConcept = ObjConceptHealth.Id
                    ObjRetroactiveDetailHealth.ValueConcept = NewValueHealth

                    ObjRetroactiveDetailHealth.ValueConceptWithRetroactive = NewValueHealth

                    ObjRetroactiveDetailHealth.ConceptCode = ObjConceptHealth.Code
                    ObjRetroactiveDetailHealth.ConceptName = ObjConceptHealth.Name

                    TotalRetroactive = TotalRetroactive - NewValueHealth

                    ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetailHealth)

                    'Pension

                    Dim ObjConceptPension = ListAllConcept.Where(Function(x) x.ConceptClass = "014").FirstOrDefault()
                    Dim ObjRetroactiveDetailPension As New RetroactiveD

                    Dim NewValuePension = ReplaceDataFormulates(ObjConceptPension.Formulates, Contract.Group.PayrollParameter, BaseHealth, BasePension, Contract.Employee.ProfessionalRiskPercentage, BaseARL, BaseSena, BaseICBF, BaseCajaCompensacion, BaseVacation, BaseIncentivePayment, BaseUnemployement)

                    ObjRetroactiveDetailPension.IdConcept = ObjConceptPension.Id
                    ObjRetroactiveDetailPension.ValueConcept = NewValuePension

                    ObjRetroactiveDetailPension.ValueConceptWithRetroactive = NewValuePension

                    ObjRetroactiveDetailPension.ConceptCode = ObjConceptPension.Code
                    ObjRetroactiveDetailPension.ConceptName = ObjConceptPension.Name

                    TotalRetroactive = TotalRetroactive - NewValuePension

                    ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetailPension)

                    'Fondo de Solidaridad
                    'If AporteFondoSolidaridad > 0 
                    Dim ObjConceptSolidarityFund = ListAllConcept.Where(Function(x) x.ConceptClass = "038").FirstOrDefault()

                    Dim NewValueSolidarityFund = ReplaceDataFormulates(ObjConceptSolidarityFund.Formulates, Contract.Group.PayrollParameter, BaseHealth, BasePension, Contract.Employee.ProfessionalRiskPercentage, BaseARL, BaseSena, BaseICBF, BaseCajaCompensacion, BaseVacation, BaseIncentivePayment, BaseUnemployement)

                    If NewValueSolidarityFund > 0 Then

                        Dim ObjRetroactiveDetailPensionSolidarity As New RetroactiveD

                        ObjRetroactiveDetailPensionSolidarity.IdConcept = ObjConceptSolidarityFund.Id
                        ObjRetroactiveDetailPensionSolidarity.ValueConcept = NewValueSolidarityFund

                        ObjRetroactiveDetailPensionSolidarity.ValueConceptWithRetroactive = NewValueSolidarityFund

                        ObjRetroactiveDetailPensionSolidarity.ConceptCode = ObjConceptSolidarityFund.Code
                        ObjRetroactiveDetailPensionSolidarity.ConceptName = ObjConceptSolidarityFund.Name

                        TotalRetroactive = TotalRetroactive - NewValueSolidarityFund

                        ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetailPensionSolidarity)
                    End If

                    'Retenciones
                    Dim ListRetention = _domainLiquidation.Retention(BaseRTF, NewValuePension, 0, NewValueSolidarityFund, 0, 0, NewValueHealth, 0, 0, Contract.Employee.HousingDeductionValue, Group.PayrollParameter.UVTValue, Group.PayrollParameter.RTFExemptPercentage, Group.PayrollParameter.LegalSalaryMinimum, Contract.Employee.ProcedureTypeRTF, Contract, Contract.Employee, InitialDate, 0, SettingsPayroll, ListLiquidationLastYear)

                    For Each ObjTuple As Tuple(Of String, Double) In ListRetention

                        If ObjTuple.Item1 = "Retenciones" Then
                            Retencion = ObjTuple.Item2
                        End If

                    Next

                    If Retencion > 0 Then
                        Dim ObjRetroactiveDetaiRetroactive As New RetroactiveD

                        Dim ObjConceptRetention = ListAllConcept.Where(Function(x) x.Code = "701").FirstOrDefault()

                        ObjRetroactiveDetaiRetroactive.IdConcept = ObjConceptRetention.Id
                        ObjRetroactiveDetaiRetroactive.ValueConcept = Retencion

                        ObjRetroactiveDetaiRetroactive.ValueConceptWithRetroactive = Retencion

                        ObjRetroactiveDetaiRetroactive.ConceptCode = ObjConceptRetention.Code
                        ObjRetroactiveDetaiRetroactive.ConceptName = ObjConceptRetention.Name

                        TotalRetroactive = TotalRetroactive - Retencion

                        ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetaiRetroactive)

                    End If

                    'Sindicatos

                    If Contract.Employee.TradeUnion = 3 And Contract.Employee.TradeUnionEmployee IsNot Nothing And Contract.Employee.TradeUnionEmployee.Count > 0 Then

                        Dim ListEmployeeSindicate = _tradeUnionRepository.GetTradeUnionById(Contract.Employee.TradeUnionEmployee.Item(0).TradeUnionId, True)


                        If ListEmployeeSindicate IsNot Nothing Then

                            Dim ObjTradeUnionEmployee As New RetroactiveD

                            Dim ObjConcept = ListAllConcept.Where(Function(x) x.Id = ListEmployeeSindicate.PayrollConceptId).FirstOrDefault()

                            Sindicato = BaseSindicato * (1 / 100)

                            ObjTradeUnionEmployee.IdConcept = ObjConcept.Id
                            ObjTradeUnionEmployee.ValueConcept = Sindicato

                            ObjTradeUnionEmployee.ValueConceptWithRetroactive = Sindicato

                            ObjTradeUnionEmployee.ConceptCode = ObjConcept.Code
                            ObjTradeUnionEmployee.ConceptName = ObjConcept.Name

                            TotalRetroactive = TotalRetroactive - Sindicato

                            ObjRetroactive.RetroactiveD.Add(ObjTradeUnionEmployee)
                        End If
                    End If

                    'SALUD PATRONO
                    'Salud


                    Dim ObjConceptHealthEmployer = ListAllConcept.Where(Function(x) x.ConceptClass = "018").FirstOrDefault()

                    Dim NewValueEmployerHealth = ReplaceDataFormulates(ObjConceptHealthEmployer.Formulates, Contract.Group.PayrollParameter, BaseHealth, BasePension, Contract.Employee.ProfessionalRiskPercentage, BaseARL, BaseSena, BaseICBF, BaseCajaCompensacion, BaseVacation, BaseIncentivePayment, BaseUnemployement)

                    If NewValueEmployerHealth > 0 Then

                        Dim ObjRetroactiveDetailHealthEmployer As New RetroactiveD
                        ObjRetroactiveDetailHealthEmployer.IdConcept = ObjConceptHealthEmployer.Id
                        ObjRetroactiveDetailHealthEmployer.ValueConcept = NewValueEmployerHealth

                        ObjRetroactiveDetailHealthEmployer.ValueConceptWithRetroactive = NewValueEmployerHealth

                        ObjRetroactiveDetailHealthEmployer.ConceptCode = ObjConceptHealthEmployer.Code
                        ObjRetroactiveDetailHealthEmployer.ConceptName = ObjConceptHealthEmployer.Name

                        ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetailHealthEmployer)
                    End If

                    'PENSIÓN PATRONO


                    Dim ObjConceptPensionEmployer = ListAllConcept.Where(Function(x) x.Code = "015").FirstOrDefault()

                    Dim NewValueEmployerPension = ReplaceDataFormulates(ObjConceptPensionEmployer.Formulates, Contract.Group.PayrollParameter, BaseHealth, BasePension, Contract.Employee.ProfessionalRiskPercentage, BaseARL, BaseSena, BaseICBF, BaseCajaCompensacion, BaseVacation, BaseIncentivePayment, BaseUnemployement)

                    If NewValueEmployerPension > 0 Then

                        Dim ObjRetroactiveDetailPensionEmployer As New RetroactiveD

                        ObjRetroactiveDetailPensionEmployer.IdConcept = ObjConceptPensionEmployer.Id
                        ObjRetroactiveDetailPensionEmployer.ValueConcept = NewValueEmployerPension

                        ObjRetroactiveDetailPensionEmployer.ValueConceptWithRetroactive = NewValueEmployerPension

                        ObjRetroactiveDetailPensionEmployer.ConceptCode = ObjConceptPensionEmployer.Code
                        ObjRetroactiveDetailPensionEmployer.ConceptName = ObjConceptPensionEmployer.Name

                        ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetailPensionEmployer)
                    End If

                    'ARL
                    If BaseARL > 0 Then

                        Dim ObjConceptARLEmployee = ListAllConcept.Where(Function(x) x.ConceptClass = "009").FirstOrDefault()

                        If ObjConceptARLEmployee IsNot Nothing Then

                            Dim NewValueARLEmployee = ReplaceDataFormulates(ObjConceptARLEmployee.Formulates, Contract.Group.PayrollParameter, BaseHealth, BasePension, Contract.Employee.ProfessionalRiskPercentage, BaseARL, BaseSena, BaseICBF, BaseCajaCompensacion, BaseVacation, BaseIncentivePayment, BaseUnemployement)

                            Dim ObjRetroactiveDetailARLEmployer As New RetroactiveD

                            ObjRetroactiveDetailARLEmployer.IdConcept = ObjConceptARLEmployee.Id
                            ObjRetroactiveDetailARLEmployer.ValueConcept = NewValueARLEmployee

                            ObjRetroactiveDetailARLEmployer.ValueConceptWithRetroactive = NewValueARLEmployee

                            ObjRetroactiveDetailARLEmployer.ConceptCode = ObjConceptARLEmployee.Code
                            ObjRetroactiveDetailARLEmployer.ConceptName = ObjConceptARLEmployee.Name

                            ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetailARLEmployer)
                        End If
                    End If

                    'SENA
                    If BaseSena > 0 Then
                        Dim ObjRetroactiveDetailSENAEmployer As New RetroactiveD

                        Dim ObjConceptSENAEmployee = ListAllConcept.Where(Function(x) x.ConceptClass = "035").FirstOrDefault()

                        If ObjConceptSENAEmployee IsNot Nothing Then
                            Dim NewValueSENAEmployee = ReplaceDataFormulates(ObjConceptSENAEmployee.Formulates, Contract.Group.PayrollParameter, BaseHealth, BasePension, Contract.Employee.ProfessionalRiskPercentage, BaseARL, BaseSena, BaseICBF, BaseCajaCompensacion, BaseVacation, BaseIncentivePayment, BaseUnemployement)

                            ObjRetroactiveDetailSENAEmployer.IdConcept = ObjConceptSENAEmployee.Id
                            ObjRetroactiveDetailSENAEmployer.ValueConcept = NewValueSENAEmployee

                            ObjRetroactiveDetailSENAEmployer.ValueConceptWithRetroactive = NewValueSENAEmployee

                            ObjRetroactiveDetailSENAEmployer.ConceptCode = ObjConceptSENAEmployee.Code
                            ObjRetroactiveDetailSENAEmployer.ConceptName = ObjConceptSENAEmployee.Name

                            ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetailSENAEmployer)
                        End If
                    End If

                    'ICBF
                    If BaseICBF > 0 Then
                        Dim ObjRetroactiveDetailCajaCompensacionEmployer As New RetroactiveD

                        Dim ObjConceptICBFEmployee = ListAllConcept.Where(Function(x) x.ConceptClass = "037").FirstOrDefault()

                        If ObjConceptICBFEmployee IsNot Nothing Then

                            Dim NewValueICBFEmployee = ReplaceDataFormulates(ObjConceptICBFEmployee.Formulates, Contract.Group.PayrollParameter, BaseHealth, BasePension, Contract.Employee.ProfessionalRiskPercentage, BaseARL, BaseSena, BaseICBF, BaseCajaCompensacion, BaseVacation, BaseIncentivePayment, BaseUnemployement)

                            ObjRetroactiveDetailCajaCompensacionEmployer.IdConcept = ObjConceptICBFEmployee.Id
                            ObjRetroactiveDetailCajaCompensacionEmployer.ValueConcept = NewValueICBFEmployee

                            ObjRetroactiveDetailCajaCompensacionEmployer.ValueConceptWithRetroactive = NewValueICBFEmployee

                            ObjRetroactiveDetailCajaCompensacionEmployer.ConceptCode = ObjConceptICBFEmployee.Code
                            ObjRetroactiveDetailCajaCompensacionEmployer.ConceptName = ObjConceptICBFEmployee.Name

                            ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetailCajaCompensacionEmployer)
                        End If
                    End If

                    'CAJA DE COMPENSACION
                    If BaseCajaCompensacion > 0 Then
                        Dim ObjRetroactiveDetailCajaCompensacionEmployer As New RetroactiveD

                        Dim ObjConceptCajaEmployee = ListAllConcept.Where(Function(x) x.ConceptClass = "036").FirstOrDefault()

                        If ObjConceptCajaEmployee IsNot Nothing Then
                            Dim NewValueCajaEmployee = ReplaceDataFormulates(ObjConceptCajaEmployee.Formulates, Contract.Group.PayrollParameter, BaseHealth, BasePension, Contract.Employee.ProfessionalRiskPercentage, BaseARL, BaseSena, BaseICBF, BaseCajaCompensacion, BaseVacation, BaseIncentivePayment, BaseUnemployement)


                            ObjRetroactiveDetailCajaCompensacionEmployer.IdConcept = ObjConceptCajaEmployee.Id
                            ObjRetroactiveDetailCajaCompensacionEmployer.ValueConcept = NewValueCajaEmployee

                            ObjRetroactiveDetailCajaCompensacionEmployer.ValueConceptWithRetroactive = NewValueCajaEmployee

                            ObjRetroactiveDetailCajaCompensacionEmployer.ConceptCode = ObjConceptCajaEmployee.Code
                            ObjRetroactiveDetailCajaCompensacionEmployer.ConceptName = ObjConceptCajaEmployee.Name

                            ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetailCajaCompensacionEmployer)
                        End If
                    End If

                    'Vacaciones
                    If BaseVacation > 0 Then

                        Dim ObjConceptVacation = ListAllConcept.Where(Function(x) x.ConceptClass = "031").FirstOrDefault()

                        If ObjConceptVacation IsNot Nothing Then
                            Dim ValueVacation As Decimal = 0

                            For Each ObjLiquidation As Liquidation In ListLiquidationConfirmEmployee
                                If ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptId = ObjConceptVacation.Id) IsNot Nothing And ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptId = ObjConceptVacation.Id).Count > 0 Then
                                    ValueVacation = ValueVacation + ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptId = ObjConceptVacation.Id).FirstOrDefault().ConceptTotalValue
                                End If
                            Next

                            Dim NewValueVacation = (ValueVacation * (PercentageIncrease / 100))

                            Dim ObjRetroactiveDetailVacation As New RetroactiveD

                            ObjRetroactiveDetailVacation.IdConcept = ObjConceptVacation.Id
                            ObjRetroactiveDetailVacation.ValueConcept = NewValueVacation

                            ObjRetroactiveDetailVacation.ValueConceptWithRetroactive = NewValueVacation

                            ObjRetroactiveDetailVacation.ConceptCode = ObjConceptVacation.Code
                            ObjRetroactiveDetailVacation.ConceptName = ObjConceptVacation.Name

                            ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetailVacation)
                        End If

                    End If

                    'Primas
                    If BaseIncentivePayment > 0 Then

                        Dim ObjConceptIncentive = ListAllConcept.Where(Function(x) x.ConceptClass = "033").FirstOrDefault()

                        If ObjConceptIncentive IsNot Nothing Then

                            Dim ValueIncentivePayment As Decimal = 0

                            For Each ObjLiquidation As Liquidation In ListLiquidationConfirmEmployee
                                If ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptId = ObjConceptIncentive.Id) IsNot Nothing And ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptId = ObjConceptIncentive.Id).Count > 0 Then
                                    ValueIncentivePayment = ValueIncentivePayment + ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptId = ObjConceptIncentive.Id).FirstOrDefault().ConceptTotalValue
                                End If
                            Next

                            Dim NewValueIncentive = (ValueIncentivePayment * (PercentageIncrease / 100))

                            Dim ObjRetroactiveDetailIncentivePayment As New RetroactiveD

                            ObjRetroactiveDetailIncentivePayment.IdConcept = ObjConceptIncentive.Id
                            ObjRetroactiveDetailIncentivePayment.ValueConcept = NewValueIncentive

                            ObjRetroactiveDetailIncentivePayment.ValueConceptWithRetroactive = NewValueIncentive

                            ObjRetroactiveDetailIncentivePayment.ConceptCode = ObjConceptIncentive.Code
                            ObjRetroactiveDetailIncentivePayment.ConceptName = ObjConceptIncentive.Name

                            ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetailIncentivePayment)
                        End If
                    End If

                    'Cesantias
                    If BaseUnemployement > 0 Then

                        Dim ObjConceptUnemployment = ListAllConcept.Where(Function(x) x.ConceptClass = "008").FirstOrDefault()

                        If ObjConceptUnemployment IsNot Nothing Then

                            Dim NewValueUnemployment = ReplaceDataFormulates(ObjConceptUnemployment.Formulates, Contract.Group.PayrollParameter, BaseHealth, BasePension, Contract.Employee.ProfessionalRiskPercentage, BaseARL, BaseSena, BaseICBF, BaseCajaCompensacion, BaseVacation, BaseIncentivePayment, BaseUnemployement)

                            Dim ObjRetroactiveDetailUnemployement As New RetroactiveD

                            ObjRetroactiveDetailUnemployement.IdConcept = ObjConceptUnemployment.Id
                            ObjRetroactiveDetailUnemployement.ValueConcept = NewValueUnemployment

                            ObjRetroactiveDetailUnemployement.ValueConceptWithRetroactive = NewValueUnemployment

                            ObjRetroactiveDetailUnemployement.ConceptCode = ObjConceptUnemployment.Code
                            ObjRetroactiveDetailUnemployement.ConceptName = ObjConceptUnemployment.Name

                            ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetailUnemployement)
                        End If

                        'Interes

                        Dim ObjConceptInterestUnemployment = ListAllConcept.Where(Function(x) x.ConceptClass = "034").FirstOrDefault()

                        If ObjConceptInterestUnemployment IsNot Nothing Then

                            Dim ValueInteresesConcept As Decimal = 0

                            For Each ObjLiquidation As Liquidation In ListLiquidationConfirmEmployee

                                If ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptId = ObjConceptInterestUnemployment.Id) IsNot Nothing And ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptId = ObjConceptInterestUnemployment.Id).Count > 0 Then
                                    ValueInteresesConcept = ValueInteresesConcept + ObjLiquidation.LiquidationDetail.Where(Function(x) x.ConceptId = ObjConceptInterestUnemployment.Id).FirstOrDefault.ConceptTotalValue
                                End If
                            Next

                            Dim NewValueInterestUnemployment = (ValueInteresesConcept * (PercentageIncrease / 100))

                            Dim ObjRetroactiveDetailUnemployementIncentive As New RetroactiveD

                            ObjRetroactiveDetailUnemployementIncentive.IdConcept = ObjConceptInterestUnemployment.Id
                            ObjRetroactiveDetailUnemployementIncentive.ValueConcept = NewValueInterestUnemployment

                            ObjRetroactiveDetailUnemployementIncentive.ValueConceptWithRetroactive = NewValueInterestUnemployment

                            ObjRetroactiveDetailUnemployementIncentive.ConceptCode = ObjConceptInterestUnemployment.Code
                            ObjRetroactiveDetailUnemployementIncentive.ConceptName = ObjConceptInterestUnemployment.Name

                            ObjRetroactive.RetroactiveD.Add(ObjRetroactiveDetailUnemployementIncentive)
                        End If

                    End If

                    ObjRetroactive.TotalRetroactiveValue = TotalRetroactive
                    ListRetroactiveC.Add(ObjRetroactive)
                Next
            End If

            Return ListRetroactiveC

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function ReplaceDataFormulates(FormulaConcept As String, PayrollParameter As PayrollParameter, IBCHealth As Double, IBCPension As Double, TarifaArp As Decimal, IBCArp As Double,
                                          IBCSena As Double, IBCICBF As Double, IBCCaja As Double, IBCVacation As Double, IBCIncentive As Double, IBCUnemployement As Double) As Double

        FormulaConcept = Replace(FormulaConcept, "[IBC Salud]", Format(IBCHealth, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Salud Empleado]", Replace(PayrollParameter.EmployeeHealthContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Pensión]", Format(IBCPension, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Pensión Empleado]", Replace(PayrollParameter.EmployeePensionContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Salario Mínimo]", Format(PayrollParameter.LegalSalaryMinimum, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Salud Patrono]", Format(IBCHealth, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Salud Patrono]", Replace(PayrollParameter.EmployerHealthContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Pensión Patrono]", Replace(PayrollParameter.EmployerPensionContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Tarifa ARP]", Replace(TarifaArp.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC ARP]", Format(IBCArp, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% SENA]", Replace(PayrollParameter.SenaContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Aporte SENA]", Format(IBCSena, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Aporte Parafiscal ICBF]", Format(IBCICBF, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% ICBF]", Replace(PayrollParameter.ICBFContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[Base Aporte Parafiscal Caja de Compensación]", Format(IBCCaja, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[% Caja]", Replace(PayrollParameter.CompensationFundContributionPercentage.ToString, ",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Periodo]", Format(IBCVacation, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Primas]", Format(IBCIncentive, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Cesantias]", Format(IBCUnemployement, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC Caja]", Format(IBCCaja, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC ICBF]", Format(IBCICBF, "0.00").Replace(",", "."))
        FormulaConcept = Replace(FormulaConcept, "[IBC SENA]", Format(IBCSena, "0.00").Replace(",", "."))

        Dim result = Utils.EvalExpression(FormulaConcept)
        Dim ConceptValue As Decimal = 0
        If result.StateResult = True Then
            ConceptValue = CType(result.ObjectEmbbeded, Decimal)
        Else
            ConceptValue = 0
        End If


        Return ConceptValue

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _domainLiquidation.Dispose()
            End If
            _payrollLiquidationRepository = Nothing
            _domainLiquidation = Nothing
            _ConceptRepository = Nothing
            _AuthorizationConceptRepository = Nothing
            _incentivePaymentRepository = Nothing
            _tradeUnionRepository = Nothing
            _manualConcepts = Nothing
            _payrollSettings = Nothing
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
