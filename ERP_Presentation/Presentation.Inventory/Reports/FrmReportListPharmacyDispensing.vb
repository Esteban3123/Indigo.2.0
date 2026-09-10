#Region "Imports"

Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Reporter

#End Region

Public Class FrmReportListPharmacyDispensing

#Region "Properties"

    Public Property ProoftCloseXpoProducts As XPInstantFeedbackSource
    Public Property ProoftCloseXpoWarehouse As XPInstantFeedbackSource
    Public Property ProoftCloseXpoDocuments As XPInstantFeedbackSource

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Detallado"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(3, "Consolidado"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

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

#End Region

#Region "Events"

#Region "Load"

    Private Sub FrmReportListPharmacyDispensing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ProoftCloseXpoDocuments = Nothing
        ProoftCloseXpoProducts = Nothing
        ProoftCloseXpoWarehouse = Nothing
        _FillingTypeReport = Nothing
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportListPharmacyDispensing_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        INDGlueTypeReport.Properties.DataSource = FillingTypeReport
        'Dar un valor por defecto a los GridLookEdit
        INDGlueTypeReport.EditValue = 1
    End Sub

#End Region

#Region "QueryPopup"

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
    '_______________________________________________________________________________________________________________________________________
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleDocument
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDSleDocument_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleDocument.QueryPopUp
        If INDSleDocument.Properties.DataSource Is Nothing Then
            INDSleDocument.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListPharmaceuticalDispensing()
        End If
    End Sub

#End Region

#Region "Selector"

    Private _selectorWarehouse As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorProduct As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorDocument As SelectorCache = New SelectorCache("Id", "Code")

    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvWarehouse.CustomUnboundColumnData, INDGvProduct.CustomUnboundColumnData, INDGvDocument.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvWarehouse" Then
                e.Value = _selectorWarehouse.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvProduct" Then
                e.Value = _selectorProduct.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvDocument" Then
                e.Value = _selectorDocument.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvWarehouse.RowCellClick, INDGvProduct.RowCellClick, INDGvDocument.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)

            If view.Name = "INDGvWarehouse" Then
                selector = _selectorWarehouse
            ElseIf view.Name = "INDGvProduct" Then
                selector = _selectorProduct
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
    Private Sub INDSle_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleWarehouse.Closed, INDSleProduct.Closed, INDSleDocument.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleWarehouse" Then
            searchLookupEdit.Properties.NullText = _selectorWarehouse.ToString()
        ElseIf searchLookupEdit.Name = "INDSleProduct" Then
            searchLookupEdit.Properties.NullText = _selectorProduct.ToString()
        ElseIf searchLookupEdit.Name = "INDSleDocument" Then
            searchLookupEdit.Properties.NullText = _selectorDocument.ToString()
        End If
    End Sub

#End Region

#Region "Report"

    Private Async Sub INDSbReport_Click(sender As Object, e As EventArgs) Handles INDSbReport.Click
        If ValidateControlsReports() = True Then
            If INDGlueTypeReport.EditValue = 1 Then
                AsyncLoader(True)
                Dim reporte As New rptListPharmaceuticalDispensing
                reporte.ParametrosReporte = New Object() {INDDateEditDateStart.EditValue,
                                                          INDDateEditDateEnd.EditValue,
                                                          _selectorWarehouse.GetKeys(),
                                                          _selectorProduct.GetKeys(),
                                                         _selectorDocument.GetKeys()
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
            ElseIf INDGlueTypeReport.EditValue = 2 Then
                AsyncLoader(True)
                Dim reporte As New rptSubListPharmaceuticalDispensing
                reporte.ParametrosReporte = New Object() {INDDateEditDateStart.EditValue,
                                                          INDDateEditDateEnd.EditValue,
                                                          _selectorWarehouse.GetKeys(),
                                                          _selectorProduct.GetKeys(),
                                                         _selectorDocument.GetKeys()}
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
            ElseIf INDGlueTypeReport.EditValue = 3 Then
                AsyncLoader(True)
                Dim reporte As New rptConsolidatedPharmaceuticalDispensing
                reporte.ParametrosReporte = New Object() {INDDateEditDateStart.EditValue,
                                                          INDDateEditDateEnd.EditValue,
                                                          _selectorWarehouse.GetKeys(),
                                                          _selectorProduct.GetKeys(),
                                                         _selectorDocument.GetKeys()
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
        End If
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

#End Region

#End Region

End Class