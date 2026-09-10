
create VIEW [Payroll].[TotalPayroll] AS

SELECT DISTINCT L.RegisterStatus as Estado, 
G.Code as CodigoGrupo,
G.Name as NombreGrupo,
TP.Nit as cod_empl, 
TP.Name as NombreEmp, 
POS.Name as nom_carg, 
FU.Name as UnidadFuncional, 
CC.Name as CentroCosto, 
L.PayrollDateLiquidated as fec_acum, 
CASE WHEN CT.ContractClass = 2 THEN 'X' ELSE '' END as SalarioAprendices, 
CASE WHEN G.Code = '04' THEN 'X' ELSE '' END AS SalarioIntegral, 
(SELECT sum(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '001' and PayrollDate = '2016-01-31' ) as SUELDO, 
L.ValueTransportingRelief as AuxTransporte, 
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '008' and PayrollDate = '2016-01-31' ) as HORA_EXTRA_DIURNA,
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '009' and PayrollDate = '2016-01-31' ) as HORA_EXTRA_NOCTURNA,
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '006' and PayrollDate = '2016-01-31' ) as HORAS_EXTRAS_FESTIVAS_DIURNAS,
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '007' and PayrollDate = '2016-01-31' ) as HORA_EXTRA_FESTIVA_NOCTURNA,
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '010' and PayrollDate = '2016-01-31' ) as RECARGO_NOCTURNO_NORMAL,
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '011' and PayrollDate = '2016-01-31' ) as RECARGO_NOCTURNO_FESTIVO,
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '081' and PayrollDate = '2016-01-31' ) as RECARGO_DIURNO_DOMINICAL_FESTIVO,
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '022' and PayrollDate = '2016-01-31' ) as RECARGOS_EXTRAS_EN_PERIODO,
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '019' and PayrollDate = '2016-01-31' ) as VALOR_DOMINICAL_ASISTENCIAL,
L.DaysWorked as DiasLaborados, 
CASE WHEN L.VacationDays > 0 THEN 'X' ELSE '' END as Vacaciones,
L.VacationDays AS Dia_Vaca, 
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptClass = '004' and PayrollDate = '2016-01-31' ) as Bonificaciones,
L.AccumulatedOtherAccrued as OtrosDevengados, 
CASE WHEN L.AmbulatoryDisabilityDays > 0 THEN 'X' ELSE '' END as IncapaAmbulatoria, 
L.AmbulatoryDisabilityDays as diaIA, 
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '013' and PayrollDate = '2016-01-31' ) as VALOR_INCAPACIDAD_AMBULATORIA,
CASE WHEN L.DisabilityHospitalDays > 0 THEN 'X' ELSE '' END AS IncapaHospitalaria,
L.DisabilityHospitalDays as DiaIH,
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '014' and PayrollDate = '2016-01-31' ) as VALOR_INCAPACIDAD_HOSPITALARIA,
CASE WHEN L.OccupationalRisksDays > 0 THEN 'X' ELSE '' END as IncapaRiesgoProfesional, 
L.OccupationalRisksDays as  diaIP, 
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '015' and PayrollDate = '2016-01-31' ) as VALOR_INCAPACIDAD_RIESGOS,
CASE WHEN L.MaternityLeaveDays > 0 THEN 'X' ELSE '' END as Licencia_Materna, 
(SELECT SUM(AccruedValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptCode = '016' and PayrollDate = '2016-01-31' ) as VALOR_LICENCIA_MATERNIDAD,
L.TotalAccrued as Total_Devengados, 
L.EmployeeHealthContributionValue as AporteSalud,
L.PensionContributionValue as AportesPension, 
(SELECT SUM(ConceptTotalValue) FROM Payroll.LiquidationDetail where PayrollId = L.Id and ConceptClass = '038' and PayrollDate = '2016-01-31' ) as Fondo_Solidaridad_Pensional,
L.CalculatedWithholdingValue as Retencion_Fuente, 
L.AccumulatedOtherDeducted as OtrosDeducidos,
L.TotalDeducted as  TotalDeducido, 
L.TotalPaid as TotalPagado, 
L.EmployerHealthContributionValue as SaludPatrono, 
L.EmployerPensionContributionValue as PensionPatrono, 
L.OccupationalRisksContributionValue as RiesgosProfesionales, 
(L.OccupationalRisksContributionValue + L.EmployerPensionContributionValue + L.EmployerHealthContributionValue) as  AportesPatronales ,
L.SenaContributionValue as Sena, 
L.ICBFContributionValue as ICBF, 
L.FamilyCompensationFundContributionValue as Caja_Compensacion, 
L.ParafiscalContribution AS AportesParafiscales,
L.ProvisionVacation as  P_Vacaciones, 
L.ProvisionIncentive AS P_Primas, 
L.ProvisionInterestsUnemployment as  P_ICesantias, 
L.UnemploymentAccumulated AS P_Cesantias, 
L.ProvisionsValue as PrestacionesSociales, 
L.TotalAccrued + (L.OccupationalRisksContributionValue + L.EmployerPensionContributionValue + L.EmployerHealthContributionValue) + L.ParafiscalContribution + L.ProvisionsValue  as TotalNomina 
FROM Payroll.Liquidation L, 
Payroll.Employee E, 
Common.ThirdParty TP, 
Payroll.[Contract] CONT, 
Payroll.Position POS, 
Payroll.FunctionalUnit FU, 
Payroll.ContractType CT, 
Payroll.[Group] G, 
Payroll.CostCenter CC,
Payroll.LiquidationDetail LD
WHERE L.EmployeeId = E.Id
AND E.ThirdPartyId = TP.Id
AND CONT.EmployeeId = e.Id
AND CONT.Id = L.ContractId
AND CONT.PositionId = POS.Id
AND FU.Id = CONT.FunctionalUnitId
AND CONT.ContractTypeId = CT.Id
AND L.GroupId = G.Id
AND FU.CostCenterId = CC.Id
AND L.Id = LD.PayrollId
AND L.PayrollDateLiquidated = '2016-01-31' -- Fecha de la Nómina???
--ORDER BY TP.Nit
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista consolidada de nómina total para el período liquidado al 31 de enero de 2016. Integra la liquidación de cada empleado (devengados, deducciones, aportes patronales y provisiones) con su información laboral: grupo de nómina, centro de costo, unidad funcional, cargo y datos del tercero (NIT y nombre). Compone las tablas de liquidación, detalle de conceptos, empleado, contrato, cargo, tipo de contrato, unidad funcional, centro de costo y terceros para producir un reporte plano de nómina que resume sueldo, horas extras, recargos nocturnos y festivos, auxilio de transporte, incapacidades, licencias, vacaciones, bonificaciones, retención en la fuente, aportes a salud y pensión (empleado y empleador), parafiscales (SENA, ICBF, caja de compensación), prestaciones sociales (cesantías, primas, vacaciones) y el total de nómina por empleado. Sirve para reportería y auditoría de nómina, conciliación contable y verificación de costos laborales por área.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'TotalPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'TotalPayroll';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único reporte plano todos los componentes de la nómina liquidada al cierre de enero/2016 por empleado: devengados por concepto, deducciones, aportes patronales, parafiscales, provisiones y total de nómina.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'TotalPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existir registros en Payroll.Liquidation con PayrollDateLiquidated = ''2016-01-31''.; Integridad referencial entre Liquidation, Employee, ThirdParty, Contract, Position, FunctionalUnit, ContractType, Group, CostCenter y LiquidationDetail (joins implícitos por igualdad).; Los códigos y clases de concepto en LiquidationDetail deben estar parametrizados según el plan de conceptos (001, 006-011, 013-016, 019, 022, 081, ConceptClass 004 y 038).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'TotalPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan liquidaciones cuya fecha liquidada (PayrollDateLiquidated) es ''2016-01-31'' (fecha quemada en la vista).; Los valores por concepto se filtran adicionalmente por PayrollDate = ''2016-01-31''.; TotalNomina = TotalAccrued + (RiesgosProfesionales + EmployerPensionContributionValue + EmployerHealthContributionValue) + ParafiscalContribution + ProvisionsValue.; AportesPatronales = OccupationalRisksContributionValue + EmployerPensionContributionValue + EmployerHealthContributionValue.; Cada empleado debe estar relacionado con un ThirdParty, un Contract activo con Position, FunctionalUnit, ContractType, y la liquidación debe pertenecer a un Group y la unidad funcional a un CostCenter (joins obligatorios sin OUTER).; El SUELDO se obtiene del concepto ''001''; horas extras diurnas ''008'', nocturnas ''009'', festivas diurnas ''006'', festivas nocturnas ''007''; recargos nocturno normal ''010'', nocturno festivo ''011'', diurno dominical/festivo ''081''; recargos extras periodo ''022''; dominical asistencial ''019''; incapacidades ambulatoria ''013'', hospitalaria ''014'', riesgos ''015''; licencia maternidad ''016''; bonificaciones por ConceptClass ''004''; Fondo Solidaridad Pensional por ConceptClass ''038''.; Usa SELECT DISTINCT para evitar duplicados generados por el join con LiquidationDetail.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'TotalPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nómina; Liquidación de nómina; Empleado; Contrato; Cargo; Unidad funcional; Centro de costo; Grupo de nómina; Salario integral; Salario aprendices; Auxilio de transporte; Horas extras diurnas/nocturnas/festivas; Recargos nocturnos y dominicales; Bonificaciones; Vacaciones; Incapacidad ambulatoria/hospitalaria/riesgos profesionales; Licencia de maternidad; Aportes a salud, pensión y riesgos profesionales; Fondo de solidaridad pensional; Retención en la fuente; Aportes parafiscales (SENA, ICBF, Caja de Compensación); Provisiones (vacaciones, primas, cesantías, intereses de cesantías); Prestaciones sociales', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'TotalPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve una fila por empleado/liquidación con PayrollDateLiquidated = ''2016-01-31'', desglosando devengados por código de concepto y agregando totales calculados (AportesPatronales, TotalNomina).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'TotalPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CT.ContractClass = 2 → Marca ''X'' en SalarioAprendices else Cadena vacía; si G.Code = ''04'' → Marca ''X'' en SalarioIntegral else Cadena vacía; si L.VacationDays > 0 → Marca ''X'' en columna Vacaciones else Cadena vacía; si L.AmbulatoryDisabilityDays > 0 → Marca ''X'' en IncapaAmbulatoria else Cadena vacía; si L.DisabilityHospitalDays > 0 → Marca ''X'' en IncapaHospitalaria else Cadena vacía; si L.OccupationalRisksDays > 0 → Marca ''X'' en IncapaRiesgoProfesional else Cadena vacía; si L.MaternityLeaveDays > 0 → Marca ''X'' en Licencia_Materna else Cadena vacía', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'TotalPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.Position; Payroll.FunctionalUnit; Payroll.ContractType; Payroll.Group; Payroll.CostCenter; Payroll.LiquidationDetail', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'TotalPayroll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'TotalPayroll';
GO
