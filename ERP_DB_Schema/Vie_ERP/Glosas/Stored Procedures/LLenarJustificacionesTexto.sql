


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