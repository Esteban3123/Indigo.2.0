#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports DevExpress.LookAndFeel
Imports DevExpress.Skins
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraLayout
Imports System.Windows.Forms
Imports DevExpress.XtraLayout.Scrolling
Imports System.Runtime.InteropServices
Imports DevExpress.XtraGrid

#End Region

''' <summary>
''' Control de navegación horizontal
''' </summary>
<ToolboxItem(True), DXToolboxItem(True)>
Public Class CtrNavigationControlPanel
    Inherits PanelControl

#Region "Consts"

    ''' <summary>
    ''' Ancho fijo del control
    ''' </summary>
    Private Const WIDTH_SIZE As Int32 = 200

#End Region

#Region "Fields"

    ''' <summary>
    ''' LayoutControl que contiene los grupos a navegar
    ''' </summary>
    Private _layoutControl As LayoutControl

    ''' <summary>
    ''' Bandera que indica si el control ya se cargo
    ''' </summary>
    Private _loadedFlag As Boolean

    ''' <summary>
    ''' Grupo que tenia anteriormente el foco actualmente
    ''' </summary>
    Private _beforeFocusedGroup As LayoutControlGroup

    ''' <summary>
    ''' Grupo que tiene el foco actualmente
    ''' </summary>
    Private _focusedGroup As LayoutControlGroup

    ''' <summary>
    ''' Referencia a las barras de scroll del LayoutControl
    ''' </summary>
    Private _ScrollBarInfo As ScrollInfo

    ''' <summary>
    ''' Bandera que indica si el evento GotFocus se ha disparado
    ''' </summary>
    Private _gotFocusFlag As Boolean

    ''' <summary>
    ''' Bandera que me indica si el evento focus
    ''' fue invocado desde el metodo SetFocusGroup
    ''' </summary>
    Private _invokeFocuseFlag As Boolean
#End Region

#Region "Properties"
    Private _layoutControlGroupNavigation As LayoutControlGroup
    <Browsable(False)>
    Public Property LayoutControlGroupNavigation() As LayoutControlGroup
        Get
            Return _layoutControlGroupNavigation
        End Get
        Set(ByVal value As LayoutControlGroup)
            _layoutControlGroupNavigation = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el LayoutControl a navegar
    ''' </summary>
    ''' <value>LayoutControl a navegar</value>
    ''' <returns>El LayoutControl a navegar</returns>
    <Description("Obtiene o asigna el LayoutControl a navegar")>
    Public Property LayoutControl As LayoutControl
        Get
            Return Me._layoutControl
        End Get
        Set(value As LayoutControl)
            Me._layoutControl = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el grupo que tiene el foco actualmente
    ''' </summary>
    ''' <returns>El grupo que tiene actualmente el foco</returns>
    <Browsable(False)>
    Public Property FocusedGroup As LayoutControlGroup
        Get
            Return Me._focusedGroup
        End Get
        Private Set(value As LayoutControlGroup)
            If Not Object.ReferenceEquals(Me._focusedGroup, value) Then
                Me._beforeFocusedGroup = Me._focusedGroup
                Me._focusedGroup = value
                OnFocusedGroupChanged(Me, New FocusedGroupChangedEventArgs(Me._beforeFocusedGroup, Me._focusedGroup))
            End If
            'Si no esta a la vista del usuario, se muestra
            If Me._focusedGroup IsNot Nothing AndAlso Me.IsIntoHView(Me._layoutControl.ClientRectangle, Me._focusedGroup.ViewInfo.BoundsRelativeToControl) <> 0 Then
                InvokeIfHandleCreated(Sub() SetLayoutGroupIntoView(Me._focusedGroup))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el grupo que tenia anteriormente el foco
    ''' </summary>
    ''' <returns>Grupo enfocado anteriormente</returns>
    Public ReadOnly Property BeforeFocusedGroup As LayoutControlGroup
        Get
            Return Me._beforeFocusedGroup
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        MyBase.New()
        If Not DesignMode Then
            InitializeProperties()
        End If
        FindFirstLayoutControl()
        Me.LayoutControlGroupNavigation = New LayoutControlGroup()
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Se dispara cuando el grupo que tiene el foco cambia. Esto ocurre cuando
    ''' uno de sus controles gana el foco
    ''' </summary>
    ''' <param name="sender">Objeto que lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event FocusedGroupChanged(ByVal sender As Object, ByVal e As FocusedGroupChangedEventArgs)

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se controla el estilo asignado al control cuando el skin cambia
    ''' </summary>
    Private Sub UserLookAndFeel_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    ''' <summary>
    ''' Aqui se invoca la carga del control de navegación
    ''' </summary>
    Private Sub CtrNavigationControlPanel_Layout(sender As Object, e As System.Windows.Forms.LayoutEventArgs) Handles Me.Layout
        If Not DesignMode Then
            LoadNavBar()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se control el evento click en los grupos de navegación
    ''' </summary>
    Private Sub NavGroup_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetFocusGroup(DirectCast(sender, LabelControl).Tag.Item1)
    End Sub

    ''' <summary>
    ''' Aqui se control el evento GotFocus de cada uno de los controles en el LayoutControl
    ''' </summary>
    Private Sub Control_GotFocus(ByVal sender As Object, ByVal e As EventArgs)
        If Me._invokeFocuseFlag Then
            Me._invokeFocuseFlag = False
            Me._gotFocusFlag = True
        End If
        Me.FocusedGroup = DirectCast(DirectCast(sender, Control).Tag, LayoutControlGroup)
    End Sub

    ''' <summary>
    ''' Aqui se controla el presionado de teclas en los controles
    ''' </summary>
    Private Sub Control_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If Me._focusedGroup IsNot Nothing AndAlso Me.IsIntoHView(Me._layoutControl.ClientRectangle, Me._focusedGroup.ViewInfo.BoundsRelativeToControl) <> 0 Then
            InvokeIfHandleCreated(Sub() SetLayoutGroupIntoView(Me._focusedGroup))
        Else

            ' Esta parte de aca es la nueva (Copyringth DRoldan)
            'Lo q se hace es revisar si se dio Enter y a su vez tiene activa la propiedad EnterMoveNextControl
            If e.KeyData = Keys.Enter AndAlso TypeOf sender Is BaseEdit AndAlso sender.EnterMoveNextControl Then
                ' Si es asi se busca a que grupo pertenece el control, eso se saca mediante la propiedad Tag del control
                ' y por que por el tag?? ps porque asi lo hicieron, por ahi en algun lado esta eso
                ' ya teniendo el grupo primero reviso si el grupo pertenece a un tabGroup o no y además si el control q se esta dando enter es el último del grupo
                ' si es así entonces tambien se revisa que no sea la última pestaña

                Dim group As LayoutControlGroup = sender.Tag
                If group IsNot Nothing AndAlso group.IsInTabbedGroup AndAlso CType(sender, BaseEdit).Name.Equals(GetLastControlInGroup(group).Name) _
                    AndAlso CType(group.ParentTabbedGroup, TabbedControlGroup).TabPages.Count - 1 > CType(group.ParentTabbedGroup, TabbedControlGroup).SelectedTabPageIndex Then

                    ' Si todo lo anterior se cumple entonces se selecciona la siguiente pestaña (.SelectedTabPageIndex += 1)
                    ' Luego se busca el grupo que quedo seleccionado (pestaña) y se envia el foco al primero control
                    ' e.handled = True es para que no se dispare el foco por defecto sino que se envia manualmente

                    e.Handled = True
                    CType(group.ParentTabbedGroup, TabbedControlGroup).SelectedTabPageIndex += 1
                    FocusToFirstPositionInlayoutGroup(CType(CType(group.ParentTabbedGroup, TabbedControlGroup).SelectedTabPage, LayoutControlGroup))
                Else

                    ' Si algo de lo anterior no se comple entonces se identifica si es el ultimo control del grupo
                    ' si es así entonces se ubica el siguiente grupo y se envia el foco al primero control
                    ' si el siguiente no es un grupo sino un Tabbedgroup ps se ubica el primero grupo (pestaña) y se envia el foco al primer control

                    If CType(sender, BaseEdit).Name.Equals(GetLastControlInGroup(group).Name) Then
                        ' Es el ultimo item del grupo, hay que enviarlo al siguiente grupo
                        Dim currentIndex As Integer = LayoutControl.Items.IndexOf(group)
                        Dim index As Integer = currentIndex + 1
                        While LayoutControl.Items.Count > index AndAlso TypeOf LayoutControl.Items(index) Is LayoutControlItem
                            index += 1
                        End While
                        If LayoutControl.Items.Count <= index Then
                            Exit Sub
                        End If
                        Dim itemLayout As Object = LayoutControl.Items(index)
                        If TypeOf itemLayout Is TabbedControlGroup Then
                            CType(itemLayout, TabbedControlGroup).SelectedTabPageIndex = 0
                            e.Handled = True
                            FocusToFirstPositionInlayoutGroup(CType(itemLayout, TabbedControlGroup).TabPages(0))
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Envia el foco al primero control del grupo
    ''' </summary>
    ''' <param name="group"></param>
    Private Sub FocusToFirstPositionInlayoutGroup(group As LayoutControlGroup)
        GetFirstControlInGroup(group).Focus()
        'Dim control As Control
        'Dim yminimo As Integer = 99999 'group.Items.Where(Function(m) TypeOf m Is LayoutControlItem).ToList()(0).Location.Y
        'Dim ymaximo As Integer = 0 'group.Items.Where(Function(m) TypeOf m Is LayoutControlItem).ToList()(0).Location.Y
        'Dim minimo As Integer = 0
        'Dim maximo As Integer = 0
        'For Each item In group.Items
        '    If TypeOf item Is LayoutControlItem Then
        '        If CType(item, LayoutControlItem).Control.Location.Y < yminimo Then
        '            yminimo = CType(item, LayoutControlItem).Control.Location.Y
        '            minimo = group.Items.IndexOf(item)
        '        End If
        '        If CType(item, LayoutControlItem).Control.Location.Y > ymaximo Then
        '            ymaximo = CType(item, LayoutControlItem).Control.Location.Y
        '            maximo = group.Items.IndexOf(item)
        '        End If
        '    End If
        'Next
        'If group.Items.Count <= minimo Then
        '    Exit Sub
        'End If
        ''Dim primero = CType(itemLayout, TabbedControlGroup).TabPages(0).Items(minimo).Name
        ''Dim ultimo = CType(itemLayout, TabbedControlGroup).TabPages(0).Items(maximo).Name
        'control = CType(group.Items(minimo), LayoutControlItem).Control
        'control.Focus()
    End Sub

    ''' <summary>
    ''' Busca el último control del grupo
    ''' </summary>
    ''' <param name="group"></param>
    ''' <returns></returns>
    Private ReadOnly Property GetLastControlInGroup(group As LayoutControlGroup) As Control
        Get
            Dim ymaximo As Integer = 0
            Dim maximo As Integer = 0
            For Each item In group.Items
                If TypeOf item Is LayoutControlItem AndAlso CType(item, LayoutControlItem).Visibility = Utils.LayoutVisibility.Always Then
                    If CType(item, LayoutControlItem).Control IsNot Nothing AndAlso TypeOf CType(item, LayoutControlItem).Control IsNot LabelControl AndAlso CType(item, LayoutControlItem).Control.Location.Y > ymaximo Then
                        ymaximo = CType(item, LayoutControlItem).Control.Location.Y
                        maximo = group.Items.IndexOf(item)
                    End If
                End If
            Next
            If group.Items.Count <= maximo Then
                Return New Control()
            End If
            Return CType(group.Items(maximo), LayoutControlItem).Control
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el primer control del grupo
    ''' </summary>
    ''' <param name="group"></param>
    ''' <returns></returns>
    Private ReadOnly Property GetFirstControlInGroup(group As LayoutControlGroup) As Control
        Get
            If group Is Nothing Then Return New Control()
            Dim yminimo As Integer = 99999
            Dim minimo As Integer = 0
            For Each item In group.Items
                If TypeOf item Is LayoutControlItem AndAlso CType(item, LayoutControlItem).Visibility = Utils.LayoutVisibility.Always Then
                    If TypeOf CType(item, LayoutControlItem).Control IsNot LabelControl AndAlso CType(item, LayoutControlItem).Control IsNot Nothing AndAlso CType(item, LayoutControlItem).Control.Location.Y < yminimo Then
                        yminimo = CType(item, LayoutControlItem).Control.Location.Y
                        minimo = group.Items.IndexOf(item)
                    End If
                End If
            Next
            If group.Items.Count <= minimo Then
                Return New Control()
            End If
            Return CType(group.Items(minimo), LayoutControlItem).Control
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el primer LayoutControlItem del grupo
    ''' </summary>
    ''' <param name="group"></param>
    ''' <returns></returns>
    Private ReadOnly Property GetFirstLayoutControlItemInGroup(group As LayoutControlGroup) As LayoutControlItem
        Get
            If group Is Nothing Then Return New LayoutControlItem()
            Dim yminimo As Integer = 99999
            Dim minimo As Integer = 0
            For Each item In group.Items
                If TypeOf item Is LayoutControlItem AndAlso CType(item, LayoutControlItem).Visibility = Utils.LayoutVisibility.Always Then
                    If TypeOf CType(item, LayoutControlItem).Control IsNot LabelControl AndAlso CType(item, LayoutControlItem).Control IsNot Nothing AndAlso CType(item, LayoutControlItem).Control.Location.Y < yminimo Then
                        yminimo = CType(item, LayoutControlItem).Control.Location.Y
                        minimo = group.Items.IndexOf(item)
                    End If
                End If
            Next
            If group.Items.Count <= minimo Then
                Return New LayoutControlItem()
            End If
            Return CType(group.Items(minimo), LayoutControlItem)
        End Get
    End Property

    ''' <summary>
    ''' Aqui se control el movimiento de la rueda del Mouse para mover el scroll
    ''' </summary>
    Private Sub CtrNavigationControlPanel_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        FindHTLScrolling()
        If sender IsNot Nothing AndAlso TypeOf sender Is Control AndAlso Me._ScrollBarInfo IsNot Nothing Then
            Dim ctr As Control = DirectCast(sender, Control)
            DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = True
            Dim scrollValue As Int32 = Me._ScrollBarInfo.HScrollPos
            Dim smallChange As Int32 = Me._ScrollBarInfo.HScrollArgs.SmallChange
            If e.Delta < 0 Then
                Dim delta = scrollValue + smallChange
                Me._ScrollBarInfo.HScrollPos = Math.Min(delta, Me._ScrollBarInfo.HScrollArgs.Maximum)
            Else
                If scrollValue < smallChange Then
                    Me._ScrollBarInfo.HScrollPos = 0
                Else
                    Me._ScrollBarInfo.HScrollPos -= smallChange
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se controla cuando se muestra u oculta un grupo
    ''' </summary>
    Private Sub LayoutGroup_HiddenShown(ByVal sender As Object, ByVal e As EventArgs)
        If Me._loadedFlag AndAlso sender IsNot Nothing AndAlso TypeOf sender Is LayoutControlGroup Then
            Dim group As LayoutControlGroup = DirectCast(sender, LayoutControlGroup)
            If group.Tag IsNot Nothing AndAlso TypeOf group.Tag Is LabelControl Then
                Dim _LabelControl As LabelControl = DirectCast(group.Tag, LabelControl)
                If group.Visibility = Utils.LayoutVisibility.Always Then
                    _LabelControl.Visible = True
                Else
                    _LabelControl.Visible = False
                End If
                If _LabelControl.Tag IsNot Nothing Then
                    DirectCast(_LabelControl.Tag.item3, LayoutControlItem).Visibility = group.Visibility
                End If
                RenameNavGroups()
                'Si no esta a la vista del usuario, se muestra
                If Me._focusedGroup IsNot Nothing AndAlso Me.IsIntoHView(Me._layoutControl.ClientRectangle, Me._focusedGroup.ViewInfo.BoundsRelativeToControl) <> 0 Then
                    InvokeIfHandleCreated(Sub() SetLayoutGroupIntoView(Me._focusedGroup))
                End If
            End If
        End If
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Aplica el estilo al control dependiendo del Skin seleccionado
    ''' </summary>
    ''' <param name="skinName">Nombre del Skin seleccionado</param>
    Private Sub ApplyStyleSkin(ByVal skinName As String)
        'If skinName.Equals(Infrastructure.CrossCutting.Base.Utils.DEFAULT_SKIN_NAME) Then
        '    'Aplicamos estilo al fondo del control
        '    MyBase.Appearance.BorderColor = Color.FromArgb(0, 70, 109)
        '    MyBase.Appearance.BackColor = Color.FromArgb(0, 70, 109)
        '    MyBase.Appearance.BackColor2 = Color.FromArgb(0, 70, 109)
        '    MyBase.Appearance.Image = Nothing
        '    'Aplicamos estilo a todos los items del control
        '    For Each lb As Control In Me.Controls
        '        If TypeOf lb Is LabelControl Then
        '            Dim navGroup As LabelControl = DirectCast(lb, LabelControl)
        '            navGroup.Appearance.Font = New Font("Segoe UI", 10.0!)
        '            navGroup.Appearance.ForeColor = Color.White
        '            navGroup.Appearance.BackColor = Color.Transparent
        '            navGroup.Appearance.BackColor2 = Color.Transparent
        '            navGroup.Appearance.Image = Nothing
        '        End If
        '    Next
        'Else
        Dim skin = NavBarSkins.GetSkin(UserLookAndFeel.Default.ActiveLookAndFeel)
        'Aplicamos estilo al fondo del control
        Dim backgroundElement = skin(NavBarSkins.SkinBackground)
        If backgroundElement IsNot Nothing Then
            'MyBase.Appearance.BackColor = If(backgroundElement.Color IsNot Nothing, backgroundElement.Color.BackColor, Color.White)
            'MyBase.Appearance.BackColor2 = If(backgroundElement.Color IsNot Nothing, backgroundElement.Color.BackColor2, Color.White)
            'MyBase.Appearance.BackColor = Color.Transparent
            'MyBase.Appearance.BackColor2 = Color.Transparent

            MyBase.Appearance.Image = If(backgroundElement.HasImage, backgroundElement.GetActualImage(), Nothing)
            If MyBase.Appearance.Image Is Nothing Then
                Me.LayoutControlGroupNavigation.ContentImageOptions.Image = Nothing

            Else
                Me.LayoutControlGroupNavigation.ContentImageOptions.Image = MyBase.Appearance.GetImage
                Me.LayoutControlGroupNavigation.ContentImageOptions.Alignment = ContentAlignment.BottomLeft
            End If

            'CType(CType(Me.Controls(0), LayoutControl).Items(1), LayoutControlGroup).BackgroundImageOptions.Image = If(backgroundElement.HasImage, backgroundElement.GetActualImage(), Nothing)

            MyBase.Appearance.ForeColor = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor


        End If
        'Aplicamos estilo a todos los items del control
        Dim groupElement = skin(NavBarSkins.SkinGroupHeader)
        If groupElement IsNot Nothing Then
            Dim lb As LabelControl
            'For Each item In DirectCast(Me.Controls(0), LayoutControl).Items 'grupo
            '    If TypeOf item Is LayoutControlGroup Then
            For Each itemgroup In Me.LayoutControlGroupNavigation.Items 'DirectCast(item, LayoutControlGroup).Items 'grupo
                If (TypeOf itemgroup Is LayoutControlItem) AndAlso (TypeOf CType(itemgroup, LayoutControlItem).Control Is LabelControl) Then
                    lb = CType(itemgroup, LayoutControlItem).Control
                    Dim navGroup As LabelControl = DirectCast(lb, LabelControl)
                    If navGroup.Tag IsNot Nothing Then
                        navGroup.Appearance.Font = New Font("Segoe UI", 10.0!)
                    End If

                    'navGroup.Appearance.ForeColor = If(groupElement.Color IsNot Nothing, groupElement.Color.ForeColor, Color.White)
                    If UserLookAndFeel.Default.ActiveLookAndFeel.ActiveSkinName = "Pumpkin" Then
                        navGroup.Appearance.ForeColor = Color.White
                    Else
                        navGroup.Appearance.ForeColor = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor
                    End If
                    'navGroup.Appearance.BackColor = If(groupElement.Color IsNot Nothing, groupElement.Color.BackColor, Color.Transparent)
                    'navGroup.Appearance.BackColor2 = If(groupElement.Color IsNot Nothing, groupElement.Color.BackColor2, Color.Transparent)
                    navGroup.Appearance.BackColor = Color.Transparent
                    navGroup.Appearance.BackColor2 = Color.Transparent
                    'navGroup.Appearance.Image = If(groupElement.HasImage, SetHorizontalStrench(groupElement.GetActualImage(), navGroup.Width), Nothing)
                    navGroup.Appearance.Image = Nothing
                End If
            Next
        End If

        SetFocusedStyleNavGroup(Me._focusedGroup)
    End Sub

    ''' <summary>
    ''' Carga la barra de navehación a partir de los grupos en el LayoutControl
    ''' </summary>
    Private Sub LoadNavBar()
        If Me._layoutControl IsNot Nothing AndAlso Me._layoutControl.Items IsNot Nothing AndAlso Me._layoutControl.Items.Count > 1 AndAlso Not Me._loadedFlag Then
            Dim CountGroups As Int32 = 0
            Dim firsControl As Control = Nothing

            Dim _layoutControlNavigation = New LayoutControl

            Dim navGroup As New LabelControl()

            _layoutControlNavigation.Dock = DockStyle.Fill

            Me.LayoutControlGroupNavigation.Text = "Panel de navegación"
            Me.LayoutControlGroupNavigation.AppearanceGroup.BackColor = Color.Transparent


            AddHandler Me._layoutControl.MouseWheel, AddressOf CtrNavigationControlPanel_MouseWheel
            'Recorremos todos los grupos en el LayoutControl, saltandonos el primero, el cual es el Grupo principal
            For i = 0 To Me._layoutControl.Root.Items.Count - 1
                If TypeOf Me._layoutControl.Root.Items(i) Is LayoutControlGroup Then
                    Dim currentLayoutControlGroup As LayoutControlGroup = DirectCast(Me._layoutControl.Root.Items(i), LayoutControlGroup)
                    'Se agregan los manejadores para el evento en que se muestra u oculta el grupo
                    AddHandler currentLayoutControlGroup.Shown, AddressOf LayoutGroup_HiddenShown
                    AddHandler currentLayoutControlGroup.Hidden, AddressOf LayoutGroup_HiddenShown
                    CountGroups += 1
                    'Recorremos todos los LayoutControlItems del Grupo
                    SetEventToControl(currentLayoutControlGroup, currentLayoutControlGroup, firsControl)
                    'Agregamos un nuevo controlde grupo a la barra de navegación
                    navGroup = New LabelControl()
                    AddHandler navGroup.Click, AddressOf NavGroup_Click
                    navGroup.Appearance.Font = New Font("Segoe UI", 10.0!)
                    navGroup.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
                    navGroup.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                    navGroup.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                    navGroup.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisWord
                    navGroup.AutoSizeMode = LabelAutoSizeMode.Vertical
                    navGroup.Dock = System.Windows.Forms.DockStyle.Top
                    navGroup.Cursor = System.Windows.Forms.Cursors.Hand
                    navGroup.Padding = New System.Windows.Forms.Padding(0, 0, 0, 0)
                    navGroup.Margin = New Padding(0)
                    navGroup.Text = CountGroups.ToString() & ". " & currentLayoutControlGroup.Text
                    navGroup.MinimumSize = New Size(150, 35)
                    navGroup.Visible = If(currentLayoutControlGroup.Visibility = Utils.LayoutVisibility.Always, True, False)
                    currentLayoutControlGroup.Tag = navGroup
                    navGroup.BringToFront()
                    Me.LayoutControlGroupNavigation.AddItem(navGroup.Text, navGroup)
                    navGroup.Tag = New Tuple(Of LayoutControlGroup, Int32, LayoutControlItem)(currentLayoutControlGroup, currentLayoutControlGroup.Size.Width, Me.LayoutControlGroupNavigation.Items.LastOrDefault())
                    Me.LayoutControlGroupNavigation.Items.LastOrDefault().TextVisible = False
                ElseIf TypeOf Me._layoutControl.Root.Items(i) Is TabbedControlGroup Then
                    For j = 0 To CType(Me._layoutControl.Root.Items(i), TabbedControlGroup).TabPages.Count - 1
                        If TypeOf CType(Me._layoutControl.Root.Items(i), TabbedControlGroup).TabPages(j) Is LayoutControlGroup Then
                            Dim currentLayoutControlGroup As LayoutControlGroup = DirectCast(CType(Me._layoutControl.Root.Items(i), TabbedControlGroup).TabPages(j), LayoutControlGroup)
                            'Se agregan los manejadores para el evento en que se muestra u oculta el grupo
                            AddHandler currentLayoutControlGroup.Shown, AddressOf LayoutGroup_HiddenShown
                            AddHandler currentLayoutControlGroup.Hidden, AddressOf LayoutGroup_HiddenShown
                            CountGroups += 1
                            'Recorremos todos los LayoutControlItems del Grupo
                            SetEventToControl(currentLayoutControlGroup, currentLayoutControlGroup, firsControl)
                            'Agregamos un nuevo controlde grupo a la barra de navegación
                            navGroup = New LabelControl()
                            AddHandler navGroup.Click, AddressOf NavGroup_Click
                            navGroup.Appearance.Font = New Font("Segoe UI", 10.0!)
                            navGroup.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
                            navGroup.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                            navGroup.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                            navGroup.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisWord
                            navGroup.AutoSizeMode = LabelAutoSizeMode.Vertical
                            navGroup.Dock = System.Windows.Forms.DockStyle.Top
                            navGroup.Cursor = System.Windows.Forms.Cursors.Hand
                            'navGroup.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
                            navGroup.Padding = New System.Windows.Forms.Padding(0, 0, 0, 0)
                            navGroup.Margin = New Padding(0)
                            navGroup.Text = CountGroups.ToString() & ". " & currentLayoutControlGroup.Text
                            navGroup.MinimumSize = New Size(150, 35)
                            navGroup.Visible = If(currentLayoutControlGroup.Visibility = Utils.LayoutVisibility.Always, True, False)
                            currentLayoutControlGroup.Tag = navGroup
                            navGroup.BringToFront()
                            Me.LayoutControlGroupNavigation.AddItem(navGroup.Text, navGroup)
                            navGroup.Tag = New Tuple(Of LayoutControlGroup, Int32, LayoutControlItem)(currentLayoutControlGroup, currentLayoutControlGroup.Size.Width, Me.LayoutControlGroupNavigation.Items.LastOrDefault())
                            Me.LayoutControlGroupNavigation.Items.LastOrDefault().TextVisible = False
                        End If
                    Next
                End If
            Next
            _layoutControlNavigation.AddGroup(Me.LayoutControlGroupNavigation)
            Me.Controls.Add(_layoutControlNavigation)
            'Buscamos el control de scroll horizontal
            FindHTLScrolling()
            'Aplicamos el estilo del tema seleccionado
            ApplyStyleSkin(UserLookAndFeel.Default.ActiveSkinName)
            'Asignamos el foco al primer control el primer grupo
            'firsControl.Focus()
            FocusToFirstPositionInlayoutGroup(firsControl.Tag)
            Me._loadedFlag = True
        End If
    End Sub

    ''' <summary>
    ''' Asigna los eventos a los controles del grupo
    ''' </summary>
    ''' <param name="item">Item que contiene los controles</param>
    ''' <param name="currentGroup">Grupo principal actual</param>
    ''' <param name="firstControl">Primer control</param>
    Private Sub SetEventToControl(ByVal item As BaseLayoutItem, ByVal currentGroup As LayoutGroup, ByRef firstControl As Control)
        If item IsNot Nothing Then
            If TypeOf item Is LayoutGroup Then
                Dim group As LayoutGroup = DirectCast(item, LayoutGroup)
                For Each it In group.Items
                    SetEventToControl(it, currentGroup, firstControl)
                Next
            ElseIf TypeOf item Is TabbedGroup Then
                Dim tabbed As TabbedGroup = DirectCast(item, TabbedGroup)
                For Each it In tabbed.TabPages
                    SetEventToControl(it, currentGroup, firstControl)
                Next
            ElseIf TypeOf item Is LayoutItem Then
                Dim it As LayoutControlItem = DirectCast(item, LayoutControlItem)
                If it.Control IsNot Nothing Then
                    it.Control.Tag = currentGroup
                    'Se asigna manejadores a los eventos del control
                    AddHandler it.Control.GotFocus, AddressOf Control_GotFocus
                    'No se asigna manejador a la rueda del mouse si es una rejilla
                    If Not TypeOf it.Control Is GridControl Then
                        AddHandler it.Control.MouseWheel, AddressOf CtrNavigationControlPanel_MouseWheel
                    End If
                    If it.Control.GetType().GetEvents().Any(Function(e) e.Name.Equals("KeyDown")) Then
                        ' En esta linea se busca el evento keydown de cada control el el layout y se le enlaza a Control_KeyDown
                        it.Control.GetType().GetEvent("KeyDown").AddEventHandler(it.Control, New KeyEventHandler(AddressOf Control_KeyDown))
                    End If
                    If firstControl Is Nothing Then
                        firstControl = it.Control
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Estira una imagen de forma horizontal
    ''' </summary>
    ''' <param name="img">Imagen a estirar</param>
    ''' <param name="maxWidth">Ancho maximo a aplicar en la imagen</param>
    ''' <returns>Imagen estirada</returns>
    Private Function SetHorizontalStrench(ByVal img As Image, ByVal maxWidth As Int32) As Image
        If img.Width < maxWidth Then
            Dim img1 As New Bitmap(CType(img.Clone(), Image))
            Dim img2 As New Bitmap(maxWidth, img.Height)
            'Hallamos la mitad de la imagen
            Dim midImg As Int32 = (img.Width / 2)
            'Vaciamos la mitad de la imagen a la nueva imagen
            For Xcount As Int32 = 0 To midImg - 1
                For Ycount As Int32 = 0 To img.Height - 1
                    img2.SetPixel(Xcount, Ycount, img1.GetPixel(Xcount, Ycount))
                Next
            Next
            'Hallamos lo que falta por estirar la imagen
            Dim totalStrench As Int32 = (maxWidth - img.Width)
            'Repetimos el centro de la imagen la cantidad de veces faltantes para estirar
            For Xcount As Int32 = midImg To (midImg + totalStrench) - 1
                For Ycount As Int32 = 0 To img.Height - 1
                    img2.SetPixel(Xcount, Ycount, img1.GetPixel(midImg, Ycount))
                Next
            Next
            'Completamos la imagen con la parte final
            For Xcount As Int32 = (midImg + totalStrench) To img2.Width - 1
                For Ycount As Int32 = 0 To img.Height - 1
                    img2.SetPixel(Xcount, Ycount, img1.GetPixel(img2.Width - Xcount, Ycount))
                Next
            Next
            Return img2
        End If
        Return img
    End Function

    ''' <summary>
    ''' Inicializa las propiedades visuales por defecto
    ''' del control, configurando su apariencia
    ''' </summary>
    Private Sub InitializeProperties()
        MyBase.Size = New Size(WIDTH_SIZE, MyBase.Size.Height)
        MyBase.BorderStyle = BorderStyles.NoBorder
        MyBase.Margin = New Padding(0)
        MyBase.Padding = New Padding(0)
        MyBase.UseDisabledStatePainter = False
        AddHandler UserLookAndFeel.Default.StyleChanged, AddressOf UserLookAndFeel_StyleChanged
    End Sub

    ''' <summary>
    ''' Busca el primer LayoutControl para usarlo como
    ''' control de navehación
    ''' </summary>
    Private Sub FindFirstLayoutControl()
        If Me.Parent IsNot Nothing AndAlso Me._layoutControl Is Nothing Then
            For Each c In Me.Parent.Controls
                If TypeOf c Is LayoutControl Then
                    Me._layoutControl = DirectCast(c, LayoutControl)
                    Exit For
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Busca el control de scroll horizontal
    ''' </summary>
    Private Sub FindHTLScrolling()
        If Me._layoutControl IsNot Nothing AndAlso Me._ScrollBarInfo Is Nothing Then
            Me._ScrollBarInfo = DirectCast(Me._layoutControl, ILayoutControl).Scroller
        End If
    End Sub

    ''' <summary>
    ''' Obtiene un valor que indica si un rectangulo se encuentra a la vista de forma horizontal
    ''' </summary>
    ''' <param name="rec">Rectangulo uno</param>
    ''' <param name="p">Rectangulo dos</param>
    ''' <returns>Valor que indica si el rectangulo dos esta a la vista</returns>
    Public Function IsIntoHView(ByVal rec As Rectangle, ByVal p As Rectangle) As Int32
        If rec.X <= p.X And (rec.X + rec.Width) >= (p.X + p.Width) Then 'Esta contenido entre el rectangulo
            Return 0
        End If
        If rec.X > p.X Then 'Esta a la izquierda del rectangulo
            Return -1
        End If
        Return 1 'Esta a la derecha del rectangulo
    End Function

    ''' <summary>
    ''' Calcula la diferencia horizontal entre dos puntos
    ''' </summary>
    ''' <param name="rec">Punto uno</param>
    ''' <param name="p">Punto dos</param>
    ''' <returns>Diferencia entre los dos puntos</returns>
    Public Function CalcDiffHView(ByVal rec As Point, ByVal p As Point) As Int32
        Dim diff As Int32 = Math.Abs(rec.X - p.X)
        Return diff
    End Function

    ''' <summary>
    ''' Renombra los grupos de navegación
    ''' </summary>
    Private Sub RenameNavGroups()
        Dim CountGroups As Int32 = 0

        Dim lb As LabelControl
        'For Each item In DirectCast(Me.Controls(0), LayoutControl).Items 'grupo
        '    If TypeOf item Is LayoutControlGroup Then
        For Each itemgroup In Me.LayoutControlGroupNavigation.Items 'DirectCast(item, LayoutControlGroup).Items 'grupo
            If (TypeOf itemgroup Is LayoutControlItem) AndAlso (TypeOf CType(itemgroup, LayoutControlItem).Control Is LabelControl) Then
                lb = CType(itemgroup, LayoutControlItem).Control
                If lb.Visible AndAlso lb.Tag IsNot Nothing Then
                    CountGroups += 1
                    lb.Text = CountGroups.ToString() & ". " & DirectCast(lb.Tag.Item1, LayoutControlGroup).Text
                End If
            End If
        Next
        '    End If
        'Next

        'For i = Me.Controls.Count - 1 To 0
        'If TypeOf Me.Controls(i) Is LabelControl Then
        '        If Me.Controls(i).Visible AndAlso Me.Controls(i).Tag IsNot Nothing Then
        '            CountGroups += 1
        '            Me.Controls(i).Text = CountGroups.ToString() & ". " & DirectCast(Me.Controls(i).Tag.Item1, LayoutControlGroup).Text
        '        End If
        '    End If
        'Next
    End Sub

    ''' <summary>
    ''' Dispara el evento FocusedGroupChanged
    ''' </summary>
    ''' <param name="sender">Objeto que lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Sub OnFocusedGroupChanged(ByVal sender As Object, ByVal e As FocusedGroupChangedEventArgs)
        InvokeIfHandleCreated(Sub() SetLayoutGroupIntoView(e.NewFocusedGroup))
        RaiseEvent FocusedGroupChanged(sender, e)
    End Sub

    ''' <summary>
    ''' Posiciona el LayoutGroup en el área visible del LayoutControl
    ''' </summary>
    ''' <param name="layoutControlGroup">LayoutGroup a mostrar</param>
    ''' <param name="setStyleNavGroup">Valor que indica si se le da estilo al grupo de navegación</param>
    Private Sub SetLayoutGroupIntoView(ByVal layoutControlGroup As LayoutControlGroup, Optional ByVal setStyleNavGroup As Boolean = True)
        FindHTLScrolling()
        'Calculamos la nueva posición del scroll horizontal hasta el nuevo grupo enfocado
        If Me._layoutControl IsNot Nothing AndAlso Me._ScrollBarInfo IsNot Nothing Then
            Dim lyg = layoutControlGroup
            Dim lyc = Me._layoutControl
            If lyg IsNot Nothing AndAlso lyg.ViewInfo IsNot Nothing Then
                Dim pos = Me.IsIntoHView(lyc.ClientRectangle, lyg.ViewInfo.BoundsRelativeToControl)
                If pos = 1 Then 'Derecha
                    Dim p2 As New Point(lyc.ClientRectangle.X + lyc.ClientRectangle.Width, lyc.ClientRectangle.Y)
                    Me._ScrollBarInfo.HScrollPos = Me._ScrollBarInfo.HScrollPos + Me.CalcDiffHView(p2, New Point(lyg.ViewInfo.BoundsRelativeToControl.X + lyg.ViewInfo.BoundsRelativeToControl.Width, lyg.ViewInfo.BoundsRelativeToControl.Y))
                End If
                If pos = -1 Then 'Izquierda
                    Me._ScrollBarInfo.HScrollPos = Me._ScrollBarInfo.HScrollPos - Me.CalcDiffHView(lyc.ClientRectangle.Location, lyg.ViewInfo.BoundsRelativeToControl.Location)
                End If
                If setStyleNavGroup Then
                    'Cambiamos el estilo al grupo de navegación seleccionado
                    SetFocusedStyleNavGroup(lyg)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Asigna el estilo correspondiente al grupo de navegación
    ''' </summary>
    ''' <param name="GroupFocused">Grupo al que pertenece el grupo de navegación a cambiar</param>
    Private Sub SetFocusedStyleNavGroup(ByVal GroupFocused As LayoutControlGroup)
        If GroupFocused IsNot Nothing Then
            Dim nav As LabelControl = Nothing
            Dim lb As LabelControl
            'For Each item In DirectCast(Me.Controls(0), LayoutControl).Items 'grupo
            '    If TypeOf item Is LayoutControlGroup Then
            For Each itemgroup In Me.LayoutControlGroupNavigation.Items ' DirectCast(item, LayoutControlGroup).Items 'grupo
                If (TypeOf itemgroup Is LayoutControlItem) AndAlso (TypeOf CType(itemgroup, LayoutControlItem).Control Is LabelControl) Then
                    lb = CType(itemgroup, LayoutControlItem).Control
                    If lb.Tag IsNot Nothing AndAlso Object.ReferenceEquals(GroupFocused, lb.Tag.Item1) Then
                        nav = lb
                        Exit For
                    End If
                End If
            Next
            '    End If
            'Next

            'For Each c In Me.Controls
            '    If TypeOf c Is LabelControl Then
            '        Dim navGroup As LabelControl = DirectCast(c, LabelControl)
            '        If Object.ReferenceEquals(GroupFocused, navGroup.Tag.Item1) Then
            '            nav = navGroup
            '            Exit For
            '        End If
            '    End If
            'Next
            If nav IsNot Nothing Then
                SetFocusedStyleNavGroup(nav)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Asigna el estilo correspondiente al grupo de navegación
    ''' </summary>
    ''' <param name="navGroupFocus">Grupo de navegación a cambiar</param>
    Private Sub SetFocusedStyleNavGroup(ByVal navGroupFocus As LabelControl)
        Dim lb As LabelControl
        'For Each item In DirectCast(Me.Controls(0), LayoutControl).Items 'grupo
        '    If TypeOf item Is LayoutControlGroup Then
        For Each itemgroup In Me.LayoutControlGroupNavigation.Items ' DirectCast(item, LayoutControlGroup).Items 'grupo
            If (TypeOf itemgroup Is LayoutControlItem) AndAlso (TypeOf CType(itemgroup, LayoutControlItem).Control Is LabelControl) Then
                lb = CType(itemgroup, LayoutControlItem).Control
                If lb.Tag IsNot Nothing AndAlso Object.ReferenceEquals(navGroupFocus, lb) Then
                    lb.Appearance.Font = New Font(lb.Appearance.Font, FontStyle.Bold)
                Else
                    lb.Appearance.Font = New Font(lb.Appearance.Font, FontStyle.Regular)
                End If
            End If
        Next
        'End If
        'Next


        If Me._focusedGroup.ViewInfo IsNot Nothing Then
            'Si no esta a la vista del usuario, se muestra
            If Me._focusedGroup IsNot Nothing AndAlso Me.IsIntoHView(Me._layoutControl.ClientRectangle, Me._focusedGroup.ViewInfo.BoundsRelativeToControl) <> 0 Then
                InvokeIfHandleCreated(Sub() SetLayoutGroupIntoView(Me._focusedGroup, False))
            End If
        End If

    End Sub

    ''' <summary>
    ''' Asigna el foco a un grupo
    ''' </summary>
    ''' <param name="item">Grupo al que se asignará el foco</param>
    Private Sub SetFocusGroup(ByVal item As BaseLayoutItem)
        If TypeOf item Is LayoutGroup Then
            Dim group As LayoutGroup = DirectCast(item, LayoutGroup)
            If group IsNot Nothing AndAlso group.Items.Count > 0 Then
                If group.Items.Any(Function(t) TypeOf t Is LayoutControlItem) Then
                    Dim it = GetFirstLayoutControlItemInGroup(group) 'DirectCast(group.Items.LastOrDefault(Function(t) TypeOf t Is LayoutControlItem AndAlso DirectCast(t, LayoutControlItem).Control IsNot Nothing), LayoutControlItem)
                    'Dim it = DirectCast(group.Items.LastOrDefault(Function(t) TypeOf t Is LayoutControlItem AndAlso DirectCast(t, LayoutControlItem).Control IsNot Nothing), LayoutControlItem)
                    If it IsNot Nothing Then
                        Me._invokeFocuseFlag = True
                        SetFocus(New HandleRef(Me, it.Control.Handle))
                        'InvokeIfHandleCreated(Sub() it.Control.Focus())
                        InvokeIfHandleCreated(Sub() FocusToFirstPositionInlayoutGroup(it.Control.Tag))
                        If Not Me._gotFocusFlag Then 'Si el evento no se ha disparado, lo hacemos a mano
                            Control_GotFocus(it.Control, New EventArgs())
                            Me._layoutControl.ActiveControl = it.Control
                        End If
                        Me._gotFocusFlag = False
                    End If
                ElseIf group.Items.Any(Function(t) TypeOf t Is LayoutGroup OrElse TypeOf t Is TabbedGroup) Then
                    SetFocusGroup(group.Items.LastOrDefault())
                End If
            End If
        ElseIf TypeOf item Is TabbedGroup Then
            Dim tabbed As TabbedGroup = DirectCast(item, TabbedGroup)
            If tabbed.TabPages.Count > 0 Then
                SetFocusGroup(tabbed.TabPages(0))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Asigna el foco a un control
    ''' </summary>
    <DllImport("user32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True)>
    Public Shared Function SetFocus(ByVal hWnd As HandleRef) As IntPtr
    End Function

    ''' <summary>
    ''' Invoca la acción con un BeginInvoke si el Handle de la ventana ha sido creado
    ''' </summary>
    ''' <param name="dl">Acción a ejecutar</param>
    Private Sub InvokeIfHandleCreated(ByVal dl As Action)
        If Me.IsHandleCreated Then
            Me.BeginInvoke(dl)
        Else
            dl()
        End If
    End Sub

    ''' <summary>
    ''' Lanza el evento SizeChanged
    ''' </summary>
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        'Controlamos que el ancho del control no sea mayor al definido en la constante
        MyBase.Size = New Size(WIDTH_SIZE, MyBase.Size.Height)
    End Sub

    Private Sub InitializeComponent()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CtrNavigationControlPanel
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.BackColor2 = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class

''' <summary>
''' Encapsula los argumento del evento FocusedGroupChanged
''' </summary>
Public Class FocusedGroupChangedEventArgs

#Region "Members"

    Private _oldFocusedGroup As LayoutControlGroup
    ''' <summary>
    ''' Obtiene el anterior grupo enfocado
    ''' </summary>
    ''' <returns>Anterior grupo enfocado</returns>
    Public ReadOnly Property OldFocusedGroup As LayoutControlGroup
        Get
            Return Me._oldFocusedGroup
        End Get
    End Property

    Private _newFocusedGroup As LayoutControlGroup
    ''' <summary>
    ''' Obtiene el nuevo grupo enfocado
    ''' </summary>
    ''' <returns>Nuevo grupo enfocado</returns>
    Public ReadOnly Property NewFocusedGroup As LayoutControlGroup
        Get
            Return Me._newFocusedGroup
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="oldFocusedGroup">Anterior grupo enfocado</param>
    ''' <param name="newFocusedGroup">Nuevo grupo enfocado</param>
    Public Sub New(ByVal oldFocusedGroup As LayoutControlGroup, ByVal newFocusedGroup As LayoutControlGroup)
        Me._oldFocusedGroup = oldFocusedGroup
        Me._newFocusedGroup = newFocusedGroup
    End Sub

#End Region

End Class