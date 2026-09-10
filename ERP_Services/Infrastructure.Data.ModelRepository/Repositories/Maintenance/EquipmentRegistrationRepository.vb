
'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base

#End Region

''' <summary>
''' clase para hacer todas las operaciones de persistencia para la entidad sucursal
''' </summary>
''' <remarks></remarks>
Public Class EquipmentRegistrationRepository
    Inherits GenericRepository(Of EquipmentRegistration)
    Implements IEquipmentRegistration

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' funcion para listar el registro de equipor po codigo de la placa
    ''' </summary>
    ''' <param name="plate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEquipmentRegistration(plate As String) As EquipmentRegistration Implements IEquipmentRegistration.GetEquipmentRegistration
        Dim fixedAssetPhysical As FixedAssetPhysicalAsset = (From f In _context.FixedAssetPhysicalAsset.AsNoTracking() Where f.Plate = plate Select f).FirstOrDefault()
        If fixedAssetPhysical Is Nothing Then
            Return Nothing
        End If

        Dim trademark = (From t In _context.FixedAssetTrademark.AsNoTracking() Where t.Id = fixedAssetPhysical.TrademarkId Select t).FirstOrDefault()
        Dim location = (From l In _context.FixedAssetLocation.AsNoTracking() Where l.Id = fixedAssetPhysical.LocationId Select l).FirstOrDefault()
        Dim policy = (From p In _context.FixedAssetPolicy.AsNoTracking() Where p.Id = fixedAssetPhysical.PolicyId Select p).FirstOrDefault()
        Dim item = (From i In _context.FixedAssetItem.AsNoTracking() Where i.Id = fixedAssetPhysical.ItemId Select i).FirstOrDefault()
        Dim ItemType = (From et In _context.FixedAssetItemType.AsNoTracking() Where et.Id = item.ItemTypeId Select et).FirstOrDefault()
        Dim inventoryType = (From it In _context.FixedAssetInventoryType.AsNoTracking() Where it.Id = ItemType.InventoryTypeId Select it).FirstOrDefault()
        Dim responsible = (From r In _context.FixedAssetResponsible.AsNoTracking() Where r.Id = fixedAssetPhysical.ResponsibleId Select r).FirstOrDefault()
        Dim third = (From t In _context.ThirdParty.AsNoTracking() Where t.Id = responsible.ThirdPartyId Select t).FirstOrDefault()

        Dim query As EquipmentRegistration = (From e In _context.EquipmentRegistration _
                                                  .Include("EquipmentInvima") _
                                                  .Include("ManualDetail") _
                                                  .Include("DrawingsDetail") _
                                                  .Include("TechnicalEquipmentSheet") Where e.FixedAssetPhysicalAssetId = fixedAssetPhysical.Id Select e).FirstOrDefault()
        If query Is Nothing Then
            query = New EquipmentRegistration()
            With query
                .FixedAssetItemId = fixedAssetPhysical.ItemId
                .FixedAssetPhysicalAssetId = fixedAssetPhysical.Id
                .Plate = fixedAssetPhysical.Plate

                .InventoryTypeId = inventoryType.Id
                .EquipmentTypeId = ItemType.Id
                .IdResponsible = fixedAssetPhysical.ResponsibleId

                .EquipmentTypeName = ItemType.Name
                .ArticleName = $"{item.Code} - {item.Description}"
                .InventoryType = inventoryType.Type
                .InventoryTypeName = String.Concat(inventoryType.Code, " - ", inventoryType.Name)
                .TrademarkName = String.Concat(trademark.Code, " - ", trademark.Name)
                .Model = fixedAssetPhysical.Model
                .Serie = fixedAssetPhysical.Serie
                .LocationName = String.Concat(location.Code, " - ", location.Name)
                .AdquisitionTypeName = If(fixedAssetPhysical.AdquisitionType = 1, "Compra Directa", If(fixedAssetPhysical.AdquisitionType = 2, "", If(fixedAssetPhysical.AdquisitionType = 3, "Comodato", If(fixedAssetPhysical.AdquisitionType = 4, "Donación", If(fixedAssetPhysical.AdquisitionType = 5, "Traspaso de Bienes", If(fixedAssetPhysical.AdquisitionType = 6, "Otro Concepto", "Leasing Financiero"))))))
                .NumberContractLeasing = fixedAssetPhysical.NumberContractLeasing
                .PurchaseDate = fixedAssetPhysical.AdquisitionDate
                .PurchaseValue = fixedAssetPhysical.FairValue
                .Other = ""
                .PolizaName = String.Concat(policy.Code, " - ", policy.Name)
                .ResponsibleName = String.Concat(responsible.Code, " - ", third.Name)
                .WarrantyExpirationDate = If(fixedAssetPhysical.WarrantyExpirationDate Is Nothing, DateTime.Now, fixedAssetPhysical.WarrantyExpirationDate) 'fixedAssetPhysical.WarrantyExpirationDate
                .InitialOperationDate = Nothing
                .InstallationDate = If(fixedAssetPhysical.InstallationDate Is Nothing, DateTime.Now, fixedAssetPhysical.InstallationDate)
                .ManufactureDate = DateTime.Now 'Nothing 'fixedAssetPhysical.AdquisitionDate
            End With
        Else
            Dim fabric = (From r In _context.Supplier.AsNoTracking() Where r.Id = query.IdManufacturer Select r).FirstOrDefault()
            Dim seller = (From t In _context.Supplier.AsNoTracking() Where t.Id = query.IdSeller Select t).FirstOrDefault()
            Dim equipmentFunction = (From e In _context.EquipmentFunction.AsNoTracking() Where e.Id = query.IdEquipmentFunction Select e).FirstOrDefault()
            Dim physicalRisk = (From p In _context.PhysicalRisk.AsNoTracking() Where p.Id = query.IdPhysicalRisk Select p).FirstOrDefault()
            Dim accesories = (From a In _context.EquipmentReceptionAccesory Where a.IdEquipmentReception = query.Id Select a).ToList()
            If accesories.Any() Then
                accesories.ForEach(Sub(i)
                                       Dim accesory = (From a In _context.Accessory.AsNoTracking() Where a.Id = i.IdAccesory).FirstOrDefault()
                                       i.AccesoryCode = accesory.Code
                                       i.AccesoryName = accesory.Name
                                       query.EquipmentReceptionAccesory.Add(i)
                                   End Sub)
            End If
            Dim consumables = (From a In _context.EquipmentReceptionConsumable Where a.IdEquipmentReception = query.Id Select a).ToList()
            If consumables.Any() Then
                consumables.ForEach(Sub(i)
                                        Dim consumable = (From a In _context.Consumable.AsNoTracking() Where a.Id = i.IdConsumable).FirstOrDefault()
                                        i.ConsumableCode = consumable.Code
                                        i.ConsumableName = consumable.Name
                                        query.EquipmentReceptionConsumable.Add(i)
                                    End Sub)
            End If
            Dim parts = (From a In _context.EquipmentReceptionPartsAccesoriesConsumables Where a.IdEquipmentReception = query.Id Select a).ToList()
            If parts.Any() Then
                parts.ForEach(Sub(i)
                                  Dim part = (From a In _context.FixedAssetPartsAccesoriesConsumables.AsNoTracking() Where a.Id = i.IdPartsAccesoriesConsumables).FirstOrDefault()
                                  i.PartCode = part.Code
                                  i.PartName = part.Name
                                  query.EquipmentReceptionPartsAccesoriesConsumables.Add(i)
                              End Sub)
            End If
            With query
                .FixedAssetPhysicalAssetId = fixedAssetPhysical.Id
                .FixedAssetItemId = fixedAssetPhysical.ItemId
                .ManufacturerName = If(fabric Is Nothing, "", $"{fabric.Code} - {fabric.Name}")
                .SellerName = If(seller Is Nothing, "", String.Concat(seller.Code, " - ", seller.Name))
                .EquipmentFunctionName = If(equipmentFunction Is Nothing, "", equipmentFunction.Name)
                .PhysicalRiskName = If(physicalRisk Is Nothing, "", physicalRisk.Name)

                .Plate = fixedAssetPhysical.Plate
                .InventoryTypeId = inventoryType.Id
                .EquipmentTypeId = ItemType.Id
                .IdResponsible = fixedAssetPhysical.ResponsibleId

                .EquipmentTypeName = ItemType.Name
                .ArticleName = $"{item.Code} - {item.Description}"
                .InventoryType = inventoryType.Type
                .InventoryTypeName = String.Concat(inventoryType.Code, " - ", inventoryType.Name)
                .TrademarkName = String.Concat(trademark.Code, " - ", trademark.Name)
                .Model = fixedAssetPhysical.Model
                .Serie = fixedAssetPhysical.Serie
                .LocationName = String.Concat(location.Code, " - ", location.Name)
                .AdquisitionTypeName = If(fixedAssetPhysical.AdquisitionType = 1, "Compra Directa", If(fixedAssetPhysical.AdquisitionType = 2, "", If(fixedAssetPhysical.AdquisitionType = 3, "Comodato", If(fixedAssetPhysical.AdquisitionType = 4, "Donación", If(fixedAssetPhysical.AdquisitionType = 5, "Traspaso de Bienes", If(fixedAssetPhysical.AdquisitionType = 6, "Otro Concepto", "Leasing Financiero"))))))
                .NumberContractLeasing = fixedAssetPhysical.NumberContractLeasing
                .PurchaseDate = fixedAssetPhysical.AdquisitionDate
                .PurchaseValue = fixedAssetPhysical.FairValue
                .Other = ""
                .PolizaName = String.Concat(policy.Code, " - ", policy.Name)
                .ResponsibleName = String.Concat(responsible.Code, " - ", third.Name)
            End With
        End If
        Return query
    End Function

    Public Function ListTechnicalLogDetailByFixedAssetPhysicalIdAndEquipmentRegistration(fixedAssetPhysicalAssetId As Integer, equipmentRegistration As Integer) As List(Of TechnicalLogDetail) Implements IEquipmentRegistration.ListTechnicalLogDetailByFixedAssetPhysicalIdAndEquipmentRegistration

        Dim details = (From tl In _context.TechnicalLogDetail Where tl.IdEquipmentRegistration = equipmentRegistration Select tl).ToList()
        If details Is Nothing OrElse Not details.Any() Then
            details = New List(Of TechnicalLogDetail)()
        End If

        Dim query = (From ph In _context.FixedAssetPhysicalAsset
                     Join i In _context.FixedAssetItem On ph.ItemId Equals i.Id
                     Join td In _context.FixedAssetItemTechnicalLog On td.FixedAssetItemId Equals i.Id
                     Join t In _context.TechnicalLog On td.TechnicalLogId Equals t.Id
                     Where ph.Id = fixedAssetPhysicalAssetId
                     Select t).ToList()
        If query IsNot Nothing AndAlso query.Any() Then
            For Each item In query.FindAll(Function(o) Not details.Select(Function(m) m.IdTechnicalLog).Contains(o.Id))
                details.Add(New TechnicalLogDetail() With {.IdTechnicalLog = item.Id, .Name = item.Name, .ValueMin = 0, .ValueMax = 0})
            Next
        End If
        Return details
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllEquipmentRegistration() As List(Of EquipmentRegistration) Implements IEquipmentRegistration.ListAllEquipmentRegistration
        Dim Busqueda = From e In _context.EquipmentRegistration
                       Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="EquipmentRegistration"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveEquipmentRegistration(EquipmentRegistration As EquipmentRegistration) As Boolean Implements IEquipmentRegistration.SaveEquipmentRegistration
        _context.EquipmentRegistration.ApplyChanges(EquipmentRegistration)
        Return True
    End Function

    Public Function ListAccesoryEquipmentType(Type As Integer) As List(Of AccesoryDetail) Implements IEquipmentRegistration.ListAccesoryEquipmentType
        Dim Busqueda = From e In _context.AccesoryDetail.Include("Accessory")
                       Where e.Accessory.State = True And e.IdEquipmentType = Type
                       Select e
        Return Busqueda.ToList
    End Function

    Public Function ListConsumableEquipmentType(Type As Integer) As List(Of ConsumableDetail) Implements IEquipmentRegistration.ListConsumableEquipmentType
        Dim Busqueda = From e In _context.ConsumableDetail.Include("Consumable")
                       Where e.Consumable.State = True And e.IdEquipmentType = Type
                       Select e
        Return Busqueda.ToList
    End Function

End Class
