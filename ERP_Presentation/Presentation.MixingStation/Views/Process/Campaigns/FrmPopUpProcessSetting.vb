'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Giovanny Plazas Lozano
' Created          : 15/09/2021
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
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
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

Public Class FrmProcessSetting

#Region "Variables"

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Configuracion contiene resultados de Inventarios
    ''' </summary>
    Dim _settingInventory As SettingInventory

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PCampaigns

    ''' <summary>
    ''' Obtiene los permisos del usuario para el formulario principal
    ''' </summary>
    Public PermissionsForm As Dictionary(Of Integer, String)

    ''' <summary>
    ''' almacena el Id de detalle de la campaña
    ''' </summary>
    Public CampaignDetailId As Integer

    ''' <summary>
    ''' Variable que almacena el estado de la campaña
    ''' </summary>
    Public CampaignStatus As Byte?

    ''' <summary>
    ''' varaible del id de la central de mezclas
    ''' </summary>
    Public CMConfigurationId As Integer

    ''' <summary>
    ''' Listado para almacenar los registros
    ''' </summary>
    Private ListViewProcessSettingXpo As List(Of ViewProcessSettingXpo)

#End Region

#Region "Events"

#Region "Event"

    ''' <summary>
    ''' Evento para cargar nuevamente la campaña
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ReloadCampaignArgs(sender As Object, e As EventArgs)

#End Region

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

    ''' <summary>
    ''' Establece el texto de la ventan
    ''' </summary>
    Public WriteOnly Property SetTitleWindow() As String
        Set(value As String)
            Me.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Establce el texto del grupo
    ''' </summary>
    Public WriteOnly Property SetLabelGroup() As String
        Set(value As String)
            INDlygDetails.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Formulario padre, donde se encuentra alojado el control
    ''' </summary>
    Public Property FormOwner As ICampaign

#End Region

#Region "Methods"

    ''' <summary>
    ''' Indica si se muestra el progressbar
    ''' </summary>
    ''' <param name="isAsyncOperation">Valor que indica si se esta realizando una operación asíncrona</param>
    Private Sub IsAsyncOperation(Optional ByVal isAsyncOperation As Boolean = True)
        If isAsyncOperation Then
            INDlygDetails.Enabled = False
            INDlyItemProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlygDetails.Enabled = True
            INDlyItemProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Consulta el paquete
    ''' </summary>
    Private Sub GetDetail()
        INDgcDetails.DataSource = Nothing
        INDviewMaster.ShowLoadingPanel()
        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Try
                                      Dim result = Presenter.ListViewProcessSetting(CampaignDetailId, 0)
                                      ListViewProcessSettingXpo = result
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcDetails.SafeInvoke(Sub()
                                                                      INDviewMaster.HideLoadingPanel()
                                                                      INDgcDetails.DataSource = result
                                                                  End Sub)
                                      End If
                                  Catch ex As Exception
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcDetails.SafeInvoke(Sub()
                                                                      INDviewMaster.HideLoadingPanel()
                                                                      Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                  End Sub)
                                      End If
                                  End Try
                              End Sub, tokenAsync.Token)
    End Sub

    ''' <summary>
    ''' asignacion de valores de la entidad campaingDetailValidation a QuantityRemaining
    ''' </summary>
    ''' <param name="ItemSelected"></param>
    ''' <returns></returns>
    Private Async Function AssigningValues(ItemSelected As ViewProcessSettingXpo) As Task(Of List(Of QuantityRemaining))
        Try
            Dim ListQuantityRemaining = New List(Of QuantityRemaining)

            Using Model As New MCampaign("")
                INDviewMaster.ShowLoadingPanel()
                Dim Result = Await Model.GetCampaignDetailValidationByCampaignDetailId(ItemSelected.CampaignDetailId)
                INDviewMaster.HideLoadingPanel()

                If Result Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "La consulta no brindo ningún resultado"
                    Return ListQuantityRemaining
                End If
                If Result?.ObjectEmbbeded?.Count = 0 OrElse Result.StateResult = False Then
                    If Not String.IsNullOrEmpty(Result.Message) Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                    Return ListQuantityRemaining
                End If

                Result.ObjectEmbbeded = (From p In Result.ObjectEmbbeded Where p.ProductId = ItemSelected.ProductId Select p).ToList()

                If Result?.ObjectEmbbeded?.Any(Function(s) s.RemnantWareHouseId Is Nothing) Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se encuentra parametrizado el almacén de remanentes"
                    Return ListQuantityRemaining
                End If

                Result.ObjectEmbbeded.ForEach(Sub(x As CampaignDetailValidation)
                                                  Dim NewQuantityRemaining = New QuantityRemaining
                                                  With NewQuantityRemaining
                                                      .ProductId = x.ProductId
                                                      .ProductFullName = x.ProductFullName
                                                      .BatchSerialId = x.BatchSerialId
                                                      .BatchSerialCode = x.BatchSerialCode
                                                      .ExpirationDate = x.ExpirationDate
                                                      .CampaignDetailId = x.CampaignDetailId
                                                      .DeliveredQuantity = x.DeliveredQuantityUnitMeasurement
                                                      .UnitMeasurementId = ItemSelected.UnitMeasurementId
                                                      .Status = 1
                                                      .Quantity = 0
                                                      .WareHouseId = x.RemnantWareHouseId
                                                      .RemnantFullName = x.RemnantFullName
                                                  End With
                                                  ListQuantityRemaining.Add(NewQuantityRemaining)
                                              End Sub)
            End Using
            Return ListQuantityRemaining
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
            Return New List(Of QuantityRemaining)
        End Try
    End Function

    ''' <summary>
    ''' accion ejecutada al oprimir ok en el popup
    ''' </summary>
    ''' <param name="senser"></param>
    ''' <param name="e"></param>
    Private Async Sub ReturnQuantityRemainingEventArgs(senser As Object, e As GetQuantityRemainingEventArgs, value As Boolean, Optional FromCampaignDetailId As Integer? = Nothing)
        Try
            Dim ListQuantityRemaining = e.ListItemDetails

            If ListQuantityRemaining Is Nothing OrElse ListQuantityRemaining.Count = 0 Then
                Exit Sub
            End If

            Using Model As New MCampaign("")
                INDviewMaster.ShowLoadingPanel()
                Dim Result = New Domain.Base.Entities.ActionResult
                If value Then
                    Result = Await Model.SaveQuantityRemaining(ListQuantityRemaining)
                Else
                    Result = Await Model.RegisterHarnessed(ListQuantityRemaining, FromCampaignDetailId)
                End If
                INDviewMaster.HideLoadingPanel()
                If Result Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "La acción no produjo ningún resultado"
                    Exit Sub
                End If
                If Result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    Exit Sub
                End If
                GetDetail()
                Mensaje(EeventViewerImages.Informacion) = "Se realizo con exito el proceso"
            End Using

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Consulta a la tabla QuantityRemaining para el aprovechamiento
    ''' </summary>
    ''' <param name="ItemSelected"></param>
    ''' <returns></returns>
    Private Async Function AssigningValuesQRtoH(ItemSelected As ViewProcessSettingXpo, _cMConfigurationId As Integer) As Task(Of List(Of QuantityRemaining))
        Try
            Dim ListQuantityRemaining = New List(Of QuantityRemaining)

            Using Model As New MCampaign("")
                INDviewMaster.ShowLoadingPanel()
                Dim Result = Await Model.GetQuantityRemainingByMixingStation(_cMConfigurationId)
                INDviewMaster.HideLoadingPanel()

                If Result Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "La consulta no brindo ningún resultado"
                    Return ListQuantityRemaining
                End If
                If Result.ObjectEmbbeded Is Nothing OrElse Result.ObjectEmbbeded.Count = 0 OrElse Result.StateResult = False Then
                    If Not String.IsNullOrEmpty(Result.Message) Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                    Return ListQuantityRemaining
                End If
                ListQuantityRemaining = Result.ObjectEmbbeded.Where(Function(x) x.ProductId = ItemSelected.ProductId And x.Balance > 0).ToList()

                Return ListQuantityRemaining
            End Using
            Return ListQuantityRemaining
        Catch ex As Exception
            Return New List(Of QuantityRemaining)
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ItemSelected"></param>
    ''' <param name="ListQuantityRemaining"></param>
    ''' <param name="value">true : sobrante , false : aprovechamiento</param>
    ''' <param name="FromCampaignDetailId"></param>
    Private Sub OpenPopUp(ItemSelected As ViewProcessSettingXpo, ListQuantityRemaining As List(Of QuantityRemaining), value As Boolean, remainderClass As RemainderClass, Optional FromCampaignDetailId As Integer? = Nothing)
        Using formulario As New PopupQuantityRemaining(value, IIf(FromCampaignDetailId Is Nothing, ItemSelected.CampaignDetailId, FromCampaignDetailId))
            formulario.Text = IIf(value, "Ingreso de remanentes", "Remanente utilizado en campaña")
            formulario.Remanent = value
            formulario.Product = ItemSelected.ProductCodeName
            formulario.Title = String.Format("{0} ({1})", ItemSelected.ProductCodeName, ItemSelected.UnitMeasurement)
            formulario.INDLabelRemnantWareHouse.Text = $"Almacén : {ListQuantityRemaining.FirstOrDefault.RemnantFullName}"
            formulario.RawMaterialQuantityBalance = ItemSelected.RawMaterialQuantityBalance
            formulario.ShowLoadingGrid()
            formulario.RemainderClass = remainderClass
            formulario.DataSource = ListQuantityRemaining
            formulario.HideLoadingGrid()
            AddHandler formulario.GetItemsResult, AddressOf ReturnQuantityRemainingEventArgs
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDetailPatients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PCampaigns()
        IndigoGridView1.SetListAcction(INDviewMaster, {
                                       eAcciones.Harnessed,
                                       eAcciones.QuantityRemaining,
                                       eAcciones.IndirectRawMaterial
                                       }.ToList())

        IndigoGridView1.RepositoryItemPopupContainerEdit.PopupControl.ResetAutoSizeMode()
        INDviewMaster.Columns.ColumnByName("colActions").Caption = "Acciones"
        INDviewMaster.Columns.ColumnByName("colActions").Width = 100
        GetDetail()

    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se ejecuta al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDetailPatients_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento para cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDetailPatients_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub

#End Region

#Region "MenuContext"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case sender.Tag.ToString()
            Case "QuantityRemaining"
                INDBtnQuantityRemaining_ItemClick(Nothing, Nothing)
            Case "Harnessed"
                INDbtnHarnessed_ItemClick(Nothing, Nothing)
            Case "IndirectRawMaterial"
                INDBtnIndirectRawMaterial_ItemClick(Nothing, Nothing)
        End Select
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Accion sobre el boton remanentes 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnQuantityRemaining_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnQuantityRemaining.ItemClick

        Dim ItemSelected = (From x In INDviewMaster.GetSelectedRows() Where Not INDviewMaster.IsGroupRow(x) Select DirectCast(INDviewMaster.GetRow(x), ViewProcessSettingXpo)).FirstOrDefault()

        Dim ListQuantityRemaining = Await AssigningValues(ItemSelected)
        If ListQuantityRemaining.Count = 0 Then
            Exit Sub
        End If

        OpenPopUp(ItemSelected, ListQuantityRemaining, True, RemainderClass.ownRemnant)

    End Sub

    ''' <summary>
    ''' Click en aprovechamiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnHarnessed_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbtnHarnessed.ItemClick
        Dim ItemSelected = (From x In INDviewMaster.GetSelectedRows() Where Not INDviewMaster.IsGroupRow(x) Select DirectCast(INDviewMaster.GetRow(x), ViewProcessSettingXpo)).FirstOrDefault()

        Dim ListQuantityRemaining = Await AssigningValuesQRtoH(ItemSelected, Me.CMConfigurationId)
        If ListQuantityRemaining.Count = 0 Then
            Mensaje(EeventViewerImages.Informacion) = "El producto no tiene remanentes disponibles"
            Exit Sub
        End If

        OpenPopUp(ItemSelected, ListQuantityRemaining, False, RemainderClass.usedRemainder, ItemSelected.CampaignDetailId)

    End Sub

    ''' <summary>
    ''' Click en Materia Prima Indirecta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnIndirectRawMaterial_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBtnIndirectRawMaterial.ItemClick
        Dim listItemsSelected = ListViewProcessSettingXpo.Where(Function(x) x.IsSelected).ToList()

        If Not listItemsSelected.Any() Then
            MessageIndigo.Show("No se ha seleccionado ninguna fila", MessageType.Warning, Me.Text.Trim(), Botones.Ok)
            Exit Sub
        End If

        If MessageIndigo.Show("¿Está seguro de trasladar a Materia prima Indirecta?", MessageType.Question, Me.Text, Botones.SiNo) = DialogResult.No Then
            Exit Sub
        End If

        Dim indirectRawMaterialErrors As New List(Of String)()
        Dim listIndirectMPQuantity As New List(Of IndirectMPQuantity)()

        Try
            For Each item In listItemsSelected
                Dim movementType As eMovementType
                Dim quantity As Decimal

                If item.RawMaterialQuantityBalance > 0 Then
                    movementType = eMovementType.Output
                    quantity = item.RawMaterialQuantityBalance.Value
                ElseIf item.RawMaterialQuantityBalance < 0 Then
                    movementType = eMovementType.Input
                    quantity = item.RawMaterialQuantityBalance.Value * -1
                Else
                    If Not String.IsNullOrEmpty(item.ProductCodeName) Then
                        indirectRawMaterialErrors.Add($"El producto seleccionado no se le puede registrar materia prima indirecta {item.ProductCodeName}, la cantidad del saldo es 0 ")
                    End If
                    Continue For
                End If

                Dim indirectMPQuantity As New IndirectMPQuantity With {
                .CampaignDetailId = item.CampaignDetailId,
                .MovementType = movementType,
                .ProductId = item.ProductId,
                .Quantity = quantity,
                .BatchSerialId = item.BatchSerialId,
                .MeasurementUnitId = item.UnitMeasurementId,
                .Description = "Movimiento Materia Prima Indirecta"
            }
                listIndirectMPQuantity.Add(indirectMPQuantity)
            Next

            If indirectRawMaterialErrors.Any() Then
                ShowListErrorForm(indirectRawMaterialErrors)
                Exit Sub
            End If

            Using model As New MCampaign("")
                INDviewMaster.ShowLoadingPanel()
                Dim result = Await model.RegisterIndirectMPQuantityAsync(listIndirectMPQuantity)
                INDviewMaster.HideLoadingPanel()

                If result Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "La acción no ha producido ningún resultado"
                    Exit Sub
                End If

                If Not result.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = If(Not String.IsNullOrEmpty(result.Message), result.Message, "Hubo un error en el proceso")
                    Exit Sub
                End If

                Mensaje(EeventViewerImages.Informacion) = "La acción se ejecutó con éxito"
                GetDetail()
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Lista los errores en una tabla
    ''' </summary>
    ''' <param name="errors"></param>
    Private Sub ShowListErrorForm(errors As List(Of String))
        Using formulario As New FrmListErrors(errors)
            formulario.StartPosition = FormStartPosition.CenterParent
            formulario.Size = New System.Drawing.Size(950, 500)
            Dim transparent As New FrmTransparent(formulario, False)
            transparent.ShowDialog(Me)
        End Using
    End Sub
#End Region

#Region "PopMenuShowing"
    Private Sub INDviewMaster_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDviewMaster.PopupMenuShowing
        Try
            IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(i) i.Visibility = DevExpress.XtraBars.BarItemVisibility.Never)

            Dim ItemSelected = (From x In INDviewMaster.GetSelectedRows() Where Not INDviewMaster.IsGroupRow(x) Select DirectCast(INDviewMaster.GetRow(x), ViewProcessSettingXpo)).FirstOrDefault()

            Dim buttonIndirectRawMaterial = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.IndirectRawMaterial)))
            If buttonIndirectRawMaterial IsNot Nothing Then buttonIndirectRawMaterial.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            If ItemSelected IsNot Nothing Then
                If ItemSelected.AllowsRemanent Then
                    Dim buttonQuantityRemaining = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.QuantityRemaining)))
                    If buttonQuantityRemaining IsNot Nothing Then buttonQuantityRemaining.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                    Dim buttonHarnessed = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.Harnessed)))
                    If buttonHarnessed IsNot Nothing Then buttonHarnessed.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub


#End Region

#Region "QueryPopUpActionButtons"
    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        Dim popUp = CType(sender, RepositoryItemPopupContainerEdit)
        e.Buttons.ToList().ForEach(Sub(i) i.Visible = False)
        Dim ItemSelected = (From x In INDviewMaster.GetSelectedRows() Where Not INDviewMaster.IsGroupRow(x) Select DirectCast(INDviewMaster.GetRow(x), ViewProcessSettingXpo)).FirstOrDefault()
        Dim count As Integer = 0

        Dim buttonIndirectRawMaterial = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.IndirectRawMaterial)))


        If buttonIndirectRawMaterial IsNot Nothing Then
            buttonIndirectRawMaterial.Visible = True
            count += 1
        End If

        If ItemSelected.AllowsRemanent Then
            Dim buttonQuantityRemaining = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.QuantityRemaining)))
            If buttonQuantityRemaining IsNot Nothing Then
                buttonQuantityRemaining.Visible = True
                count += 1
            End If

            Dim buttonHarnessed = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Harnessed)))
            If buttonHarnessed IsNot Nothing Then
                buttonHarnessed.Visible = True
                count += 1
            End If
        End If
        popUp.PopupControl.Size = New System.Drawing.Size(200, 36 * count)
    End Sub

#End Region

#Region "MasterRowGet"
    ''' <summary>
    ''' Eventos para asignar la relacion y el dataSource la sub-rejilla 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewMaster_MasterRowGetChildList(sender As Object, e As MasterRowGetChildListEventArgs) Handles INDviewMaster.MasterRowGetChildList
        If e.ChildList Is Nothing Then
            Dim campaignKardexTmp = INDviewMaster.GetFocusedObject(Of ViewProcessSettingXpo)
            INDGvDetails.ShowLoadingPanel()
            e.ChildList = Presenter.ListViewProcessSetting(CampaignDetailId, campaignKardexTmp.ProductId, If(campaignKardexTmp.BatchSerialId Is Nothing, 0, campaignKardexTmp.BatchSerialId))
            INDGvDetails.HideLoadingPanel()
        End If
    End Sub

    Private Sub INDviewMaster_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles INDviewMaster.MasterRowGetRelationName
        e.RelationName = "Movimientos"
    End Sub

    Private Sub INDviewMaster_MasterRowGetRelationCount(sender As Object, e As MasterRowGetRelationCountEventArgs) Handles INDviewMaster.MasterRowGetRelationCount
        e.RelationCount = 1
    End Sub

#End Region


#Region "Selection Check"

    ''' <summary>
    ''' Método que se encarga de poner o no visible el check en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDRpCheckEdit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRpCheckEdit.EditValueChanging
        If e IsNot Nothing Then
            Dim row As ViewProcessSettingXpo = INDviewMaster.GetFocusedRow()
            If row IsNot Nothing Then
                If row.IsSelected Then
                    row.IsSelected = False
                Else
                    row.IsSelected = True
                    End If
                VisibleCheck()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que se encarga de poner o no visible el check de la cabecera en la rejilla
    ''' </summary>
    Private Sub VisibleCheck()
        If ListViewProcessSettingXpo?.Any() Then
            Dim count As Integer = (From l In ListViewProcessSettingXpo Where l.IsSelected = True).Count
            Dim countValidate As Integer = (From l In ListViewProcessSettingXpo).Count

            If count = countValidate Then
                INDColSelItem.Image = My.Resources.check
            Else
                INDColSelItem.Image = My.Resources.undcheck
            End If

            ''INDColSelItem.Image = If(ListViewProcessSettingXpo.All(Function(item) item.IsSelected AndAlso item.ItemType <> 3 AndAlso item.ItemType <> 4), My.Resources.check, My.Resources.undcheck)
        End If
    End Sub

    ''' <summary>
    ''' Método que se encarga de seleccionar los item visibles (con o sin filtro al dar doble click en el check de la cabecera columna Sel.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDgcDetails_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcDetails.MouseDoubleClick
        If ListViewProcessSettingXpo?.Any() Then
            Dim hitPoint = INDviewMaster.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDColSelItem") Then
                    Dim listFilterXpCollection = INDviewMaster.DataController.GetAllFilteredAndSortedRows()
                    Dim count As Integer = (From l In listFilterXpCollection Where l.IsSelected = True).Count
                    Dim countValidate As Integer = (From l In listFilterXpCollection).Count


                    If count = countValidate Then
                        For Each item In listFilterXpCollection
                            item.IsSelected = False
                        Next
                        Me.INDColSelItem.Image = My.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.IsSelected = True
                        Next
                        count = (From l In listFilterXpCollection Where l.IsSelected = True).Count
                        If count = countValidate Then
                            Me.INDColSelItem.Image = My.Resources.check
                        End If
                    End If
                    INDgcDetails.RefreshDataSource()
                    INDgcDetails.Invalidate()
                End If
            End If
        End If
    End Sub

#End Region

#End Region

End Class