
CREATE VIEW [Payroll].[vConceptLiquidation]
AS
	WITH cteActiveEmployee AS(
		SELECT
			cntrc.Id AS ContractID,
			emply.Id AS EmployeeID, 
			tParty.[Name] AS Employee,
			tParty.Nit AS Cédula, 
			grp.[Name] AS Grupo, 
			pstn.[Name] AS Cargo,
			fUnit.[Name] AS UnidadFuncional,
			cntrc.BasicSalary AS Salario
		FROM Common.ThirdParty AS tParty 
		JOIN Payroll.Employee AS emply ON emply.ThirdPartyId = tParty.Id 
		JOIN Payroll.[Contract] AS cntrc ON cntrc.EmployeeId = emply.Id 
		JOIN Payroll.Position AS pstn ON cntrc.PositionId = pstn.Id 
		JOIN Payroll.[Group] AS grp ON cntrc.GroupId = grp.Id
		JOIN Payroll.FunctionalUnit AS fUnit ON cntrc.FunctionalUnitId = fUnit.Id

	), cteEmployeeConcept AS(
		SELECT
			cteActiveEmployee.ContractID,
			cteActiveEmployee.EmployeeID,
			cteActiveEmployee.Employee,
			cteActiveEmployee.Cédula,
			cteActiveEmployee.Cargo,
			cteActiveEmployee.Grupo,
			cteActiveEmployee.UnidadFuncional,
			cteActiveEmployee.Salario,
			cncpt.Id AS ConceptID,
			cncpt.[Name] AS Concept,
			lqd.ConceptTotalValue,
			lqd.PayrollDate
		FROM cteActiveEmployee 
		JOIN Payroll.Liquidation lq ON lq.ContractId = cteActiveEmployee.ContractID
		JOIN Payroll.LiquidationDetail lqd ON lqd.PayrollId = lq.Id
		JOIN Payroll.Concept cncpt ON cncpt.Id = lqd.ConceptId
	)
	
	SELECT
		ROW_NUMBER() OVER(ORDER BY(SELECT 0)) AS ID
		,ContractID	
		,EmployeeID	
		,Employee
		,Cédula
		,Cargo
		,Grupo
		,UnidadFuncional
		,Salario
		,ConceptID	
		,Concept	
		,ConceptTotalValue	
		,PayrollDate
	FROM cteEmployeeConcept
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de conceptos liquidados en nómina por empleado y período de pago. Integra información del empleado (nombre, cédula, cargo, grupo de nómina, unidad funcional y salario básico) con cada concepto devengado o deducido en sus liquidaciones, mostrando el valor total por concepto y la fecha de pago. Compone datos de contratos laborales, grupos de nómina, cargos, unidades funcionales y el detalle de liquidación para ofrecer una vista analítica de la nómina liquidada. Útil para reportes de auditoría de nómina, análisis de costos laborales por concepto, área o empleado, y validación de devengados y deducciones.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'vConceptLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'vConceptLiquidation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, por empleado y contrato, el detalle de cada concepto liquidado en nómina (valor y fecha) junto con los datos laborales básicos: cédula, cargo, grupo, unidad funcional y salario.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'vConceptLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada empleado debe tener un tercero asociado en Common.ThirdParty.; Cada contrato debe referenciar un cargo (Position), grupo (Group) y unidad funcional (FunctionalUnit) existentes.; Las liquidaciones consultadas deben estar vinculadas a un contrato existente y tener detalle con concepto válido.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'vConceptLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen empleados que tienen al menos un contrato (INNER JOIN entre Employee y Contract), un cargo, grupo y unidad funcional asociados.; Solo se exponen filas cuando existe al menos un detalle de liquidación con un concepto válido (INNER JOIN entre Liquidation, LiquidationDetail y Concept).; El salario reportado proviene del contrato (Contract.BasicSalary), no de la liquidación.; La identificación del empleado se toma del NIT del tercero asociado (ThirdParty.Nit).; El ID retornado es secuencial generado por ROW_NUMBER sin orden determinístico (ORDER BY (SELECT 0)), por lo que no es estable entre ejecuciones.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'vConceptLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Contrato; Cargo; Grupo de nómina; Unidad funcional; Salario básico; Liquidación de nómina; Concepto de nómina; Cédula (NIT del tercero)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'vConceptLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.vConceptLiquidation: Devuelve una fila por cada combinación contrato-concepto liquidado, uniendo ThirdParty→Employee→Contract→Position/Group/FunctionalUnit con Liquidation→LiquidationDetail→Concept mediante INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'vConceptLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Contract; Payroll.Position; Payroll.Group; Payroll.FunctionalUnit; Payroll.Liquidation; Payroll.LiquidationDetail; Payroll.Concept', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'vConceptLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'vConceptLiquidation';
GO
