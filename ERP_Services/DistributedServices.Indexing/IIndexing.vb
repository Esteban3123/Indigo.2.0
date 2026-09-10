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

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports MongoDB.Bson

#End Region

''' <summary>
''' Define los métodos expuestos por el servicio de indexación
''' </summary>
<ServiceContract()>
Public Interface IIndexing

#Region "Operations"

    ''' <summary>
    ''' Obtiene la fecha y hora del servidor
    ''' </summary>
    ''' <returns>Fecha y hora del servidor</returns>
    <OperationContract()>
    Function GetServerDate() As TimeSpan

    ''' <summary>
    ''' Gestiona la indexación de documentos asínconamente
    ''' </summary>
    ''' <param name="containerV12">Nombre del contenedor Futesh</param>
    ''' <param name="doc">Documento a indexar</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function IndexingDocument(ByVal containerV12 As String, ByVal doc As IndexedDocument2) As ActionResult

    ''' <summary>
    ''' Obtiene un documento por su Id
    ''' </summary>
    ''' <param name="dataBase">Base de datos a consultar</param>
    ''' <param name="id">Id del documento</param>
    ''' <returns>Documento obtenido</returns>
    <OperationContract()>
    Function GetDocument(dataBase As String, ByVal id As ObjectId) As IndexedDocument2

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
    <OperationContract()>
    Function SearchDocument(ByVal dataBase As String, ByVal text As String, ByVal initDate As DateTime?, ByVal finDate As DateTime?, ByVal skip As Int64, ByVal top As Int64, ByVal asc As Boolean) As IndexedDocumentResultSet2

    ''' <summary>
    ''' Realiza la eliminación de un documento indexado
    ''' </summary>
    ''' <param name="dataBase">Base de datos en la cual se realiza la busqueda y eliminación</param>
    ''' <param name="doc">Documento a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function DeleteDocument(ByVal dataBase As String, ByVal doc As IndexedDocument2) As ActionResult

    <OperationContract()>
    Function DeleteDocumentByIdEntity(ByVal dataBase As String, ByVal IdEntity As String) As ActionResult

#End Region

End Interface
