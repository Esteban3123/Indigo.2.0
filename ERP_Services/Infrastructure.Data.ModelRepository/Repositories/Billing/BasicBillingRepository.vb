Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class BasicBillingRepository
    Inherits GenericRepository(Of BasicBilling)
    Implements IBasicBillingRepository

#Region "Builder"

    ''' <summary>
    ''' Contexto de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de inventario
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene una factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetDocumentInvoiceProductSalesById(id As Integer) As BasicBilling Implements IBasicBillingRepository.GetBasicBillingById
        Dim res = (From bb In _context.BasicBilling Where bb.Id = id Select bb).FirstOrDefault()
        If res IsNot Nothing Then
            res.BillingAuthorizationName = (From ba In _context.BillingAuthorization.AsNoTracking() Where ba.Id = res.BillingAuthorizationId Select ba.Name).FirstOrDefault()
            res.CustomerName = (From c In _context.Customer.AsNoTracking() Where c.Id = res.CustomerId Select String.Concat(c.Nit, " - ", c.Name)).FirstOrDefault()
            res.AddressName = (From a In _context.Address.AsNoTracking().Include("Department").AsNoTracking().Include("City").AsNoTracking() Where a.Id = res.AddressId Select String.Concat(a.Department.Name, " - ", a.City.Name, ", ", a.Addresss)).FirstOrDefault()
            If res.WarehouseId IsNot Nothing Then
                res.WarehouseName = (From w In _context.Warehouse.AsNoTracking() Where w.Id = res.WarehouseId Select String.Concat(w.Code, " - ", w.Name)).FirstOrDefault()
            End If
            res.OriginalValue = (From pi In _context.BasicBilling.AsNoTracking() Where pi.Id = res.Id Select pi).FirstOrDefault()
            Return res
        End If
        Return New BasicBilling
    End Function

    ''' <summary>
    ''' obtiene una factura por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetBasicBillingByCode(code As String) As BasicBilling Implements IBasicBillingRepository.GetBasicBillingByCode

        'Diccionario de catalogos de equipos
        Dim dictionaryRetentionConcepts As New Dictionary(Of Integer, Decimal)()

        Dim res = (From bb In _context.BasicBilling.Include("Currency").Include("BasicBillingGifts.BasicBillingGiftsItem").Include("BasicBillingDetail.BasicBillingDetailItem")
                   Where bb.Code = code
                   Select bb).FirstOrDefault()

        If res IsNot Nothing Then
            res.BillingAuthorizationName = (From ba In _context.BillingAuthorization.AsNoTracking() Where ba.Id = res.BillingAuthorizationId Select ba.Name).FirstOrDefault()

            Dim customer = (From c In _context.Customer.AsNoTracking() Where c.Id = res.CustomerId Select c).FirstOrDefault()
            res.CustomerName = String.Concat(customer.Nit, " - ", customer.Name)
            res.ThirdPartyCustomerId = $"{customer.ThirdPartyId}-{customer.Id}"

            Dim address = (From a In _context.Address.AsNoTracking() Where a.Id = res.AddressId Select a).FirstOrDefault()
            Dim department = (From a In _context.Department.AsNoTracking() Where a.Id = address.DepartmentId Select a).FirstOrDefault()
            Dim city = (From a In _context.City.AsNoTracking() Where a.Id = address.CityId Select a).FirstOrDefault()
            res.AddressName = department.Name.Trim() + " - " + city.Name.Trim() + ", " + address.Addresss.Trim()
            res.CurrencyAbbreviation = $"{res?.Currency?.Abbreviation}"

            If res.BudgetId IsNot Nothing Then
                Dim budget = (From b In _context.Budget.AsNoTracking().Include("Category").AsNoTracking().Include("Category.FinancialSource").AsNoTracking().Include("RevenueType").AsNoTracking() Where b.Id = res.BudgetId).FirstOrDefault()
                If budget IsNot Nothing Then
                    res.BudgetDescription = String.Concat(budget.Category.Code, " - ", budget.Category.Name, " - ", budget.Category.FinancialSource.Code, " - ", budget.Category.FinancialSource.Name, " - ", budget.RevenueType.Code, " - ", budget.RevenueType.Name)
                    If res.BudgetaryValidityId Is Nothing Then
                        Dim budgetaryValidity = (From bh In _context.BudgetHeader.AsNoTracking()
                                                 Join bv In _context.BudgetaryValidity.AsNoTracking()
                                                    On bh.BudgetaryValidityId Equals bv.Id
                                                 Where bh.Id = budget.BudgetHeaderId Select bv).FirstOrDefault()

                        If budgetaryValidity IsNot Nothing Then
                            res.BudgetaryValidityId = budgetaryValidity.Id
                            res.BudgetaryValidityDescription = budgetaryValidity.Year

                            res.BudgetaryEntityId = budgetaryValidity.BudgetaryEntityId
                            res.BudgetaryEntityDescription = (From be In _context.BudgetaryEntity.AsNoTracking Where be.Id = budgetaryValidity.BudgetaryEntityId Select String.Concat(be.Code, " - ", be.Name)).FirstOrDefault
                        End If
                    End If
                End If
            End If

            If res.ConditionSalesId IsNot Nothing Then
                res.CodeNameConditionSales = (From cs In _context.ConditionSales.AsNoTracking
                                              Where cs.Id = res.ConditionSalesId
                                              Select String.Concat(cs.Code, " - ", cs.Name)).FirstOrDefault
            End If

            If res.EconomicActivityId IsNot Nothing Then
                res.CodeNameEconomicActivity = (From ea In _context.EconomicActivity.AsNoTracking
                                                Where ea.Id = res.EconomicActivityId
                                                Select String.Concat(ea.Code, " - ", ea.Name)).FirstOrDefault
            End If

            For Each detail In res.BasicBillingDetail

                    If detail.DetailType = 1 Then
                        detail.CodeName = (From ip In _context.InventoryProduct.AsNoTracking Where ip.Id = detail.ProductId Select ip.Code + " - " + ip.Name).FirstOrDefault()
                        detail.WarehouseName = (From w In _context.Warehouse.AsNoTracking() Where w.Id = detail.WarehouseId Select String.Concat(w.Code, " - ", w.Name)).FirstOrDefault()

                        If detail.FeeId IsNot Nothing Then
                            detail.FeeName = (From f In _context.ProductAndServiceFee.AsNoTracking() Where f.Id = detail.FeeId Select String.Concat(f.Code, " - ", f.Name)).FirstOrDefault
                        End If

                    ElseIf detail.DetailType = 2 Then
                        detail.CodeName = (From bc In _context.BillingConcept.AsNoTracking Where bc.Id = detail.BillingConceptId Select bc.Code + " - " + bc.Name).FirstOrDefault()

                        If detail.ServicesProvidedId IsNot Nothing Then
                            detail.CodeName = (From r In _context.BillingConcept.AsNoTracking Where r.Id = detail.ServicesProvidedId Select String.Concat(r.Code, " - ", r.Name)).FirstOrDefault()
                            detail.ServicesProvidedName = (From bc In _context.BillingConcept.AsNoTracking Where bc.Id = detail.BillingConceptId Select bc.Code + " - " + bc.Name).FirstOrDefault()
                        End If

                        detail.SupplierName = (From r In _context.Supplier.AsNoTracking Where r.Id = detail.SupplierId Select String.Concat(r.Code, " - ", r.Name)).FirstOrDefault()

                        detail.SalesExecutiveName = (From r In _context.SalesExecutive.AsNoTracking
                                                     Join tp In _context.ThirdParty On tp.Id Equals r.ThirdPartyId
                                                     Where r.Id = detail.SalesExecutiveId Select String.Concat(tp.Nit, " - ", tp.Name)).FirstOrDefault()

                        If detail.FeeId IsNot Nothing Then
                            detail.FeeName = (From f In _context.ProductAndServiceFee.AsNoTracking() Where f.Id = detail.FeeId Select String.Concat(f.Code, " - ", f.Name)).FirstOrDefault
                        End If

                    ElseIf detail.DetailType = 3 Then
                        Dim physicalAsset = (From fapa In _context.FixedAssetPhysicalAsset.AsNoTracking Where fapa.Id = detail.PhysicalAssetId Select fapa).FirstOrDefault()
                        Dim physicalAssetItem = (From fai In _context.FixedAssetItem.AsNoTracking Where fai.Id = physicalAsset.ItemId Select fai).FirstOrDefault()
                        detail.CodeName = physicalAsset.Plate + " - " + physicalAssetItem.Code + " - " + physicalAssetItem.Description

                    ElseIf detail.DetailType = 4 Then
                        detail.CodeName = (From fapap In _context.FixedAssetPhysicalAssetParts.AsNoTracking.Include("FixedAssetPartsAccesoriesConsumables").AsNoTracking Where fapap.Id = detail.PhysicalAssetPartId Select fapap.FixedAssetPartsAccesoriesConsumables.Code + " - " + fapap.FixedAssetPartsAccesoriesConsumables.Name).FirstOrDefault()
                    End If

                    detail.FunctionalUnitIdName = (From fu In _context.FunctionalUnit.AsNoTracking() Where fu.Id = detail.FunctionalUnitId Select String.Concat(fu.Code, " - ", fu.Name)).FirstOrDefault()
                    detail.CostCenterId = (From fu In _context.FunctionalUnit.AsNoTracking() Where fu.Id = detail.FunctionalUnitId Select fu.CostCenterId).FirstOrDefault()

                    Dim retentionBase As Decimal = 0
                    If detail.RetentionIdTax IsNot Nothing Then
                        If Not dictionaryRetentionConcepts.ContainsKey(detail.RetentionIdTax) Then
                            retentionBase = (From rc In _context.RetentionConcepts Where rc.Id = detail.RetentionIdTax Select rc.MinBase).FirstOrDefault()
                            dictionaryRetentionConcepts.Add(detail.RetentionIdTax, retentionBase)
                        Else
                            retentionBase = dictionaryRetentionConcepts(detail.RetentionIdTax)
                        End If
                    End If
                    detail.RetentionBaseTax = retentionBase

                    retentionBase = 0
                    If detail.RetentionIdICA IsNot Nothing Then
                        If Not dictionaryRetentionConcepts.ContainsKey(detail.RetentionIdICA) Then
                            retentionBase = (From rc In _context.RetentionConcepts Where rc.Id = detail.RetentionIdICA Select rc.MinBase).FirstOrDefault()
                            dictionaryRetentionConcepts.Add(detail.RetentionIdICA, retentionBase)
                        Else
                            retentionBase = dictionaryRetentionConcepts(detail.RetentionIdICA)
                        End If
                    End If
                    detail.RetentionBaseICA = retentionBase

                    If detail.BasicBillingDetailItem IsNot Nothing AndAlso detail.BasicBillingDetailItem.Count > 0 Then
                        For Each item In detail.BasicBillingDetailItem
                            item.BatchCode = (From bs In _context.BatchSerial.AsNoTracking
                                              Join p In _context.PhysicalInventory.AsNoTracking
                                          On bs.Id Equals p.BatchSerialId
                                              Where p.Id = item.PhysicalInventoryId
                                              Select bs.BatchCode).FirstOrDefault()
                        Next
                    End If
                Next

                If res.BasicBillingGifts IsNot Nothing And res.BasicBillingGifts.Count > 0 Then
                    For Each item In res.BasicBillingGifts
                        item.ProductName = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select String.Concat(p.Code, " - ", p.Name)).FirstOrDefault()
                        item.CodeNameWarehouse = (From w In _context.Warehouse.AsNoTracking() Where w.Id = item.WarehouseId Select String.Concat(w.Code, " - ", w.Name)).FirstOrDefault()
                    Next
                End If

                Return res
            Else
                Return New BasicBilling
        End If
    End Function

    ''' <summary>
    ''' Consulta para obtener los recibos de caja en CashReceipts que estan asociados a la factura en BasicBilling
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCashReceiptsByBasicBillingInvoice(id As Integer) As List(Of CashReceipts) Implements IBasicBillingRepository.GetCashReceiptsByBasicBillingInvoice
        Dim Query As IQueryable(Of CashReceipts) = Nothing
        Query = (From a In _context.AccountReceivable
                 Join b In _context.CashReceiptAccountReceivable On a.Id Equals b.AccountReceivableId
                 Join c In _context.CashReceiptDetails On b.CashReceiptDetailId Equals c.Id
                 Join d In _context.CashReceipts On c.IdCashReceipt Equals d.Id
                 Where a.InvoiceId = id
                 Select d)

        Return Query.ToList()
    End Function

    ''' <summary>
    ''' Funcion para validar si una direccion esta asociada a un documento
    ''' </summary>
    ''' <param name="AdressId"></param>
    ''' <returns></returns>
    Public Function ValidateAdressInBasicBilling(AdressId As Integer) As Boolean Implements IBasicBillingRepository.ValidateAdressInBasicBilling
        If AdressId = 0 Then
            Return True
        End If
        Return _context.BasicBilling.Any(Function(x) x.AddressId = AdressId)
    End Function

    ''' <summary>
    ''' Copiar y pegar para el formulario
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SetBasicBillingDetailFromFile(addressId As Integer, wareHouseId As Integer, xmlObject As String) As List(Of SP_SetBasicBillingDetailFromFile_Result) Implements IBasicBillingRepository.SetBasicBillingDetailFromFile
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SetBasicBillingDetailFromFile(addressId, wareHouseId, xmlObject).ToList()
    End Function

    ''' <summary>
    '''  Guarda una factura
    ''' </summary>
    ''' <param name="EntityXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Public Async Function SP_SaveBasicBilling(EntityXml As String, codeUser As String) As Task(Of SP_SaveBasicBilling_Result) Implements IBasicBillingRepository.SP_SaveBasicBilling
        Return Await Task.Run(Function()
                                  DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
                                  Return _context.SP_SaveBasicBilling(EntityXml, codeUser).SingleOrDefault
                              End Function)
    End Function

    ''' <summary>
    ''' Confirma una factura
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Public Async Function SP_ConfirmBasicBilling(id As Integer, codeUser As String, cashReceiptsXml As String, companyType As Integer) As Task(Of SP_ConfirmBasicBilling_Result) Implements IBasicBillingRepository.SP_ConfirmBasicBilling
        Return Await Task.Run(Function()
                                  DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
                                  Return _context.SP_ConfirmBasicBilling(id, codeUser, cashReceiptsXml, companyType).SingleOrDefault
                              End Function)
    End Function

    ''' <summary>
    ''' Reversa una factura
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="codeUser"></param>
    ''' <param name="reversalReasonId"></param>
    ''' <param name="reversalReasonDescription"></param>
    ''' <param name="companyType"></param>
    ''' <returns></returns>
    Public Function SP_ReverseBasicBilling(id As Integer, codeUser As String, reversalReasonId As Integer, reversalReasonDescription As String, companyType As Integer) As SP_ReverseBasicBilling_Result Implements IBasicBillingRepository.SP_ReverseBasicBilling
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ReverseBasicBilling(id, codeUser, reversalReasonId, reversalReasonDescription, companyType).SingleOrDefault
    End Function

#End Region

End Class
