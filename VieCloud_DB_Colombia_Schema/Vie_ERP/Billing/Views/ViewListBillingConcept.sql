

CREATE VIEW [Billing].[ViewListBillingConcept]
as

SELECT 
bc.*
FROM Billing.BillingConcept bc
LEFT JOIN Billing.BillingConcept bc2 ON bc.Id = bc2.AssociatedMainServiceId
WHERE bc2.AssociatedMainServiceId IS NULL AND bc.ConceptType = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de conceptos de facturación principales activos, excluyendo aquellos que están asociados como subconceptos o dependientes de otro concepto superior. Consolida información de la tabla de conceptos de facturación filtrando únicamente los de tipo principal (ConceptType = 1) que no están referenciados como servicio asociado por otro concepto. Sirve como fuente para la parametrización y consulta de conceptos de cobro en el proceso de liquidación y facturación de servicios de salud, incluyendo sus cuentas contables de ingresos, descuentos, IVA, retenciones de ICA, centro de costos y precio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListBillingConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListBillingConcept';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los conceptos de facturación de tipo principal que no están asociados como servicio principal de otro concepto, para su selección en procesos de cobro.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBillingConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone conceptos de facturación cuyo ConceptType = 1 (servicio principal).; Excluye conceptos que estén referenciados como AssociatedMainServiceId por otro concepto, es decir, retorna únicamente conceptos que NO son el ''servicio principal asociado'' de otro concepto.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBillingConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Concepto de facturación; Servicio principal asociado', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBillingConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.BillingConcept: Devuelve todas las columnas de Billing.BillingConcept filtrando por ConceptType = 1 y por aquellos cuyo Id no aparece como AssociatedMainServiceId en otro registro de la misma tabla (LEFT JOIN ... WHERE bc2.AssociatedMainServiceId IS NULL).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBillingConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.BillingConcept', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBillingConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListBillingConcept';
GO
