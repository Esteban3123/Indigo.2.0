#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.InteropCostRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP

#End Region

Public Class FrmReportListCost

#Region "Property"
    Private reporte As Object

    Private _FillingReportType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingReportType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReportType Is Nothing Then
                _FillingReportType = New List(Of Tuple(Of Integer, String))
                _FillingReportType.Add(New Tuple(Of Integer, String)(1, "Mano de Obra"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(2, "Gestión General"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(1, "Suministros"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(2, "Activos Fijos"))
            End If
            Return _FillingReportType
        End Get
    End Property
#End Region


#Region "Method"
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
#End Region


#Region "Event"
    Private Sub FrmReportListCost_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar GridLookUpEdit
        INDGleTypeReport.Properties.DataSource = FillingReportType

        'Dar un valor por defecto a los GridLookEdit
        INDGleTypeReport.EditValue = 1
    End Sub
    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDPcReport.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDCnReport.Visible = True
        Me.INDGleTypeReport.Focus()
    End Sub


#End Region
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        reporte = Nothing
        _FillingReportType = Nothing
    End Sub

    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        AsyncLoader(True)
        Select Case INDGleTypeReport.EditValue
            Case 1
                reporte = New rptGeneralProfitabilityTotalCost
            Case Else
                reporte = New rptGeneralProfitabilityGraph
        End Select
        reporte.ParametrosReporte = New Object() {INDDnYearMonthStart.GetYear,
                                                  INDDnYearMonthStart.GetMonth,
                                                  INDDnYearMonthEnd.GetYear,
                                                  INDDnYearMonthEnd.GetMonth}

        INDDvReport.DocumentSource = reporte
        reporte.CargarDataSource()
        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
            reporte.CreateDocument(True)
        End If
        AsyncLoader(False)
        If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
            Me.INDLcBase.Visible = False
            Me.INDNcpReport.Visible = False
            Me.INDPcReport.Visible = True
            INDDvReport.Show()
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            INDGleTypeReport.Focus()
        End If
    End Sub
End Class