'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Andres Alarcon
' Created          : 05/10/2022
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.MixingStation.MVP
Imports Domain.Entities

#End Region

Public Class FrmViewDetailCampaing

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PDashboardProductionSchedule
    ''' <summary>
    ''' Id de la campaña
    ''' </summary>
    Public campaignDetailId As Integer

    Public MixingStationId As Integer

    Private indigo As SessionValues

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

    Public WriteOnly Property CampaingNumber As Integer
        Set(value As Integer)
            INDtxtCampaing.Text = value
        End Set
    End Property

    Public WriteOnly Property HourProcessed As String
        Set(value As String)
            INDtxtHourProcess.Text = value
        End Set
    End Property

    Public WriteOnly Property State As String
        Set(value As String)
            INDtxtState.Text = value
        End Set
    End Property

    Public WriteOnly Property QuantityAd As Integer
        Set(value As Integer)
            INDtxtQuantityAd.Text = value
        End Set
    End Property

    Public WriteOnly Property WorkingArea As String
        Set(value As String)
            INDtxtWorkingArea.Text = value
        End Set
    End Property

    Public Property HourFinished As Date

    Public Property UnitDoseTypeId As MixinStationUnitDoseTypeXpo

    Public Property ProductionLineId As MixingStationProductionLineXpo

#End Region

#Region "Methods"
    Private Sub CampaingDetails()
        Using Model As New MCampaign(CStr(Me.Tag))
            Dim resultOperation = Model.GetCampaignDetailXpoById(campaignDetailId)

            UnitDoseTypeId = resultOperation.UnitDoseTypeId
            ProductionLineId = resultOperation.ProductionLineId
            If resultOperation.CampaignStatus = 6 Then
                HourFinished = resultOperation.FinishDate
                INDtxtHourFinished.EditValue = HourFinished
            Else
                INDtxtHourFinished.EditValue = String.Empty
            End If

            INDtxtUnitDoseType.EditValue = UnitDoseTypeId?.CodeDescription
            INDtxtProductionLine.EditValue = ProductionLineId?.CodeName

            INDgcResponsible.DataSource = Presenter.ListUsersCampaing(campaignDetailId)
            INDgcStateAd.DataSource = Presenter.ListRequestDetailStatus(campaignDetailId)

        End Using
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Async Sub FrmViewDetailCampaing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.indigo = SessionValues.Instance
        Presenter = New PDashboardProductionSchedule

        CampaingDetails()
    End Sub

#End Region
#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al oprimir escape para cerrar el popup 
    ''' </summary>
    Private Sub FrmViewDetailCampaing_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region
#End Region
End Class