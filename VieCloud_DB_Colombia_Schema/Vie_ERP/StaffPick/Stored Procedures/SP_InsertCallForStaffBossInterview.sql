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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra una nueva convocatoria de selección de personal (llamado a entrevista con jefe inmediato) en el módulo de reclutamiento y gestión de talento humano. Inserta en la tabla de convocatorias de personal los datos del cargo requerido, incluyendo la posición en el organigrama, sede, unidad funcional, centro de costos, tipo de contrato, salario, horarios entre semana y fin de semana, nivel de estudios, experiencia requerida y cantidad de empleados a vincular. Se utiliza cuando un jefe o líder del área formaliza una solicitud de contratación para cubrir una vacante, ya sea por cargo nuevo, reemplazo u otro motivo.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'PROCEDURE', @level1name = N'SP_InsertCallForStaffBossInterview';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'PROCEDURE', @level1name = N'SP_InsertCallForStaffBossInterview';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra una nueva convocatoria de personal para entrevista con jefe inmediato, dejando la fecha de solicitud en la fecha actual y marcando el origen como no proveniente de requisición de personal.', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallForStaffBossInterview';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los identificadores referenciados (cargo organigrama, posición, sede, unidad funcional, centro de costo, jefe inmediato, tipo de estudio, tipo de contrato, usuario creador) deben existir conforme a las FKs implícitas de la tabla destino', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallForStaffBossInterview';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda convocatoria insertada queda con PersonalRequisitionImport=0 (no importada desde requisición de personal); Toda convocatoria insertada queda con PersonalRequisitionId=0 (sin requisición de personal asociada); La fecha de solicitud (DateRequest) se asigna siempre con la fecha/hora del servidor al momento del insert, no se acepta desde parámetro', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallForStaffBossInterview';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Convocatoria de personal; Entrevista con jefe inmediato; Cargo en organigrama; Sede / sucursal; Unidad funcional; Centro de costo; Tipo de vacante; Motivo de vacante; Tipo de contrato; Experiencia en el cargo; Salario; Periodo de contrato; Horario semana / fin de semana', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallForStaffBossInterview';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] StaffPick.CallForStaff: Siempre inserta una fila nueva con PersonalRequisitionImport=0, PersonalRequisitionId=0 y DateRequest=GETDATE(), tomando el resto de valores de los parámetros', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallForStaffBossInterview';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_InsertCallForStaffBossInterview';
-- GO
