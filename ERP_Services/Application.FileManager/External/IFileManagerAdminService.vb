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
''' Contrato que expone los métodos públicos a los servicios distribuidos
''' </summary>
Public Interface IFileManagerAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Obtiene un token como identificación para la carga
    ''' de un archivo al almacén
    ''' </summary>
    ''' <param name="finfo">Información del archivo</param>
    ''' <returns>Token generado para el archivo a cargar</returns>
    Function GetToken(ByVal finfo As FileInfo) As String

    ''' <summary>
    ''' Carga un archivo por fracciones
    ''' </summary>
    ''' <param name="token">Token del archivo a cargar</param>
    ''' <param name="buffer">Fracción del archivo</param>
    ''' <param name="isEndPiece">Valor que indica si la fracción es la ultima</param>
    ''' <returns>Un valor que indica si se carlo la fracción del archivo</returns>
    Function UploadFile(ByVal token As String, ByVal buffer As Byte(), ByVal isEndPiece As Boolean) As Boolean

    ''' <summary>
    ''' Cancela la carga de un archivo
    ''' </summary>
    ''' <param name="token">Token del archivo a cancelar</param>
    Sub CancelUpload(ByVal token As String)

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

    ''' <summary>
    ''' Obtiene el tamaño del buffer a usar para la carga de fracmentos
    ''' </summary>
    ''' <returns>El tamaño del buffer</returns>
    Function GetBufferSize() As Int64

#End Region

End Interface