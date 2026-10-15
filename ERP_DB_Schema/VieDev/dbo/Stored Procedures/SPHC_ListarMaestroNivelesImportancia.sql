-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarMaestroNivelesImportancia]
AS
BEGIN
	SELECT 
			'Enfermería' AS 'Nivel de importancia',
			CODNIVIMP	AS 'Codigo', 
			RTRIM(DESNIVIMP) AS 'Descripcion',		
			CASE RTRIM(TIPO) WHEN 1 THEN 'Enfermería' END Tipo,
			COLNIVIMP AS Color
		FROM dbo.HCNIVIMPN 
		WHERE TIPO = '1'
	UNION ALL
		SELECT 
			'Instrumentador quirúrgico' AS 'Nivel de importancia',
			CODNIVIMP	AS 'Codigo', 
			RTRIM(DESNIVIMP) AS 'Descripcion',		
			CASE RTRIM(TIPO) WHEN 3 THEN 'Instrumentador quirúrgico' END Tipo,
			COLNIVIMP AS Color
		FROM dbo.HCNIVIMPN
		WHERE TIPO = '3'
		
	UNION ALL
		SELECT 
			'Químico farmacéutico' AS 'Nivel de importancia',
			CODNIVIMP	AS 'Codigo', 
			RTRIM(DESNIVIMP) AS 'Descripcion',		
			CASE RTRIM(TIPO) WHEN 4 THEN 'Químico farmacéutico' END Tipo,
			COLNIVIMP AS Color
		FROM dbo.HCNIVIMPN 
		WHERE TIPO = '4'
	UNION ALL
		SELECT 
			'Terapia' AS 'Nivel de importancia',
			CODNIVIMP AS 'Codigo', 
			RTRIM(DESNIVIMP)  AS 'Descripcion',
			'Terapia' AS Tipo,
			COLNIVIMP AS Color
		FROM dbo.HCNIVIMPT 
END