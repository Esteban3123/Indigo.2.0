'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 17-05-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports System.Threading.Tasks
#End Region

Public Interface IAccountingDocumentRepository
    Inherits IRepository(Of JournalVouchers)

#Region "Functions"

    Function GetDocumentByEntityId(EntityId As Integer) As JournalVouchers

    Function ListJournalVouchersMassiveConfirm(listDocuments As List(Of String)) As List(Of JournalVouchers)

    ''' <summary>
    ''' metodo para generar las homologaciones del comprobante contable
    ''' </summary>
    ''' <param name="journalXml"></param>
    ''' <param name="legalBookId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateHomologationsJournalVoucher(journalXml As String, legalBookId As Integer) As List(Of SP_HomologationJournalVoucher_Result)

    ''' <summary>
    ''' funcion para obtener el documento contable por consecutibo
    ''' </summary>
    ''' <returns></returns>
    <Obsolete("Esta función está obsoleta. Usa la función GetAccountingDocumenteByConsecutiveAsync en su lugar.", True)>
    Function GetAccountingDocumenteByConsecutive(ByVal consecutive As String, ByVal tracking As Boolean) As JournalVouchers

    ''' <summary>
    ''' funcion para obtener el documento contable por consecutibo
    ''' </summary>
    ''' <returns></returns>
    Function GetAccountingDocumenteByConsecutiveAsync(ByVal consecutive As String, ByVal tracking As Boolean) As Task(Of JournalVouchers)

    ''' <summary>
    ''' funcion para obtener el documento contable por el id
    ''' </summary>
    ''' <returns></returns>
    Function GetAccountingDocumenteById(ByVal id As Integer) As JournalVouchers

    ''' <summary>
    ''' Gets the accounting document by consecutive identifier document.
    ''' </summary>
    ''' <param name="consecutive">The consecutive.</param>
    ''' <param name="idDocument">The identifier document.</param>
    ''' <returns></returns>
    Function GetAccountingDocumentByConsecutiveIdDocument(ByVal consecutive As Integer, ByVal idDocument As Integer) As JournalVouchers

    ''' <summary>
    ''' Gets the accounting document by consecutive identifier document, the journal voucher type and legal book.
    ''' </summary>
    ''' <param name="legalbookId"></param>
    ''' <param name="journalVoucherTypeId"></param>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    Function GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookId(legalbookId As Integer, journalVoucherTypeId As Integer, consecutive As Integer) As Domain.Entities.JournalVouchers

    ''' <summary>
    ''' funcion para obtener el documento contable por id
    ''' </summary>
    ''' <param name="idJournalVouchers">el id del documento contable.</param>    
    ''' <returns></returns>
    Function GetJournalVouchersByIdAsync(ByVal idJournalVouchers As Long) As Task(Of JournalVouchers)

    ''' <summary>
    ''' funcion para obtener el documento contable por id
    ''' </summary>
    ''' <param name="idJournalVouchers">el id del documento contable.</param>    
    ''' <returns></returns>
    <Obsolete("Esta función está obsoleta. Usa GetJournalVouchersByIdAsync.", False)>
    Function GetJournalVouchersById(ByVal idJournalVouchers As Long) As JournalVouchers

    ''' <summary>
    ''' funcion para obtener el documento contable por id
    ''' </summary>
    ''' <param name="idJournalVouchers">el id del documento contable.</param>    
    ''' <returns></returns>
    Function GetJournalVouchersByIdOnlyHead(ByVal idJournalVouchers As Integer) As JournalVouchers

    ''' <summary>
    ''' funcion para obtener los comprobantes contables por estado
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetJournalVourchersByStatus(ByVal Status As Integer, ByVal month As Integer, year As Integer) As List(Of SP_GetJournalVouchersByStatus_Result)

    ''' <summary>
    ''' Saves the list journal vouchers.
    ''' </summary>
    ''' <param name="ListIdJournalVouchers">The list identifier journal vouchers.</param>
    ''' <returns></returns>
    Function SaveListJournalVouchers(ByVal Status As Integer, ListIdJournalVouchers As String, ByVal month As Integer) As List(Of SP_ChangeStatusJournalVouchers_Result)

    ''' <summary>
    ''' Crea un comprobante contable con un store procedure y enviando el comprobante como Xml
    ''' </summary>
    ''' <param name="journarlXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveJournalVoucher(journarlXml As String, codeUser As String) As SP_SaveJournalVoucher_Result

    ''' <summary>
    ''' Consulta un comprobante contable con detalles por id de la entidad que lo genero y nombre de la entidad
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="name">The name.</param>
    ''' <returns></returns>
    Function GetAccountingDocumenteByEntityIdAndEntityName(id As Integer, name As String) As JournalVouchers

    ''' <summary>
    ''' Metodo para el copy y paste de detalles del comprobante contable
    ''' </summary>
    ''' <param name="LegalBookId"></param>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SP_CopyAndPasteJournalVoucherDetails(LegalBookId As Integer, XmlObject As String) As List(Of SP_CopyAndPasteJournalVoucherDetails_Result)

    ''' <summary>
    ''' Desconfirma o anula un comprobante contable 
    ''' </summary>
    ''' <param name="journarlXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UnconfirmOrAnnulJournalVoucherAsync(journarlXml As String, codeUser As String) As Task(Of SP_UnconfirmOrAnnulJournalVoucher_Result)

    ''' <summary>
    ''' Crea un comprobante contable con un store procedure y enviando el comprobante como Xml
    ''' </summary>
    ''' <param name="journarlXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateAndValidateJournalVoucherAsync(journarlXml As String, codeUser As String) As Task(Of SP_CreateAndValidateJournalVoucherMovement_Result)
#End Region

End Interface
