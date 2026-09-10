'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceBlockRecordTreasury

    ''' <summary>
    ''' Gets the block record treasury by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBlockRecordTreasuryByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As BlockRecordTreasury

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()>
    Function SaveBlockRecordTreasury(ByVal blockRecordTreasury As BlockRecordTreasury) As ActionResult(Of BlockRecordTreasury)

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>ActionResult</returns>
    <OperationContract()>
    Function DeleteBlockRecordTreasury(ByVal blockRecordTreasury As BlockRecordTreasury) As ActionResult

End Interface
