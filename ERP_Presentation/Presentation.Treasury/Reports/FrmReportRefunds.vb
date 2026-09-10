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

Public Class FrmReportRefunds

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoRefunds As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCash As XPInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

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

        'validaciones controles de documentos
        If INDSleRefundStart.EditValue IsNot Nothing And INDSleRefundEnd.EditValue Is Nothing Or INDSleRefundStart.EditValue Is Nothing And INDSleRefundEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblRefunds.Text)
            Me.INDSleRefundStart.Focus()
            Validations = False
        ElseIf INDSleRefundStart.EditValue > INDSleRefundEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblRefunds.Text)
            Me.INDSleRefundStart.Focus()
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

        Return Validations

    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleRefundStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoRefundStart()
        Dim criteria As String = Nothing
        If (INDGleStatus.EditValue IsNot Nothing And INDGleStatus.EditValue <> 4) Then
            criteria = "Status = " & INDGleStatus.EditValue
        End If
        Using msearch As New MBusqueda
            ProoftCloseXpoRefunds = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRefundsReportTreasuryByFilter, criteria)
            INDSleRefundStart.Datasource = ProoftCloseXpoRefunds
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleRefundEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoRefundEnd()
        Dim criteria As String = Nothing
        If (INDGleStatus.EditValue IsNot Nothing And INDGleStatus.EditValue <> 4) Then
            criteria = "Status = " & INDGleStatus.EditValue
        End If
        Using msearch As New MBusqueda
            ProoftCloseXpoRefunds = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRefundsReportTreasuryByFilter, criteria)
            INDSleRefundEnd.Datasource = ProoftCloseXpoRefunds
        End Using
    End Sub

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
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable

        Dim filtroConsulta As String = "GetDate(InitialDate) >= #" & Format(INDDeDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(InitialDate) <= #" & Format(INDDeDateEnd.EditValue, "yyyy-MM-dd") & "#"

        'si filtra por documentos
        If INDSleRefundStart.EditValue IsNot Nothing And INDSleRefundEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AND Code >= '" & INDSleRefundStart.EditValue & "' AND Code <= '" & INDSleRefundEnd.EditValue & "'"
        End If

        If INDGleStatus.EditValue <> 4 Then
            filtroConsulta &= "AND Status = " & INDGleStatus.EditValue
        End If

        If INDSleCashRegisterStart.EditValue IsNot Nothing And INDSleCashRegisterEnd IsNot Nothing Then
            filtroConsulta &= " AND IdCashRegister.Code >= '" & INDSleCashRegisterStart.EditValue & "' AND IdCashRegister.Code <= '" & INDSleCashRegisterEnd.EditValue & "'"
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryRefundsXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha Inicial")
        dt.Columns.Add("Fecha Final")
        dt.Columns.Add("Código Caja")
        dt.Columns.Add("Nombre Caja")
        dt.Columns.Add("Detalle")
        dt.Columns.Add("Reembolsado")
        dt.Columns.Add("Estado")

        Dim columValueInvoice As DataColumn = New DataColumn
        columValueInvoice.DataType = System.Type.GetType("System.Decimal")
        columValueInvoice.AllowDBNull = False
        columValueInvoice.Caption = "Valor"
        columValueInvoice.ColumnName = "Valor"
        dt.Columns.Add(columValueInvoice)

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
            Dim refundsName As String = String.Empty
            Select Case itemView.Refunded
                Case 1
                    refundsName = "Si"
                Case Else
                    refundsName = "No"
            End Select
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = itemView.Code
            row.Item("Fecha Inicial") = itemView.InitialDate
            row.Item("Fecha Final") = itemView.FinalDate
            row.Item("Código Caja") = itemView.IdCashRegister.Code
            row.Item("Nombre Caja") = itemView.IdCashRegister.Name
            row.Item("Detalle") = itemView.Detail
            row.Item("Reembolsado") = refundsName
            row.Item("Estado") = statusName
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
        ProoftCloseXpoCash = Nothing
        ProoftCloseXpoRefunds = Nothing
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleRefundStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleRefundStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleRefundStart.QueryPopUp
        If INDSleRefundStart.Datasource Is Nothing Then
            LoadXpoRefundStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleRefundEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleRefundEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleRefundEnd.QueryPopUp
        If INDSleRefundEnd.Datasource Is Nothing Then
            LoadXpoRefundEnd()
        End If
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
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportRefunds_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleStatus.Properties.DataSource = FillingStatus
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleStatus.EditValue = 4
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
            AsyncLoader(True)
            Dim reporte As New rptReportRefunds
            reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                        INDDeDateEnd.EditValue,
                                                        INDSleRefundStart.EditValue,
                                                        INDSleRefundEnd.EditValue,
                                                        INDGleStatus.EditValue,
                                                        INDSleCashRegisterStart.EditValue,
                                                        INDSleCashRegisterEnd.EditValue}
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
    Private Sub FrmReportRefunds_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleRefundStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetRefund
        Me.INDSleRefundEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetRefund
        Me.INDSleCashRegisterStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetcashRegister
        Me.INDSleCashRegisterEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetcashRegister
        INDSleCashRegisterStart.View.OptionsView.ShowGroupPanel = False
        INDSleCashRegisterEnd.View.OptionsView.ShowGroupPanel = False
        INDSleRefundStart.View.OptionsView.ShowGroupPanel = False
        INDSleRefundEnd.View.OptionsView.ShowGroupPanel = False
    End Sub

    ''' <summary>
    ''' vuelve a cargar el datasource de los reembolsos filtrado por estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleStatus_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleStatus.EditValueChanged
        LoadXpoRefundStart()
        LoadXpoRefundEnd()
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