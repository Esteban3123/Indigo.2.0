Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract> _
Public Interface IGlosasJustificationTemplate

    ''' <summary>
    ''' Lista todas las plantillas de justificación.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract>
    Function ListAllJustificationTemplate(ByVal session As SessionValues) As List(Of JustificationTemplate)
    ''' <summary>
    ''' consulta una plantilla de justificación especifica
    ''' </summary>
    ''' <param name="code">El codigo de la plantilla</param>
    ''' <returns></returns>
    <OperationContract>
    Function getJustificationTemplate(ByVal code As String, ByVal session As SessionValues) As JustificationTemplate
    ''' <summary>
    ''' Elimina una plantilla de justificación
    ''' </summary>
    ''' <param name="JustificationTemplate">Objeto Justification Template</param>
    ''' <param name="session">Objeto de session</param>
    ''' <returns></returns>
    <OperationContract>
    Function DeleteJustificationTemplate(ByVal JustificationTemplate As JustificationTemplate, ByVal session As SessionValues) As ActionResult
    ''' <summary>
    ''' Guarda una plantilla de justificacion
    ''' </summary>
    ''' <param name="JustificationTemplate">Objeto Justification Template</param>
    ''' <param name="session">Objeto de session</param>
    ''' <returns></returns>
    <OperationContract>
    Function SaveJustificationTemplate(ByVal JustificationTemplate As JustificationTemplate, ByVal session As SessionValues) As ActionResult(Of JustificationTemplate)
    ''' <summary>
    ''' consulta una plantilla de justificación por código
    ''' </summary>
    ''' <param name="code">El codigo de la plantilla</param>
    ''' <returns>Lista JustificationTemplate</returns>
    <OperationContract>
    Function listJustificationTemplateByCode(code As String, ByVal session As SessionValues) As List(Of JustificationTemplate)
    ''' <summary>
    ''' Lista Plantillas de Justificación por concepto
    ''' </summary>
    ''' <param name="concept">Concepto</param>
    ''' <param name="session">Objeto Sesión</param>
    ''' <returns>Lista de Plantillas de Justificación</returns>
    <OperationContract>
    Function listJustificationTemplateByConcept(concept As String, session As SessionValues) As List(Of JustificationTemplate)

End Interface
