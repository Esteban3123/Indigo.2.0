'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.ContractRepostory
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/09/2014
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
Imports DevExpress.Data.Linq
Imports System.Configuration

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class ContractServiceXpo
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
    ''' <summary>
    ''' id del grupo de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _idCareGroup As Integer
    ''' <summary>
    ''' tipo de servicio ips
    ''' </summary>
    ''' <remarks></remarks>
    Private _serviceManual As Integer
#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase
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
    ''' lista todos los grupos de servicios IPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroup() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ContractCareGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;DefaultManual;CareGroupType;CareGroupTypeName;StatusName;NumberDetail", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDefinitionRate() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(DefinitionRateXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las definiciones de tarifa por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDefinitionRateByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(DefinitionRateXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDefinitionRateXpCollection() As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(DefinitionRateXpo))
        Return collect
    End Function

    ''' <summary>
    ''' lista los grupos de atencion por tipo de liquidacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroupByLiquidationType(LiquidationType As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("LiquidationType=" & LiquidationType & "")
        classEntity = sessionNew.GetClassInfo(GetType(ContractCareGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;DefaultManual;CareGroupType;CareGroupTypeName;StatusName;LiquidationType", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los grupos de servicios IPS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroupByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(ContractCareGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;DefaultManual;CareGroupType;CareGroupTypeName;StatusName;EntityTypeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los servicios IPS por estado
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroupByStatusXpCollection(status As Boolean) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ContractCareGroupXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista los detalles de la definicion de tarifa enviada
    ''' </summary>
    ''' <param name="definitionRateId">id de la definición de tarifa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDefinitionRateDetailByDefinitionRateId(definitionRateId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DefinitionRateId.Id=" & definitionRateId & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(DefinitionRateDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista todos los grupos de servicios IPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillingConcept() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(BillingConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function
    ''' <summary>
    ''' lista los grupos de servicios IPS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesGroupByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(BillingConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;StatusName", criteria)
        Return serverMode
    End Function
    Public Function ListBillingConceptByTypeAndStatus(type As Integer, status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ConceptType=" & type & " And Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(BillingConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;StatusName", criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' lista todos los servicios IPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServices() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ContractIPSService))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;StatusName;Presentation;PresentationName;ServiceClass;ServiceClassName;ServiceManual;ServiceManualName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los servicios IPS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(ContractIPSService))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;Presentation;ServiceManual;PresentationName;ServiceManualName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los servicios IPS por estado
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesByStatusXpCollection(status As Boolean) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ContractIPSService), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista los servicios IPS no quirurgicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesNoSurgical(ByVal manualType As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & True & " And Presentation= 1 And ServiceManual=" & manualType & " And ServiceClass<>1 And ServiceClass<>6")
        classEntity = sessionNew.GetClassInfo(GetType(ContractIPSService))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ServiceClass;ServiceClassName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los servicios IPS no quirurgicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesByService(status As Boolean, serviceClass As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And ServiceClass=" & serviceClass & "")
        classEntity = sessionNew.GetClassInfo(GetType(ContractIPSService))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ServiceClass;ServiceClassName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los servicios IPS no quirurgicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesByServiceAndPresentation(status As Boolean, serviceClass As Integer, presentation As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And ServiceClass=" & serviceClass & " And Presentation=" & presentation & "")
        classEntity = sessionNew.GetClassInfo(GetType(ContractIPSService))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ServiceClass;ServiceClassName;Status;PresentationName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los servicios IPS con presentacion diferente a quirurgico y clase servicio ninguno
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesByPresentationAndServiceClass(status As Boolean, serviceClass As Integer, presentation As Integer, options As Integer, typeManual As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If options = 1 Then
            criteria = CriteriaOperator.Parse("Status=" & status & " And (ServiceClass=" & serviceClass & " Or ServiceClass is null) And Presentation <>" & presentation & " And ServiceManual=" & typeManual)
        Else
            If typeManual = 1 OrElse typeManual = 2 Then
                criteria = CriteriaOperator.Parse("Status=" & status & " And ServiceClass >" & serviceClass & " And Presentation=" & presentation & " And ServiceManual=" & typeManual & " And ServiceClass<>2" & " And ServiceClass<>3" & " And ServiceClass<>4")
            Else
                criteria = CriteriaOperator.Parse("Status=" & status & " And ServiceClass >" & serviceClass & " And Presentation=" & presentation & " And ServiceManual=" & typeManual)
            End If
        End If
        classEntity = sessionNew.GetClassInfo(GetType(ContractIPSService))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ServiceClass;ServiceClassName;Status;PresentationName;ServiceManualName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los servicios IPS con presentacion diferente a quirurgico y clase servicio ninguno
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesByServiceManualAndServiceClass(status As Boolean, serviceClass As Integer, typeManual As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("Status=" & status & " And ServiceClass=" & serviceClass & " And ServiceManual=" & typeManual)
        classEntity = sessionNew.GetClassInfo(GetType(ContractIPSService))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ServiceClass;ServiceClassName;Status;PresentationName;ServiceManualName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los servicios IPS con presentacion diferente a quirurgico y clase servicio ninguno
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServiceByServiceClassAndServiceManualAndPresentation(status As Boolean, serviceClass As Integer, typeManual As Integer, presentation As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("Status=" & status & " And ServiceClass=" & serviceClass & " And ServiceManual=" & typeManual & " And Presentation=" & presentation)
        classEntity = sessionNew.GetClassInfo(GetType(ContractIPSService))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;ServiceClass;ServiceClassName;Status;PresentationName;ServiceManualName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los grupos cups
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsGroup() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CupsGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Description;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas subgrupos cups
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsSubGroup() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CupsSubGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Description;CodeName;CupsGroupId.CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las entidades administradoras de salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthAdministrator() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(HealthAdministratorXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;HealthEntityCode;EntityType;EntityTypeName;ThirdPartyId.NitName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las entidades administradoras de salud por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthAdministratorByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(HealthAdministratorXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;HealthEntityCode;EntityType;EntityTypeName;ThirdPartyId.NitName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las entidades administradoras de salud por tipo de entidad
    ''' </summary>
    Public Function ListHealthAdministratorByType(type As Byte) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EntityType=" & type & " and Status = 1")
        classEntity = sessionNew.GetClassInfo(GetType(HealthAdministratorXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;HealthEntityCode;EntityType;EntityTypeName;ThirdPartyId.NitName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the health administratoy by code.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function ListHealthAdministratoyByCode(code As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Code=" & code & "")
        classEntity = sessionNew.GetClassInfo(GetType(HealthAdministratorXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;HealthEntityCode;EntityType;EntityTypeName;ThirdPartyId.NitName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the type of the health administrator by not.
    ''' </summary>
    ''' <param name="type">The type.</param>
    ''' <returns></returns>
    ''' <exception cref="System.NotImplementedException"></exception>
    Function ListHealthAdministratorByNotType(type As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EntityType!=" & type & "")
        classEntity = sessionNew.GetClassInfo(GetType(HealthAdministratorXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;HealthEntityCode;EntityType;EntityTypeName;ThirdPartyId.NitName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las entidades CUPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsEntity() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CupsEntityXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;CodeDescription;RIPSCode;RIPSDescription;Status;CUPSSubGroupId.CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las entidades CUPS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsEntityByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(CupsEntityXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;CodeDescription;RIPSCode;RIPSDescription;Status;CUPSSubGroupId.CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las entidades CUPS por estado para medicalFees
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsEntityByStatusXpCollection(status As Boolean) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(CupsEntityXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todos los detalles del cubrimiento
    ''' </summary>
    ''' <param name="procedureTemplateId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProcedureCupsByProcedureTemplateId(procedureTemplateId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ProceduresTemplateId=" & procedureTemplateId & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ProcedureCupsXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todas grupos cups por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsGroupByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(CupsGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Description;CodeName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las entidades CUPS por estado para medicalFees
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsGroupByStatusXpCollection(status As Boolean) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(CupsGroupXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todas subgrupos cups por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsSubGroupByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(CupsSubGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Description;CodeName;Status;CupsGroupId.CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas subgrupos cups por estado con xpcollection
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsSubGroupByStatusXpCollection(status As Boolean) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(CupsSubGroupXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todas las unidades de mercadeo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMarketingUnit() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(MarketingUnitXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContract() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ContractXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ContractName;ContractNumber;ContractObject;Status;CodeContractName;HealthAdministratorId.CodeName;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los contratos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractByStatus(status As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(ContractXpo))
        'Se quita el criteria porque anteriormente se estaban listando los contratos que estuvieran vigentes, pero ahora se debe listar todos los contratos (Vigentes, Suspendido y Terminado)
        'serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ContractName;ContractNumber;ContractObject;Status;CodeContractName;HealthAdministratorId.CodeName;StatusName", criteria)
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ContractName;ContractNumber;ContractObject;Status;CodeContractName;HealthAdministratorId.CodeName;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de productos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProductTemplate() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ProductTemplateXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las plantillas de productos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProductTemplateByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(ProductTemplateXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los rangos uvr
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListUVRRange() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(UVRRangeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;InitialUVR;EndUVR;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los rangos uvr por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListUVRRangeByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(UVRRangeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;InitialUVR;EndUVR", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los rangos uvr
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListGroupers() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GroupersXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;CodeName;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los rangos uvr por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListGroupersByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(GroupersXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de procedimientos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProcedureTemplate() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ProcedureTemplateXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las plantillas de procedimiento por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProcedureTemplateByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(ProcedureTemplateXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de requerimiento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRequirementTemplate() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(RequirementTemplateXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las plantillas de requerimiento por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRequirementTemplateByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(RequirementTemplateXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los salarios minimos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractMinimumWage() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ContractMinimumWageXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Value;ValueWithSurcharge;CodeName;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los salarios minimos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractMinimumWageByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(ContractMinimumWageXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Value;ValueWithSurcharge;CodeName;Status;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los manuales tarifarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRateManual() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(RateManualXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;StatusName;Type;TypeName;RoundService;RoundServiceName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los manuales tarifarios por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRateManualByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(RateManualXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;StatusName;Type;TypeName;RoundService;RoundServiceName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' consulta manual tarifario por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RateManualById(id As Integer) As XPCollection
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & id & "")
        Dim collect As XPCollection = New XPCollection(GetType(RateManualXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todos los grupos quirurgicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSurgicalGroup() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(SurgicalGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los grupos quirurgicos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSurgicalGroupByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(SurgicalGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;StatusName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las entidades de contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractEntity() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ContractEntityXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;StatusName;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las entidades de contratos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractEntityByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(ContractEntityXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;StatusName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las entidades de contratos por estado con xpcollection
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractEntityByStatusXpCollection(status As Boolean) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ContractEntityXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista todos los manuales de servicio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRateManualDetail() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(RateManualDetailXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;RateManualId.CodeName;IPSServiceId.CodeName;SurgicalGroupId.CodeName;StatusName;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los manuales de servicio por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRateManualDetailByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(RateManualDetailXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;RateManualId.CodeName;IPSServiceId.CodeName;SurgicalGroupId.CodeName;StatusName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los manuales de servicio por manual tarifario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRateManualDetailByRateManualId(idRateManual As Integer) As XPCollection
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("RateManualId.Id=" & idRateManual)
        Dim collect As XPCollection = New XPCollection(GetType(RateManualDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista la asociacion entre procedureCups y marketingUnitCups
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListVProcedureMarketingUnitCUPS(idProcedureTemplate As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ProceduresTemplateId=" & idProcedureTemplate)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(VProcedureMarketingUnitCUPS), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista el detalle del grupo de atencion
    ''' </summary>
    ''' <param name="careGroupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroupRateCollection(careGroupId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId=" & careGroupId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ContractCareGroupRateXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Obtiene el grupo de atencion
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCareGroupById(Id As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & Id)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ContractCareGroupXpo), criteria)
        Return collect
    End Function

#End Region

#Region "ListIssServicesByCareGroup"
    ''' <summary>
    ''' lista los ISS por grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIssServicesByCareGroup(careGroupId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId=" & careGroupId)
        classEntity = sessionNew.GetClassInfo(GetType(ContractViewListIssByCareGroupIdXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListIssServicesByCareGroupNotQx(careGroupId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId=" & careGroupId & " AND Presentation != 2")
        classEntity = sessionNew.GetClassInfo(GetType(ContractViewListIssByCareGroupIdXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListSoatByCareGroupNotQx(idCareGroup As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId=" & idCareGroup & " AND Presentation != 2")
        classEntity = sessionNew.GetClassInfo(GetType(ContractViewListSoatByCareGroupIdXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' ista los ISS por grupo de atencion y presentación
    ''' </summary>
    ''' <param name="idCareGroup">The identifier care group.</param>
    ''' <param name="presentation">The presentation.</param>
    ''' <returns></returns>
    Public Function ListIssServicesByCareGroupAndPresentation(idCareGroup As Integer, presentation As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId = ? And Presentation = ?", idCareGroup, presentation)
        classEntity = sessionNew.GetClassInfo(GetType(ContractViewListIssByCareGroupIdXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function
#End Region

#Region "ListSoatByCareGroup"
    ''' <summary>
    ''' lista los SOAT por grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSoatByCareGroup(careGroupId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId=" & careGroupId)
        classEntity = sessionNew.GetClassInfo(GetType(ContractViewListSoatByCareGroupIdXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los SOAT por grupo de atencion y presentacion
    ''' </summary>
    Public Function ListSoatByCareGroupAndPresentation(caregroupId As Object, presentation As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId = ? And Presentation = ?", caregroupId, presentation)
        classEntity = sessionNew.GetClassInfo(GetType(ContractViewListSoatByCareGroupIdXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function
#End Region

#Region "ListCUPS"
    ''' <summary>
    ''' Objeto de consulta
    ''' </summary>
    Private WithEvents linqListCUPS As New LinqInstantFeedbackSource
    ''' <summary>
    ''' Lista todos los servicios IPS o SOAT
    ''' </summary>
    Public Function ListCUPS(idCareGroup As Integer) As LinqInstantFeedbackSource
        linqListCUPS.KeyExpression = "Id"
        _idCareGroup = idCareGroup
        Return linqListCUPS
    End Function
    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub linqListCUPS_GetQueryable(sender As Object, e As GetQueryableEventArgs) Handles linqListCUPS.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableCareGroup As XPQuery(Of ContractCareGroupXpo) = New XPQuery(Of ContractCareGroupXpo)(sessionNew)
            Dim tableProcedureCups As XPQuery(Of ContractProcedureCupsXpo) = New XPQuery(Of ContractProcedureCupsXpo)(sessionNew)
            Dim tableCUPS As XPQuery(Of CupsEntityXpo) = New XPQuery(Of CupsEntityXpo)(sessionNew)

            Dim TmpQueryableSource = From cg In tableCareGroup
                                     Join pc In tableProcedureCups On cg.ProcedureTemplateId.Id Equals pc.ProceduresTemplateId.Id
                                     Join cups In tableCUPS On pc.CupsId.Id Equals cups.Id Where cg.Id = _idCareGroup And cups.Status = True Select cups

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableCUPS
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
#End Region

End Class
