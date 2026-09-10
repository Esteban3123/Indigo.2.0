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

Public Class FrmReportListSummaryCashReceipts

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"
    Public Property ProoftCloseXpoCreationUsers As LinqInstantFeedbackSource
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

    Private _FillingTypePayments As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypePayments As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypePayments Is Nothing Then
                _FillingTypePayments = New List(Of Tuple(Of Integer, String))
                _FillingTypePayments.Add(New Tuple(Of Integer, String)(1, "Efectivo"))
                _FillingTypePayments.Add(New Tuple(Of Integer, String)(2, "Cheque"))
                _FillingTypePayments.Add(New Tuple(Of Integer, String)(3, "Tarjeta"))
                _FillingTypePayments.Add(New Tuple(Of Integer, String)(4, "Consignación"))
                _FillingTypePayments.Add(New Tuple(Of Integer, String)(5, "Todos"))
            End If
            Return _FillingTypePayments
        End Get
    End Property

    Private _FillingType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingType Is Nothing Then
                _FillingType = New List(Of Tuple(Of Integer, String))
                _FillingType.Add(New Tuple(Of Integer, String)(1, "Reducido"))
                _FillingType.Add(New Tuple(Of Integer, String)(2, "Detallado"))
            End If
            Return _FillingType
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

        'validaciones controles de caja
        If INDSleCashRegisterStart.TextEditValue <> String.Empty And INDSleCashRegisterEnd.TextEditValue = String.Empty Or INDSleCashRegisterStart.TextEditValue = String.Empty And INDSleCashRegisterEnd.TextEditValue <> String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCashRegisters.Text)
            Me.INDSleCashRegisterStart.Focus()
            Validations = False
        ElseIf INDSleCashRegisterStart.TextEditValue > INDSleCashRegisterEnd.TextEditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCashRegisters.Text)
            Me.INDSleCashRegisterStart.Focus()
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

        Dim filtroConsulta As String = "IdCashReceipt.DocumentDate >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd HH:mm:ss") & "# AND IdCashReceipt.DocumentDate <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd HH:mm:ss") & "#"

        'Se filtra por Usuarios
        If INDSleCreationUsersStart.EditValue IsNot Nothing And INDSleCreationUsersEnd.EditValue IsNot Nothing Then
            filtroConsulta &= " AND IdCashReceipt.CreationUser >= '" & INDSleCreationUsersStart.EditValue & "' AND IdCashReceipt.CreationUser <= '" & INDSleCreationUsersEnd.EditValue & "'"
        End If

        If INDCcbStatus.EditValue IsNot Nothing Then
            filtroConsulta &= "AND IdCashReceipt.Status in (" & INDCcbStatus.EditValue.ToString & ")"
        End If

        If INDGlePaymentsReport.EditValue <> 5 Then
            filtroConsulta &= "AND PaymentMethodTypes = " & INDGlePaymentsReport.EditValue
        End If

        If INDSleCashRegisterStart.TextEditValue <> String.Empty And INDSleCashRegisterEnd.TextEditValue <> String.Empty Then
            filtroConsulta &= " AND IdCashReceipt.IdCashRegister.Code >= '" & INDSleCashRegisterStart.TextEditValue & "' AND IdCashReceipt.IdCashRegister.Code <= '" & INDSleCashRegisterEnd.TextEditValue & "'"
        End If

        Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryPaymentMethodsXpo)(Nothing, filtroConsulta)

        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha")
        dt.Columns.Add("Código Usuario")
        dt.Columns.Add("Nombre Usuario")

        Dim columValueEfecty As DataColumn = New DataColumn
        columValueEfecty.DataType = System.Type.GetType("System.Decimal")
        columValueEfecty.AllowDBNull = False
        columValueEfecty.Caption = "Valor Efectivo"
        columValueEfecty.ColumnName = "Valor Efectivo"
        dt.Columns.Add(columValueEfecty)

        Dim columValueCheck As DataColumn = New DataColumn
        columValueCheck.DataType = System.Type.GetType("System.Decimal")
        columValueCheck.AllowDBNull = False
        columValueCheck.Caption = "Valor Cheque"
        columValueCheck.ColumnName = "Valor Cheque"
        dt.Columns.Add(columValueCheck)

        Dim columValueCard As DataColumn = New DataColumn
        columValueCard.DataType = System.Type.GetType("System.Decimal")
        columValueCard.AllowDBNull = False
        columValueCard.Caption = "Valor Tarjeta"
        columValueCard.ColumnName = "Valor Tarjeta"
        dt.Columns.Add(columValueCard)

        Dim columValueConsigment As DataColumn = New DataColumn
        columValueConsigment.DataType = System.Type.GetType("System.Decimal")
        columValueConsigment.AllowDBNull = False
        columValueConsigment.Caption = "Valor Consignación"
        columValueConsigment.ColumnName = "Valor Consignación"
        dt.Columns.Add(columValueConsigment)

        dt.Columns.Add("Estado")

        Dim columValueTotal As DataColumn = New DataColumn
        columValueTotal.DataType = System.Type.GetType("System.Decimal")
        columValueTotal.AllowDBNull = False
        columValueTotal.Caption = "Total"
        columValueTotal.ColumnName = "Total"
        dt.Columns.Add(columValueTotal)

        Dim dictionaryCashReceipt As Dictionary(Of String, String) = New Dictionary(Of String, String)
        For Each itemView In IndList
            Dim nameUser As String = String.Empty
            If dictionaryCashReceipt.ContainsKey(itemView.IdCashReceipt.CreationUser) Then
                nameUser = dictionaryCashReceipt(itemView.IdCashReceipt.CreationUser)
            Else
                Dim INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).TreasuryService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & itemView.IdCashReceipt.CreationUser & "'")
                nameUser = INDUser(0).PersonFullName
                dictionaryCashReceipt.Add(itemView.IdCashReceipt.CreationUser, INDUser(0).PersonFullName)
            End If

            Dim rowSelect() As System.Data.DataRow
            rowSelect = dt.Select("[Código] = '" & itemView.IdCashReceipt.Code & "'")

            Dim valueEfecty As Decimal = IIf(itemView.PaymentMethodTypes = 1, itemView.Value, 0)
            Dim valueCheck As Decimal = IIf(itemView.PaymentMethodTypes = 2, itemView.Value, 0)
            Dim valueCard As Decimal = IIf(itemView.PaymentMethodTypes = 3, itemView.Value, 0)
            Dim valueConsigment As Decimal = IIf(itemView.PaymentMethodTypes = 4, itemView.Value, 0)

            Dim row As DataRow
            If rowSelect.Count > 0 Then
                row = rowSelect(0)
                row.Item("Valor Efectivo") += valueEfecty
                row.Item("Valor Cheque") += valueCheck
                row.Item("Valor Tarjeta") += valueCard
                row.Item("Valor Consignación") += valueConsigment
                row.Item("Total") += itemView.Value
            Else
                row = dt.NewRow()
                row.Item("Código") = itemView.IdCashReceipt.Code
                row.Item("Fecha") = itemView.IdCashReceipt.DocumentDate
                row.Item("Código Usuario") = itemView.IdCashReceipt.CreationUser
                row.Item("Nombre Usuario") = nameUser
                row.Item("Valor Efectivo") = valueEfecty
                row.Item("Valor Cheque") = valueCheck
                row.Item("Valor Tarjeta") = valueCard
                row.Item("Valor Consignación") = valueConsigment
                row.Item("Estado") = itemView.IdCashReceipt.Status
                row.Item("Total") = itemView.Value

                dt.Rows.Add(row)
            End If
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
        ProoftCloseXpoCash = Nothing
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
            If (Me.INDGleTypeReport.EditValue = 1) Then
                Dim reporte As New rptListSummaryCashReceiptsReduced

                reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue, INDCcbStatus.EditValue, INDGlePaymentsReport.EditValue,
                                            INDSleCreationUsersStart.EditValue, INDSleCreationUsersEnd.EditValue, INDSleUserGrouping.EditValue,
                                            INDSleInvoicePrint.EditValue, INDGlcBreakPage.EditValue, INDSleCashRegisterStart.TextEditValue, INDSleCashRegisterEnd.TextEditValue}

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
            Else
                Dim reporte As New rptListSummaryCashReceiptsDetail

                reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue, INDCcbStatus.EditValue, INDGlePaymentsReport.EditValue,
                            INDSleCreationUsersStart.EditValue, INDSleCreationUsersEnd.EditValue, INDSleUserGrouping.EditValue,
                            INDSleInvoicePrint.EditValue, INDGlcBreakPage.EditValue, INDSleCashRegisterStart.TextEditValue, INDSleCashRegisterEnd.TextEditValue}

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
    Private Sub FrmReportListSummaryCashReceipts_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar el GridLookUpEdit
        Me.INDGlePaymentsReport.Properties.DataSource = FillingTypePayments
        Me.INDGleTypeReport.Properties.DataSource = FillingType

        'Asigna un valor por defecto a GridLookUpEdit
        Me.INDGlePaymentsReport.EditValue = 5
        Me.INDGleTypeReport.EditValue = 1
        Me.INDSleUserGrouping.EditValue = True
        Me.INDSleInvoicePrint.EditValue = True
        Me.INDGlcBreakPage.EditValue = True

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

    Private Sub FrmReportListSummaryCashReceipts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleCreationUsersStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.ConsultarUsuarioCodigo
        Me.INDSleCreationUsersEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.ConsultarUsuarioCodigo
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 1 Then
            INDLciInvoicePrintReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleInvoicePrint.EditValue = False
        Else
            INDLciInvoicePrintReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
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
End Class