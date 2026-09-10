'***********************************************************************
' Assembly         : DistributedService.Indexing
' Author           : Juan F. Tamayo
' Created          : 2013-10-08
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-10-08
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Configuration
Imports Application.Base
Imports DistributedServices.Indexing
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Servicio que gestiona la indexación y busqueda de documentos
''' </summary>
Public Class Indexing
    Implements IIndexing

    Private Shared service As IndexingService

    Shared Sub New()
        If ConfigurationManager.AppSettings("IndexingEnabled") IsNot Nothing AndAlso Not ConfigurationManager.AppSettings("IndexingEnabled").ToString().Trim().Equals(String.Empty) AndAlso ConfigurationManager.AppSettings("IndexingEnabled").ToString().Trim().ToLower().Equals("true") AndAlso ConfigurationManager.AppSettings("VituelHost") IsNot Nothing AndAlso ConfigurationManager.AppSettings("VituelPort") IsNot Nothing Then
            Dim port As Int32 = 27017
            Int32.TryParse(ConfigurationManager.AppSettings("VituelPort").ToString().Trim(), port)
            service = New IndexingService(ConfigurationManager.AppSettings("VituelHost").ToString(), If(port <= 0, 27017, port))
        End If
    End Sub


    ''' <summary>
    ''' Gestiona la indexación de documentos asínconamente
    ''' </summary>
    ''' <param name="containerV12">Nombre del contenedor V12</param>
    ''' <param name="doc">Documento a indexar</param>
    Public Function IndexingDocument(containerV12 As String, doc As Domain.Base.Entities.IndexedDocument2) As ActionResult Implements IIndexing.IndexingDocument
        If service IsNot Nothing Then
            service.IndexDocumentAsync(containerV12.Trim(), doc)
            Return New ActionResult With {.StateResult = True, .MessageResult = New List(Of String)()}
        Else
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)()}
        End If
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
    Public Function SearchDocument(dataBase As String, text As String, initDate As DateTime?, finDate As DateTime?, skip As Long, top As Long, asc As Boolean) As IndexedDocumentResultSet2 Implements IIndexing.SearchDocument
        If service IsNot Nothing Then
            Return service.SearchDocument(dataBase.Trim(), text.Trim(), initDate, finDate, skip, top, asc)
        Else
            Return New IndexedDocumentResultSet2()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la fecha y hora del servidor
    ''' </summary>
    ''' <returns>Fecha y hora del servidor</returns>
    Public Function GetServerDate() As TimeSpan Implements IIndexing.GetServerDate
        Return TimeSpan.FromTicks(DateTime.Now.Ticks)
    End Function

    ''' <summary>
    ''' Realiza la eliminación de un documento indexado
    ''' </summary>
    ''' <param name="dataBase">Base de datos en la cual se realiza la busqueda y eliminación</param>
    ''' <param name="doc">Documento a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function DeleteDocument(dataBase As String, doc As IndexedDocument2) As ActionResult Implements IIndexing.DeleteDocument
        If doc Is Nothing Then
            Return New ActionResult With {.StateResult = True, .MessageResult = New List(Of String)()}
        End If
        If service IsNot Nothing Then
            Dim res = service.DeleteDocument(dataBase.Trim(), doc)
            Return New ActionResult With {.StateResult = res, .MessageResult = New List(Of String)()}
        Else
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)()}
        End If
    End Function

    ''' <summary>
    ''' Obtiene un documento por su Id
    ''' </summary>
    ''' <param name="dataBase">Base de datos a consultar</param>
    ''' <param name="id">Id del documento</param>
    ''' <returns>Documento obtenido</returns>
    Public Function GetDocument(ByVal dataBase As String, id As MongoDB.Bson.ObjectId) As IndexedDocument2 Implements IIndexing.GetDocument
        'If service IsNot Nothing Then
        '    Return service.GetDocument(dataBase.Trim(), Text.Trim(), initDate, finDate, skip, top, Asc)
        'Else
        '    Return New IndexedDocumentResultSet2()
        'End If
    End Function

    Public Function DeleteDocumentByIdEntity(dataBase As String, IdEntity As String) As ActionResult Implements IIndexing.DeleteDocumentByIdEntity
        If IdEntity Is Nothing Then
            Return New ActionResult With {.StateResult = True, .MessageResult = New List(Of String)()}
        End If
        If service IsNot Nothing Then
            Dim doc = service.SearchDocument(dataBase.Trim(), IdEntity, Nothing, Nothing, 0, 1)
            Dim res = service.DeleteDocument(dataBase.Trim(), doc.Results(0))
            Return New ActionResult With {.StateResult = res, .MessageResult = New List(Of String)()}
        Else
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)()}
        End If
    End Function
End Class
