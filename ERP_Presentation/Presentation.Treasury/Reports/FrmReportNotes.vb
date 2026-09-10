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

Public Class FrmReportNotes

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
    Public Property ProoftCloseXpoNote As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCurrency As XPInstantFeedbackSource
    Private _FillingGrouping As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingGrouping As List(Of Tuple(Of Integer, String))
        Get
            If _FillingGrouping Is Nothing Then
                _FillingGrouping = New List(Of Tuple(Of Integer, String))
                _FillingGrouping.Add(New Tuple(Of Integer, String)(1, "Ninguno"))
                _FillingGrouping.Add(New Tuple(Of Integer, String)(2, "Cuenta/Caja"))
                _FillingGrouping.Add(New Tuple(Of Integer, String)(3, "Naturaleza"))
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
    ''' <summary>
    ''' propiedad para almacenar la moneda
    ''' </summary>
    ''' <returns></returns>
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

        If CurrencyId <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CurrencyVoid", "Commons"))
            Me.INDsleCurrency.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleDocumentStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoNoteStart()
        Dim criteria As String = Nothing
        If (INDGleStatus.EditValue IsNot Nothing And INDGleStatus.EditValue <> 4) Then
            criteria = "Status = " & INDGleStatus.EditValue
        End If
        Using msearch As New MBusqueda
            ProoftCloseXpoNote = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListNoteReportTreasuryByFilter, criteria)
            INDSleDocumentStart.Datasource = ProoftCloseXpoNote
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleDocumentEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoNoteEnd()
        Dim criteria As String = Nothing
        If (INDGleStatus.EditValue IsNot Nothing And INDGleStatus.EditValue <> 4) Then
            criteria = "Status = " & INDGleStatus.EditValue
        End If
        Using msearch As New MBusqueda
            ProoftCloseXpoNote = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListNoteReportTreasuryByFilter, criteria)
            INDSleDocumentEnd.Datasource = ProoftCloseXpoNote
        End Using
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable

        Dim filtroConsulta As String = "TreasuryNoteId.NoteDate >= #" & Format(INDDeDateStart.EditValue, "yyyy-MM-dd") & "# AND TreasuryNoteId.NoteDate <= #" & Format(INDDeDateEnd.EditValue, "yyyy-MM-dd") & "#"

        'si filtra por documentos
        If INDSleDocumentStart.EditValue IsNot Nothing And INDSleDocumentEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AND TreasuryNoteId.Code >= '" & INDSleDocumentStart.EditValue & "' AND TreasuryNoteId.Code <= '" & INDSleDocumentEnd.EditValue & "'"
        End If

        If INDGleStatus.EditValue <> 4 Then
            filtroConsulta &= "AND TreasuryNoteId.Status = " & INDGleStatus.EditValue
        End If

        If CurrencyId > 0 Then
            filtroConsulta &= " AND TreasuryNoteId.CurrencyId = " & CurrencyId
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryNotesDetailXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha")
        dt.Columns.Add("Naturaleza")
        dt.Columns.Add("Nit Tercero")
        dt.Columns.Add("Nombre Tercero")
        dt.Columns.Add("Código Cuenta Contable")
        dt.Columns.Add("Nombre Cuenta Contable")
        dt.Columns.Add("Código Caja / Banco")
        dt.Columns.Add("Nombre Caja / Banco")
        dt.Columns.Add("Detalle")

        Dim columValueInvoice As DataColumn = New DataColumn
        columValueInvoice.DataType = System.Type.GetType("System.Decimal")
        columValueInvoice.AllowDBNull = False
        columValueInvoice.Caption = "Valor"
        columValueInvoice.ColumnName = "Valor"
        dt.Columns.Add(columValueInvoice)
        dt.Columns.Add("Moneda")

        For Each itemView In IndList
            Dim codeCashBank As String
            Dim nameCashBank As String
            If itemView.TreasuryNoteId.CashRegisterId Is Nothing Then
                codeCashBank = itemView.TreasuryNoteId.EntityBankAccountId.Code
                nameCashBank = itemView.TreasuryNoteId.EntityBankAccountId.IdBank.Name
            Else
                codeCashBank = itemView.TreasuryNoteId.CashRegisterId.Code
                nameCashBank = itemView.TreasuryNoteId.CashRegisterId.Name
            End If
            Dim natureText As String = String.Empty
            Select Case itemView.Nature
                Case 1
                    natureText = "Debito"
                Case 2
                    natureText = "Credito"
            End Select

            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = itemView.TreasuryNoteId.Code
            row.Item("Fecha") = itemView.TreasuryNoteId.NoteDate
            row.Item("Naturaleza") = natureText
            row.Item("Nit Tercero") = itemView.ThirdPartyId?.Nit
            row.Item("Nombre Tercero") = itemView.ThirdPartyId?.Name
            row.Item("Código Cuenta Contable") = itemView.MainAccountId.Number
            row.Item("Nombre Cuenta Contable") = itemView.MainAccountId.Name
            row.Item("Código Caja / Banco") = codeCashBank
            row.Item("Nombre Caja / Banco") = nameCashBank
            row.Item("Detalle") = itemView.TreasuryNoteId.Description
            row.Item("Valor") = itemView.Value
            row.Item("Moneda") = itemView.TreasuryNoteId.CurrencyAbbreviation
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

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCurrency
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCurrency()
        ProoftCloseXpoCurrency = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.GetCurrency()
        INDsleCurrency.Properties.DataSource = ProoftCloseXpoCurrency
    End Sub

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoNote = Nothing
        ProoftCloseXpoCurrency = Nothing
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleDocumentStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocumentStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleDocumentStart.QueryPopUp
        If INDSleDocumentStart.Datasource Is Nothing Then
            LoadXpoNoteStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleDocumentStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocumentEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleDocumentEnd.QueryPopUp
        If INDSleDocumentEnd.Datasource Is Nothing Then
            LoadXpoNoteEnd()
        End If
    End Sub

    ''' <summary>
    ''' popup para mostrar las monedas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If INDsleCurrency.Properties.DataSource Is Nothing Then
            LoadXpoCurrency()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al mostar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportNotes_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleGrouping.Properties.DataSource = FillingGrouping
        Me.INDGleStatus.Properties.DataSource = FillingStatus

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleStatus.EditValue = 4
        Me.INDGleGrouping.EditValue = 2
        LoadXpoCurrency()
        CurrencyId = IndigoSessionValues.OfficialCurrencyId
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
    Private Async Function INDSbGenerateReport_ClickAsync(sender As Object, e As EventArgs) As Task Handles INDSbGenerateReport.Click

        If CurrencyId > 0 Then
            Using model As New MCurrency(MyBase.Tag)
                _currency = Await model.GetCurrencyById(CurrencyId)
            End Using
        End If

        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptReportNotes
            reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                        INDDeDateEnd.EditValue,
                                                        INDSleDocumentStart.EditValue,
                                                        INDSleDocumentEnd.EditValue,
                                                        INDGleStatus.EditValue,
                                                        INDGleGrouping.EditValue}
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
    End Function

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportNotes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleDocumentStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetTreasuryNote
        Me.INDSleDocumentEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetTreasuryNote
    End Sub

    ''' <summary>
    ''' vuelve a cargar el datasource de las notas filtrado por el estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleStatus_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleStatus.EditValueChanged
        LoadXpoNoteStart()
        LoadXpoNoteEnd()
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