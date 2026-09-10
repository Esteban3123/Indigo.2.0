Imports System.Runtime.Serialization

''' <summary>
''' 1. Factura EAPB con Contrato 
''' 2. Factura EAPB Sin Contrato  
''' 3. Factura Particular   
''' 4. Factura Capitada    
''' 5. Control de Capitacion 
''' 6. Factura Basica  
''' 7. Factura de Venta de Productos      
''' 91. Nota Credito Validacion Previa
''' 92. Nota Debito Validacion Previa
''' 93. Nota Credito Validacion Previa - Facturas version anterior
''' 94. Nota Debito Validacion Previa - Facturas version anterior
''' 98. Nota Debito
''' 99. Nota Credito
''' </summary>
<DataContract>
Public Enum EElectronicDocumentType As Byte
    <EnumMember>
    EAPBInvoiceWithContract = 1
    <EnumMember>
    EAPBInvoiceWithoutContract
    <EnumMember>
    ParticularInvoice
    <EnumMember>
    CapitatedInvoice
    <EnumMember>
    CapitationControl
    <EnumMember>
    BasicBilling
    <EnumMember>
    ProductSalesInvoice
    <EnumMember>
    CreditNotePV = 91
    <EnumMember>
    DebitNotePV
    <EnumMember>
    CreditNotePVIPV
    <EnumMember>
    DebitNotePVIPV
    <EnumMember>
    DebitNote = 98
    <EnumMember>
    CreditNote = 99
End Enum