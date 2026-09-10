Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MaintenanceRepository
Imports Presentation.Base
Imports Presentation.Maintenance.MVP

Public Class FrmMaintenancePlanDashboard

    Public Sub New()
        InitializeComponent()
    End Sub

#Region "Methods"
    Private lastCriteria As String
    ''' <summary>
    ''' Escribe los filtros en el popupcontainer edit
    ''' </summary>
    ''' <returns></returns>
    Public Function WriteFilters() As String
        'Nombre de Filtro

        Dim strFilter As New List(Of String)()

        Dim filterList As New List(Of String)()
        If INDChkActivo.Checked Then
            filterList.Add($"Activo Fijo: TODOS")
        ElseIf INDSleActivo.EditValue IsNot Nothing Then
            filterList.Add($"Activo Fijo: {INDSleActivo.Text.Trim()}")
            strFilter.Add($"PhysetAssetId={CInt(INDSleActivo.EditValue)}")
        End If
        If INDChkArticulo.Checked Then
            filterList.Add($"Artículo: TODOS")
        ElseIf INDSleArticulo.EditValue IsNot Nothing Then
            filterList.Add($"Artículo: {INDSleArticulo.Text.Trim()}")
            strFilter.Add($"ItemId={CInt(INDSleArticulo.EditValue)}")
        End If
        If INDChkResponsable.Checked Then
            filterList.Add($"Responsable: TODOS")
        ElseIf INDSleResponsable.EditValue IsNot Nothing Then
            filterList.Add($"Responsable: {INDSleResponsable.Text.Trim()}")
            strFilter.Add($"ResponsibleId={CInt(INDSleResponsable.EditValue)}")
        End If
        If INDChkTipoEquipo.Checked Then
            filterList.Add($"Tipo Equipo: TODOS")
        ElseIf INDSleTipoEquipo.EditValue IsNot Nothing Then
            filterList.Add($"Tipo Equipo: {INDSleTipoEquipo.Text.Trim()}")
            strFilter.Add($"ItemTypeId={CInt(INDSleTipoEquipo.EditValue)}")
        End If
        If INDChkTipoInventario.Checked Then
            filterList.Add($"Tipo Inventario: TODOS")
        ElseIf INDSleTipoInventario.EditValue IsNot Nothing Then
            filterList.Add($"Tipo Inventario: {INDSleTipoInventario.Text.Trim()}")
            strFilter.Add($"InventoryTypeId={CInt(INDSleTipoInventario.EditValue)}")
        End If
        If INDChkTipoResponsable.Checked Then
            filterList.Add($"Tipo Responsable: TODOS")
        ElseIf INDSleTipoResponsable.EditValue IsNot Nothing Then
            filterList.Add($"Tipo Responsable: {INDSleTipoResponsable.Text.Trim()}")
            strFilter.Add($"ReponsibleTypeId={CInt(INDSleTipoResponsable.EditValue)}")
        End If
        If INDChkUbicacion.Checked Then
            filterList.Add($"Ubicación: TODOS")
        ElseIf INDtreeLocation.EditValue IsNot Nothing Then
            filterList.Add($"Ubicación: {INDtreeLocation.Text.Trim()}")
            strFilter.Add($"LocationId={CInt(INDtreeLocation.EditValue)}")
        End If
        If INDChkUnidadFuncional.Checked Then
            filterList.Add($"U. Funcional: TODOS")
        ElseIf INDSleUnidadFuncional.EditValue IsNot Nothing Then
            filterList.Add($"U. Funcional: {INDSleUnidadFuncional.Text.Trim()}")
            strFilter.Add($"FunctionalUnitId={CInt(INDSleUnidadFuncional.EditValue)}")
        End If
        If Not filterList.Any() Then
            filterList.Add("TODO")
        End If
        INDPceFilters.Properties.NullText = $"{String.Join(", ", filterList)}"

        Return String.Join(" AND ", strFilter)
    End Function
#End Region

#Region "Handlers"
    ''' <summary>
    ''' Load del frontal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmMaintenancePlanDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()
        INDTcgData.SelectedTabPageIndex = 0
        loadLocation()
        WriteFilters()
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        initActionsGridWithOutPromgramming()
        initActionsGridWithPromgramming()
    End Sub

    ''' <summary>
    ''' Evento shown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmMaintenancePlanDashboard_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        initMoreInfo()
    End Sub

    ''' <summary>
    ''' Enlaza el moreInfo a la rejilla
    ''' </summary>
    Private Sub initMoreInfo()
        IndigoGridView1.MoreInfoColunmns(INDGvProgramacion)
        'IndigoGridControl1.RefreshGrid(INDGcProgramacion)
        INDGcProgramacion.RefreshDataSource()
        ' IndigoGridView1
    End Sub

    ''' <summary>
    ''' Menu de acciones para la rejilla sin programación
    ''' </summary>
    Private Sub initActionsGridWithOutPromgramming()
        IndigoGridView1.SetListAcction(INDGvSinProgramar, {eAcciones.Programming}.ToList(), False)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvSinProgramar.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Carga las ubicaciones de manera asíncrona
    ''' </summary>
    Private Sub loadLocation()
        Task.Factory.StartNew(Sub()
                                  Using model As New Presentation.FixedAsset.MVP.MEquipmentEntry(Me.Tag)
                                      Dim ds As XPCollection(Of FixedAssetRepository.FixedAssetFixedAssetLocationXpo) = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetLocationCollection()
                                      Dim dsl = ds.ToList()
                                      Me.SafeInvoke(Sub()
                                                        INDtreeLocation.Properties.DataSource = dsl
                                                    End Sub)
                                  End Using
                              End Sub)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceFilters_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceFilters.Closed

    End Sub

    ''' <summary>
    ''' Check de ubicacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDChkUbicacion_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkUbicacion.CheckedChanged
        If INDChkUbicacion.Checked Then
            INDtreeLocation.EditValue = Nothing
            INDtreeLocation.Enabled = False
        Else
            INDtreeLocation.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Check de activos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDChkActivo_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkActivo.CheckedChanged
        If INDChkActivo.Checked Then
            INDSleActivo.EditValue = Nothing
            INDSleActivo.Enabled = False
        Else
            INDSleActivo.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Check de articulos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDChkArticulo_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkArticulo.CheckedChanged
        If INDChkArticulo.Checked Then
            INDSleArticulo.EditValue = Nothing
            INDSleArticulo.Enabled = False
        Else
            INDSleArticulo.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Check de responsables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDChkTipoResponsable_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkTipoResponsable.CheckedChanged
        If INDChkTipoResponsable.Checked Then
            INDSleTipoResponsable.EditValue = Nothing
            INDSleTipoResponsable.Enabled = False
        Else
            INDSleTipoResponsable.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Check de tipos de inventario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDChkTipoInventario_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkTipoInventario.CheckedChanged
        If INDChkTipoInventario.Checked Then
            INDSleTipoInventario.EditValue = Nothing
            INDSleTipoInventario.Enabled = False
        Else
            INDSleTipoInventario.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Check de tipo equipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDChkTipoEquipo_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkTipoEquipo.CheckedChanged
        If INDChkTipoEquipo.Checked Then
            INDSleTipoEquipo.EditValue = Nothing
            INDSleTipoEquipo.Enabled = False
        Else
            INDSleTipoEquipo.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Check de responsables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDChkResponsable_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkResponsable.CheckedChanged
        If INDChkResponsable.Checked Then
            INDSleResponsable.EditValue = Nothing
            INDSleResponsable.Enabled = False
        Else
            INDSleResponsable.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Check de unidad funcional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDChkUnidadFuncional_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkUnidadFuncional.CheckedChanged
        If INDChkUnidadFuncional.Checked Then
            INDSleUnidadFuncional.EditValue = Nothing
            INDSleUnidadFuncional.Enabled = False
        Else
            INDSleUnidadFuncional.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Click de aplicar filtros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAplicar_Click(sender As Object, e As EventArgs) Handles INDBtnAplicar.Click
        Dim strFilter = WriteFilters()
        lastCriteria = strFilter
        INDPceFilters.ClosePopup()
        searchData()
    End Sub

    Private Sub searchData()
        If INDPceFilters.Properties.NullText.Equals("") Then
            Exit Sub
        End If
        If INDTcgData.SelectedTabPage.Name.Equals(INDLcgWithProgramming.Name) Then
            SearchPlanMaintenance()
            INDGvProgramacion.ExpandAllGroups()
        Else
            SearchPlanMaintenanceWithOutProgramming()
        End If
    End Sub

    Private Sub initActionsGridWithPromgramming()
        IndigoGridView2.SetListAcction(INDGvProgramacion, {eAcciones.View, eAcciones.Remove}.ToList(), False)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProgramacion.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Carga el datasource de activos sin programación segun el filtro
    ''' </summary>
    Private Sub SearchPlanMaintenanceWithOutProgramming()
        INDGcSinProgramar.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListViewMaintenanceProgrammingCollectionOnlyWithOutProgramming(lastCriteria)
    End Sub

    ''' <summary>
    ''' Busca datasource para activos con programación
    ''' </summary>
    Private Sub SearchPlanMaintenance()
        INDGcProgramacion.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListViewMaintenanceProgrammingCollectionOnlyProgramed(lastCriteria)
    End Sub

    Private Sub INDSleUbicacion_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs)

    End Sub

    ''' <summary>
    ''' carga datasource activos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleActivo_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleActivo.QueryPopUp
        If INDSleActivo.Properties.DataSource Is Nothing Then
            INDSleActivo.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetPhysicalAsset()
        End If
    End Sub

    ''' <summary>
    ''' carga datasource articulos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleArticulo_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleArticulo.QueryPopUp
        If INDSleArticulo.Properties.DataSource Is Nothing Then
            INDSleArticulo.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetEquipmentByStatus(True)
        End If
    End Sub

    ''' <summary>
    ''' carga datasource responsables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleTipoResponsable_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTipoResponsable.QueryPopUp
        If INDSleTipoResponsable.Properties.DataSource Is Nothing Then
            INDSleTipoResponsable.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetResponsibleType()
        End If
    End Sub

    ''' <summary>
    ''' carga datasource tipos de inventario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleTipoInventario_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTipoInventario.QueryPopUp
        If INDSleTipoInventario.Properties.DataSource Is Nothing Then
            INDSleTipoInventario.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetInventoryType()
        End If
    End Sub

    ''' <summary>
    ''' carga datasource tipos equipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleTipoEquipo_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTipoEquipo.QueryPopUp
        If INDSleTipoEquipo.Properties.DataSource Is Nothing Then
            INDSleTipoEquipo.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetItemType()
        End If
    End Sub

    ''' <summary>
    ''' carga datasource responsables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleResponsable_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleResponsable.QueryPopUp
        If INDSleResponsable.Properties.DataSource Is Nothing Then
            INDSleResponsable.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListMaintenanceResponsibleByStatus(True)
        End If
    End Sub

    ''' <summary>
    ''' carga datasource unidades funcionales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleUnidadFuncional_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUnidadFuncional.QueryPopUp
        If INDSleUnidadFuncional.Properties.DataSource Is Nothing Then
            'INDSleUnidadFuncional.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetItemType()
        End If
    End Sub

    ''' <summary>
    ''' boton de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        OpenModalProgramming(False, 1)
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        If sender.Tag.ToString().Equals("View") Then
            OpenModalProgramming(False, 2)
        ElseIf sender.Tag.ToString().Equals("Remove") Then
            removeProgramming(False)
        End If
    End Sub

    ''' <summary>
    ''' cell merge
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvProgramacion_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles INDGvProgramacion.CellMerge

        If INDGvProgramacion.GetRowCellValue(e.RowHandle1, "Plate") = INDGvProgramacion.GetRowCellValue(e.RowHandle2, "Plate") AndAlso e.Column.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True Then
            e.Merge = True
        Else
            e.Merge = False
        End If
        e.Handled = True

    End Sub

    ''' <summary>
    ''' programación masiva
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiMassive_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiMassive.ItemClick
        MassiveProgramming()
    End Sub

    Private Sub INDBbiRemoveProgramacion_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiRemoveProgramacion.ItemClick
        RemoveMassiveProgramming()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Programar masivamente
    ''' </summary>
    Private Sub MassiveProgramming()
        If INDGvSinProgramar.SelectedRowsCount = 0 Then
            ShowMessage(Domain.Base.Entities.eStatusResult.WARNING) = "Debe seleccionar mínimo un elemento"
            Return
        End If
        OpenModalProgramming(True, 0)
    End Sub

    Public Sub RemoveMassiveProgramming()
        If INDGvProgramacion.SelectedRowsCount = 0 Then
            ShowMessage(Domain.Base.Entities.eStatusResult.WARNING) = "Debe seleccionar mínimo un elemento"
            Return
        End If
        removeProgramming(True)
    End Sub

    Private Async Sub removeProgramming(isMassive As Boolean)
        If MessageIndigo.Show("Está seguro que desea eliminar la programación", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

            Dim programmingIds As New List(Of Integer)()
            If isMassive Then
                For Each rowHandle In INDGvProgramacion.GetSelectedRows()
                    Dim obj = CType(INDGvProgramacion.GetRow(rowHandle), Maintenance_ViewMaintenanceProgramming)
                    programmingIds.Add(CInt(obj.ProgramatedDateId))
                Next
            Else
                programmingIds = {CInt(CType(INDGvProgramacion.GetFocusedRow(), Maintenance_ViewMaintenanceProgramming).ProgramatedDateId)}.ToList()
            End If
            Try
                INDGvProgramacion.ShowLoadingPanel()
                Using model As New MMaintenancePlanAndMetrology()
                    Dim res = Await model.DeleteMaintenancePlanAndMetrologyAsync(programmingIds)
                    If res.StateResult Then
                        ShowMessage(eStatusResult.SUCCESS) = "Programación eliminada con éxito!!"
                        searchData()
                    Else
                        ShowMessage(eStatusResult.WARNING) = res.Message
                    End If
                    INDGvProgramacion.HideLoadingPanel()
                End Using
            Catch ex As Exception
                INDGvProgramacion.HideLoadingPanel()
            End Try
        End If
    End Sub

    Public Sub OpenModalProgramming(isMassive As Boolean, moduleFrom As Byte)
        Dim objGv As Maintenance_ViewMaintenanceProgramming
        Using frm As New FrmPrograming()
            frm.IsMassive = isMassive
            frm.PermissionsForm = Me.BarraBotones.PermissionsForm
            If isMassive Then
                Dim isMetrology As Boolean
                Dim selectedRows As Integer() = INDGvSinProgramar.GetSelectedRows()
                Dim selectedPhysicalAsset As New List(Of Tuple(Of Integer, String))()
                For Each rowHandle In selectedRows
                    Dim obj = CType(INDGvSinProgramar.GetRow(rowHandle), Maintenance_ViewMaintenanceProgramming)
                    selectedPhysicalAsset.Add(New Tuple(Of Integer, String)(obj.PhysetAssetId, obj.PlateArticleFullname))
                    If Not isMetrology Then
                        isMetrology = {1, 2, 3}.Contains(obj.InventoryType)
                    End If
                Next
                frm.FixedAssetPhysicalIdList = selectedPhysicalAsset
                frm.IsMetrology = isMetrology
            Else
                Select Case moduleFrom
                    Case 1
                        objGv = INDGvSinProgramar.GetFocusedRow()
                    Case 2
                        objGv = INDGvProgramacion.GetFocusedRow()

                End Select
                If objGv Is Nothing Then
                    frm.Dispose()
                    Return
                End If
                frm.FixedAssetPhysicalIdList = {New Tuple(Of Integer, String)(objGv.PhysetAssetId, objGv.PlateArticleFullname)}.ToList()
                frm.ProgramedId = objGv.ProgramingId
                frm.IsMetrology = {1, 2, 3}.Contains(objGv.InventoryType)
            End If
            AddHandler frm.ProgramingSaved, AddressOf SearchPlanMaintenanceWithOutProgramming
            Dim frmTransparent As New FrmTransparent(frm, False)
            frmTransparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub INDTcgData_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDTcgData.SelectedPageChanged
        searchData()
    End Sub

    Private Sub PopupMenu1_BeforePopup(sender As Object, e As ComponentModel.CancelEventArgs) Handles PopupMenu1.BeforePopup
        If INDTcgData.SelectedTabPage.Name.Equals(INDLcgWithProgramming.Name) Then
            INDBbiMassive.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            INDBbiRemoveProgramacion.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            INDBbiMassive.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDBbiRemoveProgramacion.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
    End Sub
#End Region

End Class