-- =============================================
-- Author:		Rafael Patiño
-- Create date: 10/04/2013
-- Description:	Sp que retorna las facturas por contenedor
-- =============================================
CREATE PROCEDURE [Glosas].[SP_invoiceListCount]
	@container varchar(150),
	@nit varchar(15),
	@InvoiceNumber varchar(50),
	@IndigoCompany varchar(3),
	@StringSQl varchar(1000)
AS
BEGIN

	SET NOCOUNT ON;
	

	Declare @tablaFactura table(
		countRegister int
	)

	INSERT INTO @tablaFactura
	select 250 
--	execute sp_executesql @sql

	

	--Select * from @tablaFactura
	/*

	IF @InvoiceNumber <> '' BEGIN
		set @sql = @sql + ' and car.cemnumfac  LIKE ''%' + @InvoiceNumber + '%'''
	END

	IF @StringSQl <> '' BEGIN
		set @sql = @sql + ' AND ' +  @StringSQl
	END

	
	--print @sql
	INSERT INTO @tablaFactura
	execute sp_executesql @sql,N'@nit varchar(15)',@nit*/

	Select * from @tablaFactura

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento destinado a contar el número de facturas asociadas a un contenedor de glosas, filtrando por NIT del pagador, número de factura y empresa Indigo. Actualmente retorna un valor fijo de 250 registros (lógica de conteo real comentada), por lo que se trata de un procedimiento en estado de desarrollo o mantenimiento. Su propósito de negocio es apoyar la paginación o el resumen del listado de facturas dentro del módulo de glosas, indicando cuántas facturas existen para los criterios de búsqueda dados.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceListCount';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_invoiceListCount';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve un conteo fijo de registros (valor 250) como resultado de un listado de facturas por contenedor, con la lógica real comentada/inactiva.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceListCount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre es 250, independiente de los parámetros recibidos.; La lógica original de filtrado dinámico por número de factura y SQL adicional está comentada y no se ejecuta.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceListCount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'factura; contenedor; glosa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceListCount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Siempre retorna un único registro con valor 250 (INSERT INTO @tablaFactura SELECT 250; SELECT * FROM @tablaFactura).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceListCount';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_invoiceListCount';
-- GO
