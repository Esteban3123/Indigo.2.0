Public Class MoveDate

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Property DateSelected As Date
    Public Property NewDateSelected As Date

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        'DateNavigator1
        NewDateSelected = DateNavigator1.Selection(0).Date
        DialogResult = DialogResult.OK
    End Sub

    Private Sub MoveDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateNavigator1.DateTime = DateSelected
    End Sub
End Class