-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [HumanTalent].[SP_InsertAnnualVacationSchedule]
	-- Add the parameters for the stored procedure here
	@IdentificationNumber VARCHAR(20),
	@VacationInitialDate DATE,
	@VacationEndDate DATE,
	@IncorporationDate DATE,
	@RequestDays INT,
	@Comments VARCHAR(500),
	@CreationUser VARCHAR(20)	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	SET NOCOUNT OFF;
	DECLARE @SumPendingDays INT, @VacationPeriodId INT,@VacationDays INT, @TakenDays INT, @VacationPeriodPendingDays INT, @RowIndexCount INT, @DaysToken INT, @CarryPendingDays INT, @EmployeeId INT;
    -- Insert statements for procedure here
	
	SET @SumPendingDays = 0;
	SET @RowIndexCount = 1;
	SET @TakenDays = 0;
	SELECT  @EmployeeId = Payroll.Employee.Id FROM Common.Person
	INNER JOIN Common.ThirdParty
	ON Common.Person.Id = Common.ThirdParty.PersonId
	INNER JOIN Payroll.Employee
	ON Payroll.Employee.ThirdPartyId = Common.ThirdParty.Id
	WHERE Common.Person.IdentificationNumber = @IdentificationNumber
	--Obtener en una tabla temporal los Id's de los Vacation Periods y los index ordenada de manera descendente por fecha inicial
	SELECT Id, PendingDays,VacationDays INTO #TempVacationPeriod FROM Payroll.VacationPeriod WHERE (EmployeeId = @EmployeeId AND PendingDays > 0) ORDER BY InitialDatePeriod DESC;
	create table #Temp
	( 
		id INT IDENTITY NOT NULL PRIMARY KEY,
		VacationPeriodId INT,
		PendingDays INT,
		RequestDays INT,
		DaysTaken INT,
		SumPendingDays INT
		
	)
	SET @CarryPendingDays =  @RequestDays
	WHILE @SumPendingDays < @RequestDays
	BEGIN
		
		WITH OrderedOrders AS  
		(  
		   SELECT ROW_NUMBER() OVER(ORDER BY Id ASC) AS RowIndex, Id, PendingDays, VacationDays  FROM #TempVacationPeriod  
		)   
		SELECT @VacationPeriodId=Id, @VacationPeriodPendingDays=OrderedOrders.PendingDays, @VacationDays = OrderedOrders.VacationDays
		FROM OrderedOrders  WHERE RowIndex = @RowIndexCount;  

		
		SET @SumPendingDays = @SumPendingDays + @VacationPeriodPendingDays;
		
		--SET @DaysToken =  @VacationPeriodPendingDays -@SumPendingDays;
		--SELECT Id, PendingDays INTO #Temp FROM #TempVacationPeriod WHERE Id = @VacationPeriodId;
		INSERT INTO #Temp
		(
		    VacationPeriodId,
			PendingDays,
			RequestDays,
			SumPendingDays
		)
		VALUES
		(   
			@VacationPeriodId, -- VacationPeriodId - int		
			@VacationPeriodPendingDays,
			@DaysToken,
			@SumPendingDays
		)
		PRINT('---')
		PRINT('Vacation Period Pending Dates')
		PRINT(@VacationPeriodPendingDays)		
		PRINT('CarryPendingDays')
		PRINT(@CarryPendingDays)
		SET @CarryPendingDays =@VacationPeriodPendingDays - @CarryPendingDays;
		IF(@CarryPendingDays < 0)
			SET @TakenDays = @VacationDays;
		ELSE
			SET @TakenDays = @VacationDays - @CarryPendingDays;  
		PRINT('TakenDays')
		PRINT(@TakenDays)
		PRINT('VacationDays')
		PRINT(@VacationDays)
		PRINT('CarryPendingDaysDef')
		PRINT(@CarryPendingDays)		
		PRINT('SUMPENDINGDATES')
		PRINT(@SumPendingDays)
		IF @CarryPendingDays < 0
		BEGIN
				PRINT('Carry inferior')
				UPDATE Payroll.VacationPeriod SET PendingDays = 0, TakenDays = @TakenDays WHERE Id = @VacationPeriodId;
				END
		ELSE
		Begin
			PRINT('Carry Superior')
			UPDATE Payroll.VacationPeriod SET  TakenDays = ABS(@TakenDays) WHERE Id = @VacationPeriodId;
			UPDATE Payroll.VacationPeriod SET  PendingDays = (@VacationDays - (SELECT TakenDays FROM Payroll.VacationPeriod WHERE Id = @VacationPeriodId)) WHERE Id = @VacationPeriodId;

			END
		SET @RowIndexCount = @RowIndexCount + 1;
	END;
END
--SELECT * FROM #Temp;
INSERT INTO HumanTalent.AnnualVacationSchedule
(
    InitialContractNumber,
	EmployeeId,
    ContractId,
	VacationPeriodId,
    VacationInitialDate,
    VacationEndDate,
    IncorporationDate,
    RequestDays,
    Comments,
    Status,
    CreationUser,
    CreationDate
)
VALUES
(   (SELECT InitialContractNumber FROM Payroll.Contract WHERE (EmployeeId = @EmployeeId AND Valid = 1 AND Status = 1)),         -- InitialContractNumber - int
	@EmployeeId,
    (SELECT Id FROM Payroll.Contract WHERE (EmployeeId = @EmployeeId AND Valid = 1 AND Status = 1)),         -- ContractId - int
    (SELECT VacationPeriodId FROM #Temp), -- PeriodInitialDate - date
    @VacationInitialDate, -- VacationInitialDate - date
    @VacationEndDate, -- VacationEndDate - date
    @IncorporationDate, -- IncorporationDate - date
    @RequestDays,  -- RequestDays - tinyint
    @Comments,      -- Comments - varchar(500)  
    1,        -- Status - tinyint
    @CreationUser,  -- CreationUser - Varchar(20)
    [Common].[GETDATE]() -- CreationDate - datetime
    )
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra una solicitud de vacaciones en el cronograma anual de vacaciones del personal. A partir del número de identificación (cédula) del empleado, localiza su registro en nómina, consulta los períodos de vacaciones pendientes y distribuye los días solicitados entre dichos períodos, actualizando los días tomados y pendientes en cada uno. Finalmente, inserta el nuevo registro de vacaciones programadas en el cronograma anual, vinculándolo al contrato vigente del empleado, las fechas de inicio y fin, los días solicitados y el usuario que generó la solicitud.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'PROCEDURE', @level1name = N'SP_InsertAnnualVacationSchedule';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'PROCEDURE', @level1name = N'SP_InsertAnnualVacationSchedule';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra la programación anual de vacaciones de un empleado, distribuyendo los días solicitados entre los períodos vacacionales pendientes y actualizando sus saldos de días tomados y pendientes.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir en Common.Person con el IdentificationNumber suministrado y estar enlazada a Common.ThirdParty y Payroll.Employee para resolver el EmployeeId.; El empleado debe tener al menos un período en Payroll.VacationPeriod con PendingDays > 0; de lo contrario el WHILE no avanza y nunca se llena #Temp.; El empleado debe tener un contrato en Payroll.Contract con Valid = 1 y Status = 1 (utilizado para obtener InitialContractNumber y ContractId del registro a insertar).; @RequestDays debe ser positivo para que el bucle pueda terminar (la condición @SumPendingDays < @RequestDays exige acumular pendientes hasta cubrirlos).', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los días solicitados se distribuyen recorriendo los períodos pendientes; el procedimiento siempre intenta consumir antes los períodos con PendingDays > 0 hasta cubrir @RequestDays.; Solo se consideran períodos vacacionales del empleado con PendingDays > 0 (filtro de #TempVacationPeriod).; El EmployeeId se deriva siempre desde el IdentificationNumber a través de la cadena Person → ThirdParty → Employee.; El registro de programación insertado siempre nace con Status = 1.; InitialContractNumber y ContractId siempre provienen del contrato del empleado marcado como Valid=1 y Status=1.; La fecha de creación se obtiene de Common.GETDATE(), no de GETDATE() del servidor.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'empleado; contrato laboral; período de vacaciones; días pendientes de vacaciones; días tomados de vacaciones; programación anual de vacaciones; fecha de incorporación; tercero', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Payroll.VacationPeriod: Cuando los días pendientes acumulados aún no cubren los días solicitados (CarryPendingDays < 0 tras restar), el período se consume totalmente: PendingDays = 0 y TakenDays = VacationDays.; [UPDATE] Payroll.VacationPeriod: Cuando los pendientes del período exceden lo que falta por cubrir (CarryPendingDays >= 0), se asigna TakenDays = ABS(VacationDays - CarryPendingDays) y luego PendingDays = VacationDays - TakenDays, dejando saldo remanente.; [INSERT] HumanTalent.AnnualVacationSchedule: Al finalizar el reparto, se inserta un único registro de programación con Status = 1, CreationDate = Common.GETDATE(), tomando InitialContractNumber y ContractId del contrato vigente (Valid=1 AND Status=1) y VacationPeriodId del primer registro de #Temp.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CarryPendingDays < 0 (los pendientes acumulados aún son insuficientes para cubrir los días solicitados) → Se consume completamente el período: SET TakenDays = @VacationDays y se actualiza VacationPeriod con PendingDays = 0. else Se consume parcialmente: TakenDays = VacationDays - CarryPendingDays y PendingDays se recalcula como VacationDays - TakenDays.; si WHILE @SumPendingDays < @RequestDays → Itera sobre los períodos vacacionales del empleado (ordenados por Id ascendente dentro de #TempVacationPeriod) acumulando PendingDays hasta cubrir los días solicitados.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.Person; Common.ThirdParty; Payroll.Employee; Payroll.VacationPeriod; Payroll.Contract', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertAnnualVacationSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertAnnualVacationSchedule';
-- GO
