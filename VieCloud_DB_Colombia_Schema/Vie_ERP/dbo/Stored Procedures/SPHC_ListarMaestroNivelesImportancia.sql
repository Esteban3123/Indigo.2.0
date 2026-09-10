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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el catálogo maestro de niveles de importancia o prioridad utilizados en la historia clínica, consolidando en un único resultado los niveles correspondientes a cuatro disciplinas clínicas: Enfermería, Instrumentador quirúrgico, Químico farmacéutico y Terapia. Para cada nivel devuelve el código, la descripción, el tipo de profesional al que pertenece y el color asociado para su visualización. Consulta dos tablas de catálogo (HCNIVIMPN y HCNIVIMPT), filtrando por tipo de profesional en la primera. Se usa para poblar selectores o listas desplegables en formularios de historia clínica donde se requiere clasificar registros según su criticidad, urgencia o relevancia clínica por rol profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarMaestroNivelesImportancia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarMaestroNivelesImportancia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el catálogo unificado de niveles de importancia clínicos clasificados por tipo de profesional (Enfermería, Instrumentador quirúrgico, Químico farmacéutico) y de Terapia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMaestroNivelesImportancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas HCNIVIMPN y HCNIVIMPT deben contener los registros maestros de niveles de importancia.; HCNIVIMPN debe manejar la columna TIPO con valores ''1'' (Enfermería), ''3'' (Instrumentador quirúrgico) y ''4'' (Químico farmacéutico).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMaestroNivelesImportancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los niveles de importancia de Enfermería, Instrumentador quirúrgico y Químico farmacéutico provienen de HCNIVIMPN filtrados por TIPO (''1'',''3'',''4'').; Los niveles de tipo Terapia provienen exclusivamente de HCNIVIMPT.; Las descripciones se devuelven con RTRIM (sin espacios finales).; El TIPO=2 no se incluye en la salida.; Cada fila incluye un color asociado (COLNIVIMP).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMaestroNivelesImportancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nivel de importancia clínico; Enfermería; Instrumentador quirúrgico; Químico farmacéutico; Terapia; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMaestroNivelesImportancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna un resultset unificado (UNION ALL) con niveles de importancia: TIPO=''1'' como Enfermería, TIPO=''3'' como Instrumentador quirúrgico, TIPO=''4'' como Químico farmacéutico desde HCNIVIMPN, y todos los registros de HCNIVIMPT como Terapia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMaestroNivelesImportancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPO = ''1'' en HCNIVIMPN → Se etiqueta el nivel como ''Enfermería''; si TIPO = ''3'' en HCNIVIMPN → Se etiqueta el nivel como ''Instrumentador quirúrgico''; si TIPO = ''4'' en HCNIVIMPN → Se etiqueta el nivel como ''Químico farmacéutico''; si Registros provenientes de HCNIVIMPT → Se etiquetan siempre como ''Terapia'' sin filtro por TIPO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMaestroNivelesImportancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCNIVIMPN; dbo.HCNIVIMPT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMaestroNivelesImportancia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarMaestroNivelesImportancia';
-- GO
