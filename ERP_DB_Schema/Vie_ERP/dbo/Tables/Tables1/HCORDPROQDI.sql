CREATE TABLE [dbo].[HCORDPROQDI] (
    [ID]          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOPROCED]  INT       NOT NULL,
    [CODPRODUC]   CHAR (20) NOT NULL,
    [CANTSOLICIT] INT       NOT NULL,
    [CANTENTREG]  INT       NOT NULL,
    [MATESTADO]   TINYINT   NOT NULL,
    CONSTRAINT [PK_HCORDPROQDI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDPROQDI_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[HCORDPROQDI] NOCHECK CONSTRAINT [FK_HCORDPROQDI_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la solicitud:  1-> Solicitado   2-> Orden Compra generada para el producto.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'MATESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad entregada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'CANTENTREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Solicitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'CANTSOLICIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del producto de osteosintesis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del autonumerico del procedimiento qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'AUTOPROCED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del autonumerico del procedimiento qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'ID';

