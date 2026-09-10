Imports System.Runtime.Serialization
<DataContract()>
Public Enum eDocumentTypeReconciliation
    <EnumMember>
    CashReceipt = 1
    <EnumMember>
    VoucherTransaction = 2
    <EnumMember>
    TreasuryNote = 3
    <EnumMember>
    Consignment = 4
End Enum
