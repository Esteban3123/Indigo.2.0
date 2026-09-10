-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Payroll].[SP_InsertVacationPeriod] 
	-- Add the parameters for the stored procedure here
	@EmployeeId INT,
    @InitialDatePeriod DATETIME,
	@EndDatePeriod DATETIME,
	@VacationDays INT,
	@UserCode VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	INSERT INTO [Payroll].[VacationPeriod]
			   ([EmployeeId]
			   ,[ContractId]
			   ,[InitialDatePeriod]
			   ,[EndDatePeriod]
			   ,[VacationDays]
			   ,[PendingDays]
			   ,[TakenDays]
			   ,[CreationUser]
			   ,[CreationDate])
		 VALUES
			   (@EmployeeId,
			   (SELECT TOP (1000) [Payroll].[Contract].[Id]
  FROM [Payroll].[Contract]
  WHERE (EmployeeId = 1 AND Status = 1 AND Valid = 1)),
			   @InitialDatePeriod,
			   @EndDatePeriod,
			   @VacationDays,
			   1,
			   1,
			   @UserCode
			   ,[Common].[GETDATE]())
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra un nuevo período de vacaciones para un empleado en la tabla de períodos vacacionales de nómina. Recibe el identificador del empleado, el rango de fechas del período vacacional y la cantidad de días de vacaciones, y busca automáticamente el contrato laboral activo y vigente del empleado para asociar el período. Al insertar, inicializa los días pendientes y los días tomados en 1, y guarda el usuario que realizó el registro junto con la fecha actual del sistema. Se utiliza en el proceso de gestión de vacaciones del personal para crear el registro formal del período vacacional asociado al contrato vigente del empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_InsertVacationPeriod';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_InsertVacationPeriod';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registrar un nuevo período de vacaciones de un empleado, asociándolo al contrato activo y vigente, con días pendientes y tomados inicializados por defecto.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationPeriod';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un contrato en Payroll.Contract con Status=1 y Valid=1 para poder asociar el ContractId; de lo contrario el INSERT registrará NULL en ContractId.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationPeriod';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Al crear un período de vacaciones, PendingDays y TakenDays se inicializan en 1 (valores fijos hardcodeados, no derivados del parámetro de días).; La fecha de creación se obtiene mediante la función centralizada Common.GETDATE() en lugar de GETDATE() nativo.; El ContractId se resuelve buscando contratos con Status=1 y Valid=1 (contrato activo y vigente).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationPeriod';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'período de vacaciones; contrato laboral; días pendientes; días tomados; empleado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationPeriod';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Payroll.VacationPeriod: Inserta un período de vacaciones con ContractId obtenido del primer contrato encontrado con (EmployeeId=1 AND Status=1 AND Valid=1) — el filtro usa el literal EmployeeId=1 en lugar del parámetro @EmployeeId, lo que constituye un posible bug: siempre busca el contrato del empleado 1.; [INSERT] Payroll.VacationPeriod: PendingDays y TakenDays siempre se insertan con valor 1, independientemente de @VacationDays.; [INSERT] Payroll.VacationPeriod: CreationDate se asigna con Common.GETDATE() al momento del insert y CreationUser con @UserCode.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationPeriod';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationPeriod';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Contract', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationPeriod';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationPeriod';
-- GO
