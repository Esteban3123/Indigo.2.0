'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : 
' Created          : 13/09/2021
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

Public Class FrmKardexCampaign

#Region "Variables"

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PCampaigns

    ''' <summary>
    ''' Obtiene los permisos del usuario para el formulario principal
    ''' </summary>
    Public PermissionsForm As Dictionary(Of Integer, String)

    ''' <summary>
    ''' String de ids que representan a los detalles agrupados, son los ids de los pacientes
    ''' </summary>
    Public StringIds As String

    ''' <summary>
    ''' almacena el Id de detalle de la campaña
    ''' </summary>
    Public CampaignDetailId As Integer

    ''' <summary>
    ''' Variable que almacena el estado de la campaña
    ''' </summary>
    Public CampaignStatus As Byte?
#End Region

#Region "Events"

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
    ''' Establece el texto de la ventan
    ''' </summary>
    Public WriteOnly Property SetTitleWindow() As String
        Set(value As String)
            Me.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Establce el texto del grupo
    ''' </summary>
    Public WriteOnly Property SetLabelGroup() As String
        Set(value As String)
            INDlygDetails.Text = value
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
    ''' Consulta el kardex
    ''' </summary>
    Private Sub GetDetail()
        INDgcDetails1.DataSource = Nothing
        INDviewMaster.ShowLoadingPanel()
        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Try
                                      Dim result = Presenter.ListViewCampaignKardex(CampaignDetailId, 0)
                                      INDviewMaster.HideLoadingPanel()
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcDetails1.SafeInvoke(Sub()
                                                                       INDgcDetails1.DataSource = result
                                                                       If result.Count = 0 Then
                                                                           Mensaje(EeventViewerImages.Advertencia) = "No se encontraron movimientos en el Kardex"
                                                                           tokenAsync.Cancel()
                                                                           Me.Close()
                                                                           Exit Sub
                                                                       End If
                                                                       INDgcDetails1.RefreshDataSource()
                                                                   End Sub)
                                      End If
                                  Catch ex As Exception
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcDetails1.SafeInvoke(Sub()
                                                                       INDviewMaster.HideLoadingPanel()
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
        Presenter = New PCampaigns()
        GetDetail()
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

#Region "MasterRowGet"
    ''' <summary>
    ''' Eventos para asignar la relacion y el dataSource a la sub-rejilla 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewMaster_MasterRowGetChildList(sender As Object, e As MasterRowGetChildListEventArgs) Handles INDviewMaster.MasterRowGetChildList
        If e.ChildList Is Nothing Then
            Dim campaignKardexTmp = INDviewMaster.GetFocusedObject(Of ViewCampaignKardexXpo)
            INDGvDetails.ShowLoadingPanel()
            e.ChildList = Presenter.ListViewCampaignKardex(CampaignDetailId, campaignKardexTmp.ProductId)
            INDGvDetails.HideLoadingPanel()
        End If
    End Sub

    Private Sub INDviewMaster_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles INDviewMaster.MasterRowGetRelationName
        e.RelationName = "Movimientos"
    End Sub

    Private Sub INDviewMaster_MasterRowGetRelationCount(sender As Object, e As MasterRowGetRelationCountEventArgs) Handles INDviewMaster.MasterRowGetRelationCount
        e.RelationCount = 1
    End Sub
#End Region
#End Region

End Class