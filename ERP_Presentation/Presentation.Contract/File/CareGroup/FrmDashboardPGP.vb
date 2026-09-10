'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/08/2017
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
Imports Presentation.Contract.MVP
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.ContractRepository
Imports DevExpress.Xpo
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Common
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

#End Region

Public Class FrmDashboardPGP

    Dim Presenter As PCareGroup

    Public IdCareGroup As Integer

#Region "Properties"

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    ''' Carga la información en la rejilla
    ''' </summary>
    Private Sub ProcessData()
        'Se valida que el mes final no sea menor al mes inicial
        If CtrMonthEnd.GetMonth < CtrMonthInitial.GetMonth Then
            Mensaje(EeventViewerImages.Advertencia) = "El mes final no puede ser menor al mes inicial"
            INDgcInfo.DataSource = Nothing
            Exit Sub
        End If
        INDgcInfo.DataSource = Presenter.ListDashboardPgp(IdCareGroup, CtrMonthInitial.GetMonth, CtrMonthEnd.GetMonth)
    End Sub

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        IdCareGroup = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardPGP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Presenter = New PCareGroup()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento de escape del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardPGP_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de procesar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnProcess_Click(sender As Object, e As EventArgs) Handles INDbtnProcess.Click
        ProcessData()
    End Sub

#End Region

#End Region

End Class