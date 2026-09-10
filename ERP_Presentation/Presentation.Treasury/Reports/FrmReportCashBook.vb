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
Imports System.Globalization
#End Region

Public Class FrmReportCashBook

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoCash As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCreationUsers As DevExpress.Data.Linq.LinqInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Detallado"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Resumido"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingPaymentType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingPaymentType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingPaymentType Is Nothing Then
                _FillingPaymentType = New List(Of Tuple(Of Integer, String))
                _FillingPaymentType.Add(New Tuple(Of Integer, String)(1, "Efectivo"))
                _FillingPaymentType.Add(New Tuple(Of Integer, String)(2, "Cheques"))
                _FillingPaymentType.Add(New Tuple(Of Integer, String)(3, "Tarjetas"))
                _FillingPaymentType.Add(New Tuple(Of Integer, String)(4, "Consignación"))
                _FillingPaymentType.Add(New Tuple(Of Integer, String)(5, "Ajustes"))
                _FillingPaymentType.Add(New Tuple(Of Integer, String)(6, "Todos"))
            End If
            Return _FillingPaymentType
        End Get
    End Property

    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Registrados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Confirmados"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Todos"))
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
    ''' metodo para Cargar el data source Del Control INDsleUserStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCreationUsersStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCreationUsers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCreationUsersReportTreasury)
            INDsleUserStart.Datasource = ProoftCloseXpoCreationUsers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDsleUserEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCreationUsersEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCreationUsers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCreationUsersReportTreasury)
            INDsleUserEnd.Datasource = ProoftCloseXpoCreationUsers
        End Using
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable
        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollectionReportCashBook(INDDeDateStart.EditValue, INDDeDateEnd.EditValue, INDCcbStatus.EditValue, INDGlePaymentTypes.EditValue, INDSleCashRegisterStart.EditValue, INDSleCashRegisterEnd.EditValue, INDsleUserStart.EditValue, INDsleUserEnd.EditValue)

        Dim dt As New DataTable
        dt.Columns.Add("Código Caja")
        dt.Columns.Add("Nombre Caja")
        dt.Columns.Add("Nombre Documento")
        dt.Columns.Add("Código Documento")
        dt.Columns.Add("Fecha Documento")
        dt.Columns.Add("Nit Tercero")
        dt.Columns.Add("Nombre Tercero")
        dt.Columns.Add("Detalle")


        Dim columValueInvoice As DataColumn = New DataColumn
        columValueInvoice.DataType = System.Type.GetType("System.Decimal")
        columValueInvoice.AllowDBNull = False
        columValueInvoice.Caption = "Valor Debito"
        columValueInvoice.ColumnName = "Valor Debito"
        dt.Columns.Add(columValueInvoice)

        Dim columValueNote As DataColumn = New DataColumn
        columValueNote.DataType = System.Type.GetType("System.Decimal")
        columValueNote.AllowDBNull = False
        columValueNote.Caption = "Valor Credito"
        columValueNote.ColumnName = "Valor Credito"
        dt.Columns.Add(columValueNote)

        Dim columBalance As DataColumn = New DataColumn
        columBalance.DataType = System.Type.GetType("System.Decimal")
        columBalance.AllowDBNull = False
        columBalance.Caption = "Saldo"
        columBalance.ColumnName = "Saldo"
        dt.Columns.Add(columBalance)

        dt.Columns.Add("Moneda")

        For Each itemView In IndList
            Dim row As DataRow = dt.NewRow()
            row.Item("Código Caja") = If(itemView.CashRegisterCode, itemView.BankAccountCode)
            row.Item("Nombre Caja") = If(itemView.CashRegisterName, itemView.BankAccountName)
            row.Item("Nombre Documento") = itemView.NameVoucher
            row.Item("Código Documento") = itemView.Code
            row.Item("Fecha Documento") = itemView.DocumentDate
            row.Item("Nit Tercero") = itemView.ThirdPartyNit
            row.Item("Nombre Tercero") = itemView.ThirdPartyName
            row.Item("Detalle") = itemView.Detail
            row.Item("Valor Debito") = itemView.ValueDebit
            row.Item("Valor Credito") = itemView.ValueCredit
            row.Item("Saldo") = itemView.NuevoSaldo
            row.Item("Moneda") = itemView.CurrencyAbbreviation

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

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoCash = Nothing
        ProoftCloseXpoCreationUsers = Nothing
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
    ''' se ejecuta en el evento QueryPopUp del Control INDsleUserStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleUserStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleUserStart.QueryPopUp
        If INDsleUserStart.Datasource Is Nothing Then
            LoadXpoCreationUsersStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDsleUserEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleUserEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleUserEnd.QueryPopUp
        If INDsleUserEnd.Datasource Is Nothing Then
            LoadXpoCreationUsersEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDPceListCashRegisters
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceListCashRegisters_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs)

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
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptReportCashBook
            reporte.ParametrosReporte = New Object() {INDDeDateStart.EditValue,
                                                      INDDeDateEnd.EditValue,
                                                      INDSleCashRegisterStart.EditValue,
                                                      INDSleCashRegisterEnd.EditValue,
                                                      INDGlePaymentTypes.EditValue,
                                                      INDGleTypeReport.EditValue,
                                                      INDsleUserStart.EditValue,
                                                      INDsleUserEnd.EditValue,
                                                      INDCcbStatus.EditValue}
            INDDvDocumentViewer.DocumentSource = reporte
            Await reporte.AsyncLoadDatasourceAsync()
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
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportCashBook_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGlePaymentTypes.Properties.DataSource = FillingPaymentType
        'Me.INDGleStatus.Properties.DataSource = FillingStatus

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGlePaymentTypes.EditValue = 6
        'Me.INDGleStatus.EditValue = 2
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportCashBook_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleCashRegisterStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetcashRegister
        Me.INDSleCashRegisterEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetcashRegister
        Me.INDsleUserStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.ConsultarUsuarioCodigo
        Me.INDsleUserEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.ConsultarUsuarioCodigo
        INDSleCashRegisterStart.View.OptionsView.ShowGroupPanel = False
        INDSleCashRegisterEnd.View.OptionsView.ShowGroupPanel = False
        INDsleUserStart.View.OptionsView.ShowGroupPanel = False
        INDsleUserEnd.View.OptionsView.ShowGroupPanel = False
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