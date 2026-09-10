'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/02/2019
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
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PAccountControlAmbulatory

#Region "Fields"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    ''' <remarks></remarks>
    Dim View As IAccountControlAmbulatory

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    ''' <remarks></remarks>
    Dim Indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview"></param>
    ''' <remarks></remarks>
    Public Sub New(ByRef iview As IAccountControlAmbulatory)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Inicializa un nuevo constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' lista los centros de atencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCentersHIS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListCentersPatientDepartureHIS(Indigo.AuditMessageWcf.CodeUser)
    End Function

    ''' <summary>
    ''' Lista los ingresos de los pacientes por código de centro de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAdmissions(CareCenterCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListViewAdmissionsToAccountControlAmbulatoryByCareCenterCode(CareCenterCode)
    End Function

    ''' <summary>
    ''' Obtiene el ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAmissionByAdmissionNumber(AdmissionCode As String) As ViewLiquidationGetAdmissionAll
        Dim filter As String = "AdmissionCode = '" & AdmissionCode & "'"
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of ViewLiquidationGetAdmissionAll)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista los procedimientos odontológicos
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListOdontologyProcedures(AdmissionCode As String, PatientCode As String, CareCenterCode As String) As List(Of ViewOdontologyProceduresXpo)
        Dim filter As String = "AdmissionNumber = '" & AdmissionCode & "' and PatientCode = '" & PatientCode & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of ViewOdontologyProceduresXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista los laboratorios
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListLaboratories(AdmissionCode As String, PatientCode As String, CareCenterCode As String) As List(Of ViewLaboratoriesAccountControlAmbulatoryXpo)
        Dim filter As String = "AdmissionNumber = '" & AdmissionCode & "' and PatientCode = '" & PatientCode & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of ViewLaboratoriesAccountControlAmbulatoryXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista las patologias
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListPathologies(AdmissionCode As String, PatientCode As String, CareCenterCode As String) As List(Of ViewPathologiesAccountControlAmbulatoryXpo)
        Dim filter As String = "AdmissionNumber = '" & AdmissionCode & "' and PatientCode = '" & PatientCode & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of ViewPathologiesAccountControlAmbulatoryXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista las imagenes
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListImages(AdmissionCode As String, PatientCode As String, CareCenterCode As String) As List(Of ViewImagesAccountControlAmbulatoryXpo)
        Dim filter As String = "AdmissionNumber = '" & AdmissionCode & "' and PatientCode = '" & PatientCode & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of ViewImagesAccountControlAmbulatoryXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista las interconsultas
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListInterconsults(AdmissionCode As String, PatientCode As String, CareCenterCode As String) As List(Of ViewInterconsultsAccountControlAmbulatoryXpo)
        Dim filter As String = "AdmissionNumber = '" & AdmissionCode & "' and PatientCode = '" & PatientCode & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of ViewInterconsultsAccountControlAmbulatoryXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista las notas administrativas
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListAdministrativeNotes(AdmissionCode As String, PatientCode As String, CareCenterCode As String) As List(Of ViewAdministrativeNotesAccountControlAmbulatoryXpo)
        Dim filter As String = "AdmissionNumber = '" & AdmissionCode & "' and PatientCode = '" & PatientCode & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of ViewAdministrativeNotesAccountControlAmbulatoryXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Lista las citas del ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListAppointments(AdmissionCode As String, PatientCode As String) As List(Of ViewListAppointmentsXpo)
        Dim filter As String = "AdmissionCode = '" & AdmissionCode & "' and PatientCode = '" & PatientCode & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of ViewListAppointmentsXpo)(Nothing, filter).ToList()
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
    ''' Lista las rias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRIASCups(cupsCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListRIASCupsByCupsCodeAndStatus(cupsCode, 1)
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
    ''' Obtiene la unidad funcional por código
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnitByCode(Code As String) As FunctionalUnitXpo
        Dim filter As String = "Code = '" & Code & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of FunctionalUnitXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene la unidad funcional por id
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnitById(Id As Integer) As FunctionalUnitXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of FunctionalUnitXpo)(Nothing, filter).FirstOrDefault()
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
    ''' Obtiene el contrato
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetViewRIASCupsByRiasCupsId(RiasCupsId As Integer) As ViewRIASCupsXpo
        Dim filter As String = "RiasCupsId = " & RiasCupsId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of ViewRIASCupsXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista los salarios minimos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllMinWage() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.HisContainer).CrystalService.ListMinWage()
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

#End Region

End Class
