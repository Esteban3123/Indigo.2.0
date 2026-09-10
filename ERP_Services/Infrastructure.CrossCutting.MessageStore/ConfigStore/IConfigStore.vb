'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.MessageStore
' Author           : Juan F. Tamayo
' Created          : 2015-10-28
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System

#End Region

''' <summary>
''' Define las carácteristicas y comportamientos de un
''' objeto de configración de almacén
''' </summary>
Public Interface IConfigStore

#Region "Propeties"

    ''' <summary>
    ''' Obtiene el nombre del archivo de almacén
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property NameFileStore As String

    ''' <summary>
    ''' Obtiene la ruta del archivo de almacén
    ''' </summary>
    ''' <returns>La ruta del archivo de almacén</returns>
    ReadOnly Property PathStore As String

    ''' <summary>
    ''' Obtiene la ruta al archivo de almacén
    ''' </summary>
    ''' <returns>La ruta al archivo de almacén</returns>
    ReadOnly Property PathFileStore As String

    ''' <summary>
    ''' Obtiene la extensión del archivo de almacén
    ''' </summary>
    ''' <returns>Extensión del archivo de almacén</returns>
    ReadOnly Property ExtFileStore As String

#End Region

#Region "Methods"

#End Region

End Interface