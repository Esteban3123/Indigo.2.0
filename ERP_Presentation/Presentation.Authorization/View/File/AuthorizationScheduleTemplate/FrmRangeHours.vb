'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/03/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports System.Text
Imports Presentation.Authorization.MVP
Imports DevExpress.Xpo
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports System.Drawing

#End Region

Public Class FrmRangeHours

#Region "Event"

    Public Event AddAppointmentArgs(sender As Object, e As AddAppointmentEventArgs)

#End Region

#Region "Properties"

    Public Appointment As Appointment

#End Region

#Region "Properties"

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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



#End Region

#Region "Handlers"

#Region "Load"

    Private Sub FrmRangeHours_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDdteInitial.EditValue = Appointment.Start.TimeOfDay
        INDdteEnd.EditValue = Appointment.End.TimeOfDay

        If Appointment.Start.Day = GetDateServer().AddDays(1).Day OrElse Appointment.End.Day = GetDateServer().AddDays(1).Day Then
            INDcheckNextDay.EditValue = True
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub AddAppointmentEventArgs(sender As Object, e As EventArgs) Handles INDbtnAddRange.Click
        Dim errors As New StringBuilder

        If INDdteInitial.EditValue Is Nothing Then
            errors.AppendLine("Debe ingresar una hora inicial")
        End If

        If INDdteEnd.EditValue Is Nothing Then
            errors.AppendLine("Debe ingresar una hora final")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If

        Dim hourInitial As TimeSpan = If(TypeOf INDdteInitial.EditValue Is TimeSpan, INDdteInitial.EditValue, CDate(INDdteInitial.EditValue).TimeOfDay)
        Dim hourEnd As TimeSpan = If(TypeOf INDdteEnd.EditValue Is TimeSpan, INDdteEnd.EditValue, CDate(INDdteEnd.EditValue).TimeOfDay)

        Appointment.Start = New Date(Appointment.Start.Year, Appointment.Start.Month, Appointment.Start.Day, hourInitial.Hours, hourInitial.Minutes, hourInitial.Seconds)
        Appointment.End = New Date(Appointment.End.Year, Appointment.End.Month, Appointment.End.Day, hourEnd.Hours, hourEnd.Minutes, hourEnd.Seconds)
        Appointment.Subject = vbNewLine & vbNewLine & hourInitial.ToString() & "   a   " & hourEnd.ToString()

        Dim args As New AddAppointmentEventArgs
        args.Appointment = Appointment
        RaiseEvent AddAppointmentArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmRangeHours_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDdteInitial.Focus()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmRangeHours_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDdteEnd_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDbtnAddRange.Focus()
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddAppointmentEventArgs
    Inherits EventArgs

    Property Appointment As Appointment

End Class