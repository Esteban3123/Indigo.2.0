'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/05/2020
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

#End Region

Public Class FrmConfigurationServicesAmbulatory
    Implements IConfigurationServicesAmbulatory

#Region "Variables"

    Dim Presenter As PConfigurationServicesAmbulatory

    Private tokenServicesAsync As CancellationTokenSource

    Private tokenProductsAsync As CancellationTokenSource

#End Region

#Region "Properties"

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IConfigurationServicesAmbulatory.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IConfigurationServicesAmbulatory.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

#End Region

#Region "ICrud"

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "Methods"

    Dim ListDeleteIds As List(Of Integer)

    Private Async Sub DeleteDetail(view As GridView)
        If Not BarraBotones.PermissionsForm.ContainsKey(1) Then
            Mensaje(EeventViewerImages.Advertencia) = "El usuario no tiene permiso para eliminar"
            Exit Sub
        End If

        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Me.AsyncLoader(True)

        ListDeleteIds = Nothing
        SelectOptions(view, True)
        If ListDeleteIds Is Nothing OrElse ListDeleteIds.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione items para eliminar"
            Me.AsyncLoader(False)
            Exit Sub
        End If

        Try
            Using model As New MConfigurationServicesAmbulatory(Me.Tag.ToString())
                Dim result = Await model.DeleteConfigurationServicesAmbulatory(ListDeleteIds)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                    EventDetail()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Dim ListPortfolioCUPSEntityIds As List(Of Integer) = Nothing
    Dim ListPortfolioInventoryProductIds As List(Of Integer) = Nothing
    Dim ConfigurationServicesAmbulatoryId As Integer = Nothing

    Private Sub OpenFormConfiguration(view As GridView, addMode As Boolean)
        ListPortfolioCUPSEntityIds = Nothing
        ListPortfolioInventoryProductIds = Nothing
        ConfigurationServicesAmbulatoryId = Nothing

        If addMode Then 'Si se selecciona el item del menu Agregar
            SelectOptions(view, False)
            If (ListPortfolioCUPSEntityIds Is Nothing OrElse ListPortfolioCUPSEntityIds.Count = 0) AndAlso (ListPortfolioInventoryProductIds Is Nothing OrElse ListPortfolioInventoryProductIds.Count = 0) Then
                Exit Sub
            End If
        Else 'Si se selecciona el item del menu editar
            ConfigurationServicesAmbulatoryId = view.GetFocusedRow().ConfigurationServicesAmbulatoryId
        End If

        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmConfigurationServicesAmbulatoryDetail()
            AddHandler Formulario.EventDetail, AddressOf EventDetail
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 920
            Formulario.Height = 680
            Formulario.ListPortfolioCUPSEntityIds = ListPortfolioCUPSEntityIds
            Formulario.ListPortfolioInventoryProductIds = ListPortfolioInventoryProductIds
            Formulario.ConfigurationServicesAmbulatoryId = ConfigurationServicesAmbulatoryId
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub EventDetail()
        CleanControls()
        ExecuteGetListServices()
        ExecuteGetListProducts()
    End Sub

    Private Sub SelectOptions(view As GridView, isDelete As Boolean)
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                GetChildsRows(view, listHandlesSelected(i), isDelete)
            Next
        End If
    End Sub

    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, isDelete As Boolean)
        If view.IsGroupRow(groupRowHandle) Then
            Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
            For i As Integer = 0 To childCount - 1
                Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
                If view.IsGroupRow(childHandle) Then
                    GetChildsRows(view, childHandle, isDelete)
                Else
                    SetValue(view, childHandle, isDelete)
                End If
            Next
        Else
            SetValue(view, groupRowHandle, isDelete)
        End If
    End Sub

    Private Sub SetValue(view As GridView, handle As Integer, isDelete As Boolean)
        Dim row = view.GetRow(handle)
        If isDelete = False Then
            If row.SelectOption = False Then
                If view.Name = INDviewServices.Name Then 'Si se realiza la accion en la rejilla de servicios
                    If ListPortfolioCUPSEntityIds Is Nothing Then
                        ListPortfolioCUPSEntityIds = New List(Of Integer)
                    End If
                    ListPortfolioCUPSEntityIds.Add(row.Id)
                Else 'Si se realiza la accion en la rejilla de productos
                    If ListPortfolioInventoryProductIds Is Nothing Then
                        ListPortfolioInventoryProductIds = New List(Of Integer)
                    End If
                    ListPortfolioInventoryProductIds.Add(row.Id)
                End If
            End If
        Else
            If row.SelectOption = True Then
                If ListDeleteIds Is Nothing Then
                    ListDeleteIds = New List(Of Integer)
                End If
                ListDeleteIds.Add(row.ConfigurationServicesAmbulatoryId)
            End If
        End If
    End Sub

    Private Sub ExecuteGetListServices()
        INDviewServices.ShowLoadingPanel()
        tokenServicesAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListServices(INDslePortfolio.EditValue)
                                  If Not tokenServicesAsync.IsCancellationRequested Then
                                      INDgcServices.SafeInvoke(Sub()
                                                                   INDviewServices.HideLoadingPanel()
                                                                   INDgcServices.DataSource = result
                                                                   INDviewServices.ExpandAllGroups()
                                                                   SetWidthGrids()
                                                               End Sub)
                                  End If
                              End Sub, tokenServicesAsync.Token)
    End Sub

    Private Sub ExecuteGetListProducts()
        INDviewProducts.ShowLoadingPanel()
        tokenProductsAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListProducts(INDslePortfolio.EditValue)
                                  If Not tokenProductsAsync.IsCancellationRequested Then
                                      INDgcProducts.SafeInvoke(Sub()
                                                                   INDviewProducts.HideLoadingPanel()
                                                                   INDgcProducts.DataSource = result
                                                                   INDviewProducts.ExpandAllGroups()
                                                                   SetWidthGrids()
                                                               End Sub)
                                  End If
                              End Sub, tokenProductsAsync.Token)
    End Sub

    Private Sub CleanControls()
        INDviewServices.HideLoadingPanel()
        INDviewProducts.HideLoadingPanel()
        INDgcServices.DataSource = Nothing
        INDgcProducts.DataSource = Nothing
    End Sub

    Private Sub SetWidthGrids()
        Dim screen As Screen = Screen.PrimaryScreen
        Dim widthScreen As Integer = screen.Bounds.Width
        Dim twoWidth As Decimal = (widthScreen / 2) - 60

        INDlyItemServices.MinSize = New System.Drawing.Size(twoWidth, 1)
        INDlyItemServices.MaxSize = New System.Drawing.Size(twoWidth, 0)

        INDlyItemProducts.MinSize = New System.Drawing.Size(twoWidth, 1)
        INDlyItemProducts.MaxSize = New System.Drawing.Size(twoWidth, 0)
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub FrmConfigurationServicesAmbulatory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()
        Presenter = New PConfigurationServicesAmbulatory(Me)
        SetWidthGrids()
        BarraBotones.ActualizarPermisosBarra("2171")
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmConfigurationServicesAmbulatory_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDslePortfolio.Properties.PopupFormMinSize = New System.Drawing.Size(INDslePortfolio.Size.Width - 11, 0)
        INDslePortfolio.Properties.PopupFormSize = New System.Drawing.Size(INDslePortfolio.Size.Width - 11, 0)
        INDslePortfolio.Focus()
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDslePortfolio_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePortfolio.EditValueChanged
        If INDslePortfolio.EditValue IsNot Nothing Then
            CleanControls()
            ExecuteGetListServices()
            ExecuteGetListProducts()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDslePortfolio_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePortfolio.QueryPopUp
        If INDslePortfolio.Properties.DataSource Is Nothing Then
            INDslePortfolio.Properties.DataSource = Presenter.InitializePortfolio()
        End If
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmConfigurationServicesAmbulatory_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenServicesAsync IsNot Nothing Then
            tokenServicesAsync.Cancel()
        End If

        If tokenProductsAsync IsNot Nothing Then
            tokenProductsAsync.Cancel()
        End If
    End Sub

#End Region

#Region "PopupMenuShowing"

    Private Sub INDviewServices_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDviewServices.PopupMenuShowing
        Dim View = CType(sender, GridView)
        Dim hideButtonEdit As Boolean = False

        'Si se selecciona un grupo o se selecciona mas de un item o si se selecciona un item pero no tiene el check no se muestra el editar
        Dim listHandlesSelected = View.GetSelectedRows
        If listHandlesSelected IsNot Nothing Then
            If listHandlesSelected.Length > 0 Then 'Si se selecciona un grupo no se muestra el editar
                For i = 0 To listHandlesSelected.Count - 1
                    If View.IsGroupRow(listHandlesSelected(i)) Then
                        hideButtonEdit = True
                    End If
                Next
            End If

            If listHandlesSelected.Count > 1 Then 'Si se selecciona mas de un item no se muestra el editar
                hideButtonEdit = True
            End If

            If listHandlesSelected.Count = 1 Then 'Si selecciona un item pero no tiene el check no se muestra el editar
                Dim entity = View.GetFocusedRow()
                If entity IsNot Nothing AndAlso entity.SelectOption = False Then
                    hideButtonEdit = True
                End If
            End If
        End If

        INDbarButtonAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        INDbarButtonDeleted.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        If hideButtonEdit Then
            INDbarButtonEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            INDbarButtonEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        PopupMenuActions.Manager = BarManager
        PopupMenuActions.ShowPopup(View.GridControl.PointToScreen(e.Point))
    End Sub

    Private Sub INDviewProducts_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDviewProducts.PopupMenuShowing
        Dim View = CType(sender, GridView)
        Dim hideButtonEdit As Boolean = False

        'Si se selecciona un grupo o se selecciona mas de un item o si se selecciona un item pero no tiene el check no se muestra el editar
        Dim listHandlesSelected = View.GetSelectedRows
        If listHandlesSelected IsNot Nothing Then
            If listHandlesSelected.Length > 0 Then 'Si se selecciona un grupo no se muestra el editar
                For i = 0 To listHandlesSelected.Count - 1
                    If View.IsGroupRow(listHandlesSelected(i)) Then
                        hideButtonEdit = True
                    End If
                Next
            End If

            If listHandlesSelected.Count > 1 Then 'Si se selecciona mas de un item no se muestra el editar
                hideButtonEdit = True
            End If

            If listHandlesSelected.Count = 1 Then 'Si selecciona un item pero no tiene el check no se muestra el editar
                Dim entity = View.GetFocusedRow()
                If entity IsNot Nothing AndAlso entity.SelectOption = False Then
                    hideButtonEdit = True
                End If
            End If
        End If

        INDbarButtonAdd2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        INDbarButtonDeleted2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        If hideButtonEdit Then
            INDbarButtonEdit2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            INDbarButtonEdit2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        PopupMenuActions2.Manager = BarManager2
        PopupMenuActions2.ShowPopup(View.GridControl.PointToScreen(e.Point))
    End Sub

#End Region

#Region "ItemClick"

    Private Sub INDbarButtonAdd_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonAdd.ItemClick
        OpenFormConfiguration(INDviewServices, True)
    End Sub

    Private Sub INDbarButtonEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonEdit.ItemClick
        OpenFormConfiguration(INDviewServices, False)
    End Sub

    Private Sub INDbarButtonDeleted_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonDeleted.ItemClick
        DeleteDetail(INDviewServices)
    End Sub

    Private Sub INDbarButtonAdd2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonAdd2.ItemClick
        OpenFormConfiguration(INDviewProducts, True)
    End Sub

    Private Sub INDbarButtonEdit2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonEdit2.ItemClick
        OpenFormConfiguration(INDviewProducts, False)
    End Sub

    Private Sub INDbarButtonDeleted2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonDeleted2.ItemClick
        DeleteDetail(INDviewProducts)
    End Sub

#End Region

#Region "GroupRowCollapsed and GroupRowExpanded"

    Private Sub INDviewServices_GroupRowCollapsed(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowEventArgs) Handles INDviewServices.GroupRowCollapsed, INDviewProducts.GroupRowCollapsed
        SetWidthGrids()
    End Sub

    Private Sub INDviewServices_GroupRowExpanded(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowEventArgs) Handles INDviewServices.GroupRowExpanded, INDviewProducts.GroupRowExpanded
        SetWidthGrids()
    End Sub

#End Region

#End Region

End Class