'***********************************************************************
' Assembly         : Presentacion.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/06/2020
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

Public Class FrmAnnotations

#Region "Variables"

    ''' <summary>
    ''' Estado del cubrimiento contractual, se asigna dependiendo de la acción que se ejecuta en el form principal
    ''' </summary>
    Public ContractCoverageStatus As Integer

    ''' <summary>
    ''' Listado que se asigna con los items seleccionados en el form principal
    ''' </summary>
    Public ListViewDashboardContractCoverageXpo As List(Of ViewDashboardContractCoverageXpo)

#End Region

#Region "Event"

    Public Event ReturnModalArgs(sender As Object, e As EventArgs)

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

#Region "Handlers"

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAnnotations_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDmemoAnnotations.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se ejecuta al presionar aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        If String.IsNullOrEmpty(INDmemoAnnotations.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar una anotación"
            Exit Sub
        End If

        Dim listTuple As New List(Of Tuple(Of Integer, Integer, String))
        For Each item In ListViewDashboardContractCoverageXpo
            listTuple.Add(New Tuple(Of Integer, Integer, String)(item.ServiceOrderDetailId, ContractCoverageStatus, INDmemoAnnotations.EditValue))
        Next

        Me.AsyncLoader(True)
        Try
            Using model As New MDashboardContractCoverage("")
                Dim result = Await model.SP_SaveContractCoverage(listTuple)
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
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAnnotations_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

End Class