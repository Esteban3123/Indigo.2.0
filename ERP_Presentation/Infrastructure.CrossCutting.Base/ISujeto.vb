Imports System.Windows.Forms

Public Interface ISujeto
    Sub Registrar(ByVal objObservador As IObservador)
    Sub Desregistrar(ByVal objObservador As IObservador)
    Sub Notificar(ByVal Mensaje As String, ByVal Tipo As MessageType, Optional form As Form = Nothing)
    Sub NotificationCenter(ByVal Title As String, ByVal Message As String, ByVal TypeMessage As MessageType, ByVal MessageDate As Date, ByVal AditionalMessage As String)
End Interface