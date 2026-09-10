'***********************************************************************
' Assembly         : Presentation.Controls.GridLookUpEditExtendido
' Author           : JulianCardozo
' Created          : 02-11-2011
'
' Last Modified By : JulianCardozo
' Last Modified On : 04-11-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Base.BaseClass
Imports Presentation.Base.EResources
Imports Presentation.Base.Eform
Imports Presentation.Controls.MVP
Imports System.ComponentModel
Imports System.Data
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Clase que contiene toda la logica funcional del control GridLookUpEditExtendido...
''' </summary>
Public Class GridLookUpEditExtendido


#Region "Variables y Load"


    ''' <summary>
    ''' Variable que se utiliza para definir el nombre de la definicion del control
    ''' </summary>
    Dim NombreDefinicionControl As String
    ''' <summary>
    ''' Variable donde se van a guardar las definiciones de las rejillas
    ''' </summary>
    Dim RutaGuardarDefiniciones As String
    ''' <summary>
    ''' 
    ''' </summary>
    Dim NombrEform As String
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' objeto BackgroundWorker para cargar las definiciones de manera asincrona
    ''' </summary>
    Private WithEvents CargarDefinicionesAsincronas As BackgroundWorker
    ''' <summary>
    ''' Variable que me controla cuando se debe guardar la definicion del control
    ''' </summary>
    Private PermitirGuardarDefinicion As Boolean
    ''' <summary>
    ''' Variable que me especifica si existe un definicion xml del control
    ''' </summary>
    Private ExisteDefinicion As Boolean
    ''' <summary>
    ''' variable que se utiliza para controlar el evento de dragdrop de las columnas del control
    ''' </summary>
    Private PermitirDragDrop As Boolean = True
    ''' <summary>
    ''' Bandera para saber cuando realmente se a inicializado el load del control.
    ''' </summary>
    Dim BanderaOrigenDatos As Boolean
    ''' <summary>
    ''' Variable que me especifica si el control establece cache
    ''' </summary>
    Private _EstablecerCache As Boolean
    ''' <summary>
    ''' Variable de tipo enumeracion que me permite saber que consulta realizar.
    ''' </summary>
    Dim OrigenDatos As Object
    ''' <summary>
    ''' Usando delegados y funciones "callback" este delegado define 
    ''' un método Sub que recibe un parámetro de tipo object.
    ''' </summary>
    Delegate Sub DelegadoDataSource(ByVal DataSource As Object)
    ''' <summary>
    ''' Evento que dispara para establecer el focus en el proximo control.
    ''' </summary>
    Public Event FocusProximoControl()
    ''' <summary>
    ''' Evento que se dispara en el evento query popup del control
    ''' </summary>
    Public Event QueryPopUpPersonalizado()
    ''' <summary>
    ''' Evento que se dispara en el evento click del boton para establecer cache de servidor
    ''' </summary>
    Public Event ClickBtnCache()
    ''' <summary>
    ''' Occurs when [focused row changed personalizado].
    ''' </summary>
    Public Event EditValueChangedPersonalizado()
    ''' <summary>
    ''' Occurs when [key down personalidado].
    ''' </summary>
    Public Event KeyDownPersonalidado()
    ''' <summary>
    ''' Manejador del evento load del control GridLookUpEditExtendido.
    ''' </summary>
    Private Sub GridLookUpEditExtendido_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BanderaOrigenDatos = True
    End Sub

#End Region

#Region "Propiedades Publicas Disponibles para el uso del control."

    ''' <summary>
    ''' Propiedad que resuelve las entidades y consume el servicio del modelo
    ''' </summary>
    <Browsable(False)> _
    Public WriteOnly Property EstablecerDatosXPO As Object
        Set(ByVal value As Object)
            If DesignMode = False Then
                If INDBackgroundWorker.IsBusy <> True And BanderaOrigenDatos = True Then
                    INDlciUploader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    ' Empieza la operacion asincrona.
                    OrigenDatos = value
                    INDBackgroundWorker.RunWorkerAsync()
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Sets the establecer origen datos.
    ''' </summary>
    ''' <value>The establecer origen datos.</value>
    WriteOnly Property INDEstablecerDatosEntidades() As Object
        Set(ByVal value As Object)

            If Object.Equals(value, Nothing) = False Then

                INDglCustomizable.Properties.DataSource = value
                INDglCustomizableView.BestFitColumns()
                'Ocultamos el icono de carga
                INDlciUploader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'Iteramos para ocultar todas las Columnas establecer el AllowFocus= false ; AllowEdit= false de todas las columnas que traiga el objeto
                INDglCustomizable.Properties.PopulateViewColumns()
                For Each col As GridColumn In INDglCustomizableView.Columns
                    col.Visible = False
                    col.OptionsColumn.AllowFocus = False
                    col.OptionsColumn.AllowEdit = False
                Next
                'Definimos la ruta donde se van a guardar las definiciones de las rejillas
                'RutaGuardarDefiniciones = String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\XML\GridlookCustomizables")
                RutaGuardarDefiniciones = String.Concat(Window.Utils.LocalFolder(), "\", "Vie HealtTech", "\", "Indigo Vie EHR", "\XML\GridlookCustomizables")

                NombreDefinicionControl = String.Concat("\", Me.ParentForm.Name, ".", Me.Name, ".IndigoUser.", Indigo.UserIndigo, ".xml")

                'Cargamos la definicion de la rejilla de forma asincrona si existe !
                CargarDefinicionesAsincronas = New BackgroundWorker
                If CargarDefinicionesAsincronas.IsBusy = False Then
                    CargarDefinicionesAsincronas.RunWorkerAsync()
                End If
            End If

        End Set
    End Property

    ''' <summary>
    ''' Propiedad que sirve para establece un valor de visible a las columnas predeterminadas por el developer
    ''' </summary>
    ''' <value>A.</value>
    WriteOnly Property INDEstablecerColumnasVisibles As String
        Set(ByVal value As String)
            If Object.Equals(INDglCustomizableView.Columns(value), Nothing) = False Then
                INDglCustomizableView.Columns(value).Visible = True
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que sirve para establecer el displayMember del control.
    ''' </summary>
    ''' <value>The display member.</value>
    WriteOnly Property INDDisplayMember As String
        Set(ByVal value As String)
            INDglCustomizable.Properties.DisplayMember = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que sirve para establecer el ValueMember del control
    ''' </summary>
    ''' <value>The value member.</value>
    WriteOnly Property INDValueMember As String
        Set(ByVal value As String)
            INDglCustomizable.Properties.ValueMember = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que me establece si el control puede consultar cache de serdor.
    ''' </summary>
    ''' <value><c>true</c> if [establecer cache]; otherwise, <c>false</c>.</value>
    WriteOnly Property INDEstablecerCache As Boolean
        Set(ByVal value As Boolean)
            _EstablecerCache = value
            If _EstablecerCache = True Then
                INDglCustomizable.Properties.Buttons(1).Visible = True
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que sirve para establecer el autofilterrow
    ''' </summary>
    ''' <value><c>true</c>ShowAutoFilterRow= true<c>ShowAutoFilterRow=false</c>.</value>
    WriteOnly Property INDEstablecerAutoFiltro As Boolean
        Set(ByVal value As Boolean)
            INDglCustomizableView.OptionsView.ShowAutoFilterRow = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que se utiliza para establecer un nombre especifico a una columna especifica
    ''' </summary>
    ''' <value>Caption que quiere hacer visible en la columna</value>
    WriteOnly Property INDEstablecerNombreColumnas(ByVal NombreColumna As String) As String
        Set(ByVal value As String)
            INDglCustomizable.Properties.PopulateViewColumns()
            If Object.Equals(INDglCustomizableView.Columns(NombreColumna), Nothing) = False Then
                INDglCustomizableView.Columns(NombreColumna).Caption = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Sets the IND imagen carga.
    ''' </summary>
    ''' <value>The IND imagen carga.</value>
    WriteOnly Property INDImagenCarga As DevExpress.XtraLayout.Utils.LayoutVisibility
        Set(ByVal value As DevExpress.XtraLayout.Utils.LayoutVisibility)
            INDlciUploader.Visibility = value
        End Set
    End Property



#End Region

#Region "Metodos y Funciones Publicas"


    ReadOnly Property Texto As String
        Get
            Return INDglCustomizable.Text.ToString.Trim
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el valor seleccionado del tipo de dato especificado
    ''' </summary>
    ''' <typeparam name="TTipoDato">el tipo dato esperado.</typeparam>
    ''' <param name="FieldName">Fieldname de la Consulta.</param>
    ''' <returns></returns>
    Public Function ObtenerValor(Of TTipoDato)(ByVal FieldName As String) As String
        If ExisteSeleccionado() = False Then
            Return Nothing
        End If
        If INDglCustomizableView.Columns(FieldName) Is Nothing Then
            Return Nothing
        End If
        Return INDglCustomizableView.GetFocusedRowCellValue(FieldName).ToString
        'Return CType(CType(INDglCustomizable.GetSelectedDataRow, DataRowView).Item(FieldName), TTipoDato)
    End Function







    Public Sub NombreColumna(ByVal NombreFieldName As String, ByVal NombreAColocar As String)
        For Each col As GridColumn In INDglCustomizableView.Columns
            If col.FieldName = NombreFieldName.Trim Then
                col.Caption = NombreAColocar
                Exit Sub
            End If
        Next
    End Sub

    ReadOnly Property ObtenerFilaSelecionada As Integer
        Get
            Return INDglCustomizableView.FocusedRowHandle
        End Get
    End Property




    ''' <summary>
    ''' Obtiene el valor(llave) de la fila seleccionada
    ''' </summary>
    ''' <typeparam name="TTipoDato">el tipo de dato de la llave.</typeparam>
    ''' <returns></returns>
    Public Function ObtenerValorPorLlave(Of TTipoDato)() As TTipoDato
        If ExisteSeleccionado() = False Then
            Return Nothing
        End If
        Return CType(INDglCustomizable.EditValue, TTipoDato)
    End Function

    ''' <summary>
    ''' metodo para la validacion de vacios
    ''' </summary>
    ''' <returns></returns>
    Public Function ExisteSeleccionado() As Boolean
        If INDglCustomizable.EditValue Is Nothing Then
            Return False
        End If
        Return True
    End Function
    ''' <summary>
    ''' Establece el valor que desea aparesca como seleccionado.
    ''' </summary>
    Public Sub EstablecerSeleccionado(ByVal ValorLlave As Integer)
        INDglCustomizable.EditValue = ValorLlave
    End Sub
    ''' <summary>
    ''' Limpia el control.
    ''' </summary>
    Public Sub LimpiarControl()
        INDglCustomizable.EditValue = Nothing
    End Sub
    ''' <summary>
    ''' Metodo para establecer el focus en el control gridLookupEdit Extendido.
    ''' </summary>
    Public Overloads Sub Focus()
        INDglCustomizable.Focus()
    End Sub

    Public Sub AbrirGridLoukup()
        INDglCustomizable.ShowPopup()
    End Sub

#End Region

#Region "Metodos privados"

    ''' <summary>
    ''' Procedimiento que se utiliza para guardar la definicion de la gridLook si se customizan
    ''' </summary>
    Public Sub GuardarDefinicionRejilla()
        'Verificamos si existe la carpeta donde se van a guardar las definiciones de las rejillas
        If My.Computer.FileSystem.DirectoryExists(RutaGuardarDefiniciones) = False Then
            My.Computer.FileSystem.CreateDirectory(RutaGuardarDefiniciones)
        End If
        'Guardamos la definicion
        INDglCustomizableView.SaveLayoutToXml(String.Concat(RutaGuardarDefiniciones, NombreDefinicionControl))
    End Sub
    ''' <summary>
    ''' Manejador del evento DoWork del control INDBackgroundWorker.
    ''' </summary>
    Private Sub INDBackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles INDBackgroundWorker.DoWork
        'INDGridLookupEdit.Properties.DataSource = OrigenDatos
        'ERROR: Cross-thread operation not valid: Control 'INDGridLookupEdit' accessed from a thread other than the thread it was created on.

        ' En lugar de asignar directamente el datasource llamamos al método EstablecerDataSource,!!!
        EstablecerDataSource(OrigenDatos)
        e.Result = True
    End Sub

    ''' <summary>
    ''' Establece el datasource del control para saber si ya a sido invocado el hilo que se encontraba en uso.
    ''' </summary>
    Private Function EstablecerDataSource(ByVal DataSource As Object) As Object
        ' InvokeRequired compara el ID del hilo que se va a ejecutar 
        ' con el ID de la llamada del hilo.
        ' retorna el datasource de tipo enumeracion eorigendatos.

        If INDglCustomizable.InvokeRequired Then
            Dim d As New DelegadoDataSource(AddressOf EstablecerDataSource)
            Me.Invoke(d, New Object() {DataSource})
        Else
            Dim modelo As New MGridLookupEditExtendido
            INDglCustomizable.Properties.DataSource = modelo.ConsultarEntidades(DataSource)
        End If
        Return DataSource
    End Function

    ''' <summary>
    ''' Manejador del evento RunWorkerCompleted del control INDBackgroundWorker.
    ''' </summary>
    Private Sub INDBackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles INDBackgroundWorker.RunWorkerCompleted
        '  objetoFormBase.barraProgresoEstado = False
        INDlciUploader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub


#End Region

#Region "Eventos"

    ''' <summary>
    ''' Grids the look up edit extendido_ focus proximo control.
    ''' </summary>
    Private Sub GridLookUpEditExtendido_FocusProximoControl() Handles Me.FocusProximoControl
        RaiseEvent FocusProximoControl()
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para guardar la definicion del gridlook cada ves que se agregan,se quitan columnas de la rejila se dispara al cerrar el formulario de customizacion.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.ColumnEventArgs" /> instance containing the event data.</param>
    Private Sub INDglCustomizableView_ColumnWidthChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ColumnEventArgs) Handles INDglCustomizableView.ColumnWidthChanged
        PermitirGuardarDefinicion = True
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para guardar la definicion de la rejilla este evento se activa cada ves que hago Drop a una columna del Grid Look
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs" /> instance containing the event data.</param>
    Private Sub INDglCustomizableView_DragObjectDrop(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs) Handles INDglCustomizableView.DragObjectDrop
        If PermitirDragDrop = True Then
            PermitirGuardarDefinicion = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the HideCustomizationForm event of the INDglCustomizableView control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDglCustomizableView_HideCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDglCustomizableView.HideCustomizationForm
        PermitirGuardarDefinicion = True
    End Sub
    ''' <summary>
    ''' Handles the StartGrouping event of the INDglCustomizableView control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDglCustomizableView_StartGrouping(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDglCustomizableView.StartGrouping
        PermitirGuardarDefinicion = True
    End Sub
    ''' <summary>
    ''' Handles the EndGrouping event of the INDglCustomizableView control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDglCustomizableView_EndGrouping(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDglCustomizableView.EndGrouping
        PermitirGuardarDefinicion = True
    End Sub

    ''' <summary>
    ''' Evento que me controla el evento click del boton de actualizacion de cache
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs" /> instance containing the event data.</param>
    Private Sub INDglCustomizable_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDglCustomizable.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            RaiseEvent ClickBtnCache()
        End If
    End Sub
    ''' <summary>
    ''' Handles the Closed event of the INDglCustomizable control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ClosedEventArgs" /> instance containing the event data.</param>
    Private Sub INDglCustomizable_Closed(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDglCustomizable.Closed
        If PermitirGuardarDefinicion = True Then
            GuardarDefinicionRejilla()
        End If
    End Sub
    ''' <summary>
    ''' Handles the ShowCustomizationForm event of the INDglCustomizableView control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDglCustomizableView_ShowCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDglCustomizableView.ShowCustomizationForm
        PermitirDragDrop = False
    End Sub

    ''' <summary>
    ''' EditValueChangedPersonalizado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDglCustomizable_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDglCustomizable.EditValueChanged
        RaiseEvent EditValueChangedPersonalizado()

    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDglCustomizable control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
    Private Sub INDglCustomizable_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles INDglCustomizable.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            RaiseEvent KeyDownPersonalidado()
        End If
    End Sub

#End Region

#Region "Eventos Asincronos"

    ''' <summary>
    ''' evento que se dispara en el querypopup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
    Private Sub INDglCustomizable_QueryPopUp(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDglCustomizable.QueryPopUp
        RaiseEvent QueryPopUpPersonalizado()
    End Sub

    ''' <summary>
    ''' Handles the DoWork event of the CargarDefinicionesAsincronas control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles CargarDefinicionesAsincronas.DoWork
        If My.Computer.FileSystem.FileExists(String.Concat(RutaGuardarDefiniciones, NombreDefinicionControl)) = True Then
            ExisteDefinicion = True
        End If
    End Sub
    ''' <summary>
    ''' Handles the RunWorkerCompleted event of the CargarDefinicionesAsincronas control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles CargarDefinicionesAsincronas.RunWorkerCompleted
        If ExisteDefinicion = True Then
            INDglCustomizableView.RestoreLayoutFromXml(String.Concat(RutaGuardarDefiniciones, NombreDefinicionControl))
        End If
    End Sub


#End Region




End Class
