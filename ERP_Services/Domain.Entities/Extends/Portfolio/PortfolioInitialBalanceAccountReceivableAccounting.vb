Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class PortfolioInitialBalanceAccountReceivableAccounting
    Inherits Entity(Of Domain.Entities.PortfolioInitialBalanceAccountReceivableAccounting)

    <DataMember>
    Property CodeNameCostCenter As String

    <DataMember>
    Property CodeNameMainAccount As String
End Class
