#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class FixedAssetItemCatalog

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameIngressAccountingAccount As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameIngressLeasingAccountingAccount As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameDebitLoanAccountingAccount As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameCreaditLoanAccountingAccount As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameDepreciationAccountingAccount As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameDebitValorizationAccountingAccount As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameCreditValorizationAccountingAccount As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameDebitDevaluationAccountingAccount As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property NumberNameCreditDevaluationAccountingAccount As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property CodeNameIncomeAccountPayableConcept As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property CodeNameDeclarantRetentionAccountPayableConcept As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property CodeNameNotDeclarantRetentionAccountPayableConcept As String

    <DataMember()>
    Public Property NetIncomeAccountNumberName As String

    <DataMember()>
    Public Property LossMainAccountNumberName As String

    <DataMember()>
    Public Property ReplacementCreditMainAccountNumberName As String

    <DataMember()>
    Public Property WarehouseAssetsMainAccountNumberName As String

    <DataMember()>
    Public Property MaintenanceAssetsMainAccountNumberName As String

    <DataMember()>
    Public Property DepreciationLeasingAccountNumberName As String

    <DataMember()>
    Public Property NumberNameCreditEquipmentPlantAcumulatedAccountingAccount As String

    <DataMember()>
    Public Property LoanLeasingAccountDescription As String

    <DataMember()>
    Public Property AccumulatedDeteriorationAccountDescription As String

    <DataMember()>
    Public Property FinancialRentingAccountDescription As String

    <DataMember>
    Property CodeNameIVAAccount As String

    <DataMember>
    Property CodeNameWithholdingTaxAccount As String

    <DataMember>
    Property CodeNameWithholdingICAAccount As String

    <DataMember>
    Property CodeNameWithholdingTaxConcept As String

    <DataMember>
    Property CodeNameWithholdingICAConcept As String

#Region "Budget Interface"

    <DataMember()>
    Public Property BudgetaryEntityId As Integer?

    <DataMember()>
    Public Property BudgetaryEntityDescription As String

    <DataMember()>
    Public Property BudgetaryValidityId As Integer?

    <DataMember()>
    Public Property BudgetaryValidityDescription As String

    <DataMember()>
    Public Property BudgetDescription As String

#End Region

End Class
