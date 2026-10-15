-- =============================================
-- Author:		
-- Create date: 12-08-2014
-- Description:	Script de actualizacion de cuenats 1302 - 1303
-- =============================================
CREATE PROCEDURE [Glosas].[SP_update_AccountRadicate_Rectificable]

AS
BEGIN

	BEGIN TRY

	begin transaction ActualizarPAcientes


	CREATE TABLE #tablaActualizacionPacientes(
		Id integer identity(1,1),
		idGlosaCartera integer,
		AccountantAccountCustomers varchar(200),
		rectifiableGlosa varchar(100),
		CPCCODCUE varchar(200)
	)
	

	select * from .crNConNOT 
	select* from  .CTNCUENTA


	INSERT INTO #tablaActualizacion
	select C.id as idcartera,C.AccountantAccountCustomers,p.rectifiableGlosa,cue.CUECODIGO  from [Glosas].[GlosaPortfolioGlosada] C inner join 
[Glosas].[AccountSettingsFOX_PrivateMethod] P  on P.invoiceradicate = C.AccountantAccountCustomers inner join
 .crNConNOT  con on con.CONCODIGO  = p.rectifiableGlosa inner join
 .CTNCUENTA cue on cue.OID = con.CTNCUENTA  
where C.AccountantAccountCustomers in (select invoiceradicate from [Glosas].[AccountSettingsFOX_PrivateMethod])
and C.state in(2,3,8,11)



	declare @count as integer = (select count(*) from #tablaActualizacion)
	select @count
	declare @contador as integer = 0

	WHILE @contador < @count BEGIN

		set @contador = @contador + 1

		--print @contador
		declare @idGlosaCartera as integer, @CPCCODCUE as varchar(20) 

		select  @idGlosaCartera = idGlosaCartera,@CPCCODCUE = CPCCODCUE from #tablaActualizacion where Id = @contador
		
		update [Glosas].[GlosaPortfolioGlosada] set AccountantAccountCustomers = @CPCCODCUE where id = @idGlosaCartera

		print @contador
	END 
	
	



	--Commit Transaction
		rollback transaction
	END TRY
	BEGIN CATCH
	rollback transaction
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END