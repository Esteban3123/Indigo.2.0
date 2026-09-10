CREATE TABLE [dbo].[INSALARIM] (
    [ISALCODIG] CHAR (3)        NOT NULL,
    [ISALNOMBR] CHAR (50)       NOT NULL,
    [ISALVALOR] NUMERIC (18, 2) NOT NULL,
    [INDAUDFOR] NUMERIC (18)    NOT NULL,
    CONSTRAINT [PK_INSalamin] PRIMARY KEY CLUSTERED ([ISALCODIG] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría; identificador único del registro de auditoría vinculado a la modificación o creación del salario mínimo; tipo: NUMERIC(18)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del salario mínimo; monto en pesos/moneda vigente del salario mínimo legal; tipo: NUMERIC(18,2); búsqueda: salario mínimo, valor, remuneración base', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'ISALVALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Salario Mininmo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'ISALVALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'ISALVALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del salario mínimo; nombre o denominación del período o categoría del salario mínimo (ej: año vigente, región); tipo: CHAR(50)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'ISALNOMBR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'ISALNOMBR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'ISALNOMBR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del salario mínimo; identificador único de tres caracteres del registro de salario mínimo; Clave primaria; tipo: CHAR(3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'ISALCODIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Salario Minimo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'ISALCODIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM', @level2type = N'COLUMN', @level2name = N'ISALCODIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de salas o áreas de atención con su valor o tarifa asociada. Registra cada sala (quirófano, urgencias, hospitalización, etc.) con su código, nombre y costo unitario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INSALARIM';
