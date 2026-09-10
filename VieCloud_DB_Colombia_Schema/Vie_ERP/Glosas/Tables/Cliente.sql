CREATE TABLE [Glosas].[Cliente] (
    [VIE18Id] INT          NULL,
    [NIT]     VARCHAR (20) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Identificación Tributaria (NIT) del cliente/empresa; identificador fiscal único de la entidad responsable de la facturación o contrato en el sistema de glosas. Tipo: VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Cliente', @level2type = N'COLUMN', @level2name = N'NIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Cliente', @level2type = N'COLUMN', @level2name = N'NIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Cliente', @level2type = N'COLUMN', @level2name = N'NIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del cliente en el sistema Indigo Vie Cloud (VIE18); clave de referencia interna que vincula el registro de glosa con el cliente maestro. Tipo: INT, nullable.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Cliente', @level2type = N'COLUMN', @level2name = N'VIE18Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desconocido', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Cliente', @level2type = N'COLUMN', @level2name = N'VIE18Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Cliente', @level2type = N'COLUMN', @level2name = N'VIE18Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clientes o entidades pagadoras del módulo de glosas: aseguradoras, EPS, empresas o fondos con quienes se gestionan cobros y objeciones de facturación. Relaciona el identificador interno del cliente con su NIT.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Cliente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Cliente';
