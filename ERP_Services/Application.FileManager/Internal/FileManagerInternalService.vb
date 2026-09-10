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
''' Implementa los métodos privados usados por las demás capas de aplicación
''' </summary>
Public Class FileManagerInternalService
    Implements IFileManagerInternalService

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Copia un archivo a una ruta específica
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <param name="pathTargetFile">Ruta del archivo destino</param>
    ''' <returns>Valor que inidica si el archivo se copió con exito</returns>
    Public Function CopyFile(token As String, pathTargetFile As String) As Boolean Implements IFileManagerInternalService.CopyFile
        Return FileManager.Instance.CopyFile(token, pathTargetFile)
    End Function

    ''' <summary>
    ''' Elimina un archivo del almacén
    ''' </summary>
    ''' <param name="token">Token del archivo a eliminar</param>
    ''' <returns>Valor que indica si se elimino el archvio con exito</returns>
    Public Function DeleteFile(token As String) As Boolean Implements IFileManagerInternalService.DeleteFile
        Return FileManager.Instance.DeleteFile(token)
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si un archivo existe
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <returns>Valor que indica si el archivo existe</returns>
    Public Function ExistsFile(token As String) As Boolean Implements IFileManagerInternalService.ExistsFile
        Return FileManager.Instance.ExistsFile(token)
    End Function

    ''' <summary>
    ''' Obtiene la ruta fisica del archivo
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <returns>La ruta del archivo</returns>
    Public Function GetPathFile(token As String) As String Implements IFileManagerInternalService.GetPathFile
        Return FileManager.Instance.GetPathFile(token)
    End Function

#End Region

End Class
