-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Payroll].[SP_GetCrossVacationPeriodOfSubordinates]
	-- Add the parameters for the stored procedure here
	@InitialVacationDate DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @InputDate DATE;
	DECLARE @InitialDatePeriod DATE;
	DECLARE @EndDatePeriod DATE;

    -- Insert statements for procedure here
	--SELECT @InitialDatePeriod = InitialDatePeriod, @EndDatePeriod = EndDatePeriod FROM Payroll.VacationPeriod WHERE EmployeeId = 1 ORDER BY InitialDatePeriod; 
	SELECT Payroll.VacationPeriod.Id
	,Payroll.VacationPeriod.InitialDatePeriod
	,Payroll.VacationPeriod.EndDatePeriod
	,Payroll.Employee.Id AS EmployeeId
	,Common.ThirdParty.Name as ThirdPartyName
	,Payroll.Position.Name as PositionName
	FROM Payroll.VacationPeriod 
	INNER JOIN Payroll.Employee
	ON Payroll.Employee.Id =  Payroll.VacationPeriod.EmployeeId 
	INNER JOIN Common.ThirdParty
	ON Payroll.Employee.ThirdPartyId = Common.ThirdParty.Id
	INNER JOIN Payroll.Contract
	ON Payroll.Contract.EmployeeId = Payroll.Employee.Id
	INNER JOIN Payroll.Position
	ON Payroll.Position.Id = Payroll.Contract.PositionId
	WHERE (EndDatePeriod > @InitialVacationDate AND Payroll.Contract.Status = 1 AND Payroll.Contract.Valid= 1)
	ORDER BY Payroll.Employee.Id;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los períodos de vacaciones de subordinados que se cruzan o superan una fecha de inicio de vacaciones indicada como parámetro, permitiendo identificar conflictos o traslapes en la planificación vacacional del equipo. Combina la información de períodos vacacionales con los datos del empleado, su nombre como tercero registrado en el sistema y el cargo que ocupa según su contrato laboral vigente y activo. Es útil para que un jefe o responsable de nómina detecte qué colaboradores ya tienen vacaciones programadas que se solapan con una fecha propuesta, evitando ausencias simultáneas no planificadas.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetCrossVacationPeriodOfSubordinates';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetCrossVacationPeriodOfSubordinates';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los períodos de vacaciones vigentes (con fecha fin posterior a una fecha base) de empleados con contrato activo y válido, incluyendo nombre del tercero y cargo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetCrossVacationPeriodOfSubordinates';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse una fecha base de comparación (@InitialVacationDate).; Los empleados deben tener un ThirdParty asociado y un contrato con cargo (Position) registrado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetCrossVacationPeriodOfSubordinates';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen períodos de empleados cuyo contrato esté en estado activo (Status = 1) y marcado como válido (Valid = 1).; Solo se consideran períodos vacacionales que aún no han finalizado respecto a la fecha base (EndDatePeriod > @InitialVacationDate).; El uso de INNER JOIN garantiza que se excluyen empleados sin tercero, sin contrato o sin cargo asignado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetCrossVacationPeriodOfSubordinates';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Período de vacaciones; Empleado; Contrato laboral activo; Cargo/Posición; Tercero', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetCrossVacationPeriodOfSubordinates';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.VacationPeriod: Devuelve períodos de vacaciones cuyo EndDatePeriod > @InitialVacationDate, restringidos a empleados con Contract.Status = 1 y Contract.Valid = 1, ordenados por Employee.Id.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetCrossVacationPeriodOfSubordinates';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.VacationPeriod; Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.Position', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetCrossVacationPeriodOfSubordinates';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetCrossVacationPeriodOfSubordinates';
-- GO
