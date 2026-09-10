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
    Public Function ChangeStateProcedureTemplate(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ProcedureTemplate) Implements IContractServiceProcedureTemplate.ChangeStateProcedureTemplate
        Using service As IProcedureTemplateAdminService = Container.Current.Resolve(Of IProcedureTemplateAdminService)()
            Return service.ChangeStateProcedureTemplate(code, state, audit)
        End Using
        'Return Me._procedureTemplateAdminService.ChangeStateProcedureTemplate(code, state, audit)
    End Function

    ''' <summary>
    ''' Elimina una ProcedureTemplate
    ''' </summary>
    ''' <param name="ProcedureTemplate"></param>
    ''' <returns></returns>
    Public Function DeleteProcedureTemplate(ProcedureTemplate As Domain.Entities.ProcedureTemplate, ListProcedureCups As List(Of ProcedureCups), ListDeleteProcedureCups As List(Of ProcedureCups), company As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractServiceProcedureTemplate.DeleteProcedureTemplate
        Using service As IProcedureTemplateAdminService = Container.Current.Resolve(Of IProcedureTemplateAdminService)()
            Return service.DeleteProcedureTemplate(ProcedureTemplate, ListProcedureCups, ListDeleteProcedureCups, company, audit)
        End Using
        'Return Me._procedureTemplateAdminService.DeleteProcedureTemplate(ProcedureTemplate, ListProcedureCups, ListDeleteProcedureCups, company, audit)
    End Function

    ''' <summary>
    ''' Obtiene una ProcedureTemplate por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetProcedureTemplate(code As String, audit As AuditMessage) As Domain.Entities.ProcedureTemplate Implements IContractServiceProcedureTemplate.GetProcedureTemplate
        Using service As IProcedureTemplateAdminService = Container.Current.Resolve(Of IProcedureTemplateAdminService)()
            Return service.GetProcedureTemplate(code, audit)
        End Using
        'Return Me._procedureTemplateAdminService.GetProcedureTemplate(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene una ProcedureTemplate por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetProcedureTemplateById(id As Integer) As Domain.Entities.ProcedureTemplate Implements IContractServiceProcedureTemplate.GetProcedureTemplateById
        Using service As IProcedureTemplateAdminService = Container.Current.Resolve(Of IProcedureTemplateAdminService)()
            Return service.GetProcedureTemplateById(id)
        End Using
        'Return Me._procedureTemplateAdminService.GetProcedureTemplateById(id)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una ProcedureTemplate
    ''' </summary>
    ''' <param name="ProcedureTemplate"></param>
    ''' <returns></returns>
    Public Function SaveProcedureTemplate(ProcedureTemplate As Domain.Entities.ProcedureTemplate, ListProcedureCups As List(Of ProcedureCups), ListDeleteProcedureCups As List(Of ProcedureCups), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ProcedureTemplate) Implements IContractServiceProcedureTemplate.SaveProcedureTemplate
        Using service As IProcedureTemplateAdminService = Container.Current.Resolve(Of IProcedureTemplateAdminService)()
            Return service.SaveProcedureTemplate(ProcedureTemplate, ListProcedureCups, ListDeleteProcedureCups, audit, idSequense)
        End Using
        'Return Me._procedureTemplateAdminService.SaveProcedureTemplate(ProcedureTemplate, ListProcedureCups, ListDeleteProcedureCups, audit, idSequense)
    End Function

    Public Function CopyAndPasteProcedureTemplate(data As List(Of List(Of String))) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.ProcedureCups), List(Of Tuple(Of String, Integer))) Implements IContractServiceProcedureTemplate.CopyAndPasteProcedureTemplate
        Using service As IProcedureTemplateAdminService = Container.Current.Resolve(Of IProcedureTemplateAdminService)()
            Return service.CopyAndPasteProcedureTemplate(data)
        End Using
        'Return Me._procedureTemplateAdminService.CopyAndPasteProcedureTemplate(data)
    End Function

End Class
