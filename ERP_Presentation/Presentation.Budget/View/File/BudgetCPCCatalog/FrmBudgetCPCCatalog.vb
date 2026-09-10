#Region "Imports"
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Budget.MVP
Imports Presentation.Controls
Imports Presentation.Controls.MVP

#End Region

''' <summary>
''' Contiene la vista de el frontal Rubros
''' </summary>
''' <remarks></remarks>
Public Class FrmBudgetCPCCatalog
    Implements IBudgetCPCCatalog

#Region "Builder"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' Contiene el modelo del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim model As MBudgetCPCCatalog

    ''' <summary>
    ''' Contiene el presentador de formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PBudgetCPCCatalog



    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim BudgetaryValidity As BudgetaryValidity

    ''' <summary>
    ''' Guarda el Nodo con foco
    ''' </summary>
    ''' <remarks></remarks>
    Private SavedFocused As TreeListNode

    ''' <summary>
    ''' Bandera para saber si necesita recuperar el foco
    ''' </summary>
    ''' <remarks></remarks>
    Private NeedRestoreFocused As Boolean

    ''' <summary>
    ''' Contiene el listado de los niveles de categoria
    ''' </summary>
    ''' <remarks></remarks>
    Dim LevelsCategory As List(Of LevelCategory)

    ''' <summary>
    ''' Listado de categorias
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListCPCCatalog As List(Of CPCCatalog)
        Get
            Return INDtlBudgetCPCCatalog.DataSource
        End Get
        Set(value As List(Of CPCCatalog))
            INDtlBudgetCPCCatalog.DataSource = value
        End Set
    End Property

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el estado de la vigencia para visualizarlo
    ''' en el control de información
    ''' </summary>
    ''' <remarks></remarks>
    Private _Status As String
    Public Property Status As String
        Get
            Return _Status
        End Get
        Set(value As String)
            _Status = value
        End Set
    End Property

#End Region

#Region "Handles"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        presenter = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        BudgetaryValidity = Nothing
        SavedFocused = Nothing
        NeedRestoreFocused = Nothing
        LevelsCategory = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBudgetCPCCatalog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter = New PBudgetCPCCatalog(Me)
        Me.BarraBotones.PrepareToolbar(eAction.None)
        LoadStructure()
        CleanControls()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Para abrir el show dialog de ingresar rubros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbAddItem_Click(sender As Object, e As EventArgs) Handles INDbtnAddItem.Click
        OpenShowDialog()
    End Sub

#End Region

#Region "MouseUp"

    ''' <summary>
    ''' Metodo para abrir el menu de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtlBudgetCPCCatalog_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDtlBudgetCPCCatalog.MouseUp
        Dim tree As TreeList = TryCast(sender, TreeList)
        If e.Button = MouseButtons.Right AndAlso ModifierKeys = Keys.None AndAlso tree.State = TreeListState.Regular Then
            Dim pt As Point = tree.PointToClient(MousePosition)
            Dim info As TreeListHitInfo = tree.CalcHitInfo(pt)
            If info.HitInfoType = HitInfoType.Cell Then
                tree.FocusedNode = info.Node
                Dim row As CPCCatalog = INDtlBudgetCPCCatalog.GetDataRecordByNode(INDtlBudgetCPCCatalog.FocusedNode)
                If row.Code IsNot Nothing Then
                    INDbarBtnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                Else
                    INDbarBtnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                INDpopUpMenu.ShowPopup(MousePosition)
            End If

        End If
    End Sub

#End Region

#Region "ItemClick"

    ''' <summary>
    ''' Click agregar CPCCatalog hijo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarBtnAdd_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarBtnAdd.ItemClick
        Dim focusedBudgetCPCCatalog As CPCCatalog = INDtlBudgetCPCCatalog.GetDataRecordByNode(INDtlBudgetCPCCatalog.FocusedNode)
        Dim tuple As New Tuple(Of Integer, String)(focusedBudgetCPCCatalog.Id, focusedBudgetCPCCatalog.Code + " - " + focusedBudgetCPCCatalog.Name)
        OpenShowDialog(Nothing, tuple)
    End Sub

    ''' <summary>
    ''' Evento para Editar un rubro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarBtnEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarBtnEdit.ItemClick
        Dim focusedBudgetCPCCatalog As CPCCatalog = INDtlBudgetCPCCatalog.GetDataRecordByNode(INDtlBudgetCPCCatalog.FocusedNode)
        OpenShowDialog(focusedBudgetCPCCatalog)
    End Sub


#End Region



#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBudgetCPCCatalog_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        LoadStructure()
    End Sub

#End Region


#Region "PopupMenuShowing"

    ''' <summary>
    ''' Evento que se dispara al presionar click derecho para aparcer el menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtlBudgetCPCCatalog_PopupMenuShowing(sender As Object, e As DevExpress.XtraTreeList.PopupMenuShowingEventArgs) Handles INDtlBudgetCPCCatalog.PopupMenuShowing
    End Sub

#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo encargado de cargar el datasource del treelist
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStructure()
        INDlyBudgetItem.BeginUpdate()
        AsyncLoader(True)
        Dim _structure As XPCollection = presenter.ListCPCCatalogTreeList()
        ListCPCCatalog = New List(Of CPCCatalog)
        If _structure IsNot Nothing Then
            For Each itemXpo As Infrastructure.Data.Xpo.BudgetRepository.BudgetCPCCatalogXpo In _structure
                Dim _item As New CPCCatalog
                With _item
                    .Id = itemXpo.Id
                    .Code = itemXpo.Code
                    .Name = itemXpo.Name
                    .CPCCatalogOwnerId = itemXpo.CPCCatalogOwnerId
                End With
                ListCPCCatalog.Add(_item)
            Next
            AsyncLoader(False)
            ActionOnControls = True
        End If
        INDtlBudgetCPCCatalog.RefreshDataSource()
        INDtlBudgetCPCCatalog.ExpandAll()
        INDlyBudgetItem.EndUpdate()
    End Sub

    ''' <summary>
    ''' Metodo que abre el formulario modal para agregar las categorias
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenShowDialog(Optional focusedBudgetCPCCatalog As CPCCatalog = Nothing, Optional TupleIdAndCodeNameCategoryParent As Tuple(Of Integer, String) = Nothing)
        If Status = obtenerRecurso(Cerrada, Eform.BudgetEntities) Then
            Mensaje(EeventViewerImages.Advertencia) = "No puede manipular un rubro cuando la vigencia está Cerrada."
            Exit Sub
        End If
        Using Formulario As New FrmShowDialogCPCCatalog()
            AddHandler Formulario.UpdateDatasourceTreeList, AddressOf LoadStructure
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.CPCCatalog = focusedBudgetCPCCatalog
            Formulario.Width = 780
            Formulario.Height = 768
            Dim frm As New FrmTransparent(Formulario, False)
            frm.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Accion que se realizaran en los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionOnControls As Boolean
        Set(value As Boolean)
            INDlyBudgetItem.BeginUpdate()
            INDbtnAddItem.Enabled = value
            INDtlBudgetCPCCatalog.Enabled = value
            INDlyBudgetItem.EndUpdate()
            If value Then
                INDbtnAddItem.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para refrescar el treelist
    ''' </summary>
    ''' <remarks></remarks>
    Sub TreelistRefresh()
        If INDtlBudgetCPCCatalog.DataSource IsNot Nothing Then
            Dim list As New List(Of Category)
            list = INDtlBudgetCPCCatalog.DataSource
            INDtlBudgetCPCCatalog.DataSource = Nothing
            INDtlBudgetCPCCatalog.DataSource = list
            INDtlBudgetCPCCatalog.RefreshDataSource()
        End If
    End Sub


    ''' <summary>
    ''' Metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Sub CleanControls()
        INDlyBudgetItem.BeginUpdate()
        ActionOnControls = True
        ListCPCCatalog = Nothing
        Status = String.Empty
        BarraBotones.StatusRecordVisible = False
        INDlyBudgetItem.EndUpdate()
    End Sub

#End Region

#Region "ICrud Base"
    ''' <summary>
    ''' Metodos crud implementando la interfaz IcrudBase
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub
    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Click deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        'Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, Validity.Value, 0, Validity.Value)
    End Sub

    ''' <summary>
    ''' Load Barra Botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

#End Region

End Class
