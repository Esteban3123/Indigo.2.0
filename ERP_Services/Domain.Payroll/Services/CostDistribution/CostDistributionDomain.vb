'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 25-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Class CostDistributionDomain

    Implements ICostDistributionDomain

#Region "Repositories"

    Private _IConceptAccountingStructureRepository As IConceptAccountingStructureRepository

    Private _IAgreementsCRepository As IAgreementsCRepository

    Private _FundsRepository As IFundsLevelRepository

    Private _thirdPartyRepository As IThirdPartyRepository

    Private _CostDistributionRepository As ICostDistributionsRepository

    Private _GroupRepository As IGroupRepository

    Private _costCenterRepository As ICostCenterRepository

    Private _conceptRepository As IConceptRepository

    Private _tradeUnionRepository As ITradeUnionRepository

    Private _payrollSettingsRepository As IPayrollSettingsRepository

    Private _customerRepository As ICustomerRepository

    Private _companyRepository As ICompanyRepository

    Private _employeeRepository As IEmployeeRepository

    Private _agreementsClass As IKindsAgreementsRepository

    Private _entityBankAccount As IEntityBankAccountRepository

    Private _cashRegisterRepository As ICashRegisterRepository

    Private _expenseConceptRepository As IExpenseConceptRepository

#End Region

#Region "Constructor"

    Public Sub New(ByVal ConceptAccountingStructureRepository As IConceptAccountingStructureRepository, ByVal AgreementsCRepository As IAgreementsCRepository, FundsRepository As IFundsLevelRepository, thirdPartyRepository As IThirdPartyRepository, CostDistributionRepository As ICostDistributionsRepository, GroupRepository As IGroupRepository, costCenterRepository As ICostCenterRepository, conceptRepository As IConceptRepository,
                   tradeUnionRepository As ITradeUnionRepository, payrollSettingsRepository As IPayrollSettingsRepository, customerRepository As ICustomerRepository, companyRepository As ICompanyRepository, employeeRepository As IEmployeeRepository,
                   agreementsClass As IKindsAgreementsRepository, entityBankAccount As IEntityBankAccountRepository, cashRegisterRepository As ICashRegisterRepository, expenseConceptRepository As IExpenseConceptRepository)
        If ConceptAccountingStructureRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio ConceptAccountingStructureRepository Vacío")
        End If

        If AgreementsCRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio AgreementsCRepository Vacío")
        End If

        If FundsRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio FundsRepository Vacío")
        End If

        If thirdPartyRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio thirdPartyRepository Vacío")
        End If

        If CostDistributionRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio CostDistributionRepository Vacío")
        End If

        If payrollSettingsRepository Is Nothing Then
            Throw New ArgumentNullException("Repositorio payrollSettingsRepository Vacío")
        End If

        _IConceptAccountingStructureRepository = ConceptAccountingStructureRepository
        _IAgreementsCRepository = AgreementsCRepository
        _FundsRepository = FundsRepository
        _thirdPartyRepository = thirdPartyRepository
        _CostDistributionRepository = CostDistributionRepository
        _GroupRepository = GroupRepository
        _costCenterRepository = costCenterRepository
        _conceptRepository = conceptRepository
        _tradeUnionRepository = tradeUnionRepository
        _payrollSettingsRepository = payrollSettingsRepository
        _customerRepository = customerRepository
        _companyRepository = companyRepository
        _employeeRepository = employeeRepository
        _agreementsClass = agreementsClass
        _entityBankAccount = entityBankAccount
        _cashRegisterRepository = cashRegisterRepository
        _expenseConceptRepository = expenseConceptRepository
    End Sub

#End Region

    ''' <summary>
    ''' Se crea el objeto de Distribución de Costo
    ''' </summary>
    ''' <param name="PayrollLiquidation">Liquidation</param>
    ''' <returns>List(Of CostDistributions)</returns>
    ''' <remarks></remarks>
    Public Function GenerateCostDistribution(PayrollLiquidation As Liquidation, ListCostDistributionCostCenter As List(Of CostDistributionCostCenter), IndigoPayrollIntegration As Integer, SessionValues As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of List(Of Entities.CostDistributions)) Implements ICostDistributionDomain.GenerateCostDistribution
        ' Dim CostDistributionConcept As CostDistributions
        Dim ActionMessageResult As New ActionResult(Of List(Of CostDistributions)) With { .MessageResult = New List(Of String)}
        ActionMessageResult.StateResult = True

        Try

            Dim PayrollIntegration = SessionValues.IndigoPayrollIntegration

            Dim PayrollSettings = _payrollSettingsRepository.GetSettingPayroll()

            If PayrollSettings Is Nothing Then
                ActionMessageResult.MessageResult.Add("No se encontraron Parámetros de Nómina Definidos")
                ActionMessageResult.StateResult = False
                Return ActionMessageResult
            End If

            Dim groupId As Integer = PayrollLiquidation.GroupId
            Dim ObjGroup = _GroupRepository.GetGroupById(groupId)

            If ObjGroup Is Nothing Then
                ActionMessageResult.MessageResult.Add("El objeto Grupo está vacío")
                ActionMessageResult.StateResult = False
                Return ActionMessageResult
            End If

            If PayrollLiquidation Is Nothing Then
                ActionMessageResult.MessageResult.Add("El objeto Liquidaciones está vacío")
                ActionMessageResult.StateResult = False
                Return ActionMessageResult
            End If

            Dim ListCostDistribution As New List(Of CostDistributions)
            Dim TotalHoursConcept As Integer = 0
            Dim ValCon As Double

            Dim ResultObjCostDistribution As New ActionResult(Of List(Of CostDistributions))

            Dim LiquidationDetail = PayrollLiquidation.LiquidationDetail
            Dim ListProvisionConceptClass As New List(Of String)
            Dim ListParafiscalConceptClass As New List(Of String)
            Dim ListInabilities As New List(Of String)
            Dim InterfaceName As String = ObjGroup.PayrollParameter.InterfaceName
            'Dim InterfaceName As String = "DGEmpres99"
            Dim ContractEmployee As Domain.Payroll.Entities.Contract = PayrollLiquidation.Contract
            Dim AccountingContractData = _CostDistributionRepository.GetContractAccountingData(ContractEmployee.Id)

            Dim ListMessageError As New List(Of String)

            'Parafiscal y 'Provision
            ListProvisionConceptClass.Add("008") ' Provisión Cesantías
            ListProvisionConceptClass.Add("031") ' Provision Vacaciones
            ListProvisionConceptClass.Add("033") ' Provisión Primas
            ListProvisionConceptClass.Add("034") ' Provisión Intereses

            ListParafiscalConceptClass.Add("035") ' Parafiscal Sena
            ListParafiscalConceptClass.Add("036") ' Parafiscal Caja
            ListParafiscalConceptClass.Add("037") ' Parafiscal ICBF
            ListParafiscalConceptClass.Add("018") ' Salud Patrono
            ListParafiscalConceptClass.Add("015") ' Pensión Patrono
            ListParafiscalConceptClass.Add("009") ' ARL

            ListInabilities.Add("021") ' Incapacidad Ambulatoria
            ListInabilities.Add("022") ' Incapacidad Hospitalaria
            ListInabilities.Add("023") ' Maternidad
            ListInabilities.Add("027") ' Incapacidad Riesgos Profesionales

            Dim PayrollJournalVoucherTypeId As Integer?
            Dim PrestacionJournalVoucherTypeId As Integer?
            Dim ProvisionJournalVoucherTypeId As Integer?

            Dim PayrollJournalVoucherType = _CostDistributionRepository.GetJournalVoucherTypes(ObjGroup.PayrollParameter.PayrollVoucherCode)
            If PayrollJournalVoucherType IsNot Nothing Then
                PayrollJournalVoucherTypeId = PayrollJournalVoucherType.Id
            Else
                ActionMessageResult.MessageResult.Add("No está paremetrizado el Comprobante Contable de Nómina")
                ActionMessageResult.StateResult = False
                Return ActionMessageResult
            End If

            Dim PrestacionJournalVoucherType = _CostDistributionRepository.GetJournalVoucherTypes(ObjGroup.PayrollParameter.PrestacionVoucherCode)
            If PrestacionJournalVoucherType IsNot Nothing Then
                PrestacionJournalVoucherTypeId = PrestacionJournalVoucherType.Id
            Else
                ActionMessageResult.MessageResult.Add("No está paremetrizado el Comprobante Contable de Prestaciones Sociales")
                ActionMessageResult.StateResult = False
                Return ActionMessageResult
            End If

            Dim ProvisionJournalVoucherType = _CostDistributionRepository.GetJournalVoucherTypes(ObjGroup.PayrollParameter.ProvisionVoucherCode)
            If ProvisionJournalVoucherType IsNot Nothing Then
                ProvisionJournalVoucherTypeId = ProvisionJournalVoucherType.Id
            Else
                ActionMessageResult.MessageResult.Add("No está parametrizado el Comprobante Contable de Provisiones")
                ActionMessageResult.StateResult = False
                Return ActionMessageResult
            End If

            If ObjGroup.PayrollParameter.PayrollAccount = Nothing Or ObjGroup.PayrollParameter.PayrollAccount.ToString() = String.Empty Then
                ListMessageError.Add("No está parametrizado la cuenta de Nómina en Grupos")
                ActionMessageResult.StateResult = False
                Return ActionMessageResult
            End If

            Dim ListConceptAccount = New List(Of ConceptAccountingStructure)

            If PayrollIntegration = 2 Then
                ListConceptAccount = _IConceptAccountingStructureRepository.GetAccountingStructureByInterfaceName(InterfaceName, ContractEmployee.FunctionalUnitId)
            Else
                ListConceptAccount = _IConceptAccountingStructureRepository.GetAccountingStructureIntegrated(ContractEmployee.FunctionalUnitId)
            End If

            Dim ListLiquidationDetailCostDistribution = LiquidationDetail.Where(Function(x) x.DistribuirGasto = True).ToList()
            Dim ListLiquidationDetailProvision = LiquidationDetail.Where(Function(x) ListProvisionConceptClass.Contains(x.ConceptClass)).ToList()
            Dim ListLiquidationDetailParafiscal = LiquidationDetail.Where(Function(x) ListParafiscalConceptClass.Contains(x.ConceptClass)).ToList()
            Dim ListLiquidationDetailInabilities = LiquidationDetail.Where(Function(x) ListInabilities.Contains(x.ConceptClass)).ToList()
            Dim ListNormalConcept = LiquidationDetail

            'Recorremos Primero los Conceptos que Distribuyen Gastos
            If ListLiquidationDetailCostDistribution IsNot Nothing And ListLiquidationDetailCostDistribution.Count() > 0 Then
                For Each ObjListLiquidationDetailCostDistribution As LiquidationDetail In ListLiquidationDetailCostDistribution
                    If ListCostDistributionCostCenter.Count() > 0 Then
                        Dim TotalConcept As Integer = 0

                        Dim CostDistributionCostCenter = ListCostDistributionCostCenter.Where(Function(x) x.ConceptId = ObjListLiquidationDetailCostDistribution.ConceptId).ToList()

                        TotalConcept = CostDistributionCostCenter.Sum(Function(x) x.TotalHours)
                        For Each VarCostDistributionCostCenter As CostDistributionCostCenter In CostDistributionCostCenter
                            Dim CostDistributionConcept As New CostDistributions()

                            If IndigoPayrollIntegration = 2 Then
                                CostDistributionConcept.CostCenter = _costCenterRepository.GetCostCenterById(PayrollLiquidation.Employee.CostCenterId, False)
                                CostDistributionConcept.Group = ObjGroup
                                CostDistributionConcept.Concept = _conceptRepository.GetConcept(ObjListLiquidationDetailCostDistribution.ConceptCode)
                            End If

                            CostDistributionConcept.CostCenterId = VarCostDistributionCostCenter.CostCenterId
                            CostDistributionConcept.GroupId = PayrollLiquidation.GroupId
                            CostDistributionConcept.ConceptId = ObjListLiquidationDetailCostDistribution.ConceptId
                            CostDistributionConcept.GroupId = PayrollLiquidation.GroupId
                            CostDistributionConcept.ContractId = PayrollLiquidation.ContractId
                            CostDistributionConcept.PayrollDateLiquidated = PayrollLiquidation.PayrollDateLiquidated
                            CostDistributionConcept.AccountingStatementNumber = ObjGroup.PayrollParameter.PayrollVoucherCode
                            CostDistributionConcept.JournalVoucherTypeId = PayrollJournalVoucherTypeId

                            ValCon = (ObjListLiquidationDetailCostDistribution.ConceptTotalValue / TotalConcept) * VarCostDistributionCostCenter.TotalHours

                            If ObjListLiquidationDetailCostDistribution.ConceptType = "1" Then
                                'Devengado
                                CostDistributionConcept.AccruedAccount = VarCostDistributionCostCenter.AccruedAccount
                                CostDistributionConcept.AccruedValue = ValCon 'Valor Débito
                                CostDistributionConcept.DeductedValue = 0 ' Valor Crédito
                                CostDistributionConcept.DeductedAccount = VarCostDistributionCostCenter.DeductedAccount
                            ElseIf ObjListLiquidationDetailCostDistribution.ConceptType = "2" Then
                                CostDistributionConcept.DeductedAccount = VarCostDistributionCostCenter.DeductedAccount
                                CostDistributionConcept.AccruedValue = 0 'Valor Débito
                                CostDistributionConcept.DeductedValue = ValCon ' Valor Crédito
                            End If

                            CostDistributionConcept.SubCostCenterId = Nothing
                            CostDistributionConcept.NumberHours = VarCostDistributionCostCenter.TotalHours

                            CostDistributionConcept.EmployeeId = PayrollLiquidation.EmployeeId
                            CostDistributionConcept.CompanyNit = "0"
                            CostDistributionConcept.EmployeeNit = PayrollLiquidation.NitEmployee

                            CostDistributionConcept.TotalConceptValue = ValCon
                            CostDistributionConcept.Status = False

                            ListCostDistribution.Add(CostDistributionConcept)

                        Next

                    End If

                    ListNormalConcept.Remove(ObjListLiquidationDetailCostDistribution)
                Next
            End If

            'Lista de Provisiones
            If ListLiquidationDetailProvision IsNot Nothing AndAlso ListLiquidationDetailProvision.Count() > 0 Then
                For Each ObjListLiquidationDetailProvision As LiquidationDetail In ListLiquidationDetailProvision

                    If ListConceptAccount.Any(Function(x) x.ConceptId = ObjListLiquidationDetailProvision.ConceptId) = False Then
                        ActionMessageResult.MessageResult.Add("El Concepto " + ObjListLiquidationDetailProvision.ConceptCode + " no se encuentra Contablemente Parametrizado")
                        ActionMessageResult.StateResult = False
                        Continue For
                    End If

                    For i As Integer = 0 To 1
                        Dim CostDistributionConcept As New CostDistributions()

                        If IndigoPayrollIntegration = 2 Then
                            CostDistributionConcept.CostCenter = _costCenterRepository.GetCostCenterById(PayrollLiquidation.Contract.FunctionalUnit.CostCenterId, False)
                            CostDistributionConcept.Group = ObjGroup
                            CostDistributionConcept.Concept = _conceptRepository.GetConcept(ObjListLiquidationDetailProvision.ConceptCode)
                        End If

                        CostDistributionConcept.CostCenterId = PayrollLiquidation.Contract.FunctionalUnit.CostCenterId
                        CostDistributionConcept.ConceptId = ObjListLiquidationDetailProvision.ConceptId
                        CostDistributionConcept.GroupId = PayrollLiquidation.GroupId
                        CostDistributionConcept.ContractId = PayrollLiquidation.ContractId
                        CostDistributionConcept.CompanyId = ObjGroup.CompanyId
                        CostDistributionConcept.PayrollDateLiquidated = PayrollLiquidation.PayrollDateLiquidated

                        CostDistributionConcept.AccountingStatementNumber = ObjGroup.PayrollParameter.ProvisionVoucherCode
                        CostDistributionConcept.JournalVoucherTypeId = ProvisionJournalVoucherTypeId

                        If i = 0 Then
                            If ListConceptAccount.Where(Function(x) x.ConceptId = ObjListLiquidationDetailProvision.ConceptId).FirstOrDefault().AccruedAccount IsNot Nothing Then
                                CostDistributionConcept.AccruedAccount = ListConceptAccount.Where(Function(x) x.ConceptId = ObjListLiquidationDetailProvision.ConceptId).FirstOrDefault().AccruedAccount
                                CostDistributionConcept.AccruedValue = ObjListLiquidationDetailProvision.ConceptTotalValue 'Valor Débito
                                CostDistributionConcept.DeductedAccount = Nothing
                                CostDistributionConcept.DeductedValue = 0
                            Else
                                ActionMessageResult.MessageResult.Add("El Concepto " + ObjListLiquidationDetailProvision.ConceptCode + " no tiene Cuenta Débito asignada")
                                ActionMessageResult.StateResult = False
                            End If
                        Else
                            If ListConceptAccount.Where(Function(x) x.ConceptId = ObjListLiquidationDetailProvision.ConceptId).FirstOrDefault().DeductedAccount IsNot Nothing Then
                                CostDistributionConcept.DeductedAccount = ListConceptAccount.Where(Function(x) x.ConceptId = ObjListLiquidationDetailProvision.ConceptId).FirstOrDefault().DeductedAccount
                                CostDistributionConcept.DeductedValue = ObjListLiquidationDetailProvision.ConceptTotalValue ' Valor Crédito
                                CostDistributionConcept.AccruedAccount = Nothing
                                CostDistributionConcept.AccruedValue = 0
                            Else
                                ActionMessageResult.MessageResult.Add("El Concepto " + ObjListLiquidationDetailProvision.ConceptCode + " no tiene Cuenta Crédito asignada")
                                ActionMessageResult.StateResult = False
                            End If
                        End If

                        CostDistributionConcept.CompanyNit = "0"
                        CostDistributionConcept.EmployeeNit = PayrollLiquidation.NitEmployee
                        CostDistributionConcept.SubCostCenterId = Nothing
                        CostDistributionConcept.EmployeeId = PayrollLiquidation.EmployeeId
                        CostDistributionConcept.NumberHours = 0
                        CostDistributionConcept.TotalConceptValue = ObjListLiquidationDetailProvision.ConceptTotalValue
                        CostDistributionConcept.Status = False

                        ListCostDistribution.Add(CostDistributionConcept)
                    Next

                    ListNormalConcept.Remove(ObjListLiquidationDetailProvision)
                Next
            End If

            'Lista de Parafiscales
            If ListLiquidationDetailParafiscal IsNot Nothing And ListLiquidationDetailParafiscal.Count() > 0 Then

                For Each ObjListLiquidationDetailParafiscal As LiquidationDetail In ListLiquidationDetailParafiscal

                    If ListConceptAccount.Any(Function(x) x.ConceptId = ObjListLiquidationDetailParafiscal.ConceptId) = False Then
                        ActionMessageResult.MessageResult.Add("El Concepto " + ObjListLiquidationDetailParafiscal.ConceptCode + " no se encuentra Contablemente Parametrizado")
                        ActionMessageResult.StateResult = False
                        Continue For
                    End If

                    For i As Integer = 0 To 1
                        Dim CostDistributionConcept As New CostDistributions()

                        If IndigoPayrollIntegration = 2 Then
                            CostDistributionConcept.CostCenter = _costCenterRepository.GetCostCenterById(PayrollLiquidation.Employee.CostCenterId, False)
                            CostDistributionConcept.Group = ObjGroup
                            CostDistributionConcept.Concept = _conceptRepository.GetConcept(ObjListLiquidationDetailParafiscal.ConceptCode)
                        End If

                        CostDistributionConcept.GroupId = PayrollLiquidation.GroupId
                        CostDistributionConcept.ContractId = PayrollLiquidation.ContractId
                        'CostDistributionConcept.CompanyId = PayrollLiquidation.Group.CompanyId
                        CostDistributionConcept.PayrollDateLiquidated = PayrollLiquidation.PayrollDateLiquidated

                        CostDistributionConcept.AccountingStatementNumber = ObjGroup.PayrollParameter.PrestacionVoucherCode
                        CostDistributionConcept.JournalVoucherTypeId = PrestacionJournalVoucherTypeId

                        If i = 0 Then
                            If ListConceptAccount.Where(Function(x) x.ConceptId = ObjListLiquidationDetailParafiscal.ConceptId).FirstOrDefault().AccruedAccount IsNot Nothing Then
                                CostDistributionConcept.AccruedAccount = ListConceptAccount.Where(Function(x) x.ConceptId = ObjListLiquidationDetailParafiscal.ConceptId).FirstOrDefault().AccruedAccount
                                CostDistributionConcept.AccruedValue = ObjListLiquidationDetailParafiscal.ConceptTotalValue 'Valor Débito
                                CostDistributionConcept.DeductedAccount = Nothing
                                CostDistributionConcept.DeductedValue = 0
                            Else
                                ActionMessageResult.MessageResult.Add("El Concepto " + ObjListLiquidationDetailParafiscal.ConceptCode + " no tiene Cuenta Débito asignada")
                                ActionMessageResult.StateResult = False
                            End If
                        Else
                            If ListConceptAccount.Where(Function(x) x.ConceptId = ObjListLiquidationDetailParafiscal.ConceptId).FirstOrDefault().DeductedAccount IsNot Nothing Then
                                CostDistributionConcept.DeductedAccount = ListConceptAccount.Where(Function(x) x.ConceptId = ObjListLiquidationDetailParafiscal.ConceptId).FirstOrDefault().DeductedAccount
                                CostDistributionConcept.DeductedValue = ObjListLiquidationDetailParafiscal.ConceptTotalValue ' Valor Crédito
                                CostDistributionConcept.AccruedAccount = Nothing
                                CostDistributionConcept.AccruedValue = 0
                            Else
                                ActionMessageResult.MessageResult.Add("El Concepto " + ObjListLiquidationDetailParafiscal.ConceptCode + " no tiene Cuenta Crédito asignada")
                                ActionMessageResult.StateResult = False
                            End If

                        End If

                        If ObjListLiquidationDetailParafiscal.ConceptClass = "035" Then ' SENA
                            If _thirdPartyRepository.GetThirdPartyByNit("899999034") IsNot Nothing Then
                                CostDistributionConcept.CompanyNit = "899999034"
                                CostDistributionConcept.CompanyId = _thirdPartyRepository.GetThirdPartyByNit("899999034").Id
                            Else
                                ActionMessageResult.MessageResult.Add("No está creado el SENA con Nit 899999034 en Terceros")
                                ActionMessageResult.StateResult = False
                            End If

                        End If

                        If ObjListLiquidationDetailParafiscal.ConceptClass = "037" Then ' ICBF
                            If _thirdPartyRepository.GetThirdPartyByNit("899999239") IsNot Nothing Then
                                CostDistributionConcept.CompanyNit = "899999239"
                                CostDistributionConcept.CompanyId = _thirdPartyRepository.GetThirdPartyByNit("899999239").Id
                            Else
                                ActionMessageResult.MessageResult.Add("No está creado el ICBF con Nit 899999239 en Terceros")
                                ActionMessageResult.StateResult = False
                            End If
                        End If

                        If ObjListLiquidationDetailParafiscal.ConceptClass = "036" Then ' CAJA DE COMPENSACIÓN
                            If AccountingContractData.FundContract.Where(Function(x) x.FundType = 5 And x.State = True).FirstOrDefault() IsNot Nothing Then
                                Dim IdCompensationFund As Integer = AccountingContractData.FundContract.Where(Function(x) x.FundType = 5 And x.State = True).FirstOrDefault().FundId
                                If IdCompensationFund > 0 Then
                                    CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(IdCompensationFund).ThirdParty.Nit
                                    CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(IdCompensationFund).ThirdParty.Id
                                End If
                            Else
                                ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Caja de Compensación Válida")
                            End If

                        End If

                        If ObjListLiquidationDetailParafiscal.ConceptClass = "018" Then ' SALUD
                            If PayrollLiquidation.HealthFundId IsNot Nothing Then
                                CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.HealthFundId).ThirdParty.Nit
                                CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.HealthFundId).ThirdParty.Id
                            End If
                        End If

                        If ObjListLiquidationDetailParafiscal.ConceptClass = "015" Then ' PENSIÓN
                            If PayrollLiquidation.PensionFundId IsNot Nothing Then
                                CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.PensionFundId).ThirdParty.Nit
                                CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.PensionFundId).ThirdParty.Id
                            End If
                        End If

                        If ObjListLiquidationDetailParafiscal.ConceptClass = "009" Then 'ARL
                            If AccountingContractData.FundContract.Where(Function(x) x.FundType = 4 And x.State = True).FirstOrDefault() IsNot Nothing Then
                                Dim IDRiskFund As Integer = AccountingContractData.FundContract.Where(Function(x) x.FundType = 4 And x.State = True).FirstOrDefault().FundId
                                If IDRiskFund > 0 Then
                                    CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(IDRiskFund).ThirdParty.Nit
                                    CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(IDRiskFund).ThirdParty.Id
                                End If
                            Else
                                ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene ARL Válida")
                            End If
                        End If

                        CostDistributionConcept.EmployeeNit = PayrollLiquidation.NitEmployee

                        CostDistributionConcept.CostCenterId = PayrollLiquidation.Contract.FunctionalUnit.CostCenterId
                        CostDistributionConcept.SubCostCenterId = Nothing
                        CostDistributionConcept.EmployeeId = PayrollLiquidation.EmployeeId
                        CostDistributionConcept.ConceptId = ObjListLiquidationDetailParafiscal.ConceptId
                        CostDistributionConcept.NumberHours = 0
                        CostDistributionConcept.TotalConceptValue = ObjListLiquidationDetailParafiscal.ConceptTotalValue
                        CostDistributionConcept.Status = False

                        ListCostDistribution.Add(CostDistributionConcept)
                    Next

                    ListNormalConcept.Remove(ObjListLiquidationDetailParafiscal)

                Next

            End If

            'Lista de Incapacidades
            If ListLiquidationDetailInabilities IsNot Nothing And ListLiquidationDetailInabilities.Count > 0 Then

                For Each objListInabilities As LiquidationDetail In ListLiquidationDetailInabilities

                    Dim SpendingInability As Double = 0
                    Dim InabilityCollect As Double = 0

                    For i As Integer = 0 To 1
                        If i = 0 And objListInabilities.SpendingInability > 0 Then

                            Dim CostDistributionConcept As New CostDistributions()

                            If IndigoPayrollIntegration = 2 Then
                                CostDistributionConcept.CostCenter = _costCenterRepository.GetCostCenterById(PayrollLiquidation.Employee.CostCenterId, False)
                                CostDistributionConcept.Group = ObjGroup
                                CostDistributionConcept.Concept = _conceptRepository.GetConcept(objListInabilities.ConceptCode)
                            End If

                            'CostDistributionConcept.CostCenter = PayrollLiquidation.Employee.CostCenter
                            CostDistributionConcept.CostCenterId = PayrollLiquidation.Contract.FunctionalUnit.CostCenterId
                            'CostDistributionConcept.Group = PayrollLiquidation.Group
                            CostDistributionConcept.GroupId = PayrollLiquidation.GroupId
                            'CostDistributionConcept.Concept = objListInabilities.Concept
                            CostDistributionConcept.ContractId = PayrollLiquidation.ContractId
                            CostDistributionConcept.PayrollDateLiquidated = PayrollLiquidation.PayrollDateLiquidated

                            CostDistributionConcept.AccountingStatementNumber = ObjGroup.PayrollParameter.PayrollVoucherCode
                            CostDistributionConcept.JournalVoucherTypeId = PayrollJournalVoucherTypeId

                            'Los primeros dos días
                            SpendingInability = objListInabilities.SpendingInability
                            CostDistributionConcept.SpendingInability = objListInabilities.SpendingInability
                            CostDistributionConcept.InabilityCollect = 0

                            If ListConceptAccount.Where(Function(x) x.ConceptId = objListInabilities.ConceptId).FirstOrDefault() IsNot Nothing Then
                                If ListConceptAccount.Where(Function(x) x.ConceptId = objListInabilities.ConceptId).FirstOrDefault().InabilityDebitValueEmployeeAccount IsNot Nothing Then
                                    CostDistributionConcept.DeductedAccount = ListConceptAccount.Where(Function(x) x.ConceptId = objListInabilities.ConceptId).FirstOrDefault().InabilityDebitValueEmployeeAccount
                                    CostDistributionConcept.DeductedValue = objListInabilities.ConceptTotalValue ' Valor Crédito
                                    CostDistributionConcept.AccruedAccount = Nothing
                                Else
                                    ListMessageError.Add("El Concepto " + objListInabilities.ConceptCode + " no tiene Cuentas asignadas")
                                End If
                            Else
                                ListMessageError.Add("El Concepto " + objListInabilities.ConceptCode + " no tiene Cuentas asignadas")
                            End If

                            If ListConceptAccount.Where(Function(x) x.ConceptId = objListInabilities.ConceptId).FirstOrDefault() IsNot Nothing Then
                                If ListConceptAccount.Where(Function(x) x.ConceptId = objListInabilities.ConceptId).FirstOrDefault().InabilityDebitValueEmployeeAccount IsNot Nothing Then
                                    CostDistributionConcept.AccruedAccount = ListConceptAccount.Where(Function(x) x.ConceptId = objListInabilities.ConceptId).FirstOrDefault().InabilityDebitValueEmployeeAccount
                                Else
                                    ListMessageError.Add("El Concepto " + objListInabilities.ConceptCode + " no tiene Cuentas asignadas")
                                End If
                            Else
                                ListMessageError.Add("El Concepto " + objListInabilities.ConceptCode + " no tiene Cuentas asignadas")
                            End If

                            CostDistributionConcept.DeductedValue = 0 ' Valor Crédito
                            CostDistributionConcept.AccruedValue = 0

                            'If PayrollLiquidation.HealthFundId IsNot Nothing Then
                            '    CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.HealthFundId).ThirdParty.Nit
                            '    CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.HealthFundId).ThirdParty.Id
                            'Else
                            '    ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Salud")
                            'End If

                            CostDistributionConcept.EmployeeId = PayrollLiquidation.EmployeeId
                            CostDistributionConcept.EmployeeNit = PayrollLiquidation.NitEmployee
                            ' CostDistributionConcept.TotalConceptValue = CDbl(objListInabilities.SpendingInability)
                            CostDistributionConcept.TotalConceptValue = 0

                            CostDistributionConcept.SubCostCenterId = Nothing
                            CostDistributionConcept.EmployeeId = PayrollLiquidation.EmployeeId
                            CostDistributionConcept.ConceptId = objListInabilities.ConceptId
                            CostDistributionConcept.NumberHours = 0
                            CostDistributionConcept.Status = False
                            ListCostDistribution.Add(CostDistributionConcept)

                        ElseIf i = 1 And objListInabilities.InabilityCollect > 0 Then

                            ' A la EPS

                            Dim CostDistributionConcept As New CostDistributions()

                            If IndigoPayrollIntegration = 2 Then
                                CostDistributionConcept.CostCenter = _costCenterRepository.GetCostCenterById(PayrollLiquidation.Employee.CostCenterId, False)
                                CostDistributionConcept.Group = ObjGroup
                                CostDistributionConcept.Concept = _conceptRepository.GetConcept(objListInabilities.ConceptCode)
                            End If

                            'CostDistributionConcept.CostCenter = PayrollLiquidation.Employee.CostCenter
                            CostDistributionConcept.CostCenterId = PayrollLiquidation.Contract.FunctionalUnit.CostCenterId
                            'CostDistributionConcept.Group = PayrollLiquidation.Group
                            CostDistributionConcept.GroupId = PayrollLiquidation.GroupId
                            'CostDistributionConcept.Concept = objListInabilities.Concept
                            CostDistributionConcept.ContractId = PayrollLiquidation.ContractId
                            CostDistributionConcept.PayrollDateLiquidated = PayrollLiquidation.PayrollDateLiquidated

                            CostDistributionConcept.AccountingStatementNumber = "Incapacidades"
                            CostDistributionConcept.JournalVoucherTypeId = Nothing

                            If PayrollLiquidation.HealthFundId IsNot Nothing Then
                                CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.HealthFundId).ThirdParty.Nit
                                CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.HealthFundId).ThirdParty.Id
                            Else
                                ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Salud")
                            End If

                            If ListConceptAccount.Where(Function(x) x.ConceptId = objListInabilities.ConceptId).FirstOrDefault() IsNot Nothing Then
                                If ListConceptAccount.Where(Function(x) x.ConceptId = objListInabilities.ConceptId).FirstOrDefault().InabilityDebitValueEPSAccount IsNot Nothing Then
                                    CostDistributionConcept.AccruedAccount = ListConceptAccount.Where(Function(x) x.ConceptId = objListInabilities.ConceptId).FirstOrDefault().InabilityDebitValueEPSAccount
                                Else
                                    ListMessageError.Add("El Concepto " + objListInabilities.ConceptCode + " no tiene Cuentas asignadas")
                                End If
                            Else
                                ListMessageError.Add("El Concepto " + objListInabilities.ConceptCode + " no tiene Cuentas asignadas")
                            End If

                            InabilityCollect = objListInabilities.InabilityCollect
                            CostDistributionConcept.InabilityCollect = objListInabilities.InabilityCollect
                            CostDistributionConcept.SpendingInability = 0
                            CostDistributionConcept.DeductedValue = 0 ' Valor Crédito
                            CostDistributionConcept.AccruedValue = 0
                            'CostDistributionConcept.TotalConceptValue = CDbl(objListInabilities.InabilityCollect)
                            CostDistributionConcept.TotalConceptValue = 0
                            CostDistributionConcept.EmployeeNit = PayrollLiquidation.NitEmployee
                            CostDistributionConcept.SubCostCenterId = Nothing
                            CostDistributionConcept.EmployeeId = PayrollLiquidation.EmployeeId
                            CostDistributionConcept.ConceptId = objListInabilities.ConceptId
                            CostDistributionConcept.NumberHours = 0
                            CostDistributionConcept.Status = False
                            ListCostDistribution.Add(CostDistributionConcept)
                        End If

                    Next

                    ListNormalConcept.Remove(objListInabilities)

                Next
            End If

            If ListNormalConcept IsNot Nothing And ListNormalConcept.Count() > 0 Then

                For Each objListNormalConcept As LiquidationDetail In ListNormalConcept

                    If objListNormalConcept.ConceptType = 1 Then

                        Dim ObjConceptAccountingStructure = ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault()

                        If ObjConceptAccountingStructure Is Nothing Then
                            Dim ObjConcept = _conceptRepository.GetConcept(objListNormalConcept.ConceptCode)
                            ListMessageError.Add("Revise el concepto " + ObjConcept.Code + " - " + ObjConcept.Name + ". Verifique las cuentas")
                            Exit For
                        End If

                        If ObjConceptAccountingStructure.DeductedAccount = Nothing Or ObjConceptAccountingStructure.DeductedAccount.ToString() = String.Empty Then
                            Dim ObjConcept = _conceptRepository.GetConcept(objListNormalConcept.ConceptCode)
                            ListMessageError.Add("El concepto " + ObjConcept.Code + " - " + ObjConcept.Name + "no tiene cuenta Crédito asignada")
                            Exit For
                        End If

                        If ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault().DeductedAccount = ObjGroup.PayrollParameter.PayrollAccount Then

                            Dim CostDistributionConcept As New CostDistributions()

                            If IndigoPayrollIntegration = 2 Then
                                CostDistributionConcept.CostCenter = _costCenterRepository.GetCostCenterById(PayrollLiquidation.Employee.CostCenterId, False)
                                CostDistributionConcept.Group = ObjGroup
                                CostDistributionConcept.Concept = _conceptRepository.GetConcept(objListNormalConcept.ConceptCode)
                            End If

                            CostDistributionConcept.CostCenterId = PayrollLiquidation.Contract.FunctionalUnit.CostCenterId
                            CostDistributionConcept.GroupId = PayrollLiquidation.GroupId
                            CostDistributionConcept.ContractId = PayrollLiquidation.ContractId
                            CostDistributionConcept.PayrollDateLiquidated = PayrollLiquidation.PayrollDateLiquidated

                            CostDistributionConcept.AccountingStatementNumber = ObjGroup.PayrollParameter.PayrollVoucherCode
                            CostDistributionConcept.JournalVoucherTypeId = PayrollJournalVoucherTypeId

                            'Reviso que sean Conceptos de Aportes de Salud y Pensión para colocar el Nit del Fondo, o un Convenio

                            ' Salud
                            If objListNormalConcept.ConceptClass = "017" Or objListNormalConcept.ConceptClass = "019" Then

                                If PayrollLiquidation.HealthFundId IsNot Nothing Then
                                    CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.HealthFundId).ThirdParty.Nit
                                    CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.HealthFundId).ThirdParty.Id
                                Else
                                    ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Salud")
                                End If

                                If objListNormalConcept.ConceptClass = "019" Then
                                    If PayrollLiquidation.VoluntaryHealthFundId IsNot Nothing Then
                                        CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.VoluntaryHealthFundId).ThirdParty.Nit
                                        CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.VoluntaryHealthFundId).ThirdParty.Id
                                    Else
                                        ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Salud Voluntario")
                                    End If
                                End If

                            ElseIf objListNormalConcept.ConceptClass = "014" Or objListNormalConcept.ConceptClass = "016" Or objListNormalConcept.ConceptClass = "038" Then
                                'Pensión

                                If PayrollLiquidation.PensionFundId IsNot Nothing Then
                                    CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.PensionFundId).ThirdParty.Nit
                                    CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.PensionFundId).ThirdParty.Id
                                Else
                                    ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Pensión")
                                End If

                                If objListNormalConcept.ConceptClass = "016" Then
                                    If PayrollLiquidation.VoluntaryPensionFundId IsNot Nothing Then
                                        CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.VoluntaryPensionFundId).ThirdParty.Nit
                                        CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.VoluntaryPensionFundId).ThirdParty.Id
                                    Else
                                        ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Pensión Voluntario")
                                    End If
                                End If

                            ElseIf objListNormalConcept.ConceptClass = "041" Then
                                'Reviso si es un Convenio, para tomar el Nit de la Entidad
                                'CONVENIOS
                                CostDistributionConcept.CompanyNit = _IAgreementsCRepository.GetAgreementsById(objListNormalConcept.AgreementsId).Company.ThirdParty.Nit
                                CostDistributionConcept.CompanyId = _IAgreementsCRepository.GetAgreementsById(objListNormalConcept.AgreementsId).Company.ThirdParty.Id
                            ElseIf objListNormalConcept.ConceptClass = "044" Then
                                ' SINDICATOS
                                CostDistributionConcept.CompanyNit = _tradeUnionRepository.GetTradeUnionByConceptId(objListNormalConcept.ConceptId, True).ThirdParty.Nit
                                CostDistributionConcept.CompanyId = _tradeUnionRepository.GetTradeUnionByConceptId(objListNormalConcept.ConceptId, True).ThirdParty.Id
                            End If

                            'Devengado
                            If AccountingContractData.FunctionalUnit.AccountingStructure.ConceptAccountingStructure.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault() IsNot Nothing Then
                                If ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault() IsNot Nothing Then
                                    If ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault().AccruedAccount IsNot Nothing Then
                                        CostDistributionConcept.AccruedAccount = ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault().AccruedAccount
                                        CostDistributionConcept.DeductedAccount = ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault().DeductedAccount
                                        CostDistributionConcept.AccruedValue = CDbl(objListNormalConcept.ConceptTotalValue) 'Valor Débito
                                        CostDistributionConcept.DeductedValue = 0 ' Valor Crédito
                                    Else
                                        ListMessageError.Add("El Concepto " + objListNormalConcept.ConceptCode + " no tiene Cuenta Débito asignada")
                                    End If
                                Else
                                    ListMessageError.Add("El Concepto " + objListNormalConcept.ConceptCode + " no tiene Cuenta Débito asignada")
                                End If
                            Else
                                ListMessageError.Add("El Concepto " + objListNormalConcept.ConceptCode + " no tiene Cuenta Débito asignada")
                            End If

                            CostDistributionConcept.EmployeeNit = PayrollLiquidation.NitEmployee
                            CostDistributionConcept.SubCostCenterId = Nothing
                            CostDistributionConcept.EmployeeId = PayrollLiquidation.EmployeeId
                            CostDistributionConcept.ConceptId = objListNormalConcept.ConceptId
                            CostDistributionConcept.NumberHours = 0
                            CostDistributionConcept.TotalConceptValue = CDbl(objListNormalConcept.ConceptTotalValue)
                            CostDistributionConcept.Status = False

                            ListCostDistribution.Add(CostDistributionConcept)
                        Else
                            For i As Integer = 0 To 1

                                Dim CostDistributionConcept As New CostDistributions()

                                If IndigoPayrollIntegration = 2 Then
                                    CostDistributionConcept.CostCenter = _costCenterRepository.GetCostCenterById(PayrollLiquidation.Employee.CostCenterId, False)
                                    CostDistributionConcept.Group = ObjGroup
                                    CostDistributionConcept.Concept = _conceptRepository.GetConcept(objListNormalConcept.ConceptCode)
                                End If

                                CostDistributionConcept.CostCenterId = PayrollLiquidation.Contract.FunctionalUnit.CostCenterId
                                CostDistributionConcept.GroupId = PayrollLiquidation.GroupId
                                CostDistributionConcept.ContractId = PayrollLiquidation.ContractId
                                CostDistributionConcept.PayrollDateLiquidated = PayrollLiquidation.PayrollDateLiquidated

                                CostDistributionConcept.AccountingStatementNumber = ObjGroup.PayrollParameter.PayrollVoucherCode
                                CostDistributionConcept.JournalVoucherTypeId = PayrollJournalVoucherTypeId

                                'Reviso que sean Conceptos de Aportes de Salud y Pensión para colocar el Nit del Fondo, o un Convenio

                                ' Salud
                                If objListNormalConcept.ConceptClass = "017" Or objListNormalConcept.ConceptClass = "019" Then

                                    If PayrollLiquidation.HealthFundId IsNot Nothing Then
                                        CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.HealthFundId).ThirdParty.Nit
                                        CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.HealthFundId).ThirdParty.Id
                                    Else
                                        ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Salud")
                                    End If

                                    If objListNormalConcept.ConceptClass = "019" Then
                                        If PayrollLiquidation.VoluntaryHealthFundId IsNot Nothing Then
                                            CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.VoluntaryHealthFundId).ThirdParty.Nit
                                            CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.VoluntaryHealthFundId).ThirdParty.Id
                                        Else
                                            ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Salud Voluntario")
                                        End If
                                    End If

                                ElseIf objListNormalConcept.ConceptClass = "014" Or objListNormalConcept.ConceptClass = "016" Or objListNormalConcept.ConceptClass = "038" Then
                                    'Pensión

                                    If PayrollLiquidation.PensionFundId IsNot Nothing Then
                                        CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.PensionFundId).ThirdParty.Nit
                                        CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.PensionFundId).ThirdParty.Id
                                    Else
                                        ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Pensión")
                                    End If

                                    If objListNormalConcept.ConceptClass = "016" Then
                                        If PayrollLiquidation.VoluntaryPensionFundId IsNot Nothing Then
                                            CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.VoluntaryPensionFundId).ThirdParty.Nit
                                            CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.VoluntaryPensionFundId).ThirdParty.Id
                                        Else
                                            ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Pensión Voluntario")
                                        End If
                                    End If

                                ElseIf objListNormalConcept.ConceptClass = "041" Then
                                    'Reviso si es un Convenio, para tomar el Nit de la Entidad
                                    'CONVENIOS
                                    CostDistributionConcept.CompanyNit = _IAgreementsCRepository.GetAgreementsById(objListNormalConcept.AgreementsId).Company.ThirdParty.Nit
                                    CostDistributionConcept.CompanyId = _IAgreementsCRepository.GetAgreementsById(objListNormalConcept.AgreementsId).Company.ThirdParty.Id
                                ElseIf objListNormalConcept.ConceptClass = "044" Then
                                    ' SINDICATOS
                                    CostDistributionConcept.CompanyNit = _tradeUnionRepository.GetTradeUnionByConceptId(objListNormalConcept.ConceptId, True).ThirdParty.Nit
                                    CostDistributionConcept.CompanyId = _tradeUnionRepository.GetTradeUnionByConceptId(objListNormalConcept.ConceptId, True).ThirdParty.Id

                                End If

                                'If objListNormalConcept.ConceptType = 1 Then

                                If i = 0 Then
                                    'Devengado
                                    If AccountingContractData.FunctionalUnit.AccountingStructure.ConceptAccountingStructure.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault() IsNot Nothing Then
                                        If ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault() IsNot Nothing Then
                                            If ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault().AccruedAccount IsNot Nothing Then
                                                CostDistributionConcept.AccruedAccount = ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault().AccruedAccount
                                                CostDistributionConcept.DeductedAccount = Nothing
                                                CostDistributionConcept.AccruedValue = CDbl(objListNormalConcept.ConceptTotalValue) 'Valor Débito
                                                CostDistributionConcept.DeductedValue = 0 ' Valor Crédito
                                            Else
                                                ListMessageError.Add("El Concepto " + objListNormalConcept.ConceptCode + " no tiene Cuenta Débito asignada")
                                            End If
                                        Else
                                            ListMessageError.Add("El Concepto " + objListNormalConcept.ConceptCode + " no tiene Cuenta Débito asignada")
                                        End If
                                    Else
                                        ListMessageError.Add("El Concepto " + objListNormalConcept.ConceptCode + " no tiene Cuenta Débito asignada")
                                    End If
                                Else
                                    If AccountingContractData.FunctionalUnit.AccountingStructure.ConceptAccountingStructure.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault() IsNot Nothing Then
                                        If ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault() IsNot Nothing Then
                                            If ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault().DeductedAccount IsNot Nothing Then

                                                If ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault().DeductedAccount = ObjGroup.PayrollParameter.PayrollAccount Then
                                                    i = 2
                                                    Exit For
                                                End If

                                                CostDistributionConcept.DeductedAccount = ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault().DeductedAccount
                                                CostDistributionConcept.AccruedAccount = Nothing
                                                CostDistributionConcept.AccruedValue = 0
                                                CostDistributionConcept.DeductedValue = CDbl(objListNormalConcept.ConceptTotalValue) 'Valor Débito
                                            Else
                                                ListMessageError.Add("El Concepto " + objListNormalConcept.ConceptCode + " no tiene Cuenta Crédito asignada")
                                            End If
                                        Else
                                            ListMessageError.Add("El Concepto " + objListNormalConcept.ConceptCode + " no tiene Cuenta Crédito asignada")
                                        End If
                                    Else
                                        ListMessageError.Add("El Concepto " + objListNormalConcept.ConceptCode + " no tiene Cuenta Crédito asignada")
                                    End If
                                End If

                                CostDistributionConcept.EmployeeNit = PayrollLiquidation.NitEmployee
                                CostDistributionConcept.SubCostCenterId = Nothing
                                CostDistributionConcept.EmployeeId = PayrollLiquidation.EmployeeId
                                CostDistributionConcept.ConceptId = objListNormalConcept.ConceptId
                                CostDistributionConcept.NumberHours = 0
                                CostDistributionConcept.TotalConceptValue = CDbl(objListNormalConcept.ConceptTotalValue)
                                CostDistributionConcept.Status = False

                                ListCostDistribution.Add(CostDistributionConcept)
                            Next

                        End If
                    ElseIf objListNormalConcept.ConceptType = 2 Then

                        Dim CostDistributionConcept As New CostDistributions()

                        If IndigoPayrollIntegration = 2 Then
                            CostDistributionConcept.CostCenter = _costCenterRepository.GetCostCenterById(PayrollLiquidation.Employee.CostCenterId, False)
                            CostDistributionConcept.Group = ObjGroup
                            CostDistributionConcept.Concept = _conceptRepository.GetConcept(objListNormalConcept.ConceptCode)
                        End If

                        CostDistributionConcept.CostCenterId = PayrollLiquidation.Contract.FunctionalUnit.CostCenterId
                        'CostDistributionConcept.CostCenter = PayrollLiquidation.Employee.CostCenter
                        'CostDistributionConcept.Group = PayrollLiquidation.Group
                        'CostDistributionConcept.Concept = objListNormalConcept.Concept
                        CostDistributionConcept.GroupId = PayrollLiquidation.GroupId
                        CostDistributionConcept.ContractId = PayrollLiquidation.ContractId
                        CostDistributionConcept.PayrollDateLiquidated = PayrollLiquidation.PayrollDateLiquidated

                        CostDistributionConcept.AccountingStatementNumber = ObjGroup.PayrollParameter.PayrollVoucherCode
                        CostDistributionConcept.JournalVoucherTypeId = PayrollJournalVoucherTypeId

                        'Reviso que sean Conceptos de Aportes de Salud y Pensión para colocar el Nit del Fondo, o un Convenio

                        ' Salud
                        If objListNormalConcept.ConceptClass = "017" Or objListNormalConcept.ConceptClass = "019" Then

                            If PayrollLiquidation.HealthFundId IsNot Nothing Then
                                CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.HealthFundId).ThirdParty.Nit
                                CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.HealthFundId).ThirdParty.Id
                            Else
                                ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Salud")
                            End If

                            If objListNormalConcept.ConceptClass = "019" Then
                                If PayrollLiquidation.VoluntaryHealthFundId IsNot Nothing Then
                                    CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.VoluntaryHealthFundId).ThirdParty.Nit
                                    CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.VoluntaryHealthFundId).ThirdParty.Id
                                Else
                                    ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Salud Voluntario")
                                End If
                            End If
                        ElseIf objListNormalConcept.ConceptClass = "014" Or objListNormalConcept.ConceptClass = "016" Or objListNormalConcept.ConceptClass = "038" Then
                            'Pensión
                            If PayrollLiquidation.PensionFundId IsNot Nothing Then
                                CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.PensionFundId).ThirdParty.Nit
                                CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.PensionFundId).ThirdParty.Id
                            Else
                                ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Pensión")
                            End If

                            If objListNormalConcept.ConceptClass = "016" Then
                                If PayrollLiquidation.VoluntaryPensionFundId IsNot Nothing Then
                                    CostDistributionConcept.CompanyNit = _FundsRepository.GetFundsById(PayrollLiquidation.VoluntaryPensionFundId).ThirdParty.Nit
                                    CostDistributionConcept.CompanyId = _FundsRepository.GetFundsById(PayrollLiquidation.VoluntaryPensionFundId).ThirdParty.Id
                                Else
                                    ListMessageError.Add("El empleado " + PayrollLiquidation.NitEmployee + " no tiene Fondo de Pensión Voluntario")
                                End If
                            End If
                        ElseIf objListNormalConcept.ConceptClass = "041" Then
                            'Reviso si es un Convenio, para tomar el Nit de la Entidad
                            'CONVENIOS
                            CostDistributionConcept.CompanyNit = _IAgreementsCRepository.GetAgreementsById(objListNormalConcept.AgreementsId).Company.ThirdParty.Nit
                            CostDistributionConcept.CompanyId = _IAgreementsCRepository.GetAgreementsById(objListNormalConcept.AgreementsId).Company.ThirdParty.Id
                        ElseIf objListNormalConcept.ConceptClass = "044" Then
                            ' SINDICATOS
                            CostDistributionConcept.CompanyNit = _tradeUnionRepository.GetTradeUnionByConceptId(objListNormalConcept.ConceptId, True).ThirdParty.Nit
                            CostDistributionConcept.CompanyId = _tradeUnionRepository.GetTradeUnionByConceptId(objListNormalConcept.ConceptId, True).ThirdParty.Id
                        End If

                        'Deducidos
                        If AccountingContractData.FunctionalUnit.AccountingStructure.ConceptAccountingStructure.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault() IsNot Nothing Then
                            If ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault() IsNot Nothing Then
                                If ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault().DeductedAccount IsNot Nothing Then
                                    CostDistributionConcept.AccruedAccount = Nothing
                                    CostDistributionConcept.DeductedAccount = ListConceptAccount.Where(Function(x) x.ConceptId = objListNormalConcept.ConceptId).FirstOrDefault().DeductedAccount
                                    CostDistributionConcept.AccruedValue = 0 'Valor Débito
                                    CostDistributionConcept.DeductedValue = CDbl(objListNormalConcept.ConceptTotalValue) ' Valor Crédito
                                Else
                                    ListMessageError.Add("El Concepto " + objListNormalConcept.ConceptCode + " no tiene Cuenta Credito asignada")
                                End If
                            Else
                                ListMessageError.Add("El Concepto " + objListNormalConcept.ConceptCode + " no tiene Cuenta Credito asignada")
                            End If
                        Else
                            ListMessageError.Add("El Concepto " + objListNormalConcept.ConceptCode + " no tiene Cuenta Credito asignada")
                        End If

                        CostDistributionConcept.EmployeeNit = PayrollLiquidation.NitEmployee
                        CostDistributionConcept.SubCostCenterId = Nothing
                        CostDistributionConcept.EmployeeId = PayrollLiquidation.EmployeeId
                        CostDistributionConcept.ConceptId = objListNormalConcept.ConceptId
                        CostDistributionConcept.NumberHours = 0
                        CostDistributionConcept.TotalConceptValue = CDbl(objListNormalConcept.ConceptTotalValue)
                        CostDistributionConcept.Status = False

                        ListCostDistribution.Add(CostDistributionConcept)
                    End If
                Next
            End If

            If ListMessageError IsNot Nothing And ListMessageError.Count > 0 Then
                ResultObjCostDistribution.StateResult = False
                ResultObjCostDistribution.MessageResult = ListMessageError
            Else
                ResultObjCostDistribution.StateResult = True
                ResultObjCostDistribution.ObjectEmbbeded = ListCostDistribution
            End If
            Return ResultObjCostDistribution
        Catch ex As Exception
            ActionMessageResult.StateResult = False
            ActionMessageResult.MessageResult.Add("Error: " + Utils.GetInnerExceptionMessageToString(ex))

            Return ActionMessageResult
        End Try
    End Function

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="ListCostDistribution"></param>
    ''' <param name="Group"></param>
    ''' <param name="indigo"></param>
    ''' <param name="LiquidationConfirm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateAccountingByVie(ListCostDistribution As List(Of CostDistributions), Group As Group, indigo As SessionValues, LiquidationConfirm As List(Of Liquidation)) As ActionResult(Of List(Of Domain.Entities.JournalVouchers)) Implements ICostDistributionDomain.CreateAccountingByVie

        Dim ResultObjJournalVouchers As New ActionResult(Of List(Of Domain.Entities.JournalVouchers))
        Dim ListMessageError As New List(Of String)
        Try

            Dim ListConcept As List(Of Domain.Payroll.Entities.Concept) = _conceptRepository.GetAll().ToList()

            Dim PayrollSettings = _payrollSettingsRepository.GetSettingPayroll()

            '' Validación para que los comprobantes contables salgan con la FEcha de la Nómina o la Fecha Actual
            Dim VoucherDate As Date

            If LiquidationConfirm.Item(0).PayrollDateLiquidated < Date.Now Then
                VoucherDate = LiquidationConfirm.Item(0).PayrollDateLiquidated
            Else
                VoucherDate = Date.Now
            End If

            If ListCostDistribution IsNot Nothing And ListCostDistribution.Count > 0 Then
                Dim ListJournalVoucher As New List(Of Domain.Entities.JournalVouchers)

                Dim PrestacionJournalVoucher As New Domain.Entities.JournalVouchers
                Dim ProvisionJournalVoucher As New Domain.Entities.JournalVouchers
                Dim InabilityJournalVoucher As New Domain.Entities.JournalVouchers

                Dim ListPayrollCostDistribution = ListCostDistribution.Where(Function(x) x.AccountingStatementNumber = Group.PayrollParameter.PayrollVoucherCode).ToList()
                Dim ListPrestacionCostDistribution = ListCostDistribution.Where(Function(x) x.AccountingStatementNumber = Group.PayrollParameter.PrestacionVoucherCode).ToList()
                Dim ListProvisionCostDistribution = ListCostDistribution.Where(Function(x) x.AccountingStatementNumber = Group.PayrollParameter.ProvisionVoucherCode).ToList()

                Dim PayrollJournalVoucherTypeId = _CostDistributionRepository.GetJournalVoucherTypes(Group.PayrollParameter.PayrollVoucherCode).Id
                Dim PrestacionJournalVoucherTypeId = _CostDistributionRepository.GetJournalVoucherTypes(Group.PayrollParameter.PrestacionVoucherCode).Id
                Dim ProvisionJournalVoucherTypeId = _CostDistributionRepository.GetJournalVoucherTypes(Group.PayrollParameter.ProvisionVoucherCode).Id

                Dim TotalNonValueConcept As Double = 0

                'ListPayrollCostDistribution = ListPayrollCostDistribution.Where(Function(x) x.EmployeeId = 89).ToList()
                If ListPayrollCostDistribution IsNot Nothing And ListPayrollCostDistribution.Count > 0 Then
                    Dim TotalValuePayrollAcount As Double = 0
                    Dim PayrollJournalVoucher As New Domain.Entities.JournalVouchers
                    Dim TmpListPayrollCostDistribution As New List(Of Domain.Payroll.Entities.CostDistributions)
                    'Dim TmpListTotalNonValueConcept As New List(Of Domain.Payroll.Entities.CostDistributions)

                    Dim ListEmployee = ((From e In ListPayrollCostDistribution Select e.EmployeeId).Distinct()).ToList
                    For Each EmployeeId In ListEmployee
                        Dim ValueAccruedValue As Double = 0
                        Dim ValueDeductedValue As Double = 0
                        Dim TotalDeducted As Double = 0

                        Dim ListIdConceptAccrued = ListPayrollCostDistribution.Where(Function(x) x.EmployeeId = EmployeeId And x.AccruedValue > 0 And x.DeductedValue = 0).ToList().Select(Function(y) y.ConceptId).ToList()
                        Dim TmpLiquidation = LiquidationConfirm.Where(Function(x) x.EmployeeId = EmployeeId).First()

                        ValueAccruedValue = ListPayrollCostDistribution.Where(Function(x) x.EmployeeId = EmployeeId And x.AccruedValue > 0 And x.DeductedValue = 0 And x.DeductedAccount = Group.PayrollParameter.PayrollAccount).ToList().Sum(Function(y) y.AccruedValue)
                        ValueDeductedValue = ListPayrollCostDistribution.Where(Function(x) x.EmployeeId = EmployeeId And x.AccruedValue = 0 And x.DeductedValue > 0 And ListIdConceptAccrued.Exists(Function(d) d = x.ConceptId) = False).ToList().Sum(Function(y) y.DeductedValue)
                        TotalDeducted = TmpLiquidation.TotalDeducted

                        Dim EmployeeThirdPartyId As Integer = TmpLiquidation.LiquidationDetail.First().IdThirdParty

                        For Each PayrollCostDistribution In ListPayrollCostDistribution.Where(Function(y) y.EmployeeId = EmployeeId)
                            Dim DetailPayrollJournalVoucherDetail As New Domain.Entities.JournalVoucherDetails
                            Dim ThirdPartyId As Integer
                            Dim TmpObjPayrollCostDistribution As New Domain.Payroll.Entities.CostDistributions

                            If PayrollCostDistribution.CompanyNit IsNot Nothing And PayrollCostDistribution.CompanyNit > 0 Then
                                ThirdPartyId = _thirdPartyRepository.GetThirdPartyByNit(PayrollCostDistribution.CompanyNit).Id
                            ElseIf PayrollCostDistribution.EmployeeId > 0 Then
                                ThirdPartyId = EmployeeThirdPartyId
                            End If

                            'Cargo el Concepto que se está revisando
                            Dim ObjConcept = ListConcept.Where(Function(x) x.Id = PayrollCostDistribution.ConceptId).FirstOrDefault()

                            If ObjConcept.ConceptType = 1 Then
                                Dim tmpDeducted As Double = If(PayrollCostDistribution.DeductedValue > TotalDeducted, TotalDeducted, PayrollCostDistribution.DeductedValue)

                                TotalDeducted = TotalDeducted - tmpDeducted
                                PayrollCostDistribution.DeductedValue = PayrollCostDistribution.DeductedValue - tmpDeducted
                            End If

                            If Not (CInt(PayrollCostDistribution.AccruedValue) > 0 OrElse CInt(PayrollCostDistribution.DeductedValue) > 0) Then
                                If Not ((PayrollCostDistribution.InabilityCollect IsNot Nothing AndAlso PayrollCostDistribution.InabilityCollect > 0) OrElse (PayrollCostDistribution.SpendingInability IsNot Nothing AndAlso PayrollCostDistribution.SpendingInability > 0)) Then
                                    Continue For
                                End If
                            End If

                            DetailPayrollJournalVoucherDetail.IdThirdParty = ThirdPartyId
                            DetailPayrollJournalVoucherDetail.IdCostCenter = PayrollCostDistribution.CostCenterId
                            If PayrollCostDistribution.AccruedValue > 0 Then
                                Dim ObjAccount = _CostDistributionRepository.GetMainAccountByNumber(PayrollCostDistribution.AccruedAccount)
                                If ObjAccount IsNot Nothing Then
                                    DetailPayrollJournalVoucherDetail.IdMainAccount = ObjAccount.Id
                                Else
                                    ListMessageError.Add("La cuenta Contable " + PayrollCostDistribution.AccruedAccount + " no existe en el Plan de Cuentas o está inactiva, para el Concepto " + ObjConcept.Code + " - " + ObjConcept.Name + " del empleado " + PayrollCostDistribution.EmployeeNit + ". Favor Cambiarla")
                                End If
                            End If

                            If PayrollCostDistribution.DeductedValue > 0 Then
                                Dim ObjAccount = _CostDistributionRepository.GetMainAccountByNumber(PayrollCostDistribution.DeductedAccount)

                                If ObjAccount IsNot Nothing Then
                                    DetailPayrollJournalVoucherDetail.IdMainAccount = ObjAccount.Id
                                Else
                                    ListMessageError.Add("La cuenta Contable " + PayrollCostDistribution.DeductedAccount + " no existe en el Plan de Cuentas o está inactiva, para el Concepto " + ObjConcept.Code + " - " + ObjConcept.Name + " del empleado " + PayrollCostDistribution.EmployeeNit + ". Favor Cambiarla")
                                End If
                            End If

                            If PayrollCostDistribution.AccruedValue IsNot Nothing Then
                                DetailPayrollJournalVoucherDetail.DebitValue = PayrollCostDistribution.AccruedValue
                            End If

                            If PayrollCostDistribution.DeductedValue IsNot Nothing Then
                                DetailPayrollJournalVoucherDetail.CreditValue = PayrollCostDistribution.DeductedValue
                            End If

                            DetailPayrollJournalVoucherDetail.Detail = "Movimiento Contable de Nómina - Comprobante de Nómina"

                            ' Incapacidades
                            If PayrollCostDistribution.InabilityCollect IsNot Nothing And PayrollCostDistribution.SpendingInability IsNot Nothing Then
                                If PayrollCostDistribution.SpendingInability > 0 AndAlso PayrollCostDistribution.AccruedAccount IsNot Nothing Then
                                    'EMPLEADO
                                    ThirdPartyId = EmployeeThirdPartyId
                                    DetailPayrollJournalVoucherDetail.DebitValue = PayrollCostDistribution.SpendingInability
                                    DetailPayrollJournalVoucherDetail.CreditValue = 0
                                    DetailPayrollJournalVoucherDetail.IdMainAccount = _CostDistributionRepository.GetMainAccountByNumber(PayrollCostDistribution.AccruedAccount).Id
                                    DetailPayrollJournalVoucherDetail.IdThirdParty = ThirdPartyId
                                    DetailPayrollJournalVoucherDetail.IdCostCenter = PayrollCostDistribution.CostCenterId
                                    DetailPayrollJournalVoucherDetail.Detail = "Movimiento Contable de Nómina - Comprobante de Nómina"
                                End If
                                If PayrollCostDistribution.InabilityCollect > 0 AndAlso PayrollCostDistribution.AccruedAccount IsNot Nothing Then
                                    'EPS
                                    ThirdPartyId = _thirdPartyRepository.GetThirdPartyByNit(PayrollCostDistribution.CompanyNit).Id
                                    DetailPayrollJournalVoucherDetail.DebitValue = PayrollCostDistribution.InabilityCollect
                                    DetailPayrollJournalVoucherDetail.CreditValue = 0
                                    DetailPayrollJournalVoucherDetail.IdMainAccount = _CostDistributionRepository.GetMainAccountByNumber(PayrollCostDistribution.AccruedAccount).Id
                                    DetailPayrollJournalVoucherDetail.IdThirdParty = ThirdPartyId
                                    DetailPayrollJournalVoucherDetail.IdCostCenter = PayrollCostDistribution.CostCenterId
                                    DetailPayrollJournalVoucherDetail.Detail = "Movimiento Contable de Nómina - Comprobante de Nómina"
                                End If
                            End If

                            If DetailPayrollJournalVoucherDetail.IdMainAccount = 0 Then
                                ListMessageError.Add("El concepto " + _conceptRepository.GetConceptId(PayrollCostDistribution.ConceptId).Code + " No tiene parametrizada las cuentas contables")
                            End If

                            PayrollJournalVoucher.JournalVoucherDetails.Add(DetailPayrollJournalVoucherDetail)

                            '' Totalizo la Nómina
                            If PayrollCostDistribution.AccruedValue > 0 And PayrollCostDistribution.DeductedAccount = Group.PayrollParameter.PayrollAccount Then
                                TmpListPayrollCostDistribution.Add(PayrollCostDistribution)
                            End If

                            If PayrollCostDistribution.SpendingInability > 0 Then
                                TmpListPayrollCostDistribution.Add(PayrollCostDistribution)
                            End If
                        Next

                        If Group.PayrollParameter.AccountedBy = 1 Then
                            Dim ValueTotalCredit As Decimal = TmpListPayrollCostDistribution.Where(Function(y) y.EmployeeId = EmployeeId).ToList().Sum(Function(x) x.AccruedValue) + TmpListPayrollCostDistribution.Where(Function(y) y.EmployeeId = EmployeeId).ToList().Sum(Function(x) x.SpendingInability) - TotalDeducted

                            If ValueTotalCredit <> 0 Then
                                Dim TotalPayrollJournalVoucherDetail As New Domain.Entities.JournalVoucherDetails
                                If ValueTotalCredit > 0 Then
                                    TotalPayrollJournalVoucherDetail.DebitValue = 0
                                    TotalPayrollJournalVoucherDetail.CreditValue = ValueTotalCredit
                                Else
                                    TotalPayrollJournalVoucherDetail.DebitValue = Math.Abs(ValueTotalCredit)
                                    TotalPayrollJournalVoucherDetail.CreditValue = 0
                                End If
                                TotalPayrollJournalVoucherDetail.IdThirdParty = EmployeeThirdPartyId
                                TotalPayrollJournalVoucherDetail.IdMainAccount = _CostDistributionRepository.GetMainAccountByNumber(Group.PayrollParameter.PayrollAccount).Id
                                TotalPayrollJournalVoucherDetail.Detail = "Movimiento Contable de Nómina - Comprobante de Nómina"
                                PayrollJournalVoucher.JournalVoucherDetails.Add(TotalPayrollJournalVoucherDetail)
                            End If
                        End If
                    Next

                    If Group.PayrollParameter.AccountedBy = 2 Then
                        Dim TotalPayrollJournalVoucherDetail As New Domain.Entities.JournalVoucherDetails
                        TotalPayrollJournalVoucherDetail.DebitValue = 0
                        TotalPayrollJournalVoucherDetail.CreditValue = LiquidationConfirm.Sum(Function(x) x.TotalPaid)
                        TotalPayrollJournalVoucherDetail.IdThirdParty = Group.Company.ThirdPartyId
                        TotalPayrollJournalVoucherDetail.IdMainAccount = _CostDistributionRepository.GetMainAccountByNumber(Group.PayrollParameter.PayrollAccount).Id
                        TotalPayrollJournalVoucherDetail.Detail = "Movimiento Contable de Nómina - Comprobante de Nómina"
                        PayrollJournalVoucher.JournalVoucherDetails.Add(TotalPayrollJournalVoucherDetail)

                    End If

                    '' Registro la Cabecera
                    PayrollJournalVoucher.IdJournalVoucher = PayrollJournalVoucherTypeId
                    PayrollJournalVoucher.VoucherDate = VoucherDate
                    'PayrollJournalVoucher.VoucherDate = Date.Now()
                    PayrollJournalVoucher.Detail = "Comprobante Contable de Nómina - Grupo: " + Group.Code + " " + Group.Name + " - Nómina De " + ListPayrollCostDistribution.Item(0).PayrollDateLiquidated
                    PayrollJournalVoucher.EntityName = "PayrollLiquidation"
                    PayrollJournalVoucher.IsClosedYear = 0
                    PayrollJournalVoucher.CreationUser = indigo.UserIndigoName
                    PayrollJournalVoucher.CreationDate = Date.Now()
                    PayrollJournalVoucher.Status = 2

                    ListJournalVoucher.Add(PayrollJournalVoucher)
                End If

                'Genero el Comprobante Contable de Prestaciones
                If ListPrestacionCostDistribution IsNot Nothing And ListPrestacionCostDistribution.Count > 0 Then

                    Dim ObjPrestacionJournalVoucher = CreatePrestacionVoucher(ListPrestacionCostDistribution, Group, indigo, VoucherDate, PrestacionJournalVoucherTypeId)

                    If ObjPrestacionJournalVoucher.StateResult = False Then
                        ResultObjJournalVouchers.MessageResult = ObjPrestacionJournalVoucher.MessageResult
                        ResultObjJournalVouchers.StateResult = False
                        Return ResultObjJournalVouchers
                    End If

                    If ObjPrestacionJournalVoucher.StateResult = True Then
                        ListJournalVoucher.Add(ObjPrestacionJournalVoucher.ObjectEmbbeded)
                    End If

                End If

                'Genero el Comprobante Contable de Provisiones
                If ListProvisionCostDistribution IsNot Nothing And ListProvisionCostDistribution.Count > 0 Then

                    Dim ObjProvisionJournalVoucher = CreateProvisionVoucher(ListProvisionCostDistribution, Group, indigo, VoucherDate, ProvisionJournalVoucherTypeId, ListConcept)

                    If ObjProvisionJournalVoucher.StateResult = False Then
                        ResultObjJournalVouchers.MessageResult = ObjProvisionJournalVoucher.MessageResult
                        ResultObjJournalVouchers.StateResult = False
                        Return ResultObjJournalVouchers
                    End If

                    If ObjProvisionJournalVoucher.StateResult = True Then
                        ListJournalVoucher.Add(ObjProvisionJournalVoucher.ObjectEmbbeded)
                    End If
                End If

                If ListMessageError IsNot Nothing And ListMessageError.Count > 0 Then
                    ResultObjJournalVouchers.StateResult = False
                    ResultObjJournalVouchers.MessageResult = ListMessageError
                Else
                    ResultObjJournalVouchers.StateResult = True
                    ResultObjJournalVouchers.ObjectEmbbeded = ListJournalVoucher
                End If

                Return ResultObjJournalVouchers
            Else
                Return Nothing
            End If
        Catch ex As Exception
            ListMessageError.Add(ex.Message.ToString())
            Return New ActionResult(Of List(Of Domain.Entities.JournalVouchers)) With {.StateResult = False, .ObjectEmbbeded = Nothing, .MessageResult = ListMessageError}
            Return Nothing
        End Try

    End Function

    Public Function CreatePrestacionVoucher(ListPrestacionCostDistribution As List(Of CostDistributions), Group As Group, indigo As SessionValues, VoucherDate As Date, PrestacionJournalVoucherTypeId As Integer) As ActionResult(Of Domain.Entities.JournalVouchers)

        Dim ResultObjJournalVouchers As New ActionResult(Of Domain.Entities.JournalVouchers)

        Try

            Dim ListMessageError As New List(Of String)

            Dim PrestacionJournalVoucher As New Domain.Entities.JournalVouchers

            For Each PrestacionCostDistribution In ListPrestacionCostDistribution
                Dim DetailPrestacionJournalVoucherDetail As New Domain.Entities.JournalVoucherDetails
                Dim ThirdPartyId As Integer

                If PrestacionCostDistribution.AccruedValue > 0 And PrestacionCostDistribution.DeductedValue <= 0 Then
                    DetailPrestacionJournalVoucherDetail.DebitValue = PrestacionCostDistribution.AccruedValue
                    DetailPrestacionJournalVoucherDetail.CreditValue = 0
                    DetailPrestacionJournalVoucherDetail.IdMainAccount = _CostDistributionRepository.GetMainAccountByNumber(PrestacionCostDistribution.AccruedAccount).Id
                Else
                    DetailPrestacionJournalVoucherDetail.DebitValue = 0
                    DetailPrestacionJournalVoucherDetail.CreditValue = PrestacionCostDistribution.DeductedValue
                    DetailPrestacionJournalVoucherDetail.IdMainAccount = _CostDistributionRepository.GetMainAccountByNumber(PrestacionCostDistribution.DeductedAccount).Id
                End If

                ThirdPartyId = _thirdPartyRepository.GetThirdPartyByNit(PrestacionCostDistribution.CompanyNit).Id

                DetailPrestacionJournalVoucherDetail.IdThirdParty = ThirdPartyId
                DetailPrestacionJournalVoucherDetail.IdCostCenter = PrestacionCostDistribution.CostCenterId

                DetailPrestacionJournalVoucherDetail.Detail = "Movimiento Contable de Nómina - Comprobante de Prestaciones Sociales"

                PrestacionJournalVoucher.JournalVoucherDetails.Add(DetailPrestacionJournalVoucherDetail)

            Next

            PrestacionJournalVoucher.IdJournalVoucher = PrestacionJournalVoucherTypeId
            PrestacionJournalVoucher.VoucherDate = VoucherDate
            'PrestacionJournalVoucher.VoucherDate = Date.Now()
            PrestacionJournalVoucher.Detail = "Comprobante Contable de Nómina - Comprobante de Prestaciones - Grupo: " + Group.Code + " " + Group.Name + " - Nómina De " + ListPrestacionCostDistribution.Item(0).PayrollDateLiquidated
            PrestacionJournalVoucher.EntityName = "PayrollLiquidation"
            PrestacionJournalVoucher.IsClosedYear = 0
            PrestacionJournalVoucher.CreationUser = indigo.UserIndigoName
            PrestacionJournalVoucher.CreationDate = Date.Now
            PrestacionJournalVoucher.Status = 2

            ResultObjJournalVouchers.ObjectEmbbeded = PrestacionJournalVoucher
            ResultObjJournalVouchers.StateResult = True
        Catch ex As Exception
            ResultObjJournalVouchers.ObjectEmbbeded = Nothing
            ResultObjJournalVouchers.StateResult = False
            ResultObjJournalVouchers.MessageResult.Add(ex.Message.ToString())
        End Try

        Return ResultObjJournalVouchers
    End Function

    Public Function CreateProvisionVoucher(ListProvisionCostDistribution As List(Of CostDistributions), Group As Group, indigo As SessionValues, VoucherDate As Date, ProvisionJournalVoucherTypeId As Integer, ListConcept As List(Of Domain.Payroll.Entities.Concept)) As ActionResult(Of Domain.Entities.JournalVouchers)

        Dim ResultObjJournalVouchers As New ActionResult(Of Domain.Entities.JournalVouchers)

        Try

            Dim ListMessageError As New List(Of String)

            Dim ProvisionJournalVoucher As New Domain.Entities.JournalVouchers

            For Each ProvisionCostDistribution In ListProvisionCostDistribution
                Dim DetailProvisionJournalVoucherDetail As New Domain.Entities.JournalVoucherDetails
                Dim ThirdPartyId As Integer
                Dim ObjMainAccount As New Entities.MainAccounts

                If ProvisionCostDistribution.AccruedValue > 0 And ProvisionCostDistribution.DeductedValue <= 0 Then
                    DetailProvisionJournalVoucherDetail.DebitValue = ProvisionCostDistribution.AccruedValue
                    DetailProvisionJournalVoucherDetail.CreditValue = 0
                    ObjMainAccount = _CostDistributionRepository.GetMainAccountByNumber(ProvisionCostDistribution.AccruedAccount)

                    If ObjMainAccount.Id > 0 Then
                        DetailProvisionJournalVoucherDetail.IdMainAccount = ObjMainAccount.Id
                    Else
                        'Cargo el Concepto que se está revisando
                        Dim ObjConcept = ListConcept.Where(Function(x) x.Id = ProvisionCostDistribution.ConceptId).FirstOrDefault()
                        ListMessageError.Add("La cuenta Contable " + ProvisionCostDistribution.AccruedAccount + " no existe en el Plan de Cuentas o está inactiva, para el Concepto " + ProvisionCostDistribution.Concept.Code + " - " + ObjConcept.Name + " del empleado " + ProvisionCostDistribution.EmployeeNit + ". Favor Cambiarla")
                    End If
                Else
                    ObjMainAccount = _CostDistributionRepository.GetMainAccountByNumber(ProvisionCostDistribution.DeductedAccount)

                    If ObjMainAccount.Id > 0 Then
                        DetailProvisionJournalVoucherDetail.IdMainAccount = ObjMainAccount.Id
                    Else
                        'Cargo el Concepto que se está revisando
                        Dim ObjConcept = ListConcept.Where(Function(x) x.Id = ProvisionCostDistribution.ConceptId).FirstOrDefault()
                        ListMessageError.Add("La cuenta Contable " + ProvisionCostDistribution.DeductedAccount + " no existe en el Plan de Cuentas o está inactiva, para el Concepto " + ProvisionCostDistribution.Concept.Code + " - " + ObjConcept.Name + " del empleado " + ProvisionCostDistribution.EmployeeNit + ". Favor Cambiarla")
                    End If

                    DetailProvisionJournalVoucherDetail.DebitValue = 0
                    DetailProvisionJournalVoucherDetail.CreditValue = ProvisionCostDistribution.DeductedValue
                    DetailProvisionJournalVoucherDetail.IdMainAccount = _CostDistributionRepository.GetMainAccountByNumber(ProvisionCostDistribution.DeductedAccount).Id
                End If

                ThirdPartyId = _thirdPartyRepository.GetThirdPartyByNit(ProvisionCostDistribution.EmployeeNit).Id
                DetailProvisionJournalVoucherDetail.IdThirdParty = ThirdPartyId
                DetailProvisionJournalVoucherDetail.IdCostCenter = ProvisionCostDistribution.CostCenterId

                DetailProvisionJournalVoucherDetail.Detail = "Movimiento Contable de Nómina - Comprobante de Provisiones"

                ProvisionJournalVoucher.JournalVoucherDetails.Add(DetailProvisionJournalVoucherDetail)

            Next

            ProvisionJournalVoucher.IdJournalVoucher = ProvisionJournalVoucherTypeId
            ProvisionJournalVoucher.VoucherDate = VoucherDate
            'ProvisionJournalVoucher.VoucherDate = Date.Now()
            ProvisionJournalVoucher.Detail = "Comprobante Contable de Nómina - Comprobante de Provisiones - Grupo: " + Group.Code + " " + Group.Name + " - Nómina De " + ListProvisionCostDistribution.Item(0).PayrollDateLiquidated
            ProvisionJournalVoucher.EntityName = "PayrollLiquidation"
            ProvisionJournalVoucher.IsClosedYear = 0
            ProvisionJournalVoucher.CreationUser = indigo.UserIndigoName
            ProvisionJournalVoucher.CreationDate = Date.Now
            ProvisionJournalVoucher.Status = 2

            ResultObjJournalVouchers.ObjectEmbbeded = ProvisionJournalVoucher
            ResultObjJournalVouchers.StateResult = True

            If ListMessageError.Count > 0 Then
                ResultObjJournalVouchers.StateResult = False
                ResultObjJournalVouchers.ObjectEmbbeded = Nothing
                ResultObjJournalVouchers.MessageResult = ListMessageError
            End If
        Catch ex As Exception
            ResultObjJournalVouchers.ObjectEmbbeded = Nothing
            ResultObjJournalVouchers.StateResult = False
            ResultObjJournalVouchers.MessageResult.Add(ex.Message.ToString())
        End Try

        Return ResultObjJournalVouchers

    End Function

    Public Function CreateAccountReceivableDocument(ListCostDistributionInabilities As List(Of CostDistributions), Group As Group, indigo As SessionValues) As ActionResult(Of List(Of Domain.Entities.AccountReceivableDocument)) Implements ICostDistributionDomain.CreateAccountReceivableDocument

        Dim ResultObjJournalVouchers As New ActionResult(Of List(Of Domain.Entities.AccountReceivableDocument)) With {.MessageResult = New List(Of String)}

        Try

            Dim ListAccountReceivableDocument As New List(Of Domain.Entities.AccountReceivableDocument)
            Dim TmpDate As Date = ListCostDistributionInabilities.FirstOrDefault().PayrollDateLiquidated

            Dim ObjPayrollParameters = _payrollSettingsRepository.GetSettingPayroll()

            If ObjPayrollParameters Is Nothing Then
                ResultObjJournalVouchers.ObjectEmbbeded = Nothing
                ResultObjJournalVouchers.StateResult = False
                ResultObjJournalVouchers.MessageResult.Add("No se encontraron Parámetros de Nómina Definidos")
                Return ResultObjJournalVouchers
            End If

            If ObjPayrollParameters.IdAccountReceivableConcept Is Nothing Or ObjPayrollParameters.IdAccountReceivableConcept <= 0 Then
                ResultObjJournalVouchers.ObjectEmbbeded = Nothing
                ResultObjJournalVouchers.StateResult = False
                ResultObjJournalVouchers.MessageResult.Add("No se encontró parametrizado el Concepto de Cuentas por Cobrar que se parametriza en Parámetros de Nómina")
                Return ResultObjJournalVouchers
            End If

            If Group.PayrollParameter.IdPayrollAccount Is Nothing Then
                ResultObjJournalVouchers.ObjectEmbbeded = Nothing
                ResultObjJournalVouchers.StateResult = False
                ResultObjJournalVouchers.MessageResult.Add("No se ha parametrizado la Cuenta Nómina del Grupo " + String.Concat(Group.Code, " -", Group.Name))
                Return ResultObjJournalVouchers
            End If

            Dim ListMessageError As New List(Of String)

            Dim ListEps = ((From e In ListCostDistributionInabilities Select e.CompanyId).Distinct()).ToList

            For Each Eps As Integer In ListEps
                Dim AccountReceivableDocument As New Domain.Entities.AccountReceivableDocument
                Dim ObjCustomer = _customerRepository.GetCustomerByThirdPartyId(Eps)

                For Each CostDistribution As CostDistributions In ListCostDistributionInabilities

                    Dim AccountReceivableDocumentDetail As New Domain.Entities.AccountReceivableDocumentDetail

                    'Crédito
                    'Nómina
                    AccountReceivableDocumentDetail.AccountReceivableConceptId = ObjPayrollParameters.IdAccountReceivableConcept
                    AccountReceivableDocumentDetail.MainAccountId = Group.PayrollParameter.IdPayrollAccount
                    AccountReceivableDocumentDetail.ThirdPartyId = _thirdPartyRepository.GetThirdPartyByNit(CostDistribution.EmployeeNit).Id
                    AccountReceivableDocumentDetail.Nature = 2
                    AccountReceivableDocumentDetail.Value = CostDistribution.InabilityCollect

                    AccountReceivableDocument.AccountReceivableDocumentDetail.Add(AccountReceivableDocumentDetail)
                Next

                AccountReceivableDocument.OperatingUnitId = indigo.IndigoOperatingUnitId
                AccountReceivableDocument.DocumentDate = Date.Now
                AccountReceivableDocument.CustomerId = ObjCustomer.Id
                AccountReceivableDocument.MainAccountId = ObjCustomer.MainAccountReceivableId
                AccountReceivableDocument.InvoiceNumber = "Gr " + Group.Code + " - " + TmpDate.Date.ToShortDateString
                AccountReceivableDocument.Term = ObjCustomer.Term
                AccountReceivableDocument.ExpiredDate = DateAdd(DateInterval.Day, ObjCustomer.Term, Date.Now)
                AccountReceivableDocument.Share = 1
                AccountReceivableDocument.Value = AccountReceivableDocument.AccountReceivableDocumentDetail.Sum(Function(x) x.Value)
                AccountReceivableDocument.DebitValue = 0
                AccountReceivableDocument.CreditValue = AccountReceivableDocument.AccountReceivableDocumentDetail.Sum(Function(x) x.Value)
                AccountReceivableDocument.Observation = "Incapacidades por Cobrar Nómina del Grupo " + Group.Code + " - " + Group.Name + " - " + TmpDate.Date.ToShortDateString
                AccountReceivableDocument.Status = 2
                AccountReceivableDocument.CreationUser = indigo.UserIndigo
                AccountReceivableDocument.CreationDate = Date.Now

                ListAccountReceivableDocument.Add(AccountReceivableDocument)
            Next

            ResultObjJournalVouchers.ObjectEmbbeded = ListAccountReceivableDocument
            ResultObjJournalVouchers.StateResult = True
        Catch ex As Exception
            ResultObjJournalVouchers.ObjectEmbbeded = Nothing
            ResultObjJournalVouchers.StateResult = False
            ResultObjJournalVouchers.MessageResult.Add(ex.Message.ToString())
        End Try

        Return ResultObjJournalVouchers
    End Function

    Public Function CreateVoucherTransaction(ListLiquidationDetail As List(Of LiquidationDetail), payrollSettings As PayrollSettings, group As Group, indigo As SessionValues) As ActionResult(Of List(Of Domain.Entities.VoucherTransaction)) Implements ICostDistributionDomain.CreateVoucherTransaction

        Dim ResultObjJournalVouchers As New ActionResult(Of List(Of Domain.Entities.VoucherTransaction))

        Try
            Dim FlagInsert As Boolean = False
            Dim ListCompany = _companyRepository.ListAllCompany()
            ListCompany = ListCompany.Where(Function(x) x.AgreementType = True).ToList()

            Dim ListVoucherTransaction As New List(Of Domain.Entities.VoucherTransaction)

            Dim ListMessageError As New List(Of String)

            Dim PayrollDate = ListLiquidationDetail.FirstOrDefault().PayrollDate

            For Each ObjCompany As Domain.Payroll.Entities.Company In ListCompany
                Dim VoucherTransaction As New Domain.Entities.VoucherTransaction

                For Each ObjLiquidationDetail As LiquidationDetail In ListLiquidationDetail

                    Dim Employee = _employeeRepository.GetEmployeeById(ObjLiquidationDetail.Liquidation.EmployeeId)
                    Dim ObjAgreements = _IAgreementsCRepository.GetAgreementsById(ObjLiquidationDetail.AgreementsId)
                    Dim ObjExpenseConcepts = _expenseConceptRepository.GetExpenseConceptById(ObjAgreements.KindsAgreements.IdExpenseConcepts)

                    If ObjAgreements.CompanyId = ObjCompany.Id Then
                        FlagInsert = True
                        Dim VoucherTransactionDetail As New Domain.Entities.VoucherTransactionDetails
                        'Débito
                        VoucherTransactionDetail.IdThirdParty = Employee.ThirdPartyId
                        VoucherTransactionDetail.IdExpenseConcept = ObjAgreements.KindsAgreements.IdExpenseConcepts
                        VoucherTransactionDetail.IdMainAccount = ObjExpenseConcepts.IdMainAccount
                        VoucherTransactionDetail.Nature = 1
                        VoucherTransactionDetail.Value = ObjLiquidationDetail.ConceptTotalValue
                        VoucherTransactionDetail.PercentRetention = 0
                        VoucherTransactionDetail.Detail = "Pago del Convenio del Empleado " + Employee.ThirdParty.Nit + " - " + Employee.ThirdParty.Name
                        VoucherTransaction.VoucherTransactionDetails.Add(VoucherTransactionDetail)
                    End If
                Next

                VoucherTransaction.Code = ""
                VoucherTransaction.IdThirdParty = ObjCompany.ThirdPartyId
                VoucherTransaction.VoucherClass = 1
                VoucherTransaction.ExpenseType = payrollSettings.ExpenseType
                VoucherTransaction.Detail = "CONVENIOS NÓMINA DEL MES DE " + PayrollDate + " del Grupo " + group.Code + " - " + group.Name
                VoucherTransaction.DocumentDate = Date.Now()

                If payrollSettings.ExpenseType = 1 Then
                    Dim ObjEntityBankAccount = _entityBankAccount.GetEntityBankAccountById(payrollSettings.IdEntityBankAccount)
                    VoucherTransaction.IdEntityBankAccount = payrollSettings.IdEntityBankAccount
                    VoucherTransaction.IdMainAccount = ObjEntityBankAccount.IdMainAccount
                    VoucherTransaction.PaymentMethod = payrollSettings.PaymentMethod
                    VoucherTransaction.NoteNumber = 1
                    VoucherTransaction.BankAccountNumber = ObjEntityBankAccount.Number
                    VoucherTransaction.BankName = ObjEntityBankAccount.Bank.Name
                Else
                    VoucherTransaction.IdCashRegister = payrollSettings.IdCashRegister
                    VoucherTransaction.IdMainAccount = _cashRegisterRepository.GetCashRegisterById(payrollSettings.IdCashRegister).IdMainAccount
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
                VoucherTransaction.CreationDate = Date.Now()
                VoucherTransaction.EmailSent = 0

                If VoucherTransaction.VoucherTransactionDetails.Count > 0 Then
                    ListVoucherTransaction.Add(VoucherTransaction)
                End If

            Next

            ResultObjJournalVouchers.ObjectEmbbeded = ListVoucherTransaction
            ResultObjJournalVouchers.StateResult = True
        Catch ex As Exception
            ResultObjJournalVouchers.ObjectEmbbeded = Nothing
            ResultObjJournalVouchers.StateResult = False
            ResultObjJournalVouchers.MessageResult.Add(ex.Message.ToString())
        End Try

        Return ResultObjJournalVouchers
    End Function

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _IConceptAccountingStructureRepository = Nothing
            _IAgreementsCRepository = Nothing
            _FundsRepository = Nothing
            _thirdPartyRepository = Nothing
            _CostDistributionRepository = Nothing
            _GroupRepository = Nothing
            _costCenterRepository = Nothing
            _conceptRepository = Nothing
            _tradeUnionRepository = Nothing
            _payrollSettingsRepository = Nothing
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