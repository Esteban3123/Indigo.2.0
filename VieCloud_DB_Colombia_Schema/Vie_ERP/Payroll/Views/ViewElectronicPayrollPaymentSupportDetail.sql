
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de conceptos de nómina electrónica para la generación del Documento Soporte de Pago de Nómina Electrónica exigido por la DIAN. Integra tres fuentes de pago: liquidaciones regulares de nómina, pagos de incentivos y liquidaciones de contrato por retiro, unificando en una sola consulta la naturaleza del concepto (devengado o deducción), el tipo y subtipo según la clasificación de nómina electrónica, la cantidad, el porcentaje aplicable (por ejemplo, aportes a salud 4%, pensión 4%, horas extras 25%-155%), y el valor total liquidado por concepto y empleado. Utiliza los catálogos de conceptos de nómina, tipos de contrato y subtipos de nómina electrónica para enriquecer cada línea con el código interno DIAN, la clase de concepto y la clase de contrato laboral. Sirve como base para reportes de nómina electrónica, auditoría de devengados y deducciones, y validación de soporte de pago ante la DIAN.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewElectronicPayrollPaymentSupportDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewElectronicPayrollPaymentSupportDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista los conceptos a reportar en la nómina electrónica (DIAN), unificando devengados y deducciones provenientes de la liquidación regular, los pagos de incentivos y la liquidación de contrato por retiro.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollPaymentSupportDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia del mapeo entre Payroll.Concept y Payroll.ElectronicPayrollConcepts vía c.IdElectronicPayrollConcepts (obligatorio para que el registro aparezca); El concepto electrónico debe estar en estado activo (epc.State = 1) y de tipo 1 ó 2 (devengado/deducción) para ser incluido; Para liquidación regular: el contrato del empleado y su tipo de contrato deben existir (Payroll.Contract y Payroll.ContractType); Los porcentajes y cantidades dependen de que ConceptClass esté correctamente codificado en el catálogo de conceptos', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollPaymentSupportDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se exponen registros cuyo concepto de nómina electrónica esté activo (epc.State = 1); Sólo se incluyen conceptos cuya naturaleza (ConceptType) sea 1 (devengado) o 2 (deducción); en liquidación de contrato adicionalmente se trata el ConceptType=3 como devengado para el cálculo del valor; Cada fila debe tener un ElectronicPayrollConcept asociado (INNER JOIN obligatorio); el subtipo es opcional y por defecto se reporta 0 si no existe; El campo EntityName identifica la fuente: ''Liquidation'', ''IncentivePayment'' o ''ContractLiquidation''; ContractClass solo se reporta para la liquidación regular de nómina; en pagos de incentivos y liquidación de contrato se devuelve NULL; Las fechas DateStart/DateEnd están vacías para nómina regular, corresponden al período del incentivo, o al rango del detalle de liquidación de contrato', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollPaymentSupportDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nómina electrónica; Liquidación de nómina; Liquidación de contrato (retiro); Pago de incentivos; Devengados; Deducciones; Aporte salud; Aporte pensión; FSP (Fondo de Solidaridad Pensional); Cesantías y sus intereses; Provisión de primas y vacaciones; Horas extras y recargos (diurnos, nocturnos, dominicales/festivos); Incapacidad; Maternidad; Tipo de contrato', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollPaymentSupportDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewElectronicPayrollPaymentSupportDetail: Para nómina regular: devuelve filas etiquetadas EntityName=''Liquidation'' uniendo Liquidation/LiquidationDetail/Concept/Contract/ContractType, calculando Quantity y Percentage según ConceptClass y filtrando epc.ConceptType IN (1,2) AND epc.State=1; [RETURN_RESULT] Payroll.ViewElectronicPayrollPaymentSupportDetail: Para incentivos: devuelve filas EntityName=''IncentivePayment'' con Value = AccruedValue si ConceptType=1 o DeductedValue en otro caso, Quantity siempre 0, ContractClass NULL y filtrando epc.ConceptType IN (1,2) AND epc.State=1; [RETURN_RESULT] Payroll.ViewElectronicPayrollPaymentSupportDetail: Para liquidación de contrato: devuelve filas EntityName=''ContractLiquidation'' con Value = ld.Accrued cuando ConceptType IN (1,3) y ld.Deducted en otro caso; ConfirmationDate = pc.LastModificationDate; filtrando epc.ConceptType IN (1,2) AND epc.State=1', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollPaymentSupportDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ConceptClass de la liquidación regular → Quantity = DaysWorked (005), ConceptTotalValue (008), ProvisionDays (031, 033, 034); en otros casos COALESCE(NULLIF(Quantity,0), TotalNumberHours, 0); si ConceptClass para cálculo de Percentage (común a las tres ramas) → Asigna porcentaje fijo: 017→4, 014→4, 038→1, 034→12 (sólo liquidación regular), 001→25, 012→75, 042→35, 013→100, 051→75, 052→75, 050→150, 043→110; el resto 0; si ConceptClass en liquidación de contrato (retiro) → Quantity = 1 para incapacidad (021); 0 para sueldo/vacaciones/maternidad (005, 030, 023); DAY(EndingDate) para provisiones (033, 031, 008, 034); 0 en los demás; si epc.ConceptType = 1 (devengado) en pago de incentivos → Value = pd.AccruedValue else Value = pd.DeductedValue; si epc.ConceptType IN (1,3) en liquidación de contrato → Value = ld.Accrued else Value = ld.Deducted', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollPaymentSupportDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.LiquidationDetail; Payroll.Concept; Payroll.ElectronicPayrollConcepts; Payroll.ElectronicPayrollConceptSubtype; Payroll.Contract; Payroll.ContractType; Payroll.IncentivePayment; Payroll.IncentivePaymentDetail; Payroll.ContractLiquidation; Payroll.ContractLiquidationDetail', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollPaymentSupportDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewElectronicPayrollPaymentSupportDetail';
GO
