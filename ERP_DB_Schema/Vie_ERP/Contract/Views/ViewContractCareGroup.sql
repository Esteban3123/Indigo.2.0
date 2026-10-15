
CREATE VIEW [Contract].[ViewContractCareGroup]
--Vista para el DataSource de  grupos de atencion de Alistamiento de cartera
AS
		SELECT
				cg.Id as Id,
				cg.Code as Code,
				cg.Name as Name,
				cg.DefaultManual as DefaultManual,
				cg.CareGroupType as CareGroupType,
				cg.ContractId as ContractId,
				cg.LiquidationType as LiquidationType,
				cg.BillingPeriod as BillingPeriod,
				cg.MaximumIndividualBilling as MaximumIndividualBilling,
				cg.PeriodMaximumBilling as PeriodMaximumBilling,
				cg.RequirementsTemplateId as RequirementsTemplateId,
				cg.InvoiceDeadlines as InvoiceDeadlines,
				cg.ProcedureTemplateId as ProcedureTemplateId,
				cg.ProductRateId as ProductRateId,
				cg.ConceptToBill as ConceptToBill,
				cg.EntityType as EntityType,
				cg.Status as Status,
				cg.ApplyRIAS as ApplyRIAS,
				cg.AuthorizationRequired as AuthorizationRequired,
				th.Id as ThirdPartyId
				FROM Contract.CareGroup cg WITH(NOLOCK) 
				LEFT JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) on cg.Id = ar.CareGroupId
				LEFT JOIN Common.ThirdParty th WITH(NOLOCK) on ar.ThirdPartyId = th.Id
				GROUP by cg.Id ,cg.Code, cg.Name,cg.DefaultManual,cg.CareGroupType, cg.ContractId,cg.LiquidationType,cg.BillingPeriod,cg.MaximumIndividualBilling, cg.PeriodMaximumBilling, cg.RequirementsTemplateId, cg.InvoiceDeadlines,cg.ProcedureTemplateId,cg.ProductRateId,cg.ConceptToBill, cg.EntityType, cg.Status,cg.ApplyRIAS,cg.AuthorizationRequired,th.Id