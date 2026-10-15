

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