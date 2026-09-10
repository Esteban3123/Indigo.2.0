

CREATE VIEW [Budget].[VReportBudgetIncome]
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
c.ItemType = 1 and c.Auxiliary = 0

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
inner join Budget.RevenueType rt  with (nolock)on c.BudgetaryValidityId = rt.BudgetaryValidityId 
left join Budget.Budget b  with (nolock) on b.CategoryId = c.Id and b.RevenueTypeId = rt.Id
where 
c.ItemType = 1 and c.Auxiliary = 1 and rt.Type = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista para reportes de presupuesto de ingresos (rentas) de una vigencia presupuestal. Combina dos conjuntos de datos: las categorías de ingresos no auxiliares (cabeceras o agrupadores) sin valores monetarios, y las categorías auxiliares (líneas de detalle) cruzadas con los tipos de ingreso o renta y sus valores presupuestales. Para cada rubro de ingreso muestra el valor inicial, modificaciones por crédito y débito, traslados, presupuesto total, valor ejecutado y saldo disponible. Sirve como fuente principal para los informes de ejecución presupuestal de ingresos, permitiendo visualizar la estructura jerárquica del presupuesto de rentas junto con su comportamiento financiero por vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'VReportBudgetIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'VReportBudgetIncome';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola estructura las categorías presupuestales de ingreso (encabezados sin valores y auxiliares con sus montos por tipo de renta) para alimentar el reporte de ejecución de ingresos.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen categorías presupuestales en Budget.Category con ItemType = 1 (ingresos); Las categorías auxiliares tienen tipos de renta asociados en Budget.RevenueType de la misma vigencia presupuestal (BudgetaryValidityId)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen categorías de ingreso (ItemType = 1); Solo se consideran tipos de renta con Type = 1 para el detalle; Las filas de encabezado (Auxiliary=0) siempre exponen ceros en los campos monetarios, independientemente de cualquier dato en Budget; La unión de categorías auxiliares con tipos de renta se restringe a la misma vigencia presupuestal (BudgetaryValidityId); El cruce con Budget.Budget es opcional (LEFT JOIN): una categoría auxiliar puede aparecer aunque no tenga línea presupuestal', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Presupuesto de ingresos; Vigencia presupuestal; Categoría presupuestal; Tipo de renta/ingreso; Categoría auxiliar vs encabezado; Modificaciones presupuestales (créditos/débitos); Traslados presupuestales; Ejecución presupuestal; Saldo presupuestal', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.VReportBudgetIncome: Devuelve filas de encabezado (Auxiliary=0) con valores numéricos en cero y filas detalle (Auxiliary=1) con los montos reales del presupuesto.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si c.ItemType = 1 AND c.Auxiliary = 0 → Retorna la categoría como fila de encabezado con RevenueTypeCode/Name vacíos y todos los valores monetarios en cero; si c.ItemType = 1 AND c.Auxiliary = 1 AND rt.Type = 1 → Retorna la categoría auxiliar cruzada con tipos de renta de tipo 1, trayendo los valores del Budget (LEFT JOIN permite filas sin presupuesto cargado)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Category; Budget.RevenueType; Budget.Budget', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'VReportBudgetIncome';
GO
