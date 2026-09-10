Public Class FrmObservationModal

#Region "Properties"
    Private _title As String
    Public Property Title As String
        Get
            Return _title
        End Get
        Set(value As String)
            Text = value
            _title = value
        End Set
    End Property

    Public Property Observations As String
        Get
            Return INDMeObservation.Text
        End Get
        Set(value As String)
            INDMeObservation.Text = value
        End Set
    End Property
#End Region

#Region "Events"
    Private Sub FrmObservationModal_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub
#End Region
End Class