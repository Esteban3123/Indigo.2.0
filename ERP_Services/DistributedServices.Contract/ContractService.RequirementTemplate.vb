'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Contract
Imports Microsoft.Practices.Unity

Partial Class ContractService
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Function ChangeStateRequirementTemplate(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RequirementTemplate) Implements IContractServiceRequirementTemplate.ChangeStateRequirementTemplate
        Using service As IRequirementTemplateAdminService = Container.Current.Resolve(Of IRequirementTemplateAdminService)()
            Return service.ChangeStateRequirementTemplate(code, state, audit)
        End Using
        'Return Me._requirementTemplateAdminService.ChangeStateRequirementTemplate(code, state, audit)
    End Function

    ''' <summary>
    ''' Elimina una RequirementTemplate
    ''' </summary>
    ''' <param name="RequirementTemplate"></param>
    ''' <returns></returns>
    Public Function DeleteRequirementTemplate(RequirementTemplate As Domain.Entities.RequirementTemplate, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractServiceRequirementTemplate.DeleteRequirementTemplate
        Using service As IRequirementTemplateAdminService = Container.Current.Resolve(Of IRequirementTemplateAdminService)()
            Return service.DeleteRequirementTemplate(RequirementTemplate, audit)
        End Using
        'Return Me._requirementTemplateAdminService.DeleteRequirementTemplate(RequirementTemplate, audit)
    End Function

    ''' <summary>
    ''' Obtiene una RequirementTemplate por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetRequirementTemplate(code As String, audit As AuditMessage) As Domain.Entities.RequirementTemplate Implements IContractServiceRequirementTemplate.GetRequirementTemplate
        Using service As IRequirementTemplateAdminService = Container.Current.Resolve(Of IRequirementTemplateAdminService)()
            Return service.GetRequirementTemplate(code, audit)
        End Using
        'Return Me._requirementTemplateAdminService.GetRequirementTemplate(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene una RequirementTemplate por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetRequirementTemplateById(id As Integer) As Domain.Entities.RequirementTemplate Implements IContractServiceRequirementTemplate.GetRequirementTemplateById
        Using service As IRequirementTemplateAdminService = Container.Current.Resolve(Of IRequirementTemplateAdminService)()
            Return service.GetRequirementTemplateById(id)
        End Using
        'Return Me._requirementTemplateAdminService.GetRequirementTemplateById(id)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una RequirementTemplate
    ''' </summary>
    ''' <param name="RequirementTemplate"></param>
    ''' <returns></returns>
    Public Function SaveRequirementTemplate(RequirementTemplate As Domain.Entities.RequirementTemplate, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RequirementTemplate) Implements IContractServiceRequirementTemplate.SaveRequirementTemplate
        Using service As IRequirementTemplateAdminService = Container.Current.Resolve(Of IRequirementTemplateAdminService)()
            Return service.SaveRequirementTemplate(RequirementTemplate, audit, idSequense)
        End Using
        'Return Me._requirementTemplateAdminService.SaveRequirementTemplate(RequirementTemplate, audit, idSequense)
    End Function
End Class
