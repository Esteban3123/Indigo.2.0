Imports System.Runtime.Serialization

Partial Public Class PaymentTransfer

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el numero y el nombre de la cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property NumberNameAccount As String

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre del centro de costo
    ''' </summary>
    <DataMember()>
    Public Property CodeNameCostCenter As String

    ''' <summary>
    ''' Obtiene o establece el codigo del anticipo
    ''' </summary>
    <DataMember()>
    Public Property CodeAdvancePayments As String

    ''' <summary>
    ''' Obtiene o establece el codigo de la cxp
    ''' </summary>
    <DataMember()>
    Public Property CodeAccountPayable As String

    ''' <summary>
    ''' Obtiene o establece el codigo y el nombre del proveedor
    ''' </summary>
    <DataMember()>
    Public Property CodeNameSupplier As String

    ''' <summary>
    ''' Obtiene o establece el saldo del anticipo
    ''' </summary>
    <DataMember()>
    Public Property BalanceAdvancePayments As Decimal

#End Region

End Class
