CREATE TABLE [dbo].[ADEMPRESA] (
    [CODEMPRES] CHAR (5)     NOT NULL,
    [DESEMPRES] CHAR (80)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_ADEmpresa] PRIMARY KEY CLUSTERED ([CODEMPRES] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador o código de auditoría de la empresa, marca numérica (NUMERIC 18) para control, seguimiento y validación de procesos auditables asociados a la entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social de la empresa, denominación completa (CHAR 80) de la entidad prestadora, IPS, centro de atención o administradora de servicios de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA', @level2type = N'COLUMN', @level2name = N'DESEMPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA', @level2type = N'COLUMN', @level2name = N'DESEMPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA', @level2type = N'COLUMN', @level2name = N'DESEMPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la empresa, identificador único (CHAR 5), clave primaria para rastrear la entidad prestadora de servicios de salud o empresa administrativa en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA', @level2type = N'COLUMN', @level2name = N'CODEMPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA', @level2type = N'COLUMN', @level2name = N'CODEMPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA', @level2type = N'COLUMN', @level2name = N'CODEMPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Empresas o entidades registradas en el sistema. Guarda el catálogo de empresas (aseguradoras, empleadores, entidades contratantes) con su código identificador y nombre, usado como maestro de referencia en contratos, admisiones y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA';
