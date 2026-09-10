'***********************************************************************
' Assembly         : Presentacion.Common
' Author           : Jose Luis Rojas
' Created          : 11-04-2011
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.ComponentModel
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common.MVP
Imports Presentation.Controls

#End Region

''' <summary>
''' Formulario de Ciudades
''' </summary>
''' <remarks></remarks>
Public Class FrmCity
    Implements ICity, ICustomizableForm

#Region "Globals And Properties"

    ''' <summary>
    ''' Esta función es una propiedad pública llamada ICARetentionConceptId
    ''' que implementa la interfaz ICity.ICARetentionConceptId. 
    ''' </summary>
    ''' <returns></returns>
    Public Property ICARetentionConceptId As Integer? Implements ICity.ICARetentionConceptId
        Get
            Return INDsleICARetentionConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleICARetentionConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta función es una propiedad pública llamada ICARetentionConceptXpo
    ''' que implementa la interfaz ICity.ICARetentionConceptXpo. 
    ''' </summary>
    ''' <returns></returns>
    Public Property ICARetentionConceptXpo As XPInstantFeedbackSource Implements ICity.ICARetentionConceptXpo
        Get
            Return INDsleICARetentionConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleICARetentionConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Variable que contiene la instancia del formulario de busquedas
    ''' </summary>
    ''' <remarks></remarks>
    Dim FormSearchObjects As FrmBusqueda

    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Contiene la entidad 
    ''' </summary>
    Dim City As City

    ''' <summary>
    ''' Contiene el modelo
    ''' </summary>
    Dim Model As MCity

    ''' <summary>
    ''' Contiene el presentador
    ''' </summary>
    Dim Presenter As PCity

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecord

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    '''  La propiedad se utiliza para controlar la habilitación y enfoque de varios controles
    '''  relacionados con la gestión de información de una ciudad (o entidad similar) en una
    '''  aplicación.
    ''' </summary>
    Public WriteOnly Property ActionsOnControsl As Boolean Implements ICity.ActionsOnControsl
        Set(value As Boolean)
            INDlyCity.BeginUpdate()
            INDgleCountry.Enabled = Not value
            INDbteCode.Enabled = value
            INDgleDepartment.Enabled = value
            INDtxtName.Enabled = value
            INDsleICARetentionConcept.Enabled = value
            INDlyCity.EndUpdate()
            If value = False Then
                INDgleCountry.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property


    ''' <summary>
    ''' Esta función es una propiedad de solo lectura (ReadOnly) llamada MyLayoutControl que implementa la interfaz ICity.MyLayoutControl. 
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICity.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property


    ''' <summary>
    ''' La propiedad se utiliza para acceder y asignar una lista de objetos de tipo Department (Departamento)
    ''' a un control de tipo INDgleDepartment (posiblemente un control de selección) que está relacionado 
    ''' con la gestión de información de departamentos en una interfaz de usuario.
    ''' </summary>
    Public Property DataSourceOfAllDepartments As List(Of Department) Implements ICity.DataSourceOfAllDepartments
        Get
            Return INDgleDepartment.Properties.DataSource
        End Get
        Set(value As List(Of Department))
            INDgleDepartment.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad se utiliza para acceder y asignar una lista de objetos de tipo Country (País)
    ''' a un control de tipo INDgleCountry (posiblemente un control de selección) que está relacionado
    ''' con la gestión de información de países en una interfaz de usuario
    ''' </summary>
    Public Property DataSourceOfAllCountries As List(Of Country) Implements ICity.DataSourceOfAllCountries
        Get
            Return INDgleCountry.Properties.DataSource
        End Get
        Set(value As List(Of Country))
            INDgleCountry.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta función es una propiedad pública llamada CodeCity que implementa la interfaz ICity.CodeCity.
    ''' Esta propiedad se utiliza para acceder y asignar el código de una ciudad en una aplicación relacionada
    ''' con la gestión de ciudades. 
    ''' </summary>
    Public Property CodeCity As String Implements ICity.CodeCity
        Get
            If (INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta función es una propiedad pública llamada IdDepartment que implementa la interfaz ICity.IdDepartment.
    ''' La propiedad se utiliza para acceder y asignar el identificador de un departamento
    ''' </summary>
    Public Property IdDepartment As String Implements ICity.IdDepartment
        Get
            Return INDgleDepartment.EditValue
        End Get
        Set(value As String)
            INDgleDepartment.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta función es una propiedad pública llamada NameCity que implementa la interfaz ICity.NameCity.
    ''' Esta propiedad se utiliza para acceder y asignar el nombre de una ciudad
    ''' </summary>
    Public Property NameCity As String Implements ICity.NameCity
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta función es una propiedad pública llamada StatusCity que implementa la interfaz ICity.StatusCity.
    ''' La propiedad se utiliza para acceder y asignar el estado de una ciudad (activo o inactivo)
    ''' en una aplicación relacionada con la gestión de ciudades.
    ''' </summary>
    Public Property StatusCity As Boolean Implements ICity.StatusCity
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property


#End Region

#Region "ICRUD"

    ''' <summary>
    ''' Esta función parece ser parte de una clase que implementa una interfaz llamada Base.IcrudBase.
    ''' La función se llama AbrirBusqueda() y está destinada a abrir una ventana de búsqueda en una aplicación
    ''' que está relacionada con la gestión de ciudades u objetos similares.
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Codigo", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.8)}}.ToList()
            .FiltroBusqueda = IdDepartment
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.City
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Esta función es un subproceso asíncrono llamado ReturnValue que es un controlador
    ''' de eventos en una aplicación que gestiona la búsqueda y selección de ciudades.
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        CodeCity = ReturnValue
        If CodeCity IsNot String.Empty Then
            Await LoadControls()
            If Not INDbteCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    '''  La función se utiliza para iniciar el proceso de búsqueda en una aplicación
    '''  relacionada con la gestión de registros, como ciudades u objetos similares.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' La función se utiliza para deshacer (limpiar) los controles de la interfaz
    ''' de usuario en una aplicación relacionada con la gestión de registros,
    ''' como ciudades u objetos similares.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Este subproceso se llama Eliminar() y parece ser parte de una implementación
    ''' de una interfaz Base.IcrudBase para realizar la eliminación de registros en una aplicación.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        'Declara una variable result para almacenar el resultado de la acción de eliminación.
        Dim result As ActionMessageResult(Of City)
        'Verifica si la variable City no es nula (City representa un objeto que probablemente representa una ciudad). Si City no es nulo, el proceso continúa.
        If City IsNot Nothing Then
            'primer bloque If, verifica si el campo Id de City es mayor que cero. Esto indica que la ciudad tiene un identificador válido y puede ser eliminada.
            If City.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Try
                        'Llama al método AsyncLoader(True) para iniciar una operación asincrónica (carga).
                        AsyncLoader(True)
                        'Llama al método DeleteCityAsync() del objeto Model (que parece ser un modelo de datos) para intentar eliminar la ciudad.
                        result = Await Model.DeleteCityAsync(City)
                        If result.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            Me.CleanControls()
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            AsyncLoader(False)
                            Deshacer()
                        Else
                            AsyncLoader(False)
                            If result.MessageResult.ElementAt(0).CodeMessage = "c-0000" Then
                                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            End If
                        End If
                    Catch ex As Exception
                        AsyncLoader(False)
                        Throw ex
                    End Try
                End If
            Else
                'Si la condición anterior es verdadera, muestra un mensaje de confirmación al usuario utilizando MessageIndigo.Show()
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaCiudad, Eform.City)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaCiudad, Eform.City)
        End If
    End Sub

    ''' <summary>
    ''' Este subproceso se llama Guardar() y parece ser parte de una implementación de la interfaz Base.IcrudBase
    ''' para gestionar la operación de guardar (crear o actualizar) registros en una aplicación.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Try
            'If City.ChangeTracker.State <> ObjectState.Unchanged Then
            AsyncLoader(True)
            Using modelCity As New MCity(MyBase.Tag)
                Dim res = Await modelCity.SaveCityAsync(City)
                If res.StateResult Then
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    If City.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                    ElseIf City.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or City.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    End If
                    Me.City = res.ObjectEmbbeded
                    AsyncLoader(False)
                    Deshacer()
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    AsyncLoader(False)
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' La función se utiliza para configurar la lógica de habilitación/deshabilitación del botón de "Actualizar"
    ''' en una barra de botones (BarraBotones) en una aplicación relacionada con la gestión de registros.
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta función es una propiedad de solo escritura (WriteOnly) llamada Mensaje que implementa la interfaz Base.IcrudBase.Mensaje.
    ''' La propiedad se utiliza para mostrar mensajes en una aplicación relacionada con la gestión de registros.
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Este subproceso se llama Nuevo() y parece ser parte de una implementación de la interfaz Base.IcrudBase
    ''' para realizar acciones relacionadas con la creación de un nuevo registro en una aplicación de gestión de registros.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        Deshacer()
        'If FormSearchObjects IsNot Nothing Then FormSearchObjects.Close()
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

#End Region

#Region "Events"
    ''' <summary>
    ''' Este subproceso se llama Frm_Disposed y parece ser un controlador de eventos que se ejecuta
    ''' cuando el formulario actual (MyBase) se está cerrando o liberando de la memoria.
    ''' </summary>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        FormSearchObjects = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        City = Nothing
        Model = Nothing
        Presenter = Nothing
        record = Nothing
    End Sub

#Region "ButtonClick"

    ''' <summary>
    ''' Este subproceso es un controlador de eventos que se activa
    ''' cuando se hace clic en un botón en un control INDsleICARetentionConcept
    ''' de tipo Selector de Lista Editable (SLE) de la librería DevExpress.
    ''' La función del subproceso parece estar relacionada con la selección y adición de conceptos de retención en una aplicación. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleICARetentionConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleICARetentionConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(605, Nothing, True)
            Presenter.InitializeICARetention()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' La función del subproceso parece estar relacionada con la inicialización de la lista de opciones de conceptos de retención en una aplicación.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleICARetentionConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleICARetentionConcept.QueryPopUp
        If ICARetentionConceptXpo Is Nothing Then
            Presenter.InitializeICARetention()
        End If
    End Sub

#End Region

    ''' <summary>
    ''' Este subproceso llamado LoadStatus() parece estar relacionado con la carga
    ''' y configuración de estados de registros (por ejemplo, activo o inactivo)
    ''' en una barra de botones (BarraBotones) de una aplicación.
    ''' </summary>
    Private Sub LoadStatus()
        'Dim listStates As New List(Of StatusRecord)()
        'listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(ComunesActivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        'listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(ComunesInactivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        'Me.BarraBotones.States = listStates
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' El subproceso FrmCity_Load es un controlador de eventos que se activa cuando el formulario FrmCity se carga.
    ''' En este subproceso, se realizan varias tareas de configuración y inicialización para preparar el formulario y sus componentes.
    ''' </summary>
    Private Sub FrmCity_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.LayoutControls.SetIsCustomizable(Me.INDlyCity, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Model = New MCity(MCity.TAG)
        Me.indigo = SessionValues.Instance
        '******************************'
        Me._funct = AddressOf GenerateDoc

        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloCommonCity.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PCity(Me)
        Presenter.Initializes()
        Presenter.LoadDefinitionLayout()
        Deshacer()
        LoadStatus()
        GleSize()
    End Sub

    ''' <summary>
    ''' La función GleSize() ajusta el tamaño del formulario emergente (popup)
    ''' que se muestra cuando se interactúa con dos controles del tipo Selector
    ''' de Lista Editable (GLE) en la interfaz de usuario.
    ''' </summary>
    Public Sub GleSize()
        INDgleCountry.Properties.PopupFormSize = New Drawing.Size(500, 200)
        INDgleDepartment.Properties.PopupFormSize = New Drawing.Size(500, 200)
    End Sub


    ''' <summary>
    ''' La función de este subproceso es abrir una búsqueda relacionada con el código en la interfaz de usuario. 
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    '''  La función de este subproceso es manejar las teclas presionadas, especialmente las teclas Enter y F4. 
    ''' </summary>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown

        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(INDbteCode.Text.ToString) Then
                Await LoadControls()
                If INDbteCode.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "Por favor digite un código"
                INDbteCode.Focus()
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    ''' <summary>
    ''' La función de este subproceso es cargar y habilitar los departamentos relacionados con el país seleccionado en la interfaz de usuario
    ''' </summary>
    Private Async Sub INDgleCountry_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleCountry.EditValueChanged
        If INDgleCountry.EditValue IsNot Nothing AndAlso Not INDgleCountry.EditValue.Equals(String.Empty) Then
            INDgleDepartment.Enabled = True
            Using Model As New MDepartaments(MDepartaments.TAG)
                DataSourceOfAllDepartments = Await Model.GetDepartmentsAsync(INDgleCountry.EditValue)
            End Using
            INDgleDepartment.Focus()
        End If
    End Sub

    ''' <summary>
    ''' La función de este subproceso es realizar ciertas acciones relacionadas con la selección de país en la interfaz de usuario.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleCountry_Leave(sender As Object, e As EventArgs) Handles INDgleCountry.Leave
        Dim cod As String = INDgleCountry.EditValue
        If cod IsNot Nothing AndAlso Not cod.Equals(String.Empty) Then
            INDgleDepartment.Enabled = True
        Else
            DataSourceOfAllDepartments = New List(Of Department)
            INDgleDepartment.EditValue = Nothing
            INDgleDepartment.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' La función de este subproceso es manejar el cambio de departamento seleccionado en la interfaz de usuario y ajustar la habilitación y el foco en otros controles en consecuencia. 
    ''' </summary>
    Private Sub INDgleDepartment_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleDepartment.EditValueChanged
        If INDgleDepartment.EditValue IsNot Nothing AndAlso Not INDgleDepartment.EditValue.Equals(String.Empty) Then
            INDbteCode.Enabled = True
            INDtxtName.Enabled = False
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' La función de este subproceso es manejar acciones relacionadas con la selección de departamento en la interfaz de usuario.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleDepartment_Leave(sender As Object, e As EventArgs) Handles INDgleDepartment.Leave
        Dim codDep As String = INDgleDepartment.EditValue
        If codDep IsNot Nothing AndAlso codDep.Equals(String.Empty) Then
            INDgleDepartment.Focus()
            INDbteCode.Enabled = False
            CodeCity = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Este subproceso es un controlador de eventos que se activa cuando el formulario FrmBank se está cerrando.
    ''' La función de este subproceso es realizar ciertas acciones antes de que el formulario se cierre completamente.
    ''' </summary>
    Private Sub FrmBank_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.City IsNot Nothing AndAlso Me.City.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                INDbteCode.Text = Me.IdEntity
                Await Me.LoadControls(CInt(Me.IdEntity.Trim))
            End If
        Else 'Realiza la consulta normal
            INDbteCode.Text = Me.IdEntity
            Await Me.LoadControls(Me.IdEntity.Trim)
        End If
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton de crear un pais
    ''' </summary>
    Private Sub INDgleCountry_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleCountry.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCountry
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Using Model As New MCountry(MCountry.TAG)
                    DataSourceOfAllCountries = Model.ListAllCountries()
                End Using
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton de crear un departamento
    ''' </summary>
    Private Async Sub INDgleDepartment_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleDepartment.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmDepartaments
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Using Model As New MDepartaments(MDepartaments.TAG)
                    DataSourceOfAllDepartments = Await Model.GetDepartmentsAsync(INDgleCountry.EditValue)
                End Using
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el formulario
    ''' </summary>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDgleCountry.Text Is String.Empty Then
            INDgleCountry.Focus()
        End If
    End Sub




#End Region

#Region "Methods"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        'Se obtiene la fecha actual del servidor a través de la función 
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmCityMetaData, Eform.InfoMetaData), Me.City.Code, Me.City.Name, INDgleCountry.Text, INDgleDepartment.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.City.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmCityMetaDataTitle, Eform.InfoMetaData), Me.City.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmCityMetaData, Eform.InfoMetaData), Me.City.Code, Me.City.Name, INDgleCountry.Text, INDgleDepartment.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmCityMetaDataTitle, Eform.InfoMetaData), Me.City.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MCity(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles del formulario
    ''' </summary>
    Public Sub CleanControls()

        INDlyCity.BeginUpdate()
        ActionsOnControsl = False

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me._doc = Nothing

        StatusCity = True
        'Limpiar controles
        City = Nothing
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDgleCountry.EditValue = Nothing
        INDgleDepartment.EditValue = Nothing
        INDgleCountry.Text = String.Empty
        INDgleDepartment.Text = String.Empty

        ICARetentionConceptId = Nothing
        INDsleICARetentionConcept.Properties.NullText = String.Empty

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyCity.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Valida los controles del formulario
    ''' </summary>
    Public Function ValidateControls() As Boolean
        ValidateControls = True
        If INDbteCode.Text = String.Empty Then
            INDbteCode.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDgleDepartment.EditValue = Nothing Then
            INDgleDepartment.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDtxtName.Text = String.Empty Then
            INDtxtName.Focus()
            ValidateControls = False
            Exit Function
        End If
    End Function

    ''' <summary>
    ''' Asigna los valores del formulario a la entidad
    ''' </summary>
    Public Sub AssigningValues()
        With City
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = CodeCity
            .Name = NameCity
            .DepartamentId = IdDepartment
            .ICARetentionConceptId = ICARetentionConceptId
        End With
    End Sub

    ''' <summary>
    ''' Carga/consulta una ciudad
    ''' </summary>
    Public Async Function LoadControls(Optional idEntity As String = "0") As Task
        'If Not String.IsNullOrEmpty(INDbteCode.Text.ToString) AndAlso Not String.IsNullOrWhiteSpace(INDbteCode.Text.ToString) Then
        '    If Me.BarraBotones.PermiteConsultar = False Then
        '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '        Exit Function
        '    End If


        '    StatusCity = True
        '    AsyncLoader(True)
        '    INDlyCity.BeginUpdate()
        '    Dim idCountry As Integer = 0
        '    If idEntity <> "0" Then
        '        City = Await Model.GetCityAsync(idEntity)
        '        Dim modelDep As MDepartaments = New MDepartaments(MDepartaments.TAG)
        '        Dim department = Await modelDep.GetDepartmentByIdAsync(City.DepartamentId)
        '        idCountry = department.CountryId
        '    Else
        '        City = Await Model.GetCityByDepartmentAsync(CodeCity, IdDepartment)
        '    End If

        '    If City IsNot Nothing Then
        '        If City.Id > 0 Then
        '            Me.BarraBotones.StatusRecordVisible = True
        '            Dim result = Await Model.GetBlockRecord(Me.Tag, City.Id)
        '            With City
        '                'Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), City.CreationUser)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), City.CreationDate)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), City.ModificationUser)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), IIf(City.ModificationDate Is Nothing, Nothing, City.ModificationDate))
        '                CodeCity = .Code
        '                NameCity = .Name
        '                StatusCity = .State
        '                IdDepartment = .DepartamentId
        '                If idCountry > 0 Then
        '                    INDgleCountry.EditValue = idCountry
        '                End If
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '            End With

        '            Me.GetDocumentIndexed(Me.Tag & "_" & Me.City.Id)

        '            If result.Id = 0 Then
        '                Me.BarraBotones.SetDocuments(City.Id)
        '                Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                state.State = Domain.Base.Entities.ObjectState.Added
        '                record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = City.Id}
        '                Dim operation = Await Model.SaveBlockRecord(record)
        '                record = operation.ObjectEmbbeded
        '            Else
        '                record = result
        '                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '            End If
        '        Else
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        End If
        '    Else
        '        City = New City()
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    End If
        '    AsyncLoader(False)
        '    INDgleCountry.Enabled = False
        '    INDgleDepartment.Enabled = False
        '    INDbteCode.Enabled = False
        '    INDtxtName.Enabled = True
        '    INDtxtName.Focus()

        '    INDlyCity.EndUpdate()
        'End If



        If Not String.IsNullOrEmpty(CodeCity) AndAlso Not String.IsNullOrWhiteSpace(CodeCity) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MCity(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim idCountry As Integer = 0
                    If idEntity <> "0" Then
                        City = Await Model.GetCityAsync(idEntity)
                        Dim modelDep As MDepartaments = New MDepartaments(MDepartaments.TAG)
                        Dim department = Await modelDep.GetDepartmentByIdAsync(City.DepartamentId)
                        idCountry = department.CountryId
                    Else
                        City = Await Model.GetCityByDepartmentAsync(CodeCity, IdDepartment)
                    End If
                    INDlyCity.BeginUpdate()
                    If City IsNot Nothing AndAlso City.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        'Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(City.Id))
                        With City
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            'Llenar Entidad

                            CodeCity = .Code
                            NameCity = .Name
                            StatusCity = .State
                            IdDepartment = .DepartamentId
                            ICARetentionConceptId = .ICARetentionConceptId
                            INDsleICARetentionConcept.Properties.NullText = .ICARetentionDescription
                            If idCountry > 0 Then
                                INDgleCountry.EditValue = idCountry
                            End If
                        End With
                        'Llenar NullText

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.City.Code)
                        If record.Id = 0 Then
                            record = (Await Model.SaveBlockRecord(
                                    New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = City.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                        Me.BarraBotones.SetDocuments(City.Id, Me.Tag.ToString(), Nothing, GetType(City).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        'ActionsOnControsl = True
                        'End Using
                    Else
                        City = New City() With {.State = True}
                        AsyncLoader(False)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    End If
                    INDgleCountry.Enabled = False
                    INDgleDepartment.Enabled = False
                    INDbteCode.Enabled = False
                    INDtxtName.Enabled = True
                    INDsleICARetentionConcept.Enabled = True
                    INDtxtName.Focus()
                    INDlyCity.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDgleCountry.Enabled = False
                INDgleDepartment.Enabled = False
                INDbteCode.Enabled = False
                INDtxtName.Enabled = True
                INDsleICARetentionConcept.Enabled = True
                INDtxtName.Focus()
                Throw ex
            End Try
        End If





    End Function
    ''' <summary>
    ''' La función de este subproceso es cambiar el estado de la entidad "City"
    ''' entre activo e inactivo y proporcionar retroalimentación al usuario sobre el resultado de esta acción. 
    ''' </summary>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive

        If Not String.IsNullOrEmpty(CodeCity) Then
            Using model As New MCity(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not Me.City.State
                Dim Result = Await model.ChangeState(CodeCity, state)
                AsyncLoader(False)
                If Result.StateResult Then
                    Me.City = Result.ObjectEmbbeded
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub
#End Region

#Region "Bar Buttons Events"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

#End Region

#Region "Customizar"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyCity.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) = True Then
            ExistDefinitionFront = True
        End If
    End Sub

    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
        If ExistDefinitionFront = True Then
            INDlyCity.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'Ejecuatamos la consulta
            Dim dsFields As DataSet = Await Model.GetFieldsNULLAsync()
            If dsFields IsNot Nothing Then
                dtFieldsCustomizables = dsFields.Tables(0)
                For j As Integer = 0 To INDlyCity.Items.Count - 1
                    INDlyCity.Items.Item(j).AllowHide = False
                    For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                        If Object.Equals(INDlyCity.Items.Item(j).Tag, Nothing) = False Then
                            If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyCity.Items.Item(j).Tag.ToString.Trim Then
                                INDlyCity.Items.Item(j).AllowHide = True
                            End If
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs)
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyCity.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyCity.SaveLayoutToXml(PathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDlyCity.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

End Class