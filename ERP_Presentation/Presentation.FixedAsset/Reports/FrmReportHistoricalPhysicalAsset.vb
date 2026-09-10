'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/11/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Controls.MVP
Imports Presentation.Reporter
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports DevExpress.Data.Async.Helpers
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Billing.MVP
Imports System.ComponentModel
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

#End Region

Public Class FrmReportHistoricalPhysicalAsset

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private _IncludeDepreciated As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' Listado de depreciados
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property IncludeDepreciated As List(Of Tuple(Of Integer, String))
        Get
            If _IncludeDepreciated Is Nothing Then
                _IncludeDepreciated = New List(Of Tuple(Of Integer, String))
                _IncludeDepreciated.Add(New Tuple(Of Integer, String)(0, "No"))
                _IncludeDepreciated.Add(New Tuple(Of Integer, String)(1, "Si"))
            End If
            Return _IncludeDepreciated
        End Get
    End Property

#End Region

#Region "Globals"

    ''' <summary>
    ''' Obtiene los estados seleccionados
    ''' </summary>
    Dim statusFilters As String

    ''' <summary>
    ''' Obtiene los tipos de adquisición seleccionados
    ''' </summary>
    Dim adquisitionTypesFilters As String

    ''' <summary>
    ''' Obtiene los articulos seleccionados
    ''' </summary>
    Dim itemsFilters As String

    ''' <summary>
    ''' Obtiene los tipos de equipo seleccionados
    ''' </summary>
    Dim itemTypeFilters As String

    ''' <summary>
    ''' Obtiene las placas seleccionadas
    ''' </summary>
    Dim plateFilters As String

    ''' <summary>
    ''' Dictionario para enviar los filtros del reporte
    ''' </summary>
    Private filters As Dictionary(Of String, String)

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub FrmReportHistoricalDepreciation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.INDSleIncludeDepreciated.Properties.DataSource = IncludeDepreciated

        Me.INDSleIncludeDepreciated.EditValue = 1
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed

    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de placa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpcePlate_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpcePlate.QueryPopUp
        If INDgcPlate.DataSource Is Nothing Then
            Using msearch As New MBusqueda
                INDgcPlate.DataSource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de articulo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceItem_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceItem.QueryPopUp
        If INDgcItem.DataSource Is Nothing Then
            Using msearch As New MBusqueda
                INDgcItem.DataSource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipment)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo equipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceEquipmentType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceEquipmentType.QueryPopUp
        If INDgcEquipmentType.DataSource Is Nothing Then
            Using msearch As New MBusqueda
                INDgcEquipmentType.DataSource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentType)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de libro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleLegalBook2_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleLegalBook.QueryPopUp
        If INDsleLegalBook.Properties.DataSource Is Nothing Then
            Using msearch As New MBusqueda
                Dim filter() As Object = {True}
                INDsleLegalBook.Properties.DataSource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBookByStatusXpCollection, filter)
            End Using
        End If
    End Sub

#End Region

#Region "SelectionChanged"

    ''' <summary>
    ''' Evento que se dispara al seleccionar un check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewPlate_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewPlate.SelectionChanged
        INDpcePlate.Text = DirectCast(INDgcPlate.FocusedView, GridView).GetSelectedRows().Count().ToString() + " Items Seleccionados"
    End Sub

    ''' <summary>
    ''' Evento que se dispara al seleccionar un check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewItem_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewItem.SelectionChanged
        INDpceItem.Text = DirectCast(INDgcItem.FocusedView, GridView).GetSelectedRows().Count().ToString() + " Items Seleccionados"
    End Sub

    ''' <summary>
    ''' Evento que se dispara al seleccionar un check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewEquipmentType_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewEquipmentType.SelectionChanged
        INDpceEquipmentType.Text = DirectCast(INDgcEquipmentType.FocusedView, GridView).GetSelectedRows().Count().ToString() + " Items Seleccionados"
    End Sub

#End Region

#Region "Click"

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If ValidateControlsResport() = False Then
            Exit Sub
        End If
        GetFilters()
        AsyncLoader(True)
        Dim reporte As New rptHistoricalPhysicalAsset
        reporte.ParametrosReporte = New Object() {filters}
        INDDvReport.DocumentSource = reporte

        Await reporte.CargarDataSourceAsync()
        AsyncLoader(False)
        If reporte.DataSource IsNot Nothing Then
            reporte.CreateDocument(True)
            Me.INDLcBase.Visible = False
            Me.INDCncNavigation.Visible = False
            Me.INDPcReport.Visible = True
            INDDvReport.Show()
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDCdnDate.Focus()
        End If
    End Sub

    Private Sub CtrNavigation1_ClickBack() Handles CtrNavigation1.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDCdnDate.Focus()
    End Sub

    Private Async Sub INDSbExportReport_Click(sender As Object, e As EventArgs) Handles INDSbExportReport.Click
        If Me.ValidateControlsResport = False Then
            Exit Sub
        End If

        Try
            GetFilters()
            AsyncLoader(True)

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SP_ReportHistoricalPhysicalAssetAsync(filters, Me.IndigoSessionValues)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim dtReportHistoricalPhysicalAsset As DataTable = ds.Tables("ReportHistoricalPhysicalAsset")

                chargueDatasource(dtReportHistoricalPhysicalAsset)

                If Me.INDgcExportExcel.DataSource IsNot Nothing Then
                    generateExcel()
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del control de libros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleLegalBook2_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLegalBook.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1682, Nothing, True)

        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Se obtienen los filtros
    ''' </summary>
    Private Sub GetFilters()
        statusFilters = String.Join(",", (From x As DevExpress.XtraEditors.Controls.CheckedListBoxItem In INDccbeStatus2.Properties.Items Where x.CheckState = System.Windows.Forms.CheckState.Checked Select x.Value).ToArray())
        adquisitionTypesFilters = String.Join(",", (From x As DevExpress.XtraEditors.Controls.CheckedListBoxItem In INDCcbeAdquisitionType.Properties.Items Where x.CheckState = System.Windows.Forms.CheckState.Checked Select x.Value).ToArray())
        itemsFilters = String.Join(",", (From x In DirectCast(INDgcItem.FocusedView, GridView).GetSelectedRows() Select DirectCast(DirectCast(INDgcItem.FocusedView, GridView).GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.Id))
        itemTypeFilters = String.Join(",", (From x In DirectCast(INDgcEquipmentType.FocusedView, GridView).GetSelectedRows() Select DirectCast(DirectCast(INDgcEquipmentType.FocusedView, GridView).GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.Id))
        plateFilters = String.Join(",", (From x In DirectCast(INDgcPlate.FocusedView, GridView).GetSelectedRows() Select DirectCast(DirectCast(INDgcPlate.FocusedView, GridView).GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.Id))

        filters = New Dictionary(Of String, String)
        filters.Add("Year", INDCdnDate.GetYear)
        filters.Add("Month", INDCdnDate.GetMonth)
        filters.Add("LegalBookId", INDsleLegalBook.EditValue)
        filters.Add("StatusFilter", statusFilters)
        filters.Add("IncludedDepreciation", INDSleIncludeDepreciated.EditValue)
        filters.Add("AdquisitionTypeFilter", adquisitionTypesFilters)
        filters.Add("ItemFilter", itemsFilters)
        filters.Add("ItemTypeFilter", itemTypeFilters)
        filters.Add("PlateFilter", plateFilters)
        filters.Add("LegalBookCodeName", INDsleLegalBook.Text)
    End Sub

    ''' <summary>
    ''' Valida los controles del reporte
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsResport() As Boolean
        Dim errors As New StringBuilder

        If INDsleLegalBook.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un libro")
        End If

        If errors.ToString().Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        Return True
    End Function

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
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Sub chargueDatasource(dtReportHistoricalPhysicalAsset As DataTable)
        Dim dt As New DataTable
        dt.Columns.Add("Libro")
        dt.Columns.Add("Año")
        dt.Columns.Add("Mes")
        dt.Columns.Add("Placa")
        dt.Columns.Add("Serie")
        dt.Columns.Add("Modelo")
        dt.Columns.Add("Fecha Adquisición", GetType(DateTime))
        dt.Columns.Add("Tipo de Adquisición")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Catálogo")
        dt.Columns.Add("Artículo")
        dt.Columns.Add("Tipo de Equipo")
        dt.Columns.Add("Responsable")
        dt.Columns.Add("Localización")
        dt.Columns.Add("Cuenta del Activo")
        dt.Columns.Add("Cuenta de la Depreciación")
        dt.Columns.Add("Valor Valorización", GetType(Decimal))
        dt.Columns.Add("Valor Desvalorización", GetType(Decimal))
        dt.Columns.Add("Valor Histórico", GetType(Decimal))
        dt.Columns.Add("Depreciación Acumulada", GetType(Decimal))
        dt.Columns.Add("Valor Depreciación Mes", GetType(Decimal))
        dt.Columns.Add("Días Depreciados", GetType(Integer))

        For Each item In dtReportHistoricalPhysicalAsset.Rows
            Dim row As DataRow = dt.NewRow()
            row.Item("Libro") = INDsleLegalBook.Text
            row.Item("Año") = INDCdnDate.GetYear
            row.Item("Mes") = INDCdnDate.GetMonth
            row.Item("Placa") = item("Plate")
            row.Item("Serie") = item("Serie")
            row.Item("Modelo") = item("Model")
            row.Item("Fecha Adquisición") = CDate(item("AdquisitionDate")).AsDate
            row.Item("Tipo de Adquisición") = item("AdquisitionTypeDescription")
            row.Item("Estado") = item("StatusDescription")
            row.Item("Catálogo") = item("ItemCatalogDescription")
            row.Item("Artículo") = item("ItemDescription")
            row.Item("Tipo de Equipo") = item("ItemTypeDescription")
            row.Item("Responsable") = item("ResponsibleDescription")
            row.Item("Localización") = item("LocationDescription")
            row.Item("Cuenta del Activo") = item("MainAccountDescription")
            row.Item("Cuenta de la Depreciación") = item("DepreciationMainAccountDescription")
            row.Item("Valor Histórico") = item("HistoricalValue")
            row.Item("Valor Valorización") = item("ValorizationValue")
            row.Item("Valor Desvalorización") = item("DevaluationValue")
            row.Item("Depreciación Acumulada") = item("AcumulatedDepreciation")
            row.Item("Valor Depreciación Mes") = item("DepreciateValue")
            row.Item("Días Depreciados") = item("DepreciatedDays")
            dt.Rows.Add(row)
        Next

        If dt IsNot Nothing Then
            INDgcExportExcel.DataSource = dt
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
        End If
    End Sub

    ''' <summary>
    ''' Metodo que genera el excel
    ''' </summary>
    Private Sub generateExcel()
        Dim _gridView = Me.INDgcExportExcel
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDgcExportExcel.DataSource = Nothing
        Me.INDgcExportExcel.RefreshDataSource()
    End Sub

#End Region

#Region "BarButtons"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

End Class