-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-27
-- Description:	Procedimiento para el reporte de estimacion de costos por tipo
-- =============================================
CREATE PROCEDURE [Cost].[SP_ReportEstimateCosts]
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
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @TypeReport = 1
		BEGIN
			EXEC [Cost].[SP_ReportEstimateCostsPrimarySummary] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 2
		BEGIN
			EXEC [Cost].[SP_ReportEstimateCostsPrimaryDetailed] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 3
		BEGIN
			EXEC [Cost].[SP_ReportEstimateCostsSecondarySummary] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 4
		BEGIN
			EXEC [Cost].[SP_ReportEstimateCostsSecondaryDetailed] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 5
		BEGIN
			EXEC [Cost].[SP_ReportEstimateCostsFinalSummary] @xmlCriterias, @xmlFilters
		END
		ELSE IF @TypeReport = 6
		BEGIN
			EXEC [Cost].[SP_ReportEstimateCostsFinalDetailed] @xmlCriterias, @xmlFilters
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento orquestador para el reporte de estimación de costos hospitalarios. Recibe criterios y filtros en formato XML, determina el tipo de reporte solicitado (1 al 6) y delega la ejecución al procedimiento especializado correspondiente: resumen primario, detalle primario, resumen secundario, detalle secundario, resumen final o detalle final de costos. Centraliza el punto de entrada para todos los niveles y modalidades del análisis de estimación de costos, permitiendo generar tanto vistas consolidadas como desgloses detallados según la etapa del proceso de costeo (primaria, secundaria o final).', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCosts';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_ReportEstimateCosts';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Despachador que ejecuta el procedimiento de reporte de estimación de costos correspondiente según el tipo de reporte indicado en los criterios XML.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener el nodo /Data/TypeReport con un entero válido entre 1 y 6 para que se ejecute alguna rama.; Los procedimientos hijos de Cost deben existir y aceptar los XML de criterios y filtros.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se ejecuta uno de los seis procedimientos hijos por invocación (ramas mutuamente excluyentes).; Cualquier error en tiempo de ejecución se captura y se transforma en un result set estándar con Code=''999'', sin propagar la excepción.; Los reportes se clasifican en tres niveles (Primary, Secondary, Final) y dos formatos (Summary, Detailed).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estimación de costos; Reporte resumen; Reporte detallado; Costos primarios; Costos secundarios; Costos finales', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Cuando ocurre una excepción en TRY, retorna un result set con Code=''999'' y Message con ERROR_MESSAGE() y la línea del error.; [RETURN_RESULT] ResultSet: Delega y retorna el result set del SP hijo invocado según el valor de TypeReport (1..6).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TypeReport = 1 → Ejecuta el reporte resumen primario (SP_ReportEstimateCostsPrimarySummary).; si TypeReport = 2 → Ejecuta el reporte detallado primario (SP_ReportEstimateCostsPrimaryDetailed).; si TypeReport = 3 → Ejecuta el reporte resumen secundario (SP_ReportEstimateCostsSecondarySummary).; si TypeReport = 4 → Ejecuta el reporte detallado secundario (SP_ReportEstimateCostsSecondaryDetailed).; si TypeReport = 5 → Ejecuta el reporte resumen final (SP_ReportEstimateCostsFinalSummary).; si TypeReport = 6 → Ejecuta el reporte detallado final (SP_ReportEstimateCostsFinalDetailed).; si TypeReport no está entre 1 y 6 → No se ejecuta ningún SP hijo y no se retorna result set (salvo error).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_ReportEstimateCostsPrimarySummary; Cost.SP_ReportEstimateCostsPrimaryDetailed; Cost.SP_ReportEstimateCostsSecondarySummary; Cost.SP_ReportEstimateCostsSecondaryDetailed; Cost.SP_ReportEstimateCostsFinalSummary; Cost.SP_ReportEstimateCostsFinalDetailed', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCosts';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_ReportEstimateCosts';
-- GO
