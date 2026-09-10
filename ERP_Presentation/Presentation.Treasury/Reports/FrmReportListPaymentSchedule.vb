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

Public Class FrmReportListPaymentSchedule

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoSupplier As XPInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' planilla inicial
    ''' </summary>
    ''' <returns></returns>
    Public Property SchedulePaymentStart As Integer
        Get
            Return INDsleSchedulePaymentStart.EditValue
        End Get
        Set(value As Integer)
            INDsleSchedulePaymentStart.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' planilla final
    ''' </summary>
    ''' <returns></returns>
    Public Property SchedulePaymentEnd As Integer
        Get
            Return INDsleSchedulePaymentEnd.EditValue
        End Get
        Set(value As Integer)
            INDsleSchedulePaymentEnd.EditValue = value
        End Set
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
        'validaciones controles de proveedores
        If INDSleSupplierStart.EditValue IsNot Nothing And INDSleSupplierEnd.EditValue Is Nothing Or INDSleSupplierStart.EditValue Is Nothing And INDSleSupplierEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblSupplier.Text)
            Me.INDSleSupplierStart.Focus()
            Validations = False
        ElseIf INDSleSupplierStart.EditValue > INDSleSupplierEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblSupplier.Text)
            Me.INDSleSupplierStart.Focus()
            Validations = False
        End If
        'validaciones controles de planillla
        If INDsleSchedulePaymentStart.EditValue IsNot Nothing And INDSleSchedulePaymentEnd.EditValue Is Nothing Or INDsleSchedulePaymentStart.EditValue Is Nothing And INDSleSchedulePaymentEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblSupplier.Text)
            Me.INDsleSchedulePaymentStart.Focus()
            Validations = False
        ElseIf INDsleSchedulePaymentStart.EditValue > INDsleSchedulePaymentEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblSupplier.Text)
            Me.INDsleSchedulePaymentStart.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' metodo para Cargar el data source De INDSleSupplierStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSupplierStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoSupplier = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierReportTreasury)
            INDSleSupplierStart.Datasource = ProoftCloseXpoSupplier
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source De INDSleSupplierEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSupplierEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoSupplier = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierReportTreasury)
            INDSleSupplierEnd.Datasource = ProoftCloseXpoSupplier
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source De planilla inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSchedulePaymentStart()
        Using msearch As New MBusqueda
            INDsleSchedulePaymentStart.Properties.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasurySchedulePaymentDetailXpo)(Nothing, Nothing)
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source De planilla final
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoSchedulePaymentEnd()
        Using msearch As New MBusqueda
            INDsleSchedulePaymentEnd.Properties.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasurySchedulePaymentDetailXpo)(Nothing, Nothing)
        End Using
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable

        Dim filtroConsulta As String = "SchedulePaymentId.ScheduledDate >= #" & Format(INDDeDateStart.EditValue, "yyyy-MM-dd") & "# AND SchedulePaymentId.ScheduledDate <= #" & Format(INDDeDateEnd.EditValue, "yyyy-MM-dd") & "#"
        ' si filtra por proveedores
        If INDSleSupplierStart.EditValue IsNot Nothing And INDSleSupplierEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AND SupplierId.IdThirdParty.Nit >= '" & INDSleSupplierStart.EditValue & "' AND SupplierId.IdThirdParty.Nit <= '" & INDSleSupplierEnd.EditValue & "'"
        End If
        'se filtra por planilla
        If SchedulePaymentStart < 0 And SchedulePaymentEnd < 0 Then
            filtroConsulta &= "AND SchedulePayment.Id >= '" & SchedulePaymentStart & "' AND SchedulePayment.Id <= '" & SchedulePaymentEnd & "'"
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasurySchedulePaymentDetailXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Nit Proveedor")
        dt.Columns.Add("Nombre Proveedor")
        dt.Columns.Add("Código Planilla")
        dt.Columns.Add("Numero Factura")
        dt.Columns.Add("Fecha Pago")
        dt.Columns.Add("Vencimiento")
        dt.Columns.Add("Moneda")

        Dim columValue As DataColumn = New DataColumn
        columValue.DataType = System.Type.GetType("System.Decimal")
        columValue.AllowDBNull = False
        columValue.Caption = "Valor"
        columValue.ColumnName = "Valor"
        dt.Columns.Add(columValue)

        For Each itemView In IndList
            Dim row As DataRow = dt.NewRow()
            row.Item("Nit Proveedor") = itemView.SupplierId.IdThirdParty.Nit
            row.Item("Nombre Proveedor") = itemView.SupplierId.IdThirdParty.Name
            row.Item("Código Planilla") = itemView.SchedulePaymentId.Code
            row.Item("Numero Factura") = itemView.AccountPayableId.BillNumber
            row.Item("Fecha Pago") = itemView.SchedulePaymentId.ScheduledDate
            row.Item("Vencimiento") = itemView.AccountPayableId.ExpirationDate
            row.Item("Moneda") = itemView.AccountPayableId.CommonCurrency.CurrencyName
            row.Item("Valor") = itemView.AmountPaid

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
        ProoftCloseXpoSupplier = Nothing
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDPceListSupplier
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplierStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleSupplierStart.QueryPopUp
        If INDSleSupplierStart.Datasource Is Nothing Then
            LoadXpoSupplierStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDPceListSupplier
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplierEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleSupplierEnd.QueryPopUp
        If INDSleSupplierEnd.Datasource Is Nothing Then
            LoadXpoSupplierEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleSchedulePayment_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleSchedulePaymentStart.QueryPopUp
        If INDsleSchedulePaymentStart.Properties.DataSource Is Nothing Then
            LoadXpoSchedulePaymentStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSchedulePaymentEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleSchedulePaymentEnd.QueryPopUp
        If INDsleSchedulePaymentEnd.Properties.DataSource Is Nothing Then
            LoadXpoSchedulePaymentEnd()
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
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerareReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerareReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptReportListPaymentSchedule
            reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                        INDDeDateEnd.EditValue,
                                                        INDSleSupplierStart.EditValue,
                                                        INDSleSupplierEnd.EditValue,
                                                        SchedulePaymentStart,
                                                        SchedulePaymentEnd}
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
    Private Sub FrmReportListPaymentSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleSupplierStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetSupplierByNitThirdParty
        Me.INDSleSupplierEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetSupplierByNitThirdParty
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