'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 24-08-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


Public Class FrmDocumentNotConfirmed
    ''' <summary>
    ''' Propiedad que establece las lista de documentos
    ''' </summary>
    WriteOnly Property ListDocumet As List(Of Tuple(Of String, String, DateTime))
        Set(value As List(Of Tuple(Of String, String, DateTime)))
            INDGcDocuments.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Evento para cerrar el frm al presionar enter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDocumentNotConfirmed_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
    ''' <summary>
    ''' Evento que carga los el formmulario de los documementos no confirmados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDocumentNotConfirmed_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGvDocuments.ExpandAllGroups()
    End Sub
End Class