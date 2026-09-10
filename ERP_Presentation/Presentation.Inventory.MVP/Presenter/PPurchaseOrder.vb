'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Henry Alejandro Vargas Polania
' Created          : 23/12/2014
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
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo

#End Region

Public Class PPurchaseOrder

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IPurchaseOrder

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
    Public Sub New(ByRef iview As IPurchaseOrder)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Async Function GetSequense() As Task
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
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
    ''' Obtiene la linea de distribución
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSettingsInventoryByOperatingUnitId(OperatingUnitId As Integer) As SettingInventoryXpo
        Dim filter As String = "OperatingUnitId = " & OperatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of SettingInventoryXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Sub LoadSuppliers(Optional SupplierId As Integer? = Nothing)
        Using Model As New MInventoryContract("")
            Me.View.ListSupplier = Model.ListSupplierMaintenanceXPO(SupplierId)
        End Using
    End Sub

    Public Sub LoadWarehouse()
        Me.View.ListWarehouse = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, Indigo.UserIndigo)
    End Sub

    Public Function GetWarehouseById(wareHouseId As Integer)
        Dim filter As String = "Id = " & wareHouseId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryRepository.WarehouseXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Function GetWarehouseByCode(wareHouseCode As String)
        Dim filter As String = "Code = '" & wareHouseCode & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryRepository.WarehouseXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Sub LoadContractInventory()
        Using Model As New MPurchaseOrder("")
            Me.View.ListContractInventory = Model.ListContractInventoryXPO()
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene el Tercero por el Id del Cliente
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetContractById(Id As Integer) As InventoryRepository.InventoryContractXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryRepository.InventoryContractXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub InitializeSupplier(Optional SupplierId As Integer? = Nothing)
        Using Model As New MInventoryContract("")
            Me.View.SuppliersDistributionLinesXpo = Model.ListSuppliersDistributionLines(SupplierId)
        End Using
    End Sub

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ValidateQuantityPurchaseOrderDetail(PurchaseOrderId As Integer) As List(Of ViewValidateQuantityPurchaseOrderDetailXpo)
        Dim filter As String = "PurchaseOrderId = " & PurchaseOrderId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of ViewValidateQuantityPurchaseOrderDetailXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el listado de productos que fueron agregados en el contrato
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetListProductsOfInventoryContract(inventoryContractId As Integer) As List(Of InventoryContractDetailXpo)
        Dim filter As String = "InventoryContractId.Id = " & inventoryContractId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryContractDetailXpo)(Nothing, filter).ToList()
    End Function

    Public Function GetSupplierDistributionLine(Id As Integer) As List(Of CommonRepository.CommonSuppliersDistibutionLineXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of CommonRepository.CommonSuppliersDistibutionLineXpo)(Nothing, $"Id={Id}").ToList()
    End Function

    ''' <summary>
    ''' inicializa el combo de moneda
    ''' </summary>
    Public Sub InitializeCurrency()
        Using ModelXpo As New MBusqueda
            View.CurrencyDatasource = ModelXpo.ConsultarEntidades(eDataSource.Currency)
        End Using
    End Sub

    ''' <summary>
    ''' inicializa el combo de las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeFunctionalUnit(userId As Integer, userCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListFunctionalUnitAuthorizedByUser(userId, userCode)
    End Function

#Region "Budget Interface"

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryEntity()
        If View.BudgetaryEntityXpo Is Nothing Then
            View.BudgetaryEntityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCollectionBudgetEntityByStatus(True)
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryValidity(budgetEntityId As Integer)
        If View.BudgetaryValidityXpo Is Nothing Then
            View.BudgetaryValidityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCollectionValidityByBudgetByBudgetEntityIdAndStatus(budgetEntityId, 2)
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las disponibilidades
    ''' </summary>
    Public Sub InitializeAvailabilityDetails(BudgetaryValidityId As Integer)
        If View.AvailabilityXpo Is Nothing Then
            Me.View.AvailabilityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListAvailabilityDetails(BudgetaryValidityId)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el detalle de la disponibilidad por el Id
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAvailabilityDetailById(Id As Integer) As BudgetRepository.ViewListAvailabilityDetailXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.GetCollection(Of BudgetRepository.ViewListAvailabilityDetailXpo)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

#End Region

End Class
