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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que corrige y sincroniza la fecha de ingreso hospitalario en la cartera de glosas. Toma los números de ingreso registrados en la tabla de facturas glosadas (GlosaPortfolioGlosada) y los cruza con la tabla de admisiones (Adingres) para actualizar la fecha real de ingreso del paciente en cada registro de glosa. Existe para garantizar que el seguimiento financiero de glosas refleje la fecha de ingreso correcta proveniente del sistema clínico-administrativo, corrigiendo inconsistencias entre el módulo de glosas y el módulo de admisiones.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_update_IngressNUmber';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_update_IngressNUmber';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Sincroniza la fecha de ingreso hospitalario en la cartera de facturas glosadas, tomándola desde el maestro de ingresos para los registros cuyo número de ingreso coincide.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_IngressNUmber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en GlosaPortfolioGlosada con ingressnumber poblado; La tabla Adingres contiene los ingresos referenciados por AINCONSEC', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_IngressNUmber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la actualización ocurre dentro de una transacción; ante error se revierte por completo; Solo se actualizan filas cuyo ingressnumber existe también en Adingres; No se modifican columnas distintas a IngressDate', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_IngressNUmber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Cartera glosada; Ingreso hospitalario; Fecha de ingreso', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_IngressNUmber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Glosas.GlosaPortfolioGlosada: Para cada ingreso encontrado en Adingres cuyo AINCONSEC coincide con ingressnumber de la cartera glosada, se actualiza IngressDate con AINFECING; [RETURN_RESULT] N/A: En caso de error se hace rollback y se retorna ERROR_NUMBER y ERROR_MESSAGE', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_IngressNUmber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @contador < @count (mientras queden registros en la tabla temporal) → Itera y actualiza IngressDate fila por fila en GlosaPortfolioGlosada', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_IngressNUmber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Adingres; Glosas.GlosaPortfolioGlosada', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_IngressNUmber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_update_IngressNUmber';
-- GO
