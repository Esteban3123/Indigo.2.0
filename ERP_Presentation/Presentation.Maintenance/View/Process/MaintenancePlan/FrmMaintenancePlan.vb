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
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Base.Entities
Imports System.Windows.Forms
Imports Presentation.Common
Imports Presentation.Common.MVP

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista para el funcional de responsables de mantenimiento
''' </summary>
Public Class FrmMaintenancePlan
    Implements IMaintenancePlan, ICustomizableForm


#Region "Constantes"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Maintenance"
#End Region

#Region "Variable Globales Propiedades Intefaz y Load"

    ''' <summary>
    ''' variable para saber cual es el numero de la fila que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Private rowEditing As Integer

    Dim FlagNew As Boolean = False

    Dim _RegimeTypeDataSource As List(Of Tuple(Of String, String))
    ReadOnly Property DatasourceRegimeType As List(Of Tuple(Of String, String))
        Get
            If _RegimeTypeDataSource Is Nothing Then
                _RegimeTypeDataSource = New List(Of Tuple(Of String, String))
                _RegimeTypeDataSource.Add(New Tuple(Of String, String)("1", "Fechas"))
                _RegimeTypeDataSource.Add(New Tuple(Of String, String)("2", "Lecturas"))
            End If
            Return _RegimeTypeDataSource
        End Get
    End Property



    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IMaintenancePlan.MyTag
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
    Public Property Sequense As Domain.Entities.MaintenanceSequence Implements IMaintenancePlan.Sequense
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
    Public Property CodePlanMaintenance As String Implements IMaintenancePlan.CodePlanMaintenance
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
    ''' Esta propiedad contiene el nombre del Plan de Mantenimiento
    ''' </summary>
    Public Property NamePlanMaintenance As String Implements IMaintenancePlan.NamePlanMaintenance
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    Public Property InitialDate As Date Implements IMaintenancePlan.InitialDate
        Get
            Return INDDeInitialDate.EditValue
        End Get
        Set(value As Date)
            INDDeInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el id del tipo de equipo
    ''' </summary>
    Public Property IdEquipmentType As Integer Implements IMaintenancePlan.IdEquipmentType
        Get
            Return INDglEquipmentType.EditValue
        End Get
        Set(value As Integer)
            INDglEquipmentType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el tipo de vinculacion 
    ''' </summary>
    Public Property IdMeasurementUnit As Integer Implements IMaintenancePlan.IdMeasurementUnit
        Get
            Return INDGlMeasurementUnit.EditValue
        End Get
        Set(value As Integer)
            INDGlMeasurementUnit.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga las sucursales
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MeasurmentUnitDataSource As List(Of MeasurementUnit) Implements IMaintenancePlan.MeasurmentUnitDataSource
        Set(value As List(Of MeasurementUnit))
            INDGlMeasurementUnit.Properties.DataSource = value
            INDGlMeasurementUnit.Properties.PopupFormWidth = INDGlMeasurementUnit.Width
        End Set
    End Property

    ''' <summary>
    ''' Lista los tipos de quipos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDglEquipmentType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDglEquipmentType.QueryPopUp
        If INDglEquipmentType.Properties.DataSource Is Nothing Then
            INDglEquipmentType.Properties.DataSource = Presenter.ListAllEquipment
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad contiene el estado del responsable
    ''' </summary>
    Public Property StateMaintenancePlan As Boolean Implements IMaintenancePlan.StateMaintenancePlan
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

    Public Property RegimeType As Byte Implements IMaintenancePlan.RegimeType
        Get
            Return INDGlRegimeType.EditValue
        End Get
        Set(value As Byte)
            INDGlRegimeType.EditValue = value
        End Set
    End Property


    Public WriteOnly Property ActionsOnControls As Boolean Implements IMaintenancePlan.ActionsOnControls
        Set(value As Boolean)
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDDeInitialDate.Enabled = value
            INDGlRegimeType.Enabled = value
            INDGlMeasurementUnit.Enabled = value
            INDglEquipmentType.Enabled = value
            BarraBotones.StatusRecordVisible = value
            If value Then
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
    Dim Model As New MMaintenancePlan
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PMaintenancePlan
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

    ''' <summary>
    ''' Variable que contiene el objeto torre
    ''' </summary>
    Dim MaintenancePlan As Domain.Entities.MaintenancePlan

    Dim MaintenancePlanDetail As Domain.Entities.MaintenancePlanDetail
    Dim ListMaintenancePlanDetail As List(Of Domain.Entities.MaintenancePlanDetail)

    Dim MaintenanceActivity As Domain.Entities.MaintenanceActivity
    Dim ListMaintenanceActivity As List(Of Domain.Entities.MaintenanceActivity)


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        rowEditing = Nothing
        _RegimeTypeDataSource = Nothing
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
        MaintenancePlan = Nothing
        MaintenancePlanDetail = Nothing
        ListMaintenancePlanDetail = Nothing
        MaintenanceActivity = Nothing
        ListMaintenanceActivity = Nothing
    End Sub


    Private Sub FrmResponsibles_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        DeleteBlockedRecord()
    End Sub


    Private Async Sub FrmMaintenancePlan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc

        Me.LoadStatus()
        Presenter = New PMaintenancePlan(Me)
        Presenter.GetSequense()
        'IndigoGridView1.MoreInfoColunmns(INDGvParts)
        AddActionsColumns()
        AddActionsColumnsGvDetail()

        INDGlRegimeType.Properties.DataSource = DatasourceRegimeType
        AsyncLoader(True)
        'EquipmentTypeDataSource = Await Model.ListAllEquipmentType
        MeasurmentUnitDataSource = Await Model.ListAllMeasurementUnit
        AsyncLoader(False)
        Deshacer()
        indigo.AuditMessageWcf.Company = indigo.TransactionalContainer
    End Sub
#End Region

#Region "CRUD Base"
    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MMaintenancePlan
                AsyncLoader(True)
                Dim result = Await Model.SaveMaintenancePlanAsync(MaintenancePlan, _idCurrentSequence)
                If result.StateResult = True Then
                    MaintenancePlan = result.ObjectEmbbeded
                    If MaintenancePlan.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Me.MaintenancePlan.Code)
                        End If
                    ElseIf MaintenancePlan.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or MaintenancePlan.ChangeTracker.State = ObjectState.Unchanged Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    'Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    If result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
            'Mensaje(EeventViewerImages.Informacion) = "Se ha guardado correctamente"
            'CleanControls()
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        Try
            If MaintenancePlan IsNot Nothing And MaintenancePlan.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MMaintenancePlan
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteMaintenancePlanAsync(MaintenancePlan)
                        AsyncLoader(False)
                        If result = True Then
                            If Me._doc IsNot Nothing Then
                                Me.DeleteDocumentIndexed()
                            End If
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            AsyncLoader(False)
                            Deshacer()
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                    End Using
                End If
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewMaintenancePlan()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Name", .ColumnWidth = 400}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListMaintenancePlan
            .ValorSolicitado = "Code"
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
            If Not INDbteCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
        CodePlanMaintenance = String.Empty
        NamePlanMaintenance = String.Empty
        InitialDate = New Date()
        RegimeType = Nothing
        IdMeasurementUnit = Nothing
        IdEquipmentType = Nothing
        INDDeInitialDate.Text = String.Empty

        MaintenancePlanDetail = Nothing
        MaintenanceActivity = Nothing
        MaintenancePlan = Nothing
        ListMaintenanceActivity = New List(Of MaintenanceActivity)
        ListMaintenancePlanDetail = New List(Of MaintenancePlanDetail)
        INDGcParts.DataSource = Nothing
        INDgcDetail.DataSource = Nothing

        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        FlagNew = False
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(CodePlanMaintenance) AndAlso Not String.IsNullOrWhiteSpace(CodePlanMaintenance) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MMaintenancePlan()
                    AsyncLoader(True)
                    MaintenancePlan = Await Model.GetMaintenancePlanAsync(INDbteCode.Text.Trim)
                    INDlyResponsible.BeginUpdate()
                    If MaintenancePlan IsNot Nothing AndAlso MaintenancePlan.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(MaintenancePlan.Id))
                            With MaintenancePlan
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                CodePlanMaintenance = .Code
                                NamePlanMaintenance = .Name
                                InitialDate = .InitialDate
                                RegimeType = .RegimeType
                                IdMeasurementUnit = .MeasurementUnitId
                                IdEquipmentType = .EquipmentTypeId
                                StateMaintenancePlan = .Status
                                If .MaintenancePlanDetail IsNot Nothing And .MaintenancePlanDetail.Count() > 0 Then
                                    INDGcParts.DataSource = .MaintenancePlanDetail
                                    For Each item In .MaintenancePlanDetail
                                        If item.MaintenanceActivity IsNot Nothing Then
                                            For Each item1 In item.MaintenanceActivity
                                                ListMaintenanceActivity.Add(item1)
                                            Next
                                        End If
                                    Next
                                End If
                            End With

                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.MaintenancePlan.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = MaintenancePlan.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(MaintenancePlan.Id, Me.Tag.ToString(), Nothing, GetType(MaintenancePlan).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewMaintenancePlan()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodePlanMaintenance = String.Empty
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
    End Function

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        'ValidateControls = True
        'If INDbteCode.Text = String.Empty Then
        '    ValidateControls = False
        'End If
        'If INDtxtName.Text = String.Empty Then
        '    ValidateControls = False
        'End If
        'If Object.Equals(TypeEntailment, Nothing) = True Then
        '    ValidateControls = False
        'ElseIf Not TypeEntailment > 0 Then
        '    ValidateControls = False
        'End If
        'If Object.Equals(ResponsibleType, Nothing) = True Then
        '    ValidateControls = False
        'ElseIf Not ResponsibleType > 0 Then
        '    ValidateControls = False
        'End If
        'If Object.Equals(IdCostCenter, Nothing) = True Then
        '    ValidateControls = False
        'ElseIf Not IdCostCenter > 0 Then
        '    ValidateControls = False
        'End If
        'If Object.Equals(IdBranch, Nothing) = True Then
        '    ValidateControls = False
        'ElseIf Not IdBranch > 0 Then
        '    ValidateControls = False
        'End If
        ValidateControls = True
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With MaintenancePlan
            .Code = CodePlanMaintenance
            .Name = NamePlanMaintenance
            .InitialDate = InitialDate
            .RegimeType = RegimeType
            .MeasurementUnitId = IdMeasurementUnit
            .EquipmentTypeId = IdEquipmentType

            For Each item In ListMaintenancePlanDetail
                .MaintenancePlanDetail.Add(item)
            Next

            Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
                Case eActionsStatusRecords.Active
                    .Status = 1
                Case eActionsStatusRecords.Inactive
                    .Status = 2
            End Select

            If .Id > 0 Then
                .MarkAsModified()
            End If


        End With
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.MaintenancePlan.Code, Me.MaintenancePlan.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.MaintenancePlan.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.MaintenancePlan.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.MaintenancePlan.Code, Me.MaintenancePlan.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.MaintenancePlan.Code)
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
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodePlanMaintenance.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodePlanMaintenance) Then
                    FlagNew = True
                    Await Me.NewMaintenancePlan()

                Else
                    FlagNew = False
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If


        'If e.KeyCode = Keys.Enter Then
        '    INDtxtName.Focus()
        '    If String.IsNullOrEmpty(CodePlanMaintenance) Then
        '        Me.NewMaintenancePlan()
        '        FlagNew = True
        '    Else
        '        Await LoadControls()
        '        FlagNew = False
        '    End If
        'End If

    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub

    Private Async Function NewMaintenancePlan() As Task
        Me.MaintenancePlan = New Domain.Entities.MaintenancePlan()
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
                Me.CodePlanMaintenance = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodePlanMaintenance = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
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
                            Me.CodePlanMaintenance = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodePlanMaintenance = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If


        'Me.MaintenancePlan = New Domain.Maintenance.Entities.MaintenancePlan()
        'If Me._sequence.Id <> 0 Then
        '    If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail(0).Id
        '    ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me._sequence.MaintenanceSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Sub
        '        End If
        '    End If
        '    If Not Me._sequence.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                Me.CodePlanMaintenance = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
        '                    Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
        '                End Using
        '                If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                    Me.CodePlanMaintenance = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.CodePlanMaintenance = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    Else
        '        Me.CodePlanMaintenance = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        '    INDbteCode.Enabled = False
        'Else
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '    INDbteCode.Enabled = True
        'End If
    End Function

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Responsible IsNot Nothing AndAlso Me.Responsible.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Deshacer()
                'CodeResponsible = Me.IdEntity.Trim()
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
        AsyncLoader(True)
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        AsyncLoader(False)
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
        NewMaintenancePlan()
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
        'Try
        '    Using model As New MResponsible
        '        AsyncLoader(True)
        '        Dim Result As New ActionResult(Of Domain.Maintenance.Entities.Responsible)
        '        Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
        '            'Case eActionsStatusRecords.Active
        '            '    Result = Await model.ChangeState(CodeResponsible, True)
        '            'Case eActionsStatusRecords.Inactive
        '            '    Result = Await model.ChangeState(CodeResponsible, False)
        '        End Select
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            Responsible = Result.ObjectEmbbeded
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
        '    AsyncLoader(False)
        '    Throw ex
        'End Try
    End Sub
#End Region

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Add)
        IndigoGridView1.SetListAcction(INDGvParts, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvParts.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub


    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumnsGvDetail()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView2.SetListAcction(INDgvMaintenanceActivity, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvMaintenanceActivity.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    'Private Async Sub INDglEquipmentType_EditValueChanged(sender As Object, e As EventArgs) Handles INDglEquipmentType.EditValueChanged
    '    If FlagNew = True Then
    '        'Using ModelEquipmentType As New MEquipamentType

    '        '    Dim EquipmentType = Await ModelEquipmentType.GetEquipamentTypeByIdAsync(IdEquipmentType)

    '        '    Dim EquipmentTypePartsAccesoriesConsumibles = EquipmentType.EquipmentTypePartsAccesoriesConsumibles.ToList()

    '        '    If EquipmentTypePartsAccesoriesConsumibles.Count() > 0 Then
    '        '        ListMaintenancePlanDetail = New List(Of MaintenancePlanDetail)
    '        '        For i As Integer = 0 To EquipmentTypePartsAccesoriesConsumibles.Count() - 1
    '        '            MaintenancePlanDetail = New MaintenancePlanDetail
    '        '            Dim ObjEquipmentTypePartsAccesoriesConsumibles As New EquipmentTypePartsAccesoriesConsumibles
    '        '            ObjEquipmentTypePartsAccesoriesConsumibles = EquipmentTypePartsAccesoriesConsumibles.Item(i)
    '        '            MaintenancePlanDetail.EquipmentTypePartsAccesoriesConsumibles = ObjEquipmentTypePartsAccesoriesConsumibles

    '        '            'MaintenancePlanDetail.EquipmentTypePartsAccesoriesConsumibles = EquipmentTypePartsAccesoriesConsumibles.Item(i)
    '        '            ListMaintenancePlanDetail.Add(MaintenancePlanDetail)
    '        '        Next

    '        '        INDGcParts.DataSource = ListMaintenancePlanDetail
    '        '    End If

    '        'End Using
    '    End If
    'End Sub

#Region "Click_ButtonAction"
    Private Async Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        ListMaintenanceActivity = New List(Of MaintenanceActivity)
        Dim IdParts = INDGvParts.GetFocusedRowCellValue("EquipmentTypePartsAccesoriesConsumibles.Id")
        Dim ObjMaintenanceActivity = New MaintenanceActivity()
        Me.Cursor = ChangeCursorIndigo()
        Using formulario As New FrmPopUpAddActivities
            formulario.Size = New Drawing.Size(973, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.VarTypeTipoRegimen = INDGlRegimeType.EditValue
            formulario.VarMetricUnit = INDGlMeasurementUnit.Text


            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default

            If transparent.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                If formulario.ValidateControls() = True Then


                    With ObjMaintenanceActivity
                        .Name = formulario.INDTxtActividad.EditValue
                        .Frequency = formulario.INDSpinFrequency.EditValue

                        If INDGlRegimeType.EditValue = "1" Then ' Fechas
                            .TimeUnit = formulario.IndGlTimeUnit.EditValue
                            .HandledControlTime = False
                        Else ' Lectura
                            .HandledControlTime = formulario.INDChkMaximum.EditValue
                            .MaxTime = CInt(formulario.IndSpinMaxtime.EditValue)
                            .MaxTimeUnit = formulario.INDGlMaximunTimeUnit.EditValue
                        End If
                        .Priority = formulario.INDGlPriority.EditValue
                        .NumberHours = formulario.INDspinPriorityhours.EditValue
                        .NumberMinutes = formulario.INDSpinPriorityMinutes.EditValue
                        .IsShutdown = formulario.INDChkRequired.EditValue
                        .ShutdownDays = CInt(formulario.INDspinShutdwonDays.EditValue)
                        .PredictiveMaintenance = formulario.INDGlPredictiveMaintenance.EditValue
                        .MeasurementUnitId = formulario.INDglUnitMetricId.EditValue
                        .MinValue = CInt(formulario.INDTxtEditMinimuValue.EditValue)
                        .MaxValue = CInt(formulario.INDTxtMaximunValue.EditValue)
                        .ActivityProcedure = formulario.INDMemoEditProcedure.EditValue

                    End With
                End If


                ListMaintenanceActivity.Add(ObjMaintenanceActivity)


                INDgcDetail.DataSource = ListMaintenanceActivity
                INDgcDetail.RefreshDataSource()

            End If

            With MaintenancePlan.MaintenancePlanDetail

                If MaintenancePlan.MaintenancePlanDetail.Count() > 0 Then
                    For Each item In ListMaintenanceActivity
                        If MaintenancePlan.MaintenancePlanDetail.Where(Function(x) x.EquipmentTypePartsAccesoriesConsumiblesId = IdParts).FirstOrDefault().Id = 0 Then
                            MaintenancePlan.MaintenancePlanDetail.Where(Function(x) x.EquipmentTypePartsAccesoriesConsumiblesId = IdParts).FirstOrDefault().ChangeTracker.State = Domain.Base.Entities.ObjectState.Added
                            MaintenancePlan.MaintenancePlanDetail.Where(Function(x) x.EquipmentTypePartsAccesoriesConsumiblesId = IdParts).FirstOrDefault().MaintenanceActivity.Add(item)
                        Else
                            MaintenancePlan.MaintenancePlanDetail.Where(Function(x) x.EquipmentTypePartsAccesoriesConsumiblesId = IdParts).FirstOrDefault().ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                            MaintenancePlan.MaintenancePlanDetail.Where(Function(x) x.EquipmentTypePartsAccesoriesConsumiblesId = IdParts).FirstOrDefault().MaintenanceActivity.Add(item)
                        End If
                    Next
                Else
                    Dim DetailAdd As MaintenancePlanDetail = INDGvParts.GetFocusedRow()

                    DetailAdd.MaintenanceActivity.Add(ObjMaintenanceActivity)

                End If



            End With


        End Using

    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        Dim DetailEditDelete As MaintenanceActivity = INDgvMaintenanceActivity.GetFocusedRow()
        Dim button = CType(sender, DevExpress.XtraEditors.SimpleButton)

        Me.Cursor = ChangeCursorIndigo()

        If button.Text = "Editar" Then
            Using formulario As New FrmPopUpAddActivities
                formulario.Size = New Drawing.Size(973, 700)
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                formulario.VarTypeTipoRegimen = INDGlRegimeType.EditValue
                formulario.VarMetricUnit = INDGlMeasurementUnit.Text

                formulario.INDTxtActividad.EditValue = DetailEditDelete.Name
                formulario.INDSpinFrequency.EditValue = DetailEditDelete.Frequency

                If INDGlRegimeType.EditValue = "1" Then ' Fechas
                    formulario.IndGlTimeUnit.EditValue = DetailEditDelete.TimeUnit
                    formulario.INDChkMaximum.EditValue = False
                Else
                    formulario.IndSpinMaxtime.EditValue = DetailEditDelete.MaxTime
                    formulario.INDChkMaximum.EditValue = DetailEditDelete.HandledControlTime
                    formulario.INDGlMaximunTimeUnit.EditValue = DetailEditDelete.MaxTimeUnit
                End If

                formulario.INDGlPriority.EditValue = DetailEditDelete.Priority
                formulario.INDspinPriorityhours.EditValue = DetailEditDelete.NumberHours
                formulario.INDSpinPriorityMinutes.EditValue = DetailEditDelete.NumberMinutes
                formulario.INDChkRequired.EditValue = DetailEditDelete.IsShutdown
                formulario.INDspinShutdwonDays.EditValue = DetailEditDelete.ShutdownDays
                formulario.INDGlPredictiveMaintenance.EditValue = DetailEditDelete.PredictiveMaintenance
                formulario.INDglUnitMetricId.EditValue = DetailEditDelete.MeasurementUnitId
                formulario.INDTxtEditMinimuValue.EditValue = DetailEditDelete.MinValue
                formulario.INDTxtMaximunValue.EditValue = DetailEditDelete.MaxValue
                formulario.INDMemoEditProcedure.EditValue = DetailEditDelete.ActivityProcedure

                Dim transparent = New Base.FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default

                If transparent.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    With DetailEditDelete

                        .Name = formulario.INDTxtActividad.EditValue
                        .Frequency = formulario.INDSpinFrequency.EditValue

                        If INDGlRegimeType.EditValue = "1" Then ' Fechas
                            .TimeUnit = formulario.IndGlTimeUnit.EditValue
                            .HandledControlTime = False
                        Else ' Lectura
                            .HandledControlTime = formulario.INDChkMaximum.EditValue
                            .MaxTime = CInt(formulario.IndSpinMaxtime.EditValue)
                            .MaxTimeUnit = formulario.INDGlMaximunTimeUnit.EditValue
                        End If
                        .Priority = formulario.INDGlPriority.EditValue
                        .NumberHours = formulario.INDspinPriorityhours.EditValue
                        .NumberMinutes = formulario.INDSpinPriorityMinutes.EditValue
                        .IsShutdown = formulario.INDChkRequired.EditValue
                        .ShutdownDays = CInt(formulario.INDspinShutdwonDays.EditValue)
                        .PredictiveMaintenance = formulario.INDGlPredictiveMaintenance.EditValue
                        .MeasurementUnitId = formulario.INDglUnitMetricId.EditValue
                        .MinValue = CInt(formulario.INDTxtEditMinimuValue.EditValue)
                        .MaxValue = CInt(formulario.INDTxtMaximunValue.EditValue)
                        .ActivityProcedure = formulario.INDMemoEditProcedure.EditValue
                    End With

                    ListMaintenanceActivity.Remove(DetailEditDelete)
                    ListMaintenanceActivity.Add(DetailEditDelete)

                    INDgcDetail.DataSource = ListMaintenanceActivity
                    INDgcDetail.RefreshDataSource()

                End If
            End Using
        Else
            ' Eliminar
            DetailEditDelete.ChangeTracker.State = ObjectState.Deleted
            ListMaintenanceActivity.Remove(DetailEditDelete)
            INDgcDetail.DataSource = ListMaintenanceActivity
            INDgcDetail.RefreshDataSource()
        End If

    End Sub
#End Region

    Private Sub RPIPPopupUpDetail_Click(sender As Object, e As EventArgs) Handles RPIPPopupUpDetail.Click
        Dim DetailShow = INDGvParts.GetFocusedRow()
        AssignValues(DetailShow)
    End Sub
    Private Sub AssignValues(ByVal DetailShow As MaintenancePlanDetail)

        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = DetailShow.MaintenanceActivity
        HideColumns()
    End Sub

    Private Sub HideColumns()
        If INDGlRegimeType.EditValue = "1" Then 'Fechas
            INDgvMaintenanceActivity.Columns(2).Visible = False

        Else 'Lectura
            INDgvMaintenanceActivity.Columns(2).Visible = True

        End If

    End Sub


    Private Sub INDgvMaintenanceActivity_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDgvMaintenanceActivity.CustomDrawCell
        Dim currentView As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        If e.Column.Name = "INDColTimeUnit" Then
            Dim Value As Integer = CType(currentView.GetRowCellValue(e.RowHandle, currentView.Columns("U. de Tiempo")), Integer)

            Select Case Value
                Case 1
                    e.DisplayText = "Días"
                Case 2
                    e.DisplayText = "Semanas"
                Case 3
                    e.DisplayText = "Meses"
                Case 4
                    e.DisplayText = "Años"
                Case 5
                    e.DisplayText = "Lunes"
                Case 6
                    e.DisplayText = "Martes"
                Case 7
                    e.DisplayText = "Miércoles"
                Case 8
                    e.DisplayText = "Jueves"
                Case 9
                    e.DisplayText = "Viernes"
                Case 10
                    e.DisplayText = "Sábado"
                Case 11
                    e.DisplayText = "Domingo"

            End Select

        End If
    End Sub
End Class