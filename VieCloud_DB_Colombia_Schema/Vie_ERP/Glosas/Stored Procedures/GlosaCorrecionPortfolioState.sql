

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de corrección masiva del estado de facturas glosadas en cartera. Recorre todos los registros temporales de conciliación de glosas (ConciliationTemp) para identificar los números de factura involucrados y actualiza su estado en la cartera de glosas (GlosaPortfolioGlosada) al valor 6, guardando previamente el estado anterior como estado temporal (TempState). Se usa para sincronizar o corregir el estado de las facturas glosadas tras un proceso de conciliación, asegurando que la cartera refleje el resultado correcto del cruce de glosas entre la IPS y el asegurador (EPS/EAPB).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'GlosaCorrecionPortfolioState';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'GlosaCorrecionPortfolioState';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Marca masivamente las glosas de portafolio cuyas facturas figuran en la tabla temporal de conciliación, llevándolas a estado 6 (corrección) y guardando el estado previo en TempState.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecionPortfolioState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla temporal de conciliación debe estar poblada con los números de factura a procesar; Los Id de la tabla temporal deben ser secuenciales desde 1 hasta el total de filas para que el recorrido los alcance', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecionPortfolioState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Antes de cambiar el estado, se preserva el estado anterior en TempState para permitir trazabilidad/reversión; El nuevo estado asignado siempre es 6 (estado fijo de corrección); La operación es transaccional: ante cualquier error se revierte por completo; Solo se afectan registros cuya factura aparece en la tabla temporal de conciliación', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecionPortfolioState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Portafolio de glosas; Conciliación; Factura', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecionPortfolioState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Glosas.GlosaPortfolioGlosada: Por cada factura listada en la tabla temporal de conciliación, se actualiza la glosa de portafolio coincidente fijando state=6 y respaldando el estado anterior en TempState; [RETURN_RESULT] ?: Si ocurre un error, se hace rollback y se devuelve un resultset con ERROR_NUMBER y ERROR_MESSAGE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecionPortfolioState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.ConciliationTemp', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecionPortfolioState';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'GlosaCorrecionPortfolioState';
-- GO
