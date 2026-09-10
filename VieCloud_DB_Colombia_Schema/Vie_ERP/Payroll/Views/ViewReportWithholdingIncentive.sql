

CREATE VIEW [Payroll].[ViewReportWithholdingIncentive]
AS

select 
ip.Id
,ct.Nit
,ct.Name as ThirdName
,2 as ConceptType
,ip.PeriodInitialDate
,ip.PeriodEndDate
,et.Code as EmployeeTypeCode
,ip.Period
,ip.TotalAccrued as AccruedValue
,ip.TotalDeducted as DeductedValue
,pe.Id as EmployeeId,pg.Id as GroupId,pg.Code as GroupCode,pg.Name as GroupName
,ip.ProcedureTypeRTF, pe.DeclarantType,ip.TypeArticleRTF,ip.RetentionBase
,(select sum(ipd.DeductedValue) from Payroll.IncentivePaymentDetail ipd inner join Payroll.Concept con on con.Id = ipd.ConceptId where ipd.IncentivePaymentId = ip.Id and con.ConceptClass = '020') as RetentionValue
,fUnit.BranchOfficeId
from Payroll.IncentivePayment as ip 
inner join Payroll.[Group] as pg on pg.Id = ip.GroupId
inner join Payroll.[Contract] as c on c.Id = ip.ContractId
inner join Payroll.Employee as pe on pe.Id = c.EmployeeId
inner join Common.ThirdParty as ct on ct.Id = pe.ThirdPartyId
inner join Payroll.EmployeeType as et on et.Id = pe.EmployeeTypeId
OUTER APPLY(
	SELECT TOP 1 fUnit.BranchOfficeId
	FROM Payroll.[Contract] cntrc
	JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = cntrc.FunctionalUnitId
	WHERE cntrc.EmployeeId = pe.Id
	ORDER BY cntrc.Id DESC
) fUnit
where (select sum(ipd.DeductedValue) from Payroll.IncentivePaymentDetail ipd inner join Payroll.Concept con on con.Id = ipd.ConceptId where ipd.IncentivePaymentId = ip.Id and con.ConceptClass = '020' and con.ConceptType <> 3) > 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reportería para retención en la fuente sobre pagos de incentivos y bonificaciones del personal. Consolida información de cada liquidación de incentivo (valor devengado total, valor deducido total, base de retención y valor exento) junto con los datos del empleado (NIT, nombre, tipo de empleado, grupo de nómina y sede) para los períodos en que exista al menos una deducción por concepto de retención en la fuente (clase de concepto 020, excluyendo tipo 3). Integra los pagos de incentivos con los contratos laborales, el registro maestro de empleados, los terceros, los grupos de nómina y las unidades funcionales. Se utiliza para generar informes de retención en la fuente aplicada sobre incentivos y bonificaciones en nómina, facilitando la conciliación tributaria y el reporte a la DIAN.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportWithholdingIncentive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportWithholdingIncentive';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la base de datos para el reporte de retención en la fuente aplicada sobre pagos de incentivos/bonificaciones de nómina, consolidando devengados, deducciones, retención calculada y datos del tercero/empleado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen pagos de incentivos en Payroll.IncentivePayment con su detalle en Payroll.IncentivePaymentDetail.; Los conceptos en Payroll.Concept están clasificados con ConceptClass y ConceptType (clase ''020'' identifica retención).; Cada empleado tiene contrato vigente y unidad funcional asociada para resolver la sucursal (BranchOfficeId).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ConceptType siempre se reporta con valor literal 2 (constante en el SELECT).; RetentionValue se calcula únicamente con conceptos de ConceptClass=''020'' (retención) sin filtrar ConceptType, mientras que el filtro de inclusión excluye ConceptType=3.; La sucursal (BranchOfficeId) corresponde al contrato más reciente del empleado (mayor Contract.Id), no necesariamente al contrato del incentivo.; Solo se incluyen pagos de incentivo con retención efectiva (>0) calculada sobre conceptos clase ''020'' distintos de tipo 3.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retención en la fuente; Incentivos/bonificaciones de nómina; Período de liquidación; Tipo de declarante (DeclarantType); Procedimiento RTF (ProcedureTypeRTF, TypeArticleRTF); Base de retención; Tercero (NIT); Tipo de empleado; Grupo de nómina; Sucursal/Unidad funcional', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportWithholdingIncentive: Devuelve un registro por IncentivePayment cuya suma de DeductedValue de detalles con Concept.ConceptClass=''020'' y ConceptType<>3 sea mayor a 0.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Subconsulta: SUM(ipd.DeductedValue) WHERE Concept.ConceptClass=''020'' AND Concept.ConceptType<>3 > 0 → Incluye el pago de incentivo en el reporte (filtro WHERE de la vista). else El pago de incentivo no aparece en el reporte.; si OUTER APPLY: TOP 1 contrato del empleado ordenado por Contract.Id DESC → Toma BranchOfficeId desde la FunctionalUnit del contrato más reciente del empleado. else BranchOfficeId queda NULL si el empleado no tiene contratos con unidad funcional.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncentivePayment; Payroll.IncentivePaymentDetail; Payroll.Concept; Payroll.Group; Payroll.Contract; Payroll.Employee; Common.ThirdParty; Payroll.EmployeeType; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingIncentive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportWithholdingIncentive';
GO
