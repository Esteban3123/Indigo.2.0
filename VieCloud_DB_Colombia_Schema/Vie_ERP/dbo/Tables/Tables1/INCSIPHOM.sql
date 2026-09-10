CREATE TABLE [dbo].[INCSIPHOM] (
    [AUTO]       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODSERIPS]  CHAR (20) NOT NULL,
    [CODSERIPS1] CHAR (20) NOT NULL,
    [TIPOTARIF]  CHAR (1)  NOT NULL,
    CONSTRAINT [PK_INCSIPHOM] PRIMARY KEY NONCLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_INCSIPHOM_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_INCSIPHOM_INCUPSIPS1] FOREIGN KEY ([CODSERIPS1]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tarifa, clasificación de valor facturado (CHAR 1). Determina categoría de cobro para procedimiento o servicio en RIPS, facturación y contrato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'TIPOTARIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'TIPOTARIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'TIPOTARIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de procedimientos y servicios RIPS normalizado (CHAR 20). Identificador estándar del procedimiento, servicio o prestación en el sistema de facturación y reportes sanitarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'CODSERIPS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'CODSERIPS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'CODSERIPS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de procedimientos y servicios RIPS normalizado (CHAR 20). Identificador estándar del procedimiento, servicio o prestación en el sistema de facturación y reportes sanitarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno, identificador único autoincrementable (INT IDENTITY). Clave primaria técnica de la tabla de incidencias de procedimientos y servicios RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación o equivalencia entre dos códigos de servicio (CUPS/IPS), usada para mapear o agrupar procedimientos y servicios de salud según el tipo de tarifa aplicada. Permite identificar qué servicios son homólogos o equivalentes entre sí dentro del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM';
