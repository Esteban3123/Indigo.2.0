-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Payroll].[SP_InsertVacationRequest]
	-- Add the parameters for the stored procedure here
	@EmployeeId INT,
    @InitialDateVacation DATETIME,
	@EndDateVacation DATETIME,
	@DaysRequest INT,
	@CreationUser VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

  
			INSERT INTO [HumanTalent].[VacationRequest]
           ([EmployeeId]
           ,[InitialContractNumber]
           ,[IdContract]
           ,[TypeVacation]
           ,[InitialDateVacation]
           ,[EndDateVacation]
           ,[DaysRequest]
           ,[EnjoyDays]
           ,[LastStatus]
           ,[CreationDate]
           ,[CreationUser]
          )
     VALUES
           (@EmployeeId,
		   (SELECT [Payroll].[Contract].[InitialContractNumber] FROM [Payroll].[Contract] WHERE (EmployeeId = 1 AND Status = 1 AND Valid = 1)),
           (SELECT [Payroll].[Contract].[Id] FROM [Payroll].[Contract] WHERE (EmployeeId = 1 AND Status = 1 AND Valid = 1)),
            1,
            @InitialDateVacation,
            @EndDateVacation, 
           @DaysRequest,
           4
           ,1
           ,[Common].[GETDATE]()
           ,@CreationUser 
		   )

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra una nueva solicitud de vacaciones para un empleado en el sistema de nómina y talento humano. Toma como datos de entrada el identificador del empleado, las fechas de inicio y fin de las vacaciones, la cantidad de días solicitados y el usuario que crea el registro. Antes de insertar, consulta automáticamente el contrato laboral activo y vigente del empleado (tabla Contract) para obtener el número de contrato inicial y el identificador del contrato, asociando así la solicitud al vínculo contractual vigente. La solicitud queda registrada con tipo de vacación 1 (vacaciones ordinarias), estado inicial 1 (pendiente) y 4 días de disfrute predeterminados, junto con la fecha actual del sistema.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_InsertVacationRequest';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_InsertVacationRequest';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra una nueva solicitud de vacaciones para un empleado, asociándola a su contrato activo y dejándola en estado inicial pendiente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un contrato en Payroll.Contract que cumpla Status=1 y Valid=1 para poder resolver InitialContractNumber e IdContract; de lo contrario se insertarán NULL en esas columnas; El filtro de contrato usa EmployeeId = 1 literal (valor hardcodeado), por lo que la búsqueda no se realiza sobre el empleado parametrizado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda solicitud de vacaciones se registra con TypeVacation=1 (tipo fijo); Toda solicitud se inserta con EnjoyDays=4 fijo; Toda solicitud se inserta con LastStatus=1 (estado inicial fijo); La fecha de creación se obtiene siempre desde Common.GETDATE() (no GETDATE() nativo), garantizando uso de hora institucional; El número de contrato inicial y el Id de contrato se resuelven desde Payroll.Contract filtrando contratos con Status=1 y Valid=1 (contrato activo y vigente)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de vacaciones; Contrato laboral; Empleado; Días de disfrute', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] HumanTalent.VacationRequest: Siempre inserta una fila en HumanTalent.VacationRequest con TypeVacation=1, EnjoyDays=4 y LastStatus=1 fijos, fechas/días desde parámetros y CreationDate=Common.GETDATE(); [INSERT] HumanTalent.VacationRequest: InitialContractNumber e IdContract se obtienen mediante subconsultas a Payroll.Contract con WHERE EmployeeId=1 AND Status=1 AND Valid=1 (EmployeeId hardcodeado a 1, no usa el parámetro @EmployeeId)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Contract', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
