Imports Infrastructure.CrossCutting.Base
Imports DevExpress.XtraEditors
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base.Window

Public Class CtrNotificationItem
    Private _TypeMessage As MessageType
    Public Sub New(ByVal Title As String, ByVal Message As String, ByVal TypeMessage As MessageType, ByVal MessageDate As Date, ByVal AditionalString As String)
        InitializeComponent()
        _TypeMessage = TypeMessage
        Select Case TypeMessage
            Case MessageType.Information
                INDpcFirstLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(87, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(214, Byte), Integer))
                INDpcTypeMessage.BackColor = System.Drawing.Color.FromArgb(CType(CType(87, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(214, Byte), Integer))
            Case MessageType.Errores
                INDpcFirstLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(46, Byte), Integer))
                INDpcTypeMessage.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(46, Byte), Integer))
            Case MessageType.Warning
                INDpcFirstLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(48, Byte), Integer))
                INDpcTypeMessage.BackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(48, Byte), Integer))
            Case MessageType.Question
                INDpcFirstLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(77, Byte), Integer))
                INDpcTypeMessage.BackColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(77, Byte), Integer))
        End Select
        Dim _color As Color
        Select Case DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName
            Case "The Bezier", "Office 2019 Dark Gray", "Black", "Office 2007 Black", "Pumpkin", "Office 2019 Colorful"
                _color = Color.White
            Case Else
                _color = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinMaskColor
        End Select
        INDlblTitleMessage.Appearance.ForeColor = _color

        INDlblTitleMessage.Text = Title
        INDlblDateMessage.Text = MessageDate.ToString("dd MMM HH:mm")
        If AditionalString = String.Empty Then
            MessageIndigo = Message
            INDlblMessage.Text = Message
        Else
            MessageIndigo = Message & "..." & Environment.NewLine & Environment.NewLine & "Informacion Adicional" & Environment.NewLine & AditionalString
            INDlblMessage.Text = Message & "..." & Environment.NewLine & Environment.NewLine & "Informacion Adicional" & Environment.NewLine & AditionalString
        End If
    End Sub

    Public Property MessageIndigo As String

    Private Sub INDpcWhiteLeft_Click(sender As Object, e As EventArgs) Handles MyBase.Click, INDpcWhiteLeft.Click, INDlblTitleMessage.Click, INDlblMessage.Click, INDlblDateMessage.Click
        Dim Formulario = New FrmTransparent(New FrmFlyoutIndigo(MessageIndigo, Botones.Cancelar, _TypeMessage, Nothing)).ShowDialog(MDISingleInstance.Instance.InstanceMDI)
        'Dim frmNotificationItem As New FrmNotificationItemDetail()
        'frmNotificationItem.TxtMessage.Text = MessageIndigo
        'Using transparent As New FrmTransparent(frmNotificationItem, False)
        '    transparent.ShowDialog(Me)
        'End Using
        'XtraMessageBox.Show(MessageIndigo, "Mensaje Indigo", MessageBoxButtons.OK)
    End Sub
End Class
