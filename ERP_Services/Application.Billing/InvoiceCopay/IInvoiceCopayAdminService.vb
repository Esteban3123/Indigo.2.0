Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IInvoiceCopayAdminService
    Inherits IDisposable

    Function Remove(invoiceCopay As InvoiceCopay) As ActionResult
    Function Create(invoiceCopay As InvoiceCopay, audit As AuditMessage) As ActionResult(Of InvoiceCopay)
End Interface
