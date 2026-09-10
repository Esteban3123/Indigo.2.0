'************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 04-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FixedAssetItemCatalogRepository
    Inherits GenericRepository(Of FixedAssetItemCatalog)
    Implements IFixedAssetItemCatalogRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub


    Public Function GetFixedAssetItemCatalogByCode(code As String, Optional tracking As Boolean = True) As FixedAssetItemCatalog Implements IFixedAssetItemCatalogRepository.GetFixedAssetItemCatalogByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As FixedAssetItemCatalog In Me._context.FixedAssetItemCatalog.Include("FixedAssetItemCatalogDetail").Include("FixedAssetItemCatalogAdquisitionType")
                   Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            ' Consultas a las entidades necesarias para poder asignar los extendidos
            Dim IngressAccountingAccount = (From a In _context.MainAccounts.AsNoTracking Where a.Id = res.IncomeAccountId Select a).FirstOrDefault()

            Dim IngressLeasingAccountingAccount As MainAccounts
            If res.IncomeLeasingAccountId IsNot Nothing Then
                IngressLeasingAccountingAccount = (From b In _context.MainAccounts.AsNoTracking Where b.Id = res.IncomeLeasingAccountId Select b).FirstOrDefault()
            Else
                IngressLeasingAccountingAccount = Nothing
            End If

            Dim DebitLoanAccountingAccount As MainAccounts = Nothing
            If res.DebitLoanAccountId IsNot Nothing Then
                DebitLoanAccountingAccount = (From c In _context.MainAccounts.AsNoTracking Where c.Id = res.DebitLoanAccountId Select c).FirstOrDefault()
            End If

            Dim CreaditLoanAccountingAccount As MainAccounts = Nothing
            If res.CreditLoanAccountId IsNot Nothing Then
                CreaditLoanAccountingAccount = (From d In _context.MainAccounts.AsNoTracking Where d.Id = res.CreditLoanAccountId Select d).FirstOrDefault()
            End If

            Dim DepreciationAccountingAccount As MainAccounts = Nothing
            If res.DepreciationAccountId IsNot Nothing Then
                DepreciationAccountingAccount = (From e In _context.MainAccounts.AsNoTracking Where e.Id = res.DepreciationAccountId Select e).FirstOrDefault()
            End If

            Dim CreditDevaluationAccountingAccount As MainAccounts = Nothing
            If res.CreditDevaluationAccountId IsNot Nothing Then
                CreditDevaluationAccountingAccount = (From e In _context.MainAccounts.AsNoTracking Where e.Id = res.CreditDevaluationAccountId Select e).FirstOrDefault()
            End If

            Dim CreditValorizationAccountingAccount As MainAccounts = Nothing
            If res.CreditValorizationAccountId IsNot Nothing Then
                CreditValorizationAccountingAccount = (From e In _context.MainAccounts.AsNoTracking Where e.Id = res.CreditValorizationAccountId Select e).FirstOrDefault()
            End If

            Dim DebitDevaluationAccountingAccount As MainAccounts = Nothing
            If res.DebitDevaluationAccountId IsNot Nothing Then
                DebitDevaluationAccountingAccount = (From e In _context.MainAccounts.AsNoTracking Where e.Id = res.DebitDevaluationAccountId Select e).FirstOrDefault()
            End If

            Dim DebitValorizationAccountingAccount As MainAccounts = Nothing
            If res.DebitValorizationAccountId IsNot Nothing Then
                DebitValorizationAccountingAccount = (From e In _context.MainAccounts.AsNoTracking Where e.Id = res.DebitValorizationAccountId Select e).FirstOrDefault()
            End If

            Dim CreditEquipmentPlantAcumulatedAccount As MainAccounts = Nothing
            If res.CreditEquipmentPlantAcumulatedAccountId IsNot Nothing Then
                CreditEquipmentPlantAcumulatedAccount = (From e In _context.MainAccounts.AsNoTracking Where e.Id = res.CreditEquipmentPlantAcumulatedAccountId Select e).FirstOrDefault()
            End If

            Dim IncomeAccountPayableConcept As AccountPayableConcepts = Nothing
            If res.IncomeAccountPayableConceptId IsNot Nothing Then
                IncomeAccountPayableConcept = (From e In _context.AccountPayableConcepts Where e.Id = res.IncomeAccountPayableConceptId Select e).FirstOrDefault()
            End If

            Dim DeclarantRetentionAccountPayableConcept As AccountPayableConcepts = Nothing
            If res.DeclarantRetentionAccountPayableConceptId IsNot Nothing Then
                DeclarantRetentionAccountPayableConcept = (From e In _context.AccountPayableConcepts Where e.Id = res.DeclarantRetentionAccountPayableConceptId Select e).FirstOrDefault()
            End If

            Dim NotDeclarantRetentionAccountPayableConcept As AccountPayableConcepts = Nothing
            If res.NotDeclarantRetentionAccountPayableConceptId IsNot Nothing Then
                NotDeclarantRetentionAccountPayableConcept = (From e In _context.AccountPayableConcepts Where e.Id = res.NotDeclarantRetentionAccountPayableConceptId Select e).FirstOrDefault()
            End If


            ' Asignación de extendidos
            Dim NumberNameIngressAccountingAccount As String
            If IngressAccountingAccount IsNot Nothing Then
                NumberNameIngressAccountingAccount = IngressAccountingAccount.Number + " - " + IngressAccountingAccount.Name
            Else
                NumberNameIngressAccountingAccount = String.Empty
            End If

            Dim NumberNameIngressLeasingAccountingAccount As String
            If IngressLeasingAccountingAccount IsNot Nothing Then
                NumberNameIngressLeasingAccountingAccount = IngressLeasingAccountingAccount.Number + " - " + IngressLeasingAccountingAccount.Name
            Else
                NumberNameIngressLeasingAccountingAccount = String.Empty
            End If

            Dim NumberNameDebitLoanAccountingAccount As String
            If DebitLoanAccountingAccount IsNot Nothing Then
                NumberNameDebitLoanAccountingAccount = DebitLoanAccountingAccount.Number + " - " + DebitLoanAccountingAccount.Name
            Else
                NumberNameDebitLoanAccountingAccount = String.Empty
            End If

            Dim NumberNameCreaditLoanAccountingAccount As String
            If CreaditLoanAccountingAccount IsNot Nothing Then
                NumberNameCreaditLoanAccountingAccount = CreaditLoanAccountingAccount.Number + " - " + CreaditLoanAccountingAccount.Name
            Else
                NumberNameCreaditLoanAccountingAccount = String.Empty
            End If

            Dim NumberNameDepreciationAccountingAccount As String
            If DepreciationAccountingAccount IsNot Nothing Then
                NumberNameDepreciationAccountingAccount = DepreciationAccountingAccount.Number + " - " + DepreciationAccountingAccount.Name
            Else
                NumberNameDepreciationAccountingAccount = String.Empty
            End If

            Dim NumberNameCreditDevaluationAccountingAccount As String
            If CreditDevaluationAccountingAccount IsNot Nothing Then
                NumberNameCreditDevaluationAccountingAccount = CreditDevaluationAccountingAccount.Number + " - " + CreditDevaluationAccountingAccount.Name
            Else
                NumberNameCreditDevaluationAccountingAccount = String.Empty
            End If

            Dim NumberNameCreditValorizationAccountingAccount As String
            If CreditValorizationAccountingAccount IsNot Nothing Then
                NumberNameCreditValorizationAccountingAccount = CreditValorizationAccountingAccount.Number + " - " + CreditValorizationAccountingAccount.Name
            Else
                NumberNameCreditValorizationAccountingAccount = String.Empty
            End If

            Dim NumberNameDebitDevaluationAccountingAccount As String
            If DebitDevaluationAccountingAccount IsNot Nothing Then
                NumberNameDebitDevaluationAccountingAccount = DebitDevaluationAccountingAccount.Number + " - " + DebitDevaluationAccountingAccount.Name
            Else
                NumberNameDebitDevaluationAccountingAccount = String.Empty
            End If

            Dim NumberNameDebitValorizationAccountingAccount As String
            If DebitValorizationAccountingAccount IsNot Nothing Then
                NumberNameDebitValorizationAccountingAccount = DebitValorizationAccountingAccount.Number + " - " + DebitValorizationAccountingAccount.Name
            Else
                NumberNameDebitValorizationAccountingAccount = String.Empty
            End If

            Dim NumberNameCreditEquipmentPlantAcumulatedAccount As String
            If CreditEquipmentPlantAcumulatedAccount IsNot Nothing Then
                NumberNameCreditEquipmentPlantAcumulatedAccount = CreditEquipmentPlantAcumulatedAccount.Number + " - " + CreditEquipmentPlantAcumulatedAccount.Name
            Else
                NumberNameCreditEquipmentPlantAcumulatedAccount = String.Empty
            End If

            Dim CodeNameIncomeAccountPayableConcept As String
            If IncomeAccountPayableConcept IsNot Nothing Then
                CodeNameIncomeAccountPayableConcept = IncomeAccountPayableConcept.Code + " - " + IncomeAccountPayableConcept.Name
            Else
                CodeNameIncomeAccountPayableConcept = String.Empty
            End If

            Dim CodeNameDeclarantRetentionAccountPayableConcept As String
            If DeclarantRetentionAccountPayableConcept IsNot Nothing Then
                CodeNameDeclarantRetentionAccountPayableConcept = DeclarantRetentionAccountPayableConcept.Code + " - " + DeclarantRetentionAccountPayableConcept.Name
            Else
                CodeNameDeclarantRetentionAccountPayableConcept = String.Empty
            End If

            Dim CodeNameNotDeclarantRetentionAccountPayableConcept As String
            If NotDeclarantRetentionAccountPayableConcept IsNot Nothing Then
                CodeNameNotDeclarantRetentionAccountPayableConcept = NotDeclarantRetentionAccountPayableConcept.Code + " - " + NotDeclarantRetentionAccountPayableConcept.Name
            Else
                CodeNameNotDeclarantRetentionAccountPayableConcept = String.Empty
            End If


            Dim account As MainAccounts = Nothing

            If res.DepreciationLeasingAccountId IsNot Nothing Then
                account = (From e In _context.MainAccounts.AsNoTracking Where e.Id = res.DepreciationLeasingAccountId Select e).FirstOrDefault()
                If account IsNot Nothing Then
                    res.DepreciationLeasingAccountNumberName = account.Number + " - " + account.Name
                Else
                    res.DepreciationLeasingAccountId = Nothing
                End If
            End If

            If res.FinancialRentingAccountId IsNot Nothing Then
                account = (From e In _context.MainAccounts.AsNoTracking Where e.Id = res.FinancialRentingAccountId Select e).FirstOrDefault()
                If account IsNot Nothing Then
                    res.FinancialRentingAccountDescription = account.Number + " - " + account.Name
                Else
                    res.FinancialRentingAccountId = Nothing
                End If
            End If

            If res.NetIncomeAccountId IsNot Nothing Then
                account = (From e In _context.MainAccounts.AsNoTracking Where e.Id = res.NetIncomeAccountId Select e).FirstOrDefault()
                res.NetIncomeAccountNumberName = account.Number + " - " + account.Name
            End If

            If res.LossMainAccountId IsNot Nothing Then
                account = (From e In _context.MainAccounts.AsNoTracking Where e.Id = res.LossMainAccountId Select e).FirstOrDefault()
                res.LossMainAccountNumberName = account.Number + " - " + account.Name
            End If

            If res.ReplacementCreditMainAccountId IsNot Nothing Then
                account = (From e In _context.MainAccounts.AsNoTracking Where e.Id = res.ReplacementCreditMainAccountId Select e).FirstOrDefault()
                res.ReplacementCreditMainAccountNumberName = account.Number + " - " + account.Name
            End If

            res.NumberNameIngressAccountingAccount = NumberNameIngressAccountingAccount
            res.NumberNameIngressLeasingAccountingAccount = NumberNameIngressLeasingAccountingAccount
            res.NumberNameDebitLoanAccountingAccount = NumberNameDebitLoanAccountingAccount
            res.NumberNameCreaditLoanAccountingAccount = NumberNameCreaditLoanAccountingAccount
            res.NumberNameDepreciationAccountingAccount = NumberNameDepreciationAccountingAccount

            res.NumberNameCreditDevaluationAccountingAccount = NumberNameCreditDevaluationAccountingAccount
            res.NumberNameCreditValorizationAccountingAccount = NumberNameCreditValorizationAccountingAccount
            res.NumberNameDebitDevaluationAccountingAccount = NumberNameDebitDevaluationAccountingAccount
            res.NumberNameDebitValorizationAccountingAccount = NumberNameDebitValorizationAccountingAccount
            res.NumberNameCreditEquipmentPlantAcumulatedAccountingAccount = NumberNameCreditEquipmentPlantAcumulatedAccount

            res.CodeNameIncomeAccountPayableConcept = CodeNameIncomeAccountPayableConcept
            res.CodeNameDeclarantRetentionAccountPayableConcept = CodeNameDeclarantRetentionAccountPayableConcept
            res.CodeNameNotDeclarantRetentionAccountPayableConcept = CodeNameNotDeclarantRetentionAccountPayableConcept

            If res.LoanLeasingAccountId IsNot Nothing Then
                res.LoanLeasingAccountDescription = (From x In _context.MainAccounts.AsNoTracking() Where x.Id = res.LoanLeasingAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.AccumulatedDeteriorationAccountId IsNot Nothing Then
                res.AccumulatedDeteriorationAccountDescription = (From x In _context.MainAccounts.AsNoTracking Where x.Id = res.AccumulatedDeteriorationAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.WarehouseAssetsMainAccountId IsNot Nothing Then
                res.WarehouseAssetsMainAccountNumberName = (From m In _context.MainAccounts.AsNoTracking Where m.Id = res.WarehouseAssetsMainAccountId Select String.Concat(m.Number, " - ", m.Name)).FirstOrDefault
            End If

            If res.MaintenanceAssetsMainAccountId IsNot Nothing Then
                res.MaintenanceAssetsMainAccountNumberName = (From m In _context.MainAccounts.AsNoTracking Where m.Id = res.MaintenanceAssetsMainAccountId Select String.Concat(m.Number, " - ", m.Name)).FirstOrDefault
            End If

            If res.IVAAccountId IsNot Nothing Then
                res.CodeNameIVAAccount = (From x In _context.MainAccounts.AsNoTracking Where x.Id = res.IVAAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.WithholdingTaxAccountId IsNot Nothing Then
                res.CodeNameWithholdingTaxAccount = (From x In _context.MainAccounts.AsNoTracking Where x.Id = res.WithholdingTaxAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.WithholdingTaxConceptId IsNot Nothing Then
                res.CodeNameWithholdingTaxConcept = (From x In _context.RetentionConcepts.AsNoTracking Where x.Id = res.WithholdingTaxConceptId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
            End If

            If res.WithholdingICAAccountId IsNot Nothing Then
                res.CodeNameWithholdingICAAccount = (From x In _context.MainAccounts.AsNoTracking Where x.Id = res.WithholdingICAAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
            End If

            If res.WithholdingICAConceptId IsNot Nothing Then
                res.CodeNameWithholdingICAConcept = (From x In _context.RetentionConcepts.AsNoTracking Where x.Id = res.WithholdingICAConceptId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
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

            If res.FixedAssetItemCatalogDetail.Count() > 0 Then
                For i As Integer = 0 To res.FixedAssetItemCatalogDetail.Count() - 1
                    Dim IdAccountingStructure = res.FixedAssetItemCatalogDetail.Item(i).AccountingStructureId
                    Dim IdCostCenter = res.FixedAssetItemCatalogDetail.Item(i).CostCenterId
                    Dim IdLoanSpendAccountingAccount = res.FixedAssetItemCatalogDetail.Item(i).LoanSpendAccountId
                    Dim IdLoanLeasingSpendAccountingAccount = res.FixedAssetItemCatalogDetail.Item(i).LoanLeasingSpendAccountId
                    Dim ExpenseLoanAccountId = res.FixedAssetItemCatalogDetail(i).ExpenseLoanAccountId
                    Dim LoanFinancialRentingAccountId = res.FixedAssetItemCatalogDetail(i).LoanFinancialRentingAccountId

                    If IdAccountingStructure Is Nothing Then
                        Dim CostCenter = (From e In _context.CostCenter.AsNoTracking Where e.Id = IdCostCenter Select e).FirstOrDefault()
                        res.FixedAssetItemCatalogDetail(i).NumberNameCostCenter = CostCenter.Code + " - " + CostCenter.Name
                    Else
                        Dim AccountingStructure = (From e In _context.AccountingStructure.AsNoTracking Where e.Id = IdAccountingStructure Select e).FirstOrDefault()
                        res.FixedAssetItemCatalogDetail(i).CodeNameAccountingStructure = AccountingStructure.Code + " - " + AccountingStructure.Description
                    End If

                    account = (From e In _context.MainAccounts.AsNoTracking Where e.Id = IdLoanSpendAccountingAccount Select e).FirstOrDefault()
                    If account IsNot Nothing Then
                        res.FixedAssetItemCatalogDetail(i).NumberNameLoanSpendAccountingAccount = account.Number + " - " + account.Name
                    Else
                        res.FixedAssetItemCatalogDetail(i).LoanSpendAccountId = Nothing
                    End If

                    account = (From e In _context.MainAccounts.AsNoTracking Where e.Id = IdLoanLeasingSpendAccountingAccount Select e).FirstOrDefault()
                    If account IsNot Nothing Then
                        res.FixedAssetItemCatalogDetail(i).NumberNameLoanLeasingAccountingAccount = account.Number + " - " + account.Name
                    Else
                        res.FixedAssetItemCatalogDetail(i).LoanLeasingSpendAccountId = Nothing
                    End If

                    account = (From e In _context.MainAccounts.AsNoTracking Where e.Id = ExpenseLoanAccountId Select e).FirstOrDefault()
                    If account IsNot Nothing Then
                        res.FixedAssetItemCatalogDetail(i).ExpenseLoanAccountDescription = account.Number + " - " + account.Name
                    Else
                        res.FixedAssetItemCatalogDetail(i).ExpenseLoanAccountId = Nothing
                    End If

                    account = (From e In _context.MainAccounts.AsNoTracking Where e.Id = LoanFinancialRentingAccountId Select e).FirstOrDefault()
                    If account IsNot Nothing Then
                        res.FixedAssetItemCatalogDetail(i).LoanFinancialRentingAccountDescription = account.Number + " - " + account.Name
                    Else
                        res.FixedAssetItemCatalogDetail(i).LoanFinancialRentingAccountId = Nothing
                    End If
                Next
            End If

            If res.FixedAssetItemCatalogAdquisitionType IsNot Nothing AndAlso res.FixedAssetItemCatalogAdquisitionType.Count > 0 Then
                For Each item In res.FixedAssetItemCatalogAdquisitionType
                    item.LegalBookDescription = (From x In _context.LegalBook.AsNoTracking() Where x.Id = item.LegalBookId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault()
                    item.MainAccountDescription = (From x In _context.MainAccounts.AsNoTracking() Where x.Id = item.MainAccountId Select String.Concat(x.Number, " - ", x.Name)).FirstOrDefault()
                Next
            End If

            res.OriginalValue = (From d As FixedAssetItemCatalog In Me._context.FixedAssetItemCatalog.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetItemCatalog()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un Catálogo de Equipos por id
    ''' </summary>
    ''' <param name="IdFixedAssetCatalog">IdFixedAssetCatalog</param>
    ''' <returns>FixedAssetItemCatalog</returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetItemCatalogById(IdFixedAssetCatalog As Integer) As FixedAssetItemCatalog Implements IFixedAssetItemCatalogRepository.GetFixedAssetItemCatalogById
        If IdFixedAssetCatalog = 0 Then
            Throw New ArgumentNullException("IdFixedAssetCatalog")
        End If
        Dim res = (From d As FixedAssetItemCatalog In Me._context.FixedAssetItemCatalog Where d.Id = IdFixedAssetCatalog Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return Nothing
        End If
    End Function


    ''' <summary>
    ''' Obtiene un Catálogo de Equipos por id
    ''' </summary>
    ''' <param name="IdItem">IdItem</param>
    ''' <returns>FixedAssetItemCatalog</returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetItemCatalogByItemId(IdItem As Integer) As FixedAssetItemCatalog Implements IFixedAssetItemCatalogRepository.GetFixedAssetItemCatalogByItemId
        If IdItem = 0 Then
            Throw New ArgumentNullException("IdItem")
        End If
        Dim res = (From d As FixedAssetItem In Me._context.FixedAssetItem.Include("FixedAssetItemCatalog") Where d.Id = IdItem Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res.FixedAssetItemCatalog
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Función para setear valores de Catálogo de Articulos desde Excel
    ''' </summary>
    ''' <param name="xmlObject"></param>
    ''' <returns></returns>
    Private Function SetFixedAssetItemCatalogDetailFromFile(xmlObject As String) As List(Of SP_SetFixedAssetItemCatalogDetailFromFile_Result) Implements IFixedAssetItemCatalogRepository.SetFixedAssetItemCatalogDetailFromFile
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim res = _context.SP_SetFixedAssetItemCatalogDetailFromFile(xmlObject).ToList()
        Return res
    End Function

End Class
