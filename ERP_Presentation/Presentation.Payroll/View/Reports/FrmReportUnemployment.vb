Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
Imports DevExpress.Spreadsheet

Public Class FrmReportUnemployment

    Private _pucModel As MCommon

    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource

    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private _FillingStatus As List(Of Tuple(Of String, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of String, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of String, String))
                _FillingStatus.Add(New Tuple(Of String, String)(1, "Confirmados"))
                _FillingStatus.Add(New Tuple(Of String, String)(0, "Sin Confirmar"))
                _FillingStatus.Add(New Tuple(Of String, String)(2, "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property
    ''' <summary>
    ''' Propiedad que trae el estado que asigne el usuario en el formulario
    ''' </summary>
    ''' <returns></returns>
    Private Property Status As String
        Get
            Return INDGleStatus.EditValue
        End Get
        Set(value As String)
            INDGleStatus.EditValue = value
        End Set
    End Property
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True



        If INDSleGroupStart.EditValue Is Nothing And INDSleGroupEnd.EditValue IsNot Nothing Or INDSleGroupEnd.EditValue Is Nothing And INDSleGroupStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        ElseIf INDSleGroupEnd.EditValue < INDSleGroupStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroup.Text)
            Me.INDSleGroupStart.Focus()
            Validations = False
        End If
        Return Validations
    End Function

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

    Private Sub LoadXpoGroupStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupStart.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    Private Sub LoadXpoGroupEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupEnd.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    Private Sub LoadXpoEmployee()
        Using msearch As New MBusqueda
            ProoftCloseXpoEmployee = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllEmployee)
            INDSleEmployee.Datasource = ProoftCloseXpoEmployee
        End Using
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        '_pucModel.Dispose()
        '_pucModel = Nothing
        ProoftCloseXpoEmployee = Nothing
        ProoftCloseXpoGroup = Nothing
    End Sub

    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            AsyncLoader(True)
            Dim reporte As New rptUnemployedLiquidation()

            Dim prms As New With {
                Key .Año = INDdnYear.GetYear,
                Key .EmployeeId = INDSleEmployee.EditValue,
                Key .Estado = INDGleStatus.EditValue,
                Key .StartGroup = INDSleGroupStart.EditValue,
                Key .EndGroup = INDSleGroupEnd.EditValue
            }

            reporte.ParametrosReporte = {prms}

            INDDvViewReport.DocumentSource = reporte

            reporte.CargarDataSource()
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
            End If
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))

            End If
        End If
    End Sub

    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False

    End Sub

    Private Sub FrmReportControlLiquidation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDGleStatus.Properties.DataSource = FillingStatus
        'Asigno un valor por defecto para el estado
        Status = 2
    End Sub

    Private Sub INDSleGroupStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupStart.QueryPopUp
        If INDSleGroupStart.Datasource Is Nothing Then
            LoadXpoGroupStart()
        End If
    End Sub

    Private Sub INDSleGroupEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupEnd.QueryPopUp
        If INDSleGroupEnd.Datasource Is Nothing Then
            LoadXpoGroupEnd()
        End If
    End Sub

    Private Sub INDSleEmployee_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEmployee.QueryPopUp
        If INDSleEmployee.Datasource Is Nothing Then
            LoadXpoEmployee()
        End If
    End Sub


End Class