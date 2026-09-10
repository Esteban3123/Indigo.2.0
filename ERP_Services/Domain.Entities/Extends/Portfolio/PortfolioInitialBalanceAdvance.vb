Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class PortfolioInitialBalanceAdvance
    Inherits Entity(Of Domain.Entities.PortfolioInitialBalanceAdvance)

    <DataMember>
    Property CodeNameCustomer As String

    <DataMember>
    Property CodeNameMainAccount As String

    <DataMember>
    Property CodeNameCostCenter As String

    <DataMember>
    Property NitNameThirdParty As String
End Class
