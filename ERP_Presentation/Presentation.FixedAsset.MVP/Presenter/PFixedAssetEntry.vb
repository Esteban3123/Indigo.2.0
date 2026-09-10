'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 20-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base

#End Region

Public Class PFixedAssetEntry

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFixedAssetEntry

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFixedAssetEntry)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetSequense() As Task
        Using model As New MBlockRecordAndSequenceFixedAsset(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Function

    ''' <summary>
    ''' Parametros de activos fijos
    ''' </summary>
    ''' <param name="OperatingUnitId"></param>
    ''' <remarks></remarks>
    Public Async Function GetSettingFixedAssetByOperatingUnitId(OperatingUnitId As Integer) As Task
        Using model As New MSettingFixedAsset(Me.View.MyTag)
            View.SettingsFixedAsset = Await model.GetSettingFixedAssetByOperatingUnitId(OperatingUnitId)
        End Using
    End Function

    ''' <summary>
    ''' lista las Marcas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSupplierDistributionLine()
        View.SupplierDistributionLineXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListSuppliersDistributionLines()
    End Sub

    ''' <summary>
    ''' lista las Marcas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeLocation()
        View.LocationXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetLocationByStatus()
    End Sub

    ''' <summary>
    ''' lista las Marcas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeResponsible()
        View.ResponsibleXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetResponsibleByStatus(True)
    End Sub

    ''' <summary>
    ''' lista las Marcas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCostCenter()
        View.CostCenterXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Sub

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListFixedAssetRemissionEntranceBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As List(Of FixedAssetRemissionEntranceXpo)
        Dim filtroConsulta As String = "SupplierDistributionLineId = " & SupplierDistributionLineId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetRemissionEntranceXpo)(Nothing, filtroConsulta)
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSupplierDistributionLineById(Id As Integer) As CommonSuppliersDistibutionLineXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of CommonSuppliersDistibutionLineXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetFixedAssetItemById(Id As Integer) As FixedAssetItemReportXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of FixedAssetItemReportXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAccountPayableConceptById(Id As Integer) As PaymentsAccountPayableConceptsXpoP
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of PaymentsAccountPayableConceptsXpoP)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetRetentionConceptById(Id As Integer) As GeneralLedgerRetentionConceptsReportXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of GeneralLedgerRetentionConceptsReportXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene la linea de distribución
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSettingsPaymentsByOperatingUnitId(OperatingUnitId As Integer) As PaymentsSettingPaymentsXpo
        Dim filter As String = "IdOperatingUnit = " & OperatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsSettingPaymentsXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Inicializa el datasource de los compromisos
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    Public Sub InitializeCommitmentDetail(ThirdPartyId As Integer, BudgetaryValidityId As Integer)
        View.CommitmentDetailXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCommitmentDetailByThirdPartyIdAndStatusXpCollection(ThirdPartyId, 2, BudgetaryValidityId)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryEntity()
        View.BudgetaryEntityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCollectionBudgetEntityByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryValidity(budgetEntityId As Integer)
        View.BudgetaryValidityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCollectionValidityByBudgetByBudgetEntityIdAndStatus(budgetEntityId, 2)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de documentos soportes
    ''' </summary>    
    ''' <remarks></remarks>
    Public Sub InitializeDocumentSupport()
        If View.DocumentSupportXpo Is Nothing Then
            View.DocumentSupportXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListDocumentSupportAuthorizationByUser(Indigo.UserIndigo)
        End If
    End Sub
    ''' <summary>
    ''' Inicializa el datasource de la actividad economica
    ''' </summary>
    Public Sub InitializeEconomicActivity()
        Me.View.EconomicActivityDatasource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).TreasuryService.ListEconomicActivity()
    End Sub

    ''' <summary>
    ''' Método que obtiene los compromisos generados por la orden de compra
    ''' </summary>
    ''' <returns></returns>
    Public Function GetListCommitmentBySourceCodeAndEntityName(list As List(Of Domain.Entities.FixedAssetEntryItem)) As List(Of BudgetCommitmentDetailXpo)
        Dim entityName As String = "'FixedAssetPurchaseOrder'"
        Dim codesFilters As String = String.Join(",", (From x In list Where x.SourceCode IsNot Nothing AndAlso x.PurchaseOrderItemId IsNot Nothing Select "'" + x.SourceCode + "'").ToArray())
        Dim filter As String = "CommitmentId.EntityCode in (" & codesFilters & ") and CommitmentId.EntityName = " & entityName
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of BudgetCommitmentDetailXpo)(Nothing, filter).ToList()
    End Function

#End Region

End Class
