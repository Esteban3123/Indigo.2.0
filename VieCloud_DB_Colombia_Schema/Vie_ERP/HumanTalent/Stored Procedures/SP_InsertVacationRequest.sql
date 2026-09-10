-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [HumanTalent].[SP_InsertVacationRequest]
	-- Add the parameters for the stored procedure here
	@EmployeeId INT,
    @InitialDateVacation DATETIME,
	@EndDateVacation DATETIME,
	@IncorporationDate DATETIME,
	@DaysRequest INT,
	@CreationUser VARCHAR(20),
	@UserStatus VARCHAR(20),
	@Comments VARCHAR(500)
	
AS
BEGIN
	
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;
	--Insert Register in Vacation Request
			DECLARE @IdVactionRequest INT 

			INSERT INTO [HumanTalent].[VacationRequest]
           ([EmployeeId]
           ,[InitialContractNumber]
           ,[IdContract]
           ,[TypeVacation]
           ,[InitialDateVacation]
           ,[EndDateVacation]
		   ,[IncorporationDate]
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
			@IncorporationDate,
           @DaysRequest,
           4
           ,1
           ,[Common].[GETDATE]()
           ,@CreationUser		   
		   )

		 SET @IdVactionRequest = SCOPE_IDENTITY();
		 PRINT(@IdVactionRequest)
		 --Insert Register in Vacation Request Detail
		 INSERT INTO [HumanTalent].[VacationRequestDetail] (IdVacationRequest, StatusRequest, DateStatus,UserStatus, Comments)
		VALUES (@IdVactionRequest, 1, [Common].[GETDATE](), @UserStatus, @Comments);

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra una nueva solicitud de vacaciones para un empleado del área de talento humano. Crea el encabezado de la solicitud en la tabla de solicitudes de vacaciones, asociándola automáticamente al contrato laboral vigente del empleado (activo y válido) obtenido desde la tabla de contratos de nómina, y establece el tipo de vacación, las fechas de inicio y fin, la fecha de reincorporación, los días solicitados y el estado inicial. A continuación, inserta el primer registro en el historial de seguimiento de la solicitud, dejando trazabilidad del estado inicial, el usuario responsable, la fecha y los comentarios asociados a la creación de la solicitud.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'PROCEDURE', @level1name = N'SP_InsertVacationRequest';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'PROCEDURE', @level1name = N'SP_InsertVacationRequest';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra una solicitud de vacaciones del empleado y crea su detalle inicial de seguimiento de estado.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un contrato en Payroll.Contract con Status=1 y Valid=1 para poder asociar InitialContractNumber e IdContract a la solicitud (de lo contrario quedarán nulos).', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda inserción en VacationRequest genera de forma inmediata un registro en VacationRequestDetail con StatusRequest=1 (estado inicial).; TypeVacation siempre se fija en 1 y EnjoyDays siempre en 4 al crear la solicitud (valores hardcodeados).; LastStatus inicial siempre es 1.; BUG/Invariante implícito: las subconsultas a Payroll.Contract usan EmployeeId = 1 literal (no el parámetro @EmployeeId), por lo que siempre se busca el contrato del empleado con Id=1.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de vacaciones; Detalle de solicitud de vacaciones; Contrato laboral activo; Días de disfrute; Fecha de incorporación', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] HumanTalent.VacationRequest: Crea la solicitud con TypeVacation=1, EnjoyDays=4, LastStatus=1, CreationDate=[Common].[GETDATE](); InitialContractNumber e IdContract se obtienen del contrato activo (Status=1 AND Valid=1) de Payroll.Contract.; [INSERT] HumanTalent.VacationRequestDetail: Tras insertar la solicitud, registra detalle con StatusRequest=1, DateStatus=[Common].[GETDATE]() e IdVacationRequest=SCOPE_IDENTITY() de la solicitud recién creada.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Contract', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequest';
-- GO
