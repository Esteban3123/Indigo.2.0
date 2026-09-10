

CREATE VIEW [Budget].[ViewListAnnualizedCashFlow]
AS

select 
c.Code
,C.Name
,fs.Code + ' - ' + fs.Name as FinancialSource
,c.ItemType
,c.BudgetaryValidityId
,(select sum(TotalBudget) from Budget.Budget where CategoryId = c.Id) as BudgetValue
,isnull((select sum(InitialValue) from Budget.AnnualizedCashFlow where CategoryId = c.Id),0) as PACValue
,isnull((select top 1 Status from Budget.AnnualizedCashFlow where CategoryId = c.Id),0) as Status

from Budget.Category c with (nolock) 
inner join Budget.FinancialSource fs with (nolock) on fs.Id = c.FinancialSourceId
where c.PAC = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada del flujo de caja anualizado (PAC - Programa Anual de Caja) por categoría presupuestal. Combina las categorías marcadas como PAC con su fuente de financiación, mostrando el valor total del presupuesto asignado y el valor inicial registrado en el flujo de caja anualizado. Sirve para consultar el estado del PAC por categoría, incluyendo el tipo de ítem presupuestal, la vigencia presupuestal y el estado actual del flujo de caja, apoyando el seguimiento y control presupuestal de caja de la entidad.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListAnnualizedCashFlow';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListAnnualizedCashFlow';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las categorías presupuestales marcadas como PAC junto con su fuente de financiación, valor presupuestado total y valor del flujo de caja anualizado (PAC) con su estado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAnnualizedCashFlow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La categoría debe estar marcada como PAC = 1 en Budget.Category; Debe existir la fuente de financiación referenciada (FinancialSourceId) en Budget.FinancialSource (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAnnualizedCashFlow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan categorías presupuestales con bandera PAC activa (PAC = 1); FinancialSource se presenta concatenada como ''Code - Name''; Lecturas con NOLOCK: puede haber lecturas sucias en Category y FinancialSource; Si una categoría no tiene flujo de caja anualizado, PACValue y Status retornan 0; El Status devuelto no es determinístico al usar TOP 1 sin ORDER BY', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAnnualizedCashFlow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Categoría presupuestal; Fuente de financiación; PAC (Programa Anual de Caja); Flujo de caja anualizado; Vigencia presupuestal; Presupuesto total', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAnnualizedCashFlow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.Category: Devuelve únicamente categorías con PAC = 1, combinadas con su fuente de financiación; [RETURN_RESULT] Budget.Budget: Calcula BudgetValue como SUM(TotalBudget) de Budget.Budget filtrado por CategoryId de la categoría; [RETURN_RESULT] Budget.AnnualizedCashFlow: Calcula PACValue como SUM(InitialValue) de Budget.AnnualizedCashFlow por CategoryId; si no hay registros retorna 0 (ISNULL); [RETURN_RESULT] Budget.AnnualizedCashFlow: Devuelve Status tomando el primer (TOP 1) registro de Budget.AnnualizedCashFlow para la categoría; si no existe, 0', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAnnualizedCashFlow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Category; Budget.FinancialSource; Budget.Budget; Budget.AnnualizedCashFlow', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAnnualizedCashFlow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAnnualizedCashFlow';
GO
