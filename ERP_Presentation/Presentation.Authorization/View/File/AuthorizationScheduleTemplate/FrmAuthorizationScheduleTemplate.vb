'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/03/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports DevExpress.Xpo
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Authorization.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls

#End Region

Public Class FrmAuthorizationScheduleTemplate
    Implements IAuthorizationScheduleTemplate

#Region "Globals"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As AuthorizationSequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordAuthorization

    ''' <summary>
    ''' Representa el presentador de rangos uvr
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PAuthorizationScheduleTemplate

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Authorization"

    ''' <summary>
    ''' representa la entidad de grupo
    ''' </summary>
    ''' <remarks></remarks>
    Private AuthorizationScheduleTemplate As AuthorizationScheduleTemplate

    ''' <summary>
    ''' Usuario xpo
    ''' </summary>
    ''' <remarks></remarks>
    Private _usersXpo As Infrastructure.Data.Xpo.SecurityRepository.UserXpo

    ''' <summary>
    ''' Listado del detallo de usarios autorizados
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListAuthorizationScheduleTemplateUsers As List(Of AuthorizationScheduleTemplateUsers)

    ''' <summary>
    ''' Listado de eliminados de usarios autorizados
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteAuthorizationScheduleTemplateUsers As List(Of AuthorizationScheduleTemplateUsers)

    ''' <summary>
    ''' Listado de ids de eliminados
    ''' </summary>
    Dim ListDeleteIds As List(Of Integer)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements IAuthorizationScheduleTemplate.Status
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

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAuthorizationScheduleTemplate.ActionsOnControls
        Set(value As Boolean)
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleSchedule.Enabled = value
            INDscScheduler.Enabled = value
            INDsleUsers.Enabled = value
            INDbtnAddUser.Enabled = value
            INDgcUsers.Enabled = value

            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el consecutivo del grupo
    ''' </summary>
    Public Property Code As String Implements IAuthorizationScheduleTemplate.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    Public Property ScheduleName As String Implements IAuthorizationScheduleTemplate.ScheduleName
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IAuthorizationScheduleTemplate.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IAuthorizationScheduleTemplate.MyTag
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
    Public Property Sequense As AuthorizationSequence Implements IAuthorizationScheduleTemplate.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As AuthorizationSequence)
            Me._sequense = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As AuthorizationSequenceDetail In Me._sequense.AuthorizationSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Id del usuario
    ''' </summary>
    ''' <returns></returns>
    Public Property UserId As Integer? Implements IAuthorizationScheduleTemplate.UserId
        Get
            Return INDsleUsers.EditValue
        End Get
        Set(value As Integer?)
            INDsleUsers.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del usuario
    ''' </summary>
    ''' <returns></returns>
    Public Property UserXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements IAuthorizationScheduleTemplate.UserXpo
        Get
            Return INDsleUsers.Properties.DataSource
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleUsers.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Aplica a desistimiento
    ''' </summary>
    ''' <returns></returns>
    Public Property Schedule As Integer Implements IAuthorizationScheduleTemplate.Schedule
        Get
            Return INDsleSchedule.EditValue
        End Get
        Set(value As Integer)
            INDsleSchedule.EditValue = value
        End Set
    End Property

#End Region

#Region "Crud"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If AuthorizationScheduleTemplate IsNot Nothing AndAlso AuthorizationScheduleTemplate.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MAuthorizationScheduleTemplate(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteAuthorizationScheduleTemplate(AuthorizationScheduleTemplate)
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

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Me.AsyncLoader(True)
        Try
            Using model As New MAuthorizationScheduleTemplate(Me.Tag.ToString())
                Dim result = Await model.SaveAuthorizationScheduleTemplate(AuthorizationScheduleTemplate, _idCurrentSequense)
                If result.StateResult Then
                    If AuthorizationScheduleTemplate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._sequense.AuthorizationSequenceDetail(0).Id).RemoveAt(0)
                        End If
                        If Me._sequense.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf AuthorizationScheduleTemplate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.AuthorizationScheduleTemplate = result.ObjectEmbbeded
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
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequense Is Nothing OrElse _sequense.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequense.IsManual Then
            Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
            Deshacer()
        Else
            Await NewBillingGroup()
        End If
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Crea el objeto para el listado de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateUser()
        If ListAuthorizationScheduleTemplateUsers Is Nothing Then
            ListAuthorizationScheduleTemplateUsers = New List(Of AuthorizationScheduleTemplateUsers)
        Else
            If ListAuthorizationScheduleTemplateUsers.FindAll(Function(item) item.UserId = UserId).ToList().Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("UserDuplicated", "Inventory")
                INDsleUsers.Focus()
                Exit Sub
            End If
        End If
        Dim AuthorizationScheduleTemplateUsers As New AuthorizationScheduleTemplateUsers
        With AuthorizationScheduleTemplateUsers
            .UserId = _usersXpo.Id
            .UserCode = _usersXpo.UserCode
            .FullNameUser = _usersXpo.IdPerson.Fullname
        End With
        ListAuthorizationScheduleTemplateUsers.Add(AuthorizationScheduleTemplateUsers)
        INDgcUsers.DataSource = Nothing
        INDgcUsers.DataSource = ListAuthorizationScheduleTemplateUsers
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UserAggregatedSatisfactory", "Inventory")
        UserId = Nothing
        INDsleUsers.Focus()
    End Sub

    ''' <summary>
    ''' Elimina un usuario de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteUser()
        Dim AuthorizationScheduleTemplateUsers As AuthorizationScheduleTemplateUsers = CType(viewUsersGrid.GetFocusedRow, AuthorizationScheduleTemplateUsers)
        If AuthorizationScheduleTemplateUsers.Id <> 0 Then
            If ListDeleteAuthorizationScheduleTemplateUsers Is Nothing Then
                ListDeleteAuthorizationScheduleTemplateUsers = New List(Of AuthorizationScheduleTemplateUsers)
            End If
            AuthorizationScheduleTemplateUsers.MarkAsDeleted()
            ListDeleteAuthorizationScheduleTemplateUsers.Add(AuthorizationScheduleTemplateUsers)
        End If
        ListAuthorizationScheduleTemplateUsers.Remove(AuthorizationScheduleTemplateUsers)
        INDgcUsers.DataSource = Nothing
        INDgcUsers.DataSource = ListAuthorizationScheduleTemplateUsers
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        '_searchMode = True
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAuthorizationScheduleTemplate
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
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    Private Sub CleanControls()
        INDlyRoot.BeginUpdate()
        ActionsOnControls = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Status = True
        Code = String.Empty
        ScheduleName = String.Empty
        Schedule = Nothing
        UserId = Nothing
        INDgcUsers.DataSource = Nothing
        ListAuthorizationScheduleTemplateUsers = Nothing
        ListDeleteAuthorizationScheduleTemplateUsers = Nothing
        ListDeleteIds = Nothing
        AuthorizationScheduleTemplate = Nothing
        _usersXpo = Nothing
        INDscScheduler.Storage.Appointments.Clear()
        INDlyRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    Private Sub AssigningValues()
        With AuthorizationScheduleTemplate
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = ScheduleName
            .Schedule = Schedule

            If INDscScheduler.Storage.Appointments.Items.Count > 0 Then
                For Each itemAppointment In INDscScheduler.Storage.Appointments.Items
                    If itemAppointment.Id IsNot Nothing AndAlso itemAppointment.Id > 0 Then
                        Dim entity = (From x In .AuthorizationScheduleTemplateSchedule Where x.Id = itemAppointment.Id Select x).FirstOrDefault()
                        If entity IsNot Nothing Then
                            entity.InitialTime = itemAppointment.Start.TimeOfDay
                            entity.EndingTime = itemAppointment.End.TimeOfDay
                            entity.NumberHour = entity.EndingTime.TotalHours - entity.InitialTime.TotalHours

                            entity.NextDay = False
                            If itemAppointment.Start.Day = GetDateServer().AddDays(1).Day OrElse itemAppointment.End.Day = GetDateServer().AddDays(1).Day Then
                                entity.NextDay = True
                            End If

                            entity.MarkAsModified()
                        End If
                    Else
                        Dim entity = New AuthorizationScheduleTemplateSchedule
                        entity.InitialTime = itemAppointment.Start.TimeOfDay
                        entity.EndingTime = itemAppointment.End.TimeOfDay
                        entity.NumberHour = entity.EndingTime.TotalHours - entity.InitialTime.TotalHours
                        entity.Status = True

                        entity.NextDay = False
                        If itemAppointment.Start.Day = GetDateServer().AddDays(1).Day OrElse itemAppointment.End.Day = GetDateServer().AddDays(1).Day Then
                            entity.NextDay = True
                        End If

                        .AuthorizationScheduleTemplateSchedule.Add(entity)
                    End If
                Next
            End If

            If ListAuthorizationScheduleTemplateUsers IsNot Nothing AndAlso ListAuthorizationScheduleTemplateUsers.Count > 0 Then
                ListAuthorizationScheduleTemplateUsers.ForEach(Sub(item) .AuthorizationScheduleTemplateUsers.Add(item))
            End If

            If ListDeleteAuthorizationScheduleTemplateUsers IsNot Nothing AndAlso ListDeleteAuthorizationScheduleTemplateUsers.Count > 0 Then
                ListDeleteAuthorizationScheduleTemplateUsers.ForEach(Sub(item) .AuthorizationScheduleTemplateUsers.Add(item))
            End If

            If ListDeleteIds IsNot Nothing AndAlso ListDeleteIds.Count > 0 Then
                For Each item In ListDeleteIds
                    Dim info = (From x In .AuthorizationScheduleTemplateSchedule Where x.Id = item Select x).FirstOrDefault()
                    If info IsNot Nothing Then
                        info.MarkAsDeleted()
                    End If
                Next
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewBillingGroup() As Task
        AuthorizationScheduleTemplate = New AuthorizationScheduleTemplate() With {.Status = True}
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.AuthorizationSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.AuthorizationSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequense.AuthorizationSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequense.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
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

    Private Async Sub DeleteBlockedRecord()
        Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Sub

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Me.BarraBotones.StatusRecordVisible = True
            Using Model As New MAuthorizationScheduleTemplate(CStr(Me.Tag))
                AsyncLoader(True)
                AuthorizationScheduleTemplate = Await Model.GetAuthorizationScheduleTemplate(INDbtnCode.Text.Trim)
                If AuthorizationScheduleTemplate IsNot Nothing AndAlso AuthorizationScheduleTemplate.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(AuthorizationScheduleTemplate.Id))
                        With AuthorizationScheduleTemplate
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code
                            ScheduleName = .Name
                            Schedule = .Schedule
                            Status = .Status

                            ListAuthorizationScheduleTemplateUsers = .AuthorizationScheduleTemplateUsers.ToList
                            INDgcUsers.DataSource = Nothing
                            INDgcUsers.DataSource = ListAuthorizationScheduleTemplateUsers

                            If .AuthorizationScheduleTemplateSchedule IsNot Nothing AndAlso .AuthorizationScheduleTemplateSchedule.Count > 0 Then
                                For Each itemSchedule In .AuthorizationScheduleTemplateSchedule.ToList()

                                    Dim setDateInitial As DateTime = GetDateServer()
                                    If itemSchedule.InitialTime < itemSchedule.EndingTime AndAlso itemSchedule.NextDay = True Then
                                        setDateInitial = GetDateServer().AddDays(1)
                                    End If

                                    Dim setDateEnd As DateTime = GetDateServer()
                                    If itemSchedule.NextDay = True Then
                                        setDateEnd = GetDateServer().AddDays(1)
                                    End If

                                    Dim Appointment = INDscScheduler.Storage.CreateAppointment(AppointmentType.Normal)
                                    Appointment.Start = New Date(setDateInitial.Year, setDateInitial.Month, setDateInitial.Day, itemSchedule.InitialTime.Hours, itemSchedule.InitialTime.Minutes, itemSchedule.InitialTime.Seconds)
                                    Appointment.End = New Date(setDateEnd.Year, setDateEnd.Month, setDateEnd.Day, itemSchedule.EndingTime.Hours, itemSchedule.EndingTime.Minutes, itemSchedule.EndingTime.Seconds)
                                    Appointment.Subject = vbNewLine & vbNewLine & itemSchedule.InitialTime.ToString() & "   a   " & itemSchedule.EndingTime.ToString()
                                    SchedulerStorage1.SetAppointmentId(Appointment, itemSchedule.Id)
                                    INDscScheduler.Storage.Appointments.Add(Appointment)
                                Next
                            End If
                        End With

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.AuthorizationScheduleTemplate.Code)
                        If record.Id = 0 Then
                            record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordAuthorization With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = AuthorizationScheduleTemplate.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(AuthorizationScheduleTemplate.Id)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    End Using
                Else
                    AsyncLoader(False)
                    If Me._sequense.IsManual Then
                        Await Me.NewBillingGroup()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Code = String.Empty
                        INDbtnCode.Focus()
                    End If
                End If
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.AuthorizationScheduleTemplate.Code, Me.AuthorizationScheduleTemplate.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.AuthorizationScheduleTemplate.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.AuthorizationScheduleTemplate.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.AuthorizationScheduleTemplate.Code, Me.AuthorizationScheduleTemplate.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.AuthorizationScheduleTemplate.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MAuthorizationScheduleTemplate(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not Me.AuthorizationScheduleTemplate.Status
                Dim Result = Await model.ChangeState(Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Me.AuthorizationScheduleTemplate = Result.ObjectEmbbeded
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.AuthorizationScheduleTemplate IsNot Nothing AndAlso Me.AuthorizationScheduleTemplate.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "Handles"

#Region "Load"

    Private Sub FrmAuthorizationScheduleTemplate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PAuthorizationScheduleTemplate(Me)
        Presenter.GetSequense()
        LoadStatus()
        Deshacer()

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewUsersGrid, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewUsersGrid.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        INDscScheduler.Start = Date.Now()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequense = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequense = Nothing
        record = Nothing
        Presenter = Nothing
        AuthorizationScheduleTemplate = Nothing
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAddUser_Click(sender As Object, e As EventArgs) Handles INDbtnAddUser.Click
        If INDsleUsers.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedUser", "Inventory")
            INDsleUsers.Focus()
            Exit Sub
        End If

        CreateUser()
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmAuthorizationScheduleTemplate_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequense Is Nothing OrElse _sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewBillingGroup()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmAuthorizationScheduleTemplate_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleUsers_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleUsers.QueryPopUp
        If UserXpo Is Nothing Then
            Presenter.InitializeUsers()
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDsleUsers_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleUsers.EditValueChanging
        If e.NewValue IsNot Nothing Then
            _usersXpo = INDsleUsers.GetFocusedRow(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)()
        End If
    End Sub

#End Region

#Region "ContextMenu"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteUser()
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteUser()
    End Sub

#End Region

#Region "CustomDrawDayHeader"

    Private Sub INDscScheduler_CustomDrawDayHeader(sender As Object, e As CustomDrawObjectEventArgs) Handles INDscScheduler.CustomDrawDayHeader
        Dim fechaHoy As Date = GetDateServer()
        Dim fechaMan As Date = fechaHoy.AddDays(1)
        Dim dayHeader As DayHeader = e.ObjectInfo
        Dim texto As String
        Dim innerRect As Rectangle = Rectangle.Inflate(e.Bounds, -1, -1)
        If fechaHoy.Day = dayHeader.Interval.Start.Day Then
            texto = "Día 1"
        Else
            texto = "Día 2"
        End If
        e.Cache.FillRectangle(Brushes.White, innerRect)
        e.Cache.DrawString(texto, dayHeader.Appearance.HeaderCaption.Font, New SolidBrush(Color.Black), innerRect, dayHeader.Appearance.HeaderCaption.GetStringFormat())
        e.Handled = True
    End Sub

#End Region

#Region "PopupMenuShowing"

    Private Sub INDscScheduler_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDscScheduler.PopupMenuShowing
        If e.Menu.Id = DevExpress.XtraScheduler.SchedulerMenuItemId.DefaultMenu Then 'Si es el menu por default
            'Quito todas las opciones que no neceito sobre el menu"
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.SwitchViewMenu)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAllDayEvent)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringAppointment)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringEvent)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoToday)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoDate)
            'Busco el item "Nueva cita", y le cambio el nombre
            Dim item As SchedulerMenuItem = e.Menu.GetMenuItemById(SchedulerMenuItemId.NewAppointment)
            If (item IsNot Nothing) Then item.Caption = "Nuevo Rango de Horas"
        ElseIf e.Menu.Id = DevExpress.XtraScheduler.SchedulerMenuItemId.AppointmentMenu Then 'Si es el menu que se despliega cuando dan click derecho sobre un appointment que ya esta creado
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.EditSeries)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.StatusSubMenu)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.LabelSubMenu)
            Dim itemAbrir As SchedulerMenuItem = e.Menu.GetMenuItemById(SchedulerMenuItemId.OpenAppointment)
            If (itemAbrir IsNot Nothing) Then itemAbrir.Caption = "Editar Rango de Horas"
        End If
    End Sub

#End Region

#Region "EditAppointmentFormShowing"

    Private Sub INDscScheduler_EditAppointmentFormShowing(sender As Object, e As AppointmentFormEventArgs) Handles INDscScheduler.EditAppointmentFormShowing
        Using formulario As New FrmRangeHours()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddAppointmentArgs, AddressOf ReturnAddAppointmentEventArgs
            formulario.Appointment = e.Appointment
            formulario.ToolBar.Visible = False
            formulario.Size = New System.Drawing.Size(450, 320)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            e.DialogResult = transparent.ShowDialog()
            e.Handled = True
        End Using
    End Sub

    Private Sub ReturnAddAppointmentEventArgs(sender As Object, e As AddAppointmentEventArgs)
        If e IsNot Nothing Then
            INDscScheduler.Storage.Appointments.Add(e.Appointment)
        End If
    End Sub

#End Region

#Region "AppointmentDeleting"

    Private Sub SchedulerStorage1_AppointmentDeleting(sender As Object, e As PersistentObjectCancelEventArgs) Handles SchedulerStorage1.AppointmentDeleting
        Dim AppointementInstance = e.Object
        If AppointementInstance.Id IsNot Nothing AndAlso AppointementInstance.Id > 0 Then
            If ListDeleteIds Is Nothing Then
                ListDeleteIds = New List(Of Integer)
            End If
            ListDeleteIds.Add(CInt(AppointementInstance.Id))
        End If
    End Sub

#End Region

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
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        '_searchMode = False
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
            If Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.AuthorizationSequenceDetail IsNot Nothing Then
                If Not Me._sequense.AuthorizationSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                    '_idCurrentSequense = Me._sequense.BillingSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                    'Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    'Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class