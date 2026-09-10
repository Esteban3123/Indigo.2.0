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
''' Clase Información documento
''' </summary>
<MessageContract()>
Public Class DocumentInfo
    <MessageBodyMember()> Public DocumentStream As System.IO.Stream
    <MessageHeader()> Public FileName As String
    <MessageHeader()> Public DocumentStreamLength As Long
    <MessageHeader()> Public IdFileContainer As Integer
    <MessageHeader()> Public IdEntity As Integer
    <MessageHeader()> Public IdForm As Integer
    <MessageHeader()> Public MetaData As String
    <MessageHeader()> Public UseFormMetaData As Boolean
    <MessageHeader()> Public AttachDate As DateTime
    <MessageHeader()> Public SessionInf As SessionInfo
End Class