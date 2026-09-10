

CREATE VIEW [Payroll].[VSurcharges]
AS
WITH cteA AS(
	SELECT DISTINCT 
		TP.Nit, 
		TP.[Name] AS Nombre, 
		C.Code AS CodigoConcepto, 
		C.[Name] AS NombreConcepto, 
		SUM(LD.TotalNumberHours) AS TotalHoras,
		LD.ConceptTotalValue AS ValorTotal, 
		L.PayrollDateLiquidated AS Fecha, 
		L.RegisterStatus AS Estado, 
		L.EmployeeId AS Empleado, 
		L.ContractId AS ContratoP, 
		G.Code AS GrupoCod, 
		G.[Name] AS GrupoN
	FROM Common.ThirdParty AS TP 
	JOIN Payroll.Employee AS E ON TP.Id = E.ThirdPartyId 
	JOIN Payroll.Liquidation AS L ON L.EmployeeId = E.Id
	JOIN Payroll.LiquidationDetail AS LD ON LD.PayrollId = L.Id
	JOIN Payroll.Concept AS C ON LD.ConceptId = C.Id 
	JOIN Payroll.[Group] AS G ON L.GroupId = G.Id
	WHERE (LD.TotalNumberHours > 0) AND (C.Code <> '001')
	GROUP BY TP.Nit, TP.[Name], C.Code, C.[Name], LD.ConceptTotalValue, L.PayrollDateLiquidated, L.RegisterStatus, L.EmployeeId, L.ContractId, G.Code, G.[Name]
	--UNION ALL
	--SELECT DISTINCT 
	--	TP.Nit, 
	--	TP.[Name], 
	--	C.Code, 
	--	C.[Name] AS Expr1, 
	--	MC.QuoteValue AS TotalHoras, 
	--	LD.ConceptTotalValue AS ValorTotal, 
	--	L.PayrollDateLiquidated AS Fecha, 
	--	L.RegisterStatus AS Estado, 
	--	L.EmployeeId AS Empleado, 
	--	L.ContractId AS ContratoP, 
	--	G.Code AS GrupoCod, 
	--	G.[Name] AS GrupoN
	--FROM Payroll.Employee AS E 
	--JOIN Common.ThirdParty AS TP ON E.ThirdPartyId = TP.Id 
	--JOIN Payroll.ManualConcepts AS MC ON E.Id = MC.EmployeeId 
	--JOIN Payroll.Concept AS C ON MC.ConceptId = C.Id 
	--JOIN Payroll.LiquidationDetail AS LD ON MC.ConceptId = LD.ConceptId AND C.Id = LD.ConceptId 
	--JOIN Payroll.Liquidation AS L ON LD.PayrollId = L.Id AND E.Id = L.EmployeeId AND MONTH(MC.InitialDate) = MONTH(L.PayrollDateLiquidated) AND YEAR(MC.InitialDate) = YEAR(L.PayrollDateLiquidated) 
	--JOIN Payroll.[Group] AS G ON L.GroupId = G.Id
	--WHERE (C.Code <> '001') AND (C.ConceptClass IN ('001', '012', '013')) AND (MC.State <> 3)
	--GROUP BY 
	--	TP.Nit, 
	--	TP.[Name], 
	--	C.Code, 
	--	C.[Name], 
	--	MC.QuoteValue, 
	--	LD.ConceptTotalValue, 
	--	L.PayrollDateLiquidated, 
	--	L.RegisterStatus, 
	--	L.EmployeeId, 
	--	L.ContractId, 
	--	G.Code, 
	--	G.[Name]
)

SELECT
	Nit + CodigoConcepto + cast(ContratoP as varchar) + convert(varchar, Fecha, 120) as RowString, 
	Nit as Cedula, 
	Nombre as NombreEmpleado , 
	CodigoConcepto, 
	NombreConcepto, 
	sum(TotalHoras) as TotalHoras,
	ValorTotal, 
	Fecha, 
	Estado, 
	Empleado, 
	ContratoP, 
	GrupoCod, 
	GrupoN,
	fUnit.BranchOfficeId
FROM cteA
OUTER APPLY(
	SELECT TOP 1
		fUnit.BranchOfficeId
	FROM Payroll.[Contract] cntrc
	JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = cntrc.FunctionalUnitId
	WHERE cntrc.Id = cteA.ContratoP
	ORDER BY cntrc.Id DESC
) fUnit
GROUP BY 
	Nit, 
	Nombre,
	CodigoConcepto, 
	NombreConcepto, 
	ValorTotal, 
	Fecha, 
	Estado, 
	Empleado, 
	ContratoP, 
	GrupoCod, 
	GrupoN,
	fUnit.BranchOfficeId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de recargos y horas extras liquidadas en nómina por empleado y período. Consolida, para cada trabajador, los conceptos de nómina que registran horas (distintos al concepto base ''001''), mostrando el total de horas, el valor liquidado, la fecha de liquidación, el grupo de nómina y la sede o sucursal asociada al contrato. Integra información de empleados, terceros (cédula y nombre), liquidaciones, detalle de conceptos y unidad funcional/contrato para permitir reportes de recargos nocturnos, dominicales, festivos u horas extras pagadas en cada ciclo de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'VSurcharges';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'VSurcharges';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida los recargos y horas adicionales liquidados por empleado, concepto, contrato y período de nómina, mostrando totales de horas y valor junto con la sede (BranchOffice) asociada al contrato.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VSurcharges';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle de liquidación debe tener TotalNumberHours > 0 para ser considerado.; El concepto no debe ser el código ''001'' (excluye el concepto base, típicamente sueldo).; El contrato referenciado debe existir en Payroll.Contract con una FunctionalUnit válida para resolver la sede.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VSurcharges';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca incluye el concepto con Code = ''001''.; Nunca incluye registros de liquidación sin horas (TotalNumberHours <= 0).; La clave RowString se construye concatenando Nit + CodigoConcepto + ContratoP + Fecha (formato 120), garantizando unicidad lógica por empleado-concepto-contrato-fecha.; La sede (BranchOfficeId) se resuelve vía la unidad funcional del contrato, tomando solo un registro por contrato.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VSurcharges';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recargos de nómina; Horas extras / horas adicionales; Liquidación de nómina; Conceptos de nómina; Contrato laboral; Unidad funcional; Sede (BranchOffice); Grupo de nómina; Empleado; Tercero (Nit)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VSurcharges';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.VSurcharges: Devuelve una fila por combinación Nit+CodigoConcepto+ContratoP+Fecha (RowString como clave compuesta), agregando la suma de horas (TotalHoras) por empleado/concepto/contrato/período.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VSurcharges';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LD.TotalNumberHours > 0 AND C.Code <> ''001'' → Incluye el detalle de liquidación en el resultado de recargos. else Excluye el registro (no aparece en la vista).; si OUTER APPLY TOP 1 ... ORDER BY cntrc.Id DESC sobre Payroll.Contract → Asocia la BranchOfficeId de la unidad funcional del contrato con mayor Id que coincida con ContratoP. else Si no hay contrato coincidente, BranchOfficeId queda NULL (OUTER APPLY).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VSurcharges';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Liquidation; Payroll.LiquidationDetail; Payroll.Concept; Payroll.Group; Payroll.Contract; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VSurcharges';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'VSurcharges';
GO
