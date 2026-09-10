Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base

Public Class FrmCrossing

#Region "Properties"
    ''' <summary>
    ''' Bandera utilizada para saber si se está enviando a guardar para asi lanzar el mensaje al cerrar el formulario
    ''' </summary>
    Private _isSaving As Boolean = False
#End Region

#Region "Handlers"
    Private Sub FrmCrossing_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub INDBtnAcept_Click(sender As Object, e As EventArgs) Handles INDBtnAcept.Click

    End Sub

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click

    End Sub

    Private Sub FrmCrossing_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmCrossing_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not _isSaving Then
            If INDGvAnticipos.RowCount > 0 Then
                If MessageIndigo.Show("Si cierra esta ventana se perderán los datos, ¿Desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Sub INDGcCrossing_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcCrossing.DataSourceChanged
        RefreshTotals()
    End Sub
#End Region

#Region "Methods"
    Private Sub RefreshTotals()
        TxtTotalCrossing.Text = 0.ToString("$0.0")
    End Sub
#End Region

End Class