
CREATE FUNCTION [Report].[GetReportName]
(	
	@IDHCHISPACA INT
)
RETURNS VARCHAR(50) 
BEGIN

	DECLARE @StoryType INT, @ReportName AS VARCHAR(50)

	SELECT @StoryType = StoryType FROM HCHISPACA WHERE ID = @IDHCHISPACA

	SELECT @ReportName = 
		CASE @StoryType
			WHEN 1 THEN '' 
			WHEN 2 THEN 'rptHCEvolucion' 
			WHEN 3 THEN 'rptHCEvolucion'
			WHEN 4 THEN 'rptHCEvolucion'
			WHEN 5 THEN 'rptHCIngreso'
			WHEN 6 THEN 'rptHCNotas' 
			WHEN 7 THEN 'rptHCNotas' 
			WHEN 8 THEN 'rptHCServiciosApoyo' 
			WHEN 9 THEN 'rptHCNotas' 
			WHEN 10 THEN 'rptHCNotas' 
			WHEN 11 THEN 'rptHCNotas' 		
			WHEN 12 THEN 'rptHCIngreso' 
			WHEN 13 THEN 'rptHCNotas'
			WHEN 14 THEN 'rptHCNotas'
			WHEN 15 THEN 'rptHCIngreso'
			WHEN 16 THEN 'rptHCNotas'
			WHEN 17 THEN 'rptHCPreAnestesia'
			WHEN 18 THEN 'rptHCPreAnestesia'
			WHEN 19 THEN 'rptHCNotas'
			WHEN 20 THEN 'rptHCAtencionParto'
			WHEN 21 THEN 'rptHCRecienNacido'
			WHEN 22 THEN 'rptHCExtramural'
			ELSE ''
		END

	 RETURN @ReportName
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que, dado el identificador de un registro de historia clínica (`HCHISPACA`), consulta su tipo de documento (`StoryType`) y retorna el nombre del reporte SSRS/RDL correspondiente. El mapeo cubre hasta 22 tipos de historia clínica, incluyendo evoluciones, ingresos, notas, servicios de apoyo, pre-anestesia, atención de parto, recién nacido y extramural. Los tipos sin reporte asignado devuelven cadena vacía.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'GetReportName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'GetReportName';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el nombre del reporte (archivo .rpt) que corresponde a una historia clínica según su tipo de historia (StoryType).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'GetReportName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCHISPACA con el ID indicado; si no existe, StoryType queda NULL y se retorna cadena vacía.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'GetReportName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre es uno del conjunto fijo: '''', ''rptHCEvolucion'', ''rptHCIngreso'', ''rptHCNotas'', ''rptHCServiciosApoyo'', ''rptHCPreAnestesia'', ''rptHCAtencionParto'', ''rptHCRecienNacido'' o ''rptHCExtramural''.; Varios tipos de historia comparten el mismo reporte físico (notas, evolución, ingreso, preanestesia).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'GetReportName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Tipo de historia (StoryType); Reportes clínicos: evolución, ingreso, notas, servicios de apoyo, preanestesia, atención de parto, recién nacido, extramural', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'GetReportName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna VARCHAR(50) con el nombre de reporte mapeado por StoryType; valores no contemplados o StoryType=1 retornan cadena vacía.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'GetReportName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si StoryType = 1 o no mapeado (ELSE) → Retorna cadena vacía; si StoryType IN (2,3,4) → Retorna ''rptHCEvolucion''; si StoryType IN (5,12,15) → Retorna ''rptHCIngreso''; si StoryType IN (6,7,9,10,11,13,14,16,19) → Retorna ''rptHCNotas''; si StoryType = 8 → Retorna ''rptHCServiciosApoyo''; si StoryType IN (17,18) → Retorna ''rptHCPreAnestesia''; si StoryType = 20 → Retorna ''rptHCAtencionParto''; si StoryType = 21 → Retorna ''rptHCRecienNacido''; si StoryType = 22 → Retorna ''rptHCExtramural''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'GetReportName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCHISPACA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'GetReportName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'FUNCTION', @level1name=N'GetReportName';
GO
