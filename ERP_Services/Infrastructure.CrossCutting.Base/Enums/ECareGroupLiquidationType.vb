Imports System.Runtime.Serialization
''' <summary>
''' Tipo de liquidacion 
''' 1 - Pago por Servicios 
''' 2 - Capitacion
''' 3 - Factura Global 
''' 4 - Capitacion Global 
''' 5 - Pago Global Prospectivo - PGP
''' </summary>
''' <remarks></remarks>
<DataContract()>
Public Enum ECareGroupLiquidationType As Byte
    <EnumMember()>
    PaymentForServices = 1
    <EnumMember()>
    Capitation
    <EnumMember()>
    GlobalInvoice
    <EnumMember()>
    GlobalCapitation
    <EnumMember()>
    PGP
End Enum