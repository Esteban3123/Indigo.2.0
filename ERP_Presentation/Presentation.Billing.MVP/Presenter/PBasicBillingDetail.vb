'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-11-14
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

#End Region

Public Class PBasicBillingDetail

#Region "Fields"

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IBasicBillingDetail

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IBasicBillingDetail)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la unidad funcional por id
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetFunctionalUnitById(Id As Integer) As PayrollRepository.PayrollFunctionalUnit
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of PayrollRepository.PayrollFunctionalUnit)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista las tarifas de productos y servicios
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeProductAndServiceFee()
        View.ProductAndServiceFeeXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListProductAndServicesFeeByStatus(True, Indigo.UserIndigo)
    End Sub

    ''' <summary>
    ''' Lista las autorizaciones de facturacion habilitadas para el tercero
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBillingConceptXPO()
        View.BillingConceptXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListAssociatedMainServiceId(0, Nothing, 1)
    End Sub

    ''' <summary>
    ''' Lista las autorizaciones de facturacion habilitadas para el tercero
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeServiceFeeXPO(ProductAndServiceId As Integer)
        View.BillingConceptXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListServiceFeeDetail(ProductAndServiceId)
    End Sub

    ''' <summary>
    ''' Inicializa conceptos de facturacion
    ''' </summary>
    Public Sub InitializeServicesProvidedXPO(AssociatedMainServiceId As Integer)
        View.ServicesProvidedXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListAssociatedSecondaryService(AssociatedMainServiceId)
    End Sub

    ''' <summary>
    ''' Inicializa los proovedores
    ''' </summary>
    Public Sub InitializeSupplierXPO()
        View.SupplierXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListSupplierByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa los ejecutivo de ventas
    ''' </summary>
    Public Sub InitializeSalesExecutiveXPO()
        View.SalesExecutiveXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListSalesExecutiveByStatus(True)
    End Sub

    ''' <summary>
    ''' Lista los clientes activos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializePhysicalAssetXPO()
        View.PhysicalAssetXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPhysicalAssetHasOutput()
    End Sub

    ''' <summary>
    ''' Lista las direcciones por tercero
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializePhysicalAssetPartXPO()
        View.PhysicalAssetPartXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPhysicalAssetParts()
    End Sub

    ''' <summary>
    ''' Lista las unidades funcionales activas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeFunctionalUnitXPO()
        View.FunctionalUnitXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListFunctionalUnit(True)
    End Sub

    ''' <summary>
    ''' Lista los almacenes habilitadas para el tercero
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeWarehouseXPO(ListWareHouse As List(Of Integer))
        View.WarehouseXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUserAndProduct(True, Indigo.UserIndigo, ListWareHouse)
    End Sub

    ''' <summary>
    ''' Lista todas las actividades económicas activas y que sean generadoras de ingreso
    ''' </summary>
    Public Sub InitializeEconomicActivityDatasource()
        View.EconomicActivityDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListEconomicActivity()
    End Sub
#End Region

End Class
