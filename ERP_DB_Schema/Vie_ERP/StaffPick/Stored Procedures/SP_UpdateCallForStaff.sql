-- =============================================
-- Author:		Johan Carranza
-- Create date: 2020-03-10
-- Description:	Valida la información del archivo Excel para cargue masivo de dependientes familiares
-- =============================================
CREATE PROCEDURE [StaffPick].[SP_UpdateCallForStaff] 
	@XMLObj XML
AS
BEGIN
	DECLARE @dataFile TABLE([Code] [varchar](20),[PersonalRequisitionImport] [int], [PersonalRequisitionId] [int],
	[DateRequest] [datetime], [DateCallForStaff] [DateTime], [OrganizationChartPositionId] [int],[PositionId] [int] ,[BranchOfficeId] [int] ,
	[FunctionalUnitId] [int] ,[CostCenterId] [int] ,[PositionImmediateBossId] [int],[StarTimeWeek] [time](7) ,[EndTimeWeek] [time](7) ,[StarTimeWeekend] [time](7) ,
	[EndTimeWeekend] [time](7),[StudyTypeId] [int],[VacancyType] [tinyint],	[ReasonForVacancy] [tinyint],[TypesOfCall] [tinyint] ,
	[ExperienceInPosition] [tinyint] ,[ContractTypeId] [int], [ApplicationDate] [date], [NumberEmployeesRequired] [tinyint], 
	[ContractPeriod] [varchar](30),[Salary] [int], [Observations] [varchar](500), [Status] [tinyint] ,[CreationUser] [varchar](20) ,[CreationDate] [datetime])

	DECLARE @dataFileCallForStaffInterview TABLE([IdEmployee] [int])

	DECLARE @IdCallForStaff INT = 0

    BEGIN TRAN [tran1]

	BEGIN TRY

	INSERT INTO @dataFile
	SELECT
	t.x.value('Code[1]', 'VARCHAR(100)') AS Code,
	t.x.value('PersonalRequisitionImport[1]', 'VARCHAR(100)') AS PersonalRequisitionImport,	
	t.x.value('PersonalRequisitionId[1]', 'INT') AS PersonalRequisitionId,	
	t.x.value('DateRequest[1]', 'VARCHAR(100)') AS DateRequest,
	t.x.value('DateCallForStaff[1]', 'VARCHAR(100)') AS DateCallForStaff,
	t.x.value('OrganizationChartPositionId[1]', 'VARCHAR(100)') AS OrganizationChartPositionId,
	t.x.value('PositionId[1]', 'VARCHAR(100)') AS PositionId,
	t.x.value('BranchOfficeId[1]', 'VARCHAR(100)') AS BranchOfficeId,
	t.x.value('FunctionalUnitId[1]', 'VARCHAR(100)') AS FunctionalUnitId,
	t.x.value('CostCenterId[1]', 'VARCHAR(100)') AS CostCenterId,
	t.x.value('PositionImmediateBossId[1]', 'VARCHAR(100)') AS PositionImmediateBossId,
	t.x.value('StarTimeWeek[1]', 'VARCHAR(100)') AS StarTimeWeek,
	t.x.value('EndTimeWeek[1]', 'VARCHAR(100)') AS EndTimeWeek,
	t.x.value('StarTimeWeekend[1]', 'VARCHAR(100)') AS StarTimeWeekend,
	t.x.value('EndTimeWeekend[1]', 'VARCHAR(100)') AS EndTimeWeekend,
	t.x.value('StudyTypeId[1]', 'VARCHAR(100)') AS StudyTypeId,
	t.x.value('VacancyType[1]', 'VARCHAR(100)') AS VacancyType,
	t.x.value('ReasonForVacancy[1]', 'VARCHAR(100)') AS ReasonForVacancy,
	t.x.value('TypesOfCall[1]', 'VARCHAR(100)') AS TypesOfCall,
	t.x.value('ExperienceInPosition[1]', 'VARCHAR(100)') AS ExperienceInPosition,
	t.x.value('ContractTypeId[1]', 'VARCHAR(100)') AS ContractTypeId,
	t.x.value('ApplicationDate[1]', 'VARCHAR(100)') AS ApplicationDate,
	t.x.value('NumberEmployeesRequired[1]', 'VARCHAR(100)') AS NumberEmployeesRequired,
	t.x.value('ContractPeriod[1]', 'VARCHAR(100)') AS ContractPeriod,
	t.x.value('Salary[1]', 'VARCHAR(100)') AS Salary,
	t.x.value('Observations[1]', 'VARCHAR(100)') AS Observations,
	t.x.value('Status[1]', 'VARCHAR(100)') AS Status,
	t.x.value('CreationUser[1]', 'VARCHAR(100)') AS CreationUser,
	t.x.value('CreationDate[1]', 'VARCHAR(100)') AS CreationDate
	FROM @XMLObj.nodes('/Data') t(x)


	--OPERACION AGREGAR

	--Insercion en la tabla cabecera Call For Staff.
	UPDATE StaffPick.CallForStaff
	SET Code = df.Code, PersonalRequisitionImport = df.PersonalRequisitionImport, PersonalRequisitionId = df.PersonalRequisitionId,
	DateRequest = df.DateRequest, DateCallForStaff = df.DateCallForStaff, OrganizationChartPositionId = df.OrganizationChartPositionId,
	PositionId = df.PositionId, BranchOfficeId = df.BranchOfficeId, FunctionalUnitId = df.FunctionalUnitId, CostCenterId = df.CostCenterId,
	PositionImmediateBossId = df.PositionImmediateBossId, StarTimeWeek = df.StarTimeWeek, EndTimeWeek = df.EndTimeWeek, StarTimeWeekend = df.StarTimeWeekend,
	EndTimeWeekend = df.EndTimeWeekend, StudyTypeId = df.StudyTypeId, VacancyType = df.VacancyType, ReasonForVacancy = df.ReasonForVacancy,
	TypesOfCall = df.TypesOfCall, ExperienceInPosition = df.ExperienceInPosition, ContractTypeId = df.ContractTypeId, ApplicationDate = df.ApplicationDate,
	NumberEmployeesRequired = df.NumberEmployeesRequired, Salary = df.Salary, Observations = df.Observations, Status = 1, ModificationUser = df.CreationUser,
	ModificationDate = [Common].[GETDATE]()
	FROM @dataFile df 
	WHERE StaffPick.CallForStaff.Code  = df.Code


	DECLARE @CodeDataFile VARCHAR(20)

	SELECT @CodeDataFile = Code FROM @dataFile

	SELECT @IdCallForStaff = Id FROM StaffPick.CallForStaff where Code = @CodeDataFile

	DELETE FROM StaffPick.CallForStaffBossInterview WHERE IdCallForStaff=@IdCallForStaff;
	
	INSERT INTO @dataFileCallForStaffInterview
	SELECT
	f.x.value('IdEmployee[1]', 'VARCHAR(100)') AS IdEmployee
	FROM @XMLObj.nodes('/Data/DataCallForStackBossInterview') f(x)
	
	INSERT INTO StaffPick.CallForStaffBossInterview
	SELECT @IdCallForStaff, IdEmployee
	FROM @dataFileCallForStaffInterview


	
	COMMIT TRAN [tran1]
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS ErrorMessage
		ROLLBACK TRAN [tran1]
	END CATCH
END