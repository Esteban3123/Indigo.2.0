-- =============================================
-- Author:		Iván Dario Ospina Acosta
-- Create date: 2019-12-03
-- Description:	
-- =============================================

CREATE PROCEDURE [Payroll].[SP_GetPayrollReport] 
	@pStartDate DATE,
	@pEndDate DATE,
	@pRegisterStatus CHAR(1) = NULL,
	@pCédula VARCHAR(20) = NULL,
	@pStartGroupCode VARCHAR(20) = NULL,
	@pEndGroupCode VARCHAR(20) = NULL
WITH RECOMPILE
AS
BEGIN

	SELECT 
		g.Code 'Grupo'
		,tp.Nit 'Numero Documento'
		,tp.Name 'Nombre'
		,CASE l.RegisterStatus
			WHEN ' ' THEN 'Sin confirmar'
			WHEN 'C' THEN 'Confirmado'
			ELSE 'No aplica'
		END 'Estado registro'
		,l.PayrollDateLiquidated 'Fecha Liquidación'
		,l.BasicSalary 'Salario básico'
		,CAST((l.BasicSalary / 30) * l.DaysWorked AS NUMERIC(18,0)) 'Salario Devengado'
		,l.ValueTransportingRelief 'Aux. Transporte'
		,l.PayrollDays 'Días Nómina'
		,l.DaysWorked 'Días Trabajados'
		,l.ProvisionDays 'Días Provisión'
		,l.RecargoNocturno 'Recargo Nocturno'
		,l.RecargoNocturnoFestivo 'Recargo Nocturno F'
		,l.Overtime 'Horas Extras'
		,l.EveningOvertime 'Horas Extras Nocturnas'
		,l.DiurnalOvertime 'Horas Extras Diurnas'
		,l.VacationDays 'Días Vacaciones'
		,IIF(ISDATE(CAST(l.VacationInitialDate AS VARCHAR(20))) = 0, NULL, l.VacationInitialDate) 'Inicio Vacaciones' 
		,IIF(ISDATE(CAST(l.VacationEndDate AS VARCHAR(20))) = 0, NULL, l.VacationEndDate) 'Fin Vacaciones'
		,l.TotalBaseRetention 'Base Total Retención'
		,l.CalculatedWithholdingValue 'Valor Retención'
		,l.PeriodJCB 'IBC Periodo'
		,l.PensionJCB 'IBC Pensión'
		,l.HealthJCB 'IBC Salud'
		,l.IBCUnemployment 'IBC Cesantías'
		,l.IBCSENA 'IBC SENA'
		,l.IBCCompensationFund 'IBC Caja Compensación'
		,l.IBCICBF 'IBC ICBF'
		,l.IBCVacation 'IBC Vacaciones'
		,l.AmbulatoryDisabilityDays 'Días Incapacidad Amb.'
		,l.AmbulatoryDisabilityInitialDate 'Fecha Inicio Inc. Ambulatoria'
		,l.AmbulatoryDisabilityEndDate 'Fecha Fin Inc. Ambulatoria'
		,l.AmbulatoryDisabilityValue 'Valor Incapacidad Amb.'
		,l.DisabilityHospitalDays 'Días Incapacidad Hos.'
		,l.DisabilityHospitalInitialDate 'Fecha Inicio Inc. Hospitalaria'
		,l.DisabilityHospitalEndDate 'Fecha Fin Inc. Hospitalaria'
		,l.DisabilityHospitalValue 'Valor Incapacidad Hos.'
		,l.MaternityLeaveDays 'Días Lic. Maternidad'
		,l.MaternityLeaveInitialDate 'Fecha Inicio Lic. Maternidad'
		,l.MaternityLeaveEndDate 'Fecha Fin Lic. Maternidad'
		,l.MaternityLeaveValue 'Valor Lic. Maternidad'
		,l.LicenseDays 'Días Licencia'
		,l.LicenseValue 'Valor Licencia'
		,l.UnpaidLicenseDays 'Días Licencia No Remuneradas'
		,l.UnpaidLicenseInitialDate 'Fecha Inicio Lic. No Remuneradas'
		,l.UnpaidLicenseEndDate 'Fecha Fin Lic. No Remuneradas'
		,l.SanctionDays 'Días Sanción'
		,l.SanctionInitialDate 'Fecha Inicio Sanción'
		,l.SanctionEndDate 'Fecha Fin Sanción'
		,l.ProvisionIncentive 'Provisión Primas'
		,l.UnemploymentAccumulated 'Provisión Cesantías'
		,l.ProvisionInterestsUnemployment 'Provisión Int. Cesantías'
		,l.ProvisionVacation 'Provisión Vacaciones'
		,l.TotalAccrued 'Total Devengado'
		,l.TotalDeducted 'Total Deducido'
		,l.TotalPaid 'Total Pagado'
	FROM Payroll.Liquidation l WITH (NOLOCK)
	JOIN Payroll.Employee e WITH (NOLOCK) ON e.Id = l.EmployeeId 
	JOIN Payroll.[Group] g WITH (NOLOCK) ON g.Id = l.GroupId 
	JOIN Common.ThirdParty tp WITH (NOLOCK) ON tp.Id = e.ThirdPartyId
	WHERE l.PayrollDateLiquidated > @pStartDate AND l.PayrollDateLiquidated <= @pEndDate
	AND (RegisterStatus = @pRegisterStatus OR @pRegisterStatus IS NULL)
	AND (e.Id = @pCédula OR @pCédula IS NULL)
	AND ((g.Code BETWEEN @pStartGroupCode AND @pEndGroupCode) OR (@pStartGroupCode IS NULL OR @pEndGroupCode IS NULL))

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte detallado de nómina por período: genera el informe de liquidación de nómina de empleados filtrando por rango de fechas de liquidación, estado del registro (confirmado o sin confirmar), cédula del empleado y rango de grupo de nómina. Compone información del empleado (nombre, documento/NIT) desde el tercero asociado, el grupo de nómina al que pertenece, y el detalle completo de la liquidación: salario básico, salario devengado, auxilio de transporte, días trabajados, horas extras diurnas y nocturnas, recargos nocturnos y festivos, vacaciones, incapacidades ambulatorias y hospitalarias, licencias de maternidad, licencias no remuneradas, sanciones, bases IBC para seguridad social (pensión, salud, cesantías, caja de compensación, SENA, ICBF), retención en la fuente, provisiones laborales (primas, cesantías, intereses de cesantías, vacaciones), y totales devengado, deducido y pagado. Es el procedimiento central para la generación del informe de nómina contable y de recursos humanos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetPayrollReport';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetPayrollReport';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte tabular de liquidaciones de nómina por rango de fechas, con devengados, deducciones, provisiones, IBC, incapacidades y licencias por empleado y grupo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPayrollReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (@pStartDate, @pEndDate) debe estar definido; la consulta usa l.PayrollDateLiquidated > @pStartDate AND <= @pEndDate.; Los filtros @pRegisterStatus, @pCédula, @pStartGroupCode y @pEndGroupCode son opcionales (NULL los desactiva).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPayrollReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen liquidaciones cuyo PayrollDateLiquidated sea estrictamente mayor a @pStartDate y menor o igual a @pEndDate (rango semiabierto por la izquierda).; El ''Salario Devengado'' se calcula como (BasicSalary / 30) * DaysWorked redondeado a NUMERIC(18,0), asumiendo mes comercial de 30 días.; Solo se reportan liquidaciones que tengan empleado, grupo y tercero asociados (JOINs internos obligatorios).; Las fechas inválidas de vacaciones se neutralizan a NULL mediante validación con ISDATE.; El procedimiento se ejecuta WITH RECOMPILE, generando plan nuevo en cada ejecución.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPayrollReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de nómina; Salario básico y devengado; Auxilio de transporte; Horas extras y recargos nocturnos/festivos; Vacaciones; Retención en la fuente; IBC (Ingreso Base de Cotización) salud, pensión, cesantías, SENA, ICBF, caja de compensación; Incapacidades ambulatoria y hospitalaria; Licencia de maternidad; Licencias remuneradas y no remuneradas; Sanciones disciplinarias; Provisiones (primas, cesantías, intereses cesantías, vacaciones); Grupo de nómina; Tercero / empleado por cédula', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPayrollReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Liquidation: Devuelve un resultset con campos de liquidación (devengados, deducciones, IBC, incapacidades, licencias, provisiones) cruzando Liquidation con Employee, Group y ThirdParty filtrando por PayrollDateLiquidated en el rango indicado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPayrollReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si l.RegisterStatus = '' '' → Etiqueta el estado como ''Sin confirmar''; si l.RegisterStatus = ''C'' → Etiqueta el estado como ''Confirmado'' else Cualquier otro valor se etiqueta como ''No aplica''; si ISDATE(VacationInitialDate/EndDate) = 0 → Devuelve NULL en las fechas de vacaciones else Devuelve la fecha original; si @pStartGroupCode IS NULL OR @pEndGroupCode IS NULL → No aplica filtro por rango de código de grupo else Filtra g.Code BETWEEN @pStartGroupCode AND @pEndGroupCode', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPayrollReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.Employee; Payroll.Group; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPayrollReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetPayrollReport';
-- GO
