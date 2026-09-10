'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 02-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IContractGroupAdminService
    Inherits IDisposable


    ''' <summary>
    ''' Lista todos los Grupos de contratos
    ''' </summary>
    ''' <returns>Lista los grupos de contratos</returns>
    Function ListAllContractGroup() As List(Of ContractGroup)

    ''' <summary>
    ''' Elimina un grupo de contrato
    ''' </summary>
    ''' <param name="contractGroup">Grupo de contrato</param>
    ''' <returns></returns>
    Function DeleteContractGroup(ByVal contractGroup As ContractGroup, ByVal audit As AuditMessage) As ActionMessageResult(Of ContractGroup)

    ''' <summary>
    ''' Guarda o edita un grupo de contrato
    ''' </summary>
    ''' <param name="contractGroup">Grupo de contrato</param>
    ''' <returns></returns>
    Function SaveContractGroup(ByVal contractGroup As ContractGroup, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene un grupo de contrato
    ''' </summary>
    ''' <param name="code">Código de grupo de contrato</param>
    ''' <returns> Grupo de contrato</returns>
    Function GetContractGroup(ByVal code As String) As ContractGroup


End Interface
