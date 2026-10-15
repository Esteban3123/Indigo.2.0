
			
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