'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Henry Alejandro Vargas Polania
' Created          : 14-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient

Public Class EntranceVoucherRepository
    Inherits GenericRepository(Of EntranceVoucher)
    Implements IEntranceVoucherRepository

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
    Public Function ListEntranceVoucherMassiveConfirm(listDocuments As List(Of String)) As List(Of EntranceVoucher) Implements IEntranceVoucherRepository.ListEntranceVoucherMassiveConfirm
        Return (From re In _context.EntranceVoucher.Include("EntranceVoucherDetail").Include("EntranceVoucherOtherDeduction").Include("EntranceVoucherDetail.EntranceVoucherDetailBatchSerial") Where listDocuments.Contains(re.Code) Select re).ToList()
    End Function

    Public Function GetEntranceVoucher(code As String) As EntranceVoucher Implements IEntranceVoucherRepository.GetEntranceVoucher
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d In Me._context.EntranceVoucher.Include("Currency").AsNoTracking()
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault

        If res IsNot Nothing Then
            res.OriginalValue = (From g In _context.EntranceVoucher.AsNoTracking Where g.Code.Equals(code.Trim()) Select g).FirstOrDefault
            Return res
        Else
            Return New EntranceVoucher
        End If
    End Function

    Public Function GetEntranceVoucherById(id As Integer) As EntranceVoucher Implements IEntranceVoucherRepository.GetEntranceVoucherById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.EntranceVoucher.
                       Include("EntranceVoucherDetail").
                       Include("EntranceVoucherDetail.EntranceVoucherDetailBatchSerial").
                       Include("EntranceVoucherOtherDeduction")
                   Where d.Id.Equals(id)
                   Select d).FirstOrDefault

        If res IsNot Nothing Then
            If res.EntranceVoucherDetail IsNot Nothing AndAlso res.EntranceVoucherDetail.Count > 0 Then
                For Each item As EntranceVoucherDetail In res.EntranceVoucherDetail
                    Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where item.ProductId = p.Id Select p).FirstOrDefault()
                    item.ProductCodeName = product.Code + " - " + product.Name
                    If product.ManufacturerId IsNot Nothing Then
                        item.ManufacturerName = (From p In _context.Manufacturer.AsNoTracking() Where p.Id = product.ManufacturerId Select p.Name).FirstOrDefault()
                    End If
                    item.HealthRegistration = product.HealthRegistration
                    item.Presentation = product.Presentation
                Next
            End If

            Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where res.SupplierDistributionLineId = sdl.Id Select sdl).FirstOrDefault
            Dim supplier = (From s In _context.Supplier.AsNoTracking Where res.SupplierId = s.Id Select s).FirstOrDefault
            Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking Where supplierDistributionLine.IdDistributionLine = dl.Id Select dl).FirstOrDefault
            res.DescriptionSupplier = supplier.Code + " - " + supplier.Name + " - " + distributionLine.Code + " - " + distributionLine.Name
            Dim wareHouse = (From wh In _context.Warehouse.AsNoTracking() Where res.WarehouseId = wh.Id Select wh).FirstOrDefault()
            res.DescriptionWarehouse = wareHouse.Code + " - " + wareHouse.Name
            Dim supplierType = (From st In _context.SupplierType.AsNoTracking() Where res.SupplierTypeId = st.Id Select st).FirstOrDefault()
            res.DescriptionSupplierType = supplierType.Code + " - " + supplierType.Name

            If res.DocumentSupportId IsNot Nothing Then
                'Codigo y nombre de la resolucion de Documento Soporte
                Dim DocumentSupport = (From ds In _context.DocumentSupport.AsNoTracking Where res.DocumentSupportId = ds.Id Select ds).FirstOrDefault
                If DocumentSupport IsNot Nothing Then
                    res.DescriptionDocumentSupport = $"{DocumentSupport.Code} - {DocumentSupport.Name}"
                Else
                    Dim BillingAuthorization = (From ds In _context.BillingAuthorization.AsNoTracking Where res.DocumentSupportId = ds.Id Select ds).FirstOrDefault
                    If BillingAuthorization IsNot Nothing Then
                        res.DescriptionDocumentSupport = $"{BillingAuthorization.Code} - {BillingAuthorization.Name}"
                    End If
                End If
            End If

            res.OriginalValue = (From g In _context.EntranceVoucher.AsNoTracking Where g.Id.Equals(id) Select g).FirstOrDefault
            Return res
        Else
            Return New EntranceVoucher
        End If
    End Function

    Public Function GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher(EntranceVoucherId As Integer) As List(Of EntranceVoucherDetailBatchSerial) Implements IEntranceVoucherRepository.GetDetailEntranceVoucherWithBatchSerialByIdEntranceVoucher
        If EntranceVoucherId = 0 Then
            Throw New ArgumentNullException("id")
        End If

        Dim res = (From evdb In _context.EntranceVoucherDetailBatchSerial.Include("EntranceVoucherDetail")
                   Join ev In _context.EntranceVoucher On evdb.EntranceVoucherDetail.EntranceVoucherId Equals ev.Id
                   Where ev.Id = EntranceVoucherId Select evdb).ToList()

        If res.Count > 0 Then
            For Each dt In res
                Dim evd = (From d In _context.EntranceVoucherDetail Where d.Id = dt.EntranceVoucherDetailId Select d).FirstOrDefault()
                dt.ProductCodeName = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = evd.ProductId Select p.Code + " - " + p.Name).FirstOrDefault()
                dt.CodeBatchSerial = If(dt.BatchSerialId IsNot Nothing, (From b In _context.BatchSerial Where dt.BatchSerialId = b.Id Select b.BatchCode).FirstOrDefault(), Nothing)

                dt.UnitValueProduct = evd.UnitValue
                dt.SubTotalValueProduct = evd.SubTotalValue
                dt.DiscountPercentageProduct = evd.DiscountPercentage
                dt.DiscountValueProduct = evd.DiscountValue
                dt.IvaPercentageProduct = evd.IvaPercentage
                dt.IvaValueProduct = evd.IvaValue
                dt.RtfPercentageProduct = evd.RTFPercentage
                dt.RtfValueProduct = evd.RTFValue
            Next
        End If

        Return res
    End Function

    ''' <summary>
    ''' Genera el comprobante contable para la reclasificación de la remision de entrada a través del comprobante de entrada
    ''' </summary>
    ''' <param name="Id">Id comprobante de entrada</param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_GenerateJournalVoucherByReclassificationRemissionEntrance(Id As Integer, CodeUser As String) As SP_GenerateJournalVoucherByReclassificationRemissionEntrance_Result Implements IEntranceVoucherRepository.SP_GenerateJournalVoucherByReclassificationRemissionEntrance
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateJournalVoucherByReclassificationRemissionEntrance(Id, CodeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Valida que la cantidad de los productos que tengan presupuesto asociado al grupo sea igual que la sumatoria de los compromisos
    ''' </summary>
    ''' <param name="listEntranceVoucherDetail"></param>
    ''' <param name="listEntranceVoucherCommitment"></param>
    ''' <returns></returns>
    Public Function ValidateProductsAndCommitments(freightValue As Decimal, freightIVAValue As Decimal, listEntranceVoucherDetail As List(Of EntranceVoucherDetail), listEntranceVoucherCommitment As List(Of EntranceVoucherCommitment)) As Boolean Implements IEntranceVoucherRepository.ValidateProductsAndCommitments
        Dim sumEntranceVoucherDetail As Decimal = 0
        Dim sumEntranceVoucherCommitment As Decimal = 0

        Dim listDetails = (From evd In listEntranceVoucherDetail
                           Join p In _context.InventoryProduct.AsNoTracking() On p.Id Equals evd.ProductId
                           Join g In _context.ProductGroup.AsNoTracking() On g.Id Equals p.ProductGroupId
                           Where evd.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted AndAlso g.BudgetId IsNot Nothing
                           Select New With {.BudgetId = g.BudgetId, .Value = evd.TotalValue}).ToList()

        If listDetails IsNot Nothing AndAlso listDetails.Count > 0 Then
            Dim listBudgets = (From evc In listEntranceVoucherCommitment
                               Join cd In _context.CommitmentDetail.AsNoTracking() On evc.CommitmentDetailId Equals cd.Id
                               Join b In _context.Budget.AsNoTracking() On cd.CategoryId Equals b.CategoryId And cd.RevenueTypeId Equals b.RevenueTypeId
                               Select New With {.BudgetId = b.Id, .Value = evc.Value}).ToList()

            For Each detail In listDetails.GroupBy(Function(d) d.BudgetId) 'Se valida que se hayan agregado al menos los detalles con valores restringidos
                sumEntranceVoucherDetail = detail.Sum(Function(d) d.Value)
                sumEntranceVoucherCommitment = 0

                If listBudgets IsNot Nothing AndAlso listBudgets.Any(Function(d) d.BudgetId = detail.Key) Then
                    sumEntranceVoucherCommitment = listBudgets.Where(Function(d) d.BudgetId = detail.Key).Sum(Function(d) d.Value)
                End If

                If Math.Round(sumEntranceVoucherCommitment, 0) < Math.Round(sumEntranceVoucherDetail, 0) Then
                    Return False
                End If
            Next
        End If

        sumEntranceVoucherDetail = listEntranceVoucherDetail.Sum(Function(d) d.TotalValue)
        sumEntranceVoucherCommitment = listEntranceVoucherCommitment.Sum(Function(d) d.Value)

        If Math.Round(sumEntranceVoucherCommitment, 0, MidpointRounding.AwayFromZero) <> (Math.Round(sumEntranceVoucherDetail, 0, MidpointRounding.AwayFromZero) + freightValue + freightIVAValue) Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Obtiene el valor con el que se afecto el kardex al momento de confirmar el comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEntrancerVoucerValueInKardex(Id As Integer, productId As Integer) As Decimal Implements IEntranceVoucherRepository.GetEntrancerVoucerValueInKardex

        Dim query = (From x In _context.Kardex
                     Where x.EntityId = Id AndAlso x.EntityName = NameOf(EntranceVoucher) AndAlso x.ProductId = productId)


        Return query.Sum(Function(k) k.Quantity * k.Value) / query.Sum(Function(k) k.Quantity)

    End Function

    Public Function CascadeRollback(code As String) As Integer Implements IRepositoryRollbackStrategy.CascadeRollback
        Dim sql = "
        DELETE FROM Inventory.EntranceVoucherDetailBatchSerial
        WHERE EXISTS (
            SELECT 1 
            FROM Inventory.EntranceVoucher ev
            JOIN Inventory.EntranceVoucherDetail evd 
                ON evd.EntranceVoucherId = ev.Id
            WHERE Inventory.EntranceVoucherDetailBatchSerial.EntranceVoucherDetailId = evd.Id
            AND ev.Code = @Code
	        AND ev.Status = 1
        );


        DELETE FROM Inventory.EntranceVoucherDetail
        WHERE EXISTS (
            SELECT 1 
            FROM Inventory.EntranceVoucher ev
            WHERE EntranceVoucherDetail.EntranceVoucherId = ev.Id
            AND ev.Code = @Code
	        AND ev.Status = 1
        );

        DELETE FROM Inventory.InventoryControlDocument WHERE DocumentNumber = @Code
            
        DELETE FROM Inventory.EntranceVoucher
            WHERE Code = @Code
            AND Status = 1;"

        Dim codeParamater = New SqlParameter("@Code", code)
        Dim rowsAffected As Integer = _context.ExecuteNonQuery(sql, codeParamater)

        Return rowsAffected
    End Function
End Class
