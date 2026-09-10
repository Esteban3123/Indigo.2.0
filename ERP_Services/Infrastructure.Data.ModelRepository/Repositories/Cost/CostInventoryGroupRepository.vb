#Region "Imports"

Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class CostInventoryGroupRepository
    Inherits GenericRepository(Of CostInventoryGroup)
    Implements ICostInventoryGroupRepository

#Region "Builder"

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetCostInventoryGroupById(id As Integer) As CostInventoryGroup Implements ICostInventoryGroupRepository.GetCostInventoryGroupById
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Id")
        End If

        Dim entity = (From i In _context.CostInventoryGroup Where i.Id = Id Select i).FirstOrDefault()
        If entity IsNot Nothing Then
            Return entity
        Else
            Return New CostInventoryGroup()
        End If
    End Function

    Public Function GetCostInventoryGroupByCode(code As String) As CostInventoryGroup Implements ICostInventoryGroupRepository.GetCostInventoryGroupByCode
        Dim entity = (From i In _context.CostInventoryGroup.AsNoTracking().
                        Include("CostInventoryGroupDetail").AsNoTracking()
                      Where i.Code.Equals(code) Select i).FirstOrDefault()

        If entity IsNot Nothing Then
            entity.MeasurementUnitCodeName = _context.InventoryMeasurementUnit.AsNoTracking().Where(Function(d) d.Id = entity.InventoryMeasurementUnitId).Select(Function(d) d.Code + " - " + d.Name).FirstOrDefault()

            For Each item In entity.CostInventoryGroupDetail
                Dim inventoryProduct = _context.InventoryProduct.AsNoTracking().Where(Function(d) d.Id = item.InventoryProductId).FirstOrDefault()
                item.InventoryProductCodeName = String.Format("{0} - {1}", inventoryProduct.Code, inventoryProduct.Name)
                If inventoryProduct.MeasurementUnitId IsNot Nothing Then
                    item.MeasurementUnitCodeName = _context.InventoryMeasurementUnit.AsNoTracking().Where(Function(d) d.Id = inventoryProduct.MeasurementUnitId).Select(Function(d) d.Code + " - " + d.Name).FirstOrDefault()
                End If                
            Next

            Return entity
        Else
            Return New CostInventoryGroup()
        End If
    End Function

    Public Function SP_SaveCostInventoryGroup(CostInventoryGroupXml As String, listCostInventoryGroupDetailXml As String, codeUser As String) As List(Of SP_SaveCostInventoryGroup_Result) Implements ICostInventoryGroupRepository.SP_SaveCostInventoryGroup
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveCostInventoryGroup(CostInventoryGroupXml, listCostInventoryGroupDetailXml, codeUser).ToList()
    End Function

    Public Function ValidateBeforeActive(CostInventoryGroupId As Integer) As String Implements ICostInventoryGroupRepository.ValidateBeforeActive
        Try
            Dim errors As New Text.StringBuilder

            Dim inventoryProductDuplicates = (From cig In _context.CostInventoryGroup.AsNoTracking
                                             Join cigd In _context.CostInventoryGroupDetail.AsNoTracking On cig.Id Equals cigd.CostInventoryGroupId
                                             Join cigdduplicate In _context.CostInventoryGroupDetail.AsNoTracking On cigd.InventoryProductId Equals cigdduplicate.InventoryProductId
                                             Join ip In _context.InventoryProduct.AsNoTracking On cigd.InventoryProductId Equals ip.Id
                                             Join cigduplicate In _context.CostInventoryGroup.AsNoTracking On cigdduplicate.CostInventoryGroupId Equals cigduplicate.Id
                                             Where cig.Id <> cigduplicate.Id AndAlso cigduplicate.Status
                                             Select New With { .InventoryProductCode = ip.Code, .InventoryProductName = ip.Name, .CostInventoryGroupCode =  cigduplicate.Code, .CostInventoryGroupName =  cigduplicate.Name }).ToList()

            If inventoryProductDuplicates IsNot Nothing Then
                For Each item In inventoryProductDuplicates
                    errors.AppendLine(String.Format("{0} - {1} (Grupo: {2} - {3})", item.InventoryProductCode, item.InventoryProductName, item.CostInventoryGroupCode, item.CostInventoryGroupName))
                Next
            End If

            Return errors.ToString()
        Catch ex As Exception
            Return ex.ToString()
        End Try
    End Function

#Region "Copy & Paste"

    Function SP_CopyAndPasteCostInventoryGroupDetail(XmlObject As String) As List(Of SP_CopyAndPasteCostInventoryGroupDetail_Result) Implements ICostInventoryGroupRepository.SP_CopyAndPasteCostInventoryGroupDetail
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteCostInventoryGroupDetail(XmlObject).ToList
    End Function

#End Region

#End Region

End Class
