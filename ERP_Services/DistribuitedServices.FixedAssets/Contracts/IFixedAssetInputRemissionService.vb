#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IFixedAssetFixedAssetRemissionEntranceService
    <OperationContract()>
    Function DeleteFixedAssetRemissionEntrance(FixedAssetRemissionEntrance As FixedAssetRemissionEntrance, audit As AuditMessage) As Boolean

    <OperationContract()>
    Function SaveFixedAssetRemissionEntrance(ByVal FixedAssetRemissionEntrance As FixedAssetRemissionEntrance, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetRemissionEntrance)

    <OperationContract()> _
    Function GetFixedAssetRemissionEntrance(ByVal codeFixedAssetRemissionEntrance As String) As FixedAssetRemissionEntrance
End Interface
