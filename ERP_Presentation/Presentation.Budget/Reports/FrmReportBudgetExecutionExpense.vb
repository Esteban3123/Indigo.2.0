#Region "Imports"

Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportBudgetExecutionExpense

#Region "Variables"

    Private _pucModel As MCommon

    Private _yearValidity As Integer

    Private _criterias As Dictionary(Of String, String)

#End Region

#Region "Datasources"
    ''' <summary>
    ''' Prpiedad usada para usarse ene el xpo de vigencia 
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
    ''' <summary>
    ''' Propiedad que trea valores quemados para el campo de codigo a usar
    ''' </summary>
    Private _FillingCodeToUse As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingCodeToUse As List(Of Tuple(Of Integer, String))
        Get
            If _FillingCodeToUse Is Nothing Then
                _FillingCodeToUse = New List(Of Tuple(Of Integer, String))
                _FillingCodeToUse.Add(New Tuple(Of Integer, String)(1, "Código Rubro"))
                _FillingCodeToUse.Add(New Tuple(Of Integer, String)(2, "Código Alterno"))
                _FillingCodeToUse.Add(New Tuple(Of Integer, String)(3, "Código CCPET"))
            End If
            Return _FillingCodeToUse
        End Get
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
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
    ''' Propiedad que selecciona el primer registro del xpo de vigencia por defecto
    ''' </summary>
    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                INDsleValidity.EditValue = item.Id
                _yearValidity = item.Year
                INDDnDate.SetYear = _yearValidity
                CleanControlsSelectValidity()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Limpiamos los controles 
    ''' </summary>
    Sub CleanControlsSelectValidity()
        INDDnDate.Enabled = True
        INDGleCodeToUse.Enabled = True
        INDSleFinancialSourceStart.Enabled = True
        INDSleFinancialSourceEnd.Enabled = True
        INDSleCategoryStart.Enabled = True
        INDSleCategoryEnd.Enabled = True
        INDSbGenerateReport.Enabled = True
        INDSbGenerateExcell.Enabled = True

        INDSleFinancialSourceStart.EditValue = Nothing
        INDSleFinancialSourceEnd.EditValue = Nothing
        INDSleCategoryStart.EditValue = Nothing
        INDSleCategoryEnd.EditValue = Nothing
    End Sub
    ''' <summary>
    ''' Validamos los controles del reporte y si esta ok se pasa los´parametros de busqueda la criteria para el reporte 
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        If INDsleValidity.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDlciValidity.Text))
            Me.INDsleValidity.Focus()
        End If

        If INDSleFinancialSourceStart.EditValue IsNot Nothing And INDSleFinancialSourceEnd.EditValue Is Nothing Or INDSleFinancialSourceStart.EditValue Is Nothing And INDSleFinancialSourceEnd.EditValue IsNot Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblFinancialSource.Text))
            Me.INDSleFinancialSourceStart.Focus()
        ElseIf INDSleFinancialSourceStart.EditValue > INDSleFinancialSourceEnd.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblFinancialSource.Text))
            Me.INDSleFinancialSourceStart.Focus()
        End If

        If INDSleCategoryStart.EditValue IsNot Nothing And INDSleCategoryEnd.EditValue Is Nothing Or INDSleCategoryStart.EditValue Is Nothing And INDSleCategoryEnd.EditValue IsNot Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCategory.Text))
            Me.INDSleCategoryStart.Focus()
        ElseIf INDSleCategoryStart.EditValue > INDSleCategoryEnd.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCategory.Text))
            Me.INDSleCategoryStart.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        _criterias = New Dictionary(Of String, String)
        _criterias.Add("ValidityId", INDsleValidity.EditValue)
        _criterias.Add("Year", INDDnDate.GetYear())
        _criterias.Add("Month", INDDnDate.GetMonth())
        _criterias.Add("CodeToUse", INDGleCodeToUse.EditValue)
        _criterias.Add("FinancialSourceStart", INDSleFinancialSourceStart.EditValue)
        _criterias.Add("FinancialSourceEnd", INDSleFinancialSourceEnd.EditValue)
        _criterias.Add("CategoryStart", INDSleCategoryStart.EditValue)
        _criterias.Add("CategoryEnd", INDSleCategoryEnd.EditValue)

        Return True
    End Function

#Region "ToExcel"
    ''' <summary>
    ''' Cargamos la data para generar el reporte de excel 
    ''' </summary>
    ''' <param name="dtReportBudgetExecutionExpense"></param>
    Private Sub chargueDataSource(ByVal dtReportBudgetExecutionExpense As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Codigo")
        If INDGleCodeToUse.EditValue <> 3 Then
            dt.Columns.Add("Tipo")
        End If
        dt.Columns.Add("Denominación")
        dt.Columns.Add("Presupuesto Inicial", GetType(Decimal))
        dt.Columns.Add("Presupuesto Traslado Crédito", GetType(Decimal))
        dt.Columns.Add("Presupuesto Traslado Débito", GetType(Decimal))
        dt.Columns.Add("Presupuesto Modificación Crédito", GetType(Decimal))
        dt.Columns.Add("Presupuesto Modificación Débito", GetType(Decimal))
        dt.Columns.Add("Presupuesto Definitivo", GetType(Decimal))
        dt.Columns.Add("CDP'S", GetType(Decimal))
        dt.Columns.Add("Compromisos Meses Anteriores", GetType(Decimal))
        dt.Columns.Add("Compromisos Mes", GetType(Decimal))
        dt.Columns.Add("Compromisos Total", GetType(Decimal))
        dt.Columns.Add("Porcentaje Ejecutado", GetType(Decimal))
        dt.Columns.Add("OBL'S", GetType(Decimal))
        dt.Columns.Add("Pagos Meses Anteriores", GetType(Decimal))
        dt.Columns.Add("Pagos Mes", GetType(Decimal))
        dt.Columns.Add("Pagos Total", GetType(Decimal))
        dt.Columns.Add("Saldo de Aprovación", GetType(Decimal))
        dt.Columns.Add("Cuentas Por Pagar", GetType(Decimal))
        dt.Columns.Add("Porcentaje Por Ejecutar", GetType(Decimal))

        For Each item In dtReportBudgetExecutionExpense.Rows
            Dim budgetTotal = item("BudgetInitial") + item("BudgetTransferCredit") - item("BudgetTransferDebit") + item("BudgetModificationCredit") - item("BudgetModificationDebit")
            Dim commitmentTotal = item("CommitmentBalanceMonthPrevious") + item("CommitmentBalanceMonth")
            Dim percentageExecuted = If(budgetTotal = 0, 1, commitmentTotal / budgetTotal) * 100

            Dim row As DataRow = dt.NewRow
            row.Item("Codigo") = If(INDGleCodeToUse.EditValue = 2, item("AlternativeCode"), item("CategoryCode"))
            If INDGleCodeToUse.EditValue <> 3 Then
                row.Item("Tipo") = item("revenueTypeCode")
            End If
            row.Item("Denominación") = item("CategoryName")
            row.Item("Presupuesto Inicial") = item("BudgetInitial")
            row.Item("Presupuesto Traslado Crédito") = item("BudgetTransferCredit")
            row.Item("Presupuesto Traslado Débito") = item("BudgetTransferDebit")
            row.Item("Presupuesto Modificación Crédito") = item("BudgetModificationCredit")
            row.Item("Presupuesto Modificación Débito") = item("BudgetModificationDebit")
            row.Item("Presupuesto Definitivo") = item("BudgetInitial") + item("BudgetTransferCredit") - item("BudgetTransferDebit") + item("BudgetModificationCredit") - item("BudgetModificationDebit")
            row.Item("CDP'S") = item("AvailabilityBalanceMonthPrevious") + item("AvailabilityBalanceMonth")
            row.Item("Compromisos Meses Anteriores") = item("CommitmentBalanceMonthPrevious")
            row.Item("Compromisos Mes") = item("CommitmentBalanceMonth")
            row.Item("Compromisos Total") = commitmentTotal
            row.Item("Porcentaje Ejecutado") = percentageExecuted
            row.Item("OBL'S") = item("ObligationBalanceMonthPrevious") + item("ObligationBalanceMonth")
            row.Item("Pagos Meses Anteriores") = item("PaymentBalanceMonthPrevious")
            row.Item("Pagos Mes") = item("PaymentBalanceMonth")
            row.Item("Pagos Total") = item("PaymentBalanceMonthPrevious") + item("PaymentBalanceMonth")
            row.Item("Saldo de Aprovación") = (budgetTotal) - (item("CommitmentBalanceMonthPrevious") + item("CommitmentBalanceMonth"))
            row.Item("Cuentas Por Pagar") = (item("CommitmentBalanceMonthPrevious") + item("CommitmentBalanceMonth")) - (item("PaymentBalanceMonthPrevious") + item("PaymentBalanceMonth"))
            row.Item("Porcentaje Por Ejecutar") = 100 - percentageExecuted
            dt.Rows.Add(row)
        Next
        INDGcExportExcell.DataSource = dt
    End Sub
    ''' <summary>
    ''' Metodo que genera el excel 
    ''' </summary>
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
    ''' Metodo para caragar el formulario 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Sub FrmReportBudgetExecutionExpense_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleCategoryStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCategoryBudgetByCode
        Me.INDSleCategoryEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCategoryBudgetByCode
        Me.INDSleFinancialSourceStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFinancialSourceByCode
        Me.INDSleFinancialSourceEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFinancialSourceByCode

        'Ocultar el panel de grupo
        INDSleFinancialSourceStart.View.OptionsView.ShowGroupPanel = False
        INDSleFinancialSourceEnd.View.OptionsView.ShowGroupPanel = False
        INDSleCategoryStart.View.OptionsView.ShowGroupPanel = False
        INDSleCategoryEnd.View.OptionsView.ShowGroupPanel = False

        'Cargar GridLookUpEdit        
        INDGleCodeToUse.Properties.DataSource = FillingCodeToUse

        'Dar un valores por defecto
        INDGleCodeToUse.EditValue = 1

        'Establecer la fecha actual
        Dim dateNow = Me.GetDateServer()
        INDDnDate.SetMonth = Month(dateNow)
        INDDnDate.SetYear = Year(dateNow)
    End Sub
    ''' <summary>
    ''' Libramos la memoria al cerrar el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()

        _model = Nothing
        _yearValidity = Nothing
        _criterias = Nothing
        _FillingCodeToUse = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' carga el datasource de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntity.QueryPopUp
        If INDsleEntity.Properties.DataSource Is Nothing Then
            INDsleEntity.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCategoryStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCategoryStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCategoryStart.QueryPopUp
        If INDSleCategoryStart.Datasource Is Nothing Then
            Dim criteria = "ItemType = 2 And Status = True And FinancialSourceId is not null"

            If INDsleValidity.EditValue IsNot Nothing Then
                criteria &= " And BudgetaryValidityId.Id =" & INDsleValidity.EditValue
            End If

            If INDSleFinancialSourceStart.EditValue IsNot Nothing AndAlso INDSleFinancialSourceEnd.EditValue IsNot Nothing Then
                criteria &= " And FinancialSourceId.Code >= '" & INDSleFinancialSourceStart.EditValue & "' And FinancialSourceId.Code <= '" & INDSleFinancialSourceEnd.EditValue & "'"
            End If

            INDSleCategoryStart.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListCategoryByFilter(criteria)
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCategoryEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCategoryEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCategoryEnd.QueryPopUp
        If INDSleCategoryEnd.Datasource Is Nothing Then
            Dim criteria = "ItemType = 2 And Status = True And FinancialSourceId is not null"

            If INDsleValidity.EditValue IsNot Nothing Then
                criteria &= " And BudgetaryValidityId.Id =" & INDsleValidity.EditValue
            End If

            If INDSleFinancialSourceStart.EditValue IsNot Nothing AndAlso INDSleFinancialSourceEnd.EditValue IsNot Nothing Then
                criteria &= " And FinancialSourceId.Code >= '" & INDSleFinancialSourceStart.EditValue & "' And FinancialSourceId.Code <= '" & INDSleFinancialSourceEnd.EditValue & "'"
            End If

            INDSleCategoryEnd.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListCategoryByFilter(criteria)
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleFinancialSourceStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleFinancialSourceStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinancialSourceStart.QueryPopUp
        If INDSleFinancialSourceStart.Datasource Is Nothing Then
            INDSleFinancialSourceStart.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListFinancialSource(INDsleEntity.EditValue, INDsleValidity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleFinancialSourceEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleFinancialSourceEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFinancialSourceEnd.QueryPopUp
        If INDSleFinancialSourceEnd.Datasource Is Nothing Then
            INDSleFinancialSourceEnd.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListFinancialSource(INDsleEntity.EditValue, INDsleValidity.EditValue)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' se ejecuta en el evento OnChangeDate del control INDCdnDateStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDDnDate_OnChangeDate(sender As Object, e As EventArgs) Handles INDDnDate.OnChangeDate
        If INDDnDate.GetYear <> _yearValidity Then
            INDDnDate.SetYear = _yearValidity
        End If
    End Sub

    ''' <summary>
    ''' evento para cargar resolucion , valor y estado.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntity.EditValueChanged
        If INDsleEntity.EditValue IsNot Nothing Then
            INDsleValidity.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(INDsleEntity.EditValue)
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
            Me._pucModel.ValidityId = INDsleValidity.EditValue
            Dim item = (From l In BudgetaryValidityXpo Where l.Id = INDsleValidity.EditValue Select l).FirstOrDefault
            If item IsNot Nothing Then
                _yearValidity = item.Year
                INDDnDate.SetYear = _yearValidity
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
            Try
                AsyncLoader(True)

                Dim reporte As New rptReportBudgetExecutionExpense()
                reporte.ParametrosReporte = New Object() {_criterias}
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
                    Me.INDDnDate.Focus()
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                AsyncLoader(False)
            End Try
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
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New Presentation.Budget.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetListReportBudgetExecutionExpense(_criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportBudgetExecutionExpense As DataTable = ds.Tables("ReportBudgetExcutionExpense")

                        Await Task.Factory.StartNew(Sub()
                                                        chargueDataSource(dtReportBudgetExecutionExpense)
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