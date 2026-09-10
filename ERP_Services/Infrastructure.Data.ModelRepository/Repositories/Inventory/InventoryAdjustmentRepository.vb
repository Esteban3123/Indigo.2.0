'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Henry Alejandro Vargas Polania
' Created          : 14-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.SqlClient

Public Class InventoryAdjustmentRepository
    Inherits GenericRepository(Of InventoryAdjustment)
    Implements IInventoryAdjustmentRepository

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub

    ''' <summary>
    ''' lista todos los documnetos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInventoryAdjustmentMassiveConfirm(listDocuments As List(Of String)) As List(Of InventoryAdjustment) Implements IInventoryAdjustmentRepository.ListInventoryAdjustmentMassiveConfirm
        Return (From re In _context.InventoryAdjustment.Include("InventoryAdjustmentControl").Include("InventoryAdjustmentDetail").Include("InventoryAdjustmentDetail.InventoryAdjustmentDetailBatchSerial") Where listDocuments.Contains(re.Code) Select re).ToList()
    End Function

    ''' <summary>
    ''' optiene un ibventory adjustment por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryAdjustment(code As String) As InventoryAdjustment Implements IInventoryAdjustmentRepository.GetInventoryAdjustment
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d In Me._context.InventoryAdjustment.Include("InventoryAdjustmentDetail").Include("InventoryAdjustmentControl") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault

        If res IsNot Nothing Then
            Dim tp = (From t In Me._context.ThirdParty.AsNoTracking() Where res.ThirdPartyId = t.Id Select t).FirstOrDefault()
            res.NameThirdParty = ""
            If tp IsNot Nothing Then
                res.NameThirdParty = tp.Nit + " - " + tp.Name
            End If

            If res.AdjustmentType = 1 OrElse res.AdjustmentType = 2 OrElse res.AdjustmentType = 4 Then
                If res.InventoryAdjustmentDetail IsNot Nothing AndAlso res.InventoryAdjustmentDetail.Count > 0 Then

                    'Dictionaries
                    Dim dictionaryAdjustmentConcepts As New Dictionary(Of Integer, AdjustmentConcept)()
                    Dim dictionaryCostCenters As New Dictionary(Of Integer, CostCenter)()

                    'Items Individuals
                    Dim adjustmentConcept As AdjustmentConcept = Nothing
                    Dim CostCenter As CostCenter = Nothing

                    For Each item As InventoryAdjustmentDetail In res.InventoryAdjustmentDetail
                        Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where item.ProductId = p.Id Select p).FirstOrDefault()
                        item.ProductCodeName = product.Code + " - " + product.Name
                        Dim packingUnit = (From pu In _context.PackagingUnit.AsNoTracking() Where pu.Id = product.PackagingUnitId Select pu).FirstOrDefault()
                        item.consumptionUnit = packingUnit.Code + " - " + packingUnit.Name
                        Dim batch = (From b In _context.InventoryAdjustmentDetailBatchSerial Where item.Id = b.InventoryAdjustmentDetailId Select b).ToList()
                        If batch IsNot Nothing Then
                            For Each bt In batch
                                item.InventoryAdjustmentDetailBatchSerial.Add(bt)
                            Next
                        End If

                        'Concepto de ajuste de inventario
                        If item.AdjustmentConceptId IsNot Nothing Then
                            If Not dictionaryAdjustmentConcepts.ContainsKey(item.AdjustmentConceptId) Then
                                adjustmentConcept = (From a In _context.AdjustmentConcept.AsNoTracking Where item.AdjustmentConceptId = a.Id Select a).FirstOrDefault
                                dictionaryAdjustmentConcepts.Add(item.AdjustmentConceptId, adjustmentConcept)
                            Else
                                adjustmentConcept = dictionaryAdjustmentConcepts(item.AdjustmentConceptId)
                            End If
                            item.CodeNameAdjustmentConcept = adjustmentConcept.Code + " - " + adjustmentConcept.Name

                            'Centro de costos de los ajustes de inventario
                            If adjustmentConcept.CostCenterId IsNot Nothing Then
                                If Not dictionaryCostCenters.ContainsKey(adjustmentConcept.CostCenterId) Then
                                    CostCenter = (From cc In _context.CostCenter.AsNoTracking Where adjustmentConcept.CostCenterId = cc.Id Select cc).FirstOrDefault
                                    dictionaryCostCenters.Add(adjustmentConcept.CostCenterId, CostCenter)
                                Else
                                    CostCenter = dictionaryCostCenters(adjustmentConcept.CostCenterId)
                                End If
                                item.CodeNameCostCenter = CostCenter.Code + " - " + CostCenter.Name
                            End If
                        End If

                    Next
                End If
                Dim wh = (From w In Me._context.Warehouse.AsNoTracking() Where res.WarehouseId = w.Id Select w).FirstOrDefault()
                res.DescriptionWarehouse = wh.Code + " - " + wh.Name
                res.Prefix = wh.Prefix
                If res.AdjustmentType <> 4 AndAlso res.AdjustmentConceptId IsNot Nothing Then
                    Dim ac = (From a In Me._context.AdjustmentConcept.AsNoTracking() Where res.AdjustmentConceptId = a.Id Select a).FirstOrDefault()
                    res.CodeNameAdjustmentConcept = ac.Code + " - " + ac.Name
                End If

            Else
                Dim ic = (From incon In _context.InventoryControl.AsNoTracking() Where incon.Id = res.InventoryControlId Select incon)?.FirstOrDefault()
                res.CodeNameInventoryControl = ic?.Code
                Dim wh = (From w In Me._context.Warehouse.AsNoTracking() Where ic.WarehouseId = w.Id Select w)?.FirstOrDefault()
                res.DescriptionWarehouse = wh?.Code + " - " + wh?.Name
                res.DocumentDateInventoryControl = If(ic?.DocumentDate, DateTime.Now)
            End If

            res.OriginalValue = res
            Return res
        Else
            Return New InventoryAdjustment
        End If
    End Function

    ''' <summary>
    ''' Obtiene un inventory adjustment por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryAdjustmentById(id As Integer) As InventoryAdjustment Implements IInventoryAdjustmentRepository.GetInventoryAdjustmentById
        Dim res = (From d In Me._context.InventoryAdjustment.Include("InventoryControl").Include("InventoryAdjustmentDetail.InventoryAdjustmentDetailBatchSerial").Include("InventoryAdjustmentControl") Where d.Id.Equals(id) Select d).FirstOrDefault
        Dim wh As Warehouse = Nothing
        Dim ac As AdjustmentConcept = Nothing
        Dim tp As ThirdParty = Nothing

        If res IsNot Nothing Then
            If res.AdjustmentType = 3 Then 'Contol de inventario
                wh = (From w In Me._context.Warehouse.AsNoTracking() Where res.InventoryControl.WarehouseId = w.Id Select w).FirstOrDefault()
                res.DescriptionWarehouse = wh?.Code + " " + wh?.Name

            ElseIf res.InventoryAdjustmentDetail IsNot Nothing AndAlso res.InventoryAdjustmentDetail.Count > 0 Then

                'Dictionaries
                Dim dictionaryAdjustmentConcepts As New Dictionary(Of Integer, AdjustmentConcept)()
                Dim dictionaryCostCenters As New Dictionary(Of Integer, CostCenter)()

                'Items Individuals
                Dim adjustmentConcept As AdjustmentConcept = Nothing
                Dim CostCenter As CostCenter = Nothing

                For Each item As InventoryAdjustmentDetail In res.InventoryAdjustmentDetail
                    Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where item.ProductId = p.Id Select p).FirstOrDefault()
                    item.ProductCodeName = product?.Code + " - " + product?.Name

                    'Concepto de ajuste de inventario
                    If item.AdjustmentConceptId IsNot Nothing Then
                        If Not dictionaryAdjustmentConcepts.ContainsKey(item.AdjustmentConceptId) Then
                            adjustmentConcept = (From a In _context.AdjustmentConcept.AsNoTracking Where item.AdjustmentConceptId = a.Id Select a).FirstOrDefault
                            dictionaryAdjustmentConcepts.Add(item.AdjustmentConceptId, adjustmentConcept)
                        Else
                            adjustmentConcept = dictionaryAdjustmentConcepts(item.AdjustmentConceptId)
                        End If
                        item.CodeNameAdjustmentConcept = adjustmentConcept.Code + " - " + adjustmentConcept.Name

                        'Centro de costos de los ajustes de inventario
                        If adjustmentConcept.CostCenterId IsNot Nothing Then
                            If Not dictionaryCostCenters.ContainsKey(adjustmentConcept.CostCenterId) Then
                                CostCenter = (From cc In _context.CostCenter.AsNoTracking Where adjustmentConcept.CostCenterId = cc.Id Select cc).FirstOrDefault
                                dictionaryCostCenters.Add(adjustmentConcept.CostCenterId, CostCenter)
                            Else
                                CostCenter = dictionaryCostCenters(adjustmentConcept.CostCenterId)
                            End If
                            item.CodeNameCostCenter = CostCenter.Code + " - " + CostCenter.Name
                        End If
                    End If
                Next
                wh = (From w In Me._context.Warehouse.AsNoTracking() Where res.WarehouseId = w.Id Select w).FirstOrDefault()
                res.DescriptionWarehouse = wh?.Code + " - " + wh?.Name

                If res.AdjustmentConceptId IsNot Nothing Then
                    ac = (From a In Me._context.AdjustmentConcept.AsNoTracking() Where res.AdjustmentConceptId = a.Id Select a).FirstOrDefault()
                    res.CodeNameAdjustmentConcept = ac?.Code + " - " + ac?.Name
                End If

            End If
            tp = (From t In Me._context.ThirdParty.AsNoTracking() Where res.ThirdPartyId = t.Id Select t).FirstOrDefault()
            res.NameThirdParty = tp?.Nit + " - " + tp?.Name

            res.OriginalValue = res
            Return res
        Else
            Return New InventoryAdjustment
        End If
    End Function

    ''' <summary>
    ''' Obtiene un inventory adjustment por id con o sin tracking
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryAdjustmentByIdOptionalTracking(id As Integer, Optional tracking As Boolean = True) As InventoryAdjustment Implements IInventoryAdjustmentRepository.GetInventoryAdjustmentByIdOptionalTracking
        Dim res As InventoryAdjustment
        If tracking = True Then
            res = (From re In _context.InventoryAdjustment Where re.Id = id Select re).FirstOrDefault()
            If res IsNot Nothing Then
                res.OriginalValue = (From re In _context.InventoryAdjustment.AsNoTracking() Where re.Id = id Select re).FirstOrDefault()
            End If
        Else
            res = (From re In _context.InventoryAdjustment.AsNoTracking() Where re.Id = id Select re).FirstOrDefault()
        End If
        If res IsNot Nothing Then
            Return res
        Else
            Return New InventoryAdjustment
        End If
    End Function

    Public Function CascadeRollback(code As String) As Integer Implements IRepositoryRollbackStrategy.CascadeRollback
        Dim sql = "
        DELETE FROM Inventory.InventoryAdjustmentDetailBatchSerial
        WHERE EXISTS (
            SELECT 1 
            FROM Inventory.InventoryAdjustment ia
            JOIN Inventory.InventoryAdjustmentDetail iad 
                ON iad.InventoryAdjustmentId = ia.Id
            WHERE InventoryAdjustmentDetailBatchSerial.InventoryAdjustmentDetailId = iad.Id
            AND ia.Code = @Code
	        AND Status = 1
        );

        DELETE FROM Inventory.InventoryAdjustmentDetail
        WHERE EXISTS (
            SELECT 1 
            FROM Inventory.InventoryAdjustment ia
            WHERE InventoryAdjustmentDetail.InventoryAdjustmentId = ia.Id
            AND ia.Code = @Code
	        AND Status = 1
        );

        DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @Code

        DELETE FROM Inventory.InventoryAdjustment
            WHERE Code = @Code
            AND Status = 1;"


        Dim codeParamater = New SqlParameter("@Code", code)
        Dim rowsAffected As Integer = _context.ExecuteNonQuery(sql, codeParamater)

        Return rowsAffected
    End Function
End Class
