'***********************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractBlockRecordContract

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetBlockRecordContractByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordContract

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function SaveBlockRecordContract(ByVal BlockRecordContract As BlockRecordContract) As ActionResult(Of BlockRecordContract)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function DeleteBlockRecordContract(ByVal BlockRecordContract As BlockRecordContract) As ActionResult

End Interface
