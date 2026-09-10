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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actualiza una convocatoria de personal (Call for Staff) existente a partir de un XML con los datos modificados. Recibe información como código de la convocatoria, requisición de personal, cargo, sede, unidad funcional, centro de costos, jefe inmediato, horarios, tipo de vacante, tipo de contrato, salario y estado, y los aplica sobre la tabla StaffPick.CallForStaff identificando el registro por su código. Además, reemplaza completamente la lista de empleados jefes entrevistadores asociados a la convocatoria, eliminando los anteriores e insertando los nuevos desde el mismo XML. Se usa en el módulo de selección y reclutamiento de personal para editar convocatorias laborales ya creadas.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateCallForStaff';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateCallForStaff';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Actualiza una convocatoria de selección de personal existente (identificada por su Code) y reemplaza la lista de jefes entrevistadores asociados, todo dentro de una transacción.', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateCallForStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe tener un nodo raíz /Data con los campos de la convocatoria.; Debe existir previamente un registro en StaffPick.CallForStaff cuyo Code coincida con el del XML; en caso contrario el UPDATE no afecta filas.; Los IdEmployee de los entrevistadores deben venir bajo /Data/DataCallForStackBossInterview.; El Code recibido debe ser único en StaffPick.CallForStaff para que la búsqueda del Id retorne un único valor.', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateCallForStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda actualización de la convocatoria deja su Status en 1 (activa).; La fecha de modificación se obtiene siempre desde Common.GETDATE() (no depende del input).; El usuario de modificación se toma del CreationUser del XML.; Antes de reinsertar entrevistadores, siempre se eliminan los previos para evitar duplicados.; Toda la operación es transaccional: si algo falla, se revierte todo y se devuelve el mensaje de error.; La convocatoria a actualizar se identifica únicamente por el campo Code.', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateCallForStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Convocatoria de personal; Requisición de personal; Vacante; Entrevistadores (jefes); Cargo/Posición; Centro de costo; Unidad funcional; Tipo de contrato; Salario', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateCallForStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] StaffPick.CallForStaff: Cuando StaffPick.CallForStaff.Code coincide con el Code del XML, se actualizan todos los datos de la convocatoria, se fija Status=1, ModificationUser=CreationUser del XML y ModificationDate=Common.GETDATE().; [DELETE] StaffPick.CallForStaffBossInterview: Tras ubicar el Id de la convocatoria por Code, se eliminan todos los registros de entrevistadores cuyo IdCallForStaff sea ese Id.; [INSERT] StaffPick.CallForStaffBossInterview: Por cada nodo /Data/DataCallForStackBossInterview con IdEmployee se inserta una fila asociando el IdCallForStaff resuelto con el IdEmployee.; [RETURN_RESULT] (error): Si ocurre una excepción, se hace ROLLBACK de la transacción y se retorna ERROR_MESSAGE() como conjunto de resultados.', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateCallForStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateCallForStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'StaffPick.CallForStaff', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateCallForStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateCallForStaff';
-- GO
