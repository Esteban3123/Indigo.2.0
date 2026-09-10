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
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Base.Entities
Imports System.Windows.Forms
Imports Presentation.Common
Imports Presentation.Common.MVP

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista para el funcional de responsables de mantenimiento
''' </summary>
Public Class FrmResponsibles
    Implements IResponsible, ICustomizableForm

#Region "Constantes"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Maintenance"
#End Region

#Region "Variable Globales Propiedades Intefaz y Load"

    Dim _jobbondingtype As List(Of Tuple(Of String, String))
    ReadOnly Property DatasourceJobBondingType As List(Of Tuple(Of String, String))
        Get
            If _jobbondingtype Is Nothing Then
                _jobbondingtype = New List(Of Tuple(Of String, String))
                _jobbondingtype.Add(New Tuple(Of String, String)("1", ResourceManager.GetString("Planta", NAME_MODULE)))
                _jobbondingtype.Add(New Tuple(Of String, String)("2", ResourceManager.GetString("Contratista", NAME_MODULE)))
                _jobbondingtype.Add(New Tuple(Of String, String)("3", ResourceManager.GetString("Paciente", NAME_MODULE)))
                _jobbondingtype.Add(New Tuple(Of String, String)("4", ResourceManager.GetString("Tercero", NAME_MODULE)))
            End If
            Return _jobbondingtype
        End Get
    End Property
    Dim _responsibleType As List(Of Tuple(Of String, String))
    ReadOnly Property DatasourceResponsibleType As List(Of Tuple(Of String, String))
        Get
            If _responsibleType Is Nothing Then
                _responsibleType = New List(Of Tuple(Of String, String))
                _responsibleType.Add(New Tuple(Of String, String)("1", ResourceManager.GetString("Administrativo", NAME_MODULE)))
                _responsibleType.Add(New Tuple(Of String, String)("2", ResourceManager.GetString("Asistencial", NAME_MODULE)))
            End If
            Return _responsibleType
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IResponsible.MyTag
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
    Public Property Sequense As Domain.Entities.MaintenanceSequence Implements IResponsible.Sequense
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
    ''' Esta propiedad contiene el codigo del responsable
    ''' </summary>
    Public Property CodeResponsible As String Implements IResponsible.CodeResponsible
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
    ''' Esta propiedad contiene el nombre del responsable
    ''' </summary>
    Public Property NameResponsible As String Implements IResponsible.NameResponsible
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el id de la sucursal
    ''' </summary>
    Public Property IdBranch As Integer Implements IResponsible.IdBranch
        Get
            Return INDglBranch.EditValue
        End Get
        Set(value As Integer)
            INDglBranch.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el id del centro de costo 
    ''' </summary>
    Public Property IdCostCenter As Integer Implements IResponsible.IdCostCenter
        Get
            Return INDglCostCenter.EditValue
        End Get
        Set(value As Integer)
            INDglCostCenter.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el tipo de responsable
    ''' </summary>
    Public Property ResponsibleType As String Implements IResponsible.ResponsibleType
        Get
            Return INDgleTypeResponsable.EditValue
        End Get
        Set(value As String)
            INDgleTypeResponsable.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el tipo de vinculacion 
    ''' </summary>
    Public Property TypeEntailment As String Implements IResponsible.TypeEntailment
        Get
            Return INDgleTypeEntailment.EditValue
        End Get
        Set(value As String)
            INDgleTypeEntailment.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga las sucursales
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property BranchDataSource As List(Of Branch) Implements IResponsible.BranchDataSource
        Set(value As List(Of Branch))
            INDglBranch.Properties.DataSource = value
            INDglBranch.Properties.PopupFormWidth = INDglBranch.Width
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga las ciudades
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property CostCenterDataSource As List(Of Domain.Maintenance.Entities.CostCenter) Implements IResponsible.CostCenterDataSource
        Set(value As List(Of Domain.Maintenance.Entities.CostCenter))
            INDglCostCenter.Properties.DataSource = value
            INDglCostCenter.Properties.PopupFormWidth = INDglCostCenter.Width
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el estado del responsable
    ''' </summary>
    Public Property StateResponsible As Boolean Implements IResponsible.StateResponsible
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

    Public WriteOnly Property ActionsOnControls As Boolean Implements IResponsible.ActionsOnControls
        Set(value As Boolean)
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDgleTypeEntailment.Enabled = value
            INDgleTypeResponsable.Enabled = value
            INDglBranch.Enabled = value
            INDglCostCenter.Enabled = value
            BarraBotones.StatusRecordVisible = value
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
    Dim Responsible As Responsible
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MResponsible
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PResponsible
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


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        IndigoManagementExceptions = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Responsible = Nothing
        Model = Nothing
        Presenter = Nothing
        ModelDepartment = Nothing
        ModelCity = Nothing
        record = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
    End Sub

    Private Sub FrmResponsibles_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        DeleteBlockedRecord()
    End Sub
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrMResponsible_Load(sender As Object, e As EventArgs) Handles Me.Load
        INDgleTypeEntailment.Properties.DataSource = DatasourceJobBondingType
        INDgleTypeResponsable.Properties.DataSource = DatasourceResponsibleType
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.LoadStatus()
        Presenter = New PResponsible(Me)
        Presenter.GetSequense()
        ModelDepartment = New Presentation.Common.MVP.MDepartaments(Presentation.Common.MVP.MDepartaments.TAG)
        BranchDataSource = Await Model.ListAllBranch
        CostCenterDataSource = Await Model.ListAllCostCenter
        Dim ModelCostCenter As New Presentation.Maintenance.MVP.MCostCenter(Me.Tag)
        'CostCenterDataSource = Await ModelCostCenter.ListAllCostCenterAsync
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
            Using Model As New MResponsible
                AsyncLoader(True)
                Dim result = Await Model.SaveResponsibleAsync(Responsible, _idCurrentSequence)
                If result.StateResult = True Then
                    If Responsible.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf Responsible.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or Responsible.ChangeTracker.State = ObjectState.Unchanged Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Responsible = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
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
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Responsible IsNot Nothing And Responsible.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MResponsible
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteResponsibleAsync(Responsible)
                        If result.StateResult = True Then
                            Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            AsyncLoader(False)
                            Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                            If result.MessageResult(0) = "-999" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                                AsyncLoader(False)
                            ElseIf result.MessageResult(0) = "-000" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                                AsyncLoader(False)
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                                AsyncLoader(False)
                            End If
                        End If
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
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewResponsible()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo", .ColumnWidth = 100}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion", .ColumnWidth = 400}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ResponsibleMaintenance
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
        INDbteCode.Text = ReturnValue
        DeleteBlockedRecord()
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
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        TypeEntailment = Nothing
        ResponsibleType = Nothing
        IdBranch = Nothing
        IdCostCenter = Nothing
        Responsible = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(CodeResponsible) AndAlso Not String.IsNullOrWhiteSpace(CodeResponsible) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MResponsible()
                    AsyncLoader(True)
                    Responsible = Await Model.GetResponsibleAsync(INDbteCode.Text.Trim)
                    INDlyResponsible.BeginUpdate()
                    If Responsible IsNot Nothing AndAlso Responsible.Id > 0 Then

                        Using ModelRecord As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(Responsible.Id))
                            With Responsible
                                CodeResponsible = .Code
                                NameResponsible = .Name
                                TypeEntailment = .TypeEntailment
                                ResponsibleType = .TypeResponsible
                                IdBranch = .IdBranch
                                IdCostCenter = .IdCostCenter
                                StateResponsible = .State
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Responsible.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New Domain.Entities.BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Responsible.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(Responsible.Id, Me.Tag.ToString(), Nothing, GetType(Responsible).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewResponsible()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodeResponsible = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlyResponsible.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If


        'If Me.BarraBotones.PermiteConsultar = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '    Exit Function
        'End If
        'Using Model As New MResponsible
        '    Responsible = Await Model.GetResponsibleAsync(INDbteCode.Text)
        'End Using
        'If Responsible IsNot Nothing And Responsible.Id > 0 Then
        '    Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
        '    With Responsible
        '        CodeResponsible = .Code
        '        NameResponsible = .Name
        '        TypeEntailment = .TypeEntailment
        '        ResponsibleType = .TypeResponsible
        '        IdBranch = .IdBranch
        '        IdCostCenter = .IdCostCenter
        '        StateResponsible = .State
        '    End With
        '    BarraBotones.SetDocuments(Responsible.Id)
        '    ActionsOnControls = True
        '    INDbteCode.Focus()
        '    Me.GetDocumentIndexed(Me.Tag & "_" & Me.Responsible.Code)
        '    Using Model As New MBlockRecordAndSequenseMaintenance(Me.Tag)
        '        Dim result = Await Model.GetBlockRecord(Me.Tag, Me.Responsible.Id)
        '        If result IsNot Nothing AndAlso result.Id = 0 Then
        '            Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '            state.State = Domain.Base.Entities.ObjectState.Added
        '            record = New Domain.Entities.BlockRecordMaintenance With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Responsible.Id}
        '            Dim operation = Await Model.SaveBlockRecord(record)
        '            record = operation.ObjectEmbbeded
        '        Else
        '            record = result
        '            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '        End If
        '    End Using
        'Else
        '    Responsible = New Responsible
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        'End If
        'ActionsOnControls = True
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
        If Object.Equals(TypeEntailment, Nothing) = True Then
            ValidateControls = False
        ElseIf Not TypeEntailment > 0 Then
            ValidateControls = False
        End If
        If Object.Equals(ResponsibleType, Nothing) = True Then
            ValidateControls = False
        ElseIf Not ResponsibleType > 0 Then
            ValidateControls = False
        End If
        If Object.Equals(IdCostCenter, Nothing) = True Then
            ValidateControls = False
        ElseIf Not IdCostCenter > 0 Then
            ValidateControls = False
        End If
        If Object.Equals(IdBranch, Nothing) = True Then
            ValidateControls = False
        ElseIf Not IdBranch > 0 Then
            ValidateControls = False
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Responsible
            .Code = CodeResponsible
            .Name = NameResponsible
            .IdBranch = IdBranch
            .IdCostCenter = IdCostCenter
            .TypeResponsible = ResponsibleType
            .TypeEntailment = TypeEntailment
            Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
                Case eActionsStatusRecords.Active
                    .State = True
                Case eActionsStatusRecords.Inactive
                    .State = False
            End Select
        End With
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Responsible.Code, Me.Responsible.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.Responsible.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Responsible.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Responsible.Code, Me.Responsible.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Responsible.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewResponsible() As Task
        'Me.Responsible = New Domain.Maintenance.Entities.Responsible()
        'With Responsible
        '    .Code = INDbteCode.EditValue
        '    .Name = INDtxtName.EditValue
        '    .TypeEntailment = INDgleTypeEntailment.EditValue
        '    .IdBranch = INDglBranch.EditValue
        '    .IdCostCenter = INDglCostCenter.EditValue
        '    .TypeResponsible = INDgleTypeResponsable.EditValue
        'End With
        Me.Responsible = New Domain.Maintenance.Entities.Responsible()
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
                Me.CodeResponsible = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodeResponsible = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
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
                            Me.CodeResponsible = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodeResponsible = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If

        'If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '    Me._idCurrentSequense = Me._sequense.MaintenanceSequenceDetail(0).Id
        'ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '    If Me._sequense.MaintenanceSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '        Me._idCurrentSequense = Me._sequense.MaintenanceSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '    Else
        '        Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '        Exit Sub
        '    End If
        'End If
        'If Not Me._sequense.Sequential Then
        '    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '        If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '            Me.CodeResponsible = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        Else
        '            Using model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
        '                Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
        '            End Using
        '            If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                Me.CodeResponsible = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
        '            End If
        '        End If
        '    Else
        '        Me.CodeResponsible = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        'Else
        '    Me.CodeResponsible = ResourceManager.GetString("LabelOrTextboxNew")
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'End If
    End Function

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodeResponsible.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodeResponsible) Then
                    Await Me.NewResponsible()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
        'If e.KeyCode = Windows.Forms.Keys.Enter Then
        '    If INDbteCode.Text = "" Then
        '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
        '        Exit Sub
        '    End If
        '    Await LoadControls()
        '    If INDbteCode.Enabled = False Then
        '        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        '        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
        '    End If
        '    'INDBteCode.Enabled = False
        'End If
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Responsible IsNot Nothing AndAlso Me.Responsible.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = Windows.Forms.DialogResult.Yes Then
                Deshacer()
                CodeResponsible = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
        If INDbteCode.Enabled = False Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
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
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
        NewResponsible()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
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

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Try
            Using model As New MResponsible
                AsyncLoader(True)
                Dim Result As New ActionResult(Of Domain.Maintenance.Entities.Responsible)
                Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
                    Case eActionsStatusRecords.Active
                        Result = Await model.ChangeState(CodeResponsible, True)
                    Case eActionsStatusRecords.Inactive
                        Result = Await model.ChangeState(CodeResponsible, False)
                End Select
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Responsible = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                Else
                    INDbteCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub
#End Region


End Class