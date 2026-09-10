CREATE TABLE [dbo].[HOMO_GRUPO_ETARIO] (
    [ID_GRUPO_ETAREO]   INT        NULL,
    [GRUPO_ETAREO_EDAD] FLOAT (53) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de grupos etarios utilizados para clasificar pacientes según rangos de edad. Permite segmentar la población atendida por franjas de edad para análisis estadísticos y reportería clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HOMO_GRUPO_ETARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HOMO_GRUPO_ETARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del grupo etario (ej: 1=neonatos, 2=pediátrico, 3=adulto, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HOMO_GRUPO_ETARIO', @level2type = N'COLUMN', @level2name = N'ID_GRUPO_ETAREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HOMO_GRUPO_ETARIO', @level2type = N'COLUMN', @level2name = N'ID_GRUPO_ETAREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de edad o límite del rango que define el grupo etario (edad en años).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HOMO_GRUPO_ETARIO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HOMO_GRUPO_ETARIO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_EDAD';
