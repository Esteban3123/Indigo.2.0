'***********************************************************************
' Assembly         : Infrastructure.Data.BillingRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 17-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class SettingsBillingRepository
    Inherits GenericRepository(Of SettingsBilling)
    Implements ISettingsBillingRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetSettingsBillingById(id As Integer, tracking As Boolean) As SettingsBilling Implements ISettingsBillingRepository.GetSettingsBillingById
        Dim res As List(Of SettingsBilling)
        If tracking Then
            res = (From d As SettingsBilling In Me._context.SettingsBilling Where d.Id = id Select d).ToList()
        Else
            res = (From d As SettingsBilling In Me._context.SettingsBilling.AsNoTracking() Where d.Id = id Select d).ToList()
        End If

        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As SettingsBilling In Me._context.SettingsBilling.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New SettingsBilling()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    ''' <param name="IdOperatingUnit"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetCustomTRMByOperatingUnitId(IdOperatingUnit As Integer, tracking As Boolean) As List(Of CustomTRM) Implements ISettingsBillingRepository.GetCustomTRMByOperatingUnitId
        Dim res As List(Of CustomTRM)
        If tracking Then
            res = (From d As CustomTRM In Me._context.CustomTRM Where d.OperatingUnitId = IdOperatingUnit Select d).ToList()
        Else
            res = (From d As CustomTRM In Me._context.CustomTRM.AsNoTracking() Where d.OperatingUnitId = IdOperatingUnit Select d).ToList()
        End If

        If res IsNot Nothing AndAlso res.Count > 0 Then
            For Each item In res
                item.CurrencyName = (From d As Currency In Me._context.Currency.AsNoTracking() Where d.Id = item.CurrencyId Select d).FirstOrDefault()?.Abbreviation
            Next
            Return res
        Else
            Return New List(Of CustomTRM)
        End If
    End Function

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    ''' <param name="IdUnitOperative"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetSettingsBillingByIdUnitOperative(IdUnitOperative As Integer, tracking As Boolean) As SettingsBilling Implements ISettingsBillingRepository.GetSettingsBillingByIdUnitOperative
        Dim res As List(Of SettingsBilling)
        If tracking Then
            res = (From d As SettingsBilling In Me._context.SettingsBilling.Include("Currency") Where d.IdOperatingUnit = IdUnitOperative Select d).ToList()
        Else
            res = (From d As SettingsBilling In Me._context.SettingsBilling.Include("Currency").AsNoTracking() Where d.IdOperatingUnit = IdUnitOperative Select d).ToList()
        End If
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Dim parameter = res(0)
            parameter.OriginalValue = (From d As SettingsBilling In Me._context.SettingsBilling.AsNoTracking() Where d.IdOperatingUnit = IdUnitOperative Select d).SingleOrDefault()
            If parameter.ReteIVAConceptId IsNot Nothing Then
                Dim iva = (From r In Me._context.RetentionConcepts.AsNoTracking() Where r.Id = parameter.ReteIVAConceptId Select r).FirstOrDefault()
                parameter.RetentionPercentageIVA = iva.Rate
                parameter.RetentionBaseIVA = iva.MinBase
            End If
            Return parameter
        Else
            Return New SettingsBilling()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro de parametros de facturacion por el id de la unidad operativa para cargar el formulario
    ''' </summary>
    ''' <param name="operatingUnitId">id de la unidad operativa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSettingBillingByUnitOperativeForm(operatingUnitId As Integer) As SettingsBilling Implements ISettingsBillingRepository.GetSettingBillingByUnitOperativeForm
        Dim res = (From insett In _context.SettingsBilling Where insett.IdOperatingUnit = operatingUnitId Select insett).FirstOrDefault
        If res IsNot Nothing Then

            If res.ParticularHealthAdministratorId IsNot Nothing Then
                res.ParticularHealthAdministratorDescription = (From ha In _context.HealthAdministrator.AsNoTracking Where ha.Id = res.ParticularHealthAdministratorId Select String.Concat(ha.Code, " - ", ha.Name)).FirstOrDefault()
            End If

            If res.SpecificCurrencyId IsNot Nothing Then
                res.Currency = (From c In _context.Currency.AsNoTracking Where c.Id = res.SpecificCurrencyId)?.FirstOrDefault
            End If

            'Tipos de comprobante contables
            Dim journalVoucherType As JournalVoucherTypes

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.InvoiceJournalVoucherTypeId Select jvt).FirstOrDefault
            res.InvoiceJournalVoucherTypeDescription = journalVoucherType?.Code + " - " + journalVoucherType?.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.InvoiceAnnulmentJournalVoucherTypeId Select jvt).FirstOrDefault
            res.InvoiceAnnulmentJournalVoucherTypeDescription = journalVoucherType?.Code + " - " + journalVoucherType?.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.ReverseTransferJournalVoucherTypeId Select jvt).FirstOrDefault
            res.ReverseTransferJournalVoucherTypeDescription = journalVoucherType?.Code + " - " + journalVoucherType?.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.ReverseRecognitionJournalVoucherTypeId Select jvt).FirstOrDefault
            res.ReverseRecognitionJournalVoucherTypeDescription = journalVoucherType?.Code + " - " + journalVoucherType?.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.RecognitionJournalVoucherTypeId Select jvt).FirstOrDefault
            res.RecognitionJournalVoucherTypeDescription = journalVoucherType?.Code + " - " + journalVoucherType?.Name

            If res.ApplyBasicBilling Then
                journalVoucherType = (From f In _context.JournalVoucherTypes Where f.Id = res.ConsignmentSalereCognition Select f).FirstOrDefault
                res.ConsignmentSalereCognitionName = journalVoucherType?.Code + " - " + journalVoucherType?.Name

                journalVoucherType = (From f In _context.JournalVoucherTypes Where f.Id = res.ReversalRecognitionConsignmentSale Select f).FirstOrDefault
                res.ReversalRecognitionConsignmentSaleName = journalVoucherType?.Code + " - " + journalVoucherType?.Name
            End If

            If res.ApplyElectronicSalesTicket Then
                journalVoucherType = (From f In _context.JournalVoucherTypes.AsNoTracking() Where f.Id = res.AccountingVoucherGenerationId Select f).FirstOrDefault
                res.AccountingVoucherGenerationName = journalVoucherType?.Code + " - " + journalVoucherType?.Name

                journalVoucherType = (From f In _context.JournalVoucherTypes.AsNoTracking() Where f.Id = res.AccountingVoucherReversalId Select f).FirstOrDefault
                res.AccountingVoucherReversalName = journalVoucherType?.Code + " - " + journalVoucherType?.Name

                res.BillingAuthorizationName = (From m In _context.BillingAuthorization.AsNoTracking() Where m.Id = res.BillingAuthorizationId.Value Select String.Concat(m.Code, " - ", m.Name)).FirstOrDefault()
            End If

            res.ProductInvoiceJournalVoucherTypeDescription = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.ProductInvoiceJournalVoucherTypeId Select String.Concat(jvt.Code, " - ", jvt.Name)).FirstOrDefault()

            If res.BasicBillingJournalVoucherTypeId IsNot Nothing Then
                res.BasicBillingJournalVoucherTypeDescription = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.BasicBillingJournalVoucherTypeId Select String.Concat(jvt.Code, " - ", jvt.Name)).FirstOrDefault()
            End If

            If res.BasicBillingAnnulmentJournalVoucherTypeId IsNot Nothing Then
                res.BasicBillingAnnulmentJournalVoucherTypeDescription = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.BasicBillingAnnulmentJournalVoucherTypeId Select String.Concat(jvt.Code, " - ", jvt.Name)).FirstOrDefault()
            End If

            If res.LiquidatedPackageJournalVoucherTypeId IsNot Nothing Then
                res.LiquidatedPackageJournalVoucherTypeDescription = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.LiquidatedPackageJournalVoucherTypeId Select String.Concat(jvt.Code, " - ", jvt.Name)).FirstOrDefault()
            End If

            If res.ReversionLiquidatedPackageJournalVoucherTypeId IsNot Nothing Then
                res.ReversionLiquidatedPackageJournalVoucherTypeDescription = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.ReversionLiquidatedPackageJournalVoucherTypeId Select String.Concat(jvt.Code, " - ", jvt.Name)).FirstOrDefault()
            End If

            If res.InvoiceEntityCapitatedDistributionJournalVoucherTypeId IsNot Nothing Then
                res.InvoiceEntityCapitatedDistributionJournalVoucherTypeDescription = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.InvoiceEntityCapitatedDistributionJournalVoucherTypeId Select String.Concat(jvt.Code, " - ", jvt.Name)).FirstOrDefault()
            End If

            If res.ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId IsNot Nothing Then
                res.ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeDescription = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.ReverseInvoiceEntityCapitatedDistributionJournalVoucherTypeId Select String.Concat(jvt.Code, " - ", jvt.Name)).FirstOrDefault()
            End If

            If res.BillingAuthorizationCopayId.HasValue Then
                res.BillingAuthorizationCopayCodeName = (From m In _context.BillingAuthorization.AsNoTracking() Where m.Id = res.BillingAuthorizationCopayId.Value Select String.Concat(m.Code, " - ", m.Name)).FirstOrDefault()
            End If

            If res.SpecificCurrencyId IsNot Nothing Then
                res.CodeNameSpecificCurrency = (From x In _context.Currency Where x.Id = res.SpecificCurrencyId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
            End If

            'Cuentas Contables
            Dim mainAccount As MainAccounts

            mainAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.CapitationRevenueMainAccountId Select ma).FirstOrDefault
            res.CapitationRevenueMainAccountDescription = mainAccount?.Number + " - " + mainAccount?.Name

            mainAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.CapitationProfitMainAccountId Select ma).FirstOrDefault
            res.CapitationProfitMainAccountDescription = mainAccount?.Number + " - " + mainAccount?.Name

            mainAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.CapitationLossMainAccountId Select ma).FirstOrDefault
            res.CapitationLossMainAccountDescription = mainAccount?.Number + " - " + mainAccount?.Name

            mainAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.RecoveryFeeDiscountMainAccountId Select ma).FirstOrDefault
            res.RecoveryFeeDiscountMainAccountDescription = mainAccount?.Number + " - " + mainAccount?.Name

            If mainAccount.HandlesCostCenter AndAlso res.RecoveryFeeDiscountCostCenterId IsNot Nothing Then
                Dim costcenterDescription = (From c In _context.CostCenter.AsNoTracking Where c.Id = res.RecoveryFeeDiscountCostCenterId Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault()
                res.RecoveryFeeDiscountCostCenterDescription = costcenterDescription
            End If

            If res.ClientMainAccountId IsNot Nothing Then
                res.ClientMainAccountDescription = (From x In _context.MainAccounts.AsNoTracking() Where x.Id = res.ClientMainAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.IVAPaymentMainAccountId IsNot Nothing Then
                res.IVAPaymentMainAccountDescription = (From x In _context.MainAccounts.AsNoTracking() Where x.Id = res.IVAPaymentMainAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.ReteIVAMainAccountId IsNot Nothing Then
                res.ReteIVAMainAccountDescription = (From x In _context.MainAccounts.AsNoTracking() Where x.Id = res.ReteIVAMainAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.AccountingPackageMainAccountId IsNot Nothing Then
                res.AccountingPackageMainAccountDescription = (From x In _context.MainAccounts.AsNoTracking() Where x.Id = res.AccountingPackageMainAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.ProjectedVariationPriceMainAccountId IsNot Nothing Then
                res.ProjectedVariationPriceMainAccountDescription = (From x In _context.MainAccounts.AsNoTracking() Where x.Id = res.ProjectedVariationPriceMainAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.ReteICAMainAccountId IsNot Nothing Then
                res.ReteICAMainAccountDescription = (From x In _context.MainAccounts.AsNoTracking() Where x.Id = res.ReteICAMainAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.ReteFuenteMainAccountId IsNot Nothing Then
                res.ReteFuenteMainAccountDescription = (From x In _context.MainAccounts.AsNoTracking() Where x.Id = res.ReteFuenteMainAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            'Autorizacion de facturacion
            res.EntityCapitatedBillingAuthorizationDescription = (From ba In _context.BillingAuthorization.AsNoTracking() Where ba.Id = res.EntityCapitatedBillingAuthorizationId Select String.Concat(ba.Code, " - ", ba.Name)).FirstOrDefault()

            'conceptos de recibos de caja
            Dim cashReceiptConcept As CashReceiptConcepts

            cashReceiptConcept = (From crc In _context.CashReceiptConcepts.AsNoTracking Where crc.Id = res.PatientAdvanceCashReceiptConceptId Select crc).FirstOrDefault
            res.PatientAdvanceCashReceiptConceptDescription = cashReceiptConcept?.Code + " - " + cashReceiptConcept?.Name

            If res.CapitedPatientAdvanceCashReceiptConceptId IsNot Nothing Then
                res.CapitedPatientAdvanceCashReceiptConceptDescription = (From crc In _context.CashReceiptConcepts.AsNoTracking Where crc.Id = res.CapitedPatientAdvanceCashReceiptConceptId Select String.Concat(crc.Code, " - ", crc.Name)).FirstOrDefault()
            End If

            cashReceiptConcept = (From crc In _context.CashReceiptConcepts.AsNoTracking Where crc.Id = res.IndvidualAdvanceCashReceiptConceptId Select crc).FirstOrDefault
            res.IndvidualAdvanceCashReceiptConceptDescription = cashReceiptConcept?.Code + " - " + cashReceiptConcept?.Name

            cashReceiptConcept = (From crc In _context.CashReceiptConcepts.AsNoTracking Where crc.Id = res.ProductSalesCashReceiptConceptId Select crc).FirstOrDefault
            res.CodeNameProductSalesCashReceiptConcept = cashReceiptConcept?.Code + " - " + cashReceiptConcept?.Name

            If res.BasicBillingCashReceiptConceptId IsNot Nothing Then
                res.BasicBillingCashReceiptConceptCodeName = (From x In _context.CashReceiptConcepts.AsNoTracking() Where x.Id = res.BasicBillingCashReceiptConceptId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
            End If

            mainAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = cashReceiptConcept.IdMainAccount Select ma).FirstOrDefault
            res.NumberNameProductSalesMainAccount = mainAccount?.Number + " - " + mainAccount?.Name

            'centro de costo
            If res.ProductSalesCostCenterId IsNot Nothing Then
                res.CodeNameCostCenter = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = res.ProductSalesCostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault()
            End If

            If res.ReteIVAConceptId IsNot Nothing Then
                res.ReteIVAConceptDescription = (From x In _context.RetentionConcepts.AsNoTracking() Where x.Id = res.ReteIVAConceptId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
            End If

            If res.FunctionalUnitId IsNot Nothing Then
                res.FunctionalUnitDescription = (From x In _context.FunctionalUnit.AsNoTracking() Where x.Id = res.FunctionalUnitId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
            End If

            If res.InvoiceProductDevolutionPartialConceptNoteId IsNot Nothing Then
                res.InvoiceProductDevolutionPartialConceptNoteDescription = (From c In _context.PortfolioNoteConcept.AsNoTracking() Where c.Id = res.InvoiceProductDevolutionPartialConceptNoteId Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault()
            End If

            If res.GiftProductOutletConcept IsNot Nothing Then
                res.GiftProductOutletConceptDescription = (From c In _context.AdjustmentConcept.AsNoTracking() Where c.Id = res.GiftProductOutletConcept Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault()
            End If

            If res.ReversalPreviousYearsMainAccountId IsNot Nothing Then
                res.ReversalPreviousYearsMainAccountIdDescription = (From c In _context.MainAccounts.AsNoTracking() Where c.Id = res.ReversalPreviousYearsMainAccountId Select String.Concat(c.Number, " - ", c.Name)).FirstOrDefault()
            End If

            If res.ReversalPreviousYearsGenericBillingMainAccountId IsNot Nothing Then
                res.ReversalPreviousYearsGenericBillingMainAccountIdDescription = (From c In _context.MainAccounts.AsNoTracking() Where c.Id = res.ReversalPreviousYearsGenericBillingMainAccountId Select String.Concat(c.Number, " - ", c.Name)).FirstOrDefault()
            End If

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

            If res.BasicBillingDependencyId IsNot Nothing Then
                Dim dependency = (From d In _context.Dependency.AsNoTracking() Where d.Id = res.BasicBillingDependencyId).FirstOrDefault()
                If dependency IsNot Nothing Then
                    res.BasicBillingDependencyDescription = String.Concat(dependency.Code, " - ", dependency.Name)
                    If res.BudgetaryValidityId Is Nothing Then
                        Dim budgetaryValidity = (From bv In _context.BudgetaryValidity.AsNoTracking() Where bv.Id = dependency.BudgetaryValidityId Select bv).FirstOrDefault()

                        If budgetaryValidity IsNot Nothing Then
                            res.BudgetaryValidityId = budgetaryValidity.Id
                            res.BudgetaryValidityDescription = budgetaryValidity.Year

                            res.BudgetaryEntityId = budgetaryValidity.BudgetaryEntityId
                            res.BudgetaryEntityDescription = (From be In _context.BudgetaryEntity.AsNoTracking Where be.Id = budgetaryValidity.BudgetaryEntityId Select String.Concat(be.Code, " - ", be.Name)).FirstOrDefault
                        End If
                    End If
                End If
            End If

            If res.BasicBillingBudgetId IsNot Nothing Then
                Dim budget = (From b In _context.Budget.AsNoTracking().Include("Category").AsNoTracking().Include("Category.FinancialSource").AsNoTracking().Include("RevenueType").AsNoTracking() Where b.Id = res.BasicBillingBudgetId).FirstOrDefault()
                If budget IsNot Nothing Then
                    res.BasicBillingBudgetDescription = String.Concat(budget.Category.Code, " - ", budget.Category.Name, " - ", budget.Category.FinancialSource.Code, " - ", budget.Category.FinancialSource.Name, " - ", budget.RevenueType.Code, " - ", budget.RevenueType.Name)
                    If res.BudgetaryValidityId Is Nothing Then
                        Dim budgetaryValidity = (From bh In _context.BudgetHeader.AsNoTracking()
                                                 Join bv In _context.BudgetaryValidity.AsNoTracking()
                                                On bh.BudgetaryValidityId Equals bv.Id
                                                 Where bh.Id = budget.BudgetHeaderId Select bv).FirstOrDefault()

                        If budgetaryValidity IsNot Nothing Then
                            res.BudgetaryValidityId = budgetaryValidity.Id
                            res.BudgetaryValidityDescription = budgetaryValidity.Year

                            res.BudgetaryEntityId = budgetaryValidity.BudgetaryEntityId
                            res.BudgetaryEntityDescription = (From be In _context.BudgetaryEntity.AsNoTracking Where be.Id = budgetaryValidity.BudgetaryEntityId Select String.Concat(be.Code, " - ", be.Name)).FirstOrDefault
                        End If
                    End If
                End If
            End If

            'Concept de Estado de Folio
            If res.StatusFolioNewId IsNot Nothing OrElse res.StatusFolioNewId > 0 Then
                Dim StatusFolioNew = (From S In _context.ConceptsCausesStatusFolio.AsNoTracking() Where S.Id = res.StatusFolioNewId).FirstOrDefault
                res.StatusFolioNewDescription = StatusFolioNew.Code & " - " & StatusFolioNew.Name
            End If

            If res.StatusFolioClosedId IsNot Nothing OrElse res.StatusFolioClosedId > 0 Then
                Dim StatusFolioClosed = (From S In _context.ConceptsCausesStatusFolio.AsNoTracking() Where S.Id = res.StatusFolioClosedId).FirstOrDefault
                res.StatusFolioClosedDescription = StatusFolioClosed.Code & " - " & StatusFolioClosed.Name
            End If

            res.OriginalValue = (From si In _context.SettingsBilling.AsNoTracking Select si).FirstOrDefault

            Return res
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de registros de facturas de tipo de documento 5
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CountBillingInvoice() As Integer Implements ISettingsBillingRepository.CountBillingInvoice

        Dim countInvoice = Aggregate bi In _context.Invoice.AsNoTracking Where bi.DocumentType = 5 Into Count()
        Return countInvoice

    End Function

    Public Function GetReportBillingStadistics(XmlCriterials As String, XmlFilters As String) As List(Of SP_ReportBillingStadistics_Result) Implements ISettingsBillingRepository.GetReportBillingStadistics
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ReportBillingStadistics(XmlCriterials, XmlFilters).ToList()
    End Function

    Public Function GetReportBillingStadisticsCount(XmlCriterials As String, XmlFilters As String) As SP_ReportBillingStadistics_Count_Result Implements ISettingsBillingRepository.ReportBillingStadisticsCount
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ReportBillingStadistics_Count(XmlCriterials, XmlFilters).FirstOrDefault()
    End Function
End Class