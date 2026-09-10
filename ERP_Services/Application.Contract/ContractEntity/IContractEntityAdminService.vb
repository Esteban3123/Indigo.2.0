'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IContractEntityAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza una entidad de contrato
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveContractEntity(ByVal ContractEntity As ContractEntity, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ContractEntity)

    ''' <summary>
    ''' Elimina una entidad de contrato
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteContractEntity(ByVal ContractEntity As ContractEntity, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene una entidad de contrato por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetContractEntity(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ContractEntity)

    ''' <summary>
    ''' Obtiene una entidad de contrato por id
    ''' </summary>
    ''' <returns></returns>
    Function GetContractEntityById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of ContractEntity)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateContractEntity(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ContractEntity)

End Interface
