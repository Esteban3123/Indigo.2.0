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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento orquestador para la generación de reportes de información exógena tributaria (medios magnéticos DIAN). Recibe como parámetro un XML con criterios de consulta, extrae el número de formato solicitado y redirige la ejecución al procedimiento específico correspondiente (formatos 1001, 1003 al 1012, 1056, 1647, 2275 y 2276), cada uno encargado de consolidar la información contable requerida por ese formato en particular. Se utiliza para cumplir con la obligación de reporte de información exógena ante la DIAN, centralizando en un único punto de entrada la lógica de despacho hacia todos los formatos tributarios disponibles en el módulo de contabilidad general.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportExogenousFormat';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportExogenousFormat';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Despachador que enruta la generación del reporte de información exógena al procedimiento específico según el código de formato recibido en el XML de criterios.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExogenousFormat';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener un nodo /Data con el elemento Format poblado con un código de formato soportado; El procedimiento específico de formato (SP_ExogenaFormatXXXX) debe existir en el esquema GeneralLedger', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExogenousFormat';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se ejecuta uno de los SP de formato por invocación (estructura IF/ELSE IF excluyente); Solo se aceptan los códigos de formato: 1001, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010, 1011, 1012, 1056, 1647, 2275, 2276; Cualquier error es capturado y transformado en un resultset con Code=''999'' en lugar de propagar la excepción; El XML de criterios se reenvía sin modificación al SP destino', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExogenousFormat';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena; Formatos tributarios (DIAN); Contabilidad general', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExogenousFormat';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Cuando ocurre una excepción en el TRY, devuelve un resultset con Code=''999'' y Message con el texto y línea del error; [RETURN_RESULT] (resultset): Cuando Format coincide con un código soportado, delega la ejecución al SP correspondiente cuyo resultset se retorna al llamador', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExogenousFormat';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Format = ''1001'' → Ejecuta SP_ExogenaFormat1001 else Evalúa siguiente formato; si @Format = ''1003'' → Ejecuta SP_ExogenaFormat1003; si @Format = ''1004'' → Ejecuta SP_ExogenaFormat1004; si @Format = ''1005'' → Ejecuta SP_ExogenaFormat1005; si @Format = ''1006'' → Ejecuta SP_ExogenaFormat1006; si @Format = ''1007'' → Ejecuta SP_ExogenaFormat1007; si @Format = ''1008'' → Ejecuta SP_ExogenaFormat1008; si @Format = ''1009'' → Ejecuta SP_ExogenaFormat1009; si @Format = ''1010'' → Ejecuta SP_ExogenaFormat1010; si @Format = ''1011'' → Ejecuta SP_ExogenaFormat1011; si @Format = ''1012'' → Ejecuta SP_ExogenaFormat1012; si @Format = ''1056'' → Ejecuta SP_ExogenaFormat1056; si @Format = ''1647'' → Ejecuta SP_ExogenaFormat1647; si @Format = ''2275'' → Ejecuta SP_ExogenaFormat2275; si @Format = ''2276'' → Ejecuta SP_ExogenaFormat2276 else Si no coincide ningún formato, no se ejecuta ningún SP y no se retorna resultado', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExogenousFormat';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_ExogenaFormat1001; GeneralLedger.SP_ExogenaFormat1003; GeneralLedger.SP_ExogenaFormat1004; GeneralLedger.SP_ExogenaFormat1005; GeneralLedger.SP_ExogenaFormat1006; GeneralLedger.SP_ExogenaFormat1007; GeneralLedger.SP_ExogenaFormat1008; GeneralLedger.SP_ExogenaFormat1009; GeneralLedger.SP_ExogenaFormat1010; GeneralLedger.SP_ExogenaFormat1011; GeneralLedger.SP_ExogenaFormat1012; GeneralLedger.SP_ExogenaFormat1056; GeneralLedger.SP_ExogenaFormat1647; GeneralLedger.SP_ExogenaFormat2275; GeneralLedger.SP_ExogenaFormat2276', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExogenousFormat';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExogenousFormat';
-- GO
