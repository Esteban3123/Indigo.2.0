'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : 
' Created          : 2014-07-15
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Presentation.Glosas.MVP
Imports Presentation.Base
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports System.Text
Imports Domain.Base.Entities

Public Class FrmConfirmationRadicate



    Public ReadOnly Property Comment As String
        Get
            Return Me.INDmeCommentConfirm.Text.Trim()
        End Get
    End Property

    Public ReadOnly Property DateConfirm As Date
        Get
            Return Me.INDdteDateConfirm.EditValue
        End Get
    End Property

    Dim _fechaServidor As Date
    ''' <summary>
    ''' propiedad de la fecha del servidor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FechaServidor As Date
        Get
            Return _fechaServidor
        End Get
        Set(value As Date)
            _fechaServidor = value
        End Set
    End Property

    Dim _fechaDocumento As Date
    ''' <summary>
    ''' fecha documento radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FechaDocumento As Date
        Get
            Return _fechaDocumento
        End Get
        Set(value As Date)
            _fechaDocumento = value
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
        If Me.INDdteDateConfirm.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiDateConfirm.Text)
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

    Private Sub FrmConfirmationRadicate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ToolBars.Visible = False
        Me.INDdteDateConfirm.EditValue = FechaServidor
        Me.INDmeCommentConfirm.Text = String.Empty
    End Sub

    Private Sub INDdteDateConfirm_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteDateConfirm.EditValueChanged
        If INDdteDateConfirm.EditValue IsNot Nothing Then
            If CDate(INDdteDateConfirm.EditValue).Date > CDate(FechaServidor).Date Then
                Mensaje(EeventViewerImages.Advertencia) = "La fecha de confirmación no puede ser superior a la fecha actual"
                INDdteDateConfirm.EditValue = FechaServidor
            End If
            If CDate(INDdteDateConfirm.EditValue).Date < CDate(FechaDocumento).Date Then
                Mensaje(EeventViewerImages.Advertencia) = "La fecha de confirmación no puede ser inferior a la fecha del documento de radicación"
                INDdteDateConfirm.EditValue = FechaServidor
            End If
        End If
    End Sub

End Class