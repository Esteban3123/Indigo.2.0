Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.ViewInfo
Imports System.Drawing

Public Class FrmAutoLiquidationMessage

    Private _generate As Boolean = False

    ''' <summary>
    ''' Propiedad para saber si se genera el plano
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Generate As Boolean
        Get
            Return _generate
        End Get
        Set(value As Boolean)
            _generate = value
        End Set
    End Property

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

    Private Sub INDgvMessage_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs)
        'If e.Column.Name = INDcolIcon.Name Then
        '    Dim value = CType(CType(e.Cell, GridCellInfo).RowInfo.RowKey, MessageIcon)
        '    Dim image = CType(CType(e.Cell, GridCellInfo).ViewInfo, PictureEditViewInfo)
        '    image.Image = FrmAutoLiquidationMessage.getImageVacation(value.IconMessage)
        'End If
    End Sub

    ''' <summary>
    ''' Funcion que obtiene la imagen que debe ir asociada a una vacacion
    ''' </summary>
    ''' <param name="vacation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getImageVacation(ByVal iconType As MessageIcon.Icon) As Image
        If iconType = MessageIcon.Icon.ERROR Then
            Return My.Resources.rojo_16x16
        ElseIf iconType = MessageIcon.Icon.WARNING Then
            Return My.Resources.amarillo_16x16
        End If
    End Function

  
End Class