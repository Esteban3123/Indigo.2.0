Imports System.Data.Entity
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class CampaignDetailValidationRepository
    Inherits GenericRepository(Of CampaignDetailValidation)
    Implements ICampaignDetailValidationRepository, Inject

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetCampaignDetailValidationByCampaignDetailId(campaignDetailId As Integer, Optional tracking As Boolean = True) As List(Of CampaignDetailValidation) Implements ICampaignDetailValidationRepository.GetCampaignDetailValidationByCampaignDetailId
        Dim query = (From p In _context.CampaignDetailValidation.Include("InventoryProduct.ATC").AsNoTracking().
                         Include("BatchSerial").AsNoTracking().Include("CampaignDetail.Campaign.CMConfiguration.CMWarehouse.Warehouse").AsNoTracking() Where p.CampaignDetailId = campaignDetailId Select p)

        If Not tracking Then
            query = query.AsNoTracking()
        End If

        Dim data = query.ToList()

        If data.Any() Then
            For Each item In data
                If item.BatchSerial Is Nothing Then
                    item.ExpirationDate = item.InventoryProduct.ExpirationDate
                Else
                    item.ExpirationDate = item.BatchSerial.ExpirationDate
                End If
                If item.InventoryProduct.ATCId IsNot Nothing Then
                    item.DeliveredQuantityUnitMeasurement = item.DeliveredQuantity * IIf(item.InventoryProduct?.ATC?.FormulationType = 2, item.InventoryProduct?.ATC?.Volume, item.InventoryProduct?.ATC?.Weight)
                Else
                    item.DeliveredQuantityUnitMeasurement = item.DeliveredQuantity
                End If
                item.ProductFullName = $"{item.InventoryProduct.Code} - {item.InventoryProduct.Name}"
                item.BatchSerialCode = item.BatchSerial?.BatchCode
                Dim RemnantWareHouse = item.CampaignDetail?.Campaign?.CMConfiguration?.CMWarehouse?. _
                                            Where(Function(x) x.WarehouseType = 6 And x.StateWH).FirstOrDefault

                item.RemnantWareHouseId = RemnantWareHouse.Warehouse.Id
                item.RemnantFullName = $"{RemnantWareHouse.Warehouse.Code} - {RemnantWareHouse.Warehouse.Name}"
            Next
        End If

        Return data
    End Function

    ''' <summary>
    ''' Obtiene las cantidades disponibles para la devolucion de materia prima
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Public Function GetCampaignDetailValidationForDevolution(campaignDetailId As Integer) As List(Of CampaignDetailValidation) Implements ICampaignDetailValidationRepository.GetCampaignDetailValidationForDevolution
        Dim validations = _context.CampaignDetailValidation _
            .Include("InventoryProduct.ATC") _
            .Include("InventoryProduct.ProductType") _
            .Include("BatchSerial") _
            .Include("CampaignDetail.Campaign.CMConfiguration.CMWarehouse.Warehouse") _
            .Include("CampaignDetail.UnitDoseType") _
            .AsNoTracking() _
            .Where(Function(p) p.CampaignDetailId = campaignDetailId) _
            .ToList()

        If Not validations.Any() Then Return validations

        ' La materia prima preparada se consulta desde CampaignRawMaterial porque el kardex
        ' conserva movimientos históricos incluso cuando la solicitud o el producto fue anulado.
        ' En devoluciones solo debe bloquear saldo lo que sigue activo en la preparación.
        Dim rawMaterialRows = _context.CampaignRawMaterial _
            .AsNoTracking() _
            .Where(Function(r) r.RequestPackageDetailStatus.RequestMixingStationDetail.CampaignDetailId = campaignDetailId _
                               AndAlso r.RequestPackageDetailStatus.RequestMixingStationDetail.Status <> 3 _
                               AndAlso r.RequestPackageDetailStatus.Status <> 6) _
            .Select(Function(r) New With {
                .ProductId = r.ProductValidationId,
                r.BatchSerialId,
                .Quantity = r.ExpendQuantity
            }) _
            .ToList()

        ' Otros movimientos de salida que reducen el saldo disponible para devolver.
        ' CampaignRawMaterial se excluye aqui para no duplicar el consumo calculado arriba.
        Dim kardexRows = _context.CampaignKardex _
            .AsNoTracking() _
            .Where(Function(k) k.CampaignDetailId = campaignDetailId _
                               AndAlso k.MovementType = 2 _
                               AndAlso k.EntityName <> "RawMaterialDevolution" _
                               AndAlso k.EntityName <> "CampaignRawMaterial") _
            .Select(Function(k) New With {k.ProductId, k.BatchSerialId, k.Quantity}) _
            .ToList()

        ' Entradas por aprovechamiento: reducen el consumo neto de la materia prima
        ' entregada originalmente a la campaña.
        Dim harnessedRows = _context.CampaignKardex _
            .AsNoTracking() _
            .Where(Function(k) k.CampaignDetailId = campaignDetailId _
                               AndAlso k.MovementType = 1 _
                               AndAlso k.EntityName = "Harnessed") _
            .Select(Function(k) New With {k.ProductId, k.BatchSerialId, k.Quantity}) _
            .ToList()

        For Each item In validations
            If item.BatchSerial Is Nothing Then
                item.ExpirationDate = item.InventoryProduct.ExpirationDate
            Else
                item.ExpirationDate = item.BatchSerial.ExpirationDate
            End If
            Dim conversionFactor = GetConversionFactorForDevolution(item)
            item.DeliveredQuantityUnitMeasurement = item.DeliveredQuantity * conversionFactor
            item.ProductFullName = $"{item.InventoryProduct.Code} - {item.InventoryProduct.Name}"
            item.BatchSerialCode = item.BatchSerial?.BatchCode
            Dim RemnantWareHouse = item.CampaignDetail?.Campaign?.CMConfiguration?.CMWarehouse?.
                                        Where(Function(x) x.WarehouseType = 6 And x.StateWH).FirstOrDefault
            item.RemnantWareHouseId = RemnantWareHouse.Warehouse.Id
            item.RemnantFullName = $"{RemnantWareHouse.Warehouse.Code} - {RemnantWareHouse.Warehouse.Name}"

            Dim batchKey As Integer = If(item.BatchSerialId, 0)
            Dim rawMaterialConsumed = rawMaterialRows _
                .Where(Function(k) k.ProductId = item.ProductId AndAlso If(k.BatchSerialId, 0) = batchKey) _
                .Sum(Function(k) k.Quantity)
            Dim otherConsumed = kardexRows _
                .Where(Function(k) k.ProductId = item.ProductId AndAlso If(k.BatchSerialId, 0) = batchKey) _
                .Sum(Function(k) k.Quantity)
            Dim harnessedQuantity = harnessedRows _
                .Where(Function(k) k.ProductId = item.ProductId AndAlso If(k.BatchSerialId, 0) = batchKey) _
                .Sum(Function(k) k.Quantity)
            Dim rawConsumed = Math.Max(0D, rawMaterialConsumed + otherConsumed - harnessedQuantity)

            item.HarnessedQuantity = harnessedQuantity
            item.ConsumedQuantity = GetConsumedQuantityForDevolution(rawConsumed, conversionFactor)
        Next

        ' Solo devolver ítems con saldo positivo disponible para devolución
        Return validations.Where(Function(v) v.DeliveredQuantity - v.ConsumedQuantity - v.DevolutionQuantity > 0).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el factor de devolucion dependiendo del tipo de formulacion del medicamento con la excepcion
    ''' de que para las NPT siempre se toma el volumen
    ''' </summary>
    ''' <param name="item"></param>
    ''' <returns></returns>
    Private Function GetConversionFactorForDevolution(item As CampaignDetailValidation) As Decimal
        Dim atc = item.InventoryProduct?.ATC
        If atc Is Nothing OrElse item.InventoryProduct?.ProductType?.Class <> 2 Then
            Return 1D
        End If

        ' En NPT las cantidades solicitadas y consumidas se manejan en mililitros;
        ' por eso el factor de conversion debe ser siempre el volumen del producto.
        If item.CampaignDetail?.UnitDoseType?.MSClass = 2 Then
            Return If(atc.Volume > 0, CDec(atc.Volume), 1D)
        End If

        Select Case atc.FormulationType
            Case 4
                Return If(atc.ConcentrationQuantity > 0, CDec(atc.ConcentrationQuantity), 1D)
            Case 2
                Return If(atc.Volume > 0, CDec(atc.Volume), 1D)
            Case Else
                Return If(atc.Weight > 0, CDec(atc.Weight), 1D)
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la cantidad consumida de producto en la campaña
    ''' </summary>
    ''' <param name="rawConsumed"></param>
    ''' <param name="conversionFactor"></param>
    ''' <returns></returns>
    Private Function GetConsumedQuantityForDevolution(rawConsumed As Decimal, conversionFactor As Decimal) As Integer
        If rawConsumed <= 0D Then
            Return 0
        End If

        If conversionFactor <= 0D Then
            conversionFactor = 1D
        End If

        ' Si se usa cualquier fraccion de una presentacion, la unidad fisica queda consumida
        ' para devolucion. Ej.: 1061 mL de una bolsa de 500 mL consumen 3 unidades.
        Return CInt(Math.Ceiling(rawConsumed / conversionFactor))
    End Function

End Class
