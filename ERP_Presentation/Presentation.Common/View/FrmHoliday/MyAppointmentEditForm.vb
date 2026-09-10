' Developer Express Code Central Example:
' How to customize the Edit Appointment form to show custom fields
' 
' This example illustrates the use of a custom form to enable the end-user to edit
' custom fields. The custom form is invoked instead of the default one by handling
' the SchedulerControl.EditAppointmentFormShowing
' (ms-help://DevExpress.NETv8.2/DevExpress.XtraScheduler/DevExpressXtraSchedulerSchedulerControl_EditAppointmentFormShowingtopic.htm)
' event.
' 
' See also:
' For a simple application that enables you to handle custom
' fields, see the http://www.devexpress.com/scid=E2782 article.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E152

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.UI
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base


Namespace CustomAppointmentEditForm
#Region "#myappointmenteditingform"
    Partial Public Class MyAppointmentEditForm
        Inherits Presentation.Controls.FormBase

        Private control As SchedulerControl
        Private apt As Appointment
        Private openRecurrenceForm As Boolean = False
        Private suspendUpdateCount As Integer
        Private action As Domain.Base.Entities.ObjectState


        ' The MyAppointmentFormController class is inherited from
        ' the AppointmentFormController to add custom properties.
        ' See its declaration below.
        Private controller As MyAppointmentFormController

        Protected ReadOnly Property Appointments() As AppointmentStorage
            Get
                Return control.Storage.Appointments
            End Get
        End Property
        Protected ReadOnly Property IsUpdateSuspended() As Boolean
            Get
                Return suspendUpdateCount > 0
            End Get
        End Property


        Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment, ByVal openRecurrenceForm As Boolean, Optional _action As Domain.Base.Entities.ObjectState = Domain.Base.Entities.ObjectState.Added)
            Me.openRecurrenceForm = openRecurrenceForm
            Me.controller = New MyAppointmentFormController(control, apt)
            Me.apt = apt
            Me.control = control
            Me.action = _action
            '
            ' Required for Windows Form Designer support
            '
            SuspendUpdate()
            InitializeComponent()
            ResumeUpdate()
            UpdateForm()
            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
        End Sub


        Private Sub MyAppointmentEditForm_Activated(ByVal sender As Object, ByVal e As System.EventArgs)
            ' Required to show the recurrence form.
            If openRecurrenceForm Then
                openRecurrenceForm = False
                OnRecurrenceButton()
            End If
        End Sub
        Private Sub btnRecurrence_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            OnRecurrenceButton()
        End Sub

        Private Sub OnRecurrenceButton()
            ShowRecurrenceForm()
        End Sub

        Private Sub ShowRecurrenceForm()

            If (Not control.SupportsRecurrence) Then
                Return
            End If

            ' Prepare to edit the appointment's recurrence.
            Dim editedAptCopy As Appointment = controller.EditedAppointmentCopy
            Dim editedPattern As Appointment = controller.EditedPattern
            Dim patternCopy As Appointment = controller.PrepareToRecurrenceEdit()

            Dim dlg As New AppointmentRecurrenceForm(patternCopy, control.OptionsView.FirstDayOfWeek, controller)

            ' Required for skin support.
            dlg.LookAndFeel.ParentLookAndFeel = Me.LookAndFeel.ParentLookAndFeel

            Dim result As DialogResult = dlg.ShowDialog(Me)
            dlg.Dispose()

            If result = System.Windows.Forms.DialogResult.Abort Then
                controller.RemoveRecurrence()
            Else
                If result = System.Windows.Forms.DialogResult.OK Then
                    controller.ApplyRecurrence(patternCopy)
                    If controller.EditedAppointmentCopy IsNot editedAptCopy Then
                        UpdateForm()
                    End If
                End If
            End If
            UpdateIntervalControls()
        End Sub



        Protected Sub SuspendUpdate()
            suspendUpdateCount += 1
        End Sub
        Protected Sub ResumeUpdate()
            If suspendUpdateCount > 0 Then
                suspendUpdateCount -= 1
            End If
        End Sub

        Private Sub UpdateForm()
            SuspendUpdate()
            Try
                txSubject.Text = controller.Subject

                dtStart.DateTime = controller.DisplayStart.Date
            Finally
                ResumeUpdate()
            End Try
            UpdateIntervalControls()
        End Sub

        Protected Overridable Sub UpdateIntervalControls()
            If IsUpdateSuspended Then
                Return
            End If

            SuspendUpdate()
            Try
                dtStart.EditValue = controller.DisplayStart.Date

            Finally
                ResumeUpdate()
            End Try
        End Sub


        Private Sub Guardar()
            ' Required to check the appointment for conflicts.
            If (Not controller.IsConflictResolved()) Then
                Return
            End If
            If txSubject.Text = String.Empty Then
                controller.Subject = "Festivo"
            Else
                controller.Subject = txSubject.Text
            End If

            'controller.SetStatus(edStatus.Status)
            'controller.SetLabel(edLabel.Label)
            'controller.AllDay = Me.checkAllDay.Checked
            controller.DisplayStart = Me.dtStart.DateTime.Date '+ Me.timeStart.Time.TimeOfDay
            'controller.DisplayEnd = Me.dtEnd.DateTime.Date + Me.timeEnd.Time.TimeOfDay
            'controller.CustomName = txCustomName.Text
            'controller.CustomStatus = txCustomStatus.Text

            ' Save all changes of the editing appointment.
            controller.ApplyChanges()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End Sub


        Public Class MyAppointmentFormController
            Inherits AppointmentFormController

            Public Property CustomName() As String
                Get
                    Return CStr(EditedAppointmentCopy.CustomFields("CustomName"))
                End Get
                Set(ByVal value As String)
                    EditedAppointmentCopy.CustomFields("CustomName") = value
                End Set
            End Property
            Public Property CustomStatus() As String
                Get
                    Return CStr(EditedAppointmentCopy.CustomFields("CustomStatus"))
                End Get
                Set(ByVal value As String)
                    EditedAppointmentCopy.CustomFields("CustomStatus") = value
                End Set
            End Property

            Private Property SourceCustomName() As String
                Get
                    Return CStr(SourceAppointment.CustomFields("CustomName"))
                End Get
                Set(ByVal value As String)
                    SourceAppointment.CustomFields("CustomName") = value
                End Set
            End Property
            Private Property SourceCustomStatus() As String
                Get
                    Return CStr(SourceAppointment.CustomFields("CustomStatus"))
                End Get
                Set(ByVal value As String)
                    SourceAppointment.CustomFields("CustomStatus") = value
                End Set
            End Property

            Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment)
                MyBase.New(control, apt)
            End Sub

            Public Overrides Function IsAppointmentChanged() As Boolean
                If MyBase.IsAppointmentChanged() Then
                    Return True
                End If
                Return SourceCustomName <> CustomName OrElse SourceCustomStatus <> CustomStatus
            End Function
        End Class

#Region "Eventos Barra Botones"
        ''' <summary>
        '''Evento load de la barra de usuarios.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub CtrBarraBotones1_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
            Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
            If Me.action = Domain.Base.Entities.ObjectState.Added Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
            End If
        End Sub

        ''' <summary>
        ''' Barras the botones_ click deshacer.
        ''' </summary>
        Private Sub BarraBotones_ClickDeshacer()
        End Sub

        ''' <summary>
        ''' Barras the botones_ click eliminar.
        ''' </summary>
        Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                apt.Delete()
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
            End If
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
#End Region
    End Class
#End Region ' #myappointmenteditingform
End Namespace
