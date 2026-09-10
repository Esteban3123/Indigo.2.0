Imports System.ComponentModel
Imports Presentation.Base

Public Class CtrMessanging
    Public Event OpenConversation(ByVal Sender As CtrMessanging)
    Public Event CloseConversation(ByVal Sender As CtrMessanging)

    <CategoryAttribute("1. Indigo Properties")>
    Public Property UserName As String
        Get
            Return INDlblUserName.Text

        End Get
        Set(value As String)
            INDlblUserName.Text = value
        End Set
    End Property

    <CategoryAttribute("1. Indigo Properties")>
    Public Property UserPhoto As Image
        Get
            Return INDpeUserPhoto.Image
        End Get
        Set(value As Image)
            INDpeUserPhoto.Image = value
        End Set
    End Property

    Private _UserState As EStatusUserMessangin
    <CategoryAttribute("1. Indigo Properties")>
    Public Property UserState As EStatusUserMessangin
        Get
            Return _UserState
        End Get
        Set(value As EStatusUserMessangin)
            _UserState = value
            Select Case value
                Case EStatusUserMessangin.absent
                    INDpcStatusUserPhoto.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(0, Byte), Integer))
                Case EStatusUserMessangin.Bussy
                    INDpcStatusUserPhoto.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(14, Byte), Integer), CType(CType(13, Byte), Integer))
                Case EStatusUserMessangin.Offline
                    INDpcStatusUserPhoto.BackColor = System.Drawing.Color.FromArgb(CType(CType(182, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(216, Byte), Integer))
                Case EStatusUserMessangin.Online
                    INDpcStatusUserPhoto.BackColor = System.Drawing.Color.FromArgb(CType(CType(93, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(85, Byte), Integer))
            End Select
        End Set
    End Property

    Private _SelectItem As Boolean
    Public Property SelectItem As Boolean
        Get
            Return _SelectItem
        End Get
        Set(value As Boolean)
            _SelectItem = value
            If value = True Then
                INDpeNotification.Visible = False
                Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(234, Byte), Integer))
            Else
                Me.BackColor = Color.White
            End If
        End Set
    End Property

    Private _Messages As List(Of INDmessage)
    Public Property Messages As List(Of INDmessage)
        Get
            Return _Messages
        End Get
        Set(value As List(Of INDmessage))
            INDpeNotification.Visible = True
            _Messages = value
        End Set
    End Property

    Private Sub INDlblUserName_Click(sender As Object, e As EventArgs) Handles INDpeUserPhoto.Click, INDpcStatusUserPhoto.Click, INDlblUserName.Click
        RaiseEvent OpenConversation(Me)
    End Sub

    Private Sub INDbtnCerrar_Click(sender As Object, e As EventArgs) Handles INDbtnCerrar.Click
        Me.Parent.Controls.Remove(Me)
        RaiseEvent CloseConversation(Me)
        Me.Dispose()
    End Sub
End Class
