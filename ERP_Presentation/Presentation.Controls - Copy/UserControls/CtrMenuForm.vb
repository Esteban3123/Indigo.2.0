Imports Domain.Security.Entities

Public Class CtrMenuForm

    Public Event ItemFormSelected(form As VieForm)



    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        Dim form As VieForm = GridView1.GetFocusedRow()
        RaiseEvent ItemFormSelected(form)
    End Sub
End Class
