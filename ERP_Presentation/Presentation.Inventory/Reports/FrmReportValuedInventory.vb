#Region "Imports"
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Reporter
Imports Domain.Entities
Imports Presentation.Accounting.MVP
Imports Presentation.CloudAgent

#End Region

Public Class FrmReportValuedInventory

#Region "Fields"
    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

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
#End Region

#Region "Properties"

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
    ''' obtiene la valorizacion de los filtros
    ''' </summary>
    ''' <returns></returns>
    Public Property Valorization As Integer
        Get
            Return INDGleValorization.EditValue
        End Get
        Set(value As Integer)
            INDGleValorization.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene el campo ordenar por
    ''' </summary>
    ''' <returns></returns>
    Public Property OrderBy As Integer
        Get
            Return INDGleOrderBy.EditValue
        End Get
        Set(value As Integer)
            INDGleOrderBy.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene el campo cantidades en cero
    ''' </summary>
    ''' <returns></returns>
    Public Property AccountsZero As Boolean
        Get
            Return INDsleAccountsZero.EditValue
        End Get
        Set(value As Boolean)
            INDsleAccountsZero.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene el tipo de reporte
    ''' </summary>
    ''' <returns></returns>
    Public Property TypeReport As Integer
        Get
            Return INDGleTypeReport.EditValue
        End Get
        Set(value As Integer)
            INDGleTypeReport.EditValue = value
        End Set
    End Property

#End Region

#Region "Datasource"

    ''' <summary>
    ''' diccionario para obtenes los rangos
    ''' </summary>
    Private _range As Dictionary(Of String, String)

    ''' <summary>
    ''' diccionario para obtenes los filtros
    ''' </summary>
    Private _filters As Dictionary(Of String, String)

    Private _FillingValorization As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingValorization As List(Of Tuple(Of Integer, String))
        Get
            If _FillingValorization Is Nothing Then
                _FillingValorization = New List(Of Tuple(Of Integer, String))
                _FillingValorization.Add(New Tuple(Of Integer, String)(1, "Costo Promedio"))
                _FillingValorization.Add(New Tuple(Of Integer, String)(2, "Ultimo Costo"))
                _FillingValorization.Add(New Tuple(Of Integer, String)(3, "Precio Venta Publico"))
            End If
            Return _FillingValorization
        End Get
    End Property

    Private _FillingOrderBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingOrderBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingOrderBy Is Nothing Then
                _FillingOrderBy = New List(Of Tuple(Of Integer, String))
                _FillingOrderBy.Add(New Tuple(Of Integer, String)(1, "Código"))
                _FillingOrderBy.Add(New Tuple(Of Integer, String)(2, "Descripción"))
            End If
            Return _FillingOrderBy
        End Get
    End Property

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Agrupar"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Detallar"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(3, "Resumido"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

#End Region

#Region "BarraBotones"
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub
#End Region


#Region "Methods"

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


#Region "Selector"

    Private _selectorProduct As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorWarehouse As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorGroup As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorSubGroup As SelectorCache = New SelectorCache("Id", "Code")

    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvProduct.CustomUnboundColumnData, INDGvWarehouse.CustomUnboundColumnData, INDGvGroup.CustomUnboundColumnData, INDGvSubGroup.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvProduct" Then
                e.Value = _selectorProduct.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvWarehouse" Then
                e.Value = _selectorWarehouse.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvGroup" Then
                e.Value = _selectorGroup.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvSubGroup" Then
                e.Value = _selectorSubGroup.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub


    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvProduct.RowCellClick, INDGvWarehouse.RowCellClick, INDGvGroup.RowCellClick, INDGvSubGroup.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)

            If view.Name = "INDGvProduct" Then
                selector = _selectorProduct
            ElseIf view.Name = "INDGvWarehouse" Then
                selector = _selectorWarehouse
            ElseIf view.Name = "INDGvGroup" Then
                selector = _selectorGroup
            ElseIf view.Name = "INDGvSubGroup" Then
                selector = _selectorSubGroup
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

    Private Sub INDSle_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleProduct.Closed, INDSleWarehouse.Closed, INDSleGroup.Closed, INDSleSubGroup.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleProduct" Then
            searchLookupEdit.Properties.NullText = _selectorProduct.ToString()
        ElseIf searchLookupEdit.Name = "INDSleWarehouse" Then
            searchLookupEdit.Properties.NullText = _selectorWarehouse.ToString()
        ElseIf searchLookupEdit.Name = "INDSleGroup" Then
            searchLookupEdit.Properties.NullText = _selectorGroup.ToString()
        ElseIf searchLookupEdit.Name = "INDSleSubGroup" Then
            searchLookupEdit.Properties.NullText = _selectorSubGroup.ToString()
        End If
    End Sub

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
#End Region


#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        _FillingOrderBy = Nothing
        _FillingTypeReport = Nothing
        _FillingValorization = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportValuedInventory_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleValorization.Properties.DataSource = FillingValorization
        Me.INDGleOrderBy.Properties.DataSource = FillingOrderBy

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleValorization.EditValue = 1
        Me.INDGleOrderBy.EditValue = 1
        Me.INDsleAccountsZero.EditValue = False
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportValuedInventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGleValorization.Focus()
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
    End Sub
#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' cargamos el datasource del search del product
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>  
    Private Sub INDSleProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProduct.QueryPopUp
        If INDSleProduct.Properties.DataSource Is Nothing Then
            INDSleProduct.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListProductsReport()
        End If
    End Sub
    ''' <summary>
    ''' Cargamos el datasource del search de almacen 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.DataSource Is Nothing Then
            INDSleWarehouse.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListWarehouseReport()
        End If
    End Sub
    ''' <summary>
    ''' Cargamos el datasource del search de INDSleGroup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroup.QueryPopUp
        If INDSleGroup.Properties.DataSource Is Nothing Then
            INDSleGroup.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListGroupReport()
        End If
    End Sub
    ''' <summary>
    ''' Cargamos el datasource del search de INDSleSubGroup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSubGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSubGroup.QueryPopUp
        If INDSleSubGroup.Properties.DataSource Is Nothing Then
            INDSleSubGroup.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListSubGroupReport()
        End If
    End Sub

    ''' <summary>
    ''' popup para mostrar las monedas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If INDsleCurrency.Properties.DataSource Is Nothing Then
            INDsleCurrency.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub
#End Region

#Region "Report"
    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        AsyncLoader(True)

        _filters = New Dictionary(Of String, String)
        _filters.Add("Valorization", Valorization)
        _filters.Add("OrderBy", OrderBy)
        _filters.Add("AccountsZero", AccountsZero)
        _filters.Add("TypeReport", TypeReport)

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

        _range = New Dictionary(Of String, String)
        _range.Add("Product", _selectorProduct.GetKeys())
        _range.Add("Warehouse", _selectorWarehouse.GetKeys())
        _range.Add("Group", _selectorGroup.GetKeys())
        _range.Add("SubGroup", _selectorSubGroup.GetKeys())

        Dim xmlFilters = Utils.DictionaryToXML(_filters)
        Dim xmlRange = Utils.DictionaryToXML(_range)

        Dim reporte As New rptReportValuedInventory
        reporte.Currency = CurrencyData
        reporte.ParametrosReporte = New Object() {_filters, _range}

        Await reporte.CargarDataSourceAsync()
        AsyncLoader(False)
        If reporte.DataSource IsNot Nothing Then
            reporte.CreateDocument()
            Me.INDLcBase.Visible = False
            Me.INDCncNavigation.Visible = False
            Me.INDPcDocumentViewer.Visible = True
            INDDvDocumentViewer.Show()
            INDDvDocumentViewer.DocumentSource = reporte
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDGleValorization.Focus()
        End If

    End Sub

#Region "ToExcel"
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        Try
            AsyncLoader(True)

            Await ChargueDatasource()

            If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                generateExcel()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    Private Async Function ChargueDatasource() As Task

        _filters = New Dictionary(Of String, String)
        _filters.Add("Valorization", Valorization)
        _filters.Add("OrderBy", OrderBy)
        _filters.Add("AccountsZero", AccountsZero)
        _filters.Add("TypeReport", TypeReport)

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

        _range = New Dictionary(Of String, String)
        _range.Add("Product", _selectorProduct.GetKeys())
        _range.Add("Warehouse", _selectorWarehouse.GetKeys())
        _range.Add("Group", _selectorGroup.GetKeys())
        _range.Add("SubGroup", _selectorSubGroup.GetKeys())

        Dim xmlFilters = Utils.DictionaryToXML(_filters)
        Dim xmlRange = Utils.DictionaryToXML(_range)

        Dim IndList = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetReportValuedInventoryAsync(_filters, _range, SessionValues.Instance)

        Dim dt As New DataTable
        dt.Columns.Add("Almacen")
        dt.Columns.Add("Grupo")
        dt.Columns.Add("Subgrupo")
        dt.Columns.Add("Codigo")
        dt.Columns.Add("Descripcion")
        dt.Columns.Add("Unidad")
        dt.Columns.Add("Concentracion")
        If Valorization = 1 Then
            dt.Columns.Add("Costo Promedio", GetType(Decimal))
        ElseIf Valorization = 2 Then
            dt.Columns.Add("Ultimo Costo", GetType(Decimal))
        Else
            dt.Columns.Add("Precio Venta Publico", GetType(Decimal))
        End If
        dt.Columns.Add("Vencimiento", GetType(DateTime))
        dt.Columns.Add("Cantidad")
        dt.Columns.Add("Moneda")
        dt.Columns.Add("Valor Total", GetType(Decimal))

        If IndList.Any() Then
            For Each itemView In IndList
                Dim row As DataRow = dt.NewRow()

                row.Item("Almacen") = itemView.WarehouseCodeName
                row.Item("Grupo") = String.Concat(itemView.ProductGroupCode, " - ", itemView.ProductGroupName)
                row.Item("Subgrupo") = String.Concat(itemView.ProductSubGroupCode, " - ", itemView.ProductSubGroupName)
                row.Item("Codigo") = itemView.ProductCode
                row.Item("Descripcion") = itemView.ProductName
                row.Item("Unidad") = itemView.AdministrationUnitName
                row.Item("Concentracion") = itemView.ATCConcentration
                If Valorization = 1 Then
                    row.Item("Costo Promedio") = itemView.ProductCost
                ElseIf Valorization = 2 Then
                    row.Item("Ultimo Costo") = itemView.FinalProductCost
                Else
                    row.Item("Precio Venta Publico") = itemView.SellingPrice
                End If
                row.Item("Vencimiento") = itemView.ExpirationDate
                row.Item("Cantidad") = itemView.Quantity
                row.Item("Moneda") = itemView.CurrencyName
                row.Item("Valor Total") = IIf(Valorization = 1, itemView.Quantity * itemView.ProductCost,
                                              IIf(Valorization = 2, itemView.Quantity * itemView.FinalProductCost, itemView.Quantity * itemView.SellingPrice))
                dt.Rows.Add(row)

            Next
            INDGcExportExcell.DataSource = dt
        End If

    End Function

    Private Sub generateExcel()
        Dim _gridView = INDGcExportExcell
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcell.DataSource = Nothing
        Me.INDGcExportExcell.RefreshDataSource()
    End Sub

#End Region

#End Region


#End Region

End Class