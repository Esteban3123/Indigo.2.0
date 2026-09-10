#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Reporter

#End Region

Public Class FrmReportResultOperation

#Region "Variables"

    Private criterias As Dictionary(Of String, String)

#End Region

#Region "Datasource"

    Private _FillingReportType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingReportType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReportType Is Nothing Then
                _FillingReportType = New List(Of Tuple(Of Integer, String))
                _FillingReportType.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(2, "Detallado"))
            End If
            Return _FillingReportType
        End Get
    End Property

    Private _FillingDetailType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingDetailType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingDetailType Is Nothing Then
                _FillingDetailType = New List(Of Tuple(Of Integer, String))
                _FillingDetailType.Add(New Tuple(Of Integer, String)(4, "Gastos Generales"))
                _FillingDetailType.Add(New Tuple(Of Integer, String)(1, "Mano de Obra"))
                _FillingDetailType.Add(New Tuple(Of Integer, String)(5, "Activos Fijos"))
                _FillingDetailType.Add(New Tuple(Of Integer, String)(2, "Suministros"))
                _FillingDetailType.Add(New Tuple(Of Integer, String)(3, "Consumo"))
                _FillingDetailType.Add(New Tuple(Of Integer, String)(6, "Ventas"))
            End If
            Return _FillingDetailType
        End Get
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Method"

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

    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        If INDCdnMonthYearStart.GetYear IsNot Nothing And INDCdnMonthYearEnd.GetYear IsNot Nothing Then
            If INDCdnMonthYearStart.GetYear = INDCdnMonthYearEnd.GetYear Then
                If Me.INDCdnMonthYearStart.GetMonth > INDCdnMonthYearEnd.GetMonth Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("CompareRangeDate", "Commons")))
                    Me.INDCdnMonthYearStart.Focus()
                End If
            End If
        End If

        If INDCdnMonthYearStart.GetYear IsNot Nothing And INDCdnMonthYearEnd.GetYear IsNot Nothing Then
            If Me.INDCdnMonthYearStart.GetYear <> INDCdnMonthYearEnd.GetYear Then
                errors.AppendLine("Debe seleccionar el mismo año para el Rango")
                Me.INDCdnMonthYearStart.Focus()
            End If
        End If

        If INDSleGroupStart.TextEditValue <> String.Empty And INDSleGroupEnd.TextEditValue = String.Empty Or INDSleGroupEnd.TextEditValue <> String.Empty And INDSleGroupStart.TextEditValue = String.Empty Then
            errors.AppendLine(String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblGroup.Text))
            Me.INDSleGroupStart.Focus()
        ElseIf (INDSleGroupEnd.TextEditValue < INDSleGroupStart.TextEditValue) AndAlso (INDSleGroupEnd.TextEditValue.Length < INDSleGroupStart.TextEditValue.Length) Then
            errors.AppendLine(String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblGroup.Text))
            Me.INDSleGroupStart.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("Year", INDCdnMonthYearStart.GetYear)
        criterias.Add("MonthStart", INDCdnMonthYearStart.GetMonth)
        criterias.Add("MonthEnd", INDCdnMonthYearEnd.GetMonth)
        criterias.Add("DetailType", INDGleDetailType.EditValue)
        criterias.Add("CodeProductionStart", INDSleGroupStart.TextEditValue)
        criterias.Add("CodeProductionEnd", INDSleGroupEnd.TextEditValue)

        Return True
    End Function

#End Region

#Region "Events"

#Region "Load"

    Private Sub FrmReportResultOperation_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Cargar GridLookUpEdit        
        INDGleReportType.Properties.DataSource = FillingReportType
        INDGleDetailType.Properties.DataSource = FillingDetailType

        'Dar un valores por defecto
        INDGleReportType.EditValue = 1
        INDGleDetailType.EditValue = 1
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingReportType = Nothing
        _FillingDetailType = Nothing
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleGroupStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupStart.QueryPopUp
        If INDSleGroupStart.Datasource Is Nothing Then
            INDSleGroupStart.Datasource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListProductionCenterByStatusAndCenterType(True, 1)
        End If
    End Sub

    Private Sub INDSleGroupEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupEnd.QueryPopUp
        If INDSleGroupEnd.Datasource Is Nothing Then
            INDSleGroupEnd.Datasource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListProductionCenterByStatusAndCenterType(True, 1)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDGleReportType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleReportType.EditValueChanged
        If INDGleReportType.EditValue = 1 Then
            INDLciDetailType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleDetailType.EditValue = Nothing
        Else
            INDLciDetailType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGleDetailType.EditValue = 1
        End If
    End Sub

#End Region

#Region "Report"

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            Dim reporte As Object = Nothing
            Select Case INDGleReportType.EditValue
                Case 1
                    reporte = New rptCostListResultOperation
                Case 2
                    reporte = New rptCostListResultOperationDetail
                Case Else
                    Exit Sub
            End Select

            AsyncLoader(True)
            reporte.ParametrosReporte = New Object() {criterias}
            INDDvReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncReport.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            End If
        End If
    End Sub

    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
    End Sub

#End Region

#End Region

End Class