'***********************************************************************
' Assembly         : Distributed.FileManager
' Author           : Juan F. Tamayo Puertas
' Created          : 2015-06-30
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.FileManager
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

''' <summary>
''' Servicio de administración de archivos
''' </summary>
'<UnityInstanceProviderServiceBehavior()> _
<UnityMessageInspectorServiceBehavior()>
<ServiceBehavior(InstanceContextMode:=InstanceContextMode.PerCall)>
Public Class FileManagerService
    Implements IFileManagerService

#Region "Fileds"

    ''' <summary>
    ''' Servicio de aplicación de administración de archivos
    ''' </summary>
    Private _fileManagerAdminService As IFileManagerAdminService

#End Region

    '#Region "Builders"

    '    ''' <summary>
    '    ''' Inicializa una nueva instancia de la clase
    '    ''' </summary>
    '    ''' <param name="fileManagerAdminService">Servicio de aplicación del administrador de archivos</param>
    '    Public Sub New(ByVal fileManagerAdminService As IFileManagerAdminService)
    '        If fileManagerAdminService Is Nothing Then
    '            Throw New ArgumentNullException("fileManagerAdminService")
    '        End If
    '        Me._fileManagerAdminService = fileManagerAdminService
    '    End Sub

    '#End Region

#Region "Methods"

    ''' <summary>
    ''' Cancela la carga de un archivo
    ''' </summary>
    ''' <param name="token">Token del archivo a cancelar</param>
    Public Sub CancelUpload(token As String) Implements IFileManagerService.CancelUpload
        Using service As IFileManagerAdminService = Container.Current.Resolve(Of IFileManagerAdminService)()
            service.CancelUpload(token)
        End Using
        'Me._fileManagerAdminService.CancelUpload(token)
    End Sub

    ''' <summary>
    ''' Copia un archivo a una ruta específica
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <param name="pathTargetFile">Ruta del archivo destino</param>
    ''' <returns>Valor que inidica si el archivo se copió con exito</returns>
    Public Function CopyFile(token As String, pathTargetFile As String) As Boolean Implements IFileManagerService.CopyFile
        Using service As IFileManagerAdminService = Container.Current.Resolve(Of IFileManagerAdminService)()
            Return service.CopyFile(token, pathTargetFile)
        End Using
        'Return Me._fileManagerAdminService.CopyFile(token, pathTargetFile)
    End Function

    ''' <summary>
    ''' Elimina un archivo del almacén
    ''' </summary>
    ''' <param name="token">Token del archivo a eliminar</param>
    ''' <returns>Valor que indica si se elimino el archvio con exito</returns>
    Public Function DeleteFile(token As String) As Boolean Implements IFileManagerService.DeleteFile
        Using service As IFileManagerAdminService = Container.Current.Resolve(Of IFileManagerAdminService)()
            Return service.DeleteFile(token)
        End Using
        'Return Me._fileManagerAdminService.DeleteFile(token)
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si un archivo existe
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <returns>Valor que indica si el archivo existe</returns>
    Public Function ExistsFile(token As String) As Boolean Implements IFileManagerService.ExistsFile
        Using service As IFileManagerAdminService = Container.Current.Resolve(Of IFileManagerAdminService)()
            Return service.ExistsFile(token)
        End Using
        'Return Me._fileManagerAdminService.ExistsFile(token)
    End Function

    ''' <summary>
    ''' Obtiene la ruta fisica del archivo
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <returns>La ruta del archivo</returns>
    Public Function GetPathFile(token As String) As String Implements IFileManagerService.GetPathFile
        Using service As IFileManagerAdminService = Container.Current.Resolve(Of IFileManagerAdminService)()
            Return service.GetPathFile(token)
        End Using
        'Return Me._fileManagerAdminService.GetPathFile(token)
    End Function

    ''' <summary>
    ''' Obtiene un token como identificación para la carga
    ''' de un archivo al almacén
    ''' </summary>
    ''' <param name="finfo">Información del archivo</param>
    ''' <returns>Token generado para el archivo a cargar</returns>
    Public Function GetToken(finfo As IO.FileInfo) As String Implements IFileManagerService.GetToken
        Using service As IFileManagerAdminService = Container.Current.Resolve(Of IFileManagerAdminService)()
            Return service.GetToken(finfo)
        End Using
        'Return Me._fileManagerAdminService.GetToken(finfo)
    End Function

    ''' <summary>
    ''' Carga un archivo por fracciones
    ''' </summary>
    ''' <param name="token">Token del archivo a cargar</param>
    ''' <param name="buffer">Fracción del archivo</param>
    ''' <param name="isEndPiece">Valor que indica si la fracción es la ultima</param>
    ''' <returns>Un valor que indica si se carlo la fracción del archivo</returns>
    Public Function UploadFile(token As String, buffer() As Byte, isEndPiece As Boolean) As Boolean Implements IFileManagerService.UploadFile
        Using service As IFileManagerAdminService = Container.Current.Resolve(Of IFileManagerAdminService)()
            Return service.UploadFile(token, buffer, isEndPiece)
        End Using
        'Return Me._fileManagerAdminService.UploadFile(token, buffer, isEndPiece)
    End Function

    ''' <summary>
    ''' Obtiene el tamaño del buffer a usar para la carga de fracmentos
    ''' </summary>
    ''' <returns>El tamaño del buffer</returns>
    Public Function GetBufferSize() As Long Implements IFileManagerService.GetBufferSize
        Using service As IFileManagerAdminService = Container.Current.Resolve(Of IFileManagerAdminService)()
            Return service.GetBufferSize()
        End Using
        'Return Me._fileManagerAdminService.GetBufferSize()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _fileManagerAdminService.Dispose()
            End If
            _fileManagerAdminService = Nothing
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