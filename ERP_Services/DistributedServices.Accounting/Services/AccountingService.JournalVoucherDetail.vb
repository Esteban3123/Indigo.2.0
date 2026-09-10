#Region "Imports"

Imports Microsoft.Practices.Unity
Imports Application.Accounting
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

Partial Public Class AccountingService

#Region "Methods"

    ''' <summary>
    ''' Obtiene los Detalles de un comprobantea contable
    ''' </summary>
    ''' <param name="journalVoucherId"></param>
    ''' <returns></returns>
    Public Function GetJournalVoucherDetails(journalVoucherId As Integer) As ActionResult(Of List(Of JournalVoucherDetails)) Implements IAccountingJournalVoucherDetail.GetJournalVoucherDetails
        Using service As IJournalVoucherDetailAdminService = Container.Current.Resolve(Of IJournalVoucherDetailAdminService)()
            Return service.GetJournalVoucherDetails(journalVoucherId)
        End Using
    End Function

#End Region

End Class
