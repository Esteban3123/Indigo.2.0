Imports System.Runtime.Serialization

Partial Public Class InventoryClosedMonth

    ''' <summary>
    ''' Lista de documentos pendientes de confirmar
    ''' </summary>
    <DataMember()>
    Public Property Documents As List(Of SP_VerifiyHasConfirmAllDocuments_Result)

    ''' <summary>
    ''' Conciliacion inventario vs contabilidad
    ''' </summary>
    <DataMember()>
    Public Property Balance As List(Of SP_ConciliationInventoryVsAccounting_Result)

    ''' <summary>
    ''' Lista de variaciones de costos de productos
    ''' </summary>
    <DataMember()>
    Public Property Variations As List(Of SP_ClosedMonthVariationCost_Result)

    <DataMember()>
    Public Property Quantity As Integer

    <DataMember()>
    Public Property ProductCost As Decimal

    <DataMember()>
    Public Property FinalProductCost As Decimal

    <DataMember()>
    Public Property SellingPrice As Integer

    <DataMember()>
    Public Property ProductId As Integer

    <DataMember()>
    Public Property CloseMonthId As Integer

    <DataMember()>
    Public Property WareHouseId As Integer


End Class
