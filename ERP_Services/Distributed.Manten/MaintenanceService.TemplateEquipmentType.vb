Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class MaintanceService

    Public Function DeleteTemplateEquipmentType(Empresa As String, TemplateEquipmentType As Domain.Entities.TemplateEquipmentType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements ITemplateEquipmentTypeService.DeleteTemplateEquipmentType
        Using TemplateEquipmentTypeAdmin As ITemplateEquipmentTypeAdminService = Container.Current.Resolve(Of ITemplateEquipmentTypeAdminService)()
            Return TemplateEquipmentTypeAdmin.DeleteTemplateEquipmentType(TemplateEquipmentType, audit)
        End Using
    End Function

    Public Function GetTemplateEquipmentType(Empresa As String, codeTemplateEquipmentType As String, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Entities.TemplateEquipmentType Implements ITemplateEquipmentTypeService.GetTemplateEquipmentType
        Using TemplateEquipmentTypeAdmin As ITemplateEquipmentTypeAdminService = Container.Current.Resolve(Of ITemplateEquipmentTypeAdminService)()
            Return TemplateEquipmentTypeAdmin.GetTemplateEquipmentType(codeTemplateEquipmentType, audit)
        End Using
    End Function

    Public Function ListAllTemplateEquipmentType(Empresa As String) As List(Of Domain.Entities.TemplateEquipmentType) Implements ITemplateEquipmentTypeService.ListAllTemplateEquipmentType
        Using TemplateEquipmentTypeAdmin As ITemplateEquipmentTypeAdminService = Container.Current.Resolve(Of ITemplateEquipmentTypeAdminService)()
            Return TemplateEquipmentTypeAdmin.ListAllTemplateEquipmentType
        End Using
    End Function

    Public Function SaveTemplateEquipmentType(Empresa As String, TemplateEquipmentType As Domain.Entities.TemplateEquipmentType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of Domain.Entities.TemplateEquipmentType) Implements ITemplateEquipmentTypeService.SaveTemplateEquipmentType
        Using TemplateEquipmentTypeAdmin As ITemplateEquipmentTypeAdminService = Container.Current.Resolve(Of ITemplateEquipmentTypeAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Return TemplateEquipmentTypeAdmin.SaveTemplateEquipmentType(TemplateEquipmentType, audit, idSequense)
        End Using
    End Function

    Public Function ChangeStateTemplate(Empresa As String, code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.TemplateEquipmentType) Implements ITemplateEquipmentTypeService.ChangeStateTemplate
        Using TemplateEquipmentTypeAdmin As ITemplateEquipmentTypeAdminService = Container.Current.Resolve(Of ITemplateEquipmentTypeAdminService)()
            Return TemplateEquipmentTypeAdmin.ChangeStateTemplate(code, state, audit)
        End Using
    End Function

    Public Function GetTemplateEquipmentTypeEquipmentTypeId(Empresa As String, IdEquipmentType As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Entities.TemplateEquipmentType Implements ITemplateEquipmentTypeService.GetTemplateEquipmentTypeEquipmentTypeId
        Using TemplateEquipmentTypeAdmin As ITemplateEquipmentTypeAdminService = Container.Current.Resolve(Of ITemplateEquipmentTypeAdminService)()
            Return TemplateEquipmentTypeAdmin.GetTemplateEquipmentTypeByEquipmentTypeId(IdEquipmentType)
        End Using
    End Function
End Class
