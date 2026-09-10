Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Globalization
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class BankReconciliationAutomaticRepository
    Inherits GenericRepository(Of BankReconciliationAutomatic)
    Implements IBankReconciliationAutomaticRepository

#Region "Fields"

    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        Me._context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetBankReconciliationAutomaticById(id As Integer, Optional tracking As Boolean = True) As BankReconciliationAutomatic Implements IBankReconciliationAutomaticRepository.GetBankReconciliationAutomaticById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.BankReconciliationAutomatic.Include("BankReconciliationAutomaticExtractDetail") Where d.Id = id Select d).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From d In Me._context.BankReconciliationAutomatic.AsNoTracking.Include("BankReconciliationAutomaticExtractDetail").AsNoTracking Where d.Id = id Select d).SingleOrDefault()
            res.Association = GetListAssociationByBankReconciliation(res.Id)
            Return res
        Else
            Return New BankReconciliationAutomatic()
        End If
    End Function

    Public Function GetBankReconciliationAutomaticByCode(code As String, Optional tracking As Boolean = True) As BankReconciliationAutomatic Implements IBankReconciliationAutomaticRepository.GetBankReconciliationAutomaticByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As BankReconciliationAutomatic In Me._context.BankReconciliationAutomatic.AsNoTracking.Include("BankReconciliationAutomaticDetail").AsNoTracking.Include("BankReconciliationAutomaticExtractDetail") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.EntityBankAccountCodeName = (From p In _context.EntityBankAccounts.AsNoTracking.Include("Bank").AsNoTracking Where res.EntityBankAccountId = p.Id Select p.Code + " - " + p.Bank.Name).FirstOrDefault
            res.Association = GetListAssociationByBankReconciliation(res.Id)
            Return res
        Else
            Return New BankReconciliationAutomatic()
        End If
    End Function

    Public Function GetListAssociationByBankReconciliation(BankReconciliationAutomaticId As Integer) As List(Of BankReconciliationAutomaticAssociation) Implements IBankReconciliationAutomaticRepository.GetListAssociationByReconciliationId
        'va traer todos los registros de la pivot asociados buscando por id
        Dim resAssociation = (From braa As BankReconciliationAutomaticAssociation In _context.BankReconciliationAutomaticAssociation
                              Join brad As BankReconciliationAutomaticDetail In _context.BankReconciliationAutomaticDetail On brad.Id Equals braa.BankReconciliationAutomaticDetailId
                              Join braed As BankReconciliationAutomaticExtractDetail In _context.BankReconciliationAutomaticExtractDetail On braed.Id Equals braa.BankReconciliationAutomaticExtractId
                              Join bra As BankReconciliationAutomatic In _context.BankReconciliationAutomatic On bra.Id Equals brad.BankReconciliationAutomaticId And bra.Id Equals braed.BankReconciliationAutomaticId
                              Where bra.Id = BankReconciliationAutomaticId Select braa).ToList

        If resAssociation IsNot Nothing Then
            Return resAssociation
        Else
            Return New List(Of BankReconciliationAutomaticAssociation)()
        End If
    End Function


    Public Function GetBankAssociation(DetailId As Integer, DetailExtractId As Integer) As BankReconciliationAutomaticAssociation Implements IBankReconciliationAutomaticRepository.GetBankAssociation
        'va consultar en la pivot
        Dim resAssociation = (From braa As BankReconciliationAutomaticAssociation In _context.BankReconciliationAutomaticAssociation.AsNoTracking
                              Where braa.BankReconciliationAutomaticDetailId.Equals(DetailId) And braa.BankReconciliationAutomaticExtractId.Equals(DetailExtractId) Select braa).FirstOrDefault()

        If resAssociation IsNot Nothing Then
            Return resAssociation
        Else
            Return New BankReconciliationAutomaticAssociation
        End If
    End Function

    Public Function SP_GetBankReconciliationAutomaticDetails(XmlCriterias As String) As List(Of SP_GetBankReconciliationAutomaticDetails_Result) Implements IBankReconciliationAutomaticRepository.SP_GetBankReconciliationAutomaticDetails
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetBankReconciliationAutomaticDetails(XmlCriterias).ToList()
    End Function

    Public Function SP_GetBankReconciliationAutomaticExtractDetails(XmlCriterias As String) As List(Of SP_GetBankReconciliationAutomaticExtractDetails_Result) Implements IBankReconciliationAutomaticRepository.SP_GetBankReconciliationAutomaticExtractDetails
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetBankReconciliationAutomaticExtractDetails(XmlCriterias).ToList()
    End Function
    ''' <summary>
    ''' Trae las reglas desde bancos 
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Public Function BankAutomaticRecognitionRules(entityId As Integer) As List(Of BankAutomaticRecognitionRules) Implements IBankReconciliationAutomaticRepository.BankAutomaticRecognitionRules
        Dim resAssociation = (From d In _context.BankAutomaticRecognitionRules
                              Join e In _context.EntityBankAccounts On d.BankId Equals e.IdBank
                              Where e.Id = entityId Select d
          ).Include(Function(x) x.NoteConcepts).ToList()

        If resAssociation IsNot Nothing Then
            Return resAssociation
        Else
            Return New List(Of BankAutomaticRecognitionRules)()
        End If
    End Function
    ''' <summary>
    ''' obtiene las descripciones que se le colocan a los extractos bancarios
    ''' </summary>
    ''' <param name="UploadBankStatementsDescription"></param>
    ''' <returns></returns>
    Public Function GetUploadBankStatementsDescriptionList(UploadBankStatementsDescription As List(Of Integer)) As List(Of UploadBankStatementsDetail) Implements IBankReconciliationAutomaticRepository.GetUploadBankStatementsDescriptionList
        Dim result = (From d In _context.BankReconciliationAutomaticExtractDetail
                      Join e In _context.UploadBankStatementsDetail On e.Id Equals d.UploadBankStatementsDetailId
                      Where UploadBankStatementsDescription.Contains(e.Id)
                      Select e).Distinct().ToList()

        Return If(result IsNot Nothing, result, New List(Of UploadBankStatementsDetail)())
    End Function
    ''' <summary>
    ''' Obtiene los bancos asociados a las reglas
    ''' </summary>
    ''' <param name="bankList"></param>
    ''' <returns></returns>
    Public Function getbanksList(bankList As List(Of Integer)) As List(Of Bank) Implements IBankReconciliationAutomaticRepository.getbanksList
        Dim result = (From x In _context.BankAutomaticRecognitionRules
                      Join y In _context.Bank On x.BankId Equals y.Id
                      Where bankList.Contains(y.Id)
                      Select y).ToList()
        Return If(result IsNot Nothing, result, New List(Of Bank))
    End Function
    ''' <summary>
    ''' Obtiene los conceptos de notas 
    ''' </summary>
    ''' <param name="noteConcepts"></param>
    ''' <returns></returns>
    Public Function getNoteconcepts(noteConcepts As List(Of Integer)) As List(Of NoteConcepts) Implements IBankReconciliationAutomaticRepository.getNoteconcepts
        Dim result = (From x In _context.BankAutomaticRecognitionRules
                      Join y In _context.NoteConcepts On x.NoteConceptsId Equals y.Id
                      Where noteConcepts.Contains(y.Id)
                      Select y).Distinct().ToList()
        Return If(result IsNot Nothing, result, New List(Of NoteConcepts))
    End Function
    ''' <summary>
    ''' Obtiene las partidas pendientes por conciliar para el segmento Libro de Bancos
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Public Function GetPendingItemsBankBook(entityBankAccountId As Integer, documentDate As Date) As List(Of BankReconciliationAutomaticDetail) Implements IBankReconciliationAutomaticRepository.GetPendingItemsBankBook
        Dim result = (From brad In _context.BankReconciliationAutomaticDetail
                      Let originalBraId = If(brad.BankReconciliationAutomaticOriginId.HasValue, brad.BankReconciliationAutomaticOriginId.Value, brad.BankReconciliationAutomaticId)
                      Join bra In _context.BankReconciliationAutomatic On bra.Id Equals originalBraId
                      Where bra.Status = 2 AndAlso bra.EntityBankAccountId = entityBankAccountId AndAlso bra.DocumentDate < documentDate AndAlso Not brad.Reconciled AndAlso brad.ReconciledStatus = 1
                      Select New With {
                          .Detail = brad,
                          .OriginalDocumentDate = bra.DocumentDate
                      }).ToList()
        If result IsNot Nothing Then
            Return result.Select(Function(x)
                                     x.Detail.Period = GetPeriodFromDate(x.OriginalDocumentDate)
                                     If Not x.Detail.BankReconciliationAutomaticOriginId.HasValue Then
                                         x.Detail.BankReconciliationAutomaticOriginId = x.Detail.BankReconciliationAutomaticId
                                     End If
                                     Return x.Detail
                                 End Function).ToList()
        End If
        Return New List(Of BankReconciliationAutomaticDetail)()
    End Function
    ''' <summary>
    ''' Obtiene las partidas pendientes por conciliar para el segmento Extracto Bancario
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Public Function GetPendingItemsExtract(entityBankAccountId As Integer, documentDate As Date) As List(Of BankReconciliationAutomaticExtractDetail) Implements IBankReconciliationAutomaticRepository.GetPendingItemsExtract
        Dim result = (From braed In _context.BankReconciliationAutomaticExtractDetail
                      Let originalBraId = If(braed.BankReconciliationAutomaticOriginId.HasValue, braed.BankReconciliationAutomaticOriginId.Value, braed.BankReconciliationAutomaticId)
                      Join bra In _context.BankReconciliationAutomatic On bra.Id Equals originalBraId
                      Where bra.Status = 2 AndAlso bra.EntityBankAccountId = entityBankAccountId AndAlso bra.DocumentDate < documentDate AndAlso Not braed.Reconciled
                      Select New With {
                          .Detail = braed,
                          .OriginalDocumentDate = bra.DocumentDate
                          }).ToList()
        If result IsNot Nothing Then
            Return result.Select(Function(x)
                                     x.Detail.Period = GetPeriodFromDate(x.OriginalDocumentDate)
                                     If Not x.Detail.BankReconciliationAutomaticOriginId.HasValue Then
                                         x.Detail.BankReconciliationAutomaticOriginId = x.Detail.BankReconciliationAutomaticId
                                     End If
                                     Return x.Detail
                                 End Function).ToList()
        End If
        Return New List(Of BankReconciliationAutomaticExtractDetail)()
    End Function

    ''' <summary>
    ''' Función auxiliar para obtener el Periodo de las partidas pendientes por conciliar
    ''' </summary>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Private Function GetPeriodFromDate(documentDate As Date) As String Implements IBankReconciliationAutomaticRepository.GetPeriodFromDate
        Dim header As String = "Partida pendiente de conciliar: "
        Dim period As String = MonthName(Month(documentDate)) + " de " + Year(documentDate).ToString
        Return header + period
    End Function

    ''' <summary>
    ''' Retorna la lista de recibos de caja relacionados con las Notas de Gastos Bancarios
    ''' </summary>
    ''' <param name="noteIds"></param>
    ''' <returns></returns>
    Public Function GetListCashReceiptsBulk(noteIds As List(Of Integer)) As Dictionary(Of Integer, List(Of CashReceipts)) Implements IBankReconciliationAutomaticRepository.GetListCashReceiptsBulk
        If noteIds Is Nothing OrElse Not noteIds.Any() Then
            Return New Dictionary(Of Integer, List(Of CashReceipts))()
        End If

        Dim query = (
            From cr In _context.CashReceipts
            Join tncrd In _context.TreasuryNoteCashReceiptsDetail On cr.Id Equals tncrd.CashReceiptsId
            Join tp In _context.ThirdParty On cr.IdThirdParty Equals tp.Id
            Join pm In _context.PaymentMethods On cr.Id Equals pm.IdCashReceipt
            Group Join c In _context.Cards On pm.IdCard Equals c.Id Into CardJoin = Group
            From c In CardJoin.DefaultIfEmpty()
            Where noteIds.Contains(tncrd.TreasuryNoteId) AndAlso cr.Status = 2
            Select New With {
                .TreasuryNoteId = tncrd.TreasuryNoteId,
                .CashReceipt = cr,
                .CardName = If(c IsNot Nothing, c.Code & " - " & c.Name, String.Empty),
                .NitNameThirdParty = tp.Nit & " - " & tp.Name
            }
        ).ToList()

        ' Asignar propiedades adicionales y agrupar por NoteId
        For Each item In query
            item.CashReceipt.CardName = item.CardName
            item.CashReceipt.NitNameThirdParty = item.NitNameThirdParty
        Next

        ' Retornar diccionario agrupado por TreasuryNoteId
        Return query.GroupBy(Function(x) x.TreasuryNoteId).ToDictionary(
            Function(g) g.Key,
            Function(g) g.Select(Function(x) x.CashReceipt).ToList()
        )
    End Function


#End Region

End Class
