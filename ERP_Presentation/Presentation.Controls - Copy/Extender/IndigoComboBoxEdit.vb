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
#Region "Librerias Importadas"
Imports System.ComponentModel
Imports System.Collections
Imports System.Diagnostics
Imports DevExpress.XtraEditors
Imports System.Drawing
#End Region

''' <summary>
''' Clase con toda la funcionalidad extendida del cotnrol comboboxEdit
''' </summary>
<ProvideProperty("CampoObligatorio", GetType(ComboBoxEdit))> _
<ProvideProperty("CampoValido", GetType(ComboBoxEdit))> _
Public Class IndigoComboBoxEdit
    Inherits System.ComponentModel.Component
    Implements IExtenderProvider
    Implements ISupportInitialize
#Region "Variable Generales"
    ''' <summary>
    ''' Variable hashtable que contiene los controles de tipo textedit
    ''' </summary>
    Private Hashtable As Hashtable
    ''' <summary>
    ''' The contenedor
    ''' </summary>
    Private Contenedor As System.ComponentModel.Container = Nothing
    ''' <summary>
    ''' The bandera
    ''' </summary>
    Dim bandera As Boolean
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Clase que contiene las propiedades que se agregan al control
    ''' </summary>
    Private Class Propiedades
        ''' <summary>
        ''' propiedad que contiene si el campo es obligatorio
        ''' </summary>
        Public CampoObligatorio As Boolean
        ''' <summary>
        ''' propiedad que contiene el tamaño minimo del texto ingresado
        ''' </summary>
        Public TamañoMinimoString As Integer
        ''' <summary>
        ''' propiedad que contiene si el campo es valido o no
        ''' </summary>
        Public CampoValido As Boolean

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

#Region "CampoObligatorio"
    ''' <summary>
    ''' Funcion para obtener si el campo es obligatorio
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad para establecer el control como obligatorio")> _
    <Category("Appearance")> _
    Public Function GetCampoObligatorio(ByVal p As ComboBoxEdit) As Boolean
        Return EnsurePropertiesExists(p).CampoObligatorio
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo es obligatorio
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Public Sub SetCampoObligatorio(ByVal p As ComboBoxEdit, ByVal value As Boolean)
        EnsurePropertiesExists(p).CampoObligatorio = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the campo obligatorio.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetCampoObligatorio(ByVal p As ComboBoxEdit)
        SetCampoObligatorio(p, CBool(System.Environment.Version.ToString()))
    End Sub

#End Region

#Region "TamañoMinimoString"
    ''' <summary>
    ''' Funcion que devuelve si el campo es valido o no
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Category("Design")> _
    <DefaultValue(False)> _
    Public Function GetTamañoMinimoString(ByVal p As ComboBoxEdit) As Integer
        Return EnsurePropertiesExists(p).TamañoMinimoString
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo es valido o no
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Public Sub SetTamañoMinimoString(ByVal p As ComboBoxEdit, ByVal value As Integer)
        EnsurePropertiesExists(p).TamañoMinimoString = value
    End Sub
#End Region

#Region "CampoValido"
    ''' <summary>
    ''' Funcion que devuelve si el campo es valido o no
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad Saber Si el Campo Es valido")> _
    <Category("Appearance")> _
    Public Function GetCampoValido(ByVal p As ComboBoxEdit) As Boolean
        Return EnsurePropertiesExists(p).CampoValido
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo es valido o no
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Private Sub SetCampoValido(ByVal p As ComboBoxEdit, ByVal value As Boolean)
        EnsurePropertiesExists(p).CampoValido = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the campo valido.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetCampoValido(ByVal p As ComboBoxEdit)
        SetCampoValido(p, CBool(System.Environment.Version.ToString()))
    End Sub

#End Region
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Inicializo el componente y inicializo la variable hashtable <see cref="IndigoTextEdit"/> class.
    ''' </summary>
    Public Sub New()
        Hashtable = New Hashtable()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoComboBoxEdit"/> class.
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


#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento Validating del control.
    ''' preguntamos por el valor minimo del control para cambiar el color del control
    ''' preguntamos si el control es obligatorio para cambiar color en caso de que sea vacio
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub EventoValidating(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim p As ComboBoxEdit = TryCast(sender, ComboBoxEdit)
        If p.SelectedIndex >= 0 Then
            p.BackColor = Color.White
            p.ToolTip = ""
            SetCampoValido(p, True)
        Else
            If GetCampoObligatorio(p) = True And p.Enabled = True Then
                SetCampoValido(p, False)
                p.BackColor = Color.MistyRose
                If p.Tag Is Nothing Then
                    p.ToolTip = "Este Campo es Necesario"
                Else
                    p.ToolTip = "Este Campo es Necesario: " + p.Tag.ToString()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento enabled changed para cambiar el color de control en el caso que corresponda
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub EventoEnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim p As ComboBoxEdit = TryCast(sender, ComboBoxEdit)
        If p.Enabled = True Then
            p.BackColor = Color.White
            SetCampoValido(p, True)
            If GetCampoObligatorio(p) = True And p.SelectedIndex < 0 Then
                SetCampoValido(p, False)
                p.BackColor = Color.MistyRose
                If p.Tag Is Nothing Then
                    p.ToolTip = "Este Campo es Necesario"
                Else
                    p.ToolTip = "Este Campo es Necesario: " + p.Tag.ToString()
                End If
            End If
        Else
            SetCampoValido(p, True)
            p.BackColor = Color.WhiteSmoke
            p.ToolTip = ""
        End If
    End Sub

    ''' <summary>
    ''' Evento gotfocus para seleccionar todo el texto del control y hacer el show popup
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        If bandera = False Then
            Dim p As ComboBoxEdit = TryCast(sender, ComboBoxEdit)
            p.ShowPopup()
            p.SelectAll()
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando el mouse sale dondel control.
    ''' </summary>
    Private Sub MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        bandera = False
    End Sub

    ''' <summary>
    ''' Evento cuando el mouse entra al control.
    ''' </summary>
    Private Sub MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        bandera = True
    End Sub

    ''' <summary>
    ''' Evento show del control
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub AbrirShow(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim p As ComboBoxEdit = TryCast(sender, ComboBoxEdit)
        p.ShowPopup()
        p.SelectAll()
    End Sub

    ''' <summary>
    ''' Indica al objeto que está comenzando la inicialización.
    ''' </summary>
    Public Sub BeginInit() Implements System.ComponentModel.ISupportInitialize.BeginInit

    End Sub

    ''' <summary>
    ''' Indica al objeto que se ha completado la inicialización.
    ''' y cambia la apariencia del control
    ''' </summary>
    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Hashtable
            Dim p As ComboBoxEdit = TryCast(de.Key, ComboBoxEdit)
            p.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
            p.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
            p.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
            p.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
            p.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            AddHandler p.SelectedIndexChanged, AddressOf EventoValidating
            AddHandler p.EnabledChanged, AddressOf EventoEnabledChanged
            AddHandler p.Enter, AddressOf GotFocus
            AddHandler p.MouseEnter, AddressOf MouseEnter
            AddHandler p.Leave, AddressOf MouseLeave
            AddHandler p.Click, AddressOf AbrirShow
        Next
    End Sub
#End Region

End Class

