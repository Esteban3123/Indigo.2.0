'***********************************************************************
' Assembly         : Presentacion.paymnets
' Author           : Rafael Eduardo PAtiño
' Created          : 2015-03-17
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


Imports Presentation.Base
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports System.Text
Imports Domain.Base.Entities
Imports Presentation.Payments.MVP

Public Class FrmRejectionComment

    Public ReadOnly Property CommentRejection As String
        Get
            Return Me.INDmeCommentRejection.Text.Trim()
        End Get
    End Property

    Public ReadOnly Property IdRejection As Integer
        Get
            Return Me.GridLookUpEdit1.EditValue
        End Get
    End Property

    Dim _Obj As DevExpress.Xpo.XPInstantFeedbackSource
    Public WriteOnly Property DataSourceRejection As DevExpress.Xpo.XPInstantFeedbackSource
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            _Obj = value
        End Set
    End Property

    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Tipo de icono del mensaje</param>
    ''' <value>Mensaje a registrar</value>
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

    Private Sub INDbtAccept_Click(sender As Object, e As EventArgs) Handles INDbtAccept.Click
        If Me.GridLookUpEdit1.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiRejection.Text)
            Exit Sub
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub FrmConfirmationRadicate_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult <> System.Windows.Forms.DialogResult.OK Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        End If
    End Sub

    Private Sub INDbteCancel_Click(sender As Object, e As EventArgs) Handles INDbteCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    ''' <summary>
    ''' Modelo 
    ''' </summary>
    ''' <remarks></remarks>
    Private Model As MAcceptanceTransfer

    Private Async Sub FrmConfirmationRadicate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ToolBars.Visible = False
        Model = New MAcceptanceTransfer(1505)
        GridLookUpEdit1.Properties.DataSource = Await Model.ListRejectionReason()
        Me.INDmeCommentRejection.Text = String.Empty
    End Sub
End Class