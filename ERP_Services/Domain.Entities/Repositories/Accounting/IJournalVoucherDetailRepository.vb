#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IJournalVoucherDetailRepository
    Inherits IRepository(Of JournalVoucherDetails)

#Region "Methods"

    Function GetJournalVoucherDetails(journalVoucherId As Integer) As List(Of JournalVoucherDetails)

#End Region

End Interface
