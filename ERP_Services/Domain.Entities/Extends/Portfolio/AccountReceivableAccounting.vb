Imports System.Runtime.Serialization

Partial Public Class AccountReceivableAccounting

    ''' <summary>
    ''' Obtiene o establece el numero de la factura
    ''' </summary>
    <DataMember()>
    Public Property InvoiceNumber As String

    ''' <summary>
    ''' Obtiene o establece el valor a cruzar (Comprobante de egreso - cruce)
    ''' </summary>
    <DataMember()>
    Public Property CrossingValue As Decimal

    ''' <summary>
    ''' Obtiene o establece el nombre de la cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property MainAccountDescription As String


    <DataMember()>
    Property AccountReceivableShareIdTmp As Integer
End Class
