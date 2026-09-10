Imports System.Runtime.Serialization

<Serializable()> _
Partial Public Class ProductATC

    'NoPOS
    <DataMember()> _
    Property ServiceOrderDetailId As Integer
    <DataMember()> _
    Property ProductIdNoPOS As Integer
    <DataMember()> _
    Property ProductCodeNoPOS As String
    <DataMember()> _
    Property ATCCodeNoPOS As String
    <DataMember()> _
    Property Quantity As Integer
    <DataMember()> _
    Property UnitValueProductNoPOS As Decimal


    'POS
    <DataMember()> _
    Property ATCCodePOS As String
    <DataMember()> _
    Property DefaultProductCodePOS As String
    <DataMember()> _
    Property DefaultProductNamePOS As String
    <DataMember()> _
    Property DefaultProductIdPOS As Integer
    <DataMember()> _
    Property DefaultProductUnitValue As Decimal?
    <DataMember()> _
    Property DefaultProductTotalValue As Decimal?

    'DIFERENCIA
    <DataMember()> _
    Property UnitValueDifference As Decimal
    <DataMember()> _
    Property TotalValueDifference As Decimal

End Class
