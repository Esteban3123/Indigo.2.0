'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Julian Andres Cardozo Flores
' Created          : 05-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.ComponentModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista en el frontal de tipos de poliza
''' </summary>
Public Class FrmFixedAssetPolizaType
    Implements IFixedAssetPolizaType


#Region "Constantes"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"
#End Region

#Region "Variable Globales Propiedades Intefaz y Load"
    ''' <summary>
    ''' Gets my tag.
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As Object Implements IFixedAssetPolizaType.MyTag
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
    Public Property Sequense As Domain.Entities.FixedAssetSequence Implements IFixedAssetPolizaType.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.FixedAssetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.FixedAssetSequenceDetail In Me._sequence.FixedAssetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Private record As Domain.Entities.BlockRecordFixedAsset

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.FixedAssetSequence

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

    Dim dtFieldsCustomizables As DataTable
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Esta propiedad contiene el estado del tipo de poliza
    ''' </summary>
    Public Property Status As Boolean Implements IFixedAssetPolizaType.StatePolizaType
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = False Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            End If
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetPolizaType.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el nombre del tipo de poliza
    ''' </summary>
    Public Property NamePolizaType As String Implements IFixedAssetPolizaType.NamePolizaType
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el codigo del tipo de poliza
    ''' </summary>
    Public Property CodePolizaType As String Implements IFixedAssetPolizaType.CodePolizaType
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Variable que contiene el parentesco
    ''' </summary>
    Dim PolizaType As FixedAssetPolicyType
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MFixedAssetPolizaType
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PFixedAssetPolizaType

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _searchMode = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        PolizaType = Nothing
        Model = Nothing
        Presenter = Nothing
    End Sub


    Private Sub FrmSpecificConcepts_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetPolizaType(Me)
        Presenter.GetSequense()
        Presenter.Initializes()
        'Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

#End Region

#Region "CRUD Base"

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        'If ValidateControls() = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
        '    Exit Sub
        'End If
        'AssigningValues()
        'Try
        '    Using Model As New MFixedAssetPolizaType
        '        AsyncLoader(True)
        '        Dim result = Await Model.SavePolizaTypeAsync(PolizaType, _idCurrentSequense)
        '        AsyncLoader(False)
        '        If result.StateResult = True Then
        '            PolizaType = result.ObjectEmbbeded
        '            If PolizaType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '                'Se descarta la secuencia numerica usada
        '                If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
        '                    If Me.DicSequense(Me._sequense.FixedAssetSequenceDetail(0).Id).Count > 0 Then
        '                        Me.DicSequense(Me._sequense.FixedAssetSequenceDetail(0).Id).RemoveAt(0)
        '                    End If
        '                End If
        '                If Me._sequense.Sequential Then
        '                    Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Me.PolizaType.Code)
        '                End If
        '            ElseIf PolizaType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or PolizaType.ChangeTracker.State = ObjectState.Unchanged Then
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
        '            End If
        '            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '            _searchMode = False
        '            Me.Deshacer()
        '        Else
        '            If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = ErrorConcurrencia Then
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                AsyncLoader(False)
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                AsyncLoader(False)
        '            End If
        '        End If
        '    End Using
        'Catch ex As Exception
        '    Throw ex
        '    AsyncLoader(False)
        'End Try


        If Not ValidateControls() Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MFixedAssetPolizaType
                AsyncLoader(True)
                Dim result As ActionResult(Of FixedAssetPolicyType) = Await Model.SavePolizaTypeAsync(Me.PolizaType, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If PolizaType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.PolizaType = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        'If PolizaType IsNot Nothing And PolizaType.Id > 0 Then
        '    If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Try
        '            Using Model As New MFixedAssetPolizaType
        '                AsyncLoader(True)
        '                Dim result = Await Model.DeletePolizaTypeAsync(PolizaType)
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

        If Me.PolizaType IsNot Nothing AndAlso Me.PolizaType.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MFixedAssetPolizaType
                        AsyncLoader(True)
                        Dim result = Await Model.DeletePolizaTypeAsync(Me.PolizaType)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
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
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewPolizaType()
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Name", .ColumnWidth = 400}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPolizaType
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
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
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
        'ActionsOnControls = False
        'INDTxtName.Text = String.Empty
        'INDBteCode.Text = String.Empty
        'Me.BarraBotones.EnableBarItems()
        'Me.BarraBotones.DisableBarDocument()
        'DeleteBlockedRecord()
        'Me._doc = Nothing
        ' Me.BarraBotones.StatusRecordVisible = False

        INDLyCtrPolizaTypes.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        ActionsOnControls = False
        INDTxtName.Text = String.Empty
        INDBteCode.Text = String.Empty
        'Limpiar controles
        PolizaType = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLyCtrPolizaTypes.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetPolizaType.ActionsOnControls
        Set(value As Boolean)
            INDLyCtrPolizaTypes.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDLyCtrPolizaTypes.EndUpdate()
            ' BarraBotones.StatusRecordVisible = value
            If value = True Then
                INDTxtName.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        'If Me.BarraBotones.PermiteConsultar = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '    Exit Function
        'End If
        'AsyncLoader(True)
        'Using Model As New MFixedAssetPolizaType
        '    PolizaType = Await Model.GetPolizaTypeAsync(INDBteCode.Text)
        'End Using

        'If PolizaType IsNot Nothing And PolizaType.Id > 0 Then
        '    Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
        '    With PolizaType
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '        CodePolizaType = .Code
        '        NamePolizaType = .Name
        '        StatePolizaType = .Status
        '    End With
        '    BarraBotones.SetDocuments(PolizaType.Id)
        '    ActionsOnControls = True
        '    INDBteCode.Focus()
        '    Me.GetDocumentIndexed(Me.Tag & "_" & Me.PolizaType.Code)
        '    Using Model As New MBlockRecordAndSequenceFixedAsset(Me.Tag)
        '        Dim result = Await Model.GetBlockRecord(Me.Tag, Me.PolizaType.Id)
        '        If result IsNot Nothing AndAlso result.Id = 0 Then
        '            Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '            state.State = Domain.Base.Entities.ObjectState.Added
        '            record = New Domain.Entities.BlockRecordFixedAsset With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = PolizaType.Id}
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
        '    'CodePolizaType = String.Empty
        '    'INDBteCode.Focus()
        '    PolizaType = New FixedAssetPolicyType
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        'End If
        'AsyncLoader(False)
        'ActionsOnControls = True


        If Not String.IsNullOrEmpty(CodePolizaType) AndAlso Not String.IsNullOrWhiteSpace(CodePolizaType) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MFixedAssetPolizaType
                    AsyncLoader(True)
                    PolizaType = Await Model.GetPolizaTypeAsync(INDBteCode.Text)
                    INDLyCtrPolizaTypes.BeginUpdate()
                    If PolizaType IsNot Nothing AndAlso PolizaType.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(PolizaType.Id))
                            With PolizaType
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                CodePolizaType = .Code
                                NamePolizaType = .Name
                                Status = .Status
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.PolizaType.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = PolizaType.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(PolizaType.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetPolicyType).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPolizaType()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodePolizaType = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLyCtrPolizaTypes.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
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
        If INDBteCode.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDTxtName.Text = String.Empty Then
            ValidateControls = False
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With PolizaType
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Name = NamePolizaType
            .Code = CodePolizaType
        End With
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDBteCodeKindship_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '    If Me._sequense.IsManual Then
        '        If Not String.IsNullOrEmpty(INDBteCode.Text) Then
        '            Await Me.LoadControls()
        '        End If
        '    Else
        '        If String.IsNullOrEmpty(INDBteCode.Text) Then
        '            Me.NewPolizaType()
        '        Else
        '            Await Me.LoadControls()
        '        End If
        '    End If
        'End If


        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodePolizaType.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodePolizaType) Then
                    Await Me.NewPolizaType()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), Me.PolizaType.Code, Me.PolizaType.Name, INDTxtName.Text), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me.PolizaType.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), Me.PolizaType.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), Me.PolizaType.Code, Me.PolizaType.Name, INDTxtName.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), Me.PolizaType.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        Using Model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
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
    Private Async Function NewPolizaType() As Task
        'Me.PolizaType = New Domain.Entities.FixedAssetPolicyType()
        'If Me._sequense.Scope IsNot Nothing Then
        '    If Me._sequense.IsManual Then
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    Else
        '        If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '            Me._idCurrentSequense = Me._sequense.FixedAssetSequenceDetail(0).Id
        '        ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '            If Me._sequense.FixedAssetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '                Me._idCurrentSequense = Me._sequense.FixedAssetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '            Else
        '                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '                Exit Sub
        '            End If
        '        End If
        '        If Not Me._sequense.Sequential Then
        '            If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '                If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                    Me.CodePolizaType = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
        '                        Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
        '                    End Using
        '                    If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                        Me.CodePolizaType = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                        Me.ActionsOnControls = True
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                    Else
        '                        Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
        '                    End If
        '                End If
        '            Else
        '                Me.CodePolizaType = ResourceManager.GetString("LabelOrTextboxNew")
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            End If
        '        Else
        '            Me.CodePolizaType = ResourceManager.GetString("LabelOrTextboxNew")
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


        PolizaType = New FixedAssetPolicyType() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.CodePolizaType = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodePolizaType = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodePolizaType = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodePolizaType = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If


    End Function

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.PolizaType IsNot Nothing AndAlso Me.PolizaType.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
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

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        If Not String.IsNullOrEmpty(Me.PolizaType.Code) Then
            Try
                Using model As New MFixedAssetPolizaType
                    AsyncLoader(True)
                    Dim state As Boolean = Not PolizaType.Status
                    Dim result As ActionResult(Of FixedAssetPolicyType) = Await model.ChangeState(Me.PolizaType.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.PolizaType = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDBteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub
    Private Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.FixedAssetSequenceDetail IsNot Nothing Then
                If Not Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub


    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region



End Class