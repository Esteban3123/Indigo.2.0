Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.ViewInfo
Imports System.Drawing
Imports Domain.Payroll.Entities
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Payroll.MVP

Public Class FrmPopuUpExtraTime

#Region "Variables"

    Public ListHoursExtras As New List(Of SP_AnalisEmployeeSchedule_Result)

    ''' <summary>
    ''' Listado de items seleccionados para cuando se filtre se reestablezca los checks de los items
    ''' </summary>
    Private selectedRows As New List(Of Integer)()

#End Region

#Region "ColumnFilterChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del filtro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INGridView1_ColumnFilterChanged(sender As Object, e As EventArgs) Handles GridView1.ColumnFilterChanged
        RestoreSelection(TryCast(sender, GridView))
    End Sub

#End Region

#Region "MouseDown"

    ''' <summary>
    ''' Evento que se dispara al checkear los items de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IGridView1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles GridView1.MouseDown
        Dim view As GridView = TryCast(sender, GridView)
        Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)
        If hi.Column IsNot Nothing AndAlso hi.Column.FieldName = "DX$CheckboxSelectorColumn" Then
            If Not hi.InRow Then
                Dim allSelected As Boolean = view.DataController.Selection.Count = view.DataRowCount
                If Not allSelected Then
                    For i As Integer = 0 To view.RowCount - 1
                        Dim sourceHandle As Integer = view.GetDataSourceRowIndex(i)
                        If Not selectedRows.Contains(sourceHandle) Then
                            selectedRows.Add(sourceHandle)
                        End If
                    Next i
                Else
                    selectedRows.Clear()
                End If
            Else
                Dim sourceHandle As Integer = view.GetDataSourceRowIndex(hi.RowHandle)
                If Not selectedRows.Contains(sourceHandle) Then
                    selectedRows.Add(sourceHandle)
                Else
                    selectedRows.Remove(sourceHandle)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Metodos"
    ''' <summary>
    ''' Metodo que reestablece el check de los items de la rejilla
    ''' </summary>
    ''' <param name="view"></param>
    Private Sub RestoreSelection(ByVal view As GridView)
        BeginInvoke(New Action(Sub()
                                   Dim i As Integer = 0
                                   Do While i < selectedRows.Count
                                       view.SelectRow(view.GetRowHandle(selectedRows(i)))
                                       i += 1
                                   Loop
                               End Sub))
    End Sub
#End Region

    ''' <summary>
    ''' Evento que se dispara al dar click sobre ok
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnOK_Click(sender As Object, e As EventArgs) Handles INDbtnOK.Click

        Dim ObjSelected = GridView1.GetSelectedRows()
        Dim ListSelectedHours = New List(Of SP_AnalisEmployeeSchedule_Result)

        If ObjSelected Is Nothing OrElse Not ObjSelected.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar por lo menos un ítem para realizar esta acción"
            Exit Sub
        End If

        For Each index As Integer In ObjSelected
            Dim AnalisisObject As SP_AnalisEmployeeSchedule_Result = GridView1.GetRow(index)
            ListSelectedHours.Add(AnalisisObject)
        Next


        Using model As New MEmployeeSchedule("2141")
            Dim result = model.SaveExtrahours(ListSelectedHours)

            If result.CodeMessage = "001" Then
                Mensaje(EeventViewerImages.Informacion) = result.Message
            Else
                Mensaje(EeventViewerImages.MensajeError) = result.Message
            End If

        End Using

        Generate = True
        'Me.Close()
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

        If ListHoursExtras.Count > 0 Then
            INDGcMessage.DataSource = ListHoursExtras
        End If

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property
End Class