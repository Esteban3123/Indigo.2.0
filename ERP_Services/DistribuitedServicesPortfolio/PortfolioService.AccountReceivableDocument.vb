'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Juan Carlos Bermudez
' Created          : 17-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.Portfolio
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
#End Region

Partial Class PortfolioService

    ''' <summary>
    ''' Obtiene un documento de cuenta x cobrar por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableDocumentById(id As Integer) As Domain.Entities.AccountReceivableDocument Implements IPortfolioServiceAccountReceivableDocument.GetAccountReceivableDocumentById
        Using service As IAccountReceivableDocumentAdminService = Container.Current.Resolve(Of IAccountReceivableDocumentAdminService)()
            Return service.GetAccountReceivableDocumentById(id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un documento de cuenta x cobrar por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableDocumentByCode(code As String, audit As AuditMessage) As AccountReceivableDocument Implements IPortfolioServiceAccountReceivableDocument.GetAccountReceivableDocumentByCode
        Using service As IAccountReceivableDocumentAdminService = Container.Current.Resolve(Of IAccountReceivableDocumentAdminService)()
            Return service.GetAccountReceivableDocumentByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un documento de cuenta x cobrar
    ''' </summary>
    ''' <param name="accountReceivableDocument"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAccountReceivableDocument(accountReceivableDocument As Domain.Entities.AccountReceivableDocument, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountReceivableDocument) Implements IPortfolioServiceAccountReceivableDocument.SaveAccountReceivableDocument
        Using service As IAccountReceivableDocumentAdminService = Container.Current.Resolve(Of IAccountReceivableDocumentAdminService)()
            Return service.SaveAccountReceivableDocument(accountReceivableDocument, audit)
        End Using
    End Function

End Class
