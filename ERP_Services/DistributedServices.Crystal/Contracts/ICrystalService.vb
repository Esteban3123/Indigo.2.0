'***********************************************************************
' Assembly         : DistributedService.Crystal
' Author           : Juan F. Tamayo
' Created          : 2015-01-24
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities

#End Region

<ServiceContract()>
Public Interface ICrystalService
    Inherits ICrystalServiceHealthCareProfessional, ICrystalServiceAdmissions, ICrystalServiceDashboardPharmacyDetail, ICrystalServiceDashboardPharmacyDetailDevolution, ICrystalServiceBedRate, ICrystalServiceExternalConsultation,
        ICrystalServiceHCUNITHIS

    ''' <summary>
    ''' Lista las estancias por número de ingreso y estado
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="status">Estado de la estancia</param>
    ''' <param name="asNoTracking">Valor que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    <OperationContract()>
    Function ListStaysByAdmissionCodeAndStatus(ByVal admissionCode As String, ByVal status As StayStatusEnum, ByVal asNoTracking As Boolean) As ActionResult(Of List(Of CHREGESTA))

    ''' <summary>
    ''' Lista las estancias por número de ingreso que se encuentran liquidadas
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="asNoTracking">Valor opcional que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    <OperationContract()>
    Function ListLiquidatedStaysByAdmissionCode(ByVal admissionCode As String, ByVal asNoTracking As Boolean) As ActionResult(Of List(Of CHREGESTA))

    ''' <summary>
    ''' Lista las estancias por número de ingreso que no se encuentran liquidadas
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    ''' <param name="medicalOrderDate">Fecha de la orden medica para hospitalización</param>
    ''' <param name="endDate">Fecha final de corte</param>
    ''' <param name="asNoTracking">Valor opcional que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    <OperationContract()>
    Function ListDontLiquidatedStaysByAdmissionCode(ByVal admissionCode As String, ByVal caregroupId As Integer, stayOption As Domain.Entities.eLiquidateStayOption, ByVal medicalOrderDate As DateTime?, ByVal endDate As DateTime?, ByVal asNoTracking As Boolean) As ActionResult(Of List(Of CHREGESTA))

    ''' <summary>
    ''' Obtiene un paciente por identificación
    ''' </summary>
    ''' <param name="Identification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetPatientByIdentification(Identification As String) As ActionResult(Of INPACIENT)

    ''' <summary>
    ''' Elimina Pacientes
    ''' </summary>
    ''' <param name="Identification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeletePatient(Identification As String, audit As AuditMessage) As ActionResult
    '<OperationContract()>
    Function ExecuteQueryDt(query As String, session As SessionValues) As DataTable
    ''' <summary>
    ''' Guarda Pacientes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SavePatient(patient As INPACIENT, audit As AuditMessage) As ActionResult(Of INPACIENT)

    ''' <summary>
    ''' Realiza la liquidación de estancias según los parámetros, la persiste y crea la orden de servicio
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    ''' <param name="medicalOrderDate">Fecha de la orden medica para hospitalización</param>
    ''' <param name="endDate">Fecha final de corte</param>
    ''' <param name="patientCode">Códio del paciente</param>
    ''' <param name="idSequence">Id de la secuencia</param>
    ''' <param name="operatingUnitId">Id de la unidad operativa</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function LiquidateStays(ByVal admissionCode As String, healthAdministratorId As Integer, thirdPartyId As Integer, ByVal caregroupId As Integer, stayOption As Domain.Entities.eLiquidateStayOption, ByVal medicalOrderDate As DateTime?, ByVal endDate As DateTime?, ByVal patientCode As String, ByVal idSequence As Integer, ByVal operatingUnitId As Integer, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene Nivel Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetLevelByCode(Code As String) As ADNIVELES

    ''' <summary>
    ''' Guarda el peso del paciente
    ''' </summary>
    ''' <param name="weight"></param>
    ''' <param name="codePatient"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveWeightPatient(weight As Integer, codePatient As String) As Boolean

    <OperationContract()>
    Function ListOfStaysByAdmission(admissionCode As String) As ActionResult(Of List(Of CHREGESTA))

    <OperationContract()>
    Function ListOfStaysByAdmissionToModel(admissionCode As String, audit As AuditMessage) As ActionResult(Of List(Of StayInfoModel))

    ''' <summary>
    ''' Calcula las unidades de estancia (días/horas) para un único rango de fechas
    ''' usando la misma lógica de corte que el módulo de estancias.
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso (NUMINGRES)</param>
    ''' <param name="startDate">Fecha/hora inicio de estancia</param>
    ''' <param name="endDate">Fecha/hora final de estancia</param>
    ''' <returns>UnitStay con Days y Hours calculados</returns>
    <OperationContract>
    Function CalculateUnitStayForRange(admissionCode As String,
                                       startDate As Date,
                                       endDate As Date) As UnitStay
End Interface
