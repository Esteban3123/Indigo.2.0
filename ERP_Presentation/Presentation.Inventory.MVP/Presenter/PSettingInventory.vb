'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Faiber Julian Mora Dussan
' Created          : 30-01-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class PSettingInventory

#Region "Fields"

    ''' <summary>
    ''' Referencia a la vista de la interfaz
    ''' </summary>
    ''' <remarks></remarks>
    Private View As ISettingInventory

    ''' <summary>
    ''' Instancia de los valores de sesión
    ''' </summary>
    ''' <remarks></remarks>
    Private _sessionValues As SessionValues


#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal view As ISettingInventory)
        Me.View = view
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    'Tipo comprobante contable

    Public Sub InitializeJournalVoucherType()
        InitializePurchaseJournalVoucherTypeId()
        InitializeSalesJournalVoucherTypeId()
        InitializeRemissionEntranceJournalVoucherTypeId()
        InitializeRemissionOutputJournalVoucherTypeId()
        InitializeRemissionEntranceDevolutionJournalVoucherTypeId()
        InitializeRemissionOutputDevolutionJournalVoucherTypeId()
        InitializeReclassificationRemissionJournalVoucherTypeId()
        InitializeLoanJournalVoucherTypeId()
        InitializeSalesReturnJournalVoucherTypeId()
        InitializePurchaseReturnJournalVoucherTypeId()
        InitializeOrderDispatchReturnJournalVoucherTypeId()
        InitializeOrderDispatchJournalVoucherTypeId()
        InitializeLoanReturnJournalVoucherTypeId()
    End Sub

    Public Function GetSettingsPaymentsByOperatingUnitId(OperatingUnitId As Integer) As PaymentsSettingPaymentsXpo
        Dim filter As String = "IdOperatingUnit = " & OperatingUnitId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsSettingPaymentsXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Sub InitializePurchaseJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.PurchaseJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeSalesJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.SalesJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeRemissionEntranceJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.RemissionEntranceJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeRemissionOutputJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.RemissionOutputJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeRemissionEntranceDevolutionJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.RemissionEntranceDevolutionJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeRemissionOutputDevolutionJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.RemissionOutputDevolutionJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeReclassificationRemissionJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.ReclassificationRemissionJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeLoanJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.LoanJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeSalesReturnJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.SalesReturnJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializePurchaseReturnJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.PurchaseReturnJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeInventoryAdjustmentJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.InventoryAdjustmentJournalVoucherTypeXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeInventoryCloseAdjustmentJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.InventoryCloseAdjustmentJournalVoucherTypeXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeConsignmentMerchandiseJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.ConsignmentMerchandiseJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeConsignmentMerchandiseReturnJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.ConsignmentMerchandiseReturnJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeConsignmentInventoryUseJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.ConsignmentInventoryUseJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeConsignmentInventoryUseDevolutionJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.ConsignmentInventoryUseDevolutionJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeOrderDispatchReturnJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.OrderDispatchReturnJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeOrderDispatchJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.OrderDispatchJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializeLoanReturnJournalVoucherTypeId()
        Using model As New MBusqueda
            Me.View.LoanReturnJournalVoucherTypeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    Public Sub InitializePartialReturnSalesJournalVoucherType()
        Using model As New MBusqueda
            Me.View.PartialReturnSalesJournalVoucherTypeXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    ''' <summary>
    ''' Traslado entre almacenes en consignación
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeTransferBetweenWarehousesConsignment()
        Using model As New MBusqueda
            Me.View.TransferBetweenWarehousesConsignmentXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    ''' <summary>
    ''' Traslado entre almacenes en consignación
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeValuationConsignmentPriceJournalVoucherTypes()
        Using model As New MBusqueda
            Me.View.ValuationConsignmentPriceJournalVoucherTypesIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListDocumentTypes)
        End Using
    End Sub

    'Concepto de pago

    Public Sub InitializeAccountPayableConcepts()
        InitializeIVAFreightAccountPayableConceptId()
        InitializeFreightAccountPayableConceptId()
        InitializeProDevelopmentAccountPayableConceptId()
        InitializeProElectrificationAccountPayableConceptId()
        InitializeProCultureAccountPayableConceptId()
        InitializeProHospitalAccountPayableConceptId()
        InitializeProGameAccountPayableConceptId1()
        InitializeIVARetentionAccountPayableConceptId()
        InitializeIVAAccountPayableConceptId()
    End Sub

    Public Sub InitializeIVAFreightAccountPayableConceptId()
        Dim filterConcept() As Object = {True, False, 2}
        Using msearch As New MBusqueda
            Me.View.IVAFreightAccountPayableConceptIdXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filterConcept)
        End Using
    End Sub

    Public Sub InitializeFreightAccountPayableConceptId()
        Dim filterConcept() As Object = {True, False, 2}
        Using msearch As New MBusqueda
            Me.View.FreightAccountPayableConceptIdXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filterConcept)
        End Using
    End Sub

    Public Sub InitializeProDevelopmentAccountPayableConceptId()
        Dim filterConcept() As Object = {True, True, 2}
        Using msearch As New MBusqueda
            Me.View.ProDevelopmentAccountPayableConceptIdXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filterConcept)
        End Using
    End Sub

    Public Sub InitializeProElectrificationAccountPayableConceptId()
        Dim filterConcept() As Object = {True, True, 2}
        Using msearch As New MBusqueda
            Me.View.ProElectrificationAccountPayableConceptIdXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filterConcept)
        End Using
    End Sub

    Public Sub InitializeProCultureAccountPayableConceptId()
        Dim filterConcept() As Object = {True, True, 2}
        Using msearch As New MBusqueda
            Me.View.ProCultureAccountPayableConceptIdXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filterConcept)
        End Using
    End Sub

    Public Sub InitializeProHospitalAccountPayableConceptId()
        Dim filterConcept() As Object = {True, True, 2}
        Using msearch As New MBusqueda
            Me.View.ProHospitalAccountPayableConceptIdXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filterConcept)
        End Using
    End Sub

    Public Sub InitializeProGameAccountPayableConceptId1()
        Dim filterConcept() As Object = {True, True, 2}
        Using msearch As New MBusqueda
            Me.View.ProGameAccountPayableConceptId1Xpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filterConcept)
        End Using
    End Sub

    Public Sub InitializeIVARetentionAccountPayableConceptId()
        Dim filterConcept() As Object = {True, True, 2}
        Using msearch As New MBusqueda
            Me.View.IVARetentionAccountPayableConceptIdXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filterConcept)
        End Using
    End Sub
    ''' <summary>
    ''' concepto pago iva
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeIVAAccountPayableConceptId()
        Dim filterConcept() As Object = {True, False, 1}
        Using msearch As New MBusqueda
            Me.View.IVAAccountPayableConceptIdXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filterConcept)
        End Using
    End Sub

    Public Sub InitializeAdjustmentAccountPayableConceptId()
        Dim filterConcept() As Object = {True, False, 2}
        Using msearch As New MBusqueda
            Me.View.AdjustmentAccountPayableConceptIdXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filterConcept)
        End Using
    End Sub

    'Unidad de radicacion

    ''' <summary>
    ''' Inicializa el datasource de unidad de radicacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeFilingUnit()
        Using model As New MBusqueda
            Me.View.FilingUnitIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFilingUnitByStatusCollection, True)
        End Using
    End Sub

    'Cuenta contable

    Public Sub InitializeIVAGeneratedMainAccountId()
        Dim filter() As Object = {5, True}
        Using modelAccountsXPO As New MBusqueda
            Me.View.IVAGeneratedMainAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeDiscountSalesMainAccountId()
        Dim filter() As Object = {5, True}
        Using modelAccountsXPO As New MBusqueda
            Me.View.DiscountSalesMainAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeCostAccount()
        Dim filter() As Object = {5, True}
        Using modelAccountsXPO As New MBusqueda
            Me.View.CostAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeSalesAccount()
        Dim filter() As Object = {5, True}
        Using modelAccountsXPO As New MBusqueda
            Me.View.SalesAccountIdXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    'Unidad funcional

    Public Sub InitializeFunctionalUnit()
        Using model As New MBusqueda
            Me.View.FunctionalUnitIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFunctionalUnit, True)
        End Using
    End Sub

    'Concepto de nota de pago para cuando se haga devolucion del comprobante de entrada

    Public Sub InitializeRefundAccountPayableConceptNote()
        Dim filter() As Object = {True, 1}
        Using model As New MBusqueda
            Me.View.RefundAccountPayableConceptNoteXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListConceptsNotesByConcepType, filter)
        End Using
    End Sub

    'Concepto de ajuste de inventario tipo entrada

    Public Sub InitializeInputAdjustmentConcept()
        Dim filter() As Object = {1, 1, True}
        Using model As New MBusqueda
            Me.View.InputAdjustmentConceptXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAdjustmentConceptByConceptType, filter)
        End Using
    End Sub

    'Concepto de ajuste de inventario tipo salida

    Public Sub InitializeOutputAdjustmentConcept()
        Dim filter() As Object = {1, 2, True}
        Using model As New MBusqueda
            Me.View.OutputAdjustmentConceptXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAdjustmentConceptByConceptType, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia Iva
    ''' </summary>
    Public Sub InitializeIva()
        Using Model As New MBusqueda
            Me.View.IVADatasource = Model.ConsultarEntidades(eDataSource.ListGeneralLedgerIva)
        End Using
    End Sub

    ''' <summary>
    ''' Listado de almacenes activos
    ''' </summary>
    Public Sub ListAllActiveWarehouse()
        Using Model As New MSettingInventory("")
            Me.View.WarehouseXpo = Model.ListAllActiveWarehouse()
        End Using
    End Sub
#End Region

End Class
