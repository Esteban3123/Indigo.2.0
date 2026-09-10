'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 22/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Public Class IPSServiceGroupRepository
    Inherits GenericRepository(Of BillingConcept)
    Implements IIPSServiceGroupRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un grupo de servicios Ips por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetIPSServiceGroup(code As String) As BillingConcept Implements IIPSServiceGroupRepository.GetIPSServiceGroup
        Dim res = (From isg In _context.BillingConcept.Include("BillingConceptAccount").Include("BillingConceptCostCenter").Include("BillingConceptAccountingPackage") Where isg.Code = code Select isg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From isg In _context.BillingConcept.AsNoTracking() Where isg.Code = code Select isg).FirstOrDefault()

            If res.CostCenterId IsNot Nothing Then
                res.CodeNameCostCenter = (From cc In _context.CostCenter.AsNoTracking Where cc.Id = res.CostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault
            End If

            If res.CopayMainAccountId.HasValue Then
                res.CopayMainAccountDescription = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.CopayMainAccountId.Value Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.RecoveryFixedAmountMainAccountId.HasValue Then
                res.RecoveryFixedAmountMainAccountDescription = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.RecoveryFixedAmountMainAccountId.Value Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.EntityIncomeAccountId IsNot Nothing Then
                res.CodeNameEntityIncomeAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.EntityIncomeAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.IndividualIncomeAccountId IsNot Nothing Then
                res.CodeNameIndividualIncomeAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.IndividualIncomeAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            res.CodeNameDiscountAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.DiscountAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()

            If res.FeesExpensesAccountId IsNot Nothing Then
                res.CodeNameFeesExpensesAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.FeesExpensesAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.IncomeRecognitionPendingBillingMainAccountId IsNot Nothing Then
                res.IncomeRecognitionPendingBillingMainAccountDescription = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.IncomeRecognitionPendingBillingMainAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.IVAAccountId IsNot Nothing Then
                res.CodeNameIVAAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.IVAAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.WithholdingTaxAccountId IsNot Nothing Then
                res.CodeNameWithholdingTaxAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.WithholdingTaxAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.AssociatedMainServiceId IsNot Nothing Then
                res.AssociatedMainServiceName = (From r In _context.BillingConcept Where r.Id = res.AssociatedMainServiceId Select String.Concat(r.Code, " - ", r.Name)).FirstOrDefault()
            End If

            If res.WithholdingICAAccountId IsNot Nothing Then
                res.CodeNameWithholdingICAAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.WithholdingICAAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.IVAId IsNot Nothing Then
                Dim iva = (From ma In _context.GeneralLedgerIVA.AsNoTracking() Where ma.Id = res.IVAId Select ma).FirstOrDefault()
                res.CodeNameIVA = String.Concat(iva.Code, " - ", iva.Name)
                res.PercentageIVA = iva.Percentage
            End If

            If res.WithholdingTaxConceptId IsNot Nothing Then
                Dim tax = (From ma In _context.RetentionConcepts.AsNoTracking() Where ma.Id = res.WithholdingTaxConceptId Select ma).FirstOrDefault()
                res.RetentionIdTax = tax.Id
                res.CodeNameWithholdingTaxConcept = String.Concat(tax.Code, " - ", tax.Name)
                res.RetentionPercentageTax = tax.Rate
                res.RetentionBaseTax = tax.MinBase
            End If

            If res.WithholdingICAConceptId IsNot Nothing Then
                Dim ica = (From ma In _context.RetentionConcepts.AsNoTracking() Where ma.Id = res.WithholdingICAConceptId Select ma).FirstOrDefault()
                res.RetentionIdICA = ica.Id
                res.CodeNameWithholdingICAConcept = String.Concat(ica.Code, " - ", ica.Name)
                res.RetentionPercentageICA = ica.Rate
                res.RetentionBaseICA = ica.MinBase
            End If

            If res.BillingConceptCostCenter IsNot Nothing AndAlso res.BillingConceptCostCenter.Any() Then
                For Each billingConceptCostCenter In res.BillingConceptCostCenter
                    billingConceptCostCenter.BranchOfficeCodeName = (From bo In _context.BranchOffice.AsNoTracking() Where bo.Id = billingConceptCostCenter.BranchOfficeId Select String.Concat(bo.Code, " - ", bo.Name)).FirstOrDefault()
                    billingConceptCostCenter.FunctionalUnitCodeName = (From fu In _context.FunctionalUnit.AsNoTracking() Where fu.Id = billingConceptCostCenter.FunctionalUnitId Select String.Concat(fu.Code, " - ", fu.Name)).FirstOrDefault()
                    billingConceptCostCenter.CostCenterCodeName = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = billingConceptCostCenter.CostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault()
                Next
            End If

            Return res
        Else
            Return New BillingConcept
        End If
    End Function

    ''' <summary>
    ''' Obtiene un grupo de servicios Ips por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetIPSServiceGroupById(id As Integer) As BillingConcept Implements IIPSServiceGroupRepository.GetIPSServiceGroupById
        Dim res = (From isg In _context.BillingConcept Where isg.Id = id Select isg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From isg In _context.BillingConcept.AsNoTracking() Where isg.Id = id Select isg).FirstOrDefault()

            If res.CostCenterId IsNot Nothing Then
                res.CodeNameCostCenter = (From cc In _context.CostCenter.AsNoTracking Where cc.Id = res.CostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault
            End If

            If res.EntityIncomeAccountId IsNot Nothing Then
                res.CodeNameEntityIncomeAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.EntityIncomeAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.IndividualIncomeAccountId IsNot Nothing Then
                res.CodeNameIndividualIncomeAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.IndividualIncomeAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            res.CodeNameDiscountAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.DiscountAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()

            If res.FeesExpensesAccountId IsNot Nothing Then
                res.CodeNameFeesExpensesAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.FeesExpensesAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.IncomeRecognitionPendingBillingMainAccountId IsNot Nothing Then
                res.IncomeRecognitionPendingBillingMainAccountDescription = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.IncomeRecognitionPendingBillingMainAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.IVAAccountId IsNot Nothing Then
                res.CodeNameIVAAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.IVAAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.WithholdingTaxAccountId IsNot Nothing Then
                res.CodeNameWithholdingTaxAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.WithholdingTaxAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.WithholdingICAAccountId IsNot Nothing Then
                res.CodeNameWithholdingICAAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.WithholdingICAAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
            End If

            If res.IVAId IsNot Nothing Then
                Dim iva = (From ma In _context.GeneralLedgerIVA.AsNoTracking() Where ma.Id = res.IVAId Select ma).FirstOrDefault()
                res.CodeNameIVA = String.Concat(iva.Code, " - ", iva.Name)
                res.PercentageIVA = iva.Percentage
            End If

            If res.WithholdingTaxConceptId IsNot Nothing Then
                Dim tax = (From ma In _context.RetentionConcepts.AsNoTracking() Where ma.Id = res.WithholdingTaxConceptId Select ma).FirstOrDefault()
                res.RetentionIdTax = tax.Id
                res.CodeNameWithholdingTaxConcept = String.Concat(tax.Code, " - ", tax.Name)
                res.RetentionPercentageTax = tax.Rate
                res.RetentionBaseTax = tax.MinBase
            End If

            If res.WithholdingICAConceptId IsNot Nothing Then
                Dim ica = (From ma In _context.RetentionConcepts.AsNoTracking() Where ma.Id = res.WithholdingICAConceptId Select ma).FirstOrDefault()
                res.RetentionIdICA = ica.Id
                res.CodeNameWithholdingICAConcept = String.Concat(ica.Code, " - ", ica.Name)
                res.RetentionPercentageICA = ica.Rate
                res.RetentionBaseICA = ica.MinBase
            End If

            res.CountSecondaryService = (From ss In _context.BillingConcept Where ss.AssociatedMainServiceId = res.Id Select ss).ToList.Count

            Return res
        Else
            Return New BillingConcept
        End If
    End Function

    ''' <summary>
    ''' Gets the billing concept by identifier includes.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="includes"></param>
    ''' <returns></returns>
    Public Function GetBillingConceptByIdIncludes(id As Integer, includes() As String) As BillingConcept Implements IIPSServiceGroupRepository.GetBillingConceptByIdIncludes
        Dim query As IQueryable(Of BillingConcept) = _context.BillingConcept.Where(Function(bc) bc.Id = id).AsQueryable()
        If includes IsNot Nothing Then
            includes.ToList().ForEach(Sub(include)
                                          query = query.Include(include)
                                      End Sub)
        End If
        Return query.FirstOrDefault()
    End Function

    Public Function GetBillingConceptById(Id As Integer, Optional tracking As Boolean = True) As BillingConcept Implements IIPSServiceGroupRepository.GetBillingConceptById
        Dim res As BillingConcept
        If tracking Then
            res = (From bc In _context.BillingConcept Where bc.Id = Id Select bc).FirstOrDefault()
        Else
            res = (From bc In _context.BillingConcept.AsNoTracking().Include("BillingConceptAccount").AsNoTracking() Where bc.Id = Id Select bc).FirstOrDefault()
        End If
        If res IsNot Nothing Then
            Return res
        End If
        Return New BillingConcept
    End Function

    ''' <summary>
    ''' Copia y pega los centros de costo por sucursal y unidad funcional
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SP_CopyAndPasteBillingConceptCostCenter(XmlObject As String) As List(Of SP_CopyAndPasteBillingConceptCostCenter_Result) Implements IIPSServiceGroupRepository.SP_CopyAndPasteBillingConceptCostCenter
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteBillingConceptCostCenter(XmlObject).ToList
    End Function
End Class
