'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IDefinitionRateAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveDefinitionRate(ByVal DefinitionRate As DefinitionRate, ListDeleteDefinitionRateDetail As List(Of DefinitionRateDetail), listDeleteDefinitionRateDetailCondition As List(Of DefinitionRateDetailCondition), Company As String, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of DefinitionRate)

    ''' <summary>
    ''' Elimina
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteDefinitionRate(ByVal DefinitionRate As DefinitionRate, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una definicion de tarifa por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetDefinitionRate(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of DefinitionRate)

    ''' <summary>
    ''' Obtiene una definicion de tarifa por id
    ''' </summary>
    ''' <returns></returns>
    Function GetDefinitionRateById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of DefinitionRate)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateDefinitionRate(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of DefinitionRate)

End Interface
