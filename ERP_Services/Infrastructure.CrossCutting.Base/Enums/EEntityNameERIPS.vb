Imports System.ComponentModel
Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para establecer el entityName
''' </summary>
<DataContract()>
Public Enum EEntityNameERIPS
    ''' <summary>
    ''' RIPS de factura
    ''' </summary>
    <EnumMember>
    Invoice = 1
    ''' <summary>
    ''' RIPS de Notas
    ''' </summary>
    <EnumMember>
    BillingNote = 2
    ''' <summary>
    ''' RIPS nota de ajuste
    ''' </summary>
    <EnumMember>
    BillingNoteAdjustment = 3
    ''' <summary>
    ''' RIPS re envio fact
    ''' </summary>
    <EnumMember>
    ResendInvoice = 4
    ''' <summary>
    ''' RIPS Reenvio de nota
    ''' </summary>
    <EnumMember>
    ResendBillingNote = 5
    ''' <summary>
    ''' RIPS Reenvio de nota ajuste
    ''' </summary>
    <EnumMember>
    ResendBillingNoteAdjustment = 6
    ''' <summary>
    ''' entityName desconocido
    ''' </summary>
    <EnumMember>
    Unknown = 7
    ''' <summary>
    ''' Envio RIPS factura Monto Fijo
    ''' </summary>
    <EnumMember>
    InvoiceFixedAmount = 8
    ''' <summary>
    ''' Re-envio RIPS factura Monto Fijo
    ''' </summary>
    <EnumMember>
    ResendInvoiceFixedAmount = 9

    ''' <summary>
    ''' Envio EntityName Capita FINAL
    ''' </summary>
    <EnumMember>
    InvoiceEntityCapitated

    ''' <summary>
    ''' RE-Envio EntityName Capita FINAL
    ''' </summary>
    <EnumMember>
    ResendInvoiceEntityCapitated

End Enum
