Public Class CtrCurrentPeriod

    Public Delegate Sub GetDatePeriod(ByVal datePeriod As DateTime)
    Public _functionGetCurrentPeriod As GetDatePeriod


    Public Sub setMethodPeriod(functionGetCurrentPeriod As GetDatePeriod)
        _functionGetCurrentPeriod = functionGetCurrentPeriod
    End Sub

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
    End Sub

End Class
