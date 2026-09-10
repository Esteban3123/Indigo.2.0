'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Julian Andres Cardozo Flores
' Created          : 10-08-2013
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
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Windows.Forms

#End Region


''' <summary>
''' Clase que contiene todo el comportamiento de la vista
''' </summary>
Public Class FrmPoliza
    Implements IPoliza, ICustomizableForm

#Region "Constantes"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Maintenance"
#End Region

#Region "Variable Globales Propiedades Intefaz y Load"
    ''' <summary>
    ''' Gets my tag.
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As Object Implements IPoliza.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As Domain.Entities.MaintenanceSequence Implements IPoliza.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.MaintenanceSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MaintenanceSequenceDetail In Me._sequence.MaintenanceSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecordMaintenance

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.MaintenanceSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64


    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' Esta propiedad contiene el codigo de la poliza
    ''' </summary>
    Public Property CodePoliza As String Implements IPoliza.CodePoliza
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
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
    ''' Esta propiedad contiene el nombre de la poliza
    ''' </summary>
    Public Property NamePoliza As String Implements IPoliza.NamePoliza
        Get
            Return INDtxtName.Text.Trim
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el id de la aseguradora
    ''' </summary>
    Public Property IdInsurance As Integer Implements IPoliza.IdInsurance
        Get
            Return INDglAseguradora.EditValue
        End Get
        Set(value As Integer)
            INDglAseguradora.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad que contiene la fecha inicial de la poliza
    ''' </summary>
    Public Property InitialDate As Date Implements IPoliza.InitialDate
        Get
            Return INDdtFechaInicio.EditValue
        End Get
        Set(value As Date)
            INDdtFechaInicio.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad que contiene la fecha final de la poliza
    ''' </summary>
    Public Property EndDate As Date Implements IPoliza.EndDate
        Get
            Return INDdtFechaFinal.EditValue
        End Get
        Set(value As Date)
            INDdtFechaFinal.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad que contiene el tipo de poliza
    ''' </summary>
    Public Property IdPolizaType As Integer Implements IPoliza.IdPolizaType
        Get
            Return INDglPolizaType.EditValue
        End Get
        Set(value As Integer)
            INDglPolizaType.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad que contiene el objecto de la poliza
    ''' </summary>
    Public Property Objects As String Implements IPoliza.Objects
        Get
            Return INDmeObjeto.Text.Trim
        End Get
        Set(value As String)
            INDmeObjeto.Text = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPoliza.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    ''' <summary>
    ''' Propiedad estado de la poliza
    ''' </summary>
    Public Property Status As Boolean Implements IPoliza.StatePoliza
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            If value = False Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga las aseguradoras
    ''' </summary>
    Public WriteOnly Property InsuranceDataSource As List(Of Insurance) Implements IPoliza.InsuranceDataSource
        Set(value As List(Of Insurance))
            INDglAseguradora.Properties.DataSource = value
            INDglAseguradora.Properties.PopupFormWidth = 400
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga los tipos de poliza
    ''' </summary>
    Public WriteOnly Property PolizaTypeDataSource As List(Of PolizaType) Implements IPoliza.PolizaTypeDataSource
        Set(value As List(Of PolizaType))
            INDglPolizaType.Properties.DataSource = value
            INDglPolizaType.Properties.PopupFormWidth = 400
        End Set
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IPoliza.ActionsOnControls
        Set(value As Boolean)
            INDlyPoliza.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDglAseguradora.Enabled = value
            INDdtFechaInicio.Enabled = value
            INDdtFechaFinal.Enabled = value
            INDglPolizaType.Enabled = value
            INDmeObjeto.Enabled = value
            'BarraBotones.StatusRecordVisible = value
            INDlyPoliza.EndUpdate()
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
    ''' Variable que contiene la entidad Poliza
    ''' </summary>
    Dim Poliza As Poliza
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MPoliza
    ''' <summary>
    ''' Variable que contiene la fecha del servidor
    ''' </summary>
    Dim ServerDate As Date
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PPoliza
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _searchMode = Nothing
        IndigoManagementExceptions = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Poliza = Nothing
        Model = Nothing
        ServerDate = Nothing
        Presenter = Nothing
    End Sub
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrMResponsible_Load(sender As Object, e As EventArgs) Handles Me.Load
        '_idOperativeUnit = BarraBotones.OperatingUnitValue
        'Me.LoadStatus()
        'Me._funct = AddressOf GenerateDoc
        'Presenter = New PPoliza(Me)
        'PolizaTypeDataSource = Await Model.ListAllPolizaType
        'InsuranceDataSource = Await Model.ListAllInsurance
        'Deshacer()
        'Presenter.GetSequense()
        'Deshacer()
        '_searchMode = False

        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PPoliza(Me)
        Presenter.GetSequense()
        PolizaTypeDataSource = Await Model.ListAllPolizaType
        InsuranceDataSource = Await Model.ListAllInsurance
        'Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub
#End Region

#Region "CRUD Base"

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MPoliza
                AsyncLoader(True)
                Dim result = Await Model.SavePoliza(Poliza, _idCurrentSequence)
                AsyncLoader(False)
                If result.StateResult = True Then

                    If Poliza.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(_idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf Poliza.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or Poliza.ChangeTracker.State = ObjectState.Unchanged Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Poliza = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    _searchMode = False
                    Me.Deshacer()
                Else
                    If result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        AsyncLoader(False)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        AsyncLoader(False)
                    End If
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try


        'If Not ValidateControls() Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
        '    Exit Sub
        'End If
        'AssigningValues()
        'Try
        '    Using Model As New MPoliza
        '        AsyncLoader(True)
        '        Dim result As ActionResult(Of Poliza) = Await Model.SavePoliza(Me.Poliza, Me._idCurrentSequence)
        '        If result.StatusCode = eStatusResult.SUCCESS Then
        '            If Poliza.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '                If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
        '                    Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
        '                End If
        '            End If
        '            Me.Poliza = result.ObjectEmbbeded
        '            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '            AsyncLoader(False)
        '            Deshacer()
        '        Else
        '            AsyncLoader(False)
        '            INDbteCode.Enabled = False
        '        End If
        '        ShowMessage(result.StatusCode) = result.Message
        '    End Using
        'Catch ex As Exception
        '    AsyncLoader(False)
        '    INDbteCode.Enabled = False
        '    Throw ex
        'End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        'If Poliza IsNot Nothing And Poliza.Id > 0 Then
        '    If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = Windows.Forms.DialogResult.Yes Then
        '        Try
        '            Using Model As New MPoliza
        '                AsyncLoader(True)
        '                Dim result = Await Model.DeletePoliza(Poliza)
        '                AsyncLoader(False)
        '                If result.StateResult = True Then
        '                    If Me._doc IsNot Nothing Then
        '                        Await Me.DeleteDocumentIndexed()
        '                    End If
        '                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
        '                    AsyncLoader(False)
        '                    _searchMode = False
        '                    Deshacer()
        '                Else
        '                    If result.MessageResult(0) = "-999" Then
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                        AsyncLoader(False)
        '                    ElseIf result.MessageResult(0) = "-000" Then
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
        '                        AsyncLoader(False)
        '                    Else
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                        AsyncLoader(False)
        '                    End If
        '                End If
        '            End Using
        '        Catch ex As Exception
        '            Throw ex
        '            AsyncLoader(False)
        '        End Try
        '    End If
        'End If

        If Me.Poliza IsNot Nothing AndAlso Me.Poliza.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MPoliza
                        AsyncLoader(True)
                        Dim result = Await Model.DeletePoliza(Me.Poliza)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
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
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        'CleanControls()
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewPoliza()
        End If
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo", .ColumnWidth = 100}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion", .ColumnWidth = 400}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Poliza
            .ValorSolicitado = "Codigo"
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            Await LoadControls()
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
        'If _searchMode = False Then
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'Else
        '    If indigo.UserViewMode = True Then
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        '    End If
        'End If
        'INDbteCode.Focus()
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

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
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.Poliza IsNot Nothing AndAlso Me.Poliza.Id > 0 Then
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

    Private Sub FrmPoliza_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
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
        'ActionsOnControls = False
        'INDbteCode.Text = String.Empty
        'INDtxtName.Text = String.Empty
        'INDglPolizaType.EditValue = Nothing
        'INDglAseguradora.EditValue = Nothing
        'INDdtFechaFinal.EditValue = Nothing
        'INDdtFechaInicio.EditValue = Nothing
        'INDmeObjeto.Text = String.Empty
        ' Poliza = Nothing
        'DeleteBlockedRecord()
        'Me._doc = Nothing
        'Me.BarraBotones.EnableBarItems()
        'Me.BarraBotones.DisableBarDocument()
        'Me.BarraBotones.StatusRecordVisible = False


        INDlyPoliza.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me._doc = Nothing
        Status = True
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDglPolizaType.EditValue = Nothing
        INDglAseguradora.EditValue = Nothing
        INDdtFechaFinal.EditValue = Nothing
        INDdtFechaInicio.EditValue = Nothing
        INDmeObjeto.Text = String.Empty
        'Limpiar controles
        Poliza = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyPoliza.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        'If Me.BarraBotones.PermiteConsultar = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '    Exit Function
        'End If

        'Using Model As New MPoliza
        '    AsyncLoader(True)
        '    Poliza = Await Model.GetPoliza(INDbteCode.Text)
        '    AsyncLoader(False)
        'End Using
        'If Poliza IsNot Nothing And Poliza.Id > 0 Then
        '    Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
        '    With Poliza
        '        CodePoliza = .Code
        '        NamePoliza = .Name
        '        IdInsurance = .InsuranceId
        '        InitialDate = .InitialDate
        '        EndDate = .EndDate
        '        IdPolizaType = .IdPolizaType
        '        Objects = .Object
        '        StatePoliza = .State
        '    End With
        '    BarraBotones.SetDocuments(Poliza.Id)
        '    ActionsOnControls = True
        '    INDbteCode.Focus()
        '    Me.GetDocumentIndexed(Me.Tag & "_" & Me.Poliza.Code)
        '    Using Model As New MBlockRecordAndSequenseMaintenance(Me.Tag)
        '        Dim result = Await Model.GetBlockRecord(Me.Tag, Me.Poliza.Id)
        '        If result IsNot Nothing AndAlso result.Id = 0 Then
        '            Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '            state.State = Domain.Base.Entities.ObjectState.Added
        '            record = New Domain.Entities.BlockRecordMaintenance With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Poliza.Id}
        '            Dim operation = Await Model.SaveBlockRecord(record)
        '            record = operation.ObjectEmbbeded
        '        Else
        '            record = result
        '            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '        End If
        '    End Using
        'Else
        '    'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '    'CodePoliza = String.Empty
        '    'INDbteCode.Focus()
        '    If Me._sequense.IsManual Then
        '        Me.NewPoliza()
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '        Me.CodePoliza = String.Empty
        '        INDbteCode.Focus()
        '    End If
        'End If


        If Not String.IsNullOrEmpty(CodePoliza) AndAlso Not String.IsNullOrWhiteSpace(CodePoliza) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MPoliza
                    AsyncLoader(True)
                    Poliza = Await Model.GetPoliza(INDbteCode.Text.Trim)
                    INDlyPoliza.BeginUpdate()
                    If Poliza IsNot Nothing AndAlso Poliza.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(Poliza.Id))
                            With Poliza
                                'LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                'Llenar Entidad
                                CodePoliza = .Code
                                NamePoliza = .Name
                                IdInsurance = .InsuranceId
                                InitialDate = .InitialDate
                                EndDate = .EndDate
                                IdPolizaType = .IdPolizaType
                                Objects = .Object
                                Status = .State
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Poliza.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New Domain.Entities.BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Poliza.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(Poliza.Id, Me.Tag.ToString(), Nothing, GetType(Poliza).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPoliza()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodePoliza = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlyPoliza.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
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
        If Object.Equals(INDglAseguradora.EditValue, Nothing) = True Then
            ValidateControls = False
        End If
        If INDdtFechaInicio.Text = String.Empty Then
            ValidateControls = False
        End If
        If Object.Equals(InitialDate, Nothing) = True Then
            ValidateControls = False
        End If
        If Object.Equals(EndDate, Nothing) = True Then
            ValidateControls = False
        End If
        If INDdtFechaFinal.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDglPolizaType.Text = String.Empty Then
            ValidateControls = False
        End If
        If Object.Equals(IdPolizaType, Nothing) = True Then
            ValidateControls = False
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Poliza
            .Code = CodePoliza
            .Name = NamePoliza
            .InsuranceId = IdInsurance
            .InitialDate = InitialDate
            .EndDate = EndDate
            .IdPolizaType = IdPolizaType
            .Object = Objects
            Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
                Case eActionsStatusRecords.Active
                    .State = True
                Case eActionsStatusRecords.Inactive
                    .State = False
            End Select
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
        ''If e.KeyCode = Keys.Enter Then
        ''    If String.IsNullOrEmpty(CodePoliza) Then
        ''        Me.NewPoliza()
        ''    Else
        ''        Await LoadControls()
        ''    End If
        ''End If
        'If e.KeyCode = Windows.Forms.Keys.Enter Then
        '    If Me._sequense.IsManual Then
        '        If Not String.IsNullOrEmpty(INDbteCode.Text) Then
        '            Await Me.LoadControls()
        '        End If
        '    Else
        '        If String.IsNullOrEmpty(INDbteCode.Text) Then
        '            Me.NewPoliza()
        '        Else
        '            Await Me.LoadControls()
        '        End If
        '    End If
        'End If


        If e.KeyCode = Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodePoliza.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodePoliza) Then
                    Await Me.NewPoliza()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
    ''' <summary>
    ''' Metodo que se utiliza para obtener la fecha de servidor 
    ''' </summary>
    Private Sub GetServerDate()
        ServerDate = CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDate()
    End Sub
    ''' <summary>
    ''' Evento para validar la fecha de inicio de la poliza
    ''' </summary>
    Private Sub INDdtFechaInicio_Validating(sender As Object, e As CancelEventArgs) Handles INDdtFechaInicio.Validating
        GetServerDate()
        If Date.Compare(INDdtFechaInicio.EditValue, ServerDate) > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "La fecha inicial no puede ser mayor a la actual"
            INDdtFechaInicio.Focus()
        End If
    End Sub
    ''' <summary>
    ''' Evento para validar la fecha de final de la poliza
    ''' </summary>
    Private Sub INDdtFechaFinal_Validating(sender As Object, e As CancelEventArgs) Handles INDdtFechaFinal.Validating
        If Date.Compare(INDdtFechaInicio.EditValue, INDdtFechaFinal.EditValue) > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "La fecha inicial no puede ser mayor a la fecha final"
            INDdtFechaFinal.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Poliza.Code, Me.Poliza.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & CStr(Me.Tag) & "_" & Me.Poliza.Code & "#$", .IdForm = CStr(Me.Tag), _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Poliza.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Poliza.Code, Me.Poliza.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Poliza.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        Using Model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewPoliza() As Task
        'Me.Poliza = New Domain.Maintenance.Entities.Poliza()
        'If Me._sequense.Scope IsNot Nothing Then
        '    If Me._sequense.IsManual Then
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    Else
        '        If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '            Me._idCurrentSequense = Me._sequense.MaintenanceSequenceDetail(0).Id
        '        ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '            If Me._sequense.MaintenanceSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '                Me._idCurrentSequense = Me._sequense.MaintenanceSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '            Else
        '                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '                Exit Sub
        '            End If
        '        End If
        '        If Not Me._sequense.Sequential Then
        '            If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '                If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                    Me.CodePoliza = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Using model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
        '                        Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
        '                    End Using
        '                    If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                        Me.CodePoliza = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                        Me.ActionsOnControls = True
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                    Else
        '                        Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
        '                    End If
        '                End If
        '            Else
        '                Me.CodePoliza = ResourceManager.GetString("LabelOrTextboxNew")
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            End If
        '        Else
        '            Me.CodePoliza = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    End If
        'Else
        '    Me.ActionsOnControls = False
        '    Mensaje(EeventViewerImages.Advertencia) = "Revise la Secuencia Numérica por favor. Comunicar con el Administrador."
        'End If
        'StatePoliza = True


        Poliza = New Poliza()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.CodePoliza = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodePoliza = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodePoliza = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodePoliza = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function
#End Region

#Region "Eventos Barra Botones"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
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
        _searchMode = False
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
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        'Try
        '    Using model As New MPoliza
        '        AsyncLoader(True)
        '        Dim Result As New ActionResult(Of Domain.Maintenance.Entities.Poliza)
        '        Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
        '            Case eActionsStatusRecords.Active
        '                Result = Await model.ChangeState(CodePoliza, True)
        '            Case eActionsStatusRecords.Inactive
        '                Result = Await model.ChangeState(CodePoliza, False)
        '        End Select
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            Poliza = Result.ObjectEmbbeded
        '            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        '        Else
        '            If Result.MessageResult(0) = ErrorConcurrencia Then
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '            End If
        '        End If
        '    End Using
        'Catch ex As Exception
        '    Throw ex
        '    AsyncLoader(False)
        'End Try


        If Not String.IsNullOrEmpty(Me.Poliza.Code) Then
            Try
                Using model As New MPoliza
                    Dim state As Boolean = Status = eActionsStatusRecords.Active
                    AsyncLoader(True)
                    Dim result As ActionResult(Of Poliza) = Await model.ChangeState(Me.Poliza.Code, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.Poliza = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDbteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub
    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MaintenanceSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Evento que abre el formulario de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDglAseguradora_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDglAseguradora.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New Presentation.Maintenance.FrmInsurance With {.ViewModeEditHold = True, .StartPosition = Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            InsuranceDataSource = Await Model.ListAllInsurance
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDglPolizaType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDglPolizaType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New Presentation.Maintenance.FrmPolizaType With {.ViewModeEditHold = True, .StartPosition = Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            PolizaTypeDataSource = Await Model.ListAllPolizaType
        End If
    End Sub

End Class