Imports System.Runtime.Serialization

''' <summary>
''' 
''' </summary>
<DataContract(IsReference:=True), Serializable(), KnownType(GetType(RevenueControlDetailCrossing))> _
<KnownType(GetType(PortfolioAdvance))> _
Partial Public Class RevenueControlDetailCrossing

    <DataMember()> _
    Public Property FolioOrder As String
    <DataMember()> _
    Public Property FolioType As Byte
    <DataMember()> _
    Public Property CareGroupId As Integer
    <DataMember()> _
    Public Property RevenueControlDetailId As Integer
    <DataMember()> _
    Public Property ListPortfolioAdvance As List(Of PortfolioAdvance)
    <DataMember()> _
    Public Property TotalPatientDiscount As Decimal
    <DataMember()> _
    Public Property RevenueControlId As Integer
    <DataMember>
    Public Property OutputDate As DateTime
    <DataMember>
    Public Property IsCutAccount As Boolean
    <DataMember>
    Public Property OutputDiagnosis As String
    <DataMember()>
    Public Property InitialDate As DateTime
    <DataMember()>
    Public Property CurrencyId As Integer?
    <DataMember()>
    Public Property TRMValue As Decimal?

    <DataMember()>
    Public Property CutType As Integer

    <DataMember()>
    Public Property TaxDevolutionValue As Decimal

    <DataMember()>
    Public Property ConditionSalesId As Integer?

    <DataMember()>
    Public Property EconomicActivityId As Integer?
End Class
