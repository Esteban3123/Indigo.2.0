'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 09-07-2013
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Common.CustomAppointmentEditForm
Imports DevExpress.XtraScheduler
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Common.MVP
#End Region
''' <summary>
''' Clase que tiene el comportamiento de la vista en el formulario centros de estudio
''' </summary>
Public Class FrmHoliday
    Implements IHoliday


#Region "Variable"

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Varaible que contiene la entidad de los Holiday
    ''' </summary> 
    Dim Holiday As Holiday

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MHoliday

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PHoliday

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
    ''' Variable que contiene el año actual del calendario
    ''' </summary>
    Dim CurrentYear As Integer

    ''' <summary>
    ''' Contiene el formulario que se despliega para ingresar un festivo
    ''' </summary>
    ''' <remarks></remarks>
    Dim AppoinmentEditForm As MyAppointmentEditForm

    Dim TmpListHolidays As List(Of Holiday)
#End Region

#Region "Properties"
    ''' <summary>
    ''' Propiedad que contiene el control de calendario
    ''' </summary>
    Public ReadOnly Property HolidaySchedulerControl As SchedulerControl Implements IHoliday.HolidaySchedulerControl
        Get
            Return INDScHoliday
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene el Almacenamieto de las fechas en el calendario
    ''' </summary>
    Public ReadOnly Property HolidaySchedulerStorage As SchedulerStorage Implements IHoliday.HolidaySchedulerStorage
        Get
            Return INDScStorage
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene el control de navegacion de el calendario
    ''' </summary>
    Public ReadOnly Property HolidayDateNavigator As DateNavigator Implements IHoliday.HolidayDateNavigator
        Get
            Return INDDateNavigator
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene el año actual de el año del control
    ''' </summary>
    Public Property ControlCurrentYear As Integer Implements IHoliday.ControlCurrentYear
        Get
            Return CurrentYear
        End Get
        Set(value As Integer)
            CurrentYear = value
        End Set
    End Property

    WriteOnly Property ActionOnClontrols() As Boolean
        Set(value As Boolean)
            INDDateNavigator.Enabled = value
            INDScHoliday.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el año actual de el año del control
    ''' </summary>
    Public Property ListHolidays As List(Of Holiday) Implements IHoliday.ListHolidays
        Get
            Return TmpListHolidays
        End Get
        Set(value As List(Of Holiday))
            TmpListHolidays = value
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo para cargar los appointments existentes en un año dado
    ''' </summary>
    Public Async Function LoadAppoinments() As Task
        AsyncLoader(True)
        RemoveHandler INDScHoliday.VisibleIntervalChanged, AddressOf INDScHoliday_VisibleIntervalChanged
        RemoveHandler INDScStorage.AppointmentInserting, AddressOf INDScStorage_AppointmentInserting
        RemoveHandler INDScStorage.AppointmentsInserted, AddressOf INDScStorage_AppointmentsInserted
        RemoveHandler INDScStorage.AppointmentChanging, AddressOf INDScStorage_AppointmentChanging
        RemoveHandler INDScStorage.AppointmentsChanged, AddressOf INDScStorage_AppointmentsChanged
        RemoveHandler INDScStorage.AppointmentDeleting, AddressOf INDScStorage_AppointmentDeleting
        'RemoveHandler INDScStorage.AppointmentsDeleted, AddressOf INDScStorage_AppointmentsDeleted
        Await Presenter.LoadHolidays()
        AddHandler INDScHoliday.VisibleIntervalChanged, AddressOf INDScHoliday_VisibleIntervalChanged

        AddHandler INDScStorage.AppointmentInserting, AddressOf INDScStorage_AppointmentInserting
        AddHandler INDScStorage.AppointmentsInserted, AddressOf INDScStorage_AppointmentsInserted
        AddHandler INDScStorage.AppointmentChanging, AddressOf INDScStorage_AppointmentChanging
        AddHandler INDScStorage.AppointmentsChanged, AddressOf INDScStorage_AppointmentsChanged
        AddHandler INDScStorage.AppointmentDeleting, AddressOf INDScStorage_AppointmentDeleting
        'AddHandler INDScStorage.AppointmentsDeleted, AddressOf INDScStorage_AppointmentsDeleted
        AsyncLoader(False)
    End Function

    ''' <summary>
    ''' Metodo para guardar un nuevo holiday
    ''' </summary>
    Public Function Guardar() As Boolean
        Using Model As New MHoliday
            If Model.SaveHoliday(Holiday) = True Then
                Return True
            Else
                Return False
            End If
        End Using
    End Function
#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ExistDefinitionFront = Nothing
        Holiday = Nothing
        Model = Nothing
        Presenter = Nothing
        PathFunctionalDefinitions = Nothing
        LoadhronousDefinitions = Nothing
        CurrentYear = Nothing
        AppoinmentEditForm = Nothing
    End Sub


    ''' <summary>
    ''' Load del formulario
    ''' </summary>
    Private Async Sub FrmHoliday_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDScHoliday.Start = Me.GetDateServer
        Presenter = New PHoliday(Me)
        Dim presenterBarra As Presentation.Controls.MVP.PBarraBotones = New Controls.MVP.PBarraBotones(Me.BarraBotones)
        Me.BarraBotones.ActualizarPermisosBarra(CStr(Me.Tag))
        If Me.BarraBotones.PermiteConsultar = True Then
            Await LoadAppoinments()
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos)
            ActionOnClontrols = False
        End If

        If TmpListHolidays IsNot Nothing Then
            For Each objHoliday As Holiday In TmpListHolidays
                Dim OBj = INDScHoliday.Storage.CreateAppointment(AppointmentType.Normal, objHoliday.Holiday1, objHoliday.Holiday1, objHoliday.Description)
                HolidaySchedulerStorage.Appointments.Items.Add(OBj)
            Next

        End If
    End Sub

    ''' <summary>
    ''' Evento que controla el popup que se despliega para Diligenciar la informacion del registro
    ''' </summary>
    Private Sub INDScHoliday_EditAppointmentFormShowing(sender As Object, e As DevExpress.XtraScheduler.AppointmentFormEventArgs) Handles INDScHoliday.EditAppointmentFormShowing
        e.Handled = True
        If Me.BarraBotones.PermissionsForm.ContainsKey(Presentation.Base.PermissionsActionsForm.Guardar) Or Me.BarraBotones.PermissionsForm.ContainsKey(Presentation.Base.PermissionsActionsForm.Actualizar) Then
            Dim apt As Appointment = e.Appointment
            ' Crear el formulario customizado
            If apt.Subject = "" Then
                If INDScStorage.Appointments.Items.Find(Function(x) x.Start = apt.Start) Is Nothing Then
                    AppoinmentEditForm = New MyAppointmentEditForm(CType(sender, SchedulerControl), apt, False)
                End If
            Else
                AppoinmentEditForm = New MyAppointmentEditForm(CType(sender, SchedulerControl), apt, False, ObjectState.Modified)
            End If
            If AppoinmentEditForm IsNot Nothing Then
                Try
                    AppoinmentEditForm.LookAndFeel.ParentLookAndFeel = INDScHoliday.LookAndFeel
                    e.DialogResult = AppoinmentEditForm.ShowDialog()
                    INDScHoliday.Refresh()
                Finally
                    AppoinmentEditForm.Dispose()
                    AppoinmentEditForm = Nothing
                End Try
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisosGuardar)
        End If
    End Sub

    ''' <summary>
    ''' Evento que controla el año del date navigator, para refrescar 
    ''' </summary>
    Private Async Sub INDScHoliday_VisibleIntervalChanged(sender As Object, e As EventArgs)
        HolidaySchedulerControl.MonthView.WeekCount = 4

        Dim SelectionStart = HolidayDateNavigator.SelectionStart.Year
        Dim SelectionEnd = HolidayDateNavigator.SelectionEnd.Year

        If ControlCurrentYear <> SelectionStart And ControlCurrentYear <> SelectionEnd Then
            ControlCurrentYear = SelectionStart
            Await LoadAppoinments()
        End If
    End Sub

    ''' <summary>
    ''' Evento para controlar cuando se va a insertar un nuevo appointment
    ''' </summary>
    Private Sub INDScStorage_AppointmentInserting(sender As Object, e As PersistentObjectCancelEventArgs)
        If Me.BarraBotones.PermissionsForm.ContainsKey(Presentation.Base.PermissionsActionsForm.Guardar) = False Then
            e.Cancel = True
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisosGuardar)
            Exit Sub
        End If
        Dim e_apt = CType(e.Object, DevExpress.XtraScheduler.Appointment)
        If e_apt.Subject = String.Empty Then
            e_apt.Subject = "Festivo"
        End If
        'Se recorren los appointment para validar que no hayan mas registros en esa fecha
        For Each apt As Appointment In CType(sender, DevExpress.XtraScheduler.SchedulerStorage).Appointments.Items
            If e_apt.Start = apt.Start Then
                e.Cancel = True
            End If
        Next

    End Sub

    ''' <summary>
    ''' Evento que inserta y guarda los appointments
    ''' </summary>
    Private Sub INDScStorage_AppointmentsInserted(sender As Object, e As PersistentObjectsEventArgs)
        Dim e_apt As Appointment = CType(CType(e.Objects, AppointmentBaseCollection).Item(0), Appointment)
        e_apt.Description = e_apt.Start
        e_apt.End = e_apt.Start
        'Contruyo la entidad del registro a guardar
        Holiday = New Holiday
        With Holiday
            .Holiday1 = e_apt.Start
            .Description = e_apt.Subject
            .State = True
        End With

        If Guardar() = True Then
            Holiday = Nothing
        Else
            HolidaySchedulerStorage.Appointments.Remove(e_apt)
        End If
    End Sub

    ''' <summary>
    ''' Evento ejecutado cuando se esta modificando un appointment
    ''' </summary>
    Private Async Sub INDScStorage_AppointmentChanging(sender As Object, e As PersistentObjectCancelEventArgs)
        If Me.BarraBotones.PermissionsForm.ContainsKey(Presentation.Base.PermissionsActionsForm.Actualizar) = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisosActualizar)
            e.Cancel = True
            Exit Sub
        End If
        Dim Cancel As Boolean = False
        Dim NewPosition As Date
        Dim e_apt = CType(e.Object, DevExpress.XtraScheduler.Appointment)
        If e_apt.Subject = String.Empty Then
            e_apt.Subject = "Festivo"
        End If
        'se valida si el appointment se arrastra a su misma fecha
        If DateTime.Parse(e_apt.Description) = e_apt.Start Then
            Using Model As New MHoliday
                Holiday = Model.GetHoliday(e_apt.Start)
            End Using
                'asignamos los cambios al  Holiday, y se actualiza el registro 
                With Holiday
                    .Description = e_apt.Subject
                End With
                If Guardar() = False Then
                    Await LoadAppoinments()
                Else
                    Holiday = Nothing
                End If
            Else
                'Nueva posicion del apoitment
                NewPosition = e_apt.Start
                'Se Coloca el appointment en su lugar de origen
                e_apt.Start = e_apt.Description
                'Se valida que la fecha nueva del appointment no este ya ocupada
                For Each apt As Appointment In CType(sender, DevExpress.XtraScheduler.SchedulerStorage).Appointments.Items
                    If NewPosition = apt.Start Then
                        e.Cancel = True
                        Cancel = True
                    End If
                Next
                'Lo que se ejecuta cuando el cambio es valido
                If Cancel = False Then
                    Using Model As New MHoliday
                        Holiday = Model.GetHoliday(e_apt.Start)
                    End Using
                    'Asignamos los cambios al appointment
                    e_apt.Start = NewPosition
                    e_apt.Description = NewPosition
                    e_apt.End = NewPosition
                    'asignamos los cambios al  Holiday, y se actualiza el registro 
                    With Holiday
                        .Holiday1 = e_apt.Start
                        .Description = e_apt.Subject
                    End With
                    If Guardar() = False Then
                        Await LoadAppoinments()
                    Else
                        Holiday = Nothing
                    End If
                End If
            End If
    End Sub

    ''' <summary>
    ''' Evento que controla cuando se modifica un appointment
    ''' </summary>
    Private Sub INDScStorage_AppointmentsChanged(sender As Object, e As PersistentObjectsEventArgs)
        Dim apt As Appointment = CType(CType(e.Objects, AppointmentBaseCollection).Item(0), Appointment)
        apt.Description = apt.Start
        apt.End = apt.Start
    End Sub

    ''' <summary>
    ''' Evento que controla cuando se elimina un registro
    ''' </summary>
    Private Sub INDScStorage_AppointmentDeleting(sender As Object, e As PersistentObjectCancelEventArgs)
        If Me.BarraBotones.PermissionsForm.ContainsKey(Presentation.Base.PermissionsActionsForm.Eliminar) = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisosEliminar)
            e.Cancel = True
            Exit Sub
        End If
        Dim apt = CType(e.Object, Appointment)
        If AppoinmentEditForm IsNot Nothing Then
            Using Model As New MHoliday
                If Model.DeleteHoliday(Model.GetHoliday(apt.Start)) = False Then
                    e.Cancel = True
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                Else
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado) & " '" & apt.Subject & "' - " & CStr(apt.Start.Day) & " / " & CStr(apt.Start.Month) & " / " & CStr(apt.Start.Year)
                End If
            End Using
        Else
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MHoliday
                    If Model.DeleteHoliday(Model.GetHoliday(apt.Start)) = False Then
                        e.Cancel = True
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado) & " '" & apt.Subject & "' - " & CStr(apt.Start.Day) & " / " & CStr(apt.Start.Month) & " / " & CStr(apt.Start.Year)
                    End If
                End Using
            Else
                e.Cancel = True
            End If
        End If
        

    End Sub

    ''' <summary>
    ''' Evento para customizar los menus que se despliegan con Click derecho
    ''' </summary>
    Private Sub INDScHoliday_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDScHoliday.PopupMenuShowing
        If e.Menu.Id = SchedulerMenuItemId.DefaultMenu Then
            e.Menu.Items(0).Visible = False
            e.Menu.Items(2).Visible = False
            e.Menu.Items(3).Visible = False
            'e.Menu.Items(6).Visible = False
            e.Menu.Items(1).Caption = "Nuevo Festivo"
        ElseIf e.Menu.Id = SchedulerMenuItemId.AppointmentMenu Then
            e.Menu.Items(1).Visible = False
            e.Menu.Items(2).Visible = False
            e.Menu.Items(3).Visible = False
            e.Menu.Items(4).Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Evento que captura el evento antes de lanzar el dialog ir a fecha
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDScHoliday_GotoDateFormShowing(sender As Object, e As GotoDateFormEventArgs) Handles INDScHoliday.GotoDateFormShowing
        e.Handled = True
        Dim form As New FrmGoToDate(INDScHoliday.Views, INDScHoliday.Start.[Date], e.SchedulerViewType)
        e.DialogResult = form.ShowDialog()
        e.[Date] = form.[Date]
        e.SchedulerViewType = form.TargetView
        'If form.DialogResult = System.Windows.Forms.DialogResult.OK Then
        '    INDScHoliday.Start = form.DateToGo
        'End If
        'form.Dispose()
    End Sub
#End Region

#Region "Icrud"
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch

    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar1() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(value As String)
            
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar,"")
            End If
        End Set
    End Property

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub
#End Region

End Class

