CREATE TABLE [dbo].[HCCTRNPOS] (
    [CODCONCEC]  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMSUMINI]  CHAR (20)    NOT NULL,
    [CODPRODUC]  CHAR (20)    NOT NULL,
    [FECSUMINI]  DATETIME     NOT NULL,
    [IPRCODIGO]  CHAR (20)    NOT NULL,
    [CANTENTREG] INT          NOT NULL,
    [CONCNOPOS]  CHAR (10)    NULL,
    [CODCUM]     VARCHAR (20) NULL,
    [NUMINGRES]  CHAR (10)    NULL,
    [NUMFOLIO]   NCHAR (10)   NULL,
    CONSTRAINT [PK_HCCTRNPOS_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCCTRNPOS_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRNPOS_HCCTRNPOS1] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCCTRNPOS] ([CODCONCEC]),
    CONSTRAINT [FK_HCCTRNPOS_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCCTRNPOS_CODPODUC_NUMINGRES_NUMFOLIO_CODCUM]
    ON [dbo].[HCCTRNPOS]([CODPRODUC] ASC, [NUMINGRES] ASC, [NUMFOLIO] ASC)
    INCLUDE([CODCUM]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio, referencia documental del suministro entregado al paciente en atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'NUMFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'número de folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'NUMFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'NUMFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente, identificador único del episodio de atención/hospitalización (FK ADINGRESO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'número de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUM (Código Único de Medicamento), registro sanitario del producto farmacéutico distribuido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CODCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código CUM producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CODCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CODCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo del producto No POS (no incluido en Plan Obligatorio de Salud), medicamento fuera de cobertura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CONCNOPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concecutivo del producto no pos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CONCNOPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CONCNOPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad entregada al paciente, número de unidades de medicamento o producto suministrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CANTENTREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad entregada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CANTENTREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CANTENTREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUM en DGH (Dirección General de Farmacovigilancia), identificador de producto en registro de invima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CUM en DGH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la orden de suministro, timestamp del registro/solicitud de medicamento o producto al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'FECSUMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'FECSUMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'FECSUMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del producto, identificador único en catálogo de farmacia (FK IHLISTPRO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del suministro, identificador de la transacción o lote de dispensación de medicamento/producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'NUMSUMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número del suministro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'NUMSUMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'NUMSUMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo, PK auto-incremental de transacción de suministro a paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de transacciones de posología y suministro de medicamentos a pacientes: controla las entregas de productos farmacéuticos, incluyendo cantidad entregada, fecha de suministro y relación con el ingreso del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS';
