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