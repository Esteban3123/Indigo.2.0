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

Public Class FrmReportCheckList

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource
    Public Property ProoftCloseXpoVoucher As XPInstantFeedbackSource
    Public Property ProoftCloseXpoBankAccount As XPInstantFeedbackSource
    Private SelectAllThirdParty = True
    Private SelectAllVoucher = True

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
        Return Validations

    End Function

    ''' <summary>
    ''' metodo para Cargar el data source De INDSleThirdPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReportTreasury)
            INDSleThirdPartyStart.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source De INDSleThirdPartyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReportTreasury)
            INDSleThirdPartyEnd.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source De INDSleDocumentStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoVoucherStart()
        Dim criteria As String = Nothing
        If (INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing) Then
            criteria = "IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
        End If
        Using msearch As New MBusqueda
            ProoftCloseXpoVoucher = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListVoucherTranscationCheckReportTreasuryByFilter, criteria)
            INDSleDocumentStart.Datasource = ProoftCloseXpoVoucher
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source De INDSleDocumentEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoVoucherEnd()
        Dim criteria As String = Nothing
        If (INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing) Then
            criteria = "IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
        End If
        Using msearch As New MBusqueda
            ProoftCloseXpoVoucher = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListVoucherTranscationCheckReportTreasuryByFilter, criteria)
            INDSleDocumentEnd.Datasource = ProoftCloseXpoVoucher
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source De la lista de cuentas bancarias
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBankAccount()
        Using msearch As New MBusqueda
            ProoftCloseXpoBankAccount = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEntityBankReportTreasury)
            INDSleEntityBankAccount.Properties.DataSource = ProoftCloseXpoBankAccount
        End Using
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable

        Dim filtroConsulta As String = "GetDate(DocumentDate) >= #" & Format(INDDeDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDDeDateEnd.EditValue, "yyyy-MM-dd") & "# AND IdChecks is not null "
        ' si filtra por terceros
        If INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AND IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
        End If
        'si filtra por documentos
        If INDSleDocumentStart.EditValue IsNot Nothing And INDSleDocumentEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "AND Code >= '" & INDSleDocumentStart.EditValue & "' AND Code <= '" & INDSleDocumentEnd.EditValue & "'"
        End If

        If INDSleEntityBankAccount.EditValue IsNot Nothing Then
            filtroConsulta &= "AND IdEntityBankAccount.Code = '" & INDSleEntityBankAccount.EditValue & "'"
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryVoucherTransactionXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Ciudad")
        dt.Columns.Add("Fecha")
        dt.Columns.Add("Nit Tercero")
        dt.Columns.Add("Nombre Tercero")

        Dim columValue As DataColumn = New DataColumn
        columValue.DataType = System.Type.GetType("System.Decimal")
        columValue.AllowDBNull = False
        columValue.Caption = "Valor"
        columValue.ColumnName = "Valor"
        dt.Columns.Add(columValue)

        For Each itemView In IndList
            Dim row As DataRow = dt.NewRow()
            row.Item("Ciudad") = itemView.IdUnitOperative.UnitName
            row.Item("Fecha") = itemView.TransactionDate
            row.Item("Nit Tercero") = If(IsNothing(itemView.IdThirdParty) = True, "", itemView.IdThirdParty.Nit)
            row.Item("Nombre Tercero") = If(IsNothing(itemView.IdThirdParty) = True, "", itemView.IdThirdParty.Name)
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
        ProoftCloseXpoBankAccount = Nothing
        ProoftCloseXpoThirdParty = Nothing
        ProoftCloseXpoVoucher = Nothing
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleEntityBankAccount
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEntityBankAccount_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleEntityBankAccount.QueryPopUp
        If INDSleEntityBankAccount.Properties.DataSource Is Nothing Then
            LoadXpoBankAccount()
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
            LoadXpoVoucherStart()
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
            LoadXpoVoucherEnd()
        End If
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
            Dim reporte As New rptReportCheckList
            reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                            INDDeDateEnd.EditValue,
                                                            INDSleDocumentStart.EditValue,
                                                            INDSleDocumentEnd.EditValue,
                                                            INDSleThirdPartyStart.EditValue,
                                                            INDSleThirdPartyEnd.EditValue,
                                                            INDSleEntityBankAccount.EditValue}
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
    Private Sub FrmReportCheckList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleDocumentStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetVoucherTransactionByCode
        Me.INDSleDocumentEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetVoucherTransactionByCode
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        INDSleEntityBankAccount.Properties.Buttons(1).Visible = False
    End Sub

    ''' <summary>
    ''' vuelve a cargar el datasource de los comprobantes de egreso que se pagaron con cheques filtrado por terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleThirdPartyStart.EditValueChanged
        LoadXpoVoucherStart()
        LoadXpoVoucherEnd()
    End Sub

    ''' <summary>
    ''' vuelve a cargar el datasource de los comprobantes de egreso que se pagaron con cheques filtrado por terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleThirdPartyEnd.EditValueChanged
        LoadXpoVoucherStart()
        LoadXpoVoucherEnd()
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