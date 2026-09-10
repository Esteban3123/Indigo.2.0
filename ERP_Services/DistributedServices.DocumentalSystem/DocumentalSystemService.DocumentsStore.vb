'***********************************************************************
' Assembly         : DistributedService.DocumentalSystem
' Author           : Juan Diego Diaz
' Created          : 10-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.DocumentalSystem.Entities
Imports Domain.DocumentalSystem
Imports Infrastructure.CrossCutting.IOC
Imports Application.DocumentalSystem
Imports Infrastructure.CrossCutting.Base
#End Region
Partial Class DocumentalSystemService

    ''' <summary>
    ''' Consulta un documento especifico.
    ''' </summary>
    ''' <param name="sessionInf">Info de session</param>
    ''' <returns>Stream</returns>
    Public Function GetDocument(sessionInf As SessionInfo) As System.IO.Stream Implements IDocumentsStore.GetDocument
        Dim Document As IDocumentalSystemAdminService = IocFactory.Instance(sessionInf.Container).CurrentContainer.Resolve(Of IDocumentalSystemAdminService)()
        Return Document.GetDocument(sessionInf.aux, sessionInf.Container)
    End Function

    ''' <summary>
    ''' Funcion para listar todas los documentos.
    ''' </summary>
    ''' <param name="sessionInf">Info de session</param>
    ''' <returns>Lista de Documentos</returns>
    Public Function ListAllDocuments(sessionInf As SessionInfo) As List(Of DocumentsStore) Implements IDocumentsStore.ListAllDocuments
        Dim Document As IDocumentalSystemAdminService = IocFactory.Instance(sessionInf.Container).CurrentContainer.Resolve(Of IDocumentalSystemAdminService)()
        Return Document.ListAllDocuments
    End Function

    ''' <summary>
    ''' Función que obtiene una lista de documentos.
    ''' </summary>
    ''' <param name="sessionInf">Info de session</param>
    ''' <returns>Lista de Documentos</returns>
    Public Function GetDocumentById(sessionInf As SessionInfo) As DocumentsStore Implements IDocumentsStore.GetDocumentById
        Dim Document As IDocumentalSystemAdminService = IocFactory.Instance(sessionInf.Container).CurrentContainer.Resolve(Of IDocumentalSystemAdminService)()
        Return Document.GetDocumentById(sessionInf.aux)
    End Function

    ''' <summary>
    ''' Metodo para guardar documento
    ''' </summary>
    ''' <param name="documentInfo">DocumentInfo</param>
    ''' <returns>ResponseDocument</returns>
    Function SaveDocument(documentInfo As DocumentInfo) As ResponseDocument Implements IDocumentsStore.SaveDocument
        Dim Document As IDocumentalSystemAdminService = IocFactory.Instance(documentInfo.SessionInf.Container).CurrentContainer.Resolve(Of IDocumentalSystemAdminService)()
        Return Document.SaveDocument(documentInfo)
    End Function

    ''' <summary>
    ''' Buscar documentos por fullText
    ''' </summary>
    ''' <param name="sessionInf">Info de session</param>
    ''' <param name="inMenu">Variable filtro</param>
    ''' <returns>Lista de documentos</returns>
    Public Function GetDocumentFullText(sessionInf As SessionInfo, inMenu As Boolean, ByVal skip As Int64, ByVal top As Int64) As List(Of DocumentsStore) Implements IDocumentsStore.GetDocumentFullText
        Dim Document As IDocumentalSystemAdminService = IocFactory.Instance(sessionInf.Container).CurrentContainer.Resolve(Of IDocumentalSystemAdminService)()
        Return Document.GetDocumentFullText(sessionInf.aux, inMenu, skip, top)
    End Function

    ''' <summary>
    ''' Obtiene un documento según Id del formulario
    ''' </summary>
    ''' <param name="withTop">Variable filtro</param>
    ''' <returns>Objeto FileStreamContext</returns>
    Public Function getDocumentByIdForm(sessionInf As SessionInfo, withTop As Boolean) As List(Of DocumentsStore) Implements IDocumentsStore.getDocumentByIdForm
        Dim Document As IDocumentalSystemAdminService = IocFactory.Instance(sessionInf.Container).CurrentContainer.Resolve(Of IDocumentalSystemAdminService)()
        Return Document.getDocumentByIdForm(sessionInf.aux, withTop)
    End Function

    ''' <summary>
    ''' Obtiene un documento según Id del formulario y el registro
    ''' </summary>
    ''' <param name="sessionInf">sessionInf</param>
    ''' <param name="IdEntity">Id Entidad</param>
    ''' <param name="withTop">Variable filtro</param>
    ''' <returns>Lista DocumentsStore</returns>
    Public Function getDocumentByIdFormAndIdEntity(sessionInf As Domain.DocumentalSystem.Entities.SessionInfo, IdEntity As Integer, withTop As Boolean) As List(Of Domain.DocumentalSystem.Entities.DocumentsStore) Implements IDocumentsStore.getDocumentByIdFormAndIdEntity
        Dim Document As IDocumentalSystemAdminService = IocFactory.Instance(sessionInf.Container).CurrentContainer.Resolve(Of IDocumentalSystemAdminService)()
        Return Document.getDocumentByIdFormAndIdEntity(sessionInf.aux, IdEntity, withTop)
    End Function

    ''' <summary>
    ''' Obtiene un documento según Id del formulario
    ''' </summary>
    ''' <param name="IdForm">Id Formulario</param>
    ''' <returns>Objeto FileStreamContext</returns>
    Public Function ListDocumentByIdFormAndIdFileContainer(IdForm As String, IdFileContainer As String, TextSearch As String, FullText As Boolean, session As SessionValues) As List(Of DocumentsStore) Implements IDocumentsStore.ListDocumentByIdFormAndIdFileContainer
        Dim Document As IDocumentalSystemAdminService = IocFactory.Instance(session.DocumentalContainer).CurrentContainer.Resolve(Of IDocumentalSystemAdminService)()
        Return Document.ListDocumentByIdFormAndIdFileContainer(IdForm, IdFileContainer, TextSearch, FullText)
    End Function

    Public Function DeleteDocument(document As DocumentsStore, sessionInf As SessionInfo) As Domain.Base.Entities.ActionResult Implements IDocumentsStore.DeleteDocument
        Dim DocumentAdminService As IDocumentalSystemAdminService = IocFactory.Instance(sessionInf.Container).CurrentContainer.Resolve(Of IDocumentalSystemAdminService)()
        Return DocumentAdminService.DeleteDocument(document)
    End Function

End Class
