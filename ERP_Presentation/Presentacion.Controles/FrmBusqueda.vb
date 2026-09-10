'***********************************************************************
' Assembly         : Presentation.Controls.MVP.IBusqueda
' Author           : WalterSierra
' Created          : 27-03-2011
'
' Last Modified By : AndresBonilla
' Last Modified On : 18-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Importaciones"

Imports System.Configuration
Imports Presentation.Controls.MVP
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Presentation.Base.EexceptionsResources
Imports Presentation.Base.BaseClass
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.XtraGrid.Controls
Imports DevExpress.XtraLayout
Imports Presentation.Base
Imports System.Globalization

#End Region

''' <summary>
''' Clase que controla los comprotamientos del formulario FrmListaUsuarios
''' </summary>
Public Class FrmBusqueda
#Region "Interfaz Implementada"
    Implements IBusqueda

#End Region

#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmBusqueda"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        CreateButtonInformationImport()
        ImportDataButton = False
    End Sub
#End Region

#Region "Eventos"
    ''' <summary>
    ''' Metodo para devolver el valor Obtenido
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Public Event ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)

    Public Event ReturnValueWithList(ByVal ReturnValue As String, ByVal ReturnObject As Object, ByVal ReturnList As List(Of String))

    Public Event ImportPreviusData()

    Public Event ReturnActiveFilter(ByVal ActiveFilterString As String)
#End Region

#Region "variables"

    ''' <summary>
    ''' valiable para manejar el presentador
    ''' </summary>
    Dim presenter As PBusquedas
    ''' <summary>
    ''' Variable que contiene el filtro en la busqueda!
    ''' </summary>
    ''' <remarks></remarks>
    Dim Filtro As String
#End Region

#Region "propiedades"

    ''' <summary>
    ''' Filtro activo en el gridview
    ''' </summary>
    Public ActiveFilterString As String

    ''' <summary>
    ''' Obtiene el grid view de las busquedas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GridViewBusquedas As Object
        Get
            Return INDGridBusquedasView
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece si se seleccion un registro para cerrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CloseNotSelection As Boolean

    ''' <summary>
    ''' Formulario Padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FormParent As XtraForm

    Dim _ListadoDevolucion As List(Of String)
    ''' <summary>
    ''' establece el listado de fieldname devueltos
    ''' </summary>
    ''' <value>
    ''' el listado de campos devueltos._
    ''' Dim buscar As New FrmBusqueda _
    ''' Dim recibidos As New List(Of String) _
    ''' For Each item As String In buscar.ListadoDevolucion _
    ''' recibidos.Add(item) _
    ''' MessageBox.Show(item) _
    ''' Next
    ''' </value>
    Public ReadOnly Property ListadoDevolucion As System.Collections.Generic.List(Of String) Implements MVP.IBusqueda.ListadoDevolucion
        Get
            If _ListadoDevolucion Is Nothing Then
                Return New List(Of String)
            Else
                Return _ListadoDevolucion
            End If
        End Get
    End Property

    Dim _ListadoOrigenDatos As Object
    ''' <summary>
    ''' Obtiene el listado del Orgen de Datos, Mediante una Enumeracion preestablecida
    ''' </summary>
    ''' <value>el origen de datos para la consulta de la busqueda.</value>
    Public Property ListadoOrigenDatos As Object Implements MVP.IBusqueda.ListadoOrigenDatos
        Get
            Return _ListadoOrigenDatos
        End Get
        Set(value As Object)
            _ListadoOrigenDatos = value
        End Set
    End Property

    Dim _ListadoSolicitud As List(Of String)
    ''' <summary>
    ''' establece el listado de fieldname solicitados
    ''' </summary>
    ''' <value>
    ''' el listado de campos solicitados. _
    ''' cuando se necesite mas de uno, por ejemplo el codigo y el nombre _
    ''' Dim buscar As New FrmBusqueda _
    ''' Dim Solictados As New List(Of String) _
    ''' Solictados.Add("CodigoUsuario") _
    ''' Solictados.Add("NombreUsuario") _
    ''' buscar.ListadoSolicitud = Solictados
    ''' </value>
    Public WriteOnly Property ListadoSolicitud As System.Collections.Generic.List(Of String) Implements MVP.IBusqueda.ListadoSolicitud
        Set(value As System.Collections.Generic.List(Of String))
            _ListadoSolicitud = value
        End Set
    End Property

    Private _CodigoDepartamento As Integer
    Public Property CodigoDepartamento As Integer
        Get
            Return _CodigoDepartamento
        End Get
        Set(ByVal value As Integer)
            _CodigoDepartamento = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene los parámetros de búsqueda
    ''' </summary>
    Property SearchParameters As Object()

    ''' <summary>
    ''' Codigo de Departamento Concatenado con el del municipio
    ''' </summary>
    Private _CodigoDptoMunicipio As String
    Public Property CodigoDto As String
        Get
            Return _CodigoDptoMunicipio
        End Get
        Set(value As String)
            _CodigoDptoMunicipio = value
        End Set
    End Property

    Dim _ListaColumnas As List(Of ColumnInfo) = Nothing
    ''' <summary>
    ''' Obtiene o asigna la lista de columnas que se motraran
    ''' y enlazaran en el datasource de la rejilla
    ''' </summary>
    ''' <value>Lista de columnas a enlazar</value>
    ''' <returns>Lista de columnas enlazadas</returns>
    Public Property ListaColumnas As List(Of ColumnInfo)
        Get
            Return Me._ListaColumnas
        End Get
        Set(value As List(Of ColumnInfo))
            Me._ListaColumnas = value
            INDGridBusquedasView.Columns.Clear()
            For Each ci As ColumnInfo In Me._ListaColumnas

                INDGridBusquedasView.Columns.Add(New DevExpress.XtraGrid.Columns.GridColumn())
                INDGridBusquedasView.Columns(INDGridBusquedasView.Columns.Count - 1).Name = "Col" & INDGridBusquedasView.Columns.Count - 1
                INDGridBusquedasView.Columns(INDGridBusquedasView.Columns.Count - 1).Caption = ci.Caption.Trim()
                INDGridBusquedasView.Columns(INDGridBusquedasView.Columns.Count - 1).FieldName = ci.FieldName.Trim()
                INDGridBusquedasView.Columns(INDGridBusquedasView.Columns.Count - 1).Visible = ci.Visible
                INDGridBusquedasView.Columns(INDGridBusquedasView.Columns.Count - 1).DisplayFormat.FormatType = ci.ColumnFormatType
                INDGridBusquedasView.Columns(INDGridBusquedasView.Columns.Count - 1).DisplayFormat.FormatString = ci.ColumnFormat
                INDGridBusquedasView.Columns(INDGridBusquedasView.Columns.Count - 1).AppearanceCell.TextOptions.HAlignment = ci.ColumnAligment
                INDGridBusquedasView.Columns(INDGridBusquedasView.Columns.Count - 1).OptionsColumn.AllowEdit = ci.AllowEdit
                INDGridBusquedasView.Columns(INDGridBusquedasView.Columns.Count - 1).DisplayFormat.Format = ci.FormatCulture

                'si se va a poner un repositorio
                If ci.ColumnEdit Then
                    Dim repositoryTmp = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
                    repositoryTmp.AutoHeight = False
                    repositoryTmp.Name = "INDIcbSource"
                    INDGridBusquedas.RepositoryItems.Add(repositoryTmp)
                    For Each item In ci.ListItemsDatasourceColumEdit
                        repositoryTmp.Items.Add(New DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Item1, item.Item2, -1))
                    Next
                    INDGridBusquedasView.Columns(INDGridBusquedasView.Columns.Count - 1).ColumnEdit = repositoryTmp
                End If
                If ci.ColumnWidth > 0 Then INDGridBusquedasView.Columns(INDGridBusquedasView.Columns.Count - 1).Width = ci.ColumnWidth
            Next
        End Set
    End Property

    ''' <summary>
    ''' la propiedad para manejar el origen de datos obtenido desde los servicios XPO, y establecidos en la regilla
    ''' </summary>
    ''' <value>el origen de datos que se va como datasource de la regilla.</value>
    Public WriteOnly Property OrigendeDatos As Object Implements MVP.IBusqueda.OrigendeDatos
        Set(value As Object)
            INDGridBusquedas.DataSource = value
            INDGridBusquedasView.BestFitColumns()
            If INDGridBusquedasView.Columns.Contains(INDGridBusquedasView.Columns.Item("Autonumerico")) = True Then
                INDGridBusquedasView.Columns("Autonumerico").Visible = False
            End If
        End Set
    End Property

    Dim _ValorDevuelto As String
    ''' <summary>
    ''' obtiene el valor seleccionado por el usuario
    ''' </summary>
    ''' <value>
    ''' el valor seleccionado por el usuario._
    ''' Dim buscar As New FrmBusqueda _
    ''' txtCodigoUsuario.text = buscar.ValorDevuelto
    ''' </value>
    Public ReadOnly Property ValorDevuelto As String Implements MVP.IBusqueda.ValorDevuelto
        Get
            If _ValorDevuelto Is Nothing Then
                Return String.Empty
            Else
                Return _ValorDevuelto.Trim
            End If
        End Get
    End Property

    Dim _ValorObjetoDevuelto As Object
    ''' <summary>
    ''' Obtiene el valor del objeto en la fila seleccionada por el usuario
    ''' </summary>
    ''' <returns>Objeto de la fila seleccionada por el usuario</returns>
    Public ReadOnly Property ValorObjetoDevuelto As Object Implements MVP.IBusqueda.ValorObjetoDevuelto
        Get
            Return _ValorObjetoDevuelto
        End Get
    End Property

    Dim _ValorSolicitado As String
    ''' <summary>
    ''' Nombre del campo solicitado (fieldname), si no se especifica se retorna el codigo de la entidad
    ''' </summary>
    ''' <value>
    ''' el fieldname. _
    ''' Dim buscar As New FrmBusqueda _
    ''' buscar.ValorSolicitado = "CodigoUsuario"
    ''' </value>
    Public WriteOnly Property ValorSolicitado As String Implements MVP.IBusqueda.ValorSolicitado
        Set(value As String)
            _ValorSolicitado = value
        End Set
    End Property
    ''' <summary>
    ''' Contiene el valor del filtro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FiltroBusqueda As String Implements MVP.IBusqueda.FiltroBusqueda
        Get
            Return Filtro
            Filtro = Nothing
        End Get
        Set(value As String)
            Filtro = value
        End Set
    End Property

#End Region

#Region "funciones y metodos de la interfaz"

    ''' <summary>
    ''' Bandera para conocer si el boton de importar informacion se hace visible
    ''' </summary>
    Property ImportDataButton As Boolean

    ''' <summary>
    ''' este metodo sirve para llamar el metodo obtener valores
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Aceptar() Implements MVP.IBusqueda.Aceptar
        CloseNotSelection = False
        ObtenerValores()
        Me.Close()
    End Sub

    ''' <summary>
    ''' este metodo sirve para cerrar el frontal
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Cancelar() Implements MVP.IBusqueda.Cancelar
        If FormParent IsNot Nothing Then
            Dim Parent As FormBase = CType(FormParent, FormBase)
            If Parent.IsMainSearch = True Then
                Parent.BarraBotones.PrepareToolbar(eAction.OnlyFind)
            End If
        End If
        CloseNotSelection = True
        _ValorDevuelto = String.Empty
        _ListadoDevolucion = New List(Of String)
        Me.Close()
    End Sub

    Private INDsbImportInformation As SimpleButton
    ''' <summary>
    ''' Crea el boton de importar informacion
    ''' </summary>
    Public Sub CreateButtonInformationImport()
        INDsbImportInformation = New SimpleButton()
        INDsbImportInformation.Image = Global.Presentation.Controls.My.Resources.ICONOGRAFIA_Importar_informacion__24x24__blanco
        INDsbImportInformation.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        INDsbImportInformation.Appearance.Options.UseFont = True
        INDsbImportInformation.Location = New System.Drawing.Point(716, 6)
        INDsbImportInformation.MaximumSize = New System.Drawing.Size(34, 28)
        INDsbImportInformation.MinimumSize = New System.Drawing.Size(34, 28)
        INDsbImportInformation.ToolTip = "Importar Información"
        INDsbImportInformation.Name = "INDsbDataImport"
        INDsbImportInformation.Size = New System.Drawing.Size(34, 28)
        AddHandler INDsbImportInformation.Click, AddressOf Sb_ImportPreviusData
        If INDGridBusquedas IsNot Nothing Then
            Me.INDGridBusquedas.Controls.Add(INDsbImportInformation)
            INDsbImportInformation.Location = New Point((INDGridBusquedas.Width - INDsbImportInformation.Size.Width - 56), 24)
            INDsbImportInformation.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            INDsbImportInformation.BringToFront()
        End If
        INDsbImportInformation.Visible = False
    End Sub

    ''' <summary>
    ''' SB_s the import previus data.
    ''' </summary>
    Private Sub Sb_ImportPreviusData()
        RaiseEvent ImportPreviusData()
    End Sub

    ''' <summary>
    ''' metodo utilizado para obtener los valores seleccionados por el usuario, de acuerdo a las propiedades
    ''' ListadoSolicitud y
    ''' ValorSolicitado
    ''' </summary>
    Public Sub ObtenerValores() Implements MVP.IBusqueda.ObtenerValores
        Dim activeFilter As DevExpress.XtraGrid.Views.Base.ViewFilter = INDGridBusquedasView.ActiveFilter

        'obtengo el valor seleccionado
        If Not _ValorSolicitado Is Nothing Then
            If _ValorSolicitado.Length > 0 Then
                'valido que haya seleccionado alguna fila
                If INDGridBusquedasView.FocusedRowHandle < 0 Then
                    Exit Sub
                End If
                Dim valorRejilla As Object
                'obtengo el valor de la regilla
                valorRejilla = INDGridBusquedasView.GetRowCellValue(INDGridBusquedasView.FocusedRowHandle, _ValorSolicitado)
                If valorRejilla Is Nothing Then
                    Throw New ArgumentNullException(obtenerExcepcion(BusquedaValorNoEncontradoComplemento), String.Concat(obtenerExcepcion(BusquedaValorNoEncontrado), "{Codigo}"))
                    Exit Sub
                End If
                _ValorDevuelto = valorRejilla.ToString
                Dim obj = INDGridBusquedasView.GetRow(INDGridBusquedasView.FocusedRowHandle)
                _ValorObjetoDevuelto = If(obj.GetType().Equals(GetType(DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)), DirectCast(obj, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, obj)
            End If
        Else
            'valido que haya seleccionado alguna fila
            If INDGridBusquedasView.FocusedRowHandle < 0 Then
                Exit Sub
            End If

            'obtengo el valor de la rejilla
            If INDGridBusquedasView.Columns.Contains(INDGridBusquedasView.Columns.Item(0)) = True Then
                _ValorDevuelto = INDGridBusquedasView.GetRowCellValue(INDGridBusquedasView.FocusedRowHandle, INDGridBusquedasView.Columns.Item(0)).ToString
                _ValorObjetoDevuelto = INDGridBusquedasView.GetRow(INDGridBusquedasView.FocusedRowHandle)
            Else
                _ValorDevuelto = String.Empty
            End If

        End If
        'obtengo el listado de valores solicitados
        If Not _ListadoSolicitud Is Nothing Then
            If _ListadoSolicitud.Count > 0 Then
                'valido que haya seleccionado alguna fila
                If INDGridBusquedasView.FocusedRowHandle < 0 Then
                    Exit Sub
                End If
                'creo el objeto a devolver
                _ListadoDevolucion = New List(Of String)
                'recorro los valores solicitados
                For Each item As String In _ListadoSolicitud
                    'establezclo los valores a  devolver
                    _ListadoDevolucion.Add(INDGridBusquedasView.GetRowCellValue(INDGridBusquedasView.FocusedRowHandle, item).ToString)
                Next
            End If
        End If
    End Sub

#End Region

#Region "Funciones y Metodos del Funcional"

    Private Sub FrmBusqueda_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If FormParent IsNot Nothing Then
            Dim Parent As FormBase = CType(FormParent, FormBase)
            For i = 0 To Parent.INDPanelControlBase.Controls.Count - 1
                Parent.INDPanelControlBase.Controls(i).Visible = True
            Next
        End If
        If CloseNotSelection = False Then
            RaiseEvent ReturnValue(ValorDevuelto, ValorObjetoDevuelto)
            If ListadoDevolucion IsNot Nothing Then
                RaiseEvent ReturnValueWithList(ValorDevuelto, ValorObjetoDevuelto, ListadoDevolucion)
            End If
        End If
        RaiseEvent ReturnActiveFilter(INDGridBusquedasView.ActiveFilterString)
    End Sub


    ''' <summary>
    ''' evento load del funcional y valida si el buscador viene con filtro
    ''' </summary>
    Private Sub FrmBusqueda_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        CloseNotSelection = True
        If Me.TopLevel = False Then
            INDpcButtons.Visible = False
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Else
            INDpcButtons.Visible = True
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        End If
        presenter = New PBusquedas(Me)
        UpdateDatasource()
        Me.INDGridBusquedas.Focus()
        Me.INDGridBusquedasView.Focus()
        Me.INDGridBusquedasView.ActiveFilterString = ActiveFilterString
    End Sub

    ''' <summary>
    ''' Metodo para mostrar el formulario de busquedas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ShowSearch(Optional main As Boolean = True)
        If FormParent IsNot Nothing Then
            Dim Parent As FormBase = CType(FormParent, FormBase)
            If Parent.BarraBotones.PermiteConsultar = False Then
                Exit Sub
            End If
            If SessionValues.Instance.UserViewMode = True And Parent.ViewModeEditHold = False Then
                If Parent.INDPanelControlBase.Controls.Count > 1 Then
                    For i = 0 To Parent.INDPanelControlBase.Controls.Count - 1
                        Parent.INDPanelControlBase.Controls(i).Visible = False
                    Next
                End If
                Me.TopLevel = False
                Me.Dock = System.Windows.Forms.DockStyle.Fill
                Me.Parent = Parent.INDPanelControlBase

                Parent.BarraBotones.PrepareToolbar(eAction.OnlyNew)

                INDlblTitle.Text = Parent.Text
                INDsbImportInformation.Visible = False
                Me.Show()
                Me.BringToFront()
            Else
                Me.TopLevel = True
                If ImportDataButton Then
                    INDsbImportInformation.Visible = True
                End If
                Dim Transparent As New FrmTransparent(Me, False)
                Transparent.ShowDialog()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Actualiza el datasource de la rejilla
    ''' </summary>
    Public Sub UpdateDatasource()
        If SearchParameters IsNot Nothing AndAlso SearchParameters.Count() > 0 Then
            presenter.ConsultarEntidades(SearchParameters)
        ElseIf FiltroBusqueda = String.Empty Then
            presenter.ConsultarEntidades()
        Else
            presenter.ConsultarEntidades(FiltroBusqueda)
        End If
    End Sub

    ''' <summary>
    ''' Evento Click en el boton aceptar
    ''' </summary>
    Private Sub INDbtnAceptar_Click(sender As System.Object, e As System.EventArgs) Handles INDbtnAceptar.Click
        If INDGridBusquedasView.RowCount > 0 Then
            Aceptar()
        Else
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento Click en el boton cancelar
    ''' </summary>
    Private Sub INDbtnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles INDbtnCancelar.Click
        If INDGridBusquedasView.RowCount > 0 Then
            Cancelar()
        Else
            Me.Close()
        End If
    End Sub

    'Private Sub INDGridBusquedasView_AsyncCompleted(sender As Object, e As EventArgs) Handles INDGridBusquedasView.AsyncCompleted
    '    Dim findPanel As FindControl = TryCast(INDGridBusquedas.Controls("FindControl"), FindControl)
    '    Dim layout As LayoutControl = TryCast(findPanel.Controls(0), LayoutControl)
    '    Dim item As LayoutControlItem = TryCast(layout.Items(2), LayoutControlItem)
    '    item.Control.Focus()
    'End Sub

    ''' <summary>
    ''' Evento DobleClick en la regilla
    ''' </summary>
    Private Sub INDGridBusquedasView_DoubleClick(sender As Object, e As System.EventArgs) Handles INDGridBusquedasView.DoubleClick
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim pt As Point = view.GridControl.PointToClient(Control.MousePosition)
        Dim info As GridHitInfo = view.CalcHitInfo(pt)
        If info.InRow OrElse info.InRowCell Then
            Aceptar()
        End If
    End Sub



    ''' <summary>
    ''' Evento Keydown en la regilla
    ''' </summary>
    Private Sub INDGridBusquedasView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDGridBusquedasView.KeyDown
        If e.KeyCode = Keys.Enter Then
            Aceptar()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para capturar las teclas precionadas
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <param name="keyData"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        If keyData = Keys.Escape Then
            Cancelar()
        End If
    End Function

#End Region

    Sub Aceptar(p1 As Object)

    End Sub
End Class

''' <summary>
''' Clase que encapsula los datos necesarios para crear una columna
''' en la rejilla del frontal de busquedas
''' </summary>
Public Class ColumnInfo

#Region "Members"

    ''' <summary>
    ''' Obtiene o asigna el nombre que sera mostrado
    ''' en la cabecera de la columna
    ''' </summary>
    ''' <value>Nombre que sera mostrado en la cabecera de la columna</value>
    ''' <returns>Nombre mostrado en la cabecera de la columna</returns>
    Public Property Caption As String
    ''' <summary>
    ''' Obtiene o asigna el nombre del campo que sera enlazado a la columna
    ''' </summary>
    ''' <value>Nombre del campo que sera enlazado a la columna</value>
    ''' <returns>Nombre del campo enlazado a la columna</returns>
    Public Property FieldName As String
    ''' <summary>
    ''' Propiedad para ocultar o mostrar una columna
    ''' </summary>
    ''' <value>Visibilidad de la columna</value>
    ''' <returns>Visibilidad</returns>
    ''' <remarks></remarks>
    Public Property Visible As Boolean = True
    ''' <summary>
    ''' propiedad que obtiene y establece el ancho de la columna
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ColumnWidth As Integer

    ''' <summary>
    ''' Propiedad que establece el formato de la columna
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ColumnFormat As String
    ''' <summary>
    ''' propiedad para saber que formato se le da a la columna
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ColumnFormatType As DevExpress.Utils.FormatType
    ''' <summary>
    ''' propiedad para alinear el texto de la columna
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ColumnAligment As DevExpress.Utils.HorzAlignment
    ''' <summary>
    ''' propiedad para asignar un repositorio a la rejilla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ColumnEdit As Boolean

    Public Property ListItemsDatasourceColumEdit As Object
    ''' <summary>
    ''' propiedad para permitir o no edicion en la columna
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllowEdit As Boolean

    ''' <summary>
    ''' Formato de la cultura para el displayformat
    ''' </summary>
    ''' <returns></returns>
    Public Property FormatCulture As CultureInfo

    ''' <summary>
    ''' Propiedad que establece el formato de la columna segun la zona horaria
    ''' </summary>
    Private _FormatMask As String
    Public Property FormatMask As String
        Get
            Dim IndigoSession = SessionValues.Instance
            Dim MaskDate = Utils.GetCustomDateFormat(If(IndigoSession IsNot Nothing, IndigoSession.dateFormat, -1))
            Return MaskDate
        End Get
        Set(value As String)
            _FormatMask = value
        End Set
    End Property
#End Region

End Class