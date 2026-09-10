'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 13-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Text

Public Class PurchaseOrderRepository

    Inherits GenericRepository(Of PurchaseOrder)
    Implements IPurchaseOrderRepository

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
    ''' Obtiene una Orden de Compra por Código
    ''' </summary>
    ''' <param name="PurchaseOrderCode">Código</param>
    ''' <returns>PurchaseOrder</returns>
    ''' <remarks></remarks>
    Public Function GetPurchaseOrderByCode(PurchaseOrderCode As String) As PurchaseOrder Implements IPurchaseOrderRepository.GetPurchaseOrderByCode
        If PurchaseOrderCode Is Nothing OrElse PurchaseOrderCode.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim res = (From d As PurchaseOrder In Me._context.PurchaseOrder.Include("PurchaseOrderDetail.RemissionEntranceDetail.RemissionEntrance").Include("PurchaseOrderDetail.EntranceVoucherDetail.EntranceVoucher").Include("PurchaseOrderAvailability").Include("Currency").AsNoTracking()
                   Where d.Code.Equals(PurchaseOrderCode.Trim())
                   Select d).FirstOrDefault


        If res IsNot Nothing Then

            Dim allDetailsMatch = res.PurchaseOrderDetail.All(Function(detail) detail.Quantity = detail.CancelledQuantity)

            If allDetailsMatch Then
                ' Obtener los IDs de las cabeceras a partir de los detalles relacionados
                Dim relatedEntranceVoucherCodes = (From d In res.PurchaseOrderDetail
                                                   From entranceDetail In d.EntranceVoucherDetail
                                                   Where entranceDetail IsNot Nothing
                                                   Select entranceDetail.EntranceVoucher.Code).Distinct().ToList()

                If relatedEntranceVoucherCodes IsNot Nothing AndAlso relatedEntranceVoucherCodes.Count > 0 Then
                    res.entranceVoucherCodes.AddRange(relatedEntranceVoucherCodes)
                End If

                Dim relatedRemissionEntranceCodes = (From d In res.PurchaseOrderDetail
                                                     From remissionDetail In d.RemissionEntranceDetail
                                                     Where remissionDetail IsNot Nothing
                                                     Select remissionDetail.RemissionEntrance.Code).Distinct().ToList()

                If relatedRemissionEntranceCodes IsNot Nothing AndAlso relatedRemissionEntranceCodes.Count > 0 Then
                    res.remissionCodes.AddRange(relatedRemissionEntranceCodes)
                End If
            Else
                res.partly = Not res.PurchaseOrderDetail.All(Function(detail) detail.Quantity = detail.OutstandingQuantity)
            End If
            If res.IsBasedContract AndAlso res.ContractId IsNot Nothing Then
                    Dim contract = (From s In _context.InventoryContract.AsNoTracking Where res.ContractId = s.Id Select s).FirstOrDefault
                    res.ContractNumber = contract.ContractNumber
                    res.ContractManageProducts = contract.ManageProducts
                End If
                Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where res.SupplierDistributionLineId = sdl.Id Select sdl).FirstOrDefault
            Dim supplier = (From s In _context.Supplier.AsNoTracking Where supplierDistributionLine.IdSupplier = s.Id Select s).FirstOrDefault
            Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking Where supplierDistributionLine.IdDistributionLine = dl.Id Select dl).FirstOrDefault
            res.DescriptionSupplier = supplier.Code + " - " + supplier.Name + " - " + distributionLine.Code + " - " + distributionLine.Name
            If res.WarehouseId IsNot Nothing Then
                Dim warehouse = (From s In _context.Warehouse.AsNoTracking Where res.WarehouseId = s.Id Select s).FirstOrDefault
                res.DescriptionWarehouse = warehouse.Code + " - " + warehouse.Name
                res.Prefix = warehouse.Prefix
            End If

            If res.PurchaseOrderDetail IsNot Nothing AndAlso res.PurchaseOrderDetail.Count > 0 Then
                For Each item As PurchaseOrderDetail In res.PurchaseOrderDetail
                    Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where item.ProductId = p.Id Select p).FirstOrDefault()
                    item.InventoryProduct = product
                    item.ProductCode = product.Code
                    item.ProductName = product.Name
                    item.TotalIva = item.SubTotalValue * (item.IvaPercentage / 100)
                    If product.ManufacturerId IsNot Nothing Then
                        item.ManufacturerName = (From p In _context.Manufacturer.AsNoTracking() Where product.ManufacturerId = p.Id Select p.Name).FirstOrDefault()
                    End If
                    item.HealthRegistration = product.HealthRegistration
                    item.Presentation = product.Presentation
                Next
            End If

            If res.PurchaseOrderAvailability IsNot Nothing AndAlso res.PurchaseOrderAvailability.Any() Then
                For Each item In res.PurchaseOrderAvailability
                    Dim availabilityDetail = (From x In _context.AvailabilityDetail.AsNoTracking().Include("Availability").AsNoTracking().Include("Budget").AsNoTracking().
                                                  Include("Budget.Category").AsNoTracking().Include("Budget.RevenueType").AsNoTracking().Include("Budget.Category.FinancialSource").AsNoTracking()
                                              Where x.Id = item.AvailabilityDetailId
                                              Select x).FirstOrDefault()

                    item.AvailabilityCode = availabilityDetail.Availability.Code
                    item.CategoryCodeName = String.Format("{0} - {1}", availabilityDetail.Budget.Category.Code, availabilityDetail.Budget.Category.Name)
                    item.FinancialSourceCodeName = String.Format("{0} - {1}", availabilityDetail.Budget.Category.FinancialSource.Code, availabilityDetail.Budget.Category.FinancialSource.Name)
                    item.RevenueTypeCodeName = String.Format("{0} - {1}", availabilityDetail.Budget.RevenueType.Code, availabilityDetail.Budget.RevenueType.Name)
                    item.Balance = availabilityDetail.Balance

                    If res.BudgetaryValidityId Is Nothing Then
                        Dim budgetaryValidity = (From bv In _context.BudgetaryValidity.AsNoTracking() Where bv.Id = availabilityDetail.Availability.BudgetaryValidityId Select bv).FirstOrDefault()

                        If budgetaryValidity IsNot Nothing Then
                            res.BudgetaryValidityId = budgetaryValidity.Id
                            res.BudgetaryValidityDescription = budgetaryValidity.Year

                            res.BudgetaryEntityId = budgetaryValidity.BudgetaryEntityId
                            res.BudgetaryEntityDescription = (From be In _context.BudgetaryEntity.AsNoTracking Where be.Id = budgetaryValidity.BudgetaryEntityId Select String.Concat(be.Code, " - ", be.Name)).FirstOrDefault
                        End If
                    End If
                Next
            End If

            res.OriginalValue = (From g In _context.PurchaseOrder.AsNoTracking
                                 Where g.Code.Equals(PurchaseOrderCode.Trim())
                                 Select g).FirstOrDefault
            Return res
        Else
            Return New PurchaseOrder
        End If
    End Function

    ''' <summary>
    ''' Obtiene una Orden de Compra por Id
    ''' </summary>
    ''' <param name="Id">Id</param>
    ''' <returns>PurchaseOrder</returns>
    ''' <remarks></remarks>
    Public Function GetPurchaseOrderById(Id As Integer) As PurchaseOrder Implements IPurchaseOrderRepository.GetPurchaseOrderById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From PurchaseOrder In _context.PurchaseOrder.Include("PurchaseOrderDetail").Include("PurchaseOrderDetail.InventoryProduct") Where PurchaseOrder.Id = Id Select PurchaseOrder).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From PurchaseOrder In _context.PurchaseOrder.AsNoTracking() Where PurchaseOrder.Id = Id Select PurchaseOrder).FirstOrDefault()
            Return query
        Else
            Return New PurchaseOrder()
        End If
    End Function

    Public Function GetQuantityAsociateDocument(code As String) As ActionResult Implements IPurchaseOrderRepository.GetQuantityAsociateDocument
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        'Variable en donde se almacena el string de si hay o no errores
        Dim listErrors As New StringBuilder

        'Consulto si esta asociada a un documento de remision de entrada
        Dim validateRemission = (From red In _context.RemissionEntranceDetail.AsNoTracking()
                                 Join re In _context.RemissionEntrance.AsNoTracking() On re.Id Equals red.RemissionEntranceId
                                 Where red.RemissionSource = 2 AndAlso red.SourceCode.Equals(code.Trim()) AndAlso re.Status <> 3
                                 Select re).ToList()

        If validateRemission IsNot Nothing AndAlso validateRemission.Count > 0 Then
            validateRemission.ForEach(Sub(x) listErrors.AppendLine("El documento no se puede desconfirmar porque tiene asociada una remisión de entrada: " + x.Code + " - " + x.Description))
        End If

        'consulto si esta asociada a un documento de comprobante de entrada
        Dim validateEntranceVoucher = (From evd In _context.EntranceVoucherDetail.AsNoTracking()
                                       Join ev In _context.EntranceVoucher.AsNoTracking() On ev.Id Equals evd.EntranceVoucherId
                                       Where evd.EntranceSource = 2 AndAlso evd.SourceCode.Equals(code.Trim()) AndAlso ev.Status <> 3
                                       Select ev).ToList()

        If validateEntranceVoucher IsNot Nothing AndAlso validateEntranceVoucher.Count > 0 Then
            validateEntranceVoucher.ForEach(Sub(x) listErrors.AppendLine("El documento no se puede desconfirmar porque tiene asociada un comprobante de entrada: " + x.Code + " - " + x.Description))
        End If

        'Se valida si hay errores para retornarlos
        If listErrors.ToString.Length > 0 Then
            Return New ActionResult With {.StateResult = False, .Message = listErrors.ToString()}
        End If

        'Retorna todo ok
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="xmlPurchaseOrder"></param>
    ''' <returns></returns>
    Function ConfirmPurchaseOrder(xmlPurchaseOrder As string) As SP_ConfirmPurchaseOrder_Result Implements IPurchaseOrderRepository.ConfirmPurchaseOrder
        Return _context.SP_ConfirmPurchaseOrder(xmlPurchaseOrder).FirstOrDefault()
    End Function

End Class
