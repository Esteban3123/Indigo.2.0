'************************************************************
' Assembly         : Application.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/04/2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IFixedAssetInitialBalanceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un saldo inicial por código
    ''' </summary>
    ''' <param name="Code"></param>
    Function GetFixedAssetInitialBalance(ByVal Code As String, audit As AuditMessage) As ActionResult(Of FixedAssetInitialBalance)

    ''' <summary>
    ''' Obtiene un saldo inicial por id
    ''' </summary>
    ''' <param name="Id"></param>
    Function GetFixedAssetInitialBalanceById(ByVal Id As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetInitialBalance)

    ''' <summary>
    ''' Guarda un saldo inicial
    ''' </summary>
    ''' <param name="FixedAssetInitialBalance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFixedAssetInitialBalance(ByVal FixedAssetInitialBalance As FixedAssetInitialBalance, ListDeleteFixedAssetInitialBalanceItem As List(Of FixedAssetInitialBalanceItem), ListDeleteFixedAssetInitialBalanceItemPartsDetailBook As List(Of FixedAssetInitialBalanceItemPartsDetailBook), ListDeleteFixedAssetInitialBalanceItemDetailBook As List(Of FixedAssetInitialBalanceItemDetailBook), ListDeleteFixedAssetInitialBalanceItemParts As List(Of FixedAssetInitialBalanceItemParts), ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetInitialBalance)

    ''' <summary>
    ''' Copiar y pegar de saldo inicial de activos fijos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPasteFixedAssetInitialBalance(data As List(Of List(Of String))) As ActionResult(Of List(Of FixedAssetInitialBalanceItem), List(Of Tuple(Of String, Integer)))

End Interface
