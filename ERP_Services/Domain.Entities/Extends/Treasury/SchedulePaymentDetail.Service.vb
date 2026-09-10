Imports System.Runtime.Serialization

Partial Public Class SchedulePaymentDetail

    <DataMember()> _
    Property InvoiceBalance As Decimal?
    <DataMember()> _
    Property BalanceShare As Decimal?
    <DataMember()> _
    Property Invoice As String
    <DataMember()> _
    Property Share As Integer
    <DataMember()> _
    Property SupplierCode As String
    <DataMember()> _
    Property SupplierName As String
    <DataMember()>
    Property AccountPayableCode As String

    ''' <summary>
    ''' Guarda la posicion de los detalles para luego organizarlos de forma correcta
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property PositionDetailsAdd As Integer


End Class
