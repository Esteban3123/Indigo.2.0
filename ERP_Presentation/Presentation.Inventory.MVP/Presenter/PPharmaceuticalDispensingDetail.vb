'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego Andrés Roldán
' Created          : 26-01-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class PPharmaceuticalDispensingDetail

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IPharmaceuticalDispensingDetail

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IPharmaceuticalDispensingDetail)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the admission number.
    ''' </summary>
    Public Sub LoadCareGroup()
        Me.View.CareGroupDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCareGroupByStatus(True)
    End Sub

    ''' <summary>
    ''' Loads the product.
    ''' </summary>
    Public Sub LoadProduct()
        Me.View.ProductDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventoryProductByStatusByNoClassType(True, 1) 'Clase Items
    End Sub

    Public Function GetWareHouseById(Id As Integer) As WarehouseXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of WarehouseXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Loads the ware house.
    ''' </summary>
    Public Sub LoadWareHouse()
        Me.View.WareHouseDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListNoTransitWarehouseByStatusAndUser(True, Indigo.UserIndigo)
    End Sub

    ''' <summary>
    ''' Carga el datasource de los almacenes para las cotizaciones
    ''' </summary>
    Public Sub LoadWareHouseVirtualStore()
        Me.View.WareHouseDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListVirtualWarehouseByStatusAndUser(True, Indigo.UserIndigo)
    End Sub

    ''' <summary>
    ''' Loads the health professional.
    ''' </summary>
    Public Sub LoadHealthProfessional()
        Me.View.HealthProfessionalDatasource = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListHealthCareProfessional()
    End Sub

    ''' <summary>
    ''' Loads the cups entity.
    ''' </summary>
    Public Sub LoadCupsEntity(admissionNumber As String, recordType As Integer)
        Me.View.CupsDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListServiceOrderDetailByAdmissionNumberRecordType(admissionNumber, recordType)
    End Sub

    ''' <summary>
    ''' carga las unidades funcionales
    ''' </summary>
    Public Sub LoadFunctionalUnit()
        Me.View.FunctionalUnitDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListFunctionalUnit(True)
    End Sub

    ''' <summary>
    ''' Obtiene los productos que necesiten una cotización
    ''' </summary>
    ''' <param name="listTuple"></param>
    ''' <returns></returns>
    Public Function GetProductsWithQuoted(listTuple As List(Of Tuple(Of Integer, Integer, Date))) As String
        Dim messageReturn As String = String.Empty
        Dim listProductRateDetail As New List(Of Infrastructure.Data.Xpo.InventoryRepository.ProductRateDetailXpo)

        'Se recorre el listado de tupla en donde contiene Item1 = ProductId, Item2 = CareGroupId, Item3 = ServiceDate
        For Each item In listTuple
            Dim currentDate As Date = item.Item3
            Dim filterCareGroup = "Id = " & item.Item2
            Dim careGroup = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of CareGroupXpo)(Nothing, filterCareGroup).FirstOrDefault()

            Dim filterProductRateDetail As String = "Quoted = 1 and ProductId.Id = " & item.Item1 & " and ProductRateId.Id = " & careGroup.ProductRateId.Id & " and GETDATE(InitialDate) <= GETDATE('" & currentDate.Date & "') and GETDATE(EndDate) >= GETDATE('" & currentDate.Date & "')"
            Dim productRateDetail = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of Infrastructure.Data.Xpo.InventoryRepository.ProductRateDetailXpo)(Nothing, filterProductRateDetail).FirstOrDefault()

            If productRateDetail IsNot Nothing Then
                listProductRateDetail.Add(productRateDetail)
            End If
        Next

        If listProductRateDetail IsNot Nothing AndAlso listProductRateDetail.Count > 0 Then
            messageReturn = "Los productos " + String.Join(",", (From x In listProductRateDetail Select x.ProductId.Code + " - " + x.ProductId.Description).ToArray()) + " requieren de una cotización, desea agregar una cotización?"
        End If

        Return messageReturn
    End Function

    ''' <summary>
    ''' Obtiene los servicios de las cotizaciones confirmadas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListQuotationPharmaceuticalDispensingDetail(patientCode As String, listProductId As List(Of Integer)) As List(Of QuotationPharmaceuticalDispensingDetailXpo)
        Dim filter As String = ""

        If listProductId Is Nothing Then 'Si el listado viene vacío es porque la importación se realiza desde la cabecera de la dispensación
            filter = "QuotationId.Status = 2 and QuotationId.ThirdPartyId.Nit = '" & patientCode & "' and (BillingPharmaceuticalDispensingDetailXpo is null or BillingPharmaceuticalDispensingDetailXpo.Count() = 0)"
        Else 'Si el listado viene lleno es porque la importación se realiza desde el detalle de la orden, control cuentas hospitalario o ambulatorio
            Dim joinCupsIds As String = String.Join(",", listProductId.ToArray())
            filter = "QuotationId.Status = 2 and QuotationId.ThirdPartyId.Nit = '" & patientCode & "' and (BillingPharmaceuticalDispensingDetailXpo is null or BillingPharmaceuticalDispensingDetailXpo.Count() = 0) and ProductId.Id in (" & joinCupsIds & ")"
        End If

        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of QuotationPharmaceuticalDispensingDetailXpo)(Nothing, filter).ToList()
    End Function

    Public Function GetInventoryProductXpoAffectInventory(warehouseId As Integer, productCode As String, virtualStore As Boolean) As InventoryRepository.InventoryProductXpo
        Dim filter As String = ""

        If virtualStore = False Then
            filter = "not ProductTypeId.Class in (1, 4) and Status = 1 and PhysicalInventoryXpo[Quantity > 0 and WarehouseId.Id = " + warehouseId.ToString() + " ] and Code = '" + productCode + "'"
        Else
            filter = "not ProductTypeId.Class in (1, 4) and Status = 1 and Code = '" + productCode + "'"
        End If

        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryRepository.InventoryProductXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Function GetInventoryProductXpoNotAffectInventory(warehouseId As Integer, productCode As String, virtualStore As Boolean) As InventoryRepository.InventoryProductXpo
        Dim filter As String = ""

        If virtualStore = False Then
            filter = "ProductTypeId.Class != 1 and Status = 1 and PhysicalInventoryXpo[Quantity >= 0 and WarehouseId.Id = " + warehouseId.ToString() + " ] and Code = '" + productCode + "'"
        Else
            filter = "ProductTypeId.Class != 1 and Status = 1 and Code = '" + productCode + "'"
        End If

        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryRepository.InventoryProductXpo)(Nothing, filter).FirstOrDefault()
    End Function

End Class