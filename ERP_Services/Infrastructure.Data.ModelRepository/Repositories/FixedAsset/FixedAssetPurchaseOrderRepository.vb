'************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 05-02-2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base

#End Region

Public Class FixedAssetPurchaseOrderRepository
    Inherits GenericRepository(Of FixedAssetPurchaseOrder)
    Implements IFixedAssetPurchaseOrderRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    
    Public Function GetFixedAssetPurchaseOrderByCode(code As String, Optional tracking As Boolean = True) As FixedAssetPurchaseOrder Implements IFixedAssetPurchaseOrderRepository.GetFixedAssetPurchaseOrderByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As FixedAssetPurchaseOrder In Me._context.FixedAssetPurchaseOrder.Include("FixedAssetPurchaseOrderItem").Include("FixedAssetPurchaseOrderAvailability").Include("Currency").AsNoTracking()
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then
            Dim ObjSuplier = (From a In _context.SuppliersDistributionLines.AsNoTracking.Include("Supplier").AsNoTracking Where a.Id = res.SupplierDistributionLineId Select a).FirstOrDefault()
            res.NameSuplier = ObjSuplier.Supplier.Code + " - " + ObjSuplier.Supplier.Name
            If res.FixedAssetPurchaseOrderItem.Count > 0 Then
                For Each ObjFixedAssetPurchaseOrderItem As FixedAssetPurchaseOrderItem In res.FixedAssetPurchaseOrderItem
                    Dim IdEquipment = ObjFixedAssetPurchaseOrderItem.ItemId
                    Dim IdIVA = ObjFixedAssetPurchaseOrderItem.IVAId
                    Dim IdTrademark = ObjFixedAssetPurchaseOrderItem.TrademarkId
                    'Dim IdPoliza = ObjFixedAssetPurchaseOrderItem.IdPolize

                    Dim ObjEquipment = (From c In _context.FixedAssetItem.AsNoTracking Where c.Id = IdEquipment Select c).FirstOrDefault()
                    Dim ObjIVA = (From d In _context.GeneralLedgerIVA.AsNoTracking Where d.Id = IdIVA Select d).FirstOrDefault()
                    Dim ObjTrademark = (From e In _context.FixedAssetTrademark.AsNoTracking Where e.Id = IdTrademark Select e).FirstOrDefault()
                    ''Dim ObjPoliza = (From f In _context.Poliza.AsNoTracking Where f.Id = IdPoliza Select f).FirstOrDefault()
                    ObjFixedAssetPurchaseOrderItem.NameEquipment = ObjEquipment.Description
                    ObjFixedAssetPurchaseOrderItem.NameTrademark = ObjTrademark.Name
                    ObjFixedAssetPurchaseOrderItem.NameIva = ObjIVA.Name

                Next
            End If

            If res.FixedAssetPurchaseOrderAvailability IsNot Nothing AndAlso res.FixedAssetPurchaseOrderAvailability.Any() Then
                For Each item In res.FixedAssetPurchaseOrderAvailability
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
            ''' Verifica si la cantidad de cada artículo es igual a su cantidad  legalizada
            Dim allItemsFullyCancelled = res.FixedAssetPurchaseOrderItem.All(Function(detail) detail.Quantity = detail.CancelledQuantity)
            If allItemsFullyCancelled Then

                ''' Obtiene los códigos de los activos fijos relacionados con la orden de compra
                Dim relatedFixedAssetEntry = (From fapoi In _context.FixedAssetPurchaseOrderItem.AsNoTracking()
                                              Join faei In _context.FixedAssetEntryItem.AsNoTracking()
                                                      On faei.PurchaseOrderItemId Equals fapoi.Id
                                              Where fapoi.PurchaseOrderId = res.Id
                                              Select assetCode = faei.FixedAssetEntry.Code).Distinct().ToList()

                If relatedFixedAssetEntry.Any() Then
                    res.FixedAssetEntryCodes.AddRange(relatedFixedAssetEntry)
                End If

                ''' Obtiene los códigos de las remisiones de entrada de activos fijos relacionadas con la orden de compra FixedAssetRemissionEntranceItem
                Dim relatedFixedAssetRemissionEntrance = (
                    From fapoi In _context.FixedAssetPurchaseOrderItem.AsNoTracking()
                    Join frei In _context.FixedAssetRemissionEntranceItem.AsNoTracking()
                        On frei.PurchaseOrderItemId Equals fapoi.Id
                    Where fapoi.PurchaseOrderId = res.Id
                    Select assetCode = frei.FixedAssetRemissionEntrance.Code
                ).Distinct().ToList()

                If relatedFixedAssetRemissionEntrance.Any() Then
                    res.FixedAssetRemissionEntranceCodes.AddRange(relatedFixedAssetRemissionEntrance)
                End If
            Else
                res.IsPartiallyLegalized = Not res.FixedAssetPurchaseOrderItem.All(Function(detail) detail.Quantity = detail.OutstandingQuantity)
            End If


            If res.RequestedFunctionalUnitId IsNot Nothing Then
                res.RequestedFunctionalUnitName = (From fu In _context.FunctionalUnit.AsNoTracking() Where fu.Id = res.RequestedFunctionalUnitId Select String.Concat(fu.Code, " - ", fu.Name)).FirstOrDefault()
            End If

            res.OriginalValue = (From d As FixedAssetPurchaseOrder In Me._context.FixedAssetPurchaseOrder.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetPurchaseOrder()
        End If
    End Function

    Public Function GetFixedAssetPurchaseOrderItemById(IdPurchaseOrder As Integer, Optional tracking As Boolean = True) As FixedAssetPurchaseOrderItem Implements IFixedAssetPurchaseOrderRepository.GetFixedAssetPurchaseOrderItemById
        If IdPurchaseOrder.ToString.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("IdPurchaseOrder")
        End If
        Return (From p In _context.FixedAssetPurchaseOrderItem Where p.Id = IdPurchaseOrder Select p).FirstOrDefault
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks><code>HRR PBI4547</code></remarks>
    Function ListPurchaseRequestToOrder() AS List(OF Domain.Entities.SP_PurchaseRequestToOrderFixedAsset_Result)Implements IFixedAssetPurchaseOrderRepository.ListPurchaseRequestToOrder
        Dim resultado As List(Of SP_PurchaseRequestToOrderFixedAsset_Result) = _context.SP_PurchaseRequestToOrderFixedAsset.ToList
        resultado.ForEach(Sub(d) d.MaxValue = d.OutstandingQuantity)
        Return resultado
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="xmlPurchaseOrder"></param>
    ''' <returns></returns>
    ''' <remarks><code>HRR PBI4547</code></remarks>
    Function ConfirmPurchaseOrder(xmlPurchaseOrder As String) As SP_ConfirmPurchaseOrderFixedAsset_Result Implements IFixedAssetPurchaseOrderRepository.ConfirmPurchaseOrder
        Return _context.SP_ConfirmPurchaseOrderFixedAsset(xmlPurchaseOrder).FirstOrDefault()
    End Function

End Class
