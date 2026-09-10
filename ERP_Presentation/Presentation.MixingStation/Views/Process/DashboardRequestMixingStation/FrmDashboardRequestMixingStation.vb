'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/02/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Text
Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmDashboardRequestMixingStation

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PDashboardRequestMixingStation

#End Region

#Region "Properties"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
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

#Region "Methods"

    ''' <summary>
    ''' Método que abre el form para asignar la linea de producción
    ''' </summary>
    Private Sub OpenFormAssignProductionLine()
        Using formulario As New FrmAssignProductionLine()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnAddEventArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 250
            formulario.ToolBar.Visible = False
            Dim infoItemSelected = CType(INDviewRequestMixingStation.GetFocusedRow(), ViewListDashboardRequestMixingStationXpo)
            formulario.RequestMixingStationDetailId = infoItemSelected.RequestMixingStationDetailId
            formulario.RequestMixingStationId = infoItemSelected.RequestMixingStationId
            formulario.ProductionLineId = infoItemSelected.ProductionLineId
            formulario.ProductionLineCodeName = infoItemSelected.ProductionLineCodeName
            formulario.MixingStationId = infoItemSelected.CMConfigurationId
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método para identificar si la solicitud tiene más de una linea de producción
    ''' </summary>
    Private Async Sub MixingStationProductionLineByRequest()
        Dim infoItemSelected = CType(INDviewRequestMixingStation.GetFocusedRow(), ViewListDashboardRequestMixingStationXpo)
        If infoItemSelected.ConfirmationStatus <> 2 And infoItemSelected.MSClass = EUnitDoseTypeClass.Cytostatic And infoItemSelected.RequestType <> 4 And Not infoItemSelected.EntityName = NameOf(Domain.Entities.RequestPackageDetailStatus) Then
            Mensaje(EeventViewerImages.Advertencia) = "El paciente no ha confirmado asistencia"
            Exit Sub
        End If
        Dim datasource = Presenter.MixingStationProductionLineXpo(infoItemSelected.CMConfigurationId, infoItemSelected.RequestMixingStationDetailId)
        If datasource.Count = 1 Then
            Try
                Using model As New MDashboardRequestMixingStation("")
                    Dim data As New Tuple(Of Integer, Integer)(infoItemSelected.RequestMixingStationDetailId, datasource.FirstOrDefault().Id)
                    Dim result = Await model.SaveRequestMixingStation(data)
                    Me.AsyncLoader(False)
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = "Se ha modificado la línea de producción correctamente"
                        BeginReloadDatasource()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                End Using
            Catch ex As Exception
                Me.AsyncLoader(False)
                Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
            End Try
        Else
            OpenFormAssignProductionLine()
        End If
    End Sub

    ''' <summary>
    ''' Carga los medicamentos para producción
    ''' </summary>
    Private Sub LoadInformation()
        If INDgcRequestMixingStation.DataSource IsNot Nothing OrElse INDsleCM.EditValue Is Nothing Then
            Exit Sub
        End If
        INDviewRequestMixingStation.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of ViewListDashboardRequestMixingStationXpo) = Nothing
                                  Try
                                      result = Presenter.ListViewListDashboardRequestMixingStation(INDsleCM.EditValue)
                                      INDgcRequestMixingStation.BeginInvoke(Sub()
                                                                                INDgcRequestMixingStation.DataSource = result?.OrderBy(Function(m) m.OrderField)?.ToList()
                                                                                INDviewRequestMixingStation.HideLoadingPanel()
                                                                            End Sub)
                                  Catch ex As Exception
                                      INDgcRequestMixingStation.BeginInvoke(Sub()
                                                                                INDviewRequestMixingStation.HideLoadingPanel()
                                                                                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                            End Sub)
                                  End Try
                              End Sub)
    End Sub

    ''' <summary>
    ''' Vuelve a cargar el datasource de la rejilla activa
    ''' </summary>
    Private Sub BeginReloadDatasource()
        INDgcRequestMixingStation.DataSource = Nothing
        LoadInformation()
    End Sub

    ''' <summary>
    ''' Método que asigna la columna de acciones a las rejillas
    ''' </summary>
    Private Sub SetActionsColumns()
        BarraBotones.ActualizarPermisosBarra(Me.Tag)

        Dim listActions As New List(Of eAcciones)

        If BarraBotones.PermissionsForm.ContainsKey(81) Then 'si tiene permiso de procesar
            listActions.Add(eAcciones.Process)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(124) Then 'si tiene permiso de asignar linea de producción
            listActions.Add(eAcciones.AssignProductionLine)
        End If

        listActions.Add(eAcciones.Annular)

        IndigoGridView1.SetListAcction(INDviewRequestMixingStation, listActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewRequestMixingStation.Columns
            If col.Name = "colactions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Abre el form de detalles de la solicitud
    ''' </summary>
    Private Sub OpenFormDetail()
        If INDValidateOpenFormDetail() = False Then Exit Sub
        Using formulario As New FrmDashboardRequestMixingStationDetail()
            AddHandler formulario.ReloadPrincipalGridArgs, AddressOf ReturnAddEventArgs
            'Se obtienen los registros seleccionados
            Dim listItems = (From x In INDviewRequestMixingStation.GetSelectedRows() Where Not INDviewRequestMixingStation.IsGroupRow(x) Select DirectCast(INDviewRequestMixingStation.GetRow(x), ViewListDashboardRequestMixingStationXpo)).ToList()
            Dim listIds = (From x In listItems Select x.RequestMixingStationDetailId).ToList()
            Dim stringsIds = String.Join(",", listIds.ToArray())
            formulario.requestIds = stringsIds
            formulario.Items = listItems
            formulario.ViewListDashboardRequestMixingStationXpo = CType(INDviewRequestMixingStation.GetFocusedRow(), ViewListDashboardRequestMixingStationXpo)
            'formulario.Size = New System.Drawing.Size(1150, 700)
            formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 300, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - 300)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' validar control.
    ''' </summary>
    ''' <returns></returns>
    Public Function INDValidateOpenFormDetail() As Boolean
        Dim listItems = (From x In INDviewRequestMixingStation.GetSelectedRows() Where Not INDviewRequestMixingStation.IsGroupRow(x) Select DirectCast(INDviewRequestMixingStation.GetRow(x), ViewListDashboardRequestMixingStationXpo)).ToList()
        Dim errors As New StringBuilder
        'Si se selecciona al menos un item que no tenga orden de produccion
        If listItems.Any(Function(i) i.ProductionLineId = 0) Then
            errors.AppendLine("Debe seleccionar una Linea de Producción")
        End If

        If listItems.Any(Function(x) x.ConfirmationStatus <> 2 And x.MSClass = EUnitDoseTypeClass.Cytostatic And x.RequestType <> 4 And x.EntityName <> NameOf(Domain.Entities.RequestPackageDetailStatus)) Then
            If listItems.Count > 1 Then
                errors.AppendLine("No se ha confirmado la asistencia de las solicitudes seleccionadas")
            ElseIf listItems.Count = 1 Then
                errors.AppendLine("El paciente no ha confirmado asistencia")
            End If
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If
        Return True
    End Function


    ''' <summary>
    ''' Método que se ejecuta al cerrar el form de detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddEventArgs(sender As Object, e As EventArgs)
        BeginReloadDatasource()
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardRequestMixingStation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'SetActionsColumns()
        Me.ToolBar.Hide()
        Presenter = New PDashboardRequestMixingStation()
        SetActionsColumns()
        INDsleCM.Font = New Font("Segoe UI", 28, FontStyle.Regular)
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardRequestMixingStation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCM.Properties.PopupFormMinSize = New System.Drawing.Size(INDsleCM.Size.Width - 11, 0)
        INDsleCM.Properties.PopupFormSize = New System.Drawing.Size(INDsleCM.Size.Width - 11, 0)
        INDsleCM.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCM_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCM.QueryPopUp
        If INDsleCM.Properties.DataSource Is Nothing Then
            INDsleCM.Properties.DataSource = Presenter.ListCMByUser()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Se dispara la cambiar el valor del control de central de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCM_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCM.EditValueChanged
        If INDsleCM.EditValue IsNot Nothing Then
            BeginReloadDatasource()
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim tagGrid As String = ""
        If sender.GetType() Is GetType(DevExpress.XtraEditors.SimpleButton) Or sender.GetType() Is GetType(DevExpress.XtraBars.BarButtonItem) Then
            tagGrid = sender.Tag.ToString()
        Else
            tagGrid = sender.Text
        End If
        Select Case tagGrid
            Case "Process", "Procesar"
                OpenFormDetail()
            Case "AssignProductionLine", "Asignar Línea Producción"
                MixingStationProductionLineByRequest()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Process", "Procesar"
                OpenFormDetail()
            Case "AssignProductionLine", "Asignar Línea Producción"
                MixingStationProductionLineByRequest()
            Case "Annular"
                Annular()
        End Select
    End Sub

    Private Async Sub Annular()
        Try
            If MessageIndigo.Show("¿Desea anular los ítems seleccionados?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            INDviewRequestMixingStation.ShowLoadingPanel()
            Dim items = SelectedItems
            Dim ids = items.Select(Function(m) m.RequestMixingStationDetailId).ToList()
            Using model As New MDashboardRequestMixingStation(Tag)
                Dim res = Await model.AnnulateRequestsAsync(ids)

                INDviewRequestMixingStation.HideLoadingPanel()
                If res.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = "Ítems anulados correctamente"
                    BeginReloadDatasource()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = res.Message
                End If
            End Using
        Catch ex As Exception
            INDviewRequestMixingStation.HideLoadingPanel()
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
            Throw ex
        End Try
    End Sub

#End Region

#Region "ItemClick"

    ''' <summary>
    ''' Evento que se dispara al presionar refrescar del menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBarRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarRefresh.ItemClick
        BeginReloadDatasource()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        BeginReloadDatasource()
    End Sub

    ''' <summary>
    ''' Muestra la bandera dependiendo de las condiciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewRequestMixingStation_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDviewRequestMixingStation.CustomUnboundColumnData
        If e.Column.Name = ColIconStateHis.Name AndAlso e.IsGetData Then
            Dim imgStatus = GetStatusImage(e.Row)
            If imgStatus IsNot Nothing Then
                e.Value = imgStatus
            End If
        End If
    End Sub

    ''' <summary>
    ''' Obtiene bandera a establecer de acuerdo al estado del producto/medicamento
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    Private Function GetStatusImage(row)
        If ShouldShowAlertImage(row) Then
            Return GetImage(My.Resources.Alerta)
        ElseIf ShouldShowAcceptImage(row) Then
            Return GetImage(My.Resources.aceptar16x16)
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Valida si la fila cumple con los siguientes estados
    ''' 2 - Tratamiento anulado o Alta médica 
    ''' 4 - Tratamiento suspendido 
    ''' 7 - Tratamiento terminado por salida del paciente'
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    Private Function ShouldShowAlertImage(row) As Boolean
        Return ({2, 4, 7}.Contains(row.StatusHCPRESCRA) _
            OrElse row.FECALTPAC IsNot Nothing _
            OrElse row.EntityName = NameOf(Domain.Entities.RequestPackageDetailStatus))
    End Function


    ''' <summary>
    ''' Valida en la fila, si el paciente ya hizo la confirmacion y si es de tipo oncologico
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    Private Function ShouldShowAcceptImage(row) As Boolean
        Return row.ConfirmationStatus = 2 AndAlso row.MSClass = EUnitDoseTypeClass.Cytostatic
    End Function


    Private Function GetImage(img As Image) As Byte()
        Return DevExpress.XtraEditors.Controls.ByteImageConverter.ToByteArray(img, ImageFormat.Gif)
    End Function

    Private Sub ToolTipController1_GetActiveObjectInfo(sender As Object, e As ToolTipControllerGetActiveObjectInfoEventArgs) Handles ToolTipController1.GetActiveObjectInfo
        If Not e.SelectedControl Is INDgcRequestMixingStation Then Return

        Dim info As ToolTipControlInfo = Nothing
        Dim view As GridView = CType(INDgcRequestMixingStation.GetViewAt(e.ControlMousePosition), GridView)

        If view Is Nothing Then Return

        Dim hi As GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)
        Dim row = view.GetRow(hi.RowHandle)
        Dim text As String = String.Empty

        If hi.Column IsNot Nothing AndAlso hi.Column.Name = ColIconStateHis.Name AndAlso hi.InDataRow Then
            Dim item = DirectCast(row, ViewListDashboardRequestMixingStationXpo)
            If item?.EntityName = NameOf(Domain.Entities.RequestPackageDetailStatus) Then
                text = "reprogramación"
            Else
                If item?.ConfirmationStatus = 2 AndAlso item?.MSClass = EUnitDoseTypeClass.Cytostatic Then
                    text = "Asistencia confirmada"
                Else
                    text = item?.StatusNameHCPRESCRA
                End If

            End If
        Else
            text = CType(view.GetRowCellValue(hi.RowHandle, hi.Column), String)
        End If

        info = New ToolTipControlInfo(row, text)
        If Not info Is Nothing Then e.Info = info
    End Sub

    ''' <summary>
    ''' items seleccionados
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property SelectedItems As List(Of ViewListDashboardRequestMixingStationXpo)
        Get
            Return INDviewRequestMixingStation.GetSelectedRows() _
                   .Where(Function(m) Not INDviewRequestMixingStation.IsGroupRow(m)) _
                   .Select(Function(m) CType(INDviewRequestMixingStation.GetRow(m), ViewListDashboardRequestMixingStationXpo)) _
                   .ToList()
        End Get
    End Property

    ''' <summary>
    ''' popup menu showing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewRequestMixingStation_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDviewRequestMixingStation.PopupMenuShowing
        If e.HitInfo.InRow Then
            If Not INDviewRequestMixingStation.GetSelectedRows().Contains(e.HitInfo.RowHandle) Then
                IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(i) i.Visibility = DevExpress.XtraBars.BarItemVisibility.Never)
                Return
            End If

            Dim process = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Process)))
            Dim assignProductionLine = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.AssignProductionLine)))
            Dim annular = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Annular)))

            Dim items = SelectedItems

            process.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            assignProductionLine.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            annular.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            If items.All(Function(m) {2, 4, 7}.Contains(m.StatusHCPRESCRA) OrElse m.FECALTPAC IsNot Nothing) Then
                annular.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            End If
        End If
    End Sub

    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As Controls.QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        Dim popUp = CType(sender, RepositoryItemPopupContainerEdit)
        e.Buttons.ToList().ForEach(Sub(i) i.Visible = False)

        If Not INDviewRequestMixingStation.GetSelectedRows().Contains(INDviewRequestMixingStation.FocusedRowHandle) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un item para ejecutar alguna acción"
            popUp.PopupControl.MinimumSize = New Size(200, 0)
            popUp.PopupControl.MaximumSize = New Size(200, 0)
            popUp.PopupControl.Size = New Size(200, 0)
            Return
        End If

        Dim items = SelectedItems

        Dim process = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Process)))
        Dim assignProductionLine = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.AssignProductionLine)))
        Dim annular = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Annular)))

        process.Visible = True
        assignProductionLine.Visible = True
        annular.Visible = False

        Dim count As Integer = 2

        If items.All(Function(m) {2, 4, 7}.Contains(m.StatusHCPRESCRA) OrElse m.FECALTPAC IsNot Nothing) Then
            annular.Visible = True
            count += 1
        End If

        popUp.PopupControl.Size = New System.Drawing.Size(200, 36 * count)
    End Sub

#End Region

#End Region

End Class