Imports DevExpress.XtraScheduler
Imports Domain.Payroll.Entities

Public Class AppointmentConcept
    Inherits SchedulerStorage

    Private datos As ScheduleTemplateConcept
    Private _appointment As Appointment

    Public ReadOnly Property ScheduleTemplate() As ScheduleTemplateConcept
        Get
            Return datos
        End Get
    End Property

    Public Property Appointment() As Appointment
        Set
            _appointment = Value
        End Set
        Get
            Return _appointment
        End Get
    End Property

    Sub New(start As DateTime, pEnd As DateTime, subject As String, item As ScheduleTemplateConcept)
        'MyBase.New(AppointmentType.Normal, start, pEnd, subject)
        Appointment = MyBase.CreateAppointment(AppointmentType.Normal, start, pEnd, subject)
        Me.datos = item
    End Sub

    Sub New(start As DateTime, duration As TimeSpan, subject As String, item As ScheduleTemplateConcept)
        'MyBase.New(AppointmentType.Normal, start, duration, subject)
        Appointment = MyBase.CreateAppointment(AppointmentType.Normal, start, duration, subject)
        Me.datos = item
    End Sub

    Sub New(appointmentAux As Appointment, item As ScheduleTemplateConcept)
        'MyBase.New(AppointmentType.Normal, appointment.Start, appointment.End, appointment.Subject)
        Appointment = MyBase.CreateAppointment(AppointmentType.Normal, appointmentAux.Start, appointmentAux.End, appointmentAux.Subject)
        'MyBase.Assign(appointmentAux)
        Me.datos = item
    End Sub

    'Public Shared Narrowing Operator CType(ByVal e As Appointment) As AppointmentConcept
    '    Dim app As New AppointmentConcept(e)
    '    Return app
    'End Operator
End Class
