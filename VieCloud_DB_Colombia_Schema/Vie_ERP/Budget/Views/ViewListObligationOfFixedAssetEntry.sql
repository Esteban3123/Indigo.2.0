CREATE VIEW [Budget].[ViewListObligationOfFixedAssetEntry]
AS
select ROW_NUMBER() OVER (ORDER BY ev.Id) Id,
ev.Id FixedAssetEntryId,
ap.Id AccountPayableId,
od.Id ObligationDetailId, od.InitialValue, od.Balance, od.EntityId, od.EntityCode, od.EntityName,
o.Id ObligationId, o.Code ObligationCode, o.Document ObligationDocument,
c.Id CategoryId, c.Name CategoryName, c.Code + ' - ' + c.Name CategoryDescription,
rt.Id RevenueTypeId, rt.Code + ' - ' + rt.Name RevenueTypeDescription,
fs.Id FinancialSourceId, fs.Code + ' - ' + fs.Name FinancialSourceDescription,
od.CommitmentDetailId
from FixedAsset.FixedAssetEntry ev
inner join Payments.AccountPayable ap on ap.Id = ev.AccountPayableId
inner join Budget.ObligationDetail od on od.EntityId = ap.Id and od.EntityName = 'AccountPayable'
inner join Budget.Obligation o on o.Id = od.ObligationId
inner join Budget.Category c on c.Id = od.CategoryId
inner join Budget.RevenueType rt on rt.Id = od.RevenueTypeId
left join Budget.FinancialSource fs on fs.Id = c.FinancialSourceId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que relaciona las entradas o adquisiciones de activos fijos con sus obligaciones presupuestarias. Para cada compra o incorporación de un activo fijo, cruza la cuenta por pagar generada al proveedor con el detalle de obligación presupuestal correspondiente, mostrando el rubro presupuestal (categoría), la fuente de financiación, el tipo de renta y los valores comprometidos (valor inicial y saldo). Sirve para reportería de ejecución presupuestal de inversión en activos fijos, permitiendo rastrear qué compromiso u obligación de gasto respalda cada adquisición de activo y cuánto presupuesto queda disponible en ese rubro.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListObligationOfFixedAssetEntry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListObligationOfFixedAssetEntry';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las obligaciones presupuestarias asociadas a entradas de activos fijos, enlazando cada ingreso de activo con su cuenta por pagar, detalle de obligación, categoría, tipo de renta y fuente de financiación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfFixedAssetEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada FixedAssetEntry debe tener una AccountPayable referenciada (ap.Id = ev.AccountPayableId); Debe existir al menos un ObligationDetail cuyo EntityId apunte al Id de la AccountPayable y cuyo EntityName sea ''AccountPayable''; El ObligationDetail debe tener Obligation, Category y RevenueType válidos (joins INNER)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfFixedAssetEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran obligaciones cuyo EntityName sea exactamente ''AccountPayable'' (polimorfismo de ObligationDetail filtrado a cuentas por pagar); La trazabilidad activo fijo → obligación se realiza siempre vía AccountPayable, nunca directamente; Las descripciones de Category, RevenueType y FinancialSource se presentan con formato ''Code - Name''; El Id de salida es un correlativo no persistente generado por ROW_NUMBER, ordenado por FixedAssetEntry.Id', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfFixedAssetEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Entrada de activo fijo; Cuenta por pagar; Obligación presupuestaria; Detalle de obligación; Compromiso (CommitmentDetail); Categoría presupuestal; Tipo de renta; Fuente de financiación; Saldo y valor inicial de obligación', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfFixedAssetEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.ViewListObligationOfFixedAssetEntry: Devuelve un conjunto numerado (ROW_NUMBER OVER ORDER BY ev.Id) con la relación entrada de activo fijo ↔ obligación presupuestaria a través de la cuenta por pagar', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfFixedAssetEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ObligationDetail.EntityName = ''AccountPayable'' y EntityId = AccountPayable.Id → Vincula la entrada de activo fijo con el detalle de obligación correspondiente else Detalles con EntityName distinto de ''AccountPayable'' quedan excluidos del resultado; si Category.FinancialSourceId tiene match en Budget.FinancialSource → Se incluye la descripción de la fuente de financiación else FinancialSource queda en NULL (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfFixedAssetEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetEntry; Payments.AccountPayable; Budget.ObligationDetail; Budget.Obligation; Budget.Category; Budget.RevenueType; Budget.FinancialSource', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfFixedAssetEntry';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationOfFixedAssetEntry';
GO
