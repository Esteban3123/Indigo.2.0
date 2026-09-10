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

Public Class LoanMerchandiseRepository
    Inherits GenericRepository(Of LoanMerchandise)
    Implements ILoanMerchandiseRepository


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
    ''' obtiene una solicitud de prestamos
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLoanMerchandiseByCode(code As String) As LoanMerchandise Implements ILoanMerchandiseRepository.GetLoanMerchandiseByCode
        Dim res = (From re In _context.LoanMerchandise Where re.Code = code Select re).FirstOrDefault()
        If res IsNot Nothing Then
            res.CodeNamethird = (From re In _context.ThirdParty Where re.Id = res.ThirdPartyId Select re.Nit & " - " & re.Name).SingleOrDefault()
            Dim wareHouse = (From wh In _context.Warehouse.AsNoTracking() Where res.WarehouseId = wh.Id Select wh).FirstOrDefault()
            res.CodeNameWareHouse = wareHouse.Code + " - " + wareHouse.Name
            res.Prefix = wareHouse.Prefix
            res.OriginalValue = (From re In _context.LoanMerchandise.AsNoTracking() Where re.Code = code Select re).FirstOrDefault()
            Return res
        Else
            Return New LoanMerchandise
        End If
    End Function
    ''' <summary>
    ''' Obtiene la solicitud de prestamo por ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLoanMerchandiseById(id As Integer, Optional tracking As Boolean = True) As LoanMerchandise Implements ILoanMerchandiseRepository.GetLoanMerchandiseById
        Dim res As LoanMerchandise
        If tracking = True Then
            res = (From re In _context.LoanMerchandise Where re.Id = id Select re).FirstOrDefault()
            If res IsNot Nothing Then
                res.OriginalValue = (From re In _context.LoanMerchandise.AsNoTracking() Where re.Id = id Select re).FirstOrDefault()
            End If
        Else
            res = (From re In _context.LoanMerchandise.AsNoTracking() Where re.Id = id Select re).FirstOrDefault()
        End If
        If res IsNot Nothing Then
            Return res
        Else
            Return New LoanMerchandise
        End If
    End Function

    Public Function CascadeRollback(code As String) As Integer Implements IRepositoryRollbackStrategy.CascadeRollback
        Dim sql = "
        DELETE FROM Inventory.LoanMerchandiseDetailBatchSerial
            WHERE EXISTS (
                SELECT 1
                FROM Inventory.LoanMerchandiseDetail lmd
                JOIN Inventory.LoanMerchandise lm ON lmd.LoanMerchandiseId = lm.Id
                WHERE LoanMerchandiseDetailBatchSerial.LoanMerchandiseDetailId = lmd.Id
                    AND lm.Code = @Code
                    AND lm.Status = 1
            );

        DELETE FROM Inventory.LoanMerchandiseDetail
            WHERE EXISTS (
                SELECT 1
                FROM Inventory.LoanMerchandise lm
                WHERE LoanMerchandiseDetail.LoanMerchandiseId = lm.Id
                    AND lm.Code = @Code
                    AND lm.Status = 1
            );

        DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @Code

        DELETE FROM Inventory.LoanMerchandise
            WHERE Code = @Code AND Status = 1;"

        Dim codeParamater = New SqlParameter("@Code", code)
        Dim rowsAffected As Integer = _context.ExecuteNonQuery(sql, codeParamater)

        Return rowsAffected
    End Function
End Class
