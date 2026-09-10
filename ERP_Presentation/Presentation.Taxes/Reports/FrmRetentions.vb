#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Common.MVP
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports System.Data.SqlClient
Imports System.Data
Imports Infrastructure.Data.Xpo.TaxesRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports DevExpress.XtraReports.UI
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent
Imports Domain.Entities

#End Region

Public Class FrmRetentions

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

    ''' <summary>
    ''' lista de los tipos de reporte
    ''' </summary>
    Dim listReportType As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Diccionario de los criterios seleccionados
    ''' </summary>
    Private _criterias As Dictionary(Of String, String)

    ''' <summary>
    ''' Diccionario de los filtros seleccionados
    ''' </summary>
    Private _filters As Dictionary(Of String, String)
#End Region


#Region "properties"
    Public Property ProoftCloseXpoAccounts As XPInstantFeedbackSource
    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource
    'Private _FillingState As List(Of Tuple(Of Integer, String))
    'Private ReadOnly Property FillingState As List(Of Tuple(Of Integer, String))
    '    Get
    '        If _FillingState Is Nothing Then
    '            _FillingState = New List(Of Tuple(Of Integer, String))
    '            _FillingState.Add(New Tuple(Of Integer, String)(1, "Registrado"))
    '            _FillingState.Add(New Tuple(Of Integer, String)(2, "Confirmado"))
    '            _FillingState.Add(New Tuple(Of Integer, String)(3, "Anulado"))
    '        End If
    '        Return _FillingState
    '    End Get
    'End Property
    Private criteria As String = Nothing
    Public Property bookXpcollection As XPCollection
    Private List As List(Of GeneralLedgerRetentionsReportXpo)

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    '////////////////////***********CRITERIOS////////////////////***********

    ''' <summary>
    ''' Obtiene o establece la fecha inicial
    ''' </summary>
    Public Property DateStart As Date
        Get
            Return INDDateStart.EditValue
        End Get
        Set(value As Date)
            INDDateStart.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha final
    ''' </summary>
    Public Property DateEnd As Date
        Get
            Return INDDateEnd.EditValue
        End Get
        Set(value As Date)
            INDDateEnd.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha final
    ''' </summary>
    Public Property Book As Integer
        Get
            Return INDsleBook.EditValue
        End Get
        Set(value As Integer)
            INDsleBook.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de almacenamiento
    ''' </summary>
    Public Property ReportType As Integer
        Get
            Return INDsleReportType.EditValue
        End Get
        Set(value As Integer)
            INDsleReportType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de moneda del reporte
    ''' </summary>
    Public Property ReportCurrency As Integer?
        Get
            Return INDsleReportCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDsleReportCurrency.EditValue = value
        End Set
    End Property

    '////////////////////***********FILTROS***********////////////////////
    ''' <summary>
    ''' Obtiene o establece la cuenta de inicio
    ''' </summary>
    Public Property AccountsStart As String
        Get
            Return INDSleAccountsStart.EditValue
        End Get
        Set(value As String)
            INDSleAccountsStart.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cuenta final
    ''' </summary>
    Public Property AccountsEnd As String
        Get
            Return INDSleAccountsEnd.EditValue
        End Get
        Set(value As String)
            INDSleAccountsEnd.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tercero de inicio
    ''' </summary>
    Public Property ThirdPartyStart As String
        Get
            Return INDSleThirdPartyStart.EditValue
        End Get
        Set(value As String)
            INDSleThirdPartyStart.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tercero final
    ''' </summary>
    Public Property ThirdPartyEnd As String
        Get
            Return INDSleThirdPartyEnd.EditValue
        End Get
        Set(value As String)
            INDSleThirdPartyEnd.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' lista de las monedas
    ''' </summary>
    Private ListXpoCurrency As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource para las tarifa IVA
    ''' </summary>
    ''' <value>
    ''' The General Ledger datasource.
    ''' </value>
    Public Property GeneralLedgerIVADatasource As XPInstantFeedbackSource
        Get
            Return CType(INDSleGeneralLedgerIVA.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleGeneralLedgerIVA.Properties.DataSource = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' Inicializa el datasource de los combos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        listReportType = New List(Of Tuple(Of Byte, String))
        listReportType.Add(New Tuple(Of Byte, String)(1, "Retención"))
        listReportType.Add(New Tuple(Of Byte, String)(2, "Declaración IVA Ventas"))
        listReportType.Add(New Tuple(Of Byte, String)(3, "Declaración IVA compras"))
        INDsleReportType.Properties.DataSource = listReportType.ToList
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCostCenterEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBook()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True}
            bookXpcollection = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBookByStatusXpCollection, filter)
            INDsleBook.Properties.DataSource = bookXpcollection
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source de las monedas del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCurrency()
        Using msearch As New MBusqueda
            ListXpoCurrency = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Currency)
            INDsleReportCurrency.Properties.DataSource = ListXpoCurrency
        End Using
    End Sub

    ''' <summary>
    ''' Método para cargar el DataSource Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountsStart()
        If INDsleBook.EditValue IsNot Nothing Then
            criteria = String.Format("LegalBookId = {0} AND AllowsMovement = 1 AND RetencionType <> 0", INDsleBook.EditValue)

            Using msearch As New MBusqueda
                ProoftCloseXpoAccounts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsReportByFilter, criteria)
                INDSleAccountsStart.Datasource = ProoftCloseXpoAccounts
            End Using
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountsEnd()
        If INDsleBook.EditValue IsNot Nothing Then
            criteria = String.Format("LegalBookId = {0} AND AllowsMovement = 1 AND RetencionType <> 0", INDsleBook.EditValue)

            Using msearch As New MBusqueda
                ProoftCloseXpoAccounts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsReportByFilter, criteria)
                INDSleAccountsEnd.Datasource = ProoftCloseXpoAccounts
            End Using
        End If
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyStart.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirPartyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyEnd.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetOfficialBook()
        If bookXpcollection IsNot Nothing AndAlso bookXpcollection.Count > 0 Then
            Dim item = (From l In bookXpcollection Where l.OfficialBook = True Select l).FirstOrDefault
            If item IsNot Nothing Then
                INDsleBook.EditValue = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
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

        'Valida Cuentas
        If INDSleAccountsStart.EditValue Is Nothing And INDSleAccountsEnd.EditValue IsNot Nothing Or INDSleAccountsEnd.EditValue Is Nothing And INDSleAccountsStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblAccounts.Text)
            Me.INDSleAccountsStart.Focus()
            Validations = False
        ElseIf INDSleAccountsEnd.EditValue < INDSleAccountsStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblAccounts.Text)
            Me.INDSleAccountsStart.Focus()
            Validations = False
        End If

        'Valida Terceros
        If INDSleThirdPartyStart.EditValue Is Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Or INDSleThirdPartyEnd.EditValue Is Nothing And INDSleThirdPartyStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        ElseIf INDSleThirdPartyEnd.EditValue < INDSleThirdPartyStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        End If

        'valida la moneda exista
        If ReportType = 2 Or ReportType = 3 Then
            If ReportCurrency Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format("Ingrese la moneda del reporte", ReportCurrency)
                Me.INDsleReportCurrency.Focus()
                Validations = False
                Return False
            End If
        End If

        ''se agregan los criterios
        _criterias = New Dictionary(Of String, String)
        _criterias.Add("DateStart", DateStart.ToString("MM/dd/yyyy HH:mm:ss"))
        _criterias.Add("DateEnd", DateEnd.ToString("MM/dd/yyyy HH:mm:ss"))
        _criterias.Add("Book", Book)
        _criterias.Add("ReportType", ReportType)
        If ReportType = 2 Or ReportType = 3  Then
            _criterias.Add("ReportCurrency", ReportCurrency)
        End If

        ''se agregan los filtros
        _filters = New Dictionary(Of String, String)
        If ReportType = 1 Then
            _filters.Add("AccountsStart", AccountsStart)
            _filters.Add("AccountsEnd", AccountsEnd)
        End If
        If (ReportType = 2 Or ReportType = 3) And _selectorGeneralLedgerIVA.Count > 0 Then
            _filters.Add("GeneralLedgerIVA", _selectorGeneralLedgerIVA.GetKeys())
        End If
        _filters.Add("ThirdPartyStart", ThirdPartyStart)
        _filters.Add("ThirdPartyEnd", ThirdPartyEnd)

        Return Validations
    End Function

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoAccounts = Nothing
        ProoftCloseXpoThirdParty = Nothing
        criteria = Nothing
        bookXpcollection = Nothing
        List = Nothing
        _selectorGeneralLedgerIVA = Nothing
    End Sub

    ''' <summary>
    ''' Se ejecuta al darle clic al Botón INDSbGenerateReport para generar el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            AsyncLoader(True)
            Dim reporte As New rptRetentions()
            reporte.ParametrosReporte = {
                INDDateStart.EditValue, INDDateEnd.EditValue,
                INDSleAccountsStart.EditValue, INDSleAccountsEnd.EditValue,
                INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                INDsleBook.EditValue
            }

            INDDvViewReport.DocumentSource = reporte
            Await reporte.CargarDataSource1(_criterias, _filters)
            reporte.CreateDocument(True)
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReportExcel_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReportExcel.Click
        If Me.ValidateControlsReports = True Then
            Await chargeDatasource()
        End If
    End Sub

    Private Async Function chargeDatasource() As Task(Of DataTable)
        AsyncLoader(True)
        Dim dtReport As DataTable
        Try

            Dim IndList = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportRetentionsAsync(
                _criterias, _filters, Me.IndigoSessionValues
            )

            If IndList.Tables.Count > 0 Then

                Await Task.Factory.StartNew(Sub()
                                                Select Case ReportType
                                                    Case 1

                                                        Me.INDGcExportExcel.DataSource = Nothing

                                                        dtReport = IndList.Tables("ReportRetentions")
                                                        INDGcExportExcel.DataSource = dtReport
                                                        Me.INDGcExportExcel.RefreshDataSource()

                                                        If INDGcExportExcel.DataSource IsNot Nothing Then
                                                            generateExcel()
                                                        End If

                                                    Case 2
                                                        ClearGridControl()

                                                        dtReport = IndList.Tables("ReportStatementSaleIVA")
                                                        INDGcExportExcelIVA.DataSource = dtReport
                                                        Me.INDGcExportExcelIVA.RefreshDataSource()

                                                        If INDGcExportExcelIVA.DataSource IsNot Nothing Then
                                                            generateExcelIVA()
                                                        End If
                                                    Case 3
                                                        ClearGridControl()

                                                        dtReport = IndList.Tables("ReportStatementPurchaseIVA")
                                                        INDGcExportExcelIVA.DataSource = dtReport
                                                        Me.INDGcExportExcelIVA.RefreshDataSource()

                                                        If INDGcExportExcelIVA.DataSource IsNot Nothing Then
                                                            generateExcelIVA()
                                                        End If
                                                    Case Else
                                                        Exit Sub
                                                End Select
                                            End Sub)


            Else
                INDGcExportExcel.DataSource = Nothing
                Mensaje(EeventViewerImages.Advertencia) = "No existen datos a mostrar"
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
        AsyncLoader(False)
    End Function

    Private Sub ClearGridControl()
        For Each item In Me.INDGcExportExcelIVA.Views
            DirectCast(item, ColumnView).Columns.Clear()
        Next
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcel.DataSource = Nothing
        Me.INDGcExportExcel.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' metodo para generar excel cuando el tipo de documento es iva
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcelIVA()
        Dim _gridView = Me.INDGcExportExcelIVA
        _gridView.MainView.OptionsPrint.AutoResetPrintDocument = True
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcelIVA.DataSource = Nothing
        Me.INDGcExportExcelIVA.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDPcViewReport.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDDateStart.Focus()
    End Sub

    ''' <summary>
    ''' Se ejecuta al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRetentions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleAccountsStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleAccountsEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        LoadXpoBook()
        SetOfficialBook()
        LoadXpoCurrency()
        InitializeTuples()
        GetDataGeneralLedgerIVA()
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAccountsStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountsStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountsStart.QueryPopUp
        If INDSleAccountsStart.Datasource Is Nothing Then
            LoadXpoAccountsStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAccountsEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountsEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountsEnd.QueryPopUp
        If INDSleAccountsEnd.Datasource Is Nothing Then
            LoadXpoAccountsEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleThirdPartyStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdPartyStart.QueryPopUp
        If INDSleThirdPartyStart.Datasource Is Nothing Then
            LoadXpoThirdPartyStart()
        End If
    End Sub

    ''' <summary>
    ''' evento cuando se abre el popup de las tarifa de iva
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleGeneralLedgerIVA_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGeneralLedgerIVA.QueryPopUp
        If INDSleGeneralLedgerIVA.Properties.DataSource Is Nothing Then
            GetDataGeneralLedgerIVA()
        End If
    End Sub

    ''' <summary>
    ''' metodo para obtener las tarifa de iva 
    ''' </summary>
    Private Sub GetDataGeneralLedgerIVA()
        Using Model As New MBusqueda
            GeneralLedgerIVADatasource = Model.ConsultarEntidades(eDataSource.ListGeneralLedgerIva)
        End Using
    End Sub

#Region "Selector"

    Private _selectorGeneralLedgerIVA As SelectorCache = New SelectorCache("Id", "Name")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvGeneralLedgerIVA.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvGeneralLedgerIVA" Then
                e.Value = _selectorGeneralLedgerIVA.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    ''' <summary>
    ''' evento que almacena las tarifa de iva que se seleccioan
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvGeneralLedgerIVA.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvGeneralLedgerIVA" Then
                selector = _selectorGeneralLedgerIVA
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
    ''' evento que muestra los items seleccionados en las tarifa de iva
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleGeneralLedgerIVA.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleGeneralLedgerIVA" Then
            searchLookupEdit.Properties.NullText = _selectorGeneralLedgerIVA.ToString()
        End If
    End Sub

#End Region

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleThirdPartyEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdPartyEnd.QueryPopUp
        If INDSleThirdPartyEnd.Datasource Is Nothing Then
            LoadXpoThirdPartyEnd()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta al cargar por primer vez en formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRetentions_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar el GridLookUpEdit
        'Me.INDGleState.Properties.DataSource = FillingState

        'Asigna un valor por defecto a GridLookUpEdit
        'Me.INDGleState.EditValue = 1
    End Sub

    Private Sub INDsleBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBook.EditValueChanged
        If INDsleBook.EditValue IsNot Nothing Then
            LoadXpoAccountsStart()
            LoadXpoAccountsEnd()
        End If
    End Sub

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

    ''' <summary>
    ''' muestra los campos dependiendo del tipo de reporte que seleccione
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleReportType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleReportType.EditValueChanged
        Select Case ReportType
            Case 1
                INDLciReportCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciGeneralLedgerIVA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDLciLblAccounts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LayoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDSbGenerateReport.Enabled = True
            Case 2, 3

                INDLciLblAccounts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDLciReportCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciGeneralLedgerIVA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDSbGenerateReport.Enabled = False
        End Select
    End Sub
End Class