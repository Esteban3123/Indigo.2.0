
CREATE VIEW [Budget].[ViewListAvailabilityDetail]
AS
SELECT 
	ad.Id,
	a.BudgetaryValidityId,
	a.Code AvailabilityCode, 
	a.Observations AvailabilityObservations,
	CONCAT(c.Code, ' - ', c.Name)  CategoryCodeName,
	CONCAT(fs.Code, ' - ', fs.Name) FinancialSourceCodeName, 
	CONCAT(rt.Code, ' - ', rt.Name) RevenueTypeCodeName,
	ad.Balance
FROM Budget.Availability a WITH (NOLOCK)
JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON a.Id = ad.AvailabilityId
JOIN Budget.Budget b WITH (NOLOCK) ON ad.BudgetId = b.Id
JOIN Budget.Category c WITH (NOLOCK) ON b.CategoryId = c.Id
JOIN Budget.RevenueType rt WITH (NOLOCK) ON b.RevenueTypeId = rt.Id
JOIN Budget.FinancialSource fs WITH (NOLOCK) ON c.FinancialSourceId = fs.Id
WHERE a.Status = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los certificados de disponibilidad presupuestal (CDP) activos y sus líneas de rubro asociadas. Integra cada documento de disponibilidad con su rubro presupuestal, la categoría del gasto, la fuente de financiación y el tipo de ingreso o renta, mostrando el saldo disponible por cada línea. Filtra únicamente los CDP en estado activo (Status = 2) y concatena los códigos con sus nombres para facilitar la lectura en reportes y consultas de disponibilidad presupuestal por vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListAvailabilityDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListAvailabilityDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de saldos de disponibilidades presupuestales activas, enriquecido con los códigos y nombres de categoría, fuente de financiación y tipo de ingreso asociados a cada rubro.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAvailabilityDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen disponibilidades en Budget.Availability con Status = 2 (estado considerado ''activo/vigente'' para listar); Cada AvailabilityDetail está asociado a un Budget válido con Category, RevenueType y FinancialSource existentes (joins INNER)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAvailabilityDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan disponibilidades cuyo Status sea exactamente 2; La fuente financiera mostrada proviene de la categoría del presupuesto (c.FinancialSourceId), no directamente de la disponibilidad; Se usan lecturas con NOLOCK en todas las tablas consultadas, asumiendo tolerancia a lecturas sucias; Los campos CategoryCodeName, FinancialSourceCodeName y RevenueTypeCodeName se construyen con el formato ''Code - Name''', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAvailabilityDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Disponibilidad presupuestal (CDP); Detalle de disponibilidad por rubro; Vigencia presupuestal; Categoría presupuestal; Tipo de ingreso/renta; Fuente de financiación; Saldo disponible (Balance)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAvailabilityDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.AvailabilityDetail: Devuelve una fila por detalle de disponibilidad cuyo encabezado (Availability) tenga Status = 2, exponiendo el balance del rubro junto con los códigos concatenados ''Code - Name'' de categoría, fuente financiera y tipo de ingreso.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAvailabilityDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si a.Status = 2 → Se incluye la disponibilidad y su detalle en el resultado else Las disponibilidades con cualquier otro Status quedan excluidas del listado', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAvailabilityDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Availability; Budget.AvailabilityDetail; Budget.Budget; Budget.Category; Budget.RevenueType; Budget.FinancialSource', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAvailabilityDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListAvailabilityDetail';
GO
