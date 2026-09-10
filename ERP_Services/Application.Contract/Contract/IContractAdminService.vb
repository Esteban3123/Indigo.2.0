'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IContractAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un contrato
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveContract(ByVal Contract As Domain.Entities.Contract, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Domain.Entities.Contract)

    ''' <summary>
    ''' Elimina un contrato
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteContract(ByVal Contract As Domain.Entities.Contract, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un contrato por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetContract(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of Domain.Entities.Contract)

    ''' <summary>
    ''' Obtiene un contrato por id
    ''' </summary>
    ''' <returns></returns>
    Function GetContractById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of Domain.Entities.Contract)

    ''' <summary>
    ''' Gets the contract by identifier with aggregates.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetContractByIdWithAggregates(id As Integer, ByVal audit As AuditMessage) As ActionResult(Of Domain.Entities.Contract)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateContract(ByVal code As String, ByVal state As Integer, ByVal audit As AuditMessage) As ActionResult(Of Domain.Entities.Contract)

End Interface
