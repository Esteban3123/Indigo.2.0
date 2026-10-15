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