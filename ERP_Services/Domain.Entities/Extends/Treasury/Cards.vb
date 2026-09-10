Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class Cards
    Inherits Entity(Of Domain.Entities.Cards)

    <DataMember>
    Property CodeNameThirdParty As String

    <DataMember>
    Property CodeNameCashReceiptConceptCommision As String

    <DataMember>
    Property CodeNameRetentionConceptCommision As String

    <DataMember>
    Property CodeNameCashReceiptConceptRTF As String

    <DataMember>
    Property CodeNamedCashReceiptConceptICA As String

    <DataMember>
    Property CodeNameRetentionConceptICA As String

    <DataMember>
    Property CodeNameRetentionConceptRTF As String

    <DataMember>
    Property CodeNameCostCenterCommision As String

    <DataMember>
    Property CodeNameCostCenterICA As String

    <DataMember>
    Property CodeNameCostCenterRTF As String

    <DataMember>
    Property HandlesCostCenterCommision As Boolean
    <DataMember>
    Property HandlesCostCenterRFT As Boolean
    <DataMember>
    Property HandlesCostCenterICA As Boolean

End Class
