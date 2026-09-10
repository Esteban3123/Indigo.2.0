'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 08-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient

Public Class RemissionEntranceRepository
    Inherits GenericRepository(Of RemissionEntrance)
    Implements IRemissionEntranceRepository

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


    ''' <summary>
    ''' lista todos los documnetos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRemissionEntranceMassiveConfirm(listDocuments As List(Of String)) As List(Of RemissionEntrance) Implements IRemissionEntranceRepository.ListRemissionEntranceMassiveConfirm
        Return (From re In _context.RemissionEntrance Where listDocuments.Contains(re.Code) Select re).ToList()
    End Function

    Public Function GetRemissionEntranceByCode(code As String) As RemissionEntrance Implements IRemissionEntranceRepository.GetRemissionEntranceByCode
        Dim res = (From re In _context.RemissionEntrance.Include("Currency") Where re.Code = code Select re).FirstOrDefault()
        If res IsNot Nothing Then
            Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where sdl.Id = res.SupplierDistributionLineId Select sdl).FirstOrDefault()
            Dim supplier = (From s In _context.Supplier.AsNoTracking() Where s.Id = res.SupplierId Select s).FirstOrDefault()
            res.CodeNameSupplier = supplier.Code + " - " + supplier.Name
            Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking() Where dl.Id = supplierDistributionLine.IdDistributionLine Select dl).FirstOrDefault()
            res.CodeNameDistributionLine = distributionLine.Code + " - " + distributionLine.Name
            Dim wareHouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = res.WarehouseId Select wh).FirstOrDefault()
            res.CodeNameWareHouse = wareHouse.Code + " - " + wareHouse.Name
            res.Prefix = wareHouse.Prefix
            res.OriginalValue = (From re In _context.RemissionEntrance.AsNoTracking() Where re.Code = code Select re).FirstOrDefault()
            Return res
        Else
            Return New RemissionEntrance
        End If
    End Function

    ''' <summary>
    ''' obtiene una remision por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetRemissionEntranceById(id As Integer) As RemissionEntrance Implements IRemissionEntranceRepository.GetRemissionEntranceById
        Dim res = (From re In _context.RemissionEntrance.AsNoTracking() Where re.Id = id Select re).FirstOrDefault()
        If res IsNot Nothing Then
            Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where sdl.Id = res.SupplierDistributionLineId Select sdl).FirstOrDefault()
            Dim supplier = (From s In _context.Supplier.AsNoTracking() Where s.Id = res.SupplierId Select s).FirstOrDefault()
            res.CodeNameSupplier = supplier.Code + " - " + supplier.Name
            Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking() Where dl.Id = supplierDistributionLine.IdDistributionLine Select dl).FirstOrDefault()
            res.CodeNameDistributionLine = distributionLine.Code + " - " + distributionLine.Name
            Dim wareHouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = res.WarehouseId Select wh).FirstOrDefault()
            res.CodeNameWareHouse = wareHouse.Code + " - " + wareHouse.Name
            Return res
        Else
            Return New RemissionEntrance
        End If
    End Function

    ''' <summary>
    ''' Genera el comprobante contable para la remision de entrada
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_GenerateJournalVoucherByRemissionEntrance(Id As Integer, CodeUser As String) As SP_GenerateJournalVoucherByRemissionEntrance_Result Implements IRemissionEntranceRepository.SP_GenerateJournalVoucherByRemissionEntrance
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateJournalVoucherByRemissionEntrance(Id, CodeUser).SingleOrDefault
    End Function

    Public Function CascadeRollback(code As String) As Integer Implements IRepositoryRollbackStrategy.CascadeRollback
        Dim sql =
            "DELETE FROM Inventory.RemissionEntranceDetailBatchSerial
                WHERE EXISTS (
                    SELECT 1
                    FROM Inventory.RemissionEntrance re
                    JOIN Inventory.RemissionEntranceDetail red 
                        ON red.RemissionEntranceId = re.Id
                    WHERE RemissionEntranceDetailBatchSerial.RemissionEntranceDetailId = red.Id
                      AND re.Code = @Code
                      AND re.Status = 1
                );

            DELETE FROM Inventory.RemissionEntranceDetail
                WHERE EXISTS (
                    SELECT 1
                    FROM Inventory.RemissionEntrance re
                    WHERE RemissionEntranceDetail.RemissionEntranceId = re.Id
                      AND re.Code = @Code
                      AND re.Status = 1
                );

            DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @Code

            DELETE FROM Inventory.RemissionEntrance
                WHERE Code = @Code AND Status = 1;"

        Dim codeParamater = New SqlParameter("@Code", code)
        Dim rowsAffected As Integer = _context.ExecuteNonQuery(sql, codeParamater)

        Return rowsAffected
    End Function
End Class
