

CREATE VIEW [Budget].[ViewListBudgetAvailabilityOfProducts]
AS

select ROW_NUMBER() OVER (ORDER BY ipr.Id) Id,
a.Code AvailabilityCode, c.Name CategoryName, fs.Code + ' - ' + fs.Name FinancialSourceDescription, rt.Code + ' - ' + rt.Name RevenueTypeDescription,
ad.Balance AvailabilityBalance, ipr.Id ProductId, pg.Id ProductGroupId, a.Id AvailabilityId, ad.Id AvailabilityDetailId,
a.Code + ' - ' + c.Name CodeName
from Inventory.InventoryProduct ipr with(nolock)
inner join Inventory.ProductGroup pg with(nolock) on pg.Id = ipr.ProductGroupId
inner join Budget.AvailabilityDetail ad with(nolock) on ad.BudgetId = pg.BudgetId
inner join Budget.Availability a with(nolock) on a.Id = ad.AvailabilityId
inner join Budget.Budget b with(nolock) on b.Id = ad.BudgetId
inner join Budget.Category c with(nolock) on c.Id = b.CategoryId
inner join Budget.RevenueType rt with(nolock) on rt.Id = b.RevenueTypeId
left join Budget.FinancialSource fs with(nolock) on fs.Id = c.FinancialSourceId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra la disponibilidad presupuestal (CDP) asociada a cada producto del inventario (medicamentos, insumos, dispositivos médicos). Cruza cada producto con su grupo de productos, el rubro presupuestal al que pertenece, la categoría de gasto, la fuente de financiación y el tipo de ingreso o renta, exponiendo el saldo disponible del certificado de disponibilidad presupuestal correspondiente. Se utiliza para consultar qué disponibilidad presupuestal respalda la adquisición o consumo de un producto específico del inventario, facilitando el control del gasto por ítem, fuente de financiación y tipo de renta durante la ejecución presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListBudgetAvailabilityOfProducts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListBudgetAvailabilityOfProducts';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista por producto de inventario el saldo de disponibilidad presupuestal asociado, mostrando categoría, fuente financiera y tipo de ingreso del presupuesto vinculado al grupo del producto.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada producto de inventario debe pertenecer a un ProductGroup con BudgetId válido; Debe existir AvailabilityDetail vinculado al BudgetId del ProductGroup; El Budget debe tener Category y RevenueType definidos', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El vínculo producto→presupuesto se establece a través de ProductGroup.BudgetId; Solo se incluyen productos cuyo grupo tenga al menos un AvailabilityDetail con el mismo BudgetId (INNER JOIN); AvailabilityCode y CodeName se construyen a partir de Budget.Availability.Code; CodeName combina el código de disponibilidad con el nombre de la categoría presupuestal; Las consultas usan WITH(NOLOCK), permitiendo lecturas sucias', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'disponibilidad presupuestal; saldo presupuestal; categoría presupuestal; tipo de ingreso/renta; fuente de financiamiento; producto de inventario; grupo de productos; presupuesto', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve una fila por cada combinación producto × detalle de disponibilidad presupuestal asociada a su grupo, numerada con ROW_NUMBER OVER (ORDER BY ipr.Id)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LEFT JOIN Budget.FinancialSource sobre Category.FinancialSourceId → FinancialSourceDescription puede ser NULL si la categoría no tiene fuente de financiamiento asociada else Se concatena Code + '' - '' + Name de la fuente', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.ProductGroup; Budget.AvailabilityDetail; Budget.Availability; Budget.Budget; Budget.Category; Budget.RevenueType; Budget.FinancialSource', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListBudgetAvailabilityOfProducts';
GO
