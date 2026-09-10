Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Public Class AccountReceivableDocumentDetail
    Inherits Entity(Of Domain.Entities.AccountReceivableDocumentDetail)

    <DataMember>
    Property DescriptionAccountReceivableConcept As String

    <DataMember>
    Property DescriptionThirdParty As String


    <DataMember>
    Property DescriptionAccount As String

    <DataMember>
    Property DescriptionCostCenter As String

End Class
