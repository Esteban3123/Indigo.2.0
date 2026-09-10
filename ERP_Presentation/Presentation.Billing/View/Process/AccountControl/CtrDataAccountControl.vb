Public Class CtrDataAccountControl

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
    End Sub

End Class
