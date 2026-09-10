'***********************************************************************
' Assembly         : Presentacion.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/11/2015
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
Imports Presentation.MedicalFees.MVP
Imports Presentation.Contract
Imports DevExpress.Utils.Menu
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns
Imports System.Windows.Forms
Imports Presentation.Billing.MVP
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.ViewInfo
Imports Presentation.Common.MVP
Imports Presentation.Billing
Imports Presentation.Contract.MVP
Imports Infrastructure.Data.Xpo.CrystalRepository

#End Region

Public Class FrmSelectPercentage

#Region "PublicEvents"

    Public Event SetPercentage(sender As Object, e As SelectContractEventArgs)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el porcentaje
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Percentage As Decimal?
        Get
            Return INDsePercentage.EditValue
        End Get
        Set(value As Decimal?)
            INDsePercentage.EditValue = value
        End Set
    End Property

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
    ''' Acepta el porcentaje y lo envia al form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AcceptPercentage()
        If Percentage = 0 OrElse Percentage Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar un porcentaje."
            Exit Sub
        End If
        Dim args As New SelectContractEventArgs
        args.Percentage = Percentage
        RaiseEvent SetPercentage(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "Events"

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar por primera vez el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSelectPercentage_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsePercentage.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAccept_Click(sender As Object, e As EventArgs) Handles INDbtnAccept.Click
        AcceptPercentage()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSelectPercentage_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

End Class