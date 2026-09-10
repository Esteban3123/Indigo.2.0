Imports System.Runtime.Serialization

Partial Public Class SettingPayments

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion del comprobante de cxp
    ''' </summary>
    <DataMember()>
    Public Property VoucherCxpDescription As String

    ''' <summary>
    '''  Obtiene o establece la descripcion del comprobante de traslados
    ''' </summary>
    <DataMember()>
    Public Property VoucherTransferDescription As String

    ''' <summary>
    '''  Obtiene o establece la descripcion del comprobante de Notas Credito
    ''' </summary>
    <DataMember()>
    Public Property VoucherCreditNotesDescription As String

    ''' <summary>
    '''  Obtiene o establece la descripcion del comprobante de Notas Debito
    ''' </summary>
    <DataMember()>
    Public Property VoucherDebitNotesDescription As String

    ''' <summary>
    '''  Obtiene o establece la descripcion del comprobante de amortizacion
    ''' </summary>
    <DataMember()>
    Public Property VoucherAmortizationDescription As String

#End Region

End Class
