/****** Object:  View [Payroll].[ViewReportRetroactive]    Script Date: 3/11/2023 3:56:25 p. m. ******/
CREATE VIEW [Payroll].[ViewReportRetroactive]
AS
SELECT rd.id,
	   ET.Id as IdTipoEmpleado, 
	   CONC.ConceptType as TipoConcepto,
	   TP.Nit, TP.Name as NombreEmpleado,
	   G.Name as NombreGrupo,
	   ET.Name as EmployeeType,
	   RC.InitialDateRetroactive, 
	   FU.Name as FuncionalUnitName, 
	   CC.Code as CodeCostCenter,
	   CC.Name as NameCostCenter, 
	   CONT.BasicSalary,
	   CONC.Code + ' - ' + CONC.Name as Concepto,
	   RD.ValueConceptwithRetroactive as ValorConcepto,
	   (SELECT top 1 TP.Nit from Payroll.FundContract FC, Payroll.Fund F, Common.ThirdParty TP WHERE FC.ContractId = RC.IdContract AND F.Id = FC.fundId AND FC.Fundtype = 1 AND TP.Id = F.ThirdPartyId) as NitFondoSalud,
	   (SELECT top 1 F.Code + ' - ' + F.Name from Payroll.FundContract FC, Payroll.Fund F, Common.ThirdParty TP WHERE FC.ContractId = RC.IdContract AND F.Id = FC.fundId AND FC.Fundtype = 1 AND TP.Id = F.ThirdPartyId) as NombreFondoSalud,
	   (SELECT top 1 TP.Nit from Payroll.FundContract FC, Payroll.Fund F, Common.ThirdParty TP WHERE FC.ContractId = RC.IdContract AND F.Id = FC.fundId AND FC.Fundtype = 2 AND TP.Id = F.ThirdPartyId) as NitFondoPension,
	   (SELECT top 1 F.Code + ' - ' + F.Name from Payroll.FundContract FC, Payroll.Fund F, Common.ThirdParty TP WHERE FC.ContractId = RC.IdContract AND F.Id = FC.fundId AND FC.Fundtype = 2 AND TP.Id = F.ThirdPartyId) as NombreFondoPension,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '035') as Sena,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '036') as CompensationFundValue,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '037') as ICBF,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '018') as HealthValueEmployer,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '017') as HealthValueEmployee,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '015') as PensionValueEmployer,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '014') as PensionValueEmployee,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '008') as UnemploymentProvision,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '034') as UnemploymentInterestProvision,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '031') as UnemploymentVacationProvision,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '033') as IncentivePaymentProvision,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '009') as ARL,
	   FU.BranchOfficeId,
	   CONT.GroupId
FROM Common.ThirdParty TP, Payroll.Employee E, Payroll.[Group] G, Payroll.EmployeeType ET, Payroll.CostCenter CC, 
Payroll.FunctionalUnit FU, Payroll.RetroactiveC RC, Payroll.RetroactiveD RD, Payroll.[Contract] CONT, Payroll.Concept CONC
WHERE TP.Id = E.ThirdPartyId
AND E.Id = CONT.EmployeeId
AND E.Id = RC.IdEmployee 
AND G.Id = CONT.GroupId
AND CC.Id = E.CostCenterId
AND CONT.FunctionalUnitId = FU.Id
AND ET.Id = E.EmployeeTypeId
AND RC.Id = RD.IdRetroactiveC
AND RC.IdContract = CONT.Id
AND RD.IdConcept = CONC.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte consolidado de liquidaciones retroactivas de nómina por empleado. Integra los datos del empleado (cédula, nombre, tipo, grupo, centro de costo, unidad funcional y salario básico) con los conceptos de nómina ajustados retroactivamente y sus valores reliquidados. Incluye el fondo de salud y el fondo de pensión afiliados al contrato, así como los valores retroactivos desglosados por concepto parafiscal: SENA, caja de compensación, ICBF, salud empleador, salud empleado, pensión empleador, pensión empleado, cesantías (provisión), intereses sobre cesantías, vacaciones, prima de servicios y ARL. Se usa para auditar y reportar los ajustes salariales con efecto retroactivo, verificar los aportes a seguridad social generados por reliquidaciones y soportar la conciliación contable de nómina retroactiva.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportRetroactive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportRetroactive';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida los detalles de liquidaciones retroactivas de nómina por empleado, contrato y concepto, incluyendo los valores parafiscales, provisiones y datos de fondos de salud y pensión asociados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado debe tener un tercero asociado (TP.Id = E.ThirdPartyId); El empleado debe tener un contrato vigente vinculado a la liquidación retroactiva (RC.IdContract = CONT.Id); Debe existir un encabezado de retroactivo (RetroactiveC) con al menos un detalle (RetroactiveD) ligado a un concepto; El contrato debe tener Grupo, Unidad Funcional y Centro de Costo asignados; El empleado debe tener un EmployeeType asignado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa un detalle de retroactivo (RetroactiveD) ligado a un único concepto y a un único encabezado RetroactiveC; Las clases de concepto (''035'',''036'',''037'',''018'',''017'',''015'',''014'',''008'',''034'',''031'',''033'',''009'') se mapean a columnas fijas del reporte representando aportes parafiscales, salud, pensión, ARL y provisiones sociales; Los fondos de salud y pensión se distinguen por Fundtype (1=Salud, 2=Pensión); Los JOINs son INNER (estilo SQL-89 con comas), por lo que solo aparecen retroactivos con todas las relaciones organizacionales completas (grupo, unidad funcional, centro de costo, tipo de empleado, tercero); El concepto mostrado en ''Concepto'' se compone como Code + '' - '' + Name', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación retroactiva de nómina; Aportes parafiscales (SENA, ICBF, Caja de Compensación); Aportes a salud empleado/empleador; Aportes a pensión empleado/empleador; ARL (Riesgos Laborales); Provisión de cesantías e intereses de cesantías; Provisión de vacaciones; Provisión de prima (incentivo); Fondo de salud y fondo de pensión; Centro de costo; Unidad funcional; Tipo de empleado; Salario básico; Concepto de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportRetroactive: Devuelve una fila por cada detalle de retroactivo (RD) cruzado con su concepto, contrato, empleado y entidades organizacionales (grupo, unidad funcional, centro de costo, tipo de empleado)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FundContract.Fundtype = 1 → Se identifica el fondo como Fondo de Salud y se devuelven NitFondoSalud y NombreFondoSalud; si FundContract.Fundtype = 2 → Se identifica el fondo como Fondo de Pensión y se devuelven NitFondoPension y NombreFondoPension; si Concept.ConceptClass = ''035'' → El valor retroactivo se reporta en la columna Sena; si Concept.ConceptClass = ''036'' → El valor retroactivo se reporta como CompensationFundValue (Caja de Compensación); si Concept.ConceptClass = ''037'' → El valor retroactivo se reporta como ICBF; si Concept.ConceptClass = ''018'' → El valor retroactivo se reporta como HealthValueEmployer (aporte salud empleador); si Concept.ConceptClass = ''017'' → El valor retroactivo se reporta como HealthValueEmployee (aporte salud empleado); si Concept.ConceptClass = ''015'' → El valor retroactivo se reporta como PensionValueEmployer; si Concept.ConceptClass = ''014'' → El valor retroactivo se reporta como PensionValueEmployee; si Concept.ConceptClass = ''008'' → El valor retroactivo se reporta como UnemploymentProvision (provisión de cesantías); si Concept.ConceptClass = ''034'' → El valor retroactivo se reporta como UnemploymentInterestProvision (intereses de cesantías); si Concept.ConceptClass = ''031'' → El valor retroactivo se reporta como UnemploymentVacationProvision (provisión de vacaciones); si Concept.ConceptClass = ''033'' → El valor retroactivo se reporta como IncentivePaymentProvision (provisión prima); si Concept.ConceptClass = ''009'' → El valor retroactivo se reporta como ARL', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Group; Payroll.EmployeeType; Payroll.CostCenter; Payroll.FunctionalUnit; Payroll.RetroactiveC; Payroll.RetroactiveD; Payroll.Contract; Payroll.Concept; Payroll.FundContract; Payroll.Fund', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportRetroactive';
GO
