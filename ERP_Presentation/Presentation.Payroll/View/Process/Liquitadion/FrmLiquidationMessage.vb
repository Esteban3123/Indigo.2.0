Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.ViewInfo
Imports System.Drawing

Public Class FrmLiquidationMessage

    ''' <summary>
    ''' Evento que se dispara al dar click sobre ok
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnOK_Click(sender As Object, e As EventArgs) Handles INDbtnOK.Click
        Generate = True
        Me.Close()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click sobre cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnCancel_Click(sender As Object, e As EventArgs) Handles INDbtnCancel.Click
        Generate = False
        Me.Close()
    End Sub

    Private Sub FrmLiquidationMessage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class