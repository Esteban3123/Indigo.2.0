'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ProductGroupsRepository
    Inherits GenericRepository(Of ProductGroup)
    Implements IProductGroupsRepository

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
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProductGroup(code As String) As ProductGroup Implements IProductGroupsRepository.GetProductGroup
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As ProductGroup In Me._context.ProductGroup.Include("ProductGroupFunctionalUnit")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim account

            account = (From ai In _context.MainAccounts.AsNoTracking Where ai.Id = res.IncomeAccountId Select ai).FirstOrDefault
            res.IncomeAccountDescription = account.Number + " - " + account.Name

            account = (From ai In _context.MainAccounts.AsNoTracking Where ai.Id = res.IncomeRecognitionMainAccountId Select ai).FirstOrDefault
            If account IsNot Nothing AndAlso account.Id > 0 Then
                res.IncomeRecognitionMainAccountDescription = account.Number + " - " + account.Name
            End If

            Dim accountPayableConcept = (From ap In _context.AccountPayableConcepts.AsNoTracking Where ap.Id = res.InventoryAccountPayableConceptId Select ap).FirstOrDefault
            res.InventoryAccountPayableConceptDescription = accountPayableConcept.Code + " - " + accountPayableConcept.Name

            accountPayableConcept = (From ap In _context.AccountPayableConcepts.AsNoTracking Where ap.Id = res.DeclarantRetentionAccountPayableConceptId Select ap).FirstOrDefault
            res.DeclarantRetentionAccountPayableConceptDescription = accountPayableConcept.Code + " - " + accountPayableConcept.Name

            accountPayableConcept = (From ap In _context.AccountPayableConcepts.AsNoTracking Where ap.Id = res.NotDeclarantRetentionAccountPayableConceptId Select ap).FirstOrDefault
            res.NotDeclarantRetentionAccountPayableConceptDescription = accountPayableConcept.Code + " - " + accountPayableConcept.Name

            Dim costCenter = (From cc In _context.CostCenter.AsNoTracking Where cc.Id = res.CostCenterId Select cc).FirstOrDefault
            res.CostCenterDescription = costCenter.Code + " - " + costCenter.Name

            account = (From id In _context.MainAccounts.AsNoTracking Where id.Id = res.ReferenceInputDebitAccountId Select id).FirstOrDefault
            res.RemissionInputDebitDescription = account.Number + " - " + account.Name

            account = (From ic In _context.MainAccounts.AsNoTracking Where ic.Id = res.ReferenceInputCreditAccountId Select ic).FirstOrDefault
            res.RemissionInputCreditDescription = account.Number + " - " + account.Name

            account = (From od In _context.MainAccounts.AsNoTracking Where od.Id = res.ReferenceOutputDebitAccountId Select od).FirstOrDefault
            res.RemissionOutputDebitDescription = account.Number + " - " + account.Name

            account = (From oc In _context.MainAccounts.AsNoTracking Where oc.Id = res.ReferenceOutputCreditAccountId Select oc).FirstOrDefault
            res.RemissionOutputCreditDescription = account.Number + " - " + account.Name

            account = (From od In _context.MainAccounts.AsNoTracking Where od.Id = res.ConsignmentMerchandiseDebitAccountId Select od).FirstOrDefault
            res.ConsignmentMerchandiseDebitDescription = account.Number + " - " + account.Name

            account = (From oc In _context.MainAccounts.AsNoTracking Where oc.Id = res.ConsignmentMerchandiseCreditAccountId Select oc).FirstOrDefault
            res.ConsignmentMerchandiseCreditDescription = account.Number + " - " + account.Name

            account = (From oc In _context.MainAccounts.AsNoTracking Where oc.Id = res.CounterpartCostConsignedInventoryId Select oc).FirstOrDefault
            res.CounterpartCostConsignedInventoryDescription = account.Number + " - " + account.Name

            If res.AccountingPackageMainAccountId IsNot Nothing Then
                res.CodeNameAccountingPackageMainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.AccountingPackageMainAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault
            End If

            If res.FavorableDeviationMainAccountId IsNot Nothing Then
                res.CodeNameFavorableDeviationMainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.FavorableDeviationMainAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault
            End If

            If res.VariationPVMainAccountId IsNot Nothing Then
                res.CodeNameVariationPVMainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.VariationPVMainAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault
            End If

            If res.InventoryCostMainAccountId IsNot Nothing Then
                res.InventoryCostMainAccountDescription = (From x In _context.MainAccounts.AsNoTracking() Where x.Id = res.InventoryCostMainAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.ReteFuenteConceptId IsNot Nothing Then
                res.ReteFuenteConceptDescription = (From x In _context.RetentionConcepts.AsNoTracking() Where x.Id = res.ReteFuenteConceptId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
            End If

            If res.IVAAccountId IsNot Nothing Then
                res.CodeNameIVAAccount = (From x In _context.MainAccounts.AsNoTracking Where x.Id = res.IVAAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.WithholdingTaxAccountId IsNot Nothing Then
                res.CodeNameWithholdingTaxAccount = (From x In _context.MainAccounts.AsNoTracking Where x.Id = res.WithholdingTaxAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.WithholdingICAAccountId IsNot Nothing Then
                res.CodeNameWithholdingICAAccount = (From x In _context.MainAccounts.AsNoTracking Where x.Id = res.WithholdingICAAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.WithholdingICAConceptId IsNot Nothing Then
                res.CodeNameWithholdingICAConcept = (From x In _context.RetentionConcepts.AsNoTracking Where x.Id = res.WithholdingICAConceptId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
            End If

            'Se recorre los detalle para agregarle las propiedades manuales
            If res?.ProductGroupFunctionalUnit?.Any() Then
                For Each itemDetail As ProductGroupFunctionalUnit In res.ProductGroupFunctionalUnit
                    Dim functionalUnit = (From fu In _context.FunctionalUnit.AsNoTracking Where fu.Id = itemDetail.FunctionalUnitId Select fu).FirstOrDefault
                    itemDetail.FunctionalUnitDescription = functionalUnit.Code + " - " + functionalUnit.Name

                    Dim costAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = itemDetail.CostAccountId Select ma).FirstOrDefault
                    itemDetail.CostAccountDescription = costAccount.Number + " - " + costAccount.Name

                    Dim salesAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = itemDetail.SalesAccountId Select ma).FirstOrDefault
                    itemDetail.SalesAccountDescription = salesAccount.Number + " - " + salesAccount.Name

                    Dim _discountAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = itemDetail.DiscountAccountId Select ma)?.FirstOrDefault
                    itemDetail.DiscountAccountDescription = $"{_discountAccount?.Number} - {_discountAccount?.Name}"
                Next
            End If

            If res.BudgetId IsNot Nothing Then
                Dim budget = (From b In _context.Budget.AsNoTracking().Include("Category").AsNoTracking().Include("Category.FinancialSource").AsNoTracking().Include("RevenueType").AsNoTracking() Where b.Id = res.BudgetId).FirstOrDefault()
                If budget IsNot Nothing Then
                    res.BudgetDescription = String.Concat(budget.Category.Code, " - ", budget.Category.Name, " - ", budget.Category.FinancialSource.Code, " - ", budget.Category.FinancialSource.Name, " - ", budget.RevenueType.Code, " - ", budget.RevenueType.Name)

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

            res.OriginalValue = (From g In _context.ProductGroup.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New ProductGroup()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProductGroupById(id As Integer) As ProductGroup Implements IProductGroupsRepository.GetProductGroupById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.ProductGroup.Include("ProductGroupFunctionalUnit").AsNoTracking
                   Where d.Id = id
                   Select d).SingleOrDefault()

        If res IsNot Nothing Then
            Return res
        Else
            Return New ProductGroup
        End If
    End Function

    Public Function GetProductGroupByProductId(id As Integer) As ProductGroup Implements IProductGroupsRepository.GetProductGroupByProductId
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.ProductGroup
                   Join p In _context.InventoryProduct On p.ProductGroupId Equals d.Id
                   Where p.Id = id Select d).ToList
        If res.Count > 0 Then
            Return res(0)
        Else
            Return New ProductGroup()
        End If
    End Function

    Public Function GetProductGroupByIdForAccounting(id As Integer, Optional IsDeclarant As Boolean = True) As ProductGroup Implements IProductGroupsRepository.GetProductGroupByIdForAccounting
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.ProductGroup.AsNoTracking().Include("ProductGroupFunctionalUnit").AsNoTracking() Where d.Id = id Select d).FirstOrDefault
        If res IsNot Nothing AndAlso res.Id > 0 Then

            'Se declara el objeto que representa al concepto de pagos para la retención
            Dim accountWithholdingSource As AccountPayableConcepts = Nothing

            If IsDeclarant = False Then 'Si el proveedor es no declarante
                accountWithholdingSource = (From aws In _context.AccountPayableConcepts.Include("MainAccounts") Where aws.Id = res.NotDeclarantRetentionAccountPayableConceptId Select aws).FirstOrDefault()
            Else 'Si el proveedor es declarante
                accountWithholdingSource = (From aws In _context.AccountPayableConcepts.Include("MainAccounts") Where aws.Id = res.DeclarantRetentionAccountPayableConceptId Select aws).FirstOrDefault()
            End If

            If accountWithholdingSource.RetentionConceptId IsNot Nothing Then
                res.WithholdingSourcePercernt = (From rc In _context.RetentionConcepts Where accountWithholdingSource.RetentionConceptId = rc.Id Select rc.Rate).FirstOrDefault()
            End If
            res.HandlesCostCenterWithholdigSourceAccount = accountWithholdingSource.MainAccounts.HandlesCostCenter
            res.AccountWithholdingSourceId = accountWithholdingSource.IdAccount
            res.RetentionConceptsWithholdingSourceId = accountWithholdingSource.RetentionConceptId
            res.ConceptAccountPayableWithholdingSourceId = accountWithholdingSource.Id

            Dim accountInventory = (From aws In _context.AccountPayableConcepts.Include("MainAccounts") Where aws.Id = res.InventoryAccountPayableConceptId Select aws).FirstOrDefault()
            res.HandlesThirdPartyAccount = accountInventory.MainAccounts.HandlesThirdParty
            res.AccountInventoryCodeName = accountInventory.MainAccounts.Number + " - " + accountInventory.MainAccounts.Name
            res.AccountInventoryId = accountInventory.IdAccount
            res.AccountInventoryHandlessCostCenter = accountInventory.MainAccounts.HandlesCostCenter
            res.ConceptAccountPayableInventory = accountInventory.Id
            res.CounterpartCostConsignedHandlessCostCenter = _context.MainAccounts.AsNoTracking().Where(Function(x) x.Id = res.CounterpartCostConsignedInventoryId).FirstOrDefault().HandlesCostCenter

            Return res
        Else
            Return New ProductGroup()
        End If
    End Function

End Class
