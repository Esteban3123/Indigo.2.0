#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportListDocumentExpense

#Region "Variables"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon
    Private _LoadTypeDocument As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' Prpiedad que carga el TypeDocument con tuplas
    ''' </summary>
    Private ReadOnly Property LoadTypeDocument As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeDocument Is Nothing Then
                _LoadTypeDocument = New List(Of Tuple(Of Integer, String))
                _LoadTypeDocument.Add(New Tuple(Of Integer, String)(1, "Disponibilidad"))
                _LoadTypeDocument.Add(New Tuple(Of Integer, String)(2, "Disponibilidad Modificación"))
                _LoadTypeDocument.Add(New Tuple(Of Integer, String)(3, "Compromiso"))
                _LoadTypeDocument.Add(New Tuple(Of Integer, String)(4, "Compromiso Modificación"))
                _LoadTypeDocument.Add(New Tuple(Of Integer, String)(5, "Obligación"))
                _LoadTypeDocument.Add(New Tuple(Of Integer, String)(6, "Obligación Modificación"))
                _LoadTypeDocument.Add(New Tuple(Of Integer, String)(7, "Orden De Pago"))
                _LoadTypeDocument.Add(New Tuple(Of Integer, String)(8, "Reintegro"))
            End If
            Return _LoadTypeDocument
        End Get
    End Property

    Private _LoadTypeReport As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' Prpiedad que carga el TypeReport con tuplas
    ''' </summary>
    Private ReadOnly Property LoadTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _LoadTypeReport Is Nothing Then
                _LoadTypeReport = New List(Of Tuple(Of Integer, String))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _LoadTypeReport.Add(New Tuple(Of Integer, String)(2, "Detallado"))
            End If
            Return _LoadTypeReport
        End Get
    End Property

    Dim _yearValidity As Integer
    ''' <summary>
    ''' Variable usada para la busqueda en el xpo
    ''' </summary>

    Private criterias As Dictionary(Of String, String)

#End Region

#Region "XPO"

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia al cual pertenece
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityId As Integer
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property

#End Region

#Region "BarraBotones"
    ''' <summary>
    ''' Carga los los permisos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' se inicializan los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportTrialBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)

        'Cargar GridLookUpEdit
        Me.INDGleTypeDocument.Properties.DataSource = LoadTypeDocument
        Me.INDGleTypeReport.Properties.DataSource = LoadTypeReport

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeDocument.EditValue = 1
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleGroupBy.EditValue = 0
        Me.INDGleGroupBy.Properties.NullText = "Sin Agrupar"
    End Sub

    ''' <summary>
    ''' Se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()
        _model = Nothing
        _yearValidity = Nothing

        _selector = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' pop up que obtiene el listado de grupos por medio de tuplas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleGroupBy_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGleGroupBy.QueryPopUp
        If INDGleGroupBy.Properties.DataSource Is Nothing Then
            Dim _list As New List(Of Tuple(Of Byte, String))
            _list.Add(New Tuple(Of Byte, String)(0, "Sin Agrupar"))
            _list.Add(New Tuple(Of Byte, String)(1, "Tercero"))
            _list.Add(New Tuple(Of Byte, String)(2, "Rubro"))
            _list.Add(New Tuple(Of Byte, String)(3, "Recurso"))
            _list.Add(New Tuple(Of Byte, String)(4, "Tipo"))
            INDGleGroupBy.Properties.DataSource = _list
        End If
    End Sub

    ''' <summary>
    ''' pop up que lista los terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleThird_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If INDSleThirdParty.Properties.DataSource Is Nothing Then
            INDSleThirdParty.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.ListThird
        End If
    End Sub

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
    ''' carga el datasource de recaudos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAvailability_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAvailability.QueryPopUp
        If INDSleAvailability.Datasource Is Nothing Then
            INDSleAvailability.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListAvailability(INDsleValidity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de Modificación de disponibilidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAvailabilityModification_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAvailabilityModification.QueryPopUp
        If INDSleAvailabilityModification.Datasource Is Nothing Then
            INDSleAvailabilityModification.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListAvailabilityModificationByValidityId(INDsleValidity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de reconocimientos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCommitment_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCommitment.QueryPopUp
        If INDSleCommitment.Datasource Is Nothing Then
            INDSleCommitment.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListCommitmentByValidityId(INDsleValidity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de Modificación de Cmpromiso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCommitmentModification_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCommitmentModification.QueryPopUp
        If INDSleCommitmentModification.Datasource Is Nothing Then
            INDSleCommitmentModification.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListCommitmentModificationByValidityId(INDsleValidity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de Obligación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleObligation_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleObligation.QueryPopUp
        If INDSleObligation.Datasource Is Nothing Then
            INDSleObligation.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListObligation(INDsleValidity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de Modificación de Obligación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleObligationModification_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleObligationModification.QueryPopUp
        If INDSleObligationModification.Datasource Is Nothing Then
            INDSleObligationModification.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListObligationModificationByValidityId(INDsleValidity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de Orden de Pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePaymentOrder_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePaymentOrder.QueryPopUp
        If INDSlePaymentOrder.Datasource Is Nothing Then
            INDSlePaymentOrder.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListPaymentOrderByValidityId(INDsleValidity.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de Reintegro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleReimbursementResource_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleReimbursementResource.QueryPopUp
        If INDSleReimbursementResource.Datasource Is Nothing Then
            INDSleReimbursementResource.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListReimbursementResourceByValidityId(INDsleValidity.EditValue)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento al cambiar el tipo de reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue IsNot Nothing And INDGleTypeReport.EditValue = 1 Then
            INDLciGroupBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciGroupBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleGroupBy.EditValue = Nothing
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
        End If
    End Sub

    ''' <summary>
    ''' al cambiar el tipo de documento se muestra el filtro del documento seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleTypeDocument_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeDocument.EditValueChanged
        INDLciAvailability.Visibility = If(INDGleTypeDocument.EditValue = 1, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciAvailabilityModification.Visibility = If(INDGleTypeDocument.EditValue = 2, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciCommitment.Visibility = If(INDGleTypeDocument.EditValue = 3, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciCommitmentModification.Visibility = If(INDGleTypeDocument.EditValue = 4, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciObligation.Visibility = If(INDGleTypeDocument.EditValue = 5, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciObligationModification.Visibility = If(INDGleTypeDocument.EditValue = 6, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciPaymentOrder.Visibility = If(INDGleTypeDocument.EditValue = 7, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciReimbursementResource.Visibility = If(INDGleTypeDocument.EditValue = 8, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        INDLciThirdParty.Visibility = If({1, 2}.Contains(INDGleTypeDocument.EditValue), DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)

        INDSleAvailability.EditValue = Nothing
        INDSleAvailabilityModification.EditValue = Nothing
        INDSleCommitment.EditValue = Nothing
        INDSleCommitmentModification.EditValue = Nothing
        INDSleObligation.EditValue = Nothing
        INDSleObligationModification.EditValue = Nothing
        INDSlePaymentOrder.EditValue = Nothing
        INDSleReimbursementResource.EditValue = Nothing
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

                Dim reporte As Object
                Select Case INDGleTypeReport.EditValue
                    Case 1
                        Select Case INDGleTypeDocument.EditValue
                            Case 1
                                reporte = New rptListAvailability
                            Case 2
                                reporte = New rptListAvailabilityModification
                            Case 3
                                reporte = New rptListCommitment
                            Case 4
                                reporte = New rptListCommitmentModification
                            Case 5
                                reporte = New rptListObligation
                            Case 6
                                reporte = New rptListObligationModification
                            Case 7
                                reporte = New rptListPaymentOrder
                            Case 8
                                reporte = New rptListReimbursementResource
                            Case Else
                                AsyncLoader(False)
                                Exit Sub
                        End Select
                    Case 2
                        Select Case INDGleTypeDocument.EditValue
                            Case 1
                                reporte = New rptSubAvailability
                            Case 2
                                reporte = New rptSubAvailabilityModification
                            Case 3
                                reporte = New rptSubCommitment
                            Case 4
                                reporte = New rptSubCommitmentModification
                            Case 5
                                reporte = New rptSubObligation
                            Case 6
                                reporte = New rptSubObligationModification
                            Case 7
                                reporte = New rptSubPaymentOrder
                            Case 8
                                reporte = New rptSubReimbursementResource
                            Case Else
                                AsyncLoader(False)
                                Exit Sub
                        End Select
                    Case Else
                        Exit Sub
                End Select

                reporte.ParametrosReporte = New Object() {criterias}
                INDDvReport.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                AsyncLoader(False)
                If reporte.DataSource IsNot Nothing Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncReport.Visible = False
                    Me.INDPcReport.Visible = True
                    INDDvReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDteDateStart.Focus()
                End If
            Catch ex As Exception
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
        Me.INDCncReport.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDDteDateStart.Focus()
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateExcel_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcel.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Using model As New Presentation.Budget.MVP.MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportListDocumentExpense(criterias)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportListDocumentExpense As DataTable = ds.Tables("ReportListDocumentExpense")

                        Await Task.Factory.StartNew(Sub()
                                                        Select Case INDGleTypeDocument.EditValue
                                                            Case 1
                                                                chargueDatasourceAvailability(dtReportListDocumentExpense)
                                                            Case 2
                                                                chargueDatasourceAvailabilityModification(dtReportListDocumentExpense)
                                                            Case 3
                                                                chargueDatasourceCommitment(dtReportListDocumentExpense)
                                                            Case 4
                                                                chargueDatasourceCommitmentModification(dtReportListDocumentExpense)
                                                            Case 5
                                                                chargueDatasourceObligation(dtReportListDocumentExpense)
                                                            Case 6
                                                                chargueDatasourceObligationModification(dtReportListDocumentExpense)
                                                            Case 7
                                                                chargueDatasourcePaymentOrder(dtReportListDocumentExpense)
                                                            Case 8
                                                                chargueDatasourceReimbursementResource(dtReportListDocumentExpense)
                                                            Case Else
                                                                Exit Sub
                                                        End Select
                                                    End Sub)

                        If Me.INDGcExportExcel.DataSource IsNot Nothing Then
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

#Region "Selector"

    Private _selector As SelectorCache = New SelectorCache("Id", "Nit")
    ''' <summary>
    ''' Evento cuando se customiza la columna de terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvThirdParty_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvThirdParty.CustomUnboundColumnData
        If e.IsGetData Then
            e.Value = _selector.ValidateExistsRow(e.Row)
        End If
    End Sub
    ''' <summary>
    ''' evento cuando se cambia un tercero en la celda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvThirdParty_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvThirdParty.RowCellClick
        If e.Column.FieldName = "UnboundSelection" Then
            If e.RowHandle >= 0 Then
                Dim row = INDGvThirdParty.GetRow(e.RowHandle)
                _selector.SetValue(row)
            Else
                _selector.Clear()
            End If
            INDGvThirdParty.RefreshData()
        End If
    End Sub
    ''' <summary>
    ''' Limpia la memoria cuando se cierra los terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleThirdPartyId_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleThirdParty.Closed
        INDSleThirdParty.EditValue = Nothing
        INDSleThirdParty.Properties.NullText = _selector.ToString()
    End Sub

#End Region

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
                BudgetaryValidityId = item.Id
                _yearValidity = item.Year
                CleanControlsSelectValidity()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Sub CleanControlsSelectValidity()
        INDDteDateStart.Enabled = True
        INDDteDateStart.EditValue = Nothing
        INDDteDateEnd.Enabled = True
        INDDteDateEnd.EditValue = Nothing
        INDGleTypeReport.Enabled = True
        INDGleTypeDocument.Enabled = True
        INDGleGroupBy.Enabled = True
        INDGleGroupBy.EditValue = 0
        INDGleGroupBy.Properties.NullText = "Sin Agrupar"

        INDSleAvailability.Enabled = True
        INDSleAvailability.EditValue = Nothing
        INDSleAvailabilityModification.EditValue = Nothing
        INDSleCommitment.EditValue = Nothing
        INDSleCommitment.Enabled = True

        INDSleThirdParty.Enabled = True
        INDSleThirdParty.EditValue = Nothing
        INDSbGenerateReport.Enabled = True
        INDSbGenerateExcel.Enabled = True
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim errors As New StringBuilder

        'validaciones controles de fecha
        If INDDteDateStart.EditValue Is Nothing Or INDDteDateEnd.EditValue Is Nothing Then
            errors.AppendLine(String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons")))
            Me.INDDteDateStart.Focus()
        ElseIf Me.INDDteDateStart.EditValue > INDDteDateEnd.EditValue Then
            errors.AppendLine(String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting")))
            Me.INDDteDateStart.Focus()
        End If

        If INDGleTypeReport.EditValue = 1 AndAlso INDGleGroupBy.EditValue Is Nothing Then
            errors.AppendLine("Seleccione Agrupar Por")
            Me.INDGleGroupBy.Focus()
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("TypeDocument", INDGleTypeDocument.EditValue)
        criterias.Add("TypeReport", INDGleTypeReport.EditValue)
        criterias.Add("BudgetValidityId", INDsleValidity.EditValue)
        criterias.Add("DateStart", INDDteDateStart.EditValue)
        criterias.Add("DateEnd", INDDteDateEnd.EditValue)
        criterias.Add("GroupBy", INDGleGroupBy.EditValue)
        criterias.Add("AvailabilityCode", INDSleAvailability.EditValue)
        criterias.Add("AvailabilityModificationCode", INDSleAvailabilityModification.EditValue)
        criterias.Add("CommitmentCode", INDSleCommitment.EditValue)
        criterias.Add("CommitmentModificationCode", INDSleCommitmentModification.EditValue)
        criterias.Add("ObligationCode", INDSleObligation.EditValue)
        criterias.Add("ObligationModificationCode", INDSleObligationModification.EditValue)
        criterias.Add("PaymentOrderCode", INDSlePaymentOrder.EditValue)
        criterias.Add("ReimbursementResourceCode", INDSleReimbursementResource.EditValue)
        criterias.Add("ThirdParties", _selector.GetKeys())

        Return True
    End Function

#Region "ToExcel"

    ''' <summary>
    ''' creamos un datatable para generar el excel de disponibilidades
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceAvailability(ByVal dtReportListDocumentExpense As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Fecha Vencimiento", GetType(DateTime))
        dt.Columns.Add("Tipo Disponibilidad")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Estado")

        dt.Columns.Add("Rubro Código")
        dt.Columns.Add("Rubro Nombre")
        dt.Columns.Add("Recurso Código")
        dt.Columns.Add("Recurso Nombre")
        dt.Columns.Add("Tipo Código")
        dt.Columns.Add("Tipo Nombre")

        dt.Columns.Add("Valor Inicial", GetType(Decimal))
        dt.Columns.Add("Valor Débito", GetType(Decimal))
        dt.Columns.Add("Valor Crédito", GetType(Decimal))
        dt.Columns.Add("Total", GetType(Decimal))
        dt.Columns.Add("Ejecutado", GetType(Decimal))
        dt.Columns.Add("Saldo", GetType(Decimal))

        For Each item In dtReportListDocumentExpense.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Fecha") = CDate(item("DocumentDate")).AsDate
            row.Item("Fecha Vencimiento") = CDate(item("ExpirationDate")).AsDate
            row.Item("Tipo Disponibilidad") = item("AvailabilityTypeName")
            row.Item("Observaciones") = item("Observations")
            row.Item("Estado") = item("StatusName")

            row.Item("Rubro Código") = item("CategoryCode")
            row.Item("Rubro Nombre") = item("CategoryName")
            row.Item("Recurso Código") = item("FinancialSourceCode")
            row.Item("Recurso Nombre") = item("FinancialSourceName")
            row.Item("Tipo Código") = item("RevenueTypeCode")
            row.Item("Tipo Nombre") = item("RevenueTypeName")

            row.Item("Valor Inicial") = item("InitialValue")
            row.Item("Valor Débito") = item("DebitValue")
            row.Item("Valor Crédito") = item("CreditValue")
            row.Item("Total") = item("TotalValue")
            row.Item("Ejecutado") = item("ExecutedValue")
            row.Item("Saldo") = item("Balance")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de modificacion de disponibilidades
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceAvailabilityModification(ByVal dtReportListDocumentExpense As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Disponibilidad")
        dt.Columns.Add("Documento")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Estado")

        dt.Columns.Add("Rubro Código")
        dt.Columns.Add("Rubro Nombre")
        dt.Columns.Add("Recurso Código")
        dt.Columns.Add("Recurso Nombre")
        dt.Columns.Add("Tipo Código")
        dt.Columns.Add("Tipo Nombre")

        dt.Columns.Add("Naturaleza")
        dt.Columns.Add("Valor", GetType(Decimal))

        For Each item In dtReportListDocumentExpense.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Fecha") = CDate(item("DocumentDate")).AsDate
            row.Item("Disponibilidad") = item("AvailabilityCode")
            row.Item("Documento") = item("Document")
            row.Item("Observaciones") = item("Observations")
            row.Item("Estado") = item("StatusName")

            row.Item("Rubro Código") = item("CategoryCode")
            row.Item("Rubro Nombre") = item("CategoryName")
            row.Item("Recurso Código") = item("FinancialSourceCode")
            row.Item("Recurso Nombre") = item("FinancialSourceName")
            row.Item("Tipo Código") = item("RevenueTypeCode")
            row.Item("Tipo Nombre") = item("RevenueTypeName")

            row.Item("Naturaleza") = item("NatureName")
            row.Item("Valor") = item("Value")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de compromisos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceCommitment(ByVal dtReportListDocumentExpense As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Fecha Vencimiento", GetType(DateTime))
        dt.Columns.Add("Tipo Compromiso")
        dt.Columns.Add("Documento")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Documento Origen")
        dt.Columns.Add("Documento Origen Código")

        dt.Columns.Add("Rubro Código")
        dt.Columns.Add("Rubro Nombre")
        dt.Columns.Add("Recurso Código")
        dt.Columns.Add("Recurso Nombre")
        dt.Columns.Add("Tipo Código")
        dt.Columns.Add("Tipo Nombre")

        dt.Columns.Add("Valor Inicial", GetType(Decimal))
        dt.Columns.Add("Valor Débito", GetType(Decimal))
        dt.Columns.Add("Valor Crédito", GetType(Decimal))
        dt.Columns.Add("Total", GetType(Decimal))
        dt.Columns.Add("Ejecutado", GetType(Decimal))
        dt.Columns.Add("Saldo", GetType(Decimal))

        For Each item In dtReportListDocumentExpense.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Fecha") = CDate(item("DocumentDate")).AsDate
            row.Item("Fecha Vencimiento") = CDate(item("ExpirationDate")).AsDate
            row.Item("Tipo Compromiso") = item("CommitmentTypeName")
            row.Item("Documento") = item("Document")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
            row.Item("Observaciones") = item("Observations")
            row.Item("Estado") = item("StatusName")
            row.Item("Documento Origen") = item("OriginName")
            row.Item("Documento Origen Código") = item("OriginCode")

            row.Item("Rubro Código") = item("CategoryCode")
            row.Item("Rubro Nombre") = item("CategoryName")
            row.Item("Recurso Código") = item("FinancialSourceCode")
            row.Item("Recurso Nombre") = item("FinancialSourceName")
            row.Item("Tipo Código") = item("RevenueTypeCode")
            row.Item("Tipo Nombre") = item("RevenueTypeName")

            row.Item("Valor Inicial") = item("InitialValue")
            row.Item("Valor Débito") = item("DebitValue")
            row.Item("Valor Crédito") = item("CreditValue")
            row.Item("Total") = item("TotalValue")
            row.Item("Ejecutado") = item("ExecutedValue")
            row.Item("Saldo") = item("Balance")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de modificacion de compromisos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceCommitmentModification(ByVal dtReportListDocumentExpense As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Compromiso")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Documento")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Documento Origen")
        dt.Columns.Add("Documento Origen Código")

        dt.Columns.Add("Rubro Código")
        dt.Columns.Add("Rubro Nombre")
        dt.Columns.Add("Recurso Código")
        dt.Columns.Add("Recurso Nombre")
        dt.Columns.Add("Tipo Código")
        dt.Columns.Add("Tipo Nombre")

        dt.Columns.Add("Naturaleza")
        dt.Columns.Add("Valor", GetType(Decimal))

        For Each item In dtReportListDocumentExpense.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Fecha") = CDate(item("DocumentDate")).AsDate
            row.Item("Compromiso") = item("CommitmentCode")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
            row.Item("Documento") = item("Document")
            row.Item("Observaciones") = item("Observations")
            row.Item("Estado") = item("StatusName")
            row.Item("Documento Origen") = item("OriginName")
            row.Item("Documento Origen Código") = item("OriginCode")

            row.Item("Rubro Código") = item("CategoryCode")
            row.Item("Rubro Nombre") = item("CategoryName")
            row.Item("Recurso Código") = item("FinancialSourceCode")
            row.Item("Recurso Nombre") = item("FinancialSourceName")
            row.Item("Tipo Código") = item("RevenueTypeCode")
            row.Item("Tipo Nombre") = item("RevenueTypeName")

            row.Item("Naturaleza") = item("NatureName")
            row.Item("Valor") = item("Value")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de obligaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceObligation(ByVal dtReportListDocumentExpense As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Fecha Vencimiento", GetType(DateTime))
        dt.Columns.Add("Tipo Obligación")
        dt.Columns.Add("Documento")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Documento Origen")
        dt.Columns.Add("Documento Origen Código")

        dt.Columns.Add("Rubro Código")
        dt.Columns.Add("Rubro Nombre")
        dt.Columns.Add("Recurso Código")
        dt.Columns.Add("Recurso Nombre")
        dt.Columns.Add("Tipo Código")
        dt.Columns.Add("Tipo Nombre")

        dt.Columns.Add("Valor Inicial", GetType(Decimal))
        dt.Columns.Add("Valor Débito", GetType(Decimal))
        dt.Columns.Add("Valor Crédito", GetType(Decimal))
        dt.Columns.Add("Total", GetType(Decimal))
        dt.Columns.Add("Ejecutado", GetType(Decimal))
        dt.Columns.Add("Saldo", GetType(Decimal))

        For Each item In dtReportListDocumentExpense.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Fecha") = CDate(item("DocumentDate")).AsDate
            row.Item("Fecha Vencimiento") = CDate(item("ExpirationDate")).AsDate
            row.Item("Tipo Obligación") = item("ObligationTypeName")
            row.Item("Documento") = item("Document")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
            row.Item("Observaciones") = item("Observations")
            row.Item("Estado") = item("StatusName")
            row.Item("Documento Origen") = item("OriginName")
            row.Item("Documento Origen Código") = item("OriginCode")

            row.Item("Rubro Código") = item("CategoryCode")
            row.Item("Rubro Nombre") = item("CategoryName")
            row.Item("Recurso Código") = item("FinancialSourceCode")
            row.Item("Recurso Nombre") = item("FinancialSourceName")
            row.Item("Tipo Código") = item("RevenueTypeCode")
            row.Item("Tipo Nombre") = item("RevenueTypeName")

            row.Item("Valor Inicial") = item("InitialValue")
            row.Item("Valor Débito") = item("DebitValue")
            row.Item("Valor Crédito") = item("CreditValue")
            row.Item("Total") = item("TotalValue")
            row.Item("Ejecutado") = item("ExecutedValue")
            row.Item("Saldo") = item("Balance")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de modificacion de obligaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceObligationModification(ByVal dtReportListDocumentExpense As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Obligación")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Documento")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Documento Origen")
        dt.Columns.Add("Documento Origen Código")

        dt.Columns.Add("Rubro Código")
        dt.Columns.Add("Rubro Nombre")
        dt.Columns.Add("Recurso Código")
        dt.Columns.Add("Recurso Nombre")
        dt.Columns.Add("Tipo Código")
        dt.Columns.Add("Tipo Nombre")

        dt.Columns.Add("Naturaleza")
        dt.Columns.Add("Valor", GetType(Decimal))

        For Each item In dtReportListDocumentExpense.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Fecha") = CDate(item("DocumentDate")).AsDate
            row.Item("Obligación") = item("ObligationCode")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
            row.Item("Documento") = item("Document")
            row.Item("Observaciones") = item("Observations")
            row.Item("Estado") = item("StatusName")
            row.Item("Documento Origen") = item("OriginName")
            row.Item("Documento Origen Código") = item("OriginCode")

            row.Item("Rubro Código") = item("CategoryCode")
            row.Item("Rubro Nombre") = item("CategoryName")
            row.Item("Recurso Código") = item("FinancialSourceCode")
            row.Item("Recurso Nombre") = item("FinancialSourceName")
            row.Item("Tipo Código") = item("RevenueTypeCode")
            row.Item("Tipo Nombre") = item("RevenueTypeName")

            row.Item("Naturaleza") = item("NatureName")
            row.Item("Valor") = item("Value")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de ordenes de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourcePaymentOrder(ByVal dtReportListDocumentExpense As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Fecha Vencimiento", GetType(DateTime))
        dt.Columns.Add("Tipo Orden Pago")
        dt.Columns.Add("Documento")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Documento Origen")
        dt.Columns.Add("Documento Origen Código")

        dt.Columns.Add("Rubro Código")
        dt.Columns.Add("Rubro Nombre")
        dt.Columns.Add("Recurso Código")
        dt.Columns.Add("Recurso Nombre")
        dt.Columns.Add("Tipo Código")
        dt.Columns.Add("Tipo Nombre")

        dt.Columns.Add("Valor Inicial", GetType(Decimal))
        dt.Columns.Add("Valor Débito", GetType(Decimal))
        dt.Columns.Add("Valor Crédito", GetType(Decimal))
        dt.Columns.Add("Total", GetType(Decimal))

        For Each item In dtReportListDocumentExpense.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Fecha") = CDate(item("DocumentDate")).AsDate
            row.Item("Fecha Vencimiento") = CDate(item("ExpirationDate")).AsDate
            row.Item("Tipo Orden Pago") = item("PaymentOrderTypeName")
            row.Item("Documento") = item("Document")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
            row.Item("Observaciones") = item("Observations")
            row.Item("Estado") = item("StatusName")
            row.Item("Documento Origen") = item("OriginName")
            row.Item("Documento Origen Código") = item("OriginCode")

            row.Item("Rubro Código") = item("CategoryCode")
            row.Item("Rubro Nombre") = item("CategoryName")
            row.Item("Recurso Código") = item("FinancialSourceCode")
            row.Item("Recurso Nombre") = item("FinancialSourceName")
            row.Item("Tipo Código") = item("RevenueTypeCode")
            row.Item("Tipo Nombre") = item("RevenueTypeName")

            row.Item("Valor Inicial") = item("InitialValue")
            row.Item("Valor Débito") = item("DebitValue")
            row.Item("Valor Crédito") = item("CreditValue")
            row.Item("Total") = item("TotalValue")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel de reintegros
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasourceReimbursementResource(ByVal dtReportListDocumentExpense As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha", GetType(DateTime))
        dt.Columns.Add("Orden Pago")
        dt.Columns.Add("Tercero Nit")
        dt.Columns.Add("Tercero Nombre")
        dt.Columns.Add("Documento")
        dt.Columns.Add("Observaciones")
        dt.Columns.Add("Estado")

        dt.Columns.Add("Rubro Código")
        dt.Columns.Add("Rubro Nombre")
        dt.Columns.Add("Recurso Código")
        dt.Columns.Add("Recurso Nombre")
        dt.Columns.Add("Tipo Código")
        dt.Columns.Add("Tipo Nombre")

        dt.Columns.Add("Naturaleza")
        dt.Columns.Add("Valor", GetType(Decimal))

        For Each item In dtReportListDocumentExpense.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = item("Code")
            row.Item("Fecha") = CDate(item("DocumentDate")).AsDate
            row.Item("Orden Pago") = item("PaymentOrderCode")
            row.Item("Tercero Nit") = item("ThirdPartyNit")
            row.Item("Tercero Nombre") = item("ThirdPartyName")
            row.Item("Documento") = item("Document")
            row.Item("Observaciones") = item("Observations")
            row.Item("Estado") = item("StatusName")

            row.Item("Rubro Código") = item("CategoryCode")
            row.Item("Rubro Nombre") = item("CategoryName")
            row.Item("Recurso Código") = item("FinancialSourceCode")
            row.Item("Recurso Nombre") = item("FinancialSourceName")
            row.Item("Tipo Código") = item("RevenueTypeCode")
            row.Item("Tipo Nombre") = item("RevenueTypeName")

            row.Item("Naturaleza") = item("NatureName")
            row.Item("Valor") = item("Value")
            dt.Rows.Add(row)
        Next

        INDGcExportExcel.DataSource = dt
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & INDGleTypeReport.EditValue & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcel.DataSource = Nothing
        Me.INDGcExportExcel.RefreshDataSource()
    End Sub

#End Region

#End Region

End Class