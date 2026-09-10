Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

Partial Class MaintanceService

    Public Function DeleteInventoryType(Empresa As String, inventoryType As Domain.Maintenance.Entities.InventoryType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IInventoryTypeService.DeleteInventoryType
        Using InventoryTypeAdmin As IInventoryTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IInventoryTypeAdminService)()
            Return InventoryTypeAdmin.DeleteInventoryType(inventoryType, audit)
        End Using
    End Function

    Public Function GetInventoryType(Empresa As String, codeInventoryType As String) As Domain.Maintenance.Entities.InventoryType Implements IInventoryTypeService.GetInventoryType
        Using InventoryTypeAdmin As IInventoryTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IInventoryTypeAdminService)()
            Return InventoryTypeAdmin.GetInventoryType(codeInventoryType)
        End Using
    End Function

    Public Function ListAllInventoryType(Empresa As String) As List(Of Domain.Maintenance.Entities.InventoryType) Implements IInventoryTypeService.ListAllInventoryType
        Using InventoryTypeAdmin As IInventoryTypeAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IInventoryTypeAdminService)()
            Return InventoryTypeAdmin.ListAllInventoryType()
        End Using
    End Function

    Public Function SaveInventoryType(InventoryType As Domain.Maintenance.Entities.InventoryType, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of Domain.Maintenance.Entities.InventoryType) Implements IInventoryTypeService.SaveInventoryType
        Using InventoryTypeAdmin As IInventoryTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInventoryTypeAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return InventoryTypeAdmin.SaveInventoryType(InventoryType, audit, idSequense)
        End Using
    End Function

    Public Function Change_StateInventoryType(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Maintenance.Entities.InventoryType) Implements IInventoryTypeService.Change_StateInventoryType
        Using InventoryTypeAdmin As IInventoryTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInventoryTypeAdminService)()
            Return InventoryTypeAdmin.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function
End Class
