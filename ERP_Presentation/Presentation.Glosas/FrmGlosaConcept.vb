'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Diego A. Roldan
' Created          : 2022-04-04
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Glosas.MVP
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraEditors.Mask

Public Class FrmGlosaConcept
    Implements IGlosaConcept


#Region "Variables"
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As GlosaSequence

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecord

    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private _presenter As PGlosaConcept

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Integer

    ''' <summary>
    ''' entity
    ''' </summary>
    Private _conceptGlosas As ConceptGlosas

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Glosas"

    ''' <summary>
    ''' variable privada para almacenar el data source de los tipos de respuesta
    ''' </summary>
    Private _listTypes As List(Of Tuple(Of String, String))
#End Region

#Region "Properties"
    ''' <summary>
    ''' Habilita o deshabilita los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IGlosaConcept.ActionsOnControls
        Set(value As Boolean)
            INDLcRoot.BeginUpdate()
            INDBeCode.Enabled = Not value
            INDTeName.Enabled = value
            INDGleType.Enabled = value
            INDGcUsers.Enabled = value
            INDSleUser.Enabled = value
            INDSbAdd.Enabled = value
            INDMeApplication.Enabled = value
            INDMeNameGeneral.Enabled = value
            INDYesNotDiscountedMedicalFees.Enabled = value
            If value Then
                INDTeName.Focus()
            Else
                INDBeCode.Focus()
            End If
            INDLcRoot.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' tag del formulario
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As String Implements IGlosaConcept.MyTag
        Get
            Return Tag
        End Get
    End Property

    ''' <summary>
    ''' secuencia numérica del formulario
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequence As GlosaSequence Implements IGlosaConcept.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As GlosaSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As GlosaSequenceDetail In Me._sequence.GlosaSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IGlosaConcept.Code
        Get
            If (INDBeCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBeCode.Text
            End If
        End Get
        Set(value As String)
            INDBeCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo
    ''' </summary>
    ''' <returns></returns>
    Public Property Type As String Implements IGlosaConcept.Type
        Get
            Return INDGleType.EditValue
        End Get
        Set(value As String)
            INDGleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Usuarios autorizados
    ''' </summary>
    ''' <returns></returns>
    Public Property ConceptGlosaList As List(Of ConceptGlosasUser) Implements IGlosaConcept.ConceptGlosaList
        Get
            Return INDGcUsers.DataSource
        End Get
        Set(value As List(Of ConceptGlosasUser))
            INDGcUsers.DataSource = value.FindAll(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)
            INDGcUsers.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' Nombre
    ''' </summary>
    ''' <returns></returns>
    Private Property IGlosaConcept_Name As String Implements IGlosaConcept.Name
        Get
            Return INDTeName.EditValue
        End Get
        Set(value As String)
            INDTeName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Estado
    ''' </summary>
    ''' <returns></returns>
    Public Property Status As Boolean Implements IGlosaConcept.Status
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

    ''' <summary>
    ''' propiedad que obtiene los tipos de respuesta de la glosa
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ListTypes As List(Of Tuple(Of String, String)) Implements IGlosaConcept.ListTypes
        Get
            If Me._listTypes Is Nothing Then
                Me._listTypes = New List(Of Tuple(Of String, String)) From {New Tuple(Of String, String)("1", "Glosa"),
                                                                            New Tuple(Of String, String)("2", "Respuesta glosa"),
                                                                            New Tuple(Of String, String)("3", "Devolución"),
                                                                            New Tuple(Of String, String)("4", "Glosa no normativa"),
                                                                            New Tuple(Of String, String)("5", "Respuesta Glosa Injustificada"),
                                                                            New Tuple(Of String, String)("6", "Respuesta Glosa Subsanada Parcial"),
                                                                            New Tuple(Of String, String)("7", "Respuesta Glosa Subsanada Total"),
                                                                            New Tuple(Of String, String)("8", "Respuesta Glosa Aceptación Total"),
                                                                            New Tuple(Of String, String)("9", "Devolución Injustificada"),
                                                                            New Tuple(Of String, String)("10", "Devolución Justificada")}
            End If
            Return Me._listTypes
        End Get
    End Property

    ''' <summary>
    ''' Concepto General
    ''' </summary>
    ''' <returns></returns>
    Public Property NameGeneral As String Implements IGlosaConcept.NameGeneral
        Get
            Return INDMeNameGeneral.EditValue
        End Get
        Set(value As String)
            INDMeNameGeneral.EditValue = value
        End Set
    End Property

    ''' <summary>
    '''Application
    ''' </summary>
    ''' <returns></returns>
    Public Property Application As String Implements IGlosaConcept.Application
        Get
            Return INDMeApplication.EditValue
        End Get
        Set(value As String)
            INDMeApplication.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Descuento Honorarios Médicos
    ''' </summary>
    ''' <returns></returns>
    Public Property DiscountedMedicalFees As Boolean Implements IGlosaConcept.DiscountedMedicalFees
        Get
            Return INDYesNotDiscountedMedicalFees.EditValue
        End Get
        Set(value As Boolean)
            INDYesNotDiscountedMedicalFees.EditValue = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' buscar
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' asigna los valores a la entidad
    ''' </summary>
    Private Sub AssigningValues()
        With _conceptGlosas
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .NameSpecific = IGlosaConcept_Name
            .NameGeneral = Me.NameGeneral
            .Application = Me.Application
            .Type = Type
            .State = Status
            .DiscountedMedicalFees = Me.DiscountedMedicalFees
        End With
    End Sub

    ''' <summary>
    ''' guardar
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If

        AssigningValues()

        Using model As New MConceptGlosas(MyTag)
            Me.AsyncLoader(True)
            Dim result = Await model.SaveConceptGlosas(_conceptGlosas, _idCurrentSequence)
            If result.StateResult Then
                If _conceptGlosas.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    'Se descarta la secuencia numerica usada
                    If Not Me._sequence.IsManual AndAlso Not _sequence.Sequential Then
                        Me.DicSequense(Me._sequence.GlosaSequenceDetail(0).Id).RemoveAt(0)
                    End If
                    If Me._sequence.Sequential Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    End If
                ElseIf _conceptGlosas.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                _conceptGlosas = result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Me.AsyncLoader(False)
                Me.Deshacer()
            Else
                Me.AsyncLoader(False)
                If result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                ElseIf Not String.IsNullOrEmpty(result.Message) Then
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' nuevo
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If

        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewConceptGlosa()
        End If
    End Sub

    ''' <summary>
    ''' deshacer
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' eliminar
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If _conceptGlosas IsNot Nothing AndAlso _conceptGlosas.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using model As New MConceptGlosas(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await model.DeleteConceptGlosas(_conceptGlosas)
                    If result.StateResult Then
                        Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' search
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "NameSpecific", .ColumnWidth = CInt(Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.88)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = eDataSource.ListGlosaConcept
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub


#Region "Events"
    ''' <summary>
    ''' Load form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmGlosaConcept_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        _presenter = New PGlosaConcept(Me)
        _presenter.GetSequense()
        LoadStatus()
        LoadTypes()
        LoadActionsColumn()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Dispose
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        _record = Nothing
        _presenter = Nothing
        _conceptGlosas = Nothing
    End Sub

    ''' <summary>
    ''' Keydown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBeCode.KeyDown
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
                    Await Me.NewConceptGlosa()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' activated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmGlosaConcept_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBeCode.Text Is String.Empty Then
            INDBeCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Load datasource of users
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleUser_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUser.QueryPopUp
        If INDSleUser.Properties.DataSource Is Nothing Then
            INDSleUser.Properties.DataSource = XpoServiceEx.Instance(indigo.SecurityContainer).SecurityService.ListUserByContainer(indigo.IndigoContainerId)
        End If
    End Sub

    ''' <summary>
    ''' Agregar usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        If INDSleUser.EditValue IsNot Nothing Then
            AddAuthorizedUser()
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedUser", "Inventory")
            INDSleUser.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        RemoveUser()
    End Sub

    ''' <summary>
    ''' evento que se ejecuta al cambiar el tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleType.EditValueChanged
        If Type Is Nothing Then
            Exit Sub
        End If

        INDLcgAuthorization.HideControl(Not Type = 4)
        If Not Type = 4 AndAlso ConceptGlosaList?.Any() Then

            For Each item In ConceptGlosaList

                If item.Id = 0 Then
                    _conceptGlosas.ConceptGlosasUser.Remove(item)
                Else
                    item.MarkAsDeleted
                    _conceptGlosas.ConceptGlosasUser.Add(item)
                End If
            Next
            ConceptGlosaList.RemoveAll(Function(s) True)
            INDGcUsers.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' evento para verificar que no vengan caracteres especiales en el memoedit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDMeNameGeneral_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDMeNameGeneral.EditValueChanging, INDMeApplication.EditValueChanging
        If String.IsNullOrEmpty(e.NewValue) Then
            Exit Sub
        End If

        Dim regexPattern = "^[a-zA-Z0-9]+(?:\s[a-zA-Z0-9]+)*\s?$"
        If Not System.Text.RegularExpressions.Regex.IsMatch(e.NewValue, regexPattern) Then
            e.Cancel = True
        End If

    End Sub
#End Region

#Region "Functions"
    ''' <summary>
    ''' Elimina un usuario de la rejilla
    ''' </summary>
    Private Sub RemoveUser()
        If MessageIndigo.Show("¿Desea eliminar el registro?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim user = INDGvUsers.GetFocusedObject(Of ConceptGlosasUser)()

            If user IsNot Nothing Then
                user.MarkAsDeleted()
                ConceptGlosaList = _conceptGlosas.ConceptGlosasUser.ToList()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Acciones sobre la rejilla
    ''' </summary>
    Private Sub LoadActionsColumn()
        IndigoGridView1.SetListAcction(INDGvUsers, {eAcciones.Remove}.ToList())

        Dim col = INDGvUsers.Columns.FirstOrDefault(Function(m) m.Name = "colActions")

        If col IsNot Nothing Then
            col.Width = 90
        End If
    End Sub

    ''' <summary>
    ''' Agrega un usuario autorizado
    ''' </summary>
    Private Sub AddAuthorizedUser()
        Dim userXpo = INDGvUserSearch.GetFocusedObject(Of SecurityRepository.UserXpo)()

        If userXpo IsNot Nothing Then

            If Not ValidateUser(userXpo) Then
                Return
            End If

            Dim newUser As New ConceptGlosasUser With {
                .ConceptGlosaId = _conceptGlosas.Id,
                .UserId = userXpo.Id,
                .UserCode = userXpo.UserCode,
                .UserFullName = userXpo.PersonFullName
            }

            _conceptGlosas.ConceptGlosasUser.Add(newUser)
            ConceptGlosaList = _conceptGlosas.ConceptGlosasUser.ToList()

            If _conceptGlosas.Id > 0 Then
                _conceptGlosas.MarkAsModified()
            End If

            CleanControlsAuthorization()
        End If
    End Sub

    ''' <summary>
    ''' validamos si el usuario ya está agregado
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateUser(user As SecurityRepository.UserXpo) As Boolean
        If _conceptGlosas.ConceptGlosasUser.Any(Function(m) m.UserId = user.Id) Then
            Mensaje(EeventViewerImages.Advertencia) = "El usuario ya se encuentra agregado"
            INDSleUser.Focus()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Limpia los controles de agregar usuario
    ''' </summary>
    Private Sub CleanControlsAuthorization()
        INDSleUser.EditValue = Nothing
        INDSleUser.Focus()
    End Sub

    ''' <summary>
    ''' Carga los tipos de glosa
    ''' </summary>
    Private Sub LoadTypes()
        INDGleType.Properties.DataSource = ListTypes
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Async Sub CleanControls()
        INDLcRoot.BeginUpdate()
        ActionsOnControls = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        Status = True
        Code = String.Empty
        IGlosaConcept_Name = String.Empty
        Type = Nothing
        INDSleUser.EditValue = Nothing
        INDGcUsers.DataSource = Nothing
        Me.ConceptGlosaList = New List(Of ConceptGlosasUser)
        ReadOnlyControls(False)
        _conceptGlosas = Nothing
        Me.NameGeneral = String.Empty
        Me.Application = String.Empty
        Me.DiscountedMedicalFees = False
        INDLcRoot.EndUpdate()
        Await DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Nuevo concepto de glosa
    ''' </summary>
    ''' <returns></returns>
    Private Async Function NewConceptGlosa() As Task
        _conceptGlosas = New ConceptGlosas() With {.State = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.GlosaSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.GlosaSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.GlosaSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
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
    End Function

    ''' <summary>
    ''' Async loader
    ''' </summary>
    ''' <param name="State"></param>
    Public Overrides Sub AsyncLoader(State As Boolean) Implements IGlosaConcept.AsyncLoader
        MyBase.AsyncLoader(State)
    End Sub
    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If _record IsNot Nothing AndAlso _record.Id > 0 Then
            Using Model As New MCustomers(Me.Tag)
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Function

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBeCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBeCode.Enabled = False
        End If
    End Sub

    Private Async Function LoadControls() As Task
        Try
            If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                    Exit Function
                End If

                Me.BarraBotones.StatusRecordVisible = True
                Using model As New MConceptGlosas(CStr(Me.Tag))
                    AsyncLoader(True)
                    _conceptGlosas = Await model.GetConceptGlosasByCode(Code)
                    If _conceptGlosas IsNot Nothing AndAlso _conceptGlosas.Id > 0 Then
                        Using ModelRecord As New MCustomers(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_conceptGlosas.Id))
                            With _conceptGlosas
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Code = .Code
                                IGlosaConcept_Name = .NameSpecific
                                Type = .Type
                                Status = .State
                                ConceptGlosaList = .ConceptGlosasUser.ToList()
                                Me.NameGeneral = .NameGeneral
                                Me.Application = .Application
                                Me.DiscountedMedicalFees = .DiscountedMedicalFees
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._conceptGlosas.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _conceptGlosas.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_conceptGlosas.Id)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True

                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewConceptGlosa()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBeCode.Focus()
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDBeCode.Enabled = False
            Throw ex
        End Try
    End Function

    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Try
                Using model As New MConceptGlosas(MyTag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me._conceptGlosas.State
                    Dim result = Await model.ChangeStateConceptGlosas(Code, state)
                    AsyncLoader(False)
                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Me._conceptGlosas = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        If result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function
#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyTag)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBeCode.ButtonClick
        OpenSearch()
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
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.GlosaSequenceDetail IsNot Nothing Then
                If Not Me._sequence.GlosaSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

End Class