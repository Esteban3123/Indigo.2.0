'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.MessageStore
' Author           : Juan F. Tamayo
' Created          : 2015-10-28
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Runtime.Serialization

#End Region

''' <summary>
''' Define las carácteristicas y comportamientos de un
''' objeto de mensaje
''' </summary>
Public Interface IMessage
    Inherits ISerializable

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el Id interno que identifica
    ''' el mensaje en el almacén de mensajes.
    ''' Ésta propiedad se llena cuando se lee el mensaje
    ''' del almacén y es de uso interno
    ''' </summary>
    ''' <value>Id interno del mensaje</value>
    ''' <returns>El Id interno del mensaje</returns>
    Property IdFile As Guid

    ''' <summary>
    ''' Obtiene o asigna el titulo del mensaje
    ''' </summary>
    ''' <value>Titulo del mensaje</value>
    ''' <returns>El titulo del mensaje</returns>
    Property Title As String

    ''' <summary>
    ''' Obtiene le hora y fecha en la que fue almacenado el mensaje
    ''' </summary>
    ''' <returns>Fecha y hora de almacenamiento</returns>
    ReadOnly Property TimeStamp As TimeSpan

    ''' <summary>
    ''' Obtiene o asigna el cuerpo del mensaje
    ''' </summary>
    ''' <value>Cuerpo del mensaje</value>
    ''' <returns>El cuerpo del mensaje</returns>
    Property Body As DataSet

    ''' <summary>
    ''' Obtiene o asigna el tamaño del mensaje
    ''' </summary>
    ''' <value>Tamaño del nmensaje</value>
    ''' <returns>El tamaño del mensaje</returns>
    Property Size As UInteger

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el mensaje
    ''' fue leido y ocurrión algún error al procesarlo
    ''' </summary>
    ''' <value>Valor que indica si el mensaje tiene errores</value>
    ''' <returns>Un valor quee indica si el mensaje tiene errores</returns>
    Property HasError As Boolean

    ''' <summary>
    ''' Obtiene o asigna el mensaje del error
    ''' </summary>
    ''' <value>Mensaje del error</value>
    ''' <returns>El mensaje del error</returns>
    Property MessageError As String

#End Region

#Region "Methods"

#End Region

End Interface
