'************************************************************
' Assembly         : Domain.DocumentalRepository.Entities
' Author           : Juan Diego Diaz
' Created          : 21-09-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports System.ServiceModel
Imports System.Runtime.Serialization

''' <summary>
''' Clase para retornar respuestas
''' </summary>
<MessageContract()>
Public Class ResponseDocument

    <MessageBodyMember()> Public response As Boolean
    <MessageHeader()> Public documentStore As DocumentsStore
    <MessageHeader()> Public message As String

End Class
