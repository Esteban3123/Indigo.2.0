
CREATE VIEW [Payroll].[ViewReportPaidAgreementsProcess]
AS
SELECT ROW_NUMBER() OVER(ORDER BY E.Id ASC) as Row,
E.Id as IdEmpleado, L.GroupId as IdGrupo, TP.Nit as Cedula, TP.Name as Empleado, L.PayrollDateLiquidated AS FechaPago,'NÓMINA' as Pago, 
CASE	WHEN L.RegisterStatus = '' THEN 'SIN CONFIRMAR' 
		WHEN L.RegisterStatus = 'C' THEN 'CONFIRMADO' END AS Estado, LD.ConceptTotalValue as ValorConcepto
FROM Payroll.Employee E, Common.ThirdParty TP, Payroll.Liquidation L, Payroll.LiquidationDetail LD
WHERE E.Id = L.EmployeeId AND LD.PayrollId = L.Id AND E.ThirdPartyId = TP.Id
and LD.ConceptClass = '041'
UNION
SELECT  ROW_NUMBER() OVER(ORDER BY E.Id ASC) as Row,
E.Id, CONT.GroupId, TP.Nit, TP.Name, V.VacationStartDate, 'VACACIONES', 'CONFIRMADO', VD.Deducted
FROM Payroll.VacationPeriod VP, Payroll.Vacation V, PAyroll.VacationDetail VD, Payroll.Employee E, Common.ThirdParty TP, Payroll.Concept C, PAyroll.Contract CONT
WHERE VP.Id = V.VacationPeriodId AND VD.IdVacation = V.Id AND VP.EmployeeId = E.ID AND E.ThirdPartyId = TP.Id AND C.Id = VD.IdConcept AND C.ConceptClass = '041' AND CONT.Id = VP.ContractId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que consolida los pagos realizados a empleados bajo el concepto de clase ''041'' (típicamente préstamos, embargos o acuerdos de pago descontados por nómina), combinando dos fuentes: las liquidaciones de nómina ordinaria y las liquidaciones de vacaciones. Para cada registro muestra el empleado (cédula/NIT y nombre), el grupo de nómina o contrato, la fecha de pago, el tipo de pago (''NÓMINA'' o ''VACACIONES''), el estado de confirmación de la liquidación y el valor del concepto deducido. Sirve para auditar y reportar los acuerdos de pago o descuentos pactados con empleados que han sido efectivamente procesados y descontados en nómina o en la liquidación de vacaciones.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPaidAgreementsProcess';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPaidAgreementsProcess';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único reporte los pagos de conceptos de clase ''041'' (acuerdos/convenios) realizados al empleado tanto vía liquidación de nómina como vía liquidación de vacaciones, mostrando estado, fecha y valor.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaidAgreementsProcess';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada liquidación debe tener un empleado existente (E.Id = L.EmployeeId) y un tercero asociado (E.ThirdPartyId = TP.Id); Cada detalle de liquidación debe corresponder a una liquidación existente (LD.PayrollId = L.Id); Cada vacación debe tener período (VP.Id = V.VacationPeriodId), detalle (VD.IdVacation = V.Id), contrato (CONT.Id = VP.ContractId), empleado con tercero, y un concepto válido (C.Id = VD.IdConcept); El concepto asociado debe tener ConceptClass = ''041'' tanto en el bloque de nómina (LD.ConceptClass) como en el de vacaciones (C.ConceptClass)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaidAgreementsProcess';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen liquidaciones y detalles de vacaciones cuyo concepto pertenece a la clase ''041'' (conceptos de pago tipo acuerdo/convenio); Las filas provenientes del bloque de vacaciones se reportan siempre con Estado=''CONFIRMADO'', sin evaluar RegisterStatus; Las filas de nómina se etiquetan con Pago=''NÓMINA'' y las de vacaciones con Pago=''VACACIONES''; Para el bloque de nómina la fecha reportada es PayrollDateLiquidated; para vacaciones es VacationStartDate; El UNION elimina duplicados exactos entre ambos bloques; El ROW_NUMBER se calcula independientemente en cada bloque del UNION antes de unificar, por lo que el campo Row puede repetirse', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaidAgreementsProcess';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'nómina; liquidación de nómina; vacaciones; período de vacaciones; concepto de nómina; empleado; contrato; tercero; grupo de pago', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaidAgreementsProcess';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve la unión de: (a) detalles de liquidación de nómina con LD.ConceptClass=''041'' etiquetados como ''NÓMINA'' con estado derivado de RegisterStatus, y (b) detalles de vacaciones cuyo concepto tiene ConceptClass=''041'' etiquetados como ''VACACIONES'' con estado fijo ''CONFIRMADO''', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaidAgreementsProcess';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si L.RegisterStatus = '''' → Estado se reporta como ''SIN CONFIRMAR''; si L.RegisterStatus = ''C'' → Estado se reporta como ''CONFIRMADO'' else Si no es '''' ni ''C'', el Estado queda NULL (CASE sin ELSE)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaidAgreementsProcess';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Employee; Common.ThirdParty; Payroll.Liquidation; Payroll.LiquidationDetail; Payroll.VacationPeriod; Payroll.Vacation; Payroll.VacationDetail; Payroll.Concept; Payroll.Contract', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaidAgreementsProcess';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPaidAgreementsProcess';
GO
