Imports System.Text
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Budget.MVP

Public Class PopUpAnnulmentReason

    Public Property BudgetaryValidityId As Integer

    Public Property ReversalReasonId As Integer
        Get
            Return CType(SleAnnulmentReason.EditValue, Integer)
        End Get
        Set(value As Integer)
            SleAnnulmentReason.EditValue = Nothing
        End Set
    End Property

    Public Property ReversalDescription As String
        Get
            Return INDmeDescription.Text
        End Get
        Set(value As String)
            INDmeDescription.Text = value
        End Set
    End Property

    Private Sub PopUpAnnulmentReason_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PopUpAnnulmentReason_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.SleAnnulmentReason.Focus()
    End Sub

    Private Sub INDsbAcept_Click(sender As Object, e As EventArgs) Handles INDsbAcept.Click
        If ValidateFields() Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If
    End Sub

    Private Sub PopUpAnnulmentReason_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub SleAnnulmentReason_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles SleAnnulmentReason.QueryPopUp
        If SleAnnulmentReason.Properties.DataSource Is Nothing Then
            LoadAnnulmentReason()
        End If
    End Sub

    Public WriteOnly Property ShowMessage(ByVal Icon As Base.EeventViewerImages) As String
        Set(value As String)
            If Icon = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Private Function ValidateFields() As Boolean
        Dim errorList As New StringBuilder()
        If SleAnnulmentReason.EditValue Is Nothing Then
            errorList.AppendLine(LiAnnulateReason.Text)
        End If
        If String.IsNullOrEmpty(INDmeDescription.Text.Trim()) Then
            errorList.AppendLine(LiAnnulateReasonDescription.Text)
        End If
        If errorList.Length > 0 Then
            Me.ShowMessage(EeventViewerImages.Advertencia) = String.Format("Existen campos sin diligenciar: " & vbCrLf & "{0}", errorList.ToString())
            Return False
        End If
        Return True
    End Function

    Private Sub LoadAnnulmentReason()
        Using model As New MBudgetConcept
            SleAnnulmentReason.Properties.DataSource = model.ListAnnulmentConcept(BudgetaryValidityId)
        End Using
    End Sub

End Class