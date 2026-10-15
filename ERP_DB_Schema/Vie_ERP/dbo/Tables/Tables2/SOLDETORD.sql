CREATE TABLE [dbo].[SOLDETORD] (
    [DETORDAUT] INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDCONSEC] VARCHAR (50)    NOT NULL,
    [PRODAUTON] INT             NOT NULL,
    [CANTCOMPR] INT             NOT NULL,
    [IVADETORD] NUMERIC (5, 2)  NOT NULL,
    [VALORNETO] NUMERIC (18, 2) NOT NULL,
    [DESCUENTO] NUMERIC (5, 2)  NOT NULL,
    [ORDCANREC] INT             NULL,
    CONSTRAINT [PK_SOLDETORD] PRIMARY KEY CLUSTERED ([DETORDAUT] ASC),
    CONSTRAINT [FK_SOLDETORD_SOLORDCOM] FOREIGN KEY ([ORDCONSEC]) REFERENCES [dbo].[SOLORDCOM] ([ORDCONSEC]),
    CONSTRAINT [FK_SOLDETORD_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON])
);


GO
ALTER TABLE [dbo].[SOLDETORD] NOCHECK CONSTRAINT [FK_SOLDETORD_SOLORDCOM];


GO
ALTER TABLE [dbo].[SOLDETORD] NOCHECK CONSTRAINT [FK_SOLDETORD_SOLPRODUC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la cantidad recibida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'ORDCANREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el descuento de la compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'DESCUENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el valor neto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'VALORNETO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene autonumerico del iva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'IVADETORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la cantidad comprada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'CANTCOMPR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'PRODAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el consecutivo de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETORD', @level2type = N'COLUMN', @level2name = N'DETORDAUT';

