#Region "Imports"

Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class FrmImportCostActivity

#Region "EVENTS"

    ''' <summary>
    ''' Evento para obtener el comprobante de contable seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="CostActivityId"></param>
    ''' <remarks></remarks>
    Public Event GetCostActivity(sender As Object, CostActivityId As Integer)

#End Region

#Region "GLOBALS"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Cost"

#End Region

#Region "HANDLES"

#Region "Load"

    ''' <summary>
    ''' load del del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportCostActivitys_Load(sender As Object, e As EventArgs) Handles MyBase.Load        
        IndigoGridControl1.RefreshGrid(INDGcImportCostActivity)

        Using model As New MVP.MCostActivity(Me.Tag)
            INDGcImportCostActivity.DataSource = Nothing
            INDGcImportCostActivity.DataSource = model.ListCostActivityXPO()
            INDGcImportCostActivity.RefreshDataSource()
        End Using
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmImportInfo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        If INDGvImportCostActivity.GetFocusedRow Is Nothing Then
            MessageIndigo.Show("Debe seleccionar un registro", MessageType.Warning, Me.Text)
            Exit Sub
        End If

        Dim CostActivity = DirectCast(DirectCast(INDGvImportCostActivity.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CostRepository.CostActivityXpo)

        Me.Close()
        RaiseEvent GetCostActivity(Nothing, CostActivity.Id)
    End Sub

#End Region

#End Region

End Class