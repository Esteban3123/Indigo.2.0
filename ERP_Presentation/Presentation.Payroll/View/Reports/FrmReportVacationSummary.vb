'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Oscar Stiven Astudillo
' Created          : 2025-11-11
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Reporter
Imports Presentation.Payroll.MVP
Imports Domain.Payroll.Entities


#End Region


Public Class FrmReportVacationSummary
    Implements IReportVacationSummary


#Region "Variables"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variables para almacenar el periodo de liquidacion
    ''' </summary>
    Dim MonthClosed As Integer = 0
    Dim YearClosed As Integer = 0

    ''' <summary>
    ''' Presentador MVP
    ''' </summary>
    Dim _presenter As PReportVacationSummary

#End Region


#Region "Properties"

    Public Property EmployeeId As Integer?
        Get
            If INDSleemployee.EditValue Is Nothing OrElse String.IsNullOrEmpty(INDSleemployee.EditValue.ToString()) Then
                Return Nothing
            End If
            Return INDSleemployee.EditValue
        End Get
        Set(value As Integer?)
            If value = 0 Then
                INDSleemployee.EditValue = Nothing
            Else
                INDSleemployee.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtencion de nit del empleado
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EmployeeNit As String
        Get
            If INDSleemployee.EditValue Is Nothing OrElse String.IsNullOrEmpty(INDSleemployee.EditValue.ToString()) Then
                Return String.Empty
            End If
            Dim view = INDSleemployee.Properties.View
            If view IsNot Nothing Then
                Dim rowHandle = view.FocusedRowHandle
                Dim nitValue = view.GetRowCellValue(rowHandle, "Nit")
                If nitValue IsNot Nothing AndAlso Not IsDBNull(nitValue) Then
                    Return nitValue.ToString()
                End If
            End If
            Return String.Empty
        End Get
    End Property

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
    ''' Establece fuente al dataSource
    ''' </summary>
    Public WriteOnly Property VacationSummaryDataSource As List(Of SP_SummaryVacationLiquidation_Result) Implements IReportVacationSummary.VacationSummaryDataSource
        Set(value As List(Of SP_SummaryVacationLiquidation_Result))
            INDgcVacationSummary.DataSource = value
            ConfigureColumnSummaries()
        End Set
    End Property

#End Region

#Region "Load"
    Private Sub FrmReportVacationSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Inicializar el presentador MVP
        _presenter = New PReportVacationSummary(Me)
        AssingCurrentDate()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyNavigationControl)
        ShowPannelBase()
        Me.BarraBotones.Minimizar(True)
        INDlciMontClose.Enabled = True
    End Sub
#End Region



#Region "Handlers"

    ''' <summary>
    ''' Asignacion fecha actual
    ''' </summary>
    Private Sub AssingCurrentDate()
        MonthClosed = INDdnMontClose.GetMonth
        YearClosed = INDdnMontClose.GetYear
    End Sub

    ''' <summary>
    ''' Configura los totales (summaries) de las columnas del GridView
    ''' </summary>
    Private Sub ConfigureColumnSummaries()
        INDgvVacationSummary.OptionsView.ColumnAutoWidth = False
        INDgvVacationSummary.OptionsBehavior.AutoPopulateColumns = False

        If INDgvVacationSummary.Columns("EmployeeNit").Summary.Count > 0 Then
            For Each column As DevExpress.XtraGrid.Columns.GridColumn In INDgvVacationSummary.Columns
                If column.FieldName <> "" Then
                    column.Summary.Clear()
                    column.MinWidth = CInt(column.Width * 0.8)
                    column.MaxWidth = CInt(column.Width * 1.5)
                End If
            Next
        End If

        INDgvVacationSummary.Columns("EmployeeNit").Summary.Add(DevExpress.Data.SummaryItemType.Count, "EmployeeNit", "N° Registros: {0}")
        INDgvVacationSummary.Columns("AccumulatedDays").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "AccumulatedDays", "{0:N2}")
        INDgvVacationSummary.Columns("TakenDaysRegistered").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TakenDaysRegistered", "{0:N2}")
        INDgvVacationSummary.Columns("PendingDaysRegistered").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PendingDaysRegistered", "{0:N2}")
        INDgvVacationSummary.Columns("PendingDays").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PendingDays", "{0:N2}")
        INDgvVacationSummary.Columns("EnjoyedDaysDF").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "EnjoyedDaysDF", "{0:N2}")
        INDgvVacationSummary.Columns("VacationDaysInLiquidationMonth").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "VacationDaysInLiquidationMonth", "{0:N2}")

        INDgvVacationSummary.Columns("BaseLiquidationValue").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "BaseLiquidationValue", "{0:c2}")
        INDgvVacationSummary.Columns("VacationValue").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "VacationValue", "{0:c2}")
        INDgvVacationSummary.Columns("HealthValue").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "HealthValue", "{0:c2}")
        INDgvVacationSummary.Columns("PensionValue").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PensionValue", "{0:c2}")
        INDgvVacationSummary.Columns("NetVacationValue").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "NetVacationValue", "{0:c2}")
        INDgvVacationSummary.Columns("LastLiquidationIBC").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "LastLiquidationIBC", "{0:c2}")


    End Sub


    '' <summary>
    ''' metodo para Cargar el data source Del Control INDSleEmployee
    ''' <remarks></remarks>
    Private Sub LoadXpoEmployee()
        Using msearch As New MBusqueda
            Dim resEmployee = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllEmployee)
            INDSleemployee.Properties.DataSource = resEmployee
        End Using
    End Sub

    ''' <summary>
    ''' Se ejecuta en el evento QueryPopUp del Control INDSleEmployees
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Se ejecuta cuando se despliega el control empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleemployee_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleemployee.QueryPopUp
        If EmployeeId Is Nothing Then
            LoadXpoEmployee()
        End If
    End Sub

    ''' <summary>
    ''' indica cuando se cambio el periodo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDdnMontClose_OnChangeDate(sender As Object, e As EventArgs) Handles INDdnMontClose.OnChangeDate
        AssingCurrentDate()
    End Sub


#End Region


#Region "Methods"

    ''' <summary>
    ''' Evento generacion reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        loadReport()
    End Sub


    ''' <summary>
    ''' Limpiar controles
    ''' </summary>
    Public Sub Deshacer()
        EmployeeId = 0
        ShowPannelBase()
        Me.BarraBotones.Minimizar(True)
    End Sub


    ''' <summary>
    ''' Carga segmentos para reporte del cierre
    ''' </summary>
    Private Async Sub loadReport()
        Try
            AsyncLoader(True)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
            Me.BarraBotones.Minimizar(False)
            Await _presenter.LoadVacationSummary(YearClosed, MonthClosed, EmployeeId)
            ShowPannelReport()
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = "Error al cargar el reporte: " & ex.Message
        Finally
            AsyncLoader(False)
        End Try
    End Sub


    ''' <summary>
    ''' Muestra el panel inicial
    ''' </summary>
    Private Sub ShowPannelBase()
        INDPanelControlBase.Visible = False
        INDPanelControlReport.Visible = False
        INDPanelControlReport.Dock = DockStyle.None
        INDPanelControlBase.Dock = DockStyle.Fill
        INDPanelControlBase.BringToFront()
        INDPanelControlBase.Visible = True
    End Sub


    ''' <summary>
    ''' Muestra el panel del reporte
    ''' </summary>
    Private Sub ShowPannelReport()
        INDPanelControlBase.Visible = False
        INDPanelControlReport.Visible = False
        INDPanelControlReport.Dock = DockStyle.Fill
        INDPanelControlReport.BringToFront()
        INDPanelControlReport.Visible = True
    End Sub




#End Region




End Class