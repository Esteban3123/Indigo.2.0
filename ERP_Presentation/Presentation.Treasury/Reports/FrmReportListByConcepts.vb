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

#End Region


Public Class FrmReportListByConcepts

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource
    Public Property ProoftCloseXpoExpensesConcepts As XPInstantFeedbackSource
    Public Property ProoftCloseXpoReceiptConcepts As XPInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Recibos"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Egresos"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingGrouping As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingGrouping As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGrouping Is Nothing Then
                _FillingGrouping = New List(Of Tuple(Of Integer, String))
                _FillingGrouping.Add(New Tuple(Of Integer, String)(1, "Detallado"))
                _FillingGrouping.Add(New Tuple(Of Integer, String)(2, "Resumido"))
            End If
            Return _FillingGrouping
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
        'validaciones controles de terceros
        If INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue Is Nothing Or INDSleThirdPartyStart.EditValue Is Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        ElseIf INDSleThirdPartyStart.EditValue > INDSleThirdPartyEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        End If
        'validaciones controles de conceptos de expense
        If INDSleConceptsExpendituresStart.EditValue IsNot Nothing And INDSleConceptsExpendituresEnd.EditValue Is Nothing Or INDSleConceptsExpendituresStart.EditValue Is Nothing And INDSleConceptsExpendituresEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblConceptsExpenditures.Text)
            Me.INDSleConceptsExpendituresStart.Focus()
            Validations = False
        ElseIf INDSleConceptsExpendituresStart.EditValue > INDSleConceptsExpendituresEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblConceptsExpenditures.Text)
            Me.INDSleConceptsExpendituresStart.Focus()
            Validations = False
        End If
        'validaciones controles de conceptos de receipts
        If INDSleConceptsReceiptsStart.EditValue IsNot Nothing And INDSleConceptsReceiptsEnd.EditValue Is Nothing Or INDSleConceptsReceiptsStart.EditValue Is Nothing And INDSleConceptsReceiptsEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblConceptsReceipts.Text)
            Me.INDSleConceptsReceiptsStart.Focus()
            Validations = False
        ElseIf INDSleConceptsReceiptsStart.EditValue > INDSleConceptsReceiptsEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblConceptsReceipts.Text)
            Me.INDSleConceptsReceiptsStart.Focus()
            Validations = False
        End If
        Return Validations

    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirdPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReportTreasury)
            INDSleThirdPartyStart.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirdPartyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReportTreasury)
            INDSleThirdPartyEnd.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleConceptsExpendituresStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoExpensesConceptsStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoExpensesConcepts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListExpensesConceptsReport)
            INDSleConceptsExpendituresStart.Datasource = ProoftCloseXpoExpensesConcepts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleConceptsExpendituresEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoExpensesConceptsEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoExpensesConcepts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListExpensesConceptsReport)
            INDSleConceptsExpendituresEnd.Datasource = ProoftCloseXpoExpensesConcepts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleConceptsReceiptsStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoReceiptConceptsStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoReceiptConcepts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListReceiptConceptsReport)
            INDSleConceptsReceiptsStart.Datasource = ProoftCloseXpoReceiptConcepts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleConceptsReceiptsEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoReceiptConceptsEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoReceiptConcepts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListReceiptConceptsReport)
            INDSleConceptsReceiptsEnd.Datasource = ProoftCloseXpoReceiptConcepts
        End Using
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de conceptos de recibos de caja
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasourceCashReceipts() As DataTable

        Dim filtroConsulta As String = "GetDate(IdCashReceipt.DocumentDate) >= #" & Format(INDDeDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(IdCashReceipt.DocumentDate) <= #" & Format(INDDeDateEnd.EditValue, "yyyy-MM-dd") & "#"
        ' si filtra por terceros
        If INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AND IdCashReceipt.IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdCashReceipt.IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
        End If
        'si filtra por conceptos
        If INDSleConceptsReceiptsStart.EditValue IsNot Nothing And INDSleConceptsReceiptsEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AND IdCashReceiptConcept.Code >= '" & INDSleConceptsReceiptsStart.EditValue & "' AND IdCashReceiptConcept.Code <= '" & INDSleConceptsReceiptsEnd.EditValue & "'"
        End If

        If INDGleStatus.EditValue <> 4 Then
            filtroConsulta &= "AND IdCashReceipt.Status = " & INDGleStatus.EditValue
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryCashReceiptDetailsXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Nit Tercero")
        dt.Columns.Add("Nombre Tercero")
        dt.Columns.Add("Código Centro Costo")
        dt.Columns.Add("Nombre Centro Costo")
        dt.Columns.Add("Código Cuenta Contable")
        dt.Columns.Add("Nombre Cuenta Contable")
        dt.Columns.Add("Código Concepto")
        dt.Columns.Add("Nombre Concepto")
        dt.Columns.Add("Naturaleza")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Valor", GetType(Decimal))

        For Each itemView In IndList
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = itemView.IdCashReceipt.Code
            row.Item("Fecha") = itemView.IdCashReceipt.DocumentDate
            row.Item("Nit Tercero") = itemView.IdCashReceipt.IdThirdParty.Nit
            row.Item("Nombre Tercero") = itemView.IdCashReceipt.IdThirdParty.Name
            row.Item("Código Centro Costo") = If(IsNothing(itemView.IdCostCenter) = True, "", itemView.IdCostCenter.Code)
            row.Item("Nombre Centro Costo") = If(IsNothing(itemView.IdCostCenter) = True, "", itemView.IdCostCenter.Name)
            row.Item("Código Cuenta Contable") = itemView.IdMainAccount.Number
            row.Item("Nombre Cuenta Contable") = itemView.IdMainAccount.Name
            row.Item("Código Concepto") = itemView.IdCashReceiptConcept.Code
            row.Item("Nombre Concepto") = itemView.IdCashReceiptConcept.Name
            row.Item("Naturaleza") = If(itemView.Nature = 1, "Debito", "Credito")
            row.Item("Estado") = itemView.IdCashReceipt.Status
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
    ''' creamos un datatable para generar el excel de conceptos de recibos de caja
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasourceVoucherTransaction() As DataTable

        Dim filtroConsulta As String = "GetDate(IdVoucherTransaction.DocumentDate) >= #" & Format(INDDeDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(IdVoucherTransaction.DocumentDate) <= #" & Format(INDDeDateEnd.EditValue, "yyyy-MM-dd") & "#"
        'si filtra por terceros
        If INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AND IdVoucherTransaction.IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdVoucherTransaction.IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
        End If
        'si filtra por conceptos
        If INDSleConceptsExpendituresStart.EditValue IsNot Nothing And INDSleConceptsExpendituresEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AND IdExpenseConcept.Code >= '" & INDSleConceptsExpendituresStart.EditValue & "' AND IdExpenseConcept.Code <= '" & INDSleConceptsExpendituresEnd.EditValue & "'"
        End If

        If INDGleStatus.EditValue <> 4 Then
            filtroConsulta &= "AND IdVoucherTransaction.Status = " & INDGleStatus.EditValue
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryVoucherTransactionDetailsXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Nit Tercero")
        dt.Columns.Add("Nombre Tercero")
        dt.Columns.Add("Código Centro Costo")
        dt.Columns.Add("Nombre Centro Costo")
        dt.Columns.Add("Código Cuenta Contable")
        dt.Columns.Add("Nombre Cuenta Contable")
        dt.Columns.Add("Código Concepto")
        dt.Columns.Add("Nombre Concepto")
        dt.Columns.Add("Naturaleza")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Valor", GetType(Decimal))

        For Each itemView In IndList
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = itemView.IdVoucherTransaction.Code
            row.Item("Fecha") = itemView.IdVoucherTransaction.DocumentDate
            row.Item("Nit Tercero") = If(IsNothing(itemView.IdVoucherTransaction.IdThirdParty) = True, "", itemView.IdVoucherTransaction.IdThirdParty.Nit)
            row.Item("Nombre Tercero") = If(IsNothing(itemView.IdVoucherTransaction.IdThirdParty) = True, "", itemView.IdVoucherTransaction.IdThirdParty.Name)
            row.Item("Código Centro Costo") = If(IsNothing(itemView.IdCostCenter) = True, "", itemView.IdCostCenter.Code)
            row.Item("Nombre Centro Costo") = If(IsNothing(itemView.IdCostCenter) = True, "", itemView.IdCostCenter.Name)
            row.Item("Código Cuenta Contable") = itemView.IdMainAccount.Number
            row.Item("Nombre Cuenta Contable") = itemView.IdMainAccount.Name
            row.Item("Código Concepto") = If(IsNothing(itemView.IdExpenseConcept) = True, "", itemView.IdExpenseConcept.Code)
            row.Item("Nombre Concepto") = If(IsNothing(itemView.IdExpenseConcept) = True, "", itemView.IdExpenseConcept.Description)
            row.Item("Naturaleza") = If(itemView.Nature = 1, "Debito", "Credito")
            row.Item("Estado") = itemView.IdVoucherTransaction.Status
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
        ProoftCloseXpoExpensesConcepts = Nothing
        ProoftCloseXpoReceiptConcepts = Nothing
        ProoftCloseXpoThirdParty = Nothing
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleThirdPartyStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyStart.QueryPopUp
        If INDSleThirdPartyStart.Datasource Is Nothing Then
            LoadXpoThirdPartyStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleThirdPartyEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyEnd.QueryPopUp
        If INDSleThirdPartyEnd.Datasource Is Nothing Then
            LoadXpoThirdPartyEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleConceptsExpendituresStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleConceptsExpendituresStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleConceptsExpendituresStart.QueryPopUp
        If INDSleConceptsExpendituresStart.Datasource Is Nothing Then
            LoadXpoExpensesConceptsStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleConceptsExpendituresEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleConceptsExpendituresEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleConceptsExpendituresEnd.QueryPopUp
        If INDSleConceptsExpendituresEnd.Datasource Is Nothing Then
            LoadXpoExpensesConceptsEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleConceptsReceiptsStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleConceptsReceiptsStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleConceptsReceiptsStart.QueryPopUp
        If INDSleConceptsReceiptsStart.Datasource Is Nothing Then
            LoadXpoReceiptConceptsStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleConceptsReceiptsEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleConceptsReceiptsEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleConceptsReceiptsEnd.QueryPopUp
        If INDSleConceptsReceiptsEnd.Datasource Is Nothing Then
            LoadXpoReceiptConceptsEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportListByConcepts_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleGrouping.Properties.DataSource = FillingGrouping
        Me.INDGleStatus.Properties.DataSource = FillingStatus

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleStatus.EditValue = 4
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleGrouping.EditValue = 1
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
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            If INDGleTypeReport.EditValue = 1 Then
                AsyncLoader(True)
                Dim reporte As New rptReportListByConceptsCashReceipts
                reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                            INDDeDateEnd.EditValue,
                                                            INDSleThirdPartyStart.EditValue,
                                                            INDSleThirdPartyEnd.EditValue,
                                                            INDSleConceptsReceiptsStart.EditValue,
                                                            INDSleConceptsReceiptsEnd.EditValue,
                                                            INDGleStatus.EditValue,
                                                            INDGleGrouping.EditValue}
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
            Else
                AsyncLoader(True)
                Dim reporte As New rptReportListByConceptsVoucherTransaction
                reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                            INDDeDateEnd.EditValue,
                                                            INDSleThirdPartyStart.EditValue,
                                                            INDSleThirdPartyEnd.EditValue,
                                                            INDSleConceptsExpendituresStart.EditValue,
                                                            INDSleConceptsExpendituresEnd.EditValue,
                                                            INDGleStatus.EditValue,
                                                            INDGleGrouping.EditValue}
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
        End If
    End Sub

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 1 Then
            INDLciConceptsExpendituresStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciConceptsExpendituresEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciLabelConceptsExpenditures.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciConceptsReceiptsStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciConceptsReceiptsEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciLabelConceptsReceipts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciConceptsExpendituresStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciConceptsExpendituresEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciLabelConceptsExpenditures.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciConceptsReceiptsStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciConceptsReceiptsEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciLabelConceptsReceipts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportListByConcepts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleConceptsExpendituresStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetExpenseConcept
        Me.INDSleConceptsExpendituresEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetExpenseConcept
        Me.INDSleConceptsReceiptsStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCashReceiptConcept
        Me.INDSleConceptsReceiptsEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCashReceiptConcept

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
            If INDGleTypeReport.EditValue = 1 Then
                INDGcExportExcell.DataSource = chargueDatasourceCashReceipts()
            Else
                INDGcExportExcell.DataSource = chargueDatasourceVoucherTransaction()
            End If
            If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub

#End Region

End Class