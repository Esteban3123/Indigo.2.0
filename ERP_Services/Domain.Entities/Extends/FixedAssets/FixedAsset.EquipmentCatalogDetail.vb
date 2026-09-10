#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class FixedAssetItemCatalogDetail

    ''' <summary>
    ''' Obtiene o establece el código y nombre de la Ubicación concatenado
    ''' </summary>
    <DataMember()>
    Public Property CodeNameAccountingStructure As String

    <DataMember()>
    Public Property NumberNameLoanSpendAccountingAccount As String

    <DataMember()>
    Public Property NumberNameLoanLeasingAccountingAccount As String

    <DataMember()>
    Public Property ExpenseLoanAccountDescription As String

    <DataMember()>
    Public Property LoanFinancialRentingAccountDescription As String

    <DataMember()>
    Public Property NumberNameCostCenter As String

End Class
