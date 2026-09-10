
CREATE VIEW [Cost].[ViewMainAccountsWithoutParameterization]
AS
SELECT DISTINCT
	CONCAT(glb.Year, '-', glb.Month, '-', ma.Id, '-', cc.Id, '-', tp.Id) Id,
	glb.Year, glb.Month,
	pma.Number ParentAccountNumber,
	pma.Name ParentAccountName,
    ma.Number MainAccountNumber, 
	ma.Name MainAccountName,
    cc.Code CostCenterCode, 
	cc.Name CostCenterName,
	tp.Nit ThirdPartyNit,
	tp.Name ThirdPartyName,
	(glb.DebitValue - glb.CreditValue) * IIF(mac.Nature = 1, 1, -1) Balance
FROM GeneralLedger.GeneralLedgerBalance glb WITH (NOLOCK)
JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON glb.IdMainAccount = ma.Id
JOIN GeneralLedger.LegalBook lb WITH (NOLOCK) ON ma.LegalBookId = lb.Id AND lb.OfficialBook = 1
JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
JOIN Payroll.CostCenter cc WITH (NOLOCK) ON glb.IdCostCenter = cc.Id
LEFT JOIN GeneralLedger.MainAccounts pma WITH (NOLOCK) ON lb.Id = pma.LegalBookId AND SUBSTRING(ma.Number, 1, 4) = pma.Number
LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON glb.IdThirdParty = tp.Id
LEFT JOIN
(
    SELECT cpch.AccountOriginId, cpccc.CostCenterId
    FROM Cost.CostProductionCenterHomologation cpch WITH (NOLOCK)
    JOIN Cost.CostProductionCenterCostCenter cpccc WITH (NOLOCK) ON cpch.ProductionCenterId = cpccc.ProductionCenterId
    GROUP BY cpch.AccountOriginId, cpccc.CostCenterId
) pc ON glb.IdMainAccount = pc.AccountOriginId AND ISNULL(glb.IdCostCenter, 0) = pc.CostCenterId
WHERE mac.Type = 2 AND (glb.DebitValue - glb.CreditValue) <> 0
    AND pc.AccountOriginId IS NULL
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuentas contables del libro oficial que tienen movimientos (saldo débito-crédito distinto de cero) pero que aún NO están parametrizadas en la homologación de centros de producción para el módulo de costos. Cruza los saldos del libro mayor con sus cuentas principales, cuentas padre a 4 dígitos, centros de costo de nómina y terceros (proveedores, aseguradoras, etc.), calculando el saldo ajustado según la naturaleza de la cuenta (débito o crédito). Sirve como reporte de control y auditoría para identificar qué combinaciones de cuenta contable y centro de costo tienen movimiento económico real pero todavía no fueron incorporadas al proceso de distribución y reclasificación de costos, permitiendo al área de costos completar la parametrización faltante.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewMainAccountsWithoutParameterization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewMainAccountsWithoutParameterization';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Identifica saldos contables de cuentas de resultado del libro oficial que no tienen parametrizada su homologación a centros de producción de costos, para detectar configuraciones faltantes.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMainAccountsWithoutParameterization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un LegalBook marcado como OfficialBook = 1; Las cuentas contables deben tener clase asociada (MainAccountClasses) con su naturaleza definida; Los saldos en GeneralLedgerBalance deben estar asociados a una cuenta principal y centro de costo válidos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMainAccountsWithoutParameterization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran cuentas pertenecientes al libro contable oficial (OfficialBook = 1); Solo se incluyen cuentas cuya clase tenga Type = 2; Se excluyen registros con saldo neto cero (DebitValue - CreditValue = 0); El balance siempre se ajusta según la naturaleza de la clase de cuenta (débito positivo, crédito negativo); La cuenta padre se determina por los primeros 4 caracteres del número de cuenta dentro del mismo libro legal; Si IdCostCenter es NULL en el saldo, se compara contra 0 al verificar homologación', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMainAccountsWithoutParameterization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Saldo contable; Plan de cuentas (PUC); Cuenta principal y cuenta padre; Naturaleza débito/crédito; Libro contable oficial; Centro de costo; Centro de producción; Tercero (NIT); Homologación de cuentas para costos; Cuentas de resultado', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMainAccountsWithoutParameterization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve solo registros donde mac.Type = 2 (cuentas de resultado), el saldo neto (Débito - Crédito) sea distinto de cero y la cuenta no tenga homologación parametrizada (pc.AccountOriginId IS NULL)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMainAccountsWithoutParameterization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si mac.Nature = 1 (naturaleza débito) → El balance se calcula como (DebitValue - CreditValue) * 1 else El balance se calcula como (DebitValue - CreditValue) * -1 (invierte el signo para naturaleza crédito); si Existe coincidencia entre IdMainAccount/IdCostCenter del saldo y la homologación de centros de producción → El registro se filtra/excluye porque sí está parametrizado else El registro se incluye en el resultado como cuenta sin parametrización', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMainAccountsWithoutParameterization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; GeneralLedger.LegalBook; GeneralLedger.MainAccountClasses; Payroll.CostCenter; Common.ThirdParty; Cost.CostProductionCenterHomologation; Cost.CostProductionCenterCostCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMainAccountsWithoutParameterization';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewMainAccountsWithoutParameterization';
GO
