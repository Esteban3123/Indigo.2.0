'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Common.Entities
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports System.IO

Public Class NationalSavingsFundDomain

    Implements INationalSavingsFundDomain

    ''' <summary>
    ''' Repositorio de Primas
    ''' </summary>
    ''' <remarks></remarks>
    Private _incentivePaymentRepository As IIncentivePaymentRepository

    Private _retroactiveRepository As IRetroactiveCRepository

    Private _conceptRepository As IConceptRepository

    Private _contractLiquidationRepository As IContractLiquidationRepository

    Private _payrollLiquidationRepository As IPayrollLiquidationRepository


    Public Sub New(incentivePaymentRepository As IIncentivePaymentRepository, retroactiveRepository As IRetroactiveCRepository, conceptRepository As IConceptRepository, contractLiquidationRepository As IContractLiquidationRepository,
                   payrollLiquidationRepository As IPayrollLiquidationRepository)
        _incentivePaymentRepository = incentivePaymentRepository
        _retroactiveRepository = retroactiveRepository
        _conceptRepository = conceptRepository
        _contractLiquidationRepository = contractLiquidationRepository
        _payrollLiquidationRepository = payrollLiquidationRepository
    End Sub

    Public Function GenerateArchive(PayrollLiquidation As List(Of Liquidation), PayrollLiquidationAcumulated As List(Of Liquidation)) As StringBuilder Implements INationalSavingsFundDomain.GenerateArchive
        Dim result As New StringBuilder()
        Dim DateTransaction = Date.Now
        Dim DateNow As String = DateTransaction.ToString("yyyyMMdd")
        Dim TotalPaid As Double = 0
        Dim PayrollDate = PayrollLiquidation.FirstOrDefault().PayrollDateLiquidated

        Dim CompanyData As String = PayrollLiquidation.Item(0).Group.Company.ThirdParty.Nit & PayrollLiquidation.Item(0).Group.Company.ThirdParty.DigitVerification

        Dim ListContractLiquidation = _contractLiquidationRepository.GetContractLiquidationByMonthAndYear(PayrollDate.Month, PayrollDate.Year)

        If ListContractLiquidation IsNot Nothing AndAlso ListContractLiquidation.Count > 0 Then
            ListContractLiquidation.Where(Function(x) x.Contract.FundContract.Any(Function(y) y.FundType = "3" And y.Fund.ThirdParty.Nit = "899999284")).ToList()
        End If

        If PayrollLiquidation IsNot Nothing AndAlso PayrollLiquidation.Count > 0 Then

            For Each LiquidationEmployee As Liquidation In PayrollLiquidation

                Dim DocumentType As String

                If LiquidationEmployee.Employee.ThirdParty.Person.IdentificationType = 0 Then
                    DocumentType = "CC"
                ElseIf LiquidationEmployee.Employee.ThirdParty.Person.IdentificationType = 1 Then
                    DocumentType = "CE"
                ElseIf LiquidationEmployee.Employee.ThirdParty.Person.IdentificationType = 7 Then
                    DocumentType = "N"
                ElseIf LiquidationEmployee.Employee.ThirdParty.Person.IdentificationType = 2 Then
                    DocumentType = "TI"
                End If

                Dim UnemployementInteresValueList = LiquidationEmployee.LiquidationDetail.Where(Function(x) x.ConceptClass = "034").ToList()

                Dim UnemployementInterestValue As Double = 0
                If UnemployementInteresValueList.Count() > 0 Then
                    For Each unemployeValueItem As LiquidationDetail In UnemployementInteresValueList
                        UnemployementInterestValue = UnemployementInterestValue + unemployeValueItem.ConceptTotalValue
                    Next
                Else
                    UnemployementInterestValue = 0
                End If

                Dim UnemployementValueList = LiquidationEmployee.LiquidationDetail.Where(Function(x) x.ConceptClass = "008").ToList()
                Dim UnemployementValue As Double = 0
                If UnemployementValueList.Count() > 0 Then
                    For Each unemployeValueItem As LiquidationDetail In UnemployementValueList
                        UnemployementValue = UnemployementValue + unemployeValueItem.ConceptTotalValue
                    Next
                Else
                    UnemployementValue = 0
                End If

                Dim LiquidationEmployeeAcumulated = PayrollLiquidationAcumulated.Where(Function(x) x.EmployeeId = LiquidationEmployee.EmployeeId).ToList()

                Dim UnemployementLiquidationObj As List(Of LiquidationDetail)
                Dim UnemploymentValueAcumulated As Double = 0
                For Each UnemployementAcumulated As Liquidation In LiquidationEmployeeAcumulated
                    UnemployementLiquidationObj = UnemployementAcumulated.LiquidationDetail.Where(Function(x) x.ConceptClass = "008").ToList()
                    If UnemployementLiquidationObj.Count() > 0 Then
                        For Each unemployeAcumulatedValueItem As LiquidationDetail In UnemployementLiquidationObj
                            UnemploymentValueAcumulated = UnemploymentValueAcumulated + unemployeAcumulatedValueItem.ConceptTotalValue
                        Next
                    End If
                Next

                'Cargo el listado de retroactivos
                Dim ListRetroactive = _retroactiveRepository.GetListRetroactiveByYear(Year(PayrollDate), LiquidationEmployee.GroupId)

                Dim ObjRetroactiveEmployee As RetroactiveC

                If ListRetroactive IsNot Nothing AndAlso ListRetroactive.Count() > 0 Then
                    ObjRetroactiveEmployee = ListRetroactive.Where(Function(x) x.IdEmployee = LiquidationEmployee.EmployeeId).FirstOrDefault()
                End If



                'Cargo los Datos de Primas de Servicios
                Dim ProvisionValueIncentiveServices As Double = 0
                Dim ProvisionValueIncentiveDecember As Double = 0
                Dim ListPaidValueAverageIncentive = _incentivePaymentRepository.GetIncentivePaymentByEmployeeIdLastLiquidation(LiquidationEmployee.EmployeeId)

                If ListPaidValueAverageIncentive IsNot Nothing AndAlso ListPaidValueAverageIncentive.Count > 0 Then

                    Dim ObjPaidValueAverageIncentiveServices = ListPaidValueAverageIncentive.Where(Function(x) x.Period = 1 And Month(x.PeriodEndDate) = Month(PayrollDate) And Year(x.PeriodEndDate) = Year(PayrollDate)).FirstOrDefault()

                    If ObjPaidValueAverageIncentiveServices IsNot Nothing Then

                        Dim ListStringConceptClass As New List(Of String)
                        ListStringConceptClass.Add("008")
                        Dim Concept = _conceptRepository.GetConceptByConceptClass(ListStringConceptClass).FirstOrDefault()

                        Dim ObjIncentivePaymentDetail = ObjPaidValueAverageIncentiveServices.IncentivePaymentDetail.Where(Function(x) x.ConceptId = Concept.Id).FirstOrDefault()
                        If ObjIncentivePaymentDetail IsNot Nothing Then
                            ProvisionValueIncentiveServices = ObjIncentivePaymentDetail.AccruedValue
                        End If

                        If ObjRetroactiveEmployee IsNot Nothing Then

                            If Concept IsNot Nothing Then
                                Dim ObjConceptRetroactive = ObjRetroactiveEmployee.RetroactiveD.Where(Function(x) x.IdConcept = Concept.Id).FirstOrDefault()
                                If ObjConceptRetroactive IsNot Nothing Then
                                    ProvisionValueIncentiveServices = ObjConceptRetroactive.ValueConceptWithRetroactive + ProvisionValueIncentiveServices
                                End If
                            End If
                        End If

                    End If

                    'Cargo los datos de Primas de Diciembre
                    Dim ObjPaidValueAverageIncentiveDecember = ListPaidValueAverageIncentive.Where(Function(x) x.Period = 2 And Month(x.PeriodEndDate) = Month(PayrollDate) And Year(x.PeriodEndDate) = Year(PayrollDate)).FirstOrDefault()

                    If ObjPaidValueAverageIncentiveDecember IsNot Nothing Then

                        Dim ListStringConceptClass As New List(Of String)
                        ListStringConceptClass.Add("008")
                        Dim Concept = _conceptRepository.GetConceptByConceptClass(ListStringConceptClass).FirstOrDefault()

                        Dim ObjIncentivePaymentDetail = ObjPaidValueAverageIncentiveDecember.IncentivePaymentDetail.Where(Function(x) x.ConceptId = Concept.Id).FirstOrDefault()
                        If ObjIncentivePaymentDetail IsNot Nothing Then
                            ProvisionValueIncentiveDecember = ObjIncentivePaymentDetail.AccruedValue
                        End If

                        If ObjRetroactiveEmployee IsNot Nothing Then

                            If Concept IsNot Nothing Then
                                Dim ObjConceptRetroactive = ObjRetroactiveEmployee.RetroactiveD.Where(Function(x) x.IdConcept = Concept.Id).FirstOrDefault()
                                If ObjConceptRetroactive IsNot Nothing Then
                                    ProvisionValueIncentiveDecember = ObjConceptRetroactive.ValueConceptWithRetroactive + ProvisionValueIncentiveDecember
                                End If
                            End If
                        End If

                    End If
                End If

                'Calculo el IBC de las Cesantias
                Dim IBCUnemployement As Double = (LiquidationEmployee.UnemploymentAccumulated / 0.0833)
                IBCUnemployement = Math.Round(IBCUnemployement)


                Dim lineHead As String = ""
                If PayrollLiquidation.IndexOf(LiquidationEmployee) > 0 Then
                    lineHead = vbCrLf
                End If

                Dim PosesionDate As Date?
                Dim RetirementDate As Date?

                If Month(LiquidationEmployee.Contract.JobBondingDate) = Month(LiquidationEmployee.PayrollDateLiquidated) And Year(LiquidationEmployee.Contract.JobBondingDate) = Year(LiquidationEmployee.PayrollDateLiquidated) Then
                    PosesionDate = LiquidationEmployee.Contract.JobBondingDate
                Else
                    PosesionDate = Nothing
                End If

                RetirementDate = Nothing
                If LiquidationEmployee.Contract.RetirementDate IsNot Nothing Then
                    If Month(LiquidationEmployee.Contract.RetirementDate) = Month(LiquidationEmployee.PayrollDateLiquidated) And Year(LiquidationEmployee.Contract.RetirementDate) = Year(LiquidationEmployee.PayrollDateLiquidated) Then
                        RetirementDate = LiquidationEmployee.Contract.RetirementDate
                    Else
                        RetirementDate = Nothing
                    End If
                End If

                lineHead &= Utils.StringPad(CompanyData, 14, 0, Utils.PadType.STR_PAD_LEFT) '1
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(LiquidationEmployee.Employee.ThirdParty.Nit, 11, 0, Utils.PadType.STR_PAD_LEFT) '2
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(DocumentType, 2, " ", Utils.PadType.STR_PAD_RIGHT) '3
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(LiquidationEmployee.Employee.ThirdParty.Person.FirstLastName, 25, " ", Utils.PadType.STR_PAD_RIGHT) '4
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(IIf(LiquidationEmployee.Employee.ThirdParty.Person.SecondLastName = String.Empty, " ", LiquidationEmployee.Employee.ThirdParty.Person.SecondLastName), 25, " ", Utils.PadType.STR_PAD_RIGHT) '5
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(LiquidationEmployee.Employee.ThirdParty.Person.FirstName + " " + LiquidationEmployee.Employee.ThirdParty.Person.SecondName, 64, " ", Utils.PadType.STR_PAD_RIGHT) '6
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(LiquidationEmployee.Contract.FunctionalUnit.BranchOffice.City.Department.Code, 2, " ", Utils.PadType.STR_PAD_RIGHT) '7
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(LiquidationEmployee.Contract.FunctionalUnit.BranchOffice.City.Code.Substring(2, 3), 3, 0, Utils.PadType.STR_PAD_LEFT) '8
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(LiquidationEmployee.Contract.BasicSalary, 12, 0, Utils.PadType.STR_PAD_LEFT) '9
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(IBCUnemployement, 12, 0, Utils.PadType.STR_PAD_LEFT) '10
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(LiquidationEmployee.UnemploymentAccumulated + ProvisionValueIncentiveDecember + ProvisionValueIncentiveServices, 12, 0, Utils.PadType.STR_PAD_LEFT) '11
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(Year(LiquidationEmployee.PayrollDateLiquidated), 4, " ", Utils.PadType.STR_PAD_LEFT) '12
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(Month(LiquidationEmployee.PayrollDateLiquidated), 2, 0, Utils.PadType.STR_PAD_LEFT) '13
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(IIf(PosesionDate IsNot Nothing, PosesionDate, " "), 10, " ", Utils.PadType.STR_PAD_LEFT) '14
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(IIf(RetirementDate IsNot Nothing, RetirementDate, " "), 10, " ", Utils.PadType.STR_PAD_LEFT) '15
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(1, 1, " ", Utils.PadType.STR_PAD_LEFT) '16
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(UnemploymentValueAcumulated, 12, 0, Utils.PadType.STR_PAD_LEFT) '17
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(0, 12, 0, Utils.PadType.STR_PAD_LEFT) '18
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineHead &= Utils.StringPad(0, 12, 0, Utils.PadType.STR_PAD_LEFT) '19
                lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                result.Append(lineHead)
            Next

        End If

        Dim lineContractLiquidation As String = ""

        If ListContractLiquidation IsNot Nothing AndAlso ListContractLiquidation.Count > 0 Then
            lineContractLiquidation = vbCrLf
            result.Append(vbCrLf)

            For Each ObjContractLiquidation As ContractLiquidation In ListContractLiquidation


                lineContractLiquidation = ""

                Dim DocumentType As String = ""

                Dim InitialDate As New Date(Year(ObjContractLiquidation.RetirementDate), 1, 1)

                'Obtengo la información de liquidaciones del último año del empleado
                Dim ListLiquidation = _payrollLiquidationRepository.LiquidationEmployeeByDate(ObjContractLiquidation.EmployeeId, InitialDate, ObjContractLiquidation.RetirementDate)

                'Declaro las variable  que utilizaré
                Dim IBCUnemployement As Double = 0
                Dim UnemploymentAccumulated As Double = 0
                Dim ProvisionValueIncentiveDecember As Double = 0
                Dim ProvisionValueIncentiveServices As Double = 0
                Dim UnemploymentValueAcumulated As Double = 0


                If ListLiquidation IsNot Nothing AndAlso ListLiquidation.Count() > 0 Then
                    UnemploymentAccumulated = ListLiquidation.Sum(Function(x) x.UnemploymentAccumulated)

                    IBCUnemployement = (UnemploymentAccumulated / 0.0833)
                    IBCUnemployement = Math.Round(IBCUnemployement)

                End If

                'Cargo el listado de retroactivos
                Dim ListRetroactive = _retroactiveRepository.GetListRetroactiveByYear(Year(PayrollDate), ObjContractLiquidation.Contract.GroupId)

                Dim ObjRetroactiveEmployee As RetroactiveC

                If ListRetroactive IsNot Nothing AndAlso ListRetroactive.Count() > 0 Then
                    ObjRetroactiveEmployee = ListRetroactive.Where(Function(x) x.IdEmployee = ObjContractLiquidation.EmployeeId).FirstOrDefault()
                End If


                Dim ListPaidValueAverageIncentive = _incentivePaymentRepository.GetIncentivePaymentByEmployeeIdLastLiquidation(ObjContractLiquidation.EmployeeId)

                If ListPaidValueAverageIncentive IsNot Nothing AndAlso ListPaidValueAverageIncentive.Count > 0 Then

                    Dim ObjPaidValueAverageIncentiveServices = ListPaidValueAverageIncentive.Where(Function(x) x.Period = 1 And Month(x.PeriodEndDate) = Month(PayrollDate) And Year(x.PeriodEndDate) = Year(PayrollDate)).FirstOrDefault()

                    If ObjPaidValueAverageIncentiveServices IsNot Nothing Then

                        Dim ListStringConceptClass As New List(Of String)
                        ListStringConceptClass.Add("008")
                        Dim Concept = _conceptRepository.GetConceptByConceptClass(ListStringConceptClass).FirstOrDefault()

                        Dim ObjIncentivePaymentDetail = ObjPaidValueAverageIncentiveServices.IncentivePaymentDetail.Where(Function(x) x.ConceptId = Concept.Id).FirstOrDefault()
                        If ObjIncentivePaymentDetail IsNot Nothing Then
                            ProvisionValueIncentiveServices = ObjIncentivePaymentDetail.AccruedValue
                        End If

                        If ObjRetroactiveEmployee IsNot Nothing Then

                            If Concept IsNot Nothing Then
                                Dim ObjConceptRetroactive = ObjRetroactiveEmployee.RetroactiveD.Where(Function(x) x.IdConcept = Concept.Id).FirstOrDefault()
                                If ObjConceptRetroactive IsNot Nothing Then
                                    ProvisionValueIncentiveServices = ObjConceptRetroactive.ValueConceptWithRetroactive + ProvisionValueIncentiveServices
                                End If
                            End If
                        End If

                    End If

                    'Cargo los datos de Primas de Diciembre
                    Dim ObjPaidValueAverageIncentiveDecember = ListPaidValueAverageIncentive.Where(Function(x) x.Period = 2 And Month(x.PeriodEndDate) = Month(PayrollDate) And Year(x.PeriodEndDate) = Year(PayrollDate)).FirstOrDefault()

                    If ObjPaidValueAverageIncentiveDecember IsNot Nothing Then

                        Dim ListStringConceptClass As New List(Of String)
                        ListStringConceptClass.Add("008")
                        Dim Concept = _conceptRepository.GetConceptByConceptClass(ListStringConceptClass).FirstOrDefault()

                        Dim ObjIncentivePaymentDetail = ObjPaidValueAverageIncentiveDecember.IncentivePaymentDetail.Where(Function(x) x.ConceptId = Concept.Id).FirstOrDefault()
                        If ObjIncentivePaymentDetail IsNot Nothing Then
                            ProvisionValueIncentiveDecember = ObjIncentivePaymentDetail.AccruedValue
                        End If

                        If ObjRetroactiveEmployee IsNot Nothing Then

                            If Concept IsNot Nothing Then
                                Dim ObjConceptRetroactive = ObjRetroactiveEmployee.RetroactiveD.Where(Function(x) x.IdConcept = Concept.Id).FirstOrDefault()
                                If ObjConceptRetroactive IsNot Nothing Then
                                    ProvisionValueIncentiveDecember = ObjConceptRetroactive.ValueConceptWithRetroactive + ProvisionValueIncentiveDecember
                                End If
                            End If
                        End If

                    End If
                End If


                If ObjContractLiquidation.Employee.ThirdParty.Person.IdentificationType = 0 Then
                    DocumentType = "CC"
                ElseIf ObjContractLiquidation.Employee.ThirdParty.Person.IdentificationType = 1 Then
                    DocumentType = "CE"
                ElseIf ObjContractLiquidation.Employee.ThirdParty.Person.IdentificationType = 7 Then
                    DocumentType = "N"
                ElseIf ObjContractLiquidation.Employee.ThirdParty.Person.IdentificationType = 2 Then
                    DocumentType = "TI"
                End If

                Dim PosesionDate As Date?

                If Month(ObjContractLiquidation.Contract.JobBondingDate) = Month(ObjContractLiquidation.RetirementDate) And Year(ObjContractLiquidation.Contract.JobBondingDate) = Year(ObjContractLiquidation.RetirementDate) Then
                    PosesionDate = ObjContractLiquidation.Contract.JobBondingDate
                Else
                    PosesionDate = Nothing
                End If


                lineContractLiquidation &= Utils.StringPad(CompanyData, 14, 0, Utils.PadType.STR_PAD_LEFT) '1
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(ObjContractLiquidation.Employee.ThirdParty.Nit, 11, 0, Utils.PadType.STR_PAD_LEFT) '2
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(DocumentType, 2, " ", Utils.PadType.STR_PAD_RIGHT) '3
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(ObjContractLiquidation.Employee.ThirdParty.Person.FirstLastName, 25, " ", Utils.PadType.STR_PAD_RIGHT) '4
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(IIf(ObjContractLiquidation.Employee.ThirdParty.Person.SecondLastName = String.Empty, " ", ObjContractLiquidation.Employee.ThirdParty.Person.SecondLastName), 25, " ", Utils.PadType.STR_PAD_RIGHT) '5
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(ObjContractLiquidation.Employee.ThirdParty.Person.FirstName + " " + ObjContractLiquidation.Employee.ThirdParty.Person.SecondName, 64, " ", Utils.PadType.STR_PAD_RIGHT) '6
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(ObjContractLiquidation.Contract.FunctionalUnit.BranchOffice.City.Department.Code, 2, " ", Utils.PadType.STR_PAD_RIGHT) '7
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(ObjContractLiquidation.Contract.FunctionalUnit.BranchOffice.City.Code.Substring(2, 3), 3, 0, Utils.PadType.STR_PAD_LEFT) '8
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(ObjContractLiquidation.Contract.BasicSalary, 12, 0, Utils.PadType.STR_PAD_LEFT) '9
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(IBCUnemployement, 12, 0, Utils.PadType.STR_PAD_LEFT) '10
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(UnemploymentAccumulated + ProvisionValueIncentiveDecember + ProvisionValueIncentiveServices, 12, 0, Utils.PadType.STR_PAD_LEFT) '11
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(Year(ObjContractLiquidation.RetirementDate), 4, " ", Utils.PadType.STR_PAD_LEFT) '12
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(Month(ObjContractLiquidation.RetirementDate), 2, 0, Utils.PadType.STR_PAD_LEFT) '13
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(IIf(PosesionDate IsNot Nothing, PosesionDate, " "), 10, " ", Utils.PadType.STR_PAD_LEFT) '14
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(ObjContractLiquidation.RetirementDate, 10, " ", Utils.PadType.STR_PAD_LEFT) '15
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(1, 1, " ", Utils.PadType.STR_PAD_LEFT) '16
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(UnemploymentValueAcumulated, 12, 0, Utils.PadType.STR_PAD_LEFT) '17
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(0, 12, 0, Utils.PadType.STR_PAD_LEFT) '18
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineContractLiquidation &= Utils.StringPad(0, 12, 0, Utils.PadType.STR_PAD_LEFT) '19
                lineContractLiquidation &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)

                result.Append(lineContractLiquidation)
            Next

        End If


        Return result
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _incentivePaymentRepository = Nothing
            _retroactiveRepository = Nothing
            _conceptRepository = Nothing
            _contractLiquidationRepository = Nothing
            _payrollLiquidationRepository = Nothing
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
