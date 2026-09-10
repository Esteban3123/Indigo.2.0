#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmReportIncomeAndWithholding

#Region "Properties"
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource
#End Region

#Region "Method"
    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        If INDSleEmployee.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDLciEmployee.Text)
            Me.INDSleEmployee.Focus()
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
    ''' metodo para Cargar el data source Del Control INDSleEmployee
    ''' </summary>
    Private Sub LoadXpoEmployee()
        Using msearch As New MBusqueda
            ProoftCloseXpoEmployee = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllEmployee)
            INDSleEmployee.Datasource = ProoftCloseXpoEmployee
        End Using
    End Sub
#End Region


#Region "Event"
    ''' <summary>
    ''' Evento que imprime el reporte y valida el año que s euqiere consultar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Not ValidateControlsReports() Then Exit Sub

        AsyncLoader(True)

        Dim reporte As Object
        If INDCdnYear.GetYear <= 2022 Then
            reporte = New rptPayrollDian()
        Else
            reporte = New rptPayrollDian2023()
        End If

        reporte.ParametrosReporte = {INDCdnYear.GetYear, INDSleEmployee.EditValue}
        INDDvReport.DocumentSource = reporte

        Await reporte.CargarDataSourceAsync()

        If reporte.DataSource IsNot Nothing Then
            reporte.CreateDocument(True)
            DisplayReport()
        Else
            ShowErrorMessage()
            Me.INDSleEmployee.Focus()
        End If

        AsyncLoader(False)
    End Sub

    Private Sub DisplayReport()
        Me.INDLcBase.Visible = False
        Me.INDCncReport.Visible = False
        Me.INDPcReport.Visible = True
        INDDvReport.Show()
    End Sub

    Private Sub ShowErrorMessage()
        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
    End Sub

    Private Sub INDSleEmployee_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEmployee.QueryPopUp
        If INDSleEmployee.Datasource Is Nothing Then
            LoadXpoEmployee()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDSleEmployee.Focus()
    End Sub
#End Region
End Class