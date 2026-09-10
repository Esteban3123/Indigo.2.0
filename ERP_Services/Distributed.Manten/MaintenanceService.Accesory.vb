Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class MaintanceService

    Public Function DeleteAccessory(Empresa As String, Accessory As Domain.Entities.Accessory, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IAccesoryService.DeleteAccessory
        Using AccesoryAdmin As IAccesoryAdminService = Container.Current.Resolve(Of IAccesoryAdminService)()
            Return AccesoryAdmin.DeleteAccessory(Accessory, audit)
        End Using
    End Function

    Public Function GetAccessory(Empresa As String, codeAccessory As String) As Domain.Entities.Accessory Implements IAccesoryService.GetAccessory
        Using AccesoryAdmin As IAccesoryAdminService = Container.Current.Resolve(Of IAccesoryAdminService)()
            Return AccesoryAdmin.GetAccessory(codeAccessory)
        End Using
    End Function

    Public Function ListAllAccessory(Empresa As String) As List(Of Domain.Entities.Accessory) Implements IAccesoryService.ListAllAccessory
        Using AccesoryAdmin As IAccesoryAdminService = Container.Current.Resolve(Of IAccesoryAdminService)()
            Return AccesoryAdmin.ListAllAccessory
        End Using
    End Function

    Public Function SaveAccessory(Accessory As Domain.Entities.Accessory, session As SessionValues) As ActionResult(Of Domain.Entities.Accessory) Implements IAccesoryService.SaveAccessory
        Using AccesoryAdmin As IAccesoryAdminService = Container.Current.Resolve(Of IAccesoryAdminService)()
            Dim idSequense As Long = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Long)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return AccesoryAdmin.SaveAccessory(Accessory, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function Change_StateAccessory(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Entities.Accessory) Implements IAccesoryService.Change_StateAccessory
        Using AccesoryAdmin As IAccesoryAdminService = Container.Current.Resolve(Of IAccesoryAdminService)()
            Return AccesoryAdmin.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function
End Class
