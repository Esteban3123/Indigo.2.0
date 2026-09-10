'***********************************************************************
' Assembly         : Presentation.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 25-04-2013
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias"
Imports System.ComponentModel
Imports System.Collections
Imports System.Diagnostics
Imports DevExpress.XtraEditors
Imports System.Drawing
Imports System.Windows.Forms.Integration
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Clase con toda la funcionalidad del control extendido PopupContainerEdit
''' </summary>
<ProvideProperty("ButtonMoreOptions", GetType(PopupContainerEdit))>
<ProvideProperty("PopUpAnimation", GetType(PopupContainerEdit))>
<ProvideProperty("PanelControl", GetType(PopupContainerEdit))>
<ProvideProperty("WpfControl", GetType(PopupContainerEdit))>
<ProvideProperty("HostControl", GetType(PopupContainerEdit))>
<ProvideProperty("OpenForm", GetType(PopupContainerEdit))> _
<ProvideProperty("TagForm", GetType(PopupContainerEdit))> _
Public Class IndigoPopUpContainerEdit
    Inherits System.ComponentModel.Component
    Implements IExtenderProvider
    Implements ISupportInitialize

#Region "Variables Globales"
    ''' <summary>
    ''' Variable hashtable que contiene los controles de tipo PopupContainerEdit
    ''' </summary>
    Private Hashtable As Hashtable
    ''' <summary>
    ''' The contenedor
    ''' </summary>
    Private Contenedor As System.ComponentModel.Container = Nothing
    ''' <summary>
    ''' Variable para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Clase que contiene las propiedades que se agregan al control
    ''' </summary>
    Private Class Propiedades
        ''' <summary>
        ''' propiedad que contiene si el campo es obligatorio
        ''' </summary>
        Public ButtonMoreOptions As Boolean
        ''' <summary>
        ''' Propiedad que establece si el popup tiene animacion cuando se despliega
        ''' </summary>
        ''' <remarks></remarks>
        Public PopUpAnimation As Boolean

        Public PanleControl As DevExpress.XtraEditors.PanelControl
        Public WpfControl As WpfAnimationPopUp
        Public HostControl As ElementHost

        Public OpenForm As Boolean
        Public TagForm As String
    End Class

    ''' <summary>
    ''' Funcion para agregar las propiedades a los controles
    ''' </summary>
    ''' <param name="key">The key.</param>
    ''' <returns></returns>
    Private Function EnsurePropertiesExists(ByVal key As Object) As Propiedades
        Dim p As Propiedades = DirectCast(Hashtable(key), Propiedades)
        If p Is Nothing Then
            p = New Propiedades()
            Hashtable(key) = p
        End If
        Return p
    End Function

#Region "ButtonMoreOptions"
    ''' <summary>
    ''' Funcion para obtener si el control es el de mas opciones
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad para establecer el control como mas opciones")> _
    <Category("Appearance")> _
    Public Function GetButtonMoreOptions(ByVal p As PopupContainerEdit) As Boolean
        Return EnsurePropertiesExists(p).ButtonMoreOptions
    End Function

    ''' <summary>
    ''' Metodo para establecer si el control como mas opciones
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Public Sub SetButtonMoreOptions(ByVal p As PopupContainerEdit, ByVal value As Boolean)
        EnsurePropertiesExists(p).ButtonMoreOptions = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the campo obligatorio.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetButtonMoreOptions(ByVal p As PopupContainerEdit)
        SetButtonMoreOptions(p, CBool(System.Environment.Version.ToString()))
    End Sub

#End Region

#Region "PopUpAnimation"
    ''' <summary>
    ''' Funcion para obtener si el popup tiene animacion
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad para establecer la animacion del popup")> _
    <Category("Appearance")> _
    Public Function GetPopUpAnimation(ByVal p As PopupContainerEdit) As Boolean
        Return EnsurePropertiesExists(p).PopUpAnimation
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo es obligatorio
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    <DefaultValue(True)>
    Public Sub SetPopUpAnimation(ByVal p As PopupContainerEdit, ByVal value As Boolean)
        EnsurePropertiesExists(p).PopUpAnimation = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the campo obligatorio.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetPopUpAnimation(ByVal p As PopupContainerEdit)
        SetPopUpAnimation(p, CBool(System.Environment.Version.ToString()))
    End Sub


#End Region

#Region "PanleControl"
    ''' <summary>
    ''' Funcion para obtener si el popup tiene animacion
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad para establecer la animacion del popup")> _
    <Category("Appearance")> _
    Public Function GetPanleControl(ByVal p As PopupContainerEdit) As DevExpress.XtraEditors.PanelControl
        Return EnsurePropertiesExists(p).PanleControl
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo es obligatorio
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Public Sub SetPanleControl(ByVal p As PopupContainerEdit, ByVal value As DevExpress.XtraEditors.PanelControl)
        EnsurePropertiesExists(p).PanleControl = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the campo obligatorio.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetPanleControl(ByVal p As PopupContainerEdit)
        SetPanleControl(p, New DevExpress.XtraEditors.PanelControl)
    End Sub


#End Region

#Region "WpfControl"
    ''' <summary>
    ''' Funcion para obtener si el popup tiene animacion
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad para establecer la animacion del popup")> _
    <Category("Appearance")> _
    Public Function GetWpfControl(ByVal p As PopupContainerEdit) As WpfAnimationPopUp
        Return EnsurePropertiesExists(p).WpfControl
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo es obligatorio
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Public Sub SetWpfControl(ByVal p As PopupContainerEdit, ByVal value As WpfAnimationPopUp)
        EnsurePropertiesExists(p).WpfControl = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the campo obligatorio.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetWpfControl(ByVal p As PopupContainerEdit)
        SetWpfControl(p, New WpfAnimationPopUp)
    End Sub


#End Region

#Region "HostControl"
    ''' <summary>
    ''' Funcion para obtener si el popup tiene animacion
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad para establecer la animacion del popup")> _
    <Category("Appearance")> _
    Public Function GetHostControl(ByVal p As PopupContainerEdit) As ElementHost
        Return EnsurePropertiesExists(p).HostControl
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo es obligatorio
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Public Sub SetHostControl(ByVal p As PopupContainerEdit, ByVal value As ElementHost)
        EnsurePropertiesExists(p).HostControl = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the campo obligatorio.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetHostControl(ByVal p As PopupContainerEdit)
        SetHostControl(p, New ElementHost)
    End Sub


#End Region

#End Region

#Region "Eventos"



    ''' <summary>
    ''' Indica al objeto que se ha completado la inicialización.
    ''' en este evento asigno la mascara correspondiente y la apariencia del control
    ''' </summary>
    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Hashtable
            Dim p As PopupContainerEdit = TryCast(de.Key, PopupContainerEdit)
            p.MinimumSize = New Size(p.Size.Width, 32)
            p.Size = New Size(p.Size.Width, 32)
            If GetButtonMoreOptions(p) = True Then
                p.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
                p.Properties.ShowPopupCloseButton = False
                p.Properties.PopupSizeable = False
                p.Properties.AutoHeight = False
                p.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                p.Properties.Buttons.Clear()
                Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
                p.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, My.Resources.Resources.Agregar16, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
            End If
            p.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)

            If DesignMode = False Then

                If GetOpenForm(p) = True Then
                    If p.Properties.Buttons.Count > 1 Then
                        p.Properties.Buttons.RemoveAt(1)
                    End If
                    If indigo.IsAllowPermissionForm(GetTagForm(p)) Then
                        p.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)})
                    End If
                End If

                If GetPopUpAnimation(p) = True Then
                    'p.BackColor = Color.White
                    p.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                    p.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.NoBorder
                    p.Properties.ShowPopupShadow = False
                    Dim PopControl As DevExpress.XtraEditors.PopupContainerControl = p.Properties.PopupControl
                    If PopControl IsNot Nothing Then
                        AddHandler p.QueryPopUp, AddressOf QueryPopUp
                    End If
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Indica al objeto que está comenzando la inicialización.
    ''' </summary>
    Public Sub BeginInit() Implements System.ComponentModel.ISupportInitialize.BeginInit

    End Sub

    ''' <summary>
    ''' Evento querypopup donde realizamos la animacion de deslizar hacia abajo
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub QueryPopUp(ByVal Sender As Object, e As System.EventArgs)
        Dim p As DevExpress.XtraEditors.PopupContainerEdit = TryCast(Sender, DevExpress.XtraEditors.PopupContainerEdit)
        Dim PopControl As DevExpress.XtraEditors.PopupContainerControl = p.Properties.PopupControl
        Dim WpTransition As WpfAnimationPopUp = New WpfAnimationPopUp
        Dim Panel As New DevExpress.XtraEditors.PanelControl
        Dim Host As ElementHost = New ElementHost
        Panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Panel.BackColor = Color.White
        Panel.Appearance.BorderColor = Color.Red
        Panel.LookAndFeel.UseDefaultLookAndFeel = False
        Panel.Dock = DockStyle.Fill
        For i = 0 To PopControl.Controls.Count - 1
            Panel.Controls.Add(PopControl.Controls(0))
        Next
        WpTransition.Host.Child = Panel
        Host.Child = WpTransition
        Host.Dock = DockStyle.Fill
        SetWpfControl(p, WpTransition)
        SetPanleControl(p, Panel)
        SetHostControl(p, Host)
        p.Properties.PopupControl.Controls.Clear()
        p.Properties.PopupControl.Controls.Add(GetHostControl(p))
        AddHandler GetWpfControl(p).AnimationComplete, Sub()
                                                           PopControl.Controls.Clear()
                                                           PopControl.Controls.Add(GetPanleControl(p))
                                                       End Sub
        CType(CType(CType(PopControl, DevExpress.XtraEditors.PopupContainerControl).Controls(0), ElementHost).Child, WpfAnimationPopUp).OpenAnimation()
    End Sub



#End Region

#Region "Metodos"
    ''' <summary>
    ''' Inicializo el componente y inicializo la variable hashtable <see cref="IndigoPopupContainerEdit"/> class.
    ''' </summary>
    Public Sub New()
        Hashtable = New Hashtable()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoPopupContainerEdit"/> class.
    ''' </summary>
    ''' <param name="container">The container.</param>
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        Me.New()
        If container IsNot Nothing Then
            container.Add(Me)
        End If
    End Sub

    ''' <summary>
    ''' Initializes the component.
    ''' </summary>
    Private Sub InitializeComponent()
        Contenedor = New System.ComponentModel.Container()
    End Sub

    ''' <summary>
    ''' Libera los recursos no administrados que utiliza <see cref="T:System.ComponentModel.Component" /> y libera los recursos administrados de forma opcional.
    ''' </summary>
    ''' <param name="disposing">Es true para liberar tanto recursos administrados como no administrados; es false para liberar únicamente recursos no administrados.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Contenedor IsNot Nothing Then
                Contenedor.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ''' <summary>
    ''' Especifica si este objeto puede proporcionar las propiedades Extender al objeto especificado.
    ''' </summary>
    ''' <param name="extendee"><see cref="T:System.Object" /> para recibir las propiedades Extender.</param>
    ''' <returns>
    ''' truesi este objeto puede proporcionar propiedades extensoras al objeto especificado; en caso contrario, false.
    ''' </returns>
    Public Function CanExtend(ByVal extendee As Object) As Boolean Implements System.ComponentModel.IExtenderProvider.CanExtend
        Return True
    End Function

    ''' <summary>
    '''Metodo para establecer el tag del formulario para abrir
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">The value.</param>
    Public Sub SetTagForm(ByVal Obj As PopupContainerEdit, Value As String)
        EnsurePropertiesExists(Obj).TagForm = Value
    End Sub
    ''' <summary>
    ''' Funcion que devuelve el tag del formulario para abrir y crear nuevo
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    Public Function GetTagForm(ByVal p As PopupContainerEdit) As String
        Return EnsurePropertiesExists(p).TagForm
    End Function
    ''' <summary>
    '''Metodo para establecer si el SearchLookUpEdit abre un fomrulario
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">if set to <c>true</c> [value].</param>
    Public Sub SetOpenForm(ByVal Obj As PopupContainerEdit, Value As Boolean)
        EnsurePropertiesExists(Obj).OpenForm = Value
    End Sub
    ''' <summary>
    ''' Funcion que devuelve si el SearchLookUpEdit abre formulario para crear nuevo registro del datasource
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad que especifica si se agrega el boton para abrir los formulario de archivo segun correspondan")> _
    Public Function GetOpenForm(ByVal p As PopupContainerEdit) As Boolean
        Return EnsurePropertiesExists(p).OpenForm
    End Function
#End Region

End Class



