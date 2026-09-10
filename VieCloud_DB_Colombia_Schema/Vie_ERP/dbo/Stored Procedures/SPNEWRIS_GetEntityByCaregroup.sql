-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SPNEWRIS_GetEntityByCaregroup] 
(
    -- Add the parameters for the stored procedure here
@CareGroup int -- = Null
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    -- SET NOCOUNT ON

    -- Insert statements for procedure here
	BEGIN
		DECLARE @careGroupType int = (Select CareGroupType FROM Contract.CareGroup where id = @CareGroup)
		DECLARE @EntityType int = (Select EntityType FROM Contract.CareGroup WHERE id = @CareGroup)
		DECLARE @fecha datetime = [Common].[GETDATE]() + 1
	END

	IF @careGroupType = 1
	BEGIN 
		select CAST(Car.CareGroupType AS int), Con.HealthAdministratorId, Hea.Name as HealthAdministratorDescription, 
				Con.ContractName as ContractDescription, Car.EntityType, Con.EndDate, Con.Code As ContractCode 
			from Contract.CareGroup as Car
				left join Contract.Contract as Con on Con.Id = Car.ContractId
				--left join Contract.ContractEntity as ce on ce.Id = cc.ContractEntityId
				left join Contract.HealthAdministrator as Hea on Hea.Id = Con.HealthAdministratorId
			where Car.id = @CareGroup
	END

	IF @careGroupType = 2
	BEGIN 
		select CAST(@careGroupType AS int), id, Name, Name, EntityType, @fecha, Code from Contract.HealthAdministrator where EntityType = @EntityType and Status = 1
	END

	IF @careGroupType = 3
	BEGIN 
		select CAST(@careGroupType AS int), id, Name, Name, EntityType, @fecha, '999' from Contract.HealthAdministrator where Code = '999' and Status = 1
	END

	IF @careGroupType = 4
	BEGIN
		select CAST(@careGroupType AS int), id, Name, Name, EntityType, @fecha, Code from Contract.HealthAdministrator where EntityType = @EntityType and Status = 1
	END

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que, dado un grupo de atención (CareGroup), identifica qué entidad pagadora o contrato debe aplicarse según el tipo de agrupación configurado. Soporta cuatro modalidades: (1) retorna el contrato y la administradora de salud (EPS/aseguradora) asociados directamente al grupo; (2) retorna todas las administradoras activas del mismo tipo de entidad configurado en el grupo; (3) retorna específicamente la administradora con código ''999'', usada convencionalmente para casos especiales como particular o no asegurado; (4) igual que la modalidad 2, filtrando por tipo de entidad. Se utiliza en procesos de facturación y liquidación para determinar dinámicamente el pagador o contrato vigente que corresponde a un grupo de atención, consultando las tablas de contratos, grupos de atención y administradoras de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPNEWRIS_GetEntityByCaregroup';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPNEWRIS_GetEntityByCaregroup';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resuelve dinámicamente la entidad pagadora (contrato o administradora de salud) asociada a un grupo de atención, según el tipo de grupo configurado, para usarse en procesos de facturación/liquidación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_GetEntityByCaregroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Contract.CareGroup con el id recibido para obtener CareGroupType y EntityType; Las administradoras consultadas deben tener Status = 1 (activas) para ser retornadas en las modalidades 2, 3 y 4; Para la modalidad 3 debe existir una administradora con Code = ''999'' activa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_GetEntityByCaregroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan administradoras con Status = 1 en las modalidades 2, 3 y 4; La fecha de vigencia entregada en las modalidades 2, 3 y 4 corresponde a GETDATE()+1 (día siguiente); En la modalidad 1 la fecha retornada proviene del EndDate del contrato; en las demás se usa una fecha calculada; El primer campo del resultado siempre expone el CareGroupType como entero; La modalidad 3 está reservada al código ''999'' (convención de paciente particular/no asegurado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_GetEntityByCaregroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo de atención (CareGroup); Contrato; Administradora de salud; Tipo de entidad; Código ''999'' (particular/no asegurado); Vigencia de contrato', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_GetEntityByCaregroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Contract.CareGroup: Cuando CareGroupType = 1, retorna los datos del contrato vinculado al grupo de atención (uniendo con Contract.Contract y Contract.HealthAdministrator) filtrando por el id del grupo; [RETURN_RESULT] Contract.HealthAdministrator: Cuando CareGroupType = 2, retorna todas las administradoras activas (Status=1) cuyo EntityType coincide con el del grupo de atención, usando como fecha de vigencia GETDATE()+1; [RETURN_RESULT] Contract.HealthAdministrator: Cuando CareGroupType = 3, retorna únicamente la administradora con Code=''999'' y Status=1 (caso especial particular/no asegurado), usando como fecha GETDATE()+1; [RETURN_RESULT] Contract.HealthAdministrator: Cuando CareGroupType = 4, retorna las administradoras activas (Status=1) cuyo EntityType coincide con el del grupo de atención, usando como fecha GETDATE()+1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_GetEntityByCaregroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @careGroupType = 1 → Consulta el contrato y administradora vinculados al CareGroup en Contract.CareGroup/Contract/HealthAdministrator; si @careGroupType = 2 → Lista todas las administradoras activas del mismo EntityType del grupo; si @careGroupType = 3 → Devuelve la administradora con Code=''999'' activa (caso particular/no asegurado); si @careGroupType = 4 → Lista las administradoras activas del mismo EntityType del grupo (equivalente a la modalidad 2)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_GetEntityByCaregroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Contract.Contract; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_GetEntityByCaregroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPNEWRIS_GetEntityByCaregroup';
-- GO
