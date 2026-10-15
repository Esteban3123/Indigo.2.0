-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [HumanTalent].[InsertAnnualVacationSchedule]
	-- Add the parameters for the stored procedure here
	@RequestDays INT,
	@EmployeeId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	SET NOCOUNT ON;
	DECLARE @SumPendingDays INT, @VacationPeriodId INT,@VacationPeriodPendingDays INT, @RowIndexCount INT, @DaysToken INT;
    -- Insert statements for procedure here
	
	SET @SumPendingDays = 0;
	SET @RowIndexCount = 1;
	--Obtener en una tabla temporal los Id's de los Vacation Periods y los index ordenada de manera descendente por fecha inicial
	SELECT Id, PendingDays INTO #TempVacationPeriod FROM Payroll.VacationPeriod WHERE (EmployeeId = @EmployeeId AND PendingDays > 0) ORDER BY InitialDatePeriod DESC;
	create table #Temp
	( 
		id INT IDENTITY NOT NULL PRIMARY KEY,
		VacationPeriodId INT,
		PendingDays INT,
		RequestDays INT,
		DaysTaken INT,
		SumPendingDays INT
		
	)
	WHILE @SumPendingDays < @RequestDays
	BEGIN
		WITH OrderedOrders AS  
		(  
		   SELECT ROW_NUMBER() OVER(ORDER BY Id ASC) AS RowIndex, Id, PendingDays  FROM #TempVacationPeriod  
		)   
		SELECT @VacationPeriodId=Id, @VacationPeriodPendingDays=OrderedOrders.PendingDays
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
		SET @RowIndexCount = @RowIndexCount + 1;
	END;
END
SELECT * FROM #Temp;
--INSERT INTO Payroll.VacationPeriod
--(
--    EmployeeId,
--    ContractId,
--    InitialDatePeriod,
--    EndDatePeriod,
--    VacationDays,
--    PendingDays,
--    TakenDays,
--    CreationUser,
--    CreationDate,
--    ModificationUser,
--    ModificationDate
--)
--VALUES
--(   @EmployeeId,         -- EmployeeId - int
--    (SELECT ContractId FROM Payroll.Contract WHERE @EmployeeId),         -- ContractId - int
--    [Common].[GETDATE](), -- InitialDatePeriod - date
--    [Common].[GETDATE](), -- EndDatePeriod - date
--    0,         -- VacationDays - tinyint
--    0,         -- PendingDays - tinyint
--    0,         -- TakenDays - tinyint
--    '',        -- CreationUser - varchar(20)
--    [Common].[GETDATE](), -- CreationDate - datetime
--    '',        -- ModificationUser - varchar(20)
--    [Common].[GETDATE]()  -- ModificationDate - datetime
--    )