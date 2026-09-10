'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/04/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository

#End Region

''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>
Public Class MFixedAssetValorization
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtiene por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetValorizationDevaluationByCode(ByVal code As String) As Task(Of ActionResult(Of FixedAssetTransaction))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetValorizationDevaluationByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetValorizationDevaluationById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of ValorizationDevaluation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetValorizationDevaluationByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveTransaction(ByVal record As FixedAssetTransaction, ListDeleteFixedAssetTransactionDetailBook As List(Of FixedAssetTransactionDetailBook), ListDeleteFixedAssetTransactionDetail As List(Of FixedAssetTransactionDetail), ByVal idSequense As Int64) As Task(Of ActionResult(Of FixedAssetTransaction))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveValorizationDevaluationAsync(record, ListDeleteFixedAssetTransactionDetailBook, ListDeleteFixedAssetTransactionDetail, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Confirma
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmValorizationDevaluation(ByVal record As FixedAssetTransaction, ListDeleteFixedAssetTransactionDetailBook As List(Of FixedAssetTransactionDetailBook), ListDeleteFixedAssetTransactionDetail As List(Of FixedAssetTransactionDetail), ByVal idSequense As Int64) As Task(Of ActionResult(Of FixedAssetTransaction))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ConfirmValorizationDevaluationAsync(record, ListDeleteFixedAssetTransactionDetailBook, ListDeleteFixedAssetTransactionDetail, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Anula
    ''' </summary>
    ''' <param name="ValorizationDevaluation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function AnnularValorizationDevaluation(ValorizationDevaluation As ValorizationDevaluation) As Task(Of Domain.Base.Entities.ActionResult(Of ValorizationDevaluation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.AnnularValorizationDevaluationAsync(ValorizationDevaluation, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' lista los proveedores por con sus lineas de distribucion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSuppliersDistributionLines() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListSuppliersDistributionLines()
    End Function

    ''' <summary>
    ''' Inicializa el datasource de centro costo
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InitializeItem()
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPhysicalAsset()
    End Function

    Public Function InitializeParts()
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPhysicalAssetParts()
    End Function

    Public Async Function EquipmentCatalogId(IdEquipmentCatalog As Integer) As Task(Of FixedAssetEquipmentCatalogXpo)
        Dim filtroConsulta As String = "Id = " & IdEquipmentCatalog
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetXPOObject(Of FixedAssetEquipmentCatalogXpo)(filtroConsulta))
    End Function

    ''' <summary>
    ''' lista todas las cuentas de Nivel 5
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccount() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True)
    End Function

    ''' <summary>
    ''' Lista de IVA
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIVA() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListGeneralLedgerIvaByStatus(True)
    End Function

    ''' <summary>
    ''' Obtiene registro de IVA por Id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIVAById(id As Integer) As GeneralLedgerIVAXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.GetXPOObject(Of GeneralLedgerIVAXpo)($"Id={id}")
    End Function

    ''' <summary>
    ''' Obtiene el physical
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetPhysicalById(Id As Integer) As Task(Of FixedAssetPhysicalAssetXpo)
        Dim filtroConsulta As String = "Id = " & Id
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetXPOObject(Of FixedAssetPhysicalAssetXpo)(filtroConsulta))
    End Function

    ''' <summary>
    ''' Consulta EL TRM de las monedas origne vs destino
    ''' </summary>
    ''' <param name="FromCurrencyId"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Public Async Function GetTRMbyCurrencyId(ToCurrencyId As Integer, FromCurrencyId As Integer, Optional DateTrm As Date? = Nothing) As Task(Of ActionResult(Of TRM))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetTRMbyCurrencyIdAsync(ToCurrencyId, FromCurrencyId, Me.Indigo, DateTrm, Nothing)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class


