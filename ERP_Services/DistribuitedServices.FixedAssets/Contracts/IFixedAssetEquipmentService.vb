#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

<ServiceContract()> _
Public Interface IFixedAssetItemService
    <OperationContract()>
    Function ListAllEquipment(session As Infrastructure.CrossCutting.Base.SessionValues) As List(Of FixedAssetItem)

    <OperationContract()>
    Function DeleteEquipment(ByVal Equipment As FixedAssetItem, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult

    <OperationContract()>
    Function SaveEquipment(Equipment As Domain.Entities.FixedAssetItem, session As Infrastructure.CrossCutting.Base.SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of Domain.Entities.FixedAssetItem)

    <OperationContract()> _
    Function GetEquipment(ByVal codeEquipment As String) As ActionResult(Of FixedAssetItem)

    <OperationContract()>
    Function Change_StateEquipment(code As String, state As Boolean, session As SessionValues) As ActionResult(Of FixedAssetItem)

    <OperationContract()>
    Function GetItemCostsPerCurrency(itemId As Integer) As ActionResult(Of List(Of Tuple(Of Decimal, Currency)))
End Interface
