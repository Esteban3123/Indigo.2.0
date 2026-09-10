'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Henry Alejandro Vargas Polania 
' Created          : 14/01/2015
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
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PEntranceVoucher

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IEntranceVoucher

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IEntranceVoucher)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Sub New()

    End Sub

#End Region

#Region "Methods"

    Public Async Function GetSequense() As Task
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Function

    Public Sub LoadSuppliers()
        Using Model As New MInventoryContract("")
            Me.View.ListSupplier = Model.ListSupplierMaintenanceXPO()
        End Using
    End Sub

    Public Sub LoadWarehouse()
        Using Model As New MEntranceVoucher("")
            Me.View.ListWareHouse = Model.ListWarehouseByStatusAndUser(True, Indigo.UserIndigo)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub InitializeSupplier()
        Dim model As New MBusqueda
        Me.View.SuppliersDistributionLinesXpo = CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSuppliersDistributionLines), DevExpress.Xpo.XPInstantFeedbackSource)
    End Sub

    ''' <summary>
    ''' Consulta los tipos de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSupplierType()
        Using model As New MBusqueda
            Me.View.SupplierTypeXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSupplierTypeByStatusTreeList, True)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene la relacion entre proveedor y linea de distribucion
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
    Public Function GetInventoryProductById(Id As Integer) As InventoryProductXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of InventoryProductXpo)(Nothing, filter).FirstOrDefault()
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
    ''' Inicializa el datasource de las actividades económicas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetEconomicActivity()
        Me.View.EconomicActivityXpo = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).TreasuryService.ListEconomicActivity()
    End Sub

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
    ''' Método que obtiene los compromisos generados por el contrato o la orden de compra
    ''' </summary>
    ''' <returns></returns>
    Public Function GetListCommitmentBySourceCodeAndEntityName(listEntranceVoucherDetail As List(Of Domain.Entities.EntranceVoucherDetail), entranceSource As Integer) As List(Of ViewCommitmentDetailXpo)
        Dim entityName As String = IIf(entranceSource = 2, "'PurchaseOrder'", "'InventoryContract'")
        Dim codesFilters As String = String.Join(",", (From x In listEntranceVoucherDetail Select "'" + x.SourceCode + "'").ToArray())
        Dim filter As String = String.Format("EntityName = {0} AND EntityCode IN ({1}) AND Balance > 0", entityName, codesFilters)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of ViewCommitmentDetailXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' inicializa el combo de moneda
    ''' </summary>
    Public Sub InitializeCurrency()
        Using ModelXpo As New MBusqueda
            View.CurrencyDatasource = ModelXpo.ConsultarEntidades(eDataSource.Currency)
        End Using
    End Sub
#End Region

End Class
