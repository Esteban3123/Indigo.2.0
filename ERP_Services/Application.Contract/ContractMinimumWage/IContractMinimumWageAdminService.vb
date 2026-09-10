'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 16/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IContractMinimumWageAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Guarda o Actualiza un salario
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveContractMinimumWage(ByVal ContractMinimumWage As ContractMinimumWage, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ContractMinimumWage)

    ''' <summary>
    ''' Elimina un salario 
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteContractMinimumWage(ByVal ContractMinimumWage As ContractMinimumWage, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un salario por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetContractMinimumWage(ByVal code As String, ByVal audit As AuditMessage) As ContractMinimumWage

    ''' <summary>
    ''' Obtiene un salario por id
    ''' </summary>
    ''' <returns></returns>
    Function GetContractMinimumWageById(ByVal id As Integer) As ContractMinimumWage
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateContractMinimumWage(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ContractMinimumWage)
End Interface
