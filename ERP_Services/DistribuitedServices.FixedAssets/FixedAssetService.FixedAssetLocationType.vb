Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.FixedAsset
Imports Microsoft.Practices.Unity

Partial Class FixedAssetService

    Public Function ListAllLocationType(Empresa As String) As List(Of Domain.Entities.FixedAssetLocationType) Implements IFixedAssetLocationTypeService.ListAllLocationType
        Using service As IFixedAssetLocationTypeAdminService = Container.Current.Resolve(Of IFixedAssetLocationTypeAdminService)()
            Return service.ListAllLocationType()
        End Using
        'Return Me._FixedAssetLocationTypeAdminService.ListAllLocationType()
    End Function


End Class
