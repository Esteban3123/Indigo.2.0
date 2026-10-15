-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-24
-- Description:	Procedimiento para el reporte del comparativo de costos por tipo
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportExogenousFormat]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Format VARCHAR(20)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@Format = t.x.value('Format[1]','varchar(20)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @Format = '1001'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat1001] @xmlCriterias
		END
		ELSE IF @Format = '1003'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat1003] @xmlCriterias
		END
		ELSE IF @Format = '1004'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat1004] @xmlCriterias
		END
		ELSE IF @Format = '1005'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat1005] @xmlCriterias
		END
		ELSE IF @Format = '1006'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat1006] @xmlCriterias
		END
		ELSE IF @Format = '1007'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat1007] @xmlCriterias
		END
		ELSE IF @Format = '1008'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat1008] @xmlCriterias
		END
		ELSE IF @Format = '1009'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat1009] @xmlCriterias
		END
		ELSE IF @Format = '1010'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat1010] @xmlCriterias
		END
		ELSE IF @Format = '1011'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat1011] @xmlCriterias
		END
		ELSE IF @Format = '1012'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat1012] @xmlCriterias
		END
		ELSE IF @Format = '1056'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat1056] @xmlCriterias
		END
		ELSE IF @Format = '1647'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat1647] @xmlCriterias
		END
		ELSE IF @Format = '2275'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat2275] @xmlCriterias
		END
		ELSE IF @Format = '2276'
		BEGIN
			EXEC [GeneralLedger].[SP_ExogenaFormat2276] @xmlCriterias
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END