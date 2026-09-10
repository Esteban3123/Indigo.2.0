'***********************************************************************
' Assembly         : Application.FileManager
' Author           : Juan F. Tamayo Puertas
' Created          : 2015-06-30
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.IO
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Implementa los métodos públicos expuestos a los servicios distribuidos
''' </summary>
Public Class FileManagerAdminService
    Implements IFileManagerAdminService

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cancela la carga de un archivo
    ''' </summary>
    ''' <param name="token">Token del archivo a cancelar</param>
    Public Sub CancelUpload(token As String) Implements IFileManagerAdminService.CancelUpload
        FileManager.Instance.CancelUpload(token)
    End Sub

    ''' <summary>
    ''' Copia un archivo a una ruta específica
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <param name="pathTargetFile">Ruta del archivo destino</param>
    ''' <returns>Valor que inidica si el archivo se copió con exito</returns>
    Public Function CopyFile(token As String, pathTargetFile As String) As Boolean Implements IFileManagerAdminService.CopyFile
        Return FileManager.Instance.CopyFile(token, pathTargetFile)
    End Function

    ''' <summary>
    ''' Elimina un archivo del almacén
    ''' </summary>
    ''' <param name="token">Token del archivo a eliminar</param>
    ''' <returns>Valor que indica si se elimino el archvio con exito</returns>
    Public Function DeleteFile(token As String) As Boolean Implements IFileManagerAdminService.DeleteFile
        Return FileManager.Instance.DeleteFile(token)
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si un archivo existe
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <returns>Valor que indica si el archivo existe</returns>
    Public Function ExistsFile(token As String) As Boolean Implements IFileManagerAdminService.ExistsFile
        Return FileManager.Instance.ExistsFile(token)
    End Function

    ''' <summary>
    ''' Obtiene la ruta fisica del archivo
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <returns>La ruta del archivo</returns>
    Public Function GetPathFile(token As String) As String Implements IFileManagerAdminService.GetPathFile
        Return FileManager.Instance.GetPathFile(token)
    End Function

    ''' <summary>
    ''' Obtiene un token como identificación para la carga
    ''' de un archivo al almacén
    ''' </summary>
    ''' <param name="finfo">Información del archivo</param>
    ''' <returns>Token generado para el archivo a cargar</returns>
    Public Function GetToken(finfo As FileInfo) As String Implements IFileManagerAdminService.GetToken
        Return FileManager.Instance.GetToken(finfo)
    End Function

    ''' <summary>
    ''' Carga un archivo por fracciones
    ''' </summary>
    ''' <param name="token">Token del archivo a cargar</param>
    ''' <param name="buffer">Fracción del archivo</param>
    ''' <param name="isEndPiece">Valor que indica si la fracción es la ultima</param>
    ''' <returns>Un valor que indica si se carlo la fracción del archivo</returns>
    Public Function UploadFile(token As String, buffer() As Byte, isEndPiece As Boolean) As Boolean Implements IFileManagerAdminService.UploadFile
        Return FileManager.Instance.UploadFile(token, buffer, isEndPiece)
    End Function

    ''' <summary>
    ''' Obtiene el tamaño del buffer a usar para la carga de fracmentos
    ''' </summary>
    ''' <returns>El tamaño del buffer</returns>
    Public Function GetBufferSize() As Long Implements IFileManagerAdminService.GetBufferSize
        Return Helper.GetBufferSize()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class