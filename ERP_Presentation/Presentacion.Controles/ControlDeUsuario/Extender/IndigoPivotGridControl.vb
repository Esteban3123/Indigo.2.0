'***********************************************************************
' Assembly         : Presentation.Controls
' Author           : Diego Andrés Roldán Lozano
' Created          : 21-08-2014
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
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports Microsoft.VisualBasic
Imports DevExpress.XtraPivotGrid
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Base
Imports System.Threading.Tasks

#End Region

''' <summary>
''' Clase con toda la funcionalidad del control extendido CheckEdit
''' </summary>
<ProvideProperty("GuardarXml", GetType(PivotGridControl))> _
Partial Public Class IndigoPivotGridControl
    Inherits System.ComponentModel.Component
    Implements IExtenderProvider
    Implements ISupportInitialize


#Region "Variable Generales"
    ''' <summary>
    ''' Nombre del tipo de control que extiende
    ''' </summary>
    Private Const MY_TYPE As String = "PivotGridControl"
    ''' <summary>
    ''' Formulario padre
    ''' </summary>
    Private _parentForm As Form
    ''' <summary>
    ''' Variable hashtable que contiene los controles de tipo textedit
    ''' </summary>
    Private Hashtable As Hashtable
    ''' <summary>
    ''' variable de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' The contenedor
    ''' </summary>
    Private Contenedor As System.ComponentModel.Container = Nothing
    ''' <summary>
    ''' Variable donde se van a guardar las definiciones de las rejillas
    ''' </summary>
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Clase que contiene todas las propiedades adicionales del control
    ''' </summary>
    Private Class Propiedades
        ''' <summary>
        ''' Propiedad que establece si se guarda la definicion del control
        ''' </summary>
        Public GuardarXml As Boolean
        Public DefaultLayout As System.IO.Stream
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
            p.GuardarXml = True
            p.DefaultLayout = New System.IO.MemoryStream()
            Hashtable(key) = p
        End If
        Return p
    End Function

#Region "GuardarXml"
    ''' <summary>
    ''' Funcion que retorna si la rejilla guarda o no XML 
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad que especifica si se guarda una definicion Xml de la Rejilla")> _
    Public Function GetGuardarXml(ByVal p As PivotGridControl) As Boolean
        Return EnsurePropertiesExists(p).GuardarXml
    End Function

    ''' <summary>
    ''' Metodo que establece si la rejilla guarda o no XML
    ''' </summary>
    ''' <param name="Value">if set to <c>true</c> [value].</param>
    Public Sub SetGuardarXml(ByVal p As PivotGridControl, ByVal value As Boolean)
        EnsurePropertiesExists(p).GuardarXml = value
    End Sub
#End Region

#End Region

#Region "Eventos"

    Private Async Sub PivotGridControl_DockChanged(sender As Object, e As EventArgs)
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub

    Private Async Sub PivotGridControl_DragDrop(sender As Object, e As DragEventArgs)
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub

    Private Async Sub PivotGridControl_FieldAreaChanged(sender As Object, e As PivotFieldEventArgs)
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub

    Private Async Sub PivotGridControl_FieldWidthChanged(sender As Object, e As PivotFieldEventArgs)
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub

    ''' <summary>
    ''' Indica al objeto que se ha completado la inicialización.
    ''' en este evento se asignan las propiedades del control
    ''' </summary>
    Public Async Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Hashtable
            Dim p As PivotGridControl = TryCast(de.Key, PivotGridControl)

            If DesignMode Then
                p.Appearance.FieldValue.Font = New System.Drawing.Font("Segoe UI Semibold", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                'p.Appearance.FieldValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
                p.Appearance.FieldHeader.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                'p.Appearance.FieldHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
                'p.Appearance.HeaderArea.BackColor = System.Drawing.Color.White
                'p.Appearance.HeaderArea.BackColor2 = System.Drawing.Color.White
                'p.Appearance.FieldValueTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
                p.Appearance.ColumnHeaderArea.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                'p.Appearance.ColumnHeaderArea.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
                'p.Appearance.FilterHeaderArea.BackColor = System.Drawing.Color.White
                'p.Appearance.FilterHeaderArea.BackColor2 = System.Drawing.Color.White
            End If
            'p.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
            'p.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))

            If DesignMode = False Then
                'Guardamos la definición por defecto
                Await Me.SaveDefaultDefinitionAsync(p)
                If EnsurePropertiesExists(de.Key).GuardarXml Then
                    AddHandler p.DockChanged, AddressOf PivotGridControl_DockChanged
                    AddHandler p.DragDrop, AddressOf PivotGridControl_DragDrop
                    AddHandler p.FieldAreaChanged, AddressOf PivotGridControl_FieldAreaChanged
                    AddHandler p.FieldWidthChanged, AddressOf PivotGridControl_FieldWidthChanged
                    'Cargamos la definición personalizada
                    Await Me.LoadDefinitionFromXmlAsync(p)
                End If
                If Me._parentForm Is Nothing Then
                    Me._parentForm = TryCast(de.Key, PivotGridControl).FindForm()
                End If
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
    ''' Carga la definición de la vista de un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Function LoadDefinitionFromXmlAsync(ByVal view As PivotGridControl) As Task
        Return Task.Factory.StartNew(Sub()
                                         If view.InvokeRequired Then
                                             view.BeginInvoke(Sub()
                                                                  Me.LoadDefinitionFromXml(view)
                                                              End Sub)
                                         Else
                                             Me.LoadDefinitionFromXml(view)
                                         End If
                                     End Sub)
    End Function

    ''' <summary>
    ''' Carga la definición de la vista de un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Sub LoadDefinitionFromXml(ByVal view As PivotGridControl)
        Try
            If My.Computer.FileSystem.FileExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml")) Then
                view.RestoreLayoutFromXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
            End If
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Graba la definición de la vista a un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a persistir</param>
    Private Function SaveDefinitionToXmlAsync(ByVal view As PivotGridControl) As Task
        Return Task.Factory.StartNew(Sub()
                                         If view.InvokeRequired Then
                                             view.BeginInvoke(Sub()
                                                                  Me.SaveDefinitionToXml(view)
                                                              End Sub)
                                         Else
                                             Me.SaveDefinitionToXml(view)
                                         End If
                                     End Sub)
    End Function

    ''' <summary>
    ''' Graba la definición de la vista a un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a persistir</param>
    Private Sub SaveDefinitionToXml(ByVal view As PivotGridControl)
        If Not My.Computer.FileSystem.DirectoryExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, "")) Then
            My.Computer.FileSystem.CreateDirectory(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, ""))
        End If
        view.SaveLayoutToXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
    End Sub

    ''' <summary>
    ''' Graba la definición por defecto de la vista a los archivos temporales de forma asíncrona
    ''' </summary>
    ''' <param name="view">Vista a grabar</param>
    Private Function SaveDefaultDefinitionAsync(ByVal view As PivotGridControl) As Task
        Return Task.Factory.StartNew(Sub() SaveDefaultDefinition(view))
    End Function

    ''' <summary>
    ''' Graba la definición por defecto de la vista a los archivos temporales
    ''' </summary>
    ''' <param name="view">Vista a grabar</param>
    Private Sub SaveDefaultDefinition(ByVal view As PivotGridControl)
        EnsurePropertiesExists(view).DefaultLayout = New System.IO.MemoryStream()
        view.SaveLayoutToStream(EnsurePropertiesExists(view).DefaultLayout)
        EnsurePropertiesExists(view).DefaultLayout.Position = 0
    End Sub

    ''' <summary>
    ''' Carga la definición por defecto de forma asíncrona
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Function LoadDefaultDefinitionAsync(ByVal view As PivotGridControl) As Task
        Return Task.Factory.StartNew(Sub() LoadDefaultDefinition(view))
    End Function

    ''' <summary>
    ''' Carga la definición por defecto
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Sub LoadDefaultDefinition(ByVal view As PivotGridControl)
        EnsurePropertiesExists(view).DefaultLayout.Position = 0
        If view.InvokeRequired Then
            view.BeginInvoke(Sub()
                                 view.RestoreLayoutFromStream(EnsurePropertiesExists(view).DefaultLayout)
                             End Sub)
        Else
            view.RestoreLayoutFromStream(EnsurePropertiesExists(view).DefaultLayout)
        End If
        EnsurePropertiesExists(view).DefaultLayout.Position = 0
    End Sub

    ''' <summary>
    ''' Restaura los layouts por defecto de todas las rejillas
    ''' </summary>
    Public Function RestoreDefaultLayoutsAsync() As Task
        Return Task.Factory.StartNew(AddressOf RestoreDefaultLayouts)
    End Function

    ''' <summary>
    ''' Restaura los layouts por defecto de todas las rejillas
    ''' </summary>
    Public Sub RestoreDefaultLayouts()
        For Each de As DictionaryEntry In Hashtable
            Dim gridView As PivotGridControl = DirectCast(de.Key, PivotGridControl)
            If gridView IsNot Nothing Then
                Dim def = String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, gridView.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml")
                'Eliminamos la definición personalizada
                If System.IO.File.Exists(def) Then
                    System.IO.File.Delete(def)
                End If
                'Cargamos la definición por defecto
                Me.LoadDefaultDefinition(gridView)
            End If
        Next
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