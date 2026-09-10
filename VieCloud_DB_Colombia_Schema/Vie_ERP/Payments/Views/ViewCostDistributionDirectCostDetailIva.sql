CREATE VIEW [Payments].[ViewCostDistributionDirectCostDetailIva]
AS

SELECT	cddcdi.Id,
		cddc.AccountPayableId, 
		apdc.Id AccountPayableDetailConceptId,
		gli.Name,
		gli.Percentage,
		cddcdi.IvaValue
FROM Cost.CostDistributionDirectCost cddc
JOIN Cost.CostDistributionDirectCostDetail cddcd ON cddc.Id = cddcd.DistributionDirectCostId
JOIN Cost.CostDistributionDirectCostDetailIva cddcdi ON cddcd.Id = cddcdi.CostDistributionDirectCostDetailId
JOIN GeneralLedger.GeneralLedgerIVA gli ON cddcdi.GeneralLedgerIvaId = gli.Id
OUTER APPLY (
	SELECT *
	FROM Payments.AccountPayableDetailConcept apdc
	WHERE cddc.AccountPayableId = apdc.IdAccountPayable
		AND apdc.IsDirectCost = 1
		AND cddcd.MainAccountId = apdc.IdAccount
		AND ISNULL(cddcd.CostCenterId, 0) = ISNULL(apdc.IdCostCenter, 0)
		AND ISNULL(cddcd.ThirdPartyId, apdc.IdThirdParty) = apdc.IdThirdParty
		AND cddcd.Value = apdc.Value
) apdc
WHERE cddc.Status = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que expone el detalle de IVA aplicado a cada línea de distribución de costos directos, mostrando el nombre y porcentaje de la tarifa de IVA (según el libro mayor) y el valor de IVA calculado para cada concepto. Integra la distribución de costos directos con su detalle de IVA y lo vincula con el concepto correspondiente de la cuenta por pagar al proveedor, filtrando únicamente las distribuciones en estado aprobado o cerrado (Status = 2). Sirve para reportería contable y de costos que requiere identificar cuánto IVA fue asignado por línea de costo directo y a qué cuenta por pagar está asociado.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewCostDistributionDirectCostDetailIva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewCostDistributionDirectCostDetailIva';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de IVA de la distribución de costos directos vinculándolo con el concepto de cuenta por pagar correspondiente, solo para distribuciones en estado 2.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewCostDistributionDirectCostDetailIva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La distribución de costo directo debe tener Status = 2 para ser incluida.; Deben existir registros relacionados en CostDistributionDirectCostDetail y CostDistributionDirectCostDetailIva.; El IVA debe estar parametrizado en GeneralLedger.GeneralLedgerIVA.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewCostDistributionDirectCostDetailIva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan distribuciones de costo directo con Status = 2.; El emparejamiento con el concepto de cuenta por pagar requiere que el concepto sea de costo directo (IsDirectCost = 1).; El cruce con AccountPayableDetailConcept exige igualdad de cuenta principal y de valor, y tolera nulos en CostCenterId y ThirdPartyId tratándolos como coincidentes.; Cada fila representa un componente de IVA de un detalle de distribución de costo directo.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewCostDistributionDirectCostDetailIva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos directos; IVA; Cuenta por pagar; Concepto de cuenta por pagar; Centro de costo; Tercero; Cuenta contable principal; Costo directo', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewCostDistributionDirectCostDetailIva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas con Id del IVA del detalle, AccountPayableId, AccountPayableDetailConceptId, nombre y porcentaje del IVA, y valor del IVA, solo cuando cddc.Status = 2.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewCostDistributionDirectCostDetailIva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cddc.Status = 2 → Se incluye la distribución de costo directo en el resultado. else Se excluye del resultado.; si En el OUTER APPLY: AccountPayableDetailConcept con IsDirectCost = 1 y coincidencia de cuenta principal, centro de costo, tercero y valor → Se asocia el AccountPayableDetailConceptId al detalle de IVA. else Se devuelve NULL en AccountPayableDetailConceptId (por OUTER APPLY).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewCostDistributionDirectCostDetailIva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionDirectCost; Cost.CostDistributionDirectCostDetail; Cost.CostDistributionDirectCostDetailIva; GeneralLedger.GeneralLedgerIVA; Payments.AccountPayableDetailConcept', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewCostDistributionDirectCostDetailIva';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewCostDistributionDirectCostDetailIva';
GO
