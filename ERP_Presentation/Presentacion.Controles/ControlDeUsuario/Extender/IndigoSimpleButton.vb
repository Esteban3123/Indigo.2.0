'***********************************************************************
' Assembly         : Presentation.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 29-01-2014
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
''' Clase con toda la funcionalidad del control extendido SimpleButton
''' </summary>
<ProvideProperty("ModernUiIndigo", GetType(SimpleButton))> _
Public Class IndigoSimpleButton
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
        ''' Propiedad que establece si el campo tiene diseño metro
        ''' </summary>
        Public ModernUiIndigo As Boolean
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

#Region "ModernUiIndigo"
    ''' <summary>
    ''' Funcion para obtener si el campo tiene diseño metro
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad para establecer el control con diseño metro")> _
    <Category("Appearance")> _
    Public Function GetModernUiIndigo(ByVal p As SimpleButton) As Boolean
        Return EnsurePropertiesExists(p).ModernUiIndigo
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo tiene diseño metro
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Public Sub SetModernUiIndigo(ByVal p As SimpleButton, ByVal value As Boolean)
        EnsurePropertiesExists(p).ModernUiIndigo = value
        p.Invalidate()
    End Sub

    ''' <summary>
    ''' Resets the campo diseño metro.
    ''' </summary>
    ''' <param name="p">The p.</param>
    Private Sub ResetModernUiIndigo(ByVal p As SimpleButton)
        SetModernUiIndigo(p, CBool(System.Environment.Version.ToString()))
    End Sub
#End Region
#End Region

#Region "Eventos"
    ''' <summary>
    ''' Indica al objeto que se ha completado la inicialización.
    ''' en este evento asigno la mascara correspondiente y la apariencia del control
    ''' y agregamos la mascara por defecto del control
    ''' </summary>
    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Hashtable
            Dim p As SimpleButton = TryCast(de.Key, SimpleButton)
            If GetModernUiIndigo(p) = True Then
                If DesignMode Then
                    p.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
                End If
                p.Size = New Size(p.Size.Width, 36)
            End If
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
