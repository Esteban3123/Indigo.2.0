'***********************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractServiceRequirementTemplate
    ''' <summary>
    ''' Guarda o Actualiza una RequirementTemplate
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveRequirementTemplate(RequirementTemplate As Domain.Entities.RequirementTemplate, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RequirementTemplate)
    ''' <summary>
    ''' Elimina una RequirementTemplate
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteRequirementTemplate(RequirementTemplate As Domain.Entities.RequirementTemplate, audit As AuditMessage) As Domain.Base.Entities.ActionResult
    ''' <summary>
    ''' Obtiene una RequirementTemplate por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRequirementTemplate(code As String, audit As AuditMessage) As Domain.Entities.RequirementTemplate
    ''' <summary>
    ''' Obtiene una RequirementTemplate por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetRequirementTemplateById(ByVal id As Integer) As RequirementTemplate
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateRequirementTemplate(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.RequirementTemplate)
End Interface
