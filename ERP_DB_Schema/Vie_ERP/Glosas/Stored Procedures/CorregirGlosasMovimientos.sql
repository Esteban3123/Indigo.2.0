
CREATE PROCEDURE [Glosas].[CorregirGlosasMovimientos]

AS
BEGIN

	BEGIN TRY

begin transaction SALDOGLOSAYACARTERA

	CREATE TABLE #tablaCorreciones(
		Id integer identity(1,1),
		factura varchar(50),
		Diferencia decimal(18,0)
	)

	

	insert into #tablaCorreciones 
	select  distinct m.InvoiceNumber, t.Diferencia   from [dbo].[FacturaSaldoGlosaMayorCartera] t inner join 
	Glosas.GlosaMovementGlosa m on t.invoicenumber = m.invoicenumber
	where m.ValueAcceptedEAPBconciliation is null  and t.procesada is null and m.valuependingconciliation > 0 --and m.invoicenumber = 'HSP 0001255396'
	

		
	declare @count as integer = (select count(*) from [dbo].#tablaCorreciones)
	declare @contador as integer = 0
	WHILE @contador < @count BEGIN

		set @contador = @contador + 1
		
		declare @InvoiceNumber varchar(20), @IdMovimiento integer, @diferencia decimal(18,0)
		select  @InvoiceNumber = Factura, @diferencia = diferencia from dbo.#tablaCorreciones where Id = @contador


		update dbo.[FacturaSaldoGlosaMayorCartera] set Procesada = 5 where invoicenumber = @InvoiceNumber 

		declare @ValuePayments as decimal(18,0) = @diferencia
		--update glosas.GlosaMovementGlosa set ValuePayments = @diferencia, valuependingconciliation =valuependingconciliation - @diferencia where id = @IdMovimiento 
		
		               declare @Idmov as int,@MainGlosa as bit, @ValuePendingConciliation as decimal(18,0)
						--recorremos los movimientos de glosas descontando el valor del pago 
						DECLARE CursoGlosaMovementGlosa CURSOR FOR select M.Id , M.MainGlosa, M.ValuePendingConciliation  from glosas.GlosaMovementGlosa as M where InvoiceNumber  = @Invoicenumber 
						OPEN CursoGlosaMovementGlosa
						FETCH NEXT FROM CursoGlosaMovementGlosa 
						INTO @Idmov,@MainGlosa,@ValuePendingConciliation

						WHILE @@FETCH_STATUS = 0
						BEGIN								
							--mientra el saldo de pago se mayor de cero
							if @MainGlosa = 1 and @ValuePendingConciliation > 0 begin
							 
								 declare @tmpvaluepayment as decimal(18,0) =@ValuePayments
								 set @ValuePayments = @ValuePayments - @ValuePendingConciliation
								 declare @Value as decimal(18,0) = 0
								 if @ValuePayments <= 0 begin
									set @Value = @tmpvaluepayment
								 end if @ValuePayments > 0 begin
									set @Value = @ValuePendingConciliation 
								 end


								 declare @tmpaluePendingConciliation as decimal(18,0) = @ValuePendingConciliation - @value
								 declare @state as int
								 if @tmpaluePendingConciliation = 0 begin 
										set @state = 7
									end else begin
										set @state = 2
								 end
								 update glosas.GlosaMovementGlosa set TempState = State, State =@state, ValuePayments = ISNULL(ValuePayments,0) + @value , ValuePendingConciliation =  ValuePendingConciliation - @value where  id = @Idmov 


								 
							end		
							--si el saldo es menor o igual a cero, salimos del recorrido de los movimientos
							if @ValuePayments <= 0 begin
								BREAK
							end										
					
				
						FETCH NEXT FROM CursoGlosaMovementGlosa 
						INTO @Idmov,@MainGlosa,@ValuePendingConciliation
						END 
						CLOSE CursoGlosaMovementGlosa
						DEALLOCATE CursoGlosaMovementGlosa




		
		declare @valorpagado as decimal = 0
		select @valorpagado = sum(ISNULL(ValuePayments,0)) from glosas.GlosaMovementGlosa where invoicenumber = @InvoiceNumber and MainGlosa = 1		
		update glosas.GlosaPortfolioGlosada set ValuePayments = @valorpagado where   invoicenumber = @InvoiceNumber

		print @contador
	END 
	
	


	Commit Transaction SALDOGLOSAYACARTERA
	--	rollback transaction SALDOGLOSAYACARTERA
	END TRY
	BEGIN CATCH
		rollback transaction SALDOGLOSAYACARTERA
		SELECT
			ERROR_NUMBER() AS CodigoMensaje,
			ERROR_MESSAGE() AS  Mensaje
	END CATCH

END