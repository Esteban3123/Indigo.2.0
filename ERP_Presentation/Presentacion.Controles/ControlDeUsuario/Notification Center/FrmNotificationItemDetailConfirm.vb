Public Class FrmNotificationItemDetailConfirm

#Region "Propeties"
    ''' <summary>
    ''' Evento que se llama al dar click en el boton aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AcceptMessage(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Permite saber si fue cerrado con aceptar o con cancelar
    ''' </summary>
    Public IsClosedWithAcept As Boolean = False
#End Region

#Region "Methods"
    Private Sub BtnAccept_Click(sender As Object, e As EventArgs) Handles BtnAccept.Click
        IsClosedWithAcept = True
        RaiseEvent AcceptMessage(Nothing, EventArgs.Empty)
        Me.Close()
    End Sub
#End Region

End Class