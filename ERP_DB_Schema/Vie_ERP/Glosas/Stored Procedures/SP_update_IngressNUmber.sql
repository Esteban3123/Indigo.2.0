-- =============================================
-- Author:		
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [Glosas].[SP_update_IngressNUmber]

AS
BEGIN

	BEGIN TRY

	begin transaction 


	CREATE TABLE #tablaActualizacion(
		Id integer identity(1,1),
		Ingressnumber  varchar(100),
		DateIngress  datetime
	)
	




	INSERT INTO #tablaActualizacion
	select  AINCONSEC, AINFECING from .Adingres where AINCONSEC in (select ingressnumber from [Glosas].[GlosaPortfolioGlosada]) 



	declare @count as integer = (select count(*) from #tablaActualizacion)
	declare @contador as integer = 0

	WHILE @contador < @count BEGIN
		set @contador = @contador + 1
		declare @Date as datetime
		declare @Numingress varchar(100)
		select  @Numingress= Ingressnumber,@date = DateIngress from #tablaActualizacion where Id = @contador
		update [Glosas].[GlosaPortfolioGlosada] set IngressDate = @date where ingressnumber = @Numingress
		print @contador
	END 
	
	

	Commit Transaction
		--rollback transaction
	END TRY
	BEGIN CATCH
	rollback transaction
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END