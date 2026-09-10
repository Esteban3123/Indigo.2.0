#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Reporter
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
Imports Domain.Entities
#End Region

Public Class FrmReportDetailIncomeFixedAsset
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon
    ''' <summary>
    ''' obtiene o establece la informacion de la moneda
    ''' </summary>
    Private _currency As Currency

#End Region

#Region "Properties"
    Private _FillingState As List(Of Tuple(Of Integer, String))
    Private _FillingTypeReport As List(Of Tuple(Of String, String))
    Private _FillingTypePrint As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private ReadOnly Property FillingState As List(Of Tuple(Of Integer, String))
        Get
            If _FillingState Is Nothing Then
                _FillingState = New List(Of Tuple(Of Integer, String))
                _FillingState.Add(New Tuple(Of Integer, String)(1, "Sin Confirmar"))
                _FillingState.Add(New Tuple(Of Integer, String)(2, "Confirmado"))
                _FillingState.Add(New Tuple(Of Integer, String)(3, "Anulado"))
                _FillingState.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingState
        End Get
    End Property
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of String, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of String, String))
                _FillingTypeReport.Add(New Tuple(Of String, String)("RE", "Remisión de entrada"))
                _FillingTypeReport.Add(New Tuple(Of String, String)("EI", "Ingreso de artículo"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private ReadOnly Property FillingTypePrint As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypePrint Is Nothing Then
                _FillingTypePrint = New List(Of Tuple(Of Integer, String))
                _FillingTypePrint.Add(New Tuple(Of Integer, String)(1, "Listado"))
                _FillingTypePrint.Add(New Tuple(Of Integer, String)(2, "Documento"))
            End If
            Return _FillingTypePrint
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla del tipo de reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingAdquisitionType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingAdquisitionType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingAdquisitionType Is Nothing Then
                _FillingAdquisitionType = New List(Of Tuple(Of Integer, String))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(0, "Todos"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(1, "Compra Directa"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(3, "Comodato"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(8, "Comodato Tercerizado"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(4, "Donación"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(5, "Traspaso de Bienes"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(6, "Otro Concepto"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(7, "Leasing Financiero"))
                '_FillingAdquisitionType.Add(New Tuple(Of Integer, String)(9, "Renting Financiero"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(10, "Renting Operativo"))
            End If
            Return _FillingAdquisitionType
        End Get
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
    ''' Propiedad para obtener la moneda 
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
#End Region

#Region "Methods"
    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        If INDDateEditStart.EditValue Is Nothing Or INDDateEditEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateEditStart.Focus()
            Validations = False
        ElseIf Me.INDDateEditStart.EditValue > INDDateEditEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateEditStart.Focus()
            Validations = False
        End If

        'Valida Ingreso
        If INDSleEntranceStart.EditValue IsNot Nothing And INDSleEntranceEnd.EditValue Is Nothing Or INDSleEntranceEnd.EditValue IsNot Nothing And INDSleEntranceStart.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblEntrance.Text)
            Me.INDSleEntranceStart.Focus()
            Validations = False
        ElseIf INDSleEntranceEnd.EditValue < INDSleEntranceStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblEntrance.Text)
            Me.INDSleEntranceStart.Focus()
            Validations = False
        End If

        'Valida remisiones
        If INDSleRemisionEntranceStart.EditValue IsNot Nothing And INDSleRemisionEntranceEnd.EditValue Is Nothing Or INDSleRemisionEntranceEnd.EditValue IsNot Nothing And INDSleRemisionEntranceStart.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblRemissionEntrance.Text)
            Me.INDSleRemisionEntranceStart.Focus()
            Validations = False
        ElseIf INDSleRemisionEntranceEnd.EditValue < INDSleRemisionEntranceStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblRemissionEntrance.Text)
            Me.INDSleRemisionEntranceStart.Focus()
            Validations = False
        End If

        'Valida Proveedor
        If INDSleSupplierStart.EditValue IsNot Nothing And INDSleSupplierEnd.EditValue Is Nothing Or INDSleSupplierEnd.EditValue IsNot Nothing And INDSleSupplierStart.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblSupplier.Text)
            Me.INDSleSupplierStart.Focus()
            Validations = False
        ElseIf INDSleSupplierEnd.EditValue < INDSleSupplierStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblSupplier.Text)
            Me.INDSleSupplierStart.Focus()
            Validations = False
        End If

        'Valida la moneda 
        If INDGleTypePrint.EditValue = 2 AndAlso INDGleTypeReport.EditValue = "EI" = 2 AndAlso CurrencyId <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CurrencyVoid", "Commons"))
            Me.INDsleCurrency.Focus()
            Validations = False
        End If

        Return Validations
    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDLciEntranceStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEntranceStart()
        Using msearch As New MBusqueda
            INDSleEntranceStart.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEntry)
        End Using
    End Sub

    Private Sub LoadXpoEntranceEnd()
        Using msearch As New MBusqueda
            INDSleEntranceEnd.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEntry)
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control de remisión inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoRemisionEntranceStart()
        Using msearch As New MBusqueda
            INDSleRemisionEntranceStart.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetInputRemission)
        End Using
    End Sub

    Private Sub LoadXpoRemisionEntranceEnd()
        Using msearch As New MBusqueda
            INDSleRemisionEntranceEnd.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetInputRemission)
        End Using
    End Sub

    Private Sub LoadXpoSupplierStart()
        Using msearch As New MBusqueda
            INDSleSupplierStart.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierReport)
        End Using
    End Sub

    Private Sub LoadXpoSupplierEnd()
        Using msearch As New MBusqueda
            INDSleSupplierEnd.Datasource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierReport)
        End Using
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable
        If Me.ValidateControlsReports = True Then
            Dim filtroConsulta As String = "Fecha >= '" & Format(INDDateEditStart.EditValue, "yyyyMMdd") & "' And Fecha <= '" & Format(INDDateEditEnd.EditValue, "yyyyMMdd") & "'"

            'filtro por Estado
            If INDGleState.EditValue <> 4 Then
                filtroConsulta &= "AND Status = " & INDGleState.EditValue
            End If

            'filtro por Ingreso
            If INDSleEntranceStart.EditValue IsNot Nothing And INDSleEntranceEnd.EditValue IsNot Nothing Then
                filtroConsulta &= "AND Code >= '" & INDSleEntranceStart.EditValue & "' AND Code <= '" & INDSleEntranceEnd.EditValue & "'"
            End If

            'filtro por TipoReport
            If INDGleTypeReport.EditValue IsNot Nothing Then
                filtroConsulta &= "AND Type = '" & INDGleTypeReport.EditValue & "'"
            End If

            'filtro por Remission
            If INDSleRemisionEntranceStart.EditValue IsNot Nothing And INDSleRemisionEntranceEnd.EditValue IsNot Nothing Then
                filtroConsulta &= "AND Documento >= '" & INDSleRemisionEntranceStart.EditValue & "' AND Documento <= '" & INDSleRemisionEntranceEnd.EditValue & "'"
            End If

            'filtro por AdquisitionType
            If INDGleAdquisitionType.EditValue <> 0 Then
                filtroConsulta &= "AND AdquisitionType = " & INDGleAdquisitionType.EditValue
            End If

            Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetVReportDetailsFixedAssetsIncomeReportXpo)(Nothing, filtroConsulta)

            Dim dt As New DataTable
            dt.Columns.Add("Fase")
            dt.Columns.Add("Documento")
            dt.Columns.Add("Fecha")
            dt.Columns.Add("Orden De Compra")
            dt.Columns.Add("Tipo Adquisición")
            dt.Columns.Add("Origen")
            dt.Columns.Add("Destino")
            dt.Columns.Add("Placa")
            dt.Columns.Add("Descripción")
            dt.Columns.Add("Serie")
            dt.Columns.Add("Modelo")
            dt.Columns.Add("Marca")
            dt.Columns.Add("SubTotal", GetType(Decimal))
            dt.Columns.Add("IVA", GetType(Decimal))
            dt.Columns.Add("Total", GetType(Decimal))
            dt.Columns.Add("Retefuente", GetType(Decimal))
            dt.Columns.Add("ReteIVA", GetType(Decimal))
            dt.Columns.Add("ReteICA", GetType(Decimal))

            For Each itemView In IndList
                Dim row As DataRow = dt.NewRow()
                row.Item("Fase") = IIf(itemView.Type = "RE", "Remisión de entrada", "Ingreso de artículo")
                row.Item("Documento") = itemView.Documento
                row.Item("Fecha") = itemView.Fecha
                row.Item("Orden De Compra") = itemView.OrdenDeCompra
                row.Item("Tipo Adquisición") = IIf(itemView.AdquisitionType = 1, "Compra Directa",
                                               IIf(itemView.AdquisitionType = 3, "Comodato",
                                               IIf(itemView.AdquisitionType = 4, "Donacion",
                                               IIf(itemView.AdquisitionType = 5, "Traspaso de Bienes",
                                               IIf(itemView.AdquisitionType = 6, "Otro Concepto",
                                               IIf(itemView.AdquisitionType = 7, "Leasing Financiero",
                                               IIf(itemView.AdquisitionType = 8, "Comodato Tercerizado",
                                               IIf(itemView.AdquisitionType = 9, "Renting Financiero",
                                               IIf(itemView.AdquisitionType = 10, "Renting Operativo",
                                                   "Todos")))))))))
                row.Item("Origen") = itemView.Origen
                row.Item("Destino") = itemView.Destino
                row.Item("Placa") = itemView.Placa
                row.Item("Descripción") = itemView.Description
                row.Item("Serie") = itemView.Serie
                row.Item("Modelo") = itemView.Model
                row.Item("Marca") = itemView.Trademark
                row.Item("SubTotal") = itemView.SubTotalValue
                row.Item("IVA") = itemView.IvaValue
                row.Item("Total") = itemView.TotalValue
                row.Item("Retefuente") = itemView.RTFValue
                row.Item("ReteIVA") = itemView.WithholdingTax
                row.Item("ReteICA") = itemView.WithholdingICA
                dt.Rows.Add(row)
            Next
            AsyncLoader(False)
            If dt IsNot Nothing Then
                Return dt
            Else
                Return New DataTable
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateEditStart.Focus()
            End If
        End If
    End Function

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleTypeReport.EditValue & ".xlsx"
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

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnReport.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDDateEditStart.Focus()
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            'si el tipo de impresion es listado 
            If INDGleTypePrint.EditValue = 1 Then
                AsyncLoader(True)
                Dim reporte As New rptDetailIncomeFixedAsset
                reporte.ParametrosReporte = New Object() {INDDateEditStart.EditValue,
                                                          INDDateEditEnd.EditValue,
                                                          INDGleState.EditValue,
                                                          INDSleEntranceStart.EditValue,
                                                          INDSleEntranceEnd.EditValue,
                                                          INDGleTypeReport.EditValue,
                                                          INDSleSupplierStart.EditValue,
                                                          INDSleSupplierEnd.EditValue,
                                                          INDSleRemisionEntranceStart.EditValue,
                                                          INDSleRemisionEntranceEnd.EditValue,
                                                          INDGleAdquisitionType.EditValue}
                INDDvReport.DocumentSource = reporte
                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateEditStart.Focus()
                End If
                AsyncLoader(False)
                'si el tipo de impresión es documento y el tipo de reporte es remisión de entrada
            ElseIf INDGleTypePrint.EditValue = 2 AndAlso INDGleTypeReport.EditValue = "RE" Then
                AsyncLoader(True)
                Dim reporte As New rptSubFixedAssetRemissionEntranceDocument
                reporte.ParametrosReporte = New Object() {INDDateEditStart.EditValue,
                                                          INDDateEditEnd.EditValue,
                                                          INDGleState.EditValue,
                                                          INDSleRemisionEntranceStart.EditValue,
                                                          INDSleRemisionEntranceEnd.EditValue,
                                                          INDGleTypeReport.EditValue,
                                                          INDSleSupplierStart.EditValue,
                                                          INDSleSupplierEnd.EditValue,
                                                          INDGleAdquisitionType.EditValue}
                INDDvReport.DocumentSource = reporte
                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateEditStart.Focus()
                End If
                AsyncLoader(False)

                'si el tipo de impresión es documento y el tipo de reporte es ingreso de activo
            ElseIf INDGleTypePrint.EditValue = 2 AndAlso INDGleTypeReport.EditValue = "EI" Then
                AsyncLoader(True)

                If CurrencyId > 0 Then
                    Using model As New MCurrency(MyBase.Tag)
                        _currency = Await model.GetCurrencyById(CurrencyId)
                    End Using
                End If

                Dim reporte As New rptSubFixedAssetEntryDocument
                reporte.Currency = _currency
                reporte.ParametrosReporte = New Object() {INDDateEditStart.EditValue,
                                                          INDDateEditEnd.EditValue,
                                                          INDGleState.EditValue,
                                                          INDSleEntranceStart.EditValue,
                                                          INDSleEntranceEnd.EditValue,
                                                          INDGleTypeReport.EditValue,
                                                          INDSleSupplierStart.EditValue,
                                                          INDSleSupplierEnd.EditValue,
                                                          INDGleAdquisitionType.EditValue,
                                                          CurrencyId}
                INDDvReport.DocumentSource = reporte
                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateEditStart.Focus()
                End If
                AsyncLoader(False)
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportDetailIncomeFixedAsset_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleState.Properties.DataSource = FillingState
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleTypePrint.Properties.DataSource = FillingTypePrint
        Me.INDGleAdquisitionType.Properties.DataSource = FillingAdquisitionType

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleState.EditValue = 4
        Me.INDGleTypeReport.EditValue = "RE"
        Me.INDGleTypePrint.EditValue = 1
        Me.INDGleAdquisitionType.EditValue = 0

        LoadCurrency()

        'Filtra la moneda que esta parametrizaba en activos fijos.
        Dim filter = "OperatingUnitId = " & Me.indigo.IndigoOperatingUnitId
        Dim settingFixedAsset = XpoServiceEx.Instance(Me.indigo.TransactionalContainer).FixedAsset.GetXPOObject(Of SettingFixedAssetXpo)(filter)

        INDsleCurrency.EditValue = settingFixedAsset.CurrencyId?.Id
    End Sub

#Region "QueryPopUp"
    Private Sub INDSleEntranceStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEntranceStart.QueryPopUp
        If INDSleEntranceStart.Datasource Is Nothing Then
            LoadXpoEntranceStart()
        End If
    End Sub

    Private Sub INDSleEntranceEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEntranceEnd.QueryPopUp
        If INDSleEntranceEnd.Datasource Is Nothing Then
            LoadXpoEntranceEnd()
        End If
    End Sub

    Private Sub INDSleSupplierStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSupplierStart.QueryPopUp
        If INDSleSupplierStart.Datasource Is Nothing Then
            LoadXpoSupplierStart()
        End If
    End Sub

    Private Sub INDSleSupplierEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSupplierEnd.QueryPopUp
        If INDSleSupplierEnd.Datasource Is Nothing Then
            LoadXpoSupplierEnd()
        End If
    End Sub

    Private Sub INDSleRemisionEntranceStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleRemisionEntranceStart.QueryPopUp
        If INDSleRemisionEntranceStart.Datasource Is Nothing Then
            LoadXpoRemisionEntranceStart()
        End If
    End Sub

    Private Sub INDSleRemisionEntranceEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleRemisionEntranceEnd.QueryPopUp
        If INDSleRemisionEntranceEnd.Datasource Is Nothing Then
            LoadXpoRemisionEntranceEnd()
        End If
    End Sub

    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        LoadCurrency()
    End Sub
    Private Sub LoadCurrency()
        If INDsleCurrency.Properties.DataSource Is Nothing Then
            INDsleCurrency.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportDetailIncomeFixedAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)

        Me.INDSleEntranceStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFixedAssetEntryByCode
        Me.INDSleEntranceEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFixedAssetEntryByCode
        Me.INDSleRemisionEntranceStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFixedAssetRemisionEntranceByCode
        Me.INDSleRemisionEntranceEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFixedAssetRemisionEntranceByCode
        Me.INDSleSupplierStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetSupplierByNitThirdParty
        Me.INDSleSupplierEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetSupplierByNitThirdParty

        INDSleEntranceStart.View.OptionsView.ShowGroupPanel = False
        INDSleEntranceEnd.View.OptionsView.ShowGroupPanel = False
        INDSleRemisionEntranceStart.View.OptionsView.ShowGroupPanel = False
        INDSleRemisionEntranceEnd.View.OptionsView.ShowGroupPanel = False
        INDSleSupplierStart.View.OptionsView.ShowGroupPanel = False
        INDSleSupplierEnd.View.OptionsView.ShowGroupPanel = False

    End Sub

    ''' <summary>
    ''' Si cambia el tipo de reporte y selecciona todos ocultamos el gridlockup de tipo de impresión
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = "RE" Then
            'mostramos el tipo de impresión
            INDLciTypePrint.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'mostramos el filtro de remisiones de entrada
            INDLciRemissionEntrance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciRemissionEntranceStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciRemissionEntranceEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'ocultamos el filtro de ingreso de activos
            INDLciLblEntrance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciEntranceStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciEntranceEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'Ocultamos el filtro de la moneda
            INDlyCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'Valor en Nothing
            INDSleEntranceStart.EditValue = Nothing
            INDSleEntranceEnd.EditValue = Nothing
        ElseIf INDGleTypeReport.EditValue = "EI" Then
            'mostramos el tipo de impresión
            INDLciTypePrint.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'ocultamos el filtro de remisiones de entrada
            INDLciRemissionEntrance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRemissionEntranceStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRemissionEntranceEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'mostramos el filtro de ingreso de activos
            INDLciLblEntrance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciEntranceStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciEntranceEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'Mostramos el filtro de la moneda
            INDlyCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'Valor en Nothing
            INDSleRemisionEntranceStart.EditValue = Nothing
            INDSleRemisionEntranceEnd.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            INDGcExportExcell.DataSource = chargueDatasource()
            If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub

#End Region
End Class