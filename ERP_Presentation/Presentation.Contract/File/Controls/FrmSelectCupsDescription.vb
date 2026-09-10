'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/03/2020
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
Imports Presentation.Base.BaseClass
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Contract.MVP
Imports System.Windows.Forms
Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports System.Threading
Imports Infrastructure.Data.Xpo.ContractRepository
#End Region

Public Class FrmSelectCupsDescription

#Region "Properties"

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

#Region "Variables"

    Private tokenAsync As CancellationTokenSource

    Dim Presenter As PProcedureTemplate

    Public ListCupsIds As List(Of Integer)

    Public ListCupsEntityWithDescriptionsXpo As List(Of ViewListCupsEntityWithDescriptionsXpo)

#End Region

#Region "Methods"

    Private Sub ExecuteGetList()
        INDviewCupsDescription.ShowLoadingPanel()
        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListCupsEntityWithDescriptions(ListCupsIds)
                                  If Not tokenAsync.IsCancellationRequested Then
                                      INDgcCupsDescription.SafeInvoke(Sub()
                                                                          INDviewCupsDescription.HideLoadingPanel()
                                                                          INDgcCupsDescription.DataSource = result
                                                                      End Sub)
                                  End If
                              End Sub, tokenAsync.Token)
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub FrmSelectCupsDescription_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PProcedureTemplate()
        ExecuteGetList()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmSelectCupsDescription_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAccept_Click(sender As Object, e As EventArgs) Handles INDbtnAccept.Click
        If INDviewCupsDescription.LoadingPanelVisible Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha terminado de cargar la rejilla"
            Exit Sub
        End If

        Dim ListDetails = (From x In DirectCast(INDgcCupsDescription.FocusedView, GridView).GetSelectedRows() Select CType(DirectCast(INDgcCupsDescription.FocusedView, GridView).GetRow(x), ViewListCupsEntityWithDescriptionsXpo)).ToList()
        If ListDetails Is Nothing OrElse ListDetails.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item"
            Exit Sub
        End If

        ListCupsEntityWithDescriptionsXpo = ListDetails
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmSelectCupsDescription_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub

#End Region

#End Region

End Class
