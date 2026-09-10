'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/05/2020
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
Imports Infrastructure.Data.Xpo.AuthorizationRepository

#End Region

Public Class FrmEventAndNovelty

#Region "Variables"

    ''' <summary>
    ''' Permite saber si se va a realizar un evento
    ''' </summary>
    Public IsEvent As Boolean

#End Region

#Region "Event"

    Public Event AddArgs(sender As Object, e As AddEventsAndNoveltiesEventArgs)

#End Region

#Region "Properties"

    Public WriteOnly Property TextForm() As String
        Set(value As String)
            Me.Text = value
        End Set
    End Property

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

    ''' <summary>
    ''' Inicializa los search que son con datos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim listNoveltyType As New List(Of Tuple(Of Integer, String))
        listNoveltyType.Add(New Tuple(Of Integer, String)(1, "Incapacidad"))
        listNoveltyType.Add(New Tuple(Of Integer, String)(2, "Permiso"))
        listNoveltyType.Add(New Tuple(Of Integer, String)(3, "Vacaciones"))
        listNoveltyType.Add(New Tuple(Of Integer, String)(4, "Otro"))
        INDsleNoveltyType.Properties.DataSource = listNoveltyType.ToList()
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub FrmRangeHours_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeTuples()
        If IsEvent = False Then 'Si es una novedad se muestra el control de tipo incapacidades
            INDlyItemNoveltyType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else 'Si es un evento se oculta
            INDlyItemNoveltyType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAddRange_Click(sender As Object, e As EventArgs) Handles INDbtnAddRange.Click
        Dim errors As New StringBuilder

        If INDlyItemNoveltyType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleNoveltyType.EditValue Is Nothing Then
                errors.AppendLine("Debe ingresar un tipo de novedad")
            End If
        End If

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

        Dim authorizationScheduleDetailHour As New AuthorizationScheduleDetailHour
        With authorizationScheduleDetailHour
            .NextDay = INDcheckNextDay.EditValue
            .InitialTime = If(TypeOf INDdteInitial.EditValue Is TimeSpan, INDdteInitial.EditValue, CDate(INDdteInitial.EditValue).TimeOfDay)
            .EndingTime = If(TypeOf INDdteEnd.EditValue Is TimeSpan, INDdteEnd.EditValue, CDate(INDdteEnd.EditValue).TimeOfDay)
            .NumberHour = .EndingTime.TotalHours - .InitialTime.TotalHours
            .Type = If(IsEvent, 2, 3)
            .NoveltyType = Nothing
            If INDlyItemNoveltyType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .NoveltyType = CInt(INDsleNoveltyType.EditValue)
            End If
        End With

        Dim args As New AddEventsAndNoveltiesEventArgs
        args.AuthorizationScheduleDetailHour = authorizationScheduleDetailHour
        RaiseEvent AddArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRangeHours_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If IsEvent Then
            INDdteInitial.Focus()
        Else
            INDsleNoveltyType.Focus()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara para cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRangeHours_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddEventsAndNoveltiesEventArgs
    Inherits EventArgs

    Property AuthorizationScheduleDetailHour As AuthorizationScheduleDetailHour

End Class
