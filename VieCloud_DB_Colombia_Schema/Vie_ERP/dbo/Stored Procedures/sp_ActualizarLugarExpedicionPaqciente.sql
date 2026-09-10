

-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[sp_ActualizarLugarExpedicionPaqciente]

AS
BEGIN

	BEGIN TRY

	CREATE TABLE #tablaActualizacionPacientes(
		Id integer identity(1,1),
		Codigo varchar(25),
		Expedicion  char(40)
	)
	

	
	INSERT INTO #tablaActualizacionPacientes
	select  IPCODPACI,IPEXPEDIC from .INPACIENT where GENEXPEDITIONCITY is null

	--drop table #tablaActualizacionPacientes

	declare @count as integer = (select count(*) from #tablaActualizacionPacientes)
	select @count
	declare @contador as integer = 0

	WHILE @contador < @count BEGIN
		set @contador = @contador + 1
			
		declare @cedula as varchar(25), @Expedicion as char(40),@Idvie as integer 
		select  @cedula = Codigo,@Expedicion = ltrim(rtrim(Expedicion)) from #tablaActualizacionPacientes where Id = @contador
		
		set @Idvie = (select top 1 id from Common.City where Name = @Expedicion  ) 

		if @Idvie is not null begin
		update dbo.INPACIENT set  GENEXPEDITIONCITY =@Idvie  where IPCODPACI = @cedula
		end 
				print @contador
	END 
		
	--	rollback transaction
	END TRY
	BEGIN CATCH
	   
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de migración y corrección de datos que actualiza el campo de ciudad de expedición del documento en el maestro de pacientes (INPACIENT). Recorre todos los pacientes que tienen registrado el lugar de expedición como texto libre (IPEXPEDIC) pero no tienen asociado el identificador normalizado de ciudad (GENEXPEDITIONCITY), y busca la coincidencia por nombre en el catálogo de municipios (Common.City) para vincular el ID correcto. Sirve para sanear y estandarizar el lugar de expedición de la cédula o documento de identidad del paciente, migrando de texto libre al catálogo oficial de ciudades del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'sp_ActualizarLugarExpedicionPaqciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'sp_ActualizarLugarExpedicionPaqciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Completa masivamente la ciudad de expedición (FK) de los pacientes que la tienen vacía, mapeando el texto de expedición almacenado contra el catálogo de ciudades por nombre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_ActualizarLugarExpedicionPaqciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla Common.City debe contener los nombres de ciudad esperados para poder mapear la expedición textual a un Id', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_ActualizarLugarExpedicionPaqciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan pacientes cuyo GENEXPEDITIONCITY está NULL (no se sobrescriben valores existentes); La coincidencia entre la expedición textual del paciente y la ciudad se hace por nombre exacto (con LTRIM/RTRIM); Si hay múltiples ciudades con el mismo nombre, se toma solo la primera (TOP 1) sin ORDER BY determinístico; Los errores no se relanzan: se devuelven como resultset con código y mensaje', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_ActualizarLugarExpedicionPaqciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ciudad de expedición del documento; Catálogo de ciudades', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_ActualizarLugarExpedicionPaqciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.INPACIENT: Cuando el paciente tiene GENEXPEDITIONCITY NULL y su IPEXPEDIC (trim) coincide con Common.City.Name, se asigna GENEXPEDITIONCITY con el Id de esa ciudad; [RETURN_RESULT] (resultset): Devuelve el conteo de pacientes a procesar y, ante excepción, retorna ERROR_NUMBER y ERROR_MESSAGE en lugar de propagar el error', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_ActualizarLugarExpedicionPaqciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe un registro en Common.City cuyo Name coincide con el texto de expedición (trim) del paciente → Actualiza GENEXPEDITIONCITY del paciente con el Id de la ciudad encontrada else No se actualiza el paciente; queda con GENEXPEDITIONCITY en NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_ActualizarLugarExpedicionPaqciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; Common.City', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_ActualizarLugarExpedicionPaqciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'sp_ActualizarLugarExpedicionPaqciente';
-- GO
