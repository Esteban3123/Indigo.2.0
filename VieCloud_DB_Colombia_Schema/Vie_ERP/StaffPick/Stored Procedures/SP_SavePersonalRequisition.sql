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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado del módulo de selección de personal (StaffPick) que recibe un XML con los datos de una requisición de personal y ejecuta las operaciones de negocio correspondientes: crear una nueva requisición (cabecera en PersonalRequisition y detalle en PersonalRequisitionDetail), modificar una requisición existente, cancelarla o aprobarla, según la acción indicada en el XML. Gestiona el ciclo de vida completo de una solicitud de vacante, registrando información como el cargo solicitado, la sede, la unidad funcional, el centro de costo, el jefe inmediato, los horarios laborales, el tipo de contrato, el nivel de estudios requerido y el número de empleados necesarios. Es el punto central para registrar y actualizar requerimientos de contratación de nuevos colaboradores dentro de la organización.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'PROCEDURE', @level1name = N'SP_SavePersonalRequisition';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'PROCEDURE', @level1name = N'SP_SavePersonalRequisition';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa en una sola transacción la creación, modificación, cancelación y aprobación de requisiciones de personal a partir de un XML, manteniendo cabecera y trazabilidad histórica en el detalle.', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_SavePersonalRequisition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro XML debe seguir la estructura /Data con los nodos esperados (Id, Code, ActionPR, Status, etc.); ActionPR debe codificar la operación: 1=Agregar/Modificar, 2=Aprobar, 3=Cancelar; Para cancelar o aprobar (ActionPR 2/3) debe existir previamente la requisición con el Id indicado y al menos un registro en PersonalRequisitionDetail; Para insertar nueva cabecera el Id debe llegar en 0', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_SavePersonalRequisition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la operación se ejecuta dentro de una transacción explícita; ante error se hace ROLLBACK y se devuelve el mensaje de error; La fecha de creación/modificación siempre se toma del servidor vía Common.GETDATE(), no del XML de entrada; Solo se crea cabecera de requisición cuando el Id entrante es 0 (es decir, es nueva); El usuario de creación entrante se utiliza también como ModificationUser al actualizar el estado; Los registros históricos del detalle no se modifican: las operaciones de cancelar/aprobar siempre insertan un nuevo detalle replicando el último vigente; Para replicar el último detalle se selecciona el de ModificationDate más reciente por PersonalRequisitionId (ROW_NUMBER particionado, FILA=1)', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_SavePersonalRequisition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Requisición de personal; Cancelación de requisición; Aprobación de requisición; Cargo / posición organizacional; Centro de costo; Unidad funcional; Tipo de contrato; Tipo de vacante; Motivo de vacante; Jefe inmediato; Jornada laboral (semana/fin de semana)', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_SavePersonalRequisition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] StaffPick.PersonalRequisition: Cuando el registro del XML tiene Id = 0 se inserta una nueva cabecera de requisición usando CreationUser como usuario de creación/modificación y Common.GETDATE() como fechas; [INSERT] StaffPick.PersonalRequisitionDetail: Cuando ActionPR = 1 se inserta un nuevo detalle vinculado al Id del XML, o a SCOPE_IDENTITY() si la cabecera fue recién creada (Id=0); [UPDATE] StaffPick.PersonalRequisition: Cuando ActionPR = 3 (cancelar) se actualizan Status, ModificationUser y ModificationDate de la cabecera coincidente por Id; [INSERT] StaffPick.PersonalRequisitionDetail: Cuando ActionPR = 3 se replica el último detalle (FILA=1 por ModificationDate DESC) con observación ''Se cancela requisición'' y el nuevo Status/usuario; [UPDATE] StaffPick.PersonalRequisition: Cuando ActionPR = 1 (modificar) se actualizan Status, ModificationUser y ModificationDate de la cabecera con Id coincidente; [UPDATE] StaffPick.PersonalRequisition: Cuando ActionPR = 2 (aprobar) se actualizan Status, ModificationUser y ModificationDate de la cabecera coincidente por Id; [INSERT] StaffPick.PersonalRequisitionDetail: Cuando ActionPR = 2 se replica el último detalle (FILA=1 por ModificationDate DESC) con observación ''Se aprueba requisición'' y el nuevo Status/usuario; [RETURN_RESULT] (resultset): Si ocurre una excepción se hace ROLLBACK de la transacción y se devuelve un resultset con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_SavePersonalRequisition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Id = 0 (registro nuevo sin requisición previa) → Inserta cabecera nueva en PersonalRequisition else No inserta cabecera; usa Id existente; si ActionPR = 1 (Agregar/Modificar) → Inserta detalle nuevo (usando SCOPE_IDENTITY si Id=0, o el Id provisto) y actualiza Status de la cabecera existente; si ActionPR = 3 (Cancelar) → Actualiza Status de la cabecera y replica el último detalle vigente con observación ''Se cancela requisición''; si ActionPR = 2 (Aprobar) → Actualiza Status de la cabecera y replica el último detalle vigente con observación ''Se aprueba requisición''', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_SavePersonalRequisition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_SavePersonalRequisition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'StaffPick.PersonalRequisitionDetail; StaffPick.PersonalRequisition', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_SavePersonalRequisition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_SavePersonalRequisition';
-- GO
