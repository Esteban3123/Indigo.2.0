#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

<ServiceContract()> _
Public Interface IFixedAssetValorizationService

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveValorizationDevaluation(FixedAssetTransaction As Domain.Entities.FixedAssetTransaction, ByVal ListDeleteFixedAssetTransactionDetailBook As List(Of Domain.Entities.FixedAssetTransactionDetailBook), ByVal ListDeleteFixedTransactionDetail As List(Of Domain.Entities.FixedAssetTransactionDetail), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetTransaction)

    ''' <summary>
    ''' Elimina
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function AnnularValorizationDevaluation(ValorizationDevaluation As Domain.Entities.ValorizationDevaluation, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ValorizationDevaluation)

    ''' <summary>
    ''' Confirma
    ''' </summary>
    ''' <param name="FixedAssetTransaction"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmValorizationDevaluation(FixedAssetTransaction As Domain.Entities.FixedAssetTransaction, ByVal ListDeleteFixedAssetTransactionDetailBook As List(Of Domain.Entities.FixedAssetTransactionDetailBook), ByVal ListDeleteFixedTransactionDetail As List(Of Domain.Entities.FixedAssetTransactionDetail), idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetTransaction)

    ''' <summary>
    ''' Consulta por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetValorizationDevaluationById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ValorizationDevaluation)

    ''' <summary>
    ''' Consulta por código
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetValorizationDevaluationByCode(Code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetTransaction)

End Interface
