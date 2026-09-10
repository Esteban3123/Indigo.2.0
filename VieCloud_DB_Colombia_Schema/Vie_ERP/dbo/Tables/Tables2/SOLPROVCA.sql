CREATE TABLE [dbo].[SOLPROVCA] (
    [PROCATAUT] TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODPROVE]  NUMERIC (18) NOT NULL,
    [CODCATE]   INT          NOT NULL,
    CONSTRAINT [PK_SOLProvCat] PRIMARY KEY CLUSTERED ([PROCATAUT] ASC),
    CONSTRAINT [FK_SOLPROVCAT_SOLCAT] FOREIGN KEY ([CODCATE]) REFERENCES [dbo].[SOLCATEGO] ([CATEAUTON]),
    CONSTRAINT [FK_SOLPROVCAT_SOLPROVE] FOREIGN KEY ([CODPROVE]) REFERENCES [dbo].[SOLPROVEE] ([PROVAUTO])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de categoría de solicitud (INT), referencia a categoría en SOLCATEGO, clasifica tipos de solicitudes o servicios del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA', @level2type = N'COLUMN', @level2name = N'CODCATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo categoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA', @level2type = N'COLUMN', @level2name = N'CODCATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA', @level2type = N'COLUMN', @level2name = N'CODCATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del proveedor (NUMERIC 18), referencia a proveedor en SOLPROVEE, identifica el proveedor o prestador de servicios asociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA', @level2type = N'COLUMN', @level2name = N'CODPROVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA', @level2type = N'COLUMN', @level2name = N'CODPROVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA', @level2type = N'COLUMN', @level2name = N'CODPROVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID autonumérico de la asociación proveedor-categoría (TINYINT IDENTITY), clave primaria única de la relación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA', @level2type = N'COLUMN', @level2name = N'PROCATAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA', @level2type = N'COLUMN', @level2name = N'PROCATAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA', @level2type = N'COLUMN', @level2name = N'PROCATAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de las categorías autorizadas por proveedor; asocia cada proveedor con las categorías de productos o servicios que tiene habilitados para suministrar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA';
