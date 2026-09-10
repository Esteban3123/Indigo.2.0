
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-06-06
-- Description:	Procedimiento para el reporte de edades de cartera FIERRROOOOO
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ReportPortfolioByAge]
	@HisContainer AS VARCHAR(20),
	@xmlCriterias AS XML,
	@xmlFilters AS XML
	WITH RECOMPILE
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
	
	DECLARE @OfficialCurrencyId integer = (
										   SELECT OfficialCurrencyId 
										   FROM GeneralLedger.LegalBook
										   WHERE OfficialBook = 1
										   GROUP by OfficialCurrencyId										   
										  ) 

										  

	IF @OfficialCurrencyId > 1
	BEGIN
		EXEC [Portfolio].[SP_ReportPortfolioByAge_International] @HisContainer,
																 @xmlCriterias,
																 @xmlFilters
																 
	END
	ELSE
	BEGIN
		EXEC [Portfolio].[SP_ReportPortfolioByAge_Native] @HisContainer,
														  @xmlCriterias,
														  @xmlFilters
	END

	END TRY
	BEGIN CATCH
	 SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de antigüedad (edades) de cartera, es decir, la clasificación de las cuentas por cobrar según el tiempo transcurrido desde su generación. Determina si la empresa opera con moneda oficial o moneda internacional consultando el libro legal contable oficial, y según eso delega la ejecución al procedimiento nativo (moneda local) o al procedimiento internacional (moneda extranjera). Recibe como parámetros el contenedor de historia clínica o empresa, criterios de agrupación y filtros de búsqueda en formato XML, permitiendo personalizar el alcance del reporte de cartera por edades.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReportPortfolioByAge';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReportPortfolioByAge';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Despacha la generación del reporte de edades de cartera hacia la versión internacional o nativa según la moneda oficial configurada en el libro contable oficial.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en GeneralLedger.LegalBook con OfficialBook = 1 que defina el OfficialCurrencyId.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La moneda oficial se determina exclusivamente desde el libro marcado como OfficialBook = 1 en GeneralLedger.LegalBook.; Solo se invoca una de las dos variantes del reporte (internacional o nativa), nunca ambas.; Cualquier error es capturado y devuelto como resultset estandarizado con código ''999'' en lugar de propagarse.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cartera; edades de cartera; moneda oficial; libro oficial contable', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Si ocurre una excepción, retorna un resultset con Code=''999'', Message=ERROR_MESSAGE() y Line=ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si OfficialCurrencyId > 1 (moneda oficial distinta de la base/local) → Ejecuta Portfolio.SP_ReportPortfolioByAge_International con los mismos parámetros. else Ejecuta Portfolio.SP_ReportPortfolioByAge_Native con los mismos parámetros.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_ReportPortfolioByAge_International; Portfolio.SP_ReportPortfolioByAge_Native', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReportPortfolioByAge';
-- GO
