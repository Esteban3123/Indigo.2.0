'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Juan Diego Diaz
' Created          : 14-09-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.DocumentalSystem.Entities
#End Region
''' <summary>
''' Modelo de conexion con los servicios distribuidos del sistema documental
''' </summary>
Public Class MDocumentalSystem
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#Region "Methods"

    ''' <summary>
    ''' Funcion para obtener documentos por fullTextIndex
    ''' </summary>
    ''' <param name="searchWord">Texto a buscar</param>
    ''' <returns></returns>
    Public Async Function getDocumentaFullText(ByVal searchWord As String, inMenu As Boolean) As Task(Of List(Of DocumentsStore))
        Dim sessionInfo As New SessionInfo With {.Container = Indigo.DocumentalContainer, .UserId = Indigo.UserIndigo, .aux = searchWord}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.GetDocumentFullTextAsync(sessionInfo, inMenu, 0, 0)
    End Function

    ''' <summary>
    ''' Funcion para obtener documentos según parametros
    ''' </summary>
    ''' <param name="searchWord">Texto a buscar</param>
    ''' <returns></returns>
    Public Async Function getDocuments(ByVal IdForm As String, ByVal IdFileContainer As String, ByVal searchWord As String, fullText As Boolean) As Task(Of List(Of DocumentsStore))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.ListDocumentByIdFormAndIdFileContainerAsync(IdForm, IdFileContainer, searchWord, fullText, Me.Indigo)
    End Function

    ''' <summary>
    ''' Funcion para obtener un stream de un documento
    ''' </summary>
    ''' <param name="Id">Id</param>
    ''' <returns></returns>
    Public Async Function getData(ByVal Id As String) As Task(Of System.IO.Stream)
        Dim sessionInfo As New SessionInfo With {.Container = Indigo.DocumentalContainer, .UserId = Indigo.UserIndigo, .aux = Id}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.GetDocumentAsync(sessionInfo)
    End Function

    ''' <summary>
    ''' Funcion para todos los contenedores de archivos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function getAllFileContainers() As Task(Of List(Of FileContainer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.ListAllFileContainersAsync(Me.Indigo)
    End Function


    ''' <summary>
    ''' Metodo para guardar documento
    ''' </summary>
    Public Async Function saveDocument(length As String, name As String, metaData As String, localFileStream As IO.Stream, attachDate As DateTime, idForm As Integer, idEntity As Integer, idFileContainer As Integer, UserFormMetaData As Boolean) As Task(Of Presentation.CloudAgent.IndigoReference.DocumentalSystem.ResponseDocument)
        Dim sessionInfo As New SessionInfo With {.Container = Indigo.DocumentalContainer, .UserId = Indigo.UserIndigo}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.SaveDocumentAsync(attachDate, length, name, idEntity, idFileContainer, idForm, metaData, sessionInfo, UserFormMetaData, localFileStream)
    End Function

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <returns>DateTime</returns>
    Public Async Function GetServerDate() As Task(Of DateTime)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
