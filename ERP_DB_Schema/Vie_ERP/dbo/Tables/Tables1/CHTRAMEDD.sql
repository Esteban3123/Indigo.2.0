CREATE TABLE [dbo].[CHTRAMEDD] (
    [CODCONCEC] NUMERIC (18) NOT NULL,
    [CODPRODUC] CHAR (20)    NOT NULL,
    [CANENVPRO] INT          NOT NULL,
    CONSTRAINT [PK_HCFARMDEVD] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_CHTRAMEDD_CHTRAMEDC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[CHTRAMEDC] ([CODCONCEC]),
    CONSTRAINT [FK_CHTRAMEDD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[CHTRAMEDD] NOCHECK CONSTRAINT [FK_CHTRAMEDD_CHTRAMEDC];


GO
ALTER TABLE [dbo].[CHTRAMEDD] NOCHECK CONSTRAINT [FK_CHTRAMEDD_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Entregada del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD', @level2type = N'COLUMN', @level2name = N'CANENVPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTRAMEDD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

