'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Julian Andres Cardozo Flores
' Created          : 04-08-2013
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Maintenance.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Maintenance.Entities
Imports Presentation.Common.MVP
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Base.Entities

#End Region


''' <summary>
''' Clase que contiene todo el comportamiento de la vista
''' </summary>
Public Class FrmInsurance
    Implements IInsurance, ICustomizableForm
#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Maintenance"

#End Region

#Region "Variable Globales Propiedades Intefaz y Load"

    ''' <summary>
    ''' Esta propiedad contiene el codigo de la aseguradora
    ''' </summary>
    Public Property CodeInsurance As String Implements IInsurance.CodeInsurance
        Get
            Return INDbteCode.Text.Trim
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la aseguradora
    ''' </summary>
    Public Property NameInsurance As String Implements IInsurance.NameInsurance
        Get
            Return INDtxtName.Text.Trim
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property
    Public Property CityInsurance As String Implements IInsurance.CityInsurance
        Get
            Return INDglCity.EditValue
        End Get
        Set(value As String)
            INDglCity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene la direccion del sitio web de la aseguradora
    ''' </summary>
    Public Property WebSiteInsurance As String Implements IInsurance.WebSiteInsurance
        Get
            Return INDtxtWebSite.Text.Trim
        End Get
        Set(value As String)
            INDtxtWebSite.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga los departamentos
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property DepartmentDataSource As List(Of Domain.Entities.Department) Implements IInsurance.DepartmentDataSource
        Set(value As List(Of Domain.Entities.Department))
            INDglDepartamentos.Properties.DataSource = value
            INDglDepartamentos.Properties.PopupFormWidth = 400
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga las ciudades
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CityDataSource As List(Of Domain.Entities.City) Implements IInsurance.CityDataSource
        Set(value As List(Of Domain.Entities.City))
            INDglCity.Properties.DataSource = value
            INDglCity.Properties.PopupFormWidth = 400
        End Set
    End Property
    Public Property StateInsurance As Boolean Implements IInsurance.StateInsurance
        Get
            If Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property
    Public WriteOnly Property ActionsOnControls As Boolean Implements IInsurance.ActionsOnControls
        Set(value As Boolean)
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDtxtWebSite.Enabled = value
            INDglCity.Enabled = value
            INDglDepartamentos.Enabled = value
            'BarraBotones.StatusRecordVisible = value
            INDpceContactData.Enabled = value
            If value = True Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    Dim IndigoManagementExceptions As Object
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
    ''' Variable que contiene la entidad aseguradoras
    ''' </summary>
    Dim Insurance As Insurance
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MInsurance
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PInsurance
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable que se utiliza para acceder al modelo de los departamentos
    ''' </summary>
    Dim ModelDepartment As Presentation.Common.MVP.MDepartaments
    ''' <summary>
    ''' Variable para utilizar el modelo de las ciudades
    ''' </summary>
    ''' <remarks></remarks>
    Dim ModelCity As Presentation.Common.MVP.MCity
    ''' <summary>
    ''' Variable que maneja el listado de los telefonos agregados al control
    ''' </summary>
    Dim ListadoEliminadosTelefono As New List(Of Phone)
    ''' <summary>
    '''  Variable que maneja el listado de los Direccions agregados al control
    ''' </summary>
    Dim ListadoEliminadosDireccion As New List(Of Address)
    ''' <summary>
    '''  Variable que maneja el listado de los Email agregados al control
    ''' </summary>
    Dim ListadoEliminadosEmail As New List(Of Email)

    ''' <summary>
    ''' Variable para conocer si el formulario abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim blockRecord As Domain.Entities.BlockRecordMaintenance

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.MaintenanceSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Gets or sets the sequense.
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Public Property Sequense As Domain.Entities.MaintenanceSequence
        Get
            Return Me._sequense
        End Get
        Set(value As Domain.Entities.MaintenanceSequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.MaintenanceSequenceDetail In Me._sequense.MaintenanceSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        IndigoManagementExceptions = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Insurance = Nothing
        Model = Nothing
        Presenter = Nothing
        ModelDepartment = Nothing
        ModelCity = Nothing
        ListadoEliminadosTelefono = Nothing
        ListadoEliminadosDireccion = Nothing
        ListadoEliminadosEmail = Nothing
        SearchMode = Nothing
        blockRecord = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _idOperativeUnit = Nothing
    End Sub
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrMResponsible_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDlyBudgetConcept, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        Presenter = New PInsurance(Me)
        'LoadDefinitionLayout()
        GetSequense()
        '******************************

        ''Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloMantenimiento.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Me.LoadStatus()
        Presenter = New PInsurance(Me)
        ModelDepartment = New Presentation.Common.MVP.MDepartaments(MDepartaments.TAG)
        DepartmentDataSource = Await ModelDepartment.ListAllDepartmentAsync()
        Deshacer()
    End Sub
#End Region

#Region "CRUD Base"

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        MarkEntity()
        If Insurance.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Unchanged Then
            Try
                Using Model As New MInsurance
                    AsyncLoader(True)
                    Dim result = Await Model.SaveInsurance(Insurance)
                    AsyncLoader(False)
                    If result.StateResult = True Then
                        If Insurance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            'If Not Me._sequense.Sequential Then
                            '    Me.DicSequense(Me._sequense.MaintenanceSequenceDetail(0).Id).RemoveAt(0)
                            'End If
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Me.Insurance.Nit)
                        ElseIf Insurance.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        SearchMode = False
                        Deshacer()
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    End If
                End Using
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try

        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Insurance IsNot Nothing And INDbteCode.Enabled = False Then
            If Insurance.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = Windows.Forms.DialogResult.Yes Then
                    Try
                        Using Model As New MInsurance
                            AsyncLoader(True)
                            Dim result = Await Model.DeleteInsurance(Insurance)
                            AsyncLoader(False)
                            If result = True Then
                                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                                SearchMode = False
                                Deshacer()
                                Await Me.DeleteDocumentIndexed()
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            End If
                        End Using
                    Catch ex As Exception
                        Throw ex
                        AsyncLoader(False)
                    End Try
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneAseguradora, ConceptosGenerales)
            End If
        Else
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneAseguradora, Aseguradoras)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Nit"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Insurance
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        If SearchMode = False Then
            CleanControls()
        End If
        INDbteCode.Focus()
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(ByVal value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        'If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
        '    Using Model As New MAccessories
        '        Await Model.DeleteBlockRecord(blockRecord)
        '    End Using

        '    blockRecord = Nothing
        'End If

        Using Model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
            If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(blockRecord)
                blockRecord = Nothing
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString("FrmInsurance_IndexContent", NAME_MODULE), Me.Insurance.Nit, Me.Insurance.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me.Insurance.Nit & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString("FrmInsurance_IndexTitle", NAME_MODULE), Me.Insurance.Nit), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString("FrmInsurance_IndexContent", NAME_MODULE), Me.Insurance.Nit, Me.Insurance.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString("FrmInsurance_IndexTitle", NAME_MODULE), Me.Insurance.Nit)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Sub NewEntity()
        Insurance = New Insurance()
        If Me._sequense.Scope IsNot Nothing Then
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.MaintenanceSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.MaintenanceSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequense = Me._sequense.MaintenanceSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Sub
                End If
            End If
            If Not Me._sequense.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
                        PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
                    Else
                        Using model As New MAccessories()
                            Me.DicSequense(Me._idCurrentSequense) = Await model.GetNumericSequenseGroup(Me._idCurrentSequense)
                        End Using
                        If Me.DicSequense(Me._idCurrentSequense) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
                            PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
                        Else
                            Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
                End If
            Else
                PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
            End If
        Else
            Me.ActionsOnControls = False
            Mensaje(EeventViewerImages.Advertencia) = "Revise la Secuencia Numérica por favor. Comunicar con el Administrador."
        End If
    End Sub

    ''' <summary>
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="code">The code.</param>
    Private Sub PrepareToolbar(code As String)
        INDbteCode.Text = code
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub

    ''' <summary>
    ''' Gets the sequense.
    ''' </summary>
    Public Async Sub GetSequense()
        Using model As New MInsurance
            Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        ActionsOnControls = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDtxtWebSite.Text = String.Empty
        INDglCity.EditValue = Nothing
        INDglDepartamentos.EditValue = Nothing
        CtrContacts.LimpiarControles()
        'DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Insurance = Nothing
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If
        Using Model As New MInsurance
            AsyncLoader(True)
            Insurance = Await Model.GetInsurance(INDbteCode.Text)
            AsyncLoader(False)
        End Using
        If Insurance.Id > 0 Then
            ActionsOnControls = True
            Me.BarraBotones.StatusRecordVisible = True
            Dim result = Await Model.GetBlockRecord(Me.Tag, Insurance.Id)
            With Insurance
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                CodeInsurance = .Nit
                NameInsurance = .Name
                WebSiteInsurance = .WebSite
                Dim modelCity = New MCity(MCity.TAG)
                Dim CityEntidad = modelCity.GetCityById(.IdCity)
                INDglDepartamentos.EditValue = CityEntidad.DepartamentId
                CityInsurance = .IdCity
                StateInsurance = .State

                'Control Datos de Contacto
                CtrContacts.EstablecerDataSourceDireccion = .Person.Address.ToList
                CtrContacts.EstablecerDataSourceTelefono = .Person.Phone.ToList
                CtrContacts.EstablecerDataSourceEmail = .Person.Email.ToList

                Me.GetDocumentIndexed(Me.Tag & "_" & Me.Insurance.Nit)
                If result.Id = 0 Then
                    Me.BarraBotones.SetDocuments(Insurance.Id, Me.Tag)
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    blockRecord = New Domain.Entities.BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Insurance.Id}
                    Dim operation = Await Model.SaveBlockRecord(blockRecord)
                    blockRecord = operation.ObjectEmbbeded
                Else
                    Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                    blockRecord = result
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
            End With
        Else
            ActionsOnControls = True
            If Insurance Is Nothing Then Insurance = New Insurance
            BarraBotones.PrepareToolbar(eAction.OnlySave)
        End If
    End Function

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDbteCode.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDtxtName.Text = String.Empty Then
            ValidateControls = False
        End If
        If Object.Equals(CityInsurance, Nothing) = True Then
            ValidateControls = False
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Insurance
            .Nit = CodeInsurance
            .Name = NameInsurance
            .WebSite = WebSiteInsurance
            .IdCity = CityInsurance
            .State = StateInsurance

            '*********Datos Contacto
            If Object.Equals(.Person.Address, Nothing) = False Then
                For i As Integer = 0 To ListadoEliminadosDireccion.Count - 1
                    ListadoEliminadosDireccion.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    .Person.Address.Add(ListadoEliminadosDireccion.Item(i))
                    If Me.Insurance.ChangeTracker.State = ObjectState.Unchanged Then
                        Me.Insurance.ChangeTracker.State = ObjectState.Modified
                    End If
                Next
            End If
            If Object.Equals(.Person.Phone, Nothing) = False Then
                For i As Integer = 0 To ListadoEliminadosTelefono.Count - 1
                    ListadoEliminadosTelefono.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    .Person.Phone.Add(ListadoEliminadosTelefono.Item(i))
                Next
                If Me.Insurance.ChangeTracker.State = ObjectState.Unchanged Then
                    Me.Insurance.ChangeTracker.State = ObjectState.Modified
                End If
            End If
            If Object.Equals(.Person.Email, Nothing) = False Then
                For i As Integer = 0 To ListadoEliminadosEmail.Count - 1
                    ListadoEliminadosEmail.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    .Person.Email.Add(ListadoEliminadosEmail.Item(i))
                Next
                If Me.Insurance.ChangeTracker.State = ObjectState.Unchanged Then
                    Me.Insurance.ChangeTracker.State = ObjectState.Modified
                End If
            End If
            If .Person.Id = 0 Then
                .Person.IdentificationNumber = CodeInsurance
                .Person.IdentificationType = 0
                .Person.FirstName = NameInsurance
                .Person.FirstLastName = NameInsurance
                .Person.State = True
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(INDbteCode.Text) Then
                Await Me.LoadControls()
            Else
                Mensaje(EeventViewerImages.Advertencia) = "Por favor digite un código."
            End If
        ElseIf e.KeyCode = Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
    ''' <summary>
    ''' Metodo que carga ciudades dependiendo el departamento
    ''' </summary>
    Private Sub INDGleDepartment_EditValueChanged(sender As Object, e As EventArgs) Handles INDglDepartamentos.EditValueChanged
        If Object.Equals(INDglDepartamentos.EditValue, Nothing) = False Then
            ModelCity = New Presentation.Common.MVP.MCity(MCity.TAG)
            CityDataSource = ModelCity.ListAllCityDepartment(INDglDepartamentos.EditValue)
        End If
    End Sub

    Private Sub INDtxtName_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles INDtxtName.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDpceContactData.ShowPopup()
            CtrContacts.EstablecerFocoInicial()
        End If
    End Sub
    ''' <summary>
    ''' Evento para eliminar las direcciones agregadas al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarDireccion() Handles CtrContacts.EliminarDireccion
        If Insurance.Person.Address.Count > 0 Then
            ListadoEliminadosDireccion.Add(Insurance.Person.Address.Item(CtrContacts.ItemSelecionadoDireccion))
            Insurance.Person.Address.RemoveAt(CtrContacts.ItemSelecionadoDireccion)
            CtrContacts.EstablecerDataSourceDireccion = Insurance.Person.Address
        End If
    End Sub
    ''' <summary>
    ''' Evento para eliminar los telefono agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarTelefono() Handles CtrContacts.EliminarTelefono
        If Insurance.Person.Phone.Count > 0 Then
            ListadoEliminadosTelefono.Add(Insurance.Person.Phone.Item(CtrContacts.ItemSelecionadoTelefono))
            Insurance.Person.Phone.RemoveAt(CtrContacts.ItemSelecionadoTelefono)
            CtrContacts.EstablecerDataSourceTelefono = Insurance.Person.Phone
        End If
    End Sub
    ''' <summary>
    ''' Evento para eliminar los Email agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarEmail() Handles CtrContacts.EliminarEmail
        If Insurance.Person.Email.Count > 0 Then
            ListadoEliminadosEmail.Add(Insurance.Person.Email.Item(CtrContacts.ItemSelecionadoEmail))
            Insurance.Person.Email.RemoveAt(CtrContacts.ItemSelecionadoEmail)
            CtrContacts.EstablecerDataSourceEmail = Insurance.Person.Email
        End If
    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de direcciones  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevaDireccion() Handles CtrContacts.InsertoNuevaDireccion
        If Insurance.Person.Address Is Nothing Then
            Insurance.Person.Address = New Domain.Base.Entities.TrackableCollection(Of Address)
        End If
        If Insurance.Person.Address.Where(Function(e) e.Addresss = CtrContacts.Direccion).Count = 0 Then
            Insurance.Person.Address.Add(New Address With {.Addresss = CtrContacts.Direccion, .Synchronized = "1", .State = True})
        End If
        CtrContacts.EstablecerDataSourceDireccion = Nothing
        CtrContacts.EstablecerDataSourceDireccion = Insurance.Person.Address
    End Sub
    ''' <summary>
    ''' Evento para agregar al listado de correos  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoEmail() Handles CtrContacts.InsertoNuevoEmail
        If Insurance.Person.Email Is Nothing Then
            Insurance.Person.Email = New Domain.Base.Entities.TrackableCollection(Of Email)
        End If
        If Insurance.Person.Email.Where(Function(e) e.Email1 = CtrContacts.Email).Count = 0 Then
            Insurance.Person.Email.Add(New Email With {.Email1 = CtrContacts.Email, .Synchronized = 2})
        End If
        CtrContacts.EstablecerDataSourceEmail = Nothing
        CtrContacts.EstablecerDataSourceEmail = Insurance.Person.Email
    End Sub
    ''' <summary>
    ''' Evento para agregar al listado de Telefonos  del control de datos de contacto.
    ''' </summary>
    Private Sub CtrContactos1_InsertoNuevoTelefono() Handles CtrContacts.InsertoNuevoTelefono
        If Insurance.Person.Phone Is Nothing Then
            Insurance.Person.Phone = New Domain.Base.Entities.TrackableCollection(Of Phone)
        End If
        If Insurance.Person.Phone.Where(Function(e) e.Phone1 = CtrContacts.Telefono).Count = 0 Then
            Insurance.Person.Phone.Add(New Phone With {.Phone1 = CtrContacts.Telefono, .IdPhoneType = CShort(CtrContacts.TipoTelefono), .Synchronized = "1"})
        End If
        CtrContacts.EstablecerDataSourceTelefono = Nothing
        CtrContacts.EstablecerDataSourceTelefono = Insurance.Person.Phone
    End Sub

    ''' <summary>
    ''' metodo que se dispara cuando el form esta activo y si el codigo esta habilitado le da el foco
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmBudgetConcept_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDBteCode.Enabled = True Then
            INDBteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmBudgetConcept_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.Insurance IsNot Nothing AndAlso Me.Insurance.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Metodo para marcar la entidad a guardar si se insertan detallados
    ''' </summary>
    ''' <remarks></remarks>
    Sub MarkEntity()
        If Me.Insurance.ChangeTracker.State = ObjectState.Unchanged Then
            If Me.Insurance.Person IsNot Nothing AndAlso Me.Insurance.Person.Email.Count > 0 Then
                For Each item In Me.Insurance.Person.Email
                    If item.ChangeTracker.State = ObjectState.Modified Or item.ChangeTracker.State = ObjectState.Deleted Or item.ChangeTracker.State = ObjectState.Modified Then
                        Me.Insurance.ChangeTracker.State = ObjectState.Modified
                        Exit Sub
                    End If
                Next
            End If
            If Me.Insurance.Person IsNot Nothing AndAlso Me.Insurance.Person.Address.Count > 0 Then
                For Each item In Me.Insurance.Person.Address
                    If item.ChangeTracker.State = ObjectState.Modified Or item.ChangeTracker.State = ObjectState.Deleted Or item.ChangeTracker.State = ObjectState.Modified Then
                        Me.Insurance.ChangeTracker.State = ObjectState.Modified
                        Exit Sub
                    End If
                Next
            End If
            If Me.Insurance.Person IsNot Nothing AndAlso Me.Insurance.Person.Phone.Count > 0 Then
                For Each item In Me.Insurance.Person.Phone
                    If item.ChangeTracker.State = ObjectState.Modified Or item.ChangeTracker.State = ObjectState.Deleted Or item.ChangeTracker.State = ObjectState.Modified Then
                        Me.Insurance.ChangeTracker.State = ObjectState.Modified
                        Exit Sub
                    End If
                Next
            End If
        End If
    End Sub

#End Region

#Region "Eventos Barra Botones"

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
        SearchMode = False
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
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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

    ''' <summary>
    ''' Click Boton de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Try
            Using model As New MInsurance()
                AsyncLoader(True)
                Dim Result As New ActionResult(Of Insurance)
                Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
                    Case eActionsStatusRecords.Active
                        Result = Await model.ChangeState(INDbteCode.Text, True)
                    Case eActionsStatusRecords.Inactive
                        Result = Await model.ChangeState(INDbteCode.Text, False)
                End Select
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Insurance = Result.ObjectEmbbeded
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Aqui se captura la unidad operativa seleccionada
    ''' </summary>
    ''' <param name="operatingUnit">Unidad operativa seleccionada</param>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        'If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.MaintenanceSequenceDetail IsNot Nothing Then
        '    If Me._sequense.MaintenanceSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
        '        Me._idOperativeUnit = Me._sequense.MaintenanceSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Else
        '        Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '    End If
        'End If
    End Sub
#End Region

#Region "Customizar"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyInsurance.ShowCustomizationForm()
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
            INDlyInsurance.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyInsurance.ShowCustomization
        'Try
        '    'Ejecuatamos la consulta
        '    Using model As New MInsurance
        '        AsyncLoader(True)
        '        Dim dsFields As DataSet = Await model.GetFieldsNULL
        '        AsyncLoader(False)
        '        If dsFields IsNot Nothing Then
        '            dtFieldsCustomizables = dsFields.Tables(0)
        '            For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
        '                For j As Integer = 0 To INDlyInsurance.Items.Count - 1
        '                    If Object.Equals(INDlyInsurance.Items.Item(j).Tag, Nothing) = False Then
        '                        If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyInsurance.Items.Item(j).Tag.ToString.Trim Then
        '                            INDlyInsurance.Items.Item(j).AllowHide = True
        '                        End If
        '                    End If
        '                Next
        '            Next
        '        End If
        '    End Using
        'Catch ex As Exception
        '    'IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
        '    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        'End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyInsurance.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyInsurance.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyInsurance.SaveLayoutToXml(PathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                'IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
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
            INDlyInsurance.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region




End Class