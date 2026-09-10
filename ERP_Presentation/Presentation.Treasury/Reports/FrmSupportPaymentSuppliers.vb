#Region "Imports"
Imports System.Dynamic
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Reporter
Imports Presentation.Treasury.MVP

#End Region

Public Class FrmSupportPaymentSuppliers

#Region "Properties"

    ''' <summary>
    ''' esta propiedad permite acceder y configurar la fuente de datos en
    ''' tiempo real para un control denominado INDSleInvoice
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoInvoice As XPInstantFeedbackSource
        Get
            Return INDSleInvoice.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleInvoice.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad permite acceder y configurar la fuente de datos en
    ''' tiempo real para un control denominado INDSleVoucher
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoVoucher As XPInstantFeedbackSource
        Get
            Return INDSleVoucher.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleVoucher.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad permite acceder y configurar la fuente de datos en
    ''' tiempo real para un control denominado INDSleSupplier
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoSupplier As XPInstantFeedbackSource
        Get
            Return INDSleSupplier.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleSupplier.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad permite acceder y configurar la fuente de datos en
    ''' tiempo real para un control denominado INDDateStart
    ''' </summary>
    ''' <returns></returns>
    Public Property DateStart As DateTime?
        Get
            Return INDDateStart.EditValue
        End Get
        Set(value As DateTime?)
            INDDateStart.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' esta propiedad permite acceder y configurar la fuente de datos en
    ''' tiempo real para un control denominado INDDateEnd
    ''' </summary>
    ''' <returns></returns>
    Public Property DateEnd As DateTime?
        Get
            Return INDDateEnd.EditValue
        End Get
        Set(value As DateTime?)
            INDDateEnd.EditValue = value
        End Set
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

#Region "Variable"
    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' The model
    ''' </summary>
    Dim model As MVoucherTransaction

    ''' <summary>
    ''' diccionario para obtenes los filtros
    ''' </summary>
    Private _filters As Dictionary(Of String, String)
#End Region

#Region "Click"

    ''' <summary>
    ''' Su función principal es generar un archivo Excel a partir de los datos
    ''' obtenidos de una fuente de datos cargada de manera asíncrona.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <returns></returns>
    Private Async Function INDSbGenerateExcell_ClickAsync(sender As Object, e As EventArgs) As Task Handles INDSbGenerateExcell.Click
        Try
            INDGcExportExcell.DataSource = Await chargueDatasource()
            generateExcel()
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error al generar el archivo Excel: " & ex.Message)
        End Try
    End Function


    ''' <summary>
    ''' Esta función, llamada GetFilters, se encarga de crear y poblar un diccionario de filtros
    ''' en una aplicación o sistema. El diccionario, denominado _filters, almacena pares clave-valor
    ''' que representan diversos criterios de filtrado.
    ''' </summary>
    Private Sub GetFilters()
        _filters = New Dictionary(Of String, String)
        _filters.Add("StartDate", DateStart)
        _filters.Add("EndDate", DateEnd)
        _filters.Add("Supplier", _selectorSuppliers.GetKeys())
        _filters.Add("Invoice", _selectorInvoice.GetKeys())
        _filters.Add("Voucher", _selectorVoucher.GetKeys())
    End Sub


    ''' <summary>
    ''' Esta función es un controlador de eventos asincrónico que responde al clic en un botón
    ''' llamado INDSbGenerareReport. Su objetivo principal es generar un informe o reporte en formato
    ''' PDF en función de los filtros configurados y mostrarlo en una vista de informe en la interfaz de usuario.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbGenerareReport_ClickAsync(sender As Object, e As EventArgs) Handles INDSbGenerareReport.Click
        Try
            AsyncLoader(True)
            GetFilters()
            Dim reporte As New rptSupportPaymentSuppliers

            Dim XmlFilter = Utils.DictionaryToXML(_filters)

            INDDvViewReport.DocumentSource = reporte

            Dim obj As Object = New ExpandoObject
            obj.XmlFilter = XmlFilter

            reporte.ParametrosReporte = New Object() {obj}

            Await CType(reporte, IReportAsync).CargarDataSourceAsync

            Dim permission As New Dictionary(Of Integer, String)
            permission.Add(PermissionsActionsForm.PersonalizarReporte, "Personalizar")

            INDDvViewReport.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Customize, CommandVisibility.All)

            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
            End If
            AsyncLoader(False)

            If reporte.DataSource IsNot Nothing Then
                INDLcBase.Visible = False
                INDCncNavigation.Visible = False
                INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                INDDateStart.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show("Ocurrió un error al generar el archivo PDF: " & ex.Message)
        End Try
    End Sub

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Su principal función es llevar a cabo ciertas tareas de inicialización en el formulario.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
        Me.Indigo = SessionValues.Instance
    End Sub
#End Region

#Region "Events"
#Region "Selector"

    ''' <summary>
    ''' Estas tres variables, _selectorSuppliers, _selectorInvoice y _selectorVoucher, son instancias
    ''' de la clase SelectorCache utilizadas en una aplicación o sistema.
    ''' Cada una de estas instancias se configura de manera similar con dos parámetros, "Id" y "Code".
    ''' </summary>
    Private _selectorSuppliers As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorInvoice As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorVoucher As SelectorCache = New SelectorCache("Id", "Code")


    ''' <summary>
    ''' Selector para obtener los datos de INDSlevSupplier, INDSlevInvoice y INDSlevVoucher
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBoxSelect_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDSlevSupplier.CustomUnboundColumnData, INDSlevInvoice.CustomUnboundColumnData, INDSlevVoucher.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDSlevSupplier" Then
                e.Value = _selectorSuppliers.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDSlevInvoice" Then
                e.Value = _selectorInvoice.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDSlevVoucher" Then
                e.Value = _selectorVoucher.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub


    ''' <summary>
    ''' Verifica si son los datos seleccionados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBoxSelect_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDSlevSupplier.RowCellClick, INDSlevInvoice.RowCellClick, INDSlevVoucher.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDSlevSupplier" Then
                selector = _selectorSuppliers
            ElseIf view.Name = "INDSlevInvoice" Then
                selector = _selectorInvoice
            ElseIf view.Name = "INDSlevVoucher" Then
                selector = _selectorVoucher
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
    ''' Se cierra los selectores con la data cargada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBoxSelect_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleSupplier.Closed, INDSleInvoice.Closed, INDSleVoucher.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = NameOf(INDSleSupplier) Then
            searchLookupEdit.Properties.NullText = _selectorSuppliers.ToString()

        ElseIf searchLookupEdit.Name = "INDSleInvoice" Then
            searchLookupEdit.Properties.NullText = _selectorInvoice.ToString()

        ElseIf searchLookupEdit.Name = "INDSleVoucher" Then
            searchLookupEdit.Properties.NullText = _selectorVoucher.ToString()
        End If
    End Sub
#End Region
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' La función principal de este evento es validar las fechas seleccionadas para asegurarse
    ''' de que la fecha de finalización no sea anterior a la fecha de inicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDDateEnd_EditValueChanged(sender As Object, e As EventArgs) Handles INDDateEnd.EditValueChanged, INDDateStart.EditValueChanged
        If DateEnd IsNot Nothing AndAlso Me.DateStart IsNot Nothing Then
            If Me.DateEnd < Me.DateStart Then
                MessageBox.Show("La fecha de finalización no puede ser mayor que la de inicio.", "Error de fecha", MessageBoxButtons.OK, MessageBoxIcon.Error)
                INDDateEnd.EditValue = Me.DateStart
            End If
        End If
    End Sub

#End Region

#Region "Method"

#Region "QueryPopUp"

    ''' <summary>
    ''' La función principal de este evento es cargar la lista de proveedores disponibles si aún no se ha cargado previamente.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleSupplier.QueryPopUp
        If ProoftCloseXpoSupplier Is Nothing Then
            ProoftCloseXpoSupplier = XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListSupplierByStatus(True)
        End If
    End Sub


    ''' <summary>
    ''' La función principal de este evento es cargar la lista de Facturas disponibles si aún no se ha cargado previamente.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleInvoice_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleInvoice.QueryPopUp
        Using msearch As New MBusqueda
            ProoftCloseXpoInvoice = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayable)
        End Using
    End Sub


    ''' <summary>
    ''' La función principal de este evento es cargar la lista de comprobantes disponibles si aún no se ha cargado previamente.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleVoucher_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleVoucher.QueryPopUp
        If INDSleVoucher.Properties.DataSource Is Nothing Then
            ProoftCloseXpoVoucher = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListAllVoucherTransaction()
        End If
    End Sub
#End Region

#Region "ToExcel"

    ''' <summary>
    ''' Esta función es una tarea asincrónica que se encarga de cargar y devolver una lista de resultados de soporte de pagos de proveedores
    ''' </summary>
    ''' <returns></returns>
    Private Async Function chargueDatasource() As Task(Of List(Of SP_SupportPaymentSuppliers_Result))

        Me.GetFilters()
        Dim xml = Utils.DictionaryToXML(_filters)

        Using Model As New MSupportPaymentSuppliers("")
            Dim result = Await Model.GetSupportPaymentSuppliers(xml)
            If result Is Nothing OrElse result.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = result?.Message
                Return New List(Of SP_SupportPaymentSuppliers_Result)
            End If
            Return result.ObjectEmbbeded
        End Using
    End Function

    ''' <summary>
    ''' Esta función se encarga de generar un archivo Excel a partir de los datos presentes
    ''' en un control de cuadrícula (INDGcExportExcell) y luego abrir este archivo Excel en
    ''' la aplicación predeterminada para visualización.
    ''' </summary>
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

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
    End Sub
#End Region
#End Region
End Class