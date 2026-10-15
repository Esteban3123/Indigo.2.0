-- =============================================
-- Author:		Johan Carranza
-- Create date: 2020-03-10
-- Description:	Valida la información del archivo Excel para cargue masivo de dependientes familiares
-- =============================================
CREATE PROCEDURE [StaffPick].[SP_SavePersonalRequisition] 
	@XMLObj XML
AS
BEGIN
	DECLARE @dataFile TABLE([Id] INT,[Code] [varchar](20),[DateRequest] [datetime],[OrganizationChartPositionId] [int],[PositionId] [int] ,[BranchOfficeId] [int] ,
	[FunctionalUnitId] [int] ,[CostCenterId] [int] ,[PositionImmediateBossId] [int],[StarTimeWeek] [time](7) ,[EndTimeWeek] [time](7) ,[StarTimeWeekend] [time](7) ,
	[EndTimeWeekend] [time](7),[Observation] [varchar](500),[Status] [tinyint] ,[CreationUser] [varchar](20) ,[CreationDate] [datetime],
	[ModificationUser] [varchar](20) ,[ModificationDate] [datetime],[PersonalRequisitionId] [int],[StudyTypeId] [int],[VacancyType] [tinyint] ,
	[ReasonForVacancy] [tinyint],[TypesOfCall] [tinyint] ,[ExperienceInPosition] [tinyint] ,[ContractTypeId] [int] ,[ApplicationDate] [date],
	[NumberEmployeesRequired] [tinyint],[ContractPeriod] [varchar](30),[Observations] [varchar](300), ModificationDatePRD DATE, ActionPR INT)

	DECLARE @IdPersonalRequisition INT = 0

    BEGIN TRAN [tran1]

	BEGIN TRY

	INSERT INTO @dataFile
	SELECT
	t.x.value('Id[1]', 'VARCHAR(100)') AS Id,
	t.x.value('Code[1]', 'VARCHAR(100)') AS Code,
	t.x.value('DateRequest[1]', 'VARCHAR(100)') AS DateRequest,
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
	t.x.value('Observation[1]', 'VARCHAR(100)') AS Observation,
	t.x.value('Status[1]', 'VARCHAR(100)') AS Status,
	t.x.value('CreationUser[1]', 'VARCHAR(100)') AS CreationUser,
	t.x.value('CreationDate[1]', 'VARCHAR(100)') AS CreationDate,
	t.x.value('ModificationUser[1]', 'VARCHAR(100)') AS ModificationUser,
	t.x.value('ModificationDate[1]', 'VARCHAR(100)') AS ModificationDate,
	t.x.value('PersonalRequisitionId[1]', 'VARCHAR(100)') AS PersonalRequisitionId,
	t.x.value('StudyTypeId[1]', 'VARCHAR(100)') AS StudyTypeId,
	t.x.value('VacancyType[1]', 'VARCHAR(100)') AS VacancyType,
	t.x.value('ReasonForVacancy[1]', 'VARCHAR(100)') AS ReasonForVacancy,
	t.x.value('TypesOfCall[1]', 'VARCHAR(100)') AS TypesOfCall,
	t.x.value('ExperienceInPosition[1]', 'VARCHAR(100)') AS ExperienceInPosition,
	t.x.value('ContractTypeId[1]', 'VARCHAR(100)') AS ContractTypeId,
	t.x.value('ApplicationDate[1]', 'VARCHAR(100)') AS ApplicationDate,
	t.x.value('NumberEmployeesRequired[1]', 'VARCHAR(100)') AS NumberEmployeesRequired,
	t.x.value('ContractPeriod[1]', 'VARCHAR(100)') AS ContractPeriod,
	t.x.value('Observations[1]', 'VARCHAR(100)') AS Observations,
	t.x.value('ModificationDate[1]', 'VARCHAR(100)') AS ModificationDatePRD,
	t.x.value('ActionPR[1]', 'VARCHAR(100)') AS ActionPR
	FROM @XMLObj.nodes('/Data') t(x)


	--OPERACION AGREGAR

	--Insercion en la tabla cabecera de requisicion de personal.
	INSERT INTO StaffPick.PersonalRequisition
	SELECT Code,DateRequest, OrganizationChartPositionId, PositionId, BranchOfficeId, FunctionalUnitId, CostCenterId, PositionImmediateBossId, StarTimeWeek,
	EndTimeWeek, StarTimeWeekend, EndTimeWeekend, Observation, Status, CreationUser, [Common].[GETDATE](), CreationUser, [Common].[GETDATE]()
	FROM @dataFile
	WHERE Id = 0

	--Insercion en la tabla detalle de requisicion de personal.

	INSERT INTO StaffPick.PersonalRequisitionDetail
	SELECT CASE WHEN Id <> 0 THEN Id ELSE SCOPE_IDENTITY() END as PersonalRequisitionId, StudyTypeId, VacancyType, ReasonForVacancy, TypesOfCall, ExperienceInPosition, ContractTypeId, ApplicationDate,
	NumberEmployeesRequired, ContractPeriod, Observations, [Common].[GETDATE](), CreationUser, Status
	FROM @dataFile
	WHERE ActionPR = 1

	--OPERACION CANCELAR

	--SELECT @IdPersonalRequisition=t.x.value('Id[1]', 'VARCHAR(100)') 
	--FROM @XMLObj.nodes('/Data') t(x)

	--Actualizar cuando se cancela una requisición.
	UPDATE StaffPick.PersonalRequisition SET [Status] = df.Status, ModificationUser = df.CreationUser, ModificationDate = [Common].[GETDATE]()
	FROM @dataFile df
	INNER JOIN StaffPick.PersonalRequisition PR ON PR.Id = df.Id
	WHERE df.ActionPR = 3

	--Insercion en datafile para cancelar requisicion
	INSERT INTO StaffPick.PersonalRequisitionDetail
	SELECT PRD.PersonalRequisitionId, PRD.StudyTypeId, PRD.VacancyType, PRD.ReasonForVacancy, PRD.TypesOfCall, PRD.ExperienceInPosition, PRD.ContractTypeId ,
	PRD.ApplicationDate, PRD.NumberEmployeesRequired, PRD.ContractPeriod, 'Se cancela requisición', [Common].[GETDATE](), df.CreationUser AS UserStatus, df.Status as StatusRequest --Cancelado
	FROM (SELECT ROW_NUMBER() OVER (PARTITION BY PersonalRequisitionId ORDER BY ModificationDate DESC) AS FILA, *
    FROM StaffPick.PersonalRequisitionDetail ) PRD
	INNER JOIN @datafile df ON df.Id = PRD.PersonalRequisitionId AND df.ActionPR = 3
	WHERE FILA=1 AND PRD.PersonalRequisitionId = df.Id

	--OPERACION MODIFICAR
	
	--Actualizar cuando se modifica una requisición despues de cancelarla.
	UPDATE StaffPick.PersonalRequisition SET [Status] = df.Status, ModificationUser = df.CreationUser, ModificationDate = [Common].[GETDATE]()
	FROM @dataFile df
	WHERE df.ActionPR = 1 AND StaffPick.PersonalRequisition.Id = df.Id

	--OPERACION APROBAR

	--Actualizar cuando se aprueba una requisición.
	UPDATE StaffPick.PersonalRequisition SET [Status] = df.Status, ModificationUser = df.CreationUser, ModificationDate = [Common].[GETDATE]()
	FROM @dataFile df
	INNER JOIN StaffPick.PersonalRequisition PR ON PR.Id = df.Id
	WHERE df.ActionPR = 2

	--Insercion en datafile para aprobar requisicion
	INSERT INTO StaffPick.PersonalRequisitionDetail
	SELECT PRD.PersonalRequisitionId, PRD.StudyTypeId, PRD.VacancyType, PRD.ReasonForVacancy, PRD.TypesOfCall, PRD.ExperienceInPosition, PRD.ContractTypeId ,
	PRD.ApplicationDate, PRD.NumberEmployeesRequired, PRD.ContractPeriod, 'Se aprueba requisición', [Common].[GETDATE](), df.CreationUser AS UserStatus, df.Status as StatusRequest --Aprobado
	FROM (SELECT ROW_NUMBER() OVER (PARTITION BY PersonalRequisitionId ORDER BY ModificationDate DESC) AS FILA, *
    FROM StaffPick.PersonalRequisitionDetail ) PRD
	INNER JOIN @datafile df ON df.Id = PRD.PersonalRequisitionId AND df.ActionPR = 2
	WHERE FILA=1 AND PRD.PersonalRequisitionId = df.Id



	COMMIT TRAN [tran1]
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() AS ErrorMessage
		ROLLBACK TRAN [tran1]
	END CATCH
END