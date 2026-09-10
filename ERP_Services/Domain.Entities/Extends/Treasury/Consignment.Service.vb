Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Public Class Consignment
    Inherits Entity(Of Domain.Entities.Consignment)

#Region "Properties"
    <DataMember>
    Property FullNameEntityBankAccount As String

    <DataMember>
    Property FullNameCostCenter As String

    <DataMember>
    Property CurrencyId As Integer?

    <DataMember>
    Property CurrencyAbbreviation As String
#End Region

End Class
