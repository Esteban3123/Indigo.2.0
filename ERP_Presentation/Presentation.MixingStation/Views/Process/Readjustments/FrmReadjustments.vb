'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Giovanny Plazas L
' Created          : 26/08/2022
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
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.MixingStation.MVP
Imports Domain.Base.Entities

#End Region

Public Class FrmReadjustments
    Implements IReadjustments

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PReadjustments

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
    ''' establece el id de la central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Property CMConfigurationId As Integer? Implements IReadjustments.CMConfigurationId
        Get
            Return CInt(INDsleCM.EditValue)
        End Get
        Set(value As Integer?)
            INDsleCM.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer el datasource de las centrales de mezclas
    ''' </summary>
    ''' <returns></returns>
    Property ListCMConfiguration As List(Of MixinStationCMConfigXpo) Implements IReadjustments.ListCMConfiguration
        Get
            Return TryCast(INDsleCM.Properties.DataSource, List(Of MixinStationCMConfigXpo))
        End Get
        Set(value As List(Of MixinStationCMConfigXpo))
            INDsleCM.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' propiedad para establecer el datasource de la rejilla
    ''' </summary>
    ''' <returns></returns>
    Property ListViewReadjustmentsXpo As List(Of ViewReadjustmentsXpo) Implements IReadjustments.ListViewReadjustmentsXpo
        Get
            Return TryCast(INDgcReadjustments.DataSource, List(Of ViewReadjustmentsXpo))
        End Get
        Set(value As List(Of ViewReadjustmentsXpo))
            INDgcReadjustments.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IReadjustments.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' items seleccionados
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property SelectedItems As List(Of ViewReadjustmentsXpo)
        Get
            Return INDviewReadjustments.GetSelectedRows() _
                   .Where(Function(m) Not INDviewReadjustments.IsGroupRow(m)) _
                   .Select(Function(m) CType(INDviewReadjustments.GetRow(m), ViewReadjustmentsXpo)) _
                   .ToList()
        End Get
    End Property

#End Region

#Region "Enumerations"
    Enum SendTo
        MixingStation = 1
    End Enum
#End Region

#Region "Methods"

    ''' <summary>
    ''' Vuelve a cargar el datasource de la rejilla activa
    ''' </summary>
    Private Async Function BeginReloadDatasource() As Task
        If CMConfigurationId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar primero una central de mezclas"
            Return
        End If
        INDgcReadjustments.DataSource = Nothing
        AsyncLoader(True)
        Await Presenter.InitializeReadjustments()
        AsyncLoader(False)
    End Function

    ''' <summary>
    ''' Método que asigna la columna de acciones a las rejillas
    ''' </summary>
    Private Sub SetActionsColumns()
        BarraBotones.ActualizarPermisosBarra(Me.Tag)
        IndigoGridView1.MoreInfoColunmns(INDviewReadjustments)

        Dim listActions As New List(Of eAcciones)

        If BarraBotones.PermissionsForm.ContainsKey(141) Then 'si tiene permiso de enviar central de mezclas
            listActions.Add(eAcciones.SendToMixingStation)
        End If

        If listActions.Count > 0 Then 'si hay menu se asigna al gridview 
            IndigoGridView1.SetListAcction(INDviewReadjustments, listActions)

            Dim col = INDviewReadjustments.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
            If col IsNot Nothing Then col.Width = 120
        End If
    End Sub

    ''' <summary>
    ''' accion para enviar a central de mezclas
    ''' </summary>
    ''' <param name="_items"></param>
    Async Sub SendToMixingStation(_items As List(Of ViewReadjustmentsXpo)) Implements IReadjustments.SendToMixingStation
        If _items Is Nothing OrElse Not _items.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione los items a enviar"
            Exit Sub
        End If

        If MessageIndigo.Show("Se generará una Orden de Traslado tipo ""Traslado"" con las adecuaciones seleccionadas. ¿Desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        Try
            Dim _selectedItems = Await AssignedValuesEntityandSendTo(_items, SendTo.MixingStation)

            Using Model As New MReadjustments(Me.Tag)
                AsyncLoader(True)
                Dim result = Await Model.SaveReadjustmentsRepository(_selectedItems, operativeUnitId:=_idOperativeUnit)
                AsyncLoader(False)
                If result Is Nothing OrElse Not result.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = result?.Message
                    Await BeginReloadDatasource()
                    Exit Sub
                End If
                Mensaje(EeventViewerImages.Informacion) = result?.Message
                Await BeginReloadDatasource()
            End Using

        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Sub

    Private Async Function AssignedValuesEntityandSendTo(_selectedItems As List(Of ViewReadjustmentsXpo), SendTo As SendTo) As Task(Of List(Of Domain.Entities.Readjustments))
        Return Await Task.Factory.StartNew(Function() As List(Of Readjustments)
                                               Dim listReadjustments = New List(Of Readjustments)
                                               For Each item In _selectedItems
                                                   Dim _newItem = New Readjustments
                                                   With _newItem
                                                       .Id = item.Id
                                                       .EntityId = item.EntityId
                                                       .EntityName = item.EntityName
                                                       .RequestPackageDetailStatusId = item.RequestPackageDetailStatusId
                                                       .BatchCode = item.BatchCodeOriginal
                                                       .Number = item.Number
                                                       .SendTo = SendTo
                                                       .CreationUser = item.CreationUser
                                                       .CreationDate = item.CreationDate
                                                       .ModificationUser = item.ModificationUser
                                                       .ModificationDate = item.ModificationDate
                                                   End With
                                                   _newItem.MarkAsModified()
                                                   listReadjustments.Add(_newItem)
                                               Next
                                               Return listReadjustments
                                           End Function)
    End Function

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardQuoted_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me.ToolBar.Hide()
        Presenter = New PReadjustments(Me)
        SetActionsColumns()
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
    ''' Evento que se dispara al desplegar el control de las centrales de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCM_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCM.QueryPopUp
        If ListCMConfiguration Is Nothing Then
            Presenter.InitializeCMConfiguration()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Se dispara la cambiar el valor del control de central de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleCM_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCM.EditValueChanged
        If CMConfigurationId IsNot Nothing Then
            Await BeginReloadDatasource()
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
        SendToMixingStation(SelectedItems)
    End Sub

#End Region

#Region "ItemClick"

    Private Async Sub INDSbRefresh_Click(sender As Object, e As EventArgs) Handles INDSbRefresh.Click, INDBarRefresh.ItemClick
        Await BeginReloadDatasource()
    End Sub

    Private Sub INDviewConfirmUnitDose_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDviewReadjustments.CustomUnboundColumnData
    End Sub

    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As Controls.QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        Dim items = SelectedItems
        Dim popUp = CType(sender, RepositoryItemPopupContainerEdit)
        e.Buttons.ToList().ForEach(Sub(i) i.Visible = False)

        If Not INDviewReadjustments.GetSelectedRows().Contains(INDviewReadjustments.FocusedRowHandle) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un item para ejecutar alguna acción"
            popUp.PopupControl.MinimumSize = New System.Drawing.Size(200, 0)
            popUp.PopupControl.MaximumSize = New System.Drawing.Size(200, 0)
            popUp.PopupControl.Size = New System.Drawing.Size(200, 0)
            Return
        End If

        Dim SendToMixingStation = e.Buttons.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.SendToMixingStation)))
        SendToMixingStation.Visible = True
        popUp.PopupControl.Size = New System.Drawing.Size(200, 36)

    End Sub

    ''' <summary>
    ''' popup menu showing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewConfirmUnitDose_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDviewReadjustments.PopupMenuShowing
        If e.HitInfo.InRow Then
            If Not INDviewReadjustments.GetSelectedRows().Contains(e.HitInfo.RowHandle) Then
                IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(i) i.Visibility = DevExpress.XtraBars.BarItemVisibility.Never)
                Return
            End If
        End If
        If BarraBotones.PermissionsForm.ContainsKey(141) Then
            Dim sendToMixingStation = IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag IsNot Nothing AndAlso m.Tag.Equals(NameOf(eAcciones.SendToMixingStation)))
            sendToMixingStation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If
    End Sub

    Private _idOperativeUnit As Integer
    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

#End Region

#End Region

End Class