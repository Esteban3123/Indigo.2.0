#Region "Imports"

Imports DevExpress.XtraMap
Imports System.Drawing
Imports System.ComponentModel
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.Data
Imports System.Reflection
Imports System.Linq
Imports DevExpress.Map
Imports System.Dynamic
Imports Microsoft.VisualBasic

#End Region

Public Class IndigoMapControl

#Region "Consts"

    ''' <summary>
    ''' Nombre del atributo interno
    ''' </summary>
    Private INNER_OBJECT_NAME As String = "__InnerObject__"

#End Region

#Region "Fields"

    ''' <summary>
    ''' Fuente de datos a pintar en el mapa
    ''' </summary>
    Private _datasource As Object
    ''' <summary>
    ''' Valor que indica si se muestra la capa de puntos indigo
    ''' </summary>
    Private _showCopyrightLayer As Boolean
    ''' <summary>
    ''' Valor que indica si se muestra la capa de predios
    ''' </summary>
    Private _showPremisesLayer As Boolean
    ''' <summary>
    ''' Nombre de la propiedad que contiene el valor de latitud
    ''' en los objetos del datasource
    ''' </summary>
    Private _latitudeFieldName As String
    ''' <summary>
    ''' Nombre de la propiedad que contiene el valor de longitud
    ''' en los objetos del datasource
    ''' </summary>
    Private _longitudeFieldName As String
    ''' <summary>
    ''' Capa de puntos indigo
    ''' </summary>
    Private _copyRightLayer As VectorItemsLayer
    ''' <summary>
    ''' Capa de predios
    ''' </summary>
    Private _premisesLayer As VectorItemsLayer
    ''' <summary>
    ''' Capa de seleccion para el área
    ''' </summary>
    Private _selectionLayer As VectorItemsLayer
    ''' <summary>
    ''' Lista de puntos para dibujar el mapa
    ''' </summary>
    Private _polygonCoordPoints As List(Of CoordPoint)
    ''' <summary>
    ''' Lista de puntos para dibujar el poligono interno
    ''' </summary>
    Private _polygonPoints As List(Of PointF)
    ''' <summary>
    ''' Valor que indica si el poligono ya se pinto
    ''' </summary>
    Private _polygonIsPainted As Boolean
    ''' <summary>
    ''' Lista de puntos a exportar
    ''' </summary>
    Private _listExportPoints As List(Of ExpandoObject)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna la fuente de datos a pintar en el mapa
    ''' </summary>
    ''' <value>Fuente de datos a pintar</value>
    ''' <returns>La fuente de datos pintada en el mapa</returns>
    <Browsable(False)>
    Public Property Datasource As Object
        Get
            Return Me._datasource
        End Get
        Set(value As Object)
            If IsValidDataSource(value) Then
                Me._datasource = value
                ReloadPremisesLayerFromDatasource()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si se muestra el panel de opciones
    ''' </summary>
    ''' <value>Valor que indica si se muestra el panel de opciones</value>
    ''' <returns>Valor que indica si se muestra el panel de opciones</returns>
    <Browsable(True), Description("Obtiene o asigna un valor que indica si se muestra el panel de opciones")>
    Public Property VisibleOptions As Boolean
        Get
            Return Me.PnlOptions.Visible
        End Get
        Set(value As Boolean)
            Me.PnlOptions.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nombre de la propiedad que tiene el valor
    ''' de la latitud en los objetos del datasource
    ''' </summary>
    ''' <value>Nombre de la propiedad</value>
    ''' <returns>El nombre de la propiedad</returns>
    <Browsable(True), Description("Obtiene o asigna el nombre de la propiedad que tiene el valor de la latitud en los objetos del datasource")>
    Public Property LatitudeFieldName As String
        Get
            Return Me._latitudeFieldName
        End Get
        Set(value As String)
            Me._latitudeFieldName = ValidateIsNothing(value)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nombre de la propiedad que tiene el valor
    ''' de la longitud en los objetos del datasource
    ''' </summary>
    ''' <value>Nombre de la propiedad</value>
    ''' <returns>El nombre de la propiedad</returns>
    <Browsable(True), Description("Obtiene o asigna el nombre de la propiedad que tiene el valor de la longitud en los objetos del datasource")>
    Public Property LongitudeFieldName As String
        Get
            Return Me._longitudeFieldName
        End Get
        Set(value As String)
            Me._longitudeFieldName = ValidateIsNothing(value)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el patrón usado para mostrar el titulo de cada predio
    ''' </summary>
    ''' <value>Patrón a usar en el titulo</value>
    ''' <returns>Patrón usado en el titulo</returns>
    <Browsable(True), Description("Obtiene o asigna el patrón usado para mostrar el titulo de cada predio")>
    Public Property PremiseTitlePattern As String
        Get
            Return Me._premisesLayer.ShapeTitlesPattern
        End Get
        Set(value As String)
            Me._premisesLayer.ShapeTitlesPattern = ValidateIsNothing(value)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el patrón usado para mostrar el text de cada predio
    ''' </summary>
    ''' <value>Patrón a usar en el text</value>
    ''' <returns>Patrón usado en el text</returns>
    <Browsable(True), Description("Obtiene o asigna el patrón usado para mostrar el text de cada predio")>
    Public Property PremiseTextPattern As String
        Get
            Return Me._premisesLayer.ToolTipPattern
        End Get
        Set(value As String)
            Me._premisesLayer.ToolTipPattern = ValidateIsNothing(value)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si se muestra la capa de puntos indigo
    ''' </summary>
    ''' <value>Valor que indica si se muestra la capa de puntos indigo</value>
    ''' <returns>Un valor que indica si se muestra la capa de puntos indigo</returns>
    <Browsable(True), Description("Obtiene o asigna un valor que indica si se muestra la capa de puntos indigo")>
    Public Property ShowCopyrightLayer As Boolean
        Get
            Return Me._showCopyrightLayer
        End Get
        Set(value As Boolean)
            Me._showCopyrightLayer = value
            Me._copyRightLayer.Visible = Me._showCopyrightLayer
            Me.ChkShowCopyright.Checked = Me._showCopyrightLayer
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si se muestra la capa de predios
    ''' </summary>
    ''' <value>Valor que indica si se muestra la capa de predios</value>
    ''' <returns>Un valor que indica si se muestra la capa de predios</returns>
    <Browsable(True), Description("Obtiene o asigna un valor que indica si se muestra la capa de predios")>
    Public Property ShowPremisesLayer As Boolean
        Get
            Return Me._showPremisesLayer
        End Get
        Set(value As Boolean)
            Me._showPremisesLayer = value
            Me._premisesLayer.Visible = Me._showPremisesLayer
            Me.ChkShowPremises.Checked = Me._showPremisesLayer
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Me._datasource = Nothing
        Me._latitudeFieldName = String.Empty
        Me._longitudeFieldName = String.Empty
        Me._showCopyrightLayer = True
        Me._showPremisesLayer = False
        Me._polygonIsPainted = False
        Me._polygonCoordPoints = New List(Of CoordPoint)()
        Me._polygonPoints = New List(Of PointF)()
        Me._listExportPoints = New List(Of ExpandoObject)()
        Me._selectionLayer = New VectorItemsLayer With {.Visible = False, .Data = New MapItemStorage()}
        Me._premisesLayer = New VectorItemsLayer With {.Name = "PremisesLayer", .Visible = Me._showPremisesLayer, .ShapeTitlesVisibility = VisibilityMode.Visible, .ShapeTitlesPattern = String.Empty, .ToolTipPattern = String.Empty}
        Me._copyRightLayer = New VectorItemsLayer With {.Name = "CopyRightLayer", .Visible = Me._showCopyrightLayer}
        Me.MapControl.Layers.Add(Me._selectionLayer)
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Evento que se lanza cada ve que ocurre un error interno en el control
    ''' </summary>
    ''' <param name="sender">Objeto quien generó el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event ErrorEventLog(ByVal sender As Object, ByVal e As ErrorEventArgs)
    ''' <summary>
    ''' Evento que se lanza cuando se exporta un conjunto de puntos resultado
    ''' </summary>
    ''' <param name="sender">Objeto que lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event ExportResult(ByVal sender As Object, ByVal e As ExportResultEventArgs)

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista los puntos dentro de un poligono
    ''' </summary>
    ''' <param name="listPoints">Poligono a consultar</param>
    ''' <param name="listSource">Puntos a consultar</param>
    ''' <returns>Lista de objetos dinamicos resultado</returns>
    Private Function ListPointsInPolygon(ByVal listPoints As List(Of PointF), ByVal listSource As List(Of MapItem)) As List(Of ExpandoObject)
        Dim list As New List(Of ExpandoObject)()

        If listPoints IsNot Nothing AndAlso listSource IsNot Nothing Then
            Dim polygon As New Polygon(listPoints.ToArray())
            For Each mp As MapDot In listSource
                Dim p As PointF = Me.MapControl.CoordPointToScreenPoint(mp.Location).ToPointF()
                If polygon.PointInPolygon(p.X, p.Y) Then
                    Dim e As New ExpandoObject()
                    For Each a As MapItemAttribute In mp.Attributes.Where(Function(f) Not f.Name.Equals(INNER_OBJECT_NAME)).ToList()
                        CType(e, IDictionary(Of String, Object)).Add(a.Name, a.Value)
                    Next
                    list.Add(e)
                End If
            Next
        End If

        Return list
    End Function

    ''' <summary>
    ''' Crea una lista fila para la rejilla de información a partir de un objeto fuente
    ''' </summary>
    ''' <param name="obj">Objeto fuente</param>
    ''' <returns>Lista de filas</returns>
    Private Function CreateRowToGridFromObject(ByVal obj As Object) As List(Of DevExpress.XtraVerticalGrid.Rows.BaseRow)
        Dim listResult As New List(Of DevExpress.XtraVerticalGrid.Rows.BaseRow)()
        If obj IsNot Nothing Then
            For Each p As PropertyInfo In obj.GetType().GetProperties()
                Dim rowAux As New DevExpress.XtraVerticalGrid.Rows.EditorRow()

                rowAux.Name = "Row" & p.Name
                rowAux.OptionsRow.AllowMove = False
                rowAux.OptionsRow.AllowMoveToCustomizationForm = False
                rowAux.OptionsRow.AllowSize = False
                rowAux.OptionsRow.ShowInCustomizationForm = False
                rowAux.Properties.Caption = p.Name & ":"
                rowAux.Properties.FieldName = p.Name
                listResult.Add(rowAux)
            Next
        End If
        Return listResult
    End Function

    ''' <summary>
    ''' Recarga la capa de predios a partir de la fuente de datos
    ''' </summary>
    Private Sub ReloadPremisesLayerFromDatasource()
        If Me._datasource IsNot Nothing AndAlso IsValidProperties() Then
            Me._premisesLayer.Visible = False
            Me._premisesLayer.Data = Nothing
            Me.GdcSelectedPointInfo.Rows.Clear()
            Dim countPoints As Integer = 0
            Dim isFormatGridInfo As Boolean = False

            Dim itemStorage As New MapItemStorage()

            For Each o In If(TypeOf Me._datasource Is IList, CType(Me._datasource, IList), If(TypeOf Me._datasource Is IListSource, CType(Me._datasource, IListSource).GetList(), CType(Me._datasource, ICollection)))
                If o Is Nothing Then
                    Continue For
                End If
                If Not isFormatGridInfo Then
                    Me.GdcSelectedPointInfo.Rows.AddRange(CreateRowToGridFromObject(o).ToArray())
                    isFormatGridInfo = True
                End If
                itemStorage.Items.Add(CreateMapDot(o))
                countPoints += 1
            Next

            Me.LblCountPoints.Text = countPoints.ToString()
            Me._premisesLayer.Data = itemStorage
            Me._premisesLayer.Visible = Me._showPremisesLayer
        End If
    End Sub

    ''' <summary>
    ''' Crea un punto en el mapa a partir de un objeto fuente
    ''' </summary>
    ''' <param name="obj">Objeto fuente</param>
    ''' <returns>Punto en el mapa</returns>
    Private Function CreateMapDot(ByVal obj As Object) As MapDot
        Dim dot As New MapDot()
        Dim latitude As Double = Double.Parse(obj.GetType().GetProperty(Me._latitudeFieldName).GetValue(obj))
        Dim longitud As Double = Double.Parse(obj.GetType().GetProperty(Me._longitudeFieldName).GetValue(obj))

        dot.Fill = Color.Coral
        dot.HighlightedFill = Color.Brown
        dot.Stroke = Color.Black
        dot.StrokeWidth = 1
        dot.Location = New GeoPoint(latitude, longitud)
        dot.Size = 13
        dot.ShapeKind = MapDotShapeKind.Circle

        dot.Attributes.Add(New MapItemAttribute With {.Name = INNER_OBJECT_NAME, .Type = obj.GetType(), .Value = obj})
        For Each p As PropertyInfo In obj.GetType().GetProperties() '.Where(Function(d) Not d.Name.Equals(Me._latitudeFieldName) And Not d.Name.Equals(Me._longitudeFieldName)).ToList()
            dot.Attributes.Add(New MapItemAttribute With {.Name = p.Name, .Type = p.PropertyType, .Value = p.GetValue(obj)})
        Next

        Return dot
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si las propiedades del control
    ''' se encuentran bien configuradas para realizar la carga desde la fuente de datos
    ''' </summary>
    ''' <returns>Valor que indica si las propiedades son validas</returns>
    Private Function IsValidProperties() As Boolean
        If Me._latitudeFieldName.Trim().Equals(String.Empty) Then
            OnErrorEvent(Me, New ErrorEventArgs("No se ha especificado el nombre de la propiedad que contiene el valor de la LATITUD en la fuente de datos"))
            Return False
        End If
        If Me._longitudeFieldName.Trim().Equals(String.Empty) Then
            OnErrorEvent(Me, New ErrorEventArgs("No se ha especificado el nombre de la propiedad que contiene el valor de la LONGITUD en la fuente de datos"))
            Return False
        End If

        If Me._datasource IsNot Nothing Then
            If (TypeOf Me._datasource Is IList AndAlso CType(Me._datasource, IList).Count > 0) OrElse (TypeOf Me._datasource Is IListSource AndAlso CType(Me._datasource, IListSource).GetList().Count > 0) OrElse (TypeOf Me._datasource Is ICollection AndAlso CType(Me._datasource, ICollection).Count > 0) Then
                Dim tSource As Type = Nothing
                tSource = If(TypeOf Me._datasource Is IList, CType(Me._datasource, IList)(0).GetType(), If(TypeOf Me._datasource Is IListSource, CType(Me._datasource, IListSource).GetList()(0).GetType(), CType(Me._datasource, ICollection)(0).GetType()))

                If tSource.GetProperty(Me._latitudeFieldName) Is Nothing Then
                    OnErrorEvent(Me, New ErrorEventArgs("La propiedad especificada que contiene el valor de la LATITUD no existe en el tipo de dato especificado como plantilla para la fuente de datos"))
                    Return False
                End If
                If Not tSource.GetProperty(Me._latitudeFieldName).PropertyType.Equals(GetType(Double)) AndAlso Not tSource.GetProperty(Me._latitudeFieldName).PropertyType.Equals(GetType(Decimal)) Then
                    OnErrorEvent(Me, New ErrorEventArgs("El tipo de dato de la propiedad especificada que contiene el valor de la LATITUD debe ser Double"))
                    Return False
                End If

                If tSource.GetProperty(Me._longitudeFieldName) Is Nothing Then
                    OnErrorEvent(Me, New ErrorEventArgs("La propiedad especificada que contiene el valor de la LONGITUD no existe en el tipo de dato especificado como plantilla para la fuente de datos"))
                    Return False
                End If
                If Not tSource.GetProperty(Me._longitudeFieldName).PropertyType.Equals(GetType(Double)) AndAlso Not tSource.GetProperty(Me._longitudeFieldName).PropertyType.Equals(GetType(Decimal)) Then
                    OnErrorEvent(Me, New ErrorEventArgs("El tipo de dato de la propiedad especificada que contiene el valor de la LONGITUD debe ser Double"))
                    Return False
                End If
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' Valida si la fuente de datos es valida
    ''' </summary>
    ''' <param name="dataSource">Fuente de datos</param>
    ''' <returns>Valor que indica si la fuente de datos es valida</returns>
    Protected Function IsValidDataSource(ByVal dataSource As Object) As Boolean
        If (dataSource Is Nothing) Then
            Return True
        End If
        If TypeOf dataSource Is IList Then
            Return True
        End If
        If TypeOf dataSource Is IListSource Then
            Return True
        End If
        If TypeOf dataSource Is IEnumerable Then
            Return True
        End If
        If TypeOf dataSource Is ICollection Then
            Return True
        End If
        Return (Not IsServerModeType(dataSource))
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si la fuente de datos es de tipo servidor
    ''' </summary>
    ''' <param name="dataSource">Fuente de datos</param>
    ''' <returns>Valor que indica si es de tipo servidor</returns>
    Private Function IsServerModeType(ByVal dataSource As Object) As Boolean
        If TypeOf dataSource Is IListSource Then
            dataSource = DirectCast(dataSource, IListSource).GetList
        End If
        If TypeOf dataSource Is AsyncListServer2DatacontrollerProxy Then
            Return True
        End If
        If TypeOf dataSource Is IListServer Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Valida si el valor de texto es nulo y lo convierte en vacio
    ''' </summary>
    ''' <param name="value">Valor de texto a validar</param>
    ''' <returns>Valor validado</returns>
    Private Function ValidateIsNothing(ByVal value As String) As String
        If value Is Nothing Then
            Return String.Empty
        End If
        Return value
    End Function

    ''' <summary>
    ''' Pone el texto y la imagen de indigo en el mapa
    ''' </summary>
    Private Sub InitializeCopyright()
        Dim indigoElement As New MapCustomElement()
        indigoElement.Image = New Bitmap(ImcLogo.Images(0), 48, 48)
        indigoElement.Location = New GeoPoint(2.93796, -75.29224)
        indigoElement.ToolTipPattern = "Vie HealtTech Office" & ControlChars.CrLf & "Kr 5A N 22 31"
        Dim itemStorage As New MapItemStorage()
        itemStorage.Items.Add(indigoElement)
        Me._copyRightLayer.Data = itemStorage
        Me.MapControl.Layers.Add(Me._copyRightLayer)
    End Sub

    ''' <summary>
    ''' Inicializa las propiedades por defecto del mapa
    ''' </summary>
    Public Sub InitializeMap()
        InitializeMap(New OpenStreetMapDataProvider(), New GeoPoint(2.93806R, -75.29179R), 18.5)
    End Sub

    ''' <summary>
    ''' Inicializa las propiedades del mapa
    ''' </summary>
    ''' <param name="dataProvider">Proveedor de imagenes del mapa</param>
    ''' <param name="centerPoint">Punto inicial del mapa</param>
    ''' <param name="zoomElevation">Elevación inicial del mapa</param>
    ''' <remarks></remarks>
    Public Sub InitializeMap(ByVal dataProvider As MapDataProviderBase, ByVal centerPoint As GeoPoint, ByVal zoomElevation As Double)
        Me.MapControl.Layers.Add(New ImageTilesLayer With {.DataProvider = dataProvider})
        Me.MapControl.CenterPoint = centerPoint
        Me.MapControl.ZoomLevel = zoomElevation
        Me.MapControl.ToolTipController = New DevExpress.Utils.ToolTipController With {.AllowHtmlText = True, .ToolTipStyle = DevExpress.Utils.ToolTipStyle.Windows7, .ToolTipType = DevExpress.Utils.ToolTipType.SuperTip, .ToolTipLocation = DevExpress.Utils.ToolTipLocation.BottomCenter}
        Me.MapControl.Layers.Add(Me._premisesLayer)
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se carga los valores por defecto del control
    ''' </summary>
    Private Sub IndigoMapControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeCopyright()
        Me.ChkShowCopyright.Checked = Me._showCopyrightLayer
        Me.ChkShowPremises.Checked = Me._showPremisesLayer
    End Sub

    ''' <summary>
    ''' Aqui se controla el dibujo del poligono
    ''' </summary>
    Private Sub MapControl_MouseClick(sender As Object, e As MouseEventArgs) Handles MapControl.MouseClick
        If Me.ChkSelectPoints.Checked AndAlso Not Me._polygonIsPainted Then
            If e.Button = System.Windows.Forms.MouseButtons.Left Then
                Me._polygonPoints.Add(New PointF(e.X, e.Y))
                Me._polygonCoordPoints.Add(Me.MapControl.ScreenPointToCoordPoint(New MapPoint(e.X, e.Y)))
                'Ocultamos y limpiamos la capa de seleccion
                Me._selectionLayer.Visible = False
                CType(Me._selectionLayer.Data, MapItemStorage).Items.Clear()

                If Me._polygonCoordPoints.Count = 1 Then 'Dibujamos un punto
                    Dim p As New MapDot With {.ShapeKind = MapDotShapeKind.Rectangle, .Stroke = Color.Black, .StrokeWidth = 1, .Fill = Color.Black, .Location = Me._polygonCoordPoints(0), .Visible = True}
                    CType(Me._selectionLayer.Data, MapItemStorage).Items.Add(p)
                End If
                If Me._polygonCoordPoints.Count > 1 Then 'Dibujamos lineas
                    Dim collectionPoints As New CoordPointCollection()
                    collectionPoints.AddRange(Me._polygonCoordPoints)
                    Dim lines As New MapPolyline With {.Stroke = Color.Black, .StrokeWidth = 1, .Fill = Color.Black, .Points = collectionPoints, .Visible = True}
                    CType(Me._selectionLayer.Data, MapItemStorage).Items.Add(lines)
                End If

                Me._selectionLayer.Visible = True
            End If

            If e.Button = System.Windows.Forms.MouseButtons.Right Then
                If Me._polygonCoordPoints.Count > 2 Then 'Dibujamos el poligono
                    'Ocultamos y limpiamos la capa de seleccion
                    Me._selectionLayer.Visible = False
                    CType(Me._selectionLayer.Data, MapItemStorage).Items.Clear()

                    Dim collectionPoints As New CoordPointCollection()
                    collectionPoints.AddRange(Me._polygonCoordPoints)
                    Dim poly As New MapPolygon With {.Stroke = Color.Red, .StrokeWidth = 1, .Fill = Color.FromArgb(120, Color.Red), .Points = collectionPoints, .Visible = True}

                    CType(Me._selectionLayer.Data, MapItemStorage).Items.Add(poly)
                    Me._selectionLayer.Visible = True

                    Me._polygonIsPainted = True

                    'Aqui obtenemos los puntos dentro del poligono
                    Me._listExportPoints = ListPointsInPolygon(Me._polygonPoints, CType(Me._premisesLayer.Data, MapItemStorage).Items.ToList())
                    Me.LyciExportSelectedPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se controla si se inicia la herramienta para seleccionar un área en el mapa
    ''' </summary>
    Private Sub ChkSelectPoints_CheckedChanged(sender As Object, e As EventArgs) Handles ChkSelectPoints.CheckedChanged
        Me.MapControl.EnableScrolling = Not Me.ChkSelectPoints.Checked
        Me.MapControl.EnableZooming = Not Me.ChkSelectPoints.Checked
        Me._selectionLayer.Visible = Me.ChkSelectPoints.Checked
        If Not Me.ChkSelectPoints.Checked Then
            'Aqui borramos el poligono
            Me._polygonPoints.Clear()
            Me._polygonCoordPoints.Clear()
            Me._listExportPoints.Clear()
            CType(Me._selectionLayer.Data, MapItemStorage).Items.Clear()
            Me._polygonIsPainted = False
            Me.LyciExportSelectedPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Aqui se controla el cambio en la propiedad ShowCopyrightLayer
    ''' </summary>
    Private Sub ChkShowCopyright_CheckedChanged(sender As Object, e As EventArgs) Handles ChkShowCopyright.CheckedChanged
        Me._showCopyrightLayer = Me.ChkShowCopyright.Checked
        Me._copyRightLayer.Visible = Me._showCopyrightLayer
    End Sub

    ''' <summary>
    ''' Aqui se controla el cambio en la propiedad ShowPremisesLayer
    ''' </summary>
    Private Sub ChkShowPremises_CheckedChanged(sender As Object, e As EventArgs) Handles ChkShowPremises.CheckedChanged
        Me._showPremisesLayer = Me.ChkShowPremises.Checked
        Me._premisesLayer.Visible = Me._showPremisesLayer
    End Sub

    ''' <summary>
    ''' Aqui controlamos el tamaño minimo del control
    ''' </summary>
    Private Sub IndigoMapControl_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Me.MinimumSize = New Size(439, 629)
    End Sub

    ''' <summary>
    ''' Se lanza cuando ocurre un error interno en el control
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Overridable Sub OnErrorEvent(ByVal sender As Object, ByVal e As ErrorEventArgs)
        RaiseEvent ErrorEventLog(sender, e)
    End Sub

    ''' <summary>
    ''' Evento que se lanza cuando se exporta un conjunto de puntos resultado
    ''' </summary>
    ''' <param name="sender">Objeto que lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Overridable Sub OnExportResult(ByVal sender As Object, ByVal e As ExportResultEventArgs)
        RaiseEvent ExportResult(sender, e)
    End Sub

    ''' <summary>
    ''' Aqui se muestra la información del punto seleccionado
    ''' </summary>
    Private Sub MapControl_SelectionChanged(sender As Object, e As MapSelectionChangedEventArgs) Handles MapControl.SelectionChanged
        If e.Selection IsNot Nothing AndAlso e.Selection.Count > 0 AndAlso TypeOf e.Selection(0) Is MapDot AndAlso CType(e.Selection(0), MapDot).Attributes.Count > 0 Then
            Dim list As New List(Of Object)(New Object() {CType(e.Selection(0), MapDot).Attributes(INNER_OBJECT_NAME).Value})
            Me.GdcSelectedPointInfo.DataSource = list
            Me.LyciSelectedPointInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            Me.GdcSelectedPointInfo.DataSource = Nothing
            Me.LyciSelectedPointInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        e.Selection.Clear()
    End Sub

    ''' <summary>
    ''' Aqui se dispara el evento que exporta los puntos seleccionados
    ''' </summary>
    Private Sub BtnExportSelectedPoints_HyperlinkClick(sender As Object, e As DevExpress.Utils.HyperlinkClickEventArgs) Handles BtnExportSelectedPoints.HyperlinkClick
        RaiseEvent ExportResult(Me, New ExportResultEventArgs(Me._listExportPoints))
    End Sub

#End Region

End Class

''' <summary>
''' Encapsula los argumentos del evento Error
''' </summary>
Public Class ErrorEventArgs
    Inherits EventArgs

#Region "Members"

    Private _message As String
    ''' <summary>
    ''' Obtiene el mensaje del error
    ''' </summary>
    ''' <returns>Mensaje del error</returns>
    Public ReadOnly Property Message As String
        Get
            Return Me._message
        End Get
    End Property

    Private _innerException As Exception
    ''' <summary>
    ''' Obtiene la excepción que generó el error
    ''' </summary>
    ''' <returns>Excepción que generó el error</returns>
    Public ReadOnly Property InnerException As Exception
        Get
            Return Me._innerException
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="message">Mensaje del error</param>
    ''' <param name="innerException">Excepción que generó el error</param>
    Public Sub New(ByVal message As String, Optional ByVal innerException As Exception = Nothing)
        Me._message = message
        Me._innerException = innerException
    End Sub

#End Region

End Class

''' <summary>
''' Encapsula los argumentos del evento ExportResult
''' </summary>
Public Class ExportResultEventArgs
    Inherits EventArgs

#Region "Members"

    Private _result As List(Of ExpandoObject)
    ''' <summary>
    ''' Obtiene el resultado exportado
    ''' </summary>
    ''' <returns>Resultado exportado</returns>
    Public ReadOnly Property Result As List(Of ExpandoObject)
        Get
            Return Me._result
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="result">Resultado exportado</param>
    Public Sub New(ByVal result As List(Of ExpandoObject))
        Me._result = result
    End Sub

#End Region

End Class

''' <summary>
''' Encapsula los metodos para tratar poligonos
''' </summary>
Public Class Polygon

#Region "Methods"

    Public Sub New()
    End Sub
    Public Sub New(points__1 As PointF())
        Points = points__1
    End Sub

    Public Points As PointF()

    ' Find the polygon's centroid.
    Public Function FindCentroid() As PointF
        ' Add the first point at the end of the array.
        Dim num_points As Integer = Points.Length
        Dim pts As PointF() = New PointF(num_points) {}
        Points.CopyTo(pts, 0)
        pts(num_points) = Points(0)

        ' Find the centroid.
        Dim X As Single = 0
        Dim Y As Single = 0
        Dim second_factor As Single
        For i As Integer = 0 To num_points - 1
            second_factor = pts(i).X * pts(i + 1).Y - pts(i + 1).X * pts(i).Y
            X += (pts(i).X + pts(i + 1).X) * second_factor
            Y += (pts(i).Y + pts(i + 1).Y) * second_factor
        Next

        ' Divide by 6 times the polygon's area.
        Dim polygon_area As Single = PolygonArea()
        X /= (6 * polygon_area)
        Y /= (6 * polygon_area)

        ' If the values are negative, the polygon is
        ' oriented counterclockwise so reverse the signs.
        If X < 0 Then
            X = -X
            Y = -Y
        End If

        Return New PointF(X, Y)
    End Function

    ' Return true if the point is in the polygon.
    Public Function PointInPolygon(X As Single, Y As Single) As Boolean
        ' Get the angle between the point and the
        ' first and last vertices.
        Dim max_point As Integer = Points.Length - 1
        Dim total_angle As Single = GetAngle(Points(max_point).X, Points(max_point).Y, X, Y, Points(0).X, Points(0).Y)

        ' Add the angles from the point
        ' to each other pair of vertices.
        For i As Integer = 0 To max_point - 1
            total_angle += GetAngle(Points(i).X, Points(i).Y, X, Y, Points(i + 1).X, Points(i + 1).Y)
        Next

        ' The total angle should be 2 * PI or -2 * PI if
        ' the point is in the polygon and close to zero
        ' if the point is outside the polygon.
        Return (Math.Abs(total_angle) > 0.000001)
    End Function

#Region "Orientation Routines"
    ' Return true if the polygon is oriented clockwise.
    Public Function PolygonIsOrientedClockwise() As Boolean
        Return (SignedPolygonArea() < 0)
    End Function

    ' If the polygon is oriented counterclockwise,
    ' reverse the order of its points.
    Private Sub OrientPolygonClockwise()
        If Not PolygonIsOrientedClockwise() Then
            Array.Reverse(Points)
        End If
    End Sub
#End Region

#Region "Area Routines"
    ' Return the polygon's area in "square units."
    ' Add the areas of the trapezoids defined by the
    ' polygon's edges dropped to the X-axis. When the
    ' program considers a bottom edge of a polygon, the
    ' calculation gives a negative area so the space
    ' between the polygon and the axis is subtracted,
    ' leaving the polygon's area. This method gives odd
    ' results for non-simple polygons.
    Public Function PolygonArea() As Single
        ' Return the absolute value of the signed area.
        ' The signed area is negative if the polygon is
        ' oriented clockwise.
        Return Math.Abs(SignedPolygonArea())
    End Function

    ' Return the polygon's area in "square units."
    ' Add the areas of the trapezoids defined by the
    ' polygon's edges dropped to the X-axis. When the
    ' program considers a bottom edge of a polygon, the
    ' calculation gives a negative area so the space
    ' between the polygon and the axis is subtracted,
    ' leaving the polygon's area. This method gives odd
    ' results for non-simple polygons.
    '
    ' The value will be negative if the polygon is
    ' oriented clockwise.
    Private Function SignedPolygonArea() As Single
        ' Add the first point to the end.
        Dim num_points As Integer = Points.Length
        Dim pts As PointF() = New PointF(num_points) {}
        Points.CopyTo(pts, 0)
        pts(num_points) = Points(0)

        ' Get the areas.
        Dim area As Single = 0
        For i As Integer = 0 To num_points - 1
            area += (pts(i + 1).X - pts(i).X) * (pts(i + 1).Y + pts(i).Y) / 2
        Next

        ' Return the result.
        Return area
    End Function
#End Region

    ' Return true if the polygon is convex.
    Public Function PolygonIsConvex() As Boolean
        ' For each set of three adjacent points A, B, C,
        ' find the dot product AB · BC. If the sign of
        ' all the dot products is the same, the angles
        ' are all positive or negative (depending on the
        ' order in which we visit them) so the polygon
        ' is convex.
        Dim got_negative As Boolean = False
        Dim got_positive As Boolean = False
        Dim num_points As Integer = Points.Length
        Dim B As Integer, C As Integer
        For A As Integer = 0 To num_points - 1
            B = (A + 1) Mod num_points
            C = (B + 1) Mod num_points

            Dim cross_product As Single = CrossProductLength(Points(A).X, Points(A).Y, Points(B).X, Points(B).Y, Points(C).X, Points(C).Y)
            If cross_product < 0 Then
                got_negative = True
            ElseIf cross_product > 0 Then
                got_positive = True
            End If
            If got_negative AndAlso got_positive Then
                Return False
            End If
        Next

        ' If we got this far, the polygon is convex.
        Return True
    End Function

#Region "Cross and Dot Products"
    ' Return the cross product AB x BC.
    ' The cross product is a vector perpendicular to AB
    ' and BC having length |AB| * |BC| * Sin(theta) and
    ' with direction given by the right-hand rule.
    ' For two vectors in the X-Y plane, the result is a
    ' vector with X and Y components 0 so the Z component
    ' gives the vector's length and direction.
    Public Shared Function CrossProductLength(Ax As Single, Ay As Single, Bx As Single, By As Single, Cx As Single, Cy As Single) As Single
        ' Get the vectors' coordinates.
        Dim BAx As Single = Ax - Bx
        Dim BAy As Single = Ay - By
        Dim BCx As Single = Cx - Bx
        Dim BCy As Single = Cy - By

        ' Calculate the Z coordinate of the cross product.
        Return (BAx * BCy - BAy * BCx)
    End Function

    ' Return the dot product AB · BC.
    ' Note that AB · BC = |AB| * |BC| * Cos(theta).
    Private Shared Function DotProduct(Ax As Single, Ay As Single, Bx As Single, By As Single, Cx As Single, Cy As Single) As Single
        ' Get the vectors' coordinates.
        Dim BAx As Single = Ax - Bx
        Dim BAy As Single = Ay - By
        Dim BCx As Single = Cx - Bx
        Dim BCy As Single = Cy - By

        ' Calculate the dot product.
        Return (BAx * BCx + BAy * BCy)
    End Function
#End Region

    ' Return the angle ABC.
    ' Return a value between PI and -PI.
    ' Note that the value is the opposite of what you might
    ' expect because Y coordinates increase downward.
    Public Shared Function GetAngle(Ax As Single, Ay As Single, Bx As Single, By As Single, Cx As Single, Cy As Single) As Single
        ' Get the dot product.
        Dim dot_product As Single = DotProduct(Ax, Ay, Bx, By, Cx, Cy)

        ' Get the cross product.
        Dim cross_product As Single = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy)

        ' Calculate the angle.
        Return CSng(Math.Atan2(cross_product, dot_product))
    End Function

#Region "Bounding Rectangle"
    Private NumPoints As Integer = 0

    ' The points that have been used in test edges.
    Private m_EdgeChecked As Boolean()

    ' The four caliper control points. They start:
    '       m_ControlPoints(0)      Left edge       xmin
    '       m_ControlPoints(1)      Bottom edge     ymax
    '       m_ControlPoints(2)      Right edge      xmax
    '       m_ControlPoints(3)      Top edge        ymin
    Private ControlPoints As Integer() = New Integer(3) {}

    ' The line from this point to the next one forms
    ' one side of the next bounding rectangle.
    Private m_CurrentControlPoint As Integer = -1

    ' The area of the current and best bounding rectangles.
    Private CurrentArea As Single = Single.MaxValue
    Private CurrentRectangle As PointF() = Nothing
    Private BestArea As Single = Single.MaxValue
    Private BestRectangle As PointF() = Nothing

    ' Get ready to start.
    Private Sub ResetBoundingRect()
        NumPoints = Points.Length

        ' Find the initial control points.
        FindInitialControlPoints()

        ' So far we have not checked any edges.
        m_EdgeChecked = New Boolean(NumPoints - 1) {}

        ' Start with this bounding rectangle.
        m_CurrentControlPoint = 1
        BestArea = Single.MaxValue

        ' Find the initial bounding rectangle.
        FindBoundingRectangle()

        ' Remember that we have checked this edge.
        m_EdgeChecked(ControlPoints(m_CurrentControlPoint)) = True
    End Sub

    ' Find the initial control points.
    Private Sub FindInitialControlPoints()
        For i As Integer = 0 To NumPoints - 1
            If CheckInitialControlPoints(i) Then
                Return
            End If
        Next
        Debug.Assert(False, "Could not find initial control points.")
    End Sub

    ' See if we can use segment i --> i + 1 as the base for the initial control points.
    Private Function CheckInitialControlPoints(i As Integer) As Boolean
        ' Get the i -> i + 1 unit vector.
        Dim i1 As Integer = (i + 1) Mod NumPoints
        Dim vix As Single = Points(i1).X - Points(i).X
        Dim viy As Single = Points(i1).Y - Points(i).Y

        ' The candidate control point indexes.
        For num As Integer = 0 To 3
            ControlPoints(num) = i
        Next

        ' Check backward from i until we find a vector
        ' j -> j+1 that points opposite to i -> i+1.
        For num As Integer = 1 To NumPoints - 1
            ' Get the new edge vector.
            Dim j As Integer = (i - num + NumPoints) Mod NumPoints
            Dim j1 As Integer = (j + 1) Mod NumPoints
            Dim vjx As Single = Points(j1).X - Points(j).X
            Dim vjy As Single = Points(j1).Y - Points(j).Y

            ' Project vj along vi. The length is vj dot vi.
            Dim dot_product As Single = vix * vjx + viy * vjy

            ' If the dot product < 0, then j1 is
            ' the index of the candidate control point.
            If dot_product < 0 Then
                ControlPoints(0) = j1
                Exit For
            End If
        Next

        ' If j == i, then i is not a suitable control point.
        If ControlPoints(0) = i Then
            Return False
        End If

        ' Check forward from i until we find a vector
        ' j -> j+1 that points opposite to i -> i+1.
        For num As Integer = 1 To NumPoints - 1
            ' Get the new edge vector.
            Dim j As Integer = (i + num) Mod NumPoints
            Dim j1 As Integer = (j + 1) Mod NumPoints
            Dim vjx As Single = Points(j1).X - Points(j).X
            Dim vjy As Single = Points(j1).Y - Points(j).Y

            ' Project vj along vi. The length is vj dot vi.
            Dim dot_product As Single = vix * vjx + viy * vjy

            ' If the dot product <= 0, then j is
            ' the index of the candidate control point.
            If dot_product <= 0 Then
                ControlPoints(2) = j
                Exit For
            End If
        Next

        ' If j == i, then i is not a suitable control point.
        If ControlPoints(2) = i Then
            Return False
        End If

        ' Check forward from m_ControlPoints[2] until
        ' we find a vector j -> j+1 that points opposite to
        ' m_ControlPoints[2] -> m_ControlPoints[2]+1.

        i = ControlPoints(2) - 1
        '@
        Dim temp As Single = vix
        vix = viy
        viy = -temp

        For num As Integer = 1 To NumPoints - 1
            ' Get the new edge vector.
            Dim j As Integer = (i + num) Mod NumPoints
            Dim j1 As Integer = (j + 1) Mod NumPoints
            Dim vjx As Single = Points(j1).X - Points(j).X
            Dim vjy As Single = Points(j1).Y - Points(j).Y

            ' Project vj along vi. The length is vj dot vi.
            Dim dot_product As Single = vix * vjx + viy * vjy

            ' If the dot product <=, then j is
            ' the index of the candidate control point.
            If dot_product <= 0 Then
                ControlPoints(3) = j
                Exit For
            End If
        Next

        ' If j == i, then i is not a suitable control point.
        If ControlPoints(0) = i Then
            Return False
        End If

        ' These control points work.
        Return True
    End Function

    ' Find the next bounding rectangle and check it.
    Private Sub CheckNextRectangle()
        ' Increment the current control point.
        ' This means we are done with using this edge.
        If m_CurrentControlPoint >= 0 Then
            ControlPoints(m_CurrentControlPoint) = (ControlPoints(m_CurrentControlPoint) + 1) Mod NumPoints
        End If

        ' Find the next point on an edge to use.
        Dim dx0 As Single, dy0 As Single, dx1 As Single, dy1 As Single, dx2 As Single, dy2 As Single, _
            dx3 As Single, dy3 As Single
        FindDxDy(dx0, dy0, ControlPoints(0))
        FindDxDy(dx1, dy1, ControlPoints(1))
        FindDxDy(dx2, dy2, ControlPoints(2))
        FindDxDy(dx3, dy3, ControlPoints(3))

        ' Switch so we can look for the smallest opposite/adjacent ratio.
        Dim opp0 As Single = dx0
        Dim adj0 As Single = dy0
        Dim opp1 As Single = -dy1
        Dim adj1 As Single = dx1
        Dim opp2 As Single = -dx2
        Dim adj2 As Single = -dy2
        Dim opp3 As Single = dy3
        Dim adj3 As Single = -dx3

        ' Assume the first control point is the best point to use next.
        Dim bestopp As Single = opp0
        Dim bestadj As Single = adj0
        Dim best_control_point As Integer = 0

        ' See if the other control points are better.
        If opp1 * bestadj < bestopp * adj1 Then
            bestopp = opp1
            bestadj = adj1
            best_control_point = 1
        End If
        If opp2 * bestadj < bestopp * adj2 Then
            bestopp = opp2
            bestadj = adj2
            best_control_point = 2
        End If
        If opp3 * bestadj < bestopp * adj3 Then
            bestopp = opp3
            bestadj = adj3
            best_control_point = 3
        End If

        ' Use the new best control point.
        m_CurrentControlPoint = best_control_point

        ' Remember that we have checked this edge.
        m_EdgeChecked(ControlPoints(m_CurrentControlPoint)) = True

        ' Find the current bounding rectangle
        ' and see if it is an improvement.
        FindBoundingRectangle()
    End Sub

    ' Find the current bounding rectangle and
    ' see if it is better than the previous best.
    Private Sub FindBoundingRectangle()
        ' See which point has the current edge.
        Dim i1 As Integer = ControlPoints(m_CurrentControlPoint)
        Dim i2 As Integer = (i1 + 1) Mod NumPoints
        Dim dx As Single = Points(i2).X - Points(i1).X
        Dim dy As Single = Points(i2).Y - Points(i1).Y

        ' Make dx and dy work for the first line.
        Select Case m_CurrentControlPoint
            Case 0
                ' Nothing to do.
                Exit Select
            Case 1
                ' dx = -dy, dy = dx
                Dim temp1 As Single = dx
                dx = -dy
                dy = temp1
                Exit Select
            Case 2
                ' dx = -dx, dy = -dy
                dx = -dx
                dy = -dy
                Exit Select
            Case 3
                ' dx = dy, dy = -dx
                Dim temp2 As Single = dx
                dx = dy
                dy = -temp2
                Exit Select
        End Select

        Dim px0 As Single = Points(ControlPoints(0)).X
        Dim py0 As Single = Points(ControlPoints(0)).Y
        Dim dx0 As Single = dx
        Dim dy0 As Single = dy
        Dim px1 As Single = Points(ControlPoints(1)).X
        Dim py1 As Single = Points(ControlPoints(1)).Y
        Dim dx1 As Single = dy
        Dim dy1 As Single = -dx
        Dim px2 As Single = Points(ControlPoints(2)).X
        Dim py2 As Single = Points(ControlPoints(2)).Y
        Dim dx2 As Single = -dx
        Dim dy2 As Single = -dy
        Dim px3 As Single = Points(ControlPoints(3)).X
        Dim py3 As Single = Points(ControlPoints(3)).Y
        Dim dx3 As Single = -dy
        Dim dy3 As Single = dx

        ' Find the points of intersection.
        CurrentRectangle = New PointF(3) {}
        FindIntersection(px0, py0, px0 + dx0, py0 + dy0, px1, py1, _
            px1 + dx1, py1 + dy1, CurrentRectangle(0))
        FindIntersection(px1, py1, px1 + dx1, py1 + dy1, px2, py2, _
            px2 + dx2, py2 + dy2, CurrentRectangle(1))
        FindIntersection(px2, py2, px2 + dx2, py2 + dy2, px3, py3, _
            px3 + dx3, py3 + dy3, CurrentRectangle(2))
        FindIntersection(px3, py3, px3 + dx3, py3 + dy3, px0, py0, _
            px0 + dx0, py0 + dy0, CurrentRectangle(3))

        ' See if this is the best bounding rectangle so far.
        ' Get the area of the bounding rectangle.
        Dim vx0 As Single = CurrentRectangle(0).X - CurrentRectangle(1).X
        Dim vy0 As Single = CurrentRectangle(0).Y - CurrentRectangle(1).Y
        Dim len0 As Single = CSng(Math.Sqrt(vx0 * vx0 + vy0 * vy0))

        Dim vx1 As Single = CurrentRectangle(1).X - CurrentRectangle(2).X
        Dim vy1 As Single = CurrentRectangle(1).Y - CurrentRectangle(2).Y
        Dim len1 As Single = CSng(Math.Sqrt(vx1 * vx1 + vy1 * vy1))

        ' See if this is an improvement.
        CurrentArea = len0 * len1
        If CurrentArea < BestArea Then
            BestArea = CurrentArea
            BestRectangle = CurrentRectangle
        End If
    End Sub

    ' Find the slope of the edge from point i to point i + 1.
    Private Sub FindDxDy(ByRef dx As Single, ByRef dy As Single, i As Integer)
        Dim i2 As Integer = (i + 1) Mod NumPoints
        dx = Points(i2).X - Points(i).X
        dy = Points(i2).Y - Points(i).Y
    End Sub

    ' Find the point of intersection between two lines.
    Private Function FindIntersection(X1 As Single, Y1 As Single, X2 As Single, Y2 As Single, A1 As Single, B1 As Single, _
        A2 As Single, B2 As Single, ByRef intersect As PointF) As Boolean
        Dim dx As Single = X2 - X1
        Dim dy As Single = Y2 - Y1
        Dim da As Single = A2 - A1
        Dim db As Single = B2 - B1
        Dim s As Single, t As Single

        ' If the segments are parallel, return False.
        If Math.Abs(da * dy - db * dx) < 0.001 Then
            Return False
        End If

        ' Find the point of intersection.
        s = (dx * (B1 - Y1) + dy * (X1 - A1)) / (da * dy - db * dx)
        t = (da * (Y1 - B1) + db * (A1 - X1)) / (db * dx - da * dy)
        intersect = New PointF(X1 + t * dx, Y1 + t * dy)
        Return True
    End Function

    ' Find a smallest bounding rectangle.
    Public Function FindSmallestBoundingRectangle() As PointF()
        ' This algorithm assumes the polygon
        ' is oriented counter-clockwise.
        Debug.Assert(Not Me.PolygonIsOrientedClockwise())

        ' Get ready;
        ResetBoundingRect()

        ' Check all possible bounding rectangles.
        For i As Integer = 0 To Points.Length - 1
            CheckNextRectangle()
        Next

        ' Return the best result.
        Return BestRectangle
    End Function
#End Region

#End Region

End Class