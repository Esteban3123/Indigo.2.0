'***********************************************************************
' Assembly         : Domain.Base
' Author           : Crishian Mauricio Salazar
' Created          : 13-11-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports System.Runtime.Serialization

''' <summary>
''' Clase utilizada para representar un menssaje y sus parametros
''' </summary>
''' <remarks></remarks>
<DataContract(IsReference:=True)> _
Public Class MessageResult

    ''' <summary>
    ''' Constructor el cual recibe el codigo del mensaje y los parametros de dicho mensaje
    ''' </summary>
    ''' <param name="codeMessage">Codigo Mensaje</param>
    ''' <param name="parameters">Parametros</param>
    ''' <remarks></remarks>
    Sub New(codeMessage As String, ByVal ParamArray parameters() As String)
        Me.CodeMessage = codeMessage
        Me.Parameters = parameters
    End Sub


    ''' <summary>
    ''' Propiedad que representa el codigo del mensaje
    ''' </summary>
    ''' <value>Codigo Mensaje</value>
    ''' <returns>Codigo Mensaje</returns>
    ''' <remarks></remarks>
    <DataMember()> _
    Public Property CodeMessage As String

    ''' <summary>
    ''' Propiedad que representa los parametros del mensaje
    ''' </summary>
    ''' <value>Parametros del mensaje</value>
    ''' <returns>Parametros del mensaje</returns>
    ''' <remarks></remarks>
    <DataMember()> _
    Public Property Parameters As String()




End Class
