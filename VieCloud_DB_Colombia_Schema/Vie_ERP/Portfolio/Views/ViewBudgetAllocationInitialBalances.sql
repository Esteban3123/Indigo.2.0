

CREATE VIEW [Portfolio].[ViewBudgetAllocationInitialBalances]
AS
select ar.Id as Id, ar.PortfolioInitialBalanceId as PortfolioInitialBalanceId,ar.InvoiceNumber as InvoiceNumber , CONCAT(tp.nit,' - ' ,tp.Name) as NitNameThird ,ar.AccountReceivableDate as AccountReceivableDate , CONCAT(ma.Number,' - ' ,ma.Name) as NumberNameAccount ,ara.Value as Value   
from Portfolio.PortfolioInitialBalanceAccountReceivable ar with (nolock)
inner join Common.ThirdParty as tp with (nolock) on tp.Id = ar.ThirdPartyId 
inner join Portfolio.PortfolioInitialBalanceAccountReceivableAccounting as ara with (nolock) on ara.PortfolioInitialBalanceAccountReceivableId =ar.id 
inner join GeneralLedger.MainAccounts as ma  with (nolock) on ara.MainAccountId = ma.Id 
where ar.AffectBudget = 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los saldos iniciales de cartera en cuentas por cobrar que NO afectan presupuesto (AffectBudget = 0), combinando el documento de cobro pendiente (factura) con el tercero deudor (NIT y nombre de la aseguradora, empresa o entidad) y la cuenta contable principal asociada (número y nombre de cuenta del plan de cuentas). Sirve para la asignación y revisión presupuestal de los saldos iniciales de cartera, permitiendo identificar qué facturas del saldo inicial están contabilizadas sin impacto presupuestal, a qué tercero pertenecen y bajo qué cuenta contable fueron registradas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewBudgetAllocationInitialBalances';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewBudgetAllocationInitialBalances';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las cuentas por cobrar del saldo inicial de cartera pendientes de afectar presupuesto, junto con su tercero, cuenta contable y valor para asignación presupuestal.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewBudgetAllocationInitialBalances';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas por cobrar del saldo inicial deben tener un tercero válido en Common.ThirdParty.; Cada cuenta por cobrar debe tener al menos un registro contable asociado en PortfolioInitialBalanceAccountReceivableAccounting.; El registro contable debe referenciar una cuenta principal existente en GeneralLedger.MainAccounts.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewBudgetAllocationInitialBalances';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen cuentas por cobrar que no han afectado presupuesto (AffectBudget = 0).; Por uso de INNER JOIN, se omiten registros sin tercero, sin contabilización o sin cuenta contable principal asociada.; La identificación del tercero se presenta siempre como ''NIT - Nombre'' y la cuenta contable como ''Número - Nombre''.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewBudgetAllocationInitialBalances';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Saldo inicial de cartera; Cuenta por cobrar; Afectación presupuestal; Tercero (NIT); Cuenta contable principal (PUC); Factura', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewBudgetAllocationInitialBalances';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solo cuentas por cobrar del saldo inicial cuyo AffectBudget = 0 (aún no han afectado presupuesto), concatenando NIT-Nombre del tercero y Número-Nombre de la cuenta contable.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewBudgetAllocationInitialBalances';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ar.AffectBudget = 0 → Se incluye la cuenta por cobrar en el resultado (pendiente de asignación presupuestal). else Se excluye del resultado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewBudgetAllocationInitialBalances';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioInitialBalanceAccountReceivable; Common.ThirdParty; Portfolio.PortfolioInitialBalanceAccountReceivableAccounting; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewBudgetAllocationInitialBalances';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewBudgetAllocationInitialBalances';
GO
