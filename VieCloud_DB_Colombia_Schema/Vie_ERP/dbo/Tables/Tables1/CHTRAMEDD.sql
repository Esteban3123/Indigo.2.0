CREATE TABLE [dbo].[CHTRAMEDD] (
    [CODCONCEC] NUMERIC (18) NOT NULL,
    [CODPRODUC] CHAR (20) NOT NULL,
    [CANENVPRO] INT NOT NULL,
    [ID] INT IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_CHTRAMEDD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHTRAMEDD_CHTRAMEDC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[CHTRAMEDC] ([CODCONCEC]),
    CONSTRAINT [FK_CHTRAMEDD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades entregadas del producto farmacéutico o medicamento; registro de dispensación o suministro en farmacia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD', @level2type = N'COLUMN', @level2name = N'CANENVPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Entregada del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD', @level2type = N'COLUMN', @level2name = N'CANENVPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD', @level2type = N'COLUMN', @level2name = N'CANENVPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto farmacéutico o medicamento; identificador único del artículo de farmacia referenciado en catálogo (FK a IHLISTPRO).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo de trámite de medicamentos; identificador único del registro de dispensación farmacéutica (FK a CHTRAMEDC).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de medicamentos o productos asociados a una orden de tratamiento médico. Registra qué producto o medicamento fue prescrito, en qué cantidad, dentro de un concepto o renglón de la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD';
