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
Imports Domain.Entities

#End Region

Public Class FrmReportEntranceVoucher

#Region "Fields"
    ''' <summary>
    ''' obtiene o establece la informacion de la moneda
    ''' </summary>
    Private Currency As Currency

    ''' <summary>
    ''' obtiene la moneda seleccionada en el campo moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId As Integer
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrency.EditValue = value
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
    Public Property ProoftCloseXpoDocuments As XPInstantFeedbackSource
    Public Property ProoftCloseXpoSupplier As XPInstantFeedbackSource
    Private _FillingGroupBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingGroupBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGroupBy Is Nothing Then
                _FillingGroupBy = New List(Of Tuple(Of Integer, String))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(1, "Proveedor"))
                _FillingGroupBy.Add(New Tuple(Of Integer, String)(2, "Fecha"))
            End If
            Return _FillingGroupBy
        End Get
    End Property

    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Registrados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Confirmados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Anulados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Detallado"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Resumido"))
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

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()

        Dim Validations As Boolean = True
        If INDDeDateStart.EditValue Is Nothing OrElse INDDeDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False

        ElseIf Me.INDDeDateStart.EditValue > INDDeDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False

        ElseIf TypeReport = 1 AndAlso CurrencyId <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CurrencyVoid", "Commons"))
            Me.INDsleCurrency.Focus()
            Validations = False
        End If

        Return Validations
    End Function

#Region "selector"

    ''' <summary>
    ''' funcionamiento del Multiselect
    ''' </summary>
    Private _selectorSupplier As SelectorCache = New SelectorCache("ThirdPartyId", "ThirdPartyNit")
    Private _selectorDocument As SelectorCache = New SelectorCache("Id", "Code")


    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvSupplier.CustomUnboundColumnData, INDGvDocument.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)

            If view.Name = "INDGvSupplier" Then
                e.Value = _selectorSupplier.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvDocument" Then
                e.Value = _selectorDocument.ValidateExistsRow(e.Row)

            End If

        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvSupplier.RowCellClick, INDGvDocument.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)

            If view.Name = "INDGvSupplier" Then
                selector = _selectorSupplier

            ElseIf view.Name = "INDGvDocument" Then
                selector = _selectorDocument

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

    Private Sub INDGvSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleSupplier.Closed, INDSleDocument.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleSupplier" Then
            searchLookupEdit.Properties.NullText = _selectorSupplier.ToString()

        ElseIf searchLookupEdit.Name = "INDSleDocument" Then
            searchLookupEdit.Properties.NullText = _selectorDocument.ToString()

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

#End Region

#Region "Events"

#Region "Load"


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingGroupBy = Nothing
        _FillingStatus = Nothing
        _FillingTypeReport = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportEntranceVoucher_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleGroupBy.Properties.DataSource = FillingGroupBy
        Me.INDGleStatus.Properties.DataSource = FillingStatus
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleGroupBy.EditValue = 1
        Me.INDGleStatus.EditValue = 4
        Me.INDGleTypeReport.EditValue = 1
    End Sub

    ''' <summary>
    '''  Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportEntranceVoucher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDDeDateStart.Focus()
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleDocument
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocument_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleDocument.QueryPopUp
        If INDSleDocument.Properties.DataSource Is Nothing Then

            INDSleDocument.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListEntranceVoucherReportByFilter(Nothing)

        End If
    End Sub


    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleSupplier
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSupplier.QueryPopUp

        If INDSleSupplier.Properties.DataSource Is Nothing Then
            INDSleSupplier.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListSupplierInventoryReport()
        End If

    End Sub


#End Region

#Region "EditValueChange"

#End Region

#Region "Report"

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
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            If TypeReport = 1 Then 'Detallado
                AsyncLoader(True)

                If CurrencyId > 0 Then
                    Using Model As New MCurrency(MyBase.Tag)
                        Currency = Await Model.GetCurrencyById(CurrencyId)
                    End Using
                End If

                Dim reporte As New rptSubEntranceVoucher
                reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                          INDDeDateEnd.EditValue,
                                                          _selectorDocument.GetKeys(),
                                                          _selectorSupplier.GetKeys(),
                                                          INDGleGroupBy.EditValue,
                                                          INDGleStatus.EditValue,
                                                          TypeReport
                                                          }
                reporte.Currency = Currency
                INDDvDocumentViewer.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                reporte.CreateDocument(True)
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcDocumentViewer.Visible = True
                    INDDvDocumentViewer.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDeDateStart.Focus()
                End If

            Else

                AsyncLoader(True)
                Dim reporte As New rptReportEntranceVoucher
                reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                          INDDeDateEnd.EditValue,
                                                         _selectorDocument.GetKeys(),
                                                          _selectorSupplier.GetKeys(),
                                                          INDGleGroupBy.EditValue,
                                                          INDGleStatus.EditValue,
                                                          TypeReport}
                INDDvDocumentViewer.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                reporte.CreateDocument(True)
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcDocumentViewer.Visible = True
                    INDDvDocumentViewer.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDeDateStart.Focus()
                End If

            End If

        End If
    End Sub

#Region "ToExcel"
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        Try
            If Me.ValidateControlsReports Then
                AsyncLoader(True)

                Await ChargueDatasource()

                If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                    generateExcel()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                End If
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    Private Async Function ChargueDatasource() As Task

        Dim reporte As New rptReportEntranceVoucher
        reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                    INDDeDateEnd.EditValue,
                                                    _selectorDocument.GetKeys(),
                                                    _selectorSupplier.GetKeys(),
                                                    INDGleGroupBy.EditValue,
                                                    INDGleStatus.EditValue,
                                                    TypeReport}
        INDDvDocumentViewer.DocumentSource = reporte
        Await reporte.CargarDataSourceAsync()
        Dim IndList = reporte.DataSource

        Dim dt As New DataTable
        dt.Columns.Add("Codigo")
        dt.Columns.Add("Proovedor")
        dt.Columns.Add("Fecha Documento", GetType(DateTime))
        dt.Columns.Add("Moneda")
        dt.Columns.Add("V/Neto", GetType(Decimal))
        dt.Columns.Add("V/Descuento", GetType(Decimal))
        dt.Columns.Add("V/Impuestos", GetType(Decimal))
        dt.Columns.Add("V/Total", GetType(Decimal))
        dt.Columns.Add("Estado Documento")

          If DirectCast(IndList, ICollection).Count > 0 Then
            For Each itemView In IndList
                Dim row As DataRow = dt.NewRow()

                row.Item("Codigo") = itemView.Code
                row.Item("Proovedor") = String.Concat(itemView.SupplierId.Code, " - ", itemView.SupplierId.Name)
                row.Item("Fecha Documento") = itemView.DocumentDate
                row.Item("Moneda") = itemView.Currency.Name
                row.Item("V/Neto") = itemView.Value
                row.Item("V/Descuento") = itemView.ValueDiscount
                row.Item("V/Impuestos") = itemView.ValueTax
                row.Item("V/Total") = itemView.TotalValue
                row.Item("Estado Documento") = itemView.StatusName
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

    ''' <summary>
    ''' envento que muestra o oculta el campo de moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDGleTypeReport.EditValueChanged
        If TypeReport = 1 Then
            INDlyCurrency.Visibility = False
            LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            INDlyCurrency.Visibility = True
            LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

#End Region


#End Region

End Class