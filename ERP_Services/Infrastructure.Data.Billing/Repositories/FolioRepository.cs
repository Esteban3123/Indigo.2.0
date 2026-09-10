using Domain.Billing.POCO;
using Domain.Billing.Repositories;
using Infrastructure.Data.Base;
using Infrastructure.Data.ModelRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Billing.Repositories
{
    public class FolioRepository : BaseRepository, IFolioRepository
    {
        private IGlobalModelUnitOfWork _context;

        public FolioRepository(IGlobalModelUnitOfWork context) : base(context) {
            _context = context;
        }

        public Folio GetFolio(long folioId)
        {
            Task<IEnumerable<FolioDetail>> cupsTask = new Task<IEnumerable<FolioDetail>>(() => GetDetailsCups(folioId));
            Task<IEnumerable<FolioDetail>> productsTask = new Task<IEnumerable<FolioDetail>>(() => GetDetailsProducts(folioId));
            cupsTask.Start();
            productsTask.Start();

            List<(String, Object)> parmas = new List<(string, object)>() { ("@idFolio", folioId) };

            IEnumerable<Folio> folios = ExecuteQueryDR<Folio>(
                @"
                    select
                        rcd.Id as RevenueControlDetailId,
                        rc.AdmissionNumber,
                        rcd.BillingAuthorizationId,
                        rcd.ThirdPartyId,
                        rcd.CareGroupId,
                        cg.EntityType as CaregroupEntityType,
                        concat(cg.Code, ' - ', cg.Name) as CareGroupCodeName,
                        concat(t.Nit, ' - ', t.Name) as ThirdPartyNitName,
                        cg.CostCenterId as CareGroupCostCenterId,
                        rcd.FolioType,
                        ISNULL(rcd.Observation, '') AS Observation,
                        rcd.TotalFolio,
                        rcd.ResponsibleRecoveryFee,
                        rcd.PatientDiscountPercentage,
                        rcd.PatientDiscount,
                        rcd.TotalPatientSalesPrice,
                        rcd.TotalPatientWithDiscount,
                        rcd.ValueVoucher as VoucherValue,
                        rcd.Status,
                        rcd.StatusFolioId,
                        CONCAT(CSS.Code,'-',CSS.Name) AS StatusFolioName,
                        rcd.LiquidationType,
                        rcd.FolioOrder,
                        cg.ContractId,
                        rcd.InvoiceCategoryId,
                        CONCAT(ic.Code, ' - ', ic.Name) AS InvoiceCategoryCodeName,
                        rc.PatientCode,
                        rcd.HealthAdministratorId,
                        CONCAT(ha.Code, ' - ', ha.Name) AS HealthAdministratorCodeName,
                        rcd.ContractEntityId,
                        tp.Id AS ThirdPartyPatientId,
                        CONCAT(THPR.Nit, ' - ', THPR.Name) AS ThirdPartyResponsibleQuotaNitName,
		                rcd.PatientQuotaResponsibleThirdPartyId AS PatientQuotaResponsibleThirdPartyId
                    from Billing.RevenueControlDetail rcd
                    inner join Billing.RevenueControl rc on rc.Id = rcd.RevenueControlId
                    inner join Common.ThirdParty t on t.Id = rcd.ThirdPartyId
                    inner join [Contract].CareGroup cg on cg.Id = rcd.CareGroupId
                    inner join Common.ThirdParty tp ON rc.PatientCode = tp.Nit
                    left join Billing.ConceptsCausesStatusFolio css on rcd.StatusFolioId = css.Id
                    left join Contract.HealthAdministrator ha on rcd.HealthAdministratorId = ha.Id
                    left join Billing.InvoiceCategories ic on rcd.InvoiceCategoryId = ic.Id
                    left join Common.ThirdParty THPR with(nolock) ON rcd.PatientQuotaResponsibleThirdPartyId = THPR.Id
                    where rcd.Id = @idFolio"
                , parmas);

            if (folios.Count() == 0)
            {
                return null;
            }

            Folio folio = folios.First();

            Task.WaitAll(cupsTask, productsTask);

            folio.FolioDetails.AddRange(cupsTask.Result);
            folio.FolioDetails.AddRange(productsTask.Result);

            return folio;
        }

        public AnnullateFolio GetAnnullateFolio(long invoiceId)
        {
            Task<IEnumerable<FolioDetail>> cupsTask = new Task<IEnumerable<FolioDetail>>(() => GetInvoiceDetailsCups(invoiceId));
            Task<IEnumerable<FolioDetail>> productsTask = new Task<IEnumerable<FolioDetail>>(() => GetInvoiceDetailsProducts(invoiceId));
            cupsTask.Start();
            productsTask.Start();

            List<(String, Object)> parmas = new List<(string, object)>() { ("@invoiceId", invoiceId) };

            IEnumerable<AnnullateFolio> folios = ExecuteQueryDR<AnnullateFolio>(
                @"
                    SELECT
                    COALESCE(BID.Id, -1) AS Id, 
                    - 1 AS RevenueControlDetailId, 
                    BI.Observation, 
                    BI.InvoiceCategoryId, 
                    COALESCE (BI.InvoiceDate, '') AS InvoiceDate, 
                    COALESCE (BI.InvoicedUser, '') AS InvoicedUser, 
                    CASE WHEN BI.InvoiceCategoryId IS NULL THEN '' ELSE CONCAT(IC.Code, ' - ', IC.Name) END  as InvoiceCategoryCodeName, 
                    BI.AdmissionNumber, 
                    - 1 AS BillingAuthorizationId, 
                    COALESCE (BI.Id, - 1) AS InvoiceId, 
                    COALESCE (BI.InvoiceNumber, '') AS InvoiceNumber, 
                    -1 AS FolioOrder, 
                    CG.CareGroupType AS FolioType, 
                    -1 AS ContractEntityId, 
                    '' AS ContractEntityCodeName, 
                    COALESCE (HA.Id, 0) AS HealthAdministratorId, 
                    CONCAT(CONCAT(COALESCE (HA.Code, ''), ' - '), COALESCE (HA.Name, '')) AS HealthAdministratorCodeName, 
                    TP.Id AS ThirdPartyId, 
                    CONCAT(CONCAT(TP.Nit, ' - '), TP.Name) AS ThirdPartyNitName, 
                    CG.Id AS CareGroupId,
                    CG.EntityType as CaregroupEntityType, 
                    CONCAT(CONCAT(CG.Code, ' - '), CG.Name) AS CareGroupCodeName, 
                    BI.TotalInvoice as TotalFolio, 
                    BI.ResponsibleRecoveryFee,
                    BI.PatientDiscountPercentage, 
                    BI.PatientDiscount,  
                    COALESCE (BI.TotalPatientSalesPrice, 0) as TotalPatientSalesPrice,
                    COALESCE (BI.TotalPatientWithDiscount, 0) as TotalPatientWithDiscount, 
                    COALESCE (BI.ValueVoucher, 0) as VoucherValue,
                    BI.Status, 
                    THP.Id as ThirdPartyPatientId
                    FROM Billing.Invoice BI with (nolock)
                    INNER JOIN	Billing.InvoiceDetail BID with (nolock) ON BID.InvoiceId = BI.Id 
                    INNER JOIN Common.ThirdParty TP with (nolock) ON BI.ThirdPartyId = TP.Id 
                    LEFT JOIN Billing.InvoiceCategories IC with (nolock) ON BI.InvoiceCategoryId = IC.Id 
                    LEFT JOIN Contract.CareGroup CG with (nolock) ON BI.CareGroupId = CG.Id 
                    LEFT JOIN Contract.HealthAdministrator HA with (nolock) ON BI.HealthAdministratorId = HA.Id 
                    LEFT JOIN Common.ThirdParty THP with (nolock) ON BI.PatientCode = THP.Nit
                    WHERE BI.Status = 2 and BI.Id = @invoiceId"
                , parmas);
            if (folios.Count() == 0)
            {
                return null;
            }

            AnnullateFolio folio = folios.First();

            Task.WaitAll(cupsTask, productsTask);

            folio.FolioDetails.AddRange(cupsTask.Result);
            folio.FolioDetails.AddRange(productsTask.Result);

            return folio;
        }

        private IEnumerable<FolioDetail> GetInvoiceDetailsCups(long invoiceId)
        {
            List<(String, Object)> parmas = new List<(string, object)>() { ("@invoiceId", invoiceId) };
            return ExecuteQueryDR<FolioDetail>(
                @"
                select 
                    sodd.Id,
                    sodd.ServiceOrderDetailId,
                    so.Code as ServiceOrderCode,
                    sodd.ServiceOrderDetailId,
                    sod.IPSServiceId,
                    sod.CUPSEntityId,
                    sod.CUPSEntityContractDescriptionId,
                    sod.SurgeryNumber,
                    CONCAT(fu.Code, ' - ', fu.Name) as PerformsFunctionalUnitCodeName,
                    CONCAT(cc.Code, ' - ', cc.Name) as CostCenterCodeName,
                    coalesce(sod.AuthorizationNumber, '0') as AuthorizationNumber,
                    sod.PerformsHealthProfessionalCode,
                    isnull(sod.PerformsProfessionalSpecialty, '-1') as PerformsProfessionalSpecialty,
                    sod.ServiceDate,
                    sod.RecordType,
                    sod.Presentation,
                    sod.SettlementType,
                    sodd.DistributionType,
                    sodd.Quantity as InvoicedQuantity,
                    sod.SupplyQuantity,
                    sod.DevolutionQuantity,
                    sod.CostValue,
                    sod.RateManualSalePrice,
                    sodd.GrandTotalSalesPrice,
                    sod.SubTotalSalesPrice,
                    sod.ThirdPartyDiscount,
                    sodd.GrandTotalDiscount,
                    sod.ThirdPartyDiscountPercentage,
                    sod.TotalSalesPrice,
                    sodd.ThirdPartySalesPrice,
                    sodd.ThirdPartyPercentage,
                    IIF(sod.SettlementType = 3, 'Si', 'No') as SurchargeApply,
                    sodd.RecoveryFeeType,
                    sodd.ApplyRecoveryFee,
                    sodd.SubTotalPatientSalesPrice,
                    sod.IsPackage,
                    sod.CodeAssociateService,
                    isnull(sod.CodeAssociateService, '') as GuidHomologation,
                    isnull(sod.ApplyRIAS, 0) as ApplyRIAS,
                    isnull(sod.RIASCupsId, 0) as RIASCupsId,
                    isnull(drd.AllowValueChange, 0) as AllowValueChange,
                    CONCAT(bc.Code, ' - ', bc.Name) as IPSServiceGroupCodeName,
                    ips.Code as ServiceCode,
                    ips.Name as ServiceName,
                    ips.Code as IPSServiceCode,
                    ips.Name as IPSServiceName,
                    concat(cdesc.Code, ' - ', cdesc.Name) as ContractDescriptionCodeName,
                    CONCAT(ISNULL(ISNULL(cupsbg.Code, riasbg.Code), bg.Code), ' - ', ISNULL(ISNULL(cupsbg.Name, riasbg.Name), bg.Name)) AS ServiceBillingGroupCodeName,
                    CONCAT(ce.Code, ' - ', ce.Description) as CUPS
                from 
                Billing.Invoice i
                inner join Billing.InvoiceDetail ind on ind.InvoiceId = i.Id
                inner join Billing.ServiceOrderDetail sod on sod.Id = ind.ServiceOrderDetailId
                inner join Billing.ServiceOrderDetailDistribution sodd on sodd.ServiceOrderDetailId = sod.Id
                inner join Billing.ServiceOrder so on so.Id = sod.ServiceOrderId
                inner join Payroll.FunctionalUnit fu on fu.Id = sod.PerformsFunctionalUnitId
                inner join Payroll.CostCenter cc on cc.Id = sod.CostCenterId
                inner join Contract.IPSService ips on ips.Id = sod.IPSServiceId
                inner join Contract.CUPSEntity ce on ce.Id = sod.CUPSEntityId
                inner join Billing.BillingGroup bg on bg.Id = ce.BillingGroupId
                left join Contract.DefinitionRateDetail drd on drd.Id = sod.DefinitionRateDetailId
                left join Billing.BillingGroup riasbg on riasbg.Id = ce.BillingGroupId
                left join Billing.BillingConcept bc on bc.Id = sod.BillingConceptId
                left join Contract.CUPSEntityContractDescriptions cecd on cecd.Id = sod.CUPSEntityContractDescriptionId
                left join Contract.ContractDescriptions cdesc on cdesc.Id = cecd.ContractDescriptionId
                left join Billing.BillingGroup cupsbg on cupsbg.Id = cecd.BillingGroupId
                where i.Id = @invoiceId"
                , parmas);
        }

        private IEnumerable<FolioDetail> GetInvoiceDetailsProducts(long invoiceId)
        {
            List<(String, Object)> parmas = new List<(string, object)>() { ("@invoiceId", invoiceId) };
            return ExecuteQueryDR<FolioDetail>(
                @"
                select
                sodd.Id,
                sodd.ServiceOrderDetailId,
                so.Code as ServiceOrderCode,
                sodd.ServiceOrderDetailId,
                sod.IPSServiceId,
                sod.CUPSEntityId,
                sod.CUPSEntityContractDescriptionId,
                sod.SurgeryNumber,
                CONCAT(fu.Code, ' - ', fu.Name) as PerformsFunctionalUnitCodeName,
                CONCAT(cc.Code, ' - ', cc.Name) as CostCenterCodeName,
                coalesce(sod.AuthorizationNumber, '0') as AuthorizationNumber,
                sod.PerformsHealthProfessionalCode,
                isnull(sod.PerformsProfessionalSpecialty, '-1') as PerformsProfessionalSpecialty,
                sod.ServiceDate,
                sod.RecordType,
                sod.Presentation,
                sod.SettlementType,
                sodd.DistributionType,
                sodd.Quantity as InvoicedQuantity,
                sod.SupplyQuantity,
                sod.DevolutionQuantity,
                sod.CostValue,
                sod.RateManualSalePrice,
                sodd.GrandTotalSalesPrice,
                sod.SubTotalSalesPrice,
                sod.ThirdPartyDiscount,
                sodd.GrandTotalDiscount,
                sod.ThirdPartyDiscountPercentage,
                sod.TotalSalesPrice,
                sodd.ThirdPartySalesPrice,
                sodd.ThirdPartyPercentage,
                IIF(sod.SettlementType = 3, 'Si', 'No') as SurchargeApply,
                sodd.RecoveryFeeType,
                sodd.ApplyRecoveryFee,
                sodd.SubTotalPatientSalesPrice,
                sod.IsPackage,
                sod.CodeAssociateService,
                isnull(sod.CodeAssociateService, '') as GuidHomologation,
                isnull(sod.ApplyRIAS, 0) as ApplyRIAS,
                isnull(sod.RIASCupsId, 0) as RIASCupsId,
                0 as AllowValueChange,
                CONCAT(bc.Code, ' - ', bc.Name) as IPSServiceGroupCodeName,
                inp.Code as ServiceCode,
                inp.Name as ServiceName,
                inp.Id as ProductId,
                inp.Code as ProductCode,
                inp.Name as ProductName,
                isnull(inp.POSProduct, 0) as IsPOSProduct,
                CONCAT(bg.Code, ' - ', bg.Name) as ServiceBillingGroupCodeName,
                CONCAT(bg.Code, ' - ', bg.Name) as ProductBillingGroupCodeName,
                ISNULL(atc.Code, '') as ProductATCCode,
                cast(isnull(pos.HasPathologies, 0) as bit) as HasPathologies
                from Billing.Invoice i
                inner join Billing.InvoiceDetail ind on ind.InvoiceId = i.Id
				inner join Billing.ServiceOrderDetail sod on sod.Id = ind.ServiceOrderDetailId
				inner join Billing.ServiceOrderDetailDistribution sodd on sodd.ServiceOrderDetailId = sod.Id
                inner join Billing.ServiceOrder so on so.Id = sod.ServiceOrderId
                inner join Payroll.FunctionalUnit fu on fu.Id = sod.PerformsFunctionalUnitId
                inner join Payroll.CostCenter cc on cc.Id = sod.CostCenterId
                inner join Inventory.InventoryProduct inp on inp.Id = sod.ProductId
                left join Billing.BillingGroup bg on bg.Id = inp.BillingGroupNoPosId
                left join Inventory.ATC atc on atc.Id = inp.ATCId
                left join Billing.BillingConcept bc on bc.Id = sod.BillingConceptId
                left join (
	                SELECT PP.ProductId, 1 as HasPathologies
	                FROM Inventory.POSPathologies PP
	                WHERE PP.ProductId IS NOT NULL
                ) pos on pos.ProductId = inp.Id
                where sodd.RevenueControlDetailId = @invoiceId"
                , parmas);
        }

        private IEnumerable<FolioDetail> GetDetailsCups(long invoiceId)
        {
            List<(String, Object)> parmas = new List<(string, object)>() { ("@idFolio", invoiceId) };
            return ExecuteQueryDR<FolioDetail>(
                @"
                select 
                sodd.Id,
                sodd.ServiceOrderDetailId,
                so.Code as ServiceOrderCode,
                sodd.ServiceOrderDetailId,
                sod.IPSServiceId,
                sod.CUPSEntityId,
                sod.CUPSEntityContractDescriptionId,
                sod.SurgeryNumber,
                CONCAT(fu.Code, ' - ', fu.Name) as PerformsFunctionalUnitCodeName,
                CONCAT(cc.Code, ' - ', cc.Name) as CostCenterCodeName,
                coalesce(sod.AuthorizationNumber, '0') as AuthorizationNumber,
                sod.PerformsHealthProfessionalCode,
                isnull(sod.PerformsProfessionalSpecialty, '-1') as PerformsProfessionalSpecialty,
                sod.ServiceDate,
                sod.RecordType,
                sod.Presentation,
                sod.SettlementType,
                sodd.DistributionType,
                sodd.Quantity as InvoicedQuantity,
                sod.SupplyQuantity,
                sod.DevolutionQuantity,
                sod.CostValue,
                sod.RateManualSalePrice,
                sodd.GrandTotalSalesPrice,
                sod.SubTotalSalesPrice,
                sod.ThirdPartyDiscount,
                sodd.GrandTotalDiscount,
                sod.ThirdPartyDiscountPercentage,
                sod.TotalSalesPrice,
                sodd.ThirdPartySalesPrice,
                sodd.ThirdPartyPercentage,
                IIF(sod.SettlementType = 3, 'Si', 'No') as SurchargeApply,
                sodd.RecoveryFeeType,
                sodd.ApplyRecoveryFee,
                sodd.SubTotalPatientSalesPrice,
                sod.IsPackage,
                sod.CodeAssociateService,
                isnull(sod.CodeAssociateService, '') as GuidHomologation,
                isnull(sod.ApplyRIAS, 0) as ApplyRIAS,
                isnull(sod.RIASCupsId, 0) as RIASCupsId,
                isnull(drd.AllowValueChange, 0) as AllowValueChange,
                CONCAT(bc.Code, ' - ', bc.Name) as IPSServiceGroupCodeName,
                ips.Code as ServiceCode,
                ips.Name as ServiceName,
                ips.Code as IPSServiceCode,
                ips.Name as IPSServiceName,
                concat(cdesc.Code, ' - ', cdesc.Name) as ContractDescriptionCodeName,
                CONCAT(ISNULL(ISNULL(cupsbg.Code, riasbg.Code), bg.Code), ' - ', ISNULL(ISNULL(cupsbg.Name, riasbg.Name), bg.Name)) AS ServiceBillingGroupCodeName,
                CONCAT(ce.Code, ' - ', ce.Description) as CUPS,
                ISNULL(SODD.DistributionType, 1) as IconType,
                sod.ProductLiquidationType
                from Billing.ServiceOrderDetailDistribution sodd
                inner join Billing.ServiceOrderDetail sod on sod.Id = sodd.ServiceOrderDetailId
                inner join Billing.ServiceOrder so on so.Id = sod.ServiceOrderId
                inner join Payroll.FunctionalUnit fu on fu.Id = sod.PerformsFunctionalUnitId
                inner join Payroll.CostCenter cc on cc.Id = sod.CostCenterId
                inner join Contract.IPSService ips on ips.Id = sod.IPSServiceId
                inner join Contract.CUPSEntity ce on ce.Id = sod.CUPSEntityId
                inner join Billing.BillingGroup bg on bg.Id = ce.BillingGroupId
                left join Contract.DefinitionRateDetail drd on drd.Id = sod.DefinitionRateDetailId
                left join Billing.BillingGroup riasbg on riasbg.Id = ce.BillingGroupId
                left join Billing.BillingConcept bc on bc.Id = sod.BillingConceptId
                left join Contract.CUPSEntityContractDescriptions cecd on cecd.Id = sod.CUPSEntityContractDescriptionId
                left join Contract.ContractDescriptions cdesc on cdesc.Id = cecd.ContractDescriptionId
                left join Billing.BillingGroup cupsbg on cupsbg.Id = cecd.BillingGroupId
                where sodd.RevenueControlDetailId = @idFolio"
                , parmas);
        }

        private IEnumerable<FolioDetail> GetDetailsProducts(long idFolio)
        {
            List<(String, Object)> parmas = new List<(string, object)>() { ("@idFolio", idFolio) };
            return ExecuteQueryDR<FolioDetail>(
                @"
                select
                sodd.Id,
                sodd.ServiceOrderDetailId,
                so.Code as ServiceOrderCode,
                sodd.ServiceOrderDetailId,
                sod.IPSServiceId,
                sod.CUPSEntityId,
                sod.CUPSEntityContractDescriptionId,
                sod.SurgeryNumber,
                CONCAT(fu.Code, ' - ', fu.Name) as PerformsFunctionalUnitCodeName,
                CONCAT(cc.Code, ' - ', cc.Name) as CostCenterCodeName,
                coalesce(sod.AuthorizationNumber, '0') as AuthorizationNumber,
                sod.PerformsHealthProfessionalCode,
                isnull(sod.PerformsProfessionalSpecialty, '-1') as PerformsProfessionalSpecialty,
                sod.ServiceDate,
                sod.RecordType,
                sod.Presentation,
                sod.SettlementType,
                sodd.DistributionType,
                sodd.Quantity as InvoicedQuantity,
                sod.SupplyQuantity,
                sod.DevolutionQuantity,
                sod.CostValue,
                sod.RateManualSalePrice,
                sodd.GrandTotalSalesPrice,
                sod.SubTotalSalesPrice,
                sod.ThirdPartyDiscount,
                sodd.GrandTotalDiscount,
                sod.ThirdPartyDiscountPercentage,
                sod.TotalSalesPrice,
                sodd.ThirdPartySalesPrice,
                sodd.ThirdPartyPercentage,
                IIF(sod.SettlementType = 3, 'Si', 'No') as SurchargeApply,
                sodd.RecoveryFeeType,
                sodd.ApplyRecoveryFee,
                sodd.SubTotalPatientSalesPrice,
                sod.IsPackage,
                sod.CodeAssociateService,
                isnull(sod.CodeAssociateService, '') as GuidHomologation,
                isnull(sod.ApplyRIAS, 0) as ApplyRIAS,
                isnull(sod.RIASCupsId, 0) as RIASCupsId,
                0 as AllowValueChange,
                CONCAT(bc.Code, ' - ', bc.Name) as IPSServiceGroupCodeName,
                inp.Code as ServiceCode,
                inp.Name as ServiceName,
                inp.Id as ProductId,
                inp.Code as ProductCode,
                inp.Name as ProductName,
                isnull(inp.POSProduct, 0) as IsPOSProduct,
                CONCAT(bg.Code, ' - ', bg.Name) as ServiceBillingGroupCodeName,
                CONCAT(bg.Code, ' - ', bg.Name) as ProductBillingGroupCodeName,
                ISNULL(atc.Code, '') as ProductATCCode,
                cast(isnull(pos.HasPathologies, 0) as bit) as HasPathologies,
                SUBSTRING((SELECT ',' + code  AS [text()] FROM Billing.MipresCode mc WHERE mc.ServiceOrderDetailId = sod.Id FOR XML PATH ('')), 2, 1000) AS Mipres,
                Case When pt.Class = 5 Then 1 Else 0 End As IsItemProduction,
		        Case When pt.Class = 5 then 99 else ISNULL(sodd.DistributionType, 1) end as IconType,
                ISNULL((SELECT top 1 1 from Billing.ProductServiceDetail psd WITH(NOLOCK) WHERE psd.ServiceOrderDetailId = sod.Id),0) FlagProductServiceDetail,
		        sod.ProductLiquidationType
                from Billing.ServiceOrderDetailDistribution sodd
                inner join Billing.ServiceOrderDetail sod on sod.Id = sodd.ServiceOrderDetailId
                inner join Billing.ServiceOrder so on so.Id = sod.ServiceOrderId
                inner join Payroll.FunctionalUnit fu on fu.Id = sod.PerformsFunctionalUnitId
                inner join Payroll.CostCenter cc on cc.Id = sod.CostCenterId
                inner join Inventory.InventoryProduct inp on inp.Id = sod.ProductId
                inner join Inventory.ProductType pt on inp.ProductTypeId = pt.Id
                left join Billing.BillingGroup bg on bg.Id = inp.BillingGroupId
                left join Inventory.ATC atc on atc.Id = inp.ATCId
                left join Billing.BillingConcept bc on bc.Id = sod.BillingConceptId
                left join (
	                SELECT PP.ProductId, 1 as HasPathologies
	                FROM Inventory.POSPathologies PP
	                WHERE PP.ProductId IS NOT NULL
                ) pos on pos.ProductId = inp.Id
                where sodd.RevenueControlDetailId = @idFolio"
                , parmas);
        }

        /// <summary>
        /// Metodo que obtiene la informacion de la cabecera y detalle de la pre-factura
        /// </summary>
        /// <param name="folioId"></param>
        /// <returns></returns>
        public InvoicePartialMasterAccount GetVReportInvoicePartial(long folioId)
        {
            Task<IEnumerable<InvoicePartialDetailMasterAccount>> detailTask = new Task<IEnumerable<InvoicePartialDetailMasterAccount>>(() => GetDetailsVReportInvoicePartial(folioId));
            detailTask.Start();

            List<(String, Object)> parmas = new List<(string, object)>() { ("@idFolio", folioId) };

            IEnumerable<InvoicePartialMasterAccount> PartialMasterAccounts = ExecuteQueryDR<InvoicePartialMasterAccount>(
                @"
                    select  *,
                            c.Id as CurrencyId,
                            c.Abbreviation as CurrencyAbbreviation,
                            iso.CurrencyName
                    FROM [Billing].[VReportInvoicePartial] v 
                    JOIN GeneralLedger.CompanySettings cs WITH(NOLOCK) on 1=1
                    JOIN Common.Currency c WITH(NOLOCK) on cs.OfficialCurrencyId =c.Id
                    JOIN Common.ISO4217 iso WITH(NOLOCK) on c.ISO4217Id=iso.Id
                    where v.Id = @idFolio"
                , parmas);

            if (PartialMasterAccounts is null || !PartialMasterAccounts.Any())
            {
                return null;
            }

            InvoicePartialMasterAccount invoicePartialMasterAccount = PartialMasterAccounts.First();

            Task.WaitAll(detailTask);

            invoicePartialMasterAccount.InvoicePartialDetail.AddRange(detailTask.Result);

            return invoicePartialMasterAccount;
        }

        /// <summary>
        /// funcion que obtiene los detalles de la prefactura
        /// </summary>
        /// <param name="folioId"></param>
        /// <returns></returns>
        private IEnumerable<InvoicePartialDetailMasterAccount> GetDetailsVReportInvoicePartial(long folioId)
        {
            List<(String, Object)> parmas = new List<(string, object)>() { ("@folioId", folioId) };
            return ExecuteQueryDR<InvoicePartialDetailMasterAccount>(
                @"
                select *              
                FROM [Billing].[VReportInvoicePartialDetail] vd                
                where vd.RevenueControlDetailId = @folioId"
                , parmas);
        }

    }
}
