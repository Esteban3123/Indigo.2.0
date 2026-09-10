Imports System.ComponentModel
Imports DevExpress.XtraEditors.Mask

Public Class CtrStatusInfo

    WriteOnly Property Status As String
        Set(value As String)
            LblStatus.Text = value
        End Set
    End Property

    WriteOnly Property ToolTipStatus As String
        Set(value As String)
            LblStatus.ToolTip = value
        End Set
    End Property

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
    End Sub

End Class
