#Region "Imports"

Imports Application.Billing
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class BillingService

    ''' <summary>
    ''' Actualiza el estado de un documento electrónico
    ''' </summary>
    ''' <returns></returns>
    Public Function UpdateStateElectronicDocument(id As Integer, status As Byte, audit As AuditMessage) As ActionResult(Of ElectronicDocument) Implements IBillingElectronicDocument.UpdateStateElectronicDocument
        Using service As IElectronicDocumentsAdminService = Container.Current.Resolve(Of IElectronicDocumentsAdminService)()
            Return service.UpdateStateElectronicDocument(id, status, audit)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza el estado de uno o más documentos electrónicos
    ''' </summary>
    ''' <returns></returns>
    Public Function UpdateStateElectronicDocuments(listElectronicDocuments As List(Of ElectronicDocument), audit As AuditMessage) As ActionResult(Of ElectronicDocument) Implements IBillingElectronicDocument.UpdateStateElectronicDocuments
        Using service As IElectronicDocumentsAdminService = Container.Current.Resolve(Of IElectronicDocumentsAdminService)()
            Return service.UpdateStateElectronicDocuments(listElectronicDocuments, audit)
        End Using
    End Function

    Public Function ReSendElectronicDocument(InvoiceNumber As String, InvoiceId As Integer, session As SessionValues) As ActionResult Implements IBillingElectronicDocument.ReSendElectronicDocument
        Using service As IElectronicDocumentsAdminService = Container.Current.Resolve(Of IElectronicDocumentsAdminService)()
            Return service.ReSendElectronicDocument(InvoiceNumber, InvoiceId, session)
        End Using
    End Function

    Public Function ReSendElectronicDocuments(listElectronicDocuments As List(Of ElectronicDocument), session As SessionValues) As ActionResult Implements IBillingElectronicDocument.ReSendElectronicDocuments
        Using service As IElectronicDocumentsAdminService = Container.Current.Resolve(Of IElectronicDocumentsAdminService)()
            Return service.ReSendElectronicDocuments(listElectronicDocuments, session)
        End Using
    End Function

    ''' <summary>
    ''' Lista todas las facturas electronicas
    ''' por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeInvoices(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument) Implements IBillingElectronicDocument.ListElectronicDocumentsTypeInvoices
        Using service As IElectronicDocumentsAdminService = Container.Current.Resolve(Of IElectronicDocumentsAdminService)()
            Return service.ListElectronicDocumentsTypeInvoices(OperatingUnitId, StatusId)
        End Using
    End Function

    ''' <summary>
    ''' Lista todas las notas debitos
    ''' por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeDebitNotes(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument) Implements IBillingElectronicDocument.ListElectronicDocumentsTypeDebitNotes
        Using service As IElectronicDocumentsAdminService = Container.Current.Resolve(Of IElectronicDocumentsAdminService)()
            Return service.ListElectronicDocumentsTypeDebitNotes(OperatingUnitId, StatusId)
        End Using
    End Function

    ''' <summary>
    ''' Lista todas las notas creditos
    ''' por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function ListElectronicDocumentsTypeCreditNotes(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument) Implements IBillingElectronicDocument.ListElectronicDocumentsTypeCreditNotes
        Using service As IElectronicDocumentsAdminService = Container.Current.Resolve(Of IElectronicDocumentsAdminService)()
            Return service.ListElectronicDocumentsTypeCreditNotes(OperatingUnitId, StatusId)
        End Using
    End Function

End Class
