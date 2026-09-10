#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Infrastructure

#End Region

Public Class JournalVoucherDetailRepository
    Inherits GenericRepository(Of JournalVoucherDetails)
    Implements IJournalVoucherDetailRepository

#Region "Properties"

    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builder"

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

#End Region

#Region "Methods"

    Private Function GetJournalVoucherDetails(journalVoucherId As Integer) As List(Of JournalVoucherDetails) Implements IJournalVoucherDetailRepository.GetJournalVoucherDetails
        Dim journalVoucherDetails = (From jvd In _context.JournalVoucherDetails.AsNoTracking() Where jvd.IdAccounting = journalVoucherId Select jvd).ToList()

        'Items Individuals
        Dim CodeNameMainAccount = String.Empty
        Dim CodeNameThirdParty = String.Empty
        Dim CodeNameCostCenter = String.Empty
        Dim CodeNameRetention = String.Empty

        'Dictionaries        
        Dim dictionaryMainAccounts As New Dictionary(Of Integer, String)()
        Dim dictionaryThirdParties As New Dictionary(Of Integer, String)()
        Dim dictionaryCostCenters As New Dictionary(Of Integer, String)()
        Dim dictionaryConceptRetentions As New Dictionary(Of Integer, String)()

        For Each journalVoucherDetail In journalVoucherDetails
            'CUENTA CONTABLE
            If Not dictionaryMainAccounts.ContainsKey(journalVoucherDetail.IdMainAccount) Then
                CodeNameMainAccount = (From m In _context.MainAccounts.AsNoTracking Where journalVoucherDetail.IdMainAccount = m.Id Select String.Concat(m.Number, " - ", m.Name)).FirstOrDefault
                dictionaryMainAccounts.Add(journalVoucherDetail.IdMainAccount, CodeNameMainAccount)
            Else
                CodeNameMainAccount = dictionaryMainAccounts(journalVoucherDetail.IdMainAccount)
            End If
            journalVoucherDetail.CodeNameMainAccount = CodeNameMainAccount

            'TERCERO
            If journalVoucherDetail.IdThirdParty IsNot Nothing Then
                If Not dictionaryThirdParties.ContainsKey(journalVoucherDetail.IdThirdParty) Then
                    CodeNameThirdParty = (From m In _context.ThirdParty.AsNoTracking Where journalVoucherDetail.IdThirdParty = m.Id Select String.Concat(m.Nit, " - ", m.Name)).FirstOrDefault
                    dictionaryThirdParties.Add(journalVoucherDetail.IdThirdParty, CodeNameThirdParty)
                Else
                    CodeNameThirdParty = dictionaryThirdParties(journalVoucherDetail.IdThirdParty)
                End If
                journalVoucherDetail.CodeNameThirdParty = CodeNameThirdParty
            End If

            'CENTRO DE COSTO
            If journalVoucherDetail.IdCostCenter IsNot Nothing Then
                If Not dictionaryCostCenters.ContainsKey(journalVoucherDetail.IdCostCenter) Then
                    CodeNameCostCenter = (From m In _context.CostCenter.AsNoTracking Where journalVoucherDetail.IdCostCenter = m.Id Select String.Concat(m.Code, " - ", m.Name)).FirstOrDefault
                    dictionaryCostCenters.Add(journalVoucherDetail.IdCostCenter, CodeNameCostCenter)
                Else
                    CodeNameCostCenter = dictionaryCostCenters(journalVoucherDetail.IdCostCenter)
                End If
                journalVoucherDetail.CodeNameCostCenter = CodeNameCostCenter
            End If

            'CONCEPTO DE RETENCION
            If journalVoucherDetail.IdRetention IsNot Nothing Then
                If Not dictionaryConceptRetentions.ContainsKey(journalVoucherDetail.IdRetention) Then
                    CodeNameRetention = (From m In _context.RetentionConcepts.AsNoTracking Where journalVoucherDetail.IdRetention = m.Id Select String.Concat(m.Code, " - ", m.Name)).FirstOrDefault
                    dictionaryConceptRetentions.Add(journalVoucherDetail.IdRetention, CodeNameRetention)
                Else
                    CodeNameRetention = dictionaryConceptRetentions(journalVoucherDetail.IdRetention)
                End If
                journalVoucherDetail.CodeNameRetention = CodeNameRetention
            End If
        Next

        Return journalVoucherDetails
    End Function

#End Region

End Class
