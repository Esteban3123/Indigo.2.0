
CREATE VIEW [Payroll].[ViewElectronicPayrollPaymentSupportDetail]
AS
	-- =====================================================================
	-- LIQUIDACIÓN REGULAR DE NÓMINA
	-- =====================================================================
	SELECT	'Liquidation' EntityName,
			l.Id EntityId,
			ld.Id EntityDetailId,
			epc.ConceptType Nature,  
			epc.InternalCode Type,          
			ISNULL(epcs.InternalSubCode, 0) Subtype,    
			c.ConceptClass,
			c.Name Detail,
			NULL DateStart,
			NULL DateEnd,
			CASE c.ConceptClass
				WHEN '005' THEN l.DaysWorked                      -- Sueldo
				WHEN '008' THEN ld.ConceptTotalValue              -- Provisión de Cesantías
				WHEN '031' THEN l.ProvisionDays                   -- Provisión de Vacaciones
				WHEN '033' THEN l.ProvisionDays                   -- Provisión de Primas
				WHEN '034' THEN l.ProvisionDays                   -- Provisión de Int. de Cesantías
				ELSE COALESCE(NULLIF(ld.Quantity, 0), ld.TotalNumberHours, 0)
			END Quantity,
			CASE c.ConceptClass
				WHEN '017' THEN 4                                 -- Aporte Salud (4%)
				WHEN '014' THEN 4                                 -- Aporte Pensión (4%)
				WHEN '038' THEN 1                                 -- FSP (1%)
				WHEN '034' THEN 12                                -- Int. Cesantías (12%)
				WHEN '001' THEN 25.00                             -- Hora Extra Diurna (25%)
				WHEN '012' THEN 75.00                             -- Hora Extra Nocturna (75%)
				WHEN '042' THEN 35.00                             -- Hora Recargo Nocturno (35%)
				WHEN '013' THEN 100.00                            -- Hora Extra Diurna Dom/Fest (100%)
				WHEN '051' THEN 75.00                             -- Hora Recargo Diurno Dom/Fest (75%)
				WHEN '052' THEN 75.00                             -- Hora Recargo Diurno Dom/Fest (75%)
				WHEN '050' THEN 150.00                            -- Hora Extra Nocturna Dom/Fest (150%)
				WHEN '043' THEN 110.00                            -- Hora Recargo Nocturno Dom/Fest (110%)
				ELSE 0
			END Percentage,
			ct.ContractClass,
			ld.ConceptTotalValue Value,
			l.PayrollConfirmationDate ConfirmationDate
	FROM Payroll.Liquidation l WITH (NOLOCK)
	JOIN Payroll.LiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.PayrollId
	JOIN Payroll.Concept c WITH (NOLOCK) ON ld.ConceptId = c.Id
	INNER JOIN Payroll.ElectronicPayrollConcepts epc WITH (NOLOCK)  ON c.IdElectronicPayrollConcepts = epc.Id
	LEFT JOIN Payroll.ElectronicPayrollConceptSubtype epcs WITH (NOLOCK) ON c.IdElectronicPayrollConceptSubtype = epcs.Id 
	JOIN [Payroll].[Contract] co WITH (NOLOCK) ON co.Id = l.ContractId
	JOIN Payroll.ContractType ct WITH (NOLOCK) ON ct.Id = co.ContractTypeId 
	WHERE epc.ConceptType IN (1, 2)                                  
	  AND epc.State = 1    
	  
UNION ALL

	-- =====================================================================
	-- PAGO DE INCENTIVOS
	-- =====================================================================
	SELECT	'IncentivePayment' EntityName,
			p.Id EntityId,
			pd.Id EntityDetailId,
			epc.ConceptType Nature,   
			epc.InternalCode Type,          
			ISNULL(epcs.InternalSubCode, 0) Subtype, 
			c.ConceptClass,
			c.Name Detail,
			p.PeriodInitialDate DateStart,
			p.PeriodEndDate DateEnd,
			0 Quantity,
			CASE c.ConceptClass
				WHEN '017' THEN 4                                    -- Aporte Salud (4%)
				WHEN '014' THEN 4                                    -- Aporte Pensión (4%)
				WHEN '038' THEN 1                                    -- FSP (1%)
				ELSE 0
			END Percentage,
			NULL ContractClass,
			IIF(epc.ConceptType = 1, pd.AccruedValue, pd.DeductedValue) Value,  
			p.ConfirmationDate ConfirmationDate
	FROM Payroll.IncentivePayment p WITH (NOLOCK)
	JOIN Payroll.IncentivePaymentDetail pd WITH (NOLOCK) ON p.Id = pd.IncentivePaymentId
	JOIN Payroll.Concept c WITH (NOLOCK) ON pd.ConceptId = c.Id
	INNER JOIN Payroll.ElectronicPayrollConcepts epc WITH (NOLOCK)  ON c.IdElectronicPayrollConcepts = epc.Id
	LEFT JOIN Payroll.ElectronicPayrollConceptSubtype epcs WITH (NOLOCK) ON c.IdElectronicPayrollConceptSubtype = epcs.Id  
	WHERE epc.ConceptType IN (1, 2)                                 
	  AND epc.State = 1                                             
	  
UNION ALL

	-- =====================================================================
	-- LIQUIDACIÓN DE CONTRATO (RETIRO)
	-- =====================================================================
	SELECT	'ContractLiquidation' EntityName,
			l.Id EntityId,
			ld.Id EntityDetailId,
			epc.ConceptType Nature,   
			epc.InternalCode Type,          
			ISNULL(epcs.InternalSubCode, 0) Subtype, 
			c.ConceptClass,
			c.Name Detail,
			ld.InitialDate DateStart,
			ld.EndingDate DateEnd,
			CASE c.ConceptClass
				WHEN '021' THEN 1                                    -- Incapacidad
				WHEN '005' THEN 0                                    -- Sueldo
				WHEN '030' THEN 0                                    -- Vacaciones
				WHEN '023' THEN 0                                    -- Maternidad
				WHEN '033' THEN DAY(ld.EndingDate)                   -- Provisión de Primas
				WHEN '031' THEN DAY(ld.EndingDate)                   -- Provisión de Vacaciones
				WHEN '008' THEN DAY(ld.EndingDate)                   -- Provisión de Cesantías
				WHEN '034' THEN DAY(ld.EndingDate)                   -- Provisión de Int. de Cesantías
				ELSE 0
			END Quantity,
			CASE c.ConceptClass
				WHEN '017' THEN 4                                    -- Aporte Salud (4%)
				WHEN '014' THEN 4                                    -- Aporte Pensión (4%)
				WHEN '038' THEN 1                                    -- FSP (1%)
				WHEN '001' THEN 25.00                                -- Hora Extra Diurna (25%)
				WHEN '012' THEN 75.00                                -- Hora Extra Nocturna (75%)
				WHEN '042' THEN 35.00                                -- Hora Recargo Nocturno (35%)
				WHEN '013' THEN 100.00                               -- Hora Extra Diurna Dom/Fest (100%)
				WHEN '051' THEN 75.00                                -- Hora Recargo Diurno Dom/Fest (75%)
				WHEN '052' THEN 75.00                                -- Hora Recargo Diurno Dom/Fest (75%)
				WHEN '050' THEN 150.00                               -- Hora Extra Nocturna Dom/Fest (150%)
				WHEN '043' THEN 110.00                               -- Hora Recargo Nocturno Dom/Fest (110%)
				ELSE 0
			END Percentage,
			NULL ContractClass,
			IIF(epc.ConceptType = 1 OR epc.ConceptType = 3, ld.Accrued, ld.Deducted) Value,  
			CAST(pc.LastModificationDate AS DATETIME) ConfirmationDate
	FROM Payroll.Contract pc WITH (NOLOCK)
	JOIN Payroll.ContractLiquidation l WITH (NOLOCK) ON pc.Id = l.ContractId
	JOIN Payroll.ContractLiquidationDetail ld WITH (NOLOCK) ON l.Id = ld.ContractLiquidationId
	JOIN Payroll.Concept c WITH (NOLOCK) ON ld.IdConcept = c.Id
	INNER JOIN Payroll.ElectronicPayrollConcepts epc WITH (NOLOCK)  ON c.IdElectronicPayrollConcepts = epc.Id
	LEFT JOIN Payroll.ElectronicPayrollConceptSubtype epcs WITH (NOLOCK) ON c.IdElectronicPayrollConceptSubtype = epcs.Id  
	WHERE epc.ConceptType IN (1, 2)                                 
	  AND epc.State = 1                    
	  

