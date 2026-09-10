'***********************************************************************
' Assembly         : Infrastructure.Data.MaintenanceRepository
' Author           : Diego Roldan Lozano
' Created          : 2018-08-31
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class MaintenanceProtocolRepository
    Inherits GenericRepository(Of MaintenanceProtocol)
    Implements IMaintenanceProtocolRepository

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

    Public Function GetMaintenanceProtocolByCode(code As String) As MaintenanceProtocol Implements IMaintenanceProtocolRepository.GetMaintenanceProtocolByCode
        Dim query = (From p In _context.MaintenanceProtocol.Include("ProtocolConsumables").Include("ProtocolSupplier").Include("ProtocolTools").Include("ProtocolActivities")
                     Where p.Code.Equals(code) Select p).FirstOrDefault()
        If query IsNot Nothing Then

            query.FixedAssetItemName = (From e In _context.FixedAssetItem.AsNoTracking() Where e.Id = query.FixedAssetItemId Select e).FirstOrDefault().Description
            'Dim third = (From r In _context.FixedAssetResponsible.AsNoTracking()
            '             Join t In _context.ThirdParty.AsNoTracking() On r.ThirdPartyId Equals t.Id
            '             Where r.Id = query.ResponsibleId
            '             Select t).FirstOrDefault()

            Dim responsibleType = (From r In _context.ResponsibleType.AsNoTracking() Where r.Id = query.ResponsibleTypeId Select r).FirstOrDefault()


            query.ResponsibleTypeName = $"{responsibleType.Code} - {responsibleType.Description}"

            If query.ProtocolActivities.Any() Then
                For Each item In query.ProtocolActivities
                    item.TimeName = $"{item.Time} {If(item.Unit = 1, "Minutos", If(item.Unit = 2, "Horas", "Días"))}"
                Next
            End If
            If query.ProtocolConsumables.Any() Then
                For Each item In query.ProtocolConsumables
                    Dim consumable As Consumable = (From c In _context.Consumable.AsNoTracking() Where c.Id = item.ConsumableId Select c).FirstOrDefault()
                    item.ConsumableCode = consumable.Code
                    item.ConsumableName = consumable.Name
                Next
            End If
            If query.ProtocolSupplier.Any() Then
                For Each item In query.ProtocolSupplier
                    Dim supplies As InventoryProduct = (From c In _context.InventoryProduct.AsNoTracking() Where c.Id = item.ProductId Select c).FirstOrDefault()
                    item.ProductCode = supplies.Code
                    item.ProductName = supplies.Name
                Next
            End If
            If query.ProtocolTools.Any() Then
                For Each item In query.ProtocolTools
                    Dim physical As FixedAssetPhysicalAsset = (From c In _context.FixedAssetPhysicalAsset.AsNoTracking() Where c.Id = item.FixedAssetPhysicalAssetId Select c).FirstOrDefault()
                    Dim itemFixed As FixedAssetItem = (From i In _context.FixedAssetItem Where i.Id = physical.ItemId Select i).FirstOrDefault()
                    item.PhysicalAssetPlate = physical.Plate
                    item.PhysicalAssetItemName = itemFixed.Description
                Next
            End If
            query.OriginalValue = query.Clone()
        Else
            query = New MaintenanceProtocol()
        End If
        Return query
    End Function

    Public Function GetMaintenanceProtocolById(id As Integer) As MaintenanceProtocol Implements IMaintenanceProtocolRepository.GetMaintenanceProtocolById
        Return (From p In _context.MaintenanceProtocol Where p.Id = id Select p).FirstOrDefault()
    End Function
End Class
