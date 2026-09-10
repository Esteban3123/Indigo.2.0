#Region "Imports"

Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MaintenanceRepository
Imports Presentation.Base
Imports Presentation.Maintenance.MVP

#End Region

Public Class FrmDashboardMaintenance

#Region "Variables"

    Dim _presenter As PDashboardMaintenance

    Dim _maintenanceResponsible As ViewMaintenanceResponsibleUserXpo

    Dim _itemCatalogFilters As String

#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As Object
        Get
            Return Me.Tag
        End Get
    End Property

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

#End Region

#Region "Datasource"

    Public Property ListRequestsXpo As List(Of ViewListRequestsXpo)
        Get
            Return INDgcRequests.DataSource
        End Get
        Set(value As List(Of ViewListRequestsXpo))
            INDgcRequests.DataSource = value
        End Set
    End Property

    Public Property ListRequestsWithWorkOrderXpo As List(Of ViewListRequestsXpo)
        Get
            Return INDgcRequestsWithWorkOrder.DataSource
        End Get
        Set(value As List(Of ViewListRequestsXpo))
            INDgcRequestsWithWorkOrder.DataSource = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Function LoadMaintenanceResponsible() As Task(Of Integer)
        _maintenanceResponsible = _presenter.GetMaintenanceResponsibleByUserCoder()
        If _maintenanceResponsible Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró un responsable de mantenimiento asociado al usuario"
        ElseIf _maintenanceResponsible.ResponsibleRole = 5 Then
            _maintenanceResponsible = Nothing
            Mensaje(EeventViewerImages.Advertencia) = "El Dashboard de mantenimiento no puede ser operado por un responsable externo"
        ElseIf _maintenanceResponsible.ResponsibleRole = 4 Then
            INDlycgRequests.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        Return Task.FromResult(Of Integer)(0)
    End Function

    Private Function LoadItemCatalog() As Task(Of Integer)
        If _maintenanceResponsible IsNot Nothing Then
            INDSleItemCatalog.Properties.DataSource = _presenter.InitializeMaintenanceResponsibleItemCatalog(_maintenanceResponsible.Id)
        End If
        Return Task.FromResult(Of Integer)(0)
    End Function

    Private Sub LoadRequests()
        If INDgcRequests.DataSource IsNot Nothing OrElse String.IsNullOrEmpty(_itemCatalogFilters) Then
            Exit Sub
        End If
        If _maintenanceResponsible.ResponsibleRole = 4 Then
            Mensaje(EeventViewerImages.Advertencia) = "Un Operario solo puede consultar solicitudes con orden de trabajo asignadas a si mismo"
            Exit Sub
        End If
        INDviewRequests.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of ViewListRequestsXpo) = Nothing
                                  Try
                                      result = _presenter.ListViewListRequests(_itemCatalogFilters)
                                      INDgcRequests.BeginInvoke(Sub()
                                                                    INDgcRequests.DataSource = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
                                                                    INDviewRequests.HideLoadingPanel()
                                                                End Sub)
                                  Catch ex As Exception
                                      INDgcRequests.BeginInvoke(Sub()
                                                                    INDviewRequests.HideLoadingPanel()
                                                                    Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                End Sub)
                                  End Try
                              End Sub)
    End Sub

    Private Sub LoadRequestsWithWorkOrder()
        If INDgcRequestsWithWorkOrder.DataSource IsNot Nothing OrElse String.IsNullOrEmpty(_itemCatalogFilters) Then
            Exit Sub
        End If
        INDviewRequestsWithWorkOrder.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of ViewListRequestsXpo) = Nothing
                                  Try
                                      result = _presenter.ListViewListRequestsWithWorkOrder(_itemCatalogFilters, _maintenanceResponsible)
                                      INDgcRequestsWithWorkOrder.BeginInvoke(Sub()
                                                                                 INDgcRequestsWithWorkOrder.DataSource = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
                                                                                 INDviewRequestsWithWorkOrder.HideLoadingPanel()
                                                                             End Sub)
                                  Catch ex As Exception
                                      INDgcRequestsWithWorkOrder.BeginInvoke(Sub()
                                                                                 INDviewRequestsWithWorkOrder.HideLoadingPanel()
                                                                                 Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                             End Sub)
                                  End Try
                              End Sub)
    End Sub

    Private Sub BeginReloadDatasource()
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                INDgcRequests.DataSource = Nothing
                LoadRequests()
            Case INDlycgRequestsWithWorkOrder.Name 'Solicitudes con Orden de Trabajo
                INDgcRequestsWithWorkOrder.DataSource = Nothing
                LoadRequestsWithWorkOrder()
        End Select
    End Sub

    ''' <summary>
    ''' Método que se ejecuta al retornar el modal de eventos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnModalArgs(sender As Object, e As EventArgs)
        BeginReloadDatasource()
    End Sub

#Region "Multiselect"

    Private Sub VisibleCheck()
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                RequestVisibleCheck()
            Case INDlycgRequestsWithWorkOrder.Name 'Solicitudes con Orden de Trabajo
                RequestsWithWorkOrderVisibleCheck()
        End Select
    End Sub

    Private Sub RequestVisibleCheck()
        If ListRequestsXpo IsNot Nothing AndAlso ListRequestsXpo.Any Then
            If ListRequestsXpo.Where(Function(item) item.SelectOption = True).Count = ListRequestsXpo.Count Then
                Me.INDviewRequests_SelectOption.Image = Global.Presentation.Maintenance.My.Resources.Resources.check
            Else
                Me.INDviewRequests_SelectOption.Image = Global.Presentation.Maintenance.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    Private Sub RequestsWithWorkOrderVisibleCheck()
        If ListRequestsWithWorkOrderXpo IsNot Nothing AndAlso ListRequestsWithWorkOrderXpo.Any Then
            If ListRequestsWithWorkOrderXpo.Where(Function(item) item.SelectOption = True).Count = ListRequestsWithWorkOrderXpo.Count Then
                Me.INDviewRequestsWithWorkOrder_SelectOption.Image = Global.Presentation.Maintenance.My.Resources.Resources.check
            Else
                Me.INDviewRequestsWithWorkOrder_SelectOption.Image = Global.Presentation.Maintenance.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    Private Sub SelectOptions(view As GridView, optionCheck As Integer, Optional selectGroupRow As Boolean = True)
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                If view.IsGroupRow(listHandlesSelected(i)) Then
                    If selectGroupRow Then
                        GetChildsRows(view, listHandlesSelected(i), optionCheck)
                    End If
                Else
                    Dim row = view.GetRow(listHandlesSelected(i))
                    row.SelectOption = optionCheck
                End If
            Next
        End If

        VisibleCheck()
    End Sub

    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, value As Decimal)
        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRows(view, childHandle, value)
            Else
                Dim row As ViewListRequestsXpo = view.GetRow(childHandle)
                row.SelectOption = If(value = 0, False, True)
            End If
        Next
    End Sub

#End Region

    Private Sub OpenFormAssignUser(ListViewRequestsXpo As List(Of ViewListRequestsXpo))
        Using formulario As New FrmAssignResponsible()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 250
            formulario.ToolBar.Visible = False
            formulario.ResponsibleRole = Me._maintenanceResponsible.ResponsibleRole
            formulario.ProcessType = If(INDtcgInformation.SelectedTabPageName = INDlycgRequests.Name, 1, 2)
            formulario.ListViewListRequestsXpo = ListViewRequestsXpo
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Async Sub FrMDashboardMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()

        'Se valida si el usuario tiene permiso para mostrar los check del search de centro de atención
        BarraBotones.ActualizarPermisosBarra(Me.MyTag)

        'Inicializamos la referencia
        _presenter = New PDashboardMaintenance()

        'Cargar el responsable de mantenimiento
        AsyncLoader(True)
        Await Me.LoadMaintenanceResponsible()
        Await Me.LoadItemCatalog()
        AsyncLoader(False)

        'De acuerdo con los permisos se permite o no seleccionar muchos centros de atención
        INDviewItemCatalog.OptionsSelection.MultiSelect = If(BarraBotones.PermissionsForm.ContainsKey(96), True, False)
    End Sub

#End Region

#Region "Shown"

    Private Sub FrMDashboardMaintenance_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDSleItemCatalog.Properties.PopupFormMinSize = New System.Drawing.Size(INDSleItemCatalog.Size.Width - 11, 0)
        INDSleItemCatalog.Properties.PopupFormSize = New System.Drawing.Size(INDSleItemCatalog.Size.Width - 11, 0)

        INDtcgInformation.SelectedTabPageIndex = 0
        INDSleItemCatalog.Focus()
    End Sub

#End Region

#Region "CloseUp"

    Private Sub INDSleItemCatalog_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleItemCatalog.CloseUp
        'Se obtienen los códigos de centro de atención seleccionados
        Dim itemCatalogs = (From x In INDviewItemCatalog.GetSelectedRows() Select DirectCast(INDviewItemCatalog.GetRow(x), ViewMaintenanceResponsibleItemCatalogXpo).Id)
        _itemCatalogFilters = String.Join(",", itemCatalogs)

        'Se valida si seleccionaron items del search
        If String.IsNullOrEmpty(_itemCatalogFilters) Then
            INDSleItemCatalog.Properties.NullText = "Seleccione un Catálogo de Artículo"
        Else
            INDSleItemCatalog.Properties.NullText = itemCatalogs.Count().ToString() + " Items Seleccionados"
        End If

        BeginReloadDatasource()
    End Sub

#End Region

#Region "SelectPageChanged"

    Private Sub INDtcgInformation_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDtcgInformation.SelectedPageChanged
        If String.IsNullOrEmpty(_itemCatalogFilters) Then
            Exit Sub
        End If

        Select Case e.Page.Name
            Case INDlycgRequests.Name 'Solicitudes
                LoadRequests()
            Case INDlycgRequestsWithWorkOrder.Name 'Solicitudes con Orden de Trabajo
                LoadRequestsWithWorkOrder()
        End Select
    End Sub

#End Region

#Region "SelectionChanged"

    Private Sub INDviewRequests_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewRequests.SelectionChanged
        If e.Action = CollectionChangeAction.Remove OrElse e.Action = CollectionChangeAction.Refresh Then
            If ListRequestsXpo IsNot Nothing Then
                For Each viewRequest In ListRequestsXpo.Where(Function(d) d.SelectOption)
                    viewRequest.SelectOption = False
                Next
            End If
        End If
        SelectOptions(INDviewRequests, 1, False)
    End Sub

    Private Sub INDviewRequestsWithWorkOrder_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewRequestsWithWorkOrder.SelectionChanged
        If e.Action = CollectionChangeAction.Remove OrElse e.Action = CollectionChangeAction.Refresh Then
            If ListRequestsWithWorkOrderXpo IsNot Nothing Then
                For Each viewRequest In ListRequestsWithWorkOrderXpo.Where(Function(d) d.SelectOption)
                    viewRequest.SelectOption = False
                Next
            End If
        End If
        SelectOptions(INDviewRequestsWithWorkOrder, 1, False)
    End Sub

#End Region

#Region "MenuContext"

    Private Sub INDview_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDviewRequests.PopupMenuShowing, INDviewRequestsWithWorkOrder.PopupMenuShowing
        INDBbiAsign.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        If e.HitInfo.RowHandle < 0 Then
            Exit Sub
        Else
            Select Case INDtcgInformation.SelectedTabPageName
                Case INDlycgRequests.Name 'Solicitudes
                    PopupMenuRequest()
                Case INDlycgRequestsWithWorkOrder.Name 'Solicitudes con Orden de Trabajo
                    PopupMenuRequestsWithWorkOrder()
            End Select
        End If

        Dim View = CType(sender, GridView)
        INDPopMenuActions2.Manager = BarManager2
        INDPopMenuActions2.ShowPopup(View.GridControl.PointToScreen(e.Point))
    End Sub

    Private Sub PopupMenuRequest()
        Dim quantitySelected As Integer = 0
        Dim assigned As Boolean = False

        If ListRequestsXpo IsNot Nothing AndAlso ListRequestsXpo.Any(Function(d) d.SelectOption) Then
            quantitySelected = ListRequestsXpo.Where(Function(d) d.SelectOption).Count
            assigned = ListRequestsXpo.Any(Function(d) d.SelectOption AndAlso d.AssignUser = indigo.AuditMessageWcf.CodeUser)
        End If

        If {1, 2, 3}.Contains(_maintenanceResponsible.ResponsibleRole) Then 'si tiene permiso de asignar
            INDBbiAsign.Caption = "Asignar"
            INDBbiAsign.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

    Private Sub PopupMenuRequestsWithWorkOrder()
        Dim quantitySelected As Integer = 0
        Dim assigned As Boolean = False

        If ListRequestsWithWorkOrderXpo IsNot Nothing AndAlso ListRequestsWithWorkOrderXpo.Any(Function(d) d.SelectOption) Then
            quantitySelected = ListRequestsWithWorkOrderXpo.Where(Function(d) d.SelectOption).Count
            assigned = ListRequestsWithWorkOrderXpo.Any(Function(d) d.SelectOption AndAlso d.AssignUser = indigo.AuditMessageWcf.CodeUser)
        End If

        If {1, 2, 3}.Contains(_maintenanceResponsible.ResponsibleRole) Then 'si tiene permiso de reasignar
            INDBbiAsign.Caption = "Reasignar"
            INDBbiAsign.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

#End Region

#Region "ItemClick"

    Private Sub INDBarRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarRefresh.ItemClick
        BeginReloadDatasource()
    End Sub

    Private Sub INDBbiReasign_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiAsign.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim ListViewRequestsXpo As List(Of ViewListRequestsXpo) = Nothing
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlycgRequests.Name 'Solicitudes
                ListViewRequestsXpo = ListRequestsXpo.Where(Function(d) d.SelectOption).ToList()
            Case INDlycgRequestsWithWorkOrder.Name 'Solicitudes con Orden de Trabajo
                ListViewRequestsXpo = ListRequestsWithWorkOrderXpo.Where(Function(d) d.SelectOption).ToList()
        End Select

        If ListViewRequestsXpo Is Nothing OrElse ListViewRequestsXpo.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos una solicitud"
            Exit Sub
        End If

        OpenFormAssignUser(ListViewRequestsXpo)
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRepCheckSelectOption.EditValueChanging
        If e IsNot Nothing Then
            Dim row = CType(INDviewRequests.GetFocusedRow(), ViewListRequestsXpo)
            If row IsNot Nothing Then
                row.SelectOption = e.NewValue
                INDgcRequests.RefreshDataSource()
                VisibleCheck()
            End If
        End If
    End Sub

#End Region

#Region "MouseDoubleClick"

    Private Sub INDgcRequests_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcRequests.MouseDoubleClick
        If ListRequestsXpo IsNot Nothing AndAlso ListRequestsXpo.Any() Then
            Dim hitPoint = Me.INDviewRequests.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDviewRequests_SelectOption") Then

                    Dim listFilterXpCollection = INDviewRequests.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDviewRequests_SelectOption.Image = Global.Presentation.Maintenance.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        cont = (From l In listFilterXpCollection Where l.SelectOption = True).Count
                        If cont = ListRequestsXpo.Count Then
                            Me.INDviewRequests_SelectOption.Image = Global.Presentation.Maintenance.My.Resources.Resources.check
                        End If
                    End If
                    Me.INDgcRequests.RefreshDataSource()
                    Me.INDgcRequests.Invalidate()
                End If
            End If
        End If
    End Sub

#End Region

#Region "RowCellClick"

    Private Sub INDviewRequests_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles INDviewRequests.RowCellClick, INDviewRequestsWithWorkOrder.RowCellClick
        Dim view = CType(sender, GridView)
        If e.Column.FieldName = "Alert" Then
            Dim row = CType(view.GetFocusedRow(), ViewListRequestsXpo)
            'OpenFormOpenAlert(row)
        End If
    End Sub

#End Region

#Region "SizeChanged"

    Private Sub INDgcRequests_SizeChanged(sender As Object, e As EventArgs) Handles INDgcRequests.SizeChanged
        If INDlyItemRequests IsNot Nothing AndAlso INDgcRequests IsNot Nothing Then
            Dim width = INDlyItemRequests.Size.Width - INDlyItemRequests.Padding.Width
            If width > INDgcRequests.Size.Width Then
                INDgcRequests.Width = width
            End If
        End If
    End Sub

    Private Sub INDgcRequestsWithWorkOrder_SizeChanged(sender As Object, e As EventArgs) Handles INDgcRequestsWithWorkOrder.SizeChanged
        If INDlyItemRequestsWithWorkOrder IsNot Nothing AndAlso INDgcRequestsWithWorkOrder IsNot Nothing Then
            Dim width = INDlyItemRequestsWithWorkOrder.Size.Width - INDlyItemRequestsWithWorkOrder.Padding.Width
            If width > INDgcRequestsWithWorkOrder.Size.Width Then
                INDgcRequestsWithWorkOrder.Width = width
            End If
        End If
    End Sub

#End Region

#End Region

End Class