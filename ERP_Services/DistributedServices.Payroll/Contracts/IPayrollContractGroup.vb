'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 02-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollContractGroup

    ''' <summary>
    ''' Lista todos los Grupos de contratos
    ''' </summary>
    ''' <returns>Lista los grupos de contratos</returns>
    <OperationContract()> _
    Function ListAllContractGroup(session As SessionValues) As List(Of ContractGroup)

    ''' <summary>
    ''' Elimina un grupo de contrato
    ''' </summary>
    ''' <param name="contractGroup">Grupo de contrato</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteContractGroup(ByVal contractGroup As ContractGroup, session As SessionValues) As ActionMessageResult(Of ContractGroup)

    ''' <summary>
    ''' Guarda o edita un grupo de contrato
    ''' </summary>
    ''' <param name="contractGroup">Grupo de contrato</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveContractGroup(ByVal contractGroup As ContractGroup, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene un grupo de contrato
    ''' </summary>
    ''' <param name="code">Código de grupo de contrato</param>
    ''' <returns> Grupo de contrato</returns>
    <OperationContract()> _
    Function GetContractGroup(ByVal code As String, session As SessionValues) As ContractGroup

End Interface
