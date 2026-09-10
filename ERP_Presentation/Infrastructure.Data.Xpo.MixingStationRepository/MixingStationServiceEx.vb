'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.MixingStationRepostory
' Author           : Yoe Andres Cardenas
' Created          : 30/04/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Data.Filtering
Imports DevExpress.Data.PLinq
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Microsoft.VisualBasic

#End Region

Public Class MixingStationServiceXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"

    ''' <summary>
    ''' Lista los detalles de las campañas
    ''' </summary>
    Public Function ListViewItemsCampaigns(campaignDetailId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewItemsCampaignsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CampaignDetailId = " & campaignDetailId & "")
        Dim classEntity = session.GetClassInfo(GetType(ViewItemsCampaignsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' funcion para consultar todas las central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewMixingStations() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CMConfigurationXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CMConfigurationXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' funcion para consultar los usuarios autorizados para la campaña
    ''' </summary>
    ''' <returns></returns>
    Public Function ListUsersCampaing(CampaingDetailId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewUsersRolsCMXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CampaignDetailId = " & CampaingDetailId)
        Dim classEntity = session.GetClassInfo(GetType(ViewUsersRolsCMXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "UserId;CampaignDetailId;UserRoleName;Nombre", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' funcion para consultar los estados de las adecuaciones de una campaña
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewRequestDetailStatus(CampaignDetailId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewRequestDetailStatusXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CampaignDetailId = " & CampaignDetailId)
        Dim classEntity = session.GetClassInfo(GetType(ViewRequestDetailStatusXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "CampaignDetailId;RequestMixingStationId;StatusName;Quantity", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las ordenes de producción
    ''' </summary>
    Public Function ListViewProductionOrder(cmConfigurationId As Integer, productionLineId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewProductionOrderXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CMConfigurationId = " & cmConfigurationId & " and ProductionLineId = " & productionLineId)
        Dim classEntity = session.GetClassInfo(GetType(ViewProductionOrderXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las ordenes de histórico campañas
    ''' </summary>
    Public Function ListViewHistoricCampaign(cmConfigurationId As Integer, productionLineId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewHistoricCampaingXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CMConfigurationId = " & cmConfigurationId & " and ProductionLineId = " & productionLineId)
        Dim classEntity = session.GetClassInfo(GetType(ViewHistoricCampaingXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las solicitudes asociadas a la campaña
    ''' </summary>
    Public Function ListViewListDashboardProductionSchedule(cmConfigurationId As Integer, productionLineId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListDashboardProductionScheduleXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CMConfigurationId = " & cmConfigurationId & " and ProductionLineId = " & productionLineId)
        Dim classEntity = session.GetClassInfo(GetType(ViewListDashboardProductionScheduleXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los centros de atención externos por permiso de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCampaignDetailXPInstantFeedbackSource(campaignId As Integer, Optional StatusFilter As String = Nothing) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CampaignDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("CampaignId.Id = " & campaignId & "{0}", If(StatusFilter Is Nothing, "", "AND CampaignStatus IN (" & StatusFilter & " )")))
        Dim classEntity = session.GetClassInfo(GetType(CampaignDetailXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)

        Return serverMode
    End Function

    ''' <summary>
    ''' lista los centros de atención externos por permiso de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCampaignDetailXPInstantFeedbackSourceAndCount(campaignId As Integer, Optional StatusFilter As String = Nothing) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CampaignDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("CampaignId.Id = " & campaignId & "{0}", If(StatusFilter Is Nothing, "", "AND CampaignStatus IN (" & StatusFilter & " )")))
        Dim classEntity = session.GetClassInfo(GetType(CampaignDetailXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)

        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las solicitudes asociadas a la campaña
    ''' </summary>
    ''' <param name="campaignDetailId">Id de la campaña</param>
    Public Function ListCampaignDetailWithRequests(campaignDetailId As Integer) As PLinqServerModeSource
        Dim session As New Session(XpoDefault.DataLayer)

        Dim tableView As New XPQuery(Of ViewListCampaignDetailWithRequestsXpo)(session)

        Dim b As New PLinqServerModeSource
        b.Source = (From T1 In tableView Where T1.CampaignDetailId = campaignDetailId Select T1).ToList()
        Return b
    End Function

    ''' <summary>
    ''' lista los centros de atención externos por permiso de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExternalCareCenterByUserPermission(userCode As String, Optional MixingStationId As Integer? = Nothing) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ExternalCareCenterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Status = 1 and ExternalCareCenterUsersXpo[UserCode = '{userCode}'] {If(MixingStationId Is Nothing, String.Empty, $"AND CMExternalCareCenterXpo[CMConfigurationId.Id = {MixingStationId}]")}")
        Dim classEntity = session.GetClassInfo(GetType(ExternalCareCenterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Status;StatusName;CodeDescription", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRequestUnitDoseExternalCareCenter() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RequestUnitDoseExternalCareCenterXpo)()
        Dim classEntity = session.GetClassInfo(GetType(RequestUnitDoseExternalCareCenterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;StatusName;RequestTypeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRequestUnitDoseInventory() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RequestUnitDoseInventoryXpo)()
        Dim classEntity = session.GetClassInfo(GetType(RequestUnitDoseInventoryXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las lineas de producción
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewListProductionLine(cmConfigurationId As Integer, careCenterCode As String, UnitDoseTypeId As Integer, SourceType As Integer) As List(Of ViewListProductionLineXpo)
        Dim session As New IndigoXPOSession(Of ViewListProductionLineXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CMConfigurationId = " & cmConfigurationId & " and CareCenterCode = '" & careCenterCode & "' and UnitDoseTypeId = " & UnitDoseTypeId & " and SourceType = " & SourceType)
        Dim classEntity = session.GetClassInfo(GetType(ViewListProductionLineXpo))
        Dim serverMode As List(Of ViewListProductionLineXpo) = New XPCollection(Of ViewListProductionLineXpo)(session, criteria).ToList()
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExternalCareCenter() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ExternalCareCenterXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ExternalCareCenterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExternalCareCenterByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ExternalCareCenterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status)
        Dim classEntity = session.GetClassInfo(GetType(ExternalCareCenterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Status;StatusName;CodeDescription", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExternalCareCenterByCustomer(customerId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ExternalCareCenterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CustomerId = " & customerId & " AND Status = 1")
        Dim classEntity = session.GetClassInfo(GetType(ExternalCareCenterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Status;StatusName;CodeDescription;CustomerId", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los clientes asociados a los centros de atención externos
    ''' </summary>
    Public Function ListCustomersWithExternalCareCenters() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewCustomersWithExternalCareCentersXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ViewCustomersWithExternalCareCentersXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "ViewKey;IdCustomer;NitCustomer;NameCustomer;NitName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCauseReprocessingRejection() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CauseReprocessingRejectionXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CauseReprocessingRejectionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Class;ClassName;Status;StatusName;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los defectos categorizados
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCategoryDefects() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CategoryDefectsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CategoryDefectsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPatientExternalCareCenter() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PatientExternalCareCenterXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PatientExternalCareCenterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;IdentificationNumber;IdentificationType;Name;LastName;Sex;BirthDate;Status;StatusName;IdentificationName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los géneros
    ''' </summary>
    ''' <returns></returns>
    Public Function ListActiveGenderTypes() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GenderTypesXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = 1 ")
        Dim classEntity = session.GetClassInfo(GetType(GenderTypesXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPatientExternalCareCenterByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PatientExternalCareCenterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status)
        Dim classEntity = session.GetClassInfo(GetType(PatientExternalCareCenterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;IdentificationNumber;IdentificationType;Name;LastName;Sex;BirthDate;Status;StatusName;IdentificationName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListStabilityTable() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of StabilityTableXpo)()
        Dim classEntity = session.GetClassInfo(GetType(StabilityTableXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;StabilityDate;Status;StatusName;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTransportationAssistant() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TransportationAssistantXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TransportationAssistantXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;AssistantType;IdentificationNumber;FullName;Status;StatusName;AssistantTypeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTransportation() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TransportationXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TransportationXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;TransportationType;TransportationCode;Status;StatusName;TransportationTypeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListContractExternalClients() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractExternalClientsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ContractExternalClientsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ContractType;DocumentDate;InitialDate;EndDate;ContractNumber;Status;StatusName;ContractTypeName;Customer.NitName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListContractExternalClientsByStatus(status As Boolean, Optional CustomerId As Integer? = Nothing) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractExternalClientsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Status = {status} {If(CustomerId Is Nothing, "", $"AND CustomerId ={CustomerId}")}")
        Dim classEntity = session.GetClassInfo(GetType(ContractExternalClientsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ContractType;DocumentDate;InitialDate;EndDate;ContractNumber;Status;StatusName;ContractTypeName;ContractExternalClientsCodeContractNumber", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las centrales de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListProductionLine() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixingStationProductionLineXpo)()
        Dim classEntity = session.GetClassInfo(GetType(MixingStationProductionLineXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;Code;Id_FunctionalUnit;Id_FunctionalUnit.CodeName;CodeName;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los criterios de despeje de linea
    ''' </summary>
    ''' <returns></returns>
    Public Function ListLineClearanceCriteria() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of LineClearanceCriteriaXpo)()
        Dim classEntity = session.GetClassInfo(GetType(LineClearanceCriteriaXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;Code;CodeName;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los defectos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDefects() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DefectsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(DefectsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Description;Code;DefectClassificationGroupId;DefectClassificationGroupId.CodeName;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las centrales de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListProductionLineByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixingStationProductionLineXpo)()
        Dim classEntity = session.GetClassInfo(GetType(MixingStationProductionLineXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State = " & status)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;Code;Id_FunctionalUnit;Id_FunctionalUnit.CodeName;CodeName;StatusName;State", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetProductionLineById(ByVal id As Integer) As MixingStationProductionLineXpo
        Dim session As New IndigoXPOSession(Of MixingStationProductionLineXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & id)
        Dim _ProductionLineXpo As MixingStationProductionLineXpo = New XPCollection(Of MixingStationProductionLineXpo)(session, criteria).FirstOrDefault()
        Return _ProductionLineXpo
    End Function

    ''' <summary>
    ''' lista las centrales de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMixingCenter() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixinStationCMConfigXpo)()
        Dim classEntity = session.GetClassInfo(GetType(MixinStationCMConfigXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;ManageMS;MixingStationType;Name;Id_depmuncod;Code;State;TypeName;StateName;CreationDate", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las centrales de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCMConfigurationByStatus(status As Boolean, Optional ListMixingStationType As List(Of Integer) = Nothing) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixinStationCMConfigXpo)()
        Dim classEntity = session.GetClassInfo(GetType(MixinStationCMConfigXpo))
        Dim MixingStationTypeString As String = String.Empty
        If ListMixingStationType IsNot Nothing Then
            MixingStationTypeString = $"AND MixingStationType IN ({String.Join(",", ListMixingStationType)})"
        End If
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("State = {0} {1}", status, MixingStationTypeString))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;State;CodeName;TypeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los turnos de la central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTurn() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixinStationTurnXpo)()
        Dim classEntity = session.GetClassInfo(GetType(MixinStationTurnXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;State;StateName;Description;InitialTimeWorkHM;EndingTimeWorkHM;InitialTimeDeliveryHM;EndingTimeDeliveryHM", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los tipos de dosis unitaria de la central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListUnitDoseType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixinStationUnitDoseTypeXpo)()
        Dim classEntity = session.GetClassInfo(GetType(MixinStationUnitDoseTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;State;Description;MSClass;ClassName;CodeDescription;Prefix", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los tipos de dosis unitaria de la central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListUnitDoseTypeByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixinStationUnitDoseTypeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State = " & status)
        Dim classEntity = session.GetClassInfo(GetType(MixinStationUnitDoseTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;State;Description;MSClass;ClassName;CodeDescription", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los tipos de dosis unitaria de la central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListUnitDoseTypeByStatusAndClass() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixinStationUnitDoseTypeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State = 1 and not MSClass in (5, 6)")
        Dim classEntity = session.GetClassInfo(GetType(MixinStationUnitDoseTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;State;Description;MSClass;ClassName;CodeDescription", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los tipos de nutricion parenteral
    ''' </summary>
    ''' <returns></returns>
    Public Function ListNPT() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HCPARNUTCXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("STATUS = 1")
        Dim classEntity = session.GetClassInfo(GetType(HCPARNUTCXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "ID;NAME;CODE;STATUS;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista el detalle de los tipos de nutricion parenteral
    ''' </summary>
    ''' <returns></returns>
    Public Function ListComponentsNPT(Id As Integer) As List(Of ViewListComponentsNPTXpo)
        Dim session As New IndigoXPOSession(Of ViewListComponentsNPTXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdHCPARNUTC = " & Id)
        Dim _ListComponentsNPTXpoXpo As List(Of ViewListComponentsNPTXpo) = New XPCollection(Of ViewListComponentsNPTXpo)(session, criteria).ToList()
        Return _ListComponentsNPTXpoXpo
    End Function

    ''' <summary>
    ''' lista los tipos de dosis unitaria de la central de mezclas: Medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListUnitDoseTypeByMSClass() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixinStationUnitDoseTypeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State = 1 and MSClass in (5, 6, 7)")
        Dim classEntity = session.GetClassInfo(GetType(MixinStationUnitDoseTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;State;Description;MSClass;ClassName;CodeDescription", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los estatus de los medicamentos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListProductionBaskets(ByVal UnitDoseTypeId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ProductionBasketsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("UnitDoseTypeId = " & UnitDoseTypeId)
        Dim classEntity = session.GetClassInfo(GetType(ProductionBasketsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;StatusName;CodeName;UnitDoseTypeId", Nothing)
        If UnitDoseTypeId = 0 Then
            serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;StatusName;CodeName;UnitDoseTypeId", Nothing)
        Else
            serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;StatusName;CodeName;UnitDoseTypeId", criteria)
        End If
        Return serverMode
    End Function


    ''' <summary>
    ''' lista los paquetes (productos) de la central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPackage() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixinStationPackageXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PersonalizedMasterPreparation = 0")
        Dim classEntity = session.GetClassInfo(GetType(MixinStationPackageXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;State;StateName;Description;UnitDoseTypeId.ClassName;UnitDoseTypeId;UnitDoseTypeId.Description", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los paquetes (productos) de la central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPackageByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixinStationPackageXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State = " & status & " and PersonalizedMasterPreparation = 0")
        Dim classEntity = session.GetClassInfo(GetType(MixinStationPackageXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;State;StateName;Description;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los paquetes (productos) por tipo de Dosis unitaria de la central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPackageByDoseType(doseTypeId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixinStationPackageXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State = 1 and PersonalizedMasterPreparation = 0 and UnitDoseTypeId = " & doseTypeId)
        Dim classEntity = session.GetClassInfo(GetType(MixinStationPackageXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;State;StateName;Description;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los paquetes (productos) de la central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPackageByATCId(atcId As Integer, Optional NPT As Boolean = False) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixinStationPackageXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("State = 1 and PersonalizedMasterPreparation = 0 and {0}", If(NPT, "UnitDoseTypeId.MSClass = 2", $"MixinStationPackageDetailXpo[AtcId.Id ={atcId} and MainMedicine = 1]")))
        Dim classEntity = session.GetClassInfo(GetType(MixinStationPackageXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;State;StateName;Description;CodeName;PersonalizedMasterPreparation", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los tipos de estantes
    ''' </summary>
    ''' <returns></returns>
    Public Function ListShelfType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of InventoryShelfTypeXpo)()
        Dim classEntity = session.GetClassInfo(GetType(InventoryShelfTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;State;Description;Large;Wide;Deep;PartitionXDeep;Partitions;LocationXPartition", Nothing)
        Return serverMode
    End Function

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
    ''' lista las Causa de Reproceso
    ''' </summary>
    ''' <returns></returns>
    Public Function ListReprocessingCause(TypeAction As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CauseReprocessingRejectionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Class = " & TypeAction & " and Status = 1")
        Dim classEntity = session.GetClassInfo(GetType(CauseReprocessingRejectionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' funcion para consultar todas las Readecuaciones
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewReadjustmentsXpoByCMConfigurationId(CMConfigurationId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewReadjustmentsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"CMConfigurationId = {CMConfigurationId}")
        Dim classEntity = session.GetClassInfo(GetType(ViewReadjustmentsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los factores de dilucion
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDilutionFactorsXpo() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DilutionFactorsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(DilutionFactorsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los diluyentes segun el medicamento parametrizado
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDilutionDetail(DilutionId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDilutionFactorsDetailByIdFactorXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"ATCDilutionFactor = " & DilutionId)
        Dim classEntity = session.GetClassInfo(GetType(ViewDilutionFactorsDetailByIdFactorXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene medicamentos diluyentes por medicamento parametrizado de la tabla de estabilidad
    ''' </summary>
    ''' <param name="ATCId"></param>
    ''' <returns></returns>
    Public Function ListStabilityTableDetailDilutionByAtcId(ATCId As Integer) As StabilityTableDetailXpo
        Dim session As New IndigoXPOSession(Of StabilityTableDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"ATCId = " & ATCId)
        Dim classEntity = session.GetClassInfo(GetType(StabilityTableDetailXpo))
        Dim serverMode As StabilityTableDetailXpo = New XPCollection(Of StabilityTableDetailXpo)(session, criteria).FirstOrDefault()
        Return serverMode
    End Function




    ''' <summary>
    ''' Consulta vista para traer las readecuaciones que hacen match con la solicitud
    ''' </summary>
    ''' <param name="StandartPackageId">Id del paquete standart</param>
    ''' <param name="Concentration">Concentracion del paquete</param>
    ''' <param name="ConcentrationMeasurementUnitId">unidad de medida de la conecntracion</param>
    ''' <param name="VolumeTotalOrder">volumen total</param>
    ''' <param name="VolumeTotalOrderMeasurementUnitId">unidad de medida del volumen</param>
    ''' <param name="AtcMainMedicine">atc del medicamento principal</param>
    ''' <param name="AtcVehicle">atc del vehiculo</param>
    ''' <returns></returns>
    Public Function ViewListToAssignReadjustment(StandartPackageId As Integer, Concentration As Integer,
                                                    ConcentrationMeasurementUnitId As Integer, VolumeTotalOrder As Decimal,
                                                    VolumeTotalOrderMeasurementUnitId As Integer, AtcMainMedicine As Integer,
                                                    AtcVehicle As Integer, DateTimeNow As DateTime) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewReadjustmentsToBeAssignedXpo)()
        Dim filter As String = $"IsReadjustment=true and Status=1 and TechnicalConceptDate is not NULL and PackageId={StandartPackageId} and Concentration={Concentration} and ConcentrationMeasurementUnitId ={ConcentrationMeasurementUnitId} and VolumeTotalOrder= '{VolumeTotalOrder.ToString.Replace(",", ".")}' and VolumeTotalOrderMeasurementUnitId={VolumeTotalOrderMeasurementUnitId} and AtcMainMedicine={AtcMainMedicine} and AtcVehicle={AtcVehicle} and ExpirationDate>= #{Format(DateTimeNow, "yyyy-MM-dd HH:mm : ss")}#"
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
        Dim classEntity = session.GetClassInfo(GetType(ViewReadjustmentsToBeAssignedXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        serverMode.DefaultSorting = "ExpirationDate ASC"
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta vista para traer las readecuaciones que similares a la original para antibioticos
    ''' </summary>
    ''' <returns></returns>
    Public Function ViewReadjustmentsToBeAssigned(StandardPackageId As Integer, DateTimeNow As DateTime,
                                                                AtcMainMedicine? As Integer, QuantityMainMedicine? As Decimal, MeasurementUnitIdMainMedicine? As Integer,
                                                                AtcVehicle? As Integer, QuantityVehicle? As Decimal, MeasurementUnitIdVehicle? As Integer,
                                                                AtcThinner? As Integer, QuantityThinner? As Decimal, MeasurementUnitIdThinner? As Integer) As XPInstantFeedbackSource

        Dim session As New IndigoXPOSession(Of ViewReadjustmentsToBeAssignedXpo)()

        ' Formatear la fecha y hora para el filtro
        Dim formattedDateTime As String = DateTimeNow.ToString("yyyy-MM-dd HH:mm:ss")

        Dim filter As String = $"IsReadjustment = 1 AND Status = 1 AND TechnicalConceptDate IS NOT NULL AND StandardPackageId = {StandardPackageId} AND ExpirationDate >= #{formattedDateTime}# " &
                               $"AND AtcMainMedicine = {AtcMainMedicine} AND QuantityMainMedicine = {QuantityMainMedicine} AND MeasurementUnitIdMainMedicine = {MeasurementUnitIdMainMedicine} " &
                               $"AND AtcVehicle = {AtcVehicle}  AND QuantityVehicle = {QuantityVehicle} AND MeasurementUnitIdVehicle = {MeasurementUnitIdVehicle}"

        If AtcThinner IsNot Nothing Then
            filter += $"AND AtcVehicle = {AtcThinner}  AND QuantityVehicle = {QuantityThinner} AND MeasurementUnitIdVehicle = {MeasurementUnitIdThinner}"
        End If

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
        Dim classEntity = session.GetClassInfo(GetType(ViewReadjustmentsToBeAssignedXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        serverMode.DefaultSorting = "ExpirationDate ASC"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los tipos de dosis unitaria de la central de mezclas 
    ''' de tipo reempaque y reenvase
    ''' </summary>
    ''' <returns></returns>
    Public Function ListUnitDoseTypeRepackage() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MixinStationUnitDoseTypeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("MSClass in (5, 7)")
        Dim classEntity = session.GetClassInfo(GetType(MixinStationUnitDoseTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;State;Description;MSClass;ClassName;CodeDescription", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista la información básica de las campañas para el control vertical del formulario Campañas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCampaignMainInfoXPInstantFeedbackSource(campaignId As Integer, Optional StatusFilter As String = Nothing) As (XPInstantFeedbackSource, Integer)
        Dim session As New IndigoXPOSession(Of ViewCampaignMainInfoXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("CampaignId = " & campaignId & "{0}", If(StatusFilter Is Nothing, "", "AND CampaignStatus IN (" & StatusFilter & " )")))
        Dim classEntity = session.GetClassInfo(GetType(ViewCampaignMainInfoXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)

        Dim count As Integer = CInt(session.Evaluate(Of ViewCampaignMainInfoXpo)(New AggregateOperand("", Aggregate.Count), serverMode.FixedFilterCriteria))
        Return (serverMode, count)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
