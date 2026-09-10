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

Public Class FrmReportConsignment

#Region "Fields"
    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon
#End Region

#Region "Properties"
    Public Property ProoftCloseXpoConsignment As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCurrentAccountSavings As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCash As XPInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private _FillingTypeSearch As List(Of Tuple(Of String, String))

    Private _FillingGrouping As List(Of Tuple(Of Integer, String))

    Private _FillingStatus As List(Of Tuple(Of Integer, String))
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoCash = Nothing
        ProoftCloseXpoConsignment = Nothing
        ProoftCloseXpoCurrentAccountSavings = Nothing
        _FillingGrouping = Nothing
        _FillingStatus = Nothing
        _FillingTypeSearch = Nothing
    End Sub
    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport1.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptReportConsignmentTransfer
            reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                            INDDeDateEnd.EditValue,
                                                            INDSleDocumentStart.EditValue,
                                                            INDSleDocumentEnd.EditValue,
                                                            INDGleGrouping.EditValue,
                                                            INDGleStatus.EditValue,
                                                            INDGleTypeSearch.EditValue,
                                                            INDSleCurrentAccountSavingStart.EditValue,
                                                            INDSleCurrentAccountSavingEnd.EditValue,
                                                            INDSleCashStart.EditValue,
                                                            INDSleCashEnd.EditValue}
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
    Private Sub FrmReportConsignment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleDocumentStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetVReportConsignmentTransferByCode
        Me.INDSleDocumentEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetVReportConsignmentTransferByCode
        Me.INDSleCashStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetcashRegister
        Me.INDSleCashEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetcashRegister
        Me.INDSleCurrentAccountSavingStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetEntityBankAccount
        Me.INDSleCurrentAccountSavingEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetEntityBankAccount
        Me.ToolBar.Visible = False
    End Sub

    ''' <summary>
    ''' vuelve a cargar el datasource de las consignaciones y traslados filtrado por estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleStatus_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleStatus.EditValueChanged
        LoadXpoConsignmentStart()
        LoadXpoConsignmentEnd()
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportConsignment_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeSearch.Properties.DataSource = FillingTypeSearch
        Me.INDGleGrouping.Properties.DataSource = FillingGrouping
        Me.INDGleStatus.Properties.DataSource = FillingStatus

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeSearch.EditValue = "Todos"
        Me.INDGleStatus.EditValue = 5
        Me.INDGleGrouping.EditValue = 2
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
    ''' se ejecuta en el evento querypopup del control INDSleDocumentStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocumentStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleDocumentStart.QueryPopUp
        If INDSleDocumentStart.Datasource Is Nothing Then
            LoadXpoConsignmentStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleDocumentEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocumentEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleDocumentEnd.QueryPopUp
        If INDSleDocumentEnd.Datasource Is Nothing Then
            LoadXpoConsignmentEnd()
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

#Region "Method"
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

    Private ReadOnly Property FillingTypeSearch As List(Of Tuple(Of String, String))
        Get
            If _FillingTypeSearch Is Nothing Then
                _FillingTypeSearch = New List(Of Tuple(Of String, String))
                _FillingTypeSearch.Add(New Tuple(Of String, String)("Todos", "Todos"))
                _FillingTypeSearch.Add(New Tuple(Of String, String)("Consignación", "Consignaciones"))
                _FillingTypeSearch.Add(New Tuple(Of String, String)("Traslado", "Traslados"))
            End If
            Return _FillingTypeSearch
        End Get
    End Property

    Private ReadOnly Property FillingGrouping As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGrouping Is Nothing Then
                _FillingGrouping = New List(Of Tuple(Of Integer, String))
                _FillingGrouping.Add(New Tuple(Of Integer, String)(1, "Ninguno"))
                _FillingGrouping.Add(New Tuple(Of Integer, String)(2, "Cuenta/Caja"))
                _FillingGrouping.Add(New Tuple(Of Integer, String)(3, "Tipo"))
                _FillingGrouping.Add(New Tuple(Of Integer, String)(4, "Banco"))
            End If
            Return _FillingGrouping
        End Get
    End Property

    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Registrados (Sin Confirmar)"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Confirmados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Anulados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Reversados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(5, "Todos"))
            End If
            Return _FillingStatus
        End Get
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
        'validaciones controles de documentos
        If INDSleDocumentStart.EditValue IsNot Nothing And INDSleDocumentEnd.EditValue Is Nothing Or INDSleDocumentStart.EditValue Is Nothing And INDSleDocumentEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblDocument.Text)
            Me.INDSleDocumentStart.Focus()
            Validations = False
        ElseIf INDSleDocumentStart.EditValue > INDSleDocumentEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblDocument.Text)
            Me.INDSleDocumentStart.Focus()
            Validations = False
        End If
        'validaciones Cuentas
        'If INDSleCurrentAccountSavingStart.EditValue Is Nothing And INDSleCurrentAccountSavingEnd.EditValue Is Nothing And INDSleCashStart.EditValue Is Nothing And INDSleCashEnd.EditValue Is Nothing Then
        '    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportTreasuryNewsletter_Filter", "Treasury"))
        '    Me.INDSleCurrentAccountSavingStart.Focus()
        '    Validations = False
        If INDSleCurrentAccountSavingStart.EditValue IsNot Nothing And INDSleCurrentAccountSavingEnd.EditValue Is Nothing Or INDSleCurrentAccountSavingStart.EditValue Is Nothing And INDSleCurrentAccountSavingEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblAccount.Text)
            Me.INDSleCurrentAccountSavingStart.Focus()
            Validations = False
        ElseIf INDSleCurrentAccountSavingStart.EditValue > INDSleCurrentAccountSavingEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblAccount.Text)
            Me.INDSleCurrentAccountSavingStart.Focus()
            Validations = False
            'validaciones Caja
        ElseIf INDSleCashStart.EditValue IsNot Nothing And INDSleCashEnd.EditValue Is Nothing Or INDSleCashStart.EditValue Is Nothing And INDSleCashEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCash.Text)
            Me.INDSleCashStart.Focus()
            Validations = False
        ElseIf INDSleCashStart.EditValue > INDSleCashEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCash.Text)
            Me.INDSleCashStart.Focus()
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
    ''' metodo para Cargar el data source Del Control INDSleDocumentStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoConsignmentStart()
        Dim criteria As String = Nothing
        If (INDGleStatus.EditValue IsNot Nothing And INDGleStatus.EditValue <> 5) Then
            criteria = "Status = " & INDGleStatus.EditValue
        End If
        Using msearch As New MBusqueda
            ProoftCloseXpoConsignment = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListVReportConsignmentTransferTreasuryByFilter, criteria)
            INDSleDocumentStart.Datasource = ProoftCloseXpoConsignment
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleDocumentEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoConsignmentEnd()
        Dim criteria As String = Nothing
        If (INDGleStatus.EditValue IsNot Nothing And INDGleStatus.EditValue <> 5) Then
            criteria = "Status = " & INDGleStatus.EditValue
        End If
        Using msearch As New MBusqueda
            ProoftCloseXpoConsignment = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListVReportConsignmentTransferTreasuryByFilter, criteria)
            INDSleDocumentEnd.Datasource = ProoftCloseXpoConsignment
        End Using
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable

        Dim filtroConsulta As String = "GetDate(DocumentDate) >= #" & Format(INDDeDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDDeDateEnd.EditValue, "yyyy-MM-dd") & "#"

        'si filtra por documentos
        If INDSleDocumentStart.EditValue IsNot Nothing And INDSleDocumentEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AND Code >= '" & INDSleDocumentStart.EditValue & "' AND Code <= '" & INDSleDocumentEnd.EditValue & "'"
        End If

        If INDGleTypeSearch.EditValue <> "Todos" Then
            filtroConsulta &= "AND Type = '" & INDGleTypeSearch.EditValue & "'"
        End If

        If INDGleStatus.EditValue <> 5 Then
            filtroConsulta &= "AND Status = " & INDGleStatus.EditValue
        End If
        'filtro para agrupamiento (Banco), retira los campos en blanco
        If INDGleGrouping.EditValue = 4 Then
            filtroConsulta &= "AND CodeBank != ''"
        End If

        'filtro por Cuenta
        If INDSleCurrentAccountSavingStart.EditValue IsNot Nothing And INDSleCurrentAccountSavingEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AND CodeBank >= '" & INDSleCurrentAccountSavingStart.EditValue & "' AND CodeBank <= '" & INDSleCurrentAccountSavingEnd.EditValue & "'"
        End If

        'filtro por Caja
        If INDSleCashStart.EditValue IsNot Nothing And INDSleCashEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AND CodeCash != '' AND CodeCash >= '" & INDSleCashStart.EditValue & "' AND CodeCash <= '" & INDSleCashEnd.EditValue & "'"
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryVReportConsignmentTransfer)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha")
        dt.Columns.Add("Tipo")
        dt.Columns.Add("Código Caja / Banco")
        dt.Columns.Add("Nombre Caja / Banco")
        dt.Columns.Add("Detalle")
        dt.Columns.Add("Estado")

        Dim columValueInvoice As DataColumn = New DataColumn
        columValueInvoice.DataType = System.Type.GetType("System.Decimal")
        columValueInvoice.AllowDBNull = False
        columValueInvoice.Caption = "Valor"
        columValueInvoice.ColumnName = "Valor"
        dt.Columns.Add(columValueInvoice)

        For Each itemView In IndList
            Dim codeCashBank As String
            Dim nameCashBank As String
            If itemView.CodeCash = String.Empty Then
                codeCashBank = itemView.CodeBank
                nameCashBank = itemView.NameBank
            Else
                codeCashBank = itemView.CodeCash
                nameCashBank = itemView.NameBank
            End If
            Dim statusName As String = String.Empty
            Select Case itemView.Status
                Case 1
                    statusName = "Registrado"
                Case 2
                    statusName = "Confirmado"
                Case 3
                    statusName = "Anulado"
                Case 4
                    statusName = "Reversado"
            End Select
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = itemView.Code
            row.Item("Fecha") = itemView.DocumentDate
            row.Item("Tipo") = itemView.Type
            row.Item("Código Caja / Banco") = codeCashBank
            row.Item("Nombre Caja / Banco") = nameCashBank
            row.Item("Detalle") = itemView.Detail
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

End Class