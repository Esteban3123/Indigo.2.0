Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

Public NotInheritable Class MessagesConversations
    ''' <summary>
    ''' Evento cuando presiona el boton responder
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <remarks></remarks>
    Shared Event Answer(ByVal Code As String)
    ''' <summary>
    ''' Formulario de Notification
    ''' </summary>
    ''' <remarks></remarks>
    Shared FrmMessage As FrmMessageConversation

    ''' <summary>
    ''' Metodo para hacer show del control de notification de mensajes recibidos
    ''' </summary>
    ''' <param name="UserName"></param>
    ''' <param name="UserCode"></param>
    ''' <param name="Photo"></param>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overloads Shared Sub Show(ByVal UserName As String, ByVal UserCode As String, ByVal Photo As String, ByVal Message As String)
        FrmMessage = New FrmMessageConversation
        If FrmMessage.InvokeRequired = True Then
            FrmMessage.BeginInvoke(Sub()
                                       LoadNotification(UserName, UserCode, Photo, Message)
                                   End Sub)
        Else
            LoadNotification(UserName, UserCode, Photo, Message)
        End If
    End Sub

    ''' <summary>
    ''' Metodo para cargar el control de mensajes recibidos
    ''' </summary>
    ''' <param name="UserName"></param>
    ''' <param name="UserCode"></param>
    ''' <param name="Photo"></param>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Private Shared Sub LoadNotification(ByVal UserName As String, ByVal UserCode As String, ByVal Photo As String, ByVal Message As String)
        Dim ImageUser As New PictureEdit
        ImageUser.EditValue = Convert.FromBase64String(Photo)
        AddHandler FrmMessage.Answer, AddressOf AnswerFrm
        FrmMessage.UserName = UserName
        FrmMessage.UserCode = UserCode
        FrmMessage.UserMessage = Message
        FrmMessage.UserPhoto = CType(ImageUser.Image, Bitmap)
        FrmMessage.Location = New Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 174 - 50, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - 208 - 50)
        FrmMessage.Show()
    End Sub

    ''' <summary>
    ''' Metodo para disparar el evento de notificacion
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <remarks></remarks>
    Private Shared Sub AnswerFrm(ByVal Code As String)
        RaiseEvent Answer(Code)
    End Sub

End Class
