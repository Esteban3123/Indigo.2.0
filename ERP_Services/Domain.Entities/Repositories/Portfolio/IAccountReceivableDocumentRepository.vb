'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Juan Carlos Bermudez 
' Created          : 11-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IAccountReceivableDocumentRepository
    Inherits IRepository(Of AccountReceivableDocument)

    ''' <summary>
    ''' Consultar un documento de cuenta x cobrar por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableDocumentById(ByVal id As Integer) As AccountReceivableDocument

    ''' <summary>
    ''' Consultar un documento de cuenta x cobrar Por codigo 
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableDocumentByCode(ByVal code As String) As AccountReceivableDocument

    ''' <summary>
    ''' Lista los documentos de cuenta x cobrar
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    Function ListAccountReceivableDocumentMassiveConfirm(listDocuments As List(Of String)) As List(Of AccountReceivableDocument)

    ''' <summary>
    ''' Guarda, Actualiza o Confirma un documento de cuenta x cobrar
    ''' </summary>
    ''' <param name="accountReceivableDocumentXml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SaveAccountReceivableDocument(accountReceivableDocumentXml As String, userCode As String) As SP_SaveAccountReceivableDocument_Result

End Interface
