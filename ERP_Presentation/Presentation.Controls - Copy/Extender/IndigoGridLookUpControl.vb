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
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Threading.Tasks

#End Region

''' <summary>
''' Clase que contiene toda la funcionalidad del control extendido gridlookupedit
''' </summary>
<ProvideProperty("GuardarXmlGrid", GetType(GridLookUpEdit))> _
<ProvideProperty("AbrirFormularioArchivo", GetType(GridLookUpEdit))> _
<ProvideProperty("TagFormularioAbrir", GetType(GridLookUpEdit))>
Partial Public Class IndigoGridLookUpControl
    Inherits System.ComponentModel.Component
    Implements IExtenderProvider
    Implements ISupportInitialize

#Region "Variables Globales"
    ''' <summary>
    ''' Nombre del tipo de control que extiende
    ''' </summary>
    Private Const MY_TYPE As String = "GridLookUpEdit"
    ''' <summary>
    ''' Formulario padre
    ''' </summary>
    Private _parentForm As Form
    ''' <summary>
    ''' Variable hashtable que contiene los controles
    ''' </summary>
    Private HashtableLookUps As Hashtable
    Private HashtableLookUpView As Hashtable
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
    ''' Clase que contiene las propiedades adcionales del control Gridlookupedit
    ''' </summary>
    Private Class Propiedades
        Public GuardarXmlGrid As Boolean
        Public AbrirFormularioArchivo As Boolean
        Public TagFormularioAbrir As String
    End Class

    ''' <summary>
    ''' Funcion para agregar las propiedades del control
    ''' </summary>
    ''' <param name="key">The key.</param>
    ''' <returns></returns>
    Private Function EnsurePropertiesExists(ByVal key As Object) As Propiedades
        Dim p As Propiedades = DirectCast(HashtableLookUps(key), Propiedades)
        If p Is Nothing Then
            p = New Propiedades()
            HashtableLookUps(key) = p
        End If
        Return p
    End Function

    ''' <summary>
    ''' Funcion que retorna si el gridlookedit guarde la definicion del xml
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad que especifica si se guarda una definicion Xml del gridlookupedit")> _
    Public Function GetGuardarXmlGrid(ByVal p As GridLookUpEdit) As Boolean
        Return EnsurePropertiesExists(p).GuardarXmlGrid
    End Function

    ''' <summary>
    ''' Funcion que devuelve si el gridlookupedit abre formulario para crear nuevo registro del datasource
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad que especifica si se agrega el boton para abrir los formulario de archivo segun correspondan")> _
    Public Function GetAbrirFormularioArchivo(ByVal p As GridLookUpEdit) As Boolean
        Return EnsurePropertiesExists(p).AbrirFormularioArchivo
    End Function

    ''' <summary>
    ''' Funcion que devuelve el tag del formulario para abrir y crear nuevo
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    Public Function GetTagFormularioAbrir(ByVal p As GridLookUpEdit) As String
        Return EnsurePropertiesExists(p).TagFormularioAbrir
    End Function

    ''' <summary>
    ''' Metodo para establecer si el gridlookupedit guarda xml
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">if set to <c>true</c> [value].</param>
    Public Sub SetGuardarXmlGrid(ByVal Obj As GridLookUpEdit, Value As Boolean)
        EnsurePropertiesExists(Obj).GuardarXmlGrid = Value
    End Sub

    ''' <summary>
    '''Metodo para establecer si el gridlookupedit abre un fomrulario
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">if set to <c>true</c> [value].</param>
    Public Sub SetAbrirFormularioArchivo(ByVal Obj As GridLookUpEdit, Value As Boolean)
        EnsurePropertiesExists(Obj).AbrirFormularioArchivo = Value
    End Sub

    ''' <summary>
    '''Metodo para establecer el tag del formulario para abrir
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">The value.</param>
    Public Sub SetTagFormularioAbrir(ByVal Obj As GridLookUpEdit, Value As String)
        EnsurePropertiesExists(Obj).TagFormularioAbrir = Value
    End Sub
#End Region

#Region "Enumeracion"
    ''' <summary>
    ''' Enumeracion de actividades
    ''' </summary>
    Enum Formulario
        Actividades = 0
    End Enum
#End Region

#Region "HotTrack"
    Private hotTrackRow_Renamed As Integer = DevExpress.XtraGrid.GridControl.InvalidRowHandle

    Private Sub HotTrackRow(ByVal Sender As GridView, ByVal Value As Integer)
        If hotTrackRow_Renamed <> Value Then
            Dim prevHotTrackRow As Integer = hotTrackRow_Renamed
            hotTrackRow_Renamed = Value
            If Sender.ActiveEditor IsNot Nothing Then
                Sender.PostEditor()
            End If
            Sender.RefreshRow(prevHotTrackRow)
            Sender.RefreshRow(hotTrackRow_Renamed)
            If hotTrackRow_Renamed >= 0 Then
                Sender.GridControl.Cursor = Cursors.Hand
            Else
                Sender.GridControl.Cursor = Cursors.Default
            End If
        End If
    End Sub


    Private Sub MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim view As GridView = TryCast(sender, GridView)
        Dim info As GridHitInfo = view.CalcHitInfo(New Point(e.X, e.Y))
        If info.InRowCell Then
            HotTrackRow(sender, info.RowHandle)
        Else
            HotTrackRow(sender, DevExpress.XtraGrid.GridControl.InvalidRowHandle)
        End If
    End Sub
    Private Sub MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        HotTrackRow(CType(sender, GridLookUpEdit).Properties.View, DevExpress.XtraGrid.GridControl.InvalidRowHandle)
    End Sub
    Private Sub RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        If e.RowHandle = hotTrackRow_Renamed Then
            e.Appearance.BackColor = Color.LightBlue
        End If
    End Sub
#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se utiliza dar la apariencia al autofilterrow
    ''' </summary>
    Private Sub INDGridControl_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        If e.RowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            e.Appearance.BackColor = Color.LightGray
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para guardar la definicion de la rejilla cada ves que dimensionamos el tamaño de las columnas.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.ColumnEventArgs" /> instance containing the event data.</param>
    Private Async Sub INDGridControl_ColumnWidthChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ColumnEventArgs)
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para guardar la definicion de la rejilla cada ves que se agregan,se quitan columnas de la rejila se dispara al cerrar el formulario de customizacion.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDGridControl_HideCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs)
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub

    ''' <summary>
    ''' Evento que se utilza para activar el timer que porsteriormente guardara la definicion de la rejilla este evento se activa cada ves que hago Drop a una columna de la rejilla
    ''' </summary>
    Private Async Sub INDGridControl_ColumnPositionChanged(sender As Object, e As EventArgs)
        Await Me.SaveDefinitionToXmlAsync(sender.View)
    End Sub

    ''' <summary>
    ''' Aqui se restaura la definicion
    ''' </summary>
    Private Async Sub INDGridControl_DataSourceChanged(sender As Object, e As EventArgs)
        Await Me.LoadDefinitionFromXmlAsync(sender)
    End Sub

    ''' <summary>
    ''' Indica al objeto que se ha completado la inicialización.
    ''' </summary>
    Public Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        For Each de As DictionaryEntry In HashtableLookUps
            AddHandler TryCast(de.Key, GridLookUpEdit).MouseLeave, AddressOf MouseLeave
            TryCast(de.Key, GridLookUpEdit).Properties.ImmediatePopup = True
            Dim p As GridView
            p = TryCast(de.Key, GridLookUpEdit).Properties.View
            If Not HashtableLookUpView.ContainsKey(p) Then
                HashtableLookUpView.Add(p, TryCast(de.Key, GridLookUpEdit))
            End If
            If p IsNot Nothing Then
                p.OptionsView.EnableAppearanceEvenRow = True
                p.OptionsView.EnableAppearanceOddRow = True
                p.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                p.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                AddHandler p.RowStyle, AddressOf INDGridControl_RowStyle
                AddHandler p.MouseMove, AddressOf MouseMove
                AddHandler p.RowStyle, AddressOf RowStyle

                Me._parentForm = TryCast(de.Key, GridLookUpEdit).FindForm()

                If Me.EnsurePropertiesExists(de.Key).GuardarXmlGrid Then
                    AddHandler p.ColumnWidthChanged, AddressOf INDGridControl_ColumnWidthChanged
                    AddHandler p.HideCustomizationForm, AddressOf INDGridControl_HideCustomizationForm
                    AddHandler p.ColumnPositionChanged, AddressOf INDGridControl_ColumnPositionChanged
                    AddHandler p.DataSourceChanged, AddressOf INDGridControl_DataSourceChanged
                End If
            End If

            Dim a As GridLookUpEdit = TryCast(de.Key, GridLookUpEdit)
            If a IsNot Nothing Then
                a.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                If GetAbrirFormularioArchivo(a) = True Then
                    If a.Properties.Buttons.Count > 1 Then
                        a.Properties.Buttons.RemoveAt(1)
                    End If
                    Try
                        If indigo.ActiveForms.ContainsKey(GetTagFormularioAbrir(a)) Then
                            Dim exist = indigo.ActiveForms(GetTagFormularioAbrir(a)).Item4.Exists(Function(x) x = "40")
                            If exist Then
                                a.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)})
                            End If
                        End If
                    Catch
                    End Try
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Indica al objeto que está comenzando la inicialización.
    ''' </summary>
    Public Sub BeginInit() Implements ISupportInitialize.BeginInit

    End Sub
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoGridLookUpControl"/> class.
    ''' </summary>
    Public Sub New()
        HashtableLookUps = New Hashtable()
        HashtableLookUpView = New Hashtable()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoGridLookUpControl"/> class.
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
    ''' Carga la definición de la vista de un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Function LoadDefinitionFromXmlAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub()
                                         If TryCast(HashtableLookUpView(view), GridLookUpEdit).InvokeRequired Then
                                             TryCast(HashtableLookUpView(view), GridLookUpEdit).BeginInvoke(Sub()
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
    Private Sub LoadDefinitionFromXml(ByVal view As GridView)
        If My.Computer.FileSystem.FileExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml")) Then
            view.RestoreLayoutFromXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
            view.ClearColumnsFilter()
            view.FindFilterText = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Graba la definición de la vista a un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a persistir</param>
    Private Function SaveDefinitionToXmlAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub()
                                         If view.GridControl.InvokeRequired Then
                                             view.GridControl.BeginInvoke(Sub()
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
    Private Sub SaveDefinitionToXml(ByVal view As GridView)
        If Not My.Computer.FileSystem.DirectoryExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, "")) Then
            My.Computer.FileSystem.CreateDirectory(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, ""))
        End If
        view.SaveLayoutToXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
    End Sub

#End Region

End Class
