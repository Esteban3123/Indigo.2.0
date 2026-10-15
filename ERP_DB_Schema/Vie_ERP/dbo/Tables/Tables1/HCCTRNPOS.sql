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
ALTER TABLE [dbo].[HCCTRNPOS] NOCHECK CONSTRAINT [FK_HCCTRNPOS_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCTRNPOS] NOCHECK CONSTRAINT [FK_HCCTRNPOS_HCCTRNPOS1];


GO
ALTER TABLE [dbo].[HCCTRNPOS] NOCHECK CONSTRAINT [FK_HCCTRNPOS_IHLISTPRO];


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRNPOS_CODPODUC_NUMINGRES_NUMFOLIO_CODCUM]
    ON [dbo].[HCCTRNPOS]([CODPRODUC] ASC, [NUMINGRES] ASC, [NUMFOLIO] ASC)
    INCLUDE([CODCUM]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'número de folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'NUMFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'número de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código CUM producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CODCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concecutivo del producto no pos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CONCNOPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad entregada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CANTENTREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CUM en DGH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'IPRCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'FECSUMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del suministro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'NUMSUMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNPOS', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

