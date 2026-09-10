'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 14/01/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Class PopupOrderCode
    ReadOnly Property OrderCode As String
        Get
            Return INDTxtOrderCode.EditValue
        End Get
    End Property

    
    Private Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        If INDTxtOrderCode.EditValue Is Nothing Then
            Exit Sub
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub PopupOrderCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub PopupOrderCode_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDTxtOrderCode.Focus()
    End Sub
End Class