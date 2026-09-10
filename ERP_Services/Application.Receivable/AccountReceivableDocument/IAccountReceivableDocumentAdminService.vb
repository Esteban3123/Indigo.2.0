'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Juan Carlos Bermudez
' Created          : 12-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IAccountReceivableDocumentAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un documento de cuenta x cobrar por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableDocumentById(id As Integer) As AccountReceivableDocument

    ''' <summary>
    ''' Obtiene un documento de cuenta x cobrar por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableDocumentByCode(code As String, audit As AuditMessage) As AccountReceivableDocument

    ''' <summary>
    ''' Guarda o Actualiza un documento de cuenta x cobrar
    ''' </summary>
    ''' <param name="accountReceivableDocument"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAccountReceivableDocument(accountReceivableDocument As AccountReceivableDocument, audit As AuditMessage) As ActionResult(Of AccountReceivableDocument)

End Interface
