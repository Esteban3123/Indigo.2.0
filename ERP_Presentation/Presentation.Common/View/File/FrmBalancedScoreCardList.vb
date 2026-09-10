Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.Base

Public Class FrmBalancedScoreCardList

    Public Property BalancedSelected As CommonBalancedScorecardXpo

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Private Sub FrmBalancedScoreCardList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub INDsbAcept_Click(sender As Object, e As EventArgs) Handles INDsbAcept.Click
        If INDsleBalancedScorecard.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Item"
            Exit Sub
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub INDsleBalancedScorecard_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleBalancedScorecard.QueryPopUp
        If INDsleBalancedScorecard.Properties.DataSource Is Nothing Then
            INDsleBalancedScorecard.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer) _
                .CommonService.GetBalancedScorecard()
        End If
    End Sub

    Private Sub INDsleBalancedScorecard_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleBalancedScorecard.EditValueChanging
        If e.NewValue Is Nothing OrElse e.NewValue.ToString().Equals("") Then
            BalancedSelected = Nothing
        Else
            BalancedSelected = CType(CType(INDgvBalancedScorecard.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, CommonBalancedScorecardXpo)
        End If
    End Sub

    Private Sub FrmBalancedScoreCardList_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

End Class