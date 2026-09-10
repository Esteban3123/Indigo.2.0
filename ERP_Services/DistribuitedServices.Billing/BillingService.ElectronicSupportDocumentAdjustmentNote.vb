Imports Application.Billing
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class BillingService
    Implements IBillingServiceElectronicSupportDocumentAdjustmentNote

    Public Function GetElectronicSupportDocumentAdjustmentNoteByCode(code As String, audit As AuditMessage) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote) Implements IBillingServiceElectronicSupportDocumentAdjustmentNote.GetElectronicSupportDocumentAdjustmentNoteByCode
        Using service As IElectronicSupportDocumentAdjustmentNoteAdminService = Container.Current.Resolve(Of IElectronicSupportDocumentAdjustmentNoteAdminService)()
            Return service.GetElectronicSupportDocumentAdjustmentNoteByCode(code, audit)
        End Using
    End Function

    Public Function GetElectronicSupportDocumentAdjustmentNoteById(id As Integer) As ElectronicSupportDocumentAdjustmentNote Implements IBillingServiceElectronicSupportDocumentAdjustmentNote.GetElectronicSupportDocumentAdjustmentNoteById
        Using service As IElectronicSupportDocumentAdjustmentNoteAdminService = Container.Current.Resolve(Of IElectronicSupportDocumentAdjustmentNoteAdminService)()
            Return service.GetElectronicSupportDocumentAdjustmentNoteById(id)
        End Using
    End Function

    Public Function SaveElectronicSupportDocumentAdjusmentNote(electronicDocumentNote As ElectronicSupportDocumentAdjustmentNote, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote) Implements IBillingServiceElectronicSupportDocumentAdjustmentNote.SaveElectronicSupportDocumentAdjusmentNote
        Using service As IElectronicSupportDocumentAdjustmentNoteAdminService = Container.Current.Resolve(Of IElectronicSupportDocumentAdjustmentNoteAdminService)()
            Return service.SaveElectronicSupportDocumentAdjusmentNote(electronicDocumentNote, audit, idSequence)
        End Using
    End Function

    Public Function ConfirmElectronicSupportDocumentAdjusmentNote(electronicDocumentNote As ElectronicSupportDocumentAdjustmentNote, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote) Implements IBillingServiceElectronicSupportDocumentAdjustmentNote.ConfirmElectronicSupportDocumentAdjusmentNote
        Using service As IElectronicSupportDocumentAdjustmentNoteAdminService = Container.Current.Resolve(Of IElectronicSupportDocumentAdjustmentNoteAdminService)()
            Return service.ConfirmElectronicSupportDocumentAdjusmentNote(electronicDocumentNote, audit, idSequence)
        End Using
    End Function

    Public Function AnulateElectronicSupportDocumentAdjusmentNote(electronicDocumentNote As ElectronicSupportDocumentAdjustmentNote, audit As AuditMessage) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote) Implements IBillingServiceElectronicSupportDocumentAdjustmentNote.AnulateElectronicSupportDocumentAdjusmentNote
        Using service As IElectronicSupportDocumentAdjustmentNoteAdminService = Container.Current.Resolve(Of IElectronicSupportDocumentAdjustmentNoteAdminService)()
            Return service.AnulateElectronicSupportDocumentAdjusmentNote(electronicDocumentNote, audit)
        End Using
    End Function

    Public Function UpdateStateElectronicSupportDocumentsAdjusmentNote(Ids As List(Of Integer), audit As AuditMessage) As ActionResult(Of ElectronicSupportDocumentAdjustmentNote) Implements IBillingServiceElectronicSupportDocumentAdjustmentNote.UpdateStateElectronicSupportDocumentsAdjusmentNote
        Using service As IElectronicSupportDocumentAdjustmentNoteAdminService = Container.Current.Resolve(Of IElectronicSupportDocumentAdjustmentNoteAdminService)()
            Return service.UpdateStateElectronicSupportDocumentsAdjusmentNote(Ids, audit)
        End Using
    End Function
End Class
