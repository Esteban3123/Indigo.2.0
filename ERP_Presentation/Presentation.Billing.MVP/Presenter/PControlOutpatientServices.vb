'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 28-10-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.CommonRepository
#End Region

Public Class PControlOutpatientServices
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IControlOutpatientServices

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IControlOutpatientServices)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Obtiene la admision por numero
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAdmissionByAdmissionNumber(AdmissionNumber As String) As ViewGetAdmissionXpo
        Dim filter As String = "AdmissionCode = " & AdmissionNumber
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of ViewGetAdmissionXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los parámetros de facturación
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetLiquidateSinceControlOutPatientService(OperatingUnitId As Integer) As SettingsBillingXpo
        Dim filter As String = "IdOperatingUnit = " & OperatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of SettingsBillingXpo)(Nothing, filter).FirstOrDefault()
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    Public Async Sub ListHemocomponent()
        If View.HemocomponentXPO Is Nothing Then
            View.HemocomponentXPO = XpoServiceEx.Instance(SessionValues.Instance.HisContainer).CrystalService.ListHemocomponent()
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="hemocomponentId"></param>
    ''' <returns></returns>
    Public Function GetHemocomponentById(hemocomponentId As Integer) As HCCOMSANXpo
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetHemocomponentById(hemocomponentId)
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="hemocomponentId"></param>
    ''' <returns></returns>
    Public Function ListReserveHemocomponentDetail(hemocomponentId As Integer) As List(Of HCCOMSANDXpo)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListReserveHemocomponentDetail(hemocomponentId).ToList
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCUPSCrystalByCode(code As String) As CUPSXpo
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCUPSCrystalByCode(code)
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="careCenterCode"></param>
    ''' <returns></returns>
    Public Function GetADPARAMETByCareCenterCode(careCenterCode As String) As ADPARAMETXpo
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetADPARAMETByCareCenterCode(careCenterCode)
    End Function

    ''' <summary>
    ''' Se obtiene el grupo de atención del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCareGroupPatientById(Id As Integer) As CareGroupXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of CareGroupXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista las descripciones asociadas al cups
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractDescriptionsByCupsEntityCode(CupsEntityCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListContractDescriptionsByCupsEntityCode(CupsEntityCode)
    End Function

    ''' <summary>
    ''' Valida si el cups tiene asociado descripciones
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateDescriptionAssociateToCups(CupsEntityCode As String) As Boolean
        Dim hideControl As Boolean = True

        Dim filter As String = $"Code = '{CupsEntityCode}' AND CUPSEntityContractDescriptionsXpo[IsDelete =0]"
        Dim info = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo)(Nothing, filter).FirstOrDefault()

        If info IsNot Nothing AndAlso info.CUPSEntityContractDescriptionsXpo IsNot Nothing AndAlso info.CUPSEntityContractDescriptionsXpo.Count > 0 Then
            hideControl = False
        End If

        Return hideControl
    End Function

    ''' <summary>
    ''' Obtiene la relación entre el cups y la descripción
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCUPSEntityContractDescriptionById(Id As Integer) As Infrastructure.Data.Xpo.ContractRepository.CUPSEntityContractDescriptionsXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of Infrastructure.Data.Xpo.ContractRepository.CUPSEntityContractDescriptionsXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Valida si un cups es suceptible
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateCUPSSusceptible(listCodes As List(Of String), careGroupId As Integer) As List(Of String)
        Dim codes As String = String.Join(",", listCodes.ToArray())

        Dim filter As String = "CUPSEntityCode in (" & codes & ") and (CareGroupId <> " & careGroupId & " or (CareGroupId = " & careGroupId & " and SusceptibleAuthorization = 1))"
        Dim listXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ViewListCUPSSusceptiblesXpo)(Nothing, filter).ToList()

        Dim listReturn As List(Of String) = Nothing
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            listReturn = New List(Of String)
            listXpo.ForEach(Sub(item) listReturn.Add("'" + item.CUPSEntityCode + "'"))
            listReturn = (From x In listReturn Select x).Distinct().ToList()
        End If

        Return listReturn
    End Function

    ''' <summary>
    ''' Lista las autorizaciones de los servicios
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListAuthorizations(listCodes As List(Of String), patientCode As String, functionalUnitCodes As String, careCenterCode As String) As List(Of ViewTraceabilityPaperworkAuthorizedXpo)
        Dim stringsCodes = String.Join(",", listCodes.ToArray())
        Dim filter As String = "ServiceCode in (" + stringsCodes + ") and PatientCode = '" & patientCode & "' and FunctionalUnitCode in (" & functionalUnitCodes & ") and CareCenterCode = '" & careCenterCode & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewTraceabilityPaperworkAuthorizedXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el parámetro de contrato por unidad operativa
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    Public Function GetSettingsContractByOperatingUnitId(operatingUnitId As Integer) As SettingsContractXpo
        Dim filter As String = "OperatingUnitId = " & operatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of SettingsContractXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    '''  Datasource de las Salas
    ''' </summary>
    ''' <param name="ServiceType"></param>
    ''' <param name="AttetionCenter"></param>
    ''' <returns></returns>
    Public Function GetRoomWithFilters(ServiceType As Byte, AttetionCenter As String) As List(Of AGENSALACXpo)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetRoomWithFilters(ServiceType, AttetionCenter)
    End Function

    ''' <summary>
    ''' DataSource de actividades de agendamiento
    ''' </summary>
    ''' <param name="RoomCode"></param>
    ''' <returns></returns>
    Public Function GetScheduleActivities(RoomCode As Integer) As List(Of AGENSALAACTXpo)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetScheduleActivities(RoomCode)
    End Function

    ''' <summary>
    ''' Tipo de actividad de agendamiento
    ''' </summary>
    ''' <param name="ScheduleActivityCode"></param>
    ''' <returns></returns>
    Public Function GetScheduleActivityType(ScheduleActivityCode As String) As Integer
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetScheduleActivityType(ScheduleActivityCode)
    End Function

    ''' <summary>
    ''' cups filtrado por Codigo de la Actividad Medica
    ''' </summary>
    ''' <param name="CODACTMED"></param>
    ''' <returns></returns>
    Public Async Function ListCUPSByCODACTMED(CODACTMED As String) As Task(Of XPInstantFeedbackSource)
        Return Await Task.Factory.StartNew(Function()
                                               Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CrystalService.ListCUPSByCODACTMED(CODACTMED)
                                           End Function)
    End Function

    ''' <summary>
    ''' funcion que obtiene el cups por codigo de la vista ViewListCupsByAGACTIMEDXpo 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCUPSByCode(code As String) As ViewListCupsByAGACTIMEDXpo
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetXPOObject(Of ViewListCupsByAGACTIMEDXpo)($"CODSERIPS = '{code}'")
    End Function

    ''' <summary>
    ''' Verifica si la sala tienen asignado un profesional
    ''' </summary>
    ''' <param name="IDSALA"></param>
    ''' <returns></returns>
    Public Function GetCodProfesional(IDSALA As Integer, RoomDate As DateTime) As AGDISPONSALAXpo
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCodProfesional(IDSALA, RoomDate)
    End Function

    ''' <summary>
    ''' consulta las citas de hemocomponente que tiene el paciente
    ''' </summary>
    ''' <param name="IPCODPACI"></param>
    ''' <returns></returns>
    Public Function GetMedicalAppointmentHemo(IPCODPACI As String) As List(Of AGASICITAXpo)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetMedicalAppointmentHemo(IPCODPACI).ToList
    End Function

    ''' <summary>
    ''' consulta las citas de hemocomponente que tiene el paciente
    ''' </summary>
    ''' <param name="IPCODPACI"></param>
    ''' <returns></returns>
    Public Function GetOrdersExtramuralHemo(IPCODPACI As String) As List(Of HCORHEMCOXpo)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetOrdersExtramuralHemo(IPCODPACI).ToList
    End Function

    ''' <summary>
    ''' Carga los permisos asignados al usuario en éste formulario
    ''' </summary>
    Public Sub LoadPermissionsForm(tag As String)
        Using model As New MLiquidation()
            Dim permissions = model.GetPermissions(tag)
            Me.View.PermissionsForm = (From a In permissions Select Action = a.TagButton, Name = [Enum].GetName(GetType(PermissionsActionsForm), a.TagButton)).ToDictionary(Function(x) x.Action, Function(y) y.Name)
        End Using
    End Sub

    ''' <summary>
    ''' consulta la justificacion por usuario autorizado
    ''' </summary>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function GetJustificationByUserCode(UserCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetJustificationByUserCode(UserCode)
    End Function

    ''' <summary>
    ''' Obtiene el Tercero por el Id del Cliente
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetUbications() As List(Of CommonRepository.ViewGeographicStructureXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of CommonRepository.ViewGeographicStructureXpo).ToList()
    End Function

    ''' <summary>
    ''' carga el valor de la bandera para saber si el sistema es impuesto incluido o no
    ''' </summary>
    Public Async Function LoadFlagTaxInclude() As Task
        Using Model As New MServiceOrder("")
            Dim CompanySettings = Await Model.CompanySettings
            Me.View.FlagTaxInclude = CompanySettings.SalePriceIncludeTax
        End Using
    End Function

    ''' <summary>
    ''' metodo que carga la lista de actividades de agendamiento para el tipo de actividad "Otros"
    ''' </summary>
    Public Sub LoadScheduleActivityOther(especialityCode As String)
        Using Model As New MControlOutpatientServices("")
            Me.View.ScheduleActivityOtherXPO = Model.GetScheduleActivityOther(especialityCode)
        End Using
    End Sub

    ''' <summary>
    ''' metodo que carga la lista de actividades de agendamiento para el tipo de actividad "Otros"
    ''' </summary>
    Public Sub LoadConsultingRoom(centerAttentionCode As String)
        Using Model As New MControlOutpatientServices("")
            Me.View.ConsultingRoomXPO = Model.GetConsultinRoom(centerAttentionCode)
        End Using
    End Sub

    ''' <summary>
    ''' metodo para inicializar el como de tipo de ingreso
    ''' </summary>
    Public Sub InitializateAdmissionType()
        Using Model As New MControlOutpatientServices("")
            Me.View.AdmissionTypeDatasource = Model.GetAdmissiontypes()
        End Using
    End Sub

    ''' <summary>
    ''' metodo para inicializar el como de Vías Ingreso Servicios de Salud
    ''' </summary>
    Public Sub InitializateEntryRoutesHealthServices()
        Using Model As New MControlOutpatientServices("")
            Me.View.EntryRoutesHealthServicesDatasource = Model.GetEntryRoutesHealthServices()
        End Using
    End Sub

    ''' <summary>
    ''' metodo para inicializar el como de Finalidades tecnologías de la salud
    ''' </summary>
    Public Sub InitializateHealthPurposes()
        Using Model As New MControlOutpatientServices("")
            Me.View.HealthPurposesDatasource = Model.GetHealthPurposes()
        End Using
    End Sub

    ''' <summary>
    ''' metodo para inicializar el como de Modalidades de Atención
    ''' </summary>
    Public Sub InitializateAdmissionModalities()
        Using Model As New MControlOutpatientServices("")
            Me.View.AdmissionModalitiesDatasource = Model.GetAdmissionModalities()
        End Using
    End Sub

    ''' <summary>
    ''' funcion que retorna los cups con sus respectivas relaciones
    ''' </summary>
    ''' <param name="listCupsEntityCode"></param>
    ''' <returns></returns>
    Public Async Function GetEntityCUPSByListCodes(listCupsEntityCode As List(Of String)) As Task(Of List(Of Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo))

        If listCupsEntityCode Is Nothing OrElse Not listCupsEntityCode.Any() Then
            Return New List(Of ContractRepository.CupsEntityXpo)
        End If

        Dim codeFilter = String.Join(",", listCupsEntityCode.Select(Function(d) $"'{d}'"))
        Dim filter As String = $"Code in ({codeFilter})"

        Dim info = Await Task.Factory.StartNew(Function()
                                                   Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo)(Nothing, filter)
                                               End Function)

        Return info.ToList()
    End Function

End Class
