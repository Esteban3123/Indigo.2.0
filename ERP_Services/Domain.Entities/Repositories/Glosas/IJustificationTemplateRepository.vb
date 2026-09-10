Imports Domain.Entities
Imports Domain.Base
Public Interface IJustificationTemplateRepository

    Inherits IRepository(Of JustificationTemplate)

    ''' <summary>
    ''' Lista todas las plantillas de justificación.
    ''' </summary>
    ''' <returns></returns>
    Function ListAllJustificationTemplate() As List(Of JustificationTemplate)
    ''' <summary>
    ''' consulta una plantilla de justificación especifica
    ''' </summary>
    ''' <param name="code">El codigo de la plantilla</param>
    ''' <returns></returns>
    Function getJustificationTemplate(ByVal code As String, Optional tracking As Boolean = True) As JustificationTemplate
    ''' <summary>
    ''' consulta una plantilla de justificación por código
    ''' </summary>
    ''' <param name="code">El codigo de la plantilla</param>
    ''' <returns>Lista JustificationTemplate</returns>
    Function listJustificationTemplateByCode(code As String) As List(Of JustificationTemplate)
    ''' <summary>
    ''' consulta una plantilla de justificación por concepto
    ''' </summary>
    ''' <param name="Concept">El codigo de la plantilla</param>
    ''' <returns>Lista JustificationTemplate</returns>
    Function listJustificationTemplateByConcept(Concept As String) As List(Of JustificationTemplate)


End Interface

