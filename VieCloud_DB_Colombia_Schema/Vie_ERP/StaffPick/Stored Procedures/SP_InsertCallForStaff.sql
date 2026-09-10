-- =============================================
-- Author:		Johan Carranza
-- Create date: 2020-03-10
-- Description:	Valida la información del archivo Excel para cargue masivo de dependientes familiares
-- =============================================
CREATE PROCEDURE [StaffPick].[SP_InsertCallForStaff] 
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
	INSERT INTO StaffPick.CallForStaff(Code,PersonalRequisitionImport, PersonalRequisitionId,DateRequest,DateCallForStaff,
	OrganizationChartPositionId, PositionId, BranchOfficeId, FunctionalUnitId, CostCenterId, PositionImmediateBossId, StarTimeWeek,
	EndTimeWeek, StarTimeWeekend, EndTimeWeekend, StudyTypeId, VacancyType, ReasonForVacancy, TypesOfCall, ExperienceInPosition,
	ContractTypeId, ApplicationDate, NumberEmployeesRequired, ContractPeriod, Salary, Observations, Status, CreationUser, CreationDate)
	SELECT Code,PersonalRequisitionImport, PersonalRequisitionId, DateRequest, DateCallForStaff, OrganizationChartPositionId, PositionId, 
	BranchOfficeId, FunctionalUnitId, CostCenterId, PositionImmediateBossId, StarTimeWeek, EndTimeWeek, StarTimeWeekend, EndTimeWeekend, 
	StudyTypeId, VacancyType, ReasonForVacancy, TypesOfCall, ExperienceInPosition, ContractTypeId, ApplicationDate, NumberEmployeesRequired,ContractPeriod,
	Salary, Observations, 1, CreationUser, [Common].[GETDATE]()
	FROM @dataFile
	
	SET @IdCallForStaff = SCOPE_IDENTITY();
		
	INSERT INTO @dataFileCallForStaffInterview
	SELECT
	f.x.value('IdEmployee[1]', 'VARCHAR(100)') AS IdEmployee
	FROM @XMLObj.nodes('/Data/DataCallForStackBossInterview') f(x)

	----Insercion en la tabla detalle Call For Staff Boss interview.

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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra una nueva convocatoria de selección de personal (llamado a concurso) a partir de datos enviados en formato XML. Procesa la información de la convocatoria —cargo, sede, unidad funcional, centro de costo, tipo de contrato, salario, horarios y requisitos académicos— y la inserta en la tabla principal CallForStaff junto con los empleados jefe designados para la entrevista en la tabla CallForStaffBossInterview. Está diseñado para el módulo de gestión de talento humano (StaffPick) y permite crear convocatorias de vacantes con toda su configuración organizacional en una sola operación transaccional.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'PROCEDURE', @level1name = N'SP_InsertCallForStaff';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'PROCEDURE', @level1name = N'SP_InsertCallForStaff';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra de forma transaccional una convocatoria de personal y sus entrevistadores (jefes) a partir de un XML, dejándola activa por defecto.', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallForStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener un nodo raíz /Data con los campos de la convocatoria; Los nodos /Data/DataCallForStackBossInterview deben aportar IdEmployee de los entrevistadores; Deben existir referencias válidas (PersonalRequisitionId, OrganizationChartPositionId, PositionId, BranchOfficeId, FunctionalUnitId, CostCenterId, PositionImmediateBossId, StudyTypeId, ContractTypeId) para mantener integridad', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallForStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda convocatoria insertada queda con Status=1 (activa) sin importar el valor recibido en el XML; La fecha de creación se sobrescribe con Common.GETDATE(), ignorando el CreationDate del XML; Las entrevistas con jefe siempre se asocian al Id de la convocatoria recién creada (SCOPE_IDENTITY); La cabecera y el detalle se insertan en una misma transacción atómica; si algo falla se hace rollback completo; Los errores se devuelven como result set (ErrorMessage), no se relanzan con RAISERROR', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallForStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Convocatoria de personal; Requisición de personal; Entrevista con jefe; Vacante; Tipo de contrato; Centro de costo; Unidad funcional; Sucursal', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallForStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] StaffPick.CallForStaff: Por cada nodo /Data del XML se inserta una convocatoria con Status fijo en 1 y CreationDate=Common.GETDATE(); [INSERT] StaffPick.CallForStaffBossInterview: Por cada nodo /Data/DataCallForStackBossInterview se inserta una fila vinculando el IdEmployee al Id de la convocatoria creada (SCOPE_IDENTITY); [RETURN_RESULT] (error): Si ocurre una excepción dentro de la transacción se hace ROLLBACK y se retorna ERROR_MESSAGE() como result set', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallForStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallForStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallForStaff';
-- GO
