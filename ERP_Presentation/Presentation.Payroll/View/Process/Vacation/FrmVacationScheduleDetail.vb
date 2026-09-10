Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.ViewInfo
Imports System.Drawing
Imports Domain.Payroll.Entities
Imports DevExpress.XtraGrid.Columns
Imports Domain.Base.Entities

'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Cristhian Mauricio Salazar Narvaez
' Created          : 15-09-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Class FrmVacationScheduleDetail

    ''' <summary>
    ''' Propiedad para saber si el usuario a dado aceptado las condiciones
    ''' </summary>
    ''' <remarks></remarks>
    Private _actionOk As Boolean = False
    Public Property ActionOk As Boolean
        Get
            Return _actionOk
        End Get
        Set(value As Boolean)
            _actionOk = value
        End Set
    End Property

    Private listScheduleDetail As List(Of ScheduleDetail)
    ''' <summary>
    ''' Contructor del formulario
    ''' </summary>
    ''' <param name="listDetail">Lista de detalles</param>
    ''' <remarks></remarks>
    Public Sub New(listDetail As List(Of ScheduleDetail))
        Me.listScheduleDetail = listDetail
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
        INDGridDetail.DataSource = listScheduleDetail
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en el boton "SI"
    ''' </summary>
    ''' <param name="sender">Objeto que disparo el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    ''' <remarks></remarks>
    Private Sub INDbtnOk_Click(sender As Object, e As EventArgs) Handles INDbtnYes.Click, INDbtnYes.Click
        ActionOk = True
        Me.Close()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click sobre cancelar o no
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnCancel_Click(sender As Object, e As EventArgs) Handles INDbtnNo.Click
        ActionOk = False
        Me.Close()
    End Sub

    ''' <summary>
    ''' Evento en el cual le asigno la imagen a cada detalle del calendario
    ''' </summary>
    ''' <param name="sender">gridView</param>
    ''' <param name="e">Celda</param>
    ''' <remarks></remarks>
    Private Sub GridView1_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDgvScheduleDetail.CustomDrawCell
        If e.Column.Name = INDcolLetter.Name Then
            If e.CellValue IsNot Nothing Then
                Dim imagenDia = CType(CType(e.Cell, GridCellInfo).ViewInfo, PictureEditViewInfo)
                imagenDia.Image = retornaImagenLetra(e.CellValue.ToString())
            End If
        End If
    End Sub

    ''' <summary>
    ''' Retorna la imagen de 16x16 de la letra que envien
    ''' </summary>
    ''' <param name="letra">Letra</param>
    ''' <returns>Imagen</returns>
    ''' <remarks></remarks>
    Private Function retornaImagenLetra(letra As String) As Image
        Dim retorno As Image
        Select Case letra
            Case "I"
                retorno = My.Resources.incapacidad_16x16
            Case "L"
                retorno = My.Resources.licencia_16x16
            Case "S"
                retorno = My.Resources.sancion_16x16
            Case "M"
                retorno = My.Resources.manana_16x16
            Case "T"
                retorno = My.Resources.tarde_16x16
            Case "N"
                retorno = My.Resources.noche_16x16
            Case "MT"
                retorno = My.Resources.manana_tarde_16x16
            Case "TN"
                retorno = My.Resources.tarde_noche_16x16
            Case "MN"
                retorno = My.Resources.manana_noche_16x16
        End Select
        Return retorno
    End Function
End Class