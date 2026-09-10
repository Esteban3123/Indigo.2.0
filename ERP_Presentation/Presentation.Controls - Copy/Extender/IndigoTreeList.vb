'***********************************************************************
' Assembly         : Presentation.Controls
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 7-03-2014
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
Imports DevExpress.XtraTreeList
#End Region

''' <summary>
''' Clase con toda la funcionalidad del control extendido CheckEdit
''' </summary>
<ProvideProperty("ExpandedAllNodes", GetType(TreeList))> _
Public Class IndigoTreeList
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
        ''' Propiedad que establece si se expanden todos los nodos del treelit
        ''' </summary>
        Public ExpandedAllNodes As Boolean
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

#Region "ExpandedAllNodes"
    ''' <summary>
    ''' Funcion para obtener si el campo es obligatorio
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad para abrir o contraer todos los nodos")> _
    <Category("Appearance")> _
    Public Function GetExpandedAllNodes(ByVal p As TreeList) As Boolean
        Return EnsurePropertiesExists(p).ExpandedAllNodes
    End Function

    ''' <summary>
    ''' Metodo para establecer si el campo es obligatorio
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <param name="value">if set to <c>true</c> [value].</param>
    Public Sub SetExpandedAllNodes(ByVal p As TreeList, ByVal value As Boolean)
        EnsurePropertiesExists(p).ExpandedAllNodes = value
        If value = True Then
            p.ExpandAll()
        Else
            p.CollapseAll()
        End If
    End Sub
#End Region

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Indica al objeto que se ha completado la inicialización.
    ''' en este evento se asignan las propiedades del control
    ''' </summary>
    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Hashtable
            Dim p As TreeList = TryCast(de.Key, TreeList)
            p.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            p.Appearance.HeaderPanel.Options.UseFont = True
            p.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
            p.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
            p.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
            p.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
            p.Appearance.Row.Options.UseFont = True
            p.Appearance.Row.ForeColor = Color.Black
            p.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
            p.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
            p.Appearance.FocusedRow.ForeColor = Color.White
            p.OptionsView.EnableAppearanceOddRow = True
            p.OptionsView.EnableAppearanceEvenRow = True
            p.OptionsBehavior.EnableFiltering = True
            p.OptionsFilter.FilterMode = FilterMode.Smart
            AddHandler p.Invalidated, AddressOf Invalidated_Control
        Next
    End Sub

    ''' <summary>
    ''' Indica al objeto que está comenzando la inicialización.
    ''' </summary>
    Public Sub BeginInit() Implements System.ComponentModel.ISupportInitialize.BeginInit

    End Sub

    ''' <summary>
    ''' Handles the Control event of the Invalidated control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="InvalidateEventArgs"/> instance containing the event data.</param>
    Private Sub Invalidated_Control(sender As Object, e As InvalidateEventArgs)
        If sender IsNot Nothing Then
            Me.SetStyleFindPanel(sender)
        End If
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Sets the style find panel.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    Private Sub SetStyleFindPanel(sender As TreeList)
        Dim find As DevExpress.Utils.FindPanel = TryCast(sender.Controls.OfType(Of DevExpress.Utils.FindPanel)().FirstOrDefault(), DevExpress.Utils.FindPanel)
        If find IsNot Nothing AndAlso find.Appearance.BackColor <> Color.White Then
            find.Dock = DockStyle.Top
            find.BorderStyle = BorderStyle.None
            find.Appearance.BackColor = Color.White
            find.FindEdit.Font = New Font("Segoe UI Light", 12.0!)
            find.FindButton.Font = New Font("Segoe UI Light", 12.0!)
            find.FindButton.Size = New Size(100, 30)
            find.ClearButton.Font = New Font("Segoe UI Light", 12.0!)
            find.ClearButton.Size = New Size(100, 30)
            find.MinimumSize = New Size(CType(sender, TreeList).Size.Width, find.Size.Height + 30)
            find.MaximumSize = New Size(CType(sender, TreeList).Size.Width, find.Size.Height + 30)
            CType(sender, TreeList).BackColor = Color.White
            'AddHandler find.FindEdit.GotFocus, AddressOf FindEdit_GotFocus
            'EnsurePropertiesExists(sender).FindPanelStyle = True
            sender.RefreshDataSource()
        End If
    End Sub

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
