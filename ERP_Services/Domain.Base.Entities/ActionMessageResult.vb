'***********************************************************************
' Assembly         : Domain.Base
' Author           : Cristhian Mauricio Salazar
' Created          : 13-11-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports System.Runtime.Serialization

''' <summary>
''' Clase encargada de establecer las caracteristicas del resultado de una accion.
''' </summary>
<DataContract(IsReference:=True)>
Public Class ActionMessageResult

    ''' <summary>
    ''' Propiedad que obtiene o asigna el estado de la acción.
    ''' </summary>
    ''' <value>Valor de la acción</value>
    ''' <returns>Valor de la ación</returns>
    <DataMember()>
    Public Property StateResult As Boolean

    ''' <summary>
    ''' Gets or sets the status code.
    ''' </summary>
    <DataMember()>
    Public Property StatusCode As eStatusResult

    ''' <summary>
    ''' Propiedad que obtiene o asigna el mensaje del resultado.
    ''' </summary>
    ''' <value>Mensaje del resultado</value>
    ''' <returns>Mensaje del resultado</returns>
    <DataMember()>
    Public Property MessageResult As List(Of MessageResult)

    ''' <summary>
    ''' Propiedad que obtiene o asigna el mensaje unido del resultado
    ''' </summary>
    ''' <value>Mensaje del resultado</value>
    ''' <returns>Mensaje del resultado</returns>
    <DataMember()>
    Public Property Message As String

End Class

''' <summary>
''' Clase encargada de establecer las caracteristicas del resultado de una accion encapsulando un objeto de tipo T.
''' </summary>
''' <typeparam name="T">Tipo a encapsular en la respuesta</typeparam>
<DataContract(IsReference:=True)>
Public Class ActionMessageResult(Of T As {Class})

    ''' <summary>
    ''' Propiedad que obtiene o asigna el estado de la acción.
    ''' </summary>
    ''' <value>Valor de la acción</value>
    ''' <returns>Valor de la ación</returns>
    <DataMember()>
    Public Property StateResult As Boolean

    ''' <summary>
    ''' Gets or sets the status code.
    ''' </summary>
    <DataMember()>
    Public Property StatusCode As eStatusResult

    Private _messageResult As New List(Of MessageResult)()
    ''' <summary>
    ''' Propiedad que obtiene o asigna el mensaje del resultado.
    ''' </summary>
    ''' <value>Mensaje del resultado</value>
    ''' <returns>Mensaje del resultado</returns>
    <DataMember()>
    Public Property MessageResult As List(Of MessageResult)
        Get
            Return _messageResult
        End Get
        Set(value As List(Of MessageResult))
            _messageResult = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o asigna el mensaje unido del resultado
    ''' </summary>
    ''' <value>Mensaje del resultado</value>
    ''' <returns>Mensaje del resultado</returns>
    <DataMember()>
    Public Property Message As String

    ''' <summary>
    ''' Propiedad que obtiene o asigna el objeto de tipo T
    ''' </summary>
    ''' <value>Objecto de tipo T</value>
    ''' <returns>El objeto de tipo T</returns>
    <DataMember()>
    Public Property ObjectEmbbeded As T

End Class
