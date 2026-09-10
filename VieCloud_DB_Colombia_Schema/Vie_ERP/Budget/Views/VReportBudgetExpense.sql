

CREATE VIEW [Budget].[VReportBudgetExpense]
as

select 
c.BudgetaryValidityId,
c.id as idCategory,
c.CategoryOwnerId,
c.Code as CategoryCode,
c.Name as CategoryName,
c.Auxiliary,
'' as RevenueTypeCode,
'' as RevenueTypeName,
0 as InitialValue,
0 as CreditValueModification,
0 as DebitValueModification,
0 as CreditValueTransfer,
0 as DebitValueTransfer,
0 as TotalBudget,
0 as ExecutedValue
,0 as Balance
from 
Budget.Category c
where 
c.ItemType = 2 and c.Auxiliary = 0

union all

select 
c.BudgetaryValidityId,
c.id as idCategory,
c.CategoryOwnerId,
c.Code as CategoryCode,
c.Name as CategoryName,
c.Auxiliary,
rt.Code as RevenueTypeCode,
rt.Name as RevenueTypeName,
b.InitialValue,
b.CreditValueModification,
b.DebitValueModification,
b.CreditValueTransfer,
b.DebitValueTransfer,
b.TotalBudget,
b.ExecutedValue
,b.Balance
from 
Budget.Category c with (nolock)
inner join Budget.RevenueType rt with (nolock) on c.BudgetaryValidityId = rt.BudgetaryValidityId 
left join Budget.Budget b  with (nolock) on b.CategoryId = c.Id and b.RevenueTypeId = rt.Id
where 
c.ItemType = 2 and c.Auxiliary = 1 and rt.Type = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el reporte de gastos o egresos presupuestales para una vigencia presupuestal. Combina dos conjuntos de información: las categorías de gasto no auxiliares (rubros agrupadores sin valores monetarios) y las categorías de gasto auxiliares con su fuente de ingreso o renta asociada (tipo de ingreso), mostrando los valores iniciales, modificaciones por créditos y débitos, traslados presupuestales, presupuesto total, valor ejecutado y saldo disponible. Sirve para la generación de reportes de ejecución presupuestal del lado del gasto o egreso, permitiendo visualizar tanto la estructura del presupuesto como su comportamiento financiero por rubro y fuente de financiamiento.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'VReportBudgetExpense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'VReportBudgetExpense';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola estructura el reporte de ejecución presupuestal de gastos, combinando categorías agregadoras (sin auxiliar) con categorías auxiliares y sus valores presupuestales por tipo de renta.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetExpense';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las categorías deben tener ItemType = 2 (gasto) para ser consideradas; Los tipos de renta (RevenueType) deben tener Type = 2 para participar en el detalle; Las categorías y tipos de renta deben compartir la misma BudgetaryValidityId (vigencia presupuestal)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetExpense';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan categorías de tipo gasto (ItemType=2); Las categorías no auxiliares siempre aparecen con valores presupuestales en cero (son encabezados/agregadoras); El cruce con tipos de renta solo aplica a categorías auxiliares y a tipos de renta con Type=2; El emparejamiento entre Categoría y Tipo de Renta se realiza siempre dentro de la misma vigencia presupuestal (BudgetaryValidityId); La existencia de valores de Budget no es obligatoria para categorías auxiliares (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetExpense';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Presupuesto de gasto; Vigencia presupuestal; Categoría presupuestal; Tipo de renta; Ejecución presupuestal; Modificaciones presupuestales (créditos/débitos); Traslados presupuestales; Saldo presupuestal; Rubro auxiliar vs agregador', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetExpense';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.VReportBudgetExpense: Devuelve filas de categorías agregadoras (Auxiliary=0) con todos los valores presupuestales en cero y sin información de tipo de renta; [RETURN_RESULT] Budget.VReportBudgetExpense: Devuelve filas de categorías auxiliares (Auxiliary=1) cruzadas con tipos de renta de la misma vigencia, incluyendo valores de presupuesto (initial, modificaciones, traslados, ejecución, saldo) cuando existe registro en Budget.Budget; si no existe, los valores quedan en NULL por el LEFT JOIN', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetExpense';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si c.ItemType = 2 AND c.Auxiliary = 0 → Se emite la categoría como fila agregadora con valores presupuestales en cero y sin tipo de renta; si c.ItemType = 2 AND c.Auxiliary = 1 AND rt.Type = 2 → Se emite la categoría auxiliar combinada con cada tipo de renta de gasto de su vigencia, anexando los valores reales de Budget.Budget', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetExpense';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Category; Budget.RevenueType; Budget.Budget', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetExpense';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetExpense';
GO
