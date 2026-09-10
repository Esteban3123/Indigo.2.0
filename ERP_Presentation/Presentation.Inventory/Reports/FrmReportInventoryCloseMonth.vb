#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Presentation.Inventory.MVP
Imports Domain.Entities
Imports Presentation.Controls
Imports Presentation.CloudAgent
Imports Presentation.Accounting.MVP
Imports System.Globalization

#End Region


Public Class FrmReportInventoryCloseMonth
    Implements IClosedMonthInventory

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' diccionario para obtenes los rangos
    ''' </summary>
    Private _filters As Dictionary(Of String, String)

    Dim ReportType As Integer

    ''' <summary>
    ''' obtiene la informacion de la moneda
    ''' </summary>
    Private CurrencyData As Currency

    ''' <summary>
    ''' obtiene la informacion de la configuracion de la compañia
    ''' </summary>
    Private CompanySettings As CompanySettings

    ''' <summary>
    ''' obtiene la moneda oficial
    ''' </summary>
    Dim OfficialCurrency As Integer


    Public Property ProoftCloseXpoWarehouse As XPInstantFeedbackSource
    Public Property ProoftCloseXpoGroup As XPInstantFeedbackSource
    Public Property YearClosed As Integer Implements IClosedMonthInventory.YearClosed
    Public Property MonthClosed As Integer Implements IClosedMonthInventory.MonthClosed

    ''' <summary>
    ''' obtiene la moneda seleccionada en el campo moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property Currency As Integer
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrency.EditValue = value
        End Set
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
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim validations As Boolean = True
        'validaciones de almacen
        If INDSleInitialWarehouse.EditValue <> Nothing And INDSleFinalWarehouse.EditValue = Nothing Or INDSleInitialWarehouse.EditValue = Nothing And INDSleFinalWarehouse.EditValue <> Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), "Almacén")
            Me.INDSleInitialWarehouse.Focus()
            validations = False
        ElseIf INDSleInitialWarehouse.EditValue > INDSleFinalWarehouse.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), "Almacén")
            Me.INDSleInitialWarehouse.Focus()
            validations = False
        End If
        'validaciones de grupo
        If INDSleInitialGroup.EditValue <> Nothing And INDSleFinalGroup.EditValue = Nothing Or INDSleInitialGroup.EditValue = Nothing And INDSleFinalGroup.EditValue <> Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), "Grupo")
            Me.INDSleInitialGroup.Focus()
            validations = False
        ElseIf INDSleInitialGroup.EditValue > INDSleFinalGroup.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), "Grupo")
            Me.INDSleInitialGroup.Focus()
            validations = False
        End If
        Return validations
    End Function

    Public WriteOnly Property ActionsOnControls As Boolean Implements IClosedMonthInventory.ActionsOnControls
        Set(value As Boolean)
            INDCdnDate.Enabled = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IClosedMonthInventory.MyLayoutControl
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IClosedMonthInventory.MyTag
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Private WriteOnly Property IcrudBase_Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

#End Region

#Region "Globals"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Representa la entidad de parametros de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim _inventorySettings As SettingInventory

#End Region

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "ICRUD"

    Public Sub Buscar() Implements ICrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Throw New NotImplementedException()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "Events"

    Private Async Sub FrmReportInventoryCloseMonth_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Await LoadControls()
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)

        Me.INDSleInitialWarehouse.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetWarehouse
        Me.INDSleFinalWarehouse.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetWarehouse
        Me.INDSleInitialGroup.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetProductGroup
        Me.INDSleFinalGroup.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetProductGroup
    End Sub

    ''' <summary>
    ''' Metodo que despliega el QueryPopUp cuando se da clic sobre el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleInitialWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialWarehouse.QueryPopUp
        If INDSleInitialWarehouse.Datasource Is Nothing Then
            LoadXpoInitialWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que despliega el QueryPopUp cuando se da clic sobre el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleFinalWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalWarehouse.QueryPopUp
        If INDSleFinalWarehouse.Datasource Is Nothing Then
            LoadXpoFinalWarehouse()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que despliega el QueryPopUp cuando se da clic sobre el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleInitialGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInitialGroup.QueryPopUp
        If INDSleInitialGroup.Datasource Is Nothing Then
            LoadXpoInitialGroup()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que despliega el QueryPopUp cuando se da clic sobre el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleFinalGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinalGroup.QueryPopUp
        If INDSleFinalGroup.Datasource Is Nothing Then
            LoadXpoFinalGroup()
        End If
    End Sub

    ''' <summary>
    ''' popup para mostrar las monedas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If INDsleCurrency.Properties.DataSource Is Nothing Then
            INDsleCurrency.Properties.DataSource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub

    ''' <summary>
    ''' agrega los filtros al dicciconario 
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetFilters() As Task
        _filters = New Dictionary(Of String, String)
        _filters.Add("Year", INDCdnDate.GetYear)
        _filters.Add("Month", INDCdnDate.GetMonth)
        _filters.Add("InitialWarehouse", INDSleInitialWarehouse.EditValue)
        _filters.Add("FinalWarehouse", INDSleFinalWarehouse.EditValue)
        _filters.Add("InitialGroup", INDSleInitialGroup.EditValue)
        _filters.Add("FinalGroup", INDSleFinalGroup.EditValue)
        _filters.Add("ReportType", ReportType)

        If Currency <= 0 Then
            Await GetOfficialCurrency()
            If OfficialCurrency <= 0 Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format("No tiene parametrizada moneda oficial")
            End If
            Await GetDataCurrency(OfficialCurrency)
            _filters.Add("ToCurrency", OfficialCurrency)
        Else
            Await GetDataCurrency(Currency)
            _filters.Add("ToCurrency", Currency)
        End If


    End Function

    ''' <summary>
    ''' obtiene la informacion de la moneda por el id
    ''' </summary>
    ''' <param name="currencyId"></param>
    ''' <returns></returns>
    Private Async Function GetDataCurrency(currencyId As Integer) As Task
        Using Model As New MCurrency("")
            CurrencyData = Await Model.GetCurrencyById(currencyId)
        End Using
    End Function

    ''' <summary>
    ''' obtiene la moneda oficial de configuracion de la compañia
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetOfficialCurrency() As Task
        Using Model As New MCompanySettings("")
            CompanySettings = Await Model.GetCompanySettings()
            OfficialCurrency = CompanySettings?.OfficialCurrencyId
        End Using
    End Function

    Private Async Sub INDSbGenrateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenrateReport.Click
        If ValidateControlsReports() = True Then
            AsyncLoader(True)

            ReportType = 2
            Await GetFilters()
            Dim reporte As New rptInventoryCloseMonth
            reporte.Currency = CurrencyData
            reporte.ParametrosReporte = New Object() {_filters}
            INDDvReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            If reporte.DataSource IsNot Nothing AndAlso reporte.DataSource.Rows.Count > 0 Then
                reporte.CreateDocument()
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDCdnDate.Focus()
            End If
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        Await chargueDatasource()
    End Sub

    Private Sub INDCnBack_ClickBack() Handles INDCnNavigationReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcReport.Visible = False
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ProoftCloseXpoGroup = Nothing
        ProoftCloseXpoWarehouse = Nothing
        YearClosed = Nothing
        MonthClosed = Nothing
        _idOperativeUnit = Nothing
        _inventorySettings = Nothing
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        Using Model As New MSettingInventory(CStr(Me.Tag))
            AsyncLoader(True)
            Try
                Dim resulOperation = Await Model.GetInventorySettingsRegister(Me._idOperativeUnit)
                If resulOperation.ObjectEmbbeded Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SettingParameter", "Inventory"))
                    Exit Function
                End If
                ActionsOnControls = False
                _inventorySettings = resulOperation.ObjectEmbbeded
                MonthClosed = _inventorySettings.Month
                YearClosed = _inventorySettings.Year
                'Se selecciona el mes cerrado
                If MonthClosed = 1 Then
                    MonthClosed = 12
                    YearClosed = YearClosed - 1
                Else
                    MonthClosed = MonthClosed - 1
                End If
                INDCdnDate.SetMonth = MonthClosed
                INDCdnDate.SetYear = YearClosed
            Catch ex As Exception
                Throw ex
            Finally
                AsyncLoader(False)
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Metodo utilizado para cargar el Xpo y mostrarlo en el SearchLookUpEdit
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialWarehouse()
        ProoftCloseXpoWarehouse = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListNoVirtualWarehouseByStatusAndUser(True, Nothing)
        INDSleInitialWarehouse.Datasource = ProoftCloseXpoWarehouse
    End Sub

    ''' <summary>
    ''' Metodo utilizado para cargar el Xpo y mostrarlo en el SearchLookUpEdit
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalWarehouse()
        ProoftCloseXpoWarehouse = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListNoVirtualWarehouseByStatusAndUser(True, Nothing)
        INDSleFinalWarehouse.Datasource = ProoftCloseXpoWarehouse
    End Sub

    ''' <summary>
    ''' Metodo utilizado para cargar el Xpo y mostrarlo en el SearchLookUpEdit
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoInitialGroup()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductGroup)
            INDSleInitialGroup.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' Metodo utilizado para cargar el Xpo y mostrarlo en el SearchLookUpEdit
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoFinalGroup()
        Using msearch As New MBusqueda
            ProoftCloseXpoGroup = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductGroup)
            INDSleFinalGroup.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function chargueDatasource() As Task
        Try
            AsyncLoader(True)

            Await GetFilters()

            ReportType = 2
            Dim dtReport As DataTable
            Dim IndList = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetReportCloseMonthAsync(_filters, Me.IndigoSessionValues)
            If IndList.Tables(0).Rows.Count > 0 Then
                dtReport = IndList.Tables("ReportCloseMonth")
                INDGcExportExcel.DataSource = dtReport
                If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                    generateExcel()
                End If
            Else
                INDGcExportExcel.DataSource = Nothing
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            End If
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Function

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

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

#End Region


End Class