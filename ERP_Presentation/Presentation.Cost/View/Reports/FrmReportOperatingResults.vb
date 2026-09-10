#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Reporter

#End Region

Public Class FrmReportOperatingResults

#Region "Datasource"

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Estructura Organizacional"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Centro de Producción"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingSortBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingSortBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingSortBy Is Nothing Then
                _FillingSortBy = New List(Of Tuple(Of Integer, String))
                _FillingSortBy.Add(New Tuple(Of Integer, String)(1, "Centro de Costo"))
                _FillingSortBy.Add(New Tuple(Of Integer, String)(2, "Costo Total"))
                _FillingSortBy.Add(New Tuple(Of Integer, String)(3, "Facturado"))
                _FillingSortBy.Add(New Tuple(Of Integer, String)(4, "Utilidad"))
                _FillingSortBy.Add(New Tuple(Of Integer, String)(5, "Porcentaje Margen"))
                _FillingSortBy.Add(New Tuple(Of Integer, String)(6, "Porcentaje Utilidad"))
            End If
            Return _FillingSortBy
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

        If INDDnYearMonthStart.GetYear <> INDDnYearMonthEnd.GetYear Then
            errors.AppendLine("El año inicial debe ser igual al año final.")
        End If

        If (INDSleProductionCenterStart.EditValue IsNot Nothing And INDSleProductionCenterEnd.EditValue Is Nothing Or INDSleProductionCenterEnd.EditValue IsNot Nothing And INDSleProductionCenterStart.EditValue Is Nothing) Or (INDSleProductionCenterStart.TextEditValue <> String.Empty And INDSleProductionCenterEnd.TextEditValue = String.Empty Or INDSleProductionCenterEnd.TextEditValue <> String.Empty And INDSleProductionCenterStart.TextEditValue = String.Empty) Then
            errors.AppendLine(String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblProductionCenter.Text))
            Me.INDSleProductionCenterStart.Focus()
        ElseIf (INDSleProductionCenterEnd.TextEditValue < INDSleProductionCenterStart.TextEditValue) Or (INDSleProductionCenterEnd.EditValue < INDSleProductionCenterStart.EditValue) Then
            errors.AppendLine(String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblProductionCenter.Text))
            Me.INDSleProductionCenterStart.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        Return True
    End Function

#End Region

#Region "Events"

#Region "Load"

    Private Sub FrmReportOperatingResults_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Cargar GridLookUpEdit        
        INDGleTypeReport.Properties.DataSource = FillingTypeReport
        INDGleSortBy.Properties.DataSource = FillingSortBy

        'Dar un valores por defecto
        INDGleTypeReport.EditValue = 1
        INDGleSortBy.EditValue = 7
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingSortBy = Nothing
        _FillingTypeReport = Nothing
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleOrganizationalStructure_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleOrganizationalStructure.QueryPopUp
        If INDsleOrganizationalStructure.Properties.DataSource Is Nothing Then
            INDsleOrganizationalStructure.Properties.DataSource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListCostOrganizationalStructureOfCostsWithPCenterReport()
        End If
    End Sub

    Private Sub INDSleProductionCenterStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductionCenterStart.QueryPopUp
        If INDSleProductionCenterStart.Datasource Is Nothing Then
            INDSleProductionCenterStart.Datasource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListProductionCenterReport()
        End If
    End Sub

    Private Sub INDSleProductionCenterEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductionCenterEnd.QueryPopUp
        If INDSleProductionCenterEnd.Datasource Is Nothing Then
            INDSleProductionCenterEnd.Datasource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListProductionCenterReport()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 1 Then
            INDliOrganizationalStructure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciGleSortBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleSortBy.EditValue = Nothing
        ElseIf INDGleTypeReport.EditValue = 2 Then
            INDliOrganizationalStructure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciGleSortBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDsleOrganizationalStructure.EditValue = Nothing
            INDGleSortBy.EditValue = 3
        End If
    End Sub

#End Region

#Region "Report"

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            AsyncLoader(True)
            Dim reporte As Object = Nothing

            If INDGleTypeReport.EditValue = 1 Then
                reporte = New rptCostOperatingResultsByCostCenter
                reporte.ParametrosReporte = New Object() {
                    INDDnYearMonthStart.GetYear,
                    INDDnYearMonthStart.GetMonth,
                    INDDnYearMonthEnd.GetYear,
                    INDDnYearMonthEnd.GetMonth,
                    INDSleProductionCenterStart.TextEditValue,
                    INDSleProductionCenterEnd.TextEditValue,
                    If(INDsleOrganizationalStructure.EditValue Is Nothing, 0, INDsleOrganizationalStructure.EditValue)
                }
            Else
                reporte = New rptCostOperatingResultsByProductionCenter
                reporte.ParametrosReporte = New Object() {
                    INDDnYearMonthStart.GetYear,
                    INDDnYearMonthStart.GetMonth,
                    INDDnYearMonthEnd.GetYear,
                    INDDnYearMonthEnd.GetMonth,
                    INDSleProductionCenterStart.TextEditValue,
                    INDSleProductionCenterEnd.TextEditValue,
                    INDGleSortBy.EditValue
                }
            End If

            INDDvReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)

            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDNcpReport.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            End If
        End If
    End Sub

    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDNcpReport.Visible = True
        Me.INDPcReport.Visible = False
    End Sub

#End Region

#End Region

End Class