

CREATE PROCEDURE [Glosas].[LLenarJustificacionesTexto]

AS
BEGIN

	BEGIN TRY

--begin transaction GlosaCorrecion

	CREATE TABLE #TablaConciliation(
		Id integer identity(1,1),
		IdMov int
	)
		
    insert into #TablaConciliation
	select m1.Id from [Glosas].[GlosaMovementGlosa]  m9 inner join 
	[Glosas].[GlosaMovementGlosa] m1 on m9.id = m1.id 
	where m9.JustificationGlosatext is null

	declare @count as integer = (select count(*) from #TablaConciliation)
	declare @contador as integer = 0
	print @count
	WHILE @contador < @count BEGIN

		set @contador = @contador + 1
		declare   @Idmovtmp int
    	select @Idmovtmp = IdMov from #TablaConciliation where Id = @contador
		

		declare @JustificationGlosatext as varchar(max)
		declare @RationaleGlosa as varchar(max)
		declare @RationaleReiteration as varchar(max)
		declare @RationaleConciliation as varchar(max)
		declare @JustificationReiterationText  as varchar(max)
	
		select @RationaleGlosa = RationaleGlosa, @JustificationGlosatext = JustificationGlosatext , @RationaleReiteration =RationaleReiteration,@JustificationReiterationText =JustificationReiterationText, @RationaleConciliation =RationaleConciliation from [Glosas].[GlosaMovementGlosa]  where id = @Idmovtmp 
	    
		update [Glosas].[GlosaMovementGlosa] set RationaleGlosa =@RationaleGlosa , JustificationGlosatext = @JustificationGlosatext,  RationaleReiteration =@RationaleReiteration, JustificationReiterationText =@JustificationReiterationText ,RationaleConciliation =@RationaleConciliation where Id = @Idmovtmp 
		

		print @contador
	END 
	
	
	
	-- Commit Transaction GlosaCorrecion
		--rollback transaction GlosaCorrecion
	END TRY
	BEGIN CATCH
	--	rollback transaction GlosaCorrecion
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de corrección y relleno de justificaciones en los movimientos de glosa. Recorre todos los registros de la tabla de movimientos de glosa (GlosaMovementGlosa) que tengan el texto de justificación de glosa vacío o nulo, y para cada uno vuelve a escribir los campos de justificación y fundamento (rationale) de glosa, reiteración y conciliación con los valores ya existentes en el mismo registro. Sirve como utilidad de saneamiento de datos para garantizar que los campos de texto justificativo del ciclo de glosa —justificación de la glosa inicial, reiteración y conciliación— queden consistentes y no nulos, asegurando la integridad de la información de glosas ante la aseguradora (EAPB) en el proceso de facturación y conciliación.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'LLenarJustificacionesTexto';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'LLenarJustificacionesTexto';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recorre los movimientos de glosa cuya justificación de texto está vacía y reescribe sobre sí mismos los campos de justificación y motivos (glosa, reiteración y conciliación), efectivamente refrescando/normalizando dichos valores.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'LLenarJustificacionesTexto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la tabla Glosas.GlosaMovementGlosa con los campos de justificaciones y motivos; Se requieren permisos de creación de tablas temporales, lectura y actualización sobre Glosas.GlosaMovementGlosa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'LLenarJustificacionesTexto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan movimientos de glosa con JustificationGlosatext nulo; El proceso no inserta ni elimina registros, únicamente actualiza los existentes; Los valores escritos en el UPDATE provienen del mismo registro que se actualiza, por lo que no se introducen datos externos; Los errores son capturados y devueltos como resultset, nunca relanzados', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'LLenarJustificacionesTexto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Justificación de glosa; Motivo de glosa; Reiteración de glosa; Conciliación de glosa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'LLenarJustificacionesTexto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Glosas.GlosaMovementGlosa: Para cada movimiento cuyo JustificationGlosatext es NULL, se leen sus valores actuales de RationaleGlosa, JustificationGlosatext, RationaleReiteration, JustificationReiterationText y RationaleConciliation y se reescriben sobre el mismo registro (UPDATE auto-asignación).; [RETURN_RESULT] (resultset): En caso de error en el TRY, devuelve un resultset con ERROR_NUMBER() y ERROR_MESSAGE() en lugar de propagar la excepción.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'LLenarJustificacionesTexto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si JustificationGlosatext IS NULL en Glosas.GlosaMovementGlosa → El registro se incluye en la tabla temporal y se procesa en el bucle de actualización else El registro se omite y no recibe ninguna actualización', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'LLenarJustificacionesTexto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaMovementGlosa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'LLenarJustificacionesTexto';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'LLenarJustificacionesTexto';
-- GO
