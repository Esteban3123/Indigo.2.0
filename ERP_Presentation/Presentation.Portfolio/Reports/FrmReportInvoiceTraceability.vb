' Assembly         : Presentation.Portfolio
' Author           : Dayan Mauricio Sabi
' Created          : 18-02-2020
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Portfolio.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportInvoiceTraceability
    Implements IReportInvoiceTraceability

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PReportInvoiceTraceability

#End Region

#Region "Datasource"

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Facturación sin Glosa"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Facturación con Glosa"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(3, "Todos"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Public Property OperatingUnitXpo As XPInstantFeedbackSource Implements IReportInvoiceTraceability.OperatingUnitXpo
        Get
            Return INDSleOperatingUnit.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleOperatingUnit.Properties.DataSource = value
        End Set
    End Property

    Public Property InvoiceXpo As XPInstantFeedbackSource Implements IReportInvoiceTraceability.InvoiceXpo
        Get
            Return INDSleInvoice.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleInvoice.Properties.DataSource = value
        End Set
    End Property

    Public Property CustomerXpo As XPInstantFeedbackSource Implements IReportInvoiceTraceability.CustomerXpo
        Get
            Return INDSleCustomer.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCustomer.Properties.DataSource = value
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
        Dim errors As New StringBuilder

        If Me.INDDeCutoffDate.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una fecha de corte")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        Return True
    End Function

#Region "Selector"

    Private _selectorOperatingUnit As SelectorCache = New SelectorCache("Id", "UnitCode")
    Private _selectorInvoice As SelectorCache = New SelectorCache("Id", "InvoiceNumber")
    Private _selectorCustomer As SelectorCache = New SelectorCache("Id", "Nit")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvOperatingUnit.CustomUnboundColumnData, INDGvInvoice.CustomUnboundColumnData, INDGvCustomer.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvOperatingUnit" Then
                e.Value = _selectorOperatingUnit.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvInvoice" Then
                e.Value = _selectorInvoice.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvCustomer" Then
                e.Value = _selectorCustomer.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvOperatingUnit.RowCellClick, INDGvInvoice.RowCellClick, INDGvCustomer.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvOperatingUnit" Then
                selector = _selectorOperatingUnit
            ElseIf view.Name = "INDGvInvoice" Then
                selector = _selectorInvoice
            ElseIf view.Name = "INDGvCustomer" Then
                selector = _selectorCustomer
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

    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleOperatingUnit.Closed, INDSleInvoice.Closed, INDSleCustomer.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleOperatingUnit" Then
            searchLookupEdit.Properties.NullText = _selectorOperatingUnit.ToString()
        ElseIf searchLookupEdit.Name = "INDSleInvoice" Then
            searchLookupEdit.Properties.NullText = _selectorInvoice.ToString()
        ElseIf searchLookupEdit.Name = "INDSleCustomer" Then
            searchLookupEdit.Properties.NullText = _selectorCustomer.ToString()
        End If
    End Sub

#End Region

#Region "ToExcel"

    Private Sub chargueDatasourceInvoiceTraceability()
        INDGcExportExcell.DataSource = _presenter.LoadDatasourceInvoiceTraceability(INDDeCutoffDate.EditValue, _selectorOperatingUnit.GetKeys(), _selectorCustomer.GetKeys(), _selectorInvoice.GetKeys(), INDGleTypeReport.EditValue)
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        '_gridView.MainView.PopulateColumns()
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
    ''' se ejecuta al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReporInvoiceTraceability_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargar el GridLookUpEdit
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport

        'Asigna un valor por defecto a GridLookUpEdit
        Me.INDGleTypeReport.EditValue = 1

        'Inicializamos la referencia
        _presenter = New PReportInvoiceTraceability(Me)
    End Sub

    ''' <summary>
    ''' Se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing

        _FillingTypeReport = Nothing
        _selectorOperatingUnit = Nothing
        _selectorInvoice = Nothing
        _selectorCustomer = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleOperatingUnit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleOperatingUnit_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleOperatingUnit.QueryPopUp
        If INDSleOperatingUnit.Properties.DataSource Is Nothing Then
            _presenter.ListCollectionOperatingUnit()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleInvoice validando el tipo de reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleInvoice_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleInvoice.QueryPopUp
        If Me.INDSleInvoice.Properties.DataSource Is Nothing Then
            _presenter.ListAccountReceivable()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCustomer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCustomer_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCustomer.QueryPopUp
        If INDSleCustomer.Properties.DataSource Is Nothing Then
            _presenter.ListCollectionCustomerReportPortfolio()
        End If
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)

            Dim reporte As New rptTraceabilityInvoice
            reporte.ParametrosReporte = {INDDeCutoffDate.EditValue, _selectorOperatingUnit.GetKeys(), _selectorCustomer.GetKeys(), _selectorInvoice.GetKeys(), INDGleTypeReport.EditValue}
            INDDvDocumentViewer.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDeCutoffDate.Focus()
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
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            Try
                AsyncLoader(True)
                Await Task.Factory.StartNew(AddressOf chargueDatasourceInvoiceTraceability)

                If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                    generateExcel()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                End If
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