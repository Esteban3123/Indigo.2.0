'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/11/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Dynamic
Imports System.Text
Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Domain.Entities
Imports IndigoeHistorias
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmDashboardConfirmationUnitDose

#Region "Variables"

    ''' <summary>
    ''' Variable que se utiliza para obtener los códigos de centro de atención y realizar las consulta con los filtros
    ''' </summary>
    Private CareCenterFilters As String

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PDashboardConfirmationUnitDose

    ''' <summary>
    ''' Almacena filas que cumplen la condicion para seleccion
    ''' </summary>
    Private _validRows As New List(Of Integer)()

    ''' <summary>
    ''' Bandera que indica si es la primera vez 
    ''' </summary>
    Private _isFirstSelection As Boolean = True

    ''' <summary>
    ''' Bandera para definir si esta en seleccion masivamente
    ''' </summary>
    Private _isProcessing As Boolean = False

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

#Region "Enumerations"
    ''' <summary>
    ''' Enum enviar a Procesar 
    ''' </summary>
    Enum eProcess
        MixingStation = 2
        Pharmacy = 1
    End Enum
#End Region

#Region "Methods"
    Private Async Sub Annular()
        If MessageIndigo.Show("¿Desea anular los items seleccionados?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        AsyncLoader(True)
        Try
            Using model As New MDashboardConfirmationUnitDose(Me.Tag.ToString())
                Dim items = SelectedItems

                Dim data As List(Of AnnulateUnitDoseModel) = items.Select(Function(m) New AnnulateUnitDoseModel With {
                    .CodeSusceptibleMixingStation = Guid.Parse(m.CodeSusceptibleMixingStation),
                    .GroupingCodeDose = Guid.Parse(m.AGRUPAQUETE),
                    .Quantity = 1
                })?.ToList()

                Dim result = Await model.AnnulateUnitDosesAsync(data)
                AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    BeginReloadDatasource()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Muestra el perfil farmacoterapeutico
    ''' </summary>
    Private Sub PharmacotherapeuticProfile()
        Dim ItemSelect = CType(INDviewConfirmUnitDose.GetFocusedRow(), ViewListDashboardConfirmationUnitDoseXpo)

        Using formulario As New frmHCPerfilFarmacoterapeutico2(ItemSelect.CareCenterCode, ItemSelect.FunctionalUnitCode, ItemSelect.PatientCode, ItemSelect.AdmissionCode, False)
            formulario.Size = New Size(1500, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Envia a farmacia
    ''' </summary>
    Private Sub SendPharmacy()

        Dim listItemsSelected = (From x In INDviewConfirmUnitDose.GetSelectedRows() Where Not INDviewConfirmUnitDose.IsGroupRow(x) Select DirectCast(INDviewConfirmUnitDose.GetRow(x), ViewListDashboardConfirmationUnitDoseXpo)).ToList()

        Dim listCodes = (From x In listItemsSelected Select x.ServiceCode).ToList()
        Dim messagesCodes = String.Join(",", listCodes.ToArray())
        If MessageIndigo.Show("Desea enviar los medicamentos " + messagesCodes + " a dashboard farmacia?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If (From x In listItemsSelected Where x.PackageId <> Nothing AndAlso x.PackageId > 0 Select x).Count > 0 Then
            Dim message As New StringBuilder
            For Each item In (From x In listItemsSelected Where x.PackageId <> Nothing AndAlso x.PackageId > 0 Select x).ToList
                message.AppendLine("No se puede enviar a farmacia porque el registro " + item.ServiceDescription + " tiene asignado un paquete")
            Next
            Mensaje(EeventViewerImages.Advertencia) = message.ToString()
            Exit Sub
        End If

        ProcessSendTo(eProcess.Pharmacy)
    End Sub

    ''' <summary>
    ''' Envia a central de mezclas
    ''' </summary>
    Private Sub ProcessMixingStation()
        Dim listItemsSelected = (From x In INDviewConfirmUnitDose.GetSelectedRows() Where Not INDviewConfirmUnitDose.IsGroupRow(x) Select DirectCast(INDviewConfirmUnitDose.GetRow(x), ViewListDashboardConfirmationUnitDoseXpo)).ToList()

        Dim messages = String.Empty
        If listItemsSelected.Count > 1 Then
            messages = "Las solicitudes seleccionadas en la rejilla serán enviadas para procesamiento de la central de mezclas"
        Else
            messages = $"La solicitud {listItemsSelected.FirstOrDefault.ServiceCode} será enviada para procesamiento de la central de mezclas"
        End If

        If MessageIndigo.Show($"{messages}, ¿Desea Continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If (From x In listItemsSelected Where x.PackageId = Nothing OrElse x.PackageId = 0 Select x).Count > 0 Then
            'Dim message As New StringBuilder
            Dim message As New List(Of String)()

            For Each item In (From x In listItemsSelected Where x.PackageId = Nothing OrElse x.PackageId = 0 Select x).ToList
                message.Add("No se puede enviar a procesar central mezclas porque el registro " + item.ServiceDescription + " no tiene asignado un paquete")
            Next

            Mensaje(EeventViewerImages.Advertencia) = String.Join(vbCrLf, message.Distinct().ToList())
            Exit Sub
        End If

        ProcessSendTo(eProcess.MixingStation)
    End Sub

    ''' <summary>
    ''' Envia los datos a guardar para enviar a farmacia o central de mezclas
    ''' </summary>
    ''' <param name="sendTo"></param>
    Private Async Sub ProcessSendTo(sendTo As Integer)
        Dim args = AssigningValues(sendTo)
        Me.AsyncLoader(True)
        Try
            Using model As New MDashboardConfirmationUnitDose(Me.Tag.ToString())
                Dim result = Await model.UpdateMedicalOrderCM(args)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    BeginReloadDatasource()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad principal
    ''' </summary>
    Private Function AssigningValues(sendTo As Integer?) As Object
        Dim args As Object = New ExpandoObject()
        Dim myListDetail As New ConcurrentBag(Of Object)()

        Dim listItemsSelected = SelectedItems

        For Each item In listItemsSelected
            Dim detail As Object = New ExpandoObject()
            detail.SendTo = sendTo
            detail.SourceType = item.SourceType
            detail.CodeSusceptibleMixingStation = item.CodeSusceptibleMixingStation
            detail.ConfirmationUnitDoseId = item.ConfirmationUnitDoseId
            detail.PatientCode = item.PatientCodeName.Split("-").ElementAt(0).Trim()
            detail.Bed = item.Bed
            detail.EntityId = item.EntityId
            detail.EntityName = item.EntityName
            myListDetail.Add(detail)
        Next

        args.CMConfigurationId = INDsleCM.EditValue
        args.SendComplete = myListDetail
        Return args
    End Function

    ''' <summary>
    ''' Carga los medicamentos para producción
    ''' </summary>
    Private Async Function LoadInformation() As Task
        If INDgcConfirmUnitDose.DataSource IsNot Nothing OrElse INDsleCM.EditValue Is Nothing Then
            Exit Function
        End If

        INDviewConfirmUnitDose.ShowLoadingPanel()
        Try
            Dim result = Await Task.Factory.StartNew(Function() Presenter.ListViewListDashboardConfirmationUnitDose(INDsleCM.EditValue))
            INDgcConfirmUnitDose.DataSource = result
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            INDviewConfirmUnitDose.HideLoadingPanel()
        End Try
    End Function

    ''' <summary>
    ''' Vuelve a cargar el datasource de la rejilla activa
    ''' </summary>
    Private Sub BeginReloadDatasource()
        INDgcConfirmUnitDose.DataSource = Nothing
        LoadInformation()
        _validRows.Clear()
    End Sub

    ''' <summary>
    ''' Método que asigna la columna de acciones a las rejillas
    ''' </summary>
    Private Sub SetActionsColumns()
        BarraBotones.ActualizarPermisosBarra(Me.Tag)
        IndigoGridView1.MoreInfoColunmns(INDviewConfirmUnitDose)

        Dim actions As New List(Of eAcciones)
        actions.Add(eAcciones.PackageDetail)
        actions.Add(eAcciones.ProductionPlanNPT)

        If BarraBotones.PermissionsForm.ContainsKey(120) Then 'si tiene permiso de asignar paquete
            actions.Add(eAcciones.AssignPackageAndProductionLine)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(121) Then 'si tiene permiso de quitar paquete
            actions.Add(eAcciones.RemovePackageAndProductionLine)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(122) Then 'si tiene permiso de procesar central de mezclas
            actions.Add(eAcciones.ProcessMixingStation)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(123) Then 'si tiene permiso de enviar farmacia
            actions.Add(eAcciones.SendPharmacy)
        End If

        actions.Add(eAcciones.PharmacotherapeuticProfile)
        actions.Add(eAcciones.Annular)

        IndigoGridView1.SetListAcction(INDviewConfirmUnitDose, actions)

        Dim colActions = INDviewConfirmUnitDose.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
        If colActions IsNot Nothing Then colActions.Width = 100
    End Sub

    ''' <summary>
    ''' Abre el modal el plan de produccion NPT
    ''' </summary>
    Private Sub OpenFormProductionPlanNPT()
        Dim items = SelectedItems

        Dim frm As New FrmPopupNPTProductionPlan()
        frm.ItemsToProcess = items(0)
        Dim transparent As New FrmTransparent(frm, False)
        Me.Cursor = Windows.Forms.Cursors.Default
        If transparent.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            BeginReloadDatasource()
        End If
    End Sub

    ''' <summary>
    ''' Abre el modal para asignar el paquete
    ''' </summary>
    Private Sub OpenFormAssignPackage()
        Dim items = SelectedItems

        If items.Any(Function(m) m.PackageId > 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Existen items seleccionados que ya tienen un paquete asignado"
            Exit Sub
        End If

        If items Is Nothing OrElse Not items.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar el item para la asignación de paquete"
            Exit Sub
        End If

        Using formulario As New FrmAssignPackage()
            AddHandler formulario.AddPackageArgs, AddressOf ReturnAddEventArgs
            formulario.ViewListDashboardConfirmationUnitDoseXpo = items(0)
            formulario.ItemsToAssign = items
            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.8
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.8
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Retorna el modal de asignar paquete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddEventArgs(sender As Object, e As EventArgs)
        BeginReloadDatasource()
    End Sub

    ''' <summary>
    ''' Abre el modal para ver el paquete
    ''' </summary>
    Private Sub OpenFormShowPackage()

        Dim info = CType(INDviewConfirmUnitDose.GetFocusedRow(), ViewListDashboardConfirmationUnitDoseXpo)
        If info.PackageId = Nothing OrElse info.PackageId = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El item seleccionado no tiene un paquete asignado"
            Exit Sub
        End If

        Using formulario As New FrmShowPackageAssociated()
            formulario.PackageId = info.PackageId
            formulario.PackagePersonalizedId = info.PackagePersonalizedId
            formulario.Size = New System.Drawing.Size(1000, 600)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using

    End Sub

    ''' <summary>
    ''' Abre el modal para ver el detalle de aplicación
    ''' </summary>
    Private Sub OpenFormApplicationDetail()
        Using formulario As New FrmDetailApplication()
            AddHandler formulario.ReloadPrincipalGridArgs, AddressOf ReturnAddEventArgs
            formulario.ViewListDashboardConfirmationUnitDoseXpo = CType(INDviewConfirmUnitDose.GetFocusedRow(), ViewListDashboardConfirmationUnitDoseXpo)
            formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 600, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - 500)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Elimina los registros
    ''' </summary>
    Private Async Sub DeleteConfirmationUnitDoses()
        Dim listSelected = (From x In INDviewConfirmUnitDose.GetSelectedRows() Select DirectCast(INDviewConfirmUnitDose.GetRow(x), ViewListDashboardConfirmationUnitDoseXpo)).ToList()

        If listSelected Is Nothing OrElse listSelected.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se han seleccionado items para quitar los paquetes"
            Exit Sub
        End If

        If (From x In listSelected Where x.PackageId = Nothing OrElse x.PackageId = 0 Select x).Count > 0 Then
            Dim message As New StringBuilder
            For Each item In (From x In listSelected Where x.PackageId = Nothing OrElse x.PackageId = 0 Select x).ToList()
                message.AppendLine("El medicamento " + item.ServiceDescription + " no tiene un paquete asignado")
            Next
            Mensaje(EeventViewerImages.Advertencia) = message.ToString()
            Exit Sub
        End If

        Try
            If MessageIndigo.Show("Desea quitar los paquetes?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            AsyncLoader(True)
            Dim listIds = (From x In listSelected Select x.ConfirmationUnitDoseId).ToList()
            If listIds IsNot Nothing AndAlso listIds.Count > 0 Then
                Using model As New MDashboardConfirmationUnitDose(Me.Tag)
                    Dim result = Await model.DeleteConfirmationUnitDose(listIds, indigo.TransactionalContainer)
                    If result.StateResult = False Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                        AsyncLoader(False)
                        Exit Sub
                    End If

                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Informacion) = "Se quitaron los paquetes correctamente"
                    BeginReloadDatasource()
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardQuoted_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'SetActionsColumns()
        Me.ToolBar.Hide()
        Presenter = New PDashboardConfirmationUnitDose()
        SetActionsColumns()
        INDsleCM.Font = New Font("Segoe UI", 28, FontStyle.Regular)


        Dim ListItems As New List(Of Tuple(Of Byte, String))
        ListItems.Add(New Tuple(Of Byte, String)(1, "Seguro"))
        ListItems.Add(New Tuple(Of Byte, String)(2, "Inseguro"))
        INDRptSleSafeStatus.DataSource = ListItems

    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardQuoted_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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
            INDsleCM.Properties.DataSource = Presenter.ListCMByUserInstant()
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
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Select Case (sender.Tag.ToString)
            Case "ProductionPlanNPT"
                OpenFormProductionPlanNPT()
            Case "AssignPackageAndProductionLine", "Asignar Paquete y Línea de Produccíon"
                OpenFormAssignPackage()
            Case "PackageDetail"
                OpenFormShowPackage()
            Case "RemovePackageAndProductionLine", "Quitar Paquete y Línea de Produccíon"
                DeleteConfirmationUnitDoses()
            Case "ProcessMixingStation", "Procesar Central Mezclas"
                ProcessMixingStation()
            Case "SendPharmacy", "Enviar Farmacia"
                SendPharmacy()
            Case "Annular"
                Annular()
            Case "PharmacotherapeuticProfile"
                PharmacotherapeuticProfile()
        End Select
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

    Private Sub INDSbRefresh_Click(sender As Object, e As EventArgs) Handles INDSbRefresh.Click
        BeginReloadDatasource()
    End Sub

    Private Sub INDviewConfirmUnitDose_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDviewConfirmUnitDose.CustomUnboundColumnData
        If e.Column.Name = ColIconStateHis.Name AndAlso e.IsGetData AndAlso {3, 4, 7}.Contains(e.Row.StatusHCPRESCRA) Then
            e.Value = GetImage(My.Resources.Alerta)
        ElseIf e.Column.Name = INDColPackageName.Name Then
            Dim row As ViewListDashboardConfirmationUnitDoseXpo = e.Row
            ''Se valida cuando es NPT, se mostrara el paquete asignado
            e.Value = If(row.MSClass = EUnitDoseTypeClass.ParenteralNutrition, row.PackageCode & " - " & row.PackageName, If(row.PackagePersonalizedId.HasValue, row.PackagePersonalizedCodeName, row.PackageDescription))
        End If
    End Sub

    Private Function GetImage(img As Image) As Byte()
        Return DevExpress.XtraEditors.Controls.ByteImageConverter.ToByteArray(img, ImageFormat.Gif)
    End Function

    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As Controls.QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        Dim items = SelectedItems
        Dim popUp = CType(sender, RepositoryItemPopupContainerEdit)
        e.Buttons.ToList().ForEach(Sub(i) i.Visible = False)

        If Not INDviewConfirmUnitDose.GetSelectedRows().Contains(INDviewConfirmUnitDose.FocusedRowHandle) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un item para ejecutar alguna acción"
            popUp.PopupControl.MinimumSize = New Size(200, 0)
            popUp.PopupControl.MaximumSize = New Size(200, 0)
            popUp.PopupControl.Size = New Size(200, 0)
            Return
        End If

        Dim packageDetail = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.PackageDetail)))
        Dim productionPlanNPT = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ProductionPlanNPT)))
        Dim removePackage = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.RemovePackageAndProductionLine)))
        Dim assignPackage = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.AssignPackageAndProductionLine)))
        Dim processMixingStation = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ProcessMixingStation)))
        Dim sendPharmacy = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.SendPharmacy)))
        Dim annular = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Annular)))
        Dim PharmacotherapeuticProfile = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.PharmacotherapeuticProfile)))

        Dim count As Integer = 0

        If items.Any(Function(x) x.SafeStatus = 2) Then
            sendPharmacy.Visible = True
            PharmacotherapeuticProfile.Visible = True
            count += 2
        Else
            If items.Any(Function(x) x.OriginOrder = 4) Then

                If items.Exists(Function(y) y.MSClass = EUnitDoseTypeClass.ParenteralNutrition AndAlso Not y.NPTVerified And y.ProductionLineId = 0) Then
                    productionPlanNPT.Visible = True
                    PharmacotherapeuticProfile.Visible = True
                    sendPharmacy.Visible = True
                    count += 3

                Else
                    If items.All(Function(m) m.ProductionLineId = 0) Then
                        assignPackage.Visible = True
                        count += 1
                    End If

                    If items.All(Function(x) x.ProductionLineId > 0) Then
                        packageDetail.Visible = True
                        processMixingStation.Visible = True
                        removePackage.Visible = True
                        count += 3
                    End If
                End If
            Else
                productionPlanNPT.Visible = False
                packageDetail.Visible = False
                removePackage.Visible = False
                assignPackage.Visible = False
                processMixingStation.Visible = True
                sendPharmacy.Visible = True
                PharmacotherapeuticProfile.Visible = True

                count += 3

                If items.All(Function(m) m.ProductionLineId > 0) Then
                    removePackage.Visible = True
                    count += 1
                End If

                If items.Count = 1 AndAlso items(0).ProductionLineId > 0 Then
                    packageDetail.Visible = True
                    count += 1
                End If

                If items.All(Function(m) m.ProductionLineId = 0) Then
                    assignPackage.Visible = True
                    count += 1
                End If

                If items.All(Function(m) m.SourceType = 1 AndAlso ({4, 7}.Contains(m.StatusHCPRESCRA) OrElse m.FECALTPAC.HasValue)) Then
                    annular.Visible = True
                    count += 1
                End If

                If items.Count = 1 Then
                    packageDetail.Enabled = True
                Else
                    packageDetail.Enabled = False
                End If

                Dim allowedStatuses() As Integer = {3, 4, 7}
                If items.Any(Function(m) allowedStatuses.Contains(m.StatusHCPRESCRA)) Then
                    annular.Visible = False
                    assignPackage.Visible = False
                    processMixingStation.Visible = False
                    count -= 3
                End If
            End If
        End If

        popUp.PopupControl.Size = New Size(200, 36 * count)
    End Sub

    ''' <summary>
    ''' items seleccionados
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property SelectedItems As List(Of ViewListDashboardConfirmationUnitDoseXpo)
        Get
            Return INDviewConfirmUnitDose.GetSelectedRows() _
                   .Where(Function(m) Not INDviewConfirmUnitDose.IsGroupRow(m)) _
                   .Select(Function(m) CType(INDviewConfirmUnitDose.GetRow(m), ViewListDashboardConfirmationUnitDoseXpo)) _
                   .ToList()
        End Get
    End Property

    ''' <summary>
    ''' popup menu showing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewConfirmUnitDose_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDviewConfirmUnitDose.PopupMenuShowing
        If e.HitInfo.InRow Then
            If Not INDviewConfirmUnitDose.GetSelectedRows().Contains(e.HitInfo.RowHandle) Then
                IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(i) i.Visibility = DevExpress.XtraBars.BarItemVisibility.Never)
                Return
            End If

            Dim items = SelectedItems
            Dim packageDetail = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.PackageDetail)))
            Dim productionPlanNPT = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ProductionPlanNPT)))
            Dim removePackage = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.RemovePackageAndProductionLine)))
            Dim assignPackage = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.AssignPackageAndProductionLine)))
            Dim processMixingStation = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.ProcessMixingStation)))
            Dim sendPharmacy = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.SendPharmacy)))
            Dim annular = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Annular)))
            Dim PharmacotherapeuticProfile = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.PharmacotherapeuticProfile)))

            If items.Any(Function(m) m.SafeStatus = 2) Then
                sendPharmacy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                PharmacotherapeuticProfile.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                packageDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                removePackage.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                assignPackage.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                processMixingStation.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                annular.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                Exit Sub
            End If

            If items.Any(Function(x) x.OriginOrder = 4) Then
                IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(i) i.Visibility = DevExpress.XtraBars.BarItemVisibility.Never)

                If items.Any(Function(y) Not y.NPTVerified And y.ProductionLineId = 0) Then
                    productionPlanNPT.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    PharmacotherapeuticProfile.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    sendPharmacy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Exit Sub
                End If

                If items.All(Function(m) m.ProductionLineId = 0) Then
                    assignPackage.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                If items.All(Function(j) j.ProductionLineId > 0) Then
                    packageDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    removePackage.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    processMixingStation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

            Else
                productionPlanNPT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                packageDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                removePackage.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                assignPackage.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                processMixingStation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                sendPharmacy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                PharmacotherapeuticProfile.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                annular.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

                If items.All(Function(m) m.ProductionLineId > 0) Then
                    removePackage.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                If items.Count = 1 AndAlso items(0).ProductionLineId > 0 Then
                    packageDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                If items.All(Function(m) m.ProductionLineId = 0) Then
                    assignPackage.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                If items.All(Function(m) m.SourceType = 1 AndAlso ({4, 7}.Contains(m.StatusHCPRESCRA) OrElse m.FECALTPAC.HasValue)) Then
                    annular.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
                ''Estados: 3-Tratamiento descontinuado; 4-Tratamiento suspendido; 7-Tratamiento terminado por salida del paciente
                Dim allowedStatuses() As Integer = {3, 4, 7}
                If items.Any(Function(m) allowedStatuses.Contains(m.StatusHCPRESCRA)) Then
                    annular.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    assignPackage.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    processMixingStation.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                End If
            End If
        End If
    End Sub




    ''' <summary>
    ''' Se valida si se selecciona masivamente, se escoja la primera fila y se filtre por las coincidencias
    ''' de lo contrario que realize la validacion individual.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewConfirmUnitDose_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewConfirmUnitDose.SelectionChanged
        If _isProcessing Then Return
        _isProcessing = True
        Try
            Dim view As GridView = CType(sender, GridView)
            If e.Action = CollectionChangeAction.Refresh Then
                view.ClearSelection()
                Dim referenceItem = view.GetRow(0)
                If referenceItem IsNot Nothing Then
                    If _isFirstSelection Then
                        _isFirstSelection = False
                        For i As Integer = 0 To view.RowCount - 1
                            Dim currentRow As ViewListDashboardConfirmationUnitDoseXpo = view.GetRow(i)
                            If currentRow IsNot Nothing AndAlso
                           currentRow.ProductionLineId = referenceItem.ProductionLineId AndAlso
                           currentRow.ServiceName = referenceItem.ServiceName AndAlso
                           currentRow.Dosage = referenceItem.Dosage AndAlso
                           currentRow.UnitDoseTypeId = referenceItem.UnitDoseTypeId AndAlso
                           currentRow.SourceName = referenceItem.SourceName Then
                                view.SelectRow(i)
                                If Not _validRows.Contains(i) Then
                                    _validRows.Add(i)
                                End If
                            End If
                        Next
                    Else
                        view.ClearSelection()
                        _validRows.Clear()
                        _isFirstSelection = True
                    End If
                End If
            ElseIf e.Action = CollectionChangeAction.Add Then
                Dim row As ViewListDashboardConfirmationUnitDoseXpo = view.GetRow(e.ControllerRow)
                Dim items = SelectedItems
                If items IsNot Nothing AndAlso items.Any(Function(m) m.Id <> row.Id) Then
                    Dim item0 = items.FirstOrDefault(Function(m) m.Id <> row.Id)
                    If items.Any(Function(m) m.ProductionLineId <= 0) AndAlso (item0.ProductionLineId <> row.ProductionLineId _
                    OrElse item0.ServiceName <> row.ServiceName _
                    OrElse item0.Dosage <> row.Dosage _
                    OrElse item0.UnitDoseTypeId <> row.UnitDoseTypeId _
                    OrElse item0.SourceName <> row.SourceName) Then
                        UpdateSelectedGroups(view, e.ControllerRow)
                        view.UnselectRow(e.ControllerRow)
                    Else
                        UpdateSelectedGroups(view, e.ControllerRow)
                        If Not _validRows.Contains(e.ControllerRow) Then
                            _validRows.Add(e.ControllerRow)
                        End If
                    End If
                Else
                    UpdateSelectedGroups(view, e.ControllerRow)
                    If Not _validRows.Contains(e.ControllerRow) Then
                        _validRows.Add(e.ControllerRow)
                    End If
                End If
            ElseIf e.Action = CollectionChangeAction.Remove Then
                If _validRows.Contains(e.ControllerRow) Then
                    _validRows.Remove(e.ControllerRow)
                End If
            End If
        Finally
            _isProcessing = False
        End Try

        RefreshTotals()
    End Sub

    Private Sub RemoveInvalidRows(view As GridView)
        Dim rows = view.GetSelectedRows()

        For Each roww In rows
            If Not _validRows.Contains(roww) Then
                RemoveHandler view.SelectionChanged, AddressOf INDviewConfirmUnitDose_SelectionChanged
                view.UnselectRow(roww)
                AddHandler view.SelectionChanged, AddressOf INDviewConfirmUnitDose_SelectionChanged
            End If
        Next
    End Sub

    Private Sub UpdateSelectedGroups(view As GridView, controllerRow As Integer)
        If view.IsGroupRow(controllerRow) Then
            If view.IsRowSelected(controllerRow) Then
                SelectedGroupRowsCount += 1
            Else
                SelectedGroupRowsCount -= 1
            End If

            If SelectedGroupRowsCount < 0 Then
                SelectedGroupRowsCount = 0
            End If
        End If
        view.InvalidateFooter()
    End Sub

    Private Sub INDgcConfirmUnitDose_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcConfirmUnitDose.DataSourceChanged
        RefreshTotals()
    End Sub

    Private SelectedGroupRowsCount As Integer = 0

    Private Sub RefreshTotals()
        If INDgcConfirmUnitDose.DataSource Is Nothing Then Return

        'SelectedGroupRowsCount
        INDLblTotal.Text = INDviewConfirmUnitDose.SelectedRowsCount - (From x In INDviewConfirmUnitDose.GetSelectedRows() Where INDviewConfirmUnitDose.IsGroupRow(x) Select x).Count()
        INDLblTotalVisualizations.Text = INDviewConfirmUnitDose.DataRowCount
        INDLblTotalAdecuations.Text = CType(INDviewConfirmUnitDose.DataSource, IList).Count
    End Sub

    Private Sub INDviewConfirmUnitDose_ColumnFilterChanged(sender As Object, e As EventArgs) Handles INDviewConfirmUnitDose.ColumnFilterChanged
        RefreshTotals()
    End Sub

    Private Sub ToolTipController1_GetActiveObjectInfo(sender As Object, e As DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs) Handles ToolTipController1.GetActiveObjectInfo
        If Not e.SelectedControl Is INDgcConfirmUnitDose Then Return

        Dim info As ToolTipControlInfo = Nothing
        Dim view As GridView = INDgcConfirmUnitDose.GetViewAt(e.ControlMousePosition)

        If view Is Nothing Then Return

        Dim hi As GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)
        Dim row = view.GetRow(hi.RowHandle)
        Dim text As String = String.Empty

        If hi.Column IsNot Nothing AndAlso hi.Column.Name = ColIconStateHis.Name Then
            Dim item = DirectCast(row, ViewListDashboardConfirmationUnitDoseXpo)
            text = item?.StatusNameHCPRESCRA
        Else
            text = CType(view.GetRowCellValue(hi.RowHandle, hi.Column), String)
        End If

        info = New ToolTipControlInfo(row, text)
        If Not info Is Nothing Then e.Info = info
    End Sub

    Private Async Sub INDRptSleSafeStatus_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptSleSafeStatus.EditValueChanged
        Dim ItemSelect = CType(INDviewConfirmUnitDose.GetFocusedRow(), ViewListDashboardConfirmationUnitDoseXpo)
        Dim ea = CType(e, DevExpress.XtraEditors.Controls.ChangingEventArgs)

        If ItemSelect.Id IsNot Nothing AndAlso ea.NewValue <> ea.OldValue Then
            AsyncLoader(True)
            Try
                Using model As New MDashboardConfirmationUnitDose(Me.Tag.ToString())
                    Dim item = New ConfirmationUnitDoseValidations With {
                        .Id = ItemSelect.Id,
                        .SafeStatus = ea.NewValue
                    }
                    Dim result = Await model.SetSafeStatus(New List(Of ConfirmationUnitDoseValidations) From {item})
                    If Not result.StateResult Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                End Using
            Catch ex As Exception
                Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                Me.AsyncLoader(False)
                BeginReloadDatasource()
            End Try
        End If

    End Sub

#End Region

#Region "ShowingEditor"

    ''' <summary>
    ''' Cancela la edicion si ya tiene asignado un paquete y linea de produccion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewConfirmUnitDose_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDviewConfirmUnitDose.ShowingEditor
        Dim view = TryCast(sender, GridView)
        If view.FocusedColumn.FieldName = "SafeStatus" Then
            Dim row = TryCast(view.GetRow(view.FocusedRowHandle), ViewListDashboardConfirmationUnitDoseXpo)
            If row IsNot Nothing AndAlso row.ProductionLineId <> 0 Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#End Region

End Class