'***********************************************************************
' Assembly         : Presentation.Payroll
' Author           : Oscar stiven Astudillo
' Created          : 2026-02-08
' Description      : Formulario de reporte liquidacion de contratos
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
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
Imports Presentation.Controls
Imports System.Windows.Forms
Imports Presentation.Payroll.MVP
Imports System.Data.SqlClient

#End Region

Public Class FrmReportContractLiquidation

#Region "Properties"
    ''' <summary>
    ''' Fuente de datos para los empleados
    ''' </summary>
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesión
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Propiedad para registrar mensajes en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Tipo de icono a mostrar (Advertencia, Información, Error)</param>
    ''' <value>Mensaje a mostrar</value>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

#Region "Load"

    ''' <summary>
    ''' Se ejecuta cuando se carga el formulario
    ''' Inicializa los controles y configura el estado inicial
    ''' </summary>
    ''' <param name="sender">Objeto que dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Private Sub FrmReportContractLiquidation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.PrepareToolbar(eAction.OnlyNavigationControl)
        ShowPannelBase()
        Me.BarraBotones.Minimizar(True)
    End Sub

    ''' <summary>
    ''' Se ejecuta cuando se elimina el formulario
    ''' Libera los recursos utilizados
    ''' </summary>
    ''' <param name="sender">Objeto que dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ProoftCloseXpoEmployee = Nothing
    End Sub

#End Region

#Region "Validation"

    ''' <summary>
    ''' Realiza las validaciones de los controles del formulario antes de generar el reporte
    ''' </summary>
    ''' <returns>True si todas las validaciones son exitosas, False en caso contrario</returns>
    Private Function ValidateControlsReports() As Boolean
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

#End Region

#Region "Data Methods"

    ''' <summary>
    ''' Carga el data source del control INDSleEmployee con la lista de empleados
    ''' </summary>
    Private Sub LoadXpoEmployee()
        Using msearch As New MBusqueda
            ProoftCloseXpoEmployee = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllEmployee)
            INDSleEmployee.Datasource = ProoftCloseXpoEmployee
        End Using
    End Sub

    ''' <summary>
    ''' Carga el reporte de detalle de liquidación en el grid de forma asíncrona
    ''' </summary>
    Private Async Sub LoadDataLiquidation()
        Try
            AsyncLoader(True)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
            Me.BarraBotones.Minimizar(False)

            ' Obtener valores de los controles
            Dim startDate As Date = INDDateStart.EditValue
            Dim endDate As Date = INDDateEnd.EditValue
            Dim employeeId As Integer? = INDSleEmployee.EditValue

            Dim dtLiquidationDetailReport As DataTable = Nothing
            Using model As New MContractLiquidation(MContractLiquidation.TAG)
                dtLiquidationDetailReport = Await model.GetContractLiquidationDetailReportAsync(startDate, endDate, employeeId)
            End Using
            If dtLiquidationDetailReport IsNot Nothing AndAlso dtLiquidationDetailReport.Rows.Count > 0 Then
                INDgcContractLiquidation.DataSource = dtLiquidationDetailReport
                ConfigureGridColumns()
                IndigoGridControl1.SetExportButton(INDgcContractLiquidation, True)
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros para los parámetros seleccionados"
            End If
            ShowPannelReport()
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = "Error al cargar la informacion: " & ex.Message
        Finally
            AsyncLoader(False)
        End Try
    End Sub

#End Region

#Region "Methods GridControl"

    ''' <summary>
    ''' Diccionario de traducciones para los nombres de columnas fijas del grid
    ''' </summary>
    ''' <returns>Diccionario con las traducciones de inglés a español</returns>
    Private ReadOnly Property ColumnTranslations As Dictionary(Of String, String)
        Get
            Dim translations As New Dictionary(Of String, String)
            translations("DocumentNumber") = "Número de Documento"
            translations("EmployeeName") = "Nombre Empleado"
            translations("PositionName") = "Nombre Cargo"
            translations("FunctionalUnitName") = "Nombre Unidad Funcional"
            translations("CodeGroup") = "Código Grupo"
            translations("NameGroup") = "Nombre Grupo"
            translations("JobBondingDate") = "Fecha de Contratación"
            translations("BasicSalary") = "Salario Básico"
            translations("StatusDescription") = "Estado liquidacion"

            translations("RetirementDate") = "Fecha retiro"
            translations("RetirementReason") = "Razon retiro"
            translations("TotalDaysWorked") = "Días trabajados"

            translations("TotalAccrued") = "Total Devengado"
            translations("TotalDeducted") = "Total Deducido"
            translations("NetPay") = "Neto a Pagar"

            Return translations
        End Get
    End Property


    ''' <summary>
    ''' Configura las columnas del grid para mejorar la visualización y exportación a Excel
    ''' Aplica traducciones, formatos de moneda, sumatorias y ajustes de ancho
    ''' </summary>
    Private Sub ConfigureGridColumns()
        Dim gridView As GridView = TryCast(INDgcContractLiquidation.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

        If gridView Is Nothing Then Return

        ' Configuración básica del grid
        gridView.OptionsView.ColumnAutoWidth = False
        gridView.OptionsView.ShowFooter = True
        gridView.OptionsMenu.EnableColumnMenu = True
        gridView.OptionsMenu.EnableFooterMenu = True
        gridView.OptionsMenu.EnableGroupPanelMenu = False

        ' Ajustar automáticamente el ancho de las columnas según el contenido
        gridView.BestFitColumns()

        ' Obtener diccionario de traducciones y configuración de moneda
        Dim translations = ColumnTranslations
        Dim currencyISO4217 As String = IndigoSessionValues.CurrencyISO4217

        Dim firstVisibleColumn As GridColumn = Nothing

        ' Configurar cada columna según su tipo
        For Each column As GridColumn In gridView.Columns

            ' Ocultar columnas de identificación interna
            If column.FieldName = "ContractLiquidationId" OrElse column.FieldName = "EmployeeId" Then
                column.Visible = False
            End If

            ' Identificar la primera columna visible para el conteo de registros
            If firstVisibleColumn Is Nothing AndAlso column.Visible Then
                firstVisibleColumn = column
            End If

            ' Identificar si es columna dinámica (conceptos que no están en el diccionario)
            Dim isDynamicColumn As Boolean = Not translations.ContainsKey(column.FieldName)

            ' Traducir solo columnas fijas
            If Not isDynamicColumn AndAlso translations.ContainsKey(column.FieldName) Then
                column.Caption = translations(column.FieldName)
            End If

            ' Establecer ancho mínimo y máximo para todas las columnas
            If column.Visible AndAlso column.Width > 0 Then
                column.MinWidth = CInt(column.Width * 0.8)
                column.MaxWidth = CInt(column.Width * 1.5)
            End If

            ' Formato para columnas numéricas dinámicas (conceptos)
            If isDynamicColumn AndAlso currencyISO4217 IsNot Nothing Then
                Window.Utils.FormatGrid(column, currencyISO4217, 0)
                column.Summary.Clear()
                column.Summary.Add(DevExpress.Data.SummaryItemType.Sum, column.FieldName, "{0:c2}")
            End If

            ' Formato para columnas fijas numéricas (montos y totales)
            If Not isDynamicColumn AndAlso currencyISO4217 IsNot Nothing AndAlso (
                column.FieldName = "BasicSalary" OrElse
                column.FieldName = "TotalAccrued" OrElse
                column.FieldName = "TotalDeducted" OrElse
                column.FieldName = "NetPay") Then
                Window.Utils.FormatGrid(column, currencyISO4217, 0)
                column.Summary.Clear()
                column.Summary.Add(DevExpress.Data.SummaryItemType.Sum, column.FieldName, "{0:c2}")
            End If

            ' Deshabilitar edición en todas las columnas
            column.OptionsColumn.AllowEdit = False
        Next

        ' Agregar conteo total en la primera columna visible
        If firstVisibleColumn IsNot Nothing Then
            firstVisibleColumn.Summary.Clear()
            firstVisibleColumn.Summary.Add(DevExpress.Data.SummaryItemType.Count, firstVisibleColumn.FieldName, "N° Registros:{0}")
        End If

        ' Habilitar opciones de personalización del grid
        gridView.OptionsCustomization.AllowColumnResizing = True
        gridView.OptionsCustomization.AllowColumnMoving = True
    End Sub

#End Region

#Region "UI Methods"

    ''' <summary>
    ''' Muestra el panel inicial del formulario con los controles de filtro
    ''' </summary>
    Private Sub ShowPannelBase()

        INDPanelControlBase.Visible = False
        INDPanelContractLiquidation.Visible = False
        INDPanelContractLiquidation.Dock = DockStyle.None
        INDPanelControlBase.Dock = DockStyle.Fill
        INDPanelControlBase.BringToFront()
        INDPanelControlBase.Visible = True
    End Sub

    ''' <summary>
    ''' Muestra el panel con el reporte en la rejilla
    ''' </summary>
    Private Sub ShowPannelReport()
        INDPanelControlBase.Visible = False
        INDPanelControlBase.Visible = False
        INDPanelContractLiquidation.Visible = False
        INDPanelContractLiquidation.Dock = DockStyle.Fill
        INDPanelContractLiquidation.BringToFront()
        INDPanelContractLiquidation.Visible = True
    End Sub

    ''' <summary>
    ''' Limpia los controles y restablece el formulario a su estado inicial
    ''' </summary>
    Public Sub Deshacer()
        ShowPannelBase()
        Me.BarraBotones.Minimizar(True)
    End Sub

#End Region

#Region "Handlers - Click"
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub
#End Region

#Region "Handlers - QueryPopUp"
    ''' <summary>
    ''' Se ejecuta cuando se abre el popup del control INDSleEmployee
    ''' Carga el data source si no está cargado
    ''' </summary>
    ''' <param name="sender">Objeto que dispara el evento</param>
    ''' <param name="e">Argumentos del evento con opción de cancelar</param>
    Private Sub INDSleEmployee_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleEmployee.QueryPopUp
        If INDSleEmployee.Datasource Is Nothing Then
            LoadXpoEmployee()
        End If
    End Sub

    Private Sub INDSbGenerarReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerarReport.Click
        If Me.ValidateControlsReports Then
            INDgcContractLiquidation.DataSource = Nothing
            LoadDataLiquidation()
        End If
    End Sub
#End Region


End Class
