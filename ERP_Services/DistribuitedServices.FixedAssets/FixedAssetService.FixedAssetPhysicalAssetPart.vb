Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.FixedAsset
Imports Microsoft.Practices.Unity

Partial Public Class FixedAssetService

    Public Function GetFixedAssetPhysicalAssetPartById(Id As Integer) As Domain.Entities.FixedAssetPhysicalAssetParts Implements IFixedAssetPhysicalAssetPartService.GetFixedAssetPhysicalAssetPartById
        Using service As IFixedAssetPhysicalAssetPartAdminService = Container.Current.Resolve(Of IFixedAssetPhysicalAssetPartAdminService)()
            Return service.GetFixedAssetPhysicalAssetPartById(Id)
        End Using
    End Function

End Class
