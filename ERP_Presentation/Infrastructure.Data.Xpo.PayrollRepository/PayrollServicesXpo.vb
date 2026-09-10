'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.SecurityRepository
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-28
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
'Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports System.Configuration
#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class PayrollServicesXpo
    Inherits XpoBaseService

#Region "Fields"

    ''' <summary>
    ''' uri donde estan localizado los servicios xpo
    ''' </summary>
    Dim uriServiceEntitiesXpo As String

    ''' <summary>
    ''' protocolo utilizado para los servicios xpo
    ''' </summary>
    Dim protocolServicesXpo As Protocol

    ''' <summary>
    ''' Variable de Tipo Consultas asincronas de xpo
    ''' </summary>
    Dim serverMode As XPInstantFeedbackSource

    ''' <summary>
    ''' variable que contiene el mapeo especifo por entidad para realizar la consulta mediante xpo
    ''' </summary>
    Dim classEntity As XPClassInfo

#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase <see cref="PayrollServicesXpo" />.
    ''' </summary>
    Public Sub New(Company As String)
        'verifico que exista el archivo
        ReadConfiguration()
        'establezclo la capa de datos para XPO
        XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStore(GetEndPoint, GetRemoteAddress, Company))
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' metodo necesario para leer la configuracion xml de la aplicacion
    ''' </summary>
    Private Sub ReadConfiguration()
        'verifico que exista el archivo
        'If File.Exists(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\", FileName)) = False Then
        '    Throw New FileNotFoundException("El Archivo de Configuracion no existe!", FileName)
        'End If
        ''leo el archivo
        'Dim xmlConfiguration As New DataTable("ConfiguracionGenesis")
        'With xmlConfiguration
        '    .Columns.Add("UrlServidorWeb")
        '    .Columns.Add("UrlServidorEntidades")
        '    .Columns.Add("ProtocoloURLServidorWeb")
        '    .Columns.Add("ProtocoloURlServidorEntidades")
        '    .Columns.Add("RutaActualizacion")
        '    .Columns.Add("BuscarActulizaciones")
        '    .ReadXml(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\", FileName))
        'End With
        'cargo la uri de servicios
        uriServiceEntitiesXpo = ConfigurationFile.Instance.UrlXpoWebServer
        'cargo el protocolo
        protocolServicesXpo = ConfigurationFile.Instance.ProtocolUrlXpoWebServer
    End Sub

    ''' <summary>
    ''' funcion para contatenar el nombre del endpoint por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre de la configuracion del Endpoint Correspondiente</returns>
    Private Function GetEndPoint() As String
        Return System.String.Format("{0}_Endpoint", [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

    ''' <summary>
    ''' funcion para contatenar el remoteaddress por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre del remoteaddress del Endpoint Correspondiente</returns>
    Private Function GetRemoteAddress() As String
        Return System.String.Format("{0}XpoGate.svc/{1}", uriServiceEntitiesXpo, [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Lista las liquidaciones por periodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTradeUnion() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollTradeUnionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;PayrollConceptId", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las liquidaciones por periodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTradeUnionByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollTradeUnionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;PayrollConceptId", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las liquidaciones por periodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLiquidationByYearMont(year As Integer, month As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("GetYear([PayrollDateLiquidated]) = ? AND GetMonth([PayrollDateLiquidated]) = ? AND RegisterStatus = 'C'", year, month)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollLiquidation))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the cost center dinamic.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListConceptAccountingStructure() As XPCollection(Of ConceptAccountingStructureXpo)
        Dim collect As XPCollection(Of ConceptAccountingStructureXpo) = New XPCollection(Of ConceptAccountingStructureXpo)()
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPositionLevel() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollPositionLevelXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function
    ''' <summary>
    ''' Consulta el listado de coorporaciones
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCorporation() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollCorporationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de los niveles de educacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEducationLevels() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollEducationLevelsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de las Profesiones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProfessions() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollProfessionsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' consulta los fondos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFunds() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollFundsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;Unemployment;Health;Pension;Risk;Type", Nothing)
        Return serverMode
    End Function


    ''' <summary>
    ''' consulta las Razones de Retiro
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRetirementReason() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollRetirementReasonXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' consulta los Grupos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGroup() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;FechaUltimaLiquidacion;FechaProximaLiquidacion;Apply", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las unidades de negocio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBusinessUnit() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollBusinessUnitXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion;CustomerId.Nit;CustomerId.Name;CityId.Code;CityId.Name;CityId.DepartamentId.Code;CityId.DepartamentId.Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta todos los idiomas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLanguage() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollLanguageXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta todas las compañías
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCompany() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollCompanyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los parentescos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKinship() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollKinshipXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista El Talento Humano
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHumanTalent() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollHumanTalentXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las sucursales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBranchOffice() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollBranchOffice))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion;Address;Telephone;CompanyId.Codigo;CompanyId.Descripcion", Nothing)
        'serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion;Address;Telephone", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnit() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollFunctionalUnit))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion", Nothing)
        Return serverMode
    End Function

    Public Function ListFunctionalUnitXpCollection(ByVal status As Boolean) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(PayrollFunctionalUnit), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista los conceptos por estado y clase
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsPayroll(ByVal status As Boolean, ByVal conceptClass As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status & " and ConceptClass = '" & conceptClass & "'")
        classEntity = sessionNew.GetClassInfo(GetType(PayrollConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;State;ConceptClass;CodeName", criteria)
        Return serverMode
    End Function

    '' <summary>
    ''' Lista los conceptos por estado y clase
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsPayrollAll() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;State;ConceptClass;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las unidades funcionales por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnit(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollFunctionalUnit))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion;State;CodeDescription", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las unidades funcionales por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnitUserAuthorized(userCode As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Payroll_FunctionalUnitUsers[UserCode='" & userCode & "'] and  State=1")
        classEntity = sessionNew.GetClassInfo(GetType(PayrollFunctionalUnit))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion;State;CodeDescription", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las unidades funcionales por usuario y por los tipos de unidad requeridos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnitByUnitTypeAndUserAuthorized(unitTypes As String, userCode As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("UnitType In (" & unitTypes & ") and Payroll_FunctionalUnitUsers[UserCode='" & userCode & "'] and  State=1")
        classEntity = sessionNew.GetClassInfo(GetType(PayrollFunctionalUnit))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion;State;CodeDescription;UnitType", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los centros de costos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenter() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollCostCenterXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;CodeName", Nothing)
        Return serverMode
    End Function
    ''' <summary>
    ''' obtiene los centros de costo por estado
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenterByState(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(PayrollCostCenterXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;State;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los todos bancos 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBank() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollBankXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;BankFileCode;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los todos bancos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBankByStatus(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(PayrollBankXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;BankFileCode;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene todos los Tipos de Pensionados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPensionaryType() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollPensionaryTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene todos los Centros de Trabajo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWorkCenter() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollWorkCenterXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los tipos de estudios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStudyType() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollStudyTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las todas las ciudades con datos de departamentos y pais
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCity() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonCityXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;DepartamentId.Name;DepartamentId.Code;DepartamentId.CountryId.Name;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los Riesgos Profesionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProfessionalRisk() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollProfessionalRiskXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los Grupos de contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractGroup() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollContractGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;Name;Description", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los Centros de Estudio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStudyCenter() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollStudyCenterXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene Tipo de Contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractType() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollContractTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;JobBondingTypeId;SalaryType;AutoRenew;ContractClass;SeveranceType", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los tipso de contribuyentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContributorType() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollContributorTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las Plantillas de Contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractTemplate() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollContractTemplateXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las Retenciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRetention() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollRetentionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;Consecutive;Year;Rate", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los cargos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPosition() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollPositionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las Incapacidades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInability() As XPInstantFeedbackSource
        'Dim sessionNew = New Session(XpoDefault.DataLayer)
        'classEntity = sessionNew.GetClassInfo(GetType(PayrollInabilityXpo))
        'serverMode = New XPInstantFeedbackSource(classEntity, "Codigo", Nothing)
        'Return serverMode
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene los Conceptos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConcept() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene todos los Tipos de Vinculación Laboral
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetJobBondingType() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollJobBondingTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' consulta los Grupos que tiene una empresa
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGroupByCompany(companyId As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CompanyId=" & companyId & "")
        classEntity = sessionNew.GetClassInfo(GetType(PayrollGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las sucursales por empresa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBranchOfficeByCompany(companyId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CompanyId=" & companyId & "")
        classEntity = sessionNew.GetClassInfo(GetType(PayrollBranchOffice))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion;Address;Telephone;CompanyId.Codigo;CompanyId.Descripcion", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las unidades funcionales por sucursal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnitByBranchOffice(branchOfficeId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BranchOfficeId=" & branchOfficeId)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollFunctionalUnit))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene todas las plantillas de turno
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleTemplate() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollScheduleTemplateXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;Name;Letter", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las incapacidades que tengan un empleado y filtra si estan liquidados o no
    ''' </summary>
    ''' <param name="employeeId">id empleado</param>
    ''' <param name="status">estado de la novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInabilityLiquidate(employeeId As Integer, status As Integer) As XPInstantFeedbackSource
        'Dim xpCollection1 As XPCollection(Of PayrollNoveltyXpo) = New XPCollection(Of PayrollNoveltyXpo)
        'Dim sorting As SortingCollection = New SortingCollection()
        'sorting.Add(New SortProperty("Consecutive", DevExpress.Xpo.DB.SortingDirection.Ascending))
        'sorting.Add(New SortProperty("RealDate", DevExpress.Xpo.DB.SortingDirection.Ascending))getco
        'xpCollection1.Sorting = sortingget
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EmployeeId=" & employeeId & " AND Status =" & status)
        'xpCollection1.Filter = criteria
        'xpCollection1.Session.DropIdentityMap()
        'xpCollection1.Reload()
        ''For i As Integer = 0 To xpCollection1.Count - 1
        ''    xpCollection1(i).Reload()
        ''Next
        'Return xpCollection1
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EmployeeId=" & employeeId & " AND Status =" & status)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollNoveltyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Consecutive;Reason;TypeNovelty;RealDate;EndDate;Extension;Value;LiquidationBase;LicenseClass;InabilityClass;NoveltyLiquidate", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las incapacidades que tengan un empleado y filtra si estan liquidados o no
    ''' </summary>
    ''' <param name="employeeId">id empleado</param>
    ''' <param name="typeNovelty">tipo de novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyLiquidateByType(employeeId As Integer, typeNovelty As Byte) As XPInstantFeedbackSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EmployeeId=" & employeeId & " AND TypeNovelty =" & typeNovelty & " AND Status <> 0")
        classEntity = sessionNew.GetClassInfo(GetType(PayrollNoveltyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Consecutive;Reason;TypeNovelty;RealDate;EndDate;Extension;Value;LiquidationBase;LicenseClass;InabilityClass;NoveltyLiquidate", criteria)
        Return serverMode
       
        'Dim xpCollection1 As XPCollection(Of PayrollNoveltyXpo) = New XPCollection(Of PayrollNoveltyXpo)
        'Dim sorting As SortingCollection = New SortingCollection()
        'sorting.Add(New SortProperty("Consecutive", DevExpress.Xpo.DB.SortingDirection.Ascending))
        'sorting.Add(New SortProperty("RealDate", DevExpress.Xpo.DB.SortingDirection.Ascending))
        'xpCollection1.Sorting = sorting
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EmployeeId=" & employeeId & " AND TypeNovelty =" & typeNovelty & " AND Status <> 0")
        'xpCollection1.Filter = criteria
        'xpCollection1.Session.DropIdentityMap()
        'xpCollection1.Reload()
        ''For i As Integer = 0 To xpCollection1.Count - 1
        ''    xpCollection1(i).Reload()
        ''Next
        'Return xpCollection1

    End Function

    ''' <summary>
    ''' Obtiene todos los tipos de empleado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeType() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollEmployeeTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;State", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene todas las plantillas de turno
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractModificationReason() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollContractModificationReasonXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Description;State", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' consulta los Grupos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGroupByContractClass(contractClass As Byte) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ContractClass=" & contractClass)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;FechaUltimaLiquidacion;FechaProximaLiquidacion;CompanyId", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnitByCompany(companyId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BranchOfficeId.CompanyId=" & companyId)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollFunctionalUnit))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene Tipo de Contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractTypeByContractClass(ByVal ParamArray contractClass() As String) As XPInstantFeedbackSource
        Dim StrCriteria As String = ""
        For index = 0 To UBound(contractClass)
            StrCriteria &= " ContractClass = " & contractClass(index).ToString()
            If index <> UBound(contractClass) Then
                StrCriteria &= " OR "
            End If
        Next
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(StrCriteria)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollContractTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;JobBondingTypeId;SalaryType;AutoRenew;ContractClass;SeveranceType", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las clases de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListKindsAgreements() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollKindsAgreementsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;Description", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene una lista de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAgreements() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollAgreementsCXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Consecutive;EmployeeId.ThirdPartyId.Nit;EmployeeId.ThirdPartyId.Name;CompanyId.Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene una lista de conceptos manuales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListManualConcepts() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollManualConceptsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Consecutive;EmployeeId.ThirdPartyId.Nit;EmployeeId.ThirdPartyId.Name;ConceptId.Descripcion;QuoteValue;PaidFormat;PaidEndContract;Description;InitialDate;State", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta todos los idiomas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountingStructure() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollAccountingStructureXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los empleados con el contrato activo
    ''' </summary>
    Public Function ListEmployeeWithActiveContract() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PayrollContracts[Status=1] And PayrollContracts[Valid=True]")
        classEntity = sessionNew.GetClassInfo(GetType(PayrollEmployeeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode

        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PayrollContracts[Status=1] And PayrollContracts[Valid=True]")
        'Dim collect As XPCollection(Of PayrollEmployeeXpo) = New XPCollection(Of PayrollEmployeeXpo)(New Session(XpoDefault.DataLayer), criteria)
        'collect.Load()
        'Return collect
    End Function

    ''' <summary>
    ''' Obtiene una unidad funcional por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFunctionalUnitById(ByVal IdFunctional As Integer) As XPCollection(Of PayrollFunctionalUnit)
        Dim collect As XPCollection(Of PayrollFunctionalUnit) = New XPCollection(Of PayrollFunctionalUnit)()
        Return collect
    End Function

    ''' <summary>
    ''' Obtiene una unidad funcional por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetLiquidationPayroll(ByVal IdGroup As Integer, ByVal PayrollDateLiquidated As Date) As XPCollection

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("GroupId = " & IdGroup & " and PayrollDateLiquidated = " & PayrollDateLiquidated)

        Dim collection = New XPCollection(GetType(PayrollLiquidation), criteria)
        Return collection
    End Function


#Region "GetEmployee LinqInstantFeedbackSpurce"
    Private WithEvents vlinq As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployee() As LinqInstantFeedbackSource
        vlinq.KeyExpression = "Id"
        Return vlinq
    End Function

    Private Sub OnGetQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinq.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableEmployee As XPQuery(Of PayrollEmployeeXpo) = New XPQuery(Of PayrollEmployeeXpo)(sessionNew)
            Dim tableContract As XPQuery(Of PayrollContractXpo) = New XPQuery(Of PayrollContractXpo)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableEmployee
                                     Join T2 In tableContract On T1.Id Equals T2.EmployeeId.Id
                                     Where T1.Id = T2.EmployeeId.Id And T2.Valid = True
                                     Select T1.Id, T1.ThirdPartyId.Nit, T1.ThirdPartyId.Name, T2.GroupId.Descripcion, GroupId = T2.GroupId.Id
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableEmployee

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DismissQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinq.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "GetEmployee ByFunctionalUnit or Group LinqInstantFeedbackSpurce"
    Private WithEvents vlinq1 As New LinqInstantFeedbackSource
    Dim _functionalUnitId As Integer
    Dim _groupId As Integer

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeByFunctionalUnitIdOrGroupId(Optional functionalUnitId As Integer = 0, Optional groupId As Integer = 0) As LinqInstantFeedbackSource
        _functionalUnitId = functionalUnitId
        _groupId = groupId
        vlinq1.KeyExpression = "Id"
        Return vlinq1
    End Function

    Private Sub OnGetQueryable1(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinq1.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableEmployee As XPQuery(Of PayrollEmployeeXpo) = New XPQuery(Of PayrollEmployeeXpo)(sessionNew)
            Dim tableContract As XPQuery(Of PayrollContractXpo) = New XPQuery(Of PayrollContractXpo)(sessionNew)


            If _functionalUnitId > 0 Then
                Dim TmpQueryableSource = From T1 In tableEmployee
                                     Join T2 In tableContract On T1.Id Equals T2.EmployeeId.Id
                                     Where T1.Id = T2.EmployeeId.Id And T2.Valid = True And T2.FunctionalUnitId = _functionalUnitId
                                     Select T1.Id, T1.ThirdPartyId.Nit, T1.ThirdPartyId.Name, Check = False, T2.GroupId.Descripcion, GroupId = T2.GroupId.Id

                Dim prueba = TmpQueryableSource.ToList
                e.QueryableSource = TmpQueryableSource
                e.Tag = tableEmployee
            ElseIf _groupId > 0 Then
                Dim TmpQueryableSource = From T1 In tableEmployee
                                         Join T2 In tableContract On T1.Id Equals T2.EmployeeId.Id
                                         Where T1.Id = T2.EmployeeId.Id And T2.Valid = True And T2.GroupId.Id = _groupId
                                         Select T1.Id, T1.ThirdPartyId.Nit, T1.ThirdPartyId.Name, Check = False, T2.GroupId.Descripcion, GroupId = T2.GroupId.Id

                Dim prueba = TmpQueryableSource.ToList
                e.QueryableSource = TmpQueryableSource
                e.Tag = tableEmployee

            Else
                Dim TmpQueryableSource = From T1 In tableEmployee
                                     Join T2 In tableContract On T1.Id Equals T2.EmployeeId.Id
                                     Where T1.Id = T2.EmployeeId.Id And T2.Valid = True
                                     Select T1.Id, T1.ThirdPartyId.Nit, T1.ThirdPartyId.Name, Check = False, T2.GroupId.Descripcion, GroupId = T2.GroupId.Id

                Dim prueba = TmpQueryableSource.ToList
                e.QueryableSource = TmpQueryableSource
                e.Tag = tableEmployee
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub DismissQueryable1(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinq1.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "GetAllEmployees"
    Private WithEvents vlinqEmployees As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllEmployees() As LinqInstantFeedbackSource
        vlinqEmployees.KeyExpression = "Id"
        Return vlinqEmployees
    End Function

    Private Sub OnGetQueryableEmployee(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqEmployees.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableEmployee As XPQuery(Of PayrollEmployeeXpo) = New XPQuery(Of PayrollEmployeeXpo)(sessionNew)
            Dim tableContract As XPQuery(Of PayrollContractXpo) = New XPQuery(Of PayrollContractXpo)(sessionNew)

            Dim TmpQueryableSource = From T1 In tableEmployee
                                     Join T2 In tableContract On T1.Id Equals T2.EmployeeId.Id
                                     Where T2.Valid = True
                                     Select T1.Id, T1.ThirdPartyId.Nit, T1.ThirdPartyId.Name, T2.GroupId.Descripcion

            Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableEmployee

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DismissQueryableEmployee(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqEmployees.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region


#Region "GetAllEmployeesWithContratToLiquidate"
    Private WithEvents vlinqEmployeesWithContratToLiquidate As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllEmployeesWithContratToLiquidate() As LinqInstantFeedbackSource
        vlinqEmployeesWithContratToLiquidate.KeyExpression = "Id"
        Return vlinqEmployeesWithContratToLiquidate
    End Function

    Private Sub OnGetQueryableEmployeeWithContratToLiquidate(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqEmployeesWithContratToLiquidate.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableEmployee As XPQuery(Of PayrollEmployeeXpo) = New XPQuery(Of PayrollEmployeeXpo)(sessionNew)
            Dim tableContract As XPQuery(Of PayrollContractXpo) = New XPQuery(Of PayrollContractXpo)(sessionNew)
            Dim tableContractType As XPQuery(Of PayrollContractTypeXpo) = New XPQuery(Of PayrollContractTypeXpo)(sessionNew)

            Dim TmpQueryableSource = From T1 In tableEmployee
                                     Join T2 In tableContract On T1.Id Equals T2.EmployeeId.Id
                                     Join T3 In tableContractType On T2.ContractTypeId Equals T3.Id
                                     Where T2.Valid = True And (T3.ContractClass = CByte(3) Or T3.ContractClass = CByte(4)) And T2.RetirementDate.ToString() Is Nothing
                                     Select T1.Id, T1.ThirdPartyId.Nit, T1.ThirdPartyId.Name, T2.GroupId.Descripcion

            Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableEmployee

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DismissQueryableEmployeeWithContractToLiquidate(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqEmployeesWithContratToLiquidate.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region



#End Region

#Region "Coleccion Reportes"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        Return result
    End Function

#End Region

End Class
