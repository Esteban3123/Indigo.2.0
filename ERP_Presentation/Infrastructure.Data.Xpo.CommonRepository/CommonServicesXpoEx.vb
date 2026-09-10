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
Public Class CommonServicesXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        result.Sort()
        Return result
    End Function

    Public Function ListAllHealthProfessional() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewHealthProfessionalXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ViewHealthProfessionalXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
    End Function

    Public Function ListHealthProfessionalByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Status={status}")
        Dim session As New IndigoXPOSession(Of ViewHealthProfessionalXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ViewHealthProfessionalXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    Public Function ListEmailByIdPerson(IdPerson As Integer) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"IdPerson={IdPerson} And Type = 2")
        Dim session As New IndigoXPOSession(Of CommonEmailXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonEmailXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function
    Public Function GetCommonThirdPartyXpo(nit As String) As CommonThirdPartyXpo
        Dim session As New IndigoXPOSession(Of CommonThirdPartyXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Nit='" & nit & "'")
        Return session.FindObject(Of CommonThirdPartyXpo)(criteria)
    End Function
    Public Function ListAllSupplier() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of Maintenance_Supplier)()
        Dim _classEntity = session.GetClassInfo(GetType(Maintenance_Supplier))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
    End Function

    Public Function ListSupplierByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Status={status}")
        Dim session As New IndigoXPOSession(Of Maintenance_Supplier)()
        Dim _classEntity = session.GetClassInfo(GetType(Maintenance_Supplier))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    Public Function ListAllSupplierManufacturer() As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Manufacturer={1} And Status={1}")
        Dim session As New IndigoXPOSession(Of Maintenance_Supplier)()
        Dim _classEntity = session.GetClassInfo(GetType(Maintenance_Supplier))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    Public Function ListAllSupplierSeller() As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Seller={1} And Status={1}")
        Dim session As New IndigoXPOSession(Of Maintenance_Supplier)()
        Dim _classEntity = session.GetClassInfo(GetType(Maintenance_Supplier))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    Public Function ListThirdPartyDocuments(thirdPartyId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewThirdPartyDocumentsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ThirdPartyId=" & thirdPartyId)
        Dim _classEntity = session.GetClassInfo(GetType(ViewThirdPartyDocumentsXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    Public Function ListCustomerByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonCustomerXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status)
        Dim _classEntity = session.GetClassInfo(GetType(CommonCustomerXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Nit;ThirdPartyId.ContributionTypeName;ThirdPartyId.RetentionTypeName;Name;ThirdPartyId.PersonId.IdentificationTypeName;NitName;ThirdPartyId.PersonId.IdentificacionCityId.Descripcion;ThirdPartyId.ContributionType;StateName;State;ThirdPartyId.Id", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los terceros  que tenga cierta persona
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAddressByPersonId(personId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonAddressXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdPerson = " & personId & " AND DepartmentId IS NOT NULL AND CityId IS NOT NULL")
        Dim _classEntity = session.GetClassInfo(GetType(CommonAddressXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    '''' <summary>
    '''' Obtiene todos los empleados
    '''' </summary>
    'Public Function ListBalancedScorecard() As LinqInstantFeedbackSource
    '    Dim vlinq As New LinqInstantFeedbackSource
    '    AddHandler vlinq.GetQueryable, AddressOf OnGetQueryableBalanced
    '    AddHandler vlinq.DismissQueryable, AddressOf DismissQueryable
    '    vlinq.KeyExpression = "Id"
    '    Return vlinq
    'End Function

    'Private Sub OnGetQueryableBalanced(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
    '    Try
    '        Dim session As New Session(XpoDefault.DataLayer)
    '        Dim tableBalanced As New XPQuery(Of CommonBalancedScorecardXpo)(session)
    '        Dim tablePerson As New XPQuery(Of CommonPersonXpo)(session)
    '        Dim TmpQueryableSource = From T1 In tableThird
    '                                 Join T2 In tablePerson On T1.PersonId.Id Equals T2.Id
    '                                 Select T1.Id, T1.Nit, T1.Name, T2.FirstName, T2.FirstLastName
    '        Dim prueba = TmpQueryableSource.ToList
    '        e.QueryableSource = TmpQueryableSource
    '        e.Tag = tableThird
    '        'End Using
    '    Catch ex As Exception
    '    End Try
    'End Sub
    Public Function GetBalancedScorecard(Optional userGroup As Integer = 0) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonBalancedScorecardXpo)()
        Dim criteria = CriteriaOperator.Parse($"BalancedScorecardByGroups[UserGroup={userGroup}]")
        If userGroup = 0 Then
            criteria = Nothing
        End If
        Dim _classEntity = session.GetClassInfo(GetType(CommonBalancedScorecardXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCountry() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonCountryXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonCountryXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;Nationality", Nothing)
    End Function

    ''' <summary>
    ''' Metodo que trae una collección de monedas desde el xpo de CommonCurrency 
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    Public Function getCollectionCurrency(Optional Status As Boolean = True) As XPCollection
        Dim session As New IndigoXPOSession(Of CommonCurrencyXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & Status & "")
        Return New XPCollection(session, GetType(CommonCurrencyXpo), criteria)
    End Function

    ''' <summary>
    ''' Consulta el listado de Monedas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCurrency(Optional Status As Boolean = True) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonCurrencyXpo)()
        Dim criteria As CriteriaOperator = If(Status, CriteriaOperator.Parse($"State={Status}"), Nothing)
        Dim _classEntity = session.GetClassInfo(GetType(CommonCurrencyXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Codigo;CurrencyName;State;EstadoName;CodeName;Abbreviation;AbbreviationName", Nothing)
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionLine() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonDistibutionLineXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonDistibutionLineXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;IdMainAccount.NumberName;ExpensesConceptId.CodeName", Nothing)
    End Function

    ''' <summary>
    ''' Consulta el listado lineas distribucion proveedor
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSuppliersDistributionLines(Optional SupplierId As Integer? = Nothing) As XPInstantFeedbackSource
        Dim filters As String = "Status = 1 and IdSupplier.Status =" & True & " and IdSupplier.IdThirdParty.State = " & True
        If SupplierId IsNot Nothing Then
            filters = filters & " and IdSupplier.Id =" & SupplierId
        End If

        Dim session As New IndigoXPOSession(Of CommonSuppliersDistibutionLineXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filters)
        Dim _classEntity = session.GetClassInfo(GetType(CommonSuppliersDistibutionLineXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;IdSupplier.Id;IdSupplier.Code;IdSupplier.Name;IdSupplier.CodeName;IdDistributionLine.Id;IdDistributionLine.Code;IdDistributionLine.Name;IdDistributionLine.CodeName;DisplaySupplier;IdDistributionLine.IdMainAccount.Id;IdSupplier.IdThirdParty.Id;IdSupplier.IdThirdParty.Nit;Status", criteria)
    End Function

    ''' <summary>
    ''' Consulta el listado lineas distribucion proveedor por id
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSuppliersDistributionLinesById(ByVal id As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of CommonSuppliersDistibutionLineXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & id & "")
        Return New XPCollection(session, GetType(CommonSuppliersDistibutionLineXpo), criteria)
        'End Using
    End Function

    Public Function ListSuppliersDistributionLines(ByVal id As Integer) As XPCollection(Of CommonSuppliersDistibutionLineXpo)
        Dim session As New IndigoXPOSession(Of CommonSuppliersDistibutionLineXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & id & "")
        Return New XPCollection(Of CommonSuppliersDistibutionLineXpo)(session, GetType(CommonSuppliersDistibutionLineXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta un tercero por nit
    ''' </summary>
    ''' <returns></returns>
    Public Function GetThirdPartyByNit(ByVal nit As String) As XPCollection
        Dim session As New IndigoXPOSession(Of CommonThirdPartyXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Nit= '" & nit & "'")
        Return New XPCollection(session, GetType(CommonThirdPartyXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Consulta el Listado de Departamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDepartment(idPais As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonDepartmentXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CountryId='" & idPais & "'")
        Dim _classEntity = session.GetClassInfo(GetType(CommonDepartmentXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Codigo;Descripcion", criteria)
    End Function

    ''' <summary>
    ''' Consulta departamento por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetDepartmentByCode(ByVal code As String) As CommonDepartmentXpo
        Dim session As New IndigoXPOSession(Of CommonDepartmentXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Codigo='" & code & "'")
        Dim _departmentXpo As CommonDepartmentXpo = New XPCollection(Of CommonDepartmentXpo)(session, criteria).FirstOrDefault()
        Return _departmentXpo
    End Function

    ''' <summary>
    ''' Consulta país por código estándar
    ''' </summary>
    ''' <param name="standardCode"></param>
    ''' <returns></returns>
    Public Function GetCountryByStandardCode(ByVal standardCode As String) As CommonCountryXpo
        Dim session As New IndigoXPOSession(Of CommonCountryXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("StandardCode='" & standardCode & "'")
        Dim _countryXpo As CommonCountryXpo = New XPCollection(Of CommonCountryXpo)(session, criteria).FirstOrDefault()
        Return _countryXpo
    End Function

    ''' <summary>
    ''' Consulta todas las ciudades pertenecientes a un departamento
    ''' </summary>
    ''' <param name="IdDepartamento">Id del departamento</param>
    ''' <returns></returns>
    Public Function GetCities(ByVal IdDepartamento As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonCityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DepartamentId='" & CStr(IdDepartamento) & "'")
        Dim _classEntity = session.GetClassInfo(GetType(CommonCityXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Codigo;Descripcion;CodeName", criteria)
    End Function

    ''' <summary>
    ''' Consulta el Listado de Centros de Atención
    ''' </summary>
    ''' <returns></returns>
    Public Function GetHealthCenter() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonHealthCenter)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonHealthCenter))
        Return New XPInstantFeedbackSource(_classEntity, "Codigo;Descripcion", Nothing)
    End Function

    ''' <summary>
    ''' Consulta el Listado de Usuarios
    ''' </summary>
    ''' <returns></returns>
    Public Function GetUsers() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonSEGUSUARUXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonSEGUSUARUXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Codigo;IDENTIFICACION.IPNOMCOMP", Nothing)
    End Function

    ''' <summary>
    ''' Consulta el listado de unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnit() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonFunctionalUnitXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonFunctionalUnitXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de telefonos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPhoneType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonPhoneTypeXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonPhoneTypeXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Codigo;Descripcion", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los terceros por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdParty() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonThirdPartyXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & True)
        Dim _classEntity = session.GetClassInfo(GetType(CommonThirdPartyXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Nit;ContributionTypeName;RetentionTypeName;Name;PersonId.IdentificationTypeName;PersonId.IdentificationTypeId.NOMBRE;PersonId.IdentificationNumber;PersonId.FirstName;PersonId.FirstLastName;NitName;PersonId.IdentificationType;PersonId.IdentificacionCityId.Descripcion;RetentionType;ContributionType;State;StateName", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllThirdParty() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonThirdPartyXpo)
        Dim _classEntity = session.GetClassInfo(GetType(CommonThirdPartyXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Nit;Name;PersonId.IdentificationNumber;PersonId.FirstName;PersonId.FirstLastName;NitName;PersonId.IdentificationType;PersonId.IdentificacionCityId.Descripcion;RetentionType;ContributionType;State;StateName;PersonTypeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todas las unidades operativas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListOperatingUnitTreeList() As XPCollection
        Dim session As New IndigoXPOSession(Of CommonOperatingUnitXpo)()
        Return New XPCollection(session, GetType(CommonOperatingUnitXpo))
    End Function

    ''' <summary>
    ''' Lista todas las unidades operativas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListOperatingUnit() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonOperatingUnitXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonOperatingUnitXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;UnitCode;UnitName;CodeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todas las unidades operativas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListOperatingUnitById(ByVal operatingUnitId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of CommonOperatingUnitXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id = " & operatingUnitId)
        Dim collect As XPCollection = New XPCollection(GetType(CommonOperatingUnitXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todos las discapacidades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDisability() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonDisabilityXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonDisabilityXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
    End Function

    ''' <summary>
    ''' Lista todas las ciudades por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllCities(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonCityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(CommonCityXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion;CodeName;DepartamentId.Descripcion;State", criteria)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCityByCode(ByVal code As String) As CommonCityXpo
        Dim session As New IndigoXPOSession(Of CommonCityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Codigo='" & code & "'")
        Dim _cityXpo As CommonCityXpo = new XPCollection(Of CommonCityXpo)(session, criteria).FirstOrDefault()
        Return _cityXpo
    End Function

    ''' <summary>
    ''' Lista todos los terceros  que tenga cierta persona
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyByPersonId(personId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonThirdPartyXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PersonId='" & personId & "'")
        Dim _classEntity = session.GetClassInfo(GetType(CommonThirdPartyXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Nit;Name;PersonId.IdentificationNumber;PersonId.FirstName;PersonId.FirstLastName", criteria)
    End Function

    ''' <summary>
    ''' Lista todas las personas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPerson() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonPersonXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonPersonXpo))
        Return New XPInstantFeedbackSource(_classEntity, "IdentificationNumber;FirstName;SecondName;FirstLastName;SecondLastName;Name;LastName;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista Todas las Unidades de Tiempo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTimeUnit() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonTimeUnitXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonTimeUnitXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Codigo;Descripcion", Nothing)
    End Function

    ''' <summary>
    ''' lista todas las actividades economicas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEconomicActivity() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonEconomicActivity)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonEconomicActivity))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Status;StatusName;CodeName;IsIncomeGenerating;IsIncomeGeneratingName", Nothing)
    End Function

    ''' <summary>
    ''' lista todas exoneraciones tributarias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTaxExemptions() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonTaxExemptionsXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CommonTaxExemptionsXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Description;ApplicationType;InternalCode;CodeName", Nothing)
    End Function

    ''' <summary>
    ''' lista todas exoneraciones tributarias por medio del tipo de aplicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTaxExemptionsByApplicationType(ApplicationType As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonTaxExemptionsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"ApplicationType = {ApplicationType} And Status = True")
        Dim _classEntity = session.GetClassInfo(GetType(CommonTaxExemptionsXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Description;ApplicationType;InternalCode;CodeName", criteria)
    End Function

    ''' <summary>
    ''' lista las actividades economicas por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEconomicActivityByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonEconomicActivity)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(CommonEconomicActivity))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Status;StatusName;CodeName", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado y si maneja retencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsAccountsPayableByStatusAndHandlesRetentionOfCommon(status As Boolean, handlesRetention As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of ConceptsAccountPayableXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And HandlesRetention=" & handlesRetention)
        Dim collect As XPCollection = New XPCollection(GetType(ConceptsAccountPayableXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista Auditoria Basica
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBasicAudit(Month As String, Year As String) As XPInstantFeedbackSource
        Dim dictionary As XPDictionary = XpoDefault.DataLayer.Dictionary
        Dim myBaseClass As XPClassInfo = dictionary.GetClassInfo(GetType(CommonBasicAuditBase))
        Dim classEntity As XPClassInfo = dictionary.CreateClass(myBaseClass, "BasicAudit", New PersistentAttribute("BasicAudit" & Month & "_" & Year))
        'classEntity.CreateMember("Id", GetType(Integer), New KeyAttribute(True))
        'classEntity.CreateMember("Name", GetType(String), New SizeAttribute(50))
        'classEntity.CreateMember("Dato", GetType(String), New SizeAttribute(50))
        Dim session As New IndigoXPOSession(Of CommonBasicAuditBase)
        classEntity = session.GetClassInfo(GetType(CommonBasicAuditBase))
        Return New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
    End Function

    Public Function ListFiscalResponsibilityByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonFiscalResponsibilityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Dim _classEntity = session.GetClassInfo(GetType(CommonFiscalResponsibilityXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' lista los emails de los terceros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEmailPerson(PersonId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonEmailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdPerson=" & PersonId & "")
        Dim _classEntity = session.GetClassInfo(GetType(CommonEmailXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;IdPerson;Email;State;Synchronized;Type", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los registros de moneda segun la iso 4217
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllISO4217() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ISO4217Xpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ISO4217Xpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los registros de moneda segun la iso 4217 excepto la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllISO4217WithoutOfficialCurrency(officialCurrencyId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonCurrencyXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id <>" & officialCurrencyId & "")
        Dim _classEntity = session.GetClassInfo(GetType(CommonCurrencyXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function
    ''' <summary>
    ''' Lista todos los registros de moneda segun la iso 4217
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllISO4217(filter As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ISO4217Xpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ISO4217Xpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function
    ''' <summary>
    ''' Lista las rentas Exentas anuales del empleado
    ''' </summary>
    ''' <param name="Nit"></param>
    ''' <returns></returns>
    Public Function ListExemptIncome(ByVal Nit As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonExemptIncomeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ThirdPartyId='" & Nit & "'")
        Dim _ExemptIncome = session.GetClassInfo(GetType(CommonExemptIncomeXpo))
        Return New XPInstantFeedbackSource(_ExemptIncome, "Year;AccumulatedValue", criteria)
    End Function

    ''' <summary>
    ''' Lista Detalle de las rentas Exentas anuales del empleado
    ''' </summary>
    ''' <param name="Nit"></param>
    ''' <param name="Year"></param>
    ''' <returns></returns>
    Public Function ListExemptIncomeDetail(ByVal Nit As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of CommonExemptIncomeDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ThirdPartyId='" & Nit & "' AND RegisterStatus = 'C' AND ExemptIncomeValue > 0")
        ' Dim _ExemptIncome = session.GetClassInfo(GetType(CommonExemptIncomeDetailXpo))
        Return New XPCollection(session, GetType(CommonExemptIncomeDetailXpo), criteria)
    End Function


#Region "GetEmployee LinqInstantFeedbackSpurce"

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListThird() As LinqInstantFeedbackSource
        Dim vlinq As New LinqInstantFeedbackSource
        AddHandler vlinq.GetQueryable, AddressOf OnGetQueryable
        AddHandler vlinq.DismissQueryable, AddressOf DismissQueryable
        vlinq.KeyExpression = "Id"
        Return vlinq
    End Function

    Private Sub OnGetQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableThird As New XPQuery(Of CommonThirdPartyXpo)(session)
            Dim tablePerson As New XPQuery(Of CommonPersonXpo)(session)
            Dim TmpQueryableSource = From T1 In tableThird
                                     Join T2 In tablePerson On T1.PersonId.Id Equals T2.Id
                                     Select T1.Id, T1.Nit, T1.Name, T2.FirstName, T2.FirstLastName
            Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableThird
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

End Class