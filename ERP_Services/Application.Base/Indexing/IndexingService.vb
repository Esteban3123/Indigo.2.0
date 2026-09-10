'***********************************************************************
' Assembly         : Application.Base
' Author           : Juan F. Tamayo
' Created          : 2013-10-08
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-10-08
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base.Entities
Imports Futesh.Enterprise.MongoDb
'Imports Infrastructure.CrossCutting.Msmq
Imports Infrastructure.CrossCutting.Exceptions

#End Region

''' <summary>
''' Servicio encargado del envío asíncrono de documentos
''' a indexar hacia MSMQ
''' </summary>
Public NotInheritable Class IndexingService

#Region "Fields"
    ''' <summary>
    ''' Cliente de conexión a V12
    ''' </summary>
    Private _clientMongoDb As Futesh.Enterprise.MongoDb.Client

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal host As String, ByVal port As Int32, Optional ByVal user As String = Nothing, Optional ByVal passwd As String = Nothing)
        Try
            _clientMongoDb = New Client(host, port, Nothing, user, passwd)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        End Try
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Envia el documento a la cola de mensajes para su posterior indexación
    ''' </summary>
    ''' <param name="container">Nombre del contenedor V12</param>
    ''' <param name="doc">Documento a indexar</param>
    Public Function IndexDocumentAsync(ByVal container As String, ByVal doc As IndexedDocument2) As Task
        Return Task.Factory.StartNew(Sub()
                                         IndexDocument(container, doc)
                                     End Sub)
    End Function

    ''' <summary>
    ''' Envia el documento a la cola de mensajes para su posterior indexación
    ''' </summary>
    ''' <param name="container">Nombre del contenedor V12</param>
    ''' <param name="doc">Documento a indexar</param>
    Public Sub IndexDocument(ByVal container As String, ByVal doc As IndexedDocument2)
        If Not String.IsNullOrEmpty(container) Then
            _clientMongoDb.UseDatabase(container.Trim().ToLower())
            If doc.Id.Equals(MongoDB.Bson.ObjectId.Empty) Then
                _clientMongoDb.InsertOne(Of IndexedDocument2)("IndexedDocuments", RemoveExtraChars(doc))
            Else
                _clientMongoDb.UpdateOne(Of IndexedDocument2)("IndexedDocuments", RemoveExtraChars(doc), doc.Id)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Elimina los caracteres especiales del campo IdEntity
    ''' </summary>
    ''' <param name="doc">Documento a limpiar</param>
    ''' <returns>Documento limpio</returns>
    Private Function RemoveExtraChars(ByVal doc As IndexedDocument2) As IndexedDocument2
        doc.IdEntity = doc.IdEntity.Replace("$#", "").Replace("#$", "")
        Return doc
    End Function

    ''' <summary>
    ''' Obtiene un documento por su Id
    ''' </summary>
    ''' <param name="dataBase">Base de datos a consultar</param>
    ''' <param name="id">Id del documento</param>
    ''' <returns>Documento obtenido</returns>
    Public Function GetDocument(ByVal dataBase As String, id As MongoDB.Bson.ObjectId) As IndexedDocument2
        Try
            _clientMongoDb.UseDatabase(dataBase.Trim().ToLower())
            Return _clientMongoDb.GetOne(Of IndexedDocument2)("IndexedDocuments", id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New IndexedDocument2()
        End Try
    End Function

    ''' <summary>
    ''' Realiza la busqueda de documentos indexados
    ''' </summary>
    ''' <param name="nameDataBase">Nombre de la base de datos</param>
    ''' <param name="text">Texto usado como patrón de busqueda</param>
    ''' <param name="initDate">Fecha inicial de indexación</param>
    ''' <param name="finDate">Fecha final de indexación</param>
    ''' <param name="skip">Cantidad de ocurrencias a ignorar</param>
    ''' <param name="top">Cantitdad de ocurrencias a tomar</param>
    ''' <param name="asc">Valor que indica si los resultados se organizan de forma acendente</param>
    ''' <returns>Una lista de documentos indexados</returns>
    Public Function SearchDocumentAsync(ByVal nameDataBase As String, ByVal text As String, ByVal initDate As DateTime?, ByVal finDate As DateTime?, ByVal skip As Int64, ByVal top As Int64, Optional ByVal asc As Boolean = True) As Task(Of IndexedDocumentResultSet2)
        Return Task.Factory.StartNew(Of IndexedDocumentResultSet2)(Function()
                                                                       Return SearchDocument(nameDataBase.Trim().ToLower(), text, initDate, finDate, skip, top, asc)
                                                                   End Function)
    End Function

    ''' <summary>
    ''' Realiza la busqueda de documentos indexados de forma asíncrona
    ''' </summary>
    ''' <param name="nameDataBase">Nombre de la base de datos</param>
    ''' <param name="text">Texto usado como patrón de busqueda</param>
    ''' <param name="initDate">Fecha inicial de indexación</param>
    ''' <param name="finDate">Fecha final de indexación</param>
    ''' <param name="skip">Cantidad de ocurrencias a ignorar</param>
    ''' <param name="top">Cantitdad de ocurrencias a tomar</param>
    ''' <param name="asc">Valor que indica si los resultados se organizan de forma acendente</param>
    ''' <returns>Una lista de documentos indexados</returns>
    Public Function SearchDocument(ByVal nameDataBase As String, ByVal text As String, ByVal initDate As DateTime?, ByVal finDate As DateTime?, ByVal skip As Int64, ByVal top As Int64, Optional ByVal asc As Boolean = True) As IndexedDocumentResultSet2
        Try
            _clientMongoDb.UseDatabase(nameDataBase.Trim().ToLower())
            Dim res = _clientMongoDb.Find(Of IndexedDocument2)("IndexedDocuments", text, top)
            Dim ret As New IndexedDocumentResultSet2()
            ret.RawCount = res.RawCount
            ret.ElapsedTime = res.ElapsedTime
            ret.Results = res.GetResults()
            Return ret
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New IndexedDocumentResultSet2()
        End Try
    End Function

    ''' <summary>
    ''' Realiza la eliminación de un documento indexado de forma asíncrona
    ''' </summary>
    ''' <param name="nameDataBase">Nombre de la base de datos</param>
    ''' <param name="doc">Documento a eliminar</param>
    Public Function DeleteDocumentAsync(ByVal nameDataBase As String, ByVal doc As IndexedDocument2) As Task(Of Boolean)
        Return Task.Factory.StartNew(Of Boolean)(Function()
                                                     Return DeleteDocument(nameDataBase.Trim().ToLower(), doc)
                                                 End Function)
    End Function

    ''' <summary>
    ''' Realiza la eliminación de un documento indexado
    ''' </summary>
    ''' <param name="nameDataBase">Nombre de la base de datos</param>
    ''' <param name="doc">Documento a eliminar</param>
    Public Function DeleteDocument(ByVal nameDataBase As String, ByVal doc As IndexedDocument2) As Boolean
        Try
            Try
                _clientMongoDb.UseDatabase(nameDataBase.Trim().ToLower())
                Dim res = _clientMongoDb.DeleteOne(Of IndexedDocument2)("IndexedDocuments", doc.Id)
                Return If(res > 0, True, False)
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return False
            End Try
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

#End Region

End Class