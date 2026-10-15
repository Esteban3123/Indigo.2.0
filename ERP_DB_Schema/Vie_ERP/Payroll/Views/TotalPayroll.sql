
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