Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class ConsignmentDetail
    Inherits Entity(Of Domain.Entities.ConsignmentDetail)

#Region "Properties"
    <DataMember>
    Property FullNameCashRegister As String
    <DataMember>
    Property FullNameCostCenter As String
    <DataMember>
    Property FullNameMainAccount As String
    ''' <summary>
    ''' Saldo de la caja relacionada a la consignacion
    ''' </summary>
    <DataMember>
    Property CurrentBalance As Decimal

    <DataMember>
    Property CashCurrencyId As Integer?

    <DataMember>
    Property CashCurrencyAbbreviation As String
#End Region

End Class
