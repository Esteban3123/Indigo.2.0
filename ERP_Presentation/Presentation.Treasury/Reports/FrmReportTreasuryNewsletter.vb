#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.TreasuryRepository
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

Public Class FrmReportTreasuryNewsletter

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

    ''' <summary>
    ''' obtiene o establece la informacion de la moneda
    ''' </summary>
    Private Currency As Currency
#End Region

#Region "properties"
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Public Property ProoftCloseXpoCurrentAccountSavings As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCash As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCurrency As XPInstantFeedbackSource
    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Boletin"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Libro Bancos"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingFormat As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingFormat As List(Of Tuple(Of Integer, String))
        Get
            If _FillingFormat Is Nothing Then
                _FillingFormat = New List(Of Tuple(Of Integer, String))
                _FillingFormat.Add(New Tuple(Of Integer, String)(1, "Detallado"))
                _FillingFormat.Add(New Tuple(Of Integer, String)(2, "Resumido"))
            End If
            Return _FillingFormat
        End Get
    End Property

    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Registrados (Sin Confirmar)"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Confirmados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Anulados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property

    ''' <summary>
    ''' obtiene la moneda seleccionada en el campo moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId As Integer?
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDSleCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el formato del reporte seleccionado en el campo Formato
    ''' </summary>
    ''' <returns></returns>
    Public Property FormatReport As Integer?
        Get
            Return INDGleFormat.EditValue
        End Get
        Set(value As Integer?)
            INDGleFormat.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tipo del reporte seleccionado en el campo Tipo Reporte
    ''' </summary>
    ''' <returns></returns>
    Public Property TypeReport As Integer?
        Get
            Return INDGleTypeReport.EditValue
        End Get
        Set(value As Integer?)
            INDGleTypeReport.EditValue = value
        End Set
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
        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        End If
        'validaciones de rangos
        If INDSleCurrentAccountSavingStart.TextEditValue = String.Empty And INDSleCurrentAccountSavingEnd.TextEditValue = String.Empty And INDSleCashStart.TextEditValue = String.Empty And INDSleCashEnd.TextEditValue = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportTreasuryNewsletter_Filter", "Treasury"))
            Me.INDSleCurrentAccountSavingStart.Focus()
            Validations = False
        ElseIf INDSleCurrentAccountSavingStart.TextEditValue <> String.Empty And INDSleCurrentAccountSavingEnd.TextEditValue = String.Empty Or INDSleCurrentAccountSavingStart.TextEditValue = String.Empty And INDSleCurrentAccountSavingEnd.TextEditValue <> String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCurrentAccountSavings.Text)
            Me.INDSleCurrentAccountSavingStart.Focus()
            Validations = False
        ElseIf INDSleCurrentAccountSavingStart.TextEditValue > INDSleCurrentAccountSavingEnd.TextEditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCurrentAccountSavings.Text)
            Me.INDSleCurrentAccountSavingStart.Focus()
            Validations = False
        ElseIf INDSleCashStart.TextEditValue <> String.Empty And INDSleCashEnd.TextEditValue = String.Empty Or INDSleCashStart.TextEditValue = String.Empty And INDSleCashEnd.TextEditValue <> String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCash.Text)
            Me.INDSleCashStart.Focus()
            Validations = False
        ElseIf INDSleCashStart.TextEditValue > INDSleCashEnd.TextEditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCash.Text)
            Me.INDSleCashStart.Focus()
            Validations = False
        End If

        'Valida El control de Status
        If INDCcbStatus.EditValue Is Nothing Or INDCcbStatus.EditValue = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un Estado Cuenta Bancaria / Caja"
            Me.INDCcbStatus.Focus()
            Validations = False
        End If
        If INDCcbDocumentStatus.EditValue Is Nothing Or INDCcbDocumentStatus.EditValue = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un Estado de Documento"
            Me.INDCcbDocumentStatus.Focus()
            Validations = False
        End If

        'Valida la moneda
        If CurrencyId <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una Moneda"
            Me.INDSleCurrency.Focus()
            Validations = False
        End If

        Return Validations
    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCurrentAccountSavingStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCurrentAccountSavingsStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCurrentAccountSavings = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEntityBankAccountsReport)
            INDSleCurrentAccountSavingStart.Datasource = ProoftCloseXpoCurrentAccountSavings
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCurrentAccountSavingEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCurrentAccountSavingsEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCurrentAccountSavings = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEntityBankAccountsReport)
            INDSleCurrentAccountSavingEnd.Datasource = ProoftCloseXpoCurrentAccountSavings
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCashStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCashStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCash = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashRegistersEntity)
            INDSleCashStart.Datasource = ProoftCloseXpoCash
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCashEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCashEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCash = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashRegistersEntity)
            INDSleCashEnd.Datasource = ProoftCloseXpoCash
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCurrency
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCurrency()
        ProoftCloseXpoCurrency = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.GetCurrency()
        INDSleCurrency.Properties.DataSource = ProoftCloseXpoCurrency
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCurrentAccountSavingStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCurrentAccountSavingStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCurrentAccountSavingStart.QueryPopUp
        If INDSleCurrentAccountSavingStart.Datasource Is Nothing Then
            LoadXpoCurrentAccountSavingsStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCurrentAccountSavingEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCurrentAccountSavingEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCurrentAccountSavingEnd.QueryPopUp
        If INDSleCurrentAccountSavingEnd.Datasource Is Nothing Then
            LoadXpoCurrentAccountSavingsEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCashStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCashStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashStart.QueryPopUp
        If INDSleCashStart.Datasource Is Nothing Then
            LoadXpoCashStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCashEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCashEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashEnd.QueryPopUp
        If INDSleCashEnd.Datasource Is Nothing Then
            LoadXpoCashEnd()
        End If
    End Sub

    ''' <summary>
    ''' popup para mostrar las monedas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCurrency.QueryPopUp, INDSleCurrency.QueryPopUp
        If INDSleCurrency.Properties.DataSource Is Nothing Then
            LoadXpoCurrency()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoCash = Nothing
        ProoftCloseXpoCurrentAccountSavings = Nothing
        ProoftCloseXpoCurrency = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportTreasuryNewsletter_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleFormat.Properties.DataSource = FillingFormat
        LoadXpoCurrency()

        'Dar un valor por defecto a los GridLookEdit
        TypeReport = 1
        FormatReport = 1
        CurrencyId = 1
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del Control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        INDLcBase.Visible = True
        INDCncNavigation.Visible = True
        INDPcViewReport.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerareReport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Async Sub INDSbGenerareReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerareReport.Click
        If Me.ValidateControlsReports = True Then
            Try
                If TypeReport = 1 And FormatReport = 1 Or TypeReport = 1 And FormatReport = 2 Then
                    AsyncLoader(True)
                    If CurrencyId > 0 Then
                        Using Model As New MCurrency(MyBase.Tag)
                            Currency = Await Model.GetCurrencyById(CurrencyId)
                        End Using
                    End If

                    Dim reporte As New rptTreasuryNewsletter
                    reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                                INDDateEnd.EditValue,
                                                                INDSleCurrentAccountSavingStart.TextEditValue,
                                                                INDSleCurrentAccountSavingEnd.TextEditValue,
                                                                INDSleCashStart.TextEditValue,
                                                                INDSleCashEnd.TextEditValue,
                                                                INDCcbDocumentStatus.EditValue,
                                                                TypeReport,
                                                                FormatReport,
                                                                INDCcbStatus.EditValue}
                    reporte.Currency = Currency
                    INDDvViewReport.DocumentSource = reporte
                    Dim DataSourceEmpty As Boolean = True

                    If TypeReport = 1 And FormatReport = 1 Then
                        If INDSleCurrentAccountSavingStart.TextEditValue <> "" And INDSleCurrentAccountSavingEnd.TextEditValue <> "" Then
                            reporte.INDSrTreasuryNewsletterReceiptsBank.ReportSource.DataSource = Await reporte.FillListTreasuryNewsletterEntityBank(1, Currency.Abbreviation)
                            reporte.INDSrTreasuryNewsletterExpendituresBank.ReportSource.DataSource = Await reporte.FillListTreasuryNewsletterEntityBank(2, Currency.Abbreviation)
                        End If
                        If INDSleCashStart.TextEditValue <> "" And INDSleCashEnd.TextEditValue <> "" Then
                            reporte.INDSrTreasuryNewsletterReceiptsCash.ReportSource.DataSource = Await reporte.FillListTreasuryNewsletterEntityCash(1, Currency.Abbreviation)
                            reporte.INDSrTreasuryNewsletterExpendituresCash.ReportSource.DataSource = Await reporte.FillListTreasuryNewsletterEntityCash(2, Currency.Abbreviation)
                        End If
                        If DirectCast(reporte.INDSrTreasuryNewsletterReceiptsBank.ReportSource.DataSource, ICollection).Count <= 0 And DirectCast(reporte.INDSrTreasuryNewsletterExpendituresBank.ReportSource.DataSource, ICollection).Count <= 0 And DirectCast(reporte.INDSrTreasuryNewsletterReceiptsCash.ReportSource.DataSource, ICollection).Count <= 0 And DirectCast(reporte.INDSrTreasuryNewsletterExpendituresCash.ReportSource.DataSource, ICollection).Count <= 0 Then
                            DataSourceEmpty = False
                        End If
                    ElseIf TypeReport = 1 And FormatReport = 2 Then
                        reporte.XrSubreport3.ReportSource.DataSource = Await reporte.FillListSummaryNewsletter()
                        If DirectCast(reporte.XrSubreport3.ReportSource.DataSource, ICollection).Count <= 0 Then
                            DataSourceEmpty = False
                        End If
                    End If

                    reporte.CargarDataSource()
                    AsyncLoader(False)
                    If DataSourceEmpty = True Then
                        reporte.CreateDocument(True)
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                ElseIf TypeReport = 2 Then
                    AsyncLoader(True)
                    Dim reporte As New rptTreasuryNewsletterEntityBank

                    reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue, INDCcbDocumentStatus.EditValue,
                                                 INDSleCurrentAccountSavingStart.TextEditValue, INDSleCurrentAccountSavingEnd.TextEditValue,
                                                 INDCcbStatus.EditValue}

                    INDDvViewReport.DocumentSource = reporte

                    Await reporte.CargarDataSourceAsync()
                    AsyncLoader(False)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                End If
            Catch ex As Exception
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportTreasuryNewsletter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleCurrentAccountSavingStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetEntityBankAccount
        Me.INDSleCurrentAccountSavingEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetEntityBankAccount
        Me.INDSleCashStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetcashRegister
        Me.INDSleCashEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetcashRegister
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If TypeReport = 2 Then
            INDLciFormat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCashStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciCashEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciLabelCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            FormatReport = Nothing
            INDSleCashStart.EditValue = Nothing
            INDSleCashEnd.EditValue = Nothing
        Else
            INDLciFormat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciCashStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciCashEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciLabelCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            FormatReport = 1
        End If
    End Sub

    Private Sub INDGleFormat_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleFormat.EditValueChanged
        'Dependiendo si es Formato Detalladao (1) Muestra el campo de Moneda
        If TypeOf INDGleFormat.EditValue Is Integer AndAlso CInt(INDGleFormat.EditValue) = 1 Then
            INDLciCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            CurrencyId = 1
        Else
            INDLciCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            CurrencyId = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Generar Reporte Excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                Dim INDListBank As List(Of TreasuryVReportTreasuryNewsletterEntityBankAccount) = New List(Of TreasuryVReportTreasuryNewsletterEntityBankAccount)()

                'Moneda para el reporte Excel
                If CurrencyId > 0 Then
                    Using Model As New MCurrency(MyBase.Tag)
                        Currency = Await Model.GetCurrencyById(CurrencyId)
                    End Using
                End If
                If INDSleCurrentAccountSavingStart.EditValue IsNot Nothing And INDSleCurrentAccountSavingEnd.EditValue IsNot Nothing Then
                    Dim filtroConsultaBancos As String = Nothing
                    filtroConsultaBancos &= "DocumentDate >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd HH:mm:ss") & "# AND DocumentDate <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd HH:mm:ss") & "#"
                    'filtro por cuentas bancarias
                    filtroConsultaBancos &= "AND EntityBankAccountCode >= '" & INDSleCurrentAccountSavingStart.EditValue & "' AND EntityBankAccountCode <= '" & INDSleCurrentAccountSavingEnd.EditValue & "'"
                    If INDCcbDocumentStatus.EditValue IsNot Nothing Then
                        filtroConsultaBancos &= " AND Status in(" & INDCcbDocumentStatus.EditValue & ")"
                    End If
                    'filtro por moneda
                    If Currency IsNot Nothing Then
                        filtroConsultaBancos &= " AND CurrencyAbbreviation = '" & Currency.Abbreviation & "'"
                    End If
                    Await Task.Run(Sub()
                                       INDListBank = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of TreasuryVReportTreasuryNewsletterEntityBankAccount)(Nothing, filtroConsultaBancos)
                                   End Sub)
                End If
                Dim INDListCash As List(Of TreasuryVReportTreasuryNewsletterCash) = New List(Of TreasuryVReportTreasuryNewsletterCash)()
                If INDSleCashStart.EditValue IsNot Nothing And INDSleCashEnd.EditValue IsNot Nothing Then
                    'Definir Criteria
                    Dim criteriaCajas As String = Nothing
                    criteriaCajas &= "DocumentDate >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd HH:mm:ss") & "# AND DocumentDate <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd HH:mm:ss") & "# "
                    criteriaCajas &= "AND CashRegisterCode >= '" & INDSleCashStart.EditValue & "' AND CashRegisterCode <= '" & INDSleCashEnd.EditValue & "'"
                    If INDCcbDocumentStatus.EditValue IsNot Nothing Then
                        criteriaCajas &= " AND Status in(" & INDCcbDocumentStatus.EditValue.ToString & ")"
                    End If
                    'filtro por moneda
                    If Currency IsNot Nothing Then
                        criteriaCajas &= " AND CurrencyAbbreviation = '" & Currency.Abbreviation & "'"
                    End If
                    Await Task.Run(Sub()
                                       INDListCash = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PayrollService.GetCollection(Of TreasuryVReportTreasuryNewsletterCash)(Nothing, criteriaCajas)
                                   End Sub)
                End If
                Dim listExcel As New List(Of rptExcel)
                For Each itemBank In INDListBank
                    Dim itemExcel As New rptExcel
                    itemExcel.Banco = itemBank.EntityBankAccountName
                    itemExcel.NumCuenta = itemBank.EntityBankAccountNumber
                    itemExcel.Documento = itemBank.NameVoucher
                    itemExcel.Consecutivo = itemBank.Code
                    itemExcel.Tercero = itemBank.ThirdPartyNit & " " & itemBank.ThirdPartyName
                    itemExcel.Fecha = itemBank.DocumentDate
                    itemExcel.Moneda = itemBank.CurrencyAbbreviation
                    itemExcel.Valor = itemBank.Value
                    itemExcel.Detalle = itemBank.Detail
                    Select Case itemBank.Status
                        Case 1
                            itemExcel.Estado = "Registrado"
                        Case 2
                            itemExcel.Estado = "Confirmado"
                        Case 3
                            itemExcel.Estado = "Anulado"
                        Case 4
                            itemExcel.Estado = "Reversado"
                    End Select
                    If itemBank.Nature = 1 Then
                        itemExcel.Naturaleza = "Debito"
                    Else
                        itemExcel.Naturaleza = "Credito"
                    End If
                    listExcel.Add(itemExcel)
                Next
                For Each itemCash In INDListCash
                    Dim itemExcel As New rptExcel
                    itemExcel.Caja = itemCash.CashRegisterName
                    itemExcel.Documento = itemCash.NameVoucher
                    itemExcel.Consecutivo = itemCash.Code
                    itemExcel.Tercero = itemCash.ThirdPartyNit & " " & itemCash.ThirdPartyName
                    itemExcel.Fecha = itemCash.DocumentDate
                    itemExcel.Moneda = itemCash.CurrencyAbbreviation
                    itemExcel.Valor = itemCash.Value
                    itemExcel.Detalle = itemCash.Detail
                    Select Case itemCash.Status
                        Case 1
                            itemExcel.Estado = "Registrado"
                        Case 2
                            itemExcel.Estado = "Confirmado"
                        Case 3
                            itemExcel.Estado = "Anulado"
                        Case 4
                            itemExcel.Estado = "Reversado"
                    End Select
                    If itemCash.Nature = 1 Then
                        itemExcel.Naturaleza = "Debito"
                    Else
                        itemExcel.Naturaleza = "Credito"
                    End If
                    listExcel.Add(itemExcel)
                Next
                Me.INDGcExcel.DataSource = listExcel
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = ex.Message
            End Try
            If Me.INDGcExcel.DataSource IsNot Nothing Then
                generateExcel()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Clase para cargar los datos en la regilla de excel
    ''' </summary>
    ''' <remarks></remarks>
    Class rptExcel
        Property Caja As String
        Property Banco As String
        Property NumCuenta As String
        Property Documento As String
        Property Consecutivo As String
        Property Tercero As String
        Property Fecha As Date
        Property Moneda As String
        Property Valor As Decimal
        Property Detalle As String
        Property Estado As String
        Property Naturaleza As String
    End Class

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExcel
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExcel.DataSource = Nothing
        Me.INDGcExcel.RefreshDataSource()
    End Sub

End Class