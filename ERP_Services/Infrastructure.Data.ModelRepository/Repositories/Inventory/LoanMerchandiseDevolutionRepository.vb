'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 24-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.SqlClient

Public Class LoanMerchandiseDevolutionRepository
    Inherits GenericRepository(Of LoanMerchandiseDevolution)
    Implements ILoanMerchandiseDevolutionRepository

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
    Public Function ListLoanMerchandiseDevolutionMassiveConfirm(listDocuments As List(Of String)) As List(Of LoanMerchandiseDevolution) Implements ILoanMerchandiseDevolutionRepository.ListLoanMerchandiseDevolutionMassiveConfirm
        Dim res = (From re In _context.LoanMerchandiseDevolution.Include("LoanMerchandiseDevolutionDetail").Include("LoanMerchandiseDevolutionDetail.LoanMerchandiseDevolutionDetailBatchSerial") Where listDocuments.Contains(re.Code) Select re).ToList()
        For Each item In res
            For Each itemDetail In item.LoanMerchandiseDevolutionDetail
                Dim LoanMerchandiseDetail = (From p In _context.LoanMerchandiseDetail.AsNoTracking() Where p.Id = itemDetail.LoanMerchandiseDetaillId Select p).SingleOrDefault()
                Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = LoanMerchandiseDetail.ProductId Select p).FirstOrDefault()
                itemDetail.ProductId = product.Id
                itemDetail.UnitValue = product.FinalProductCost
                itemDetail.BalanceQuantity = LoanMerchandiseDetail.Quantity
                itemDetail.ProductId = product.Id
                If LoanMerchandiseDetail.OutstandingQuantity > itemDetail.Quantity Then
                    itemDetail.OutstandingQuantity = LoanMerchandiseDetail.OutstandingQuantity - itemDetail.Quantity
                Else
                    itemDetail.OutstandingQuantity = LoanMerchandiseDetail.OutstandingQuantity
                End If
            Next
        Next
        Return res
    End Function

    ''' <summary>
    ''' obtiene una solicitud de prestamos
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLoanMerchandiseDevolutionByCode(code As String) As LoanMerchandiseDevolution Implements ILoanMerchandiseDevolutionRepository.GetLoanMerchandiseDevolutionByCode
        Dim res = (From re In _context.LoanMerchandiseDevolution Where re.Code = code Select re).FirstOrDefault()
        If res IsNot Nothing Then
            Dim wareHouse = (From wh In _context.Warehouse.AsNoTracking() Where res.WarehouseId = wh.Id Select wh).FirstOrDefault()
            res.CodeNameWareHouse = wareHouse.Code + " - " + wareHouse.Name
            res.Prefix = wareHouse.Prefix
            res.CodeLeanThirdParty = (From re In _context.LoanMerchandise.Include("ThirdParty") Where re.Id = res.LoanMerchandiseId Select String.Concat(re.Code, " - " & re.ThirdParty.Nit, " - " & re.ThirdParty.Name)).SingleOrDefault()
            res.OriginalValue = (From re In _context.LoanMerchandiseDevolution.AsNoTracking() Where re.Code = code Select re).FirstOrDefault()
            Return res
        Else
            Return New LoanMerchandiseDevolution
        End If
    End Function
    ''' <summary>
    ''' Obtiene la solicitud de prestamo por ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLoanMerchandiseDevolutionById(id As Integer) As LoanMerchandiseDevolution Implements ILoanMerchandiseDevolutionRepository.GetLoanMerchandiseDevolutionById
        Dim res = (From re In _context.LoanMerchandiseDevolution Where re.Id = id Select re).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From re In _context.LoanMerchandiseDevolution.AsNoTracking() Where re.Id = id Select re).FirstOrDefault()
            Return res
        Else
            Return New LoanMerchandiseDevolution
        End If
    End Function

    Public Function CascadeRollback(code As String) As Integer Implements IRepositoryRollbackStrategy.CascadeRollback
        Dim sql =
            "DELETE FROM Inventory.LoanMerchandiseDevolutionDetailBatchSerial
                WHERE EXISTS (
                    SELECT 1
                    FROM Inventory.LoanMerchandiseDevolutionDetail lmdd
                    JOIN Inventory.LoanMerchandiseDevolution lmd ON lmdd.LoanMerchandiseDevolutionId = lmd.Id
                    WHERE LoanMerchandiseDevolutionDetailBatchSerial.LoanMerchandiseDevolutionDetailId = lmdd.Id
                      AND lmd.Code = @Code
                      AND lmd.Status = 1
                );

            DELETE FROM Inventory.LoanMerchandiseDevolutionDetail
                WHERE EXISTS (
                    SELECT 1
                    FROM Inventory.LoanMerchandiseDevolution lmd
                    WHERE LoanMerchandiseDevolutionDetail.LoanMerchandiseDevolutionId = lmd.Id
                      AND lmd.Code = @Code
                      AND lmd.Status = 1
                );

            DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @Code

            DELETE FROM Inventory.LoanMerchandiseDevolution
                WHERE Code = @Code AND Status = 1;"

        Dim codeParamater = New SqlParameter("@Code", code)
        Dim rowsAffected As Integer = _context.ExecuteNonQuery(sql, codeParamater)

        Return rowsAffected
    End Function
End Class
