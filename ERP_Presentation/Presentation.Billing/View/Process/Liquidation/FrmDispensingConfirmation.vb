Public Class FrmDispensingConfirmation

    Private _datasourceMessage As New List(Of String)()
    Private _messagePharmaceuticalDispensing As String
    Private _messageDevolutionDispensing As String
    Public WriteOnly Property MessagePharmaceuticalDispensing As String
        Set(value As String)
            If Not String.IsNullOrEmpty(value) Then
                _messagePharmaceuticalDispensing = value
                _datasourceMessage.Add(value)
            End If
        End Set
    End Property

    Public WriteOnly Property MessageDevolutionDispensing As String
        Set(value As String)
            If Not String.IsNullOrEmpty(value) Then
                _messageDevolutionDispensing = value
                _datasourceMessage.Add(value)
            End If
        End Set
    End Property

    Private Sub BtnAcept_Click(sender As Object, e As EventArgs) Handles BtnAcept.Click
        DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub FrmDispensingConfirmation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGcDispensing.DataSource = (From m In _datasourceMessage Select New With {.DocumentCode = m}).ToList()
    End Sub

    Private Sub GvDispensing_CustomDrawCardCaption(sender As Object, e As DevExpress.XtraGrid.Views.Card.CardCaptionCustomDrawEventArgs) Handles GvDispensing.CustomDrawCardCaption
        If _datasourceMessage.Count > 1 Then
            If e.RowHandle = 0 Then
                e.CardCaption = "Dispensaciones sin Confirmar"
            Else
                e.CardCaption = "Devoluciones sin Confirmar"
            End If
        Else
            If String.IsNullOrEmpty(_messagePharmaceuticalDispensing) Then
                e.CardCaption = "Devoluciones sin Confirmar"
            Else
                e.CardCaption = "Dispensaciones sin Confirmar"
            End If
        End If
    End Sub

End Class