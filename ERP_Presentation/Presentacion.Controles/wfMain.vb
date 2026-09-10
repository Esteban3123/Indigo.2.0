Imports System.ComponentModel
Imports System.Text
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraWaitForm

Partial Public Class wfMain
    Inherits DemoWaitForm

    Public Sub New()
        InitializeComponent()
        Me.ShowOnTopMode = ShowFormOnTopMode.AboveParent
        ProgressPanel.Caption = "Procesando"
        ProgressPanel.Description = "Espere por favor..."
    End Sub

    Private lookAndFeel As UserLookAndFeel
    Protected Overrides ReadOnly Property TargetLookAndFeel() As DevExpress.LookAndFeel.UserLookAndFeel
        Get
            If lookAndFeel Is Nothing Then
                lookAndFeel = New UserLookAndFeel(Me)
                lookAndFeel.UseDefaultLookAndFeel = False
                lookAndFeel.SkinName = Infrastructure.CrossCutting.Base.Window.Utils.GetThemeSkinName(Infrastructure.CrossCutting.Base.SessionValues.Instance.UserIndigo)
            End If
            Return lookAndFeel
        End Get
    End Property

End Class
