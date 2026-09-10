

CREATE VIEW [Billing].[ViewListRecognitionEntrance]
AS
	SELECT	cg.Id CareGroupId,
			CONCAT(cg.Code, ' - ', cg.Name) as CareGroupCodeName,
			cg.OperativeUnitId,
			SUM(sodd.ThirdPartySalesPrice + sodd.SubTotalPatientSalesPrice + sodd.GrandTotalDiscount - sodd.GrandTotalTaxes) TotalCareGroup,
			COUNT(DISTINCT rcd.Id) FolioQuantity
	FROM Contract.CareGroup cg WITH (NOLOCK)
	JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON cg.Id = rcd.CareGroupId
	JOIN Billing.RevenueControl rc WITH (NOLOCK) ON rc.Id = rcd.RevenueControlId
	JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON rcd.Id = sodd.RevenueControlDetailId
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
	JOIN ..ADINGRESO ing WITH (NOLOCK) ON ing.NUMINGRES = rc.AdmissionNumber
	WHERE rcd.[Status] IN( 1,3) 
		and rcd.IsMasterAccount <> 3
		AND ing.IESTADOIN IN (' ', 'P','B')
		AND cg.LiquidationType In (1, 3)
		AND sod.IsDelete = 0 
		AND sod.SettlementType != 3 
		AND sod.GrandTotalSalesPrice > 0
	GROUP BY cg.Id, cg.Code, cg.Name, cg.OperativeUnitId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el reconocimiento de ingresos por grupo de atención (CareGroup) para admisiones activas de pacientes. Para cada grupo de atención vigente en un contrato, calcula el valor total facturado (suma de lo que paga el tercero/asegurador más la parte del paciente, incluyendo descuentos e impuestos) y la cantidad de folios de facturación asociados, cruzando los controles de ingreso, el detalle de distribución financiera de órdenes de servicio y los ingresos/admisiones del paciente. Filtra únicamente folios activos o reconocidos (estados 1 y 3), admisiones en curso o pendientes, grupos con tipos de liquidación estándar o especial (1 y 3), y excluye ítems eliminados, servicios de tipo liquidación 3 y valores en cero. Sirve como base para reportería de reconocimiento de ingresos por grupo de atención y unidad operativa dentro del módulo de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListRecognitionEntrance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListRecognitionEntrance';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida por grupo de atención (CareGroup) el valor total facturable y la cantidad de folios pendientes de reconocimiento de ingresos para admisiones activas/pendientes con liquidación elegible.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntrance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Billing.RevenueControlDetail enlazados a Billing.RevenueControl, ServiceOrderDetailDistribution y ServiceOrderDetail.; La admisión (ADINGRESO.NUMINGRES) referenciada por RevenueControl.AdmissionNumber debe existir en la tabla legacy ADINGRESO.; El CareGroup debe tener LiquidationType en (1,3) para ser considerado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntrance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El total por CareGroup se calcula como ThirdPartySalesPrice + SubTotalPatientSalesPrice + GrandTotalDiscount - GrandTotalTaxes a nivel de distribución de la orden de servicio.; Nunca se incluyen ítems eliminados (IsDelete=1), ni con SettlementType=3, ni con valor cero o negativo.; Nunca se incluyen folios cancelados/cerrados fuera de Status 1 o 3, ni cuentas maestras tipo 3.; Solo se cuentan admisiones cuyo estado legacy esté en ('' '',''P'',''B''), excluyendo admisiones en otros estados (p.ej. cerradas/anuladas).; El conteo de folios usa DISTINCT sobre RevenueControlDetail.Id para evitar duplicación por la distribución.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntrance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo de atención (CareGroup); Folio de facturación; Control de ingresos; Admisión del paciente; Liquidación; Cuenta maestra; Distribución tercero pagador / paciente; Reconocimiento de ingresos', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntrance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewListRecognitionEntrance: Devuelve por cada CareGroup el TotalCareGroup = SUM(ThirdPartySalesPrice + SubTotalPatientSalesPrice + GrandTotalDiscount - GrandTotalTaxes) y FolioQuantity = COUNT(DISTINCT RevenueControlDetail.Id), agrupando por Id, Code, Name y OperativeUnitId del CareGroup.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntrance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rcd.Status IN (1,3) AND rcd.IsMasterAccount <> 3 → Solo se incluyen folios de control de ingresos con estado 1 o 3 y que no sean cuenta maestra tipo 3.; si ing.IESTADOIN IN ('' '',''P'',''B'') → Solo se consideran admisiones cuyo estado en ADINGRESO sea vacío, ''P'' (pendiente) o ''B''.; si cg.LiquidationType IN (1,3) → Solo CareGroups con tipo de liquidación 1 o 3 entran al reconocimiento.; si sod.IsDelete = 0 AND sod.SettlementType <> 3 AND sod.GrandTotalSalesPrice > 0 → Solo se suman ítems de orden de servicio no eliminados, con tipo de liquidación distinto de 3 y valor total mayor a cero.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntrance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Billing.RevenueControlDetail; Billing.RevenueControl; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; ADINGRESO', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntrance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntrance';
GO
