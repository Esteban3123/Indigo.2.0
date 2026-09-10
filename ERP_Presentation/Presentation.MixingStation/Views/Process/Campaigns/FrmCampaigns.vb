'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/03/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Drawing
Imports DevExpress.LookAndFeel
Imports DevExpress.Utils.Colors
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.MixingStation.MVP
Imports System.Linq
Imports Infrastructure.Data.Xpo
#End Region

Public Class FrmCampaigns
    Implements ICampaign

    Public Sub New()
        InitializeComponent()
        INDGvCampaingStatus.OptionsView.ShowAutoFilterRow = False
        INDGvCampaingStatus.OptionsFind.AlwaysVisible = False
    End Sub

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PCampaigns

    ''' <summary>
    ''' Id de la cabecera de la campaña, se obtiene cuando se selecciona la central de mezcla
    ''' </summary>
    Private CampaignId As Integer

    ''' <summary>
    ''' Entidad xpo que representa a la cabecera de la campaña
    ''' </summary>
    Private CampaignXpo As CampaignXpo

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna los permisos que el usuario tiene asignados en éste formulario
    ''' </summary>
    ''' <value>Diccionario de permisos del usuario</value>
    ''' <returns>El diccionario de permisos del usuario</returns>
    Public Property PermissionsForm As Dictionary(Of Integer, String) Implements ICampaign.PermissionsForm

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICampaign.Mensaje
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
    ''' Show/Hide loading del form
    ''' </summary>
    Public Property IsBusy As Boolean
        Get
            Return If(Me.LyciBusyIndicator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always, True, False)
        End Get
        Set(value As Boolean)
            INDsleCM.Enabled = Not value
            Me.LyciBusyIndicator.Visibility = If(value = True, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            If value Then
                'PnlProgressPanel.ImageHorzOffset = (System.Windows.Forms.Screen.FromControl(Me).WorkingArea.Width / 2) - 150
                'PnlProgressPanel.Visible = True
                'PnlProgressPanel.BringToFront()
            Else
                'PnlProgressPanel.Visible = False
                'PnlProgressPanel.SendToBack()
            End If
        End Set
    End Property
#End Region

#Region "DataSource"
    ''' <summary>
    ''' Filtro Estado Campaña
    ''' </summary>
    ''' <remarks></remarks>
    Private _campaignStatus As List(Of Tuple(Of Byte, String))

    Private ReadOnly Property CampaignStatus As List(Of Tuple(Of Byte, String))
        Get
            If _campaignStatus Is Nothing Then
                _campaignStatus = New List(Of Tuple(Of Byte, String))
                _campaignStatus.Add(New Tuple(Of Byte, String)(1, "Abierta"))
                _campaignStatus.Add(New Tuple(Of Byte, String)(2, "Cerrada"))
                _campaignStatus.Add(New Tuple(Of Byte, String)(3, "Bloqueada"))
                _campaignStatus.Add(New Tuple(Of Byte, String)(4, "Anulada"))
                _campaignStatus.Add(New Tuple(Of Byte, String)(5, "Procesada"))
                _campaignStatus.Add(New Tuple(Of Byte, String)(6, "Terminada"))
            End If
            Return _campaignStatus
        End Get
    End Property
#End Region

#Region "Methods"

    ''' <summary>
    ''' Abre el form de detalles de la solicitud
    ''' </summary>
    Private Sub OpenFormAddCampaign()
        If INDsleCM.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una central de mezcla"
            Exit Sub
        End If

        Using formulario As New FrmAddCampaigns()
            AddHandler formulario.ReloadPrincipalGridArgs, AddressOf ReturnAddEventArgs
            formulario.PermissionsForm = PermissionsForm
            formulario.CMConfigurationId = INDsleCM.EditValue
            formulario.CampaignId = CampaignId
            formulario.CampaignXpo = CampaignXpo
            formulario.WindowState = Windows.Forms.FormWindowState.Maximized
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.MinimizeBox = True
            formulario.MaximizeBox = True
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Se ejecuta al cerrar el form modal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddEventArgs(sender As Object, e As EventArgs)
        INDpanelCampaign.Controls.Clear()
        INDsleCM_EditValueChanged(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' Asigna las acciones del usuario
    ''' </summary>
    Private Sub SetListActions()
        Presenter.LoadPermissionsForm(Me.Tag)
        If PermissionsForm IsNot Nothing AndAlso PermissionsForm.Count > 0 Then
            MbtnAddCampaign.Visibility = If(PermissionsForm.ContainsKey(125), DevExpress.XtraBars.BarItemVisibility.Always, DevExpress.XtraBars.BarItemVisibility.Never)
        End If
    End Sub

    ''' <summary>
    ''' Actualiza el estado de una campaña
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="campaignDetailId"></param>
    Private Async Sub BeginReloadCampaignDetailStatis(sender As Object, campaignDetailId As Integer)
        If INDTvCampaigns.GetFocusedRow.GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then
            Exit Sub
        End If

        DirectCast(INDpanelCampaign.Controls(0), CtrCampaign).SetLoadingGrid(True)
        CampaignXpo = Await Task.Factory.StartNew(Function() Presenter.GetCampaignByCMConfigurationId(INDsleCM.EditValue))
        AddCampaignToPanel(campaignDetailId)
        INDTvCampaigns.HideFindPanel()
    End Sub

    ''' <summary>
    ''' Recarga las campañas seleccionadas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CtrFolio_RequiereReloadCampaignList(sender As Object, e As RequiereReloadCampaignListEventArgs)
        For Each campaignDetailId As Integer In e.ListCampaignDetailId
            Dim currentFolio As CtrCampaign = INDpanelCampaign.Controls(0)
            currentFolio.SetDatasourceAsync(campaignDetailId)
        Next
    End Sub
#End Region

#Region "Selector"
    Private _selectorCampaignStatus As SelectorCache = New SelectorCache("Item1", "Item2")

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvCampaingStatus.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCampaingStatus" Then
                e.Value = _selectorCampaignStatus.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvCampaingStatus.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCampaingStatus" Then
                selector = _selectorCampaignStatus
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

    Private Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleCampaingStatus.Closed
        Dim searchLookupEdit = TryCast(sender, SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleCampaingStatus" Then
            searchLookupEdit.Properties.NullText = _selectorCampaignStatus.ToString()
        End If
    End Sub
#End Region

#Region "ICrudBase"

    Public Sub Buscar() Implements ICrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Throw New NotImplementedException()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Se ejecuta al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmCampaigns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DdbMenu.StyleController = Nothing
        Me.DdbMenu.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.DdbMenu.LookAndFeel.UseDefaultLookAndFeel = False
        INDSleCampaingStatus.Properties.DataSource = CampaignStatus
        DefaultStatus({CampaignStatus(0), CampaignStatus(1), CampaignStatus(4)}.ToList())
        Presenter = New PCampaigns(Me)
        SetListActions()
        UpdateColors()

        SidePanel1.Width = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 300
    End Sub

    Private Sub DefaultStatus(Datasource As List(Of Tuple(Of Byte, String)))
        For Each x In Datasource
            _selectorCampaignStatus.SetValue(x)
            INDGvCampaingStatus.RefreshData()
        Next
        INDSleCampaingStatus.Properties.NullText = _selectorCampaignStatus.ToString()
    End Sub
#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Se ejecuta al desplegar el control de central de mezcla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCM_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCM.QueryPopUp
        If INDsleCM.Properties.DataSource Is Nothing Then
            INDsleCM.Properties.DataSource = New PDashboardConfirmationUnitDose().ListCMByUserInstant()
        End If
    End Sub



#End Region

#Region "EditValueChanged"

    'Private Sub SearchControl1_QueryIsSearchColumn(sender As Object, args As QueryIsSearchColumnEventArgs) _
    'Handles SearchControl1.QueryIsSearchColumn
    '    If args.FieldName <> "ShipCountry" Then
    '        args.IsSearchColumn = False
    '    End If
    'End Sub

    Private UnreadTextColor As Color

    Private Sub UpdateColors()
        UnreadTextColor = DXSkinColorHelper.GetDXSkinColor(DXSkinColors.FillColors.Primary, LookAndFeel)
        'Me.TileView2.Appearance.GroupText.ForeColor = UnreadTextColor
        Me.INDTvCampaigns.Appearance.ItemFocused.BackColor = Color.FromArgb(40, UnreadTextColor)
        Me.INDTvCampaigns.Appearance.ItemHovered.BackColor = Color.FromArgb(40, UnreadTextColor)
    End Sub

    Private _colors As List(Of (Integer, Color)) = {
        (1, ColorTranslator.FromHtml("#303F9F")),
        (2, ColorTranslator.FromHtml("#D32F2F")),
        (3, ColorTranslator.FromHtml("#455A64")),
        (4, ColorTranslator.FromHtml("#E64A19")),
        (5, ColorTranslator.FromHtml("#388E3C")),
        (6, ColorTranslator.FromHtml("#5D4037"))
    }.ToList()

    Private ReadOnly Property ColorState(state As Byte) As Color
        Get
            Return _colors.Find(Function(m) m.Item1 = state).Item2
        End Get
    End Property

    Private Sub tileView1_ItemCustomize(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs) Handles INDTvCampaigns.ItemCustomize
        Dim msg = TryCast(INDTvCampaigns.GetRow(e.RowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If msg Is Nothing Then
            Return
        End If

        Dim obj = TryCast(msg.OriginalRow, ViewCampaignMainInfoXpo)
        If obj Is Nothing Then
            Return
        End If

        e.Item("StateBar").Appearance.Normal.BackColor = ColorState(obj.CampaignStatus)

        If obj.CampaignStatus < 5 Then
            e.Item.GetElementByName("QFProductionTitle").TextVisible = False
            e.Item.GetElementByName("QFProductionValue").TextVisible = False
        End If

        If obj.CampaignStatus <> 6 Then
            e.Item.GetElementByName("QFQualityTitle").TextVisible = False
            e.Item.GetElementByName("QFQualityValue").TextVisible = False
        End If
    End Sub

    Private Sub INDTvCampaigns_CustomItemTemplate(sender As Object, e As DevExpress.XtraGrid.Views.Tile.TileViewCustomItemTemplateEventArgs) Handles INDTvCampaigns.CustomItemTemplate
        Dim msg = TryCast(INDTvCampaigns.GetRow(e.RowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If msg Is Nothing Then
            Return
        End If

        Dim obj = TryCast(msg.OriginalRow, ViewCampaignMainInfoXpo)
        If obj Is Nothing Then
            Return
        End If

    End Sub

    Private _oldCM As String
    Property _oldKeys As String
    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleCM_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCM.EditValueChanged
        _oldCM = INDsleCM.EditValue
        _oldKeys = _selectorCampaignStatus.GetKeys()

        If INDsleCM.EditValue IsNot Nothing Then
            Try
                INDGcCampaigns.DataSource = Nothing
                CampaignXpo = Nothing
                CampaignId = 0
                IsBusy = True
                INDpanelHeader.Visible = True
                INDpanelBody.Visible = False
                INDpanelNoData.Visible = True

                CampaignXpo = Await Task.Factory.StartNew(Function() Presenter.GetCampaignByCMConfigurationId(INDsleCM.EditValue))

                If CampaignXpo Is Nothing Then
                    IsBusy = False
                    Exit Sub
                End If

                CampaignId = CampaignXpo.Id
                Dim data = Presenter.ListCampaignMainInfoXPInstantFeedbackSourceAndCount(CampaignXpo.Id, _selectorCampaignStatus.GetKeys())
                INDGcCampaigns.DataSource = data.Item1
                IsBusy = False

                If data.Item2 > 0 Then
                    INDTvCampaigns.FocusedRowHandle = 2

                    INDpanelNoData.Visible = False
                    INDpanelBody.Visible = True
                    'Ocultamos el CTR
                    INDpanelCampaign.Visible = False
                End If

            Catch ex As Exception
                IsBusy = False
                INDpanelBody.Visible = False
                INDpanelNoData.Visible = True
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            End Try
        End If
    End Sub

    Private Sub INDSleCampaingStatus_CloseUp(sender As Object, e As EventArgs) Handles INDSleCampaingStatus.CloseUp
        _selectorCampaignStatus.Clear()
        INDSleCampaingStatus.Properties.View.GetSelectedRows().ToList() _
                .ForEach(Sub(m) _selectorCampaignStatus.SetValue(INDSleCampaingStatus.Properties.View.GetRow(m)))

        If INDsleCM.EditValue Is Nothing Then
            Exit Sub
        End If

        If INDsleCM.EditValue = _oldCM AndAlso _oldKeys = _selectorCampaignStatus.GetKeys() Then Exit Sub

        MbtnRefresh_ItemClick(Nothing, Nothing)
    End Sub


#End Region

#Region "ItemClick"
    ''' <summary>
    ''' Se ejecuta al presionar click sobre los items de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TileView2_ItemClick(sender As Object, e As DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs) Handles INDTvCampaigns.ItemClick
        If INDTvCampaigns.GetFocusedRow.GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then Exit Sub

        Dim objCampaigDetailXpo = DirectCast(DirectCast(INDTvCampaigns.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewCampaignMainInfoXpo)

        If objCampaigDetailXpo Is Nothing Then Exit Sub

        AddCampaignToPanel(objCampaigDetailXpo.Id)
    End Sub

    Private Sub AddCampaignToPanel(campaignDetailId As Integer)
        INDpanelCampaign.Visible = True
        Dim newCampaign As New CtrCampaign(Me)
        newCampaign.SetLoadingGrid(True)
        'Dim campaignDetail1 = Await Task.Factory.StartNew(Function() XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).MixingStationService.GetXPOObject(Of CampaignDetailXpo)($"Id = {campaignDetailId}"))
        newCampaign.CampaignXpo = CampaignXpo
        'newCampaign.objCampaignDetailXpo = campaignDetail1
        'newCampaign.CampaignId = campaignDetail1.CampaignId.Id
        newCampaign.CMConfigurationId = INDsleCM.EditValue
        'newCampaign.SetInformationControls(campaignDetail1)
        newCampaign.SetDatasourceAsync(campaignDetailId)
        'newCampaign.ProductionBasketId = campaignDetail1.ProductionBasketId
        newCampaign.Dock = Windows.Forms.DockStyle.Fill
        AddHandler newCampaign.BeginReloadCampaignDetailIdStatus, AddressOf BeginReloadCampaignDetailStatis

        INDpanelCampaign.Controls.Clear()
        INDpanelCampaign.Controls.Add(newCampaign)
    End Sub

    ''' <summary>
    ''' Abre el modal de generar campaña
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MbtnAddCampaign_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnAddCampaign.ItemClick
        OpenFormAddCampaign()
    End Sub

    ''' <summary>
    ''' Click refrescar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MbtnRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MbtnRefresh.ItemClick
        INDpanelCampaign.Controls.Clear()
        INDpanelBody.Visible = False
        INDpanelNoData.Visible = True
        INDsleCM_EditValueChanged(Nothing, Nothing)
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmCampaigns_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCM.Focus()
    End Sub

#End Region

#Region "DocumentClosing"

    ''' <summary>
    ''' Se manda a eliminar el folio
    ''' </summary>
    Private Async Sub WidgetView_DocumentClosing(sender As Object, e As DevExpress.XtraBars.Docking2010.Views.DocumentCancelEventArgs) Handles WidgetView.DocumentClosing
        If MessageIndigo.Show("Desea eliminar la campaña?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Exit Sub
        End If

        IsBusy = True
        INDpanelHeader.Visible = True

        Using model As New MCampaign(Me.Tag)
            Dim _campaignDetailId = CType(e.Document.Control, CtrCampaign).CampaignDetailId
            Dim res = Await model.DeleteCampaign(_campaignDetailId)

            If res.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = res.Message
            End If

            INDsleCM_EditValueChanged(Nothing, Nothing)
        End Using
    End Sub


    Private _firstSelected As Boolean = False
    Private Sub INDSleCampaingStatus_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCampaingStatus.QueryPopUp
        If Not _firstSelected Then
            INDSleCampaingStatus.Properties.View.SelectRow(0)
            INDSleCampaingStatus.Properties.View.SelectRow(1)
            INDSleCampaingStatus.Properties.View.SelectRow(4)
            _firstSelected = True
        End If
    End Sub

#End Region

#End Region

End Class