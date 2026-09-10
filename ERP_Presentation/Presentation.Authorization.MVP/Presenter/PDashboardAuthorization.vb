'***********************************************************************
' Assembly         : Presentacion.Authorization.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/05/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository

#End Region

Public Class PDashboardAuthorization

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    Public Sub New()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga las unidades funcionales
    ''' </summary>
    Public Function InitializeCareCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListCenters()
    End Function

    ''' <summary>
    ''' Carga las unidades funcionales
    ''' </summary>
    Public Function InitializeHealthAdministrator() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListHealthAdministratorByStatus(True)
    End Function

    ''' <summary>
    ''' Carga los motivos de cancelación por usuario
    ''' </summary>
    Public Function InitializeCancellationReason() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.ListCancellationReasonsByUserCode(Indigo.UserIndigo)
    End Function

    ''' <summary>
    ''' Carga los motivos de postergación por usuario
    ''' </summary>
    Public Function InitializePostponementReasons() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.ListPostponementReasonsByUserCode(Indigo.UserIndigo)
    End Function

    ''' <summary>
    ''' Carga los usuarios que estan registrados en las plantillas de turnos
    ''' </summary>
    Public Function InitializeListScheduleTemplateUsers() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.ListViewListScheduleTemplateUsers()
    End Function

    ''' <summary>
    ''' Lista las solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListViewListRequests(filter As String, topRows As Integer) As List(Of ViewListRequestsXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListRequestsXpo)(Nothing, filter, topRows:=topRows, withSort:=True, propertyToOrderBy:="RequestDate").ToList()
    End Function

    ''' <summary>
    ''' Lista los radicados
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListViewListRadicated(careCenterCodes As String, topRows As Integer, Optional aditionalFilter As String = Nothing) As List(Of ViewListRequestsXpo)
        Dim filter As String = "CareCenterCode in (" & careCenterCodes & ") and TraceabilityPaperworkStatus in (2, 3, 13)"
        filter = $"{filter} {If(String.IsNullOrEmpty(aditionalFilter), "", $"AND {aditionalFilter}")}"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListRequestsXpo)(Nothing, filter, topRows).ToList()
    End Function

    ''' <summary>
    ''' Lista los autorizados
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListViewListAuthorized(careCenterCodes As String, topRows As Integer, Optional aditionalFilter As String = Nothing) As List(Of ViewListRequestsXpo)
        Dim filter As String = "CareCenterCode in (" & careCenterCodes & ") and TraceabilityPaperworkStatus in (5, 15)"
        filter = $"{filter} {If(String.IsNullOrEmpty(aditionalFilter), "", $"AND {aditionalFilter}")}"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListRequestsXpo)(Nothing, filter, topRows).ToList()
    End Function

    ''' <summary>
    ''' Lista los postergados
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListViewListPostponement(careCenterCodes As String) As List(Of ViewListRequestsXpo)
        Dim filter As String = "CareCenterCode in (" & careCenterCodes & ") and TraceabilityPaperworkStatus in (16)"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListRequestsXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista los folios del ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListHCHISPACAXpo(patientCodes As String, admissionNumbers As String) As List(Of HCHISPACAXpo)
        Dim filter As String = "IPCODPACI in (" & patientCodes & ") and NUMINGRES in (" & admissionNumbers & ")"
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of HCHISPACAXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista los diagnosticos
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListDiagnosXpo() As List(Of INDIAGNOS)
        Dim filter As String = "ESTADO = 1"
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of INDIAGNOS)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista las solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListViewListRequestsByAdmissionNumberAndPatientCodeAndStatus(admissionNumber As String, patientCode As String, stringStatus As String) As List(Of ViewListRequestsXpo)
        Dim filter As String = String.Empty

        'Si viene vacío el no. admision es porque se agrego un servicio manualmente
        If String.IsNullOrEmpty(admissionNumber) Then
            filter = "PatientCode = '" & patientCode & "' and TraceabilityPaperworkStatus in (" + stringStatus + ")"
        Else
            filter = "AdmissionNumber = '" & admissionNumber & "' and PatientCode = '" & patientCode & "' and TraceabilityPaperworkStatus in (" + stringStatus + ")"
        End If

        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListRequestsXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' tabla para consultar la vista especial para trazabilidad
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="stringStatus"></param>
    ''' <returns></returns>
    Public Function ListViewListRequestsTraceabilityXpoByAdmissionNumberAndPatientCodeAndStatus(admissionNumber As String, patientCode As String, stringStatus As String) As List(Of ViewListRequestsTraceabilityXpo)
        Dim filter As String = String.Empty
        'Si viene vacío el no. admision es porque se agrego un servicio manualmente
        If String.IsNullOrEmpty(admissionNumber) Then
            filter = "PatientCode = '" & patientCode & "' and TraceabilityPaperworkStatus in (" + stringStatus + ")"
        Else
            filter = "AdmissionNumber = '" & admissionNumber & "' and PatientCode = '" & patientCode & "' and TraceabilityPaperworkStatus in (" + stringStatus + ")"
        End If

        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListRequestsTraceabilityXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista las toda la info de trazabilidad
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListViewListRequestsByAdmissionNumberAndPatientCode(admissionNumber As String, patientCode As String, Optional TypeSearch As Byte? = Nothing) As List(Of ViewListRequestsTraceabilityXpo)
        Dim filter As String = String.Format("PatientCode ='{0}' {1}", patientCode, IIf(TypeSearch Is Nothing OrElse TypeSearch = 1, $"AND AdmissionNumber='{admissionNumber}'", ""))
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListRequestsTraceabilityXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista los eventos
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListViewListEventsByTraceabilityId(listIds As List(Of Integer)) As List(Of ViewListEventsXpo)
        Dim stringsIds = String.Join(",", listIds.ToArray())
        Dim filter As String = "TraceabilityPaperworkId in (" + stringsIds + ")"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListEventsXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista los eventos
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListViewListAnnexesByTraceabilityId(listIds As List(Of Integer)) As List(Of ViewListAnnexesXpo)
        Dim stringsIds = String.Join(",", listIds.ToArray())
        Dim filter As String = "TraceabilityPaperworkId in (" + stringsIds + ")"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListAnnexesXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el ingreso por código
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Public Function GetAdmissionByAdmissionNumber(admissionNumber As String) As ViewAdmissionOpenPartialInvoicedClosedXpo
        Dim filter As String = "AdmissionCode = " & admissionNumber
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of ViewAdmissionOpenPartialInvoicedClosedXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Carga el datasource del control de ingresos
    ''' </summary>
    Public Function InitializeAdmission() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetViewAdmissionOpenPartialInvoicedClosed()
    End Function

    ''' <summary>
    ''' Carga el datasource de cups
    ''' </summary>
    Public Function InitializeCUPS(careCenterCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.ListViewListCUPSSusceptibles(careCenterCode)
    End Function

    ''' <summary>
    ''' Obtener el CUPS por Id
    ''' </summary>
    Public Function GetCUPSSusceptible(id As String) As ViewListCUPSSusceptiblesXpo
        Dim filter As String = "Id = '" & id & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListCUPSSusceptiblesXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Carga el datasource de productos
    ''' </summary>
    Public Function InitializeProducts(careCenterCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.ListViewListProductsSusceptibles(careCenterCode)
    End Function

    ''' <summary>
    ''' Obtener el producto por Id
    ''' </summary>
    Public Function GetProductSusceptible(InventoryProductId As Integer, CareCenterCode As String) As ViewListProductsSusceptiblesXpo
        Dim filter As String = "InventoryProductId = " & InventoryProductId & " and CareCenterCode = '" & CareCenterCode & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListProductsSusceptiblesXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Carga el datasource de origen
    ''' </summary>
    Public Function InitializeSource() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.ListAuthorizationSourceByStatus(True)
    End Function

    ''' <summary>
    ''' Obtiene el grupo de atención
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCareGroupById(CareGroupId As Integer) As ContractCareGroupReportXpo
        Dim filter As String = "Id = " & CareGroupId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ContractCareGroupReportXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetHealthAdministratorById(HealthAdministratorId As Integer) As HealthAdministratorReportXpo
        Dim filter As String = "Id = " & HealthAdministratorId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of HealthAdministratorReportXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el paciente
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetPatientByNit(nit As String) As PatientXpo
        Dim filter As String = "IPCODPACI = '" & nit & "'"
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of PatientXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el contrato
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetContractById(ContractId As Integer) As ContractReportXpo
        Dim filter As String = "Id = " & ContractId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ContractReportXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista los grupos de atencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroup() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCareGroupByStatus(True)
    End Function

    ''' <summary>
    ''' Lista los contratos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListContractXpo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListContractByStatus(1)
    End Function

    ''' <summary>
    ''' Lista las entidades prestadoras de salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthAdministratorByStatus() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListHealthAdministratorByStatus(True)
    End Function

    ''' <summary>
    ''' Inicializa datasource profesionales
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeProfessional() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListHealthCareProfessional()
    End Function

    ''' <summary>
    ''' Obtiene el listado de documentos asociados al evento
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListDocumentsByEventId(traceabilityPaperworkEventsId As Integer) As List(Of AttachmentXpo)
        Dim filter As String = "EntityDetailId = " & traceabilityPaperworkEventsId & " and EntityDetailName = 'TraceabilityPaperworkEvents'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of AttachmentXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Carga el datasource las alaertas relacionadas a una solicitud por estado
    ''' </summary>
    Public Function InitializeAlerts(traceabilityPaperworkId As Integer, status As Boolean) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.ListTraceabilityPaperworkAlertByStatus(traceabilityPaperworkId, status)
    End Function

    Public Function InitializePatiens() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListAllPatients()
    End Function

#End Region

End Class
