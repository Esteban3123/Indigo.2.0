
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los grupos de atención definidos en los contratos con entidades pagadoras, enriquecidos con el tercero (aseguradora, EPS u otra entidad) asociado a través de las cuentas por cobrar de cartera. Sirve como fuente de datos para el proceso de alistamiento de cartera, permitiendo identificar para cada grupo de atención su tipo de liquidación, período de facturación, topes individuales y por período, plantillas de requisitos y procedimientos, tarifas, conceptos a facturar, y si requiere autorización o aplica RIAS. Es utilizada principalmente en reportería y configuración de facturación y cobro por grupo de atención contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewContractCareGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewContractCareGroup';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los grupos de atención de un contrato junto con el tercero asociado vía cuentas por cobrar, para alimentar el alistamiento de cartera.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewContractCareGroup';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Usa LEFT JOIN, por lo que un CareGroup sin AccountReceivable o sin ThirdParty asociado se devuelve con ThirdPartyId nulo; El GROUP BY sobre todas las columnas garantiza que no se dupliquen filas por múltiples cuentas por cobrar del mismo tercero; Lecturas con NOLOCK: tolera lecturas sucias para reportes de alistamiento de cartera', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewContractCareGroup';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo de atención; Alistamiento de cartera; Cuenta por cobrar; Tercero; Contrato; Liquidación; Periodo de facturación; Tarifa de producto; RIAS; Autorización', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewContractCareGroup';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Contract.CareGroup: Devuelve un registro por cada combinación de grupo de atención y tercero relacionado mediante Portfolio.AccountReceivable.CareGroupId', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewContractCareGroup';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Portfolio.AccountReceivable; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewContractCareGroup';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewContractCareGroup';
GO
