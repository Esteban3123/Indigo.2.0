-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-17
-- Description:	Procedimiento para el reporte de balance general comparativo
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportGeneralBalance]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ReportType TINYINT

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT	@ReportType = t.x.value('ReportType[1]','tinyint')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @ReportType = 1 OR @ReportType = 2
		BEGIN
			EXEC [GeneralLedger].[SP_ReportGeneralBalanceMonthly] @xmlCriterias
		END
		ELSE IF @ReportType = 3
		BEGIN
			EXEC [GeneralLedger].[SP_ReportGeneralBalanceComparative] @xmlCriterias
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento principal para generar el reporte de Balance General contable. Recibe criterios de filtro en formato XML, incluido el tipo de reporte solicitado, y según ese tipo enruta la ejecución: si el tipo es 1 o 2 genera un balance general mensual (invocando SP_ReportGeneralBalanceMonthly), y si el tipo es 3 genera un balance general comparativo entre períodos (invocando SP_ReportGeneralBalanceComparative). Actúa como punto de entrada unificado para los distintos formatos del reporte de balance general en el módulo de contabilidad general (General Ledger).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportGeneralBalance';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportGeneralBalance';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Despacha la generación del reporte de balance general hacia el procedimiento mensual o comparativo según el tipo de reporte indicado en el XML de criterios.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener el nodo /Data/ReportType con un valor numérico (tinyint).; ReportType debe ser 1, 2 o 3 para que se ejecute alguna lógica de reporte.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se invoca un procedimiento de reporte por ejecución.; Los errores nunca se propagan: siempre se capturan y devuelven como resultset con Code=''999''.; Si ReportType no es 1, 2 ni 3, no se ejecuta ningún reporte ni se retorna error.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Balance general; Reporte contable comparativo; Reporte contable mensual', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando ocurre una excepción en el TRY, se retorna un resultset con Code=''999'' y Message con el error y la línea.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ReportType = 1 OR ReportType = 2 → Ejecuta SP_ReportGeneralBalanceMonthly pasando el XML de criterios. else Si ReportType = 3 ejecuta SP_ReportGeneralBalanceComparative; cualquier otro valor no produce reporte.; si ReportType = 3 → Ejecuta SP_ReportGeneralBalanceComparative pasando el XML de criterios.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_ReportGeneralBalanceMonthly; GeneralLedger.SP_ReportGeneralBalanceComparative', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportGeneralBalance';
-- GO
