'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 28-04-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

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
Public Class FrmBudgetItems
    Implements IBudgetItem

#Region "Builder"

    Public ctrTmp As CtrInfo

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrInfo()
        ctrTmp.SetTotalValues(AddressOf getInfo)
        ctrTmp.RefreshInfo()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    Private Function getInfo() As Tuple(Of String, String, String)
        Return New Tuple(Of String, String, String)(INDsleBudgetEntity.Text, INDsleValidity.Text, Status)
    End Function

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable para conocer si el formulario abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim _Indigo As SessionValues

    ''' <summary>
    ''' Contiene el modelo del formulario
    ''' </summary>
    ''' <remarks></remarks>
    'Dim model As MBudgetItem

    ''' <summary>
    ''' Contiene el presentador de formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PBudgetItem

    ''' <summary>
    ''' DataTable
    ''' </summary>
    Dim dtFieldsCustomizables As DataTable

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
    ''' variable que indentifica que tipo de rubros vamos a trabajar.... 1 = ingreso, 2 = gasto
    ''' </summary>
    ''' <remarks></remarks>
    Dim _processItemType As EItemType

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
    Public Property ListCategory As List(Of Category)
        Get
            Return INDtlBudgetItems.DataSource
        End Get
        Set(value As List(Of Category))
            INDtlBudgetItems.DataSource = value
        End Set
    End Property

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityId As Integer? Implements IBudgetItem.BudgetEntityId
        Get
            Return INDsleBudgetEntity.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de entidades presupuestales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IBudgetItem.BudgetEntityXpo
        Get
            Return INDsleBudgetEntity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleBudgetEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el año de la vigencia que tiene la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Validity As Integer? Implements IBudgetItem.Validity
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer?)
            INDsleValidity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityXpo As DevExpress.Xpo.XPCollection Implements IBudgetItem.ValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de rubros que vamos a trabajar.... 1 = ingreso, 2 = gasto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProcessItemType As EItemType
        Get
            Return _processItemType
        End Get
        Set(value As EItemType)
            _processItemType = value
        End Set
    End Property

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
    ''' <summary>
    ''' Libera memoria al cerrar el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        SearchMode = Nothing
        presenter = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        _processItemType = Nothing
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
    Private Sub FrmBudgetItems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter = New PBudgetItem(Me)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
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

#Region "EditValueChanged"

    ''' <summary>
    ''' LLena el control de vigencias cuando cambien las entidades presupuestales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleBudgetEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntity.EditValueChanged
        If BudgetEntityId IsNot Nothing Then
            BarraBotones.StatusRecordVisible = True
            BarraBotones.ControlHideStatus = False
            Validity = Nothing
            ValidityXpo = Nothing
            INDtlBudgetItems.DataSource = Nothing
            ActionOnControls = False
            presenter.InitializeValidity(BudgetEntityId)
            SetFirstOrDefaultValidity()
            ctrTmp.RefreshInfo()
        End If
    End Sub

    ''' <summary>
    ''' Cuando se selecciona una vigencia, consulta los rubros de determinada entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If Validity IsNot Nothing Then
            If BudgetEntityId IsNot Nothing Then
                SetStatus()
                ActionOnControls = True
                LoadStructure()
                ctrTmp.RefreshInfo()

                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, Validity.Value, 0, Validity.Value)
            End If
        End If
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    ''' <summary>
    ''' Para Colocar nombres en los campos numericos dentro de la rejilla del search look up de vigencias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridView2_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgvValidity.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDColVStatus.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = obtenerRecurso(Registrada, Eform.BudgetEntities)
                Case 2
                    e.DisplayText = obtenerRecurso(Activa, Eform.BudgetEntities)
                Case 3
                    e.DisplayText = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                Case Else

            End Select
        End If
    End Sub

#End Region

#Region "MouseUp"

    ''' <summary>
    ''' Metodo para abrir el menu de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtlBudgetItems_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDtlBudgetItems.MouseUp
        Dim tree As TreeList = TryCast(sender, TreeList)
        If e.Button = MouseButtons.Right AndAlso ModifierKeys = Keys.None AndAlso tree.State = TreeListState.Regular Then
            Dim pt As Point = tree.PointToClient(MousePosition)
            Dim info As TreeListHitInfo = tree.CalcHitInfo(pt)
            If info.HitInfoType = HitInfoType.Cell Then
                tree.FocusedNode = info.Node
                Dim row As Category = INDtlBudgetItems.GetDataRecordByNode(INDtlBudgetItems.FocusedNode)
                If row.FinancialSourceId IsNot Nothing Then
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
    ''' Click agregar rubro hijo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarBtnAdd_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarBtnAdd.ItemClick
        Dim focusedBudgetItem As Category = INDtlBudgetItems.GetDataRecordByNode(INDtlBudgetItems.FocusedNode)
        Dim tuple As New Tuple(Of Integer, String)(focusedBudgetItem.Id, focusedBudgetItem.Code + " - " + focusedBudgetItem.Name)
        OpenShowDialog(Nothing, tuple)
    End Sub

    ''' <summary>
    ''' Evento para Editar un rubro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarBtnEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarBtnEdit.ItemClick
        Dim focusedBudgetItem As Category = INDtlBudgetItems.GetDataRecordByNode(INDtlBudgetItems.FocusedNode)
        OpenShowDialog(focusedBudgetItem)
    End Sub

    ''' <summary>
    ''' Metodo para cerrar el pop up
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarBtnClose_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarBtnClose.ItemClick
        INDpopUpMenu.HidePopup()
    End Sub


#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' evento que abre el formulario de entidad presupuestal en popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleBudgetEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBudgetEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmBudgetEntities
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                presenter.InitializeBudgetEntity()
                If BudgetEntityId IsNot Nothing Then
                    presenter.InitializeValidity(BudgetEntityId)
                End If
            End Using
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBudgetItems_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleBudgetEntity.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetEntity.QueryPopUp
        If BudgetEntityXpo Is Nothing Then
            presenter.InitializeBudgetEntity()
        End If
    End Sub

#End Region

#Region "PopupMenuShowing"

    ''' <summary>
    ''' Evento que se dispara al presionar click derecho para aparcer el menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtlBudgetItems_PopupMenuShowing(sender As Object, e As DevExpress.XtraTreeList.PopupMenuShowingEventArgs) Handles INDtlBudgetItems.PopupMenuShowing

        'If e.Menu Is Nothing Then
        '    Exit Sub
        'End If
        'e.Menu.Items.Clear()

        'Dim addLevelText As String = "Agregar Nivel"
        'Dim ItemMenuAddLevel As DXMenuItem = New DXMenuItem(addLevelText, AddressOf ContexMenuActions_Click)
        'ItemMenuAddLevel.Tag = "01"

        'Dim consultModifyText As String = "Modificar"
        'Dim ItemMenuConsultModify As DXMenuItem = New DXMenuItem(consultModifyText, AddressOf ContexMenuActions_Click)
        'ItemMenuConsultModify.Tag = "02"

        'e.Menu.Items.Add(ItemMenuAddLevel)
        'e.Menu.Items.Add(ItemMenuConsultModify)




        'Dim row As Category = CType(INDtlBudgetItems.GetDataRecordByNode(INDtlBudgetItems.FocusedNode), Category)
        'If row.FinancialSourceId IsNot Nothing Then
        '    BarManager1.Items.Item(0).Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '    'BarManager1.Items.Item(1).Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        'Else
        '    BarManager1.Items.Item(0).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        '    'BarManager1.Items.Item(1).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        'End If

        'INDpopUpMenu.Manager = BarManager1

    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que coloca el estado en el control de información
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetStatus()
        If ValidityXpo IsNot Nothing AndAlso ValidityXpo.Count > 0 AndAlso Validity IsNot Nothing Then
            Dim item = (From l In ValidityXpo Where l.Id = Validity Select l).FirstOrDefault
            If item IsNot Nothing Then
                Select Case item.Status
                    Case 1
                        Status = obtenerRecurso(Registrada, Eform.BudgetEntities)
                    Case 2
                        Status = obtenerRecurso(Activa, Eform.BudgetEntities)
                    Case 3
                        Status = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                    Case Else
                        Status = String.Empty
                End Select
            End If
        End If
    End Sub


    ''' <summary>
    ''' Metodo encargado de cargar el datasource del treelist
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStructure()
        INDlyBudgetItem.BeginUpdate()
        AsyncLoader(True)
        Dim _structure As XPCollection = presenter.ListCategoryTreeList(Validity, 1)
        ListCategory = New List(Of Category)
        If _structure IsNot Nothing Then
            For Each itemXpo As Infrastructure.Data.Xpo.BudgetRepository.BudgetCategoryXpo In _structure
                Dim _item As New Category
                With _item
                    .Id = itemXpo.Id
                    .Code = itemXpo.Code
                    .AlternativeCode = itemXpo.AlternativeCode
                    If itemXpo.FinancialSourceId IsNot Nothing Then
                        .FinancialSourceId = itemXpo.FinancialSourceId.Id
                        .FinancialSourceDescription = itemXpo.FinancialSourceId.NameCode
                    End If
                    .Name = itemXpo.Name
                    .Auxiliary = itemXpo.Auxiliary
                    .Used = itemXpo.Used
                    If itemXpo.CategoryOwnerId IsNot Nothing Then
                        .CategoryOwnerId = itemXpo.CategoryOwnerId.Id
                    End If
                End With
                ListCategory.Add(_item)
            Next
            AsyncLoader(False)
            ActionOnControls = True
        End If
        INDtlBudgetItems.RefreshDataSource()
        INDtlBudgetItems.ExpandAll()
        INDlyBudgetItem.EndUpdate()
    End Sub

    ''' <summary>
    ''' Metodo que abre el formulario modal para agregar las categorias
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenShowDialog(Optional focusedBudgetItem As Category = Nothing, Optional TupleIdAndCodeNameCategoryParent As Tuple(Of Integer, String) = Nothing)
        If Status = obtenerRecurso(Cerrada, Eform.BudgetEntities) Then
            Mensaje(EeventViewerImages.Advertencia) = "No puede manipular un rubro cuando la vigencia está Cerrada."
            Exit Sub
        End If
        Using Formulario As New FrmShowDialogItems(Validity)
            AddHandler Formulario.UpdateDatasourceTreeList, AddressOf LoadStructure
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Formulario.FocusedBudgetItem = focusedBudgetItem
            Formulario.TupleIdAndCodeNameCategoryParent = TupleIdAndCodeNameCategoryParent
            Formulario.BudgetaryEntityCodeName = INDsleBudgetEntity.Text
            Formulario.ValidityYear = INDsleValidity.Text
            Formulario.StatusValidity = Status
            Formulario.Size = New System.Drawing.Size(1100, 700)
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
            'INDsleBudgetEntity.Enabled = Not value
            'INDsleValidity.Enabled = Not value
            INDbtnAddItem.Enabled = value
            INDtlBudgetItems.Enabled = value
            INDlyBudgetItem.EndUpdate()
            If value Then
                INDbtnAddItem.Focus()
            Else
                INDsleBudgetEntity.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para refrescar el treelist
    ''' </summary>
    ''' <remarks></remarks>
    Sub TreelistRefresh()
        If INDtlBudgetItems.DataSource IsNot Nothing Then
            Dim list As New List(Of Category)
            list = INDtlBudgetItems.DataSource
            INDtlBudgetItems.DataSource = Nothing
            INDtlBudgetItems.DataSource = list
            INDtlBudgetItems.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que carga los searchlookup necesarios
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function Inizialite() As Task
        Using modelB As New MBusqueda
            INDsleBudgetEntity.Properties.DataSource = modelB.ConsultarEntidades(eDataSource.ListBudgetEntitiesXPSCS)
        End Using
        Using modelLevel As New MLevelCategory
            LevelsCategory = Await modelLevel.ListLevelsCategoryAsync
        End Using
        SetFirstOrDefaultBudgetInstitution()
    End Function

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultBudgetInstitution()
        Dim listBudget_Entities As DevExpress.Xpo.XPServerCollectionSource = INDsleBudgetEntity.Properties.DataSource
        Dim Ilist As IListSource = TryCast(listBudget_Entities, IListSource)
        If Ilist.GetList().Count = 1 Then
            INDsleBudgetEntity.EditValue = CType(Ilist.GetList()(0), BudgetBudgetInstitutionsXpo).Id
        End If
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If ValidityXpo IsNot Nothing AndAlso ValidityXpo.Count > 0 Then
            Dim item = (From l In ValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                Validity = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Sub CleanControls()
        INDlyBudgetItem.BeginUpdate()
        ActionOnControls = False
        BudgetEntityId = Nothing
        Validity = Nothing
        ValidityXpo = Nothing
        ListCategory = Nothing
        Status = String.Empty
        ctrTmp.RefreshInfo()
        BarraBotones.StatusRecordVisible = False
        INDsleBudgetEntity.Focus()
        INDlyBudgetItem.EndUpdate()
    End Sub

#End Region

#Region "ICrud Base"
    ''' <summary>
    ''' Metodos Crud sin usarse, por eso estan vacios
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
    ''' Propiedad para mostrar los mensajes en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
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
    ''' <summary>
    ''' Metodo Nuevo sin usarse
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub
    ''' <summary>
    ''' Metodo OpenSearch sin usar
    ''' </summary>
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
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, Validity.Value, 0, Validity.Value)
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

''' <summary>
''' Enumeración que contiene el tipo de gasto
''' </summary>
''' <remarks></remarks>
Public Enum EItemType As Integer
    ''' <summary>
    ''' Ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Earning = 1
    ''' <summary>
    ''' Gasto
    ''' </summary>
    ''' <remarks></remarks>
    Expense = 2
End Enum