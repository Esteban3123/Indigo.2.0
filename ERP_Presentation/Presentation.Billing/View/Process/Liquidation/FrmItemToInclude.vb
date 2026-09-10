Imports Presentation.Contract
Imports System.Drawing
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Base

Public Class FrmItemToInclude

    Public Property ListServiceOrderDetailId As List(Of Integer)
    Public Property AdmissionNumber As String
    Public Event ServiceOrderDetailSelected(ByVal serviceOrderDetailSelected As Integer, serviceOrderDetailInclude As List(Of Integer))

    Private Async Function SearchLookUpEdit1_ButtonClickAsync(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) As Task Handles INDsleServiceOrderDetail.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmIPSService
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Await InitializeIPSService()
            End Using
        End If
    End Function

    Private Sub SearchLookUpEdit1_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleServiceOrderDetail.QueryPopUp
        
    End Sub

    Private Async Function InitializeIPSService() As Task
        Using model As New MLiquidation
            Dim result = Await model.GetListServiceOrderDetail(ListServiceOrderDetailId, AdmissionNumber)
            Me.INDsleServiceOrderDetail.Properties.DataSource = result
        End Using
    End Function

    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If INDsleServiceOrderDetail.EditValue IsNot Nothing Then
            RaiseEvent ServiceOrderDetailSelected(CInt(INDsleServiceOrderDetail.EditValue), ListServiceOrderDetailId)
            Me.Close()
        End If
    End Sub

    Private Async Function FrmItemToInclude_LoadAsync(sender As Object, e As EventArgs) As Task Handles MyBase.Load
        Await InitializeIPSService()
    End Function

End Class