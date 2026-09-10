-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-07-15
-- Description:	Procedimiento para el reporte de autorizaciones (solicitudes)
-- =============================================
CREATE PROCEDURE [Authorization].[SP_ReportRequests]
	@xmlCriterias AS XML
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
			EXEC [Authorization].[SP_ReportRequestSummary] @xmlCriterias
		END
		ELSE IF @TypeReport = 2
		BEGIN
			EXEC [Authorization].[SP_ReportRequestDetailed] @xmlCriterias
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, CONCAT('SP_ReportRequests: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()) AS Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento principal para generar el reporte de autorizaciones (solicitudes de servicios de salud). Recibe criterios de búsqueda en formato XML, entre los cuales se incluye el tipo de reporte solicitado. Según el tipo seleccionado, enruta la ejecución hacia un reporte resumido (SP_ReportRequestSummary, tipo 1) o un reporte detallado (SP_ReportRequestDetailed, tipo 2) de las autorizaciones. Sirve como punto de entrada unificado para la consulta y reportería del módulo de autorizaciones de servicios médicos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_ReportRequests';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_ReportRequests';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Despachador de reporte de solicitudes de autorización que delega la generación a un sub-procedimiento (resumen o detallado) según el tipo indicado en el XML de criterios.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener un nodo /Data/TypeReport con valor entero; Deben existir y ser ejecutables los SP destino SP_ReportRequestSummary y SP_ReportRequestDetailed, los cuales deben aceptar el mismo XML de criterios', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El tipo de reporte se determina exclusivamente por el nodo TypeReport del XML de criterios; Solo se soportan dos modalidades de reporte: resumen (1) y detallado (2); cualquier otro valor no produce salida de datos; Los errores se capturan y devuelven como resultset con Code=''999'' y un Message descriptivo que incluye nombre del SP y línea', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorizaciones; Solicitudes de autorización; Reporte resumen; Reporte detallado', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset de error): En el bloque CATCH se devuelve un resultset con Code=''999'' y Message conteniendo ''SP_ReportRequests: '' + ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE(); [RETURN_RESULT] Authorization.SP_ReportRequestSummary: Cuando TypeReport=1 se ejecuta SP_ReportRequestSummary que retorna el resultset del reporte resumen; [RETURN_RESULT] Authorization.SP_ReportRequestDetailed: Cuando TypeReport=2 se ejecuta SP_ReportRequestDetailed que retorna el resultset del reporte detallado', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TypeReport = 1 (extraído del XML de criterios) → Ejecuta SP_ReportRequestSummary con los criterios XML else Si TypeReport = 2, ejecuta SP_ReportRequestDetailed con los criterios XML', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Authorization.SP_ReportRequestSummary; Authorization.SP_ReportRequestDetailed', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequests';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequests';
-- GO
