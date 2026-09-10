CREATE TABLE [dbo].[IHGRUFARM] (
    [CODGRUFAR] VARCHAR (20) NOT NULL,
    [DESGRUFAR] CHAR (100)   NOT NULL,
    CONSTRAINT [PK_IHGRUFARM] PRIMARY KEY CLUSTERED ([CODGRUFAR] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del Grupo Farmacológico: nombre completo y clasificación del grupo terapéutico de medicamentos (ej: Antibióticos, Antiinflamatorios, Analgésicos). Tipo SQL: CHAR(100). Utilizado en catálogos de medicinas, recetas, dispensación farmacéutica y RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHGRUFARM', @level2type = N'COLUMN', @level2name = N'DESGRUFAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Grupo Farmacologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHGRUFARM', @level2type = N'COLUMN', @level2name = N'DESGRUFAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHGRUFARM', @level2type = N'COLUMN', @level2name = N'DESGRUFAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Grupo Farmacológico: identificador único y alfanumérico del grupo terapéutico para clasificación de medicamentos en recetas, inventario y facturación. Tipo SQL: VARCHAR(20). Clave primaria. Sinónimos: código terapéutico, clasificación farmacológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHGRUFARM', @level2type = N'COLUMN', @level2name = N'CODGRUFAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Grupo Farmacologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHGRUFARM', @level2type = N'COLUMN', @level2name = N'CODGRUFAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHGRUFARM', @level2type = N'COLUMN', @level2name = N'CODGRUFAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupos farmacológicos o categorías de medicamentos. Permite clasificar los fármacos por familia terapéutica (antibióticos, analgésicos, antihipertensivos, etc.) para su gestión en el módulo de farmacia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHGRUFARM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHGRUFARM';
