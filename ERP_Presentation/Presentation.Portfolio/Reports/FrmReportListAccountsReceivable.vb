#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmReportListAccountsReceivable

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Public Property ProoftCloseXpoCustomers As XPInstantFeedbackSource
    Public Property ProoftCloseXpoSellers As XPInstantFeedbackSource
    Public Property ProoftCloseXpoAccountsReceivables As XPInstantFeedbackSource
    Private List As List(Of PortfolioAccountReceivableReportXpo)

    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Registrado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Confirmado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Anulado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property

    Private criteria As String = Nothing

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
    ''' metodo para Cargar el data source Del Control INDSleCustomersStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCustomersStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCustomers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCustomerReportPortfolio)
            INDSleCustomerStart.Datasource = ProoftCloseXpoCustomers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCustomersEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCustomersEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCustomers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCustomerReportPortfolio)
            INDSleCustomerEnd.Datasource = ProoftCloseXpoCustomers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountsReceivableStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountsReceivableStart()
        criteria = Nothing

        If (INDGleStatusReport.EditValue IsNot Nothing And INDGleStatusReport.EditValue <> 4) Then
            criteria = "Status = " & INDGleStatusReport.EditValue
        End If

        If (INDSleCustomerStart.EditValue IsNot Nothing And INDSleCustomerEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "CustomerId.Nit >= '" & INDSleCustomerStart.EditValue & "' AND CustomerId.Nit <= '" & INDSleCustomerEnd.EditValue & "'"
            Else
                criteria &= " AND CustomerId.Nit >= '" & INDSleCustomerStart.EditValue & "' AND CustomerId.Nit <= '" & INDSleCustomerEnd.EditValue & "'"
            End If
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoAccountsReceivables = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListInvoiceReportPortfolioFilter, criteria)
            INDSleAccountsReceivableStart.Datasource = ProoftCloseXpoAccountsReceivables
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountsReceivableEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountsReceivableEnd()
        criteria = Nothing

        If (INDGleStatusReport.EditValue IsNot Nothing And INDGleStatusReport.EditValue <> 4) Then
            criteria = "Status = " & INDGleStatusReport.EditValue
        End If

        If (INDSleCustomerStart.EditValue IsNot Nothing And INDSleCustomerEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "CustomerId.Nit >= '" & INDSleCustomerStart.EditValue & "' AND CustomerId.Nit <= '" & INDSleCustomerEnd.EditValue & "'"
            Else
                criteria &= " AND CustomerId.Nit >= '" & INDSleCustomerStart.EditValue & "' AND CustomerId.Nit <= '" & INDSleCustomerEnd.EditValue & "'"
            End If
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoAccountsReceivables = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListInvoiceReportPortfolioFilter, criteria)
            INDSleAccountsReceivableEnd.Datasource = ProoftCloseXpoAccountsReceivables
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

        'Valida Clientes
        If INDSleCustomerStart.EditValue Is Nothing And INDSleCustomerEnd.EditValue IsNot Nothing Or INDSleCustomerEnd.EditValue Is Nothing And INDSleCustomerStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCustomers.Text)
            Me.INDSleCustomerStart.Focus()
            Validations = False
        ElseIf INDSleCustomerEnd.EditValue < INDSleCustomerStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCustomers.Text)
            Me.INDSleCustomerStart.Focus()
            Validations = False
        End If

        'Valida Cuentas por Cobrar
        If INDSleAccountsReceivableStart.EditValue Is Nothing And INDSleAccountsReceivableEnd.EditValue IsNot Nothing Or INDSleAccountsReceivableEnd.EditValue Is Nothing And INDSleAccountsReceivableStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblAccountsReceivable.Text)
            Me.INDSleAccountsReceivableStart.Focus()
            Validations = False
        ElseIf INDSleAccountsReceivableEnd.EditValue < INDSleAccountsReceivableStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblAccountsReceivable.Text)
            Me.INDSleAccountsReceivableStart.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Me.INDGcExportExcel.DataSource = chargueDatasource()
            If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub

    Private Function chargueDatasource() As DataTable
        Dim filtroConsulta As String = "GetDate(AccountReceivableDate) >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(AccountReceivableDate) <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd") & "#"

        'Se filtra por Clientes
        If INDSleCustomerStart.EditValue IsNot Nothing And INDSleCustomerEnd.EditValue IsNot Nothing Then
            filtroConsulta &= " AND ThirdPartyId.Nit >= '" & INDSleCustomerStart.EditValue & "' AND ThirdPartyId.Nit <= '" & INDSleCustomerEnd.EditValue & "'"
        End If

        'Se filtra por Cuentas por Cobrar (Facturas)
        If INDSleAccountsReceivableStart.EditValue IsNot Nothing And INDSleAccountsReceivableEnd.EditValue IsNot Nothing Then
            filtroConsulta &= " AND Code >= '" & INDSleAccountsReceivableStart.EditValue & "' AND Code <= '" & INDSleAccountsReceivableEnd.EditValue & "'"
        End If

        If INDGleStatusReport.EditValue <> 4 Then
            filtroConsulta &= " AND Status = " & INDGleStatusReport.EditValue
        End If

        List = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioAccountReceivableReportXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Cliente")
        dt.Columns.Add("Factura")
        dt.Columns.Add("Fecha")
        dt.Columns.Add("Fecha de Vencimiento")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Moneda")

        Dim columValue As DataColumn = New DataColumn
        columValue.DataType = System.Type.GetType("System.Decimal")
        columValue.AllowDBNull = False
        columValue.Caption = "Valor"
        columValue.ColumnName = "Valor"
        dt.Columns.Add(columValue)

        Dim columBalance As DataColumn = New DataColumn
        columBalance.DataType = System.Type.GetType("System.Decimal")
        columBalance.AllowDBNull = False
        columBalance.Caption = "Saldo"
        columBalance.ColumnName = "Saldo"
        dt.Columns.Add(columBalance)


        For Each itemView In List
            Dim row As DataRow = dt.NewRow()
            row.Item("Cliente") = itemView.ThirdPartyId?.NitName
            row.Item("Factura") = itemView.InvoiceNumber
            row.Item("Fecha") = CDate(itemView.AccountReceivableDate).AsDate
            row.Item("Fecha de Vencimiento") = CDate(itemView.ExpiredDate).AsDate
            row.Item("Observaciones") = itemView.Observations
            row.Item("Moneda") = itemView.CurrencyAbbreviation
            row.Item("Valor") = itemView.Value
            row.Item("Saldo") = itemView.Balance
            
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
        Dim _gridView = Me.INDGcExportExcel
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcel.DataSource = Nothing
        Me.INDGcExportExcel.RefreshDataSource()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoAccountsReceivables = Nothing
        ProoftCloseXpoCustomers = Nothing
        ProoftCloseXpoSellers = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerareReport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click

        If Me.ValidateControlsReports Then
            AsyncLoader(True)
            Dim reporte As New rptListAccountsReceivable

            reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue, INDGleStatusReport.EditValue, INDSleExcludeBill.EditValue,
                                         INDSleCustomerStart.EditValue, INDSleCustomerEnd.EditValue,
                                         INDSleAccountsReceivableStart.EditValue, INDSleAccountsReceivableEnd.EditValue}

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
    ''' se ejecuta en el evento Shown del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportListAccountsReceivable_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar el GridLookUpEdit
        Me.INDGleStatusReport.Properties.DataSource = FillingStatus

        'Asigna un valor por defecto a GridLookUpEdit
        Me.INDGleStatusReport.EditValue = 4
        Me.INDSleExcludeBill.EditValue = False

    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCustomerStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCustomerStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCustomerStart.QueryPopUp
        If INDSleCustomerStart.Datasource Is Nothing Then
            LoadXpoCustomersStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCustomerEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCustomerEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCustomerEnd.QueryPopUp
        If INDSleCustomerEnd.Datasource Is Nothing Then
            LoadXpoCustomersEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleAccountsReceivableStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountsReceivableStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountsReceivableStart.QueryPopUp
        If INDSleAccountsReceivableStart.Datasource Is Nothing Then
            LoadXpoAccountsReceivableStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleAccountsReceivableEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountsReceivableEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountsReceivableEnd.QueryPopUp
        If INDSleAccountsReceivableEnd.Datasource Is Nothing Then
            LoadXpoAccountsReceivableEnd()
        End If
    End Sub

    Private Sub INDGleStatusReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleStatusReport.EditValueChanged
        LoadXpoAccountsReceivableStart()
        LoadXpoAccountsReceivableEnd()
    End Sub

    Private Sub INDSleCustomerStart_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleCustomerStart.EditValueChanged
        LoadXpoAccountsReceivableStart()
        LoadXpoAccountsReceivableEnd()
    End Sub

    Private Sub INDSleCustomerEnd_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleCustomerEnd.EditValueChanged
        LoadXpoAccountsReceivableStart()
        LoadXpoAccountsReceivableEnd()
    End Sub

    Private Sub FrmReportListAccountsReceivable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleCustomerStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCustomerByNit
        Me.INDSleCustomerEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCustomerByNit
        Me.INDSleAccountsReceivableStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountReceivableByInvoiceNumber
        Me.INDSleAccountsReceivableEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountReceivableByInvoiceNumber
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub
End Class