
CREATE VIEW [Payments].[VAccountPayablesWithoutDistribuit]
AS
	SELECT	ap.Id, ap.Code, ap.BillNumber, ap.DocumentDate BillDate, 
			ap.IdThirdParty, ap.InvoiceValue, ap.Coments,
			ap.Status, cddc.Id AS CostDistributionDirectCostId
	FROM Payments.AccountPayable ap
	LEFT JOIN Cost.CostDistributionDirectCost  cddc
		ON ap.IdThirdParty = cddc.ThirdPartyId 
			AND cddc.Status <> 3
			AND ap.Id = cddc.AccountPayableId
	WHERE ap.Status = 1 AND cddc.Id IS NULL
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuentas por pagar a proveedores que aún no han sido distribuidas en costos directos. Combina las facturas o documentos recibidos de terceros (proveedores) con el registro de distribución de costos directos, filtrando únicamente aquellas cuentas por pagar activas (estado 1) que no tienen ninguna distribución de costo directo asociada vigente. Es útil para identificar facturas de proveedores pendientes de imputar a un centro de costo o proceso de distribución contable, evitando que queden sin asignación en el cierre de costos.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'VAccountPayablesWithoutDistribuit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'VAccountPayablesWithoutDistribuit';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las cuentas por pagar activas que aún no tienen una distribución de costo directo vigente asignada, para identificar pendientes de distribución.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VAccountPayablesWithoutDistribuit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en Payments.AccountPayable con Status = 1.; La relación con Cost.CostDistributionDirectCost requiere coincidencia por tercero (ThirdPartyId) y por la misma cuenta por pagar (AccountPayableId).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VAccountPayablesWithoutDistribuit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone cuentas por pagar con Status = 1 (activas/vigentes).; Excluye cuentas por pagar que ya tengan al menos un registro de distribución de costo directo asociado con Status <> 3 (no anulado).; El emparejamiento entre cuenta por pagar y distribución se hace por tercero y por identificador de la cuenta por pagar simultáneamente.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VAccountPayablesWithoutDistribuit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por pagar; Distribución de costos directos; Tercero/Proveedor; Factura', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VAccountPayablesWithoutDistribuit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payments.AccountPayable: Devuelve cuentas por pagar cuando ap.Status = 1 y no existe coincidencia activa en CostDistributionDirectCost (cddc.Status <> 3) por tercero y cuenta por pagar (cddc.Id IS NULL).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VAccountPayablesWithoutDistribuit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Cost.CostDistributionDirectCost', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VAccountPayablesWithoutDistribuit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VAccountPayablesWithoutDistribuit';
GO
