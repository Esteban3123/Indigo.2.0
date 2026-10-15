-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [StaffPick].[SP_InsertCallForStaffBossInterview] 
	-- Add the parameters for the stored procedure here

	@Code INT,
	@DateCallForStaff DateTime,
	@OrganizationChartPositionId int,
	@PositionId int,
	@BranchOfficeId int,
	@FunctionalUnitId int,
	@CostCenterId int,
	@PositionImmediateBossId int,
	@StarTimeWeek Time,
	@EndTimeWeek Time,
	@StarTimeWeekend Time,
	@EndTimeWeekend Time,
	@StudyTypeId int,
	@VacancyType int,
	@ReasonForVacancy int,
	@TypesOfCall int,
	@ExperienceInPosition int,
	@ContractTypeId int,
	@ApplicationDate DateTime,
	@NumberEmployeesRequired int,
	@ContractPeriod int,
	@Salary int,
	@Observations varchar(500),
	@Status int,
	@CreationUser int,
	@CreationDate DateTime 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	INSERT INTO [StaffPick].[CallForStaff] 
([Code] 
,PersonalRequisitionImport
,PersonalRequisitionId
,[DateRequest] 
,[DateCallForStaff] 
,[OrganizationChartPositionId] 
,[PositionId] 
,[BranchOfficeId] 
,[FunctionalUnitId] 
,[CostCenterId] 
,[PositionImmediateBossId] 
,[StarTimeWeek] 
,[EndTimeWeek] 
,[StarTimeWeekend] 
,[EndTimeWeekend] 
,[StudyTypeId] 
,[VacancyType] 
,[ReasonForVacancy] 
,[TypesOfCall] 
,[ExperienceInPosition] 
,[ContractTypeId] 
,[ApplicationDate] 
,[NumberEmployeesRequired] 
,[ContractPeriod] 
,[Salary] 
,[Observations] 
,[Status] 
,[CreationUser] 
,[CreationDate]) 
VALUES 
(@Code 
,0 
,0
,GetDate()
,@DateCallForStaff 
,@OrganizationChartPositionId 
,@PositionId 
,@BranchOfficeId 
,@FunctionalUnitId 
,@CostCenterId 
,@PositionImmediateBossId
,@StarTimeWeek 
,@EndTimeWeek 
,@StarTimeWeekend 
,@EndTimeWeekend 
,@StudyTypeId 
,@VacancyType 
,@ReasonForVacancy 
,@TypesOfCall 
,@ExperienceInPosition 
,@ContractTypeId 
,@ApplicationDate
,@NumberEmployeesRequired 
,@ContractPeriod 
,@Salary 
,@Observations 
,@Status 
,@CreationUser 
,@CreationDate 
); 
END