

CREATE PROCEDURE [Glosas].[GlosaCorrecion]

AS
BEGIN

	BEGIN TRY

begin transaction GlosaCorrecion

	CREATE TABLE #TablaConciliation(
		Id integer identity(1,1),
		IdConciliation int,
		Factura varchar(20)
	)
		
    insert into #TablaConciliation
	select Id,Invoicenumber from [Glosas].[ConciliationD] where state = 1 and invoicenumber not in ('HSP 0001351542','HSP 0001305525','HSP 0001305425')

	declare @count as integer = (select count(*) from #TablaConciliation)
	declare @contador as integer = 0
	print @count
	WHILE @contador < @count BEGIN

		set @contador = @contador + 1
		declare   @InvoiceNumber varchar(20)
    	select @InvoiceNumber = Factura from #TablaConciliation where Id = @contador

		update [Glosas].[GlosaMovementGlosa] set ValuePendingConciliation = isnull(ValueAcceptedIPSconciliation + ValueAcceptedEAPBconciliation,ValuePendingConciliation), State =2,TempState = state where InvoiceNumber = @invoiceNUmber
		-------------------------------------reseteo valores en movimiento de glosa, comentaripo y fecha de la conciliacion-----------------------------------
		update [Glosas].[GlosaMovementGlosa] set ValueAcceptedIPSconciliation = null, ValueAcceptedEAPBconciliation = null , rationaleconciliation = null, rationaleDateConciliation = null where InvoiceNumber = @invoiceNUmber
		-----------------------------------------------------------------------------------------------------------	
			 
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