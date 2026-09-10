'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/04/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IFixedAssetValorizationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveValorizationDevaluation(FixedAssetTransaction As FixedAssetTransaction, ListDeleteFixedAssetTransactionDetailBook As List(Of FixedAssetTransactionDetailBook), ListDeleteFixedAssetTransactionDetail As List(Of FixedAssetTransactionDetail), audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetTransaction)

    ''' <summary>
    ''' Elimina
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function AnnularValorizationDevaluation(ByVal ValorizationDevaluation As ValorizationDevaluation, ByVal audit As AuditMessage) As ActionResult(Of ValorizationDevaluation)

    ''' <summary>
    ''' Confirma
    ''' </summary>
    ''' <param name="FixedAssetTransaction"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmValorizationDevaluation(FixedAssetTransaction As FixedAssetTransaction, ListDeleteFixedAssetTransactionDetailBook As List(Of FixedAssetTransactionDetailBook), ListDeleteFixedAssetTransactionDetail As List(Of FixedAssetTransactionDetail), audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetTransaction)

    ''' <summary>
    ''' Consulta por id
    ''' </summary>
    ''' <returns></returns>
    Function GetValorizationDevaluationById(ByVal Id As Integer, ByVal audit As AuditMessage) As ActionResult(Of ValorizationDevaluation)

    ''' <summary>
    ''' Consulta por código
    ''' </summary>
    ''' <returns></returns>
    Function GetValorizationDevaluationByCode(ByVal Code As String, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetTransaction)

End Interface
