'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/06/2020
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

Public Class FrmAddEvent

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PDashboardAuthorization

    ''' <summary>
    ''' Item de la rejilla de la solicitud
    ''' </summary>
    Public ViewListRequestsXpo As ViewListRequestsXpo

    ''' <summary>
    ''' Permite saber si el evento que se va a crear se asocia aun anexo
    ''' </summary>
    Public TraceabilityPaperworkAnnexesId As Integer?

    ''' <summary>
    ''' Entidad que representa a los tramites
    ''' </summary>
    Dim TraceabilityPaperwork As TraceabilityPaperwork

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

#Region "Event"

    ''' <summary>
    ''' Evento que se ejecuta cuando se guarda un evento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event ReturnModalArgs(sender As Object, e As EventArgs)

#End Region

#Region "Methods"

    ''' <summary>
    ''' Valida los controles del form
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsForm() As String
        Dim errors As New StringBuilder

        If INDsleHealthAdministrator.EditValue Is Nothing Then
            errors.AppendLine("Seleccione una entidad")
        End If

        If INDsleReportType.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un tipo de reporte")
        End If

        If INDsleStatus.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un estado")
        End If

        If INDlyItemAuthorizationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If String.IsNullOrEmpty(INDtxtAuthorizationNumber.EditValue) Then
                errors.AppendLine("Ingrese un no. de autorización")
            End If

            If INDtxtAuthorizationNumber.Text.Trim().Length < 4 Then
                errors.AppendLine("El numero de autorización debe tener mínimo 4 dígitos")
            End If

            If INDseAuthorizedQuantity.EditValue Is Nothing OrElse INDseAuthorizedQuantity.EditValue = 0 Then
                errors.AppendLine("Ingrese una cantidad autorizada")
            End If

            If INDdteAuthorizationDate.EditValue Is Nothing Then
                errors.AppendLine("Ingrese una fecha de autorización")
            End If

            If INDdteAuthorizationExpiredDate.EditValue Is Nothing Then
                errors.AppendLine("Ingrese una fecha de vencimiento de autorización")
            End If
        End If

        If INDlyItemAuthorizedBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If String.IsNullOrEmpty(INDtxtAuthorizedBy.EditValue) Then
                errors.AppendLine("Ingrese autorizado por")
            End If
        End If

        If INDlygCall.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If String.IsNullOrEmpty(INDtxtPhoneNumber.EditValue) Then
                errors.AppendLine("Ingrese número telefónico")
            End If

            If String.IsNullOrEmpty(INDtxtExtension.EditValue) Then
                errors.AppendLine("Ingrese una extensión")
            End If

            If INDteInitialTime.EditValue Is Nothing Then
                errors.AppendLine("Ingrese una hora inicial")
            End If

            If INDteEndTime.EditValue Is Nothing Then
                errors.AppendLine("Ingrese una hora final")
            End If

            If String.IsNullOrEmpty(INDtxtContactPerson.EditValue) Then
                errors.AppendLine("Ingrese una persona contacto")
            End If

            If String.IsNullOrEmpty(INDtxtCharge.EditValue) Then
                errors.AppendLine("Ingrese un cargo")
            End If
        End If

        If INDlygPhysicalSend.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If String.IsNullOrEmpty(INDtxtRadicateNumber.EditValue) Then
                errors.AppendLine("Ingrese un no. radicado")
            End If

            If INDsleSendType.EditValue Is Nothing Then
                errors.AppendLine("Seleccione un tipo de envío")
            End If

            If INDdteReceivedDate.EditValue Is Nothing Then
                errors.AppendLine("Ingrese una fecha recibido")
            End If

            If String.IsNullOrEmpty(INDtxtReceivePerson.EditValue) Then
                errors.AppendLine("Ingrese una persona recibe")
            End If

            If String.IsNullOrEmpty(INDtxtCharge2.EditValue) Then
                errors.AppendLine("Ingrese un cargo")
            End If
        End If

        If INDlygWebPage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If String.IsNullOrEmpty(INDtxtURL.EditValue) Then
                errors.AppendLine("Ingrese una URL")
            End If

            If INDdteRegistrationDate.EditValue Is Nothing Then
                errors.AppendLine("Ingrese una fecha registro")
            End If

            If String.IsNullOrEmpty(INDtxtRadicateNumberWebPage.EditValue) Then
                errors.AppendLine("Ingrese un no. radicado")
            End If
        End If

        If INDlygEmail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If String.IsNullOrEmpty(INDtxtEmail.EditValue) Then
                errors.AppendLine("Ingrese un email")
            End If

            If INDdteSendDate.EditValue Is Nothing Then
                errors.AppendLine("Ingrese una fecha envío")
            End If
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Guarda un evento
    ''' </summary>
    Private Async Sub Guardar()
        Dim errors = ValidateControlsForm()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        AssigningValues()

        Me.AsyncLoader(True)
        Try
            Using model As New MDashboardAuthorization(Me.Tag.ToString())
                Dim result = Await model.SaveTraceabilityPaperwork({TraceabilityPaperwork}.ToList())
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
    ''' Asigna los valores a la entidad principal
    ''' </summary>
    Private Sub AssigningValues()
        TraceabilityPaperwork = New TraceabilityPaperwork

        With TraceabilityPaperwork
            If ViewListRequestsXpo.TraceabilityPaperworkId <> Nothing AndAlso ViewListRequestsXpo.TraceabilityPaperworkId > 0 Then 'Si ya existe un registro se asigna el id
                .Id = ViewListRequestsXpo.TraceabilityPaperworkId
            End If

            .AdmissionNumber = ViewListRequestsXpo.AdmissionNumber
            .Folio = ViewListRequestsXpo.Folio
            .ServiceCode = ViewListRequestsXpo.ItemCodeOriginal
            .Type = ViewListRequestsXpo.Type
            .PatientCode = ViewListRequestsXpo.PatientCode
            .CareCenterCode = ViewListRequestsXpo.CareCenterCode
            .RequestDate = ViewListRequestsXpo.RequestDate
            .RequestQuantity = ViewListRequestsXpo.Quantity
            .FunctionalUnitCode = ViewListRequestsXpo.FunctionalUnitCode
            .EntityId = ViewListRequestsXpo.EntityId
            .EntityName = ViewListRequestsXpo.EntityName
            .AssignUserCode = If(ViewListRequestsXpo.AssignUser IsNot Nothing, ViewListRequestsXpo.AssignUser.Split(" - ")(0).ToString(), Nothing)
            .IsManual = ViewListRequestsXpo.IsManual
            .CareGroupId = ViewListRequestsXpo.CareGroupId
            .HealthAdministratorId = ViewListRequestsXpo.HealthAdministratorId
            .AuthorizationSourceId = Nothing
            If ViewListRequestsXpo.AuthorizationSourceId <> Nothing AndAlso ViewListRequestsXpo.AuthorizationSourceId > 0 Then
                .AuthorizationSourceId = ViewListRequestsXpo.AuthorizationSourceId
            End If
            .ProfessionalCode = ViewListRequestsXpo.ProfessionalCode
            .ServiceId = ViewListRequestsXpo.ServiceId
            .ContractDescriptionId = ViewListRequestsXpo.ContractDescriptionId
            .PreviousStatus = If(ViewListRequestsXpo.TraceabilityPaperworkStatus = 0, 1, ViewListRequestsXpo.TraceabilityPaperworkStatus)

            'Se asigna el status de la cabecera dependiendo del status del evento
            If INDsleStatus.EditValue = 1 OrElse INDsleStatus.EditValue = 4 Then 'Pendiente por autorizar o Autorizado con aval
                If ViewListRequestsXpo.TraceabilityPaperworkStatus = 12 OrElse ViewListRequestsXpo.TraceabilityPaperworkStatus = 13 Then
                    .Status = 13
                Else
                    .Status = 3
                End If
            ElseIf INDsleStatus.EditValue = 2 Then 'Autorizado
                If ViewListRequestsXpo.TraceabilityPaperworkStatus = 12 OrElse ViewListRequestsXpo.TraceabilityPaperworkStatus = 13 Then
                    .Status = 15
                Else
                    .Status = 5
                End If
            ElseIf INDsleStatus.EditValue = 3 Then 'No autorizado
                .Status = 4
            End If

            Dim TraceabilityPaperworkEvents As New TraceabilityPaperworkEvents
            With TraceabilityPaperworkEvents
                If TraceabilityPaperworkAnnexesId IsNot Nothing AndAlso TraceabilityPaperworkAnnexesId > 0 Then 'Si el evento esta asociado a un anexo
                    .TraceabilityPaperworkAnnexesId = TraceabilityPaperworkAnnexesId
                End If

                .HealthAdministratorId = INDsleHealthAdministrator.EditValue
                .ReportType = INDsleReportType.EditValue
                .Instructions = INDmemoInstructions.EditValue
                .Status = INDsleStatus.EditValue

                .AuthorizationNumber = Nothing
                .AuthorizedQuantity = Nothing
                .AuthorizationDate = Nothing
                .AuthorizationExpiredDate = Nothing
                If INDlyItemAuthorizationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .AuthorizationNumber = INDtxtAuthorizationNumber.EditValue
                    .AuthorizedQuantity = CInt(INDseAuthorizedQuantity.EditValue)
                    .AuthorizationDate = INDdteAuthorizationDate.EditValue
                    .AuthorizationExpiredDate = INDdteAuthorizationExpiredDate.EditValue
                End If

                .AuthorizedBy = Nothing
                If INDlyItemAuthorizedBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .AuthorizedBy = INDtxtAuthorizedBy.EditValue
                End If

                .Observations = INDmemoObservations.EditValue
                .PatientNotificated = INDslePatientNotificated.EditValue
                .InformationPatient = INDmemoInformationPatient.EditValue

                .PhoneNumber = Nothing
                .Extension = Nothing
                .InitialTime = Nothing
                .EndTime = Nothing
                .ContactPerson = Nothing
                .Charge = Nothing
                If INDlygCall.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .PhoneNumber = INDtxtPhoneNumber.EditValue
                    .Extension = INDtxtExtension.EditValue
                    .InitialTime = If(TypeOf INDteInitialTime.EditValue Is TimeSpan, INDteInitialTime.EditValue, CDate(INDteInitialTime.EditValue).TimeOfDay)
                    .EndTime = If(TypeOf INDteEndTime.EditValue Is TimeSpan, INDteEndTime.EditValue, CDate(INDteEndTime.EditValue).TimeOfDay)
                    .ContactPerson = INDtxtContactPerson.EditValue
                    .Charge = INDtxtCharge.EditValue
                End If

                .RadicateNumber = Nothing
                .SendType = Nothing
                .ReceivedDate = Nothing
                .ReceivePerson = Nothing
                .Charge = Nothing
                If INDlygPhysicalSend.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .RadicateNumber = INDtxtRadicateNumber.EditValue
                    .SendType = CInt(INDsleSendType.EditValue)
                    .ReceivedDate = INDdteReceivedDate.EditValue
                    .ReceivePerson = INDtxtReceivePerson.EditValue
                    .Charge = INDtxtCharge2.EditValue
                End If

                .URL = Nothing
                .RegistrationDate = Nothing
                If INDlygWebPage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .URL = INDtxtURL.EditValue
                    .RegistrationDate = INDdteRegistrationDate.EditValue
                    .RadicateNumber = INDtxtRadicateNumberWebPage.EditValue
                End If

                .Email = Nothing
                .SendDate = Nothing
                If INDlygEmail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .Email = INDtxtEmail.EditValue
                    .SendDate = INDdteSendDate.EditValue
                End If

                'Si hay documentos asociados
                If Me.BarraBotones._listDocuments IsNot Nothing AndAlso Me.BarraBotones._listDocuments.Count > 0 Then
                    .ListAttachment = New List(Of Attachment)
                    For Each item In Me.BarraBotones._listDocuments
                        Dim attachment As New Attachment
                        With attachment
                            .Name = item.Name
                            .Extension = item.Type
                            .Description = item.MetaData
                            .FileAttached = item.Content
                        End With
                        .ListAttachment.Add(attachment)
                    Next
                End If
            End With

            .TraceabilityPaperworkEvents.Add(TraceabilityPaperworkEvents)
        End With
    End Sub

    ''' <summary>
    ''' Elimina un evento
    ''' </summary>
    Private Sub Eliminar()

    End Sub

    ''' <summary>
    ''' Inicializa los search que son con datos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim listReportType As New List(Of Tuple(Of Integer, String))
        listReportType.Add(New Tuple(Of Integer, String)(1, "Llamada Telefónica"))
        listReportType.Add(New Tuple(Of Integer, String)(2, "Envío Físico"))
        listReportType.Add(New Tuple(Of Integer, String)(3, "Registro Página Web"))
        listReportType.Add(New Tuple(Of Integer, String)(4, "Correo Electrónico"))
        listReportType.Add(New Tuple(Of Integer, String)(5, "Tramita Paciente"))
        INDsleReportType.Properties.DataSource = listReportType.ToList()

        Dim listStatus As New List(Of Tuple(Of Integer, String))
        listStatus.Add(New Tuple(Of Integer, String)(1, "Pendiente por Autorizar"))
        listStatus.Add(New Tuple(Of Integer, String)(2, "Autorizado"))
        listStatus.Add(New Tuple(Of Integer, String)(3, "No Autorizado"))
        listStatus.Add(New Tuple(Of Integer, String)(4, "Autorizado con Aval Interno"))
        INDsleStatus.Properties.DataSource = listStatus.ToList()

        Dim listSendType As New List(Of Tuple(Of Integer, String))
        listSendType.Add(New Tuple(Of Integer, String)(1, "Correo Certificado"))
        listSendType.Add(New Tuple(Of Integer, String)(2, "Mensajería"))
        INDsleSendType.Properties.DataSource = listSendType.ToList()

        Dim listYesNot As New List(Of Tuple(Of Boolean, String))
        listYesNot.Add(New Tuple(Of Boolean, String)(True, "Si"))
        listYesNot.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDslePatientNotificated.Properties.DataSource = listYesNot.ToList()
        INDslePatientNotificated.EditValue = False
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddEvent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeTuples()
        Presenter = New PDashboardAuthorization()

        INDsleHealthAdministrator.EditValue = ViewListRequestsXpo.HealthAdministratorId
        INDsleHealthAdministrator.Properties.NullText = ViewListRequestsXpo.HealthAdministratorCodeName
        INDseRequestQuantity.EditValue = ViewListRequestsXpo.Quantity
        INDtxtPatientDescription.EditValue = ViewListRequestsXpo.PatientCode + " - " + ViewListRequestsXpo.PatientName
        INDtxtPatientAddress.EditValue = ViewListRequestsXpo.PatientAddress
        INDtxtPatientPhone.EditValue = ViewListRequestsXpo.PatientPhone
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddEvent_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleHealthAdministrator_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles INDsleHealthAdministrator.ButtonClick
        If e.Button.Kind = ButtonPredefines.Plus Then
            OpenForm(972, Nothing, True)
            INDsleHealthAdministrator.Properties.DataSource = Presenter.InitializeHealthAdministrator()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleHealthAdministrator_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHealthAdministrator.QueryPopUp
        If INDsleHealthAdministrator.Properties.DataSource Is Nothing Then
            INDsleHealthAdministrator.Properties.DataSource = Presenter.InitializeHealthAdministrator()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddEvent_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleReportType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleReportType.EditValueChanged
        If INDsleReportType.EditValue IsNot Nothing Then
            Select Case INDsleReportType.EditValue
                Case 1 'Llamada telefonica
                    INDlygCall.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlygPhysicalSend.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlygWebPage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlygEmail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 2 'Envio fisico
                    INDlygPhysicalSend.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlygCall.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlygWebPage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlygEmail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 3 'Registro pagina web
                    INDlygWebPage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlygCall.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlygPhysicalSend.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlygEmail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 4 'Correo electronico
                    INDlygEmail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlygCall.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlygPhysicalSend.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlygWebPage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 5 'Tramita paciente
                    INDlygEmail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlygCall.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlygPhysicalSend.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlygWebPage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleStatus_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleStatus.EditValueChanged
        If INDsleStatus.EditValue IsNot Nothing Then
            If INDsleStatus.EditValue = 2 Then 'Autorizado
                INDlyItemAuthorizationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemAuthorizedQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemAuthorizationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemAuthorizationExpiredDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemAuthorizedBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf INDsleStatus.EditValue = 4 Then 'Autorizado con aval
                INDlyItemAuthorizationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemAuthorizedQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemAuthorizationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemAuthorizationExpiredDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemAuthorizedBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else 'No autorizado o pendiente por autorizar
                INDlyItemAuthorizationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemAuthorizedQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemAuthorizationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemAuthorizationExpiredDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemAuthorizedBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de cantidad solicitada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDseRequestQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDseRequestQuantity.EditValueChanged
        If INDseRequestQuantity.EditValue IsNot Nothing Then
            INDseAuthorizedQuantity.Properties.MaxValue = INDseRequestQuantity.EditValue
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
        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveWithoutUndoAndFind)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = False
        Me.BarraBotones.SetEnableButtonAttach = True
        Me.BarraBotones.SetEnableButtonDigitalization = False
        Me.BarraBotones.SetEnableButtonMoreAttach = True
        Me.BarraBotones.SetFormId = CInt(Me.Tag)
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

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub



#End Region

End Class