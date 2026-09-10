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
Imports System.Dynamic

#End Region

Public Class FrmReportPhysicalInventory

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"
    Public Property ProoftCloseXpoProducts As XPInstantFeedbackSource
    Public Property ProoftCloseXpoWarehouse As XPInstantFeedbackSource

    Private _FillingType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingType Is Nothing Then
                _FillingType = New List(Of Tuple(Of Integer, String))
                _FillingType.Add(New Tuple(Of Integer, String)(1, "Inventario Físico"))
                _FillingType.Add(New Tuple(Of Integer, String)(2, "Inventario en Custodia"))
            End If
            Return _FillingType
        End Get
    End Property

    Private _FillingCriteria As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingCriteria As List(Of Tuple(Of Integer, String))
        Get
            If _FillingCriteria Is Nothing Then
                _FillingCriteria = New List(Of Tuple(Of Integer, String))
                _FillingCriteria.Add(New Tuple(Of Integer, String)(1, "Almacén"))
                _FillingCriteria.Add(New Tuple(Of Integer, String)(2, "Producto"))
            End If
            Return _FillingCriteria
        End Get
    End Property

    Private _FillingCriteriaCero As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingCriteriaCero As List(Of Tuple(Of Integer, String))
        Get
            If _FillingCriteriaCero Is Nothing Then
                _FillingCriteriaCero = New List(Of Tuple(Of Integer, String))
                _FillingCriteriaCero.Add(New Tuple(Of Integer, String)(1, "Si"))
                _FillingCriteriaCero.Add(New Tuple(Of Integer, String)(2, "No"))
            End If
            Return _FillingCriteriaCero
        End Get
    End Property

    Private _FillingOrderBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingOrderBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingOrderBy Is Nothing Then
                _FillingOrderBy = New List(Of Tuple(Of Integer, String))
                _FillingOrderBy.Add(New Tuple(Of Integer, String)(1, "Codigo"))
                _FillingOrderBy.Add(New Tuple(Of Integer, String)(2, "Producto"))
            End If
            Return _FillingOrderBy
        End Get
    End Property

    Private _FillingWarehouseByRowOrColumn As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingWarehouseByRowOrColumn As List(Of Tuple(Of Integer, String))
        Get
            If _FillingWarehouseByRowOrColumn Is Nothing Then
                _FillingWarehouseByRowOrColumn = New List(Of Tuple(Of Integer, String))
                _FillingWarehouseByRowOrColumn.Add(New Tuple(Of Integer, String)(1, "Fila"))
                _FillingWarehouseByRowOrColumn.Add(New Tuple(Of Integer, String)(2, "Columna"))
            End If
            Return _FillingWarehouseByRowOrColumn
        End Get
    End Property

    Private _FillingBatchSerial As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingBatchSerial As List(Of Tuple(Of Integer, String))
        Get
            If _FillingBatchSerial Is Nothing Then
                _FillingBatchSerial = New List(Of Tuple(Of Integer, String))
                _FillingBatchSerial.Add(New Tuple(Of Integer, String)(1, "Si"))
                _FillingBatchSerial.Add(New Tuple(Of Integer, String)(2, "No"))
            End If
            Return _FillingBatchSerial
        End Get
    End Property
#End Region

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

        'validaciones controles de producto
        If INDSleProductsStart.EditValue IsNot Nothing And INDSleProductsEnd.EditValue Is Nothing Or INDSleProductsStart.EditValue Is Nothing And INDSleProductsEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblProducts.Text)
            Me.INDSleProductsStart.Focus()
            Validations = False
        ElseIf INDSleProductsStart.EditValue > INDSleProductsEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblProducts.Text)
            Me.INDSleProductsStart.Focus()
            Validations = False
        End If

        'validaciones controles de almacenes
        If INDSleWarehouseStart.EditValue IsNot Nothing And INDSleWarehouseEnd.EditValue Is Nothing Or INDSleWarehouseStart.EditValue Is Nothing And INDSleWarehouseEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblWarehouse.Text)
            Me.INDSleWarehouseStart.Focus()
            Validations = False
        ElseIf INDSleWarehouseStart.EditValue > INDSleWarehouseEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblWarehouse.Text)
            Me.INDSleWarehouseStart.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoProductStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoProducts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductsReport)
            INDSleProductsStart.Datasource = ProoftCloseXpoProducts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleProductEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoProductEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoProducts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductsReport)
            INDSleProductsEnd.Datasource = ProoftCloseXpoProducts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleWarehouseStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoWarehouseStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoWarehouse = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListWarehouseReport)
            INDSleWarehouseStart.Datasource = ProoftCloseXpoWarehouse
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleWarehouseEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoWarehouseEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoWarehouse = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListWarehouseReport)
            INDSleWarehouseEnd.Datasource = ProoftCloseXpoWarehouse
        End Using
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleWarehouseStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleWarehouseStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouseStart.QueryPopUp
        If INDSleWarehouseStart.Datasource Is Nothing Then
            LoadXpoWarehouseStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleWarehouseEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleWarehouseEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouseEnd.QueryPopUp
        If INDSleWarehouseEnd.Datasource Is Nothing Then
            LoadXpoWarehouseEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleProductStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductsStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductsStart.QueryPopUp
        If INDSleProductsStart.Datasource Is Nothing Then
            LoadXpoProductStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleProductEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleProductsEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProductsEnd.QueryPopUp
        If INDSleProductsEnd.Datasource Is Nothing Then
            LoadXpoProductEnd()
        End If
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

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoProducts = Nothing
        ProoftCloseXpoWarehouse = Nothing
        _FillingType = Nothing
        _FillingCriteria = Nothing
        _FillingCriteriaCero = Nothing
        _FillingOrderBy = Nothing
        _FillingWarehouseByRowOrColumn = Nothing
        _FillingBatchSerial = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            If INDGleType.EditValue = 1 Then
                If Me.INDGleRowOrColumn.EditValue = 1 Then
                    AsyncLoader(True)
                    Dim reporte As New rptPhysicalInventory
                    reporte.ParametrosReporte = New Object() {INDSleWarehouseStart.EditValue,
                                                              INDSleWarehouseEnd.EditValue,
                                                              INDSleProductsStart.EditValue,
                                                              INDSleProductsEnd.EditValue,
                                                              INDGleGroupBy.EditValue,
                                                              INDGleCero.EditValue,
                                                              INDGleOrderBy.EditValue}
                    INDDvDocumentViewer.DocumentSource = reporte
                    reporte.CargarDataSource()
                    reporte.CreateDocument(True)
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcDocumentViewer.Visible = True
                        INDDvDocumentViewer.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDSleWarehouseStart.Focus()
                    End If
                Else
                    AsyncLoader(True)
                    Dim reporte As New rptPhysicalInventoryByColumn
                    reporte.ParametrosReporte = New Object() {INDSleWarehouseStart.EditValue,
                                                              INDSleWarehouseEnd.EditValue,
                                                              INDSleProductsStart.EditValue,
                                                              INDSleProductsEnd.EditValue,
                                                              INDGleGroupBy.EditValue,
                                                              INDGleCero.EditValue,
                                                              INDGleOrderBy.EditValue}
                    INDDvDocumentViewer.DocumentSource = reporte
                    reporte.CargarDataSource()
                    reporte.CreateDocument(True)
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcDocumentViewer.Visible = True
                        INDDvDocumentViewer.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDSleWarehouseStart.Focus()
                    End If
                End If
            Else
                AsyncLoader(True)
                Dim reporte As New rptPhysicalInventoryCustody
                reporte.ParametrosReporte = New Object() {INDSleWarehouseStart.EditValue,
                                                          INDSleWarehouseEnd.EditValue,
                                                          INDSleProductsStart.EditValue,
                                                          INDSleProductsEnd.EditValue,
                                                          INDGleGroupBy.EditValue,
                                                          INDGleCero.EditValue,
                                                          INDGleOrderBy.EditValue}
                INDDvDocumentViewer.DocumentSource = reporte
                reporte.CargarDataSource()
                reporte.CreateDocument(True)
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcDocumentViewer.Visible = True
                    INDDvDocumentViewer.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDSleWarehouseStart.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    '''  Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        INDSleWarehouseStart.Focus()

        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)

        Me.INDSleProductsStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetInventoryProduct
        Me.INDSleProductsEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetInventoryProduct
        Me.INDSleWarehouseStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetWarehouse
        Me.INDSleWarehouseEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetWarehouse

        INDSleProductsStart.View.OptionsView.ShowGroupPanel = False
        INDSleProductsEnd.View.OptionsView.ShowGroupPanel = False
        INDSleWarehouseStart.View.OptionsView.ShowGroupPanel = False
        INDSleWarehouseEnd.View.OptionsView.ShowGroupPanel = False

    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportPhysicalInventory_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDGleType.Properties.DataSource = FillingType
        Me.INDGleGroupBy.Properties.DataSource = FillingCriteria
        Me.INDGleCero.Properties.DataSource = FillingCriteriaCero
        Me.INDGleOrderBy.Properties.DataSource = FillingOrderBy
        Me.INDGleRowOrColumn.Properties.DataSource = FillingWarehouseByRowOrColumn
        Me.INDGleBatchSerial.Properties.DataSource = FillingBatchSerial

        Me.INDGleType.EditValue = 1
        Me.INDGleGroupBy.EditValue = 2
        Me.INDGleCero.EditValue = 2
        Me.INDGleOrderBy.EditValue = 1
        Me.INDGleRowOrColumn.EditValue = 1
        INDGleBatchSerial.EditValue = 1
    End Sub
    Private Sub INDGleGroupBy_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleGroupBy.EditValueChanged
        If Me.INDGleGroupBy.EditValue = 1 Then
            Me.INDGleRowOrColumn.EditValue = 1
            INDLciRowOrColumn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.INDGleRowOrColumn.EditValue = 1
            INDLciRowOrColumn.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    Private Sub INDGleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleType.EditValueChanged
        Me.INDGleRowOrColumn.EditValue = 1
        INDLciRowOrColumn.Visibility = IIf(INDGleType.EditValue = 1, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
    End Sub

    Private Sub INDGleRowOrColumn_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleRowOrColumn.EditValueChanged
        If Me.INDGleRowOrColumn.EditValue = 1 Then
            INDLcCero.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciOrderBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        ElseIf Me.INDGleRowOrColumn.EditValue = 2 Then
            INDLcCero.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciOrderBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Dim filtroConsulta As String = ""
            Dim IndigoSessionValues As SessionValues = SessionValues.Instance
            If INDGleType.EditValue = 1 Then
                'si filtra por almacenes
                If INDSleWarehouseStart.EditValue IsNot Nothing And INDSleWarehouseEnd.EditValue IsNot Nothing Then
                    If filtroConsulta = "" Then
                        filtroConsulta &= "WarehouseId.Code >= '" & INDSleWarehouseStart.EditValue & "' AND WarehouseId.Code <= '" & INDSleWarehouseEnd.EditValue & "'"
                    Else
                        filtroConsulta &= " AND WarehouseId.Code >= '" & INDSleWarehouseStart.EditValue & "' AND WarehouseId.Code <= '" & INDSleWarehouseEnd.EditValue & "'"
                    End If
                End If

                ' si filtra por productos
                If INDSleProductsStart.EditValue IsNot Nothing And INDSleProductsEnd.EditValue IsNot Nothing Then
                    If filtroConsulta = "" Then
                        filtroConsulta &= "ProductId.Code >= '" & INDSleProductsStart.EditValue & "' AND ProductId.Code <= '" & INDSleProductsEnd.EditValue & "'"
                    Else
                        filtroConsulta &= " AND ProductId.Code >= '" & INDSleProductsStart.EditValue & "' AND ProductId.Code <= '" & INDSleProductsEnd.EditValue & "'"
                    End If
                End If

                If INDGleCero.EditValue = 2 Then
                    If filtroConsulta = "" Then
                        filtroConsulta &= "Quantity >  '0' "
                    Else
                        filtroConsulta &= " AND Quantity > '0' "
                    End If
                End If


                'Organiza el filtro por Codigo ó Producto
                Dim finalData As IEnumerable(Of Object) = New List(Of Object)
                Dim filterOrder As IEnumerable(Of Object) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of InventoryPhysicalInventoryReportXpo)(Nothing, filtroConsulta)
                If INDGleOrderBy.EditValue = 1 Then
                    filterOrder = filterOrder.OrderBy(Function(x) x.ProductId.Code).ToList
                Else
                    filterOrder = filterOrder.OrderBy(Function(x) x.ProductId.Name).ToList
                End If
                ''si se muestra o no el lote
                If INDGleBatchSerial.EditValue = 2 Then

                    finalData = From item In filterOrder
                                Group By ProductName = item.ProductId.Name,
                                                                       ProductCode = item.ProductId.Code,
                                                                       WarehouseCode = item.WarehouseId.Code,
                                                                       WarehouseName = item.WarehouseId.Name
                                                                   Into mygroup = Group, SumQuantity = Sum(CDec(item.Quantity))
                                Select New With {Key .ProductCode = ProductCode,
                                                                               Key .ProductName = ProductName,
                                                                               Key .WarehouseCode = WarehouseCode,
                                                                               Key .WarehouseName = WarehouseName,
                                                                               Key .Quantity = SumQuantity}

                    ''se cambian labels segun el agrupador
                    INDColBatchSerial.Visible = False
                    INDColExpirationDate.Visible = False
                    If INDGleGroupBy.EditValue = 1 Then
                        INDColCode.Caption = "Código Producto"
                        INDCcolName.Caption = "Nombre Producto"
                        INDColGroup.Caption = "Almacén"

                        INDColCode.FieldName = "ProductCode"
                        INDCcolName.FieldName = "ProductName"
                        INDColGroup.UnboundExpression = "[WarehouseCode] + ' - ' + [WarehouseName]"
                    Else
                        INDColCode.Caption = "Código Almacén"
                        INDCcolName.Caption = "Nombre Almacén"

                        INDColCode.FieldName = "WarehouseCode"
                        INDCcolName.FieldName = "WarehouseName"

                        INDColGroup.Caption = "Producto"
                        INDColGroup.UnboundExpression = "[ProductCode] + ' - ' + [ProductName]"
                    End If

                Else
                    finalData = filterOrder
                    ''se cambian labels segun el agrupador
                    If INDGleGroupBy.EditValue = 1 Then
                        INDColCode.Caption = "Código Producto"
                        INDCcolName.Caption = "Nombre Producto"
                        INDColGroup.Caption = "Almacén"

                        INDColCode.FieldName = "ProductId.Code"
                        INDCcolName.FieldName = "ProductId.Name"
                        INDColBatchSerial.FieldName = "BatchSerialId.BatchCode"
                        INDColExpirationDate.FieldName = "BatchSerialId.ExpirationDate"
                        INDColGroup.UnboundExpression = "[WarehouseId.Code] + ' - ' + [WarehouseId.Name]"
                    Else
                        INDColCode.Caption = "Código Almacén"
                        INDCcolName.Caption = "Nombre Almacén"

                        INDColCode.FieldName = "WarehouseId.Code"
                        INDCcolName.FieldName = "WarehouseId.Name"
                        INDColBatchSerial.FieldName = "BatchSerialId.BatchCode"
                        INDColExpirationDate.FieldName = "BatchSerialId.ExpirationDate"

                        INDColGroup.Caption = "Producto"
                        INDColGroup.UnboundExpression = "[ProductId.Code] + ' - ' + [ProductId.Name]"
                    End If
                End If


                INDgcExportExcel.DataSource = finalData
                GenerateExcel()
            Else
                'si filtra por almacenes
                If INDSleWarehouseStart.EditValue IsNot Nothing And INDSleWarehouseEnd.EditValue IsNot Nothing Then
                    If filtroConsulta = "" Then
                        filtroConsulta &= "WarehouseCode >= '" & INDSleWarehouseStart.EditValue & "' AND WarehouseCode <= '" & INDSleWarehouseEnd.EditValue & "'"
                    Else
                        filtroConsulta &= " AND WarehouseCode >= '" & INDSleWarehouseStart.EditValue & "' AND WarehouseCode <= '" & INDSleWarehouseEnd.EditValue & "'"
                    End If
                End If

                ' si filtra por productos
                If INDSleProductsStart.EditValue IsNot Nothing And INDSleProductsEnd.EditValue IsNot Nothing Then
                    If filtroConsulta = "" Then
                        filtroConsulta &= "ProductCode >= '" & INDSleProductsStart.EditValue & "' AND ProductCode <= '" & INDSleProductsEnd.EditValue & "'"
                    Else
                        filtroConsulta &= " AND ProductCode >= '" & INDSleProductsStart.EditValue & "' AND ProductCode <= '" & INDSleProductsEnd.EditValue & "'"
                    End If
                End If

                If INDGleCero.EditValue = 2 Then
                    If filtroConsulta = "" Then
                        filtroConsulta &= "Quantity >  '0' "
                    Else
                        filtroConsulta &= " AND Quantity > '0' "
                    End If
                End If


                'Organiza el filtro por Codigo ó Producto
                Dim finalData As IEnumerable(Of Object) = New List(Of Object)
                Dim filterOrder As IEnumerable(Of Object) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of ViewListProductCustodyXpo)(Nothing, filtroConsulta)
                If INDGleOrderBy.EditValue = 1 Then
                    filterOrder = filterOrder.OrderBy(Function(x) x.ProductCode).ToList
                Else
                    filterOrder = filterOrder.OrderBy(Function(x) x.ProductName).ToList
                End If

                ''si se muestra o no el lote
                If INDGleBatchSerial.EditValue = 2 Then

                    finalData = From item In filterOrder
                                Group By ProductName = item.ProductName,
                                                                       ProductCode = item.ProductCode,
                                                                       WarehouseCode = item.WarehouseCode,
                                                                       WarehouseName = item.WarehouseName
                                                                   Into mygroup = Group, SumQuantity = Sum(CDec(item.Quantity))
                                Select New With {Key .ProductCode = ProductCode,
                                                                               Key .ProductName = ProductName,
                                                                               Key .WarehouseCode = WarehouseCode,
                                                                               Key .WarehouseName = WarehouseName,
                                                                               Key .Quantity = SumQuantity}

                    ''se cambian labels segun el agrupador
                    INDColBatchSerial.Visible = False
                    INDColExpirationDate.Visible = False
                Else
                    finalData = filterOrder
                End If
                If INDGleGroupBy.EditValue = 1 Then
                    INDColCode.Caption = "Código Producto"
                    INDCcolName.Caption = "Nombre Producto"
                    INDColGroup.Caption = "Almacén"

                    INDColCode.FieldName = "ProductCode"
                    INDCcolName.FieldName = "ProductName"
                    INDColBatchSerial.FieldName = "BatchCode"
                    INDColExpirationDate.FieldName = "ExpirationDate"
                    INDColGroup.UnboundExpression = "[WarehouseCode] + ' - ' + [WarehouseName]"
                Else
                    INDColCode.Caption = "Código Almacén"
                    INDCcolName.Caption = "Nombre Almacén"

                    INDColCode.FieldName = "WarehouseCode"
                    INDCcolName.FieldName = "WarehouseName"
                    INDColBatchSerial.FieldName = "BatchCode"
                    INDColExpirationDate.FieldName = "ExpirationDate"

                    INDColGroup.Caption = "Producto"
                    INDColGroup.UnboundExpression = "[ProductCode] + ' - ' + [ProductName]"
                End If

                INDgcExportExcel.DataSource = finalData
                GenerateExcel()

            End If
        End If
    End Sub


#Region "ToExcel"
    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenerateExcel()
        Dim _gridView = INDgcExportExcel
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        INDgcExportExcel.DataSource = Nothing
        INDgcExportExcel.RefreshDataSource()
    End Sub
#End Region

End Class