-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-24
-- Description:	Procedimiento para el reporte de la circular unica
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportSingleCircular]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Archive INT

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		--Se obtienen los datos de los criterios
		SELECT 
			@Archive = t.x.value('Archive[1]','int')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		IF @Archive = 1
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT001] @xmlCriterias
		END
			ELSE IF @Archive = 3
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT003] @xmlCriterias
		END
		ELSE IF @Archive = 4
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT004] @xmlCriterias
		END
		ELSE IF @Archive = 6
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT006] @xmlCriterias
		END
		ELSE IF @Archive = 7
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT007] @xmlCriterias
		END
		ELSE IF @Archive = 8
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT008] @xmlCriterias
		END
		ELSE IF @Archive = 9
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT009] @xmlCriterias
		END
		ELSE IF @Archive = 10
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT010] @xmlCriterias
		END
		ELSE IF @Archive = 25
		BEGIN
			EXEC [GeneralLedger].[SP_ReportSingleCircularFT025] @xmlCriterias
		END

	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) AS Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento orquestador para la generación del reporte de Circular Única Contable (Superintendencia de Salud u organismo regulador equivalente). Recibe criterios de filtro en formato XML, extrae el número de archivo o formulario requerido (FT001, FT003, FT004, FT006, FT007, FT008, FT009, FT010, FT025) y delega la ejecución al subprocedimiento específico correspondiente a ese formulario dentro del módulo de Libro Mayor (GeneralLedger). Se utiliza como punto de entrada único para obtener cualquiera de los formularios de la Circular Única contable-financiera, centralizando la lógica de enrutamiento según el tipo de archivo solicitado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircular';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircular';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Despacha la generación del reporte de Circular Única hacia el procedimiento específico según el tipo de archivo (FT) solicitado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircular';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener el nodo /Data/Archive con un entero válido; El valor de Archive debe corresponder a uno de los archivos soportados (1, 3, 4, 6, 7, 8, 9, 10, 25)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircular';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se invoca un único SP de archivo FT por ejecución; Si Archive no coincide con ningún valor soportado (1,3,4,6,7,8,9,10,25), no se ejecuta ningún SP hijo y no se retorna información; Cualquier error es capturado y transformado a un resultado con código ''999'' en lugar de propagarse', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircular';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Circular Única; Archivo tipo FT (FT001, FT003, FT004, FT006, FT007, FT008, FT009, FT010, FT025); Reporte contable / General Ledger', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircular';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: En CATCH devuelve un result set con Code=''999'' y mensaje de error con número de línea; [RETURN_RESULT] ResultSet: Retorna el result set producido por el SP FTxxx correspondiente al valor de Archive', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircular';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Archive = 1 → Ejecuta SP_ReportSingleCircularFT001; si Archive = 3 → Ejecuta SP_ReportSingleCircularFT003; si Archive = 4 → Ejecuta SP_ReportSingleCircularFT004; si Archive = 6 → Ejecuta SP_ReportSingleCircularFT006; si Archive = 7 → Ejecuta SP_ReportSingleCircularFT007; si Archive = 8 → Ejecuta SP_ReportSingleCircularFT008; si Archive = 9 → Ejecuta SP_ReportSingleCircularFT009; si Archive = 10 → Ejecuta SP_ReportSingleCircularFT010; si Archive = 25 → Ejecuta SP_ReportSingleCircularFT025', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircular';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_ReportSingleCircularFT001; GeneralLedger.SP_ReportSingleCircularFT003; GeneralLedger.SP_ReportSingleCircularFT004; GeneralLedger.SP_ReportSingleCircularFT006; GeneralLedger.SP_ReportSingleCircularFT007; GeneralLedger.SP_ReportSingleCircularFT008; GeneralLedger.SP_ReportSingleCircularFT009; GeneralLedger.SP_ReportSingleCircularFT010; GeneralLedger.SP_ReportSingleCircularFT025', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircular';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircular';
-- GO
