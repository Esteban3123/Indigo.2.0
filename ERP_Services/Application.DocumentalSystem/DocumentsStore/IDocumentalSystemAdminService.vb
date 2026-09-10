'***********************************************************************
' Assembly         : Application.DocumentalSystem
' Author           : Juan Diego Diaz
' Created          : 10-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.DocumentalSystem.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz para el servicio del sistema documental.
''' </summary>
Public Interface IDocumentalSystemAdminService
    ''' <summary>
    ''' Funcion para listar todas los documentos.
    ''' </summary>
    ''' <returns>Lista de Documentos</returns>
    Function ListAllDocuments() As List(Of DocumentsStore)
    ''' <summary>
    ''' Consulta un documento especifico.
    ''' </summary>
    ''' <param name="Id">El código del documento</param>
    ''' <param name="Company">El código de la compañia actual</param>
    ''' <returns>Objeto Documento</returns>
    Function GetDocument(ByVal Id As String, ByVal Container As String) As System.IO.Stream
    ''' <summary>
    ''' Función que obtiene una lista de documentos.
    ''' </summary>
    ''' <returns>Lista de Documentos</returns>
    Function GetDocumentById(code As String) As DocumentsStore
    ''' <summary>
    ''' Buscar documentos por fullText
    ''' </summary>
    ''' <param name="searchWords"></param>
    ''' <returns></returns>
    Function GetDocumentFullText(searchWords As String, inMenu As Boolean, ByVal skip As Int64, ByVal top As Int64) As List(Of DocumentsStore)
    ''' <summary>
    ''' Metodo para guardar documento
    ''' </summary>
    ''' <param name="document">DocumentInfo</param>
    Function SaveDocument(document As DocumentInfo) As ResponseDocument
    ''' <summary>
    ''' Obtiene un documento según Id del archivador y Id del registro
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <param name="withTop">Variable para filtro</param>
    ''' <returns>Lista DocumentsStore</returns>
    Function getDocumentByIdForm(IdForm As Integer, withTop As Boolean) As List(Of DocumentsStore)
    ''' <summary>
    ''' Obtiene un documento según Id del formulario y el registro
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <param name="IdEntity">Id Entidad</param>
    ''' <param name="withTop">Variable para filtro</param>
    ''' <returns>Lista DocumentsStore</returns>
    Function getDocumentByIdFormAndIdEntity(IdForm As Integer, IdEntity As Integer, withTop As Boolean) As List(Of DocumentsStore)
    ''' <summary>
    ''' Obtiene un documento según Id del formulario
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <returns>Objeto FileStreamContext</returns>
    Function ListDocumentByIdFormAndIdFileContainer(IdForm As String, IdFileContainer As String, TextSearch As String, FullText As Boolean) As List(Of DocumentsStore)

    ''' <summary>
    ''' Elimina un documento
    ''' </summary>
    ''' <param name="document"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteDocument(document As DocumentsStore) As ActionResult

End Interface
