#Region "Imports"

Imports System.Globalization
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportCashReceipts

#Region "Variables"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon
    Private _culture = CultureInfo.CurrentCulture.Clone()
#End Region

#Region "Datasource"

    Private _FillingGrouping As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingGrouping As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGrouping Is Nothing Then
                _FillingGrouping = New List(Of Tuple(Of Integer, String))
                _FillingGrouping.Add(New Tuple(Of Integer, String)(1, "Cuenta/Caja"))
                _FillingGrouping.Add(New Tuple(Of Integer, String)(2, "Tercero"))
            End If
            Return _FillingGrouping
        End Get
    End Property

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "General"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Facturas Pagas y Cheques Girados"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(3, "Detallado"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private Property CashRegisterXpo As XPInstantFeedbackSource
        Get
            Return INDSleCashRegister.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCashRegister.Properties.DataSource = value
        End Set
    End Property

    Private Property EntityBankAccountXpo As XPInstantFeedbackSource
        Get
            Return INDSleEntityBankAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleEntityBankAccount.Properties.DataSource = value
        End Set
    End Property

    Private Property ThirdPartyXpo As XPInstantFeedbackSource
        Get
            Return INDSleThirdParty.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleThirdParty.Properties.DataSource = value
        End Set
    End Property

    Private Property DocumentXpo As XPInstantFeedbackSource
        Get
            Return INDSleDocument.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleDocument.Properties.DataSource = value
        End Set
    End Property

    Private Property UserXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource
        Get
            Return INDSleUser.Properties.DataSource
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDSleUser.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de la moneda
    ''' </summary>
    ''' <returns></returns>
    Private Property CurrencyXpo As XPInstantFeedbackSource
        Get
            Return INDSleCurrency.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' id de la moneda
    ''' </summary>
    ''' <returns></returns>
    Private Property CurrencyId As Integer?
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDSleCurrency.EditValue = value
        End Set
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

        If INDGleTypeReport.EditValue = 3 AndAlso CurrencyId <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CurrencyVoid", "Commons"))
            Me.INDSleCurrency.Focus()
            Validations = False
        End If

        Return Validations
    End Function

#Region "Selector"

    Private _selectorCashRegister As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorEntityBankAccount As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorThirdParty As SelectorCache = New SelectorCache("Id", "Nit")
    Private _selectorDocument As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorUser As SelectorCache = New SelectorCache("UserCode", "UserCode")

    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvCashRegister.CustomUnboundColumnData, INDGvEntityBankAccount.CustomUnboundColumnData, INDGvThirdParty.CustomUnboundColumnData, INDGvDocument.CustomUnboundColumnData, INDGvUser.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCashRegister" Then
                e.Value = _selectorCashRegister.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvEntityBankAccount" Then
                e.Value = _selectorEntityBankAccount.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvThirdParty" Then
                e.Value = _selectorThirdParty.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvDocument" Then
                e.Value = _selectorDocument.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvUser" Then
                e.Value = _selectorUser.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvCashRegister.RowCellClick, INDGvEntityBankAccount.RowCellClick, INDGvThirdParty.RowCellClick, INDGvDocument.RowCellClick, INDGvUser.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCashRegister" Then
                selector = _selectorCashRegister
            ElseIf view.Name = "INDGvEntityBankAccount" Then
                selector = _selectorEntityBankAccount
            ElseIf view.Name = "INDGvThirdParty" Then
                selector = _selectorThirdParty
            ElseIf view.Name = "INDGvDocument" Then
                selector = _selectorDocument
            ElseIf view.Name = "INDGvUser" Then
                selector = _selectorUser
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

    Private Sub INDSle_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleCashRegister.Closed, INDSleEntityBankAccount.Closed, INDSleThirdParty.Closed, INDSleDocument.Closed, INDSleUser.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleCashRegister" Then
            searchLookupEdit.Properties.NullText = _selectorCashRegister.ToString()
        ElseIf searchLookupEdit.Name = "INDSleEntityBankAccount" Then
            searchLookupEdit.Properties.NullText = _selectorEntityBankAccount.ToString()
        ElseIf searchLookupEdit.Name = "INDSleThirdParty" Then
            searchLookupEdit.Properties.NullText = _selectorThirdParty.ToString()
        ElseIf searchLookupEdit.Name = "INDSleDocument" Then
            searchLookupEdit.Properties.NullText = _selectorDocument.ToString()
        ElseIf searchLookupEdit.Name = "INDSleUser" Then
            searchLookupEdit.Properties.NullText = _selectorUser.ToString()
        End If
    End Sub

#End Region

#Region "ToExcel"

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable
        Dim filtroConsulta As String = "DocumentDate >= #" & Format(INDDeDateStart.EditValue, "yyyy-MM-dd HH:mm:ss") & "# AND DocumentDate <= #" & Format(INDDeDateEnd.EditValue, "yyyy-MM-dd HH:mm:ss") & "#"

        If INDCcbeStatus.EditValue IsNot Nothing Then
            filtroConsulta &= String.Format(" AND Status IN ({0})", INDCcbeStatus.EditValue)
        End If

        Dim cashRegisters = _selectorCashRegister.GetKeys()
        Dim entityBankAccounts = _selectorEntityBankAccount.GetKeys()
        Dim thirdParties = _selectorThirdParty.GetKeys()
        Dim documents = _selectorDocument.GetKeys()
        Dim users = _selectorUser.GetKeys()

        If Not String.IsNullOrEmpty(cashRegisters) AndAlso Not String.IsNullOrEmpty(entityBankAccounts) Then
            filtroConsulta &= String.Format(" AND (IdCashRegister.Id IN ({0}) OR IdBankAccount.Id IN ({1}))", cashRegisters, entityBankAccounts)
        ElseIf Not String.IsNullOrEmpty(cashRegisters) Then
            filtroConsulta &= String.Format(" AND IdCashRegister.Id IN ({0})", cashRegisters)
        ElseIf Not String.IsNullOrEmpty(entityBankAccounts) Then
            filtroConsulta &= String.Format(" AND IdBankAccount.Id IN ({0})", entityBankAccounts)
        End If

        If Not String.IsNullOrEmpty(thirdParties) Then
            filtroConsulta &= String.Format(" AND IdThirdParty.Id IN ({0})", thirdParties)
        End If

        If Not String.IsNullOrEmpty(documents) Then
            filtroConsulta &= String.Format(" AND Id IN ({0})", documents)
        End If

        If CurrencyId > 0 Then
            filtroConsulta &= String.Format("AND CurrencyId = {0}", CurrencyId)
        End If

        If Not String.IsNullOrEmpty(users) Then
            filtroConsulta &= String.Format(" AND CreationUser IN ({0})", String.Join(",", users.Split(",").Select(Function(x) String.Format("'{0}'", x)).ToList()))
        End If

        Dim IndList = XpoServiceEx.Instance(indigo.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryCashReceiptsXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Código Caja / Banco")
        dt.Columns.Add("Nombre Caja / Banco")
        dt.Columns.Add("Consecutivo")
        dt.Columns.Add("Fecha")
        dt.Columns.Add("Nit Tercero")
        dt.Columns.Add("Nombre Tercero")
        dt.Columns.Add("Usuario Creación")
        dt.Columns.Add("Código Cuenta Contable")
        dt.Columns.Add("Nombre Cuenta Contable")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Moneda")

        Dim columValueInvoice As DataColumn = New DataColumn
        columValueInvoice.DataType = System.Type.GetType("System.Decimal")
        columValueInvoice.AllowDBNull = False
        columValueInvoice.Caption = "Valor"
        columValueInvoice.ColumnName = "Valor"
        dt.Columns.Add(columValueInvoice)

        For Each itemView In IndList
            Dim codeCashBank As String
            Dim nameCashBank As String
            If itemView.IdCashRegister Is Nothing Then
                codeCashBank = itemView.IdBankAccount.Code
                nameCashBank = itemView.IdBankAccount.IdBank.Name
            Else
                codeCashBank = itemView.IdCashRegister.Code
                nameCashBank = itemView.IdCashRegister.Name
            End If

            Dim row As DataRow = dt.NewRow()
            row.Item("Código Caja / Banco") = codeCashBank
            row.Item("Nombre Caja / Banco") = nameCashBank
            row.Item("Consecutivo") = itemView.Code
            row.Item("Fecha") = itemView.DocumentDate
            row.Item("Nit Tercero") = itemView.IdThirdParty.Nit
            row.Item("Nombre Tercero") = itemView.IdThirdParty.Name
            row.Item("Usuario Creación") = itemView.CreationUser
            row.Item("Código Cuenta Contable") = itemView.IdMainAccount.Number
            row.Item("Nombre Cuenta Contable") = itemView.IdMainAccount.Name
            row.Item("Estado") = itemView.Status
            row.Item("Moneda") = itemView.CurrencyAbbreviation
            row.Item("Valor") = itemView.Value

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

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportCashReceipts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)

        'Cargar GridLookUpEdit
        Me.INDGleGrouping.Properties.DataSource = FillingGrouping
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleGrouping.EditValue = 1
        Me.INDGleTypeReport.EditValue = 1
        Me.CurrencyId = indigo?.OfficialCurrencyId
        Me.INDSleCurrency.Properties.NullText = indigo?.CurrencyISO4217
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing

        _FillingGrouping = Nothing
        _FillingTypeReport = Nothing

        CashRegisterXpo = Nothing
        EntityBankAccountXpo = Nothing
        ThirdPartyXpo = Nothing
        DocumentXpo = Nothing
        UserXpo = Nothing
        CurrencyXpo = Nothing
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleCashRegister_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCashRegister.QueryPopUp
        If CashRegisterXpo Is Nothing Then
            CashRegisterXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).TreasuryService.ListCashRegistersEntity()
        End If
    End Sub

    Private Sub INDSleEntityBankAccount_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleEntityBankAccount.QueryPopUp
        If EntityBankAccountXpo Is Nothing Then
            EntityBankAccountXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).TreasuryService.ListEntityBankReportTreasury()
        End If
    End Sub

    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If ThirdPartyXpo Is Nothing Then
            ThirdPartyXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).TreasuryService.ListThirdPartyReportTreasury()
        End If
    End Sub

    Private Sub INDSleDocument_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleDocument.QueryPopUp
        If DocumentXpo Is Nothing Then
            DocumentXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).TreasuryService.ListCashReceiptReport(Nothing)
        End If
    End Sub

    Private Sub INDSleUser_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleUser.QueryPopUp
        If UserXpo Is Nothing Then
            'UserXpo = XpoServiceEx.Instance(indigo.SecurityContainer).TreasuryService.ListCreationUsersReportTreasury()
            Using msearch As New MBusqueda
                UserXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCreationUsersReportTreasury)
            End Using
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
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            If INDGleTypeReport.EditValue = 1 Then
                AsyncLoader(True)
                Dim reporte As New rptReportCashReceiptsGeneral
                reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                        INDDeDateEnd.EditValue,
                                                        INDCcbeStatus.EditValue,
                                                        _selectorCashRegister.GetKeys(),
                                                        _selectorEntityBankAccount.GetKeys(),
                                                        _selectorThirdParty.GetKeys(),
                                                        _selectorDocument.GetKeys(),
                                                        _selectorUser.GetKeys(),
                                                        INDGleGrouping.EditValue}
                INDDvDocumentViewer.DocumentSource = reporte
                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                End If
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
                If INDGleTypeReport.EditValue = 2 Then
                    AsyncLoader(True)
                    Dim reporte As New rptReportCashReceiptsBillsPaid
                    reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                                INDDeDateEnd.EditValue,
                                                                INDCcbeStatus.EditValue,
                                                                _selectorCashRegister.GetKeys(),
                                                                _selectorEntityBankAccount.GetKeys(),
                                                                _selectorThirdParty.GetKeys(),
                                                                _selectorDocument.GetKeys(),
                                                                _selectorUser.GetKeys(),
                                                                INDGleGrouping.EditValue}
                    INDDvDocumentViewer.DocumentSource = reporte
                    reporte.CargarDataSource()
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                    End If
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
                    Dim reporte As New rptSubCashReceipt
                    reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                                INDDeDateEnd.EditValue,
                                                                INDCcbeStatus.EditValue,
                                                                _selectorCashRegister.GetKeys(),
                                                                _selectorEntityBankAccount.GetKeys(),
                                                                _selectorThirdParty.GetKeys(),
                                                                _selectorDocument.GetKeys(),
                                                                _selectorUser.GetKeys(),
                                                                INDGleGrouping.EditValue,
                                                                CurrencyId}
                    INDDvDocumentViewer.DocumentSource = reporte
                    reporte.CargarDataSource()

                    _culture.NumberFormat = INDSleCurrency?.Text?.GetNumberFormat
                    reporte.ApplyLocalization(_culture)
                    If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                    End If
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCurrency.QueryPopUp
        If CurrencyXpo Is Nothing Then
            CurrencyXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.GetCurrency
        End If
    End Sub

    ''' <summary>
    ''' oculta el campo moneda cuando el reporte no sea de tipo detallado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 1 Or INDGleTypeReport.EditValue = 2 Then
            INDLciCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            CurrencyId = Nothing
        Else
            INDLciCurrency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            CurrencyId = indigo?.OfficialCurrencyId
        End If
    End Sub

#End Region

#End Region

End Class