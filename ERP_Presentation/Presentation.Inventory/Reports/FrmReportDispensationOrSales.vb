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
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmReportDispensationOrSales

#Region "Fields"
    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon
#End Region

#Region "Properties"
    Private Property ProoftCloseXpoProducts As XPInstantFeedbackSource
    Private Property ProoftCloseXpoWarehouse As XPInstantFeedbackSource
    Private Property ProoftCloseXpoDocuments As LinqInstantFeedbackSource
    Private Property ProoftCloseXpoUnitFunctional As XPInstantFeedbackSource
    Private _FillingReports As List(Of Tuple(Of Integer, String))
    Private reporte As Object
#End Region

#Region "Events"
    Private Sub FrmReportDispensationOrSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleUser.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.ConsultarUsuarioCodigo

    End Sub
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoDocuments = Nothing
        ProoftCloseXpoProducts = Nothing
        ProoftCloseXpoUnitFunctional = Nothing
        ProoftCloseXpoWarehouse = Nothing
        _FillingReports = Nothing
        reporte = Nothing
    End Sub

    Private Async Sub INDSbReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsReports() = True Then
            AsyncLoader(True)
            Select Case INDGleReport.EditValue
                Case 1
                    reporte = New rptDispensation
                Case 2
                    reporte = New rptSales
                Case 3
                    reporte = New rptTransferOrderInventory
                Case 4
                    reporte = New rptEntranceVoucherInven
                Case Else
                    reporte = New rptKardexInventory
            End Select
            reporte.ParametrosReporte = New Object() {INDDateEditDateStart.EditValue,
                                                      INDDateEditDateEnd.EditValue,
                                                      INDSleUser.EditValue,
                                                      _selectorWarehouse.GetKeys(),
                                                      _selectorProduct.GetKeys(),
                                                      _selectorFunctionalUnit.GetKeys()
                                                      }
            INDDvReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
            End If
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBase.Visible = False
                Me.INDCncReport.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateEditDateStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportListPharmacyDispensing_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        INDGleReport.Properties.DataSource = FillingReport
        'Dar un valor por defecto a los GridLookEdit
        INDGleReport.EditValue = 1

    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnReport
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleUser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocumentStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUser.QueryPopUp
        If INDSleUser.Datasource Is Nothing Then
            LoadXpoDocumentStart()
        End If
    End Sub

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

    Private ReadOnly Property FillingReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReports Is Nothing Then
                _FillingReports = New List(Of Tuple(Of Integer, String))
                _FillingReports.Add(New Tuple(Of Integer, String)(1, "Dispensación"))
                _FillingReports.Add(New Tuple(Of Integer, String)(2, "Ventas"))
                _FillingReports.Add(New Tuple(Of Integer, String)(3, "Orden de Traslado"))
                _FillingReports.Add(New Tuple(Of Integer, String)(4, "Comprobante de Entrada"))
                _FillingReports.Add(New Tuple(Of Integer, String)(5, "Kardex"))
            End If
            Return _FillingReports
        End Get
    End Property


#Region "Selector"

    Private _selectorWarehouse As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorProduct As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorFunctionalUnit As SelectorCache = New SelectorCache("Id", "Codigo")

    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvWarehouse.CustomUnboundColumnData, INDGvProduct.CustomUnboundColumnData, INDGvFuncionalUnit.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvWarehouse" Then
                e.Value = _selectorWarehouse.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvProduct" Then
                e.Value = _selectorProduct.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvFuncionalUnit" Then
                e.Value = _selectorFunctionalUnit.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvWarehouse.RowCellClick, INDGvProduct.RowCellClick, INDGvFuncionalUnit.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)

            If view.Name = "INDGvWarehouse" Then
                selector = _selectorWarehouse
            ElseIf view.Name = "INDGvProduct" Then
                selector = _selectorProduct
            ElseIf view.Name = "INDGvFuncionalUnit" Then
                selector = _selectorFunctionalUnit
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
    Private Sub INDSle_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleWarehouse.Closed, INDSleProduct.Closed, INDSleFunctionalUnit.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleWarehouse" Then
            searchLookupEdit.Properties.NullText = _selectorWarehouse.ToString()
        ElseIf searchLookupEdit.Name = "INDSleProduct" Then
            searchLookupEdit.Properties.NullText = _selectorProduct.ToString()
        ElseIf searchLookupEdit.Name = "INDSleFunctionalUnit" Then
            searchLookupEdit.Properties.NullText = _selectorFunctionalUnit.ToString()
        End If
    End Sub

#End Region


#Region "QueryPoup"

    ''' <summary>
    ''' Cargamos el datasource del search de almacen destino
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
    ''' Cargamos el datasource del search de unidad funcional 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleFunctionalUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFunctionalUnit.QueryPopUp
        If INDSleFunctionalUnit.Properties.DataSource Is Nothing Then
            INDSleFunctionalUnit.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.ListFunctionalUnit(True)
        End If
    End Sub
#End Region


    '' <summary>
    '' metodo para Cargar el data source Del Control INDSleUnitFunctionalStart
    '' </summary>
    '' <remarks></remarks>
    'Private Sub LoadXpoUnitFunctionalStart()
    '    Using msearch As New MBusqueda
    '        ProoftCloseXpoUnitFunctional = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.FunctionalUnit)
    '        INDSleUnitFunctionalStart.Datasource = ProoftCloseXpoUnitFunctional
    '    End Using
    'End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleUser
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoDocumentStart()
        Dim criteria As String = Nothing

        Using msearch As New MBusqueda
            ProoftCloseXpoDocuments = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCreationUsersReportTreasury)
            INDSleUser.Datasource = ProoftCloseXpoDocuments
        End Using
    End Sub
#End Region

#Region "Validate"
    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()

        Dim Validations As Boolean = True

        'validaciones controles de Fechas
        If INDDateEditDateStart.EditValue Is Nothing Or INDDateEditDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateEditDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateEditDateStart.EditValue > INDDateEditDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDateEditDateStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function


    Private Sub INDGleReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleReport.EditValueChanged
        If INDGleReport.EditValue = 1 Then
            INDLblWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLblProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLblFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            _selectorFunctionalUnit = New SelectorCache("Id", "Codigo")
        ElseIf INDGleReport.EditValue = 2 Then
            INDLblWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLblProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLblFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            _selectorFunctionalUnit = New SelectorCache("Id", "Codigo")
        ElseIf INDGleReport.EditValue = 3 Then
            INDLblWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLblProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLblFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        ElseIf INDGleReport.EditValue = 4 Then
            INDLblWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLblProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLblFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            _selectorFunctionalUnit = New SelectorCache("Id", "Codigo")
        ElseIf INDGleReport.EditValue = 5 Then
            INDLblWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLblProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLblFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            _selectorFunctionalUnit = New SelectorCache("Id", "Codigo")
        End If
    End Sub

#End Region
End Class