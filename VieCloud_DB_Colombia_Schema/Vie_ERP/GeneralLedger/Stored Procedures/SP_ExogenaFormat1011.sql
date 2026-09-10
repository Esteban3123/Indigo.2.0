-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-11
-- Description:	SP que genera la informacion para el XML Formato 1011
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat1011]
   @xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @YearData INT,
			@ExogenousFormatId INT

	SELECT	@YearData = t.x.value('Year[1]','int'),
			@ExogenousFormatId = t.x.value('ExogenousFormatId[1]','int')
	FROM @xmlCriterias.nodes('/Data') t(x)

    SELECT	v.Concept,
			CAST(SUM(IIF(v.ConceptType = 1, v.Value, 0)) AS DECIMAL(18, 0)) AS Balance
	FROM GeneralLedger.ViewReportExogenousFormat v
	WHERE v.Year = @YearData
		AND v.ExogenousFormatId = @ExogenousFormatId
		AND v.[Format] = 1011
	GROUP BY v.Concept
	ORDER BY v.Concept
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la información contable requerida para el Formato 1011 de información exógena tributaria (DIAN), agrupando los saldos del libro mayor por concepto para un año fiscal y formato específicos. Recibe los criterios de búsqueda (año fiscal e identificador del formato exógeno) empaquetados en XML, los desempaqueta y consulta la vista GeneralLedger.ViewReportExogenousFormat filtrando por el formato 1011. Retorna cada concepto contable con su saldo acumulado (suma de valores de tipo 1), ordenado por concepto, listo para construir el XML que se reporta a la DIAN como información exógena.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1011';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1011';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de información exógena Formato 1011 sumando los saldos por concepto para un año y formato exógeno determinados.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1011';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener los nodos /Data/Year y /Data/ExogenousFormatId; Debe existir información en la vista de reporte exógeno para el año y formato exógeno indicados', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1011';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan registros del Formato 1011; El Balance se calcula únicamente con valores cuyo ConceptType=1; Los resultados se agrupan y ordenan por Concepto; El Balance se entrega como entero (DECIMAL(18,0)) sin decimales', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1011';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena; Formato 1011; Concepto contable; Saldo (Balance)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1011';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.ViewReportExogenousFormat: Cuando Year=@YearData, ExogenousFormatId=@ExogenousFormatId y Format=1011, retorna por cada Concepto la suma de Value (solo si ConceptType=1) como Balance', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1011';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ConceptType = 1 → Suma el Value al Balance del concepto else Suma 0 (no aporta al Balance)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1011';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1011';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1011';
-- GO
