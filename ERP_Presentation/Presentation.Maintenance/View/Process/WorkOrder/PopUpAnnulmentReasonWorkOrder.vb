Imports System.Text
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo

Public Class PopUpAnnulmentReasonWorkOrder

#Region "Properties"

    Public Property PopUpType As Byte

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

#End Region

#Region "Methods"

    Private Function ValidateFields() As Boolean
        Dim errorList As New StringBuilder()
        If Not {1, 2}.Contains(Me.PopUpType) Then
            If SleAnnulmentReason.EditValue Is Nothing Then
                errorList.AppendLine(LiAnnulateReason.Text)
            End If
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
        SleAnnulmentReason.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).MaintenanceService.ListMaintenanceAnulateReason()
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Sub PopUpAnnulmentReason_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.PopUpType = 1 Then
            Me.Text = "Aprobación"
            LiAnnulateReason.HideControl(True)
        ElseIf Me.PopUpType = 2 Then
            Me.Text = "Rechazo"
            LiAnnulateReason.HideControl(True)
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub PopUpAnnulmentReason_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.SleAnnulmentReason.Focus()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub PopUpAnnulmentReason_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub SleAnnulmentReason_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles SleAnnulmentReason.QueryPopUp
        If SleAnnulmentReason.Properties.DataSource Is Nothing Then
            LoadAnnulmentReason()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDsbAcept_Click(sender As Object, e As EventArgs) Handles INDsbAcept.Click
        If ValidateFields() Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If
    End Sub

#End Region

#End Region

End Class