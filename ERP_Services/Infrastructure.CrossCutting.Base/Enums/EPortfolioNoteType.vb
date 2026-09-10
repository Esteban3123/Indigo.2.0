Imports System.Runtime.Serialization

''' <summary>
''' La nota aplica:   1 - Factura Total (Se cargan las facturas que tengan saldos en una o mas de una cuenta contable asociada y que solo tenga una cuota)  
''' 2 - Factura Cuota (Se cargan las facturas que tengan saldo solo en una cuenta contable pero que tenga mas de una cuota)   
''' 3 - Anticipo
''' 4 - Distribucion de Anticipo (Cuando sea de este tipo la naturaleza siempre va hacer de tipo credito)
''' 5 - Reversión Anticipo vs CxC  
''' 6 - Factura Detallada
''' </summary>
<DataContract>
Public Enum EPortfolioNoteType As Integer
    <EnumMember>
    TotalInvoice = 1
    <EnumMember>
    InvoiceShare
    <EnumMember>
    Advance
    <EnumMember>
    AdvanceDistribution
    <EnumMember>
    ReversalAdvanceVsCxC
    <EnumMember>
    Detail
End Enum