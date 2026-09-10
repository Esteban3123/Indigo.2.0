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

Public Class FrmReportExpenseRecordBook

#Region "Variables"

    Private criterias As Dictionary(Of String, String)

#End Region

#Region "Datasource"
    ''' <summary>
    ''' Propiedad para el xpo de entidad
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
    ''' Propiedad para la el campo validacion que se usara en el xpo
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
    ''' Propiedad donde se llenara el pop puo del codigo a usar con tuplas
    ''' </summary>
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
    ''' Prpiedad usada para el campo de xpo del presupuesto
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

#End Region

#Region "BarraBotones"
    ''' <summary>
    ''' Carga la barra de permisos
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
    ''' Limpia los controles(Habilita los campos)
    ''' </summary>
    Sub CleanControlsSelectValidity()
        INDsleValidity.Enabled = True
        INDDeCutoffDate.Enabled = True
        INDGleCodeToUse.Enabled = True
        INDSleBudget.Enabled = True
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

        If INDSleBudget.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDLciBudget.Text))
            Me.INDSleBudget.Focus()
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
        criterias.Add("BudgetId", INDSleBudget.EditValue)

        Return True
    End Function

#Region "ToExcel"
    ''' <summary>
    ''' Metodo para crear la tabla de excel
    ''' </summary>
    ''' <param name="dtReportExpenseRecordBook"></param>
    Private Sub chargueDataSourceMonthlyExpenseExecution(ByVal dtReportExpenseRecordBook As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Mes")
        dt.Columns.Add("Día")
        dt.Columns.Add("Número")
        dt.Columns.Add("Clase")
        dt.Columns.Add("Signo")
        dt.Columns.Add("Valor", GetType(Decimal))
        dt.Columns.Add("Tipo Documento Soporte")
        dt.Columns.Add("Número Documento Soporte")
        dt.Columns.Add("Tercero")
        dt.Columns.Add("Apropiación Vigente", GetType(Decimal))
        dt.Columns.Add("Total CDP's", GetType(Decimal))
        dt.Columns.Add("Apropiación Vigente no Afectada", GetType(Decimal))
        dt.Columns.Add("Total Compromisos", GetType(Decimal))
        dt.Columns.Add("CDP por Comprometer", GetType(Decimal))
        dt.Columns.Add("Total Obligaciones", GetType(Decimal))
        dt.Columns.Add("Compromisos por Cumplir", GetType(Decimal))
        dt.Columns.Add("Total Pagos", GetType(Decimal))
        dt.Columns.Add("Obligaciones por Pagar", GetType(Decimal))
        dt.Columns.Add("Descripción")

        For Each item In dtReportExpenseRecordBook.Rows
            Dim row As DataRow = dt.NewRow
            row.Item("Mes") = CDate(item("DocumentDate")).Month
            row.Item("Día") = CDate(item("DocumentDate")).Day
            row.Item("Número") = item("DocumenNumber")
            row.Item("Clase") = item("DocumentClass")
            row.Item("Signo") = If(item("DocumentNature") = 1, "-", "+")
            row.Item("Valor") = item("DocumentValue")
            row.Item("Tipo Documento Soporte") = item("DocumentTypeName")
            row.Item("Número Documento Soporte") = item("DocumentCode")
            row.Item("Tercero") = item("DocumentThirdParty")
            row.Item("Apropiación Vigente") = item("BudgetTotal")
            row.Item("Total CDP's") = item("AvailabilityTotal")
            row.Item("Apropiación Vigente no Afectada") = item("BudgetPending")
            row.Item("Total Compromisos") = item("CommitmentTotal")
            row.Item("CDP por Comprometer") = item("AvailabilityPending")
            row.Item("Total Obligaciones") = item("ObligationTotal")
            row.Item("Compromisos por Cumplir") = item("CommitmentPending")
            row.Item("Total Pagos") = item("PaymentOrderTotal")
            row.Item("Obligaciones por Pagar") = item("ObligationPending")
            row.Item("Descripción") = item("DocumentDescription")
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
    ''' evento que carga el Frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmReportTrialBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar GridLookUpEdit        
        INDGleCodeToUse.Properties.DataSource = FillingCodeToUse

        'Dar un valores por defecto
        INDGleCodeToUse.EditValue = 1

        'Cargar
        INDDeCutoffDate.EditValue = Me.GetDateServer()
    End Sub
    ''' <summary>
    ''' Evento que libera memoria al cerrar el Frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingCodeToUse = Nothing
    End Sub

#End Region

#Region "QueryPopup"
    ''' <summary>
    ''' pop pup con la consulta de las entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntity.QueryPopUp
        If BudgetaryEntityXpo Is Nothing Then
            BudgetaryEntityXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
        End If
    End Sub
    ''' <summary>
    ''' Por pup con la consulta de los presupuestos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleBudget_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleBudget.QueryPopUp
        If BudgetXpo Is Nothing Then
            BudgetXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListBudgetByBudgetValidityIdAndTypeAndStatus(INDsleValidity.EditValue, 2, 2, False)
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
            Dim reporte As New rptReportExpenseRecordBook()
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
    ''' Se ejecuta cuando se le da click al boton de excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New Presentation.Budget.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetListReportExpenseRecordBook(criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportExpenseRecordBook As DataTable = ds.Tables("ReportExpenseRecordBook")

                        Await Task.Factory.StartNew(Sub()
                                                        chargueDataSourceMonthlyExpenseExecution(dtReportExpenseRecordBook)
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