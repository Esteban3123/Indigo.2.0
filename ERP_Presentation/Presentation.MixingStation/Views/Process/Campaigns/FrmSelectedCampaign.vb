'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/03/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Base.Entities
Imports System.Text
Imports Presentation.Base.BaseClass
Imports System.ComponentModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Common.MVP
Imports System.Threading
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.Data.Xpo.MixingStationRepository
#End Region

Public Class FrmSelectedCampaign

#Region "Variables"

    ''' <summary>
    ''' Listado de campañas para que el usuario escoja a donde quiere enviar las solicitudes
    ''' </summary>
    Public ListCampaignDetail As List(Of CampaignDetailXpo)

    ''' <summary>
    ''' Listado de solicitudes
    ''' </summary>
    Public ListItems As Object

    ''' <summary>
    ''' Id linea de producción
    ''' </summary>
    Public ProductionLineId As Integer

    ''' <summary>
    ''' Id tipo dosis unitaria
    ''' </summary>
    Public UnitDoseTypeId As Integer

#End Region

#Region "Public Event"

    ''' <summary>
    ''' Evento para importar la informacion
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddSelected(ByVal e As AddSelectedCampaign)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmSelectedCampaign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDgcCampaigns.DataSource = ListCampaignDetail
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de importar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAcept_Click(sender As Object, e As EventArgs) Handles INDbtnAcept.Click
        Dim infoDetail = (From x In INDviewCampaign.GetSelectedRows() Select CType(INDviewCampaign.GetRow(x), CampaignDetailXpo)).FirstOrDefault()

        If infoDetail Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item"
            Exit Sub
        End If

        Dim args As New AddSelectedCampaign With {.CampaignDetailId = infoDetail.Id, .ListItems = ListItems, .ProductionLineId = ProductionLineId, .UnitDoseTypeId = UnitDoseTypeId}
        RaiseEvent AddSelected(args)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmSelectedCampaign_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "SelectionChanged"

    ''' <summary>
    ''' Evento que se dispara al seleccionar el check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewQuotations_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewCampaign.SelectionChanged
        Dim rowHandleInfoSelected = INDviewCampaign.FocusedRowHandle()
        Dim listRowHandles = INDviewCampaign.GetSelectedRows()

        If listRowHandles.Length > 0 Then
            For i = 0 To listRowHandles.Count - 1 Step 1
                If rowHandleInfoSelected <> listRowHandles(i) Then
                    INDviewCampaign.UnselectRow(listRowHandles(i))
                End If
            Next
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddSelectedCampaign
    Inherits EventArgs

    Property CampaignDetailId As Integer

    Property ListItems As Object

    Property ProductionLineId As Integer

    Property UnitDoseTypeId As Integer

End Class