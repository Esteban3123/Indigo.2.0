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

Public Class FrmAdvancesList

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"
    Public Property ProoftCloseXpoSuppliers As XPInstantFeedbackSource
    Public Property ProoftCloseXpoAdvances As XPInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Anticipos Con Saldo"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Anticipos Sin Saldo"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property
    Private criteria As String = Nothing

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
    ''' metodo para Cargar el data source Del Control INDSleSuppliersStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSupplierStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoSuppliers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierReportTreasury)
            INDSleSuppliersStart.Datasource = ProoftCloseXpoSuppliers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleSuppliersStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSupplierEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoSuppliers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierReportTreasury)
            INDSleSuppliersEnd.Datasource = ProoftCloseXpoSuppliers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAdvancesStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAdvanceStart()
        criteria = Nothing
        If (INDGleStatusReport.EditValue <> 3) Then
            If (INDGleStatusReport.EditValue = 1) Then
                If (criteria Is Nothing) Then
                    criteria = "Balance > 0"
                Else
                    criteria &= " AND Balance > 0 "
                End If

            Else
                If (criteria Is Nothing) Then
                    criteria = "Balance = 0 "
                Else
                    criteria &= " AND Balance = 0 "
                End If
            End If
        End If

        If (INDSleSuppliersStart.EditValue IsNot Nothing And INDSleSuppliersEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "IdSupplier.IdThirdParty.Nit >= '" & INDSleSuppliersStart.EditValue & "' AND IdSupplier.IdThirdParty.Nit <= '" & INDSleSuppliersEnd.EditValue & "'"
            Else
                criteria &= " AND IdSupplier.IdThirdParty.Nit >= '" & INDSleSuppliersStart.EditValue & "' AND IdSupplier.IdThirdParty.Nit <= '" & INDSleSuppliersEnd.EditValue & "'"
            End If
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoAdvances = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPaymentsAdvancesReportTreasury, criteria)
            INDSleAdvancesStart.Datasource = ProoftCloseXpoAdvances
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAdvancesEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAdvanceEnd()
        criteria = Nothing
        If (INDGleStatusReport.EditValue <> 3) Then
            If (INDGleStatusReport.EditValue = 1) Then
                If (criteria Is Nothing) Then
                    criteria = "Balance > 0"
                Else
                    criteria &= " AND Balance > 0 "
                End If

            Else
                If (criteria Is Nothing) Then
                    criteria = "Balance = 0 "
                Else
                    criteria &= " AND Balance = 0 "
                End If
            End If
        End If

        If (INDSleSuppliersStart.EditValue IsNot Nothing And INDSleSuppliersEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "IdSupplier.IdThirdParty.Nit >= '" & INDSleSuppliersStart.EditValue & "' AND IdSupplier.IdThirdParty.Nit <= '" & INDSleSuppliersEnd.EditValue & "'"
            Else
                criteria &= " AND IdSupplier.IdThirdParty.Nit >= '" & INDSleSuppliersStart.EditValue & "' AND IdSupplier.IdThirdParty.Nit <= '" & INDSleSuppliersEnd.EditValue & "'"
            End If
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoAdvances = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPaymentsAdvancesReportTreasury, criteria)
            INDSleAdvancesEnd.Datasource = ProoftCloseXpoAdvances
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

        'Valida Terceros
        If INDSleSuppliersStart.EditValue Is Nothing And INDSleSuppliersEnd.EditValue IsNot Nothing Or INDSleSuppliersEnd.EditValue Is Nothing And INDSleSuppliersStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblSuppliers.Text)
            Me.INDSleSuppliersStart.Focus()
            Validations = False
        ElseIf INDSleSuppliersEnd.EditValue < INDSleSuppliersStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblSuppliers.Text)
            Me.INDSleSuppliersStart.Focus()
            Validations = False
        End If

        'Valida Comprobantes De Egreso
        If INDSleAdvancesStart.EditValue Is Nothing And INDSleAdvancesEnd.EditValue IsNot Nothing Or INDSleAdvancesEnd.EditValue Is Nothing And INDSleAdvancesStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblAdvances.Text)
            Me.INDSleAdvancesStart.Focus()
            Validations = False
        ElseIf INDSleAdvancesEnd.EditValue < INDSleAdvancesStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblAdvances.Text)
            Me.INDSleAdvancesStart.Focus()
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

        Dim filtroConsulta As String = "GetDate(MovesDate) >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(MovesDate) <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd") & "#"

        'Se filtra por Proveedores
        If INDSleSuppliersStart.EditValue IsNot Nothing And INDSleSuppliersEnd.EditValue IsNot Nothing Then
            filtroConsulta &= " AND ThirdPartyNit >= '" & INDSleSuppliersStart.EditValue & "' AND ThirdPartyNit <= '" & INDSleSuppliersEnd.EditValue & "'"
        End If

        'Se filtra por Anticipos
        If INDSleAdvancesStart.EditValue IsNot Nothing And INDSleAdvancesEnd.EditValue IsNot Nothing Then
            filtroConsulta &= " AND MovesCode >= '" & INDSleAdvancesStart.EditValue & "' AND MovesCode <= '" & INDSleAdvancesEnd.EditValue & "'"
        End If

        'Se filtra por Saldo
        If INDGleStatusReport.EditValue <> 3 Then
            If INDGleStatusReport.EditValue = 1 Then
                filtroConsulta &= " AND BillCurrentBalance > 0"
            Else
                filtroConsulta &= " AND BillCurrentBalance = 0"
            End If
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryVReportAdvancesListXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Tipo Movimiento")
        dt.Columns.Add("Consecutivo")
        dt.Columns.Add("Anticipo")

        Dim columValueInitial As DataColumn = New DataColumn
        columValueInitial.DataType = System.Type.GetType("System.Decimal")
        columValueInitial.AllowDBNull = False
        columValueInitial.Caption = "Valor Inicial"
        columValueInitial.ColumnName = "Valor Inicial"
        dt.Columns.Add(columValueInitial)

        Dim columBalance As DataColumn = New DataColumn
        columBalance.DataType = System.Type.GetType("System.Decimal")
        columBalance.AllowDBNull = False
        columBalance.Caption = "Saldo Actual"
        columBalance.ColumnName = "Saldo Actual"
        dt.Columns.Add(columBalance)

        dt.Columns.Add("Nit Tercero")
        dt.Columns.Add("Nombre Tercero")
        dt.Columns.Add("Código Cuenta Contable")
        dt.Columns.Add("Numero Cuenta Contable")
        dt.Columns.Add("Fecha")
        dt.Columns.Add("Estado")

        Dim columCurrency As DataColumn = New DataColumn
        columCurrency.DataType = System.Type.GetType("System.String")
        columCurrency.AllowDBNull = False
        columCurrency.Caption = "Moneda"
        columCurrency.ColumnName = "Moneda"
        dt.Columns.Add(columCurrency)

        Dim columValueDebit As DataColumn = New DataColumn
        columValueDebit.DataType = System.Type.GetType("System.Decimal")
        columValueDebit.AllowDBNull = False
        columValueDebit.Caption = "Valor Debito"
        columValueDebit.ColumnName = "Valor Debito"
        dt.Columns.Add(columValueDebit)

        Dim columValueCredit As DataColumn = New DataColumn
        columValueCredit.DataType = System.Type.GetType("System.Decimal")
        columValueCredit.AllowDBNull = False
        columValueCredit.Caption = "Valor Credito"
        columValueCredit.ColumnName = "Valor Credito"
        dt.Columns.Add(columValueCredit)

        For Each itemView In IndList
            Dim statusName As String = String.Empty
            Select Case itemView.Status
                Case 1
                    statusName = "Registrado"
                Case 2
                    statusName = "Confirmado"
                Case 3
                    statusName = "Anulado"
            End Select

            Dim row As DataRow = dt.NewRow()
            row.Item("Tipo Movimiento") = itemView.NameVoucher
            row.Item("Consecutivo") = itemView.MovesCode
            row.Item("Anticipo") = itemView.AdvanceId
            row.Item("Valor Inicial") = itemView.BillValueInitial
            row.Item("Saldo Actual") = itemView.BillCurrentBalance
            row.Item("Nit Tercero") = itemView.ThirdPartyNit
            row.Item("Nombre Tercero") = itemView.ThirdPartyName
            row.Item("Código Cuenta Contable") = itemView.AccountNumber
            row.Item("Numero Cuenta Contable") = itemView.AccountName
            row.Item("Fecha") = itemView.MovesDate
            row.Item("Estado") = statusName
            row.Item("Moneda") = itemView.Abbreviation
            row.Item("Valor Debito") = itemView.MovesDebit
            row.Item("Valor Credito") = itemView.MovesCredit

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
        ProoftCloseXpoAdvances = Nothing
        ProoftCloseXpoSuppliers = Nothing
    End Sub
    Private Sub INDSbGenerareReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerareReport.Click

        If Me.ValidateControlsReports Then
            AsyncLoader(True)
            Dim reporte As New rptAdvancesList

            reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue,
                                         INDGleStatusReport.EditValue,
                                         INDSleSuppliersStart.EditValue, INDSleSuppliersEnd.EditValue,
                                         INDSleAdvancesStart.EditValue, INDSleAdvancesEnd.EditValue}

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
    Private Sub FrmAdvancesList_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar el GridLookUpEdit
        Me.INDGleStatusReport.Properties.DataSource = FillingStatus

        'Asigna un valor por defecto a GridLookUpEdit
        Me.INDGleStatusReport.EditValue = 3
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleSuppliersStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSuppliersStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleSuppliersStart.QueryPopUp
        If (INDSleSuppliersStart.Datasource Is Nothing) Then
            LoadXpoSupplierStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleSuppliersEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSuppliersEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleSuppliersEnd.QueryPopUp
        If (INDSleSuppliersEnd.Datasource Is Nothing) Then
            LoadXpoSupplierEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAdvancesStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAdvancesStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAdvancesStart.QueryPopUp
        If (INDSleAdvancesStart.Datasource Is Nothing) Then
            LoadXpoAdvanceStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAdvancesEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAdvancesEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAdvancesEnd.QueryPopUp
        If (INDSleAdvancesEnd.Datasource Is Nothing) Then
            LoadXpoAdvanceEnd()
        End If
    End Sub

    Private Sub INDGleStatusReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleStatusReport.EditValueChanged
        LoadXpoAdvanceStart()
        LoadXpoAdvanceEnd()
    End Sub

    Private Sub INDSleSuppliersStart_EditValueChanged(sender As Object, e As EventArgs)
        LoadXpoAdvanceStart()
        LoadXpoAdvanceEnd()
    End Sub

    Private Sub INDSleSuppliersEnd_EditValueChanged(sender As Object, e As EventArgs)
        LoadXpoAdvanceStart()
        LoadXpoAdvanceEnd()
    End Sub

    Private Sub FrmAdvancesList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleSuppliersStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetSupplierByNitThirdParty
        Me.INDSleSuppliersEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetSupplierByNitThirdParty
        Me.INDSleAdvancesStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAdvanceByCode
        Me.INDSleAdvancesEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAdvanceByCode
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