#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IJournalVoucherDetailAdminService
    Inherits IDisposable

#Region "Methods"

    Function GetJournalVoucherDetails(journalVoucherId As Integer) As ActionResult(Of List(Of JournalVoucherDetails))

#End Region

End Interface
