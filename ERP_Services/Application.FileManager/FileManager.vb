'***********************************************************************
' Assembly         : Application.FileManager
' Author           : Juan F. Tamayo Puertas
' Created          : 2015-06-30
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Configuration
Imports System.Collections.Concurrent
Imports System.Text
Imports System.IO
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Administra los archivos cargados al sistema, liberando memoria automáticamente
''' cuando el archivo a caducado según el tiempo de expiración configurado en el
''' Web.Config
''' </summary>
Public Class FileManager

#Region "Singleton"

    ''' <summary>
    ''' Unica instancia a la clase de administración de archivos
    ''' </summary>
    Private Shared _instance As FileManager

    ''' <summary>
    ''' Obtiene la única instancia de la clase administradora de archivos
    ''' </summary>
    ''' <returns>La única instancia de la clase</returns>
    Public Shared ReadOnly Property Instance As FileManager
        Get
            If _instance Is Nothing Then
                _instance = New FileManager()
            End If
            Return _instance
        End Get
    End Property

#End Region

#Region "Fields"

    ''' <summary>
    ''' Diccionario concurrente donde se administran los archivos cargados
    ''' </summary>
    Private _files As ConcurrentDictionary(Of String, Tuple(Of FileInfo, TimeSpan, FileStream))

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa la única instancia de la clase
    ''' </summary>
    Private Sub New()
        Me._files = New ConcurrentDictionary(Of String, Tuple(Of FileInfo, TimeSpan, FileStream))()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un token como identificación para la carga
    ''' de un archivo al almacén
    ''' </summary>
    ''' <param name="finfo">Información del archivo</param>
    ''' <returns>Token generado para el archivo a cargar</returns>
    Public Function GetToken(ByVal finfo As FileInfo) As String
        Try
            Me.RecycleFiles()
            Dim token As String = Utils.MD5(finfo.FullName & DateTime.Now.Ticks)
            If Not Me._files.ContainsKey(token) Then
                Me._files.TryAdd(token, New Tuple(Of FileInfo, TimeSpan, FileStream)(finfo, New TimeSpan(DateTime.Now.Ticks), New FileStream(Path.Combine(Helper.GetFileStoreFolder(), token & finfo.Extension & ".part"), FileMode.Create)))
            End If
            Return token
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Carga un archivo por fracciones
    ''' </summary>
    ''' <param name="token">Token del archivo a cargar</param>
    ''' <param name="buffer">Fracción del archivo</param>
    ''' <param name="isEndPiece">Valor que indica si la fracción es la ultima</param>
    ''' <returns>Un valor que indica si se carlo la fracción del archivo</returns>
    Public Function UploadFile(ByVal token As String, ByVal buffer As Byte(), ByVal isEndPiece As Boolean) As Boolean
        Try
            If Me._files.ContainsKey(token) Then
                Me._files(token).Item3.Write(buffer, 0, buffer.Length)
                Me._files(token).Item3.Flush()
                Me._files(token).Item2.Add(New TimeSpan(DateTime.Now.Ticks))
                If isEndPiece Then
                    If Directory.GetFiles(Helper.GetFileStoreFolder()).Any(Function(f) f.Substring(f.LastIndexOf("\") + 1).StartsWith(token) AndAlso f.EndsWith(Me._files(token).Item1.Extension)) Then
                        Dim fil As String = Directory.GetFiles(Helper.GetFileStoreFolder()).Where(Function(f) f.Substring(f.LastIndexOf("\") + 1).StartsWith(token)).SingleOrDefault()
                        File.Delete(fil)
                    End If
                    Me._files(token).Item3.Close()
                    File.Move(Me._files(token).Item3.Name, Path.Combine(Helper.GetFileStoreFolder(), token & Me._files(token).Item1.Extension))
                    Dim aux As Tuple(Of FileInfo, TimeSpan, FileStream) = Nothing
                    Me._files.TryRemove(token, aux)
                    aux = Nothing
                End If
                Return True
            End If
            Return False
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Cancela la carga de un archivo
    ''' </summary>
    ''' <param name="token">Token del archivo a cancelar</param>
    Public Sub CancelUpload(ByVal token As String)
        Try
            If Me._files.ContainsKey(token) Then
                Me._files(token).Item3.Flush(True)
                Me._files(token).Item3.Close()
                Dim aux As Tuple(Of FileInfo, TimeSpan, FileStream) = Nothing
                Me._files.TryRemove(token, aux)
                aux = Nothing
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene la ruta fisica del archivo
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <returns>La ruta del archivo</returns>
    Public Function GetPathFile(ByVal token As String) As String
        Try
            If Me.ExistsFile(token) Then
                Dim fil As String = Directory.GetFiles(Helper.GetFileStoreFolder()).Where(Function(f) f.Substring(f.LastIndexOf("\") + 1).StartsWith(token)).SingleOrDefault()
                Return fil
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si un archivo existe
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <returns>Valor que indica si el archivo existe</returns>
    Public Function ExistsFile(ByVal token As String) As Boolean
        Try
            If Directory.GetFiles(Helper.GetFileStoreFolder()).Any(Function(f) f.Substring(f.LastIndexOf("\") + 1).StartsWith(token)) Then
                Return True
            End If
            Return False
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Copia un archivo a una ruta específica
    ''' </summary>
    ''' <param name="token">Token del archivo</param>
    ''' <param name="pathTargetFile">Ruta del archivo destino</param>
    ''' <returns>Valor que inidica si el archivo se copió con exito</returns>
    Public Function CopyFile(ByVal token As String, ByVal pathTargetFile As String) As Boolean
        Try
            If Directory.GetFiles(Helper.GetFileStoreFolder()).Any(Function(f) f.Substring(f.LastIndexOf("\") + 1).StartsWith(token)) Then
                Dim fil As String = Directory.GetFiles(Helper.GetFileStoreFolder()).Where(Function(f) f.Substring(f.LastIndexOf("\") + 1).StartsWith(token)).SingleOrDefault()
                File.Copy(fil, pathTargetFile)
                Return True
            End If
            Return False
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Elimina un archivo del almacén
    ''' </summary>
    ''' <param name="token">Token del archivo a eliminar</param>
    ''' <returns>Valor que indica si se elimino el archvio con exito</returns>
    Public Function DeleteFile(ByVal token As String) As Boolean
        Try
            If Directory.GetFiles(Helper.GetFileStoreFolder()).Any(Function(f) f.Substring(f.LastIndexOf("\") + 1).StartsWith(token)) Then
                Dim fil As String = Directory.GetFiles(Helper.GetFileStoreFolder()).Where(Function(f) f.Substring(f.LastIndexOf("\") + 1).StartsWith(token)).SingleOrDefault()
                File.Delete(fil)
            End If
            If Me._files.ContainsKey(token) Then
                Dim aux As Tuple(Of FileInfo, TimeSpan, FileStream) = Nothing
                Me._files.TryRemove(token, aux)
                aux = Nothing
            End If
            Return True
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Se encarga de reciclar la memoria y el disco usados en la carga de archivos, eliminando
    ''' los fracmentos huerfanos que superen el tiempo configurado, así como los archivos
    ''' cargados donde su fecha de última modificación superen el tiempo configurado.
    ''' </summary>
    Private Sub RecycleFiles()
        Dim aux2 As Tuple(Of FileInfo, TimeSpan, FileStream) = Nothing
        'Recorremos los tokens y verificamos
        'su marca de tiempo, eliminando los que
        'superen el tiempo configurado en el Web.Config
        For Each kv In Me._files
            Dim t As New TimeSpan(DateTime.Now.Ticks)
            If t.Subtract(kv.Value.Item2).TotalSeconds > Helper.GetTimeoutToUploadFiles() Then
                Me._files.TryRemove(kv.Key, aux2)
            End If
        Next

        'Recorremos todos los archivos de la carpeta compartida
        'y verificamos la fecha de ultima modificación, eliminando
        'los que superen el tiempo configurado en el Web.Config
        If Directory.Exists(Helper.GetFileStoreFolder()) Then
            For Each d In Directory.GetFiles(Helper.GetFileStoreFolder())
                Try
                    Dim finfo As DateTime = File.GetLastWriteTime(d)
                    Dim t As New TimeSpan(DateTime.Now.Ticks)
                    If t.Subtract(New TimeSpan(finfo.Ticks)).TotalSeconds > Helper.GetFileExpirationTime() Then
                        File.Delete(d)
                    End If
                Catch ex As Exception
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                End Try
            Next
        End If
    End Sub

#End Region

End Class