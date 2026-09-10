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

Public Class RemissionDevolutionRepository
    Inherits GenericRepository(Of RemissionDevolution)
    Implements IRemissionDevolutionRepository


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
    Public Function ListRemissionDevolutionMassiveConfirm(listDocuments As List(Of String)) As List(Of RemissionDevolution) Implements IRemissionDevolutionRepository.ListRemissionDevolutionMassiveConfirm
        Return (From re In _context.RemissionDevolution.Include("RemissionDevolutionDetail") Where listDocuments.Contains(re.Code) Select re).ToList()
    End Function

    Public Function GetRemissionDevolutionByCode(code As String) As RemissionDevolution Implements IRemissionDevolutionRepository.GetRemissionDevolutionByCode
        Dim res = (From rd In _context.RemissionDevolution.Include("RemissionDevolutionDetail") Where rd.Code = code Select rd).FirstOrDefault()
        If res IsNot Nothing Then
            Dim Warehouse = (From w In Me._context.Warehouse.AsNoTracking() Where w.Id = res.WarehouseId Select w).FirstOrDefault
            res.CodeNameWarehouse = Warehouse.Code + " - " + Warehouse.Name
            res.Prefix = Warehouse.Prefix
            If res.RemissionEntranceId IsNot Nothing Then
                res.CodeRemissionEntrace = (From re In _context.RemissionEntrance.AsNoTracking() Where re.Id = res.RemissionEntranceId Select re).FirstOrDefault().Code
            End If
            If res.RemissionOutputId IsNot Nothing Then
                res.CodeRemissionOutput = (From ro In _context.RemissionOutput.AsNoTracking() Where ro.Id = res.RemissionOutputId Select ro).FirstOrDefault().Code
            End If
            If res.ConsignmentInventoryRemissionId IsNot Nothing Then
                res.CodeConsignmentInventoryRemission = (From cir In _context.ConsignmentInventoryRemission.AsNoTracking() Where cir.Id = res.ConsignmentInventoryRemissionId Select cir).FirstOrDefault().Code
            End If
            res.OriginalValue = (From rd In _context.RemissionDevolution.AsNoTracking() Where rd.Code = code Select rd).FirstOrDefault()
            Return res
        Else
            Return New RemissionDevolution
        End If
    End Function

    ''' <summary>
    ''' Genera el comprobante contable para la devolución de la remision
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_GenerateJournalVoucherByRemissionDevolution(Id As Integer, CodeUser As String) As SP_GenerateJournalVoucherByRemissionDevolution_Result Implements IRemissionDevolutionRepository.SP_GenerateJournalVoucherByRemissionDevolution
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateJournalVoucherByRemissionDevolution(Id, CodeUser).SingleOrDefault
    End Function

    Public Function CascadeRollback(code As String) As Integer Implements IRepositoryRollbackStrategy.CascadeRollback
        Dim sql = "
        DELETE FROM Inventory.RemissionDevolutionDetail
        WHERE EXISTS (
            SELECT 1
            FROM Inventory.RemissionDevolution rd
            WHERE rd.Id = RemissionDevolutionDetail.RemissionDevolutionId
            AND rd.Code = @Code
	        AND rd.Status = 1
        );
        
        DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @Code
        
        DELETE FROM Inventory.RemissionDevolution
        WHERE Code = @Code
        AND Status = 1;"

        Dim codeParamater = New SqlParameter("@Code", code)
        Dim rowsAffected As Integer = _context.ExecuteNonQuery(sql, codeParamater)

        Return rowsAffected
    End Function
End Class
