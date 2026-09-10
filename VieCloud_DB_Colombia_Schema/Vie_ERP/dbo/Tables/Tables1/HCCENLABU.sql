CREATE TABLE [dbo].[HCCENLABU] (
    [CODCENLAB] CHAR (80)     NULL,
    [DESCENLAB] VARCHAR (200) NULL,
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGOIPS] CHAR (100)    NULL,
    CONSTRAINT [PK_HCCENLABU] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCENLABU_ADCONTIPS] FOREIGN KEY ([CODIGOIPS]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la IPS (Institución Prestadora de Servicios) relacionado; clave foránea a tabla ADCONTIPS para identificar el centro de atención o prestador de salud responsable del laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del ips relacionado con tabla ADCONTIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de la relación entre centro de laboratorio e IPS; clave primaria de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre del centro de remisión de laboratorios; nombre de la entidad donde se procesan análisis clínicos y exámenes de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'DESCENLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de Remision de Laboratorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'DESCENLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'DESCENLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del registro del centro de remisión de laboratorios; identificador del laboratorio o unidad funcional de análisis clínicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'CODCENLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Registro Centros de remision ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'CODCENLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU', @level2type = N'COLUMN', @level2name = N'CODCENLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de centros de laboratorio o laboratorios clínicos habilitados en la institución. Relaciona el código interno del centro de laboratorio con su código IPS y su nombre descriptivo, permitiendo identificar los laboratorios disponibles para la solicitud y procesamiento de exámenes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCENLABU';
