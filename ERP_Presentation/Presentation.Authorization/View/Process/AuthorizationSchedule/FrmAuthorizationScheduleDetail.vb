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
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports System.Drawing
Imports DevExpress.XtraEditors.BaseCheckedListBoxControl
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports DevExpress.XtraEditors.Controls

#End Region

Public Class FrmAuthorizationScheduleDetail

#Region "Properties"

    ''' <summary>
    ''' Nombre del usuario
    ''' </summary>
    Public WriteOnly Property UserDescription As String
        Set(value As String)
            INDlbUserName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre de la plantilla de turnos
    ''' </summary>
    Public WriteOnly Property ScheduleTemplateDescription As String
        Set(value As String)
            INDtxtScheduleTemplate.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
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

#Region "Event"

    ''' <summary>
    ''' Evento para cargar el calendario cuando se guarde desde este modal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event ReturnModalArgs(sender As Object, e As EventArgs)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Listado de horas que viene cuando se edita el día en el calendario principal
    ''' </summary>
    Public ListAuthorizationScheduleDetailHour As List(Of AuthorizationScheduleDetailHour)

    ''' <summary>
    ''' Representa el día en el que se va a editar
    ''' </summary>
    Public AuthorizationScheduleDetailXpo As AuthorizationScheduleDetailXpo

    ''' <summary>
    ''' Entidad que se utiliza para guardar
    ''' </summary>
    Dim AuthorizationSchedule As AuthorizationSchedule

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa los search que son con datos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim listType As New List(Of Tuple(Of Integer, String))
        listType.Add(New Tuple(Of Integer, String)(1, "Turno"))
        listType.Add(New Tuple(Of Integer, String)(2, "Evento"))
        listType.Add(New Tuple(Of Integer, String)(3, "Novedad"))
        INDsleType.Properties.DataSource = listType.ToList()

        Dim listNoveltyType As New List(Of Tuple(Of Integer, String))
        listNoveltyType.Add(New Tuple(Of Integer, String)(1, "Incapacidad"))
        listNoveltyType.Add(New Tuple(Of Integer, String)(2, "Permiso"))
        listNoveltyType.Add(New Tuple(Of Integer, String)(3, "Vacaciones"))
        listNoveltyType.Add(New Tuple(Of Integer, String)(4, "Otro"))
        INDsleNoveltyType.Properties.DataSource = listNoveltyType.ToList()
    End Sub

    ''' <summary>
    ''' Guarda las horas
    ''' </summary>
    Private Async Sub Guardar()
        AssigningValues()

        Me.AsyncLoader(True)
        Try
            Using model As New MAuthorizationSchedule(Me.Tag.ToString())
                Dim result = Await model.SaveAuthorizationSchedule(AuthorizationSchedule, Nothing)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    RaiseEvent ReturnModalArgs(Nothing, Nothing)
                    Me.Close()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Asigna los valores cuando se va a guardar los turnos normales
    ''' </summary>
    Private Sub AssigningValues()
        AuthorizationSchedule = New AuthorizationSchedule()

        With AuthorizationSchedule
            .Id = AuthorizationScheduleDetailXpo.AuthorizationScheduleId.Id
            .AuthorizationScheduleTemplateId = AuthorizationScheduleDetailXpo.AuthorizationScheduleId.AuthorizationScheduleTemplateId.Id
            .Month = AuthorizationScheduleDetailXpo.AuthorizationScheduleId.Month
            .Year = AuthorizationScheduleDetailXpo.AuthorizationScheduleId.Year
            .UserId = AuthorizationScheduleDetailXpo.AuthorizationScheduleId.UserId
            .UserCode = AuthorizationScheduleDetailXpo.AuthorizationScheduleId.UserCode
            .Status = AuthorizationScheduleDetailXpo.AuthorizationScheduleId.Status

            Dim authorizationScheduleDetail As New AuthorizationScheduleDetail
            With authorizationScheduleDetail
                .Id = AuthorizationScheduleDetailXpo.Id
                .AuthorizationScheduleId = AuthorizationScheduleDetailXpo.AuthorizationScheduleId.Id
                .Schedule = AuthorizationScheduleDetailXpo.Schedule
                .Day = AuthorizationScheduleDetailXpo.Day
                .NumberHour = 0
                .Status = AuthorizationScheduleDetailXpo.Status
            End With

            ListAuthorizationScheduleDetailHour.ForEach(Sub(item) authorizationScheduleDetail.AuthorizationScheduleDetailHour.Add(item))

            .AuthorizationScheduleDetail.Add(authorizationScheduleDetail)
        End With
    End Sub

    ''' <summary>
    ''' Asigna los valores para eliminar
    ''' </summary>
    Private Sub AssigningValuesToDelete()
        'Se instancia la cabecera pero esta no se elimina, solo se elimina el día con sus horas
        AuthorizationSchedule = New AuthorizationSchedule()

        'Se establece el día a eliminar
        Dim authorizationScheduleDetail As New AuthorizationScheduleDetail
        authorizationScheduleDetail.Id = AuthorizationScheduleDetailXpo.Id

        'Se asocia el día a la cabecera para enviar a eliminar
        AuthorizationSchedule.AuthorizationScheduleDetail.Add(authorizationScheduleDetail)
    End Sub

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    Private Async Sub Eliminar()
        If Not BarraBotones.PermissionsForm.ContainsKey(1) Then
            Mensaje(EeventViewerImages.Advertencia) = "El usuario no tiene permiso para eliminar"
            Exit Sub
        End If

        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        AssigningValuesToDelete()

        Me.AsyncLoader(True)

        Try
            Using model As New MAuthorizationSchedule(Me.Tag.ToString())
                Dim result = Await model.DeleteAuthorizationSchedule(AuthorizationSchedule)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                    RaiseEvent ReturnModalArgs(Nothing, Nothing)
                    Me.Close()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAuthorizationScheduleDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeTuples()
        INDgcHours.DataSource = Nothing
        INDgcHours.DataSource = ListAuthorizationScheduleDetailHour
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If INDsleType.EditValue IsNot Nothing Then
            If INDsleType.EditValue = 3 Then 'Si es una novedad
                INDlyItemNoveltyType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else 'Si es un turno o evento
                INDlyItemNoveltyType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnEdit_Click(sender As Object, e As EventArgs) Handles INDbtnEdit.Click
        Dim errors As New StringBuilder

        If INDsleType.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un tipo")
        End If

        If INDlyItemNoveltyType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleNoveltyType.EditValue Is Nothing OrElse INDsleNoveltyType.EditValue = 0 Then
                errors.AppendLine("Debe seleccionar un tipo de novedad")
            End If
        End If

        If INDdteInitialTime.EditValue Is Nothing Then
            errors.AppendLine("Debe ingresar una hora inicial")
        End If

        If INDdteEndingTime.EditValue Is Nothing Then
            errors.AppendLine("Debe ingresar una hora final")
        End If

        If errors.ToString.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If

        Dim hour = CType(INDviewHours.GetFocusedRow(), AuthorizationScheduleDetailHour)
        Dim indexOf = ListAuthorizationScheduleDetailHour.IndexOf(hour)

        With hour
            .Type = CInt(INDsleType.EditValue)
            .NoveltyType = Nothing
            If INDlyItemNoveltyType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .NoveltyType = CInt(INDsleNoveltyType.EditValue)
            End If
            .InitialTime = If(TypeOf INDdteInitialTime.EditValue Is TimeSpan, INDdteInitialTime.EditValue, CDate(INDdteInitialTime.EditValue).TimeOfDay)
            .EndingTime = If(TypeOf INDdteEndingTime.EditValue Is TimeSpan, INDdteEndingTime.EditValue, CDate(INDdteEndingTime.EditValue).TimeOfDay)
            .NextDay = INDcheckNextDay.EditValue
        End With

        ListAuthorizationScheduleDetailHour.RemoveAt(indexOf)
        ListAuthorizationScheduleDetailHour.Insert(indexOf, hour)

        INDgcHours.DataSource = Nothing
        INDgcHours.DataSource = ListAuthorizationScheduleDetailHour
        INDgcHours.RefreshDataSource()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de editar de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepPceEdit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPceEdit.QueryPopUp
        Dim hour = CType(INDviewHours.GetFocusedRow(), AuthorizationScheduleDetailHour)
        With hour
            INDsleType.EditValue = .Type
            INDsleNoveltyType.EditValue = .NoveltyType
            INDdteInitialTime.EditValue = .InitialTime
            INDdteEndingTime.EditValue = .EndingTime
            INDcheckNextDay.EditValue = .NextDay
        End With

        INDsleType.Focus()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara para cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAuthorizationScheduleDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

#End Region

End Class