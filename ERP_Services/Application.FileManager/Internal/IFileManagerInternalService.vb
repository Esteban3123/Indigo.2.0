'***********************************************************************
' Assembly         : Application.FileManager
' Author           : Juan F. Tamayo Puertas
' Created          : 2015-06-30
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.IO

#End Region

''' <summary>
''' Contrato que expone los métodos privados usados sólo 
''' por las demás capas de aplicación en la solución
''' </summary>
Public Interface IFileManagerInternalService

#Region "Methods"

    ''' <summary>
    ''' Obtiene la ruta fisica del archivo
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <returns>La ruta del archivo</returns>
    Function GetPathFile(ByVal token As String) As String

    ''' <summary>
    ''' Obtiene un valor que indica si un archivo existe
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <returns>Valor que indica si el archivo existe</returns>
    Function ExistsFile(ByVal token As String) As Boolean

    ''' <summary>
    ''' Copia un archivo a una ruta específica
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <param name="pathTargetFile">Ruta del archivo destino</param>
    ''' <returns>Valor que inidica si el archivo se copió con exito</returns>
    Function CopyFile(ByVal token As String, ByVal pathTargetFile As String) As Boolean

    ''' <summary>
    ''' Elimina un archivo del almacén
    ''' </summary>
    ''' <param name="token">Token del archivo a eliminar</param>
    ''' <returns>Valor que indica si se elimino el archvio con exito</returns>
    Function DeleteFile(ByVal token As String) As Boolean

#End Region

End Interface