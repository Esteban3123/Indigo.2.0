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
End Class
