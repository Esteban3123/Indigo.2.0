
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

#End Region

Public Class FrmReportControlLiquidation

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de MCommon
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"

    ''' <summary>
    ''' Fuente de datos para los grupos de nómina (inicio)
    ''' </summary>
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource

    ''' <summary>
    ''' Fuente de datos para los empleados
    ''' </summary>
    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesión
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Propiedad que se usa para cargar la lista de estados de registro de liquidación
    ''' </summary>
    Private _FillingStatus As List(Of Tuple(Of String, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of String, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of String, String))
                _FillingStatus.Add(New Tuple(Of String, String)("C", "Confirmados"))
                _FillingStatus.Add(New Tuple(Of String, String)("", "Sin Confirmar"))
                _FillingStatus.Add(New Tuple(Of String, String)("S", "Saldo Inicial"))
                _FillingStatus.Add(New Tuple(Of String, String)("T", "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property

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

    ''' <summary>
    ''' Diccionario de traducciones para los nombres de columnas fijas del grid
    ''' </summary>
    ''' <returns>Diccionario con las traducciones de inglés a español</returns>
    Private ReadOnly Property ColumnTranslations As Dictionary(Of String, String)
        Get
            Dim translations As New Dictionary(Of String, String)

            ' Información del Empleado
            translations("InternalCode") = "Código Interno"
            translations("DocumentNumber") = "Número de Documento"
            translations("EmployeeName") = "Nombre Empleado"
            translations("Gender") = "Género"
            translations("Email") = "Correo Electrónico"
            translations("MobilePhone") = "Telefono Movil"
            translations("LandlinePhone") = "Telefono fijo"
            translations("BirthDate") = "Fecha de Nacimiento"

            ' Información Laboral
            translations("PositionCode") = "Código Cargo"
            translations("PositionName") = "Nombre Cargo"
            translations("FunctionalUnitCode") = "Código Unidad Funcional"
            translations("FunctionalUnitName") = "Nombre Unidad Funcional"
            translations("CostCenterCode") = "Código Centro de Costo"
            translations("CostCenterName") = "Nombre Centro de Costo"
            translations("GroupCode") = "Código Grupo"
            translations("GroupName") = "Nombre Grupo"
            translations("HiringDate") = "Fecha de Contratación"
            translations("BasicSalary") = "Salario Básico"

            ' Seguridad Social
            translations("HealthFundCode") = "Código EPS"
            translations("HealthFundName") = "Nombre EPS"
            translations("PensionFundCode") = "Código Fondo de Pensión"
            translations("PensionFundName") = "Nombre Fondo de Pensión"

            ' Información Bancaria
            translations("BankCode") = "Código Banco"
            translations("BankName") = "Nombre Banco"
            translations("BankAccountNumber") = "Número de Cuenta"
            translations("ProfessionalRiskPercentage") = "Porcentaje Riesgo Profesional"

            ' Información de Liquidación
            translations("LiquidationDate") = "Fecha Liquidación"
            translations("Year") = "Año Liquidación"
            translations("Month") = "Mes Liquidación"
            translations("Day") = "Dia Liquidación"

            ' Días Trabajados
            translations("PayrollDays") = "Días Nómina"
            translations("WorkedDays") = "Días Trabajados"
            translations("VacationDays") = "Días Vacaciones"
            translations("DisabilityDays") = "Días Incapacidad"
            translations("UnpaidLeaveDays") = "Días Permiso"
            translations("PaidLeaveDays") = "Días Permiso disfrutados"

            ' Totales
            translations("TotalAccrued") = "Total Devengado"
            translations("TotalDeducted") = "Total Deducido"
            translations("NetPay") = "Neto a Pagar"
            translations("IBCHealth") = "IBC Salud"

            Return translations
        End Get
    End Property

#End Region


#Region "Load"

    ''' <summary>
    ''' Se ejecuta cuando se carga el formulario
    ''' Inicializa los controles y configura el estado inicial
    ''' </summary>
    ''' <param name="sender">Objeto que dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Private Sub FrmReportControlLiquidation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Cargar GridLookUpEdit con los estados de registro
        Me.INDGleStatus.Properties.DataSource = FillingStatus
        ' Establecer valor por defecto a "Todos"
        Me.INDGleStatus.EditValue = "T"
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
        ProoftCloseXpoGroup = Nothing
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

#End Region


#Region "Data Methods"

    ''' <summary>
    ''' Carga los parámetros de nómina desde XPO
    ''' </summary>
    ''' <returns>Objeto PayrollSettingsXpo con los parámetros configurados</returns>
    Private Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Carga el data source del control INDSleGroupStart con los grupos de nómina
    ''' </summary>
    Private Sub LoadXpoGroupStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupStart.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' Carga el data source del control INDSleGroupEnd con los grupos de nómina
    ''' </summary>
    Private Sub LoadXpoGroupEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
            INDSleGroupEnd.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

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

            ' Obtener valores de los controles de fecha
            Dim startDate As Date = INDDateStart.EditValue
            Dim endDate As Date = INDDateEnd.EditValue
            Dim employeeId As Integer? = INDSleEmployee.EditValue

            ' Obtener valores de grupos (opcionales)
            Dim groupInitial As Integer? = Nothing
            Dim groupFinal As Integer? = Nothing
            If INDSleGroupStart.EditValue IsNot Nothing Then
                groupInitial = INDSleGroupStart.EditValue
            End If
            If INDSleGroupEnd.EditValue IsNot Nothing Then
                groupFinal = INDSleGroupEnd.EditValue
            End If

            ' Obtener valores de sucursales (opcionales)
            Dim branchOfficeInitial As Integer? = Nothing
            Dim branchOfficeFinal As Integer? = Nothing
            If INDsleSucursalStart.EditValue IsNot Nothing Then
                branchOfficeInitial = INDsleSucursalStart.EditValue
            End If
            If INDsleSucursalEnd.EditValue IsNot Nothing Then
                branchOfficeFinal = INDsleSucursalEnd.EditValue
            End If

            ' Obtener estado de registro (opcional)
            ' Si es "T" (Todos), se envía Nothing para no filtrar
            Dim registerStatus As Char? = Nothing
            If INDGleStatus.EditValue IsNot Nothing AndAlso Not INDGleStatus.EditValue.Equals("T") Then
                Dim statusValue As String = INDGleStatus.EditValue.ToString()
                If Not String.IsNullOrEmpty(statusValue) Then
                    registerStatus = statusValue(0)
                End If
            End If

            ' Obtener datos del reporte desde el servicio
            Using model As New MPayrollLiquidation
                Dim dtLiquidationDetailReport As DataTable = Await model.GetLiquidationDetailReportAsync(
                    startDate,
                    endDate,
                    employeeId,
                    groupInitial,
                    groupFinal,
                    branchOfficeInitial,
                    branchOfficeFinal,
                    registerStatus)

                If dtLiquidationDetailReport IsNot Nothing AndAlso dtLiquidationDetailReport.Rows.Count > 0 Then
                    INDgcControlLiquidation.DataSource = dtLiquidationDetailReport
                    ConfigureGridColumns()
                    IndigoGridControl1.SetExportButton(INDgcControlLiquidation, True)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros para los parámetros seleccionados"
                End If
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = "Error al cargar la informacion: " & ex.Message
        Finally
            AsyncLoader(False)
        End Try
    End Sub

#End Region

#Region "Methods GridControl"

    ''' <summary>
    ''' Configura las columnas del grid para mejorar la visualización y exportación a Excel
    ''' Aplica traducciones, formatos de moneda, sumatorias y ajustes de ancho
    ''' </summary>
    Private Sub ConfigureGridColumns()
        Dim gridView As GridView = TryCast(INDgcControlLiquidation.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

        If gridView Is Nothing Then Return

        ' Configuración básica del grid
        gridView.OptionsView.ColumnAutoWidth = False
        gridView.OptionsView.ShowFooter = True
        gridView.OptionsMenu.EnableColumnMenu = True
        gridView.OptionsMenu.EnableFooterMenu = True

        ' Ajustar automáticamente el ancho de las columnas según el contenido
        gridView.BestFitColumns()

        ' Obtener diccionario de traducciones y configuración de moneda
        Dim translations = ColumnTranslations
        ' Cargar moneda desde PayrollSettings en lugar de la sesión
        Dim payrollSettings = LoadPayrollSettings()
        Dim currencyISO4217 As String = If(payrollSettings?.CurrencyId?.Abbreviation, IndigoSessionValues.CurrencyISO4217)

        Dim firstVisibleColumn As GridColumn = Nothing

        ' Configurar cada columna según su tipo
        For Each column As GridColumn In gridView.Columns

            ' Ocultar columnas de identificación interna
            If column.FieldName = "LiquidationId" OrElse column.FieldName = "EmployeeId" Then
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
            ' Excluir columnas de cantidad que no deben tener formato de moneda
            Dim isQuantityColumn As Boolean = column.FieldName.ToUpper().Contains("CANTIDAD")
            Dim isPhoneColumn As Boolean = column.FieldName.Equals("MobilePhone", StringComparison.OrdinalIgnoreCase) OrElse
                               column.FieldName.Equals("LandlinePhone", StringComparison.OrdinalIgnoreCase)


            If isDynamicColumn AndAlso currencyISO4217 IsNot Nothing AndAlso Not isQuantityColumn Then
                ' Window.Utils.FormatGrid ya formatea la columna Y el summary con la cultura correcta
                Window.Utils.FormatGrid(column, currencyISO4217, 2)
                If column.Summary.Count = 0 Then
                    column.Summary.Add(DevExpress.Data.SummaryItemType.Sum, column.FieldName, "{0}")
                End If
            ElseIf isDynamicColumn AndAlso isQuantityColumn Then
                ' Formato numérico sin símbolo de moneda para columnas de cantidad
                column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                column.DisplayFormat.FormatString = "n2"
                column.Summary.Clear()
                column.Summary.Add(DevExpress.Data.SummaryItemType.Sum, column.FieldName, "{0:n2}")
            ElseIf isPhoneColumn Then
                column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                column.Summary.Clear()
            End If

            ' Formato para columnas fijas numéricas (montos y totales)
            If Not isDynamicColumn AndAlso currencyISO4217 IsNot Nothing AndAlso (
                column.FieldName = "BasicSalary" OrElse
                column.FieldName = "TotalAccrued" OrElse
                column.FieldName = "TotalDeducted" OrElse
                column.FieldName = "NetPay" OrElse
                column.FieldName = "IBCHealth") Then
                ' Window.Utils.FormatGrid ya formatea la columna Y el summary con la cultura correcta
                Window.Utils.FormatGrid(column, currencyISO4217, 2)
                ''column.Summary.Clear()
                If column.Summary.Count = 0 Then
                    column.Summary.Add(DevExpress.Data.SummaryItemType.Sum, column.FieldName, "{0:c2}")
                End If
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

#Region "Excel Export Methods"

    ''' <summary>
    ''' Crea y exporta el reporte de nómina a Excel
    ''' </summary>
    Private Sub CreatePayrollReport()
        Dim sfd As New System.Windows.Forms.SaveFileDialog()
        Dim model As New MCommon(Me.Tag)

        ' Obtener valores de los controles
        Dim startDate As Date = INDDateStart.EditValue
        Dim endDate As Date = INDDateEnd.EditValue
        Dim registerStatus As String = IIf(INDGleStatus.EditValue.Equals("T"), Nothing, INDGleStatus.EditValue)
        Dim cédula As String = INDSleEmployee.EditValue
        Dim startGroupCode As String = INDSleGroupStart.EditValue
        Dim endGroupCode As String = INDSleGroupEnd.EditValue

        ' Obtener datos del reporte
        Dim dtPayrollReport As DataTable = model.GetPayrollReport(startDate, endDate, registerStatus, cédula, startGroupCode, endGroupCode)

        ' Configurar diálogo de guardado
        sfd.Filter = "Archivos de Excel |*.xlsx"
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim wbook As New Workbook
            CreateExcelFile(dtPayrollReport, wbook)
            wbook.SaveDocument(sfd.FileName, DocumentFormat.Xlsx)
            Mensaje(EeventViewerImages.Informacion) = "Archivo creado correctamente"
        End If
    End Sub

    ''' <summary>
    ''' Crea el archivo Excel con los datos del reporte
    ''' </summary>
    ''' <param name="pDT">DataTable con los datos a exportar</param>
    ''' <param name="pWbook">Workbook de DevExpress donde se creará el archivo</param>
    Private Sub CreateExcelFile(pDT As DataTable, pWbook As Workbook)
        CreateExcelColumnHeader(pDT, pWbook)
        FillExcelData(pDT, pWbook)
    End Sub

    ''' <summary>
    ''' Llena los datos en el archivo Excel
    ''' </summary>
    ''' <param name="pDT">DataTable con los datos a exportar</param>
    ''' <param name="pWbook">Workbook de DevExpress donde se escribirán los datos</param>
    Private Sub FillExcelData(pDT As DataTable, pWbook As Workbook)
        For i As Integer = 0 To pDT.Rows.Count - 1
            Dim dRow As DataRow = pDT.Rows(i)
            For j As Integer = 0 To pDT.Columns.Count - 1
                Dim spValue As Object = Nothing
                Select Case dRow(j).GetType().Name
                    Case "DBNull"
                        pWbook.Worksheets(0).Rows(i + 1)(j).Value = CType(0, Decimal)
                    Case "Decimal"
                        pWbook.Worksheets(0).Rows(i + 1)(j).Value = CType(dRow(j), Decimal)
                    Case Else
                        pWbook.Worksheets(0).Rows(i + 1)(j).Value = CType(dRow(j), String)
                End Select
            Next
        Next
    End Sub

    ''' <summary>
    ''' Crea los encabezados de las columnas en el archivo Excel
    ''' </summary>
    ''' <param name="pDT">DataTable con los datos a exportar</param>
    ''' <param name="pWbook">Workbook de DevExpress donde se escribirán los encabezados</param>
    Private Sub CreateExcelColumnHeader(pDT As DataTable, pWbook As Workbook)
        For i As Integer = 0 To pDT.Columns.Count - 1
            pWbook.Worksheets(0).Rows(0)(i).Value = pDT.Columns(i).ColumnName
        Next
    End Sub

#End Region

#Region "UI Methods"

    ''' <summary>
    ''' Muestra el panel inicial del formulario con los controles de filtro
    ''' </summary>
    Private Sub ShowPannelBase()
        INDPcBase.Visible = False
        INDPanelControlBase.Visible = False
        INDPcMainControlLiquidation.Visible = False
        INDPcMainControlLiquidation.Dock = DockStyle.None
        INDPcBase.Dock = DockStyle.Fill
        INDPcBase.BringToFront()
        INDPcBase.Visible = True
    End Sub

    ''' <summary>
    ''' Muestra el panel con el reporte en la rejilla
    ''' </summary>
    Private Sub ShowPannelReport()
        INDPcBase.Visible = False
        INDPanelControlBase.Visible = False
        INDPcMainControlLiquidation.Visible = False
        INDPcMainControlLiquidation.Dock = DockStyle.Fill
        INDPcMainControlLiquidation.BringToFront()
        INDPcMainControlLiquidation.Visible = True
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

    ''' <summary>
    ''' Se ejecuta cuando se hace clic en el botón de generar reporte
    ''' </summary>
    ''' <param name="sender">Objeto que dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            INDgcControlLiquidation.DataSource = Nothing
            LoadDataLiquidation()
            ShowPannelReport()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta cuando se hace clic en el botón de exportar a Excel
    ''' </summary>
    ''' <param name="sender">Objeto que dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Private Function INDsbExcelExport_ClickAsync(sender As Object, e As EventArgs) As Task Handles INDsbExcelExport.Click
        If INDDateStart.EditValue IsNot Nothing And INDDateEnd.EditValue IsNot Nothing Then
            CreatePayrollReport()
        Else
            Mensaje(EeventViewerImages.MensajeError) = "Debe seleccionar una fecha inicial y fecha final"
        End If
    End Function

    ''' <summary>
    ''' Se ejecuta cuando se hace clic en el botón deshacer de la barra de herramientas
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Se ejecuta cuando se hace clic en el botón de retroceso
    ''' </summary>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
        Me.INDDateStart.Focus()
    End Sub

#End Region

#Region "Handlers - QueryPopUp"

    ''' <summary>
    ''' Se ejecuta cuando se abre el popup del control INDSleGroupStart
    ''' Carga el data source si no está cargado
    ''' </summary>
    ''' <param name="sender">Objeto que dispara el evento</param>
    ''' <param name="e">Argumentos del evento con opción de cancelar</param>
    Private Sub INDSleGroupStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleGroupStart.QueryPopUp
        If INDSleGroupStart.Datasource Is Nothing Then
            LoadXpoGroupStart()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta cuando se abre el popup del control INDSleGroupEnd
    ''' Carga el data source si no está cargado
    ''' </summary>
    ''' <param name="sender">Objeto que dispara el evento</param>
    ''' <param name="e">Argumentos del evento con opción de cancelar</param>
    Private Sub INDSleGroupEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleGroupEnd.QueryPopUp
        If INDSleGroupEnd.Datasource Is Nothing Then
            LoadXpoGroupEnd()
        End If
    End Sub

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

    ''' <summary>
    ''' Se ejecuta cuando se abre el popup del control INDsleSucursalStart
    ''' Carga el data source con las sucursales si no está cargado
    ''' </summary>
    ''' <param name="sender">Objeto que dispara el evento</param>
    ''' <param name="e">Argumentos del evento con opción de cancelar</param>
    Private Sub INDsleSucursalStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleSucursalStart.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalStart.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub

    ''' <summary>
    ''' Se ejecuta cuando se abre el popup del control INDsleSucursalEnd
    ''' Carga el data source con las sucursales si no está cargado
    ''' </summary>
    ''' <param name="sender">Objeto que dispara el evento</param>
    ''' <param name="e">Argumentos del evento con opción de cancelar</param>
    Private Sub INDsleSucursalEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleSucursalEnd.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoBranchOffice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBranchOffice)
            INDsleSucursalEnd.Properties.DataSource = ProoftCloseXpoBranchOffice
        End Using
    End Sub

#End Region



End Class
