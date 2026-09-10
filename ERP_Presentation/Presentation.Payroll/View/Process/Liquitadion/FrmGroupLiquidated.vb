Imports Domain.Payroll.Entities
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports System.ComponentModel


Public Class FrmGroupLiquidated

    Public GroupId As String
    Public PayrollDateLiquidated As Date
    Public VisualizarFlag As Boolean = False
#Region "Properties"
    ''' <summary>
    ''' Establece el datasource de la rejilla de los mensajes 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property InfoDialogDatasource As List(Of Object)
        Set(value As List(Of Object))
            INDGCPayrollGroupLiquidation.DataSource = value.Distinct()
        End Set
    End Property
#End Region

#Region "Events"
    Private Sub INDBtnCancelar_Click(sender As Object, e As EventArgs) Handles INDBtnCancelar.Click
        VisualizarFlag = False
        GroupId = ""
        PayrollDateLiquidated = New Date()
        Me.Close()
    End Sub
#End Region

    Private Sub INDBtnVisualizar_Click(sender As Object, e As EventArgs) Handles INDBtnVisualizar.Click
        VisualizarFlag = True
        Me.Close()
    End Sub

    Private Sub GridView1_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        CargarLiquidacion(e)
    End Sub

    Private Sub CargarLiquidacion(e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs)
        If (e.RowHandle < 0) Then
            'Writing code for invalid row selected
        Else
            ''Recover informationView about row using some method: GetRow(), GetRowCellValue(), etc
            Dim register = GridView1.GetRow(e.RowHandle)
            GroupId = GridView1.GetRowCellValue(e.RowHandle, "GroupId")
            PayrollDateLiquidated = GridView1.GetRowCellValue(e.RowHandle, "PayrollDateLiquidated")
        End If
    End Sub

    Private Sub FrmGroupLiquidated_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

End Class