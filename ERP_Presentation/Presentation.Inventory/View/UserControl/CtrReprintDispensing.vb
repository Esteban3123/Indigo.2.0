Imports System.ComponentModel
Imports DevExpress.XtraEditors.Mask

Public Class CtrReprintDispensing

#Region "Properties"

    <BrowsableAttribute(False)>
    Public Property PopupContainerControlTotalValue As DevExpress.XtraEditors.PopupContainerControl
        Get
            Return Me.INDpceRealizedDispensing.Properties.PopupControl
        End Get
        Set(value As DevExpress.XtraEditors.PopupContainerControl)
            Me.INDpceRealizedDispensing.Properties.PopupControl = value
        End Set
    End Property

#End Region

#Region "Methods"

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDpceRealizedDispensing)
    End Sub

#End Region

#Region "Events"

    Private Sub INDPceTotalValue_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceRealizedDispensing.QueryPopUp
        RaiseEvent OpenPopupValue(sender, e)
    End Sub

    Event OpenPopupValue(sender As Object, e As CancelEventArgs)

#End Region

End Class
