Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class WorkOrderRepository
    Inherits GenericRepository(Of WorkOrder)
    Implements IWorkOrderRepository

#Region "Builder"

    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

#End Region

#Region "Methods"

    Public Function GetWorkOrderById(id As Integer) As WorkOrder Implements IWorkOrderRepository.GetWorkOrderById
        Dim workOrder = (From q In _context.WorkOrder.
                             Include("WorkOrderNotification")
                         Where q.Id = id Select q).FirstOrDefault()

        Dim physicalAsset = (From fapa In Me._context.FixedAssetPhysicalAsset.AsNoTracking() Where fapa.Id = workOrder.PhysicalAssetId).FirstOrDefault()
        Dim responsible = (From far In Me._context.FixedAssetResponsible.AsNoTracking().Include("ThirdParty").AsNoTracking() Where far.Id = physicalAsset.ResponsibleId).FirstOrDefault()
        Dim phone = (From p In Me._context.Phone.AsNoTracking() Where p.IdPerson = responsible.ThirdParty.PersonId).FirstOrDefault()

        workOrder.Company = (From gls In Me._context.GeneralLedgerSettings.AsNoTracking().Include("ThirdParty").AsNoTracking() Select gls.ThirdParty.Name).FirstOrDefault()
        workOrder.BrachOfficeCodeName = (From bo In Me._context.BranchOffice.AsNoTracking() Where bo.Id = workOrder.BranchOfficeId Select bo.Name).FirstOrDefault()
        workOrder.PhysicalAssetDescription = (From fai In Me._context.FixedAssetItem.AsNoTracking() Where fai.Id = physicalAsset.ItemId Select fai.Description).FirstOrDefault()
        workOrder.Plate = physicalAsset.Plate
        workOrder.Model = physicalAsset.Model
        workOrder.Serie = physicalAsset.Serie
        workOrder.Location = (From fal In Me._context.FixedAssetLocation.AsNoTracking() Where fal.Id = physicalAsset.LocationId Select fal.Name).FirstOrDefault()
        workOrder.MaintenanceResponsibleCodeName = responsible.ThirdParty.Name
        workOrder.ResponsiblePhone = If(phone IsNot Nothing, phone.Phone1, "-")

        Return workOrder
    End Function

    Public Function GetWorkOrderByCode(code As String) As WorkOrder Implements IWorkOrderRepository.GetWorkOrderByCode
        Dim query = (From q In _context.WorkOrder.
                         Include("WorkOrderActivities").
                         Include("WorkOrderConsumables").
                         Include("WorkOrderTools").
                         Include("WorkOrderSupplies")
                     Where q.Consecutive.Equals(code) Select q).FirstOrDefault()
        If query IsNot Nothing Then
            Dim branchOffice = (From b In _context.BranchOffice.AsNoTracking() Where b.Id = query.BranchOfficeId Select b).FirstOrDefault()
            query.BrachOfficeCodeName = $"{branchOffice.Code} - {branchOffice.Name}"

            If query.ProtocolId IsNot Nothing Then
                Dim protocol = (From p In _context.MaintenanceProtocol.AsNoTracking() Where p.Id = query.ProtocolId Select p).FirstOrDefault()
                query.ProtocolCodeName = $"{protocol.Code} - {protocol.Name}"
            End If

            Dim physicalAsset = (From p In _context.FixedAssetPhysicalAsset.AsNoTracking().Include("FixedAssetItem").AsNoTracking() Where p.Id = query.PhysicalAssetId Select p).FirstOrDefault()
            query.PhysicalAssetDescription = $"{physicalAsset.Plate} - {physicalAsset.FixedAssetItem.Code} - {physicalAsset.FixedAssetItem.Description}"

            If query.MaintenanceResponsibleId IsNot Nothing Then
                Dim responsible = (From p In _context.MaintenanceResponsible.AsNoTracking().Include("ThirdParty").AsNoTracking() Where p.Id = query.MaintenanceResponsibleId Select p).FirstOrDefault()
                query.MaintenanceResponsibleCodeName = $"{responsible.ThirdParty.Nit} - {responsible.ThirdParty.Name}"
            End If
        End If
        Return query
    End Function

    Function SP_SaveWorkOrder(listWorkOrderXml As String, userCode As String) As SP_SaveWorkOrder_Result Implements IWorkOrderRepository.SP_SaveWorkOrder
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveWorkOrder(listWorkOrderXml, userCode).SingleOrDefault()
    End Function

#End Region

End Class
