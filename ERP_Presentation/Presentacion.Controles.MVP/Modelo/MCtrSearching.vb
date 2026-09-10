'***********************************************************************
' Assembly         : Presentacion.Controls.MVP
' Author           : Juan F. Tamayo
' Created          : 2013-10-28
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Base
Imports Domain.Base.Entities
Imports System.Threading.Tasks
Imports Domain.DocumentalSystem.Entities

#End Region

''' <summary>
''' Encapsula los metodos del servicio de búsqueda e indexación
''' </summary>
Public Class MCtrSearching
    Implements IDisposable

#Region "Methods"

    ''' <summary>
    ''' Obtiene la fecha y hora del servidor
    ''' </summary>
    ''' <returns>Fecha y hora del servidor</returns>
    Public Async Function GetDateServer() As Threading.Tasks.Task(Of TimeSpan)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoIndexing.GetServerDateAsync()
    End Function

    ''' <summary>
    ''' Gestiona la indexación de documentos asínconamente
    ''' </summary>
    ''' <param name="dataBase">Nombre de la base de datos en la que se realiza la indexación</param>
    ''' <param name="doc">Documento a indexar</param>
    Public Async Function IndexingDocument(ByVal dataBase As String, ByVal doc As IndexedDocument2) As Threading.Tasks.Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoIndexing.IndexingDocumentAsync(dataBase, doc)
    End Function

    ''' <summary>
    ''' Realiza la busqueda de documentos indexados
    ''' </summary>
    ''' <param name="dataBase">Base de datos en la cual se realiza la busqueda</param>
    ''' <param name="text">Texto usado como patrón de busqueda</param>
    ''' <param name="initDate">Fecha inicial de indexación</param>
    ''' <param name="finDate">Fecha final de indexación</param>
    ''' <param name="skip">Cantidad de ocurrencias a ignorar</param>
    ''' <param name="top">Cantitdad de ocurrencias a tomar</param>
    ''' <param name="asc">Valor que indica si se ordena los resultados ascendentemente</param>
    ''' <returns>Una lista de documentos indexados</returns>
    Public Async Function SearchDocument(ByVal dataBase As String, ByVal text As String, ByVal initDate As DateTime?, ByVal finDate As DateTime?, ByVal skip As Long, ByVal top As Long, ByVal asc As Boolean) As Threading.Tasks.Task(Of IndexedDocumentResultSet2)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoIndexing.SearchDocumentAsync(dataBase, text, initDate, finDate, skip, top, asc)
    End Function

    ''' <summary>
    ''' Funcion para obtener un stream de un documento
    ''' </summary>
    ''' <param name="Id">Id</param>
    ''' <returns></returns>
    Public Async Function getData(ByVal Id As String) As Task(Of System.IO.Stream)
        Dim sessionInfo As New SessionInfo With {.Container = SessionValues.Instance.DocumentalContainer, .UserId = SessionValues.Instance.UserIndigo, .aux = Id}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.GetDocumentAsync(sessionInfo)
    End Function

    ''' <summary>
    ''' Funcion para obtener documentos por fullTextIndex
    ''' </summary>
    ''' <param name="searchWord">Texto a buscar</param>
    ''' <returns></returns>
    Public Async Function getDocumentaFullText(ByVal searchWord As String, ByVal inMenu As Boolean, ByVal skip As Int64, ByVal top As Int64) As Task(Of List(Of DocumentsStore))
        Dim sessionInfo As New SessionInfo With {.Container = SessionValues.Instance.DocumentalContainer, .UserId = SessionValues.Instance.UserIndigo, .aux = searchWord}
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoDocumentalSystem.GetDocumentFullTextAsync(sessionInfo, inMenu, skip, top)
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
