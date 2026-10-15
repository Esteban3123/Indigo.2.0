-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-10-08
-- Description:	Procedimiento para el reporte de conciliacion
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportReconcileModule]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Module INT

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@Module = t.x.value('Module[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @Module = 1
		BEGIN
			EXEC [GeneralLedger].[SP_ReportReconcileTreasury] @xmlCriterias
		END
		ELSE IF @Module = 2
		BEGIN
			EXEC [GeneralLedger].[SP_ReportReconcilePortfolio] @xmlCriterias
		END
		ELSE IF @Module = 3
		BEGIN
			EXEC [GeneralLedger].[SP_ReportReconcilePayments] @xmlCriterias
		END
		ELSE IF @Module = 4
		BEGIN
			EXEC [GeneralLedger].[SP_ReportReconcileInventory] @xmlCriterias
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END