'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IRequirementTemplateAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Guarda o Actualiza una RequirementTemplate
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveRequirementTemplate(ByVal RequirementTemplate As RequirementTemplate, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of RequirementTemplate)

    ''' <summary>
    ''' Elimina una RequirementTemplate
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteRequirementTemplate(ByVal RequirementTemplate As RequirementTemplate, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una RequirementTemplate por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetRequirementTemplate(ByVal code As String, ByVal audit As AuditMessage) As RequirementTemplate

    ''' <summary>
    ''' Obtiene una RequirementTemplate por id
    ''' </summary>
    ''' <returns></returns>
    Function GetRequirementTemplateById(ByVal id As Integer) As RequirementTemplate
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateRequirementTemplate(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of RequirementTemplate)
End Interface
