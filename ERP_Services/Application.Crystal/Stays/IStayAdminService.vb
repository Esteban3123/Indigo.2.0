'***********************************************************************
' Assembly         : Application.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Dynamic
Imports Domain.Crystal.Entities

Public Interface IStayAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Lista las estancias por número de ingreso y estado
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="status">Estado de la estancia</param>
    ''' <param name="asNoTracking">Valor que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    Function ListStaysByAdmissionCodeAndStatus(ByVal admissionCode As String, ByVal status As StayStatusEnum, ByVal asNoTracking As Boolean) As ActionResult(Of List(Of CHREGESTA))

    ''' <summary>
    ''' Lista las estancias por número de ingreso y estado
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="asNoTracking">Valor opcional que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
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
    Function ListDontLiquidatedStaysByAdmissionCode(ByVal admissionCode As String, ByVal caregroupId As Integer, stayOption As Domain.Entities.eLiquidateStayOption, ByVal medicalOrderDate As DateTime?, Optional ByVal endDate As DateTime? = Nothing, Optional ByVal asNoTracking As Boolean = True) As ActionResult(Of List(Of CHREGESTA))

    ''' <summary>
    ''' Realiza la liquidación de estancias según los parámetros, la persiste y crea la orden de servicio
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    ''' <param name="medicalOrderDate">Fecha de la orden medica para hospitalización</param>
    ''' <param name="endDate">Fecha final de corte</param>
    ''' <param name="patientCode">Código del paciente</param>
    ''' <param name="idSequence">Id de la secuencia numerica</param>
    ''' <param name="operatingUnitId">Id de la unidad operativa</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Resultado de la acción</returns>
    Function LiquidateStays(ByVal admissionCode As String, healthAdministratorId As Integer, thirdPartyId As Integer, ByVal caregroupId As Integer, stayOption As Domain.Entities.eLiquidateStayOption, ByVal medicalOrderDate As DateTime?, ByVal endDate As DateTime?, ByVal patientCode As String, ByVal idSequence As Integer, ByVal operatingUnitId As Integer, ByVal audit As AuditMessage) As ActionResult

    Function ListOfStaysByAdmission(admissionCode As String) As ActionResult(Of List(Of CHREGESTA))
    Function ListOfStaysByAdmissionToModel(admissionCode As String, audit As AuditMessage) As ActionResult(Of List(Of StayInfoModel))

    ''' <summary>
    ''' Calcula las unidades de estancia (días/horas) para un único rango de fechas
    ''' usando la misma lógica de corte que el módulo de estancias.
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso (NUMINGRES)</param>
    ''' <param name="startDate">Fecha/hora inicio de estancia</param>
    ''' <param name="endDate">Fecha/hora final de estancia</param>
    ''' <returns>UnitStay con Days y Hours calculados</returns>
    Function CalculateUnitStayForRange(admissionCode As String,
                                       startDate As Date,
                                       endDate As Date) As UnitStay
#End Region

End Interface
