#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo.CrystalRepository

#End Region

Public Class FrmReportSlipOut

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"

    ''' <summary>
    ''' listado de adminiones
    ''' </summary>
    ''' <remarks></remarks
    Public Property ProoftCloseXpoAdmissionNumber As XPInstantFeedbackSource

    Public Property itemAdmission As XPCollection(Of ViewAdmissionsToReportSlipOut)

#End Region

#Region "Methods"

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
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        If Me.INDsleAdmissionNumber.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("UnselectedAdmission", "Billing"))
            Me.INDsleAdmissionNumber.Focus()
            Validations = False
        End If

        Return Validations
    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAdmissionNumber
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAdmissionNumber()
        Using msearch As New MBusqueda
            ProoftCloseXpoAdmissionNumber = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAdmissionsToReportSlipOut)
            INDsleAdmissionNumber.Datasource = ProoftCloseXpoAdmissionNumber
        End Using
    End Sub

#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
    End Sub


    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCncReturn.ClickBack
        Me.INDLcBaseHome.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
    End Sub

    Private Sub INDsleAdmissionNumber_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAdmissionNumber.QueryPopUp
        If INDsleAdmissionNumber.Datasource Is Nothing Then
            LoadXpoAdmissionNumber()
        End If
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    Private Sub FrmReportListInvoices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDsleAdmissionNumber.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAdmissionNumberByNumberSlipOut

        INDsleAdmissionNumber.View.OptionsView.ShowGroupPanel = False
    End Sub

    Private Sub INDsleAdmissionNumber_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDsleAdmissionNumber.EditValueChanged
        If INDsleAdmissionNumber.EditValue IsNot Nothing Then
            Using msearch As New MBusqueda
                Dim filter As Object() = {INDsleAdmissionNumber.EditValue}
                itemAdmission = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAdmissionsToReportSlipOutById, filter)
                If itemAdmission IsNot Nothing AndAlso itemAdmission.Count > 0 Then
                    INDTxtPatient.Text = itemAdmission(0).PatientName
                    INDTxtDateEntry.Text = itemAdmission(0).AdmissionDate
                    INDTxtDateEgress.Text = itemAdmission(0).AdmissionDate
                    INDTxtBed.Text = itemAdmission(0).BedStay
                End If
            End Using
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("UnselectedAdmission", "Billing"))
            Me.INDsleAdmissionNumber.Focus()
        End If
    End Sub

    Private Async Sub INDSbReportGenerate_Click(sender As Object, e As EventArgs) Handles INDSbReportGenerate.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptSlipOut
            reporte.ParametrosReporte = New Object() {INDsleAdmissionNumber.EditValue}
            INDDvDocumentViewer.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
            End If
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBaseHome.Visible = False
                Me.INDCncReport.Visible = False
                Me.INDPcReport.Visible = True
                INDDvDocumentViewer.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDsleAdmissionNumber.Focus()
            End If
        End If
    End Sub

#End Region

End Class