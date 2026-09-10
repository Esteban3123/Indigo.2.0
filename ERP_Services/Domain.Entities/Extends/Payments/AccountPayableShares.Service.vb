Imports System.Runtime.Serialization

Partial Public Class AccountPayableShares

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el numero de lafactura
    ''' </summary>
    ''' <value>Numero de la factura</value>
    ''' <returns>El numero de la factura</returns>
    <DataMember()>
    Public Property InvoiceBillNumber As String

    ''' <summary>
    ''' Obtiene o establece la fecha de la factura
    ''' </summary>
    <DataMember()>
    Public Property InvoiceBillDate As DateTime

    ''' <summary>
    ''' Obtiene o establece el valor de la nota de la cuota
    ''' </summary>
    <DataMember()>
    Public Property ValueNoteShare As Decimal

#End Region

End Class
