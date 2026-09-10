#Region "Imports"

Imports Presentation.Reporter
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Common.MVP
Imports Presentation.FixedAsset.MVP
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Text

#End Region

Public Class FrmReportResponsibleForFixedAssets
    Implements IReportResponsibleForFixedAssets

#Region "Fields"

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PReportResponsibleForFixedAssets

#End Region

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private criterias As Dictionary(Of String, String)
    Private filters As Dictionary(Of String, String)

    Private ListResponsibleId As New List(Of Integer)()
    Private ResponsibleIds As String

    Private ListItemCatalogId As New List(Of Integer)()
    Private ItemCatalogIds As String

    Private ListItemId As New List(Of Integer)()
    Private ItemIds As String

    Private ListItemTypeId As New List(Of Integer)()
    Private ItemTypeIds As String

    Private ListLocationId As New List(Of Integer)()
    Private LocationIds As String

    Private ListPhysicalAssetId As New List(Of Integer)()
    Private PhysicalAssetIds As String

    Private Property TypeReport As Integer
        Get
            Return INDsleTypeReport.EditValue
        End Get
        Set(value As Integer)
            INDsleTypeReport.EditValue = value
        End Set
    End Property

#End Region

#Region "XPO"

    Public Property ResponsibleXpo As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetResponsibleReportXpo) Implements IReportResponsibleForFixedAssets.ResponsibleXpo
        Get
            Return INDSleResponsible.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetResponsibleReportXpo))
            INDSleResponsible.Properties.DataSource = value
        End Set
    End Property

    Public Property ItemCatalogXpo As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetItemCatalogReportXpo) Implements IReportResponsibleForFixedAssets.ItemCatalogXpo
        Get
            Return INDSleItemCatalog.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetItemCatalogReportXpo))
            INDSleItemCatalog.Properties.DataSource = value
        End Set
    End Property

    Public Property ItemXpo As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetItemReportXpo) Implements IReportResponsibleForFixedAssets.ItemXpo
        Get
            Return INDSleItem.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetItemReportXpo))
            INDSleItem.Properties.DataSource = value
        End Set
    End Property

    Public Property ItemTypeXpo As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetItemTypeReportXpo) Implements IReportResponsibleForFixedAssets.ItemTypeXpo
        Get
            Return INDSleItemType.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetItemTypeReportXpo))
            INDSleItemType.Properties.DataSource = value
        End Set
    End Property

    Public Property LocationXpo As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetLocationReportXpo) Implements IReportResponsibleForFixedAssets.LocationXpo
        Get
            Return INDSleLocation.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetLocationReportXpo))
            INDSleLocation.Properties.DataSource = value
        End Set
    End Property

    Public Property PhysicalAssetXpo As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetPhysicalAssetReportXpo) Implements IReportResponsibleForFixedAssets.PhysicalAssetXpo
        Get
            Return INDSlePhysicalAsset.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetPhysicalAssetReportXpo))
            INDSlePhysicalAsset.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' se ejecuta al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportResponsibleForFixedAssets_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia
        Presenter = New PReportResponsibleForFixedAssets(Me)
        InitializeTuples()
        CleanControls()
    End Sub

    Private Sub CleanControls()
        INDLciDateDocument.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciDateCuteOff.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLyGenerateExcellResponsability.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciSbGenerateReportResponsability.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing

        ListResponsibleId = Nothing
        ResponsibleIds = Nothing
        ListItemCatalogId = Nothing
        ItemCatalogIds = Nothing
        ListItemId = Nothing
        ItemIds = Nothing
        ListItemTypeId = Nothing
        ItemTypeIds = Nothing
        ListLocationId = Nothing
        LocationIds = Nothing
        ListPhysicalAssetId = Nothing
        PhysicalAssetIds = Nothing
    End Sub

    Private Sub InitializeTuples()
        Dim ListTypeReport = New List(Of Tuple(Of Integer, String))
        ListTypeReport.Add(New Tuple(Of Integer, String)(1, "General"))
        ListTypeReport.Add(New Tuple(Of Integer, String)(2, "Certificado de Responsabilidad"))
        INDsleTypeReport.Properties.DataSource = ListTypeReport.ToList
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleResponsible
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleResponsible_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleResponsible.QueryPopUp
        If INDSleResponsible.Properties.DataSource Is Nothing Then
            Presenter.ListCollectionResponsible()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleItemCatalog
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItemCatalog_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleItemCatalog.QueryPopUp
        If INDSleItemCatalog.Properties.DataSource Is Nothing Then
            Presenter.ListCollectionItemCatalog()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleItem
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItem_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleItem.QueryPopUp
        If INDSleItem.Properties.DataSource Is Nothing Then
            Presenter.ListCollectionItem()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleItemType
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItemType_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleItemType.QueryPopUp
        If INDSleItemType.Properties.DataSource Is Nothing Then
            Presenter.ListCollectionItemType()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleLocation
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleLocation_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleLocation.QueryPopUp
        If INDSleLocation.Properties.DataSource Is Nothing Then
            Presenter.ListCollectionLocation()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSlePhysicalAsset
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePhysicalAsset_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSlePhysicalAsset.QueryPopUp
        If INDSlePhysicalAsset.Properties.DataSource Is Nothing Then
            Presenter.ListCollectionPhysicalAsset()
        End If
    End Sub

#End Region

#Region "MouseDown"

    ''' <summary>
    ''' Evento que se dispara al checkear los items de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgv_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDgvResponsible.MouseDown, INDgvItemCatalog.MouseDown, INDgvItem.MouseDown, INDgvItemType.MouseDown, INDgvLocation.MouseDown, INDgvPhysicalAsset.MouseDown
        Dim view As GridView = TryCast(sender, GridView)
        Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)
        If hi.Column IsNot Nothing Then
            If hi.Column.FieldName = "DX$CheckboxSelectorColumn" Then
                If view.Name = "INDgvResponsible" Then
                    UpdateList(view, hi, ListResponsibleId)
                ElseIf view.Name = "INDgvItemCatalog" Then
                    UpdateList(view, hi, ListItemCatalogId)
                ElseIf view.Name = "INDgvItem" Then
                    UpdateList(view, hi, ListItemId)
                ElseIf view.Name = "INDgvItemType" Then
                    UpdateList(view, hi, ListItemTypeId)
                ElseIf view.Name = "INDgvLocation" Then
                    UpdateList(view, hi, ListLocationId)
                ElseIf view.Name = "INDgvPhysicalAsset" Then
                    UpdateList(view, hi, ListPhysicalAssetId)
                End If
            End If
        End If
    End Sub

#End Region

#Region "ColumnFilterChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del filtro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvCustomer_ColumnFilterChanged(sender As Object, e As EventArgs) Handles INDgvResponsible.ColumnFilterChanged, INDgvItemCatalog.ColumnFilterChanged, INDgvItem.ColumnFilterChanged, INDgvItemType.ColumnFilterChanged, INDgvLocation.ColumnFilterChanged, INDgvPhysicalAsset.ColumnFilterChanged
        Dim view As GridView = TryCast(sender, GridView)
        If view.Name = "INDgvResponsible" Then
            RestoreSelection(view, ListResponsibleId)
        ElseIf view.Name = "INDgvItemCatalog" Then
            RestoreSelection(view, ListItemCatalogId)
        ElseIf view.Name = "INDgvItem" Then
            RestoreSelection(view, ListItemId)
        ElseIf view.Name = "INDgvItemType" Then
            RestoreSelection(view, ListItemTypeId)
        ElseIf view.Name = "INDgvLocation" Then
            RestoreSelection(view, ListLocationId)
        ElseIf view.Name = "INDgvPhysicalAsset" Then
            RestoreSelection(view, ListPhysicalAssetId)
        End If
    End Sub

#End Region

#Region "CloseUp"

    Private Sub INDSleResponsible_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleResponsible.CloseUp
        ResponsibleIds = RecuperarSeleccionados(sender, "Id", "Code")
    End Sub

    Private Sub INDSleItemCatalog_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleItemCatalog.CloseUp
        ItemCatalogIds = RecuperarSeleccionados(sender, "Id", "Code")
    End Sub

    Private Sub INDSleItem_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleItem.CloseUp
        ItemIds = RecuperarSeleccionados(sender, "Id", "Code")
    End Sub

    Private Sub INDSleItemType_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleItemType.CloseUp
        ItemTypeIds = RecuperarSeleccionados(sender, "Id", "Code")
    End Sub

    Private Sub INDSleLocation_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleLocation.CloseUp
        LocationIds = RecuperarSeleccionados(sender, "Id", "Code")
    End Sub

    Private Sub INDSlePhysicalAsset_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSlePhysicalAsset.CloseUp
        PhysicalAssetIds = RecuperarSeleccionados(sender, "Id", "Plate")
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte de tipo "General"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            AsyncLoader(True)

            Dim reporte As New rptReportResponsibleForFixedAssets
            reporte.ParametrosReporte = New Object() {criterias, filters}
            INDDvDocumentViewer.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDSleResponsible.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte de tipo "Certificado de responsabilidad"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReportCertifiedResponsability_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReportResponsability.Click
        If Me.ValidateControlsReportsCertifiedResponsability Then
            AsyncLoader(True)

            Dim reporte As New rptReportCertifiedResponsibleForFixedAssets
            reporte.ParametrosReporte = New Object() {criterias, filters}
            INDDvDocumentViewer.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcDocumentViewer.Visible = True
                INDDvDocumentViewer.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDSleResponsible.Focus()
            End If
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
    ''' se ejecuta para exportar a excel el reporte de tipo "General"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports Then
            Try
                AsyncLoader(True)
                Using model As New MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportResponsibleForFixedAssets(criterias, filters)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportResponsibleForFixedAssets As DataTable = ds.Tables("ReportResponsibleForFixedAssets")

                        Await Task.Factory.StartNew(Sub()
                                                        chargueDatasource(dtReportResponsibleForFixedAssets, 1)
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

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateResponsabilityExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcellResponsability.Click
        If Me.ValidateControlsReportsCertifiedResponsability Then
            Try
                AsyncLoader(True)
                Using model As New MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetReportResponsibleForFixedAssets(criterias, filters)
                    If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Dim dtReportResponsibleForFixedAssets As DataTable = ds.Tables("ReportResponsibleForFixedAssets")

                        Await Task.Factory.StartNew(Sub()
                                                        chargueDatasource(dtReportResponsibleForFixedAssets, 2)
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

#Region "EditValueChanged"

    Private Sub INDsleTypeReport_EditavalueChanged(sender As Object, e As EventArgs) Handles INDsleTypeReport.EditValueChanged

        If TypeReport = 1 Then 'General
            INDLcgFilterOptional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciItemCatalog.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciItemType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciSbGenerateReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            INDLciDateDocument.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciDateCuteOff.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLyGenerateExcellResponsability.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciSbGenerateReportResponsability.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never


        Else 'Certificado de Responsabilidad
            INDLcgFilterOptional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciItemCatalog.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciItemType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            INDLciDateCuteOff.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciDateDocument.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLyGenerateExcellResponsability.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciSbGenerateReportResponsability.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

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

    Private Sub UpdateList(view As GridView, hi As GridHitInfo, ListId As List(Of Integer))
        If Not hi.InRow Then
            Dim allSelected As Boolean = view.DataController.Selection.Count = view.DataRowCount
            If Not allSelected Then
                For i As Integer = 0 To view.RowCount - 1
                    Dim sourceHandle As Integer = view.GetDataSourceRowIndex(i)
                    If Not ListId.Contains(sourceHandle) Then
                        ListId.Add(sourceHandle)
                    End If
                Next i
            Else
                ListId.Clear()
            End If
        Else
            Dim sourceHandle As Integer = view.GetDataSourceRowIndex(hi.RowHandle)
            If Not ListId.Contains(sourceHandle) Then
                ListId.Add(sourceHandle)
            Else
                ListId.Remove(sourceHandle)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que reestablece el check de los items de la rejilla
    ''' </summary>
    ''' <param name="view"></param>
    Private Sub RestoreSelection(ByVal view As GridView, ListId As List(Of Integer))
        BeginInvoke(New Action(Sub()
                                   Dim i As Integer = 0
                                   Do While i < ListId.Count
                                       view.SelectRow(view.GetRowHandle(ListId(i)))
                                       i += 1
                                   Loop
                               End Sub))
    End Sub

    Private Function RecuperarSeleccionados(sender As Object, keyField As String, descripcionField As String) As String
        Dim edit As DevExpress.XtraEditors.SearchLookUpEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim identificadores As String = String.Empty
        Dim identificador As String = String.Empty
        Dim descripciones As String = String.Empty
        Dim separador As String = String.Empty
        Dim ListId As Integer() = edit.Properties.View.GetSelectedRows()
        For Each selectionRow As Integer In ListId
            identificador = edit.Properties.View.GetRowCellValue(selectionRow, keyField).ToString()
            If Not String.IsNullOrEmpty(identificador) Then
                identificadores += separador & identificador
                descripciones += separador & edit.Properties.View.GetRowCellValue(selectionRow, descripcionField).ToString()
                separador = ", "
            End If
        Next
        edit.Properties.NullText = descripciones.ToString()
        edit.ToolTip = descripciones.ToString()
        Return identificadores.ToString()
    End Function

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario cuando es de tipo "General"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports() As Boolean
        Dim errors As New StringBuilder

        If String.IsNullOrEmpty(Me.INDchkAdquisitionType.EditValue) Then
            errors.AppendLine("Debe seleccionar al menos un tipo de adquisición")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("Responsibles", ResponsibleIds)
        criterias.Add("ItemCatalogs", ItemCatalogIds)
        criterias.Add("Items", ItemIds)
        criterias.Add("ItemTypes", ItemTypeIds)

        filters = New Dictionary(Of String, String)
        filters.Add("Locations", LocationIds)
        filters.Add("PhysicalAssets", PhysicalAssetIds)
        filters.Add("AdquisitionType", INDchkAdquisitionType.EditValue)

        Return True
    End Function

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario cuando es de tipo "Certificado de responsabilidad"
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReportsCertifiedResponsability() As Boolean
        Dim errors As New StringBuilder

        If INDgvResponsible.GetSelectedRows().Count > 1 Then
            errors.AppendLine("Para generar el certificado de responsabilidad solo se puede seleccionar un responsable")
        End If

        If ResponsibleIds Is Nothing OrElse ResponsibleIds = String.Empty Then
            errors.AppendLine("Debe seleccionar un responsable")
        End If

        If INDDeDateDocument.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar fecha del documento")
        End If

        If INDDeDateCutOff.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar fecha de corte")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If

        criterias = New Dictionary(Of String, String)
        criterias.Add("Responsibles", ResponsibleIds)

        filters = New Dictionary(Of String, String)
        filters.Add("DocumentDate", INDDeDateDocument.EditValue)
        filters.Add("CutOffDate", INDDeDateCutOff.EditValue)
        Return True
    End Function

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub chargueDatasource(ByVal dtReportResponsibleForFixedAssets As DataTable, ByVal ReportType As Byte)
        Dim dt As New DataTable

        If ReportType = 1 Then 'General
            dt.Columns.Add("Nit Tercero")
            dt.Columns.Add("Nombre Tercero")
            dt.Columns.Add("Código Ubicación")
            dt.Columns.Add("Nombre Ubicación")
            dt.Columns.Add("Código Artículo")
            dt.Columns.Add("Nombre Artículo")
            dt.Columns.Add("Placa")
            dt.Columns.Add("Serie")
            dt.Columns.Add("Modelo")
            dt.Columns.Add("Marca")
            dt.Columns.Add("Fecha Adquisición", GetType(DateTime))
            dt.Columns.Add("Valor Histórico", GetType(Decimal))

            For Each item In dtReportResponsibleForFixedAssets.Rows
                Dim row As DataRow = dt.NewRow()
                row.Item("Nit Tercero") = item("ThirdPartyNit")
                row.Item("Nombre Tercero") = item("ThirdPartyName")
                row.Item("Código Ubicación") = item("LocationCode")
                row.Item("Nombre Ubicación") = item("LocationName")
                row.Item("Código Artículo") = item("ItemCode")
                row.Item("Nombre Artículo") = item("ItemDescription")
                row.Item("Placa") = item("Plate")
                row.Item("Serie") = item("Serie")
                row.Item("Modelo") = item("Model")
                row.Item("Marca") = item("TrademarkName")
                row.Item("Fecha Adquisición") = CDate(item("AdquisitionDate")).AsDate
                row.Item("Valor Histórico") = item("HistoricalValue")
                dt.Rows.Add(row)
            Next

        Else 'Certificado de Responsabilidad

            dt.Columns.Add("Nit Tercero")
            dt.Columns.Add("Placa")
            dt.Columns.Add("Código Artículo")
            dt.Columns.Add("Nombre Artículo")
            dt.Columns.Add("Catalogo")
            dt.Columns.Add("Nombre Ubicación")
            dt.Columns.Add("Marca")
            dt.Columns.Add("Serie")
            dt.Columns.Add("Fecha Adquisición", GetType(DateTime))
            dt.Columns.Add("Estado")

            For Each item In dtReportResponsibleForFixedAssets.Rows
                Dim row As DataRow = dt.NewRow()
                row.Item("Nit Tercero") = item("ThirdPartyNit")
                row.Item("Placa") = item("Plate")
                row.Item("Código Artículo") = item("ItemCode")
                row.Item("Nombre Artículo") = item("ItemDescription")
                row.Item("Catalogo") = item("ItemCatalogDescription")
                row.Item("Nombre Ubicación") = item("LocationName")
                row.Item("Marca") = item("TrademarkName")
                row.Item("Serie") = item("Serie")
                row.Item("Fecha Adquisición") = CDate(item("AdquisitionDate")).AsDate
                row.Item("Estado") = item("Status")
                dt.Rows.Add(row)
            Next
        End If

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

End Class