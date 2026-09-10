#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP

Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Windows.Forms


#End Region

Public Class FrmDepartureBallotListt

#Region "Properties"


    Public Property ProoftCloseXpoPatient As XPInstantFeedbackSource
    Public Property ProoftCloseXpoHealthAdministrator As XPInstantFeedbackSource

    Public Property ProoftCloseXpoAdmissionNumber As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCreationUsers As LinqInstantFeedbackSource

    Dim report As Object

    Private _LoadTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property LoadTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeReport Is Nothing Then
                _LoadTypeReport = New List(Of Tuple(Of Integer, String))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(2, "Detallado"))
            End If
            Return _LoadTypeReport
        End Get
    End Property

#End Region

#Region "Method"

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbReportGenerate
    ''' </summary>
    ''' ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>    
    Private Async Sub indsbreportgenerate_click(sender As Object, e As EventArgs) Handles INDSbReportGenerate.Click

        If ValidateControlsReports() = True Then
            AsyncLoader(True)
            Me.Cursor = BaseClass.ChangeCursorIndigo()

            Select Case INDGleTypeReport.EditValue
                Case 1
                    report = New rptDepartureBallotList
                Case Else
                    report = New rptSlipOutDetailed
            End Select

            report.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                      INDDateEnd.EditValue,
                                                      INDSleHealthAdministrator.TextEditValue,
                                                      INDSlePatient.TextEditValue,
                                                      INDsleAdmissionNumber.TextEditValue,
                                                      INDSleUser.TextEditValue}


            INDDvReport.DocumentSource = report
            'Await CType(reporte, IReportAsync).CargarDataSourceAsync()
            Await report.CargarDataSourceAsync

            If DirectCast(report.DataSource, ICollection).Count > 0 Then
                report.CreateDocument(True)
            End If
            Me.Cursor = Cursors.Default
            AsyncLoader(False)
            If DirectCast(report.DataSource, ICollection).Count > 0 Then
                Me.INDLcBaseHome.Visible = False
                Me.INDCncReport.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateStart.Focus()
            End If

        End If
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
        Set(value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleHealthAdministrator
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoHealthAdministrator()
        Using msearch As New MBusqueda
            ProoftCloseXpoHealthAdministrator = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListHealthAdministrator)
            INDSleHealthAdministrator.Datasource = ProoftCloseXpoHealthAdministrator
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlePatient
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBillingPatient()
        Using msearch As New MBusqueda
            ProoftCloseXpoPatient = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllPatients)
            INDSlePatient.Datasource = ProoftCloseXpoPatient
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAdmissionNumber
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAdmissionNumber()
        Using msearch As New MBusqueda
            ProoftCloseXpoAdmissionNumber = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAdmissionsToLiquidation)
            INDsleAdmissionNumber.Datasource = ProoftCloseXpoAdmissionNumber
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleUser
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCreationUsers()
        Using msearch As New MBusqueda
            ProoftCloseXpoCreationUsers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCreationUsersReportTreasury)
            INDSleUser.Datasource = ProoftCloseXpoCreationUsers
        End Using
    End Sub
#End Region



#Region "Event"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        report = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBaseHome.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
    End Sub

    Private Sub FrmReportListInvoices_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = LoadTypeReport

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
        INDSleHealthAdministrator.View.OptionsView.ShowGroupPanel = False
        INDSlePatient.View.OptionsView.ShowGroupPanel = False
        INDsleAdmissionNumber.View.OptionsView.ShowGroupPanel = False
        INDSleUser.View.OptionsView.ShowGroupPanel = False

    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSlePatient
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePatient_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSlePatient.QueryPopUp
        If INDSlePatient.Datasource Is Nothing Then
            LoadXpoBillingPatient()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleHealthAdministrator
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleHealthAdministrator_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleHealthAdministrator.QueryPopUp
        If INDSleHealthAdministrator.Datasource Is Nothing Then
            LoadXpoHealthAdministrator()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDsleAdmissionNumber
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdmissionNumber_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleAdmissionNumber.QueryPopUp
        If INDsleAdmissionNumber.Datasource Is Nothing Then
            LoadXpoAdmissionNumber()
        End If
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    Private Sub INDSleUser_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleUser.QueryPopUp
        If INDSleUser.Datasource Is Nothing Then
            LoadXpoCreationUsers()
        End If
    End Sub
#End Region
End Class