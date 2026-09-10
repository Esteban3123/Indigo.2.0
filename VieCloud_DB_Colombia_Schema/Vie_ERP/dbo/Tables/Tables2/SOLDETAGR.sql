CREATE TABLE [dbo].[SOLDETAGR] (
    [AUTO]      INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [COMAUTON]  INT NOT NULL,
    [AGRUPAUTO] INT NOT NULL,
    CONSTRAINT [PK_SOLDETAGR] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico (INT, FK) de la agrupación de artículos; identifica el grupo de productos enlazados a la compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR', @level2type = N'COLUMN', @level2name = N'AGRUPAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la Agrupacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR', @level2type = N'COLUMN', @level2name = N'AGRUPAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR', @level2type = N'COLUMN', @level2name = N'AGRUPAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico (INT, FK) de la solicitud de compra asociada; clave para vincular órdenes de compra a agrupaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR', @level2type = N'COLUMN', @level2name = N'COMAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'contiene el Autonumerico de la solicitud de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR', @level2type = N'COLUMN', @level2name = N'COMAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR', @level2type = N'COLUMN', @level2name = N'COMAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico, INT, PK) de la relación entre solicitud de compra y agrupación en SOLDETAGR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de agrupaciones de solicitudes: relaciona cada ítem de una solicitud con su agrupación correspondiente, vinculando el registro automático del detalle con el encabezado de la solicitud y el grupo al que pertenece.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETAGR';
