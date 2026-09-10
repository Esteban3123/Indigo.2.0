'***********************************************************************
' Assembly         : Application.FileManager
' Author           : Juan F. Tamayo Puertas
' Created          : 2015-06-30
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Configuration

#End Region

''' <summary>
''' Clase de ayuda que contine métodos utilidad para la generación y obtención
''' de configuración en el el administrador de archivos
''' </summary>
Public Class Helper

#Region "Methods"

    ''' <summary>
    ''' Obtiene la ruta de archivos temporales configurada en el Web.Config
    ''' </summary>
    ''' <returns>Ruta de archivos temporales</returns>
    Public Shared Function GetFileStoreFolder() As String
        Try
            Dim conf = ConfigurationManager.AppSettings("FileManager_FileStoreFolder")
            If conf IsNot Nothing Then
                Return conf.ToString().Trim()
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el tiempo en segundos en que expira un archivo luego de haberse cargado
    ''' al almacén de archivos. Éste tiempo se verifica con respecto a la fecha de última
    ''' modificación
    ''' </summary>
    ''' <returns>El tiempo en que expira un archivo luego de ser cargado</returns>
    Public Shared Function GetFileExpirationTime() As Int64
        Try
            Dim conf = ConfigurationManager.AppSettings("FileManager_FileExpirationTime")
            If conf IsNot Nothing Then
                Return CLng(conf.ToString().Trim())
            Else
                Return 60
            End If
        Catch ex As Exception
            Return 60
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el tiempo de espera en segundos para la carga de un archivo luego de enviar
    ''' el primer fracmento. Éste tiempo se actualiza con cada carga de fracmentos en memoria.
    ''' </summary>
    ''' <returns>El tiempo de espera en segundos para la carga de un archivo</returns>
    Public Shared Function GetTimeoutToUploadFiles() As Int64
        Try
            Dim conf = ConfigurationManager.AppSettings("FileManager_TimeoutToUploadFiles")
            If conf IsNot Nothing Then
                Return CLng(conf.ToString().Trim())
            Else
                Return 60
            End If
        Catch ex As Exception
            Return 60
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el tamaño del buffer usado para la carga de fracmentos
    ''' </summary>
    ''' <returns>El tamaño del buffer</returns>
    Public Shared Function GetBufferSize() As Int64
        Try
            Dim conf = ConfigurationManager.AppSettings("FileManager_BufferSize")
            If conf IsNot Nothing Then
                Return CLng(conf.ToString().Trim())
            Else
                Return 10240 '10KB
            End If
        Catch ex As Exception
            Return 10240 '10KB
        End Try
    End Function

#End Region

End Class
