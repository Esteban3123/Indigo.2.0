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
''' Clase con toda la funcionalidad del control extendido RadioGroup
''' </summary>
<ProvideProperty("CampoObligatorio", GetType(RadioGroup))> _
Public Class IndigoRadioGroup
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
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Clase que contiene todas las propiedades adicionales del control
    ''' </summary>
    Private Class Propiedades
        ''' <summary>
        ''' Propiedad que establece si el campo es obligatorio
        ''' </summary>
        Public CampoObligatorio As Boolean
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
    Public Function GetCampoObligatorio(ByVal p As RadioGroup) As Boolean
        Return EnsurePropertiesExists(p).CampoObligatorio
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo es obligatorio
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Public Sub SetCampoObligatorio(ByVal p As RadioGroup, ByVal value As Boolean)
        EnsurePropertiesExists(p).CampoObligatorio = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the campo obligatorio.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetCampoObligatorio(ByVal p As RadioGroup)
        SetCampoObligatorio(p, CBool(System.Environment.Version.ToString()))
    End Sub
#End Region
#End Region

#Region "Eventos"
    ''' <summary>
    ''' Evento Validating del control donde preguntamos por la propiedad numerico para cambiar el separador decimales a .
    ''' preguntamos por el valor minimo del control para cambiar el color del control
    ''' preguntamos si el control es obligatorio para cambiar color en caso de que sea vacio
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub EventoValidating(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim p As RadioGroup = TryCast(sender, RadioGroup)
        If GetCampoObligatorio(p) = True And p.Enabled = True Then
            p.BackColor = Color.MistyRose
            If p.EditValue Is Nothing Then
                If p.Tag Is Nothing Then
                    p.ToolTip = "Este Campo es Necesario"
                Else
                    p.ToolTip = "Este Campo es Necesario: " + p.Tag.ToString()
                End If
            Else
                p.BackColor = Color.White
                p.ToolTip = ""
            End If
        Else
            p.BackColor = Color.White
            p.ToolTip = ""
        End If
    End Sub

    ''' <summary>
    ''' Evento enabled changed para cambiar el color de control en el caso que corresponda
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub EventoEnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim p As RadioGroup = TryCast(sender, RadioGroup)
        If p.Enabled = True Then
            p.BackColor = Color.White
            If GetCampoObligatorio(p) = True And p.Text = String.Empty Then
                p.BackColor = Color.MistyRose
                If p.EditValue Is Nothing Then
                    If p.Tag Is Nothing Then
                        p.ToolTip = "Este Campo es Necesario"
                    Else
                        p.ToolTip = "Este Campo es Necesario: " + p.Tag.ToString()
                    End If
                End If
            End If
        Else
            p.BackColor = Color.WhiteSmoke
            p.ToolTip = ""
        End If
    End Sub

    ''' <summary>
    ''' Evento gotfocus para seleccionar todo el texto del control
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim p As RadioGroup = TryCast(sender, RadioGroup)
        p.SelectAll()
    End Sub

    ''' <summary>
    ''' Indica al objeto que se ha completado la inicialización.
    ''' en este evento asigno la mascara correspondiente y la apariencia del control
    ''' y agregamos la mascara por defecto del control
    ''' </summary>
    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Hashtable
            Dim p As RadioGroup = TryCast(de.Key, RadioGroup)
            p.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
            p.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
            p.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
            p.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            p.Size = New Size(p.Size.Width, 30)
            AddHandler p.EditValueChanged, AddressOf EventoValidating
            AddHandler p.EnabledChanged, AddressOf EventoEnabledChanged
            AddHandler p.GotFocus, AddressOf GotFocus
        Next
    End Sub

    ''' <summary>
    ''' Indica al objeto que está comenzando la inicialización.
    ''' </summary>
    Public Sub BeginInit() Implements System.ComponentModel.ISupportInitialize.BeginInit

    End Sub
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoDate"/> class.
    ''' </summary>
    Public Sub New()
        Hashtable = New Hashtable()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoDate"/> class.
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

End Class


