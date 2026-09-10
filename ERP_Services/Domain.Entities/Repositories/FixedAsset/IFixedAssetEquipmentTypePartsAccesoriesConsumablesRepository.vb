'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 27-08-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Maintenance.Entities
#End Region

Public Interface IFixedAssetItemTypePartsAccesoriesConsumablesRepository
    Inherits IRepository(Of FixedAssetItemTypePartsAccesories)
End Interface
