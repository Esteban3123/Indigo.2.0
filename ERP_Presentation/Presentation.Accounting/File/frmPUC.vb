'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 06-05-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Accounting.MVP
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports Domain.Entities
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraTreeList.Nodes
Imports Presentation.Base
Imports DevExpress.XtraTreeList.Columns
Imports Presentation.Common.MVP
Imports Presentation.Common
Imports Infrastructure.Data.Xpo
Imports DevExpress.Data
Imports System.ComponentModel

#End Region

Public Class frmPUC
    Implements IFormPUC

#Region "Members"
    'Declara una lista de objetos de tipo MainAccounts para almacenar una colección de cuentas principales
    Dim ListAccounts As List(Of MainAccounts)

    'Declara un objeto de tipo TreeListNode para representar un nodo en un control TreeList
    Dim node As TreeListNode
#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LegalBookId As Integer?
        Get
            Return INDsleLegalBook.EditValue
        End Get
        Set(value As Integer?)
            INDsleLegalBook.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LegalBookXpo As XPInstantFeedbackSource
        Get
            Return INDsleLegalBook.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleLegalBook.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer

    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements Base.IcrudBase.Guardar

    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(value As String)

        End Set
    End Property

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Metodo para editar una cuenta
    ''' </summary>
    Private Sub EditAccounts()

    End Sub

    ''' <summary>
    ''' Inicializa el datasource del search de libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeLegalBook()
        LegalBookXpo = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountingService.ListAllBookByStatus(True)
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el datasource de las cuentas contables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeDataSourceTreeList()
        If LegalBookId Is Nothing Then
            Me.treeAccounts.DataSource = Nothing
            Exit Sub
        End If
        AsyncLoader(True)
        Dim nodeTmp = node
        Dim XpServerCollectionSource1 = XpoServiceEx.Instance(indigo.TransactionalContainer).AccountingService.ListAccounts(LegalBookId)
        Me.treeAccounts.DataSource = XpServerCollectionSource1
        treeAccounts.ExpandAll()
        treeAccounts.SetFocusedNode(nodeTmp)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = False
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Metodo que lanza el formulario del PUC
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowFormFrmPopupPUC(codeAC As String)
        Using Formulario As New FrmPopupPUC
            Formulario.CodeAccountingClass = codeAC
            Formulario.LegalBookId = LegalBookId
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Dim transparent As New FrmTransparent(Formulario, False)
            transparent.ShowDialog(Me)
            InitializeDataSourceTreeList()
        End Using
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListAccounts = Nothing
        node = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub frmPUC_Load(sender As Object, e As EventArgs) Handles Me.Load
        treeAccounts.BackColor = Drawing.Color.White
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
    End Sub

#End Region

#Region "DoubleClick"

    ''' <summary>
    ''' Handles the DoubleClick event of the treeAccounts control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub treeAccounts_DoubleClick(sender As Object, e As EventArgs) Handles treeAccounts.DoubleClick
        Dim pMouse As System.Drawing.Point = MousePosition
        Dim hit = treeAccounts.CalcHitInfo(treeAccounts.PointToClient(pMouse))
        If hit IsNot Nothing AndAlso hit.Node IsNot Nothing AndAlso Not hit.Node.GetType().Equals(GetType(DevExpress.XtraTreeList.Nodes.TreeListAutoFilterNode)) Then
            AsyncLoader(True)
            Dim idMainAccount As Integer
            Dim _account As MainAccounts
            Using model As New MPUC("602")
                idMainAccount = treeAccounts.FocusedNode.GetValue("Id")
                _account = Await model.GetAccountById(idMainAccount)
            End Using
            If _account IsNot Nothing Then
                ShowFormFrmPopupPUC(_account.Number)
            End If
            AsyncLoader(False)
        End If
    End Sub

#End Region

#Region "FocusedNodeChanged"

    ''' <summary>
    ''' Handles the FocusedNodeChanged event of the treeAccounts control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraTreeList.FocusedNodeChangedEventArgs"/> instance containing the event data.</param>
    Private Sub treeAccounts_FocusedNodeChanged(sender As Object, e As DevExpress.XtraTreeList.FocusedNodeChangedEventArgs) Handles treeAccounts.FocusedNodeChanged

        node = e.Node


    End Sub

#End Region

#Region "PopupMenuShowing"

    ''' <summary>
    ''' Handles the PopupMenuShowing event of the treeAccounts control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraTreeList.PopupMenuShowingEventArgs"/> instance containing the event data.</param>
    Private Sub treeAccounts_PopupMenuShowing(sender As Object, e As DevExpress.XtraTreeList.PopupMenuShowingEventArgs) Handles treeAccounts.PopupMenuShowing
        'If e.Menu Is Nothing Then
        '    Exit Sub
        'End If
        'e.Menu.Items.Clear()
        'e.Menu.Items.Add(New DXMenuItem(Resources.ResourceManager.GetString("AddAccount"), AddressOf EditAccounts, Nothing))
        'e.Menu.Items.Add(New DXMenuItem(Resources.ResourceManager.GetString("EditAccount"), AddressOf EditAccounts, Nothing))
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Este evento se activa cuando se muestra el formulario frmPUC
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub frmPUC_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleLegalBook.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de libro oficial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLegalBook_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleLegalBook.QueryPopUp
        If LegalBookXpo Is Nothing Then
            InitializeLegalBook()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de libro oficial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLegalBook_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLegalBook.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1682, Nothing, True)
            InitializeLegalBook()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de libro oficial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleLegalBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleLegalBook.EditValueChanged
        InitializeDataSourceTreeList()
    End Sub

#End Region

#End Region

#Region "BarraBotones"

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        AsyncLoader(True)
        ShowFormFrmPopupPUC(String.Empty)
        BarraBotones.PrepareToolbar(eAction.OnlyNew)
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
    End Sub

#End Region

End Class