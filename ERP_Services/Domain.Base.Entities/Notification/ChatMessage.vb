''' <summary>
''' Encapsula los datos de un mensaje
''' </summary>
Public Class ChatMessage

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el id del mensaje
    ''' </summary>
    ''' <value>Id del mensaje</value>
    ''' <returns>El id del mensaje</returns>
    Public Property Id As String

    ''' <summary>
    ''' Obtiene o asigna el texto del mensaje
    ''' </summary>
    ''' <value>Texto del mensaje</value>
    ''' <returns>El texto del mensaje</returns>
    Public Property Text As String

    ''' <summary>
    ''' Valor que indica si el mensaje ha sido chequeado por el destinatario
    ''' </summary>
    ''' <value></value>
    ''' <returns>Valor que indica si el mensaje ha sido chequeado</returns>
    ''' <remarks>Un valor que indica si el mensaje ha sido chequeado</remarks>
    Public Property Checked As Boolean


    ''' <summary>
    ''' Obtiene o establece la fecha y hora de envio del mensaje.
    ''' </summary>
    ''' <value>
    ''' la fecha hora.
    ''' </value>
    Public Property [Date] As DateTime

#End Region

End Class