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