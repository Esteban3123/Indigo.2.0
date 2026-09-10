-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetInfoTest]
	@file as xml
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
		declare @field1 varchar(100), @field2 varchar(100)
		declare @testing table (Id int , ServiceName varchar(150), invoicedValue money)
		declare @tableAVG table(Id int, Percentage decimal(5,2))
		declare @aux nvarchar(max) =  'select top 100 Id, ServiceName, invoicedValue from Glosas.GlosaInvoiceDetail'
		insert into @testing
		exec sp_executesql @aux
		insert into @tableAVG
		select Id, (invoicedValue * 100 / (select SUM(invoicedValue) from @testing))
		from @testing

		--select * from @testing

		select @field1 = t.x.value('field1[1]','varchar(100)') from @file.nodes('/test/file') t(x)

		select @field1

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de prueba que lee los primeros 100 registros de detalle de glosas facturadas (GlosaInvoiceDetail), calcula el porcentaje que representa el valor facturado de cada servicio sobre el total facturado, y adicionalmente extrae un campo desde un parámetro XML de entrada. Toca la entidad de glosas y valores facturados por servicio. Es un procedimiento experimental o de desarrollo, sin uso productivo definido, que combina consulta dinámica de glosas con procesamiento de XML.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'GetInfoTest';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'GetInfoTest';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Prototipo/prueba que extrae los primeros 100 detalles de glosas de factura, calcula el porcentaje de participación de cada ítem sobre el total facturado y devuelve un valor obtenido desde un XML de entrada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetInfoTest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un XML con estructura /test/file/field1 para poder extraer el valor solicitado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetInfoTest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Limita el universo de análisis a los primeros 100 registros del detalle de glosas de factura.; El porcentaje calculado por ítem se hace sobre la suma del valor facturado de los 100 registros tomados, no sobre el total real.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetInfoTest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Detalle de factura; Servicio facturado; Valor facturado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetInfoTest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar): Devuelve como resultado el valor de field1 extraído del nodo /test/file del XML recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetInfoTest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetInfoTest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaInvoiceDetail', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetInfoTest';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetInfoTest';
-- GO
