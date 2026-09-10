'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/03/2021
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

Public Class FrmViewDetails

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PDashboardProductionSchedule
    ''' <summary>
    ''' Id de la campaña
    ''' </summary>
    Public campaignDetailId As Integer
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
    ''' Establece el texto de la ventan
    ''' </summary>
    Public WriteOnly Property SetTitleWindow() As String
        Set(value As String)
            Me.Text = $"   {value}"
        End Set
    End Property
#End Region

#Region "Methods"

    ''' <summary>
    ''' Indica si se muestra el progressbar
    ''' </summary>
    ''' <param name="isAsyncOperation">Valor que indica si se esta realizando una operación asíncrona</param>
    Private Sub IsAsyncOperation(Optional ByVal isAsyncOperation As Boolean = True)
        If isAsyncOperation Then
            INDlygDetails.Enabled = False
            INDlyItemProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlygDetails.Enabled = True
            INDlyItemProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
    ''' <summary>
    ''' Propiedad para la barra de cargue.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsBusy As Boolean
        Get
            Return If(Me.INDlyItemProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always, True, False)
        End Get
        Set(value As Boolean)
            Me.INDlyItemProgress.Visibility = If(value = True, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        End Set
    End Property

    ''' <summary>
    ''' Consulta el paquete
    ''' </summary>
    Private Async Sub GetDetails()
        Try
            Using model As New MDashboardProductionSchedule(Me.Tag)
                IsBusy = True
                Dim result = Await model.GetItemsByCampaigns(campaignDetailId)
                If result IsNot Nothing Then
                    INDgcDetails.DataSource = result.Data.ToList()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros para poder continuar"
                End If
                If model Is Nothing Then
                    IsBusy = False
                    Exit Sub
                End If
                IsBusy = False
            End Using
        Catch ex As Exception
            IsBusy = False
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
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
        Presenter = New PDashboardProductionSchedule()
        GetDetails()
    End Sub

#End Region


#Region "KeyDown"

    ''' <summary>
    ''' Evento que se ejecuta al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDetailPatients_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

End Class