#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Reporter

#End Region

Public Class FrmReportbudgetExecutionByCategoryThird

#Region "Variables"

    Private criterias As Dictionary(Of String, String)

#End Region

#Region "Datasource"
    ''' <summary>
    ''' Propiedad para el  BudgetaryEntityXpo
    ''' </summary>
    ''' <returns></returns>
    Private Property BudgetaryEntityXpo As XPInstantFeedbackSource
        Get
            Return INDsleEntity.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntity.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad usada para el xpo de vigencia
    ''' </summary>
    ''' <returns></returns>
    Private Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    Private _FillingCodeToUse As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' Propiedad la cual llenara la regilla de el codigo a usar con valores quemados 
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingCodeToUse As List(Of Tuple(Of Integer, String))
        Get
            If _FillingCodeToUse Is Nothing Then
                _FillingCodeToUse = New List(Of Tuple(Of Integer, String))
                _FillingCodeToUse.Add(New Tuple(Of Integer, String)(1, "Código Rubro"))
                _FillingCodeToUse.Add(New Tuple(Of Integer, String)(2, "Código Alterno"))
            End If
            Return _FillingCodeToUse
        End Get
    End Property
    ''' <summary>
    ''' Propiedad  que tarera tuplas quemadas para el nivel 
    ''' </summary>
    Private _FillingLevel As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingLevel As List(Of Tuple(Of Integer, String))
        Get
            If _FillingLevel Is Nothing Then
                _FillingLevel = New List(Of Tuple(Of Integer, String))
                _FillingLevel.Add(New Tuple(Of Integer, String)(1, "Disponibilidades"))
                _FillingLevel.Add(New Tuple(Of Integer, String)(2, "Disponibilidades Modificaciones"))
                _FillingLevel.Add(New Tuple(Of Integer, String)(3, "Compromisos"))
                _FillingLevel.Add(New Tuple(Of Integer, String)(4, "Compromisos Modificaciones"))
                _FillingLevel.Add(New Tuple(Of Integer, String)(5, "Obligaciones"))
                _FillingLevel.Add(New Tuple(Of Integer, String)(6, "Obligaciones Modificaciones"))
                _FillingLevel.Add(New Tuple(Of Integer, String)(7, "Ordenes de Pago"))
                _FillingLevel.Add(New Tuple(Of Integer, String)(8, "Reintegros"))
            End If
            Return _FillingLevel
        End Get
    End Property
    ''' <summary>
    ''' Propiedad para generar el xpo de presupuesto en el campo de budget 
    ''' </summary>
    ''' <returns></returns>
    Private Property BudgetXpo As XPInstantFeedbackSource
        Get
            Return INDSleBudget.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleBudget.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para hacer la bsuqueda de terceros 
    ''' </summary>
    ''' <returns></returns>
    Private Property ThirdPartyXpo As XPInstantFeedbackSource
        Get
            Return INDSleThirdParty.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleThirdParty.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "BarraBotones"
    ''' <summary>
    ''' Carga la barra de botones 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Methods"

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
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                INDsleValidity.EditValue = item.Id
                CleanControlsSelectValidity()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Limpia o habilita los controles de los elementos dentro del Frm
    ''' </summary>
    Sub CleanControlsSelectValidity()
        INDsleValidity.Enabled = True
        INDDeCutoffDate.Enabled = True
        INDGleCodeToUse.Enabled = True
        INDGleLevel.Enabled = True
        INDSleBudget.Enabled = True
        INDSleThirdParty.Enabled = True
        INDSbGenerateReport.Enabled = True
        INDSbGenerateExcell.Enabled = True
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        If INDsleValidity.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDlciValidity.Text))
            Me.INDsleValidity.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("BudgetaryEntityId", INDsleEntity.EditValue)
        criterias.Add("BudgetaryValidityId", INDsleValidity.EditValue)
        criterias.Add("CutoffDate", INDDeCutoffDate.EditValue)
        criterias.Add("CodeToUse", INDGleCodeToUse.EditValue)
        criterias.Add("Level", INDGleLevel.EditValue)
        criterias.Add("Budgets", _selectorBudget.GetKeys())
        criterias.Add("ThirdParties", _selectorThirdParty.GetKeys())

        Return True
    End Function

#Region "Selector"

    Private _selectorBudget As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorThirdParty As SelectorCache = New SelectorCache("Id", "Nit")
    ''' <summary>
    ''' Controlador para personalizar celdas en terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvBudget.CustomUnboundColumnData, INDGvThirdParty.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvBudget" Then
                e.Value = _selectorBudget.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvThirdParty" Then
                e.Value = _selectorThirdParty.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento cuando se selecciona la celda de terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvBudget.RowCellClick, INDGvThirdParty.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvBudget" Then
                selector = _selectorBudget
            ElseIf view.Name = "INDGvThirdParty" Then
                selector = _selectorThirdParty
            End If

            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)
            Else
                selector.Clear()
            End If
            view.RefreshData()
        End If
    End Sub
    ''' <summary>
    ''' Evento al cerrar la celda de terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSle_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleBudget.Closed, INDSleThirdParty.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleBudget" Then
            searchLookupEdit.Properties.NullText = _selectorBudget.ToString()
        ElseIf searchLookupEdit.Name = "INDSleThirdParty" Then
            searchLookupEdit.Properties.NullText = _selectorThirdParty.ToString()
        End If
    End Sub

#End Region

#Region "ToExcel"
    ''' <summary>
    ''' metodo que genrara la tabla excel 
    ''' </summary>
    ''' <param name="dtReportBudgetExecutionByCategoryThird"></param>
    Private Sub chargueDataSource(ByVal dtReportBudgetExecutionByCategoryThird As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Rubro")
        dt.Columns.Add("Recurso")
        dt.Columns.Add("Tipo")
        dt.Columns.Add("Tipo Movimiento")
        dt.Columns.Add("Disponibilidad")
        dt.Columns.Add("Disponibilidad Fecha", GetType(Date))
        dt.Columns.Add("Disponibilidad Modificación")
        dt.Columns.Add("Disponibilidad Modificación Fecha", GetType(Date))
        dt.Columns.Add("Tercero")
        dt.Columns.Add("Compromiso")
        dt.Columns.Add("Compromiso Fecha", GetType(Date))
        dt.Columns.Add("Compromiso Modificación")
        dt.Columns.Add("Compromiso Modificación Fecha", GetType(Date))
        dt.Columns.Add("Obligación")
        dt.Columns.Add("Obligación Fecha", GetType(Date))
        dt.Columns.Add("Obligación Modificación")
        dt.Columns.Add("Obligación Modificación Fecha", GetType(Date))
        dt.Columns.Add("Orden de Pago")
        dt.Columns.Add("Orden de Pago Fecha", GetType(Date))
        dt.Columns.Add("Reintegro")
        dt.Columns.Add("Reintegro Fecha", GetType(Date))
        dt.Columns.Add("Disponibilidad Inicial", GetType(Decimal))
        dt.Columns.Add("Disponibilidad Débito", GetType(Decimal))
        dt.Columns.Add("Disponibilidad Crédito", GetType(Decimal))
        dt.Columns.Add("Compromiso Inicial", GetType(Decimal))
        dt.Columns.Add("Compromiso Débito", GetType(Decimal))
        dt.Columns.Add("Compromiso Crédito", GetType(Decimal))
        dt.Columns.Add("Obligación Inicial", GetType(Decimal))
        dt.Columns.Add("Obligación Débito", GetType(Decimal))
        dt.Columns.Add("Obligación Crédito", GetType(Decimal))
        dt.Columns.Add("Valor Pagado", GetType(Decimal))
        dt.Columns.Add("Valor Reintegro", GetType(Decimal))

        For Each item In dtReportBudgetExecutionByCategoryThird.Rows
            Dim row As DataRow = dt.NewRow
            row.Item("Rubro") = item("CategoryCodeName")
            row.Item("Recurso") = item("FinancialSourceCodeName")
            row.Item("Tipo") = item("RevenueTypeCode")
            row.Item("Tipo Movimiento") = item("MovementType")
            row.Item("Disponibilidad") = item("AvailabilityCode")
            row.Item("Disponibilidad Fecha") = If(IsDBNull(item("AvailabilityDate")), DBNull.Value, CDate(item("AvailabilityDate")).AsDate)
            row.Item("Disponibilidad Modificación") = item("AvailabilityModificationCode")
            row.Item("Disponibilidad Modificación Fecha") = If(IsDBNull(item("AvailabilityModificationDate")), DBNull.Value, CDate(item("AvailabilityModificationDate")).AsDate)
            row.Item("Tercero") = item("ThirdPartyNitName")
            row.Item("Compromiso") = item("CommitmentCode")
            row.Item("Compromiso Fecha") = If(IsDBNull(item("CommitmentDate")), DBNull.Value, CDate(item("CommitmentDate")).AsDate)
            row.Item("Compromiso Modificación") = item("CommitmentModificationCode")
            row.Item("Compromiso Modificación Fecha") = If(IsDBNull(item("CommitmentModificationDate")), DBNull.Value, CDate(item("CommitmentModificationDate")).AsDate)
            row.Item("Obligación") = item("ObligationCode")
            row.Item("Obligación Fecha") = If(IsDBNull(item("ObligationDate")), DBNull.Value, CDate(item("ObligationDate")).AsDate)
            row.Item("Obligación Modificación") = item("ObligationModificationCode")
            row.Item("Obligación Modificación Fecha") = If(IsDBNull(item("ObligationModificationDate")), DBNull.Value, CDate(item("ObligationModificationDate")).AsDate)
            row.Item("Orden de Pago") = item("PaymentOrderCode")
            row.Item("Orden de Pago Fecha") = If(IsDBNull(item("PaymentOrderDate")), DBNull.Value, CDate(item("PaymentOrderDate")).AsDate)
            row.Item("Reintegro") = item("ReimbursementResourceCode")
            row.Item("Reintegro Fecha") = If(IsDBNull(item("ReimbursementResourceDate")), DBNull.Value, CDate(item("ReimbursementResourceDate")).AsDate)
            row.Item("Disponibilidad Inicial") = item("AvailabilityValue")
            row.Item("Disponibilidad Débito") = item("AvailabilityDebitValue")
            row.Item("Disponibilidad Crédito") = item("AvailabilityCreditValue")
            row.Item("Compromiso Inicial") = item("CommitmentValue")
            row.Item("Compromiso Débito") = item("CommitmentDebitValue")
            row.Item("Compromiso Crédito") = item("CommitmentCreditValue")
            row.Item("Obligación Inicial") = item("ObligationValue")
            row.Item("Obligación Débito") = item("ObligationDebitValue")
            row.Item("Obligación Crédito") = item("ObligationCreditValue")
            row.Item("Valor Pagado") = item("PaymentOrderValue")
            row.Item("Valor Reintegro") = item("PaymentOrderDebitValue")
            dt.Rows.Add(row)
        Next
        INDGcExportExcell.DataSource = dt
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        _gridView.MainView.PopulateColumns()
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

#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' Metodo para cargar el FrmTrialBalance
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReportTrialBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit        
        INDGleCodeToUse.Properties.DataSource = FillingCodeToUse
        INDGleLevel.Properties.DataSource = FillingLevel

        'Dar un valores por defecto
        INDGleCodeToUse.EditValue = 1
        INDGleLevel.EditValue = 8

        'Cargar
        INDDeCutoffDate.EditValue = Me.GetDateServer()
    End Sub
    ''' <summary>
    ''' Controlador de eventos para liberar memeoria cuando el frm se cierra 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingCodeToUse = Nothing
    End Sub

#End Region

#Region "QueryPopup"
    ''' <summary>
    ''' Pop Pup que se genera con la informacion de entidad por medio del xpo 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntity.QueryPopUp
        If BudgetaryEntityXpo Is Nothing Then
            BudgetaryEntityXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
        End If
    End Sub
    ''' <summary>
    ''' Pop Pup generado En presupuestos con el xpo 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleBudget_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleBudget.QueryPopUp
        If BudgetXpo Is Nothing Then
            BudgetXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListBudgetByBudgetValidityIdAndTypeAndStatus(INDsleValidity.EditValue, 2, 2, False)
        End If
    End Sub
    ''' <summary>
    ''' Pop Pup Generado con la bsuqueda de los terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If ThirdPartyXpo Is Nothing Then
            ThirdPartyXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.ListAllThirdParty()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' evento para cargar resolucion , valor y estado.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntity.EditValueChanged
        If INDsleEntity.EditValue IsNot Nothing Then
            BudgetaryValidityXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(INDsleEntity.EditValue)
            SetFirstOrDefaultValidity()
        End If
    End Sub

    ''' <summary>
    ''' se dispara al cambiar el valor de la vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If INDsleValidity.EditValue IsNot Nothing Then
            CleanControlsSelectValidity()
            Dim item = (From l In BudgetaryValidityXpo Where l.Id = INDsleValidity.EditValue Select l).FirstOrDefault
            If item IsNot Nothing Then
                INDDeCutoffDate.EditValue = New Date(item.Year, item.ExpenseMonth, 1).AddMonths(1).AddDays(-1)
            End If
        End If
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta en el evento click del boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptReportReportbudgetExecutionByCategoryThird()
            reporte.ParametrosReporte = New Object() {criterias}
            INDDvViewReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDeCutoffDate.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack en el control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
    End Sub
    ''' <summary>
    ''' Evento cuando se le da click al boton de Excell
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New Presentation.Budget.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportBudgetExecutionByCategoryThird(criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportBudgetExecutionByCategoryThird As DataTable = ds.Tables("ReportBudgetExecutionByCategoryThird")

                        Await Task.Factory.StartNew(Sub()
                                                        chargueDataSource(dtReportBudgetExecutionByCategoryThird)
                                                    End Sub)

                        If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                            generateExcel()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    End If
                End Using
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
            End Try
        End If
    End Sub

#End Region

#End Region

End Class