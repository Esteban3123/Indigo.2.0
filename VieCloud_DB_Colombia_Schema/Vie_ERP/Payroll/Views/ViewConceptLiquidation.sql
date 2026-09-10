-- =============================================
-- Author:		Iván Dario Ospina Acosta
-- Create date: 2019-12-03
-- Description:	
-- =============================================

CREATE VIEW [Payroll].[ViewConceptLiquidation]
AS
	WITH cteActiveEmployee AS(
		SELECT
			cntrc.Id AS ContractID,
			emply.Id AS EmployeeID, 
			tParty.[Name] AS Empleado,
			tParty.Nit AS Cédula, 
			grp.Code AS GroupCode,
			grp.[Name] AS Grupo, 
			pstn.[Name] AS Cargo,
			fUnit.[Name] AS UnidadFuncional,
			cntrc.BasicSalary AS Salario
		FROM Common.ThirdParty AS tParty 
		JOIN Payroll.Employee AS emply ON emply.ThirdPartyId = tParty.Id
		CROSS APPLY(
			SELECT TOP 1
				cntrc.Id,
				cntrc.EmployeeId,
				cntrc.PositionId,
				cntrc.GroupId,
				cntrc.FunctionalUnitId,
				cntrc.BasicSalary
			FROM Payroll.[Contract] cntrc
			WHERE cntrc.EmployeeId = emply.Id
			ORDER BY cntrc.Id DESC
		) cntrc
		JOIN Payroll.Position AS pstn ON cntrc.PositionId = pstn.Id 
		JOIN Payroll.[Group] AS grp ON cntrc.GroupId = grp.Id
		JOIN Payroll.FunctionalUnit AS fUnit ON cntrc.FunctionalUnitId = fUnit.Id

	), cteEmployeeConcept AS(
		SELECT
			cteActiveEmployee.ContractID,
			cteActiveEmployee.EmployeeID,
			cteActiveEmployee.Empleado,
			cteActiveEmployee.Cédula,
			cteActiveEmployee.Cargo,
			cteActiveEmployee.GroupCode,
			cteActiveEmployee.Grupo,
			cteActiveEmployee.UnidadFuncional,
			cteActiveEmployee.Salario,
			cncpt.Id AS ConceptID,
			cncpt.Code AS ConceptCode,
			cncpt.[Name] AS Concept,
			cncpt.ConceptType,
			lqd.ConceptTotalValue,
			lqd.PayrollDate,
			lq.RegisterStatus
		FROM cteActiveEmployee 
		JOIN Payroll.Liquidation lq ON lq.ContractId = cteActiveEmployee.ContractID
		JOIN Payroll.LiquidationDetail lqd ON lqd.PayrollId = lq.Id
		JOIN Payroll.Concept cncpt ON cncpt.Id = lqd.ConceptId
	)
	
	SELECT
		ROW_NUMBER() OVER(ORDER BY(SELECT 0)) AS ID
		,ContractID	
		,EmployeeID	
		,Empleado
		,Cédula
		,Cargo
		,GroupCode
		,Grupo
		,UnidadFuncional
		,Salario
		,ConceptID	
		,ConceptCode
		,Concept	
		,ConceptTotalValue
		,ConceptType
		,PayrollDate
		,RegisterStatus
	FROM cteEmployeeConcept
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de liquidación de conceptos de nómina por empleado. Combina el último contrato vigente de cada empleado con sus datos personales (nombre y cédula), cargo, grupo de nómina y unidad funcional, y los cruza con los conceptos liquidados (devengos, deducciones u otros) de cada período de nómina, mostrando el valor total por concepto y la fecha de liquidación. Sirve para reportería y análisis de nómina: permite consultar qué conceptos se le liquidaron a cada trabajador, en qué período, con qué salario base y bajo qué estructura organizacional (grupo, cargo, unidad funcional).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewConceptLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewConceptLiquidation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida los conceptos liquidados en nómina por empleado, mostrando datos del contrato vigente (más reciente), cargo, grupo, unidad funcional y el detalle de cada concepto con su valor liquidado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewConceptLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada empleado debe tener al menos un contrato registrado en Payroll.Contract para aparecer en la vista (se aplica CROSS APPLY TOP 1).; El contrato debe tener PositionId, GroupId y FunctionalUnitId válidos (JOINs internos no nulos).; Deben existir liquidaciones (Payroll.Liquidation) y detalles (Payroll.LiquidationDetail) asociados al contrato para que el empleado aparezca con conceptos.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewConceptLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el contrato más reciente por empleado: CROSS APPLY (SELECT TOP 1 ... ORDER BY cntrc.Id DESC).; Los empleados sin contrato, sin liquidación o sin detalle de liquidación quedan excluidos por los JOIN internos.; El identificador ID generado no es estable entre ejecuciones porque ROW_NUMBER usa ORDER BY (SELECT 0).; Cada fila representa un concepto liquidado (devengado/deducción/aporte según ConceptType) sobre el contrato vigente del empleado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewConceptLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Contrato laboral; Salario básico; Cargo; Grupo de nómina; Unidad funcional; Liquidación de nómina; Concepto de nómina; Tipo de concepto (devengado/deducción/aporte); Fecha de nómina; Estado de registro de liquidación', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewConceptLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewConceptLiquidation: Devuelve un row por cada combinación contrato-liquidación-concepto, asignando un ID secuencial mediante ROW_NUMBER() OVER(ORDER BY (SELECT 0)).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewConceptLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Contract; Payroll.Position; Payroll.Group; Payroll.FunctionalUnit; Payroll.Liquidation; Payroll.LiquidationDetail; Payroll.Concept', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewConceptLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewConceptLiquidation';
GO
