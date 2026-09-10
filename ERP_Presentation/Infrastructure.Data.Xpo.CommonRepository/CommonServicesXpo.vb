'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.SecurityRepository
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-28
'
' Last Modified By : Juan Diego Diaz
' Last Modified On : 11-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports System.Configuration
Imports DevExpress.Data.Linq

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class CommonServicesXpo

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
    ''' Incializa una nueva instancia de la clase <see cref="CommonServicesXpo" />.
    ''' </summary>
    Public Sub New(Company As String)
        'verifico que exista el archivo
        ReadConfiguration()
        Dim DataSource = ConfigurationManager.AppSettings("DataSourceGenesis")
        Dim User = ConfigurationManager.AppSettings("UserGenesis")
        Dim Password = ConfigurationManager.AppSettings("PasswordGenesis")
        Dim Db = ConfigurationManager.AppSettings("DbGenesis")
        'establezclo la capa de datos para XPO
        XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStore(GetEndPoint, GetRemoteAddress, Company))

    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' metodo necesario para leer la configuracion xml de la aplicacion
    ''' </summary>
    Private Sub ReadConfiguration()
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

    Public Function ListCustomerByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status)
        classEntity = sessionNew.GetClassInfo(GetType(CommonCustomerXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCountry() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonCountryXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;Nationality", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDistributionLine() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonDistibutionLineXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdMainAccount.NumberName;ExpensesConceptId.CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado lineas distribucion proveedor
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSuppliersDistributionLines() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdSupplier.Status=" & True)
        classEntity = sessionNew.GetClassInfo(GetType(CommonSuppliersDistibutionLineXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;IdSupplier.Id;IdSupplier.Code;IdSupplier.Name;IdSupplier.CodeName;IdDistributionLine.Id;IdDistributionLine.Code;IdDistributionLine.Name;IdDistributionLine.CodeName;DisplaySupplier;IdDistributionLine.IdMainAccount.Id;IdSupplier.IdThirdParty.Id;IdSupplier.IdThirdParty.Nit", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado lineas distribucion proveedor por id
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSuppliersDistributionLinesById(ByVal id As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & id & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(CommonSuppliersDistibutionLineXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el Listado de Departamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDepartment(idPais As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CountryId='" & idPais & "'")
        classEntity = sessionNew.GetClassInfo(GetType(CommonDepartmentXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta todas las ciudades pertenecientes a un departamento
    ''' </summary>
    ''' <param name="IdDepartamento">Id del departamento</param>
    ''' <returns></returns>
    Public Function GetCities(ByVal IdDepartamento As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DepartamentId='" & CStr(IdDepartamento) & "'")
        classEntity = sessionNew.GetClassInfo(GetType(CommonCityXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el Listado de Centros de Atención
    ''' </summary>
    ''' <returns></returns>
    Public Function GetHealthCenter() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonHealthCenter))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el Listado de Usuarios
    ''' </summary>
    ''' <returns></returns>
    Public Function GetUsers() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonSEGUSUARUXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;IDENTIFICACION.IPNOMCOMP", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnit() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonFunctionalUnitXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de telefonos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPhoneType() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonPhoneTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los terceros por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdParty() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & True)
        classEntity = sessionNew.GetClassInfo(GetType(CommonThirdPartyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Nit;Name;PersonId.IdentificationNumber;PersonId.FirstName;PersonId.FirstLastName;NitName;PersonId.IdentificationType;PersonId.IdentificacionCityId.Descripcion;RetentionType;ContributionType;State;StateName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllThirdParty() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonThirdPartyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Nit;Name;PersonId.IdentificationNumber;PersonId.FirstName;PersonId.FirstLastName;NitName;PersonId.IdentificationType;PersonId.IdentificacionCityId.Descripcion;RetentionType;ContributionType;State;StateName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las unidades operativas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListOperatingUnitTreeList() As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(CommonOperatingUnitXpo))
        Return collect
    End Function

    ''' <summary>
    ''' Lista todas las unidades operativas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListOperatingUnit() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonOperatingUnitXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;UnitCode;UnitName;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las unidades operativas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListOperatingUnitById(ByVal operatingUnitId As Integer) As XPCollection
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
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonDisabilityXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las ciudades por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllCities(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(CommonCityXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;CodeName;DepartamentId.Descripcion;State", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los terceros  que tenga cierta persona
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyByPersonId(personId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PersonId='" & personId & "'")
        classEntity = sessionNew.GetClassInfo(GetType(CommonThirdPartyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Nit;Name;PersonId.IdentificationNumber;PersonId.FirstName;PersonId.FirstLastName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las personas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPerson() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonPersonXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "IdentificationNumber;FirstName;SecondName;FirstLastName;SecondLastName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista Todas las Unidades de Tiempo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTimeUnit() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonTimeUnitXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las actividades economicas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEconomicActivity() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonEconomicActivity))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;StatusName;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las actividades economicas por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEconomicActivityByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(CommonEconomicActivity))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;StatusName;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pago por estado y si maneja retencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptsAccountsPayableByStatusAndHandlesRetentionOfCommon(status As Boolean, handlesRetention As Boolean) As XPCollection
        'Dim sessionNew = New Session(XpoDefault.DataLayer)
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
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonBasicAuditBase))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

#Region "GetEmployee LinqInstantFeedbackSpurce"
    Private WithEvents vlinq As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListThird() As LinqInstantFeedbackSource
        vlinq.KeyExpression = "Id"
        Return vlinq
    End Function

    Private Sub OnGetQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinq.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableThird As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(sessionNew)
            Dim tablePerson As XPQuery(Of CommonPersonXpo) = New XPQuery(Of CommonPersonXpo)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableThird
                                     Join T2 In tablePerson On T1.PersonId.Id Equals T2.Id
                                     Select T1.Id, T1.Nit, T1.Name, T2.FirstName, T2.FirstLastName
            Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableThird
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
#End Region

End Class
