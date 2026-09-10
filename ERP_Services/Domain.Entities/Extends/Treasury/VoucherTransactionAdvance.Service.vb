Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Public Class VoucherTransactionAdvance
    Inherits Entity(Of VoucherTransactionAdvance)

#Region "Properties"
    <DataMember()>
    Property PortfolioAdvanceCode As String
    <DataMember()>
    Property PortfolioAdvanceDocumentDate As Date
    <DataMember()>
    Property PortfolioAdvanceBalance As Decimal
#End Region

End Class
