CREATE TABLE [dbo].[INMUNICIP] (
    [DEPMUNCOD] CHAR (5)     NOT NULL,
    [DEPCODIGO] CHAR (2)     NOT NULL,
    [MUNCODIGO] CHAR (3)     NOT NULL,
    [MUNNOMBRE] CHAR (40)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INMunicip] PRIMARY KEY CLUSTERED ([DEPMUNCOD] ASC),
    CONSTRAINT [FK_INMunicip_INDepatar] FOREIGN KEY ([DEPCODIGO]) REFERENCES [dbo].[INDEPARTA] ([depcodigo])
);






GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría, identificador único para trazabilidad y control de cambios en registros municipales (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del municipio, denominación oficial de la entidad territorial (CHAR 40)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'MUNNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'MUNNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'MUNNOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del municipio, identificador numérico de tres dígitos para clasificación territorial (CHAR 3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'MUNCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'MUNCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'MUNCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del departamento, identificador de dos dígitos de la división política superior; referencia a tabla INDEPARTA (CHAR 2, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'DEPCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'DEPCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'DEPCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código compuesto departamento-municipio, clave primaria que combina código de departamento y municipio para ubicación geográfica única (CHAR 5, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Municipio y Departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de municipios de Colombia. Relaciona cada municipio con su departamento, incluyendo el código compuesto, el código del departamento, el código del municipio y su nombre oficial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP';
