'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities


Public Class SettingPortfolioRepository
    Inherits GenericRepository(Of SettingPortfolio)
    Implements ISettingPortfolioRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    

    Public Function GetSettingPortfolioByIdOperatingUnit(idOperatingUnit As Integer) As SettingPortfolio Implements ISettingPortfolioRepository.GetSettingPortfolioByIdOperatingUnit
        Dim res = (From sp In _context.SettingPortfolio.Include("DeteriorationBasicBillingPortfolio").Include("RulesDeteriorationClassification") Where sp.OperatingUnitId = idOperatingUnit Select sp).FirstOrDefault()
        If res IsNot Nothing Then
            Dim creditNote = (From jvt In _context.JournalVoucherTypes.AsNoTracking() Where jvt.Id = res.JournalVoucherTypeCreditNotesId Select jvt).FirstOrDefault()
            res.CodeNameJournalVoucerTypeCreditNote = creditNote.Code + " - " + creditNote.Name
            Dim debitNote = (From jvt In _context.JournalVoucherTypes.AsNoTracking() Where jvt.Id = res.JournalVoucherTypeDebitNotesId Select jvt).FirstOrDefault()
            res.CodeNameJournalVoucerTypeDebitNote = debitNote.Code + " - " + debitNote.Name
            Dim transfer = (From jvt In _context.JournalVoucherTypes.AsNoTracking() Where jvt.Id = res.JournalVoucherTypeTranslationId Select jvt).FirstOrDefault()
            res.CodeNameJournalVoucerTypeTransfer = transfer.Code + " - " + transfer.Name
            Dim provision = (From jvt In _context.JournalVoucherTypes.AsNoTracking() Where jvt.Id = res.JournalVoucherTypeProvisionId Select jvt).FirstOrDefault()
            res.CodeNameJournalVoucerTypeProvision = provision.Code + " - " + provision.Name
            Dim filing = (From jvt In _context.JournalVoucherTypes.AsNoTracking() Where jvt.Id = res.JournalVoucherTypeFilingAccountId Select jvt).FirstOrDefault()
            res.CodeNameJournalVoucerTypeFilingAccount = filing.Code + " - " + filing.Name
            Dim journalVouches = (From jvt In _context.JournalVoucherTypes.AsNoTracking() Where jvt.Id = res.JournalVoucherTypeDocumentAccountReceivableId Select jvt).FirstOrDefault()
            res.CodeNameJournalVoucherTypeDocumentAccountReceivable = journalVouches.Code + " - " + journalVouches.Name
            If res.JournalVoucherTypeDeteriorationAccountId IsNot Nothing Then
                Dim deterioration = (From jvt In _context.JournalVoucherTypes.AsNoTracking() Where jvt.Id = res.JournalVoucherTypeDeteriorationAccountId Select jvt).FirstOrDefault()
                res.CodeNameJournalVoucherTypeDeteriorationAccount = deterioration.Code + " - " + deterioration.Name
            End If
            res.OriginalValue = (From sp In _context.SettingPortfolio.AsNoTracking() Where sp.OperatingUnitId = idOperatingUnit Select sp).FirstOrDefault()
            res.Legalcol = (From sp In _context.SettingPortfolio.AsNoTracking() Where sp.OperatingUnitId = idOperatingUnit Select sp).FirstOrDefault().LegalCollection

            If res.DependencyId IsNot Nothing Then
                Dim dependency = (From d In _context.Dependency.AsNoTracking() Where d.Id = res.DependencyId).FirstOrDefault()
                If dependency IsNot Nothing Then
                    res.DependencyDescription = String.Concat(dependency.Code, " - ", dependency.Name)

                    Dim budgetaryValidity = (From bv In _context.BudgetaryValidity.AsNoTracking() Where bv.Id = dependency.BudgetaryValidityId Select bv).FirstOrDefault()

                    If budgetaryValidity IsNot Nothing Then
                        res.BudgetaryValidityId = budgetaryValidity.Id
                        res.BudgetaryValidityDescription = budgetaryValidity.Year

                        res.BudgetaryEntityId = budgetaryValidity.BudgetaryEntityId
                        res.BudgetaryEntityDescription = (From be In _context.BudgetaryEntity.AsNoTracking Where be.Id = budgetaryValidity.BudgetaryEntityId Select String.Concat(be.Code, " - ", be.Name)).FirstOrDefault
                    End If
                End If
            End If

            Return res
        Else
            Return New SettingPortfolio
        End If
    End Function
End Class
