'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 30-07-2013
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
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll.Entities
Imports Presentation.Payroll.MVP
Imports Domain.Base.Entities
Imports DevExpress.XtraEditors
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports System.Drawing
Imports Infrastructure.CrossCutting.Resources
Imports System.Collections.Generic

#End Region

''' <summary>
''' Clase que contiene el formulario de la plantilla de horario a utilizar
''' </summary>
Public Class FrmTemplate

#Region "Fields"

    ''' <summary>
    ''' Listado de los check de los dias de la semana
    ''' </summary>
    ''' <remarks></remarks>
    Dim ChkWeekDays As List(Of Boolean)

    ''' <summary>
    ''' Variable que contiene la accion del formulario (cancelar o aceptar)
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ActionForm As Integer

    ''' <summary>
    ''' objeto que contiene la entidad detalle de turno
    ''' </summary>
    ''' <remarks></remarks>
    Dim _schedulDetail As ScheduleDetail

    ''' <summary>
    ''' variable para almacenar la fecha donde iniciara la plantilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim fechaSchedule As Date

    ''' <summary>
    ''' Objeto que contiene la unidad funcional seleccionada
    ''' </summary>
    ''' <remarks></remarks>
    Dim _FunctionalUnit As FunctionalUnit

    ''' <summary>
    ''' Objeto que contiene la unidad funcional nueva, si se cambia
    ''' </summary>
    Dim _newFunctionalUnit As FunctionalUnit

    ''' <summary>
    ''' Contiene un listado de dias a guardar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _DaysToSave As List(Of Date)

    ''' <summary>
    ''' Objeto para almacenar los registros temporales de schedule
    ''' </summary>
    ''' <remarks></remarks>
    Dim _scheduleDatasource As List(Of Schedule)

    ''' <summary>
    ''' Lista que contiene los empleados chekeados para registrar turno
    ''' </summary>
    ''' <remarks></remarks>
    Dim _EmployeesChecked As New List(Of Integer)


#End Region

#Region "Properties"

    Dim _timeInitialOrdinaryDay As TimeSpan

    Public AllowAllPermission As Boolean = False
    Public FunctionalUnitId As Integer
    Public EditFlag As Boolean = False
    Public EmployeeId As Integer

    Dim EventLastMonth As Boolean = False

    ''' <summary>
    ''' Obtiene o Establece la hora inicio de la jornada ordinaria segun grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TimeInitialOrdinaryDay As TimeSpan
        Get
            Return _timeInitialOrdinaryDay
        End Get
        Set(value As TimeSpan)
            _timeInitialOrdinaryDay = value
        End Set
    End Property

    Dim _timeEndingOrdinaryDay As TimeSpan
    ''' <summary>
    ''' Obtiene o Establece la hora fin de la jornada ordinaria segun grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TimeEndingOrdinaryDay As TimeSpan
        Get
            Return _timeEndingOrdinaryDay
        End Get
        Set(value As TimeSpan)
            _timeEndingOrdinaryDay = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el objeto del detalle de turno
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SchedulDetail As ScheduleDetail
        Get
            Return _schedulDetail
        End Get
        Set(value As ScheduleDetail)

            _schedulDetail = value
            DrawHoursInScheduleControl()
        End Set
    End Property

    ''' <summary>
    ''' Metodo para dibujar las horas en el control de schedule
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DrawHoursInScheduleControl()
        'INDScheduleControl.Storage.Appointments.Clear()
        'Dim appointment As Appointment
        'For Each item As ScheduleDetailHour In _schedulDetail.ScheduleDetailHour
        '    appointment = INDScheduleControl.Storage.CreateAppointment(DevExpress.XtraScheduler.AppointmentType.Normal)
        '    appointment.Start = item.DateTimeInitial
        '    appointment.End = item.DateTimeEnding
        '    appointment.Subject = item.DateTimeInitial.ToString() & " a " & item.DateTimeEnding.ToString()
        '    INDScheduleControl.Storage.Appointments.Add(appointment)
        '    appointment.StatusId = AppointmentStatusType.Busy
        'Next

        Dim appointment As Appointment
        INDScheduleControl.Storage.Appointments.Clear()
        For Each item As ScheduleDetailHour In SchedulDetail.ScheduleDetailHour
            appointment = INDScheduleControl.Storage.CreateAppointment(DevExpress.XtraScheduler.AppointmentType.Normal)
            Dim fechaRango As Date = fechaSchedule
            If item.NextDay = True Then
                fechaRango = fechaRango.AddDays(1)
            End If
            appointment.Start = New Date(fechaRango.Year, fechaRango.Month, fechaRango.Day, item.DateTimeInitial.Hour, item.DateTimeInitial.Minute, item.DateTimeInitial.Second)
            If item.DateTimeEnding.Hour = 0 And item.DateTimeEnding.Minute = 0 Then
                fechaRango = fechaRango.AddDays(1)
            End If
            appointment.End = New Date(fechaRango.Year, fechaRango.Month, fechaRango.Day, item.DateTimeEnding.Hour, item.DateTimeEnding.Minute, item.DateTimeEnding.Second)
            appointment.Subject = item.DateTimeInitial.ToString() & " a " & item.DateTimeEnding.ToString()
            appointment.StatusId = AppointmentStatusType.Busy
            INDScheduleControl.Storage.Appointments.Add(appointment)
        Next
    End Sub

    Private _NextDateLiquidation As Date?
    Public WriteOnly Property NextDateLiquidation() As Date?
        Set(ByVal value As Date?)
            _NextDateLiquidation = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el nombre del funcionario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EmployeeName As String
        Set(value As String)
            INDLcEmployee.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de las plantillas de contratos
    ''' </summary>
    Public WriteOnly Property ScheduleTemplateDatasource As List(Of ScheduleTemplate)
        Set(value As List(Of ScheduleTemplate))
            If AllowAllPermission = True Then
                INDGleScheduleTemplate.Properties.DataSource = value
            Else
                Dim TmpScheduleTemplateList As New List(Of ScheduleTemplate)
                TmpScheduleTemplateList = value.Where(Function(x) x.ScheduleTemplateFunctionalUnit.Any(Function(y) y.FunctionalUnitId = FunctionalUnitId)).ToList()
                INDGleScheduleTemplate.Properties.DataSource = TmpScheduleTemplateList
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de las unidades funcionales
    ''' </summary>
    Public WriteOnly Property FunctionalUnitDatasource As DevExpress.Xpo.XPInstantFeedbackSource
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDGleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el objeto de la unidad funcional
    ''' </summary>
    Public Property FunctionalUnit As FunctionalUnit
        Get
            Return _FunctionalUnit
        End Get
        Set(value As FunctionalUnit)
            _FunctionalUnit = value
            INDGleFunctionalUnit.EditValue = _FunctionalUnit.Code
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el objeto de la unidad funcional nueva, si cambia de unidad funcional
    ''' </summary>
    Public Property NewFunctionalUnit As FunctionalUnit
        Get
            Return _newFunctionalUnit
        End Get
        Set(value As FunctionalUnit)
            _newFunctionalUnit = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el Id De la plantilla de turno
    ''' </summary>
    Public Property ScheduleTemplateId As Integer
        Get
            Return INDGleScheduleTemplate.EditValue
        End Get
        Set(value As Integer)
            RemoveHandler INDGleScheduleTemplate.EditValueChanged, AddressOf INDGleScheduleTemplate_EditValueChanged
            INDGleScheduleTemplate.EditValue = value
            AddHandler INDGleScheduleTemplate.EditValueChanged, AddressOf INDGleScheduleTemplate_EditValueChanged
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el tipo de frecuencia que se va a manejar
    ''' </summary>
    Public Property Frequency As Integer
        Get
            Return INDCbeFrequency.SelectedIndex
        End Get
        Set(value As Integer)
            INDCbeFrequency.SelectedIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene los dias que se repiten en el turno
    ''' </summary>
    Public Property RepeatDays As Integer
        Get
            Return INDTxtRepeatDays.Text
        End Get
        Set(value As Integer)
            INDTxtRepeatDays.Value = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene los dias de la semana que aplica el turno
    ''' </summary>
    Public ReadOnly Property WeekDays_Checked As List(Of Boolean)
        Get
            ChkWeekDays = New List(Of Boolean)
            For i As Integer = 0 To INDChkWeekDays.Controls.Count - 1
                ChkWeekDays.Add(CType(INDChkWeekDays.Controls.Item(i), DevExpress.XtraEditors.CheckEdit).EditValue)
            Next
            Return ChkWeekDays
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene los dias del mes que se repite el turno
    ''' </summary>
    Public ReadOnly Property MonthDays As String
        Get
            Return CtrCalendarMini.Text
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene el dia de inicio del turno
    ''' </summary>
    Public Property StartDate As Date
        Get
            Return INDDeStartDate.EditValue
        End Get
        Set(value As Date)
            INDDeStartDate.EditValue = value
            fechaSchedule = value

            'Limitar los dateedit
            INDDeStartDate.Properties.MinValue = value
            INDDeEndDate.Properties.MinValue = value.AddDays(1)
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el dia en que finaliza el turno
    ''' </summary>
    Public Property EndDate As Date
        Get
            Return INDDeEndDate.EditValue
        End Get
        Set(value As Date)
            INDDeEndDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el limite de finalizacion de fecha para frecuencia de un turno
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SetLimitEndingDate As Date
        Set(value As Date)
            INDDeStartDate.Properties.MaxValue = value
            INDDeEndDate.Properties.MaxValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el limite de inicializacion de fecha para frecuenca de un turno
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SetLimitInitialDate As Date
        Set(value As Date)
            INDDeStartDate.Properties.MinValue = value
            INDDeEndDate.Properties.MinValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece si se incluyen dias festivo y dominicales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Include_Holiday As Boolean
        Get
            Return INDRgHoliday.EditValue
        End Get
        Set(value As Boolean)
            INDRgHoliday.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la accion que se realizara con el pop up
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ActionForm As Integer
        Get
            Return _ActionForm
        End Get
        Set(value As Integer)
            _ActionForm = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el estado del los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionOnControls As Boolean
        Set(value As Boolean)
            If value = False Then
                INDLyGrFrequency.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyGrRepeatInterval.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyGrLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyItemScheduleMatch.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene los dias en total que se van a guardar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DaysToSave As List(Of Date)
        Get
            Return _DaysToSave
        End Get
        Set(value As List(Of Date))
            _DaysToSave = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los empleados
    ''' </summary>
    Public WriteOnly Property EmployeeDatasource As List(Of Schedule)
        Set(value As List(Of Schedule))
            Dim NewList = (From a In value
                           Order By a.Employee.ThirdParty.Name Ascending
                           Select New With {.id = a.Employee.Id, .name = a.Employee.ThirdParty.Name}).ToList

            For Each item In NewList
                INDCLBCEmployee.Items.Add(item.id, item.name)
            Next
        End Set
    End Property

    ''' <summary>
    ''' Devuelve los empleados Checkeados
    ''' </summary>
    Public ReadOnly Property EmployeesChecked As List(Of Integer)
        Get
            Return _EmployeesChecked
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que establece el empleado checkeado desde el form schedule
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EmployeeCheked As List(Of Integer)
        Set(value As List(Of Integer))
            RemoveHandler INDCLBCEmployee.ItemCheck, AddressOf INDCLBCEmployee_ItemCheck
            For Each _int As Integer In value
                _EmployeesChecked.Add(_int)
                Dim _value As Object = _int
                INDCLBCEmployee.Items.Item(_value).CheckState = System.Windows.Forms.CheckState.Checked
                INDCLBCEmployee.Items.Item(_value).Enabled = False
            Next

            AddHandler INDCLBCEmployee.ItemCheck, AddressOf INDCLBCEmployee_ItemCheck

            If value.Count > 1 Then
                INDLyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para marcar como evento 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EditScheduleDetail As Boolean
        Set(value As Boolean)
            If value = True Then
                If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.RegistrarEvento) Then
                    INDLyItemAddHours.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
                INDLyItemDetailHours.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDGcDetailHours.DataSource = SchedulDetail.ScheduleDetailHour
                ListScheduleDetailHourEdit = SchedulDetail.ScheduleDetailHour.ToList()
            Else
                INDLyItemDetailHours.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDGcDetailHours.DataSource = Nothing
                ListScheduleDetailHourEdit = Nothing
                INDLyItemAddHours.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Assing_Values()
                DrawHoursInScheduleControl()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la hora final de el nuevo registro de hora
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PopupEndingTime As TimeSpan
        Get
            Dim _thisDate As Date = INDPopupTeEndingTime.EditValue
            Return New TimeSpan(_thisDate.Hour, _thisDate.Minute, _thisDate.Second)
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la hora final de el nuevo registro de hora
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PopupInitialTime As TimeSpan
        Get
            Dim _thisDate As Date = INDPopupTeInitialTime.EditValue
            Return New TimeSpan(_thisDate.Hour, _thisDate.Minute, _thisDate.Second)
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String ' Implements IcrudBase.Mensaje
        Set(ByVal value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, Botones.Aceptar, Base.Icono.Errores)
            End If
        End Set
    End Property

    ''' <summary>
    ''' COntiene el estado si oculta el grupo de eventos
    ''' </summary>
    ''' <remarks></remarks>
    Private _OcultGrEvents As Boolean
    ''' <summary>
    ''' Oculta el grupo de editar eventos cuando se va a editar un turno
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property OcultGrEvents As Boolean
        Set(value As Boolean)
            _OcultGrEvents = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el control del calendario mini
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CtrCalendarMini1 As CtrCalendarMini
        Get
            Return CtrCalendarMini
        End Get
    End Property

    Dim ListDays As List(Of ScheduleDetail) = New List(Of ScheduleDetail)

    Dim _listScheduleWithoutSave As List(Of DaysNotsave) = New List(Of DaysNotsave)

    Dim TmpListDays As New List(Of ScheduleDetail)

    Public ObjContract As Contract

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ChkWeekDays = Nothing
        _ActionForm = Nothing
        _schedulDetail = Nothing
        fechaSchedule = Nothing
        _FunctionalUnit = Nothing
        _newFunctionalUnit = Nothing
        _DaysToSave = Nothing
        _scheduleDatasource = Nothing
        _EmployeesChecked = Nothing
    End Sub
    ''' <summary>
    ''' Load del formulario
    ''' </summary>
    Private Async Sub FrmTemplate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If EditFlag = False Then
            Using model As New MSchedule()
                Dim NewFunctionalUnit = Await model.GetScheduleDetailByEmployeeBetweenDateAsync(EmployeeId, New Date(CtrCalendarMini1.YearControl, CtrCalendarMini1.MonthControl, 1), New Date(CtrCalendarMini1.YearControl, CtrCalendarMini1.MonthControl, Date.DaysInMonth(CtrCalendarMini1.YearControl, CtrCalendarMini1.MonthControl)))
                If NewFunctionalUnit IsNot Nothing Then
                    For Each objScheduleDetail As ScheduleDetail In NewFunctionalUnit
                        ListDays.Add(objScheduleDetail)
                    Next
                End If
            End Using
        End If

        INDScheduleControl.Start = fechaSchedule
        Clean_FrequencyType()
        INDGleScheduleTemplate.Properties.PopupFormSize = New System.Drawing.Size(INDGleScheduleTemplate.Width, 200)
        For i As Integer = 0 To INDChkWeekDays.Controls.Count - 1
            CType(INDChkWeekDays.Controls.Item(i), DevExpress.XtraEditors.CheckEdit).Checked = False
            CType(INDChkWeekDays.Controls.Item(i), DevExpress.XtraEditors.CheckEdit).Font = New Font("Segoe UI Light", 12.0!)
            CType(INDChkWeekDays.Controls.Item(i), DevExpress.XtraEditors.CheckEdit).Size = New Size(100, CType(INDChkWeekDays.Controls.Item(i), DevExpress.XtraEditors.CheckEdit).Size.Height)
        Next

        ValidatePastMonth()

    End Sub

    Private Sub ValidatePastMonth()

        Dim InitialCurrentDate = New Date(StartDate.Year, StartDate.Month, 1)
        Dim EndCurrentDate = New Date(InitialCurrentDate.Year, InitialCurrentDate.Month, Date.DaysInMonth(InitialCurrentDate.Year, InitialCurrentDate.Month))

        If ObjContract.Group.Liquidation = 2 Then
            If ObjContract.LastLiquidationDate IsNot Nothing Then
                If ObjContract.LastLiquidationDate.Value.Day = 15 Then
                    InitialCurrentDate = New Date(StartDate.Year, StartDate.Month, 16)
                End If
            Else
                InitialCurrentDate = ObjContract.Group.NextDateLiquidation
                EndCurrentDate = New Date(InitialCurrentDate.Year, InitialCurrentDate.Month, Date.DaysInMonth(InitialCurrentDate.Year, InitialCurrentDate.Month))
            End If
        End If

        If ObjContract.LastLiquidationDate IsNot Nothing Then
            If ObjContract.LastLiquidationDate.Value >= InitialCurrentDate AndAlso ObjContract.LastLiquidationDate.Value <= EndCurrentDate Then
                EventLastMonth = True
                INDRgEvent.SelectedIndex = 1
                INDCbeFrequency.Enabled = False
                INDRgEvent.Enabled = False
                INDLyItemDetailHours.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                ShowHideEditSchedule()
            Else
                ShowHideEditSchedule()
            End If
        Else
            If ObjContract.Group.LastDateLiquidation >= InitialCurrentDate AndAlso ObjContract.Group.LastDateLiquidation <= EndCurrentDate Then
                EventLastMonth = True
                INDRgEvent.SelectedIndex = 1
                INDCbeFrequency.Enabled = False
                INDRgEvent.Enabled = False
                INDLyItemDetailHours.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                ShowHideEditSchedule()
            End If
        End If

    End Sub

    ''' <summary>
    ''' Evento que controla la opcion elegida en el tipo de frecuencia del turno
    ''' </summary>
    Private Sub INDCbeFrequency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles INDCbeFrequency.SelectedIndexChanged
        If INDCbeFrequency.SelectedIndex > -1 Then
            ActionOn_FrequencyType(INDCbeFrequency.SelectedIndex)
        End If
    End Sub

    ''' <summary>
    ''' Evento que controla el cambio del grid look up de las plantillas de turno
    ''' </summary>
    Private Sub INDGleScheduleTemplate_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleScheduleTemplate.EditValueChanged
        If INDGleScheduleTemplate.EditValue IsNot Nothing AndAlso INDGleScheduleTemplate.Text <> "" AndAlso CInt(INDGleScheduleTemplate.EditValue) > 0 Then
            ValidatePastMonth()
            Assing_Values()
            DrawHoursInScheduleControl()
            ShowHideEditSchedule()
        End If
    End Sub

    ''' <summary>
    ''' Evento que controla los submenus que se despliegan con click derecho, para no mostrarlos
    ''' </summary>
    Private Sub INDScheduleControl_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs)
        If e.Menu.Id = DevExpress.XtraScheduler.SchedulerMenuItemId.DefaultMenu Then
            'Obtengo el item "Cambio de vista"
            Dim itemCambiarVista As SchedulerPopupMenu = e.Menu.GetPopupMenuById(SchedulerMenuItemId.SwitchViewMenu)
            itemCambiarVista.Visible = False
            'Elimino el item de crear evento todo el dia
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAllDayEvent)

            e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringAppointment)

            e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewRecurringEvent)

            e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoToday)

            e.Menu.RemoveMenuItem(SchedulerMenuItemId.GotoDate)

            e.Menu.RemoveMenuItem(SchedulerMenuItemId.NewAppointment)
        ElseIf e.Menu.Id = DevExpress.XtraScheduler.SchedulerMenuItemId.AppointmentMenu Then
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.EditSeries)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.StatusSubMenu)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.LabelSubMenu)
            e.Menu.RemoveMenuItem(SchedulerMenuItemId.OpenAppointment)
        End If
    End Sub

    ''' <summary>
    ''' Evento que carga la unidad funcional cada que se cambia el valor en el control
    ''' </summary>
    Private Async Sub INDGleFunctionalUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleFunctionalUnit.EditValueChanged
        If INDGleFunctionalUnit.EditValue IsNot Nothing Then
            If INDGleFunctionalUnit.EditValue <> FunctionalUnit.Code Then
                Using model As New MFunctionalUnit(MFunctionalUnit.TAG)
                    NewFunctionalUnit = Await model.GetFuncUnitAsync(INDGleFunctionalUnit.EditValue)
                End Using
            Else
                NewFunctionalUnit = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que controla cuando se selecciona o se quita un empleado a registrar turno
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDCLBCEmployee_ItemCheck(sender As Object, e As DevExpress.XtraEditors.Controls.ItemCheckEventArgs)
        If e IsNot Nothing Then
            If e.State = System.Windows.Forms.CheckState.Checked Then
                _EmployeesChecked.Add(INDCLBCEmployee.Items.Item(e.Index).Value)
            ElseIf e.State = System.Windows.Forms.CheckState.Unchecked Then
                _EmployeesChecked.Remove(INDCLBCEmployee.Items.Item(e.Index).Value)
            End If
        End If

    End Sub

    ''' <summary>
    ''' Evento para Saber si se marca como evento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDRgEvent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles INDRgEvent.SelectedIndexChanged
        If INDRgEvent.SelectedIndex = 1 And INDGleScheduleTemplate.EditValue IsNot Nothing And INDGleFunctionalUnit.EditValue IsNot Nothing Then ' marcado como evento y lleno rejilla
            EditScheduleDetail = True
        Else ' marcado como no evento, y limpio rejilla
            If EventLastMonth = True Then
                EditScheduleDetail = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Controla la visibilidad de los compañeros de rotación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDLcScheduleMates_Click(sender As Object, e As EventArgs) Handles INDLcScheduleMates.Click
        If INDLyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
            INDLyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            For i As Integer = 0 To INDCLBCEmployee.Items.Count - 1
                If INDCLBCEmployee.Items.Item(i).Enabled = True Then
                    INDCLBCEmployee.Items.Item(i).CheckState = System.Windows.Forms.CheckState.Unchecked
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Evento para añadir nuevas horas al turno
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbAddHours_Click(sender As Object, e As EventArgs) Handles INDSbAddHours.Click
        If INDPopupTeInitialTime.EditValue Is Nothing Then
            INDPopupTeInitialTime.Focus()
            Exit Sub
        End If
        If INDPopupTeEndingTime.EditValue Is Nothing Then
            INDPopupTeEndingTime.Focus()
            Exit Sub
        End If
        Dim initialTime = PopupInitialTime
        Dim EndTime = PopupEndingTime
        If INDPopupRgInitialNextDay.EditValue = INDPopupRgNextDay.EditValue Then
            AddHourSchedule(initialTime, EndTime, INDPopupRgInitialNextDay.EditValue)
        Else
            AddHourSchedule(initialTime, New TimeSpan(0, 0, 0), INDPopupRgInitialNextDay.EditValue)
            AddHourSchedule(New TimeSpan(0, 0, 0), EndTime, INDPopupRgNextDay.EditValue)
            Clean_Values_Popup()
        End If


    End Sub

    Private Sub AddHourSchedule(initialTime As TimeSpan, EndTime As TimeSpan, nextDay As Boolean)
        Dim hourInitial = initialTime
        Dim hourEnding = EndTime

        If EndTime = TimeSpan.Zero Then
            hourEnding = TimeSpan.FromHours(24)
        End If

        If (hourEnding - hourInitial) < TimeSpan.FromHours(1) Then
            If (hourEnding - hourInitial) <> TimeSpan.FromMinutes(30) Then
                Mensaje(EeventViewerImages.Informacion) = "Esta intentando ingresar un evento de " + (hourEnding - hourInitial).TotalMinutes.ToString() + " Minutos" ' menor a 1 hora  y no es 30 minutos."
                Exit Sub
            End If
        End If

        Dim list_det_hour As List(Of ScheduleDetailHour) = ValidateAddHour(EndTime, initialTime, nextDay)
        If list_det_hour.Count > 0 Then
            Dim _messege As String = obtenerRecurso(SobreescribirHorario, CuadroDeTurno)
            If MessageIndigo.Show(_messege, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                For Each item In list_det_hour
                    Dim _schHourToDelete As ScheduleDetailHour = SchedulDetail.ScheduleDetailHour.Where(Function(x) x.DateTimeInitial = item.DateTimeInitial And x.DateTimeEnding = item.DateTimeEnding).FirstOrDefault
                    _schHourToDelete.MarkAsDeleted()
                Next
            Else
                Exit Sub
            End If
        End If

        Dim newScheduleDetailHour As ScheduleDetailHour = New ScheduleDetailHour()
        With newScheduleDetailHour
            .NextDay = nextDay
            If .NextDay = False Then
                .DateTimeInitial = New Date(fechaSchedule.Year, fechaSchedule.Month, fechaSchedule.Day, initialTime.Hours, initialTime.Minutes, initialTime.Seconds)
                .DateTimeEnding = New Date(fechaSchedule.Year, fechaSchedule.Month, fechaSchedule.Day, EndTime.Hours, EndTime.Minutes, EndTime.Seconds)
                If .DateTimeEnding.Hour = 0 Then
                    Dim NumberDays As Integer = 0
                    NumberDays = DateTime.DaysInMonth(fechaSchedule.Year, fechaSchedule.Month)

                    If NumberDays <= fechaSchedule.Day + 1 Then
                        .DateTimeEnding = New Date(fechaSchedule.Year, fechaSchedule.Month + 1, 1, EndTime.Hours, EndTime.Minutes, EndTime.Seconds)
                    Else
                        .DateTimeEnding = New Date(fechaSchedule.Year, fechaSchedule.Month, fechaSchedule.Day + 1, EndTime.Hours, EndTime.Minutes, EndTime.Seconds)
                    End If


                End If
            Else
                .DateTimeInitial = New Date(fechaSchedule.Year, fechaSchedule.Month, fechaSchedule.Day, initialTime.Hours, initialTime.Minutes, initialTime.Seconds).AddDays(1)
                .DateTimeEnding = New Date(fechaSchedule.Year, fechaSchedule.Month, fechaSchedule.Day, EndTime.Hours, EndTime.Minutes, EndTime.Seconds).AddDays(1)
                If .DateTimeEnding.Hour = 0 Then
                    .DateTimeEnding = New Date(fechaSchedule.Year, fechaSchedule.Month, fechaSchedule.Day + 1, EndTime.Hours, EndTime.Minutes, EndTime.Seconds)
                End If
            End If
            .Event = True
            .Approved = False
        End With
        SchedulDetail.ScheduleDetailHour.Add(newScheduleDetailHour)
        INDGcDetailHours.RefreshDataSource()
        INDDDBAddHours.HideDropDown()
        DrawHoursInScheduleControl()
    End Sub

    ''' <summary>
    ''' Funcion que valida si la hora que se esta registrando no se cruza con otro horario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateAddHour(_PopupEndingTime As TimeSpan, _PopupInitialTime As TimeSpan, _nextDay As Boolean) As List(Of ScheduleDetailHour)
        Dim list_DetailHours As List(Of ScheduleDetailHour) = New List(Of ScheduleDetailHour)()
        For Each item In SchedulDetail.ScheduleDetailHour
            Dim Di As TimeSpan = New TimeSpan(item.DateTimeInitial.Hour, item.DateTimeInitial.Minute, item.DateTimeInitial.Second)
            Dim De As TimeSpan = New TimeSpan(item.DateTimeEnding.Hour, item.DateTimeEnding.Minute, item.DateTimeEnding.Second)
            Dim hourInitial = _PopupInitialTime
            Dim hourEnding = _PopupEndingTime
            If _PopupEndingTime = TimeSpan.Zero Then
                hourEnding = TimeSpan.FromHours(24)
            End If
            If De = TimeSpan.Zero Then
                De = New TimeSpan(23, 59, 59)
            End If
            For i As Integer = 1 To CType((hourEnding - hourInitial).TotalHours, Integer)
                Dim _hour As TimeSpan = _PopupInitialTime.Add(New TimeSpan(i, 0, 0))
                If _hour > Di And _hour < De And item.NextDay = _nextDay Then
                    list_DetailHours.Add(item)
                    Exit For
                End If
            Next
        Next
        Return list_DetailHours
    End Function

    ''' <summary>
    ''' Para refrescar las horas pintadas en el schedule control, cuando actualizan un registro de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGvDetailHours_RowUpdated(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles INDGvDetailHours.RowUpdated
        DrawHoursInScheduleControl()
    End Sub

    ''' <summary>
    ''' Coloca la hora fin igual a la hora incio cada que escriban en el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPopupTeInitialTime_EditValueChanged(sender As Object, e As EventArgs) Handles INDPopupTeInitialTime.EditValueChanged
        'INDPopupTeEndingTime.EditValue = INDPopupTeInitialTime.EditValue
    End Sub

    ''' <summary>
    ''' Controla que la hora fin sea mayor a la hora inicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPopupTeEndingTime_EditValueChanged(sender As Object, e As EventArgs) Handles INDPopupTeEndingTime.EditValueChanged
        'If INDPopupTeEndingTime.EditValue < INDPopupTeInitialTime.EditValue Then
        '    INDPopupTeEndingTime.EditValue = INDPopupTeInitialTime.EditValue
        'End If
    End Sub

    ''' <summary>
    ''' Evento para cargar en el pop up los datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepPopupContainerEdit_Popup(sender As Object, e As EventArgs) Handles RepPopupContainerEdit.Popup
        Dim SchDetHour As ScheduleDetailHour = INDGvDetailHours.GetFocusedRow
        INDGPopupTeEndingTime.EditValue = SchDetHour.DateTimeEnding
        INDGPopupTeInitialTime.EditValue = SchDetHour.DateTimeInitial
        INDGPopupRgNextDay.EditValue = SchDetHour.NextDay
        INDGPopupRgEvent.EditValue = SchDetHour.Event
    End Sub

    ''' <summary>
    ''' Evento para evaluar si ya se confirmo un evento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepPopupContainerEdit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepPopupContainerEdit.QueryPopUp
        Dim SchDetHour As ScheduleDetailHour = INDGvDetailHours.GetFocusedRow
        If SchDetHour.Approved = True Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(EventoAprobado, CuadroDeTurno)
            Clean_Values_Popup()
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Evento para Actualizar Los registros del pop up de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGSbAddHours_Click(sender As Object, e As EventArgs) Handles INDGSbAddHours.Click
        Dim SchDetHour As ScheduleDetailHour = INDGvDetailHours.GetFocusedRow

        Dim hi As TimeSpan = New TimeSpan(CType(INDGPopupTeInitialTime.EditValue, Date).Hour, CType(INDGPopupTeInitialTime.EditValue, Date).Minute, CType(INDGPopupTeInitialTime.EditValue, Date).Second)
        Dim he As TimeSpan = New TimeSpan(CType(INDGPopupTeEndingTime.EditValue, Date).Hour, CType(INDGPopupTeEndingTime.EditValue, Date).Minute, CType(INDGPopupTeEndingTime.EditValue, Date).Second)
        Dim hi_n = hi.Hours
        Dim he_n = he.Hours
        If INDGPopupTeInitialTime.EditValue Is Nothing Then
            INDGPopupTeInitialTime.Focus()
            Exit Sub
        End If
        If INDGPopupTeEndingTime.EditValue Is Nothing Then
            INDGPopupTeEndingTime.Focus()
            Exit Sub
        End If
        If he = New TimeSpan(0, 0, 0) Then
            he_n = 24
        End If
        If he_n - hi_n < 1 Then
            Exit Sub
        End If

        Dim list_det_hour As List(Of ScheduleDetailHour) = ValidateAddHour(he, hi, INDGPopupRgNextDay.EditValue)
        If list_det_hour.Count > 0 Then
            Dim SchDetHourSame As ScheduleDetailHour = list_det_hour.Find(Function(x) x.Id = SchDetHour.Id)
            If SchDetHourSame IsNot Nothing Then
                list_det_hour.Remove(SchDetHourSame)
            End If
        End If

        If list_det_hour.Count > 0 Then
            Dim _messege As String = obtenerRecurso(SobreescribirHorario, CuadroDeTurno)
            If MessageIndigo.Show(_messege, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                For Each item In list_det_hour
                    Dim _schHourToDelete As ScheduleDetailHour = SchedulDetail.ScheduleDetailHour.Where(Function(x) x.Id = item.Id).FirstOrDefault
                    _schHourToDelete.MarkAsDeleted()
                Next
            Else
                Exit Sub
            End If
        End If


        '********Valido que el nuevo horario no se salga del que pertenece, normal o nocturno
        If INDGPopupRgEvent.EditValue = False Then
            If GetTypeDayHour(SchDetHour.DateTimeEnding.TimeOfDay) <> GetTypeDayHour(CDate(INDGPopupTeEndingTime.EditValue).TimeOfDay) Then
                If GetTypeDayHour(SchDetHour.DateTimeEnding.TimeOfDay) = EDayType.Night Then 'no se puede salir del horario nocturno
                    Mensaje(EeventViewerImages.Advertencia) = "La hora fin no puede salir del horario nocturno"
                Else 'no se puede salir del horario normal
                    Mensaje(EeventViewerImages.Advertencia) = "La hora fin no puede salir del horario normal"
                End If
                Exit Sub
            End If
            If GetTypeDayHour(SchDetHour.DateTimeInitial.TimeOfDay) <> GetTypeDayHour(CDate(INDGPopupTeInitialTime.EditValue).TimeOfDay) Then
                If GetTypeDayHour(SchDetHour.DateTimeInitial.TimeOfDay) = EDayType.Night Then 'no se puede salir del horario nocturno
                    Mensaje(EeventViewerImages.Advertencia) = "La hora inicio no puede salir del horario nocturno"
                Else 'no se puede salir del horario normal
                    Mensaje(EeventViewerImages.Advertencia) = "La hora inicio no puede salir del horario normal"
                End If
                Exit Sub
            End If

        End If

        'validar si cambian evento a normal y ya estaba aprobado
        SchDetHour.DateTimeEnding = INDGPopupTeEndingTime.EditValue
        SchDetHour.DateTimeInitial = INDGPopupTeInitialTime.EditValue
        SchDetHour.NextDay = INDGPopupRgNextDay.EditValue
        SchDetHour.Event = INDGPopupRgEvent.EditValue
        SchDetHour.TotalNumberHours = he_n - hi_n
        INDRgEvent.Focus()
        INDGcDetailHours.RefreshDataSource()
        DrawHoursInScheduleControl()
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Devuelve si la hora es de un dia normal o nocturno
    ''' </summary>
    ''' <param name="hour"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTypeDayHour(hour As TimeSpan) As EDayType
        If hour >= TimeInitialOrdinaryDay AndAlso hour <= TimeEndingOrdinaryDay Then
            Return EDayType.Normal
        Else
            Return EDayType.Night
        End If
    End Function

    ''' <summary>
    ''' Metodo para controlar si se muestra o no el editar turno
    ''' </summary>
    ''' <remarks></remarks>
    Sub ShowHideEditSchedule()
        If INDGleScheduleTemplate.EditValue IsNot Nothing AndAlso INDGleScheduleTemplate.Text <> "" AndAlso CInt(INDGleScheduleTemplate.EditValue) > 0 Then
            If _EmployeesChecked.Count > 1 Then
                INDLyGrEvent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.EditarHorario) AndAlso _OcultGrEvents = False Then
                    INDLyGrEvent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.RegistrarEvento) Then
                        INDGLyItemEvent1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para habilitar la auditoria
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetDocumentsSchedule()
        If SchedulDetail IsNot Nothing AndAlso SchedulDetail.Id > 0 Then
            Me.BarraBotones.SetDocuments(SchedulDetail.Id)

            Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
        Else

            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        End If
    End Sub

    ''' <summary>
    ''' Funciona para validar que los campos esten correctamente diligenciados y registrar el turno
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateControls() As Boolean
        ValidateControls = True
        If INDGleScheduleTemplate.EditValue Is Nothing Then
            INDGleScheduleTemplate.Focus()
            ValidateControls = False
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Function
        End If

        If INDGleFunctionalUnit.EditValue Is Nothing Then
            INDGleFunctionalUnit.Focus()
            ValidateControls = False
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Function
        End If

        If Not (EmployeesChecked.Count > 0) And INDLyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            INDCLBCEmployee.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDDeStartDate.EditValue Is Nothing Then
            INDDeStartDate.Focus()
            ValidateControls = False
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Function
        End If

        If INDDeEndDate.EditValue Is Nothing Then
            INDDeEndDate.Focus()
            ValidateControls = False
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Function
        End If

        If INDCbeFrequency.SelectedIndex > 0 AndAlso INDCbeFrequency.SelectedIndex <> 3 Then

            If CType(INDDeStartDate.EditValue, Date).Date >= CType(INDDeEndDate.EditValue, Date).Date Then
                ValidateControls = False
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FechaInicioMenor, CuadroDeTurno)
                Exit Function
            End If

            If (CType(INDDeEndDate.EditValue, Date).Date - CType(INDDeStartDate.EditValue, Date).Date).Days > 365 Then
                ValidateControls = False
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(RangoFrecuenciaAlto, CuadroDeTurno)
                Exit Function
            End If
        End If

        If INDCbeFrequency.SelectedIndex = 3 AndAlso CtrCalendarMini1.ListDaysToDelete.Count <= 0 Then
            ValidateControls = False
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayDiasSeleccionados, CuadroDeTurno)
            Exit Function
        End If

    End Function

    Public Async Function ValidateTurns() As Task

        If EditFlag = False And ListDays.Count > 0 Then

            TmpListDays = New List(Of ScheduleDetail)
            _listScheduleWithoutSave = New List(Of DaysNotsave)

            If INDCbeFrequency.SelectedIndex = 0 Then
                Exit Function
            End If

            Dim TmpFunctionalUnit = FunctionalUnitId

            If _newFunctionalUnit IsNot Nothing Then
                TmpFunctionalUnit = _newFunctionalUnit.Id
            End If

            If CtrCalendarMini1.ListDaysToDelete.Count > 0 Then

                For Each tmpInteger As Integer In CtrCalendarMini1.ListDaysToDelete

                    If ListDays.Any(Function(x) x.DateDetail.Day = tmpInteger And x.ScheduleFunctionalUnitId = TmpFunctionalUnit) Then
                        TmpListDays.Add(ListDays.Where(Function(x) x.DateDetail.Day = tmpInteger).FirstOrDefault())
                    End If

                Next
            Else
                For Each tmpInteger As ScheduleDetail In ListDays
                    If TmpListDays.Any(Function(x) x.DateDetail = tmpInteger.DateDetail And x.ScheduleFunctionalUnitId = TmpFunctionalUnit) Then
                        TmpListDays.Add(tmpInteger)
                    End If
                Next
            End If

            For Each ObjDaysNotSave As ScheduleDetail In TmpListDays

                CtrCalendarMini1.ListDaysToDelete.Remove(ObjDaysNotSave.DateDetail.Day)

                Dim DaysNotSave As New DaysNotsave

                DaysNotSave.EmployeeError = INDLcEmployee.Text
                DaysNotSave.MessegeError = obtenerRecurso(ErrorScheduleDetailExisting, CuadroDeTurno)
                DaysNotSave.DayError = ObjDaysNotSave.DateDetail.ToShortDateString

                Using mTemp As New MScheduleControl
                    If ObjDaysNotSave.ScheduleTemplateId IsNot Nothing Then
                        Dim temp As ScheduleTemplate = Await mTemp.GetScheduleTemplateByIdAsync(ObjDaysNotSave.ScheduleTemplateId)
                        If temp IsNot Nothing Then
                            DaysNotSave.TemplateError = temp.Name
                        End If
                    End If
                End Using
                _listScheduleWithoutSave.Add(DaysNotSave)
            Next

            If _listScheduleWithoutSave IsNot Nothing AndAlso _listScheduleWithoutSave.Count > 0 Then

                AsyncLoader(True)
                Using FormInfoDialog As New FrmInfoDialog
                    With FormInfoDialog
                        .InfoDialogDatasource = _listScheduleWithoutSave
                        .ShowDialog()
                        .Dispose()
                    End With
                End Using

                AsyncLoader(False)

            End If

        End If
    End Function

    ''' <summary>
    ''' Metodo para asignar valores a la entidad Schedule
    ''' </summary>
    Public Sub Assing_Values()
        Dim totalHoras As Decimal = 0
        Dim indiceHour As Integer
        Dim indiceConcept As Integer

        If ScheduleTemplateId = 0 Then
            ScheduleTemplateId = INDGleScheduleTemplate.EditValue
        End If

        With SchedulDetail
            .ScheduleTemplateId = ScheduleTemplateId
            While .ScheduleDetailHour.Count > 0
                indiceHour = .ScheduleDetailHour.Count - 1
                While .ScheduleDetailHour.Item(indiceHour).ScheduleDetailConcept.Count > 0
                    indiceConcept = .ScheduleDetailHour.Item(indiceHour).ScheduleDetailConcept.Count - 1
                    .ScheduleDetailHour.Item(indiceHour).ScheduleDetailConcept.Item(indiceConcept).MarkAsDeleted()
                End While
                .ScheduleDetailHour.Item(indiceHour).MarkAsDeleted()
            End While
            Dim template As ScheduleTemplate = CType(INDGleScheduleTemplate.Properties.DataSource, List(Of ScheduleTemplate)).Find(Function(x) x.Id = ScheduleTemplateId)
            .Letter = template.Letter

            For Each item As ScheduleTemplateConcept In template.ScheduleTemplateConcept

                totalHoras += item.NumberHour
                Dim rango As ScheduleDetailHour = New ScheduleDetailHour()
                rango.TotalNumberHours = item.NumberHour
                Dim fechaRango As Date = fechaSchedule
                If item.NextDay = True Then
                    fechaRango = fechaRango.AddDays(1)
                End If
                rango.DateTimeInitial = New Date(fechaRango.Year, fechaRango.Month, fechaRango.Day, item.InitialTime.Hours, item.InitialTime.Minutes, item.InitialTime.Seconds)
                If item.EndingTime.Hours = 0 And item.EndingTime.Minutes = 0 Then
                    fechaRango = fechaRango.AddDays(1)
                End If
                rango.DateTimeEnding = New Date(fechaRango.Year, fechaRango.Month, fechaRango.Day, item.EndingTime.Hours, item.EndingTime.Minutes, item.EndingTime.Seconds)
                rango.NextDay = item.NextDay
                rango.AppliedLiquidationConcept = 0
                rango.Event = INDGPopupRgEvent.EditValue

                If EventLastMonth And rango.Event = True Then
                    rango.EventLastMonth = True
                End If
                For Each itemConcepto As ScheduleTemplateConceptDetail In item.ScheduleTemplateConceptDetail
                    Dim conceptoDetail As ScheduleDetailConcept = New ScheduleDetailConcept()
                    conceptoDetail.ConceptId = itemConcepto.ConceptId
                    conceptoDetail.ConceptType = itemConcepto.ConceptType
                    rango.ScheduleDetailConcept.Add(conceptoDetail)
                Next

                .ScheduleDetailHour.Add(rango)
            Next

            .TotalNumberHours = totalHoras
        End With
    End Sub

    ''' <summary>
    ''' Metodo que controla el proceso a realizar con los scheduleDetail
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Process_ScheduleDetail()
        DaysToSave = New List(Of Date)
        Select Case Frequency
            'sin programacion
            Case Is = 0
                DaysToSave.Add(fechaSchedule)
                Include_Holiday = True 'Envio "Include_Holiday" verdadero para que registre el dia seleccionado asi sea un festivo

                'Por cada número de días
            Case Is = 1
                If RepeatDays > 0 Then
                    Dim MinDate As Date = StartDate
                    Dim MaxDate As Date = EndDate
                    While (MinDate <= MaxDate)
                        DaysToSave.Add(MinDate)
                        MinDate = MinDate.AddDays(RepeatDays)
                    End While
                End If


                'Por días de la semana
            Case Is = 2
                Dim MinDate As Date = StartDate
                Dim MaxDate As Date = EndDate
                Dim _index As Integer = 0
                While (MinDate <= MaxDate)
                    If WeekDays_Checked.Item(MinDate.DayOfWeek) = True Then
                        DaysToSave.Add(MinDate)
                    End If
                    MinDate = MinDate.AddDays(1)
                    _index += 1
                End While

                'Días del mes
            Case Is = 3
                If CtrCalendarMini1.ListDaysToDelete.Count > 0 Then
                    For i As Integer = 0 To CtrCalendarMini1.ListDaysToDelete.Count - 1
                        DaysToSave.Add(New Date(CtrCalendarMini1.YearControl, CtrCalendarMini1.MonthControl, CtrCalendarMini1.ListDaysToDelete.Item(i)))
                    Next
                End If
        End Select

        If DaysToSave IsNot Nothing AndAlso DaysToSave.Count > 0 Then
            If TmpListDays IsNot Nothing AndAlso TmpListDays.Count > 0 Then
                For Each objDetail As ScheduleDetail In TmpListDays
                    DaysToSave.Remove(objDetail.DateDetail)
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para el tamaño de los controles
    ''' </summary>
    Public Sub GleSize()
        INDGleFunctionalUnit.Properties.PopupFormSize = New System.Drawing.Size(800, 300)
        INDGleScheduleTemplate.Properties.PopupFormSize = New System.Drawing.Size(500, 200)
        For i As Integer = 0 To INDChkWeekDays.Controls.Count - 1
            Dim _checkEdit As DevExpress.XtraEditors.CheckEdit = CType(INDChkWeekDays.Controls.Item(i), DevExpress.XtraEditors.CheckEdit)
            _checkEdit.Size = New System.Drawing.Size(100, _checkEdit.Size.Height)
            _checkEdit.Font = New Font("Segoe UI Light", 12.0!)
        Next
    End Sub

    ''' <summary>
    ''' Método para ocultar las opciones de frecuencia
    ''' </summary>
    Public Sub Clean_FrequencyType()
        INDLyRepeatDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLyRepeatDays1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLyWeekDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLyMonthDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLyGrRepeatInterval.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLyHoliday.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDRgHoliday.SelectedIndex = -1
        INDTxtRepeatDays.EditValue = 1

    End Sub

    ''' <summary>
    ''' Método que Establece la accion a realizar para el tipo de frecuencia de un turno
    ''' </summary>
    ''' <param name="Action"></param>
    ''' <remarks></remarks>
    Public Sub ActionOn_FrequencyType(ByVal Action As Integer)
        Clean_FrequencyType()
        Select Case Action
            Case Is = 1
                INDLyRepeatDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLyRepeatDays1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLyHoliday.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLyGrRepeatInterval.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDRgHoliday.SelectedIndex = 0
                INDTxtRepeatDays.Focus()
            Case Is = 2
                INDLyWeekDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLyHoliday.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLyGrRepeatInterval.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDRgHoliday.SelectedIndex = 0
            Case Is = 3
                INDLyMonthDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLyHoliday.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDRgHoliday.SelectedIndex = 0
        End Select
    End Sub

    ''' <summary>
    ''' Metodo para limpiar los campos del pop up de agregar nuevas horas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Clean_Values_Popup()
        INDPopupTeEndingTime.EditValue = Nothing
        INDPopupTeInitialTime.EditValue = Nothing
        INDPopupRgNextDay.EditValue = False
        INDPopupRgInitialNextDay.EditValue = False
    End Sub

#End Region

#Region "bar buttons"
    ''' <summary>
    '''Evento load de la barra de Schedule Detail.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        'ShowHideEditSchedule()
        SetDocumentsSchedule()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        ActionForm = 2
        Close()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Async Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar

        If EventLastMonth = True Then
            Dim eventSelected As Boolean = Await SetEventSelected()
            If eventSelected Then
                Await ValidateTurns()
                'Assing_Values()
                If ValidateControls() = True Then
                    Process_ScheduleDetail()
                    Close()
                    ActionForm = 1
                End If
            Else
                ActionForm = 4
                Close()
            End If
        Else
            Await ValidateTurns()
            ' Assing_Values()
            If ValidateControls() = True Then
                Process_ScheduleDetail()
                Close()
                ActionForm = 1
            End If
        End If

    End Sub

    Private Async Function SetEventSelected() As Task(Of Boolean)
        Dim eventSelected As Boolean = False
        Dim lista As New TrackableCollection(Of ScheduleDetailHour)
        For Each item In SchedulDetail.ScheduleDetailHour
            If item.Event Then
                item.EventLastMonth = True
                lista.Add(item)
                eventSelected = True
            End If
        Next
        SchedulDetail.ScheduleDetailHour = lista

        Return eventSelected
    End Function

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo

    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar

    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        If ValidateControls() = True Then
            Process_ScheduleDetail()
            Close()
            ActionForm = 1
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout

    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean)
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub
#End Region


    Private Sub INDPopupRgInitialNextDay_EditValueChanged(sender As Object, e As EventArgs) Handles INDPopupRgInitialNextDay.EditValueChanged
        Dim rgInitial = CType(sender, RadioGroup)
        If CType(rgInitial.EditValue, Boolean) = True Then
            INDPopupRgNextDay.EditValue = True
            INDPopupRgNextDay.Enabled = False
        Else
            INDPopupRgNextDay.EditValue = False
            INDPopupRgNextDay.Enabled = True
        End If
    End Sub

    Private Sub CtrCalendarMini_Load(sender As Object, e As EventArgs) Handles CtrCalendarMini.Load


    End Sub
End Class
''' <summary>
''' Enumeracion que contiene el tipo de dia, normal o nocturno
''' </summary>
''' <remarks></remarks>
Public Enum EDayType
    ''' <summary>
    ''' Normal
    ''' </summary>
    ''' <remarks></remarks>
    Normal = 1
    ''' <summary>
    ''' Nocturno
    ''' </summary>
    ''' <remarks></remarks>
    Night = 2
End Enum
