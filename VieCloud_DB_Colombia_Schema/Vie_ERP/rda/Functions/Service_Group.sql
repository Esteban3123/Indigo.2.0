

CREATE FUNCTION [rda].[Service_Group] (@Tipo as int)
RETURNS varchar (60)
AS
BEGIN

declare @grupo varchar(60)
SET @grupo=
     CASE @Tipo
		WHEN 1  THEN 'Atención inmediata'
		WHEN 2  THEN 'Internación'
		WHEN 3  THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 4  THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 5  THEN 'Internación'
		WHEN 6  THEN 'Internación'
		WHEN 7  THEN 'Internación'
		WHEN 8  THEN 'Internación'
		WHEN 9  THEN 'Internación'
		WHEN 10 THEN 'Internación'
		WHEN 11 THEN 'Internación'
		WHEN 12 THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 13 THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 14 THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 15 THEN 'Consulta Externa'
		WHEN 16 THEN 'Internación'
		WHEN 17 THEN 'Internación'
		WHEN 18 THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 19 THEN 'Quirúrgico'
		WHEN 20 THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 21 THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 22 THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 23 THEN 'Internación'
		WHEN 24 THEN 'Consulta Externa'
		WHEN 30 THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 31 THEN 'Consulta Externa'
		WHEN 32 THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 33 THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 34 THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 35 THEN 'Quirúrgico'
		WHEN 36 THEN 'Apoyo diagnóstico y complementación terapéutica'
		WHEN 37 THEN 'Apoyo diagnóstico y complementación terapéutica'
    ELSE NULL
END
RETURN @grupo
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que convierte un código numérico de tipo de servicio en su agrupación funcional hospitalaria, retornando una de cinco categorías: "Atención inmediata", "Internación", "Apoyo diagnóstico y complementación terapéutica", "Consulta Externa" o "Quirúrgico". Sirve como tabla de lookup codificada para clasificar servicios en reportes o procesos que requieran agrupar tipos de atención bajo categorías estándar del modelo prestacional.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'Service_Group';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'Service_Group';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Clasifica un código numérico de tipo de servicio en uno de los grupos estándar de servicios de salud (Atención inmediata, Internación, Apoyo diagnóstico, Consulta Externa o Quirúrgico) usado en reportes RDA/RIPS.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'Service_Group';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un código entero de tipo de servicio; valores fuera del catálogo soportado producen NULL.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'Service_Group';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre es uno de cinco grupos fijos del RIPS/RDA: ''Atención inmediata'', ''Internación'', ''Apoyo diagnóstico y complementación terapéutica'', ''Consulta Externa'', ''Quirúrgico'', o NULL.; Los códigos de tipo 25 a 29 no están mapeados y producen NULL.; El tipo de retorno es varchar(60) y nunca excede esa longitud por los literales codificados.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'Service_Group';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Atención inmediata; Internación; Apoyo diagnóstico y complementación terapéutica; Consulta Externa; Quirúrgico; Grupo de servicio', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'Service_Group';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo = 1 → Retorna ''Atención inmediata''; si Tipo IN (2,5,6,7,8,9,10,11,16,17,23) → Retorna ''Internación''; si Tipo IN (3,4,12,13,14,18,20,21,22,30,32,33,34,36,37) → Retorna ''Apoyo diagnóstico y complementación terapéutica''; si Tipo IN (15,24,31) → Retorna ''Consulta Externa''; si Tipo IN (19,35) → Retorna ''Quirúrgico''; si Tipo no coincide con ningún valor mapeado (incluye 25,26,27,28,29 y >37) → Retorna NULL', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'Service_Group';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'FUNCTION', @level1name=N'Service_Group';
GO
