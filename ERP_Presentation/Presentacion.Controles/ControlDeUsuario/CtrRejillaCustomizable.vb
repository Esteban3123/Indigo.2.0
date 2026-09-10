'***********************************************************************
' Assembly         : Presentation.Controls.CtrRejillaCustomizable
' Author           : JulianCardozo
' Created          : 01-11-2011
'
' Last Modified By : JulianCardozo
' Last Modified On : 01-11-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Impostadas"
Imports Presentation.Controls.MVP
Imports System.Configuration
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Presentation.Base.EexceptionsResources
Imports Presentation.Base.BaseClass
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO
Imports System.ComponentModel
Imports DevExpress.XtraGrid.Columns
Imports Infrastructure.CrossCutting.Base

#End Region
''' <summary>
''' Clase que contiene todos los procedimientos para ejecurtar acciones del control de usuario CtrRejillaCustomizable
''' </summary>
Public Class CtrRejillaCustomizable


#Region "Variables y Load"

    ''' <summary>
    ''' Variable para instanciar los valores de session
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable que se utiliza para definir el nombre de la definicion del control
    ''' </summary>
    Dim NombreDefinicionControl As String
    ''' <summary>
    ''' Variable que me controla si dispara el evento FocusedRowChanged
    ''' </summary>
    Dim _PermitirFocusedRowChanged As Boolean
    ''' <summary>
    ''' Evento [focused row changed].
    ''' </summary>
    Public Event FocusedRowChanged()
    ''' <summary>
    ''' variable que se utiliza para controlar el evento de dragdrop de las columnas de la rejilla
    ''' </summary>
    Dim PermitirDragDrop As Boolean = True
    ''' <summary>
    '''  Objeto asincrono
    ''' </summary>
    Dim Asincrono As BackgroundWorker
    ''' <summary>
    ''' Variable donde se van a guardar las definiciones de las rejillas
    ''' </summary>
    Dim RutaGuardarDefiniciones As String
    ''' <summary>
    ''' objeto BackgroundWorker para cargar las definiciones de manera asincrona
    ''' </summary>
    Private WithEvents CargarDefinicionesAsicrona As BackgroundWorker
    ''' <summary>
    ''' Funcion que se utilzica para verificar si se carga una definicion de rejilla de forma asincrona
    ''' </summary>
    Private ExisteDefinicion As Boolean

    ''' <summary>
    ''' Handles the Load event of the CtrRejillaCustomizable control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub CtrRejillaCustomizable_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        NombreDefinicionControl = String.Concat("\", MyBase.ParentForm.Name, ".", Me.Name, ".IndigoUser.",Indigo.UserIndigo, ".xml")
    End Sub


#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad que sirve para establece un valor de visible a las columnas predeterminadas por el developer
    ''' </summary>
    ''' <value>A.</value>
    WriteOnly Property EstablecerColumnasVisibles As String
        Set(ByVal value As String)
            If Object.Equals(INDgcCustomizableView.Columns(value), Nothing) = False Then
                INDgcCustomizableView.Columns(value).Visible = True
            End If
        End Set
    End Property



    Public Sub NombreColumna(NombreFieldName As String, NombreAColocar As String)
        For Each col As GridColumn In INDgcCustomizableView.Columns
            If col.FieldName = NombreFieldName.Trim Then
                col.Caption = NombreAColocar
                Exit Sub
            End If
        Next
    End Sub


    Public Sub NombreFieldName(NombreFiedlNameAnterior As String, NombreFieldNameNuevo As String)
        For Each col As GridColumn In INDgcCustomizableView.Columns
            If col.FieldName = NombreFiedlNameAnterior.Trim Then
                col.FieldName = NombreFieldNameNuevo
                Exit Sub
            End If
        Next
    End Sub

    ' ''' <summary>
    ' ''' Sets the obtener listado.
    ' ''' </summary>
    ' ''' <value>The obtener listado.</value>
    WriteOnly Property EstablecerOrigenDatos() As Object
        Set(ByVal value As Object)

            If Object.Equals(value, Nothing) = False Then
                'Cargamos el Control
                INDgcCustomizable.DataSource = value
                INDgcCustomizableView.BestFitColumns()

                'Iteramos para ocultar todas las Columnas establecer el AllowFocus= false ; AllowEdit= false de todas las columnas que traiga el objeto
                For Each colum As DevExpress.XtraGrid.Columns.GridColumn In INDgcCustomizableView.Columns
                    colum.Visible = False
                    colum.OptionsColumn.AllowFocus = False
                    colum.OptionsColumn.AllowEdit = False
                Next

                'Definimos la ruta donde se van a guardar las definiciones de las rejillas
                'RutaGuardarDefiniciones = String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\",Indigo.IndigoCompanyName, "\", Indigo.IndigoCompanyName, "\XML\RejillasCustomizables")
                RutaGuardarDefiniciones = String.Concat(Window.Utils.LocalFolder(), "\", Indigo.IndigoCompanyName, "\", Indigo.IndigoCompanyName, "\XML\RejillasCustomizables")
                'Cargamos la definicion de la rejilla de forma asincrona si existe !
                CargarDefinicionesAsicrona = New BackgroundWorker
                If CargarDefinicionesAsicrona.IsBusy = False Then
                    CargarDefinicionesAsicrona.RunWorkerAsync()
                End If

            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que se utiliza para establecer un nombre especifico a una columna especifica
    ''' </summary>
    ''' <value>The establecer nombre columnas.</value>
    WriteOnly Property EstablecerNombreColumnas(ByVal NombreColumna As String) As String
        Set(ByVal value As String)
            If Object.Equals(INDgcCustomizableView.Columns(NombreColumna), Nothing) = False Then
                INDgcCustomizableView.Columns(NombreColumna).Caption = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que sirve para establecer el autofilterrow
    ''' </summary>
    ''' <value><c>true</c>ShowAutoFilterRow= true<c>ShowAutoFilterRow=false</c>.</value>
    WriteOnly Property EstablecerAutoFiltro As Boolean
        Set(ByVal value As Boolean)
            INDgcCustomizableView.OptionsView.ShowAutoFilterRow = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad que sirve para permitir el find panel del la rejilla
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [establecer panel busqueda]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property EstablecerPanelBusqueda As Boolean
        Set(ByVal value As Boolean)
            INDgcCustomizableView.OptionsFind.AllowFindPanel = value
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
    Public ReadOnly Property ValorDevuelto As String
        Get
            If _ValorDevuelto Is Nothing Then
                Return String.Empty
            Else
                Return _ValorDevuelto.Trim
            End If
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
    Public WriteOnly Property ValorSolicitado As String
        Set(ByVal value As String)
            _ValorSolicitado = value
        End Set
    End Property
    ''' <summary>
    ''' Sets a value indicating whether [permitir focused row changed].
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [permitir focused row changed]; otherwise, <c>false</c>.
    ''' </value>
    Property PermitirFocusedRowChanged As Boolean
        Get
            Return _PermitirFocusedRowChanged
        End Get
        Set(ByVal value As Boolean)
            _PermitirFocusedRowChanged = value
        End Set
    End Property

    WriteOnly Property BarraNavegacion As Boolean
        Set(ByVal value As Boolean)
            INDgcCustomizable.UseEmbeddedNavigator = value
        End Set
    End Property

#End Region

#Region "Metodos"


    Public Sub LimpiarRejilla()
        INDgcCustomizable.DataSource = Nothing
    End Sub

    Public Function ExistenDatosRejilla() As Boolean

    End Function

    ''' <summary>
    ''' Procedimiento que se utiliza para guardar la definicion de la rejilla si se customizan
    ''' </summary>
    Public Sub GuardarDefinicionRejilla()

        'Verificamos si existe la carpeta donde se van a guardar las definiciones de las rejillas
        If My.Computer.FileSystem.DirectoryExists(RutaGuardarDefiniciones) = False Then
            My.Computer.FileSystem.CreateDirectory(RutaGuardarDefiniciones)
        End If
        'Guardamos la definicion
        INDgcCustomizableView.SaveLayoutToXml(String.Concat(RutaGuardarDefiniciones, "\", MyBase.ParentForm.Name, ".", Me.Name, ".IndigoUser.",Indigo.UserIndigo, ".xml"))

    End Sub


    ''' <summary>
    ''' metodo utilizado para obtener los valores seleccionados por el usuario, de acuerdo a las propiedades
    ''' ListadoSolicitud y
    ''' ValorSolicitado
    ''' </summary>
    Public Sub ObtenerValores()
        'obtengo el valor seleccionado
        If Not _ValorSolicitado Is Nothing Then
            If _ValorSolicitado.Length > 0 Then
                'valido que haya seleccionado alguna fila
                If INDgcCustomizableView.FocusedRowHandle < 0 Then
                    Exit Sub
                End If
                Dim valorRejilla As Object
                'obtengo el valor de la regilla
                valorRejilla = INDgcCustomizableView.GetFocusedRowCellValue(_ValorSolicitado)
                If valorRejilla Is Nothing Then
                    Throw New ArgumentNullException(obtenerExcepcion(BusquedaValorNoEncontradoComplemento), String.Concat(obtenerExcepcion(BusquedaValorNoEncontrado), "{Codigo}"))
                    Exit Sub
                End If
                _ValorDevuelto = valorRejilla.ToString
            End If
        Else
            'valido que haya seleccionado alguna fila
            If INDgcCustomizableView.FocusedRowHandle < 0 Then
                Exit Sub
            End If
            'obtengo el valor de la rejilla
            If INDgcCustomizableView.Columns.Contains(INDgcCustomizableView.Columns.Item("Codigo")) = True Then
                _ValorDevuelto = INDgcCustomizableView.GetRowCellValue(INDgcCustomizableView.FocusedRowHandle, "Codigo").ToString
            Else
                _ValorDevuelto = String.Empty
            End If

        End If
    End Sub

#End Region

#Region "Eventos Privados "

    ''' <summary>
    ''' Handles the DoWork event of the CargarDefinicionesAsicrona control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsicrona_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles CargarDefinicionesAsicrona.DoWork
        If My.Computer.FileSystem.FileExists(String.Concat(RutaGuardarDefiniciones, "\", MyBase.ParentForm.Name, ".", Me.Name, ".IndigoUser.",Indigo.UserIndigo, ".xml")) = True Then
            ExisteDefinicion = True
        End If
    End Sub
    ''' <summary>
    ''' Handles the RunWorkerCompleted event of the CargarDefinicionesAsicrona control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsicrona_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles CargarDefinicionesAsicrona.RunWorkerCompleted
        If ExisteDefinicion = True Then
            INDgcCustomizableView.RestoreLayoutFromXml(String.Concat(RutaGuardarDefiniciones, "\", MyBase.ParentForm.Name, ".", Me.Name, ".IndigoUser.",Indigo.UserIndigo, ".xml"))
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para guardar la definicion de la rejilla cada ves que dimensionamos el tamaño de las columnas.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.ColumnEventArgs" /> instance containing the event data.</param>
    Private Sub INDgcCustomizableView_ColumnWidthChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ColumnEventArgs) Handles INDgcCustomizableView.ColumnWidthChanged
        GuardarDefinicionRejilla()
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para guardar la definicion de la rejilla cada ves que se agregan,se quitan columnas de la rejila se dispara al cerrar el formulario de customizacion.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDgcCustomizableView_HideCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDgcCustomizableView.HideCustomizationForm
        GuardarDefinicionRejilla()
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para obtener un valor cada ves que se seleccion una fila de la rejilla se dipara el evento y devuelve un valor.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs" /> instance containing the event data.</param>
    Private Sub INDgcCustomizableView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles INDgcCustomizableView.FocusedRowChanged
        If INDgcCustomizableView.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        If _PermitirFocusedRowChanged = True Then
            RaiseEvent FocusedRowChanged()
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utilza para activar el timer que porsteriormente guardara la definicion de la rejilla este evento se activa cada ves que hago Drop a una columna de la rejilla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs" /> instance containing the event data.</param>
    Private Sub INDgcCustomizableView_DragObjectDrop(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs) Handles INDgcCustomizableView.DragObjectDrop
        If PermitirDragDrop = True Then
            GuardarDefinicionRejilla()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para controlar la bandera que permite el drop se activa al abrir el formulario de customizacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDgcCustomizableView_ShowCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDgcCustomizableView.ShowCustomizationForm
        PermitirDragDrop = False
    End Sub

    ''' <summary>
    ''' Obtiene el iten seleccionado de la rejilla
    ''' </summary>
    ''' <value>The item selecionado direccion.</value>
    ReadOnly Property ItemSelecionado As Integer
        Get
            Return INDgcCustomizableView.FocusedRowHandle
        End Get
    End Property


    ''' <summary>
    ''' Metodo Utilizado para borrar el item seleccionado
    ''' </summary>
    Public Sub EliminarsSeleccionado()
        INDgcCustomizableView.DeleteRow(ItemSelecionado)
    End Sub


#End Region







End Class
