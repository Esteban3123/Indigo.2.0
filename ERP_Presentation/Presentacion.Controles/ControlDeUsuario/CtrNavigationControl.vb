'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 20 - 05 - 2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports DevExpress.XtraLayout
Imports System.Threading.Tasks
Imports Microsoft.VisualBasic
Imports System.ComponentModel

#End Region


''' <summary>
''' Clase con tada la funcionalidad del control de usuario de navegacion.
''' </summary>
<ToolboxItem(False), DXToolboxItem(False), Obsolete("CtrNavigationControlPanel reemplaza éste control", True)>
Public Class CtrNavigationControl

#Region "Variables Globales"
    ''' <summary>
    ''' Bandera que determina el momento en que se hace mousewhell
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagTimeMouseWhell As DateTime
    ''' <summary>
    ''' Variable que almacena  la posicion de grupo actual
    ''' </summary>
    ''' <remarks></remarks>
    Dim PositionGroup As Integer
    Dim FlagLoadNavigationControl As Boolean
    ''' <summary>
    ''' Manejador del evento valueChanged del scroll del layout
    ''' </summary>
    Dim EventScrolling As System.EventHandler = AddressOf Scrolling
    ''' <summary>
    ''' Variable que contiene el index de la posicion del layout en los controles del parent
    ''' </summary>
    Dim PosicionLayout As Integer
    ''' <summary>
    ''' Variable que contiene el index de la posicion del scrolling en el layout control
    ''' </summary>
    Dim PosicionScroll As Integer
    ''' <summary>
    ''' Bandera que indica si el mouse se encuentra sobre los controles
    ''' </summary>
    Dim isMouseOver As Boolean = False
    ''' <summary>
    ''' Objeto Scroll
    ''' </summary
    Dim HTLScroll As Object

#End Region

#Region "metodos"

    ''' <summary>
    ''' Evento Load del control donde ejectucamos el metodo loadnavigationpanel
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrNavigationControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If DesignMode = False Then
            AddHandler CType(Me.Parent, DevExpress.XtraEditors.PanelControl).Layout, AddressOf LoadNavigationPanel
            DelegateMouseWheel()
        End If
    End Sub

    Private Sub DelegateMouseWheel()
        AddHandler INDpcNavigation.MouseWheel, AddressOf LayoutControl1_MouseWheel
        For i = 0 To Me.Parent.Controls.Count - 1
            If Me.Parent.Controls(i).GetType.ToString = "DevExpress.XtraLayout.LayoutControl" Then
                AddHandler Me.Parent.Controls(i).MouseWheel, AddressOf LayoutControl1_MouseWheel
                For j = 0 To CType(Me.Parent.Controls(i), LayoutControl).Controls.Count - 1
                    AddHandler CType(Me.Parent.Controls(i), LayoutControl).Controls(j).MouseWheel, AddressOf LayoutControl1_MouseWheel
                    If CType(Me.Parent.Controls(i), LayoutControl).Controls(j).TabIndex = 0 Then
                        CType(Me.Parent.Controls(i), LayoutControl).Controls(j).Focus()
                    End If
                Next
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo donde creamos los botones del panel de navegacion con respecto a los grupos que hay en el layout control
    ''' </summary>
    Private Sub LoadNavigation()
        Me.Size = New Size(200, Me.Size.Height)
        If FlagLoadNavigationControl = True Then
            Exit Sub
        End If
        FlagLoadNavigationControl = True
        'Limpiamos el panel de navegacion
        INDpcNavigation.Controls.Clear()
        'Variable contador del numero de grupos
        Dim Contador As Integer = 0
        'Variable que contiene el tamaño del scroll
        Dim Tamaño As Integer = 0
        'Variable que contiene el tamaño maximo del scroll
        Dim Maximum As Integer
        'Variale que contiene el tamaño del scroll
        Dim Size As Size
        Dim FormSearchExist As Boolean
        'For para recorrer los controles del parent
        For i = 0 To Me.Parent.Controls.Count - 1
            If Me.Parent.Controls(i).Name = "FrmBusqueda" Then
                FormSearchExist = True
            End If
            If Me.Parent.Controls(i).GetType.ToString = "DevExpress.XtraLayout.LayoutControl" Then
                AddHandler Me.Parent.Controls(i).MouseWheel, AddressOf LayoutControl1_MouseWheel
                AddHandler Me.Parent.Controls(i).MouseEnter, AddressOf Controls_MouseEnter
                AddHandler Me.Parent.Controls(i).MouseHover, AddressOf Controls_MouseHover
                AddHandler Me.Parent.Controls(i).MouseLeave, AddressOf Controls_MouseLeave


                PosicionLayout = i
                'for para recorrer los controles del layout control inicializamos la variable j en 1 porque la posicion 0 es el layout control group principal.
                If CType(Me.Parent.Controls(i), LayoutControl).Items.Count > 1 Then
                    For j = 1 To CType(Me.Parent.Controls(i), LayoutControl).Items.Count - 1
                        'si encontramos un layout group entonces cremos el boton en el panel de navegacion
                        If CType(Me.Parent.Controls(i), LayoutControl).Items(j).GetType.ToString = "DevExpress.XtraLayout.LayoutControlGroup" Then
                            For h = 0 To CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup).Items.Count - 1
                                If CType(CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup).Items(h), LayoutControlItem).Control IsNot Nothing Then
                                    CType(CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup).Items(h), LayoutControlItem).Control.Tag = New Button With {.Tag = CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup)}
                                    AddHandler CType(CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup).Items(h), LayoutControlItem).Control.EnabledChanged, AddressOf ControlsEnabledChanged
                                    AddHandler CType(CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup).Items(h), LayoutControlItem).Control.GotFocus, AddressOf ControlsGotFocus
                                    If TypeOf CType(CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup).Items(h), LayoutControlItem).Control Is DevExpress.XtraGrid.GridControl Then
                                        AddHandler CType(CType(CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup).Items(h), LayoutControlItem).Control, DevExpress.XtraGrid.GridControl).GotFocus, AddressOf ControlsGotFocus
                                    End If
                                    AddHandler CType(CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup).Items(h), LayoutControlItem).Control.MouseEnter, AddressOf Controls_MouseEnter
                                    AddHandler CType(CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup).Items(h), LayoutControlItem).Control.MouseHover, AddressOf Controls_MouseHover
                                End If
                            Next
                            Dim Button As Button
                            Button = New Button
                            If CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup).Visibility = Utils.LayoutVisibility.Always Then
                                Button.Visible = True
                            Else
                                Button.Visible = False
                            End If
                            Button.TextAlign = ContentAlignment.TopLeft
                            Button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(107, Byte), Integer))
                            Button.FlatAppearance.BorderSize = 0
                            Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
                            Button.Dock = DockStyle.Top
                            Button.Cursor = Cursors.Hand
                            Button.Padding = New System.Windows.Forms.Padding(25, 0, 0, 0)
                            Button.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
                            Button.ForeColor = System.Drawing.Color.White
                            Button.Size = New System.Drawing.Size(173, 35)
                            Button.TabIndex = 0
                            Contador += 1
                            Button.Text = Contador & ". " & CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup).Text
                            If Button.Text.Length > 24 Then
                                Button.Size = New Size(179, 53)
                            End If
                            Button.Tag = CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup)
                            Button.Name = (CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup).Size.Width).ToString
                            AddHandler Button.Click, AddressOf ButtonClic
                            Button.UseVisualStyleBackColor = True
                            INDpcNavigation.Controls.Add(Button)
                            Button.BringToFront()
                            CType(CType(Me.Parent.Controls(i), LayoutControl).Items(j), LayoutControlGroup).Tag = Button

                            Me.FindHTLScrolling()

                            If Me.Parent.Controls(i).Controls.Count > j - 1 Then
                                'Si encontramos un scrolling entonces asignamos la posicion y los tamaños del scroll
                                If Me.Parent.Controls(i).Controls(j - 1).GetType.ToString = "DevExpress.XtraLayout.Scrolling.HTLScrollBar" Then
                                    PosicionLayout = i
                                    PosicionScroll = j - 1
                                    Maximum = CInt(HTLScroll.GetType.GetProperty("Maximum").GetValue(HTLScroll))
                                    Size = CType(HTLScroll.GetType.GetProperty("Size").GetValue(HTLScroll), Drawing.Size)
                                    Tamaño = Maximum - Size.Width
                                    'agregamos el manejador para el evento value changed del control scroll del layout
                                    HTLScroll.GetType.GetEvent("ValueChanged").AddEventHandler(HTLScroll, EventScrolling)
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Next

        AddHandler CType(Me.Parent.Controls(PosicionLayout), LayoutControl).EnabledChanged, AddressOf EnabledChangedLayoutControl
        'preguntamos si el panel de navegacion tiene controles 
        If INDpcNavigation.Controls.Count > 0 Then
            Dim NewSize = New Size(CType(Me.Parent.Controls(PosicionLayout), LayoutControl).Size.Width * 2, 1)
            Dim controlGroup As New LayoutControlGroup
            controlGroup.ShowInCustomizationForm = False
            controlGroup.AllowCustomizeChildren = False
            controlGroup.OptionsCustomization.AllowDrag = ItemDragDropMode.Disable
            controlGroup.OptionsCustomization.AllowDrop = ItemDragDropMode.Disable
            controlGroup.Size = New System.Drawing.Size(414, 566)
            controlGroup.Text = " "
            controlGroup.CustomizationFormText = "Render"
            Dim EmptySpace As New DevExpress.XtraLayout.EmptySpaceItem
            EmptySpace.AllowHotTrack = False
            EmptySpace.CustomizationFormText = "Espacio Vacio"
            EmptySpace.MaxSize = NewSize
            EmptySpace.MinSize = NewSize
            EmptySpace.Size = NewSize
            EmptySpace.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            EmptySpace.Text = "Espacio Vacio"
            EmptySpace.TextSize = New System.Drawing.Size(0, 0)
            controlGroup.Add(EmptySpace)
            Dim root = CType(Me.Parent.Controls(PosicionLayout), LayoutControl).Root
            root.AddGroup(controlGroup, root.Item(root.Items.Count - 1), Utils.InsertType.Right)
            'root.Move(root.Items.Item(root.Items.Count - 1), Utils.InsertType.Right)
            'root.Move(controlGroup, Utils.InsertType.Right)
            'CType(Me.Parent.Controls(PosicionLayout), LayoutControl).AddGroup(controlGroup, Utils.InsertType.Right)
            'CType(Me.Parent.Controls(PosicionLayout), LayoutControl).Root.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {controlGroup})
        End If
        'Scrolling(Nothing, Nothing)
        If FormSearchExist = True Then
            PosicionLayout -= 1
        End If
    End Sub

    ''' <summary>
    ''' Busca el control de scroll
    ''' </summary>
    Private Sub FindHTLScrolling()
        For j = 0 To Me.Parent.Controls(PosicionLayout).Controls.Count - 1
            If Me.Parent.Controls(PosicionLayout).Controls(j).GetType.ToString = "DevExpress.XtraLayout.Scrolling.HTLScrollBar" Then
                Me.HTLScroll = Me.Parent.Controls(PosicionLayout).Controls(j)
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que se dispara cuando se habilita o deshabilitan los LayoutControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub EnabledChangedLayoutControl(ByVal LayoutControl As LayoutControl, e As System.EventArgs)
        If LayoutControl.Enabled = False Then
            For Each Button As Button In INDpcNavigation.Controls
                Button.ForeColor = Color.White
                Button.BackgroundImage = Nothing
            Next
        Else
            SetScroll(CType(CType(INDpcNavigation.Controls(PositionGroup), Button).Tag, LayoutControlGroup).Location.X)
        End If
    End Sub

    ''' <summary>
    ''' Se controla que el control gano el foco por el cambio de estado
    ''' </summary>
    Private Sub ControlsEnabledChanged(sender As Control, e As EventArgs)
        'If sender.Enabled Then
        '    Me._flagControlsEnabledChanged = True
        'Else
        '    Me._flagControlsEnabledChanged = False
        'End If
    End Sub

    Private _flagControlsEnabledChanged As Boolean = False

    ''' <summary>
    ''' Metodo cuando se gana el foco por un control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ControlsGotFocus(sender As Control, e As EventArgs)
        'If Not Me._flagControlsEnabledChanged Then

        'End If
        Me._flagControlsEnabledChanged = False
        Me.FindHTLScrolling()
        If HTLScroll IsNot Nothing Then
            Dim lyg = CType(CType(CType(sender, Control).Tag, Control).Tag, LayoutControlGroup)
            Dim lyc = CType(lyg.Owner, LayoutControl)
            Dim pos = Me.IsIntoHView(lyc.ClientRectangle, lyg.ViewInfo.BoundsRelativeToControl)
            If pos = 1 Then 'Derecha
                Dim p2 As New Point(lyc.ClientRectangle.X + lyc.ClientRectangle.Width, lyc.ClientRectangle.Y)
                HTLScroll.GetType().GetProperty("Value").SetValue(HTLScroll, CInt(HTLScroll.GetType().GetProperty("Value").GetValue(HTLScroll)) + Me.CalcDiffHView(p2, New Point(lyg.ViewInfo.BoundsRelativeToControl.X + lyg.ViewInfo.BoundsRelativeToControl.Width, lyg.ViewInfo.BoundsRelativeToControl.Y)))
            End If
            If pos = -1 Then 'Izquierda
                HTLScroll.GetType().GetProperty("Value").SetValue(HTLScroll, CInt(HTLScroll.GetType().GetProperty("Value").GetValue(HTLScroll)) - Me.CalcDiffHView(lyc.ClientRectangle.Location, lyg.ViewInfo.BoundsRelativeToControl.Location))
            End If
            Me.SetFocusButtonNavControl(lyg)
        End If
    End Sub

    ''' <summary>
    ''' Metodo para cargar el controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LoadNavigationPanel(sender As Object, e As EventArgs)
        LoadNavigation()
        PositionGroup = INDpcNavigation.Controls.Count - 1
    End Sub

    ''' <summary>
    ''' Evento Button clic para mover el scroll a la posicion del grupo seleccionado
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Public Sub ButtonClic(sender As Object, e As EventArgs)
        For i = INDpcNavigation.Controls.Count - 1 To 0 Step -1
            If INDpcNavigation.Controls(i).Tag Is CType(sender, Button).Tag Then
                PositionGroup = i
                Exit For
            End If
        Next
        CType(INDpcNavigation.Controls(PositionGroup), Button).ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(107, Byte), Integer))
        If CType(INDpcNavigation.Controls(PositionGroup), Button).Size.Height = 35 Then
            CType(INDpcNavigation.Controls(PositionGroup), Button).BackgroundImage = My.Resources.PestañaNavigator1
        Else
            CType(INDpcNavigation.Controls(PositionGroup), Button).BackgroundImage = My.Resources.PestañaNavigator
        End If
        SetScroll(CType(CType(sender, Button).Tag, LayoutControlGroup).Location.X)
    End Sub

    ''' <summary>
    ''' Establece la posicion del scroll horizontal
    ''' </summary>
    ''' <param name="LocationX">The location X.</param>
    Public Sub SetScroll(ByVal LocationX As Integer)
        Me.FindHTLScrolling()
        If HTLScroll IsNot Nothing Then
            Dim ActualValue = HTLScroll.GetType.GetProperty("Value").GetValue(HTLScroll)
            HTLScroll.GetType.GetProperty("Value").SetValue(HTLScroll, LocationX, Nothing)
            'Scrolling(Nothing, Nothing)
        End If
    End Sub

    Public Function IsIntoHView(ByVal rec As Rectangle, ByVal p As Rectangle) As Int32
        If rec.X <= p.X And (rec.X + rec.Width) >= (p.X + p.Width) Then 'Esta contenido entre el rectangulo
            Return 0
        End If
        If rec.X > p.X Then 'Esta a la izquierda del rectangulo
            Return -1
        End If
        Return 1 'Esta a la derecha del rectangulo
    End Function

    Public Function CalcDiffHView(ByVal rec As Point, ByVal p As Point) As Int32
        Dim diff As Int32 = Math.Abs(rec.X - p.X)
        Return diff
    End Function

    ''' <summary>
    ''' Metodo para cambiar la apariencia del boton del panel de navegacion segun el grupo que esta visible en la parte izquierda del layout
    ''' </summary>
    Private Sub Scrolling(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ValueScroll As Integer = If(HTLScroll IsNot Nothing, CInt(HTLScroll.GetType.GetProperty("Value").GetValue(HTLScroll)), 0)
        For i = INDpcNavigation.Controls.Count - 1 To 0 Step -1
            If CInt(CType(CType(INDpcNavigation.Controls(i), Button).Tag, LayoutControlGroup).Location.X) <= ValueScroll And CInt(CType(INDpcNavigation.Controls(i), Button).Name) + CInt(CType(CType(INDpcNavigation.Controls(i), Button).Tag, LayoutControlGroup).Location.X) > ValueScroll Then
                CType(INDpcNavigation.Controls(i), Button).ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(107, Byte), Integer))
                If CType(INDpcNavigation.Controls(i), Button).Size.Height = 35 Then
                    CType(INDpcNavigation.Controls(i), Button).BackgroundImage = My.Resources.PestañaNavigator1
                Else
                    CType(INDpcNavigation.Controls(i), Button).BackgroundImage = My.Resources.PestañaNavigator
                End If
                PositionGroup = i
            Else
                CType(INDpcNavigation.Controls(i), Button).ForeColor = Color.White
                CType(INDpcNavigation.Controls(i), Button).BackgroundImage = Nothing
            End If
        Next
    End Sub

    Private Sub SetFocusButtonNavControl(ByVal lyg As LayoutControlGroup)
        For i = INDpcNavigation.Controls.Count - 1 To 0 Step -1
            If CType(CType(INDpcNavigation.Controls(i), Button).Tag, LayoutControlGroup) Is lyg Then
                CType(INDpcNavigation.Controls(i), Button).ForeColor = System.Drawing.Color.FromArgb(CType(CType(2, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(107, Byte), Integer))
                If CType(INDpcNavigation.Controls(i), Button).Size.Height = 35 Then
                    CType(INDpcNavigation.Controls(i), Button).BackgroundImage = My.Resources.PestañaNavigator1
                Else
                    CType(INDpcNavigation.Controls(i), Button).BackgroundImage = My.Resources.PestañaNavigator
                End If
                PositionGroup = i
            Else
                CType(INDpcNavigation.Controls(i), Button).ForeColor = Color.White
                CType(INDpcNavigation.Controls(i), Button).BackgroundImage = Nothing
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que se activa cuando se hacen visibles los grupos
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="Value"></param>
    ''' <remarks></remarks>
    Public Sub VisibleGroup(Sender As LayoutControlGroup, Value As Boolean)
        If Sender.Tag IsNot Nothing Then
            If Sender.Visibility = Utils.LayoutVisibility.Always Then
                CType(Sender.Tag, Button).Visible = True
            Else
                CType(Sender.Tag, Button).Visible = False
            End If
            Dim Contador As Integer = INDpcNavigation.Controls.Count
            For Each Button As Button In INDpcNavigation.Controls
                If Button.Visible = False Then
                    Contador -= 1
                End If
            Next
            For Each Button As Button In INDpcNavigation.Controls
                If Button.Visible = True Then
                    Button.Text = Contador & ". " & CType(Button.Tag, LayoutControlGroup).Text
                    Contador -= 1
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Metodo para ubicar la posicion del scroll en el lugar exacto del grupo 
    ''' se condiciona el evento cada 200 milisegundos para lograr el efecto deseado 
    ''' avanzar uno a uno por los grupos con el scroll del mouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LayoutControl1_MouseWheel(sender As Object, e As MouseEventArgs)
        If sender.GetType Is GetType(DevExpress.XtraGrid.GridControl) Then
            Exit Sub
        End If
        Dim runLength As Global.System.TimeSpan = Now.Subtract(FlagTimeMouseWhell)
        Dim millisecs As Double = runLength.TotalMilliseconds
        FlagTimeMouseWhell = DateTime.Now
        If millisecs > 200 Then
            If e.Delta = -120 Then
                PositionGroup -= 1
                If PositionGroup < 0 Then
                    PositionGroup = 0
                End If
                If CType(INDpcNavigation.Controls(PositionGroup), Button).Visible = False Then
                    PositionGroup -= 1
                    If PositionGroup < 0 Then
                        PositionGroup = 0
                    End If
                End If
            Else
                PositionGroup += 1
                If PositionGroup > INDpcNavigation.Controls.Count - 1 Then
                    PositionGroup = INDpcNavigation.Controls.Count - 1
                End If
                If CType(INDpcNavigation.Controls(PositionGroup), Button).Visible = False Then
                    PositionGroup += 1
                    If PositionGroup > INDpcNavigation.Controls.Count - 1 Then
                        PositionGroup = INDpcNavigation.Controls.Count - 1
                    End If
                End If
            End If
            SetScroll(CType(CType(INDpcNavigation.Controls(PositionGroup), Button).Tag, LayoutControlGroup).Location.X)
        End If
    End Sub

    ''' <summary>
    ''' Aqui se controla la bandera que indica si el
    ''' mouse se encuentra encima de los controles
    ''' </summary>
    Private Sub Controls_MouseHover(sender As Object, e As EventArgs)
        Me.isMouseOver = True
    End Sub
    Private Sub Controls_MouseEnter(sender As Object, e As EventArgs)
        Me.isMouseOver = True
    End Sub
    Private Sub Controls_MouseLeave(sender As Object, e As EventArgs)
        Me.isMouseOver = False
    End Sub

#End Region

End Class