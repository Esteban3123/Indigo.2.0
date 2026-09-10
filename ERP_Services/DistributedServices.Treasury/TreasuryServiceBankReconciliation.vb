Imports Application.Treasury
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class TreasuryService
    Implements ITreasuryServiceBankReconciliation

    Public Function GetBankReconciliationById(id As Integer, audit As AuditMessage) As ActionResult(Of BankReconciliation) Implements ITreasuryServiceBankReconciliation.GetBankReconciliationById
        Using service As IBankReconciliationAdminService = Container.Current.Resolve(Of IBankReconciliationAdminService)()
            Return service.GetBankReconciliationById(id, audit)
        End Using
    End Function

    Public Function GetBankReconciliationByCode(code As String, audit As AuditMessage) As ActionResult(Of BankReconciliation) Implements ITreasuryServiceBankReconciliation.GetBankReconciliationByCode
        Using service As IBankReconciliationAdminService = Container.Current.Resolve(Of IBankReconciliationAdminService)()
            Return service.GetBankReconciliationByCode(code, audit)
        End Using
    End Function

    Public Function SaveBankReconciliation(BankReconciliation As BankReconciliation, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of BankReconciliation) Implements ITreasuryServiceBankReconciliation.SaveBankReconciliation
        Using service As IBankReconciliationAdminService = Container.Current.Resolve(Of IBankReconciliationAdminService)()
            Return service.SaveBankReconciliation(BankReconciliation, audit, idSequense)
        End Using
    End Function

    Public Function GetBankReconciliationDetails(criterias As Dictionary(Of String, String)) As ActionResult(Of List(Of BankReconciliationDetail)) Implements ITreasuryServiceBankReconciliation.GetBankReconciliationDetails
        Using service As IBankReconciliationAdminService = Container.Current.Resolve(Of IBankReconciliationAdminService)()
            Return service.GetBankReconciliationDetails(criterias)
        End Using
    End Function

End Class
