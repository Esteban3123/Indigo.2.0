Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.ViewInfo
Imports System.Drawing
Imports Domain.Payroll.Entities
Imports DevExpress.XtraGrid.Columns
Imports Domain.Base.Entities

'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Cristhian Mauricio Salazar Narvaez
' Created          : 28-09-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Class FrmVacationDetail

    Private listNovelty As List(Of Novelty)
    ''' <summary>
    ''' Contructor del formulario
    ''' </summary>
    ''' <param name="listDetail">Lista de detalles</param>
    ''' <remarks></remarks>
    Public Sub New(listDetail As List(Of Novelty))
        Me.listNovelty = listDetail
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se carga un formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmNoveltyScheduleDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGridDetail.DataSource = listNovelty
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click sobre cancelar o no
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnCancel_Click(sender As Object, e As EventArgs) Handles INDbtnCancel.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Evento en el cual le asigno la imagen a cada detalle del calendario
    ''' </summary>
    ''' <param name="sender">gridView</param>
    ''' <param name="e">Celda</param>
    ''' <remarks></remarks>
    Private Sub GridView1_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDgvScheduleDetail.CustomDrawCell
        If e.Column.Name = INDcolTypeNovelty.Name Then
            e.DisplayText = FrmNovelty.GetStringTypeNovelty(e.CellValue)
        End If
    End Sub

End Class