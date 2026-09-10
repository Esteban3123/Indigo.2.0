
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de corrección de saldos de glosas que detecta y ajusta inconsistencias donde el saldo pendiente de conciliación de los movimientos de glosa (GlosaMovementGlosa) supera el saldo registrado en cartera de la factura. Recorre cada factura afectada e itera sobre sus movimientos de glosa principales, distribuyendo el valor del pago pendiente hasta saldar la diferencia, actualizando los campos de valor pagado, saldo pendiente de conciliación y estado del movimiento (pagado parcial o totalmente). Finalmente, actualiza el valor total pagado en la cartera glosada (GlosaPortfolioGlosada) para mantener la consistencia financiera entre los movimientos individuales y el resumen de cartera por factura. Existe como mecanismo de saneamiento o re-proceso contable para corregir desajustes entre el módulo de glosas y cartera, garantizando la integridad de los valores de glosa frente al asegurador (EPS/EAPB).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'CorregirGlosasMovimientos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'CorregirGlosasMovimientos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Distribuye la diferencia de saldo de glosa frente a cartera entre los movimientos de glosa principales de cada factura, actualizando estados y pagos, y sincroniza el valor pagado en la cartera glosada.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirGlosasMovimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en FacturaSaldoGlosaMayorCartera con Procesada NULL; Las facturas de esa tabla deben tener movimientos en GlosaMovementGlosa con ValueAcceptedEAPBconciliation NULL y ValuePendingConciliation > 0; Las tablas GlosaMovementGlosa y GlosaPortfolioGlosada deben estar relacionadas por InvoiceNumber', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirGlosasMovimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan facturas con ValueAcceptedEAPBconciliation NULL, Procesada NULL y ValuePendingConciliation > 0; Solo los movimientos marcados como MainGlosa = 1 reciben aplicación de pago; El valor aplicado a un movimiento nunca excede su ValuePendingConciliation; ValuePendingConciliation nunca queda negativo: el aplicado se topa al saldo disponible; Cuando se salda completamente un movimiento (saldo en 0) queda con State = 7; en caso contrario State = 2; El State previo se preserva en TempState antes de actualizarlo; El ValuePayments de la cartera glosada se sincroniza como suma de pagos de movimientos MainGlosa=1 de la factura; Toda la operación es atómica vía transacción; ante error se hace rollback completo', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirGlosasMovimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'glosa; factura; conciliación; movimiento de glosa; cartera glosada; saldo pendiente de conciliación; pago de glosa; glosa principal', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirGlosasMovimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.FacturaSaldoGlosaMayorCartera: Para cada factura seleccionada (Procesada NULL y con movimientos pendientes), se marca Procesada = 5 indicando que ya fue corregida; [UPDATE] glosas.GlosaMovementGlosa: Cuando MainGlosa=1 y ValuePendingConciliation>0, se incrementa ValuePayments con el valor aplicado, se decrementa ValuePendingConciliation, se guarda State previo en TempState y se asigna State=7 si queda saldado o State=2 si queda parcial; [UPDATE] glosas.GlosaPortfolioGlosada: Tras procesar la factura, ValuePayments se actualiza con la suma de ValuePayments de los movimientos MainGlosa=1 de esa misma factura; [RETURN_RESULT] (resultset de error): En caso de error en el TRY, se hace rollback y se devuelve un resultset con ERROR_NUMBER y ERROR_MESSAGE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirGlosasMovimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MainGlosa = 1 AND ValuePendingConciliation > 0 → Aplica abono al movimiento: descuenta del saldo pendiente, actualiza ValuePayments, TempState y State else No procesa el movimiento (no es glosa principal o no tiene saldo pendiente); si Tras restar ValuePendingConciliation, @ValuePayments <= 0 → El valor aplicado al movimiento es el remanente del pago (@tmpvaluepayment) else Si @ValuePayments > 0, el valor aplicado es el ValuePendingConciliation completo del movimiento; si ValuePendingConciliation queda en 0 tras la aplicación → State = 7 (saldado) else State = 2 (parcial); si @ValuePayments <= 0 después de aplicar a un movimiento → Sale del cursor (BREAK), no aplica a más movimientos de la factura', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirGlosasMovimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.FacturaSaldoGlosaMayorCartera; Glosas.GlosaMovementGlosa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirGlosasMovimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'CorregirGlosasMovimientos';
-- GO
