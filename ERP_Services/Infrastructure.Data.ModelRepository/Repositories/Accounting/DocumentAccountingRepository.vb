'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
' Author           : Sergio Fernandez
' Created          : 2014-15-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Infrastructure
Imports Domain.Base
Imports System.Threading.Tasks
Imports System.Data.Entity
#End Region
Public Class DocumentAccountingRepository
    Inherits GenericRepository(Of JournalVouchers)
    Implements IAccountingDocumentRepository, Inject

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

#Region "Constructor"
    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Functions"

    Public Function ListJournalVouchersMassiveConfirm(listDocuments As List(Of String)) As List(Of JournalVouchers) Implements IAccountingDocumentRepository.ListJournalVouchersMassiveConfirm
        Return (From tn In _context.JournalVouchers.Include("JournalVoucherDetails") Where listDocuments.Contains(tn.Id) Select tn).ToList()
    End Function

    ''' <summary>
    ''' funcion para obtener el documento contable por consecutibo
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    <Obsolete("Esta función está obsoleta. Usa la función NuevaFuncion en su lugar.", True)>
    Public Function GetAccountingDocumenteByConsecutive(consecutive As String, ByVal tracking As Boolean) As JournalVouchers Implements IAccountingDocumentRepository.GetAccountingDocumenteByConsecutive
        If tracking Then

            Return (From e In _context.JournalVouchers.Include("JournalVoucherDetails")
                    Where e.Consecutive = consecutive
                    Select e).FirstOrDefault
        Else
            Return (From e In _context.JournalVouchers.AsNoTracking().Include("JournalVoucherDetails").AsNoTracking()
                    Where e.Consecutive = consecutive
                    Select e).FirstOrDefault
        End If
    End Function

    Public Async Function GetAccountingDocumenteByConsecutiveAsync(consecutive As String, ByVal tracking As Boolean) As Task(Of JournalVouchers) Implements IAccountingDocumentRepository.GetAccountingDocumenteByConsecutiveAsync
        If tracking Then

            Return Await (From e In _context.JournalVouchers.Include("JournalVoucherDetails")
                          Where e.Consecutive = consecutive
                          Select e).FirstOrDefaultAsync
        Else
            Return Await (From e In _context.JournalVouchers.AsNoTracking().Include("JournalVoucherDetails").AsNoTracking()
                          Where e.Consecutive = consecutive
                          Select e).FirstOrDefaultAsync
        End If
    End Function

    ''' <summary>
    ''' funcion para obtener el documento contable por el id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAccountingDocumenteById(id As Integer) As JournalVouchers Implements IAccountingDocumentRepository.GetAccountingDocumenteById
        Dim query = (From e In _context.JournalVouchers.Include("JournalVoucherDetails").AsNoTracking()
                     Where e.Id = id
                     Select e).FirstOrDefault

        If query IsNot Nothing Then
            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Gets the accounting document by consecutive identifier document.
    ''' </summary>
    ''' <param name="consecutive">The consecutive.</param>
    ''' <param name="idDocument">The identifier document.</param>
    ''' <returns></returns>
    <Obsolete("Este metodo ya no se usa , se tiene que usar el que pide el tipo de comprobante")>
    Public Function GetAccountingDocumentByConsecutiveIdDocument(consecutive As Integer, idDocument As Integer) As JournalVouchers Implements IAccountingDocumentRepository.GetAccountingDocumentByConsecutiveIdDocument
        Dim query = (From e In _context.JournalVouchers Where e.Consecutive = consecutive And e.IdJournalVoucher = idDocument Select e).FirstOrDefault()
        If query IsNot Nothing Then
            query.OriginalValue = (From e In _context.JournalVouchers Where e.Consecutive = consecutive And e.IdJournalVoucher = idDocument Select e).FirstOrDefault()
            Dim journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking() Where query.IdJournalVoucher = jvt.Id Select jvt).FirstOrDefault()
            query.CodeNameJournalVoucherType = journalVoucherType.Code + " - " + journalVoucherType.Name

            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Gets the accounting document by consecutive identifier document, the journal voucher type and legal book.
    ''' </summary>
    ''' <param name="legalbookId"></param>
    ''' <param name="journalVoucherTypeId"></param>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    Public Function GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookId(legalbookId As Integer, journalVoucherTypeId As Integer, consecutive As Integer) As JournalVouchers Implements IAccountingDocumentRepository.GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookId
        Dim query = (From e In _context.JournalVouchers Where e.LegalBookId = legalbookId AndAlso e.IdJournalVoucher = journalVoucherTypeId AndAlso e.Consecutive = consecutive Select e).FirstOrDefault()
        If query IsNot Nothing Then
            query.OriginalValue = (From e In _context.JournalVouchers Where e.LegalBookId = legalbookId AndAlso e.IdJournalVoucher = journalVoucherTypeId AndAlso e.Consecutive = consecutive Select e).FirstOrDefault()
            Dim journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking() Where query.IdJournalVoucher = jvt.Id Select jvt).FirstOrDefault()
            query.CodeNameJournalVoucherType = journalVoucherType.Code + " - " + journalVoucherType.Name

            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' funcion para obtener el documento contable por id
    ''' </summary>
    ''' <param name="idJournalVouchers">el id del documento contable.</param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Obsolete("Esta función está obsoleta. Usa GetJournalVouchersByIdAsync.", False)>
    Public Function GetJournalVouchersById(idJournalVouchers As Long) As JournalVouchers Implements IAccountingDocumentRepository.GetJournalVouchersById
        Dim query = (From e In _context.JournalVouchers Where e.Id = idJournalVouchers Select e).FirstOrDefault()
        If query IsNot Nothing Then
            query.OriginalValue = (From e In _context.JournalVouchers Where e.Id = idJournalVouchers Select e).FirstOrDefault()
            Dim journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking() Where query.IdJournalVoucher = jvt.Id Select jvt).FirstOrDefault()
            query.CodeNameJournalVoucherType = journalVoucherType.Code + " - " + journalVoucherType.Name
            Return query
        Else
            Return Nothing
        End If
    End Function
    ''' <summary>
    ''' funcion para obtener el documento contable por id
    ''' </summary>
    ''' <param name="idJournalVouchers"></param>
    ''' <returns></returns>
    Public Async Function GetJournalVouchersByIdAsync(idJournalVouchers As Long) As Task(Of JournalVouchers) Implements IAccountingDocumentRepository.GetJournalVouchersByIdAsync
        Dim query = Await (From e In _context.JournalVouchers Where e.Id = idJournalVouchers Select e).FirstOrDefaultAsync()
        If query IsNot Nothing Then
            query.OriginalValue = Await (From e In _context.JournalVouchers Where e.Id = idJournalVouchers Select e).FirstOrDefaultAsync()
            Dim journalVoucherType = Await (From jvt In _context.JournalVoucherTypes.AsNoTracking() Where query.IdJournalVoucher = jvt.Id Select jvt).FirstOrDefaultAsync()
            query.CodeNameJournalVoucherType = journalVoucherType.Code + " - " + journalVoucherType.Name
            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' funcion para obtener los comprobantes contables por estado
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    Public Function GetJournalVourchersByStatus(Status As Integer, ByVal month As Integer, year As Integer) As List(Of SP_GetJournalVouchersByStatus_Result) Implements IAccountingDocumentRepository.GetJournalVourchersByStatus
        Dim result = (_context.SP_GetJournalVouchersByStatus(Status, month, year)).ToList()
        If result.Count() > 0 Then
            For Each item As SP_GetJournalVouchersByStatus_Result In result
                Select Case item.Origin
                    Case "Accounting"
                        item.EntityName = ResourceManager.GetString("AccountingEntity", "Accounting")
                    Case "TreasuryControl"
                        Select Case item.DocumentType
                            Case 1
                                item.EntityName = ResourceManager.GetString("CashReceiptEntity", "Accounting")
                            Case 2
                                item.EntityName = ResourceManager.GetString("VoucherTransactionEntity", "Accounting")
                            Case 3
                                item.EntityName = ResourceManager.GetString("TreasuryNotes", "Accounting")
                            Case 4
                                item.EntityName = ResourceManager.GetString("DepositEntity", "Accounting")
                            Case 5
                                item.EntityName = ResourceManager.GetString("RefundEntity", "Accounting")
                            Case 6
                                item.EntityName = ResourceManager.GetString("CrossingEntity", "Accounting")
                            Case 7
                                item.EntityName = ResourceManager.GetString("FundDispersionEntity", "Accounting")
                        End Select
                    Case "PortfolioControl"
                        Select Case item.DocumentType
                            Case 1
                                item.EntityName = ResourceManager.GetString("PortfolioNotesEntity", "Accounting")
                            Case 2
                                item.EntityName = "Cuenta por Cobrar - Cruce de anticipos vs cxc"
                            Case 3
                                item.EntityName = "Cuenta por Cobrar - Cuenta por Cobrar"
                        End Select
                    Case "PaymentsControl"
                        Select Case item.DocumentType
                            Case 1
                                item.EntityName = ResourceManager.GetString("AccountPayableEntity", "Accounting")
                            Case 2
                                item.EntityName = ResourceManager.GetString("PaymentNotesEntity", "Accounting")
                            Case 3
                                item.EntityName = ResourceManager.GetString("AdvancePaymentsEntity", "Accounting")
                            Case 4
                                item.EntityName = ResourceManager.GetString("PaymentTransferEntity", "Accounting")
                            Case 5
                                item.EntityName = ResourceManager.GetString("InitialBalanceEntity", "Accounting")
                        End Select
                End Select
            Next
            Return result
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' funcion para actualizar un listado de documentos contables
    ''' </summary>
    ''' <param name="ListIdJournalVouchers">The list identifier journal vouchers.</param>
    ''' <returns></returns>
    Public Function SaveListJournalVouchers(ByVal Status As Integer, ListIdJournalVouchers As String, ByVal month As Integer) As List(Of SP_ChangeStatusJournalVouchers_Result) Implements IAccountingDocumentRepository.SaveListJournalVouchers
        Try
            Dim list As New List(Of SP_ChangeStatusJournalVouchers_Result)
            Dim result = (_context.SP_ChangeStatusJournalVouchers(Status, ListIdJournalVouchers, month)).ToList()
            Dim item As SP_ChangeStatusJournalVouchers_Result
            Dim journalVouchers As JournalVouchers
            If result.Count() > 0 Then
                For Each i As SP_ChangeStatusJournalVouchers_Result In result
                    item = New SP_ChangeStatusJournalVouchers_Result()
                    journalVouchers = GetJournalVouchersById(i.Id)
                    item.Data = i.Data
                    item.Id = i.Id
                    list.Add(item)
                Next
            End If
            Return list
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Crea un comprobante contable con un store procedure y enviando el comprobante como Xml
    ''' </summary>
    ''' <param name="journarlXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveJournalVoucher(journarlXml As String, codeUser As String) As SP_SaveJournalVoucher_Result Implements IAccountingDocumentRepository.SaveJournalVoucher
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveJournalVoucher(journarlXml, codeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Crea un comprobante contable con un store procedure enviando el comprobante como Xml
    ''' </summary>
    ''' <param name="journarlXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function CreateAndValidateJournalVoucherAsync(journarlXml As String, codeUser As String) As Task(Of SP_CreateAndValidateJournalVoucherMovement_Result) Implements IAccountingDocumentRepository.CreateAndValidateJournalVoucherAsync
        Return Await _context.SP_CreateAndValidateJournalVoucherMovementAsync(journarlXml, codeUser)
    End Function

    ''' <summary>
    ''' Desconfirma o anula un comprobante contable con un store procedure enviando el comprobante como Xml
    ''' </summary>
    ''' <param name="journarlXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function UnconfirmOrAnnulJournalVoucherAsync(journarlXml As String, codeUser As String) As Task(Of SP_UnconfirmOrAnnulJournalVoucher_Result) Implements IAccountingDocumentRepository.UnconfirmOrAnnulJournalVoucherAsync
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return Await _context.SP_UnconfirmOrAnnulJournalVoucherAsync(journarlXml, codeUser)
    End Function
    ''' <summary>
    ''' metodo para generar las homologaciones del comprobante contable
    ''' </summary>
    ''' <param name="journalXml"></param>
    ''' <param name="legalBookId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateHomologationsJournalVoucher(journalXml As String, legalBookId As Integer) As List(Of SP_HomologationJournalVoucher_Result) Implements IAccountingDocumentRepository.GenerateHomologationsJournalVoucher
        Return _context.SP_HomologationJournalVoucher(journalXml, legalBookId).ToList()
    End Function
#End Region
    ''' <summary>
    ''' Obtiene la cabecera de un Comprobante contable solo por ID
    ''' </summary>
    ''' <param name="idJournalVouchers"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetJournalVouchersByIdOnlyHead(idJournalVouchers As Integer) As JournalVouchers Implements IAccountingDocumentRepository.GetJournalVouchersByIdOnlyHead
        Dim query = (From e In _context.JournalVouchers.Include("LegalBook") Where e.Id = idJournalVouchers Select e).FirstOrDefault()
        If query IsNot Nothing Then
            query.BookCurrencyId = query?.LegalBook?.OfficialCurrencyId
            Return query
        Else
            Return Nothing
        End If
    End Function

    Public Function GetDocumentByEntityId(EntityId As Integer) As JournalVouchers Implements IAccountingDocumentRepository.GetDocumentByEntityId
        Return (From e In _context.JournalVouchers.AsNoTracking() Where e.EntityId = EntityId Select e).FirstOrDefault()
    End Function

    Public Function GetAccountingDocumenteByEntityIdAndEntityName(EntityId As Integer, EntityName As String) As JournalVouchers Implements IAccountingDocumentRepository.GetAccountingDocumenteByEntityIdAndEntityName
        Return (From e In _context.JournalVouchers.AsNoTracking().Include("JournalVoucherDetails").AsNoTracking() Where e.EntityId = EntityId AndAlso e.EntityName = EntityName Select e).FirstOrDefault()
    End Function

    Public Function SP_CopyAndPasteJournalVoucherDetails(LegalBookId As Integer, XmlObject As String) As List(Of SP_CopyAndPasteJournalVoucherDetails_Result) Implements IAccountingDocumentRepository.SP_CopyAndPasteJournalVoucherDetails
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteJournalVoucherDetails(LegalBookId, XmlObject).ToList
    End Function

End Class
