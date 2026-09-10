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
''' Clase para transportar información basica 
''' de sesión
''' </summary>
<DataContract()>
Public Class SessionInfo
    <DataMember()> Public UserId As String
    <DataMember()> Public Container As String
    <DataMember()> Public aux As String
End Class
