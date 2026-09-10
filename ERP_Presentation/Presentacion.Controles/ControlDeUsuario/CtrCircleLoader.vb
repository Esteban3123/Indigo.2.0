Public Class CtrCircleLoader

    Public Property MessageLbl As String
        Get
            Return Me.INDMessageLbl.Text
        End Get
        Set(value As String)
            Me.INDMessageLbl.Text = value
        End Set
    End Property

    Public WriteOnly Property BorderControl As Boolean
        Set(value As Boolean)
            If value Then
                Me.INDMessageLbl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
                Me.PictureEdit1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
            Else
                Me.INDMessageLbl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                Me.PictureEdit1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            End If
        End Set
    End Property

End Class
