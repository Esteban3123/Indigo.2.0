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
''' Clase con toda la funcionalidad del control LabelControl
''' </summary>
<ProvideProperty("CampoObligatorio", GetType(LabelControl))> _
<ProvideProperty("AplicaEstilo", GetType(LabelControl))> _
Public Class IndigoLabelControl
    Inherits System.ComponentModel.Component
    Implements IExtenderProvider
    Implements ISupportInitialize

#Region "Variables Globales"
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
    ''' Clase con las propiedades adicionales del control
    ''' </summary>
    Private Class Propiedades
        Public CampoObligatorio As Boolean
        Public AplicaEstilo As Boolean
    End Class

    ''' <summary>
    ''' Metodo para agregar las propiedades adicionales al control
    ''' </summary>
    ''' <param name="key">The key.</param>
    ''' <returns></returns>
    Private Function EnsurePropertiesExists(ByVal key As Object) As Propiedades
        Dim p As Propiedades = DirectCast(Hashtable(key), Propiedades)
        If p Is Nothing Then
            p = New Propiedades()
            p.AplicaEstilo = True
            Hashtable(key) = p
        End If
        Return p
    End Function

#Region "CampoObligatorio"
    <Description("Propiedad para establecer el control como obligatorio")> _
    <Category("Appearance")> _
    Public Function GetCampoObligatorio(ByVal p As LabelControl) As Boolean
        Return EnsurePropertiesExists(p).CampoObligatorio
    End Function

    Public Sub SetCampoObligatorio(ByVal p As LabelControl, ByVal value As Boolean)
        EnsurePropertiesExists(p).CampoObligatorio = value
        p.Invalidate()
    End Sub

    Private Sub ResetCampoObligatorio(ByVal p As LabelControl)
        SetCampoObligatorio(p, CBool(System.Environment.Version.ToString()))
    End Sub

#End Region

#Region "AplicaEstilo"
    <Description("Propiedad para establecer si el label toma el estilo del control extendido")> _
    <Category("Appearance")> _
    Public Function GetAplicaEstilo(ByVal p As LabelControl) As Boolean
        Return EnsurePropertiesExists(p).AplicaEstilo
    End Function

    Public Sub SetAplicaEstilo(ByVal p As LabelControl, ByVal value As Boolean)
        EnsurePropertiesExists(p).AplicaEstilo = value
        p.Invalidate()
    End Sub

    Private Sub ResetAplicaEstilo(ByVal p As LabelControl)
        SetAplicaEstilo(p, CBool(System.Environment.Version.ToString()))
    End Sub


#End Region

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Indica al objeto que se ha completado la inicialización.
    ''' </summary>
    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Hashtable
            Dim p As LabelControl = TryCast(de.Key, LabelControl)
            Dim propiedades As Propiedades = TryCast(de.Value, Propiedades)

            If propiedades.AplicaEstilo Then

                p.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)

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
    ''' Initializes a new instance of the <see cref="IndigoLabelControl"/> class.
    ''' </summary>
    Public Sub New()
        Hashtable = New Hashtable()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoLabelControl"/> class.
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
