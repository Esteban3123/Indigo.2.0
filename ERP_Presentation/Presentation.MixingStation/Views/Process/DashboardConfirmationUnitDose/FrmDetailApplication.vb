'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Duván Albeiro Mejia Cortes
' Created          : 2021-12-20
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Collections.Concurrent
Imports System.Dynamic
Imports System.Threading
Imports System.Windows.Forms
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmDetailApplication

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

#Region "Events"
    ''' <summary>
    ''' Evento para cargar nuevamente la rejilla principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ReloadPrincipalGridArgs(sender As Object, e As EventArgs)

#Region "Event"

    ''' <summary>
    ''' Evento para cargar nuevamente la campaña
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ReloadCampaignArgs(sender As Object, e As EventArgs)

#End Region

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

    ''' <summary>
    ''' Establece el texto de la ventana
    ''' </summary>
    Public WriteOnly Property SetTitleWindow() As String
        Set(value As String)
            Me.Text = value
        End Set
    End Property
#End Region

#Region "Methods"

    ''' <summary>
    ''' Consulto el detalle de la Solicitud
    ''' </summary>
    Private Sub GetApplicationDetail()
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
#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDetailPatients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PDashboardConfirmationUnitDose
        dictionaryPatientSchedules = New Dictionary(Of String, List(Of HCHOJAMEDXpo))
        GetApplicationDetail()
    End Sub
#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento para cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDetailPatients_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub
#End Region

#Region "QueryPopup"

    ''' <summary>
    '''  Evento que se dispara al desplegar el control de horarios de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepPopupSchedule_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDrepPopupSchedule.QueryPopUp
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

End Class