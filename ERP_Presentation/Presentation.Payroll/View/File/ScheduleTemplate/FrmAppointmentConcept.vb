Imports DevExpress.XtraEditors
Imports Presentation.Payroll.MVP
Imports Domain.Payroll.Entities
Imports DevExpress.XtraScheduler
Imports Domain.Base.Entities
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Base


Public Class FrmAppointmentConcept

#Region "Properties"

    ''' <summary>
    ''' Variable para almacenar los detalles de los conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Private listConceptDetail As List(Of ScheduleTemplateConceptDetail)
    ''' <summary>
    ''' Lista de concepto el cual cargara los combos
    ''' </summary>
    ''' <remarks></remarks>
    Private listConcept As List(Of Concept)
    ''' <summary>
    ''' Control de schedule
    ''' </summary>
    ''' <remarks></remarks>
    Private controlSchedule As SchedulerControl
    ''' <summary>
    ''' Appointment que estan editando
    ''' </summary>
    ''' <remarks></remarks>
    Private appointmentConcept As AppointmentConcept

    ''' <summary>
    ''' Variable para almacenar la plantilla que se esta modificando
    ''' </summary>
    ''' <remarks></remarks>
    Private scheduleTemplate As ScheduleTemplate

    Dim ListDeleteConcept As New List(Of ScheduleTemplateConceptDetail)

    Public FlagEdit As Boolean = False

#End Region

    Sub New(ByVal control As SchedulerControl, ByVal apt As AppointmentConcept, ByRef scheduleTemplateArg As ScheduleTemplate)

        Me.controlSchedule = control
        Me.appointmentConcept = apt
        Me.scheduleTemplate = scheduleTemplateArg
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub INDTeInitialTime_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs) Handles INDTeInitialTime.Spin, INDTeFinalTime.Spin
        Dim timeEdit As TimeEdit = CType(sender, TimeEdit)
        Dim fechaAntes As Date = timeEdit.Time
        If timeEdit.SelectionStart = 3 Then
            If e.IsSpinUp Then
                timeEdit.Time = timeEdit.Time.AddMinutes(30)
            Else
                timeEdit.Time = timeEdit.Time.AddMinutes(-30)
            End If
            timeEdit.SelectionStart = 3
        End If
        If timeEdit.SelectionStart = 0 Then
            If e.IsSpinUp Then
                timeEdit.Time = timeEdit.Time.AddHours(1)
            Else
                timeEdit.Time = timeEdit.Time.AddHours(-1)
            End If
        End If
        Dim fff = DateDiff(DateInterval.Hour, INDTeInitialTime.Time, INDTeFinalTime.Time)
        If INDTeFinalTime.Time.TimeOfDay < INDTeInitialTime.Time.TimeOfDay And INDTeFinalTime.Time.TimeOfDay <> New TimeSpan(0, 0, 0) Then
            timeEdit.Time = fechaAntes
            MessageIndigo.Show("La fecha final no puede ser menor a la fecha inicial", Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
        End If
        e.Handled = True
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        listConceptDetail = Nothing
        listConcept = Nothing
        controlSchedule = Nothing
        appointmentConcept = Nothing
        scheduleTemplate = Nothing
    End Sub

    Private Async Sub FrmAppointmentConcept_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.Visible = False
        If appointmentConcept.Appointment.Start.Day <> appointmentConcept.Appointment.End.Day Then
            appointmentConcept.Appointment.End = New Date(appointmentConcept.Appointment.Start.Year, appointmentConcept.Appointment.Start.Month, appointmentConcept.Appointment.Start.Day, 0, 0, 0).AddDays(1)
        End If
        INDTeInitialTime.EditValue = appointmentConcept.Appointment.Start
        INDTeFinalTime.EditValue = appointmentConcept.Appointment.End
        INDCeNextDay.Checked = appointmentConcept.ScheduleTemplate.NextDay
        Using model As New MConcept(MConcept.TAG)
            listConcept = Await model.GetConceptByConceptClassAsync(New List(Of String)({"001", "005", "012", "013", "042", "043", "051", "052"}))
        End Using
        INDGlueConcept.Properties.DataSource = listConcept

        Dim ListScheduleTemplateConceptDetail As New List(Of ScheduleTemplateConceptDetail)

        If appointmentConcept.ScheduleTemplate.ScheduleTemplateConceptDetail.Count <= 0 And FlagEdit = True Then

            For Each objScheduleTemplateConcept As ScheduleTemplateConcept In scheduleTemplate.ScheduleTemplateConcept

                If objScheduleTemplateConcept.InitialTime = appointmentConcept.Appointment.Start.TimeOfDay And objScheduleTemplateConcept.EndingTime = appointmentConcept.Appointment.End.TimeOfDay Then
                    ListScheduleTemplateConceptDetail.AddRange(objScheduleTemplateConcept.ScheduleTemplateConceptDetail)
                End If
            Next

            For Each ObjConceptDetail As ScheduleTemplateConceptDetail In ListScheduleTemplateConceptDetail
                appointmentConcept.ScheduleTemplate.ScheduleTemplateConceptDetail.Add(ObjConceptDetail)
            Next

        Else

            ListScheduleTemplateConceptDetail = appointmentConcept.ScheduleTemplate.ScheduleTemplateConceptDetail.ToList()
        End If

        INDGControlConcepts.DataSource = appointmentConcept.ScheduleTemplate.ScheduleTemplateConceptDetail

        If FlagEdit = True Then
            'Estamos editando un control
            INDTeInitialTime.Enabled = False
            INDTeFinalTime.Enabled = False
            INDCeNextDay.Enabled = False
        End If

    End Sub

    Private Sub INDBtnCancel_Click(sender As Object, e As EventArgs) Handles INDBtnCancel.Click
        Me.Close()
    End Sub

    Private Sub INDbtnAddConcept_Click(sender As Object, e As EventArgs) Handles INDbtnAddConcept.Click
        If INDcmbConceptType.SelectedItem IsNot Nothing And INDGlueConcept.EditValue IsNot Nothing Then
            Dim detail As New ScheduleTemplateConceptDetail()
            detail.ConceptType = CType(INDcmbConceptType.EditValue, Byte)
            detail.Concept = CType(INDGlueConcept.EditValue, Concept).MarkAsUnchanged()
            appointmentConcept.ScheduleTemplate.ScheduleTemplateConceptDetail.Add(detail)
            INDGControlConcepts.RefreshDataSource()
            INDGlueConcept.EditValue = Nothing
        End If
    End Sub

    Private Sub RepositoryItemTextEdit1_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles RepositoryItemTextEdit1.CustomDisplayText
        If e.Value = 0 Then
            e.DisplayText = "Ordinario"
        Else
            e.DisplayText = "Feriado"
        End If
    End Sub

    Private Sub INDbtnEliminar_Click(sender As Object, e As EventArgs) Handles INDbtnEliminar.Click
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim ToDelete = CType(INDGControlConcepts.DefaultView.GetRow(CType(INDGControlConcepts.DefaultView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle), ScheduleTemplateConceptDetail)
            ToDelete.MarkAsDeleted()
            appointmentConcept.ScheduleTemplate.ScheduleTemplateConceptDetail.Remove(ToDelete)
            ListDeleteConcept.Add(ToDelete)
            INDGControlConcepts.RefreshDataSource()
        End If
    End Sub

    Private Sub INDBtnOK_Click(sender As Object, e As EventArgs) Handles INDBtnOK.Click
        Dim feriado = From f In appointmentConcept.ScheduleTemplate.ScheduleTemplateConceptDetail
                      Where f.ConceptType = 1
                      Select f
        Dim ordinario = From o In appointmentConcept.ScheduleTemplate.ScheduleTemplateConceptDetail
                        Where o.ConceptType = 0
                        Select o
        Dim diferencia = Math.Abs(DateDiff(DateInterval.Minute, INDTeInitialTime.Time, INDTeFinalTime.Time) / 60)
        'If CInt(diferencia) <> diferencia Then
        '    MessageIndigo.Show(String.Format(obtenerRecurso(RangoHorasCompletas, Eform.PlantillaDeTurno), diferencia), Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
        '    Exit Sub
        'End If
        If diferencia = 0 Then
            MessageIndigo.Show(obtenerRecurso(TiempoMayorCero, Eform.PlantillaDeTurno), Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
            Exit Sub
        End If
        If feriado.Count = 0 Then
            MessageIndigo.Show(obtenerRecurso(MinimoUnFeriado, Eform.PlantillaDeTurno), Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
            Exit Sub
        ElseIf ordinario.Count = 0 Then
            MessageIndigo.Show(obtenerRecurso(MinimoUnOrdinario, Eform.PlantillaDeTurno), Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
            Exit Sub
        End If

        'For Each item As AppointmentConcept In controlSchedule.Storage.Appointments.Items
        '    If item.ScheduleTemplate.InitialTime <> appointmentConcept.ScheduleTemplate.InitialTime And item.ScheduleTemplate.EndingTime <> appointmentConcept.ScheduleTemplate.EndingTime Then 'Si es otro item diferente al que estoy modificando actualmente
        '        Dim timeFinalTime = ReturnTimeSpan23(INDTeFinalTime.Time.TimeOfDay)
        '        Dim timeInitialTime = ReturnTimeSpan00(INDTeInitialTime.Time.TimeOfDay)
        '        Dim timeItemInitial = ReturnTimeSpan00(item.ScheduleTemplate.InitialTime)
        '        Dim timeItemFinal = ReturnTimeSpan23(item.ScheduleTemplate.EndingTime)
        '        ''Dim timeInitialTime = ReturnTimeSpan23()
        '        If ((timeInitialTime >= timeItemInitial And timeFinalTime <= timeItemFinal) Or (timeItemInitial >= timeInitialTime And timeItemFinal <= timeFinalTime)) And item.ScheduleTemplate.NextDay = INDCeNextDay.Checked Then
        '            MessageIndigo.Show(obtenerRecurso(AppointmentExiste, Eform.PlantillaDeTurno), Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
        '            Exit Sub
        '        End If
        '    End If
        'Next

        If scheduleTemplate IsNot Nothing AndAlso scheduleTemplate.Id > 0 And FlagEdit = True Then
            'Estoy editando una plantilla ya creada
            Dim ListScheduleDetailConcept As New List(Of ScheduleTemplateConceptDetail)

            ListScheduleDetailConcept = appointmentConcept.ScheduleTemplate.ScheduleTemplateConceptDetail.ToList()


            Dim ListScheduleTemplateConcept = scheduleTemplate.ScheduleTemplateConcept.ToList()

            ListScheduleTemplateConcept = ListScheduleTemplateConcept.Where(Function(x) x.InitialTime = CDate(INDTeInitialTime.EditValue).TimeOfDay And x.EndingTime = CDate(INDTeFinalTime.EditValue).TimeOfDay).ToList()

            If ListScheduleTemplateConcept IsNot Nothing AndAlso ListScheduleTemplateConcept.Count > 0 Then
                For Each objScheduleTemplateConcept As ScheduleTemplateConcept In ListScheduleTemplateConcept

                    While objScheduleTemplateConcept.ScheduleTemplateConceptDetail.Count() > 0
                        objScheduleTemplateConcept.ScheduleTemplateConceptDetail.Item(0).MarkAsDeleted()
                    End While

                    For Each ObjTmp As ScheduleTemplateConceptDetail In ListScheduleDetailConcept
                        objScheduleTemplateConcept.ScheduleTemplateConceptDetail.Add(ObjTmp)
                    Next

                Next
            End If

        Else

            Dim dateAppointment As Date
            If INDCeNextDay.Checked = False Then
                dateAppointment = Date.Now()
            Else
                dateAppointment = Date.Now().AddDays(1)
            End If
            INDTeInitialTime.Time = New Date(dateAppointment.Year, dateAppointment.Month, dateAppointment.Day, INDTeInitialTime.Time.Hour, INDTeInitialTime.Time.Minute, INDTeInitialTime.Time.Second)
            If INDTeFinalTime.Time.Hour = 0 And INDTeFinalTime.Time.Minute = 0 Then
                dateAppointment = dateAppointment.AddDays(1)
            End If
            INDTeFinalTime.Time = New Date(dateAppointment.Year, dateAppointment.Month, dateAppointment.Day, INDTeFinalTime.Time.Hour, INDTeFinalTime.Time.Minute, INDTeFinalTime.Time.Second)
            appointmentConcept.Appointment.Start = INDTeInitialTime.Time
            appointmentConcept.Appointment.End = INDTeFinalTime.Time
            With appointmentConcept.ScheduleTemplate
                .Consecutive = 0
                .NumberHour = diferencia
                .InitialTime = INDTeInitialTime.Time.TimeOfDay
                .EndingTime = INDTeFinalTime.Time.TimeOfDay
                .NextDay = INDCeNextDay.Checked
                .State = True
            End With
            If appointmentConcept.ScheduleTemplate.ScheduleTemplateId = 0 Then
                scheduleTemplate.ScheduleTemplateConcept.Add(appointmentConcept.ScheduleTemplate)
            End If
            appointmentConcept.Appointment.Subject = FrmAppointmentConcept.DescripcionAppointment(INDTeInitialTime.Time.TimeOfDay, INDTeFinalTime.Time.TimeOfDay, appointmentConcept.ScheduleTemplate.ScheduleTemplateConceptDetail)
            controlSchedule.Storage.Appointments.Add(appointmentConcept.Appointment)
        End If

        Me.Close()
    End Sub

    Public Shared Function DescripcionAppointment(initialTime As TimeSpan, EndingTime As TimeSpan, ListConceptDetail As TrackableCollection(Of ScheduleTemplateConceptDetail)) As String
        Dim feriado = From f In ListConceptDetail
                      Where f.ConceptType = 1
                      Select f
        Dim ordinario = From o In ListConceptDetail
                        Where o.ConceptType = 0
                        Select o
        Dim StrFeriado As String = vbNewLine
        For Each item As ScheduleTemplateConceptDetail In feriado
            StrFeriado += vbNewLine & item.Concept.Code & " - " & item.Concept.Name
        Next
        Dim StrOrdinario As String = vbNewLine
        For Each item As ScheduleTemplateConceptDetail In ordinario
            StrOrdinario += vbNewLine & item.Concept.Code & " - " & item.Concept.Name
        Next
        Return initialTime.ToString() & " a " & EndingTime.ToString() & vbNewLine & vbNewLine & vbNewLine & obtenerRecurso(ConceptoOrdinario, PlantillaDeTurno) & ": " & ordinario.Count() & StrOrdinario & vbNewLine & vbNewLine & vbNewLine & obtenerRecurso(ConceptoFeriado, PlantillaDeTurno) & ": " & feriado.Count() & StrFeriado
    End Function

    Private Function ReturnTimeSpan23(time As TimeSpan) As TimeSpan
        Dim TimeSpan As TimeSpan = time
        If TimeSpan = New TimeSpan(0, 0, 0) Then
            TimeSpan = New TimeSpan(23, 59, 59)
        End If
        Return TimeSpan
    End Function

    Private Function ReturnTimeSpan00(time As TimeSpan) As TimeSpan
        Dim TimeSpan As TimeSpan = time
        If TimeSpan = New TimeSpan(0, 0, 0) Then
            TimeSpan = New TimeSpan(0, 0, 1)
        End If
        Return TimeSpan
    End Function

End Class