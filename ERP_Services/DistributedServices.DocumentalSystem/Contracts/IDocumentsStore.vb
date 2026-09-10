#Region "Imports"
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.DocumentalSystem.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()> _
Public Interface IDocumentsStore

    ''' <summary>
    ''' Funcion para listar todas los documentos.
    ''' </summary>
    ''' <param name="sessionInf">Info de session</param>
    ''' <returns>Lista de Documentos</returns>
    <OperationContract()> _
    Function ListAllDocuments(sessionInf As SessionInfo) As List(Of DocumentsStore)
    ''' <summary>
    ''' Consulta un documento especifico.
    ''' </summary>
    ''' <param name="sessionInf">Info de session</param>
    ''' <returns>Stream</returns>
    <OperationContract()> _
    Function GetDocument(sessionInf As SessionInfo) As System.IO.Stream
    ''' <summary>
    ''' Función que obtiene una lista de documentos.
    ''' </summary>
    ''' <param name="sessionInf">Info de session</param>
    ''' <returns>Lista de Documentos</returns>
    <OperationContract>
    Function GetDocumentById(sessionInf As SessionInfo) As DocumentsStore
    ''' <summary>
    ''' Metodo para guardar documento
    ''' </summary>
    ''' <param name="documentInfo">DocumentInfo</param>
    ''' <returns>ResponseDocument</returns>
    <OperationContract>
    Function SaveDocument(documentInfo As DocumentInfo) As ResponseDocument
    ''' <summary>
    ''' Buscar documentos por fullText
    ''' </summary>
    ''' <param name="sessionInf">Info de session</param>
    ''' <param name="inMenu">Variable filtro</param>
    ''' <returns>Lista de documentos</returns>
    <OperationContract>
    Function GetDocumentFullText(sessionInf As SessionInfo, inMenu As Boolean, ByVal skip As Int64, ByVal top As Int64) As List(Of DocumentsStore)
    ''' <summary>
    ''' Obtiene un documento según Id del formulario
    ''' </summary>
    ''' <param name="withTop">Variable filtro</param>
    ''' <returns>Objeto FileStreamContext</returns>
    <OperationContract>
    Function getDocumentByIdForm(sessionInf As SessionInfo, withTop As Boolean) As List(Of DocumentsStore)
    ''' <summary>
    ''' Obtiene un documento según Id del formulario y el registro
    ''' </summary>
    ''' <param name="sessionInf">sessionInf</param>
    ''' <param name="IdEntity">Id Entidad</param>
    ''' <param name="withTop">Variable filtro</param>
    ''' <returns>Lista DocumentsStore</returns>
    <OperationContract>
    Function getDocumentByIdFormAndIdEntity(sessionInf As SessionInfo, IdEntity As Integer, withTop As Boolean) As List(Of DocumentsStore)
    ''' <summary>
    ''' Obtiene un documento según Id del formulario
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <returns>Objeto FileStreamContext</returns>
    <OperationContract>
    Function ListDocumentByIdFormAndIdFileContainer(IdForm As String, IdFileContainer As String, TextSearch As String, FullText As Boolean, session As SessionValues) As List(Of DocumentsStore)

    <OperationContract>
    Function DeleteDocument(document As DocumentsStore, sessionInf As SessionInfo) As ActionResult

End Interface



