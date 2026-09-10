Imports DevExpress.XtraBars.Alerter
Imports Infrastructure.CrossCutting.Base

Public Class CustomAlertControl
    Inherits AlertControl
    'Inherits ToastNotification

    Private _TypeMessage As MessageType
    Public Property TypeMessage() As MessageType
        Get
            Return _TypeMessage
        End Get
        Set(ByVal value As MessageType)
            _TypeMessage = value
        End Set
    End Property
    Public Sub New()
    End Sub
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        MyBase.New(container)
    End Sub
    Protected Overrides Function CreateAlertForm(ByVal location As System.Drawing.Point, ByVal control As AlertControl, ByVal info As AlertInfo) As AlertForm
        Return New MyAlertForm(location, control, info)
    End Function

    Public Sub Show(ByVal owner As Form, ByVal caption As String, ByVal text As String, ByVal hotTrackedText As String, ByVal image As Image, ByVal tag As Object, ByVal color As Color)
        MyBase.Show(owner, New MyAlertInfo(caption, text, hotTrackedText, image, tag)) ', color))
    End Sub

    Private Sub InitializeComponent()
        '
        'CustomAlertControl
        '
        Me.AllowHotTrack = False

    End Sub
End Class

Public Class MyAlertForm
    Inherits AlertForm
    Public Sub New(ByVal location As System.Drawing.Point, ByVal control As IAlertControl, ByVal info As AlertInfo)
        MyBase.New(location, control, info)

    End Sub
    Protected Overrides Function CreatePainter() As AlertPainter
        Return New MyAlertPainter(Me)
    End Function
End Class

Public Class MyAlertPainter
    Inherits AlertPainter
    Public Sub New(ByVal form As AlertFormCore)
        MyBase.New(form)
    End Sub

    Protected Overrides Sub DrawContent(ByVal graphicsCache As DevExpress.Utils.Drawing.GraphicsCache, ByVal skin As DevExpress.Skins.Skin)
        MyBase.DrawContent(graphicsCache, skin)

        ' Verificar que Owner.Info sea del tipo correcto y tenga un color válido
        Dim myInfo As MyAlertInfo = TryCast(Owner.Info, MyAlertInfo)
        If myInfo IsNot Nothing AndAlso myInfo.BackColor <> Color.Empty AndAlso myInfo.BackColor <> Color.Transparent Then
            Dim rect As New Rectangle(Owner.ClientRectangle.Location, Owner.ClientRectangle.Size)
            rect.Inflate(-2, -2)
            Using brush As New SolidBrush(myInfo.BackColor)
                graphicsCache.Graphics.FillRectangle(brush, rect)
            End Using
        End If
    End Sub
End Class

Public Class MyAlertInfo
    Inherits AlertInfo
    Public Sub New(ByVal caption As String, ByVal text As String)
        MyBase.New(caption, text)

    End Sub
    Public Sub New(ByVal caption As String, ByVal text As String, ByVal hotTrackedText As String)
        MyBase.New(caption, text, hotTrackedText)

    End Sub
    Public Sub New(ByVal caption As String, ByVal text As String, ByVal image As Image)
        MyBase.New(caption, text, image)

    End Sub
    Public Sub New(ByVal caption As String, ByVal text As String, ByVal hotTrackedText As String, ByVal image As Image)
        MyBase.New(caption, text, hotTrackedText, image)

    End Sub
    Public Sub New(ByVal caption As String, ByVal text As String, ByVal hotTrackedText As String, ByVal image As Image, ByVal tag As Object)
        MyBase.New(caption, text, hotTrackedText, image, tag)

    End Sub

    Public Sub New(ByVal caption As String, ByVal text As String, ByVal hotTrackedText As String, ByVal image As Image, ByVal tag As Object, ByVal color As Color)
        MyBase.New(caption, text, hotTrackedText, image, tag)
        BackColor = color
    End Sub

    Private privateBackColor As Color
    Public Property BackColor() As Color
        Get
            Return privateBackColor
        End Get
        Set(ByVal value As Color)
            privateBackColor = value
        End Set
    End Property

End Class