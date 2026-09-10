#Region "Imports"

Imports System.Drawing
Imports System.Windows.Forms
Imports System.ComponentModel
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.Data
Imports DevExpress.XtraTab
Imports System.Linq
Imports Infrastructure.CrossCutting.Resources

#End Region

<ProvideProperty("TotalPageHeadersVisible", GetType(XtraTabControl))> _
<ProvideProperty("IsAssistant", GetType(XtraTabControl))> _
<ProvideProperty("TabHeadersBaseColor", GetType(XtraTabControl))> _
<ProvideProperty("DegradeFactor", GetType(XtraTabControl))> _
<ProvideProperty("ShowTabHeaderText", GetType(XtraTabControl))> _
Public Class XtraTabControlAssistant
    Inherits Component
    Implements IExtenderProvider
    Implements ISupportInitialize

#Region "PanelControlWithText"

    ''' <summary>
    ''' Representa un PanelControl con la ventaja de dibujar su
    ''' texto en su propio background
    ''' </summary>
    Private Class PanelControlWithText
        Inherits PanelControl

#Region "Builders"

        ''' <summary>
        ''' Inicializa una nueva instancia de la clase
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Inicializa una nueva instancia de la clase
        ''' </summary>
        ''' <param name="text">Texto en el panel</param>
        Public Sub New(ByVal text As String)
            MyBase.New()
            Me.Text = text
        End Sub

#End Region

#Region "Methods"

        ''' <summary>
        ''' Se activa en el evento que pinta el panel, y aqui
        ''' se dibuja el texto
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)

            Dim rec As Rectangle = Me.ClientRectangle
            rec.Y = -35
            rec.X = 4

            Using cache As New DevExpress.Utils.Drawing.GraphicsCache(e.Graphics)
                cache.DrawVString(Me.Text, New Font("Segoe UI Light", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), Brushes.White, rec, New StringFormat(), -90)
            End Using
        End Sub

#End Region

    End Class

#End Region

#Region "Fields"

    ''' <summary>
    ''' Encapsula los controles que extienden sus propiedades
    ''' </summary>
    Private _hashtable As Hashtable

#End Region

#Region "Properties"

    ''' <summary>
    ''' Encapsula las propiedades que extiende cada control
    ''' </summary>
    Private Class Properties

        ''' <summary>
        ''' Inicializa una nueva instancia de la clase
        ''' </summary>
        Public Sub New()
            Me.TotalPageHeadersVisible = 3
            Me.IsAssistant = False
            Me.ShowTabHeaderText = False
            Me.TabHeadersBaseColor = Color.LightBlue
            Me.DegradeFactor = 17
            Me.Panels = New List(Of PanelControl)()
        End Sub

        ''' <summary>
        ''' Obtiene o asigna el numero total de pestañas a mantener visibles
        ''' </summary>
        ''' <value>Numero total de pestañas a mantener visibles</value>
        ''' <returns>El numero total de pestañas a mantener visibles</returns>
        Public Property TotalPageHeadersVisible As Int32

        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se muestra 
        ''' el texto de las cabeceras
        ''' </summary>
        ''' <value>Valor que indica si se muestra el texto en las cabeceras</value>
        ''' <returns>Un valor que indica si se puestra el texto en las cabeceras</returns>
        Public Property ShowTabHeaderText As Boolean

        ''' <summary>
        ''' Obtiene o asigna un valor que indica si el control
        ''' tiene caracteristicas de asistente
        ''' </summary>
        ''' <value>Un valor que indica si el control es asistente</value>
        ''' <returns>Valor que indica si el control es asistente</returns>
        Public Property IsAssistant As Boolean

        ''' <summary>
        ''' Obtiene o asigna el color base usado para el degradado de cada
        ''' uno de las cabeceras de cada pagina del control
        ''' </summary>
        ''' <value>Color base usado para las cabeceras</value>
        ''' <returns>El color base usado para las cabeceras</returns>
        Public Property TabHeadersBaseColor As Color

        ''' <summary>
        ''' Obtiene o asigna un valor entero que indica el
        ''' intervalo de degradado usado entre cada cabecera
        ''' </summary>
        ''' <value>Valor entero</value>
        ''' <returns>El valor del intervalo de degradado entre cabeceras</returns>
        Public Property DegradeFactor As Int32

        ''' <summary>
        ''' Obtiene o asigna la lista de paneles usados como cabeceras
        ''' de cada pagina
        ''' </summary>
        ''' <value>Lista de paneles</value>
        ''' <returns>La lista de paneles</returns>
        Public Property Panels As List(Of PanelControl)

    End Class

    ''' <summary>
    ''' Obtiene las propiedades extendidas del control
    ''' y se asegura de registrarlo si no las tiene
    ''' </summary>
    ''' <param name="obj">Control extendido</param>
    ''' <returns>Propiedades del control</returns>
    Private Function EnsurePropertiesExists(ByVal obj As Object) As Properties
        Dim p As Properties = DirectCast(Me._hashtable(obj), Properties)
        If p Is Nothing Then
            p = New Properties()
            Me._hashtable.Add(obj, p)
        End If
        Return p
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si el control actua como asistente
    ''' </summary>
    ''' <param name="obj">Control quien extiende sus propiedades</param>
    ''' <returns>Valor que indica si el control actua como asistente</returns>
    <Category("Indigo"), Description("Obtiene o asigna un valor que indica si el control actua como asistente"), DefaultValue(False)>
    Public Function GetIsAssistant(ByVal obj As XtraTabControl) As Boolean
        Return Me.EnsurePropertiesExists(obj).IsAssistant
    End Function

    ''' <summary>
    ''' Asigna un valor que indica si el control actua como asistente
    ''' </summary>
    ''' <param name="obj">Control que extiende sus propiedades</param>
    ''' <param name="value">Valor que indica si el control actua como asistente</param>
    Public Sub SetIsAssistant(ByVal obj As XtraTabControl, ByVal value As Boolean)
        Me.EnsurePropertiesExists(obj).IsAssistant = value
    End Sub

    ''' <summary>
    ''' Obtiene el color base usado en las cabeceras de cada pagina
    ''' </summary>
    ''' <param name="obj">Control que extiende sus propiedades</param>
    ''' <returns>Color base de las cabeceras de pagina</returns>
    <Category("Indigo"), Description("Obtiene o asigna el color base usado en las cabeceras de cada pagina"), DefaultValue(GetType(Color), "LightBlue")>
    Public Function GetTabHeadersBaseColor(ByVal obj As XtraTabControl) As Color
        Return Me.EnsurePropertiesExists(obj).TabHeadersBaseColor
    End Function

    ''' <summary>
    ''' Asigna el color base usado en las cabeceras de cada pagina
    ''' </summary>
    ''' <param name="obj">Control que extiende sus pripiedades</param>
    ''' <param name="value">Color base a usar</param>
    Public Sub SetTabHeadersBaseColor(ByVal obj As XtraTabControl, ByVal value As Color)
        Me.EnsurePropertiesExists(obj).TabHeadersBaseColor = value
    End Sub

    ''' <summary>
    ''' Obtiene el factor de degradado usado entre cada cabecera
    ''' </summary>
    ''' <param name="obj">Control que extiende sus propiedades</param>
    ''' <returns>Factor de degradado entre cabecera</returns>
    <Category("Indigo"), Description("Obtiene o asigna el factor de degradado usado entre cada cabecera"), DefaultValue(17)>
    Public Function GetDegradeFactor(ByVal obj As XtraTabControl) As Int32
        Return Me.EnsurePropertiesExists(obj).DegradeFactor
    End Function

    ''' <summary>
    ''' Asigna el factor de degradado usado entre cada cabecera
    ''' </summary>
    ''' <param name="obj">Control que extiende sus pripiedades</param>
    ''' <param name="value">Factor de degradado usado entre cada cabecera</param>
    Public Sub SetDegradeFactor(ByVal obj As XtraTabControl, ByVal value As Int32)
        Me.EnsurePropertiesExists(obj).DegradeFactor = value
    End Sub

    ''' <summary>
    ''' Obtiene el numero total de pestañas a mantener visibles
    ''' </summary>
    ''' <param name="obj">Control que extiende sus propiedades</param>
    ''' <returns>El numero total de pestañas a mantener visibles</returns>
    <Category("Indigo"), Description("Obtiene o asigna el numero total de pestañas a mantener visibles"), DefaultValue(3)>
    Public Function GetTotalPageHeadersVisible(ByVal obj As XtraTabControl) As Int32
        Return Me.EnsurePropertiesExists(obj).TotalPageHeadersVisible
    End Function

    ''' <summary>
    ''' Asigna el numero total de pestañas a mantener visibles
    ''' </summary>
    ''' <param name="obj">Control que extiende sus pripiedades</param>
    ''' <param name="value">Numero total de pestañas a mantener visibles</param>
    Public Sub SetTotalPageHeadersVisible(ByVal obj As XtraTabControl, ByVal value As Int32)
        Me.EnsurePropertiesExists(obj).TotalPageHeadersVisible = value
    End Sub

    ''' <summary>
    ''' Obtiene un valor que indica si se muestra el texto de las cabeceras
    ''' </summary>
    ''' <param name="obj">Control quien extiende sus propiedades</param>
    ''' <returns>Un valor que indica si se puestra el texto en las cabeceras</returns>
    <Category("Indigo"), Description("Obtiene o asigna un valor que indica si se muestra el texto de las cabeceras"), DefaultValue(False)>
    Public Function GetShowTabHeaderText(ByVal obj As XtraTabControl) As Boolean
        Return Me.EnsurePropertiesExists(obj).ShowTabHeaderText
    End Function

    ''' <summary>
    ''' Asigna un valor que indica si se muestra el texto de las cabeceras
    ''' </summary>
    ''' <param name="obj">Control que extiende sus propiedades</param>
    ''' <param name="value">Valor que indica si se muestra el texto en las cabeceras</param>
    Public Sub SetShowTabHeaderText(ByVal obj As XtraTabControl, ByVal value As Boolean)
        Me.EnsurePropertiesExists(obj).ShowTabHeaderText = value
    End Sub

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me._hashtable = New Hashtable()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Genera un nuevo color a partir de uno base
    ''' y teniendo en cuenta un factor de degradado
    ''' </summary>
    ''' <param name="baseColor">Color base</param>
    ''' <param name="degradeLevel">Nivel de degrado</param>
    ''' <param name="degradeFactor">Factor de degradado</param>
    ''' <returns>Color generado</returns>
    Private Function GenerateColor(ByVal baseColor As Color, ByVal degradeLevel As Int32, ByVal degradeFactor As Int32) As Color
        Dim R, G, B As Int32
        R = baseColor.R - (degradeLevel * degradeFactor)
        G = baseColor.G - (degradeLevel * degradeFactor)
        B = baseColor.B - (degradeLevel * degradeFactor)

        If (R < 0) OrElse (G < 0) OrElse (B < 0) Then
            Return baseColor
        End If

        Return Color.FromArgb(R, G, B)
    End Function

    ''' <summary>
    ''' Se encarga de lanzar el evento AssistantQueryPageChange, el cual
    ''' ocurre cuando se inicia el cambio de pagina en el control asistente
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="args">Argumentos del evento</param>
    Protected Overridable Sub OnAssistantQueryPageChange(ByVal sender As Object, ByVal args As AssistantQueryPageChangeEventArgs)
        If args IsNot Nothing AndAlso Not args.Cancel Then
            RaiseEvent AssistantQueryPageChange(sender, args)
        End If
    End Sub

    ''' <summary>
    ''' Cambia la pagina en el asistente
    ''' </summary>
    ''' <param name="toPage">Pagina destino</param>
    Private Sub ChangePage(ByVal toPage As Int32, ByVal pnl As PanelControl)
        If (From c As Control In pnl.Parent.Controls Where (Not c.GetType().Equals(GetType(XtraTabControl)) And Not c.GetType().Equals(GetType(PanelControlWithText))) Select c).ToList().Count > 0 Then
            MessageBox.Show(ResourceManager.GetString("MsgBox_ErrorChangePage", Me.GetType()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim tabControl As XtraTabControl = DirectCast(pnl.Tag, XtraTabControl)
        Dim totalPagesVisible As Int32 = Me.EnsurePropertiesExists(tabControl).TotalPageHeadersVisible
        tabControl.SelectedTabPageIndex = Convert.ToInt32(toPage)
        pnl.Visible = False
        If pnl.Dock = DockStyle.Right Then
            'Obtenemos el indice del panel
            Dim indexOf As Int32 = Me.EnsurePropertiesExists(tabControl).Panels.IndexOf(pnl)
            'Movemos los paneles
            For i As Int32 = 0 To (indexOf - 1)
                Me.EnsurePropertiesExists(tabControl).Panels(i).Dock = DockStyle.Left
                Me.EnsurePropertiesExists(tabControl).Panels(i).BringToFront()
                DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Text = (Convert.ToInt32(DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Tag) + 1)
                If i < (indexOf - totalPagesVisible) Then
                    Me.EnsurePropertiesExists(tabControl).Panels(i).Visible = False
                Else
                    If i = (indexOf - totalPagesVisible) AndAlso (indexOf - totalPagesVisible) > 0 Then
                        DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Text = "-" & (Convert.ToInt32(DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Tag) + 1)
                    End If
                    Me.EnsurePropertiesExists(tabControl).Panels(i).Visible = True
                End If
            Next
            'Aplicamos el efecto al otro lado
            For i As Int32 = (indexOf + 1) To Me.EnsurePropertiesExists(tabControl).Panels.Count - 1
                DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Text = (Convert.ToInt32(DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Tag) + 1)
                If i > (indexOf + totalPagesVisible) Then
                    Me.EnsurePropertiesExists(tabControl).Panels(i).Visible = False
                Else
                    If i = (indexOf + totalPagesVisible) AndAlso (indexOf + totalPagesVisible) < Me.EnsurePropertiesExists(tabControl).Panels.Count - 1 Then
                        DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Text = (Convert.ToInt32(DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Tag) + 1) & "+"
                    End If
                    Me.EnsurePropertiesExists(tabControl).Panels(i).Visible = True
                End If
            Next
        Else
            'Obtenemos el indice del panel
            Dim indexOf As Int32 = Me.EnsurePropertiesExists(tabControl).Panels.IndexOf(pnl)
            For i As Int32 = (indexOf + 1) To Me.EnsurePropertiesExists(tabControl).Panels.Count - 1
                Me.EnsurePropertiesExists(tabControl).Panels(i).Dock = DockStyle.Right
                Me.EnsurePropertiesExists(tabControl).Panels(i).SendToBack()
                DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Text = (Convert.ToInt32(DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Tag) + 1)
                If i > (indexOf + totalPagesVisible) Then
                    Me.EnsurePropertiesExists(tabControl).Panels(i).Visible = False
                Else
                    If i = (indexOf + totalPagesVisible) AndAlso (indexOf + totalPagesVisible) < Me.EnsurePropertiesExists(tabControl).Panels.Count - 1 Then
                        DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Text = (Convert.ToInt32(DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Tag) + 1) & "+"
                    End If
                    Me.EnsurePropertiesExists(tabControl).Panels(i).Visible = True
                End If
            Next
            'Aplicamos el efecto al otro lado
            For i As Int32 = 0 To (indexOf - 1)
                DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Text = (Convert.ToInt32(DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Tag) + 1)
                If i < (indexOf - totalPagesVisible) Then
                    Me.EnsurePropertiesExists(tabControl).Panels(i).Visible = False
                Else
                    If i = (indexOf - totalPagesVisible) AndAlso (indexOf - totalPagesVisible) > 0 Then
                        DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Text = "-" & (Convert.ToInt32(DirectCast(Me.EnsurePropertiesExists(tabControl).Panels(i).Controls(0), LabelControl).Tag) + 1)
                    End If
                    Me.EnsurePropertiesExists(tabControl).Panels(i).Visible = True
                End If
            Next
        End If
        tabControl.BringToFront()
        tabControl.Dock = DockStyle.Fill
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Ocurre cuando se inicia el cambio de pagina en el control asistente
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event AssistantQueryPageChange(ByVal sender As Object, ByVal e As AssistantQueryPageChangeEventArgs)

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Ocurre cuando se da click en uno de los botones
    ''' de navegacion
    ''' </summary>
    ''' <param name="sender">Objeto quien invoca provoca el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Private Sub NavButton_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs)
        Dim btn = DirectCast(e.Button, DevExpress.XtraBars.Docking2010.WindowsUIButton)
        Dim btnPnl = DirectCast(sender, DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel)
        Dim pnl = DirectCast(btnPnl.Parent, PanelControl)
        Dim tabControl = DirectCast(pnl.Tag, XtraTabControl)
        Dim args As New AssistantQueryPageChangeEventArgs(tabControl.SelectedTabPageIndex, Convert.ToInt32(btnPnl.Tag) + (Convert.ToInt32(btn.Tag)), False)
        Me.OnAssistantQueryPageChange(tabControl, args)
        If Not args.Cancel Then
            Me.ChangePage(args.ToPage, Me.EnsurePropertiesExists(tabControl).Panels(args.ToPage))
        End If
    End Sub

    ''' <summary>
    ''' Ocurre cuando se da click en una de las cabeceras
    ''' del control
    ''' </summary>
    ''' <param name="sender">Objeto quien invoca provoca el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Private Sub XtraTabPageHeader_Click(sender As Object, e As EventArgs)
        Dim lbl As LabelControl = DirectCast(sender, LabelControl)
        Dim pnl As PanelControl = DirectCast(lbl.Parent, PanelControl)

        Dim tabControl As XtraTabControl = DirectCast(pnl.Tag, XtraTabControl)
        Dim args As New AssistantQueryPageChangeEventArgs(tabControl.SelectedTabPageIndex, Convert.ToInt32(lbl.Tag), False)
        Me.OnAssistantQueryPageChange(tabControl, args)
        If Not args.Cancel Then
            Me.ChangePage(args.ToPage, Me.EnsurePropertiesExists(tabControl).Panels(args.ToPage))
        End If
    End Sub

#End Region

#Region "IExtenderProvider"

    ''' <summary>
    ''' Obtiene un valor que indica si el control 
    ''' puede extender sus propiedades
    ''' </summary>
    ''' <param name="extendee">Control a evaluar</param>
    ''' <returns>Valor que indica si el control puede extender</returns>
    Public Function CanExtend(extendee As Object) As Boolean Implements IExtenderProvider.CanExtend
        If extendee.GetType().Equals(GetType(XtraTabControl)) Then
            Return True
        End If
        Return False
    End Function

#End Region

#Region "ISupportInitialize"

    ''' <summary>
    ''' No se implementa
    ''' </summary>
    Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        'No se implementa
    End Sub

    ''' <summary>
    ''' Ejecuta el procedimiento indicado para finalizar la
    ''' inicializacion del control extendido
    ''' </summary>
    Public Sub EndInit() Implements ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Me._hashtable
            Dim tabControl As XtraTabControl = DirectCast(de.Key, XtraTabControl)
            If DirectCast(de.Value, Properties).IsAssistant AndAlso tabControl.Parent IsNot Nothing Then
                'Se obliga a iniciar con la primera pagina
                If tabControl.TabPages.Count > 0 Then
                    tabControl.SelectedTabPageIndex = 0
                End If
                'Se configura el XtraTabControl
                tabControl.Dock = DockStyle.Fill
                tabControl.ShowTabHeader = DefaultBoolean.False
                tabControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                tabControl.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                tabControl.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat
                tabControl.LookAndFeel.UseDefaultLookAndFeel = False
                'Se obtiene el valor de algunas propiedades
                Dim baseColor As Color = DirectCast(de.Value, Properties).TabHeadersBaseColor
                Dim degradeFactor As Int32 = DirectCast(de.Value, Properties).DegradeFactor
                For i As Int32 = 0 To tabControl.TabPages.Count - 1

                    'Panel de la cabecera
                    Dim pnlNavTop = New DevExpress.XtraEditors.PanelControl()
                    pnlNavTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                    pnlNavTop.BackColor = Color.Transparent
                    pnlNavTop.Dock = System.Windows.Forms.DockStyle.Top
                    pnlNavTop.Location = New System.Drawing.Point(0, 341)
                    pnlNavTop.Name = ("pnlNavTop" & i)
                    pnlNavTop.Size = New System.Drawing.Size(530, 55)
                    pnlNavTop.Tag = tabControl

                    If i > 0 Then
                        'Boton atras
                        Dim pnlNavButtonsTop = New DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel()
                        pnlNavButtonsTop.AppearanceButton.Hovered.BackColor = System.Drawing.Color.Silver
                        pnlNavButtonsTop.AppearanceButton.Hovered.ForeColor = System.Drawing.Color.LightGray
                        pnlNavButtonsTop.AppearanceButton.Hovered.Options.UseBackColor = True
                        pnlNavButtonsTop.AppearanceButton.Hovered.Options.UseForeColor = True
                        pnlNavButtonsTop.AppearanceButton.Normal.BackColor = System.Drawing.Color.Transparent
                        pnlNavButtonsTop.AppearanceButton.Normal.ForeColor = System.Drawing.Color.Silver
                        pnlNavButtonsTop.AppearanceButton.Normal.Options.UseBackColor = True
                        pnlNavButtonsTop.AppearanceButton.Normal.Options.UseForeColor = True
                        pnlNavButtonsTop.AppearanceButton.Pressed.BackColor = System.Drawing.Color.Silver
                        pnlNavButtonsTop.AppearanceButton.Pressed.ForeColor = System.Drawing.Color.LightGray
                        pnlNavButtonsTop.AppearanceButton.Pressed.Options.UseBackColor = True
                        pnlNavButtonsTop.AppearanceButton.Pressed.Options.UseForeColor = True
                        pnlNavButtonsTop.ButtonInterval = 20
                        pnlNavButtonsTop.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter
                        pnlNavButtonsTop.Cursor = System.Windows.Forms.Cursors.Hand
                        pnlNavButtonsTop.Dock = System.Windows.Forms.DockStyle.Left
                        pnlNavButtonsTop.Location = New System.Drawing.Point(399, 0)
                        pnlNavButtonsTop.Name = ("pnlNavButtonsTop" & i)
                        pnlNavButtonsTop.Buttons.AddRange(New DevExpress.XtraEditors.ButtonPanel.IBaseButton() {New DevExpress.XtraBars.Docking2010.WindowsUIButton("", Global.Presentation.Controls.My.Resources.Resources.Back, -1, DevExpress.XtraBars.Docking2010.ImageLocation.[Default], DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, ResourceManager.GetString("BtnBack_Text", Me.GetType()), True, -1, True, Nothing, True, False, True, Nothing, -1, -1, False, False)})
                        pnlNavButtonsTop.Size = New System.Drawing.Size(66, 55)
                        pnlNavButtonsTop.Tag = i
                        AddHandler pnlNavButtonsTop.ButtonClick, AddressOf NavButton_ButtonClick

                        pnlNavTop.Controls.Add(pnlNavButtonsTop)
                    End If

                    'Label del texto de la cabecera
                    Dim lblHeaderCaption As New LabelControl()
                    lblHeaderCaption.Appearance.BackColor = System.Drawing.Color.Transparent
                    lblHeaderCaption.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    lblHeaderCaption.Appearance.ForeColor = System.Drawing.Color.Black
                    lblHeaderCaption.Appearance.TextOptions.HAlignment = HorzAlignment.Near
                    lblHeaderCaption.Appearance.TextOptions.VAlignment = VertAlignment.Center
                    lblHeaderCaption.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter
                    lblHeaderCaption.Appearance.TextOptions.WordWrap = WordWrap.Wrap
                    lblHeaderCaption.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                    lblHeaderCaption.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                    lblHeaderCaption.Dock = System.Windows.Forms.DockStyle.Fill
                    lblHeaderCaption.Location = New System.Drawing.Point(0, 0)
                    lblHeaderCaption.Name = ("lblHeaderCaption" & i)
                    lblHeaderCaption.Size = New System.Drawing.Size(536, 55)
                    lblHeaderCaption.Text = (i + 1) & ". " & tabControl.TabPages(i).Text
                    If i = 0 Then
                        lblHeaderCaption.Padding = New Padding(15, 0, 0, 0)
                    End If

                    pnlNavTop.Controls.Add(lblHeaderCaption)
                    lblHeaderCaption.BringToFront()

                    'Se agrega la cabecera a la pagina
                    tabControl.TabPages(i).Controls.Add(pnlNavTop)

                    'Panel inferior de navegacion
                    Dim pnlNavButtom = New DevExpress.XtraEditors.PanelControl()
                    pnlNavButtom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                    pnlNavButtom.BackColor = Color.Transparent

                    pnlNavButtom.Dock = System.Windows.Forms.DockStyle.Bottom
                    pnlNavButtom.Location = New System.Drawing.Point(0, 341)
                    pnlNavButtom.Name = ("pnlNavButtom" & i)
                    pnlNavButtom.Size = New System.Drawing.Size(530, 55)
                    pnlNavButtom.Tag = tabControl

                    If i < (tabControl.TabPages.Count - 1) Then
                        'Boton de siguiente
                        Dim pnlNavButtonsButtom = New DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel()
                        pnlNavButtonsButtom.AppearanceButton.Hovered.BackColor = System.Drawing.Color.Silver
                        pnlNavButtonsButtom.AppearanceButton.Hovered.ForeColor = System.Drawing.Color.LightGray
                        pnlNavButtonsButtom.AppearanceButton.Hovered.Options.UseBackColor = True
                        pnlNavButtonsButtom.AppearanceButton.Hovered.Options.UseForeColor = True
                        pnlNavButtonsButtom.AppearanceButton.Normal.BackColor = System.Drawing.Color.Transparent
                        pnlNavButtonsButtom.AppearanceButton.Normal.ForeColor = System.Drawing.Color.Silver
                        pnlNavButtonsButtom.AppearanceButton.Normal.Options.UseBackColor = True
                        pnlNavButtonsButtom.AppearanceButton.Normal.Options.UseForeColor = True
                        pnlNavButtonsButtom.AppearanceButton.Pressed.BackColor = System.Drawing.Color.Silver
                        pnlNavButtonsButtom.AppearanceButton.Pressed.ForeColor = System.Drawing.Color.LightGray
                        pnlNavButtonsButtom.AppearanceButton.Pressed.Options.UseBackColor = True
                        pnlNavButtonsButtom.AppearanceButton.Pressed.Options.UseForeColor = True
                        pnlNavButtonsButtom.ButtonInterval = 20
                        pnlNavButtonsButtom.ContentAlignment = System.Drawing.ContentAlignment.TopCenter
                        pnlNavButtonsButtom.Cursor = System.Windows.Forms.Cursors.Hand
                        pnlNavButtonsButtom.Dock = System.Windows.Forms.DockStyle.Right
                        pnlNavButtonsButtom.Location = New System.Drawing.Point(399, 0)
                        pnlNavButtonsButtom.Name = ("pnlNavButtonsButtom" & i)
                        pnlNavButtonsButtom.Size = New System.Drawing.Size(66, 55)
                        pnlNavButtonsButtom.Tag = i
                        pnlNavButtonsButtom.Buttons.AddRange(New DevExpress.XtraEditors.ButtonPanel.IBaseButton() {New DevExpress.XtraBars.Docking2010.WindowsUIButton("", Global.Presentation.Controls.My.Resources.Resources.Forwar, -1, DevExpress.XtraBars.Docking2010.ImageLocation.[Default], DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, ResourceManager.GetString("BtnForward_Text", Me.GetType()), True, -1, True, Nothing, True, False, True, Nothing, 1, -1, False, False)})
                        AddHandler pnlNavButtonsButtom.ButtonClick, AddressOf NavButton_ButtonClick

                        pnlNavButtom.Controls.Add(pnlNavButtonsButtom)

                    End If

                    'Se agrega el panel inferior de navegacion a la pagina
                    tabControl.TabPages(i).Controls.Add(pnlNavButtom)

                    'Label que contiene el numero de pagina
                    Dim lbl As New LabelControl()
                    lbl.Appearance.BackColor = System.Drawing.Color.Transparent
                    lbl.Appearance.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    lbl.Appearance.ForeColor = System.Drawing.Color.White
                    lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                    lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                    lbl.Cursor = Cursors.Hand
                    lbl.Dock = System.Windows.Forms.DockStyle.Bottom
                    lbl.Location = New System.Drawing.Point(0, 248)
                    lbl.Name = ("lbl" & i)
                    lbl.Size = New System.Drawing.Size(284, 30)
                    lbl.ToolTip = String.Format(ResourceManager.GetString("LblNumPageHeader_Tooltip", Me.GetType()), (i + 1), tabControl.TabPages.Count)
                    If i = Me.EnsurePropertiesExists(tabControl).TotalPageHeadersVisible Then
                        lbl.Text = (i + 1) & "+"
                    Else
                        lbl.Text = (i + 1)
                    End If
                    lbl.Tag = i
                    AddHandler lbl.Click, AddressOf XtraTabPageHeader_Click
                    'Panel que representa la pestaña de la pagina
                    Dim pnl As New PanelControlWithText()
                    pnl.Appearance.BackColor = Me.GenerateColor(baseColor, i, degradeFactor)
                    pnl.Appearance.Options.UseBackColor = True
                    pnl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                    pnl.Dock = System.Windows.Forms.DockStyle.Right
                    pnl.Location = New System.Drawing.Point(254, 0)
                    pnl.Name = ("pnl" & i)
                    pnl.Size = New System.Drawing.Size(30, 248)
                    pnl.Tag = tabControl
                    If Me.EnsurePropertiesExists(tabControl).ShowTabHeaderText Then
                        pnl.Text = tabControl.TabPages(i).Text
                    End If
                    'Se agrega el label de pagina al panel de pestaña
                    pnl.Controls.Add(lbl)
                    'Se agrega el panel de pestaña al control
                    tabControl.Parent.Controls.Add(pnl)

                    'Se agrega el panel de pestaña a la lista interna de paneles creados
                    DirectCast(de.Value, Properties).Panels.Add(pnl)

                    'Si es el primer panel, se pone invisible
                    If i = 0 OrElse i > Me.EnsurePropertiesExists(tabControl).TotalPageHeadersVisible Then
                        pnl.Visible = False
                    End If

                Next
            End If
        Next
    End Sub

#End Region

End Class

''' <summary>
''' Encapsula los argumentos del evento AssistantQueryPageChange
''' </summary>
Public Class AssistantQueryPageChangeEventArgs
    Inherits EventArgs

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el indice de la pagina actual
    ''' </summary>
    ''' <value>Indice de la pagina actual</value>
    ''' <returns>El indice de la pagina actual</returns>
    Public Property CurrentPage As Int32

    ''' <summary>
    ''' Obtiene o asigna el indice de la pagina destino
    ''' </summary>
    ''' <value>Indice de la pagina destino</value>
    ''' <returns>El indice de la pagina destino</returns>
    Public Property ToPage As Int32

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si se cancela el cambio de pagina
    ''' </summary>
    ''' <value>Valor que indica si se cancela el cambio de pagina</value>
    ''' <returns>Un valor que indica si se cancela el cambio de pagina</returns>
    Public Property Cancel As Boolean

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="currentPage">Pagina actual</param>
    ''' <param name="toPage">Pagina destino</param>
    ''' <param name="cancel">Valor que indica si se cancela el cambio</param>
    Public Sub New(ByVal currentPage As Int32, ByVal toPage As Int32, ByVal cancel As Boolean)
        Me.CurrentPage = currentPage
        Me.ToPage = toPage
        Me.Cancel = cancel
    End Sub

#End Region

End Class