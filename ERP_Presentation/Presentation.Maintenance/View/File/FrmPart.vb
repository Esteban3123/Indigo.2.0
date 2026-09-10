'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Julian Andres Cardozo Flores
' Created          : 03-09-2013
'
' Last Modified By : Carlos Ernesto Cordoba
' Last Modified On : 05-06-2014
' Description      : implementacion de la secuencia numerica y la indexacion
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Maintenance.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Maintenance.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports System.Windows.Forms

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista en el frontal de partes
''' </summary>
Public Class FrmPart
    Implements IPart

#Region "Constantes"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Maintenance"
#End Region

#Region "Propiedades Intefaz"

    Dim dtFieldsCustomizables As DataTable
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Esta propiedad contiene el estado de la parte
    ''' </summary>
    Public Property Status As Boolean Implements IPart.StatePart
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
    ''' Esta propiedad contiene id del equipo seleccionado
    ''' </summary>
    Public Property IdEquipment As Integer Implements IPart.IdEquipment
        Get
            Return INDglEquipmentType.EditValue
        End Get
        Set(value As Integer)
            INDglEquipmentType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el nombre de la parte
    ''' </summary>
    Public Property NamePart As String Implements IPart.NamePart
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property

    Public WriteOnly Property EquipmentDataSource As List(Of Domain.Maintenance.Entities.EquipmentType) Implements IPart.EquipmentDataSource
        Set(value As List(Of Domain.Maintenance.Entities.EquipmentType))
            INDglEquipmentType.Properties.DataSource = value
            INDglEquipmentType.Properties.PopupFormWidth = INDglEquipmentType.Width
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el codigo de la parte
    ''' </summary>
    Public Property CodePart As String Implements IPart.CodePart
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
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPart.ActionsOnControls
        Set(value As Boolean)
            INDLyCtrPart.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDglEquipmentType.Enabled = value
            'BarraBotones.StatusRecordVisible = value
            INDbtnAgregar.Enabled = value
            INDLyCtrPart.EndUpdate()
            If value = True Then
                INDTxtName.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IPart.MyTag
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
    Public Property Sequense As MaintenanceSequence Implements IPart.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As MaintenanceSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MaintenanceSequenceDetail In Me._sequence.MaintenanceSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property
#End Region

#Region "Variables Globales"
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Variable que contiene el objeto parte
    ''' </summary>
    Dim Part As Domain.Maintenance.Entities.Part
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MPart
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PPart
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' variable que contiene el listado de equipos agregados
    ''' </summary>
    Dim EquipmentTypeList As New List(Of Domain.Maintenance.Entities.EquipmentType)
    ''' <summary>
    ''' variable que contiene el listado de los registro eliminados
    ''' </summary>
    Dim DeletedList As New List(Of Domain.Maintenance.Entities.PartDetail)
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordMaintenance

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
#End Region

#Region "CRUD Base"
    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If

        If DeletedList.Count > 0 Then
            Part.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            For i As Integer = 0 To DeletedList.Count - 1
                If DeletedList.Item(i).Id <> 0 Then
                    DeletedList.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    Part.PartDetail.Add(DeletedList.Item(i))
                End If
            Next
        End If
        AssigningValues()
        Try
            Using Model As New MPart
                AsyncLoader(True)
                Dim result = Await Model.SavePartAsync(Part, _idCurrentSequence)
                AsyncLoader(False)
                If result.StateResult = True Then
                    If Part.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                        End If
                    ElseIf Part.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or Part.ChangeTracker.State = ObjectState.Unchanged Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Part = result.ObjectEmbbeded
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
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Part IsNot Nothing And Part.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MPart
                        AsyncLoader(True)
                        Dim result = Await Model.DeletePartAsync(Part)
                        If result.StateResult = True Then
                            If Me._doc IsNot Nothing Then
                                Await Me.DeleteDocumentIndexed()
                            End If
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            AsyncLoader(False)
                            _searchMode = False
                            Deshacer()
                        Else
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
            Await Me.NewPart()
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
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Part
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
        'If _searchMode = False Then
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'Else
        '    If indigo.UserViewMode = True Then
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        '    End If
        'End If
        'INDBteCode.Focus()
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

#Region "Metodos"

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
        'INDglEquipmentType.EditValue = Nothing
        'INDgcListEquipmentType.DataSource = Nothing
        'EquipmentTypeList.Clear()
        'DeletedList.Clear()
        'DeleteBlockedRecord()
        ' Me._doc = Nothing
        'Me.BarraBotones.EnableBarItems()
        'Me.BarraBotones.DisableBarDocument()
        'Me.BarraBotones.StatusRecordVisible = False
        'Part = Nothing


        INDLyCtrPart.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me._doc = Nothing
        LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Status = True
        INDTxtName.Text = String.Empty
        INDBteCode.Text = String.Empty
        INDglEquipmentType.EditValue = Nothing
        INDgcListEquipmentType.DataSource = Nothing
        EquipmentTypeList.Clear()
        DeletedList.Clear()
        'Limpiar controles
        Part = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLyCtrPart.EndUpdate()
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

        'Using Model As New MPart
        '    Part = Await Model.GetPartAsync(INDBteCode.Text)
        'End Using

        'If Part IsNot Nothing And Part.Id > 0 Then
        '    Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
        '    With Part
        '        CodePart = .Code
        '        NamePart = .Name
        '        For i As Integer = 0 To .PartDetail.ToList.Count - 1
        '            EquipmentTypeList.Add(.PartDetail.ToList.Item(i).EquipmentType)
        '        Next
        '        INDgcListEquipmentType.DataSource = EquipmentTypeList
        '        StatePart = .State
        '    End With
        '    BarraBotones.SetDocuments(Part.Id)
        '    ActionsOnControls = True
        '    INDBteCode.Focus()
        '    Me.GetDocumentIndexed(Me.Tag & "_" & Me.Part.Code)
        '    Using Model As New MBlockRecordAndSequenseMaintenance(Me.Tag)
        '        Dim result = Await Model.GetBlockRecord(Me.Tag, Me.Part.Id)
        '        If result IsNot Nothing AndAlso result.Id = 0 Then
        '            Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '            state.State = Domain.Base.Entities.ObjectState.Added
        '            record = New BlockRecordMaintenance With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Part.Id}
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
        '    'CodePart = String.Empty
        '    'INDBteCode.Focus()
        '    If Me._sequense.IsManual Then
        '        Me.NewPart()
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '        INDBteCode.Text = String.Empty
        '        INDBteCode.Focus()
        '    End If
        'End If



        If Not String.IsNullOrEmpty(CodePart) AndAlso Not String.IsNullOrWhiteSpace(CodePart) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MPart
                    AsyncLoader(True)
                    Part = Await Model.GetPartAsync(INDBteCode.Text)
                    INDLyCtrPart.BeginUpdate()
                    If Part IsNot Nothing AndAlso Part.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(Part.Id))
                            With Part
                                'LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad

                                CodePart = .Code
                                NamePart = .Name
                                For i As Integer = 0 To .PartDetail.ToList.Count - 1
                                    EquipmentTypeList.Add(.PartDetail.ToList.Item(i).EquipmentType)
                                Next
                                If EquipmentTypeList.Count > 0 Then
                                    LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                End If
                                INDgcListEquipmentType.DataSource = EquipmentTypeList
                                Status = .State
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Part.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Part.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(Part.Id, Me.Tag.ToString(), Nothing, GetType(Domain.Maintenance.Entities.Part).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPart()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodePart = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLyCtrPart.EndUpdate()
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
        With Part
            .Name = NamePart
            .Code = CodePart
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
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Part.Code, Me.Part.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & CStr(Me.Tag) & "_" & Me.Part.Code & "#$", .IdForm = CStr(Me.Tag), _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Part.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Part.Code, Me.Part.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Part.Code)
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
    Private Async Function NewPart() As Task
        'Me.Part = New Domain.Maintenance.Entities.Part()
        'If Me._sequense.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequense = Me._sequense.MaintenanceSequenceDetail(0).Id
        '    ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me._sequense.MaintenanceSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequense = Me._sequense.MaintenanceSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Sub
        '        End If
        '    End If
        '    If Not Me._sequense.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                Me.CodePart = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
        '                    Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
        '                End Using
        '                If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                    Me.CodePart = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.CodePart = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    Else
        '        Me.CodePart = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        'End If


        Part = New Domain.Maintenance.Entities.Part()
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
                Me.CodePart = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodePart = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
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
                            Me.CodePart = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodePart = ResourceManager.GetString("LabelOrTextboxNew")
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
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        'Try
        '    Using model As New MPart
        '        AsyncLoader(True)
        '        Dim Result As New ActionResult(Of Domain.Maintenance.Entities.Part)
        '        Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
        '            Case eActionsStatusRecords.Active
        '                Result = Await model.ChangeState(CodePart, True)
        '            Case eActionsStatusRecords.Inactive
        '                Result = Await model.ChangeState(CodePart, False)
        '        End Select
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            Part = Result.ObjectEmbbeded
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

        If Not String.IsNullOrEmpty(Me.Part.Code) Then
            Try
                Using model As New MPart
                    Dim state As Boolean = Status = eActionsStatusRecords.Active
                    AsyncLoader(True)
                    Dim result As ActionResult(Of Domain.Maintenance.Entities.Part) = Await model.ChangeState(Me.Part.Code, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.Part = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
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
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmSpecificConcepts_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' _idOperativeUnit = BarraBotones.OperatingUnitValue
        'Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        'Dim ListActions As New List(Of eAcciones)

        'ListActions.Add(eAcciones.Remove)
        'IndigoGridView1.SetListAcction(INDgcListEquipmentTypeView, ListActions)
        'For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgcListEquipmentTypeView.Columns
        '    If col.Name = "colActions" Then
        '        col.Width = 100
        '    End If
        'Next
        'Presenter = New PPart(Me)
        'Presenter.Initializes()
        'Presenter.GetSequense()
        'Deshacer()
        'Me.LoadStatus()
        '_searchMode = False


        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PPart(Me)
        Presenter.GetSequense()
        Dim ListActions As New List(Of eAcciones)

        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgcListEquipmentTypeView, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgcListEquipmentTypeView.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        Presenter.Initializes()
        'Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
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
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        ''If e.KeyCode = Keys.Enter Then
        ''    If String.IsNullOrEmpty(CodePart) Then
        ''        Me.NewPart()
        ''    Else
        ''        Await LoadControls()
        ''    End If
        ''End If
        'If e.KeyCode = Windows.Forms.Keys.Enter Then
        '    If Me._sequense.IsManual Then
        '        If Not String.IsNullOrEmpty(INDBteCode.Text) Then
        '            Await Me.LoadControls()
        '        End If
        '    Else
        '        If String.IsNullOrEmpty(INDBteCode.Text) Then
        '            Me.NewPart()
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
                If Not String.IsNullOrEmpty(CodePart.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodePart) Then
                    Await Me.NewPart()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDbtnAgregar control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDbtnAgregar_Click(sender As Object, e As EventArgs) Handles INDbtnAgregar.Click
        If Object.Equals(INDglEquipmentType.EditValue, Nothing) = True Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Tipo de Equipo"
            Exit Sub
        End If
        If Part.PartDetail.Where(Function(x) x.IdEquipmentType = IdEquipment).Count = 0 Then
            Part.PartDetail.Add(New Domain.Maintenance.Entities.PartDetail With {.IdPart = Part.Id, .IdEquipmentType = IdEquipment})
            If Part.Id = 0 Then
                Part.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added
            Else
                Part.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
            EquipmentTypeList.Add(New Domain.Maintenance.Entities.EquipmentType With {.Code = INDglEquipmentType.EditValue.ToString.Trim, .Name = INDglEquipmentType.Text.ToString.Trim})
            INDglEquipmentType.EditValue = Nothing
        Else
            Mensaje(EeventViewerImages.Advertencia) = "El Tipo de Equipo ya existe en la lista"
            INDglEquipmentType.EditValue = Nothing
        End If
        INDgcListEquipmentType.DataSource = Nothing
        INDgcListEquipmentType.DataSource = EquipmentTypeList
        If EquipmentTypeList.Count > 0 Then
            LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDgcListEquipmentType_EmbeddedNavigator control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.NavigatorButtonClickEventArgs"/> instance containing the event data.</param>
    Private Sub INDgcListEquipmentType_EmbeddedNavigator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles INDgcListEquipmentType.EmbeddedNavigator.ButtonClick
        If e.Button.ButtonType = DevExpress.XtraEditors.NavigatorButtonType.Remove Then
            Dim Row = INDgcListEquipmentTypeView.FocusedRowHandle
            DeletedList.Add(Part.PartDetail.Item(Row))
            Part.PartDetail.RemoveAt(Row)
        End If
    End Sub

    Private Sub FrmPart_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.Part IsNot Nothing AndAlso Me.Part.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = Windows.Forms.DialogResult.Yes) Then
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

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim Row = INDgcListEquipmentTypeView.FocusedRowHandle
        DeletedList.Add(Part.PartDetail.Item(Row))
        Part.PartDetail.RemoveAt(Row)
        EquipmentTypeList.RemoveAt(Row)
        INDgcListEquipmentType.DataSource = Nothing
        INDgcListEquipmentType.DataSource = EquipmentTypeList

    End Sub
#End Region
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        PathFunctionalDefinitions = Nothing
        Part = Nothing
        Model = Nothing
        Presenter = Nothing
        EquipmentTypeList = Nothing
        DeletedList = Nothing
        record = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _searchMode = Nothing
    End Sub
    ''' <summary>
    ''' Evento que abre el formulario de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDglEquipmentType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDglEquipmentType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            'Using pop As New FrmTransparent(New Presentation.FixedAsset.FrmEquipmentType With {.ViewModeEditHold = True, .StartPosition = Windows.Forms.FormStartPosition.CenterParent}, False)
            '    pop.Show()
            'End Using
            Await Presenter.Initializes()
        End If
    End Sub

End Class