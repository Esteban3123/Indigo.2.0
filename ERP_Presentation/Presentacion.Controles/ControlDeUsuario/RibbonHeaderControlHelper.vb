Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraBars.Ribbon.ViewInfo
Imports System.ComponentModel

Public Class RibbonHeaderControlHelper
    Inherits Component

    Public Sub New()

    End Sub

    Public Property RibbonControl() As RibbonControl
        Get
            Return _RibbonControl
        End Get
        Set(ByVal value As RibbonControl)
            _RibbonControl = value
            OnChanged()
        End Set
    End Property

    Private _RibbonControl As RibbonControl
    Private _HeaderControl As Control

    Public Property HeaderControl() As Control
        Get
            Return _HeaderControl
        End Get
        Set(ByVal value As Control)
            If _HeaderControl IsNot Nothing Then
                _HeaderControl.Visible = False
            End If
            _HeaderControl = value
            OnChanged()
        End Set
    End Property

    Private Sub OnChanged()
        If RibbonControl IsNot Nothing Then
            AddHandler RibbonControl.Paint, AddressOf RibbonControl_Paint
        End If
    End Sub

    Private Sub RibbonControl_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
        ReplaceUserControl(sender, e)
    End Sub

    Public Function GetRibbon() As RibbonControl
        Return RibbonControl
    End Function

    Public Sub ReplaceUserControl(ByVal sender As Object, ByVal e As PaintEventArgs)
        If HeaderControl IsNot Nothing Then
            Dim rc As RibbonControl = GetRibbon()
            HeaderControl.Parent = rc
            HeaderControl.Visible = True
            Dim vi As RibbonViewInfo = rc.ViewInfo
            Dim captionInfo As RibbonCaptionViewInfo = vi.Caption
            Dim height As Integer = If(HeaderControl.Height <= 1, 17, HeaderControl.Height)
            Dim width As Integer = HeaderControl.Width
            Dim buttonRect As New Rectangle(captionInfo.ContentBounds.Right - width, captionInfo.ContentBounds.Y + (captionInfo.ContentBounds.Height - height) \ 2, width, height)
            HeaderControl.Bounds = buttonRect
        End If
    End Sub

End Class