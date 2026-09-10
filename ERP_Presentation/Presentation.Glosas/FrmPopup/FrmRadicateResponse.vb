'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : JOHAN SEBASTIAN CUELLAR ESQUIVEL
' Created          : 2022-05-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources

Public Class FrmRadicateResponse

    ''' <summary>
    ''' Entidad de la respues de radicación
    ''' </summary>
    Public radicateResponse As RadicateResponse


    Public ReadOnly Property Comment As String
        Get
            Return Me.INDmeCommentConfirm.Text.Trim()
        End Get
    End Property

    Dim _DateConfirm As Date
    Public Property DateConfirm As Date
        Get
            Return Me.INDdteDateConfirm.EditValue
        End Get
        Set(value As Date)
            Me.INDdteDateConfirm.EditValue = value
        End Set
    End Property

    Dim _DateDocument As Date
    ''' <summary>
    ''' fecha documento oficio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DateDocument As Date
        Get
            Return _DateDocument
        End Get
        Set(value As Date)
            _DateDocument = value
        End Set
    End Property

    Dim _DateNow As Date
    ''' <summary>
    ''' fecha actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DateNow As Date
        Get
            Return _DateNow
        End Get
        Set(value As Date)
            _DateNow = value
        End Set
    End Property

    Dim _State As Integer
    ''' <summary>
    ''' Estado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property State As Integer
        Get
            Return _State
        End Get
        Set(value As Integer)
            _State = value
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


#Region "Click"

    Private Sub INDbtSave_Click(sender As Object, e As EventArgs) Handles INDbtSave.Click
        If Me.INDdteDateConfirm.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiDateRadicate.Text)
            Exit Sub
        End If
        If INDdteDateConfirm.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la fecha de radicacion"
            Exit Sub
        End If
        If String.IsNullOrEmpty(INDmeWhoReceive.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe indicar quien recibe la respuesta"
            Exit Sub
        End If
        If String.IsNullOrEmpty(INDmeWhoSend.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe indicar quien envia la respuesta"
            Exit Sub
        End If
        If State = 2 Then
            Mensaje(EeventViewerImages.Advertencia) = "El oficio ya se encuentra confirmado"
            Exit Sub
        End If
        AssignValues()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    ''' <summary>
    ''' Asignar valores para la respuesta de radicación
    ''' </summary>
    ''' <returns></returns>
    Private Function AssignValues() As RadicateResponse
        If radicateResponse Is Nothing OrElse radicateResponse?.Id = 0 Then
            radicateResponse = New RadicateResponse()
        End If

        With radicateResponse
            .DateRadicate = INDdteDateConfirm.EditValue
            .WhoReceive = INDmeWhoSend.EditValue
            .WhoSend = INDmeWhoReceive.EditValue
            .Comments = Comment
        End With
        Return radicateResponse
    End Function

    ''' <summary>
    '''  
    ''' </summary>
    ''' <returns></returns>
    Private Function LoadValue()
        With radicateResponse
            INDdteDateConfirm.EditValue = .DateRadicate
            INDmeWhoSend.EditValue = .WhoReceive
            INDmeWhoReceive.EditValue = .WhoSend
            INDmeCommentConfirm.EditValue = .Comments
        End With
    End Function
#End Region

    Private Sub FrmRadicateResponse_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.DialogResult <> System.Windows.Forms.DialogResult.OK Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        End If
    End Sub

    Private Sub INDbteCancel_Click(sender As Object, e As EventArgs) Handles INDbteCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub FrmRadicateResponse_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ToolBars.Visible = False
        If radicateResponse IsNot Nothing Then
            LoadValue()
        End If
    End Sub


End Class