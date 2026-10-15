

CREATE PROCEDURE [Glosas].[HomologacionClientes]

AS
BEGIN

	BEGIN TRY

begin transaction 


	
	
		
	declare @count as integer = (select count(*) from HomologarClientes)
	select @count
	declare @contador as integer = 0

	WHILE @contador < @count BEGIN

		set @contador = @contador + 1

		--print @contador
		declare @nit as varchar(20) 

		select  @nit = Nit from HomologarClientes where Id = @contador
		
		update HomologarClientes set IdVie = (select top 1 Id from Common.Customer where Nit = @Nit  ) 	where Id = @contador  

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