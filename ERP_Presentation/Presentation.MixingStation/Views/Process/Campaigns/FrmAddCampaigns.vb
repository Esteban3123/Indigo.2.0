'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/03/2021
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
Imports System.Dynamic
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmAddCampaigns

#Region "Variables"

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenPatientsAsync As CancellationTokenSource

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PCampaigns

    ''' <summary>
    ''' Obtiene los permisos del usuario para el formulario principal
    ''' </summary>
    Public PermissionsForm As Dictionary(Of Integer, String)

    ''' <summary>
    ''' Id de la central de mezcla escogida en el form principal
    ''' </summary>
    Public CMConfigurationId As Integer

    ''' <summary>
    ''' Id de la cabecera de la campaña, viene desde el form principal
    ''' </summary>
    Public CampaignId As Integer

    ''' <summary>
    ''' Entidad xpo que representa a la cabecera de la campaña
    ''' </summary>
    Public CampaignXpo As CampaignXpo

    ''' <summary>
    ''' Diccionario para almacenar los horarios del paciente
    ''' </summary>
    Private dictionaryPatients As Dictionary(Of String, List(Of ViewListDetailPatientsXpo))

#End Region

#Region "Event"

    ''' <summary>
    ''' Evento para cargar nuevamente la rejilla principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ReloadPrincipalGridArgs(sender As Object, e As EventArgs)

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
    ''' Consulta el paquete
    ''' </summary>
    Private Sub GetDetails()
        INDgcDetail.DataSource = Nothing
        INDviewDetail.ShowLoadingPanel()
        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Try
                                      Dim result = Presenter.ListViewListRequestsByCampaign(CMConfigurationId)
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcDetail.SafeInvoke(Sub()
                                                                     INDviewDetail.HideLoadingPanel()
                                                                     INDgcDetail.DataSource = result
                                                                     If result.Count = 0 Then
                                                                         Mensaje(EeventViewerImages.Informacion) = "No se encontraron solicitudes procesadas para poder continuar"
                                                                     End If
                                                                 End Sub)
                                      End If
                                  Catch ex As Exception
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcDetail.SafeInvoke(Sub()
                                                                     INDviewDetail.HideLoadingPanel()
                                                                     Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                 End Sub)
                                      End If
                                  End Try
                              End Sub, tokenAsync.Token)
    End Sub

    ''' <summary>
    ''' Consulta el paquete
    ''' </summary>
    Private Sub GetDetailPatients()
        Dim info = CType(INDviewDetail.GetFocusedRow(), ViewListRequestsByCampaignXpo)
        INDgcPatients.DataSource = Nothing
        INDviewPatients.ShowLoadingPanel()
        tokenPatientsAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Try
                                      Dim result As List(Of ViewListDetailPatientsXpo) = Nothing
                                      If Not dictionaryPatients.ContainsKey(info.Id) Then
                                          result = Presenter.ListViewListDetailPatients(info.StringIds, "1, 2, 3")
                                          dictionaryPatients.Add(info.Id, result)
                                      Else
                                          result = dictionaryPatients(info.Id)
                                      End If

                                      If Not tokenPatientsAsync.IsCancellationRequested Then
                                          INDgcPatients.SafeInvoke(Sub()
                                                                       INDviewPatients.HideLoadingPanel()
                                                                       INDgcPatients.DataSource = result
                                                                   End Sub)
                                      End If
                                  Catch ex As Exception
                                      If Not tokenPatientsAsync.IsCancellationRequested Then
                                          INDgcPatients.SafeInvoke(Sub()
                                                                       INDviewPatients.HideLoadingPanel()
                                                                       Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                   End Sub)
                                      End If
                                  End Try
                              End Sub, tokenPatientsAsync.Token)
    End Sub

    ''' <summary>
    ''' Procesa los registros para generar el cronograma
    ''' </summary>
    Private Sub GenerateCampaign()
        If CType(INDgcDetail.DataSource, List(Of ViewListRequestsByCampaignXpo)) Is Nothing OrElse CType(INDgcDetail.DataSource, List(Of ViewListRequestsByCampaignXpo)).Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay detalles para generar la campaña"
            Exit Sub
        End If

        If (From x In INDviewDetail.GetSelectedRows() Where Not INDviewDetail.IsGroupRow(x) Select DirectCast(INDviewDetail.GetRow(x), ViewListRequestsByCampaignXpo)).Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item para generar la campaña"
            Exit Sub
        End If

        Dim errors As New StringBuilder

        'Se obtienen los registros seleccionados
        Dim listItems = (From x In INDviewDetail.GetSelectedRows() Where Not INDviewDetail.IsGroupRow(x) Select DirectCast(INDviewDetail.GetRow(x), ViewListRequestsByCampaignXpo)).ToList()

        'Se valida que no puedan mezclarse solicitudes externas de contratos diferentes
        Dim ContractExternalClientId = listItems.Where(Function(x) x.ContractExternalClientsId IsNot Nothing And x.ManagesMaquila = True).Select(Function(i) i.ContractExternalClientsId).FirstOrDefault

        If ContractExternalClientId IsNot Nothing AndAlso (From x In listItems Where x.ContractExternalClientsId = ContractExternalClientId).Count <> listItems.Count Then
            errors.AppendLine("Existen restricciones para atender solicitudes de diferentes centros de atención Externo")
        End If

        'Se valida que no puedan mezclar solicitudes externas con internas
        If (From x In listItems Where x.ContractExternalClientsId IsNot Nothing).Count >= 1 AndAlso (From x In listItems Where x.ContractExternalClientsId IsNot Nothing).Count <> listItems.Count Then
            errors.AppendLine("No se pueden Mezclar Solicitudes Internas Con externas")
        End If

        'Se valida que hayan no hayan seleccionado diferentes lineas de producción
        Dim productionLineId = listItems(0).ProductionLineId
        If (From x In listItems Where x.ProductionLineId = productionLineId).Count <> listItems.Count Then
            errors.AppendLine("No se puede seleccionar solicitudes de diferentes líneas de producción")
        End If

        'Se valida que hayan no hayan seleccionado diferentes tipos de dosis unitarias
        Dim unitDoseTypeId = listItems(0).UnitDoseTypeId
        If (From x In listItems Where x.UnitDoseTypeId = unitDoseTypeId).Count <> listItems.Count Then
            errors.AppendLine("No se puede seleccionar solicitudes de diferentes tipos dosis unitaria")
        End If

        'Se valida que hayan seleccionado solicitudes con linea de producción asignada
        If (From x In listItems Where x.ProductionLineId = 0 OrElse x.ProductionLineId = Nothing).Count > 0 Then
            errors.AppendLine("Hay solicitudes seleccionadas sin línea de producción asignada")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If

        'Permite saber si se escogió YES en la pregunta siempre y cuando salga
        Dim selectedYesInQuestion As Boolean = False

        'Se valida si hay detalles y además que estos detalles tengan la misma linea de producción y tipo dosis unitaria, se realiza esto
        'para preguntarle al usuario si quiere asociar los items a una campaña existente o generar una campaña nueva
        If CampaignXpo IsNot Nothing AndAlso CampaignXpo.CampaignDetailXpo IsNot Nothing AndAlso CampaignXpo.CampaignDetailXpo.Count > 0 Then
            If (From x In CampaignXpo.CampaignDetailXpo Where x.ProductionLineId.Id = productionLineId AndAlso x.UnitDoseTypeId.Id = unitDoseTypeId _
                                                            AndAlso x.CampaignStatus = 1).Count > 0 Then
                If MessageIndigo.Show("¿Desea asociar las solicitudes a una campaña existente?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    selectedYesInQuestion = True
                End If
            End If
        End If

        If selectedYesInQuestion Then
            Using Formulario As New FrmSelectedCampaign()
                AddHandler Formulario.AddSelected, AddressOf ReturnAddSelectedEventArgs
                Formulario.ListCampaignDetail = (From x In CampaignXpo.CampaignDetailXpo Where x.ProductionLineId.Id = productionLineId AndAlso x.UnitDoseTypeId.Id = unitDoseTypeId AndAlso x.CampaignStatus = 1).ToList()
                Formulario.ListItems = listItems
                Formulario.ProductionLineId = productionLineId
                Formulario.UnitDoseTypeId = unitDoseTypeId
                Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Formulario.Width = 750
                Formulario.Height = 500
                Dim frm As New FrmTransparent(Formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                frm.ShowDialog(Me)
            End Using
        Else
            Guardar(listItems, productionLineId, unitDoseTypeId, 0)
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta al terminar el modal de selección
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub ReturnAddSelectedEventArgs(e As AddSelectedCampaign)
        Guardar(e.ListItems, e.ProductionLineId, e.UnitDoseTypeId, e.CampaignDetailId)
    End Sub

    ''' <summary>
    ''' Guarda los items
    ''' </summary>
    ''' <param name="listItems"></param>
    ''' <param name="productionLineId"></param>
    ''' <param name="unitDoseTypeId"></param>
    ''' <param name="campaignDetailId"></param>
    Private Async Sub Guardar(listItems As List(Of ViewListRequestsByCampaignXpo), productionLineId As Integer, unitDoseTypeId As Integer, campaignDetailId As Integer)
        Dim myListDetail = AssigningDetails(listItems)
        Dim myPackageDetail = AssigningPackageDetails(listItems)
        Dim args As Object = New ExpandoObject()
        args.Id = CampaignId
        args.CMConfigurationId = CMConfigurationId
        args.ProductionLineId = productionLineId
        args.UnitDoseTypeId = unitDoseTypeId
        args.CampaignDetailId = campaignDetailId
        args.Details = myListDetail

        Me.AsyncLoader(True)

        Try
            Using model As New MCampaign(Me.Tag.ToString())
                Dim result = Await model.SaveCampaignAndMixingStationDetailAsync(args, myPackageDetail)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message

                    RaiseEvent ReloadPrincipalGridArgs(Nothing, Nothing)
                    Me.Close()
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
    Private Function AssigningDetails(listItems As List(Of ViewListRequestsByCampaignXpo)) As ConcurrentBag(Of Object)
        Dim myListDetail As New ConcurrentBag(Of Object)()

        If listItems IsNot Nothing AndAlso listItems.Count > 0 Then
            For Each item In listItems
                Dim detail As Object = New ExpandoObject()
                detail.SourceType = item.RequestType
                detail.StringIds = item.StringIds
                detail.RequestMixingStationDetailId = item.RequestMixingStationDetailId
                detail.CampaingDetailOriginId = Nothing
                myListDetail.Add(detail)
            Next
        End If

        Return myListDetail
    End Function

    ''' <summary>
    ''' Asigna los valores a la entidad principal
    ''' </summary>
    Private Function AssigningPackageDetails(listItems As List(Of ViewListRequestsByCampaignXpo)) As List(Of RequestMixingStationDetail)
        Dim myPackageDetail = New List(Of RequestMixingStationDetail)()
        If listItems IsNot Nothing AndAlso listItems.Count > 0 Then
            listItems.ForEach(Sub(ls As ViewListRequestsByCampaignXpo)
                                  Dim packageDetail = New RequestMixingStationDetail
                                  packageDetail.PackageId = Nothing
                                  packageDetail.PackagePersonalizedId = Nothing
                                  With packageDetail
                                      .Id = ls.RequestMixingStationDetailId
                                      If ls.PackageId > 0 Then
                                          .PackageId = ls.PackageId
                                      End If
                                      If ls.PackagePersonalizedId > 0 Then
                                          .PackagePersonalizedId = ls.PackagePersonalizedId
                                      End If
                                      .Quantity = ls.RequestQuantity
                                      myPackageDetail.Add(packageDetail)
                                  End With
                              End Sub)
        End If
        Return myPackageDetail
    End Function

    ''' <summary>
    ''' Método que abre el form para asignar la linea de producción
    ''' </summary>
    Private Sub OpenFormAssignProductionLine()
        Dim infoItemSelected = CType(INDviewDetail.GetFocusedRow(), ViewListRequestsByCampaignXpo)

        If infoItemSelected.RequestType = 1 OrElse infoItemSelected.RequestType = 2 Then 'Si es oreden medica o solicitud externa paciente
            Dim listAnnulated = Presenter.ListViewListDetailPatients(infoItemSelected.StringIds, "3")
            If listAnnulated IsNot Nothing AndAlso listAnnulated.Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede cambiar la línea de producción porque el item seleccionado tiene pacientes en estado anulado"
                Exit Sub
            End If
        End If

        Using formulario As New FrmAssignProductionLine()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnAddEventArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 250
            formulario.ToolBar.Visible = False
            formulario.RequestMixingStationDetailId = infoItemSelected.RequestMixingStationDetailId
            formulario.RequestMixingStationId = infoItemSelected.RequestMixingStationId
            formulario.ProductionLineId = infoItemSelected.ProductionLineId
            formulario.ProductionLineCodeName = infoItemSelected.ProductionLineCodeName
            formulario.MixingStationId = CMConfigurationId
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Se ejecuta al realizar las acciones del modal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddEventArgs(sender As Object, e As EventArgs)
        GetDetails()
    End Sub

    ''' <summary>
    ''' Método que asigna la columna de acciones a las rejillas
    ''' </summary>
    Private Sub SetActionsColumns()
        IndigoGridView1.MoreInfoColunmns(INDviewDetail)

        If PermissionsForm IsNot Nothing AndAlso PermissionsForm.Count > 0 Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = Not PermissionsForm.ContainsKey(125)
            Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Procesar, "Generar Campaña")

            Dim ListActions As New List(Of eAcciones)

            If PermissionsForm.ContainsKey(124) Then 'si tiene permiso de asignar linea de producción
                ListActions.Add(eAcciones.ReassignProductionLine)
            End If

            If ListActions.Count > 0 Then 'si hay menu se asigna al gridview 
                IndigoGridView1.SetListAcction(INDviewDetail, ListActions)

                For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewDetail.Columns
                    If col.Name = "colactions" Then
                        col.Width = 100
                    End If
                Next
            End If
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddCampaigns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me.indigo = SessionValues.Instance
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True

        dictionaryPatients = New Dictionary(Of String, List(Of ViewListDetailPatientsXpo))
        SetActionsColumns()
        Presenter = New PCampaigns()
        GetDetails()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento para cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddCampaigns_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
        If tokenPatientsAsync IsNot Nothing Then
            tokenPatientsAsync.Cancel()
        End If
    End Sub

#End Region

#Region "KeyDown"

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se ejecuta al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddCampaigns_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim tagGrid As String = ""
        If sender.GetType() Is GetType(DevExpress.XtraEditors.SimpleButton) Or sender.GetType() Is GetType(DevExpress.XtraBars.BarButtonItem) Then
            tagGrid = sender.Tag.ToString()
        Else
            tagGrid = sender.Text
        End If
        Select Case tagGrid
            Case "ReassignProductionLine", "Reasignar linea de Producción"
                OpenFormAssignProductionLine()
        End Select
    End Sub

    '''' <summary>
    '''' Menu contextual para la rejilla de solicitudes
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    'Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
    '    Select Case (sender.Tag.ToString)
    '        Case "ReassignProductionLine", "Reasignar Línea Producción"
    '            OpenFormAssignProductionLine()
    '    End Select
    'End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Se dispara al abrir el popup de detalles de pacientes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepPcePatients_Popup(sender As Object, e As EventArgs) Handles INDrepPcePatients.Popup
        GetDetailPatients()
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Se dispara al cerrar el popup de detalles de pacientes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepPcePatients_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDrepPcePatients.CloseUp
        If tokenPatientsAsync IsNot Nothing Then
            tokenPatientsAsync.Cancel()
        End If
    End Sub

#End Region

#Region "ShowingEditor"

    ''' <summary>
    ''' Se dispara al pintar las columnas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewDetail_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDviewDetail.ShowingEditor
        Dim pMouse As System.Drawing.Point = INDgcDetail.PointToClient(Control.MousePosition)
        Dim hit = INDviewDetail.CalcHitInfo(pMouse)
        If hit.Column IsNot Nothing AndAlso hit.Column.Name.Equals("INDcolInfoPatients") Then
            Dim info As ViewListRequestsByCampaignXpo = INDviewDetail.GetFocusedRow()
            If info.RequestType = 1 OrElse info.RequestType = 2 Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#End Region

#Region "BarButton Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Click procesar
    ''' </summary>
    Private Sub BarraBotones_ClickProcesar() Handles BarraBotones.ClickProcesar
        GenerateCampaign()
    End Sub

#End Region

End Class