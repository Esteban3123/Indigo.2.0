'***********************************************************************
' Assembly         : DistributedServices.FixedAsset
' Author           : Sergio Fernandez
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IFixedAssetBlockRecordFixedAsset

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetBlockRecordFixedAssetByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As Domain.Entities.BlockRecordFixedAsset

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function SaveBlockRecordFixedAsset(ByVal blockRecordFixedAsset As Domain.Entities.BlockRecordFixedAsset, ByVal session As SessionValues) As ActionResult(Of Domain.Entities.BlockRecordFixedAsset)
    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>ActionResult</returns>
    <OperationContract()> _
    Function DeleteBlockRecordFixedAsset(ByVal blockRecordFixedAsset As Domain.Entities.BlockRecordFixedAsset, ByVal session As SessionValues) As ActionResult

End Interface
