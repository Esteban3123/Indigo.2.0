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

Public Class FrmRemissionNotLegalized

#Region "Fields"


#End Region

#Region "Datasource"

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Remision Entrada"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Remision Salida"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(3, "Remision de Inventario en Consignación"))
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
        If INDDeDateStart.EditValue Is Nothing Or INDDeDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        ElseIf Me.INDDeDateStart.EditValue > INDDeDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        End If

        Return Validations

    End Function

#Region "Selector"
    ''' <summary>
    ''' funcionamiento del Multiselect
    ''' </summary>
    Private _selectorProduct As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorSupplier As SelectorCache = New SelectorCache("ThirdPartyId", "ThirdPartyNit")
    Private _selectorCustomer As SelectorCache = New SelectorCache("Id", "Nit")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvProduct.CustomUnboundColumnData, INDGvSupplier.CustomUnboundColumnData, INDGvCustomer.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvProduct" Then
                e.Value = _selectorProduct.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvSupplier" Then
                e.Value = _selectorSupplier.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvCustomer" Then
                e.Value = _selectorCustomer.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvProduct.RowCellClick, INDGvSupplier.RowCellClick, INDGvCustomer.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvProduct" Then
                selector = _selectorProduct

            ElseIf view.Name = "INDGvSupplier" Then
                selector = _selectorSupplier

            ElseIf view.Name = "INDGvCustomer" Then
                selector = _selectorCustomer

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

    Private Sub INDGvSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleProduct.Closed, INDSleSupplier.Closed, INDSleCustomer.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleProduct" Then
            searchLookupEdit.Properties.NullText = _selectorProduct.ToString()

        ElseIf searchLookupEdit.Name = "INDSleSupplier" Then
            searchLookupEdit.Properties.NullText = _selectorSupplier.ToString()

        ElseIf searchLookupEdit.Name = "INDSleCustomer" Then
            searchLookupEdit.Properties.NullText = _selectorCustomer.ToString()

        End If
    End Sub


#End Region
#End Region



#Region "Load"

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmRemissionNotLegalized_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        INDDeDateStart.Focus()

        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleProductStart
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



    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCustomer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCustomer_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCustomer.QueryPopUp
        If INDSleCustomer.Properties.DataSource Is Nothing Then
            INDSleCustomer.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListCustomerInventoryReport()
        End If
    End Sub


#End Region

#Region "EditValueChange"

    ''' <summary>
    ''' se ejecuta en el evento editValueChanged del control indgletypereport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged

        If Not INDGleTypeReport.EditValue = Nothing Then
            'si tipo de reporte es igual a Remision Entrada mostrar el filtro por proveedores
            If INDGleTypeReport.EditValue = 1 Then
                INDLcSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLcCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never


                'vaciar campos
                _selectorCustomer.Clear()
                INDSleCustomer.Text = ""

                'si tipo de reporte es igual a Remision salida mostrar el filtro por cliente
            ElseIf INDGleTypeReport.EditValue = 2 Then
                INDLcSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLcCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                'vaciar campos
                _selectorSupplier.Clear()
                INDSleSupplier.Text = ""

                'si tipo de reporte es igual a Remision de Inventario en Consignación mostrar el filtro por proveedores
            ElseIf INDGleTypeReport.EditValue = 3 Then
                INDLcSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLcCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                'vaciar campos
                _selectorCustomer.Clear()
                INDSleCustomer.Text = ""

            End If
        End If

    End Sub
#End Region


#Region "handlers"
    ''' <summary>
    ''' Evento que al dar clic se procede a generar el archivo de excel, de acuerdo a los parametros indicados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbExportData_Click(sender As Object, e As EventArgs) Handles INDSbExportData.Click
        If Me.ValidateControlsReports = True Then
            Dim reporte = Await Task.Run(Function() GetDataTypeReport(INDGleTypeReport.EditValue))
            If reporte IsNot Nothing AndAlso DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Select Case INDGleTypeReport.EditValue
                    Case 1
                        INDGcExportDataEntry.DataSource = Nothing
                        INDGcExportDataEntry.DataSource = reporte.DataSource
                        GenerateExcel(INDGcExportDataEntry)
                    Case 2
                        INDGcExportDataExit.DataSource = Nothing
                        INDGcExportDataExit.DataSource = reporte.DataSource
                        GenerateExcel(INDGcExportDataExit)
                    Case 3
                        INDGcExportDataConsignmentInventory.DataSource = Nothing
                        INDGcExportDataConsignmentInventory.DataSource = reporte.DataSource
                        GenerateExcel(INDGcExportDataConsignmentInventory)
                End Select
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            End If
        End If
    End Sub

#End Region


#Region "Methods"
    ''' <summary>
    ''' Devuelve la data dependiendo del tipo de reporte seleccionado
    ''' </summary>
    ''' <param name="typeReport"></param>
    Private Function GetDataTypeReport(ByVal typeReport As Integer) As Object
        Dim reporte As New Object

        Select Case INDGleTypeReport.EditValue
            Case 1
                reporte = New rptRemissionNotLegalizedEntrance()
                reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                             INDDeDateEnd.EditValue,
                                             _selectorProduct.GetKeys(),
                                             _selectorSupplier.GetKeys()}
                reporte.CargarDataSource()
            Case 2
                reporte = New rptRemissionNotLegalizedOutput()
                reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                      INDDeDateEnd.EditValue,
                                                      _selectorProduct.GetKeys(),
                                                      _selectorCustomer.GetKeys()}
                reporte.CargarDataSource()
            Case 3
                reporte = New rptRemissionNotLegalizedConsignment()
                reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                              INDDeDateEnd.EditValue,
                                              _selectorProduct.GetKeys(),
                                              _selectorSupplier.GetKeys()}
                reporte.CargarDataSource()
        End Select
        Return reporte
    End Function


    ''' <summary>
    ''' Metodo para generar el archivo en excel
    ''' </summary>
    Private Sub GenerateExcel(ByVal _gridControl As Object)
        If _gridControl IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridControl.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
    End Sub

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

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingTypeReport = Nothing
    End Sub

    ''' <summary>
    ''' Metodo refactorizado, genera reporte para visualizar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click

        If Me.ValidateControlsReports = True Then
            Dim reporte = Await Task.Run(Function() GetDataTypeReport(INDGleTypeReport.EditValue))
            AsyncLoader(True)
            INDDvDocumentViewer.DocumentSource = reporte
            reporte.CargarDataSource()
            reporte.CreateDocument(True)
            AsyncLoader(False)

            If reporte IsNot Nothing AndAlso DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDeDateStart.Focus()
            End If
        End If

    End Sub


#End Region




End Class