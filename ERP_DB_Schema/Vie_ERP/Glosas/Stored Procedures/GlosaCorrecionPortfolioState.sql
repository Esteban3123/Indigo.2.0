

CREATE PROCEDURE [Glosas].[GlosaCorrecionPortfolioState]

AS
BEGIN

	BEGIN TRY

begin transaction GlosaCorrecion

	
		
  
	declare @count as integer = (select count(*) from Glosas.ConciliationTemp)
	declare @contador as integer = 0
	print @count
	WHILE @contador < @count BEGIN

		set @contador = @contador + 1
		declare   @InvoiceNumber varchar(20)
    	select @InvoiceNumber = Invoicenumber from Glosas.ConciliationTemp where Id = @contador

		update [Glosas].[GlosaPortfolioGlosada] set state = 6, TempState = state where InvoiceNumber =@invoiceNUmber
			 
		print @contador
	END 
	
	
	
	 Commit Transaction GlosaCorrecion
		--rollback transaction GlosaCorrecion
	END TRY
	BEGIN CATCH
		rollback transaction GlosaCorrecion
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END