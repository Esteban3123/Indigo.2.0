'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : rafael Patiño
' Created          : 2014-03-12
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


''' <summary>
''' Formulario modal para comentario de reiteracion multiple
''' </summary>
Public Class FrmCommentReiteration

#Region "property"

    Public ReadOnly Property CommentReiteration() As String
        Get
            Return Me.INDmeCommet.Text
        End Get
    End Property

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento al guardar la evaluación
    ''' </summary>
    Private Sub INDGuardarBtn_Click(sender As Object, e As EventArgs) Handles INDGuardarBtn.Click
        Me.INDGuardarBtn.Enabled = False
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.INDGuardarBtn.Enabled = True
    End Sub

#End Region

End Class