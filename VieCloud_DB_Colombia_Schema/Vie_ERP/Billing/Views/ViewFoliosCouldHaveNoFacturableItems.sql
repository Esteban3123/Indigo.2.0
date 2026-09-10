

CREATE view [Billing].[ViewFoliosCouldHaveNoFacturableItems]
as (
select distinct rcd.Id, rcd.CareGroupId
from Billing.RevenueControlDetail rcd
inner join Billing.RevenueControl rc on rc.Id = rcd.RevenueControlId
inner join [Contract].CareGroup cg on cg.Id = rcd.CareGroupId
inner join [Contract].CareGroupBillingItemsRestriction cgr on cg.Id = cgr.CareGroupId
inner join Contract.BillingItemsRestriction r on r.Id = cgr.BillingItemsRestrictionId and r.[Status] = 1
where rcd.Status in (1,3)
)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que identifica los folios de facturación (detalles de control de ingresos) que podrían no tener ítems facturables, porque su grupo de atención tiene restricciones de facturación activas aplicadas por contrato. Cruza los detalles de folio activos o en proceso (estados 1 y 3) con los grupos de atención del contrato que poseen al menos una restricción de ítem de facturación vigente. Se usa para detectar previamente qué folios están en riesgo de quedar sin conceptos cobrables debido a exclusiones o limitaciones contractuales, evitando así errores en el proceso de facturación. Devuelve el identificador del detalle de folio y el grupo de atención asociado, para su revisión o corrección antes de emitir la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewFoliosCouldHaveNoFacturableItems';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewFoliosCouldHaveNoFacturableItems';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Identifica los folios de facturación cuyo grupo de atención (CareGroup) tiene restricciones activas de ítems facturables, por lo que podrían no contener ítems facturables.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewFoliosCouldHaveNoFacturableItems';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada RevenueControlDetail debe estar asociado a un RevenueControl existente y a un CareGroup existente.; El CareGroup debe tener registrada al menos una asociación en CareGroupBillingItemsRestriction con una BillingItemsRestriction activa (Status = 1).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewFoliosCouldHaveNoFacturableItems';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran detalles de folio cuyo Status está en (1,3), excluyendo cualquier otro estado.; Solo se consideran restricciones de ítems de facturación activas (BillingItemsRestriction.Status = 1).; Un folio aparece únicamente si su CareGroup tiene al menos una restricción de ítems facturables activa vinculada.; Los resultados son distintos (DISTINCT), evitando duplicados producto del cruce con múltiples restricciones.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewFoliosCouldHaveNoFacturableItems';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'folio de facturación; control de ingresos; grupo de atención (CareGroup); restricciones de ítems facturables; contrato', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewFoliosCouldHaveNoFacturableItems';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.RevenueControlDetail: Devuelve Id y CareGroupId distintos de RevenueControlDetail cuando Status IN (1,3) y existe una restricción activa de ítems de facturación (BillingItemsRestriction.Status = 1) asociada a su CareGroup vía CareGroupBillingItemsRestriction.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewFoliosCouldHaveNoFacturableItems';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Billing.RevenueControl; Contract.CareGroup; Contract.CareGroupBillingItemsRestriction; Contract.BillingItemsRestriction', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewFoliosCouldHaveNoFacturableItems';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewFoliosCouldHaveNoFacturableItems';
GO
