-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-24
-- Description:	Procedimiento para el reporte del comparativo de costos por tipo
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportComparativeCosts]
	@xmlCriterias AS XML,
	@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TypeReport INT

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@TypeReport = t.x.value('TypeReport[1]','int')
		FROM @xmlFilters.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @TypeReport = 1
		BEGIN
			EXEC [Cost].[SP_ReportComparativeManPowerDistribution] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 2
		BEGIN
			EXEC [Cost].[SP_ReportComparativeDispensingDistribution] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 3
		BEGIN
			EXEC [Cost].[SP_ReportComparativeTransferDistribution] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 4
		BEGIN
			EXEC [Cost].[SP_ReportComparativeCostDistribution] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 5
		BEGIN
			EXEC [Cost].[SP_ReportComparativeFixedAssetDistribution] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 6
		BEGIN
			EXEC [Cost].[SP_ReportComparativeSales] @xmlCriterias, @xmlFilters
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END