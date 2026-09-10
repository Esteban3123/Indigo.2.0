'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 26-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollContractModificationReason

    ''' <summary>
    ''' Elimina una razon de otro si
    ''' </summary>
    ''' <param name="contractModificationReason">Razon de otro si</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteContractModificationReason(ByVal contractModificationReason As ContractModificationReason, session As SessionValues) As ActionMessageResult(Of ContractModificationReason)

    ''' <summary>
    ''' Almacena o Actualiza una razon de otro si
    ''' </summary>
    ''' <param name="contractModificationReason">Razon de otro si</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveContractModificationReason(ByVal contractModificationReason As ContractModificationReason, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene una razon de otro si por codigo
    ''' </summary>
    ''' <returns>razones de otro si</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetContractModificationReason(ByVal code As String, session As SessionValues) As ContractModificationReason

End Interface
