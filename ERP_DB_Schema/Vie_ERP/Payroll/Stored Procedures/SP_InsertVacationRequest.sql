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