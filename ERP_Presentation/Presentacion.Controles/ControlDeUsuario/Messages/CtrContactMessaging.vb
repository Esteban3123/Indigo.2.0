Imports Presentation.Base
Imports System.ComponentModel

Public Class CtrContactMessaging

    Public Event ClicContact(ByVal Sender As CtrContactMessaging)


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
    Public Property UserLocation As String
        Get
            Return INDlblUserPosition.Text
        End Get
        Set(value As String)
            INDlblUserPosition.Text = value
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

    Private Sub INDpeUserPhoto_Click(sender As Object, e As EventArgs) Handles MyBase.Click, INDpeUserPhoto.Click, INDpcStatusUserPhoto.Click, INDlblUserPosition.Click, INDlblUserName.Click
        RaiseEvent ClicContact(Me)
    End Sub
End Class
