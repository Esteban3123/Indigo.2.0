#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Reporter
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
Imports Domain.Entities

#End Region

Public Class FrmReportCashJournal

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

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ProoftCloseXpoCash As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCurrency As XPInstantFeedbackSource

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

    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Sin Confirmar"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Confirmados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Confirmados y Sin Confirmar"))
            End If
            Return _FillingStatus
        End Get
    End Property

    Private _FillingPaymentType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingPaymentType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingPaymentType Is Nothing Then
                _FillingPaymentType = New List(Of Tuple(Of Integer, String))
                _FillingPaymentType.Add(New Tuple(Of Integer, String)(1, "Efectivo"))
                _FillingPaymentType.Add(New Tuple(Of Integer, String)(2, "Cheques"))
                _FillingPaymentType.Add(New Tuple(Of Integer, String)(3, "Tarjetas"))
                _FillingPaymentType.Add(New Tuple(Of Integer, String)(4, "Consignación"))
                _FillingPaymentType.Add(New Tuple(Of Integer, String)(5, "Todos"))
            End If
            Return _FillingPaymentType
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

    Public Property TypeReport As Integer
        Get
            Return INDGleTypeReport.EditValue
        End Get
        Set(value As Integer)
            INDGleTypeReport.EditValue = value
        End Set
    End Property

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
        If INDDeDateStart.EditValue Is Nothing Or INDDeDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        ElseIf Me.INDDeDateStart.EditValue > INDDeDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        End If
        'validaciones controles de caja
        If INDSleCashRegisterStart.EditValue IsNot Nothing And INDSleCashRegisterEnd.EditValue Is Nothing Or INDSleCashRegisterStart.EditValue Is Nothing And INDSleCashRegisterEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCashRegisters.Text)
            Me.INDSleCashRegisterStart.Focus()
            Validations = False
        ElseIf INDSleCashRegisterStart.EditValue > INDSleCashRegisterEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCashRegisters.Text)
            Me.INDSleCashRegisterStart.Focus()
            Validations = False
        End If

        If TypeReport = 1 AndAlso CurrencyId <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CurrencyVoid", "Commons"))
            Me.INDsleCurrency.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCashRegisterStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCashStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCash = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashRegistersEntity)
            INDSleCashRegisterStart.Datasource = ProoftCloseXpoCash
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCashRegisterEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCashEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCash = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashRegistersEntity)
            INDSleCashRegisterEnd.Datasource = ProoftCloseXpoCash
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCurrency
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCurrency()
        ProoftCloseXpoCurrency = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.GetCurrency()
        INDsleCurrency.Properties.DataSource = ProoftCloseXpoCurrency
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollectionReportCashJournal(INDDeDateStart.EditValue, INDDeDateEnd.EditValue, INDGleStatus.EditValue, INDGlePaymentTypes.EditValue, INDSleCashRegisterStart.EditValue, INDSleCashRegisterEnd.EditValue, CurrencyId)

        Dim dt As New DataTable
        dt.Columns.Add("Código Caja")
        dt.Columns.Add("Nombre Caja")
        dt.Columns.Add("Nombre Documento")
        dt.Columns.Add("Código Documento")
        dt.Columns.Add("Fecha Documento")
        dt.Columns.Add("Nit Tercero")
        dt.Columns.Add("Nombre Tercero")
        dt.Columns.Add("Detalle")
        dt.Columns.Add("Metodo Pago")

        Dim columValueInvoice As DataColumn = New DataColumn
        columValueInvoice.DataType = System.Type.GetType("System.Decimal")
        columValueInvoice.AllowDBNull = False
        columValueInvoice.Caption = "Valor Debito"
        columValueInvoice.ColumnName = "Valor Debito"
        dt.Columns.Add(columValueInvoice)

        Dim columValueNote As DataColumn = New DataColumn
        columValueNote.DataType = System.Type.GetType("System.Decimal")
        columValueNote.AllowDBNull = False
        columValueNote.Caption = "Valor Credito"
        columValueNote.ColumnName = "Valor Credito"
        dt.Columns.Add(columValueNote)

        dt.Columns.Add("Moneda")

        For Each itemView In IndList
            Dim paymentMethod As String = String.Empty
            Select Case itemView.PaymentMethod
                Case 1
                    paymentMethod = "Efectivo"
                Case 2
                    paymentMethod = "Cheque"
                Case 3
                    paymentMethod = "Tarjeta"
                Case 4
                    paymentMethod = "Consignación"
                Case Else
                    paymentMethod = "Nota"
            End Select
            Dim row As DataRow = dt.NewRow()
            row.Item("Código Caja") = itemView.CashRegisterCode
            row.Item("Nombre Caja") = itemView.CashRegisterName
            row.Item("Nombre Documento") = itemView.NameVoucher
            row.Item("Código Documento") = itemView.Code
            row.Item("Fecha Documento") = itemView.DocumentDate
            row.Item("Nit Tercero") = itemView.ThirdPartyNit
            row.Item("Nombre Tercero") = itemView.ThirdPartyName
            row.Item("Detalle") = itemView.Detail
            row.Item("Metodo Pago") = paymentMethod
            row.Item("Valor Debito") = itemView.ValueDebit
            row.Item("Valor Credito") = itemView.ValueCredit
            row.Item("Moneda") = itemView.CurrencyAbbreviation

            dt.Rows.Add(row)
        Next
        AsyncLoader(False)
        If dt IsNot Nothing Then
            Return dt
        Else
            Return New DataTable
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDeDateStart.Focus()
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
        ProoftCloseXpoCash = Nothing
        ProoftCloseXpoCurrency = Nothing
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCashRegisterStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCashRegisterStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashRegisterStart.QueryPopUp
        If INDSleCashRegisterStart.Datasource Is Nothing Then
            LoadXpoCashStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCashRegisterEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCashRegisterEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashRegisterEnd.QueryPopUp
        If INDSleCashRegisterEnd.Datasource Is Nothing Then
            LoadXpoCashEnd()
        End If
    End Sub

    ''' <summary>
    ''' popup para mostrar las monedas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp, INDsleCurrency.QueryPopUp
        If INDsleCurrency.Properties.DataSource Is Nothing Then
            LoadXpoCurrency()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportCashJournal_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGlePaymentTypes.Properties.DataSource = FillingPaymentType
        Me.INDGleStatus.Properties.DataSource = FillingStatus

        'Dar un valor por defecto a los GridLookEdit
        TypeReport = 1
        Me.INDGlePaymentTypes.EditValue = 5
        Me.INDGleStatus.EditValue = 2
        LoadXpoCurrency()
        CurrencyId = 2
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
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            If CurrencyId > 0 Then
                Using Model As New MCurrency(MyBase.Tag)
                    _currency = Await Model.GetCurrencyById(CurrencyId)
                End Using
            End If
            Dim reporte As New rptReportCashJournal
            reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                      INDDeDateEnd.EditValue,
                                                      INDSleCashRegisterStart.EditValue,
                                                      INDSleCashRegisterEnd.EditValue,
                                                      INDGlePaymentTypes.EditValue,
                                                      TypeReport,
                                                      INDGleStatus.EditValue,
                                                      CurrencyId}
            reporte.Currency = _currency
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
                Me.INDDeDateStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportCashJournal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleCashRegisterStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetcashRegister
        Me.INDSleCashRegisterEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetcashRegister
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
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

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged

        If TypeReport = 2 Then '2 - resumido
            INDlyCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            CurrencyId = Nothing
        Else '1 - detallado
            INDlyCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            CurrencyId = IndigoSessionValues.OfficialCurrencyId
        End If
    End Sub

#End Region

End Class