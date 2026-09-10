

CREATE VIEW [Cost].[ViewCostDistributionManpower]
AS
	SELECT 
		CONCAT(v.ManpowerType, '-', v.EntityId) UUID, 
		*
	FROM
	(
		SELECT 
			YEAR(l.PayrollDateLiquidated) Year, MONTH(l.PayrollDateLiquidated) Month,
			1 ManpowerType, l.Id EntityId,
			CONCAT(g.Code, ' - ', g.Name) GroupCodeName,
			CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
			CONCAT(p.Code, ' - ', p.Name) PositionCodeName
		FROM Payroll.Liquidation l
		JOIN Payroll.[Group] g ON l.GroupId = g.Id
		JOIN Payroll.Employee e ON l.EmployeeId = e.Id
		JOIN Common.ThirdParty tp ON e.ThirdPartyId = tp.Id	
		JOIN Payroll.Contract c ON l.ContractId = c.Id
		JOIN Payroll.Position p ON c.PositionId = p.Id
		WHERE l.RegisterStatus = 'C'

		UNION ALL

		SELECT
			YEAR(ap.DocumentDate) Year, MONTH(ap.DocumentDate) Month,
			2 ManpowerType, ap.Id EntityId,
			NULL GroupCodeName,
			CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
			CONCAT(p.Code, ' - ', p.Name) PositionCodeName
		FROM Payments.AccountPayable ap
		JOIN Common.ThirdParty tp ON ap.IdThirdParty = tp.Id
		JOIN Payroll.Position p ON ap.PositionId = p.Id
		JOIN
		(
			SELECT
				apdc.IdAccountPayable
			FROM Cost.CostProductionCenter cpc
			JOIN Cost.CostProductionCenterCostCenter cpccc ON cpc.Id = cpccc.ProductionCenterId
			JOIN Cost.CostProductionCenterHomologation cpch ON cpc.Id = cpch.ProductionCenterId
			JOIN Payments.AccountPayableDetailConcept apdc ON cpccc.CostCenterId = apdc.IdCostCenter AND cpch.AccountOriginId = apdc.IdAccount
			GROUP BY apdc.IdAccountPayable
		) apdc ON ap.Id = apdc.IdAccountPayable
		WHERE ap.Status = 2 
	) v
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la distribución de costos de mano de obra para el módulo de costos, combinando dos fuentes: las liquidaciones de nómina confirmadas del personal vinculado directamente (empleados con contrato laboral) y las cuentas por pagar a contratistas o terceros que han sido asociadas a centros de costo y cuentas contables de producción. Para cada registro expone el año y mes del período, el tipo de mano de obra (1=nómina propia, 2=cuenta por pagar a tercero), el grupo de nómina, el NIT y nombre del tercero o empleado, y el cargo o puesto de trabajo. Sirve como base para reportes de costeo de recurso humano, permitiendo comparar y acumular el gasto de personal propio versus contratado por período, centro de costo y cargo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostDistributionManpower';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewCostDistributionManpower';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica en una sola fuente la mano de obra distribuible al costeo: liquidaciones de nómina cerradas y cuentas por pagar aprobadas que tengan conceptos asociados a centros de producción homologados.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionManpower';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las liquidaciones de nómina deben tener RegisterStatus=''C'' (cerradas/confirmadas) para ser consideradas.; Las cuentas por pagar deben tener Status=2 para ser consideradas.; Las cuentas por pagar deben tener al menos un detalle de concepto cuyo centro de costo y cuenta contable estén homologados a un centro de producción en Cost.; Cada liquidación debe tener empleado, tercero, contrato y cargo asociados (joins internos).; Cada cuenta por pagar debe tener tercero y cargo (PositionId) asociados.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionManpower';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ManpowerType=1 corresponde siempre a liquidaciones de nómina cerradas; ManpowerType=2 a cuentas por pagar aprobadas.; Solo se incluyen cuentas por pagar cuyos conceptos tienen homologación vigente a centros de producción de costos.; GroupCodeName solo aplica a la fuente de nómina; en cuentas por pagar siempre es NULL.; Año y mes provienen de la fecha de liquidación de nómina o de la fecha del documento de la cuenta por pagar, según el origen.; El UUID combina tipo de mano de obra y Id de entidad para evitar colisiones entre fuentes.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionManpower';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Mano de obra; Liquidación de nómina; Cuenta por pagar; Tercero; Cargo; Grupo de nómina; Centro de producción; Centro de costo; Homologación contable; Distribución de costos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionManpower';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando RegisterStatus=''C'' en Payroll.Liquidation → se expone como mano de obra tipo 1 (nómina) con grupo, tercero y cargo.; [RETURN_RESULT] N/A: Cuando Status=2 en Payments.AccountPayable y existe al menos un detalle de concepto cuyo CostCenter+Account está homologado a un centro de producción → se expone como mano de obra tipo 2 (cuenta por pagar) con GroupCodeName en NULL.; [RETURN_RESULT] N/A: Cada fila se identifica con UUID = ManpowerType + ''-'' + EntityId, garantizando unicidad entre las dos fuentes.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionManpower';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen = Payroll.Liquidation con RegisterStatus=''C'' → ManpowerType=1, año/mes derivados de PayrollDateLiquidated, incluye GroupCodeName del grupo de nómina.; si Origen = Payments.AccountPayable con Status=2 y conceptos en centros de producción homologados → ManpowerType=2, año/mes derivados de DocumentDate, GroupCodeName=NULL. else Se descartan cuentas por pagar sin conceptos homologados a centros de producción.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionManpower';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.Group; Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.Position; Payments.AccountPayable; Payments.AccountPayableDetailConcept; Cost.CostProductionCenter; Cost.CostProductionCenterCostCenter; Cost.CostProductionCenterHomologation', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionManpower';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewCostDistributionManpower';
GO
