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

#End Region

Public Class FrmReportChecksWritten

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"
    Public Property ProoftCloseXpoTransactionVouchers As XPInstantFeedbackSource
    Public Property ProoftCloseXpoBanks As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEntityBankAccounts As XPInstantFeedbackSource
    Private SelectAllTransactionVouchers = True

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

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

#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleTransactionVoucherStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoTransactionVoucherStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoTransactionVouchers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListVoucherTranscationCheckReportTreasury)
            INDSleTransactionVouchersStart.Datasource = ProoftCloseXpoTransactionVouchers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleTransactionVoucherEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoTransactionVoucherEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoTransactionVouchers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListVoucherTranscationCheckReportTreasury)
            INDSleTransactionVouchersEnd.Datasource = ProoftCloseXpoTransactionVouchers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control de bancos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBanks()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True}
            ProoftCloseXpoBanks = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllBank, filter)
            INDsleBank.Properties.DataSource = ProoftCloseXpoBanks
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control de cuentas bancarias
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEntityBankAccounts()
        Using msearch As New MBusqueda
            Dim filter() As Object
            If INDsleBank.EditValue = Nothing Then
                filter = {True, 0}
            Else
                filter = {True, INDsleBank.EditValue}
            End If
            ProoftCloseXpoEntityBankAccounts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEntityBankAccountByBank, filter)
            INDsleEntityBankAccounts.Properties.DataSource = ProoftCloseXpoEntityBankAccounts
        End Using
    End Sub

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
            Me.INDDateEnd.Focus()
            Validations = False
        End If

        'Valida Comprobantes de Egreso (Consecutivos)
        If INDSleTransactionVouchersStart.EditValue Is Nothing And INDSleTransactionVouchersEnd.EditValue IsNot Nothing Or INDSleTransactionVouchersEnd.EditValue Is Nothing And INDSleTransactionVouchersStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblTransactionVouchers.Text)
            Me.INDSleTransactionVouchersStart.Focus()
            Validations = False
        ElseIf INDSleTransactionVouchersEnd.EditValue < INDSleTransactionVouchersStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblTransactionVouchers.Text)
            Me.INDSleTransactionVouchersStart.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable

        Dim filtroConsulta As String = "GetDate(TransactionDate) >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(TransactionDate) <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd") & "# AND IdCashRegister is null"

        'Se filtra por Clientes
        If INDSleTransactionVouchersStart.EditValue IsNot Nothing And INDSleTransactionVouchersEnd.EditValue IsNot Nothing Then
            filtroConsulta &= " AND CheckNumber >= '" & INDSleTransactionVouchersStart.EditValue & "' AND CheckNumber <= '" & INDSleTransactionVouchersEnd.EditValue & "'"
        End If
        If INDsleBank.EditValue IsNot Nothing And INDsleEntityBankAccounts.EditValue Is Nothing Then
            filtroConsulta &= " AND IdEntityBankAccount.IdBank.Id = " & INDsleBank.EditValue
        End If
        If INDsleEntityBankAccounts.EditValue IsNot Nothing Then
            filtroConsulta &= " AND IdEntityBankAccount.Id = " & INDsleEntityBankAccounts.EditValue
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryVoucherTransactionXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("No Comp. Egreso")
        dt.Columns.Add("No Cheque")
        dt.Columns.Add("Fecha Consignación")
        dt.Columns.Add("Nit Cliente")
        dt.Columns.Add("Nombre Cliente")
        dt.Columns.Add("Código Banco")
        dt.Columns.Add("Nombre Banco")
        dt.Columns.Add("No Cuenta Bancaria")
        dt.Columns.Add("Estado")

        Dim columValue As DataColumn = New DataColumn
        columValue.DataType = System.Type.GetType("System.Decimal")
        columValue.AllowDBNull = False
        columValue.Caption = "Valor"
        columValue.ColumnName = "Valor"
        dt.Columns.Add(columValue)

        For Each itemView In IndList
            Dim row As DataRow = dt.NewRow()
            row.Item("No Comp. Egreso") = itemView.Code
            row.Item("No Cheque") = itemView.CheckNumber
            row.Item("Fecha Consignación") = itemView.TransactionDate
            row.Item("Nit Cliente") = If(IsNothing(itemView.IdThirdParty) = True, "", itemView.IdThirdParty.Nit)
            row.Item("Nombre Cliente") = If(IsNothing(itemView.IdThirdParty) = True, "", itemView.IdThirdParty.Name)
            row.Item("Código Banco") = If(IsNothing(itemView.IdEntityBankAccount) = True, "", itemView.IdEntityBankAccount.Code)
            row.Item("Nombre Banco") = If(IsNothing(itemView.IdEntityBankAccount) = True, "", itemView.IdEntityBankAccount.IdBank.Name)
            row.Item("No Cuenta Bancaria") = If(IsNothing(itemView.IdEntityBankAccount) = True, "", itemView.IdEntityBankAccount.Number)
            row.Item("Estado") = itemView.Status
            row.Item("Valor") = itemView.Value

            dt.Rows.Add(row)
        Next
        AsyncLoader(False)
        If dt IsNot Nothing Then
            Return dt
        Else
            Return New DataTable
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDateStart.Focus()
        End If
    End Function

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
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

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoBanks = Nothing
        ProoftCloseXpoEntityBankAccounts = Nothing
        ProoftCloseXpoTransactionVouchers = Nothing
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerareReport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerareReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerareReport.Click

        If Me.ValidateControlsReports Then
            AsyncLoader(True)
            Dim reporte As New rptChecksWritten

            reporte.ParametrosReporte = {INDDateStart.EditValue,
                                         INDDateEnd.EditValue,
                                         INDSleTransactionVouchersStart.EditValue,
                                         INDSleTransactionVouchersEnd.EditValue,
                                         INDsleBank.EditValue,
                                         INDsleEntityBankAccounts.EditValue}

            INDDvViewReport.DocumentSource = reporte
            reporte.CargarDataSource()
            reporte.CreateDocument(True)
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateStart.Focus()
            End If
        End If
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
    ''' se ejecuta en el evento QueryPopUp del Control INDSleTransactionVouchersStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTransactionVouchersStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleTransactionVouchersStart.QueryPopUp
        If INDSleTransactionVouchersStart.Datasource Is Nothing Then
            LoadXpoTransactionVoucherStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleTransactionVouchersEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleTransactionVouchersEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleTransactionVouchersEnd.QueryPopUp
        If INDSleTransactionVouchersEnd.Datasource Is Nothing Then
            LoadXpoTransactionVoucherEnd()
        End If
    End Sub

    Private Sub FrmReportChecksWritten_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleTransactionVouchersStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetVoucherTransactionByCheckNumber
        Me.INDSleTransactionVouchersEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetVoucherTransactionByCheckNumber
        INDsleBank.Properties.Buttons(1).Visible = False
        INDsleEntityBankAccounts.Properties.Buttons(1).Visible = False
    End Sub

    ''' <summary>
    ''' se dispara al cambiar el valor del banco
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBank_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBank.EditValueChanged
        INDsleEntityBankAccounts.EditValue = Nothing
        LoadXpoEntityBankAccounts()
    End Sub

    ''' <summary>
    ''' se dispara para cargar el datasource de bancos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBank_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleBank.QueryPopUp
        If INDsleBank.Properties.DataSource Is Nothing Then
            LoadXpoBanks()
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de las cuentas bancarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityBankAccounts_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntityBankAccounts.QueryPopUp
        If INDsleEntityBankAccounts.Properties.DataSource Is Nothing Then
            LoadXpoEntityBankAccounts()
        End If
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

#End Region

End Class