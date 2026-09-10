#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class CostActivityRepository
    Inherits GenericRepository(Of CostActivity)
    Implements ICostActivityRepository

#Region "Builder"

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetCostActivityById(id As Integer) As CostActivity Implements ICostActivityRepository.GetCostActivityById
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Id")
        End If

        Dim entity = (From i In _context.CostActivity Where i.Id = Id Select i).FirstOrDefault()
        If entity IsNot Nothing Then
            Return entity
        Else
            Return New CostActivity()
        End If
    End Function

    Public Function GetCostActivityByIdWithAggregates(id As Integer) As CostActivity Implements ICostActivityRepository.GetCostActivityByIdWithAggregates
        Dim entity = (From i In _context.CostActivity.AsNoTracking().
                        Include("CostActivityStep").AsNoTracking().
                        Include("CostActivityStep.CostActivityStepFixedAsset").AsNoTracking().
                        Include("CostActivityStep.CostActivityStepPayroll").AsNoTracking().
                        Include("CostActivityStep.CostActivityStepInventory").AsNoTracking().
                        Include("CostActivityStep.CostActivityStepAddictionalCost").AsNoTracking()
                      Where i.Id = id Select i).FirstOrDefault()

        If entity IsNot Nothing Then
            entity.CUPSEntityCodeName = _context.CUPSEntity.AsNoTracking().Where(Function(d) d.Id = entity.CUPSEntityId).Select(Function(d) d.Code + " - " + d.Description).FirstOrDefault()

            For Each item In entity.CostActivityStep
                item.OrderDescription = String.Concat(item.Order, " - ", item.Description)

                If item.CostActivityStepFixedAsset IsNot Nothing Then
                    For Each detail In item.CostActivityStepFixedAsset
                        detail.CostActivityStepOrderDescription = item.OrderDescription
                        detail.FixedAssetItemCodeName = _context.FixedAssetItem.AsNoTracking().Where(Function(d) d.Id = detail.FixedAssetItemId).Select(Function(d) d.Code + " - " + d.Description).FirstOrDefault()
                    Next
                End If

                If item.CostActivityStepPayroll IsNot Nothing Then
                    For Each detail In item.CostActivityStepPayroll
                        detail.CostActivityStepOrderDescription = item.OrderDescription
                        detail.PositionCodeName = _context.Position.AsNoTracking().Where(Function(d) d.Id = detail.PayrollPositionId).Select(Function(d) d.Code + " - " + d.Name).FirstOrDefault()
                    Next
                End If

                If item.CostActivityStepInventory IsNot Nothing Then
                    For Each detail In item.CostActivityStepInventory
                        detail.CostActivityStepOrderDescription = item.OrderDescription
                        Dim costInventoryGroup = _context.CostInventoryGroup.AsNoTracking().Where(Function(d) d.Id = detail.CostInventoryGroupId).FirstOrDefault()
                        detail.CostInventoryGroupCodeName = String.Format("{0} - {1}", costInventoryGroup.Code, costInventoryGroup.Description)
                        detail.MeasurementUnitCodeName = _context.InventoryMeasurementUnit.AsNoTracking().Where(Function(d) d.Id = costInventoryGroup.InventoryMeasurementUnitId).Select(Function(d) d.Code + " - " + d.Name).FirstOrDefault()
                    Next
                End If

                If item.CostActivityStepAddictionalCost IsNot Nothing Then
                    For Each detail In item.CostActivityStepAddictionalCost
                        detail.CostActivityStepOrderDescription = item.OrderDescription
                    Next
                End If
            Next

            Return entity
        Else
            Return New CostActivity()
        End If
    End Function

    Public Function GetCostActivityByCode(code As String) As CostActivity Implements ICostActivityRepository.GetCostActivityByCode
        Dim entity = (From i In _context.CostActivity.AsNoTracking().
                        Include("CostActivityProductionCenter").AsNoTracking().
                        Include("CostActivityStep").AsNoTracking().
                        Include("CostActivityStep.CostActivityStepFixedAsset").AsNoTracking().
                        Include("CostActivityStep.CostActivityStepPayroll").AsNoTracking().
                        Include("CostActivityStep.CostActivityStepInventory").AsNoTracking().
                        Include("CostActivityStep.CostActivityStepAddictionalCost").AsNoTracking()
                      Where i.Code.Equals(code) Select i).FirstOrDefault()

        If entity IsNot Nothing Then
            entity.CUPSEntityCodeName = _context.CUPSEntity.AsNoTracking().Where(Function(d) d.Id = entity.CUPSEntityId).Select(Function(d) d.Code + " - " + d.Description).FirstOrDefault()

            For Each item In entity.CostActivityProductionCenter
                item.CostProductionCenterCodeName = _context.CostProductionCenter.AsNoTracking().Where(Function(d) d.Id = item.CostProductionCenterId).Select(Function(d) d.Code + " - " + d.Name).FirstOrDefault()
            Next

            For Each item In entity.CostActivityStep
                item.OrderDescription = String.Concat(item.Order, " - ", item.Description)

                If item.CostActivityStepFixedAsset IsNot Nothing Then
                    For Each detail In item.CostActivityStepFixedAsset
                        detail.CostActivityStepOrderDescription = item.OrderDescription
                        detail.FixedAssetItemCodeName = _context.FixedAssetItem.AsNoTracking().Where(Function(d) d.Id = detail.FixedAssetItemId).Select(Function(d) d.Code + " - " + d.Description).FirstOrDefault()
                    Next
                End If

                If item.CostActivityStepPayroll IsNot Nothing Then
                    For Each detail In item.CostActivityStepPayroll
                        detail.CostActivityStepOrderDescription = item.OrderDescription
                        detail.PositionCodeName = _context.Position.AsNoTracking().Where(Function(d) d.Id = detail.PayrollPositionId).Select(Function(d) d.Code + " - " + d.Name).FirstOrDefault()
                    Next
                End If

                If item.CostActivityStepInventory IsNot Nothing Then
                    For Each detail In item.CostActivityStepInventory
                        detail.CostActivityStepOrderDescription = item.OrderDescription
                        Dim costInventoryGroup = _context.CostInventoryGroup.AsNoTracking().Where(Function(d) d.Id = detail.CostInventoryGroupId).FirstOrDefault()
                        detail.CostInventoryGroupCodeName = String.Format("{0} - {1}", costInventoryGroup.Code, costInventoryGroup.Description)
                        detail.MeasurementUnitCodeName = _context.InventoryMeasurementUnit.AsNoTracking().Where(Function(d) d.Id = costInventoryGroup.InventoryMeasurementUnitId).Select(Function(d) d.Code + " - " + d.Name).FirstOrDefault()                        
                    Next
                End If

                If item.CostActivityStepAddictionalCost IsNot Nothing Then
                    For Each detail In item.CostActivityStepAddictionalCost
                        detail.CostActivityStepOrderDescription = item.OrderDescription
                    Next
                End If
            Next

            Return entity
        Else
            Return New CostActivity()
        End If
    End Function

    Public Function SP_SaveCostActivity(costActivityXml As String, listCostActivityProductionCenterXml As String, listCostActivityStepXml As String, listCostActivityStepFixedAssetXml As String, listCostActivityStepPayrollXml As String, listCostActivityStepInventoryXml As String, listCostActivityStepAddictionalCostXml As String, codeUser As String) As List(Of SP_SaveCostActivity_Result) Implements ICostActivityRepository.SP_SaveCostActivity
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveCostActivity(costActivityXml, listCostActivityProductionCenterXml, listCostActivityStepXml, listCostActivityStepFixedAssetXml, listCostActivityStepPayrollXml, listCostActivityStepInventoryXml, listCostActivityStepAddictionalCostXml, codeUser).ToList()
    End Function

    Public Function ValidateBeforeActive(costActivityId As Integer, CUPSEntityId As Integer) As String Implements ICostActivityRepository.ValidateBeforeActive
        Try
            Dim errors As New Text.StringBuilder

            Dim productionCenterDuplicates = (From cao In _context.CostActivity
                                             Join capco In _context.CostActivityProductionCenter On capco.CostActivityId Equals cao.Id
                                             Join cpco In _context.CostProductionCenter On capco.CostProductionCenterId Equals cpco.Id
                                             Join capc In _context.CostActivityProductionCenter On capco.CostProductionCenterId Equals capc.CostProductionCenterId
                                             Where capc.CostActivityId = costActivityId AndAlso cao.Status AndAlso cao.CUPSEntityId = CUPSEntityId AndAlso cao.Id <> costActivityId
                                             Select New With { .ProductionCenterCode =  cpco.Code, .ProductionCenterName = cpco.Name, .CostActivityCode = cao.Code, .CostActivityName = cao.Name }).ToList()

            If productionCenterDuplicates IsNot Nothing Then
                For Each item In productionCenterDuplicates
                    errors.AppendLine(String.Format("{0} - {1} ({2} - {3})", item.ProductionCenterCode, item.ProductionCenterName, item.CostActivityCode, item.CostActivityName))
                Next
            End If

            Return errors.ToString()
        Catch ex As Exception
            Return ex.ToString()
        End Try
    End Function

#Region "Copy & Paste"

    Function SP_CostActivityCopyAndPasteProductionCenter(XmlObject As String) As List(Of SP_CostActivityCopyAndPasteProductionCenter_Result) Implements ICostActivityRepository.SP_CostActivityCopyAndPasteProductionCenter
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CostActivityCopyAndPasteProductionCenter(XmlObject).ToList
    End Function

    Function SP_CostActivityCopyAndPasteFixedAsset(XmlObject As String) As List(Of SP_CostActivityCopyAndPasteFixedAsset_Result) Implements ICostActivityRepository.SP_CostActivityCopyAndPasteFixedAsset
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CostActivityCopyAndPasteFixedAsset(XmlObject).ToList
    End Function

    Function SP_CostActivityCopyAndPastePayroll(XmlObject As String) As List(Of SP_CostActivityCopyAndPastePayroll_Result) Implements ICostActivityRepository.SP_CostActivityCopyAndPastePayroll
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CostActivityCopyAndPastePayroll(XmlObject).ToList
    End Function

    Function SP_CostActivityCopyAndPasteInventory(XmlObject As String) As List(Of SP_CostActivityCopyAndPasteInventory_Result) Implements ICostActivityRepository.SP_CostActivityCopyAndPasteInventory
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CostActivityCopyAndPasteInventory(XmlObject).ToList
    End Function

#End Region

#End Region

End Class
