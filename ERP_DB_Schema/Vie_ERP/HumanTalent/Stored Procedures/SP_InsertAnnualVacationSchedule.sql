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