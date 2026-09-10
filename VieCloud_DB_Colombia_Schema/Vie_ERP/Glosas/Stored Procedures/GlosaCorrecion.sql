

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de corrección masiva del proceso de conciliación de glosas. Revierte o resetea los valores de conciliación (montos aceptados por IPS y EAPB, justificación y fecha de conciliación) en los movimientos de glosa correspondientes a facturas que están en estado de conciliación activa, devolviendo el valor pendiente de conciliación a su estado anterior. Recorre todos los ítems de detalle de conciliación (ConciliationD) en estado activo y, para cada factura asociada, restaura el campo de valor pendiente y limpia los campos de valores aceptados y justificaciones en la tabla de movimientos de glosa (GlosaMovementGlosa), con el fin de corregir o reabrir conciliaciones de glosa que fueron registradas de forma incorrecta o requieren ajuste.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'GlosaCorrecion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'GlosaCorrecion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reabre/corrige movimientos de glosa de facturas conciliadas: recalcula el valor pendiente como la suma de lo aceptado por IPS y EAPB, marca el movimiento en estado 2 guardando el estado previo y limpia los datos de conciliación.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en Glosas.ConciliationD con state = 1 cuya InvoiceNumber no esté en la lista excluida.; Las facturas referenciadas deben existir en Glosas.GlosaMovementGlosa para que los UPDATE tengan efecto.; La columna TempState debe existir en GlosaMovementGlosa para preservar el estado previo.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las facturas excluidas explícitamente (''HSP 0001351542'',''HSP 0001305525'',''HSP 0001305425'') nunca son afectadas por la corrección.; Solo se procesan conciliaciones con state = 1.; Tras la corrección, el estado del movimiento de glosa queda en 2 y el estado anterior se preserva en TempState.; Después de la corrección, los campos de conciliación (IPS, EAPB, racional y fecha) quedan siempre en NULL para las facturas procesadas.; Toda la operación es atómica: se ejecuta dentro de una transacción que hace rollback ante cualquier error.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Conciliación de glosa; Factura; Movimiento de glosa; Valor aceptado IPS; Valor aceptado EAPB; Valor pendiente de conciliación', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Glosas.GlosaMovementGlosa: Para cada factura de ConciliationD con state=1 y no excluida: ValuePendingConciliation = ISNULL(ValueAcceptedIPSconciliation + ValueAcceptedEAPBconciliation, ValuePendingConciliation), State = 2 y TempState = State (preserva estado anterior).; [UPDATE] Glosas.GlosaMovementGlosa: Para las mismas facturas procesadas: ValueAcceptedIPSconciliation, ValueAcceptedEAPBconciliation, rationaleconciliation y rationaleDateConciliation se establecen a NULL (reseteo de datos de conciliación).; [RETURN_RESULT] resultset: En caso de error en la transacción se hace rollback y se devuelve un resultset con ERROR_NUMBER() y ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ConciliationD.state = 1 y InvoiceNumber no está en la lista excluida (''HSP 0001351542'',''HSP 0001305525'',''HSP 0001305425'') → Se incluye la factura en el ciclo de corrección y se actualiza GlosaMovementGlosa else Se omite la factura (no se procesa); si ValueAcceptedIPSconciliation + ValueAcceptedEAPBconciliation IS NULL → ValuePendingConciliation conserva su valor actual (ISNULL devuelve el valor previo) else ValuePendingConciliation toma la suma de los valores aceptados por IPS y EAPB', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.ConciliationD; Glosas.GlosaMovementGlosa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecion';
-- GO
