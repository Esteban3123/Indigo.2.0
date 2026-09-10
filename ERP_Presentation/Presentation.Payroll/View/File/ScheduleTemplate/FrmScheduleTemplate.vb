'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Cristhian Mauricio Salazar Narvaez
' Created          : 30-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Payroll.MVP
Imports Presentation.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Controls
Imports Domain.Payroll.Entities
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Services
Imports DevExpress.XtraScheduler.Commands
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraEditors
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraScheduler.Drawing
Imports System.Drawing
Imports Infrastructure.CrossCutting.Resources


Public Class FrmScheduleTemplate
    Implements IScheduleTemplate

#Region "Fields"

    ''' <summary>
    ''' VAriable que contiene los fields nulls para la personalizacion de campos
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtFieldsCustomizables As DataTable
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Variable para almacenar la listas de unidades funcionales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListfunctionalUnits As List(Of FunctionalUnit)

    ''' <summary>
    ''' variable para almacenar la plantilla de turno
    ''' </summary>
    ''' <remarks></remarks>
    Dim scheduleTemplate As ScheduleTemplate

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PScheduleTemplate

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable que contiene el registro bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim record As Domain.Entities.BlockRecord

    Dim FlagEdit As Boolean = False


#End Region


    ''' <summary>
    ''' Propiedad del estado del cargo
    ''' </summary>
    Public Property Status As Boolean

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

#Region "Implementacion de IScheduleTemplate"

    Public Sub AbrirBusqueda() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ScheduleTemplate
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Codigo", .FieldName = "Code"}, New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name"}, New ColumnInfo() With {.Caption = "Jornada", .FieldName = "Letter"}}.ToList
            BarraBotones.PrepareToolbar(eAction.New)
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
        INDTxtCode.Text = ReturnValue
        If INDTxtCode.Text <> String.Empty Then
            Await LoadControls()
            If INDTxtCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDTxtCode.Enabled = False
        End If
    End Sub

    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        If scheduleTemplate IsNot Nothing Then
            If scheduleTemplate.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    While scheduleTemplate.ScheduleTemplateFunctionalUnit.Count > 0 'Marcos las unidades funcionales
                        With scheduleTemplate.ScheduleTemplateFunctionalUnit
                            .Item(.Count - 1).MarkAsDeleted()
                        End With
                    End While
                    While scheduleTemplate.ScheduleTemplateConcept.Count > 0
                        With scheduleTemplate.ScheduleTemplateConcept
                            While .Item(.Count() - 1).ScheduleTemplateConceptDetail.Count > 0
                                Dim index = .Item(.Count() - 1).ScheduleTemplateConceptDetail.Count() - 1
                                .Item(.Count() - 1).ScheduleTemplateConceptDetail.Item(index).MarkAsDeleted()
                            End While
                            .Item(.Count() - 1).MarkAsDeleted()
                        End With
                    End While
                    scheduleTemplate.MarkAsDeleted()
                    Using model As New MScheduleControl
                        AsyncLoader(True)
                        If Await model.SaveScheduleTemplateAsync(scheduleTemplate) = True Then ' Este guardar realmente elimina ya que habia marcado el objeto como eliminado
                            Await Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                        End If
                        AsyncLoader(False)
                    End Using
                    Deshacer()
                End If
            End If
        End If
    End Sub

    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        INDTxtName.Focus()
        AssigningValues()
        AsyncLoader(True)
        Dim banderaGuardado As Integer = 0

        Using model As New MScheduleControl
            If Await model.SaveScheduleTemplateAsync(scheduleTemplate) = True Then
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                banderaGuardado = 1
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
            End If
        End Using
        AsyncLoader(False)
        If banderaGuardado = 1 Then
            Deshacer()
        End If
    End Sub

    Private Sub AssigningValues()
        With scheduleTemplate
            .Code = INDTxtCode.Text
            .Name = INDTxtName.Text
            .Letter = INDCmbSchedule.EditValue
            .ScheduleTemplateConcept.All(Function(x)
                                             x.ScheduleTemplateConceptDetail.All(Function(y)
                                                                                     Dim idConcept As Integer = y.ConceptId
                                                                                     y.Concept = Nothing
                                                                                     y.ConceptId = idConcept
                                                                                     Return True
                                                                                 End Function)
                                             Return True
                                         End Function)
            For Each functional As FunctionalUnit In ListfunctionalUnits
                Dim objFunctional = .ScheduleTemplateFunctionalUnit.Where(Function(x) x.FunctionalUnitId = functional.Id)
                If objFunctional.Count > 0 Then
                    If functional.Apply = False Then
                        objFunctional.SingleOrDefault().MarkAsDeleted()
                    End If
                Else ' si no existe el registro en la propiedad de navegacion
                    If functional.Apply = True Then
                        Dim addFunctional As New ScheduleTemplateFunctionalUnit()
                        addFunctional.FunctionalUnit = functional
                        .ScheduleTemplateFunctionalUnit.Add(addFunctional)
                    End If
                End If
            Next
        End With
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDTxtCode.Text = String.Empty Then
            INDTxtCode.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDTxtName.Text = String.Empty Then
            INDTxtName.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDTxtGroupName.Text = String.Empty Then
            INDTxtGroupName.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDScheduleControl.Storage.Appointments.Count = 0 Then
            ValidateControls = False
            Exit Function
        End If
    End Function

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

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

    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo

    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements IScheduleTemplate.ActionsOnControls
        Set(value As Boolean)
            INDTxtCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDTxtGroupCode.Enabled = value
            INDTxtGroupName.Enabled = value
            INDCmbSchedule.Enabled = value
            Me.BarraBotones.StatusRecordEnabled = value
            INDGlueEmpresa.Enabled = value
            INDScheduleControl.Enabled = value
            If value = False Then
                INDTxtCode.Focus()
            Else
                INDTxtName.Focus()
            End If
        End Set
    End Property

#End Region

#Region "Eventos Formulario"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        ListfunctionalUnits = Nothing
        scheduleTemplate = Nothing
        Presenter = Nothing
        PathFunctionalDefinitions = Nothing
        record = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara cuando se carga por completo todo el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmScheduleTemplate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc

        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollConcepts.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PScheduleTemplate(Me)
        INDScheduleControl.Start = Date.Now()
        'SchedulerStorage1.Appointments.Add(New AppointmentConcept(Date.Now(), Date.Now.AddHours(4), "Concepto: Sueldo", Nothing))
        Using model As New MCompany(MCompany.TAG)
            INDGlueEmpresa.Properties.DataSource = Await model.ListAllCompanyAsync()
            Dim todos As New Company()
            todos.Nit = "--"
            todos.Name = "Todas"
            CType(INDGlueEmpresa.Properties.DataSource, List(Of Company)).Insert(0, todos)
            INDGlueEmpresa.EditValue = todos
        End Using
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' evento que se dispara cuando presionan click sobre el boton que tiene el textedit txtCode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDTxtCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Se dispara cuando se carga por completo la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub
    ''' <summary>
    ''' evento que se dispara cuando presionan click sobre el boton que tiene el textedit txtGroupCode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtGroupCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDTxtGroupCode.ButtonClick
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        Dim formListaGrupos As New FrmBusqueda
        With formListaGrupos
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Codigo", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripcion", .FieldName = "Descripcion"}}.ToList
            .ShowDialog()
            INDTxtGroupCode.Text = .ValorDevuelto
            .Dispose()
        End With
    End Sub
    ''' <summary>
    ''' evento que se dispara cuando presionan una tecla sobre el control txtCode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDTxtCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTxtCode.KeyDown
        If Not String.IsNullOrEmpty(INDTxtCode.Text) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                Await LoadControls()
                If INDTxtCode.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
                End If
            ElseIf e.KeyCode = Keys.F4 Then
                AbrirBusqueda()
            End If
        End If
    End Sub
    ''' <summary>
    ''' evento que se dispara cuando presionan una tecla sobre el control txtGroupCode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDTxtGroupCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTxtGroupCode.KeyDown
        If Not String.IsNullOrEmpty(INDTxtCode.Text) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                Using model As New MGroups(MGroups.TAG)
                    scheduleTemplate.Group = Await model.GetGroupAsync(INDTxtGroupCode.Text)
                End Using
                With scheduleTemplate.Group
                    INDTxtGroupCode.Text = .Code
                    INDTxtGroupName.Text = .Name
                End With
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara cuando se abre el menu con el click derecho sobre el control de Schedule
    ''' 
    ''' en este metodo lo que hacemos es quitar las opciones que viene por default en el menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDScheduleControl_PopupMenuShowing(sender As Object, e As DevExpress.XtraScheduler.PopupMenuShowingEventArgs) Handles INDScheduleControl.PopupMenuShowing
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
            FlagEdit = False
        ElseIf e.Menu.Id = DevExpress.XtraScheduler.SchedulerMenuItemId.AppointmentMenu Then 'Si es el menu que se despliega cuando dan click derecho sobre un appointment que ya esta creado
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.EditSeries)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.StatusSubMenu)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.LabelSubMenu)
            Dim itemAbrir As SchedulerMenuItem = e.Menu.GetMenuItemById(SchedulerMenuItemId.OpenAppointment)
            itemAbrir.Caption = "Editar Rango de Horas"
            FlagEdit = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se edita un appointment
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDScheduleControl_EditAppointmentFormShowing(sender As Object, e As AppointmentFormEventArgs) Handles INDScheduleControl.EditAppointmentFormShowing
        Dim appConcept As AppointmentConcept
        If e.Appointment.GetType = GetType(AppointmentConcept) Then
            appConcept = CType(e.Appointment, AppointmentConcept)
        Else
            'appConcept = New AppointmentConcept(e.Appointment.Start, e.Appointment.End, "", New ScheduleTemplateConcept())
            appConcept = New AppointmentConcept(e.Appointment, New ScheduleTemplateConcept())
            If appConcept.Appointment.Start.Day = Date.Now().AddDays(1).Day Then
                appConcept.ScheduleTemplate.NextDay = True
            End If
        End If

        Dim form As FrmAppointmentConcept = New FrmAppointmentConcept(INDScheduleControl, appConcept, scheduleTemplate)
        form.FlagEdit = FlagEdit
        form.LookAndFeel.ParentLookAndFeel = INDScheduleControl.LookAndFeel
        form.StartPosition = FormStartPosition.CenterScreen
        Dim frmTransparent As New FrmTransparent(form, False)
        e.DialogResult = frmTransparent.ShowDialog()
        INDScheduleControl.Refresh()
        e.Handled = True
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se trata de eliminar un appointment
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SchedulerStorage1_AppointmentDeleting(sender As Object, e As PersistentObjectCancelEventArgs) Handles SchedulerStorage1.AppointmentDeleting

        Dim appConcept As AppointmentConcept
        If e.Object.GetType = GetType(AppointmentConcept) Then
            appConcept = CType(e.Object, AppointmentConcept)
        Else
            'appConcept = New AppointmentConcept(e.Appointment.Start, e.Appointment.End, "", New ScheduleTemplateConcept())
            appConcept = New AppointmentConcept(e.Object, New ScheduleTemplateConcept())
            If appConcept.Appointment.Start.Day = Date.Now().AddDays(1).Day Then
                appConcept.ScheduleTemplate.NextDay = True
            End If
        End If

        Dim IdDelete As Integer = 0

        Dim ObjTemp = scheduleTemplate.ScheduleTemplateConcept.Where(Function(x) x.InitialTime = appConcept.Appointment.Start.TimeOfDay And x.EndingTime = appConcept.Appointment.End.TimeOfDay).FirstOrDefault()

        IdDelete = ObjTemp.Id

        Dim indice As Integer
        While appConcept.ScheduleTemplate.ScheduleTemplateConceptDetail.Count > 0 ' Elimino los agregados de ese appointment
            indice = appConcept.ScheduleTemplate.ScheduleTemplateConceptDetail.Count - 1
            appConcept.ScheduleTemplate.ScheduleTemplateConceptDetail(indice).MarkAsDeleted()
        End While

        appConcept.ScheduleTemplate.MarkAsDeleted()

        If IdDelete > 0 Then

            Dim ObjDelete = scheduleTemplate.ScheduleTemplateConcept.Where(Function(x) x.Id = ObjTemp.Id).FirstOrDefault()

            While ObjDelete.ScheduleTemplateConceptDetail.Count > 0 ' Elimino los agregados de ese appointment
                indice = ObjDelete.ScheduleTemplateConceptDetail.Count - 1

                ObjDelete.ScheduleTemplateConceptDetail(indice).MarkAsDeleted()
            End While

            ObjDelete.MarkAsDeleted()

        End If

    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cambia el valor del gridlookupEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGlueEmpresa_EditValueChanged(sender As Object, e As EventArgs) Handles INDGlueEmpresa.EditValueChanged
        Dim item As Company = CType(CType(sender, GridLookUpEdit).EditValue, Company)
        If item.Id <> 0 Then
            INDGcUnidad.DataSource = ListfunctionalUnits.Where(Function(x) x.BranchOffice.CompanyId = item.Id)
        Else
            INDGcUnidad.DataSource = ListfunctionalUnits
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara cuando se da click en deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer, BarraBotones.ClickNuevo
        CleanControls()
    End Sub
    ''' <summary>
    ''' Evento que se dispara cuando se presiona click sobre buscar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub
    ' ''' <summary>
    ' ''' Evento que se dispara cuando dan click en customizar
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
    '    CustomizationOpen()
    'End Sub
    ''' <summary>
    ''' Evento que se dispara cuando se da click en guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando damos click sobre el boton actualizar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub
    ' ''' <summary>
    ' ''' Evento que se dispara al presionar el boton de restablecer layout
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
    '    ResetLayout()
    'End Sub
    ''' <summary>
    ''' Evento que se dispara al dar click en eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Este evento se dispara cuando se van asignar los captions a las cabeceras de las columnas del schedule
    ''' 
    ''' Lo que hacemos en este metodo es cambiar el nombre de las columnas a "Dia 1" y "Dia 2"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDScheduleControl_CustomDrawDayHeader(sender As Object, e As CustomDrawObjectEventArgs) Handles INDScheduleControl.CustomDrawDayHeader
        Dim fechaHoy As Date = Date.Now()
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

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.scheduleTemplate IsNot Nothing AndAlso Me.scheduleTemplate.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), Botones.SiNo, MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes)) = System.Windows.Forms.DialogResult.Yes Then
                Me.INDTxtCode.Text = Me.IdEntity.Trim()
                Me.ViewModeEditHold = True
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDTxtCode.Text = Me.IdEntity.Trim()
            Me.ViewModeEditHold = True
            Await Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmScheduleTemplateMetaData, Eform.InfoMetaData), Me.scheduleTemplate.Code, Me.scheduleTemplate.Name, Me.INDCmbSchedule.Properties.Items(Me.INDCmbSchedule.SelectedIndex).Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.scheduleTemplate.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmScheduleTemplateMetaDataTitle, Eform.InfoMetaData), Me.scheduleTemplate.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmScheduleTemplateMetaData, Eform.InfoMetaData), Me.scheduleTemplate.Code, Me.scheduleTemplate.Name, Me.INDCmbSchedule.Properties.Items(Me.INDCmbSchedule.SelectedIndex).Description)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmScheduleTemplateMetaDataTitle, Eform.InfoMetaData), Me.scheduleTemplate.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Funcion la cual se encarga de limpiar todos los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        ActionsOnControls = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        INDTxtCode.Text = String.Empty
        INDTxtName.Text = String.Empty
        INDTxtGroupCode.Text = String.Empty
        INDTxtGroupName.Text = String.Empty
        INDCmbSchedule.SelectedIndex = 1
        INDGcUnidad.DataSource = Nothing
        INDGlueEmpresa.EditValue = CType(INDGlueEmpresa.Properties.DataSource, List(Of Company)).Item(0)
        INDScheduleControl.Storage.Appointments.Clear()
        Me.BarraBotones.StatusRecord = True
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        DeleteBlockedRecord()
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MScheduleControl
                Await Model.DeleteBlockRecord(record)
            End Using
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' funcion encargada de cargar la informacion de la plantilla una vez l dan enter sobre el codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = True
        AsyncLoader(True)
        Using Model As New MScheduleControl
            scheduleTemplate = Await Model.GetScheduleTemplateAsync(INDTxtCode.Text)
        End Using
        Using model As New MFunctionalUnit(MFunctionalUnit.TAG)
            ListfunctionalUnits = Await model.ListAllAsync()
            INDGcUnidad.DataSource = ListfunctionalUnits
            If scheduleTemplate IsNot Nothing Then
                If scheduleTemplate.Id > 0 Then
                    Dim result = Await model.GetBlockRecord(Me.Tag, scheduleTemplate.Id)

                    With scheduleTemplate
                        Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
                        INDTxtName.Text = .Name
                        INDTxtGroupCode.Text = .Group.Code
                        INDTxtGroupName.Text = .Group.Name
                        SeleccionarItemHorario(.Letter)
                        Status = .State
                        SeleccionarUnidadesPlantilla()
                        CrearAppointment(.ScheduleTemplateConcept)
                    End With
                    Me.GetDocumentIndexed(Me.Tag & "_" & Me.scheduleTemplate.Code)

                    If result.Id = 0 Then
                        Me.BarraBotones.SetDocuments(scheduleTemplate.Id)
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = scheduleTemplate.Id}
                        Dim operation = Await model.SaveBlockRecord(record)
                        record = operation.ObjectEmbbeded
                    Else
                        record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                End If
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                scheduleTemplate = New ScheduleTemplate() With {.State = True}
            End If
        End Using
        AsyncLoader(False)
        ActionsOnControls = True
    End Function

    ''' <summary>
    ''' Funcion la cual es la encargada de dibijar los appointment en el scheduleControl
    ''' </summary>
    ''' <param name="listaConceptos"></param>
    ''' <remarks></remarks>
    Private Sub CrearAppointment(ByVal listaConceptos As TrackableCollection(Of ScheduleTemplateConcept))
        For Each item As ScheduleTemplateConcept In listaConceptos
            Dim Appointment = INDScheduleControl.Storage.CreateAppointment(AppointmentType.Normal)
            Dim DateHoy = Date.Now()
            Dim DateMa = Date.Now().AddDays(1)
            Dim DatePasMa = Date.Now().AddDays(2)
            Dim startDate As Date
            Dim EndDate As Date
            If item.NextDay = False Then
                startDate = New Date(DateHoy.Year, DateHoy.Month, DateHoy.Day, item.InitialTime.Hours, item.InitialTime.Minutes, item.InitialTime.Seconds)
                If item.EndingTime.Hours = 0 And item.EndingTime.Minutes = 0 Then
                    EndDate = New Date(DateMa.Year, DateMa.Month, DateMa.Day, item.EndingTime.Hours, item.EndingTime.Minutes, item.EndingTime.Seconds)
                Else
                    EndDate = New Date(DateHoy.Year, DateHoy.Month, DateHoy.Day, item.EndingTime.Hours, item.EndingTime.Minutes, item.EndingTime.Seconds)
                End If
            Else
                startDate = New Date(DateMa.Year, DateMa.Month, DateMa.Day, item.InitialTime.Hours, item.InitialTime.Minutes, item.InitialTime.Seconds)
                If item.EndingTime.Hours = 0 And item.EndingTime.Minutes = 0 Then
                    EndDate = New Date(DatePasMa.Year, DatePasMa.Month, DatePasMa.Day, item.EndingTime.Hours, item.EndingTime.Minutes, item.EndingTime.Seconds)
                Else
                    EndDate = New Date(DateMa.Year, DateMa.Month, DateMa.Day, item.EndingTime.Hours, item.EndingTime.Minutes, item.EndingTime.Seconds)
                End If
            End If

            Appointment.Start = startDate
            Appointment.End = EndDate
            Appointment.Subject = FrmAppointmentConcept.DescripcionAppointment(item.InitialTime, item.EndingTime, item.ScheduleTemplateConceptDetail)
            Appointment.StatusId = AppointmentStatusType.Busy

            Dim appConcept As New AppointmentConcept(Appointment, item)

            INDScheduleControl.Storage.Appointments.Add(appConcept.Appointment)
        Next
    End Sub

    ''' <summary>
    ''' Posiciona el combobox en la letra que le envien como parametro
    ''' </summary>
    ''' <param name="letra"></param>
    ''' <remarks></remarks>
    Private Sub SeleccionarItemHorario(letra As String)
        For Each item As ImageComboBoxItem In INDCmbSchedule.Properties.Items
            If item.Value = letra Then
                INDCmbSchedule.SelectedItem = item
            End If
        Next
    End Sub

    ''' <summary>
    ''' Chequea las unidades funcionales que tenga el objeto ScheduleTemplate
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SeleccionarUnidadesPlantilla()
        For Each item As ScheduleTemplateFunctionalUnit In scheduleTemplate.ScheduleTemplateFunctionalUnit
            Dim QueryUnidad = ListfunctionalUnits.Where(Function(x) x.Id = item.FunctionalUnitId)
            If QueryUnidad.Count > 0 Then
                Dim unidad As FunctionalUnit = QueryUnidad.FirstOrDefault()
                unidad.Apply = True
            End If
        Next
        INDGcUnidad.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(INDTxtCode.Text) Then
            Using model As New MScheduleControl()
                AsyncLoader(True)
                Dim state As Boolean = Not scheduleTemplate.State
                Dim Result = Await model.ChangeStateScheduleTemplate(INDTxtCode.Text, state)
                AsyncLoader(False)
                If Result = True Then
                    scheduleTemplate.State = state
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")

                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

#End Region

    '#Region "Customize"

    '    ''' <summary>
    '    ''' Metodo para abrir el formulario de customizar el frontal
    '    ''' </summary>
    '    Private Sub CustomizationOpen()
    '        INDLcScheduleTemplate.ShowCustomizationForm()
    '    End Sub

    '    ''' <summary>
    '    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    '    ''' </summary>
    '    ''' <param name="sender">The source of the event.</param>
    '    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    '    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
    '        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) = True Then
    '            ExistDefinitionFront = True
    '        End If
    '    End Sub
    '    ''' <summary>
    '    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    '    ''' </summary>
    '    ''' <param name="sender">The source of the event.</param>
    '    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    '    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
    '        If ExistDefinitionFront = True Then
    '            INDLcScheduleTemplate.RestoreLayoutFromXml(PathFunctionalDefinitions)
    '        End If
    '    End Sub
    '    ''' <summary>
    '    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    '    ''' </summary>
    '    ''' <param name="sender">The source of the event.</param>
    '    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    '    Private Async Sub INDLyConcept_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLcScheduleTemplate.ShowCustomization
    '        Try
    '            'Ejecuatamos la consulta
    '            Using model As New MScheduleControl
    '                Dim dsFields As DataSet = Await model.GetFieldsNull()
    '                If dsFields IsNot Nothing Then
    '                    dtFieldsCustomizables = dsFields.Tables(0)
    '                    For j As Integer = 0 To INDLcScheduleTemplate.Items.Count - 1
    '                        INDLcScheduleTemplate.Items.Item(j).AllowHide = False
    '                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
    '                            If Object.Equals(INDLcScheduleTemplate.Items.Item(j).Tag, Nothing) = False Then
    '                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDLcScheduleTemplate.Items.Item(j).Tag.ToString.Trim Then
    '                                    INDLcScheduleTemplate.Items.Item(j).AllowHide = True
    '                                End If
    '                            End If
    '                        Next
    '                    Next
    '                End If
    '            End Using
    '        Catch ex As Exception
    '            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
    '            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    '    ''' </summary>
    '    ''' <param name="sender">The source of the event.</param>
    '    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    '    Private Sub INDLyConcept_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLcScheduleTemplate.HideCustomization
    '        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
    '        If INDLcScheduleTemplate.IsModified = True Then
    '            Try
    '                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
    '                    INDLcScheduleTemplate.SaveLayoutToXml(PathFunctionalDefinitions)
    '                Else
    '                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
    '                End If
    '            Catch ex As Exception
    '                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
    '                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
    '            End Try
    '        End If
    '    End Sub

    '    ''' <summary>
    '    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    '    ''' </summary>
    '    Private Sub ResetLayout()
    '        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
    '            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
    '            INDLcScheduleTemplate.RestoreDefaultLayout()
    '            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
    '        End If
    '    End Sub


    '#End Region

End Class