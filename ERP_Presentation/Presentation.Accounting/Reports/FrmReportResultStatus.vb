#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Base.Utils
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportResultStatus

#Region "Fields"

    ''' <summary>
    ''' Diccionario que almacena diferentes criterios 
    ''' </summary>
    Private criterias As Dictionary(Of String, String)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad que almacena la última fecha de cierre
    ''' </summary>
    ''' <returns></returns>
    Public Property INDLastClosingDate As Date?

    ''' <summary>
    ''' Toma el valor seleccionado en el control INDsleExpressedValues y lo mapea a una enumeración que representa un nivel de redondeo específico
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ExpressedValue As RoundLevel
        Get
            Dim _expressedValuesToSend As RoundLevel
            Select Case INDsleExpressedValues.EditValue
                Case 1
                    _expressedValuesToSend = RoundLevel.Unit
                Case 2
                    _expressedValuesToSend = RoundLevel.Thousands
                Case 3
                    _expressedValuesToSend = RoundLevel.Millions
            End Select
            Return _expressedValuesToSend
        End Get
    End Property

#End Region

#Region "Datasources"

    ''' <summary>
    '''  Permite acceder a una lista de opciones de visualización representadas por tuplas que contienen un número y una descripción
    ''' </summary>
    Private _FillingVisualization As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingVisualization As List(Of Tuple(Of Integer, String))
        Get
            If _FillingVisualization Is Nothing Then
                _FillingVisualization = New List(Of Tuple(Of Integer, String))
                _FillingVisualization.Add(New Tuple(Of Integer, String)(1, "Estandar"))
                _FillingVisualization.Add(New Tuple(Of Integer, String)(2, "Trimestral"))
                _FillingVisualization.Add(New Tuple(Of Integer, String)(3, "Estandar - Presentación Bancaria"))
                _FillingVisualization.Add(New Tuple(Of Integer, String)(4, "Anual"))
                _FillingVisualization.Add(New Tuple(Of Integer, String)(5, "Comparativo"))
            End If
            Return _FillingVisualization
        End Get
    End Property

    ''' <summary>
    ''' Almacena una lista de tuplas que representan diferentes periodos de tiempo (Activo, Cerrado)
    ''' </summary>
    Private _FillingPeriod As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingPeriod As List(Of Tuple(Of Integer, String))
        Get
            If _FillingPeriod Is Nothing Then
                _FillingPeriod = New List(Of Tuple(Of Integer, String))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(1, "Periodo Contable Activo"))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(2, "Periodo Contable Cerrado"))
            End If
            Return _FillingPeriod
        End Get
    End Property

    ''' <summary>
    ''' Permite acceder a una lista de tuplas que representan diferentes opciones de formato
    ''' </summary>
    Private _FillingFormatAnnex As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingFormatAnnex As List(Of Tuple(Of Integer, String))
        Get
            If _FillingFormatAnnex Is Nothing Then
                _FillingFormatAnnex = New List(Of Tuple(Of Integer, String))
                _FillingFormatAnnex.Add(New Tuple(Of Integer, String)(1, "Formato CGN96-001 Anexo 3"))
                _FillingFormatAnnex.Add(New Tuple(Of Integer, String)(2, "Formato CGN96-001 Anexo 4"))
            End If
            Return _FillingPeriod
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que almacena el origen de datos para libros contables
    ''' </summary>
    ''' <returns></returns>
    Public Property bookXpcollection As XPCollection

    ''' <summary>
    ''' Permite acceder a una lista de niveles de cuenta predefinidos (Grupo, Cuenta, Subcuenta, Auxiliar) 
    ''' </summary>
    Private _FillingNivel As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingNivel As List(Of Tuple(Of Integer, String))
        Get
            If _FillingNivel Is Nothing Then
                _FillingNivel = New List(Of Tuple(Of Integer, String))
                _FillingNivel.Add(New Tuple(Of Integer, String)(2, "Grupo"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(3, "Cuenta"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(4, "Subcuenta"))
                _FillingNivel.Add(New Tuple(Of Integer, String)(5, "Auxiliar"))
            End If
            Return _FillingNivel
        End Get
    End Property

    ''' <summary>
    ''' Permite acceder a una lista de valores expresados en diferentes cantidades de moneda funcional
    ''' </summary>
    Private _ExpressedValues As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property ExpressedValues As List(Of Tuple(Of Integer, String))
        Get
            If _ExpressedValues Is Nothing Then
                _ExpressedValues = New List(Of Tuple(Of Integer, String))
                _ExpressedValues.Add(New Tuple(Of Integer, String)(1, "En Moneda Funcional"))
                _ExpressedValues.Add(New Tuple(Of Integer, String)(2, "En Miles Moneda Funcional"))
                _ExpressedValues.Add(New Tuple(Of Integer, String)(3, "En Millones Moneda Funcional"))
            End If
            Return _ExpressedValues
        End Get
    End Property

#End Region
#Region "Messages"
    ''' <summary>
    ''' Propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
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


#Region "BarraBotones"

    ''' <summary>
    ''' Se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método para realizar las validaciones del formulario
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        Dim DateStart As Date = "01/" & INDCdnDateStart.GetMonth & "/" & INDCdnDateStart.GetYear
        Dim DateEnd As Date = "01/" & INDCdnDateEnd.GetMonth & "/" & INDCdnDateEnd.GetYear
        If Me.INDGleVisualization.EditValue <> 5 Then
            If DateStart > DateEnd Then
                errors.AppendLine(String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting")))
                Me.INDCdnDateStart.Focus()
            End If
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("Visualization", INDGleVisualization.EditValue)
        criterias.Add("Period", INDGlePeriod.EditValue)
        criterias.Add("FormatAnnex", INDGleFormatAnnex.EditValue)

        criterias.Add("InitialRangeYear", INDCdnDateStart.GetYear)
        criterias.Add("InitialRangeMonthStart", INDCdnDateStart.GetMonth)
        criterias.Add("InitialRangeMonthEnd", INDCdnDateStart2.GetMonth)
        criterias.Add("FinalRangeYear", INDCdnDateEnd.GetYear)
        criterias.Add("FinalRangeMonthStart", INDCdnDateEnd.GetMonth)
        criterias.Add("FinalRangeMonthEnd", INDCdnDateEnd2.GetMonth)

        criterias.Add("Title", INDTxtTitle.EditValue)
        criterias.Add("SubTitle", INDTxtSubTitle.EditValue)
        criterias.Add("ExpressedValues", ExpressedValue)
        criterias.Add("LegalBookId", INDsleBook.EditValue)
        criterias.Add("LegalBookName", INDsleBook.Text)
        criterias.Add("LevelAccount", INDGleLevelAccount.EditValue)
        criterias.Add("IncludedAccountNumber", INDsleIncludedAccountNumber.EditValue)
        criterias.Add("AccountsZero", INDSleAccountsZero.EditValue)
        criterias.Add("DetailingThird", INDsleDetailingThird.EditValue)
        criterias.Add("HandlesCostCenter", INDSleAccountsCostCenter.EditValue)
        criterias.Add("CostCenters", _selectorCostCenter.GetKeys())
        criterias.Add("BranchOffices", _selectorBranchOffice.GetKeys())

        Return True
    End Function

    ''' <summary>
    ''' Método para obtener el datasource de libros contables
    ''' </summary>
    Private Sub LoadXpoBook()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True}
            bookXpcollection = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBookByStatusXpCollection, filter)
            INDsleBook.Properties.DataSource = bookXpcollection
        End Using
    End Sub

    ''' <summary>
    ''' Método que establece el libro oficial en la colección de libros contables
    ''' </summary>
    Sub SetOfficialBook()
        If bookXpcollection IsNot Nothing AndAlso bookXpcollection.Count > 0 Then
            Dim item = (From l In bookXpcollection Where l.OfficialBook = True Select l).FirstOrDefault
            If item IsNot Nothing Then
                INDsleBook.EditValue = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Genera archivo excel
    ''' </summary>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleVisualization.EditValue & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcel.DataSource = Nothing
        Me.INDGcExportExcel.RefreshDataSource()
    End Sub

#Region "ToExcel"

    ''' <summary>
    ''' Carga datos desde una fuente de datos externa, los transforma y los organiza en un DataTable para finalmente establecer ese DataTable como fuente de datos
    ''' </summary>
    Private Sub chargueDataSourceStandard()
        Dim ds As DataSet = IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetReportResulStatus(criterias, Me.indigo)
        If ds IsNot Nothing Then
            Dim dtReportResulStatus = ds.Tables("ReportResulStatus")
            If dtReportResulStatus IsNot Nothing AndAlso dtReportResulStatus.Rows.Count > 0 Then
                Dim dt As New DataTable
                dt.Columns.Add("Código")
                dt.Columns.Add("Nombre Cuenta")
                dt.Columns.Add("Código Sucursal")
                dt.Columns.Add("Nombre Sucursal")
                dt.Columns.Add("Código Centro de Costo")
                dt.Columns.Add("Nombre Centro de Costo")

                dt.Columns.Add("Moneda")
                dt.Columns.Add("Saldo Inicial", GetType(Decimal))
                dt.Columns.Add("Total Auxiliar", GetType(Decimal))
                dt.Columns.Add("Total Subcuenta", GetType(Decimal))
                dt.Columns.Add("Total Cuenta", GetType(Decimal))
                dt.Columns.Add("Total Grupo", GetType(Decimal))
                dt.Columns.Add("Total Clase", GetType(Decimal))
                dt.Columns.Add("Saldo Final", GetType(Decimal))

                For Each item In dtReportResulStatus.Rows
                    Dim row As DataRow = dt.NewRow()
                    row.Item("Código") = item("mainAccountCode")
                    row.Item("Nombre Cuenta") = item("mainAccountName")
                    row.Item("Código Sucursal") = item("branchOfficeCode")
                    row.Item("Nombre Sucursal") = item("branchOfficeName")
                    row.Item("Código Centro de Costo") = item("costCenterCode")
                    row.Item("Nombre Centro de Costo") = item("costCenterName")

                    row.Item("Moneda") = item("LegalBookCurrency") + "(" + item("LegalBookCurrencyAbbreviation") + ")"
                    row.Item("Saldo Inicial") = item("valueCreditInitial") - item("valueDebitInitial")
                    row.Item("Total Auxiliar") = If(item("mainAccountLevel") >= 5, item("valueCreditMovement") - item("valueDebitMovement"), 0)
                    row.Item("Total Subcuenta") = If(item("mainAccountLevel") = 4, item("valueCreditMovement") - item("valueDebitMovement"), 0)
                    row.Item("Total Cuenta") = If(item("mainAccountLevel") = 3, item("valueCreditMovement") - item("valueDebitMovement"), 0)
                    row.Item("Total Grupo") = If(item("mainAccountLevel") = 2, item("valueCreditMovement") - item("valueDebitMovement"), 0)
                    row.Item("Total Clase") = If(item("mainAccountLevel") = 1, item("valueCreditMovement") - item("valueDebitMovement"), 0)
                    row.Item("Saldo Final") = item("valueCreditInitial") - item("valueDebitInitial") + item("valueCreditMovement") - item("valueDebitMovement")
                    dt.Rows.Add(row)
                Next

                INDGcExportExcel.DataSource = dt
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se crea una estructura de datos con columnas que representan meses específicos y asigna los valores de las columnas relevantes a esas fechas
    ''' </summary>
    Private Sub chargueDataSourceQuaterly()
        Dim ds As DataSet = IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetReportResulStatus(criterias, Me.indigo)
        If ds IsNot Nothing Then
            Dim dtReportResulStatus = ds.Tables("ReportResulStatus")
            If dtReportResulStatus IsNot Nothing AndAlso dtReportResulStatus.Rows.Count > 0 Then
                Dim monthStart = String.Format("{0}/{1}", INDCdnDateStart.GetMonth, INDCdnDateStart.GetYear)
                Dim monthIntermediate = String.Format("{0}/{1}", INDCdnDateStart.GetMonth + 1, INDCdnDateStart.GetYear)
                Dim monthEnd = String.Format("{0}/{1}", INDCdnDateEnd.GetMonth, INDCdnDateStart.GetYear)


                Dim dt As New DataTable
                dt.Columns.Add("Código")
                dt.Columns.Add("Nombre Cuenta")
                dt.Columns.Add("Código Sucursal")
                dt.Columns.Add("Nombre Sucursal")
                dt.Columns.Add("Código Centro de Costo")
                dt.Columns.Add("Nombre Centro de Costo")

                dt.Columns.Add(monthStart, GetType(Decimal))
                dt.Columns.Add(monthIntermediate, GetType(Decimal))
                dt.Columns.Add(monthEnd, GetType(Decimal))

                For Each item In dtReportResulStatus.Rows
                    If item("movementMonthStart") <> 0 OrElse item("movementMonthIntermediate") <> 0 OrElse item("movementMonthEnd") <> 0 Then
                        Dim row As DataRow = dt.NewRow()
                        row.Item("Código") = item("mainAccountCode")
                        row.Item("Nombre Cuenta") = item("mainAccountName")
                        row.Item("Código Sucursal") = item("branchOfficeCode")
                        row.Item("Nombre Sucursal") = item("branchOfficeName")
                        row.Item("Código Centro de Costo") = item("costCenterCode")
                        row.Item("Nombre Centro de Costo") = item("costCenterName")

                        row.Item(monthStart) = item("movementMonthStart")
                        row.Item(monthIntermediate) = item("movementMonthIntermediate")
                        row.Item(monthEnd) = item("movementMonthEnd")
                        dt.Rows.Add(row)
                    End If
                Next

                INDGcExportExcel.DataSource = dt
            End If
        End If
    End Sub

    ''' <summary>
    ''' Crea una estructura de datos con columnas que representan fechas de comparación
    ''' </summary>
    Private Sub chargueDataSourceComparative()
        Dim ds As DataSet = IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetReportResulStatusComparative(criterias, Me.indigo)
        If ds IsNot Nothing Then
            Dim dtReportResulStatus = ds.Tables("ReportResulStatusComparative")
            If dtReportResulStatus IsNot Nothing AndAlso dtReportResulStatus.Rows.Count > 0 Then
                Dim initialRangefechaIni As Date = New Date(INDCdnDateStart.GetYear, INDCdnDateStart.GetMonth, 1)
                Dim initialRangefechafin As Date = New Date(INDCdnDateStart.GetYear, INDCdnDateStart2.GetMonth, 1)
                Dim finalRangefechaIni As Date = New Date(INDCdnDateEnd.GetYear, INDCdnDateEnd.GetMonth, 1)
                Dim finalRangefechafin As Date = New Date(INDCdnDateEnd.GetYear, INDCdnDateEnd2.GetMonth, 1)

                Dim INDDateStart As String = initialRangefechaIni.ToString("MMMM Del yyyy") & " a " & initialRangefechafin.ToString("MMMM Del yyyy")
                Dim INDDateEnd As String = finalRangefechaIni.ToString("MMMM Del yyyy") & " a " & finalRangefechafin.ToString("MMMM Del yyyy")

                Dim dt As New DataTable
                dt.Columns.Add("Código")
                dt.Columns.Add("Nombre Cuenta")
                dt.Columns.Add("Código Sucursal")
                dt.Columns.Add("Nombre Sucursal")
                dt.Columns.Add("Código Centro de Costo")
                dt.Columns.Add("Nombre Centro de Costo")

                dt.Columns.Add(INDDateStart, GetType(Decimal))
                dt.Columns.Add(INDDateEnd, GetType(Decimal))
                dt.Columns.Add("Var. Ab.", GetType(Decimal))
                dt.Columns.Add("Var. Rel.", GetType(Decimal))

                For Each item In dtReportResulStatus.Rows
                    Dim row As DataRow = dt.NewRow()
                    row.Item("Código") = item("mainAccountCode")
                    row.Item("Nombre Cuenta") = item("mainAccountName")
                    row.Item("Código Sucursal") = item("branchOfficeCode")
                    row.Item("Nombre Sucursal") = item("branchOfficeName")
                    row.Item("Código Centro de Costo") = item("costCenterCode")
                    row.Item("Nombre Centro de Costo") = item("costCenterName")

                    row.Item(INDDateStart) = item("newBalanceInitial")
                    row.Item(INDDateEnd) = item("newBalanceFinal")
                    row.Item("Var. Ab.") = item("newBalanceInitial") - item("newBalanceFinal")
                    row.Item("Var. Rel.") = If(item("newBalanceFinal") = 0, 0, (item("newBalanceInitial") - item("newBalanceFinal")) / item("newBalanceFinal"))
                    dt.Rows.Add(row)
                Next

                INDGcExportExcel.DataSource = dt
            End If
        End If
    End Sub

#End Region

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Carga y configura varios controles y datos al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReportResultStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'obtener el ultimo cierre
        Using msearch As New MBusqueda
            INDLastClosingDate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetLastClosingDate)
            If INDLastClosingDate Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateExistencePeriodClosingReport", "Commons"))
                INDLastClosingDate = Date.Now
            Else
                INDLastClosingDate = DateAdd(DateInterval.Month, 1, INDLastClosingDate.Value)
            End If
        End Using

        'Cargar GridLookUpEdit
        Me.INDGleVisualization.Properties.DataSource = Me.FillingVisualization
        Me.INDGlePeriod.Properties.DataSource = Me.FillingPeriod
        Me.INDGleFormatAnnex.Properties.DataSource = Me.FillingFormatAnnex
        Me.INDsleExpressedValues.Properties.DataSource = Me.ExpressedValues
        Me.LoadXpoBook()
        Me.INDGleLevelAccount.Properties.DataSource = Me.FillingNivel

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleVisualization.EditValue = 1
        Me.INDGlePeriod.EditValue = 1
        Me.INDGleFormatAnnex.EditValue = 1
        Me.INDsleExpressedValues.EditValue = 1
        Me.SetOfficialBook()
        Me.INDGleLevelAccount.EditValue = 2

        Me.INDSleAccountsZero.EditValue = False
        Me.INDsleIncludedAccountNumber.EditValue = True
        Me.INDsleDetailingThird.EditValue = False
        Me.INDSleAccountsCostCenter.EditValue = False
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando el formulario es eliminado y sus recursos deben ser liberados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        criterias = Nothing
        INDLastClosingDate = Nothing
        _FillingVisualization = Nothing
        _FillingPeriod = Nothing
        _FillingFormatAnnex = Nothing
        bookXpcollection = Nothing
        _FillingNivel = Nothing
        _ExpressedValues = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Carga los datos del control "Centro de Costo"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCostCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCostCenter.QueryPopUp
        If INDSleCostCenter.Properties.DataSource Is Nothing Then
            INDSleCostCenter.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountingService.ListCostcenterReport()
        End If
    End Sub

    ''' <summary>
    ''' Carga los datos del control "Sucursal"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleBranchOffice_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleBranchOffice.QueryPopUp
        If INDSleBranchOffice.Properties.DataSource Is Nothing Then
            INDSleBranchOffice.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetBranchOffice()
        End If
    End Sub

#End Region

#Region "Selector"

    ''' <summary>
    ''' Crea una instancia de la clase SelectorCache, esperando dos argumentos (Id, Code)
    ''' </summary>
    Private _selectorCostCenter As SelectorCache = New SelectorCache("Id", "Code")

    ''' <summary>
    ''' Crea una instancia de la clase SelectorCache, esperando dos argumentos (Id, Codigo)
    ''' </summary>
    Private _selectorBranchOffice As SelectorCache = New SelectorCache("Id", "Codigo")

    ''' <summary>
    ''' Maneja la obtención de datos no vinculados para columnas personalizadas en las vistas de cuadrícula "INDGvCostCenter y INDGvBranchOffice"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvCostCenter.CustomUnboundColumnData, INDGvBranchOffice.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCostCenter" Then
                e.Value = _selectorCostCenter.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvBranchOffice" Then
                e.Value = _selectorBranchOffice.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    ''' <summary>
    '''  Controla la selección y deselección de elementos en un selector personalizado cuando se hace clic en una celda de una fila en las vistas de cuadrícula INDGvCostCenter y INDGvBranchOffice
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvCostCenter.RowCellClick, INDGvBranchOffice.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCostCenter" Then
                selector = _selectorCostCenter
            ElseIf view.Name = "INDGvBranchOffice" Then
                selector = _selectorBranchOffice
            End If

            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)
            Else
                selector.Clear()
            End If
            view.RefreshData()
        End If
    End Sub

    ''' <summary>
    '''  Restablece el valor nulo y el texto de visualización personalizado en los controles INDSleCostCenter y INDSleBranchOffice cuando se cierra la ventana emergente (popup) sin seleccionar ningún elemento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleCostCenter.Closed, INDSleBranchOffice.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleCostCenter" Then
            searchLookupEdit.Properties.NullText = _selectorCostCenter.ToString()
        ElseIf searchLookupEdit.Name = "INDSleBranchOffice" Then
            searchLookupEdit.Properties.NullText = _selectorBranchOffice.ToString()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Ajusta dinámicamente la interfaz de usuario y los controles en función de la opción de visualización seleccionada en el control INDGleVisualization
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleVisualization_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleVisualization.EditValueChanged
        INDLciDateStart.MinSize = If({5}.Contains(INDGleVisualization.EditValue), New System.Drawing.Size(180, 92), New System.Drawing.Size(360, 92))
        INDLciDateStart.MaxSize = If({5}.Contains(INDGleVisualization.EditValue), New System.Drawing.Size(180, 92), New System.Drawing.Size(360, 92))
        INDLciDateEnd.MinSize = If({5}.Contains(INDGleVisualization.EditValue), New System.Drawing.Size(180, 92), New System.Drawing.Size(360, 92))
        INDLciDateEnd.MaxSize = If({5}.Contains(INDGleVisualization.EditValue), New System.Drawing.Size(180, 92), New System.Drawing.Size(360, 92))

        INDLciPeriod.Visibility = If({1, 3}.Contains(INDGleVisualization.EditValue), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciFormatAnnex.Visibility = If({2}.Contains(INDGleVisualization.EditValue), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemInitialSince.Visibility = If({5}.Contains(INDGleVisualization.EditValue), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDlyItemEndUntil.Visibility = If({5}.Contains(INDGleVisualization.EditValue), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciSubTitle.Visibility = If({1, 2, 4, 5}.Contains(INDGleVisualization.EditValue), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)

        INDLciAccountsZero.Visibility = If({1, 2, 4, 5}.Contains(INDGleVisualization.EditValue), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciDetailingThirdParty.Visibility = If({1}.Contains(INDGleVisualization.EditValue), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciAccountsCostCenter.Visibility = If({1, 2, 4, 5}.Contains(INDGleVisualization.EditValue), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciCostCenter.Visibility = If({1}.Contains(INDGleVisualization.EditValue), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciBranchOffice.Visibility = If({1}.Contains(INDGleVisualization.EditValue), DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)

        INDCdnDateStart.Enabled = If({1, 2, 3, 5}.Contains(INDGleVisualization.EditValue), True, False)
        INDCdnDateStart.SetMonth = If({1, 3, 5}.Contains(INDGleVisualization.EditValue), Month(INDLastClosingDate), If(INDGleVisualization.EditValue = 2, If(Month(INDLastClosingDate) < 3, 1, Month(INDLastClosingDate) - 2), 1))
        INDCdnDateStart.SetYear = Year(INDLastClosingDate)
        INDCdnDateStart2.SetMonth = INDCdnDateStart.GetMonth
        INDCdnDateStart2.SetYear = INDCdnDateStart.GetYear

        INDCdnDateEnd.Enabled = If({1, 3, 4, 5}.Contains(INDGleVisualization.EditValue), True, False)
        INDCdnDateEnd.SetMonth = If(INDGleVisualization.EditValue = 2, If(Month(INDLastClosingDate) < 3, 3, Month(INDLastClosingDate)), If(INDGleVisualization.EditValue = 4, 12, Month(INDLastClosingDate)))
        INDCdnDateEnd.SetYear = Year(INDLastClosingDate)
        INDCdnDateEnd2.SetMonth = INDCdnDateEnd.GetMonth
        INDCdnDateEnd2.SetYear = INDCdnDateEnd.GetYear
    End Sub

#End Region

#Region "OnChange"

    ''' <summary>
    ''' Se ejectura cuando se altera la fecha del control INDCdnDateStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCdnDateStart_OnChangeDate(sender As Object, e As EventArgs) Handles INDCdnDateStart.OnChangeDate
        'si el periodo es periodo contable activo y la visializacion es estandar o presentacion bancaria
        If INDGleVisualization.EditValue = 1 And INDGlePeriod.EditValue = 1 Or INDGleVisualization.EditValue = 3 And INDGlePeriod.EditValue = 1 Then
            'cuando el usuario cambie la fecha inicial si la fecha inicial es menor que la fecha del ultimo cierre hacer la fecha inicial igual que la fecha del ultimo cierre
            If INDCdnDateStart.GetYear < Year(INDLastClosingDate) Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidatePeriodClosingReport", "Commons"), INDCdnDateStart.GetMonth, INDCdnDateStart.GetYear)
                INDCdnDateStart.SetYear = Year(INDLastClosingDate)
                'If INDCdnDateStart.GetMonth < Month(INDLastClosingDate) Then
                INDCdnDateStart.SetMonth = Month(INDLastClosingDate)
                'End If
            End If
            'cuando el usuario cambie la fecha inicial si la fecha final es menor que la fecha inicial hacer la fecha final igual que la fecha inicial
            If INDCdnDateEnd.GetYear <= INDCdnDateStart.GetYear Then
                INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
                If INDCdnDateEnd.GetMonth < INDCdnDateStart.GetMonth Then
                    INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
                End If
            End If
            'cuando el usuario cambie la fecha incial si la fecha final es mayor que la fecha inicial + 1 año  hacer la fecha final igual que la fecha inicial + 1 año
            If INDCdnDateStart.GetMonth = 12 Then
                If INDCdnDateEnd.GetYear > INDCdnDateStart.GetYear + 1 Then
                    INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear + 1
                    INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
                End If
            ElseIf INDCdnDateEnd.GetYear >= INDCdnDateStart.GetYear Then
                INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
                If INDCdnDateEnd.GetMonth <= 1 Then
                    INDCdnDateEnd.SetMonth = 12
                End If
            End If
            'si el periodo es periodo contable cerrado y la visializacion es estandar o presentacion bancaria
        ElseIf INDGleVisualization.EditValue = 1 And INDGlePeriod.EditValue = 2 Or INDGleVisualization.EditValue = 3 And INDGlePeriod.EditValue = 2 Then
            'cuando el usuario cambie la fecha inicial si la fecha inicial es mayor que la fecha del ultimo cierre hacer la fecha inicial igual que la fecha del ultimo cierre
            If INDCdnDateStart.GetYear > Year(INDLastClosingDate) Then
                INDCdnDateStart.SetYear = Year(INDLastClosingDate)
                'If INDCdnDateStart.GetMonth <= 1 Then
                INDCdnDateStart.SetMonth = Month(INDLastClosingDate)
                'End If
            End If
            'cuando el usuario cambie la fecha inicial si la fecha final es menor que la fecha inicial hacer la fecha final igual que la fecha inicial
            If INDCdnDateEnd.GetYear <= INDCdnDateStart.GetYear Then
                INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
                If INDCdnDateEnd.GetMonth <= INDCdnDateStart.GetMonth Then
                    INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
                End If
            End If
            'cuando el usuario cambie la fecha final si la fecha final es mayor que la fecha inicial + 1 año  hacer la fecha final igual que la fecha inicial + 1 año
            If INDCdnDateEnd.GetYear >= Year(INDLastClosingDate) Then
                INDCdnDateEnd.SetYear = Year(INDLastClosingDate)
                If INDCdnDateEnd.GetMonth > Month(INDLastClosingDate) Then
                    INDCdnDateEnd.SetMonth = Month(INDLastClosingDate)
                End If
            ElseIf INDCdnDateStart.GetMonth = 12 Then
                If INDCdnDateEnd.GetYear >= INDCdnDateStart.GetYear + 1 Then
                    INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear + 1
                    INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
                End If
            ElseIf INDCdnDateEnd.GetYear >= INDCdnDateStart.GetYear Then
                INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
                If INDCdnDateEnd.GetMonth <= 1 Then
                    INDCdnDateEnd.SetMonth = 1
                End If
            End If
            'si la visualizacion es trimestral
        ElseIf INDGleVisualization.EditValue = 2 Then
            If INDCdnDateStart.GetMonth > 10 Then
                INDCdnDateStart.SetMonth = 10
                INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
            End If
            INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth + 2
            INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
        ElseIf INDGleVisualization.EditValue = 5 Then 'Si el tipo de informe es comparativo
            INDCdnDateStart2.SetYear = INDCdnDateStart.GetYear
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia la fecha en el control INDCdnDateStart2
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCdnDateStart2_OnChangeDate(sender As Object, e As EventArgs) Handles INDCdnDateStart2.OnChangeDate
        If INDGleVisualization.EditValue = 5 Then 'Si el tipo de informe es comparativo
            INDCdnDateStart.SetYear = INDCdnDateStart2.GetYear
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia la fecha en el control INDCdnDateEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCdnDateEnd_OnChangeDate(sender As Object, e As EventArgs) Handles INDCdnDateEnd.OnChangeDate
        'si el periodo es periodo contable activo y la visializacion es estandar o presentacion bancaria
        If INDGleVisualization.EditValue = 1 And INDGlePeriod.EditValue = 1 Or INDGleVisualization.EditValue = 3 And INDGlePeriod.EditValue = 1 Then
            'cuando el usuario cambie la fecha final si la fecha final es menor que la fecha inicial hacer la fecha final igual que la fecha inicial
            If INDCdnDateEnd.GetYear <> INDCdnDateStart.GetYear Then
                INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
            End If
            If INDCdnDateEnd.GetMonth < INDCdnDateStart.GetMonth Then
                INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
            End If
            'si el periodo es periodo contable cerrado y la visializacion es estandar o presentacion bancaria
        ElseIf INDGleVisualization.EditValue = 1 And INDGlePeriod.EditValue = 2 Or INDGleVisualization.EditValue = 3 And INDGlePeriod.EditValue = 2 Then
            'cuando el usuario cambie la fecha final si la fecha final es menor que la fecha inicial hacer la fecha final igual que la fecha inicial
            If INDCdnDateEnd.GetYear <= INDCdnDateStart.GetYear Then
                INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
                If INDCdnDateEnd.GetMonth < INDCdnDateStart.GetMonth Then
                    INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
                End If
            End If
            'cuando el usuario cambie la fecha final si la fecha final es mayor que la fecha inicial + 1 año  hacer la fecha final igual que la fecha inicial + 1 año
            If INDCdnDateEnd.GetYear >= Year(INDLastClosingDate) Then
                INDCdnDateEnd.SetYear = Year(INDLastClosingDate)
                If INDCdnDateEnd.GetMonth <= 1 Then
                    INDCdnDateEnd.SetMonth = 12
                End If
            ElseIf INDCdnDateStart.GetMonth = 12 Then
                If INDCdnDateEnd.GetYear >= INDCdnDateStart.GetYear + 1 Then
                    INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear + 1
                    INDCdnDateEnd.SetMonth = INDCdnDateStart.GetMonth
                End If
            ElseIf INDCdnDateEnd.GetYear >= INDCdnDateStart.GetYear Then
                INDCdnDateEnd.SetYear = INDCdnDateStart.GetYear
                If INDCdnDateEnd.GetMonth <= 1 Then
                    INDCdnDateEnd.SetMonth = 12
                End If
            End If
        ElseIf INDGleVisualization.EditValue = 4 Then 'Si el tipo de informe es anual
            INDCdnDateStart.SetYear = INDCdnDateEnd.GetYear
        ElseIf INDGleVisualization.EditValue = 5 Then 'Si el tipo de reporte es comprativo
            INDCdnDateEnd2.SetYear = INDCdnDateEnd.GetYear
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia la fecha en el control INDCdnDateEnd2
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCdnDateEnd2_OnChangeDate(sender As Object, e As EventArgs) Handles INDCdnDateEnd2.OnChangeDate
        If INDGleVisualization.EditValue = 5 Then 'Si el tipo de reporte es comprativo
            INDCdnDateEnd.SetYear = INDCdnDateEnd2.GetYear
        End If
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' Controla la generación y visualización de diferentes tipos de informes según los valores seleccionados en los controles correspondientes. El proceso varía según el tipo de informe y los datos disponibles en las fuentes de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            If {1, 2, 3}.Contains(Me.INDGleVisualization.EditValue) Then
                Dim reporte As New rptResultStatus()

                Dim DataSourceEmpty As Boolean = False
                reporte.ParametrosReporte = New Object() {criterias}
                INDDvViewReport.DocumentSource = reporte
                 If Me.INDGleVisualization.EditValue = 1 Then
                    'se envia la naturaleza de la clase de la cuenta  1 = Debito, 2 = credito
                    reporte.INDSbrStandar.ReportSource.DataSource = Await reporte.FillListResultStatus("1,2")
                    reporte.INDSbrStandar.ReportSource.DataMember = "ReportResulStatus"
                    reporte.INDSbrStandar.ReportSource.Parameters("IncludedAccountNumber").Value = INDsleIncludedAccountNumber.EditValue
                    If reporte.INDSbrStandar.ReportSource.DataSource IsNot Nothing AndAlso CType(reporte.INDSbrStandar.ReportSource.DataSource, DataTable).Rows.Count > 0 Then
                        DataSourceEmpty = True
                    End If
                ElseIf Me.INDGleVisualization.EditValue = 2 Then
                    'se envia la naturaleza de la clase de la cuenta  1 = Debito, 2 = credito
                    reporte.INDSbrTrimester.ReportSource.DataSource = Await reporte.FillListResultStatus("1,2")
                    reporte.INDSbrTrimester.ReportSource.DataMember = "ReportResulStatus"
                    reporte.INDSbrTrimester.ReportSource.Parameters("IncludedAccountNumber").Value = INDsleIncludedAccountNumber.EditValue
                    If reporte.INDSbrTrimester.ReportSource.DataSource IsNot Nothing AndAlso CType(reporte.INDSbrTrimester.ReportSource.DataSource, DataTable).Rows.Count > 0 Then
                        DataSourceEmpty = True
                    End If
                ElseIf Me.INDGleVisualization.EditValue = 3 Then
                    'se envia la naturaleza 2 = Credito
                    reporte.INDSbrIncome.ReportSource.DataSource = Await reporte.FillListResultStatus("2")
                    reporte.INDSbrIncome.ReportSource.DataMember = "ReportResulStatus"
                    reporte.INDSbrIncome.ReportSource.Parameters("IncludedAccountNumber").Value = INDsleIncludedAccountNumber.EditValue
                    If reporte.INDSbrIncome.ReportSource.DataSource IsNot Nothing AndAlso CType(reporte.INDSbrIncome.ReportSource.DataSource, DataTable).Rows.Count > 0 Then
                        DataSourceEmpty = True
                    End If
                    'se envia la naturaleza 1 = Debito 
                    reporte.INDSbrCosts.ReportSource.DataSource = Await reporte.FillListResultStatus("1")
                    reporte.INDSbrCosts.ReportSource.DataMember = "ReportResulStatus"
                    reporte.INDSbrCosts.ReportSource.Parameters("IncludedAccountNumber").Value = INDsleIncludedAccountNumber.EditValue
                    If reporte.INDSbrIncome.ReportSource.DataSource IsNot Nothing AndAlso CType(reporte.INDSbrCosts.ReportSource.DataSource, DataTable).Rows.Count > 0 Then
                        DataSourceEmpty = True
                    End If
                End If

                AsyncLoader(False)
                If DataSourceEmpty = True Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDCdnDateStart.Focus()
                End If
            ElseIf Me.INDGleVisualization.EditValue = 4 Then 'Si el tipo de reporte es anual
                Dim reporte As New rptResultStatusAnualGroupSubReports()
                rptResultStatusAnual.ExpressedValues = ExpressedValue

                Dim DataSourceEmpty As Boolean = False
                reporte.ParametrosReporte = New Object() {criterias}
                INDDvViewReport.DocumentSource = reporte

                Dim listDatasourceGeneral = Await reporte.FillListResultStatus("1,2")

                If listDatasourceGeneral IsNot Nothing AndAlso listDatasourceGeneral.Rows.Count > 0 Then
                    Dim tmp1 As DataRow() = listDatasourceGeneral.Select("availability = 4 and classCode in ('4', '6', '7')")
                    If tmp1.Count > 0 Then
                        Dim listDatasource = tmp1.CopyToDataTable()
                        Dim valueBalanceAccount As Decimal = ReturnDecimal(listDatasource.Compute("SUM(NewBalance)", "mainAccountLevel = 1 and classCode in ('4')"))
                        Dim valueBalanceAccount2 As Decimal = ReturnDecimal(listDatasource.Compute("SUM(NewBalance)", "mainAccountLevel = 1 and classCode in ('6', '7')"))
                        reporte.TotalValue1 = valueBalanceAccount - valueBalanceAccount2

                        reporte.INDSbrAnual.ReportSource.DataSource = listDatasource
                        reporte.INDSbrAnual.ReportSource.DataMember = "ReportResulStatus"
                        reporte.INDSbrAnual.ReportSource.Parameters("IncludedAccountNumber").Value = INDsleIncludedAccountNumber.EditValue
                        If reporte.INDSbrAnual.ReportSource.DataSource IsNot Nothing AndAlso CType(reporte.INDSbrAnual.ReportSource.DataSource, DataTable).Rows.Count > 0 Then
                            DataSourceEmpty = True
                        End If
                    End If

                    Dim tmp2 As DataRow() = listDatasourceGeneral.Select("availability = 4 and classCode in ('5')")
                    If tmp2.Count > 0 Then
                        Dim listDatasource = tmp2.CopyToDataTable()
                        reporte.TotalValue2 = reporte.TotalValue1 - ReturnDecimal(listDatasource.Compute("SUM(NewBalance)", "mainAccountLevel = 1 and classCode in ('5')"))

                        reporte.INDSbrAnual2.ReportSource.DataSource = listDatasource
                        reporte.INDSbrAnual2.ReportSource.DataMember = "ReportResulStatus"
                        reporte.INDSbrAnual2.ReportSource.Parameters("IncludedAccountNumber").Value = INDsleIncludedAccountNumber.EditValue
                        If reporte.INDSbrAnual2.ReportSource.DataSource IsNot Nothing AndAlso CType(reporte.INDSbrAnual2.ReportSource.DataSource, DataTable).Rows.Count > 0 Then
                            DataSourceEmpty = True
                        End If
                    End If

                    Dim tmp3 As DataRow() = listDatasourceGeneral.Select("availability = 5 and classCode in ('4', '5')")
                    If tmp3.Count > 0 Then
                        Dim listDatasource = tmp3.CopyToDataTable()
                        Dim valueBalanceAccount As Decimal = ReturnDecimal(listDatasource.Compute("SUM(NewBalance)", "mainAccountLevel = 1 and classCode in ('4')"))
                        Dim valueBalanceAccount2 As Decimal = ReturnDecimal(listDatasource.Compute("SUM(NewBalance)", "mainAccountLevel = 1 and classCode in ('5')"))
                        reporte.TotalValue3 = reporte.TotalValue2 + valueBalanceAccount - valueBalanceAccount2

                        reporte.INDSbrAnual3.ReportSource.DataSource = listDatasource
                        reporte.INDSbrAnual3.ReportSource.DataMember = "ReportResulStatus"
                        reporte.INDSbrAnual3.ReportSource.Parameters("IncludedAccountNumber").Value = INDsleIncludedAccountNumber.EditValue
                        If reporte.INDSbrAnual3.ReportSource.DataSource IsNot Nothing AndAlso CType(reporte.INDSbrAnual3.ReportSource.DataSource, DataTable).Rows.Count > 0 Then
                            DataSourceEmpty = True
                        End If
                    End If
                End If

                AsyncLoader(False)
                If DataSourceEmpty Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDCdnDateStart.Focus()
                End If
            ElseIf Me.INDGleVisualization.EditValue = 5 Then 'Si el tipo de reporte es comparativo
                Dim reporte As New rptResultStatusComparativeGroupSubReports()
                rptResultStatusComparative.ExpressedValues = ExpressedValue

                Dim DataSourceEmpty As Boolean = False
                reporte.ParametrosReporte = New Object() {criterias}
                INDDvViewReport.DocumentSource = reporte

                Dim listDatasourceGeneral = Await reporte.FillListResultStatus("1,2")

                If listDatasourceGeneral IsNot Nothing AndAlso listDatasourceGeneral.Rows.Count > 0 Then
                    Dim tmp1 As DataRow() = listDatasourceGeneral.Select("availability = 4 and classCode in ('4', '6', '7')")
                    If tmp1.Count > 0 Then
                        Dim listDatasource = tmp1.CopyToDataTable()

                        Dim valueBalanceAccount As Decimal = ReturnDecimal(listDatasource?.Compute("SUM(newBalanceInitial)", "mainAccountLevel = 1 and classCode in ('4')"))

                        Dim valueBalanceAccount2 As Decimal = ReturnDecimal(listDatasource?.Compute("SUM(newBalanceInitial)", "mainAccountLevel = 1 and classCode in ('6', '7')"))

                        Dim valueBalanceAccountFinal As Decimal = ReturnDecimal(listDatasource.Compute("SUM(newBalanceFinal)", "mainAccountLevel = 1 and classCode in ('4')"))

                        Dim valueBalanceAccountFinal2 As Decimal = ReturnDecimal(listDatasource.Compute("SUM(newBalanceFinal)", "mainAccountLevel = 1 and classCode in ('6', '7')"))

                        reporte.TotalValueInitial1 = valueBalanceAccount - valueBalanceAccount2
                        reporte.TotalValueFinal1 = valueBalanceAccountFinal - valueBalanceAccountFinal2
                        reporte.AbsoluteValue1 = reporte.TotalValueInitial1 - reporte.TotalValueFinal1
                        reporte.PercentageAbsolute1 = If(reporte.TotalValueFinal1 > 0, (reporte.AbsoluteValue1 / reporte.TotalValueFinal1 * 100) / 100, 100)

                        reporte.INDSbrComparative.ReportSource.DataSource = listDatasource
                        reporte.INDSbrComparative.ReportSource.DataMember = "ReportResulStatusComparative"
                        reporte.INDSbrComparative.ReportSource.Parameters("IncludedAccountNumber").Value = INDsleIncludedAccountNumber.EditValue
                        If reporte.INDSbrComparative.ReportSource.DataSource IsNot Nothing AndAlso CType(reporte.INDSbrComparative.ReportSource.DataSource, DataTable).Rows.Count > 0 Then
                            DataSourceEmpty = True
                        End If
                    End If

                    Dim tmp2 As DataRow() = listDatasourceGeneral.Select("availability = 4 and classCode in ('5')")
                    If tmp2.Count > 0 Then
                        Dim listDatasource = tmp2.CopyToDataTable()
                        reporte.TotalValueInitial2 = reporte.TotalValueInitial1 - ReturnDecimal(listDatasource.Compute("SUM(newBalanceInitial)", "mainAccountLevel = 1 and classCode in ('5')"))
                        reporte.TotalValueFinal2 = reporte.TotalValueFinal1 - ReturnDecimal(listDatasource.Compute("SUM(newBalanceFinal)", "mainAccountLevel = 1 and classCode in ('5')"))
                        reporte.AbsoluteValue2 = reporte.TotalValueInitial2 - reporte.TotalValueFinal2
                        reporte.PercentageAbsolute2 = If(reporte.TotalValueFinal2 > 0, (reporte.AbsoluteValue2 / reporte.TotalValueFinal2 * 100) / 100, 100)

                        reporte.INDSbrComparative2.ReportSource.DataSource = listDatasource
                        reporte.INDSbrComparative2.ReportSource.DataMember = "ReportResulStatusComparative"
                        reporte.INDSbrComparative2.ReportSource.Parameters("IncludedAccountNumber").Value = INDsleIncludedAccountNumber.EditValue
                        If reporte.INDSbrComparative2.ReportSource.DataSource IsNot Nothing AndAlso CType(reporte.INDSbrComparative2.ReportSource.DataSource, DataTable).Rows.Count > 0 Then
                            DataSourceEmpty = True
                        End If
                    End If

                    Dim tmp3 As DataRow() = listDatasourceGeneral.Select("availability = 5 and classCode in ('4', '5')")
                    If tmp3.Count > 0 Then
                        Dim listDatasource = tmp3.CopyToDataTable()
                        Dim valueBalanceAccount As Decimal = ReturnDecimal(listDatasource.Compute("SUM(newBalanceInitial)", "mainAccountLevel = 1 and classCode in ('4')"))
                        Dim valueBalanceAccount2 As Decimal = ReturnDecimal(listDatasource.Compute("SUM(newBalanceInitial)", "mainAccountLevel = 1 and classCode in ('5')"))

                        Dim valueBalanceAccountFinal As Decimal = ReturnDecimal(listDatasource.Compute("SUM(newBalanceFinal)", "mainAccountLevel = 1 and classCode in ('4')"))
                        Dim valueBalanceAccountFinal2 As Decimal = ReturnDecimal(listDatasource.Compute("SUM(newBalanceFinal)", "mainAccountLevel = 1 and classCode in ('5')"))

                        reporte.TotalValueInitial3 = reporte.TotalValueInitial2 + valueBalanceAccount - valueBalanceAccount2
                        reporte.TotalValueFinal3 = reporte.TotalValueFinal2 + valueBalanceAccountFinal - valueBalanceAccountFinal2
                        reporte.AbsoluteValue3 = reporte.TotalValueInitial3 - reporte.TotalValueFinal3
                        reporte.PercentageAbsolute3 = If(reporte.TotalValueFinal3 > 0, (reporte.AbsoluteValue3 / reporte.TotalValueFinal3 * 100) / 100, 100)

                        reporte.INDSbrComparative3.ReportSource.DataSource = listDatasource
                        reporte.INDSbrComparative3.ReportSource.DataMember = "ReportResulStatusComparative"
                        reporte.INDSbrComparative3.ReportSource.Parameters("IncludedAccountNumber").Value = INDsleIncludedAccountNumber.EditValue
                        If reporte.INDSbrComparative3.ReportSource.DataSource IsNot Nothing AndAlso CType(reporte.INDSbrComparative3.ReportSource.DataSource, DataTable).Rows.Count > 0 Then
                            DataSourceEmpty = True
                        End If
                    End If
                End If

                AsyncLoader(False)
                If DataSourceEmpty Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDCdnDateStart.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Restablece la interfaz del usuario para que vuelva a la vista que permite al usuario generar y personalizar informes
    ''' </summary>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
    End Sub

    ''' <summary>
    ''' Genera un archivo excel basado en los datos del informe seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcel.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Await Task.Factory.StartNew(Sub()
                                                Select Case INDGleVisualization.EditValue
                                                    Case 1
                                                        chargueDataSourceStandard()
                                                    Case 2
                                                        chargueDataSourceQuaterly()
                                                    Case 3
                                                        chargueDataSourceStandard()
                                                    Case 4
                                                        chargueDataSourceStandard()
                                                    Case 5
                                                        chargueDataSourceComparative()
                                                    Case Else
                                                        Exit Sub
                                                End Select
                                            End Sub)

                If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                    generateExcel()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' funcion que valida que el obj no sea db null
    ''' </summary>
    ''' <returns></returns>
    Private Function ReturnDecimal(Value As Object) As Decimal
        Return If(DBNull.Value.Equals(Value), 0, Convert.ToDecimal(Value))
    End Function
#End Region

#End Region

End Class