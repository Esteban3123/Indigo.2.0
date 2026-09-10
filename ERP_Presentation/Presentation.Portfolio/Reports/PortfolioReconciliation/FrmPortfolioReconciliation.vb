#Region "Imports"

Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.GlosasRepository
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Controls
Imports Presentation.Portfolio.MVP

#End Region

Public Class FrmPortfolioReconciliation
    Implements IPortfolioReconciliation

#Region "Variables"

    Private _criterias As Dictionary(Of String, String)
    Private _filters As Dictionary(Of String, String)
    Private _thirdPartyId As Integer
    Private _documentTypeFilter As String
    Private _statusFilter As String
    Dim DataSource As DataTable

#End Region

#Region "DataSource"
    ''' <summary>
    ''' Especifica el tipo de documento
    ''' </summary>
    ''' <remarks></remarks>
    Private _documentType As List(Of Tuple(Of Byte, String))

    Private ReadOnly Property ListDocumentType As List(Of Tuple(Of Byte, String))
        Get
            If _documentType Is Nothing Then
                _documentType = New List(Of Tuple(Of Byte, String))
                _documentType.Add(New Tuple(Of Byte, String)(1, "Facturación Básica"))
                _documentType.Add(New Tuple(Of Byte, String)(2, "Facturación"))
                _documentType.Add(New Tuple(Of Byte, String)(3, "Impuestos"))
                _documentType.Add(New Tuple(Of Byte, String)(4, "Pagarés"))
                _documentType.Add(New Tuple(Of Byte, String)(5, "Arrendamientos"))
                _documentType.Add(New Tuple(Of Byte, String)(6, "Documento de Pago a Couta Moderadora"))
                _documentType.Add(New Tuple(Of Byte, String)(7, "Factura de Producto"))
            End If
            Return _documentType
        End Get
    End Property

    ''' <summary>
    ''' Especifica el status
    ''' </summary>
    ''' <remarks></remarks>
    Private _status As List(Of Tuple(Of Byte, String))

    Private ReadOnly Property ListStatus As List(Of Tuple(Of Byte, String))
        Get
            If _status Is Nothing Then
                _status = New List(Of Tuple(Of Byte, String))
                _status.Add(New Tuple(Of Byte, String)(3, "Radicada Entidad"))
                _status.Add(New Tuple(Of Byte, String)(7, "Certificada Parcial"))
                _status.Add(New Tuple(Of Byte, String)(8, "Certificada Total"))
                _status.Add(New Tuple(Of Byte, String)(14, "Devolución Factura"))
                _status.Add(New Tuple(Of Byte, String)(15, "Cuenta de Dificil Recaudo"))
                _status.Add(New Tuple(Of Byte, String)(16, "Cobro Juridico"))
            End If
            Return _status
        End Get
    End Property

#End Region

#Region "Properties"

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

    Private WriteOnly Property ICrudBase_Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IPortfolioReconciliation.ActionsOnControls
        Set(value As Boolean)
            Throw New NotImplementedException()
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Despliega el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValueWithList, AddressOf ReturnValues
        'lista de string de solicitados
        Dim listSolicitado As List(Of String) = New List(Of String)
        listSolicitado.Add("Nit")
        listSolicitado.Add("Name")
        listSolicitado.Add("ThirdPartyId")
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Nit", .FieldName = "Nit", .ColumnWidth = 150},
                              New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = 200}}.ToList()
            .ListadoSolicitud = listSolicitado
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Customers
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValues(ByVal ReturnValue As String, ByVal ReturnObject As Object, ByVal ReturnList As List(Of String))
        CleanControls()
        INDbteEntity.Text = ReturnList.Item(0)
        INDbteEntity.Focus()
        INDlblEntity.Text = ReturnList.Item(1)
        Me.INDgcInvocieList.DataSource = Nothing
        _thirdPartyId = ReturnList.Item(2)
        INDbteEntity.Enabled = False
    End Sub

    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        If INDbteEntity.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una Entidad")
            Me.INDbteEntity.Focus()
        End If

        If INDDClosingDate.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("ValidateClosingDateReport", "Commons")))
            Me.INDDClosingDate.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        _criterias = New Dictionary(Of String, String)

        _criterias.Add("IncludeAdvance", INDSleIncludeAdvance.EditValue)

        _filters = New Dictionary(Of String, String)
        _filters.Add("ThirdParties", _thirdPartyId)
        _filters.Add("OperatingUnitId", _selectorOperatingUnit.GetKey(0))
        _filters.Add("OperatingUnits", _selectorOperatingUnit.GetKeys())
        _filters.Add("DocumentTypes", _documentTypeFilter)
        _filters.Add("Status", _statusFilter)
        _filters.Add("InvoiceCategories", _selectorCategory.GetKeys())
        _filters.Add("CareGroup", _selectorAttentionGroup.GetKeys())
        _filters.Add("ClosingDate", INDDClosingDate.EditValue)
        Return True
    End Function

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDbteEntity.Text = String.Empty
        Me.INDSleIncludeAdvance.EditValue = False
        Me.INDSleOperatingUnit.Text = String.Empty
        Me.INDSleCategory.Text = String.Empty
        Me.INDSleAttentionGroup.Text = String.Empty
        Me.INDlblEntity.Text = String.Empty
        Me.INDgcInvocieList.DataSource = Nothing
        Me.INDgcInvocieList.RefreshDataSource()
        Me.INDDClosingDate.EditValue = Nothing
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = True
        INDgcInvocieList.DataSource = Nothing
        INDgcInvocieList.RefreshDataSource()
        INDbteEntity.Enabled = True
        INDSleAttentionGroup.Properties.DataSource = Nothing
        _thirdPartyId = 0
    End Sub

    Public Async Function CargarDataSourceAsync() As Task
        Try
            AsyncLoader(True)
            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetListReportPortfolioReconciliationAsync(_criterias, _filters, Me.indigo)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dtPortfolioReconciliation As DataTable = ds.Tables("ReportPortfolioReconciliation")
                Me.DataSource = dtPortfolioReconciliation
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.DataSource = Nothing
        Finally
            AsyncLoader(False)
        End Try
    End Function

#End Region

#Region "Events"

#Region "Load"

    Private Sub FrmPortfolioReconciliation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDgcInvocieList.DataSource = Nothing
        Me.INDSleIncludeAdvance.EditValue = False
        Me.INDbteEntity.Focus()
        Me.BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Validar) = True
        Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Validar, "Exportar a Excel")
        Me.BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Deshacer) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Buscar) = False
        INDGleDocumentType.Properties.DataSource = ListDocumentType
        INDGleStatus.Properties.DataSource = ListStatus
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed

        _criterias = Nothing
        _filters = Nothing
        _selectorOperatingUnit = Nothing
        _selectorCategory = Nothing
        _documentType = Nothing
        _status = Nothing
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.None)
    End Sub

#End Region

#Region "ToExcel"

    Private Sub chargueDataSourceDetailed(ByVal dtPortfolioReconciliation As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Numerodocumento")
        dt.Columns.Add("NombreTercero")
        dt.Columns.Add("Numerofactura")
        dt.Columns.Add("FechaDocumento", GetType(DateTime))
        dt.Columns.Add("Fechacorte", GetType(DateTime))
        dt.Columns.Add("EstadoCartera")
        dt.Columns.Add("CentroAtencion")
        dt.Columns.Add("CodigoGrupoAtencion")
        dt.Columns.Add("NombreGrupoAtencion")
        dt.Columns.Add("CodigoContrato")
        dt.Columns.Add("NombreContrato")
        dt.Columns.Add("Regimen")
        dt.Columns.Add("Categoría")
        dt.Columns.Add("NumeroRadicado")
        dt.Columns.Add("FechaRadicado", GetType(DateTime))
        dt.Columns.Add("Valorinicial", GetType(Decimal))
        dt.Columns.Add("Retención", GetType(Decimal))
        dt.Columns.Add("MovimientoInicial", GetType(Decimal))
        dt.Columns.Add("NotasDébito", GetType(Decimal))
        dt.Columns.Add("NotasCrédito", GetType(Decimal))
        dt.Columns.Add("Transferencias", GetType(Decimal))
        dt.Columns.Add("RecibosCaja", GetType(Decimal))
        dt.Columns.Add("Cruces", GetType(Decimal))
        dt.Columns.Add("Anticipo", GetType(Decimal))
        dt.Columns.Add("Saldo", GetType(Decimal))
        dt.Columns.Add("Valorglosado", GetType(Decimal))
        dt.Columns.Add("ValorAceptadoIPS", GetType(Decimal))
        dt.Columns.Add("EstadoGlosa")
        dt.Columns.Add("SaldoInicial")
        dt.Columns.Add("ValorAceptadoReiterado")
        dt.Columns.Add("Estadocarteraentidad")
        dt.Columns.Add("Saldocartera")
        dt.Columns.Add("DiferenciaCartera", GetType(Decimal))
        dt.Columns.Add("Saldoglosa")
        dt.Columns.Add("Conceptoconciliación")
        dt.Columns.Add("observación")

        For Each item In dtPortfolioReconciliation.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Numerodocumento") = item("ThirdPartyNit")
            row.Item("NombreTercero") = item("ThirdPartyName")
            row.Item("Numerofactura") = item("DocumentCode")
            row.Item("FechaDocumento") = CDate(item("AccountReceivableDate")).AsDate
            row.Item("Fechacorte") = CDate(INDDClosingDate.EditValue).AsDate
            row.Item("EstadoCartera") = item("PortfolioStatusName")
            row.Item("CentroAtencion") = item("CenterAttention")
            row.Item("CodigoGrupoAtencion") = item("CareGroupCode")
            row.Item("NombreGrupoAtencion") = item("CareGroupName")
            row.Item("CodigoContrato") = item("ContractCode")
            row.Item("NombreContrato") = item("ContractName")
            row.Item("Regimen") = item("RegimenCalculated")
            row.Item("Categoría") = item("Category")
            row.Item("NumeroRadicado") = item("RadicatedConsecutive")
            row.Item("FechaRadicado") = If(IsDBNull(item("RadicatedDate")), DBNull.Value, CDate(item("RadicatedDate")).AsDate)
            row.Item("Valorinicial") = item("DocumentValue")
            row.Item("Retención") = item("RetentionValue")
            row.Item("MovimientoInicial") = item("InitialValue")
            row.Item("NotasDébito") = item("DebitValue")
            row.Item("NotasCrédito") = item("CreditValue")
            row.Item("Transferencias") = item("TransferValue")
            row.Item("RecibosCaja") = item("CashReceiptValue")
            row.Item("Cruces") = item("CrossingValue")
            row.Item("Anticipo") = If(item("AccountReceivableType") = 0, item("Balance"), 0)
            row.Item("Saldo") = item("Balance")
            row.Item("Valorglosado") = item("ValueGlosado")
            row.Item("ValorAceptadoIPS") = item("ValueAcceptedFirstInstance") + item("ValueAcceptedSecondInstance")
            row.Item("EstadoGlosa") = item("GlosaStateName")
            row.Item("SaldoInicial") = If(item("OpeningBalance") = 1, "SI", "NO")
            row.Item("ValorAceptadoReiterado") = item("ValueAcceptedSecondInstance")
            dt.Rows.Add(row)
        Next

        INDGcExportExcell.DataSource = dt
    End Sub

    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
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

#Region "ButtonClick"

    Private Sub INDbteEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteEntity.ButtonClick
        OpenSearch()
    End Sub

    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    Private Async Sub INDBtnFilter_Click(sender As Object, e As EventArgs) Handles INDBtnFilter.Click
        Try
            If Me.ValidateControlsReports = True Then
                Await CargarDataSourceAsync()

                INDgcInvocieList.DataSource = DataSource
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = False

            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "BarraBotones"

    Private Async Sub BarraBotones_GenerarArchivo() Handles BarraBotones.Click_Validar

        If Me.DataSource IsNot Nothing Then

            Try
                AsyncLoader(True)
                If DataSource IsNot Nothing Then
                    Await Task.Factory.StartNew(Sub()
                                                    chargueDataSourceDetailed(DataSource)
                                                End Sub)

                    If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                        generateExcel()
                    End If
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

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

    Private Sub BarraBotones_ClickBusqueda() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

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

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleOperatingUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleOperatingUnit.QueryPopUp
        If INDSleOperatingUnit.Properties.DataSource Is Nothing Then
            INDSleOperatingUnit.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.ListOperatingUnit()
        End If
    End Sub

    Private Sub INDSleCategory_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCategory.QueryPopUp
        If INDSleCategory.Properties.DataSource Is Nothing Then
            INDSleCategory.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BillingService.ListInvoiceCategories()
        End If
    End Sub

    Private Sub INDSleAttentionGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAttentionGroup.QueryPopUp
        If _thirdPartyId > 0 Then
            If INDSleAttentionGroup.Properties.DataSource Is Nothing Then
                INDSleAttentionGroup.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).ContractService.ListCareGroupByThirdParty(_thirdPartyId)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ("Seleccione una Entidad")
        End If

    End Sub

#End Region

#Region "Selector"

    Private _selectorOperatingUnit As SelectorCache = New SelectorCache("Id", "UnitCode")
    Private _selectorCategory As SelectorCache = New SelectorCache("Id", "CodeName")
    Private _selectorAttentionGroup As SelectorCache = New SelectorCache("Id", "CodeName")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvOperatingUnit.CustomUnboundColumnData, INDGvCategory.CustomUnboundColumnData, INDGvAttentionGroup.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvOperatingUnit" Then
                e.Value = _selectorOperatingUnit.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvCategory" Then
                e.Value = _selectorCategory.ValidateExistsRow(e.Row)

            ElseIf view.Name = "INDGvAttentionGroup" Then
                e.Value = _selectorAttentionGroup.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvOperatingUnit.RowCellClick, INDGvCategory.RowCellClick, INDGvAttentionGroup.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvOperatingUnit" Then
                selector = _selectorOperatingUnit
            ElseIf view.Name = "INDGvCategory" Then
                selector = _selectorCategory
            ElseIf view.Name = "INDGvAttentionGroup" Then
                selector = _selectorAttentionGroup
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

    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleOperatingUnit.Closed, INDSleCategory.Closed, INDSleAttentionGroup.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleOperatingUnit" Then
            searchLookupEdit.Properties.NullText = _selectorOperatingUnit.ToString()

        ElseIf searchLookupEdit.Name = "INDSleCategory" Then
            searchLookupEdit.Properties.NullText = _selectorCategory.ToString()

        ElseIf searchLookupEdit.Name = "INDSleAttentionGroup" Then
            searchLookupEdit.Properties.NullText = _selectorAttentionGroup.ToString()
        End If
    End Sub

    Private Sub INDbteEntity_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteEntity.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDbteEntity.Text IsNot String.Empty Then
                Dim filter As String = "Nit = " & INDbteEntity.Text
                Dim entityXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).GlosasService.GetCollection(Of GlosasCustomerXpo)(Nothing, filter).FirstOrDefault()

                CleanControls()
                If entityXpo Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontró el cliente seleccionado"
                    Exit Sub
                End If

                INDbteEntity.EditValue = entityXpo.Nit
                INDlblEntity.EditValue = entityXpo.Name
                _thirdPartyId = entityXpo.ThirdPartyId
                INDSleIncludeAdvance.Focus()
                INDbteEntity.Enabled = False
            Else
                Mensaje(EeventViewerImages.Advertencia) = "Debe diligenciar un cliente"
            End If
        End If
    End Sub

    Private Sub INDGleDocumentType_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDGleDocumentType.CloseUp

        Dim itemDocumentType = (From x In INDGleViewDocumentType.GetSelectedRows() Select DirectCast(INDGleViewDocumentType.GetRow(x), Tuple(Of Byte, String)).Item1)
        Dim itemDocumentTypeText = (From x In INDGleViewDocumentType.GetSelectedRows() Select DirectCast(INDGleViewDocumentType.GetRow(x), Tuple(Of Byte, String)).Item2)
        Dim _text = String.Join(",", itemDocumentTypeText)
        _documentTypeFilter = String.Join(",", itemDocumentType)

        'Se valida si seleccionaron items del search
        If String.IsNullOrEmpty(_documentTypeFilter) Then
            INDGleDocumentType.Properties.NullText = "Seleccione un Tipo de Factura"
        Else
            INDGleDocumentType.Properties.NullText = _text
        End If
    End Sub

    Private Sub INDGleStatus_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDGleStatus.CloseUp

        Dim itemStatus = (From x In INDGleViewStatus.GetSelectedRows() Select DirectCast(INDGleViewStatus.GetRow(x), Tuple(Of Byte, String)).Item1)
        Dim itemStatusText = (From x In INDGleViewStatus.GetSelectedRows() Select DirectCast(INDGleViewStatus.GetRow(x), Tuple(Of Byte, String)).Item2)
        Dim _text = String.Join(",", itemStatusText)
        _statusFilter = String.Join(",", itemStatus)

        'Se valida si seleccionaron items del search
        If String.IsNullOrEmpty(_statusFilter) Then
            INDGleStatus.Properties.NullText = "Seleccione un Estado"
        Else
            INDGleStatus.Properties.NullText = _text
        End If
    End Sub
#End Region

#End Region

End Class