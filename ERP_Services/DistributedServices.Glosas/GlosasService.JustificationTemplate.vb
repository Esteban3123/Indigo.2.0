'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Juan Diego Diaz M.
' Created          : 01-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region
Partial Class GlosasService

#Region "JustificationTemplate"

    ''' <summary>
    ''' Borrar plantilla justificación
    ''' </summary>
    ''' <param name="JustificationTemplate"></param>
    ''' <param name="session"></param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteJustificationTemplate(JustificationTemplate As JustificationTemplate, session As SessionValues) As ActionResult Implements IGlosasJustificationTemplate.DeleteJustificationTemplate
        Using JustificatioTemplateAdmin As IJustificationTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IJustificationTemplateAdminService)()
            Return JustificatioTemplateAdmin.DeleteJustificationTemplate(JustificationTemplate, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtener plantilla de justificación
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="session"></param>
    ''' <returns>Objeto Plantilla Justificación</returns>
    Public Function getJustificationTemplate(code As String, session As SessionValues) As JustificationTemplate Implements IGlosasJustificationTemplate.getJustificationTemplate
        Using JustificatioTemplateAdmin As IJustificationTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IJustificationTemplateAdminService)()
            Return JustificatioTemplateAdmin.getJustificationTemplate(code, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Listar todas las plantillas de justificación
    ''' </summary>
    ''' <param name="session"></param>
    ''' <returns>Lista de plantillas de justificación</returns>
    Public Function ListAllJustificationTemplate(session As SessionValues) As List(Of JustificationTemplate) Implements IGlosasJustificationTemplate.ListAllJustificationTemplate
        Using JustificatioTemplateAdmin As IJustificationTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IJustificationTemplateAdminService)()
            Return JustificatioTemplateAdmin.ListAllJustificationTemplate(session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Guardar Plantillas de Justificación
    ''' </summary>
    ''' <param name="JustificationTemplate"></param>
    ''' <param name="session"></param>
    ''' <returns>ActionResult</returns>
    Public Function SaveJustificationTemplate(JustificationTemplate As JustificationTemplate, session As SessionValues) As ActionResult(Of JustificationTemplate) Implements IGlosasJustificationTemplate.SaveJustificationTemplate
        Using JustificatioTemplateAdmin As IJustificationTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IJustificationTemplateAdminService)()
            Return JustificatioTemplateAdmin.SaveJustificationTemplate(JustificationTemplate, session.AuditMessageWcf)
        End Using
    End Function

    Public Function listJustificationTemplateByCode(code As String, session As SessionValues) As List(Of JustificationTemplate) Implements IGlosasJustificationTemplate.listJustificationTemplateByCode
        Using JustificatioTemplateAdmin As IJustificationTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IJustificationTemplateAdminService)()
            Return JustificatioTemplateAdmin.listJustificationTemplateByCode(code, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Lista Plantillas de Justificación por concepto
    ''' </summary>
    ''' <param name="concept">Concepto</param>
    ''' <param name="session">Objeto Sesión</param>
    ''' <returns>Lista de Plantillas de Justificación</returns>
    Public Function listJustificationTemplateByConcept(concept As String, session As SessionValues) As List(Of JustificationTemplate) Implements IGlosasJustificationTemplate.listJustificationTemplateByConcept
        Using JustificatioTemplateAdmin As IJustificationTemplateAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IJustificationTemplateAdminService)()
            Return JustificatioTemplateAdmin.listJustificationTemplateByConcept(concept, session.AuditMessageWcf)
        End Using
    End Function

#End Region

End Class

