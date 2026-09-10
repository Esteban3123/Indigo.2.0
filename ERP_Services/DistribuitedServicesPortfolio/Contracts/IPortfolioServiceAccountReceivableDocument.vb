'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Juan Carlos Bermudez
' Created          : 17-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface IPortfolioServiceAccountReceivableDocument

    ''' <summary>
    ''' Obtiene un documento de cuenta x cobrar por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountReceivableDocumentById(id As Integer) As AccountReceivableDocument

    ''' <summary>
    ''' Obtiene un documento de cuenta x cobrar por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAccountReceivableDocumentByCode(code As String, audit As AuditMessage) As AccountReceivableDocument

    ''' <summary>
    ''' Guarda o Actualiza un documento de cuenta x cobrar
    ''' </summary>
    ''' <param name="accountReceivableDocument"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveAccountReceivableDocument(accountReceivableDocument As Domain.Entities.AccountReceivableDocument, audit As AuditMessage) As ActionResult(Of Domain.Entities.AccountReceivableDocument)

End Interface
