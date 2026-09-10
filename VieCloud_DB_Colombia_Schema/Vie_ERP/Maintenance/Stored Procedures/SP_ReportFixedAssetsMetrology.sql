-- =============================================
-- Author:		Giovanny Plazas Lozano
-- Create date: 2021-02-11
-- Description:	Procedimiento para el INFORME CERTIFICADOS DE CALIBRACION EQUIPOS BIOMEDICOS
-- =============================================
CREATE PROCEDURE [Maintenance].[SP_ReportFixedAssetsMetrology]
		@xmlCriterias AS XML,
		@xmlFilters AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ReportType INT

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@ReportType = t.x.value('ReportType[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @ReportType = 1
		BEGIN
			EXEC [Maintenance].[SP_ReportFixedAssetsMetrologySummary] @xmlCriterias, @xmlFilters
		END
		ELSE IF @ReportType = 2
		BEGIN
			EXEC [Maintenance].[SP_ReportFixedAssetsMetrologyDetail] @xmlCriterias, @xmlFilters
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento principal para generar el informe de certificados de calibración de equipos biomédicos (metrología). Según el tipo de reporte solicitado, enruta la ejecución hacia un reporte resumido (SP_ReportFixedAssetsMetrologySummary) o un reporte detallado (SP_ReportFixedAssetsMetrologyDetail), ambos filtrados por criterios enviados como XML. Sirve como punto de entrada único para la reportería de metrología y mantenimiento de activos fijos biomédicos, cubriendo el control de calibración y certificación de equipos en las instalaciones.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFixedAssetsMetrology';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFixedAssetsMetrology';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Despachador del informe de certificados de calibración de equipos biomédicos que enruta a la versión resumen o detalle según el tipo de reporte indicado.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrology';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener el nodo /Data/ReportType con un valor entero (1 o 2) para que se invoque alguno de los procedimientos hijo.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrology';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se ejecuta uno de los dos procedimientos hijo, nunca ambos.; Los errores no se propagan: se capturan y se transforman en un result set con Code=''999''.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrology';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Certificados de calibración; Equipos biomédicos; Activos fijos; Metrología', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrology';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Cuando ocurre una excepción en el TRY, devuelve un result set con Code=''999'' y Message=ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrology';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ReportType = 1 → Ejecuta Maintenance.SP_ReportFixedAssetsMetrologySummary con los XML de criterios y filtros. else Si @ReportType = 2, ejecuta Maintenance.SP_ReportFixedAssetsMetrologyDetail; cualquier otro valor no produce resultado.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrology';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Maintenance.SP_ReportFixedAssetsMetrologySummary; Maintenance.SP_ReportFixedAssetsMetrologyDetail', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrology';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetsMetrology';
-- GO
