Imports Presentation.Controls

Public Class FrmAlertCostDistribution

    Private Sub FrmAlertCostDistribution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    Private Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Close()
    End Sub
End Class