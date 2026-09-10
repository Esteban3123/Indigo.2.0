'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/01/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Dynamic
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmApplicationDetail

#Region "Variables"

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PDashboardConfirmationUnitDose

    ''' <summary>
    ''' Item seleccionado en la rejilla
    ''' </summary>
    Public ViewListDashboardConfirmationUnitDoseXpo As ViewListDashboardConfirmationUnitDoseXpo

    ''' <summary>
    ''' Diccionario para almacenar los horarios del paciente
    ''' </summary>
    Private dictionaryPatientSchedules As Dictionary(Of String, List(Of HCHOJAMEDXpo))

#End Region

#Region "Event"

    ''' <summary>
    ''' Evento para cargar nuevamente la rejilla principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ReloadPrincipalGridArgs(sender As Object, e As EventArgs)

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
    ''' Método que asigna la columna de acciones a las rejillas
    ''' </summary>
    Private Sub SetActionsColumns()
        BarraBotones.ActualizarPermisosBarra(Me.Tag)
        Dim ListActions As New List(Of eAcciones)

        If BarraBotones.PermissionsForm.ContainsKey(122) Then 'si tiene permiso de procesar central de mezclas
            ListActions.Add(eAcciones.ProcessMixingStation)
        End If

        If BarraBotones.PermissionsForm.ContainsKey(123) Then 'si tiene permiso de enviar farmacia
            ListActions.Add(eAcciones.SendPharmacy)
        End If

        If ListActions.Count > 0 Then 'si hay menu se asigna al gridview 
            IndigoGridView1.SetListAcction(INDviewApplicationDetail, ListActions)

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewApplicationDetail.Columns
                If col.Name = "colactions" Then
                    col.Width = 100
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Consulta el paquete
    ''' </summary>
    Private Sub GetApplicationDetail()
        ' TODO: StringIds
        INDgcApplicationDetail.DataSource = Nothing
        INDviewApplicationDetail.ShowLoadingPanel()
        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Try
                                      Dim result = Presenter.ListApplicationDetail(ViewListDashboardConfirmationUnitDoseXpo.AGRUPAQUETE, ViewListDashboardConfirmationUnitDoseXpo.SourceType)
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcApplicationDetail.SafeInvoke(Sub()
                                                                                INDviewApplicationDetail.HideLoadingPanel()
                                                                                INDgcApplicationDetail.DataSource = result
                                                                            End Sub)
                                      End If
                                  Catch ex As Exception
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcApplicationDetail.SafeInvoke(Sub()
                                                                                INDviewApplicationDetail.HideLoadingPanel()
                                                                                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                            End Sub)
                                      End If
                                  End Try
                              End Sub, tokenAsync.Token)
    End Sub

    ''' <summary>
    ''' Método que guarda un paquete
    ''' </summary>
    Public Async Sub Guardar(Optional sendTo As Integer? = Nothing)
        If CType(INDgcApplicationDetail.DataSource, List(Of ViewListApplicationDetailXpo)) Is Nothing OrElse CType(INDgcApplicationDetail.DataSource, List(Of ViewListApplicationDetailXpo)).Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay detalles para guardar"
            Exit Sub
        End If
        Dim args = AssigningValues(sendTo)
        Me.AsyncLoader(True)
        Try
            Using model As New MDashboardConfirmationUnitDose(Me.Tag.ToString())
                Dim result = Await model.UpdateMedicalOrderCM(args)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    If sendTo IsNot Nothing Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                        RaiseEvent ReloadPrincipalGridArgs(Nothing, Nothing)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = "Se actualizó correctamente las observaciones"
                    End If
                    GetApplicationDetail()
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
    Private Function AssigningValues(sendTo As Integer?) As Object
        Dim args As Object = New ExpandoObject()
        Dim myListDetail As New ConcurrentBag(Of Object)()
        Dim listItems As List(Of ViewListApplicationDetailXpo) = Nothing

        If sendTo Is Nothing Then 'Si solo se va a guardar las observaciones
            listItems = CType(INDgcApplicationDetail.DataSource, List(Of ViewListApplicationDetailXpo))
        ElseIf sendTo = 1 OrElse sendTo = 2 Then 'Si viene de la accion procesar central de mezclas o enviar a farmacia
            listItems = (From x In INDviewApplicationDetail.GetSelectedRows() Select DirectCast(INDviewApplicationDetail.GetRow(x), ViewListApplicationDetailXpo)).ToList()
        End If

        For Each item In listItems
            Dim detail As Object = New ExpandoObject()
            detail.Id = item.Id
            detail.Observations = item.Observations
            detail.SendTo = sendTo
            detail.SourceType = item.SourceType
            detail.ConfirmationUnitDoseId = ViewListDashboardConfirmationUnitDoseXpo.ConfirmationUnitDoseId
            myListDetail.Add(detail)
        Next

        args.CMConfigurationId = ViewListDashboardConfirmationUnitDoseXpo.CMConfigurationId
        args.SendOneToOne = myListDetail
        Return args
    End Function

    ''' <summary>
    ''' Envia a farmacia
    ''' </summary>
    Private Sub SendPharmacy()
        Dim listItems = (From x In INDviewApplicationDetail.GetSelectedRows() Select DirectCast(INDviewApplicationDetail.GetRow(x), ViewListApplicationDetailXpo)).ToList()
        Dim listCodes = (From x In listItems Select x.PatientCode).ToList()
        Dim messagesCodes = String.Join(",", listCodes.ToArray())
        If MessageIndigo.Show("Desea enviar los pacientes " + messagesCodes + " a dashboard farmacia?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If ViewListDashboardConfirmationUnitDoseXpo.PackageId <> Nothing AndAlso ViewListDashboardConfirmationUnitDoseXpo.PackageId > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede enviar a farmacia porque el registro principal tiene asignado un paquete"
            Exit Sub
        End If

        Guardar(2)
    End Sub

    ''' <summary>
    ''' Envia a central de mezclas
    ''' </summary>
    Private Sub ProcessMixingStation()
        Dim listItems = (From x In INDviewApplicationDetail.GetSelectedRows() Select DirectCast(INDviewApplicationDetail.GetRow(x), ViewListApplicationDetailXpo)).ToList()
        Dim listCodes = (From x In listItems Select x.PatientCode).ToList()
        Dim messagesCodes = String.Join(",", listCodes.ToArray())
        If MessageIndigo.Show("Desea enviar los pacientes " + messagesCodes + " a procesar a central de mezclas?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If ViewListDashboardConfirmationUnitDoseXpo.PackageId = Nothing OrElse ViewListDashboardConfirmationUnitDoseXpo.PackageId = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede enviar a procesar central de mezclas porque el registro principal no tiene asignado un paquete"
            Exit Sub
        End If

        Guardar(1)
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmApplicationDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me.indigo = SessionValues.Instance
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

        Presenter = New PDashboardConfirmationUnitDose
        dictionaryPatientSchedules = New Dictionary(Of String, List(Of HCHOJAMEDXpo))
        'SetActionsColumns()
        GetApplicationDetail()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento para cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmApplicationDetail_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub

#End Region

#Region "KeyDown"

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se ejecuta al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmApplicationDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Se ejecuta al cerrar el popup de observaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepMemoObservations_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDrepMemoObservations.CloseUp
        Dim info = CType(INDviewApplicationDetail.GetFocusedRow(), ViewListApplicationDetailXpo)
        Dim control = CType(sender, MemoExEdit)
        info.Observations = e.Value
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim tagGrid As String = ""
        If sender.GetType() Is GetType(DevExpress.XtraEditors.SimpleButton) Or sender.GetType() Is GetType(DevExpress.XtraBars.BarButtonItem) Then
            tagGrid = sender.Tag.ToString()
        Else
            tagGrid = sender.Text
        End If
        Select Case tagGrid
            Case "ProcessMixingStation", "Procesar Central Mezclas"
                ProcessMixingStation()
            Case "SendPharmacy", "Enviar Farmacia"
                SendPharmacy()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "ProcessMixingStation", "Procesar Central Mezclas"
                ProcessMixingStation()
            Case "SendPharmacy", "Enviar Farmacia"
                SendPharmacy()
        End Select
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de horarios de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepPopupSchedule_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPopupSchedule.QueryPopUp
        Dim info = CType(INDviewApplicationDetail.GetFocusedRow(), ViewListApplicationDetailXpo)
        Dim listSchedules As List(Of HCHOJAMEDXpo) = Nothing

        If info IsNot Nothing Then
            If Not dictionaryPatientSchedules.ContainsKey(info.PatientCode) Then
                listSchedules = Presenter.GetSchedules(ViewListDashboardConfirmationUnitDoseXpo.CareCenterCode, ViewListDashboardConfirmationUnitDoseXpo.ServiceCode, ViewListDashboardConfirmationUnitDoseXpo.Dosage, info.PatientCode)
                dictionaryPatientSchedules.Add(info.PatientCode, listSchedules)
            Else
                listSchedules = dictionaryPatientSchedules(info.PatientCode)
            End If
        End If

        INDgcSchedule.DataSource = Nothing
        INDgcSchedule.DataSource = listSchedules
    End Sub

#End Region

#End Region

#Region "BarButton Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

#End Region

End Class