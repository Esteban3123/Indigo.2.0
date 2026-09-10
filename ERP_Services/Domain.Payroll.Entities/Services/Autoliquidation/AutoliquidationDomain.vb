Imports Domain.Base.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Base

Public Class AutoliquidationDomain
    Implements IAutoliquidationDomain


    Public Function GenerateArchive(company As Company, workCenter As WorkCenter, periodLiquidation As String, isCorrection As Boolean, _
                                    dateLiquidation As Nullable(Of Date), numberTemplate As String, listLiquidation As List(Of Liquidation)) As ActionMessageResult(Of Text.StringBuilder) Implements IAutoliquidationDomain.GenerateArchive

        Dim resultActionMessage As New ActionMessageResult(Of StringBuilder)()
        Try

            resultActionMessage.StateResult = False
            Dim result As New StringBuilder()
            ' Dim company As Company = _companyRepository.GetCompanyById(companyId)
            'Dim workCenter As WorkCenter = _workCenterRepository.GetWorkCenterById(workCenterId)
            Dim periodDateLiquidation As Date = New Date(periodLiquidation.Substring(0, 4), periodLiquidation.Substring(5, 2), 1)
            Dim PeriodDateLiquidationPlane As String = periodDateLiquidation.ToString("yyyy-MM")
            Dim periodHealth As String = periodDateLiquidation.AddMonths(1).ToString("yyyy-MM")
            'Dim listLiquidation = _liquidationRepository.GetLiquidationByPeriod(periodLiquidation)

            Dim listOnlyEmployeeLiquidation = (From e In listLiquidation
                                               Select e.EmployeeId).Distinct().ToList()
            Dim correction As String = " "
            Dim StrDateLiquidation As String = ""
            Dim presentation As String
            Dim codeWorkCenter As String = ""
            Dim nameWorkCenter As String = ""
            Dim numberRadication As String = ""
            Dim PayrollDateLiquidated As Date = listLiquidation.FirstOrDefault().PayrollDateLiquidated
            Dim totalEmployees As Integer = listLiquidation.Count()
            Dim totalValuePayroll As Decimal = (From a In listLiquidation Select a.TotalPaid).Sum()
            Dim typeContributor As Integer = 1
            Dim codeOperator As String = ""
            Dim codeARP As String = company.Fund.MinistryCode

            Dim PayrollStarDate = Me.GetStarPayrollDate(PayrollDateLiquidated)

            If workCenter Is Nothing OrElse workCenter.Id = 0 Then
                presentation = "C"
            Else
                presentation = "S"
                codeWorkCenter = workCenter.Code
                nameWorkCenter = workCenter.Name
            End If
            If isCorrection = True Then
                correction = "X"
                If dateLiquidation IsNot Nothing Then
                    StrDateLiquidation = dateLiquidation.ToString("yyyy-MM-dd")
                End If
            End If
            Dim sequenceHead As Integer = 1
            'Comformo la linea de la cabecera
            'Dim lineHead As String = "01" & Utils.StringPad(sequenceHead, 5, 0, Utils.PadType.STR_PAD_LEFT)
            'lineHead &= Utils.StringPad(company.Name.Trim(), 200, " ", Utils.PadType.STR_PAD_RIGHT) & "NI"
            'lineHead &= Left(Utils.StringPad(company.ThirdParty.Nit.Trim(), 16, " ", Utils.PadType.STR_PAD_RIGHT), 16)
            'lineHead &= Calculate_VerificationCode(company.ThirdParty.Nit.Trim()) & correction & Left(Utils.StringPad(numberTemplate.Trim(), 10, " "), 10)
            'lineHead &= Utils.StringPad(StrDateLiquidation, 10, " ") & presentation
            'lineHead &= Utils.StringPad(codeWorkCenter, 10, " ") & Utils.StringPad(nameWorkCenter, 40, " ")
            'lineHead &= Utils.StringPad(codeARP, 6, " ") & periodLiquidation & periodHealth
            'lineHead &= Utils.StringPad(numberRadication, 10, 0) & Date.Now().ToString("yyyy-MM-dd")
            'lineHead &= Utils.StringPad(totalEmployees, 5, 0, Utils.PadType.STR_PAD_LEFT)
            'lineHead &= Utils.StringPad(totalValuePayroll, 12, 0, Utils.PadType.STR_PAD_LEFT)
            'lineHead &= typeContributor & Utils.StringPad(codeOperator, 2, 0)
            'result.Append(lineHead

            Dim lineHead As String = "01" & Utils.StringPad(sequenceHead, 5, 0, Utils.PadType.STR_PAD_LEFT) '01
            lineHead &= Utils.StringPad(company.Name.Trim(), 200, " ", Utils.PadType.STR_PAD_RIGHT) & "NI" '02
            lineHead &= Left(Utils.StringPad(company.ThirdParty.Nit.Trim(), 16, " ", Utils.PadType.STR_PAD_RIGHT), 16) '03
            lineHead &= Utils.StringPad(Calculate_VerificationCode(company.ThirdParty.Nit.Trim()), 1, " ", Utils.PadType.STR_PAD_LEFT) '04
            lineHead &= Utils.StringPad("T", 1, " ", Utils.PadType.STR_PAD_RIGHT) '05
            lineHead &= Utils.StringPad(" ", 20, " ", Utils.PadType.STR_PAD_LEFT) '06
            lineHead &= Utils.StringPad("U", 1, " ", Utils.PadType.STR_PAD_RIGHT) '07
            lineHead &= Utils.StringPad(" ", 50, " ", Utils.PadType.STR_PAD_LEFT) '08
            lineHead &= Utils.StringPad(codeARP, 6, " ", Utils.PadType.STR_PAD_LEFT) '09
            lineHead &= Utils.StringPad(PeriodDateLiquidationPlane, 7, " ", Utils.PadType.STR_PAD_LEFT) '10
            lineHead &= Utils.StringPad(periodHealth, 7, " ", Utils.PadType.STR_PAD_LEFT) '11
            lineHead &= Utils.StringPad(" ", 20, " ", Utils.PadType.STR_PAD_RIGHT) '12
            lineHead &= Utils.StringPad(totalEmployees, 5, 0, Utils.PadType.STR_PAD_LEFT) '13
            lineHead &= Utils.StringPad(totalValuePayroll, 11, 0, Utils.PadType.STR_PAD_LEFT) '14
            lineHead &= Utils.StringPad("31860", 4, " ", Utils.PadType.STR_PAD_LEFT) '15
            result.Append(lineHead)

            Dim sequenceDetail As Integer = 0
            For Each idEmployee As Integer In listOnlyEmployeeLiquidation
                Dim typeDocument As String = ""
                Dim typeContractEmployee As String = ""
                Dim subTypeEmployee As String = ""
                Dim foreignNotBound As String = " "
                Dim colombianForeignResident As String = " "
                Dim entry As String = " "
                Dim retirement As String = " "
                Dim TDE As String = " " 'Traslado desde otra EPS  :(
                Dim TAE As String = " " 'Traslado a otra EPS   :(
                Dim TDP As String = " " 'Translado desde otra administradora de pensiones :(
                Dim TAP As String = " " 'Traslado a otra administradora de pensiones :(
                Dim VSP As String = " " 'Variacion permanente del salario :)
                Dim VTE As String = " " 'Cambio de tarifa Especial :(
                Dim VST As String = " " 'Variacion Transitoria de Salario :(
                Dim SLN As String = " " 'Suspension temporal del contrato o Licencia no Remunerada :|
                Dim IGE As String = " " 'Incapacidad por enfermedad General :|
                Dim LMA As String = " " 'Licencia de maternidad o incapacidad :)
                Dim VAC As String = " " 'Vacaciones :)
                Dim AVP As String = " " 'Aporte Voluntario :)
                Dim VCT As String = " " 'Variacion de Centro de trabajo :(
                Dim IRP As String = "  "  'Incapacidad por Accidente de trabajo o Emfermedad Profesional Catidad de dias :)
                Dim LR As String = "  " 'Licencias Remuneradas
                Dim pensionAdministratorCode As String = "" 'Codigo de la entidad administradora de pensiones a la cual pertenece el empleado :)
                Dim transferPensionAdministratorCode As String = "" 'Codigo de la administrador de pensiones a l cual se traslado :(
                Dim EPSCode As String = "" 'Codigo de la EPS :)
                Dim transferEPSCode As String = "" 'Codigo de la eps a la cual se traslado :(
                Dim CCFCode As String = "" 'Codigo de la caja de compensacion :)
                Dim pensionDays As String = "" 'Dias cotiados en pension 0-30 :)
                Dim healthDays As String = "" 'Dias cotizados en salud 0-30 :)
                Dim professionalRiskDays = "" 'Dias Cotizados a riesgos profesionales :)
                Dim compensationFundDays = "" 'Dias cotizados en caja de compensacion familiar :)
                Dim integralSalary = " " 'Marcar con una x si es salario integral :)
                Dim IBCPension As Double = 0 'Ingreso Base cotizacion de pension :)
                Dim IBCHealth As Double = 0 'Ingreso Base cotizacion de pension :)
                Dim IBCProfessionalRisk As Double = 0 'Ingreso Base cotizacion de riesgos profesionales :(
                Dim IBCCompensationFund As Double = 0 'Ingreso Base cotizacion de Caja de compensacion :)
                Dim IBCSENA As Double = 0 'Ingreso base cotizacion para SENA :)
                Dim IBCICBF As Double = 0 'Ingreso base cotizacion para ICBF :)
                'Variable para el sistema general de pension
                Dim rateContributionsPension As Decimal = 0.04 'Tarifa de de aportes deacuerdo a la ley :(
                Dim voluntaryContributionPensionValuePatron As Decimal = 0 'tarifa de aportes voluntarios a pension pero el patron es decir la empresa :)
                Dim PensionSolidarityFundValueContribution As Decimal = 0 'Aporte a fondo de solidaridad de pension Subcuenta de solidaridad :(
                Dim PensionSolidarityFundValueContributionSubSistence As Decimal = 0 'Aporte a fondo de solidaridad de pension Subcuenta de Subsistencia :(
                Dim ValueNotRetainedByVoluntaryContributions As Decimal = 0 'Valor no retenido por aportes voluntarios :(
                'Variables para el sistema general de salud
                Dim rateContributionsHealth As Decimal = 0.04 'Tarifa de aportes para el sector salud :(
                Dim valueAditionalUPC As Decimal = 0 'Valor adicional de la UPC (Unidad de pago por Capitacion) :(
                Dim authorizationNumberDisability As String = "" 'Numero de la autorizacion de la emfermedad general si la hay :(
                Dim valueGeneralDisability As Decimal = 0 'Valor de la incapacidad general si la hay :(
                Dim authorizationNumberMaternityLicense As String = "" 'Numero de autorizacion de la licencia de maternidad si hay :(
                Dim valueMaternityLicense As Decimal = 0 'Valor de la incapacidad por licencia de maternidad :(
                'Variables para el sistema general de riesgos profesionales
                Dim rateContributionProfessionalRisk As Decimal = 0 'Tarifa de aportes para riesgos, correspondiente a la actividad del centro de trabajo :(
                'Variables para los pagos de parafiscales
                Dim rateContributorCCF As Decimal = 0.04 'Tarifa de aportes para CCF
                Dim rateContributorSENA As Decimal = 0.02 'Tarifa de aportes para SENA
                Dim rateContributorICBF As Decimal = 0.03 'Tarifa de aportes para ICBF
                Dim rateContributorESAP As Decimal = 0 'Tarifa de aportes para ESAP
                Dim rateContributorEducationMinistry As Decimal = 0 'Tarifa de áportes al ministerio de educacion
                Dim TarifaEspecialPensiones As String = " "


                sequenceDetail += 1
                Dim employee As Employee = (From e In listLiquidation
                                            Where e.EmployeeId = idEmployee
                                            Select e).FirstOrDefault().Employee
                Dim ultimateContract As Contract = (From e In listLiquidation
                                                    Where e.EmployeeId = idEmployee And e.PayrollDateLiquidated = PayrollDateLiquidated
                                                    Order By e.PayrollDateLiquidated Descending
                                                    Select e).FirstOrDefault().Contract
                Dim unpaidLicenseDays As Integer = (Aggregate e In listLiquidation
                                                    Where e.EmployeeId = idEmployee
                                                    Into Sum(e.UnpaidLicenseDays))
                Dim generalInabilityDays As Integer = (Aggregate e In listLiquidation
                                                     Where e.EmployeeId = idEmployee
                                                     Into Sum(e.AmbulatoryDisabilityDays))
                Dim maternityLeaveDays As Integer = (Aggregate e In listLiquidation
                                                     Where e.EmployeeId = idEmployee
                                                     Into Sum(e.MaternityLeaveDays))
                Dim vacationDays As Integer = (Aggregate e In listLiquidation
                                               Where e.EmployeeId = idEmployee
                                               Into Sum(e.VacationDays))
                Dim voluntaryContributionPensionValue As Decimal = (Aggregate e In listLiquidation
                                                                    Where e.EmployeeId = idEmployee
                                                                    Into Sum(e.VoluntaryContributionPensionValue))
                Dim occupationalRisksContributionDays As Integer = (Aggregate e In listLiquidation
                                                                  Where e.EmployeeId = idEmployee
                                                                  Into Sum(e.OccupationalRisksDays))

                Dim LicenseDays As Integer = (Aggregate e In listLiquidation
                                                  Where e.EmployeeId = idEmployee
                                                  Into Sum(e.LicenseDays))

                Dim ObjLiquidation As Liquidation = (From e In listLiquidation
                                                     Where e.EmployeeId = idEmployee
                                                     Select e).FirstOrDefault()

                professionalRiskDays = ObjLiquidation.ProvisionDays
                compensationFundDays = ObjLiquidation.ProvisionDays


                '' Cargo el Fondo de Solidaridad Pensional
                PensionSolidarityFundValueContribution = ((From x In ObjLiquidation.LiquidationDetail Where x.ConceptClass = "038" Select x.ConceptTotalValue).Sum()) / 2
                PensionSolidarityFundValueContributionSubSistence = PensionSolidarityFundValueContribution


                If LicenseDays > 0 Then 'Si tiene Licencias Remuneradas
                    LR = "X"
                End If

                If unpaidLicenseDays > 0 Then 'Si tiene dias de licencia no remunerada
                    SLN = "X"
                End If
                If generalInabilityDays > 0 Then 'Si tiene dias de incapacidad general
                    IGE = "X"
                End If
                If maternityLeaveDays > 0 Then 'Si tiene licencia de maternidad
                    LMA = "X"
                End If
                If vacationDays > 0 Then ' Si tiene dias de vacaciones
                    VAC = "X"
                End If
                If voluntaryContributionPensionValue > 0 Then 'Si tiene un aporte voluntario a pension
                    AVP = "X"
                End If
                If occupationalRisksContributionDays > 0 Then ' Si tiene dias de incapacidad profesional
                    IRP = Right(Utils.StringPad(occupationalRisksContributionDays, 2, "0", Utils.PadType.STR_PAD_LEFT), 2)
                Else
                    IRP = Right(Utils.StringPad("", 2, "0", Utils.PadType.STR_PAD_LEFT), 2)
                End If
                Dim EPS As Fund = Nothing
                Dim listEPS = (From e In ultimateContract.FundContract
                               Where e.FundType = 1 And e.VoluntaryContribution = False And e.State = True
                               Select e.Fund).ToList()
                If listEPS.Count > 0 Then
                    If listEPS.Count > 1 Then
                        resultActionMessage.MessageResult.Add(New MessageResult(" - 001", employee.ThirdParty.Name))
                    End If
                    EPS = listEPS.Item(0)
                Else
                    resultActionMessage.MessageResult.Add(New MessageResult("-002", employee.ThirdParty.Name))
                    Continue For
                End If
                If EPS IsNot Nothing Then
                    EPSCode = EPS.MinistryCode
                End If
                'Se busca el Fondo de Riesgos Laborales
                Dim RiskFound As Fund = Nothing
                Dim listRiskFound = (From e In ultimateContract.FundContract
                                     Where e.FundType = 4 And e.VoluntaryContribution = False And e.State = True
                                     Select e.Fund).ToList()
                If listRiskFound.Count > 0 Then
                    If listRiskFound.Count > 1 Then
                        resultActionMessage.MessageResult.Add(New MessageResult(" - 008", employee.ThirdParty.Name))
                    End If
                    RiskFound = listRiskFound.Item(0)
                Else
                    resultActionMessage.MessageResult.Add(New MessageResult("-007", employee.ThirdParty.Name))
                    Continue For
                End If

                healthDays = Utils.StringPad((Aggregate e In listLiquidation
                              Where e.EmployeeId = idEmployee
                              Into Sum(e.QuoteHealthDays)).ToString(), 2, 0, Utils.PadType.STR_PAD_LEFT)
                IBCHealth = Utils.RoundValueNearestThousand((Aggregate e In listLiquidation
                                                             Where e.EmployeeId = idEmployee
                                                             Into Sum(e.HealthJCB)))
                Select Case ultimateContract.ContractType.SalaryType
                    Case 1 'Fijo

                    Case 2 'Integral
                        integralSalary = "X"
                    Case 3 'Variable
                        'VSP = "X"
                        VSP = " "
                End Select
                Select Case ultimateContract.ContractType.ContractClass
                    Case 2 'Aprendizaje
                        typeContractEmployee = "12"
                    Case 3, 4 'Indefinidos y fijos
                        typeContractEmployee = "01"
                        'IBC pension
                        IBCPension = Utils.RoundValueNearestThousand((Aggregate e In listLiquidation
                                      Where e.EmployeeId = idEmployee
                                      Into Sum(e.PensionJCB)))
                        'Administrador de pensiones
                        Dim listPensionAdministrator = (From e In ultimateContract.FundContract
                                                        Where e.FundType = 2 And e.VoluntaryContribution = False And e.State = True
                                                        Select e.Fund).ToList()
                        Dim pensionAdministrator As Fund
                        If listPensionAdministrator.Count > 0 Then
                            If listPensionAdministrator.Count > 1 Then
                                resultActionMessage.MessageResult.Add(New MessageResult("-003", employee.ThirdParty.Name))
                                Continue For
                            End If
                            pensionAdministrator = listPensionAdministrator.Item(0)
                        Else
                            resultActionMessage.MessageResult.Add(New MessageResult("-004", employee.ThirdParty.Name))
                            Continue For
                        End If
                        If pensionAdministrator IsNot Nothing Then
                            pensionAdministratorCode = pensionAdministrator.MinistryCode
                        End If
                        'Caja de compensacion Familiar
                        Dim listCCF = (From e In ultimateContract.FundContract
                                       Where e.FundType = 5 And e.State = True
                                       Select e.Fund).ToList()
                        Dim CCF As Fund
                        If listCCF.Count > 0 Then
                            If listCCF.Count > 1 Then
                                resultActionMessage.MessageResult.Add(New MessageResult("-005", employee.ThirdParty.Name))
                                Continue For
                            End If
                            CCF = listCCF.Item(0)
                        Else
                            resultActionMessage.MessageResult.Add(New MessageResult("-006", employee.ThirdParty.Name))
                            Continue For
                        End If
                        If CCF IsNot Nothing Then
                            CCFCode = CCF.MinistryCode
                        End If
                        'Ibc de la caja de compensacion
                        IBCCompensationFund = Utils.RoundValueNearestThousand((Aggregate e In listLiquidation
                                               Where e.EmployeeId = idEmployee
                                               Into Sum(e.IBCCompensationFund)))
                        'Obtengo el ingreso base cotizacion del Sena
                        IBCSENA = Utils.RoundValueNearestThousand((Aggregate e In listLiquidation
                                               Where e.EmployeeId = idEmployee
                                               Into Sum(e.IBCSENA)))
                        IBCICBF = Utils.RoundValueNearestThousand((Aggregate e In listLiquidation
                                               Where e.EmployeeId = idEmployee
                                               Into Sum(e.IBCICBF)))
                        IBCProfessionalRisk = Utils.RoundValueNearestThousand((Aggregate e In listLiquidation
                                               Where e.EmployeeId = idEmployee
                                               Into Sum(e.IBCOccupationalRisks)))
                        'Dias de pension
                        pensionDays = Utils.StringPad((Aggregate e In listLiquidation
                                       Where e.EmployeeId = idEmployee
                                       Into Sum(e.PensionContributionDays)).ToString(), 2, 0, Utils.PadType.STR_PAD_LEFT)
                End Select
                Select Case employee.ThirdParty.Person.IdentificationType
                    Case 0 'Cedula
                        typeDocument = "CC"
                    Case 1 'Cedula de Extranjeria
                        typeDocument = "CE"
                    Case 2 'Tarjeta Identidad
                        typeDocument = "TI"
                    Case 3 'Registro Civil
                        typeDocument = "RC"
                    Case 4 'Pasaporte
                        typeDocument = "PA"
                End Select
                If ultimateContract.JobBondingDate.ToString("yyyy-MM") = periodLiquidation Then
                    entry = "X"
                End If
                If ultimateContract.RetirementDate IsNot Nothing AndAlso ultimateContract.RetirementDate.Value.ToString("yyyy-MM") = periodLiquidation Then
                    retirement = "X"
                End If

                If pensionDays = "" Then
                    pensionDays = "00"
                End If

                If typeContractEmployee = "12" Then
                    professionalRiskDays = "0"
                    compensationFundDays = "0"
                End If

                If ultimateContract.Group.Code = "05" Then
                    integralSalary = "X"
                End If

                'Cargo los porcentajes
                rateContributionProfessionalRisk = (employee.ProfessionalRiskPercentage / 100)
                rateContributionsPension = ((ultimateContract.Group.PayrollParameter.EmployeePensionContributionPercentage + ultimateContract.Group.PayrollParameter.EmployerPensionContributionPercentage) / 100)
                rateContributionsHealth = ((ultimateContract.Group.PayrollParameter.EmployeeHealthContributionPercentage + ultimateContract.Group.PayrollParameter.EmployerHealthContributionPercentage) / 100)
                rateContributorCCF = (ultimateContract.Group.PayrollParameter.CompensationFundContributionPercentage) / 100
                rateContributorICBF = (ultimateContract.Group.PayrollParameter.ICBFContributionPercentage) / 100
                rateContributorSENA = (ultimateContract.Group.PayrollParameter.SenaContributionPercentage) / 100

                If rateContributionProfessionalRisk = 0.0696 Then
                    TarifaEspecialPensiones = "1"
                End If


                result.Append(vbCrLf)
                result.Append("02") '1
                result.Append(Utils.StringPad(sequenceDetail, 5, 0, Utils.PadType.STR_PAD_LEFT)) '2
                result.Append(typeDocument) '3
                result.Append(Utils.StringPad(employee.ThirdParty.Nit, 16, " ")) '4
                result.Append(Utils.StringPad(typeContractEmployee, 2, 0, Utils.PadType.STR_PAD_LEFT)) '5
                result.Append(Utils.StringPad(subTypeEmployee, 2, 0, Utils.PadType.STR_PAD_LEFT)) '6
                result.Append(foreignNotBound & colombianForeignResident) '7 y 8
                'result.Append(Left(Utils.StringPad(ultimateContract.FunctionalUnit.BranchOffice.City.Department.Code, 2, 0, Utils.PadType.STR_PAD_LEFT), 3)) '9
                result.Append(Left(Utils.StringPad(ultimateContract.FunctionalUnit.BranchOffice.City.Code, 5, 0, Utils.PadType.STR_PAD_LEFT), 5)) '9 y 10
                result.Append(Left(Utils.StringPad(employee.ThirdParty.Person.FirstLastName, 20, " "), 20)) '11
                result.Append(Left(Utils.StringPad(employee.ThirdParty.Person.SecondLastName, 30, " "), 30)) '12
                result.Append(Left(Utils.StringPad(employee.ThirdParty.Person.FirstName, 20, " "), 20)) '13
                result.Append(Left(Utils.StringPad(employee.ThirdParty.Person.SecondName, 30, " "), 30)) '14
                result.Append(entry & retirement & TDE & TAE & TDP & TAP & VSP)
                result.Append(" " & VST & SLN & IGE & LMA & VAC & AVP & VCT & IRP)
                result.Append(Utils.StringPad(pensionAdministratorCode, 6, " ", Utils.PadType.STR_PAD_LEFT)) '31
                result.Append(Utils.StringPad(transferPensionAdministratorCode, 6, " ", Utils.PadType.STR_PAD_LEFT)) '32
                result.Append(Utils.StringPad(EPSCode, 6, " ")) '33 
                result.Append(Utils.StringPad(transferEPSCode, 6, " ")) '34
                result.Append(Utils.StringPad(CCFCode, 6, " ")) '35
                result.Append(pensionDays & healthDays) '36 y 37
                result.Append(Utils.StringPad(professionalRiskDays, 2, 0, Utils.PadType.STR_PAD_LEFT)) '38
                result.Append(Utils.StringPad(compensationFundDays, 2, 0, Utils.PadType.STR_PAD_LEFT)) '39
                result.Append(Utils.StringPad(ultimateContract.BasicSalary, 9, 0, Utils.PadType.STR_PAD_LEFT)) '40
                result.Append(integralSalary) '41
                result.Append(Utils.StringPad(IBCPension, 9, 0, Utils.PadType.STR_PAD_LEFT)) '42
                result.Append(Utils.StringPad(IBCHealth, 9, 0, Utils.PadType.STR_PAD_LEFT)) '43
                result.Append(Utils.StringPad(IBCHealth, 9, 0, Utils.PadType.STR_PAD_LEFT)) '44 -- ACA DEBERIA IR EL IBC DE RIESGOS "IBCProfessionalRisk" pero el sistema esta guardando mal el IBC

                result.Append(Utils.StringPad(IBCCompensationFund, 9, 0, Utils.PadType.STR_PAD_LEFT)) '45
                'Detalle de pensiones
                result.Append(Utils.StringPad(Replace(rateContributionsPension.ToString(), ",", "."), 7, 0)) '46
                Dim ValuePension As Decimal = IBCPension * rateContributionsPension
                ValuePension = Math.Ceiling(ValuePension / 100) * 100

                PensionSolidarityFundValueContribution = Math.Ceiling(PensionSolidarityFundValueContribution / 100) * 100
                PensionSolidarityFundValueContributionSubSistence = Math.Ceiling(PensionSolidarityFundValueContributionSubSistence / 100) * 100

                result.Append(Utils.StringPad(ValuePension, 9, 0, Utils.PadType.STR_PAD_LEFT)) '47
                result.Append(Utils.StringPad(Utils.RoundValueNearestThousand(voluntaryContributionPensionValue), 9, 0, Utils.PadType.STR_PAD_LEFT)) '48
                result.Append(Utils.StringPad(Utils.RoundValueNearestThousand(voluntaryContributionPensionValuePatron), 9, 0, Utils.PadType.STR_PAD_LEFT)) '49
                result.Append(Utils.StringPad(Utils.RoundValueNearestThousand((rateContributionsPension * IBCPension) + voluntaryContributionPensionValue + voluntaryContributionPensionValuePatron), 9, 0, Utils.PadType.STR_PAD_LEFT)) 'Total Aportes a pension - 50
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(PensionSolidarityFundValueContribution), 9, 0, Utils.PadType.STR_PAD_LEFT)) '51
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(PensionSolidarityFundValueContributionSubSistence), 9, 0, Utils.PadType.STR_PAD_LEFT)) '52
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(ValueNotRetainedByVoluntaryContributions), 9, 0, Utils.PadType.STR_PAD_LEFT)) '53

                'Detalle para el sistema de salud
                result.Append(Utils.StringPad(Replace(rateContributionsHealth.ToString(), ",", "."), 7, 0)) '54
                Dim ValueHealth As Decimal = IBCHealth * rateContributionsHealth
                ValueHealth = Math.Ceiling(ValueHealth / 100) * 100

                'result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(IBCHealth * rateContributionsHealth), 9, 0, Utils.PadType.STR_PAD_LEFT)) '55
                result.Append(Utils.StringPad(ValueHealth, 9, 0, Utils.PadType.STR_PAD_LEFT)) '55
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(valueAditionalUPC), 9, 0, Utils.PadType.STR_PAD_LEFT)) '56
                result.Append(Utils.StringPad(authorizationNumberDisability, 15, " ")) '57
                result.Append(Utils.StringPad(valueGeneralDisability, 9, 0)) '58
                result.Append(Utils.StringPad(authorizationNumberMaternityLicense, 15, " ")) '59
                result.Append(Utils.StringPad(valueMaternityLicense, 9, 0, Utils.PadType.STR_PAD_LEFT)) '60

                'Detalle del sistema general de riesgos profesionales
                result.Append(Utils.StringPad(Replace(rateContributionProfessionalRisk.ToString(), ",", "."), 9, 0)) '61
                result.Append(Utils.StringPad(employee.WorkCenter.Code.Trim(), 9, 0, Utils.PadType.STR_PAD_LEFT)) '62

                Dim ValueContributionProfessionalRisk As Decimal = IBCProfessionalRisk * rateContributionProfessionalRisk
                ValueContributionProfessionalRisk = Math.Ceiling(ValueContributionProfessionalRisk / 100) * 100

                result.Append(Utils.StringPad(ValueContributionProfessionalRisk, 9, 0, Utils.PadType.STR_PAD_LEFT)) '63

                'Detalle de pagos parafiscales
                result.Append(Utils.StringPad(Replace(rateContributorCCF.ToString(), ",", "."), 7, 0)) '64

                Dim ValueContributionCCF As Decimal = IBCCompensationFund * rateContributorCCF
                ValueContributionCCF = Math.Ceiling(ValueContributionCCF / 100) * 100

                result.Append(Utils.StringPad(ValueContributionCCF, 9, 0, Utils.PadType.STR_PAD_LEFT)) '65
                result.Append(Utils.StringPad(Replace(rateContributorSENA.ToString(), ",", "."), 7, 0)) '66

                Dim ValueSena As Decimal = IBCSENA * rateContributorSENA
                ValueSena = Math.Ceiling(ValueSena / 100) * 100

                result.Append(Utils.StringPad(ValueSena, 9, 0, Utils.PadType.STR_PAD_LEFT)) '67
                result.Append(Utils.StringPad(Replace(rateContributorICBF.ToString(), ",", "."), 7, 0)) '68

                Dim ValueICBF As Decimal = IBCICBF * rateContributorICBF
                ValueICBF = Math.Ceiling(ValueICBF / 100) * 100

                result.Append(Utils.StringPad(ValueICBF, 9, 0, Utils.PadType.STR_PAD_LEFT)) '69
                result.Append(Utils.StringPad(rateContributorESAP, 7, 0)) '70
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(rateContributorESAP * IBCCompensationFund), 9, 0, Utils.PadType.STR_PAD_LEFT)) '71
                result.Append(Utils.StringPad(rateContributorEducationMinistry, 7, 0)) '72
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(rateContributorEducationMinistry * IBCCompensationFund), 9, 0, Utils.PadType.STR_PAD_LEFT)) '73

                ''
                result.Append(Utils.StringPad(" ", 2, " ", Utils.PadType.STR_PAD_LEFT)) '74
                result.Append(Utils.StringPad(" ", 16, " ", Utils.PadType.STR_PAD_LEFT)) '75
                result.Append(Utils.StringPad(" ", 1, " ", Utils.PadType.STR_PAD_LEFT)) '76
                result.Append(Utils.StringPad(RiskFound.MinistryCode, 6, " ", Utils.PadType.STR_PAD_LEFT)) '77
                result.Append(Utils.StringPad(Right(ultimateContract.Position.ProfessionalRisk.Code, 1), 1, " ", Utils.PadType.STR_PAD_LEFT)) '78
                result.Append(Utils.StringPad(TarifaEspecialPensiones, 1, " ", Utils.PadType.STR_PAD_LEFT)) '79
                result.Append(Utils.StringPad(IIf(entry = "X", ultimateContract.JobBondingDate.ToString("yyyy-MM-dd"), " "), 10, " ", Utils.PadType.STR_PAD_LEFT)) '80

                Dim DateRetirement As String
                Dim VacationInitialDate As String
                Dim VacationEndDate As String
                Dim SanctionInitialDate As String
                Dim SanctionEndDate As String
                Dim AmbulatoryDisabilityInitialDate As String
                Dim AmbulatoryDisabilityEndDate As String
                Dim MaternityLeaveInitialDate As String
                Dim MaternityLeaveEndDate As String
                Dim UnpaidLicenseInitialDate As String
                Dim UnpaidLicenseEndDate As String
                Dim OccupationalRiskInitialDate As String
                Dim OccupationalRiskEndDate As String

                If retirement = "X" Then
                    DateRetirement = ultimateContract.RetirementDate.ToString("yyyy-MM-dd")
                Else
                    DateRetirement = String.Empty
                End If

                If VAC = "X" Then
                    VacationInitialDate = ObjLiquidation.VacationInitialDate.ToString()
                    VacationEndDate = ObjLiquidation.VacationEndDate.ToString()
                    VacationInitialDate = DateTime.Parse(VacationInitialDate).ToString("yyyy-MM-dd")
                    VacationEndDate = DateTime.Parse(VacationEndDate).ToString("yyyy-MM-dd")
                Else
                    VacationInitialDate = String.Empty
                    VacationEndDate = String.Empty
                End If

                If SLN = "X" Then

                    If ObjLiquidation.SanctionInitialDate < PayrollStarDate Then
                        SanctionInitialDate = PayrollStarDate.ToString()
                    Else
                        SanctionInitialDate = ObjLiquidation.SanctionInitialDate.ToString()
                    End If

                    If ObjLiquidation.SanctionEndDate > PayrollDateLiquidated Then
                        SanctionEndDate = PayrollDateLiquidated.ToString()
                    Else
                        SanctionEndDate = ObjLiquidation.SanctionEndDate.ToString()
                    End If

                    SanctionInitialDate = DateTime.Parse(SanctionInitialDate).ToString("yyyy-MM-dd")
                    SanctionEndDate = DateTime.Parse(SanctionEndDate).ToString("yyyy-MM-dd")

                    If SanctionInitialDate = "0001-01-01" Then
                        SanctionInitialDate = String.Empty
                        SanctionEndDate = String.Empty
                    End If
                Else
                    SanctionInitialDate = String.Empty
                    SanctionEndDate = String.Empty
                End If

                If IGE = "X" Then

                    If ObjLiquidation.AmbulatoryDisabilityInitialDate < PayrollStarDate Then
                        AmbulatoryDisabilityInitialDate = PayrollStarDate.ToString()
                    Else
                        AmbulatoryDisabilityInitialDate = ObjLiquidation.AmbulatoryDisabilityInitialDate.ToString()
                    End If

                    If ObjLiquidation.AmbulatoryDisabilityEndDate > PayrollDateLiquidated Then
                        AmbulatoryDisabilityEndDate = PayrollDateLiquidated.ToString()
                    Else
                        AmbulatoryDisabilityEndDate = ObjLiquidation.AmbulatoryDisabilityEndDate.ToString()
                    End If


                    AmbulatoryDisabilityInitialDate = DateTime.Parse(AmbulatoryDisabilityInitialDate).ToString("yyyy-MM-dd")
                    AmbulatoryDisabilityEndDate = DateTime.Parse(AmbulatoryDisabilityEndDate).ToString("yyyy-MM-dd")

                    If AmbulatoryDisabilityInitialDate = "0001-01-01" Then
                        AmbulatoryDisabilityInitialDate = String.Empty
                        AmbulatoryDisabilityEndDate = String.Empty
                    End If
                Else
                    AmbulatoryDisabilityInitialDate = String.Empty
                    AmbulatoryDisabilityEndDate = String.Empty
                End If

                If LMA = "X" Then

                    If ObjLiquidation.MaternityLeaveInitialDate < PayrollStarDate Then
                        MaternityLeaveInitialDate = PayrollStarDate.ToString()
                    Else
                        MaternityLeaveInitialDate = ObjLiquidation.MaternityLeaveInitialDate.ToString()
                    End If

                    If ObjLiquidation.MaternityLeaveEndDate > PayrollDateLiquidated Then
                        MaternityLeaveEndDate = PayrollDateLiquidated.ToString()
                    Else
                        MaternityLeaveEndDate = ObjLiquidation.MaternityLeaveEndDate.ToString()
                    End If


                    MaternityLeaveInitialDate = DateTime.Parse(MaternityLeaveInitialDate).ToString("yyyy-MM-dd")
                    MaternityLeaveEndDate = DateTime.Parse(MaternityLeaveEndDate).ToString("yyyy-MM-dd")

                    If MaternityLeaveInitialDate = "0001-01-01" Then
                        MaternityLeaveInitialDate = String.Empty
                        MaternityLeaveEndDate = String.Empty
                    End If
                Else
                    MaternityLeaveInitialDate = String.Empty
                    MaternityLeaveEndDate = String.Empty
                End If

                If VAC = "X" And LR = "X" Then

                    If ObjLiquidation.UnpaidLicenseInitialDate < PayrollStarDate Then
                        UnpaidLicenseInitialDate = PayrollStarDate.ToString()
                    Else
                        UnpaidLicenseInitialDate = ObjLiquidation.UnpaidLicenseInitialDate.ToString()
                    End If

                    If ObjLiquidation.UnpaidLicenseEndDate > PayrollDateLiquidated Then
                        UnpaidLicenseEndDate = PayrollDateLiquidated.ToString()
                    Else
                        UnpaidLicenseEndDate = ObjLiquidation.UnpaidLicenseEndDate.ToString()
                    End If

                    UnpaidLicenseInitialDate = DateTime.Parse(UnpaidLicenseInitialDate).ToString("yyyy-MM-dd")
                    UnpaidLicenseEndDate = DateTime.Parse(UnpaidLicenseEndDate).ToString("yyyy-MM-dd")

                    If UnpaidLicenseInitialDate = "0001-01-01" Then
                        UnpaidLicenseInitialDate = String.Empty
                        UnpaidLicenseEndDate = String.Empty
                    End If
                Else
                    UnpaidLicenseInitialDate = String.Empty
                    UnpaidLicenseEndDate = String.Empty
                End If



                If ObjLiquidation.OccupationalRisksDisabilityInitialDate < PayrollStarDate Then
                    OccupationalRiskInitialDate = PayrollStarDate.ToString()
                Else
                    OccupationalRiskInitialDate = ObjLiquidation.OccupationalRisksDisabilityInitialDate.ToString()
                End If

                If ObjLiquidation.OccupationalRisksDisabilityEndDate > PayrollDateLiquidated Then
                    OccupationalRiskEndDate = PayrollDateLiquidated.ToString()
                Else
                    OccupationalRiskEndDate = ObjLiquidation.OccupationalRisksDisabilityEndDate.ToString()
                End If


                OccupationalRiskInitialDate = DateTime.Parse(OccupationalRiskInitialDate).ToString("yyyy-MM-dd")
                OccupationalRiskEndDate = DateTime.Parse(OccupationalRiskEndDate).ToString("yyyy-MM-dd")

                If OccupationalRiskInitialDate = "0001-01-01" Then
                    OccupationalRiskInitialDate = String.Empty
                    OccupationalRiskEndDate = String.Empty
                End If

                Dim TotalHours As Double = 0

                TotalHours = CDbl(ultimateContract.HoursDaily) * CDbl(ObjLiquidation.DaysWorked)

                If ObjLiquidation.LiquidationDetail.Sum(Function(x) x.TotalNumberHours) > 0 Then
                    TotalHours = TotalHours + ObjLiquidation.LiquidationDetail.Sum(Function(x) x.TotalNumberHours)
                End If


                result.Append(Utils.StringPad(DateRetirement, 10, " ", Utils.PadType.STR_PAD_LEFT)) '81
                result.Append(Utils.StringPad(" ", 10, " ", Utils.PadType.STR_PAD_LEFT)) '82
                result.Append(Utils.StringPad(SanctionInitialDate, 10, " ", Utils.PadType.STR_PAD_LEFT)) '83
                result.Append(Utils.StringPad(SanctionEndDate, 10, " ", Utils.PadType.STR_PAD_LEFT)) '84
                result.Append(Utils.StringPad(AmbulatoryDisabilityInitialDate, 10, " ", Utils.PadType.STR_PAD_LEFT)) '85
                result.Append(Utils.StringPad(AmbulatoryDisabilityEndDate, 10, " ", Utils.PadType.STR_PAD_LEFT)) '86
                result.Append(Utils.StringPad(MaternityLeaveInitialDate, 10, " ", Utils.PadType.STR_PAD_LEFT)) '87
                result.Append(Utils.StringPad(MaternityLeaveEndDate, 10, " ", Utils.PadType.STR_PAD_LEFT)) '88
                result.Append(Utils.StringPad(VacationInitialDate, 10, " ", Utils.PadType.STR_PAD_LEFT)) '89
                result.Append(Utils.StringPad(VacationEndDate, 10, " ", Utils.PadType.STR_PAD_LEFT)) '90
                result.Append(Utils.StringPad(" ", 10, " ", Utils.PadType.STR_PAD_LEFT)) '91
                result.Append(Utils.StringPad(" ", 10, " ", Utils.PadType.STR_PAD_LEFT)) '92
                result.Append(Utils.StringPad(OccupationalRiskInitialDate, 10, " ", Utils.PadType.STR_PAD_LEFT)) '93
                result.Append(Utils.StringPad(OccupationalRiskEndDate, 10, " ", Utils.PadType.STR_PAD_LEFT)) '94
                result.Append(Utils.StringPad(IBCCompensationFund, 9, " ", Utils.PadType.STR_PAD_LEFT)) '95
                result.Append(Utils.StringPad(TotalHours, 3, " ", Utils.PadType.STR_PAD_LEFT)) '96
                result.Append(Utils.StringPad("", 10, " ", Utils.PadType.STR_PAD_LEFT)) '97 -- Fecha en la que un empleado salio al exterior

            Next
            resultActionMessage.ObjectEmbbeded = result
            resultActionMessage.StateResult = True
            Return resultActionMessage
        Catch ex As Exception
            resultActionMessage.ObjectEmbbeded = Nothing
            resultActionMessage.StateResult = False
            Return resultActionMessage
        End Try

    End Function

    ''' <summary>
    ''' Funcion que calcula el digito de verificacion del Nit
    ''' </summary>
    ''' <param name="Nit">Nit del tercero</param>
    Public Function Calculate_VerificationCode(ByVal nit As String) As String Implements IAutoliquidationDomain.Calculate_VerificationCode
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

    Public Function GetStarPayrollDate(PayrollEndDate As Date) As Date

        Dim PayrollStarDate As Date

        PayrollStarDate = DateAdd(DateInterval.Month, -1, PayrollEndDate)
        PayrollStarDate = DateAdd(DateInterval.Day, 1, PayrollStarDate)

        Return PayrollStarDate

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
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
