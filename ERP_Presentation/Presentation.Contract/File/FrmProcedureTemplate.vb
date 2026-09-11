'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 07/10/2014
'
' Last Modified By : Carlos Mario Arias Rubiano
' Last Modified On : 11/06/2015
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Contract.MVP
Imports Presentation.Controls

#End Region

Public Class FrmProcedureTemplate
    Implements IProcedureTemplate, ICustomizableForm

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador de grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PProcedureTemplate

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Dim _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Dim _idCurrentSequence As Int64

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Dim _sequence As Domain.Entities.ContractSequence

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Dim _blockRecord As BlockRecordContract

    ''' <summary>
    ''' representa la entidad de plantillas de procedimiento
    ''' </summary>
    ''' <remarks></remarks>
    Dim _procedureTemplate As ProcedureTemplate

    ''' <summary>
    ''' Representa a la entidad del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim _procedureCups As ProcedureCups

    ''' <summary>
    ''' Listado de detalles del cubrimiento
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listProcedureCups As List(Of ProcedureCups)

    ''' <summary>
    ''' Listado de detalles del cubrimiento
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteProcedureCups As List(Of ProcedureCups)


    ''' <summary>
    ''' Cantidad de items seleccionados
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property SelectedCups As List(Of Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo)
        Get
            Dim items = CupsEntityXPO.Where(Function(m) m.SelectOption).ToList()
            Return items
        End Get
    End Property
    ''' <summary>
    ''' Cantidad de items seleccionados
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property SelectedCount As Integer
        Get
            Dim count = SelectedCups.Count
            Return count
        End Get
    End Property
#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IProcedureTemplate.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IProcedureTemplate.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IProcedureTemplate.ActionsOnControls
        Set(value As Boolean)
            INDLcProcedureTemplate.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDTxtName.Enabled = value

            INDpceAddCups.Enabled = value
            INDEsbExport.Enabled = value
            INDGcProcedureCups.Enabled = value

            INDLcProcedureTemplate.EndUpdate()

            If value Then
                INDTxtName.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    Public Property Sequense As ContractSequence Implements IProcedureTemplate.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As ContractSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.ContractSequenceDetail In Me._sequence.ContractSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el consecutivo del grupo
    ''' </summary>
    Public Property Code As String Implements IProcedureTemplate.Code
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
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
    ''' Obtiene o establece el nombre
    ''' </summary>
    Public Property NameTemplate As String Implements IProcedureTemplate.NameTemplate
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements IProcedureTemplate.Status
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

#Region "Datasource"
    ''' <summary>
    ''' datasource de entidades CUPS
    ''' </summary>
    Public Property CupsEntityXPO As List(Of Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo) Implements IProcedureTemplate.CupsEntityXPO
        Get
            Return CType(INDSleCupsEntity.Properties.DataSource, List(Of Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo))
        End Get
        Set(value As List(Of Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo))
            INDSleCupsEntity.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' metodo para abrir el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListProcedureTemplate
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewProcedureTemplate()
        End If
    End Sub

    Public Overrides Sub AsyncLoader(State As Boolean) Implements IProcedureTemplate.AsyncLoader
        MyBase.AsyncLoader(State)
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        Dim errors = ValidateFields()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        Try
            AssigningValues()
            'Obtengo en un listado solo los items que fueron agregados o modificados para enviarlos a guardar
            Dim ListSave As List(Of ProcedureCups) = (From l In _listProcedureCups Where l.ChangeTracker.State = ObjectState.Added OrElse l.ChangeTracker.State = ObjectState.Modified Select l).ToList
            Using model As New MProcedureTemplate(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveProcedureTemplate(_procedureTemplate, ListSave, _listDeleteProcedureCups, _idCurrentSequence)
                If Result.StateResult = True Then
                    If _procedureTemplate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me._procedureTemplate = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If _procedureTemplate IsNot Nothing AndAlso _procedureTemplate.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MProcedureTemplate(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteProcedureTemplate(_procedureTemplate, _listProcedureCups, _listDeleteProcedureCups, indigo.TransactionalContainer)
                        If result.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBteCode.Enabled = False
                            If result.MessageResult(0) = "-999" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                            ElseIf result.MessageResult(0) = "-000" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                            ElseIf result.MessageResult(0) = "-111" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                            End If
                        End If
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
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que inicializa las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        Dim ListYesNot As New List(Of Tuple(Of Boolean, String))
        ListYesNot.Add(New Tuple(Of Boolean, String)(True, "Sí"))
        ListYesNot.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleContrated.Properties.DataSource = ListYesNot.ToList
        INDsleQuoted.Properties.DataSource = ListYesNot.ToList
    End Sub

    ''' <summary>
    ''' Metodo que selecciona todo el grupo o todo el subGrupo
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptions(optionCheck As Integer)
        Dim view As GridView = INDGvCupsEntity
        Dim listHandlesSelected = view.GetSelectedRows

        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                GetChildsRows(view, listHandlesSelected(i), optionCheck)
            Next
        End If

        Dim cont = (From l In CupsEntityXPO Where l.SelectOption = True Select l).Count
        INDSleCupsEntity.Text = cont.ToString + " item seleccionado"
    End Sub

    ''' <summary>
    ''' Obtiene la informacion de las filas de la rejilla
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, optionCheck As Integer)
        If view.IsGroupRow(groupRowHandle) Then
            Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
            For i As Integer = 0 To childCount - 1
                Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
                If view.IsGroupRow(childHandle) Then
                    GetChildsRows(view, childHandle, optionCheck)
                Else
                    Dim row As Object = view.GetRow(childHandle)
                    SetCheck(optionCheck, childHandle, view)
                End If
            Next
        Else
            SetCheck(optionCheck, groupRowHandle, view)
        End If
    End Sub

    ''' <summary>
    ''' Asigna el check
    ''' </summary>
    Private Sub SetCheck(optionCheck As Integer, handle As Integer, view As GridView)
        Dim row As Object = view.GetRow(handle)
        If optionCheck = 0 Then
            row.SelectOption = False
        Else
            row.SelectOption = True
        End If
        INDGvCupsEntity.RefreshRow(handle)
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        INDLcProcedureTemplate.BeginUpdate()
        Await DeleteBlockedRecord()

        Code = String.Empty
        NameTemplate = String.Empty
        Status = True

        INDGvCupsEntity.RefreshData()
        INDsleContrated.EditValue = True
        INDsleQuoted.EditValue = False
        INDGcProcedureCups.DataSource = Nothing

        _doc = Nothing
        _procedureTemplate = Nothing
        _listProcedureCups = Nothing
        _listDeleteProcedureCups = Nothing
        ActionsOnControls = False

        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        ClearSelectedCups()

        INDLcProcedureTemplate.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(MyTag)
                Await Model.DeleteBlockRecord(_blockRecord)
                _blockRecord = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(MyTag, Me._procedureTemplate.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                _blockRecord = New BlockRecordContract With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me._procedureTemplate.Id}
                Dim operation = Await model.SaveBlockRecord(_blockRecord)
                _blockRecord = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                _blockRecord = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._procedureTemplate.Code, Me._procedureTemplate.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._procedureTemplate.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._procedureTemplate.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._procedureTemplate.Code, Me._procedureTemplate.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._procedureTemplate.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewProcedureTemplate() As Task
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.ContractSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.ContractSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If

        Status = True
        BarraBotones.StatusRecordVisible = True
        _procedureTemplate = New ProcedureTemplate With {.Status = True}
    End Function

    ''' <summary>
    ''' metodo para cargar los controles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Me.BarraBotones.StatusRecordVisible = True
                Using Model As New MProcedureTemplate(CStr(Me.Tag))
                    AsyncLoader(True)
                    _procedureTemplate = Await Model.GetProcedureTemplate(INDBteCode.Text.Trim)
                    INDLcProcedureTemplate.BeginUpdate()
                    If _procedureTemplate IsNot Nothing AndAlso _procedureTemplate.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_procedureTemplate.Id))
                            With _procedureTemplate
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Code = .Code
                                NameTemplate = .Name
                                Status = .Status
                                _listProcedureCups = New List(Of ProcedureCups)
                                Dim ListXpCollection = Model.ListProcedureCupsByProcedureTemplateId(.Id)
                                For Each itemXpo In ListXpCollection
                                    Dim procedureCupsIteration = New ProcedureCups
                                    procedureCupsIteration.Id = itemXpo.Id
                                    procedureCupsIteration.ProceduresTemplateId = itemXpo.ProceduresTemplateId
                                    procedureCupsIteration.CupsId = itemXpo.CupsId.Id
                                    procedureCupsIteration.CodeNameCUPS = itemXpo.CupsId.CodeDescription
                                    procedureCupsIteration.CUPSCode = itemXpo.CupsId.Code
                                    procedureCupsIteration.CUPSName = itemXpo.CupsId.Description
                                    procedureCupsIteration.Contracted = itemXpo.Contracted
                                    procedureCupsIteration.Quoted = itemXpo.Quoted
                                    If itemXpo.ContractDescriptionId IsNot Nothing Then
                                        procedureCupsIteration.CUPSEntityContractDescriptionId = itemXpo.CUPSEntityContractDescriptionId
                                        procedureCupsIteration.ContractDescriptionId = itemXpo.ContractDescriptionId.Id
                                        procedureCupsIteration.ContractDescriptionCodeName = itemXpo.ContractDescriptionId.CodeName
                                    End If
                                    procedureCupsIteration.MarkAsUnchanged()
                                    _listProcedureCups.Add(procedureCupsIteration)
                                Next
                                INDGcProcedureCups.DataSource = Nothing
                                INDGcProcedureCups.DataSource = _listProcedureCups
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._procedureTemplate.Code)
                            If _blockRecord.Id = 0 Then
                                _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _procedureTemplate.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_procedureTemplate.Id, Me.Tag.ToString(), Nothing, GetType(ProcedureTemplate).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewProcedureTemplate()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLcProcedureTemplate.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' metodo para validar controles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateFields() As String
        Dim errors As New StringBuilder
        If INDGvProcedureCups.RowCount = 0 Then
            errors.AppendLine(ResourceManager.GetString("AddServiceCUPS", NAME_MODULE))
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' asiganar valores
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With _procedureTemplate
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameTemplate
        End With
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Try
                Using model As New MProcedureTemplate(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim state As Boolean = Not _procedureTemplate.Status
                    Dim Result = Await model.ChangeStateProcedureTemplate(Code, state)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")

                        Me._procedureTemplate = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If

                        INDBteCode.Enabled = False
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

#Region "Details"

    ''' <summary>
    ''' Metodo que elimina el detalle de la cabecera
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteAgregated()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim rowSelectCount = INDGvProcedureCups.SelectedRowsCount
            If rowSelectCount > 1 Then
                Dim ListRows As New List(Of Object)
                For i = 0 To rowSelectCount - 1
                    If INDGvProcedureCups.GetSelectedRows()(i) >= 0 Then
                        Dim row As ProcedureCups = INDGvProcedureCups.GetRow(INDGvProcedureCups.GetSelectedRows()(i))
                        ListRows.Add(row)
                        If row.Id > 0 Then
                            If _listDeleteProcedureCups Is Nothing Then
                                _listDeleteProcedureCups = New List(Of ProcedureCups)
                            End If
                            row.MarkAsDeleted()
                            _listDeleteProcedureCups.Add(row)
                        End If
                    End If
                Next
                ListRows.ForEach(Sub(item) _listProcedureCups.Remove(item))
            Else
                _procedureCups = DirectCast(INDGvProcedureCups.GetFocusedRow, ProcedureCups)
                _listProcedureCups.Remove(_procedureCups)
                If _procedureCups.Id > 0 Then
                    If _listDeleteProcedureCups Is Nothing Then
                        _listDeleteProcedureCups = New List(Of ProcedureCups)
                    End If
                    _procedureCups.MarkAsDeleted()
                    _listDeleteProcedureCups.Add(_procedureCups)
                End If
            End If

            INDGcProcedureCups.DataSource = Nothing
            INDGcProcedureCups.DataSource = _listProcedureCups
        End If
    End Sub

#End Region

#End Region

#Region "Events"

#Region "Load"

    Private Sub FrmProcedureTemplate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcProcedureTemplate, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        _presenter = New PProcedureTemplate(Me)
        _presenter.GetSequense()
        '******************************
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        INDsleContrated.EditValue = True
        INDsleQuoted.EditValue = False
        IndigoGridControl1.RefreshGrid(INDGcProcedureCups)
        INDEsbExport.AddRangeColumns("Código CUPS", "Código Descripción")

        LoadStatus()
        Deshacer()
        InitializeTuples()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _blockRecord = Nothing
        _procedureTemplate = Nothing
        _procedureCups = Nothing
        _listProcedureCups = Nothing
        _listDeleteProcedureCups = Nothing
    End Sub

#End Region

#Region "Activated"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub

    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.ContractSequenceDetail IsNot Nothing Then
                If Not Me._sequence.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

#Region "Closing"

    Private Async Sub FrmProcedureTemplate_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewProcedureTemplate()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceAddCups_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddCups.KeyDown
        If e.KeyCode = Keys.F4 OrElse e.KeyCode = Keys.Enter Then
            INDpceAddCups.ShowPopup()
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara despues de desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceAddCups_Popup(sender As Object, e As EventArgs) Handles INDpceAddCups.Popup
        INDSleCupsEntity.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    Private Async Sub INDSleCupsEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCupsEntity.QueryPopUp
        If CupsEntityXPO Is Nothing Then
            Await _presenter.InitializeCUPSEntity()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAddCups_Click(sender As Object, e As EventArgs) Handles INDBtnAddCups.Click
        If SelectedCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione al menos un item de entidad CUPS."
            Exit Sub
        End If

        If _listProcedureCups Is Nothing Then
            _listProcedureCups = New List(Of ProcedureCups)
        End If

        Dim keysArray = SelectedCups.Select(Function(m) m.Id).ToArray()
        Dim keys = String.Join(",", keysArray)

        Dim listCupsWithDescriptionsIds = _presenter.CupsWithDescriptionsId(keys)
        Dim listCupsWithoutDescriptionsIds = keysArray.Where(Function(s) Not listCupsWithDescriptionsIds.Contains(s)).ToList()

        If listCupsWithDescriptionsIds.Any() Then
            Me.Cursor = ChangeCursorIndigo()
            Using Formulario As New FrmSelectCupsDescription()
                Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Formulario.Width = 600
                Formulario.Height = 500
                Formulario.ListCupsIds = listCupsWithDescriptionsIds
                Dim frm As New FrmTransparent(Formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                frm.ShowDialog(Me)

                If Formulario.DialogResult = System.Windows.Forms.DialogResult.OK Then
                    If Formulario.ListCupsEntityWithDescriptionsXpo IsNot Nothing AndAlso Formulario.ListCupsEntityWithDescriptionsXpo.Count > 0 Then
                        For Each item In Formulario.ListCupsEntityWithDescriptionsXpo
                            Dim cupsTmp = _listProcedureCups.Where(Function(x) x.CupsId = item.CUPSEntityId AndAlso x.ContractDescriptionId IsNot Nothing AndAlso x.ContractDescriptionId = item.ContractDescriptionId).FirstOrDefault()
                            If cupsTmp IsNot Nothing Then
                                Continue For
                            End If

                            _procedureCups = New ProcedureCups
                            _procedureCups.CupsId = item.CUPSEntityId
                            _procedureCups.CUPSCode = item.CUPSEntityCode
                            _procedureCups.CUPSName = item.CUPSEntityName
                            _procedureCups.CodeNameCUPS = item.CUPSEntityCodeName
                            _procedureCups.Contracted = INDsleContrated.EditValue
                            _procedureCups.Quoted = INDsleQuoted.EditValue
                            _procedureCups.ContractDescriptionId = item.ContractDescriptionId
                            _procedureCups.CUPSEntityContractDescriptionId = item.CUPSEntityContractDescriptionId
                            _procedureCups.ContractDescriptionCodeName = item.ContractDescriptionCodeName
                            _listProcedureCups.Add(_procedureCups)
                        Next
                    End If
                End If
            End Using
        End If
        For Each cupsEntityId In listCupsWithoutDescriptionsIds
            Dim cupsTmp = _listProcedureCups.Where(Function(x) x.CupsId = cupsEntityId AndAlso x.ContractDescriptionId Is Nothing).FirstOrDefault()
            If cupsTmp IsNot Nothing Then
                Continue For
            End If

            Dim selected = SelectedCups.Find(Function(m) m.Id = cupsEntityId AndAlso m.Status)
            If selected IsNot Nothing Then
                _procedureCups = New ProcedureCups
                _procedureCups.CupsId = cupsEntityId
                _procedureCups.CUPSCode = selected.Code ' _selectorCupsEntity.GetValueByKey(cupsEntityId, "Code")
                _procedureCups.CUPSName = selected.Description '_selectorCupsEntity.GetValueByKey(cupsEntityId, "Description")
                _procedureCups.CodeNameCUPS = selected.CodeDescription '_selectorCupsEntity.GetValueByKey(cupsEntityId, "CodeDescription")
                _procedureCups.Contracted = INDsleContrated.EditValue
                _procedureCups.Quoted = INDsleQuoted.EditValue
                _listProcedureCups.Add(_procedureCups)
            End If
        Next


        ClearSelectedCups()
        INDSleCupsEntity.Properties.NullText = "0 item seleccionado"
        INDGvCupsEntity.RefreshData()
        INDsleContrated.EditValue = True
        INDsleQuoted.EditValue = False
        INDSleCupsEntity.Focus()

        INDGcProcedureCups.DataSource = Nothing
        INDGcProcedureCups.DataSource = _listProcedureCups
    End Sub

    Private Sub INDrepBtnDelete_Click(sender As Object, e As EventArgs) Handles INDrepBtnDelete.Click
        DeleteAgregated()
    End Sub

#End Region

#Region "Selection"

    Private Sub ClearSelectedCups()
        If INDSleCupsEntity.Properties.DataSource IsNot Nothing Then
            SelectedCups?.ForEach(Sub(m) m.SelectOption = False)
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvCupsEntity.RowCellClick
        If e.Column.FieldName.Contains("SelectOption") Then
            If e.RowHandle >= 0 Then
                Dim row = INDGvCupsEntity.GetRow(e.RowHandle)
                row.SelectOption = Not row.SelectOption
                INDGvCupsEntity.RefreshRow(e.RowHandle)
            Else
                ClearSelectedCups()
                INDGvCupsEntity.RefreshData()
            End If
        End If
    End Sub

    Private Sub INDGvSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleCupsEntity.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing

        If SelectedCount = 1 Then
            searchLookupEdit.Properties.NullText = "1 Item Seleccionado"
        Else
            searchLookupEdit.Properties.NullText = String.Format("{0} Items Seleccionados", SelectedCount)
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDrepCheckContrated_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckContrated.EditValueChanging
        Dim entity = DirectCast(INDGvProcedureCups.GetFocusedRow, ProcedureCups)
        entity.Contracted = e.NewValue
    End Sub

    Private Sub INDrepCheckQuoted_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckQuoted.EditValueChanging
        Dim entity = DirectCast(INDGvProcedureCups.GetFocusedRow, ProcedureCups)
        entity.Quoted = e.NewValue
    End Sub

#End Region

#Region "PopupMenuShowing"
    ''' <summary>
    ''' INDGvCupsEntity_PopupMenuShowing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvCupsEntity_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDGvCupsEntity.PopupMenuShowing
        ShowMenuInGrid(sender, e)
    End Sub

    Private Sub INDGvProcedureCups_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDGvProcedureCups.PopupMenuShowing
        ShowMenuInGrid(sender, e)
    End Sub

    ''' <summary>
    ''' Método que muestra el menu dependiendo de la rejilla
    ''' </summary>
    Private Sub ShowMenuInGrid(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs)
        Dim View = CType(sender, GridView)

        If View.Name = INDGvProcedureCups.Name Then
            INDbarButtonSelectContrated.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDbarButtonUnSelectContrated.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDbarButtonSelectQuoted.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDbarButtonUnSelectQuoted.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDbarButtonDeleted.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            INDbarButtonSelectAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            INDbarButtonUnSelectAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            INDbarButtonSelectContrated.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            INDbarButtonUnSelectContrated.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            INDbarButtonSelectQuoted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            INDbarButtonUnSelectQuoted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            INDbarButtonDeleted.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

            INDbarButtonSelectAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDbarButtonUnSelectAll.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        End If

        PopupMenuActions.Manager = BarManager
        PopupMenuActions.ShowPopup(View.GridControl.PointToScreen(e.Point))
    End Sub

#End Region

#Region "ItemClick"



    ''' <summary>
    ''' Seleccionar todo el grupo o subGrupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarButtonSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonSelectAll.ItemClick
        SelectOptions(1)
    End Sub

    ''' <summary>
    ''' Quitar seleccion de todo el grupo o subGrupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarButtonUnSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonUnSelectAll.ItemClick
        SelectOptions(0)
    End Sub

    Private Sub INDbarButtonSelectContrated_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonSelectContrated.ItemClick
        SelectOptionsGridProcedures(True, Nothing)
    End Sub

    Private Sub INDbarButtonUnSelectContrated_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonUnSelectContrated.ItemClick
        SelectOptionsGridProcedures(False, Nothing)
    End Sub

    Private Sub INDbarButtonSelectQuoted_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonSelectQuoted.ItemClick
        SelectOptionsGridProcedures(Nothing, True)
    End Sub

    Private Sub INDbarButtonUnSelectQuoted_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonUnSelectQuoted.ItemClick
        SelectOptionsGridProcedures(Nothing, False)
    End Sub

    Private Sub INDbarButtonDeleted_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonDeleted.ItemClick
        DeleteAgregated()
    End Sub

    ''' <summary>
    ''' Metodo que pone el check en contratados y cotizados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptionsGridProcedures(optionCheckContrated As Boolean?, optionCheckQuoted As Boolean?)
        Dim view As GridView = INDGvProcedureCups
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                Dim row As ProcedureCups = view.GetRow(listHandlesSelected(i))
                If optionCheckContrated IsNot Nothing Then
                    row.Contracted = optionCheckContrated.Value
                End If
                If optionCheckQuoted IsNot Nothing Then
                    row.Quoted = optionCheckQuoted.Value
                End If
            Next
        End If
        INDGcProcedureCups.RefreshDataSource()
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    ''' 
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._procedureTemplate IsNot Nothing AndAlso Me._procedureTemplate.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Await DeleteBlockedRecord()
                Code = Me.IdEntity.Trim()
                Await LoadControls()
            End If
        Else 'Realiza la consulta normal
            Code = Me.IdEntity.Trim()
            Await LoadControls()
        End If
        Me.IdEntity = String.Empty
        If INDBteCode.Enabled = False Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        End If
    End Sub

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Await PasteToGrid(sender, e.Rows)
    End Sub

    ''' <summary>
    ''' Metodo que copia y pega los items a la rejilla de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function PasteToGrid(sender As DevExpress.XtraGrid.GridControl, ListInfo As List(Of List(Of String))) As Task
        If sender.Name = INDGcProcedureCups.Name Then
            INDGvProcedureCups.ShowLoadingPanel()
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MProcedureTemplate(MyTag)
                Dim result = Await model.CopyAndPasteProcedureTemplate(ListInfo)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDGvProcedureCups.HideLoadingPanel()
                    Exit Function
                End If

                Dim listErrors As New List(Of Tuple(Of String, Integer))

                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    If _listProcedureCups IsNot Nothing AndAlso _listProcedureCups.Count > 0 Then
                        For Each item In result.ObjectEmbbeded
                            If _listProcedureCups.Where(Function(x) x.CupsId = item.CupsId AndAlso ((x.ContractDescriptionId Is Nothing AndAlso item.ContractDescriptionId Is Nothing) OrElse (x.ContractDescriptionId IsNot Nothing AndAlso item.ContractDescriptionId IsNot Nothing AndAlso x.ContractDescriptionId = item.ContractDescriptionId))).Count > 0 Then
                                listErrors.Add(New Tuple(Of String, Integer)("El CUPS " + item.CodeNameCUPS + " ya existe en la lista", 2))
                                Continue For
                            End If
                            _listProcedureCups.Add(item)
                        Next
                    Else
                        _listProcedureCups = result.ObjectEmbbeded
                    End If
                End If

                If listErrors IsNot Nothing AndAlso listErrors.Count > 0 Then
                    result.ObjectEmbbededAux.AddRange(listErrors)
                End If

                If result.ObjectEmbbededAux IsNot Nothing AndAlso result.ObjectEmbbededAux.Count > 0 Then
                    Using formulario As New FrmListErrors(result.ObjectEmbbededAux)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End Using
            INDGcProcedureCups.DataSource = Nothing
            INDGcProcedureCups.DataSource = _listProcedureCups
            Me.Cursor = System.Windows.Forms.Cursors.Default
            INDGvProcedureCups.HideLoadingPanel()
        End If
    End Function

#End Region

#End Region

#Region "BarButton Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

#End Region

End Class