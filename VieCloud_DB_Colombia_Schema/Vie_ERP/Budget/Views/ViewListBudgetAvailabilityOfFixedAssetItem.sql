

CREATE VIEW [Budget].[ViewListBudgetAvailabilityOfFixedAssetItem]
AS

select ROW_NUMBER() OVER (ORDER BY i.Id) Id,
a.Code AvailabilityCode, c.Name CategoryName, fs.Code + ' - ' + fs.Name FinancialSourceDescription, rt.Code + ' - ' + rt.Name RevenueTypeDescription,
ad.Balance AvailabilityBalance, i.Id ItemId, ic.Id ItemCatalogId, a.Id AvailabilityId, ad.Id AvailabilityDetailId,
a.Code + ' - ' + c.Name CodeName
from FixedAsset.FixedAssetItem i with(nolock)
inner join FixedAsset.FixedAssetItemCatalog ic with(nolock) on ic.Id = i.ItemCatalogId
inner join Budget.AvailabilityDetail ad with(nolock) on ad.BudgetId = ic.BudgetId
inner join Budget.Availability a with(nolock) on a.Id = ad.AvailabilityId
inner join Budget.Budget b with(nolock) on b.Id = ad.BudgetId
inner join Budget.Category c with(nolock) on c.Id = b.CategoryId
inner join Budget.RevenueType rt with(nolock) on rt.Id = b.RevenueTypeId
left join Budget.FinancialSource fs with(nolock) on fs.Id = c.FinancialSourceId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista la disponibilidad presupuestal (CDP) asociada a cada ítem de activo fijo registrado en el sistema. Para cada activo, muestra el certificado de disponibilidad presupuestal (código y saldo disponible), la categoría presupuestal, la fuente de financiación y el tipo de renta o ingreso con los que fue respaldado. Integra información del catálogo de activos fijos con los detalles de disponibilidad presupuestal, permitiendo consultar qué recursos presupuestales están reservados para la adquisición o gestión de activos fijos en cada vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListBudgetAvailabilityOfFixedAssetItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListBudgetAvailabilityOfFixedAssetItem';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista la disponibilidad presupuestal vigente asociada a cada ítem del catálogo de activos fijos, mostrando categoría, fuente de financiación, tipo de ingreso y saldo disponible.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfFixedAssetItem';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada FixedAssetItem debe tener un FixedAssetItemCatalog asociado (ItemCatalogId).; El FixedAssetItemCatalog debe estar vinculado a un Budget (BudgetId) que tenga al menos un registro en Budget.AvailabilityDetail.; El Budget debe tener Categoría (CategoryId) y Tipo de ingreso (RevenueTypeId) válidos.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfFixedAssetItem';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ítems de activo fijo cuyo catálogo (ItemCatalogId) tenga presupuesto con detalle de disponibilidad (INNER JOIN sobre AvailabilityDetail por BudgetId).; La relación entre disponibilidad y presupuesto se establece exclusivamente vía BudgetId del FixedAssetItemCatalog.; Cada fila combina los códigos+nombre de Availability y Category en CodeName con el formato ''Code - Name''.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfFixedAssetItem';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Disponibilidad presupuestal; Activo fijo; Catálogo de ítems de activo fijo; Categoría presupuestal; Tipo de ingreso (renta); Fuente de financiación; Saldo disponible (Balance)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfFixedAssetItem';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.ViewListBudgetAvailabilityOfFixedAssetItem: Devuelve una fila por cada combinación ítem-disponibilidad numerada con ROW_NUMBER() OVER (ORDER BY i.Id), exponiendo balance disponible, categoría, fuente financiera y tipo de ingreso.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfFixedAssetItem';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LEFT JOIN Budget.FinancialSource fs ON fs.Id = c.FinancialSourceId → Si la categoría tiene fuente financiera asociada, se incluye su código y nombre en FinancialSourceDescription. else Si la categoría no tiene FinancialSourceId, FinancialSourceDescription queda en NULL pero la fila igualmente se retorna.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfFixedAssetItem';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemCatalog; Budget.AvailabilityDetail; Budget.Availability; Budget.Budget; Budget.Category; Budget.RevenueType; Budget.FinancialSource', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfFixedAssetItem';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfFixedAssetItem';
GO
