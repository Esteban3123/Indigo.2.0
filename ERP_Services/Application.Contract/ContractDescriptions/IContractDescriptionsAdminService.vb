'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IContractDescriptionsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveContractDescriptions(ByVal ContractDescriptions As ContractDescriptions, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ContractDescriptions)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteContractDescriptions(ByVal ContractDescriptions As ContractDescriptions, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetContractDescriptions(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ContractDescriptions)

    ''' <summary>
    ''' Obtiene un grupo por id
    ''' </summary>
    ''' <returns></returns>
    Function GetContractDescriptionsById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of ContractDescriptions)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateContractDescriptions(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ContractDescriptions)

End Interface
