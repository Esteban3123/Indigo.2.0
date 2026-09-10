Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent
Imports Domain.AccountManagement.Model

Public Class PDashboardAccountManagement

#Region "Fields"
    ''' <summary>
    ''' Interfaz del formulario
    ''' </summary>
    Dim View As IDashboardAccountManagement

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues
#End Region

#Region "Builder"
    Public Sub New(ByRef iView As IDashboardAccountManagement)
        View = iView
        Indigo = SessionValues.Instance
    End Sub
#End Region
#Region "Methods"

    ''' <summary>
    ''' Obtiene la lista de todos los grupos de atención
    ''' </summary>
    Public Sub InitializeAttentionCenter()
        View.AttentionCenterXpo = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListCenters()
    End Sub

    ''' <summary>
    ''' Obtiene y establece el datasource de areas de gestión
    ''' </summary>
    Public Sub GetManagementAreas()
        View.ManagementAreasXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).AccountManagementService.ListManagementAreas()
    End Sub

    ''' <summary>
    ''' Obtiene y establece el datasource de trazabilidad de folios
    ''' </summary>
    Public Async Sub ListFolioEventsTraceability(attentionCenterCode As String, userCode As String)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.ListFolioEventsAsync(attentionCenterCode, Nothing, Nothing, userCode)
        View.TraceabilityDatasource = res.ObjectEmbbeded
    End Sub

    ''' <summary>
    ''' Obtiene y establece el datasource de los ingresos
    ''' </summary>
    Public Sub ListAllAdmissions()
        View.AdmissionsXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).CrystalService.ListAllAdmissions()
    End Sub

    ''' <summary>
    ''' Obtiene y establece el datasource de los pacientes
    ''' </summary>
    Public Sub ListAllPatients()
        View.PatientsXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).CrystalService.ListAllPatients()
    End Sub
#End Region

End Class
