'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 01-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region
Public Interface IJustificationTemplateAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todas las plantillas de justificación.
    ''' </summary>
    ''' <returns></returns>
    Function ListAllJustificationTemplate(audit As AuditMessage) As List(Of JustificationTemplate)
    ''' <summary>
    ''' consulta una plantilla de justificación especifica
    ''' </summary>
    ''' <param name="code">El codigo de la plantilla</param>
    ''' <returns></returns>
    Function getJustificationTemplate(ByVal code As String, audit As AuditMessage) As JustificationTemplate
    ''' <summary>
    ''' Elimina una plantilla de justificación
    ''' </summary>
    ''' <param name="JustificationTemplate">Objeto Justification Template</param>
    ''' <param name="audit">Objeto de auditoria</param>
    ''' <returns></returns>
    Function DeleteJustificationTemplate(ByVal JustificationTemplate As JustificationTemplate, ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' Guarda una plantilla de justificacion
    ''' </summary>
    ''' <param name="JustificationTemplate">Objeto Justification Template</param>
    ''' <param name="audit">Objeto de auditoria</param>
    ''' <returns></returns>
    Function SaveJustificationTemplate(ByVal JustificationTemplate As JustificationTemplate, ByVal audit As AuditMessage) As ActionResult(Of JustificationTemplate)
    ''' <summary>
    ''' consulta una plantilla de justificación por código
    ''' </summary>
    ''' <param name="code">El codigo de la plantilla</param>
    ''' <returns>Lista JustificationTemplate</returns>
    Function listJustificationTemplateByCode(code As String, audit As AuditMessage) As List(Of JustificationTemplate)
    ''' <summary>
    ''' consulta una plantilla de justificación por concepto
    ''' </summary>
    ''' <param name="Concept">El codigo de la plantilla</param>
    ''' <returns>Lista JustificationTemplate</returns>
    Function listJustificationTemplateByConcept(Concept As String, audit As AuditMessage) As List(Of JustificationTemplate)


End Interface
