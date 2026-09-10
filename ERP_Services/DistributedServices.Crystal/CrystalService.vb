'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Juan F. Tamayo
' Created          : 2015-01-24
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Configuration
Imports System.Data.SqlClient
Imports Application.Crystal
Imports DistributedServices.Authentication
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity


#End Region

<JwtMessageServiceBehavior>
<ServiceBehavior(InstanceContextMode:=InstanceContextMode.PerCall)>
Public Class CrystalService
    Implements ICrystalService

#Region "Methods"
    ''' <summary>
    ''' Executes the query dt.
    ''' </summary>
    ''' <param name="query">The query.</param>
    ''' <param name="session">The session.</param>
    ''' <returns></returns>
    Public Function ExecuteQueryDt(query As String, session As SessionValues) As DataTable Implements ICrystalService.ExecuteQueryDt
        Dim conx = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "", session.HisContainer)
        Dim sqlWebConexion = New SqlConnection(conx)
        Dim da As SqlDataAdapter = New SqlDataAdapter(query, sqlWebConexion)
        da.SelectCommand.CommandTimeout = 90
        Dim ds As New DataSet
        da.Fill(ds, "Datos")
        Dim dtResult As DataTable = ds.Tables("Datos")
        da = Nothing
        ds = Nothing
        sqlWebConexion.Close()
        Return dtResult
    End Function

    ''' <summary>
    ''' Lista las estancias por número de ingreso y estado
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="asNoTracking">Valor opcional que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    Public Function ListLiquidatedStaysByAdmissionCode(admissionCode As String, asNoTracking As Boolean) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) Implements ICrystalService.ListLiquidatedStaysByAdmissionCode
        Using service As IStayAdminService = Container.Current.Resolve(Of IStayAdminService)()
            Return service.ListLiquidatedStaysByAdmissionCode(admissionCode, asNoTracking)
        End Using
        'Return Me._stayAdminService.ListLiquidatedStaysByAdmissionCode(admissionCode, asNoTracking)
    End Function

    ''' <summary>
    ''' Lista las estancias por número de ingreso y estado
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="status">Estado de la estancia</param>
    ''' <param name="asNoTracking">Valor que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    Public Function ListStaysByAdmissionCodeAndStatus(admissionCode As String, status As Domain.Crystal.Entities.StayStatusEnum, asNoTracking As Boolean) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) Implements ICrystalService.ListStaysByAdmissionCodeAndStatus
        Using service As IStayAdminService = Container.Current.Resolve(Of IStayAdminService)()
            Return service.ListStaysByAdmissionCodeAndStatus(admissionCode, status, asNoTracking)
        End Using
        'Return Me._stayAdminService.ListStaysByAdmissionCodeAndStatus(admissionCode, status, asNoTracking)
    End Function

    ''' <summary>
    ''' Lista las estancias por número de ingreso que no se encuentran liquidadas
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    ''' <param name="medicalOrderDate">Fecha de la orden medica para hospitalización</param>
    ''' <param name="endDate">Fecha de corte</param>
    ''' <param name="asNoTracking">Valor opcional que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    Public Function ListDontLiquidatedStaysByAdmissionCode(admissionCode As String, caregroupId As Integer, stayOption As Domain.Entities.eLiquidateStayOption, medicalOrderDate As DateTime?, endDate As Date?, asNoTracking As Boolean) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) Implements ICrystalService.ListDontLiquidatedStaysByAdmissionCode
        Using service As IStayAdminService = Container.Current.Resolve(Of IStayAdminService)()
            Return service.ListDontLiquidatedStaysByAdmissionCode(admissionCode, caregroupId, stayOption, medicalOrderDate, endDate, asNoTracking)
        End Using
        'Return Me._stayAdminService.ListDontLiquidatedStaysByAdmissionCode(admissionCode, caregroupId, stayOption, medicalOrderDate, endDate, asNoTracking)
    End Function

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
    Public Function LiquidateStays(admissionCode As String, healthAdministratorId As Integer, thirdPartyId As Integer, caregroupId As Integer, stayOption As Domain.Entities.eLiquidateStayOption, medicalOrderDate As Date?, endDate As Date?, ByVal patientCode As String, idSequence As Integer, ByVal operatingUnitId As Integer, ByVal audit As AuditMessage) As ActionResult Implements ICrystalService.LiquidateStays
        Using service As IStayAdminService = Container.Current.Resolve(Of IStayAdminService)()
            Return service.LiquidateStays(admissionCode, healthAdministratorId, thirdPartyId, caregroupId, stayOption, medicalOrderDate, endDate, patientCode, idSequence, operatingUnitId, audit)
        End Using
        'Return Me._stayAdminService.LiquidateStays(admissionCode, healthAdministratorId, thirdPartyId, caregroupId, stayOption, medicalOrderDate, endDate, patientCode, idSequence, operatingUnitId, audit)
    End Function

    Public Function ListOfStaysByAdmission(admissionCode As String) As ActionResult(Of List(Of CHREGESTA)) Implements ICrystalService.ListOfStaysByAdmission
        Using service As IStayAdminService = Container.Current.Resolve(Of IStayAdminService)()
            Return service.ListOfStaysByAdmission(admissionCode)
        End Using
    End Function

    Public Function ListOfStaysByAdmissionToModel(admissionCode As String, audit As AuditMessage) As ActionResult(Of List(Of StayInfoModel)) Implements ICrystalService.ListOfStaysByAdmissionToModel
        Using service As IStayAdminService = Container.Current.Resolve(Of IStayAdminService)()
            Return service.ListOfStaysByAdmissionToModel(admissionCode, audit)
        End Using
    End Function

    ''' <summary>
    ''' Calcula las unidades de estancia (días/horas) para un único rango de fechas
    ''' usando la misma lógica de corte que el módulo de estancias.
    ''' </summary>
    Public Function CalculateUnitStayForRange(admissionCode As String, startDate As Date, endDate As Date) As UnitStay Implements ICrystalService.CalculateUnitStayForRange
        Using service As IStayAdminService = Container.Current.Resolve(Of IStayAdminService)()
            Return service.CalculateUnitStayForRange(admissionCode, startDate, endDate)
        End Using
    End Function

#Region "Patient"

    ''' <summary>
    ''' Obtiene un paciente por identificación
    ''' </summary>
    ''' <param name="Identification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPatientByIdentification(Identification As String) As ActionResult(Of INPACIENT) Implements ICrystalService.GetPatientByIdentification
        Using service As IPatientAdminService = Container.Current.Resolve(Of IPatientAdminService)()
            Return service.GetPatientByIdentification(Identification)
        End Using
        'Return Me._patientAdminService.GetPatientByIdentification(Identification)
    End Function

    ''' <summary>
    ''' Elimina Pacientes
    ''' </summary>
    ''' <param name="Identification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeletePatient(Identification As String, audit As AuditMessage) As ActionResult Implements ICrystalService.DeletePatient
        Using service As IPatientAdminService = Container.Current.Resolve(Of IPatientAdminService)()
            Return service.DeletePatient(Identification, audit)
        End Using
        'Return Me._patientAdminService.DeletePatient(Identification, audit)
    End Function

    ''' <summary>
    ''' Guarda Pacientes
    ''' </summary>
    ''' <param name="patient"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePatient(patient As INPACIENT, audit As AuditMessage) As ActionResult(Of INPACIENT) Implements ICrystalService.SavePatient
        Using service As IPatientAdminService = Container.Current.Resolve(Of IPatientAdminService)()
            Return service.SavePatient(patient, audit)
        End Using
        'Return Me._patientAdminService.SavePatient(patient, audit)
    End Function
#End Region

#Region "Levels"

    ''' <summary>
    ''' Obtiene Nivel Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLevelByCode(Code As String) As ADNIVELES Implements ICrystalService.GetLevelByCode
        Using service As IPatientAdminService = Container.Current.Resolve(Of IPatientAdminService)()
            Return service.GetLevelByCode(Code)
        End Using
        'Return Me._patientAdminService.GetLevelByCode(Code)
    End Function

    Public Function SaveWeightPatient(weight As Integer, codePatient As String) As Boolean Implements ICrystalService.SaveWeightPatient
        Using service As IPatientAdminService = Container.Current.Resolve(Of IPatientAdminService)()
            Return service.SaveWeightPatient(weight, codePatient)
        End Using
        'Return Me._patientAdminService.SaveWeightPatient(weight, codePatient)
    End Function

#End Region

#End Region

End Class