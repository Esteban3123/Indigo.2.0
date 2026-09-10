'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
<ServiceContract()> _
Public Interface IPayrollContractType

    ''' <summary>
    ''' Lista Todos los Tipos de Contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllContractType(session As SessionValues) As List(Of ContractType)

    ''' <summary>
    ''' Elimina un tipo de Contrato
    ''' </summary>
    ''' <param name="contractType">Tipo de Contrato</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteContractType(ByVal contractType As ContractType, session As SessionValues) As Boolean

    ''' <summary>
    ''' Almacena un Tipo de Contrato
    ''' </summary>
    ''' <param name="contractType">Tipo de Contrato</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveContractType(ByVal contractType As ContractType, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene un Tipo de Contrato
    ''' </summary>
    ''' <param name="code">Código del Tipo de Contrato</param>
    ''' <returns>Tipo de Contrato</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetContractType(ByVal code As String, session As SessionValues) As ContractType

    ''' <summary>
    ''' Obtiene un Tipo de Contrato por ID
    ''' </summary>
    ''' <param name="ID">ID del Tipo de Contrato</param>
    ''' <returns>Tipo de Contrato</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetContractTypeById(ByVal ID As String, session As SessionValues) As ContractType

End Interface
