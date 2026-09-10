
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