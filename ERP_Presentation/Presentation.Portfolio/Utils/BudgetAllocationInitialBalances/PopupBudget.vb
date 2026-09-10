'***********************************************************************
' Assembly         : Presentacion.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 17/03/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Portfolio.MVP
#End Region

Public Class PopupBudget

#Region "PROPERTIES"
    ReadOnly Property BudgetId As Integer
        Get
            Return INDSleBudget.EditValue
        End Get
    End Property
#End Region

#Region "HANDLES"
    Private Sub PopupBudget_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDSleBudget.Properties.Buttons(1).Visible = False

        Using model As New MPortfolioInitialBalance(Me.Tag)
            INDSleBudget.Properties.DataSource = model.ListBudgetByCategoryItemType()
        End Using
    End Sub


    Private Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        If INDSleBudget.EditValue Is Nothing Then
            Exit Sub
        End If
        Me.Close()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    Private Sub PopupBudget_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region
    
End Class