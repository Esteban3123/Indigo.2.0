Imports System.ComponentModel

Public Class CtrStatusTransactions

    Public Delegate Function SetInfoDelegate() As Tuple(Of Boolean, Boolean)

    Private _setInfoDelegate As SetInfoDelegate

    Private Sub Ctr_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem2)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem3)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem4)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblNameVie)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblNameHeon)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblVie)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDlblHeon)
    End Sub

    Public Sub SetInfoFunction(setInfoDelegate As SetInfoDelegate)
        _setInfoDelegate = setInfoDelegate
    End Sub

    Public Sub PrintInfo()
        If _setInfoDelegate IsNot Nothing Then
            Dim value As Tuple(Of Boolean, Boolean) = _setInfoDelegate()
            If value.Item1 Then
                INDlblVie.Text = "CORRECTO"
            Else
                INDlblVie.Text = "ERROR"
            End If
            If value.Item2 Then
                INDlblHeon.Text = "CORRECTO"
            Else
                INDlblHeon.Text = "ERROR"
            End If
        End If
    End Sub

End Class
