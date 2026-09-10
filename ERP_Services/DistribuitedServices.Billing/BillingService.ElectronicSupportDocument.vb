#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Domain.Entities

#End Region

Partial Class BillingService

    ''' <summary>
    ''' obtiene por Id
    ''' por código de usuario
    ''' </summary>
    ''' <param name="id">Identificador del registro</param>
    ''' <returns></returns>
    Public Function GetElectronicSupportDocumentById(id As Integer) As ActionResult(Of ElectronicSupportDocument) Implements IBillingServiceElectronicSupportDocument.GetElectronicSupportDocumentById
        Using service As IElectronicSupportDocumentAdminService = Container.Current.Resolve(Of IElectronicSupportDocumentAdminService)()
            Return service.GetElectronicSupportDocumentById(id)
        End Using
    End Function

    ''' <summary>
    ''' obtiene  por codigo
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <param name="audit">Auditoria</param>
    ''' <returns></returns>
    Public Function GetElectronicSupportDocumentByCode(code As String, audit As AuditMessage) As ActionResult(Of ElectronicSupportDocument) Implements IBillingServiceElectronicSupportDocument.GetElectronicSupportDocumentByCode
        Using service As IElectronicSupportDocumentAdminService = Container.Current.Resolve(Of IElectronicSupportDocumentAdminService)()
            Return service.GetElectronicSupportDocumentByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' servicio para guardar  y confirmar
    ''' </summary>
    ''' <param name="ElectronicSupportDocument"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    Public Function SaveConfirmElectronicSupportDocument(ElectronicSupportDocument As ElectronicSupportDocument, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult Implements IBillingServiceElectronicSupportDocument.SaveConfirmElectronicSupportDocument
        Using service As IElectronicSupportDocumentAdminService = Container.Current.Resolve(Of IElectronicSupportDocumentAdminService)()
            Return service.SaveConfirmElectronicSupportDocument(ElectronicSupportDocument, audit, idSequence)
        End Using
    End Function

    Public Function UpdateStateElectronicSupportDocuments(Ids As List(Of Integer), audit As AuditMessage) As ActionResult(Of ElectronicSupportDocument) Implements IBillingServiceElectronicSupportDocument.UpdateStateElectronicSupportDocuments
        Using Service As IElectronicSupportDocumentAdminService = Container.Current.Resolve(Of IElectronicSupportDocumentAdminService)()
            Return Service.UpdateStateElectronicSupportDocuments(Ids, audit)
        End Using
    End Function

End Class
