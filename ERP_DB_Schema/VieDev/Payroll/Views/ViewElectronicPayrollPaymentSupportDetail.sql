








CREATE VIEW [Payroll].[ViewElectronicPayrollPaymentSupportDetail]
AS
		SELECT	'Liquidation' EntityName,
				l.Id EntityId,
				ld.Id EntityDetailId,
				c.ConceptType Nature,
				c.ConceptClass,
				c.Name Detail,
				NULL DateStart,
				NULL DateEnd,
				CASE c.ConceptClass
					WHEN '005' THEN l.DaysWorked
					WHEN '008' THEN ld.ConceptTotalValue -- Provisión de Cesantías
					WHEN '031' THEN l.ProvisionDays -- Provisión de Vacaciones
					WHEN '033' THEN l.ProvisionDays -- Provisión de Primas
					WHEN '034' THEN l.ProvisionDays -- Provisión de Int. de Cesantías
					WHEN '023' THEN l.MaternityLeaveDays
					WHEN '021' THEN L.AmbulatoryDisabilityDays
					ELSE ISNULL(ld.TotalNumberHours, 0) 
				END Quantity,
				CASE c.ConceptClass
					WHEN '017' THEN 4
					WHEN '014' THEN 4
					WHEN '038' THEN 1
					WHEN '034' THEN 12
					WHEN '001' THEN 25.00  ---Hora Extra Diurna
					---WHEN '011' THEN 25.00  ---Hora Extra Diurna REAJUSTE HORA EXTRA DIURNA 1,25%
					WHEN '012' THEN 75.00  ---Hora Extra Nocturna
					WHEN '042' THEN 35.00  ---Hora Recargo Nocturno
					---WHEN '010' THEN 35.00  ---Hora Recargo Nocturno  -REINTEGRO RECARGO NOCTURNO NORMAL
					WHEN '013' THEN 100.00  ---Hora Extra Diurna Dominical y Festivos
					WHEN '051' THEN 75.00  ---Hora Recargo Diurno Dominical y Festivos
					WHEN '052' THEN 75.00  --Hora Recargo Diurno Dominical y Festivo
					WHEN '050' THEN 150.00  ---Hora Extra Nocturna Dominical y Festivo
					WHEN '043' THEN 110.00  ---Hora Recargo Nocturno Dominical y Festivos
					ELSE 0
				END Percentage,
				ct.ContractClass,
				ld.ConceptTotalValue Value,
				l.PayrollConfirmationDate ConfirmationDate
		FROM Payroll.Liquidation l WITH (NOLOCK)
		JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
		JOIN Payroll.Concept c WITH (NOLOCK) ON ld.ConceptId = c.Id
		JOIN [Payroll].[Contract] co WITH (NOLOCK)ON co.Id = l.ContractId
		JOIN Payroll.ContractType ct WITH (NOLOCK) ON ct.Id = co.ContractTypeId 
		WHERE c.ConceptType IN (1, 2) AND ld.ConceptClass <> '030' OR ld.ConceptClass IN ('008', '031','033','034')
	UNION ALL
		SELECT	'IncentivePayment' EntityName,
				p.Id EntityId,
				pd.Id EntityDetailId,
				c.ConceptType Nature,
				c.ConceptClass,
				c.Name Detail,
				p.PeriodInitialDate DateStart,
				p.PeriodEndDate DateEnd,
				0 Quantity,
				CASE c.ConceptClass
					WHEN '017' THEN 4
					WHEN '014' THEN 4
					WHEN '038' THEN 1
					ELSE 0
				END Percentage,
				NULL ContractClass,
				IIF(c.ConceptType = 1, pd.AccruedValue, pd.DeductedValue) Value,
				p.ConfirmationDate ConfirmationDate
		FROM Payroll.IncentivePayment p WITH (NOLOCK)
		JOIN Payroll.IncentivePaymentDetail pd WITH (NOLOCK) ON p.Id = pd.IncentivePaymentId
		JOIN Payroll.Concept c WITH (NOLOCK) ON pd.ConceptId = c.Id
		WHERE c.ConceptType IN (1, 2)
	UNION ALL
		SELECT	'ContractLiquidation' EntityName,
				l.Id EntityId,
				ld.Id EntityDetailId,
				c.ConceptType Nature,
				c.ConceptClass,
				c.Name Detail,
				ld.InitialDate DateStart,
				ld.EndingDate DateEnd,
				CASE c.ConceptClass
					WHEN '021' THEN 1
					WHEN '005' THEN 0
					WHEN '030' THEN 0
					WHEN '023' THEN 0
					WHEN '033' THEN DAY(ld.EndingDate) -- Provisión de Primas
					WHEN '031' THEN DAY(ld.EndingDate) -- Provisión de Vacaciones
					WHEN '008' THEN DAY(ld.EndingDate) -- Provisión de Cesantías
					WHEN '034' THEN DAY(ld.EndingDate) -- Provisión de Int. de Cesantías
					ELSE 0
				END Quantity,
				CASE c.ConceptClass
					WHEN '017' THEN 4
					WHEN '014' THEN 4
					WHEN '038' THEN 1
					WHEN '001' THEN 25.00  ---Hora Extra Diurna
					---WHEN '011' THEN 25.00  ---Hora Extra Diurna REAJUSTE HORA EXTRA DIURNA 1,25%
					WHEN '012' THEN 75.00  ---Hora Extra Nocturna
					WHEN '042' THEN 35.00  ---Hora Recargo Nocturno
					---WHEN '010' THEN 35.00  ---Hora Recargo Nocturno  -REINTEGRO RECARGO NOCTURNO NORMAL
					WHEN '013' THEN 100.00  ---Hora Extra Diurna Dominical y Festivos
					WHEN '051' THEN 75.00  ---Hora Recargo Diurno Dominical y Festivos
					WHEN '052' THEN 75.00  --Hora Recargo Diurno Dominical y Festivo
					WHEN '050' THEN 150.00  ---Hora Extra Nocturna Dominical y Festivo
					WHEN '043' THEN 110.00  ---Hora Recargo Nocturno Dominical y Festivos
					ELSE 0
				END Percentage,
				NULL ContractClass,
				IIF(c.ConceptType = 1 or c.ConceptType = 3, ld.Accrued, ld.Deducted) Value,
				CAST(pc.LastModificationDate AS DATETIME) ConfirmationDate
		FROM Payroll.Contract pc WITH (NOLOCK)
		JOIN Payroll.ContractLiquidation l WITH (NOLOCK) ON pc.Id = l.ContractId
		JOIN Payroll.ContractLiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.ContractLiquidationId
		JOIN Payroll.Concept c WITH (NOLOCK) ON ld.IdConcept = c.Id
		WHERE c.ConceptType IN (1,2) AND c.ConceptClass not in ('030', '002') OR c.ConceptClass IN ('008', '031','033','034')