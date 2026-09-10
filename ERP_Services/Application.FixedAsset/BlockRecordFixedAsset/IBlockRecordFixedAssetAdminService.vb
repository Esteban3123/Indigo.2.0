'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Sergio Fernandez
' Created          : 08/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBlockRecordFixedAssetAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Gets the block record payments by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Function GetBlockRecordFixedAssetByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String) As Domain.Entities.BlockRecordFixedAsset

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>ActionResult</returns>
    Function SaveBlockRecordFixedAsset(ByVal blockRecordFixedAsset As Domain.Entities.BlockRecordFixedAsset, ByVal audit As AuditMessage) As ActionResult(Of Domain.Entities.BlockRecordFixedAsset)
    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>ActionResult</returns>
    Function DeleteBlockRecordFixedAsset(ByVal blockRecordFixedAsset As Domain.Entities.BlockRecordFixedAsset, ByVal audit As AuditMessage) As ActionResult

End Interface
