CREATE TABLE [dbo].[HCORDPROQD] (
    [ID]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOPROCED]  INT          NOT NULL,
    [CODPRODUC]   CHAR (20)    NOT NULL,
    [CANTSOLICIT] INT          NOT NULL,
    [CANTENTREG]  INT          CONSTRAINT [DF_HCORDPROQD_CANTENTREG] DEFAULT ((0)) NOT NULL,
    [MATESTADO]   TINYINT      NOT NULL,
    [NUMORDEN]    VARCHAR (20) NULL,
    CONSTRAINT [PK_HCORDPROQD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDPROQD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[HCORDPROQD] NOCHECK CONSTRAINT [FK_HCORDPROQD_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que indica el numero de orden generado  lo afecta VIE ERP cuando hacen la orden de compra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'NUMORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la solicitud:   1-> Solicitado.   2-> Orden Compra generada para el producto, estado que lo coloca vie erp.       ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'MATESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad entregada, la afecta VIE ERP cuando hacen la orden de compra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'CANTENTREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Solicitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'CANTSOLICIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del producto de osteosintesis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del autonumerico del procedimiento qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'AUTOPROCED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'ID';

