'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.CrystalRepository
' Author           : Juan F. Tamayo
' Created          : 2016-02-08
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class ContractServiceXpoEx
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

    ''' <summary>
    ''' Devuelve la entidad previamente enviada
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="Fun"></param>
    ''' <param name="criteria"></param>
    ''' <param name="withSort"></param>
    ''' <returns></returns>
    Public Async Function GetCollectionAsync(Of T)(Optional ByVal Fun As Func(Of T, Boolean) = Nothing, Optional ByVal criteria As String = Nothing, Optional ByVal withSort As Boolean = True) As Threading.Tasks.Task(Of List(Of T))
        Dim result As Object = Nothing
        Await Threading.Tasks.Task.Factory.StartNew(Sub()
                                                        result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria, withSort:=withSort)
                                                        result.Sort()
                                                    End Sub)

        Return DirectCast(result, List(Of T))
    End Function


    ''' <summary>
    ''' lista todos los grupos de servicios IPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractAccountingStructure() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractAccountingStructureXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ContractAccountingStructureXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CareGroupType;CareGroupTypeName;Status;StatusName;CodeName", Nothing)
    End Function

    Public Function GetHealthAdministratorXpo(id As Integer) As HealthAdministratorXpo
        Dim session As New IndigoXPOSession(Of HealthAdministratorXpo)()
        Return session.GetObjectByKey(Of HealthAdministratorXpo)(id)
    End Function

    Public Function GetContractCareGroupXpo(id As Integer) As ContractCareGroupXpo
        Dim session As New IndigoXPOSession(Of ContractCareGroupXpo)()
        Return session.GetObjectByKey(Of ContractCareGroupXpo)(id)
    End Function

    ''' <summary>
    ''' lista todos los grupos de servicios IPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroup() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractCareGroupXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ContractCareGroupXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;DefaultManual;CareGroupType;CareGroupTypeName;StatusName;NumberDetail", Nothing)
    End Function

    ''' <summary>
    ''' lista todos los grupos de servicios IPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllCareGroup() As XPCollection
        Dim session As New IndigoXPOSession(Of ContractCareGroupXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ContractCareGroupXpo))
        Return New XPCollection(session, GetType(ContractCareGroupXpo))
    End Function


    ''' <summary>
    ''' lista todos los grupos de atencion por tercero
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroupByThirdParty(ThirdPartyId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewContractCareGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ThirdPartyId=" & ThirdPartyId & "")
        Dim _classEntity = session.GetClassInfo(GetType(ViewContractCareGroupXpo))
        Dim serverMode = New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;StatusName;ThirdPartyId;CareGroupTypeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDefinitionRate() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DefinitionRateXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(DefinitionRateXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' lista todas las parametrizaciones de Servicios no facturables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillingItemsRestrictionXpo() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingItemsRestrictionXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(BillingItemsRestrictionXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Status", Nothing)
    End Function

    ''' <summary>
    ''' lista las definiciones de tarifa por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDefinitionRateByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DefinitionRateXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(DefinitionRateXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName", criteria)
    End Function

    ''' <summary>
    ''' lista los Servicios no facturables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillingItemsRestrictionStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingItemsRestrictionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(BillingItemsRestrictionXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName", criteria)
    End Function

    ''' <summary>
    ''' lista las definiciones de tarifa por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractPackage(careGroupId As Integer, status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractPackageXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Status={status} && CareGroupPackages[CareGroupId={careGroupId}]")
        Dim _classEntity = session.GetClassInfo(GetType(ContractPackageXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' lista las definiciones de tarifa por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractAccountingStructureByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractAccountingStructureXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(ContractAccountingStructureXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CareGroupType;CareGroupTypeName;Status;StatusName;CodeName", criteria)
    End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDefinitionRateXpCollection() As XPCollection
        Dim session As New IndigoXPOSession(Of DefinitionRateXpo)()
        Return New XPCollection(session, GetType(DefinitionRateXpo))
    End Function

    ''' <summary>
    ''' lista los grupos de atencion por tipo de liquidacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroupByLiquidationTypeAndyStatus(liquidationType As Integer, status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractCareGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("LiquidationType = {0} AND Status = {1}", liquidationType, status))
        Dim _classEntity = session.GetClassInfo(GetType(ContractCareGroupXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;DefaultManual;CareGroupType;CareGroupTypeName;StatusName;EntityTypeName", criteria)
    End Function

    Public Function ListAllGroupers() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GroupersXpo)()
        Dim classEntity = session.GetClassInfo(GetType(GroupersXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
    End Function

    ''' <summary>
    ''' lista los grupos de servicios IPS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroupByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractCareGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(ContractCareGroupXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;DefaultManual;CareGroupType;CareGroupTypeName;StatusName;EntityTypeName", criteria)
    End Function

    ''' <summary>
    ''' lista los grupos de servicios IPS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroupByStatusAndLiquidationType(status As Boolean, liquidationType As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractCareGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("Status={0} And LiquidationType In ({1})", status, liquidationType))
        Dim _classEntity = session.GetClassInfo(GetType(ContractCareGroupXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;DefaultManual;CareGroupType;CareGroupTypeName;StatusName;EntityTypeName", criteria)
    End Function

    Public Function InitializeGrouperWithOut(code As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GroupersXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Code <> '" & code & "'")
        Dim classEntity = session.GetClassInfo(GetType(GroupersXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' lista todos los servicios IPS por estado
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroupByStatusXpCollection(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of ContractCareGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Return New XPCollection(session, GetType(ContractCareGroupXpo), criteria)
    End Function

    ''' <summary>
    ''' lista los detalles de la definicion de tarifa enviada
    ''' </summary>
    ''' <param name="definitionRateId">id de la definición de tarifa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDefinitionRateDetailByDefinitionRateId(definitionRateId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of DefinitionRateDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DefinitionRateId.Id=" & definitionRateId & "")
        Return New XPCollection(session, GetType(DefinitionRateDetailXpo), criteria)
    End Function

    ''' <summary>
    ''' lista todos los grupos de servicios IPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillingConcept() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingConceptXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(BillingConceptXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
    End Function
    ''' <summary>
    ''' lista los grupos de servicios IPS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesGroupByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingConceptXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(BillingConceptXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;StatusName", criteria)
    End Function

    Public Function ListBillingConceptByTypeAndStatus(type As Integer, status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BillingConceptXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ConceptType=" & type & " And Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(BillingConceptXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;StatusName", criteria)
    End Function

    ''' <summary>
    ''' lista todos los servicios IPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServices() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractIPSServiceXPO)()
        Dim _classEntity = session.GetClassInfo(GetType(ContractIPSServiceXPO))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;StatusName;Presentation;PresentationName;ServiceClass;ServiceClassName;ServiceManual;ServiceManualName", Nothing)
    End Function

    ''' <summary>
    ''' lista todos los servicios IPS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractIPSServiceXPO)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(ContractIPSServiceXPO))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;Presentation;ServiceManual;PresentationName;ServiceManualName", criteria)
    End Function

    ''' <summary>
    ''' lista todos los servicios IPS por estado
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesByStatusXpCollection(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of ContractIPSServiceXPO)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Return New XPCollection(session, GetType(ContractIPSServiceXPO), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' lista todos los servicios IPS por estado con sus respectivos homologos
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewListIPSServiceWithHomologations(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of ViewListIPSServiceWithHomologationsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Return New XPCollection(session, GetType(ViewListIPSServiceWithHomologationsXpo), criteria)
    End Function

    ''' <summary>
    ''' lista los servicios IPS no quirurgicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesNoSurgical(ByVal manualType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractIPSServiceXPO)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & True & " And Presentation= 1 And ServiceManual=" & manualType & " And ServiceClass<>1")
        Dim _classEntity = session.GetClassInfo(GetType(ContractIPSServiceXPO))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;ServiceClass;ServiceClassName;Status", criteria)
    End Function

    ''' <summary>
    ''' lista los servicios IPS no quirurgicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesByService(status As Boolean, serviceClass As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractIPSServiceXPO)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And ServiceClass=" & serviceClass & "")
        Dim _classEntity = session.GetClassInfo(GetType(ContractIPSServiceXPO))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;ServiceClass;ServiceClassName;Status", criteria)
    End Function

    ''' <summary>
    ''' lista los servicios IPS no quirurgicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesByServiceAndPresentation(status As Boolean, serviceClass As Integer, presentation As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractIPSServiceXPO)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And ServiceClass=" & serviceClass & " And Presentation=" & presentation & "")
        Dim _classEntity = session.GetClassInfo(GetType(ContractIPSServiceXPO))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;ServiceClass;ServiceClassName;Status;PresentationName", criteria)
    End Function


    ''' <summary>
    ''' lista los servicios IPS por el campo Presentation
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServiceByStatusAndPresentation(status As Boolean, presentation As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractIPSServiceXPO)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And Presentation=" & presentation & "")
        Dim _classEntity = session.GetClassInfo(GetType(ContractIPSServiceXPO))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;ServiceClass;ServiceClassName;Status;PresentationName", criteria)
    End Function

    ''' <summary>
    ''' lista los servicios IPS con presentacion diferente a quirurgico y clase servicio ninguno
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesByPresentationAndServiceClass(status As Boolean, serviceClass As Integer, presentation As Integer, options As Integer, typeManual As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractIPSServiceXPO)()
        Dim criteria As CriteriaOperator
        If options = 1 Then
            If typeManual = 4 Then
                criteria = CriteriaOperator.Parse("Status=" & status & " And Presentation <> " & presentation & " And ServiceManual = " & typeManual)
            Else
                criteria = CriteriaOperator.Parse("Status=" & status & " And (ServiceClass=" & serviceClass & " Or ServiceClass is null) And Presentation <>" & presentation & " And ServiceManual=" & typeManual)
            End If
        Else
            If typeManual = 1 OrElse typeManual = 2 Then
                criteria = CriteriaOperator.Parse("Status=" & status & " And ServiceClass >" & serviceClass & " And Presentation=" & presentation & " And ServiceManual=" & typeManual & " And ServiceClass<>2" & " And ServiceClass<>3" & " And ServiceClass<>4")
            ElseIf typeManual = 4 Then
                criteria = CriteriaOperator.Parse("Status=" & status & " And ServiceClass > " & serviceClass & " And Presentation = 1 And ServiceManual=" & typeManual)
            Else
                criteria = CriteriaOperator.Parse("Status=" & status & " And ServiceClass >" & serviceClass & " And Presentation=" & presentation & " And ServiceManual=" & typeManual)
            End If
        End If
        Dim _classEntity = session.GetClassInfo(GetType(ContractIPSServiceXPO))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;ServiceClass;ServiceClassName;Status;PresentationName;ServiceManualName", criteria)
    End Function

    ''' <summary>
    ''' lista los servicios IPS con presentacion diferente a quirurgico y clase servicio ninguno
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServicesByServiceManualAndServiceClass(status As Boolean, serviceClass As Integer, typeManual As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractIPSServiceXPO)()
        Dim criteria = CriteriaOperator.Parse("Status=" & status & " And ServiceClass=" & serviceClass & " And ServiceManual=" & typeManual)
        Dim _classEntity = session.GetClassInfo(GetType(ContractIPSServiceXPO))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;ServiceClass;ServiceClassName;Status;PresentationName;ServiceManualName", criteria)
    End Function

    ''' <summary>
    ''' lista los servicios IPS con presentacion diferente a quirurgico y clase servicio ninguno
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSServiceByServiceClassAndServiceManualAndPresentation(status As Boolean, serviceClass As Integer, typeManual As Integer, presentation As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractIPSServiceXPO)()
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("Status=" & status & " And ServiceClass=" & serviceClass & " And ServiceManual=" & typeManual & " And Presentation=" & presentation)
        Dim _classEntity = session.GetClassInfo(GetType(ContractIPSServiceXPO))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;ServiceClass;ServiceClassName;Status;PresentationName;ServiceManualName", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los grupos cups
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsGroup() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CupsGroupXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CupsGroupXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Description;CodeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los grupos cups
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCompanyType(Optional Type As Byte? = Nothing) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CompanyTypeXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CompanyTypeXpo))
        Dim criteria = If(Type IsNot Nothing, CriteriaOperator.Parse($"Type ={Type}"), Nothing)
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Type;CodeName;TypeName", criteria)
    End Function

    ''' <summary>
    ''' Lista todas subgrupos cups
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsSubGroup() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CupsSubGroupXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CupsSubGroupXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Description;CodeName;CupsGroupId.CodeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todas las entidades administradoras de salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthAdministrator() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HealthAdministratorXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(HealthAdministratorXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;HealthEntityCode;EntityType;EntityTypeName;ThirdPartyId.NitName;CompanyType.Name", Nothing)
    End Function

    ''' <summary>
    ''' Lista todas las entidades administradoras de salud por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthAdministratorByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HealthAdministratorXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(HealthAdministratorXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;HealthEntityCode;EntityType;EntityTypeName;ThirdPartyId.NitName;Status", criteria)
    End Function

    ''' <summary>
    ''' Lista las liquidaciones por periodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCollectionHealthAdministratorByStatus(status As Boolean) As XPCollection(Of HealthAdministratorXpo)
        Dim session As New IndigoXPOSession(Of HealthAdministratorXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim list = New XPCollection(Of HealthAdministratorXpo)(session, criteria)
        Return list
    End Function

    ''' <summary>
    ''' Lista todas las entidades administradoras de salud por tipo de entidad
    ''' </summary>
    Public Function ListHealthAdministratorByType(type As Byte) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HealthAdministratorXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EntityType=" & type & " and Status = 1")
        Dim _classEntity = session.GetClassInfo(GetType(HealthAdministratorXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;HealthEntityCode;EntityType;CompanyType.Name;ThirdPartyId.NitName;Status", criteria)
    End Function

    ''' <summary>
    ''' Lists the health administratoy by code.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function ListHealthAdministratoyByCode(code As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HealthAdministratorXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Code=" & code & "")
        Dim _classEntity = session.GetClassInfo(GetType(HealthAdministratorXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;HealthEntityCode;EntityType;EntityTypeName;ThirdPartyId.NitName;Status", criteria)
    End Function

    ''' <summary>
    ''' Lists the type of the health administrator by not.
    ''' </summary>
    ''' <param name="type">The type.</param>
    ''' <returns></returns>
    ''' <exception cref="System.NotImplementedException"></exception>
    Function ListHealthAdministratorByNotType(type As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HealthAdministratorXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("EntityType!=" & type & "")
        Dim _classEntity = session.GetClassInfo(GetType(HealthAdministratorXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;HealthEntityCode;EntityType;EntityTypeName;ThirdPartyId.NitName;Status", criteria)
    End Function

    ''' <summary>
    ''' Lista todas las entidades CUPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsEntity() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CupsEntityXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CupsEntityXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Description;CodeDescription;RIPSCode;RIPSDescription;Status;CUPSSubGroupId.CodeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todas los Paquetes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractPackages(Optional Status? As Boolean = Nothing) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractPackageXpo)()
        Dim criteria As CriteriaOperator = Nothing
        If Status IsNot Nothing Then
            criteria = CriteriaOperator.Parse("Status=" & Status & "")
        End If
        Dim _classEntity = session.GetClassInfo(GetType(ContractPackageXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Description;CodeName;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista todas las entidades CUPS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsEntityByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CupsEntityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(CupsEntityXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Description;CodeDescription;RIPSCode;RIPSDescription;Status;CUPSSubGroupId.CodeName;SubGroupCodeName;CUPSSubGroupId.CupsGroupId.CodeName,GroupCodeName", criteria)
    End Function

    ''' <summary>
    ''' Lista todas las entidades CUPS por estado para medicalFees
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsEntityByStatusXpCollection(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of CupsEntityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Return New XPCollection(session, GetType(CupsEntityXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Lista todos los detalles del cubrimiento
    ''' </summary>
    ''' <param name="procedureTemplateId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProcedureCupsByProcedureTemplateId(procedureTemplateId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of ProcedureCupsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ProceduresTemplateId=" & procedureTemplateId & "")
        Return New XPCollection(session, GetType(ProcedureCupsXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Lista todas grupos cups por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsGroupByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CupsGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(CupsGroupXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Description;CodeName;Status;ImagingType", criteria)
    End Function

    ''' <summary>
    ''' Lista todas las entidades CUPS por estado para medicalFees
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsGroupByStatusXpCollection(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of CupsGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Return New XPCollection(session, GetType(CupsGroupXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Lista todas subgrupos cups por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsSubGroupByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CupsSubGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(CupsSubGroupXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Description;CodeName;Status;CupsGroupId.CodeName", criteria)
    End Function

    ''' <summary>
    ''' Lista todas subgrupos cups por estado con xpcollection
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsSubGroupByStatusXpCollection(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of CupsSubGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Return New XPCollection(session, GetType(CupsSubGroupXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Lista todas las unidades de mercadeo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMarketingUnit() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MarketingUnitXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(MarketingUnitXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todas las unidades de mercadeo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractDescriptions() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractDescriptionsXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ContractDescriptionsXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todas las unidades de mercadeo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDiscountTypes() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DiscountTypesXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(DiscountTypesXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;DiscountType;DiscountPercentage", Nothing)
    End Function

    ''' <summary>
    ''' Lista todas las unidades de mercadeo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractDescriptionsByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractDescriptionsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & Status & "")
        Dim _classEntity = session.GetClassInfo(GetType(ContractDescriptionsXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista las descripciones asociadas al cups por el id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractDescriptionsByCupsEntityId(CupsEntityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CUPSEntityContractDescriptionsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CUPSEntityId.Id = " & CupsEntityId & " AND IsDelete = false")
        Dim _classEntity = session.GetClassInfo(GetType(CUPSEntityContractDescriptionsXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;ContractDescriptionId.Id;ContractDescriptionId.Code;ContractDescriptionId.Name;ContractDescriptionId.CodeName;ContractDescriptionId.Status;ContractDescriptionId.StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista las descripciones asociadas al cups por el código
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractDescriptionsByCupsEntityCode(CupsEntityCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CUPSEntityContractDescriptionsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"CUPSEntityId.Code = '{CupsEntityCode}' AND IsDelete =0 ")
        Dim _classEntity = session.GetClassInfo(GetType(CUPSEntityContractDescriptionsXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;ContractDescriptionId.Id;ContractDescriptionId.Code;ContractDescriptionId.Name;ContractDescriptionId.CodeName;ContractDescriptionId.Status;ContractDescriptionId.StatusName;CUPSEntityId.Code", criteria)
    End Function

    Public Function ListContractDescriptionsByStatusXpCollection(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of ContractDescriptionsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Return New XPCollection(session, GetType(ContractDescriptionsXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Lista todos los contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContract() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ContractXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;ContractName;ContractNumber;ContractObject;Status;CodeContractName;HealthAdministratorId.CodeName;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractOpenSearch() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListContractsOpenSearchXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ViewListContractsOpenSearchXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractSearchLookUp(status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListContractsSearchLookUpXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(ViewListContractsSearchLookUpXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista los contratos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractByStatus(status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(ContractXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;ContractName;ContractNumber;ContractObject;Status;CodeContractName;HealthAdministratorId.CodeName;Status;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de productos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProductTemplate() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ProductTemplateXpo)
        Dim _classEntity = session.GetClassInfo(GetType(ProductTemplateXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Status;CodeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista las plantillas de productos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProductTemplateByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ProductTemplateXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(ProductTemplateXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Status;CodeName", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los rangos uvr
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListUVRRange() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of UVRRangeXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(UVRRangeXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;InitialUVR;EndUVR;Status", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los rangos uvr por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListUVRRangeByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of UVRRangeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(UVRRangeXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;InitialUVR;EndUVR", criteria)
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de procedimientos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProcedureTemplate() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ProcedureTemplateXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ProcedureTemplateXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista las plantillas de procedimiento por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProcedureTemplateByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ProcedureTemplateXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(ProcedureTemplateXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de requerimiento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRequirementTemplate() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RequirementTemplateXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(RequirementTemplateXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista las plantillas de requerimiento por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRequirementTemplateByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RequirementTemplateXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(RequirementTemplateXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los salarios minimos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractMinimumWage() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractMinimumWageXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ContractMinimumWageXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Value;ValueWithSurcharge;CodeName;Status;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista los salarios minimos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractMinimumWageByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractMinimumWageXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(ContractMinimumWageXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Value;ValueWithSurcharge;CodeName;Status;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los manuales tarifarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRateManual() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RateManualXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(RateManualXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName;Type;TypeName;RoundService;RoundServiceName", Nothing)
    End Function

    ''' <summary>
    ''' Lista los manuales tarifarios por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRateManualValidityByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RateManualValidityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(RateManualValidityXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista los manuales tarifarios por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRateManualByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RateManualXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(RateManualXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName;Type;TypeName;RoundService;RoundServiceName", criteria)
    End Function

    ''' <summary>
    ''' consulta manual tarifario por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RateManualById(id As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of RateManualXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & id & "")
        Dim collect As XPCollection = New XPCollection(session, GetType(RateManualXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todos los grupos quirurgicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSurgicalGroup() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of SurgicalGroupXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(SurgicalGroupXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista los grupos quirurgicos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSurgicalGroupByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of SurgicalGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(SurgicalGroupXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista todas las entidades de contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractEntity() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractEntityXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ContractEntityXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;StatusName;Status", Nothing)
    End Function

    ''' <summary>
    ''' Lista todas las entidades de contratos por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractEntityByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractEntityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(ContractEntityXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;StatusName;Status", criteria)
    End Function

    ''' <summary>
    ''' Lista todas las entidades de contratos por estado con xpcollection
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractEntityByStatusXpCollection(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of ContractEntityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Return New XPCollection(session, GetType(ContractEntityXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' lista todos los manuales de servicio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRateManualDetail() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RateManualDetailXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(RateManualDetailXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;RateManualId.CodeName;IPSServiceId.CodeName;SurgicalGroupId.CodeName;StatusName;Status", Nothing)
    End Function

    ''' <summary>
    ''' Lista los manuales de servicio por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRateManualDetailByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RateManualDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(RateManualDetailXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;RateManualId.CodeName;IPSServiceId.CodeName;SurgicalGroupId.CodeName;StatusName;Status", criteria)
    End Function

    ''' <summary>
    ''' Lista los manuales de servicio por manual tarifario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRateManualDetailByRateManualId(idRateManual As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of RateManualDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("RateManualId.Id=" & idRateManual)
        Dim collect As XPCollection = New XPCollection(session, GetType(RateManualDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista la asociacion entre procedureCups y marketingUnitCups
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListVProcedureMarketingUnitCUPS(idProcedureTemplate As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of VProcedureMarketingUnitCUPS)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ProceduresTemplateId=" & idProcedureTemplate)
        Return New XPCollection(session, GetType(VProcedureMarketingUnitCUPS), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Lista el detalle del grupo de atencion
    ''' </summary>
    ''' <param name="careGroupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroupRateCollection(careGroupId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of ContractCareGroupRateXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId=" & careGroupId)
        Return New XPCollection(session, GetType(ContractCareGroupRateXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Obtiene el grupo de atencion
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCareGroupById(Id As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of ContractCareGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & Id)
        Return New XPCollection(session, GetType(ContractCareGroupXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' lista los SOAT por grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSoatByCareGroup(careGroupId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractViewListSoatByCareGroupIdXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId=" & careGroupId)
        Dim _classEntity = session.GetClassInfo(GetType(ContractViewListSoatByCareGroupIdXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    Public Function ListSoatByCareGroupNotQx(idCareGroup As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractViewListSoatByCareGroupIdXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId=" & idCareGroup & " AND Presentation != 2")
        Dim _classEntity = session.GetClassInfo(GetType(ContractViewListSoatByCareGroupIdXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' lista los SOAT por grupo de atencion y presentacion
    ''' </summary>
    Public Function ListSoatByCareGroupAndPresentation(caregroupId As Object, presentation As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractViewListSoatByCareGroupIdXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId = ? And Presentation = ?", caregroupId, presentation)
        Dim _classEntity = session.GetClassInfo(GetType(ContractViewListSoatByCareGroupIdXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' lista los ISS por grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIssServicesByCareGroup(careGroupId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractViewListIssByCareGroupIdXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId=" & careGroupId)
        Dim _classEntity = session.GetClassInfo(GetType(ContractViewListIssByCareGroupIdXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    Public Function ListIssServicesByCareGroupNotQx(careGroupId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractViewListIssByCareGroupIdXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId=" & careGroupId & " AND Presentation != 2")
        Dim _classEntity = session.GetClassInfo(GetType(ContractViewListIssByCareGroupIdXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' ista los ISS por grupo de atencion y presentación
    ''' </summary>
    ''' <param name="idCareGroup">The identifier care group.</param>
    ''' <param name="presentation">The presentation.</param>
    ''' <returns></returns>
    Public Function ListIssServicesByCareGroupAndPresentation(idCareGroup As Integer, presentation As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractViewListIssByCareGroupIdXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId = ? And Presentation = ?", idCareGroup, presentation)
        Dim _classEntity = session.GetClassInfo(GetType(ContractViewListIssByCareGroupIdXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todos los rangos uvr
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListGroupers() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GroupersXpo)()
        Dim classEntity = session.GetClassInfo(GetType(GroupersXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Description;CodeName;Status;StatusName;ParentId", Nothing)
    End Function

    ''' <summary>
    ''' Lists the groupers.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGroupersCollection() As XPCollection(Of GroupersXpo)
        Dim session As New IndigoXPOSession(Of GroupersXpo)()
        Return New XPCollection(Of GroupersXpo)(session)
        'End Using
    End Function

    ''' <summary>
    ''' Lista todos los rangos uvr por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListGroupersByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GroupersXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(GroupersXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Description;CodeName;Status", criteria)
    End Function

    ''' <summary>
    ''' lista todos los grupos de servicios IPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRateManualValidity() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RateManualValidityXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(RateManualValidityXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Status;StatusName;CodeName", Nothing)
    End Function

    ''' <summary>
    ''' lista todos los grupos de servicios IPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTechnicalNote() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TechnicalNoteXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(TechnicalNoteXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Status;StatusName;CodeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista los manuales tarifarios por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTechnicalNoteByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TechnicalNoteXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(TechnicalNoteXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;CodeName;Status;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista los grupos de servicios RIPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRIPSServiceGroups() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RIPSServiceGroupsXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(RIPSServiceGroupsXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Status;StatusName;CodeName", Nothing)
    End Function

    Public Function ListRIPSServices() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RIPSServicesXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(RIPSServicesXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Status;StatusName;CodeName", Nothing)
    End Function

    ''' <summary>
    '''  Lista los registros de la tabla de CareGroupLiquidation por id de la tabla encabezado
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCareGroupMixLiquidationByIdCareGroup(Id As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of CareGroupMixLiquidationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareGroupId=" & Id & "")
        Return New XPCollection(session, GetType(CareGroupMixLiquidationXpo), criteria)
    End Function

#Region "ListCUPS"

    ''' <summary>
    ''' id del grupo de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _idCareGroup As Integer

    Private _cupsId As List(Of Integer)
    ' ''' <summary>
    ' ''' Objeto de consulta
    ' ''' </summary>
    'Private WithEvents linqListCUPS As LinqInstantFeedbackSource
    ''' <summary>
    ''' Lista todos los servicios IPS o SOAT
    ''' </summary>
    Public Function ListCUPS(idCareGroup As Integer, Optional CupsId As List(Of Integer) = Nothing) As LinqInstantFeedbackSource
        Dim linqListCUPS As New LinqInstantFeedbackSource
        AddHandler linqListCUPS.GetQueryable, AddressOf linqListCUPS_GetQueryable
        linqListCUPS.KeyExpression = "Id"
        _idCareGroup = idCareGroup
        _cupsId = CupsId
        Return linqListCUPS
    End Function
    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub linqListCUPS_GetQueryable(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableCareGroup As New XPQuery(Of ContractCareGroupXpo)(session)
            Dim tableProcedureCups As New XPQuery(Of ContractProcedureCupsXpo)(session)
            Dim tableCUPS As New XPQuery(Of CupsEntityXpo)(session)

            Dim TmpQueryableSource = From cg In tableCareGroup
                                     Join pc In tableProcedureCups On cg.ProcedureTemplateId.Id Equals pc.ProceduresTemplateId.Id
                                     Join cups In tableCUPS On pc.CupsId.Id Equals cups.Id
                                     Where cg.Id = _idCareGroup And cups.Status = True And (_cupsId Is Nothing OrElse _cupsId.Contains(pc.CupsId.Id))
                                     Group cups By Id = cups.Id, Code = cups.Code, Description = cups.Description, CodeDescription = cups.CodeDescription Into Group
                                     Select Id, Code, Description, CodeDescription

            e.QueryableSource = TmpQueryableSource.AsQueryable()
            'e.Tag = tableCUPS
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#End Region

#Region "ListHealthAdministrator"

    ''' <summary>
    ''' Lista de contratos
    ''' </summary>
    ''' <remarks></remarks>
    Private _LContract As List(Of Integer)
    ' ''' <summary>
    ' ''' Objeto de consulta
    ' ''' </summary>
    'Private WithEvents linqListHealthAdministrator As LinqInstantFeedbackSource
    ''' <summary>
    ''' Lista las administradoras de salud de una lista de contratos
    ''' </summary>
    Public Function ListHealthAdministrator(lContract As List(Of Integer)) As LinqInstantFeedbackSource
        Dim linqListHealthAdministrator As New LinqInstantFeedbackSource
        AddHandler linqListHealthAdministrator.GetQueryable, AddressOf linqListHealthAdministrator_GetQueryable
        linqListHealthAdministrator.KeyExpression = "Id"
        _LContract = lContract
        Return linqListHealthAdministrator
    End Function
    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub linqListHealthAdministrator_GetQueryable(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableContract As New XPQuery(Of ContractXpo)(session)
            Dim tableHealthAdministrator As New XPQuery(Of HealthAdministratorXpo)(session)

            Dim TmpQueryableSource = From c In tableContract
                                     Where _LContract.Contains(c.Id) Select c.HealthAdministratorId

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableHealthAdministrator
            'End Using
        Catch ex As Exception
            Console.WriteLine(ex.Message)
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