'************************************************************
' Assembly         : Domain.DocumentalRepository
' Author           : Juan Diego Diaz
' Created          : 10-09-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.DocumentalSystem.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio de documentos.
''' </summary>
Public Interface IDocumentsStoreRepository
    Inherits IRepository(Of DocumentsStore)

    ''' <summary>
    ''' Función que obtiene una lista de documentos.
    ''' </summary>
    ''' <returns>Lista de Documentos</returns>
    Function ListAllDocuments() As List(Of DocumentsStore)
    ''' <summary>
    ''' Funcion para obtener datos del fileStream
    ''' </summary>
    ''' <param name="Id">Id del Documento</param>
    ''' <param name="tran">Commitable Transaction</param>
    ''' <param name="Company">Código compañia actual</param>
    ''' <returns>FileStreamContext</returns>
    Function GetDocument(ByVal Id As String, ByVal Tran As Object) As FileStreamContext
    ''' <summary>
    ''' Función que obtiene una lista de documentos.
    ''' </summary>
    ''' <returns>Lista de Documentos</returns>
    Function GetDocumentById(Id As String) As DocumentsStore
    ''' <summary>
    ''' Buscar documentos por fullText
    ''' </summary>
    ''' <param name="searchWords"></param>
    ''' <returns></returns>
    Function GetDocumentFullText(searchWords As String, inMenu As Boolean, ByVal skip As Int64, ByVal top As Int64) As List(Of DocumentsStore)
    ''' <summary>
    ''' Funcion que obtiene registro fileStream
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="Company"></param>
    ''' <returns>FileStreamContext</returns>
    Function GetFileStream(Id As String, Container As String) As FileStreamContext
    ''' <summary>
    ''' Obtiene un documento según Id del formulario
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <param name="withTop">Variable filtro</param>
    ''' <returns>Lista DocumentsStore</returns>
    Function getDocumentByIdForm(IdForm As Integer, withTop As Boolean) As List(Of DocumentsStore)
    ''' <summary>
    ''' Obtiene un documento según Id del formulario
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <param name="IdEntity">Id Registro</param>
    ''' <param name="withTop">Variable filtro</param>
    ''' <returns>Lista DocumentsStore</returns>
    Function getDocumentByIdFormAndIdEntity(IdForm As Integer, IdEntity As Integer, withTop As Boolean) As List(Of DocumentsStore)
    ''' <summary>
    ''' Función para obtener un documento según el codigo.
    ''' </summary>
    ''' <param name="Id">Codigo Documento</param>
    ''' <param name="Tran">Commitable Transaction</param>
    ''' <returns>Objeto FileStreamContext</returns>
    Function GetDocumentContext(Id As String, Tran As Object, ByVal Container As String) As FileStreamContext

    ''' <summary>
    ''' Obtiene un documento según Id del formulario y el contenedor
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <param name="IdFileContainer">Id Entidad</param>
    ''' <returns>Lista DocumentsStore</returns>
    Function ListDocumentByIdFormAndIdFileContainer(IdForm As String, IdFileContainer As String, TextSearch As String, FullText As Boolean) As List(Of DocumentsStore)

End Interface
