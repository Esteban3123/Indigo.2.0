Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient

Public Class TransferOrderDevolutionRepository
    Inherits GenericRepository(Of TransferOrderDevolution)
    Implements ITransferOrderDevolutionRepository

#Region "Builder"

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub

#End Region

#Region "Methods"

    Public Function GetTransferOrderDevolutionById(id As Integer) As TransferOrderDevolution Implements ITransferOrderDevolutionRepository.GetTransferOrderDevolutionById
        Dim res = (From tod In _context.TransferOrderDevolution.Include("TransferOrderDevolutionDetail") Where tod.Id = id Select tod).FirstOrDefault()
        If res IsNot Nothing Then
            Dim transferOrder = (From tro In _context.TransferOrder.AsNoTracking() Where tro.Id = res.TransferOrderId Select tro).FirstOrDefault()
            res.CodeTransferOrder = transferOrder.Code
            Dim Warehouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = transferOrder.SourceWarehouseId Select w).FirstOrDefault()
            res.Prefix = Warehouse.Prefix
            res.OriginalValue = (From tod In _context.TransferOrderDevolution.AsNoTracking() Where tod.Id = id Select tod).FirstOrDefault()
            Return res
        Else
            Return New TransferOrderDevolution
        End If
    End Function

    Public Function GetTransferOrderDevolutionByCode(code As String) As TransferOrderDevolution Implements ITransferOrderDevolutionRepository.GetTransferOrderDevolutionByCode
        Dim res = (From tod In _context.TransferOrderDevolution.Include("TransferOrderDevolutionDetail") Where tod.Code = code Select tod).FirstOrDefault()
        If res IsNot Nothing Then
            Dim transferOrder = (From tro In _context.TransferOrder.AsNoTracking() Where tro.Id = res.TransferOrderId Select tro).FirstOrDefault()
            res.CodeTransferOrder = transferOrder.Code
            Dim Warehouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = transferOrder.SourceWarehouseId Select w).FirstOrDefault()
            res.Prefix = Warehouse.Prefix
            res.OriginalValue = (From tod In _context.TransferOrderDevolution.AsNoTracking() Where tod.Code = code Select tod).FirstOrDefault()
            Return res
        Else
            Return New TransferOrderDevolution
        End If
    End Function

    Public Function SP_SaveTransferOrderDevolution(transferOrderDevolutionXml As String, userCode As String) As SP_SaveTransferOrderDevolution_Result Implements ITransferOrderDevolutionRepository.SP_SaveTransferOrderDevolution
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveTransferOrderDevolution(transferOrderDevolutionXml, userCode).SingleOrDefault()
    End Function

    Public Function CascadeRollback(code As String) As Integer Implements IRepositoryRollbackStrategy.CascadeRollback
        Dim sql =
            "DELETE FROM Inventory.TransferOrderDevolutionDetail
                WHERE EXISTS (
                    SELECT 1
                    FROM Inventory.TransferOrderDevolution tod
                    WHERE tod.Code = @Code
                        AND tod.Status = 1
                        AND tod.Id = TransferOrderDevolutionDetail.TransferOrderDevolutionId
                );

            DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @Code
            
            DELETE FROM Inventory.TransferOrderDevolution
                WHERE Code = @Code
                AND Status = 1
            ;"
        Dim codeParamater = New SqlParameter("@Code", code)
        Dim rowsAffected As Integer = _context.ExecuteNonQuery(sql, codeParamater)

        Return rowsAffected
    End Function

#End Region

End Class
