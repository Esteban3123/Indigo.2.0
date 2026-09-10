-- =============================================
-- Author:		Johan Carranza
-- Create date: 2020-03-10
-- Description:	Valida la información del archivo Excel para cargue masivo de dependientes familiares
-- =============================================
CREATE PROCEDURE [StaffPick].[SP_InsertCallFosStaff] 
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona el ciclo completo de requisiciones de personal (selección de talento humano) recibiendo los datos en formato XML. Permite crear, modificar, aprobar y cancelar solicitudes de nuevas vacantes, registrando tanto la cabecera de la requisición (cargo, sede, unidad funcional, centro de costo, jefe inmediato, horarios) en la tabla StaffPick.PersonalRequisition, como el detalle del perfil requerido (tipo de estudio, tipo de vacante, motivo de vacante, tipo de contrato, número de empleados requeridos) en StaffPick.PersonalRequisitionDetail. Es el punto central del módulo de convocatoria y reclutamiento de staff, utilizado para registrar masivamente o individualmente las necesidades de contratación de la organización.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'PROCEDURE', @level1name = N'SP_InsertCallFosStaff';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'PROCEDURE', @level1name = N'SP_InsertCallFosStaff';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa desde un XML las operaciones de creación, modificación, cancelación y aprobación de requisiciones de personal, registrando cabecera y trazabilidad en el detalle según la acción indicada.', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallFosStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /Data con los nodos esperados (Id, Code, ActionPR, Status, etc.).; ActionPR debe indicar la operación: 1=Agregar/Modificar, 2=Aprobar, 3=Cancelar.; Para crear cabecera nueva, Id debe venir en 0.; Para cancelar/aprobar/modificar una requisición existente, Id debe corresponder a un PersonalRequisition existente.; Debe existir al menos un registro previo en PersonalRequisitionDetail para la requisición al cancelar o aprobar (se toma el último por ModificationDate).', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallFosStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda operación se ejecuta dentro de una transacción explícita; ante error se hace ROLLBACK completo.; La fecha de creación/modificación siempre se obtiene mediante [Common].[GETDATE]() y no del XML.; Los detalles de cancelación y aprobación siempre se generan a partir del último detalle existente (ROW_NUMBER ORDER BY ModificationDate DESC, FILA=1).; Las operaciones de cancelación y aprobación dejan trazabilidad en PersonalRequisitionDetail con observaciones fijas (''Se cancela requisición'' / ''Se aprueba requisición'').', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallFosStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Requisición de personal; Cabecera y detalle de requisición; Cancelación de requisición; Aprobación de requisición; Modificación de requisición; Vacante; Tipo de contrato; Cargo / posición organizacional', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallFosStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] StaffPick.PersonalRequisition: Cuando Id = 0 en el XML, se inserta una nueva cabecera de requisición de personal con CreationUser y fecha actual.; [INSERT] StaffPick.PersonalRequisitionDetail: Cuando ActionPR = 1, se inserta el detalle vinculado al Id del XML o, si Id=0, al SCOPE_IDENTITY() de la cabecera recién creada.; [UPDATE] StaffPick.PersonalRequisition: Cuando ActionPR = 3 (cancelar), se actualiza Status, ModificationUser y ModificationDate de la requisición cuyo Id coincide.; [INSERT] StaffPick.PersonalRequisitionDetail: Cuando ActionPR = 3, se clona el último detalle (FILA=1 por ModificationDate DESC) de la requisición agregando observación ''Se cancela requisición'' y el nuevo Status.; [UPDATE] StaffPick.PersonalRequisition: Cuando ActionPR = 1 y el Id coincide, se actualiza Status, ModificationUser y ModificationDate (modificación tras cancelación).; [UPDATE] StaffPick.PersonalRequisition: Cuando ActionPR = 2 (aprobar), se actualiza Status, ModificationUser y ModificationDate de la requisición cuyo Id coincide.; [INSERT] StaffPick.PersonalRequisitionDetail: Cuando ActionPR = 2, se clona el último detalle (FILA=1 por ModificationDate DESC) agregando observación ''Se aprueba requisición'' y el nuevo Status.; [RETURN_RESULT] : Si ocurre una excepción, se hace ROLLBACK de la transacción y se retorna ERROR_MESSAGE() como ErrorMessage.', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallFosStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Id = 0 en el XML → Inserta nueva cabecera en PersonalRequisition else No crea cabecera; usa el Id provisto; si ActionPR = 1 → Inserta detalle (con SCOPE_IDENTITY si Id=0) y actualiza estado de cabecera existente; si ActionPR = 3 → Cancela: actualiza estado de cabecera e inserta detalle de trazabilidad con texto ''Se cancela requisición''; si ActionPR = 2 → Aprueba: actualiza estado de cabecera e inserta detalle de trazabilidad con texto ''Se aprueba requisición''; si Al insertar detalle tras crear cabecera: Id <> 0 → Usa Id del XML como PersonalRequisitionId else Usa SCOPE_IDENTITY() de la cabecera recién insertada', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallFosStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallFosStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'StaffPick.PersonalRequisition; StaffPick.PersonalRequisitionDetail', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallFosStaff';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallFosStaff';
-- GO
