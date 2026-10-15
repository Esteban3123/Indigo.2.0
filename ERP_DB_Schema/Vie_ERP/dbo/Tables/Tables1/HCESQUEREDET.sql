CREATE TABLE [dbo].[HCESQUEREDET] (
    [ID]           INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCESQUERENID] INT       NOT NULL,
    [CODPRODUC]    CHAR (20) NOT NULL,
    [CANTIDAD]     INT       NOT NULL,
    CONSTRAINT [PK_HCESQUEREDET] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESQUEREDET_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[HCESQUEREDET] NOCHECK CONSTRAINT [FK_HCESQUEREDET_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la cabecera de Esquema Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'HCESQUERENID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del detalle esquema renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'ID';

