#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface ISettingFixedAssetService

    <OperationContract()> _
    Function DeleteSettingFixedAsset(ByVal SettingFixedAsset As SettingFixedAsset, ByVal audit As AuditMessage) As Boolean

    <OperationContract()> _
    Function SaveSettingFixedAsset(ByVal SettingFixedAsset As SettingFixedAsset, ByVal audit As AuditMessage) As ActionResult(Of SettingFixedAsset)

    <OperationContract()> _
    Function GetSettingFixedAssetByOperatingUnitId(OperatingUnidId As Integer, ByVal audit As AuditMessage) As SettingFixedAsset

End Interface
