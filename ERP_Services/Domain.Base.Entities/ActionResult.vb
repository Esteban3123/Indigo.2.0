'***********************************************************************
' Assembly         : Domain.Base
' Author           : Juan Diego Diaz
' Created          : 19-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports System.Runtime.Serialization

''' <summary>
''' Clase encargada de establecer las caracteristicas del resultado de una accion.
''' </summary>
<DataContract(IsReference:=True)>
Public Class ActionResult

    Public Sub New()

    End Sub

    Public Sub New(stateresult As Boolean, message As String)
        Me.StateResult = stateresult
        Me.Message = message
    End Sub

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
    Public Property MessageResult As List(Of String)

    ''' <summary>
    ''' Propiedad que obtiene o asigna el mensaje unido del resultado
    ''' </summary>
    ''' <value>Mensaje del resultado</value>
    ''' <returns>Mensaje del resultado</returns>
    <DataMember()>
    Public Property Message As String

End Class

<DataContract()>
Public Enum eStatusResult
    <EnumMember()>
    SUCCESS
    <EnumMember()>
    WARNING
    <EnumMember()>
    EXCEPTION
End Enum

''' <summary>
''' Clase encargada de establecer las caracteristicas del resultado de una accion encapsulando un objeto de tipo T.
''' </summary>
''' <typeparam name="T">Tipo a encapsular en la respuesta</typeparam>
<DataContract(IsReference:=True)>
Public Class ActionResult(Of T) ' As {Class})

    Public Sub New(stateresult As Boolean, messageresult As List(Of String), message As String, objectresult As T)
        Me.StateResult = stateresult
        Me.Message = message
        Me.MessageResult = messageresult
        Me.ObjectEmbbeded = objectresult
    End Sub

    Public Sub New()

    End Sub

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
    ''' Propiedad que obtiene o asigna el estado de la accion en un proceso con confirmacion
    ''' </summary>
    ''' <value>Valor de la acción</value>
    ''' <returns>Valor de la ación</returns>
    <DataMember()>
    Public Property StateResultAux As Boolean

    ''' <summary>
    ''' Propiedad que obtiene o asigna el mensaje del resultado.
    ''' </summary>
    ''' <value>Mensaje del resultado</value>
    ''' <returns>Mensaje del resultado</returns>
    <DataMember()>
    Public Property MessageResult As List(Of String)

    ''' <summary>
    ''' Propiedad que obtiene o asigna el mensaje unido del resultado
    ''' </summary>
    ''' <value>Mensaje del resultado</value>
    ''' <returns>Mensaje del resultado</returns>
    <DataMember()>
    Public Property Message As String

    ''' <summary>
    ''' Propiedad que obtiene o asigna el mensaje unido del resultado
    ''' </summary>
    ''' <value>Mensaje del resultado</value>
    ''' <returns>Mensaje del resultado</returns>
    <DataMember()>
    Public Property MessageAux As String

    ''' <summary>
    ''' Propiedad que obtiene o asigna el mensaje del resultado.
    ''' </summary>
    ''' <value>Mensaje del resultado</value>
    ''' <returns>Mensaje del resultado</returns>
    <DataMember()>
    Public Property MessageResultAux As List(Of String)

    ''' <summary>
    ''' Propiedad que obtiene o asigna el objeto de tipo T
    ''' </summary>
    ''' <value>Objecto de tipo T</value>
    ''' <returns>El objeto de tipo T</returns>
    <DataMember()>
    Public Property ObjectEmbbeded As T

    <DataMember()>
    Public Property Data As List(Of T)

    ''' <summary>
    ''' Propiedad que obtiene un arreglo de tipo string
    ''' </summary>
    ''' <value>Mensaje del resultado</value>
    ''' <returns>Mensaje del resultado</returns>
    <DataMember()>
    Public Property ListMessageResult As List(Of String())

End Class

''' <summary>
''' Clase encargada de establecer las caracteristicas del resultado de una accion encapsulando un objeto de tipo T para la entidad principal y T2 para una entidad auxiliar a retornar.
''' </summary>
''' <typeparam name="T">Tipo a encapsular en la respuesta</typeparam>
<DataContract(IsReference:=True)>
Public Class ActionResult(Of T As {Class}, T2 As {Class})

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
    Public Property MessageResult As List(Of String)

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

    ''' <summary>
    ''' Propiedad que obtiene o asigna el objeto de tipo T2
    ''' </summary>
    ''' <value>
    ''' The object embbeded aux.
    ''' </value>
    <DataMember()>
    Public Property ObjectEmbbededAux As T2

End Class