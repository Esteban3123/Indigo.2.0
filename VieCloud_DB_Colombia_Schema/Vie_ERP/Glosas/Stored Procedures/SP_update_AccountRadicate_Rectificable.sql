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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de actualización masiva de cuentas contables (cuentas 1302-1303) asociadas a facturas glosadas en cartera. Cruza la cartera de glosas activas (estados 2, 3, 8 y 11, correspondientes a facturas en proceso de glosa o conciliación) con la configuración de métodos privados de liquidación para identificar qué facturas radicadas tienen una glosa rectificable configurada, y luego reemplaza el identificador de cuenta contable del asegurador en la cartera glosada por el código de cuenta contable correcto obtenido desde las tablas maestras de contratos y cuentas. El procedimiento fue diseñado como una corrección puntual de datos contables en el módulo de Glosas; actualmente finaliza con un ROLLBACK, por lo que no persiste cambios en producción hasta que se habilite el COMMIT.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_update_AccountRadicate_Rectificable';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_update_AccountRadicate_Rectificable';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reemplaza el código de cuenta contable (AccountantAccountCustomers) en cartera de glosas rectificables, mapeándolo desde la configuración de glosa al código de cuenta CUECODIGO según el concepto contable asociado.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_AccountRadicate_Rectificable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en GlosaPortfolioGlosada con state IN (2,3,8,11); El AccountantAccountCustomers de la cartera debe coincidir con un invoiceradicate en AccountSettingsFOX_PrivateMethod; El rectifiableGlosa debe corresponder a un CONCODIGO existente en crNConNOT; El concepto debe tener una cuenta asociada en CTNCUENTA vía OID = CONCODIGO.CTNCUENTA', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_AccountRadicate_Rectificable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El procedimiento nunca persiste cambios: siempre finaliza con ROLLBACK (el COMMIT está comentado); Solo procesa cuentas en estados 2, 3, 8 u 11; Solo procesa radicados que tengan configuración en AccountSettingsFOX_PrivateMethod; El nuevo código de cuenta proviene siempre de la cadena rectifiableGlosa → crNConNOT → CTNCUENTA', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_AccountRadicate_Rectificable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa rectificable; Cartera glosada; Cuenta contable; Radicado de factura; Concepto contable', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_AccountRadicate_Rectificable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Glosas.GlosaPortfolioGlosada: Para cada cartera con state IN (2,3,8,11) cuyo radicado esté configurado en AccountSettingsFOX_PrivateMethod, se actualiza AccountantAccountCustomers al CUECODIGO derivado del concepto rectifiableGlosa; sin embargo la transacción termina en ROLLBACK, por lo que no persiste.; [RETURN_RESULT] resultset: En caso de error en el TRY, retorna ERROR_NUMBER y ERROR_MESSAGE tras hacer rollback.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_AccountRadicate_Rectificable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Cartera con state IN (2,3,8,11) y radicado presente en AccountSettingsFOX_PrivateMethod → Se incluye en el conjunto a actualizar con el nuevo CUECODIGO else Queda fuera del proceso de actualización; si Ocurre excepción durante el proceso → Se hace ROLLBACK y se retorna código y mensaje de error else Igualmente se ejecuta ROLLBACK al final del TRY (commit deshabilitado)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_AccountRadicate_Rectificable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaPortfolioGlosada; Glosas.AccountSettingsFOX_PrivateMethod; crNConNOT; CTNCUENTA', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_AccountRadicate_Rectificable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_AccountRadicate_Rectificable';
-- GO
