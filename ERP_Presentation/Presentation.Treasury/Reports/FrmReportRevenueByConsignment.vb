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

Public Class FrmReportRevenueByConsignment

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"
    Public Property ProoftCloseXpoAccounts As XPInstantFeedbackSource
    Public Property ProoftCloseXpoCreationUsers As LinqInstantFeedbackSource

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
    ''' metodo para Cargar el data source Del Control INDSleAccountsStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountsStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoAccounts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEntityBankAccountsReport)
            INDSleAccountsStart.Datasource = ProoftCloseXpoAccounts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountsStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountsEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoAccounts = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEntityBankAccountsReport)
            INDSleAccountsEnd.Datasource = ProoftCloseXpoAccounts
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCreationUsersStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCreationUsersStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoCreationUsers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCreationUsersReportTreasury)
            INDSleCreationUsersStart.Datasource = ProoftCloseXpoCreationUsers
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCreationUsersEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCreationUsersEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoCreationUsers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCreationUsersReportTreasury)
            INDSleCreationUsersEnd.Datasource = ProoftCloseXpoCreationUsers
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

        'Valida Cuentas
        If INDSleAccountsStart.EditValue Is Nothing And INDSleAccountsEnd.EditValue IsNot Nothing Or INDSleAccountsEnd.EditValue Is Nothing And INDSleAccountsStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblAccounts.Text)
            Me.INDSleAccountsStart.Focus()
            Validations = False
        ElseIf INDSleAccountsEnd.EditValue < INDSleAccountsStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblAccounts.Text)
            Me.INDSleAccountsStart.Focus()
            Validations = False
        End If

        'Valida Usuarios
        If INDSleCreationUsersStart.EditValue Is Nothing And INDSleCreationUsersEnd.EditValue IsNot Nothing Or INDSleCreationUsersEnd.EditValue Is Nothing And INDSleCreationUsersStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCreationUsers.Text)
            Me.INDSleCreationUsersStart.Focus()
            Validations = False
        ElseIf INDSleCreationUsersEnd.EditValue < INDSleCreationUsersStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCreationUsers.Text)
            Me.INDSleCreationUsersStart.Focus()
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

        Dim filtroConsulta As String = "GetDate(IdCashReceipt.DocumentDate) >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(IdCashReceipt.DocumentDate) <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd") & "# AND PaymentMethodTypes = 4"

        'Se filtra por Cuentas
        If INDSleAccountsStart.EditValue IsNot Nothing And INDSleAccountsEnd.EditValue IsNot Nothing Then
            filtroConsulta &= " AND IdEntityBankAccount.Code >= '" & INDSleAccountsStart.EditValue & "' AND IdEntityBankAccount.Code <= '" & INDSleAccountsEnd.EditValue & "'"
        End If

        'Se filtra por Usuarios
        If INDSleCreationUsersStart.EditValue IsNot Nothing And INDSleCreationUsersEnd.EditValue IsNot Nothing Then
            filtroConsulta &= " AND IdCashReceipt.CreationUser >= '" & INDSleCreationUsersStart.EditValue & "' AND IdCashReceipt.CreationUser <= '" & INDSleCreationUsersEnd.EditValue & "'"
        End If

        If INDGleStatusReport.EditValue <> 4 Then
            filtroConsulta &= "AND IdCashReceipt.Status = " & INDGleStatusReport.EditValue
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryPaymentMethodsXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("No Recibo")
        dt.Columns.Add("Fecha")
        dt.Columns.Add("Código Banco")
        dt.Columns.Add("Nombre Banco")
        dt.Columns.Add("Nit Tercero")
        dt.Columns.Add("Nombre Tercero")
        dt.Columns.Add("Código Cuenta Contable")
        dt.Columns.Add("Nombre Cuenta Contable")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Consignación")

        Dim columValue As DataColumn = New DataColumn
        columValue.DataType = System.Type.GetType("System.Decimal")
        columValue.AllowDBNull = False
        columValue.Caption = "Valor"
        columValue.ColumnName = "Valor"
        dt.Columns.Add(columValue)

        For Each itemView In IndList
            Dim row As DataRow = dt.NewRow()
            row.Item("No Recibo") = itemView.IdCashReceipt.Code
            row.Item("Fecha") = itemView.IdCashReceipt.DocumentDate
            row.Item("Código Banco") = If(IsNothing(itemView.IdBank) = True, "", itemView.IdBank.Code)
            row.Item("Nombre Banco") = If(IsNothing(itemView.IdBank) = True, "", itemView.IdBank.Name)
            row.Item("Nit Tercero") = If(IsNothing(itemView.IdCashReceipt.IdThirdParty) = True, "", itemView.IdCashReceipt.IdThirdParty.Nit)
            row.Item("Nombre Tercero") = If(IsNothing(itemView.IdCashReceipt.IdThirdParty) = True, "", itemView.IdCashReceipt.IdThirdParty.Name)
            row.Item("Código Cuenta Contable") = If(IsNothing(itemView.IdEntityBankAccount) = True, "", itemView.IdEntityBankAccount.IdMainAccount.Number)
            row.Item("Nombre Cuenta Contable") = If(IsNothing(itemView.IdEntityBankAccount) = True, "", itemView.IdEntityBankAccount.IdMainAccount.Name)
            row.Item("Estado") = itemView.IdCashReceipt.Status
            row.Item("Consignación") = itemView.DepositNumber
            row.Item("Valor") = itemView.IdCashReceipt.Value

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
        ProoftCloseXpoAccounts = Nothing
        ProoftCloseXpoCreationUsers = Nothing
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
            Dim reporte As New rptRevenueByConsignment

            reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue, INDGleStatusReport.EditValue,
                                         INDSleAccountsStart.EditValue, INDSleAccountsEnd.EditValue,
                                         INDSleCreationUsersStart.EditValue, INDSleCreationUsersEnd.EditValue}

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
    Private Sub FrmReportRevenueByConsignment_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar el GridLookUpEdit
        Me.INDGleStatusReport.Properties.DataSource = FillingStatus

        'Asigna un valor por defecto a GridLookUpEdit
        Me.INDGleStatusReport.EditValue = 4

    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleAccountStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountsStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountsStart.QueryPopUp
        If INDSleAccountsStart.Datasource Is Nothing Then
            LoadXpoAccountsStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleAccountEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountsEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountsEnd.QueryPopUp
        If INDSleAccountsEnd.Datasource Is Nothing Then
            LoadXpoAccountsEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCreationUserStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCreationUsersStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCreationUsersStart.QueryPopUp
        If INDSleCreationUsersStart.Datasource Is Nothing Then
            LoadXpoCreationUsersStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleCreationUserEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCreationUsersEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCreationUsersEnd.QueryPopUp
        If INDSleCreationUsersEnd.Datasource Is Nothing Then
            LoadXpoCreationUsersEnd()
        End If
    End Sub

    Private Sub FrmReportRevenueByConsignment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleAccountsStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetEntityBankAccount
        Me.INDSleAccountsEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetEntityBankAccount
        Me.INDSleCreationUsersStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.ConsultarUsuarioCodigo
        Me.INDSleCreationUsersEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.ConsultarUsuarioCodigo
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