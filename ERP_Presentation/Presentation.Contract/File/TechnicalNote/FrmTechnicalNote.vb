#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
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

Public Class FrmTechnicalNote
    Implements ITechnicalNote, ICustomizableForm

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PTechnicalNote

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
    ''' Representa la entidad de nota tecnica
    ''' </summary>
    ''' <remarks></remarks>
    Dim _TechnicalNote As TechnicalNote

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim _TechnicalNoteDetail As TechnicalNoteDetail

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listTechnicalNoteDetail As List(Of TechnicalNoteDetail)

    ''' <summary>
    ''' Listado eliminado de detalles
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDeleteTechnicalNoteDetail As List(Of TechnicalNoteDetail)

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim _indexEditRecord As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ITechnicalNote.MyTag
        Get
            Return Me.Tag.ToString
        End Get
    End Property

    ''' <summary>
    ''' Layout del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ITechnicalNote.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ITechnicalNote.ActionsOnControls
        Set(value As Boolean)
            INDlyTechnicalNote.BeginUpdate()

            INDBtnCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDPceGrouperId.Enabled = value
            INDGcTechnicalNoteDetail.Enabled = value

            INDlyTechnicalNote.EndUpdate()

            If value Then
                INDTxtName.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As ContractSequence Implements ITechnicalNote.Sequense
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
    ''' Obtiene o establece el codigo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements ITechnicalNote.Code
        Get
            If (INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBtnCode.Text
            End If
        End Get
        Set(value As String)
            INDBtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre del grupo de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameTechnicalNote As String Implements ITechnicalNote.NameTechnicalNote
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements ITechnicalNote.Status
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

#Region "Popup Details"

    ''' <summary>
    ''' Obtiene o establece el id del agrupador
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GrouperId As Integer Implements ITechnicalNote.GrouperId
        Get
            Return INDSleGrouperId.EditValue
        End Get
        Set(value As Integer)
            INDSleGrouperId.EditValue = value
        End Set
    End Property

#End Region

#End Region

#Region "Datasource"

    ''' <summary>
    ''' Establece el datasource de la definicion de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GrouperXpo As XPInstantFeedbackSource Implements ITechnicalNote.GrouperXpo
        Get
            Return INDSleGrouperId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleGrouperId.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.5)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListTechnicalNote
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
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewTechnicalNote()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        If _listTechnicalNoteDetail Is Nothing OrElse _listTechnicalNoteDetail.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe tener al menos un agrupador."
            Exit Sub
        End If
        Try
            AssigningValues()
            Using model As New MTechnicalNote(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveTechnicalNote(_TechnicalNote, _idCurrentSequence)
                AsyncLoader(False)
                If Result.StateResult Then
                    If _TechnicalNote.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf _TechnicalNote.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If

                    Me._TechnicalNote = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.Deshacer()
                Else
                    INDBtnCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    ElseIf Result.MessageResult(0) IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0).ToString()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvTechnicalNoteDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvTechnicalNoteDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        INDlyTechnicalNote.BeginUpdate()
        Await DeleteBlockedRecord()

        Code = String.Empty
        NameTechnicalNote = String.Empty
        Status = Nothing

        INDPceGrouperId.Enabled = False
        INDGcTechnicalNoteDetail.DataSource = Nothing
        CleanControlsPopup()

        _doc = Nothing
        _TechnicalNote = Nothing
        _listTechnicalNoteDetail = Nothing
        _listDeleteTechnicalNoteDetail = Nothing
        _indexEditRecord = -1
        _TechnicalNoteDetail = Nothing

        ReadOnlyControls(False)
        ActionsOnControls = False

        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False

        INDlyTechnicalNote.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        _indexEditRecord = -1
        _TechnicalNoteDetail = Nothing

        INDSleGrouperId.EditValue = Nothing
        INDSleGrouperId.Properties.NullText = String.Empty
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._TechnicalNote IsNot Nothing AndAlso Me._TechnicalNote.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDBtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(_blockRecord)
                _blockRecord = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._TechnicalNote.Code, Me._TechnicalNote.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._TechnicalNote.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._TechnicalNote.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._TechnicalNote.Code, Me._TechnicalNote.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._TechnicalNote.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewTechnicalNote() As Task
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
        _TechnicalNote = New TechnicalNote() With {.Status = True}
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MTechnicalNote(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim result = Await Model.GetTechnicalNote(INDBtnCode.Text.Trim)
                    If Not result.StateResult Then
                        AsyncLoader(False)
                        Me.Mensaje(EeventViewerImages.Advertencia) = result.Message
                        Code = String.Empty
                        INDBtnCode.Focus()
                    End If

                    _TechnicalNote = result.ObjectEmbbeded
                    If _TechnicalNote Is Nothing OrElse _TechnicalNote.Id = 0 Then
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewTechnicalNote()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                        Exit Function
                    End If

                    INDlyTechnicalNote.BeginUpdate()
                    Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_TechnicalNote.Id))
                        With _TechnicalNote
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code
                            NameTechnicalNote = .Name
                            Status = .Status
                            BarraBotones.StatusRecordVisible = True

                            _listTechnicalNoteDetail = .TechnicalNoteDetail.ToList()
                            INDGcTechnicalNoteDetail.DataSource = Nothing
                            INDGcTechnicalNoteDetail.DataSource = _listTechnicalNoteDetail
                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._TechnicalNote.Code)
                        If _blockRecord.Id = 0 Then
                            _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _TechnicalNote.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                        End If

                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Me.BarraBotones.SetDocuments(_TechnicalNote.Id, Me.Tag.ToString(), Nothing, GetType(TechnicalNote).Name)

                        ActionsOnControls = True
                        AsyncLoader(False)
                    End Using

                    INDlyTechnicalNote.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _TechnicalNote
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameTechnicalNote

            .TechnicalNoteDetail.Clear()
            If _listTechnicalNoteDetail IsNot Nothing AndAlso _listTechnicalNoteDetail.Count > 0 Then
                For Each item In _listTechnicalNoteDetail
                    .TechnicalNoteDetail.Add(item)
                Next
            End If
            If _listDeleteTechnicalNoteDetail IsNot Nothing AndAlso _listDeleteTechnicalNoteDetail.Count > 0 Then
                For Each item In _listDeleteTechnicalNoteDetail
                    .TechnicalNoteDetail.Add(item)
                Next
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If _TechnicalNote.Id > 0 Then
            Using model As New MTechnicalNote(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not _TechnicalNote.Status
                Dim Result = Await model.ChangeState(_TechnicalNote.Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Me._TechnicalNote = Result.ObjectEmbbeded
                    Status = Me._TechnicalNote.Status
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Else
                    INDBtnCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = Result.MessageResult(0)
                    End If
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

#Region "Details"

    ''' <summary>
    ''' Metodo que agrega una definicion de tarifa a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddTechnicalNoteDetail()
        Dim errors As String = ValidateControlsPopupTechnicalNoteDetail()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        If _TechnicalNoteDetail Is Nothing Then
            _TechnicalNoteDetail = New TechnicalNoteDetail()
        End If
        With _TechnicalNoteDetail
            .GrouperId = GrouperId
            .GrouperDescription = INDSleGrouperId.Text
        End With
        If _TechnicalNoteDetail.Id = 0 Then
            _listTechnicalNoteDetail.Add(_TechnicalNoteDetail)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else
            _TechnicalNoteDetail.MarkAsModified()
            _listTechnicalNoteDetail.Remove(_TechnicalNoteDetail)
            _listTechnicalNoteDetail.Insert(_indexEditRecord, _TechnicalNoteDetail)
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If
        INDGcTechnicalNoteDetail.DataSource = Nothing
        INDGcTechnicalNoteDetail.DataSource = _listTechnicalNoteDetail
        CleanControlsPopup()
        INDSleGrouperId.Focus()
    End Sub

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopupTechnicalNoteDetail() As String
        Dim listErrors As New StringBuilder
        If GrouperId = 0 Then
            listErrors.AppendLine("Debe ingresar un agrupador.")
        End If
        If listErrors.Length = 0 Then
            If _listTechnicalNoteDetail Is Nothing Then
                _listTechnicalNoteDetail = New List(Of TechnicalNoteDetail)
            Else
                Dim repeatedDetails = _listTechnicalNoteDetail.Where(Function(item) item.GrouperId = GrouperId).ToList()
                For Each repeated In repeatedDetails
                    Dim indexRepeated = _listTechnicalNoteDetail.IndexOf(repeated)
                    If indexRepeated <> _indexEditRecord Then
                        listErrors.AppendLine("El agrupador ya se encuentra agregado")
                        Exit For
                    End If
                Next
            End If
        End If
        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo que elimina la definicion de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteTechnicalNoteDetail()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim TechnicalNoteDetail = CType(INDGvTechnicalNoteDetail.GetFocusedRow, TechnicalNoteDetail)
        If TechnicalNoteDetail.Id > 0 Then
            If TechnicalNoteDetail.Id > 0 Then
                If _listDeleteTechnicalNoteDetail Is Nothing Then
                    _listDeleteTechnicalNoteDetail = New List(Of TechnicalNoteDetail)
                End If
                TechnicalNoteDetail.MarkAsDeleted()
                _listDeleteTechnicalNoteDetail.Add(TechnicalNoteDetail)
            End If
        End If
        _listTechnicalNoteDetail.Remove(TechnicalNoteDetail)
        INDGcTechnicalNoteDetail.DataSource = Nothing
        INDGcTechnicalNoteDetail.DataSource = _listTechnicalNoteDetail
    End Sub

#End Region

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmTechnicalNote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyTechnicalNote, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        _presenter = New PTechnicalNote(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        '******************************
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        AddActionsColumns()
        LoadStatus()
        Deshacer()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _blockRecord = Nothing
        _TechnicalNote = Nothing
        _listTechnicalNoteDetail = Nothing
        _listDeleteTechnicalNoteDetail = Nothing
        _TechnicalNoteDetail = Nothing
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
        End If
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmTechnicalNote_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBtnCode.KeyDown
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
                    Await Me.NewTechnicalNote()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4 sobre el control de popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceGrouperId_KeyDown(sender As Object, e As KeyEventArgs) Handles INDPceGrouperId.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDPceGrouperId.ShowPopup()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de definicion de tarifas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGrouperId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleGrouperId.QueryPopUp
        If GrouperXpo Is Nothing Then
            _presenter.InitializeGrouper()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de definicion de tarifas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGrouperId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleGrouperId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1958, Nothing, True)
            _presenter.InitializeGrouper()
        End If
    End Sub

#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Menu que se despliega al presionar click derecho sobre la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        DeleteTechnicalNoteDetail()
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceGrouperId_Popup(sender As Object, e As EventArgs) Handles INDPceGrouperId.Popup
        INDSleGrouperId.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddDefinitionRate_Click(sender As Object, e As EventArgs) Handles INDbtnAddGrouperId.Click
        AddTechnicalNoteDetail()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBtnCode.ButtonClick
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
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
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

End Class