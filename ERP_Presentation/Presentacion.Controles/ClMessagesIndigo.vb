Imports DevExpress.XtraEditors
Imports Domain.Base.Entities

Public Class ClMessagesIndigo
    Public Property Notification As Integer
    Public Property UserCode As String
    Public Property Name As String
    Public ReadOnly Property Status As Bitmap
        Get
            Select Case StatusUser
                Case AppUserStatus.Online
                    Return My.Resources.Online
                Case AppUserStatus.Missing
                    Return My.Resources.Absentee
                Case AppUserStatus.Busy
                    Return My.Resources.Bussy
                Case AppUserStatus.Offline
                    Return My.Resources.Offline
                Case Else
                    Return My.Resources.Offline
            End Select
        End Get
    End Property
    Public ReadOnly Property Photo As Bitmap
        Get
            If PhotoUser Is Nothing Then
                Return My.Resources.User50x50
            Else
                Dim ImageUser As New PictureEdit
                ImageUser.EditValue = PhotoUser
                ImageUser.Size = New Size(50, 50)
                CType(ImageUser.Image, Bitmap).SetResolution(50, 50)
                Return CType(ImageUser.Image, Bitmap)
            End If
        End Get
    End Property
    Public Property GroupName As String
    Public Property GroupId As Integer
    Public Property Ocupation As String
    Public Property PersonalNote As String
    Public Property PhotoUser As Byte()
    Public Property PhotoUserString As String
        Get
            If PhotoUser IsNot Nothing Then Return Convert.ToBase64String(PhotoUser) Else Return String.Empty
        End Get
        Set(value As String)
            If value IsNot String.Empty Then
                PhotoUser = Convert.FromBase64String(value)
            Else
                PhotoUser = Nothing
            End If
        End Set
    End Property
    Public Property StatusUser As AppUserStatus
End Class
