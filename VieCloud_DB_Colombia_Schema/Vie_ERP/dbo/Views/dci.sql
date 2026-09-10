

CREATE VIEW [dbo].[dci]
AS
SELECT     CODDCIMED, DESDCIMED
FROM         dbo.IHDCIMEDI
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista que expone un subconjunto reducido del catálogo maestro de medicamentos o insumos médicos, mostrando únicamente el código y la descripción de cada ítem. Sirve como lookup simplificado para consultas rápidas sobre denominaciones comunes internacionales (DCI) de medicamentos en procesos de prescripción, dispensación o registros clínicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'dci';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'dci';
GO
