Imports Domain.Base

Public Interface IInvoiceCopayRepository
    Inherits IRepository(Of InvoiceCopay)

    Function GetInvoiceCopayInfo(BasicBillingInvoiceId As Integer) As InvoiceCopay
End Interface
