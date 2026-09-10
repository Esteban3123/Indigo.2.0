Public Class CtrTotalValue

    Public WriteOnly Property TotalValue As Decimal
        Set(value As Decimal)
            INDlblTotalValue.Text = String.Format("{0:C2}", value)
        End Set
    End Property

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblTotalValue)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblTxt)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem2)
        Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem1)
    End Sub


End Class
