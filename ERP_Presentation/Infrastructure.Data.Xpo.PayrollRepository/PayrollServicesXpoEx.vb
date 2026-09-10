'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.CrystalRepository
' Author           : Juan F. Tamayo
' Created          : 2016-02-08
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Xpo.Base
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.DB.Helpers
Imports DevExpress.Xpo.Helpers
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Linq
Imports DevExpress.Data.Filtering

#End Region

Public Class PayrollServicesXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"

    Public Function ListCostCenterDinamicByListId(listId As List(Of Integer)) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollCostCenterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State = 1 and Id in (" & String.Join(",", listId.ToArray) & ")")
            Dim classEntity = session.GetClassInfo(GetType(PayrollCostCenterXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;State;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las vacaciones del empleado
    ''' </summary>
    ''' <param name="EmployeeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListVacationsByEmployeeId(EmployeeId As Integer) As List(Of PayrollVacation)
        Dim session As New IndigoXPOSession(Of PayrollVacationPeriod)()
        Dim tableVacationPeriod As XPQuery(Of PayrollVacationPeriod) = New XPQuery(Of PayrollVacationPeriod)(session)
        Dim tableVacation As XPQuery(Of PayrollVacation) = New XPQuery(Of PayrollVacation)(session)
        Dim TmpQueryableSource = From vp In tableVacationPeriod
                                 Join v In tableVacation On v.VacationPeriodId.Id Equals vp.Id
                                 Where vp.EmployeeId.Id = EmployeeId
                                 Select v
        Return TmpQueryableSource.ToList
    End Function

    ''' <summary>
    ''' Lista las liquidaciones por periodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTradeUnion() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollTradeUnionXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollTradeUnionXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;PayrollConceptId;IdThirdParty", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista las liquidaciones por periodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTradeUnionByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollTradeUnionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status)
            Dim _classEntity = session.GetClassInfo(GetType(PayrollTradeUnionXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;PayrollConceptId;IdThirdParty", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista las liquidaciones por periodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLiquidationByYearMont(year As Integer, month As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollLiquidation)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("GetYear([PayrollDateLiquidated]) = ? AND GetMonth([PayrollDateLiquidated]) = ? AND RegisterStatus = 'C'", year, month)
            Dim _classEntity = session.GetClassInfo(GetType(PayrollLiquidation))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    Public Function GetCollectionLiquidationByYearMont(year As Integer, month As Integer) As XPCollection(Of PayrollLiquidation)
        Dim session As New IndigoXPOSession(Of PayrollLiquidation)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("GetYear([PayrollDateLiquidated]) = ? AND GetMonth([PayrollDateLiquidated]) = ? AND RegisterStatus = 'C'", year, month)
        Dim collect As XPCollection(Of PayrollLiquidation) = New XPCollection(Of PayrollLiquidation)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Obtiene la fecha de la ultima liquidacion de nomina confirmada de un empleado
    ''' </summary>
    ''' <returns></returns>
    Public Function GetLastEmployeeDateLiquidated(GroupId As Integer, EmployeeId As Integer) As Date?
        Dim session As New IndigoXPOSession(Of PayrollLiquidationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("GroupId = ? AND EmployeeId = ? AND RegisterStatus = 'C'", GroupId, EmployeeId)
        Dim sortProperty = New SortProperty("PayrollDateLiquidated", DevExpress.Xpo.DB.SortingDirection.Descending)
        Dim collect = New XPCollection(Of PayrollLiquidation)(session, criteria, sortProperty)
        Return collect.FirstOrDefault()?.PayrollDateLiquidated
    End Function

    ''' <summary>
    ''' Lists the cost center dinamic.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListConceptAccountingStructure() As XPCollection(Of ConceptAccountingStructureXpo)
        Dim session As New IndigoXPOSession(Of ConceptAccountingStructureXpo)()
        Dim collect As XPCollection(Of ConceptAccountingStructureXpo) = New XPCollection(Of ConceptAccountingStructureXpo)(session)
        Return collect
        'End Using
    End Function

    Function ListConceptAccountingStructureByStructureIds(structuresId As List(Of Integer)) As XPCollection(Of ConceptAccountingStructureXpo)
        Dim session As New IndigoXPOSession(Of ConceptAccountingStructureXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[AccountingStructureId] in (" & String.Join(",", structuresId.ToArray) & ")")
        Dim collect As XPCollection(Of ConceptAccountingStructureXpo) = New XPCollection(Of ConceptAccountingStructureXpo)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Listado de conceptos de conciliación bancaria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBankConciliationConcept() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BankConciliationConceptXpo)
        Dim _classEntity = session.GetClassInfo(GetType(BankConciliationConceptXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;StatusName", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' obtiene un concepto de conciliación bancaria    
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBankConciliationConceptById(id As Integer) As BankConciliationConceptXpo
        Dim session As New IndigoXPOSession(Of BankConciliationConceptXpo)()
        Return session.GetObjectByKey(Of BankConciliationConceptXpo)(id)
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPositionLevel() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollPositionLevelXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollPositionLevelXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function
    ''' <summary>
    ''' Consulta el listado de coorporaciones
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCorporation() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollCorporationXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollCorporationXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de los niveles de educacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEducationLevels() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollEducationLevelsXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollEducationLevelsXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de las Profesiones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProfessions() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollProfessionsXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollProfessionsXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Consulta los cambios hechos en los contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListContractAuditByEmployeeId(Id As Integer) As XPCollection(Of ViewContractAuditXpo)
        Dim session As New IndigoXPOSession(Of ViewContractAuditXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EmployeeId = ?", Id)
        Return New XPCollection(Of ViewContractAuditXpo)(session, criteria)
    End Function

    ''' <summary>
    ''' consulta los fondos por tipo de fondo
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFundsByTypeFund(TypeFund As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollFundsXpo)()
        Dim criteri As CriteriaOperator
            criteri = Nothing

            If TypeFund = 1 Then
                'salud
                criteri = CriteriaOperator.Parse("Health = 1")
            ElseIf TypeFund = 2 OrElse TypeFund = 5 OrElse TypeFund = 6 Then
                'pensión - solidaridad - Pensión – Fondo de Solidaridad
                criteri = CriteriaOperator.Parse("Pension = 1")
            ElseIf TypeFund = 3 OrElse TypeFund = 7 Then
                'cesantias - Prima
                criteri = CriteriaOperator.Parse("Unemployment = 1")
            ElseIf TypeFund = 4 Then
                'riesgos
                criteri = CriteriaOperator.Parse("Risk = 1")
            End If

            Dim _classEntity = session.GetClassInfo(GetType(PayrollFundsXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;Unemployment;Health;Pension;Risk;Type;ThirdPartyId", criteri)
        Return _serverMode
    End Function

    ''' <summary>
    ''' consulta los fondos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFunds() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollFundsXpo)
        Dim _classEntity = session.GetClassInfo(GetType(PayrollFundsXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;Unemployment;Health;Pension;Risk;Type", Nothing)
        Return _serverMode
    End Function


    ''' <summary>
    ''' consulta las Razones de Retiro
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRetirementReason() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollRetirementReasonXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollRetirementReasonXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' consulta los Grupos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGroup() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollGroupXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollGroupXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;FechaUltimaLiquidacion;FechaProximaLiquidacion;Apply;PayrollParameterId", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Consulta las unidades de negocio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBusinessUnit() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollBusinessUnitXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollBusinessUnitXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Codigo;Descripcion;CustomerId.Nit;CustomerId.Name;CityId.Code;CityId.Name;CityId.DepartamentId.Code;CityId.DepartamentId.Name", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Consulta todos los idiomas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLanguage() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollLanguageXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollLanguageXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Consulta todas las compañías
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCompany() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollCompanyXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollCompanyXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function
    ''' <summary>
    ''' Consulta la compañía de la session.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCompanySession(NitCompany As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollCompanyXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Codigo = " & NitCompany)
        Dim _classEntity = session.GetClassInfo(GetType(PayrollCompanyXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista todos los parentescos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKinship() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollKinshipXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollKinshipXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista El Talento Humano
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHumanTalent() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollHumanTalentXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollHumanTalentXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las sucursales donde la compañia no sea nula
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBranchOffice() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollBranchOffice)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CompanyId.Id is not null")
        Dim _classEntity = session.GetClassInfo(GetType(PayrollBranchOffice))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;CodeName;Address;Telephone;CompanyId.Codigo;CompanyId.Descripcion;CompanyId.Id", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las sucursales donde la compañia sean nulas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBranchOfficeByCompanyIsNull() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollBranchOffice)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CompanyId.Id is null")
            Dim _classEntity = session.GetClassInfo(GetType(PayrollBranchOffice))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;Address;Telephone;CompanyId.Codigo;CompanyId.Descripcion;CompanyId.Id", criteria)
        'serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion;Address;Telephone", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las sucursales donde la compañia sean nulas y por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBranchOfficeByCompanyIsNullAndState(state As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollBranchOffice)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollBranchOffice))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;Address;Telephone;CompanyId.Codigo;CompanyId.Descripcion;CompanyId.Id;State", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnit() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollFunctionalUnit))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;CodeDescription;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion", Nothing)
        Return _serverMode
    End Function

    Function GetFunctionalUnitByCostCenterId(idCostCenter As List(Of Integer)) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CostCenterId.Id in (" & String.Join(",", idCostCenter.ToArray) & ")")
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollFunctionalUnit))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion;CodeDescription", criteria)
        Return _serverMode
    End Function

    Public Function ListFunctionalUnitXpCollection(ByVal status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status)
        Dim collect As XPCollection = New XPCollection(session, GetType(PayrollFunctionalUnit), criteria)
        Return collect
        'End Using
    End Function

    Public Function GetFunctionalUnitXpo(code As String) As PayrollFunctionalUnit
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Codigo='" & code & "'")
        Return session.FindObject(Of PayrollFunctionalUnit)(criteria)
    End Function

    ''' <summary>
    ''' Lista los conceptos por estado y clase
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsPayroll(ByVal status As Boolean, ByVal conceptClass As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status & " and ConceptClass = '" & conceptClass & "'")
            Dim _classEntity = session.GetClassInfo(GetType(PayrollConceptXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;State;ConceptClass;CodeName", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista los conceptos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsPayrollByStatus(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollConceptXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State = " & status)
            Dim _classEntity = session.GetClassInfo(GetType(PayrollConceptXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;State;ConceptClass;CodeName", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista los conceptos por estado y clase
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsPayroll() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollConceptXpo)
        Dim _classEntity = session.GetClassInfo(GetType(PayrollConceptXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;State;ConceptClass;CodeName", Nothing)
        Return _serverMode
    End Function


    ''' <summary>
    ''' Lista los conceptos (Solo muestra los diferentes a los de tipo 3(Patronales))
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsPayrollOnlyAccruedAndDeducted() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollConceptXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ConceptType <> 3")
            Dim _classEntity = session.GetClassInfo(GetType(PayrollConceptXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;State;ConceptClass;CodeName", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' lista las unidades funcionales por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnit(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status)
        Dim _classEntity = session.GetClassInfo(GetType(PayrollFunctionalUnit))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion;State;CodeDescription", criteria)
        Return _serverMode
    End Function

    Public Function ListFunctionalUnitByUser(ByVal IdUser As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Payroll_FunctionalUnitResponsible[UserId=" & IdUser & "]")
        Dim _classEntity = session.GetClassInfo(GetType(PayrollFunctionalUnit))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion;State;CodeDescription", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' obtiene las unidades funcionales a las cuales el usuario esta saignado y que etsas mismas se encuentren activas
    ''' </summary>
    ''' <param name="IdUser"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function ListFunctionalUnitAuthorizedByUser(ByVal IdUser As Integer, userCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Payroll_FunctionalUnitUsers[UserId=" & IdUser & "] and Payroll_FunctionalUnitUsers[UserCode='" & userCode & "'] and  State=1")
        Dim _classEntity = session.GetClassInfo(GetType(PayrollFunctionalUnit))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion;State;CodeDescription", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' lista las unidades funcionales por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnitUserAuthorized(userCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Payroll_FunctionalUnitUsers[UserCode='" & userCode & "'] and  State=1")
            Dim _classEntity = session.GetClassInfo(GetType(PayrollFunctionalUnit))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion;State;CodeDescription", criteria)
        Return _serverMode
    End Function

    Public Function ListBillingJustificationControl() As Object
        Throw New NotImplementedException()
    End Function

    ''' <summary>
    ''' lista las unidades funcionales por usuario y por los tipos de unidad requeridos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnitByUnitTypeAndUserAuthorized(unitTypes As String, userCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("UnitType In (" & unitTypes & ") and Payroll_FunctionalUnitUsers[UserCode='" & userCode & "'] and  State=1 ")
            Dim _classEntity = session.GetClassInfo(GetType(PayrollFunctionalUnit))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion;State;CodeDescription;UnitType", criteria)
        Return _serverMode
    End Function
    ''' <summary>
    ''' lista las unidades funcionales por usuario y por los tipos de unidad requeridos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnitByUnitTypeCareCenterUserAuthorized(careCenterCode As String, unitTypes As String, userCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollViewFunctionalUnitCareCenterUserXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("UnitType in (" & unitTypes & ") and UserCode='" & userCode & "' and  CODCENATE='" & careCenterCode & "'")
        Dim _classentity = session.GetClassInfo(GetType(PayrollViewFunctionalUnitCareCenterUserXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classentity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' lista las unidades funcionales por usuario y por los tipos de unidad requeridos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllFunctionalUnitByUnitTypeCareCenterUserAuthorized(careCenterCode As String, userCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollViewFunctionalUnitCareCenterUserXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("UserCode='" & userCode & "' and  CODCENATE='" & careCenterCode & "'")
        Dim _classentity = session.GetClassInfo(GetType(PayrollViewFunctionalUnitCareCenterUserXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classentity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene los centros de costos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenter() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollCostCenterXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollCostCenterXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;CodeName", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene los Conceptos de nómina electrónica
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetElectronicPayrollConcepts() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollElectronicPayrollConceptsXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollElectronicPayrollConceptsXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Code;Name;ConceptTypeName;StateName", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' obtiene los centros de costo por estado
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenterByState(ByVal status As Boolean, Optional Ids As String = Nothing) As XPInstantFeedbackSource
        Dim filter = $"State={status}"

        If Not String.IsNullOrEmpty(Ids) Then
            filter &= $" AND Id IN ({Ids})"
        End If

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
        Dim session As New IndigoXPOSession(Of PayrollCostCenterXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PayrollCostCenterXpo)), "Id;Codigo;Descripcion;State;CodeName", criteria)
    End Function

    ''' <summary>
    ''' Lista los centros de costo que no estén incluidos en otros centros de producción
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostCenterByIds(ListIds As List(Of Integer)) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollCostCenterXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("not Id in (" & String.Join(",", ListIds.ToArray) & ")")
            Dim classEntity = session.GetClassInfo(GetType(PayrollCostCenterXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;State;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los todos bancos 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBank() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollBankXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollBankXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;BankFileCode;CodeName", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene los todos bancos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBankByStatus(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollBankXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status & "")
            Dim _classEntity = session.GetClassInfo(GetType(PayrollBankXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;BankFileCode;CodeName", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene todos los Tipos de Pensionados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPensionaryType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollPensionaryTypeXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollPensionaryTypeXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene todos los Centros de Trabajo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWorkCenter() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollWorkCenterXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollWorkCenterXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene los tipos de estudios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStudyType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollStudyTypeXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollStudyTypeXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las todas las ciudades con datos de departamentos y pais
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCity() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonCityXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonCityXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;DepartamentId.Name;DepartamentId.Code;DepartamentId.CountryId.Name;CodeName", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene los Riesgos Profesionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProfessionalRisk() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollProfessionalRiskXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollProfessionalRiskXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene los Grupos de contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractGroup() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollContractGroupXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollContractGroupXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Code;Name;Description", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene los Centros de Estudio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStudyCenter() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollStudyCenterXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollStudyCenterXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene Tipo de Contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollContractTypeXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollContractTypeXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;JobBondingTypeId;SalaryType;AutoRenew;ContractClass;SeveranceType", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene los tipso de contribuyentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContributorType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollContributorTypeXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollContributorTypeXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las Plantillas de Contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractTemplate() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollContractTemplateXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollContractTemplateXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las Retenciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRetention() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollRetentionXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollRetentionXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Code;Consecutive;Year;Rate", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene los cargos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPosition() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollPositionXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollPositionXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;CodeName", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las Incapacidades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInability() As XPInstantFeedbackSource
        'Dim session = New Session(XpoDefault.DataLayer)
        'classEntity = session.GetClassInfo(GetType(PayrollInabilityXpo))
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
        Dim session As New IndigoXPOSession(Of PayrollConceptXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollConceptXpo))
        'Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Codigo;Descripcion", Nothing)
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;Tipo;ConceptClassName", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene todos los Tipos de Vinculación Laboral
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetJobBondingType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollJobBondingTypeXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollJobBondingTypeXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' consulta los Grupos que tiene una empresa
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGroupByCompany(companyId As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CompanyId=" & companyId & "")
            Dim _classEntity = session.GetClassInfo(GetType(PayrollGroupXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Codigo;Descripcion", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las sucursales por empresa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBranchOfficeByCompany(companyId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollBranchOffice)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CompanyId.Id = " & companyId & "")
            Dim _classEntity = session.GetClassInfo(GetType(PayrollBranchOffice))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Codigo;Descripcion;Address;Telephone;CompanyId.Codigo;CompanyId.Descripcion;CompanyId.Id", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las unidades funcionales por sucursal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnitByBranchOffice(branchOfficeId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BranchOfficeId=" & branchOfficeId)
            Dim _classEntity = session.GetClassInfo(GetType(PayrollFunctionalUnit))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene todas las plantillas de turno
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetScheduleTemplate() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollScheduleTemplateXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollScheduleTemplateXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Code;Name;Letter", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las incapacidades que tengan un empleado y filtra si estan liquidados o no
    ''' </summary>
    ''' <param name="employeeId">id empleado</param>
    ''' <param name="status">estado de la novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInabilityLiquidate(employeeId As Integer, status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollNoveltyXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EmployeeId=" & employeeId & " AND Status =" & status)
        Dim _classEntity = session.GetClassInfo(GetType(PayrollNoveltyXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Consecutive;Reason;TypeNovelty;RealDate;EndDate;Extension;Value;LiquidationBase;LiquidationBasePatrono;LicenseClass;InabilityClass;NoveltyLiquidate;ResolutionNumber;ResolutionDate;Days;EmployerDays;EPSDays;EPSRecognizeValue;PaidEmployerValue", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las incapacidades que tengan un empleado y filtra si estan liquidados o no
    ''' </summary>
    ''' <param name="employeeId">id empleado</param>
    ''' <param name="typeNovelty">tipo de novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNoveltyLiquidateByType(employeeId As Integer, typeNovelty As Byte) As XPInstantFeedbackSource

        Dim session As New IndigoXPOSession(Of PayrollNoveltyXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EmployeeId=" & employeeId & " AND TypeNovelty =" & typeNovelty & " AND Status <> 0")
        Dim _classEntity = session.GetClassInfo(GetType(PayrollNoveltyXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Consecutive;Reason;TypeNovelty;RealDate;EndDate;Extension;Value;LiquidationBase;LicenseClass;InabilityClass;NoveltyLiquidate;ResolutionNumber;ResolutionDate;Days", criteria)
        Return _serverMode

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
    ''' Retorna las incapacidades de un empleado en un rango de fechas
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="startDate"></param>
    ''' <param name="endDate"></param>
    ''' <returns></returns>
    Public Function GetInabilitiesByDate(employeeId As Integer, startDate As Date, endDate As Date) As XPCollection(Of PayrollNoveltyXpo)
        Dim session As New IndigoXPOSession(Of PayrollNoveltyXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EmployeeId = ? AND RealDate >= ? AND RealDate <= ? AND TypeNovelty = 1", employeeId, startDate, endDate)
        Dim collection As New XPCollection(Of PayrollNoveltyXpo)(session, criteria)
        Return collection
    End Function

    ''' <summary>
    ''' Obtenemos la ultima fecha de inicio del ciclo para postular descuento.
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <returns></returns>
    Public Function GetLastCycleStartDate(employeeId As Integer, initialDate As Date) As DateTime?
        Dim session As New IndigoXPOSession(Of PayrollNoveltyXpo)()
        Dim startDate As New Date(initialDate.Year, initialDate.Month - 1, 1)
        Dim endDate As Date = startDate.AddMonths(2).AddDays(-1)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EmployeeId = ? AND RealDate >= ? AND RealDate <= ? AND TypeNovelty = 1 AND IsCycleDate = True", employeeId, startDate, endDate)
        Dim sortProperties As New SortingCollection(New SortProperty("RealDate", DevExpress.Xpo.DB.SortingDirection.Descending))
        Dim collection As New XPCollection(Of PayrollNoveltyXpo)(session, criteria) With {
            .Sorting = sortProperties,
            .TopReturnedObjects = 1}

        If collection?.Count > 0 Then
            Return collection(0).RealDate
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene todos los tipos de empleado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollEmployeeTypeXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollEmployeeTypeXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;State", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene todas las plantillas de turno
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractModificationReason() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollContractModificationReasonXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollContractModificationReasonXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Description;State", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' consulta los Grupos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGroupByContractClass(contractClass As Byte) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ContractClass=" & contractClass)
            Dim _classEntity = session.GetClassInfo(GetType(PayrollGroupXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;FechaUltimaLiquidacion;FechaProximaLiquidacion;CompanyId", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnitByCompany(companyId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("BranchOfficeId.CompanyId=" & companyId)
            Dim _classEntity = session.GetClassInfo(GetType(PayrollFunctionalUnit))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;BranchOfficeId.Codigo;BranchOfficeId.Descripcion;BranchOfficeId.CompanyId.Codigo;BranchOfficeId.CompanyId.Descripcion;CostCenterId.Id;CostCenterId.Codigo;CostCenterId.Descripcion", criteria)
        Return _serverMode
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
        Dim session As New IndigoXPOSession(Of PayrollContractTypeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(StrCriteria)
            Dim _classEntity = session.GetClassInfo(GetType(PayrollContractTypeXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;JobBondingTypeId;SalaryType;AutoRenew;ContractClass;SeveranceType", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene las clases de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListKindsAgreements() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollKindsAgreementsXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollKindsAgreementsXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Code;Description", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene una lista de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAgreements() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollAgreementsCXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollAgreementsCXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Consecutive;EmployeeId.ThirdPartyId.Nit;EmployeeId.ThirdPartyId.Name;CompanyId.Descripcion;State", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene una lista de conceptos manuales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListManualConcepts() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollManualConceptsXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollManualConceptsXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Consecutive;EmployeeId.ThirdPartyId.Nit;EmployeeId.ThirdPartyId.Name;ConceptId.Descripcion;QuoteValue;PaidFormat;PaidEndContract;Description;InitialDate;State;Estado", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Consulta todos los idiomas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountingStructure() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollAccountingStructureXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollAccountingStructureXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista todos los empleados con el contrato activo
    ''' </summary>
    Public Function ListEmployeeWithActiveContract() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollEmployeeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PayrollContracts[Status=1] or PayrollContracts[Status=1] And PayrollContracts[Valid=True]")
        Dim classEntity = session.GetClassInfo(GetType(PayrollEmployeeXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los empleados con el contrato activo
    ''' </summary>
    Public Function ListEmployeeByPosition(positionId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollContractXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = 1 or Status = 5 and Valid = 1 and PositionId.Id = " & positionId)
        Dim classEntity = session.GetClassInfo(GetType(PayrollContractXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "EmployeeId.Id;EmployeeId.ThirdPartyNitName;EmployeeId.ThirdPartyId.Nit;EmployeeId.ThirdPartyId.Name", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene una unidad funcional por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFunctionalUnitById(ByVal IdFunctional As Integer) As XPCollection(Of PayrollFunctionalUnit)
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)()
        Dim collect As XPCollection(Of PayrollFunctionalUnit) = New XPCollection(Of PayrollFunctionalUnit)(session)
        Return collect
    End Function

    ''' <summary>
    ''' Obtiene una unidad funcional por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetLiquidationPayroll(ByVal IdGroup As Integer, ByVal PayrollDateLiquidated As Date) As XPCollection
        Dim session As New IndigoXPOSession(Of PayrollLiquidation)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("GroupId = " & IdGroup & " and PayrollDateLiquidated = " & PayrollDateLiquidated)

        Dim collection = New XPCollection(session, GetType(PayrollLiquidation), criteria)
        Return collection
    End Function

    Function ListFunctionalUnitByCostCenter(costCenterCode As List(Of String)) As XPCollection(Of PayrollFunctionalUnit)
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CostCenterId.Codigo in ('" & String.Join("','", costCenterCode.ToArray) & "')")
        Dim collect As XPCollection(Of PayrollFunctionalUnit) = New XPCollection(Of PayrollFunctionalUnit)(session, criteria)
        Return collect
        'End Using
    End Function

    Function ListFunctionalUnitByCostCenterId(costCenterIdList As List(Of Integer)) As XPCollection(Of PayrollFunctionalUnit)
        Dim session As New IndigoXPOSession(Of PayrollFunctionalUnit)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CostCenterId.Id in (" & String.Join(",", costCenterIdList.ToArray) & ")")
        Dim collect As XPCollection(Of PayrollFunctionalUnit) = New XPCollection(Of PayrollFunctionalUnit)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Obtiene los cargos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPositionByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollPositionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & Status)
            Dim _classEntity = session.GetClassInfo(GetType(PayrollPositionXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;CodeName", criteria)
        Return _serverMode
    End Function
    ''' <summary>
    ''' Metodo para obtener el datasource para el reporte de liquidacion de contrato, recibe una lista para crear los filtros
    ''' </summary>
    ''' <param name="ListEmployee"></param>
    ''' <returns></returns>
    Function ListContractLiquidation(ListEmployee As List(Of Integer)) As XPCollection(Of PayrollViewReportContractLiquidation)
        Dim session As New IndigoXPOSession(Of PayrollViewReportContractLiquidation)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("contractLiquidationId in ('" & String.Join("','", ListEmployee.ToArray) & "')")
        Dim collect As XPCollection(Of PayrollViewReportContractLiquidation) = New XPCollection(Of PayrollViewReportContractLiquidation)(session, criteria)
        Return collect
        'End Using
    End Function
    ''' <summary>
    ''' Metodo para obtener el datasource del subreporte accured de contractliquidation
    ''' </summary>
    ''' <param name="ContractLiquidationId"></param>
    ''' <returns></returns>
    Function ListContractLiquidationAccured(ContractLiquidationId As Integer)
        Dim session As New IndigoXPOSession(Of PayrollViewReportContractLiquidation)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("contractLiquidationId = '" + ContractLiquidationId.ToString() + "' and sumaryAccured > 0")
        Console.WriteLine(criteria)
        Dim collect As XPCollection(Of PayrollViewReportContractLiquidation) = New XPCollection(Of PayrollViewReportContractLiquidation)(session, criteria)
        Return collect
        'End Using
    End Function
    ''' <summary>
    ''' Metodo para obtener el datasource del subreporte deducted de contractliquidation
    ''' </summary>
    ''' <param name="ContractLiquidationId"></param>
    ''' <returns></returns>
    Function ListContractLiquidationDeducted(ContractLiquidationId As Integer)
        Dim session As New IndigoXPOSession(Of PayrollViewReportContractLiquidation)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("contractLiquidationId = '" + ContractLiquidationId.ToString() + "' and sumaryDeducted > 0")
        Dim collect As XPCollection(Of PayrollViewReportContractLiquidation) = New XPCollection(Of PayrollViewReportContractLiquidation)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lista los deportes practicados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSportPractice() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollSportPracticeXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollSportPracticeXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName", Nothing)
        Return _serverMode
    End Function


    ''' <summary>
    ''' Lista los deportes practicados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSportPracticeByState(state As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollSportPracticeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State = " & state)
        Dim _classEntity = session.GetClassInfo(GetType(PayrollSportPracticeXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista las actividades de tiempo libre
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFreeTimeUse() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollFreeTimeUseXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollFreeTimeUseXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName", Nothing)
        Return _serverMode
    End Function

    Public Function ListBranchOffice() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollBranchOffice)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollBranchOffice))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
        Return _serverMode
    End Function

    Public Function ListBranchOfficeByState(state As Boolean) As XPInstantFeedbackSource
        Dim criteria = CriteriaOperator.Parse($"State={state}")
        Dim session As New IndigoXPOSession(Of PayrollBranchOffice)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollBranchOffice))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;CodeName", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista las actividades de tiempo libre
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFreeTimeUseByState(state As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollFreeTimeUseXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State = " & state)
        Dim _classEntity = session.GetClassInfo(GetType(PayrollFreeTimeUseXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName", criteria)
        Return _serverMode
    End Function

    
    ''' <summary>
    ''' Lista las creencias religiosas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListReligiousBeliefs() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollReligiousBeliefsXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollReligiousBeliefsXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Description;CodeName", Nothing)
        Return _serverMode
    End Function

        ''' <summary>
    ''' Lista los grupos étnicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEthnicGroups() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollEthnicGroupsXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollEthnicGroupsXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Description;CodeName", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista las enfermedades diagnosticadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDiagnosedDisease() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollDiagnosedDiseaseXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollDiagnosedDiseaseXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName", Nothing)
        Return _serverMode
    End Function


    ''' <summary>
    ''' Lista las enfermedades diagnosticadas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDiagnosedDiseaseByState(state As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollDiagnosedDiseaseXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State = " & state)
        Dim _classEntity = session.GetClassInfo(GetType(PayrollDiagnosedDiseaseXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Obtiene una lista de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListForeclousure() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollForeclousureXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollForeclousureXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Code;IdEmployee.ThirdPartyId.Nit;IdEmployee.ThirdPartyId.Name;IdJudgment.Descripcion;State", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista las entidades bancarias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCompanies() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollTradeUnionXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollTradeUnionXpo))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;PayrollConceptId", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista las liquidaciones por periodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLiquidationsByCompanyAndPayrollDateLiquidated(companyId As Integer, liquidationDate As DateTime) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollLiquidationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("RegisterStatus = 'C' AND GroupId.CompanyId = " & companyId & " And PayrollDateLiquidated = #" & Format(liquidationDate, "yyyy-MM-dd") & "#")
        Dim _classEntity = session.GetClassInfo(GetType(PayrollLiquidationXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista las liquidaciones por periodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLiquidationsByCompanyAndPayrollDateLiquidated(companyId As Integer, liquidationDate As DateTime) As XPCollection(Of PayrollLiquidationXpo)
        Dim session As New IndigoXPOSession(Of PayrollLiquidationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("RegisterStatus = 'C'  AND GroupId.CompanyId = " & companyId & " And PayrollDateLiquidated = #" & Format(liquidationDate, "yyyy-MM-dd") & "#" & " AND  PayrollBankFiles[BankFileId.Process = 2].Count = 0")
        Dim list = New XPCollection(Of PayrollLiquidationXpo)(session, criteria)
        Return list
    End Function

    ''' <summary>
    ''' Lista de liquidaciones ya confirmadas por periodo 
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <param name="liquidationDate"></param>
    ''' <returns></returns>
    Public Function ListLiquidationsByCompanyAndPayrollDateLiquidated2(companyId As Integer, liquidationDate As DateTime) As XPCollection(Of PayrollLiquidationXpo)
        Dim session As New IndigoXPOSession(Of PayrollLiquidationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("RegisterStatus = 'C'  AND GroupId.CompanyId = " & companyId & " And PayrollDateLiquidated = #" & Format(liquidationDate, "yyyy-MM-dd") & "#" & " AND PayrollBankFiles.Count>0")
        Dim list = New XPCollection(Of PayrollLiquidationXpo)(session, criteria)
        Return list
    End Function

    ''' <summary>
    ''' Lista las primma sapra AP Bancos
    ''' </summary>
    ''' <param name="Period"></param>
    ''' <param name="DateLiquidated"></param>
    ''' <returns></returns>
    Public Function ListBankFileIncentivePayment(Period As String, DateLiquidated As Date) As XPCollection(Of PayrollBankFileIncentivePaymentXpo)
        Dim session As New IndigoXPOSession(Of PayrollBankFileIncentivePaymentXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Period = " & Period & " And PeriodEndDate = '" & Format(DateLiquidated, "yyyy-MM-dd") & "' And RegisterStatus = 2 ")
        Dim list = New XPCollection(Of PayrollBankFileIncentivePaymentXpo)(session, criteria)
        Return list
    End Function

    ''' <summary>
    ''' Lista las primas confirmadas
    ''' </summary>
    ''' <param name="Period"></param>
    ''' <param name="DateLiquidated"></param>
    ''' <returns></returns>

    Public Function ListBankFileIncentivePaymentConfirm(Period As String, DateLiquidated As Date, bankFileId As Integer) As XPCollection(Of BankFileIncentivePaymentConfirmXpo)
        Dim session As New IndigoXPOSession(Of BankFileIncentivePaymentConfirmXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Period = " & Period & " And PeriodEndDate = '" & Format(DateLiquidated, "yyyy-MM-dd") & "' And Process = 1  and BankFileId = " & bankFileId)
        Dim list = New XPCollection(Of BankFileIncentivePaymentConfirmXpo)(session, criteria)
        Return list
    End Function

    ''' <summary>
    ''' Lista las liquidaciones por periodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLiquidationsByCompanyAndPayrollDateLiquidatedAsync(companyId As Integer, liquidationDate As DateTime) As Task(Of XPCollection(Of PayrollLiquidationXpo))
        Dim session As New IndigoXPOSession(Of PayrollLiquidationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("RegisterStatus = 'C'  AND DaysWorked > 0  AND GroupId.CompanyId = " & companyId & " And PayrollDateLiquidated = #" & Format(liquidationDate, "yyyy-MM-dd") & "#")
        Dim list = New XPCollection(Of PayrollLiquidationXpo)(session, criteria)
        Return Task.FromResult(list)
    End Function

    Public Function ListBankFiles() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollBankFileXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollBankFileXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Code;CompanyId.CodeName;LiquidationDate;EntityBankAccountId.CodeName;Value;StatusName;PeriodIncentivePayment;ProcessName", Nothing)
        Return _serverMode
    End Function


    Public Function ListViewVerifyAutoliquidationFile(WorkCenterCode As String, varPayrollDateLiquidated As Date) As XPCollection(Of PayrollViewVerifyAutoliquidationFileXpo)
        Dim session As New IndigoXPOSession(Of PayrollViewVerifyAutoliquidationFileXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PayrollDateLiquidated =  #" & Format(varPayrollDateLiquidated, "yyyy-MM-dd") & "# and WorkCenter = " & WorkCenterCode)
        Dim list = New XPCollection(Of PayrollViewVerifyAutoliquidationFileXpo)(session, criteria)
        Return list
    End Function

    Public Function ListEmployeeScheduleC() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollEmployeeScheduleCXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PayrollEmployeeScheduleCXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;StatusName;Status", Nothing)
        Return _serverMode
    End Function

    Public Function ListMinimumSalary() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollMinimumSalaryXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PayrollMinimumSalaryXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Year;LegalMinimumSalary;TransportHelpValue", Nothing)
        Return serverMode
    End Function

    Public Function ListContributorSubtype() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollContributorSubTypeXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PayrollContributorSubTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", Nothing)
        Return serverMode
    End Function

    Public Function ListLicensingConcepts() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollLicensingConceptsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PayrollLicensingConceptsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;ClassName", Nothing)
        Return serverMode
    End Function
    ''' <summary>
    ''' Lista que contiene los conceptos de licencias filtrados por la clase del concepto 
    ''' </summary>
    ''' <param name="LicencingType"></param>
    ''' <returns></returns>
    Public Function ListLicensingConceptsXpo(LicencingType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollLicensingConceptsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PayrollLicensingConceptsXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("LicensingConceptsClass =" & LicencingType)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;ClassName", criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' Lista que contiene los tipos de estado civil
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMaritalStatus() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollMaritalStatusXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PayrollMaritalStatusXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", Nothing)
        Return serverMode
    End Function


#Region "ListEmployee LinqInstantFeedbackSpurce"
    Public Function ListAllEmployee() As LinqInstantFeedbackSource
        Dim vlinq As New LinqInstantFeedbackSource
        AddHandler vlinq.GetQueryable, AddressOf OnGetQueryableAllEmployee
        AddHandler vlinq.DismissQueryable, AddressOf DismissQueryableAllEmployee
        vlinq.KeyExpression = "Id"
        Return vlinq
    End Function

    Private Sub OnGetQueryableAllEmployee(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableEmployee As XPQuery(Of PayrollEmployeeXpo) = New XPQuery(Of PayrollEmployeeXpo)(session)
            Dim TmpQueryableSource = From T1 In tableEmployee
                                     Select T1.Id, T1.ThirdPartyId.Nit, T1.ThirdPartyId.Name, T1.ThirdPartyNitName
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableEmployee
            'End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DismissQueryableAllEmployee(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region


#Region "GetEmployee LinqInstantFeedbackSpurce"

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployee() As LinqInstantFeedbackSource
        Dim vlinq As New LinqInstantFeedbackSource
        AddHandler vlinq.GetQueryable, AddressOf OnGetQueryable
        AddHandler vlinq.DismissQueryable, AddressOf DismissQueryable
        vlinq.KeyExpression = "Id"
        Return vlinq
    End Function

    Private Sub OnGetQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableEmployee As XPQuery(Of PayrollEmployeeXpo) = New XPQuery(Of PayrollEmployeeXpo)(session)
            Dim tableContract As XPQuery(Of PayrollContractXpo) = New XPQuery(Of PayrollContractXpo)(session)
            Dim TmpQueryableSource = From T1 In tableEmployee
                                     Join T2 In tableContract On T1.Id Equals T2.EmployeeId.Id
                                     Where T1.Id = T2.EmployeeId.Id And T2.Valid = True
                                     Select T1.Id, T1.ThirdPartyId.Nit, T1.ThirdPartyId.Name, T2.GroupId.Descripcion, GroupId = T2.GroupId.Id
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableEmployee
            'End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DismissQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "GetEmployee ByFunctionalUnit or Group LinqInstantFeedbackSpurce"

    Dim _functionalUnitId As Integer
    Dim _groupId As Integer

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeByFunctionalUnitIdOrGroupId(Optional functionalUnitId As Integer = 0, Optional groupId As Integer = 0) As LinqInstantFeedbackSource
        Dim vlinq1 As New LinqInstantFeedbackSource
        AddHandler vlinq1.GetQueryable, AddressOf OnGetQueryable
        AddHandler vlinq1.DismissQueryable, AddressOf DismissQueryable
        _functionalUnitId = functionalUnitId
        _groupId = groupId
        vlinq1.KeyExpression = "Id"
        Return vlinq1
    End Function

    Private Sub OnGetQueryable1(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableEmployee As XPQuery(Of PayrollEmployeeXpo) = New XPQuery(Of PayrollEmployeeXpo)(session)
            Dim tableContract As XPQuery(Of PayrollContractXpo) = New XPQuery(Of PayrollContractXpo)(session)


            If _functionalUnitId > 0 Then
                Dim TmpQueryableSource = From T1 In tableEmployee
                                         Join T2 In tableContract On T1.Id Equals T2.EmployeeId.Id
                                         Where T1.Id = T2.EmployeeId.Id And T2.Valid = True And T2.FunctionalUnitId.Id = _functionalUnitId
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
            'End Using


        Catch ex As Exception

        End Try
    End Sub

    Private Sub DismissQueryable1(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "GetAllEmployees"

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllEmployees() As LinqInstantFeedbackSource
        Dim vlinqEmployees As New LinqInstantFeedbackSource
        AddHandler vlinqEmployees.GetQueryable, AddressOf OnGetQueryableEmployee
        AddHandler vlinqEmployees.DismissQueryable, AddressOf DismissQueryableEmployee
        vlinqEmployees.KeyExpression = "Id"
        Return vlinqEmployees
    End Function
    Private Sub OnGetQueryableEmployee(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableEmployee As XPQuery(Of PayrollEmployeeXpo) = New XPQuery(Of PayrollEmployeeXpo)(session)
            Dim tableContract As XPQuery(Of PayrollContractXpo) = New XPQuery(Of PayrollContractXpo)(session)

            Dim TmpQueryableSource = From T1 In tableEmployee
                                     Join T2 In tableContract On T1.Id Equals T2.EmployeeId.Id
                                     Where T2.Valid = True
                                     Select T1.Id, T1.ThirdPartyId.Nit, T1.ThirdPartyId.Name, T2.GroupId.Descripcion

            Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableEmployee
            'End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DismissQueryableEmployee(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllEmployeesNoLiquidated() As LinqInstantFeedbackSource
        Dim vlinqEmployees As New LinqInstantFeedbackSource
        AddHandler vlinqEmployees.GetQueryable, AddressOf OnGetQueryableEmployeeNoLiquidated
        AddHandler vlinqEmployees.DismissQueryable, AddressOf DismissQueryableEmployeeNoLiquidated
        vlinqEmployees.KeyExpression = "Id"
        Return vlinqEmployees
    End Function

    Private Sub OnGetQueryableEmployeeNoLiquidated(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableEmployee As XPQuery(Of PayrollEmployeeXpo) = New XPQuery(Of PayrollEmployeeXpo)(session)
            Dim tableContract As XPQuery(Of PayrollContractXpo) = New XPQuery(Of PayrollContractXpo)(session)

            Dim TmpQueryableSource = From T1 In tableEmployee
                                     Join T2 In tableContract On T1.Id Equals T2.EmployeeId.Id
                                     Where T2.Valid = True And T2.Status <> 2
                                     Select T1.Id, T1.ThirdPartyId.Nit, T1.ThirdPartyId.Name, T2.GroupId.Descripcion

            Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableEmployee
            'End Using

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DismissQueryableEmployeeNoLiquidated(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region


#Region "GetAllEmployeesWithContratToLiquidate"

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllEmployeesWithContratToLiquidate() As LinqInstantFeedbackSource
        Dim vlinqEmployeesWithContratToLiquidate As New LinqInstantFeedbackSource
        AddHandler vlinqEmployeesWithContratToLiquidate.GetQueryable, AddressOf OnGetQueryableEmployeeWithContratToLiquidate
        AddHandler vlinqEmployeesWithContratToLiquidate.DismissQueryable, AddressOf DismissQueryableEmployeeWithContractToLiquidate
        vlinqEmployeesWithContratToLiquidate.KeyExpression = "Id"
        Return vlinqEmployeesWithContratToLiquidate
    End Function

    Private Sub OnGetQueryableEmployeeWithContratToLiquidate(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableEmployee As XPQuery(Of PayrollEmployeeXpo) = New XPQuery(Of PayrollEmployeeXpo)(session)
            Dim tableContract As XPQuery(Of PayrollContractXpo) = New XPQuery(Of PayrollContractXpo)(session)
            Dim tableContractType As XPQuery(Of PayrollContractTypeXpo) = New XPQuery(Of PayrollContractTypeXpo)(session)

            Dim TmpQueryableSource = From T1 In tableEmployee
                                     Join T2 In tableContract On T1.Id Equals T2.EmployeeId.Id
                                     Join T3 In tableContractType On T2.ContractTypeId Equals T3.Id
                                     Where (T2.Valid = True And (T3.ContractClass <> 1) And T2.RetirementDate.ToString() Is Nothing) Or (T2.Valid = True And T2.Status = 5)
                                     Select T1.Id, T1.ThirdPartyId.Nit, T1.ThirdPartyId.Name, T2.GroupId.Descripcion

            Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableEmployee
            'End Using


        Catch ex As Exception

        End Try
    End Sub

    Private Sub DismissQueryableEmployeeWithContractToLiquidate(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "Nomina Electronica"

    ''' <summary>
    ''' Lista todas las los documentos electronicos de tipo Soporte de pago de nomina electronica
    ''' </summary>
    ''' <returns></returns>
    Public Function ListElectronicPayrollPaymentSupports() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ElectronicPayrollXpo)()
        Dim strCriteria = "DocumentType In (1)"
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicPayrollXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todas las los documentos electronicos de tipo Notas de Ajuste
    ''' </summary>
    ''' <returns></returns>
    Public Function ListElectronicPayrollsAdjustmentNotes() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ElectronicPayrollXpo)()
        Dim strCriteria = "DocumentType In (2)"
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicPayrollXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todas las los detalles de un documentos electronicos
    ''' </summary>
    ''' <param name="ElectronicPayrollId">Electronic Document Id.</param>
    ''' <returns></returns>
    Public Function ListElectronicPayrollDetails(ElectronicPayrollId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ElectronicPayrollDetailXpo)()
        Dim strCriteria = "ElectronicPayrollId.Id = " & ElectronicPayrollId
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicPayrollDetailXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todas las los detalles de un documentos electronicos
    ''' </summary>
    ''' <param name="ElectronicPayrollId">Electronic Document Id.</param>
    ''' <returns></returns>
    Public Function ListElectronicPayrollNotifications(ElectronicPayrollId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ElectronicPayrollNotificationXpo)()
        Dim strCriteria = "ElectronicPayrollId.Id = " & ElectronicPayrollId
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ElectronicPayrollNotificationXpo)), Nothing, criteria)
    End Function

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

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

#Region "LinqInstantFeedBackSource"

#Region "ListBranchOfficeByThirdPartyId"
    Private _filterThirdPartyId As Integer
    Private _filterStatus As Boolean
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListBranchOfficeByThirdPartyId(ThirdPartyId As Integer, Status As Boolean) As LinqInstantFeedbackSource
        Dim vlinqBranchOffice As New LinqInstantFeedbackSource
        AddHandler vlinqBranchOffice.GetQueryable, AddressOf OnGetQueryableEntity
        AddHandler vlinqBranchOffice.DismissQueryable, AddressOf DismissQueryableEntity
        _filterThirdPartyId = ThirdPartyId
        _filterStatus = Status
        vlinqBranchOffice.KeyExpression = "Id"
        Return vlinqBranchOffice
    End Function

    Private Sub OnGetQueryableEntity(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session = New Session(XpoDefault.DataLayer)
            Dim tableThirdPartyBranchOffice As XPQuery(Of ThirdPartyBranchOfficeXpo) = New XPQuery(Of ThirdPartyBranchOfficeXpo)(session)
            Dim tableBranchOffice As XPQuery(Of PayrollBranchOffice) = New XPQuery(Of PayrollBranchOffice)(session)
            Dim TmpQueryableSource = From b In tableBranchOffice
                                     Join tb In tableThirdPartyBranchOffice On b.Id Equals tb.BranchOfficeId.Id
                                     Where tb.ThirdPartyId = _filterThirdPartyId
                                     Select b
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableBranchOffice
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableEntity(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

#End Region

#End Region

End Class
