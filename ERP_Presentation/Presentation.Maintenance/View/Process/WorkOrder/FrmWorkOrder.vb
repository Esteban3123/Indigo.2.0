#Region "Imports"

Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraScheduler
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MaintenanceRepository
Imports Presentation.Base
Imports Presentation.Maintenance.MVP

#End Region

Public Class FrmWorkOrder

#Region "Variables"

    Dim _presenter As PWorkOrder

    Dim _maintenanceResponsible As ViewMaintenanceResponsibleUserXpo

    Dim _strFilters As String

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

    Public Property ListScheduledMaintenanceXpo As List(Of ViewWorkOrderScheduledMaintenanceXpo)
        Get
            Return INDGcScheduledMaintenance.DataSource
        End Get
        Set(value As List(Of ViewWorkOrderScheduledMaintenanceXpo))
            INDGcScheduledMaintenance.DataSource = value
        End Set
    End Property

    Public Property ListUnscheduledMaintenanceXpo As List(Of ViewWorkOrderUnScheduledMaintenanceXpo)
        Get
            Return INDGcUnscheduledMaintenance.DataSource
        End Get
        Set(value As List(Of ViewWorkOrderUnScheduledMaintenanceXpo))
            INDGcUnscheduledMaintenance.DataSource = value
        End Set
    End Property

    Public Property ListMaintenanceToBeEvaluatedXpo As List(Of ViewWorkOrderXpo)
        Get
            Return INDGcMaintenanceToBeEvaluated.DataSource
        End Get
        Set(value As List(Of ViewWorkOrderXpo))
            INDGcMaintenanceToBeEvaluated.DataSource = value
        End Set
    End Property

#End Region

#Region "Methods"

    Private Function LoadMaintenanceResponsible() As Task(Of Integer)
        _maintenanceResponsible = _presenter.GetMaintenanceResponsibleByUserCoder()
        If _maintenanceResponsible Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró un responsable de mantenimiento asociado al usuario"
        ElseIf _maintenanceResponsible.ResponsibleRole = 5 Then
            _maintenanceResponsible = Nothing
            Mensaje(EeventViewerImages.Advertencia) = "El Dashboard de mantenimiento no puede ser operado por un responsable externo"
            Exit Function
        End If
        If _maintenanceResponsible Is Nothing Then
            INDLcgScheduledMaintenance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgUnscheduledMaintenance.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        Return Task.FromResult(Of Integer)(0)
    End Function

    Public Function WriteFilters(selectedPage As String) As String
        Dim strFilter As New List(Of String)()
        Dim filterList As New List(Of String)()

        If INDChkActivo.Checked Then
            filterList.Add($"Activo Fijo: TODOS")
        ElseIf INDSleActivo.EditValue IsNot Nothing Then
            filterList.Add($"Activo Fijo: {INDSleActivo.Text.Trim()}")
            strFilter.Add($"PhysicalAssetId = {CInt(INDSleActivo.EditValue)}")
        End If

        If INDChkArticulo.Checked Then
            filterList.Add($"Artículo: TODOS")
        ElseIf INDSleArticulo.EditValue IsNot Nothing Then
            filterList.Add($"Artículo: {INDSleArticulo.Text.Trim()}")
            strFilter.Add($"ItemId = {CInt(INDSleArticulo.EditValue)}")
        End If

        If INDChkResponsable.Checked Then
            filterList.Add($"Responsable: TODOS")
        ElseIf INDSleResponsable.EditValue IsNot Nothing Then
            filterList.Add($"Responsable: {INDSleResponsable.Text.Trim()}")
            strFilter.Add($"MaintenanceResponsibleId = {CInt(INDSleResponsable.EditValue)}")
        End If

        If INDChkTipoEquipo.Checked Then
            filterList.Add($"Tipo Equipo: TODOS")
        ElseIf INDSleTipoEquipo.EditValue IsNot Nothing Then
            filterList.Add($"Tipo Equipo: {INDSleTipoEquipo.Text.Trim()}")
            strFilter.Add($"ItemTypeId = {CInt(INDSleTipoEquipo.EditValue)}")
        End If

        If INDChkTipoInventario.Checked Then
            filterList.Add($"Tipo Inventario: TODOS")
        ElseIf INDSleTipoInventario.EditValue IsNot Nothing Then
            filterList.Add($"Tipo Inventario: {INDSleTipoInventario.Text.Trim()}")
            strFilter.Add($"InventoryTypeId = {CInt(INDSleTipoInventario.EditValue)}")
        End If

        If INDChkTipoResponsable.Checked Then
            filterList.Add($"Tipo Responsable: TODOS")
        ElseIf INDSleTipoResponsable.EditValue IsNot Nothing Then
            filterList.Add($"Tipo Responsable: {INDSleTipoResponsable.Text.Trim()}")
            strFilter.Add($"ReponsibleTypeId = {CInt(INDSleTipoResponsable.EditValue)}")
        End If

        If INDChkUbicacion.Checked Then
            filterList.Add($"Ubicación: TODOS")
        ElseIf INDtreeLocation.EditValue IsNot Nothing Then
            filterList.Add($"Ubicación: {INDtreeLocation.Text.Trim()}")
            strFilter.Add($"LocationId = {CInt(INDtreeLocation.EditValue)}")
        End If

        strFilter.Add(Me.WriteFilterResponsible(selectedPage))
        strFilter.Add(Me.WriteFilterState(selectedPage))

        INDPceFilters.Properties.NullText = $"{String.Join(", ", filterList)}"

        Return String.Join(" AND ", strFilter)
    End Function

    Private Function WriteFilterResponsible(selectedPage As String) As String
        Dim strFilter As String = String.Empty
        If _maintenanceResponsible Is Nothing Then
            Exit Function
        End If
        Select Case selectedPage
            Case INDLcgScheduledMaintenance.Name, 'Mantenimientos Programados
                 INDLcgUnscheduledMaintenance.Name 'Mantenimientos no Programados

                If _maintenanceResponsible.ResponsibleRole = 4 Then
                    strFilter = String.Format("(MaintenanceResponsibleId = {0})", _maintenanceResponsible.Id)
                ElseIf _maintenanceResponsible.ResponsibleRole = 3 Then
                    strFilter = String.Format("(ISNULL(MaintenanceResponsibleId, {0}) = {0} OR ResponsibleRole IN ({1}))", _maintenanceResponsible.Id, "4,5")
                ElseIf _maintenanceResponsible.ResponsibleRole = 2 Then
                    strFilter = String.Format("(ISNULL(MaintenanceResponsibleId, {0}) = {0} OR ResponsibleRole IN ({1}))", _maintenanceResponsible.Id, "3,4,5")
                ElseIf _maintenanceResponsible.ResponsibleRole = 1 Then
                    strFilter = String.Format("(ISNULL(MaintenanceResponsibleId, {0}) = {0} OR ResponsibleRole IN ({1}))", _maintenanceResponsible.Id, "2,3,4,5")
                End If

            Case INDLcgMaintenanceToBeEvaluated.Name 'Mantenimientos por evaluar

                If _maintenanceResponsible Is Nothing OrElse _maintenanceResponsible.ResponsibleRole = 4 Then
                    strFilter = String.Format("(FixedAssetUserCode = '{0}')", indigo.AuditMessageWcf.CodeUser)
                ElseIf _maintenanceResponsible.ResponsibleRole = 3 Then
                    strFilter = String.Format("(ISNULL(FixedAssetUserCode, '{0}') = '{0}' OR ResponsibleRole IN ({1}))", indigo.AuditMessageWcf.CodeUser, "4,5")
                ElseIf _maintenanceResponsible.ResponsibleRole = 2 Then
                    strFilter = String.Format("(ISNULL(FixedAssetUserCode, '{0}') = '{0}' OR ResponsibleRole IN ({1}))", indigo.AuditMessageWcf.CodeUser, "3,4,5")
                ElseIf _maintenanceResponsible.ResponsibleRole = 1 Then
                    strFilter = String.Format("(ISNULL(FixedAssetUserCode, '{0}') = '{0}' OR ResponsibleRole IN ({1}))", indigo.AuditMessageWcf.CodeUser, "2,3,4,5")
                End If

        End Select

        Return strFilter
    End Function

    Private Function WriteFilterState(selectedPage As String) As String
        Dim strFilter As String = String.Empty

        Select Case selectedPage
            Case INDLcgScheduledMaintenance.Name, 'Mantenimientos Programados
                 INDLcgUnscheduledMaintenance.Name 'Mantenimientos no Programados

                strFilter = "ProgramState IN (1,2) AND WorkOrderState IN (1)"

            Case INDLcgMaintenanceToBeEvaluated.Name 'Mantenimientos por evaluar

                strFilter = "WorkOrderState IN (2)"

        End Select

        Return strFilter
    End Function

    Private Sub LoadScheduledMaintenances()
        If ListScheduledMaintenanceXpo IsNot Nothing OrElse _maintenanceResponsible Is Nothing Then
            Exit Sub
        End If
        INDGvScheduledMaintenance.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of ViewWorkOrderScheduledMaintenanceXpo) = Nothing
                                  Try
                                      result = _presenter.ListScheduledMaintenances(_strFilters)
                                      INDGcScheduledMaintenance.BeginInvoke(Sub()
                                                                                ListScheduledMaintenanceXpo = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
                                                                                INDGvScheduledMaintenance.HideLoadingPanel()
                                                                            End Sub)
                                  Catch ex As Exception
                                      INDGcScheduledMaintenance.BeginInvoke(Sub()
                                                                                INDGvScheduledMaintenance.HideLoadingPanel()
                                                                                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                            End Sub)
                                  End Try
                              End Sub)
    End Sub

    Private Sub LoadUnscheduledMaintenances()
        If ListUnscheduledMaintenanceXpo IsNot Nothing OrElse _maintenanceResponsible Is Nothing Then
            Exit Sub
        End If
        INDGvUnscheduledMaintenance.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of ViewWorkOrderUnScheduledMaintenanceXpo) = Nothing
                                  Try
                                      result = _presenter.ListUnscheduledMaintenances(_strFilters)
                                      INDGcUnscheduledMaintenance.BeginInvoke(Sub()
                                                                                  ListUnscheduledMaintenanceXpo = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
                                                                                  INDGvUnscheduledMaintenance.HideLoadingPanel()
                                                                              End Sub)
                                  Catch ex As Exception
                                      INDGcUnscheduledMaintenance.BeginInvoke(Sub()
                                                                                  INDGvUnscheduledMaintenance.HideLoadingPanel()
                                                                                  Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                              End Sub)
                                  End Try
                              End Sub)
    End Sub

    Private Sub LoadMaintenanceToBeEvaluated()
        If ListMaintenanceToBeEvaluatedXpo IsNot Nothing OrElse _maintenanceResponsible Is Nothing Then
            Exit Sub
        End If
        INDGvMaintenanceToBeEvaluated.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of ViewWorkOrderXpo) = Nothing
                                  Try
                                      result = _presenter.ListMaintenanceToBeEvaluated(_strFilters)
                                      INDGcMaintenanceToBeEvaluated.BeginInvoke(Sub()
                                                                                    ListMaintenanceToBeEvaluatedXpo = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
                                                                                    INDGvMaintenanceToBeEvaluated.HideLoadingPanel()
                                                                                End Sub)
                                  Catch ex As Exception
                                      INDGcMaintenanceToBeEvaluated.BeginInvoke(Sub()
                                                                                    INDGvMaintenanceToBeEvaluated.HideLoadingPanel()
                                                                                    Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                                End Sub)
                                  End Try
                              End Sub)
    End Sub

    Private Sub BeginReloadDatasource(selectedPage As String)
        _strFilters = WriteFilters(selectedPage)

        Select Case selectedPage
            Case INDLcgScheduledMaintenance.Name 'Mantenimientos Programados
                ListScheduledMaintenanceXpo = Nothing
                LoadScheduledMaintenances()
            Case INDLcgUnscheduledMaintenance.Name 'Mantenimientos no Programados
                ListUnscheduledMaintenanceXpo = Nothing
                LoadUnscheduledMaintenances()
            Case INDLcgMaintenanceToBeEvaluated.Name 'Mantenimientos por evaluar
                ListMaintenanceToBeEvaluatedXpo = Nothing
                LoadMaintenanceToBeEvaluated()
        End Select
    End Sub

    Private Sub ReturnModalArgs(sender As Object, e As EventArgs)
        BeginReloadDatasource(INDTcgWorkOrder.SelectedTabPageName)
    End Sub

    Private Function InstanceWorkOrderByScheduledMaintenance() As Domain.Entities.WorkOrder
        Dim view As ViewWorkOrderScheduledMaintenanceXpo = CType(INDGvScheduledMaintenance.GetFocusedRow(), ViewWorkOrderScheduledMaintenanceXpo)
        Return New Domain.Entities.WorkOrder With
        {
            .Id = If(view.WorkOrderId Is Nothing, 0, view.WorkOrderId),
            .Consecutive = view.WorkOrderCode,
            .OperatingUnitId = BarraBotones.OperatingUnitValue,
            .BranchOfficeId = view.BranchOfficeId,
            .BrachOfficeCodeName = view.BranchOfficeCodeName,
            .ProtocolId = view.ProtocolId,
            .ProtocolCodeName = view.ProtocolCodeName,
            .PhysicalAssetId = view.PhysicalAssetId,
            .PhysicalAssetDescription = String.Format("{0} - {1}", view.Plate, view.ItemCodeName),
            .MaintenanceResponsibleId = view.MaintenanceResponsibleId,
            .MaintenanceResponsibleCodeName = view.ResponsibleCodeName,
            .RequestDate = view.RequestDate,
            .ProgramDate = view.ProgramDate,
            .Description = view.Observation,
            .State = 1,
            .EntityId = view.MaintenancePlanProgramatedId,
            .EntityName = "MaintenancePlanProgramated"
        }
    End Function

    Private Function InstanceWorkOrderByMaintenanceToBeEvaluated(state As Byte, descriptionReversal As String) As List(Of Domain.Entities.WorkOrder)
        Dim listWorkOrder As New List(Of Domain.Entities.WorkOrder)
        For Each viewWorkOrder In ListMaintenanceToBeEvaluatedXpo.Where(Function(d) d.SelectOption).ToList()
            listWorkOrder.Add(New Domain.Entities.WorkOrder With
            {
                .Id = viewWorkOrder.Id,
                .Consecutive = viewWorkOrder.WorkOrderCode,
                .State = state,
                .DescriptionReversal = descriptionReversal
            })
        Next
        Return listWorkOrder
    End Function

    Private Function InstanceWorkOrderByUnScheduledMaintenance() As Domain.Entities.WorkOrder
        Dim view As ViewWorkOrderUnScheduledMaintenanceXpo = CType(INDGvUnscheduledMaintenance.GetFocusedRow(), ViewWorkOrderUnScheduledMaintenanceXpo)
        Return New Domain.Entities.WorkOrder With
        {
            .Id = If(view.WorkOrderId Is Nothing, 0, view.WorkOrderId),
            .Consecutive = view.WorkOrderCode,
            .OperatingUnitId = BarraBotones.OperatingUnitValue,
            .BranchOfficeId = view.BranchOfficeId,
            .BrachOfficeCodeName = view.BranchOfficeCodeName,
            .ProtocolId = view.ProtocolId,
            .ProtocolCodeName = view.ProtocolCodeName,
            .PhysicalAssetId = view.PhysicalAssetId,
            .PhysicalAssetDescription = String.Format("{0} - {1}", view.Plate, view.ItemCodeName),
            .MaintenanceResponsibleId = view.MaintenanceResponsibleId,
            .MaintenanceResponsibleCodeName = view.ResponsibleCodeName,
            .RequestDate = view.RequestDate,
            .ProgramDate = view.ProgramDate,
            .Description = view.Observation,
            .State = 1,
            .EntityId = view.FailureRequestDetailId,
            .EntityCode = view.FailureRequestCode,
            .EntityName = "MaintenanceFailureRequest"
        }
    End Function

    Private Sub OpenFormWorkOrder(workOrder As Domain.Entities.WorkOrder)
        Using form As New FrmWorkOrderModal()
            AddHandler form.ReturnModalArgs, AddressOf ReturnModalArgs
            form.MinimizeBox = False
            form.MaximizeBox = False
            form.Size = New System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)
            form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            form.WorkOrder = workOrder
            Dim transparent As New FrmTransparent(form, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Public Async Sub Guardar(listWorkOrder As List(Of Domain.Entities.WorkOrder))
        Try
            Using model As New MWorkOrder(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result = Await model.SaveWorkOrderAsync(listWorkOrder)
                AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    BeginReloadDatasource(INDTcgWorkOrder.SelectedTabPageName)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#Region "Multiselect"

    Private Sub VisibleCheck()
        Select Case INDTcgWorkOrder.SelectedTabPageName
            Case INDLcgMaintenanceToBeEvaluated.Name 'Mantenimientos por evaluar
                MaintenanceToBeEvaluatedVisibleCheck()
        End Select
    End Sub

    Private Sub MaintenanceToBeEvaluatedVisibleCheck()
        If ListMaintenanceToBeEvaluatedXpo IsNot Nothing AndAlso ListMaintenanceToBeEvaluatedXpo.Any Then
            If ListMaintenanceToBeEvaluatedXpo.Where(Function(item) item.SelectOption = True).Count = ListMaintenanceToBeEvaluatedXpo.Count Then
                Me.INDGvMaintenanceToBeEvaluated_SelectOption.Image = Global.Presentation.Maintenance.My.Resources.Resources.check
            Else
                Me.INDGvMaintenanceToBeEvaluated_SelectOption.Image = Global.Presentation.Maintenance.My.Resources.Resources.undcheck
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

#End Region

#Region "Handlers"

#Region "Load"

    Private Async Sub FrmWorkOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()

        _presenter = New PWorkOrder()

        AsyncLoader(True)
        Await Me.LoadMaintenanceResponsible()
        AsyncLoader(False)
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmWorkOrder_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDTcgWorkOrder.SelectedTabPageIndex = 0
        ReturnModalArgs(Nothing, Nothing)

        INDPceFilters.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleUbicacion_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDtreeLocation.QueryPopUp
        If INDtreeLocation.Properties.DataSource Is Nothing Then
            Task.Factory.StartNew(Sub()
                                      Using model As New Presentation.FixedAsset.MVP.MEquipmentEntry(Me.Tag)
                                          Dim ds As XPCollection(Of FixedAssetRepository.FixedAssetFixedAssetLocationXpo) = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetLocationCollection()
                                          Dim dsl = ds.ToList()
                                          Me.SafeInvoke(Sub()
                                                            INDtreeLocation.Properties.DataSource = dsl
                                                        End Sub)
                                      End Using
                                  End Sub)
        End If
    End Sub

    Private Sub INDSleActivo_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleActivo.QueryPopUp
        If INDSleActivo.Properties.DataSource Is Nothing Then
            INDSleActivo.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetPhysicalAsset()
        End If
    End Sub

    Private Sub INDSleArticulo_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleArticulo.QueryPopUp
        If INDSleArticulo.Properties.DataSource Is Nothing Then
            INDSleArticulo.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetEquipmentByStatus(True)
        End If
    End Sub

    Private Sub INDSleTipoResponsable_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTipoResponsable.QueryPopUp
        If INDSleTipoResponsable.Properties.DataSource Is Nothing Then
            INDSleTipoResponsable.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetResponsibleType()
        End If
    End Sub

    Private Sub INDSleTipoInventario_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTipoInventario.QueryPopUp
        If INDSleTipoInventario.Properties.DataSource Is Nothing Then
            INDSleTipoInventario.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetInventoryType()
        End If
    End Sub

    Private Sub INDSleTipoEquipo_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTipoEquipo.QueryPopUp
        If INDSleTipoEquipo.Properties.DataSource Is Nothing Then
            INDSleTipoEquipo.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListFixedAssetItemType()
        End If
    End Sub

    Private Sub INDSleResponsable_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleResponsable.QueryPopUp
        If INDSleResponsable.Properties.DataSource Is Nothing Then
            INDSleResponsable.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListMaintenanceResponsibleByStatus(True)
        End If
    End Sub

#End Region

#Region "CheckedChanged"

    Private Sub INDChkUbicacion_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkUbicacion.CheckedChanged
        If INDChkUbicacion.Checked Then
            INDtreeLocation.EditValue = Nothing
            INDtreeLocation.Enabled = False
        Else
            INDtreeLocation.Enabled = True
        End If
    End Sub

    Private Sub INDChkActivo_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkActivo.CheckedChanged
        If INDChkActivo.Checked Then
            INDSleActivo.EditValue = Nothing
            INDSleActivo.Enabled = False
        Else
            INDSleActivo.Enabled = True
        End If
    End Sub

    Private Sub INDChkArticulo_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkArticulo.CheckedChanged
        If INDChkArticulo.Checked Then
            INDSleArticulo.EditValue = Nothing
            INDSleArticulo.Enabled = False
        Else
            INDSleArticulo.Enabled = True
        End If
    End Sub

    Private Sub INDChkTipoResponsable_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkTipoResponsable.CheckedChanged
        If INDChkTipoResponsable.Checked Then
            INDSleTipoResponsable.EditValue = Nothing
            INDSleTipoResponsable.Enabled = False
        Else
            INDSleTipoResponsable.Enabled = True
        End If
    End Sub

    Private Sub INDChkTipoInventario_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkTipoInventario.CheckedChanged
        If INDChkTipoInventario.Checked Then
            INDSleTipoInventario.EditValue = Nothing
            INDSleTipoInventario.Enabled = False
        Else
            INDSleTipoInventario.Enabled = True
        End If
    End Sub

    Private Sub INDChkTipoEquipo_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkTipoEquipo.CheckedChanged
        If INDChkTipoEquipo.Checked Then
            INDSleTipoEquipo.EditValue = Nothing
            INDSleTipoEquipo.Enabled = False
        Else
            INDSleTipoEquipo.Enabled = True
        End If
    End Sub

    Private Sub INDChkResponsable_CheckedChanged(sender As Object, e As EventArgs) Handles INDChkResponsable.CheckedChanged
        If INDChkResponsable.Checked Then
            INDSleResponsable.EditValue = Nothing
            INDSleResponsable.Enabled = False
        Else
            INDSleResponsable.Enabled = True
        End If
    End Sub

    Private Sub INDBtnAplicar_Click(sender As Object, e As EventArgs) Handles INDBtnAplicar.Click
        BeginReloadDatasource(INDTcgWorkOrder.SelectedTabPageName)
    End Sub

#End Region

#Region "SelectPageChanged"

    Private Sub INDTcgWorkOrder_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDTcgWorkOrder.SelectedPageChanged
        BeginReloadDatasource(e.Page.Name)
    End Sub

#End Region

#Region "SelectionChanged"

    Private Sub INDGvMaintenanceToBeEvaluated_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDGvMaintenanceToBeEvaluated.SelectionChanged
        If e.Action = ComponentModel.CollectionChangeAction.Remove OrElse e.Action = ComponentModel.CollectionChangeAction.Refresh Then
            If ListMaintenanceToBeEvaluatedXpo IsNot Nothing Then
                For Each viewRequest In ListMaintenanceToBeEvaluatedXpo.Where(Function(d) d.SelectOption)
                    viewRequest.SelectOption = False
                Next
            End If
        End If
        SelectOptions(INDGvMaintenanceToBeEvaluated, 1, False)
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    Private Sub INDGvScheduledMaintenance_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvScheduledMaintenance.CustomColumnDisplayText, INDGvUnscheduledMaintenance.CustomColumnDisplayText, INDGvMaintenanceToBeEvaluated.CustomColumnDisplayText
        If e.Column.FieldName = "ProgramDateMonth" Then
            Select Case CInt(e.Value)
                Case 1
                    e.DisplayText = "Enero"
                Case 2
                    e.DisplayText = "Febrero"
                Case 3
                    e.DisplayText = "Marzo"
                Case 4
                    e.DisplayText = "Abril"
                Case 5
                    e.DisplayText = "Mayo"
                Case 6
                    e.DisplayText = "Junio"
                Case 7
                    e.DisplayText = "Julio"
                Case 8
                    e.DisplayText = "Agosto"
                Case 9
                    e.DisplayText = "Septiembre"
                Case 10
                    e.DisplayText = "Octubre"
                Case 11
                    e.DisplayText = "Noviembre"
                Case 12
                    e.DisplayText = "Diciembre"
                Case Else
                    e.DisplayText = String.Empty
            End Select
        End If
    End Sub

#End Region

#Region "MenuContext"

    Private Sub INDview_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvScheduledMaintenance.PopupMenuShowing, INDGvUnscheduledMaintenance.PopupMenuShowing, INDGvMaintenanceToBeEvaluated.PopupMenuShowing
        INDBbiWorkOrder.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiApprove.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiReject.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        If e.HitInfo.RowHandle < 0 Then
            Exit Sub
        Else
            Select Case INDTcgWorkOrder.SelectedTabPageName
                Case INDLcgScheduledMaintenance.Name 'Mantenimientos Programados
                    PopupMenuScheduledMaintenance()
                Case INDLcgUnscheduledMaintenance.Name 'Mantenimientos no Programados
                    PopupMenuUnScheduledMaintenance()
                Case INDLcgMaintenanceToBeEvaluated.Name 'Mantenimientos por evaluar
                    PopupMenuMaintenanceToBeEvaluated()
            End Select
        End If

        Dim View = CType(sender, GridView)
        INDPopMenuActions1.Manager = BarManager1
        INDPopMenuActions1.ShowPopup(View.GridControl.PointToScreen(e.Point))
    End Sub

    Private Sub PopupMenuScheduledMaintenance()
        INDBbiWorkOrder.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
    End Sub

    Private Sub PopupMenuUnScheduledMaintenance()
        INDBbiWorkOrder.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
    End Sub

    Private Sub PopupMenuMaintenanceToBeEvaluated()
        INDBbiApprove.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        INDBbiReject.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
    End Sub

#End Region

#Region "ItemClick"

    Private Sub INDBbiWorkOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiWorkOrder.ItemClick
        'Se obtiene el registro que tiene el foco
        Dim workOrder As New Domain.Entities.WorkOrder
        Select Case INDTcgWorkOrder.SelectedTabPageName
            Case INDLcgScheduledMaintenance.Name 'Mantenimientos Programados
                workOrder = InstanceWorkOrderByScheduledMaintenance()
            Case INDLcgUnscheduledMaintenance.Name 'Mantenimientos no Programados
                workOrder = InstanceWorkOrderByUnScheduledMaintenance()
            Case Else
                Mensaje(EeventViewerImages.Advertencia) = "Opción no válida"
                Exit Sub
        End Select

        OpenFormWorkOrder(workOrder)
    End Sub

    Private Sub INDBbiApproveAndReject_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiApprove.ItemClick, INDBbiReject.ItemClick
        Dim descriptionReversal As String = String.Empty

        Using PopUpAnnulmentReason As New PopUpAnnulmentReasonWorkOrder()
            PopUpAnnulmentReason.PopUpType = If(e.Item.Name = "INDBbiApprove", 1, 2)
            Dim transparent As New FrmTransparent(PopUpAnnulmentReason, False)
            If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                AsyncLoader(False)
                Exit Sub
            End If

            descriptionReversal = PopUpAnnulmentReason.ReversalDescription
        End Using

        Dim state = If(e.Item.Name = "INDBbiApprove", 4, 5)
        Dim listWorkOrder = InstanceWorkOrderByMaintenanceToBeEvaluated(state, descriptionReversal)

        Guardar(listWorkOrder)
    End Sub

#End Region

#Region "MouseDoubleClick"

    Private Sub INDGcMaintenanceToBeEvaluated_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcMaintenanceToBeEvaluated.MouseDoubleClick
        If ListMaintenanceToBeEvaluatedXpo IsNot Nothing AndAlso ListMaintenanceToBeEvaluatedXpo.Any() Then
            Dim hitPoint = Me.INDGvMaintenanceToBeEvaluated.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDGvMaintenanceToBeEvaluated_SelectOption") Then

                    Dim listFilterXpCollection = INDGvMaintenanceToBeEvaluated.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDGvMaintenanceToBeEvaluated_SelectOption.Image = Global.Presentation.Maintenance.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        cont = (From l In listFilterXpCollection Where l.SelectOption = True).Count
                        If cont = ListMaintenanceToBeEvaluatedXpo.Count Then
                            Me.INDGvMaintenanceToBeEvaluated_SelectOption.Image = Global.Presentation.Maintenance.My.Resources.Resources.check
                        End If
                    End If
                    Me.INDGcMaintenanceToBeEvaluated.RefreshDataSource()
                    Me.INDGcMaintenanceToBeEvaluated.Invalidate()
                End If
            End If
        End If
    End Sub

#End Region

#Region "ShowingEditor"

    Private Sub INDGvScheduledMaintenance_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDGvScheduledMaintenance.ShowingEditor
        INDGvScheduledMaintenance_PceNotification.PopupControl = Nothing
        INDGvUnscheduledMaintenance_PceNotification.PopupControl = Nothing
        INDGvMaintenanceToBeEvaluated_PceNotification.PopupControl = Nothing
        INDGcWorkOrderNotification.DataSource = Nothing

        Dim view = CType(INDGvScheduledMaintenance.GetFocusedRow(), ViewWorkOrderScheduledMaintenanceXpo)
        INDGcWorkOrderNotification.DataSource = Me._presenter.GetNotificationsBySource("MaintenancePlanProgramated", view.MaintenancePlanProgramatedId)
        INDGvScheduledMaintenance_PceNotification.PopupControl = INDPccWorkOrderNotification
    End Sub

    Private Sub INDGvUnScheduledMaintenance_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDGvUnscheduledMaintenance.ShowingEditor
        INDGvScheduledMaintenance_PceNotification.PopupControl = Nothing
        INDGvUnscheduledMaintenance_PceNotification.PopupControl = Nothing
        INDGvMaintenanceToBeEvaluated_PceNotification.PopupControl = Nothing
        INDGcWorkOrderNotification.DataSource = Nothing

        Dim view = CType(INDGvUnscheduledMaintenance.GetFocusedRow(), ViewWorkOrderUnScheduledMaintenanceXpo)
        INDGcWorkOrderNotification.DataSource = Me._presenter.GetNotificationsBySource("MaintenanceFailureRequest", view.FailureRequestDetailId)
        INDGvUnscheduledMaintenance_PceNotification.PopupControl = INDPccWorkOrderNotification
    End Sub

    Private Sub INDGvMaintenanceToBeEvaluated_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDGvMaintenanceToBeEvaluated.ShowingEditor
        INDGvScheduledMaintenance_PceNotification.PopupControl = Nothing
        INDGvUnscheduledMaintenance_PceNotification.PopupControl = Nothing
        INDGvMaintenanceToBeEvaluated_PceNotification.PopupControl = Nothing
        INDGcWorkOrderNotification.DataSource = Nothing

        Dim view = CType(INDGvMaintenanceToBeEvaluated.GetFocusedRow, ViewWorkOrderXpo)
        INDGcWorkOrderNotification.DataSource = Me._presenter.GetNotificationsByWorkOrderId(view.Id)
        INDGvMaintenanceToBeEvaluated_PceNotification.PopupControl = INDPccWorkOrderNotification
    End Sub

#End Region

#End Region

End Class