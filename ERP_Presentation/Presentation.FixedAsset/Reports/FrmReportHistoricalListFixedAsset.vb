#Region "Imports"

Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportHistoricalListFixedAsset

#Region "Datasource"

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla del tipo de reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _FillingAdquisitionType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingAdquisitionType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingAdquisitionType Is Nothing Then
                _FillingAdquisitionType = New List(Of Tuple(Of Integer, String))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(0, "Todos"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(1, "Compra Directa"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(3, "Comodato"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(8, "Comodato Tercerizado"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(4, "Donación"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(5, "Traspaso de Bienes"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(6, "Otro Concepto"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(7, "Leasing Financiero"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(9, "Renting Financiero"))
                _FillingAdquisitionType.Add(New Tuple(Of Integer, String)(10, "Renting Operativo"))
            End If
            Return _FillingAdquisitionType
        End Get
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
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        Dim errors As New StringBuilder

        If INDSleResponsible.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un responsable")
        End If

        If INDGleAdquisitionType.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un tipo adquisición")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Validations = False
        End If

        Return Validations
    End Function

#Region "Selector"

    Private _selectorItem As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorEquipmentType As SelectorCache = New SelectorCache("Id", "Code")
    Private _selectorPlate As SelectorCache = New SelectorCache("Id", "Plate")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvItem.CustomUnboundColumnData, INDGvEquipmentType.CustomUnboundColumnData, INDGvPlate.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvItem" Then
                e.Value = _selectorItem.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvEquipmentType" Then
                e.Value = _selectorEquipmentType.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvPlate" Then
                e.Value = _selectorPlate.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvItem.RowCellClick, INDGvEquipmentType.RowCellClick, INDGvPlate.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvItem" Then
                selector = _selectorItem
            ElseIf view.Name = "INDGvEquipmentType" Then
                selector = _selectorEquipmentType
            ElseIf view.Name = "INDGvPlate" Then
                selector = _selectorPlate
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

    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleItem.Closed, INDSleEquipmentType.Closed, INDSlePlate.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleItem" Then
            searchLookupEdit.Properties.NullText = _selectorItem.ToString()
        ElseIf searchLookupEdit.Name = "INDSleEquipmentType" Then
            searchLookupEdit.Properties.NullText = _selectorEquipmentType.ToString()
        ElseIf searchLookupEdit.Name = "INDSlePlate" Then
            searchLookupEdit.Properties.NullText = _selectorPlate.ToString()
        End If
    End Sub

#End Region

#Region "ToExcel"

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasource()
        Dim filtroConsulta As String = Nothing

        'filtro por responsable
        If INDSleResponsible.EditValue IsNot Nothing Then
            filtroConsulta = filtroConsulta & If(String.IsNullOrEmpty(filtroConsulta), "", " AND ") & " PhysicalAssetId.ResponsibleId.Id = " & INDSleResponsible.EditValue
        End If

        'filtro por Catalogo
        If INDSleClassification.EditValue IsNot Nothing Then
            filtroConsulta = filtroConsulta & If(String.IsNullOrEmpty(filtroConsulta), "", " AND ") & " PhysicalAssetId.ItemId.ItemCatalogId.Id = " & INDSleClassification.EditValue
        End If

        'filtro por tipo de adquisición
        If INDGleAdquisitionType.EditValue <> 0 Then
            filtroConsulta = filtroConsulta & If(String.IsNullOrEmpty(filtroConsulta), "", " AND ") & " PhysicalAssetId.AdquisitionType = " & INDGleAdquisitionType.EditValue
        End If

        'filtro por articulo
        If String.IsNullOrEmpty(_selectorItem.GetKeys()) = False Then
            filtroConsulta = filtroConsulta & If(String.IsNullOrEmpty(filtroConsulta), "", " AND ") & " PhysicalAssetId.ItemId.Id IN (" & _selectorItem.GetKeys() & ")"
        End If

        'filtro por Tipo de Equipo
        If String.IsNullOrEmpty(_selectorEquipmentType.GetKeys()) = False Then
            filtroConsulta = filtroConsulta & If(String.IsNullOrEmpty(filtroConsulta), "", " AND ") & " PhysicalAssetId.ItemId.ItemTypeId.Id IN (" & _selectorEquipmentType.GetKeys() & ")"
        End If

        'filtro por Placa
        If String.IsNullOrEmpty(_selectorPlate.GetKeys()) = False Then
            filtroConsulta = filtroConsulta & If(String.IsNullOrEmpty(filtroConsulta), "", " AND ") & " PhysicalAssetId.Id IN (" & _selectorPlate.GetKeys() & ")"
        End If

        Dim dt As DataTable = Nothing
        Dim List = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetTransferDetailReportXpo)(Nothing, filtroConsulta)

        If List IsNot Nothing AndAlso List.Count > 0 Then
            dt = New DataTable
            dt.Columns.Add("Placa")
            dt.Columns.Add("Responsable Actual")
            dt.Columns.Add("Artículo")
            dt.Columns.Add("Catálogo Bienes y Servicios")
            dt.Columns.Add("Serie")
            dt.Columns.Add("Marca")
            dt.Columns.Add("Modelo")
            dt.Columns.Add("Tipo de Adquisición")
            dt.Columns.Add("Fecha Traslado")
            dt.Columns.Add("Localización Origen")
            dt.Columns.Add("Responsable Origen")
            dt.Columns.Add("Localización Destino")
            dt.Columns.Add("Responsable Destino")
            dt.Columns.Add("Valor", GetType(Decimal))

            For Each itemView In List
                Dim row As DataRow = dt.NewRow()
                row.Item("Placa") = itemView.PhysicalAssetId.Plate
                row.Item("Responsable Actual") = itemView.PhysicalAssetId.ResponsibleId.ThirdPartyId.NitName
                row.Item("Artículo") = itemView.PhysicalAssetId.ItemId.Code & " - " & itemView.PhysicalAssetId.ItemId.Description
                row.Item("Catálogo Bienes y Servicios") = itemView.PhysicalAssetId.ItemId.CatalogOfPropertyandServicesId?.CodeDescription
                row.Item("Serie") = itemView.PhysicalAssetId.Serie
                row.Item("Marca") = itemView.PhysicalAssetId.TrademarkId.Code & " - " & itemView.PhysicalAssetId.TrademarkId.Name
                row.Item("Modelo") = itemView.PhysicalAssetId.Model
                row.Item("Tipo de Adquisición") = itemView.PhysicalAssetId.AdquisitionTypeName
                row.Item("Fecha Traslado") = itemView.FixedAssetTransferId.DocumentDate
                row.Item("Localización Origen") = If(itemView.FixedAssetTransferId.SourceLocationId Is Nothing, "", itemView.FixedAssetTransferId.SourceLocationId.Code & " - " & itemView.FixedAssetTransferId.SourceLocationId.Name)
                row.Item("Responsable Origen") = If(itemView.FixedAssetTransferId.SourceResponsibleId Is Nothing, "", itemView.FixedAssetTransferId.SourceResponsibleId.ThirdPartyId.NitName)
                row.Item("Localización Destino") = If(itemView.FixedAssetTransferId.TargetLocationId Is Nothing, "", itemView.FixedAssetTransferId.TargetLocationId.Code & " - " & itemView.FixedAssetTransferId.TargetLocationId.Name)
                row.Item("Responsable Destino") = If(itemView.FixedAssetTransferId.TargetResponsibleId Is Nothing, "", itemView.FixedAssetTransferId.TargetResponsibleId.ThirdPartyId.NitName)
                row.Item("Valor") = itemView.PhysicalAssetId.HistoricalValue

                dt.Rows.Add(row)
            Next
        End If

        INDGcExportExcel.DataSource = dt
    End Sub

    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
        _gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
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

#Region "Events"

#Region "Load"

    Private Sub FrmReportHistoricalListFixedAsset_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Cargar GridLookUpEdit        
        INDGleAdquisitionType.Properties.DataSource = FillingAdquisitionType

        'Dar un valores por defecto
        INDGleAdquisitionType.EditValue = 0
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingAdquisitionType = Nothing
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' cargamos el datasource de Responsable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleResponsible_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleResponsible.QueryPopUp
        If INDSleResponsible.Properties.DataSource Is Nothing Then
            Using msearch As New MBusqueda
                INDSleResponsible.Properties.DataSource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetResponsible)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de Clasificación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleClassification_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleClassification.QueryPopUp
        If INDSleClassification.Properties.DataSource Is Nothing Then
            Using msearch As New MBusqueda
                INDSleClassification.Properties.DataSource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentCatalog)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de articulo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleItem_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleItem.QueryPopUp
        If INDSleItem.Properties.DataSource Is Nothing Then
            Using msearch As New MBusqueda
                INDSleItem.Properties.DataSource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipment)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo equipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleEquipmentType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEquipmentType.QueryPopUp
        If INDSleEquipmentType.Properties.DataSource Is Nothing Then
            Using msearch As New MBusqueda
                INDSleEquipmentType.Properties.DataSource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipmentType)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de placa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlePlate_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePlate.QueryPopUp
        If INDSlePlate.Properties.DataSource Is Nothing Then
            Using msearch As New MBusqueda
                INDSlePlate.Properties.DataSource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset)
            End Using
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
    Private Async Sub INDSbGenerateReport_ClickAsync(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptHistoricalListFixedAsset
            reporte.ParametrosReporte = New Object() {INDSleResponsible.EditValue,
                                                  INDSleClassification.EditValue,
                                                  INDGleAdquisitionType.EditValue,
                                                  _selectorItem.GetKeys(),
                                                  _selectorEquipmentType.GetKeys(),
                                                  _selectorPlate.GetKeys()}

            INDDvViewReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing AndAlso reporte.DataSource.Count > 0 Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDSleResponsible.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
        Me.INDSleResponsible.Focus()
    End Sub

    Private Async Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        If ValidateControlsReports() Then
            Try
                AsyncLoader(True)

                Await Task.Factory.StartNew(Sub()
                                                chargueDatasource()
                                            End Sub)

                If Me.INDGcExportExcel.DataSource IsNot Nothing Then
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