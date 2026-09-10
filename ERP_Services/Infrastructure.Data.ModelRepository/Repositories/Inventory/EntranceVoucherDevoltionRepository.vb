'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Henry Alejandro Vargas Polania
' Created          : 09-03-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.SqlClient

Public Class EntranceVoucherDevoltionRepository
    Inherits GenericRepository(Of EntranceVoucherDevolution)
    Implements IEntranceVoucherDevolutionRepository

    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub
#End Region

    ''' <summary>
    ''' Método con el cual consulta la devolucion del comprobante de entrada por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEntranceVoucherDevolution(code As String) As EntranceVoucherDevolution Implements IEntranceVoucherDevolutionRepository.GetEntranceVoucherDevolution
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d In Me._context.EntranceVoucherDevolution.Include("EntranceVoucherDevolutionDetail").Include("EntranceVoucherDevolutionOtherDeduction").Include("EntranceVoucherDevolutionObligationBudget")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault

        If res IsNot Nothing Then
            res.CodeEntranceVoucher = (From ev In _context.EntranceVoucher.AsNoTracking() Where res.EntranceVoucherId = ev.Id Select ev).FirstOrDefault().Code
            res.WithholdingIcaPercentage = (From ev In _context.EntranceVoucher.AsNoTracking() Where res.EntranceVoucherId = ev.Id Select ev.IcaPercentage).FirstOrDefault()
            Dim wareHouse = (From wh In _context.Warehouse.AsNoTracking() Where res.WarehouseId = wh.Id Select wh).FirstOrDefault()
            res.DescriptionWarehouse = wareHouse.Code + " - " + wareHouse.Name
            res.Prefix = wareHouse.Prefix
            res.OriginalValue = (From g In _context.EntranceVoucherDevolution.AsNoTracking Where g.Code.Equals(code.Trim()) Select g).FirstOrDefault

            For Each deductionRetention In res.EntranceVoucherDevolutionOtherDeduction
                deductionRetention.OtherWithholdingDeductionId = (From evod In _context.EntranceVoucherOtherDeduction.AsNoTracking() Where evod.Id = deductionRetention.EntranceVoucherOtherDeductionId Select evod.OtherWithholdingDeductionId).FirstOrDefault()
            Next

            If res.EntranceVoucherDevolutionObligationBudget IsNot Nothing AndAlso res.EntranceVoucherDevolutionObligationBudget.Count > 0 Then
                For Each item In res.EntranceVoucherDevolutionObligationBudget
                    Dim obligationDetail = (From x In _context.ObligationDetail.AsNoTracking().Include("Obligation").AsNoTracking.Include("Category").AsNoTracking.Include("Category.FinancialSource").AsNoTracking.Include("RevenueType").AsNoTracking
                                            Where x.Id = item.ObligationDetailId
                                            Select x).FirstOrDefault()
                    item.ObligationCode = obligationDetail.Obligation.Code
                    item.ObligationDocument = obligationDetail.Obligation.Document
                    item.CategoryName = obligationDetail.Category.Code + " - " + obligationDetail.Category.Name
                    If obligationDetail.Category.FinancialSource IsNot Nothing Then
                        item.FinancialSourceDescription = obligationDetail.Category.FinancialSource.Code + " - " + obligationDetail.Category.FinancialSource.Name
                    End If
                    item.RevenueTypeDescription = obligationDetail.RevenueType.Code + " - " + obligationDetail.RevenueType.Name
                    item.ObligationBalance = obligationDetail.Balance
                    item.CommitmentDetailId = obligationDetail.CommitmentDetailId
                Next
            End If

            Return res
        Else
            Return New EntranceVoucherDevolution
        End If
    End Function

    ''' <summary>
    ''' Método con el cual consulta la devolucion del comprobante de entrada por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEntranceVoucherDevolutionById(id As Integer) As EntranceVoucherDevolution Implements IEntranceVoucherDevolutionRepository.GetEntranceVoucherDevolutionById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.EntranceVoucherDevolution.Include("EntranceVoucherDevolutionDetail").Include("EntranceVoucherDevolutionOtherDeduction") Where d.Id.Equals(id)
                   Select d).FirstOrDefault

        If res IsNot Nothing Then
            Return res
        Else
            Return New EntranceVoucherDevolution
        End If
    End Function

    ''' <summary>
    ''' Método con el cual consulta el producto que contiene el detalle de la devolucion
    ''' </summary>
    ''' <param name="EntranceVoucherDetailBatchSerialId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryProductByEntranceVoucherDetailBatchSerialId(EntranceVoucherDetailBatchSerialId As Integer) As InventoryProduct Implements IEntranceVoucherDevolutionRepository.GetInventoryProductByEntranceVoucherDetailBatchSerialId
        Dim res = (From evdbs In _context.EntranceVoucherDetailBatchSerial.AsNoTracking()
                   Join evd In _context.EntranceVoucherDetail.AsNoTracking() On evd.Id Equals evdbs.EntranceVoucherDetailId
                   Join p In _context.InventoryProduct.AsNoTracking() On p.Id Equals evd.ProductId
                   Where evdbs.Id = EntranceVoucherDetailBatchSerialId Select p).FirstOrDefault()
        If res IsNot Nothing Then
            If res.ProductGroupId > 0 Then
                Dim productGroup = (From pg In _context.ProductGroup.AsNoTracking() Where pg.Id = res.ProductGroupId Select pg).FirstOrDefault()
                res.ProductGroup = productGroup

                Dim accountWithholdingSource = (From aws In _context.AccountPayableConcepts.Include("MainAccounts") Where aws.Id = productGroup.DeclarantRetentionAccountPayableConceptId Select aws).FirstOrDefault()
                If accountWithholdingSource.RetentionConceptId IsNot Nothing Then
                    res.WithholdingSourcePercernt = (From rc In _context.RetentionConcepts Where accountWithholdingSource.RetentionConceptId = rc.Id Select rc.Rate).FirstOrDefault()
                End If

                res.HandlesCostCenterWithholdigSourceAccount = accountWithholdingSource.MainAccounts.HandlesCostCenter
                res.AccountWithholdingSourceId = accountWithholdingSource.IdAccount
                res.RetentionConceptsWithholdingSourceId = accountWithholdingSource.RetentionConceptId
                res.ConceptAccountPayableWithholdingSourceId = accountWithholdingSource.Id
                Dim accountInventory = (From aws In _context.AccountPayableConcepts.Include("MainAccounts") Where aws.Id = productGroup.InventoryAccountPayableConceptId Select aws).FirstOrDefault()
                res.HandlesThirdPartyAccount = accountInventory.MainAccounts.HandlesThirdParty
                res.AccountInventoryCodeName = accountInventory.MainAccounts.Number + " - " + accountInventory.MainAccounts.Name
                res.AccountInventoryId = accountInventory.IdAccount
                res.ConceptAccountPayableInventory = accountInventory.Id
            Else
                res.WithholdingSourcePercernt = 0
                res.AccountWithholdingSourceId = 0
                res.RetentionConceptsWithholdingSourceId = 0
                res.ConceptAccountPayableWithholdingSourceId = 0
                res.AccountInventoryId = 0
                res.ConceptAccountPayableInventory = 0
            End If

            Return res
        Else
            Return New InventoryProduct
        End If
    End Function

    ''' <summary>
    ''' Obtiene todas las devoluciones confirmadas de un comprobante de entrada
    ''' </summary>
    ''' <param name="EntranceVoucherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEntranceVoucherDevolutionsByEntranceVoucherId(EntranceVoucherId As Integer) As List(Of EntranceVoucherDevolution) Implements IEntranceVoucherDevolutionRepository.GetEntranceVoucherDevolutionsByEntranceVoucherId
        If EntranceVoucherId = 0 Then
            Throw New ArgumentNullException("id")
        End If

        Return (From evdev In _context.EntranceVoucherDevolution.AsNoTracking()
                Where evdev.EntranceVoucherId = EntranceVoucherId And evdev.Status = 2 Select evdev).ToList()
    End Function

    Public Function CascadeRollback(code As String) As Integer Implements IRepositoryRollbackStrategy.CascadeRollback
        Dim sql =
            "DELETE FROM Inventory.EntranceVoucherDevolutionDetail
                WHERE EXISTS (
                    SELECT 1
                    FROM Inventory.EntranceVoucherDevolution evd
                    WHERE EntranceVoucherDevolutionDetail.EntranceVoucherDevolutionId = evd.Id
                      AND evd.Code = @Code
                      AND evd.Status = 1
                );
               
            DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @Code

            DELETE FROM Inventory.EntranceVoucherDevolution
                WHERE Code = @Code AND Status = 1;"

        Dim codeParamater = New SqlParameter("@Code", code)
        Dim rowsAffected As Integer = _context.ExecuteNonQuery(sql, codeParamater)

        Return rowsAffected
    End Function
End Class
