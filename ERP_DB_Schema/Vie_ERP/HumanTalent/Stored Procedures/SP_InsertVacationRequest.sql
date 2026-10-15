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