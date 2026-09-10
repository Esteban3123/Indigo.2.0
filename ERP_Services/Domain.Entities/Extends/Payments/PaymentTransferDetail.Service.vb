Imports System.Runtime.Serialization

Partial Public Class PaymentTransferDetail

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el numero de la factura
    ''' </summary>
    <DataMember()>
    Public Property NumberBill As String

    ''' <summary>
    ''' Obtiene o establece el numero y el nombre de la cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property NumberNameMainAccount As String

    ''' <summary>
    ''' Obtiene o establece el saldo de la factura
    ''' </summary>
    <DataMember()>
    Public Property BalanceBill As Decimal

    ''' <summary>
    ''' Obtiene o establece el numero de la cuota
    ''' </summary>
    <DataMember()>
    Public Property Share As Integer

    ''' <summary>
    ''' Obtiene o establece el saldo de la cuota
    ''' </summary>
    <DataMember()>
    Public Property BalanceShare As Decimal

    ''' <summary>
    ''' Obtiene o establece la descripcion del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property SupplierDescription As String

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property ThirdPartyId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property SupplierId As Integer

    ''' <summary>
    ''' abreviacion de la moneda de la factura
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CurrencyAbbreviationInvoice As String

#End Region

End Class
