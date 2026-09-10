'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/06/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports System.Text
Imports Presentation.Authorization.MVP
Imports DevExpress.Xpo
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports System.Drawing
Imports DevExpress.XtraEditors.BaseCheckedListBoxControl
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Authorization
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository

#End Region

Public Class FrmDashboardContractCoverage

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDashboardContractCoverage

    ''' <summary>
    ''' Variable que se utiliza para obtener los códigos de centro de atención y realizar las consulta con los filtros
    ''' </summary>
    Private CareCenterFilters As String

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
    ''' Método que abre el form para asignar las anotaciones
    ''' </summary>
    Private Sub OpenFormAnnotations(ContractCoverageStatus As Integer)
        Dim list = (From x In INDviewInformation.GetSelectedRows() Where INDviewInformation.IsGroupRow(x) = False Select CType(INDviewInformation.GetRow(x), ViewDashboardContractCoverageXpo)).ToList()
        If list.Count = 0 Then
            list = {CType(INDviewInformation.GetFocusedRow(), ViewDashboardContractCoverageXpo)}.ToList()
        End If

        Using formulario As New FrmAnnotations
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 555
            formulario.Height = 500
            formulario.ToolBar.Visible = False
            formulario.ContractCoverageStatus = ContractCoverageStatus
            formulario.ListViewDashboardContractCoverageXpo = list
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que se ejecuta al cerrar el form de anotaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnModalArgs(sender As Object, e As EventArgs)
        BeginReloadDatasource()
    End Sub

    ''' <summary>
    ''' Carga las solicitudes
    ''' </summary>
    Private Sub LoadInformation()
        If INDgcInformation.DataSource IsNot Nothing Then
            Exit Sub
        End If
        INDviewInformation.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListViewDashboardContractCoverage(CareCenterFilters)
                                  INDgcInformation.BeginInvoke(Sub()
                                                                   INDgcInformation.DataSource = result
                                                                   INDviewInformation.HideLoadingPanel()
                                                               End Sub)
                              End Sub)
    End Sub

    ''' <summary>
    ''' Vuelve a cargar el datasource de la rejilla activa
    ''' </summary>
    Private Sub BeginReloadDatasource()
        INDgcInformation.DataSource = Nothing
        LoadInformation()
    End Sub

    ''' <summary>
    ''' Método que asigna la columna de acciones a las rejillas
    ''' </summary>
    Private Sub SetActionsColumns()
        'Se valida si el usuario tiene permiso para mostrar los check del search de centro de atención
        BarraBotones.ActualizarPermisosBarra(Me.Tag)
        'Se ajusta  el ancho de la columna de Quoted
        INDviewInformation.Columns.ColumnByName("GridColumn12").Width = 160
        INDviewSearchCareCenter.OptionsSelection.MultiSelect = If(BarraBotones.PermissionsForm.ContainsKey(96), True, False)

        Dim ListActions As New List(Of eAcciones)

        If BarraBotones.PermissionsForm.ContainsKey(7) Then 'Si tiene permiso de confirmar
            ListActions.Add(eAcciones.ConfirmItem)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(104) Then 'Si tiene permiso de contratado
            ListActions.Add(eAcciones.ContractedItem)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(105) Then 'Si tiene permiso de cotizado
            ListActions.Add(eAcciones.QuotedItem)
        End If

        If ListActions.Count > 0 Then 'Si hay menu se asigna al gridView 
            IndigoGridView1.SetListAcction(INDviewInformation, ListActions)

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewInformation.Columns
                If col.Name = "colActions" Then
                    col.Width = 100
                End If
            Next
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardContractCoverage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetActionsColumns()
        Me.ToolBar.Hide()
        Presenter = New PDashboardContractCoverage()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardContractCoverage_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCareCenter.Properties.PopupFormMinSize = New System.Drawing.Size(INDsleCareCenter.Size.Width - 11, 0)
        INDsleCareCenter.Properties.PopupFormSize = New System.Drawing.Size(INDsleCareCenter.Size.Width - 11, 0)
        INDsleCareCenter.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCareCenter.QueryPopUp
        If INDsleCareCenter.Properties.DataSource Is Nothing Then
            INDsleCareCenter.Properties.DataSource = Presenter.InitializeCareCenter()
        End If
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el search de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDsleCareCenter.CloseUp
        'Se obtienen los códigos de centro de atención seleccionados
        CareCenterFilters = String.Join(",", (From x In INDviewSearchCareCenter.GetSelectedRows() Select "'" & DirectCast(INDviewSearchCareCenter.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow.CODCENATE & "'"))

        'Se valida si seleccionaron items del search
        If String.IsNullOrEmpty(CareCenterFilters) Then
            INDsleCareCenter.Properties.NullText = "Seleccione un Centro de Atención"
        Else
            INDsleCareCenter.Properties.NullText = INDviewSearchCareCenter.GetSelectedRows().Count().ToString() + " Item Seleccionados"
        End If

        BeginReloadDatasource()
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "ConfirmItem"
                OpenFormAnnotations(1)
            Case "ContractedItem"
                OpenFormAnnotations(2)
            Case "QuotedItem"
                OpenFormAnnotations(3)
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "ConfirmItem"
                OpenFormAnnotations(1)
            Case "ContractedItem"
                OpenFormAnnotations(2)
            Case "QuotedItem"
                OpenFormAnnotations(3)
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

#End Region

#End Region

End Class