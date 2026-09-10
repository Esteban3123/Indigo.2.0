'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Globalization
Imports System.IO
Imports System.Text
Imports DevExpress.Data.Helpers
Imports Domain.Base.Entities
Imports Domain.Common.Entities
Imports Domain.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Class AutoliquidationDomain

    Implements IAutoliquidationDomain

#Region "Fields"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>

    Private _autoliquidationRepository As IAutoliquidationRepository

    Private _payrollSettingsRepository As IPayrollSettingsRepository

    Private _cityRepository As ICityRepository

    Private _workCenterRepository As IWorkCenterRepository

    ''' <summary>
    ''' Repositorio de liquidacion de nomina
    ''' </summary>
    ''' <remarks></remarks>
    Private _liquidationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Prefijo para Costa Rica
    ''' </summary>
    Private Const _crPrefix As String = "506"
    ''' <summary>
    ''' Diccionario que almacena la abreviacion de los paises 
    ''' </summary>
    Private ReadOnly TStandardCodeDict As New Dictionary(Of String, String) From {
   {"020", "AD"},
    {"784", "AE"},
    {"004", "AF"},
    {"028", "AG"},
    {"660", "AI"},
    {"008", "AL"},
    {"051", "AM"},
    {"530", "AN"},
    {"024", "AO"},
    {"010", "AQ"},
    {"032", "AR"},
    {"016", "AS"},
    {"040", "AT"},
    {"036", "AU"},
    {"533", "AW"},
    {"248", "AX"},
    {"031", "AZ"},
    {"070", "BA"},
    {"052", "BB"},
    {"050", "BD"},
    {"056", "BE"},
    {"854", "BF"},
    {"100", "BG"},
    {"048", "BH"},
    {"108", "BI"},
    {"204", "BJ"},
    {"652", "BL"},
    {"060", "BM"},
    {"096", "BN"},
    {"068", "BO"},
    {"076", "BR"},
    {"044", "BS"},
    {"064", "BT"},
    {"074", "BV"},
    {"072", "BW"},
    {"112", "BY"},
    {"084", "BZ"},
    {"124", "CA"},
    {"166", "CC"},
    {"140", "CF"},
    {"178", "CG"},
    {"756", "CH"},
    {"384", "CI"},
    {"184", "CK"},
    {"152", "CL"},
    {"120", "CM"},
    {"156", "CN"},
    {"170", "CO"},
    {"188", "CR"},
    {"192", "CU"},
    {"132", "CV"},
    {"162", "CX"},
    {"196", "CY"},
    {"203", "CZ"},
    {"276", "DE"},
    {"262", "DJ"},
    {"208", "DK"},
    {"212", "DM"},
    {"214", "DO"},
    {"012", "DZ"},
    {"218", "EC"},
    {"233", "EE"},
    {"818", "EG"},
    {"732", "EH"},
    {"232", "ER"},
    {"724", "ES"},
    {"231", "ET"},
    {"246", "FI"},
    {"242", "FJ"},
    {"238", "FK"},
    {"583", "FM"},
    {"234", "FO"},
    {"250", "FR"},
    {"266", "GA"},
    {"826", "GB"},
    {"308", "GD"},
    {"268", "GE"},
    {"254", "GF"},
    {"831", "GG"},
    {"288", "GH"},
    {"292", "GI"},
    {"304", "GL"},
    {"270", "GM"},
    {"324", "GN"},
    {"312", "GP"},
    {"226", "GQ"},
    {"300", "GR"},
    {"239", "GS"},
    {"320", "GT"},
    {"316", "GU"},
    {"624", "GW"},
    {"328", "GY"},
    {"344", "HK"},
    {"334", "HM"},
    {"340", "HN"},
    {"191", "HR"},
    {"332", "HT"},
    {"348", "HU"},
    {"360", "ID"},
    {"372", "IE"},
    {"376", "IL"},
    {"833", "IM"},
    {"356", "IN"},
    {"086", "IO"},
    {"368", "IQ"},
    {"364", "IR"},
    {"352", "IS"},
    {"380", "IT"},
    {"832", "JE"},
    {"388", "JM"},
    {"400", "JO"},
    {"392", "JP"},
    {"404", "KE"},
    {"417", "KG"},
    {"116", "KH"},
    {"296", "KI"},
    {"174", "KM"},
    {"659", "KN"},
    {"408", "KP"},
    {"410", "KR"},
    {"414", "KW"},
    {"136", "KY"},
    {"398", "KZ"},
    {"418", "LA"},
    {"422", "LB"},
    {"662", "LC"},
    {"438", "LI"},
    {"144", "LK"},
    {"430", "LR"},
    {"426", "LS"},
    {"440", "LT"},
    {"442", "LU"},
    {"428", "LV"},
    {"434", "LY"},
    {"504", "MA"},
    {"492", "MC"},
    {"498", "MD"},
    {"499", "ME"},
    {"450", "MG"},
    {"584", "MH"},
    {"807", "MK"},
    {"466", "ML"},
    {"104", "MM"},
    {"496", "MN"},
    {"446", "MO"},
    {"474", "MQ"},
    {"478", "MR"},
    {"500", "MS"},
    {"470", "MT"},
    {"480", "MU"},
    {"462", "MV"},
    {"454", "MW"},
    {"484", "MX"},
    {"458", "MY"},
    {"508", "MZ"},
    {"516", "NA"},
    {"540", "NC"},
    {"562", "NE"},
    {"574", "NF"},
    {"566", "NG"},
    {"558", "NI"},
    {"528", "NL"},
    {"578", "NO"},
    {"524", "NP"},
    {"520", "NR"},
    {"570", "NU"},
    {"554", "NZ"},
    {"512", "OM"},
    {"591", "PA"},
    {"604", "PE"},
    {"258", "PF"},
    {"598", "PG"},
    {"608", "PH"},
    {"586", "PK"},
    {"616", "PL"},
    {"666", "PM"},
    {"612", "PN"},
    {"630", "PR"},
    {"275", "PS"},
    {"620", "PT"},
    {"585", "PW"},
    {"600", "PY"},
    {"634", "QA"},
    {"638", "RE"},
    {"642", "RO"},
    {"688", "RS"},
    {"643", "RU"},
    {"646", "RW"},
    {"682", "SA"},
    {"090", "SB"},
    {"690", "SC"},
    {"736", "SD"},
    {"752", "SE"},
    {"702", "SG"},
    {"654", "SH"},
    {"705", "SI"},
    {"744", "SJ"},
    {"703", "SK"},
    {"694", "SL"},
    {"674", "SM"},
    {"686", "SN"},
    {"706", "SO"},
    {"740", "SR"},
    {"678", "ST"},
    {"222", "SV"},
    {"760", "SY"},
    {"748", "SZ"},
    {"796", "TC"},
    {"148", "TD"},
    {"260", "TF"},
    {"768", "TG"},
    {"764", "TH"},
    {"834", "TH"},
    {"762", "TJ"},
    {"772", "TK"},
    {"626", "TL"},
    {"795", "TM"},
    {"788", "TN"},
    {"776", "TO"},
    {"792", "TR"},
    {"780", "TT"},
    {"798", "TV"},
    {"158", "TW"},
    {"804", "UA"},
    {"800", "UG"},
    {"840", "US"},
    {"858", "UY"},
    {"860", "UZ"},
    {"336", "VA"},
    {"670", "VC"},
    {"862", "VE"},
    {"092", "VG"},
    {"850", "VI"},
    {"704", "VN"},
    {"548", "VU"},
    {"876", "WF"},
    {"882", "WS"},
    {"887", "YE"},
    {"175", "YT"},
    {"710", "ZA"}}
#End Region

#Region "Builder"
    Public Sub New(autoliquidationRepository As IAutoliquidationRepository, payrollSettingsRepository As IPayrollSettingsRepository, cityRepository As ICityRepository, workcenterId As IWorkCenterRepository, liquidationRepository As IPayrollLiquidationRepository)
        _autoliquidationRepository = autoliquidationRepository
        _payrollSettingsRepository = payrollSettingsRepository
        _cityRepository = cityRepository
        _workCenterRepository = workcenterId
        _liquidationRepository = liquidationRepository
    End Sub
#End Region

    Public Function GenerateArchive(company As Entities.Company, workCenter As WorkCenter, periodLiquidation As String, isCorrection As Boolean, dateLiquidation As Date?, numberTemplate As String, listLiquidation As List(Of Liquidation), SpreadsheetType As String) As ActionMessageResult(Of StringBuilder) Implements IAutoliquidationDomain.GenerateArchive

        Dim resultActionMessage As New ActionMessageResult(Of StringBuilder)()
        Try

            Dim payrollSettings = _payrollSettingsRepository.GetSettingPayroll()

            Dim PayrollDate As Date = New Date(listLiquidation.FirstOrDefault().PayrollDateLiquidated.Year, listLiquidation.FirstOrDefault().PayrollDateLiquidated.Month, 1)
            Dim cityName As String = "-"
            If workCenter.Code Is Nothing Then
                Dim aux_city = _cityRepository.GetCityById(company.CityId)
                If aux_city IsNot Nothing Then
                    cityName = aux_city.Name
                End If
            End If

            'Validamos que los grupos tengan parametrizado el SMMLVAmountExemption
            Dim validation = _autoliquidationRepository.ValidateSMMLVAmountExemptionParametrization(listLiquidation)
            If validation IsNot Nothing Then
                Dim message As String = "Debe parametrizar la cantidad de SMMLV  para exoneración de aportes parafiscales para el grupo " & validation
                resultActionMessage.MessageResult.Add(New MessageResult("-009", message))
                resultActionMessage.ObjectEmbbeded = Nothing
                resultActionMessage.StateResult = False
                Return resultActionMessage
            End If

            Dim totalValuePayroll As Decimal = (From a In listLiquidation Select a.TotalPaid).Sum()

            Dim ListObjAutoliquidation = _autoliquidationRepository.ListVerifyAutoliquidation(workCenter.Code, PayrollDate)

            If ListObjAutoliquidation Is Nothing Then
                resultActionMessage.MessageResult.Add(New MessageResult("-009", "No se encontraron registros"))
                resultActionMessage.ObjectEmbbeded = Nothing
                resultActionMessage.StateResult = False
                Return resultActionMessage
            End If
            If SpreadsheetType = "E" Then
                ListObjAutoliquidation = ListObjAutoliquidation.Where(Function(x) x.Employee.EmployeeType.EmployeeClass <> "23").ToList()
            ElseIf SpreadsheetType = "K" Then
                ListObjAutoliquidation = ListObjAutoliquidation.Where(Function(x) x.Employee.EmployeeType.EmployeeClass = "23").ToList()
            End If


            Dim TotalEmployees As Integer = (From e In ListObjAutoliquidation Select e.Employee).Distinct.Count()

            If ListObjAutoliquidation.Any(Function(x) x.RegisterStatus = False) Then
                resultActionMessage.MessageResult.Add(New MessageResult("-009", "No se puede generar un archivo de Seguridad Social sin Confirmar primero"))
                resultActionMessage.ObjectEmbbeded = Nothing
                resultActionMessage.StateResult = False
                Return resultActionMessage
            End If

            If payrollSettings.PILAOperators Is Nothing Then
                resultActionMessage.MessageResult.Add(New MessageResult("-009", "No ha parametrizado el Parámetro del Operador de PILA en Parámetros de Nómina"))
                resultActionMessage.ObjectEmbbeded = Nothing
                resultActionMessage.StateResult = False
                Return resultActionMessage
            End If

            If company.Fund Is Nothing Then
                resultActionMessage.MessageResult.Add(New MessageResult("-009", "No ha parametrizado el Fondo de ARL para la Empresa. Se parametriza en el formulario de Empresas de Nómina."))
                resultActionMessage.ObjectEmbbeded = Nothing
                resultActionMessage.StateResult = False
                Return resultActionMessage
            End If


            Dim CodeArp As String = company.Fund.MinistryCode

            resultActionMessage.StateResult = False
            Dim result As New StringBuilder()
            Dim periodDateLiquidation As Date = New Date(periodLiquidation.Substring(0, 4), periodLiquidation.Substring(5, 2), 1)
            Dim PeriodDateLiquidationPlane As String = periodDateLiquidation.ToString("yyyy-MM")
            Dim periodHealth As String = periodDateLiquidation.AddMonths(1).ToString("yyyy-MM")

            Dim sequenceHead As Integer = 1
            Dim lineHead As String = "01" & Utils.StringPad(sequenceHead, 1, 0, Utils.PadType.STR_PAD_LEFT) '01 y 02
            lineHead &= Utils.StringPad("0001", 4, " ", Utils.PadType.STR_PAD_RIGHT) '03
            lineHead &= Utils.StringPad(company.Name.Trim(), 200, " ", Utils.PadType.STR_PAD_RIGHT) '04
            lineHead &= Utils.StringPad("NI", 2, " ", Utils.PadType.STR_PAD_RIGHT) '05
            lineHead &= Left(Utils.StringPad(company.ThirdParty.Nit.Trim(), 16, " ", Utils.PadType.STR_PAD_RIGHT), 16) '06
            lineHead &= Utils.StringPad(Calculate_VerificationCode(company.ThirdParty.Nit.Trim()), 1, " ", Utils.PadType.STR_PAD_LEFT) '07
            If SpreadsheetType = "E" Then
                lineHead &= Utils.StringPad("E", 1, " ", Utils.PadType.STR_PAD_RIGHT) '08
            ElseIf SpreadsheetType = "K" Then
                lineHead &= Utils.StringPad("K", 1, " ", Utils.PadType.STR_PAD_RIGHT) '08
            End If
            lineHead &= Utils.StringPad(" ", 10, " ", Utils.PadType.STR_PAD_LEFT) '09
            lineHead &= Utils.StringPad(" ", 10, " ", Utils.PadType.STR_PAD_LEFT) '10
            lineHead &= Utils.StringPad("S", 1, " ", Utils.PadType.STR_PAD_RIGHT) '11
            lineHead &= Utils.StringPad(LTrim(RTrim(workCenter.Code)), 10, " ", Utils.PadType.STR_PAD_RIGHT) '12
            lineHead &= Utils.StringPad(If(workCenter.Name IsNot Nothing, workCenter.Name, cityName), 40, " ", Utils.PadType.STR_PAD_RIGHT) '13
            lineHead &= Utils.StringPad(CodeArp, 6, " ", Utils.PadType.STR_PAD_RIGHT) '14
            lineHead &= Utils.StringPad(PeriodDateLiquidationPlane, 7, " ", Utils.PadType.STR_PAD_LEFT) '15
            lineHead &= Utils.StringPad(periodHealth, 7, " ", Utils.PadType.STR_PAD_LEFT) '16
            lineHead &= Utils.StringPad(" ", 10, " ", Utils.PadType.STR_PAD_RIGHT) '17
            lineHead &= Utils.StringPad(" ", 10, " ", Utils.PadType.STR_PAD_RIGHT) '18
            lineHead &= Utils.StringPad(TotalEmployees, 5, 0, Utils.PadType.STR_PAD_LEFT) '19
            lineHead &= Utils.StringPad(totalValuePayroll, 12, 0, Utils.PadType.STR_PAD_LEFT) '20
            lineHead &= Utils.StringPad("01", 2, " ", Utils.PadType.STR_PAD_LEFT) '21
            lineHead &= Utils.StringPad(payrollSettings.PILAOperators, 2, " ", Utils.PadType.STR_PAD_LEFT) '22
            result.Append(lineHead)

            Dim Sequense As Integer = 1

            For Each a As Domain.Payroll.Entities.VerifyAutoliquidationFile In ListObjAutoliquidation
                result.Append(vbCrLf)
                result.Append("02") '1
                result.Append(Utils.StringPad(Sequense, 5, 0, Utils.PadType.STR_PAD_LEFT)) '2

                Dim documentType = GetAbbreviationByDocumentType(a.DocumentType)
                If documentType Is Nothing Then
                    Throw New Exception("No se puede generar archivo para este tipo de documento")
                End If
                result.Append(documentType) '3
                result.Append(Utils.StringPad(a.NitEmployee, 16, " ")) '4
                result.Append(Utils.StringPad(a.TypeContractEmployee, 2, 0, Utils.PadType.STR_PAD_LEFT)) '5
                result.Append(Utils.StringPad(a.ContributorSubtype, 2, 0, Utils.PadType.STR_PAD_LEFT)) '6
                result.Append(a.ForeignNotBound) '7
                result.Append(a.ColombianForeignResident) ' 8
                result.Append(Left(Utils.StringPad(a.CodeCityBranchOffice, 5, 0, Utils.PadType.STR_PAD_LEFT), 5)) '9 y 10
                result.Append(Left(Utils.StringPad(a.FirstLastName, 20, " "), 20)) '11          
                result.Append(Left(Utils.StringPad(a.SecondLastName, 30, " "), 30)) '12
                result.Append(Left(Utils.StringPad(a.FirstName, 20, " "), 20)) '13
                result.Append(Left(Utils.StringPad(a.SecondName, 30, " "), 30)) '14
                result.Append(a.Entry & a.Retirement & a.TDE & a.TAE & a.TDP & a.TAP & a.VSP) '15-21

                Dim IRP As String = 0
                If a.IRL > 0 Then ' Si tiene dias de incapacidad profesional
                    IRP = Right(Utils.StringPad(a.IRL, 2, "0", Utils.PadType.STR_PAD_LEFT), 2)
                Else
                    IRP = Right(Utils.StringPad("", 2, "0", Utils.PadType.STR_PAD_LEFT), 2)
                End If

                result.Append(" " & a.VST & a.SLN & a.IGE & a.LMA & a.VAC & a.AVP & a.VCT & IRP) '22-30
                result.Append(Utils.StringPad(a.PensionAdministratorCode, 6, " ", Utils.PadType.STR_PAD_RIGHT)) '31
                result.Append(Utils.StringPad(a.TransferPensionAdministratorCode, 6, " ", Utils.PadType.STR_PAD_LEFT)) '32
                result.Append(Utils.StringPad(a.EPSCode, 6, " ")) '33 
                result.Append(Utils.StringPad(a.TransferEPSCode, 6, " ")) '34
                result.Append(Utils.StringPad(a.CCFCode, 6, " ")) '35
                result.Append(Utils.StringPad(a.PensionDays, 2, 0, Utils.PadType.STR_PAD_LEFT)) '36
                result.Append(Utils.StringPad(a.HealthDays, 2, 0, Utils.PadType.STR_PAD_LEFT)) '37
                result.Append(Utils.StringPad(a.ProfessionalRiskDays, 2, 0, Utils.PadType.STR_PAD_LEFT)) '38
                result.Append(Utils.StringPad(a.CompensationFundDays, 2, 0, Utils.PadType.STR_PAD_LEFT)) '39
                result.Append(Utils.StringPad(a.BasicSalary, 9, 0, Utils.PadType.STR_PAD_LEFT)) '40
                result.Append(a.IntegralSalary) '41
                result.Append(Utils.StringPad(a.IBCPension, 9, 0, Utils.PadType.STR_PAD_LEFT)) '42
                result.Append(Utils.StringPad(a.IBCHealth, 9, 0, Utils.PadType.STR_PAD_LEFT)) '43
                result.Append(Utils.StringPad(a.IBCProfessionalRisk, 9, 0, Utils.PadType.STR_PAD_LEFT)) '44 -- ACA DEBERIA IR EL IBC DE RIESGOS "IBCProfessionalRisk" pero el sistema esta guardando mal el IBC

                result.Append(Utils.StringPad(a.IBCCompensationFund, 9, 0, Utils.PadType.STR_PAD_LEFT)) '45
                'Detalle de pensiones
                result.Append(Utils.StringPad(Replace(a.RateContributionPension.ToString(), ",", "."), 7, 0)) '46

                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(a.ValuePension), 9, 0, Utils.PadType.STR_PAD_LEFT)) '47
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(a.VoluntaryContributionPensionValue), 9, 0, Utils.PadType.STR_PAD_LEFT)) '48
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(a.VoluntaryContributionPensionValuePatron), 9, 0, Utils.PadType.STR_PAD_LEFT)) '49
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(a.TotalPensionContribution), 9, 0, Utils.PadType.STR_PAD_LEFT)) 'Total Aportes a pension - 50

                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(a.PensionSolidarityFundValueContribution), 9, 0, Utils.PadType.STR_PAD_LEFT)) '51
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(a.PensionSolidarityFundValueContributionSubsistence), 9, 0, Utils.PadType.STR_PAD_LEFT)) '52 PensionSolidarityFundValueContributionSubSistence
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(a.ValueNotRetainedByVoluntaryContributions), 9, 0, Utils.PadType.STR_PAD_LEFT)) '53

                'Detalle para el sistema de salud
                Dim RateContributionHealth As String = ""
                If a.RateContributionHealth = 0 Then
                    RateContributionHealth = "0.00"
                Else
                    RateContributionHealth = a.RateContributionHealth.ToString()
                End If

                result.Append(Utils.StringPad(Replace(RateContributionHealth, ",", "."), 7, 0)) '54


                result.Append(Utils.StringPad(a.ValueHealth, 9, 0, Utils.PadType.STR_PAD_LEFT)) '55
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(a.ValueAditionalUPC), 9, 0, Utils.PadType.STR_PAD_LEFT)) '56
                result.Append(Utils.StringPad(a.AuthorizationNumberDisability, 15, " ")) '57 a.authorizationNumberDisability
                result.Append(Utils.StringPad(a.ValueGeneralDisability, 9, 0)) '58 a.valueGeneralDisability
                result.Append(Utils.StringPad(" ", 15, " ")) '59 a.authorizationNumberMaternityLicense
                result.Append(Utils.StringPad(0, 9, 0)) '60

                Dim contributionRate As Decimal = 0
                If Not String.IsNullOrWhiteSpace(a.IGE) Or Not String.IsNullOrWhiteSpace(a.VAC) Then
                    contributionRate = 0
                Else
                    contributionRate = Replace((a.Employee.ProfessionalRiskPercentage / 100).ToString(), ",", ".")
                End If
                ' Detalle del sistema general de riesgos profesionales
                result.Append(Utils.StringPad(contributionRate, 9, 0, Utils.PadType.STR_PAD_LEFT)) '61
                result.Append(Utils.StringPad(a.WorkCenter, 9, 0, Utils.PadType.STR_PAD_LEFT)) '62
                result.Append(Utils.StringPad(a.ValueContributionProfessionalRisk, 9, 0, Utils.PadType.STR_PAD_LEFT)) '63

                'Detalle de pagos parafiscales
                result.Append(Utils.StringPad(Replace(a.RateContributorCCF.ToString(), ",", "."), 7, 0, Utils.PadType.STR_PAD_LEFT)) '64
                result.Append(Utils.StringPad(a.ValueContributionCCF, 9, 0, Utils.PadType.STR_PAD_LEFT)) '65

                result.Append(Utils.StringPad(Replace(a.RateContributorSENA.ToString(), ",", "."), 7, 0, Utils.PadType.STR_PAD_LEFT)) '66
                result.Append(Utils.StringPad(a.ValueSena / 100, 9, 0, Utils.PadType.STR_PAD_LEFT)) '67

                result.Append(Utils.StringPad(Replace(a.RateContributionICBF.ToString(), ",", "."), 7, 0, Utils.PadType.STR_PAD_LEFT)) '68
                result.Append(Utils.StringPad(a.ValueICBF / 100, 9, 0, Utils.PadType.STR_PAD_LEFT)) '69

                result.Append(Utils.StringPad(Replace(a.RateContributorESAP.ToString(), ",", "."), 7, 0, Utils.PadType.STR_PAD_LEFT)) '70
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(a.RateContributorESAP * a.IBCCompensationFund), 9, 0, Utils.PadType.STR_PAD_LEFT)) '71
                result.Append(Utils.StringPad(Replace(a.RateContributorEducationMinistry.ToString(), ",", "."), 7, 0, Utils.PadType.STR_PAD_LEFT)) '72
                result.Append(Utils.StringPad(Utils.RoundedValuesNearestHundred(a.RateContributorEducationMinistry * a.IBCCompensationFund), 9, 0, Utils.PadType.STR_PAD_LEFT)) '73


                Dim CotizanteExonerado As String = "S"
                If a.IBCHealth >= (a.SMMLVAmountExemption * a.LegalSalaryMinimum) Then
                    CotizanteExonerado = "N"
                End If

                If a.TypeContractEmployee = 19 Then
                    CotizanteExonerado = "N"
                End If

                Dim TarifaEspecialPensiones As String = ""
                If a.TarifaEspecialPensiones = 0 Then
                    TarifaEspecialPensiones = " "
                Else
                    TarifaEspecialPensiones = a.TarifaEspecialPensiones.ToString()
                End If

                result.Append(Utils.StringPad(" ", 2, " ", Utils.PadType.STR_PAD_RIGHT)) '74
                result.Append(Utils.StringPad(" ", 16, " ", Utils.PadType.STR_PAD_RIGHT)) '75
                result.Append(Utils.StringPad(CotizanteExonerado, 1, " ", Utils.PadType.STR_PAD_RIGHT)) '76
                result.Append(Utils.StringPad(a.MinistryCodeRiskFound, 6, " ", Utils.PadType.STR_PAD_RIGHT)) '77
                result.Append(Utils.StringPad(Right(a.ProfessionalRiskCode, 1), 1, " ", Utils.PadType.STR_PAD_RIGHT)) '78
                result.Append(Utils.StringPad(TarifaEspecialPensiones, 1, " ", Utils.PadType.STR_PAD_RIGHT)) '79


                Dim IngressDate As String
                Dim DateRetirement As String
                Dim SanctionInitialDate As String
                Dim SanctionEndDate As String
                Dim AmbulatoryDisabilityInitialDate As String
                Dim AmbulatoryDisabilityEndDate As String
                Dim MaternityLeaveInitialDate As String
                Dim MaternityLeaveEndDate As String
                Dim VacationInitialDate As String
                Dim VacationEndDate As String
                Dim FechaInicioVCT As String
                Dim FechaFinVCT As String
                Dim FechaInicioIRL As String
                Dim FechaFinIRL As String
                Dim FechaEmpleadoExterior As String


                If a.IngressDate IsNot Nothing Then
                    IngressDate = DateTime.Parse(a.IngressDate).ToString("yyyy-MM-dd")
                Else
                    IngressDate = String.Empty
                End If

                If a.DateRetirement IsNot Nothing Then
                    DateRetirement = DateTime.Parse(a.DateRetirement).ToString("yyyy-MM-dd")
                Else
                    DateRetirement = String.Empty
                End If

                If a.SanctionInitialDate IsNot Nothing Then
                    SanctionInitialDate = DateTime.Parse(a.SanctionInitialDate).ToString("yyyy-MM-dd")
                Else
                    SanctionInitialDate = String.Empty
                End If

                If a.SanctionEndDate IsNot Nothing Then
                    SanctionEndDate = DateTime.Parse(a.SanctionEndDate).ToString("yyyy-MM-dd")
                Else
                    SanctionEndDate = String.Empty
                End If

                If a.AmbulatoryDisabilityInitialDate IsNot Nothing Then
                    AmbulatoryDisabilityInitialDate = DateTime.Parse(a.AmbulatoryDisabilityInitialDate).ToString("yyyy-MM-dd")
                Else
                    AmbulatoryDisabilityInitialDate = String.Empty
                End If

                If a.AmbulatoryDisabiltyEndDate IsNot Nothing Then
                    AmbulatoryDisabilityEndDate = DateTime.Parse(a.AmbulatoryDisabiltyEndDate).ToString("yyyy-MM-dd")
                Else
                    AmbulatoryDisabilityEndDate = String.Empty
                End If

                If a.MaternityLeaveInitialDate IsNot Nothing Then
                    MaternityLeaveInitialDate = DateTime.Parse(a.MaternityLeaveInitialDate).ToString("yyyy-MM-dd")
                Else
                    MaternityLeaveInitialDate = String.Empty
                End If

                If a.MaternityLeaveEndDate IsNot Nothing Then
                    MaternityLeaveEndDate = DateTime.Parse(a.MaternityLeaveEndDate).ToString("yyyy-MM-dd")
                Else
                    MaternityLeaveEndDate = String.Empty
                End If

                If a.VacationInitialDate IsNot Nothing Then
                    VacationInitialDate = DateTime.Parse(a.VacationInitialDate).ToString("yyyy-MM-dd")
                Else
                    VacationInitialDate = String.Empty
                End If

                If a.VacationEndDate IsNot Nothing Then
                    VacationEndDate = DateTime.Parse(a.VacationEndDate).ToString("yyyy-MM-dd")
                Else
                    VacationEndDate = String.Empty
                End If

                If a.FechaInicioVCT IsNot Nothing Then
                    FechaInicioVCT = DateTime.Parse(a.FechaInicioVCT).ToString("yyyy-MM-dd")
                Else
                    FechaInicioVCT = String.Empty
                End If

                If a.FechaFinVCT IsNot Nothing Then
                    FechaFinVCT = DateTime.Parse(a.FechaFinVCT).ToString("yyyy-MM-dd")
                Else
                    FechaFinVCT = String.Empty
                End If

                If a.FechaInicioIRL IsNot Nothing Then
                    FechaInicioIRL = DateTime.Parse(a.FechaInicioIRL).ToString("yyyy-MM-dd")
                Else
                    FechaInicioIRL = String.Empty
                End If

                If a.FechaFinIRL IsNot Nothing Then
                    FechaFinIRL = DateTime.Parse(a.FechaFinIRL).ToString("yyyy-MM-dd")
                Else
                    FechaFinIRL = String.Empty
                End If

                If a.FechaEmpleadoExterior IsNot Nothing Then
                    FechaEmpleadoExterior = DateTime.Parse(a.FechaEmpleadoExterior).ToString("yyyy-MM-dd")
                Else
                    FechaEmpleadoExterior = String.Empty
                End If

                result.Append(Utils.StringPad(IngressDate, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '80
                result.Append(Utils.StringPad(DateRetirement, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '81
                result.Append(Utils.StringPad(" ", 10, " ", Utils.PadType.STR_PAD_RIGHT)) '82
                result.Append(Utils.StringPad(SanctionInitialDate, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '83
                result.Append(Utils.StringPad(SanctionEndDate, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '84
                result.Append(Utils.StringPad(AmbulatoryDisabilityInitialDate, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '85
                result.Append(Utils.StringPad(AmbulatoryDisabilityEndDate, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '86
                result.Append(Utils.StringPad(MaternityLeaveInitialDate, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '87
                result.Append(Utils.StringPad(MaternityLeaveEndDate, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '88
                result.Append(Utils.StringPad(VacationInitialDate, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '89
                result.Append(Utils.StringPad(VacationEndDate, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '90
                result.Append(Utils.StringPad(FechaInicioVCT, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '91
                result.Append(Utils.StringPad(FechaFinVCT, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '92
                result.Append(Utils.StringPad(FechaInicioIRL, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '93
                result.Append(Utils.StringPad(FechaFinIRL, 10, " ", Utils.PadType.STR_PAD_RIGHT)) '94
                result.Append(Utils.StringPad(a.IBCOtrosParafiscales, 9, 0, Utils.PadType.STR_PAD_LEFT)) '95
                result.Append(Utils.StringPad(a.TotalHours, 3, 0, Utils.PadType.STR_PAD_LEFT)) '96
                result.Append(Utils.StringPad(FechaEmpleadoExterior, 10, " ", Utils.PadType.STR_PAD_LEFT)) '97 -- Fecha en la que un empleado salio al exterior
                result.Append(Utils.StringPad(IIf(a.EconomicActivityARL IsNot Nothing, a.EconomicActivityARL, String.Empty), 7, " ", Utils.PadType.STR_PAD_LEFT)) '98 -- Código de la ARL del riesgo profesional al que pertenece el funcionario

                Sequense = Sequense + 1

            Next


            resultActionMessage.ObjectEmbbeded = result
            resultActionMessage.StateResult = True
            Return resultActionMessage
        Catch ex As Exception
            resultActionMessage.ObjectEmbbeded = Nothing
            resultActionMessage.StateResult = False
            resultActionMessage.Message = Utils.GetInnerExceptionMessageToString(ex)
            resultActionMessage.MessageResult = Nothing
            Return resultActionMessage
        End Try
    End Function
    ''' <summary>
    ''' Logica que crea el Archivo Plano Para CCSS
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="workCenterId"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <param name="listLiquidation"></param>
    ''' <returns></returns>
    Public Function GenerateCCSS(company As Domain.Payroll.Entities.Company, workCenterId As Integer, periodLiquidation As String, listLiquidation As List(Of Liquidation)) As ActionMessageResult(Of StringBuilder) Implements IAutoliquidationDomain.GenerateCCSS
        Dim resultActionMessage As New ActionMessageResult(Of StringBuilder)()
        Try
            Dim PayrollDate As Date = New Date(listLiquidation.FirstOrDefault().PayrollDateLiquidated.Year, listLiquidation.FirstOrDefault().PayrollDateLiquidated.Month, 1)
            resultActionMessage.StateResult = False
            Dim WorkerInformationCount As Integer = 0

            Dim result As New StringBuilder()
            Dim workCenter As New WorkCenter
            workCenter = _workCenterRepository.GetWorkCenterById(workCenterId)
            Dim periodDateLiquidation As Date = New Date(periodLiquidation.Substring(0, 4), periodLiquidation.Substring(5, 2), 1)

            'Cabecera
            Dim lineHead As String = "25"
            lineHead &= "20" & company.Nit.Trim() & "001001" ' Campo 2: Número Patronal
            lineHead &= Utils.StringPad("1123", 4, "0", Utils.PadType.STR_PAD_LEFT) ' Campo 3: Sucursal adscrita
            lineHead &= periodDateLiquidation.ToString("yyyyMM") ' Campo 4: Periodo planilla (formato AAAAMM)
            lineHead &= Utils.StringPad(" ", 1, " ", Utils.PadType.STR_PAD_RIGHT) ' Campo 5: Indicador de suspensión (en blanco)
            result.Append(lineHead & Environment.NewLine)

            'Detalle
            Dim listObjAutoliquidation = GenerateViewVerifyAutoliquidationFile(workCenter, periodLiquidation)
            listObjAutoliquidation = listObjAutoliquidation _
            .OrderBy(Function(x) x.FirstLastName) _
            .ThenBy(Function(x) x.SecondLastName) _
            .ThenBy(Function(x) x.CompleteName) _
            .ThenBy(Function(x) If(x.ChangeType = "SA", 0, If(x.ChangeType = "IC", 1, If(x.ChangeType = "OC", 2, If(x.ChangeType = "PE", 3, If(x.ChangeType = "IN", 4, If(x.ChangeType = "EX", 5, 6))))))) _
            .ThenBy(Function(x) x.ChangeCode) _
            .ThenBy(Function(x) x.InitialDate) _
            .ToList()

            Dim icIds = listObjAutoliquidation.Where(Function(r) r.ChangeType = "IC").Select(Function(r) r.EmployeeId)
            Dim employeesWithIC As New HashSet(Of Integer)(icIds)

            For Each detail As ViewVerifyAutoliquidationFile In listObjAutoliquidation
                If detail.ChangeType = "SA" AndAlso String.IsNullOrEmpty(detail.ChangeCode) AndAlso employeesWithIC.Contains(detail.EmployeeId) Then
                    Continue For
                End If
                Dim lineDet As String = "35"
                            Select Case detail.IdentificationType
                                Case "CC", "CF"
                                    detail.IdentificationType = "0"
                                    lineDet &= Utils.StringPad(detail.IdentificationType, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                                Case "DM", "PA"
                                    detail.IdentificationType = "7"
                                    lineDet &= Utils.StringPad(detail.IdentificationType, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                                Case Else
                                    lineDet &= " "
                            End Select

                            If (detail.IdentificationType = "7") Then 'Se valida que sea cédula fisica si no lo es coloca el codigo asegurado 
                                lineDet &= Utils.StringPad(detail.InsuredCCSSCode, 25, "0", Utils.PadType.STR_PAD_LEFT)
                            Else
                                lineDet &= Utils.StringPad(detail.Nit, 25, "0", Utils.PadType.STR_PAD_LEFT)
                            End If
                            lineDet &= Utils.StringPad(detail.FirstLastName, 20, " ", Utils.PadType.STR_PAD_RIGHT)
                            lineDet &= Utils.StringPad(detail.SecondLastName, 20, " ", Utils.PadType.STR_PAD_RIGHT)
                            lineDet &= Utils.StringPad(detail.CompleteName, 60, " ", Utils.PadType.STR_PAD_RIGHT)
                            lineDet &= Utils.StringPad(detail.CCSSCode, 4, " ", Utils.PadType.STR_PAD_RIGHT)
                            lineDet &= Utils.StringPad(detail.IBCHealth & "00", 15, "0", Utils.PadType.STR_PAD_LEFT) 'Salario Basico IBCSalud
                            Select Case detail.Pensionary
                                Case "Si"
                                    detail.Pensionary = "A"
                                    lineDet &= Utils.StringPad(detail.Pensionary, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                                Case "No"
                                    detail.Pensionary = "C"
                                    lineDet &= Utils.StringPad(detail.Pensionary, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                                Case Else
                                    lineDet &= " "
                            End Select
                            lineDet &= Utils.StringPad(detail.ChangeType, 2, " ", Utils.PadType.STR_PAD_RIGHT) 'Tipo de Cambio
                            lineDet &= If(detail.ChangeType = "IC", Utils.StringPad(detail.HoursDaily.ToString, 2, " ", Utils.PadType.STR_PAD_RIGHT), "  ") 'Horas Jornada
                            lineDet &= If(detail.ChangeType = "IC", "DIU", "   ")

                            Select Case detail.ChangeType 'Codigo de cambio
                                Case "IC"
                                    lineDet &= "GEN"
                                Case Else
                                    lineDet &= If(detail.ChangeCode IsNot Nothing, Utils.StringPad(detail.ChangeCode, 3, " ", Utils.PadType.STR_PAD_RIGHT), "   ")
                            End Select
                            If detail.ChangeType = "EX" Then
                                lineDet &= Utils.StringPad(detail.FinalDate.ToString("yyyMMdd"), 8, " ", Utils.PadType.STR_PAD_RIGHT)
                            ElseIf Not String.IsNullOrEmpty(detail.ChangeCode) Then
                                lineDet &= Utils.StringPad(detail.InitialDate.ToString("yyyMMdd"), 8, " ", Utils.PadType.STR_PAD_RIGHT)
                                lineDet &= Utils.StringPad(If(detail.FinalDate <> Date.MinValue, detail.FinalDate.ToString("yyyMMdd"), "        "), 8, " ", Utils.PadType.STR_PAD_RIGHT)
                            Else
                                lineDet &= Utils.StringPad(detail.InitialDate.ToString("yyyMMdd"), 8, " ", Utils.PadType.STR_PAD_RIGHT)
                            End If
                            WorkerInformationCount += 1
                            result.Append(lineDet & Environment.NewLine)
                        Next

                        'pie de pagina 
                        Dim footerLine As String = "15"
                        footerLine &= "PAT"
                        footerLine &= Utils.StringPad(DateTime.Now.ToString("yyyMMdd"), 8, "0", Utils.PadType.STR_PAD_LEFT)
                        footerLine &= Utils.StringPad("1", 10, "0", Utils.PadType.STR_PAD_LEFT)
                        footerLine &= Utils.StringPad(WorkerInformationCount, 10, "0", Utils.PadType.STR_PAD_LEFT)
                        result.Append(footerLine)
                        'Fin Footer
                        resultActionMessage.ObjectEmbbeded = result
                        resultActionMessage.StateResult = True

                        Return resultActionMessage

        Catch ex As Exception
            resultActionMessage.ObjectEmbbeded = Nothing
            resultActionMessage.StateResult = False
            resultActionMessage.Message = Utils.GetInnerExceptionMessageToString(ex)
            resultActionMessage.MessageResult = Nothing
            Return resultActionMessage
        End Try
    End Function

    ''' <summary>
    ''' Logica que crea el Archivo Plano Para INS
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="policyNumber"></param>
    ''' <param name="workCenter"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <param name="listLiquidation"></param>
    ''' <returns></returns>
    Public Function GenerateINS(company As Domain.Payroll.Entities.Company, policyNumber As String, workCenter As WorkCenter, periodLiquidation As String, listLiquidation As List(Of Liquidation)) As ActionMessageResult(Of StringBuilder) Implements IAutoliquidationDomain.GenerateINS
        Dim resultActionMessage As New ActionMessageResult(Of StringBuilder)()
        Try
            Dim PayrollDate As Date = New Date(listLiquidation.FirstOrDefault().PayrollDateLiquidated.Year, listLiquidation.FirstOrDefault().PayrollDateLiquidated.Month, 1)
            resultActionMessage.StateResult = False

            Dim companyData = _autoliquidationRepository.ExecuteQuery(Of INEMPRESU)("SELECT * FROM INEMPRESU").FirstOrDefault()

            Dim periodDateLiquidation As Date = New Date(periodLiquidation.Substring(0, 4), periodLiquidation.Substring(5, 2), 1)

            Dim listObjAutoliquidation = GenerateViewVerifyAutoliquidationFileINS(workCenter, periodLiquidation)
            Dim result As New StringBuilder()

            'Cabecera
            Dim lineHead As String = ""
            lineHead &= Utils.StringPad(policyNumber, 7, "0", Utils.PadType.STR_PAD_LEFT) 'Campo 1: Numero de poliza
            lineHead &= Utils.StringPad("M", 1, " ", Utils.PadType.STR_PAD_RIGHT) ' Campo 2: Tipo de plantilla
            lineHead &= periodDateLiquidation.ToString("yyyyMM") ' Campo 3: Periodo planilla (formato AAAAMM)
            lineHead &= Utils.StringPad(" ", 1, " ", Utils.PadType.STR_PAD_RIGHT) ' Campo 4: Indicador de suspensión (en blanco)
            lineHead &= Utils.StringPad(companyData.INDIDERES, 20, " ", Utils.PadType.STR_PAD_RIGHT) 'Campo 5: Identificacion Tomador
            lineHead &= Utils.StringPad(CleanPhone(companyData.INDTE1EMP), 8, " ", Utils.PadType.STR_PAD_RIGHT) ' Campo 6: Numero de telefono
            lineHead &= Utils.StringPad(CleanPhone(companyData.INDTE2EMP), 8, " ", Utils.PadType.STR_PAD_RIGHT) ' Campo 7: Numero de fax
            lineHead &= Utils.StringPad(" ", 1, " ", Utils.PadType.STR_PAD_RIGHT) ' Campo 8: Indicador de suspensión (en blanco)
            lineHead &= Utils.StringPad("M", 1, " ", Utils.PadType.STR_PAD_RIGHT) ' Campo 9: Tipo de calemdario
            lineHead &= Utils.StringPad("V01C", 4, " ", Utils.PadType.STR_PAD_RIGHT) ' Campo 10: Version de la plantilla
            lineHead &= Utils.StringPad(" ", 24, " ", Utils.PadType.STR_PAD_RIGHT) ' Campo 11: Indicador de suspensión (en blanco)
            lineHead &= Environment.NewLine
            Dim email As String = Utils.StringPad(companyData.INDCOERES, 40, " ", Utils.PadType.STR_PAD_RIGHT)

            'EMAIL + 6 espacios + correo de 40
            Dim emailFormatted As String = "Email" & New String(" "c, 6) & email

            lineHead &= Utils.StringPad(emailFormatted, 51, " ", Utils.PadType.STR_PAD_RIGHT) 'Linea 2,Campo : Email

            lineHead &= Environment.NewLine
            lineHead &= Utils.StringPad("Domicilio " & companyData.INDDIREMP, 24, " ", Utils.PadType.STR_PAD_RIGHT) ' Linea 3,Campo 1: Direccion
            result.Append(lineHead & Environment.NewLine)

            'Detalle
            For Each detail As ViewVerifyAutoliquidationFile In listObjAutoliquidation
                Dim lineDet As String = ""
                Select Case detail.IdentificationType 'Campo 1:Tipo de identificacion
                    Case "CF"

                        detail.IdentificationType = "0"
                        lineDet &= Utils.StringPad(detail.IdentificationType, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    Case "DM"
                        detail.IdentificationType = "6"
                        lineDet &= Utils.StringPad(detail.IdentificationType, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    Case "PA"
                        detail.IdentificationType = "9"
                        lineDet &= Utils.StringPad(detail.IdentificationType, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    Case Else
                        lineDet &= " "
                End Select
                lineDet &= Utils.StringPad(detail.Nit, 19, " ", Utils.PadType.STR_PAD_RIGHT) 'Campo 2:Numero de identificacion
                lineDet &= Utils.StringPad(If(String.IsNullOrWhiteSpace(detail.InsuredCCSSCode), " ", detail.InsuredCCSSCode), 20, " ", Utils.PadType.STR_PAD_RIGHT) 'Campo 3: Numero de Asegurado CCSS
                lineDet &= Utils.StringPad(detail.CompleteName, 15, " ", Utils.PadType.STR_PAD_RIGHT) 'Campo 4: Nombre Completo
                lineDet &= Utils.StringPad(detail.FirstLastName, 15, " ", Utils.PadType.STR_PAD_RIGHT) 'Campo 5: Primer Apellido
                lineDet &= Utils.StringPad(detail.SecondLastName, 15, " ", Utils.PadType.STR_PAD_RIGHT) 'Campo 6: Segundo Apellido
                lineDet &= Utils.StringPad(detail.BirthDate.ToString("dd/MM/yyyy"), 10, " ", Utils.PadType.STR_PAD_RIGHT) ''Campo 7:Fecha de Nacimiento
                lineDet &= Utils.StringPad(detail.Phone, 8, " ", Utils.PadType.STR_PAD_RIGHT) 'Campo 8:Numero de telefono
                lineDet &= Utils.StringPad(detail.Email, 40, " ", Utils.PadType.STR_PAD_RIGHT) 'Campo 9:Correo Electronico
                lineDet &= Utils.StringPad(detail.Gender, 1, " ", Utils.PadType.STR_PAD_RIGHT) 'Campo 10:Codigo de Genero
                Select Case detail.MaritalStatus 'Campo 11:Codigo de Estado Civil
                    Case 0
                        lineDet &= "1" 'Soltero
                    Case 1
                        lineDet &= "2" 'Casado
                    Case 3
                        lineDet &= "3"'Viudo
                    Case 2
                        lineDet &= "4"'Divorciado
                    Case 4
                        lineDet &= "6" 'Union Libre
                    Case Else
                        lineDet &= " "
                End Select
                'Campo 12:Codigo de Nacionalidad
                Dim countryCode As String = ""
                Dim nationalityCodeInt As Integer
                If Integer.TryParse(detail.NationalityCode, nationalityCodeInt) Then
                    countryCode = GetCountryAbbreviation(nationalityCodeInt, TStandardCodeDict)
                Else
                    countryCode = If(String.IsNullOrEmpty(detail.NationalityCode), "", detail.NationalityCode)
                End If
                lineDet &= Utils.StringPad(countryCode, 2, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(detail.IBCHealth.ToString("F2", System.Globalization.CultureInfo.InvariantCulture), 13, "0", Utils.PadType.STR_PAD_LEFT) 'Campo 13:Salario Devengado
                lineDet &= Utils.StringPad(detail.HealthDays, 3, "0", Utils.PadType.STR_PAD_LEFT) 'Campo 14:DIas Laborales
                lineDet &= Utils.StringPad((detail.HealthDays * detail.HoursDaily), 4, "0", Utils.PadType.STR_PAD_LEFT) 'Campo 15:Código de la Jornada Laboral
                lineDet &= "01" 'Campo 16:Código de la Jornada Laboral(debe ir con 01)

                Select Case detail.ChangeCode'Campo 17:Código de Novedad
                    'Incapacidad
                    Case "INS"
                        detail.NoveltyType = "04"
                    'Licencia no remunerada
                    Case "S"
                        detail.NoveltyType = "06"
                    'Incapacaidad General
                    Case "SEM"
                        detail.NoveltyType = "03"
                    'Licencia de maternidad
                    Case "MAT"
                        detail.NoveltyType = "07"
                    Case Else
                        ' Obtenemos el ultimo contrato 
                        Dim EmployeContract = _liquidationRepository.GetEmployesContract(detail.EmployeeId).OrderByDescending(Function(x) x.Id).FirstOrDefault

                        If EmployeContract Is Nothing Then
                            resultActionMessage.ObjectEmbbeded = Nothing
                            resultActionMessage.StateResult = False
                            resultActionMessage.Message = String.Format("El empleado: {0} con Registro : {1} no tiene un contrato.", detail.CompleteName, detail.Nit)
                            resultActionMessage.MessageResult = Nothing
                            Return resultActionMessage
                        Else

                            ' Convertimos el string "yyyy-MM" a Date (primer día del mes)
                            Dim periodStartDate As Date = Date.ParseExact(periodLiquidation, "yyyy-MM", CultureInfo.InvariantCulture)

                            ' Calculamos el último día del mes
                            Dim periodEndDate As Date = New Date(periodStartDate.Year, periodStartDate.Month, Date.DaysInMonth(periodStartDate.Year, periodStartDate.Month))

                            Dim RetirementDate As Date = If(EmployeContract.RetirementDate, New Date(9999, 1, 1))
                            ' Evaluamos condiciones del contrato 
                            Select Case True
                            ' Caso 1: El contrato comenzó y terminó dentro del mismo mes del periodo
                                Case EmployeContract.ContractInitialDate >= periodStartDate AndAlso RetirementDate <= periodEndDate
                                    detail.NoveltyType = "05"

                            'Caso 2: El contrato comenzó durante ese mes (sin importar si terminó o sigue activo)
                                Case EmployeContract.ContractInitialDate >= periodStartDate AndAlso EmployeContract.ContractInitialDate <= periodEndDate
                                    detail.NoveltyType = "01"
                            'Caso 3: Finalizo durante el periodo de liquidacion
                                Case EmployeContract.RetirementDate >= periodStartDate AndAlso RetirementDate <= periodEndDate
                                    detail.NoveltyType = "02"
                                    'No tiene ninguna novedad 
                                Case Else
                                    detail.NoveltyType = "00"
                            End Select
                        End If

                End Select
                lineDet &= detail.NoveltyType
                lineDet &= Utils.StringPad(detail.INSCode.Trim, 5, "0", Utils.PadType.STR_PAD_LEFT) 'Campo 18:Código de Ocupacion


                result.Append(lineDet & Environment.NewLine)
            Next

            resultActionMessage.ObjectEmbbeded = result
            resultActionMessage.StateResult = True

            Return resultActionMessage

        Catch ex As Exception
            resultActionMessage.ObjectEmbbeded = Nothing
            resultActionMessage.StateResult = False
            resultActionMessage.Message = Utils.GetInnerExceptionMessageToString(ex)
            resultActionMessage.MessageResult = Nothing
            Return resultActionMessage
        End Try
    End Function
    ''' <summary>
    ''' Limpia el número desde la tabla INEMPRESU 
    ''' </summary>
    ''' <param name="numero"></param>
    ''' <returns></returns>
    Private Function CleanPhone(numero As String) As String
        If String.IsNullOrWhiteSpace(numero) Then
            Return String.Empty
        End If
        Dim cleanNumber As String = numero.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "")

        If cleanNumber.StartsWith(_crPrefix) Then 'Prefijo de CR
            cleanNumber = cleanNumber.Substring(3)
        End If

        Return cleanNumber
    End Function

    ''' <summary>
    ''' Obtencion del codigo del pais
    ''' </summary>
    ''' <param name="nationalCode"></param>
    ''' <param name="diccionario"></param>
    ''' <returns></returns>
    Private Function GetCountryAbbreviation(nationalCode As Integer, diccionario As Dictionary(Of String, String)) As String
        Dim codigoStr As String = nationalCode.ToString().PadLeft(3, "0"c)

        Dim codigoPais As String = ""
        If diccionario.TryGetValue(codigoStr, codigoPais) Then
            Return codigoPais
        Else
            Return ""
        End If
    End Function
    ''' <summary>
    ''' Funcion genera una lista desde de la vista ViewVerifyAutoliquidationFile
    ''' </summary>
    ''' <param name="workCenter"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <returns></returns>
    Public Function GenerateViewVerifyAutoliquidationFile(workCenter As WorkCenter, periodLiquidation As String) As List(Of ViewVerifyAutoliquidationFile)
        Dim params As New List(Of (String, Object)) From {
        ("@WorkCenter", workCenter.Code),
        ("@PayrollDateLiquidated", $"{periodLiquidation}-01")
        }

        Dim query = "SELECT * FROM [Payroll].[ViewVerifyAutoliquidationFile] WHERE WorkCenter = @WorkCenter AND PayrollDateLiquidated = @PayrollDateLiquidated"

        Return _autoliquidationRepository.ExecuteQueryDR(Of ViewVerifyAutoliquidationFile)(query, params).ToList()
    End Function
    ''' <summary>
    ''' Funcion genera una lista desde de la vista ViewVerifyAutoliquidationFile INS
    ''' </summary>
    ''' <param name="workCenter"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <returns></returns>
    Public Function GenerateViewVerifyAutoliquidationFileINS(workCenter As WorkCenter, periodLiquidation As String) As List(Of ViewVerifyAutoliquidationFile)
        Dim params As New List(Of (String, Object)) From {
        ("@WorkCenter", workCenter.Code),
        ("@PayrollDateLiquidated", $"{periodLiquidation}-01")
        }

        Dim query = "SELECT * FROM [Payroll].[ViewVerifyAutoliquidationFileINS] WHERE WorkCenter = @WorkCenter AND PayrollDateLiquidated = @PayrollDateLiquidated"

        Return _autoliquidationRepository.ExecuteQueryDR(Of ViewVerifyAutoliquidationFile)(query, params).ToList()
    End Function
    ''' <summary>
    ''' Funcion para retornar la abreviacion del tipo de documento que llega
    ''' </summary>
    ''' <param name="documentType"></param>
    ''' <returns></returns>
    Private Function GetAbbreviationByDocumentType(documentType As Integer?) As String
        Select Case documentType
            Case 0
                Return "CC"
            Case 1
                Return "CE"
            Case 2
                Return "TI"
            Case 3
                Return "RC"
            Case 4
                Return "PA"
            Case 10
                Return "CD"
            Case 11
                Return "SC"
            Case 12
                Return "PE"
            Case 13
                Return "PT"
            Case Else
                Return Nothing
        End Select
    End Function

    Public Function Calculate_VerificationCode(ByVal nit As String) As String 'Implements IAutoliquidationDomain.Calculate_VerificationCode
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

    Private Function ValidateData(ListImportFileRow As List(Of ImportFileRow), ListVerifyAutoliquidationFile As List(Of VerifyAutoliquidationFile)) As ActionResult(Of List(Of VerifyAutoliquidationFile)) Implements IAutoliquidationDomain.ValidateData

        Dim ResultObject As New ActionResult(Of List(Of VerifyAutoliquidationFile))

        Dim TmpListVerifyAutoliquidationFile As New List(Of VerifyAutoliquidationFile)

        Dim ListErrors As New List(Of String)

        Try

            For Each ImportFileRow As ImportFileRow In ListImportFileRow

                Dim ObjVerifyAutoliquidationFile As VerifyAutoliquidationFile
                Dim Id = ImportFileRow.Row(0)

                ObjVerifyAutoliquidationFile = ListVerifyAutoliquidationFile.Where(Function(x) x.Id = Id).FirstOrDefault()

                If ObjVerifyAutoliquidationFile IsNot Nothing Then

                    'Modificamos la entidad:

                    If ImportFileRow.Row(4) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.Entry = ImportFileRow.Row(4).ToString.ToUpper ' Ingreso
                    End If

                    If ImportFileRow.Row(5) IsNot Nothing Then

                        If IsDate(ImportFileRow.Row(5)) Then
                            ObjVerifyAutoliquidationFile.IngressDate = ImportFileRow.Row(5).ToString ' Fecha Ingreso
                        Else
                            ListErrors.Add("El campo FECHA INGRESO tiene datos no válidos")
                        End If
                    End If

                    If ImportFileRow.Row(6) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.Retirement = ImportFileRow.Row(6).ToString.ToUpper() ' Retiro
                    End If

                    If ImportFileRow.Row(7) IsNot Nothing Then
                        If IsDate(ImportFileRow.Row(7)) Then
                            ObjVerifyAutoliquidationFile.DateRetirement = ImportFileRow.Row(7).ToString ' Fecha Retiro
                        Else
                            ListErrors.Add("El campo FECHA RETIRO tiene datos no válidos")
                        End If
                    End If

                    If ImportFileRow.Row(8) IsNot Nothing Then
                        If IsNumeric(ImportFileRow.Row(8)) Then
                            ObjVerifyAutoliquidationFile.IBCOtrosParafiscales = ImportFileRow.Row(8).ToString ' Ibc Otros Parafiscales
                        Else
                            ListErrors.Add("El campo IBC OTROS PARAFISCALES tiene datos no válidos")
                        End If
                    End If

                    If ImportFileRow.Row(9) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.VSP = ImportFileRow.Row(9).ToString ' VSP
                    End If

                    If ImportFileRow.Row(12) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.VST = ImportFileRow.Row(12).ToString ' VST
                    End If

                    If ImportFileRow.Row(13) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.SLN = ImportFileRow.Row(13).ToString ' SLN
                    End If

                    If ImportFileRow.Row(14) IsNot Nothing Then
                        If IsDate(ImportFileRow.Row(14)) Then
                            ObjVerifyAutoliquidationFile.SanctionInitialDate = ImportFileRow.Row(14).ToString ' Fecha Inicio SLN
                        Else
                            ListErrors.Add("El campo FECHA INICIO SLN tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(15) IsNot Nothing Then
                        If IsDate(ImportFileRow.Row(15)) Then
                            ObjVerifyAutoliquidationFile.SanctionEndDate = ImportFileRow.Row(15).ToString ' Fecha Fin SLN
                        Else
                            ListErrors.Add("El campo FECHA INICIO SLN tiene datos no válidos")
                        End If
                    End If

                    If ImportFileRow.Row(16) IsNot Nothing Then
                        If IsNumeric(ImportFileRow.Row(16)) Then
                            ObjVerifyAutoliquidationFile.IBCSLN = ImportFileRow.Row(16).ToString ' IBC SLN
                        Else
                            ListErrors.Add("El campo IBC SLN tiene datos no válidos")
                        End If
                    End If

                    If ImportFileRow.Row(17) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.IGE = ImportFileRow.Row(17).ToString ' IGE
                    End If

                    If ImportFileRow.Row(18) IsNot Nothing Then

                        If IsDate(ImportFileRow.Row(18)) Then
                            ObjVerifyAutoliquidationFile.AmbulatoryDisabilityInitialDate = ImportFileRow.Row(18).ToString ' Fecha Inicio IGE
                        Else
                            ListErrors.Add("El campo FECHA INICIO IGE tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(19) IsNot Nothing Then

                        If IsDate(ImportFileRow.Row(19)) Then
                            ObjVerifyAutoliquidationFile.AmbulatoryDisabiltyEndDate = ImportFileRow.Row(19).ToString ' Fecha Fin IGE
                        Else
                            ListErrors.Add("El campo FECHA FIN IGE tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(20) IsNot Nothing Then

                        If IsNumeric(ImportFileRow.Row(20)) Then
                            If ObjVerifyAutoliquidationFile.BaseIGE Is Nothing Then
                                ObjVerifyAutoliquidationFile.BaseIGE = 0
                            End If

                            ObjVerifyAutoliquidationFile.BaseIGE = ImportFileRow.Row(20).ToString ' Base IGE
                        Else
                            ListErrors.Add("El campo Base IGE tiene datos no válidos")

                        End If

                    End If

                    If ImportFileRow.Row(21) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.LMA = ImportFileRow.Row(21).ToString ' LMA
                    End If

                    If ImportFileRow.Row(22) IsNot Nothing Then

                        If IsDate(ImportFileRow.Row(22)) Then
                            ObjVerifyAutoliquidationFile.MaternityLeaveInitialDate = ImportFileRow.Row(22).ToString ' Fecha Inicio LMA
                        Else
                            ListErrors.Add("El campo FECHA INICIO LMA tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(23) IsNot Nothing Then

                        If IsDate(ImportFileRow.Row(23)) Then
                            ObjVerifyAutoliquidationFile.MaternityLeaveEndDate = ImportFileRow.Row(23).ToString ' Fecha Fin LMA
                        Else
                            ListErrors.Add("El campo FECHA FIN LMA tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(24) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.VAC = ImportFileRow.Row(24).ToString ' VAC
                    End If

                    If ImportFileRow.Row(25) IsNot Nothing Then

                        If IsDate(ImportFileRow.Row(25)) Then
                            ObjVerifyAutoliquidationFile.VacationInitialDate = ImportFileRow.Row(25).ToString ' Fecha Inicio Vacaciones
                        Else
                            ListErrors.Add("El campo FECHA INICIO VAC tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(26) IsNot Nothing Then

                        If IsDate(ImportFileRow.Row(26)) Then
                            ObjVerifyAutoliquidationFile.VacationEndDate = ImportFileRow.Row(26).ToString ' Fecha Fin Vacaciones
                        Else

                        End If

                    End If

                    If ImportFileRow.Row(27) IsNot Nothing Then

                        If IsNumeric(ImportFileRow.Row(27)) Then
                            If ObjVerifyAutoliquidationFile.BaseVAC Is Nothing Then
                                ObjVerifyAutoliquidationFile.BaseVAC = 0
                            End If
                            ObjVerifyAutoliquidationFile.BaseVAC = ImportFileRow.Row(27).ToString ' Base Vacaciones
                        Else
                            ListErrors.Add("El campo BASE VACACIONES tiene datos no válidos")
                        End If
                    End If

                    If ImportFileRow.Row(28) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.IRL = ImportFileRow.Row(28).ToString ' IRL
                    End If

                    If ImportFileRow.Row(29) IsNot Nothing Then

                        If IsDate(ImportFileRow.Row(29)) Then
                            ObjVerifyAutoliquidationFile.FechaInicioIRL = ImportFileRow.Row(29).ToString ' Fecha Inicio IRL
                        Else
                            ListErrors.Add("El campo FECHA INICIO IRL tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(30) IsNot Nothing Then

                        If IsDate(ImportFileRow.Row(30)) Then
                            ObjVerifyAutoliquidationFile.FechaFinIRL = ImportFileRow.Row(30).ToString ' Fecha Fin IRL
                        Else
                            ListErrors.Add("El campo FECHA FIN IRL tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(31) IsNot Nothing Then

                        If IsNumeric(ImportFileRow.Row(31)) Then
                            If ObjVerifyAutoliquidationFile.BaseIRL Is Nothing Then
                                ObjVerifyAutoliquidationFile.BaseIRL = 0
                            End If
                            ObjVerifyAutoliquidationFile.BaseIRL = ImportFileRow.Row(31).ToString ' Base IRL
                        Else
                            ListErrors.Add("El campo BASE IRL tiene datos no válidos")
                        End If
                    End If

                    If ImportFileRow.Row(35) IsNot Nothing Then
                        If IsNumeric(ImportFileRow.Row(35)) Then
                            ObjVerifyAutoliquidationFile.PensionDays = ImportFileRow.Row(35).ToString ' Dias Pension
                        Else
                            ListErrors.Add("El campo DIAS PENSIÓN tiene datos no válidos")
                        End If
                    End If

                    If ImportFileRow.Row(36) IsNot Nothing Then

                        If IsNumeric(ImportFileRow.Row(36)) Then
                            ObjVerifyAutoliquidationFile.HealthDays = ImportFileRow.Row(36).ToString ' Dias Salud
                        Else
                            ListErrors.Add("El campo DIAS SALUD tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(37) IsNot Nothing Then

                        If IsNumeric(ImportFileRow.Row(37)) Then
                            ObjVerifyAutoliquidationFile.ProfessionalRiskDays = ImportFileRow.Row(37).ToString ' Dias Riesgos
                        Else
                            ListErrors.Add("El campo DIAS RIESGOS tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(38) IsNot Nothing Then

                        If IsNumeric(ImportFileRow.Row(38)) Then
                            ObjVerifyAutoliquidationFile.CompensationFundDays = ImportFileRow.Row(38).ToString ' Dias Caja de Compensacion
                        Else
                            ListErrors.Add("El campo DIAS CAJAS tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(39) IsNot Nothing Then

                        If IsNumeric(ImportFileRow.Row(39)) Then
                            ObjVerifyAutoliquidationFile.BasicSalary = ImportFileRow.Row(39).ToString ' Salario Básico
                        Else
                            ListErrors.Add("El campo SALARIO BÁSICO tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(40) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.IntegralSalary = ImportFileRow.Row(40).ToString ' Integral
                    End If

                    If ImportFileRow.Row(42) IsNot Nothing Then

                        If IsNumeric(ImportFileRow.Row(42)) Then
                            ObjVerifyAutoliquidationFile.IBCPension = ImportFileRow.Row(42).ToString ' IBC Pension
                        Else
                            ListErrors.Add("El campo IBC PENSION tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(43) IsNot Nothing Then

                        If IsNumeric(ImportFileRow.Row(43)) Then
                            ObjVerifyAutoliquidationFile.IBCHealth = ImportFileRow.Row(43).ToString ' IBC Salud
                        Else
                            ListErrors.Add("El campo IBC SALUD tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(44) IsNot Nothing Then
                        If IsNumeric(ImportFileRow.Row(44)) Then
                            ObjVerifyAutoliquidationFile.IBCProfessionalRisk = ImportFileRow.Row(44).ToString ' IBC Riesgos
                        Else
                            ListErrors.Add("El campo IBC RIESGOS tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(45) IsNot Nothing Then

                        If IsNumeric(ImportFileRow.Row(45)) Then
                            ObjVerifyAutoliquidationFile.IBCCompensationFund = ImportFileRow.Row(45).ToString ' IBC Caja
                        Else
                            ListErrors.Add("El campo IBC CAJA tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(46) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.RateContributionPension = ImportFileRow.Row(46).ToString ' Tarifa Pension
                    End If

                    If ImportFileRow.Row(47) IsNot Nothing Then

                        If IsNumeric(ImportFileRow.Row(47)) Then
                            ObjVerifyAutoliquidationFile.ValuePension = ImportFileRow.Row(47).ToString ' Valor Pension
                        Else
                            ListErrors.Add("El campo VALOR PENSIÓN tiene datos no válidos")
                        End If


                    End If

                    If ImportFileRow.Row(48) IsNot Nothing Then
                        If IsNumeric(ImportFileRow.Row(48)) Then
                            ObjVerifyAutoliquidationFile.PensionSolidarityFundValueContribution = ImportFileRow.Row(48).ToString ' Subcuenta Solidaridad
                        Else
                            ListErrors.Add("El campo SUBCUENTA SOLIDARIDAD tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(49) IsNot Nothing Then
                        If IsNumeric(ImportFileRow.Row(49)) Then
                            ObjVerifyAutoliquidationFile.PensionSolidarityFundValueContributionSubsistence = ImportFileRow.Row(49).ToString ' Subcuenta Solidaridad Subsistencia
                        Else
                            ListErrors.Add("El campo SUBCUENTA SOLIDARIDAD SUBSISTENCIA tiene datos no válidos")
                        End If
                    End If

                    If ImportFileRow.Row(50) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.RateContributionHealth = ImportFileRow.Row(50).ToString ' Tarifa EPS
                    End If

                    If ImportFileRow.Row(51) IsNot Nothing Then
                        If IsNumeric(ImportFileRow.Row(51)) Then
                            ObjVerifyAutoliquidationFile.ValueHealth = ImportFileRow.Row(51).ToString ' Aporte EPS
                        Else
                            ListErrors.Add("El campo APORTE EPS tiene datos no válidos")
                        End If
                    End If

                    If ImportFileRow.Row(52) IsNot Nothing Then
                        ''REVISAR ACÁ
                        ObjVerifyAutoliquidationFile.ValueSena = ImportFileRow.Row(52).ToString ' Aporte Sena
                    End If

                    If ImportFileRow.Row(53) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.RateContributionProfessionalRisk = ImportFileRow.Row(53).ToString ' Tarifa Riesgos
                    End If

                    If ImportFileRow.Row(55) IsNot Nothing Then
                        If IsNumeric(ImportFileRow.Row(55)) Then
                            ObjVerifyAutoliquidationFile.ValueContributionProfessionalRisk = ImportFileRow.Row(55).ToString ' Aporte ARP
                        Else
                            ListErrors.Add("El campo APORTE ARP tiene datos no válidos")
                        End If
                    End If

                    If ImportFileRow.Row(56) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.RateContributorCCF = ImportFileRow.Row(56).ToString ' Tarifa CCF
                    End If

                    If ImportFileRow.Row(57) IsNot Nothing Then
                        If IsNumeric(ImportFileRow.Row(57)) Then
                            ObjVerifyAutoliquidationFile.ValueContributionCCF = ImportFileRow.Row(57).ToString ' Valor CCF
                        Else
                            ListErrors.Add("El campo Valor CCF tiene datos no válidos")
                        End If
                    End If

                    If ImportFileRow.Row(58) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.RateContributorSENA = ImportFileRow.Row(58).ToString ' Tarifa Sena
                    End If

                    If ImportFileRow.Row(59) IsNot Nothing Then

                        If IsNumeric(ImportFileRow.Row(59)) Then
                            ObjVerifyAutoliquidationFile.ValueSena = ImportFileRow.Row(59).ToString ' Valor Sena
                        Else
                            ListErrors.Add("El campo Valor SENA tiene datos no válidos")
                        End If
                    End If

                    If ImportFileRow.Row(60) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.RateContributionICBF = ImportFileRow.Row(60).ToString ' Tarfa ICBF
                    End If

                    If ImportFileRow.Row(61) IsNot Nothing Then
                        If IsNumeric(ImportFileRow.Row(61)) Then
                            ObjVerifyAutoliquidationFile.ValueICBF = ImportFileRow.Row(61).ToString ' Aporte ICBF
                        Else
                            ListErrors.Add("El campo APORTE ICBF tiene datos no válidos")
                        End If

                    End If

                    If ImportFileRow.Row(62) IsNot Nothing Then
                        ObjVerifyAutoliquidationFile.TarifaEspecialPensiones = ImportFileRow.Row(62).ToString ' Tarifa Especial Pensiones
                    End If

                    ObjVerifyAutoliquidationFile.MarkAsModified()
                    TmpListVerifyAutoliquidationFile.Add(ObjVerifyAutoliquidationFile)

                End If

            Next

            ResultObject.StateResult = True

            If ListErrors.Count > 0 Then
                ResultObject.StateResult = False
                ResultObject.MessageResult = ListErrors
                ResultObject.Message = "Se generaron errores al guardar"
            End If

            ResultObject.ObjectEmbbeded = TmpListVerifyAutoliquidationFile

        Catch ex As Exception
            ResultObject.StateResult = False
            ResultObject.MessageResult.Add(ex.Message.ToString())
        End Try

        Return ResultObject

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _autoliquidationRepository = Nothing
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
