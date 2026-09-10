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
Imports System.Threading.Tasks
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraLayout
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Clase con toda la funcionalidad del control extendido GridControl
''' </summary>
<ProvideProperty("HoldSize", GetType(GridControl))>
<ProvideProperty("HotTrack", GetType(GridControl))>
<ProvideProperty("SizeConstraintsType", GetType(GridControl))>
<ProvideProperty("GuardarXml", GetType(GridControl))>
<ProvideProperty("AddActions", GetType(GridControl))>
<ProvideProperty("ExportButton", GetType(GridControl))>
<ProvideProperty("ControlNextFocus", GetType(GridControl))>
<ProvideProperty("HideNoRecords", GetType(GridControl))>
Partial Public Class IndigoGridControl
    Inherits System.ComponentModel.Component
    Implements IExtenderProvider
    Implements ISupportInitialize

#Region "Variables Globales"

    ''' <summary>
    ''' Nombre del tipo de control que extiende
    ''' </summary>
    Private Const MY_TYPE As String = "GridControl"
    ''' <summary>
    ''' Formulario padre
    ''' </summary>
    Private _parentForm As Form
    ''' <summary>
    ''' Variable hashtable que contiene los controles de tipo textedit
    ''' </summary>
    Private Hashtable As Hashtable
    ''' <summary>
    ''' Rejillas que tienen los eventos pausados
    ''' </summary>
    Private GridControlStopEventList As New List(Of GridControl)()
    ''' <summary>
    ''' The contenedor
    ''' </summary>
    Private Contenedor As System.ComponentModel.Container = Nothing
    ''' <summary>
    ''' Variable para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    Private LayouControlItem As DevExpress.XtraLayout.LayoutControlItem
    Private HoldSize As Boolean
    Private HotTrack As Boolean
    'Private newRecordButton As NewRecordButton

    Private lis As List(Of eAction)
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' propiedad para indicarle a la rejilla que acepte tipos XPInstantFeedbackSource y LinqInstantFeedbackSource
    ''' </summary>
    ''' <remarks></remarks>
    Dim _acceptXpo As Boolean = False
    WriteOnly Property AcceptXPO As Boolean
        Set(value As Boolean)
            _acceptXpo = value
        End Set
    End Property

    ''' <summary>
    ''' Clase con las propiedades adicionales del control
    ''' </summary>
    Private Class Propiedades
        Public Event OnClick(sender As Object, e As EventArgs)
        Public Property SizeConstraintsType As DevExpress.XtraLayout.SizeConstraintsType
        Public Property SizeLayoutItem As Size
        Public Property MinSizeLayoutItem As Size
        Public Property MaxSizeLayoutItem As Size
        Public Property HScrollVisibility As Views.Base.ScrollVisibility
        Public GuardarXml As Boolean
        Public HoldSize As Boolean
        Public HotTrack As Boolean
        Public LayouControlItem As DevExpress.XtraLayout.LayoutControlItem
        Public AddActions As List(Of Presentation.Base.eAcciones)
        Public HideNoRecords As Boolean
        Public Property ExportButton As Boolean
        Public Property ExportButtonControl As ExportDataButton
        Public Property ShowNewRecordButton As Boolean
        Public Property NewRecordButton As NewRecordButton
        Public Property FindPanelStyle As Boolean
        Public Property ShowFooter As Boolean
        Public Property ControlNextFocus As Control
        Public Property DefaultLayout As System.IO.Stream
    End Class

    ''' <summary>
    ''' Metodo para agregar las propiedades adcionales a cada control de tipo gridcontrol
    ''' </summary>
    ''' <param name="key">The key.</param>
    ''' <returns></returns>
    Private Function EnsurePropertiesExists(ByVal key As Object) As Propiedades
        Dim p As Propiedades = DirectCast(Hashtable(key), Propiedades)
        If p Is Nothing Then
            p = New Propiedades()
            'Inicializamos propiedades por defecto
            p.GuardarXml = True
            p.DefaultLayout = New System.IO.MemoryStream()
            Hashtable(key) = p
        End If
        Return p
    End Function

#Region "SizeConstraintsType"
    ''' <summary>
    ''' Funcion que retorna el tipo de constraints
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad que especifica el tipo de constraints")>
    Public Function GetSizeConstraintsType(ByVal p As GridControl) As DevExpress.XtraLayout.SizeConstraintsType
        Return EnsurePropertiesExists(p).SizeConstraintsType
    End Function

    ''' <summary>
    ''' Metodo que establece el tipo de constraints
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">if set to <c>true</c> [value].</param>
    Public Sub SetSizeConstraintsType(ByVal Obj As GridControl, Value As DevExpress.XtraLayout.SizeConstraintsType)
        EnsurePropertiesExists(Obj).SizeConstraintsType = Value
    End Sub
#End Region

#Region "Size Layout"
    ''' <summary>
    ''' Funcion que retorna el tamaño del layout
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("El Tamaño del layout Item")>
    Public Function GetSizeLayoutItem(ByVal p As GridControl) As Size
        Return EnsurePropertiesExists(p).SizeLayoutItem
    End Function

    ''' <summary>
    ''' Metodo que establece el tipo de constraints
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">if set to <c>true</c> [value].</param>
    Public Sub SetSizeLayoutItem(ByVal Obj As GridControl, Value As Size)
        EnsurePropertiesExists(Obj).SizeLayoutItem = Value
    End Sub

    Private Function GetMinSizeLayoutItem(ByVal p As GridControl) As Size
        Return EnsurePropertiesExists(p).MinSizeLayoutItem
    End Function
    Private Sub SetMinSizeLayoutItem(ByVal Obj As GridControl, Value As Size)
        EnsurePropertiesExists(Obj).MinSizeLayoutItem = Value
    End Sub

    Private Function GetMaxSizeLayoutItem(ByVal p As GridControl) As Size
        Return EnsurePropertiesExists(p).MaxSizeLayoutItem
    End Function
    Private Sub SetMaxSizeLayoutItem(ByVal Obj As GridControl, Value As Size)
        EnsurePropertiesExists(Obj).MaxSizeLayoutItem = Value
    End Sub 'Views.Base.ScrollVisibility.Never

    Private Function GetHScrollVisibility(ByVal p As GridControl) As Views.Base.ScrollVisibility
        Return EnsurePropertiesExists(p).HScrollVisibility
    End Function
    Private Sub SetHScrollVisibility(ByVal Obj As GridControl, Value As Views.Base.ScrollVisibility)
        EnsurePropertiesExists(Obj).HScrollVisibility = Value
    End Sub 'Views.Base.ScrollVisibility.Never
#End Region

#Region "GuardarXml"
    ''' <summary>
    ''' Funcion que retorna si la rejilla guarda o no XML 
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad que especifica si se guarda una definicion Xml de la Rejilla")>
    Public Function GetGuardarXml(ByVal p As GridControl) As Boolean
        Return EnsurePropertiesExists(p).GuardarXml
    End Function

    ''' <summary>
    ''' Metodo que establece si la rejilla guarda o no XML
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">if set to <c>true</c> [value].</param>
    Public Sub SetGuardarXml(ByVal Obj As GridControl, Value As Boolean)
        EnsurePropertiesExists(Obj).GuardarXml = Value
    End Sub
#End Region

#Region "ShowNewRecordButton"
    ''' <summary>
    ''' Funcion que retorna si la rejilla guarda o no XML 
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad que especifica si se muestra el botón de nuevo registro en la rejilla")>
    Public Function GetShowNewRecordButton(ByVal p As GridControl) As Boolean
        Return EnsurePropertiesExists(p).ShowNewRecordButton
    End Function

    ''' <summary>
    ''' Metodo que establece si la rejilla guarda o no XML
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">if set to <c>true</c> [value].</param>
    Public Sub SetShowNewRecordButton(ByVal Obj As GridControl, Value As Boolean)
        EnsurePropertiesExists(Obj).ShowNewRecordButton = Value
        If EnsurePropertiesExists(Obj).ShowNewRecordButton AndAlso Obj.MainView IsNot Nothing AndAlso CType(Obj.MainView, GridView).OptionsView.ShowFooter Then
            If EnsurePropertiesExists(Obj).NewRecordButton IsNot Nothing Then
                EnsurePropertiesExists(Obj).NewRecordButton.Visible = True
            End If
        Else
            If EnsurePropertiesExists(Obj).NewRecordButton IsNot Nothing Then
                EnsurePropertiesExists(Obj).NewRecordButton.Visible = False
            End If
        End If

    End Sub
#End Region

#Region "LayouControlItem"

    <Description("Propiedad que especifica si se guarda una definicion Xml de la Rejilla")>
    <DefaultValue(True)>
    Public Function GetLayouControlItem(ByVal p As GridControl) As DevExpress.XtraLayout.LayoutControlItem
        Return EnsurePropertiesExists(p).LayouControlItem
    End Function


    Public Sub SetLayouControlItem(ByVal Obj As GridControl, Value As DevExpress.XtraLayout.LayoutControlItem)
        EnsurePropertiesExists(Obj).LayouControlItem = Value
    End Sub
#End Region

#Region "HoldSize"

    <Description("Propiedad que especifica si se guarda una definicion Xml de la Rejilla")>
    <DefaultValue(True)>
    Public Function GetHoldSize(ByVal p As GridControl) As Boolean
        Return EnsurePropertiesExists(p).HoldSize
    End Function


    Public Sub SetHoldSize(ByVal Obj As GridControl, Value As Boolean)
        EnsurePropertiesExists(Obj).HoldSize = Value
    End Sub
#End Region

#Region "HotTrack"

    <Description("Propiedad que especifica si se activa la funcionalidad del hotTrack")>
    <DefaultValue(True)>
    Public Function GetHotTrack(ByVal p As GridControl) As Boolean
        Return EnsurePropertiesExists(p).HotTrack
    End Function


    Public Sub SetHotTrack(ByVal Obj As GridControl, Value As Boolean)
        EnsurePropertiesExists(Obj).HotTrack = Value
    End Sub
#End Region

#Region "Actions"
    <Description("Propiedad que obtiene el listado de las acciones")>
    Public Function GetAddActions(ByVal p As GridControl) As List(Of Presentation.Base.eAcciones)
        Return EnsurePropertiesExists(p).AddActions
    End Function

    <Description("Propiedad que estable el listado de  las acciones")>
    Public Sub SetAddActions(ByVal Obj As GridControl, ByVal ListActions As List(Of Presentation.Base.eAcciones))
        EnsurePropertiesExists(Obj).AddActions = ListActions
    End Sub
#End Region

#Region "HideNoRecords"
    <Description("Propiedad que indica si se va a mostrar el mensaje de no hay registros"), DefaultValue(False)>
    Public Function GetHideNoRecords(ByVal p As GridControl) As Boolean
        Return EnsurePropertiesExists(p).HideNoRecords
    End Function

    <Description("Propiedad que indica si se va a mostrar el mensaje de no hay registros")>
    Public Sub SetHideNoRecords(ByVal Obj As GridControl, ByVal hideNoRecords As Boolean)
        EnsurePropertiesExists(Obj).HideNoRecords = hideNoRecords
    End Sub
#End Region

#Region "ExportButton"

    ''' <summary>
    ''' Obtiene un valor que indica si se muestra el botón de exportado
    ''' </summary>
    ''' <param name="p">Objeto a extender</param>
    ''' <returns>Un valor que indica si se muestra el botón de exportado</returns>
    <Description("Obtiene o asigna un valor que indica si se muestra el botón de exportado")>
    Public Function GetExportButton(ByVal p As GridControl) As Boolean
        Return EnsurePropertiesExists(p).ExportButton
    End Function


    Public Sub SetExportButtonControl(ByVal obj As GridControl, value As Boolean)
        If value Then
            EnsurePropertiesExists(obj).ExportButtonControl = New ExportDataButton(obj)
        Else
            EnsurePropertiesExists(obj).ExportButtonControl = Nothing
        End If
    End Sub
    ''' <summary>
    ''' Asigna un valor que indica si se muestra el boton de exportado
    ''' </summary>
    ''' <param name="Obj">Objeto a extender</param>
    ''' <param name="Value">Valor que indica si se muestra el botón de exportado</param>
    Public Sub SetExportButton(ByVal Obj As GridControl, Value As Boolean)
        EnsurePropertiesExists(Obj).ExportButton = Value
        If EnsurePropertiesExists(Obj).ExportButton AndAlso Obj.MainView IsNot Nothing AndAlso CType(Obj.MainView, GridView).OptionsView.ShowFooter Then
            If EnsurePropertiesExists(Obj).ExportButtonControl IsNot Nothing Then
                EnsurePropertiesExists(Obj).ExportButtonControl.Visible = True
            End If
        Else
            If EnsurePropertiesExists(Obj).ExportButtonControl IsNot Nothing Then
                EnsurePropertiesExists(Obj).ExportButtonControl.Visible = False
            End If
        End If
    End Sub

#End Region

#Region "ControlNextFocus"

    <Browsable(False)>
    Public Function GetControlNextFocus(ByVal p As GridControl) As Control
        Return EnsurePropertiesExists(p).ControlNextFocus
    End Function


    Public Sub SetControlNextFocus(ByVal obj As GridControl, ByVal value As Control)
        EnsurePropertiesExists(obj).ControlNextFocus = value
    End Sub


    Public WriteOnly Property ControlNextFocus
        Set(value)
            Me.flagFindPanelFocus = True
        End Set
    End Property

#End Region

#End Region

#Region "Eventos"
    ''' <summary>
    ''' Evento que se utiliza dar la apariencia al autofilterrow
    ''' </summary>
    Private Sub INDGridControl_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        If e.RowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            'e.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para guardar la definicion de la rejilla cada ves que dimensionamos el tamaño de las columnas.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.ColumnEventArgs" /> instance containing the event data.</param>
    Private Async Sub INDGridControl_ColumnWidthChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ColumnEventArgs)
        If Not Me.GridControlStopEventList.Contains(sender.GridControl) Then
            Await Me.SaveDefinitionToXmlAsync(sender)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para guardar la definicion de la rejilla cada ves que se agregan,se quitan columnas de la rejila se dispara al cerrar el formulario de customizacion.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDGridControl_HideCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not Me.GridControlStopEventList.Contains(sender.GridControl) Then
            Await Me.SaveDefinitionToXmlAsync(sender)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utilza para activar el timer que porsteriormente guardara la definicion de la rejilla este evento se activa cada ves que hago Drop a una columna de la rejilla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs" /> instance containing the event data.</param>
    Private Async Sub INDGridControl_ColumnPositionChanged(sender As Object, e As EventArgs)
        If Not Me.GridControlStopEventList.Contains(sender.View.GridControl) Then
            Await Me.SaveDefinitionToXmlAsync(sender.View)
        End If
    End Sub

    Private Sub INDGridView_CustomDrawEmptyForeground(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs)
        If DesignMode Then
            Exit Sub
        End If
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        If view.RowCount <> 0 Then
            Return
        End If
        Dim drawFormat As New StringFormat()
        drawFormat.LineAlignment = StringAlignment.Center
        drawFormat.Alignment = drawFormat.LineAlignment
        'Dim newImage As Image = Global.Resources.NoRecordFound
        'e.Graphics.DrawImage(newImage, 100, 100)
        e.Graphics.DrawString(Presentation.Base.BaseClass.obtenerRecurso(Base.Eresources.ComunesNoHayRegistros, Base.Eform.Comunes),
                              e.Appearance.Font, SystemBrushes.ControlDark, New RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), drawFormat)
    End Sub

    ''' <summary>
    ''' Indica al objeto que se ha completado la inicialización.
    ''' </summary>
    Public Async Sub EndInit() Implements System.ComponentModel.ISupportInitialize.EndInit
        Dim grid As GridControl = Nothing
        Dim gridView As GridView = Nothing
        Dim propertyObj As Propiedades = Nothing
        For Each de As DictionaryEntry In Hashtable
            grid = CType(de.Key, GridControl)
            gridView = grid.MainView
            propertyObj = EnsurePropertiesExists(de.Key)
            SetSizeLayoutItem(grid, New Size(grid.Size.Width, grid.Size.Height))
            SetMinSizeLayoutItem(grid, New Size(grid.Size.Width, grid.Size.Height))
            SetMaxSizeLayoutItem(grid, New Size(grid.Size.Width, grid.Size.Height))

            If gridView IsNot Nothing Then
                SetHScrollVisibility(grid, CType(grid.MainView, GridView).HorzScrollVisibility)
                If DesignMode = False Then
                    gridView.Appearance.Empty.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
                    AddHandler gridView.RowCountChanged, AddressOf RowChanged
                    AddHandler grid.DataSourceChanged, AddressOf DataSourceChanged
                    AddHandler gridView.CustomDrawEmptyForeground, AddressOf INDGridView_CustomDrawEmptyForeground
                End If
                SetLayouControlItem(grid, Nothing)
                If grid.Parent IsNot Nothing Then
                    If grid.Parent.GetType.FullName = "DevExpress.XtraLayout.LayoutControl" Then
                        Dim layoutControl As DevExpress.XtraLayout.LayoutControl = CType(grid.Parent, DevExpress.XtraLayout.LayoutControl)
                        For i = 0 To layoutControl.Items.Count - 1
                            If layoutControl.Items(i).GetType.FullName = "DevExpress.XtraLayout.LayoutControlItem" Then
                                Dim layoutControlItem As DevExpress.XtraLayout.LayoutControlItem = CType(layoutControl.Items(i), DevExpress.XtraLayout.LayoutControlItem)
                                If layoutControlItem.Control.Name = grid.Name Then
                                    SetLayouControlItem(de.Key, layoutControl.Items(i))
                                    SetSizeConstraintsType(grid, layoutControlItem.SizeConstraintsType)
                                    SetSizeLayoutItem(grid, New Size(layoutControlItem.Size.Width, 0))
                                    SetMinSizeLayoutItem(grid, New Size(layoutControlItem.MinSize.Width, layoutControlItem.MinSize.Height))
                                    SetMaxSizeLayoutItem(grid, New Size(layoutControlItem.MaxSize.Width, layoutControlItem.MaxSize.Height))
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                End If

                If Not DesignMode Then
                    'Habilitamos el botón de exportado
                    propertyObj.ExportButtonControl = New ExportDataButton(grid)
                    propertyObj.ShowFooter = gridView.OptionsView.ShowFooter

                    If propertyObj.ShowFooter Then
                        If propertyObj.ExportButton Then
                            propertyObj.ExportButtonControl.Visible = True
                        End If
                        If propertyObj.ShowNewRecordButton Then
                            gridView.ViewCaption = " "
                            'gridView.OptionsView.ShowViewCaption = True
                            propertyObj.NewRecordButton = New NewRecordButton(grid)
                            propertyObj.NewRecordButton.Visible = True

                            'AddHandler propertyObj.NewRecordButton.NewRecordClick, Sub(sender, e) RaiseEvent ClickAdd(grid, e)
                            AddHandler propertyObj.NewRecordButton.NewRecordClick, Sub(sender, e) RaiseEvent ClickAdd(sender, e)
                        End If
                    Else
                        propertyObj.ExportButtonControl.Visible = False
                        'propertyObj.NewRecordButton.Visible = False
                    End If

                    AddHandler grid.Invalidated, AddressOf Invalidated_GridControl
                    AddHandler grid.DefaultViewChanged, AddressOf DefaultViewChanged_GridControl

                    If Me._parentForm Is Nothing Then
                        Me._parentForm = grid.FindForm()
                    End If
                End If
                If DesignMode Then
                    gridView.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
                    'gridView.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
                    'gridView.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
                End If
                If DesignMode = False Then
                    'Guardamos la definición por defecto
                    Await Me.SaveDefaultDefinitionAsync(gridView)
                    If propertyObj.GuardarXml Then
                        AddHandler gridView.ColumnWidthChanged, AddressOf INDGridControl_ColumnWidthChanged
                        AddHandler gridView.HideCustomizationForm, AddressOf INDGridControl_HideCustomizationForm
                        AddHandler gridView.ColumnPositionChanged, AddressOf INDGridControl_ColumnPositionChanged
                        'Cargamos la definición personalizada
                        Me.LoadDefinitionFromXml(gridView)
                        RefreshGrid(grid)
                    End If
                    AddHandler gridView.GotFocus, AddressOf GotFocus
                    AddHandler gridView.MouseMove, AddressOf MouseMove
                    AddHandler gridView.RowStyle, AddressOf RowStyle
                    'Se agrega manejador al evento KeyDown para lanzar el evento PasteToGrid
                    AddHandler gridView.KeyDown, AddressOf KeyDown_GridView
                End If
            End If
        Next
        RaiseEvent EndInitCompleted(Me, New EventArgs())
    End Sub

    ''' <summary>
    ''' Asocia los eventos a una nueva MainView
    ''' </summary>
    ''' <param name="newGridView"></param>
    Public Sub UpdateNewMainView(ByVal newGridView As GridView)
        ' Primero eliminamos los controladores de eventos existentes
        RemoveHandlersFromGridView(newGridView)

        AddHandler newGridView.GotFocus, AddressOf GotFocus
        AddHandler newGridView.MouseMove, AddressOf MouseMove
        AddHandler newGridView.RowStyle, AddressOf RowStyle
        'Se agrega manejador al evento KeyDown para lanzar el evento PasteToGrid
        AddHandler newGridView.KeyDown, AddressOf KeyDown_GridView
    End Sub

    Public Sub RemoveHandlersFromGridView(ByVal gridView As GridView)
        RemoveHandler gridView.GotFocus, AddressOf GotFocus
        RemoveHandler gridView.MouseMove, AddressOf MouseMove
        RemoveHandler gridView.RowStyle, AddressOf RowStyle
        RemoveHandler gridView.KeyDown, AddressOf KeyDown_GridView
    End Sub


    Private Sub DataSourceChanged(ByVal Sender As GridControl, ByVal e As System.EventArgs)
        RefreshGrid(Sender)
    End Sub

    Public Sub RefreshGrid(ByVal Sender As GridControl)
        If GetHoldSize(Sender) = False Then
            If Sender.DataSource IsNot Nothing Then
                If _acceptXpo = False Then
                    If Sender.DataSource.GetType.Name = "XPInstantFeedbackSource" Then
                        Exit Sub
                    End If
                    If Sender.DataSource.GetType.Name = "LinqInstantFeedbackSource" Then
                        Exit Sub
                    End If
                End If
            End If

            Dim LayoutItem As DevExpress.XtraLayout.LayoutControlItem = GetLayouControlItem(Sender)
            Dim sizeLayoutItem As Size = GetSizeLayoutItem(Sender)
            Dim minSizeLayoutItem As Size = GetMinSizeLayoutItem(Sender)
            Dim maxSizeLayoutItem As Size = GetMaxSizeLayoutItem(Sender)
            Dim gridView As GridView = CType(Sender.MainView, GridView)

            If gridView Is Nothing Then
                Exit Sub
            End If

            Dim propertyObj As Propiedades = EnsurePropertiesExists(Sender)
            Dim viewInfo As GridViewInfo = CType(gridView.GetViewInfo(), GridViewInfo)
            Dim heigth As Int32 = 35 'If(viewInfo.HScrollBarPresence = ScrollBarPresence.Visible, 50, 35)
            Try
                If Sender.DataSource Is Nothing AndAlso Not GetHideNoRecords(Sender) Then
                    'gridView.ViewCaption = Presentation.Base.BaseClass.obtenerRecurso(Base.Eresources.ComunesNoHayRegistros, Base.Eform.Comunes)
                    'gridView.OptionsView.ShowViewCaption = True
                    'gridView.HorzScrollVisibility = Views.Base.ScrollVisibility.Never
                    'If LayoutItem IsNot Nothing Then
                    '    LayoutItem.SizeConstraintsType = SizeConstraintsType.Custom
                    '    If GetSizeConstraintsType(Sender) = SizeConstraintsType.Custom Then
                    '        LayoutItem.MinSize = New Size(minSizeLayoutItem.Width, heigth)
                    '        LayoutItem.MaxSize = New Size(maxSizeLayoutItem.Width, heigth)
                    '    Else
                    '        LayoutItem.MinSize = New Size(sizeLayoutItem.Width, heigth)
                    '        LayoutItem.MaxSize = New Size(sizeLayoutItem.Width, heigth)
                    '    End If
                    '    'LayoutItem.Size = New Size(LayoutItem.MaxSize.Width, 35)
                    'Else
                    '    Sender.Size = New Size(Sender.Size.Width, 35)
                    'End If
                    'gridView.OptionsView.ShowFooter = False
                    'gridView.OptionsFind.AlwaysVisible = False
                    ''Se oculta el boton de exportar
                    'If propertyObj.ExportButtonControl IsNot Nothing AndAlso propertyObj.ExportButton Then
                    '    propertyObj.ExportButtonControl.Visible = False
                    'End If
                Else
                    If Sender.DataSource IsNot Nothing AndAlso Not (Sender.DataSource.GetType().Name = "XPInstantFeedbackSource" Or Sender.DataSource.GetType().Name = "LinqInstantFeedbackSource") AndAlso Sender.DataSource.count <= 0 AndAlso Not GetHideNoRecords(Sender) Then
                        'gridView.ViewCaption = Presentation.Base.BaseClass.obtenerRecurso(Base.Eresources.ComunesNoHayRegistros, Base.Eform.Comunes)
                        'gridView.OptionsView.ShowViewCaption = True
                        'gridView.HorzScrollVisibility = Views.Base.ScrollVisibility.Never
                        'If LayoutItem IsNot Nothing Then
                        '    LayoutItem.SizeConstraintsType = SizeConstraintsType.Custom
                        '    If GetSizeConstraintsType(Sender) = SizeConstraintsType.Custom Then
                        '        LayoutItem.MinSize = New Size(minSizeLayoutItem.Width, heigth)
                        '        LayoutItem.MaxSize = New Size(maxSizeLayoutItem.Width, heigth)
                        '    Else
                        '        LayoutItem.MinSize = New Size(sizeLayoutItem.Width, heigth)
                        '        LayoutItem.MaxSize = New Size(sizeLayoutItem.Width, heigth)
                        '    End If
                        '    'LayoutItem.Size = New Size(LayoutItem.MaxSize.Width, 35)
                        'Else
                        '    Sender.Size = New Size(Sender.Size.Width, 35)
                        'End If
                        'gridView.OptionsView.ShowFooter = False
                        'gridView.OptionsFind.AlwaysVisible = False
                        ''Se oculta el boton de exportar
                        'If propertyObj.ExportButtonControl IsNot Nothing AndAlso propertyObj.ExportButton Then
                        '    propertyObj.ExportButtonControl.Visible = False
                        'End If
                    Else
                        'gridView.OptionsView.ShowViewCaption = False
                        gridView.HorzScrollVisibility = GetHScrollVisibility(Sender)
                        If LayoutItem IsNot Nothing Then
                            LayoutItem.SizeConstraintsType = GetSizeConstraintsType(Sender)
                            LayoutItem.MinSize = GetMinSizeLayoutItem(Sender)
                            LayoutItem.MaxSize = GetMaxSizeLayoutItem(Sender)
                            'LayoutItem.Size = GetSizeLayoutItem(Sender)
                        Else
                            Sender.Size = GetSizeLayoutItem(Sender)
                        End If
                        gridView.OptionsView.ShowFooter = propertyObj.ShowFooter
                        'Se muestra el boton de exportar
                        If propertyObj.ExportButtonControl IsNot Nothing AndAlso propertyObj.ExportButton Then
                            propertyObj.ExportButtonControl.Visible = propertyObj.ShowFooter
                        End If

                        If propertyObj.NewRecordButton IsNot Nothing AndAlso propertyObj.ShowNewRecordButton Then
                            propertyObj.NewRecordButton.Visible = propertyObj.ShowFooter
                        End If
                    End If
                End If
            Catch
                'gridView.OptionsView.ShowViewCaption = False
                gridView.HorzScrollVisibility = GetHScrollVisibility(Sender)
                If LayoutItem IsNot Nothing Then
                    LayoutItem.SizeConstraintsType = GetSizeConstraintsType(Sender)
                    LayoutItem.MinSize = GetMinSizeLayoutItem(Sender)
                    LayoutItem.MaxSize = GetMaxSizeLayoutItem(Sender)
                    'LayoutItem.Size = GetSizeLayoutItem(Sender)
                Else
                    Sender.Size = GetSizeLayoutItem(Sender)
                End If
            End Try
        End If
    End Sub

    Private Sub RowChanged(ByVal Sender As GridView, ByVal e As System.EventArgs)
        If GetHoldSize(Sender.GridControl) = False Then
            If Sender.GridControl.DataSource IsNot Nothing Then
                Dim IndigoSession = SessionValues.Instance
                Dim MaskDate = Utils.GetCustomDateFormat(If(IndigoSession IsNot Nothing, IndigoSession.dateFormat, -1))
                Dim MaskTime = Utils.GetCustomTimeFormat(If(IndigoSession IsNot Nothing, IndigoSession.dateFormat, -1))

                Sender.Columns.Where(Function(x) DirectCast(x, DevExpress.XtraGrid.Columns.GridColumn).ColumnType.Name = "DateTime").ToList() _
                        .ForEach(Sub(col)
                                     col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime

                                     If col.DisplayFormat.FormatString.ToLower().StartsWith("hh:mm") Then
                                         col.DisplayFormat.FormatString = MaskTime
                                     ElseIf col.DisplayFormat.FormatString.ToLower().Contains("hh:mm") Then
                                         col.DisplayFormat.FormatString = $"{MaskDate} {MaskTime}"
                                     Else
                                         col.DisplayFormat.FormatString = MaskDate
                                     End If
                                 End Sub)

                If Sender.GridControl.DataSource.GetType.Name = "XPInstantFeedbackSource" Then
                    Exit Sub
                End If
                If Sender.GridControl.DataSource.GetType.Name = "LinqInstantFeedbackSource" Then
                    Exit Sub
                End If
            End If
            Dim LayoutItem As DevExpress.XtraLayout.LayoutControlItem = GetLayouControlItem(Sender.GridControl)
            Try
                If Sender.GridControl.DataSource Is Nothing OrElse CType(Sender.GridControl.DataSource, IList).Count = 0 AndAlso Not GetHideNoRecords(Sender.GridControl) Then
                    'Sender.ViewCaption = Presentation.Base.BaseClass.obtenerRecurso(Base.Eresources.ComunesNoHayRegistros, Base.Eform.Comunes)
                    'Sender.OptionsView.ShowViewCaption = True
                    'If LayoutItem IsNot Nothing Then
                    '    LayoutItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
                    '    LayoutItem.MinSize = New Size(LayoutItem.Size.Width, 35)
                    '    LayoutItem.MaxSize = New Size(LayoutItem.Size.Width, 35)
                    '    ' LayoutItem.Size = New Size(LayoutItem.Size.Width, 35)
                    'Else
                    '    Sender.GridControl.Size = New Size(Sender.GridControl.Size.Width, 35)
                    'End If
                Else
                    If Sender.GridControl.DataSource.count <= 0 AndAlso Not GetHideNoRecords(Sender.GridControl) Then
                        'Sender.ViewCaption = Presentation.Base.BaseClass.obtenerRecurso(Base.Eresources.ComunesNoHayRegistros, Base.Eform.Comunes)
                        'Sender.OptionsView.ShowViewCaption = True
                        'If LayoutItem IsNot Nothing Then
                        '    LayoutItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
                        '    LayoutItem.MinSize = New Size(LayoutItem.MaxSize.Width, 35)
                        '    LayoutItem.MaxSize = New Size(LayoutItem.MaxSize.Width, 35)
                        '    ' LayoutItem.Size = New Size(LayoutItem.MaxSize.Width, 35)
                        'Else
                        '    Sender.GridControl.Size = New Size(Sender.GridControl.Size.Width, 35)
                        'End If
                    Else
                        'Sender.OptionsView.ShowViewCaption = False
                        If LayoutItem IsNot Nothing Then
                            LayoutItem.SizeConstraintsType = GetSizeConstraintsType(Sender.GridControl)
                            LayoutItem.MinSize = GetSizeLayoutItem(Sender.GridControl)
                            LayoutItem.MaxSize = GetSizeLayoutItem(Sender.GridControl)
                            ' LayoutItem.Size = GetSizeLayoutItem(Sender.GridControl)
                        Else
                            Sender.GridControl.Size = GetSizeLayoutItem(Sender.GridControl)
                        End If
                    End If
                End If
            Catch
                'Sender.OptionsView.ShowViewCaption = False
                If Sender.GridControl.InvokeRequired Then
                    Sender.GridControl.BeginInvoke(Sub()
                                                       If LayoutItem IsNot Nothing Then
                                                           LayoutItem.SizeConstraintsType = GetSizeConstraintsType(Sender.GridControl)
                                                           LayoutItem.MinSize = GetSizeLayoutItem(Sender.GridControl)
                                                           LayoutItem.MaxSize = GetSizeLayoutItem(Sender.GridControl)
                                                           ' LayoutItem.Size = GetSizeLayoutItem(Sender.GridControl)
                                                       Else
                                                           Sender.GridControl.Size = GetSizeLayoutItem(Sender.GridControl)
                                                       End If
                                                   End Sub)
                Else
                    If LayoutItem IsNot Nothing Then
                        LayoutItem.SizeConstraintsType = GetSizeConstraintsType(Sender.GridControl)
                        LayoutItem.MinSize = GetSizeLayoutItem(Sender.GridControl)
                        LayoutItem.MaxSize = GetSizeLayoutItem(Sender.GridControl)
                        ' LayoutItem.Size = GetSizeLayoutItem(Sender.GridControl)
                    Else
                        Sender.GridControl.Size = GetSizeLayoutItem(Sender.GridControl)
                    End If
                End If
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Indica al objeto que está comenzando la inicialización.
    ''' </summary>
    Public Sub BeginInit() Implements ISupportInitialize.BeginInit

    End Sub

    ''' <summary>
    ''' Ocurre cuando se repinta la rejilla
    ''' </summary>
    Private Sub Invalidated_GridControl(sender As Object, e As InvalidateEventArgs)
        If sender IsNot Nothing Then
            Me.SetStyleFindPanel(sender)
        End If
    End Sub

    ''' <summary>
    ''' Aqui se controla si se realiza una navegación hacia
    ''' una sub vista en la rejilla, para ocultar el botón
    ''' </summary>
    Private Sub DefaultViewChanged_GridControl(sender As Object, e As EventArgs)
        If EnsurePropertiesExists(sender).ExportButton Then
            If CType(sender, GridControl).DefaultView.ParentView IsNot Nothing Then
                EnsurePropertiesExists(sender).ExportButtonControl.Visible = False
            Else
                EnsurePropertiesExists(sender).ExportButtonControl.Visible = True
            End If
        End If

        If EnsurePropertiesExists(sender).ShowNewRecordButton Then
            If CType(sender, GridControl).DefaultView.ParentView IsNot Nothing Then
                EnsurePropertiesExists(sender).NewRecordButton.Visible = False
            Else
                EnsurePropertiesExists(sender).NewRecordButton.Visible = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Bandera usada para el evento de foco en
    ''' el panel de busqueda
    ''' </summary>
    Dim flagFindPanelFocus As Boolean

    ''' <summary>
    ''' Le da estilo al panel de busqueda de la rejilla
    ''' </summary>
    ''' <param name="grid"></param>
    Private Sub SetStyleFindPanel(ByVal grid As GridControl)
        'If grid.MainView IsNot Nothing AndAlso CType(grid.MainView, DevExpress.XtraGrid.Views.Grid.GridView).OptionsFind.AlwaysVisible Then
        '    Dim find As DevExpress.XtraGrid.Controls.FindControl = TryCast(grid.Controls.OfType(Of DevExpress.XtraGrid.Controls.FindControl)().FirstOrDefault(), DevExpress.XtraGrid.Controls.FindControl)
        '    If find IsNot Nothing AndAlso find.Appearance.BackColor <> Color.White Then
        '        find.Dock = DockStyle.Top
        '        find.BorderStyle = BorderStyle.None
        '        find.Appearance.BackColor = Color.White
        '        find.FindEdit.Font = New Font("Segoe UI Light", 12.0!)

        '        find.FindButton.Font = New Font("Segoe UI Light", 12.0!)
        '        find.FindButton.Size = New Size(100, 36)
        '        find.ClearButton.Font = New Font("Segoe UI Light", 12.0!)
        '        find.ClearButton.Size = New Size(100, 36)
        '        find.MinimumSize = New Size(find.Size.Width, find.Size.Height + 30)
        '        find.MaximumSize = New Size(find.Size.Width, find.Size.Height + 30)
        '        'AddHandler find.FindEdit.GotFocus, AddressOf FindEdit_GotFocus

        '        ''Dim _searchCombo As ComboBoxEdit = CType(DirectCast(find.Controls.Item(0), DevExpress.XtraLayout.LayoutControl).Controls.Item(5), ComboBoxEdit)
        '        ''_searchCombo.Properties.AllowFocused = False
        '        ''_searchCombo.Properties.ShowNullValuePromptWhenFocused = False
        '        ''find.FindButton.Focus()

        '        'Dim mreEdit As DevExpress.XtraEditors.MRUEdit = CType(CType(find.Controls.Item(0), DevExpress.XtraLayout.LayoutControl).Controls.Item(5), DevExpress.XtraEditors.MRUEdit)
        '        'mreEdit.Properties.AllowFocused = False
        '        'mreEdit.Properties.ShowNullValuePromptWhenFocused = False

        '        'EnsurePropertiesExists(grid).FindPanelStyle = True
        '        grid.RefreshDataSource()
        '    End If
        'End If
    End Sub
    Private Sub FindEdit_GotFocus(sender As Object, e As EventArgs)
        DirectCast(DirectCast(sender, DevExpress.XtraEditors.MRUEdit).Parent.Parent.Parent, GridControl).MainView.Focus()
        If flagFindPanelFocus Then
            If DirectCast(sender, DevExpress.XtraEditors.MRUEdit).Parent IsNot Nothing AndAlso DirectCast(sender, DevExpress.XtraEditors.MRUEdit).Parent.Parent IsNot Nothing AndAlso DirectCast(sender, DevExpress.XtraEditors.MRUEdit).Parent.Parent.Parent IsNot Nothing Then
                If EnsurePropertiesExists(DirectCast(DirectCast(sender, DevExpress.XtraEditors.MRUEdit).Parent.Parent.Parent, GridControl)).ControlNextFocus IsNot Nothing Then
                    EnsurePropertiesExists(DirectCast(DirectCast(sender, DevExpress.XtraEditors.MRUEdit).Parent.Parent.Parent, GridControl)).ControlNextFocus.Focus()
                End If
            End If
        End If
        flagFindPanelFocus = False
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Se lanza cuando se realiza un pegado de datos en una rejilla
    ''' </summary>
    ''' <param name="sender">Rejilla en la que ocurrió el pegado de datos</param>
    ''' <param name="e">Argumento del evento</param>
    Public Event PasteToGrid(ByVal sender As GridControl, ByVal e As PasteToGridEventArgs)

    ''' <summary>
    ''' Ocurre cuando se completa el evento EndInit
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event EndInitCompleted(ByVal sender As Object, ByVal e As EventArgs)

    ''' <summary>
    ''' Evento de click en agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event ClickAdd(sender As Object, e As EventArgs)
#End Region

#Region "HotTrack"
    Private hotTrackRow_Renamed As Integer = DevExpress.XtraGrid.GridControl.InvalidRowHandle
    Private isValidEditing As Boolean = True

    Private Sub HotTrackRow(ByVal Sender As GridView, ByVal Value As Integer)
        Try
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
        Catch
        End Try
    End Sub

    Private Sub MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If Not GetHotTrack(CType(sender, GridView).GridControl) Then
            Dim view As GridView = TryCast(sender, GridView)
            Dim info As GridHitInfo = view.CalcHitInfo(New Point(e.X, e.Y))
            If info.InRowCell Then
                HotTrackRow(sender, info.RowHandle)
            Else
                HotTrackRow(sender, DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            End If
        End If
    End Sub

    Private Sub RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        If e.RowHandle = hotTrackRow_Renamed Then
            'e.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
            'e.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(109, Byte), Integer))
            'e.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
            'e.Appearance.BorderColor = Color.White
        End If
    End Sub
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Detiene el guardado de la personalización para una rejilla
    ''' </summary>
    ''' <param name="grid">Rejilla a aplicar</param>
    Public Sub StopCustomization(ByVal grid As GridControl)
        If Not Me.GridControlStopEventList.Contains(grid) Then
            Me.GridControlStopEventList.Add(grid)
        End If
    End Sub

    ''' <summary>
    ''' Inicia el guardado de la personalización para una rejilla
    ''' </summary>
    ''' <param name="grid">Rejilla a aplicar</param>
    Public Sub StartCustomization(ByVal grid As GridControl)
        If Me.GridControlStopEventList.Contains(grid) Then
            Me.GridControlStopEventList.Remove(grid)
        End If
    End Sub

    ''' <summary>
    ''' Aqui se control la combinación de teclas Ctrl+V para lanzar el evento PasteToGrid
    ''' </summary>
    Private Sub KeyDown_GridView(sender As Object, e As KeyEventArgs)
        If e.Control AndAlso e.KeyCode = Keys.V Then 'Ctrl+V
            If Clipboard.ContainsText Then
                RaiseEvent PasteToGrid(CType(sender, GridView).GridControl, New PasteToGridEventArgs(Clipboard.GetText()))
            End If
        End If
    End Sub

    Private Sub GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim p As GridView = TryCast(sender, GridView)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoGridControl"/> class.
    ''' </summary>
    Public Sub New()
        Hashtable = New Hashtable()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="IndigoGridControl"/> class.
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
        If TypeOf extendee Is GridControl Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Carga la definición de la vista de un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Function LoadDefinitionFromXmlAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub()
                                         If view.GridControl.InvokeRequired Then
                                             view.GridControl.BeginInvoke(Sub()
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
        If Me._parentForm IsNot Nothing AndAlso My.Computer.FileSystem.FileExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml")) Then
            If view.GridControl IsNot Nothing Then

                If view.GridControl.InvokeRequired Then
                    view.GridControl.BeginInvoke(Sub()
                                                     view.RestoreLayoutFromXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
                                                     view.ClearColumnsFilter()
                                                     view.FindFilterText = String.Empty
                                                 End Sub)
                Else
                    view.RestoreLayoutFromXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
                    view.ClearColumnsFilter()
                    view.FindFilterText = String.Empty
                End If
            End If
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
        Try
            If Me._parentForm IsNot Nothing Then
                If Not My.Computer.FileSystem.DirectoryExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, "")) Then
                    My.Computer.FileSystem.CreateDirectory(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, ""))
                End If
                If view.GridControl.InvokeRequired Then
                    view.GridControl.BeginInvoke(Sub() view.SaveLayoutToXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml")))
                Else
                    view.SaveLayoutToXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Graba la definición por defecto de la vista a los archivos temporales de forma asíncrona
    ''' </summary>
    ''' <param name="view">Vista a grabar</param>
    Private Function SaveDefaultDefinitionAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub() SaveDefaultDefinition(view))
    End Function

    ''' <summary>
    ''' Graba la definición por defecto de la vista a los archivos temporales
    ''' </summary>
    ''' <param name="view">Vista a grabar</param>
    Private Sub SaveDefaultDefinition(ByVal view As GridView)
        EnsurePropertiesExists(view.GridControl).DefaultLayout = New System.IO.MemoryStream()
        view.SaveLayoutToStream(EnsurePropertiesExists(view.GridControl).DefaultLayout)
        EnsurePropertiesExists(view.GridControl).DefaultLayout.Position = 0
    End Sub

    ''' <summary>
    ''' Carga la definición por defecto de forma asíncrona
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Function LoadDefaultDefinitionAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub() LoadDefaultDefinition(view))
    End Function

    ''' <summary>
    ''' Carga la definición por defecto
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Sub LoadDefaultDefinition(ByVal view As GridView)
        EnsurePropertiesExists(view.GridControl).DefaultLayout.Position = 0
        If view.GridControl.InvokeRequired Then
            view.GridControl.BeginInvoke(Sub()
                                             view.RestoreLayoutFromStream(EnsurePropertiesExists(view.GridControl).DefaultLayout)
                                             view.ClearColumnsFilter()
                                             view.FindFilterText = String.Empty
                                             RefreshGrid(view.GridControl)
                                         End Sub)
        Else
            view.RestoreLayoutFromStream(EnsurePropertiesExists(view.GridControl).DefaultLayout)
            view.ClearColumnsFilter()
            view.FindFilterText = String.Empty
            RefreshGrid(view.GridControl)
        End If
        EnsurePropertiesExists(view.GridControl).DefaultLayout.Position = 0
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
            Dim gridView As GridView = DirectCast(de.Key, GridControl).MainView
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

#End Region

End Class

''' <summary>
''' Encapsula los argumentos del evento PasteToGrid
''' </summary>
Public Class PasteToGridEventArgs
    Inherits EventArgs

#Region "Fields"

    ''' <summary>
    ''' Lista de registros obtenidos de la Clipboard
    ''' </summary>
    Private _rows As List(Of List(Of String))

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene la lista de registros obtenidos de la Clipboard
    ''' </summary>
    ''' <returns>Lista de registros</returns>
    Public ReadOnly Property Rows As List(Of List(Of String))
        Get
            Return Me._rows
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="contentClipboard">Texto crudo contenido en la Clipboard</param>
    Public Sub New(ByVal contentClipboard As String)
        Me._rows = New List(Of List(Of String))()
        If contentClipboard IsNot Nothing AndAlso Not contentClipboard.Trim().Equals(String.Empty) Then
            Me.GetRows(contentClipboard.Trim())
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la lista de registros del contenido texto de la Clipboard
    ''' </summary>
    ''' <param name="contentClipboard">Contenido texto de la Clipboard</param>
    Private Sub GetRows(ByVal contentClipboard As String)
        For Each line As String In contentClipboard.Split(Microsoft.VisualBasic.Constants.vbNewLine)
            Dim item() As String = line.Trim.Split(Microsoft.VisualBasic.Constants.vbTab)
            If item.Length > 0 Then
                Me._rows.Add(New List(Of String)(item))
            End If
        Next
    End Sub

#End Region

End Class