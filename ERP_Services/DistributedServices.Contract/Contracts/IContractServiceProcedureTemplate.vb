'***********************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface IContractServiceProcedureTemplate
    ''' <summary>
    ''' Guarda o Actualiza una ProcedureTemplate
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveProcedureTemplate(ProcedureTemplate As Domain.Entities.ProcedureTemplate, ListProcedureCups As List(Of ProcedureCups), ListDeleteProcedureCups As List(Of ProcedureCups), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ProcedureTemplate)
    ''' <summary>
    ''' Elimina una ProcedureTemplate
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteProcedureTemplate(ProcedureTemplate As Domain.Entities.ProcedureTemplate, ListProcedureCups As List(Of ProcedureCups), ListDeleteProcedureCups As List(Of ProcedureCups), company As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult
    ''' <summary>
    ''' Obtiene una ProcedureTemplate por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetProcedureTemplate(code As String, audit As AuditMessage) As Domain.Entities.ProcedureTemplate
    ''' <summary>
    ''' Obtiene una ProcedureTemplate por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetProcedureTemplateById(ByVal id As Integer) As ProcedureTemplate
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateProcedureTemplate(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ProcedureTemplate)

    ''' <summary>
    ''' metodo para pegar en la rejilla del form de plantilla de procedimiento
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function CopyAndPasteProcedureTemplate(data As List(Of List(Of String))) As ActionResult(Of List(Of ProcedureCups), List(Of Tuple(Of String, Integer)))

End Interface
