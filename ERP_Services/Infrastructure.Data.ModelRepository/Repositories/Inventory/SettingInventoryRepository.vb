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
Imports Infrastructure.CrossCutting.Base

Public Class SettingInventoryRepository
    Inherits GenericRepository(Of SettingInventory)
    Implements ISettingInventoryRepository


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
    ''' obtiene una configuracion 
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSettingInventory(OperatingUnitId As Integer) As SettingInventory Implements ISettingInventoryRepository.GetSettingInventory
        Dim res = (From si In _context.SettingInventory.AsNoTracking().Include("SettingInventoryFunctionalUnit").AsNoTracking().Include("BatchSerialRange").AsNoTracking() Where si.OperatingUnitId = OperatingUnitId Select si).FirstOrDefault()
        If res IsNot Nothing Then
            ' Conceptos de rentencion de iva y cuenta
            Dim AccountPayableConceptsIVA = (From apc In _context.AccountPayableConcepts.Include("RetentionConcepts").Include("MainAccounts").Include("MainAccounts1").Include("MainAccounts2") Where res.IVAAccountPayableConceptId = apc.Id Select apc).FirstOrDefault()
            res.AccountIVAId = AccountPayableConceptsIVA?.IdAccount
            res.RetentionConceptsIVAId = AccountPayableConceptsIVA?.RetentionConceptId
            res.IVAHandlessCostCenter = AccountPayableConceptsIVA?.MainAccounts?.HandlesCostCenter
            res.AccountIVACodeName = AccountPayableConceptsIVA?.MainAccounts?.Number + " - " + AccountPayableConceptsIVA?.MainAccounts?.Name
            ' Retencion de IVA
            If res.IVARetention = 1 Then
                res.WithholdingIvaPercentage = Nothing
            Else
                Dim IvaRetention = (From ir In _context.AccountPayableConcepts.Include("RetentionConcepts").Include("MainAccounts").AsNoTracking() Select ir Where res.IVARetentionAccountPayableConceptId = ir.Id).FirstOrDefault()
                res.WithholdingIvaPercentage = IvaRetention.RetentionConcepts.Rate
                res.WithholdingIvaBase = IvaRetention.RetentionConcepts.MinBase
                res.AccountIVARetentionId = IvaRetention.IdAccount
                res.RetentionConceptsIVARetentionId = IvaRetention.RetentionConceptId
                res.HandlesCostCenterIVARetention = IvaRetention.MainAccounts.HandlesCostCenter
            End If
            ' Fletes
            Dim frA = (From fa In _context.AccountPayableConcepts.Include("MainAccounts").AsNoTracking() Select fa Where fa.Id = res.FreightAccountPayableConceptId).FirstOrDefault()
            res.FreightHandlesCostCenter = frA.MainAccounts.HandlesCostCenter
            res.FreightAccountCodeName = frA.MainAccounts.Number + " - " + frA.MainAccounts.Name
            res.FreigthAcountId = frA.IdAccount
            ' IVA de los Fletes
            Dim AcountPayableConceptsIVAFreight = (From apc In _context.AccountPayableConcepts.Include("RetentionConcepts").Include("MainAccounts").AsNoTracking() Select apc Where apc.Id = res.IVAFreightAccountPayableConceptId).FirstOrDefault()
            res.IvaFreightHandlesCostCenter = AcountPayableConceptsIVAFreight.MainAccounts.HandlesCostCenter
            res.IvaFreightAccountCodeName = AcountPayableConceptsIVAFreight.MainAccounts.Number + " - " + AcountPayableConceptsIVAFreight.MainAccounts.Name
            res.IvaFreigthAccountId = AcountPayableConceptsIVAFreight.IdAccount
            res.IvaFreigthPercentage = (From e In _context.GeneralLedgerIVA.AsNoTracking() Where e.Id = res.FreightIVAId Select e.Percentage).FirstOrDefault()  'AcountPayableConceptsIVAFreight.RetentionConcepts.Rate
            res.IvaFreigthRetentionConceptsId = AcountPayableConceptsIVAFreight.RetentionConceptId
            ' Ajuste de inventario
            ''Entrada
            Dim ConceptsAdjustmentIn = (From ca In _context.AdjustmentConcept.Include("MainAccounts").AsNoTracking() Where ca.Id = res.InputAdjustmentConceptId Select ca).FirstOrDefault()
            res.AdjustmentInAccountId = ConceptsAdjustmentIn.AdjustmentAccountId
            res.AdjustmentInHandlessCostCenter = ConceptsAdjustmentIn.MainAccounts.HandlesCostCenter
            res.AdjustmentInCostCenterId = ConceptsAdjustmentIn.CostCenterId
            ''Salida
            Dim ConceptsAdjustmentOut = (From ca In _context.AdjustmentConcept.Include("MainAccounts").AsNoTracking() Where ca.Id = res.OutputAdjustmentConceptId Select ca).FirstOrDefault()
            res.AdjustmentOutAccountId = ConceptsAdjustmentOut.AdjustmentAccountId
            res.AdjustmentOutHandlessCostCenter = ConceptsAdjustmentOut.MainAccounts.HandlesCostCenter
            res.AdjustmentOutCostCenterId = ConceptsAdjustmentOut.CostCenterId
            'bandera de parametros de empresa para identificar si maneja impuesto incluido o no
            res.FlagTaxInclude = _context.CompanySettings?.FirstOrDefault()?.SalePriceIncludeTax
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene el registro de parametros de inventario para el formulario por la unidad operativa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventorySettingsRegister(operatingUnitId As Integer) As SettingInventory Implements ISettingInventoryRepository.GetInventorySettingsRegister
        Dim res = (From insett In _context.SettingInventory _
                       .Include("SettingInventoryFunctionalUnit") _
                       .Include("SettingInventoryPBSControl") _
                       .Include("BatchSerialRange") _
                       .Include("SettingInventoryMedicationsControl")
                   Where insett.OperatingUnitId = operatingUnitId Select insett).FirstOrDefault()

        If res IsNot Nothing Then

            'Tipos de comprobante contables
            Dim journalVoucherType As JournalVoucherTypes

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.PurchaseJournalVoucherTypeId Select jvt).FirstOrDefault
            res.PurchaseJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.SalesJournalVoucherTypeId Select jvt).FirstOrDefault
            res.SalesJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.RemissionEntranceJournalVoucherTypeId Select jvt).FirstOrDefault
            res.RemissionEntranceJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.RemissionOutputJournalVoucherTypeId Select jvt).FirstOrDefault
            res.RemissionOutputJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.RemissionEntranceDevolutionJournalVoucherTypeId Select jvt).FirstOrDefault
            res.RemissionEntranceDevolutionJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.RemissionOutputDevolutionJournalVoucherTypeId Select jvt).FirstOrDefault
            res.RemissionOutputDevolutionJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.ReclassificationRemissionJournalVoucherTypeId Select jvt).FirstOrDefault
            res.ReclassificationRemissionJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.LoanJournalVoucherTypeId Select jvt).FirstOrDefault
            res.LoanJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.LoanReturnJournalVoucherTypeId Select jvt).FirstOrDefault
            res.LoanReturnJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.SalesReturnJournalVoucherTypeId Select jvt).FirstOrDefault
            res.SalesReturnJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.PurchaseReturnJournalVoucherTypeId Select jvt).FirstOrDefault
            res.PurchaseReturnJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.ConsignmentMerchandiseJournalVoucherTypeId Select jvt).FirstOrDefault
            res.ConsignmentMerchandiseJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.ConsignmentMerchandiseDevolutionJournalVoucherTypeId Select jvt).FirstOrDefault
            res.ConsignmentMerchandiseDevolutionJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.ConsignmentInventoryUseJournalVoucherTypeId Select jvt).FirstOrDefault
            res.ConsignmentInventoryUseJournalVoucherTypDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.ConsignmentInventoryUseDevolutionJournalVoucherTypeId Select jvt).FirstOrDefault
            res.ConsignmentInventoryUseDevolutionJournalVoucherTypDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.OrderDispatchJournalVoucherTypeId Select jvt).FirstOrDefault
            res.OrderDispatchJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.OrderDispatchReturnJournalVoucherTypeId Select jvt).FirstOrDefault
            res.OrderDispatchReturnJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.InventoryAdjustmentJournalVoucherTypeId Select jvt).FirstOrDefault
            res.InventoryAdjustmentJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            If res.TransferBetweenWarehousesConsignmentId IsNot Nothing Then
                journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.TransferBetweenWarehousesConsignmentId Select jvt).FirstOrDefault
                res.TransferBetweenWarehousesConsignmentDescription = journalVoucherType.Code + " - " + journalVoucherType.Name
            End If

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.InventoryCloseAdjustmentJournalVoucherTypeId Select jvt).FirstOrDefault
            res.InventoryCloseAdjustmentJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            If res.PartialReturnSalesJournalVoucherTypeId IsNot Nothing Then
                journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.PartialReturnSalesJournalVoucherTypeId Select jvt).FirstOrDefault
                res.PartialReturnSalesJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name
            End If

            If res.ValuationConsignmentPriceJournalVoucherTypesId IsNot Nothing Then
                journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.ValuationConsignmentPriceJournalVoucherTypesId Select jvt).FirstOrDefault
                res.ValuationByPriceConsignmentDescription = journalVoucherType.Code + " - " + journalVoucherType.Name
            End If

            'Conceptos de pagos
            Dim accountPayableConcepts As AccountPayableConcepts

            accountPayableConcepts = (From apc In _context.AccountPayableConcepts.AsNoTracking Where apc.Id = res.IVAFreightAccountPayableConceptId Select apc).FirstOrDefault
            res.IVAFreightAccountPayableConceptDescription = accountPayableConcepts.Code + " - " + accountPayableConcepts.Name

            accountPayableConcepts = (From apc In _context.AccountPayableConcepts.AsNoTracking Where apc.Id = res.FreightAccountPayableConceptId Select apc).FirstOrDefault
            res.FreightAccountPayableConceptDescription = accountPayableConcepts.Code + " - " + accountPayableConcepts.Name

            If res.ProDevelopmentAccountPayableConceptId IsNot Nothing Then
                accountPayableConcepts = (From apc In _context.AccountPayableConcepts.AsNoTracking Where apc.Id = res.ProDevelopmentAccountPayableConceptId Select apc).FirstOrDefault
                res.ProDevelopmentAccountPayableConceptDescription = accountPayableConcepts.Code + " - " + accountPayableConcepts.Name
            End If

            If res.ProElectrificationAccountPayableConceptId IsNot Nothing Then
                accountPayableConcepts = (From apc In _context.AccountPayableConcepts.AsNoTracking Where apc.Id = res.ProElectrificationAccountPayableConceptId Select apc).FirstOrDefault
                res.ProElectrificationAccountPayableConceptDescription = accountPayableConcepts.Code + " - " + accountPayableConcepts.Name
            End If

            If res.ProCultureAccountPayableConceptId IsNot Nothing Then
                accountPayableConcepts = (From apc In _context.AccountPayableConcepts.AsNoTracking Where apc.Id = res.ProCultureAccountPayableConceptId Select apc).FirstOrDefault
                res.ProCultureAccountPayableConceptDescription = accountPayableConcepts.Code + " - " + accountPayableConcepts.Name
            End If

            If res.ProHospitalAccountPayableConceptId IsNot Nothing Then
                accountPayableConcepts = (From apc In _context.AccountPayableConcepts.AsNoTracking Where apc.Id = res.ProHospitalAccountPayableConceptId Select apc).FirstOrDefault
                res.ProHospitalAccountPayableConceptDescription = accountPayableConcepts.Code + " - " + accountPayableConcepts.Name
            End If

            If res.ProGameAccountPayableConceptId1 IsNot Nothing Then
                accountPayableConcepts = (From apc In _context.AccountPayableConcepts.AsNoTracking Where apc.Id = res.ProGameAccountPayableConceptId1 Select apc).FirstOrDefault
                res.ProGameAccountPayableConceptDescription = accountPayableConcepts.Code + " - " + accountPayableConcepts.Name
            End If

            If res.IVARetentionAccountPayableConceptId IsNot Nothing Then
                accountPayableConcepts = (From apc In _context.AccountPayableConcepts.AsNoTracking Where apc.Id = res.IVARetentionAccountPayableConceptId Select apc).FirstOrDefault
                res.IVARetentionAccountPayableConceptDescription = accountPayableConcepts.Code + " - " + accountPayableConcepts.Name
            End If

            accountPayableConcepts = (From apc In _context.AccountPayableConcepts.AsNoTracking Where apc.Id = res.IVAAccountPayableConceptId Select apc).FirstOrDefault
            res.IVAAccountPayableConceptDescription = accountPayableConcepts.Code + " - " + accountPayableConcepts.Name

            accountPayableConcepts = (From apc In _context.AccountPayableConcepts.AsNoTracking Where apc.Id = res.AdjustmentAccountPayableConceptId Select apc).FirstOrDefault
            res.AdjustmentAccountPayableConceptDescription = accountPayableConcepts.Code + " - " + accountPayableConcepts.Name

            'Unidad Radicacion
            Dim filingUnit As FilingUnit = (From fu In _context.FilingUnit.AsNoTracking Where fu.Id = res.FilingUnitId Select fu).FirstOrDefault
            res.FilingUnitDescription = filingUnit.Code + " - " + filingUnit.Name

            'Cuentas Contables
            Dim mainAccount As MainAccounts

            mainAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.IVAGeneratedMainAccountId Select ma).FirstOrDefault
            res.IVAGeneratedMainAccountDescription = mainAccount.Number + " - " + mainAccount.Name

            mainAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.DiscountSalesMainAccountId Select ma).FirstOrDefault
            res.DiscountSalesMainAccountDescription = mainAccount.Number + " - " + mainAccount.Name


            'Se recorre loas detalle para agregarle las propiedades manuales
            If res.SettingInventoryFunctionalUnit IsNot Nothing AndAlso res.SettingInventoryFunctionalUnit.Count > 0 Then
                For Each itemDetail As SettingInventoryFunctionalUnit In res.SettingInventoryFunctionalUnit

                    Dim functionalUnit = (From fu In _context.FunctionalUnit.AsNoTracking Where fu.Id = itemDetail.FunctionalUnitId Select fu).FirstOrDefault
                    itemDetail.FunctionalUnitDescription = functionalUnit.Code + " - " + functionalUnit.Name

                    Dim costAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = itemDetail.CostAccountId Select ma).FirstOrDefault
                    itemDetail.CostAccountDescription = costAccount.Number + " - " + costAccount.Name

                    Dim salesAccount = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = itemDetail.SalesAccountId Select ma).FirstOrDefault
                    itemDetail.SalesAccountDescription = salesAccount.Number + " - " + salesAccount.Name
                Next
            End If

            Dim accountpayableConceptsNote = (From apc In _context.AccountPayableConceptNotes.AsNoTracking Where apc.Id = res.RefundAccountPayableConceptNoteId Select apc).FirstOrDefault
            res.RefundAccountPayableConceptNoteDescription = accountpayableConceptsNote.Code + " - " + accountpayableConceptsNote.Name

            Dim adjustmentConcept As AdjustmentConcept

            adjustmentConcept = (From ac In _context.AdjustmentConcept.AsNoTracking Where ac.Id = res.InputAdjustmentConceptId Select ac).FirstOrDefault
            res.InputAdjustmentConceptIdDescription = adjustmentConcept.Code + " - " + adjustmentConcept.Name

            adjustmentConcept = (From ac In _context.AdjustmentConcept.AsNoTracking Where ac.Id = res.OutputAdjustmentConceptId Select ac).FirstOrDefault
            res.OutputAdjustmentConceptIdDescription = adjustmentConcept.Code + " - " + adjustmentConcept.Name

            res.NitNameTransferOrderThirdParty = (From tp In _context.ThirdParty.AsNoTracking() Where tp.Id = res.TransferOrderThirdPartyId Select String.Concat(tp.Nit, " - ", tp.Name)).FirstOrDefault()

            If res.PharmaceuticalDispensingThirdPartyId IsNot Nothing Then
                res.NitNamePharmaceuticalDispensingThirdParty = (From tp In _context.ThirdParty.AsNoTracking() Where tp.Id = res.PharmaceuticalDispensingThirdPartyId Select String.Concat(tp.Nit, " - ", tp.Name)).FirstOrDefault()
            End If

            If res.MainWarehouseId IsNot Nothing Then
                Dim warehouse = (From wh In _context.Warehouse.AsNoTracking() Where wh.Id = res.MainWarehouseId Select wh).FirstOrDefault()
                res.MainWarehouseCodeName = $"{warehouse.Code} - {warehouse.Name}"
            End If

            res.OriginalValue = (From si In _context.SettingInventory.AsNoTracking Select si).FirstOrDefault

                Return res
            Else
                Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Realiza el proceso de cierre de inventario
    ''' </summary>
    ''' <param name="MonthClosed"></param>
    ''' <param name="YearClosed"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ClosedMonthInventory(MonthClosed As Integer, YearClosed As Integer, CodeUser As String) As SP_ClosedMonthInventory_Result Implements ISettingInventoryRepository.SP_ClosedMonthInventory
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ClosedMonthInventory(MonthClosed, YearClosed, CodeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Retorna la lista de los documentos de inventario que estan sin confirmar
    ''' </summary>
    ''' <param name="MonthClosed"></param>
    ''' <param name="YearClosed"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_VerifiyHasConfirmAllDocuments(MonthClosed As Integer, YearClosed As Integer) As List(Of SP_VerifiyHasConfirmAllDocuments_Result) Implements ISettingInventoryRepository.SP_VerifiyHasConfirmAllDocuments
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim Documents = (From e In _context.SP_VerifiyHasConfirmAllDocuments(MonthClosed, YearClosed)
                         Select e).ToList
        If Documents.Count > 0 Then
            Return Documents
        Else
            Return New List(Of SP_VerifiyHasConfirmAllDocuments_Result)
        End If
    End Function

    ''' <summary>
    ''' Retorna la conciliación de los modulos de inventario y contabilidad
    ''' </summary>
    ''' <param name="MonthClosed"></param>
    ''' <param name="YearClosed"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ConciliationInventoryVsAccounting(MonthClosed As Integer, YearClosed As Integer) As Task(Of List(Of SP_ConciliationInventoryVsAccounting_Result)) Implements ISettingInventoryRepository.SP_ConciliationInventoryVsAccounting
        Dim currentContainer = ServerSessionValues.Current.CurrentContainer
        Return Task.Factory.StartNew(Function() As List(Of SP_ConciliationInventoryVsAccounting_Result)
                                         Using context1 As New GlobalModelUnitOfWork(currentContainer)

                                             DirectCast(context1, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600

                                             Return (From e In context1.SP_ConciliationInventoryVsAccounting(MonthClosed, YearClosed)
                                                     Select e).ToList()
                                         End Using
                                     End Function)
    End Function

    ''' <summary>
    ''' Retorna la variacion del costo promedio de los productos
    ''' </summary>
    ''' <param name="MonthClosed"></param>
    ''' <param name="YearClosed"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ClosedMonthVariationCost(MonthClosed As Integer, YearClosed As Integer) As List(Of SP_ClosedMonthVariationCost_Result) Implements ISettingInventoryRepository.SP_ClosedMonthVariationCost
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim Documents = (From e In _context.SP_ClosedMonthVariationCost(MonthClosed, YearClosed)
                         Select e).ToList
        If Documents.Count > 0 Then
            Return Documents
        Else
            Return New List(Of SP_ClosedMonthVariationCost_Result)
        End If
    End Function

    ''' <summary>
    ''' obtiene una configuracion 
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSettingInventoryByOperatingUnitId(OperatingUnitId As Integer) As SettingInventory Implements ISettingInventoryRepository.GetSettingInventoryByOperatingUnitId
        Dim res = (From si In _context.SettingInventory.AsNoTracking() Where si.OperatingUnitId = OperatingUnitId Select si).FirstOrDefault()
        If res IsNot Nothing Then

            Dim journalVoucherType As JournalVoucherTypes

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.OrderDispatchJournalVoucherTypeId Select jvt).FirstOrDefault
            res.OrderDispatchJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            journalVoucherType = (From jvt In _context.JournalVoucherTypes.AsNoTracking Where jvt.Id = res.OrderDispatchReturnJournalVoucherTypeId Select jvt).FirstOrDefault
            res.OrderDispatchReturnJournalVoucherTypeDescription = journalVoucherType.Code + " - " + journalVoucherType.Name

            ' Ajuste de inventario
            ''Entrada
            Dim ConceptsAdjustmentIn = (From ca In _context.AdjustmentConcept.AsNoTracking().Include("MainAccounts").AsNoTracking() Where ca.Id = res.InputAdjustmentConceptId Select ca).FirstOrDefault()
            res.AdjustmentInAccountId = ConceptsAdjustmentIn.AdjustmentAccountId
            res.AdjustmentInHandlessCostCenter = ConceptsAdjustmentIn.MainAccounts.HandlesCostCenter
            res.AdjustmentInCostCenterId = ConceptsAdjustmentIn.CostCenterId
            ''Salida
            Dim ConceptsAdjustmentOut = (From ca In _context.AdjustmentConcept.AsNoTracking().Include("MainAccounts").AsNoTracking() Where ca.Id = res.OutputAdjustmentConceptId Select ca).FirstOrDefault()
            res.AdjustmentOutAccountId = ConceptsAdjustmentOut.AdjustmentAccountId
            res.AdjustmentOutHandlessCostCenter = ConceptsAdjustmentOut.MainAccounts.HandlesCostCenter
            res.AdjustmentOutCostCenterId = ConceptsAdjustmentOut.CostCenterId

            Return res
        Else
            Return New SettingInventory
        End If

    End Function

    ''' <summary>
    ''' obtiene el reporte de valorizacion de inventario
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="range"></param>
    ''' <returns></returns>
    Public Function GetSP_ReportValuedInventory(filters As String, range As String) As List(Of SP_ReportValuedInventory_Result) Implements ISettingInventoryRepository.GetSP_ReportValuedInventory
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ReportValuedInventory(filters, range).ToList()
    End Function

    Public Function SP_GetMonthlyClosureSummary(YearClosed As Integer, MonthClosed As Integer) As List(Of SP_GetMonthlyClosureSummary_Result) Implements ISettingInventoryRepository.SP_GetMonthlyClosureSummary
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return (From e In _context.SP_GetMonthlyClosureSummary(YearClosed, MonthClosed)
                Select e).ToList
    End Function

    ''' <summary>
    ''' Obtiene informacion de cierre por periodo
    ''' </summary>
    ''' <param name="yearClosed"></param>
    ''' <param name="monthClosed"></param>
    ''' <returns></returns>
    Public Function GetMonthlyClosed(yearClosed As Integer, monthClosed As Integer) As ClosedMonthInventoryHeader Implements ISettingInventoryRepository.GetMonthlyClosed
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Dim closedMonth = (From e In _context.ClosedMonthInventoryHeader
                           Where e.Year = yearClosed AndAlso e.Month = monthClosed
                           Select e).FirstOrDefault()
        If closedMonth IsNot Nothing Then
            Return closedMonth
        Else
            Return New ClosedMonthInventoryHeader()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el primer registro de parámetros de inventario
    ''' * ESTE MÉTODO SE USA ÚNICAMENTE PARA INTEGRACIÓN, DEBIDO A QUE NO SE ENVIAN DATOS DESDE EL CLIENTE Y NO SE PUEDE OBTENER LA UNIDAD OPERATIVA*
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFirstOrDefaultSettingInventory() As SettingInventory Implements ISettingInventoryRepository.GetFirstOrDefaultSettingInventory
        Dim res = (From si In _context.SettingInventory.AsNoTracking() Select si).FirstOrDefault()

        If res IsNot Nothing Then
            Return res
        Else
            Return Nothing
        End If
    End Function
End Class
