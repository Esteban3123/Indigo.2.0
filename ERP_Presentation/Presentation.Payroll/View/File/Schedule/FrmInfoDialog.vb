''' <summary>
''' Clase que contiene la vista de los mensajes de los dias que no se registraron
''' </summary>
''' <remarks></remarks>
Public Class FrmInfoDialog

#Region "Properties"
    ''' <summary>
    ''' Establece el datasource de la rejilla de los mensajes 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property InfoDialogDatasource As List(Of DaysNotsave)
        Set(value As List(Of DaysNotsave))
            INDgcInfoDialog.DataSource = value
        End Set
    End Property
#End Region

#Region "Events"
    ''' <summary>
    ''' Evento Click para cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnOK_Click(sender As Object, e As EventArgs) Handles INDBtnOK.Click
        Me.Close()
    End Sub
#End Region

End Class