
			
			CREATE VIEW [Payroll].[ViewPayrollKardex]
			AS
			SELECT LD.Id, E.Id as IdEmployee, TP.Nit, TP.Name, L.PayrollDateLiquidated AS Mes, C.Code as CodigoConcepto, C.Name as NombreConcepto, 
			CASE WHEN C.ConceptType = 1 THEN 'DEVENGADO' ELSE 'DEDUCIDO' END AS TipoConcepto, SUM(LD.ConceptTotalValue) AS ValorConcepto
			FROM Payroll.LiquidationDetail LD, Payroll.Liquidation L, Payroll.Concept C, Payroll.Employee E, Common.ThirdParty TP
			WHERE LD.PayrollId = L.Id and C.Id = LD.ConceptId and L.RegisterStatus <> '' AND L.EmployeeId = E.Id AND E.ThirdPartyId = TP.Id AND C.ConceptType <> 3
			GROUP BY LD.Id, E.Id, TP.Nit, TP.Name, l.PayrollDateLiquidated, C.Code, C.Name, C.ConceptType
			
			UNION ALL

			SELECT RD.Id, E.Id as IdEmployee, TP.Nit, TP.Name, RC.NextPayrollDate as Mes, C.Code, C.Name, 
			CASE WHEN C.ConceptType = 1 THEN 'DEVENGADO' ELSE 'DEDUCIDO' END AS TipoConcepto, RD.ValueConceptWithRetroactive 
			FROM Payroll.RetroactiveC RC, Payroll.RetroactiveD RD, Payroll.Concept c, Payroll.Employee E, Common.ThirdParty TP
			WHERE RD.IdRetroactiveC = RC.Id and C.Id = RD.IdConcept AND C.ConceptType <> 3
	
			UNION ALL
	
			SELECT IP.ID, E.Id as IdEmployee, TP.Nit, TP.Name, IP.PeriodEndDate as Mes,
			(SELECT C.Code FROM Payroll.PayrollSettings PS, Payroll.Concept C WHERE C.Id = PS.ServicesIncentivePaymentConceptId) AS CodigoConcepto,
			(SELECT C.Name FROM Payroll.PayrollSettings PS, Payroll.Concept C WHERE C.Id = PS.ServicesIncentivePaymentConceptId) AS NombreConcepto,
			(SELECT CASE WHEN C.ConceptType = 1 THEN 'DEVENGADO' ELSE 'DEDUCIDO' END FROM Payroll.PayrollSettings PS, Payroll.Concept C WHERE C.Id = PS.ServicesIncentivePaymentConceptId) AS TipoConcepto,
			IP.TotalAccrued 
			FROM Payroll.IncentivePayment IP, Payroll.Contract c, Payroll.Employee E, Common.ThirdParty TP
			where IP.ContractId = c.Id AND c.EmployeeId = E.Id AND E.ThirdPartyId = TP.Id and ip.[Period] = 1

			UNION ALL
	
			SELECT IP.ID, E.Id as IdEmployee, TP.Nit, TP.Name, IP.PeriodEndDate as Mes,
			(SELECT C.Code FROM Payroll.PayrollSettings PS, Payroll.Concept C WHERE C.Id = PS.ChristmasIncentivePaymentConceptId) AS CodigoConcepto,
			(SELECT C.Name FROM Payroll.PayrollSettings PS, Payroll.Concept C WHERE C.Id = PS.ChristmasIncentivePaymentConceptId) AS NombreConcepto,
			(SELECT CASE WHEN C.ConceptType = 1 THEN 'DEVENGADO' ELSE 'DEDUCIDO' END FROM Payroll.PayrollSettings PS, Payroll.Concept C WHERE C.Id = PS.ChristmasIncentivePaymentConceptId) AS TipoConcepto,
			IP.TotalAccrued 
			FROM Payroll.IncentivePayment IP, Payroll.Contract c, Payroll.Employee E, Common.ThirdParty TP
			where IP.ContractId = c.Id AND c.EmployeeId = E.Id AND E.ThirdPartyId = TP.Id and ip.[Period] = 2
			
			UNION ALL

			-- RETIROS
			SELECT CLD.Id, E.Id as IdEmployee, TP.Nit, TP.Name, CL.RetirementDate as Mes, C.Code, C.Name, 
			CASE WHEN C.ConceptType = 1 THEN 'DEVENGADO' ELSE 'DEDUCIDO' END AS ConceptType, SUM(CLD.Accrued) + SUM(CLD.Deducted) as ValorConcepto 
			FROM Payroll.ContractLiquidation CL, Payroll.ContractLiquidationDetail CLD, Payroll.Concept C, Payroll.Employee E, Common.ThirdParty TP
			where CLD.ContractLiquidationId = CL.ID AND C.Id = CLD.IdConcept AND CL.EmployeeId = E.Id AND E.ThirdPartyId = TP.Id AND C.ConceptType <> 3
			group by CLD.Id, E.Id, TP.Nit, TP.Name, CL.RetirementDate, C.Code, C.Name, C.ConceptType
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Kardex consolidado de nómina por empleado: integra en una sola consulta todos los movimientos de devengados y deducciones que afectan a cada trabajador, sin importar su origen. Combina liquidaciones ordinarias de nómina, ajustes retroactivos, incentivos por servicios, incentivos navideños y liquidaciones por retiro o desvinculación. Para cada movimiento muestra el NIT e identificación del empleado, el período (mes), el código y nombre del concepto de nómina, si es devengado o deducción, y el valor total. Sirve como reporte maestro de historial salarial y de pagos por empleado, útil para auditoría de nómina, verificación de conceptos liquidados, análisis de novedades y soporte a procesos de revisión de remuneración.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewPayrollKardex';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewPayrollKardex';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una vista única tipo kárdex todos los movimientos de conceptos de nómina por empleado: liquidaciones ordinarias, retroactivos, primas de servicios, prima de navidad y liquidaciones de contrato por retiro.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewPayrollKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las liquidaciones (Payroll.Liquidation) deben tener RegisterStatus distinto de cadena vacía para incluirse.; Existe configuración en Payroll.PayrollSettings con los conceptos ServicesIncentivePaymentConceptId y ChristmasIncentivePaymentConceptId definidos.; Cada empleado debe tener ThirdParty asociado (E.ThirdPartyId) para resolver Nit y Nombre.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewPayrollKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los conceptos con ConceptType = 3 nunca aparecen en el kárdex.; La clasificación TipoConcepto es binaria: ''DEVENGADO'' si ConceptType=1, ''DEDUCIDO'' en caso contrario.; Los pagos de incentivos solo se reportan para Period=1 (servicios) y Period=2 (navidad); otros períodos no se incluyen.; El valor monetario reportado siempre se agrega: SUM(ConceptTotalValue) en liquidaciones ordinarias y SUM(Accrued)+SUM(Deducted) en liquidaciones de retiro.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewPayrollKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kárdex de nómina; Concepto devengado; Concepto deducido; Liquidación de nómina; Retroactivo salarial; Prima de servicios; Prima de navidad; Liquidación de contrato por retiro; Empleado; Tercero (NIT)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewPayrollKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewPayrollKardex: Devuelve filas tipo DEVENGADO cuando C.ConceptType = 1 y DEDUCIDO en cualquier otro caso (excluyendo siempre ConceptType = 3).; [RETURN_RESULT] Payroll.ViewPayrollKardex: Filtra IncentivePayment con Period = 1 y lo etiqueta con el concepto configurado en PayrollSettings.ServicesIncentivePaymentConceptId (prima de servicios).; [RETURN_RESULT] Payroll.ViewPayrollKardex: Filtra IncentivePayment con Period = 2 y lo etiqueta con el concepto configurado en PayrollSettings.ChristmasIncentivePaymentConceptId (prima de navidad).; [RETURN_RESULT] Payroll.ViewPayrollKardex: Para retiros, suma Accrued + Deducted (SUM(CLD.Accrued) + SUM(CLD.Deducted)) como valor del concepto, usando RetirementDate como mes.; [RETURN_RESULT] Payroll.ViewPayrollKardex: Para retroactivos usa NextPayrollDate como mes y ValueConceptWithRetroactive como valor del concepto.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewPayrollKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.ConceptType = 1 → Marca el concepto como ''DEVENGADO'' else Marca como ''DEDUCIDO'' (aplica a todos los bloques UNION); si C.ConceptType <> 3 → Incluye el concepto en la vista else Excluye del kárdex (no aparece en bloques de Liquidation, Retroactive ni ContractLiquidation); si IP.Period = 1 → Asocia el pago al concepto de prima de servicios (ServicesIncentivePaymentConceptId); si IP.Period = 2 → Asocia el pago al concepto de prima de navidad (ChristmasIncentivePaymentConceptId); si L.RegisterStatus <> '''' → Incluye el detalle de liquidación ordinaria en el kárdex else Lo excluye', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewPayrollKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.LiquidationDetail; Payroll.Liquidation; Payroll.Concept; Payroll.Employee; Common.ThirdParty; Payroll.RetroactiveC; Payroll.RetroactiveD; Payroll.IncentivePayment; Payroll.Contract; Payroll.PayrollSettings; Payroll.ContractLiquidation; Payroll.ContractLiquidationDetail', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewPayrollKardex';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewPayrollKardex';
GO
