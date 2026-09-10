#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

<ServiceContract()> _
Public Interface IFixedAssetInitialBalanceService

    ''' <summary>
    ''' Obtiene un saldo inicial por código
    ''' </summary>
    ''' <param name="Code"></param>
    <OperationContract()>
    Function GetFixedAssetInitialBalance(Code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetInitialBalance)

    ''' <summary>
    ''' Obtiene un saldo inicial por id
    ''' </summary>
    ''' <param name="Id"></param>
    <OperationContract()>
    Function GetFixedAssetInitialBalanceById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetInitialBalance)

    ''' <summary>
    ''' Guarda un saldo inicial
    ''' </summary>
    ''' <param name="FixedAssetInitialBalance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveFixedAssetInitialBalance(FixedAssetInitialBalance As Domain.Entities.FixedAssetInitialBalance, ListDeleteFixedAssetInitialBalanceItem As List(Of FixedAssetInitialBalanceItem), ListDeleteFixedAssetInitialBalanceItemPartsDetailBook As List(Of FixedAssetInitialBalanceItemPartsDetailBook), ListDeleteFixedAssetInitialBalanceItemDetailBook As List(Of FixedAssetInitialBalanceItemDetailBook), ListDeleteFixedAssetInitialBalanceItemParts As List(Of FixedAssetInitialBalanceItemParts), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetInitialBalance)

    ''' <summary>
    ''' Copiar y pegar de saldo inicial de activos fijos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SP_CopyAndPasteFixedAssetInitialBalance(data As List(Of List(Of String))) As ActionResult(Of List(Of FixedAssetInitialBalanceItem), List(Of Tuple(Of String, Integer)))

End Interface
