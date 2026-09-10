'***********************************************************************
' Assembly         : DistributedServices.FixedAsset
' Author           : Sergio Fernandez
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.FixedAsset
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Public Class FixedAssetService

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function DeleteBlockRecordFixedAsset(blockRecordFixedAsset As Domain.Entities.BlockRecordFixedAsset, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IFixedAssetBlockRecordFixedAsset.DeleteBlockRecordFixedAsset
        Using service As IBlockRecordFixedAssetAdminService = Container.Current.Resolve(Of IBlockRecordFixedAssetAdminService)()
            Return service.DeleteBlockRecordFixedAsset(blockRecordFixedAsset, Nothing)
        End Using
        'Return Me._blockRecordFixedAssetAdminService.DeleteBlockRecordFixedAsset(blockRecordFixedAsset, Nothing)
    End Function

    ''' <summary>
    ''' Gets the block record treasury by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Function GetBlockRecordFixedAssetByIdformAndIdRecord(IdForm As String, IdRecord As String) As Domain.Entities.BlockRecordFixedAsset Implements IFixedAssetBlockRecordFixedAsset.GetBlockRecordFixedAssetByIdformAndIdRecord
        Using service As IBlockRecordFixedAssetAdminService = Container.Current.Resolve(Of IBlockRecordFixedAssetAdminService)()
            Return service.GetBlockRecordFixedAssetByIdformAndIdRecord(IdForm, IdRecord)
        End Using
        'Return Me._blockRecordFixedAssetAdminService.GetBlockRecordFixedAssetByIdformAndIdRecord(IdForm, IdRecord)
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function SaveBlockRecordFixedAsset(blockRecordFixedAsset As Domain.Entities.BlockRecordFixedAsset, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BlockRecordFixedAsset) Implements IFixedAssetBlockRecordFixedAsset.SaveBlockRecordFixedAsset
        Using service As IBlockRecordFixedAssetAdminService = Container.Current.Resolve(Of IBlockRecordFixedAssetAdminService)()
            Return service.SaveBlockRecordFixedAsset(blockRecordFixedAsset, Nothing)
        End Using
        'Return Me._blockRecordFixedAssetAdminService.SaveBlockRecordFixedAsset(blockRecordFixedAsset, Nothing)
    End Function

End Class
