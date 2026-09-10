#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmReportListAccountsPayable

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Datasource"

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Listado Cuentas Por Pagar"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Documento Cuentas Por Pagar"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(3, "Listado por Fuentes de Financiación"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private Property DistributionLineXpo As XPInstantFeedbackSource
        Get
            Return INDSleDistributionLine.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleDistributionLine.Properties.DataSource = value
        End Set
    End Property

    Private Property SupplierXpo As XPInstantFeedbackSource
        Get
            Return INDSleSupplier.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleSupplier.Properties.DataSource = value
        End Set
    End Property

    Private Property BudgetXpo As XPInstantFeedbackSource
        Get
            Return INDSleBudget.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleBudget.Properties.DataSource = value
        End Set
    End Property

    Private Property AccountPayableXpo As XPInstantFeedbackSource
        Get
            Return INDSleAccountPayable.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAccountPayable.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "BarraBotones"

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

        Return Validations
    End Function

#Region "Selector"

    Private _selectorDistributionLine As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorSupplier As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorBudget As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorAccountPayable As SelectorCache = New SelectorCache("Id", "Code")

    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvDistributionLine.CustomUnboundColumnData, INDGvSupplier.CustomUnboundColumnData, INDGvBudget.CustomUnboundColumnData, INDGvAccountPayable.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvDistributionLine" Then
                e.Value = _selectorDistributionLine.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvSupplier" Then
                e.Value = _selectorSupplier.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvBudget" Then
                e.Value = _selectorBudget.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvAccountPayable" Then
                e.Value = _selectorAccountPayable.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvDistributionLine.RowCellClick, INDGvSupplier.RowCellClick, INDGvBudget.RowCellClick, INDGvAccountPayable.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvDistributionLine" Then
                selector = _selectorDistributionLine
            ElseIf view.Name = "INDGvSupplier" Then
                selector = _selectorSupplier
            ElseIf view.Name = "INDGvBudget" Then
                selector = _selectorBudget
            ElseIf view.Name = "INDGvAccountPayable" Then
                selector = _selectorAccountPayable
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

    Private Sub INDSle_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleDistributionLine.Closed, INDSleSupplier.Closed, INDSleBudget.Closed, INDSleAccountPayable.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleDistributionLine" Then
            searchLookupEdit.Properties.NullText = _selectorDistributionLine.ToString()
        ElseIf searchLookupEdit.Name = "INDSleSupplier" Then
            searchLookupEdit.Properties.NullText = _selectorSupplier.ToString()
        ElseIf searchLookupEdit.Name = "INDSleBudget" Then
            searchLookupEdit.Properties.NullText = _selectorBudget.ToString()
        ElseIf searchLookupEdit.Name = "INDSleAccountPayable" Then
            searchLookupEdit.Properties.NullText = _selectorAccountPayable.ToString()
        End If
    End Sub

#End Region

#End Region

#Region "Events"

#Region "Load"

    Private Sub FrmReportListAccountsPayable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)

        'Cargar GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleTypeReport.EditValue = 1
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing

        _FillingTypeReport = Nothing

        DistributionLineXpo = Nothing
        SupplierXpo = Nothing
        BudgetXpo = Nothing
        AccountPayableXpo = Nothing
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleDistributionLine_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleDistributionLine.QueryPopUp
        If DistributionLineXpo Is Nothing Then
            DistributionLineXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).CommonService.ListDistributionLine()
        End If
    End Sub

    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleSupplier.QueryPopUp
        If SupplierXpo Is Nothing Then
            SupplierXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).PaymentsService.ListSupplierReport()
        End If
    End Sub

    Private Sub INDSleBudget_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleBudget.QueryPopUp
        If BudgetXpo Is Nothing Then
            BudgetXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListBudgetByType(2)
        End If
    End Sub

    Private Sub INDSleAccountPayable_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAccountPayable.QueryPopUp
        If AccountPayableXpo Is Nothing Then
            AccountPayableXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).PaymentsService.ListPaymentsAccountPayableReport()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        INDLciBudget.Visibility = If(INDGleTypeReport.EditValue = 3, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerareReport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            Try
                AsyncLoader(True)
                If INDGleTypeReport.EditValue = 1 Then
                    Dim reporte As New rptListAccountsPayable
                    reporte.ParametrosReporte = {INDDateStart.EditValue,
                                                INDDateEnd.EditValue,
                                                INDCcbeStatus.EditValue,
                                                _selectorDistributionLine.GetKeys(),
                                                _selectorSupplier.GetKeys(),
                                                _selectorAccountPayable.GetKeys()}

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
                ElseIf INDGleTypeReport.EditValue = 2 Then
                    Dim reporte As New rptDocumentAccountsPayable
                    reporte.ParametrosReporte = {INDDateStart.EditValue,
                                                INDDateEnd.EditValue,
                                                INDCcbeStatus.EditValue,
                                                _selectorDistributionLine.GetKeys(),
                                                _selectorSupplier.GetKeys(),
                                                _selectorAccountPayable.GetKeys()}

                    INDDvViewReport.DocumentSource = reporte
                    reporte.CargarDataSource()
                    If reporte.DataSource IsNot Nothing AndAlso DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                        AsyncLoader(False)
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                ElseIf INDGleTypeReport.EditValue = 3 Then
                    Dim reporte As New rptListAccountsPayableByBudget
                    reporte.ParametrosReporte = {INDDateStart.EditValue,
                                                INDDateEnd.EditValue,
                                                INDCcbeStatus.EditValue,
                                                _selectorDistributionLine.GetKeys(),
                                                _selectorSupplier.GetKeys(),
                                                _selectorAccountPayable.GetKeys(),
                                                _selectorBudget.GetKeys()}

                    INDDvViewReport.DocumentSource = reporte
                    reporte.CargarDataSource()
                    If reporte.DataSource IsNot Nothing AndAlso DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                        reporte.CreateDocument(True)
                        AsyncLoader(False)
                        Me.INDLcBase.Visible = False
                        Me.INDCncNavigation.Visible = False
                        Me.INDPcViewReport.Visible = True
                        INDDvViewReport.Show()
                    Else
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                        Me.INDDateStart.Focus()
                    End If
                End If
            Catch ex As Exception
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            End Try
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

#End Region

#End Region

End Class