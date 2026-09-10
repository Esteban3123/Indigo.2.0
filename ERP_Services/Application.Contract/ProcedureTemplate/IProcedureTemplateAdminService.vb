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

Public Interface IProcedureTemplateAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Guarda o Actualiza una ProcedureTemplate
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveProcedureTemplate(ByVal ProcedureTemplate As ProcedureTemplate, ListProcedureCups As List(Of ProcedureCups), ListDeleteProcedureCups As List(Of ProcedureCups), ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ProcedureTemplate)

    ''' <summary>
    ''' Elimina una ProcedureTemplate
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteProcedureTemplate(ByVal ProcedureTemplate As ProcedureTemplate, ListProcedureCups As List(Of ProcedureCups), ListDeleteProcedureCups As List(Of ProcedureCups), company As String, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una ProcedureTemplate por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetProcedureTemplate(ByVal code As String, ByVal audit As AuditMessage) As ProcedureTemplate

    ''' <summary>
    ''' Obtiene una ProcedureTemplate por id
    ''' </summary>
    ''' <returns></returns>
    Function GetProcedureTemplateById(ByVal id As Integer) As ProcedureTemplate
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateProcedureTemplate(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ProcedureTemplate)

    ''' <summary>
    ''' metodo para pegar en la rejilla de plantilla de procedimientos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CopyAndPasteProcedureTemplate(data As List(Of List(Of String))) As ActionResult(Of List(Of ProcedureCups), List(Of Tuple(Of String, Integer)))

End Interface
