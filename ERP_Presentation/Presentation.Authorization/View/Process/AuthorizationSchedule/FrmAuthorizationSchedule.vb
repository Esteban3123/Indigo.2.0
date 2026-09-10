'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/05/2020
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
Imports Presentation.Authorization
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmAuthorizationSchedule

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PAuthorizationSchedule

    ''' <summary>
    ''' Representa el listado de días cuando se le da click a alguno
    ''' </summary>
    Dim ListDays As List(Of Integer)

    ''' <summary>
    ''' Cabecera del turno, que se utiliza para mandar a guardar
    ''' </summary>
    Dim AuthorizationSchedule As AuthorizationSchedule

    ''' <summary>
    ''' Entidad xpo utlizada para cuando se carga los turnos en el loadControls
    ''' </summary>
    Dim AuthorizationScheduleXpo As AuthorizationScheduleXpo

    ''' <summary>
    ''' Plantilla de turno cargada cuando se cambia el valor del search de plantilla de turno
    ''' </summary>
    Dim AuthorizationScheduleTemplateXpo As AuthorizationScheduleTemplateXpo

    ''' <summary>
    ''' Entidad que contiene el día, esta variable se llena cuando se realiza el popupover en cada día para desplegar el detalle
    ''' </summary>
    Dim AuthorizationScheduleDetailXpo As AuthorizationScheduleDetailXpo

#End Region

#Region "Properties"

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

#Region "Methods"

    ''' <summary>
    ''' Método que abre el form de detalle para poder editar el turno, el evento o la novedad
    ''' </summary>
    Private Sub OpenFormDetail()
        'Se cierra el popup
        INDPcCDetail.HidePopup()

        'Se obtiene el usuario checkiado para sacar el id
        Dim itemChecked As CheckedListBoxItem = (From x In INDclbcUsers.Items Where x.CheckState = CheckState.Checked Select x).FirstOrDefault()

        Using formulario As New FrmAuthorizationScheduleDetail()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 750
            formulario.Height = 600

            Dim listEditDetailHours As New List(Of AuthorizationScheduleDetailHour)
            For Each item In AuthorizationScheduleDetailXpo.AuthorizationScheduleDetailHourXpo.ToList()
                listEditDetailHours.Add(SetValuesHoursClone(item))
            Next
            formulario.ListAuthorizationScheduleDetailHour = listEditDetailHours
            formulario.AuthorizationScheduleDetailXpo = AuthorizationScheduleDetailXpo

            formulario.UserDescription = itemChecked.Description
            formulario.ScheduleTemplateDescription = INDsleScheduleTemplate.Text
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Se crea este método para poder clonar el xpo
    ''' </summary>
    ''' <param name="entityXpo"></param>
    ''' <returns></returns>
    Private Function SetValuesHoursClone(entityXpo As AuthorizationScheduleDetailHourXpo) As AuthorizationScheduleDetailHour
        Dim newEntity As New AuthorizationScheduleDetailHour

        With newEntity
            .Id = entityXpo.Id
            .AuthorizationScheduleDetailId = entityXpo.AuthorizationScheduleDetailId.Id
            .NextDay = entityXpo.NextDay
            .InitialTime = entityXpo.InitialTime
            .EndingTime = entityXpo.EndingTime
            .NumberHour = entityXpo.NumberHour
            .Type = entityXpo.Type
            .NoveltyType = entityXpo.NoveltyType
            .Status = entityXpo.Status
        End With

        Return newEntity
    End Function

    ''' <summary>
    ''' Método que se dispara al retornar valor del modal de detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnModalArgs(sender As Object, e As EventArgs)
        AsyncLoader(True)
        Try
            LoadControls()
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para limpiar los valores del detalle de schedule en PopUp
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControlsPopupDetail()
        INDPopupLcEmployee.Text = String.Empty
        INDPopupLcTxtTemplate.Text = String.Empty
        INDPopupLcNumberDay.Text = Nothing
        INDPopupGcMatches.DataSource = Nothing
        INDgcDetHours.DataSource = Nothing
        INDPopupLcTxtTemplateHoursNumber.Text = String.Empty
        INDTcgDetailsSchedule.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDTcgDetailsSchedule.SelectedTabPageIndex = 0
    End Sub

    ''' <summary>
    ''' Obtener el nombre de la plantilla por la letra
    ''' </summary>
    ''' <param name="letter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNameTemplate(letter As String) As String
        Select Case letter
            Case Is = "M"
                Return obtenerRecurso(Manana, CuadroDeTurno)
            Case Is = "MT"
                Return obtenerRecurso(MananaTarde, CuadroDeTurno)
            Case Is = "T"
                Return obtenerRecurso(Tarde, CuadroDeTurno)
            Case Is = "MN"
                Return obtenerRecurso(MananaNoche, CuadroDeTurno)
            Case Is = "TN"
                Return obtenerRecurso(TardeNoche, CuadroDeTurno)
            Case Is = "N"
                Return obtenerRecurso(Noche, CuadroDeTurno)
            Case Is = "I"
                Return obtenerRecurso(Incapacidad, CuadroDeTurno)
            Case Is = "L"
                Return obtenerRecurso(Licencia, CuadroDeTurno)
            Case Is = "S"
                Return obtenerRecurso(Sancion, CuadroDeTurno)
            Case Is = "V"
                Return obtenerRecurso(Eresources.Vacaciones, CuadroDeTurno)
            Case Is = "PV"
                Return obtenerRecurso(PermisoVacaciones, CuadroDeTurno)
        End Select
    End Function

    ''' <summary>
    ''' Evento que se dispara al colocar el mouse sobre el icono del día
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Sub OnMouseHoverDays(ByVal sender As Object, ByVal e As EventArgs)
        'Se limpian los controles del poup
        INDCalendar.HighLightPopUpDayClose(Nothing, EventArgs.Empty)
        CleanControlsPopupDetail()

        ListDays = Nothing

        'Se obtiene el control que tiene el foco
        Dim _Control As System.Windows.Forms.Control = sender
        Dim PanelControl As PanelControl = _Control.Parent
        Dim PopUpDropDownButton As DevExpress.XtraEditors.DropDownButton = PanelControl.Controls.Item("DDB" & PanelControl.Tag)
        PopUpDropDownButton.HideDropDown()

        If _Control.GetType().ToString = "DevExpress.XtraEditors.LabelControl" Then
            If _Control.Text = "" Then
                Exit Sub
            End If
        End If

        'Se obtiene el usuario checkiado para sacar el id
        Dim itemChecked As CheckedListBoxItem = (From x In INDclbcUsers.Items Where x.CheckState = CheckState.Checked Select x).FirstOrDefault()

        'Se obtiene el día que tiene el foco cuando se despliega el popup de detalles
        AuthorizationScheduleDetailXpo = (From x In AuthorizationScheduleXpo.AuthorizationScheduleDetailXpo Where x.Day = PanelControl.Tag Select x).FirstOrDefault()

        'Inicializar valores en el popup
        INDLyItemDetailEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDTcgDetailsSchedule.TabPages(1).Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDPopupLcEmployee.Text = itemChecked.Description
        INDPopupLcTxtTemplate.Text = GetNameTemplate(LetterSchedule(AuthorizationScheduleDetailXpo.Schedule))
        INDPopupLcTxtTemplateHoursNumber.Text = AuthorizationScheduleDetailXpo.NumberHour

        'Se obtiene la fecha
        Dim dtfi = indigo.Culture.DateTimeFormat
        Dim popupDate = New Date(INDCalendar.YearControl, INDCalendar.MonthControl, PanelControl.Tag)
        INDPopupLcNumberDay.Text = dtfi.GetDayName(popupDate.DayOfWeek) & ", " & popupDate.Day & " de " & dtfi.GetMonthName(popupDate.Month) & " de " & popupDate.Year

        INDgcDetHours.DataSource = AuthorizationScheduleDetailXpo.AuthorizationScheduleDetailHourXpo.ToList()

        'Se despliega el popup
        PopUpDropDownButton.ShowDropDown()
    End Sub

    ''' <summary>
    ''' Carga el calendario
    ''' </summary>
    Private Sub LoadControls()
        AsyncLoader(True)

        'Se limpian los controles
        CleanDays()

        'Se obtiene el usuario checkiado para sacar el id
        Dim itemChecked As CheckedListBoxItem = (From x In INDclbcUsers.Items Where x.CheckState = CheckState.Checked Select x).FirstOrDefault()

        'Si no hay nada checkiado se retorna
        If itemChecked Is Nothing Then
            AsyncLoader(False)
            Exit Sub
        End If

        'Se consulta la entidad xpo para poder cargar el calendario
        AuthorizationScheduleXpo = Presenter.GetAuthorizationScheduleXpo(INDsleScheduleTemplate.EditValue, INDCalendar.MonthControl, INDCalendar.YearControl, itemChecked.Value)

        'Si no trae datos se retorna
        If AuthorizationScheduleXpo Is Nothing Then
            AsyncLoader(False)
            Exit Sub
        End If

        'Se construye la cabecera
        AuthorizationSchedule = New AuthorizationSchedule
        With AuthorizationSchedule
            .Id = AuthorizationScheduleXpo.Id
            .AuthorizationScheduleTemplateId = AuthorizationScheduleXpo.AuthorizationScheduleTemplateId.Id
            .Month = AuthorizationScheduleXpo.Month
            .Year = AuthorizationScheduleXpo.Year
            .UserId = AuthorizationScheduleXpo.UserId
            .UserCode = AuthorizationScheduleXpo.UserCode
            .Status = AuthorizationScheduleXpo.Status
        End With

        'Se pinta el calendario
        Dim PanelControlMain As DevExpress.XtraEditors.PanelControl = INDCalendar.PanelControlMain
        Dim PanelControlChilds As System.Windows.Forms.Control.ControlCollection = PanelControlMain.Controls
        For i As Integer = 0 To PanelControlChilds.Count - 1
            If PanelControlChilds.Item(i).GetType.ToString = "DevExpress.XtraEditors.PanelControl" Then
                Dim item As DevExpress.XtraEditors.PanelControl = PanelControlChilds.Item(i)
                If item.Tag <> "" Then
                    'Se obtiene el día 
                    Dim dayXpo = (From x In AuthorizationScheduleXpo.AuthorizationScheduleDetailXpo Where x.Day = item.Tag).FirstOrDefault()

                    'Si se encontro el día
                    If dayXpo IsNot Nothing Then

                        Dim PictureEditItem1 As DevExpress.XtraEditors.PictureEdit = Nothing
                        Dim LcItem1 As DevExpress.XtraEditors.LabelControl = Nothing

                        'Se pinta el icono del día siempre y cuando tenga un horario normal
                        If dayXpo.Schedule <= 6 Then
                            PictureEditItem1 = item.Controls.Item("INDPeFirst" & item.Tag)
                            With PictureEditItem1
                                Dim _enumIcon As Controls.EImageIconSchedule = IconSchedule(dayXpo.Schedule)
                                Dim _image As System.Drawing.Image = INDCalendar.SetImageIconSchedule(_enumIcon, True)
                                .Image = _image
                                .Visible = True
                                .Tag = "1"
                            End With
                            LcItem1 = item.Controls.Item("INDLcLetterFirst" & item.Tag)
                            With LcItem1
                                .Text = LetterSchedule(dayXpo.Schedule)
                                .Visible = True
                                .Tag = "1"
                            End With
                        End If

                        Dim PictureEditItem2 As DevExpress.XtraEditors.PictureEdit = Nothing
                        Dim LcItem2 As DevExpress.XtraEditors.LabelControl = Nothing

                        'Se valida si el día tiene horario guardado
                        If dayXpo.AuthorizationScheduleDetailHourXpo IsNot Nothing AndAlso dayXpo.AuthorizationScheduleDetailHourXpo.Count > 0 Then
                            'Se obtiene el horario que no sea un horario normal, sino que sea evento o novedad
                            Dim hourXpo = (From x In dayXpo.AuthorizationScheduleDetailHourXpo Where x.Type > 1).FirstOrDefault()

                            'Si encontro se pinta el icono respectivo para el evento o la novedad
                            If hourXpo IsNot Nothing Then
                                PictureEditItem2 = item.Controls.Item("INDPeSecond" & item.Tag)
                                With PictureEditItem2
                                    .Image = IconScheduleEventOrNovelty(If(hourXpo.Type = 2, 0, hourXpo.NoveltyType))
                                    .Visible = True
                                    .Tag = "2"
                                End With
                                LcItem2 = item.Controls.Item("INDLcLetterSecond" & item.Tag)
                                With LcItem2
                                    .Text = LetterSchedule(If(hourXpo.Type = 2, 7, 8))
                                    .Visible = True
                                    .Tag = "2"
                                End With
                            End If
                        End If

                        'Se posicionan las imagenes y los labels
                        If PictureEditItem1 IsNot Nothing AndAlso PictureEditItem2 IsNot Nothing Then
                            PictureEditItem1.Location = INDCalendar.PicPositionCouple1
                            LcItem1.Location = INDCalendar.LabPositionCouple1

                            PictureEditItem2.Location = INDCalendar.PicPositionCouple2
                            LcItem2.Location = INDCalendar.LabPositionCouple2
                        ElseIf PictureEditItem1 IsNot Nothing Then
                            PictureEditItem1.Location = INDCalendar.PicPositionSingle
                            LcItem1.Location = INDCalendar.LabPositionSingle
                        ElseIf PictureEditItem2 IsNot Nothing Then
                            PictureEditItem2.Location = INDCalendar.PicPositionSingle
                            LcItem2.Location = INDCalendar.LabPositionSingle
                        End If

                    End If
                End If
            End If
        Next

        'Se realiza la sumatoria de las horas
        If AuthorizationScheduleXpo IsNot Nothing AndAlso AuthorizationScheduleXpo.AuthorizationScheduleDetailXpo IsNot Nothing AndAlso AuthorizationScheduleXpo.AuthorizationScheduleDetailXpo.Count > 0 Then
            Dim sumHour = (From x In AuthorizationScheduleXpo.AuthorizationScheduleDetailXpo Select x.NumberHour).Sum()
            INDLcTotalHoursNumber1.Text = sumHour.ToString()
        End If

        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Metodo que devuelve la enumeracion del icono que se debe colocar en un determinado turno dependiendo el horario
    ''' </summary>
    Public Function IconSchedule(schedule As Integer) As EImageIconSchedule
        Select Case schedule
            Case 1
                Return EImageIconSchedule.Morning
            Case 2
                Return EImageIconSchedule.Afternoon
            Case 3
                Return EImageIconSchedule.Night
            Case 4
                Return EImageIconSchedule.Morning_Afternoon
            Case 5
                Return EImageIconSchedule.Afternoon_Night
            Case 6
                Return EImageIconSchedule.Morning_Night
        End Select
    End Function

    ''' <summary>
    ''' Metodo que devuelve la enumeracion del icono que se debe colocar cuando el día tenga un evento o una novedad
    ''' </summary>
    Public Function IconScheduleEventOrNovelty(type As Integer) As System.Drawing.Bitmap
        Select Case type
            Case 0 'Evento
                Return Presentation.Controls.My.Resources.permiso
            Case 1 'Novedad: Incapacidad
                Return Presentation.Controls.My.Resources.incapacidad
            Case 2 'Novedad: Permiso
                Return Presentation.Controls.My.Resources.permiso
            Case 3 'Novedad: Vacaciones
                Return Presentation.Controls.My.Resources.trabajador_en_vacaciones
            Case 4 'Novedad: Otro
                Return Presentation.Controls.My.Resources.sancion
        End Select
    End Function

    ''' <summary>
    ''' Metodo que devuelve la enumeracion del icono que se debe colocar en un determinado turno dependiendo el horario
    ''' </summary>
    Public Function LetterSchedule(schedule As Integer) As String
        Select Case schedule
            Case 1
                Return "M"
            Case 2
                Return "T"
            Case 3
                Return "N"
            Case 4
                Return "MT"
            Case 5
                Return "TN"
            Case 6
                Return "MN"
            Case 7
                Return "EVE"
            Case 8
                Return "NOV"
        End Select
    End Function

    ''' <summary>
    ''' Guarda los días con su respectiva configuración
    ''' </summary>
    Private Async Sub SaveAuthorizationSchedule(isCallSinceEventAndNovelty As Boolean, authorizationScheduleDetailHour As AuthorizationScheduleDetailHour)
        Dim errors = ValidateControlsForm(False)
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If isCallSinceEventAndNovelty = False Then 'Si este método es llamado desde agregar turno normal
            AssigningValues()
        Else 'Si este método es llamado desde agregar eventos o novedades
            AssigningValuesEventOrNovelty(authorizationScheduleDetailHour)
        End If

        Me.AsyncLoader(True)
        Try
            Using model As New MAuthorizationSchedule(Me.Tag.ToString())
                Dim result = Await model.SaveAuthorizationSchedule(AuthorizationSchedule, ListDays)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    LoadControls()
                    Me.AsyncLoader(False)
                Else
                    Me.AsyncLoader(False)
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
        If AuthorizationSchedule Is Nothing Then
            AuthorizationSchedule = New AuthorizationSchedule()
        End If

        AuthorizationSchedule.AuthorizationScheduleDetail.Clear()

        With AuthorizationSchedule
            .AuthorizationScheduleTemplateId = INDsleScheduleTemplate.EditValue
            .Month = INDCalendar.MonthControl
            .Year = INDCalendar.YearControl

            Dim itemChecked As CheckedListBoxItem = (From x In INDclbcUsers.Items Where x.CheckState = CheckState.Checked Select x).FirstOrDefault()
            .UserId = itemChecked.Value
            .UserCode = itemChecked.Description.Split(" - ")(0)
            .Status = True

            Dim sumNumberHour = (From t In AuthorizationScheduleTemplateXpo.AuthorizationScheduleTemplateScheduleXpo Select t.NumberHour).Sum()

            For Each itemDay In ListDays
                'Se busca el día para saber si ya esta guardado
                Dim dayXpo As AuthorizationScheduleDetailXpo = Nothing
                If AuthorizationScheduleXpo IsNot Nothing AndAlso AuthorizationScheduleXpo.AuthorizationScheduleDetailXpo IsNot Nothing AndAlso AuthorizationScheduleXpo.AuthorizationScheduleDetailXpo.Count > 0 Then
                    dayXpo = (From x In AuthorizationScheduleXpo.AuthorizationScheduleDetailXpo Where x.Day = itemDay).FirstOrDefault()
                End If

                Dim authorizationScheduleDetail As New AuthorizationScheduleDetail
                With authorizationScheduleDetail
                    'Si ya se guardó el día se asigna el id y el id de la cabecera
                    If dayXpo IsNot Nothing Then
                        .Id = dayXpo.Id
                        .AuthorizationScheduleId = dayXpo.AuthorizationScheduleId.Id
                    End If

                    .Schedule = AuthorizationScheduleTemplateXpo.Schedule
                    .Day = itemDay
                    .NumberHour = sumNumberHour
                    .Status = True
                End With

                For Each itemHour In AuthorizationScheduleTemplateXpo.AuthorizationScheduleTemplateScheduleXpo
                    Dim authorizationScheduleDetailHour As New AuthorizationScheduleDetailHour
                    With authorizationScheduleDetailHour
                        .NextDay = itemHour.NextDay
                        .InitialTime = itemHour.InitialTime
                        .EndingTime = itemHour.EndingTime
                        .NumberHour = itemHour.EndingTime.TotalHours - itemHour.InitialTime.TotalHours
                        .Type = 1
                        .Status = True
                    End With

                    authorizationScheduleDetail.AuthorizationScheduleDetailHour.Add(authorizationScheduleDetailHour)
                Next

                .AuthorizationScheduleDetail.Add(authorizationScheduleDetail)
            Next
        End With
    End Sub

    ''' <summary>
    ''' Asigna los valores cuando se va a guardar los eventos o novedades
    ''' </summary>
    Private Sub AssigningValuesEventOrNovelty(authorizationScheduleDetailHour As AuthorizationScheduleDetailHour)
        If AuthorizationSchedule Is Nothing Then
            AuthorizationSchedule = New AuthorizationSchedule()
        End If

        AuthorizationSchedule.AuthorizationScheduleDetail.Clear()

        With AuthorizationSchedule
            .AuthorizationScheduleTemplateId = INDsleScheduleTemplate.EditValue
            .Month = INDCalendar.MonthControl
            .Year = INDCalendar.YearControl

            Dim itemChecked As CheckedListBoxItem = (From x In INDclbcUsers.Items Where x.CheckState = CheckState.Checked Select x).FirstOrDefault()
            .UserId = itemChecked.Value
            .UserCode = itemChecked.Description.Split(" - ")(0)
            .Status = True

            For Each itemDay In ListDays

                'Se busca el día para saber si ya esta guardado
                Dim dayXpo As AuthorizationScheduleDetailXpo = Nothing
                If AuthorizationScheduleXpo IsNot Nothing AndAlso AuthorizationScheduleXpo.AuthorizationScheduleDetailXpo IsNot Nothing AndAlso AuthorizationScheduleXpo.AuthorizationScheduleDetailXpo.Count > 0 Then
                    dayXpo = (From x In AuthorizationScheduleXpo.AuthorizationScheduleDetailXpo Where x.Day = itemDay).FirstOrDefault()
                End If

                Dim authorizationScheduleDetail As New AuthorizationScheduleDetail
                With authorizationScheduleDetail
                    If dayXpo IsNot Nothing Then 'Si se encontro el día se asignan los valores de la entidad encontrada
                        .Id = dayXpo.Id
                        .AuthorizationScheduleId = dayXpo.AuthorizationScheduleId.Id
                        .Schedule = dayXpo.Schedule
                        .Day = itemDay
                        .NumberHour = 0
                        .Status = dayXpo.Status
                    Else 'Si no se encontro se crea una entidad de cero
                        .Schedule = AuthorizationScheduleTemplateXpo.Schedule
                        .Day = itemDay
                        .NumberHour = 0
                        .Status = True
                    End If
                End With

                'Se agrega la entidad que devuelve el modal de agregar eventos y novedades
                With authorizationScheduleDetailHour
                    If dayXpo IsNot Nothing Then 'Si se encontro el dia se asigna el id del dia
                        .AuthorizationScheduleDetailId = dayXpo.Id
                    End If
                End With

                authorizationScheduleDetail.AuthorizationScheduleDetailHour.Add(authorizationScheduleDetailHour.Clone())

                .AuthorizationScheduleDetail.Add(authorizationScheduleDetail)
            Next
        End With
    End Sub

    ''' <summary>
    ''' Valida los controles del form
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsForm(isCallSinceEventAndNovelty As Boolean) As String
        Dim errors As New StringBuilder

        If INDsleScheduleTemplate.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una plantilla")
        End If

        If INDclbcUsers.CheckedItems.Count = 0 Then
            errors.AppendLine("Debe seleccionar un usuario")
        End If

        If ListDays Is Nothing OrElse ListDays.Count = 0 Then
            errors.AppendLine("Debe seleccionar días para guardar")
        End If

        'Se valida si la plantilla seleccionada tiene horario asociado siempre y cuando esta función no sea llamada desde el form modal de eventos y novedades
        If AuthorizationScheduleTemplateXpo IsNot Nothing AndAlso isCallSinceEventAndNovelty = False Then
            If AuthorizationScheduleTemplateXpo.AuthorizationScheduleTemplateScheduleXpo Is Nothing OrElse AuthorizationScheduleTemplateXpo.AuthorizationScheduleTemplateScheduleXpo.Count = 0 Then
                errors.AppendLine("La plantilla de turno seleccionado no tiene horario definido")
            End If
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el día del calendario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OnClickDays(sender As Object, e As EventArgs)
        If ListDays Is Nothing Then
            ListDays = New List(Of Integer)
        End If

        Dim LcDays As DevExpress.XtraEditors.LabelControl = sender
        Dim _parent As DevExpress.XtraEditors.PanelControl = LcDays.Parent

        ''Si le dan click sobre un día que ya tenga icono(Eso quiere decir que ya se ha guardado el turno) no se pinta
        'Dim pictureEditItem As DevExpress.XtraEditors.PictureEdit = _parent.Controls.Item("INDPeFirst" & _parent.Tag)
        'If pictureEditItem IsNot Nothing AndAlso pictureEditItem.Visible = True Then
        '    Exit Sub
        'End If

        If _parent.Tag <> "" Then
            If LcDays.BackColor = Color.WhiteSmoke Then
                LcDays.BackColor = Color.LightGreen
                ListDays.Add(_parent.Tag)
            Else
                LcDays.BackColor = Color.WhiteSmoke
                ListDays.Remove(_parent.Tag)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Limpia los paneles de los días del calendario
    ''' </summary>
    Private Sub CleanDays()
        ListDays = Nothing
        AuthorizationSchedule = Nothing
        AuthorizationScheduleXpo = Nothing
        INDLcTotalHoursNumber1.Text = "0"

        Dim panelControlMain = INDCalendar.PanelControlMain
        Dim panelControlChilds = panelControlMain.Controls
        For i As Integer = 0 To panelControlChilds.Count - 1
            If panelControlChilds.Item(i).GetType.ToString = "DevExpress.XtraEditors.PanelControl" Then
                Dim item As DevExpress.XtraEditors.PanelControl = panelControlChilds.Item(i)
                If item.Tag <> "" Then
                    Dim LabelControlItem As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcDays" & item.Tag)
                    LabelControlItem.BackColor = Color.WhiteSmoke
                    Dim LcItemMore As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcMore" & item.Tag)
                    Dim PictureEditItem1 As DevExpress.XtraEditors.PictureEdit = item.Controls.Item("INDPeFirst" & item.Tag)
                    Dim PictureEditItem2 As DevExpress.XtraEditors.PictureEdit = item.Controls.Item("INDPeSecond" & item.Tag)
                    Dim LcItem1 As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcLetterFirst" & item.Tag)
                    Dim LcItem2 As DevExpress.XtraEditors.LabelControl = item.Controls.Item("INDLcLetterSecond" & item.Tag)
                    PictureEditItem1.Visible = False
                    If PictureEditItem1.Image IsNot Nothing Then
                        PictureEditItem1.Image = Nothing
                    End If
                    PictureEditItem1.Tag = ""
                    PictureEditItem2.Visible = False
                    If PictureEditItem2.Image IsNot Nothing Then
                        PictureEditItem2.Image = Nothing
                    End If
                    PictureEditItem2.Tag = ""
                    LcItem1.Tag = ""
                    LcItem1.Text = ""
                    LcItem1.Visible = False
                    LcItem2.Tag = ""
                    LcItem2.Text = ""
                    LcItem2.Visible = False
                    LcItemMore.Text = ""
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre agregar turno
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OnItemClickAddTurn(sender As Object, e As EventArgs)
        SaveAuthorizationSchedule(False, Nothing)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre agregar novedad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OnItemClickAddNovelty(sender As Object, e As EventArgs)
        OpenFormAddEventOrNovelty(False)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre agregar evento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OnItemClickAddEvent(sender As Object, e As EventArgs)
        OpenFormAddEventOrNovelty(True)
    End Sub

    ''' <summary>
    ''' Método que agrega un evento o una novedad al día seleccionado
    ''' </summary>
    Private Sub OpenFormAddEventOrNovelty(isEvent As Boolean)
        Dim errors = ValidateControlsForm(True)
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        Using formulario As New FrmEventAndNovelty()
            AddHandler formulario.AddArgs, AddressOf AddArgs
            formulario.ToolBar.Visible = False
            formulario.ViewModeEditHold = True
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 400
            formulario.IsEvent = isEvent
            formulario.TextForm = If(isEvent, "Registro Evento", "Registro Novedad")
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Se ejecuta al aceptar el evento o la novedad despues de abrir el form modal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AddArgs(sender As Object, e As AddEventsAndNoveltiesEventArgs)
        SaveAuthorizationSchedule(True, e.AuthorizationScheduleDetailHour)
    End Sub

    ''' <summary>
    ''' Se dispara al cambiar el valor del control de mes y año
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OnChangeDate(sender As Object, e As EventArgs)
        LoadControls()
    End Sub

    ''' <summary>
    ''' Carga la información de la plantilla al seleccionarla en el search
    ''' </summary>
    Private Sub LoadInfoScheduleTemplate()
        If INDsleScheduleTemplate.EditValue Is Nothing Then
            Exit Sub
        End If

        CleanDays()

        AuthorizationScheduleTemplateXpo = Presenter.GetAuthorizationShceduleTemplateById(INDsleScheduleTemplate.EditValue)

        INDclbcUsers.Items.Clear()
        Dim listUsers = Presenter.ListUsersForSchedule(INDsleScheduleTemplate.EditValue)
        If listUsers IsNot Nothing AndAlso listUsers.Count > 0 Then
            For Each item In listUsers
                INDclbcUsers.Items.Add(item.UserId, item.UserCodeName)
            Next
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAuthorizationSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()
        Presenter = New PAuthorizationSchedule()

        INDCalendar.YearControl = GetDateServer().Year
        INDCalendar.MonthControl = GetDateServer().Month

        AddHandler INDCalendar.OnClickDays, AddressOf OnClickDays
        AddHandler INDCalendar.OnItemClickAddTurn, AddressOf OnItemClickAddTurn
        AddHandler INDCalendar.OnItemClickAddEvent, AddressOf OnItemClickAddEvent
        AddHandler INDCalendar.OnItemClickAddNovelty, AddressOf OnItemClickAddNovelty
        AddHandler INDCalendar.OnChangeDate, AddressOf OnChangeDate
        AddHandler INDCalendar.OnMouseHoverDays, AddressOf OnMouseHoverDays
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Se dispara cuando se despliega el control de plantilla de turno
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleScheduleTemplate_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleScheduleTemplate.QueryPopUp
        If INDsleScheduleTemplate.Properties.DataSource Is Nothing Then
            INDsleScheduleTemplate.Properties.DataSource = Presenter.InitializeScheduleTemplate()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Se dispara al cambiar el valor del control de plantilla de turno
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleScheduleTemplate_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleScheduleTemplate.EditValueChanged
        If INDsleScheduleTemplate.EditValue IsNot Nothing Then
            AsyncLoader(True)
            LoadInfoScheduleTemplate()
            AsyncLoader(False)
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAuthorizationSchedule_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleScheduleTemplate.Properties.PopupFormMinSize = New System.Drawing.Size(INDsleScheduleTemplate.Size.Width - 11, 0)
        INDsleScheduleTemplate.Properties.PopupFormSize = New System.Drawing.Size(INDsleScheduleTemplate.Size.Width - 11, 0)
        INDsleScheduleTemplate.Focus()
    End Sub

#End Region

#Region "ItemCheck"

    ''' <summary>
    ''' Se dispara al checkiar un usuario, solo se permite checkiar uno
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDclbcUsers_ItemCheck(sender As Object, e As DevExpress.XtraEditors.Controls.ItemCheckEventArgs) Handles INDclbcUsers.ItemCheck
        If e.State = CheckState.Checked Then
            For i = 0 To INDclbcUsers.Items.Count - 1
                If i <> e.Index Then
                    INDclbcUsers.SetItemChecked(i, False)
                End If
            Next

            LoadControls()
        ElseIf INDclbcUsers.CheckedItemsCount = 0 Then
            CleanDays()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de editar del detalle de cada día
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPopupSBEdit_Click(sender As Object, e As EventArgs) Handles INDPopupSBEdit.Click
        OpenFormDetail()
    End Sub

#End Region

#Region "ItemClick"

    ''' <summary>
    ''' Evento que se dispara al presionar refrescar del menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBarRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarRefresh.ItemClick
        AsyncLoader(True)
        Try
            'Se obtiene el item checkiado si lo hay
            Dim itemChecked As CheckedListBoxItem = (From x In INDclbcUsers.Items Where x.CheckState = CheckState.Checked Select x).FirstOrDefault()

            'Se carga la información de la plantilla de turnos
            LoadInfoScheduleTemplate()

            'Si habia un item checkiado antes de realizar el refresh y si tiene datos el control de usuarios
            If itemChecked IsNot Nothing AndAlso INDclbcUsers.Items.Count > 0 Then
                'Se obtiene con el id del usuario el item en el control
                Dim info = (From x In INDclbcUsers.Items Where x.Value = itemChecked.Value Select x).FirstOrDefault()

                'Si se encontró el usuario checkiado anteriormente en los nuevos datos del control
                If info IsNot Nothing Then
                    Dim indexOf = INDclbcUsers.Items.IndexOf(info)
                    INDclbcUsers.SetItemChecked(indexOf, True)
                End If
            End If

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#End Region

End Class