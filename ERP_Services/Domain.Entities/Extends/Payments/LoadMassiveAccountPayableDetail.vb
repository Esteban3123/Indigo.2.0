Imports System.Runtime.Serialization

Partial Public Class LoadMassiveAccountPayableDetail

#Region "Properties"

    <DataMember()>
    Public Property HeaderId As Integer

    <DataMember()>
    Public Property AccountPayableConceptCode As String

    <DataMember()>
    Public Property MainAccountNumber As String

    <DataMember()>
    Public Property ThirdPartyNit As String

    <DataMember()>
    Public Property CostCenterCode As String

    <DataMember()>
    Public Property RetentionConceptCode As String

    <DataMember()>
    Public Property Percentage As Decimal

#End Region

End Class
